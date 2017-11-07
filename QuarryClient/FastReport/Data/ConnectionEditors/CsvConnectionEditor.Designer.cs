namespace FastReport.Data.ConnectionEditors
{
    partial class CsvConnectionEditor
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
            this.tbCsvFile = new FastReport.Controls.TextBoxButton();
            this.gbSelectDatabase = new System.Windows.Forms.GroupBox();
            this.cbxSeparator = new System.Windows.Forms.ComboBox();
            this.dgvTablePreview = new System.Windows.Forms.DataGridView();
            this.labelDataPreview = new System.Windows.Forms.Label();
            this.labelFilePreview = new System.Windows.Forms.Label();
            this.tbFilePreview = new System.Windows.Forms.TextBox();
            this.cbxFieldNames = new System.Windows.Forms.CheckBox();
            this.cbxRemoveQuotes = new System.Windows.Forms.CheckBox();
            this.cbxCodepage = new System.Windows.Forms.ComboBox();
            this.labelCodepage = new System.Windows.Forms.Label();
            this.tbSeparator = new System.Windows.Forms.TextBox();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelSelectCsvFile = new System.Windows.Forms.Label();
            this.cbxTryConvertTypes = new System.Windows.Forms.CheckBox();
            this.gbSelectDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCsvFile
            // 
            this.tbCsvFile.Image = null;
            this.tbCsvFile.Location = new System.Drawing.Point(12, 40);
            this.tbCsvFile.Name = "tbCsvFile";
            this.tbCsvFile.Size = new System.Drawing.Size(296, 21);
            this.tbCsvFile.TabIndex = 3;
            this.tbCsvFile.ButtonClick += new System.EventHandler(this.tbCsvFile_ButtonClick);
            // 
            // gbSelectDatabase
            // 
            this.gbSelectDatabase.Controls.Add(this.cbxTryConvertTypes);
            this.gbSelectDatabase.Controls.Add(this.cbxSeparator);
            this.gbSelectDatabase.Controls.Add(this.dgvTablePreview);
            this.gbSelectDatabase.Controls.Add(this.labelDataPreview);
            this.gbSelectDatabase.Controls.Add(this.labelFilePreview);
            this.gbSelectDatabase.Controls.Add(this.tbFilePreview);
            this.gbSelectDatabase.Controls.Add(this.cbxFieldNames);
            this.gbSelectDatabase.Controls.Add(this.cbxRemoveQuotes);
            this.gbSelectDatabase.Controls.Add(this.cbxCodepage);
            this.gbSelectDatabase.Controls.Add(this.labelCodepage);
            this.gbSelectDatabase.Controls.Add(this.tbSeparator);
            this.gbSelectDatabase.Controls.Add(this.labelSeparator);
            this.gbSelectDatabase.Controls.Add(this.labelSelectCsvFile);
            this.gbSelectDatabase.Controls.Add(this.tbCsvFile);
            this.gbSelectDatabase.Location = new System.Drawing.Point(8, 4);
            this.gbSelectDatabase.Name = "gbSelectDatabase";
            this.gbSelectDatabase.Size = new System.Drawing.Size(320, 455);
            this.gbSelectDatabase.TabIndex = 4;
            this.gbSelectDatabase.TabStop = false;
            this.gbSelectDatabase.Text = "Configure database";
            // 
            // cbxSeparator
            // 
            this.cbxSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSeparator.FormattingEnabled = true;
            this.cbxSeparator.Location = new System.Drawing.Point(168, 94);
            this.cbxSeparator.Name = "cbxSeparator";
            this.cbxSeparator.Size = new System.Drawing.Size(94, 21);
            this.cbxSeparator.TabIndex = 14;
            this.cbxSeparator.SelectedIndexChanged += new System.EventHandler(this.cbxSeparator_SelectedIndexChanged);
            // 
            // dgvTablePreview
            // 
            this.dgvTablePreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablePreview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTablePreview.Location = new System.Drawing.Point(12, 349);
            this.dgvTablePreview.Name = "dgvTablePreview";
            this.dgvTablePreview.ReadOnly = true;
            this.dgvTablePreview.Size = new System.Drawing.Size(296, 100);
            this.dgvTablePreview.TabIndex = 13;
            // 
            // labelDataPreview
            // 
            this.labelDataPreview.AutoSize = true;
            this.labelDataPreview.Location = new System.Drawing.Point(12, 333);
            this.labelDataPreview.Name = "labelDataPreview";
            this.labelDataPreview.Size = new System.Drawing.Size(75, 13);
            this.labelDataPreview.TabIndex = 12;
            this.labelDataPreview.Text = "Data preview:";
            // 
            // labelFilePreview
            // 
            this.labelFilePreview.AutoSize = true;
            this.labelFilePreview.Location = new System.Drawing.Point(12, 203);
            this.labelFilePreview.Name = "labelFilePreview";
            this.labelFilePreview.Size = new System.Drawing.Size(68, 13);
            this.labelFilePreview.TabIndex = 11;
            this.labelFilePreview.Text = "File preview:";
            // 
            // tbFilePreview
            // 
            this.tbFilePreview.Location = new System.Drawing.Point(12, 219);
            this.tbFilePreview.Multiline = true;
            this.tbFilePreview.Name = "tbFilePreview";
            this.tbFilePreview.ReadOnly = true;
            this.tbFilePreview.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbFilePreview.Size = new System.Drawing.Size(296, 100);
            this.tbFilePreview.TabIndex = 0;
            // 
            // cbxFieldNames
            // 
            this.cbxFieldNames.AutoSize = true;
            this.cbxFieldNames.Location = new System.Drawing.Point(15, 122);
            this.cbxFieldNames.Name = "cbxFieldNames";
            this.cbxFieldNames.Size = new System.Drawing.Size(145, 17);
            this.cbxFieldNames.TabIndex = 10;
            this.cbxFieldNames.Text = "Field names in first string";
            this.cbxFieldNames.UseVisualStyleBackColor = true;
            this.cbxFieldNames.CheckedChanged += new System.EventHandler(this.cbxFieldNames_CheckedChanged);
            // 
            // cbxRemoveQuotes
            // 
            this.cbxRemoveQuotes.AutoSize = true;
            this.cbxRemoveQuotes.Location = new System.Drawing.Point(15, 145);
            this.cbxRemoveQuotes.Name = "cbxRemoveQuotes";
            this.cbxRemoveQuotes.Size = new System.Drawing.Size(145, 17);
            this.cbxRemoveQuotes.TabIndex = 9;
            this.cbxRemoveQuotes.Text = "Remove quotation marks";
            this.cbxRemoveQuotes.UseVisualStyleBackColor = true;
            this.cbxRemoveQuotes.CheckedChanged += new System.EventHandler(this.cbxRemoveQuotes_CheckedChanged);
            // 
            // cbxCodepage
            // 
            this.cbxCodepage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCodepage.FormattingEnabled = true;
            this.cbxCodepage.Location = new System.Drawing.Point(168, 67);
            this.cbxCodepage.Name = "cbxCodepage";
            this.cbxCodepage.Size = new System.Drawing.Size(140, 21);
            this.cbxCodepage.TabIndex = 8;
            this.cbxCodepage.SelectedIndexChanged += new System.EventHandler(this.cbxEncoding_SelectedIndexChanged);
            // 
            // labelCodepage
            // 
            this.labelCodepage.AutoSize = true;
            this.labelCodepage.Location = new System.Drawing.Point(12, 70);
            this.labelCodepage.Name = "labelCodepage";
            this.labelCodepage.Size = new System.Drawing.Size(60, 13);
            this.labelCodepage.TabIndex = 7;
            this.labelCodepage.Text = "Codepage:";
            // 
            // tbSeparator
            // 
            this.tbSeparator.Enabled = false;
            this.tbSeparator.Location = new System.Drawing.Point(268, 94);
            this.tbSeparator.Name = "tbSeparator";
            this.tbSeparator.Size = new System.Drawing.Size(40, 20);
            this.tbSeparator.TabIndex = 6;
            this.tbSeparator.Text = ";";
            this.tbSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSeparator.TextChanged += new System.EventHandler(this.tbSeparator_TextChanged);
            // 
            // labelSeparator
            // 
            this.labelSeparator.AutoSize = true;
            this.labelSeparator.Location = new System.Drawing.Point(12, 97);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(59, 13);
            this.labelSeparator.TabIndex = 5;
            this.labelSeparator.Text = "Separator:";
            // 
            // labelSelectCsvFile
            // 
            this.labelSelectCsvFile.AutoSize = true;
            this.labelSelectCsvFile.Location = new System.Drawing.Point(12, 20);
            this.labelSelectCsvFile.Name = "labelSelectCsvFile";
            this.labelSelectCsvFile.Size = new System.Drawing.Size(80, 13);
            this.labelSelectCsvFile.TabIndex = 4;
            this.labelSelectCsvFile.Text = "Select .csv file:";
            // 
            // cbxTryConvertTypes
            // 
            this.cbxTryConvertTypes.AutoSize = true;
            this.cbxTryConvertTypes.Location = new System.Drawing.Point(15, 168);
            this.cbxTryConvertTypes.Name = "cbxTryConvertTypes";
            this.cbxTryConvertTypes.Size = new System.Drawing.Size(148, 17);
            this.cbxTryConvertTypes.TabIndex = 15;
            this.cbxTryConvertTypes.Text = "Try to convert field types";
            this.cbxTryConvertTypes.UseVisualStyleBackColor = true;
            this.cbxTryConvertTypes.CheckedChanged += new System.EventHandler(this.cbxTryConvertTypes_CheckedChanged);
            // 
            // CsvConnectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.gbSelectDatabase);
            this.Name = "CsvConnectionEditor";
            this.Size = new System.Drawing.Size(336, 467);
            this.gbSelectDatabase.ResumeLayout(false);
            this.gbSelectDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Controls.TextBoxButton tbCsvFile;
        private System.Windows.Forms.GroupBox gbSelectDatabase;
        private System.Windows.Forms.Label labelSelectCsvFile;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.TextBox tbSeparator;
        private System.Windows.Forms.ComboBox cbxCodepage;
        private System.Windows.Forms.Label labelCodepage;
        private System.Windows.Forms.CheckBox cbxRemoveQuotes;
        private System.Windows.Forms.CheckBox cbxFieldNames;
        private System.Windows.Forms.TextBox tbFilePreview;
        private System.Windows.Forms.Label labelFilePreview;
        private System.Windows.Forms.Label labelDataPreview;
        private System.Windows.Forms.DataGridView dgvTablePreview;
        private System.Windows.Forms.ComboBox cbxSeparator;
        private System.Windows.Forms.CheckBox cbxTryConvertTypes;

    }
}
