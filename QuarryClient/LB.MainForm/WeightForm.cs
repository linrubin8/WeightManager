using LB.Common;
using LB.Common.Camera;
using LB.Controls;
using LB.Controls.Args;
using LB.Controls.Report;
using LB.Login;
using LB.MI.MI;
using LB.Page.Helper;
using LB.SysConfig.SysConfig;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.MainForm
{
    public partial class WeightForm : LBForm
    {
        
        private System.Windows.Forms.Timer mTimer =null;
        private System.Windows.Forms.Timer mTimerCamera = null;
        public bool bolIsCancel = false;
        LB.MainForm.CtlBaseInfoSelection _ctlBaseInfo = null;
        public bool _OpenCamera = false;
        public enWeightType _WeightType;
        LB.MainForm.frmAutoPrint frmPrint = null;//打印等待界面

        private ViewCamera viewCamera1 = null;
        private ViewCamera viewCamera2 = null;
        private ViewCamera viewCamera3 = null;
        private ViewCamera viewCamera4 = null;
        private bool _Cameria1IsOpen = false;//摄像头1是否启动
        private bool _Cameria2IsOpen = false;//摄像头2是否启动
        private bool _Cameria3IsOpen = false;//摄像头3是否启动
        private bool _Cameria4IsOpen = false;//摄像头4是否启动
        private DataTable _DTCamera = null;

        public WeightForm()
        {
            InitializeComponent();
            InitCamera();
            this.AutoSize = false;
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 100;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;

            mTimerCamera = new System.Windows.Forms.Timer();
            mTimerCamera.Interval = 100;
            mTimerCamera.Enabled = true;
            mTimerCamera.Tick += MTimerCamera_Tick;
            
            viewCamera1.MouseDoubleClick += ViewCamera_MouseDoubleClick;
            viewCamera2.MouseDoubleClick += ViewCamera_MouseDoubleClick;
            viewCamera3.MouseDoubleClick += ViewCamera_MouseDoubleClick;
            viewCamera4.MouseDoubleClick += ViewCamera_MouseDoubleClick;

            this.gbCamera.SizeChanged += GbCamera_SizeChanged;
        }

        private void GbCamera_SizeChanged(object sender, EventArgs e)
        {
            this.pnlCamera1.Height = this.pnlCamera2.Height = 
                this.pnlCamera3.Height = this.pnlCamera4.Height = this.gbCamera.Height / 4;
        }

        private void InitCamera()
        {
            viewCamera1 = new ViewCamera();
            this.viewCamera1.Account = "";
            this.viewCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCamera1.IPAddress = "";
            this.viewCamera1.Name = "viewCamera1";
            this.viewCamera1.Password = "";
            this.viewCamera1.Port = 0;
            frmShowMaxCameral frmCamera1 = new frmShowMaxCameral(viewCamera1);
            frmCamera1.TopLevel = false; // 不是最顶层窗体
            frmCamera1.ControlBox = false;
            frmCamera1.FormBorderStyle = FormBorderStyle.None;
            frmCamera1.Dock = DockStyle.Fill;
            this.pnlCamera1.Controls.Add(frmCamera1);
            frmCamera1.Show();

            viewCamera2 = new ViewCamera();
            this.viewCamera2.Account = "";
            this.viewCamera2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewCamera2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCamera2.IPAddress = "";
            this.viewCamera2.Name = "viewCamera2";
            this.viewCamera2.Password = "";
            this.viewCamera2.Port = 0;
            frmShowMaxCameral frmCamera2 = new frmShowMaxCameral(viewCamera2);
            frmCamera2.TopLevel = false; // 不是最顶层窗体
            frmCamera2.ControlBox = false;
            frmCamera2.FormBorderStyle = FormBorderStyle.None;
            frmCamera2.Dock = DockStyle.Fill;
            this.pnlCamera2.Controls.Add(frmCamera2);
            frmCamera2.Show();

            viewCamera3 = new ViewCamera();
            this.viewCamera3.Account = "";
            this.viewCamera3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewCamera3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCamera3.IPAddress = "";
            this.viewCamera3.Name = "viewCamera3";
            this.viewCamera3.Password = "";
            this.viewCamera3.Port = 0;
            frmShowMaxCameral frmCamera3 = new frmShowMaxCameral(viewCamera3);
            frmCamera3.TopLevel = false; // 不是最顶层窗体
            frmCamera3.ControlBox = false;
            frmCamera3.FormBorderStyle = FormBorderStyle.None;
            frmCamera3.Dock = DockStyle.Fill;
            this.pnlCamera3.Controls.Add(frmCamera3);
            frmCamera3.Show();

            viewCamera4 = new ViewCamera();
            this.viewCamera4.Account = "";
            this.viewCamera4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewCamera4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCamera4.IPAddress = "";
            this.viewCamera4.Name = "viewCamera4";
            this.viewCamera4.Password = "";
            this.viewCamera4.Port = 0;
            frmShowMaxCameral frmCamera4 = new frmShowMaxCameral(viewCamera4);
            frmCamera4.TopLevel = false; // 不是最顶层窗体
            frmCamera4.ControlBox = false;
            frmCamera4.FormBorderStyle = FormBorderStyle.None;
            frmCamera4.Dock = DockStyle.Fill;
            this.pnlCamera4.Controls.Add(frmCamera4);
            frmCamera4.Show();
        }

        private void ViewCamera_MouseDoubleClick(object sender, EventArgs e)
        {
            ViewCamera vc = (ViewCamera)sender;
            Panel pnl = null;
            if (vc == viewCamera1)
                pnl = pnlCamera1;
            else if (vc == viewCamera2)
                pnl = pnlCamera2;
            else if (vc == viewCamera3)
                pnl = pnlCamera3;
            else if (vc == viewCamera4)
                pnl = pnlCamera4;
            try
            {
                vc.MouseDoubleClick -= ViewCamera_MouseDoubleClick;
                vc.MouseClick += Vc_MouseClick;
                Form frm = vc.FindForm();
                frm.Hide();
                pnl.Controls.Remove(frm);
                frm.WindowState = FormWindowState.Maximized;
                frm.TopLevel = true;
                frm.TopMost = true;
                frm.Show();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
            finally
            {
                vc.MouseDoubleClick += ViewCamera_MouseDoubleClick;
            }
        }

        private void Vc_MouseClick(object sender, MouseEventArgs e)
        {
            ViewCamera vc = (ViewCamera)sender;
            vc.MouseClick -= Vc_MouseClick;

            Panel pnl = null;
            if (vc == viewCamera1)
                pnl = pnlCamera1;
            else if (vc == viewCamera2)
                pnl = pnlCamera2;
            else if (vc == viewCamera3)
                pnl = pnlCamera3;
            else if (vc == viewCamera4)
                pnl = pnlCamera4;

            Form frm = vc.FindForm();
            frm.Hide();

            frm.WindowState = FormWindowState.Normal;
            frm.TopLevel = false; // 不是最顶层窗体
            frm.ControlBox = false;
            frm.Dock = DockStyle.Fill;
            frm.TopMost = false;
            pnl.Controls.Add(frm);
            frm.Show();
        }

        private void MTimerCamera_Tick(object sender, EventArgs e)
        {
            try
            {
                mTimerCamera.Interval = 30000;
                if (_DTCamera == null)
                {
                    _DTCamera = ExecuteSQL.CallView(122, "", "MachineName='" + LoginInfo.MachineName + "'", "");
                }
                if (_DTCamera.Rows.Count > 0)
                {
                    DataRow dr = _DTCamera.Rows[0];
                    if (!_Cameria1IsOpen)
                    {
                        string strIPAddress1 = dr["IPAddress1"].ToString().TrimEnd();
                        int iPort1 = LBConverter.ToInt32(dr["Port1"]);
                        PingCamera ping = new PingCamera();
                        bool bolConnected = ping.Connect(strIPAddress1, iPort1, 200);
                        if (bolConnected)
                        {
                            _Cameria1IsOpen = true;
                            viewCamera1.IPAddress = dr["IPAddress1"].ToString().TrimEnd();
                            viewCamera1.Port = LBConverter.ToInt32(dr["Port1"]);
                            viewCamera1.Account = dr["Account1"].ToString().TrimEnd();
                            viewCamera1.Password = dr["Password1"].ToString().TrimEnd();
                            viewCamera1.OpenCamera(1);
                        }
                    }
                    if (!_Cameria2IsOpen)
                    {
                        string strIPAddress = dr["IPAddress2"].ToString().TrimEnd();
                        int iPort = LBConverter.ToInt32(dr["Port2"]);
                        PingCamera ping = new PingCamera();
                        bool bolConnected = ping.Connect(strIPAddress, iPort, 200);
                        if (bolConnected)
                        {
                            _Cameria2IsOpen = true;
                            viewCamera2.IPAddress = dr["IPAddress2"].ToString().TrimEnd();
                            viewCamera2.Port = LBConverter.ToInt32(dr["Port2"]);
                            viewCamera2.Account = dr["Account2"].ToString().TrimEnd();
                            viewCamera2.Password = dr["Password2"].ToString().TrimEnd();
                            viewCamera2.OpenCamera(2);
                        }
                    }
                    if (!_Cameria3IsOpen)
                    {
                        string strIPAddress = dr["IPAddress3"].ToString().TrimEnd();
                        int iPort = LBConverter.ToInt32(dr["Port3"]);
                        PingCamera ping = new PingCamera();
                        bool bolConnected = ping.Connect(strIPAddress, iPort, 200);
                        if (bolConnected)
                        {
                            _Cameria3IsOpen = true;
                            viewCamera3.IPAddress = dr["IPAddress3"].ToString().TrimEnd();
                            viewCamera3.Port = LBConverter.ToInt32(dr["Port3"]);
                            viewCamera3.Account = dr["Account3"].ToString().TrimEnd();
                            viewCamera3.Password = dr["Password3"].ToString().TrimEnd();
                            viewCamera3.OpenCamera(3);
                        }
                    }
                    if (!_Cameria4IsOpen)
                    {
                        string strIPAddress = dr["IPAddress4"].ToString().TrimEnd();
                        int iPort = LBConverter.ToInt32(dr["Port4"]);
                        PingCamera ping = new PingCamera();
                        bool bolConnected = ping.Connect(strIPAddress, iPort, 200);
                        if (bolConnected)
                        {
                            _Cameria4IsOpen = true;
                            viewCamera4.IPAddress = dr["IPAddress4"].ToString().TrimEnd();
                            viewCamera4.Port = LBConverter.ToInt32(dr["Port4"]);
                            viewCamera4.Account = dr["Account4"].ToString().TrimEnd();
                            viewCamera4.Password = dr["Password4"].ToString().TrimEnd();
                            viewCamera4.OpenCamera(4);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.lblWeight.Text = LBSerialHelper.WeightValue.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            LBSerialHelper.CloseSerial();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReportHelper.LBFinishReport += ReportHelper_LBFinishReport;

            ReadWeightType();//读取磅房类型

            InitData();
            LoadAllSalesBill();//磅单清单
            this.ctlSearcher1.SetGridView(this.grdMain, "CarNum");

            InitBaseControl();
            InitTextDataSource();

            //OpenCamera();//开启监控

            LBSerialHelper.StartSerial();//启动串口

            //System.Threading.Thread threadCamera1 = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart( OpenCamera));
            //threadCamera1.Start(1);

            //threadCamera1 = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenCamera));
            //threadCamera1.Start(2);

            //threadCamera1 = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenCamera));
            //threadCamera1.Start(3);

            //threadCamera1 = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenCamera));
            //threadCamera1.Start(4);

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;

            SetButtonReadOnlyByPermission();
        }

        #region -- 双击打开清单  --

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex>=0 && e.ColumnIndex >= 0)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(this.grdMain["SaleCarInBillID",e.RowIndex].Value);
                    if (lSaleCarInBillID > 0)
                    {
                        frmSaleCarInOutEdit frmEdit = new frmSaleCarInOutEdit(lSaleCarInBillID);
                        LBShowForm.ShowDialog(frmEdit);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 双击打开清单  --

        #region -- 读取磅房类型 -- 

        private void ReadWeightType()
        {
            int iSysSaleBillType;//0必须入场才能出场 1只生成出场磅单
            SysConfigValue.GetSysConfig("SysSaleBillType", out iSysSaleBillType);

            if (iSysSaleBillType == 0)
            {
                DataTable dtType = ExecuteSQL.CallView(126, "", "MachineName='" + LoginInfo.MachineName + "'", "");
                if (dtType.Rows.Count > 0)
                {
                    _WeightType = (enWeightType)LBConverter.ToInt32(dtType.Rows[0]["WeightType"]);
                }
            }
            else
            {
                _WeightType = enWeightType.WeightOnlyOut;
            }

            ChangeTextBoxStatusByType(_WeightType);
        }

        private void ChangeTextBoxStatusByType(enWeightType eWeightType)
        {
            if(eWeightType== enWeightType.WeightIn)//入场磅
            {
                this.txtItemID.TextBox.ReadOnly = false;
                this.txtCalculateType.Enabled = false;
                this.txtReceiveType.Enabled = false;

                this.lblWeightTypeName.Text = "磅单(入场)";
                this.btnSave.LBPermissionCode = "WeightSalesInBill_Save";
            }
            else if (eWeightType == enWeightType.WeightOut)//出场磅
            {
                this.txtItemID.TextBox.ReadOnly = true;
                this.txtCalculateType.Enabled = true;
                this.txtReceiveType.Enabled = true;
                this.lblWeightTypeName.Text = "磅单(出场)";
                this.btnSave.LBPermissionCode = "WeightSalesOutBill_Save";
            }
            else if (eWeightType == enWeightType.WeightOnlyOut)//只生成出场磅单
            {
                this.txtItemID.TextBox.ReadOnly = false;
                this.txtCalculateType.Enabled = true;
                this.txtReceiveType.Enabled = true;
                this.lblWeightTypeName.Text = "磅单(出场)";
                this.btnSave.LBPermissionCode = "WeightSalesOutBill_Save";
            }

            SetButtonReadOnlyByPermission();
        }

        #endregion

        #region -- Init TextBox DataSource --

        private void InitTextDataSource()
        {
            this.txtReceiveType.DataSource = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            this.txtCalculateType.DataSource = LB.Common.LBConst.GetConstData("CalculateType");//计价方式
            this.txtCalculateType.DisplayMember = "ConstText";
            this.txtCalculateType.ValueMember = "ConstValue";

            DataTable dtCustom = ExecuteSQL.CallView(110);
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtCar = ExecuteSQL.CallView(113);
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtCarID.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;

            this.txtCarID.TextBox.GotFocus += CoolText_GotFocus;
            this.txtItemID.TextBox.GotFocus += CoolText_GotFocus;
            this.txtCustomerID.TextBox.GotFocus += CoolText_GotFocus;

            this.txtCarID.TextBox.LostFocus += CoolText_LostFocus;
            this.txtItemID.TextBox.LostFocus += CoolText_LostFocus;
            this.txtCustomerID.TextBox.LostFocus += CoolText_LostFocus;

            this.txtCarID.TextBox.TextChanged += CoolText_TextChanged;
            this.txtItemID.TextBox.TextChanged += CoolText_TextChanged;
            this.txtCustomerID.TextBox.TextChanged += Customer_TextChanged;
            this.txtCalculateType.TextChanged += TxtCalculateType_TextChanged;

            this.txtTotalWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtSuttleWeight.TextChanged+= TxtCalAmount_TextChanged;
            this.txtPrice.TextChanged+= TxtCalAmount_TextChanged;
            this.txtCarTare.TextChanged += TxtCalAmount_TextChanged;
            
        }

        private void TxtCalculateType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //读取物料价格
                ReadPrice(sender);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void Customer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RereadBaseInfo(sender);
                //读取物料价格
                ReadPrice(sender);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void CoolText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtCustomerID.TextBox.TextChanged -= Customer_TextChanged;
                RereadBaseInfo(sender);

                if(sender == this.txtCarID.TextBox)
                {
                    if(_WeightType== enWeightType.WeightOut)//出场磅房
                    {
                        this.txtTotalWeight.Text = "0";

                        string strCarNum = this.txtCarID.TextBox.Text.ToString();
                        long lCarID = 0;
                        using (DataTable dtCar = ExecuteSQL.CallView(113, "CarID", "CarNum='" + strCarNum + "'", ""))
                        {
                            if (dtCar.Rows.Count > 0)
                            {
                                lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                            }
                        }
                        //读取入场数据
                        ReadCarInBill(lCarID);
                    }
                    else if (_WeightType == enWeightType.WeightIn)//入场磅房
                    {
                        string strCarNum = this.txtCarID.TextBox.Text.ToString();
                        long lCarID = 0;
                        long lCustomerID = 0;
                        using (DataTable dtCar = ExecuteSQL.CallView(117, "CarID,CustomerID", "CarNum='" + strCarNum + "'", ""))
                        {
                            if (dtCar.Rows.Count > 0)
                            {
                                lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                                lCustomerID = LBConverter.ToInt64(dtCar.Rows[0]["CustomerID"]);
                            }
                        }

                        if (lCustomerID > 0)
                        {
                            this.txtCustomerID.TextBox.SelectedItemID = lCustomerID;
                        }
                    }
                    else if (_WeightType == enWeightType.WeightOnlyOut)//只有出场磅单
                    {
                        string strCarNum = this.txtCarID.TextBox.Text.ToString();
                        long lCarID = 0;
                        long lCustomerID = 0;
                        using (DataTable dtCar = ExecuteSQL.CallView(117, "CarID,CustomerID", "CarNum='" + strCarNum + "'", ""))
                        {
                            if (dtCar.Rows.Count > 0)
                            {
                                lCarID = LBConverter.ToInt64(dtCar.Rows[0]["CarID"]);
                                lCustomerID = LBConverter.ToInt64(dtCar.Rows[0]["CustomerID"]);
                            }
                        }

                        if (lCustomerID > 0)
                        {
                            this.txtCustomerID.TextBox.SelectedItemID = lCustomerID;
                        }

                        //读取车辆最新皮重
                        DataTable dtWeight = ExecuteSQL.CallDirectSQL("select top 1 * from DbCarWeight where CarID=" + lCarID+" order by CreateTime desc");
                        if (dtWeight.Rows.Count > 0)
                        {
                            decimal decWeight = 0;
                            decimal.TryParse(dtWeight.Rows[0]["CarWeight"].ToString(), out decWeight);
                            this.txtCarTare.Text = decWeight.ToString("0");
                        }

                    }
                }
                //读取物料价格
                ReadPrice(sender);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
            finally
            {
                this.txtCustomerID.TextBox.TextChanged += Customer_TextChanged;
            }
        }

        private void RereadBaseInfo(object sender)
        {
            if (_ctlBaseInfo.BaseInfoType == enBaseInfoType.CarInfo && sender == txtCarID.TextBox)
            {
                string strFilter = "";
                string strValue = txtCarID.TextBox.Text.TrimEnd();
                if (strValue != "")
                {
                    strFilter = "CarNum like '%" + strValue + "%'";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
            else if (_ctlBaseInfo.BaseInfoType == enBaseInfoType.CarIn && sender == txtCarID.TextBox)
            {
                string strFilter = "";
                string strValue = txtCarID.TextBox.Text.TrimEnd();
                if (strValue != "")
                {
                    strFilter = "CarNum like '%" + strValue + "%'";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
            else if (_ctlBaseInfo.BaseInfoType == enBaseInfoType.CarOut && sender == txtCarID.TextBox)
            {
                string strFilter = "";
                string strValue = txtCarID.TextBox.Text.TrimEnd();
                if (strValue != "")
                {
                    strFilter = "CarNum like '%" + strValue + "%'";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
            else if (_ctlBaseInfo.BaseInfoType == enBaseInfoType.Item && sender == txtItemID.TextBox)
            {
                string strFilter = "";
                string strValue = txtItemID.TextBox.Text.TrimEnd();
                if (strValue != "")
                {
                    strFilter = "ItemName like '%" + strValue + "%'";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
            else if (_ctlBaseInfo.BaseInfoType == enBaseInfoType.Customer && sender == txtCustomerID.TextBox)
            {
                string strFilter = "";
                string strValue = txtCustomerID.TextBox.Text.TrimEnd();
                if (strValue != "")
                {
                    strFilter = "CustomerName like '%" + strValue + "%'";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
        }

        private void ReadPrice(object sender)
        {
            if (sender == this.txtCustomerID.TextBox || sender == this.txtCarID.TextBox || sender == this.txtItemID.TextBox || sender == this.txtCalculateType)
            {
                this.txtPrice.Text = "0";

                string strCarNum = this.txtCarID.TextBox.Text.ToString();
                string strItemName = this.txtItemID.TextBox.Text.ToString();
                string strCustomerName = this.txtCustomerID.TextBox.Text.ToString();

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

                using (DataTable dtCustomer = ExecuteSQL.CallView(112, "CustomerID", "CustomerName='" + strCustomerName + "'", ""))
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        lCustomerID = LBConverter.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                    }
                }


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
                        this.txtPrice.Text = LBConverter.ToDecimal(dictValue["Price"]).ToString("0.0");
                    }
                }
            }
        }

        private void CoolText_LostFocus(object sender, EventArgs e)
        {
            try
            {
                this._ctlBaseInfo.ChangeItemType(enBaseInfoType.None);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void CoolText_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if(sender == txtCarID.TextBox)
                {
                    if (_WeightType == enWeightType.WeightIn)
                    {
                        this._ctlBaseInfo.ChangeItemType(enBaseInfoType.CarIn);
                    }
                    else if (_WeightType == enWeightType.WeightOut)
                    {
                        this._ctlBaseInfo.ChangeItemType(enBaseInfoType.CarOut);
                    }
                    else if (_WeightType == enWeightType.WeightOnlyOut)
                    {
                        this._ctlBaseInfo.ChangeItemType(enBaseInfoType.CarInfo);
                    }
                    RereadBaseInfo(txtCarID.TextBox);
                }
                else if (sender == txtItemID.TextBox)
                {
                    this._ctlBaseInfo.ChangeItemType(enBaseInfoType.Item);
                    RereadBaseInfo(txtItemID.TextBox);
                }
                else if (sender == txtCustomerID.TextBox)
                {
                    this._ctlBaseInfo.ChangeItemType(enBaseInfoType.Customer);
                    RereadBaseInfo(txtCustomerID.TextBox);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion

        #region -- 初始化基础资料列表 --

        private void InitBaseControl()
        {
            _ctlBaseInfo = new LB.MainForm.CtlBaseInfoSelection();
            _ctlBaseInfo.Dock = DockStyle.Fill;
            this.pnlBaseSelect.Controls.Add(_ctlBaseInfo);

            _ctlBaseInfo.SelectedRowEvent += _ctlBaseInfo_SelectedRowEvent;
        }

        private void _ctlBaseInfo_SelectedRowEvent(Controls.Args.SelectedRowArgs e)
        {
            try
            {
                if(e.BaseInfoType== enBaseInfoType.CarIn)
                {
                    this.txtCarID.TextBox.SelectedItemID = e.SelectedRow["CarID"];
                }
                else if (e.BaseInfoType == enBaseInfoType.CarOut)
                {
                    this.txtCarID.TextBox.SelectedItemID = e.SelectedRow["CarID"];
                }
                else if (e.BaseInfoType == enBaseInfoType.Item)
                {
                    this.txtItemID.TextBox.SelectedItemID = e.SelectedRow["ItemID"];
                }
                else if (e.BaseInfoType == enBaseInfoType.Customer)
                {
                    this.txtCustomerID.TextBox.SelectedItemID = e.SelectedRow["CustomerID"];
                }
                else if (e.BaseInfoType == enBaseInfoType.CarInfo)
                {
                    this.txtCarID.TextBox.SelectedItemID = e.SelectedRow["CarID"];
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion

        #region -- InitData 读取全局数据 --

        private void InitData()
        {
            LBPermission.ReadAllPermission();//加载所有权限信息

            LBLog.AssemblyStart();

            this.grdMain.LBLoadConst();

            this.txtBillStatus.DataSource = LB.Common.LBConst.GetConstData("BillStatus");//单据状态
            this.txtBillStatus.DisplayMember = "ConstText";
            this.txtBillStatus.ValueMember = "ConstValue";
        }

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认退出系统？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            viewCamera1.CloseCamera();
            viewCamera2.CloseCamera();
            viewCamera3.CloseCamera();
            viewCamera4.CloseCamera();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region -- 按钮事件 --
        //皮重
        private void btnReadTareWeight_Click(object sender, EventArgs e)
        {
            try
            {
                if (_WeightType == enWeightType.WeightOut)
                {
                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("当前磅房设置为【出场】，是否确认读取皮重？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                this.txtCarTare.Text = decWeight.ToString("0");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //毛重
        private void btnTotalWeight_Click(object sender, EventArgs e)
        {
            try
            {
                if(_WeightType== enWeightType.WeightIn)
                {
                    if(LB.WinFunction.LBCommonHelper.ConfirmMessage("当前磅房设置为【入场】，是否确认读取毛重？","提示", MessageBoxButtons.OKCancel)!= DialogResult.OK)
                    {
                        return;
                    }
                }
                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读毛重
                this.txtTotalWeight.Text = decWeight.ToString("0");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_WeightType == enWeightType.WeightIn)
                {
                    SaveInBill();
                }
                else if (_WeightType == enWeightType.WeightOut|| _WeightType== enWeightType.WeightOnlyOut)
                {
                    SaveOutBill();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                LB.SysConfig.frmChangePassword frmChangePW = new SysConfig.frmChangePassword();
                frmChangePW.ShowDialog();
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
                bolIsCancel = true;
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeviceConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmWeightDecive frmDevice = new frmWeightDecive();
                LBShowForm.ShowDialog(frmDevice);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnCameraConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmCameraConfig frmDevice = new frmCameraConfig();
                LBShowForm.ShowDialog(frmDevice);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnRoomManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmWeightConfigType frm = new SysConfig.SysConfig.frmWeightConfigType();
                LBShowForm.ShowDialog(frm);
                if (frm.IsChangeWeightType)//入磅修改了磅房类型，则强制注销系统
                {
                    ReadWeightType();
                    //bolIsCancel = true;
                    //this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnWeightReportSet_Click(object sender, EventArgs e)
        {
            try
            {
                WeightReportConfig();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //补打小票
        private void btnRePrintInReport_Click(object sender, EventArgs e)
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

                if (this.grdMain.SelectedRows.Count == 0)
                {
                    throw new Exception("请选择需要补打的数据行！");
                }

                DataGridViewRow dgvr = this.grdMain.SelectedRows[0];
                DataRowView drv = dgvr.DataBoundItem as DataRowView;
                long lSaleCarInBillID = Convert.ToInt64(drv["SaleCarInBillID"]);

                DataTable dtIn = ExecuteSQL.CallView(123, "PrintCount", "SaleCarInBillID=" + lSaleCarInBillID, "");
                if (dtIn.Rows.Count > 0)
                {
                    int iInPrintCount = Convert.ToInt32(dtIn.Rows[0]["PrintCount"]);
                    if (iInPrintCount >= iAllowPrintInReportCount + 1)
                    {
                        throw new Exception("补打次数已超出系统设置的次数！");
                    }
                }
                PreviceReport(lSaleCarInBillID, enWeightType.WeightIn);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //补打磅单报表
        private void btnRePrintOutReport_Click(object sender, EventArgs e)
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

                if (this.grdMain.SelectedRows.Count == 0)
                {
                    throw new Exception("请选择需要补打的数据行！");
                }


                DataGridViewRow dgvr = this.grdMain.SelectedRows[0];
                DataRowView drv = dgvr.DataBoundItem as DataRowView;
                long lSaleCarOutBillID = Convert.ToInt64(drv["SaleCarOutBillID"]);

                DataTable dtOut = ExecuteSQL.CallView(124, "OutPrintCount", "SaleCarOutBillID="+ lSaleCarOutBillID, "");
                if (dtOut.Rows.Count > 0)
                {
                    int iOutPrintCount = Convert.ToInt32(dtOut.Rows[0]["OutPrintCount"]);
                    if(iOutPrintCount>= iAllowPrintOutReportCount + 1)
                    {
                        throw new Exception("补打次数已超出系统设置的次数！");
                    }
                }

                PreviceReport(lSaleCarOutBillID, enWeightType.WeightOut);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //查看皮重库
        private void btnCarTareManger_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarTareManager frmTare = new frmCarTareManager(this.txtCarID.TextBox.Text);
                LBShowForm.ShowDialog(frmTare);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //重新读取最新单价
        private void btnReadPrice_Click(object sender, EventArgs e)
        {
            try
            {
                ReadPrice(this.txtCarID);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        #endregion -- 按钮事件 --

        #region-- 保存入磅记录 --

        private void SaveInBill()
        {
            Dictionary<string, double> dictTest = new Dictionary<string, double>();
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);
            long lCarID= LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
            long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
            long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
            int iReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            
            if (decCarTare == 0)
            {
                throw new Exception("当前【皮重】值为0，无法保存！");
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
            parmCol.Add(new LBParameter("SaleCarInBillCode", enLBDbType.String, ""));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, DateTime.Now));
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));
            //parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
            //parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
            //parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
            //parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14100, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SaleCarInBillID"))
            {
                this.txtSaleCarInBillID.Text = dictValue["SaleCarInBillID"].ToString();
                Thread threadSavePic = new Thread(SaveInSalesPicture);
                threadSavePic.Start(dictValue["SaleCarInBillID"]);
            }
            if (dictValue.ContainsKey("SaleCarInBillCode"))
            {
                this.txtSaleCarInBillCode.Text = dictValue["SaleCarInBillCode"].ToString();
            }
            if (dictValue.ContainsKey("BillDate"))
            {
                this.txtBillDateIn.Text = dictValue["BillDate"].ToString();
            }
            
            dt2 = DateTime.Now;
            dictTest.Add("CallSP", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            LoadAllSalesBill(); //刷新磅单清单
            
            dt2 = DateTime.Now;
            dictTest.Add("LoadAllSalesBill", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            //打印磅单
            frmPrint = new frmAutoPrint();

            PreviceReport(lSaleCarInBillID, _WeightType);

            dt2 = DateTime.Now;
            dictTest.Add("PreviceReport", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            LBShowForm.ShowDialog(frmPrint);

            dt2 = DateTime.Now;
            dictTest.Add("ShowDialog", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            this.ClearAllBillInfo();
            dt2 = DateTime.Now;
            dictTest.Add("ClearAllBillInfo", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;
        }

        private void SaveInSalesPicture(object objSaleCarInBillID)
        {
            long lSaleCarInBillID = LBConverter.ToInt64(objSaleCarInBillID);
            byte[] bImg1 = null;
            byte[] bImg2 = null;
            byte[] bImg3 = null;
            byte[] bImg4 = null;

            bImg1 = viewCamera1.CapturePic();
            bImg2 = viewCamera2.CapturePic();
            bImg3 = viewCamera3.CapturePic();
            bImg4 = viewCamera4.CapturePic();

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
            parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
            parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
            parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
            parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14111, parmCol, out dsReturn, out dictValue);

        }

        #endregion-- 保存入磅记录 --

        #region-- 保存出磅记录 --

        private void SaveOutBill()
        {
            long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);

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

            byte[] bImg1 = null;
            byte[] bImg2 = null;
            byte[] bImg3 = null;
            byte[] bImg4 = null;

            try
            {
                bImg1 = viewCamera1.CapturePic();
                bImg2 = viewCamera2.CapturePic();
                bImg3 = viewCamera3.CapturePic();
                bImg4 = viewCamera4.CapturePic();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.ShowCommonMessage(ex.Message);
            }

            if (decTotalWeight == 0)
            {
                throw new Exception("当前【毛重】值为0，无法保存！");
            }
            if (decSuttleWeight == 0)
            {
                throw new Exception("当前【净重】值为0，无法保存！");
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("Price", enLBDbType.Decimal, decPrice));
            parmCol.Add(new LBParameter("Amount", enLBDbType.Decimal, decAmount));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("TotalWeight", enLBDbType.Decimal, decTotalWeight));
            parmCol.Add(new LBParameter("SuttleWeight", enLBDbType.Decimal, decSuttleWeight));
            parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
            parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
            parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
            parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));
            //入库单信息
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));

            int iSPType = 14102;
            if(_WeightType== enWeightType.WeightOnlyOut)
            {
                iSPType = 14112;
            }
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SaleCarOutBillID"))
            {
                this.txtSaleCarOutBillID.Text = dictValue["SaleCarOutBillID"].ToString();
            }
            if (dictValue.ContainsKey("BillDate"))
            {
                this.txtBillDateOut.Text = dictValue["BillDate"].ToString();
            }

            LoadAllSalesBill(); //刷新磅单清单

            //打印磅单
            frmPrint = new frmAutoPrint();
            long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSaleCarOutBillID.Text);
            PreviceReport(lSaleCarOutBillID,_WeightType);
            LBShowForm.ShowDialog(frmPrint);

            this.ClearAllBillInfo();
        }

        #endregion-- 保存出磅记录 --

        #region -- 查询磅单清单 --

        private void LoadAllSalesBill()
        {
            string strFilter = this.ctlSearcher1.GetFilter();
            if (strFilter != "")
            {
                strFilter += " and ";
            }
            DateTime dtBillDateFrom = Convert.ToDateTime(this.txtBillDateFrom.Text);
            DateTime dtBillDateTo = Convert.ToDateTime(this.txtBillDateTo.Text);
            strFilter += "(BillDateIn>='" + dtBillDateFrom.ToString("yyyy-MM-dd") + "' and BillDateIn<='" + dtBillDateTo.AddDays(1).ToString("yyyy-MM-dd") +"')";
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "");
            this.grdMain.DataSource = dtBill.DefaultView;
        }

        #endregion

        #region -- 自动开启监控 --

        private void OpenCamera(object CameraIndex)
        {
            int iCameraIndex = Convert.ToInt16(CameraIndex);
            DataTable dtCamera = ExecuteSQL.CallView(122, "", "MachineName='" + LoginInfo.MachineName + "'", "");
            if (dtCamera.Rows.Count > 0)
            {
                DataRow dr = dtCamera.Rows[0];

                if (iCameraIndex == 1)
                {
                    MethodInvoker func = delegate ()
                    {
                        viewCamera1.IPAddress = dr["IPAddress1"].ToString().TrimEnd();
                        viewCamera1.Port = LBConverter.ToInt32(dr["Port1"]);
                        viewCamera1.Account = dr["Account1"].ToString().TrimEnd();
                        viewCamera1.Password = dr["Password1"].ToString().TrimEnd();
                        viewCamera1.OpenCamera(1);
                    };

                    if (viewCamera1.InvokeRequired)
                    {
                        viewCamera1.BeginInvoke(func);
                    }
                    else
                    {
                        func();
                    }
                }
                else if (iCameraIndex == 2)
                {
                    MethodInvoker func2 = delegate ()
                    {
                        viewCamera2.IPAddress = dr["IPAddress2"].ToString().TrimEnd();
                        viewCamera2.Port = LBConverter.ToInt32(dr["Port2"]);
                        viewCamera2.Account = dr["Account2"].ToString().TrimEnd();
                        viewCamera2.Password = dr["Password2"].ToString().TrimEnd();
                        viewCamera2.OpenCamera(2);
                    };

                    if (viewCamera2.InvokeRequired)
                    {
                        viewCamera2.BeginInvoke(func2);
                    }
                    else
                    {
                        func2();
                    }
                }
                else if (iCameraIndex == 3)
                {
                    MethodInvoker func2 = delegate ()
                    {
                        viewCamera3.IPAddress = dr["IPAddress3"].ToString().TrimEnd();
                        viewCamera3.Port = LBConverter.ToInt32(dr["Port3"]);
                        viewCamera3.Account = dr["Account3"].ToString().TrimEnd();
                        viewCamera3.Password = dr["Password3"].ToString().TrimEnd();
                        viewCamera3.OpenCamera(3);
                    };

                    if (viewCamera3.InvokeRequired)
                    {
                        viewCamera3.BeginInvoke(func2);
                    }
                    else
                    {
                        func2();
                    }
                }
                else if (iCameraIndex == 4)
                {
                    MethodInvoker func2 = delegate ()
                    {
                        viewCamera4.IPAddress = dr["IPAddress4"].ToString().TrimEnd();
                        viewCamera4.Port = LBConverter.ToInt32(dr["Port4"]);
                        viewCamera4.Account = dr["Account4"].ToString().TrimEnd();
                        viewCamera4.Password = dr["Password4"].ToString().TrimEnd();
                        viewCamera4.OpenCamera(4);
                    };

                    if (viewCamera4.InvokeRequired)
                    {
                        viewCamera4.BeginInvoke(func2);
                    }
                    else
                    {
                        func2();
                    }
                }

                /*if (this.viewCamera1.InvokeRequired)//等待异步
                {
                    OpenViewCamera ovc = new OpenViewCamera(DoneViewCamera);
                    this.Invoke(ovc, viewCamera1,
                        dr["IPAddress1"].ToString().TrimEnd(),
                        LBConverter.ToInt32(dr["Port1"]),
                        dr["Account1"].ToString().TrimEnd(),
                        dr["Password1"].ToString().TrimEnd());
                }

                ovc = new OpenViewCamera(DoneViewCamera);
                this.Invoke(ovc, viewCamera2,
                    dr["IPAddress2"].ToString().TrimEnd(),
                    LBConverter.ToInt32(dr["Port2"]),
                    dr["Account2"].ToString().TrimEnd(),
                    dr["Password2"].ToString().TrimEnd());

                ovc = new OpenViewCamera(DoneViewCamera);
                this.Invoke(ovc, viewCamera3,
                    dr["IPAddress3"].ToString().TrimEnd(),
                    LBConverter.ToInt32(dr["Port3"]),
                    dr["Account3"].ToString().TrimEnd(),
                    dr["Password3"].ToString().TrimEnd());

                ovc = new OpenViewCamera(DoneViewCamera);
                this.Invoke(ovc, viewCamera4,
                    dr["IPAddress4"].ToString().TrimEnd(),
                    LBConverter.ToInt32(dr["Port4"]),
                    dr["Account4"].ToString().TrimEnd(),
                    dr["Password4"].ToString().TrimEnd());*/
            }
        }
        public delegate void OpenViewCamera(ViewCamera vc, string ip,int port,string account,string pw);
        public void DoneViewCamera(ViewCamera vc, string ip, int port, string account, string pw)
        {
            vc.IPAddress = ip;
            vc.Port = port;
            vc.Account = account;
            vc.Password = pw;

            vc.OpenCamera(5);
        }


        #endregion -- 自动开启监控 --

        #region -- TextBox 事件 计算金额 --

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
        #endregion -- TextBox 事件 --

        #region -- 计算 金额=净重*单价 --

        private void CalAmount()
        {
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);//0按重量计算 1按车计算
            if (_WeightType == enWeightType.WeightOut|| _WeightType == enWeightType.WeightOnlyOut)//出场时才需要计算净重和金额
            {
                this.txtSuttleWeight.Text = (decTotalWeight - decCarTare).ToString("0");
                decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
                if(iCalculateType==0)
                    this.txtAmount.Text = (decPrice * (decTotalWeight - decCarTare)).ToString("0.0");
                else
                    this.txtAmount.Text = decPrice.ToString("0.0");
            }
        }

        #endregion

        #region -- 读取最近入场但未出场的数据 --

        private void ReadCarInBill(long lCarID)
        {
            if (lCarID > 0)
            {
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14101, parmCol, out dsReturn, out dictValue);
                if(dictValue != null&& dictValue.Keys.Count > 0)
                {
                    bool IsReaded = false;
                    if (dictValue.ContainsKey("IsReaded"))
                    {
                        IsReaded = LBConverter.ToBoolean(dictValue["IsReaded"]);
                    }
                    
                    if (!IsReaded)
                    {
                        ClearInBillInfo();
                        throw new Exception("该车辆没有入场记录！");
                    }
                    else
                    {
                        if (dictValue.ContainsKey("BillDateIn"))
                        {
                            this.txtBillDateIn.Text = LBConverter.ToDateTime(dictValue["BillDateIn"]).ToString();
                        }
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
                        if (dictValue.ContainsKey("Description"))
                        {
                            this.txtDescription.Text = LBConverter.ToString(dictValue["Description"]);
                        }
                        if (dictValue.ContainsKey("ReceiveType"))
                        {
                            this.txtReceiveType.SelectedValue = LBConverter.ToInt32(dictValue["ReceiveType"]);
                        }
                        if (dictValue.ContainsKey("SaleCarInBillCode"))
                        {
                            this.txtSaleCarInBillCode.Text = LBConverter.ToString(dictValue["SaleCarInBillCode"]);
                        }
                        if (dictValue.ContainsKey("SaleCarInBillID"))
                        {
                            this.txtSaleCarInBillID.Text = LBConverter.ToString(dictValue["SaleCarInBillID"]);
                        }
                        if (dictValue.ContainsKey("BillStatus"))
                        {
                            this.txtBillStatus.SelectedValue = dictValue["BillStatus"];
                        }
                        //this.txtCalculateType.SelectedValue = drv["CalculateType"];
                        //this.txtItemID.TextBox.SelectedItemID = drv["ItemID"];
                        //this.txtCustomerID.TextBox.SelectedItemID = drv["CustomerID"];
                        //this.txtCarTare.Text = drv["CarTare"].ToString();
                        //this.txtDescription.Text= drv["Description"].ToString();
                        //this.txtReceiveType.SelectedValue = drv["ReceiveType"];
                        //this.txtSaleCarInBillCode.Text = drv["SaleCarInBillCode"].ToString().TrimEnd();
                        //this.txtSaleCarInBillID.Text= drv["SaleCarInBillID"].ToString().TrimEnd();
                        //this.txtBillStatus.SelectedValue = drv["BillStatus"];
                    }
                }
            }
            else
            {
                ClearInBillInfo();
            }
        }

        #endregion

        #region -- 出场磅单（当鼠标离开车牌控件时校验是否存在入场磅单，如果否则清空相关控件值）

        private void ClearInBillInfo()
        {
            this.txtTotalWeight.Text = "0";
            this.txtSaleCarInBillCode.Text = "";
            this.txtPrice.Text = "0";
            this.txtCarTare.Text = "0";
            this.txtItemID.TextBox.Text = "";
            this.txtDescription.Text = "";
            this.txtSaleCarInBillID.Text = "";
            this.txtSaleCarOutBillID.Text = "";
            this.txtBillDateIn.Text = "";
            this.txtBillDateOut.Text = "";
        }


        private void ClearAllBillInfo()
        {
            this.txtCarID.TextBox.Text = "";
            this.txtTotalWeight.Text = "0";
            this.txtSaleCarInBillCode.Text = "";
            this.txtPrice.Text = "0";
            this.txtAmount.Text = "0";
            this.txtCustomerID.TextBox.Text = "";
            this.txtCarTare.Text = "0";
            this.txtItemID.TextBox.Text = "";
            this.txtDescription.Text = "";
            this.txtSaleCarInBillID.Text = "";
            this.txtSaleCarOutBillID.Text = "";
            this.txtBillDateIn.Text = "";
            this.txtBillDateOut.Text = "";
        }

        #endregion

        #region -- 报表设计 --

        private void WeightReportConfig()
        {
            ReportRequestArgs args;
            if (_WeightType == enWeightType.WeightIn)
            {
                args = new ReportRequestArgs(0, 6, null, null);

                long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);
                DataTable dtBill = ExecuteSQL.CallView(128, "", "SaleCarInBillID=" + lSaleCarInBillID, "");
                //if (dtBill.Rows.Count > 0)
                //{
                //    DataRow drHeader = dtBill.Rows[0];
                //    args.RecordDR = drHeader;
                //}

                dtBill.TableName = "T006";
                DataSet dsSource = new DataSet("Report");
                dsSource.Tables.Add(dtBill);
                args.DSDataSource = dsSource;

                frmReport frm = new frmReport(args);
                LBShowForm.ShowDialog(frm);
            }
            else if (_WeightType == enWeightType.WeightOut|| _WeightType == enWeightType.WeightOnlyOut)
            {
                args = new ReportRequestArgs(0, 7, null, null);

                long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSaleCarOutBillID.Text);
                DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleCarOutBillID=" + lSaleCarOutBillID, "");
                dtBill.TableName = "T007";
                DataSet dsSource = new DataSet("Report");
                dsSource.Tables.Add(dtBill);
                args.DSDataSource = dsSource;

                frmReport frm = new frmReport(args);
                LBShowForm.ShowDialog(frm);
            }
        }

        #endregion

        #region -- 预览磅单 --

        private void PreviceReport(long saleID, enWeightType weightType)
        {
            ProcessStep.mdtStep = null;
               ReportRequestArgs args;
            if (weightType == enWeightType.WeightIn)
            {
                ProcessStep.AddStep("StartPreviceReport", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                DataTable dtReportTemp = ExecuteSQL.CallView(105, "", "ReportTypeID=6", "");
                ProcessStep.AddStep("CallView_105", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                if (dtReportTemp.Rows.Count > 0)
                {
                    DataRow drReport = dtReportTemp.Rows[0];
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);

                    args = new ReportRequestArgs(lReportTemplateID, 6, null, null);

                    //long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(128, "", "SaleCarInBillID=" + saleID, "");
                    dtBill.TableName = "T006";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ProcessStep.AddStep("CallView_128", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

                    ReportHelper.OpenReportDialog(enRequestReportActionType.DirectPrint, args);

                    ProcessStep.AddStep("OpenReportDialog_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

                    //记录小票打印次数
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, saleID));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14109, parmCol, out dsReturn, out dictValue);

                    ProcessStep.AddStep("CallSP_14109", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                    DataTable dtt = ProcessStep.mdtStep;
                }
            }
            else if (weightType == enWeightType.WeightOut || 
                    weightType == enWeightType.WeightOnlyOut)
            {
                DataTable dtReportTemp = ExecuteSQL.CallView(105, "", "ReportTypeID=7", "");
                if (dtReportTemp.Rows.Count > 0)
                {
                    DataRow drReport = dtReportTemp.Rows[0];
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);

                    args = new ReportRequestArgs(lReportTemplateID, 7, null, null);

                    //long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSaleCarOutBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleCarOutBillID=" + saleID, "");
                    dtBill.TableName = "T007";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ReportHelper.OpenReportDialog(enRequestReportActionType.DirectPrint, args);

                    //记录磅单打印次数
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("SaleCarOutBillID", enLBDbType.Int64, saleID));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14110, parmCol, out dsReturn, out dictValue);
                }
            }
        }

        //报表打印完毕后触发事件
        private void ReportHelper_LBFinishReport(object sender, EventArgs e)
        {
            try
            {
                if (frmPrint != null)
                {
                    frmPrint.IsFinish = true;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion

        private void lbSkinButton2_Click(object sender, EventArgs e)
        {

        }

        #region -- 根据权限设置按钮的只读属性 --

        private void SetButtonReadOnlyByPermission()
        {
            this.btnSave.Enabled = LBPermission.GetUserPermission(this.btnSave.LBPermissionCode);
            this.btnTotalWeight.Enabled = LBPermission.GetUserPermission(this.btnTotalWeight.LBPermissionCode);
            this.btnRePrintInReport.Enabled = LBPermission.GetUserPermission(this.btnRePrintInReport.LBPermissionCode);
            this.btnRePrintOutReport.Enabled = LBPermission.GetUserPermission(this.btnRePrintOutReport.LBPermissionCode);
            this.btnReadTareWeight.Enabled = LBPermission.GetUserPermission(this.btnReadTareWeight.LBPermissionCode);
            this.btnReadPrice.Enabled = LBPermission.GetUserPermission(this.btnReadPrice.LBPermissionCode);
            this.btnCarTareManger.Enabled = LBPermission.GetUserPermission(this.btnCarTareManger.LBPermissionCode);
        }

        #endregion -- 根据权限设置按钮的只读属性 --

        #region --充值 --
        private void btnAddReceive_Click(object sender, EventArgs e)
        {
            int lUserIDTemp = LoginInfo.UserID;
            int iUserType = LoginInfo.UserType;
            string strUserName = LoginInfo.LoginName;

            try
            {
                bool bolPermission = LBPermission.GetUserPermission("RPReceive_Add");//校验是否具有充值权限
                if (!bolPermission)
                {
                    if(LB.WinFunction.LBCommonHelper.ConfirmMessage("该用户没有充值权限，是否切换其他用户进行充值？","提示", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        frmLoginTemp frmLogin = new frmLoginTemp();
                        frmLogin.ShowDialog();
                        if (frmLogin.IsLogin)//用户登录成功
                        {
                            LBPermission.VerifyUserPermission("充值", "RPReceive_Add");
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                RPReceive.frmEditReceiveBill frm = new LB.RPReceive.frmEditReceiveBill(0);
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
            finally
            {
                LoginInfo.UserID = lUserIDTemp;
                LoginInfo.UserType = iUserType;
                LoginInfo.LoginName = strUserName;
                LoginInfo.IsVerifySuccess = true;
            }
        }

        #endregion --充值 --
    }
}
