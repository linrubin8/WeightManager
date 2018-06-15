using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using LB.Controls.Args;

namespace LB.Controls.LBEditor
{
	public class TSTextBox : Control, ITSEditor
	{
		internal static int MC_iCaptionLabelTop = 3;
		internal static int MC_iErrorWidth = 5;
		private static int MC_iErrorLeftBase = -10;
		private const int MC_iPromptButtonWidth = 20;
		private const int MC_iTextBoxMinWidth = 80;
		private Color mBorderColor = Color.FromArgb( 49, 106, 197 );

		private readonly Color MC_clrDropDownButtonDisable = SystemColors.Control;
		private readonly Color MC_clrDropDownButtonFocus = Color.FromArgb( 193, 210, 238 );
		private readonly Color MC_clrDropDownButtonDropedDown = Color.FromArgb( 152, 181, 226 );
		private readonly Color MC_clrDropDownButtonArrowDisable = Color.DarkGray;
		private readonly Color MC_clrDropDownButtonArrowFocus = Color.Black;
		private readonly Color MC_clrDropDownButtonArrowDropedDown = Color.FromArgb( 73, 73, 73 );

		//private LinkLabel mlblCaption = null;
		//private Label mlblErrorMsg = null;
		private ToolTip mtpError = null;
		private int miCaptionWidth = 0;
		private string mstrCaption = "";
		private string mstrErrorMsg = null;
		private Color mColorCaptionFore = System.Drawing.SystemColors.WindowText;

		private TextBox txtBox = null;
		private enBorderSides mBorderSides = enBorderSides.Bottom;
		private enPromptButtonType mPromptButtonType = enPromptButtonType.None;
		private bool mbIsPromptHot = false;
		private bool mbReadOnly = false;
		private bool mbTextBoxReadOnly = false;
		private string mstrCaptionValidation = "";
		private bool mbPromptStepAllowNotExists = false;
		private string mstrToolTipCaption = "";
		private bool mbShowImageBox = false;

		public event PromptClickEventHandler PromptClick;
		public event EventHandler SelectedIndexChanged;
		public event EventHandler ReadOnlyChanged;
		public event EventHandler ValueChangedByEdit;
		public event EventHandler DataSourceChanged;
		public event TabKeyPressEventHandler TabKeyPress;

		public event HandledEventHandler PopupAddClicked;
		public event HandledEventHandler PopupManageClicked;
		public event ImageBoxClickedEventHandler ImageBoxClicked;
		public event SetSelectedImageEventHandler SetSelectedImageByPrompt;

		public TSTextBox()
		{
			PlaceTextBox();
			ChangeTextBoxBackColor();

			mtpError = new ToolTip();

			this.TabStop = false;
			//this.DoubleBuffered = true;
		}

		// 测试代码
		//int iii = 0;
		//protected override void OnTextChanged( EventArgs e )
		//{
		//	base.OnTextChanged( e );
		//	iii++;
		//	System.Diagnostics.Debug.WriteLine( "OnTextChanged: " + iii.ToString() + " | " + this.Text );
		//}

		public override string ToString()
		{
			return ( "TSTextBox { Name=" + this.Name + " }" );
		}

		#region -- 与数据类型相关属性 --

		private enTSTextBoxValueType mDataType = enTSTextBoxValueType.String;
		private bool mbCanBeEmpty = false;
		private string mstrMaxValue = "";
		private string mstrMinValue = "";
		private int miMaxLength = -1;
		private string mstrFormatString;
		private bool mbIsPercent = false;
		private bool mbIsCode = false;
		private bool mbIsNChar = false;

		[DefaultValue( "" )]
		public string CaptionValidation
		{
			get
			{
				return mstrCaptionValidation;
			}
			set
			{
				this.mstrCaptionValidation = value;
			}
		}

		[DefaultValue( enTSTextBoxValueType.String )]
		public enTSTextBoxValueType TSValueType
		{
			get
			{
				return mDataType;
			}
			set
			{
				if( mDataType != value )
				{
					mDataType = value;

					this.PlaceTextBox();
					this.SetTextBoxReadOnly();
					this.Invalidate();
				}
			}
		}

		[DefaultValue( false )]
		public bool CanBeEmpty
		{
			get
			{
				return mbCanBeEmpty;
			}
			set
			{
				if( mbCanBeEmpty != value )
				{
					mbCanBeEmpty = value;
					AddEmptyRowToDataSource();
					//ReCalCaptionWidth();
					//PlaceTextBox();
					this.Invalidate();
				}
			}
		}

		[DefaultValue( -1 )]
		public int MaxLength
		{
			get
			{
				return miMaxLength;
			}
			set
			{
				if( miMaxLength != value && value > 0 )
				{
					if( txtBox != null )
					{
						txtBox.MaxLength = value;
					}
				}
			}
		}

		[DefaultValue( "" )]
		public string MinValue
		{
			get
			{
				return mstrMinValue;
			}
			set
			{
				mstrMinValue = value;
			}
		}

		[DefaultValue( "" )]
		public string MaxValue
		{
			get
			{
				return mstrMaxValue;
			}
			set
			{
				mstrMaxValue = value;
			}
		}

		[DefaultValue( false )]
		public bool IsCode
		{
			get
			{
				return mbIsCode;
			}
			set
			{
				this.mbIsCode = value;
			}
		}

		[DefaultValue( false )]
		public bool IsNChar
		{
			get
			{
				return mbIsNChar;
			}
			set
			{
				this.mbIsNChar = value;
			}
		}

		[DefaultValue( false )]
		public bool IsPercent
		{
			get
			{
				return mbIsPercent;
			}
			set
			{
				mbIsPercent = value;
			}
		}

		[DefaultValue( null )]
		public string FormatString
		{
			get
			{
				return mstrFormatString;
			}
			set
			{
				mstrFormatString = value;
			}
		}

		#endregion -- 与数据类型相关属性 --

		#region -- 校验 --

		public bool Validation( out string strErrorMsg )
		{
			string strCaption = this.CaptionValidation;
			if( strCaption == "" || strCaption == null )
			{
				strCaption = this.Caption;
			}

			bool bValid = CommonFuntion.Validation(
				out strErrorMsg, this.Text4Validation,
				this.TextBoxReadOnly, strCaption, this.TSValueType,
				this.CanBeEmpty, this.MaxLength, this.MinValue, this.MaxValue,
				this.IsCode, this.IsNChar, this.IsPercent, this.FormatString );
			if( !bValid )
			{
				this.SetErrorMessage( strErrorMsg );
			}
			else
			{
				this.SetErrorMessage( "" );
			}
			return bValid;
		}

		protected virtual string Text4Validation
		{
			get
			{
				if( this.TSValueType == enTSTextBoxValueType.DateTime )
				{
					return this.Text;
				}
				else
				{
					object objValue = this.Value4DB;
					if( objValue == null )
					{
						return "";
					}
					else
					{
						return this.Value4DB.ToString();
					}
				}
			}
		}

		#endregion -- 校验 --

		#region -- 属性 --

		internal TextBox InnerTextBox
		{
			get
			{
				return txtBox;
			}
		}

		[DefaultValue( false )]
		public bool ReadOnly
		{
			get
			{
				return mbReadOnly;
			}
			set
			{
				if( mbReadOnly != value )
				{
					mbReadOnly = value;
					this.SetTextBoxReadOnly();
					this.ChangeTextBoxSize();

					// 只读变更
					OnReadOnlyChanged( EventArgs.Empty );
				}
			}
		}

		protected virtual void OnReadOnlyChanged( EventArgs e )
		{
			if( ReadOnlyChanged != null )
			{
				ReadOnlyChanged( this, e );
			}
		}

		protected override void OnEnabledChanged( EventArgs e )
		{
			base.OnEnabledChanged( e );

			try
			{
				this.PlaceTextBox();
				this.Invalidate();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		[DefaultValue( false )]
		public bool TextBoxReadOnly
		{
			get
			{
				return mbTextBoxReadOnly;
			}
			set
			{
				if( mbTextBoxReadOnly != value )
				{
					mbTextBoxReadOnly = value;
					this.SetTextBoxReadOnly();
				}
			}
		}

		private void SetTextBoxReadOnly()
		{
			if( this.PromptButtonType == enPromptButtonType.Popup ||
				this.PromptButtonType == enPromptButtonType.PopupAllowPrompt )
			{
				txtBox.ReadOnly = true;
			}
			else
			{
				txtBox.ReadOnly = ( this.TextBoxReadOnly || this.ReadOnly );
			}
		}

		[DefaultValue( System.Windows.Forms.HorizontalAlignment.Left )]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return txtBox.TextAlign;
			}
			set
			{
				txtBox.TextAlign = value;
			}
		}

		[DefaultValue( typeof( Color ), "49,106,197" )]
		public Color BorderColor
		{
			get
			{
				return mBorderColor;
			}
			set
			{
				if( mBorderColor != value )
				{
					mBorderColor = value;
				}
			}
		}

		[DefaultValue( enBorderSides.Bottom )]
		public enBorderSides BorderSides
		{
			get
			{
				return mBorderSides;
			}
			set
			{
				if( mBorderSides != value )
				{
					mBorderSides = value;

					this.Invalidate();
				}
			}
		}

