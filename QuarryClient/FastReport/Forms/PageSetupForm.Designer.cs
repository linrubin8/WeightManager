namespace FastReport.Forms
{
  partial class PageSetupForm
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
        this.cbMirrorMargins = new System.Windows.Forms.CheckBox();
        this.tbBottom = new System.Windows.Forms.TextBox();
        this.lblBottom = new System.Windows.Forms.Label();
        this.tbRight = new System.Windows.Forms.TextBox();
        this.lblRight = new System.Windows.Forms.Label();
        this.tbTop = new System.Windows.Forms.TextBox();
        this.lblTop = new System.Windows.Forms.Label();
        this.tbLeft = new System.Windows.Forms.TextBox();
        this.lblLeft = new System.Windows.Forms.Label();
        this.cbxOtherPages = new System.Windows.Forms.ComboBox();
        this.cbxFirstPage = new System.Windows.Forms.ComboBox();
        this.lblOtherPages = new System.Windows.Forms.Label();
        this.lblFirstPage = new System.Windows.Forms.Label();
        this.pnOrientation = new System.Windows.Forms.Panel();
        this.rbLandscape = new System.Windows.Forms.RadioButton();
        this.rbPortrait = new System.Windows.Forms.RadioButton();
        this.tbHeight = new System.Windows.Forms.TextBox();
        this.tbWidth = new System.Windows.Forms.TextBox();
        this.lblHeight = new System.Windows.Forms.Label();
        this.lblWidth = new System.Windows.Forms.Label();
        this.cbxPaper = new System.Windows.Forms.ComboBox();
        this.cbExtraWidth = new System.Windows.Forms.CheckBox();
        this.btnEdit = new System.Windows.Forms.Button();
        this.cbxDuplex = new System.Windows.Forms.ComboBox();
        this.lblDuplex = new System.Windows.Forms.Label();
        this.tbColumnWidth = new System.Windows.Forms.TextBox();
        this.lblColumnWidth = new System.Windows.Forms.Label();
        this.tbPositions = new System.Windows.Forms.TextBox();
        this.lblPositions = new System.Windows.Forms.Label();
        this.udCount = new System.Windows.Forms.NumericUpDown();
        this.lblCount = new System.Windows.Forms.Label();
        this.pcPages = new FastReport.Controls.PageControl();
        this.pnPaper = new FastReport.Controls.PageControlPage();
        this.gbOrientation = new System.Windows.Forms.GroupBox();
        this.pnMargins = new FastReport.Controls.PageControlPage();
        this.pnSource = new FastReport.Controls.PageControlPage();
        this.pnColumns = new FastReport.Controls.PageControlPage();
        this.pnOther = new FastReport.Controls.PageControlPage();
        this.cbUnlimitedWidth = new System.Windows.Forms.CheckBox();
        this.cbUnlimitedHeight = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.udCount)).BeginInit();
        this.pcPages.SuspendLayout();
        this.pnPaper.SuspendLayout();
        this.gbOrientation.SuspendLayout();
        this.pnMargins.SuspendLayout();
        this.pnSource.SuspendLayout();
        this.pnColumns.SuspendLayout();
        this.pnOther.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnOk
        // 
        this.btnOk.Location = new System.Drawing.Point(196, 224);
        // 
        // btnCancel
        // 
        this.btnCancel.Location = new System.Drawing.Point(276, 224);
        // 
        // cbMirrorMargins
        // 
        this.cbMirrorMargins.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
        this.cbMirrorMargins.Location = new System.Drawing.Point(16, 136);
        this.cbMirrorMargins.Name = "cbMirrorMargins";
        this.cbMirrorMargins.Size = new System.Drawing.Size(180, 44);
        this.cbMirrorMargins.TabIndex = 8;
        this.cbMirrorMargins.Text = "Mirror margins on even pages";
        this.cbMirrorMargins.TextAlign = System.Drawing.ContentAlignment.TopLeft;
        this.cbMirrorMargins.UseVisualStyleBackColor = true;
        // 
        // tbBottom
        // 
        this.tbBottom.Location = new System.Drawing.Point(116, 88);
        this.tbBottom.Name = "tbBottom";
        this.tbBottom.Size = new System.Drawing.Size(80, 20);
        this.tbBottom.TabIndex = 7;
        this.tbBottom.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
        // 
        // lblBottom
        // 
        this.lblBottom.AutoSize = true;
        this.lblBottom.Location = new System.Drawing.Point(16, 92);
        this.lblBottom.Name = "lblBottom";
        this.lblBottom.Size = new System.Drawing.Size(41, 13);
        this.lblBottom.TabIndex = 6;
        this.lblBottom.Text = "Bottom";
        // 
        // tbRight
        // 
        this.tbRight.Location = new System.Drawing.Point(116, 64);
        this.tbRight.Name = "tbRight";
        this.tbRight.Size = new System.Drawing.Size(80, 20);
        this.tbRight.TabIndex = 5;
        this.tbRight.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
        // 
        // lblRight
        // 
        this.lblRight.AutoSize = true;
        this.lblRight.Location = new System.Drawing.Point(16, 68);
        this.lblRight.Name = "lblRight";
        this.lblRight.Size = new System.Drawing.Size(32, 13);
        this.lblRight.TabIndex = 4;
        this.lblRight.Text = "Right";
        // 
        // tbTop
        // 
        this.tbTop.Location = new System.Drawing.Point(116, 40);
        this.tbTop.Name = "tbTop";
        this.tbTop.Size = new System.Drawing.Size(80, 20);
        this.tbTop.TabIndex = 3;
        this.tbTop.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
        // 
        // lblTop
        // 
        this.lblTop.AutoSize = true;
        this.lblTop.Location = new System.Drawing.Point(16, 44);
        this.lblTop.Name = "lblTop";
        this.lblTop.Size = new System.Drawing.Size(25, 13);
        this.lblTop.TabIndex = 2;
        this.lblTop.Text = "Top";
        // 
        // tbLeft
        // 
        this.tbLeft.Location = new System.Drawing.Point(116, 16);
        this.tbLeft.Name = "tbLeft";
        this.tbLeft.Size = new System.Drawing.Size(80, 20);
        this.tbLeft.TabIndex = 1;
        this.tbLeft.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
        // 
        // lblLeft
        // 
        this.lblLeft.AutoSize = true;
        this.lblLeft.Location = new System.Drawing.Point(16, 20);
        this.lblLeft.Name = "lblLeft";
        this.lblLeft.Size = new System.Drawing.Size(26, 13);
        this.lblLeft.TabIndex = 0;
        this.lblLeft.Text = "Left";
        // 
        // cbxOtherPages
        // 
        this.cbxOtherPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbxOtherPages.FormattingEnabled = true;
        this.cbxOtherPages.Location = new System.Drawing.Point(16, 88);
        this.cbxOtherPages.Name = "cbxOtherPages";
        this.cbxOtherPages.Size = new System.Drawing.Size(180, 21);
        this.cbxOtherPages.TabIndex = 2;
        // 
        // cbxFirstPage
        // 
        this.cbxFirstPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbxFirstPage.FormattingEnabled = true;
        this.cbxFirstPage.Location = new System.Drawing.Point(16, 36);
        this.cbxFirstPage.Name = "cbxFirstPage";
        this.cbxFirstPage.Size = new System.Drawing.Size(180, 21);
        this.cbxFirstPage.TabIndex = 2;
        // 
        // lblOtherPages
        // 
        this.lblOtherPages.AutoSize = true;
        this.lblOtherPages.Location = new System.Drawing.Point(16, 68);
        this.lblOtherPages.Name = "lblOtherPages";
        this.lblOtherPages.Size = new System.Drawing.Size(67, 13);
        this.lblOtherPages.TabIndex = 1;
        this.lblOtherPages.Text = "Other pages";
        // 
        // lblFirstPage
        // 
        this.lblFirstPage.AutoSize = true;
        this.lblFirstPage.Location = new System.Drawing.Point(16, 16);
        this.lblFirstPage.Name = "lblFirstPage";
        this.lblFirstPage.Size = new System.Drawing.Size(55, 13);
        this.lblFirstPage.TabIndex = 0;
        this.lblFirstPage.Text = "First page";
        // 
        // pnOrientation
        // 
        this.pnOrientation.Location = new System.Drawing.Point(128, 16);
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
        // tbHeight
        // 
        this.tbHeight.Location = new System.Drawing.Point(116, 68);
        this.tbHeight.Name = "tbHeight";
        this.tbHeight.Size = new System.Drawing.Size(80, 20);
        this.tbHeight.TabIndex = 4;
        this.tbHeight.TextChanged += new System.EventHandler(this.tbHeight_TextChanged);
        // 
        // tbWidth
        // 
        this.tbWidth.Location = new System.Drawing.Point(116, 44);
        this.tbWidth.Name = "tbWidth";
        this.tbWidth.Size = new System.Drawing.Size(80, 20);
        this.tbWidth.TabIndex = 3;
        this.tbWidth.TextChanged += new System.EventHandler(this.tbHeight_TextChanged);
        // 
        // lblHeight
        // 
        this.lblHeight.AutoSize = true;
        this.lblHeight.Location = new System.Drawing.Point(16, 72);
        this.lblHeight.Name = "lblHeight";
        this.lblHeight.Size = new System.Drawing.Size(38, 13);
        this.lblHeight.TabIndex = 2;
        this.lblHeight.Text = "Height";
        // 
        // lblWidth
        // 
        this.lblWidth.AutoSize = true;
        this.lblWidth.Location = new System.Drawing.Point(16, 48);
        this.lblWidth.Name = "lblWidth";
        this.lblWidth.Size = new System.Drawing.Size(35, 13);
        this.lblWidth.TabIndex = 1;
        this.lblWidth.Text = "Width";
        // 
        // cbxPaper
        // 
        this.cbxPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbxPaper.FormattingEnabled = true;
        this.cbxPaper.Location = new System.Drawing.Point(16, 16);
        this.cbxPaper.Name = "cbxPaper";
        this.cbxPaper.Size = new System.Drawing.Size(180, 21);
        this.cbxPaper.TabIndex = 0;
        this.cbxPaper.SelectedIndexChanged += new System.EventHandler(this.cbxPaper_SelectedIndexChanged);
        // 
        // cbExtraWidth
        // 
        this.cbExtraWidth.AutoSize = true;
        this.cbExtraWidth.Location = new System.Drawing.Point(16, 122);
        this.cbExtraWidth.Name = "cbExtraWidth";
        this.cbExtraWidth.Size = new System.Drawing.Size(115, 17);
        this.cbExtraWidth.TabIndex = 4;
        this.cbExtraWidth.Text = "Extra design width";
        this.cbExtraWidth.UseVisualStyleBackColor = true;
        // 
        // btnEdit
        // 
        this.btnEdit.Location = new System.Drawing.Point(16, 68);
        this.btnEdit.Name = "btnEdit";
        this.btnEdit.Size = new System.Drawing.Size(179, 23);
        this.btnEdit.TabIndex = 3;
        this.btnEdit.Text = "Edit...";
        this.btnEdit.UseVisualStyleBackColor = true;
        this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
        // 
        // cbxDuplex
        // 
        this.cbxDuplex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbxDuplex.FormattingEnabled = true;
        this.cbxDuplex.Location = new System.Drawing.Point(16, 36);
        this.cbxDuplex.Name = "cbxDuplex";
        this.cbxDuplex.Size = new System.Drawing.Size(180, 21);
        this.cbxDuplex.TabIndex = 1;
        // 
        // lblDuplex
        // 
        this.lblDuplex.AutoSize = true;
        this.lblDuplex.Location = new System.Drawing.Point(16, 16);
        this.lblDuplex.Name = "lblDuplex";
        this.lblDuplex.Size = new System.Drawing.Size(40, 13);
        this.lblDuplex.TabIndex = 0;
        this.lblDuplex.Text = "Duplex";
        // 
        // tbColumnWidth
        // 
        this.tbColumnWidth.Location = new System.Drawing.Point(116, 40);
        this.tbColumnWidth.Name = "tbColumnWidth";
        this.tbColumnWidth.Size = new System.Drawing.Size(80, 20);
        this.tbColumnWidth.TabIndex = 5;
        // 
        // lblColumnWidth
        // 
        this.lblColumnWidth.AutoSize = true;
        this.lblColumnWidth.Location = new System.Drawing.Point(16, 44);
        this.lblColumnWidth.Name = "lblColumnWidth";
        this.lblColumnWidth.Size = new System.Drawing.Size(35, 13);
        this.lblColumnWidth.TabIndex = 4;
        this.lblColumnWidth.Text = "Width";
        // 
        // tbPositions
        // 
        this.tbPositions.AcceptsReturn = true;
        this.tbPositions.Location = new System.Drawing.Point(116, 64);
        this.tbPositions.Multiline = true;
        this.tbPositions.Name = "tbPositions";
        this.tbPositions.Size = new System.Drawing.Size(80, 116);
        this.tbPositions.TabIndex = 3;
        // 
        // lblPositions
        // 
        this.lblPositions.AutoSize = true;
        this.lblPositions.Location = new System.Drawing.Point(16, 68);
        this.lblPositions.Name = "lblPositions";
        this.lblPositions.Size = new System.Drawing.Size(49, 13);
        this.lblPositions.TabIndex = 2;
        this.lblPositions.Text = "Positions";
        // 
        // udCount
        // 
        this.udCount.Location = new System.Drawing.Point(116, 16);
        this.udCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.udCount.Name = "udCount";
        this.udCount.Size = new System.Drawing.Size(80, 20);
        this.udCount.TabIndex = 1;
        this.udCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.udCount.ValueChanged += new System.EventHandler(this.udCount_ValueChanged);
        // 
        // lblCount
        // 
        this.lblCount.AutoSize = true;
        this.lblCount.Location = new System.Drawing.Point(16, 20);
        this.lblCount.Name = "lblCount";
        this.lblCount.Size = new System.Drawing.Size(36, 13);
        this.lblCount.TabIndex = 0;
        this.lblCount.Text = "Count";
        // 
        // pcPages
        // 
        this.pcPages.Controls.Add(this.pnPaper);
        this.pcPages.Controls.Add(this.pnMargins);
        this.pcPages.Controls.Add(this.pnSource);
        this.pcPages.Controls.Add(this.pnColumns);
        this.pcPages.Controls.Add(this.pnOther);
        this.pcPages.HighlightPageIndex = -1;
        this.pcPages.Location = new System.Drawing.Point(12, 12);
        this.pcPages.Name = "pcPages";
        this.pcPages.SelectorWidth = 127;
        this.pcPages.Size = new System.Drawing.Size(340, 200);
        this.pcPages.TabIndex = 4;
        this.pcPages.Text = "pageControl1";
        // 
        // pnPaper
        // 
        this.pnPaper.BackColor = System.Drawing.SystemColors.Window;
        this.pnPaper.Controls.Add(this.gbOrientation);
        this.pnPaper.Controls.Add(this.tbHeight);
        this.pnPaper.Controls.Add(this.tbWidth);
        this.pnPaper.Controls.Add(this.lblHeight);
        this.pnPaper.Controls.Add(this.cbxPaper);
        this.pnPaper.Controls.Add(this.lblWidth);
        this.pnPaper.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnPaper.Location = new System.Drawing.Point(127, 1);
        this.pnPaper.Name = "pnPaper";
        this.pnPaper.Size = new System.Drawing.Size(212, 198);
        this.pnPaper.TabIndex = 0;
        this.pnPaper.Text = "Paper";
        // 
        // gbOrientation
        // 
        this.gbOrientation.Controls.Add(this.rbPortrait);
        this.gbOrientation.Controls.Add(this.pnOrientation);
        this.gbOrientation.Controls.Add(this.rbLandscape);
        this.gbOrientation.Location = new System.Drawing.Point(16, 110);
        this.gbOrientation.Name = "gbOrientation";
        this.gbOrientation.Size = new System.Drawing.Size(180, 72);
        this.gbOrientation.TabIndex = 5;
        this.gbOrientation.TabStop = false;
        this.gbOrientation.Text = "Orientation";
        // 
        // pnMargins
        // 
        this.pnMargins.BackColor = System.Drawing.SystemColors.Window;
        this.pnMargins.Controls.Add(this.cbMirrorMargins);
        this.pnMargins.Controls.Add(this.tbBottom);
        this.pnMargins.Controls.Add(this.lblLeft);
        this.pnMargins.Controls.Add(this.lblBottom);
        this.pnMargins.Controls.Add(this.tbLeft);
        this.pnMargins.Controls.Add(this.tbRight);
        this.pnMargins.Controls.Add(this.lblTop);
        this.pnMargins.Controls.Add(this.lblRight);
        this.pnMargins.Controls.Add(this.tbTop);
        this.pnMargins.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnMargins.Location = new System.Drawing.Point(127, 1);
        this.pnMargins.Name = "pnMargins";
        this.pnMargins.Size = new System.Drawing.Size(212, 198);
        this.pnMargins.TabIndex = 1;
        this.pnMargins.Text = "Margins";
        // 
        // pnSource
        // 
        this.pnSource.BackColor = System.Drawing.SystemColors.Window;
        this.pnSource.Controls.Add(this.cbxOtherPages);
        this.pnSource.Controls.Add(this.cbxFirstPage);
        this.pnSource.Controls.Add(this.lblOtherPages);
        this.pnSource.Controls.Add(this.lblFirstPage);
        this.pnSource.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnSource.Location = new System.Drawing.Point(127, 1);
        this.pnSource.Name = "pnSource";
        this.pnSource.Size = new System.Drawing.Size(212, 198);
        this.pnSource.TabIndex = 2;
        this.pnSource.Text = "Source";
        // 
        // pnColumns
        // 
        this.pnColumns.BackColor = System.Drawing.SystemColors.Window;
        this.pnColumns.Controls.Add(this.tbColumnWidth);
        this.pnColumns.Controls.Add(this.lblColumnWidth);
        this.pnColumns.Controls.Add(this.lblCount);
        this.pnColumns.Controls.Add(this.tbPositions);
        this.pnColumns.Controls.Add(this.udCount);
        this.pnColumns.Controls.Add(this.lblPositions);
        this.pnColumns.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnColumns.Location = new System.Drawing.Point(127, 1);
        this.pnColumns.Name = "pnColumns";
        this.pnColumns.Size = new System.Drawing.Size(212, 198);
        this.pnColumns.TabIndex = 3;
        this.pnColumns.Text = "Columns";
        // 
        // pnOther
        // 
        this.pnOther.BackColor = System.Drawing.SystemColors.Window;
        this.pnOther.Controls.Add(this.cbUnlimitedHeight);
        this.pnOther.Controls.Add(this.cbUnlimitedWidth);
        this.pnOther.Controls.Add(this.cbExtraWidth);
        this.pnOther.Controls.Add(this.btnEdit);
        this.pnOther.Controls.Add(this.lblDuplex);
        this.pnOther.Controls.Add(this.cbxDuplex);
        this.pnOther.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnOther.Location = new System.Drawing.Point(127, 1);
        this.pnOther.Name = "pnOther";
        this.pnOther.Size = new System.Drawing.Size(212, 198);
        this.pnOther.TabIndex = 4;
        this.pnOther.Text = "Other";
        // 
        // cbUnlimitedWidth
        // 
        this.cbUnlimitedWidth.AutoSize = true;
        this.cbUnlimitedWidth.Location = new System.Drawing.Point(16, 168);
        this.cbUnlimitedWidth.Name = "cbUnlimitedWidth";
        this.cbUnlimitedWidth.Size = new System.Drawing.Size(98, 17);
        this.cbUnlimitedWidth.TabIndex = 6;
        this.cbUnlimitedWidth.Text = "Unlimited width";
        this.cbUnlimitedWidth.UseVisualStyleBackColor = true;
        // 
        // cbUnlimitedHeight
        // 
        this.cbUnlimitedHeight.AutoSize = true;
        this.cbUnlimitedHeight.Location = new System.Drawing.Point(16, 145);
        this.cbUnlimitedHeight.Name = "cbUnlimitedHeight";
        this.cbUnlimitedHeight.Size = new System.Drawing.Size(102, 17);
        this.cbUnlimitedHeight.TabIndex = 7;
        this.cbUnlimitedHeight.Text = "Unlimited height";
        this.cbUnlimitedHeight.UseVisualStyleBackColor = true;
        // 
        // PageSetupForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(363, 258);
        this.Controls.Add(this.pcPages);
        this.Name = "PageSetupForm";
        this.Text = "PageSettingsForm";
        this.Shown += new System.EventHandler(this.PageSetupForm_Shown);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PageSetupForm_FormClosed);
        this.Controls.SetChildIndex(this.btnOk, 0);
        this.Controls.SetChildIndex(this.btnCancel, 0);
        this.Controls.SetChildIndex(this.pcPages, 0);
        ((System.ComponentModel.ISupportInitialize)(this.udCount)).EndInit();
        this.pcPages.ResumeLayout(false);
        this.pnPaper.ResumeLayout(false);
        this.pnPaper.PerformLayout();
        this.gbOrientation.ResumeLayout(false);
        this.gbOrientation.PerformLayout();
        this.pnMargins.ResumeLayout(false);
        this.pnMargins.PerformLayout();
        this.pnSource.ResumeLayout(false);
        this.pnSource.PerformLayout();
        this.pnColumns.ResumeLayout(false);
        this.pnColumns.PerformLayout();
        this.pnOther.ResumeLayout(false);
        this.pnOther.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox tbBottom;
    private System.Windows.Forms.Label lblBottom;
    private System.Windows.Forms.TextBox tbRight;
    private System.Windows.Forms.Label lblRight;
    private System.Windows.Forms.TextBox tbTop;
    private System.Windows.Forms.Label lblTop;
    private System.Windows.Forms.TextBox tbLeft;
    private System.Windows.Forms.Label lblLeft;
    private System.Windows.Forms.RadioButton rbLandscape;
    private System.Windows.Forms.RadioButton rbPortrait;
    private System.Windows.Forms.TextBox tbHeight;
    private System.Windows.Forms.TextBox tbWidth;
    private System.Windows.Forms.Label lblHeight;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.ComboBox cbxPaper;
    private System.Windows.Forms.Label lblPositions;
    private System.Windows.Forms.NumericUpDown udCount;
    private System.Windows.Forms.Label lblCount;
    private System.Windows.Forms.TextBox tbPositions;
    private System.Windows.Forms.CheckBox cbMirrorMargins;
    private System.Windows.Forms.ComboBox cbxOtherPages;
    private System.Windows.Forms.ComboBox cbxFirstPage;
    private System.Windows.Forms.Label lblOtherPages;
    private System.Windows.Forms.Label lblFirstPage;
    private System.Windows.Forms.ComboBox cbxDuplex;
    private System.Windows.Forms.Label lblDuplex;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.TextBox tbColumnWidth;
    private System.Windows.Forms.Label lblColumnWidth;
    private FastReport.Controls.PageControl pcPages;
    private FastReport.Controls.PageControlPage pnPaper;
    private FastReport.Controls.PageControlPage pnMargins;
    private FastReport.Controls.PageControlPage pnSource;
    private FastReport.Controls.PageControlPage pnColumns;
    private FastReport.Controls.PageControlPage pnOther;
    private System.Windows.Forms.Panel pnOrientation;
    private System.Windows.Forms.CheckBox cbExtraWidth;
    private System.Windows.Forms.GroupBox gbOrientation;
    private System.Windows.Forms.CheckBox cbUnlimitedWidth;
    private System.Windows.Forms.CheckBox cbUnlimitedHeight;
  }
}
