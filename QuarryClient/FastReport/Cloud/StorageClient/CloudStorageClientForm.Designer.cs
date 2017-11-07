namespace FastReport.Cloud.StorageClient
{
    partial class CloudStorageClientForm
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
            this.pgFile = new FastReport.Controls.PageControlPage();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.cbFileType = new System.Windows.Forms.ComboBox();
            this.labelFileType = new System.Windows.Forms.Label();
            this.pgProxy = new FastReport.Controls.PageControlPage();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.labelColon = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.pageControl1.SuspendLayout();
            this.pgFile.SuspendLayout();
            this.pgProxy.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(368, 138);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(449, 138);
            // 
            // pageControl1
            // 
            this.pageControl1.Controls.Add(this.pgFile);
            this.pageControl1.Controls.Add(this.pgProxy);
            this.pageControl1.HighlightPageIndex = -1;
            this.pageControl1.Location = new System.Drawing.Point(12, 12);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.SelectorWidth = 100;
            this.pageControl1.Size = new System.Drawing.Size(512, 120);
            this.pageControl1.TabIndex = 19;
            this.pageControl1.Text = "pageControl1";
            // 
            // pgFile
            // 
            this.pgFile.BackColor = System.Drawing.SystemColors.Window;
            this.pgFile.Controls.Add(this.buttonSettings);
            this.pgFile.Controls.Add(this.cbFileType);
            this.pgFile.Controls.Add(this.labelFileType);
            this.pgFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgFile.Location = new System.Drawing.Point(100, 1);
            this.pgFile.Name = "pgFile";
            this.pgFile.Size = new System.Drawing.Size(411, 118);
            this.pgFile.TabIndex = 0;
            this.pgFile.Text = "File";
            // 
            // buttonSettings
            // 
            this.buttonSettings.Enabled = false;
            this.buttonSettings.Location = new System.Drawing.Point(313, 40);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(85, 23);
            this.buttonSettings.TabIndex = 2;
            this.buttonSettings.Text = "Settings...";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // cbFileType
            // 
            this.cbFileType.FormattingEnabled = true;
            this.cbFileType.Location = new System.Drawing.Point(178, 13);
            this.cbFileType.Name = "cbFileType";
            this.cbFileType.Size = new System.Drawing.Size(220, 21);
            this.cbFileType.TabIndex = 1;
            this.cbFileType.SelectedIndexChanged += new System.EventHandler(this.cbFileType_SelectedIndexChanged);
            // 
            // labelFileType
            // 
            this.labelFileType.AutoSize = true;
            this.labelFileType.Location = new System.Drawing.Point(22, 16);
            this.labelFileType.Name = "labelFileType";
            this.labelFileType.Size = new System.Drawing.Size(54, 13);
            this.labelFileType.TabIndex = 0;
            this.labelFileType.Text = "File Type:";
            // 
            // pgProxy
            // 
            this.pgProxy.BackColor = System.Drawing.SystemColors.Window;
            this.pgProxy.Controls.Add(this.tbPassword);
            this.pgProxy.Controls.Add(this.tbUsername);
            this.pgProxy.Controls.Add(this.tbPort);
            this.pgProxy.Controls.Add(this.tbServer);
            this.pgProxy.Controls.Add(this.labelColon);
            this.pgProxy.Controls.Add(this.labelPassword);
            this.pgProxy.Controls.Add(this.labelUsername);
            this.pgProxy.Controls.Add(this.labelServer);
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(100, 1);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.Size = new System.Drawing.Size(411, 118);
            this.pgProxy.TabIndex = 1;
            this.pgProxy.Text = "Proxy";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(126, 83);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(200, 20);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(126, 57);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(200, 20);
            this.tbUsername.TabIndex = 6;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(349, 13);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(50, 20);
            this.tbPort.TabIndex = 5;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(126, 13);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(200, 20);
            this.tbServer.TabIndex = 4;
            // 
            // labelColon
            // 
            this.labelColon.AutoSize = true;
            this.labelColon.Location = new System.Drawing.Point(332, 16);
            this.labelColon.Name = "labelColon";
            this.labelColon.Size = new System.Drawing.Size(11, 13);
            this.labelColon.TabIndex = 3;
            this.labelColon.Text = ":";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(22, 86);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(57, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(22, 60);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(59, 13);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Username:";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(22, 16);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(43, 13);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server:";
            // 
            // CloudStorageClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 173);
            this.Controls.Add(this.pageControl1);
            this.Name = "CloudStorageClientForm";
            this.Text = "Save to Dropbox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloudStorageClientForm_FormClosing);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.pageControl1, 0);
            this.pageControl1.ResumeLayout(false);
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            this.pgProxy.ResumeLayout(false);
            this.pgProxy.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// Page File.
        /// </summary>
        protected FastReport.Controls.PageControlPage pgFile;
        
        /// <summary>
        /// Page Proxy.
        /// </summary>
        protected FastReport.Controls.PageControlPage pgProxy;
        
        /// <summary>
        /// ComboBox File Type.
        /// </summary>
        protected System.Windows.Forms.ComboBox cbFileType;
        
        /// <summary>
        /// Label File Type.
        /// </summary>
        protected System.Windows.Forms.Label labelFileType;
        
        /// <summary>
        /// Buttons Settings.
        /// </summary>
        protected System.Windows.Forms.Button buttonSettings;
        
        /// <summary>
        /// Label Colon.
        /// </summary>
        protected System.Windows.Forms.Label labelColon;
        
        /// <summary>
        /// Label Password.
        /// </summary>
        protected System.Windows.Forms.Label labelPassword;
        
        /// <summary>
        /// Label Username.
        /// </summary>
        protected System.Windows.Forms.Label labelUsername;
        
        /// <summary>
        /// Label Server.
        /// </summary>
        protected System.Windows.Forms.Label labelServer;
        
        /// <summary>
        /// TextBox Username.
        /// </summary>
        protected System.Windows.Forms.TextBox tbUsername;
        
        /// <summary>
        /// TextBox Port.
        /// </summary>
        protected System.Windows.Forms.TextBox tbPort;
        
        /// <summary>
        /// TextBox Server.
        /// </summary>
        protected System.Windows.Forms.TextBox tbServer;
        
        /// <summary>
        /// TextBox Password.
        /// </summary>
        protected System.Windows.Forms.TextBox tbPassword;
        
        /// <summary>
        /// PageControl.
        /// </summary>
        protected FastReport.Controls.PageControl pageControl1;
    }
}