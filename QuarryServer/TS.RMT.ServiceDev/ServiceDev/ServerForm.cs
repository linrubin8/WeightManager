using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TS.RMT.ServiceDev
{
	public partial class ServerForm : Form
	{
		public ServerForm()
		{
			InitializeComponent();
		}

		private void ServerForm_Load( object sender, EventArgs e )
		{
			try
			{
				TS.RMT.Server.ServerHelper.ServerRunningChanged += new EventHandler( ServerHelper_ServerRunningChanged );

				Start();
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		void ServerHelper_ServerRunningChanged( object sender, EventArgs e )
		{
			try
			{
				btnStart.Enabled = !TS.RMT.Server.ServerHelper.ServerRunning;
				btnStop.Enabled = TS.RMT.Server.ServerHelper.ServerRunning;

				this.Text = TS.RMT.Base.ConfigHelper.ServiceName + ":" + TS.RMT.Base.ConfigHelper.Port.ToString() + " " +
					( TS.RMT.Server.ServerHelper.ServerRunning ? "运行中" : "已停止" );
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		private void btnStop_Click( object sender, EventArgs e )
		{
			try
			{
				if( TS.RMT.Server.ServerHelper.ServerRunning )
				{
					TS.RMT.Server.ServerHelper.ServerStop();
				}
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		private void btnStart_Click( object sender, EventArgs e )
		{
			try
			{
				if( !TS.RMT.Server.ServerHelper.ServerRunning )
				{
					Start();
				}
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		private void Start()
		{
			TS.RMT.Server.ServerHelper.ServerStart();

			// 显示 URL
			txtServerName.Text = TS.RMT.Base.ConfigHelper.ServiceName;
			txtServerPort.Text = TS.RMT.Base.ConfigHelper.Port.ToString();
		}

		private void ServerForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			try
			{
				if( TS.RMT.Server.ServerHelper.ServerRunning )
				{
					TS.RMT.Server.ServerHelper.ServerStop();
				}
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		private void btnClose_Click( object sender, EventArgs e )
		{
			try
			{
				this.Close();
			}
			catch( Exception ex )
			{
				DealWithError( ex );
			}
		}

		private void DealWithError( Exception ex )
		{
			using( TSErrorShower frm = new TSErrorShower( ex ) )
			{
				frm.ShowDialog();
			}
		}
	}
}