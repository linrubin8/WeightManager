namespace FastReport.Cloud.StorageClient.FastCloud
{
    partial class FastCloudStorageClientSimpleForm
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
            this.labelServer = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelColon = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.gbProxySettings = new System.Windows.Forms.GroupBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.gbUserDetails = new System.Windows.Forms.GroupBox();
            this.labelUserPassword = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.gbProxySettings.SuspendLayout();
            this.gbUserDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(326, 241);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(407, 241);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(22, 25);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(43, 13);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "Server:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(22, 60);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(59, 13);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "Username:";
            // 
            // labelColon
            // 
            this.labelColon.AutoSize = true;
            this.labelColon.Location = new System.Drawing.Point(389, 25);
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
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password:";
            // 
            // gbProxySettings
            // 
            this.gbProxySettings.Controls.Add(this.tbPort);
            this.gbProxySettings.Controls.Add(this.tbPassword);
            this.gbProxySettings.Controls.Add(this.tbUsername);
            this.gbProxySettings.Controls.Add(this.tbServer);
            this.gbProxySettings.Controls.Add(this.labelServer);
            this.gbProxySettings.Controls.Add(this.labelUsername);
            this.gbProxySettings.Controls.Add(this.labelPassword);
            this.gbProxySettings.Controls.Add(this.labelColon);
            this.gbProxySettings.Location = new System.Drawing.Point(12, 112);
            this.gbProxySettings.Name = "gbProxySettings";
            this.gbProxySettings.Size = new System.Drawing.Size(470, 120);
            this.gbProxySettings.TabIndex = 5;
            this.gbProxySettings.TabStop = false;
            this.gbProxySettings.Text = "Proxy settings";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(406, 22);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(50, 20);
            this.tbPort.TabIndex = 8;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(103, 83);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(280, 20);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(103, 57);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(280, 20);
            this.tbUsername.TabIndex = 6;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(103, 22);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(280, 20);
            this.tbServer.TabIndex = 5;
            // 
            // gbUserDetails
            // 
            this.gbUserDetails.Controls.Add(this.labelUserPassword);
            this.gbUserDetails.Controls.Add(this.labelEmail);
            this.gbUserDetails.Controls.Add(this.tbUserPassword);
            this.gbUserDetails.Controls.Add(this.tbEmail);
            this.gbUserDetails.Location = new System.Drawing.Point(12, 17);
            this.gbUserDetails.Name = "gbUserDetails";
            this.gbUserDetails.Size = new System.Drawing.Size(470, 84);
            this.gbUserDetails.TabIndex = 6;
            this.gbUserDetails.TabStop = false;
            this.gbUserDetails.Text = "User details";
            // 
            // labelUserPassword
            // 
            this.labelUserPassword.AutoSize = true;
            this.labelUserPassword.Location = new System.Drawing.Point(22, 51);
            this.labelUserPassword.Name = "labelUserPassword";
            this.labelUserPassword.Size = new System.Drawing.Size(57, 13);
            this.labelUserPassword.TabIndex = 3;
            this.labelUserPassword.Text = "Password:";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(22, 25);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "Email:";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(103, 48);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.Size = new System.Drawing.Size(280, 20);
            this.tbUserPassword.TabIndex = 1;
            this.tbUserPassword.UseSystemPasswordChar = true;
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(103, 22);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(280, 20);
            this.tbEmail.TabIndex = 0;
            // 
            // FastCloudStorageClientSimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(494, 276);
            this.Controls.Add(this.gbUserDetails);
            this.Controls.Add(this.gbProxySettings);
            this.Name = "FastCloudStorageClientSimpleForm";
            this.Text = "Save to Cloud";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.gbProxySettings, 0);
            this.Controls.SetChildIndex(this.gbUserDetails, 0);
            this.gbProxySettings.ResumeLayout(false);
            this.gbProxySettings.PerformLayout();
            this.gbUserDetails.ResumeLayout(false);
            this.gbUserDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelColon;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.GroupBox gbProxySettings;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.GroupBox gbUserDetails;
        private System.Windows.Forms.Label labelUserPassword;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.TextBox tbEmail;
    }
}
