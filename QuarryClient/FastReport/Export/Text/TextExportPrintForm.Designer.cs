namespace FastReport.Export.Text
{
    partial class TextExportPrintForm
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
            this.gbPageRange = new System.Windows.Forms.GroupBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.tbNumbers = new System.Windows.Forms.TextBox();
            this.rbNumbers = new System.Windows.Forms.RadioButton();
            this.rbCurrent = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.cbcbbCommands = new System.Windows.Forms.CheckedListBox();
            this.lblCommands = new System.Windows.Forms.Label();
            this.cbxPrinterTypes = new System.Windows.Forms.ComboBox();
            this.lblPrinterType = new System.Windows.Forms.Label();
            this.gbPrinter = new System.Windows.Forms.GroupBox();
            this.cbxPrinter = new System.Windows.Forms.ComboBox();
            this.gbCopies = new System.Windows.Forms.GroupBox();
            this.udCount = new System.Windows.Forms.NumericUpDown();
            this.lblCount = new System.Windows.Forms.Label();
            this.gbPageRange.SuspendLayout();
            this.gbOther.SuspendLayout();
            this.gbPrinter.SuspendLayout();
            this.gbCopies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(380, 260);
            this.btnOk.TabIndex = 10;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(460, 260);
            this.btnCancel.TabIndex = 11;
            // 
            // gbPageRange
            // 
            this.gbPageRange.Controls.Add(this.lblHint);
            this.gbPageRange.Controls.Add(this.tbNumbers);
            this.gbPageRange.Controls.Add(this.rbNumbers);
            this.gbPageRange.Controls.Add(this.rbCurrent);
            this.gbPageRange.Controls.Add(this.rbAll);
            this.gbPageRange.Location = new System.Drawing.Point(8, 120);
            this.gbPageRange.Name = "gbPageRange";
            this.gbPageRange.Size = new System.Drawing.Size(260, 128);
            this.gbPageRange.TabIndex = 4;
            this.gbPageRange.TabStop = false;
            this.gbPageRange.Text = "Page range";
            // 
            // lblHint
            // 
            this.lblHint.Location = new System.Drawing.Point(12, 88);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(236, 28);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Enter page numbers and/or page ranges, separated by commas. For example, 1,3,5-12" +
                "";
            // 
            // tbNumbers
            // 
            this.tbNumbers.Location = new System.Drawing.Point(104, 60);
            this.tbNumbers.Name = "tbNumbers";
            this.tbNumbers.Size = new System.Drawing.Size(144, 20);
            this.tbNumbers.TabIndex = 6;
            this.tbNumbers.TextChanged += new System.EventHandler(this.tbNumbers_TextChanged);
            // 
            // rbNumbers
            // 
            this.rbNumbers.AutoSize = true;
            this.rbNumbers.Location = new System.Drawing.Point(12, 60);
            this.rbNumbers.Name = "rbNumbers";
            this.rbNumbers.Size = new System.Drawing.Size(71, 17);
            this.rbNumbers.TabIndex = 5;
            this.rbNumbers.TabStop = true;
            this.rbNumbers.Text = "Numbers:";
            this.rbNumbers.UseVisualStyleBackColor = true;
            // 
            // rbCurrent
            // 
            this.rbCurrent.AutoSize = true;
            this.rbCurrent.Location = new System.Drawing.Point(12, 40);
            this.rbCurrent.Name = "rbCurrent";
            this.rbCurrent.Size = new System.Drawing.Size(62, 17);
            this.rbCurrent.TabIndex = 4;
            this.rbCurrent.TabStop = true;
            this.rbCurrent.Text = "Current";
            this.rbCurrent.UseVisualStyleBackColor = true;
            this.rbCurrent.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(12, 20);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // gbOther
            // 
            this.gbOther.Controls.Add(this.cbcbbCommands);
            this.gbOther.Controls.Add(this.lblCommands);
            this.gbOther.Controls.Add(this.cbxPrinterTypes);
            this.gbOther.Controls.Add(this.lblPrinterType);
            this.gbOther.Location = new System.Drawing.Point(276, 60);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(260, 188);
            this.gbOther.TabIndex = 7;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "Other";
            // 
            // cbcbbCommands
            // 
            this.cbcbbCommands.CheckOnClick = true;
            this.cbcbbCommands.FormattingEnabled = true;
            this.cbcbbCommands.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbcbbCommands.Location = new System.Drawing.Point(92, 52);
            this.cbcbbCommands.Name = "cbcbbCommands";
            this.cbcbbCommands.Size = new System.Drawing.Size(156, 124);
            this.cbcbbCommands.TabIndex = 8;
            this.cbcbbCommands.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbcbbCommands_ItemCheck);
            // 
            // lblCommands
            // 
            this.lblCommands.AutoSize = true;
            this.lblCommands.Location = new System.Drawing.Point(12, 52);
            this.lblCommands.Name = "lblCommands";
            this.lblCommands.Size = new System.Drawing.Size(59, 13);
            this.lblCommands.TabIndex = 6;
            this.lblCommands.Text = "Commands";
            // 
            // cbxPrinterTypes
            // 
            this.cbxPrinterTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrinterTypes.FormattingEnabled = true;
            this.cbxPrinterTypes.Location = new System.Drawing.Point(92, 20);
            this.cbxPrinterTypes.Name = "cbxPrinterTypes";
            this.cbxPrinterTypes.Size = new System.Drawing.Size(156, 21);
            this.cbxPrinterTypes.TabIndex = 7;
            this.cbxPrinterTypes.SelectedIndexChanged += new System.EventHandler(this.cbxPrinterTypes_SelectedIndexChanged);
            // 
            // lblPrinterType
            // 
            this.lblPrinterType.AutoSize = true;
            this.lblPrinterType.Location = new System.Drawing.Point(12, 24);
            this.lblPrinterType.Name = "lblPrinterType";
            this.lblPrinterType.Size = new System.Drawing.Size(64, 13);
            this.lblPrinterType.TabIndex = 4;
            this.lblPrinterType.Text = "Printer type";
            // 
            // gbPrinter
            // 
            this.gbPrinter.Controls.Add(this.cbxPrinter);
            this.gbPrinter.Location = new System.Drawing.Point(8, 4);
            this.gbPrinter.Name = "gbPrinter";
            this.gbPrinter.Size = new System.Drawing.Size(528, 52);
            this.gbPrinter.TabIndex = 8;
            this.gbPrinter.TabStop = false;
            this.gbPrinter.Text = "Printer";
            // 
            // cbxPrinter
            // 
            this.cbxPrinter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrinter.FormattingEnabled = true;
            this.cbxPrinter.Location = new System.Drawing.Point(12, 20);
            this.cbxPrinter.Name = "cbxPrinter";
            this.cbxPrinter.Size = new System.Drawing.Size(504, 21);
            this.cbxPrinter.TabIndex = 1;
            this.cbxPrinter.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxPrinter_DrawItem);
            // 
            // gbCopies
            // 
            this.gbCopies.Controls.Add(this.udCount);
            this.gbCopies.Controls.Add(this.lblCount);
            this.gbCopies.Location = new System.Drawing.Point(8, 60);
            this.gbCopies.Name = "gbCopies";
            this.gbCopies.Size = new System.Drawing.Size(260, 56);
            this.gbCopies.TabIndex = 9;
            this.gbCopies.TabStop = false;
            this.gbCopies.Text = "Copies";
            // 
            // udCount
            // 
            this.udCount.Location = new System.Drawing.Point(140, 20);
            this.udCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udCount.Name = "udCount";
            this.udCount.Size = new System.Drawing.Size(108, 20);
            this.udCount.TabIndex = 2;
            this.udCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(12, 24);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(40, 13);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "Count:";
            // 
            // TextExportPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(545, 294);
            this.Controls.Add(this.gbPrinter);
            this.Controls.Add(this.gbCopies);
            this.Controls.Add(this.gbOther);
            this.Controls.Add(this.gbPageRange);
            this.Name = "TextExportPrintForm";
            this.Text = "Print";
            this.Shown += new System.EventHandler(this.TextExportPrintForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextExportPrintForm_FormClosing);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.gbPageRange, 0);
            this.Controls.SetChildIndex(this.gbOther, 0);
            this.Controls.SetChildIndex(this.gbCopies, 0);
            this.Controls.SetChildIndex(this.gbPrinter, 0);
            this.gbPageRange.ResumeLayout(false);
            this.gbPageRange.PerformLayout();
            this.gbOther.ResumeLayout(false);
            this.gbOther.PerformLayout();
            this.gbPrinter.ResumeLayout(false);
            this.gbCopies.ResumeLayout(false);
            this.gbCopies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbPageRange;
        protected System.Windows.Forms.Label lblHint;
        protected System.Windows.Forms.TextBox tbNumbers;
        protected System.Windows.Forms.RadioButton rbNumbers;
        protected System.Windows.Forms.RadioButton rbCurrent;
        protected System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.GroupBox gbOther;
        private System.Windows.Forms.CheckedListBox cbcbbCommands;
        private System.Windows.Forms.Label lblCommands;
        private System.Windows.Forms.ComboBox cbxPrinterTypes;
        private System.Windows.Forms.Label lblPrinterType;
        private System.Windows.Forms.GroupBox gbPrinter;
        private System.Windows.Forms.ComboBox cbxPrinter;
        private System.Windows.Forms.GroupBox gbCopies;
        private System.Windows.Forms.NumericUpDown udCount;
        private System.Windows.Forms.Label lblCount;
    }
}
