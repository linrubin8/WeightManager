namespace FastReport.Map.Forms
{
    partial class MapEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          this.pageControl1 = new FastReport.Controls.PageControl();
          this.pgGeneral = new FastReport.Controls.PageControlPage();
          this.cbMercator = new System.Windows.Forms.CheckBox();
          this.pgColorScale = new FastReport.Controls.PageControlPage();
          this.labelLine1 = new FastReport.Controls.LabelLine();
          this.tabControl1 = new System.Windows.Forms.TabControl();
          this.tabGeneral = new System.Windows.Forms.TabPage();
          this.btnFill = new System.Windows.Forms.Button();
          this.btnBorder = new System.Windows.Forms.Button();
          this.labelLine2 = new FastReport.Controls.LabelLine();
          this.btnD4 = new System.Windows.Forms.Button();
          this.lblDock = new System.Windows.Forms.Label();
          this.btnD6 = new System.Windows.Forms.Button();
          this.btnD1 = new System.Windows.Forms.Button();
          this.btnD7 = new System.Windows.Forms.Button();
          this.btnD2 = new System.Windows.Forms.Button();
          this.btnD8 = new System.Windows.Forms.Button();
          this.btnD3 = new System.Windows.Forms.Button();
          this.btnD5 = new System.Windows.Forms.Button();
          this.tabTitle = new System.Windows.Forms.TabPage();
          this.lblTitleText = new System.Windows.Forms.Label();
          this.lblTitleFont = new System.Windows.Forms.Label();
          this.lblTitleColor = new System.Windows.Forms.Label();
          this.tbTitleFont = new FastReport.Controls.TextBoxButton();
          this.cbxTitleColor = new FastReport.Controls.ColorComboBox();
          this.tbTitleText = new System.Windows.Forms.TextBox();
          this.tabValues = new System.Windows.Forms.TabPage();
          this.lblFont = new System.Windows.Forms.Label();
          this.lblTextColor = new System.Windows.Forms.Label();
          this.tbFont = new FastReport.Controls.TextBoxButton();
          this.lblBorderColor = new System.Windows.Forms.Label();
          this.lblFormat = new System.Windows.Forms.Label();
          this.cbxTextColor = new FastReport.Controls.ColorComboBox();
          this.lblNoDataText = new System.Windows.Forms.Label();
          this.cbxBorderColor = new FastReport.Controls.ColorComboBox();
          this.tbFormat = new System.Windows.Forms.TextBox();
          this.tbNoDataText = new System.Windows.Forms.TextBox();
          this.cbVisible = new System.Windows.Forms.CheckBox();
          this.cbHideIfNoData = new System.Windows.Forms.CheckBox();
          this.pageControl1.SuspendLayout();
          this.pgGeneral.SuspendLayout();
          this.pgColorScale.SuspendLayout();
          this.tabControl1.SuspendLayout();
          this.tabGeneral.SuspendLayout();
          this.tabTitle.SuspendLayout();
          this.tabValues.SuspendLayout();
          this.SuspendLayout();
          // 
          // pageControl1
          // 
          this.pageControl1.Controls.Add(this.pgGeneral);
          this.pageControl1.Controls.Add(this.pgColorScale);
          this.pageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.pageControl1.HighlightPageIndex = -1;
          this.pageControl1.Location = new System.Drawing.Point(0, 0);
          this.pageControl1.Name = "pageControl1";
          this.pageControl1.SelectorWidth = 120;
          this.pageControl1.Size = new System.Drawing.Size(436, 504);
          this.pageControl1.TabIndex = 0;
          this.pageControl1.Text = "pageControl1";
          // 
          // pgGeneral
          // 
          this.pgGeneral.BackColor = System.Drawing.SystemColors.Window;
          this.pgGeneral.Controls.Add(this.cbMercator);
          this.pgGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
          this.pgGeneral.Location = new System.Drawing.Point(120, 1);
          this.pgGeneral.Name = "pgGeneral";
          this.pgGeneral.Size = new System.Drawing.Size(315, 502);
          this.pgGeneral.TabIndex = 0;
          this.pgGeneral.Text = "General";
          // 
          // cbMercator
          // 
          this.cbMercator.AutoSize = true;
          this.cbMercator.Location = new System.Drawing.Point(16, 20);
          this.cbMercator.Name = "cbMercator";
          this.cbMercator.Size = new System.Drawing.Size(117, 17);
          this.cbMercator.TabIndex = 0;
          this.cbMercator.Text = "Mercator projection";
          this.cbMercator.UseVisualStyleBackColor = true;
          this.cbMercator.CheckedChanged += new System.EventHandler(this.cbMercator_CheckedChanged);
          // 
          // pgColorScale
          // 
          this.pgColorScale.BackColor = System.Drawing.SystemColors.Window;
          this.pgColorScale.Controls.Add(this.labelLine1);
          this.pgColorScale.Controls.Add(this.tabControl1);
          this.pgColorScale.Controls.Add(this.cbVisible);
          this.pgColorScale.Controls.Add(this.cbHideIfNoData);
          this.pgColorScale.Dock = System.Windows.Forms.DockStyle.Fill;
          this.pgColorScale.Location = new System.Drawing.Point(120, 1);
          this.pgColorScale.Name = "pgColorScale";
          this.pgColorScale.Size = new System.Drawing.Size(315, 502);
          this.pgColorScale.TabIndex = 1;
          this.pgColorScale.Text = "Color scale";
          // 
          // labelLine1
          // 
          this.labelLine1.Location = new System.Drawing.Point(16, 64);
          this.labelLine1.Name = "labelLine1";
          this.labelLine1.Size = new System.Drawing.Size(284, 12);
          this.labelLine1.TabIndex = 11;
          // 
          // tabControl1
          // 
          this.tabControl1.Controls.Add(this.tabGeneral);
          this.tabControl1.Controls.Add(this.tabTitle);
          this.tabControl1.Controls.Add(this.tabValues);
          this.tabControl1.Location = new System.Drawing.Point(16, 84);
          this.tabControl1.Name = "tabControl1";
          this.tabControl1.SelectedIndex = 0;
          this.tabControl1.Size = new System.Drawing.Size(284, 400);
          this.tabControl1.TabIndex = 10;
          // 
          // tabGeneral
          // 
          this.tabGeneral.Controls.Add(this.btnFill);
          this.tabGeneral.Controls.Add(this.btnBorder);
          this.tabGeneral.Controls.Add(this.labelLine2);
          this.tabGeneral.Controls.Add(this.btnD4);
          this.tabGeneral.Controls.Add(this.lblDock);
          this.tabGeneral.Controls.Add(this.btnD6);
          this.tabGeneral.Controls.Add(this.btnD1);
          this.tabGeneral.Controls.Add(this.btnD7);
          this.tabGeneral.Controls.Add(this.btnD2);
          this.tabGeneral.Controls.Add(this.btnD8);
          this.tabGeneral.Controls.Add(this.btnD3);
          this.tabGeneral.Controls.Add(this.btnD5);
          this.tabGeneral.Location = new System.Drawing.Point(4, 22);
          this.tabGeneral.Name = "tabGeneral";
          this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
          this.tabGeneral.Size = new System.Drawing.Size(276, 374);
          this.tabGeneral.TabIndex = 0;
          this.tabGeneral.Text = "General";
          this.tabGeneral.UseVisualStyleBackColor = true;
          // 
          // btnFill
          // 
          this.btnFill.Location = new System.Drawing.Point(140, 16);
          this.btnFill.Name = "btnFill";
          this.btnFill.Size = new System.Drawing.Size(120, 23);
          this.btnFill.TabIndex = 1;
          this.btnFill.Text = "Fill...";
          this.btnFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
          this.btnFill.UseVisualStyleBackColor = true;
          this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
          // 
          // btnBorder
          // 
          this.btnBorder.Location = new System.Drawing.Point(16, 16);
          this.btnBorder.Name = "btnBorder";
          this.btnBorder.Size = new System.Drawing.Size(120, 23);
          this.btnBorder.TabIndex = 1;
          this.btnBorder.Text = "Border...";
          this.btnBorder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
          this.btnBorder.UseVisualStyleBackColor = true;
          this.btnBorder.Click += new System.EventHandler(this.btnBorder_Click);
          // 
          // labelLine2
          // 
          this.labelLine2.Location = new System.Drawing.Point(16, 48);
          this.labelLine2.Name = "labelLine2";
          this.labelLine2.Size = new System.Drawing.Size(244, 16);
          this.labelLine2.TabIndex = 10;
          // 
          // btnD4
          // 
          this.btnD4.Location = new System.Drawing.Point(200, 92);
          this.btnD4.Name = "btnD4";
          this.btnD4.Size = new System.Drawing.Size(20, 20);
          this.btnD4.TabIndex = 9;
          this.btnD4.UseVisualStyleBackColor = true;
          this.btnD4.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // lblDock
          // 
          this.lblDock.AutoSize = true;
          this.lblDock.Location = new System.Drawing.Point(12, 96);
          this.lblDock.Name = "lblDock";
          this.lblDock.Size = new System.Drawing.Size(36, 13);
          this.lblDock.TabIndex = 2;
          this.lblDock.Text = "Dock:";
          // 
          // btnD6
          // 
          this.btnD6.Location = new System.Drawing.Point(200, 112);
          this.btnD6.Name = "btnD6";
          this.btnD6.Size = new System.Drawing.Size(20, 20);
          this.btnD6.TabIndex = 9;
          this.btnD6.UseVisualStyleBackColor = true;
          this.btnD6.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD1
          // 
          this.btnD1.Location = new System.Drawing.Point(200, 72);
          this.btnD1.Name = "btnD1";
          this.btnD1.Size = new System.Drawing.Size(20, 20);
          this.btnD1.TabIndex = 9;
          this.btnD1.Tag = "";
          this.btnD1.UseVisualStyleBackColor = true;
          this.btnD1.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD7
          // 
          this.btnD7.Location = new System.Drawing.Point(220, 112);
          this.btnD7.Name = "btnD7";
          this.btnD7.Size = new System.Drawing.Size(20, 20);
          this.btnD7.TabIndex = 9;
          this.btnD7.UseVisualStyleBackColor = true;
          this.btnD7.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD2
          // 
          this.btnD2.Location = new System.Drawing.Point(220, 72);
          this.btnD2.Name = "btnD2";
          this.btnD2.Size = new System.Drawing.Size(20, 20);
          this.btnD2.TabIndex = 9;
          this.btnD2.UseVisualStyleBackColor = true;
          this.btnD2.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD8
          // 
          this.btnD8.Location = new System.Drawing.Point(240, 112);
          this.btnD8.Name = "btnD8";
          this.btnD8.Size = new System.Drawing.Size(20, 20);
          this.btnD8.TabIndex = 9;
          this.btnD8.UseVisualStyleBackColor = true;
          this.btnD8.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD3
          // 
          this.btnD3.Location = new System.Drawing.Point(240, 72);
          this.btnD3.Name = "btnD3";
          this.btnD3.Size = new System.Drawing.Size(20, 20);
          this.btnD3.TabIndex = 9;
          this.btnD3.UseVisualStyleBackColor = true;
          this.btnD3.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // btnD5
          // 
          this.btnD5.Location = new System.Drawing.Point(240, 92);
          this.btnD5.Name = "btnD5";
          this.btnD5.Size = new System.Drawing.Size(20, 20);
          this.btnD5.TabIndex = 9;
          this.btnD5.UseVisualStyleBackColor = true;
          this.btnD5.Click += new System.EventHandler(this.btnD1_Click);
          // 
          // tabTitle
          // 
          this.tabTitle.Controls.Add(this.lblTitleText);
          this.tabTitle.Controls.Add(this.lblTitleFont);
          this.tabTitle.Controls.Add(this.lblTitleColor);
          this.tabTitle.Controls.Add(this.tbTitleFont);
          this.tabTitle.Controls.Add(this.cbxTitleColor);
          this.tabTitle.Controls.Add(this.tbTitleText);
          this.tabTitle.Location = new System.Drawing.Point(4, 22);
          this.tabTitle.Name = "tabTitle";
          this.tabTitle.Padding = new System.Windows.Forms.Padding(3);
          this.tabTitle.Size = new System.Drawing.Size(276, 374);
          this.tabTitle.TabIndex = 2;
          this.tabTitle.Text = "Title";
          this.tabTitle.UseVisualStyleBackColor = true;
          // 
          // lblTitleText
          // 
          this.lblTitleText.AutoSize = true;
          this.lblTitleText.Location = new System.Drawing.Point(12, 20);
          this.lblTitleText.Name = "lblTitleText";
          this.lblTitleText.Size = new System.Drawing.Size(31, 13);
          this.lblTitleText.TabIndex = 11;
          this.lblTitleText.Text = "Text:";
          // 
          // lblTitleFont
          // 
          this.lblTitleFont.AutoSize = true;
          this.lblTitleFont.Location = new System.Drawing.Point(12, 48);
          this.lblTitleFont.Name = "lblTitleFont";
          this.lblTitleFont.Size = new System.Drawing.Size(31, 13);
          this.lblTitleFont.TabIndex = 10;
          this.lblTitleFont.Text = "Font:";
          // 
          // lblTitleColor
          // 
          this.lblTitleColor.AutoSize = true;
          this.lblTitleColor.Location = new System.Drawing.Point(12, 76);
          this.lblTitleColor.Name = "lblTitleColor";
          this.lblTitleColor.Size = new System.Drawing.Size(57, 13);
          this.lblTitleColor.TabIndex = 9;
          this.lblTitleColor.Text = "Text color:";
          // 
          // tbTitleFont
          // 
          this.tbTitleFont.Image = null;
          this.tbTitleFont.Location = new System.Drawing.Point(128, 44);
          this.tbTitleFont.Name = "tbTitleFont";
          this.tbTitleFont.Size = new System.Drawing.Size(132, 21);
          this.tbTitleFont.TabIndex = 12;
          this.tbTitleFont.ButtonClick += new System.EventHandler(this.tbTitleFont_ButtonClick);
          // 
          // cbxTitleColor
          // 
          this.cbxTitleColor.Color = System.Drawing.Color.Transparent;
          this.cbxTitleColor.Location = new System.Drawing.Point(128, 72);
          this.cbxTitleColor.Name = "cbxTitleColor";
          this.cbxTitleColor.ShowColorName = true;
          this.cbxTitleColor.Size = new System.Drawing.Size(132, 21);
          this.cbxTitleColor.TabIndex = 13;
          this.cbxTitleColor.ColorSelected += new System.EventHandler(this.cbxTitleColor_ColorSelected);
          // 
          // tbTitleText
          // 
          this.tbTitleText.Location = new System.Drawing.Point(128, 16);
          this.tbTitleText.Name = "tbTitleText";
          this.tbTitleText.Size = new System.Drawing.Size(132, 20);
          this.tbTitleText.TabIndex = 14;
          this.tbTitleText.Leave += new System.EventHandler(this.tbTitleText_Leave);
          // 
          // tabValues
          // 
          this.tabValues.Controls.Add(this.lblFont);
          this.tabValues.Controls.Add(this.lblTextColor);
          this.tabValues.Controls.Add(this.tbFont);
          this.tabValues.Controls.Add(this.lblBorderColor);
          this.tabValues.Controls.Add(this.lblFormat);
          this.tabValues.Controls.Add(this.cbxTextColor);
          this.tabValues.Controls.Add(this.lblNoDataText);
          this.tabValues.Controls.Add(this.cbxBorderColor);
          this.tabValues.Controls.Add(this.tbFormat);
          this.tabValues.Controls.Add(this.tbNoDataText);
          this.tabValues.Location = new System.Drawing.Point(4, 22);
          this.tabValues.Name = "tabValues";
          this.tabValues.Padding = new System.Windows.Forms.Padding(3);
          this.tabValues.Size = new System.Drawing.Size(276, 374);
          this.tabValues.TabIndex = 3;
          this.tabValues.Text = "Values";
          this.tabValues.UseVisualStyleBackColor = true;
          // 
          // lblFont
          // 
          this.lblFont.AutoSize = true;
          this.lblFont.Location = new System.Drawing.Point(12, 20);
          this.lblFont.Name = "lblFont";
          this.lblFont.Size = new System.Drawing.Size(31, 13);
          this.lblFont.TabIndex = 13;
          this.lblFont.Text = "Font:";
          // 
          // lblTextColor
          // 
          this.lblTextColor.AutoSize = true;
          this.lblTextColor.Location = new System.Drawing.Point(12, 48);
          this.lblTextColor.Name = "lblTextColor";
          this.lblTextColor.Size = new System.Drawing.Size(57, 13);
          this.lblTextColor.TabIndex = 12;
          this.lblTextColor.Text = "Text color:";
          // 
          // tbFont
          // 
          this.tbFont.Image = null;
          this.tbFont.Location = new System.Drawing.Point(128, 16);
          this.tbFont.Name = "tbFont";
          this.tbFont.Size = new System.Drawing.Size(132, 21);
          this.tbFont.TabIndex = 14;
          this.tbFont.ButtonClick += new System.EventHandler(this.tbFont_ButtonClick);
          // 
          // lblBorderColor
          // 
          this.lblBorderColor.AutoSize = true;
          this.lblBorderColor.Location = new System.Drawing.Point(12, 76);
          this.lblBorderColor.Name = "lblBorderColor";
          this.lblBorderColor.Size = new System.Drawing.Size(67, 13);
          this.lblBorderColor.TabIndex = 9;
          this.lblBorderColor.Text = "Border color:";
          // 
          // lblFormat
          // 
          this.lblFormat.AutoSize = true;
          this.lblFormat.Location = new System.Drawing.Point(12, 104);
          this.lblFormat.Name = "lblFormat";
          this.lblFormat.Size = new System.Drawing.Size(42, 13);
          this.lblFormat.TabIndex = 10;
          this.lblFormat.Text = "Format:";
          // 
          // cbxTextColor
          // 
          this.cbxTextColor.Color = System.Drawing.Color.Transparent;
          this.cbxTextColor.Location = new System.Drawing.Point(128, 44);
          this.cbxTextColor.Name = "cbxTextColor";
          this.cbxTextColor.ShowColorName = true;
          this.cbxTextColor.Size = new System.Drawing.Size(132, 21);
          this.cbxTextColor.TabIndex = 16;
          this.cbxTextColor.ColorSelected += new System.EventHandler(this.cbxTextColor_ColorSelected);
          // 
          // lblNoDataText
          // 
          this.lblNoDataText.AutoSize = true;
          this.lblNoDataText.Location = new System.Drawing.Point(12, 132);
          this.lblNoDataText.Name = "lblNoDataText";
          this.lblNoDataText.Size = new System.Drawing.Size(68, 13);
          this.lblNoDataText.TabIndex = 11;
          this.lblNoDataText.Text = "No data text:";
          // 
          // cbxBorderColor
          // 
          this.cbxBorderColor.Color = System.Drawing.Color.Transparent;
          this.cbxBorderColor.Location = new System.Drawing.Point(128, 72);
          this.cbxBorderColor.Name = "cbxBorderColor";
          this.cbxBorderColor.ShowColorName = true;
          this.cbxBorderColor.Size = new System.Drawing.Size(132, 21);
          this.cbxBorderColor.TabIndex = 15;
          this.cbxBorderColor.ColorSelected += new System.EventHandler(this.cbxBorderColor_ColorSelected);
          // 
          // tbFormat
          // 
          this.tbFormat.Location = new System.Drawing.Point(128, 100);
          this.tbFormat.Name = "tbFormat";
          this.tbFormat.Size = new System.Drawing.Size(132, 20);
          this.tbFormat.TabIndex = 18;
          this.tbFormat.Leave += new System.EventHandler(this.tbFormat_Leave);
          // 
          // tbNoDataText
          // 
          this.tbNoDataText.Location = new System.Drawing.Point(128, 128);
          this.tbNoDataText.Name = "tbNoDataText";
          this.tbNoDataText.Size = new System.Drawing.Size(132, 20);
          this.tbNoDataText.TabIndex = 17;
          this.tbNoDataText.Leave += new System.EventHandler(this.tbNoDataText_Leave);
          // 
          // cbVisible
          // 
          this.cbVisible.AutoSize = true;
          this.cbVisible.Location = new System.Drawing.Point(16, 16);
          this.cbVisible.Name = "cbVisible";
          this.cbVisible.Size = new System.Drawing.Size(56, 17);
          this.cbVisible.TabIndex = 6;
          this.cbVisible.Text = "Visible";
          this.cbVisible.UseVisualStyleBackColor = true;
          this.cbVisible.CheckedChanged += new System.EventHandler(this.cbVisible_CheckedChanged);
          // 
          // cbHideIfNoData
          // 
          this.cbHideIfNoData.AutoSize = true;
          this.cbHideIfNoData.Location = new System.Drawing.Point(16, 40);
          this.cbHideIfNoData.Name = "cbHideIfNoData";
          this.cbHideIfNoData.Size = new System.Drawing.Size(95, 17);
          this.cbHideIfNoData.TabIndex = 6;
          this.cbHideIfNoData.Text = "Hide if no data";
          this.cbHideIfNoData.UseVisualStyleBackColor = true;
          this.cbHideIfNoData.CheckedChanged += new System.EventHandler(this.cbHideIfNoData_CheckedChanged);
          // 
          // MapEditorControl
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.BackColor = System.Drawing.SystemColors.Control;
          this.Controls.Add(this.pageControl1);
          this.Name = "MapEditorControl";
          this.Size = new System.Drawing.Size(436, 504);
          this.pageControl1.ResumeLayout(false);
          this.pgGeneral.ResumeLayout(false);
          this.pgGeneral.PerformLayout();
          this.pgColorScale.ResumeLayout(false);
          this.pgColorScale.PerformLayout();
          this.tabControl1.ResumeLayout(false);
          this.tabGeneral.ResumeLayout(false);
          this.tabGeneral.PerformLayout();
          this.tabTitle.ResumeLayout(false);
          this.tabTitle.PerformLayout();
          this.tabValues.ResumeLayout(false);
          this.tabValues.PerformLayout();
          this.ResumeLayout(false);

        }

        #endregion

      private FastReport.Controls.PageControl pageControl1;
      private FastReport.Controls.PageControlPage pgGeneral;
      private FastReport.Controls.PageControlPage pgColorScale;
      private System.Windows.Forms.CheckBox cbMercator;
      private System.Windows.Forms.Button btnBorder;
      private System.Windows.Forms.CheckBox cbVisible;
      private System.Windows.Forms.Button btnFill;
      private System.Windows.Forms.CheckBox cbHideIfNoData;
      private System.Windows.Forms.Button btnD4;
      private System.Windows.Forms.Button btnD6;
      private System.Windows.Forms.Button btnD7;
      private System.Windows.Forms.Button btnD8;
      private System.Windows.Forms.Button btnD5;
      private System.Windows.Forms.Button btnD3;
      private System.Windows.Forms.Button btnD2;
      private System.Windows.Forms.Button btnD1;
      private System.Windows.Forms.Label lblDock;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabGeneral;
      private System.Windows.Forms.TabPage tabTitle;
      private FastReport.Controls.LabelLine labelLine1;
      private FastReport.Controls.LabelLine labelLine2;
      private System.Windows.Forms.Label lblTitleText;
      private System.Windows.Forms.Label lblTitleFont;
      private System.Windows.Forms.Label lblTitleColor;
      private FastReport.Controls.TextBoxButton tbTitleFont;
      private FastReport.Controls.ColorComboBox cbxTitleColor;
      private System.Windows.Forms.TextBox tbTitleText;
      private System.Windows.Forms.TabPage tabValues;
      private System.Windows.Forms.Label lblFont;
      private System.Windows.Forms.Label lblTextColor;
      private FastReport.Controls.TextBoxButton tbFont;
      private System.Windows.Forms.Label lblBorderColor;
      private System.Windows.Forms.Label lblFormat;
      private FastReport.Controls.ColorComboBox cbxTextColor;
      private System.Windows.Forms.Label lblNoDataText;
      private FastReport.Controls.ColorComboBox cbxBorderColor;
      private System.Windows.Forms.TextBox tbFormat;
      private System.Windows.Forms.TextBox tbNoDataText;

    }
}
