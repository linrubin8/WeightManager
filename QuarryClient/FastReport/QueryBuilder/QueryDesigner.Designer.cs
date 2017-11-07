namespace FastReport.FastQueryBuilder
{
    internal partial class QueryDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Table1", 0);
          System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("table 2", 0);
          System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("table 3");
          System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Table4");
          System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Table 5");
          this.tabControl1 = new System.Windows.Forms.TabControl();
          this.tabPage1 = new System.Windows.Forms.TabPage();
          this.splitContainer1 = new System.Windows.Forms.SplitContainer();
          this.splitContainer2 = new System.Windows.Forms.SplitContainer();
          this.tabControl2 = new System.Windows.Forms.TabControl();
          this.tabPage4 = new System.Windows.Forms.TabPage();
          this.dataGridView1 = new System.Windows.Forms.DataGridView();
          this.tabPage5 = new System.Windows.Forms.TabPage();
          this.linkView = new System.Windows.Forms.DataGridView();
          this.tabPage6 = new System.Windows.Forms.TabPage();
          this.button2 = new System.Windows.Forms.Button();
          this.button1 = new System.Windows.Forms.Button();
          this.groupedList = new System.Windows.Forms.ListBox();
          this.listView1 = new System.Windows.Forms.ListView();
          this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
          this.tabPage2 = new System.Windows.Forms.TabPage();
          this.richTextBox1 = new System.Windows.Forms.RichTextBox();
          this.tabPage3 = new System.Windows.Forms.TabPage();
          this.dataGridView2 = new System.Windows.Forms.DataGridView();
          this.toolStrip1 = new System.Windows.Forms.ToolStrip();
          this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
          this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
          this.tabControl1.SuspendLayout();
          this.tabPage1.SuspendLayout();
          this.splitContainer1.Panel1.SuspendLayout();
          this.splitContainer1.Panel2.SuspendLayout();
          this.splitContainer1.SuspendLayout();
          this.splitContainer2.Panel2.SuspendLayout();
          this.splitContainer2.SuspendLayout();
          this.tabControl2.SuspendLayout();
          this.tabPage4.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
          this.tabPage5.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.linkView)).BeginInit();
          this.tabPage6.SuspendLayout();
          this.tabPage2.SuspendLayout();
          this.tabPage3.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
          this.toolStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // tabControl1
          // 
          this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.tabControl1.Controls.Add(this.tabPage1);
          this.tabControl1.Controls.Add(this.tabPage2);
          this.tabControl1.Controls.Add(this.tabPage3);
          this.tabControl1.Location = new System.Drawing.Point(0, 28);
          this.tabControl1.Name = "tabControl1";
          this.tabControl1.SelectedIndex = 0;
          this.tabControl1.Size = new System.Drawing.Size(785, 481);
          this.tabControl1.TabIndex = 0;
          this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
          // 
          // tabPage1
          // 
          this.tabPage1.Controls.Add(this.splitContainer1);
          this.tabPage1.Location = new System.Drawing.Point(4, 22);
          this.tabPage1.Name = "tabPage1";
          this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage1.Size = new System.Drawing.Size(777, 455);
          this.tabPage1.TabIndex = 0;
          this.tabPage1.Text = "Designer";
          this.tabPage1.UseVisualStyleBackColor = true;
          // 
          // splitContainer1
          // 
          this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.splitContainer1.Location = new System.Drawing.Point(3, 3);
          this.splitContainer1.Name = "splitContainer1";
          // 
          // splitContainer1.Panel1
          // 
          this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
          // 
          // splitContainer1.Panel2
          // 
          this.splitContainer1.Panel2.Controls.Add(this.listView1);
          this.splitContainer1.Size = new System.Drawing.Size(771, 449);
          this.splitContainer1.SplitterDistance = 566;
          this.splitContainer1.TabIndex = 1;
          // 
          // splitContainer2
          // 
          this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.splitContainer2.Location = new System.Drawing.Point(0, 0);
          this.splitContainer2.Name = "splitContainer2";
          this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
          // 
          // splitContainer2.Panel1
          // 
          this.splitContainer2.Panel1.AllowDrop = true;
          this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
          this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
          this.splitContainer2.Panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel1_DragOver);
          this.splitContainer2.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel1_DragDrop);
          // 
          // splitContainer2.Panel2
          // 
          this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
          this.splitContainer2.Size = new System.Drawing.Size(566, 449);
          this.splitContainer2.SplitterDistance = 300;
          this.splitContainer2.TabIndex = 0;
          // 
          // tabControl2
          // 
          this.tabControl2.Controls.Add(this.tabPage4);
          this.tabControl2.Controls.Add(this.tabPage5);
          this.tabControl2.Controls.Add(this.tabPage6);
          this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.tabControl2.Location = new System.Drawing.Point(0, 0);
          this.tabControl2.Name = "tabControl2";
          this.tabControl2.SelectedIndex = 0;
          this.tabControl2.Size = new System.Drawing.Size(566, 145);
          this.tabControl2.TabIndex = 0;
          // 
          // tabPage4
          // 
          this.tabPage4.Controls.Add(this.dataGridView1);
          this.tabPage4.Location = new System.Drawing.Point(4, 22);
          this.tabPage4.Name = "tabPage4";
          this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage4.Size = new System.Drawing.Size(558, 119);
          this.tabPage4.TabIndex = 0;
          this.tabPage4.Text = "Select";
          this.tabPage4.UseVisualStyleBackColor = true;
          // 
          // dataGridView1
          // 
          this.dataGridView1.AllowUserToAddRows = false;
          this.dataGridView1.AllowUserToDeleteRows = false;
          this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.dataGridView1.Location = new System.Drawing.Point(3, 3);
          this.dataGridView1.Name = "dataGridView1";
          this.dataGridView1.RowHeadersWidth = 22;
          this.dataGridView1.Size = new System.Drawing.Size(552, 113);
          this.dataGridView1.TabIndex = 0;
          this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
          // 
          // tabPage5
          // 
          this.tabPage5.Controls.Add(this.linkView);
          this.tabPage5.Location = new System.Drawing.Point(4, 22);
          this.tabPage5.Name = "tabPage5";
          this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage5.Size = new System.Drawing.Size(558, 119);
          this.tabPage5.TabIndex = 1;
          this.tabPage5.Text = "Joins";
          this.tabPage5.UseVisualStyleBackColor = true;
          // 
          // linkView
          // 
          this.linkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.linkView.Dock = System.Windows.Forms.DockStyle.Fill;
          this.linkView.Location = new System.Drawing.Point(3, 3);
          this.linkView.Name = "linkView";
          this.linkView.Size = new System.Drawing.Size(552, 113);
          this.linkView.TabIndex = 0;
          this.linkView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.linkView_DataError);
          this.linkView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.linkView_CellContentClick);
          // 
          // tabPage6
          // 
          this.tabPage6.Controls.Add(this.button2);
          this.tabPage6.Controls.Add(this.button1);
          this.tabPage6.Controls.Add(this.groupedList);
          this.tabPage6.Location = new System.Drawing.Point(4, 22);
          this.tabPage6.Name = "tabPage6";
          this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage6.Size = new System.Drawing.Size(558, 119);
          this.tabPage6.TabIndex = 2;
          this.tabPage6.Text = "Group";
          this.tabPage6.UseVisualStyleBackColor = true;
          // 
          // button2
          // 
          this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.button2.Location = new System.Drawing.Point(531, 31);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(20, 18);
          this.button2.TabIndex = 2;
          this.button2.UseVisualStyleBackColor = true;
          this.button2.Click += new System.EventHandler(this.button2_Click);
          // 
          // button1
          // 
          this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.button1.Location = new System.Drawing.Point(531, 7);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(20, 18);
          this.button1.TabIndex = 1;
          this.button1.UseVisualStyleBackColor = true;
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // groupedList
          // 
          this.groupedList.Dock = System.Windows.Forms.DockStyle.Left;
          this.groupedList.FormattingEnabled = true;
          this.groupedList.Items.AddRange(new object[] {
            "thdsq",
            "anjhjq",
            "nhtnbq",
            "sagafg",
            "thjjyte",
            "gdfhji"});
          this.groupedList.Location = new System.Drawing.Point(3, 3);
          this.groupedList.Name = "groupedList";
          this.groupedList.Size = new System.Drawing.Size(522, 108);
          this.groupedList.TabIndex = 0;
          // 
          // listView1
          // 
          this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
          this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
          this.listView1.Location = new System.Drawing.Point(0, 0);
          this.listView1.MultiSelect = false;
          this.listView1.Name = "listView1";
          this.listView1.Size = new System.Drawing.Size(201, 449);
          this.listView1.TabIndex = 0;
          this.listView1.UseCompatibleStateImageBehavior = false;
          this.listView1.View = System.Windows.Forms.View.List;
          this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
          this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
          this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
          this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
          // 
          // columnHeader1
          // 
          this.columnHeader1.Text = "Tables";
          this.columnHeader1.Width = 0;
          // 
          // tabPage2
          // 
          this.tabPage2.Controls.Add(this.richTextBox1);
          this.tabPage2.Location = new System.Drawing.Point(4, 22);
          this.tabPage2.Name = "tabPage2";
          this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage2.Size = new System.Drawing.Size(777, 455);
          this.tabPage2.TabIndex = 1;
          this.tabPage2.Text = "SQL";
          this.tabPage2.UseVisualStyleBackColor = true;
          // 
          // richTextBox1
          // 
          this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
          this.richTextBox1.Location = new System.Drawing.Point(3, 3);
          this.richTextBox1.Name = "richTextBox1";
          this.richTextBox1.ReadOnly = true;
          this.richTextBox1.Size = new System.Drawing.Size(771, 449);
          this.richTextBox1.TabIndex = 0;
          this.richTextBox1.Text = "";
          // 
          // tabPage3
          // 
          this.tabPage3.Controls.Add(this.dataGridView2);
          this.tabPage3.Location = new System.Drawing.Point(4, 22);
          this.tabPage3.Name = "tabPage3";
          this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage3.Size = new System.Drawing.Size(777, 455);
          this.tabPage3.TabIndex = 2;
          this.tabPage3.Text = "Result";
          this.tabPage3.UseVisualStyleBackColor = true;
          // 
          // dataGridView2
          // 
          this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.dataGridView2.Location = new System.Drawing.Point(3, 3);
          this.dataGridView2.Name = "dataGridView2";
          this.dataGridView2.ReadOnly = true;
          this.dataGridView2.Size = new System.Drawing.Size(771, 449);
          this.dataGridView2.TabIndex = 0;
          // 
          // toolStrip1
          // 
          this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton6});
          this.toolStrip1.Location = new System.Drawing.Point(0, 0);
          this.toolStrip1.Name = "toolStrip1";
          this.toolStrip1.Size = new System.Drawing.Size(785, 25);
          this.toolStrip1.TabIndex = 1;
          this.toolStrip1.Text = "toolStrip1";
          // 
          // toolStripButton5
          // 
          this.toolStripButton5.Name = "toolStripButton5";
          this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
          this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton1_Click);
          // 
          // toolStripButton6
          // 
          this.toolStripButton6.Name = "toolStripButton6";
          this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
          this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton2_Click);
          // 
          // QueryDesigner
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.ClientSize = new System.Drawing.Size(785, 509);
          this.Controls.Add(this.toolStrip1);
          this.Controls.Add(this.tabControl1);
          this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
          this.MinimizeBox = false;
          this.Name = "QueryDesigner";
          this.ShowIcon = false;
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "Query Designer";
          this.Resize += new System.EventHandler(this.QueryDesigner_Resize);
          this.tabControl1.ResumeLayout(false);
          this.tabPage1.ResumeLayout(false);
          this.splitContainer1.Panel1.ResumeLayout(false);
          this.splitContainer1.Panel2.ResumeLayout(false);
          this.splitContainer1.ResumeLayout(false);
          this.splitContainer2.Panel2.ResumeLayout(false);
          this.splitContainer2.ResumeLayout(false);
          this.tabControl2.ResumeLayout(false);
          this.tabPage4.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
          this.tabPage5.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.linkView)).EndInit();
          this.tabPage6.ResumeLayout(false);
          this.tabPage2.ResumeLayout(false);
          this.tabPage3.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
          this.toolStrip1.ResumeLayout(false);
          this.toolStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListBox groupedList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView linkView;
    }
}