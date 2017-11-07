namespace FastReport.Cloud.StorageClient.Dropbox
{
    partial class ApplicationInfoForm
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
            this.labelAccessToken = new System.Windows.Forms.Label();
            this.tbAccessToken = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(396, 46);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(477, 46);
            // 
            // labelAccessToken
            // 
            this.labelAccessToken.AutoSize = true;
            this.labelAccessToken.Location = new System.Drawing.Point(12, 15);
            this.labelAccessToken.Name = "labelAccessToken";
            this.labelAccessToken.Size = new System.Drawing.Size(76, 13);
            this.labelAccessToken.TabIndex = 1;
            this.labelAccessToken.Text = "Access Token:";
            // 
            // tbAccessToken
            // 
            this.tbAccessToken.Location = new System.Drawing.Point(152, 12);
            this.tbAccessToken.Name = "tbAccessToken";
            this.tbAccessToken.Size = new System.Drawing.Size(400, 20);
            this.tbAccessToken.TabIndex = 3;
            // 
            // ApplicationInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 81);
            this.Controls.Add(this.tbAccessToken);
            this.Controls.Add(this.labelAccessToken);
            this.Name = "ApplicationInfoForm";
            this.Text = "Application Info";
            this.Controls.SetChildIndex(this.labelAccessToken, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tbAccessToken, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAccessToken;
        private System.Windows.Forms.TextBox tbAccessToken;
    }
}