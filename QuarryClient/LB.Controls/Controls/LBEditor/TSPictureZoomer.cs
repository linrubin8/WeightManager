using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace LB.Controls.LBEditor
{
	public class TSPictureZoomer : PictureBox
	{
		private Image mImg = null;
		private int miZoomPercent = 100;
		private int miZoomLocation;
		private bool mbAutoZoomFit = true;
		private bool mbLeftButton4Select = true;
		private ToolTip mToolTip = null;
		private string mstrImageName = "";
		private int miLoadFilePromptSize = 0;

		public event EventHandler ImageChanged;

		// 当 Image 由 Stream 而来时，想再 Save，就会报错：A generic error occurred in GDI+.。此问题可能是 .Net 的 Bug
		// 解决办法是由 Stream 而来时，记录下它的 byte[]
		private bool mbImageFromStream = false;
		private byte[] mBuffers = null;

		private int[] mZoomArray = new int[] { 5, 10, 20, 30, 50, 70, 85, 100, 150, 200, 300, 500, 1000, 2000, 3000, 5000, 8000, 10000 };

		public TSPictureZoomer()
		{
			miZoomLocation = FindLocation( 100 );
			mToolTip = new ToolTip();
			mToolTip.SetToolTip( this, "双击浏览图片" );
			mToolTip.Active = true;
		}

		public int LoadFilePromptSize
		{
			get
			{
				return miLoadFilePromptSize;
			}
			set
			{
				miLoadFilePromptSize = value;
			}
		}

		public bool DoubleClickViewPicture
		{
			get
			{
				return mToolTip.Active;
			}
			set
			{
				mToolTip.Active = value;
			}
		}

		public bool LeftButton4Select
		{
			get
			{
				return mbLeftButton4Select;
			}
			set
			{
				mbLeftButton4Select = value;
			}
		}

		public new Image Image
		{
			get
			{
				return mImg;
			}
			set
			{
				if( value != mImg )
				{
					mbImageFromStream = false;
					mBuffers = null;
					mImg = value;

					if( value != null )
					{
						if( mbAutoZoomFit )
						{
							this.ZoomFit();
						}
					}

					this.Invalidate();

					OnImageChanged( EventArgs.Empty );
				}
			}
		}

		public string ImageName
		{
			get
			{
				return mstrImageName;
			}
			set
			{
				mstrImageName = value;
			}
		}

		protected virtual void OnImageChanged( EventArgs e )
		{
			if( ImageChanged != null )
			{
				ImageChanged( this, e );
			}
		}

		public string Value4DBGet()
		{
			string strCode = "";
			if( mImg != null )
			{
				strCode = GetImageBase64( mImg );
				strCode = CommonFuntion.Zip( strCode );
			}

			return strCode;
		}

		public void Value4DBSet( string value )
		{
			byte[] buffers = null;
			Image img = null;
			if( !string.IsNullOrEmpty( value ) )
			{
				string strCode = CommonFuntion.UnZip( value );
				img = ImageFromBase64( strCode, out buffers );
			}

			this.Image = img;

			if( buffers != null )
			{
				mbImageFromStream = true;
				mBuffers = buffers;
			}
		}

		private string GetImageBase64( Image img )
		{
			string s = "";

			if( mbImageFromStream )
			{
				s = Base64.ToBase64( mBuffers );
			}
			else
			{
				byte[] b = CommonFuntion.GetImageBytes( img );
				s = Base64.ToBase64( b );
			}

			return s;
		}

		private Image ImageFromBase64( string strBase64, out byte[] buffers )
		{
			buffers = null;
			Image img = null;

			if( !string.IsNullOrEmpty( strBase64 ) )
			{
				buffers = Base64.FromBase64( strBase64 );

				using( System.IO.MemoryStream stream = new System.IO.MemoryStream( buffers ) )
				{
					img = Image.FromStream( stream );
				}
			}

			return img;
		}

		public int ZoomPercent
		{
			get
			{
				return miZoomPercent;
			}
			set
			{
				miZoomPercent = value;
			}
		}

		public void ZoomPlus()
		{
			bool bNotReDraw = true;

			if( miZoomLocation + 1 < mZoomArray.Length )
			{
				int iZoomPercent_old = miZoomPercent;

				// 新的比例
				miZoomLocation++;
				miZoomPercent = mZoomArray[miZoomLocation];
				bNotReDraw = false;

				// 中心点不变
				mfX_old = mfX_old +
					(float)Width * 100f / (float)iZoomPercent_old / 2f -
					(float)Width * 100f / (float)miZoomPercent / 2f;
				mfY_old = mfY_old +
					(float)Height * 100f / (float)iZoomPercent_old / 2f -
					(float)Height * 100f / (float)miZoomPercent / 2f;

				AdjustLocation();
			}

			if( !bNotReDraw )
			{
				this.Invalidate();
			}
		}

		public void ZoomMinus()
		{
			bool bNotReDraw = true;

			if( miZoomLocation > 0 )
			{
				int iZoomPercent_old = miZoomPercent;

				// 新的比例
				miZoomLocation--;
				miZoomPercent = mZoomArray[miZoomLocation];
				bNotReDraw = false;

				// 中心点不变
				mfX_old = mfX_old +
					(float)Width * 100f / (float)iZoomPercent_old / 2f -
					(float)Width * 100f / (float)miZoomPercent / 2f;
				mfY_old = mfY_old +
					(float)Height * 100f / (float)iZoomPercent_old / 2f -
					(float)Height * 100f / (float)miZoomPercent / 2f;

				AdjustLocation();
			}

			if( !bNotReDraw )
			{
				this.Invalidate();
			}
		}

		public void ZoomOriginal()
		{
			int iTemp = FindLocation( 100 );
			if( miZoomLocation != iTemp )
			{
				miZoomPercent = 100;
				miZoomLocation = iTemp;
				AdjustLocation();

				this.Invalidate();
			}
		}

		public void ZoomFit()
		{
			if( this.mImg == null )
			{
				return;
			}

			int iXPercent = (int)( ( (float)this.Width / (float)this.mImg.Width ) * 100 );
			int iYPercent = (int)( ( (float)this.Height / (float)this.mImg.Height ) * 100 );
			int iFinal = Math.Min( iXPercent, iYPercent );
			if( miZoomPercent != iFinal )
			{
				miZoomPercent = iFinal;
				miZoomLocation = FindLocation( miZoomPercent );
				AdjustLocation();

				this.Invalidate();
			}
		}

		private int FindLocation( int percent )
		{
			int iLast = mZoomArray[0];
			if( percent < iLast )
			{
				return 0;
			}
			for( int i = 1, j = mZoomArray.Length; i < j; i++ )
			{
				if( iLast <= percent && percent < mZoomArray[i] )
				{
					return i;
				}
			}
			return mZoomArray.Length - 1;
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			if( mImg != null )
			{
				if( miZoomPercent == 0 )
				{
					this.ZoomFit();
				}

				// 只绘制图片与控件框相对应的部分
				Graphics g = e.Graphics;
				g.DrawImage(
					mImg, new Rectangle( 0, 0, this.Width, this.Height ),
					mfX_old, mfY_old,
					(float)Width * 100f / (float)( miZoomPercent == 0 ? 100 : miZoomPercent ),
					(float)Height * 100f / (float)( miZoomPercent == 0 ? 100 : miZoomPercent ),
					GraphicsUnit.Pixel );

				if( mbMouseDown &&
					( mbLeftButton4Select && Control.MouseButtons == MouseButtons.Left ||
					!mbLeftButton4Select && Control.MouseButtons == MouseButtons.Right ) )
				{
					using( Pen pen = new Pen( Color.WhiteSmoke ) )
					{
						pen.DashPattern = new float[] { 5, 8 };
						int iWidth = Math.Abs( mPZoom_moving.X - mPZoom_old.X );
						int iHeight = Math.Abs( mPZoom_moving.Y - mPZoom_old.Y );
						g.DrawRectangle( pen, Math.Min( mPZoom_moving.X, mPZoom_old.X ), Math.Min( mPZoom_moving.Y, mPZoom_old.Y ), iWidth, iHeight );
					}
				}
			}
			else
			{
				Graphics g = e.Graphics;
				using( SolidBrush brush = new SolidBrush( Color.White ) )
				{
					g.FillRectangle( brush, 0, 0, this.Width, this.Height );
				}

				//base.OnPaint( e );
			}
		}

		private Point mP_old = Point.Empty;
		private float mfX_old = 0;
		private float mfY_old = 0;
		private Point mPZoom_old = Point.Empty;
		private Point mPZoom_moving = Point.Empty;
		private bool mbMouseDown = false;

		protected override void OnMouseDown( MouseEventArgs e )
		{
			base.OnMouseDown( e );

			if( mbLeftButton4Select && e.Button == MouseButtons.Right ||
				!mbLeftButton4Select && e.Button == MouseButtons.Left )
			{
				mP_old = e.Location;
				this.Cursor = Cursors.SizeAll;
			}

			if( !mbLeftButton4Select && e.Button == MouseButtons.Right ||
				mbLeftButton4Select && e.Button == MouseButtons.Left )
			{
				mPZoom_old = e.Location;
				this.Cursor = Cursors.Cross;
				mbMouseDown = true;
			}
		}

		protected override void OnMouseMove( MouseEventArgs e )
		{
			base.OnMouseMove( e );

			if( mbLeftButton4Select && e.Button == MouseButtons.Right ||
				!mbLeftButton4Select && e.Button == MouseButtons.Left )
			{
				mfX_old += (float)( mP_old.X - e.X ) * 100f / (float)miZoomPercent;
				mfY_old += (float)( mP_old.Y - e.Y ) * 100f / (float)miZoomPercent;

				AdjustLocation();

				mP_old = e.Location;

				this.Invalidate();
			}

			if( !mbLeftButton4Select && e.Button == MouseButtons.Right ||
				mbLeftButton4Select && e.Button == MouseButtons.Left )
			{
				mPZoom_moving = e.Location;
				this.Invalidate();
			}
		}

		protected override void OnMouseUp( MouseEventArgs e )
		{
			base.OnMouseUp( e );

			if( mbLeftButton4Select && e.Button == MouseButtons.Right ||
				!mbLeftButton4Select && e.Button == MouseButtons.Left )
			{
				this.Cursor = Cursors.Default;
			}

			////System.Diagnostics.Debug.WriteLine( "mbMouseDown=" + mbMouseDown.ToString() );

			if( mbMouseDown &&
				( !mbLeftButton4Select && e.Button == MouseButtons.Right ||
				mbLeftButton4Select && e.Button == MouseButtons.Left ) )
			{
				this.Cursor = Cursors.Default;

				int iZoomPercent_old = miZoomPercent;

				float fRectX = e.X - mPZoom_old.X;
				float fRectY = e.Y - mPZoom_old.Y;
				if( fRectX != 0 && fRectY != 0 )
				{
					int iXPercent = Math.Abs( (int)( (float)this.Width / fRectX ) );
					int iYPercent = Math.Abs( (int)( (float)this.Height / fRectY ) );
					miZoomPercent *= Math.Min( iXPercent, iYPercent );
					if( miZoomPercent > mZoomArray[mZoomArray.Length - 1] )
					{
						miZoomPercent = mZoomArray[mZoomArray.Length - 1];
					}
					miZoomLocation = FindLocation( miZoomPercent );

					// 中心点不变
					mfX_old = mfX_old +
						(float)Math.Min( e.X, mPZoom_old.X ) * 100f / (float)iZoomPercent_old;
					mfY_old = mfY_old +
						(float)Math.Min( e.Y, mPZoom_old.Y ) * 100f / (float)iZoomPercent_old;

					AdjustLocation();

					this.Invalidate();

					mbMouseDown = false;
				}
			}
		}

		private void AdjustLocation()
		{
			if( mImg == null )
			{
				return;
			}

			float fXTemp = (float)Width * 100f / (float)miZoomPercent;
			if( mImg.Width > fXTemp )
			{
				if( mfX_old < 0 )
				{
					mfX_old = 0;
				}
				else if( mfX_old + fXTemp > mImg.Width )
				{
					mfX_old = mImg.Width - fXTemp;
				}
			}
			else
			{
				// 如果缩放图比图框小，则居中
				mfX_old = ( mImg.Width - fXTemp ) / 2;
			}

			float fYTemp = (float)Height * 100f / (float)miZoomPercent;
			if( mImg.Height > fYTemp )
			{
				if( mfY_old < 0 )
				{
					mfY_old = 0;
				}
				else if( mfY_old + fYTemp > mImg.Height )
				{
					mfY_old = mImg.Height - fYTemp;
				}
			}
			else
			{
				// 如果缩放图比图框小，则居中
				mfY_old = ( mImg.Height - fYTemp ) / 2;
			}
		}

		protected override void OnSizeChanged( EventArgs e )
		{
			base.OnSizeChanged( e );

			AdjustLocation();
			this.Invalidate();
		}

		protected override void OnDoubleClick( EventArgs e )
		{
			base.OnDoubleClick( e );

			if( DoubleClickViewPicture )
			{
				frmFullPictureViewer frm = new frmFullPictureViewer( this.Image, this.ImageName );
				frm.Show();
			}
		}

		public void LoadFile()
		{
			using( OpenFileDialog dlg = new OpenFileDialog() )
			{
				dlg.Filter = "所有支持图片|*.bmp;*.jpg;*.jpeg;*jpe;*.jfif;*.gif;*.tif;*.tiff;*.png;*.wmf|" +
					"位图文件(*.bmp)|*.bmp|" +
					"JPFG(*.jpg;*.jpeg;*jpe;*.jfif)|*.jpg;*.jpeg;*jpe;*.jfif|" +
					"GIF(*.gif)|*.gif|" +
					"TIFF(*.tif;*.tiff)|*.tif;*.tiff|" +
					"PNG(*.png)|*.png|" +
					"WMF(*.wmf)|*.wmf";
				dlg.ShowDialog();

				string strFile = dlg.FileName;
				if( File.Exists( strFile ) )
				{
					FileInfo info = new FileInfo( strFile );
					if( miLoadFilePromptSize > 0 && info.Length > miLoadFilePromptSize * 1024 )
					{
						MessageBox.Show( string.Format( "当前选择的图片超过 {0}M，系统将会自动收缩图片，清晰度会被降低。", ( info.Length / 1024 ).ToString( "n3" )  ), 
							"提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}

					LoadFile( strFile );
				}
			}
		}

		public void LoadFile( string strFile )
		{
			if( File.Exists( strFile ) )
			{
				this.Image = Image.FromFile( strFile );
			}
			else if( strFile != "" )
			{
				throw new FileNotFoundException();
			}
		}

		public void SaveFileWithoutName()
		{
			SaveFileWithName( null );
		}

		/// <summary>
		/// 保存图片
		/// </summary>
		/// <param name="strFileName"></param>
		public void SaveFileWithName( string strFileName )
		{
			if( this.mImg == null )
			{
				MessageBox.Show( "当前没有图片。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			else
			{
				using( SaveFileDialog dlg = new SaveFileDialog() )
				{
					if( this.mImg.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Bmp ) )
					{
						dlg.Filter = "位图文件(*.bmp)|*.bmp";
					}
					else if( this.mImg.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Jpeg ))
					{
						dlg.Filter = "JPFG(*.jpg;*.jpeg;*jpe;*.jfif)|*.jpg;*.jpeg;*jpe;*.jfif";
					}
					else if( this.mImg.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Gif ))
					{
						dlg.Filter = "GIF(*.gif)|*.gif";
					}
					else if( this.mImg.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Tiff ))
					{
						dlg.Filter = "TIFF(*.tif;*.tiff)|*.tif;*.tiff";
					}
					else if( this.mImg.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Png ) )
					{
						dlg.Filter = "PNG(*.png)|*.png";
					}

					if( !string.IsNullOrEmpty( strFileName ) )
					{
						strFileName = strFileName.Replace( "\\", "＼" ).Replace( "/", "／" ).Replace( ":", "：" ).Replace( "*", "＊" ).
							Replace( "?", "？" ).Replace( "\"", "＂" ).Replace( "<", "＜" ).Replace( ">", "＞" ).Replace( "|", "｜" );
						dlg.FileName = strFileName;
					}

					dlg.ShowDialog();

					string strFile = dlg.FileName;
					if( !string.IsNullOrEmpty( strFile ) )
					{
						SaveFile( strFile );
					}
				}
			}
		}

		public void SaveFile( string strFileFullName )
		{
			if( string.IsNullOrEmpty( strFileFullName ) )
			{
				return;
			}

			if( this.mImg == null )
			{
				MessageBox.Show( "当前没有图片。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			else if( mbImageFromStream )
			{
				using( FileStream stream = File.OpenWrite( strFileFullName ) )
				{
					stream.Write( this.mBuffers, 0, this.mBuffers.Length );
				}
			}
			else
			{
				this.mImg.Save( strFileFullName, mImg.RawFormat );
			}
		}
	}
}
