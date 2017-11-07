using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.FastQueryBuilder
{
    internal partial class QueryDesigner : Form, IQueryDesigner
    {
        private Rectangle dragBoxFromMouseDown;
        private Point screenOffset;
        private PosCounter posC = new PosCounter(10, 10);
        private List<Link> links;
        private List<Field> groupedFields;
        private int tabPrevIndex;

        private BindingSource biSourceQuery = new BindingSource();
        private BindingSource biSourceGrouped = new BindingSource();
        private BindingSource biSourceLinks = new BindingSource();

        public QueryDesigner()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 0;
            posC.MaxX = ClientRectangle.Width;
            posC.MaxY = ClientRectangle.Height;

            MyRes res = new MyRes("Forms,QueryBuilder");

            Text = res.Get("");
            toolStripButton5.ToolTipText = Res.Get("Buttons,OK");
            toolStripButton6.ToolTipText = Res.Get("Buttons,Cancel");
            tabPage1.Text = res.Get("Designer");
            tabPage2.Text = res.Get("Sql");
            tabPage3.Text = res.Get("Result");
            tabPage4.Text = res.Get("Select");
            tabPage5.Text = res.Get("Joins");
            tabPage6.Text = res.Get("Group");


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[0].HeaderText = res.Get("Name");
            dataGridView1.Columns[0].DataPropertyName = "Name";
            dataGridView1.Columns[0].ReadOnly = true;            

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[1].HeaderText = res.Get("Alias");
            dataGridView1.Columns[1].DataPropertyName = "Alias";
            dataGridView1.Columns[1].Width = 80;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[2].HeaderText = res.Get("Filter");
            dataGridView1.Columns[2].DataPropertyName = "Filter";
            dataGridView1.Columns[2].Width = 80;

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn());
            dataGridView1.Columns[3].HeaderText = res.Get("Group");
            dataGridView1.Columns[3].DataPropertyName = "Group";
            dataGridView1.Columns[3].Width = 80;

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn());
            dataGridView1.Columns[4].HeaderText = res.Get("Order");
            dataGridView1.Columns[4].DataPropertyName = "Order";
            dataGridView1.Columns[4].Width = 80;

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn());
            dataGridView1.Columns[5].HeaderText = res.Get("Func");
            dataGridView1.Columns[5].DataPropertyName = "Func";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewComboBoxCell cell4 = new DataGridViewComboBoxCell();
            cell4.Items.AddRange(new string[] { "", "Asc", "Desc" });
            cell4.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns[4].CellTemplate = cell4;
            dataGridView1.DataSource = biSourceQuery;

            DataGridViewComboBoxCell cell5 = new DataGridViewComboBoxCell();
            cell5.Items.AddRange(new string[] { "", "Avg", "Count", "Max", "Min", "Sum" });
            cell5.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns[5].CellTemplate = cell5;
            dataGridView1.DataSource = biSourceQuery;
            
            groupedList.DataSource = biSourceGrouped;
            biSourceGrouped.DataSource = groupedFields;

            // таблица связей
            linkView.AutoGenerateColumns = false;
            linkView.DataSource = biSourceLinks;
            biSourceLinks.DataSource = links;

            linkView.Columns.Add(new DataGridViewTextBoxColumn());
            linkView.Columns[0].HeaderText = res.Get("Name");
            linkView.Columns[0].DataPropertyName = "Name";
            linkView.Columns[0].Width = 300;
            linkView.Columns[0].ReadOnly = true;

            linkView.Columns.Add(new DataGridViewLinkColumn());
            linkView.Columns[1].HeaderText = res.Get("Editor");
            linkView.Columns[1].DataPropertyName = "Editor";

            linkView.Columns.Add(new DataGridViewLinkColumn());
            linkView.Columns[2].HeaderText = res.Get("Delete");
            linkView.Columns[2].DataPropertyName = "Delete";
            
            listView1.SmallImageList = Res.GetImages();
            toolStrip1.Renderer = Config.DesignerSettings.ToolStripRenderer;
            toolStripButton5.Image = Res.GetImage(210);
            toolStripButton6.Image = Res.GetImage(212);

            button1.Image = Res.GetImage(208);
            button2.Image = Res.GetImage(209);
        }

        #region IQueryDesigner Members

        public void DesignQuery()
        {
            if (OnGetTableList != null)
                OnGetTableList(null, null);
            ShowDialog();
        }

        public void DoFillTableList(List<Table> tl)
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (Table tbl in tl)
            {
                ListViewItem itm = new ListViewItem(tbl.Name);
                itm.ImageIndex = 222;
                itm.Tag = tbl;
                listView1.Items.Add(itm);
            }
            listView1.EndUpdate();
        }

        public TableView DoAddTable(Table table, Point position)
        {
            TableView tbl = new TableView();
            tbl.OnDeleteTable += OnDeleteTable;
            table.tableView = tbl;
            tbl.Table = table;
            tbl.Location = position;
            tbl.Parent = splitContainer2.Panel1;
            tbl.BringToFront();
            return tbl;
        }

        public void DoRefreshLinks()
        {
            biSourceLinks.DataSource = null;
            biSourceLinks.DataSource = links;
        }

        public List<Link> Links
        {
            get { return links; }
            set { links = value; }
        }

        public List<Field> Fields
        {
            get { return (List<Field>)dataGridView1.DataSource; }
            set 
            {
                biSourceQuery.DataSource = null;
                biSourceQuery.DataSource = value;
                UpdateGroupedField();
            }
        }

        public List<Field> Groups
        {
            get { return groupedFields; }
            set { groupedFields = value; }
        }

        public string SQLText
        {
            get
              { return richTextBox1.Text; }
            set
              { richTextBox1.Text = value; }
        }

        public object DataSource
        {
            get
              { return dataGridView2.DataSource; }
            set
              { dataGridView2.DataSource = value; }
        }

        public event EventHandler OnOk;

        public event EventHandler OnCancel;

        public event EventHandler OnGetTableList;

        public event AddTableEventHandler OnAddTable;

        public event EventHandler OnGenerateSQL;

        public event EventHandler OnRunSQL;

        #endregion

        private void OnDeleteTable(object sender, AddTableEventArgs e)
        {
            for (int i = links.Count - 1; i >= 0; i--)
            {
                if ((links[i].From.Table == e.table) || (links[i].To.Table == e.table))
                    links.RemoveAt(i);
            }
            splitContainer1.Panel1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (OnOk != null)
                OnOk(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (OnCancel != null)
                OnCancel(sender, e);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListView _sender = sender as ListView;
            if (_sender != null)
            {
                if ((OnAddTable != null) && (_sender.SelectedItems.Count != 0))
                {
                    AddTableEventArgs ate = new AddTableEventArgs(_sender.SelectedItems[0].Tag as Table, posC.Next);
                    OnAddTable(sender, ate);
                }
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listView1.GetItemAt(e.X, e.Y) != null)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if ((dragBoxFromMouseDown != Rectangle.Empty) &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    screenOffset = SystemInformation.WorkingArea.Location;
                    listView1.DoDragDrop(listView1.SelectedItems[0], DragDropEffects.Copy);
                }
            }
        }

        private void splitContainer2_Panel1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void splitContainer2_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem _item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            if (_item != null)
            {
                if (OnAddTable != null)
                {
                    Point p = (sender as Panel).PointToClient(new Point(e.X, e.Y));
                    AddTableEventArgs ate = new AddTableEventArgs(_item.Tag as Table, p);
                    OnAddTable(sender, ate);                    
                }
            }
        }

        private void QueryDesigner_Resize(object sender, EventArgs e)
        {
            posC.MaxX = ClientRectangle.Width;
            posC.MaxY = ClientRectangle.Height;
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (Link link in links)
            {
                link.Paint(this.splitContainer2.Panel1);
            }
        }
       
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateGroupedField();
        }

        private void UpdateGroupedField()
        {
            if (groupedFields == null) return;
            List<Field> flds = biSourceQuery.DataSource as List<Field>;
            groupedList.BeginUpdate();
            biSourceGrouped.DataSource = null;
            groupedFields.Clear();
            foreach (Field f in flds)
            {
                if (f.Group)
                    groupedFields.Add(f);
            }
            biSourceGrouped.DataSource = groupedFields;
            groupedList.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // move down            
            if (biSourceGrouped.Count == 0 || biSourceGrouped.Position == biSourceGrouped.Count - 1)
                return;

            int r = biSourceGrouped.Position;

            Field f = groupedFields[r];
            groupedFields[r] = groupedFields[r + 1];
            groupedFields[r + 1] = f;

            biSourceGrouped.DataSource = null;
            biSourceGrouped.DataSource = groupedFields;

            biSourceGrouped.Position = r + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (biSourceGrouped.Count == 0 || biSourceGrouped.Position == 0)
                return;
            // move up
            int r = biSourceGrouped.Position;

            Field f = groupedFields[r];
            groupedFields[r] = groupedFields[r - 1];
            groupedFields[r - 1] = f;

            biSourceGrouped.DataSource = null;
            biSourceGrouped.DataSource = groupedFields;

            biSourceGrouped.Position = r - 1;
        }

        private void linkView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
              return;

            if (e.ColumnIndex == 1)
            {
                Link lnk = (Link)biSourceLinks.List[e.RowIndex];
                JoinEditorForm frm = new JoinEditorForm();
                frm.link = lnk;
                frm.ShowDialog();
                lnk = frm.link;
            }
            else if (e.ColumnIndex == 2)
            {
                links.RemoveAt(e.RowIndex);
                linkView.DataSource = null;
                linkView.DataSource = biSourceLinks;
                splitContainer1.Panel1.Refresh();
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 1:
                    if (tabPrevIndex != 2)
                    {
                        if (OnGenerateSQL != null)
                            OnGenerateSQL(sender, e);
                    }
                    break;
                case 2:
                    if (OnRunSQL != null)
                    {
                        try
                        {
                            OnRunSQL(sender, e);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, Res.Get("Messages,SQLError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }                                           
                    }
                    break;
                default:
                    break;
            }
            tabPrevIndex = e.TabPageIndex;
        }

        private void linkView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }        
    }
}
