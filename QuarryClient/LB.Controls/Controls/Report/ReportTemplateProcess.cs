using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LB.Controls.Report
{
    public class ReportTemplateProcess
    {
        public static void CustomReportUpload(object sender, FastReport.Design.ReportUploadEventArgs e)
        {
            try
            {
                FastReport.Report report = e.Report;
                string strTemplateFileName = report.FileName;
                string strExt = Path.GetExtension(strTemplateFileName);
                string strFileName = Path.GetFileName(strTemplateFileName);
                string strReportTemplateName = strFileName.Substring(0, strFileName.Length - strExt.Length);
                int iReportTemplateID = GetReportTemplateID(strReportTemplateName);
                if (iReportTemplateID > 0)
                {
                    byte[] bReport = ReportHelper.ConvertToByte(strTemplateFileName);
                    LBDbParameterCollection parms = new LBDbParameterCollection();
                    parms.Add(new LBParameter("ReportTemplateID", enLBDbType.Int64, iReportTemplateID));
                    parms.Add(new LBParameter("TemplateFileTime", enLBDbType.DateTime, DateTime.Now));
                    parms.Add(new LBParameter("TemplateData", enLBDbType.Object, bReport));
                    DataSet dsReturn;
                    Dictionary<string, object> dictResult;
                    ExecuteSQL.CallSP(12003, parms, out dsReturn, out dictResult);
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("上传成功！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private static int GetReportTemplateID(string strTemplateFileName)
        {
            int iReportTemplateID = 0;
            DataTable dtReportTemplate = ExecuteSQL.CallView(105, "", "ReportTemplateName='" + strTemplateFileName + "'", "");
            if (dtReportTemplate.Rows.Count > 0)
            {
                DataRow drReport = dtReportTemplate.Rows[0];
                iReportTemplateID = Convert.ToInt32(drReport["ReportTemplateID"]);
            }
            return iReportTemplateID;
        }
    }
}
