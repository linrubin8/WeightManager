namespace FastReport.Forms
{
    partial class PDFExportForm
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
            this.pageControlOptions = new FastReport.Controls.PageControlPage();
            this.gbImages = new System.Windows.Forms.GroupBox();
            this.cbOriginalResolution = new System.Windows.Forms.CheckBox();
            this.lblColorSpace = new System.Windows.Forms.Label();
            this.cbColorSpace = new System.Windows.Forms.ComboBox();
            this.cbJpegCompression = new System.Windows.Forms.CheckBox();
            this.cbPrintOptimized = new System.Windows.Forms.CheckBox();
            this.nudJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.lblJpegQuality = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.lblCompliance = new System.Windows.Forms.Label();
            this.cbPdfStandard = new System.Windows.Forms.ComboBox();
            this.cbTextInCurves = new System.Windows.Forms.CheckBox();
            this.cbBackground = new System.Windows.Forms.CheckBox();
            this.cbEmbeddedFonts = new System.Windows.Forms.CheckBox();
            this.pageControlInformation = new FastReport.Controls.PageControlPage();
            this.gbDocumentInfo = new System.Windows.Forms.GroupBox();
            this.tbProducer = new System.Windows.Forms.TextBox();
            this.lbProducer = new System.Windows.Forms.Label();
            this.tbCreator = new System.Windows.Forms.TextBox();
            this.lbCreator = new System.Windows.Forms.Label();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.lbKeywords = new System.Windows.Forms.Label();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.lbSubject = new System.Windows.Forms.Label();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pageControlSecurity = new FastReport.Controls.PageControlPage();
            this.gbPermissions = new System.Windows.Forms.GroupBox();
            this.cbAnnotations = new System.Windows.Forms.CheckBox();
            this.cbCopyOfTextAndGraphics = new System.Windows.Forms.CheckBox();
            this.cbModifyTheDocument = new System.Windows.Forms.CheckBox();
            this.cbPrintTheDocument = new System.Windows.Forms.CheckBox();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.lbUserPassword = new System.Windows.Forms.Label();
            this.tbOwnerPassword = new System.Windows.Forms.TextBox();
            this.lbOwnerPassword = new System.Windows.Forms.Label();
            this.pageControlViewer = new FastReport.Controls.PageControlPage();
            this.gbViewerPrfs = new System.Windows.Forms.GroupBox();
            this.cbPrintDialog = new System.Windows.Forms.CheckBox();
            this.cbbZoom = new System.Windows.Forms.ComboBox();
            this.lblInitialZoom = new System.Windows.Forms.Label();
            this.cbOutline = new System.Windows.Forms.CheckBox();
            this.cbPrintScaling = new System.Windows.Forms.CheckBox();
            this.cbCenterWindow = new System.Windows.Forms.CheckBox();
            this.cbFitWindow = new System.Windows.Forms.CheckBox();
            this.cbHideUI = new System.Windows.Forms.CheckBox();
            this.cbHideMenubar = new System.Windows.Forms.CheckBox();
            this.cbHideToolbar = new System.Windows.Forms.CheckBox();
            this.gbPageRange.SuspendLayout();
            this.pcPages.SuspendLayout();
            this.panPages.SuspendLayout();
            this.pageControlOptions.SuspendLayout();
            this.gbImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJpegQuality)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.pageControlInformation.SuspendLayout();
            this.gbDocumentInfo.SuspendLayout();
            this.pageControlSecurity.SuspendLayout();
            this.gbPermissions.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.pageControlViewer.SuspendLayout();
            this.gbViewerPrfs.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPageRange
            // 
            this.gbPageRange.Location = new System.Drawing.Point(8, 4);
            // 
            // pcPages
            // 
            this.pcPages.Controls.Add(this.pageControlOptions);
            this.pcPages.Controls.Add(this.pageControlInformation);
            this.pcPages.Controls.Add(this.pageControlSecurity);
            this.pcPages.Controls.Add(this.pageControlViewer);
            this.pcPages.Location = new System.Drawing.Point(12, 12);
            this.pcPages.SelectorWidth = 100;
            this.pcPages.Size = new System.Drawing.Size(376, 308);
            this.pcPages.Text = "Export";
            this.pcPages.Controls.SetChildIndex(this.pageControlViewer, 0);
            this.pcPages.Controls.SetChildIndex(this.pageControlSecurity, 0);
            this.pcPages.Controls.SetChildIndex(this.pageControlInformation, 0);
            this.pcPages.Controls.SetChildIndex(this.pageControlOptions, 0);
            this.pcPages.Controls.SetChildIndex(this.panPages, 0);
            // 
            // panPages
            // 
            this.panPages.BackColor = System.Drawing.SystemColors.Window;
            this.panPages.Location = new System.Drawing.Point(100, 1);
            this.panPages.Size = new System.Drawing.Size(275, 306);
            this.panPages.Text = "Export";
            // 
            // cbOpenAfter
            // 
            this.cbOpenAfter.Location = new System.Drawing.Point(12, 330);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(231, 327);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 326);
            this.btnCancel.TabIndex = 1;
            // 
            // pageControlOptions
            // 
            this.pageControlOptions.BackColor = System.Drawing.SystemColors.Window;
            this.pageControlOptions.Controls.Add(this.gbImages);
            this.pageControlOptions.Controls.Add(this.gbOptions);
            this.pageControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControlOptions.Location = new System.Drawing.Point(100, 1);
            this.pageControlOptions.Name = "pageControlOptions";
            this.pageControlOptions.Size = new System.Drawing.Size(275, 306);
            this.pageControlOptions.TabIndex = 1;
            this.pageControlOptions.Text = "Options";
            // 
            // gbImages
            // 
            this.gbImages.Controls.Add(this.cbOriginalResolution);
            this.gbImages.Controls.Add(this.lblColorSpace);
            this.gbImages.Controls.Add(this.cbColorSpace);
            this.gbImages.Controls.Add(this.cbJpegCompression);
            this.gbImages.Controls.Add(this.cbPrintOptimized);
            this.gbImages.Controls.Add(this.nudJpegQuality);
            this.gbImages.Controls.Add(this.lblJpegQuality);
            this.gbImages.Location = new System.Drawing.Point(7, 128);
            this.gbImages.Name = "gbImages";
            this.gbImages.Size = new System.Drawing.Size(260, 155);
            this.gbImages.TabIndex = 7;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // cbOriginalResolution
            // 
            this.cbOriginalResolution.AutoSize = true;
            this.cbOriginalResolution.Location = new System.Drawing.Point(12, 51);
            this.cbOriginalResolution.Name = "cbOriginalResolution";
            this.cbOriginalResolution.Size = new System.Drawing.Size(115, 17);
            this.cbOriginalResolution.TabIndex = 14;
            this.cbOriginalResolution.Text = "Original Resolution";
            this.cbOriginalResolution.UseVisualStyleBackColor = true;
            // 
            // lblColorSpace
            // 
            this.lblColorSpace.AutoSize = true;
            this.lblColorSpace.Location = new System.Drawing.Point(9, 23);
            this.lblColorSpace.Name = "lblColorSpace";
            this.lblColorSpace.Size = new System.Drawing.Size(74, 13);
            this.lblColorSpace.TabIndex = 13;
            this.lblColorSpace.Text = "Color Space:  ";
            // 
            // cbColorSpace
            // 
            this.cbColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorSpace.FormattingEnabled = true;
            this.cbColorSpace.Items.AddRange(new object[] {
            "RGB",
            "CMYK"});
            this.cbColorSpace.Location = new System.Drawing.Point(173, 20);
            this.cbColorSpace.Name = "cbColorSpace";
            this.cbColorSpace.Size = new System.Drawing.Size(81, 21);
            this.cbColorSpace.TabIndex = 12;
            // 
            // cbJpegCompression
            // 
            this.cbJpegCompression.AutoSize = true;
            this.cbJpegCompression.Location = new System.Drawing.Point(12, 97);
            this.cbJpegCompression.Name = "cbJpegCompression";
            this.cbJpegCompression.Size = new System.Drawing.Size(113, 17);
            this.cbJpegCompression.TabIndex = 10;
            this.cbJpegCompression.Text = "Jpeg Compression";
            this.cbJpegCompression.UseVisualStyleBackColor = true;
            // 
            // cbPrintOptimized
            // 
            this.cbPrintOptimized.AutoSize = true;
            this.cbPrintOptimized.Location = new System.Drawing.Point(12, 74);
            this.cbPrintOptimized.Name = "cbPrintOptimized";
            this.cbPrintOptimized.Size = new System.Drawing.Size(98, 17);
            this.cbPrintOptimized.TabIndex = 2;
            this.cbPrintOptimized.Text = "Print Optimized";
            this.cbPrintOptimized.UseVisualStyleBackColor = true;
            // 
            // nudJpegQuality
            // 
            this.nudJpegQuality.Location = new System.Drawing.Point(193, 126);
            this.nudJpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudJpegQuality.Name = "nudJpegQuality";
            this.nudJpegQuality.Size = new System.Drawing.Size(61, 20);
            this.nudJpegQuality.TabIndex = 5;
            this.nudJpegQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudJpegQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblJpegQuality
            // 
            this.lblJpegQuality.AutoSize = true;
            this.lblJpegQuality.Location = new System.Drawing.Point(9, 128);
            this.lblJpegQuality.Name = "lblJpegQuality";
            this.lblJpegQuality.Size = new System.Drawing.Size(75, 13);
            this.lblJpegQuality.TabIndex = 9;
            this.lblJpegQuality.Text = "Jpeg quality:  ";
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.lblCompliance);
            this.gbOptions.Controls.Add(this.cbPdfStandard);
            this.gbOptions.Controls.Add(this.cbTextInCurves);
            this.gbOptions.Controls.Add(this.cbBackground);
            this.gbOptions.Controls.Add(this.cbEmbeddedFonts);
            this.gbOptions.Location = new System.Drawing.Point(7, 3);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(260, 119);
            this.gbOptions.TabIndex = 6;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // lblCompliance
            // 
            this.lblCompliance.AutoSize = true;
            this.lblCompliance.Location = new System.Drawing.Point(9, 22);
            this.lblCompliance.Name = "lblCompliance";
            this.lblCompliance.Size = new System.Drawing.Size(93, 13);
            this.lblCompliance.TabIndex = 11;
            this.lblCompliance.Text = "PDF Compliance:  ";
            // 
            // cbPdfStandard
            // 
            this.cbPdfStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPdfStandard.FormattingEnabled = true;
            this.cbPdfStandard.Items.AddRange(new object[] {
            "PDF 1.5",
            "PDF/A-2a",
            "PDF/A-2b",
            "PDF/A-3a",
            "PDF/A-3b",
            "PDF/X-3",
            "PDF/X-4"});
            this.cbPdfStandard.Location = new System.Drawing.Point(173, 19);
            this.cbPdfStandard.Name = "cbPdfStandard";
            this.cbPdfStandard.Size = new System.Drawing.Size(81, 21);
            this.cbPdfStandard.TabIndex = 7;
            // 
            // cbTextInCurves
            // 
            this.cbTextInCurves.AutoSize = true;
            this.cbTextInCurves.Location = new System.Drawing.Point(12, 93);
            this.cbTextInCurves.Name = "cbTextInCurves";
            this.cbTextInCurves.Size = new System.Drawing.Size(96, 17);
            this.cbTextInCurves.TabIndex = 4;
            this.cbTextInCurves.Text = "Text in Curves";
            this.cbTextInCurves.UseVisualStyleBackColor = true;
            // 
            // cbBackground
            // 
            this.cbBackground.AutoSize = true;
            this.cbBackground.Location = new System.Drawing.Point(12, 70);
            this.cbBackground.Name = "cbBackground";
            this.cbBackground.Size = new System.Drawing.Size(82, 17);
            this.cbBackground.TabIndex = 3;
            this.cbBackground.Text = "Background";
            this.cbBackground.UseVisualStyleBackColor = true;
            // 
            // cbEmbedded
            // 
            this.cbEmbeddedFonts.AutoSize = true;
            this.cbEmbeddedFonts.Location = new System.Drawing.Point(12, 47);
            this.cbEmbeddedFonts.Name = "cbEmbedded";
            this.cbEmbeddedFonts.Size = new System.Drawing.Size(106, 17);
            this.cbEmbeddedFonts.TabIndex = 1;
            this.cbEmbeddedFonts.Text = "Embedded Fonts";
            this.cbEmbeddedFonts.UseVisualStyleBackColor = true;
            // 
            // pageControlInformation
            // 
            this.pageControlInformation.BackColor = System.Drawing.SystemColors.Window;
            this.pageControlInformation.Controls.Add(this.gbDocumentInfo);
            this.pageControlInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControlInformation.Location = new System.Drawing.Point(100, 1);
            this.pageControlInformation.Name = "pageControlInformation";
            this.pageControlInformation.Size = new System.Drawing.Size(275, 306);
            this.pageControlInformation.TabIndex = 2;
            this.pageControlInformation.Text = "Information";
            // 
            // gbDocumentInfo
            // 
            this.gbDocumentInfo.Controls.Add(this.tbProducer);
            this.gbDocumentInfo.Controls.Add(this.lbProducer);
            this.gbDocumentInfo.Controls.Add(this.tbCreator);
            this.gbDocumentInfo.Controls.Add(this.lbCreator);
            this.gbDocumentInfo.Controls.Add(this.tbKeywords);
            this.gbDocumentInfo.Controls.Add(this.lbKeywords);
            this.gbDocumentInfo.Controls.Add(this.tbSubject);
            this.gbDocumentInfo.Controls.Add(this.lbSubject);
            this.gbDocumentInfo.Controls.Add(this.tbAuthor);
            this.gbDocumentInfo.Controls.Add(this.lbAuthor);
            this.gbDocumentInfo.Controls.Add(this.tbTitle);
            this.gbDocumentInfo.Controls.Add(this.lbTitle);
            this.gbDocumentInfo.Location = new System.Drawing.Point(7, 4);
            this.gbDocumentInfo.Name = "gbDocumentInfo";
            this.gbDocumentInfo.Size = new System.Drawing.Size(260, 194);
            this.gbDocumentInfo.TabIndex = 0;
            this.gbDocumentInfo.TabStop = false;
            this.gbDocumentInfo.Text = "Document Information";
            // 
            // tbProducer
            // 
            this.tbProducer.Location = new System.Drawing.Point(104, 161);
            this.tbProducer.Name = "tbProducer";
            this.tbProducer.Size = new System.Drawing.Size(144, 20);
            this.tbProducer.TabIndex = 11;
            // 
            // lbProducer
            // 
            this.lbProducer.AutoSize = true;
            this.lbProducer.Location = new System.Drawing.Point(12, 165);
            this.lbProducer.Name = "lbProducer";
            this.lbProducer.Size = new System.Drawing.Size(50, 13);
            this.lbProducer.TabIndex = 10;
            this.lbProducer.Text = "Producer";
            // 
            // tbCreator
            // 
            this.tbCreator.Location = new System.Drawing.Point(104, 133);
            this.tbCreator.Name = "tbCreator";
            this.tbCreator.Size = new System.Drawing.Size(144, 20);
            this.tbCreator.TabIndex = 9;
            // 
            // lbCreator
            // 
            this.lbCreator.AutoSize = true;
            this.lbCreator.Location = new System.Drawing.Point(12, 137);
            this.lbCreator.Name = "lbCreator";
            this.lbCreator.Size = new System.Drawing.Size(44, 13);
            this.lbCreator.TabIndex = 8;
            this.lbCreator.Text = "Creator";
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(104, 105);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(144, 20);
            this.tbKeywords.TabIndex = 7;
            // 
            // lbKeywords
            // 
            this.lbKeywords.AutoSize = true;
            this.lbKeywords.Location = new System.Drawing.Point(12, 109);
            this.lbKeywords.Name = "lbKeywords";
            this.lbKeywords.Size = new System.Drawing.Size(54, 13);
            this.lbKeywords.TabIndex = 6;
            this.lbKeywords.Text = "Keywords";
            // 
            // tbSubject
            // 
            this.tbSubject.Location = new System.Drawing.Point(104, 77);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(144, 20);
            this.tbSubject.TabIndex = 5;
            // 
            // lbSubject
            // 
            this.lbSubject.AutoSize = true;
            this.lbSubject.Location = new System.Drawing.Point(12, 81);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(43, 13);
            this.lbSubject.TabIndex = 4;
            this.lbSubject.Text = "Subject";
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(104, 49);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(144, 20);
            this.tbAuthor.TabIndex = 3;
            // 
            // lbAuthor
            // 
            this.lbAuthor.AutoSize = true;
            this.lbAuthor.Location = new System.Drawing.Point(12, 53);
            this.lbAuthor.Name = "lbAuthor";
            this.lbAuthor.Size = new System.Drawing.Size(40, 13);
            this.lbAuthor.TabIndex = 2;
            this.lbAuthor.Text = "Author";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(104, 21);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(144, 20);
            this.tbTitle.TabIndex = 1;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(12, 25);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(27, 13);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title";
            // 
            // pageControlSecurity
            // 
            this.pageControlSecurity.BackColor = System.Drawing.SystemColors.Window;
            this.pageControlSecurity.Controls.Add(this.gbPermissions);
            this.pageControlSecurity.Controls.Add(this.gbAuth);
            this.pageControlSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControlSecurity.Location = new System.Drawing.Point(100, 1);
            this.pageControlSecurity.Name = "pageControlSecurity";
            this.pageControlSecurity.Size = new System.Drawing.Size(275, 306);
            this.pageControlSecurity.TabIndex = 3;
            this.pageControlSecurity.Text = "Security";
            // 
            // gbPermissions
            // 
            this.gbPermissions.Controls.Add(this.cbAnnotations);
            this.gbPermissions.Controls.Add(this.cbCopyOfTextAndGraphics);
            this.gbPermissions.Controls.Add(this.cbModifyTheDocument);
            this.gbPermissions.Controls.Add(this.cbPrintTheDocument);
            this.gbPermissions.Location = new System.Drawing.Point(8, 88);
            this.gbPermissions.Name = "gbPermissions";
            this.gbPermissions.Size = new System.Drawing.Size(260, 132);
            this.gbPermissions.TabIndex = 1;
            this.gbPermissions.TabStop = false;
            this.gbPermissions.Text = "Permissions";
            // 
            // cbAnnotations
            // 
            this.cbAnnotations.AutoSize = true;
            this.cbAnnotations.Location = new System.Drawing.Point(12, 100);
            this.cbAnnotations.Name = "cbAnnotations";
            this.cbAnnotations.Size = new System.Drawing.Size(156, 17);
            this.cbAnnotations.TabIndex = 3;
            this.cbAnnotations.Text = "Add Or Modify Annotations";
            this.cbAnnotations.UseVisualStyleBackColor = true;
            // 
            // cbCopyOfTextAndGraphics
            // 
            this.cbCopyOfTextAndGraphics.AutoSize = true;
            this.cbCopyOfTextAndGraphics.Location = new System.Drawing.Point(12, 76);
            this.cbCopyOfTextAndGraphics.Name = "cbCopyOfTextAndGraphics";
            this.cbCopyOfTextAndGraphics.Size = new System.Drawing.Size(157, 17);
            this.cbCopyOfTextAndGraphics.TabIndex = 2;
            this.cbCopyOfTextAndGraphics.Text = "Copy Of Text And Graphics";
            this.cbCopyOfTextAndGraphics.UseVisualStyleBackColor = true;
            // 
            // cbModifyTheDocument
            // 
            this.cbModifyTheDocument.AutoSize = true;
            this.cbModifyTheDocument.Location = new System.Drawing.Point(12, 52);
            this.cbModifyTheDocument.Name = "cbModifyTheDocument";
            this.cbModifyTheDocument.Size = new System.Drawing.Size(130, 17);
            this.cbModifyTheDocument.TabIndex = 1;
            this.cbModifyTheDocument.Text = "Modify The Document";
            this.cbModifyTheDocument.UseVisualStyleBackColor = true;
            // 
            // cbPrintTheDocument
            // 
            this.cbPrintTheDocument.AutoSize = true;
            this.cbPrintTheDocument.Location = new System.Drawing.Point(12, 28);
            this.cbPrintTheDocument.Name = "cbPrintTheDocument";
            this.cbPrintTheDocument.Size = new System.Drawing.Size(120, 17);
            this.cbPrintTheDocument.TabIndex = 0;
            this.cbPrintTheDocument.Text = "Print The Document";
            this.cbPrintTheDocument.UseVisualStyleBackColor = true;
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.tbUserPassword);
            this.gbAuth.Controls.Add(this.lbUserPassword);
            this.gbAuth.Controls.Add(this.tbOwnerPassword);
            this.gbAuth.Controls.Add(this.lbOwnerPassword);
            this.gbAuth.Location = new System.Drawing.Point(8, 4);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(260, 80);
            this.gbAuth.TabIndex = 0;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authentification";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(140, 48);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.PasswordChar = '*';
            this.tbUserPassword.Size = new System.Drawing.Size(108, 20);
            this.tbUserPassword.TabIndex = 3;
            // 
            // lbUserPassword
            // 
            this.lbUserPassword.AutoSize = true;
            this.lbUserPassword.Location = new System.Drawing.Point(12, 52);
            this.lbUserPassword.Name = "lbUserPassword";
            this.lbUserPassword.Size = new System.Drawing.Size(78, 13);
            this.lbUserPassword.TabIndex = 2;
            this.lbUserPassword.Text = "User Password";
            // 
            // tbOwnerPassword
            // 
            this.tbOwnerPassword.Location = new System.Drawing.Point(140, 20);
            this.tbOwnerPassword.Name = "tbOwnerPassword";
            this.tbOwnerPassword.PasswordChar = '*';
            this.tbOwnerPassword.Size = new System.Drawing.Size(108, 20);
            this.tbOwnerPassword.TabIndex = 1;
            // 
            // lbOwnerPassword
            // 
            this.lbOwnerPassword.AutoSize = true;
            this.lbOwnerPassword.Location = new System.Drawing.Point(12, 24);
            this.lbOwnerPassword.Name = "lbOwnerPassword";
            this.lbOwnerPassword.Size = new System.Drawing.Size(88, 13);
            this.lbOwnerPassword.TabIndex = 0;
            this.lbOwnerPassword.Text = "Owner Password";
            // 
            // pageControlViewer
            // 
            this.pageControlViewer.BackColor = System.Drawing.SystemColors.Window;
            this.pageControlViewer.Controls.Add(this.gbViewerPrfs);
            this.pageControlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControlViewer.Location = new System.Drawing.Point(100, 1);
            this.pageControlViewer.Name = "pageControlViewer";
            this.pageControlViewer.Size = new System.Drawing.Size(275, 306);
            this.pageControlViewer.TabIndex = 4;
            this.pageControlViewer.Text = "Viewer";
            // 
            // gbViewerPrfs
            // 
            this.gbViewerPrfs.Controls.Add(this.cbPrintDialog);
            this.gbViewerPrfs.Controls.Add(this.cbbZoom);
            this.gbViewerPrfs.Controls.Add(this.lblInitialZoom);
            this.gbViewerPrfs.Controls.Add(this.cbOutline);
            this.gbViewerPrfs.Controls.Add(this.cbPrintScaling);
            this.gbViewerPrfs.Controls.Add(this.cbCenterWindow);
            this.gbViewerPrfs.Controls.Add(this.cbFitWindow);
            this.gbViewerPrfs.Controls.Add(this.cbHideUI);
            this.gbViewerPrfs.Controls.Add(this.cbHideMenubar);
            this.gbViewerPrfs.Controls.Add(this.cbHideToolbar);
            this.gbViewerPrfs.Location = new System.Drawing.Point(8, 4);
            this.gbViewerPrfs.Name = "gbViewerPrfs";
            this.gbViewerPrfs.Size = new System.Drawing.Size(260, 216);
            this.gbViewerPrfs.TabIndex = 0;
            this.gbViewerPrfs.TabStop = false;
            this.gbViewerPrfs.Text = "Viewer Preferences";
            // 
            // cbPrintDialog
            // 
            this.cbPrintDialog.AutoSize = true;
            this.cbPrintDialog.Location = new System.Drawing.Point(12, 20);
            this.cbPrintDialog.Name = "cbPrintDialog";
            this.cbPrintDialog.Size = new System.Drawing.Size(109, 17);
            this.cbPrintDialog.TabIndex = 0;
            this.cbPrintDialog.Text = "Show Print Dialog";
            this.cbPrintDialog.UseVisualStyleBackColor = true;
            // 
            // cbbZoom
            // 
            this.cbbZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbZoom.FormattingEnabled = true;
            this.cbbZoom.Items.AddRange(new object[] {
            "Actual Size",
            "Fit Page",
            "Fit Width",
            "Default",
            "10%",
            "25%",
            "50%",
            "75%",
            "100%",
            "125%",
            "150%",
            "200%",
            "400%",
            "800%"});
            this.cbbZoom.Location = new System.Drawing.Point(131, 188);
            this.cbbZoom.Name = "cbbZoom";
            this.cbbZoom.Size = new System.Drawing.Size(121, 21);
            this.cbbZoom.TabIndex = 9;
            // 
            // lblInitialZoom
            // 
            this.lblInitialZoom.AutoSize = true;
            this.lblInitialZoom.Location = new System.Drawing.Point(12, 191);
            this.lblInitialZoom.Name = "lblInitialZoom";
            this.lblInitialZoom.Size = new System.Drawing.Size(69, 13);
            this.lblInitialZoom.TabIndex = 8;
            this.lblInitialZoom.Text = "Initial Zoom: ";
            // 
            // cbOutline
            // 
            this.cbOutline.AutoSize = true;
            this.cbOutline.Location = new System.Drawing.Point(12, 165);
            this.cbOutline.Name = "cbOutline";
            this.cbOutline.Size = new System.Drawing.Size(60, 17);
            this.cbOutline.TabIndex = 7;
            this.cbOutline.Text = "Outline";
            this.cbOutline.UseVisualStyleBackColor = true;
            // 
            // cbPrintScaling
            // 
            this.cbPrintScaling.AutoSize = true;
            this.cbPrintScaling.Location = new System.Drawing.Point(12, 144);
            this.cbPrintScaling.Name = "cbPrintScaling";
            this.cbPrintScaling.Size = new System.Drawing.Size(84, 17);
            this.cbPrintScaling.TabIndex = 5;
            this.cbPrintScaling.Text = "Print Scaling";
            this.cbPrintScaling.UseVisualStyleBackColor = true;
            // 
            // cbCenterWindow
            // 
            this.cbCenterWindow.AutoSize = true;
            this.cbCenterWindow.Location = new System.Drawing.Point(12, 123);
            this.cbCenterWindow.Name = "cbCenterWindow";
            this.cbCenterWindow.Size = new System.Drawing.Size(100, 17);
            this.cbCenterWindow.TabIndex = 4;
            this.cbCenterWindow.Text = "Center Window";
            this.cbCenterWindow.UseVisualStyleBackColor = true;
            // 
            // cbFitWindow
            // 
            this.cbFitWindow.AutoSize = true;
            this.cbFitWindow.Location = new System.Drawing.Point(12, 102);
            this.cbFitWindow.Name = "cbFitWindow";
            this.cbFitWindow.Size = new System.Drawing.Size(79, 17);
            this.cbFitWindow.TabIndex = 3;
            this.cbFitWindow.Text = "Fit Window";
            this.cbFitWindow.UseVisualStyleBackColor = true;
            // 
            // cbHideUI
            // 
            this.cbHideUI.AutoSize = true;
            this.cbHideUI.Location = new System.Drawing.Point(12, 81);
            this.cbHideUI.Name = "cbHideUI";
            this.cbHideUI.Size = new System.Drawing.Size(161, 17);
            this.cbHideUI.TabIndex = 2;
            this.cbHideUI.Text = "Hide Window User Interface";
            this.cbHideUI.UseVisualStyleBackColor = true;
            // 
            // cbHideMenubar
            // 
            this.cbHideMenubar.AutoSize = true;
            this.cbHideMenubar.Location = new System.Drawing.Point(12, 60);
            this.cbHideMenubar.Name = "cbHideMenubar";
            this.cbHideMenubar.Size = new System.Drawing.Size(92, 17);
            this.cbHideMenubar.TabIndex = 1;
            this.cbHideMenubar.Text = "Hide Menubar";
            this.cbHideMenubar.UseVisualStyleBackColor = true;
            // 
            // cbHideToolbar
            // 
            this.cbHideToolbar.AutoSize = true;
            this.cbHideToolbar.Location = new System.Drawing.Point(12, 40);
            this.cbHideToolbar.Name = "cbHideToolbar";
            this.cbHideToolbar.Size = new System.Drawing.Size(86, 17);
            this.cbHideToolbar.TabIndex = 0;
            this.cbHideToolbar.Text = "Hide Toolbar";
            this.cbHideToolbar.UseVisualStyleBackColor = true;
            // 
            // PDFExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(399, 361);
            this.Name = "PDFExportForm";
            this.OpenAfterVisible = true;
            this.Text = "Export to PDF";
            this.gbPageRange.ResumeLayout(false);
            this.gbPageRange.PerformLayout();
            this.pcPages.ResumeLayout(false);
            this.panPages.ResumeLayout(false);
            this.pageControlOptions.ResumeLayout(false);
            this.gbImages.ResumeLayout(false);
            this.gbImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJpegQuality)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.pageControlInformation.ResumeLayout(false);
            this.gbDocumentInfo.ResumeLayout(false);
            this.gbDocumentInfo.PerformLayout();
            this.pageControlSecurity.ResumeLayout(false);
            this.gbPermissions.ResumeLayout(false);
            this.gbPermissions.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.pageControlViewer.ResumeLayout(false);
            this.gbViewerPrfs.ResumeLayout(false);
            this.gbViewerPrfs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastReport.Controls.PageControlPage pageControlOptions;
        private FastReport.Controls.PageControlPage pageControlInformation;
        private FastReport.Controls.PageControlPage pageControlSecurity;
        private FastReport.Controls.PageControlPage pageControlViewer;
        private System.Windows.Forms.GroupBox gbDocumentInfo;
        private System.Windows.Forms.TextBox tbProducer;
        private System.Windows.Forms.Label lbProducer;
        private System.Windows.Forms.TextBox tbCreator;
        private System.Windows.Forms.Label lbCreator;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Label lbKeywords;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.Label lbSubject;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox gbPermissions;
        private System.Windows.Forms.GroupBox gbAuth;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.Label lbUserPassword;
        private System.Windows.Forms.TextBox tbOwnerPassword;
        private System.Windows.Forms.Label lbOwnerPassword;
        private System.Windows.Forms.CheckBox cbAnnotations;
        private System.Windows.Forms.CheckBox cbCopyOfTextAndGraphics;
        private System.Windows.Forms.CheckBox cbModifyTheDocument;
        private System.Windows.Forms.CheckBox cbPrintTheDocument;
        private System.Windows.Forms.GroupBox gbViewerPrfs;
        private System.Windows.Forms.CheckBox cbHideMenubar;
        private System.Windows.Forms.CheckBox cbHideToolbar;
        private System.Windows.Forms.CheckBox cbPrintScaling;
        private System.Windows.Forms.CheckBox cbCenterWindow;
        private System.Windows.Forms.CheckBox cbFitWindow;
        private System.Windows.Forms.CheckBox cbHideUI;
        private System.Windows.Forms.CheckBox cbOutline;
        private System.Windows.Forms.GroupBox gbImages;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbPrintOptimized;
        private System.Windows.Forms.CheckBox cbBackground;
        private System.Windows.Forms.CheckBox cbEmbeddedFonts;
        private System.Windows.Forms.Label lblInitialZoom;
        private System.Windows.Forms.ComboBox cbbZoom;
        private System.Windows.Forms.ComboBox cbPdfStandard;
        private System.Windows.Forms.NumericUpDown nudJpegQuality;
        private System.Windows.Forms.Label lblJpegQuality;
        private System.Windows.Forms.CheckBox cbTextInCurves;
        private System.Windows.Forms.CheckBox cbPrintDialog;
        private System.Windows.Forms.CheckBox cbJpegCompression;
        private System.Windows.Forms.Label lblCompliance;
        private System.Windows.Forms.CheckBox cbOriginalResolution;
        private System.Windows.Forms.Label lblColorSpace;
        private System.Windows.Forms.ComboBox cbColorSpace;
    }
}
