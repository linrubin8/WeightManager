using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.LBEditor
{
	public partial class frmFullPictureViewer : Form
	{
		private ToolStripButton btnBig = null;
		private ToolStripButton btnSmall = null;
		private ToolStripButton btnFit = null;
		private ToolStripButton btnOrg = null;

		public frmFullPictureViewer( Image img, string strSaveFileDefaultName )
		{
			InitializeComponent();

			this.Text += " " + strSaveFileDefaultName;
			this.picMain.Image = img;
			this.picMain.DoubleClickViewPicture = false;

			btnBig = new ToolStripButton( "放大" );
			btnSmall = new ToolStripButton( "缩小" );
			btnFit = new ToolStripButton( "适合大小" );
			btnOrg = new ToolStripButton( "原图片大小" );

			this.ContextMenuStrip = new ContextMenuStrip();
			this.ContextMenuStrip.Items.Add( btnBig );
			this.ContextMenuStrip.Items.Add( btnSmall );
			this.ContextMenuStrip.Items.Add( btnFit );
			this.ContextMenuStrip.Items.Add( btnOrg );

			btnBig.Click += new EventHandler( btnBig_Click );
			btnSmall.Click += new EventHandler( btnSmall_Click );
			btnFit.Click += new EventHandler( btnFit_Click );
			btnOrg.Click += new EventHandler( btnOrg_Click );
		}

		void btnOrg_Click( object sender, EventArgs e )
		{
			try
			{
				this.picMain.ZoomOriginal();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void btnFit_Click( object sender, EventArgs e )
		{
			try
			{
				this.picMain.ZoomFit();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void btnSmall_Click( object sender, EventArgs e )
		{
			try
			{
				this.picMain.ZoomMinus();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void btnBig_Click( object sender, EventArgs e )
		{
			try
			{
				this.picMain.ZoomPlus();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void frmFullPictureViewer_SizeChanged( object sender, EventArgs e )
		{
			try
			{
				this.picMain.ZoomFit();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void checkBox1_CheckedChanged( object sender, EventArgs e )
		{
			try
			{
				this.TopMost = checkBox1.Checked;
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}
	}
}