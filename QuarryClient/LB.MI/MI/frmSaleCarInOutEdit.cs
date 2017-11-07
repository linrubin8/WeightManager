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
using System.IO;
using System.Threading;
using LB.Page.Helper;
using LB.Controls.Report;

namespace LB.MI.MI
{
    public partial class frmSaleCarInOutEdit : LBUIPageBase
    {
        private long mlSaleCarInBillID = 0;
        private long mlSaleCarOutBillID = 0;
        private DataRow _drBillInfo = null;
        public frmSaleCarInOutEdit(long lSaleCarInBillID)
        {
            mlSaleCarInBillID = lSaleCarInBillID;
            InitializeComponent();
            this.PageAutoSize = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //如果当前登录用户为地磅文员，则将非现金客户的单价和金额隐藏
            if (LoginInfo.UserType == 0)
            {
                txtPrice.PasswordChar = '*';
                txtAmount.IsPasswordChat = '*';
            }

            InitData();
            ReadFeildValue();

            this.txtCarID.TextBox.ReadOnly = true;
            this.txtItemID.TextBox.ReadOnly = true;
            this.txtCustomerID.TextBox.ReadOnly = true;

            this.txtPrice.TextChanged += TxtPrice_TextChanged;
            this.txtItemID.TextBox.TextChanged += ItemTextBox_TextChanged;
            this.txtCustomerID.TextBox.TextChanged += CustomerTextBox_TextChanged;

            this.picIn1.DoubleClick += Pic_DoubleClick;
            this.picIn2.DoubleClick += Pic_DoubleClick;
            this.picIn3.DoubleClick += Pic_DoubleClick;
            this.picIn4.DoubleClick += Pic_DoubleClick;
            this.picOut1.DoubleClick += Pic_DoubleClick;
            this.picOut2.DoubleClick += Pic_DoubleClick;
            this.picOut3.DoubleClick += Pic_DoubleClick;
            this.picOut4.DoubleClick += Pic_DoubleClick;

        }

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PictureBox pb = sender as PictureBox;
                frmSaleCarInOutDisneyBigPicture frm = new frmSaleCarInOutDisneyBigPicture(pb.Image);
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void TxtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal decStuffWeight = LBConverter.ToDecimal(txtSuttleWeight.Text);
                decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
                int iCalculateType = LBConverter.ToInt32(txtCalculateType.SelectedValue);
                if (iCalculateType == 0)//按重量
                {
                    this.txtAmount.Text = (decPrice * decStuffWeight).ToString("0.00");
                }
                else if (iCalculateType == 0)//按车
                {
                    this.txtAmount.Text = (decPrice * 1).ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void InitData()
        {
            //this.txtBillStatus.DataSource = LB.Common.LBConst.GetConstData("BillStatus");//单据状态
            //this.txtBillStatus.DisplayMember = "ConstText";
            //this.txtBillStatus.ValueMember = "ConstValue";
            DataTable dtCar = ExecuteSQL.CallView(113, "", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

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

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;
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

        /// <summary>
        /// 读取物料价格
        /// </summary>
        /// <param name="sender"></param>
        private void ReadPrice()
        {
            //if (sender == this.txtCustomerID.TextBox || sender == this.txtCarID.TextBox || sender == this.txtItemID.TextBox || sender == this.txtCalculateType)
            {
                this.txtPrice.Text = "0";

                string strCarNum = _drBillInfo["CarNum"].ToString();
                string strItemName = this.txtItemID.TextBox.Text.ToString();
                string strCustomerName = this.txtCustomerID.TextBox.Text.ToString();

                if (strCarNum == "" || strItemName == "")
                    return;

                long lCarID = 0;
                long lItemID = 0;
                long lCustomerID = 0;
                lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
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

        #region -- 加载图片 --
        private void ReadFeildValue()
        {
            string strFilter = "SaleCarInBillID=" + mlSaleCarInBillID;
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "");
            if (dtBill.Rows.Count > 0)
            {
                DataRow drBill = dtBill.Rows[0];
                _drBillInfo = drBill;
                mlSaleCarOutBillID = LBConverter.ToInt64(drBill["SaleCarOutBillID"]);
                this.lblBillDateIn.Text = drBill["BillDateIn"].ToString().TrimEnd();
                this.lblBillDateOut.Text = drBill["BillDateOut"].ToString().TrimEnd();
                this.txtSaleCarInBillCode.Text= drBill["SaleCarInBillCode"].ToString().TrimEnd();
                this.txtSaleCarOutBillCode.Text = drBill["SaleCarOutBillCode"].ToString().TrimEnd();

                this.txtCalculateType.SelectedValue = drBill["CalculateType"];
                this.txtItemID.TextBox.Text = drBill["ItemName"].ToString();
                this.txtCustomerID.TextBox.Text = drBill["CustomerName"].ToString();
                this.txtCarID.TextBox.Text= drBill["CarNum"].ToString();
                this.txtCarTare.Text = drBill["CarTare"].ToString();
                this.txtDescription.Text = drBill["Description"].ToString();
                this.txtReceiveType.SelectedValue = drBill["ReceiveType"];
                //this.txtSaleCarInBillID.Text = drv["SaleCarInBillID"].ToString().TrimEnd();
                //this.txtBillStatus.SelectedValue = drBill["BillStatus"];

                int iCalculateType = LBConverter.ToInt32(drBill["CalculateType"]);
                this.txtTotalWeight.Text = drBill["TotalWeight"].ToString();
                this.txtCarTare.Text = drBill["CarTare"].ToString();
                this.txtSuttleWeight.Text = drBill["SuttleWeight"].ToString();
                this.txtPrice.Text= drBill["Price"].ToString();
                this.txtAmount.Text = drBill["Amount"].ToString();
                this.lblPriceQty.Text = iCalculateType == 0 ?
                    drBill["SuttleWeight"].ToString() + "KG": "1 车";

                BillStatusStyle(
                    LBConverter.ToBoolean(drBill["IsCancel"]),
                    drBill["CancelBy"].ToString(),
                    drBill["CancelTime"].ToString(),
                    drBill["CancelDesc"].ToString(),
                    LBConverter.ToInt32(drBill["BillStatus"]));

                Thread thread = new Thread(ReadMonitoreImg);
                thread.Start();

                //读取退货单信息
                DataTable dtReturn= ExecuteSQL.CallView(137, "", strFilter, "");
                if (dtReturn.Rows.Count == 0)
                {
                    this.tabControl1.TabPages.Remove(this.tpReturnIn);
                    this.tabControl1.TabPages.Remove(this.tpReturnOut);
                }
                else
                {
                    long lSaleCarReturnBillID = LBConverter.ToInt64(dtReturn.Rows[0]["SaleCarReturnBillID"]);
                    thread = new Thread(ReadReturnImg);
                    thread.Start(lSaleCarReturnBillID);
                }
            }
        }

        private void ReadMonitoreImg()
        {
            byte[] PicIn1 = null;
            byte[] PicIn2 = null;
            byte[] PicIn3 = null;
            byte[] PicIn4 = null;
            byte[] PicOut1 = null;
            byte[] PicOut2 = null;
            byte[] PicOut3 = null;
            byte[] PicOut4 = null;

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14103, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("InMonitoreImg1"))
            {
                PicIn1 = dictValue["InMonitoreImg1"] as byte[];
                PicIn2 = dictValue["InMonitoreImg2"] as byte[];
                PicIn3 = dictValue["InMonitoreImg3"] as byte[];
                PicIn4 = dictValue["InMonitoreImg4"] as byte[];
                PicOut1 = dictValue["OutMonitoreImg1"] as byte[];
                PicOut2 = dictValue["OutMonitoreImg2"] as byte[];
                PicOut3 = dictValue["OutMonitoreImg3"] as byte[];
                PicOut4 = dictValue["OutMonitoreImg4"] as byte[];
            }

            SetImage(picIn1, PicIn1);
            SetImage(picIn2, PicIn2);
            SetImage(picIn3, PicIn3);
            SetImage(picIn4, PicIn4);
            SetImage(picOut1, PicOut1);
            SetImage(picOut2, PicOut2);
            SetImage(picOut3, PicOut3);
            SetImage(picOut4, PicOut4);
        }

        private void ReadReturnImg(object objReturnBillID)
        {
            long lSaleCarReturnBillID = LBConverter.ToInt64(objReturnBillID.ToString());
            byte[] PicIn1 = null;
            byte[] PicIn2 = null;
            byte[] PicIn3 = null;
            byte[] PicIn4 = null;
            byte[] PicOut1 = null;
            byte[] PicOut2 = null;
            byte[] PicOut3 = null;
            byte[] PicOut4 = null;

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarReturnBillID", enLBDbType.Int64, lSaleCarReturnBillID));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(30005, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("InMonitoreImg1"))
            {
                PicIn1 = dictValue["InMonitoreImg1"] as byte[];
                PicIn2 = dictValue["InMonitoreImg2"] as byte[];
                PicIn3 = dictValue["InMonitoreImg3"] as byte[];
                PicIn4 = dictValue["InMonitoreImg4"] as byte[];
                PicOut1 = dictValue["OutMonitoreImg1"] as byte[];
                PicOut2 = dictValue["OutMonitoreImg2"] as byte[];
                PicOut3 = dictValue["OutMonitoreImg3"] as byte[];
                PicOut4 = dictValue["OutMonitoreImg4"] as byte[];
            }

            SetImage(picReturnIn1, PicIn1);
            SetImage(picReturnIn2, PicIn2);
            SetImage(picReturnIn3, PicIn3);
            SetImage(picReturnIn4, PicIn4);
            SetImage(picReturnOut1, PicOut1);
            SetImage(picReturnOut2, PicOut2);
            SetImage(picReturnOut3, PicOut3);
            SetImage(picReturnOut4, PicOut4);
        }

        private void SetImage(PictureBox picBox,byte[] bImage)
        {
            if (bImage != null)
            {
                MethodInvoker func = delegate ()
                {
                    MemoryStream ms = new MemoryStream(bImage);
                    Image image = System.Drawing.Image.FromStream(ms);
                    picBox.Image = image;
                };

                if (picBox.InvokeRequired)
                {
                    picBox.BeginInvoke(func);
                }
                else
                {
                    func();
                }
            }
        }

        #endregion -- 加载图片 --

        #region -- 按钮事件 --

        private void btnChangeBill_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleCarChangeBill frmChange = new frmSaleCarChangeBill(mlSaleCarInBillID, mlSaleCarOutBillID);
                LBShowForm.ShowDialog(frmChange);
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

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                AppeoveBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void AppeoveBill()
        {
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14104, parmCol, out dsReturn, out dictValue);

            LB.WinFunction.LBCommonHelper.ShowCommonMessage("审核成功！");
            ReadFeildValue();
        }

        private void btnUnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14105, parmCol, out dsReturn, out dictValue);

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("取消审核成功！");
                ReadFeildValue();
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
                frmSaleCarInOutCancel frmCancel = new frmSaleCarInOutCancel(txtCarID.TextBox.Text);
                LBShowForm.ShowDialog(frmCancel);

                if (frmCancel.IsAllowCancel)
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14106, parmCol, out dsReturn, out dictValue);

                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("作废成功！");
                    ReadFeildValue();
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
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14107, parmCol, out dsReturn, out dictValue);

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("取消作废成功！");
                ReadFeildValue();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        

