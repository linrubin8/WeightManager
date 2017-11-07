namespace FastReport.Forms
{
    partial class XpsExportForm
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
          this.gbPageRange.SuspendLayout();
          this.pcPages.SuspendLayout();
          this.panPages.SuspendLayout();
          this.SuspendLayout();
          // 
          // gbPageRange
          // 
          this.gbPageRange.Location = new System.Drawing.Point(8, 4);
          // 
          // pcPages
          // 
          this.pcPages.Location = new System.Drawing.Point(0, 0);
          this.pcPages.Size = new System.Drawing.Size(276, 136);
          // 
          // panPages
          // 
          this.panPages.Size = new System.Drawing.Size(276, 136);
          // 
          // cbOpenAfter
          // 
          this.cbOpenAfter.Location = new System.Drawing.Point(8, 144);
          // 
          // btnOk
          // 
          this.btnOk.Location = new System.Drawing.Point(112, 168);
          // 
          // btnCancel
          // 
          this.btnCancel.Location = new System.Drawing.Point(192, 168);
          this.btnCancel.TabIndex = 1;
          // 
          // XpsExportForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.ClientSize = new System.Drawing.Size(276, 201);
          this.Name = "XpsExportForm";
          this.Text = "Export to MS XPS document";
          this.gbPageRange.ResumeLayout(false);
          this.gbPageRange.PerformLayout();
          this.pcPages.ResumeLayout(false);
          this.panPages.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion


      }
}
