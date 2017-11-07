namespace FastReport.Forms
{
  partial class CustomLabelForm
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
      this.gbLabel = new System.Windows.Forms.GroupBox();
      this.udColumns = new System.Windows.Forms.NumericUpDown();
      this.lblColumns = new System.Windows.Forms.Label();
      this.udRows = new System.Windows.Forms.NumericUpDown();
      this.lblRows = new System.Windows.Forms.Label();
      this.tbColumnGap = new System.Windows.Forms.TextBox();
      this.lblColumnGap = new System.Windows.Forms.Label();
      this.tbRowGap = new System.Windows.Forms.TextBox();
      this.lblRowGap = new System.Windows.Forms.Label();
      this.tbLabelHeight = new System.Windows.Forms.TextBox();
      this.lblLabelHeight = new System.Windows.Forms.Label();
      this.tbLabelWidth = new System.Windows.Forms.TextBox();
      this.lblLabelWidth = new System.Windows.Forms.Label();
      this.lblPaperWidth = new System.Windows.Forms.Label();
      this.tbPaperWidth = new System.Windows.Forms.TextBox();
      this.lblPaperHeight = new System.Windows.Forms.Label();
      this.tbPaperHeight = new System.Windows.Forms.TextBox();
      this.lblLeftMargin = new System.Windows.Forms.Label();
      this.tbLeftMargin = new System.Windows.Forms.TextBox();
      this.lblTopMargin = new System.Windows.Forms.Label();
      this.tbTopMargin = new System.Windows.Forms.TextBox();
      this.gbPaper = new System.Windows.Forms.GroupBox();
      this.cbLandscape = new System.Windows.Forms.CheckBox();
      this.cbxPaper = new System.Windows.Forms.ComboBox();
      this.gbSample = new System.Windows.Forms.GroupBox();
      this.rcSample = new FastReport.Controls.SampleReportControl();
      this.lblWarning = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.gbLabel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udColumns)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udRows)).BeginInit();
      this.gbPaper.SuspendLayout();
      this.gbSample.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(376, 404);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(456, 404);
      // 
      // gbLabel
      // 
      this.gbLabel.Controls.Add(this.udColumns);
      this.gbLabel.Controls.Add(this.lblColumns);
      this.gbLabel.Controls.Add(this.udRows);
      this.gbLabel.Controls.Add(this.lblRows);
      this.gbLabel.Controls.Add(this.tbColumnGap);
      this.gbLabel.Controls.Add(this.lblColumnGap);
      this.gbLabel.Controls.Add(this.tbRowGap);
      this.gbLabel.Controls.Add(this.lblRowGap);
      this.gbLabel.Controls.Add(this.tbLabelHeight);
      this.gbLabel.Controls.Add(this.lblLabelHeight);
      this.gbLabel.Controls.Add(this.tbLabelWidth);
      this.gbLabel.Controls.Add(this.lblLabelWidth);
      this.gbLabel.Location = new System.Drawing.Point(8, 220);
      this.gbLabel.Name = "gbLabel";
      this.gbLabel.Size = new System.Drawing.Size(264, 172);
      this.gbLabel.TabIndex = 1;
      this.gbLabel.TabStop = false;
      this.gbLabel.Text = "Label parameters";
      // 
      // udColumns
      // 
      this.udColumns.Location = new System.Drawing.Point(168, 92);
      this.udColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.udColumns.Name = "udColumns";
      this.udColumns.Size = new System.Drawing.Size(84, 20);
      this.udColumns.TabIndex = 3;
      this.udColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.udColumns.ValueChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblColumns
      // 
      this.lblColumns.AutoSize = true;
      this.lblColumns.Location = new System.Drawing.Point(8, 96);
      this.lblColumns.Name = "lblColumns";
      this.lblColumns.Size = new System.Drawing.Size(51, 13);
      this.lblColumns.TabIndex = 2;
      this.lblColumns.Text = "Columns:";
      // 
      // udRows
      // 
      this.udRows.Location = new System.Drawing.Point(168, 68);
      this.udRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.udRows.Name = "udRows";
      this.udRows.Size = new System.Drawing.Size(84, 20);
      this.udRows.TabIndex = 3;
      this.udRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.udRows.ValueChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblRows
      // 
      this.lblRows.AutoSize = true;
      this.lblRows.Location = new System.Drawing.Point(8, 72);
      this.lblRows.Name = "lblRows";
      this.lblRows.Size = new System.Drawing.Size(37, 13);
      this.lblRows.TabIndex = 2;
      this.lblRows.Text = "Rows:";
      // 
      // tbColumnGap
      // 
      this.tbColumnGap.Location = new System.Drawing.Point(168, 140);
      this.tbColumnGap.Name = "tbColumnGap";
      this.tbColumnGap.Size = new System.Drawing.Size(84, 20);
      this.tbColumnGap.TabIndex = 1;
      this.tbColumnGap.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblColumnGap
      // 
      this.lblColumnGap.AutoSize = true;
      this.lblColumnGap.Location = new System.Drawing.Point(8, 144);
      this.lblColumnGap.Name = "lblColumnGap";
      this.lblColumnGap.Size = new System.Drawing.Size(67, 13);
      this.lblColumnGap.TabIndex = 0;
      this.lblColumnGap.Text = "Column gap:";
      // 
      // tbRowGap
      // 
      this.tbRowGap.Location = new System.Drawing.Point(168, 116);
      this.tbRowGap.Name = "tbRowGap";
      this.tbRowGap.Size = new System.Drawing.Size(84, 20);
      this.tbRowGap.TabIndex = 1;
      this.tbRowGap.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblRowGap
      // 
      this.lblRowGap.AutoSize = true;
      this.lblRowGap.Location = new System.Drawing.Point(8, 120);
      this.lblRowGap.Name = "lblRowGap";
      this.lblRowGap.Size = new System.Drawing.Size(53, 13);
      this.lblRowGap.TabIndex = 0;
      this.lblRowGap.Text = "Row gap:";
      // 
      // tbLabelHeight
      // 
      this.tbLabelHeight.Location = new System.Drawing.Point(168, 44);
      this.tbLabelHeight.Name = "tbLabelHeight";
      this.tbLabelHeight.Size = new System.Drawing.Size(84, 20);
      this.tbLabelHeight.TabIndex = 1;
      this.tbLabelHeight.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblLabelHeight
      // 
      this.lblLabelHeight.AutoSize = true;
      this.lblLabelHeight.Location = new System.Drawing.Point(8, 48);
      this.lblLabelHeight.Name = "lblLabelHeight";
      this.lblLabelHeight.Size = new System.Drawing.Size(69, 13);
      this.lblLabelHeight.TabIndex = 0;
      this.lblLabelHeight.Text = "Label height:";
      // 
      // tbLabelWidth
      // 
      this.tbLabelWidth.Location = new System.Drawing.Point(168, 20);
      this.tbLabelWidth.Name = "tbLabelWidth";
      this.tbLabelWidth.Size = new System.Drawing.Size(84, 20);
      this.tbLabelWidth.TabIndex = 1;
      this.tbLabelWidth.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblLabelWidth
      // 
      this.lblLabelWidth.AutoSize = true;
      this.lblLabelWidth.Location = new System.Drawing.Point(8, 24);
      this.lblLabelWidth.Name = "lblLabelWidth";
      this.lblLabelWidth.Size = new System.Drawing.Size(65, 13);
      this.lblLabelWidth.TabIndex = 0;
      this.lblLabelWidth.Text = "Label width:";
      // 
      // lblPaperWidth
      // 
      this.lblPaperWidth.AutoSize = true;
      this.lblPaperWidth.Location = new System.Drawing.Point(8, 76);
      this.lblPaperWidth.Name = "lblPaperWidth";
      this.lblPaperWidth.Size = new System.Drawing.Size(68, 13);
      this.lblPaperWidth.TabIndex = 0;
      this.lblPaperWidth.Text = "Paper width:";
      // 
      // tbPaperWidth
      // 
      this.tbPaperWidth.Location = new System.Drawing.Point(168, 72);
      this.tbPaperWidth.Name = "tbPaperWidth";
      this.tbPaperWidth.Size = new System.Drawing.Size(84, 20);
      this.tbPaperWidth.TabIndex = 1;
      this.tbPaperWidth.TextChanged += new System.EventHandler(this.tbPaperWidth_TextChanged);
      // 
      // lblPaperHeight
      // 
      this.lblPaperHeight.AutoSize = true;
      this.lblPaperHeight.Location = new System.Drawing.Point(8, 100);
      this.lblPaperHeight.Name = "lblPaperHeight";
      this.lblPaperHeight.Size = new System.Drawing.Size(72, 13);
      this.lblPaperHeight.TabIndex = 0;
      this.lblPaperHeight.Text = "Paper height:";
      // 
      // tbPaperHeight
      // 
      this.tbPaperHeight.Location = new System.Drawing.Point(168, 96);
      this.tbPaperHeight.Name = "tbPaperHeight";
      this.tbPaperHeight.Size = new System.Drawing.Size(84, 20);
      this.tbPaperHeight.TabIndex = 1;
      this.tbPaperHeight.TextChanged += new System.EventHandler(this.tbPaperWidth_TextChanged);
      // 
      // lblLeftMargin
      // 
      this.lblLeftMargin.AutoSize = true;
      this.lblLeftMargin.Location = new System.Drawing.Point(8, 124);
      this.lblLeftMargin.Name = "lblLeftMargin";
      this.lblLeftMargin.Size = new System.Drawing.Size(65, 13);
      this.lblLeftMargin.TabIndex = 0;
      this.lblLeftMargin.Text = "Left margin:";
      // 
      // tbLeftMargin
      // 
      this.tbLeftMargin.Location = new System.Drawing.Point(168, 120);
      this.tbLeftMargin.Name = "tbLeftMargin";
      this.tbLeftMargin.Size = new System.Drawing.Size(84, 20);
      this.tbLeftMargin.TabIndex = 1;
      this.tbLeftMargin.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // lblTopMargin
      // 
      this.lblTopMargin.AutoSize = true;
      this.lblTopMargin.Location = new System.Drawing.Point(8, 148);
      this.lblTopMargin.Name = "lblTopMargin";
      this.lblTopMargin.Size = new System.Drawing.Size(64, 13);
      this.lblTopMargin.TabIndex = 0;
      this.lblTopMargin.Text = "Top margin:";
      // 
      // tbTopMargin
      // 
      this.tbTopMargin.Location = new System.Drawing.Point(168, 144);
      this.tbTopMargin.Name = "tbTopMargin";
      this.tbTopMargin.Size = new System.Drawing.Size(84, 20);
      this.tbTopMargin.TabIndex = 1;
      this.tbTopMargin.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // gbPaper
      // 
      this.gbPaper.Controls.Add(this.cbLandscape);
      this.gbPaper.Controls.Add(this.cbxPaper);
      this.gbPaper.Controls.Add(this.tbPaperWidth);
      this.gbPaper.Controls.Add(this.lblPaperWidth);
      this.gbPaper.Controls.Add(this.lblPaperHeight);
      this.gbPaper.Controls.Add(this.tbPaperHeight);
      this.gbPaper.Controls.Add(this.lblLeftMargin);
      this.gbPaper.Controls.Add(this.tbLeftMargin);
      this.gbPaper.Controls.Add(this.lblTopMargin);
      this.gbPaper.Controls.Add(this.tbTopMargin);
      this.gbPaper.Location = new System.Drawing.Point(8, 40);
      this.gbPaper.Name = "gbPaper";
      this.gbPaper.Size = new System.Drawing.Size(264, 176);
      this.gbPaper.TabIndex = 2;
      this.gbPaper.TabStop = false;
      this.gbPaper.Text = "Paper";
      // 
      // cbLandscape
      // 
      this.cbLandscape.AutoSize = true;
      this.cbLandscape.Location = new System.Drawing.Point(12, 48);
      this.cbLandscape.Name = "cbLandscape";
      this.cbLandscape.Size = new System.Drawing.Size(77, 17);
      this.cbLandscape.TabIndex = 4;
      this.cbLandscape.Text = "Landscape";
      this.cbLandscape.UseVisualStyleBackColor = true;
      this.cbLandscape.CheckedChanged += new System.EventHandler(this.cbLandscape_CheckedChanged);
      // 
      // cbxPaper
      // 
      this.cbxPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPaper.FormattingEnabled = true;
      this.cbxPaper.Location = new System.Drawing.Point(12, 20);
      this.cbxPaper.Name = "cbxPaper";
      this.cbxPaper.Size = new System.Drawing.Size(240, 21);
      this.cbxPaper.TabIndex = 3;
      this.cbxPaper.SelectedIndexChanged += new System.EventHandler(this.cbxPaper_SelectedIndexChanged);
      // 
      // gbSample
      // 
      this.gbSample.Controls.Add(this.rcSample);
      this.gbSample.Location = new System.Drawing.Point(280, 40);
      this.gbSample.Name = "gbSample";
      this.gbSample.Size = new System.Drawing.Size(252, 352);
      this.gbSample.TabIndex = 3;
      this.gbSample.TabStop = false;
      this.gbSample.Text = "Sample";
      // 
      // rcSample
      // 
      this.rcSample.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.rcSample.FullPagePreview = true;
      this.rcSample.Location = new System.Drawing.Point(12, 20);
      this.rcSample.Name = "rcSample";
      this.rcSample.Size = new System.Drawing.Size(228, 320);
      this.rcSample.TabIndex = 0;
      this.rcSample.Text = "sampleReportControl1";
      // 
      // lblWarning
      // 
      this.lblWarning.AutoSize = true;
      this.lblWarning.ForeColor = System.Drawing.Color.Red;
      this.lblWarning.Location = new System.Drawing.Point(8, 404);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.Size = new System.Drawing.Size(149, 13);
      this.lblWarning.TabIndex = 4;
      this.lblWarning.Text = "Labels do not fit on the page!";
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(12, 16);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(38, 13);
      this.lblName.TabIndex = 5;
      this.lblName.Text = "Name:";
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(96, 12);
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(436, 20);
      this.tbName.TabIndex = 6;
      this.tbName.TextChanged += new System.EventHandler(this.tbLeftMargin_TextChanged);
      // 
      // CustomLabelForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(540, 438);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.lblWarning);
      this.Controls.Add(this.gbSample);
      this.Controls.Add(this.gbPaper);
      this.Controls.Add(this.gbLabel);
      this.Name = "CustomLabelForm";
      this.Text = "Custom Label";
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbLabel, 0);
      this.Controls.SetChildIndex(this.gbPaper, 0);
      this.Controls.SetChildIndex(this.gbSample, 0);
      this.Controls.SetChildIndex(this.lblWarning, 0);
      this.Controls.SetChildIndex(this.lblName, 0);
      this.Controls.SetChildIndex(this.tbName, 0);
      this.gbLabel.ResumeLayout(false);
      this.gbLabel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udColumns)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udRows)).EndInit();
      this.gbPaper.ResumeLayout(false);
      this.gbPaper.PerformLayout();
      this.gbSample.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbLabel;
    private System.Windows.Forms.Label lblPaperWidth;
    private System.Windows.Forms.NumericUpDown udColumns;
    private System.Windows.Forms.Label lblColumns;
    private System.Windows.Forms.NumericUpDown udRows;
    private System.Windows.Forms.Label lblRows;
    private System.Windows.Forms.TextBox tbColumnGap;
    private System.Windows.Forms.Label lblColumnGap;
    private System.Windows.Forms.TextBox tbRowGap;
    private System.Windows.Forms.Label lblRowGap;
    private System.Windows.Forms.TextBox tbLabelHeight;
    private System.Windows.Forms.Label lblLabelHeight;
    private System.Windows.Forms.TextBox tbLabelWidth;
    private System.Windows.Forms.Label lblLabelWidth;
    private System.Windows.Forms.TextBox tbPaperWidth;
    private System.Windows.Forms.Label lblPaperHeight;
    private System.Windows.Forms.TextBox tbPaperHeight;
    private System.Windows.Forms.Label lblLeftMargin;
    private System.Windows.Forms.TextBox tbLeftMargin;
    private System.Windows.Forms.Label lblTopMargin;
    private System.Windows.Forms.TextBox tbTopMargin;
    private System.Windows.Forms.GroupBox gbPaper;
    private System.Windows.Forms.GroupBox gbSample;
    private FastReport.Controls.SampleReportControl rcSample;
    private System.Windows.Forms.ComboBox cbxPaper;
    private System.Windows.Forms.CheckBox cbLandscape;
    private System.Windows.Forms.Label lblWarning;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox tbName;
  }
}
