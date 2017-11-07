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
    public delegate void GetCameraHandle(object sender, CameraInfo e);
    public delegate void PrintOutBillHandle(object sender, Billinfo e);
    public partial class frmSalesReturnBill : LBUIPageBase
    {
        public event GetCameraHandle GetCameraEvent;
        public event PrintOutBillHandle PrintOutBillEvent;
        private long mlSaleCarInBillID = 0;
        private System.Windows.Forms.Timer mTimer = null;
        public frmSalesReturnBill()
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

            DataTable dtOutBill = ExecuteSQL.CallView(125, "SaleCarOutBillID,SaleCarOutBillCode", "IsCancel=0 and SaleCarOutBillID is not null", "SaleCarOutBillID asc");
            //this.txtSelectSaleCarOutBillCode.TextBox.LBViewType = 113;
            this.txtSelectSaleCarOutBillCode.TextBox.LBSort = "SaleCarOutBillID asc";
            this.txtSelectSaleCarOutBillCode.TextBox.IDColumnName = "SaleCarOutBillID";
            this.txtSelectSaleCarOutBillCode.TextBox.TextColumnName = "SaleCarOutBillCode";
            this.txtSelectSaleCarOutBillCode.TextBox.PopDataSource = dtOutBill.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            DataTable dtReturnType= LB.Common.LBConst.GetConstData("ReturnType");//退货方式
            dtReturnType.Rows.InsertAt(dtReturnType.NewRow(),0);
            this.txtReturnType.DataSource = dtReturnType;
            this.txtReturnType.DisplayMember = "ConstText";
            this.txtReturnType.ValueMember = "ConstValue";

            DataTable dtReturnReason = LB.Common.LBConst.GetConstData("ReturnReason");//退货原因
            dtReturnReason.Rows.InsertAt(dtReturnReason.NewRow(), 0);
            this.txtReturnReason.DataSource = dtReturnReason;
            this.txtReturnReason.DisplayMember = "ConstText";
            this.txtReturnReason.ValueMember = "ConstValue";

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtSelectSaleCarOutBillCode.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;

            this.txtSelectSaleCarOutBillCode.TextBox.TextChanged += SelectSaleCarOutBill_TextChanged;
            LoadReturnBill();
            NoneStatus();

            this.txtCarID.TextBox.ReadOnly = true;
            this.txtItemID.TextBox.ReadOnly = true;
            this.txtCustomerID.TextBox.ReadOnly = true;
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
                long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSelectSaleCarOutBillCode.TextBox.SelectedItemID);
                if (lSaleCarOutBillID > 0)//如果存在该车辆
                {
                    #region -- 清空界面信息 --

                    this.txtItemID.TextBox.Text = "";
                    this.txtCustomerID.TextBox.Text = "";
                    this.txtCarID.TextBox.Text = "";
                    this.txtSaleCarInBillCode.Text = "";
                    this.txtSaleCarOutBillCode.Text = "";

                    this.txtReturnType.SelectedIndex = 0;

                    this.txtTotalWeight.Text = "";
                    this.txtCarTare.Text = "";
                    this.txtSuttleWeight.Text = "";
                    this.txtInWeight.Text = "";
                    this.txtOutWeight.Text = "";

                    NoneStatus();

                    #endregion

                    #region -- 判断是否作废 --
                    long lSaleCarInBillID=0;
                    DataTable dtInBill = ExecuteSQL.CallView(125, "SaleCarInBillID,IsCancel", "SaleCarOutBillID="+lSaleCarOutBillID, "");
                    if (dtInBill.Rows.Count > 0)
                    {
                        int iIsCancel = LBConverter.ToInt32(dtInBill.Rows[0]["IsCancel"]);
                        lSaleCarInBillID = LBConverter.ToInt64(dtInBill.Rows[0]["SaleCarInBillID"]);
                        if(iIsCancel==1)
                            throw new Exception("该入场单已作废，无法退货！");
                    }
                    #endregion

                    #region -- 判断该车辆是否正在走退货流程（即退货入场但未出场）

                    DataTable dtInReturnBill = ExecuteSQL.CallView(137, "", "ReturnStatus=0 and SaleCarInBillID=" + lSaleCarInBillID, "");
                    if (dtInReturnBill.Rows.Count > 0)
                    {
                        DataRow drReturnBill = dtInReturnBill.Rows[0];

                        DataTable dtBill = ExecuteSQL.CallView(125,
                            @"SaleCarInBillID,SaleCarOutBillID,SaleCarOutBillCode,SaleCarInBillCode,CarID, CarNum, ItemID,ItemName,
                            CustomerID, CustomerName,CarTare,BillDateOut,TotalWeight,SuttleWeight",
                        "SaleCarInBillID="+ lSaleCarInBillID.ToString(), "");

                        DataRow drLast = dtBill.Rows[0];
                        mlSaleCarInBillID = LBConverter.ToInt64(drLast["SaleCarInBillID"]);
                        this.txtCustomerID.TextBox.SelectedItemID = drLast["CustomerID"];
                        this.txtItemID.TextBox.SelectedItemID = drLast["ItemID"];
                        this.txtCarID.TextBox.SelectedItemID = drLast["CarID"];
                        this.txtCarTare.Text = drLast["CarTare"].ToString();
                        this.txtSaleCarInBillCode.Text = drLast["SaleCarInBillCode"].ToString();
                        this.txtSaleCarOutBillCode.Text = drLast["SaleCarOutBillCode"].ToString();
                        this.txtTotalWeight.Text = drLast["TotalWeight"].ToString();
                        this.txtSuttleWeight.Text = drLast["SuttleWeight"].ToString();

                        this.txtInWeight.Text = drReturnBill["TotalWeight"].ToString();

                        OutWeightStatus();//切换至出场状态
                        LoadReturnBill();
                    }
                    else
                    {
                        //读取该车辆最近的出场记录
                        DataTable dtLastOutBill = ExecuteSQL.CallView(125,
                            @"SaleCarInBillID,SaleCarOutBillID,SaleCarOutBillCode,SaleCarInBillCode,CarID, CarNum, ItemID,ItemName,
                            CustomerID, CustomerName,CarTare,BillDateOut,TotalWeight,SuttleWeight",
                        "IsCancel = 0 and SaleCarOutBillID is not null and BillStatus=2  and SaleCarOutBillID=" + lSaleCarOutBillID.ToString(), "SaleCarOutBillID desc");

                        if (dtLastOutBill.Rows.Count == 0)
                        {
                            throw new Exception("该出场单号未审核或者已作废，无法退货！");
                        }
                        else
                        {
                            DataRow drLast = dtLastOutBill.Rows[0];
                            mlSaleCarInBillID = LBConverter.ToInt64(drLast["SaleCarInBillID"]);
                            this.txtCustomerID.TextBox.SelectedItemID = drLast["CustomerID"];
                            this.txtItemID.TextBox.SelectedItemID = drLast["ItemID"];
                            this.txtCarID.TextBox.SelectedItemID = drLast["CarID"];
                            this.txtCarTare.Text = drLast["CarTare"].ToString();
                            this.txtSaleCarInBillCode.Text = drLast["SaleCarInBillCode"].ToString();
                            this.txtSaleCarOutBillCode.Text = drLast["SaleCarOutBillCode"].ToString();
                            this.txtTotalWeight.Text = drLast["TotalWeight"].ToString();
                            this.txtSuttleWeight.Text = drLast["SuttleWeight"].ToString();

                            InWeightStatus();//切换至入场状态
                            LoadReturnBill();
                        }
                    }

                    #endregion
                    
                }
                else
                {
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
                LBPermission.VerifyUserPermission("称重并保存", "WeightSalesReturnIn_Save");
                //先读取重量
                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常
                VerifyTextBoxIsEmpty();//判断相关控件值是否为空
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                string strSaleCarInBillCode = this.txtSaleCarInBillCode.Text.TrimEnd();

                if (strSaleCarInBillCode!="")
                {
                    long lSaleCarInBillID=0;

                    #region -- 判断是否存在未出场记录 --

                    DataTable dtInBill = ExecuteSQL.CallView(125, "SaleCarInBillID,SaleCarOutBillID", "SaleCarInBillCode='" + strSaleCarInBillCode + "'", "");
                    if (dtInBill.Rows.Count > 0)
                    {
                        lSaleCarInBillID = LBConverter.ToInt64(dtInBill.Rows[0]["SaleCarInBillID"]);
                        long lSaleCarOutBillID = LBConverter.ToInt64(dtInBill.Rows[0]["SaleCarOutBillID"]);
                        if (lSaleCarOutBillID == 0)
                        {
                            throw new Exception("该车辆存在入场而未出场记录，无法退货！");
                        }
                    }
                    
                    #endregion

                    #region -- 判断该车辆是否正在走退货流程（即退货入场但未出场）

                    DataTable dtInReturnBill = ExecuteSQL.CallView(137, "*", "ReturnStatus=0 and SaleCarInBillID=" + lSaleCarInBillID.ToString(), "");
                    if (dtInReturnBill.Rows.Count > 0)
                    {
                        throw new Exception("该车辆正在场内退货，无法进行入场称重！");
                    }

                    #endregion

                    decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                    this.txtInWeight.Text = decWeight.ToString("0");

                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认退货？","提示", MessageBoxButtons.YesNo) == 
                        DialogResult.Yes)
                    {
                        decimal decInWeight = LBConverter.ToDecimal(this.txtInWeight.Text);

                        if (decInWeight <= 0)
                        {
                            throw new Exception("入场称重值不能为0");
                        }

                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarReturnBillID", enLBDbType.Int64, 0));
                        parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
                        parmCol.Add(new LBParameter("TotalWeight", enLBDbType.Decimal, decInWeight));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(30000, parmCol, out dsReturn, out dictValue);
                        if (dictValue.ContainsKey("SaleCarReturnBillID"))
                        {
                            long SaleCarReturnBillID = LBConverter.ToInt64(dictValue["SaleCarReturnBillID"]);

                            if (SaleCarReturnBillID > 0)
                            {
                                Thread threadSavePic = new Thread(SaveInSalesPicture);
                                threadSavePic.Start(SaleCarReturnBillID);

                                LB.WinFunction.LBCommonHelper.ShowCommonMessage("生成入场退货单成功！");
                                ClearAllBillInfo();
                                LoadReturnBill();
                            }
                        }
                    }
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

                LBPermission.VerifyUserPermission("确认退货", "WeightSalesReturnOut_Save");

                decimal decTareCar = LBConverter.ToDecimal(this.txtCarTare.Text);//原皮重值

                decimal decInWeight = LBConverter.ToDecimal(this.txtInWeight.Text);
                decimal decOutWeight = LBConverter.ToDecimal(this.txtOutWeight.Text);
                int iReturnType = LBConverter.ToInt32(this.txtReturnType.SelectedValue);
                int iReturnReason = LBConverter.ToInt32(this.txtReturnReason.SelectedValue);
                if (decInWeight <= 0)
                {
                    throw new Exception("入场称重值不能为0");
                }

                if (decOutWeight <= 0)
                {
                    throw new Exception("出场场称重值不能为0");
                }

                if (this.txtReturnType.SelectedValue == null||
                    this.txtReturnType.SelectedValue.ToString()=="")
                {
                    throw new Exception("请选择退货方式");
                }
                if (this.txtReturnReason.SelectedValue == null ||
                    this.txtReturnReason.SelectedValue.ToString() == "")
                {
                    throw new Exception("请选择退货原因");
                }

                if (iReturnType == 0)//完全退货状态下必须校验原来车辆入场皮重与现在出场皮重是否存在较大磅差
                {
                    if (Math.Abs(decTareCar - decOutWeight) > 1000)
                    {
                        throw new Exception("该车辆记录的入场皮重与空车出场重量的磅差太大，无法出场，请检查该车辆是否已卸载完毕！");
                    }
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

                    #region -- 判断该车辆是否正在走退货流程（即退货入场但未出场）
                    long lSaleCarReturnBillID = 0;
                    DataTable dtInReturnBill = ExecuteSQL.CallView(137, "", "ReturnStatus=0 and SaleCarInBillID=" + lSaleCarInBillID.ToString(), "");
                    if (dtInReturnBill.Rows.Count == 0)
                    {
                        throw new Exception("该车辆没有退货入场记录！");
                    }
                    else
                    {
                        lSaleCarReturnBillID = LBConverter.ToInt64(dtInReturnBill.Rows[0]["SaleCarReturnBillID"]);
                    }
                    #endregion

                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认退货？", "提示", MessageBoxButtons.YesNo) ==
                        DialogResult.Yes)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarReturnBillID", enLBDbType.Int64, lSaleCarReturnBillID));
                        parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decOutWeight));
                        parmCol.Add(new LBParameter("SuttleWeight", enLBDbType.Decimal, decInWeight - decOutWeight));
                        parmCol.Add(new LBParameter("ReturnType", enLBDbType.Int16, iReturnType));
                        parmCol.Add(new LBParameter("ReturnReason", enLBDbType.Int16, iReturnReason));
                        parmCol.Add(new LBParameter("Description", enLBDbType.String, ""));
                        parmCol.Add(new LBParameter("NewSaleCarInBillCode", enLBDbType.String, ""));
                        parmCol.Add(new LBParameter("NewSaleCarOutBillCode", enLBDbType.String, ""));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(30001, parmCol, out dsReturn, out dictValue);
                        if (dictValue.ContainsKey("NewSaleCarOutBillCode"))
                        {
                            string strNewSaleCarOutBillCode = dictValue["NewSaleCarOutBillCode"].ToString();

                            if (strNewSaleCarOutBillCode != "")
                            {
                                Thread threadSavePic = new Thread(SaveOutSalesPicture);
                                threadSavePic.Start(lSaleCarReturnBillID);

                                LB.WinFunction.LBCommonHelper.ShowCommonMessage("退货完成！原磅单已作废，生成新的磅单为："+ strNewSaleCarOutBillCode);
                                ClearAllBillInfo();
                                if (dictValue.ContainsKey("NewSaleCarOutBillID"))
                                {
                                    long lNewSaleCarOutBillID = LBConverter.ToInt64(dictValue["NewSaleCarOutBillID"]);
                                    if (lNewSaleCarOutBillID > 0)
                                    {
                                        if (PrintOutBillEvent != null)
                                        {
                                            Billinfo billInfo = new Billinfo(lNewSaleCarOutBillID);
                                            PrintOutBillEvent(this, billInfo);
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (iReturnType == 0)//完全退货
                        {
                            LB.WinFunction.LBCommonHelper.ShowCommonMessage("完全退货完成，原磅单已作废！");
                            ClearAllBillInfo();
                        }
                        LoadReturnBill();
                    }
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
            DataTable dtBill = ExecuteSQL.CallView(137, "", "ReturnStatus=0", "SaleCarReturnBillID asc");
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

                    long lSaleCarReturnBillID = LBConverter.ToInt64(drvSelected["SaleCarReturnBillID"]);
                    if (lSaleCarReturnBillID > 0 )
                    {
                        this.txtSelectSaleCarOutBillCode.TextBox.Text = drvSelected["SaleCarOutBillCode"].ToString();
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
            this.txtSelectSaleCarOutBillCode.TextBox.Text = "";
            this.txtItemID.TextBox.Text = "";
            this.txtCustomerID.TextBox.Text = "";
            this.txtCarID.TextBox.Text = "";

            this.txtSaleCarInBillCode.Text = "";
            this.txtSaleCarOutBillCode.Text = "";
            this.txtReturnType.SelectedIndex = 0;

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
            this.btnInWeight.Enabled = false;
            this.btnOutWeight.Enabled = false;
            this.btnSubmitReturn.Enabled = false;
            this.txtReturnType.Enabled = false;
        }

        //退货入场状态
        private void InWeightStatus()
        {
            this.btnInWeight.Enabled = true;
            this.btnOutWeight.Enabled = false;
            this.btnSubmitReturn.Enabled = false;
            this.txtReturnType.Enabled = false;
        }
        //退货出场场状态
        private void OutWeightStatus()
        {
            this.btnInWeight.Enabled = false;
            this.btnOutWeight.Enabled = true;
            this.btnSubmitReturn.Enabled = true;
            this.txtReturnType.Enabled = true;
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

    public class CameraInfo
    {
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }

        //public CameraInfo(byte[] Image1, byte[] Image2,byte[] Image3,byte[] Image4)
        //{
        //    this.Image1 = Image1;
        //    this.Image2 = Image2;
        //    this.Image3 = Image3;
        //    this.Image4 = Image4;
        //}
    }

    public class Billinfo
    {
        public long NewSaleCarOutBillID;
        public Billinfo(long lNewSaleCarOutBillID)
        {
            NewSaleCarOutBillID = lNewSaleCarOutBillID;
        }
    }
}
