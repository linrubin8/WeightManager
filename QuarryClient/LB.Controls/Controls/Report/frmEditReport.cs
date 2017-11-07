using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using System.IO;
using LB.WinFunction;
using LB.Common;

namespace LB.Controls.Report
{
    public partial class frmEditReport : LBUIPageBase
    {
        private ReportRequestArgs mReportArgs;
        private System.Drawing.Printing.PrinterSettings mSelectedPrinter = null;
        private DataTable dtPaperType=null;
        private DataTable dtPaperSources;				//纸张来源

        public ReportRequestArgs ReportArgs
        {
            get
            {
                return mReportArgs;
            }
        }

        public frmEditReport( ReportRequestArgs reportArgs)
        {
            InitializeComponent();
            mReportArgs = reportArgs;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtPrinterName.SelectedValueChanged += TxtPrinterName_SelectedValueChanged;
            this.rbAutoPaperType.CheckedChanged += RbAutoPaperType_CheckedChanged;
            this.rbAutoPaperSize.CheckedChanged += RbAutoPaperSize_CheckedChanged;

            InitPrinter();//加载打印机清单

            if (mReportArgs.ReportTemplateID > 0)
            {
                ReadFieldValue();
            }

            SetPrintControlEnable();
        }

        private void RbAutoPaperSize_CheckedChanged(object sender, EventArgs e)
        {
            SetPrintControlEnable();
        }

        private void RbAutoPaperType_CheckedChanged(object sender, EventArgs e)
        {
            SetPrintControlEnable();
        }

        #region -- 事件 -- 

