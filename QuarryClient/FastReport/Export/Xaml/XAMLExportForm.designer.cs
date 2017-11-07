namespace FastReport.Forms
{
    partial class XAMLExportForm
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
            this.lblImageFormat = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbToMultipleFiles = new System.Windows.Forms.CheckBox();
            this.cbScroll = new System.Windows.Forms.CheckBox();
            this.gbPageRange.SuspendLayout();
            this.pcPages.SuspendLayout();
            this.panPages.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPageRange
            // 
            this.gbPageRange.Location = new System.Drawing.Point(8, 4);
            // 
            // pcPages
            // 
            this.pcPages.Location = new System.Drawing.Point(0, 0);
            this.pcPages.Size = new System.Drawing.Size(276, 196);
            // 
            // panPages
            // 
            this.panPages.Controls.Add(this.gbOptions);
            this.panPages.Size = new System.Drawing.Size(276, 196);
            this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
            this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
            // 
            // cbOpenAfter
            // 
            this.cbOpenAfter.Location = new System.Drawing.Point(20, 248);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(112, 266);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(193, 266);
            this.btnCancel.TabIndex = 1;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.lblImageFormat);
            this.gbOptions.Controls.Add(this.comboBox1);
            this.gbOptions.Location = new System.Drawing.Point(8, 136);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(260, 57);
            this.gbOptions.TabIndex = 5;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // lblImageFormat
            // 
            this.lblImageFormat.AutoSize = true;
            this.lblImageFormat.Location = new System.Drawing.Point(12, 24);
            this.lblImageFormat.Name = "lblImageFormat";
            this.lblImageFormat.Size = new System.Drawing.Size(76, 13);
            this.lblImageFormat.TabIndex = 1;
            this.lblImageFormat.Text = "Image format:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Png",
            "Jpeg"});
            this.comboBox1.Location = new System.Drawing.Point(104, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // cbToMultipleFiles
            // 
            this.cbToMultipleFiles.AutoSize = true;
            this.cbToMultipleFiles.Location = new System.Drawing.Point(20, 202);
            this.cbToMultipleFiles.Name = "cbToMultipleFiles";
            this.cbToMultipleFiles.Size = new System.Drawing.Size(132, 17);
            this.cbToMultipleFiles.TabIndex = 6;
            this.cbToMultipleFiles.Text = "Export to multiple files";
            this.cbToMultipleFiles.UseVisualStyleBackColor = true;
            // 
            // cbScroll
            // 
            this.cbScroll.AutoSize = true;
            this.cbScroll.Location = new System.Drawing.Point(20, 225);
            this.cbScroll.Name = "cbScroll";
            this.cbScroll.Size = new System.Drawing.Size(91, 17);
            this.cbScroll.TabIndex = 7;
            this.cbScroll.Text = "Add scroll bar";
            this.cbScroll.UseVisualStyleBackColor = true;
            // 
            // XAMLExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(276, 295);
            this.Controls.Add(this.cbScroll);
            this.Controls.Add(this.cbToMultipleFiles);
            this.Name = "XAMLExportForm";
            this.Text = "Export to XAML";
            this.Controls.SetChildIndex(this.pcPages, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.cbOpenAfter, 0);
            this.Controls.SetChildIndex(this.cbToMultipleFiles, 0);
            this.Controls.SetChildIndex(this.cbScroll, 0);
            this.gbPageRange.ResumeLayout(false);
            this.gbPageRange.PerformLayout();
            this.pcPages.ResumeLayout(false);
            this.panPages.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.CheckBox cbToMultipleFiles;
        private System.Windows.Forms.CheckBox cbScroll;
    }
}
