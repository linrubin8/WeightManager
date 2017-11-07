namespace FastReport.Forms
{
    partial class PSExportForm
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
            this.chCurves = new System.Windows.Forms.CheckBox();
            this.gbPageRange.SuspendLayout();
            this.panPages.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPageRange
            // 
            this.gbPageRange.Location = new System.Drawing.Point(8, 4);
            this.gbPageRange.Size = new System.Drawing.Size(260, 141);
            // 
            // panPages
            // 
            this.panPages.Dock = System.Windows.Forms.DockStyle.None;
            this.panPages.Size = new System.Drawing.Size(276, 148);
            // 
            // cbOpenAfter
            // 
            this.cbOpenAfter.Location = new System.Drawing.Point(35, 264);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(125, 298);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 298);
            this.btnCancel.TabIndex = 1;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chCurves);
            this.gbOptions.Controls.Add(this.lblImageFormat);
            this.gbOptions.Controls.Add(this.comboBox1);
            this.gbOptions.Location = new System.Drawing.Point(20, 161);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(260, 97);
            this.gbOptions.TabIndex = 7;
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
            // chCurves
            // 
            this.chCurves.AutoSize = true;
            this.chCurves.Location = new System.Drawing.Point(15, 58);
            this.chCurves.Name = "chCurves";
            this.chCurves.Size = new System.Drawing.Size(94, 17);
            this.chCurves.TabIndex = 7;
            this.chCurves.Text = "Text in curves";
            this.chCurves.UseVisualStyleBackColor = true;
            // 
            // PSExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(300, 339);
            this.Controls.Add(this.gbOptions);
            this.Name = "PSExportForm";
            this.Text = "Export to PostScript";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.gbOptions, 0);
            this.Controls.SetChildIndex(this.cbOpenAfter, 0);
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
        private System.Windows.Forms.CheckBox chCurves;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
