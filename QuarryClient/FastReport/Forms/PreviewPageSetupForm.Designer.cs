namespace FastReport.Forms
{
  partial class PreviewPageSetupForm
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
      this.gbPaper = new System.Windows.Forms.GroupBox();
      this.tbHeight = new System.Windows.Forms.TextBox();
      this.tbWidth = new System.Windows.Forms.TextBox();
      this.lblHeight = new System.Windows.Forms.Label();
      this.lblWidth = new System.Windows.Forms.Label();
      this.cbxPaper = new System.Windows.Forms.ComboBox();
      this.gbOrientation = new System.Windows.Forms.GroupBox();
      this.pnOrientation = new System.Windows.Forms.Panel();
      this.rbLandscape = new System.Windows.Forms.RadioButton();
      this.rbPortrait = new System.Windows.Forms.RadioButton();
      this.gbMargins = new System.Windows.Forms.GroupBox();
      this.tbBottom = new System.Windows.Forms.TextBox();
      this.lblBottom = new System.Windows.Forms.Label();
      this.tbRight = new System.Windows.Forms.TextBox();
      this.lblRight = new System.Windows.Forms.Label();
      this.tbTop = new System.Windows.Forms.TextBox();
      this.lblTop = new System.Windows.Forms.Label();
      this.tbLeft = new System.Windows.Forms.TextBox();
      this.lblLeft = new System.Windows.Forms.Label();
      this.cbApply = new System.Windows.Forms.CheckBox();
      this.gbPaper.SuspendLayout();
      this.gbOrientation.SuspendLayout();
      this.gbMargins.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(156, 300);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(236, 300);
      // 
      // gbPaper
      // 
      this.gbPaper.Controls.Add(this.tbHeight);
      this.gbPaper.Controls.Add(this.tbWidth);
      this.gbPaper.Controls.Add(this.lblHeight);
      this.gbPaper.Controls.Add(this.lblWidth);
      this.gbPaper.Controls.Add(this.cbxPaper);
      this.gbPaper.Location = new System.Drawing.Point(8, 4);
      this.gbPaper.Name = "gbPaper";
      this.gbPaper.Size = new System.Drawing.Size(304, 104);
      this.gbPaper.TabIndex = 7;
      this.gbPaper.TabStop = false;
      this.gbPaper.Text = "Paper";
      // 
      // tbHeight
      // 
      this.tbHeight.Location = new System.Drawing.Point(80, 72);
      this.tbHeight.Name = "tbHeight";
      this.tbHeight.Size = new System.Drawing.Size(60, 20);
      this.tbHeight.TabIndex = 4;
      this.tbHeight.TextChanged += new System.EventHandler(this.tbWidth_TextChanged);
      // 
      // tbWidth
      // 
      this.tbWidth.Location = new System.Drawing.Point(80, 48);
      this.tbWidth.Name = "tbWidth";
      this.tbWidth.Size = new System.Drawing.Size(60, 20);
      this.tbWidth.TabIndex = 3;
      this.tbWidth.TextChanged += new System.EventHandler(this.tbWidth_TextChanged);
      // 
      // lblHeight
      // 
      this.lblHeight.AutoSize = true;
      this.lblHeight.Location = new System.Drawing.Point(8, 76);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(38, 13);
      this.lblHeight.TabIndex = 2;
      this.lblHeight.Text = "Height";
      // 
      // lblWidth
      // 
      this.lblWidth.AutoSize = true;
      this.lblWidth.Location = new System.Drawing.Point(8, 52);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(35, 13);
      this.lblWidth.TabIndex = 1;
      this.lblWidth.Text = "Width";
      // 
      // cbxPaper
      // 
      this.cbxPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPaper.FormattingEnabled = true;
      this.cbxPaper.Location = new System.Drawing.Point(12, 20);
      this.cbxPaper.Name = "cbxPaper";
      this.cbxPaper.Size = new System.Drawing.Size(280, 21);
      this.cbxPaper.TabIndex = 0;
      this.cbxPaper.SelectedIndexChanged += new System.EventHandler(this.cbxPaper_SelectedIndexChanged);
      // 
      // gbOrientation
      // 
      this.gbOrientation.Controls.Add(this.pnOrientation);
      this.gbOrientation.Controls.Add(this.rbLandscape);
      this.gbOrientation.Controls.Add(this.rbPortrait);
      this.gbOrientation.Location = new System.Drawing.Point(8, 112);
      this.gbOrientation.Name = "gbOrientation";
      this.gbOrientation.Size = new System.Drawing.Size(304, 72);
      this.gbOrientation.TabIndex = 8;
      this.gbOrientation.TabStop = false;
      this.gbOrientation.Text = "Orientation";
      // 
      // pnOrientation
      // 
      this.pnOrientation.Location = new System.Drawing.Point(232, 16);
      this.pnOrientation.Name = "pnOrientation";
      this.pnOrientation.Size = new System.Drawing.Size(48, 48);
      this.pnOrientation.TabIndex = 1;
      this.pnOrientation.Paint += new System.Windows.Forms.PaintEventHandler(this.pnOrientation_Paint);
      // 
      // rbLandscape
      // 
      this.rbLandscape.AutoSize = true;
      this.rbLandscape.Location = new System.Drawing.Point(12, 44);
      this.rbLandscape.Name = "rbLandscape";
      this.rbLandscape.Size = new System.Drawing.Size(76, 17);
      this.rbLandscape.TabIndex = 0;
      this.rbLandscape.TabStop = true;
      this.rbLandscape.Text = "Landscape";
      this.rbLandscape.UseVisualStyleBackColor = true;
      this.rbLandscape.CheckedChanged += new System.EventHandler(this.rbPortrait_CheckedChanged);
      // 
      // rbPortrait
      // 
      this.rbPortrait.AutoSize = true;
      this.rbPortrait.Location = new System.Drawing.Point(12, 20);
      this.rbPortrait.Name = "rbPortrait";
      this.rbPortrait.Size = new System.Drawing.Size(61, 17);
      this.rbPortrait.TabIndex = 0;
      this.rbPortrait.TabStop = true;
      this.rbPortrait.Text = "Portrait";
      this.rbPortrait.UseVisualStyleBackColor = true;
      this.rbPortrait.CheckedChanged += new System.EventHandler(this.rbPortrait_CheckedChanged);
      // 
      // gbMargins
      // 
      this.gbMargins.Controls.Add(this.tbBottom);
      this.gbMargins.Controls.Add(this.lblBottom);
      this.gbMargins.Controls.Add(this.tbRight);
      this.gbMargins.Controls.Add(this.lblRight);
      this.gbMargins.Controls.Add(this.tbTop);
      this.gbMargins.Controls.Add(this.lblTop);
      this.gbMargins.Controls.Add(this.tbLeft);
      this.gbMargins.Controls.Add(this.lblLeft);
      this.gbMargins.Location = new System.Drawing.Point(8, 188);
      this.gbMargins.Name = "gbMargins";
      this.gbMargins.Size = new System.Drawing.Size(304, 76);
      this.gbMargins.TabIndex = 9;
      this.gbMargins.TabStop = false;
      this.gbMargins.Text = "Margins";
      // 
      // tbBottom
      // 
      this.tbBottom.Location = new System.Drawing.Point(232, 44);
      this.tbBottom.Name = "tbBottom";
      this.tbBottom.Size = new System.Drawing.Size(60, 20);
      this.tbBottom.TabIndex = 7;
      // 
      // lblBottom
      // 
      this.lblBottom.AutoSize = true;
      this.lblBottom.Location = new System.Drawing.Point(160, 48);
      this.lblBottom.Name = "lblBottom";
      this.lblBottom.Size = new System.Drawing.Size(41, 13);
      this.lblBottom.TabIndex = 6;
      this.lblBottom.Text = "Bottom";
      // 
      // tbRight
      // 
      this.tbRight.Location = new System.Drawing.Point(232, 20);
      this.tbRight.Name = "tbRight";
      this.tbRight.Size = new System.Drawing.Size(60, 20);
      this.tbRight.TabIndex = 5;
      // 
      // lblRight
      // 
      this.lblRight.AutoSize = true;
      this.lblRight.Location = new System.Drawing.Point(160, 24);
      this.lblRight.Name = "lblRight";
      this.lblRight.Size = new System.Drawing.Size(32, 13);
      this.lblRight.TabIndex = 4;
      this.lblRight.Text = "Right";
      // 
      // tbTop
      // 
      this.tbTop.Location = new System.Drawing.Point(80, 44);
      this.tbTop.Name = "tbTop";
      this.tbTop.Size = new System.Drawing.Size(60, 20);
      this.tbTop.TabIndex = 3;
      // 
      // lblTop
      // 
      this.lblTop.AutoSize = true;
      this.lblTop.Location = new System.Drawing.Point(8, 48);
      this.lblTop.Name = "lblTop";
      this.lblTop.Size = new System.Drawing.Size(25, 13);
      this.lblTop.TabIndex = 2;
      this.lblTop.Text = "Top";
      // 
      // tbLeft
      // 
      this.tbLeft.Location = new System.Drawing.Point(80, 20);
      this.tbLeft.Name = "tbLeft";
      this.tbLeft.Size = new System.Drawing.Size(60, 20);
      this.tbLeft.TabIndex = 1;
      // 
      // lblLeft
      // 
      this.lblLeft.AutoSize = true;
      this.lblLeft.Location = new System.Drawing.Point(8, 24);
      this.lblLeft.Name = "lblLeft";
      this.lblLeft.Size = new System.Drawing.Size(26, 13);
      this.lblLeft.TabIndex = 0;
      this.lblLeft.Text = "Left";
      // 
      // cbApply
      // 
      this.cbApply.AutoSize = true;
      this.cbApply.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cbApply.Checked = true;
      this.cbApply.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbApply.Location = new System.Drawing.Point(8, 272);
      this.cbApply.Name = "cbApply";
      this.cbApply.Size = new System.Drawing.Size(200, 17);
      this.cbApply.TabIndex = 10;
      this.cbApply.Text = "Apply to all pages and rebuild report";
      this.cbApply.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.cbApply.UseVisualStyleBackColor = true;
      // 
      // PreviewPageSetupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(320, 332);
      this.Controls.Add(this.cbApply);
      this.Controls.Add(this.gbMargins);
      this.Controls.Add(this.gbPaper);
      this.Controls.Add(this.gbOrientation);
      this.Name = "PreviewPageSetupForm";
      this.Text = "Page Setup";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewPageSetupForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbOrientation, 0);
      this.Controls.SetChildIndex(this.gbPaper, 0);
      this.Controls.SetChildIndex(this.gbMargins, 0);
      this.Controls.SetChildIndex(this.cbApply, 0);
      this.gbPaper.ResumeLayout(false);
      this.gbPaper.PerformLayout();
      this.gbOrientation.ResumeLayout(false);
      this.gbOrientation.PerformLayout();
      this.gbMargins.ResumeLayout(false);
      this.gbMargins.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbPaper;
    private System.Windows.Forms.TextBox tbHeight;
    private System.Windows.Forms.TextBox tbWidth;
    private System.Windows.Forms.Label lblHeight;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.ComboBox cbxPaper;
    private System.Windows.Forms.GroupBox gbOrientation;
    private System.Windows.Forms.Panel pnOrientation;
    private System.Windows.Forms.RadioButton rbLandscape;
    private System.Windows.Forms.RadioButton rbPortrait;
    private System.Windows.Forms.GroupBox gbMargins;
    private System.Windows.Forms.TextBox tbBottom;
    private System.Windows.Forms.Label lblBottom;
    private System.Windows.Forms.TextBox tbRight;
    private System.Windows.Forms.Label lblRight;
    private System.Windows.Forms.TextBox tbTop;
    private System.Windows.Forms.Label lblTop;
    private System.Windows.Forms.TextBox tbLeft;
    private System.Windows.Forms.Label lblLeft;
    private System.Windows.Forms.CheckBox cbApply;
  }
}
