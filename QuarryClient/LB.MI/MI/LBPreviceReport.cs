using LB.Common;
using LB.Controls.Report;
using LB.Page.Helper;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.MI
{
    public class LBPreviceReport
    {
        private static frmAutoPrint frmPrint;
        public static void PreviceReport(long saleID, enWeightType weightType, enRequestReportActionType requestType)
        {
            ProcessStep.mdtStep = null;
            ReportRequestArgs args;
            if (weightType == enWeightType.WeightIn)
            {
                ProcessStep.AddStep("StartPreviceReport", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                DataTable dtReportTemp = ReportHelper.GetReportTemplateRowByType(6);
                ProcessStep.AddStep("CallView_105", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                if (dtReportTemp.Rows.Count > 0)
                {
                    //打印磅单
                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        frmPrint = new frmAutoPrint();
                    }

                    DataRow drReport = dtReportTemp.Rows[0];
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);

                    args = new ReportRequestArgs(lReportTemplateID, 6, null, null);

                    //long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(128, "", "SaleCarInBillID=" + saleID, "");
                    dtBill.TableName = "T006";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ProcessStep.AddStep("CallView_128", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

                    ReportHelper.OpenReportDialog(requestType, args);

                    ProcessStep.AddStep("OpenReportDialog_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        //记录小票打印次数
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, saleID));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14109, parmCol, out dsReturn, out dictValue);

                        ProcessStep.AddStep("CallSP_14109", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                        DataTable dtt = ProcessStep.mdtStep;

                        LBShowForm.ShowDialog(frmPrint);
                    }
                }
            }
            else if (weightType == enWeightType.WeightOut ||
                    weightType == enWeightType.WeightOnlyOut)
            {
                DataTable dtReportTemp = ReportHelper.GetReportTemplateRowByType(7);
                if (dtReportTemp.Rows.Count > 0)
                {
                    //打印磅单
                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        frmPrint = new frmAutoPrint();
                    }

                    DataRow drReport = dtReportTemp.Rows[0];
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);

                    args = new ReportRequestArgs(lReportTemplateID, 7, null, null);

                    //long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSaleCarOutBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleCarOutBillID=" + saleID, "");
                    dtBill.TableName = "T007";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ReportHelper.OpenReportDialog(requestType, args);

                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        //记录磅单打印次数
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarOutBillID", enLBDbType.Int64, saleID));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14110, parmCol, out dsReturn, out dictValue);

                        LBShowForm.ShowDialog(frmPrint);
                    }
                }
            }
        }

    }
}
