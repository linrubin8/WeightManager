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

namespace LB.MI.MI
{
    public partial class frmSaleCarChangeBill : LBUIPageBase
    {
        private long _SaleCarInBillID;
        private long _SaleCarOutBillID;
        private long _CarID=0;
        private DataRow _OrgBill = null;
        private DataTable _DTReceiveType = null;
        private DataTable _DTCalculateType = null;

        public frmSaleCarChangeBill(long lSaleCarInBillID,long lSaleCarOutBillID)
        {
            InitializeComponent();
            this._SaleCarInBillID = lSaleCarInBillID;
            _SaleCarOutBillID = lSaleCarOutBillID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitTextDataSource();

            ReadSaleInfo();
        }

        private void ReadSaleInfo()
        {
            DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleCarInBillID="+_SaleCarInBillID, "");
            DataTable dtReceiveType = _DTReceiveType;
            DataTable dtCalculateType = _DTCalculateType;
            if (dtBill.Rows.Count > 0)
            {
                DataRow drBill = dtBill.Rows[0];
                _OrgBill = drBill;
                _CarID = LBConverter.ToInt64(drBill["CarID"]);
                this.lblCarNum.Text = drBill["CarNum"].ToString().TrimEnd();
                this.lblCustomName.Text = drBill["CustomerName"].ToString().TrimEnd();
                this.lblItemName.Text = drBill["ItemName"].ToString().TrimEnd();
                DataRow[] drAry = dtReceiveType.Select("ConstValue=" + drBill["ReceiveType"].ToString());
                if(drAry.Length>0)
                    this.lblReceiveType.Text = drAry[0]["ConstText"].ToString().TrimEnd();
                drAry = dtCalculateType.Select("ConstValue=" + drBill["CalculateType"].ToString());
                if (drAry.Length > 0)
                    this.lblCalculateType.Text = drAry[0]["ConstText"].ToString().TrimEnd();
                this.lblTotalWeight.Text= drBill["TotalWeight"].ToString().TrimEnd();
                this.lblCarTare.Text = drBill["CarTare"].ToString().TrimEnd();
                this.lblSuttleWeight.Text = drBill["SuttleWeight"].ToString().TrimEnd();
                this.lblPrice.Text = drBill["Price"].ToString().TrimEnd();
                this.lblAmount.Text = drBill["Amount"].ToString().TrimEnd();

                this.txtCarID.TextBox.SelectedItemID = drBill["CarID"];
                this.txtCustomerID.TextBox.SelectedItemID = drBill["CustomerID"];
                this.txtItemID.TextBox.SelectedItemID= drBill["ItemID"];
                this.txtCalculateType.SelectedValue = drBill["CalculateType"];
                this.txtReceiveType.SelectedValue= drBill["ReceiveType"];
                this.txtDescription.TextBox.Text = drBill["Description"].ToString();
                this.txtTotalWeight.Text= drBill["TotalWeight"].ToString().TrimEnd();
                this.txtCarTare.Text= drBill["CarTare"].ToString().TrimEnd();
                this.txtSuttleWeight.Text = drBill["SuttleWeight"].ToString().TrimEnd();
                this.txtPrice.Text= drBill["Price"].ToString().TrimEnd();
                this.txtAmount.Text = drBill["Amount"].ToString().TrimEnd();
            }
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

            _DTReceiveType = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            _DTCalculateType = LB.Common.LBConst.GetConstData("CalculateType");//计价方式

            DataTable dtCar = ExecuteSQL.CallView(113, "CarID,CarNum,SortLevel", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            this.txtReceiveType.DataSource = _DTReceiveType;
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            this.txtCalculateType.DataSource = _DTCalculateType;
            this.txtCalculateType.DisplayMember = "ConstText";
            this.txtCalculateType.ValueMember = "ConstValue";

            DataTable dtCustom = ExecuteSQL.CallView(110);
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;
            
            DataTable dtDesc = ExecuteSQL.CallView(121);
            this.txtDescription.TextBox.LBViewType = 121;
            this.txtDescription.TextBox.IDColumnName = "DescriptionID";
            this.txtDescription.TextBox.TextColumnName = "Description";
            this.txtDescription.TextBox.PopDataSource = dtDesc.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtCarID.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;
            this.txtDescription.TextBox.IsAllowNotExists = true;

            //this.txtItemID.TextBox.GotFocus += CoolText_GotFocus;
            //this.txtCustomerID.TextBox.GotFocus += CoolText_GotFocus;

            //this.txtItemID.TextBox.LostFocus += CoolText_LostFocus;
            //this.txtCustomerID.TextBox.LostFocus += CoolText_LostFocus;

            this.txtItemID.TextBox.TextChanged += ItemTextBox_TextChanged;
            this.txtCustomerID.TextBox.TextChanged += CustomerTextBox_TextChanged;
            this.txtCalculateType.TextChanged += TxtCalculateType_TextChanged;

            this.txtTotalWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtSuttleWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtPrice.TextChanged += TxtCalAmount_TextChanged;
            this.txtCarTare.TextChanged += TxtCalAmount_TextChanged;
        }

        private string GetReceiveTypeName(int ReceiveType)
        {
            string strValue = "";
            DataRow[] drAry = _DTReceiveType.Select("ConstValue="+ ReceiveType);
            if (drAry.Length > 0)
                strValue = drAry[0]["ConstText"].ToString().TrimEnd();
            return strValue;
        }

        private string GetCalculateTypeName(int CalculateType)
        {
            string strValue = "";
            DataRow[] drAry = _DTCalculateType.Select("ConstValue=" + CalculateType);
            if (drAry.Length > 0)
                strValue = drAry[0]["ConstText"].ToString().TrimEnd();
            return strValue;
        }
        #endregion

        #region -- 计算 金额=净重*单价 --

        private void CalAmount()
        {
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);//0按重量计算 1按车计算
            this.txtSuttleWeight.Text = (decTotalWeight - decCarTare).ToString("0");
            decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
            if (iCalculateType == 0)
                this.txtAmount.Text = (decPrice * (decTotalWeight - decCarTare)).ToString("0.0");
            else
                this.txtAmount.Text = decPrice.ToString("0.0");
        }

