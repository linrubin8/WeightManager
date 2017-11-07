namespace FastReport.Forms
{
  partial class HyperlinkEditorForm
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
      this.pageControl1 = new FastReport.Controls.PageControl();
      this.pnUrl = new FastReport.Controls.PageControlPage();
      this.tbUrlExpression = new FastReport.Controls.TextBoxButton();
      this.tbUrlValue = new System.Windows.Forms.TextBox();
      this.lblUrlExpression = new System.Windows.Forms.Label();
      this.lblUrl = new System.Windows.Forms.Label();
      this.pnPageNumber = new FastReport.Controls.PageControlPage();
      this.tbPageNumberValue = new System.Windows.Forms.TextBox();
      this.tbPageNumberExpression = new FastReport.Controls.TextBoxButton();
      this.lblPageNumberExpression = new System.Windows.Forms.Label();
      this.lblPageNumber = new System.Windows.Forms.Label();
      this.pnBookmark = new FastReport.Controls.PageControlPage();
      this.tbBookmarkExpression = new FastReport.Controls.TextBoxButton();
      this.lblBookmarkExpression = new System.Windows.Forms.Label();
      this.tbBookmarkValue = new FastReport.Controls.TextBoxButton();
      this.lblBookmark = new System.Windows.Forms.Label();
      this.pnReport = new FastReport.Controls.PageControlPage();
      this.tbParameterExpression1 = new FastReport.Controls.TextBoxButton();
      this.tbParameterValue1 = new System.Windows.Forms.TextBox();
      this.lblParameterExpression1 = new System.Windows.Forms.Label();
      this.lblParameterValue1 = new System.Windows.Forms.Label();
      this.cbxReportParameter1 = new FastReport.Controls.ParametersComboBox();
      this.lblReportParameter1 = new System.Windows.Forms.Label();
      this.tbReport = new FastReport.Controls.TextBoxButton();
      this.lblReport = new System.Windows.Forms.Label();
      this.pnReportPage = new FastReport.Controls.PageControlPage();
      this.tbParameterExpression2 = new FastReport.Controls.TextBoxButton();
      this.tbParameterValue2 = new System.Windows.Forms.TextBox();
      this.lblParameterExpression2 = new System.Windows.Forms.Label();
      this.lblParameterValue2 = new System.Windows.Forms.Label();
      this.cbxReportParameter2 = new FastReport.Controls.ParametersComboBox();
      this.cbxReportPage = new FastReport.Controls.ComponentRefComboBox();
      this.lblReportParameter2 = new System.Windows.Forms.Label();
      this.lblReportPage = new System.Windows.Forms.Label();
      this.pnCustom = new FastReport.Controls.PageControlPage();
      this.tbCustomExpression = new FastReport.Controls.TextBoxButton();
      this.tbCustomValue = new System.Windows.Forms.TextBox();
      this.lblCustomExpression = new System.Windows.Forms.Label();
      this.lblCustom = new System.Windows.Forms.Label();
      this.lblHint2 = new System.Windows.Forms.Label();
      this.cbModifyAppearance = new System.Windows.Forms.CheckBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblHint1 = new System.Windows.Forms.Label();
      this.pageControl1.SuspendLayout();
      this.pnUrl.SuspendLayout();
      this.pnPageNumber.SuspendLayout();
      this.pnBookmark.SuspendLayout();
      this.pnReport.SuspendLayout();
      this.pnReportPage.SuspendLayout();
      this.pnCustom.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(396, 364);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(476, 364);
      // 
      // pageControl1
      // 
      this.pageControl1.Controls.Add(this.pnUrl);
      this.pageControl1.Controls.Add(this.pnPageNumber);
      this.pageControl1.Controls.Add(this.pnBookmark);
      this.pageControl1.Controls.Add(this.pnReport);
      this.pageControl1.Controls.Add(this.pnReportPage);
      this.pageControl1.Controls.Add(this.pnCustom);
      this.pageControl1.Location = new System.Drawing.Point(12, 12);
      this.pageControl1.Name = "pageControl1";
      this.pageControl1.SelectorWidth = 139;
      this.pageControl1.Size = new System.Drawing.Size(540, 316);
      this.pageControl1.TabIndex = 1;
      this.pageControl1.Text = "pageControl1";
      this.pageControl1.PageSelected += new System.EventHandler(this.pageControl1_PageSelected);
      // 
      // pnUrl
      // 
      this.pnUrl.BackColor = System.Drawing.SystemColors.Window;
      this.pnUrl.Controls.Add(this.tbUrlExpression);
      this.pnUrl.Controls.Add(this.tbUrlValue);
      this.pnUrl.Controls.Add(this.lblUrlExpression);
      this.pnUrl.Controls.Add(this.lblUrl);
      this.pnUrl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnUrl.Location = new System.Drawing.Point(139, 1);
      this.pnUrl.Name = "pnUrl";
      this.pnUrl.Size = new System.Drawing.Size(400, 314);
      this.pnUrl.TabIndex = 0;
      this.pnUrl.Text = "URL";
      // 
      // tbUrlExpression
      // 
      this.tbUrlExpression.Image = null;
      this.tbUrlExpression.Location = new System.Drawing.Point(16, 88);
      this.tbUrlExpression.Name = "tbUrlExpression";
      this.tbUrlExpression.Size = new System.Drawing.Size(368, 21);
      this.tbUrlExpression.TabIndex = 2;
      this.tbUrlExpression.ButtonClick += new System.EventHandler(this.tbUrlExpression_ButtonClick);
      // 
      // tbUrlValue
      // 
      this.tbUrlValue.Location = new System.Drawing.Point(16, 36);
      this.tbUrlValue.Name = "tbUrlValue";
      this.tbUrlValue.Size = new System.Drawing.Size(368, 20);
      this.tbUrlValue.TabIndex = 1;
      // 
      // lblUrlExpression
      // 
      this.lblUrlExpression.AutoSize = true;
      this.lblUrlExpression.Location = new System.Drawing.Point(16, 68);
      this.lblUrlExpression.Name = "lblUrlExpression";
      this.lblUrlExpression.Size = new System.Drawing.Size(222, 13);
      this.lblUrlExpression.TabIndex = 0;
      this.lblUrlExpression.Text = "or enter the expression that returns an URL:";
      // 
      // lblUrl
      // 
      this.lblUrl.AutoSize = true;
      this.lblUrl.Location = new System.Drawing.Point(16, 16);
      this.lblUrl.Name = "lblUrl";
      this.lblUrl.Size = new System.Drawing.Size(236, 13);
      this.lblUrl.TabIndex = 0;
      this.lblUrl.Text = "Specify an URL (example: http://www.url.com):";
      // 
      // pnPageNumber
      // 
      this.pnPageNumber.BackColor = System.Drawing.SystemColors.Window;
      this.pnPageNumber.Controls.Add(this.tbPageNumberValue);
      this.pnPageNumber.Controls.Add(this.tbPageNumberExpression);
      this.pnPageNumber.Controls.Add(this.lblPageNumberExpression);
      this.pnPageNumber.Controls.Add(this.lblPageNumber);
      this.pnPageNumber.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnPageNumber.Location = new System.Drawing.Point(139, 1);
      this.pnPageNumber.Name = "pnPageNumber";
      this.pnPageNumber.Size = new System.Drawing.Size(400, 314);
      this.pnPageNumber.TabIndex = 1;
      this.pnPageNumber.Text = "Page number";
      // 
      // tbPageNumberValue
      // 
      this.tbPageNumberValue.Location = new System.Drawing.Point(16, 36);
      this.tbPageNumberValue.Name = "tbPageNumberValue";
      this.tbPageNumberValue.Size = new System.Drawing.Size(112, 20);
      this.tbPageNumberValue.TabIndex = 5;
      // 
      // tbPageNumberExpression
      // 
      this.tbPageNumberExpression.Image = null;
      this.tbPageNumberExpression.Location = new System.Drawing.Point(16, 88);
      this.tbPageNumberExpression.Name = "tbPageNumberExpression";
      this.tbPageNumberExpression.Size = new System.Drawing.Size(368, 21);
      this.tbPageNumberExpression.TabIndex = 4;
      this.tbPageNumberExpression.ButtonClick += new System.EventHandler(this.tbPageNumberExpression_ButtonClick);
      // 
      // lblPageNumberExpression
      // 
      this.lblPageNumberExpression.AutoSize = true;
      this.lblPageNumberExpression.Location = new System.Drawing.Point(16, 68);
      this.lblPageNumberExpression.Name = "lblPageNumberExpression";
      this.lblPageNumberExpression.Size = new System.Drawing.Size(260, 13);
      this.lblPageNumberExpression.TabIndex = 3;
      this.lblPageNumberExpression.Text = "or enter the expression that returns a page number:";
      // 
      // lblPageNumber
      // 
      this.lblPageNumber.AutoSize = true;
      this.lblPageNumber.Location = new System.Drawing.Point(16, 16);
      this.lblPageNumber.Name = "lblPageNumber";
      this.lblPageNumber.Size = new System.Drawing.Size(112, 13);
      this.lblPageNumber.TabIndex = 0;
      this.lblPageNumber.Text = "Specify page number:";
      // 
      // pnBookmark
      // 
      this.pnBookmark.BackColor = System.Drawing.SystemColors.Window;
      this.pnBookmark.Controls.Add(this.tbBookmarkExpression);
      this.pnBookmark.Controls.Add(this.lblBookmarkExpression);
      this.pnBookmark.Controls.Add(this.tbBookmarkValue);
      this.pnBookmark.Controls.Add(this.lblBookmark);
      this.pnBookmark.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnBookmark.Location = new System.Drawing.Point(139, 1);
      this.pnBookmark.Name = "pnBookmark";
      this.pnBookmark.Size = new System.Drawing.Size(400, 314);
      this.pnBookmark.TabIndex = 2;
      this.pnBookmark.Text = "Bookmark";
      // 
      // tbBookmarkExpression
      // 
      this.tbBookmarkExpression.Image = null;
      this.tbBookmarkExpression.Location = new System.Drawing.Point(16, 88);
      this.tbBookmarkExpression.Name = "tbBookmarkExpression";
      this.tbBookmarkExpression.Size = new System.Drawing.Size(368, 21);
      this.tbBookmarkExpression.TabIndex = 6;
      this.tbBookmarkExpression.ButtonClick += new System.EventHandler(this.tbBookmarkExpression_ButtonClick);
      // 
      // lblBookmarkExpression
      // 
      this.lblBookmarkExpression.AutoSize = true;
      this.lblBookmarkExpression.Location = new System.Drawing.Point(16, 68);
      this.lblBookmarkExpression.Name = "lblBookmarkExpression";
      this.lblBookmarkExpression.Size = new System.Drawing.Size(272, 13);
      this.lblBookmarkExpression.TabIndex = 5;
      this.lblBookmarkExpression.Text = "or enter the expression that returns a bookmark name:";
      // 
      // tbBookmarkValue
      // 
      this.tbBookmarkValue.Image = null;
      this.tbBookmarkValue.Location = new System.Drawing.Point(16, 36);
      this.tbBookmarkValue.Name = "tbBookmarkValue";
      this.tbBookmarkValue.Size = new System.Drawing.Size(368, 21);
      this.tbBookmarkValue.TabIndex = 1;
      // 
      // lblBookmark
      // 
      this.lblBookmark.AutoSize = true;
      this.lblBookmark.Location = new System.Drawing.Point(16, 16);
      this.lblBookmark.Name = "lblBookmark";
      this.lblBookmark.Size = new System.Drawing.Size(133, 13);
      this.lblBookmark.TabIndex = 0;
      this.lblBookmark.Text = "Specify a bookmark name:";
      // 
      // pnReport
      // 
      this.pnReport.BackColor = System.Drawing.SystemColors.Window;
      this.pnReport.Controls.Add(this.tbParameterExpression1);
      this.pnReport.Controls.Add(this.tbParameterValue1);
      this.pnReport.Controls.Add(this.lblParameterExpression1);
      this.pnReport.Controls.Add(this.lblParameterValue1);
      this.pnReport.Controls.Add(this.cbxReportParameter1);
      this.pnReport.Controls.Add(this.lblReportParameter1);
      this.pnReport.Controls.Add(this.tbReport);
      this.pnReport.Controls.Add(this.lblReport);
      this.pnReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnReport.Location = new System.Drawing.Point(139, 1);
      this.pnReport.Name = "pnReport";
      this.pnReport.Size = new System.Drawing.Size(400, 314);
      this.pnReport.TabIndex = 3;
      this.pnReport.Text = "Report";
      // 
      // tbParameterExpression1
      // 
      this.tbParameterExpression1.Image = null;
      this.tbParameterExpression1.Location = new System.Drawing.Point(16, 192);
      this.tbParameterExpression1.Name = "tbParameterExpression1";
      this.tbParameterExpression1.Size = new System.Drawing.Size(368, 21);
      this.tbParameterExpression1.TabIndex = 7;
      this.tbParameterExpression1.ButtonClick += new System.EventHandler(this.tbParameterExpression1_ButtonClick);
      // 
      // tbParameterValue1
      // 
      this.tbParameterValue1.Location = new System.Drawing.Point(16, 140);
      this.tbParameterValue1.Name = "tbParameterValue1";
      this.tbParameterValue1.Size = new System.Drawing.Size(368, 20);
      this.tbParameterValue1.TabIndex = 6;
      // 
      // lblParameterExpression1
      // 
      this.lblParameterExpression1.AutoSize = true;
      this.lblParameterExpression1.Location = new System.Drawing.Point(16, 172);
      this.lblParameterExpression1.Name = "lblParameterExpression1";
      this.lblParameterExpression1.Size = new System.Drawing.Size(276, 13);
      this.lblParameterExpression1.TabIndex = 4;
      this.lblParameterExpression1.Text = "or enter the expression that returns a parameter value:";
      // 
      // lblParameterValue1
      // 
      this.lblParameterValue1.AutoSize = true;
      this.lblParameterValue1.Location = new System.Drawing.Point(16, 120);
      this.lblParameterValue1.Name = "lblParameterValue1";
      this.lblParameterValue1.Size = new System.Drawing.Size(137, 13);
      this.lblParameterValue1.TabIndex = 5;
      this.lblParameterValue1.Text = "Specify a parameter value:";
      // 
      // cbxReportParameter1
      // 
      this.cbxReportParameter1.Location = new System.Drawing.Point(16, 88);
      this.cbxReportParameter1.Name = "cbxReportParameter1";
      this.cbxReportParameter1.Size = new System.Drawing.Size(368, 21);
      this.cbxReportParameter1.TabIndex = 3;
      this.cbxReportParameter1.DropDownOpening += new System.EventHandler(this.cbxReportParameter1_DropDownOpening);
      // 
      // lblReportParameter1
      // 
      this.lblReportParameter1.AutoSize = true;
      this.lblReportParameter1.Location = new System.Drawing.Point(16, 68);
      this.lblReportParameter1.Name = "lblReportParameter1";
      this.lblReportParameter1.Size = new System.Drawing.Size(97, 13);
      this.lblReportParameter1.TabIndex = 2;
      this.lblReportParameter1.Text = "Report parameter:";
      // 
      // tbReport
      // 
      this.tbReport.Image = null;
      this.tbReport.Location = new System.Drawing.Point(16, 36);
      this.tbReport.Name = "tbReport";
      this.tbReport.Size = new System.Drawing.Size(368, 21);
      this.tbReport.TabIndex = 1;
      this.tbReport.ButtonClick += new System.EventHandler(this.tbReport_ButtonClick);
      // 
      // lblReport
      // 
      this.lblReport.AutoSize = true;
      this.lblReport.Location = new System.Drawing.Point(16, 16);
      this.lblReport.Name = "lblReport";
      this.lblReport.Size = new System.Drawing.Size(44, 13);
      this.lblReport.TabIndex = 0;
      this.lblReport.Text = "Report:";
      // 
      // pnReportPage
      // 
      this.pnReportPage.BackColor = System.Drawing.SystemColors.Window;
      this.pnReportPage.Controls.Add(this.tbParameterExpression2);
      this.pnReportPage.Controls.Add(this.tbParameterValue2);
      this.pnReportPage.Controls.Add(this.lblParameterExpression2);
      this.pnReportPage.Controls.Add(this.lblParameterValue2);
      this.pnReportPage.Controls.Add(this.cbxReportParameter2);
      this.pnReportPage.Controls.Add(this.cbxReportPage);
      this.pnReportPage.Controls.Add(this.lblReportParameter2);
      this.pnReportPage.Controls.Add(this.lblReportPage);
      this.pnReportPage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnReportPage.Location = new System.Drawing.Point(139, 1);
      this.pnReportPage.Name = "pnReportPage";
      this.pnReportPage.Size = new System.Drawing.Size(400, 314);
      this.pnReportPage.TabIndex = 4;
      this.pnReportPage.Text = "Page";
      // 
      // tbParameterExpression2
      // 
      this.tbParameterExpression2.Image = null;
      this.tbParameterExpression2.Location = new System.Drawing.Point(16, 192);
      this.tbParameterExpression2.Name = "tbParameterExpression2";
      this.tbParameterExpression2.Size = new System.Drawing.Size(368, 21);
      this.tbParameterExpression2.TabIndex = 11;
      this.tbParameterExpression2.ButtonClick += new System.EventHandler(this.tbParameterExpression2_ButtonClick);
      // 
      // tbParameterValue2
      // 
      this.tbParameterValue2.Location = new System.Drawing.Point(16, 140);
      this.tbParameterValue2.Name = "tbParameterValue2";
      this.tbParameterValue2.Size = new System.Drawing.Size(368, 20);
      this.tbParameterValue2.TabIndex = 10;
      // 
      // lblParameterExpression2
      // 
      this.lblParameterExpression2.AutoSize = true;
      this.lblParameterExpression2.Location = new System.Drawing.Point(16, 172);
      this.lblParameterExpression2.Name = "lblParameterExpression2";
      this.lblParameterExpression2.Size = new System.Drawing.Size(276, 13);
      this.lblParameterExpression2.TabIndex = 8;
      this.lblParameterExpression2.Text = "or enter the expression that returns a parameter value:";
      // 
      // lblParameterValue2
      // 
      this.lblParameterValue2.AutoSize = true;
      this.lblParameterValue2.Location = new System.Drawing.Point(16, 120);
      this.lblParameterValue2.Name = "lblParameterValue2";
      this.lblParameterValue2.Size = new System.Drawing.Size(137, 13);
      this.lblParameterValue2.TabIndex = 9;
      this.lblParameterValue2.Text = "Specify a parameter value:";
      // 
      // cbxReportParameter2
      // 
      this.cbxReportParameter2.Location = new System.Drawing.Point(16, 88);
      this.cbxReportParameter2.Name = "cbxReportParameter2";
      this.cbxReportParameter2.Size = new System.Drawing.Size(368, 21);
      this.cbxReportParameter2.TabIndex = 4;
      // 
      // cbxReportPage
      // 
      this.cbxReportPage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxReportPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxReportPage.FormattingEnabled = true;
      this.cbxReportPage.ItemHeight = 19;
      this.cbxReportPage.Location = new System.Drawing.Point(16, 36);
      this.cbxReportPage.Name = "cbxReportPage";
      this.cbxReportPage.Size = new System.Drawing.Size(368, 25);
      this.cbxReportPage.TabIndex = 3;
      // 
      // lblReportParameter2
      // 
      this.lblReportParameter2.AutoSize = true;
      this.lblReportParameter2.Location = new System.Drawing.Point(16, 68);
      this.lblReportParameter2.Name = "lblReportParameter2";
      this.lblReportParameter2.Size = new System.Drawing.Size(97, 13);
      this.lblReportParameter2.TabIndex = 1;
      this.lblReportParameter2.Text = "Report parameter:";
      // 
      // lblReportPage
      // 
      this.lblReportPage.AutoSize = true;
      this.lblReportPage.Location = new System.Drawing.Point(16, 16);
      this.lblReportPage.Name = "lblReportPage";
      this.lblReportPage.Size = new System.Drawing.Size(71, 13);
      this.lblReportPage.TabIndex = 0;
      this.lblReportPage.Text = "Report page:";
      // 
      // pnCustom
      // 
      this.pnCustom.BackColor = System.Drawing.SystemColors.Window;
      this.pnCustom.Controls.Add(this.tbCustomExpression);
      this.pnCustom.Controls.Add(this.tbCustomValue);
      this.pnCustom.Controls.Add(this.lblCustomExpression);
      this.pnCustom.Controls.Add(this.lblCustom);
      this.pnCustom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnCustom.Location = new System.Drawing.Point(139, 1);
      this.pnCustom.Name = "pnCustom";
      this.pnCustom.Size = new System.Drawing.Size(400, 314);
      this.pnCustom.TabIndex = 5;
      this.pnCustom.Text = "Custom";
      // 
      // tbCustomExpression
      // 
      this.tbCustomExpression.Image = null;
      this.tbCustomExpression.Location = new System.Drawing.Point(16, 88);
      this.tbCustomExpression.Name = "tbCustomExpression";
      this.tbCustomExpression.Size = new System.Drawing.Size(368, 21);
      this.tbCustomExpression.TabIndex = 6;
      this.tbCustomExpression.ButtonClick += new System.EventHandler(this.tbCustomExpression_ButtonClick);
      // 
      // tbCustomValue
      // 
      this.tbCustomValue.Location = new System.Drawing.Point(16, 36);
      this.tbCustomValue.Name = "tbCustomValue";
      this.tbCustomValue.Size = new System.Drawing.Size(368, 20);
      this.tbCustomValue.TabIndex = 5;
      // 
      // lblCustomExpression
      // 
      this.lblCustomExpression.AutoSize = true;
      this.lblCustomExpression.Location = new System.Drawing.Point(16, 68);
      this.lblCustomExpression.Name = "lblCustomExpression";
      this.lblCustomExpression.Size = new System.Drawing.Size(223, 13);
      this.lblCustomExpression.TabIndex = 3;
      this.lblCustomExpression.Text = "or enter the expression that returns a value:";
      // 
      // lblCustom
      // 
      this.lblCustom.AutoSize = true;
      this.lblCustom.Location = new System.Drawing.Point(16, 16);
      this.lblCustom.Name = "lblCustom";
      this.lblCustom.Size = new System.Drawing.Size(130, 13);
      this.lblCustom.TabIndex = 4;
      this.lblCustom.Text = "Specify a hyperlink value:";
      // 
      // lblHint2
      // 
      this.lblHint2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblHint2.Location = new System.Drawing.Point(0, 28);
      this.lblHint2.Name = "lblHint2";
      this.lblHint2.Size = new System.Drawing.Size(368, 28);
      this.lblHint2.TabIndex = 0;
      this.lblHint2.Text = "Specified URL will opened.";
      this.lblHint2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbModifyAppearance
      // 
      this.cbModifyAppearance.AutoSize = true;
      this.cbModifyAppearance.Location = new System.Drawing.Point(12, 336);
      this.cbModifyAppearance.Name = "cbModifyAppearance";
      this.cbModifyAppearance.Size = new System.Drawing.Size(330, 17);
      this.cbModifyAppearance.TabIndex = 2;
      this.cbModifyAppearance.Text = "Modify the object\'s appearance so it will look like a clickable link.";
      this.cbModifyAppearance.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.Controls.Add(this.lblHint2);
      this.panel1.Controls.Add(this.lblHint1);
      this.panel1.Location = new System.Drawing.Point(168, 254);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(368, 56);
      this.panel1.TabIndex = 4;
      // 
      // lblHint1
      // 
      this.lblHint1.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblHint1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblHint1.Location = new System.Drawing.Point(0, 0);
      this.lblHint1.Name = "lblHint1";
      this.lblHint1.Size = new System.Drawing.Size(368, 28);
      this.lblHint1.TabIndex = 0;
      this.lblHint1.Text = "What will happen if you click this object in the preview window:";
      this.lblHint1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // HyperlinkEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(563, 400);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.cbModifyAppearance);
      this.Controls.Add(this.pageControl1);
      this.Name = "HyperlinkEditorForm";
      this.Text = "Edit Hyperlink";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HyperlinkEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pageControl1, 0);
      this.Controls.SetChildIndex(this.cbModifyAppearance, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.pageControl1.ResumeLayout(false);
      this.pnUrl.ResumeLayout(false);
      this.pnUrl.PerformLayout();
      this.pnPageNumber.ResumeLayout(false);
      this.pnPageNumber.PerformLayout();
      this.pnBookmark.ResumeLayout(false);
      this.pnBookmark.PerformLayout();
      this.pnReport.ResumeLayout(false);
      this.pnReport.PerformLayout();
      this.pnReportPage.ResumeLayout(false);
      this.pnReportPage.PerformLayout();
      this.pnCustom.ResumeLayout(false);
      this.pnCustom.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private FastReport.Controls.PageControl pageControl1;
    private FastReport.Controls.PageControlPage pnUrl;
    private FastReport.Controls.PageControlPage pnPageNumber;
    private FastReport.Controls.PageControlPage pnBookmark;
    private System.Windows.Forms.TextBox tbUrlValue;
    private System.Windows.Forms.Label lblUrl;
    private System.Windows.Forms.Label lblPageNumber;
    private FastReport.Controls.TextBoxButton tbBookmarkValue;
    private System.Windows.Forms.Label lblBookmark;
    private FastReport.Controls.PageControlPage pnReport;
    private System.Windows.Forms.Label lblReportParameter1;
    private FastReport.Controls.TextBoxButton tbReport;
    private System.Windows.Forms.Label lblReport;
    private FastReport.Controls.PageControlPage pnReportPage;
    private FastReport.Controls.PageControlPage pnCustom;
    private System.Windows.Forms.Label lblReportParameter2;
    private System.Windows.Forms.Label lblReportPage;
    private FastReport.Controls.ComponentRefComboBox cbxReportPage;
    private FastReport.Controls.ParametersComboBox cbxReportParameter1;
    private FastReport.Controls.ParametersComboBox cbxReportParameter2;
    private FastReport.Controls.TextBoxButton tbUrlExpression;
    private System.Windows.Forms.Label lblHint2;
    private System.Windows.Forms.Label lblUrlExpression;
    private System.Windows.Forms.CheckBox cbModifyAppearance;
    private FastReport.Controls.TextBoxButton tbPageNumberExpression;
    private System.Windows.Forms.Label lblPageNumberExpression;
    private FastReport.Controls.TextBoxButton tbBookmarkExpression;
    private System.Windows.Forms.Label lblBookmarkExpression;
    private FastReport.Controls.TextBoxButton tbCustomExpression;
    private System.Windows.Forms.TextBox tbCustomValue;
    private System.Windows.Forms.Label lblCustomExpression;
    private System.Windows.Forms.Label lblCustom;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblHint1;
    private FastReport.Controls.TextBoxButton tbParameterExpression1;
    private System.Windows.Forms.TextBox tbParameterValue1;
    private System.Windows.Forms.Label lblParameterExpression1;
    private System.Windows.Forms.Label lblParameterValue1;
    private FastReport.Controls.TextBoxButton tbParameterExpression2;
    private System.Windows.Forms.TextBox tbParameterValue2;
    private System.Windows.Forms.Label lblParameterExpression2;
    private System.Windows.Forms.Label lblParameterValue2;
    private System.Windows.Forms.TextBox tbPageNumberValue;
  }
}
