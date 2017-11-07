namespace FastReport.Forms
{
    partial class XMLExportForm
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
          this.cbDataOnly = new System.Windows.Forms.CheckBox();
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
          this.pcPages.Size = new System.Drawing.Size(276, 240);
          // 
          // panPages
          // 
          this.panPages.Controls.Add(this.gbOptions);
          this.panPages.Size = new System.Drawing.Size(276, 240);
          this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
          this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
          // 
          // cbOpenAfter
          // 
          this.cbOpenAfter.Location = new System.Drawing.Point(8, 244);
          // 
          // btnOk
          // 
          this.btnOk.Location = new System.Drawing.Point(112, 268);
          // 
          // btnCancel
          // 
          this.btnCancel.Location = new System.Drawing.Point(192, 268);
          this.btnCancel.TabIndex = 1;
          // 
          // gbOptions
          // 
          this.gbOptions.Controls.Add(this.cbDataOnly);
          this.gbOptions.Controls.Add(this.cbPageBreaks);
          this.gbOptions.Controls.Add(this.cbWysiwyg);
          this.gbOptions.Location = new System.Drawing.Point(8, 136);
          this.gbOptions.Name = "gbOptions";
          this.gbOptions.Size = new System.Drawing.Size(260, 96);
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
          // cbDataOnly
          // 
          this.cbDataOnly.AutoSize = true;
          this.cbDataOnly.Location = new System.Drawing.Point(12, 68);
          this.cbDataOnly.Name = "cbDataOnly";
          this.cbDataOnly.Size = new System.Drawing.Size(72, 17);
          this.cbDataOnly.TabIndex = 3;
          this.cbDataOnly.Text = "Data only";
          this.cbDataOnly.UseVisualStyleBackColor = true;
          // 
          // XMLExportForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.ClientSize = new System.Drawing.Size(276, 301);
          this.Name = "XMLExportForm";
          this.Text = "Export to XML";
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
      private System.Windows.Forms.CheckBox cbDataOnly;

    }
}
