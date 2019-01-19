using LB.Common;
using LB.Common.Camera;
using LB.Common.Synchronous;
using LB.Controls;
using LB.Controls.Report;
using LB.Login;
using LB.MainForm.MainForm;
using LB.MI;
using LB.MI.MI;
using LB.Page.Helper;
using LB.RPReceive;
using LB.RPReceive.RPReceive;
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
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.MainForm
{
    public partial class WeightForm2 : LBForm
    {
        private LB.MainForm.CtlBaseInfoSelection _ctlBaseInfo;
        public bool bolIsCancel = false;
        LB.MainForm.frmAutoPrint frmPrint = null;//打印等待界面
        private enWeightType _WeightType;
        private System.Windows.Forms.Timer mTimer = null;
        private System.Windows.Forms.Timer mAutoComputeTimer = null;
        private System.Windows.Forms.Timer mTimerCamera = null;
        private long mlSaleCarInBillID;
        private frmSalesReturnBill _frmReturnBill = null;
        private frmPurchaseBill _frmPurchaseBill = null;
        private long CientVersion_SaleBill = 0;
        private long ServerVersion_SaleBill = 0;
        private Thread mVersionThread = null;

        public WeightForm2()
        {
            InitializeComponent();
            this.pnlSteadyStatus.Paint += PnlSteadyStatus_Paint;
            this.pnlWebSteadyStatus.Paint += PnlWebSteadyStatus_Paint;
        }

        private void PnlSteadyStatus_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float fwidth = 20;
                
                Pen pen = new Pen(Brushes.Black);
                Brush brush = Brushes.Green;
                if (LBSerialHelper.IsSteady)
                {
                    brush = Brushes.Green;
                    this.lblSteady.Text = "稳定";
                    this.lblSteady.ForeColor = Color.Green;
                }
                else
                {
                    brush = Brushes.Red;
                    this.lblSteady.Text = "不稳定";
                    this.lblSteady.ForeColor = Color.Red;
                }
                
                e.Graphics.FillEllipse(brush, new RectangleF((pnlSteadyStatus.Width- fwidth)/2, (pnlSteadyStatus.Height - fwidth) / 2, fwidth, fwidth));
                e.Graphics.DrawEllipse(pen, new RectangleF((pnlSteadyStatus.Width - fwidth) / 2, (pnlSteadyStatus.Height - fwidth) / 2, fwidth, fwidth));
            }
            catch (Exception ex)
            {
            }
        }

        private void PnlWebSteadyStatus_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float fwidth = 20;

                Pen pen = new Pen(Brushes.Black);
                Brush brush = Brushes.Green;
                if (LBPortHelper.IsWebSteady)
                {
                    brush = Brushes.Green;
                    this.lblWebSteady.Text = "稳定";
                    this.lblWebSteady.ForeColor = Color.Green;
                }
                else
                {
                    brush = Brushes.Red;
                    this.lblWebSteady.Text = "不稳定";
                    this.lblWebSteady.ForeColor = Color.Red;
                }

                e.Graphics.FillEllipse(brush, new RectangleF((pnlWebSteadyStatus.Width - fwidth) / 2, (pnlWebSteadyStatus.Height - fwidth) / 2, fwidth, fwidth));
                e.Graphics.DrawEllipse(pen, new RectangleF((pnlWebSteadyStatus.Width - fwidth) / 2, (pnlWebSteadyStatus.Height - fwidth) / 2, fwidth, fwidth));
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblCustomerRemainAmount.Visible = label4.Visible = false;

            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 100;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;

            mAutoComputeTimer = new System.Windows.Forms.Timer();
            mAutoComputeTimer.Interval = 2000;
            mAutoComputeTimer.Enabled = true;
            mAutoComputeTimer.Tick += MAutoComputeTimer_Tick;

            InitBaseControl();

            InitTextDataSource();//初始化控件数据源

            LoadAllSalesBill();//磅单清单
            InitData();
            LBSerialHelper.StartSerial();//启动串口
            LBInFrareHelper.StartSerial();//红外线对射串口
            SessionHelper.StartTakeSession();//开启定时检测Session
            InitCameraPanel();
            SetButtonReadOnlyByPermission();

            this.grdMain.MouseClick += GrdMain_MouseClick;
            this.grdMain.CellMouseClick += GrdMain_CellMouseClick;
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;

            mTimerCamera = new System.Windows.Forms.Timer();
            mTimerCamera.Interval = 100;
            mTimerCamera.Enabled = true;
            mTimerCamera.Tick += MTimerCamera_Tick;

            mVersionThread = new Thread(VerifyRefleshVersion);
            mVersionThread.Start();

            if (!LBRegisterPermission.Permission_ModelSynchorToServer)
            {
                btnSynchornousData.Visible = false;
            }
            //LBSerialHelper.LBSerialDataEvent += LBSerialHelper_LBSerialDataEvent;
        }

        #region -- 校验刷新版本号 --

        private void VerifyRefleshVersion()
        {
            while (true)
            {
                try
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("ClientVersion", enLBDbType.Int64, CientVersion_SaleBill));

                    int iSPType = 14118;
                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                    if (dictValue.ContainsKey("ServerVersion"))
                    {
                        ServerVersion_SaleBill = LBConverter.ToInt64(dictValue["ServerVersion"].ToString());
                    }
                }
                catch (Exception ex)
                {

                }

                Thread.Sleep(2000);
            }
        }

        #endregion -- 校验刷新版本号 --

        #region -- InitData 读取全局数据 --

        private void InitData()
        {
            LBPermission.ReadAllPermission();//加载所有权限信息
            LBPortHelper.StartCheckConnect();//启动网络检测进程
            LBLog.AssemblyStart();

            this.grdMain.LBLoadConst();
        }

        #endregion

        private void GrdMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell cell = this.grdMain[e.ColumnIndex, e.RowIndex];
                    int cellX = grdMain.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X;
                    int cellY = grdMain.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Y;

                    DataGridViewRow dgvr = this.grdMain.Rows[e.RowIndex];
                    DataRowView drv = dgvr.DataBoundItem as DataRowView;
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"]);
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drv["IsCancel"]);

                    this.tsmCancel.Visible = !bolIsCancel&& lSaleCarOutBillID==0;
                    this.tsmUnCancel.Visible = bolIsCancel;
                    this.tsmPreviewOutBill.Visible = lSaleCarOutBillID > 0;
                    this.tsmRePrintOutBill.Visible = lSaleCarOutBillID > 0;

                    contextMenuStrip1.Show(this.grdMain, new Point(cellX+e.X, cellY+e.Y));
                    contextMenuStrip1.Tag = this.grdMain.Rows[e.RowIndex].DataBoundItem;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewCell cell = this.grdMain[e.ColumnIndex, e.RowIndex];
                    DataRowView drvSelected = this.grdMain.Rows[cell.RowIndex].DataBoundItem as DataRowView;

                    long lSaleCarInBillID = LBConverter.ToInt64(drvSelected["SaleCarInBillID"]) ;
                    long lSaleCarOutBillID = LBConverter.ToInt64(drvSelected["SaleCarOutBillID"]);
                    if (lSaleCarInBillID > 0 && lSaleCarOutBillID == 0)
                    {
                        this.txtCarID.TextBox.ReadDataSource();//先刷新数据源,当新增车辆时，可能未及时刷新
                        long lCarID = LBConverter.ToInt64(drvSelected["CarID"]);
                        this.txtCarID.TextBox.Text = drvSelected["CarNum"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdMain_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.pnlSteadyStatus.Invalidate();
                this.pnlWebSteadyStatus.Invalidate();
                this.lblWeight.Text = LBSerialHelper.WeightValue.ToString();
                this.pnlCarHeader.BackColor = LBInFrareHelper.HeaderClosed ? Color.Green : Color.Red;
                this.pnlCarTail.BackColor = LBInFrareHelper.TailClosed ? Color.Green : Color.Red;

                //自动刷新(网络稳定状态下才自动刷新)
                if (LBPortHelper.IsWebSteady)
                {
                    if (CientVersion_SaleBill != ServerVersion_SaleBill)
                    {
                        Thread thread = new Thread(LoadAllSalesBill);
                        thread.Start();
                        //LoadAllSalesBill();
                        CientVersion_SaleBill = ServerVersion_SaleBill;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region -- 查询磅单清单 --

        private void LoadAllSalesBill()
        {
            string strFilter = "";
            string strSort = "";

            int FilterTypeSelectedIndex = 0;
            if (this.cbFilterType.InvokeRequired)
            {
                this.cbFilterType.Invoke((MethodInvoker)delegate {
                    FilterTypeSelectedIndex = this.cbFilterType.SelectedIndex;
                });
            }
            else
            {
                FilterTypeSelectedIndex = this.cbFilterType.SelectedIndex;
            }

            if (FilterTypeSelectedIndex == 0)
            {
                strFilter = "SaleCarOutBillID is null and isnull(IsCancel,0) = 0";
                if (this.grdMain.InvokeRequired)
                {
                    this.grdMain.Invoke((MethodInvoker)delegate {
                        this.grdMain.Columns["SaleCarOutBillCode"].Visible = false;
                        this.grdMain.Columns["SaleCarInBillCode"].Visible = true;
                    });
                }
                else
                {
                    this.grdMain.Columns["SaleCarOutBillCode"].Visible = false;
                    this.grdMain.Columns["SaleCarInBillCode"].Visible = true;
                }
                
                strSort = "SaleCarInBillCode asc";
            }
            else if (FilterTypeSelectedIndex == 1)
            {
                strFilter = "SaleCarOutBillID is not null and isnull(IsCancel,0) = 0 and BillDateOut>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                if (this.grdMain.InvokeRequired)
                {
                    this.grdMain.Invoke((MethodInvoker)delegate {
                        this.grdMain.Columns["SaleCarOutBillCode"].Visible = true;
                        this.grdMain.Columns["SaleCarInBillCode"].Visible = false;
                    });
                }
                else
                {
                    this.grdMain.Columns["SaleCarOutBillCode"].Visible = true;
                    this.grdMain.Columns["SaleCarInBillCode"].Visible = false;
                }
                strSort = "SaleCarOutBillCode asc";
            }
            else if (FilterTypeSelectedIndex == 2)
            {
                strFilter = "isnull(IsCancel,0) = 1 and BillDateIn>='" + DateTime.Now.AddHours(-12).ToString("yyyy-MM-dd HH:mm") + "'";
                //strFilter = "SaleCarOutBillID is null and isnull(IsCancel,0) = 1";
                if (this.grdMain.InvokeRequired)
                {
                    this.grdMain.Invoke((MethodInvoker)delegate
                    {
                        this.grdMain.Columns["SaleCarOutBillCode"].Visible = true;
                        this.grdMain.Columns["SaleCarInBillCode"].Visible = true;
                    });
                }
                else
                {
                    this.grdMain.Columns["SaleCarOutBillCode"].Visible = true;
                    this.grdMain.Columns["SaleCarInBillCode"].Visible = true;
                }
            }

            string strFilterText = "";
            if (this.txtFilter.InvokeRequired)
            {
                this.txtFilter.Invoke((MethodInvoker)delegate {
                    strFilterText = this.txtFilter.Text.TrimEnd();
                });
            }
            else
            {
                strFilterText = this.txtFilter.Text.TrimEnd();
            }

            if (strFilterText != "")
            {
                strFilter += " and ";
                strFilter += "(CarNum like '%" + strFilterText + "%' or ";
                strFilter += "ItemName like '%" + strFilterText + "%' or ";
                strFilter += "CustomerName like '%" + strFilterText + "%' ) ";
            }
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, strSort);

            //如果当前登录用户为地磅文员，则将非现金客户的单价和金额隐藏
            if (LoginInfo.UserType == 0)
            {
                foreach (DataRow dr in dtBill.Rows)
                {
                    int iReceiveType = LBConverter.ToInt32(dr["ReceiveType"]);
                    if (iReceiveType != 0)
                    {
                        dr["Price"] = DBNull.Value;
                        dr["Amount"] = DBNull.Value;
                    }
                }
            }
            if (this.grdMain.InvokeRequired)
            {
                this.grdMain.Invoke((MethodInvoker)delegate
                {
                    this.grdMain.DataSource = dtBill.DefaultView;
                });
            }
            else
            {
                this.grdMain.DataSource = dtBill.DefaultView;
            }
        }

        #endregion

        #region -- Init TextBox DataSource --

        private void InitTextDataSource()
        {
            DataTable dtFilterType = new DataTable();
            dtFilterType.Columns.Add("Index", typeof(int));
            dtFilterType.Columns.Add("Value", typeof(string));
            dtFilterType.Rows.Add("0", "未出场记录");
            dtFilterType.Rows.Add("1", "当天磅单记录");
            dtFilterType.Rows.Add("2", "当天作废（过期）记录");

            this.cbFilterType.DataSource = dtFilterType;
            this.cbFilterType.ValueMember = "Index";
            this.cbFilterType.DisplayMember = "Value";
            this.cbFilterType.SelectedIndex = 0;

            this.txtReceiveType.DataSource = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";
            this.txtReceiveType.SelectedIndexChanged += TxtReceiveType_SelectedIndexChanged;

            this.txtCalculateType.DataSource = LB.Common.LBConst.GetConstData("CalculateType");//计价方式
            this.txtCalculateType.DisplayMember = "ConstText";
            this.txtCalculateType.ValueMember = "ConstValue";

            DataTable dtCustom = ExecuteSQL.CallView(110,"","isnull(IsForbid,0)=0", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.LBFilter = "isnull(IsForbid,0)=0";
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

            DataTable dtInBill = ExecuteSQL.CallView(135);
            this.txtSaleCarInBillCode.TextBox.LBViewType = 135;
            this.txtSaleCarInBillCode.TextBox.IDColumnName = "SaleCarInBillCode";
            this.txtSaleCarInBillCode.TextBox.TextColumnName = "SaleCarInBillCode";
            this.txtSaleCarInBillCode.TextBox.PopDataSource = dtInBill.DefaultView;

            DataTable dtDesc = ExecuteSQL.CallView(121);
            this.txtDescription.TextBox.LBViewType = 121;
            this.txtDescription.TextBox.IDColumnName = "DescriptionID";
            this.txtDescription.TextBox.TextColumnName = "Description";
            this.txtDescription.TextBox.PopDataSource = dtDesc.DefaultView;

            this.txtCustomerID.TextBox.IsAllowNotExists = true;
            this.txtCarID.TextBox.IsAllowNotExists = true;
            this.txtItemID.TextBox.IsAllowNotExists = true;
            this.txtDescription.TextBox.IsAllowNotExists = true;

            this.txtCarID.TextBox.GotFocus += CoolText_GotFocus;
            this.txtItemID.TextBox.GotFocus += CoolText_GotFocus;
            this.txtCustomerID.TextBox.GotFocus += CoolText_GotFocus;

            this.txtCarID.TextBox.LostFocus += CoolText_LostFocus;
            this.txtItemID.TextBox.LostFocus += CoolText_LostFocus;
            this.txtCustomerID.TextBox.LostFocus += CoolText_LostFocus;

            this.txtSaleCarInBillCode.TextBox.TextChanged += BillCodeTextBox_TextChanged;
            this.txtCarID.TextBox.TextChanged += CarTextBox_TextChanged;
            this.txtItemID.TextBox.TextChanged += ItemTextBox_TextChanged;
            this.txtCustomerID.TextBox.TextChanged += CustomerTextBox_TextChanged;
            //this.txtCarID.TextBox.TextChanged += CoolText_TextChanged;
            //this.txtItemID.TextBox.TextChanged += CoolText_TextChanged;
            //this.txtCustomerID.TextBox.TextChanged += Customer_TextChanged;
            this.txtCalculateType.TextChanged += TxtCalculateType_TextChanged;

            this.txtTotalWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtSuttleWeight.TextChanged += TxtCalAmount_TextChanged;
            this.txtPrice.TextChanged += TxtCalAmount_TextChanged;
            this.txtCarTare.TextChanged += TxtCalAmount_TextChanged;

        }

        #endregion

        #region -- 客户、车号、物料 控件选中事件 --

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
                if (sender == txtCarID.TextBox)
                {
                    this._ctlBaseInfo.ChangeItemType(enBaseInfoType.CarInfo);
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
                    strFilter = "CustomerName like '%" + strValue + "%' and isnull(IsForbid,0)=0";
                }
                else
                {
                    strFilter = "isnull(IsForbid,0)=0";
                }
                _ctlBaseInfo.LoadDataSource(strFilter);
            }
        }


        private void TxtReceiveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (LBConverter.ToInt32(this.txtReceiveType.SelectedValue) == 5)
                {
                    this.lblWeiXinRate.Visible = true;
                }
                else
                {
                    this.lblWeiXinRate.Visible = false;
                }
                CalAmount();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        //选择单据编码触发事件
        private void BillCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtSaleCarInBillCode.TextBox.SelectedItemID!=null &&
                    this.txtSaleCarInBillCode.TextBox.SelectedItemID.ToString() != "" &&
                    this.txtSaleCarInBillCode.TextBox.SelectedRow != null)
                {
                    this.txtCarID.TextBox.ReadDataSource();//先刷新数据源,当新增车辆时，可能未及时刷新
                    this.txtCarID.TextBox.Text = this.txtSaleCarInBillCode.TextBox.SelectedRow["CarNum"].ToString();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //选择车辆触发事件
        private void CarTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
                                try
                                {
                                    //避免修改物料触发读价事件
                                    this.txtItemID.TextBox.TextChanged -= ItemTextBox_TextChanged;
                                    this.txtItemID.TextBox.SelectedItemID = LBConverter.ToInt64(dictValue["ItemID"]);
                                }
                                finally
                                {
                                    this.txtItemID.TextBox.TextChanged += ItemTextBox_TextChanged;
                                }
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
                            
                            this.txtCustomerID.Enabled = false;//出场时不能修改客户
                            this.txtItemID.Enabled = false;//出场时不能修改物料
                            //读取物料价格
                            ReadPrice();

                            #endregion -- 读取入场记录，并将入场记录值填写到对应控件 --
                        }
                        else
                        {
                            #region -- 该车无入场记录 --

                            this.txtCustomerID.Enabled = true;//入场时允许修改客户
                            this.txtItemID.Enabled = true;//入场时允许修改物料
                            this.txtSaleCarInBillCode.Text = "";
                            if (lCustomerID > 0)
                            {
                                this.txtCustomerID.TextBox.SelectedItemID = lCustomerID;
                            }

                            #endregion -- 该车无入场记录 --
                        }
                    }

                    #endregion
                }
                else
                {
                    this.txtCustomerID.Enabled = true;//出场时不能修改客户
                    this.txtItemID.Enabled = true;//出场时不能修改物料
                    this.txtSaleCarInBillCode.Text = "";
                }
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

        //判断该车辆是否存在场内
        private bool VerifyCarIsInside(long lCarID,string strCarNum,out long lSaleCarInBillID)
        {
            lSaleCarInBillID = 0;
            bool bolExists = false;
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("CarNum", enLBDbType.String, strCarNum));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14101, parmCol, out dsReturn, out dictValue);
            if (dictValue != null && dictValue.Keys.Count > 0)
            {
                if (dictValue.ContainsKey("IsReaded"))
                {
                    bolExists = LBConverter.ToBoolean(dictValue["IsReaded"]);
                }
                if (dictValue.ContainsKey("SaleCarInBillID"))
                {
                    lSaleCarInBillID = LBConverter.ToInt64(dictValue["SaleCarInBillID"]);
                }
            }
            return bolExists;
        }

        /// <summary>
        /// 读取物料价格
        /// </summary>
        /// <param name="sender"></param>
        private void ReadPrice()
        {
            this.txtPrice.PasswordChar = '*';//默认隐藏单价
            this.txtAmount.PasswordChar = '*';//默认隐藏金额
            this.lblCustomerRemainAmount.Text = "";
            
            //if (sender == this.txtCustomerID.TextBox || sender == this.txtCarID.TextBox || sender == this.txtItemID.TextBox || sender == this.txtCalculateType)
            {
                this.txtPrice.Text = "0";

                string strCarNum = this.txtCarID.TextBox.Text.ToString();
                string strItemName = this.txtItemID.TextBox.Text.ToString();
                string strCustomerName = this.txtCustomerID.TextBox.Text.ToString();

                if (this.txtCustomerID.TextBox.SelectedRow != null)
                {
                    bool bolIsDisplayPrice = LBConverter.ToBoolean(this.txtCustomerID.TextBox.SelectedRow["IsDisplayPrice"]);
                    bool bolIsDisplayAmount = LBConverter.ToBoolean(this.txtCustomerID.TextBox.SelectedRow["IsDisplayAmount"]);

                    if (bolIsDisplayPrice)
                    {
                        this.txtPrice.PasswordChar = '\0';
                    }
                    else
                    {
                        //隐藏单价
                        this.txtPrice.PasswordChar = '*';
                    }
                    if (bolIsDisplayAmount)
                    {
                        this.txtAmount.PasswordChar = '\0';
                    }
                    else
                    {
                        //隐藏金额
                        this.txtAmount.PasswordChar = '*';
                    }
                }

                long lCustomerID = 0;
                using (DataTable dtCustomer = ExecuteSQL.CallView(112, "CustomerID,ReceiveType,TotalReceivedAmount,SalesReceivedAmount", "CustomerName='" + strCustomerName + "'", ""))
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        lCustomerID = LBConverter.ToInt64(dtCustomer.Rows[0]["CustomerID"]);
                        decimal decTotalReceivedAmount = LBConverter.ToDecimal(dtCustomer.Rows[0]["TotalReceivedAmount"]);
                        decimal decSalesReceivedAmount = LBConverter.ToDecimal(dtCustomer.Rows[0]["SalesReceivedAmount"]);
                        this.lblCustomerRemainAmount.Text = (decTotalReceivedAmount - decSalesReceivedAmount).ToString();
                        //客户收款方式
                        int iReceiveType = LBConverter.ToInt32(dtCustomer.Rows[0]["ReceiveType"]);
                        this.txtReceiveType.SelectedValue = iReceiveType;

                        //if (iReceiveType == 0|| iReceiveType == 3|| iReceiveType == 4)//现金、免费、汽油外购客户需要将客户充值余额隐藏
                        //{
                        //    lblCustomerRemainAmount.Visible=label4.Visible = false;
                        //}
                        //else
                        //{
                        //    lblCustomerRemainAmount.Visible = label4.Visible = true;
                        //}
                    }
                }

                if (strCarNum == "" || strItemName == "")
                    return;

                long lCarID = 0;
                long lItemID = 0;
                
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
                    this.txtPrice.Text = LBConverter.ToDecimal(dictValue["Price"]).ToString("0.00000");
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
        #endregion

        #region -- 初始化基础资料列表 --

        private void InitBaseControl()
        {
            _ctlBaseInfo = new LB.MainForm.CtlBaseInfoSelection();
            _ctlBaseInfo.Dock = DockStyle.Fill;
            this.pnlBaseInfo.Controls.Add(_ctlBaseInfo);

            _ctlBaseInfo.SelectedRowEvent += _ctlBaseInfo_SelectedRowEvent;
        }

        private void _ctlBaseInfo_SelectedRowEvent(Controls.Args.SelectedRowArgs e)
        {
            try
            {
                if (e.BaseInfoType == enBaseInfoType.CarIn)
                {
                    this.txtCarID.TextBox.ReadDataSource();//先刷新数据源,当新增车辆时，可能未及时刷新
                    this.txtCarID.TextBox.SelectedItemID = e.SelectedRow["CarID"];
                }
                else if (e.BaseInfoType == enBaseInfoType.CarOut)
                {
                    this.txtCarID.TextBox.ReadDataSource();//先刷新数据源,当新增车辆时，可能未及时刷新
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

        #region -- 查询磅单清单 --

        private void btnFilter_Click(object sender, EventArgs e)
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

        #endregion -- 查询磅单清单 --

        #region -- 磅单操作按钮事件 --

        //毛重
        private void btnTotalWeight_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常
                VerifyTextBoxIsEmpty();//判断相关控件值是否为空
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                string strCarNum = this.txtCarID.TextBox.Text;
                long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                int iReceiveType = 0;
                if (this.txtCustomerID.TextBox.SelectedRow != null)
                {
                    iReceiveType = LBConverter.ToInt32(this.txtCustomerID.TextBox.SelectedRow["ReceiveType"]);
                }
                //判断该车辆是否存在入场记录，如果不存在则报错
                long lSaleCarInBillID;
                bool bolCarIsInside = VerifyCarIsInside(lCarID, strCarNum, out lSaleCarInBillID);

                if (!bolCarIsInside)//保存
                {
                    bool bolAllowOut = false;
                    //判断该客户是否允许空磅入场
                    if (lCustomerID > 0)
                    {
                        DataRowView drvCustomer = this.txtCustomerID.TextBox.SelectedRow;
                        if (LBConverter.ToBoolean(drvCustomer["IsAllowEmptyIn"]))
                        {
                            bolAllowOut = true;
                        }
                    }

                    if(!bolAllowOut)
                        throw new Exception("该车辆不在场内，无法重车出榜！");
                }
                
                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读毛重
                this.txtTotalWeight.Text = decWeight.ToString("0");

                if (decWeight == 0)
                {
                    throw new Exception("当前【毛重】值为0！");
                }
                //读取单价
                ReadPrice(lCarID, lItemID, lCustomerID);

                decimal decAmount = LBConverter.ToDecimal(this.txtAmount.Text);//读金额
                if (iReceiveType!=3 && decAmount == 0)//收款方式为免费，则金额可为0
                {
                    throw new Exception("当前【金额】值为0！");
                }

                mlSaleCarInBillID = lSaleCarInBillID;
                
                //this.btnReadTareWeight.Enabled = false;
                //this.btnTotalWeight.Enabled = false;

                _WeightType = enWeightType.WeightOut;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        //空车
        private void btnReadTareWeight_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常
                VerifyTextBoxIsEmpty();//判断相关控件值是否为空
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                string strCarNum = this.txtCarID.TextBox.Text;
                long lItemID = LBConverter.ToInt64(this.txtItemID.TextBox.SelectedItemID);
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);

                //判断该车辆是否存在入场记录，如果存在则报错
                long lSaleCarInBillID;
                bool bolCarIsInside = VerifyCarIsInside(lCarID, strCarNum,out lSaleCarInBillID);

                if (bolCarIsInside)//保存
                {
                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否空车出场，请确认当前车辆没有装货？","提示", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                        this.txtTotalWeight.Text = decWeight.ToString("0");
                        mlSaleCarInBillID = lSaleCarInBillID;
                        _WeightType = enWeightType.WeightOutNull;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                    this.txtCarTare.Text = decWeight.ToString("0");

                    if (decWeight == 0)
                    {
                        throw new Exception("当前【皮重】值为0！");
                    }
                    this.txtTotalWeight.Text = "0";
                    this.txtSaleCarInBillCode.Text = "";
                    mlSaleCarInBillID = 0;
                    _WeightType = enWeightType.WeightIn;
                }
                
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //保存并打印
        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            try
            {
                long lCustomerID = LBConverter.ToInt64(this.txtCustomerID.TextBox.SelectedItemID);
                if (lCustomerID > 0)
                {
                    //校验预付客户余额是否低于预警值
                    VerifyAmountNotEnough(lCustomerID);
                }

                VerifyDeviceIsSteady();//校验地磅数值是否稳定以及红外线对射是否正常

                if (_WeightType== enWeightType.WeightIn)
                {
                    decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                    this.txtCarTare.Text = decWeight.ToString("0");

                    long lSaleCarInBillID = SaveInBill();
                    if (lSaleCarInBillID>0)
                    {
                        this.ClearAllBillInfo();
                        _WeightType = enWeightType.None;
                    }
                }
                else if (_WeightType == enWeightType.WeightOut)
                {
                    decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读毛重
                    this.txtTotalWeight.Text = decWeight.ToString("0");

                    SaveOutBill(mlSaleCarInBillID);
                    _WeightType = enWeightType.None;
                    mlSaleCarInBillID = 0;
                }
                else if(_WeightType== enWeightType.WeightOutNull)//空车出场
                {
                    decimal decInWeight = LBConverter.ToDecimal(this.txtCarTare.Text);
                    decimal decOutWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);

                    if(Math.Abs(decInWeight- decOutWeight)>1000)
                    {
                        throw new Exception("该车辆入场重量与出场重量的磅差太大，无法空车出场，请检查该车辆是否空车！");
                    }

                    SaveOutBill(mlSaleCarInBillID);
                    mlSaleCarInBillID = 0;
                    _WeightType = enWeightType.None;
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("空车磅单生成成功！");
                }

                //this.btnReadTareWeight.Enabled = true;
                //this.btnTotalWeight.Enabled = true;

                //LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        //补打磅单
        private void btnRePrintOutReport_Click(object sender, EventArgs e)
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        //保存皮重
        private void btnSaveTareWeight_Click(object sender, EventArgs e)
        {
            try
            {
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                if (lCarID == 0)
                {
                    throw new Exception("车号不能为空或者该车号不存在！");
                }

                decimal decWeight = LBConverter.ToDecimal(lblWeight.Text);//读皮重
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage(
                    "是否确认记录当前车辆的最新皮重？\n车  号："+ this.txtCarID.TextBox.Text+"\n"+"皮  重："+ decWeight.ToString("0"),
                    "提示", MessageBoxButtons.YesNo)== DialogResult.Yes)
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
                    parmCol.Add(new LBParameter("CarWeight", enLBDbType.Decimal, decWeight));
                    parmCol.Add(new LBParameter("Description", enLBDbType.String, "皮重来源：地磅保存皮重"));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(20400, parmCol, out dsReturn, out dictValue);

                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("该车辆皮重更新成功！");
                }
                
                
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        //读取皮重
        private void btnLoadTareWeight_Click(object sender, EventArgs e)
        {
            try
            {
                long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
                if (lCarID == 0)
                {
                    throw new Exception("车号不能为空或者该车号不存在！");
                }

                DataTable dtWeight = ExecuteSQL.CallView(131, "top 1 *", "CarWeight>0 and CarID=" + lCarID, "CreateTime desc");
                if (dtWeight.Rows.Count > 0)
                {
                    decimal decCarWeight = LBConverter.ToDecimal(dtWeight.Rows[0]["CarWeight"]);
                    this.txtCarTare.Text = decCarWeight.ToString("0");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
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
        
        private void btnClearValue_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearAllBillInfo();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnViewSalesBill_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleCarInOutBillManager frmBill = new frmSaleCarInOutBillManager();
                frmBill.PageAutoSize = true;
                LBShowForm.ShowDialog(frmBill);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //退货处理
        private void btnReturnSaleBill_Click(object sender, EventArgs e)
        {
            try
            {
                LBPermission.VerifyUserPermission("退货处理", "WeightSalesReturn_View");

                _frmReturnBill = new frmSalesReturnBill();
                _frmReturnBill.GetCameraEvent += FrmReturn_GetCameraEvent;
                _frmReturnBill.PrintOutBillEvent += _frmReturnBill_PrintOutBillEvent;
                LBShowForm.Show(_frmReturnBill, true);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //汽油采购
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                LBPermission.VerifyUserPermission("汽油采购", "WeightPurchase_View");

                _frmPurchaseBill = new frmPurchaseBill();
                _frmPurchaseBill.GetCameraEvent += FrmReturn_GetCameraEvent;
                _frmPurchaseBill.PrintOutBillEvent += _frmPurchaseBill_PrintOutBillEvent;
                _frmPurchaseBill.PrintInBillEvent += _frmPurchaseBill_PrintInBillEvent;
                LBShowForm.Show(_frmPurchaseBill, true);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void _frmPurchaseBill_PrintInBillEvent(object sender, Billinfo e)
        {
            try
            {
                PreviceReport(e.NewSaleCarOutBillID, enWeightType.WeightIn, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void _frmReturnBill_PrintOutBillEvent(object sender, Billinfo e)
        {
            try
            {
                PreviceReport(e.NewSaleCarOutBillID, enWeightType.WeightOut, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void _frmPurchaseBill_PrintOutBillEvent(object sender, Billinfo e)
        {
            try
            {
                PreviceReport(e.NewSaleCarOutBillID, enWeightType.WeightOut, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void FrmReturn_GetCameraEvent(object sender, CameraInfo e)
        {
            try
            {
                byte[] bImg1 = null;
                byte[] bImg2 = null;
                byte[] bImg3 = null;
                byte[] bImg4 = null;

                foreach (KeyValuePair<ViewCamera, bool> keyvalue in dictIsOpen)
                {
                    if (bImg1 == null)
                        bImg1 = keyvalue.Key.CapturePic();
                    else if (bImg2 == null)
                        bImg2 = keyvalue.Key.CapturePic();
                    else if (bImg3 == null)
                        bImg3 = keyvalue.Key.CapturePic();
                    else if (bImg4 == null)
                        bImg4 = keyvalue.Key.CapturePic();
                }

                e.Image1 = bImg1;
                e.Image2 = bImg2;
                e.Image3 = bImg3;
                e.Image4 = bImg4;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        #endregion -- 磅单操作按钮事件 --

        #region-- 保存入磅记录 --

        private long SaveInBill()
        {
            Dictionary<string, double> dictTest = new Dictionary<string, double>();
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            long lSaleCarInBillID=0;
            long lCarID = LBConverter.ToInt64(this.txtCarID.TextBox.SelectedItemID);
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
            parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, 0));
            parmCol.Add(new LBParameter("SaleCarInBillCode", enLBDbType.String, ""));
            parmCol.Add(new LBParameter("BillDate", enLBDbType.DateTime, DateTime.Now));
            parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, lCarID));
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14100, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SaleCarInBillID"))
            {
                lSaleCarInBillID = LBConverter.ToInt64(dictValue["SaleCarInBillID"]);
                Thread threadSavePic = new Thread(SaveInSalesPicture);
                threadSavePic.Start(dictValue["SaleCarInBillID"]);
            }
            if (dictValue.ContainsKey("SaleCarInBillCode"))
            {
                this.txtSaleCarInBillCode.Text = dictValue["SaleCarInBillCode"].ToString();
            }

            dt2 = DateTime.Now;
            dictTest.Add("CallSP", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            LoadAllSalesBill(); //刷新磅单清单

            dt2 = DateTime.Now;
            dictTest.Add("LoadAllSalesBill", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;


            try
            {
                PreviceReport(lSaleCarInBillID, enWeightType.WeightIn, enRequestReportActionType.DirectPrint);
            }
            catch (Exception ex)
            {
            }
            dt2 = DateTime.Now;
            dictTest.Add("PreviceReport", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;
            

            dt2 = DateTime.Now;
            dictTest.Add("ShowDialog", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            
            dt2 = DateTime.Now;
            dictTest.Add("ClearAllBillInfo", dt2.Subtract(dt1).TotalMilliseconds);
            dt1 = dt2;

            return lSaleCarInBillID;
        }

        private void SaveInSalesPicture(object objSaleCarInBillID)
        {
            long lSaleCarInBillID = LBConverter.ToInt64(objSaleCarInBillID);
            try
            {
                byte[] bImg1 = null;
                byte[] bImg2 = null;
                byte[] bImg3 = null;
                byte[] bImg4 = null;

                foreach (KeyValuePair<ViewCamera, bool> keyvalue in dictIsOpen)
                {
                    if (bImg1 == null)
                        bImg1 = keyvalue.Key.CapturePic();
                    else if (bImg2 == null)
                        bImg2 = keyvalue.Key.CapturePic();
                    else if (bImg3 == null)
                        bImg3 = keyvalue.Key.CapturePic();
                    else if (bImg4 == null)
                        bImg4 = keyvalue.Key.CapturePic();
                }

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
            catch (Exception ex)
            {
                LBErrorLog.InsertErrorLog("保存入场图片时报错，入场单号：" + lSaleCarInBillID.ToString() + "\n错误信息：" + ex.Message);
            }
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
        }

        #endregion

        #region-- 保存出磅记录 --

        private void SaveOutBill(long lSaleCarInBillID)
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
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);//0按重量计算 1按车计算
            decimal decCustomerRemainAmount = 0;
            bool bolNeedCreateInBill = false;//是否需要生成入场单
            if (lSaleCarInBillID == 0)//如果入场单号为空，则判断该客户是否允许空磅入场，如果是，则先读取默认皮重然后自动生成入场订单
            {
                if (lCustomerID > 0)
                {
                    DataRowView drvCustomer = this.txtCustomerID.TextBox.SelectedRow;
                    if (LBConverter.ToBoolean(drvCustomer["IsAllowEmptyIn"]))
                    {
                        DataRowView drvCar = this.txtCarID.TextBox.SelectedRow;
                        decimal decDefaultCarWeight = LBConverter.ToDecimal(drvCar["DefaultCarWeight"]);

                        decimal decTotalReceivedAmount = LBConverter.ToDecimal(drvCustomer["TotalReceivedAmount"]);
                        decimal decSalesReceivedAmount = LBConverter.ToDecimal(drvCustomer["SalesReceivedAmount"]);
                        decCustomerRemainAmount = decTotalReceivedAmount - decSalesReceivedAmount;

                        if (iCalculateType == 0)
                        {
                            bolNeedCreateInBill = true;
                            this.txtCarTare.Text = decDefaultCarWeight.ToString("0");
                        }
                        else
                        {
                            if (decDefaultCarWeight <= 0)
                            {
                                LB.WinFunction.LBCommonHelper.ShowCommonMessage("该车辆未设置默认皮重");
                                return;
                            }

                            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("该车牌默认皮重为【" + decDefaultCarWeight.ToString("0") + "】,是否读取默认皮重值？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                this.txtCarTare.Text = decDefaultCarWeight.ToString("0");
                                bolNeedCreateInBill = true;
                            }
                        }
                        
                    }
                }
            }

            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);

            int iReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decSuttleWeight = decTotalWeight - decCarTare; //LBConverter.ToDecimal(this.txtSuttleWeight.Text);
            decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);
            decimal decAmount = LBConverter.ToDecimal(this.txtAmount.Text);

            //iReceiveType=3免费
            if (iReceiveType!=3 && _WeightType != enWeightType.WeightOutNull && decPrice > 0 && ( decSuttleWeight < 100 || decAmount < 1))//重车保存时出现异常，记录日志
            {
                Thread thread = new Thread(SaveScreenPicture);
                thread.Start(lSaleCarInBillID);
                LBErrorLog.InsertErrorLog("客户端重车异常：TotalWeight=" + decTotalWeight.ToString() + " CarTare=" + decCarTare.ToString() + " SuttleWeight=" + decSuttleWeight.ToString() + " SaleCarInBillID=" + lSaleCarInBillID);

                throw new Exception("净重值计算异常，请先[清空内容]，然后重新选择车牌号码进行重车出场操作！");
            }

            //if (iReceiveType == 3)//收款方式：免费
            //{
            //    throw new Exception("当前【毛重】值为0，无法保存！");
            //}

            if (decTotalWeight == 0)
            {
                throw new Exception("当前【毛重】值为0，无法保存！");
            }
            if (_WeightType != enWeightType.WeightOutNull && decSuttleWeight <= 0)//空车出场时不校验净重
            {
                throw new Exception("当前【净重】值为零或者负数，无法保存！");
            }
            if (iCalculateType==1 && decCarTare == 0)
            {
                throw new Exception("当前【皮重】值为0，无法保存！");
            }

            bool bolIsDisplayAmount = false;//默认隐藏金额
            if (this.txtCustomerID.TextBox.SelectedRow != null)
            {
                bolIsDisplayAmount = LBConverter.ToBoolean(this.txtCustomerID.TextBox.SelectedRow["IsDisplayAmount"]);
            }

            decimal decCustomerPayAmount = 0;
            if (_WeightType != enWeightType.WeightOutNull)
            {
                LB.MainForm.MainForm.frmChooseReceiveType frmReceive =
                new LB.MainForm.MainForm.frmChooseReceiveType(iReceiveType,
                this.txtCustomerID.TextBox.Text,
                this.txtCarID.TextBox.Text,
                this.txtItemID.TextBox.Text,
                this.txtAmount.Text,
                decCustomerRemainAmount.ToString("0"),
                bolIsDisplayAmount);

                LBShowForm.ShowDialog(frmReceive);
                if (!frmReceive.IsSubmit)
                {
                    return;
                }
                this.txtReceiveType.SelectedValue = frmReceive.ReceiveType;
                iReceiveType = frmReceive.ReceiveType;
                decCustomerPayAmount = frmReceive.CustomerPayAmount;

                if (bolNeedCreateInBill)
                {
                    lSaleCarInBillID = SaveInBill();
                }
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
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, iReceiveType));
            parmCol.Add(new LBParameter("CalculateType", enLBDbType.Int32, iCalculateType));
            parmCol.Add(new LBParameter("TotalWeight", enLBDbType.Decimal, decTotalWeight));
            parmCol.Add(new LBParameter("SuttleWeight", enLBDbType.Decimal, decSuttleWeight));
            //入库单信息
            parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, lItemID));
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));
            parmCol.Add(new LBParameter("CarTare", enLBDbType.Decimal, decCarTare));
            parmCol.Add(new LBParameter("CustomerPayAmount", enLBDbType.Decimal, decCustomerPayAmount));
            parmCol.Add(new LBParameter("IsEmptyOut", enLBDbType.Int16, (_WeightType == enWeightType.WeightOutNull?1:0)));//是否空车出

            int iSPType = 14102;
            if (_WeightType == enWeightType.WeightOnlyOut)
            {
                iSPType = 14112;
            }
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            long lSaleCarOutBillID = 0;
            if (dictValue.ContainsKey("SaleCarOutBillID"))
            {
                lSaleCarOutBillID = LBConverter.ToInt64(dictValue["SaleCarOutBillID"].ToString());
                Thread threadSavePic = new Thread(SaveOutSalesPicture);
                threadSavePic.Start(lSaleCarOutBillID);
            }
            LoadAllSalesBill(); //刷新磅单清单

            try
            {
                if (_WeightType != enWeightType.WeightOutNull)//空车出无需打印磅单
                {
                    //打印磅单
                    //frmPrint = new frmAutoPrint();
                    PreviceReport(lSaleCarOutBillID, _WeightType, enRequestReportActionType.DirectPrint);
                    //LBShowForm.ShowDialog(frmPrint);
                }
            }
            catch (Exception ex)
            {
            }

            this.ClearAllBillInfo();
        }

        private void SaveOutSalesPicture(object objSaleCarOutBillID)
        {
            long lSaleCarOutBillID = LBConverter.ToInt64(objSaleCarOutBillID);
            try
            {
                byte[] bImg1 = null;
                byte[] bImg2 = null;
                byte[] bImg3 = null;
                byte[] bImg4 = null;

                foreach (KeyValuePair<ViewCamera, bool> keyvalue in dictIsOpen)
                {
                    if (bImg1 == null)
                        bImg1 = keyvalue.Key.CapturePic();
                    else if (bImg2 == null)
                        bImg2 = keyvalue.Key.CapturePic();
                    else if (bImg3 == null)
                        bImg3 = keyvalue.Key.CapturePic();
                    else if (bImg4 == null)
                        bImg4 = keyvalue.Key.CapturePic();
                }

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarOutBillID", enLBDbType.Int64, lSaleCarOutBillID));
                parmCol.Add(new LBParameter("MonitoreImg1", enLBDbType.Bytes, bImg1));
                parmCol.Add(new LBParameter("MonitoreImg2", enLBDbType.Bytes, bImg2));
                parmCol.Add(new LBParameter("MonitoreImg3", enLBDbType.Bytes, bImg3));
                parmCol.Add(new LBParameter("MonitoreImg4", enLBDbType.Bytes, bImg4));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14114, parmCol, out dsReturn, out dictValue);
            }
            catch (Exception ex)
            {
                LBErrorLog.InsertErrorLog("保存出场图片时报错，出场单号：" + lSaleCarOutBillID.ToString() + "\n错误信息：" + ex.Message);
            }
        }

        private void SaveScreenPicture(object objSaleCarOutBillID)
        {
            try
            {
                long lSaleCarOutBillID = LBConverter.ToInt64(objSaleCarOutBillID);
                byte[] bImg3 = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap map = GetCurrentScreenImg();
                    map.Save(ms, ImageFormat.Jpeg);
                    if (ms != null)
                    {
                        bImg3 = new byte[ms.Length];
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.Read(bImg3, 0, bImg3.Length);
                    }
                }

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("SaleCarOutBillID", enLBDbType.Int64, lSaleCarOutBillID));
                parmCol.Add(new LBParameter("ScreenPicture", enLBDbType.Bytes, bImg3));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14121, parmCol, out dsReturn, out dictValue);
            }
            catch (Exception ex)
            {

            }
        }

        //读取当前电脑截屏
        private Bitmap GetCurrentScreenImg()
        {
            //创建图象，保存将来截取的图象
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            //设置截屏区域 柯乐义
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            image.Save( Path.Combine(Application.StartupPath, "ooo.jpg"), ImageFormat.Jpeg);
            //imgGraphics.Dispose();
            return image;
        }
        #endregion-- 保存出磅记录 --

        #region -- 预览磅单 --

        private void PreviceReport(long saleID, enWeightType weightType, enRequestReportActionType requestType)
        {
            ProcessStep.mdtStep = null;
            ReportRequestArgs args;
            if (weightType == enWeightType.WeightIn)
            {
                DataTable dtReport = ReportHelper.GetReportTemplateRowByType(6);
                if (dtReport.Rows.Count > 0)
                {
                    DataRow drReport = dtReport.Rows[0];
                    //打印磅单
                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        frmPrint = new frmAutoPrint();
                    }
                    
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);
                    

                    args = new ReportRequestArgs(lReportTemplateID, 6, null, null);

                    //long lSaleCarInBillID = LBConverter.ToInt64(this.txtSaleCarInBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(128, "", "SaleCarInBillID=" + saleID, "");

                    //DataTable dtTemp = dtBill.Copy();
                    //foreach (DataRow dr in dtTemp.Rows)
                    //{
                    //    dtBill.ImportRow(dr);
                    //}

                    dtBill.TableName = "T006";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ProcessStep.AddStep("CallView_128", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                    
                    ReportHelper.OpenReportDialog(requestType, args);

                    ProcessStep.AddStep("OpenReportDialog_End", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);

                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        //记录小票打印次数
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, saleID));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14109, parmCol, out dsReturn, out dictValue);

                        ProcessStep.AddStep("CallSP_14109", DateTime.Now.ToString("MMdd HH:mm:ss ") + DateTime.Now.Millisecond);
                        DataTable dtt = ProcessStep.mdtStep;

                        LBShowForm.ShowDialog(frmPrint);
                    }
                }
            }
            else if (weightType == enWeightType.WeightOut ||
                    weightType == enWeightType.WeightOnlyOut)
            {
                DataTable dtReport = ReportHelper.GetReportTemplateRowByType(7);
                if (dtReport.Rows.Count > 0)
                {
                    DataRow drReport = dtReport.Rows[0];
                    //打印磅单
                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        frmPrint = new frmAutoPrint();
                    }
                    
                    long lReportTemplateID = Convert.ToInt64(drReport["ReportTemplateID"]);
                    long lReportTypeID = Convert.ToInt64(drReport["ReportTypeID"]);

                    args = new ReportRequestArgs(lReportTemplateID, 7, null, null);

                    //long lSaleCarOutBillID = LBConverter.ToInt64(this.txtSaleCarOutBillID.Text);
                    DataTable dtBill = ExecuteSQL.CallView(125, "", "SaleCarOutBillID=" + saleID, "");
                    dtBill.TableName = "T007";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtBill);
                    args.DSDataSource = dsSource;

                    ReportHelper.OpenReportDialog(requestType, args);

                    if (requestType == enRequestReportActionType.DirectPrint)
                    {
                        //记录磅单打印次数
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarOutBillID", enLBDbType.Int64, saleID));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14110, parmCol, out dsReturn, out dictValue);

                        LBShowForm.ShowDialog(frmPrint);
                    }
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

        #region -- 计算 金额=净重*单价 --

        private void CalAmount()
        {
            decimal decTotalWeight = LBConverter.ToDecimal(this.txtTotalWeight.Text);
            decimal decCarTare = LBConverter.ToDecimal(this.txtCarTare.Text);
            int iCalculateType = LBConverter.ToInt32(this.txtCalculateType.SelectedValue);//0按重量计算 1按车计算
            int iReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);//收款方式
            this.txtSuttleWeight.Text = (decTotalWeight - decCarTare).ToString("0");
            decimal decPrice = LBConverter.ToDecimal(this.txtPrice.Text);

            int iAmountType = 0;//金额格式：0整数 1一位小数 2两位小数
            if (this.txtCustomerID.TextBox.SelectedRow != null)
            {
                iAmountType = LBConverter.ToInt32(this.txtCustomerID.TextBox.SelectedRow["AmountType"]);
            }
            decimal decRate = 1;
            if (iReceiveType == 5)//微信支付加收0.005
            {
                decRate += (decimal)0.005;
            }

            string strFormat = "0";
            if(iAmountType==1)
                strFormat = "0.0";
            else if (iAmountType == 2)
                strFormat = "0.00";

            if (iCalculateType == 0)
                this.txtAmount.Text = (decPrice * (decTotalWeight - decCarTare)* decRate).ToString(strFormat);
            else
                this.txtAmount.Text = (decPrice * decRate).ToString(strFormat);
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
            //LBSerialHelper.LBSerialDataEvent -= LBSerialHelper_LBSerialDataEvent;
            if (mVersionThread != null && mVersionThread.IsAlive)
            {
                mVersionThread.Abort();
            }

            if (mTimer != null)
            {
                mTimer.Enabled = false;
            }
            if (mAutoComputeTimer != null)
            {
                mAutoComputeTimer.Enabled = false;
            }
            if (mTimerCamera != null)
            {
                mTimerCamera.Enabled = false;
            }
            
            LBSerialHelper.StopTimer();
            LBInFrareHelper.StopTimer();
            LBPortHelper.EndThread();
            SessionHelper.EndTakeSession();
            try
            {
                ExecuteSQL.LogOutSession();//删除Session
            }
            catch(Exception ex)
            {

            }

            foreach (KeyValuePair<ViewCamera,Panel> keyvalue in dictCamera)
            {
                if (keyvalue.Key != null)
                {
                    try
                    {
                        keyvalue.Key.CloseCamera();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        #region -- grdMain右键按钮事件 --

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    string strCarNum = drv["CarNum"].ToString().TrimEnd();
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"].ToString());
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"].ToString());

                    if (lSaleCarOutBillID == 0)//作废入场记录
                    {
                        LBPermission.VerifyUserPermission("作废", "");
                    }
                    else
                    {
                        //作废入场和出场记录

                    }

                    frmSaleCarInOutCancel frmCancel = new frmSaleCarInOutCancel(strCarNum);
                    LBShowForm.ShowDialog(frmCancel);

                    if (frmCancel.IsAllowCancel)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
                        parmCol.Add(new LBParameter("CancelDesc", enLBDbType.String, frmCancel.CancelDesc));
                        
                        int iSPType = 14106;
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);

                        LoadAllSalesBill(); //刷新磅单清单
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        //补打小票
        private void tsmRePrintInBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    long lSaleCarInBillID = Convert.ToInt64(drv["SaleCarInBillID"]);

                    //允许补打磅单次数
                    int iAllowPrintInReportCount;
                    SysConfigValue.GetSysConfig("AllowPrintInReportCount", out iAllowPrintInReportCount);

                    if (iAllowPrintInReportCount == 0)
                    {
                        throw new Exception("系统设置不允许补打小票！");
                    }

                    if (lSaleCarInBillID==0)
                    {
                        throw new Exception("请选择需要补打的数据行！");
                    }
                    
                    DataTable dtIn = ExecuteSQL.CallView(123, "PrintCount", "SaleCarInBillID=" + lSaleCarInBillID, "");
                    if (dtIn.Rows.Count > 0)
                    {
                        int iInPrintCount = LBConverter.ToInt32(dtIn.Rows[0]["PrintCount"]);
                        if (iInPrintCount >= iAllowPrintInReportCount + 1)
                        {
                            throw new Exception("补打次数已超出系统设置的次数！");
                        }
                    }
                    PreviceReport(lSaleCarInBillID, enWeightType.WeightIn, enRequestReportActionType.DirectPrint);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void tsmRePrintOutBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    long lSaleCarOutBillID = Convert.ToInt64(drv["SaleCarOutBillID"]);
                    //允许补打磅单次数
                    int iAllowPrintOutReportCount;
                    SysConfigValue.GetSysConfig("AllowPrintOutReportCount", out iAllowPrintOutReportCount);

                    if (iAllowPrintOutReportCount == 0)
                    {
                        throw new Exception("系统设置不允许补打磅单！");
                    }

                    if (lSaleCarOutBillID==0)
                    {
                        throw new Exception("请选择需要补打的数据行！");
                    }
                    
                    DataTable dtOut = ExecuteSQL.CallView(124, "OutPrintCount", "SaleCarOutBillID=" + lSaleCarOutBillID, "");
                    if (dtOut.Rows.Count > 0)
                    {
                        int iOutPrintCount = LBConverter.ToInt32(dtOut.Rows[0]["OutPrintCount"]);
                        if (iOutPrintCount >= iAllowPrintOutReportCount + 1)
                        {
                            throw new Exception("补打次数已超出系统设置的次数！");
                        }
                    }

                    PreviceReport(lSaleCarOutBillID, enWeightType.WeightOut, enRequestReportActionType.DirectPrint);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void tsmPreviewInBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    long lSaleCarInBillID = Convert.ToInt64(drv["SaleCarInBillID"]);

                    if (lSaleCarInBillID == 0)
                    {
                        throw new Exception("请选择需要预览的数据行！");
                    }

                    PreviceReport(lSaleCarInBillID, enWeightType.WeightIn, enRequestReportActionType.Preview);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void tsmPreviewOutBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    long lSaleCarOutBillID = Convert.ToInt64(drv["SaleCarOutBillID"]);
                    
                    if (lSaleCarOutBillID == 0)
                    {
                        throw new Exception("请选择需要预览的数据行！");
                    }

                    PreviceReport(lSaleCarOutBillID, enWeightType.WeightOut, enRequestReportActionType.Preview);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void tsmUnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    string strCarNum = drv["CarNum"].ToString().TrimEnd();
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"].ToString());
                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否取消作废车牌号码为【" + strCarNum + "】的记录？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));

                        int iSPType = 14107;
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);

                        LoadAllSalesBill(); //刷新磅单清单
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void tsmChangeBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (contextMenuStrip1.Tag != null)
                {
                    DataRowView drv = contextMenuStrip1.Tag as DataRowView;
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"].ToString());
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"].ToString());

                    if (lSaleCarOutBillID > 0)//已出场的单据不允许走改单流程
                    {
                        throw new Exception("该单据已生成出场单，无法变更，请与相关负责人联系！");
                    }

                    frmSaleCarChangeBill frmChange = new frmSaleCarChangeBill(lSaleCarInBillID, lSaleCarOutBillID);
                    LBShowForm.ShowDialog(frmChange);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- grdMain右键按钮事件 --

        #region -- 工具栏按钮事件 --

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


        private void btnInfraredDeviceConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmInfraredDeviceConfig frmConfig = new frmInfraredDeviceConfig();
                LBShowForm.ShowDialog(frmConfig);
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
                //WeightReportConfig();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnWeightReportSetIn_Click(object sender, EventArgs e)
        {
            try
            {
                WeightReportConfig(enWeightType.WeightIn);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnWeightReportSetOut_Click(object sender, EventArgs e)
        {
            try
            {
                WeightReportConfig(enWeightType.WeightOut);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        #endregion -- 工具栏按钮事件 --

        #region -- 报表设计 --

        private void WeightReportConfig(enWeightType weightType)
        {
            ReportRequestArgs args;
            if (weightType == enWeightType.WeightIn)
            {
                args = new ReportRequestArgs(0, 6, null, null);

                long lSaleCarInBillID = LBConverter.ToInt64(0);
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
            else if (weightType == enWeightType.WeightOut )
            {
                args = new ReportRequestArgs(0, 7, null, null);

                long lSaleCarOutBillID = LBConverter.ToInt64(0);
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
        
        private void MAutoComputeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(AutoComputeThread);
                thread.Start();
            }
            catch (Exception ex)
            {
                //LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void AutoComputeThread()
        {
            try
            {
                int iCarCount = 0;
                decimal decWeight = 0;
                int iTotalCarCount = 0;
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("InsideCarCount", enLBDbType.Int32, 0, true));
                parmCol.Add(new LBParameter("SalesTotalWeight", enLBDbType.Decimal, true));
                parmCol.Add(new LBParameter("TotalCar", enLBDbType.Int32, true));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14113, parmCol, out dsReturn, out dictValue);
                if (dictValue != null && dictValue.Keys.Count > 0)
                {
                    if (dictValue.ContainsKey("InsideCarCount"))
                    {
                        iCarCount = LBConverter.ToInt32(dictValue["InsideCarCount"]);
                    }
                    if (dictValue.ContainsKey("SalesTotalWeight"))
                    {
                        decWeight = LBConverter.ToDecimal(dictValue["SalesTotalWeight"]);
                    }
                    if (dictValue.ContainsKey("SalesTotalWeight"))
                    {
                        iTotalCarCount = LBConverter.ToInt32(dictValue["TotalCar"]);
                    }
                }

                if (this.lblCarIn.InvokeRequired)
                {
                    this.lblCarIn.Invoke((MethodInvoker)delegate {
                        this.lblCarIn.Text = iCarCount.ToString();
                    });
                }
                else
                {
                    this.lblCarIn.Text = iCarCount.ToString();
                }
                if (this.lblSalesWeight.InvokeRequired)
                {
                    this.lblSalesWeight.Invoke((MethodInvoker)delegate {
                        this.lblSalesWeight.Text = decWeight.ToString("0.00");
                    });
                }
                else
                {
                    this.lblSalesWeight.Text = decWeight.ToString("0.00");
                }
                if (this.lblAllCar.InvokeRequired)
                {
                    this.lblAllCar.Invoke((MethodInvoker)delegate {
                        this.lblAllCar.Text = iTotalCarCount.ToString();
                    });
                }
                else
                {
                    this.lblAllCar.Text = iTotalCarCount.ToString();
                }
                //this.lblCarIn.Text = iCarCount.ToString();
                //this.lblSalesWeight.Text = decWeight.ToString("0.00");
                //this.lblAllCar.Text = iTotalCarCount.ToString();
            }
            catch (Exception ex)
            {
                //LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

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
                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("该用户没有充值权限，是否切换其他用户进行充值？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        #region -- 摄像头 --

        private DataTable _DTCamera = null;
        Dictionary<ViewCamera, Panel> dictCamera = new Dictionary<ViewCamera, Panel>();
        Dictionary<ViewCamera, bool> dictIsOpen = new Dictionary<ViewCamera, bool>();
        Dictionary<ViewCamera, CameraConfig> dictCameraSet = new Dictionary<ViewCamera, CameraConfig>();

        private void InitCameraPanel()
        {
            //读取启用了摄像头的数量
            _DTCamera = ExecuteSQL.CallView(122, "", "MachineName='" + LoginInfo.MachineName + "'", "");
            if (_DTCamera.Rows.Count > 0)
            {
                bool bolUseCamera1 = false;
                bool bolUseCamera2 = false;
                bool bolUseCamera3 = false;
                bool bolUseCamera4 = false;

                DataRow dr = _DTCamera.Rows[0];
                bolUseCamera1 = LBConverter.ToBoolean(dr["UseCamera1"]);
                bolUseCamera2 = LBConverter.ToBoolean(dr["UseCamera2"]);
                bolUseCamera3 = LBConverter.ToBoolean(dr["UseCamera3"]);
                bolUseCamera4 = LBConverter.ToBoolean(dr["UseCamera4"]);

                int iUseCount = 0;
                iUseCount += bolUseCamera1 ? 1 : 0;
                iUseCount += bolUseCamera2 ? 1 : 0;
                iUseCount += bolUseCamera3? 1 : 0;
                iUseCount += bolUseCamera4 ? 1 : 0;

                if (bolUseCamera1)
                {
                    Panel pnlCamera = new Panel();
                    pnlCamera.Dock = DockStyle.Top;
                    pnlCamera.Height = this.pnlRight.Height / iUseCount;
                    this.pnlRight.Controls.Add(pnlCamera);

                    ViewCamera viewCamera1 = new ViewCamera();
                    viewCamera1.Account = "";
                    viewCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    viewCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
                    viewCamera1.IPAddress = "";
                    viewCamera1.Name = "viewCamera1";
                    viewCamera1.Password = "";
                    viewCamera1.Port = 0;
                    viewCamera1.MouseDoubleClick += ViewCamera_MouseDoubleClick;
                    frmShowMaxCameral frmCamera1 = new frmShowMaxCameral(viewCamera1);
                    frmCamera1.TopLevel = false; // 不是最顶层窗体
                    frmCamera1.ControlBox = false;
                    frmCamera1.FormBorderStyle = FormBorderStyle.None;
                    frmCamera1.Dock = DockStyle.Fill;
                    pnlCamera.Controls.Add(frmCamera1);
                    frmCamera1.Show();

                    dictCamera.Add(viewCamera1,pnlCamera );
                    dictIsOpen.Add(viewCamera1, false);

                    CameraConfig camera = new CameraConfig(
                        dr["IPAddress1"].ToString().TrimEnd(),
                        LBConverter.ToInt32(dr["Port1"]),
                        dr["Account1"].ToString().TrimEnd(),
                        dr["Password1"].ToString().TrimEnd());
                    dictCameraSet.Add(viewCamera1, camera);
                }

                if (bolUseCamera2)
                {
                    Panel pnlCamera = new Panel();
                    pnlCamera.Dock = DockStyle.Top;
                    pnlCamera.Height = this.pnlRight.Height / iUseCount;
                    this.pnlRight.Controls.Add(pnlCamera);

                    ViewCamera viewCamera1 = new ViewCamera();
                    viewCamera1.Account = "";
                    viewCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    viewCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
                    viewCamera1.IPAddress = "";
                    viewCamera1.Name = "viewCamera2";
                    viewCamera1.Password = "";
                    viewCamera1.Port = 0;
                    viewCamera1.MouseDoubleClick += ViewCamera_MouseDoubleClick;
                    frmShowMaxCameral frmCamera1 = new frmShowMaxCameral(viewCamera1);
                    frmCamera1.TopLevel = false; // 不是最顶层窗体
                    frmCamera1.ControlBox = false;
                    frmCamera1.FormBorderStyle = FormBorderStyle.None;
                    frmCamera1.Dock = DockStyle.Fill;
                    pnlCamera.Controls.Add(frmCamera1);
                    frmCamera1.Show();

                    dictCamera.Add(viewCamera1, pnlCamera);
                    dictIsOpen.Add(viewCamera1, false);

                    CameraConfig camera = new CameraConfig(
                        dr["IPAddress2"].ToString().TrimEnd(),
                        LBConverter.ToInt32(dr["Port2"]),
                        dr["Account2"].ToString().TrimEnd(),
                        dr["Password2"].ToString().TrimEnd());
                    dictCameraSet.Add(viewCamera1, camera);
                }

                if (bolUseCamera3)
                {
                    Panel pnlCamera = new Panel();
                    pnlCamera.Dock = DockStyle.Top;
                    pnlCamera.Height = this.pnlRight.Height / iUseCount;
                    this.pnlRight.Controls.Add(pnlCamera);

                    ViewCamera viewCamera1 = new ViewCamera();
                    viewCamera1.Account = "";
                    viewCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    viewCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
                    viewCamera1.IPAddress = "";
                    viewCamera1.Name = "viewCamera3";
                    viewCamera1.Password = "";
                    viewCamera1.Port = 0;
                    viewCamera1.MouseDoubleClick += ViewCamera_MouseDoubleClick;
                    frmShowMaxCameral frmCamera1 = new frmShowMaxCameral(viewCamera1);
                    frmCamera1.TopLevel = false; // 不是最顶层窗体
                    frmCamera1.ControlBox = false;
                    frmCamera1.FormBorderStyle = FormBorderStyle.None;
                    frmCamera1.Dock = DockStyle.Fill;
                    pnlCamera.Controls.Add(frmCamera1);
                    frmCamera1.Show();

                    dictCamera.Add(viewCamera1, pnlCamera);
                    dictIsOpen.Add(viewCamera1, false);

                    CameraConfig camera = new CameraConfig(
                        dr["IPAddress3"].ToString().TrimEnd(),
                        LBConverter.ToInt32(dr["Port3"]),
                        dr["Account3"].ToString().TrimEnd(),
                        dr["Password3"].ToString().TrimEnd());
                    dictCameraSet.Add(viewCamera1, camera);
                }
                
                if (bolUseCamera4)
                {
                    Panel pnlCamera = new Panel();
                    pnlCamera.Dock = DockStyle.Top;
                    pnlCamera.Height = this.pnlRight.Height / iUseCount;
                    this.pnlRight.Controls.Add(pnlCamera);

                    ViewCamera viewCamera1 = new ViewCamera();
                    viewCamera1.Account = "";
                    viewCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    viewCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
                    viewCamera1.IPAddress = "";
                    viewCamera1.Name = "viewCamera4";
                    viewCamera1.Password = "";
                    viewCamera1.Port = 0;
                    viewCamera1.MouseDoubleClick += ViewCamera_MouseDoubleClick;
                    frmShowMaxCameral frmCamera1 = new frmShowMaxCameral(viewCamera1);
                    frmCamera1.TopLevel = false; // 不是最顶层窗体
                    frmCamera1.ControlBox = false;
                    frmCamera1.FormBorderStyle = FormBorderStyle.None;
                    frmCamera1.Dock = DockStyle.Fill;
                    pnlCamera.Controls.Add(frmCamera1);
                    frmCamera1.Show();

                    dictCamera.Add(viewCamera1, pnlCamera);
                    dictIsOpen.Add(viewCamera1, false);

                    CameraConfig camera = new CameraConfig(
                        dr["IPAddress4"].ToString().TrimEnd(),
                        LBConverter.ToInt32(dr["Port4"]),
                        dr["Account4"].ToString().TrimEnd(),
                        dr["Password4"].ToString().TrimEnd());
                    dictCameraSet.Add(viewCamera1, camera);
                }
            }
        }

        private void MTimerCamera_Tick(object sender, EventArgs e)
        {
            try
            {
                mTimerCamera.Interval = 30000;
                List<ViewCamera> lstConnectedPnl = new List<ViewCamera>();
                foreach(KeyValuePair<ViewCamera, bool> keyvalue in dictIsOpen)
                {
                    if (!keyvalue.Value)
                    {
                        string strIPAddress1 = dictCameraSet[keyvalue.Key].Address;
                        int iPort1 = dictCameraSet[keyvalue.Key].Port;
                        PingCamera ping = new PingCamera();
                        bool bolConnected = ping.Connect(strIPAddress1, iPort1, 200);
                        if (bolConnected)
                        {
                            lstConnectedPnl.Add(keyvalue.Key);
                            keyvalue.Key.IPAddress = strIPAddress1;
                            keyvalue.Key.Port = iPort1;
                            keyvalue.Key.Account = dictCameraSet[keyvalue.Key].Account;
                            keyvalue.Key.Password = dictCameraSet[keyvalue.Key].Password;
                            keyvalue.Key.OpenCamera(1);
                        }
                    }
                }
                
                foreach(ViewCamera vc in lstConnectedPnl)
                {
                    dictIsOpen[vc] = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ViewCamera_MouseDoubleClick(object sender, EventArgs e)
        {
            ViewCamera vc = (ViewCamera)sender;
            Panel pnl = dictCamera[vc];
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

            Panel pnl = dictCamera[vc];

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

        #endregion

        #region -- 工具栏按钮事件 --

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerEdit frmCustomer = new frmCustomerEdit(0);
                LBShowForm.ShowDialog(frmCustomer);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCustomerManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerManager frmCustomer = new frmCustomerManager();
                frmCustomer.PageAutoSize = true;
                LBShowForm.ShowDialog(frmCustomer);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarEdit frmCar = new frmCarEdit(0, 0);
                LBShowForm.ShowDialog(frmCar);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCarQuery_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarWeightManager frmCar = new frmCarWeightManager();
                frmCar.PageAutoSize = true;
                LBShowForm.ShowDialog(frmCar);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnRPReceive_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditReceiveBill frmReceive = new frmEditReceiveBill(0);
                LBShowForm.ShowDialog(frmReceive);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnRPReceiveList_Click(object sender, EventArgs e)
        {
            try
            {
                frmReceiveBillQuery frmQuery = new frmReceiveBillQuery();
                frmQuery.PageAutoSize = true;
                LBShowForm.ShowDialog(frmQuery);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnItemBaseManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemBaseManager frmItemBaseMag = new frmItemBaseManager();
                frmItemBaseMag.PageAutoSize = true;
                LBShowForm.ShowMainPage(frmItemBaseMag);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddChangePriceBill_Click(object sender, EventArgs e)
        {
            try
            {
                frmModifyBillHeaderEdit frm = new frmModifyBillHeaderEdit(0);
                frm.PageAutoSize = true;
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnChangePriceManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmModifyBillHeaderQuery frmModify = new frmModifyBillHeaderQuery();
                frmModify.PageAutoSize = true;
                LBShowForm.ShowDialog(frmModify);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 工具栏按钮事件 --

        #region -- 根据权限设置按钮的只读属性 --

        private void SetButtonReadOnlyByPermission()
        {
            this.btnTotalWeight.Enabled = LBPermission.GetUserPermission("WeightSalesOutBill_TotalValue");
            this.btnReadTareWeight.Enabled = LBPermission.GetUserPermission("WeightSalesOutBill_TareValue");
            this.btnSaveAndPrint.Enabled = LBPermission.GetUserPermission("WeightSalesOutBill_Save");
            this.btnSaveTareWeight.Enabled = LBPermission.GetUserPermission("WeightSalesOutBill_SaveTare");
            this.btnLoadTareWeight.Enabled = LBPermission.GetUserPermission("WeightSalesOutBill_ReadTare");
            this.btnViewSalesBill.Enabled= LBPermission.GetUserPermission("SalesManager_Query");
        }

        #endregion -- 根据权限设置按钮的只读属性 --

        #region -- 校验预付客户是否余额不足 --

        private void VerifyAmountNotEnough(long lCustomerID)
        {
            using (DataTable dtCustomer = ExecuteSQL.CallView(112, "AmountNotEnough,ReceiveType,TotalReceivedAmount,SalesReceivedAmount", "CustomerID=" + lCustomerID.ToString(), ""))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    decimal decTotalReceivedAmount = LBConverter.ToDecimal(dtCustomer.Rows[0]["TotalReceivedAmount"]);
                    decimal decSalesReceivedAmount = LBConverter.ToDecimal(dtCustomer.Rows[0]["SalesReceivedAmount"]);
                    decimal decAmountNotEnough = LBConverter.ToDecimal(dtCustomer.Rows[0]["AmountNotEnough"]);//预警值
                    int iReceiveType = LBConverter.ToInt32(dtCustomer.Rows[0]["ReceiveType"]);

                    if (iReceiveType == 1)//预付客户需要校验余额是否低于预警值
                    {
                        decimal decWarmAmount = 0;
                        if (decAmountNotEnough > 0)
                        {
                            decWarmAmount = decAmountNotEnough;
                        }
                        else
                        {
                            //预付客户充值余额预警值
                            int iAmountNotEnough;
                            SysConfigValue.GetSysConfig("AmountNotEnough", out iAmountNotEnough);
                            decWarmAmount = iAmountNotEnough;
                        }
                        if (decWarmAmount > 0)
                        {
                            if (decTotalReceivedAmount - decSalesReceivedAmount < decWarmAmount)
                            {
                                LB.WinFunction.LBCommonHelper.ShowCommonMessage("该客户余额低于" + decWarmAmount.ToString("0") + "元，请通知客户及时充值！");
                            }
                        }
                    }
                }
            }
        }

        #endregion

        ////frmReceiveSerialData _frmReceiveData = null;
        private void lblWeight_Click(object sender, EventArgs e)
        {
            //if (_frmReceiveData == null)
            //{
            //    _frmReceiveData = new MainForm.frmReceiveSerialData();
            //    _frmReceiveData.FormClosed += _frmReceiveData_FormClosed;
            //}
            //_frmReceiveData.ShowDialog();

        }

        //private void _frmReceiveData_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (_frmReceiveData != null)
        //    {
        //        _frmReceiveData = null;
        //    }
        //}

        private void LBSerialHelper_LBSerialDataEvent(Common.Args.LBSerialDataArgs e)
        {
            try
            {
                //string strReceiveData = e.ReceiveData;
                //if (_frmReceiveData != null)
                //{
                //    _frmReceiveData.AppendReceiveData(strReceiveData);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        //同步车辆信息
        private void btnSynCar_Click(object sender, EventArgs e)
        {
            frmSynchornousCarData frm = new frmSynchornousCarData();
            LBShowForm.ShowDialog(frm);
        }
        //同步客户信息
        private void btnSynCustomer_Click(object sender, EventArgs e)
        {
            frmSynchornousCustomerData frm = new frmSynchornousCustomerData();
            LBShowForm.ShowDialog(frm);
        }
        //同步单据信息
        private void btnSynSalesBill_Click(object sender, EventArgs e)
        {
            frmSaleCarInOutBillSynchornous frm = new MI.MI.frmSaleCarInOutBillSynchornous();
            LBShowForm.ShowDialog(frm);
        }

        private void btnSynPrice_Click(object sender, EventArgs e)
        {
            try
            {
                //SynchronousPrice.SynchronousClientFromServer();
                //LB.WinFunction.LBCommonHelper.ShowCommonMessage("同步成功！");
                frmWaistProcess frm = new frmWaistProcess();
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }

    internal class CameraConfig
    {
        public string Address="";
        public int Port = 0;
        public string Account = "";
        public string Password = "";

        public CameraConfig(string Address, int Port, string Account, string Password)
        {
            this.Address = Address;
            this.Port = Port;
            this.Account = Account;
            this.Password = Password;
        }
    }
}
