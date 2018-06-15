using LB.Controls.Args;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.LBEditor
{
	internal partial class TSTextBoxPopup4Calendar : Form, ITSPromptForm
	{
		private Control mOwnerControl = null;
		private TSPopupWindowMessageFilter mFilter = null;

		public TSTextBoxPopup4Calendar( Control ctrlOwner, string strDefaultValue )
		{
			InitializeComponent();

			mOwnerControl = ctrlOwner;

			SetDefaultValue( strDefaultValue );

			ctrlOwner.LostFocus += new EventHandler( ctrlOwner_LostFocus );

			#region -- 以下代码通过 API 来实现 WS_CHILD，不再使用 --

			//// 以下代码，更改 Form 的风格，使其不会抢去主窗口的焦点
			//// 关键是增加 WS_CHILD 风格，同时要注意 Form 的 TopMost 必须为 false
			//uint styleOld = TSTextBoxPopupAPI.GetWindowLong( this.Handle, TSTextBoxPopupAPI.GWL_STYLE );
			//TSTextBoxPopupAPI.SetWindowLong( this.Handle, TSTextBoxPopupAPI.GWL_STYLE, (uint)( styleOld | TSTextBoxPopupAPI.WS_CHILD ) );
			////public const int GWL_STYLE = -16;
			////public const int WS_CHILD = 0x40000000;

			////[DllImport( "user32.dll" )]
			////public extern static uint GetWindowLong( IntPtr hwnd, int nIndex );

			////[DllImport( "user32.dll" )]
			////public extern static uint SetWindowLong( IntPtr hwnd, int nIndex, uint dwNewLong );

			#endregion -- 以下代码通过 API 来实现 WS_CHILD，不再使用 --
		}

		~TSTextBoxPopup4Calendar()
		{
			try
			{
				if( mFilter != null )
				{
					Application.RemoveMessageFilter( mFilter );
					mFilter = null;
				}
			}
			catch
			{
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				// 注意 !!!!!!!!!!!!!!!!!!!!!!!!!
				// ShowInTaskbar 属性必须为 true，该属性默认为 true
				// TopMost 属性是否为 true 都不影响最终的效果
				// 否则，窗口打不开

				int WS_CHILD = 0x40000000;
				int WS_THICKFRAME = 0x00040000;
				int WS_EX_NOACTIVATE = 0x08000000;
				int WS_EX_TOOLWINDOW = 0x00000080;

				CreateParams ret = base.CreateParams;
				ret.Style = WS_THICKFRAME | WS_CHILD;
				ret.ExStyle |= (int)WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW;
				ret.X = this.Location.X;
				ret.Y = this.Location.Y;
				return ret;
			}
		}

		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad( e );

			try
			{
				mFilter = new TSPopupWindowMessageFilter( this, mOwnerControl );
				Application.AddMessageFilter( mFilter );

				// Win7 下与 XP\2003 等，Calendar 的大小不一
				this.ClientSize = tsCalendar.Size;
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void ctrlOwner_LostFocus( object sender, EventArgs e )
		{
			try
			{
				this.Hide();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		public void SetDefaultValue( string strDefaultValue )
		{
			if( !string.IsNullOrEmpty( strDefaultValue ) )
			{
				try
				{
					this.tsCalendar.SetDate( Convert.ToDateTime( strDefaultValue ) );
				}
				catch
				{
				}
			}
		}

		private void tsCalendar_DateSelected( object sender, DateRangeEventArgs e )
		{
			try
			{
				DateSelected( e.Start );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		public void DateSelected()
		{
			DateSelected( this.tsCalendar.SelectionStart );
		}

		private void DateSelected( DateTime dtSelected )
		{
			DataTable dtReturn = new DataTable();
			dtReturn.Columns.Add( "Return", typeof( string ) );

			DataRow drNew = dtReturn.NewRow();
			drNew["Return"] = dtSelected.ToString( "yyyy-MM-dd" );
			dtReturn.Rows.Add( drNew );

			// 返回所选的日期值
			PromptReturnArgs args = new PromptReturnArgs( drNew, null );
			this.Hide();
			OnPromptReturn( args );
		}

		private void tsCalendar_DateChanged( object sender, DateRangeEventArgs e )
		{
			try
			{
				// 解决一个 Bug: 在年份中选择以往的年份时，控件显示不正常
				this.tsCalendar.DateSelected -= new System.Windows.Forms.DateRangeEventHandler( this.tsCalendar_DateSelected );
				this.tsCalendar.SetDate( e.Start );
				this.tsCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler( this.tsCalendar_DateSelected );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		public void SelectUp()
		{
			tsCalendar.SetDate( tsCalendar.SelectionStart.AddDays( -7 ) );
		}

		public void SelectDown()
		{
			tsCalendar.SetDate( tsCalendar.SelectionStart.AddDays( 7 ) );
		}

		public void SelectLeft()
		{
			tsCalendar.SetDate( tsCalendar.SelectionStart.AddDays( -1 ) );
		}

		public void SelectRight()
		{
			tsCalendar.SetDate( tsCalendar.SelectionStart.AddDays( 1 ) );
		}

		private void OnPromptReturn( PromptReturnArgs args )
		{
			if( PromptReturn != null )
			{
				PromptReturn( this, args );
			}
		}

		#region IPromptForm Members

		public event PromptReturnEventHandler PromptReturn;

		#endregion

		#region ITSPromptForm Members

		private Point mPoint = Point.Empty;
		public Point PromptCellAddress
		{
			get
			{
				return mPoint;
			}
			set
			{
				mPoint = value;
			}
		}

		#endregion
	}
}