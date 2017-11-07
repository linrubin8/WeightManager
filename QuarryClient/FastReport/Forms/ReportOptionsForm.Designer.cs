namespace FastReport.Forms
{
  partial class ReportOptionsForm
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
      this.tbDescription = new FastReport.Controls.TextBoxButton();
      this.cbSavePreviewPicture = new System.Windows.Forms.CheckBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.pbPicture = new System.Windows.Forms.PictureBox();
      this.lblModified1 = new System.Windows.Forms.Label();
      this.lblModified = new System.Windows.Forms.Label();
      this.lblCreated1 = new System.Windows.Forms.Label();
      this.lblCreated = new System.Windows.Forms.Label();
      this.lblPicture = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.lblName = new System.Windows.Forms.Label();
      this.tbAuthor = new System.Windows.Forms.TextBox();
      this.lblDescription = new System.Windows.Forms.Label();
      this.tbVersion = new System.Windows.Forms.TextBox();
      this.lblVersion = new System.Windows.Forms.Label();
      this.lblAuthor = new System.Windows.Forms.Label();
      this.btnAdd = new System.Windows.Forms.Button();
      this.tbRefAssemblies = new System.Windows.Forms.TextBox();
      this.lblRefAssemblies = new System.Windows.Forms.Label();
      this.lblLanguage = new System.Windows.Forms.Label();
      this.lblScriptNote = new System.Windows.Forms.Label();
      this.rbVB = new System.Windows.Forms.RadioButton();
      this.rbC = new System.Windows.Forms.RadioButton();
      this.lblPassword = new System.Windows.Forms.Label();
      this.tbRetypePassword = new System.Windows.Forms.TextBox();
      this.tbPassword = new System.Windows.Forms.TextBox();
      this.lblRetypePassword = new System.Windows.Forms.Label();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.rbInherit = new System.Windows.Forms.RadioButton();
      this.rbDetach = new System.Windows.Forms.RadioButton();
      this.rbDontChange = new System.Windows.Forms.RadioButton();
      this.lblChooseInheritance = new System.Windows.Forms.Label();
      this.lblBaseName = new System.Windows.Forms.Label();
      this.lblInheritance = new System.Windows.Forms.Label();
      this.pcPages = new FastReport.Controls.PageControl();
      this.pnGeneral = new FastReport.Controls.PageControlPage();
      this.cbxTextQuality = new System.Windows.Forms.ComboBox();
      this.lblTextQuality = new System.Windows.Forms.Label();
      this.cbDoublePass = new System.Windows.Forms.CheckBox();
      this.cbConvertNulls = new System.Windows.Forms.CheckBox();
      this.cbCompress = new System.Windows.Forms.CheckBox();
      this.cbSmoothGraphics = new System.Windows.Forms.CheckBox();
      this.cbUseFileCache = new System.Windows.Forms.CheckBox();
      this.pnDescription = new FastReport.Controls.PageControlPage();
      this.pnScript = new FastReport.Controls.PageControlPage();
      this.pnSecurity = new FastReport.Controls.PageControlPage();
      this.pnInheritance = new FastReport.Controls.PageControlPage();
      this.pnEmail = new FastReport.Controls.PageControlPage();
      this.tbSubject = new System.Windows.Forms.TextBox();
      this.lblSubject = new System.Windows.Forms.Label();
      this.tbMessage = new System.Windows.Forms.TextBox();
      this.lblMessage = new System.Windows.Forms.Label();
      this.tbRecipients = new System.Windows.Forms.TextBox();
      this.lblRecipients = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
      this.pcPages.SuspendLayout();
      this.pnGeneral.SuspendLayout();
      this.pnDescription.SuspendLayout();
      this.pnScript.SuspendLayout();
      this.pnSecurity.SuspendLayout();
      this.pnInheritance.SuspendLayout();
      this.pnEmail.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(292, 336);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(372, 336);
      // 
      // tbDescription
      // 
      this.tbDescription.Image = null;
      this.tbDescription.Location = new System.Drawing.Point(96, 88);
      this.tbDescription.Name = "tbDescription";
      this.tbDescription.Size = new System.Drawing.Size(204, 21);
      this.tbDescription.TabIndex = 3;
      this.tbDescription.ButtonClick += new System.EventHandler(this.tbDescription_ButtonClick);
      // 
      // cbSavePreviewPicture
      // 
      this.cbSavePreviewPicture.AutoSize = true;
      this.cbSavePreviewPicture.Location = new System.Drawing.Point(96, 228);
      this.cbSavePreviewPicture.Name = "cbSavePreviewPicture";
      this.cbSavePreviewPicture.Size = new System.Drawing.Size(127, 17);
      this.cbSavePreviewPicture.TabIndex = 6;
      this.cbSavePreviewPicture.Text = "Save preview picture";
      this.cbSavePreviewPicture.UseVisualStyleBackColor = true;
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(204, 144);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(96, 23);
      this.btnClear.TabIndex = 5;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(204, 116);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(96, 23);
      this.btnLoad.TabIndex = 4;
      this.btnLoad.Text = "Load...";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // pbPicture
      // 
      this.pbPicture.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.pbPicture.Location = new System.Drawing.Point(96, 116);
      this.pbPicture.Name = "pbPicture";
      this.pbPicture.Size = new System.Drawing.Size(96, 104);
      this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbPicture.TabIndex = 10;
      this.pbPicture.TabStop = false;
      // 
      // lblModified1
      // 
      this.lblModified1.AutoSize = true;
      this.lblModified1.Location = new System.Drawing.Point(96, 284);
      this.lblModified1.Name = "lblModified1";
      this.lblModified1.Size = new System.Drawing.Size(47, 13);
      this.lblModified1.TabIndex = 10;
      this.lblModified1.Text = "Modified";
      // 
      // lblModified
      // 
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(16, 284);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(47, 13);
      this.lblModified.TabIndex = 10;
      this.lblModified.Text = "Modified";
      // 
      // lblCreated1
      // 
      this.lblCreated1.AutoSize = true;
      this.lblCreated1.Location = new System.Drawing.Point(96, 268);
      this.lblCreated1.Name = "lblCreated1";
      this.lblCreated1.Size = new System.Drawing.Size(46, 13);
      this.lblCreated1.TabIndex = 10;
      this.lblCreated1.Text = "Created";
      // 
      // lblCreated
      // 
      this.lblCreated.AutoSize = true;
      this.lblCreated.Location = new System.Drawing.Point(16, 268);
      this.lblCreated.Name = "lblCreated";
      this.lblCreated.Size = new System.Drawing.Size(46, 13);
      this.lblCreated.TabIndex = 10;
      this.lblCreated.Text = "Created";
      // 
      // lblPicture
      // 
      this.lblPicture.AutoSize = true;
      this.lblPicture.Location = new System.Drawing.Point(16, 116);
      this.lblPicture.Name = "lblPicture";
      this.lblPicture.Size = new System.Drawing.Size(40, 13);
      this.lblPicture.TabIndex = 10;
      this.lblPicture.Text = "Picture";
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(96, 16);
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(204, 20);
      this.tbName.TabIndex = 0;
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(16, 20);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(34, 13);
      this.lblName.TabIndex = 10;
      this.lblName.Text = "Name";
      // 
      // tbAuthor
      // 
      this.tbAuthor.Location = new System.Drawing.Point(96, 40);
      this.tbAuthor.Name = "tbAuthor";
      this.tbAuthor.Size = new System.Drawing.Size(204, 20);
      this.tbAuthor.TabIndex = 1;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(16, 92);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(60, 13);
      this.lblDescription.TabIndex = 10;
      this.lblDescription.Text = "Description";
      // 
      // tbVersion
      // 
      this.tbVersion.Location = new System.Drawing.Point(96, 64);
      this.tbVersion.Name = "tbVersion";
      this.tbVersion.Size = new System.Drawing.Size(204, 20);
      this.tbVersion.TabIndex = 2;
      // 
      // lblVersion
      // 
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new System.Drawing.Point(16, 68);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(42, 13);
      this.lblVersion.TabIndex = 10;
      this.lblVersion.Text = "Version";
      // 
      // lblAuthor
      // 
      this.lblAuthor.AutoSize = true;
      this.lblAuthor.Location = new System.Drawing.Point(16, 44);
      this.lblAuthor.Name = "lblAuthor";
      this.lblAuthor.Size = new System.Drawing.Size(40, 13);
      this.lblAuthor.TabIndex = 10;
      this.lblAuthor.Text = "Author";
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(212, 272);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(88, 23);
      this.btnAdd.TabIndex = 6;
      this.btnAdd.Text = "Add...";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // tbRefAssemblies
      // 
      this.tbRefAssemblies.AcceptsReturn = true;
      this.tbRefAssemblies.Location = new System.Drawing.Point(16, 156);
      this.tbRefAssemblies.Multiline = true;
      this.tbRefAssemblies.Name = "tbRefAssemblies";
      this.tbRefAssemblies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbRefAssemblies.Size = new System.Drawing.Size(284, 104);
      this.tbRefAssemblies.TabIndex = 5;
      // 
      // lblRefAssemblies
      // 
      this.lblRefAssemblies.AutoSize = true;
      this.lblRefAssemblies.Location = new System.Drawing.Point(16, 136);
      this.lblRefAssemblies.Name = "lblRefAssemblies";
      this.lblRefAssemblies.Size = new System.Drawing.Size(121, 13);
      this.lblRefAssemblies.TabIndex = 4;
      this.lblRefAssemblies.Text = "Referenced assemblies:";
      // 
      // lblLanguage
      // 
      this.lblLanguage.AutoSize = true;
      this.lblLanguage.Location = new System.Drawing.Point(16, 16);
      this.lblLanguage.Name = "lblLanguage";
      this.lblLanguage.Size = new System.Drawing.Size(85, 13);
      this.lblLanguage.TabIndex = 3;
      this.lblLanguage.Text = "Script language:";
      // 
      // lblScriptNote
      // 
      this.lblScriptNote.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblScriptNote.Location = new System.Drawing.Point(16, 92);
      this.lblScriptNote.Name = "lblScriptNote";
      this.lblScriptNote.Size = new System.Drawing.Size(284, 28);
      this.lblScriptNote.TabIndex = 1;
      this.lblScriptNote.Text = "Note: if you change the language, entire script will be cleared!";
      // 
      // rbVB
      // 
      this.rbVB.AutoSize = true;
      this.rbVB.Location = new System.Drawing.Point(24, 64);
      this.rbVB.Name = "rbVB";
      this.rbVB.Size = new System.Drawing.Size(99, 17);
      this.rbVB.TabIndex = 0;
      this.rbVB.TabStop = true;
      this.rbVB.Text = "VisualBasic.NET";
      this.rbVB.UseVisualStyleBackColor = true;
      // 
      // rbC
      // 
      this.rbC.AutoSize = true;
      this.rbC.Location = new System.Drawing.Point(24, 40);
      this.rbC.Name = "rbC";
      this.rbC.Size = new System.Drawing.Size(40, 17);
      this.rbC.TabIndex = 0;
      this.rbC.TabStop = true;
      this.rbC.Text = "C#";
      this.rbC.UseVisualStyleBackColor = true;
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(16, 20);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(57, 13);
      this.lblPassword.TabIndex = 0;
      this.lblPassword.Text = "Password:";
      // 
      // tbRetypePassword
      // 
      this.tbRetypePassword.Location = new System.Drawing.Point(140, 40);
      this.tbRetypePassword.Name = "tbRetypePassword";
      this.tbRetypePassword.Size = new System.Drawing.Size(160, 20);
      this.tbRetypePassword.TabIndex = 1;
      this.tbRetypePassword.UseSystemPasswordChar = true;
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(140, 16);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(160, 20);
      this.tbPassword.TabIndex = 0;
      this.tbPassword.UseSystemPasswordChar = true;
      this.tbPassword.TextChanged += new System.EventHandler(this.tbPasswordLoad_TextChanged);
      // 
      // lblRetypePassword
      // 
      this.lblRetypePassword.AutoSize = true;
      this.lblRetypePassword.Location = new System.Drawing.Point(16, 44);
      this.lblRetypePassword.Name = "lblRetypePassword";
      this.lblRetypePassword.Size = new System.Drawing.Size(95, 13);
      this.lblRetypePassword.TabIndex = 0;
      this.lblRetypePassword.Text = "Retype password:";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Enabled = false;
      this.btnBrowse.Location = new System.Drawing.Point(40, 156);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(92, 23);
      this.btnBrowse.TabIndex = 3;
      this.btnBrowse.Text = "Browse...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Visible = false;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // rbInherit
      // 
      this.rbInherit.AutoSize = true;
      this.rbInherit.Location = new System.Drawing.Point(24, 132);
      this.rbInherit.Name = "rbInherit";
      this.rbInherit.Size = new System.Drawing.Size(138, 17);
      this.rbInherit.TabIndex = 2;
      this.rbInherit.Text = "Inherit the report from:";
      this.rbInherit.UseVisualStyleBackColor = true;
      this.rbInherit.Visible = false;
      this.rbInherit.CheckedChanged += new System.EventHandler(this.rbInherit_CheckedChanged);
      // 
      // rbDetach
      // 
      this.rbDetach.AutoSize = true;
      this.rbDetach.Location = new System.Drawing.Point(24, 108);
      this.rbDetach.Name = "rbDetach";
      this.rbDetach.Size = new System.Drawing.Size(137, 17);
      this.rbDetach.TabIndex = 2;
      this.rbDetach.Text = "Detach the base report";
      this.rbDetach.UseVisualStyleBackColor = true;
      // 
      // rbDontChange
      // 
      this.rbDontChange.AutoSize = true;
      this.rbDontChange.Checked = true;
      this.rbDontChange.Location = new System.Drawing.Point(24, 84);
      this.rbDontChange.Name = "rbDontChange";
      this.rbDontChange.Size = new System.Drawing.Size(95, 17);
      this.rbDontChange.TabIndex = 2;
      this.rbDontChange.TabStop = true;
      this.rbDontChange.Text = "Do not change";
      this.rbDontChange.UseVisualStyleBackColor = true;
      // 
      // lblChooseInheritance
      // 
      this.lblChooseInheritance.AutoSize = true;
      this.lblChooseInheritance.Location = new System.Drawing.Point(16, 60);
      this.lblChooseInheritance.Name = "lblChooseInheritance";
      this.lblChooseInheritance.Size = new System.Drawing.Size(99, 13);
      this.lblChooseInheritance.TabIndex = 1;
      this.lblChooseInheritance.Text = "Choose the option:";
      // 
      // lblBaseName
      // 
      this.lblBaseName.Location = new System.Drawing.Point(32, 184);
      this.lblBaseName.Name = "lblBaseName";
      this.lblBaseName.Size = new System.Drawing.Size(268, 36);
      this.lblBaseName.TabIndex = 0;
      this.lblBaseName.Text = "   ";
      this.lblBaseName.Visible = false;
      // 
      // lblInheritance
      // 
      this.lblInheritance.Location = new System.Drawing.Point(16, 16);
      this.lblInheritance.Name = "lblInheritance";
      this.lblInheritance.Size = new System.Drawing.Size(284, 36);
      this.lblInheritance.TabIndex = 0;
      this.lblInheritance.Text = "The report is not inherited.";
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.pnGeneral);
      this.pcPages.Controls.Add(this.pnDescription);
      this.pcPages.Controls.Add(this.pnScript);
      this.pcPages.Controls.Add(this.pnSecurity);
      this.pcPages.Controls.Add(this.pnInheritance);
      this.pcPages.Controls.Add(this.pnEmail);
      this.pcPages.Location = new System.Drawing.Point(12, 12);
      this.pcPages.Name = "pcPages";
      this.pcPages.SelectorWidth = 119;
      this.pcPages.Size = new System.Drawing.Size(436, 312);
      this.pcPages.TabIndex = 1;
      this.pcPages.Text = "pageControl1";
      // 
      // pnGeneral
      // 
      this.pnGeneral.BackColor = System.Drawing.SystemColors.Window;
      this.pnGeneral.Controls.Add(this.cbxTextQuality);
      this.pnGeneral.Controls.Add(this.lblTextQuality);
      this.pnGeneral.Controls.Add(this.cbDoublePass);
      this.pnGeneral.Controls.Add(this.cbConvertNulls);
      this.pnGeneral.Controls.Add(this.cbCompress);
      this.pnGeneral.Controls.Add(this.cbSmoothGraphics);
      this.pnGeneral.Controls.Add(this.cbUseFileCache);
      this.pnGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnGeneral.Location = new System.Drawing.Point(119, 1);
      this.pnGeneral.Name = "pnGeneral";
      this.pnGeneral.Size = new System.Drawing.Size(316, 310);
      this.pnGeneral.TabIndex = 4;
      this.pnGeneral.Text = "General";
      // 
      // cbxTextQuality
      // 
      this.cbxTextQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxTextQuality.FormattingEnabled = true;
      this.cbxTextQuality.Location = new System.Drawing.Point(132, 112);
      this.cbxTextQuality.Name = "cbxTextQuality";
      this.cbxTextQuality.Size = new System.Drawing.Size(168, 21);
      this.cbxTextQuality.TabIndex = 7;
      // 
      // lblTextQuality
      // 
      this.lblTextQuality.AutoSize = true;
      this.lblTextQuality.Location = new System.Drawing.Point(16, 116);
      this.lblTextQuality.Name = "lblTextQuality";
      this.lblTextQuality.Size = new System.Drawing.Size(68, 13);
      this.lblTextQuality.TabIndex = 6;
      this.lblTextQuality.Text = "Text quality:";
      // 
      // cbDoublePass
      // 
      this.cbDoublePass.AutoSize = true;
      this.cbDoublePass.Location = new System.Drawing.Point(16, 16);
      this.cbDoublePass.Name = "cbDoublePass";
      this.cbDoublePass.Size = new System.Drawing.Size(84, 17);
      this.cbDoublePass.TabIndex = 0;
      this.cbDoublePass.Text = "Double pass";
      this.cbDoublePass.UseVisualStyleBackColor = true;
      // 
      // cbConvertNulls
      // 
      this.cbConvertNulls.AutoSize = true;
      this.cbConvertNulls.Location = new System.Drawing.Point(16, 88);
      this.cbConvertNulls.Name = "cbConvertNulls";
      this.cbConvertNulls.Size = new System.Drawing.Size(118, 17);
      this.cbConvertNulls.TabIndex = 3;
      this.cbConvertNulls.Text = "Convert null values";
      this.cbConvertNulls.UseVisualStyleBackColor = true;
      // 
      // cbCompress
      // 
      this.cbCompress.AutoSize = true;
      this.cbCompress.Location = new System.Drawing.Point(16, 40);
      this.cbCompress.Name = "cbCompress";
      this.cbCompress.Size = new System.Drawing.Size(123, 17);
      this.cbCompress.TabIndex = 1;
      this.cbCompress.Text = "Compress report file";
      this.cbCompress.UseVisualStyleBackColor = true;
      // 
      // cbSmoothGraphics
      // 
      this.cbSmoothGraphics.AutoSize = true;
      this.cbSmoothGraphics.Location = new System.Drawing.Point(16, 140);
      this.cbSmoothGraphics.Name = "cbSmoothGraphics";
      this.cbSmoothGraphics.Size = new System.Drawing.Size(105, 17);
      this.cbSmoothGraphics.TabIndex = 5;
      this.cbSmoothGraphics.Text = "Smooth graphics";
      this.cbSmoothGraphics.UseVisualStyleBackColor = true;
      // 
      // cbUseFileCache
      // 
      this.cbUseFileCache.AutoSize = true;
      this.cbUseFileCache.Location = new System.Drawing.Point(16, 64);
      this.cbUseFileCache.Name = "cbUseFileCache";
      this.cbUseFileCache.Size = new System.Drawing.Size(92, 17);
      this.cbUseFileCache.TabIndex = 2;
      this.cbUseFileCache.Text = "Use file cache";
      this.cbUseFileCache.UseVisualStyleBackColor = true;
      // 
      // pnDescription
      // 
      this.pnDescription.BackColor = System.Drawing.SystemColors.Window;
      this.pnDescription.Controls.Add(this.tbDescription);
      this.pnDescription.Controls.Add(this.cbSavePreviewPicture);
      this.pnDescription.Controls.Add(this.lblName);
      this.pnDescription.Controls.Add(this.btnClear);
      this.pnDescription.Controls.Add(this.lblAuthor);
      this.pnDescription.Controls.Add(this.btnLoad);
      this.pnDescription.Controls.Add(this.lblVersion);
      this.pnDescription.Controls.Add(this.pbPicture);
      this.pnDescription.Controls.Add(this.tbVersion);
      this.pnDescription.Controls.Add(this.lblModified1);
      this.pnDescription.Controls.Add(this.lblDescription);
      this.pnDescription.Controls.Add(this.lblModified);
      this.pnDescription.Controls.Add(this.tbAuthor);
      this.pnDescription.Controls.Add(this.lblCreated1);
      this.pnDescription.Controls.Add(this.tbName);
      this.pnDescription.Controls.Add(this.lblCreated);
      this.pnDescription.Controls.Add(this.lblPicture);
      this.pnDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnDescription.Location = new System.Drawing.Point(119, 1);
      this.pnDescription.Name = "pnDescription";
      this.pnDescription.Size = new System.Drawing.Size(316, 310);
      this.pnDescription.TabIndex = 3;
      this.pnDescription.Text = "Description";
      // 
      // pnScript
      // 
      this.pnScript.BackColor = System.Drawing.SystemColors.Window;
      this.pnScript.Controls.Add(this.btnAdd);
      this.pnScript.Controls.Add(this.tbRefAssemblies);
      this.pnScript.Controls.Add(this.lblLanguage);
      this.pnScript.Controls.Add(this.lblRefAssemblies);
      this.pnScript.Controls.Add(this.rbC);
      this.pnScript.Controls.Add(this.rbVB);
      this.pnScript.Controls.Add(this.lblScriptNote);
      this.pnScript.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnScript.Location = new System.Drawing.Point(119, 1);
      this.pnScript.Name = "pnScript";
      this.pnScript.Size = new System.Drawing.Size(316, 310);
      this.pnScript.TabIndex = 0;
      this.pnScript.Text = "Script";
      // 
      // pnSecurity
      // 
      this.pnSecurity.BackColor = System.Drawing.SystemColors.Window;
      this.pnSecurity.Controls.Add(this.lblPassword);
      this.pnSecurity.Controls.Add(this.tbRetypePassword);
      this.pnSecurity.Controls.Add(this.tbPassword);
      this.pnSecurity.Controls.Add(this.lblRetypePassword);
      this.pnSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnSecurity.Location = new System.Drawing.Point(119, 1);
      this.pnSecurity.Name = "pnSecurity";
      this.pnSecurity.Size = new System.Drawing.Size(316, 310);
      this.pnSecurity.TabIndex = 1;
      this.pnSecurity.Text = "Security";
      // 
      // pnInheritance
      // 
      this.pnInheritance.BackColor = System.Drawing.SystemColors.Window;
      this.pnInheritance.Controls.Add(this.btnBrowse);
      this.pnInheritance.Controls.Add(this.rbInherit);
      this.pnInheritance.Controls.Add(this.lblInheritance);
      this.pnInheritance.Controls.Add(this.rbDetach);
      this.pnInheritance.Controls.Add(this.lblBaseName);
      this.pnInheritance.Controls.Add(this.rbDontChange);
      this.pnInheritance.Controls.Add(this.lblChooseInheritance);
      this.pnInheritance.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnInheritance.Location = new System.Drawing.Point(119, 1);
      this.pnInheritance.Name = "pnInheritance";
      this.pnInheritance.Size = new System.Drawing.Size(316, 310);
      this.pnInheritance.TabIndex = 2;
      this.pnInheritance.Text = "Inheritance";
      // 
      // pnEmail
      // 
      this.pnEmail.BackColor = System.Drawing.SystemColors.Window;
      this.pnEmail.Controls.Add(this.tbSubject);
      this.pnEmail.Controls.Add(this.lblSubject);
      this.pnEmail.Controls.Add(this.tbMessage);
      this.pnEmail.Controls.Add(this.lblMessage);
      this.pnEmail.Controls.Add(this.tbRecipients);
      this.pnEmail.Controls.Add(this.lblRecipients);
      this.pnEmail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnEmail.Location = new System.Drawing.Point(119, 1);
      this.pnEmail.Name = "pnEmail";
      this.pnEmail.Size = new System.Drawing.Size(316, 310);
      this.pnEmail.TabIndex = 5;
      this.pnEmail.Text = "Email";
      // 
      // tbSubject
      // 
      this.tbSubject.Location = new System.Drawing.Point(16, 112);
      this.tbSubject.Name = "tbSubject";
      this.tbSubject.Size = new System.Drawing.Size(284, 20);
      this.tbSubject.TabIndex = 3;
      // 
      // lblSubject
      // 
      this.lblSubject.AutoSize = true;
      this.lblSubject.Location = new System.Drawing.Point(16, 92);
      this.lblSubject.Name = "lblSubject";
      this.lblSubject.Size = new System.Drawing.Size(47, 13);
      this.lblSubject.TabIndex = 2;
      this.lblSubject.Text = "Subject:";
      // 
      // tbMessage
      // 
      this.tbMessage.AcceptsReturn = true;
      this.tbMessage.AcceptsTab = true;
      this.tbMessage.Location = new System.Drawing.Point(16, 160);
      this.tbMessage.Multiline = true;
      this.tbMessage.Name = "tbMessage";
      this.tbMessage.Size = new System.Drawing.Size(284, 132);
      this.tbMessage.TabIndex = 1;
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new System.Drawing.Point(16, 140);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(53, 13);
      this.lblMessage.TabIndex = 0;
      this.lblMessage.Text = "Message:";
      // 
      // tbRecipients
      // 
      this.tbRecipients.AcceptsReturn = true;
      this.tbRecipients.AcceptsTab = true;
      this.tbRecipients.Location = new System.Drawing.Point(16, 36);
      this.tbRecipients.Multiline = true;
      this.tbRecipients.Name = "tbRecipients";
      this.tbRecipients.Size = new System.Drawing.Size(284, 48);
      this.tbRecipients.TabIndex = 1;
      // 
      // lblRecipients
      // 
      this.lblRecipients.AutoSize = true;
      this.lblRecipients.Location = new System.Drawing.Point(16, 16);
      this.lblRecipients.Name = "lblRecipients";
      this.lblRecipients.Size = new System.Drawing.Size(209, 13);
      this.lblRecipients.TabIndex = 0;
      this.lblRecipients.Text = "Recipient(s) (for example, john@url.com):";
      // 
      // ReportOptionsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(458, 370);
      this.Controls.Add(this.pcPages);
      this.Name = "ReportOptionsForm";
      this.Text = "Report Settings";
      this.Shown += new System.EventHandler(this.ReportOptionsForm_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportOptionsForm_FormClosing);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pcPages, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
      this.pcPages.ResumeLayout(false);
      this.pnGeneral.ResumeLayout(false);
      this.pnGeneral.PerformLayout();
      this.pnDescription.ResumeLayout(false);
      this.pnDescription.PerformLayout();
      this.pnScript.ResumeLayout(false);
      this.pnScript.PerformLayout();
      this.pnSecurity.ResumeLayout(false);
      this.pnSecurity.PerformLayout();
      this.pnInheritance.ResumeLayout(false);
      this.pnInheritance.PerformLayout();
      this.pnEmail.ResumeLayout(false);
      this.pnEmail.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblAuthor;
    private System.Windows.Forms.TextBox tbAuthor;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblVersion;
    private System.Windows.Forms.TextBox tbVersion;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.PictureBox pbPicture;
    private System.Windows.Forms.Label lblPicture;
    private System.Windows.Forms.TextBox tbRetypePassword;
    private System.Windows.Forms.Label lblRetypePassword;
    private System.Windows.Forms.TextBox tbPassword;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblCreated;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.RadioButton rbInherit;
    private System.Windows.Forms.RadioButton rbDetach;
    private System.Windows.Forms.RadioButton rbDontChange;
    private System.Windows.Forms.Label lblChooseInheritance;
    private System.Windows.Forms.Label lblInheritance;
    private System.Windows.Forms.Label lblLanguage;
    private System.Windows.Forms.Label lblScriptNote;
    private System.Windows.Forms.RadioButton rbVB;
    private System.Windows.Forms.RadioButton rbC;
    private System.Windows.Forms.Label lblModified;
    private System.Windows.Forms.Label lblCreated1;
    private System.Windows.Forms.Label lblModified1;
    private System.Windows.Forms.Label lblBaseName;
    private FastReport.Controls.PageControl pcPages;
    private FastReport.Controls.PageControlPage pnDescription;
    private FastReport.Controls.PageControlPage pnScript;
    private FastReport.Controls.PageControlPage pnSecurity;
    private FastReport.Controls.PageControlPage pnInheritance;
    private FastReport.Controls.PageControlPage pnGeneral;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.TextBox tbRefAssemblies;
    private System.Windows.Forms.Label lblRefAssemblies;
    private System.Windows.Forms.CheckBox cbUseFileCache;
    private System.Windows.Forms.CheckBox cbCompress;
    private System.Windows.Forms.CheckBox cbSmoothGraphics;
    private System.Windows.Forms.CheckBox cbConvertNulls;
    private System.Windows.Forms.CheckBox cbDoublePass;
    private System.Windows.Forms.ComboBox cbxTextQuality;
    private System.Windows.Forms.Label lblTextQuality;
    private System.Windows.Forms.CheckBox cbSavePreviewPicture;
    private FastReport.Controls.TextBoxButton tbDescription;
    private FastReport.Controls.PageControlPage pnEmail;
    private System.Windows.Forms.TextBox tbRecipients;
    private System.Windows.Forms.Label lblRecipients;
    private System.Windows.Forms.TextBox tbSubject;
    private System.Windows.Forms.Label lblSubject;
    private System.Windows.Forms.TextBox tbMessage;
    private System.Windows.Forms.Label lblMessage;

  }
}
