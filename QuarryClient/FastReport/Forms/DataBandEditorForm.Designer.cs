namespace FastReport.Forms
{
  partial class DataBandEditorForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBandEditorForm));
      this.tvDataSource = new FastReport.Controls.DataTreeView();
      this.lblNoData = new System.Windows.Forms.Label();
      this.cbxSort3 = new FastReport.Controls.DataColumnComboBox();
      this.lblSort3 = new FastReport.Controls.LabelLine();
      this.rbSortAsc3 = new System.Windows.Forms.RadioButton();
      this.rbSortDesc3 = new System.Windows.Forms.RadioButton();
      this.cbxSort2 = new FastReport.Controls.DataColumnComboBox();
      this.lblSort2 = new FastReport.Controls.LabelLine();
      this.rbSortAsc2 = new System.Windows.Forms.RadioButton();
      this.rbSortDesc2 = new System.Windows.Forms.RadioButton();
      this.cbxSort1 = new FastReport.Controls.DataColumnComboBox();
      this.lblSort1 = new FastReport.Controls.LabelLine();
      this.rbSortAsc1 = new System.Windows.Forms.RadioButton();
      this.rbSortDesc1 = new System.Windows.Forms.RadioButton();
      this.lblFilter = new System.Windows.Forms.Label();
      this.tbFilter = new FastReport.Controls.TextBoxButton();
      this.pageControl1 = new FastReport.Controls.PageControl();
      this.pnDataSource = new FastReport.Controls.PageControlPage();
      this.pnSort = new FastReport.Controls.PageControlPage();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pnFilter = new FastReport.Controls.PageControlPage();
      this.pageControl1.SuspendLayout();
      this.pnDataSource.SuspendLayout();
      this.pnSort.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.pnFilter.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(368, 246);
      this.btnOk.TabIndex = 1;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(448, 246);
      this.btnCancel.TabIndex = 2;
      // 
      // tvDataSource
      // 
      this.tvDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvDataSource.ExpandedNodes = ((System.Collections.Generic.List<string>)(resources.GetObject("tvDataSource.ExpandedNodes")));
      this.tvDataSource.HideSelection = false;
      this.tvDataSource.ImageIndex = 0;
      this.tvDataSource.Location = new System.Drawing.Point(3, 3);
      this.tvDataSource.Name = "tvDataSource";
      this.tvDataSource.SelectedImageIndex = 0;
      this.tvDataSource.ShowColumns = false;
      this.tvDataSource.ShowDataSources = true;
      this.tvDataSource.ShowEnabledOnly = true;
      this.tvDataSource.ShowNone = true;
      this.tvDataSource.ShowParameters = false;
      this.tvDataSource.ShowRelations = false;
      this.tvDataSource.ShowTotals = false;
      this.tvDataSource.ShowVariables = false;
      this.tvDataSource.Size = new System.Drawing.Size(368, 212);
      this.tvDataSource.TabIndex = 0;
      this.tvDataSource.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDataSource_NodeMouseDoubleClick);
      this.tvDataSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDataSource_AfterSelect);
      // 
      // lblNoData
      // 
      this.lblNoData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblNoData.Location = new System.Drawing.Point(3, 3);
      this.lblNoData.Name = "lblNoData";
      this.lblNoData.Padding = new System.Windows.Forms.Padding(20);
      this.lblNoData.Size = new System.Drawing.Size(368, 212);
      this.lblNoData.TabIndex = 1;
      this.lblNoData.Text = "NoData";
      this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cbxSort3
      // 
      this.cbxSort3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxSort3.Location = new System.Drawing.Point(16, 34);
      this.cbxSort3.Name = "cbxSort3";
      this.cbxSort3.Size = new System.Drawing.Size(203, 21);
      this.cbxSort3.TabIndex = 0;
      // 
      // lblSort3
      // 
      this.lblSort3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblSort3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblSort3.Location = new System.Drawing.Point(16, 4);
      this.lblSort3.Name = "lblSort3";
      this.lblSort3.Size = new System.Drawing.Size(342, 16);
      this.lblSort3.TabIndex = 1;
      this.lblSort3.Text = "Then by";
      // 
      // rbSortAsc3
      // 
      this.rbSortAsc3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortAsc3.Location = new System.Drawing.Point(235, 27);
      this.rbSortAsc3.Name = "rbSortAsc3";
      this.rbSortAsc3.Size = new System.Drawing.Size(120, 17);
      this.rbSortAsc3.TabIndex = 2;
      this.rbSortAsc3.TabStop = true;
      this.rbSortAsc3.Text = "Ascending";
      this.rbSortAsc3.UseVisualStyleBackColor = true;
      // 
      // rbSortDesc3
      // 
      this.rbSortDesc3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortDesc3.Location = new System.Drawing.Point(235, 47);
      this.rbSortDesc3.Name = "rbSortDesc3";
      this.rbSortDesc3.Size = new System.Drawing.Size(120, 17);
      this.rbSortDesc3.TabIndex = 2;
      this.rbSortDesc3.TabStop = true;
      this.rbSortDesc3.Text = "Descending";
      this.rbSortDesc3.UseVisualStyleBackColor = true;
      // 
      // cbxSort2
      // 
      this.cbxSort2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxSort2.Location = new System.Drawing.Point(16, 32);
      this.cbxSort2.Name = "cbxSort2";
      this.cbxSort2.Size = new System.Drawing.Size(199, 21);
      this.cbxSort2.TabIndex = 0;
      // 
      // lblSort2
      // 
      this.lblSort2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblSort2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblSort2.Location = new System.Drawing.Point(16, 4);
      this.lblSort2.Name = "lblSort2";
      this.lblSort2.Size = new System.Drawing.Size(342, 16);
      this.lblSort2.TabIndex = 1;
      this.lblSort2.Text = "Then by";
      // 
      // rbSortAsc2
      // 
      this.rbSortAsc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortAsc2.Location = new System.Drawing.Point(235, 24);
      this.rbSortAsc2.Name = "rbSortAsc2";
      this.rbSortAsc2.Size = new System.Drawing.Size(120, 17);
      this.rbSortAsc2.TabIndex = 2;
      this.rbSortAsc2.TabStop = true;
      this.rbSortAsc2.Text = "Ascending";
      this.rbSortAsc2.UseVisualStyleBackColor = true;
      // 
      // rbSortDesc2
      // 
      this.rbSortDesc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortDesc2.Location = new System.Drawing.Point(235, 44);
      this.rbSortDesc2.Name = "rbSortDesc2";
      this.rbSortDesc2.Size = new System.Drawing.Size(120, 17);
      this.rbSortDesc2.TabIndex = 2;
      this.rbSortDesc2.TabStop = true;
      this.rbSortDesc2.Text = "Descending";
      this.rbSortDesc2.UseVisualStyleBackColor = true;
      // 
      // cbxSort1
      // 
      this.cbxSort1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxSort1.Location = new System.Drawing.Point(16, 44);
      this.cbxSort1.Name = "cbxSort1";
      this.cbxSort1.Size = new System.Drawing.Size(199, 21);
      this.cbxSort1.TabIndex = 0;
      // 
      // lblSort1
      // 
      this.lblSort1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblSort1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblSort1.Location = new System.Drawing.Point(16, 16);
      this.lblSort1.Name = "lblSort1";
      this.lblSort1.Size = new System.Drawing.Size(342, 16);
      this.lblSort1.TabIndex = 1;
      this.lblSort1.Text = "Sort by";
      // 
      // rbSortAsc1
      // 
      this.rbSortAsc1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortAsc1.Location = new System.Drawing.Point(235, 36);
      this.rbSortAsc1.Name = "rbSortAsc1";
      this.rbSortAsc1.Size = new System.Drawing.Size(120, 17);
      this.rbSortAsc1.TabIndex = 2;
      this.rbSortAsc1.TabStop = true;
      this.rbSortAsc1.Text = "Ascending";
      this.rbSortAsc1.UseVisualStyleBackColor = true;
      // 
      // rbSortDesc1
      // 
      this.rbSortDesc1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.rbSortDesc1.Location = new System.Drawing.Point(235, 56);
      this.rbSortDesc1.Name = "rbSortDesc1";
      this.rbSortDesc1.Size = new System.Drawing.Size(120, 17);
      this.rbSortDesc1.TabIndex = 2;
      this.rbSortDesc1.TabStop = true;
      this.rbSortDesc1.Text = "Descending";
      this.rbSortDesc1.UseVisualStyleBackColor = true;
      // 
      // lblFilter
      // 
      this.lblFilter.AutoSize = true;
      this.lblFilter.Location = new System.Drawing.Point(16, 16);
      this.lblFilter.Name = "lblFilter";
      this.lblFilter.Size = new System.Drawing.Size(90, 13);
      this.lblFilter.TabIndex = 1;
      this.lblFilter.Text = "Filter expression:";
      // 
      // tbFilter
      // 
      this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbFilter.Image = null;
      this.tbFilter.Location = new System.Drawing.Point(16, 36);
      this.tbFilter.Name = "tbFilter";
      this.tbFilter.Size = new System.Drawing.Size(342, 21);
      this.tbFilter.TabIndex = 0;
      this.tbFilter.ButtonClick += new System.EventHandler(this.tbFilter_ButtonClick);
      // 
      // pageControl1
      // 
      this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pageControl1.Controls.Add(this.pnDataSource);
      this.pageControl1.Controls.Add(this.pnSort);
      this.pageControl1.Controls.Add(this.pnFilter);
      this.pageControl1.Location = new System.Drawing.Point(12, 12);
      this.pageControl1.Name = "pageControl1";
      this.pageControl1.SelectorWidth = 135;
      this.pageControl1.Size = new System.Drawing.Size(510, 220);
      this.pageControl1.TabIndex = 3;
      this.pageControl1.Text = "pageControl1";
      // 
      // pnDataSource
      // 
      this.pnDataSource.BackColor = System.Drawing.SystemColors.Window;
      this.pnDataSource.Controls.Add(this.tvDataSource);
      this.pnDataSource.Controls.Add(this.lblNoData);
      this.pnDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnDataSource.Location = new System.Drawing.Point(135, 1);
      this.pnDataSource.Name = "pnDataSource";
      this.pnDataSource.Padding = new System.Windows.Forms.Padding(3);
      this.pnDataSource.Size = new System.Drawing.Size(374, 218);
      this.pnDataSource.TabIndex = 0;
      this.pnDataSource.Text = "Data source";
      // 
      // pnSort
      // 
      this.pnSort.BackColor = System.Drawing.SystemColors.Window;
      this.pnSort.Controls.Add(this.panel3);
      this.pnSort.Controls.Add(this.panel2);
      this.pnSort.Controls.Add(this.panel1);
      this.pnSort.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnSort.Location = new System.Drawing.Point(135, 1);
      this.pnSort.Name = "pnSort";
      this.pnSort.Size = new System.Drawing.Size(374, 218);
      this.pnSort.TabIndex = 1;
      this.pnSort.Text = "Sort";
      // 
      // panel3
      // 
      this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel3.Controls.Add(this.lblSort3);
      this.panel3.Controls.Add(this.cbxSort3);
      this.panel3.Controls.Add(this.rbSortAsc3);
      this.panel3.Controls.Add(this.rbSortDesc3);
      this.panel3.Location = new System.Drawing.Point(0, 140);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(374, 68);
      this.panel3.TabIndex = 5;
      // 
      // panel2
      // 
      this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel2.Controls.Add(this.lblSort2);
      this.panel2.Controls.Add(this.cbxSort2);
      this.panel2.Controls.Add(this.rbSortAsc2);
      this.panel2.Controls.Add(this.rbSortDesc2);
      this.panel2.Location = new System.Drawing.Point(0, 76);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(374, 64);
      this.panel2.TabIndex = 4;
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this.lblSort1);
      this.panel1.Controls.Add(this.cbxSort1);
      this.panel1.Controls.Add(this.rbSortAsc1);
      this.panel1.Controls.Add(this.rbSortDesc1);
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(374, 76);
      this.panel1.TabIndex = 3;
      // 
      // pnFilter
      // 
      this.pnFilter.BackColor = System.Drawing.SystemColors.Window;
      this.pnFilter.Controls.Add(this.tbFilter);
      this.pnFilter.Controls.Add(this.lblFilter);
      this.pnFilter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnFilter.Location = new System.Drawing.Point(135, 1);
      this.pnFilter.Name = "pnFilter";
      this.pnFilter.Size = new System.Drawing.Size(374, 218);
      this.pnFilter.TabIndex = 2;
      this.pnFilter.Text = "Filter";
      // 
      // DataBandEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(532, 278);
      this.Controls.Add(this.pageControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(540, 312);
      this.Name = "DataBandEditorForm";
      this.ShowIcon = false;
      this.Text = "Edit DataBand";
      this.Shown += new System.EventHandler(this.DataBandEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataBandEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pageControl1, 0);
      this.pageControl1.ResumeLayout(false);
      this.pnDataSource.ResumeLayout(false);
      this.pnSort.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.pnFilter.ResumeLayout(false);
      this.pnFilter.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private FastReport.Controls.DataTreeView tvDataSource;
    private System.Windows.Forms.Label lblFilter;
    private FastReport.Controls.TextBoxButton tbFilter;
    private System.Windows.Forms.Label lblNoData;
    private FastReport.Controls.DataColumnComboBox cbxSort1;
    private System.Windows.Forms.RadioButton rbSortDesc3;
    private System.Windows.Forms.RadioButton rbSortDesc2;
    private System.Windows.Forms.RadioButton rbSortDesc1;
    private System.Windows.Forms.RadioButton rbSortAsc3;
    private System.Windows.Forms.RadioButton rbSortAsc2;
    private System.Windows.Forms.RadioButton rbSortAsc1;
    private FastReport.Controls.LabelLine lblSort3;
    private FastReport.Controls.LabelLine lblSort2;
    private FastReport.Controls.LabelLine lblSort1;
    private FastReport.Controls.DataColumnComboBox cbxSort3;
    private FastReport.Controls.DataColumnComboBox cbxSort2;
    private FastReport.Controls.PageControl pageControl1;
    private FastReport.Controls.PageControlPage pnDataSource;
    private FastReport.Controls.PageControlPage pnSort;
    private FastReport.Controls.PageControlPage pnFilter;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel1;
  }
}
