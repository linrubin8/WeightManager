namespace FastReport.Cloud.StorageClient.GoogleDrive
{
    partial class GoogleDriveStorageClientForm
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
            this.pgFile.SuspendLayout();
            this.pgProxy.SuspendLayout();
            this.pageControl1.SuspendLayout();
            this.SuspendLayout();
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
            // btnOk
            // 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GoogleDriveStorageClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 173);
            this.Controls.Add(this.pageControl1);
            this.Name = "GoogleDriveStorageClientForm";
            this.Text = "Save to Google Drive";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.pageControl1, 0);
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            this.pgProxy.ResumeLayout(false);
            this.pgProxy.PerformLayout();
            this.pageControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
