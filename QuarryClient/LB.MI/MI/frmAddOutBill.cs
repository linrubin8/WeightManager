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
    public partial class frmAddOutBill : LBUIPageBase
    {
        long mlSaleCarInBillID = 0;
        public frmAddOutBill()
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
            this.txtReceiveType.DataSource = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            this.txtCalculateType.DataSource = LB.Common.LBConst.GetConstData("CalculateType");//计价方式
            this.txtCalculateType.DisplayMember = "ConstText";
            this.txtCalculateType.ValueMember = "ConstValue";

            DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtCar = ExecuteSQL.CallView(135, "", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 135;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            DataTable dtDesc = ExecuteSQL.CallView(121);
            this.txtAddReason.TextBox.LBViewType = 121;
            this.txtAddReason.TextBox.IDColumnName = "DescriptionID";
            this.txtAddReason.TextBox.TextColumnName = "Description";
            this.txtAddReason.TextBox.PopDataSource = dtDesc.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtCarID.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;
            this.txtAddReason.TextBox.IsAllowNotExists = true;

            this.txtCarID.TextBox.TextChanged += CarTextBox_TextChanged;
            this.txtCalculateType.TextChanged += TxtCalculateType_TextChanged;

            this.txtTotalWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtSuttleWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtPrice.TextChanged += TxtCalAmount_TextChanged;
            this.txtCarTare.TextChanged += TxtCalAmount_TextChanged;

            this.txtCustomerID.TextBox.ReadOnly = true;
            this.txtItemID.TextBox.ReadOnly = true;
        }

        #endregion
        //选择车辆触发事件
        private void CarTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mlSaleCarInBillID = 0;
                string strCarNum = this.txtCarID.TextBox.Text.ToString();
                long lCarID = 0;
                long lCustomerID = 0;
                #region -- 读取车辆ID号 --
                using (DataTable dtCar = ExecuteSQL.CallView(117, "CarID,CustomerID", "CarNum='" + strCarNum + "'", ""))
                {
                    if (dtCar.Rows.Count > 0)
                    {
                        lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                        lCustomerID = LBConverter.ToInt64(dtCar.Rows[0]["CustomerID"]);
                    }
                }
                #endregion -- 读取车辆ID号 --

                if (lCarID > 0)//如果存在该车辆
                {
                    #region --  判断该车辆是否已入场 --

                    bool bolExists = false;
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14101, parmCol, out dsReturn, out dictValue);
                    if (dictValue != null && dictValue.Keys.Count > 0)
                    {
                        if (dictValue.ContainsKey("IsReaded"))
                        {
                            bolExists = LBConverter.ToBoolean(dictValue["IsReaded"]);
                        }

                        if (bolExists)//已读取入场记录
                        {
                            this.txtTotalWeight.Text = "";//清空毛重
                            this.txtPrice.Text = "";//清空单价

                            #region -- 读取入场记录，并将入场记录值填写到对应控件 --

                            if (dictValue.ContainsKey("CalculateType"))
                            {
                                this.txtCalculateType.SelectedValue = LBConverter.ToInt32(dictValue["CalculateType"]);
                            }
                            if (dictValue.ContainsKey("ItemID"))
                            {
                               this.txtItemID.TextBox.SelectedItemID = LBConverter.ToInt64(dictValue["ItemID"]);
                            }
                            if (dictValue.ContainsKey("CustomerID"))
                            {
                                this.txtCustomerID.TextBox.SelectedItemID = LBConverter.ToInt64(dictValue["CustomerID"]);
                            }
                            if (dictValue.ContainsKey("CarTare"))
                            {
                                this.txtCarTare.Text = LBConverter.ToString(dictValue["CarTare"]);
                            }
                            if (dictValue.ContainsKey("ReceiveType"))
                            {
                                this.txtReceiveType.SelectedValue = LBConverter.ToInt32(dictValue["ReceiveType"]);
                            }
                            if (dictValue.ContainsKey("SaleCarInBillID"))
                            {
                                mlSaleCarInBillID = LBConverter.ToInt64(dictValue["SaleCarInBillID"]);
                            }
                            //读取物料价格
                            ReadPrice();

                            #endregion -- 读取入场记录，并将入场记录值填写到对应控件 --
                        }
                        else
                        {
                            #region -- 该车无入场记录 --
                            this.txtCustomerID.TextBox.Text = "";
                            this.txtItemID.TextBox.Text = "";
                            this.txtTotalWeight.Text = "";
                            this.txtPrice.Text = "";
                            #endregion -- 该车无入场记录 --
                        }
                    }

                    #endregion
                }
                else
                {
                    this.txtCustomerID.TextBox.Text = "";
                    this.txtItemID.TextBox.Text = "";
                    this.txtTotalWeight.Text = "";
                    this.txtPrice.Text = "";
                }
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

        #region -- 计算 金额=净重*单价 --

        private void CalAmount()
        {
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);//0按重量计算 1按车计算
            this.txtSuttleWeight.Text = (decTotalWeight - decCarTare).ToString("0");
            decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
            if (iCalculateType == 0)
                this.txtAmount.Text = (decPrice * (decTotalWeight - decCarTare)).ToString("0.00");
            else
                this.txtAmount.Text = decPrice.ToString("0.00");
        }

        #endregion

        /// <summary>
        /// 读取物料价格
        /// </summary>
        /// <param name="sender"></param>
        private void ReadPrice()
        {
            //if (sender == this.txtCustomerID.TextBox || sender == this.txtCarID.TextBox || sender == this.txtItemID.TextBox || sender == this.txtCalculateType)
            {
                this.txtPrice.Text = "0";

                string strCarNum = this.txtCarID.TextBox.Text.ToString();
                string strItemName = this.txtItemID.TextBox.Text.ToString();
                string strCustomerName = this.txtCustomerID.TextBox.Text.ToString();

                if (this.txtCustomerID.TextBox.SelectedRow != null)
                {
                    decimal decTotalReceivedAmount = LBConverter.ToDecimal(this.txtCustomerID.TextBox.SelectedRow["TotalReceivedAmount"]);
                    decimal decSalesReceivedAmount = LBConverter.ToDecimal(this.txtCustomerID.TextBox.SelectedRow["SalesReceivedAmount"]);
                }

                if (strCarNum == "" || strItemName == "")
                    return;

                long lCarID = 0;
                long lItemID = 0;
                long lCustomerID = 0;
                using (DataTable dtCar = ExecuteSQL.CallView(113, "CarID", "CarNum='" + strCarNum + "'", ""))
                {
                    if (dtCar.Rows.Count > 0)
                    {
                        lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                    }
                }
                using (DataTable dtItem = ExecuteSQL.CallView(203, "ItemID", "ItemName='" + strItemName + "'", ""))
                {
                    if (dtItem.Rows.Count > 0)
                    {
                        lItemID = LBConverter.ToInt64(dtItem.Rows[0]["ItemID"]);
                    }
                }

                using (DataTable dtCustomer = ExecuteSQL.CallView(112, "CustomerID,ReceiveType,TotalReceivedAmount,SalesReceivedAmount", "CustomerName='" + strCustomerName + "'", ""))
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        lCustomerID = LBConverter.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                        //客户收款方式
                        this.txtReceiveType.SelectedValue = LBConverter.ToInt32(dtCustomer.Rows[0]["ReceiveType"]);
                    }
                }

                ReadPrice(lCarID, lItemID, lCustomerID);
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

        private long SaveOutBill(long lSaleCarInBillID)
        {
            string strCarNum = this.txtCarID.TextBox.Text.ToString();
            long lCarID = 0;
            using (DataTable dtCar = ExecuteSQL.CallView(113, "CarID", "CarNum='" + strCarNum + "'", ""))
            {
                if (dtCar.Rows.Count > 0)
                {
                    lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                }
            }

            long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
            
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);

            int iReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decSuttleWeight = LBConverter.ToDecimal(this.txtSuttleWeight.Text);
            decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
            decimal decAmount = LBConverter.ToDecimal(this.txtAmount.Text);
            decimal decCustomerPayAmount = iReceiveType == 0 ? decAmount : 0;

            string strBillDateOut = Convert.ToDateTime(this.txtBillDateIn.Text).ToString("yyyy-MM-dd") + " " +
                Convert.ToDateTime(this.txtBillTimeIn.Text).ToString("HH:mm:ss");

            if (decTotalWeight == 0)
            {
                throw new Exception("当前【毛重】值为0，无法保存！");
            }
            if (decSuttleWeight == 0)
            {
                throw new Exception("当前【净重】值为0，无法保存！");
            }
            if (decCarTare == 0)
            {
                throw new Exception("当前【皮重】值为0，无法保存！");
            }

            if (lSaleCarInBillID == 0)
            {
                throw new Exception("该车辆没有入场纪录，无法保存！");
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
            parmCol.Add(new LBParameter("SaleCarOutBillCode", enLBDbType.String, ""));
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("Price", enLBDbType.Decimal, decPrice));
            parmCol.Add(new LBParameter("Amount", enLBDbType.Decimal, decAmount));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtAddReason.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("TotalWeight", enLBDbType.Decimal, decTotalWeight));
            parmCol.Add(new LBParameter("SuttleWeight", enLBDbType.Decimal, decSuttleWeight));
            //入库单信息
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));
            parmCol.Add(new LBParameter("CustomerPayAmount", enLBDbType.Decimal, decCustomerPayAmount));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, Convert.ToDateTime(strBillDateOut)));

            int iSPType = 14102;
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            long lSaleCarOutBillID = 0;
            if (dictValue.ContainsKey("SaleCarOutBillID"))
            {
                lSaleCarOutBillID = LBConverter.ToInt64(dictValue["SaleCarOutBillID"].ToString());
            }
            return lSaleCarOutBillID;
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

                long lSaleCarOutBillID = SaveOutBill(mlSaleCarInBillID);

                if (lSaleCarOutBillID > 0)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("出场记录生成成功！");
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
