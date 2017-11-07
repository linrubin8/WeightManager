namespace FastReport.Forms
{
    partial class HTMLExportForm
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
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbEmbPic = new System.Windows.Forms.CheckBox();
            this.cbLayers = new System.Windows.Forms.CheckBox();
            this.cbNavigator = new System.Windows.Forms.CheckBox();
            this.cbSinglePage = new System.Windows.Forms.CheckBox();
            this.cbSubFolder = new System.Windows.Forms.CheckBox();
            this.cbPictures = new System.Windows.Forms.CheckBox();
            this.cbWysiwyg = new System.Windows.Forms.CheckBox();
            this.gbPageRange.SuspendLayout();
            this.panPages.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPageRange
            // 
            this.gbPageRange.Location = new System.Drawing.Point(12, 9);
            this.gbPageRange.Size = new System.Drawing.Size(260, 136);
            // 
            // cbOpenAfter
            // 
            this.cbOpenAfter.Location = new System.Drawing.Point(36, 382);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(129, 410);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(209, 410);
            this.btnCancel.TabIndex = 1;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbEmbPic);
            this.gbOptions.Controls.Add(this.cbLayers);
            this.gbOptions.Controls.Add(this.cbNavigator);
            this.gbOptions.Controls.Add(this.cbSinglePage);
            this.gbOptions.Controls.Add(this.cbSubFolder);
            this.gbOptions.Controls.Add(this.cbPictures);
            this.gbOptions.Controls.Add(this.cbWysiwyg);
            this.gbOptions.Location = new System.Drawing.Point(24, 176);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(260, 192);
            this.gbOptions.TabIndex = 7;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbEmbPic
            // 
            this.cbEmbPic.AutoSize = true;
            this.cbEmbPic.Checked = true;
            this.cbEmbPic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEmbPic.Location = new System.Drawing.Point(12, 163);
            this.cbEmbPic.Name = "cbEmbPic";
            this.cbEmbPic.Size = new System.Drawing.Size(119, 17);
            this.cbEmbPic.TabIndex = 9;
            this.cbEmbPic.Text = "Embedding pictures";
            this.cbEmbPic.UseVisualStyleBackColor = true;
            // 
            // cbLayers
            // 
            this.cbLayers.AutoSize = true;
            this.cbLayers.Location = new System.Drawing.Point(12, 140);
            this.cbLayers.Name = "cbLayers";
            this.cbLayers.Size = new System.Drawing.Size(58, 17);
            this.cbLayers.TabIndex = 8;
            this.cbLayers.Text = "Layers";
            this.cbLayers.UseVisualStyleBackColor = true;
            // 
            // cbNavigator
            // 
            this.cbNavigator.AutoSize = true;
            this.cbNavigator.Checked = true;
            this.cbNavigator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNavigator.Location = new System.Drawing.Point(12, 93);
            this.cbNavigator.Name = "cbNavigator";
            this.cbNavigator.Size = new System.Drawing.Size(73, 17);
            this.cbNavigator.TabIndex = 6;
            this.cbNavigator.Text = "Navigator";
            this.cbNavigator.UseVisualStyleBackColor = true;
            // 
            // cbSinglePage
            // 
            this.cbSinglePage.AutoSize = true;
            this.cbSinglePage.Location = new System.Drawing.Point(12, 117);
            this.cbSinglePage.Name = "cbSinglePage";
            this.cbSinglePage.Size = new System.Drawing.Size(81, 17);
            this.cbSinglePage.TabIndex = 7;
            this.cbSinglePage.Text = "Single page";
            this.cbSinglePage.UseVisualStyleBackColor = true;
            // 
            // cbSubFolder
            // 
            this.cbSubFolder.AutoSize = true;
            this.cbSubFolder.Checked = true;
            this.cbSubFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSubFolder.Location = new System.Drawing.Point(12, 69);
            this.cbSubFolder.Name = "cbSubFolder";
            this.cbSubFolder.Size = new System.Drawing.Size(76, 17);
            this.cbSubFolder.TabIndex = 5;
            this.cbSubFolder.Text = "Sub-folder";
            this.cbSubFolder.UseVisualStyleBackColor = true;
            // 
            // cbPictures
            // 
            this.cbPictures.AutoSize = true;
            this.cbPictures.Checked = true;
            this.cbPictures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPictures.Location = new System.Drawing.Point(12, 45);
            this.cbPictures.Name = "cbPictures";
            this.cbPictures.Size = new System.Drawing.Size(64, 17);
            this.cbPictures.TabIndex = 3;
            this.cbPictures.Text = "Pictures";
            this.cbPictures.UseVisualStyleBackColor = true;
            // 
            // cbWysiwyg
            // 
            this.cbWysiwyg.AutoSize = true;
            this.cbWysiwyg.Checked = true;
            this.cbWysiwyg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWysiwyg.Location = new System.Drawing.Point(12, 21);
            this.cbWysiwyg.Name = "cbWysiwyg";
            this.cbWysiwyg.Size = new System.Drawing.Size(69, 17);
            this.cbWysiwyg.TabIndex = 1;
            this.cbWysiwyg.Text = "Wysiwyg";
            this.cbWysiwyg.UseVisualStyleBackColor = true;
            // 
            // HTMLExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(310, 453);
            this.Controls.Add(this.gbOptions);
            this.Name = "HTMLExportForm";
            this.Text = "Export to HTML";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.cbOpenAfter, 0);
            this.Controls.SetChildIndex(this.gbOptions, 0);
            this.gbPageRange.ResumeLayout(false);
            this.gbPageRange.PerformLayout();
            this.panPages.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbEmbPic;
        private System.Windows.Forms.CheckBox cbLayers;
        private System.Windows.Forms.CheckBox cbNavigator;
        private System.Windows.Forms.CheckBox cbSinglePage;
        private System.Windows.Forms.CheckBox cbSubFolder;
        private System.Windows.Forms.CheckBox cbPictures;
        private System.Windows.Forms.CheckBox cbWysiwyg;
    }
}
