using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace FastReport.FastQueryBuilder
{
    internal partial class TableView : UserControl, ITableView
    {
        private int frameWidth = 3;
        private Point oldPos = new Point(0,0);
        private Point minSize = new Point(50, 20);
        private TableBorder MyBorder;
        private Table table;
        private Rectangle dragBoxFromMouseDown;
        private Point screenOffset;
        private Size oldSize; 
        FqbCheckedListBox checkedListBox1;

        public TableView()
        {
            InitializeComponent();
            
            MyBorder = new TableBorder(this);
            label1.Left = frameWidth;
            label1.Top = frameWidth;
            label1.Width = ClientRectangle.Width - 2 * frameWidth;

            checkedListBox1 = new FqbCheckedListBox();
            checkedListBox1.AllowDrop = true;
            checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.IntegralHeight = false;
            checkedListBox1.Location = new System.Drawing.Point(18, 43);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(115, 132);
            checkedListBox1.TabIndex = 1;
            checkedListBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseUp);
            checkedListBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.checkedListBox1_DragOver);
            checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            checkedListBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.checkedListBox1_DragDrop);
            checkedListBox1.MouseEnter += new System.EventHandler(this.checkedListBox1_MouseEnter);
            checkedListBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseMove);
            checkedListBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseDown);
            checkedListBox1.VertScrollValueChanged += new ScrollEventHandler(this.checkedListBox1_Scroll);
            checkedListBox1.Parent = this;

            checkedListBox1.Top = frameWidth + label1.Height;
            checkedListBox1.Left = frameWidth;
            checkedListBox1.Width = ClientRectangle.Width - 2 * frameWidth;
            checkedListBox1.Height = ClientRectangle.Height - label1.Height - frameWidth * 2;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BringToFront();
                Update();
                Win32.ReleaseCapture();
                Win32.SendMessage(Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
            }
        }

        #region ITableView Members

        public event EventHandler OnChangeAlias;

        public event CheckFieldEventHandler OnSelectField;

        public event AddLinkEventHandler OnAddLink;

        public event AddTableEventHandler OnDeleteTable;

        public void DoAddLink()
        {
           //MessageBox.Show("Оно самое...");
        }

        public void SetTableName(string tableName)
        {
            label1.Text = tableName;
        }

        public void SetTabeleAlias()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Table Table
        {
            get{ return table;  }
            set 
            { 
                table = value;
                SetTableName(table.getNameAndAlias());
                checkedListBox1.Items.Add('*');
                foreach (Field fld in table.FieldList)
                {
                    checkedListBox1.Items.Add(fld);
                }
            }
        }

        public Point GetPosition(Field field, LinkPosition lp)
        {            
            int pos = checkedListBox1.FindString(field.ToString());
            Rectangle rec = checkedListBox1.GetItemRectangle(pos);
            Point pnt = new Point();
            pnt.Y = rec.Top + rec.Height/2;

            if (pnt.Y < 0)
                pnt.Y = 0;
            else if (pnt.Y > checkedListBox1.Height)
                pnt.Y = checkedListBox1.Height;

            if (lp == LinkPosition.Left)
                pnt.X =  - 10;
            else
                pnt.X = checkedListBox1.Width + 10;

            return checkedListBox1.PointToScreen(pnt);
        }

        public int GetLeft()
        {
            return Left;
        }

        public int GetWidth()
        {
            return Width;
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
            Parent.Update();
        }

        private void TableView_MouseMove(object sender, MouseEventArgs e)
        {
            if (MyBorder.IsResize)
            {
                switch (MyBorder.SelectedBorder)
                {
                    case BorderPosition.Top:
                        if ((Height - e.Y) >= minSize.Y)
                            SetBounds(Left, Top + e.Y, Width, Height - e.Y);
                        break;
                    case BorderPosition.Bottom:
                        if ((Height - (oldPos.Y - e.Y)) >= minSize.Y)
                            Height = Height - (oldPos.Y - e.Y);
                        break;
                    case BorderPosition.Left:
                        if ((Width - e.X) >= minSize.X)
                            SetBounds(Left + e.X, Top, Width - e.X, Height);
                        break;
                    case BorderPosition.Right:
                        if ((Width - (oldPos.X - e.X)) >= minSize.X)
                            Width = Width - (oldPos.X - e.X);
                        break;                    
                }
            }
            else
            {
                if ((e.X > ClientRectangle.Width - frameWidth) && (e.X < ClientRectangle.Width))
                    MyBorder.SelectedBorder = BorderPosition.Right;
                else
                    if ((e.X > 0) && (e.X < frameWidth))
                        MyBorder.SelectedBorder = BorderPosition.Left;
                    else
                        if ((e.Y > 0) && (e.Y < frameWidth))
                            MyBorder.SelectedBorder = BorderPosition.Top;
                        else
                            if ((e.Y > ClientRectangle.Height - frameWidth) && (e.Y < ClientRectangle.Height))
                                MyBorder.SelectedBorder = BorderPosition.Bottom;
                            else
                                MyBorder.SelectedBorder = BorderPosition.None;
            }
            oldPos.X = e.X;
            oldPos.Y = e.Y;
            Parent.Refresh();
        }

        private void checkedListBox1_MouseEnter(object sender, EventArgs e)
        {
            MyBorder.SelectedBorder = BorderPosition.None;
        }

        private void TableView_MouseDown(object sender, MouseEventArgs e)
        {
            if ((MyBorder.SelectedBorder != BorderPosition.None) && (e.Button == MouseButtons.Left))
                MyBorder.IsResize = true;
        }

        private void TableView_MouseUp(object sender, MouseEventArgs e)
        {
            MyBorder.IsResize = false;
        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkedListBox1.SelectedItem != null)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if ((dragBoxFromMouseDown != Rectangle.Empty) &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    screenOffset = SystemInformation.WorkingArea.Location;
                    checkedListBox1.DoDragDrop(checkedListBox1.SelectedItem, DragDropEffects.Copy);
                }
            }
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void checkedListBox1_DragOver(object sender, DragEventArgs e)
        {
            TableView _sender = ((sender as Control).Parent as TableView);
            int n = _sender.checkedListBox1.IndexFromPoint(_sender.checkedListBox1.PointToClient(new Point(e.X, e.Y)));
            Field _sended = _sender.checkedListBox1.Items[n] as Field;
            Field _current = (Field)e.Data.GetData(typeof(Field));

            if ((_sender.Table != _current.Table) && ( _current.FieldType == _sended.FieldType))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void checkedListBox1_DragDrop(object sender, DragEventArgs e)
        {
            TableView _sender = ((sender as Control).Parent as TableView);
            int n = _sender.checkedListBox1.IndexFromPoint(_sender.checkedListBox1.PointToClient(new Point(e.X, e.Y)));
            Field _sended = _sender.checkedListBox1.Items[n] as Field;
            Field _current = (Field)e.Data.GetData(typeof(Field));

            if ((_sender.Table != _current.Table) && (_current.FieldType == _sended.FieldType))
            {
                if (OnAddLink != null)
                    OnAddLink(sender, new AddLinkEventArgs(_current, _sended));
                if (Parent != null)
                    Parent.Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            if (OnDeleteTable != null)
            {
                AddTableEventArgs ate = new AddTableEventArgs(this.table, new Point());
                OnDeleteTable(sender, ate);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            InputBox box = new InputBox();            
            box.TextBox.Text = table.Alias;
            if (box.ShowDialog() == DialogResult.OK)
            {
                if (OnChangeAlias != null)
                    OnChangeAlias(sender, e);
                table.Alias = box.TextBox.Text;
                label1.Text = table.getNameAndAlias();
            }
        }

        private void TableView_Paint(object sender, PaintEventArgs e)
        {
            Parent.Update();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (OnSelectField != null)
            {
                if (checkedListBox1.Items[e.Index] is Field)
                {
                    Field field = checkedListBox1.Items[e.Index] as Field;
                    CheckFieldEventArgs e2 = new CheckFieldEventArgs(field);
                    e2.value = e.NewValue == CheckState.Checked;
                    OnSelectField(sender, e2);
                    checkedListBox1.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
                    checkedListBox1.SetItemChecked(0, false);
                    checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
                }
                else
                {
                    for (int i = 1; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, e.NewValue == CheckState.Checked);
                    }
                }
            }
        }

        private void TableView_Move(object sender, EventArgs e)
        {
            if (Parent != null)
                Parent.Refresh();
        }

        private void checkedListBox1_Scroll(object sender, ScrollEventArgs e)
        {
            if (Parent != null)
                Parent.Refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Height <= label1.Height + 6)
                Height = oldSize.Height;
            else
            {
                oldSize = this.Size;
                Height = label1.Height + 6;
            }
            Parent.Refresh();
        }

    }
}
