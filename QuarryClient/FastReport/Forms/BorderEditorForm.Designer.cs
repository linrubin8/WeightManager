using System;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Controls;

namespace FastReport.Forms
{
  partial class BorderEditorForm
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
      this.gbBorder = new System.Windows.Forms.GroupBox();
      this.btnAllLines = new System.Windows.Forms.Button();
      this.btnNoLines = new System.Windows.Forms.Button();
      this.cbTopLine = new System.Windows.Forms.CheckBox();
      this.cbBottomLine = new System.Windows.Forms.CheckBox();
      this.cbLeftLine = new System.Windows.Forms.CheckBox();
      this.cbRightLine = new System.Windows.Forms.CheckBox();
      this.FSample = new FastReport.Controls.BorderSample();
      this.cbShadow = new System.Windows.Forms.CheckBox();
      this.lblShadow = new FastReport.Controls.LabelLine();
      this.cbxShadowWidth = new FastReport.Controls.LineWidthComboBox();
      this.lblShadowWidth = new System.Windows.Forms.Label();
      this.cbxShadowColor = new FastReport.Controls.ColorComboBox();
      this.lblShadowColor = new System.Windows.Forms.Label();
      this.gbLine = new System.Windows.Forms.GroupBox();
      this.lsLineStyle = new FastReport.Controls.LineStyleControl();
      this.lblStyle = new System.Windows.Forms.Label();
      this.cbxLineWidth = new FastReport.Controls.LineWidthComboBox();
      this.lblLineWidth = new System.Windows.Forms.Label();
      this.cbxLineColor = new FastReport.Controls.ColorComboBox();
      this.lblLineColor = new System.Windows.Forms.Label();
      this.lblHint = new System.Windows.Forms.Label();
      this.gbBorder.SuspendLayout();
      this.gbLine.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(140, 276);
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(220, 276);
      // 
      // gbBorder
      // 
      this.gbBorder.Controls.Add(this.btnAllLines);
      this.gbBorder.Controls.Add(this.btnNoLines);
      this.gbBorder.Controls.Add(this.cbTopLine);
      this.gbBorder.Controls.Add(this.cbBottomLine);
      this.gbBorder.Controls.Add(this.cbLeftLine);
      this.gbBorder.Controls.Add(this.cbRightLine);
      this.gbBorder.Controls.Add(this.FSample);
      this.gbBorder.Controls.Add(this.cbShadow);
      this.gbBorder.Controls.Add(this.lblShadow);
      this.gbBorder.Controls.Add(this.cbxShadowWidth);
      this.gbBorder.Controls.Add(this.lblShadowWidth);
      this.gbBorder.Controls.Add(this.cbxShadowColor);
      this.gbBorder.Controls.Add(this.lblShadowColor);
      this.gbBorder.Location = new System.Drawing.Point(8, 4);
      this.gbBorder.Name = "gbBorder";
      this.gbBorder.Size = new System.Drawing.Size(184, 228);
      this.gbBorder.TabIndex = 0;
      this.gbBorder.TabStop = false;
      this.gbBorder.Text = "Border";
      // 
      // btnAllLines
      // 
      this.btnAllLines.Location = new System.Drawing.Point(11, 20);
      this.btnAllLines.Name = "btnAllLines";
      this.btnAllLines.Size = new System.Drawing.Size(24, 24);
      this.btnAllLines.TabIndex = 0;
      this.btnAllLines.Click += new System.EventHandler(this.btnAllLines_Click);
      // 
      // btnNoLines
      // 
      this.btnNoLines.Location = new System.Drawing.Point(36, 20);
      this.btnNoLines.Name = "btnNoLines";
      this.btnNoLines.Size = new System.Drawing.Size(24, 24);
      this.btnNoLines.TabIndex = 1;
      this.btnNoLines.Click += new System.EventHandler(this.btnNoLines_Click);
      // 
      // cbTopLine
      // 
      this.cbTopLine.Appearance = System.Windows.Forms.Appearance.Button;
      this.cbTopLine.Location = new System.Drawing.Point(74, 20);
      this.cbTopLine.Name = "cbTopLine";
      this.cbTopLine.Size = new System.Drawing.Size(24, 24);
      this.cbTopLine.TabIndex = 2;
      this.cbTopLine.Click += new System.EventHandler(this.Line_Click);
      // 
      // cbBottomLine
      // 
      this.cbBottomLine.Appearance = System.Windows.Forms.Appearance.Button;
      this.cbBottomLine.Location = new System.Drawing.Point(99, 20);
      this.cbBottomLine.Name = "cbBottomLine";
      this.cbBottomLine.Size = new System.Drawing.Size(24, 24);
      this.cbBottomLine.TabIndex = 3;
      this.cbBottomLine.Click += new System.EventHandler(this.Line_Click);
      // 
      // cbLeftLine
      // 
      this.cbLeftLine.Appearance = System.Windows.Forms.Appearance.Button;
      this.cbLeftLine.Location = new System.Drawing.Point(124, 20);
      this.cbLeftLine.Name = "cbLeftLine";
      this.cbLeftLine.Size = new System.Drawing.Size(24, 24);
      this.cbLeftLine.TabIndex = 4;
      this.cbLeftLine.Click += new System.EventHandler(this.Line_Click);
      // 
      // cbRightLine
      // 
      this.cbRightLine.Appearance = System.Windows.Forms.Appearance.Button;
      this.cbRightLine.Location = new System.Drawing.Point(149, 20);
      this.cbRightLine.Name = "cbRightLine";
      this.cbRightLine.Size = new System.Drawing.Size(24, 24);
      this.cbRightLine.TabIndex = 5;
      this.cbRightLine.Click += new System.EventHandler(this.Line_Click);
      // 
      // FSample
      // 
      this.FSample.BackColor = System.Drawing.SystemColors.Window;
      this.FSample.Location = new System.Drawing.Point(12, 52);
      this.FSample.Name = "FSample";
      this.FSample.Size = new System.Drawing.Size(160, 94);
      this.FSample.TabIndex = 6;
      this.FSample.ToggleLine += new FastReport.Controls.ToggleLineEventHandler(this.FSample_ToggleLine);
      // 
      // cbShadow
      // 
      this.cbShadow.AutoSize = true;
      this.cbShadow.Location = new System.Drawing.Point(12, 156);
      this.cbShadow.Name = "cbShadow";
      this.cbShadow.Size = new System.Drawing.Size(64, 17);
      this.cbShadow.TabIndex = 6;
      this.cbShadow.Text = "Shadow";
      this.cbShadow.CheckedChanged += new System.EventHandler(this.cbShadow_CheckedChanged);
      // 
      // lblShadow
      // 
      this.lblShadow.Location = new System.Drawing.Point(12, 156);
      this.lblShadow.Name = "lblShadow";
      this.lblShadow.Size = new System.Drawing.Size(158, 16);
      this.lblShadow.TabIndex = 7;
      // 
      // cbxShadowWidth
      // 
      this.cbxShadowWidth.Location = new System.Drawing.Point(12, 192);
      this.cbxShadowWidth.Name = "cbxShadowWidth";
      this.cbxShadowWidth.Size = new System.Drawing.Size(70, 21);
      this.cbxShadowWidth.TabIndex = 8;
      this.cbxShadowWidth.Text = "1";
      this.cbxShadowWidth.WidthSelected += new System.EventHandler(this.cbxShadowWidth_WidthSelected);
      // 
      // lblShadowWidth
      // 
      this.lblShadowWidth.AutoSize = true;
      this.lblShadowWidth.Location = new System.Drawing.Point(12, 176);
      this.lblShadowWidth.Name = "lblShadowWidth";
      this.lblShadowWidth.Size = new System.Drawing.Size(35, 13);
      this.lblShadowWidth.TabIndex = 9;
      this.lblShadowWidth.Text = "Width";
      // 
      // cbxShadowColor
      // 
      this.cbxShadowColor.Color = System.Drawing.Color.Transparent;
      this.cbxShadowColor.Location = new System.Drawing.Point(100, 192);
      this.cbxShadowColor.Name = "cbxShadowColor";
      this.cbxShadowColor.Size = new System.Drawing.Size(70, 21);
      this.cbxShadowColor.TabIndex = 10;
      this.cbxShadowColor.ColorSelected += new System.EventHandler(this.cbxShadowColor_ColorSelected);
      // 
      // lblShadowColor
      // 
      this.lblShadowColor.AutoSize = true;
      this.lblShadowColor.Location = new System.Drawing.Point(100, 176);
      this.lblShadowColor.Name = "lblShadowColor";
      this.lblShadowColor.Size = new System.Drawing.Size(32, 13);
      this.lblShadowColor.TabIndex = 11;
      this.lblShadowColor.Text = "Color";
      // 
      // gbLine
      // 
      this.gbLine.Controls.Add(this.lsLineStyle);
      this.gbLine.Controls.Add(this.lblStyle);
      this.gbLine.Controls.Add(this.cbxLineWidth);
      this.gbLine.Controls.Add(this.lblLineWidth);
      this.gbLine.Controls.Add(this.cbxLineColor);
      this.gbLine.Controls.Add(this.lblLineColor);
      this.gbLine.Location = new System.Drawing.Point(200, 4);
      this.gbLine.Name = "gbLine";
      this.gbLine.Size = new System.Drawing.Size(96, 228);
      this.gbLine.TabIndex = 1;
      this.gbLine.TabStop = false;
      this.gbLine.Text = "Line";
      // 
      // lsLineStyle
      // 
      this.lsLineStyle.BackColor = System.Drawing.SystemColors.Window;
      this.lsLineStyle.LineColor = System.Drawing.Color.Black;
      this.lsLineStyle.LineWidth = 1F;
      this.lsLineStyle.Location = new System.Drawing.Point(12, 32);
      this.lsLineStyle.Name = "lsLineStyle";
      this.lsLineStyle.ShowBorder = true;
      this.lsLineStyle.Size = new System.Drawing.Size(70, 100);
      this.lsLineStyle.Style = FastReport.LineStyle.Solid;
      this.lsLineStyle.TabIndex = 0;
      // 
      // lblStyle
      // 
      this.lblStyle.AutoSize = true;
      this.lblStyle.Location = new System.Drawing.Point(12, 16);
      this.lblStyle.Name = "lblStyle";
      this.lblStyle.Size = new System.Drawing.Size(31, 13);
      this.lblStyle.TabIndex = 1;
      this.lblStyle.Text = "Style";
      // 
      // cbxLineWidth
      // 
      this.cbxLineWidth.Location = new System.Drawing.Point(12, 152);
      this.cbxLineWidth.Name = "cbxLineWidth";
      this.cbxLineWidth.Size = new System.Drawing.Size(70, 21);
      this.cbxLineWidth.TabIndex = 2;
      this.cbxLineWidth.Text = "1";
      this.cbxLineWidth.WidthSelected += new System.EventHandler(this.cbxLineWidth_WidthSelected);
      // 
      // lblLineWidth
      // 
      this.lblLineWidth.AutoSize = true;
      this.lblLineWidth.Location = new System.Drawing.Point(12, 136);
      this.lblLineWidth.Name = "lblLineWidth";
      this.lblLineWidth.Size = new System.Drawing.Size(35, 13);
      this.lblLineWidth.TabIndex = 3;
      this.lblLineWidth.Text = "Width";
      // 
      // cbxLineColor
      // 
      this.cbxLineColor.Color = System.Drawing.Color.Transparent;
      this.cbxLineColor.Location = new System.Drawing.Point(12, 192);
      this.cbxLineColor.Name = "cbxLineColor";
      this.cbxLineColor.Size = new System.Drawing.Size(70, 21);
      this.cbxLineColor.TabIndex = 4;
      this.cbxLineColor.ColorSelected += new System.EventHandler(this.cbxLineColor_ColorSelected);
      // 
      // lblLineColor
      // 
      this.lblLineColor.AutoSize = true;
      this.lblLineColor.Location = new System.Drawing.Point(12, 176);
      this.lblLineColor.Name = "lblLineColor";
      this.lblLineColor.Size = new System.Drawing.Size(32, 13);
      this.lblLineColor.TabIndex = 5;
      this.lblLineColor.Text = "Color";
      // 
      // lblHint
      // 
      this.lblHint.Location = new System.Drawing.Point(8, 236);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(288, 36);
      this.lblHint.TabIndex = 2;
      this.lblHint.Text = "Hint";
      // 
      // BorderEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(305, 309);
      this.Controls.Add(this.gbBorder);
      this.Controls.Add(this.gbLine);
      this.Controls.Add(this.lblHint);
      this.Name = "BorderEditorForm";
      this.Text = "Border Editor";
      this.Shown += new System.EventHandler(this.BorderEditorForm_Shown);
      this.Controls.SetChildIndex(this.lblHint, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbLine, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbBorder, 0);
      this.gbBorder.ResumeLayout(false);
      this.gbBorder.PerformLayout();
      this.gbLine.ResumeLayout(false);
      this.gbLine.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private GroupBox gbBorder;
    private GroupBox gbLine;
    private Button btnAllLines;
    private Button btnNoLines;
    private CheckBox cbTopLine;
    private CheckBox cbBottomLine;
    private CheckBox cbLeftLine;
    private CheckBox cbRightLine;
    private Label lblStyle;
    private LineStyleControl lsLineStyle;
    private Label lblLineWidth;
    private LineWidthComboBox cbxLineWidth;
    private Label lblLineColor;
    private ColorComboBox cbxLineColor;
    private CheckBox cbShadow;
    private LabelLine lblShadow;
    private Label lblShadowWidth;
    private LineWidthComboBox cbxShadowWidth;
    private Label lblShadowColor;
    private ColorComboBox cbxShadowColor;
    private Label lblHint;
    private BorderSample FSample;
  }
}