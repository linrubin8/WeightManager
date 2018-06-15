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
using LB.Common;
using LB.Page.Helper;
using LB.Controls.Args;
using LB.Controls.Report;

namespace LB.MI.MI
{
    public partial class frmModifyBillHeaderEdit : LBUIPageBase
    {
        private enBillStatus _BillStatus = enBillStatus.Add;
        private long mModifyBillHeaderID;
        public frmModifyBillHeaderEdit(long lModifyBillHeaderID)
        {
            InitializeComponent();
            mModifyBillHeaderID = lModifyBillHeaderID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ctlFieldHeaderPanel1.AddFiledControl("单据编码:", this.txtModifyBillCode, 1, 1, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("单据日期:", this.txtBillDate, 1, 2, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("生效日期:", this.txtEffectDate, 1, 3, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("客户名称:", this.txtCustomerID, 2, 1, 2);
            this.ctlFieldHeaderPanel1.AddFiledControl("备     注 :", this.txtDescription, 2, 3, 2);
            this.ctlFieldHeaderPanel1.AddFiledControl("制 单 人 :", this.txtCreateBy, 3, 1, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("制单日期:", this.txtCreateTime, 3, 2, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("修 改 人 :", this.txtChangeBy, 3, 3, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("修改日期:", this.txtChangeTime, 3, 4, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("审 核 人 :", this.txtApproveBy, 4, 1, 1);
            this.ctlFieldHeaderPanel1.AddFiledControl("审核日期:", this.txtApproveTime, 4, 2, 1);

            DataTable dtCustom = ExecuteSQL.CallView(110);
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            //刷新单头信息
            ReadFieldValue(mModifyBillHeaderID);

            //刷新明细信息
            ReadDetailData(mModifyBillHeaderID);

            this.grdMain.LBLoadConst();

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
            this.grdMain.CellBeginEdit += GrdMain_CellBeginEdit;
        }

        #region -- grdMain事件 --

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (_BillStatus== enBillStatus.Add|| _BillStatus== enBillStatus.Edit)
                    {
                        string strColumn = this.grdMain.Columns[e.ColumnIndex].DataPropertyName;
                        if (strColumn.Equals("CarNum"))
                        {
                            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                            frmCarQuery frmCar = new frmCarQuery(lCustomerID);
                            LBShowForm.ShowDialog(frmCar);
                            DataRowView drv = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                            if (frmCar.LstReturn.Count > 0)
                            {
                                if (drv.Row.RowState == DataRowState.Unchanged)
                                    drv.Row.SetModified();
                                drv["CarNum"] = frmCar.LstReturn[0]["CarNum"];
                                drv["CarID"] = frmCar.LstReturn[0]["CarID"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdMain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (_BillStatus == enBillStatus.Approve || _BillStatus == enBillStatus.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- grdMain事件 --

        #region -- 按钮事件 --

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemBaseManager frmItem = new frmItemBaseManager();
                frmItem.NeedReturn = true;
                LBShowForm.ShowDialog(frmItem);

                if (frmItem.LstReturn.Count > 0)
                {
                    foreach (DataRow dr in frmItem.LstReturn)
                    {
                        dr["ChangeBy"] = "";
                        dr["ChangeTime"] = DBNull.Value;
                        DataView dvSource = this.grdMain.DataSource as DataView;
                        dvSource.Table.ImportRow(dr);
                        dvSource.Table.Rows[dvSource.Table.Rows.Count-1]["CalculateType"] = 0;
                        /*int iRowIndex = this.grdMain.Rows.Add();
                        this.grdMain.Rows[iRowIndex].Cells["ItemID"].Value = dr["ItemID"];
                        this.grdMain.Rows[iRowIndex].Cells["ItemCode"].Value = dr["ItemCode"];
                        this.grdMain.Rows[iRowIndex].Cells["ItemName"].Value = dr["ItemName"];
                        this.grdMain.Rows[iRowIndex].Cells["ItemMode"].Value = dr["ItemMode"];
                        this.grdMain.Rows[iRowIndex].Cells["ItemRate"].Value = dr["ItemRate"];
                        this.grdMain.Rows[iRowIndex].Cells["UOMName"].Value = dr["UOMName"];*/
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveHeader();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除调价单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mModifyBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13602, parmCol, out dsReturn, out dictValue);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认审核调价单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mModifyBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13603, parmCol, out dsReturn, out dictValue);

                        //刷新单头信息
                        ReadFieldValue(mModifyBillHeaderID);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnUnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认取消审核调价单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mModifyBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13604, parmCol, out dsReturn, out dictValue);

                        //刷新单头信息
                        ReadFieldValue(mModifyBillHeaderID);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认作废调价单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mModifyBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13605, parmCol, out dsReturn, out dictValue);

                        //刷新单头信息
                        ReadFieldValue(mModifyBillHeaderID);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnUnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认取消作废调价单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mModifyBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13606, parmCol, out dsReturn, out dictValue);

                        //刷新单头信息
                        ReadFieldValue(mModifyBillHeaderID);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                mModifyBillHeaderID = 0;
                ReadFieldValue(mModifyBillHeaderID);
                ReadDetailData(mModifyBillHeaderID);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                //刷新单头信息
                ReadFieldValue(mModifyBillHeaderID);

                //刷新明细信息
                ReadDetailData(mModifyBillHeaderID);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                this.mModifyBillHeaderID = 0;
                this.txtApproveBy.Text = "";
                this.txtApproveTime.Text = "";
                this.txtChangeBy.Text = "";
                this.txtChangeTime.Text = "";
                this.txtCreateBy.Text = "";
                this.txtCreateTime.Text = "";
                this.txtModifyBillCode.Text = "";

                ((DataView)this.grdMain.DataSource).Table.AcceptChanges();
                foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    ((DataRowView)dgvr.DataBoundItem).Row.SetAdded();
                    ((DataRowView)dgvr.DataBoundItem)["ModifyBillDetailID"] = DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 按钮事件 --

        #region -- 保存单头信息 --

        private void SaveHeader()
        {
            this.VerifyTextBoxIsEmpty();
            int iSPType = 13600;
            if (mModifyBillHeaderID > 0)
            {
                iSPType = 13601;
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, this.txtCustomerID.TextBox.SelectedItemID));
            parmCol.Add(new LBParameter("ModifyBillCode", enLBDbType.String, this.txtModifyBillCode.Text));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, this.txtBillDate.Text));
            parmCol.Add(new LBParameter("EffectDate", enLBDbType.DateTime, this.txtEffectDate.Text));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("ModifyBillHeaderID"))
            {
                mModifyBillHeaderID = LBConverter.ToInt64(dictValue["ModifyBillHeaderID"]);
            }
            if (dictValue.ContainsKey("ModifyBillCode"))
            {
                this.txtModifyBillCode.Text = dictValue["ModifyBillCode"].ToString();
            }

            #region -- 保存明细 --

            string strMsg;
            bool bolExistsSave = ExistsSameDetail(out strMsg);
            if (bolExistsSave)
            {
                throw new Exception(strMsg);
            }

            this.grdMain.EndEdit();
            this.grdMain.CurrentCell = null;
            DataView dvResult = this.grdMain.DataSource as DataView;
            //int iRowIndex = 0;
            StringBuilder strError = new StringBuilder();
            int iIndex =0;
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
                    long lModifyBillDetailID = dr["ModifyBillDetailID", DataRowVersion.Original] == DBNull.Value ?
                        0 : Convert.ToInt64(dr["ModifyBillDetailID", DataRowVersion.Original]);
                    if (lModifyBillDetailID > 0)
                    {
                        parmCol.Add(new LBParameter("ModifyBillDetailID", enLBDbType.Int64, lModifyBillDetailID));
                        ExecuteSQL.CallSP(13702, parmCol, out dsReturn, out dictValue);

                    }
                }
                else
                {
                    long lModifyBillDetailID = dr["ModifyBillDetailID"] == DBNull.Value ?
                        0 : Convert.ToInt64(dr["ModifyBillDetailID"]);
                    long ItemID = LBConverter.ToInt64(dr["ItemID"]);
                    long CarID = LBConverter.ToInt64(dr["CarID"]);
                    int CalculateType = LBConverter.ToInt32(dr["CalculateType"]);
                    decimal Price = LBConverter.ToDecimal(dr["Price"]);
                    long UOMID = LBConverter.ToInt64(dr["UOMID"]);
                    string Description = dr["Description"].ToString().TrimEnd();
                    decimal MaterialPrice = LBConverter.ToDecimal(dr["MaterialPrice"]);
                    decimal FarePrice = LBConverter.ToDecimal(dr["FarePrice"]);
                    decimal TaxPrice = LBConverter.ToDecimal(dr["TaxPrice"]);
                    decimal BrokerPrice = LBConverter.ToDecimal(dr["BrokerPrice"]);

                    iSPType = 13700;//Insert

                    if (lModifyBillDetailID > 0)
                    {
                        parmCol.Add(new LBParameter("ModifyBillDetailID", enLBDbType.Int64, lModifyBillDetailID));
                        iSPType = 13701;//Update
                    }
                    else
                    {
                        parmCol.Add(new LBParameter("ModifyBillDetailID", enLBDbType.Int64, lModifyBillDetailID, true));
                    }

                    parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, mModifyBillHeaderID));
                    parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, ItemID));
                    if (CarID > 0)
                        parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, CarID));
                    parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int16, CalculateType));
                    parmCol.Add(new LBParameter("Price", enLBDbType.Decimal, Price));
                    parmCol.Add(new LBParameter("MaterialPrice", enLBDbType.Decimal, MaterialPrice));
                    parmCol.Add(new LBParameter("FarePrice", enLBDbType.Decimal, FarePrice));
                    parmCol.Add(new LBParameter("TaxPrice", enLBDbType.Decimal, TaxPrice));
                    parmCol.Add(new LBParameter("BrokerPrice", enLBDbType.Decimal, BrokerPrice));
                    parmCol.Add(new LBParameter("UOMID", enLBDbType.Int64, UOMID));
                    parmCol.Add(new LBParameter("Description", enLBDbType.String, Description));

                    try
                    {
                        ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                        //dgvr.ErrorText = "";
                        if (dictValue.ContainsKey("ModifyBillDetailID"))
                        {
                            dr["ModifyBillDetailID"] = dictValue["ModifyBillDetailID"];
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
            if (strError.ToString() != "")
                throw new Exception(strError.ToString());
            LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");

            #endregion -- 保存明细 --

            //刷新单头信息
            ReadFieldValue(mModifyBillHeaderID);

            ReadDetailData(mModifyBillHeaderID);
        }

        private bool ExistsSameDetail(out string strMsg)
        {
            bool bolExists = false;
            strMsg = "";
            List<string> lstVerify = new List<string>();
            foreach(DataGridViewRow dgvr in this.grdMain.Rows)
            {
                long lItemID = LBConverter.ToInt64(dgvr.Cells["ItemID"].Value);
                long lCarID = LBConverter.ToInt64(dgvr.Cells["CarID"].Value);
                int iCalculateType = LBConverter.ToInt32(dgvr.Cells["CalculateType"].Value);
                string strKey = lItemID.ToString() + "-" + lCarID.ToString() + "-" + iCalculateType.ToString();
                if (!lstVerify.Contains(strKey))
                {
                    lstVerify.Add(strKey);
                }
                else
                {
                    if (strMsg != "")
                        strMsg += "\n";
                    strMsg += "第"+ (dgvr.Index+1)+"行，存在重复的数据行！";
                    bolExists = true;
                }
            }
            return bolExists;
        }

        #endregion

        #region -- 刷新单头数据 --

        private void ReadFieldValue(long lModifyBillHeaderID)
        {
            if (lModifyBillHeaderID > 0)
            {
                DataTable dtHeader = ExecuteSQL.CallView(115, "", "ModifyBillHeaderID=" + lModifyBillHeaderID, "");
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    this.txtApproveBy.Text = drHeader["ApproveBy"].ToString();
                    this.txtApproveTime.Text = drHeader["ApproveTime"].ToString();
                    this.txtBillDate.Text = drHeader["BillDate"].ToString();
                    this.txtChangeBy.Text = drHeader["ChangeBy"].ToString();
                    this.txtChangeTime.Text = drHeader["ChangeTime"].ToString();
                    this.txtCreateBy.Text = drHeader["CreateBy"].ToString();
                    this.txtCreateTime.Text = drHeader["CreateTime"].ToString();
                    this.txtCustomerID.TextBox.SelectedItemID =LBConverter.ToInt64( drHeader["CustomerID"]);
                    this.txtDescription.Text = drHeader["Description"].ToString();
                    this.txtEffectDate.Text = drHeader["EffectDate"].ToString();
                    this.txtModifyBillCode.Text = drHeader["ModifyBillCode"].ToString();

                    ResetFieldControlStatus(drHeader);
                }
            }
            else
            {
                this.txtApproveBy.Text = "";
                this.txtApproveTime.Text = "";
                this.txtChangeBy.Text = "";
                this.txtChangeTime.Text = "";
                this.txtCreateBy.Text = "";
                this.txtCreateTime.Text = "";
                this.txtDescription.Text = "";
                this.txtModifyBillCode.Text = "";
                this.grdMain.DataSource = null;

                ResetFieldControlStatus(null);
            }
        }

        #endregion -- 刷新单头数据 --

        #region -- 刷新明细行信息 --

        private void ReadDetailData(long lModifyBillHeaderID)
        {
            DataTable dtDetail = ExecuteSQL.CallView(116, "", "ModifyBillHeaderID=" + lModifyBillHeaderID, "");
            this.grdMain.DataSource = dtDetail.DefaultView;
        }

        #endregion

        #region -- 刷新按钮以及控件状态 --

        private void ResetFieldControlStatus(DataRow drHeader)
        {
            this.btnAdd.Visible = true;
            this.btnAddDetail.Visible = true;
            this.btnApprove.Visible = true;
            this.btnUnApprove.Visible = true;
            this.btnCancel.Visible = true;
            this.btnUnCancel.Visible = true;
            this.btnSave.Visible = true;
            this.btnDeleteDetail.Visible = true;
            this.btnDelete.Visible = true;
            this.btnReflesh.Visible = true;
            this.btnCopy.Visible = true;

            this.txtBillDate.Enabled = true;
            this.txtCustomerID.Enabled = true;
            this.txtDescription.Enabled = true;
            this.txtEffectDate.Enabled = true;

            if (mModifyBillHeaderID == 0 || drHeader==null)
            {
                //添加状态
                this.btnApprove.Visible = false;
                this.btnUnApprove.Visible = false;
                this.btnCancel.Visible = false;
                this.btnUnCancel.Visible = false;
                this.btnDelete.Visible = false;
                this.btnCopy.Visible = false;
                this.btnReflesh.Visible = false;

                _BillStatus = enBillStatus.Add;
            }
            else
            {
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);

                if (bolIsCancel)//已作废
                {
                    this.btnAddDetail.Visible = false;
                    this.btnApprove.Visible = false;
                    this.btnUnApprove.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnSave.Visible = false;
                    this.btnDeleteDetail.Visible = false;
                    this.btnDelete.Visible = false;

                    this.txtBillDate.Enabled = false;
                    this.txtCustomerID.Enabled = false;
                    this.txtDescription.Enabled = false;
                    this.txtEffectDate.Enabled = false;
                    _BillStatus = enBillStatus.Cancel;
                }
                else if (bolIsApprove)//已审核
                {
                    this.btnAddDetail.Visible = false;
                    this.btnApprove.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnUnCancel.Visible = false;
                    this.btnSave.Visible = false;
                    this.btnDeleteDetail.Visible = false;
                    this.btnDelete.Visible = false;

                    this.txtBillDate.Enabled = false;
                    this.txtCustomerID.Enabled = false;
                    this.txtDescription.Enabled = false;
                    this.txtEffectDate.Enabled = false;
                    
                    _BillStatus = enBillStatus.Approve;
                }
                else if (!bolIsApprove)//未审核
                {
                    this.btnUnApprove.Visible = false;
                    this.btnUnCancel.Visible = false;
                    _BillStatus = enBillStatus.Edit;
                }

            }
        }

        #endregion

        #region -- 明细行车辆下拉数据源 --



        #endregion -- 明细行车辆下拉数据源 --

        #region -- 报表 --

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 5;//调价单序时簿
            base.OnInitToolStripControl(args);

        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtHeader = ExecuteSQL.CallView(115, "", "ModifyBillHeaderID=" + mModifyBillHeaderID, "");
            if (dtHeader.Rows.Count > 0)
            {
                DataRow drHeader = dtHeader.Rows[0];
                args.RecordDR = drHeader;
            }
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T005";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        #endregion

    }
}
