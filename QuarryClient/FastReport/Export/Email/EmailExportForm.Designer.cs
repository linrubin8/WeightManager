namespace FastReport.Export.Email
{
  partial class EmailExportForm
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
      this.pageControl1 = new FastReport.Controls.PageControl();
      this.pgEmail = new FastReport.Controls.PageControlPage();
      this.btnSettings = new System.Windows.Forms.Button();
      this.cbxAttachment = new System.Windows.Forms.ComboBox();
      this.lblAttachment = new System.Windows.Forms.Label();
      this.tbMessage = new System.Windows.Forms.TextBox();
      this.lblMessage = new System.Windows.Forms.Label();
      this.cbxSubject = new System.Windows.Forms.ComboBox();
      this.lblSubject = new System.Windows.Forms.Label();
      this.cbxAddressTo = new System.Windows.Forms.ComboBox();
      this.lblAsterisk1 = new System.Windows.Forms.Label();
      this.lblAddressTo = new System.Windows.Forms.Label();
      this.pgAccount = new FastReport.Controls.PageControlPage();
      this.cbEnableSSL = new System.Windows.Forms.CheckBox();
      this.tbPassword = new System.Windows.Forms.TextBox();
      this.tbUserName = new System.Windows.Forms.TextBox();
      this.udPort = new System.Windows.Forms.NumericUpDown();
      this.tbHost = new System.Windows.Forms.TextBox();
      this.tbTemplate = new System.Windows.Forms.TextBox();
      this.tbName = new System.Windows.Forms.TextBox();
      this.tbAddressFrom = new System.Windows.Forms.TextBox();
      this.lblAsterisk3 = new System.Windows.Forms.Label();
      this.lblAsterisk2 = new System.Windows.Forms.Label();
      this.lblPassword = new System.Windows.Forms.Label();
      this.lblUserName = new System.Windows.Forms.Label();
      this.labelLine1 = new FastReport.Controls.LabelLine();
      this.lblPort = new System.Windows.Forms.Label();
      this.lblHost = new System.Windows.Forms.Label();
      this.lblTemplate = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.lblAddressFrom = new System.Windows.Forms.Label();
      this.lblCC = new System.Windows.Forms.Label();
      this.tbCC = new System.Windows.Forms.TextBox();
      this.pageControl1.SuspendLayout();
      this.pgEmail.SuspendLayout();
      this.pgAccount.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPort)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(368, 316);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(448, 316);
      // 
      // pageControl1
      // 
      this.pageControl1.Controls.Add(this.pgEmail);
      this.pageControl1.Controls.Add(this.pgAccount);
      this.pageControl1.HighlightPageIndex = -1;
      this.pageControl1.Location = new System.Drawing.Point(12, 12);
      this.pageControl1.Name = "pageControl1";
      this.pageControl1.SelectorWidth = 100;
      this.pageControl1.Size = new System.Drawing.Size(512, 288);
      this.pageControl1.TabIndex = 1;
      this.pageControl1.Text = "pageControl1";
      // 
      // pgEmail
      // 
      this.pgEmail.BackColor = System.Drawing.SystemColors.Window;
      this.pgEmail.Controls.Add(this.tbCC);
      this.pgEmail.Controls.Add(this.lblCC);
      this.pgEmail.Controls.Add(this.btnSettings);
      this.pgEmail.Controls.Add(this.cbxAttachment);
      this.pgEmail.Controls.Add(this.lblAttachment);
      this.pgEmail.Controls.Add(this.tbMessage);
      this.pgEmail.Controls.Add(this.lblMessage);
      this.pgEmail.Controls.Add(this.cbxSubject);
      this.pgEmail.Controls.Add(this.lblSubject);
      this.pgEmail.Controls.Add(this.cbxAddressTo);
      this.pgEmail.Controls.Add(this.lblAsterisk1);
      this.pgEmail.Controls.Add(this.lblAddressTo);
      this.pgEmail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pgEmail.Location = new System.Drawing.Point(100, 1);
      this.pgEmail.Name = "pgEmail";
      this.pgEmail.Size = new System.Drawing.Size(411, 286);
      this.pgEmail.TabIndex = 0;
      this.pgEmail.Text = "Email";
      // 
      // btnSettings
      // 
      this.btnSettings.Location = new System.Drawing.Point(304, 248);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new System.Drawing.Size(92, 23);
      this.btnSettings.TabIndex = 4;
      this.btnSettings.Text = "Settings...";
      this.btnSettings.UseVisualStyleBackColor = true;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
      // 
      // cbxAttachment
      // 
      this.cbxAttachment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxAttachment.FormattingEnabled = true;
      this.cbxAttachment.Location = new System.Drawing.Point(108, 248);
      this.cbxAttachment.Name = "cbxAttachment";
      this.cbxAttachment.Size = new System.Drawing.Size(188, 21);
      this.cbxAttachment.TabIndex = 3;
      this.cbxAttachment.SelectedIndexChanged += new System.EventHandler(this.cbxAttachment_SelectedIndexChanged);
      // 
      // lblAttachment
      // 
      this.lblAttachment.AutoSize = true;
      this.lblAttachment.Location = new System.Drawing.Point(16, 252);
      this.lblAttachment.Name = "lblAttachment";
      this.lblAttachment.Size = new System.Drawing.Size(67, 13);
      this.lblAttachment.TabIndex = 4;
      this.lblAttachment.Text = "Attachment:";
      // 
      // tbMessage
      // 
      this.tbMessage.AcceptsReturn = true;
      this.tbMessage.Location = new System.Drawing.Point(108, 100);
      this.tbMessage.Multiline = true;
      this.tbMessage.Name = "tbMessage";
      this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbMessage.Size = new System.Drawing.Size(288, 140);
      this.tbMessage.TabIndex = 2;
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new System.Drawing.Point(16, 104);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(53, 13);
      this.lblMessage.TabIndex = 2;
      this.lblMessage.Text = "Message:";
      // 
      // cbxSubject
      // 
      this.cbxSubject.FormattingEnabled = true;
      this.cbxSubject.Location = new System.Drawing.Point(108, 72);
      this.cbxSubject.Name = "cbxSubject";
      this.cbxSubject.Size = new System.Drawing.Size(288, 21);
      this.cbxSubject.TabIndex = 1;
      // 
      // lblSubject
      // 
      this.lblSubject.AutoSize = true;
      this.lblSubject.Location = new System.Drawing.Point(16, 76);
      this.lblSubject.Name = "lblSubject";
      this.lblSubject.Size = new System.Drawing.Size(47, 13);
      this.lblSubject.TabIndex = 0;
      this.lblSubject.Text = "Subject:";
      // 
      // cbxAddressTo
      // 
      this.cbxAddressTo.FormattingEnabled = true;
      this.cbxAddressTo.Location = new System.Drawing.Point(108, 16);
      this.cbxAddressTo.Name = "cbxAddressTo";
      this.cbxAddressTo.Size = new System.Drawing.Size(288, 21);
      this.cbxAddressTo.TabIndex = 0;
      // 
      // lblAsterisk1
      // 
      this.lblAsterisk1.AutoSize = true;
      this.lblAsterisk1.ForeColor = System.Drawing.Color.Red;
      this.lblAsterisk1.Location = new System.Drawing.Point(96, 20);
      this.lblAsterisk1.Name = "lblAsterisk1";
      this.lblAsterisk1.Size = new System.Drawing.Size(13, 13);
      this.lblAsterisk1.TabIndex = 0;
      this.lblAsterisk1.Text = "*";
      // 
      // lblAddressTo
      // 
      this.lblAddressTo.AutoSize = true;
      this.lblAddressTo.Location = new System.Drawing.Point(16, 20);
      this.lblAddressTo.Name = "lblAddressTo";
      this.lblAddressTo.Size = new System.Drawing.Size(50, 13);
      this.lblAddressTo.TabIndex = 0;
      this.lblAddressTo.Text = "Address:";
      // 
      // pgAccount
      // 
      this.pgAccount.BackColor = System.Drawing.SystemColors.Window;
      this.pgAccount.Controls.Add(this.cbEnableSSL);
      this.pgAccount.Controls.Add(this.tbPassword);
      this.pgAccount.Controls.Add(this.tbUserName);
      this.pgAccount.Controls.Add(this.udPort);
      this.pgAccount.Controls.Add(this.tbHost);
      this.pgAccount.Controls.Add(this.tbTemplate);
      this.pgAccount.Controls.Add(this.tbName);
      this.pgAccount.Controls.Add(this.tbAddressFrom);
      this.pgAccount.Controls.Add(this.lblAsterisk3);
      this.pgAccount.Controls.Add(this.lblAsterisk2);
      this.pgAccount.Controls.Add(this.lblPassword);
      this.pgAccount.Controls.Add(this.lblUserName);
      this.pgAccount.Controls.Add(this.labelLine1);
      this.pgAccount.Controls.Add(this.lblPort);
      this.pgAccount.Controls.Add(this.lblHost);
      this.pgAccount.Controls.Add(this.lblTemplate);
      this.pgAccount.Controls.Add(this.lblName);
      this.pgAccount.Controls.Add(this.lblAddressFrom);
      this.pgAccount.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pgAccount.Location = new System.Drawing.Point(100, 1);
      this.pgAccount.Name = "pgAccount";
      this.pgAccount.Size = new System.Drawing.Size(411, 286);
      this.pgAccount.TabIndex = 1;
      this.pgAccount.Text = "Account";
      // 
      // cbEnableSSL
      // 
      this.cbEnableSSL.AutoSize = true;
      this.cbEnableSSL.Location = new System.Drawing.Point(108, 256);
      this.cbEnableSSL.Name = "cbEnableSSL";
      this.cbEnableSSL.Size = new System.Drawing.Size(78, 17);
      this.cbEnableSSL.TabIndex = 12;
      this.cbEnableSSL.Text = "Enable SSL";
      this.cbEnableSSL.UseVisualStyleBackColor = true;
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(312, 228);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(84, 20);
      this.tbPassword.TabIndex = 6;
      this.tbPassword.UseSystemPasswordChar = true;
      // 
      // tbUserName
      // 
      this.tbUserName.Location = new System.Drawing.Point(108, 228);
      this.tbUserName.Name = "tbUserName";
      this.tbUserName.Size = new System.Drawing.Size(120, 20);
      this.tbUserName.TabIndex = 5;
      // 
      // udPort
      // 
      this.udPort.Location = new System.Drawing.Point(336, 200);
      this.udPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.udPort.Name = "udPort";
      this.udPort.Size = new System.Drawing.Size(60, 20);
      this.udPort.TabIndex = 4;
      this.udPort.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
      // 
      // tbHost
      // 
      this.tbHost.Location = new System.Drawing.Point(108, 200);
      this.tbHost.Name = "tbHost";
      this.tbHost.Size = new System.Drawing.Size(168, 20);
      this.tbHost.TabIndex = 3;
      // 
      // tbTemplate
      // 
      this.tbTemplate.AcceptsReturn = true;
      this.tbTemplate.Location = new System.Drawing.Point(108, 72);
      this.tbTemplate.Multiline = true;
      this.tbTemplate.Name = "tbTemplate";
      this.tbTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbTemplate.Size = new System.Drawing.Size(288, 96);
      this.tbTemplate.TabIndex = 2;
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(108, 44);
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(288, 20);
      this.tbName.TabIndex = 1;
      // 
      // tbAddressFrom
      // 
      this.tbAddressFrom.Location = new System.Drawing.Point(108, 16);
      this.tbAddressFrom.Name = "tbAddressFrom";
      this.tbAddressFrom.Size = new System.Drawing.Size(288, 20);
      this.tbAddressFrom.TabIndex = 0;
      // 
      // lblAsterisk3
      // 
      this.lblAsterisk3.AutoSize = true;
      this.lblAsterisk3.ForeColor = System.Drawing.Color.Red;
      this.lblAsterisk3.Location = new System.Drawing.Point(96, 204);
      this.lblAsterisk3.Name = "lblAsterisk3";
      this.lblAsterisk3.Size = new System.Drawing.Size(13, 13);
      this.lblAsterisk3.TabIndex = 11;
      this.lblAsterisk3.Text = "*";
      // 
      // lblAsterisk2
      // 
      this.lblAsterisk2.AutoSize = true;
      this.lblAsterisk2.ForeColor = System.Drawing.Color.Red;
      this.lblAsterisk2.Location = new System.Drawing.Point(96, 20);
      this.lblAsterisk2.Name = "lblAsterisk2";
      this.lblAsterisk2.Size = new System.Drawing.Size(13, 13);
      this.lblAsterisk2.TabIndex = 11;
      this.lblAsterisk2.Text = "*";
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(244, 232);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(57, 13);
      this.lblPassword.TabIndex = 9;
      this.lblPassword.Text = "Password:";
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(16, 232);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(62, 13);
      this.lblUserName.TabIndex = 9;
      this.lblUserName.Text = "User name:";
      // 
      // labelLine1
      // 
      this.labelLine1.Location = new System.Drawing.Point(16, 172);
      this.labelLine1.Name = "labelLine1";
      this.labelLine1.Size = new System.Drawing.Size(380, 23);
      this.labelLine1.TabIndex = 8;
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(292, 204);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(31, 13);
      this.lblPort.TabIndex = 5;
      this.lblPort.Text = "Port:";
      // 
      // lblHost
      // 
      this.lblHost.AutoSize = true;
      this.lblHost.Location = new System.Drawing.Point(16, 204);
      this.lblHost.Name = "lblHost";
      this.lblHost.Size = new System.Drawing.Size(33, 13);
      this.lblHost.TabIndex = 4;
      this.lblHost.Text = "Host:";
      // 
      // lblTemplate
      // 
      this.lblTemplate.AutoSize = true;
      this.lblTemplate.Location = new System.Drawing.Point(16, 76);
      this.lblTemplate.Name = "lblTemplate";
      this.lblTemplate.Size = new System.Drawing.Size(55, 13);
      this.lblTemplate.TabIndex = 2;
      this.lblTemplate.Text = "Template:";
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(16, 48);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(38, 13);
      this.lblName.TabIndex = 0;
      this.lblName.Text = "Name:";
      // 
      // lblAddressFrom
      // 
      this.lblAddressFrom.AutoSize = true;
      this.lblAddressFrom.Location = new System.Drawing.Point(16, 20);
      this.lblAddressFrom.Name = "lblAddressFrom";
      this.lblAddressFrom.Size = new System.Drawing.Size(50, 13);
      this.lblAddressFrom.TabIndex = 0;
      this.lblAddressFrom.Text = "Address:";
      // 
      // lblCC
      // 
      this.lblCC.AutoSize = true;
      this.lblCC.Location = new System.Drawing.Point(16, 48);
      this.lblCC.Name = "lblCC";
      this.lblCC.Size = new System.Drawing.Size(25, 13);
      this.lblCC.TabIndex = 5;
      this.lblCC.Text = "CC:";
      // 
      // tbCC
      // 
      this.tbCC.Location = new System.Drawing.Point(108, 44);
      this.tbCC.Name = "tbCC";
      this.tbCC.Size = new System.Drawing.Size(288, 20);
      this.tbCC.TabIndex = 6;
      // 
      // EmailExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(534, 350);
      this.Controls.Add(this.pageControl1);
      this.Name = "EmailExportForm";
      this.Text = "Send Email";
      this.Shown += new System.EventHandler(this.EmailExportForm_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmailExportForm_FormClosing);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pageControl1, 0);
      this.pageControl1.ResumeLayout(false);
      this.pgEmail.ResumeLayout(false);
      this.pgEmail.PerformLayout();
      this.pgAccount.ResumeLayout(false);
      this.pgAccount.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPort)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private FastReport.Controls.PageControlPage pgAccount;
    private FastReport.Controls.PageControlPage pgEmail;
    private System.Windows.Forms.ComboBox cbxSubject;
    private System.Windows.Forms.Label lblSubject;
    private System.Windows.Forms.ComboBox cbxAddressTo;
    private System.Windows.Forms.Label lblAddressTo;
    private FastReport.Controls.PageControl pageControl1;
    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.ComboBox cbxAttachment;
    private System.Windows.Forms.Label lblAttachment;
    private System.Windows.Forms.TextBox tbMessage;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox tbAddressFrom;
    private System.Windows.Forms.Label lblAddressFrom;
    private System.Windows.Forms.TextBox tbTemplate;
    private System.Windows.Forms.Label lblTemplate;
    private System.Windows.Forms.TextBox tbHost;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.NumericUpDown udPort;
    private FastReport.Controls.LabelLine labelLine1;
    private System.Windows.Forms.TextBox tbPassword;
    private System.Windows.Forms.TextBox tbUserName;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.Label lblAsterisk1;
    private System.Windows.Forms.Label lblAsterisk3;
    private System.Windows.Forms.Label lblAsterisk2;
    private System.Windows.Forms.CheckBox cbEnableSSL;
    private System.Windows.Forms.Label lblCC;
    private System.Windows.Forms.TextBox tbCC;
  }
}
