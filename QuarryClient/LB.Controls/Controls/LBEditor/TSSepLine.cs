using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace LB.Controls.LBEditor

{
	public class TSSepLine : Control
	{
		private Color mSepColor1 = Color.FromArgb( 206, 235, 255 );
		private Color mSepColor2 = Color.FromArgb( 132, 195, 255 );
		private Color mSepColor3 = Color.FromArgb( 255, 255, 255 );
		private Color mTextColor = Color.DarkBlue;
		private Color mTextColorHover = Color.Blue;

		private const int MC_iLineLeft = 3;
		private const int MC_iTextLeft = 40;

		private string mstrText = "";
		private bool mbIsExpanded = true;
		private Rectangle mRect;
		private bool mbIsEnter = false;
		private List<Control> mlstGroupControls = new List<Control>();

		public List<Control> GroupControls
		{
			get
			{
				return mlstGroupControls;
			}
		}

		public TSSepLine()
		{
			this.Height = this.Font.Height + 4;
			this.ForeColor = mTextColor;
		}

		protected Color SepColor1
		{
			get
			{
				return mSepColor1;
			}
			set
			{
				mSepColor1 = value;
			}
		}

		protected Color SepColor2
		{
			get
			{
				return mSepColor2;
			}
			set
			{
				mSepColor2 = value;
			}
		}

		[
		DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ),
		Browsable( true )
		]
		public override string Text
		{
			get
			{
				return mstrText;
			}
			set
			{
				mstrText = value;
			}
		}

		[
		DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden ),
		Browsable( false )
		]
		public bool IsExpanded
		{
			get
			{
				return mbIsExpanded;
			}
			set
			{
				mbIsExpanded = value;
				Repaint();
			}
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			base.OnPaint( e );

			try
			{
				Image img = null;
				if( mbIsExpanded )
				{
                    img = LB.Properties.Resources.SeparatorArrowClose;
                }
				else
				{
					img = LB.Properties.Resources.SeparatorArrow;
				}
				Graphics g = e.Graphics;
				int iWidth = ( int )g.MeasureString( this.Text, this.Font ).Width + img.Width + 10;
				int iRightLineLeft = MC_iTextLeft + iWidth;
				mRect = new Rectangle( MC_iTextLeft, 2, iWidth, this.Font.Height );

				int iTop = this.Height / 2 - 1;
				using( Pen pen = new Pen( mSepColor1 ) )
				{
					g.DrawLine( pen, MC_iLineLeft, iTop, MC_iTextLeft, iTop );
					g.DrawLine( pen, iRightLineLeft, iTop, this.Width - MC_iLineLeft, iTop );
				}
				using( Pen pen = new Pen( mSepColor2 ) )
				{
					g.DrawLine( pen, MC_iLineLeft, iTop + 1, MC_iTextLeft, iTop + 1 );
					g.DrawLine( pen, iRightLineLeft, iTop + 1, this.Width - MC_iLineLeft, iTop + 1 );
				}
				using( Pen pen = new Pen( mSepColor3 ) )
				{
					g.DrawLine( pen, MC_iLineLeft, iTop + 2, MC_iTextLeft, iTop + 2 );
					g.DrawLine( pen, iRightLineLeft, iTop + 2, this.Width - MC_iLineLeft, iTop + 2 );
				}

				g.DrawImage( img, MC_iTextLeft + 3, 2 );
				Color clr = mbIsEnter ? mTextColorHover : this.ForeColor;
				using( SolidBrush brush = new SolidBrush( clr ) )
				{
					g.DrawString( this.Text, this.Font, brush, MC_iTextLeft + img.Width + 5, 2 );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		public void PerformClick()
		{
			OnClick( EventArgs.Empty );
		}

		protected override void OnClick( EventArgs e )
		{
			try
			{
				mbIsExpanded = !mbIsExpanded;
				base.OnClick( e );
				Repaint();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnMouseEnter( EventArgs e )
		{
			base.OnMouseEnter( e );

			try
			{
				mbIsEnter = true;
				Repaint();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnEnter( EventArgs e )
		{
			base.OnEnter( e );

			try
			{
				mbIsEnter = true;
				Repaint();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnMouseLeave( EventArgs e )
		{
			base.OnMouseLeave( e );

			try
			{
				mbIsEnter = false;
				Repaint();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnLeave( EventArgs e )
		{
			base.OnLeave( e );

			try
			{
				mbIsEnter = false;
				Repaint();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void Repaint()
		{
			if( mRect != null )
			{
				this.Invalidate( mRect );
			}
		}
	}
}