        private void TxtPrinterName_SelectedValueChanged(object sender, EventArgs e)
        {
            SetPrintControlEnable();

            try
            {
                string strPrinterName = txtPrinterName.Text;

                InitPaperTypeSource(strPrinterName);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                #region -- 保存报表 -- 
                if (mReportArgs.ReportTemplateID == 0)
                {
                    ReportRequestArgs args = new ReportRequestArgs(0, mReportArgs.ReportTypeID, mReportArgs.DSDataSource, mReportArgs.RecordDR);

                    this.txtReportPath.Text = ReportHelper.AddNewReport(args, this.txtReportTemplateName.Text);

                    if (!File.Exists(this.txtReportPath.Text))
                    {
                        throw new Exception("报表文件不存在，无法保存！");
                    }
                    byte[] bReport = ReportHelper.ConvertToByte(this.txtReportPath.Text);
                    LBDbParameterCollection parms = new LBDbParameterCollection();
                    parms.Add(new LBParameter("ReportTemplateID", enLBDbType.Int64, 0));
                    parms.Add(new LBParameter("ReportTemplateName", enLBDbType.String, this.txtReportTemplateName.Text));
                    parms.Add(new LBParameter("TemplateFileTime", enLBDbType.DateTime, DateTime.Now));
                    parms.Add(new LBParameter("TemplateSeq", enLBDbType.Int32, 0));
                    parms.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
                    parms.Add(new LBParameter("TemplateData", enLBDbType.Object, bReport));
                    parms.Add(new LBParameter("ReportTypeID", enLBDbType.Int64, mReportArgs.ReportTypeID));
                    parms.Add(new LBParameter("PrinterName", enLBDbType.String, this.txtPrinterName.Text));
                    parms.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                    parms.Add(new LBParameter("IsManualPaperType", enLBDbType.Boolean, rbManualPaperType.Checked));
                    parms.Add(new LBParameter("PaperType", enLBDbType.String, this.txtPaperType.Text));
                    parms.Add(new LBParameter("IsManualPaperSize", enLBDbType.Boolean, rbManualPaperSize.Checked));
                    if(this.txtPaperSizeHeight.Text!="")
                        parms.Add(new LBParameter("PaperSizeHeight", enLBDbType.Int32, this.txtPaperSizeHeight.Text));
                    if (this.txtPaperSizeWidth.Text != "")
                        parms.Add(new LBParameter("PaperSizeWidth", enLBDbType.Int32, this.txtPaperSizeWidth.Text));
                    parms.Add(new LBParameter("IsPaperTransverse", enLBDbType.Boolean, rbPaperTransverse.Checked));
                    parms.Add(new LBParameter("PrintCount", enLBDbType.Int32,LBConverter.ToInt32( this.txtPrintCount.Text)));
                    DataSet dsReturn;
                    Dictionary<string, object> dictResult;
                    ExecuteSQL.CallSP(12000, parms, out dsReturn, out dictResult);

                    if (dictResult.ContainsKey("ReportTemplateID"))
                    {
                        if (dictResult["ReportTemplateID"] != null)
                        {
                            mReportArgs.ReportTemplateID = Convert.ToInt64(dictResult["ReportTemplateID"]);
                        }
                    }
                }
                else
                {
                    string strReportFile;
                    bool bolExists = ReportHelper.RefleshClientReport(mReportArgs.ReportTemplateID, out strReportFile);

                    byte[] bReport = null;
                    DateTime dtTemplateFileTime = DateTime.Now;
                    if (bolExists)
                    {
                        bReport = ReportHelper.ConvertToByte(strReportFile);
                        dtTemplateFileTime = File.GetLastWriteTime(strReportFile);
                    }
                    else
                    {
                        throw new Exception("报表文件不存在，无法保存！");
                    }

                    LBDbParameterCollection parms = new LBDbParameterCollection();
                    parms.Add(new LBParameter("ReportTemplateID", enLBDbType.Int64, mReportArgs.ReportTemplateID));
                    parms.Add(new LBParameter("ReportTemplateName", enLBDbType.String, this.txtReportTemplateName.Text));
                    parms.Add(new LBParameter("TemplateFileTime", enLBDbType.DateTime, dtTemplateFileTime));
                    parms.Add(new LBParameter("TemplateSeq", enLBDbType.Int32, 0));
                    parms.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
                    parms.Add(new LBParameter("TemplateData", enLBDbType.Object, bReport));
                    parms.Add(new LBParameter("ReportTypeID", enLBDbType.Int64, mReportArgs.ReportTypeID));
                    parms.Add(new LBParameter("PrinterName", enLBDbType.String, this.txtPrinterName.Text));
                    parms.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                    parms.Add(new LBParameter("IsManualPaperType", enLBDbType.Boolean, rbManualPaperType.Checked));
                    parms.Add(new LBParameter("PaperType", enLBDbType.String, this.txtPaperType.Text));
                    parms.Add(new LBParameter("IsManualPaperSize", enLBDbType.Boolean, rbManualPaperSize.Checked));
                    if (this.txtPaperSizeHeight.Text != "")
                        parms.Add(new LBParameter("PaperSizeHeight", enLBDbType.Int32, this.txtPaperSizeHeight.Text));
                    if (this.txtPaperSizeWidth.Text != "")
                        parms.Add(new LBParameter("PaperSizeWidth", enLBDbType.Int32, this.txtPaperSizeWidth.Text));
                    parms.Add(new LBParameter("IsPaperTransverse", enLBDbType.Boolean, rbPaperTransverse.Checked));
                    parms.Add(new LBParameter("PrintCount", enLBDbType.Int32, LBConverter.ToInt32(this.txtPrintCount.Text)));
                    DataSet dsReturn;
                    Dictionary<string, object> dictResult;
                    ExecuteSQL.CallSP(12001, parms, out dsReturn, out dictResult);
                }
                #endregion -- 保存报表 --

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该报表？", "提示", MessageBoxButtons.YesNo) ==
                     DialogResult.Yes)
                {
                    LBDbParameterCollection parms = new LBDbParameterCollection();
                    parms.Add(new LBParameter("ReportTemplateID", enLBDbType.Int64, mReportArgs.ReportTemplateID));
                    DataSet dsReturn;
                    Dictionary<string, object> dictResult;
                    ExecuteSQL.CallSP(12002, parms, out dsReturn, out dictResult);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnUpLoadReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (mReportArgs.ReportTemplateID > 0)
                {
                    ReportHelper.OpenReportDesign(mReportArgs);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSelectReport_Click(object sender, EventArgs e)
        {
            try
            {
                openFile.InitialDirectory = ReportHelper.ReportPath;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string strFile = openFile.FileName;
                    this.txtReportPath.Text = strFile;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 事件 -- 

        #region -- 读取并加载报表模板数据 --

        private void ReadFieldValue()
        {
            DataTable dtReportTemplate = ExecuteSQL.CallView(105, "", "ReportTemplateID=" + mReportArgs.ReportTemplateID, "");
            if (dtReportTemplate.Rows.Count > 0)
            {
                DataRow drReport = dtReportTemplate.Rows[0];
                this.txtReportTemplateName.Text = drReport["ReportTemplateName"].ToString();
                this.txtDescription.Text = drReport["Description"].ToString();
                this.txtPrinterName.SelectedValue = drReport["PrinterName"].ToString().TrimEnd();
                this.txtPaperType.SelectedValue= drReport["PaperType"].ToString().TrimEnd();
                this.txtPaperSizeHeight.Text = drReport["PaperSizeHeight"].ToString().TrimEnd();
                this.txtPaperSizeWidth.Text= drReport["PaperSizeWidth"].ToString().TrimEnd();
                this.rbManualPaperType.Checked = drReport["IsManualPaperType"] == DBNull.Value ? 
                    false : Convert.ToBoolean(drReport["IsManualPaperType"]);
                this.rbManualPaperSize.Checked = drReport["IsManualPaperSize"] == DBNull.Value ?
                    false : Convert.ToBoolean(drReport["IsManualPaperSize"]);
                this.rbPaperTransverse.Checked= drReport["IsPaperTransverse"] == DBNull.Value ?
                    false : Convert.ToBoolean(drReport["IsPaperTransverse"]);
                this.txtPrintCount.Text= drReport["PrintCount"].ToString().TrimEnd();
                //this.txtReportPath.Text = Path.Combine(ReportHelper.ReportPath, drReport["ReportTemplateName"].ToString() + ".frx");
            }
        }

        #endregion

        #region -- InitPrinter --

        private void InitPrinter()
        {
            DataTable dtPrinter = new DataTable();
            dtPrinter.Columns.Add("PrinterName", typeof(string));

            System.Drawing.Printing.PrinterSettings.StringCollection printers = null;
            try
            {
                printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            }
            catch
            {
                // 有时访问 System.Drawing.Printing.PrinterSettings.InstalledPrinters，会报出“RPC服务器不可用”的错误，忽略此错
            }

            dtPrinter.Rows.Add(dtPrinter.NewRow());
            if (printers != null)
            {
                foreach (string strPrinterName in printers)
                {
                    DataRow drRow = dtPrinter.NewRow();
                    drRow["PrinterName"] = strPrinterName;
                    dtPrinter.Rows.Add(drRow);
                }
            }

            this.txtPrinterName.DataSource = dtPrinter.DefaultView;
            this.txtPrinterName.DisplayMember = "PrinterName";
            this.txtPrinterName.ValueMember= "PrinterName";
        }
        /// <summary>
        /// 纸张格式
        /// </summary>
        /// <param name="strPrinterName"></param>
        private void InitPaperTypeSource(string strPrinterName)
        {
            if (strPrinterName != "")
            {
                if (mSelectedPrinter == null)
                {
                    mSelectedPrinter = new System.Drawing.Printing.PrinterSettings();
                }
                mSelectedPrinter.PrinterName = strPrinterName;
                if (mSelectedPrinter.IsValid)
                {
                    if (dtPaperType == null)
                    {
                        dtPaperType = new DataTable();
                        dtPaperType.Columns.Add("PaperType", typeof(string));
                        dtPaperType.Columns.Add("PaperKind", typeof(int));
                        dtPaperType.Columns.Add("PaperSizeHeight", typeof(int));
                        dtPaperType.Columns.Add("PaperSizeWidth", typeof(int));
                    }
                    dtPaperType.Rows.Clear();
                    foreach (System.Drawing.Printing.PaperSize pageSize in mSelectedPrinter.PaperSizes)
                    {
                        DataRow drPaperType = dtPaperType.NewRow();
                        drPaperType["PaperType"] = pageSize.PaperName;
                        drPaperType["PaperKind"] = Convert.ToInt32(pageSize.Kind);
                        drPaperType["PaperSizeHeight"] = InchToMM(pageSize.Height);
                        drPaperType["PaperSizeWidth"] = InchToMM(pageSize.Width);
                        dtPaperType.Rows.Add(drPaperType);
                    }

                    if (dtPaperSources == null)
                    {
                        dtPaperSources = new DataTable();
                        dtPaperSources.Columns.Add("PaperSourceName", typeof(string));
                        dtPaperSources.Columns.Add("PaperSourceKind", typeof(string));
                        dtPaperSources.Columns.Add("PaperSourceRawKind", typeof(int));
                    }
                    dtPaperSources.Rows.Clear();
                    DataRow drPaperSourcesNew = dtPaperSources.NewRow();
                    drPaperSourcesNew["PaperSourceName"] = "自动选择";
                    drPaperSourcesNew["PaperSourceKind"] = "FormSource";
                    drPaperSourcesNew["PaperSourceRawKind"] = 0;
                    dtPaperSources.Rows.Add(drPaperSourcesNew);
                    foreach (System.Drawing.Printing.PaperSource source in mSelectedPrinter.PaperSources)
                    {
                        if (source.Kind.ToString() == "FormSource")
                        {
                            continue;
                        }
                        DataRow drPaperSources = dtPaperSources.NewRow();
                        drPaperSources["PaperSourceName"] = source.SourceName.ToString();
                        drPaperSources["PaperSourceKind"] = source.Kind.ToString();
                        drPaperSources["PaperSourceRawKind"] = source.RawKind;
                        dtPaperSources.Rows.Add(drPaperSources);
                    }

                    txtPaperType.DisplayMember = txtPaperType.ValueMember = "PaperType";
                    txtPaperType.DataSource = dtPaperType.DefaultView;

                }
                else
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("所选打印机正处于脱机状态中");
                    return;
                }
            }
        }

        #endregion

        #region -- 打印机设置控件只读性控制 --

        private void SetPrintControlEnable()
        {
            if(this.txtPrinterName.SelectedValue!=null&& this.txtPrinterName.SelectedValue.ToString() != "")
            {
                this.rbAutoPaperType.Enabled = true;
                this.rbManualPaperType.Enabled = true;

                this.txtPaperType.Enabled = !rbAutoPaperType.Checked;
                this.rbAutoPaperSize.Enabled = !rbAutoPaperType.Checked;
                this.rbManualPaperSize.Enabled = !rbAutoPaperType.Checked;
                this.rbPaperLengthways.Enabled = !rbAutoPaperType.Checked;
                this.rbPaperTransverse.Enabled = !rbAutoPaperType.Checked;

                this.txtPaperSizeHeight.Enabled = !rbAutoPaperSize.Checked;
                this.txtPaperSizeWidth.Enabled = !rbAutoPaperSize.Checked;
            }
            else
            {
                this.rbAutoPaperSize.Enabled = false;
                this.rbAutoPaperType.Enabled = false;
                this.rbManualPaperSize.Enabled = false;
                this.rbManualPaperType.Enabled = false;
                this.rbPaperLengthways.Enabled = false;
                this.rbPaperTransverse.Enabled = false;
                this.txtPaperType.Enabled = false;
                this.txtPaperSizeHeight.Enabled = false;
                this.txtPaperSizeWidth.Enabled = false;
            }
        }

        #endregion

        // 1英寸 = 2.54cm
        private int InchToMM(int inch)
        {
            return (int)Math.Round(((decimal)inch * (decimal)254 / (decimal)1000), 0, MidpointRounding.AwayFromZero);
        }
    }
}
