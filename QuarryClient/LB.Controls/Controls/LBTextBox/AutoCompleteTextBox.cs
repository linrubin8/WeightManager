using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using LB.WinFunction;
using System.Drawing.Drawing2D;

namespace LB.Controls.LBTextBox
{
	/// <summary>
	/// Summary description for AutoCompleteTextBox.
	/// </summary>
	[Serializable]
	public class AutoCompleteTextBox : TextBox
	{

		#region Classes and Structures

		public enum EntryMode
		{
			Text,
			List
		}

		/// <summary>
		/// This is the class we will use to hook mouse events.
		/// </summary>
		private class WinHook : NativeWindow
		{
			private AutoCompleteTextBox tb;

			/// <summary>
			/// Initializes a new instance of <see cref="WinHook"/>
			/// </summary>
			/// <param name="tbox">The <see cref="AutoCompleteTextBox"/> the hook is running for.</param>
			public WinHook(AutoCompleteTextBox tbox)
			{
				this.tb = tbox;
			}

			/// <summary>
			/// Look for any kind of mouse activity that is not in the
			/// text box itself, and hide the popup if it is visible.
			/// </summary>
			/// <param name="m"></param>
			protected override void WndProc(ref Message m)
			{
				switch (m.Msg)
				{
					case Messages.WM_LBUTTONDOWN:
					case Messages.WM_LBUTTONDBLCLK:
					case Messages.WM_MBUTTONDOWN:
					case Messages.WM_MBUTTONDBLCLK:
					case Messages.WM_RBUTTONDOWN:
					case Messages.WM_RBUTTONDBLCLK:
					case Messages.WM_NCLBUTTONDOWN:
					case Messages.WM_NCMBUTTONDOWN:
					case Messages.WM_NCRBUTTONDOWN:
					{
						// Lets check to see where the event took place
						Form form = tb.FindForm();
						Point p = form.PointToScreen(new Point((int)m.LParam));
						Point p2 = tb.PointToScreen(new Point(0, 0));
						Rectangle rect = new Rectangle(p2, tb.Size);
						// Hide the popup if it is not in the text box
						if (!rect.Contains(p))
						{
							tb.HideList();
						}
					} break;
					case Messages.WM_SIZE:
					case Messages.WM_MOVE:
					{
						tb.HideList();
					} break;
					// This is the message that gets sent when a childcontrol gets activity
					case Messages.WM_PARENTNOTIFY:
					{
						switch ((int)m.WParam)
						{
							case Messages.WM_LBUTTONDOWN:
							case Messages.WM_LBUTTONDBLCLK:
							case Messages.WM_MBUTTONDOWN:
							case Messages.WM_MBUTTONDBLCLK:
							case Messages.WM_RBUTTONDOWN:
							case Messages.WM_RBUTTONDBLCLK:
							case Messages.WM_NCLBUTTONDOWN:
							case Messages.WM_NCMBUTTONDOWN:
							case Messages.WM_NCRBUTTONDOWN:
							{
								// Same thing as before
								Form form = tb.FindForm();
								Point p = form.PointToScreen(new Point((int)m.LParam));
								Point p2 = tb.PointToScreen(new Point(0, 0));
								Rectangle rect = new Rectangle(p2, tb.Size);
								if (!rect.Contains(p))
								{
									tb.HideList();
								}
							} break;
						}
					} break;
				}
				
				base.WndProc (ref m);
			}
		}

		#endregion

		#region Members

		private ListBox list;
		protected Form popup;
        protected Panel drapDown;
		private AutoCompleteTextBox.WinHook hook;

        #endregion
        
        #region Properties

