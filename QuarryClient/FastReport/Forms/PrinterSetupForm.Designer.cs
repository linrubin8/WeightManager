namespace FastReport.Forms
{
  partial class PrinterSetupForm
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
      this.gbPrinter = new System.Windows.Forms.GroupBox();
      this.cbxPrinter = new System.Windows.Forms.ComboBox();
      this.cbSavePrinter = new System.Windows.Forms.CheckBox();
      this.cbPrintToFile = new System.Windows.Forms.CheckBox();
      this.btnSettings = new System.Windows.Forms.Button();
      this.gbPageRange = new System.Windows.Forms.GroupBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.tbNumbers = new System.Windows.Forms.TextBox();
      this.rbNumbers = new System.Windows.Forms.RadioButton();
      this.rbCurrent = new System.Windows.Forms.RadioButton();
      this.rbAll = new System.Windows.Forms.RadioButton();
      this.gbCopies = new System.Windows.Forms.GroupBox();
      this.pnCollate = new System.Windows.Forms.Panel();
      this.cbCollate = new System.Windows.Forms.CheckBox();
      this.udCount = new System.Windows.Forms.NumericUpDown();
      this.lblCount = new System.Windows.Forms.Label();
      this.gbOther = new System.Windows.Forms.GroupBox();
      this.lblSource = new System.Windows.Forms.Label();
      this.lblDuplex = new System.Windows.Forms.Label();
      this.cbxSource = new System.Windows.Forms.ComboBox();
      this.cbxDuplex = new System.Windows.Forms.ComboBox();
      this.cbxOrder = new System.Windows.Forms.ComboBox();
      this.lblOrder = new System.Windows.Forms.Label();
      this.cbxOddEven = new System.Windows.Forms.ComboBox();
      this.lblOddEven = new System.Windows.Forms.Label();
      this.gbPrintMode = new System.Windows.Forms.GroupBox();
      this.cbxPrintMode = new System.Windows.Forms.ComboBox();
      this.lblPagesOnSheet = new System.Windows.Forms.Label();
      this.lblPrintOnSheet = new System.Windows.Forms.Label();
      this.cbxPagesOnSheet = new System.Windows.Forms.ComboBox();
      this.cbxPrintOnSheet = new System.Windows.Forms.ComboBox();
      this.btnMoreOptions = new System.Windows.Forms.Button();
      this.gbPrinter.SuspendLayout();
      this.gbPageRange.SuspendLayout();
      this.gbCopies.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCount)).BeginInit();
      this.gbOther.SuspendLayout();
      this.gbPrintMode.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(376, 356);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(456, 356);
      this.btnCancel.TabIndex = 1;
      // 
      // gbPrinter
      // 
      this.gbPrinter.Controls.Add(this.cbxPrinter);
      this.gbPrinter.Controls.Add(this.cbSavePrinter);
      this.gbPrinter.Controls.Add(this.cbPrintToFile);
      this.gbPrinter.Controls.Add(this.btnSettings);
      this.gbPrinter.Location = new System.Drawing.Point(8, 4);
      this.gbPrinter.Name = "gbPrinter";
      this.gbPrinter.Size = new System.Drawing.Size(524, 76);
      this.gbPrinter.TabIndex = 1;
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
      this.cbxPrinter.Size = new System.Drawing.Size(340, 21);
      this.cbxPrinter.TabIndex = 2;
      this.cbxPrinter.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxPrinter_DrawItem);
      this.cbxPrinter.SelectedIndexChanged += new System.EventHandler(this.cbxPrinter_SelectedIndexChanged);
      // 
      // cbSavePrinter
      // 
      this.cbSavePrinter.AutoSize = true;
      this.cbSavePrinter.Location = new System.Drawing.Point(12, 48);
      this.cbSavePrinter.Name = "cbSavePrinter";
      this.cbSavePrinter.Size = new System.Drawing.Size(185, 17);
      this.cbSavePrinter.TabIndex = 1;
      this.cbSavePrinter.Text = "Save this printer in the report file";
      this.cbSavePrinter.UseVisualStyleBackColor = true;
      // 
      // cbPrintToFile
      // 
      this.cbPrintToFile.AutoSize = true;
      this.cbPrintToFile.Location = new System.Drawing.Point(396, 49);
      this.cbPrintToFile.Name = "cbPrintToFile";
      this.cbPrintToFile.Size = new System.Drawing.Size(78, 17);
      this.cbPrintToFile.TabIndex = 1;
      this.cbPrintToFile.Text = "Print to file";
      this.cbPrintToFile.UseVisualStyleBackColor = true;
      // 
      // btnSettings
      // 
      this.btnSettings.Location = new System.Drawing.Point(396, 20);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new System.Drawing.Size(116, 23);
      this.btnSettings.TabIndex = 0;
      this.btnSettings.Text = "Settings...";
      this.btnSettings.UseVisualStyleBackColor = true;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
      // 
      // gbPageRange
      // 
      this.gbPageRange.Controls.Add(this.lblHint);
      this.gbPageRange.Controls.Add(this.tbNumbers);
      this.gbPageRange.Controls.Add(this.rbNumbers);
      this.gbPageRange.Controls.Add(this.rbCurrent);
      this.gbPageRange.Controls.Add(this.rbAll);
      this.gbPageRange.Location = new System.Drawing.Point(8, 84);
      this.gbPageRange.Name = "gbPageRange";
      this.gbPageRange.Size = new System.Drawing.Size(264, 128);
      this.gbPageRange.TabIndex = 2;
      this.gbPageRange.TabStop = false;
      this.gbPageRange.Text = "Page range";
      // 
      // lblHint
      // 
      this.lblHint.Location = new System.Drawing.Point(12, 88);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(240, 28);
      this.lblHint.TabIndex = 4;
      this.lblHint.Text = "Enter page numbers and/or page ranges, separated by commas. For example, 1,3,5-12" +
          "";
      // 
      // tbNumbers
      // 
      this.tbNumbers.Location = new System.Drawing.Point(108, 60);
      this.tbNumbers.Name = "tbNumbers";
      this.tbNumbers.Size = new System.Drawing.Size(145, 20);
      this.tbNumbers.TabIndex = 3;
      this.tbNumbers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNumbers_KeyPress);
      // 
      // rbNumbers
      // 
      this.rbNumbers.AutoSize = true;
      this.rbNumbers.Location = new System.Drawing.Point(12, 60);
      this.rbNumbers.Name = "rbNumbers";
      this.rbNumbers.Size = new System.Drawing.Size(71, 17);
      this.rbNumbers.TabIndex = 2;
      this.rbNumbers.TabStop = true;
      this.rbNumbers.Text = "Numbers:";
      this.rbNumbers.UseVisualStyleBackColor = true;
      // 
      // rbCurrent
      // 
      this.rbCurrent.AutoSize = true;
      this.rbCurrent.Enabled = false;
      this.rbCurrent.Location = new System.Drawing.Point(12, 40);
      this.rbCurrent.Name = "rbCurrent";
      this.rbCurrent.Size = new System.Drawing.Size(62, 17);
      this.rbCurrent.TabIndex = 1;
      this.rbCurrent.TabStop = true;
      this.rbCurrent.Text = "Current";
      this.rbCurrent.UseVisualStyleBackColor = true;
      this.rbCurrent.CheckedChanged += new System.EventHandler(this.rbCurrent_CheckedChanged);
      // 
      // rbAll
      // 
      this.rbAll.AutoSize = true;
      this.rbAll.Location = new System.Drawing.Point(12, 20);
      this.rbAll.Name = "rbAll";
      this.rbAll.Size = new System.Drawing.Size(36, 17);
      this.rbAll.TabIndex = 0;
      this.rbAll.TabStop = true;
      this.rbAll.Text = "All";
      this.rbAll.UseVisualStyleBackColor = true;
      this.rbAll.CheckedChanged += new System.EventHandler(this.rbCurrent_CheckedChanged);
      // 
      // gbCopies
      // 
      this.gbCopies.Controls.Add(this.pnCollate);
      this.gbCopies.Controls.Add(this.cbCollate);
      this.gbCopies.Controls.Add(this.udCount);
      this.gbCopies.Controls.Add(this.lblCount);
      this.gbCopies.Location = new System.Drawing.Point(280, 84);
      this.gbCopies.Name = "gbCopies";
      this.gbCopies.Size = new System.Drawing.Size(252, 128);
      this.gbCopies.TabIndex = 3;
      this.gbCopies.TabStop = false;
      this.gbCopies.Text = "Copies";
      // 
      // pnCollate
      // 
      this.pnCollate.Location = new System.Drawing.Point(16, 52);
      this.pnCollate.Name = "pnCollate";
      this.pnCollate.Size = new System.Drawing.Size(92, 60);
      this.pnCollate.TabIndex = 3;
      this.pnCollate.Paint += new System.Windows.Forms.PaintEventHandler(this.pnCollate_Paint);
      // 
      // cbCollate
      // 
      this.cbCollate.Location = new System.Drawing.Point(124, 48);
      this.cbCollate.Name = "cbCollate";
      this.cbCollate.Size = new System.Drawing.Size(100, 40);
      this.cbCollate.TabIndex = 2;
      this.cbCollate.Text = "Collate";
      this.cbCollate.UseVisualStyleBackColor = true;
      this.cbCollate.CheckedChanged += new System.EventHandler(this.cbCollate_CheckedChanged);
      // 
      // udCount
      // 
      this.udCount.Location = new System.Drawing.Point(124, 20);
      this.udCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.udCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.udCount.Name = "udCount";
      this.udCount.Size = new System.Drawing.Size(116, 20);
      this.udCount.TabIndex = 1;
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
      // gbOther
      // 
      this.gbOther.Controls.Add(this.lblSource);
      this.gbOther.Controls.Add(this.lblDuplex);
      this.gbOther.Controls.Add(this.cbxSource);
      this.gbOther.Controls.Add(this.cbxDuplex);
      this.gbOther.Controls.Add(this.cbxOrder);
      this.gbOther.Controls.Add(this.lblOrder);
      this.gbOther.Controls.Add(this.cbxOddEven);
      this.gbOther.Controls.Add(this.lblOddEven);
      this.gbOther.Location = new System.Drawing.Point(8, 216);
      this.gbOther.Name = "gbOther";
      this.gbOther.Size = new System.Drawing.Size(264, 124);
      this.gbOther.TabIndex = 4;
      this.gbOther.TabStop = false;
      this.gbOther.Text = "Other";
      // 
      // lblSource
      // 
      this.lblSource.AutoSize = true;
      this.lblSource.Location = new System.Drawing.Point(12, 96);
      this.lblSource.Name = "lblSource";
      this.lblSource.Size = new System.Drawing.Size(40, 13);
      this.lblSource.TabIndex = 5;
      this.lblSource.Text = "Source";
      // 
      // lblDuplex
      // 
      this.lblDuplex.AutoSize = true;
      this.lblDuplex.Location = new System.Drawing.Point(12, 72);
      this.lblDuplex.Name = "lblDuplex";
      this.lblDuplex.Size = new System.Drawing.Size(40, 13);
      this.lblDuplex.TabIndex = 5;
      this.lblDuplex.Text = "Duplex";
      // 
      // cbxSource
      // 
      this.cbxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxSource.FormattingEnabled = true;
      this.cbxSource.Location = new System.Drawing.Point(108, 92);
      this.cbxSource.Name = "cbxSource";
      this.cbxSource.Size = new System.Drawing.Size(145, 21);
      this.cbxSource.TabIndex = 4;
      // 
      // cbxDuplex
      // 
      this.cbxDuplex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxDuplex.FormattingEnabled = true;
      this.cbxDuplex.Location = new System.Drawing.Point(108, 68);
      this.cbxDuplex.Name = "cbxDuplex";
      this.cbxDuplex.Size = new System.Drawing.Size(145, 21);
      this.cbxDuplex.TabIndex = 4;
      // 
      // cbxOrder
      // 
      this.cbxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxOrder.FormattingEnabled = true;
      this.cbxOrder.Location = new System.Drawing.Point(108, 44);
      this.cbxOrder.Name = "cbxOrder";
      this.cbxOrder.Size = new System.Drawing.Size(145, 21);
      this.cbxOrder.TabIndex = 3;
      // 
      // lblOrder
      // 
      this.lblOrder.AutoSize = true;
      this.lblOrder.Location = new System.Drawing.Point(12, 48);
      this.lblOrder.Name = "lblOrder";
      this.lblOrder.Size = new System.Drawing.Size(35, 13);
      this.lblOrder.TabIndex = 2;
      this.lblOrder.Text = "Order";
      // 
      // cbxOddEven
      // 
      this.cbxOddEven.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxOddEven.FormattingEnabled = true;
      this.cbxOddEven.Location = new System.Drawing.Point(108, 20);
      this.cbxOddEven.Name = "cbxOddEven";
      this.cbxOddEven.Size = new System.Drawing.Size(145, 21);
      this.cbxOddEven.TabIndex = 1;
      // 
      // lblOddEven
      // 
      this.lblOddEven.AutoSize = true;
      this.lblOddEven.Location = new System.Drawing.Point(12, 24);
      this.lblOddEven.Name = "lblOddEven";
      this.lblOddEven.Size = new System.Drawing.Size(29, 13);
      this.lblOddEven.TabIndex = 0;
      this.lblOddEven.Text = "Print";
      // 
      // gbPrintMode
      // 
      this.gbPrintMode.Controls.Add(this.cbxPrintMode);
      this.gbPrintMode.Controls.Add(this.lblPagesOnSheet);
      this.gbPrintMode.Controls.Add(this.lblPrintOnSheet);
      this.gbPrintMode.Controls.Add(this.cbxPagesOnSheet);
      this.gbPrintMode.Controls.Add(this.cbxPrintOnSheet);
      this.gbPrintMode.Location = new System.Drawing.Point(280, 216);
      this.gbPrintMode.Name = "gbPrintMode";
      this.gbPrintMode.Size = new System.Drawing.Size(252, 124);
      this.gbPrintMode.TabIndex = 5;
      this.gbPrintMode.TabStop = false;
      this.gbPrintMode.Text = "Print mode";
      // 
      // cbxPrintMode
      // 
      this.cbxPrintMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPrintMode.FormattingEnabled = true;
      this.cbxPrintMode.ItemHeight = 39;
      this.cbxPrintMode.Location = new System.Drawing.Point(12, 20);
      this.cbxPrintMode.Name = "cbxPrintMode";
      this.cbxPrintMode.Size = new System.Drawing.Size(228, 45);
      this.cbxPrintMode.TabIndex = 2;
      this.cbxPrintMode.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxPrintMode_DrawItem);
      this.cbxPrintMode.SelectedIndexChanged += new System.EventHandler(this.cbxPrintMode_SelectedIndexChanged);
      // 
      // lblPagesOnSheet
      // 
      this.lblPagesOnSheet.AutoSize = true;
      this.lblPagesOnSheet.Location = new System.Drawing.Point(12, 96);
      this.lblPagesOnSheet.Name = "lblPagesOnSheet";
      this.lblPagesOnSheet.Size = new System.Drawing.Size(81, 13);
      this.lblPagesOnSheet.TabIndex = 1;
      this.lblPagesOnSheet.Text = "Pages on sheet";
      // 
      // lblPrintOnSheet
      // 
      this.lblPrintOnSheet.AutoSize = true;
      this.lblPrintOnSheet.Location = new System.Drawing.Point(12, 72);
      this.lblPrintOnSheet.Name = "lblPrintOnSheet";
      this.lblPrintOnSheet.Size = new System.Drawing.Size(74, 13);
      this.lblPrintOnSheet.TabIndex = 1;
      this.lblPrintOnSheet.Text = "Print on sheet";
      // 
      // cbxPagesOnSheet
      // 
      this.cbxPagesOnSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPagesOnSheet.FormattingEnabled = true;
      this.cbxPagesOnSheet.Location = new System.Drawing.Point(124, 92);
      this.cbxPagesOnSheet.Name = "cbxPagesOnSheet";
      this.cbxPagesOnSheet.Size = new System.Drawing.Size(116, 21);
      this.cbxPagesOnSheet.TabIndex = 0;
      // 
      // cbxPrintOnSheet
      // 
      this.cbxPrintOnSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPrintOnSheet.FormattingEnabled = true;
      this.cbxPrintOnSheet.Location = new System.Drawing.Point(124, 68);
      this.cbxPrintOnSheet.Name = "cbxPrintOnSheet";
      this.cbxPrintOnSheet.Size = new System.Drawing.Size(116, 21);
      this.cbxPrintOnSheet.TabIndex = 0;
      // 
      // btnMoreOptions
      // 
      this.btnMoreOptions.Location = new System.Drawing.Point(8, 356);
      this.btnMoreOptions.Name = "btnMoreOptions";
      this.btnMoreOptions.Size = new System.Drawing.Size(101, 23);
      this.btnMoreOptions.TabIndex = 1;
      this.btnMoreOptions.Text = "More options";
      this.btnMoreOptions.UseVisualStyleBackColor = true;
      this.btnMoreOptions.Click += new System.EventHandler(this.btnMoreOptions_Click);
      // 
      // PrinterSetupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(541, 392);
      this.Controls.Add(this.gbPrintMode);
      this.Controls.Add(this.gbOther);
      this.Controls.Add(this.btnMoreOptions);
      this.Controls.Add(this.gbPrinter);
      this.Controls.Add(this.gbPageRange);
      this.Controls.Add(this.gbCopies);
      this.Name = "PrinterSetupForm";
      this.Text = "Print";
      this.Shown += new System.EventHandler(this.PrinterSetupForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PrinterSetupForm_FormClosed);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrinterSetupForm_FormClosing);
      this.Controls.SetChildIndex(this.gbCopies, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbPageRange, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbPrinter, 0);
      this.Controls.SetChildIndex(this.btnMoreOptions, 0);
      this.Controls.SetChildIndex(this.gbOther, 0);
      this.Controls.SetChildIndex(this.gbPrintMode, 0);
      this.gbPrinter.ResumeLayout(false);
      this.gbPrinter.PerformLayout();
      this.gbPageRange.ResumeLayout(false);
      this.gbPageRange.PerformLayout();
      this.gbCopies.ResumeLayout(false);
      this.gbCopies.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCount)).EndInit();
      this.gbOther.ResumeLayout(false);
      this.gbOther.PerformLayout();
      this.gbPrintMode.ResumeLayout(false);
      this.gbPrintMode.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbPrinter;
    private System.Windows.Forms.CheckBox cbPrintToFile;
    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.GroupBox gbPageRange;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.TextBox tbNumbers;
    private System.Windows.Forms.RadioButton rbNumbers;
    private System.Windows.Forms.RadioButton rbCurrent;
    private System.Windows.Forms.RadioButton rbAll;
    private System.Windows.Forms.GroupBox gbCopies;
    private System.Windows.Forms.CheckBox cbCollate;
    private System.Windows.Forms.NumericUpDown udCount;
    private System.Windows.Forms.Label lblCount;
    private System.Windows.Forms.ComboBox cbxPrinter;
    private System.Windows.Forms.GroupBox gbOther;
    private System.Windows.Forms.Label lblDuplex;
    private System.Windows.Forms.ComboBox cbxDuplex;
    private System.Windows.Forms.ComboBox cbxOrder;
    private System.Windows.Forms.Label lblOrder;
    private System.Windows.Forms.ComboBox cbxOddEven;
    private System.Windows.Forms.Label lblOddEven;
    private System.Windows.Forms.GroupBox gbPrintMode;
    private System.Windows.Forms.Label lblPrintOnSheet;
    private System.Windows.Forms.ComboBox cbxPrintOnSheet;
    private System.Windows.Forms.ComboBox cbxPrintMode;
    private System.Windows.Forms.Label lblSource;
    private System.Windows.Forms.ComboBox cbxSource;
    private System.Windows.Forms.Label lblPagesOnSheet;
    private System.Windows.Forms.ComboBox cbxPagesOnSheet;
    private System.Windows.Forms.Button btnMoreOptions;
    private System.Windows.Forms.CheckBox cbSavePrinter;
    private System.Windows.Forms.Panel pnCollate;
  }
}
