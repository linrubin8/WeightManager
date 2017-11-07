using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

namespace LB.Web.ServiceMonitor
{
	public partial class frmServerForm : Form
	{
		private const string MC_strAppTitle = "LB系统服务";
		private bool mbIsWithUpdateService = false;
		private bool mbIsWithDBAutoBackUp = false;

		private bool mbCloseFromNotifyIcon = false;
		private delegate void DelegateShowStatusOnLoad();

		private string ServiceName4WebAutoUpdate
		{
			get
			{
				return LB.RMT.Base.ConfigHelper.ServiceName + "_WebAutoUpdate";
			}
		}

		private string ServiceName4DBAutoBackUp
		{
			get
			{
				return LB.RMT.Base.ConfigHelper.ServiceName + "_DBAutoBackUp";
			}
		}

		public frmServerForm()
		{
			InitializeComponent();

            this.Text = MC_strAppTitle;
			this.notifyIconTS.Text = MC_strAppTitle;
		}

		private void ServerForm_Load( object sender, EventArgs e )
		{
			try
			{
                // 读取 remoting 相关的配置
                LB.RMT.Base.ConfigHelper.ReadIniConfig();
                // 显示 URL
                txtServerURL.Text ="tcp://"+ LB.RMT.Base.ConfigHelper.ServiceURL+"/LBRMT";
				txtName.Text = LB.RMT.Base.ConfigHelper.ServiceName;

				// 自动更新
				//txtName4WebAutoUpdate.Text = ServiceName4WebAutoUpdate;
				//mbIsWithUpdateService = LB.Web.Base.ConfigHelper.WithUpdateService;

				//if( !mbIsWithUpdateService )
				{
					tcService.TabPages.Remove( tpWebAutoUpdate );
					itemWebAutoUpdate.Visible = false;
				}

				// 自动备份服务
				//txtName4DBAutoBackUp.Text = ServiceName4DBAutoBackUp;
				//mbIsWithDBAutoBackUp = LB.Web.Base.ConfigHelper.WithBackupService;
				//if( !mbIsWithDBAutoBackUp )
				{
					tcService.TabPages.Remove( tpDBAutoBackUp );
					itemDBAutoBackUp.Visible = false;
				}

                // 网上订单配置按钮
                //if( TS.DW.Setting.SettingDBType.DisplayNETORDER )
                //{
                //    btnWebLink.Visible = true;
                //    btnConfig.Location = new Point( 110, btnConfig.Location.Y );
                //}

				// 定时刷新状态
				timerStatus.Enabled = true;
				timerStatus.Start();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void ServerForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			try
			{
				if( !mbCloseFromNotifyIcon && e.CloseReason == CloseReason.UserClosing )
				{
					this.ShowInTaskbar = false;
					this.Hide();
					e.Cancel = true;
				}
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnStart_Click( object sender, EventArgs e )
		{
			try
			{
				Start();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		internal void Start()
		{
			using( ServiceController controller = new ServiceController( LB.RMT.Base.ConfigHelper.ServiceName ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
				{
					controller.Start();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus( status, true );
				}
			}
		}

		private void btnStop_Click( object sender, EventArgs e )
		{
			try
			{
				Stop();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		internal void Stop()
		{
			using( ServiceController controller = new ServiceController( LB.RMT.Base.ConfigHelper.ServiceName ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Running )
				{
					controller.Stop();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus( status, true );
				}
			}
		}

		private void timerStatus_Tick( object sender, EventArgs e )
		{
			try
			{
				RefreshStatus();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}

			try
			{
				RefreshStatus4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}

			try
			{
				RefreshStatus4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}


		private void btnRefresh_Click( object sender, EventArgs e )
		{
			try
			{
				RefreshStatus();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void RefreshStatus()
		{
			string strName = LB.RMT.Base.ConfigHelper.ServiceName;

			try
			{
				ServiceControllerStatus status = ServiceControllerStatus.Stopped;
				bool bServiceBinded = true;

				try
				{
					using( ServiceController controller = new ServiceController( strName ) )
					{
						controller.Refresh();
						status = controller.Status;
					}
				}
				catch
				{
					bServiceBinded = false;
				}

				DisplayStatus( status, bServiceBinded );
			}
			catch( Exception ex )
			{
				this.timerStatus.Enabled = false;

				txtStatus.Text = "服务已停止";
				this.notifyIconTS.Text = strName + " －已停止";
				btnStart.Enabled = itemStart.Enabled = true;
				btnStop.Enabled = itemStop.Enabled = false;

				throw ex;
			}
		}

		private void DisplayStatus( ServiceControllerStatus status, bool bServiceBinded )
		{
			string strName = LB.RMT.Base.ConfigHelper.ServiceName;

			// 显示名称
			txtServerURL.Text = LB.RMT.Base.ConfigHelper.ServiceURL;
			txtName.Text = strName;

			if( bServiceBinded == false )
			{
				picStatus.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus.Text = "无法关联服务";
				this.notifyIconTS.Text = "服务 －无法关联";
				this.Icon = this.notifyIconTS.Icon = LB.Web.Properties.Resources.ServerStopM;

				btnStart.Enabled = itemStart.Enabled = true;
				btnStop.Enabled = itemStop.Enabled = btnReStart.Enabled = itemRestart.Enabled = false;
			}
			else if( status == System.ServiceProcess.ServiceControllerStatus.Running )
			{
				picStatus.Image = LB.Web.Properties.Resources.ServerStartBig;
				txtStatus.Text = "服务正在运行中";
				this.notifyIconTS.Text = strName + " －运行中";
				this.Icon = this.notifyIconTS.Icon = LB.Web.Properties.Resources.ServerStartM;

				btnStart.Enabled = itemStart.Enabled = false;
				btnStop.Enabled = itemStop.Enabled = btnReStart.Enabled = itemRestart.Enabled = true;
			}
			else
			{
				picStatus.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus.Text = "服务已停止";
				this.notifyIconTS.Text = strName + " －已停止";
				this.Icon = this.notifyIconTS.Icon = LB.Web.Properties.Resources.ServerStopM;

				btnStart.Enabled = itemStart.Enabled = true;
				btnStop.Enabled = itemStop.Enabled = btnReStart.Enabled = itemRestart.Enabled = false;
			}
		}

		private void notifyIconTS_MouseDoubleClick( object sender, MouseEventArgs e )
		{
			try
			{
				if( e.Button == MouseButtons.Left )
				{
					this.Show();
					this.ShowInTaskbar = true;
				}
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemOpenForm_Click( object sender, EventArgs e )
		{
			try
			{
				this.Show();
				this.ShowInTaskbar = true;
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemStart_Click( object sender, EventArgs e )
		{
			try
			{
				Start();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemStop_Click( object sender, EventArgs e )
		{
			try
			{
				Stop();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemExit_Click( object sender, EventArgs e )
		{
			try
			{
				mbCloseFromNotifyIcon = true;
				this.Close();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnConfig_Click( object sender, EventArgs e )
		{
			try
			{
				using( frmRemoteConfig frm = new frmRemoteConfig( this ) )
				{
					frm.ShowDialog();
				}
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		#region--自动更新相关--

		private void RefreshStatus4WebAutoUpdate()
		{
			string strName = ServiceName4WebAutoUpdate;

			try
			{
				ServiceControllerStatus status = ServiceControllerStatus.Stopped;
				bool bServiceBinded = true;

				try
				{
					using( ServiceController controller = new ServiceController( strName ) )
					{
						controller.Refresh();
						status = controller.Status;
					}
				}
				catch
				{
					bServiceBinded = false;
				}

				DisplayStatus4WebAutoUpdate( status, bServiceBinded );
			}
			catch( Exception ex )
			{
				picStatus4WebAutoUpdate.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4WebAutoUpdate.Text = "服务已停止";

				btnStart4WebAutoUpdate.Enabled = itemStart4itemWebAutoUpdate.Enabled = true;
				btnStop4WebAutoUpdate.Enabled = itemStop4itemWebAutoUpdate.Enabled = btnReStart4WebAutoUpdate.Enabled = itemRestart4itemWebAutoUpdate.Enabled = false;

				throw ex;
			}
		}

		private void DisplayStatus4WebAutoUpdate( ServiceControllerStatus status, bool bServiceBinded )
		{
			string strName = ServiceName4WebAutoUpdate;

			// 显示名称
			txtName4WebAutoUpdate.Text = strName;

			if( bServiceBinded == false )
			{
				picStatus4WebAutoUpdate.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4WebAutoUpdate.Text = "无法关联服务";

				btnStart4WebAutoUpdate.Enabled = itemStart4itemWebAutoUpdate.Enabled = true;
				btnStop4WebAutoUpdate.Enabled = itemStop4itemWebAutoUpdate.Enabled = btnReStart4WebAutoUpdate.Enabled = itemRestart4itemWebAutoUpdate.Enabled = false;
			}
			else if( status == System.ServiceProcess.ServiceControllerStatus.Running )
			{
				picStatus4WebAutoUpdate.Image = LB.Web.Properties.Resources.ServerStartBig;
				txtStatus4WebAutoUpdate.Text = "服务正在运行中";

				btnStart4WebAutoUpdate.Enabled = itemStart4itemWebAutoUpdate.Enabled = false;
				btnStop4WebAutoUpdate.Enabled = itemStop4itemWebAutoUpdate.Enabled = btnReStart4WebAutoUpdate.Enabled = itemRestart4itemWebAutoUpdate.Enabled = true;

			}
			else
			{
				picStatus4WebAutoUpdate.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4WebAutoUpdate.Text = "服务已停止";

				btnStart4WebAutoUpdate.Enabled = itemStart4itemWebAutoUpdate.Enabled = true;
				btnStop4WebAutoUpdate.Enabled = itemStop4itemWebAutoUpdate.Enabled = btnReStart4WebAutoUpdate.Enabled = itemRestart4itemWebAutoUpdate.Enabled = false;
			}
		}

		private void Stop4WebAutoUpdate()
		{
			using( ServiceController controller = new ServiceController( ServiceName4WebAutoUpdate ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Running )
				{
					controller.Stop();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus4WebAutoUpdate( status, true );
				}
			}
		}

		private void Start4WebAutoUpdate()
		{
			using( ServiceController controller = new ServiceController( ServiceName4WebAutoUpdate ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
				{
					controller.Start();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus4WebAutoUpdate( status, true );
				}
			}
		}

		private void btnStart4WebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				Start4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnStop4WebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				Stop4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnRefresh4WebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				RefreshStatus4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemStart4itemWebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				Start4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemStop4itemWebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				Stop4WebAutoUpdate();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		#endregion--自动更新相关--

		#region 自动备份服务

		private void btnRefresh4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				RefreshStatus4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnStart4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				Start4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void btnStop4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				Stop4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void timerStatus4DBAutoBackUp_Tick( object sender, EventArgs e )
		{
			try
			{
				RefreshStatus4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void RefreshStatus4DBAutoBackUp()
		{
			string strName = ServiceName4DBAutoBackUp;

			try
			{
				ServiceControllerStatus status = ServiceControllerStatus.Stopped;
				bool bServiceBinded = true;

				try
				{
					using( ServiceController controller = new ServiceController( strName ) )
					{
						controller.Refresh();
						status = controller.Status;
					}
				}
				catch
				{
					bServiceBinded = false;
				}

				DisplayStatus4DBAutoBackUp( status, bServiceBinded );
			}
			catch( Exception ex )
			{
				picStatus4DBAutoBackUp.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4DBAutoBackUp.Text = "服务已停止";

				btnStart4DBAutoBackUp.Enabled = itemStart4DBAutoBackUp.Enabled = true;
				btnStop4DBAutoBackUp.Enabled = itemStop4DBAutoBackUp.Enabled = btnReStart4DBAutoBackUp.Enabled = itemRestart4DBAutoBackUp.Enabled = false;

				throw ex;
			}
		}

		private void DisplayStatus4DBAutoBackUp( ServiceControllerStatus status, bool bServiceBinded )
		{
			string strName = ServiceName4DBAutoBackUp;

			// 显示名称
			txtName4WebAutoUpdate.Text = strName;

			if( bServiceBinded == false )
			{
				picStatus4DBAutoBackUp.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4DBAutoBackUp.Text = "无法关联服务";

				btnStart4DBAutoBackUp.Enabled = itemStart4DBAutoBackUp.Enabled = true;
				btnStop4DBAutoBackUp.Enabled = itemStop4DBAutoBackUp.Enabled = btnReStart4DBAutoBackUp.Enabled = itemRestart4DBAutoBackUp.Enabled = false;
			}
			else if( status == System.ServiceProcess.ServiceControllerStatus.Running )
			{
				picStatus4DBAutoBackUp.Image = LB.Web.Properties.Resources.ServerStartBig;
				txtStatus4DBAutoBackUp.Text = "服务正在运行中";

				btnStart4DBAutoBackUp.Enabled = itemStart4DBAutoBackUp.Enabled = false;
				btnStop4DBAutoBackUp.Enabled = itemStop4DBAutoBackUp.Enabled = btnReStart4DBAutoBackUp.Enabled = itemRestart4DBAutoBackUp.Enabled = true;
			}
			else
			{
				picStatus4DBAutoBackUp.Image = LB.Web.Properties.Resources.ServerStopBig;
				txtStatus4DBAutoBackUp.Text = "服务已停止";

				btnStart4DBAutoBackUp.Enabled = itemStart4DBAutoBackUp.Enabled = true;
				btnStop4DBAutoBackUp.Enabled = itemStop4DBAutoBackUp.Enabled = btnReStart4DBAutoBackUp.Enabled = itemRestart4DBAutoBackUp.Enabled = false;
			}
		}

		private void Stop4DBAutoBackUp()
		{
			using( ServiceController controller = new ServiceController( ServiceName4DBAutoBackUp ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Running )
				{
					controller.Stop();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus4DBAutoBackUp( status, true );
				}
			}
		}

		private void Start4DBAutoBackUp()
		{
			using( ServiceController controller = new ServiceController( ServiceName4DBAutoBackUp ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
				{
					controller.Start();
					controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
					status = controller.Status;
					DisplayStatus4DBAutoBackUp( status, true );
				}
			}
		}

		private void itemStart4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				Start4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		private void itemStop4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				Stop4DBAutoBackUp();
			}
			catch( Exception err )
			{
				MonitorHelper.DealWithError( err );
			}
		}

		#endregion 自动备份服务

		#region 重启服务

		private Timer timerReStart = null;
		private Timer timerReStart4WebAutoUpdate = null;
		private Timer timerReStart4DBAutoBackUp = null;

		private bool mbRS = false;
		private bool mbRS4WebAutoUpdate = false;
		private bool mbRS4DBAutoBackUp = false;

		private void btnReStart_Click( object sender, EventArgs e )
		{
			try
			{
				btnReStart.Enabled = btnStart.Enabled = btnStop.Enabled = false;

				if( timerReStart == null )
				{
					timerReStart = new Timer();
					timerReStart.Interval = 500;
					timerReStart.Tick += timerReStart_Tick;
				}
				if( !timerReStart.Enabled && !mbRS )
				{
					//miIndexRS = 0;
					//mbRS = false;
					timerReStart.Start();
				}
			}
			catch( Exception ex )
			{
				MonitorHelper.DealWithError( ex );
			}
		}

		private void timerReStart_Tick( object sender, EventArgs e )
		{
			using( ServiceController controller = new ServiceController( LB.RMT.Base.ConfigHelper.ServiceName ) )
			{
				controller.Refresh();

				try
				{
					timerReStart.Stop();

					if( mbRS )
						return;

					mbRS = true;

					ServiceControllerStatus status = controller.Status;
					if( status == System.ServiceProcess.ServiceControllerStatus.Running )
					{
						controller.Stop();
						controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
						status = controller.Status;
						DisplayStatus( status, true );
					}

					this.Update();

					int iCount = 0;
					bool bFlag = false;
					while( iCount <= 120 )
					{
						if( controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							bFlag = true;
							break;
						}

						System.Threading.Thread.Sleep( 500 );

						iCount++;
					}

					if( bFlag )
					{
						status = controller.Status;
						if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							controller.Start();
							controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
							status = controller.Status;
							DisplayStatus( status, true );
						}
					}
				}
				catch( Exception err )
				{
					MonitorHelper.DealWithError( err );
				}
				finally
				{
					mbRS = false;
					try
					{
						btnReStart.Enabled = btnStop.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Running;
						btnStart.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped;
					}
					catch
					{
					}
				}
			}
		}

		private void btnReStart4WebAutoUpdate_Click( object sender, EventArgs e )
		{
			try
			{
				btnReStart4WebAutoUpdate.Enabled = btnStart4WebAutoUpdate.Enabled = btnStop4WebAutoUpdate.Enabled = false;

				if( timerReStart4WebAutoUpdate == null )
				{
					timerReStart4WebAutoUpdate = new Timer();
					timerReStart4WebAutoUpdate.Interval = 500;
					timerReStart4WebAutoUpdate.Tick += timerReStart4WebAutoUpdate_Tick;
				}
				if( !timerReStart4WebAutoUpdate.Enabled && !mbRS4WebAutoUpdate )
				{
					timerReStart4WebAutoUpdate.Start();
				}
			}
			catch( Exception ex )
			{
				MonitorHelper.DealWithError( ex );
			}
		}

		private void timerReStart4WebAutoUpdate_Tick( object sender, EventArgs e )
		{
			using( ServiceController controller = new ServiceController( ServiceName4WebAutoUpdate ) )
			{
				controller.Refresh();

				try
				{
					timerReStart4WebAutoUpdate.Stop();

					if( mbRS4WebAutoUpdate )
						return;

					mbRS4WebAutoUpdate = true;

					ServiceControllerStatus status = controller.Status;
					if( status == System.ServiceProcess.ServiceControllerStatus.Running )
					{
						controller.Stop();
						controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
						status = controller.Status;
						DisplayStatus4WebAutoUpdate( status, true );
					}

					this.Update();

					int iCount = 0;
					bool bFlag = false;
					while( iCount <= 120 )
					{
						if( controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							bFlag = true;
							break;
						}

						System.Threading.Thread.Sleep( 500 );

						iCount++;
					}

					if( bFlag )
					{
						status = controller.Status;
						if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							controller.Start();
							controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
							status = controller.Status;
							DisplayStatus4WebAutoUpdate( status, true );
						}
					}
				}
				catch( Exception err )
				{
					MonitorHelper.DealWithError( err );
				}
				finally
				{
					try
					{
						mbRS4WebAutoUpdate = false;
						btnReStart4WebAutoUpdate.Enabled = btnStop4WebAutoUpdate.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Running;
						btnStart4WebAutoUpdate.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped;
					}
					catch
					{
					}
				}
			}
		}

		private void btnReStart4DBAutoBackUp_Click( object sender, EventArgs e )
		{
			try
			{
				btnReStart4DBAutoBackUp.Enabled = btnStart4DBAutoBackUp.Enabled = btnStop4DBAutoBackUp.Enabled = false;

				if( timerReStart4DBAutoBackUp == null )
				{
					timerReStart4DBAutoBackUp = new Timer();
					timerReStart4DBAutoBackUp.Interval = 500;
					timerReStart4DBAutoBackUp.Tick += timerReStart4DBAutoBackUp_Tick;
				}
				if( !timerReStart4DBAutoBackUp.Enabled && !mbRS4DBAutoBackUp )
				{
					timerReStart4DBAutoBackUp.Start();
				}
			}
			catch( Exception ex )
			{
				MonitorHelper.DealWithError( ex );
			}
		}

		private void timerReStart4DBAutoBackUp_Tick( object sender, EventArgs e )
		{
			using( ServiceController controller = new ServiceController( ServiceName4DBAutoBackUp ) )
			{
				controller.Refresh();

				try
				{
					timerReStart4DBAutoBackUp.Stop();

					if( mbRS4DBAutoBackUp )
						return;

					mbRS4DBAutoBackUp = true;

					ServiceControllerStatus status = controller.Status;
					if( status == System.ServiceProcess.ServiceControllerStatus.Running )
					{
						controller.Stop();
						controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Stopped, new TimeSpan( 0, 1, 0 ) );
						status = controller.Status;
						DisplayStatus4DBAutoBackUp( status, true );
					}

					this.Update();

					int iCount = 0;
					bool bFlag = false;
					while( iCount <= 120 )
					{
						if( controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							bFlag = true;
							break;
						}

						System.Threading.Thread.Sleep( 500 );

						iCount++;
					}

					if( bFlag )
					{
						status = controller.Status;
						if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
						{
							controller.Start();
							controller.WaitForStatus( System.ServiceProcess.ServiceControllerStatus.Running, new TimeSpan( 0, 1, 0 ) );
							status = controller.Status;
							DisplayStatus4DBAutoBackUp( status, true );
						}
					}
				}
				catch( Exception err )
				{
					MonitorHelper.DealWithError( err );
				}
				finally
				{
					try
					{
						mbRS4DBAutoBackUp = false;
						btnReStart4DBAutoBackUp.Enabled = btnStop4DBAutoBackUp.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Running;
						btnStart4DBAutoBackUp.Enabled = controller.Status == System.ServiceProcess.ServiceControllerStatus.Stopped;
					}
					catch
					{
					}
				}
			}
		}

		#endregion 重启服务

        private void btnWebLink_Click( object sender, EventArgs e )
        {
            try
            {
                using( frmWebLinkConfig frm = new frmWebLinkConfig() )
                {
                    frm.ShowDialog();
                }
            }
            catch( Exception err )
            {
                MonitorHelper.DealWithError( err );
            }
        }
	}
}