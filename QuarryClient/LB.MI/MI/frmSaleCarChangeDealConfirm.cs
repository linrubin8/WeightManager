using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.MI.MI.MIControls;
using LB.WinFunction;
using LB.Common;

namespace LB.MI.MI
{
    public partial class frmSaleCarChangeDealConfirm : LBUIPageBase
    {
        ChangeItem _changeItem;
        long _SaleCarInBillID;
        string _ChangeDesc="";
        int _IsPayMoney = 0;
        public bool IsSuccess = false;
        public frmSaleCarChangeDealConfirm(long SaleCarInBillID, ChangeItem changeItem,string ChangeDesc)
        {
            _changeItem = changeItem;
            _ChangeDesc = ChangeDesc;
            _SaleCarInBillID = SaleCarInBillID;
            InitializeComponent();
            this.gbAmoutDeal.Visible = false;
            if (changeItem.NewCarID != changeItem.OldCarID)
            {
                SaleChangeItem item = new SaleChangeItem("车牌号码", changeItem.OldCarNum, changeItem.NewCarNum);
                item.Dock = DockStyle.Top;
                item.TabIndex = 0;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewCustomerName != changeItem.OldCustomerName)
            {
                SaleChangeItem item = new SaleChangeItem("客户名称", changeItem.OldCustomerName, changeItem.NewCustomerName);
                item.Dock = DockStyle.Top;
                item.TabIndex = 0;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewItemName != changeItem.OldItemName)
            {
                SaleChangeItem item = new SaleChangeItem("货物名称", changeItem.OldItemName, changeItem.NewItemName);
                item.Dock = DockStyle.Top;
                item.TabIndex = 1;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewReceiveName != changeItem.OldReceiveName)
            {
                SaleChangeItem item = new SaleChangeItem("收款方式", changeItem.OldReceiveName, changeItem.NewReceiveName);
                item.Dock = DockStyle.Top;
                item.TabIndex = 2;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewCalculateName != changeItem.OldCalculateName)
            {
                SaleChangeItem item = new SaleChangeItem("计价方式", changeItem.OldCalculateName, changeItem.NewCalculateName);
                item.Dock = DockStyle.Top;
                item.TabIndex = 3;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewPrice != changeItem.OldPrice)
            {
                SaleChangeItem item = new SaleChangeItem("单价", changeItem.OldPrice.ToString("0.000"), changeItem.NewPrice.ToString("0.000"));
                item.Dock = DockStyle.Top;
                item.TabIndex = 4;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            if (changeItem.NewAmount != changeItem.OldAmount)
            {
                SaleChangeItem item = new SaleChangeItem("金额", changeItem.OldAmount.ToString("0.00"), changeItem.NewAmount.ToString("0.000"));
                item.Dock = DockStyle.Top;
                item.TabIndex = 5;
                item.BringToFront();
                this.gbChange.Controls.Add(item);
            }
            StringBuilder strAmountDeal = new StringBuilder();
            
            if (changeItem.NewAmount != changeItem.OldAmount && 
                changeItem.NewCustomerName == changeItem.OldCustomerName)
            {
                this.gbAmoutDeal.Visible = true;
                if (changeItem.NewAmount > changeItem.OldAmount)
                {
                    this.lblNeedPay1.Text = (changeItem.NewAmount - changeItem.OldAmount).ToString("0.00");
                    pnlDealAmount1.Visible = true;
                }
                else if (changeItem.NewAmount < changeItem.OldAmount)
                {
                    this.lblNeedPay2.Text = (changeItem.OldAmount - changeItem.NewAmount).ToString("0.00");
                    pnlDealAmount2.Visible = true;
                }
            }
            else if (changeItem.NewCustomerName != changeItem.OldCustomerName&&
                    changeItem.NewAmount == changeItem.OldAmount)
            {
                this.gbAmoutDeal.Visible = true;
                pnlDealAmount3.Visible = true;
                this.lblNeedPay3.Text ="涉及石款："+ changeItem.NewAmount.ToString();
                this.lblAddToCustomer.Text = changeItem.OldCustomerName;
                this.lblDelToCustomer.Text = changeItem.NewCustomerName;
            }
            else if (changeItem.NewCustomerName != changeItem.OldCustomerName &&
                    changeItem.NewAmount != changeItem.OldAmount)
            {
                this.gbAmoutDeal.Visible = true;
                pnlDealAmount4.Visible = true;
                this.lblNewAmount4.Text = changeItem.NewAmount.ToString();
                this.lblOldAmount4.Text = changeItem.OldAmount.ToString();
                this.lblAddToCustomer2.Text = changeItem.OldCustomerName;
                this.lblDelToCustomer2.Text = changeItem.NewCustomerName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal PayMoney=0;
                if (this.rbReturnAmount1.Checked)
                {
                    decimal.TryParse(this.txtReturnAmount1.Text, out PayMoney);
                }

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarChangeBillID", enLBDbType.Int64, 0));
                parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, _SaleCarInBillID));
                parmCol.Add(new LBParameter("ChangeDesc", enLBDbType.String, _ChangeDesc));
                parmCol.Add(new LBParameter("ChangeDetail", enLBDbType.String, ""));
                parmCol.Add(new LBParameter("IsPayMoney", enLBDbType.Boolean, this.rbReturnAmount1.Checked?1:0));
                parmCol.Add(new LBParameter("PayMoney", enLBDbType.Decimal, PayMoney));
                parmCol.Add(new LBParameter("NewCustomerID", enLBDbType.Int64, _changeItem.NewCustomerID));
                parmCol.Add(new LBParameter("NewItemID", enLBDbType.Int64, _changeItem.NewItemID));
                parmCol.Add(new LBParameter("NewCarID", enLBDbType.Int64, _changeItem.NewCarID));
                parmCol.Add(new LBParameter("NewReceiveType", enLBDbType.Int32, _changeItem.NewReceiveType));
                parmCol.Add(new LBParameter("NewCalculateType", enLBDbType.Int32, _changeItem.NewCalculateType));
                parmCol.Add(new LBParameter("NewPrice", enLBDbType.Decimal, _changeItem.NewPrice));
                parmCol.Add(new LBParameter("NewAmount", enLBDbType.Decimal, _changeItem.NewAmount));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, _changeItem.NewDescription));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14115, parmCol, out dsReturn, out dictValue);

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("单据变更成功！");
                IsSuccess = true;
                this.Close();
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
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }

    public class ChangeItem
    {
        public long NewItemID;
        public long NewCustomerID;
        public long NewCarID;
        public int NewReceiveType;
        public int NewCalculateType;
        public string NewCarNum;
        public string NewItemName;
        public string NewCustomerName;
        public string NewReceiveName;
        public string NewCalculateName;
        public string NewDescription;
        public decimal NewPrice;
        public decimal NewAmount;
        public long OldItemID;
        public long OldCustomerID;
        public long OldCarID;
        public int OldReceiveType;
        public int OldCalculateType;
        public string OldCarNum;
        public string OldItemName;
        public string OldCustomerName;
        public string OldReceiveName;
        public string OldCalculateName;
        public string OldDescription;
        public decimal OldPrice;
        public decimal OldAmount;

        public ChangeItem(
            long NewItemID,long NewCustomerID,int NewReceiveType,int NewCalculateType,
            string NewCarNum, string NewItemName,string NewCustomerName,string NewReceiveName,string NewCalculateName,
            decimal NewPrice,decimal NewAmount,long NewCarID,string NewDescription,
            long OldItemID, long OldCustomerID, int OldReceiveType, int OldCalculateType,
            string OldCarNum, string OldItemName, string OldCustomerName, string OldReceiveName, string OldCalculateName,
            decimal OldPrice, decimal OldAmount, long OldCarID, string OldDescription)
        {
            this.NewItemID = NewItemID;
            this.NewCustomerID = NewCustomerID;
            this.NewCarID = NewCarID;
            this.NewReceiveType = NewReceiveType;
            this.NewCalculateType = NewCalculateType;
            this.NewCarNum = NewCarNum;
            this.NewItemName = NewItemName;
            this.NewCustomerName = NewCustomerName;
            this.NewReceiveName = NewReceiveName;
            this.NewCalculateName = NewCalculateName;
            this.NewDescription = NewDescription;
            this.NewPrice = NewPrice;
            this.NewAmount = NewAmount;
            this.OldItemID = OldItemID;
            this.OldCustomerID = OldCustomerID;
            this.OldCarID = OldCarID;
            this.OldReceiveType = OldReceiveType;
            this.OldCalculateType = OldCalculateType;
            this.OldCarNum = OldCarNum;
            this.OldItemName = OldItemName;
            this.OldCustomerName = OldCustomerName;
            this.OldReceiveName = OldReceiveName;
            this.OldCalculateName = OldCalculateName;
            this.OldPrice = OldPrice;
            this.OldAmount = OldAmount;
            this.OldDescription = OldDescription;
        }
    }
}
