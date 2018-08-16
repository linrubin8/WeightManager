using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.MainForm.Permission;
using LB.Page.Helper;
using System.Threading;
using LB.Common;
using LB.SysConfig;
using LB.WinFunction;
using LB.RPReceive;
using LB.MI;
using LB.RPReceive.RPReceive;
using LB.MI.MI;
using LB.SysConfig.SysConfig;

namespace LB.MainForm
{
    public partial class MasterMainForm : LBForm
    {
        private Thread mThreadStatus;
        public bool bolIsCancel = false;
        private bool bolIsClosing = false;
        public MasterMainForm()
        {
            InitializeComponent();
            LBShowForm.LBUIPageBaseAdded += LBShowForm_LBUIPageBaseAdded;
            this.tcMain.TabPageClosingEvent += TcMain_TabPageClosingEvent;
            this.tcMain.TabPageClosedEvent += TcMain_TabPageClosedEvent;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!LBRegisterPermission.Permission_ModelSynchorToServer)
            {
                btnSynchornousData.Visible = false;
            }

            InitData();

            mThreadStatus = new Thread(TestServerConnectStatus);
            mThreadStatus.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认退出系统？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;   
            }

            if (this.mThreadStatus.ThreadState != ThreadState.Stopped ||
            this.mThreadStatus.ThreadState != ThreadState.StopRequested)
            {
                bolIsClosing = true;
                this.mThreadStatus.Abort();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        #region -- 按钮事件  --
        //注销
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                bolIsCancel = true;
                LBShowForm.LBUIPageBaseAdded -= LBShowForm_LBUIPageBaseAdded;
                //this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //修改密码
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

        //用户权限管理
        private void btnUserManager_Click(object sender, EventArgs e)
        {
            try
            {
                SysConfig.frmUserManager frmUserMag = new SysConfig.frmUserManager();
                LBShowForm.ShowMainPage(frmUserMag);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //视图配置
        private void btnViewConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmDevelopViewConfig frmView = new frmDevelopViewConfig();
                LBShowForm.ShowMainPage(frmView);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnExportConfigSQL_Click(object sender, EventArgs e)
        {
            try
            {
                ExportSQLConfig.ExportAction();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //权限配置
        private void btnPermissionConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmPermissionConfig frmView = new frmPermissionConfig();
                LBShowForm.ShowMainPage(frmView);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDbSysConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmSysConfig frmConfig = new frmSysConfig();
                LBShowForm.ShowDialog(frmConfig);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSQLBuilder_Click(object sender, EventArgs e)
        {
            try
            {
                frmSQLScriptBuilder frmView = new frmSQLScriptBuilder();
                frmView.ShowDialog();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //操作日志管理
        private void btnLogManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmLogManager frmLog = new frmLogManager();
                LBShowForm.ShowMainPage(frmLog);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //备份设置
        private void btnDBBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                frmBackUpConfig frmBackUp = new frmBackUpConfig();
                LBShowForm.ShowMainPage(frmBackUp);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //充值
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

        //充值记录
        private void btnRPReceiveList_Click(object sender, EventArgs e)
        {
            try
            {
                frmReceiveBillQuery frmQuery = new frmReceiveBillQuery();
                LBShowForm.ShowMainPage(frmQuery);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //物料管理
        private void btnItemBaseManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemBaseManager frmItemBaseMag = new frmItemBaseManager();
                LBShowForm.ShowMainPage(frmItemBaseMag);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //计量单位管理
        private void btnUOMManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmUOMManager frm = new frmUOMManager();
                LBShowForm.ShowMainPage(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //客户资料管理
        private void btnCustomerManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerManager frmCustomer = new frmCustomerManager();
                LBShowForm.ShowMainPage(frmCustomer);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //添加车辆
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

        //车辆管理
        private void btnCarQuery_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarWeightManager frmCar = new frmCarWeightManager();
                LBShowForm.ShowMainPage(frmCar);
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
                LBShowForm.ShowMainPage(frmModify);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
            
        }

        //添加客户
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerEdit frmCustomer = new frmCustomerEdit( 0);
                LBShowForm.ShowDialog(frmCustomer);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        //备注管理
        private void btnDescriptionManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmDescriptionManager frm = new frmDescriptionManager();
                LBShowForm.ShowMainPage(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnWeightDevice_Click(object sender, EventArgs e)
        {
            try
            {
                frmWeightDecive frmDevice = new frmWeightDecive();
                LBShowForm.ShowMainPage(frmDevice);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCameraDevice_Click(object sender, EventArgs e)
        {
            try
            {
                frmCameraConfig frmDevice = new frmCameraConfig();
                LBShowForm.ShowMainPage(frmDevice);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCarWeightManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarWeightManager frmDevice = new frmCarWeightManager();
                LBShowForm.ShowMainPage(frmDevice);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        private void btnSaleInOutManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleCarInOutBillManager frmBill = new frmSaleCarInOutBillManager();
                LBShowForm.ShowMainPage(frmBill);
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


        private void btnReportManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportView frmBill = new frmReportView();
                LBShowForm.ShowMainPage(frmBill);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnBankManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmBankManager frmBank = new frmBankManager();
                LBShowForm.ShowMainPage(frmBank);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnChargeManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmChargeTypeManager frm = new frmChargeTypeManager();
                LBShowForm.ShowMainPage(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }


        //同步车辆信息
        private void btnSynCar_Click(object sender, EventArgs e)
        {
            frmSynchornousCarData frm = new frmSynchornousCarData();
            LBShowForm.ShowMainPage(frm);
        }
        //同步客户信息
        private void btnSynCustomer_Click(object sender, EventArgs e)
        {
            frmSynchornousCustomerData frm = new frmSynchornousCustomerData();
            LBShowForm.ShowMainPage(frm);
        }
        //同步单据信息
        private void btnSynSalesBill_Click(object sender, EventArgs e)
        {
            frmSaleCarInOutBillSynchornous frm = new MI.MI.frmSaleCarInOutBillSynchornous();
            LBShowForm.ShowMainPage(frm);
        }
        
        private void btnSynPrice_Click(object sender, EventArgs e)
        {
            try
            {
                frmWaistProcess frm = new frmWaistProcess();
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnSaleInOutSynK3Receive_Click(object sender, EventArgs e)
        {
            frmSaleCarInOutBillManagerSynK3 frm = new frmSaleCarInOutBillManagerSynK3(0);
            LBShowForm.ShowMainPage(frm);
        }
        
        private void btnSaleInOutSynK3OutBill_Click(object sender, EventArgs e)
        {
            frmSaleCarInOutBillManagerSynK3 frm = new frmSaleCarInOutBillManagerSynK3(1);
            LBShowForm.ShowMainPage(frm);
        }
        #endregion -- 按钮事件  --

        #region -- ShowMainPage --

        private void LBShowForm_LBUIPageBaseAdded(object sender, EventArgs e)
        {
            int iMaxIndex = 0;
            foreach(DMSkin.Metro.Controls.MetroTabPage tp in this.tcMain.TabPages)
            {
                if(tp.TabIndex> iMaxIndex)
                {
                    iMaxIndex = tp.TabIndex;
                }
            }
            LBUIPageBase Uipagebase = sender as LBUIPageBase;
            Uipagebase.FormClosed += Uipagebase_FormClosed;
            
            string strTabTitle= Uipagebase.LBPageTitle == "" ? Uipagebase.Name : Uipagebase.LBPageTitle;
            DMSkin.Metro.Controls.MetroTabPage tpTp1 = new DMSkin.Metro.Controls.MetroTabPage();
            tpTp1.Name = Uipagebase.Name;
            tpTp1.BackColor = System.Drawing.Color.White;
            //tpTp1.Dock = System.Windows.Forms.DockStyle.Fill;
            Graphics g = this.CreateGraphics();
            SizeF size = g.MeasureString(strTabTitle, this.tcMain.Font);
            tpTp1.Size = new System.Drawing.Size((int)size.Width+20, 316);
            tpTp1.TabIndex = iMaxIndex+1;
            tpTp1.Text = Uipagebase.LBPageTitle==""? Uipagebase.Name: Uipagebase.LBPageTitle;
            tpTp1.Tag = Uipagebase;
            this.tcMain.TabPages.Add(tpTp1);

            Uipagebase.Size = new Size(tpTp1.Width, tpTp1.Height);
            Uipagebase.Location = new Point(1, 1);
            tpTp1.Controls.Add(Uipagebase);
            
            this.tcMain.SelectedTab = tpTp1;
            tpTp1.Invalidate();
            tpTp1.SizeChanged += TpTp1_SizeChanged;
            Uipagebase.Invalidate();
            Uipagebase.RefTabPage = tpTp1;
        }

        private void TpTp1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                DMSkin.Metro.Controls.MetroTabPage tpCurrent = sender as DMSkin.Metro.Controls.MetroTabPage;
                
                if (tpCurrent.Tag is LBUIPageBase)
                {
                    LBUIPageBase pageBase = tpCurrent.Tag as LBUIPageBase;
                    pageBase.Size = new Size(tpCurrent.Width, tpCurrent.Height);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void Uipagebase_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                LBUIPageBase Uipagebase = sender as LBUIPageBase;
                if (Uipagebase.RefTabPage != null)
                {
                    if (this.tcMain.TabPages.Contains(Uipagebase.RefTabPage))
                    {
                        Uipagebase.Dispose();
                        this.tcMain.TabPages.Remove(Uipagebase.RefTabPage);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void TcMain_TabPageClosedEvent(object sender, Controls.Args.TabPageClosedEventArgs e)
        {
            try
            {
                TabPage tpCurrent = e.TabPageClose as TabPage;
                if (tpCurrent.Tag is LBUIPageBase)
                {
                    LBUIPageBase pageBase = tpCurrent.Tag as LBUIPageBase;
                    //pageBase.Dispose();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
            
        }

        private void TcMain_TabPageClosingEvent(object sender, Controls.Args.TabPageClosingEventArgs e)
        {
            try
            {
                TabPage tpCurrent = e.TabPageClose as TabPage;
                if (tpCurrent.Tag is LBUIPageBase)
                {
                    LBUIPageBase pageBase = tpCurrent.Tag as LBUIPageBase;
                    bool bolCancel = false;
                    pageBase.StartClose(out bolCancel);
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
            SetLoginStatus();//设置状态栏信息
            LBLog.AssemblyStart();
        }

        #endregion

        #region -- 状态栏信息 --

        private void SetLoginStatus()
        {
            this.lblLoginTime.Text = LoginInfo.LoginTime.ToString("yyyy-MM-dd HH:mm");
            this.lblLoginName.Text = LoginInfo.LoginName;
        }

        private delegate void setRichTexBox(string s);
        private void TestServerConnectStatus()
        {
            while (true)
            {
                if (bolIsClosing)
                {
                    break;
                }
                try
                {
                    bool bolConnected = ExecuteSQL.TestConnectStatus();
                    if (bolConnected)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            this.lblConnectStatus.Text = "正常";
                            this.lblConnectStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate {
                            this.lblConnectStatus.Text = "异常";
                            this.lblConnectStatus.ForeColor = Color.Red;
                        });
                    }
                }
                catch (Exception ex)
                {
                    if (bolIsClosing)
                    {
                        break;
                    }
                    this.Invoke((MethodInvoker)delegate {
                        this.lblConnectStatus.Text = "异常";
                        this.lblConnectStatus.ForeColor = Color.Red;
                    });
                }
                finally
                {
                    Thread.Sleep(1000 * 2);
                }
            }
        }



        #endregion

        private void 导入客户余额ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
    }
}
