namespace FastReport.Map.Forms
{
  partial class MapEditorForm
  {
    #region Fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion // Fields

    #region Protected Methods

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing"><b>true</b> if managed resources should be disposed. Otherwise, <b>false</b>.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion // Protected Methods

    #region Windows Forms Designer Generated Code

    private void InitializeComponent()
    {
      this.pnSample = new System.Windows.Forms.Panel();
      this.lblHint = new System.Windows.Forms.Label();
      this.tvMap = new System.Windows.Forms.TreeView();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.pcMap = new FastReport.Controls.PageControl();
      this.pageControlPage1 = new FastReport.Controls.PageControlPage();
      this.pcMap.SuspendLayout();
      this.pageControlPage1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(627, 528);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(708, 528);
      // 
      // pnSample
      // 
      this.pnSample.Cursor = System.Windows.Forms.Cursors.Default;
      this.pnSample.Location = new System.Drawing.Point(16, 252);
      this.pnSample.Name = "pnSample";
      this.pnSample.Size = new System.Drawing.Size(288, 232);
      this.pnSample.TabIndex = 15;
      this.pnSample.Paint += new System.Windows.Forms.PaintEventHandler(this.pnSample_Paint);
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
      // tvMap
      // 
      this.tvMap.HideSelection = false;
      this.tvMap.Location = new System.Drawing.Point(16, 36);
      this.tvMap.Name = "tvMap";
      this.tvMap.ShowRootLines = false;
      this.tvMap.Size = new System.Drawing.Size(288, 168);
      this.tvMap.TabIndex = 12;
      this.tvMap.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMap_AfterSelect);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(250, 216);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 23);
      this.btnUp.TabIndex = 10;
      this.btnUp.Text = " ";
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
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
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(97, 216);
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
      // pcMap
      // 
      this.pcMap.Controls.Add(this.pageControlPage1);
      this.pcMap.HighlightPageIndex = -1;
      this.pcMap.Location = new System.Drawing.Point(12, 12);
      this.pcMap.Name = "pcSeries";
      this.pcMap.SelectorWidth = 1;
      this.pcMap.Size = new System.Drawing.Size(324, 504);
      this.pcMap.TabIndex = 14;
      this.pcMap.Text = "pageControl1";
      // 
      // pageControlPage1
      // 
      this.pageControlPage1.BackColor = System.Drawing.SystemColors.Window;
      this.pageControlPage1.Controls.Add(this.lblHint);
      this.pageControlPage1.Controls.Add(this.tvMap);
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
      // MapEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(794, 562);
      this.Controls.Add(this.pcMap);
      this.Name = "MapEditorForm";
      this.Text = "Edit Map";
      this.Shown += new System.EventHandler(this.MapEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pcMap, 0);
      this.pcMap.ResumeLayout(false);
      this.pageControlPage1.ResumeLayout(false);
      this.pageControlPage1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion // Windows Forms Designer Generated Code

    private FastReport.Controls.PageControl pcMap;
    private FastReport.Controls.PageControlPage pageControlPage1;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.TreeView tvMap;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Panel pnSample;


  }
}