        #endregion

        //选择物料触发事件
        private void ItemTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //读取物料价格
                ReadPrice();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //选择客户触发事件
        private void CustomerTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //读取物料价格
                ReadPrice();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 读取物料价格
        /// </summary>
        /// <param name="sender"></param>
        private void ReadPrice()
        {
            //if (sender == this.txtCustomerID.TextBox || sender == this.txtCarID.TextBox || sender == this.txtItemID.TextBox || sender == this.txtCalculateType)
            {
                this.txtPrice.Text = "0";
                
                string strItemName = this.txtItemID.TextBox.Text.ToString();
                string strCustomerName = this.txtCustomerID.TextBox.Text.ToString();

                if ( strItemName == "")
                    return;

                long lItemID = 0;
                long lCustomerID = 0;

                using (DataTable dtItem = ExecuteSQL.CallView(203, "ItemID", "ItemName='" + strItemName + "'", ""))
                {
                    if (dtItem.Rows.Count > 0)
                    {
                        lItemID = LBConverter.ToInt64(dtItem.Rows[0]["ItemID"]);
                    }
                }

                using (DataTable dtCustomer = ExecuteSQL.CallView(112, "CustomerID,TotalReceivedAmount,SalesReceivedAmount", "CustomerName='" + strCustomerName + "'", ""))
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        lCustomerID = LBConverter.ToInt64(dtCustomer.Rows[0]["CustomerID"]);

                    }
                }

