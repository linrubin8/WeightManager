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
using System.Threading;

namespace LB.MI.MI
{
    public partial class frmPurchaseBill : LBUIPageBase
    {
        public event GetCameraHandle GetCameraEvent;
        public event PrintOutBillHandle PrintOutBillEvent;
        public event PrintOutBillHandle PrintInBillEvent;
        private long mlSaleCarInBillID = 0;
        private System.Windows.Forms.Timer mTimer = null;
        public frmPurchaseBill()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 100;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;

            DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtCar = ExecuteSQL.CallView(113, "CarID,CarNum,SortLevel", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            DataTable dtInBill = ExecuteSQL.CallView(125, "SaleCarInBillID,SaleCarInBillCode", "BillStatus = 1 and IsCancel=0 and SaleCarOutBillID is not null and SaleBillType=1", "SaleCarInBillCode desc");
            //this.txtSelectSaleCarOutBillCode.TextBox.LBViewType = 113;
            this.txtSelectSaleCarInBillCode.TextBox.LBSort = "SaleCarInBillID asc";
            this.txtSelectSaleCarInBillCode.TextBox.IDColumnName = "SaleCarInBillID";
            this.txtSelectSaleCarInBillCode.TextBox.TextColumnName = "SaleCarInBillCode";
            this.txtSelectSaleCarInBillCode.TextBox.PopDataSource = dtInBill.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtSelectSaleCarInBillCode.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;

            this.txtSelectSaleCarInBillCode.TextBox.TextChanged += SelectSaleCarOutBill_TextChanged;
            LoadReturnBill();
            NoneStatus();

            this.txtCarID.TextBox.ReadOnly = false;
            this.txtItemID.TextBox.ReadOnly = false;
            this.txtCustomerID.TextBox.ReadOnly = false;
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.pnlSteadyStatus.Invalidate();
                this.lblWeight.Text = LBSerialHelper.WeightValue.ToString();
                this.pnlCarHeader.BackColor = LBInFrareHelper.HeaderClosed ? Color.Green : Color.Red;
                this.pnlCarTail.BackColor = LBInFrareHelper.TailClosed ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {

            }
        }

