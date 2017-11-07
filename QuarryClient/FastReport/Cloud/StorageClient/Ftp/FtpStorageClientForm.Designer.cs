namespace FastReport.Cloud.StorageClient.Ftp
{
    partial class FtpStorageClientForm
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
            this.tbFtpServer = new System.Windows.Forms.TextBox();
            this.tbFtpUsername = new System.Windows.Forms.TextBox();
            this.tbFtpPassword = new System.Windows.Forms.TextBox();
            this.labelFtpServer = new System.Windows.Forms.Label();
            this.labelFtpUsername = new System.Windows.Forms.Label();
            this.labelFtpPassword = new System.Windows.Forms.Label();
            this.pgFile.SuspendLayout();
            this.pgProxy.SuspendLayout();
            this.pageControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgFile
            // 
            this.pgFile.Controls.Add(this.tbFtpServer);
            this.pgFile.Controls.Add(this.tbFtpUsername);
            this.pgFile.Controls.Add(this.labelFtpServer);
            this.pgFile.Controls.Add(this.labelFtpPassword);
            this.pgFile.Controls.Add(this.tbFtpPassword);
            this.pgFile.Controls.Add(this.labelFtpUsername);
            this.pgFile.Size = new System.Drawing.Size(411, 178);
            this.pgFile.Controls.SetChildIndex(this.labelFtpUsername, 0);
            this.pgFile.Controls.SetChildIndex(this.tbFtpPassword, 0);
            this.pgFile.Controls.SetChildIndex(this.labelFtpPassword, 0);
            this.pgFile.Controls.SetChildIndex(this.cbFileType, 0);
            this.pgFile.Controls.SetChildIndex(this.labelFtpServer, 0);
            this.pgFile.Controls.SetChildIndex(this.tbFtpUsername, 0);
            this.pgFile.Controls.SetChildIndex(this.buttonSettings, 0);
            this.pgFile.Controls.SetChildIndex(this.labelFileType, 0);
            this.pgFile.Controls.SetChildIndex(this.tbFtpServer, 0);
            // 
            // pgProxy
            // 
            this.pgProxy.Size = new System.Drawing.Size(411, 178);
            // 
            // cbFileType
            // 
            this.cbFileType.Location = new System.Drawing.Point(179, 108);
            // 
            // labelFileType
            // 
            this.labelFileType.Location = new System.Drawing.Point(23, 111);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(314, 140);
            // 
            // pageControl1
            // 
            this.pageControl1.Size = new System.Drawing.Size(512, 180);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(367, 198);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(448, 198);
            // 
            // tbFtpServer
            // 
            this.tbFtpServer.Location = new System.Drawing.Point(179, 12);
            this.tbFtpServer.Name = "tbFtpServer";
            this.tbFtpServer.Size = new System.Drawing.Size(220, 20);
            this.tbFtpServer.TabIndex = 3;
            this.tbFtpServer.Text = "ftp://";
            // 
            // tbFtpUsername
            // 
            this.tbFtpUsername.Location = new System.Drawing.Point(179, 47);
            this.tbFtpUsername.Name = "tbFtpUsername";
            this.tbFtpUsername.Size = new System.Drawing.Size(220, 20);
            this.tbFtpUsername.TabIndex = 4;
            // 
            // tbFtpPassword
            // 
            this.tbFtpPassword.Location = new System.Drawing.Point(179, 73);
            this.tbFtpPassword.Name = "tbFtpPassword";
            this.tbFtpPassword.Size = new System.Drawing.Size(220, 20);
            this.tbFtpPassword.TabIndex = 5;
            this.tbFtpPassword.UseSystemPasswordChar = true;
            // 
            // labelFtpServer
            // 
            this.labelFtpServer.AutoSize = true;
            this.labelFtpServer.Location = new System.Drawing.Point(23, 15);
            this.labelFtpServer.Name = "labelFtpServer";
            this.labelFtpServer.Size = new System.Drawing.Size(64, 13);
            this.labelFtpServer.TabIndex = 6;
            this.labelFtpServer.Text = "FTP Server:";
            // 
            // labelFtpUsername
            // 
            this.labelFtpUsername.AutoSize = true;
            this.labelFtpUsername.Location = new System.Drawing.Point(23, 50);
            this.labelFtpUsername.Name = "labelFtpUsername";
            this.labelFtpUsername.Size = new System.Drawing.Size(59, 13);
            this.labelFtpUsername.TabIndex = 7;
            this.labelFtpUsername.Text = "Username:";
            // 
            // labelFtpPassword
            // 
            this.labelFtpPassword.AutoSize = true;
            this.labelFtpPassword.Location = new System.Drawing.Point(23, 76);
            this.labelFtpPassword.Name = "labelFtpPassword";
            this.labelFtpPassword.Size = new System.Drawing.Size(33, 13);
            this.labelFtpPassword.TabIndex = 8;
            this.labelFtpPassword.Text = "Pass:";
            // 
            // FtpStorageClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 233);
            this.Name = "FtpStorageClientForm";
            this.Text = "Save to FTP";
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            this.pgProxy.ResumeLayout(false);
            this.pgProxy.PerformLayout();
            this.pageControl1.ResumeLayout(false);
            this.ResumeLayout(false);

            // apply Right to Left layout
            if (Utils.Config.RightToLeft)
            {
                RightToLeft = System.Windows.Forms.RightToLeft.Yes;

                // move components to other side
                tbFtpServer.Left = pgFile.Width - tbFtpServer.Left - tbFtpServer.Width;
                tbFtpServer.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                tbFtpUsername.Left = pgFile.Width - tbFtpUsername.Left - tbFtpUsername.Width;
                tbFtpUsername.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                tbFtpPassword.Left = pgFile.Width - tbFtpPassword.Left - tbFtpPassword.Width;
                tbFtpPassword.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                cbFileType.Left = pgFile.Width - cbFileType.Left - cbFileType.Width;
                cbFileType.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            }
        }

        #endregion

        private System.Windows.Forms.Label labelFtpPassword;
        private System.Windows.Forms.Label labelFtpUsername;
        private System.Windows.Forms.Label labelFtpServer;
        private System.Windows.Forms.TextBox tbFtpPassword;
        private System.Windows.Forms.TextBox tbFtpUsername;
        private System.Windows.Forms.TextBox tbFtpServer;
    }
}
