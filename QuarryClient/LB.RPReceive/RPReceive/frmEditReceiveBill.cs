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
using LB.Controls.LBTextBox;

namespace LB.RPReceive
{
    public partial class frmEditReceiveBill : LBUIPageBase
    {
        private long mlReceiveBillHeaderID;
        public frmEditReceiveBill(long lReceiveBillHeaderID)
        {
            InitializeComponent();
            mlReceiveBillHeaderID = lReceiveBillHeaderID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetButtonStatus();

            InitCustomerList();

            ReadFieldValue();
        }

        private void InitCustomerList()
        {
            DataTable dtCustom = ExecuteSQL.CallView(110);
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            //this.txtRPReceiveType.DataSource = LB.Common.LBConst.GetConstData("RPReceiveType");//收款方式
            //this.txtRPReceiveType.DisplayMember = "ConstText";
            //this.txtRPReceiveType.ValueMember = "ConstValue";

            DataTable dtBank = ExecuteSQL.CallView(136, "", "", "BankName asc");
            this.txtReceiveBankID.TextBox.LBViewType = 136;
            this.txtReceiveBankID.TextBox.LBSort = "BankName asc";
            this.txtReceiveBankID.TextBox.IDColumnName = "ReceiveBankID";
            this.txtReceiveBankID.TextBox.TextColumnName = "BankName";
            this.txtReceiveBankID.TextBox.PopDataSource = dtBank.DefaultView;

            DataTable dtCharge = ExecuteSQL.CallView(138, "", "", "ChargeTypeID asc");
            this.txtRPReceiveType.TextBox.LBViewType = 138;
            this.txtRPReceiveType.TextBox.LBSort = "ChargeTypeName asc";
            this.txtRPReceiveType.TextBox.IDColumnName = "ChargeTypeID";
            this.txtRPReceiveType.TextBox.TextColumnName = "ChargeTypeName";
            this.txtRPReceiveType.TextBox.PopDataSource = dtCharge.DefaultView;

            this.txtReceiveAmount.Leave += TxtReceiveAmount_Leave;

            //this.txtRPReceiveType.TextBox.SelectedValue = 0;
            //lblBank.Visible = false;
            //this.txtReceiveBankID.Visible = false;

            //if(LoginInfo.UserType== 0)//地磅文员不能编辑应收调整金额
            //{
            //    this.txtSalesReceiveAmountAdd.ReadOnly = true;
            //    this.txtSalesReceiveAmountReduce.ReadOnly = true;
            //    this.txtOriginalAmount.ReadOnly = true;
            //}
        }
        
        private void ReadFieldValue()
        {
            if (mlReceiveBillHeaderID > 0)
            {
                DataTable dtHeader = ExecuteSQL.CallView(111,"", "ReceiveBillHeaderID="+ mlReceiveBillHeaderID,"");
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    decimal decReceiveAmount = LBConverter.ToDecimal(drHeader["ReceiveAmount"]);
                    decimal decSalesReceiveAmountAdd = LBConverter.ToDecimal(drHeader["SalesReceiveAmountAdd"]);
                    decimal decSalesReceiveAmountReduce = LBConverter.ToDecimal(drHeader["SalesReceiveAmountReduce"]);
                    decimal decOriginalAmount = LBConverter.ToDecimal(drHeader["OriginalAmount"]);
                    this.txtBillDate.Text = LBConverter.ToString(drHeader["BillDate"]);
                    this.txtBillStatus.Text= bolIsApprove?"已审核":
                    (bolIsCancel?"已作废":("未审核"));
                    this.txtCustomerID.TextBox.SelectedItemID = drHeader["CustomerID"].ToString();
                    this.txtDescription.Text = drHeader["Description"].ToString();
                    this.txtReceiveAmount.Text = decReceiveAmount.ToString("N0") ;
                    this.txtReceiveBillCode.Text = drHeader["ReceiveBillCode"].ToString();

                    this.txtChangedBy.Text = drHeader["ChangedBy"].ToString();
                    this.txtChangeTime.Text = drHeader["ChangeTime"].ToString();
                    this.txtApproveBy.Text = drHeader["ApproveBy"].ToString();
                    this.txtApproveTime.Text = drHeader["ApproveTime"].ToString();
                    this.txtCancelBy.Text = drHeader["CancelBy"].ToString();
                    this.txtCancelTime.Text = drHeader["CancelTime"].ToString();
                    this.txtRPReceiveType.TextBox.SelectedItemID = drHeader["ChargeTypeID"];
                    this.txtReceiveBankID.TextBox.SelectedItemID= drHeader["ReceiveBankID"];

                }
            }
        }

