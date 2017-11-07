namespace FastReport.Forms
{
  partial class FormatEditorForm
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
      this.pc1 = new FastReport.Controls.PageControl();
      this.pnGeneral = new FastReport.Controls.PageControlPage();
      this.pnNumber = new FastReport.Controls.PageControlPage();
      this.cbxNumberNegativePattern = new System.Windows.Forms.ComboBox();
      this.cbxNumberGroupSeparator = new System.Windows.Forms.ComboBox();
      this.cbNumberUseLocale = new System.Windows.Forms.CheckBox();
      this.cbxNumberDecimalSeparator = new System.Windows.Forms.ComboBox();
      this.lblNumberDecimalDigits = new System.Windows.Forms.Label();
      this.udNumberDecimalDigits = new System.Windows.Forms.NumericUpDown();
      this.lblNumberDecimalSeparator = new System.Windows.Forms.Label();
      this.lblNumberNegativePattern = new System.Windows.Forms.Label();
      this.lblNumberGroupSeparator = new System.Windows.Forms.Label();
      this.pnCurrency = new FastReport.Controls.PageControlPage();
      this.cbxCurrencySymbol = new System.Windows.Forms.ComboBox();
      this.cbxCurrencyNegativePattern = new System.Windows.Forms.ComboBox();
      this.cbCurrencyUseLocale = new System.Windows.Forms.CheckBox();
      this.cbxCurrencyPositivePattern = new System.Windows.Forms.ComboBox();
      this.lblCurrencyDecimalDigits = new System.Windows.Forms.Label();
      this.cbxCurrencyGroupSeparator = new System.Windows.Forms.ComboBox();
      this.lblCurrencyDecimalSeparator = new System.Windows.Forms.Label();
      this.cbxCurrencyDecimalSeparator = new System.Windows.Forms.ComboBox();
      this.lblCurrencyGroupSeparator = new System.Windows.Forms.Label();
      this.udCurrencyDecimalDigits = new System.Windows.Forms.NumericUpDown();
      this.lblCurrencyPositivePattern = new System.Windows.Forms.Label();
      this.lblCurrencySymbol = new System.Windows.Forms.Label();
      this.lblCurrencyNegativePattern = new System.Windows.Forms.Label();
      this.pnDate = new FastReport.Controls.PageControlPage();
      this.lbxDates = new System.Windows.Forms.ListBox();
      this.pnTime = new FastReport.Controls.PageControlPage();
      this.lbxTimes = new System.Windows.Forms.ListBox();
      this.pnPercent = new FastReport.Controls.PageControlPage();
      this.cbxPercentPositivePattern = new System.Windows.Forms.ComboBox();
      this.lblPercentPositivePattern = new System.Windows.Forms.Label();
      this.cbPercentUseLocale = new System.Windows.Forms.CheckBox();
      this.cbxPercentSymbol = new System.Windows.Forms.ComboBox();
      this.lblPercentDecimalDigits = new System.Windows.Forms.Label();
      this.cbxPercentNegativePattern = new System.Windows.Forms.ComboBox();
      this.lblPercentDecimalSeparator = new System.Windows.Forms.Label();
      this.cbxPercentGroupSeparator = new System.Windows.Forms.ComboBox();
      this.lblPercentGroupSeparator = new System.Windows.Forms.Label();
      this.cbxPercentDecimalSeparator = new System.Windows.Forms.ComboBox();
      this.lblPercentNegativePattern = new System.Windows.Forms.Label();
      this.udPercentDecimalDigits = new System.Windows.Forms.NumericUpDown();
      this.lblPercentSymbol = new System.Windows.Forms.Label();
      this.pnBoolean = new FastReport.Controls.PageControlPage();
      this.cbxBooleanTrue = new System.Windows.Forms.ComboBox();
      this.cbxBooleanFalse = new System.Windows.Forms.ComboBox();
      this.lblBooleanFalse = new System.Windows.Forms.Label();
      this.lblBooleanTrue = new System.Windows.Forms.Label();
      this.pnCustom = new FastReport.Controls.PageControlPage();
      this.lbxCustom = new System.Windows.Forms.ListBox();
      this.tbCustom = new System.Windows.Forms.TextBox();
      this.gbSample = new System.Windows.Forms.GroupBox();
      this.lblSample = new System.Windows.Forms.Label();
      this.lblExpression = new System.Windows.Forms.Label();
      this.cbxExpression = new System.Windows.Forms.ComboBox();
      this.pc1.SuspendLayout();
      this.pnNumber.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udNumberDecimalDigits)).BeginInit();
      this.pnCurrency.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCurrencyDecimalDigits)).BeginInit();
      this.pnDate.SuspendLayout();
      this.pnTime.SuspendLayout();
      this.pnPercent.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPercentDecimalDigits)).BeginInit();
      this.pnBoolean.SuspendLayout();
      this.pnCustom.SuspendLayout();
      this.gbSample.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(232, 348);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(312, 348);
      // 
      // pc1
      // 
      this.pc1.Controls.Add(this.pnGeneral);
      this.pc1.Controls.Add(this.pnNumber);
      this.pc1.Controls.Add(this.pnCurrency);
      this.pc1.Controls.Add(this.pnDate);
      this.pc1.Controls.Add(this.pnTime);
      this.pc1.Controls.Add(this.pnPercent);
      this.pc1.Controls.Add(this.pnBoolean);
      this.pc1.Controls.Add(this.pnCustom);
      this.pc1.Location = new System.Drawing.Point(12, 44);
      this.pc1.Name = "pc1";
      this.pc1.SelectorWidth = 128;
      this.pc1.Size = new System.Drawing.Size(376, 292);
      this.pc1.TabIndex = 1;
      this.pc1.Text = "pageControl1";
      this.pc1.PageSelected += new System.EventHandler(this.pc1_PageSelected);
      // 
      // pnGeneral
      // 
      this.pnGeneral.BackColor = System.Drawing.SystemColors.Window;
      this.pnGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnGeneral.Location = new System.Drawing.Point(128, 1);
      this.pnGeneral.Name = "pnGeneral";
      this.pnGeneral.Size = new System.Drawing.Size(247, 290);
      this.pnGeneral.TabIndex = 0;
      this.pnGeneral.Text = "General";
      // 
      // pnNumber
      // 
      this.pnNumber.BackColor = System.Drawing.SystemColors.Window;
      this.pnNumber.Controls.Add(this.cbxNumberNegativePattern);
      this.pnNumber.Controls.Add(this.cbxNumberGroupSeparator);
      this.pnNumber.Controls.Add(this.cbNumberUseLocale);
      this.pnNumber.Controls.Add(this.cbxNumberDecimalSeparator);
      this.pnNumber.Controls.Add(this.lblNumberDecimalDigits);
      this.pnNumber.Controls.Add(this.udNumberDecimalDigits);
      this.pnNumber.Controls.Add(this.lblNumberDecimalSeparator);
      this.pnNumber.Controls.Add(this.lblNumberNegativePattern);
      this.pnNumber.Controls.Add(this.lblNumberGroupSeparator);
      this.pnNumber.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnNumber.Location = new System.Drawing.Point(128, 1);
      this.pnNumber.Name = "pnNumber";
      this.pnNumber.Size = new System.Drawing.Size(247, 290);
      this.pnNumber.TabIndex = 1;
      this.pnNumber.Text = "Number";
      // 
      // cbxNumberNegativePattern
      // 
      this.cbxNumberNegativePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxNumberNegativePattern.FormattingEnabled = true;
      this.cbxNumberNegativePattern.Location = new System.Drawing.Point(176, 112);
      this.cbxNumberNegativePattern.Name = "cbxNumberNegativePattern";
      this.cbxNumberNegativePattern.Size = new System.Drawing.Size(56, 21);
      this.cbxNumberNegativePattern.TabIndex = 10;
      this.cbxNumberNegativePattern.SelectedValueChanged += new System.EventHandler(this.cbxNumberNegativePattern_SelectedValueChanged);
      // 
      // cbxNumberGroupSeparator
      // 
      this.cbxNumberGroupSeparator.FormattingEnabled = true;
      this.cbxNumberGroupSeparator.Location = new System.Drawing.Point(176, 88);
      this.cbxNumberGroupSeparator.Name = "cbxNumberGroupSeparator";
      this.cbxNumberGroupSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxNumberGroupSeparator.TabIndex = 9;
      this.cbxNumberGroupSeparator.TextChanged += new System.EventHandler(this.cbxNumberGroupSeparator_TextChanged);
      // 
      // cbNumberUseLocale
      // 
      this.cbNumberUseLocale.AutoSize = true;
      this.cbNumberUseLocale.Location = new System.Drawing.Point(16, 16);
      this.cbNumberUseLocale.Name = "cbNumberUseLocale";
      this.cbNumberUseLocale.Size = new System.Drawing.Size(115, 17);
      this.cbNumberUseLocale.TabIndex = 0;
      this.cbNumberUseLocale.Text = "Use locale settings";
      this.cbNumberUseLocale.UseVisualStyleBackColor = true;
      this.cbNumberUseLocale.CheckedChanged += new System.EventHandler(this.cbNumberUseLocale_CheckedChanged);
      // 
      // cbxNumberDecimalSeparator
      // 
      this.cbxNumberDecimalSeparator.FormattingEnabled = true;
      this.cbxNumberDecimalSeparator.Location = new System.Drawing.Point(176, 64);
      this.cbxNumberDecimalSeparator.Name = "cbxNumberDecimalSeparator";
      this.cbxNumberDecimalSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxNumberDecimalSeparator.TabIndex = 7;
      this.cbxNumberDecimalSeparator.TextChanged += new System.EventHandler(this.cbxNumberDecimalSeparator_TextChanged);
      // 
      // lblNumberDecimalDigits
      // 
      this.lblNumberDecimalDigits.AutoSize = true;
      this.lblNumberDecimalDigits.Location = new System.Drawing.Point(16, 44);
      this.lblNumberDecimalDigits.Name = "lblNumberDecimalDigits";
      this.lblNumberDecimalDigits.Size = new System.Drawing.Size(75, 13);
      this.lblNumberDecimalDigits.TabIndex = 1;
      this.lblNumberDecimalDigits.Text = "Decimal digits:";
      // 
      // udNumberDecimalDigits
      // 
      this.udNumberDecimalDigits.Location = new System.Drawing.Point(176, 40);
      this.udNumberDecimalDigits.Name = "udNumberDecimalDigits";
      this.udNumberDecimalDigits.Size = new System.Drawing.Size(56, 20);
      this.udNumberDecimalDigits.TabIndex = 6;
      this.udNumberDecimalDigits.ValueChanged += new System.EventHandler(this.udNumberDecimalDigits_ValueChanged);
      // 
      // lblNumberDecimalSeparator
      // 
      this.lblNumberDecimalSeparator.AutoSize = true;
      this.lblNumberDecimalSeparator.Location = new System.Drawing.Point(16, 68);
      this.lblNumberDecimalSeparator.Name = "lblNumberDecimalSeparator";
      this.lblNumberDecimalSeparator.Size = new System.Drawing.Size(97, 13);
      this.lblNumberDecimalSeparator.TabIndex = 2;
      this.lblNumberDecimalSeparator.Text = "Decimal separator:";
      // 
      // lblNumberNegativePattern
      // 
      this.lblNumberNegativePattern.AutoSize = true;
      this.lblNumberNegativePattern.Location = new System.Drawing.Point(16, 116);
      this.lblNumberNegativePattern.Name = "lblNumberNegativePattern";
      this.lblNumberNegativePattern.Size = new System.Drawing.Size(93, 13);
      this.lblNumberNegativePattern.TabIndex = 5;
      this.lblNumberNegativePattern.Text = "Negative pattern:";
      // 
      // lblNumberGroupSeparator
      // 
      this.lblNumberGroupSeparator.AutoSize = true;
      this.lblNumberGroupSeparator.Location = new System.Drawing.Point(16, 92);
      this.lblNumberGroupSeparator.Name = "lblNumberGroupSeparator";
      this.lblNumberGroupSeparator.Size = new System.Drawing.Size(90, 13);
      this.lblNumberGroupSeparator.TabIndex = 4;
      this.lblNumberGroupSeparator.Text = "Group separator:";
      // 
      // pnCurrency
      // 
      this.pnCurrency.BackColor = System.Drawing.SystemColors.Window;
      this.pnCurrency.Controls.Add(this.cbxCurrencySymbol);
      this.pnCurrency.Controls.Add(this.cbxCurrencyNegativePattern);
      this.pnCurrency.Controls.Add(this.cbCurrencyUseLocale);
      this.pnCurrency.Controls.Add(this.cbxCurrencyPositivePattern);
      this.pnCurrency.Controls.Add(this.lblCurrencyDecimalDigits);
      this.pnCurrency.Controls.Add(this.cbxCurrencyGroupSeparator);
      this.pnCurrency.Controls.Add(this.lblCurrencyDecimalSeparator);
      this.pnCurrency.Controls.Add(this.cbxCurrencyDecimalSeparator);
      this.pnCurrency.Controls.Add(this.lblCurrencyGroupSeparator);
      this.pnCurrency.Controls.Add(this.udCurrencyDecimalDigits);
      this.pnCurrency.Controls.Add(this.lblCurrencyPositivePattern);
      this.pnCurrency.Controls.Add(this.lblCurrencySymbol);
      this.pnCurrency.Controls.Add(this.lblCurrencyNegativePattern);
      this.pnCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnCurrency.Location = new System.Drawing.Point(128, 1);
      this.pnCurrency.Name = "pnCurrency";
      this.pnCurrency.Size = new System.Drawing.Size(247, 290);
      this.pnCurrency.TabIndex = 2;
      this.pnCurrency.Text = "Currency";
      // 
      // cbxCurrencySymbol
      // 
      this.cbxCurrencySymbol.FormattingEnabled = true;
      this.cbxCurrencySymbol.Location = new System.Drawing.Point(176, 160);
      this.cbxCurrencySymbol.Name = "cbxCurrencySymbol";
      this.cbxCurrencySymbol.Size = new System.Drawing.Size(56, 21);
      this.cbxCurrencySymbol.TabIndex = 10;
      this.cbxCurrencySymbol.TextChanged += new System.EventHandler(this.cbxCurrencySymbol_TextChanged);
      // 
      // cbxCurrencyNegativePattern
      // 
      this.cbxCurrencyNegativePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxCurrencyNegativePattern.FormattingEnabled = true;
      this.cbxCurrencyNegativePattern.Location = new System.Drawing.Point(176, 136);
      this.cbxCurrencyNegativePattern.Name = "cbxCurrencyNegativePattern";
      this.cbxCurrencyNegativePattern.Size = new System.Drawing.Size(56, 21);
      this.cbxCurrencyNegativePattern.TabIndex = 10;
      this.cbxCurrencyNegativePattern.SelectedValueChanged += new System.EventHandler(this.cbxCurrencyNegativePattern_SelectedValueChanged);
      // 
      // cbCurrencyUseLocale
      // 
      this.cbCurrencyUseLocale.AutoSize = true;
      this.cbCurrencyUseLocale.Location = new System.Drawing.Point(16, 16);
      this.cbCurrencyUseLocale.Name = "cbCurrencyUseLocale";
      this.cbCurrencyUseLocale.Size = new System.Drawing.Size(115, 17);
      this.cbCurrencyUseLocale.TabIndex = 0;
      this.cbCurrencyUseLocale.Text = "Use locale settings";
      this.cbCurrencyUseLocale.UseVisualStyleBackColor = true;
      this.cbCurrencyUseLocale.CheckedChanged += new System.EventHandler(this.cbxCurrencyUseLocale_CheckedChanged);
      // 
      // cbxCurrencyPositivePattern
      // 
      this.cbxCurrencyPositivePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxCurrencyPositivePattern.FormattingEnabled = true;
      this.cbxCurrencyPositivePattern.Location = new System.Drawing.Point(176, 112);
      this.cbxCurrencyPositivePattern.Name = "cbxCurrencyPositivePattern";
      this.cbxCurrencyPositivePattern.Size = new System.Drawing.Size(56, 21);
      this.cbxCurrencyPositivePattern.TabIndex = 10;
      this.cbxCurrencyPositivePattern.SelectedValueChanged += new System.EventHandler(this.cbxCurrencyPositivePattern_SelectedValueChanged);
      // 
      // lblCurrencyDecimalDigits
      // 
      this.lblCurrencyDecimalDigits.AutoSize = true;
      this.lblCurrencyDecimalDigits.Location = new System.Drawing.Point(16, 44);
      this.lblCurrencyDecimalDigits.Name = "lblCurrencyDecimalDigits";
      this.lblCurrencyDecimalDigits.Size = new System.Drawing.Size(75, 13);
      this.lblCurrencyDecimalDigits.TabIndex = 1;
      this.lblCurrencyDecimalDigits.Text = "Decimal digits:";
      // 
      // cbxCurrencyGroupSeparator
      // 
      this.cbxCurrencyGroupSeparator.FormattingEnabled = true;
      this.cbxCurrencyGroupSeparator.Location = new System.Drawing.Point(176, 88);
      this.cbxCurrencyGroupSeparator.Name = "cbxCurrencyGroupSeparator";
      this.cbxCurrencyGroupSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxCurrencyGroupSeparator.TabIndex = 9;
      this.cbxCurrencyGroupSeparator.TextChanged += new System.EventHandler(this.cbxCurrencyGroupSeparator_TextChanged);
      // 
      // lblCurrencyDecimalSeparator
      // 
      this.lblCurrencyDecimalSeparator.AutoSize = true;
      this.lblCurrencyDecimalSeparator.Location = new System.Drawing.Point(16, 68);
      this.lblCurrencyDecimalSeparator.Name = "lblCurrencyDecimalSeparator";
      this.lblCurrencyDecimalSeparator.Size = new System.Drawing.Size(97, 13);
      this.lblCurrencyDecimalSeparator.TabIndex = 2;
      this.lblCurrencyDecimalSeparator.Text = "Decimal separator:";
      // 
      // cbxCurrencyDecimalSeparator
      // 
      this.cbxCurrencyDecimalSeparator.FormattingEnabled = true;
      this.cbxCurrencyDecimalSeparator.Location = new System.Drawing.Point(176, 64);
      this.cbxCurrencyDecimalSeparator.Name = "cbxCurrencyDecimalSeparator";
      this.cbxCurrencyDecimalSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxCurrencyDecimalSeparator.TabIndex = 7;
      this.cbxCurrencyDecimalSeparator.TextChanged += new System.EventHandler(this.cbxCurrencyDecimalSeparator_TextChanged);
      // 
      // lblCurrencyGroupSeparator
      // 
      this.lblCurrencyGroupSeparator.AutoSize = true;
      this.lblCurrencyGroupSeparator.Location = new System.Drawing.Point(16, 92);
      this.lblCurrencyGroupSeparator.Name = "lblCurrencyGroupSeparator";
      this.lblCurrencyGroupSeparator.Size = new System.Drawing.Size(90, 13);
      this.lblCurrencyGroupSeparator.TabIndex = 4;
      this.lblCurrencyGroupSeparator.Text = "Group separator:";
      // 
      // udCurrencyDecimalDigits
      // 
      this.udCurrencyDecimalDigits.Location = new System.Drawing.Point(176, 40);
      this.udCurrencyDecimalDigits.Name = "udCurrencyDecimalDigits";
      this.udCurrencyDecimalDigits.Size = new System.Drawing.Size(56, 20);
      this.udCurrencyDecimalDigits.TabIndex = 6;
      this.udCurrencyDecimalDigits.ValueChanged += new System.EventHandler(this.udCurrencyDecimalDigits_ValueChanged);
      // 
      // lblCurrencyPositivePattern
      // 
      this.lblCurrencyPositivePattern.AutoSize = true;
      this.lblCurrencyPositivePattern.Location = new System.Drawing.Point(16, 116);
      this.lblCurrencyPositivePattern.Name = "lblCurrencyPositivePattern";
      this.lblCurrencyPositivePattern.Size = new System.Drawing.Size(87, 13);
      this.lblCurrencyPositivePattern.TabIndex = 5;
      this.lblCurrencyPositivePattern.Text = "Positive pattern:";
      // 
      // lblCurrencySymbol
      // 
      this.lblCurrencySymbol.AutoSize = true;
      this.lblCurrencySymbol.Location = new System.Drawing.Point(16, 164);
      this.lblCurrencySymbol.Name = "lblCurrencySymbol";
      this.lblCurrencySymbol.Size = new System.Drawing.Size(91, 13);
      this.lblCurrencySymbol.TabIndex = 5;
      this.lblCurrencySymbol.Text = "Currency symbol:";
      // 
      // lblCurrencyNegativePattern
      // 
      this.lblCurrencyNegativePattern.AutoSize = true;
      this.lblCurrencyNegativePattern.Location = new System.Drawing.Point(16, 140);
      this.lblCurrencyNegativePattern.Name = "lblCurrencyNegativePattern";
      this.lblCurrencyNegativePattern.Size = new System.Drawing.Size(93, 13);
      this.lblCurrencyNegativePattern.TabIndex = 5;
      this.lblCurrencyNegativePattern.Text = "Negative pattern:";
      // 
      // pnDate
      // 
      this.pnDate.BackColor = System.Drawing.SystemColors.Window;
      this.pnDate.Controls.Add(this.lbxDates);
      this.pnDate.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnDate.Location = new System.Drawing.Point(128, 1);
      this.pnDate.Name = "pnDate";
      this.pnDate.Size = new System.Drawing.Size(247, 290);
      this.pnDate.TabIndex = 3;
      this.pnDate.Text = "Date";
      // 
      // lbxDates
      // 
      this.lbxDates.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.lbxDates.FormattingEnabled = true;
      this.lbxDates.IntegralHeight = false;
      this.lbxDates.Location = new System.Drawing.Point(16, 16);
      this.lbxDates.Name = "lbxDates";
      this.lbxDates.Size = new System.Drawing.Size(216, 192);
      this.lbxDates.TabIndex = 0;
      this.lbxDates.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxDates_DrawItem);
      this.lbxDates.SelectedIndexChanged += new System.EventHandler(this.lbxDates_SelectedIndexChanged);
      // 
      // pnTime
      // 
      this.pnTime.BackColor = System.Drawing.SystemColors.Window;
      this.pnTime.Controls.Add(this.lbxTimes);
      this.pnTime.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnTime.Location = new System.Drawing.Point(128, 1);
      this.pnTime.Name = "pnTime";
      this.pnTime.Size = new System.Drawing.Size(247, 290);
      this.pnTime.TabIndex = 4;
      this.pnTime.Text = "Time";
      // 
      // lbxTimes
      // 
      this.lbxTimes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.lbxTimes.FormattingEnabled = true;
      this.lbxTimes.IntegralHeight = false;
      this.lbxTimes.Location = new System.Drawing.Point(16, 16);
      this.lbxTimes.Name = "lbxTimes";
      this.lbxTimes.Size = new System.Drawing.Size(216, 192);
      this.lbxTimes.TabIndex = 0;
      this.lbxTimes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxDates_DrawItem);
      this.lbxTimes.SelectedIndexChanged += new System.EventHandler(this.lbxTimes_SelectedIndexChanged);
      // 
      // pnPercent
      // 
      this.pnPercent.BackColor = System.Drawing.SystemColors.Window;
      this.pnPercent.Controls.Add(this.cbxPercentPositivePattern);
      this.pnPercent.Controls.Add(this.lblPercentPositivePattern);
      this.pnPercent.Controls.Add(this.cbPercentUseLocale);
      this.pnPercent.Controls.Add(this.cbxPercentSymbol);
      this.pnPercent.Controls.Add(this.lblPercentDecimalDigits);
      this.pnPercent.Controls.Add(this.cbxPercentNegativePattern);
      this.pnPercent.Controls.Add(this.lblPercentDecimalSeparator);
      this.pnPercent.Controls.Add(this.cbxPercentGroupSeparator);
      this.pnPercent.Controls.Add(this.lblPercentGroupSeparator);
      this.pnPercent.Controls.Add(this.cbxPercentDecimalSeparator);
      this.pnPercent.Controls.Add(this.lblPercentNegativePattern);
      this.pnPercent.Controls.Add(this.udPercentDecimalDigits);
      this.pnPercent.Controls.Add(this.lblPercentSymbol);
      this.pnPercent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnPercent.Location = new System.Drawing.Point(128, 1);
      this.pnPercent.Name = "pnPercent";
      this.pnPercent.Size = new System.Drawing.Size(247, 290);
      this.pnPercent.TabIndex = 5;
      this.pnPercent.Text = "Percent";
      // 
      // cbxPercentPositivePattern
      // 
      this.cbxPercentPositivePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPercentPositivePattern.FormattingEnabled = true;
      this.cbxPercentPositivePattern.Location = new System.Drawing.Point(176, 112);
      this.cbxPercentPositivePattern.Name = "cbxPercentPositivePattern";
      this.cbxPercentPositivePattern.Size = new System.Drawing.Size(56, 21);
      this.cbxPercentPositivePattern.TabIndex = 12;
      this.cbxPercentPositivePattern.SelectedValueChanged += new System.EventHandler(this.cbxPercentPositivePattern_SelectedValueChanged);
      // 
      // lblPercentPositivePattern
      // 
      this.lblPercentPositivePattern.AutoSize = true;
      this.lblPercentPositivePattern.Location = new System.Drawing.Point(16, 116);
      this.lblPercentPositivePattern.Name = "lblPercentPositivePattern";
      this.lblPercentPositivePattern.Size = new System.Drawing.Size(87, 13);
      this.lblPercentPositivePattern.TabIndex = 11;
      this.lblPercentPositivePattern.Text = "Positive pattern:";
      // 
      // cbPercentUseLocale
      // 
      this.cbPercentUseLocale.AutoSize = true;
      this.cbPercentUseLocale.Location = new System.Drawing.Point(16, 16);
      this.cbPercentUseLocale.Name = "cbPercentUseLocale";
      this.cbPercentUseLocale.Size = new System.Drawing.Size(115, 17);
      this.cbPercentUseLocale.TabIndex = 0;
      this.cbPercentUseLocale.Text = "Use locale settings";
      this.cbPercentUseLocale.UseVisualStyleBackColor = true;
      this.cbPercentUseLocale.CheckedChanged += new System.EventHandler(this.cbPercentUseLocale_CheckedChanged);
      // 
      // cbxPercentSymbol
      // 
      this.cbxPercentSymbol.FormattingEnabled = true;
      this.cbxPercentSymbol.Location = new System.Drawing.Point(176, 160);
      this.cbxPercentSymbol.Name = "cbxPercentSymbol";
      this.cbxPercentSymbol.Size = new System.Drawing.Size(56, 21);
      this.cbxPercentSymbol.TabIndex = 10;
      this.cbxPercentSymbol.TextChanged += new System.EventHandler(this.cbxPercentSymbol_TextChanged);
      // 
      // lblPercentDecimalDigits
      // 
      this.lblPercentDecimalDigits.AutoSize = true;
      this.lblPercentDecimalDigits.Location = new System.Drawing.Point(16, 44);
      this.lblPercentDecimalDigits.Name = "lblPercentDecimalDigits";
      this.lblPercentDecimalDigits.Size = new System.Drawing.Size(75, 13);
      this.lblPercentDecimalDigits.TabIndex = 1;
      this.lblPercentDecimalDigits.Text = "Decimal digits:";
      // 
      // cbxPercentNegativePattern
      // 
      this.cbxPercentNegativePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxPercentNegativePattern.FormattingEnabled = true;
      this.cbxPercentNegativePattern.Location = new System.Drawing.Point(176, 136);
      this.cbxPercentNegativePattern.Name = "cbxPercentNegativePattern";
      this.cbxPercentNegativePattern.Size = new System.Drawing.Size(56, 21);
      this.cbxPercentNegativePattern.TabIndex = 10;
      this.cbxPercentNegativePattern.SelectedValueChanged += new System.EventHandler(this.cbxPercentNegativePattern_SelectedValueChanged);
      // 
      // lblPercentDecimalSeparator
      // 
      this.lblPercentDecimalSeparator.AutoSize = true;
      this.lblPercentDecimalSeparator.Location = new System.Drawing.Point(16, 68);
      this.lblPercentDecimalSeparator.Name = "lblPercentDecimalSeparator";
      this.lblPercentDecimalSeparator.Size = new System.Drawing.Size(97, 13);
      this.lblPercentDecimalSeparator.TabIndex = 2;
      this.lblPercentDecimalSeparator.Text = "Decimal separator:";
      // 
      // cbxPercentGroupSeparator
      // 
      this.cbxPercentGroupSeparator.FormattingEnabled = true;
      this.cbxPercentGroupSeparator.Location = new System.Drawing.Point(176, 88);
      this.cbxPercentGroupSeparator.Name = "cbxPercentGroupSeparator";
      this.cbxPercentGroupSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxPercentGroupSeparator.TabIndex = 9;
      this.cbxPercentGroupSeparator.TextChanged += new System.EventHandler(this.cbxPercentGroupSeparator_TextChanged);
      // 
      // lblPercentGroupSeparator
      // 
      this.lblPercentGroupSeparator.AutoSize = true;
      this.lblPercentGroupSeparator.Location = new System.Drawing.Point(16, 92);
      this.lblPercentGroupSeparator.Name = "lblPercentGroupSeparator";
      this.lblPercentGroupSeparator.Size = new System.Drawing.Size(90, 13);
      this.lblPercentGroupSeparator.TabIndex = 4;
      this.lblPercentGroupSeparator.Text = "Group separator:";
      // 
      // cbxPercentDecimalSeparator
      // 
      this.cbxPercentDecimalSeparator.FormattingEnabled = true;
      this.cbxPercentDecimalSeparator.Location = new System.Drawing.Point(176, 64);
      this.cbxPercentDecimalSeparator.Name = "cbxPercentDecimalSeparator";
      this.cbxPercentDecimalSeparator.Size = new System.Drawing.Size(56, 21);
      this.cbxPercentDecimalSeparator.TabIndex = 7;
      this.cbxPercentDecimalSeparator.TextChanged += new System.EventHandler(this.cbxPercentDecimalSeparator_TextChanged);
      // 
      // lblPercentNegativePattern
      // 
      this.lblPercentNegativePattern.AutoSize = true;
      this.lblPercentNegativePattern.Location = new System.Drawing.Point(16, 140);
      this.lblPercentNegativePattern.Name = "lblPercentNegativePattern";
      this.lblPercentNegativePattern.Size = new System.Drawing.Size(93, 13);
      this.lblPercentNegativePattern.TabIndex = 5;
      this.lblPercentNegativePattern.Text = "Negative pattern:";
      // 
      // udPercentDecimalDigits
      // 
      this.udPercentDecimalDigits.Location = new System.Drawing.Point(176, 40);
      this.udPercentDecimalDigits.Name = "udPercentDecimalDigits";
      this.udPercentDecimalDigits.Size = new System.Drawing.Size(56, 20);
      this.udPercentDecimalDigits.TabIndex = 6;
      this.udPercentDecimalDigits.ValueChanged += new System.EventHandler(this.udPercentDecimalDigits_ValueChanged);
      // 
      // lblPercentSymbol
      // 
      this.lblPercentSymbol.AutoSize = true;
      this.lblPercentSymbol.Location = new System.Drawing.Point(16, 164);
      this.lblPercentSymbol.Name = "lblPercentSymbol";
      this.lblPercentSymbol.Size = new System.Drawing.Size(84, 13);
      this.lblPercentSymbol.TabIndex = 5;
      this.lblPercentSymbol.Text = "Percent symbol:";
      // 
      // pnBoolean
      // 
      this.pnBoolean.BackColor = System.Drawing.SystemColors.Window;
      this.pnBoolean.Controls.Add(this.cbxBooleanTrue);
      this.pnBoolean.Controls.Add(this.cbxBooleanFalse);
      this.pnBoolean.Controls.Add(this.lblBooleanFalse);
      this.pnBoolean.Controls.Add(this.lblBooleanTrue);
      this.pnBoolean.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnBoolean.Location = new System.Drawing.Point(128, 1);
      this.pnBoolean.Name = "pnBoolean";
      this.pnBoolean.Size = new System.Drawing.Size(247, 290);
      this.pnBoolean.TabIndex = 6;
      this.pnBoolean.Text = "Boolean";
      // 
      // cbxBooleanTrue
      // 
      this.cbxBooleanTrue.FormattingEnabled = true;
      this.cbxBooleanTrue.Location = new System.Drawing.Point(152, 40);
      this.cbxBooleanTrue.Name = "cbxBooleanTrue";
      this.cbxBooleanTrue.Size = new System.Drawing.Size(80, 21);
      this.cbxBooleanTrue.TabIndex = 3;
      this.cbxBooleanTrue.TextChanged += new System.EventHandler(this.cbxBooleanTrue_TextChanged);
      // 
      // cbxBooleanFalse
      // 
      this.cbxBooleanFalse.FormattingEnabled = true;
      this.cbxBooleanFalse.Location = new System.Drawing.Point(152, 16);
      this.cbxBooleanFalse.Name = "cbxBooleanFalse";
      this.cbxBooleanFalse.Size = new System.Drawing.Size(80, 21);
      this.cbxBooleanFalse.TabIndex = 2;
      this.cbxBooleanFalse.TextChanged += new System.EventHandler(this.cbxBooleanFalse_TextChanged);
      // 
      // lblBooleanFalse
      // 
      this.lblBooleanFalse.AutoSize = true;
      this.lblBooleanFalse.Location = new System.Drawing.Point(16, 20);
      this.lblBooleanFalse.Name = "lblBooleanFalse";
      this.lblBooleanFalse.Size = new System.Drawing.Size(88, 13);
      this.lblBooleanFalse.TabIndex = 0;
      this.lblBooleanFalse.Text = "False value text:";
      // 
      // lblBooleanTrue
      // 
      this.lblBooleanTrue.AutoSize = true;
      this.lblBooleanTrue.Location = new System.Drawing.Point(16, 44);
      this.lblBooleanTrue.Name = "lblBooleanTrue";
      this.lblBooleanTrue.Size = new System.Drawing.Size(85, 13);
      this.lblBooleanTrue.TabIndex = 1;
      this.lblBooleanTrue.Text = "True value text:";
      // 
      // pnCustom
      // 
      this.pnCustom.BackColor = System.Drawing.SystemColors.Window;
      this.pnCustom.Controls.Add(this.lbxCustom);
      this.pnCustom.Controls.Add(this.tbCustom);
      this.pnCustom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnCustom.Location = new System.Drawing.Point(128, 1);
      this.pnCustom.Name = "pnCustom";
      this.pnCustom.Size = new System.Drawing.Size(247, 290);
      this.pnCustom.TabIndex = 7;
      this.pnCustom.Text = "Custom";
      // 
      // lbxCustom
      // 
      this.lbxCustom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.lbxCustom.FormattingEnabled = true;
      this.lbxCustom.IntegralHeight = false;
      this.lbxCustom.Location = new System.Drawing.Point(16, 44);
      this.lbxCustom.Name = "lbxCustom";
      this.lbxCustom.Size = new System.Drawing.Size(216, 164);
      this.lbxCustom.TabIndex = 1;
      this.lbxCustom.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxCustom_DrawItem);
      this.lbxCustom.SelectedIndexChanged += new System.EventHandler(this.lbxCustom_SelectedIndexChanged);
      // 
      // tbCustom
      // 
      this.tbCustom.Location = new System.Drawing.Point(16, 16);
      this.tbCustom.Name = "tbCustom";
      this.tbCustom.Size = new System.Drawing.Size(216, 20);
      this.tbCustom.TabIndex = 0;
      this.tbCustom.TextChanged += new System.EventHandler(this.tbCustom_TextChanged);
      // 
      // gbSample
      // 
      this.gbSample.BackColor = System.Drawing.SystemColors.Window;
      this.gbSample.Controls.Add(this.lblSample);
      this.gbSample.Location = new System.Drawing.Point(156, 270);
      this.gbSample.Name = "gbSample";
      this.gbSample.Size = new System.Drawing.Size(216, 48);
      this.gbSample.TabIndex = 3;
      this.gbSample.TabStop = false;
      this.gbSample.Text = "Sample";
      // 
      // lblSample
      // 
      this.lblSample.AutoSize = true;
      this.lblSample.Location = new System.Drawing.Point(12, 20);
      this.lblSample.Name = "lblSample";
      this.lblSample.Size = new System.Drawing.Size(41, 13);
      this.lblSample.TabIndex = 0;
      this.lblSample.Text = "Sample";
      // 
      // lblExpression
      // 
      this.lblExpression.AutoSize = true;
      this.lblExpression.Location = new System.Drawing.Point(12, 16);
      this.lblExpression.Name = "lblExpression";
      this.lblExpression.Size = new System.Drawing.Size(63, 13);
      this.lblExpression.TabIndex = 4;
      this.lblExpression.Text = "Expression:";
      // 
      // cbxExpression
      // 
      this.cbxExpression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxExpression.FormattingEnabled = true;
      this.cbxExpression.Location = new System.Drawing.Point(140, 12);
      this.cbxExpression.Name = "cbxExpression";
      this.cbxExpression.Size = new System.Drawing.Size(248, 21);
      this.cbxExpression.TabIndex = 5;
      this.cbxExpression.SelectedIndexChanged += new System.EventHandler(this.cbxExpression_SelectedIndexChanged);
      // 
      // FormatEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(398, 383);
      this.Controls.Add(this.cbxExpression);
      this.Controls.Add(this.lblExpression);
      this.Controls.Add(this.gbSample);
      this.Controls.Add(this.pc1);
      this.Name = "FormatEditorForm";
      this.Text = "Select Format";
      this.Shown += new System.EventHandler(this.FormatEditorForm_Shown);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.pc1, 0);
      this.Controls.SetChildIndex(this.gbSample, 0);
      this.Controls.SetChildIndex(this.lblExpression, 0);
      this.Controls.SetChildIndex(this.cbxExpression, 0);
      this.pc1.ResumeLayout(false);
      this.pnNumber.ResumeLayout(false);
      this.pnNumber.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udNumberDecimalDigits)).EndInit();
      this.pnCurrency.ResumeLayout(false);
      this.pnCurrency.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCurrencyDecimalDigits)).EndInit();
      this.pnDate.ResumeLayout(false);
      this.pnTime.ResumeLayout(false);
      this.pnPercent.ResumeLayout(false);
      this.pnPercent.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPercentDecimalDigits)).EndInit();
      this.pnBoolean.ResumeLayout(false);
      this.pnBoolean.PerformLayout();
      this.pnCustom.ResumeLayout(false);
      this.pnCustom.PerformLayout();
      this.gbSample.ResumeLayout(false);
      this.gbSample.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private FastReport.Controls.PageControl pc1;
    private FastReport.Controls.PageControlPage pnGeneral;
    private FastReport.Controls.PageControlPage pnNumber;
    private FastReport.Controls.PageControlPage pnCurrency;
    private FastReport.Controls.PageControlPage pnDate;
    private FastReport.Controls.PageControlPage pnTime;
    private FastReport.Controls.PageControlPage pnPercent;
    private FastReport.Controls.PageControlPage pnBoolean;
    private FastReport.Controls.PageControlPage pnCustom;
    private System.Windows.Forms.GroupBox gbSample;
    private System.Windows.Forms.Label lblSample;
    private System.Windows.Forms.Label lblNumberDecimalSeparator;
    private System.Windows.Forms.Label lblNumberDecimalDigits;
    private System.Windows.Forms.CheckBox cbNumberUseLocale;
    private System.Windows.Forms.Label lblNumberNegativePattern;
    private System.Windows.Forms.Label lblNumberGroupSeparator;
    private System.Windows.Forms.ComboBox cbxNumberNegativePattern;
    private System.Windows.Forms.ComboBox cbxNumberGroupSeparator;
    private System.Windows.Forms.ComboBox cbxNumberDecimalSeparator;
    private System.Windows.Forms.NumericUpDown udNumberDecimalDigits;
    private System.Windows.Forms.ComboBox cbxCurrencyNegativePattern;
    private System.Windows.Forms.ComboBox cbxCurrencyPositivePattern;
    private System.Windows.Forms.ComboBox cbxCurrencyGroupSeparator;
    private System.Windows.Forms.ComboBox cbxCurrencyDecimalSeparator;
    private System.Windows.Forms.NumericUpDown udCurrencyDecimalDigits;
    private System.Windows.Forms.Label lblCurrencyNegativePattern;
    private System.Windows.Forms.Label lblCurrencyPositivePattern;
    private System.Windows.Forms.Label lblCurrencyGroupSeparator;
    private System.Windows.Forms.Label lblCurrencyDecimalSeparator;
    private System.Windows.Forms.Label lblCurrencyDecimalDigits;
    private System.Windows.Forms.CheckBox cbCurrencyUseLocale;
    private System.Windows.Forms.ListBox lbxDates;
    private System.Windows.Forms.ListBox lbxTimes;
    private System.Windows.Forms.ComboBox cbxCurrencySymbol;
    private System.Windows.Forms.Label lblCurrencySymbol;
    private System.Windows.Forms.ComboBox cbxPercentNegativePattern;
    private System.Windows.Forms.ComboBox cbxPercentGroupSeparator;
    private System.Windows.Forms.ComboBox cbxPercentDecimalSeparator;
    private System.Windows.Forms.NumericUpDown udPercentDecimalDigits;
    private System.Windows.Forms.Label lblPercentNegativePattern;
    private System.Windows.Forms.Label lblPercentGroupSeparator;
    private System.Windows.Forms.Label lblPercentDecimalSeparator;
    private System.Windows.Forms.Label lblPercentDecimalDigits;
    private System.Windows.Forms.CheckBox cbPercentUseLocale;
    private System.Windows.Forms.ComboBox cbxPercentSymbol;
    private System.Windows.Forms.Label lblPercentSymbol;
    private System.Windows.Forms.ComboBox cbxBooleanTrue;
    private System.Windows.Forms.ComboBox cbxBooleanFalse;
    private System.Windows.Forms.Label lblBooleanTrue;
    private System.Windows.Forms.Label lblBooleanFalse;
    private System.Windows.Forms.ListBox lbxCustom;
    private System.Windows.Forms.TextBox tbCustom;
    private System.Windows.Forms.ComboBox cbxPercentPositivePattern;
    private System.Windows.Forms.Label lblPercentPositivePattern;
    private System.Windows.Forms.Label lblExpression;
    private System.Windows.Forms.ComboBox cbxExpression;
  }
}
