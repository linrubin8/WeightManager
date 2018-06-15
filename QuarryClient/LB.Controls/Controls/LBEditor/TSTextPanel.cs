using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace LB.Controls.LBEditor
{
	public class TSTextPanel : Panel
	{
		private PointF mTextPoint;

		public PointF TextPoint
		{
			get
			{
				return mTextPoint;
			}
			set
			{
				if( mTextPoint != value )
				{
					mTextPoint = value;
					this.Invalidate();
				}
			}
		}


		[DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ), Browsable( true ), DefaultValue( (string)null )]
		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if( base.Text != value )
				{
					base.Text = value;
					this.Invalidate();
				}
			}
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			base.OnPaint( e );

			try
			{
				using( SolidBrush brush = new SolidBrush( this.ForeColor ) )
				{
					e.Graphics.DrawString( this.Text, this.Font, brush, this.TextPoint );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}
	}
}
