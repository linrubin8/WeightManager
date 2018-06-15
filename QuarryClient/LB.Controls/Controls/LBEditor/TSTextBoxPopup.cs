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
	/// <summary>
	/// 在 Win7、Win2008Server 两个系统上，某些电脑可能会在重启后第一次打开 popup 时，卡死
	/// 经过反复尝试，发现，卡死原因可能是：popup 无焦点，如果在 popup 上放 DataGridView 或 Button 这些控件时，就可能会卡死；不知这些控件与焦点有什么关系？
	/// 所以，现在只能这样来避开这个问题：不用 DataGridView ，而是用 ListBox，自己画 Item ；不用 Button ，按钮自己画来实现
	/// </summary>
	internal partial class TSTextBoxPopup : Form, ITSPromptForm
	{
		private const int MC_iScrollWidth = 18;

		private Control mOwnerControl = null;
		private List<string> mListColumns = new List<string>( 2 );
		private DataRow mdrSelected = null;
		private int miSelectedIndex = -1;
		private int miInitWidth = 0;
		private int miGridColumnIndex = -1;		// 记录触发的 DataGridView ColumnIndex
		private TSPopupWindowMessageFilter mFilter = null;

		private List<int> mlstColumnWidth = new List<int>();	// 记录计算出的每列宽度

		public event EventHandler AddClicked;
		public event EventHandler ManageClicked;

		public TSTextBoxPopup( Control ctrlOwner, int iInitWidth, int iGridColumnIndex ) : this( ctrlOwner, iInitWidth )
		{
			miGridColumnIndex = iGridColumnIndex;
		}

		public TSTextBoxPopup( Control ctrlOwner, int iInitWidth )
		{
			InitializeComponent();

			mOwnerControl = ctrlOwner;
			pnlButton.Visible = false;
			miInitWidth = iInitWidth;
			this.Width = iInitWidth + 2;

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

		~TSTextBoxPopup()
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

		private Rectangle mrectAdd = new Rectangle( 10, 6, 60, 23 );
		private Rectangle mrectManager = new Rectangle( 80, 6, 60, 23 );

		private void pnlButton_Paint( object sender, PaintEventArgs e )
		{
			try
			{
				if( pnlButton.Visible )
				{
					Graphics g = e.Graphics;

					PaintButton( g, mrectAdd, "添加", LB.Properties.Resources.btnDropDownAdd, ( mbMouseDownInAdd ? ButtonState.Pushed : ButtonState.Normal ) );
					PaintButton( g, mrectManager, "管理", LB.Properties.Resources.btnDropDownManager, ( mbMouseDownInManager ? ButtonState.Pushed : ButtonState.Normal ) );
				}
			}
			catch
			{
			}
		}

		private void PaintButton( Graphics g, Rectangle rect, string text, Image img, ButtonState state )
		{
			using( SolidBrush brushBack = new SolidBrush( pnlButton.BackColor ) )
			{
				g.FillRectangle( brushBack, rect );
			}

			ControlPaint.DrawButton( g, rect, state );

			int iAdjust = ( state == ButtonState.Pushed ? 1 : 0 );
			SizeF sizeText = g.MeasureString( text, this.Font );
			g.DrawImage( img,
				(int)( rect.Left + ( rect.Width - img.Width - sizeText.Width ) / 2 ) + iAdjust, (int)( rect.Top + ( rect.Height - img.Height ) / 2 + iAdjust ),
				img.Width, img.Height );
			g.DrawString( text, this.Font, SystemBrushes.ControlText,
				rect.Left + img.Width + ( rect.Width - img.Width - sizeText.Width ) / 2 + iAdjust, rect.Top + ( rect.Height - sizeText.Height ) / 2 + iAdjust );
		}

		private bool mbMouseDownInAdd = false;
		private bool mbMouseDownInManager = false;

		private void pnlButton_MouseDown( object sender, MouseEventArgs e )
		{
			try
			{
				if( mrectAdd.Contains( e.Location ) )
				{
					mbMouseDownInAdd = true;
					mbMouseDownInManager = false;

					pnlButton.Invalidate( mrectAdd );
				}
				else if( mrectManager.Contains( e.Location ) )
				{
					mbMouseDownInAdd = false;
					mbMouseDownInManager = true;

					pnlButton.Invalidate( mrectManager );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void pnlButton_MouseUp( object sender, MouseEventArgs e )
		{
			try
			{
				if( mbMouseDownInAdd && mrectAdd.Contains( e.Location ) )
				{
					mbMouseDownInAdd = false;
					mbMouseDownInManager = false;

					pnlButton.Invalidate( mrectAdd );
					pnlButton.Update();
					OnAddClicked( EventArgs.Empty );
				}
				else if( mbMouseDownInManager && mrectManager.Contains( e.Location ) )
				{
					mbMouseDownInAdd = false;
					mbMouseDownInManager = false;

					pnlButton.Invalidate( mrectManager );
					pnlButton.Update();
					OnManageClicked( EventArgs.Empty );
				}
				else if( mbMouseDownInAdd || mbMouseDownInManager )
				{
					mbMouseDownInAdd = false;
					mbMouseDownInManager = false;
					pnlButton.Invalidate();
					pnlButton.Update();
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void pnlButton_MouseMove( object sender, MouseEventArgs e )
		{
			try
			{
				if( mbMouseDownInAdd && !mrectAdd.Contains( e.Location ) )
				{
					mbMouseDownInAdd = false;
					pnlButton.Invalidate( mrectAdd );
				}
				else if( mbMouseDownInManager && !mrectManager.Contains( e.Location ) )
				{
					mbMouseDownInManager = false;
					pnlButton.Invalidate( mrectManager );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad( e );

			try
			{
				mFilter = new TSPopupWindowMessageFilter( this, mOwnerControl );
				Application.AddMessageFilter( mFilter );

				if( !this.ManageButtonVisible )
				{
					this.Height = 200;
				}
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

		private void OnAddClicked( EventArgs e )
		{
			if( AddClicked != null )
			{
				AddClicked( this, e );
			}
		}

		private void OnManageClicked( EventArgs e )
		{
			if( ManageClicked != null )
			{
				ManageClicked( this, e );
			}
		}

		public int GridColumnIndex
		{
			get
			{
				return miGridColumnIndex;
			}
			set
			{
				miGridColumnIndex = value;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return miSelectedIndex;
			}
			set
			{
				if( miSelectedIndex != value )
				{
					miSelectedIndex = value;
					if( this.DataSource != null )
					{
						if( miSelectedIndex >= 0 && miSelectedIndex < this.DataSource.Count )
						{
							lstBox.SelectedIndex = miSelectedIndex;
						}
					}
				}
			}
		}

		public bool ManageButtonVisible
		{
			get
			{
				return pnlButton.Visible;
			}
			set
			{
				pnlButton.Visible = value;
			}
		}

		public Control OwnerControl
		{
			get
			{
				return mOwnerControl;
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
		public List<string> ListColumns
		{
			get
			{
				return mListColumns;
			}
			set
			{
				mListColumns = value;
			}
		}

		private DataView mdvSource = null;
		public DataView DataSource
		{
			get
			{
				return mdvSource;
			}
			set
			{
				////System.Diagnostics.Debug.WriteLine( "1-" + DateTime.Now.ToString( "mm:ss.fff" ) );

				if( mdvSource != value && value != null )
				{
					int iCount = mListColumns.Count;
					if( iCount <= 0 )
					{
						iCount = value.Table.Columns.Count;
					}
					if( iCount <= 0 )
					{
						iCount = 1;
					}
					int iMinWidth = ( miInitWidth - MC_iScrollWidth - 2 * iCount ) / iCount;
					if( iMinWidth < 5 )
					{
						iMinWidth = 5;
					}

					lstBox.DataSource = mdvSource = value;

					if( miSelectedIndex >= value.Count )
					{
						miSelectedIndex = -1;
					}
					if( value.Count > 0 && miSelectedIndex >= 0 )
					{
						lstBox.SelectedIndex = miSelectedIndex;
					}
					else
					{
						miSelectedIndex = -1;
					}

					ComputeColumnWidth();
					////System.Diagnostics.Debug.WriteLine( "2-" + DateTime.Now.ToString( "mm:ss.fff" ) );
				}
				else if( value == null )
				{
					lstBox.DataSource = mdvSource = value;
					miSelectedIndex = -1;
				}
			}
		}

		private void lstBox_DrawItem( object sender, DrawItemEventArgs e )
		{
			try
			{
				int iIndex = e.Index;
				if( iIndex < 0 || mdvSource == null || mdvSource.Count == 0 || iIndex + 1 > mdvSource.Count )
				{
					return;
				}

				DataRowView drv = this.mdvSource[iIndex];

				e.DrawBackground();
				Color colorFont = this.ForeColor;
				Color colorLine = Color.LightGray;

				//// System.Diagnostics.Debug.WriteLine( "OnDrawItem:" + e.Index.ToString() + "  -------------------" );

				if( ( e.State & DrawItemState.Selected ) == DrawItemState.Selected ||
					( e.State & DrawItemState.Focus ) == DrawItemState.Focus ||
					( e.State & DrawItemState.HotLight ) == DrawItemState.HotLight )
				{
					e.DrawFocusRectangle();
					colorFont = this.BackColor;
					colorLine = this.BackColor;

					//// System.Diagnostics.Debug.WriteLine( "DrawFocusRectangle: State = " + e.State.ToString() );
				}

				using( Pen pen = new Pen( colorLine ) )
				{
					using( SolidBrush brush = new SolidBrush( colorFont ) )
					{
						int iLeft = 0;
						for( int i = 0, j = this.mListColumns.Count; i < j; i++ )
						{
							// 分隔线
							if( i > 0 )
							{
								e.Graphics.DrawLine( pen,
									(float)( e.Bounds.X + iLeft - 2 ), e.Bounds.Y,
									(float)( e.Bounds.X + iLeft - 2 ), e.Bounds.Y + e.Bounds.Height );
							}

							// 字段的值
							string strCur = GetCellValueString( drv, this.mListColumns[i] );

							if( strCur != "" )
							{
								e.Graphics.DrawString( strCur, this.Font, brush,
									(float)( e.Bounds.X + iLeft + 2 ), e.Bounds.Y + 1 );
							}

							//// System.Diagnostics.Debug.WriteLine( "DrawString: Font = " + colorFont.ToString() + "\tState = " + e.State.ToString() );

							if( this.mlstColumnWidth.Count > i )	// 多线程进入？不加判断会出错
							{
								iLeft += this.mlstColumnWidth[i];
							}
						}
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private string GetCellValueString( DataRowView drv, string columnName )
		{
			DataTable table = drv.DataView.Table;
			Type typ = table.Columns[columnName].DataType;
			if( typ == typeof( decimal ) || typ == typeof( double ) || typ == typeof( float ) )
			{
				string strValue = drv[columnName].ToString();
				decimal decValue;
				bool bOK = decimal.TryParse( strValue, out decValue );
				if( bOK )
				{
					return decValue.ToString( "0.#" );
				}
				else
				{
					return strValue.TrimEnd();
				}
			}
			else
			{
				return drv[columnName].ToString().TrimEnd();
			}
		}

		private void ComputeColumnWidth()
		{
			//mlstColumnWidth
			if( mdvSource != null && mdvSource.Count > 0 && this.mListColumns.Count > 0 )
			{
				mlstColumnWidth.Clear();

				using( Graphics g = this.CreateGraphics() )
				{
					for( int i = 0, j = mdvSource.Count; i < j; i++ )
					{
						DataRowView drv = mdvSource[i];

						for( int x = 0, y = this.mListColumns.Count; x < y; x++ )
						{
							int iTemp = (int)g.MeasureString( GetCellValueString( drv, this.mListColumns[x] ), this.Font ).Width + 4;
							if( mlstColumnWidth.Count <= x )
							{
								mlstColumnWidth.Add( iTemp );
							}
							else if( iTemp > mlstColumnWidth[x] )
							{
								mlstColumnWidth[x] = iTemp;
							}
						}
					}
				}

				int iWidth = 0;
				for( int i = 0, j = mlstColumnWidth.Count; i < j; i++ )
				{
					iWidth += mlstColumnWidth[i];
				}
				if( iWidth < miInitWidth )
				{
					this.Width = miInitWidth + 2;
				}
				else
				{
					this.Width = iWidth + 20;
				}
			}
		}

		private void lstBox_Click( object sender, EventArgs e )
		{
			try
			{
				// 估计是因为 popup 出来的窗口没有焦点，鼠标点击某行无法令该行选中
				// 所以这里要自己实现这个鼠标点击选中的功能
				Point point = lstBox.PointToClient( Control.MousePosition );
				int index = 0;
				for( index = 0; index < lstBox.Items.Count; index++ )
				{
					Rectangle rect = lstBox.GetItemRectangle( index );
					if( rect.Contains( point ) )
					{
						lstBox.SelectedIndex = index;
						break;
					}
				}

				// 返回
				SelectRowReturn( lstBox.SelectedIndex );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		internal void SelectRowReturn( int index )
		{
			if( index < 0 && lstBox.Items.Count > 0 )
			{
				index = 0;
			}

			if( index >= 0 && index < lstBox.Items.Count )
			{
				DataRowView drv = mdvSource[index];
				if( drv != null )
				{
					#region -- 由于 PromptStep 查询的不是所有列，这里需要重新对这一行查询所有字段 --

					bool bHasSetSelectedRow = false;
					TSTextBox txtBox = mOwnerControl as TSTextBox;
					if( txtBox != null )
					{
						string strColName;
						switch( txtBox.ValueMember )
						{
							case enTSTextBoxMemberType.ID:
								strColName = txtBox.IDColumnName;
								break;

							case enTSTextBoxMemberType.Code:
								strColName = txtBox.CodeColumnName;
								break;

							case enTSTextBoxMemberType.Text:
							default:
								strColName = txtBox.TextColumnName;
								break;
						}

						if( drv.Row.Table.Columns.Contains( strColName ) )
						{
							TSEditorSetValueEventArgs args = new TSEditorSetValueEventArgs( txtBox.ValueMember, drv[strColName], index );
							txtBox.OnTSEditorSetValue( args );
							if( args.ValueDataRow != null )
							{
								bHasSetSelectedRow = true;
								mdrSelected = args.ValueDataRow;
							}
						}
					}
					else if( mOwnerControl is DataGridViewTextBoxEditingControl )
					{
                        //TODO TSDataGridView 注释
      //                  DataGridViewTextBoxEditingControl editingControl = (DataGridViewTextBoxEditingControl)mOwnerControl;
						//TSDataGridView gridView = (TSDataGridView)editingControl.EditingControlDataGridView;
						//TSDataGridViewTextBoxColumn column = gridView.Columns[miGridColumnIndex] as TSDataGridViewTextBoxColumn;

						//if( column != null )
						//{
						//	string strColName;
						//	switch( column.ValueMember )
						//	{
						//		case enTSTextBoxMemberType.ID:
						//			strColName = column.IDColumnName;
						//			break;

						//		case enTSTextBoxMemberType.Code:
						//			strColName = column.CodeColumnName;
						//			break;

						//		case enTSTextBoxMemberType.Text:
						//		default:
						//			strColName = column.TextColumnName;
						//			break;
						//	}

						//	if( drv.Row.Table.Columns.Contains( strColName ) )
						//	{
						//		TSEditorSetValueEventArgs args = new TSEditorSetValueEventArgs( column.ValueMember, drv[strColName], index );
						//		gridView.OnInputPromptSetValue( args );
						//		if( args.ValueDataRow != null )
						//		{
						//			bHasSetSelectedRow = true;
						//			mdrSelected = args.ValueDataRow;
						//		}
						//	}
						//}
					}

					#endregion -- 由于 PromptStep 查询的不是所有列，这里需要重新对这一行查询所有字段 --

					if( !bHasSetSelectedRow )
					{
						mdrSelected = drv.Row;
					}

					this.Hide();
					OnPromptReturn();

					if( txtBox != null )
					{
						txtBox.SetSelectedIndexInternal( index );
					}
				}
			}
		}

		private void OnPromptReturn()
		{
			if( PromptReturn != null )
			{
				PromptReturnArgs args = new PromptReturnArgs( mdrSelected, null );
				PromptReturn( this, args );
			}
		}

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

		#region IPromptForm Members

		public event PromptReturnEventHandler PromptReturn;

		#endregion
	}
}