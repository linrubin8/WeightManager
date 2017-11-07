namespace FastReport.Forms
{
    partial class ODFExportForm
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
          this.cbPageBreaks = new System.Windows.Forms.CheckBox();
          this.cbWysiwyg = new System.Windows.Forms.CheckBox();
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
          this.pcPages.Size = new System.Drawing.Size(276, 216);
          // 
          // panPages
          // 
          this.panPages.Controls.Add(this.gbOptions);
          this.panPages.Size = new System.Drawing.Size(276, 216);
          this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
          this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
          // 
          // cbOpenAfter
          // 
          this.cbOpenAfter.Location = new System.Drawing.Point(8, 220);
          // 
          // btnOk
          // 
          this.btnOk.Location = new System.Drawing.Point(112, 244);
          // 
          // btnCancel
          // 
          this.btnCancel.Location = new System.Drawing.Point(192, 244);
          this.btnCancel.TabIndex = 1;
          // 
          // gbOptions
          // 
          this.gbOptions.Controls.Add(this.cbPageBreaks);
          this.gbOptions.Controls.Add(this.cbWysiwyg);
          this.gbOptions.Location = new System.Drawing.Point(8, 136);
          this.gbOptions.Name = "gbOptions";
          this.gbOptions.Size = new System.Drawing.Size(260, 72);
          this.gbOptions.TabIndex = 5;
          this.gbOptions.TabStop = false;
          this.gbOptions.Text = "Options";
          // 
          // cbPageBreaks
          // 
          this.cbPageBreaks.AutoSize = true;
          this.cbPageBreaks.Checked = true;
          this.cbPageBreaks.CheckState = System.Windows.Forms.CheckState.Checked;
          this.cbPageBreaks.Location = new System.Drawing.Point(12, 44);
          this.cbPageBreaks.Name = "cbPageBreaks";
          this.cbPageBreaks.Size = new System.Drawing.Size(85, 17);
          this.cbPageBreaks.TabIndex = 2;
          this.cbPageBreaks.Text = "Page breaks";
          this.cbPageBreaks.UseVisualStyleBackColor = true;
          // 
          // cbWysiwyg
          // 
          this.cbWysiwyg.AutoSize = true;
          this.cbWysiwyg.Checked = true;
          this.cbWysiwyg.CheckState = System.Windows.Forms.CheckState.Checked;
          this.cbWysiwyg.Location = new System.Drawing.Point(12, 20);
          this.cbWysiwyg.Name = "cbWysiwyg";
          this.cbWysiwyg.Size = new System.Drawing.Size(69, 17);
          this.cbWysiwyg.TabIndex = 1;
          this.cbWysiwyg.Text = "Wysiwyg";
          this.cbWysiwyg.UseVisualStyleBackColor = true;
          // 
          // ODFExportForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.ClientSize = new System.Drawing.Size(276, 277);
          this.Name = "ODFExportForm";
          this.Text = "Export to Open Office";
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
        private System.Windows.Forms.CheckBox cbWysiwyg;
        private System.Windows.Forms.CheckBox cbPageBreaks;

    }
}
