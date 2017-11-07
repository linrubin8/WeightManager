namespace FastReport.Forms
{
    partial class DbfExportForm
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
          this.cbDataOnly = new System.Windows.Forms.CheckBox();
          this.cbbCodepage = new System.Windows.Forms.ComboBox();
          this.lblCodepage = new System.Windows.Forms.Label();
          this.tbFieldNames = new System.Windows.Forms.TextBox();
          this.lblFieldNames = new System.Windows.Forms.Label();
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
          this.pcPages.Controls.Add(this.panPages);
          this.pcPages.HighlightPageIndex = -1;
          this.pcPages.Location = new System.Drawing.Point(0, 0);
          this.pcPages.Name = "pcPages";
          this.pcPages.SelectorWidth = 0;
          this.pcPages.Size = new System.Drawing.Size(276, 272);
          this.pcPages.TabIndex = 1;
          this.pcPages.Text = "pageControl1";
          // 
          // panPages
          // 
          this.panPages.Controls.Add(this.gbOptions);
          this.panPages.Dock = System.Windows.Forms.DockStyle.None;
          this.panPages.Size = new System.Drawing.Size(276, 268);
          this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
          this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
          // 
          // cbOpenAfter
          // 
          this.cbOpenAfter.Location = new System.Drawing.Point(12, 273);
          // 
          // btnOk
          // 
          this.btnOk.Location = new System.Drawing.Point(108, 296);
          // 
          // btnCancel
          // 
          this.btnCancel.Location = new System.Drawing.Point(189, 296);
          this.btnCancel.TabIndex = 1;
          // 
          // gbOptions
          // 
          this.gbOptions.Controls.Add(this.lblFieldNames);
          this.gbOptions.Controls.Add(this.tbFieldNames);
          this.gbOptions.Controls.Add(this.cbDataOnly);
          this.gbOptions.Controls.Add(this.cbbCodepage);
          this.gbOptions.Controls.Add(this.lblCodepage);
          this.gbOptions.Location = new System.Drawing.Point(8, 136);
          this.gbOptions.Name = "gbOptions";
          this.gbOptions.Size = new System.Drawing.Size(260, 128);
          this.gbOptions.TabIndex = 5;
          this.gbOptions.TabStop = false;
          this.gbOptions.Text = "Options";
          // 
          // cbDataOnly
          // 
          this.cbDataOnly.AutoSize = true;
          this.cbDataOnly.Location = new System.Drawing.Point(12, 100);
          this.cbDataOnly.Name = "cbDataOnly";
          this.cbDataOnly.Size = new System.Drawing.Size(72, 17);
          this.cbDataOnly.TabIndex = 9;
          this.cbDataOnly.Text = "Data only";
          this.cbDataOnly.UseVisualStyleBackColor = true;
          // 
          // cbbCodepage
          // 
          this.cbbCodepage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.cbbCodepage.FormattingEnabled = true;
          this.cbbCodepage.Items.AddRange(new object[] {
            "Default",
            "OEM"});
          this.cbbCodepage.Location = new System.Drawing.Point(104, 20);
          this.cbbCodepage.Name = "cbbCodepage";
          this.cbbCodepage.Size = new System.Drawing.Size(144, 21);
          this.cbbCodepage.TabIndex = 8;
          // 
          // lblCodepage
          // 
          this.lblCodepage.AutoSize = true;
          this.lblCodepage.Location = new System.Drawing.Point(12, 24);
          this.lblCodepage.Name = "lblCodepage";
          this.lblCodepage.Size = new System.Drawing.Size(56, 13);
          this.lblCodepage.TabIndex = 7;
          this.lblCodepage.Text = "Codepage";
          // 
          // tbFieldNames
          // 
          this.tbFieldNames.Location = new System.Drawing.Point(12, 68);
          this.tbFieldNames.Name = "tbFieldNames";
          this.tbFieldNames.Size = new System.Drawing.Size(236, 20);
          this.tbFieldNames.TabIndex = 10;
          // 
          // lblFieldNames
          // 
          this.lblFieldNames.AutoSize = true;
          this.lblFieldNames.Location = new System.Drawing.Point(12, 48);
          this.lblFieldNames.Name = "lblFieldNames";
          this.lblFieldNames.Size = new System.Drawing.Size(202, 13);
          this.lblFieldNames.TabIndex = 11;
          this.lblFieldNames.Text = "Field names (for example, Field1;Field2):";
          // 
          // DbfExportForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.ClientSize = new System.Drawing.Size(276, 331);
          this.Name = "DbfExportForm";
          this.Text = "Export to dBase";
          this.gbPageRange.ResumeLayout(false);
          this.gbPageRange.PerformLayout();
          this.pcPages.ResumeLayout(false);
          this.panPages.ResumeLayout(false);
          this.gbOptions.ResumeLayout(false);
          this.gbOptions.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion // Windows Form Designer generated code

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.ComboBox cbbCodepage;
        private System.Windows.Forms.Label lblCodepage;
      private System.Windows.Forms.CheckBox cbDataOnly;
      private System.Windows.Forms.Label lblFieldNames;
      private System.Windows.Forms.TextBox tbFieldNames;
    }
}