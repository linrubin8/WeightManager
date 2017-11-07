namespace FastReport.Forms
{
    partial class TextExportForm
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextExportForm));
          this.gbOptions = new System.Windows.Forms.GroupBox();
          this.lblFrames = new System.Windows.Forms.Label();
          this.cbbFrames = new System.Windows.Forms.ComboBox();
          this.cbDataOnly = new System.Windows.Forms.CheckBox();
          this.cbbCodepage = new System.Windows.Forms.ComboBox();
          this.lblCodepage = new System.Windows.Forms.Label();
          this.cbEmptyLines = new System.Windows.Forms.CheckBox();
          this.cbPageBreaks = new System.Windows.Forms.CheckBox();
          this.lblLoss = new System.Windows.Forms.Label();
          this.tbPreview = new System.Windows.Forms.TextBox();
          this.Status = new System.Windows.Forms.StatusStrip();
          this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
          this.lblPageWidth = new System.Windows.Forms.ToolStripStatusLabel();
          this.lblPageWidthValue = new System.Windows.Forms.ToolStripStatusLabel();
          this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
          this.lblPageHeight = new System.Windows.Forms.ToolStripStatusLabel();
          this.lblPageHeightValue = new System.Windows.Forms.ToolStripStatusLabel();
          this.toolStrip = new System.Windows.Forms.ToolStrip();
          this.btnPrint = new System.Windows.Forms.ToolStripButton();
          this.btnSave = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
          this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
          this.cbFontSize = new System.Windows.Forms.ToolStripComboBox();
          this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
          this.btnFirst = new System.Windows.Forms.ToolStripButton();
          this.btnPrior = new System.Windows.Forms.ToolStripButton();
          this.tbPage = new System.Windows.Forms.ToolStripTextBox();
          this.lblTotalPages = new System.Windows.Forms.ToolStripLabel();
          this.btnNext = new System.Windows.Forms.ToolStripButton();
          this.btnLast = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
          this.btnClose = new System.Windows.Forms.ToolStripButton();
          this.picPerforation = new System.Windows.Forms.PictureBox();
          this.gbScale = new System.Windows.Forms.GroupBox();
          this.udX = new System.Windows.Forms.NumericUpDown();
          this.lblX = new System.Windows.Forms.Label();
          this.btnCalculate = new System.Windows.Forms.Button();
          this.udY = new System.Windows.Forms.NumericUpDown();
          this.lblY = new System.Windows.Forms.Label();
          this.gbPageRange.SuspendLayout();
          this.pcPages.SuspendLayout();
          this.panPages.SuspendLayout();
          this.gbOptions.SuspendLayout();
          this.Status.SuspendLayout();
          this.toolStrip.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.picPerforation)).BeginInit();
          this.gbScale.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.udX)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.udY)).BeginInit();
          this.SuspendLayout();
          // 
          // gbPageRange
          // 
          this.gbPageRange.Location = new System.Drawing.Point(8, 328);
          this.gbPageRange.Visible = false;
          // 
          // pcPages
          // 
          this.pcPages.Location = new System.Drawing.Point(4, 28);
          this.pcPages.Size = new System.Drawing.Size(276, 324);
          this.pcPages.Text = "";
          // 
          // panPages
          // 
          this.panPages.Controls.Add(this.gbScale);
          this.panPages.Controls.Add(this.gbOptions);
          this.panPages.Size = new System.Drawing.Size(276, 324);
          this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
          this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
          this.panPages.Controls.SetChildIndex(this.gbScale, 0);
          // 
          // cbOpenAfter
          // 
          this.cbOpenAfter.Location = new System.Drawing.Point(8, 360);
          this.cbOpenAfter.TabIndex = 15;
          this.cbOpenAfter.Visible = false;
          // 
          // btnOk
          // 
          this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.btnOk.Enabled = false;
          this.btnOk.Location = new System.Drawing.Point(116, 448);
          this.btnOk.Visible = false;
          // 
          // btnCancel
          // 
          this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.btnCancel.Enabled = false;
          this.btnCancel.Location = new System.Drawing.Point(200, 448);
          this.btnCancel.TabIndex = 1;
          this.btnCancel.Visible = false;
          // 
          // gbOptions
          // 
          this.gbOptions.Controls.Add(this.lblFrames);
          this.gbOptions.Controls.Add(this.cbbFrames);
          this.gbOptions.Controls.Add(this.cbDataOnly);
          this.gbOptions.Controls.Add(this.cbbCodepage);
          this.gbOptions.Controls.Add(this.lblCodepage);
          this.gbOptions.Controls.Add(this.cbEmptyLines);
          this.gbOptions.Controls.Add(this.cbPageBreaks);
          this.gbOptions.Location = new System.Drawing.Point(8, 4);
          this.gbOptions.Name = "gbOptions";
          this.gbOptions.Size = new System.Drawing.Size(260, 172);
          this.gbOptions.TabIndex = 5;
          this.gbOptions.TabStop = false;
          this.gbOptions.Text = "Options";
          // 
          // lblFrames
          // 
          this.lblFrames.AutoSize = true;
          this.lblFrames.Location = new System.Drawing.Point(12, 107);
          this.lblFrames.Name = "lblFrames";
          this.lblFrames.Size = new System.Drawing.Size(42, 13);
          this.lblFrames.TabIndex = 29;
          this.lblFrames.Text = "Frames";
          // 
          // cbbFrames
          // 
          this.cbbFrames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbbFrames.FormattingEnabled = true;
          this.cbbFrames.Items.AddRange(new object[] {
            "None",
            "Text",
            "Graphic"});
          this.cbbFrames.Location = new System.Drawing.Point(108, 103);
          this.cbbFrames.Name = "cbbFrames";
          this.cbbFrames.Size = new System.Drawing.Size(136, 21);
          this.cbbFrames.TabIndex = 7;
          // 
          // cbDataOnly
          // 
          this.cbDataOnly.AutoSize = true;
          this.cbDataOnly.Location = new System.Drawing.Point(12, 76);
          this.cbDataOnly.Name = "cbDataOnly";
          this.cbDataOnly.Size = new System.Drawing.Size(72, 17);
          this.cbDataOnly.TabIndex = 6;
          this.cbDataOnly.Text = "Data only";
          this.cbDataOnly.UseVisualStyleBackColor = true;
          this.cbDataOnly.Click += new System.EventHandler(this.cbPageBreaks_Click);
          // 
          // cbbCodepage
          // 
          this.cbbCodepage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbbCodepage.FormattingEnabled = true;
          this.cbbCodepage.Items.AddRange(new object[] {
            "Default",
            "Unicode",
            "OEM"});
          this.cbbCodepage.Location = new System.Drawing.Point(108, 136);
          this.cbbCodepage.Name = "cbbCodepage";
          this.cbbCodepage.Size = new System.Drawing.Size(136, 21);
          this.cbbCodepage.TabIndex = 8;
          this.cbbCodepage.SelectedValueChanged += new System.EventHandler(this.cbbCodepage_SelectedValueChanged);
          // 
          // lblCodepage
          // 
          this.lblCodepage.AutoSize = true;
          this.lblCodepage.Location = new System.Drawing.Point(12, 140);
          this.lblCodepage.Name = "lblCodepage";
          this.lblCodepage.Size = new System.Drawing.Size(56, 13);
          this.lblCodepage.TabIndex = 9;
          this.lblCodepage.Text = "Codepage";
          // 
          // cbEmptyLines
          // 
          this.cbEmptyLines.AutoSize = true;
          this.cbEmptyLines.Location = new System.Drawing.Point(12, 52);
          this.cbEmptyLines.Name = "cbEmptyLines";
          this.cbEmptyLines.Size = new System.Drawing.Size(80, 17);
          this.cbEmptyLines.TabIndex = 5;
          this.cbEmptyLines.Text = "Empty lines";
          this.cbEmptyLines.UseVisualStyleBackColor = true;
          this.cbEmptyLines.Click += new System.EventHandler(this.cbPageBreaks_Click);
          // 
          // cbPageBreaks
          // 
          this.cbPageBreaks.AutoSize = true;
          this.cbPageBreaks.Checked = true;
          this.cbPageBreaks.CheckState = System.Windows.Forms.CheckState.Checked;
          this.cbPageBreaks.Location = new System.Drawing.Point(12, 28);
          this.cbPageBreaks.Name = "cbPageBreaks";
          this.cbPageBreaks.Size = new System.Drawing.Size(85, 17);
          this.cbPageBreaks.TabIndex = 4;
          this.cbPageBreaks.Text = "Page breaks";
          this.cbPageBreaks.UseVisualStyleBackColor = true;
          this.cbPageBreaks.Click += new System.EventHandler(this.cbPageBreaks_Click);
          // 
          // lblLoss
          // 
          this.lblLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
          this.lblLoss.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lblLoss.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
          this.lblLoss.ForeColor = System.Drawing.Color.Red;
          this.lblLoss.Location = new System.Drawing.Point(371, 227);
          this.lblLoss.Name = "lblLoss";
          this.lblLoss.Size = new System.Drawing.Size(389, 52);
          this.lblLoss.TabIndex = 27;
          this.lblLoss.Text = "POSSIBLE DATA LOSS\r\nplease increase Scale X/Y";
          this.lblLoss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          this.lblLoss.Visible = false;
          // 
          // tbPreview
          // 
          this.tbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.tbPreview.BackColor = System.Drawing.Color.White;
          this.tbPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.tbPreview.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
          this.tbPreview.ForeColor = System.Drawing.Color.Black;
          this.tbPreview.Location = new System.Drawing.Point(316, 25);
          this.tbPreview.Multiline = true;
          this.tbPreview.Name = "tbPreview";
          this.tbPreview.ReadOnly = true;
          this.tbPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
          this.tbPreview.Size = new System.Drawing.Size(498, 455);
          this.tbPreview.TabIndex = 17;
          this.tbPreview.Text = "1\r\n2\r\n3";
          this.tbPreview.WordWrap = false;
          // 
          // Status
          // 
          this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.lblPageWidth,
            this.lblPageWidthValue,
            this.toolStripStatusLabel1,
            this.lblPageHeight,
            this.lblPageHeightValue});
          this.Status.Location = new System.Drawing.Point(0, 480);
          this.Status.Name = "Status";
          this.Status.Size = new System.Drawing.Size(813, 22);
          this.Status.TabIndex = 29;
          this.Status.Text = "Status";
          // 
          // toolStripStatusLabel2
          // 
          this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
          this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 17);
          this.toolStripStatusLabel2.Text = "   ";
          // 
          // lblPageWidth
          // 
          this.lblPageWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.lblPageWidth.Name = "lblPageWidth";
          this.lblPageWidth.Size = new System.Drawing.Size(64, 17);
          this.lblPageWidth.Text = "Page width:";
          // 
          // lblPageWidthValue
          // 
          this.lblPageWidthValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.lblPageWidthValue.Name = "lblPageWidthValue";
          this.lblPageWidthValue.Size = new System.Drawing.Size(25, 17);
          this.lblPageWidthValue.Text = "100";
          // 
          // toolStripStatusLabel1
          // 
          this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
          this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
          this.toolStripStatusLabel1.Text = "   ";
          // 
          // lblPageHeight
          // 
          this.lblPageHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.lblPageHeight.Name = "lblPageHeight";
          this.lblPageHeight.Size = new System.Drawing.Size(68, 17);
          this.lblPageHeight.Text = "Page height:";
          // 
          // lblPageHeightValue
          // 
          this.lblPageHeightValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.lblPageHeightValue.Name = "lblPageHeightValue";
          this.lblPageHeightValue.Size = new System.Drawing.Size(25, 17);
          this.lblPageHeightValue.Text = "100";
          // 
          // toolStrip
          // 
          this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.btnSave,
            this.toolStripSeparator1,
            this.cbFontSize,
            this.btnZoomOut,
            this.btnZoomIn,
            this.toolStripSeparator2,
            this.btnFirst,
            this.btnPrior,
            this.tbPage,
            this.lblTotalPages,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator3,
            this.btnClose});
          this.toolStrip.Location = new System.Drawing.Point(0, 0);
          this.toolStrip.Name = "toolStrip";
          this.toolStrip.Size = new System.Drawing.Size(813, 25);
          this.toolStrip.TabIndex = 30;
          this.toolStrip.Text = "toolStrip";
          // 
          // btnPrint
          // 
          this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnPrint.Name = "btnPrint";
          this.btnPrint.Size = new System.Drawing.Size(33, 22);
          this.btnPrint.Text = "Print";
          this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
          // 
          // btnSave
          // 
          this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnSave.Name = "btnSave";
          this.btnSave.Size = new System.Drawing.Size(35, 22);
          this.btnSave.Text = "Save";
          this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
          // 
          // toolStripSeparator1
          // 
          this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
          // 
          // btnZoomOut
          // 
          this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
          this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnZoomOut.Name = "btnZoomOut";
          this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
          this.btnZoomOut.Text = "toolStripButton1";
          this.btnZoomOut.ToolTipText = "Zoom Out";
          this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
          // 
          // cbFontSize
          // 
          this.cbFontSize.AutoSize = false;
          this.cbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbFontSize.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14"});
          this.cbFontSize.Name = "cbFontSize";
          this.cbFontSize.Size = new System.Drawing.Size(40, 21);
          this.cbFontSize.SelectedIndexChanged += new System.EventHandler(this.cbFontSize_SelectedIndexChanged);
          // 
          // btnZoomIn
          // 
          this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
          this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnZoomIn.Name = "btnZoomIn";
          this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
          this.btnZoomIn.Text = "toolStripButton2";
          this.btnZoomIn.ToolTipText = "Zoom In";
          this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
          // 
          // toolStripSeparator2
          // 
          this.toolStripSeparator2.Name = "toolStripSeparator2";
          this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
          // 
          // btnFirst
          // 
          this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
          this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnFirst.Name = "btnFirst";
          this.btnFirst.Size = new System.Drawing.Size(23, 22);
          this.btnFirst.ToolTipText = "First page";
          this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
          // 
          // btnPrior
          // 
          this.btnPrior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnPrior.Image = ((System.Drawing.Image)(resources.GetObject("btnPrior.Image")));
          this.btnPrior.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnPrior.Name = "btnPrior";
          this.btnPrior.Size = new System.Drawing.Size(23, 22);
          this.btnPrior.ToolTipText = "Previous page";
          this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
          // 
          // tbPage
          // 
          this.tbPage.MaxLength = 5;
          this.tbPage.Name = "tbPage";
          this.tbPage.Size = new System.Drawing.Size(40, 25);
          this.tbPage.Text = "1";
          this.tbPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          this.tbPage.TextChanged += new System.EventHandler(this.tbPage_TextChanged);
          // 
          // lblTotalPages
          // 
          this.lblTotalPages.Name = "lblTotalPages";
          this.lblTotalPages.Size = new System.Drawing.Size(26, 22);
          this.lblTotalPages.Text = "of 5";
          // 
          // btnNext
          // 
          this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
          this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnNext.Name = "btnNext";
          this.btnNext.Size = new System.Drawing.Size(23, 22);
          this.btnNext.ToolTipText = "Next page";
          this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
          // 
          // btnLast
          // 
          this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
          this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnLast.Name = "btnLast";
          this.btnLast.Size = new System.Drawing.Size(23, 22);
          this.btnLast.ToolTipText = "Last page";
          this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
          // 
          // toolStripSeparator3
          // 
          this.toolStripSeparator3.Name = "toolStripSeparator3";
          this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
          // 
          // btnClose
          // 
          this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
          this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
          this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.btnClose.Name = "btnClose";
          this.btnClose.Size = new System.Drawing.Size(37, 22);
          this.btnClose.Text = "Close";
          this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
          // 
          // picPerforation
          // 
          this.picPerforation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.picPerforation.BackColor = System.Drawing.Color.Transparent;
          this.picPerforation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPerforation.BackgroundImage")));
          this.picPerforation.Location = new System.Drawing.Point(285, 25);
          this.picPerforation.Name = "picPerforation";
          this.picPerforation.Size = new System.Drawing.Size(30, 455);
          this.picPerforation.TabIndex = 31;
          this.picPerforation.TabStop = false;
          // 
          // gbScale
          // 
          this.gbScale.Controls.Add(this.udX);
          this.gbScale.Controls.Add(this.lblX);
          this.gbScale.Controls.Add(this.btnCalculate);
          this.gbScale.Controls.Add(this.udY);
          this.gbScale.Controls.Add(this.lblY);
          this.gbScale.Location = new System.Drawing.Point(8, 180);
          this.gbScale.Name = "gbScale";
          this.gbScale.Size = new System.Drawing.Size(260, 128);
          this.gbScale.TabIndex = 6;
          this.gbScale.TabStop = false;
          this.gbScale.Text = "Scale";
          // 
          // udX
          // 
          this.udX.DecimalPlaces = 2;
          this.udX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
          this.udX.Location = new System.Drawing.Point(108, 28);
          this.udX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
          this.udX.Name = "udX";
          this.udX.Size = new System.Drawing.Size(92, 20);
          this.udX.TabIndex = 27;
          this.udX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.udX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // lblX
          // 
          this.lblX.AutoSize = true;
          this.lblX.Location = new System.Drawing.Point(16, 31);
          this.lblX.Name = "lblX";
          this.lblX.Size = new System.Drawing.Size(45, 13);
          this.lblX.TabIndex = 30;
          this.lblX.Text = "Scale X:";
          // 
          // btnCalculate
          // 
          this.btnCalculate.AutoSize = true;
          this.btnCalculate.Location = new System.Drawing.Point(108, 92);
          this.btnCalculate.Name = "btnCalculate";
          this.btnCalculate.Size = new System.Drawing.Size(92, 23);
          this.btnCalculate.TabIndex = 29;
          this.btnCalculate.Text = "Auto scale";
          this.btnCalculate.UseVisualStyleBackColor = true;
          this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
          // 
          // udY
          // 
          this.udY.DecimalPlaces = 2;
          this.udY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
          this.udY.Location = new System.Drawing.Point(108, 60);
          this.udY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
          this.udY.Name = "udY";
          this.udY.Size = new System.Drawing.Size(92, 20);
          this.udY.TabIndex = 28;
          this.udY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.udY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // lblY
          // 
          this.lblY.AutoSize = true;
          this.lblY.Location = new System.Drawing.Point(16, 62);
          this.lblY.Name = "lblY";
          this.lblY.Size = new System.Drawing.Size(45, 13);
          this.lblY.TabIndex = 31;
          this.lblY.Text = "Scale Y:";
          // 
          // TextExportForm
          // 
          this.AcceptButton = null;
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.CancelButton = null;
          this.ClientSize = new System.Drawing.Size(813, 502);
          this.Controls.Add(this.picPerforation);
          this.Controls.Add(this.toolStrip);
          this.Controls.Add(this.Status);
          this.Controls.Add(this.lblLoss);
          this.Controls.Add(this.tbPreview);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
          this.KeyPreview = true;
          this.MaximizeBox = true;
          this.MinimizeBox = true;
          this.Name = "TextExportForm";
          this.ShowIcon = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
          this.Text = "Export to text/dot-matrix";
          this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextExportForm_FormClosing);
          this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextExportForm_KeyDown);
          this.Controls.SetChildIndex(this.btnOk, 0);
          this.Controls.SetChildIndex(this.cbOpenAfter, 0);
          this.Controls.SetChildIndex(this.btnCancel, 0);
          this.Controls.SetChildIndex(this.tbPreview, 0);
          this.Controls.SetChildIndex(this.lblLoss, 0);
          this.Controls.SetChildIndex(this.Status, 0);
          this.Controls.SetChildIndex(this.toolStrip, 0);
          this.Controls.SetChildIndex(this.picPerforation, 0);
          this.Controls.SetChildIndex(this.pcPages, 0);
          this.gbPageRange.ResumeLayout(false);
          this.gbPageRange.PerformLayout();
          this.pcPages.ResumeLayout(false);
          this.panPages.ResumeLayout(false);
          this.gbOptions.ResumeLayout(false);
          this.gbOptions.PerformLayout();
          this.Status.ResumeLayout(false);
          this.Status.PerformLayout();
          this.toolStrip.ResumeLayout(false);
          this.toolStrip.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.picPerforation)).EndInit();
          this.gbScale.ResumeLayout(false);
          this.gbScale.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.udX)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.udY)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbPageBreaks;
        private System.Windows.Forms.CheckBox cbEmptyLines;
        private System.Windows.Forms.ComboBox cbbCodepage;
        private System.Windows.Forms.Label lblCodepage;
        private System.Windows.Forms.CheckBox cbDataOnly;
        private System.Windows.Forms.Label lblLoss;
        private System.Windows.Forms.TextBox tbPreview;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel lblPageWidth;
        private System.Windows.Forms.ToolStripStatusLabel lblPageWidthValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblPageHeight;
        private System.Windows.Forms.ToolStripStatusLabel lblPageHeightValue;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripComboBox cbFontSize;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrior;
        private System.Windows.Forms.ToolStripTextBox tbPage;
        private System.Windows.Forms.ToolStripLabel lblTotalPages;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.Label lblFrames;
        private System.Windows.Forms.ComboBox cbbFrames;
        private System.Windows.Forms.PictureBox picPerforation;
        private System.Windows.Forms.GroupBox gbScale;
        private System.Windows.Forms.NumericUpDown udX;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.NumericUpDown udY;
        private System.Windows.Forms.Label lblY;

    }
}
