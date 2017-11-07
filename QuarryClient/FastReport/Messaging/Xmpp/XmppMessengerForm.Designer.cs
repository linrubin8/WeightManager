namespace FastReport.Messaging.Xmpp
{
    partial class XmppMessengerForm
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
            this.tbJidTo = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbJidFrom = new System.Windows.Forms.TextBox();
            this.labelJidFrom = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelJidTo = new System.Windows.Forms.Label();
            this.labelCloudWarning = new System.Windows.Forms.Label();
            this.pgFile.SuspendLayout();
            this.pgProxy.SuspendLayout();
            this.pageControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgFile
            // 
            this.pgFile.Controls.Add(this.labelJidTo);
            this.pgFile.Controls.Add(this.labelPassword);
            this.pgFile.Controls.Add(this.labelJidFrom);
            this.pgFile.Controls.Add(this.tbJidTo);
            this.pgFile.Controls.Add(this.tbJidFrom);
            this.pgFile.Controls.Add(this.tbPassword);
            this.pgFile.Controls.Add(this.labelCloudWarning);
            this.pgFile.Size = new System.Drawing.Size(411, 184);
            this.pgFile.Controls.SetChildIndex(this.tbPassword, 0);
            this.pgFile.Controls.SetChildIndex(this.tbJidFrom, 0);
            this.pgFile.Controls.SetChildIndex(this.buttonSettings, 0);
            this.pgFile.Controls.SetChildIndex(this.cbFileType, 0);
            this.pgFile.Controls.SetChildIndex(this.labelFileType, 0);
            this.pgFile.Controls.SetChildIndex(this.tbJidTo, 0);
            this.pgFile.Controls.SetChildIndex(this.labelJidFrom, 0);
            this.pgFile.Controls.SetChildIndex(this.labelPassword, 0);
            this.pgFile.Controls.SetChildIndex(this.labelJidTo, 0);
            // 
            // cbFileType
            // 
            this.cbFileType.Location = new System.Drawing.Point(178, 94);
            this.cbFileType.Enabled = false;
            this.cbFileType.Visible = false;
            this.cbFileType.TabStop = false;
            // 
            // labelFileType
            // 
            this.labelFileType.Location = new System.Drawing.Point(12, 97);
            this.labelFileType.Enabled = false;
            this.labelFileType.Visible = false;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(313, 121);
            this.buttonSettings.Enabled = false;
            this.buttonSettings.Visible = false;
            this.buttonSettings.TabStop = false;
            // 
            // pageControl1
            // 
            this.pageControl1.Size = new System.Drawing.Size(512, 160);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(368, 178);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(449, 178);
            // 
            // tbJidTo
            // 
            this.tbJidTo.Location = new System.Drawing.Point(178, 66);
            this.tbJidTo.Name = "tbJidTo";
            this.tbJidTo.Size = new System.Drawing.Size(220, 20);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(178, 40);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(220, 20);
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbJidFrom
            // 
            this.tbJidFrom.Location = new System.Drawing.Point(178, 14);
            this.tbJidFrom.Name = "tbJidFrom";
            this.tbJidFrom.Size = new System.Drawing.Size(220, 20);
            // 
            // labelJidFrom
            // 
            this.labelJidFrom.AutoSize = true;
            this.labelJidFrom.Location = new System.Drawing.Point(12, 17);
            this.labelJidFrom.Name = "labelJidFrom";
            this.labelJidFrom.Size = new System.Drawing.Size(59, 13);
            this.labelJidFrom.Text = "";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 43);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(57, 13);
            this.labelPassword.Text = "";
            // 
            // labelJidTo
            // 
            this.labelJidTo.AutoSize = true;
            this.labelJidTo.Location = new System.Drawing.Point(12, 69);
            this.labelJidTo.Name = "labelJidTo";
            this.labelJidTo.Size = new System.Drawing.Size(72, 13);
            this.labelJidTo.Text = "";
            //
            // labelCloudWarning
            //
            this.labelCloudWarning.AutoSize = false;
            this.labelCloudWarning.Location = new System.Drawing.Point(22, 110);
            this.labelCloudWarning.Size = new System.Drawing.Size(368, 48);
            this.labelCloudWarning.Text = "Warning!";
            // 
            // XmppMessengerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 213);
            this.Name = "XmppMessengerForm";
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            this.pgProxy.ResumeLayout(false);
            this.pgProxy.PerformLayout();
            this.pageControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            //
            // Tab Order
            //
            this.tbJidFrom.TabIndex = 1;
            this.tbPassword.TabIndex = 2;
            this.tbJidTo.TabIndex = 3;
            this.btnOk.TabIndex = 4;
            this.btnCancel.TabIndex = 5;
            this.tbProxyServer.TabIndex = 1;
            this.tbProxyPort.TabIndex = 2;
            this.tbProxyUsername.TabIndex = 3;
            this.tbProxyPassword.TabIndex = 4;

            // apply Right to Left layout
            if (FastReport.Utils.Config.RightToLeft)
            {
                RightToLeft = System.Windows.Forms.RightToLeft.Yes;

                // move components to other side
                tbJidFrom.Left = pgFile.Width - tbJidFrom.Left - tbJidFrom.Width;
                tbJidFrom.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                tbPassword.Left = pgFile.Width - tbPassword.Left - tbPassword.Width;
                tbPassword.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                tbJidTo.Left = pgFile.Width - tbJidTo.Left - tbJidTo.Width;
                tbJidTo.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            }
        }

        #endregion

        private System.Windows.Forms.TextBox tbJidTo;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbJidFrom;
        private System.Windows.Forms.Label labelJidTo;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelJidFrom;
        private System.Windows.Forms.Label labelCloudWarning;
    }
}
