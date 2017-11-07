namespace FastReport.Data.ConnectionEditors
{
  partial class MsSqlConnectionEditor
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
      this.gbDatabase = new System.Windows.Forms.GroupBox();
      this.tbDatabaseFile = new FastReport.Controls.TextBoxButton();
      this.rbDatabaseFile = new System.Windows.Forms.RadioButton();
      this.cbxDatabaseName = new System.Windows.Forms.ComboBox();
      this.rbDatabaseName = new System.Windows.Forms.RadioButton();
      this.cbxServer = new System.Windows.Forms.ComboBox();
      this.lblServer = new System.Windows.Forms.Label();
      this.gbServerLogon = new System.Windows.Forms.GroupBox();
      this.cbSavePassword = new System.Windows.Forms.CheckBox();
      this.tbPassword = new System.Windows.Forms.TextBox();
      this.tbUserName = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.lblUserName = new System.Windows.Forms.Label();
      this.rbUseSql = new System.Windows.Forms.RadioButton();
      this.rbUseWindows = new System.Windows.Forms.RadioButton();
      this.labelLine1 = new FastReport.Controls.LabelLine();
      this.btnAdvanced = new System.Windows.Forms.Button();
      this.gbDatabase.SuspendLayout();
      this.gbServerLogon.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbDatabase
      // 
      this.gbDatabase.Controls.Add(this.tbDatabaseFile);
      this.gbDatabase.Controls.Add(this.rbDatabaseFile);
      this.gbDatabase.Controls.Add(this.cbxDatabaseName);
      this.gbDatabase.Controls.Add(this.rbDatabaseName);
      this.gbDatabase.Location = new System.Drawing.Point(8, 200);
      this.gbDatabase.Name = "gbDatabase";
      this.gbDatabase.Size = new System.Drawing.Size(320, 128);
      this.gbDatabase.TabIndex = 7;
      this.gbDatabase.TabStop = false;
      this.gbDatabase.Text = "Connect to a database";
      // 
      // tbDatabaseFile
      // 
      this.tbDatabaseFile.Image = null;
      this.tbDatabaseFile.Location = new System.Drawing.Point(32, 92);
      this.tbDatabaseFile.Name = "tbDatabaseFile";
      this.tbDatabaseFile.Size = new System.Drawing.Size(276, 21);
      this.tbDatabaseFile.TabIndex = 3;
      this.tbDatabaseFile.ButtonClick += new System.EventHandler(this.tbDatabaseFile_ButtonClick);
      // 
      // rbDatabaseFile
      // 
      this.rbDatabaseFile.AutoSize = true;
      this.rbDatabaseFile.Location = new System.Drawing.Point(12, 72);
      this.rbDatabaseFile.Name = "rbDatabaseFile";
      this.rbDatabaseFile.Size = new System.Drawing.Size(135, 17);
      this.rbDatabaseFile.TabIndex = 2;
      this.rbDatabaseFile.TabStop = true;
      this.rbDatabaseFile.Text = "Attach a database file:";
      this.rbDatabaseFile.UseVisualStyleBackColor = true;
      this.rbDatabaseFile.CheckedChanged += new System.EventHandler(this.UpdateControls);
      // 
      // cbxDatabaseName
      // 
      this.cbxDatabaseName.FormattingEnabled = true;
      this.cbxDatabaseName.Location = new System.Drawing.Point(32, 40);
      this.cbxDatabaseName.Name = "cbxDatabaseName";
      this.cbxDatabaseName.Size = new System.Drawing.Size(276, 21);
      this.cbxDatabaseName.TabIndex = 1;
      this.cbxDatabaseName.DropDown += new System.EventHandler(this.cbxDatabaseName_DropDown);
      // 
      // rbDatabaseName
      // 
      this.rbDatabaseName.AutoSize = true;
      this.rbDatabaseName.Location = new System.Drawing.Point(12, 20);
      this.rbDatabaseName.Name = "rbDatabaseName";
      this.rbDatabaseName.Size = new System.Drawing.Size(186, 17);
      this.rbDatabaseName.TabIndex = 0;
      this.rbDatabaseName.TabStop = true;
      this.rbDatabaseName.Text = "Select or enter a database name:";
      this.rbDatabaseName.UseVisualStyleBackColor = true;
      this.rbDatabaseName.CheckedChanged += new System.EventHandler(this.UpdateControls);
      // 
      // cbxServer
      // 
      this.cbxServer.FormattingEnabled = true;
      this.cbxServer.Location = new System.Drawing.Point(8, 24);
      this.cbxServer.Name = "cbxServer";
      this.cbxServer.Size = new System.Drawing.Size(320, 21);
      this.cbxServer.TabIndex = 6;
      this.cbxServer.TextChanged += new System.EventHandler(this.ServerOrLoginChanged);
      this.cbxServer.DropDown += new System.EventHandler(this.cbxServer_DropDown);
      // 
      // lblServer
      // 
      this.lblServer.AutoSize = true;
      this.lblServer.Location = new System.Drawing.Point(8, 8);
      this.lblServer.Name = "lblServer";
      this.lblServer.Size = new System.Drawing.Size(72, 13);
      this.lblServer.TabIndex = 5;
      this.lblServer.Text = "Server name:";
      // 
      // gbServerLogon
      // 
      this.gbServerLogon.Controls.Add(this.cbSavePassword);
      this.gbServerLogon.Controls.Add(this.tbPassword);
      this.gbServerLogon.Controls.Add(this.tbUserName);
      this.gbServerLogon.Controls.Add(this.lblPassword);
      this.gbServerLogon.Controls.Add(this.lblUserName);
      this.gbServerLogon.Controls.Add(this.rbUseSql);
      this.gbServerLogon.Controls.Add(this.rbUseWindows);
      this.gbServerLogon.Location = new System.Drawing.Point(8, 52);
      this.gbServerLogon.Name = "gbServerLogon";
      this.gbServerLogon.Size = new System.Drawing.Size(320, 144);
      this.gbServerLogon.TabIndex = 4;
      this.gbServerLogon.TabStop = false;
      this.gbServerLogon.Text = "Log on to the server";
      // 
      // cbSavePassword
      // 
      this.cbSavePassword.AutoSize = true;
      this.cbSavePassword.Location = new System.Drawing.Point(120, 116);
      this.cbSavePassword.Name = "cbSavePassword";
      this.cbSavePassword.Size = new System.Drawing.Size(116, 17);
      this.cbSavePassword.TabIndex = 4;
      this.cbSavePassword.Text = "Save my password";
      this.cbSavePassword.UseVisualStyleBackColor = true;
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(120, 92);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(188, 20);
      this.tbPassword.TabIndex = 3;
      this.tbPassword.UseSystemPasswordChar = true;
      this.tbPassword.TextChanged += new System.EventHandler(this.ServerOrLoginChanged);
      // 
      // tbUserName
      // 
      this.tbUserName.Location = new System.Drawing.Point(120, 68);
      this.tbUserName.Name = "tbUserName";
      this.tbUserName.Size = new System.Drawing.Size(188, 20);
      this.tbUserName.TabIndex = 3;
      this.tbUserName.TextChanged += new System.EventHandler(this.ServerOrLoginChanged);
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(28, 96);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(57, 13);
      this.lblPassword.TabIndex = 2;
      this.lblPassword.Text = "Password:";
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(28, 72);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(62, 13);
      this.lblUserName.TabIndex = 1;
      this.lblUserName.Text = "User name:";
      // 
      // rbUseSql
      // 
      this.rbUseSql.AutoSize = true;
      this.rbUseSql.Location = new System.Drawing.Point(12, 44);
      this.rbUseSql.Name = "rbUseSql";
      this.rbUseSql.Size = new System.Drawing.Size(173, 17);
      this.rbUseSql.TabIndex = 0;
      this.rbUseSql.TabStop = true;
      this.rbUseSql.Text = "Use SQL Server Authentication";
      this.rbUseSql.UseVisualStyleBackColor = true;
      this.rbUseSql.CheckedChanged += new System.EventHandler(this.ServerOrLoginChanged);
      // 
      // rbUseWindows
      // 
      this.rbUseWindows.AutoSize = true;
      this.rbUseWindows.Location = new System.Drawing.Point(12, 20);
      this.rbUseWindows.Name = "rbUseWindows";
      this.rbUseWindows.Size = new System.Drawing.Size(162, 17);
      this.rbUseWindows.TabIndex = 0;
      this.rbUseWindows.TabStop = true;
      this.rbUseWindows.Text = "Use Windows Authentication";
      this.rbUseWindows.UseVisualStyleBackColor = true;
      this.rbUseWindows.CheckedChanged += new System.EventHandler(this.ServerOrLoginChanged);
      // 
      // labelLine1
      // 
      this.labelLine1.Location = new System.Drawing.Point(8, 360);
      this.labelLine1.Name = "labelLine1";
      this.labelLine1.Size = new System.Drawing.Size(320, 16);
      this.labelLine1.TabIndex = 9;
      // 
      // btnAdvanced
      // 
      this.btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdvanced.AutoSize = true;
      this.btnAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAdvanced.Location = new System.Drawing.Point(250, 336);
      this.btnAdvanced.Name = "btnAdvanced";
      this.btnAdvanced.Size = new System.Drawing.Size(77, 23);
      this.btnAdvanced.TabIndex = 8;
      this.btnAdvanced.Text = "Advanced...";
      this.btnAdvanced.UseVisualStyleBackColor = true;
      this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
      // 
      // MsSqlConnectionEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.Controls.Add(this.labelLine1);
      this.Controls.Add(this.btnAdvanced);
      this.Controls.Add(this.gbDatabase);
      this.Controls.Add(this.cbxServer);
      this.Controls.Add(this.lblServer);
      this.Controls.Add(this.gbServerLogon);
      this.Name = "MsSqlConnectionEditor";
      this.Size = new System.Drawing.Size(336, 378);
      this.gbDatabase.ResumeLayout(false);
      this.gbDatabase.PerformLayout();
      this.gbServerLogon.ResumeLayout(false);
      this.gbServerLogon.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbDatabase;
    private FastReport.Controls.TextBoxButton tbDatabaseFile;
    private System.Windows.Forms.RadioButton rbDatabaseFile;
    private System.Windows.Forms.ComboBox cbxDatabaseName;
    private System.Windows.Forms.RadioButton rbDatabaseName;
    private System.Windows.Forms.ComboBox cbxServer;
    private System.Windows.Forms.Label lblServer;
    private System.Windows.Forms.GroupBox gbServerLogon;
    private System.Windows.Forms.CheckBox cbSavePassword;
    private System.Windows.Forms.TextBox tbPassword;
    private System.Windows.Forms.TextBox tbUserName;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.RadioButton rbUseSql;
    private System.Windows.Forms.RadioButton rbUseWindows;
    private FastReport.Controls.LabelLine labelLine1;
    private System.Windows.Forms.Button btnAdvanced;
  }
}
