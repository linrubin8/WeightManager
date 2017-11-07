using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.WinFunction;
using LB.Common;
using LB.Controls;

namespace LB.MI
{
    public partial class frmAddInBill : LBUIPageBase
    {
        public frmAddInBill()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitTextDataSource();
        }

        #region -- Init TextBox DataSource --

        private void InitTextDataSource()
        {
            DataTable dtFilterType = new DataTable();
            dtFilterType.Columns.Add("Index", typeof(int));
            dtFilterType.Columns.Add("Value", typeof(string));
            dtFilterType.Rows.Add("0", "未出场记录");
            dtFilterType.Rows.Add("1", "当天磅单记录");
            dtFilterType.Rows.Add("2", "当天作废（过期）记录");
            
            DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtCar = ExecuteSQL.CallView(113, "", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            this.txtCalculateType.DataSource = LB.Common.LBConst.GetConstData("CalculateType");//计价方式
            this.txtCalculateType.DisplayMember = "ConstText";
            this.txtCalculateType.ValueMember = "ConstValue";

            DataTable dtDesc = ExecuteSQL.CallView(121);
            this.txtAddReason.TextBox.LBViewType = 121;
            this.txtAddReason.TextBox.IDColumnName = "DescriptionID";
            this.txtAddReason.TextBox.TextColumnName = "Description";
            this.txtAddReason.TextBox.PopDataSource = dtDesc.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtCarID.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;
            this.txtAddReason.TextBox.IsAllowNotExists = true;

        }

        #endregion

        private long SaveInBill()
        {
            long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
            long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
            int iReceiveType = 1;

            using (DataTable dtCustomer = ExecuteSQL.CallView(112, "CustomerID,ReceiveType,TotalReceivedAmount,SalesReceivedAmount", "CustomerID=" + lCustomerID.ToString() , ""))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    lCustomerID = LBConverter.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                    //客户收款方式
                    iReceiveType = LBConverter.ToInt32(dtCustomer.Rows[0]["ReceiveType"]);
                }
            }

            Dictionary<string, double> dictTest = new Dictionary<string, double>();
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            long lSaleCarInBillID = 0;
            
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            string strBillDateIn = Convert.ToDateTime(this.txtBillDateIn.Text).ToString("yyyy-MM-dd") + " " + 
                Convert.ToDateTime(this.txtBillTimeIn.Text).ToString("HH:mm:ss");

            if (decCarTare == 0)
            {
                throw new Exception("当前【皮重】值为0，无法保存！");
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, 0));
            parmCol.Add(new LBParameter("SaleCarInBillCode", enLBDbType.String, ""));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, Convert.ToDateTime(strBillDateIn)));
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtAddReason.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14100, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SaleCarInBillID"))
            {
                lSaleCarInBillID = LBConverter.ToInt64(dictValue["SaleCarInBillID"]);
            }
            if (dictValue.ContainsKey("SaleCarInBillCode"))
            {
                this.txtSaleCarInBillCode.Text = dictValue["SaleCarInBillCode"].ToString();
            }
            
            return lSaleCarInBillID;
        }

        private void VerifyTextBoxIsEmpty()
        {
            long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
            long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);

            if (lCarID == 0)
            {
                throw new Exception("车号不能为空或者该车号不存在！");
            }
            if (lItemID == 0)
            {
                throw new Exception("货物名称不能为空！");
            }
            if (lCustomerID == 0)
            {
                throw new Exception("客户不能为空或者该客户不存在！");
            }

            if (this.txtAddReason.Text.TrimEnd() == "")
            {
                throw new Exception("手工录入原因不能为空！");
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyTextBoxIsEmpty();

                long lSaleCarInBillID= SaveInBill();

                if (lSaleCarInBillID > 0)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("入场记录生成成功！");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
