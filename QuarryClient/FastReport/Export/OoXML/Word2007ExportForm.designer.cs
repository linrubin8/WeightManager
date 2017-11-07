namespace FastReport.Forms
{
    partial class Word2007ExportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbRh = new System.Windows.Forms.ComboBox();
            this.cbWysiwyg = new System.Windows.Forms.CheckBox();
            this.radioButtonLayers = new System.Windows.Forms.RadioButton();
            this.radioButtonTable = new System.Windows.Forms.RadioButton();
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
            this.pcPages.Size = new System.Drawing.Size(276, 255);
            // 
            // panPages
            // 
            this.panPages.Controls.Add(this.gbOptions);
            this.panPages.Size = new System.Drawing.Size(276, 255);
            this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
            this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
            // 
            // cbOpenAfter
            // 
            this.cbOpenAfter.Location = new System.Drawing.Point(8, 261);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(112, 285);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(193, 285);
            this.btnCancel.TabIndex = 1;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.label1);
            this.gbOptions.Controls.Add(this.cbRh);
            this.gbOptions.Controls.Add(this.cbWysiwyg);
            this.gbOptions.Controls.Add(this.radioButtonLayers);
            this.gbOptions.Controls.Add(this.radioButtonTable);
            this.gbOptions.Location = new System.Drawing.Point(8, 136);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(260, 116);
            this.gbOptions.TabIndex = 5;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Row height is";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // cbRh
            // 
            this.cbRh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRh.FormattingEnabled = true;
            this.cbRh.Items.AddRange(new object[] {
            "Exactly",
            "Minimum"});
            this.cbRh.Location = new System.Drawing.Point(127, 73);
            this.cbRh.Name = "cbRh";
            this.cbRh.Size = new System.Drawing.Size(121, 21);
            this.cbRh.TabIndex = 3;
            this.cbRh.Visible = false;
            // 
            // cbWysiwyg
            // 
            this.cbWysiwyg.AutoSize = true;
            this.cbWysiwyg.Location = new System.Drawing.Point(136, 42);
            this.cbWysiwyg.Name = "cbWysiwyg";
            this.cbWysiwyg.Size = new System.Drawing.Size(69, 17);
            this.cbWysiwyg.TabIndex = 2;
            this.cbWysiwyg.Text = "Wysiwyg";
            this.cbWysiwyg.UseVisualStyleBackColor = true;
            this.cbWysiwyg.CheckedChanged += new System.EventHandler(this.cbWysiwyg_CheckedChanged);
            // 
            // radioButtonLayers
            // 
            this.radioButtonLayers.AutoSize = true;
            this.radioButtonLayers.Location = new System.Drawing.Point(12, 42);
            this.radioButtonLayers.Name = "radioButtonLayers";
            this.radioButtonLayers.Size = new System.Drawing.Size(119, 17);
            this.radioButtonLayers.TabIndex = 1;
            this.radioButtonLayers.Text = "Layer based export";
            this.radioButtonLayers.UseVisualStyleBackColor = true;
            // 
            // radioButtonTable
            // 
            this.radioButtonTable.AutoSize = true;
            this.radioButtonTable.Checked = true;
            this.radioButtonTable.Location = new System.Drawing.Point(12, 19);
            this.radioButtonTable.Name = "radioButtonTable";
            this.radioButtonTable.Size = new System.Drawing.Size(118, 17);
            this.radioButtonTable.TabIndex = 0;
            this.radioButtonTable.TabStop = true;
            this.radioButtonTable.Text = "Table based export";
            this.radioButtonTable.UseVisualStyleBackColor = true;
            this.radioButtonTable.CheckedChanged += new System.EventHandler(this.radioButtonTable_CheckedChanged);
            // 
            // Word2007ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(276, 320);
            this.Name = "Word2007ExportForm";
            this.OpenAfterVisible = true;
            this.Text = "Export to MS Word 2007";
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
        private System.Windows.Forms.RadioButton radioButtonLayers;
        private System.Windows.Forms.RadioButton radioButtonTable;
        private System.Windows.Forms.CheckBox cbWysiwyg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRh;

    }
}