		[DefaultValue( enPromptButtonType.None )]
		public virtual enPromptButtonType PromptButtonType
		{
			get
			{
				return mPromptButtonType;
			}
			set
			{
				if( mPromptButtonType != value )
				{
					mPromptButtonType = value;

					this.PlaceTextBox();
					this.SetTextBoxReadOnly();
					this.Invalidate();
				}
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ), Browsable( true ), DefaultValue( (string)null )]
		public new string Text
		{
			get
			{
				return this.txtBox.Text;
			}
			set
			{
				this.txtBox.Text = CommonFuntion.Format4Editor(
					value, this.FormatString,
					this.TSValueType, this.PromptButtonType, this.TextBoxReadOnly );
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public object Value4DB
		{
			get
			{
				string strText = this.Text;
				switch( this.PromptButtonType )
				{
					case enPromptButtonType.ComboStep:
					case enPromptButtonType.PromptStep:
						if( this.PromptStepAllowNotExists )
						{
							if( strText != "" )
							{
								return strText;
							}
							else
							{
								return DBNull.Value;
							}
						}
						else
						{
							if( this.SelectedValue != null )
							{
								return this.SelectedValue;
							}
							else if( strText != "" )
							{
								return strText;
							}
							else
							{
								return DBNull.Value;
							}
						}

					case enPromptButtonType.Popup:
					case enPromptButtonType.PopupAllowPrompt:
						if( this.SelectedValue != null )
						{
							return this.SelectedValue;
						}
						else if( strText != "" )
						{
							switch( this.TSValueType )
							{
								case enTSTextBoxValueType.DateTime:
									try
									{
										DateTime dtTemp = Convert.ToDateTime( strText );
										strText = dtTemp.ToString( "yyyy-MM-dd HH:mm:ss" );
									}
									catch
									{
										return DBNull.Value;
									}
									break;

								case enTSTextBoxValueType.Number:
									try
									{
										decimal decTemp = Convert.ToDecimal( strText );
									}
									catch
									{
										return DBNull.Value;
									}
									break;

								case enTSTextBoxValueType.String:
								default:
									break;
							}
							return strText;
						}
						else
						{
							return DBNull.Value;
						}

					case enPromptButtonType.None:
					case enPromptButtonType.Prompt:
					default:
						if( this.TSValueType == enTSTextBoxValueType.DateTime )
						{
							if( strText == "" )
							{
								return DBNull.Value;
							}
							else
							{
								return Convert.ToDateTime( strText );
							}
						}
						else if( this.TSValueType == enTSTextBoxValueType.Number )
						{
							if( strText == "" )
							{
								return DBNull.Value;
							}
							else
							{
								return strText;
							}
						}
						else
						{
							return strText;
						}
				}
			}
			set
			{
				bool bSetText = false;
				switch( this.PromptButtonType )
				{
					case enPromptButtonType.ComboStep:
					case enPromptButtonType.PromptStep:
						if( this.PromptStepAllowNotExists )
						{
							bSetText = true;
						}
						else
						{
							this.SelectedValue = value;
							if( value != null && value != DBNull.Value && this.SelectedValue == null )
							{
								bSetText = true;
							}
						}
						break;

					case enPromptButtonType.Popup:
					case enPromptButtonType.PopupAllowPrompt:
						this.SelectedValue = value;
						//if( value != null && value != DBNull.Value && this.SelectedValue == null )
						//{
						//    bSetText = true;
						//}
						break;

					case enPromptButtonType.None:
					case enPromptButtonType.Prompt:
					default:
						bSetText = true;
						break;
				}

				if( bSetText )
				{
					if( value == null || value == DBNull.Value )
					{
						this.Text = "";
					}
					else
					{
						this.Text = value.ToString().TrimEnd();
					}
				}
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden ), Browsable( false )]
		public char PasswordChar
		{
			get
			{
				if( this.txtBox != null )
				{
					return this.txtBox.PasswordChar;
				}
				else
				{
					return '\0';
				}
			}
			set
			{
				if( this.txtBox != null )
				{
					this.txtBox.PasswordChar = value;
				}
			}
		}

		[DefaultValue( false )]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden ), Browsable( false )]
		public CharacterCasing CharacterCasing
		{
			get
			{
				if( this.txtBox != null )
				{
					return this.txtBox.CharacterCasing;
				}
				else
				{
					return CharacterCasing.Normal;
				}
			}
			set
			{
				if( this.txtBox != null )
				{
					this.txtBox.CharacterCasing = value;
				}
			}
		}

		[DefaultValue( false )]
		public bool Multiline
		{
			get
			{
				if( txtBox != null )
				{
					return txtBox.Multiline;
				}
				return false;
			}
			set
			{
				if( txtBox != null && txtBox.Multiline != value )
				{
					txtBox.Multiline = value;
					txtBox.AcceptsReturn = value;
					if( value )
					{
						txtBox.ScrollBars = ScrollBars.Both;
						txtBox.WordWrap = false;
					}
					else
					{
						txtBox.ScrollBars = ScrollBars.None;
					}
					this.ChangeTextBoxSize();
					this.ChangeTextBoxBackColor();
				}
			}
		}

		public bool WordWrap
		{
			get
			{
				if( txtBox != null )
				{
					return txtBox.WordWrap;
				}
				return false;
			}
			set
			{
				if( txtBox != null && txtBox.WordWrap != value )
				{
					txtBox.WordWrap = value;
				}
			}
		}

		protected virtual void OnValueChangedByEdit( EventArgs e )
		{
			if( ValueChangedByEdit != null )
			{
				ValueChangedByEdit( this, e );
			}
		}

		public void FireValueChangedByEdit()
		{
			OnValueChangedByEdit( EventArgs.Empty );
		}

		public int SelectionStart
		{
			get
			{
				if( txtBox != null )
				{
					return txtBox.SelectionStart;
				}
				else
				{
					return -1;
				}
			}
			set
			{
				if( txtBox != null )
				{
					txtBox.SelectionStart = value;
				}
			}
		}

		public override bool Focused
		{
			get
			{
				if( base.Focused )
				{
					return true;
				}
				else if( txtBox != null && txtBox.Focused )
				{
					return true;
				}

				return false;
			}
		}

		#endregion -- 属性 --

		#region -- PlaceTextBox --

		private void PlaceTextBox()
		{
			if( txtBox == null )
			{
				txtBox = new TextBox();
				txtBox.AutoSize = false;
				txtBox.BorderStyle = BorderStyle.None;
				txtBox.Location = new Point( 2, 3 );
				this.Controls.Add( txtBox );
				txtBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

				txtBox.GotFocus += new EventHandler( txtBox_GotFocus );
				txtBox.LostFocus += new EventHandler( txtBox_LostFocus );
				txtBox.DoubleClick += new EventHandler( txtBox_DoubleClick );
				txtBox.Click += new EventHandler( txtBox_Click );
				txtBox.ReadOnlyChanged += new EventHandler( txtBox_ReadOnlyChanged );
				txtBox.TextChanged += new EventHandler( txtBox_TextChanged );
				txtBox.TextChanged += new EventHandler( txtBox_TextChanged_Value4DBChanged );
				txtBox.KeyDown += new KeyEventHandler( txtBox_KeyDown );
				txtBox.KeyPress += new KeyPressEventHandler( txtBox_KeyPress );
				txtBox.MouseWheel += new MouseEventHandler( txtBox_MouseWheel );
			}

			if( this.ShowImageBox )
			{
				txtBox.Location = new Point( 2, this.Height - ImageBox_TextBoxHeight + 2 );
			}
			else
			{
				txtBox.Location = new Point( miCaptionWidth + 2, 3 );
			}

			if( txtBox.Multiline )
			{
				txtBox.Size = new Size(
					this.Width - miCaptionWidth - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
					this.Height - txtBox.Top - 2 );
			}
			else if( this.ShowImageBox )
			{
				txtBox.Size = new Size(
					this.Width - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
					this.Font.Height );
			}
			else
			{
				txtBox.Size = new Size(
					this.Width - miCaptionWidth - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
					this.Font.Height );
			}

			txtBox.Visible = true;
			txtBox.BringToFront();
		}

		#endregion -- PlaceTextBox --

		#region -- 标题相关 --

		[DefaultValue( typeof( Color ), "WindowText" )]
		public Color CaptionForeColor
		{
			get
			{
				return mColorCaptionFore;
			}
			set
			{
				mColorCaptionFore = value;
			}
		}

		[DefaultValue( "" )]
		public string Caption
		{
			get
			{
				return mstrCaption;
			}
			set
			{
				if( mstrCaption != value )
				{
					mstrCaption = value;

					ReCalCaptionWidth();

					// 设置输入控件的起始位置
					// 不足四字长以四字长为准，超过四字长的以两字长为增长
					//using( Graphics g = mlblCaption.CreateGraphics() )
					//{
					//    miCaptionWidth = (int)g.MeasureString( value, mlblCaption.Font ).Width;
					//    int iWidth = (int)g.MeasureString( "软件", mlblCaption.Font ).Width;
					//    if( miCaptionWidth <= iWidth )
					//    {
					//        miCaptionWidth = iWidth;
					//    }
					//    //else if( miCaptionWidth <= iWidth * 2 )
					//    //{
					//    //    miCaptionWidth = iWidth * 2;
					//    //}
					//    else
					//    {
					//        float fWidth = g.MeasureString( "软", mlblCaption.Font ).Width;
					//        float fTemp = (float)miCaptionWidth / fWidth;
					//        int iTemp = (int)Math.Floor( fTemp );
					//        if( (float)iTemp != fTemp &&
					//            ( fTemp - (float)iTemp ) > 0.1f )
					//        {
					//            iTemp++;
					//        }
					//        miCaptionWidth = (int)( iTemp * fWidth );
					//    }

					//    miCaptionWidth += MC_iErrorWidth;
					//}

					PlaceTextBox();
				}
			}
		}

		private void ReCalCaptionWidth()
		{
			using( Graphics g = this.CreateGraphics() )
			{
				Font font = new Font( this.Font, FontStyle.Bold );
				miCaptionWidth = (int)g.MeasureString( this.Caption, font ).Width;
				miCaptionWidth += MC_iErrorWidth + 2 /*+ ( mbCanBeEmpty ? -2 : 0 )*/;
			}
		}

		#endregion -- 标题相关 --

		#region -- Prompt 按钮绘画 & 显示风格 --