        private void btnChangeBillInfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                if (lItemID == 0)
                {
                    throw new Exception("请输入正确的货物名称！");
                }
                if (lCustomerID == 0)
                {
                    throw new Exception("请输入正确的客户名称！");
                }
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, mlSaleCarInBillID));
                parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
                parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
                parmCol.Add(new LBParameter("Price", enLBDbType.Decimal, LBConverter.ToDecimal(this.txtPrice.Text)));
                parmCol.Add(new LBParameter("Amount", enLBDbType.Decimal, LBConverter.ToDecimal(this.txtAmount.Text)));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14116, parmCol, out dsReturn, out dictValue);
                if( LB.WinFunction.LBCommonHelper.ConfirmMessage("修改成功！是否自动审核该磅单？", "提示", MessageBoxButtons.YesNo)==
                     DialogResult.Yes)
                {
                    AppeoveBill();
                }
                else
                {
                    ReadFeildValue();
                }

                btnChangeBillInfoSave.Visible = false;
                this.btnChangeBillInfoDirect.Visible = true;
                this.txtItemID.TextBox.ReadOnly = true;
                this.txtCarID.TextBox.ReadOnly = true;
                this.txtCustomerID.TextBox.ReadOnly = true;
                this.txtDescription.ReadOnly = true;
                txtPrice.ReadOnly = true;

                this.btnApprove.Visible = true;
                this.btnUnApprove.Visible = true;
                this.btnCancel.Visible = true;
                this.btnUnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        private void btnChangeBillInfoDirect_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dtBill = ExecuteSQL.CallView(123, "", "SaleCarInBillID=" + mlSaleCarInBillID, ""))
                {
                    if (dtBill.Rows.Count > 0)
                    {
                        DataRow drBill = dtBill.Rows[0];
                        int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                        if (iBillStatus == 2)
                        {
                            throw new Exception("该磅单已审核，请先【取消审核】，然后再进行改单！");
                        }
                    }
                }

                btnChangeBillInfoDirect.Visible = false;
                this.txtItemID.TextBox.ReadOnly = false;
                this.txtCarID.TextBox.ReadOnly = false;
                this.txtCustomerID.TextBox.ReadOnly = false;
                this.txtDescription.ReadOnly = false;
                txtPrice.ReadOnly = false;
                btnChangeBillInfoSave.Visible = true;

                this.btnApprove.Visible = false;
                this.btnUnApprove.Visible = false;
                this.btnCancel.Visible = false;
                this.btnUnCancel.Visible = false;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        #endregion -- 按钮事件 --

        private void btnPrintInBill_Click(object sender, EventArgs e)
        {
            try
            {
                //允许补打磅单次数
                int iAllowPrintInReportCount;
                SysConfigValue.GetSysConfig("AllowPrintInReportCount", out iAllowPrintInReportCount);

                if (iAllowPrintInReportCount == 0)
                {
                    throw new Exception("系统设置不允许补打小票！");
                }

                if (mlSaleCarInBillID == 0)
                {
                    throw new Exception("请选择需要补打的数据行！");
                }

                DataTable dtIn = ExecuteSQL.CallView(123, "PrintCount", "SaleCarInBillID=" + mlSaleCarInBillID, "");
                if (dtIn.Rows.Count > 0)
                {
                    int iInPrintCount = LBConverter.ToInt32(dtIn.Rows[0]["PrintCount"]);
                    if (iInPrintCount >= iAllowPrintInReportCount + 1)
                    {
                        throw new Exception("补打次数已超出系统设置的次数！");
                    }
                }
                LBPreviceReport.PreviceReport(mlSaleCarInBillID, enWeightType.WeightIn, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnPrintOutBill_Click(object sender, EventArgs e)
        {
            try
            {
                //允许补打磅单次数
                int iAllowPrintOutReportCount;
                SysConfigValue.GetSysConfig("AllowPrintOutReportCount", out iAllowPrintOutReportCount);

                if (iAllowPrintOutReportCount == 0)
                {
                    throw new Exception("系统设置不允许补打磅单！");
                }

                if (mlSaleCarOutBillID == 0)
                {
                    throw new Exception("请选择需要补打的数据行！");
                }

                DataTable dtOut = ExecuteSQL.CallView(124, "OutPrintCount", "SaleCarOutBillID=" + this.mlSaleCarOutBillID, "");
                if (dtOut.Rows.Count > 0)
                {
                    int iOutPrintCount = LBConverter.ToInt32(dtOut.Rows[0]["OutPrintCount"]);
                    if (iOutPrintCount >= iAllowPrintOutReportCount + 1)
                    {
                        throw new Exception("补打次数已超出系统设置的次数！");
                    }
                }

                LBPreviceReport.PreviceReport(mlSaleCarOutBillID, enWeightType.WeightOut, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void BillStatusStyle(bool bolIsCancel,string strCancelBy,string strCancelTime,
            string strCancelDesc,int iBillStatus)
        {
            this.gbCancel.Visible = false;
            if (bolIsCancel)
            {
                this.gbCancel.Visible = true;
                this.txtCancelBy.Text = strCancelBy;
                this.txtCancelTime.Text = strCancelTime;
                this.txtCancelReasion.Text = strCancelDesc;
                lblBillStatus.Text = "（已作废）";
                lblBillStatus.ForeColor = Color.Red;
            }
            else if(iBillStatus<=1)
            {
                lblBillStatus.Text = "（未审核）";
                lblBillStatus.ForeColor = Color.Blue;
            }
            else
            {
                lblBillStatus.Text = "（已审核）";
                lblBillStatus.ForeColor = Color.DarkGreen;
            }
        }
    }
}
