namespace FastReport.Messaging
{
    partial class MessengerForm
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
            this.tbProxyPassword = new System.Windows.Forms.TextBox();
            this.tbProxyUsername = new System.Windows.Forms.TextBox();
            this.tbProxyPort = new System.Windows.Forms.TextBox();
            this.tbProxyServer = new System.Windows.Forms.TextBox();
            this.labelProxyColon = new System.Windows.Forms.Label();
            this.labelProxyPassword = new System.Windows.Forms.Label();
            this.labelProxyUsername = new System.Windows.Forms.Label();
            this.labelProxyServer = new System.Windows.Forms.Label();
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
            this.pgFile.Text = "File";
            // 
            // buttonSettings
            // 
            this.buttonSettings.Enabled = false;
            this.buttonSettings.Location = new System.Drawing.Point(313, 40);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(85, 23);
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
            this.cbFileType.SelectedIndexChanged += new System.EventHandler(this.cbFileType_SelectedIndexChanged);
            // 
            // labelFileType
            // 
            this.labelFileType.AutoSize = true;
            this.labelFileType.Location = new System.Drawing.Point(22, 16);
            this.labelFileType.Name = "labelFileType";
            this.labelFileType.Size = new System.Drawing.Size(54, 13);
            this.labelFileType.Text = "File Type:";
            // 
            // pgProxy
            // 
            this.pgProxy.BackColor = System.Drawing.SystemColors.Window;
            this.pgProxy.Controls.Add(this.tbProxyPassword);
            this.pgProxy.Controls.Add(this.tbProxyUsername);
            this.pgProxy.Controls.Add(this.tbProxyPort);
            this.pgProxy.Controls.Add(this.tbProxyServer);
            this.pgProxy.Controls.Add(this.labelProxyColon);
            this.pgProxy.Controls.Add(this.labelProxyPassword);
            this.pgProxy.Controls.Add(this.labelProxyUsername);
            this.pgProxy.Controls.Add(this.labelProxyServer);
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(100, 1);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.Size = new System.Drawing.Size(411, 118);
            this.pgProxy.Text = "Proxy";
            // 
            // tbProxyPassword
            // 
            this.tbProxyPassword.Location = new System.Drawing.Point(126, 83);
            this.tbProxyPassword.Name = "tbProxyPassword";
            this.tbProxyPassword.Size = new System.Drawing.Size(200, 20);
            this.tbProxyPassword.UseSystemPasswordChar = true;
            // 
            // tbProxyUsername
            // 
            this.tbProxyUsername.Location = new System.Drawing.Point(126, 57);
            this.tbProxyUsername.Name = "tbProxyUsername";
            this.tbProxyUsername.Size = new System.Drawing.Size(200, 20);
            // 
            // tbProxyPort
            // 
            this.tbProxyPort.Location = new System.Drawing.Point(349, 13);
            this.tbProxyPort.Name = "tbProxyPort";
            this.tbProxyPort.Size = new System.Drawing.Size(50, 20);
            // 
            // tbProxyServer
            // 
            this.tbProxyServer.Location = new System.Drawing.Point(126, 13);
            this.tbProxyServer.Name = "tbProxyServer";
            this.tbProxyServer.Size = new System.Drawing.Size(200, 20);
            // 
            // labelProxyColon
            // 
            this.labelProxyColon.AutoSize = true;
            this.labelProxyColon.Location = new System.Drawing.Point(332, 16);
            this.labelProxyColon.Name = "labelProxyColon";
            this.labelProxyColon.Size = new System.Drawing.Size(11, 13);
            this.labelProxyColon.Text = ":";
            // 
            // labelProxyPassword
            // 
            this.labelProxyPassword.AutoSize = true;
            this.labelProxyPassword.Location = new System.Drawing.Point(22, 86);
            this.labelProxyPassword.Name = "labelProxyPassword";
            this.labelProxyPassword.Size = new System.Drawing.Size(57, 13);
            this.labelProxyPassword.Text = "Password:";
            // 
            // labelProxyUsername
            // 
            this.labelProxyUsername.AutoSize = true;
            this.labelProxyUsername.Location = new System.Drawing.Point(22, 60);
            this.labelProxyUsername.Name = "labelProxyUsername";
            this.labelProxyUsername.Size = new System.Drawing.Size(59, 13);
            this.labelProxyUsername.Text = "Username:";
            // 
            // labelProxyServer
            // 
            this.labelProxyServer.AutoSize = true;
            this.labelProxyServer.Location = new System.Drawing.Point(22, 16);
            this.labelProxyServer.Name = "labelProxyServer";
            this.labelProxyServer.Size = new System.Drawing.Size(43, 13);
            this.labelProxyServer.Text = "Server:";
            // 
            // MessengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 173);
            this.Controls.Add(this.pageControl1);
            this.Name = "MessengerForm";
            this.Text = "Send Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessengerForm_FormClosing);
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
        protected System.Windows.Forms.Label labelProxyColon;
        
        /// <summary>
        /// Label Password.
        /// </summary>
        protected System.Windows.Forms.Label labelProxyPassword;
        
        /// <summary>
        /// Label Username.
        /// </summary>
        protected System.Windows.Forms.Label labelProxyUsername;
        
        /// <summary>
        /// Label Server.
        /// </summary>
        protected System.Windows.Forms.Label labelProxyServer;
        
        /// <summary>
        /// TextBox Username.
        /// </summary>
        protected System.Windows.Forms.TextBox tbProxyUsername;
        
        /// <summary>
        /// TextBox Port.
        /// </summary>
        protected System.Windows.Forms.TextBox tbProxyPort;
        
        /// <summary>
        /// TextBox Server.
        /// </summary>
        protected System.Windows.Forms.TextBox tbProxyServer;
        
        /// <summary>
        /// TextBox Password.
        /// </summary>
        protected System.Windows.Forms.TextBox tbProxyPassword;

        /// <summary>
        /// PageControl pageControl1.
        /// </summary>
        protected FastReport.Controls.PageControl pageControl1;
    }
}