		private bool IsMouseOnPromptButton()
		{
			Point point = this.PointToClient( Control.MousePosition );

			if( this.ShowImageBox )
			{
				if( point.X >= this.Width - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ) - 1 && point.Y >= this.Height - ImageBox_TextBoxHeight &&
					point.X <= this.Width && point.Y < this.Height )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				if( point.X >= this.Width - 1 - MC_iPromptButtonWidth && point.Y >= 0 &&
					point.X <= this.Width - 1 && point.Y < this.Height )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		private bool IsMouseOnImageBox()
		{
			Point point = this.PointToClient( Control.MousePosition );

			if( this.ShowImageBox )
			{
				if( point.X >= 1 && point.Y >= ImageBox_LabelHeight &&
					point.X <= this.Width && point.Y < this.Height - ImageBox_TextBoxHeight )
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			return false;
		}

		void txtBox_ReadOnlyChanged( object sender, EventArgs e )
		{
			try
			{
				ChangeTextBoxBackColor();

				if( txtBox.ReadOnly )
				{
					txtBox.Cursor = Cursors.Default;
				}
				else
				{
					txtBox.Cursor = Cursors.IBeam;
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnBackColorChanged( EventArgs e )
		{
			base.OnBackColorChanged( e );

			try
			{
				ChangeTextBoxBackColor();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void ChangeTextBoxBackColor()
		{
			if( ( txtBox.ReadOnly || !txtBox.Focused ) && !txtBox.Multiline )
			{
				txtBox.BackColor = this.BackColor;
			}
			else
			{
				txtBox.BackColor = Color.White;
			}
		}

		protected override void OnSizeChanged( EventArgs e )
		{
			base.OnSizeChanged( e );

			PlaceTextBox();
			ChangeTextBoxSize();
		}

		private void ChangeTextBoxSize()
		{
			try
			{
				if( txtBox != null )
				{
					if( txtBox.Multiline )
					{
						txtBox.Size = new Size(
							this.Width - miCaptionWidth - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
							this.Height - txtBox.Top - 2 );
					}
					else if( this.ShowImageBox )
					{
						txtBox.Size = new Size(
							this.Width - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
							this.Font.Height );
					}
					else
					{
						txtBox.Size = new Size(
							this.Width - miCaptionWidth - 7 - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ),
							this.Font.Height );
					}
				}
				this.Invalidate();
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private bool DisplayPromptButton()
		{
			if( this.Enabled && !this.ReadOnly &&
				( this.PromptButtonType != enPromptButtonType.None || this.TSValueType == enTSTextBoxValueType.DateTime ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private int ImageBox_LabelHeight
		{
			get
			{
				return this.Font.Height + 3;
			}
		}

		private int ImageBox_TextBoxHeight
		{
			get
			{
				return 18;
			}
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			//base.OnPaint( e );

			try
			{
				using( Pen pen = new Pen( this.BorderColor ) )
				{
					Graphics g = e.Graphics;

					// TextBox 底色
					using( SolidBrush brush = new SolidBrush( this.txtBox.BackColor ) )
					{
						if( this.ShowImageBox )
						{
							g.FillRectangle( brush, 0, this.Height - ImageBox_TextBoxHeight, this.Width - 1, ImageBox_TextBoxHeight );
						}
						else
						{
							g.FillRectangle( brush, miCaptionWidth, 0, this.Width - miCaptionWidth - 1, this.Height - 1 );
						}
					}

					// 画标题
					Color colorCaption;
					if( this.ReadOnly || !this.Enabled )
					{
						colorCaption = Color.FromArgb( 90, 90, 90 );
					}
					else
					{
						colorCaption = this.CaptionForeColor;
					}
					using( SolidBrush brushCaption = new SolidBrush( colorCaption ) )
					{
						Font font;
						if( this.CanBeEmpty )
						{
							font = new Font( this.Font, FontStyle.Regular );
						}
						else
						{
							font = new Font( this.Font, FontStyle.Bold );
						}
						g.DrawString( this.Caption, font, brushCaption, new PointF( 0, 3 ) );
					}

					// 画错误标识
					if( !string.IsNullOrEmpty( mstrErrorMsg ) )
					{
						using( SolidBrush brushCaption = new SolidBrush( Color.Red ) )
						{
							Font font = new Font( this.Font.FontFamily, 11f, FontStyle.Bold );
							g.DrawString( "！", font, brushCaption, new PointF( miCaptionWidth + MC_iErrorLeftBase, 3 ) );
						}
					}

					// 画边框
					if( this.BorderSides != enBorderSides.None )
					{
						if( this.BorderSides == enBorderSides.All || this.ShowImageBox )
						{
							if( this.ShowImageBox )
							{
								g.DrawRectangle( pen, 0, this.Height - ImageBox_TextBoxHeight - 1, this.Width - 1, ImageBox_TextBoxHeight );
							}
							else
							{
								g.DrawRectangle( pen, miCaptionWidth, 0, this.Width - miCaptionWidth - 1, this.Height - 1 );
							}

							// 画按钮前的竖线
							if( this.DisplayPromptButton() )
							{
								if( this.ShowImageBox )
								{
									g.DrawLine(
										pen,
										this.Width - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ) - 1, ImageBox_TextBoxHeight + 1,
										this.Width - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ) - 1, this.Height - 1 );
								}
								else
								{
									g.DrawLine(
										pen,
										this.Width - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ) - 1, 0,
										this.Width - ( DisplayPromptButton() ? MC_iPromptButtonWidth : 0 ) - 1, this.Height - 1 );
								}
							}

							if( this.ShowImageBox )
							{
								// 底色
								using( SolidBrush brush = new SolidBrush( Color.WhiteSmoke ) )
								{
									g.FillRectangle( brush, 0, ImageBox_LabelHeight, this.Width - 1, this.Height - ImageBox_TextBoxHeight - ImageBox_LabelHeight );
								}

								int width = this.Width - 2;
								int height = this.Height - ImageBox_TextBoxHeight - ImageBox_LabelHeight - 1;

								if( width > 0 && height > 0 )
								{
									if( this.SelectedImage != null )
									{
										float fRateImg = (float)this.SelectedImage.Width / (float)this.SelectedImage.Height;
										float fRate = (float)width / (float)height;
										int widthImg;
										int heightImg;
										if( fRateImg >= fRate )
										{
											widthImg = width;
											heightImg = (int)( (float)width * (float)this.SelectedImage.Height / (float)this.SelectedImage.Width );
										}
										else
										{
											heightImg = height;
											widthImg = (int)( (float)height * (float)this.SelectedImage.Width / (float)this.SelectedImage.Height );
										}

										g.DrawImage( this.SelectedImage, 1 + ( width - widthImg ) / 2, 1 + ImageBox_LabelHeight + ( height - heightImg ) / 2, widthImg, heightImg );
									}
									else
									{
										using( SolidBrush brush = new SolidBrush( Color.Gray ) )
										{
											int fontSize = height / 3;
											if( fontSize < 8 )
											{
												fontSize = 8;
											}
											Font font = new Font( this.txtBox.Font.FontFamily, fontSize );
											SizeF size = g.MeasureString( "图片", font );
											g.DrawString( "图片", font, brush, 2 + ( width - size.Width ) / 2, 3 + ( height - size.Height ) / 2 + ImageBox_TextBoxHeight );
										}
									}
								}

								// 图片边框
								g.DrawRectangle( pen, 0, ImageBox_LabelHeight, this.Width - 1, this.Height - ImageBox_TextBoxHeight - ImageBox_LabelHeight );

								//g.DrawLine(
								//    pen,
								//    1, this.Height - ImageBox_TextBoxHeight - ImageBox_LabelHeight,
								//    this.Width - 1, this.Height - ImageBox_TextBoxHeight - ImageBox_LabelHeight );
							}
						}
						else
						{
							// Bottom
							if( ( this.BorderSides & enBorderSides.Bottom ) == enBorderSides.Bottom )
							{
								g.DrawLine( pen, miCaptionWidth, this.Height - 1, this.Width - 1, this.Height - 1 );
							}

							// Left
							if( ( this.BorderSides & enBorderSides.Left ) == enBorderSides.Left )
							{
								g.DrawLine( pen, miCaptionWidth, 0, miCaptionWidth, this.Height - 1 );
							}

							// Right
							if( ( this.BorderSides & enBorderSides.Right ) == enBorderSides.Right )
							{
								g.DrawLine( pen, this.Width - 1, 0, this.Width - 1, this.Height - 1 );
							}

							// Top
							if( ( this.BorderSides & enBorderSides.Top ) == enBorderSides.Top )
							{
								g.DrawLine( pen, miCaptionWidth, 0, this.Width - 1, 0 );
							}
						}
					}

					// 画按钮
					if( DisplayPromptButton() )
					{
						mbIsPromptHot = this.Focused || IsMouseOnPromptButton();
						int iHeightImgBtn = 0;
						if( this.ShowImageBox )
						{
							iHeightImgBtn = this.Height - ImageBox_TextBoxHeight - 1;
						}

						switch( this.PromptButtonType )
						{
							case enPromptButtonType.Prompt:
							case enPromptButtonType.PromptStep:
								g.DrawImage(
									( mbIsPromptHot ? LB.Properties.Resources.PromptHot : LB.Properties.Resources.Prompt ),
									this.Width - 1 - MC_iPromptButtonWidth, iHeightImgBtn );
								break;

							case enPromptButtonType.Popup:
							case enPromptButtonType.PopupAllowPrompt:
							case enPromptButtonType.Combo:
							case enPromptButtonType.ComboStep:
								DrawDropDownButton( g );
								DrawDropDownArrow( g );
								break;

							case enPromptButtonType.None:
								if( this.TSValueType == enTSTextBoxValueType.DateTime )
								{
									g.DrawImage(
										( mbIsPromptHot ? LB.Properties.Resources.CalendarHot : LB.Properties.Resources.Calendar ),
										this.Width - 1 - MC_iPromptButtonWidth, iHeightImgBtn );
								}
								break;
						}
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void DrawDropDownButton( Graphics g )
		{
			Color colorButton = ( CommonFuntion.TSEditorChangeStyleByFocus ? this.BackColor : MC_clrDropDownButtonDisable );
			if( this.Enabled )
			{
				if( mbIsPromptHot )
				{
					colorButton = MC_clrDropDownButtonDropedDown;
				}
				else
				{
					colorButton = this.BackColor;
				}
			}

			int iTopImgBtn = 1;
			int iHeight = this.Height - 2;
			if( this.ShowImageBox )
			{
				iTopImgBtn = this.Height - ImageBox_TextBoxHeight + 1;
				iHeight = ImageBox_TextBoxHeight - 2;
			}

			using( Brush brushButton = new SolidBrush( colorButton ) )
			{
				Rectangle rectButton = new Rectangle( this.Width - MC_iPromptButtonWidth, iTopImgBtn, MC_iPromptButtonWidth - 1, iHeight );
				g.FillRectangle( brushButton, rectButton );
			}

			if( this.Focused && IsMouseOnPromptButton() )
			{
				using( Pen penBorder = new Pen( this.BorderColor ) )
				{
					g.DrawLine( penBorder, this.Width - MC_iPromptButtonWidth - 1, iTopImgBtn, this.Width - MC_iPromptButtonWidth - 1, iHeight );
				}
			}

			DrawDropDownArrow( g );
		}

		private void DrawDropDownArrow( Graphics g )
		{
			PointF[] pntArrow = new PointF[3];
			int iVMiddle;
			if( this.ShowImageBox )
			{
				iVMiddle = this.Height - ImageBox_TextBoxHeight + (int)Math.Round( (double)( ( (double)ImageBox_TextBoxHeight ) / 2 ) );
			}
			else
			{
				iVMiddle = (int)Math.Round( (double)( ( (double)this.Height ) / 2 ) );
			}
			int iHMiddle = this.Width - MC_iPromptButtonWidth / 2 - 1;
			pntArrow[0] = new PointF( (float)( iHMiddle - 4 ), (float)( iVMiddle - 2 ) );
			pntArrow[1] = new PointF( (float)iHMiddle, (float)( iVMiddle + 2 ) );
			pntArrow[2] = new PointF( (float)( iHMiddle + 4 ), (float)( iVMiddle - 2 ) );

			Color colorButton = MC_clrDropDownButtonArrowDisable;
			if( this.Enabled )
			{
				colorButton = MC_clrDropDownButtonArrowFocus;
			}

			using( Brush brushArrow = new SolidBrush( colorButton ) )
			{
				g.FillPolygon( brushArrow, pntArrow );
			}
		}

		protected override void OnMouseEnter( EventArgs e )
		{
			base.OnMouseEnter( e );

			try
			{
				if( this.DisplayPromptButton() && !this.ShowImageBox )
				{
					Rectangle rectButton = new Rectangle( this.Width - 1 - MC_iPromptButtonWidth, 1, MC_iPromptButtonWidth, this.Height - 2 );
					this.Invalidate( rectButton );
				}
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
				if( this.DisplayPromptButton() && !this.ShowImageBox )
				{
					Rectangle rectButton = new Rectangle( this.Width - 1 - MC_iPromptButtonWidth, 1, MC_iPromptButtonWidth, this.Height - 2 );
					this.Invalidate( rectButton );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnMouseMove( MouseEventArgs e )
		{
			base.OnMouseMove( e );

			try
			{
				if( this.DisplayPromptButton() && !this.ShowImageBox )
				{
					bool bHot = this.Focused || IsMouseOnPromptButton();
					if( bHot != mbIsPromptHot )
					{
						this.Invalidate();
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		#endregion -- Prompt 按钮绘画 & 显示风格 --

		#region -- Prompt 相关处理 --

		protected override void OnGotFocus( EventArgs e )
		{
			base.OnGotFocus( e );

			try
			{
				if( this.txtBox != null )
				{
					this.txtBox.Focus();
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void txtBox_GotFocus( object sender, EventArgs e )
		{
			try
			{
				mbTxtBoxFocusedFirstClicked = true;
				mbTextChanged_Value4DBChanged = false;
				if( ( this.PromptButtonType != enPromptButtonType.PromptStep && this.PromptButtonType != enPromptButtonType.Combo && this.PromptButtonType != enPromptButtonType.ComboStep ) ||
					!this.PromptStepAllowNotExists )
				{
					txtBox.TextChanged -= new EventHandler( txtBox_TextChanged );
					this.SetTextByMember();
					txtBox.TextChanged += new EventHandler( txtBox_TextChanged );
				}

				this.BorderSides = enBorderSides.All;
				this.ChangeTextBoxBackColor();

				// 定位光标在数字小数点的前面
				SetCursorInNumber();

				base.OnGotFocus( e );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private void SetCursorInNumber()
		{
			if( this.TSValueType == enTSTextBoxValueType.Number )
			{
				bool bIsValid = false;
				try
				{
					decimal decTemp = Convert.ToDecimal( txtBox.Text );
					bIsValid = true;
				}
				catch
				{
				}

				if( bIsValid && txtBox.Text.IndexOf( "." ) > 0 )
				{
					txtBox.SelectionStart = txtBox.Text.IndexOf( "." );
					txtBox.SelectionLength = 0;
					//System.Diagnostics.Debug.WriteLine( this.Name + " SelectionStart :" + this.txtBox.SelectionStart.ToString() );
				}
			}
		}

		void txtBox_LostFocus( object sender, EventArgs e )
		{
			try
			{
				this.BorderSides = enBorderSides.Bottom;
				this.ChangeTextBoxBackColor();

				if( this.Focused )
				{
					return;
				}

				// 先执行校验，以便取得 Selected 的几个值。再执行 OnLostFocus 触发事件
				// 否则在 LostFocus 中取值可能有误
				string strErrorMsg = "";
				bool bPass = TSCodeValidationAndProcessor( out strErrorMsg );

				this.OnLostFocus( e );

				if( !bPass )
				{
					try
					{
						this.txtBox.LostFocus -= new EventHandler( txtBox_LostFocus );

						DialogResult result = MessageBox.Show(
							strErrorMsg + Environment.NewLine + "是 - 清空并重新输入" + Environment.NewLine + "否 - 不清空并继续输入",
							"提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1 );
						this.txtBox.Focus();
						if( result == DialogResult.Yes )
						{
							this.txtBox.Text = "";
						}
					}
					finally
					{
						this.txtBox.LostFocus += new EventHandler( txtBox_LostFocus );
					}
				}
				else if( !this.PromptStepAllowNotExists )
				{
					this.SetTextByMember();
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnLostFocus( EventArgs e )
		{
			base.OnLostFocus( e );

			try
			{
				if( mbTextChanged_Value4DBChanged &&
					( this.PromptButtonType == enPromptButtonType.None ||
					this.PromptButtonType == enPromptButtonType.Prompt ||
					this.PromptButtonType == enPromptButtonType.PromptStep ||
					this.PromptButtonType == enPromptButtonType.Combo ||
					this.PromptButtonType == enPromptButtonType.ComboStep ) )
				{
					mbTextChanged_Value4DBChanged = false;
					this.OnValueChangedByEdit( EventArgs.Empty );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private bool mbTextChanged_Value4DBChanged = false;
		void txtBox_TextChanged_Value4DBChanged( object sender, EventArgs e )
		{
			if( this.PromptButtonType == enPromptButtonType.None ||
				this.PromptButtonType == enPromptButtonType.Prompt ||
				this.PromptButtonType == enPromptButtonType.PromptStep ||
				this.PromptButtonType == enPromptButtonType.Combo ||
				this.PromptButtonType == enPromptButtonType.ComboStep )
			{
				//if( !this.Focused && !this.txtBox.Focused )
				//{
				//    // 没有焦点，不触发事件
				//    ////this.OnTSFireValue4DBChanged( EventArgs.Empty );
				//}
				//else
				{
					mbTextChanged_Value4DBChanged = true;
				}
			}
		}

		public void ResetValueChangedByEdit()
		{
			mbTextChanged_Value4DBChanged = false;
		}

		void txtBox_MouseWheel( object sender, MouseEventArgs e )
		{
			try
			{
				if( this.mPopup != null && this.mPopup.Visible &&
					this.mPopup.DataSource != null && this.mPopup.DataSource.Count > 0 )
				{
					// 鼠标滚动一下 120，向下为负，向上为正
					int iIndex = this.mPopup.SelectedIndex - e.Delta / 40;

					if( iIndex >= 0 && iIndex <= this.mPopup.DataSource.Count - 1 )
					{
						this.mPopup.SelectedIndex = iIndex;
					}
					else if( iIndex < 0 )
					{
						this.mPopup.SelectedIndex = 0;
					}
					else
					{
						this.mPopup.SelectedIndex = this.mPopup.DataSource.Count - 1;
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void txtBox_KeyDown( object sender, KeyEventArgs e )
		{
			try
			{
				base.OnKeyDown( e );

				if( e.Handled )
				{
					return;
				}

				#region -- Keys.Up, Down, Space：显示下拉 --

				if( e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space )
				{
					if( this.PromptButtonType == enPromptButtonType.Popup ||
						this.PromptButtonType == enPromptButtonType.PopupAllowPrompt ||
						this.PromptButtonType == enPromptButtonType.Combo ||
						this.PromptButtonType == enPromptButtonType.ComboStep )
					{
						e.Handled = true;

						if( this.mPopup == null || !this.mPopup.Visible )
						{
							PromptClickEventArgs args = new PromptClickEventArgs( enPromptButtonClickType.Popup, true, this );
							OnPromptClickPrivate( args );
						}
					}
				}

				#endregion -- Keys.Up, Down, Space：显示下拉 --

				#region -- 下拉： Keys.Up, Down, Enter --

				if( this.mPopup != null && this.mPopup.Visible )
				{
					if( e.KeyCode == Keys.Up )
					{
						e.Handled = true;
						if( this.mPopup.SelectedIndex > 0 )
						{
							this.mPopup.SelectedIndex--;
						}
					}
					else if( e.KeyCode == Keys.Down )
					{
						e.Handled = true;
						if( this.mPopup.SelectedIndex < this.mPopup.DataSource.Count - 1 )
						{
							this.mPopup.SelectedIndex++;
						}
					}
					else if( e.KeyCode == Keys.Enter &&
						( ( this.PromptButtonType != enPromptButtonType.PromptStep && this.PromptButtonType != enPromptButtonType.ComboStep )
						|| mThread == null || mThread.ThreadState == ThreadState.Stopped ) )
					{
						e.Handled = true;
						this.mPopup.SelectRowReturn( this.mPopup.SelectedIndex );
						this.mPopup.Hide();
					}
				}

				#endregion -- 下拉： Keys.Up, Down, Enter --

				#region -- 日历： Keys.Up, Down, Enter --

				if( this.mPopup4Calendar != null && this.mPopup4Calendar.Visible )
				{
					if( e.KeyCode == Keys.Up )
					{
						e.Handled = true;
						this.mPopup4Calendar.SelectUp();
					}
					else if( e.KeyCode == Keys.Down )
					{
						e.Handled = true;
						this.mPopup4Calendar.SelectDown();
					}
					else if( e.KeyCode == Keys.Left )
					{
						e.Handled = true;
						this.mPopup4Calendar.SelectLeft();
					}
					else if( e.KeyCode == Keys.Right )
					{
						e.Handled = true;
						this.mPopup4Calendar.SelectRight();
					}
					else if( e.KeyCode == Keys.Enter )
					{
						e.Handled = true;
						this.mPopup4Calendar.DateSelected();
					}
				}

				#endregion -- 日历： Keys.Up, Down, Enter --

				if( e.KeyCode == Keys.Enter )
				{
					if( this.Multiline )
					{
						return;
					}

					e.Handled = true;
					SendKeys.Send( "{TAB}" );
				}
				else if( (int)e.KeyData == (int)CommonFuntion.TSPromptShortcut )	// 打开提示窗口的快捷键处理
				{
					DoPromptAction();
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void txtBox_KeyPress( object sender, KeyPressEventArgs e )
		{
			try
			{
				base.OnKeyPress( e );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected override void OnClick( EventArgs e )
		{
			base.OnClick( e );

			try
			{
				this.Focus();

				if( this.DisplayPromptButton() && IsMouseOnPromptButton() )
				{
					DoPromptAction();
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected virtual void OnImageBoxClicked( ImageBoxClickEventArgs e )
		{
			if( ImageBoxClicked != null )
			{
				ImageBoxClicked( this, e );
			}
		}

		private void DoClickImageBoxAction()
		{
			ImageBoxClickEventArgs args = new ImageBoxClickEventArgs();
			OnImageBoxClicked( args );

			if( !args.Handled && args.NormalImage != null )
			{
				string strImageName = "";
				if( this.Value4DB != null )
				{
					strImageName = this.Value4DB.ToString().Trim();
				}

				frmFullPictureViewer frm = new frmFullPictureViewer( args.NormalImage, strImageName );
				frm.Show();
			}
		}

		private void DoPromptAction()
		{
			enPromptButtonClickType clickType = enPromptButtonClickType.Popup;
			switch( this.PromptButtonType )
			{
				case enPromptButtonType.Prompt:
				case enPromptButtonType.PromptStep:
					clickType = enPromptButtonClickType.Prompt;
					break;

				case enPromptButtonType.None:
					if( this.TSValueType == enTSTextBoxValueType.DateTime )
					{
						clickType = enPromptButtonClickType.Prompt;
					}
					break;
			}

			PromptClickEventArgs args = new PromptClickEventArgs( clickType, true, this );
			OnPromptClickPrivate( args );
		}

		protected override void OnDoubleClick( EventArgs e )
		{
			base.OnDoubleClick( e );

			try
			{
				if( IsMouseOnImageBox() )
				{
					DoClickImageBoxAction();
				}
				else
				{
					this.Focus();

					bool bPrompt = false;
					switch( this.PromptButtonType )
					{
						case enPromptButtonType.Prompt:
						case enPromptButtonType.PromptStep:
						case enPromptButtonType.PopupAllowPrompt:
							bPrompt = true;
							break;

						case enPromptButtonType.None:
							if( this.TSValueType == enTSTextBoxValueType.DateTime )
							{
								bPrompt = true;
							}
							break;
					}

					if( bPrompt )
					{
						enPromptButtonClickType clickType = enPromptButtonClickType.Prompt;
						PromptClickEventArgs args = new PromptClickEventArgs( clickType, true, this );
						OnPromptClickPrivate( args );
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		private bool mbTxtBoxFocusedFirstClicked = false;
		void txtBox_Click( object sender, EventArgs e )
		{
			try
			{
				switch( this.PromptButtonType )
				{
					case enPromptButtonType.Popup:
					case enPromptButtonType.PopupAllowPrompt:
					case enPromptButtonType.Combo:
					case enPromptButtonType.ComboStep:
						if( this.DisplayPromptButton() && this.TextBoxReadOnly )
						{
							enPromptButtonClickType clickType = enPromptButtonClickType.Popup;
							PromptClickEventArgs args = new PromptClickEventArgs( clickType, true, this );
							OnPromptClickPrivate( args );
						}
						break;
				}

				if( mbTxtBoxFocusedFirstClicked )
				{
					mbTxtBoxFocusedFirstClicked = false;

					// 定位光标在数字小数点的前面
					SetCursorInNumber();
				}
				//System.Diagnostics.Debug.WriteLine( this.Name + " Focused :" + this.txtBox.Focused.ToString() );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		void txtBox_DoubleClick( object sender, EventArgs e )
		{
			try
			{
				bool bPrompt = false;
				switch( this.PromptButtonType )
				{
					case enPromptButtonType.Prompt:
					case enPromptButtonType.PromptStep:
					case enPromptButtonType.PopupAllowPrompt:
						bPrompt = true;
						break;

					case enPromptButtonType.None:
						if( this.TSValueType == enTSTextBoxValueType.DateTime )
						{
							bPrompt = true;
						}
						break;
				}

				if( bPrompt )
				{
					enPromptButtonClickType clickType = enPromptButtonClickType.Prompt;
					PromptClickEventArgs args = new PromptClickEventArgs( clickType, true, this );
					OnPromptClickPrivate( args );
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		public void FirePromptClick()
		{
			DoPromptAction();
		}

		private void OnPromptClickPrivate( PromptClickEventArgs args )
		{
			if( args.ShowPopup && ( this.ReadOnly || !this.Enabled ) )
			{
				return;
			}

			this.txtBox.LostFocus -= new EventHandler( txtBox_LostFocus );
			try
			{
				OnPromptClick( args );

				if( !args.Handled &&
					args.ShowPopup &&
					args.PromptButtonClickType == enPromptButtonClickType.Popup )
				{
					ShowPopupWindow( args );
				}
				else if( !args.Handled &&
					this.TSValueType == enTSTextBoxValueType.DateTime &&
					args.PromptButtonClickType == enPromptButtonClickType.Prompt )
				{
					ShowCalendar( args );
				}
			}
			finally
			{
				this.txtBox.LostFocus += new EventHandler( txtBox_LostFocus );
			}
		}

		protected virtual void OnPromptClick( PromptClickEventArgs args )
		{
			if( PromptClick != null )
			{
				PromptClick( this, args );
			}
		}

		private TSTextBoxPopup4Calendar mPopup4Calendar = null;
		private void ShowCalendar( PromptClickEventArgs args )
		{
			if( mPopup4Calendar == null )
			{
				mPopup4Calendar = new TSTextBoxPopup4Calendar( this, null );
			}
			args.PromptForm = mPopup4Calendar;

			// 定位在所属控件的下方
			Point p = new Point( miCaptionWidth - 1, this.Height );
			p = this.PointToScreen( p );
			int iX = p.X;
			if( iX + mPopup4Calendar.Width > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width )
			{
				iX = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - mPopup4Calendar.Width;
			}
			int iY = p.Y;
			if( iY + mPopup4Calendar.Height > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height )
			{
				iY = iY - mPopup4Calendar.Height - this.Height;
			}
			mPopup4Calendar.Location = new Point( iX, iY );
			mPopup4Calendar.Show();
			mPopup4Calendar.BringToFront();
		}

		private TSTextBoxPopup mPopup = null;
		private void ShowPopupWindow( PromptClickEventArgs args )
		{
			int iInitWidth = this.Width - miCaptionWidth;
			if( mPopup == null )
			{
				mPopup = new TSTextBoxPopup( this, iInitWidth );
				mPopup.AddClicked += new EventHandler( mPopup_AddClicked );
				mPopup.ManageClicked += new EventHandler( mPopup_ManageClicked );
			}
			args.PromptForm = mPopup;
			mPopup.ManageButtonVisible = this.PopupManageButtonVisible;
			mPopup.ListColumns = this.ListColumns;
			mPopup.DataSource = this.DataSource;

			// 定位在所属控件的下方
			Point p;
			if( this.ShowImageBox )
			{
				p = new Point( -1, this.Height );
			}
			else
			{
				p = new Point( miCaptionWidth - 1, this.Height );
			}
			p = this.PointToScreen( p );
			if( p.Y + mPopup.Height > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height )
			{
				p = new Point( p.X, p.Y - mPopup.Height - this.Height );
			}
			if( p.X + mPopup.Width > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width )
			{
				p = new Point( System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - mPopup.Width, p.Y );
			}
			mPopup.Location = p;
			//mPopup.Width = iInitWidth;
			mPopup.Show();
			mPopup.BringToFront();

			// 当打开 Popup 时，缺省选择当前项
			if( this.PromptButtonType == enPromptButtonType.Popup ||
				this.PromptButtonType == enPromptButtonType.PopupAllowPrompt ||
				this.PromptButtonType == enPromptButtonType.Combo )
			{
				if( mPopup != null && mPopup.DataSource != null )
				{
					int iSelectedIndex = -1;

					if( this.SelectedValue != null && this.SelectedValue != DBNull.Value )
					{
						string strValueField = "";
						switch( this.ValueMember )
						{
							case enTSTextBoxMemberType.Code:
								strValueField = this.CodeColumnName;
								break;

							case enTSTextBoxMemberType.Text:
								strValueField = this.TextColumnName;
								break;

							case enTSTextBoxMemberType.ID:
							default:
								strValueField = this.IDColumnName;
								break;
						}

						if( strValueField != "" )
						{
							DataTable dtPopup = mPopup.DataSource.Table;
							if( strValueField != "" && dtPopup.Columns.Contains( strValueField ) )
							{
								string strValue = this.SelectedValue.ToString().TrimEnd().ToLower();

								for( int i = 0, j = mPopup.DataSource.Count; i < j; i++ )
								{
									string strCurrent = mPopup.DataSource[i][strValueField].ToString().TrimEnd().ToLower();
									if( strCurrent == strValue )
									{
										iSelectedIndex = i;
										break;
									}
								}
							}
						}
					}

					mPopup.SelectedIndex = iSelectedIndex;
				}
				else if( mPopup != null )
				{
					mPopup.SelectedIndex = -1;
				}
			}
			else if( this.PromptButtonType == enPromptButtonType.PromptStep ||
				this.PromptButtonType == enPromptButtonType.ComboStep )
			{
				if( mPopup != null && mPopup.DataSource != null )
				{
					int iSelectedIndex = -1;
					if( mPopup.DataSource.Count > 0 )
					{
						iSelectedIndex = 0;
					}

					string strInput = this.txtBox.Text.Trim();
					if( strInput != "" )
					{
						DataTable dtPopup = mPopup.DataSource.Table;
						for( int i = 0, j = mPopup.DataSource.Count; i < j; i++ )
						{
							DataRowView drv = mPopup.DataSource[i];

							bool bMatch = false;
							foreach( string strField in this.ListColumns )
							{
								string strCurrent = drv[strField].ToString().TrimEnd();

								if( strInput.Equals( strCurrent, StringComparison.CurrentCultureIgnoreCase ) )
								{
									iSelectedIndex = i;
									bMatch = true;
									break;
								}
							}

							if( bMatch )
							{
								break;
							}
						}
					}

					mPopup.SelectedIndex = iSelectedIndex;
				}
				else if( mPopup != null )
				{
					mPopup.SelectedIndex = -1;
				}
			}
		}

		protected virtual void OnPopupAddClicked( HandledEventArgs e )
		{
			if( PopupAddClicked != null )
			{
				PopupAddClicked( this, e );
			}
		}

		protected virtual void OnPopupManageClicked( HandledEventArgs e )
		{
			if( PopupManageClicked != null )
			{
				PopupManageClicked( this, e );
			}
		}

		void mPopup_ManageClicked( object sender, EventArgs e )
		{
			OnPopupManageClicked( new HandledEventArgs() );
		}

		void mPopup_AddClicked( object sender, EventArgs e )
		{
			OnPopupAddClicked( new HandledEventArgs() );
		}

		void form_PromptReturn( object sender, PromptReturnArgs e )
		{
			try
			{
				ProcessPromptReturn( e );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		protected void ProcessPromptReturn( PromptReturnArgs e )
		{
			bool bValueChanged = false;
			bool bValueChangedByEdit;
			bool bValueChangedByEditFinal = false;
			DataRow drPromptReturn = e.SelectedDataRow;

			if( drPromptReturn != null &&
				( this.PromptButtonType == enPromptButtonType.Popup ||
				this.PromptButtonType == enPromptButtonType.PopupAllowPrompt ||
				this.PromptButtonType == enPromptButtonType.Combo ||
				this.PromptButtonType == enPromptButtonType.ComboStep ||
				this.PromptButtonType == enPromptButtonType.PromptStep ) )
			{
				DataTable dtPromptReturn = drPromptReturn.Table;

				if( dtPromptReturn.Columns.Contains( mstrIDColumnName ) )
				{
					bValueChanged = SelectedID_Inner_Set( drPromptReturn[mstrIDColumnName], true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}
				else
				{
					bValueChanged = SelectedID_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}

				if( dtPromptReturn.Columns.Contains( mstrCodeColumnName ) )
				{
					bValueChanged = SelectedCode_Inner_Set( drPromptReturn[mstrCodeColumnName], true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}
				else
				{
					bValueChanged = SelectedCode_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}

				if( dtPromptReturn.Columns.Contains( mstrTextColumnName ) )
				{
					bValueChanged = SelectedText_Inner_Set( drPromptReturn[mstrTextColumnName], true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}
				else
				{
					bValueChanged = SelectedText_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
					bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
				}

				SelectedImage_Inner_set( drPromptReturn );

				this.txtBox.TextChanged -= new EventHandler( txtBox_TextChanged );
				string strText_Old = this.Text.Trim();
				SetTextByMember();
				if( !strText_Old.Equals( this.Text, StringComparison.CurrentCultureIgnoreCase ) )
				{
					OnTextChanged( EventArgs.Empty );
				}
				this.txtBox.TextChanged += new EventHandler( txtBox_TextChanged );
			}
			else if( drPromptReturn != null &&
				!string.IsNullOrEmpty( this.PromptReturnField ) &&
				drPromptReturn.Table.Columns.Contains( this.PromptReturnField ) )
			{
				bValueChanged = SelectedID_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedCode_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedText_Inner_Set( drPromptReturn[this.PromptReturnField], true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				this.txtBox.TextChanged -= new EventHandler( txtBox_TextChanged );
				string strText_Old = this.Text.Trim();
				this.Text = SelectedText_Inner.ToString().TrimEnd();
				if( !strText_Old.Equals( this.Text, StringComparison.CurrentCultureIgnoreCase ) )
				{
					OnTextChanged( EventArgs.Empty );
					this.OnValueChangedByEdit( EventArgs.Empty );
				}
				this.txtBox.TextChanged += new EventHandler( txtBox_TextChanged );
			}
			else if( drPromptReturn != null &&
				this.TSValueType == enTSTextBoxValueType.DateTime )
			{
				bValueChanged = SelectedID_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedCode_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedText_Inner_Set( drPromptReturn[0], true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				this.txtBox.TextChanged -= new EventHandler( txtBox_TextChanged );
				string strText_Old = this.Text.Trim();
				this.Text = SelectedText_Inner.ToString().TrimEnd();
				if( !strText_Old.Equals( this.Text, StringComparison.CurrentCultureIgnoreCase ) )
				{
					OnTextChanged( EventArgs.Empty );
				}
				this.txtBox.TextChanged += new EventHandler( txtBox_TextChanged );
			}
			else
			{
				bValueChanged = SelectedID_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedCode_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;

				bValueChanged = SelectedText_Inner_Set( null, true, out bValueChangedByEdit ) || bValueChanged;
				bValueChangedByEditFinal = bValueChangedByEditFinal || bValueChangedByEdit;
			}

			// 对于 PromptReturn 的情况，先触发 PromptReturn 事件，再触发 ValueChangedByEdit 事件
			// 否则，对于计算公式，可能不能正确取得相关字段的值
			if( bValueChanged )
			{
				// 触发事件
				// 如果值没有变更，则不触发 PromptReturn 事件，以免相关值被重置
				OnPromptReturn( e );

				if( bValueChangedByEditFinal )
				{
					this.OnValueChangedByEdit( EventArgs.Empty );
				}
			}
		}

		private void SetTextByMember()
		{
			if( this.PromptButtonType != enPromptButtonType.Popup &&
				this.PromptButtonType != enPromptButtonType.PopupAllowPrompt &&
				this.PromptButtonType != enPromptButtonType.PromptStep &&
				this.PromptButtonType != enPromptButtonType.ComboStep )
			{
				return;
			}

			object objText = null;

			enTSTextBoxMemberType type;
			if( this.Focused || this.txtBox.Focused )
			{
				type = GetFocusMember;
			}
			else
			{
				type = LostFocusMember;
			}
			switch( type )
			{
				case enTSTextBoxMemberType.ID:
					objText = SelectedID_Inner;
					break;

				case enTSTextBoxMemberType.Text:
					objText = SelectedText_Inner;
					break;

				case enTSTextBoxMemberType.Code:
				default:
					objText = SelectedCode_Inner;
					break;
			}

			if( objText != null )
			{
				this.Text = objText.ToString().TrimEnd();
			}
		}

		public bool InitByMethod
		{
			get
			{
				if( this.PromptButtonType == enPromptButtonType.None )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		private List<ITSPromptForm> mlstBindPromptReturn = new List<ITSPromptForm>();
		void ICreatePromptPageControl.OnPromptPageCreated( PromptPageCreatedArgs args )
		{
			ITSPromptForm prompt = args.PromptPage;
			if( prompt != null && !mlstBindPromptReturn.Contains( prompt ) )
			{
				prompt.PromptReturn += new PromptReturnEventHandler( form_PromptReturn );
				mlstBindPromptReturn.Add( prompt );
			}

			if( PromptPageCreated != null )
			{
				PromptPageCreated( this, args );
			}
		}

		public event PromptPageCreatedEventHandler PromptPageCreated;
		public event PromptReturnEventHandler PromptReturn;

		protected virtual void OnPromptReturn( PromptReturnArgs args )
		{
			if( PromptReturn != null )
			{
				PromptReturn( this, args );
			}
		}

		#endregion -- Prompt 相关处理 --

		#region -- Popup 窗口相关 --

		private DataView mdvDataSource = null;
		private List<string> mListColumns = new List<string>( 2 );
		private string mstrTextColumnName = "";
		private string mstrIDColumnName = "";
		private string mstrCodeColumnName = "";
		private enTSTextBoxMemberType mLostFocusMember = enTSTextBoxMemberType.Text;
		private enTSTextBoxMemberType mGetFocusMember = enTSTextBoxMemberType.Code;
		private enTSTextBoxMemberType mValueMember = enTSTextBoxMemberType.ID;
		private string mstrPromptReturnField = "";
		private event TSEditorSetValueEventHandler TSEditorSetValue;
		private bool mbPopupManageButtonVisible = false;

		[DefaultValue( false )]
		public bool PopupManageButtonVisible
		{
			get
			{
				return mbPopupManageButtonVisible;
			}
			set
			{
				mbPopupManageButtonVisible = value;
			}
		}

		[DefaultValue( enTSTextBoxMemberType.Text )]
		public enTSTextBoxMemberType LostFocusMember
		{
			get
			{
				return mLostFocusMember;
			}
			set
			{
				mLostFocusMember = value;
			}
		}

		[DefaultValue( enTSTextBoxMemberType.Code )]
		public enTSTextBoxMemberType GetFocusMember
		{
			get
			{
				return mGetFocusMember;
			}
			set
			{
				mGetFocusMember = value;
			}
		}

		[DefaultValue( enTSTextBoxMemberType.ID )]
		public enTSTextBoxMemberType ValueMember
		{
			get
			{
				return mValueMember;
			}
			set
			{
				mValueMember = value;
			}
		}

		[DefaultValue( "" )]
		public string PromptReturnField
		{
			get
			{
				return mstrPromptReturnField;
			}
			set
			{
				mstrPromptReturnField = value;
			}
		}

		[DefaultValue( false )]
		public bool PromptStepAllowNotExists
		{
			get
			{
				return mbPromptStepAllowNotExists;
			}
			set
			{
				mbPromptStepAllowNotExists = value;
			}
		}

		[DefaultValue( "" )]
		public string TextColumnName
		{
			get
			{
				return mstrTextColumnName;
			}
			set
			{
				mstrTextColumnName = value;
			}
		}

		[DefaultValue( "" )]
		public string IDColumnName
		{
			get
			{
				return mstrIDColumnName;
			}
			set
			{
				mstrIDColumnName = value;
			}
		}

		[DefaultValue( "" )]
		public string CodeColumnName
		{
			get
			{
				return mstrCodeColumnName;
			}
			set
			{
				mstrCodeColumnName = value;
			}
		}

		private object mobjSelectedID = null;
		[DefaultValue( null )]
		public object SelectedID
		{
			get
			{
				return SelectedID_Inner;
			}
			set
			{
				SetValueByMember( enTSTextBoxMemberType.ID, value );
			}
		}

		private object SelectedID_Inner
		{
			get
			{
				return mobjSelectedID;
			}
		}

		private bool SelectedID_Inner_Set( object value, bool bFromPromptPage, out bool bValueChangedByEdit )
		{
			bool bChanged = false;
			bValueChangedByEdit = false;

			if( ( mobjSelectedID != null && value != null && !mobjSelectedID.Equals( value ) ) ||
				( mobjSelectedID == null && value != null ) ||
				( mobjSelectedID != null && value == null ) )
			{
				mobjSelectedID = value;
				bChanged = true;

				if( this.ValueMember == enTSTextBoxMemberType.ID &&
					( this.Focused || this.txtBox.Focused || bFromPromptPage ) )
				{
					bValueChangedByEdit = true;
					if( !bFromPromptPage )
					{
						this.OnValueChangedByEdit( EventArgs.Empty );
					}
				}
			}
			return bChanged;
		}

		private object mobjSelectedCode = null;
		[DefaultValue( null )]
		public object SelectedCode
		{
			get
			{
				return SelectedCode_Inner;
			}
			set
			{
				SetValueByMember( enTSTextBoxMemberType.Code, value );
			}
		}

		private object SelectedCode_Inner
		{
			get
			{
				return mobjSelectedCode;
			}
		}

		private bool SelectedCode_Inner_Set( object value, bool bFromPromptPage, out bool bValueChangedByEdit )
		{
			bool bChanged = false;
			bValueChangedByEdit = false;

			if( ( mobjSelectedCode != null && value != null && !mobjSelectedCode.Equals( value ) ) ||
				( mobjSelectedCode == null && value != null ) ||
				( mobjSelectedCode != null && value == null ) )
			{
				mobjSelectedCode = value;
				bChanged = true;

				if( this.ValueMember == enTSTextBoxMemberType.Code &&
					( this.Focused || this.txtBox.Focused || bFromPromptPage ) )
				{
					bValueChangedByEdit = true;
					if( !bFromPromptPage )
					{
						this.OnValueChangedByEdit( EventArgs.Empty );
					}
				}
			}
			return bChanged;
		}

		private string mstrImageColumnName = "";
		[DefaultValue( "" )]
		public string ImageColumnName
		{
			get
			{
				return mstrImageColumnName;
			}
			set
			{
				mstrImageColumnName = value;
			}
		}

		private Image mimgSelectedImage = null;
		[DefaultValue( null )]
		public Image SelectedImage
		{
			get
			{
				return mimgSelectedImage;
			}
			set
			{
				if( mimgSelectedImage != value )
				{
					mimgSelectedImage = value;
					this.Invalidate( new Rectangle( miCaptionWidth + 1, 1, this.Width - miCaptionWidth - 2, this.Height - 22 ) );
				}
			}
		}

		public bool ShowImageBox
		{
			get
			{
				return mbShowImageBox;
			}
			set
			{
				if( mbShowImageBox != value )
				{
					mbShowImageBox = value;

					PlaceTextBox();
				}
			}
		}

		private void SelectedImage_Inner_set( DataRow row )
		{
			// 触发事件，以便外部读取缩略图
			OnSetSelectedImageByPrompt( new SetSelectedImageEventArgs( row ) );

			if( row == null )
			{
				this.SelectedImage = null;
			}
			else
			{
				Image img = null;
				DataTable table = row.Table;
				if( !string.IsNullOrEmpty( this.ImageColumnName ) &&
					table.Columns.Contains( this.ImageColumnName ) )
				{
					object objValue = row[this.ImageColumnName];
					if( objValue != null && objValue != DBNull.Value )
					{
						Type type = table.Columns[this.ImageColumnName].DataType;
						if( type == typeof( string ) )
						{
							img = CommonFuntion.GetImageFromValue4DB( Convert.ToString( objValue ).TrimEnd() );
						}
						else if( type == typeof( byte[] ) )
						{
							img = CommonFuntion.GetImageFromZippedBytes( (byte[])objValue );
						}
					}
					else
					{
						objValue = null;
					}
				}
				this.SelectedImage = img;
			}
		}

		protected virtual void OnSetSelectedImageByPrompt( SetSelectedImageEventArgs e )
		{
			if( SetSelectedImageByPrompt != null )
			{
				SetSelectedImageByPrompt( this, e );
			}
		}

		private object mobjSelectedText = null;
		[DefaultValue( null )]
		public object SelectedText
		{
			get
			{
				return SelectedText_Inner;
			}
			set
			{
				SetValueByMember( enTSTextBoxMemberType.Text, value );
			}
		}

		private object SelectedText_Inner
		{
			get
			{
				return mobjSelectedText;
			}
		}

		private bool SelectedText_Inner_Set( object value, bool bFromPromptPage, out bool bValueChangedByEdit )
		{
			bool bChanged = false;
			bValueChangedByEdit = false;

			if( ( mobjSelectedText != null && value != null && !mobjSelectedText.Equals( value ) ) ||
				( mobjSelectedText == null && value != null ) ||
				( mobjSelectedText != null && value == null ) )
			{
				mobjSelectedText = value;
				bChanged = true;

				if( this.ValueMember == enTSTextBoxMemberType.Text &&
					( this.Focused || this.txtBox.Focused || bFromPromptPage ) )
				{
					bValueChangedByEdit = true;
					if( !bFromPromptPage )
					{
						this.OnValueChangedByEdit( EventArgs.Empty );
					}
				}
			}
			return bChanged;
		}

		private void PromptBySetValue()
		{
			if( this.DataSource == null )
			{
				switch( this.PromptButtonType )
				{
					case enPromptButtonType.Popup:
					case enPromptButtonType.PromptStep:
					case enPromptButtonType.ComboStep:
					case enPromptButtonType.PopupAllowPrompt:
						PromptClickEventArgs args = new PromptClickEventArgs( enPromptButtonClickType.Popup, false, this );
						OnPromptClickPrivate( args );
						break;
				}
			}
		}

		private void SetValueByMember( enTSTextBoxMemberType member, object value )
		{
			switch( this.PromptButtonType )
			{
				case enPromptButtonType.Popup:
				case enPromptButtonType.PopupAllowPrompt:
					SetValueByMember4Popup( member, value );
					break;

				default:
					SetValueByMember4PromptStep( member, value );
					break;
			}
		}

		private void SetValueByMember4PromptStep( enTSTextBoxMemberType member, object value )
		{
			bool bValueChangedByEdit;
			if( value == null || value == DBNull.Value || value.ToString().Trim() == "" )
			{
				SelectedID_Inner_Set( null, false, out bValueChangedByEdit );
				SelectedCode_Inner_Set( null, false, out bValueChangedByEdit );
				SelectedText_Inner_Set( null, false, out bValueChangedByEdit );
				SetSelectedIndexInternal( 0 );
				this.Text = "";
			}
			else
			{
				TSEditorSetValueEventArgs args = new TSEditorSetValueEventArgs( member, value, 0 );
				OnTSEditorSetValue( args );
				if( args.ValueDataRow != null )
				{
					DataRow dr = args.ValueDataRow;
					DataColumnCollection columns = dr.Table.Columns;

					if( this.IDColumnName != "" && columns.Contains( this.IDColumnName ) )
					{
						SelectedID_Inner_Set( dr[this.IDColumnName], false, out bValueChangedByEdit );
					}
					if( this.CodeColumnName != "" && columns.Contains( this.CodeColumnName ) )
					{
						SelectedCode_Inner_Set( dr[this.CodeColumnName], false, out bValueChangedByEdit );
					}
					if( this.TextColumnName != "" && columns.Contains( this.TextColumnName ) )
					{
						SelectedText_Inner_Set( dr[this.TextColumnName], false, out bValueChangedByEdit );
					}
					SetTextByMember();
					SelectedImage_Inner_set( dr );
				}
				else
				{
					SelectedID_Inner_Set( null, false, out bValueChangedByEdit );
					SelectedCode_Inner_Set( null, false, out bValueChangedByEdit );
					SelectedText_Inner_Set( null, false, out bValueChangedByEdit );
					SetSelectedIndexInternal( 0 );
					this.Text = "";
					SelectedImage_Inner_set( null );
				}
			}
		}

		internal protected virtual void OnTSEditorSetValue( TSEditorSetValueEventArgs args )
		{
			if( TSEditorSetValue != null )
			{
				TSEditorSetValue( this, args );
			}
		}

		private void SetValueByMember4Popup( enTSTextBoxMemberType member, object value )
		{
			PromptBySetValue();

			if( this.DataSource == null )
			{
				return;
			}

			string strColName = "";
			switch( member )
			{
				case enTSTextBoxMemberType.ID:
					strColName = this.IDColumnName;
					break;

				case enTSTextBoxMemberType.Code:
					strColName = this.CodeColumnName;
					break;

				case enTSTextBoxMemberType.Text:
				default:
					strColName = this.TextColumnName;
					break;
			}
			DataColumnCollection columns = this.DataSource.Table.Columns;
			if( !columns.Contains( strColName ) )
			{
				return;
			}

			string strValue = ( value == null ? "" : value.ToString().TrimEnd() );
			bool bFindRow = false;
			DataRow drSelected = null;
			bool bValueChangedByEdit;
			for( int i = 0, j = this.DataSource.Count; i < j; i++ )
			{
				DataRowView drv = this.DataSource[i];
				string strValueCurrent = drv[strColName].ToString().TrimEnd();
				if( strValue == strValueCurrent )
				{
					if( this.IDColumnName != "" && columns.Contains( this.IDColumnName ) )
					{
						SelectedID_Inner_Set( drv[this.IDColumnName], false, out bValueChangedByEdit );
					}
					if( this.CodeColumnName != "" && columns.Contains( this.CodeColumnName ) )
					{
						SelectedCode_Inner_Set( drv[this.CodeColumnName], false, out bValueChangedByEdit );
					}
					if( this.TextColumnName != "" && columns.Contains( this.TextColumnName ) )
					{
						SelectedText_Inner_Set( drv[this.TextColumnName], false, out bValueChangedByEdit );
					}
					SetTextByMember();
					bFindRow = true;
					SetSelectedIndexInternal( i );
					drSelected = drv.Row;
					SelectedImage_Inner_set( drSelected );
					break;
				}
			}

			if( !bFindRow )
			{
				SelectedID_Inner_Set( null, false, out bValueChangedByEdit );
				SelectedCode_Inner_Set( null, false, out bValueChangedByEdit );
				SelectedText_Inner_Set( null, false, out bValueChangedByEdit );
				SetSelectedIndexInternal( 0 );
				SelectedImage_Inner_set( null );
				this.Text = "";
			}
		}

		public void FirePromptReturn4NCodeOrList()
		{
			if( this.ReadOnly || !this.Enabled )
			{
				return;
			}

			switch( this.PromptButtonType )
			{
				case enPromptButtonType.Popup:
				case enPromptButtonType.PopupAllowPrompt:

					#region -- 下拉框 --

					{
						if( this.DataSource == null )
						{
							return;
						}

						string strColName = "";
						switch( this.ValueMember )
						{
							case enTSTextBoxMemberType.ID:
								strColName = this.IDColumnName;
								break;

							case enTSTextBoxMemberType.Code:
								strColName = this.CodeColumnName;
								break;

							case enTSTextBoxMemberType.Text:
							default:
								strColName = this.TextColumnName;
								break;
						}
						DataColumnCollection columns = this.DataSource.Table.Columns;
						if( !columns.Contains( strColName ) )
						{
							return;
						}

						string strValue = this.Value4DB.ToString();
						bool bFindRow = false;
						DataRow drSelected = null;
						for( int i = 0, j = this.DataSource.Count; i < j; i++ )
						{
							DataRowView drv = this.DataSource[i];
							string strValueCurrent = drv[strColName].ToString().TrimEnd();
							if( strValue == strValueCurrent )
							{
								bFindRow = true;
								drSelected = drv.Row;
								break;
							}
						}

						if( bFindRow )
						{
							this.OnPromptReturn( new PromptReturnArgs( drSelected, null ) );
						}
					}

					#endregion -- 下拉框 --

					break;

				case enPromptButtonType.ComboStep:
				case enPromptButtonType.PromptStep:

					#region -- 逐级提示 --

					{
						string strText = this.Text.Trim();

						if( strText == "" )
						{
							return;
						}

						if( this.PromptStepAllowNotExists )
						{
							return;
						}

						TSEditorInputPromptGetDataEventArgs args;
						if( this.Value4DB != null )
						{
							args = new TSEditorInputPromptGetDataEventArgs( true, this.Value4DB );
						}
						else
						{
							args = new TSEditorInputPromptGetDataEventArgs( strText );
						}
						OnInputPromptValidating( args );

						if( args.PromptData != null && args.PromptData.Count > 0 )	// 存在编码
						{
							// 必须马上记录下 Row，不能是 DataRowView；否则如果在 base.DataSource 增加了一行空数据，会变成指向该空行
							DataRow dr = args.PromptData[0].Row;

							// 触发 PromptReturn 事件
							PromptReturnArgs argsReturn = new PromptReturnArgs( dr, null );
							this.OnPromptReturn( argsReturn );
						}
					}

					#endregion -- 逐级提示 --

					break;
			}
		}

		private int miSelectedIndex = -1;
		[DefaultValue( -1 )]
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
					// DownList 那种类型的控件，才去执行 PromptBySetValue
					// 因为 PromptBySetValue 可能引发读取所有数据，导致性能问题
					if( this.PromptButtonType == enPromptButtonType.Popup ||
						this.PromptButtonType == enPromptButtonType.PopupAllowPrompt )
					{
						PromptBySetValue();
					}

					if( this.DataSource == null )
					{
						return;
					}

					bool bValueChangedByEdit;
					if( value >= 0 && value < this.DataSource.Count )
					{
						DataColumnCollection columns = this.DataSource.Table.Columns;
						DataRowView drv = this.DataSource[value];
						if( this.IDColumnName != "" && columns.Contains( this.IDColumnName ) )
						{
							SelectedID_Inner_Set( drv[this.IDColumnName], false, out bValueChangedByEdit );
						}
						if( this.CodeColumnName != "" && columns.Contains( this.CodeColumnName ) )
						{
							SelectedCode_Inner_Set( drv[this.CodeColumnName], false, out bValueChangedByEdit );
						}
						if( this.TextColumnName != "" && columns.Contains( this.TextColumnName ) )
						{
							SelectedText_Inner_Set( drv[this.TextColumnName], false, out bValueChangedByEdit );
						}
						SetTextByMember();

						SetSelectedIndexInternal( value );
						SelectedImage_Inner_set( drv.Row );
					}
					else if( this.DataSource.Count > 0 )
					{
						throw new ArgumentOutOfRangeException( "SelectedIndex" );
					}
				}
			}
		}

		internal void SetSelectedIndexInternal( int value )
		{
			if( miSelectedIndex != value )
			{
				miSelectedIndex = value;

				// 事件
				OnSelectedIndexChanged( EventArgs.Empty );
			}
		}

		protected virtual void OnSelectedIndexChanged( EventArgs e )
		{
			if( SelectedIndexChanged != null )
			{
				SelectedIndexChanged( this, e );
			}
		}

		[DefaultValue( null )]
		public object SelectedValue
		{
			get
			{
				switch( ValueMember )
				{
					case enTSTextBoxMemberType.ID:
						return SelectedID_Inner;

					case enTSTextBoxMemberType.Code:
						return SelectedCode_Inner;

					case enTSTextBoxMemberType.Text:
					default:
						return SelectedText_Inner;
				}
			}
			set
			{
				switch( ValueMember )
				{
					case enTSTextBoxMemberType.Code:
						this.SelectedCode = value;
						break;

					case enTSTextBoxMemberType.Text:
						this.SelectedText = value;
						break;

					case enTSTextBoxMemberType.ID:
					default:
						this.SelectedID = value;
						break;
				}
			}
		}

		[DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
		public List<string> ListColumns
		{
			get
			{
				return mListColumns;
			}
		}

		[DefaultValue( null )]
		public DataView DataSource
		{
			get
			{
				return mdvDataSource;
			}
			set
			{
				if( mdvDataSource != value )
				{
					mdvDataSource = value;
					AddEmptyRowToDataSource();

					if( mPopup != null )
					{
						mPopup.DataSource = value;
					}

					OnDataSourceChanged( EventArgs.Empty );
				}
			}
		}

		protected virtual void OnDataSourceChanged( EventArgs e )
		{
			if( DataSourceChanged != null )
			{
				DataSourceChanged( this, e );
			}
		}

		private void AddEmptyRowToDataSource()
		{
			if( ( this.PromptButtonType == enPromptButtonType.Popup || this.PromptButtonType == enPromptButtonType.PopupAllowPrompt ) &&
				mdvDataSource != null &&
				mdvDataSource.Table.Columns.Contains( this.TextColumnName ) )
			{
				if( this.CanBeEmpty )
				{
					using( DataView dv = new DataView( mdvDataSource.Table ) )
					{
						dv.RowFilter = string.Format( "{0} is null or Convert({0},'System.String')=''", this.TextColumnName );
						if( dv.Count == 0 )
						{
							DataRow drNew = dv.Table.NewRow();
							drNew[this.TextColumnName] = DBNull.Value;
							dv.Table.Rows.InsertAt( drNew, 0 );
						}
					}
				}
				else
				{
					using( DataView dv = new DataView( mdvDataSource.Table ) )
					{
						dv.RowFilter = string.Format( "{0} is null or Convert({0},'System.String')=''", this.TextColumnName );
						if( dv.Count == 1 )
						{
							dv[0].Delete();
						}
					}
				}

				// 以下代码，会导致一点下拉就选第一项（但希望的效果是一创建就已选第一项，而不是点下拉才选择），但不会触发 PromptReturn 事件
				////// 不允许为空时，选择第一项
				////if( !this.CanBeEmpty &&
				////    ( this.SelectedValue == null || this.SelectedValue == DBNull.Value ) )
				////{
				////    if( mdvDataSource.Count > 0 )
				////    {
				////        this.SelectedIndex = 0;
				////    }
				////    else
				////    {
				////        this.SelectedIndex = -1;
				////    }
				////}
			}
		}

		public void InitSelectedValue( object objSelectedID, object objSelectedCode, object objSelectedText )
		{
			bool bValueChangedByEdit;
			SelectedID_Inner_Set( objSelectedID, false, out bValueChangedByEdit );
			SelectedCode_Inner_Set( objSelectedCode, false, out bValueChangedByEdit );
			SelectedText_Inner_Set( objSelectedText, false, out bValueChangedByEdit );

			SetTextByMember();
		}

		#endregion -- Popup 窗口相关 --

		#region -- ErrorProvider 相关的方法 --

		public void SetErrorMessage( string strErrorMsg )
		{
			mstrErrorMsg = strErrorMsg;

			if( string.IsNullOrEmpty( strErrorMsg ) )
			{
				if( !string.IsNullOrEmpty( mstrToolTipCaption ) )
				{
					mtpError.SetToolTip( this, mstrToolTipCaption );
				}
				else
				{
					mtpError.SetToolTip( this, "" );
				}
			}
			else
			{
				if( !string.IsNullOrEmpty( mstrToolTipCaption ) )
				{
					strErrorMsg += Environment.NewLine + mstrToolTipCaption;
				}

				mtpError.SetToolTip( this, strErrorMsg );
			}

			this.Invalidate();
		}

		public void SetToolTip( string caption )
		{
			mstrToolTipCaption = caption;

			if( txtBox != null )
			{
				mtpError.SetToolTip( txtBox, caption );
			}

			mtpError.SetToolTip( this, caption );
		}

		#endregion -- ErrorProvider 相关的方法 --

		#region ITSEditor Members

		[DefaultValue( false )]
		public bool Checked
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		event EventHandler ITSEditor.CheckedChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public int GetMinWidth()
		{
			return this.miCaptionWidth + MC_iTextBoxMinWidth;
		}

		#endregion

		#region -- 输入逐级提示 --

		public event TSEditorInputPromptGetDataEventHandler InputPromptGetData;
		public event TSEditorInputPromptGetDataEventHandler InputPromptValidating;
		internal const string MC_strDefaultInputNotExistsMessage = "输入的编码不存在或被禁止使用。";

		void txtBox_TextChanged( object sender, EventArgs e )
		{
			try
			{
				OnTextChanged( EventArgs.Empty );

				//InputStepPrompt();

				// 使用 BeginInvoke 方式调用 InputStepPrompt，以便让输入不用等待 InputStepPrompt 结束而产生输入延后的情况
				if( CommonFuntion.TSInputStepPromptLength >= 0 &&
					txtBox.Handle != IntPtr.Zero )
				{
					if( txtBox.Text.Trim().Length >= CommonFuntion.TSInputStepPromptLength )
					{
						if( ( this.PromptButtonType != enPromptButtonType.PromptStep && this.PromptButtonType != enPromptButtonType.ComboStep ) ||
							this.ReadOnly ||
							this.TextBoxReadOnly ||
							!this.Enabled ||
							!this.txtBox.Focused )
						{
						}
						else
						{
							try
							{
								if( mThread != null && mThread.ThreadState == ThreadState.Running )
								{
									mThread.Abort();
								}
							}
							catch
							{
							}

							mThread = new Thread( new ThreadStart( InputStepPromptGetData ) );
							mThread.Start();
						}
					}
					else if( mPopup != null && mPopup.Visible )
					{
						mPopup.Visible = false;
					}
				}
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}
		}

		Thread mThread = null;
		DataView mdvPromptStepData = null;
		private void InputStepPromptGetData()
		{
			try
			{
				TSEditorInputPromptGetDataEventArgs argsPrompt = new TSEditorInputPromptGetDataEventArgs( this.txtBox.Text );
				OnInputPromptGetData( argsPrompt );
				mdvPromptStepData = argsPrompt.PromptData;

				txtBox.BeginInvoke( new InputStepPromptDelegate( InputStepPrompt ) );
			}
			catch( System.Threading.ThreadAbortException ex )
			{
			}
			catch( Exception ex )
			{
				// 不在此报错，当光标离开时才报
				//CommonFuntion.OnDealError( this, ex );
			}
		}

		private delegate void InputStepPromptDelegate();	// 为了使用 BeginInvoke 方式调用 InputStepPrompt
		private void InputStepPrompt()
		{
			try
			{
				if( ( this.PromptButtonType != enPromptButtonType.PromptStep && this.PromptButtonType != enPromptButtonType.ComboStep ) ||
					this.ReadOnly ||
					this.TextBoxReadOnly ||
					!this.Enabled ||
					!this.txtBox.Focused )
				{
					return;
				}

				this.DataSource = mdvPromptStepData;
				PromptClickEventArgs args = new PromptClickEventArgs( enPromptButtonClickType.Popup, true, this );
				this.ShowPopupWindow( args );
			}
			catch
			{
			}
		}

		protected virtual void OnInputPromptGetData( TSEditorInputPromptGetDataEventArgs e )
		{
			if( InputPromptGetData != null )
			{
				InputPromptGetData( this, e );
			}
		}

		protected virtual void OnInputPromptValidating( TSEditorInputPromptGetDataEventArgs e )
		{
			if( InputPromptValidating != null )
			{
				InputPromptValidating( this, e );
			}
		}

		public bool ValidationCode( out string strErrorMsg )
		{
			return TSCodeValidationAndProcessor( out strErrorMsg );
		}

		// 记录上次校验的值，以免输入没有变更，而只是光标点入再离开时，都会校验，并且由于还触发了 PromptReturn 事件，会导致其它列的值又被重置了
		private string mstrLastValidateText = "";	// 记录在 TSCodeValidationAndProcessor 中，上次校验的输入
		private object mobjLastValidateValue = null;	// 记录在 TSCodeValidationAndProcessor 中，上次校验时的值
		private bool TSCodeValidationAndProcessor( out string strErrorMsg )
		{
			bool bPass = false;
			strErrorMsg = "";

			string strText = this.Text.Trim();

			if( strText == "" )
			{
				this.InitSelectedValue( null, null, null );
				return true;
			}

			if( ( this.PromptButtonType != enPromptButtonType.PromptStep && this.PromptButtonType != enPromptButtonType.ComboStep ) ||
				this.ReadOnly ||
				//this.TextBoxReadOnly ||
				!this.Enabled ||
				this.PromptStepAllowNotExists )
			{
				return true;
			}

			TSEditorInputPromptGetDataEventArgs args = new TSEditorInputPromptGetDataEventArgs( strText );
			OnInputPromptValidating( args );

			if( args.HasError )
			{
				strErrorMsg = args.ErrorMsg;
				return false;
			}

			if( args.PromptData == null || args.PromptData.Count == 0 )	// 不存在编码
			{
				strErrorMsg = MC_strDefaultInputNotExistsMessage;
				this.InitSelectedValue( null, null, null );
				bPass = false;
			}
			else	// 存在编码
			{
				// 必须马上记录下 Row，不能是 DataRowView；否则如果在 base.DataSource 增加了一行空数据，会变成指向该空行
				DataRow dr = args.PromptData[0].Row;

				DataTable dt = args.PromptData.Table;

				// 设置 Selected 值
				object objCode = null;
				object objID = null;
				object objText = null;
				if( this.CodeColumnName != "" &&
					dt.Columns.Contains( this.CodeColumnName ) )
				{
					objCode = dr[this.CodeColumnName];
				}
				if( this.IDColumnName != "" &&
					dt.Columns.Contains( this.IDColumnName ) )
				{
					objID = dr[this.IDColumnName];
				}
				if( this.TextColumnName != "" &&
					dt.Columns.Contains( this.TextColumnName ) )
				{
					objText = dr[this.TextColumnName].ToString();
				}

				object objNewValue = null;
				switch( this.ValueMember )
				{
					case enTSTextBoxMemberType.ID:
						objNewValue = objID;
						break;

					case enTSTextBoxMemberType.Code:
						objNewValue = objCode;
						break;

					default:
						objNewValue = objText;
						break;
				}

				if( objNewValue == null && this.mobjLastValidateValue == null )
				{
					// 相等时忽略，以免重新对 Cell 赋相同值或触发 OnCellPromptReturn
				}
				else if( objNewValue != null &&
					this.mobjLastValidateValue != null &&
					objNewValue.ToString().TrimEnd().ToUpper() == this.mobjLastValidateValue.ToString().TrimEnd().ToUpper() )
				{
					// 相等时忽略，以免重新对 Cell 赋相同值或触发 OnCellPromptReturn
				}
				else if( objNewValue == null && this.Value4DB == null )
				{
					// 相等时忽略，以免重新对 Cell 赋相同值或触发 OnCellPromptReturn
				}
				else if( objNewValue != null &&
					this.Value4DB != null &&
					objNewValue.ToString().TrimEnd().ToUpper() == this.Value4DB.ToString().TrimEnd().ToUpper() )
				{
					// 相等时忽略，以免重新对 Cell 赋相同值或触发 OnCellPromptReturn
				}
				else
				{
					this.InitSelectedValue( objID, objCode, objText );

					SelectedImage_Inner_set( dr );

					// 触发 PromptReturn 事件
					PromptReturnArgs argsReturn = new PromptReturnArgs( dr, null );
					this.OnPromptReturn( argsReturn );
				}

				// 当校验通过时，记录下本次校验的 Text
				mstrLastValidateText = strText;
				mobjLastValidateValue = objNewValue;

				bPass = true;
			}

			return bPass;
		}

		#endregion -- 输入逐级提示 --

		protected override bool ProcessCmdKey( ref Message msg, Keys keyData )
		{
			if( keyData == Keys.Tab )
			{
				TabKeyPressEventArgs args = new TabKeyPressEventArgs();
				OnTabKeyPress( args );
				if( args.Handled )
				{
					return true;
				}
			}

			return base.ProcessCmdKey( ref msg, keyData );
		}

		protected virtual void OnTabKeyPress( TabKeyPressEventArgs e )
		{
			if( TabKeyPress != null )
			{
				TabKeyPress( this, e );
			}
		}
	}
}
