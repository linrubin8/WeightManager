using FastReport;
using FastReport.Data;
using FastReport.Utils;
using LB.Common;
using LB.Page.Helper;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Report
{
    public enum enBuildParmsType
    {
        /// <summary>
        /// 添加新报表
        /// </summary>
        AddNewReport,

        /// <summary>
        /// 更新参数和数据源
        /// </summary>
        ReSetData
    }

    public enum enRequestReportActionType
    {
        Preview,
        DirectPrint,
        DirectFax,
        PreviewContinuously,
        DirectPrintContinuously,
        GenTemplate
    }

    public enum enBuildParmsAndDataActionType
    {
        /// <summary>
        /// 根据模板设置参数的值，或对数据源赋值，不改变模板的参数及数据源的个数类型
        /// </summary>
        SetValue,

        /// <summary>
        /// 生成新的模板，即不用考虑对模板删除原参数及数据源
        /// </summary>
        AddNew,

        /// <summary>
        /// 更新，需要对模板删除原参数及数据源
        /// </summary>
        ReSet
    }


    public class ReportHelper
    {
        public static event EventHandler LBFinishReport;
        /// <summary>
        /// 报表文件保存路径
        /// </summary>
        public static string ReportPath
        {
            get
            {
                string strPath = Path.Combine(Application.StartupPath, "ReportFile");
                if (!Directory.Exists(strPath))
                    Directory.CreateDirectory(strPath);
                return strPath;
            }
        }

        //public static void BuildReportParmsAndData(FastReport.Report report, ReportArgs reportArgs)
        //{
        //    report.Parameters.Clear();
        //    List<FastReport.Data.Parameter> lstParm = new List<FastReport.Data.Parameter>();

        //    if (reportArgs.RecordDR != null)
        //    {
        //        foreach (DataColumn dc in reportArgs.RecordDR.Table.Columns)
        //        {
        //            FastReport.Data.Parameter parm = new FastReport.Data.Parameter(dc.ColumnName);
        //            parm.DataType = dc.DataType;
        //            parm.Value = reportArgs.RecordDR[dc.ColumnName];
        //            lstParm.Add(parm);
        //        }
        //    }

            
        //    if (reportArgs.DSDataSource != null)
        //    {
        //        foreach (DataTable dtSource in reportArgs.DSDataSource.Tables)
        //        {
        //            report.RegisterData(dtSource, dtSource.TableName);
        //            report.GetDataSource(dtSource.TableName).Enabled = true;
        //        }
        //    }
        //}

        /// <summary>
        /// 打开报表设计器
        /// </summary>
        /// <param name="lReportTemplateID"></param>
        /// <param name="reportArgs"></param>
        public static void OpenReportDesign( ReportRequestArgs reportArgs)
        {
            string strReportFileName;
            bool bolExists = RefleshClientReport(reportArgs.ReportTemplateID, out strReportFileName);
            if (bolExists)
            {
                using (FastReport.Report report = new FastReport.Report())
                {
                    report.Load(strReportFileName);
                    
                    BuildParmsAndData(reportArgs, report, enBuildParmsAndDataActionType.AddNew);
                    
                    Form frm = new Form();
                    frm.Icon = FastReport.Utils.Config.PreviewSettings.Icon;
                    frm.Show();
                    report.Design();
                    //report.Designer.cmdNew.CustomAction += CmdNew_CustomAction;
                    report.Dispose();
                    frm.Close();
                }
            }
        }

        /// <summary>
        /// 预览报表
        /// </summary>
        public static void OpenReportDialog(enRequestReportActionType eActionType, ReportRequestArgs reportRequestArgs)
        {
            ProcessStep.AddStep("OpenReportDialog_Start", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
            DataRow drReportTemplateConfig = GetReportTemplateRow(reportRequestArgs.ReportTemplateID);
            int iPrintCount = 1;

            if(eActionType == enRequestReportActionType.DirectPrint)
            {
                iPrintCount = LBConverter.ToInt32(drReportTemplateConfig["PrintCount"]);
                if (iPrintCount <= 0)
                {
                    iPrintCount = 1;
                }
            }

            ProcessStep.AddStep("GetReportTemplateRow", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
            reportRequestArgs.ReportTemplateConfig = drReportTemplateConfig;

            for(int i=0;i< iPrintCount; i++)
            {
                ShowReport(eActionType, reportRequestArgs);
            }
            
            ProcessStep.AddStep("ShowReport_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
        }

        /// <summary>
        /// 添加新报表
        /// </summary>
        /// <param name="e"></param>
        /// <param name="strReportTemplateName"></param>
        /// <returns></returns>
        public static string AddNewReport(ReportRequestArgs e,string strReportTemplateName)
        {
            string strReportPath = "";
            using (FastReport.Report report = new FastReport.Report())
            {
                BuildParmsAndData(e,report, enBuildParmsAndDataActionType.AddNew);

                string strReportFullName = Path.Combine(ReportHelper.ReportPath, strReportTemplateName + ".frx");
                report.Save(strReportFullName);

                strReportPath = strReportFullName;

                //Form frm = new Form();
                //frm.Icon = FastReport.Utils.Config.PreviewSettings.Icon;
                //frm.Show();
                //report.Design();
                report.Dispose();
                //frm.Close();
            }
            return strReportPath;
        }

        /// <summary>
        /// 更新本地报表以及参数
        /// </summary>
        /// <param name="lReportTemplateID"></param>
        public static void ResetLocalReport(long lReportTemplateID, DataSet dsSource, DataRow drRecord)
        {
            DataRow drReportTemplateConfig = GetReportTemplateRow(lReportTemplateID);

            byte[] reportTempleData = (byte[])drReportTemplateConfig["TemplateData"];
            long lReportTypeID = Convert.ToInt64(drReportTemplateConfig["ReportTypeID"]);
            DateTime dtTemplateFileTime = DateTime.Parse(drReportTemplateConfig["TemplateFileTime"].ToString());
            string strReportTemplateName = drReportTemplateConfig["ReportTemplateName"].ToString().Trim();
            string strReportTemplateNameExt = drReportTemplateConfig["ReportTemplateNameExt"].ToString().Trim();
            //检测本地是否存在报表文件，如果不存在或者与服务器比本地文件新时更新本地文件
            string strFileFullName = WriteReportWithCheck(lReportTemplateID, strReportTemplateName, dtTemplateFileTime, reportTempleData, strReportTemplateNameExt);

            ReportRequestArgs args = new ReportRequestArgs(lReportTemplateID, lReportTypeID, dsSource, drRecord);
            using (FastReport.Report report = new FastReport.Report())
            {
                report.Load(strFileFullName);
                BuildParmsAndData(args, report, enBuildParmsAndDataActionType.ReSet);
                report.Save(strFileFullName);
                File.SetLastWriteTime(strFileFullName, DateTime.Now);
            }
        }

        private static void ShowReport(enRequestReportActionType eActionType, ReportRequestArgs e)
        {
            // FastReport 配置
            Config.DesignerSettings.ShowInTaskbar = true;
            Config.DesignerSettings.DefaultFont = new System.Drawing.Font(Config.DesignerSettings.DefaultFont.FontFamily, 10f);
            //Config.DesignerSettings.Icon = TS.Win.Styles.Sheet.AppIcon;
            Config.DesignerSettings.Text = "报表设计器";
            //Config.PreviewSettings.Buttons = PreviewButtons.Print;
            Config.PreviewSettings.ShowInTaskbar = true;
            //Config.PreviewSettings.Icon = TS.Win.Styles.Sheet.AppIcon;

            long lReportTemplateID = Convert.ToInt64(e.ReportTemplateConfig["ReportTemplateID"]);
            byte[] reportTempleData = (byte[])e.ReportTemplateConfig["TemplateData"];
            DateTime dtTemplateFileTime = DateTime.Parse(e.ReportTemplateConfig["TemplateFileTime"].ToString());
            string strReportTemplateName = e.ReportTemplateConfig["ReportTemplateName"].ToString().Trim();
            string strReportTemplateNameExt = e.ReportTemplateConfig["ReportTemplateNameExt"].ToString().Trim();
            //检测本地是否存在报表文件，如果不存在或者与服务器比本地文件新时更新本地文件
            string strFileFullName = WriteReportWithCheck(lReportTemplateID,strReportTemplateName,dtTemplateFileTime, reportTempleData, strReportTemplateNameExt);

            ProcessStep.AddStep("WriteReportWithCheck_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

            // 加载模板
            FastReport.Report report = null;
            
            //Form frm = null;
            try
            {
                //frm = new Form();
                report = new FastReport.Report();
                
                ProcessStep.AddStep("Report_New", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                report.FinishReport += Report_FinishReport;
                report.Load(strFileFullName);
                ProcessStep.AddStep("Report_Load", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                // 纸张设置
                //SetPaperAuto(report, iReportTemplateID);

                BuildParmsAndData(e, report, enBuildParmsAndDataActionType.SetValue);
                ProcessStep.AddStep("BuildParmsAndData", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                //ReportPreviewer previewer = new Report.ReportPreviewer(report);

                SetPrintSettings(report, lReportTemplateID);
                ProcessStep.AddStep("SetPrintSettings", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                if (eActionType == enRequestReportActionType.Preview)//预览
                {
                    //frm.FormBorderStyle = FormBorderStyle.None;
                    //frm.Text = "报表预览[" + strReportTemplateName + "]";
                    //frm.TransparencyKey = frm.BackColor;
                    //frm.Show();

                    //report.Show(true, frm);
                    //LBShowForm.ShowDialog(previewer);
                    report.Show(true);
                }
                else if (eActionType == enRequestReportActionType.DirectPrint)//直接打印
                {
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    //string strPrinterStatus =PrinterHelper.GetPrinterStatus(report.PrintSettings.Printer);
                    ProcessStep.AddStep("Print", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                }
            }
            finally
            {
                try
                {
                    if (report != null)
                    {
                        report.Dispose();
                        //if (frm != null)
                        //{
                        //    frm.Close();
                        //}
                    }
                }
                catch
                {
                }
            }
        }

        private static void CmdNew_CustomAction(object sender, EventArgs e)
        {
            
        }

        public static FastReport.Report GetReport(ReportRequestArgs e)
        {
            DataRow drReportTemplateConfig = GetReportTemplateRow(e.ReportTemplateID);
            e.ReportTemplateConfig = drReportTemplateConfig;

            long lReportTemplateID = Convert.ToInt64(e.ReportTemplateConfig["ReportTemplateID"]);
            byte[] reportTempleData = (byte[])e.ReportTemplateConfig["TemplateData"];
            DateTime dtTemplateFileTime = DateTime.Parse(e.ReportTemplateConfig["TemplateFileTime"].ToString());
            string strReportTemplateName = e.ReportTemplateConfig["ReportTemplateName"].ToString().Trim();
            string strReportTemplateNameExt = e.ReportTemplateConfig["ReportTemplateNameExt"].ToString().Trim();
            //检测本地是否存在报表文件，如果不存在或者与服务器比本地文件新时更新本地文件
            string strFileFullName = WriteReportWithCheck(lReportTemplateID, strReportTemplateName, dtTemplateFileTime, reportTempleData, strReportTemplateNameExt);

            ProcessStep.AddStep("WriteReportWithCheck_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

            // 加载模板
            FastReport.Report report = null;

            //Form frm = null;
            try
            {
                //frm = new Form();
                report = new FastReport.Report();
                ProcessStep.AddStep("Report_New", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                report.FinishReport += Report_FinishReport;
                report.Load(strFileFullName);
                ProcessStep.AddStep("Report_Load", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                // 纸张设置
                //SetPaperAuto(report, iReportTemplateID);

                BuildParmsAndData(e, report, enBuildParmsAndDataActionType.SetValue);
                ProcessStep.AddStep("BuildParmsAndData", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                //ReportPreviewer previewer = new Report.ReportPreviewer(report);

                SetPrintSettings(report, lReportTemplateID);
                ProcessStep.AddStep("SetPrintSettings", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
            }
            catch(Exception ex)
            {

            }
            return report;
        }

        private static void Report_FinishReport(object sender, EventArgs e)
        {
            if (LBFinishReport != null)
            {
                LBFinishReport(sender, e);
            }
        }

        public static string WriteReportWithCheck(long lReportTemplateID,string strReportTemplateName, DateTime dtTemplateFileTime, byte[] reportTempleData, string strReportTemplateNameExt)
        {
            string strFileFullName = Path.Combine(ReportPath, strReportTemplateName + strReportTemplateNameExt);

            // 仅当用户有权限更新模板时，才检查本地模板；否则，以服务器的模板覆盖本地的
            if (File.Exists(strFileFullName))
            {
                DateTime dtLastWriteTime = File.GetLastWriteTime(strFileFullName);
                if (dtLastWriteTime > dtTemplateFileTime)
                {
                    return strFileFullName;
                }
            }

            RefleshClientReport(lReportTemplateID,out strFileFullName);

            return strFileFullName;
        }


        private static DataRow GetReportTemplateRow(long lReportTemplateID)
        {
            DataTable dtReportTemplate = ExecuteSQL.CallView(105, "", "ReportTemplateID=" + lReportTemplateID, "");
            if (dtReportTemplate.Rows.Count > 0)
            {
                return dtReportTemplate.Rows[0];
            }
            return null;
        }

        public static void BuildParmsAndData(ReportRequestArgs e, FastReport.Report report, enBuildParmsAndDataActionType actionType)
        {
            if (actionType == enBuildParmsAndDataActionType.SetValue)  // 对模板已有参数赋值
            {
                foreach (FastReport.Data.Parameter parm in report.Parameters)
                {
                    // 参数值是从 fastParms.ReportParameters 中传入的
                    foreach (DataColumn dc in e.RecordDR.Table.Columns)
                    {
                        if (parm.Name.Equals(dc.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            parm.Value = e.RecordDR[dc.ColumnName];
                            break;
                        }
                    }
                }
            }
            else    // 添加新的参数至模板
            {
                if (e.RecordDR != null)
                {
                    report.Parameters.Clear();
                    foreach (DataColumn dc in e.RecordDR.Table.Columns)
                    {
                        FastReport.Data.Parameter parm = new FastReport.Data.Parameter(dc.ColumnName);
                        parm.DataType = dc.DataType;
                        parm.Value = e.RecordDR[dc.ColumnName];
                        report.Parameters.Add(parm);
                    }
                }
            }

            if (e.DSDataSource != null)
            {
                for (int i = 0, j = e.DSDataSource.Tables.Count; i < j; i++)
                {
                    DataTable table = e.DSDataSource.Tables[i];
                    string description;
                    string strTableName = table.TableName;
                    report.RegisterData(table, strTableName);
                    //report.RegisterData(e.DSDataSource);
                    //report.ReportInfo.Description = table.TableName;
                    DataSourceBase dataSource = report.GetDataSource(strTableName);
                    dataSource.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 更新本地FastReport报表文件，先判断本地是否最新，如果非最新或者不存在，则更新本地文件
        /// </summary>
        /// <param name="lReportTemplateID"></param>
        public static bool RefleshClientReport(long lReportTemplateID,out string strReportFileName)
        {
            bool bolExists = false;
            strReportFileName = "";
            DataTable dtReportTemp = ExecuteSQL.CallView(105, "", "ReportTemplateID=" + lReportTemplateID, "");
            if (dtReportTemp.Rows.Count > 0)
            {
                DataRow drReportTemp = dtReportTemp.Rows[0];
                string strReportTemplateName = drReportTemp["ReportTemplateName"].ToString().TrimEnd();
                DateTime dTemplateFileTime = Convert.ToDateTime(drReportTemp["TemplateFileTime"]);
                string strReportTemplateNameExt = drReportTemp["ReportTemplateNameExt"].ToString().TrimEnd();
                byte[] bTemplateData = drReportTemp["TemplateData"] as byte[];

                string strReportFullName = Path.Combine(ReportPath, strReportTemplateName + strReportTemplateNameExt);
                if (File.Exists(strReportFullName))
                {
                    FileInfo fiReport = new FileInfo(strReportFullName);
                    if(fiReport.LastWriteTime< dTemplateFileTime)//服务器报表更新时间与本地时间不一样时，先更新本地报表
                    {
                        /*if (LB.WinFunction.LBCommonHelper.ConfirmMessage("本地报表与服务器不一致，是否更新本地报表文件？", "提示", MessageBoxButtons.YesNo)== 
                            DialogResult.Yes)
                        {
                            File.Delete(strReportFullName);
                            ConvertToReportFile(bTemplateData, strReportFullName, dTemplateFileTime);
                        }*/
                        File.Delete(strReportFullName);
                        ConvertToReportFile(bTemplateData, strReportFullName, dTemplateFileTime);
                        bolExists = true;
                    }
                    else
                    {
                        bolExists = true;
                    }
                }
                else
                {
                    ConvertToReportFile(bTemplateData, strReportFullName, dTemplateFileTime);
                    bolExists = true;
                }
                strReportFileName = strReportFullName;
            }
            else
            {
                throw new Exception("该报表不存在！");
            }
            return bolExists;
        }

        #region -- 报表文件与byte[]的相互转换 --
        public static byte[] ConvertToByte(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {

                    //关闭资源  
                    fs.Close();
                }
            }
        }

        public static void ConvertToReportFile(byte[] bTemplateData,string strFileFullName,DateTime dTemplateFileTime)
        {
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(strFileFullName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bTemplateData);
            bw.Close();
            fs.Close();

            FileInfo fi = new FileInfo(strFileFullName);
            fi.LastWriteTime = dTemplateFileTime;
            
        }
        #endregion -- 报表文件与byte[]的相互转换 --

        private static void SetPrintSettings(FastReport.Report report, long lReportTemplateID)
        {
            DataTable dtReportTemplate = ExecuteSQL.CallView(105, "", "ReportTemplateID=" + lReportTemplateID, "");
            if (dtReportTemplate.Rows.Count > 0)
            {
                DataRow drReport = dtReportTemplate.Rows[0];

                string strPrinterName = drReport["PrinterName"].ToString().TrimEnd();
                string strPaperType = drReport["PaperType"].ToString().TrimEnd();
                int PaperSizeHeight =LBConverter.ToInt32( drReport["PaperSizeHeight"]);
                int PaperSizeWidth = LBConverter.ToInt32(drReport["PaperSizeWidth"]);
                bool IsManualPaperType  = LBConverter.ToBoolean( drReport["IsManualPaperType"]);//自动识别纸张类型
                bool IsManualPaperSize = LBConverter.ToBoolean(drReport["IsManualPaperSize"] );//自动识别纸张大小
                bool IsPaperTransverse = LBConverter.ToBoolean(drReport["IsPaperTransverse"]);//是否纵向打印

                if (strPrinterName != "")
                {
                    report.PrintSettings.Printer = strPrinterName;
                    /*if (IsManualPaperType)
                    {
                        System.Drawing.Printing.PrinterSettings mSelectedPrinter = new System.Drawing.Printing.PrinterSettings();
                        int iIndex = 0;
                        foreach (System.Drawing.Printing.PaperSize pageSize in mSelectedPrinter.PaperSizes)
                        {
                            if (pageSize.PaperName.Equals(strPaperType))
                            {
                                //report.PrintSettings.PaperSource = Convert.ToInt32(pageSize.Kind);
                                break;
                            }
                            iIndex++;
                        }

                        report.PrintSettings.PaperSource = iIndex;
                    }

                    if (IsManualPaperSize)
                    {
                        report.PrintSettings.PrintOnSheetHeight = PaperSizeHeight;
                        report.PrintSettings.PrintOnSheetWidth = PaperSizeWidth;
                    }

                    report.PrintSettings.Duplex = IsPaperTransverse ? System.Drawing.Printing.Duplex.Vertical :
                         System.Drawing.Printing.Duplex.Horizontal;
                         */
                }
            }
        }
    }

    //  public class ReportHelper
    //  {
    //      /// <summary>
    ///// 生成模板
    ///// </summary>
    ///// <param name="e">参数</param>
    ///// <param name="strTemplateName">模板文件名</param>
    //internal static void GenReportTemplate(ReportArgs e, string strTemplateName)
    //      {
    //          // 已处理打印预览
    //          if (e.HandledPrint)
    //          {
    //              return;
    //          }

    //          // 新建 FastReport
    //          using (FastReport.Report report = new FastReport.Report())
    //          {
    //              // 根据报表参数的 ReportParms 应用 参数及数据源 至 FastReport 中
    //              ReportHelper.BuildParmsAndData(e, report, ReportHelper.enBuildParmsAndDataActionType.AddNew);

    //              // 保存模板
    //              string strReportPath = Path.Combine(TS.Win.ReportExt.Helper.GetReportDirectory(), strTemplateName);
    //              report.Save(strReportPath);
    //              e.ReportPath = strTemplateName;
    //              // Dispose
    //              report.Dispose();
    //          }

    //          // 打开设计器
    //          Helper.OpenDesigner(strTemplateName, e.BizObjID, e.ReportIsFromEditPage, e.ReportTemplateTypeID);
    //      }

    //      /// <summary>
    ///// 根据报表参数的 ReportParms 应用 参数及数据源 至 FastReport 中
    ///// </summary>
    ///// <param name="e">报表参数</param>
    ///// <param name="report">FastReport</param>
    //public static void BuildParmsAndData(ReportArgs e, FastReport.Report report, enBuildParmsType actionType)
    //      {
    //          // FastReport 参数
    //          ReportFastParms fastParms = (ReportFastParms)e.ReportParms;

    //          // 先将旧的参数及数据源删除，以便刷新数据源
    //          if (actionType == enBuildParmsType.ReSetData)
    //          {
    //              report.Parameters.Clear();
    //              //report.ClearRegisterData();
    //          }

    //          // 参数
    //          if (!fastParms.HasInvokeBuildParametersAuto)
    //          {
    //              HelperFast.BuildParametersByReportArgs(e);
    //          }

    //          if (actionType == enBuildParmsAndDataActionType.SetValue)  // 对模板已有参数赋值
    //          {
    //              foreach (FastReport.Data.Parameter parm in report.Parameters)
    //              {
    //                  // 参数值是从 fastParms.ReportParameters 中传入的
    //                  foreach (FastReport.Data.Parameter parmArgs in fastParms.ReportParameters)
    //                  {
    //                      if (parm.Name.Equals(parmArgs.Name, StringComparison.CurrentCultureIgnoreCase))
    //                      {
    //                          parm.Value = parmArgs.Value;
    //                          break;
    //                      }
    //                  }
    //              }
    //          }
    //          else    // 添加新的参数至模板
    //          {
    //              foreach (FastReport.Data.Parameter parm in fastParms.ReportParameters)
    //              {
    //                  report.Parameters.Add(parm);
    //              }
    //          }

    //          // 数据源
    //          if (fastParms.HasInvokeBuildDataSourcesAuto)
    //          {
    //              Dictionary<string, object>.Enumerator enumerator = fastParms.ReportDataSources.GetEnumerator();
    //              while (enumerator.MoveNext())
    //              {
    //                  string description;
    //                  string strTableName = MakeTableNameStandard(enumerator.Current.Key, e.TableNames, out description);

    //                  DataTable table = null;
    //                  if (enumerator.Current.Value is DataTable)
    //                  {
    //                      table = (DataTable)enumerator.Current.Value;
    //                      report.RegisterData(table, strTableName);
    //                  }
    //                  else
    //                  {
    //                      DataView dvSource = (DataView)enumerator.Current.Value;
    //                      report.RegisterData(dvSource, strTableName);
    //                      if (dvSource != null)
    //                      {
    //                          table = dvSource.Table;
    //                      }
    //                  }
    //                  DataSourceBase dataSource = report.GetDataSource(strTableName);
    //                  dataSource.Enabled = true;
    //                  dataSource.SetDescription(description);
    //                  if (e.TableBizObj.ContainsKey(table))
    //                  {
    //                      dataSource.SetRelateBizObjID(e.TableBizObj[table]);
    //                  }
    //              }
    //          }
    //          else if (e.DataSource != null)
    //          {
    //              for (int i = 0, j = e.DataSource.Tables.Count; i < j; i++)
    //              {
    //                  DataTable table = RemoveDataSumLine(e.DataSource.Tables[i]);
    //                  string description;
    //                  string strTableName = MakeTableNameStandard(table.TableName, e.TableNames, out description);
    //                  report.RegisterData(table, strTableName);
    //                  DataSourceBase dataSource = report.GetDataSource(strTableName);
    //                  dataSource.Enabled = true;
    //                  dataSource.SetDescription(description);
    //                  if (e.TableBizObj.ContainsKey(table))
    //                  {
    //                      dataSource.SetRelateBizObjID(e.TableBizObj[table]);
    //                  }
    //              }
    //          }
    //      }

    //      #region -- 参数添加 --

    //      internal static void BuildParametersByReportArgs(RequestReportArgs e)
    //      {
    //          ReportFastParms fastParms = (ReportFastParms)e.ReportParms;
    //          List<FastReport.Data.Parameter> reportParameters = fastParms.ReportParameters;

    //          if (e.ReportIsFromEditPage)
    //          {
    //              if (e.MTRecordDR != null)
    //              {
    //                  BuildParamters(e.MTRecordDR, e.DTFieldConfig, reportParameters);
    //              }
    //          }
    //          else
    //          {
    //              BuildParamtersByDTCriteria(e.DTCriteria, reportParameters);
    //          }

    //          // 添加全局报表参数
    //          AddGlobalParameter(e, reportParameters);

    //          fastParms.HasInvokeBuildParametersAuto = true;
    //      }

    //      internal static void BuildParamters(DataRow row, DataTable fieldConfig, List<FastReport.Data.Parameter> reportParameters)
    //      {
    //          DataColumnCollection dataColumns = row.Table.Columns;
    //          DataView dvResultConfig = new DataView(fieldConfig);

    //          foreach (DataColumn dataColumn in dataColumns)
    //          {
    //              if (dataColumn.ColumnName.StartsWith("_"))
    //              {
    //                  continue;
    //              }

    //              string strFieldCaption = "";
    //              DataRow[] rows = fieldConfig.Select("FieldName='" + dataColumn.ColumnName + "'");
    //              if (rows.Length > 0)
    //              {
    //                  strFieldCaption = rows[0]["FieldCaption"].ToString().Trim();
    //              }
    //              if (strFieldCaption == "")
    //              {
    //                  strFieldCaption = dataColumn.ColumnName;
    //              }

    //              if (ParametersContainsName(reportParameters, dataColumn.ColumnName))
    //              {
    //                  string strTemp = strFieldCaption + "(" + dataColumn.ColumnName + ")";

    //                  int index = 1;
    //                  while (ParametersContainsName(reportParameters, strTemp))
    //                  {
    //                      strTemp = strFieldCaption + index.ToString() + "(" + dataColumn.ColumnName + ")";
    //                  }

    //                  strFieldCaption = strTemp;
    //              }

    //              FastReport.Data.Parameter parm = new FastReport.Data.Parameter(dataColumn.ColumnName);
    //              parm.DataType = dataColumn.DataType;
    //              parm.Value = row[dataColumn];
    //              parm.Description = strFieldCaption;
    //              reportParameters.Add(parm);
    //          }
    //      }

    //      private static bool ParametersContainsName(List<FastReport.Data.Parameter> reportParameters, string fieldName)
    //      {
    //          foreach (FastReport.Data.Parameter parm in reportParameters)
    //          {
    //              if (parm.Name.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
    //              {
    //                  return true;
    //              }
    //          }
    //          return false;
    //      }

    //      #endregion -- 参数添加 --
    //  }
}