                ReadPrice(_CarID, lItemID, lCustomerID);
            }
        }

        private void ReadPrice(long lCarID, long lItemID, long lCustomerID)
        {
            this.txtPrice.Text = "0";
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);
            if (lCarID > 0 && lItemID > 0)
            {
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
                parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
                parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(13608, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("Price"))
                {
                    this.txtPrice.Text = LBConverter.ToDecimal(dictValue["Price"]).ToString("0.000");
                }
            }
        }

        private void TxtCalAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalAmount();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void TxtCalculateType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //读取物料价格
                ReadPrice();
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
                long lNewCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                long lNewItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lNewCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                int iNewReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
                int iNewCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);
                decimal decNewAmount = _SaleCarOutBillID > 0 ? LBConverter.ToDecimal(this.txtAmount.Text):0;
                decimal decNewPrice = _SaleCarOutBillID>0?LBConverter.ToDecimal(this.txtPrice.Text):0;
                string strNewDescription = this.txtDescription.Text;
                string strNewItemName = this.txtItemID.TextBox.Text;
                string strNewCarNum = this.txtCarID.TextBox.Text;
                string strNewCustomerName = this.txtCustomerID.TextBox.Text;
                string strNewReceiveName = this.GetReceiveTypeName(iNewReceiveType);
                string strNewCalculateName = this.GetCalculateTypeName(iNewCalculateType);

                long lOldCarID = LBConverter.ToInt64(this._OrgBill["CarID"]);
                long lOldItemID = LBConverter.ToInt64(this._OrgBill["ItemID"]);
                long lOldCustomerID = LBConverter.ToInt64(this._OrgBill["CustomerID"]);
                string strOldItemName = this._OrgBill["ItemName"].ToString().TrimEnd();
                string strOldCustomerName = this._OrgBill["CustomerName"].ToString().TrimEnd();
                string strOldCarNum = this._OrgBill["CarNum"].ToString().TrimEnd();
                int iOldReceiveType = LBConverter.ToInt32(this._OrgBill["ReceiveType"]);
                int iOldCalculateType = LBConverter.ToInt32(this._OrgBill["CalculateType"]);
                decimal decOldAmount = _SaleCarOutBillID > 0 ? LBConverter.ToDecimal(this._OrgBill["Amount"]):0;
                decimal decOldPrice = _SaleCarOutBillID > 0 ? LBConverter.ToDecimal(this._OrgBill["Price"]):0;
                string strOldDescription = this._OrgBill["Description"].ToString();
                string strOldReceiveName = this.GetReceiveTypeName(iOldReceiveType);
                string strOldCalculateName = this.GetCalculateTypeName(iOldCalculateType);

                string strChangeDesc = "";
                bool bolIsChanged = false;
                if (this._OrgBill["ItemID"].ToString() != lNewItemID.ToString())
                {
                    bolIsChanged = true;
                    if (strChangeDesc != "")
                    {
                        strChangeDesc += ";";
                    }
                    strChangeDesc += this._OrgBill["ItemName"].ToString() + " -> " + this.txtItemID.TextBox.Text;
                }

                if (this._OrgBill["CarID"].ToString() != lNewCarID.ToString())
                {
                    bolIsChanged = true;
                    if (strChangeDesc != "")
                    {
                        strChangeDesc += ";";
                    }
                    strChangeDesc += this._OrgBill["CarNum"].ToString() + " -> " + this.txtCarID.TextBox.Text;
                }

                if (this._OrgBill["CustomerID"].ToString() != lNewCustomerID.ToString())
                {
                    bolIsChanged = true;
                    if (strChangeDesc != "")
                    {
                        strChangeDesc += ";";
                    }
                    strChangeDesc += this._OrgBill["CustomerName"].ToString() + " -> " + this.txtCustomerID.TextBox.Text;
                }

                if (this._OrgBill["Description"].ToString() != strNewDescription)
                {
                    bolIsChanged = true;
                    if (strChangeDesc != "")
                    {
                        strChangeDesc += ";";
                    }
                    strChangeDesc += this._OrgBill["Description"].ToString() + " -> " + this.txtDescription.TextBox.Text;
                }

                if (_SaleCarOutBillID > 0)
                {
                    if (this._OrgBill["ReceiveType"].ToString() != iNewReceiveType.ToString())
                    {
                        bolIsChanged = true;
                        if (strChangeDesc != "")
                        {
                            strChangeDesc += ";";
                        }
                        strChangeDesc +=this.GetReceiveTypeName(LBConverter.ToInt32( this._OrgBill["ReceiveType"])) + " -> " + this.GetReceiveTypeName(iNewReceiveType);
                    }

                    if (this._OrgBill["CalculateType"].ToString() != iNewCalculateType.ToString())
                    {
                        bolIsChanged = true;
                        if (strChangeDesc != "")
                        {
                            strChangeDesc += ";";
                        }
                        strChangeDesc += this.GetCalculateTypeName(LBConverter.ToInt32(this._OrgBill["CalculateType"])) + " -> " + this.GetCalculateTypeName(iNewCalculateType);
                    }

                    if(LBConverter.ToDecimal(this._OrgBill["Price"]) != decNewPrice)
                    {
                        bolIsChanged = true;
                        if (strChangeDesc != "")
                        {
                            strChangeDesc += ";";
                        }
                        strChangeDesc += LBConverter.ToDecimal(this._OrgBill["Price"]).ToString("0.000") + " -> " + decNewPrice.ToString("0.000");
                    }

                    if (LBConverter.ToDecimal(this._OrgBill["Amount"]) != decNewAmount)
                    {
                        bolIsChanged = true;
                        if (strChangeDesc != "")
                        {
                            strChangeDesc += ";";
                        }
                        strChangeDesc += LBConverter.ToDecimal(this._OrgBill["Amount"]).ToString("0.000") + " -> " + decNewAmount.ToString("0.000");
                    }
                }

                if (!bolIsChanged)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("变更前后信息一致！");
                    return;
                }

                ChangeItem changeItem = new ChangeItem(lNewItemID, lNewCustomerID, iNewReceiveType, iNewCalculateType,
                    strNewCarNum,strNewItemName, strNewCustomerName, strNewReceiveName, strNewCalculateName, decNewPrice, decNewAmount, lNewCarID, strNewDescription,
                    lOldItemID, lOldCustomerID, iOldReceiveType, iOldCalculateType,
                    strOldCarNum,strOldItemName, strOldCustomerName, strOldReceiveName, strOldCalculateName, decOldPrice, decOldAmount, lOldCarID, strOldDescription);

                frmSaleCarChangeDealConfirm frmConfirm = new MI.frmSaleCarChangeDealConfirm(_SaleCarInBillID, changeItem, strChangeDesc);
                LBShowForm.ShowDialog(frmConfirm);

                if (frmConfirm.IsSuccess)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void txtSuttleWeight_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
