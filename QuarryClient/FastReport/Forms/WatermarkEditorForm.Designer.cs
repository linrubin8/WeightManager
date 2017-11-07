namespace FastReport.Forms
{
  partial class WatermarkEditorForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatermarkEditorForm));
      this.cbxRotation = new System.Windows.Forms.ComboBox();
      this.cbxText = new System.Windows.Forms.ComboBox();
      this.lblRotation = new System.Windows.Forms.Label();
      this.lblText = new System.Windows.Forms.Label();
      this.cbEnabled = new System.Windows.Forms.CheckBox();
      this.trbTransparency = new System.Windows.Forms.TrackBar();
      this.lblTransparency = new System.Windows.Forms.Label();
      this.cbxSize = new System.Windows.Forms.ComboBox();
      this.lblSize = new System.Windows.Forms.Label();
      this.gbZorder = new System.Windows.Forms.GroupBox();
      this.cbPictureOnTop = new System.Windows.Forms.CheckBox();
      this.cbTextOnTop = new System.Windows.Forms.CheckBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cbApplyToAll = new System.Windows.Forms.CheckBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.pgText = new System.Windows.Forms.TabPage();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.btnFont = new System.Windows.Forms.ToolStripButton();
      this.btnColor = new System.Windows.Forms.ToolStripButton();
      this.pgPicture = new System.Windows.Forms.TabPage();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnLoad = new System.Windows.Forms.ToolStripButton();
      this.btnClear = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).BeginInit();
      this.gbZorder.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.pgText.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.pgPicture.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(284, 348);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(364, 348);
      // 
      // cbxRotation
      // 
      this.cbxRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxRotation.FormattingEnabled = true;
      this.cbxRotation.Location = new System.Drawing.Point(92, 80);
      this.cbxRotation.Name = "cbxRotation";
      this.cbxRotation.Size = new System.Drawing.Size(132, 21);
      this.cbxRotation.TabIndex = 6;
      this.cbxRotation.SelectedIndexChanged += new System.EventHandler(this.cbxRotation_SelectedIndexChanged);
      // 
      // cbxText
      // 
      this.cbxText.FormattingEnabled = true;
      this.cbxText.Location = new System.Drawing.Point(92, 52);
      this.cbxText.Name = "cbxText";
      this.cbxText.Size = new System.Drawing.Size(132, 21);
      this.cbxText.TabIndex = 6;
      this.cbxText.TextChanged += new System.EventHandler(this.cbxText_TextChanged);
      // 
      // lblRotation
      // 
      this.lblRotation.AutoSize = true;
      this.lblRotation.Location = new System.Drawing.Point(12, 84);
      this.lblRotation.Name = "lblRotation";
      this.lblRotation.Size = new System.Drawing.Size(48, 13);
      this.lblRotation.TabIndex = 4;
      this.lblRotation.Text = "Rotation";
      // 
      // lblText
      // 
      this.lblText.AutoSize = true;
      this.lblText.Location = new System.Drawing.Point(12, 56);
      this.lblText.Name = "lblText";
      this.lblText.Size = new System.Drawing.Size(29, 13);
      this.lblText.TabIndex = 0;
      this.lblText.Text = "Text";
      // 
      // cbEnabled
      // 
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Location = new System.Drawing.Point(8, 8);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new System.Drawing.Size(64, 17);
      this.cbEnabled.TabIndex = 2;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      this.cbEnabled.CheckedChanged += new System.EventHandler(this.cbEnabled_CheckedChanged);
      // 
      // trbTransparency
      // 
      this.trbTransparency.AutoSize = false;
      this.trbTransparency.BackColor = System.Drawing.SystemColors.Window;
      this.trbTransparency.Location = new System.Drawing.Point(4, 104);
      this.trbTransparency.Maximum = 100;
      this.trbTransparency.Name = "trbTransparency";
      this.trbTransparency.Size = new System.Drawing.Size(228, 32);
      this.trbTransparency.TabIndex = 5;
      this.trbTransparency.TickFrequency = 10;
      this.trbTransparency.ValueChanged += new System.EventHandler(this.trbTransparency_ValueChanged);
      // 
      // lblTransparency
      // 
      this.lblTransparency.AutoSize = true;
      this.lblTransparency.Location = new System.Drawing.Point(12, 84);
      this.lblTransparency.Name = "lblTransparency";
      this.lblTransparency.Size = new System.Drawing.Size(73, 13);
      this.lblTransparency.TabIndex = 4;
      this.lblTransparency.Text = "Transparency";
      // 
      // cbxSize
      // 
      this.cbxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxSize.FormattingEnabled = true;
      this.cbxSize.Location = new System.Drawing.Point(92, 52);
      this.cbxSize.Name = "cbxSize";
      this.cbxSize.Size = new System.Drawing.Size(132, 21);
      this.cbxSize.TabIndex = 3;
      this.cbxSize.SelectedIndexChanged += new System.EventHandler(this.cbxSize_SelectedIndexChanged);
      // 
      // lblSize
      // 
      this.lblSize.AutoSize = true;
      this.lblSize.Location = new System.Drawing.Point(12, 56);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(26, 13);
      this.lblSize.TabIndex = 2;
      this.lblSize.Text = "Size";
      // 
      // gbZorder
      // 
      this.gbZorder.BackColor = System.Drawing.SystemColors.Window;
      this.gbZorder.Controls.Add(this.cbPictureOnTop);
      this.gbZorder.Controls.Add(this.cbTextOnTop);
      this.gbZorder.Location = new System.Drawing.Point(24, 238);
      this.gbZorder.Name = "gbZorder";
      this.gbZorder.Size = new System.Drawing.Size(212, 72);
      this.gbZorder.TabIndex = 4;
      this.gbZorder.TabStop = false;
      this.gbZorder.Text = "Z-Order";
      // 
      // cbPictureOnTop
      // 
      this.cbPictureOnTop.AutoSize = true;
      this.cbPictureOnTop.Location = new System.Drawing.Point(12, 44);
      this.cbPictureOnTop.Name = "cbPictureOnTop";
      this.cbPictureOnTop.Size = new System.Drawing.Size(93, 17);
      this.cbPictureOnTop.TabIndex = 1;
      this.cbPictureOnTop.Text = "Picture on top";
      this.cbPictureOnTop.UseVisualStyleBackColor = true;
      this.cbPictureOnTop.CheckedChanged += new System.EventHandler(this.cbPictureOnTop_CheckedChanged);
      // 
      // cbTextOnTop
      // 
      this.cbTextOnTop.AutoSize = true;
      this.cbTextOnTop.Location = new System.Drawing.Point(12, 20);
      this.cbTextOnTop.Name = "cbTextOnTop";
      this.cbTextOnTop.Size = new System.Drawing.Size(82, 17);
      this.cbTextOnTop.TabIndex = 0;
      this.cbTextOnTop.Text = "Text on top";
      this.cbTextOnTop.UseVisualStyleBackColor = true;
      this.cbTextOnTop.CheckedChanged += new System.EventHandler(this.cbTextOnTop_CheckedChanged);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.Location = new System.Drawing.Point(252, 72);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(168, 240);
      this.panel1.TabIndex = 0;
      this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
      // 
      // cbApplyToAll
      // 
      this.cbApplyToAll.AutoSize = true;
      this.cbApplyToAll.Location = new System.Drawing.Point(8, 348);
      this.cbApplyToAll.Name = "cbApplyToAll";
      this.cbApplyToAll.Size = new System.Drawing.Size(111, 17);
      this.cbApplyToAll.TabIndex = 6;
      this.cbApplyToAll.Text = "Apply to all pages";
      this.cbApplyToAll.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.pgText);
      this.tabControl1.Controls.Add(this.pgPicture);
      this.tabControl1.Location = new System.Drawing.Point(8, 32);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(432, 300);
      this.tabControl1.TabIndex = 7;
      // 
      // pgText
      // 
      this.pgText.BackColor = System.Drawing.SystemColors.Window;
      this.pgText.Controls.Add(this.toolStrip2);
      this.pgText.Controls.Add(this.cbxRotation);
      this.pgText.Controls.Add(this.lblText);
      this.pgText.Controls.Add(this.lblRotation);
      this.pgText.Controls.Add(this.cbxText);
      this.pgText.Location = new System.Drawing.Point(4, 22);
      this.pgText.Name = "pgText";
      this.pgText.Padding = new System.Windows.Forms.Padding(3);
      this.pgText.Size = new System.Drawing.Size(424, 274);
      this.pgText.TabIndex = 0;
      this.pgText.Text = "Text";
      // 
      // toolStrip2
      // 
      this.toolStrip2.AutoSize = false;
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFont,
            this.btnColor});
      this.toolStrip2.Location = new System.Drawing.Point(12, 16);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(212, 25);
      this.toolStrip2.TabIndex = 7;
      this.toolStrip2.Text = "toolStrip2";
      // 
      // btnFont
      // 
      this.btnFont.Image = ((System.Drawing.Image)(resources.GetObject("btnFont.Image")));
      this.btnFont.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFont.Name = "btnFont";
      this.btnFont.Size = new System.Drawing.Size(49, 22);
      this.btnFont.Text = "Font";
      this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
      // 
      // btnColor
      // 
      this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
      this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnColor.Name = "btnColor";
      this.btnColor.Size = new System.Drawing.Size(52, 22);
      this.btnColor.Text = "Color";
      this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
      // 
      // pgPicture
      // 
      this.pgPicture.BackColor = System.Drawing.SystemColors.Window;
      this.pgPicture.Controls.Add(this.toolStrip1);
      this.pgPicture.Controls.Add(this.trbTransparency);
      this.pgPicture.Controls.Add(this.lblTransparency);
      this.pgPicture.Controls.Add(this.cbxSize);
      this.pgPicture.Controls.Add(this.lblSize);
      this.pgPicture.Location = new System.Drawing.Point(4, 22);
      this.pgPicture.Name = "pgPicture";
      this.pgPicture.Padding = new System.Windows.Forms.Padding(3);
      this.pgPicture.Size = new System.Drawing.Size(424, 274);
      this.pgPicture.TabIndex = 1;
      this.pgPicture.Text = "Picture";
      // 
      // toolStrip1
      // 
      this.toolStrip1.AutoSize = false;
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnClear});
      this.toolStrip1.Location = new System.Drawing.Point(12, 16);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(212, 25);
      this.toolStrip1.TabIndex = 6;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnLoad
      // 
      this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
      this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(50, 22);
      this.btnLoad.Text = "Load";
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnClear
      // 
      this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
      this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(52, 22);
      this.btnClear.Text = "Clear";
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // WatermarkEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(447, 381);
      this.Controls.Add(this.gbZorder);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.cbApplyToAll);
      this.Controls.Add(this.cbEnabled);
      this.Name = "WatermarkEditorForm";
      this.Text = "Watermark";
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.cbEnabled, 0);
      this.Controls.SetChildIndex(this.cbApplyToAll, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.gbZorder, 0);
      ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).EndInit();
      this.gbZorder.ResumeLayout(false);
      this.gbZorder.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.pgText.ResumeLayout(false);
      this.pgText.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.pgPicture.ResumeLayout(false);
      this.pgPicture.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblRotation;
    private System.Windows.Forms.Label lblText;
    private System.Windows.Forms.ComboBox cbxText;
    private System.Windows.Forms.CheckBox cbEnabled;
    private System.Windows.Forms.TrackBar trbTransparency;
    private System.Windows.Forms.Label lblTransparency;
    private System.Windows.Forms.ComboBox cbxSize;
    private System.Windows.Forms.Label lblSize;
    private System.Windows.Forms.ComboBox cbxRotation;
    private System.Windows.Forms.GroupBox gbZorder;
    private System.Windows.Forms.CheckBox cbPictureOnTop;
    private System.Windows.Forms.CheckBox cbTextOnTop;
    private System.Windows.Forms.CheckBox cbApplyToAll;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage pgText;
    private System.Windows.Forms.TabPage pgPicture;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton btnFont;
    private System.Windows.Forms.ToolStripButton btnColor;
    private System.Windows.Forms.ToolStripButton btnLoad;
    private System.Windows.Forms.ToolStripButton btnClear;
  }
}
