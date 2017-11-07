namespace FastReport.Forms
{
  partial class TotalEditorForm
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
      this.lblFunction = new System.Windows.Forms.Label();
      this.cbxFunction = new System.Windows.Forms.ComboBox();
      this.lblDataBand = new System.Windows.Forms.Label();
      this.cbxDataBand = new FastReport.Controls.BandComboBox();
      this.lblDataColumnOrExpression = new System.Windows.Forms.Label();
      this.gbTotal = new System.Windows.Forms.GroupBox();
      this.cbResetRepeated = new System.Windows.Forms.CheckBox();
      this.cbResetAfterPrint = new System.Windows.Forms.CheckBox();
      this.cbxPrintOn = new FastReport.Controls.BandComboBox();
      this.tbTotalName = new System.Windows.Forms.TextBox();
      this.lblEvaluateCondition = new System.Windows.Forms.Label();
      this.cbxDataColumn = new FastReport.Controls.DataColumnComboBox();
      this.lblPrintOn = new System.Windows.Forms.Label();
      this.lblTotalName = new System.Windows.Forms.Label();
      this.cbInvisibleRows = new System.Windows.Forms.CheckBox();
      this.tbEvaluateCondition = new FastReport.Controls.TextBoxButton();
      this.gbOptions = new System.Windows.Forms.GroupBox();
      this.gbTotal.SuspendLayout();
      this.gbOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(200, 408);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(280, 408);
      // 
      // lblFunction
      // 
      this.lblFunction.AutoSize = true;
      this.lblFunction.Location = new System.Drawing.Point(12, 52);
      this.lblFunction.Name = "lblFunction";
      this.lblFunction.Size = new System.Drawing.Size(52, 13);
      this.lblFunction.TabIndex = 1;
      this.lblFunction.Text = "Function:";
      // 
      // cbxFunction
      // 
      this.cbxFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxFunction.FormattingEnabled = true;
      this.cbxFunction.Location = new System.Drawing.Point(116, 48);
      this.cbxFunction.Name = "cbxFunction";
      this.cbxFunction.Size = new System.Drawing.Size(219, 21);
      this.cbxFunction.TabIndex = 2;
      this.cbxFunction.SelectedIndexChanged += new System.EventHandler(this.cbxFunction_SelectedIndexChanged);
      // 
      // lblDataBand
      // 
      this.lblDataBand.AutoSize = true;
      this.lblDataBand.Location = new System.Drawing.Point(12, 132);
      this.lblDataBand.Name = "lblDataBand";
      this.lblDataBand.Size = new System.Drawing.Size(174, 13);
      this.lblDataBand.TabIndex = 3;
      this.lblDataBand.Text = "Evaluate on each row of the band:";
      // 
      // cbxDataBand
      // 
      this.cbxDataBand.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxDataBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxDataBand.FormattingEnabled = true;
      this.cbxDataBand.ItemHeight = 15;
      this.cbxDataBand.Location = new System.Drawing.Point(12, 152);
      this.cbxDataBand.Name = "cbxDataBand";
      this.cbxDataBand.Size = new System.Drawing.Size(324, 21);
      this.cbxDataBand.TabIndex = 4;
      this.cbxDataBand.SelectedIndexChanged += new System.EventHandler(this.cbxDataBand_SelectedIndexChanged);
      // 
      // lblDataColumnOrExpression
      // 
      this.lblDataColumnOrExpression.AutoSize = true;
      this.lblDataColumnOrExpression.Location = new System.Drawing.Point(12, 80);
      this.lblDataColumnOrExpression.Name = "lblDataColumnOrExpression";
      this.lblDataColumnOrExpression.Size = new System.Drawing.Size(138, 13);
      this.lblDataColumnOrExpression.TabIndex = 5;
      this.lblDataColumnOrExpression.Text = "Data column or expression:";
      // 
      // gbTotal
      // 
      this.gbTotal.Controls.Add(this.cbxPrintOn);
      this.gbTotal.Controls.Add(this.tbTotalName);
      this.gbTotal.Controls.Add(this.lblEvaluateCondition);
      this.gbTotal.Controls.Add(this.cbxDataColumn);
      this.gbTotal.Controls.Add(this.lblPrintOn);
      this.gbTotal.Controls.Add(this.lblTotalName);
      this.gbTotal.Controls.Add(this.cbxDataBand);
      this.gbTotal.Controls.Add(this.lblDataBand);
      this.gbTotal.Controls.Add(this.tbEvaluateCondition);
      this.gbTotal.Controls.Add(this.cbxFunction);
      this.gbTotal.Controls.Add(this.lblDataColumnOrExpression);
      this.gbTotal.Controls.Add(this.lblFunction);
      this.gbTotal.Location = new System.Drawing.Point(8, 4);
      this.gbTotal.Name = "gbTotal";
      this.gbTotal.Size = new System.Drawing.Size(348, 292);
      this.gbTotal.TabIndex = 10;
      this.gbTotal.TabStop = false;
      this.gbTotal.Text = "Total";
      // 
      // cbResetRepeated
      // 
      this.cbResetRepeated.AutoSize = true;
      this.cbResetRepeated.Location = new System.Drawing.Point(12, 44);
      this.cbResetRepeated.Name = "cbResetRepeated";
      this.cbResetRepeated.Size = new System.Drawing.Size(147, 17);
      this.cbResetRepeated.TabIndex = 15;
      this.cbResetRepeated.Text = "Reset if band is repeated";
      this.cbResetRepeated.UseVisualStyleBackColor = true;
      // 
      // cbResetAfterPrint
      // 
      this.cbResetAfterPrint.AutoSize = true;
      this.cbResetAfterPrint.Location = new System.Drawing.Point(12, 20);
      this.cbResetAfterPrint.Name = "cbResetAfterPrint";
      this.cbResetAfterPrint.Size = new System.Drawing.Size(106, 17);
      this.cbResetAfterPrint.TabIndex = 14;
      this.cbResetAfterPrint.Text = "Reset after print";
      this.cbResetAfterPrint.UseVisualStyleBackColor = true;
      // 
      // cbxPrintOn
      // 
      this.cbxPrintOn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxPrintOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPrintOn.FormattingEnabled = true;
      this.cbxPrintOn.ItemHeight = 15;
      this.cbxPrintOn.Location = new System.Drawing.Point(12, 256);
      this.cbxPrintOn.Name = "cbxPrintOn";
      this.cbxPrintOn.Size = new System.Drawing.Size(324, 21);
      this.cbxPrintOn.TabIndex = 12;
      // 
      // tbTotalName
      // 
      this.tbTotalName.Location = new System.Drawing.Point(116, 20);
      this.tbTotalName.Name = "tbTotalName";
      this.tbTotalName.Size = new System.Drawing.Size(219, 20);
      this.tbTotalName.TabIndex = 12;
      // 
      // lblEvaluateCondition
      // 
      this.lblEvaluateCondition.AutoSize = true;
      this.lblEvaluateCondition.Location = new System.Drawing.Point(12, 184);
      this.lblEvaluateCondition.Name = "lblEvaluateCondition";
      this.lblEvaluateCondition.Size = new System.Drawing.Size(203, 13);
      this.lblEvaluateCondition.TabIndex = 11;
      this.lblEvaluateCondition.Text = "Evaluate if the following condition is met:";
      // 
      // cbxDataColumn
      // 
      this.cbxDataColumn.Location = new System.Drawing.Point(12, 100);
      this.cbxDataColumn.Name = "cbxDataColumn";
      this.cbxDataColumn.Size = new System.Drawing.Size(324, 21);
      this.cbxDataColumn.TabIndex = 13;
      // 
      // lblPrintOn
      // 
      this.lblPrintOn.AutoSize = true;
      this.lblPrintOn.Location = new System.Drawing.Point(12, 236);
      this.lblPrintOn.Name = "lblPrintOn";
      this.lblPrintOn.Size = new System.Drawing.Size(94, 13);
      this.lblPrintOn.TabIndex = 9;
      this.lblPrintOn.Text = "Print on the band:";
      // 
      // lblTotalName
      // 
      this.lblTotalName.AutoSize = true;
      this.lblTotalName.Location = new System.Drawing.Point(12, 24);
      this.lblTotalName.Name = "lblTotalName";
      this.lblTotalName.Size = new System.Drawing.Size(64, 13);
      this.lblTotalName.TabIndex = 11;
      this.lblTotalName.Text = "Total name:";
      // 
      // cbInvisibleRows
      // 
      this.cbInvisibleRows.AutoSize = true;
      this.cbInvisibleRows.Location = new System.Drawing.Point(12, 68);
      this.cbInvisibleRows.Name = "cbInvisibleRows";
      this.cbInvisibleRows.Size = new System.Drawing.Size(127, 17);
      this.cbInvisibleRows.TabIndex = 10;
      this.cbInvisibleRows.Text = "Include invisible rows";
      this.cbInvisibleRows.UseVisualStyleBackColor = true;
      // 
      // tbEvaluateCondition
      // 
      this.tbEvaluateCondition.Image = null;
      this.tbEvaluateCondition.Location = new System.Drawing.Point(12, 204);
      this.tbEvaluateCondition.Name = "tbEvaluateCondition";
      this.tbEvaluateCondition.Size = new System.Drawing.Size(324, 21);
      this.tbEvaluateCondition.TabIndex = 8;
      this.tbEvaluateCondition.ButtonClick += new System.EventHandler(this.tbEvaluateCondition_ButtonClick);
      // 
      // gbOptions
      // 
      this.gbOptions.Controls.Add(this.cbResetRepeated);
      this.gbOptions.Controls.Add(this.cbResetAfterPrint);
      this.gbOptions.Controls.Add(this.cbInvisibleRows);
      this.gbOptions.Location = new System.Drawing.Point(8, 300);
      this.gbOptions.Name = "gbOptions";
      this.gbOptions.Size = new System.Drawing.Size(348, 96);
      this.gbOptions.TabIndex = 11;
      this.gbOptions.TabStop = false;
      this.gbOptions.Text = "Options";
      // 
      // TotalEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(364, 441);
      this.Controls.Add(this.gbOptions);
      this.Controls.Add(this.gbTotal);
      this.Name = "TotalEditorForm";
      this.ShowIcon = false;
      this.Text = "Edit Total";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TotalEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbTotal, 0);
      this.Controls.SetChildIndex(this.gbOptions, 0);
      this.gbTotal.ResumeLayout(false);
      this.gbTotal.PerformLayout();
      this.gbOptions.ResumeLayout(false);
      this.gbOptions.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblFunction;
    private System.Windows.Forms.ComboBox cbxFunction;
    private System.Windows.Forms.Label lblDataBand;
    private FastReport.Controls.BandComboBox cbxDataBand;
    private System.Windows.Forms.Label lblDataColumnOrExpression;
    private System.Windows.Forms.GroupBox gbTotal;
    private System.Windows.Forms.Label lblTotalName;
    private System.Windows.Forms.TextBox tbTotalName;
    private System.Windows.Forms.Label lblPrintOn;
    private FastReport.Controls.BandComboBox cbxPrintOn;
    private System.Windows.Forms.Label lblEvaluateCondition;
    private System.Windows.Forms.CheckBox cbInvisibleRows;
    private FastReport.Controls.TextBoxButton tbEvaluateCondition;
    private FastReport.Controls.DataColumnComboBox cbxDataColumn;
    private System.Windows.Forms.CheckBox cbResetAfterPrint;
    private System.Windows.Forms.CheckBox cbResetRepeated;
    private System.Windows.Forms.GroupBox gbOptions;
  }
}
