using System.Drawing.Drawing2D;

namespace FastReport.Forms
{
    partial class OutlineEditorForm
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
            this.cbxLineStyle = new System.Windows.Forms.ComboBox();
            this.cbxLineWidth = new FastReport.Controls.LineWidthComboBox();
            this.lblLineWidth = new System.Windows.Forms.Label();
            this.gbLine = new System.Windows.Forms.GroupBox();
            this.cbxDrawBehind = new System.Windows.Forms.CheckBox();
            this.lblStyle = new System.Windows.Forms.Label();
            this.cbxLineColor = new FastReport.Controls.ColorComboBox();
            this.lblLineColor = new System.Windows.Forms.Label();
            this.cbxEnabled = new System.Windows.Forms.CheckBox();
            this.gbLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(138, 141);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 141);
            // 
            // cbxLineStyle
            // 
            this.cbxLineStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cbxLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLineStyle.Items.AddRange(new object[] {
            "Solid",
            "Dot",
            "Dash",
            "DashDot",
            "DashDotDot"});
            this.cbxLineStyle.Location = new System.Drawing.Point(15, 32);
            this.cbxLineStyle.Name = "cbxLineStyle";
            this.cbxLineStyle.Size = new System.Drawing.Size(82, 21);
            this.cbxLineStyle.TabIndex = 0;
            this.cbxLineStyle.SelectedValueChanged += new System.EventHandler(this.cbxLineStyle_SelectedValueChanged);
            // 
            // cbxLineWidth
            // 
            this.cbxLineWidth.Location = new System.Drawing.Point(112, 32);
            this.cbxLineWidth.Name = "cbxLineWidth";
            this.cbxLineWidth.Size = new System.Drawing.Size(70, 21);
            this.cbxLineWidth.TabIndex = 2;
            this.cbxLineWidth.Text = "1";
            this.cbxLineWidth.WidthSelected += new System.EventHandler(this.cbxLineWidth_WidthSelected);
            // 
            // lblLineWidth
            // 
            this.lblLineWidth.AutoSize = true;
            this.lblLineWidth.Location = new System.Drawing.Point(109, 16);
            this.lblLineWidth.Name = "lblLineWidth";
            this.lblLineWidth.Size = new System.Drawing.Size(35, 13);
            this.lblLineWidth.TabIndex = 3;
            this.lblLineWidth.Text = "Width";
            // 
            // gbLine
            // 
            this.gbLine.Controls.Add(this.cbxDrawBehind);
            this.gbLine.Controls.Add(this.cbxLineStyle);
            this.gbLine.Controls.Add(this.lblStyle);
            this.gbLine.Controls.Add(this.cbxLineWidth);
            this.gbLine.Controls.Add(this.lblLineWidth);
            this.gbLine.Controls.Add(this.cbxLineColor);
            this.gbLine.Controls.Add(this.lblLineColor);
            this.gbLine.Location = new System.Drawing.Point(12, 35);
            this.gbLine.Name = "gbLine";
            this.gbLine.Size = new System.Drawing.Size(282, 100);
            this.gbLine.TabIndex = 2;
            this.gbLine.TabStop = false;
            // 
            // cbxDrawBehind
            // 
            this.cbxDrawBehind.AutoSize = true;
            this.cbxDrawBehind.Location = new System.Drawing.Point(15, 68);
            this.cbxDrawBehind.Name = "cbxDrawBehind";
            this.cbxDrawBehind.Size = new System.Drawing.Size(86, 17);
            this.cbxDrawBehind.TabIndex = 6;
            this.cbxDrawBehind.Text = "Draw behind";
            this.cbxDrawBehind.UseVisualStyleBackColor = true;
            this.cbxDrawBehind.CheckedChanged += new System.EventHandler(this.cbxDrawBehind_CheckedChanged);
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
            // cbxLineColor
            // 
            this.cbxLineColor.Color = System.Drawing.Color.Transparent;
            this.cbxLineColor.Location = new System.Drawing.Point(197, 32);
            this.cbxLineColor.Name = "cbxLineColor";
            this.cbxLineColor.Size = new System.Drawing.Size(70, 21);
            this.cbxLineColor.TabIndex = 4;
            this.cbxLineColor.ColorSelected += new System.EventHandler(this.cbxLineColor_ColorSelected);
            // 
            // lblLineColor
            // 
            this.lblLineColor.AutoSize = true;
            this.lblLineColor.Location = new System.Drawing.Point(194, 16);
            this.lblLineColor.Name = "lblLineColor";
            this.lblLineColor.Size = new System.Drawing.Size(32, 13);
            this.lblLineColor.TabIndex = 5;
            this.lblLineColor.Text = "Color";
            // 
            // cbxEnabled
            // 
            this.cbxEnabled.AutoSize = true;
            this.cbxEnabled.Location = new System.Drawing.Point(12, 12);
            this.cbxEnabled.Name = "cbxEnabled";
            this.cbxEnabled.Size = new System.Drawing.Size(64, 17);
            this.cbxEnabled.TabIndex = 3;
            this.cbxEnabled.Text = "Enabled";
            this.cbxEnabled.UseVisualStyleBackColor = true;
            this.cbxEnabled.CheckedChanged += new System.EventHandler(this.cbxEnabled_CheckedChanged);
            // 
            // OutlineEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 176);
            this.Controls.Add(this.cbxEnabled);
            this.Controls.Add(this.gbLine);
            this.Name = "OutlineEditorForm";
            this.Text = "Outline Editor";
            this.Controls.SetChildIndex(this.gbLine, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.cbxEnabled, 0);
            this.gbLine.ResumeLayout(false);
            this.gbLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxLineStyle;
        private FastReport.Controls.LineWidthComboBox cbxLineWidth;
        private System.Windows.Forms.Label lblLineWidth;
        private System.Windows.Forms.GroupBox gbLine;
        private FastReport.Controls.ColorComboBox cbxLineColor;
        private System.Windows.Forms.Label lblLineColor;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.CheckBox cbxDrawBehind;
        private System.Windows.Forms.CheckBox cbxEnabled;
    }
}