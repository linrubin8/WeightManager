using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using LB.Page.Helper;
using LB.Common;
using LB.Controls.Report;

namespace LB.MI
{
    public partial class frmAddReportView : LBUIPageBase
    {
        private long mlReportViewID = 0;
        public frmAddReportView(long lReportViewID)
        {
            InitializeComponent();
            mlReportViewID = lReportViewID;
            btnDelete.Visible = lReportViewID > 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReadFieldValue();

            //刷新明细信息
            ReadDetailData(mlReportViewID);

            this.grdMain.LBLoadConst();
        }

        #region -- 刷新明细行信息 --

        private void ReadDetailData(long lReportViewID)
        {
            DataTable dtDetail = ExecuteSQL.CallView(133, "", "ReportViewID=" + lReportViewID, "");
            dtDetail.AcceptChanges();
            this.grdMain.DataSource = dtDetail.DefaultView;
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.VerifyTextBoxIsEmpty();//校验控件值是否为空

                int iSPType = mlReportViewID > 0 ? 14401 : 14400;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, mlReportViewID));
                parmCol.Add(new LBParameter("ReportViewName", enLBDbType.String, this.txtReportViewName.Text));
                parmCol.Add(new LBParameter("ReportDataSource", enLBDbType.String, this.txtReportDataSource.Text));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("ReportViewID"))
                {
                    long.TryParse(dictValue["ReportViewID"].ToString(), out mlReportViewID);
                }
                
                btnDelete.Visible = mlReportViewID > 0;

                if (mlReportViewID > 0)
                {
                    this.grdMain.EndEdit();
                    this.grdMain.CurrentCell = null;
                    DataView dvResult = this.grdMain.DataSource as DataView;
                    //int iRowIndex = 0;
                    StringBuilder strError = new StringBuilder();
                    int iIndex = 0;
                    foreach (DataRow dr in dvResult.Table.Rows)
                    {
                        iIndex++;
                        if (dr.RowState != DataRowState.Added &&
                           dr.RowState != DataRowState.Modified &&
                           dr.RowState != DataRowState.Deleted)
                        {
                            continue;
                        }

                        parmCol = new LBDbParameterCollection();

                        if (dr.RowState == DataRowState.Deleted)
                        {
                            long lModifyBillDetailID = dr["ReportViewFieldID", DataRowVersion.Original] == DBNull.Value ?
                                0 : Convert.ToInt64(dr["ReportViewFieldID", DataRowVersion.Original]);
                            if (lModifyBillDetailID > 0)
                            {
                                parmCol.Add(new LBParameter("ReportViewFieldID", enLBDbType.Int64, lModifyBillDetailID));
                                ExecuteSQL.CallSP(14405, parmCol, out dsReturn, out dictValue);

                            }
                        }
                        else
                        {
                            long lReportViewFieldID = dr["ReportViewFieldID"] == DBNull.Value ?
                                0 : Convert.ToInt64(dr["ReportViewFieldID"]);
                            int FieldType = LBConverter.ToInt32(dr["FieldType"]);
                            string strFieldName = dr["FieldName"].ToString().TrimEnd();
                            string strFieldText = dr["FieldText"].ToString().TrimEnd();
                            string strFieldFormat = dr["FieldFormat"].ToString().TrimEnd();
                            iSPType = 14403;//Insert

                            if (lReportViewFieldID > 0)
                            {
                                parmCol.Add(new LBParameter("ReportViewFieldID", enLBDbType.Int64, lReportViewFieldID));
                                iSPType = 14404;//Update
                            }
                            else
                            {
                                parmCol.Add(new LBParameter("ReportViewFieldID", enLBDbType.Int64, lReportViewFieldID, true));
                            }

                            parmCol.Add(new LBParameter("FieldName", enLBDbType.String, strFieldName));
                            parmCol.Add(new LBParameter("FieldText", enLBDbType.String, strFieldText));
                            parmCol.Add(new LBParameter("FieldType", enLBDbType.Int32, FieldType));
                            parmCol.Add(new LBParameter("FieldFormat", enLBDbType.String, strFieldFormat));
                            parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, mlReportViewID));

                            try
                            {
                                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                                //dgvr.ErrorText = "";
                                if (dictValue.ContainsKey("ReportViewFieldID"))
                                {
                                    dr["ReportViewFieldID"] = dictValue["ReportViewFieldID"];
                                }
                            }
                            catch (Exception ex)
                            {
                                //dgvr.ErrorText = ex.Message;
                                strError.AppendLine("第" + (iIndex + 1).ToString() + "行保存失败：" + ex.Message);
                            }
                        }
                    }

                    dvResult.Table.AcceptChanges();
                    ReadDetailData(mlReportViewID);
                }

               
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该报表？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlReportViewID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, mlReportViewID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14402, parmCol, out dsReturn, out dictValue);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 读取控件参数值
        /// </summary>
        private void ReadFieldValue()
        {
            if (mlReportViewID > 0)
            {
                DataTable dt = ExecuteSQL.CallView(132,"", "ReportViewID="+ mlReportViewID, "");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtReportViewName.Text = LBConverter.ToString(dr["ReportViewName"]);
                    this.txtReportDataSource.Text = LBConverter.ToString(dr["ReportDataSource"]);
                }
            }
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            DataView dvSource = this.grdMain.DataSource as DataView;
            dvSource.Table.Rows.Add(dvSource.Table.NewRow());
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否删除选中明细行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.grdMain.SelectedRows.Count == 0)
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("请选择需要删除的明细行！");
                        return;
                    }
                    DataView dvSource = this.grdMain.DataSource as DataView;
                    List<DataRow> lstDelete = new List<DataRow>();
                    foreach (DataGridViewRow dgvr in this.grdMain.SelectedRows)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        lstDelete.Add(drv.Row);
                    }

                    while (lstDelete.Count > 0)
                    {
                        DataRow dr = lstDelete[0];
                        dr.Delete();
                        lstDelete.Remove(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditReport_Click(object sender, EventArgs e)
        {
            try
            {
                long lReportTemplateID = 0;
                DataTable dtReport = ExecuteSQL.CallView(132, "ReportTemplateID", "ReportViewID=" + mlReportViewID, "");

                if (dtReport.Rows.Count > 0)
                {
                    lReportTemplateID = dtReport.Rows[0]["ReportTemplateID"].ToString() == "" ?
                        0 : Convert.ToInt64(dtReport.Rows[0]["ReportTemplateID"]);
                }

                DataRow drRecord = null;
                DataTable dtRecord = new DataTable("Field");
                DataTable dtDetail = ExecuteSQL.CallView(133, "", "ReportViewID=" + mlReportViewID, "");
                foreach(DataRow drDetail in dtDetail.Rows)
                {
                    if (!dtRecord.Columns.Contains(drDetail["FieldName"].ToString()))
                    {
                        dtRecord.Columns.Add(drDetail["FieldName"].ToString(), typeof(string));
                    }
                }

                if (dtDetail.Rows.Count > 0)
                {
                    DataRow drNew = dtRecord.NewRow();
                    dtRecord.Rows.Add(drNew);

                    drRecord = dtRecord.Rows[0];
                }
                
                ReportRequestArgs args;
                args = new ReportRequestArgs(lReportTemplateID, 8, null, drRecord);

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, mlReportViewID));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14407, parmCol, out dsReturn, out dictValue);

                DataTable dtSource = null;
                if (dsReturn != null && dsReturn.Tables.Count > 0)
                {
                    dtSource = dsReturn.Tables[0].Copy();
                    dtSource.TableName = "T008";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtSource);
                    args.DSDataSource = dsSource;
                    args.ReportTemplateID = lReportTemplateID;
                    frmEditReport frm = new frmEditReport(args);
                    LBShowForm.ShowDialog(frm);

                    if (frm.ReportArgs != null &&
                        frm.ReportArgs.ReportTemplateID>0)
                    {
                        parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, mlReportViewID));
                        parmCol.Add(new LBParameter("ReportTemplateID", enLBDbType.Int64, frm.ReportArgs.ReportTemplateID));
                        ExecuteSQL.CallSP(14406, parmCol, out dsReturn, out dictValue);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
