using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.XWPF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace TS.RMT.ServiceDev
{
	public partial class TSErrorShower : Form
	{
		private string mstrMoreInfo = "";
		private string mstrCompanyName = "";
		private string mstrVersion = "";
		private string mstrButtonAction = "";

		public TSErrorShower( Exception ex )
			: this( ex, "", "", "" )
		{
		}

		public TSErrorShower( Exception ex, string strCompanyName, string strVersion, string strButtonAction )
		{
			InitializeComponent();

			mstrCompanyName = strCompanyName;
			mstrVersion = strVersion;
			mstrButtonAction = strButtonAction;

			this.KeyPreview = true;
			string msg = "";
			string stackTrace = "";
			PrepareErrorInfo( ex, ref msg, ref stackTrace );
			mstrMoreInfo = stackTrace;

			msg = msg.Replace( "\n", Environment.NewLine );
			txtMainInfo.Text = msg;

			int iMaxWidth = (int)( (float)System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.55f );
			int iMaxHeight = (int)( (float)System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.4f );
			int iMinWidth = 400;
			int iMinHeight = 43;

			txtMainInfo.MaximumSize = new Size( iMaxWidth, iMaxHeight );
			txtMainInfo.MinimumSize = new Size( iMinWidth, iMinHeight );
			txtMainInfo.Size = txtMainInfo.PreferredSize;
			if( txtMainInfo.PreferredHeight > txtMainInfo.Height )
			{
				txtMainInfo.ScrollBars = ScrollBars.Vertical;
			}
		}

		private void PrepareErrorInfo( Exception ex, ref string msg, ref string stackTrace )
		{
			if( ex.Message != "" )
			{
				using( StringReader reader = new StringReader( ex.Message ) )
				{
					string strLine = reader.ReadLine();
					while( strLine != null )
					{
						if( strLine.IndexOf( ".NET Framework", StringComparison.CurrentCultureIgnoreCase ) > 0 ||
							strLine.IndexOf( "more info", StringComparison.CurrentCultureIgnoreCase ) > 0 )
						{
							stackTrace += ( stackTrace == "" ? "" : Environment.NewLine ) + strLine + reader.ReadToEnd();
							break;
						}

						msg += ( msg == "" ? "" : Environment.NewLine ) + strLine;
						strLine = reader.ReadLine();
					}
				}

				stackTrace += ( stackTrace == "" ? "" : Environment.NewLine ) + "-- StackTrace --" + Environment.NewLine + ex.StackTrace;
			}

			if( ex.InnerException != null )
			{
				msg += ( msg == "" ? "" : Environment.NewLine ) + "-- v --";
				stackTrace += ( stackTrace == "" ? "" : Environment.NewLine ) + "-- v --";

				PrepareErrorInfo( ex.InnerException, ref msg, ref stackTrace );
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
				DealWithErrorFatal( ex );
			}
		}

		private void btnSave_Click( object sender, EventArgs e )
		{
			try
			{
				Bitmap img = GetScreenImage();

				string strFile = "";
				bool bMsg = false;

				try
				{
					OpenFileDialog dlg = new OpenFileDialog();
					dlg.AddExtension = true;
					dlg.CheckFileExists = false;
					dlg.DefaultExt = "docx";
					dlg.Filter = "Word 文档 (*.docx)|*.docx";
					if( dlg.ShowDialog() == DialogResult.OK )
					{
						strFile = dlg.FileName;
					}
				}
				catch
				{
					string path = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory );
					strFile = Path.Combine( path, "错误报告_" + DateTime.Now.ToString( "yyMMddmmssfff" ) + ".docx" );
					bMsg = true;
				}

				if( File.Exists( strFile ) )
				{
					File.Delete( strFile );
				}
				SaveErrorFile( strFile, img );

				if( bMsg )
				{
					MessageBox.Show( "错误报告文件已保存：" + Environment.NewLine + strFile, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
			catch( Exception ex )
			{
				DealWithErrorFatal( ex );
			}
		}

		private void SaveErrorFile( string strFile, Bitmap img )
		{

			XWPFDocument m_Docx = new XWPFDocument();
			CT_SectPr m_SectPr = new CT_SectPr();

			//页面设置A4
			m_SectPr.pgSz.w = (ulong)16838;
			m_SectPr.pgSz.h = (ulong)11906;

			m_Docx.Document.body.sectPr = m_SectPr;

			// 创建段落
			CT_P m_p = m_Docx.Document.body.AddNewP();
			m_p.AddNewPPr().AddNewJc().val = ST_Jc.left;	//段落水平居左
			XWPFParagraph gp = new XWPFParagraph( m_p, m_Docx ); //创建XWPFParagraph

			CT_R m_r = m_p.AddNewR();
			m_r.AddNewT().Value = "记录时间：" + DateTime.Now.ToString( "yyyy-MM-dd mm:dd:ss" );
			m_r.AddNewCr();
			m_r.AddNewT().Value = "账套公司名称：" + mstrCompanyName;
			m_r.AddNewCr();
			m_r.AddNewT().Value = "按钮名称：" + mstrButtonAction;
			WriteText( m_r, txtMainInfo.Text );
			WriteText( m_r, mstrMoreInfo );

			using( System.IO.MemoryStream stream = new System.IO.MemoryStream() )
			{
				using( Bitmap bmp = GetScreenImage() )
				{
					bmp.Save( stream, System.Drawing.Imaging.ImageFormat.Gif );
				}

				stream.Position = 0;

				m_p = m_Docx.Document.body.AddNewP();
				m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;//段落水平居中
				gp = new XWPFParagraph( m_p, m_Docx );
				XWPFRun gr = gp.CreateRun();
				gr.AddPicture( stream, (int)NPOI.XWPF.UserModel.PictureType.GIF, "1.gif", 360000 * 23, 360000 * 16 );
			}

			using( FileStream writer = new FileStream( strFile, FileMode.CreateNew ) )
			{
				m_Docx.Write( writer );
				writer.Close();
			}
		}

		private void WriteText( CT_R m_r, string text )
		{
			using( StringReader reader = new StringReader( text ) )
			{
				string strLine = reader.ReadLine();
				while( strLine != null )
				{
					m_r.AddNewT().Value = strLine;
					m_r.AddNewCr();
					strLine = reader.ReadLine();
				}
			}
		}

		private Bitmap GetScreenImage()
		{
			Bitmap bmp = null;
			try
			{
				int width = Screen.PrimaryScreen.Bounds.Width;
				int height = Screen.PrimaryScreen.Bounds.Height;
				bmp = new Bitmap( width, height );
				using( Graphics g = Graphics.FromImage( bmp ) )
				{
					g.CopyFromScreen( 0, 0, 0, 0, new Size( width, height ) );
				}
			}
			catch
			{
			}
			return bmp;
		}

		private void DealWithErrorFatal( Exception err )
		{
			if( err.InnerException != null )
			{
				DealWithErrorFatal( err.InnerException );
			}
			else if( err.Message != "" )
			{
				string strMsg = err.Message;

				System.Windows.Forms.MessageBox.Show( strMsg, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void TSErrorShower_BackColorChanged( object sender, EventArgs e )
		{
			try
			{
				this.txtMainInfo.BackColor = this.BackColor;
				this.txtMoreInfo.BackColor = this.BackColor;
			}
			catch( Exception ex )
			{
				DealWithErrorFatal( ex );
			}
		}
	}
}