        private void SelectSaleCarOutBill_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NoneStatus();//全部不允许编辑
                string strInBillCode = this.txtSelectSaleCarInBillCode.TextBox.Text;
                if (strInBillCode!="")//如果存在该车辆
                {
                    #region -- 清空界面信息 --

                    this.txtItemID.TextBox.Text = "";
                    this.txtCustomerID.TextBox.Text = "";
                    this.txtCarID.TextBox.Text = "";
                    this.txtSaleCarInBillCode.Text = "";
                    this.txtSaleCarOutBillCode.Text = "";
                    
                    this.txtTotalWeight.Text = "";
                    this.txtCarTare.Text = "";
                    this.txtSuttleWeight.Text = "";
                    this.txtInWeight.Text = "";
                    this.txtOutWeight.Text = "";

                    NoneStatus();

                    #endregion

                    #region -- 判断是否作废 --
                    DataTable dtInBill = ExecuteSQL.CallView(125, "", "SaleCarInBillCode='" + strInBillCode+"'", "");
                    if (dtInBill.Rows.Count > 0)
                    {
                        int iIsCancel = LBConverter.ToInt32(dtInBill.Rows[0]["IsCancel"]);
                        if(iIsCancel==1)
                            throw new Exception("该入场单已作废，无法重车出场！");

                        DataRow drInBill = dtInBill.Rows[0];
                        this.txtItemID.TextBox.SelectedItemID = drInBill["ItemID"];
                        this.txtCarID.TextBox.SelectedItemID = drInBill["CarID"];
                        this.txtCustomerID.TextBox.SelectedItemID = drInBill["CustomerID"];

                        this.txtTotalWeight.Text = drInBill["TotalWeight"].ToString();
                        this.txtSaleCarInBillCode.Text = drInBill["SaleCarInBillCode"].ToString();

                        InWeightStatus();
                    }
                    #endregion
                    
                }
                else
                {
                    #region -- 清空界面信息 --

                    this.txtItemID.TextBox.Text = "";
                    this.txtCustomerID.TextBox.Text = "";
                    this.txtCarID.TextBox.Text = "";
                    this.txtSaleCarInBillCode.Text = "";
                    this.txtSaleCarOutBillCode.Text = "";

                    this.txtTotalWeight.Text = "";
                    this.txtCarTare.Text = "";
                    this.txtSuttleWeight.Text = "";
                    this.txtInWeight.Text = "";
                    this.txtOutWeight.Text = "";

                    NoneStatus();

                    #endregion

                    this.txtCustomerID.Enabled = true;//出场时不能修改客户
                    this.txtItemID.Enabled = true;//出场时不能修改物料
                    this.txtCarID.Enabled = true;//出场时不能修改车辆
                    this.txtSaleCarInBillCode.Text = "";
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnInWeight_Click(object sender, EventArgs e)
        {
            try
            {
                //LBPermission.VerifyUserPermission("称重并保存", "WeightSalesReturnIn_Save");
                //先读取重量
                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常
                VerifyTextBoxIsEmpty();//判断相关控件值是否为空
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                string strSaleCarInBillCode = this.txtSaleCarInBillCode.Text.TrimEnd();

                if (lCarID == 0)
                    throw new Exception("车辆不能为空");
                if (lItemID == 0)
                    throw new Exception("货物不能为空");
                if (lCustomerID == 0)
                    throw new Exception("客户不能为空");

                long lSaleCarInBillID = 0;

                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读总重
                this.txtInWeight.Text = decWeight.ToString("0");

                decimal decInWeight = LBConverter.ToDecimal(this.txtInWeight.Text);

                if (decInWeight <= 0)
                {
                    throw new Exception("入场称重值不能为0");
                }

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, 0));
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
                parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
                parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int64, 0));
                parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
                parmCol.Add(new LBParameter("TotalWeight", enLBDbType.Decimal, decInWeight));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14119, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("SaleCarInBillID"))
                {
                    lSaleCarInBillID = LBConverter.ToInt64(dictValue["SaleCarInBillID"]);

                    if (lSaleCarInBillID > 0)
                    {
                        Thread threadSavePic = new Thread(SaveInSalesPicture);
                        threadSavePic.Start(lSaleCarInBillID);

                        if (PrintInBillEvent != null)
                        {
                            Billinfo billInfo = new Billinfo(lSaleCarInBillID);
                            PrintInBillEvent(this, billInfo);
                        }

                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("生成入场单成功！");
                        ClearAllBillInfo();
                        LoadReturnBill();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //判断地磅数值是否稳定以及红外线设备是否报警
        private void VerifyDeviceIsSteady()
        {
            if (!LBSerialHelper.IsSteady)
            {
                throw new Exception("地磅数值未稳定，无法保存！");
            }

            if (LBInFrareHelper.IsHeaderEffect && !LBInFrareHelper.HeaderClosed)
            {
                throw new Exception("车头触动红外线报警器，无法保存！");
            }
            if (LBInFrareHelper.IsTailEffect && !LBInFrareHelper.TailClosed)
            {
                throw new Exception("车尾触动红外线报警器，无法保存！");
            }
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
        }

        private void btnOutWeight_Click(object sender, EventArgs e)
        {
            try
            {
                //先读取重量
                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常
                VerifyTextBoxIsEmpty();//判断相关控件值是否为空

                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重

                this.txtOutWeight.Text = decWeight.ToString("0");
                txtCarTare.Text= decWeight.ToString("0");
                this.txtSuttleWeight.Text = (LBConverter.ToDecimal(txtTotalWeight.Text)- decWeight).ToString("0");

                OutWeightStatus();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSubmitReturn_Click(object sender, EventArgs e)
        {
            try
            {

                //LBPermission.VerifyUserPermission("确认退货", "WeightSalesReturnOut_Save");
                
                decimal decInWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
                decimal decOutWeight = LBConverter.ToDecimal(this.txtOutWeight.Text);
                if (decInWeight <= 0)
                {
                    throw new Exception("入场称重值不能为0");
                }

                if (decOutWeight <= 0)
                {
                    throw new Exception("出场场称重值不能为0");
                }

                string strSaleCarInBillCode = this.txtSaleCarInBillCode.Text.TrimEnd();
                if (strSaleCarInBillCode != "")
                {
                    long lSaleCarInBillID = 0;

                    #region -- 查询入场单信息 --

                    DataTable dtInBill = ExecuteSQL.CallView(125, "SaleCarInBillID,SaleCarOutBillID", "SaleCarInBillCode='" + strSaleCarInBillCode + "'", "");
                    if (dtInBill.Rows.Count > 0)
                    {
                        lSaleCarInBillID = LBConverter.ToInt64(dtInBill.Rows[0]["SaleCarInBillID"]);
                    }

                    #endregion

                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
                    parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decOutWeight));
                    parmCol.Add(new LBParameter("SuttleWeight", enLBDbType.Decimal, decInWeight - decOutWeight));
                    parmCol.Add(new LBParameter("Description", enLBDbType.String, ""));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14120, parmCol, out dsReturn, out dictValue);
                    if (dictValue.ContainsKey("SaleCarOutBillCode")&& dictValue.ContainsKey("SaleCarOutBillID"))
                    {
                        string strSaleCarOutBillCode = dictValue["SaleCarOutBillCode"].ToString();
                        long lSaleCarOutBillID = LBConverter.ToInt64(dictValue["SaleCarOutBillID"]);

                        if (strSaleCarOutBillCode != "")
                        {
                            Thread threadSavePic = new Thread(SaveOutSalesPicture);
                            threadSavePic.Start(lSaleCarOutBillID);
                            
                            ClearAllBillInfo();
                            if (PrintOutBillEvent != null)
                            {
                                Billinfo billInfo = new Billinfo(lSaleCarOutBillID);
                                PrintOutBillEvent(this, billInfo);
                            }
                        }
                    }
                    LoadReturnBill();
                }
                else
                {
                    throw new Exception("入场单号不能为空！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnClearValue_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllBillInfo();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //场内车辆信息清单
        private void LoadReturnBill()
        {
            DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleBillType=1 and BillStatus=1 and IsCancel=0", "SaleCarInBillID desc");
            this.grdMain.DataSource = dtBill.DefaultView;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewCell cell = this.grdMain[e.ColumnIndex, e.RowIndex];
                    DataRowView drvSelected = this.grdMain.Rows[cell.RowIndex].DataBoundItem as DataRowView;
                    
                    if (drvSelected["SaleCarInBillCode"].ToString()!="")
                    {
                        this.txtSelectSaleCarInBillCode.TextBox.Text = drvSelected["SaleCarInBillCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void ClearAllBillInfo()
        {
            this.txtSelectSaleCarInBillCode.TextBox.Text = "";
            this.txtItemID.TextBox.Text = "";
            this.txtCustomerID.TextBox.Text = "";
            this.txtCarID.TextBox.Text = "";

            this.txtSaleCarInBillCode.Text = "";
            this.txtSaleCarOutBillCode.Text = "";

            this.txtTotalWeight.Text = "";
            this.txtCarTare.Text = "";
            this.txtSuttleWeight.Text = "";
            this.txtInWeight.Text = "";
            this.txtOutWeight.Text = "";

            NoneStatus();
        }

        //全部不允许编辑
        private void NoneStatus()
        {
            this.btnInWeight.Enabled = true;
            this.btnOutWeight.Enabled = false;
            this.btnSubmitReturn.Enabled = false;
        }

        //入场状态
        private void InWeightStatus()
        {
            this.btnInWeight.Enabled = false;
            this.btnOutWeight.Enabled = true;
            this.btnSubmitReturn.Enabled = false;
        }
        //出场场状态
        private void OutWeightStatus()
        {
            this.btnInWeight.Enabled = false;
            this.btnOutWeight.Enabled = false;
            this.btnSubmitReturn.Enabled = true;
        }

        #region -- 保存录像图片 --

        private void SaveInSalesPicture(object objSaleCarReturnBillID)
        {
            long lSaleCarReturnBillID = LBConverter.ToInt64(objSaleCarReturnBillID);
            try
            {
                CameraInfo cameraInfo = new CameraInfo();
                if (GetCameraEvent != null)
                {
                    GetCameraEvent(this, cameraInfo);
                }

                byte[] bImg1 = cameraInfo.Image1;
                byte[] bImg2 = cameraInfo.Image2;
                byte[] bImg3 = cameraInfo.Image3;
                byte[] bImg4 = cameraInfo.Image4;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarReturnBillID", enLBDbType.Int64, lSaleCarReturnBillID));
                parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
                parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
                parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
                parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(30003, parmCol, out dsReturn, out dictValue);
            }
            catch (Exception ex)
            {
                LBErrorLog.InsertErrorLog("保存出场图片时报错，退货单号：" + lSaleCarReturnBillID.ToString() + "\n错误信息：" + ex.Message);
            }
        }

        private void SaveOutSalesPicture(object objSaleCarReturnBillID)
        {
            long lSaleCarReturnBillID = LBConverter.ToInt64(objSaleCarReturnBillID);
            try
            {
                CameraInfo cameraInfo = new CameraInfo();
                if (GetCameraEvent != null)
                {
                    GetCameraEvent(this, cameraInfo);
                }

                byte[] bImg1 = cameraInfo.Image1;
                byte[] bImg2 = cameraInfo.Image2;
                byte[] bImg3 = cameraInfo.Image3;
                byte[] bImg4 = cameraInfo.Image4;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarReturnBillID", enLBDbType.Int64, lSaleCarReturnBillID));
                parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
                parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
                parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
                parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(30004, parmCol, out dsReturn, out dictValue);
            }
            catch (Exception ex)
            {
                LBErrorLog.InsertErrorLog("保存出场图片时报错，退货单号：" + lSaleCarReturnBillID.ToString() + "\n错误信息：" + ex.Message);
            }
        }

        #endregion
    }
    
}