        private AutoCompleteTextBox.EntryMode mode = EntryMode.Text;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AutoCompleteTextBox.EntryMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		private AutoCompleteEntryCollection items = new AutoCompleteEntryCollection();
		[System.ComponentModel.Editor(typeof(AutoCompleteEntryCollection.AutoCompleteEntryCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal AutoCompleteEntryCollection Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		private AutoCompleteTriggerCollection triggers = new AutoCompleteTriggerCollection();
		[System.ComponentModel.Editor(typeof(AutoCompleteTriggerCollection.AutoCompleteTriggerCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AutoCompleteTriggerCollection Triggers
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		[Browsable(true)]
		[Description("The width of the popup (-1 will auto-size the popup to the width of the textbox).")]
		public int PopupWidth
		{
			get
			{
				return this.popup.Width;
			}
			set
			{
				if (value == -1)
				{
					this.popup.Width = this.Width;
				} 
				else
				{
					this.popup.Width = value;
				}
			}
		}

		public BorderStyle PopupBorderStyle
		{
			get
			{
				return this.list.BorderStyle;
			}
			set
			{
				this.list.BorderStyle = value;
			}
		}

		private Point popOffset = new Point(12, 0);
		[Description("The popup defaults to the lower left edge of the textbox.")]
		public Point PopupOffset
		{
			get
			{
				return this.popOffset;
			}
			set
			{
				this.popOffset = value;
			}
		}

		private Color popSelectBackColor = SystemColors.Highlight;
		public Color PopupSelectionBackColor
		{
			get
			{
				return this.popSelectBackColor;
			}
			set
			{
				this.popSelectBackColor = value;
			}
		}

		private Color popSelectForeColor = SystemColors.HighlightText;
		public Color PopupSelectionForeColor
		{
			get
			{
				return this.popSelectForeColor;
			}
			set
			{
				this.popSelectForeColor = value;
			}
		}

		private bool triggersEnabled = true;
		protected bool TriggersEnabled
		{
			get
			{
				return this.triggersEnabled;
			}
			set
			{
				this.triggersEnabled = value;
			}
		}

        private bool _IsAllowNotExists = false;
        [Description("是否允许输入不存在的内容")]
        public bool IsAllowNotExists
        {
            get
            {
                return _IsAllowNotExists;
            }
            set
            {
                _IsAllowNotExists = value; ;
            }
        }

        private DataView _DataSource = null;
        [Description("可查询数据源")]
        public DataView PopDataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
                if (_DataSource != null)
                {
                    this.Items.Clear();
                    foreach (DataRowView drv in _DataSource)
                    {
                        string strDisplayName = "";
                        if (_DataSource.Table.Columns.Contains(TextColumnName))
                        {
                            strDisplayName = drv[TextColumnName].ToString().TrimEnd();
                        }
                        this.Items.Add(new AutoCompleteEntry(strDisplayName, drv, ""));
                    }
                }
            }
        }

        private string _IDColumnName = "";
        [Description("ID字段")]
        public string IDColumnName
        {
            get
            {
                return _IDColumnName;
            }
            set
            {
                _IDColumnName = value;
            }
        }

        private string _TextColumnName = "";
        [Description("Text字段")]
        public string TextColumnName
        {
            get
            {
                return _TextColumnName;
            }
            set
            {
                _TextColumnName = value;
            }
        }

        private int _LBViewType = 0;
        [Description("查询视图号")]
        public int LBViewType
        {
            get
            {
                return _LBViewType;
            }
            set
            {
                _LBViewType = value;
            }
        }

        private string _LBSort = "";
        [Description("查询排序方式")]
        public string LBSort
        {
            get
            {
                return _LBSort;
            }
            set
            {
                _LBSort = value;
            }
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                object objID = null;
                if (this.selectedEntry != null)
                {
                    if (this.selectedEntry.DataBindItem.DataView.Table.Columns.Contains(TextColumnName))
                    {
                        string strText = this.selectedEntry.DataBindItem[TextColumnName].ToString();
                        //return this.selectedEntry.DataBindItem[TextColumnName].ToString();
                    }
                }
                return base.Text;
            }
            set
            {
                this.TriggersEnabled = false;

                selectedEntry = null;
                //this.SelectedItemID = null;
                if (value != null && value.ToString() != "" && _DataSource != null)
                {
                    if (_DataSource.Table.Columns.Contains(IDColumnName) && _DataSource.Table.Columns.Contains(TextColumnName))
                    {
                        foreach (AutoCompleteEntry entry in this.items)
                        {
                            if (entry.DataBindItem[TextColumnName].ToString() == value.ToString())
                            {
                                //this.SelectedItemID = entry.DataBindItem[IDColumnName].ToString();
                                selectedEntry = entry;
                                //this.TriggersEnabled = true;
                                break;
                            }
                        }
                    }

                    //可能会导致卡死，暂时注释
                    //if (LBViewType > 0 && TextColumnName != ""&& selectedEntry==null)
                    //{
                    //    DataView dvTemp = ExecuteSQL.CallView(LBViewType, "1", TextColumnName + "='" + value.ToString() + "'", LBSort).DefaultView;
                    //    if (dvTemp.Count > 0 )
                    //    {
                    //        PopDataSource = ExecuteSQL.CallView(LBViewType, "", "", LBSort).DefaultView;
                    //        if (_DataSource.Table.Columns.Contains(IDColumnName) && _DataSource.Table.Columns.Contains(TextColumnName))
                    //        {
                    //            foreach (AutoCompleteEntry entry in this.items)
                    //            {
                    //                if (entry.DataBindItem[TextColumnName].ToString() == value.ToString())
                    //                {
                    //                    //this.SelectedItemID = entry.DataBindItem[IDColumnName].ToString();
                    //                    selectedEntry = entry;
                    //                    //this.TriggersEnabled = true;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                base.Text = value;
                this.TriggersEnabled = true;
            }
        }

        public DataRowView SelectedRow
        {
            get
            {
                if (SelectedItemID != null)
                {
                    if (this.selectedEntry != null)
                    {
                        return this.selectedEntry.DataBindItem;
                    }
                }
                return null;
            }
        }

        public object SelectedItemID
        {
            get
            {
                object objID = null;
                if (this.Text!=null && this.Text!="")
                {
                    if (this.selectedEntry != null)
                    {
                        if (this.selectedEntry.DataBindItem.DataView.Table.Columns.Contains(IDColumnName))
                        {
                            return this.selectedEntry.DataBindItem[IDColumnName];
                        }
                    }
                    else
                    {
                        if (this.PopDataSource != null && this.PopDataSource.Table.Columns.Contains(IDColumnName))
                        {
                            DataRow[] drArys = this.PopDataSource.Table.Select(TextColumnName+"='"+ this.Text + "'");
                            if (drArys.Length > 0)
                            {
                                return drArys[0][IDColumnName];
                            }
                        }
                    }
                }
                return null;
            }
            set
            {
                selectedEntry = null;
                if (value!=null&& value.ToString()!="" && _DataSource != null)
                {
                    if (_DataSource.Table.Columns.Contains(IDColumnName)&& _DataSource.Table.Columns.Contains(TextColumnName))
                    {
                        foreach (AutoCompleteEntry entry in this.items)
                        {
                            if (entry.DataBindItem[IDColumnName].ToString() == value.ToString())
                            {
                                this.Text = entry.DataBindItem[TextColumnName].ToString();
                                selectedEntry = entry;
                                break;
                            }
                        }
                    }
                    //可能会导致卡死，暂时注释
                    //if (LBViewType > 0 && IDColumnName != "" && selectedEntry == null)
                    //{
                    //    DataView dvTemp = ExecuteSQL.CallView(LBViewType, "1", IDColumnName + "='" + value.ToString() + "'", LBSort).DefaultView;
                    //    if (dvTemp.Count > 0)
                    //    {
                    //        PopDataSource = ExecuteSQL.CallView(LBViewType, "", "", LBSort).DefaultView;
                    //        if (_DataSource.Table.Columns.Contains(IDColumnName) && _DataSource.Table.Columns.Contains(TextColumnName))
                    //        {
                    //            foreach (AutoCompleteEntry entry in this.items)
                    //            {
                    //                if (entry.DataBindItem[IDColumnName].ToString() == value.ToString())
                    //                {
                    //                    this.Text = entry.DataBindItem[TextColumnName].ToString();
                    //                    selectedEntry = entry;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }

        private AutoCompleteEntry selectedEntry = null;

        #endregion

        public AutoCompleteTextBox()
		{

			// Create the form that will hold the list
			this.popup = new Form();
			this.popup.StartPosition = FormStartPosition.Manual;
			this.popup.ShowInTaskbar = false;
			this.popup.FormBorderStyle = FormBorderStyle.None;
			this.popup.TopMost = true;
			this.popup.Deactivate += new EventHandler(Popup_Deactivate);

			// Create the list box that will hold mathcing items
			this.list = new ListBox();
			this.list.Cursor = Cursors.Hand;
			this.list.BorderStyle = BorderStyle.None;
			this.list.SelectedIndexChanged += new EventHandler(List_SelectedIndexChanged);
			this.list.MouseDown += new MouseEventHandler(List_MouseDown);
			this.list.ItemHeight = 25;
			this.list.DrawMode = DrawMode.OwnerDrawFixed;
			this.list.DrawItem += new DrawItemEventHandler(List_DrawItem);
			this.list.Dock = DockStyle.Fill;
            this.list.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.popup.Controls.Add(this.list);

            this.drapDown = new Panel();
            this.drapDown.Name = "DrapDown";
            this.drapDown.Height = 20;
            this.drapDown.Width = 20;
            this.drapDown.Text = "";
            this.drapDown.Location = new Point(this.Width - this.drapDown.Width-2, (this.Height - this.drapDown.Height) / 2);
            this.drapDown.Click += DrapDown_Click;
            this.drapDown.Paint += DrapDown_Paint;
            this.drapDown.Cursor = Cursors.Hand;
            // Add the list box to the popup form
            this.Controls.Add(drapDown);

			// Add default triggers.
			this.triggers.Add(new TextLengthTrigger(0));
			this.triggers.Add(new ShortCutTrigger(Keys.Enter, TriggerState.SelectAndConsume));
			this.triggers.Add(new ShortCutTrigger(Keys.Tab, TriggerState.Select));
			this.triggers.Add(new ShortCutTrigger(Keys.Control | Keys.Space, TriggerState.ShowAndConsume));
			this.triggers.Add(new ShortCutTrigger(Keys.Escape, TriggerState.HideAndConsume));
		}

        //重新刷新数据源
        public void ReadDataSource()
        {
            if (LBViewType > 0 && IDColumnName != "")
            {
                PopDataSource = ExecuteSQL.CallView(LBViewType, "", "", LBSort).DefaultView;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        private void DrapDown_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(new PointF(4, 7), new PointF(16, 7));
            path.AddLine(new PointF(16, 7), new PointF(10, 13));
            path.AddLine(new PointF(10, 13), new PointF(4, 7));
            e.Graphics.FillPath(Brushes.Black, path);
        }

        private void DrapDown_Click(object sender, EventArgs e)
        {
            if (!this.ReadOnly && this.Enabled)
            {
                if (LBViewType > 0)
                {
                    //点击下拉时重新读取数据
                    PopDataSource = ExecuteSQL.CallView(LBViewType, "", "", LBSort).DefaultView;
                }
                this.ShowList(false);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.drapDown.Location = new Point(this.Width - this.drapDown.Width-2, (this.Height - this.drapDown.Height) / 2);
        }

        protected virtual bool DefaultCmdKey(ref Message msg, Keys keyData)
		{
			bool val = base.ProcessCmdKey (ref msg, keyData);

			if (this.TriggersEnabled)
			{
				switch (this.Triggers.OnCommandKey(keyData))
				{
					case TriggerState.ShowAndConsume:
					{
						val = true;
						this.ShowList(true);
					} break;
					case TriggerState.Show:
					{
						this.ShowList(true);
					} break;
					case TriggerState.HideAndConsume:
					{
						val = true;
						this.HideList();
					} break;
					case TriggerState.Hide:
					{
						this.HideList();
					} break;
					case TriggerState.SelectAndConsume:
					{
						if (this.popup.Visible == true)
						{
							val = true;
							this.SelectCurrentItem();
						}
					} break;
					case TriggerState.Select:
					{
						if (this.popup.Visible == true)
						{
							this.SelectCurrentItem();
						}
					} break;
					default:
						break;
				}
			}

			return val;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
				{
					this.Mode = EntryMode.List;
					if (this.list.SelectedIndex > 0)
					{
						this.list.SelectedIndex--;
					}
					return true;
				} break;
				case Keys.Down:
				{
					this.Mode = EntryMode.List;
					if (this.list.SelectedIndex < this.list.Items.Count - 1)
					{
						this.list.SelectedIndex++;
					}
					return true;
				} break;
				default:
				{
					return DefaultCmdKey(ref msg, keyData);
				} break;
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged (e);

            if (this.TriggersEnabled)
			{
                switch (this.Triggers.OnTextChanged(this.Text))
                {
                    case TriggerState.Show:
                        {
                            if (LBViewType > 0)
                            {
                                PopDataSource = ExecuteSQL.CallView(LBViewType, "", "", LBSort).DefaultView;
                            }
                            this.ShowList(true);
                        }
                        break;
                    case TriggerState.Hide:
                        {
                            this.HideList();
                        }
                        break;
                    default:
                        {
                            this.UpdateList(true);
                        }
                        break;
                }
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus (e);

			if (!(this.Focused || this.popup.Focused || this.list.Focused))
			{
				this.HideList();

                if (!IsAllowNotExists)//校验改信息是否存在
                {
                    if (this.Text.TrimEnd() != "")
                    {
                        this.selectedEntry = null;
                        bool bolExists = false;
                        foreach (AutoCompleteEntry entry in this.Items)
                        {
                            if (entry.DisplayName == this.Text.TrimEnd())
                            {
                                bolExists = true;
                                this.selectedEntry = entry;
                                break;
                            }
                        }
                        if (!bolExists)
                        {
                            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("所选择的内容不存在,请重新输入！", "提示", MessageBoxButtons.YesNo) ==
                                DialogResult.Yes)
                            {
                                this.Focus();
                            }
                            else
                            {
                                this.Text = "";
                            }
                        }
                    }
                }
            }
		}

		protected virtual void SelectCurrentItem()
		{
			if (this.list.SelectedIndex == -1)
			{
                this.selectedEntry = null;

                return;
			}

			this.Focus();
            this.selectedEntry = this.Items[this.list.SelectedIndex] as AutoCompleteEntry;

            this.Text = this.list.SelectedItem.ToString();
			if (this.Text.Length > 0)
			{
				this.SelectionStart = this.Text.Length;
			}

			this.HideList();
		}

		protected virtual void ShowList(bool bolNeedFilter)
		{
			if (this.popup.Visible == false)
			{
				this.list.SelectedIndex = -1;
				this.UpdateList(bolNeedFilter);
				Point p = this.PointToScreen(new Point(0,0));
				p.X += this.PopupOffset.X;
				p.Y += this.Height + this.PopupOffset.Y;
				this.popup.Location = p;
				if (this.list.Items.Count > 0)
				{
					this.popup.Show();
					if (this.hook == null)
					{
						this.hook = new WinHook(this);
						this.hook.AssignHandle(this.FindForm().Handle);
					}
					this.Focus();
				}
			} 
			else
			{
				this.UpdateList(bolNeedFilter);
			}
		}

		protected virtual void HideList()
		{
			this.Mode = EntryMode.Text;
			if (this.hook != null)
				this.hook.ReleaseHandle();
			this.hook = null;
			this.popup.Hide();
		}

		protected virtual void UpdateList(bool bolNeedFilter)
		{
			object selectedItem = this.list.SelectedItem;

			this.list.Items.Clear();
			this.list.Items.AddRange(this.FilterList(this.Items, bolNeedFilter).ToObjectArray());

			if (selectedItem != null &&
				this.list.Items.Contains(selectedItem))
			{
				EntryMode oldMode = this.Mode;
				this.Mode = EntryMode.List;
				this.list.SelectedItem = selectedItem;
				this.Mode = oldMode;
			}

			if (this.list.Items.Count == 0)
			{
				this.HideList();
			} 
			else
			{
				int visItems = this.list.Items.Count;
				if (visItems > 8)
					visItems = 8;

				this.popup.Height = (visItems * this.list.ItemHeight) + 2;
				switch (this.BorderStyle)
				{
					case BorderStyle.FixedSingle:
					{
						this.popup.Height += 2;
					} break;
					case BorderStyle.Fixed3D:
					{
						this.popup.Height += 4;
					} break;
					case BorderStyle.None:
					default:
					{
					} break;
				}
				
				this.popup.Width = this.PopupWidth;

				if (this.list.Items.Count > 0 &&
					this.list.SelectedIndex == -1)
				{
					EntryMode oldMode = this.Mode;
					this.Mode = EntryMode.List;
					this.list.SelectedIndex = 0;
					this.Mode = oldMode;
				}

			}
		}

        protected virtual AutoCompleteEntryCollection FilterList(AutoCompleteEntryCollection list, bool bolNeedFilter)
        {
            int iFontMaxCount = 0;//最大字数
            string strMaxFont = "";
            AutoCompleteEntryCollection newList = new AutoCompleteEntryCollection();
            foreach (IAutoCompleteEntry entry in list)
            {
                AutoCompleteEntry autocom = (AutoCompleteEntry)entry;

                if (autocom.DisplayName.Length > iFontMaxCount)
                {
                    iFontMaxCount = autocom.DisplayName.Length;
                    strMaxFont = autocom.DisplayName;
                }

                if (!bolNeedFilter || autocom.DisplayName.ToUpper().Contains(this.Text.TrimEnd().ToUpper()))
                {
                    newList.Add(entry);
                }

                /*foreach (string match in entry.MatchStrings)
				{
					if (match.ToUpper().StartsWith(this.Text.ToUpper()))
					{
						newList.Add(entry);
						break;
					}
				}*/
            }
            Graphics g = this.CreateGraphics();
            SizeF sizeFont = g.MeasureString(strMaxFont, this.Font);
            this.PopupWidth = Math.Max((int)sizeFont.Width, this.Width);
            return newList;
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.Mode != EntryMode.List)
			{
				SelectCurrentItem();
			}
		}

		private void List_MouseDown(object sender, MouseEventArgs e)
		{
			for (int i=0; i<this.list.Items.Count; i++)
			{
				if (this.list.GetItemRectangle(i).Contains(e.X, e.Y))
				{
					this.list.SelectedIndex = i;
					this.SelectCurrentItem();
				}
			}
			this.HideList();
		}

		private void List_DrawItem(object sender, DrawItemEventArgs e)
		{
			Color bColor = e.BackColor;
			if (e.State == DrawItemState.Selected)
			{
				e.Graphics.FillRectangle(new SolidBrush(this.PopupSelectionBackColor), e.Bounds);
				e.Graphics.DrawString(this.list.Items[e.Index].ToString(), e.Font, new SolidBrush(this.PopupSelectionForeColor), e.Bounds, StringFormat.GenericDefault);
			} 
			else
			{
				e.DrawBackground();
				e.Graphics.DrawString(this.list.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
			}
		}

		private void Popup_Deactivate(object sender, EventArgs e)
		{
			if (!(this.Focused || this.popup.Focused || this.list.Focused))
			{
				this.HideList();
			}
		}

        
	}
}