        #region -- 根据单据状态显示或者隐藏相关按钮 --

        private void SetButtonStatus()
        {
            this.txtBillDate.Enabled = true;
            this.txtCustomerID.Enabled = true;
            this.txtDescription.ReadOnly = false;
            this.txtReceiveAmount.ReadOnly = false;

            this.btnSave.Visible = true;
            this.btnDelete.Visible = true;
            this.btnCancel.Visible = true;
            this.btnUnCancel.Visible = true;
            this.btnApprove.Visible = true;
            this.btnUnApprove.Visible = true;
            this.btnAdd.Visible = true;

            if (mlReceiveBillHeaderID > 0)
            {
                DataTable dtReceiveBill = ExecuteSQL.CallView(109, "", "ReceiveBillHeaderID=" + mlReceiveBillHeaderID, "");
                if (dtReceiveBill.Rows.Count > 0)
                {
                    DataRow drBill = dtReceiveBill.Rows[0];
                    this.txtChangedBy.Text = drBill["ChangedBy"].ToString();
                    this.txtChangeTime.Text = drBill["ChangeTime"].ToString();
                    this.txtApproveBy.Text = drBill["ApproveBy"].ToString();
                    this.txtApproveTime.Text = drBill["ApproveTime"].ToString();
                    this.txtCancelBy.Text = drBill["CancelBy"].ToString();
                    this.txtCancelTime.Text = drBill["CancelTime"].ToString();
                    bool bolIsApprove = LBConverter.ToBoolean(drBill["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                    if (bolIsApprove)
                    {
                        this.lblBillStatus.Visible = true;
                        this.lblBillStatus.Text = "(审核)";
                        this.txtBillStatus.Text = "已审核";

                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                        this.btnCancel.Visible = false;
                        this.btnUnCancel.Visible = false;
                        this.btnApprove.Visible = false;

                        this.txtBillDate.Enabled = false;
                        this.txtCustomerID.Enabled = false;
                        this.txtDescription.ReadOnly = true;
                        this.txtReceiveAmount.ReadOnly = true;
                        this.btnAdd.Visible = true;
                    }
                    else if (bolIsCancel)
                    {
                        this.lblBillStatus.Visible = true;
                        this.lblBillStatus.Text = "(作废)";
                        this.txtBillStatus.Text = "已作废";

                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                        this.btnCancel.Visible = false;
                        this.btnApprove.Visible = false;
                        this.btnUnApprove.Visible = false;

                        this.txtBillDate.Enabled = false;
                        this.txtCustomerID.Enabled = false;
                        this.txtDescription.ReadOnly = true;
                        this.txtReceiveAmount.ReadOnly = true;
                        this.btnAdd.Visible = true;
                    }
                    else
                    {
                        this.lblBillStatus.Visible = false;
                        this.txtBillStatus.Text = "未审核";

                        this.btnUnCancel.Visible = false;
                        this.btnUnApprove.Visible = false;
                        this.btnAdd.Visible = true;
                    }
                }
                else
                {
                    mlReceiveBillHeaderID = 0;

                    this.lblBillStatus.Visible = false;
                    this.txtBillStatus.Text = "添加";

                    this.btnDelete.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnUnCancel.Visible = false;
                    this.btnApprove.Visible = false;
                    this.btnUnApprove.Visible = false;
                    this.btnAdd.Visible = true;

                    ClearFieldValue();
                }
            }
            else
            {
                this.lblBillStatus.Visible = false;
                this.txtBillStatus.Text = "添加";

                this.btnDelete.Visible = false;
                this.btnCancel.Visible = false;
                this.btnUnCancel.Visible = false;
                this.btnApprove.Visible = false;
                this.btnUnApprove.Visible = false;
                this.btnAdd.Visible = false;

                ClearFieldValue();
            }
        }

        private void ClearFieldValue()
        {
            this.txtApproveBy.Text = "";
            this.txtApproveTime.Text = "";
            this.txtBillStatus.Text = "添加";
            this.txtCancelBy.Text = "";
            this.txtCancelTime.Text = "";
            this.txtChangedBy.Text = "";
            this.txtChangeTime.Text = "";
            this.txtCustomerID.Text = "";
            this.txtDescription.Text = "";
            this.txtReceiveAmount.Text = "0";
            this.txtReceiveBillCode.Text = "";
        }

        #endregion

        #region -- 按钮事件 --

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否添加新充值单，当前信息将被清空？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mlReceiveBillHeaderID = 0;

                    SetButtonStatus();
                }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.VerifyTextBoxIsEmpty();//校验控件值是否为空

                SaveBillHeader();
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                SetButtonStatus();//更新状态
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除充值单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlReceiveBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13302, parmCol, out dsReturn, out dictValue);
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
                if (mlReceiveBillHeaderID > 0)
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(13303, parmCol, out dsReturn, out dictValue);
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("审核成功！");
                    SetButtonStatus();//更新状态
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
                if (mlReceiveBillHeaderID > 0)
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(13304, parmCol, out dsReturn, out dictValue);
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("取消审核成功！");
                    SetButtonStatus();//更新状态
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认作废充值单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlReceiveBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13305, parmCol, out dsReturn, out dictValue);

                        SetButtonStatus();//更新状态
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认反作废充值单？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlReceiveBillHeaderID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13306, parmCol, out dsReturn, out dictValue);

                        SetButtonStatus();//更新状态
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        #endregion -- 按钮事件 --

        #region -- 保存 --

        private void SaveBillHeader()
        {
            int iSPType = 13300;
            if (mlReceiveBillHeaderID > 0)
            {
                iSPType = 13301;
            }

            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
            if (lCustomerID == 0)
            {
                throw new Exception("请输入正确的客户名称！");
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("ReceiveBillHeaderID", enLBDbType.Int64, mlReceiveBillHeaderID));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, this.txtBillDate.Text));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("ReceiveAmount", enLBDbType.Decimal, this.txtReceiveAmount.Text));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("ChargeTypeID", enLBDbType.Int32, this.txtRPReceiveType.TextBox.SelectedItemID));
            parmCol.Add(new LBParameter("ReceiveBankID", enLBDbType.String, this.txtReceiveBankID.TextBox.SelectedItemID));
            //parmCol.Add(new LBParameter("SalesReceiveAmountAdd", enLBDbType.Decimal, this.txtSalesReceiveAmountAdd.Text));
            //parmCol.Add(new LBParameter("SalesReceiveAmountReduce", enLBDbType.Decimal, this.txtSalesReceiveAmountReduce.Text));
            //parmCol.Add(new LBParameter("OriginalAmount", enLBDbType.Decimal, this.txtOriginalAmount.Text));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("ReceiveBillHeaderID"))
            {
                mlReceiveBillHeaderID = LBConverter.ToInt64(dictValue["ReceiveBillHeaderID"]);
            }
            if (dictValue.ContainsKey("ReceiveBillCode"))
            {
                this.txtReceiveBillCode.Text = dictValue["ReceiveBillCode"].ToString();
            }
        }

        #endregion

        private void TxtReceiveAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal decReceiveAmount = LBConverter.ToDecimal(this.txtReceiveAmount.Text);
                this.txtReceiveAmount.Text = String.Format("{0:N}", decReceiveAmount);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //private void txtRPReceiveType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int iRPReceiveType = LBConverter.ToInt32(txtRPReceiveType.TextBox.SelectedItemID);
        //        if (iRPReceiveType == 1)
        //        {
        //            lblBank.Visible = true;
        //            this.txtReceiveBankID.Visible = true;
        //        }
        //        else
        //        {
        //            lblBank.Visible = false;
        //            this.txtReceiveBankID.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
        //    }
        //}
    }
}
