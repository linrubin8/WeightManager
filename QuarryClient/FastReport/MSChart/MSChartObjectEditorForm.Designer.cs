namespace FastReport.MSChart
{
  partial class MSChartObjectEditorForm
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
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.tvChart = new System.Windows.Forms.TreeView();
      this.pnSample = new System.Windows.Forms.Panel();
      this.pcSeries = new FastReport.Controls.PageControl();
      this.pageControlPage1 = new FastReport.Controls.PageControlPage();
      this.lblHint = new System.Windows.Forms.Label();
      this.pcSeries.SuspendLayout();
      this.pageControlPage1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(628, 528);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(708, 528);
      // 
      // btnDown
      // 
      this.btnDown.Location = new System.Drawing.Point(280, 216);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(24, 23);
      this.btnDown.TabIndex = 11;
      this.btnDown.Text = " ";
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(252, 216);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 23);
      this.btnUp.TabIndex = 10;
      this.btnUp.Text = " ";
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(96, 216);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 9;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(16, 216);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 8;
      this.btnAdd.Text = "Add...";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // tvChart
      // 
      this.tvChart.HideSelection = false;
      this.tvChart.Location = new System.Drawing.Point(16, 36);
      this.tvChart.Name = "tvChart";
      this.tvChart.ShowRootLines = false;
      this.tvChart.Size = new System.Drawing.Size(288, 168);
      this.tvChart.TabIndex = 12;
      this.tvChart.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvChart_AfterSelect);
      // 
      // pnSample
      // 
      this.pnSample.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pnSample.Location = new System.Drawing.Point(16, 252);
      this.pnSample.Name = "pnSample";
      this.pnSample.Size = new System.Drawing.Size(288, 232);
      this.pnSample.TabIndex = 13;
      this.pnSample.Paint += new System.Windows.Forms.PaintEventHandler(this.pnSample_Paint);
      this.pnSample.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnSample_MouseDown);
      // 
      // pcSeries
      // 
      this.pcSeries.Controls.Add(this.pageControlPage1);
      this.pcSeries.Location = new System.Drawing.Point(12, 12);
      this.pcSeries.Name = "pcSeries";
      this.pcSeries.SelectorWidth = 1;
      this.pcSeries.Size = new System.Drawing.Size(324, 504);
      this.pcSeries.TabIndex = 14;
      this.pcSeries.Text = "pageControl1";
      // 
      // pageControlPage1
      // 
      this.pageControlPage1.BackColor = System.Drawing.SystemColors.Window;
      this.pageControlPage1.Controls.Add(this.lblHint);
      this.pageControlPage1.Controls.Add(this.tvChart);
      this.pageControlPage1.Controls.Add(this.pnSample);
      this.pageControlPage1.Controls.Add(this.btnUp);
      this.pageControlPage1.Controls.Add(this.btnDown);
      this.pageControlPage1.Controls.Add(this.btnDelete);
      this.pageControlPage1.Controls.Add(this.btnAdd);
      this.pageControlPage1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pageControlPage1.Location = new System.Drawing.Point(1, 1);
      this.pageControlPage1.Name = "pageControlPage1";
      this.pageControlPage1.Size = new System.Drawing.Size(322, 502);
      this.pageControlPage1.TabIndex = 0;
      this.pageControlPage1.Text = "Page1";
      // 
      // lblHint
      // 
      this.lblHint.AutoSize = true;
      this.lblHint.Location = new System.Drawing.Point(16, 12);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(182, 13);
      this.lblHint.TabIndex = 14;
      this.lblHint.Text = "Click an item to set up its properties.";
      // 
      // MSChartObjectEditorForm
      // 
      this.AcceptButton = null;
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(794, 562);
      this.Controls.Add(this.pcSeries);
      this.Name = "MSChartObjectEditorForm";
      this.Text = "Chart Editor";
      this.Shown += new System.EventHandler(this.MSChartObjectEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MSChartObjectEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pcSeries, 0);
      this.pcSeries.ResumeLayout(false);
      this.pageControlPage1.ResumeLayout(false);
      this.pageControlPage1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.TreeView tvChart;
    private System.Windows.Forms.Panel pnSample;
    private FastReport.Controls.PageControl pcSeries;
    private FastReport.Controls.PageControlPage pageControlPage1;
    private System.Windows.Forms.Label lblHint;
  }
}
