namespace FastReport.Forms
{
  partial class RichEditorForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichEditorForm));
      this.ts1 = new System.Windows.Forms.ToolStrip();
      this.btnOk = new System.Windows.Forms.ToolStripButton();
      this.btnCancel = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.btnUndo = new System.Windows.Forms.ToolStripButton();
      this.btnRedo = new System.Windows.Forms.ToolStripButton();
      this.cbxZoom = new System.Windows.Forms.ToolStripComboBox();
      this.sep1 = new System.Windows.Forms.ToolStripSeparator();
      this.cbxFontName = new FastReport.Controls.ToolStripFontComboBox();
      this.cbxFontSize = new FastReport.Controls.ToolStripFontSizeComboBox();
      this.btnBold = new System.Windows.Forms.ToolStripButton();
      this.btnItalic = new System.Windows.Forms.ToolStripButton();
      this.btnUnderline = new System.Windows.Forms.ToolStripButton();
      this.sep2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnAlignLeft = new System.Windows.Forms.ToolStripButton();
      this.btnAlignCenter = new System.Windows.Forms.ToolStripButton();
      this.btnAlignRight = new System.Windows.Forms.ToolStripButton();
      this.btnAlignJustify = new System.Windows.Forms.ToolStripButton();
      this.sep3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnColor = new FastReport.Controls.ToolStripColorButton();
      this.btnSubscript = new System.Windows.Forms.ToolStripButton();
      this.btnSuperscript = new System.Windows.Forms.ToolStripButton();
      this.btnBullets = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.rtbText = new FastReport.Controls.FRRichTextBox();
      this.tvData = new FastReport.Controls.DataTreeView();
      this.expandableSplitter1 = new System.Windows.Forms.Splitter();
      this.lblDescription = new FastReport.Controls.DescriptionControl();
      this.ts1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ts1
      // 
      this.ts1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOk,
            this.btnCancel,
            this.toolStripSeparator1,
            this.btnOpen,
            this.btnSave,
            this.btnUndo,
            this.btnRedo,
            this.cbxZoom,
            this.sep1,
            this.cbxFontName,
            this.cbxFontSize,
            this.btnBold,
            this.btnItalic,
            this.btnUnderline,
            this.sep2,
            this.btnAlignLeft,
            this.btnAlignCenter,
            this.btnAlignRight,
            this.btnAlignJustify,
            this.sep3,
            this.btnColor,
            this.btnSubscript,
            this.btnSuperscript,
            this.btnBullets});
      this.ts1.Location = new System.Drawing.Point(0, 0);
      this.ts1.Name = "ts1";
      this.ts1.Size = new System.Drawing.Size(764, 25);
      this.ts1.TabIndex = 0;
      this.ts1.Text = "toolStrip1";
      // 
      // btnOk
      // 
      this.btnOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
      this.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(23, 22);
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(23, 22);
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnOpen
      // 
      this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(23, 22);
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // btnSave
      // 
      this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
      this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(23, 22);
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnUndo
      // 
      this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
      this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUndo.Name = "btnUndo";
      this.btnUndo.Size = new System.Drawing.Size(23, 22);
      this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
      // 
      // btnRedo
      // 
      this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
      this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRedo.Name = "btnRedo";
      this.btnRedo.Size = new System.Drawing.Size(23, 22);
      this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
      // 
      // cbxZoom
      // 
      this.cbxZoom.AutoSize = false;
      this.cbxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxZoom.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "200%",
            "500%"});
      this.cbxZoom.Name = "cbxZoom";
      this.cbxZoom.Size = new System.Drawing.Size(60, 21);
      this.cbxZoom.SelectedIndexChanged += new System.EventHandler(this.cbxZoom_SelectedIndexChanged);
      // 
      // sep1
      // 
      this.sep1.Name = "sep1";
      this.sep1.Size = new System.Drawing.Size(6, 25);
      // 
      // cbxFontName
      // 
      this.cbxFontName.AutoSize = false;
      this.cbxFontName.DropDownHeight = 302;
      this.cbxFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxFontName.DropDownWidth = 270;
      this.cbxFontName.IntegralHeight = false;
      this.cbxFontName.Name = "cbxFontName";
      this.cbxFontName.Size = new System.Drawing.Size(131, 25);
      this.cbxFontName.FontSelected += new System.EventHandler(this.cbxFontName_FontSelected);
      // 
      // cbxFontSize
      // 
      this.cbxFontSize.AutoSize = false;
      this.cbxFontSize.Name = "cbxFontSize";
      this.cbxFontSize.Size = new System.Drawing.Size(40, 25);
      this.cbxFontSize.Text = "0";
      this.cbxFontSize.SizeSelected += new System.EventHandler(this.cbxFontSize_SizeSelected);
      // 
      // btnBold
      // 
      this.btnBold.CheckOnClick = true;
      this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnBold.Image = ((System.Drawing.Image)(resources.GetObject("btnBold.Image")));
      this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnBold.Name = "btnBold";
      this.btnBold.Size = new System.Drawing.Size(23, 22);
      this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
      // 
      // btnItalic
      // 
      this.btnItalic.CheckOnClick = true;
      this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnItalic.Image = ((System.Drawing.Image)(resources.GetObject("btnItalic.Image")));
      this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnItalic.Name = "btnItalic";
      this.btnItalic.Size = new System.Drawing.Size(23, 22);
      this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
      // 
      // btnUnderline
      // 
      this.btnUnderline.CheckOnClick = true;
      this.btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("btnUnderline.Image")));
      this.btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUnderline.Name = "btnUnderline";
      this.btnUnderline.Size = new System.Drawing.Size(23, 22);
      this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
      // 
      // sep2
      // 
      this.sep2.Name = "sep2";
      this.sep2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnAlignLeft
      // 
      this.btnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignLeft.Image")));
      this.btnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignLeft.Name = "btnAlignLeft";
      this.btnAlignLeft.Size = new System.Drawing.Size(23, 22);
      this.btnAlignLeft.Click += new System.EventHandler(this.btnAlignLeft_Click);
      // 
      // btnAlignCenter
      // 
      this.btnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignCenter.Image")));
      this.btnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignCenter.Name = "btnAlignCenter";
      this.btnAlignCenter.Size = new System.Drawing.Size(23, 22);
      this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);
      // 
      // btnAlignRight
      // 
      this.btnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignRight.Image")));
      this.btnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignRight.Name = "btnAlignRight";
      this.btnAlignRight.Size = new System.Drawing.Size(23, 22);
      this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);
      // 
      // btnAlignJustify
      // 
      this.btnAlignJustify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignJustify.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignJustify.Image")));
      this.btnAlignJustify.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignJustify.Name = "btnAlignJustify";
      this.btnAlignJustify.Size = new System.Drawing.Size(23, 22);
      this.btnAlignJustify.Click += new System.EventHandler(this.btnAlignJustify_Click);
      // 
      // sep3
      // 
      this.sep3.Name = "sep3";
      this.sep3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnColor
      // 
      this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
      this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnColor.Name = "btnColor";
      this.btnColor.Size = new System.Drawing.Size(32, 22);
      this.btnColor.Text = "toolStripColorButton1";
      this.btnColor.ButtonClick += new System.EventHandler(this.btnColor_ButtonClick);
      // 
      // btnSubscript
      // 
      this.btnSubscript.CheckOnClick = true;
      this.btnSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSubscript.Image")));
      this.btnSubscript.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSubscript.Name = "btnSubscript";
      this.btnSubscript.Size = new System.Drawing.Size(23, 22);
      this.btnSubscript.Click += new System.EventHandler(this.btnSubscript_Click);
      // 
      // btnSuperscript
      // 
      this.btnSuperscript.CheckOnClick = true;
      this.btnSuperscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSuperscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSuperscript.Image")));
      this.btnSuperscript.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSuperscript.Name = "btnSuperscript";
      this.btnSuperscript.Size = new System.Drawing.Size(23, 22);
      this.btnSuperscript.Click += new System.EventHandler(this.btnSuperscript_Click);
      // 
      // btnBullets
      // 
      this.btnBullets.CheckOnClick = true;
      this.btnBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnBullets.Image = ((System.Drawing.Image)(resources.GetObject("btnBullets.Image")));
      this.btnBullets.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnBullets.Name = "btnBullets";
      this.btnBullets.Size = new System.Drawing.Size(23, 22);
      this.btnBullets.Click += new System.EventHandler(this.btnBullets_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 25);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.rtbText);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tvData);
      this.splitContainer1.Panel2.Controls.Add(this.expandableSplitter1);
      this.splitContainer1.Panel2.Controls.Add(this.lblDescription);
      this.splitContainer1.Size = new System.Drawing.Size(764, 284);
      this.splitContainer1.SplitterDistance = 591;
      this.splitContainer1.TabIndex = 2;
      this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
      // 
      // rtbText
      // 
      this.rtbText.AcceptsTab = true;
      this.rtbText.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtbText.BulletIndent = 4;
      this.rtbText.DetectUrls = false;
      this.rtbText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbText.HideSelection = false;
      this.rtbText.Location = new System.Drawing.Point(0, 0);
      this.rtbText.Name = "rtbText";
      this.rtbText.Size = new System.Drawing.Size(591, 284);
      this.rtbText.TabIndex = 1;
      this.rtbText.Text = "";
      this.rtbText.SelectionChanged += new System.EventHandler(this.rtbText_SelectionChanged);
      // 
      // tvData
      // 
      this.tvData.AllowDrop = true;
      this.tvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvData.ExpandedNodes = ((System.Collections.Generic.List<string>)(resources.GetObject("tvData.ExpandedNodes")));
      this.tvData.ImageIndex = 0;
      this.tvData.Location = new System.Drawing.Point(0, 0);
      this.tvData.Name = "tvData";
      this.tvData.SelectedImageIndex = 0;
      this.tvData.ShowColumns = true;
      this.tvData.ShowDataSources = true;
      this.tvData.ShowDialogs = true;
      this.tvData.ShowEnabledOnly = true;
      this.tvData.ShowFunctions = true;
      this.tvData.ShowNone = false;
      this.tvData.ShowParameters = true;
      this.tvData.ShowRelations = true;
      this.tvData.ShowTotals = true;
      this.tvData.ShowVariables = true;
      this.tvData.Size = new System.Drawing.Size(169, 205);
      this.tvData.TabIndex = 1;
      this.tvData.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvData_NodeMouseDoubleClick);
      this.tvData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvData_AfterSelect);
      this.tvData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvData_ItemDrag);
      // 
      // expandableSplitter1
      // 
      this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.expandableSplitter1.Location = new System.Drawing.Point(0, 205);
      this.expandableSplitter1.Name = "expandableSplitter1";
      this.expandableSplitter1.Size = new System.Drawing.Size(169, 3);
      this.expandableSplitter1.TabIndex = 3;
      this.expandableSplitter1.TabStop = false;
      this.expandableSplitter1.Visible = false;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoScroll = true;
      this.lblDescription.BackColor = System.Drawing.SystemColors.Window;
      this.lblDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblDescription.Location = new System.Drawing.Point(0, 208);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(169, 76);
      this.lblDescription.TabIndex = 2;
      this.lblDescription.Visible = false;
      // 
      // RichEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(764, 309);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.ts1);
      this.Font = new System.Drawing.Font("Tahoma", 8F);
      this.MinimizeBox = false;
      this.Name = "RichEditorForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "RichText Editor";
      this.Shown += new System.EventHandler(this.RichEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RichEditorForm_FormClosed);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RichEditorForm_FormClosing);
      this.ts1.ResumeLayout(false);
      this.ts1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip ts1;
    private FastReport.Controls.FRRichTextBox rtbText;
    private System.Windows.Forms.ToolStripButton btnOpen;
    private System.Windows.Forms.ToolStripButton btnSave;
    private System.Windows.Forms.ToolStripSeparator sep1;
    private System.Windows.Forms.ToolStripButton btnUndo;
    private System.Windows.Forms.ToolStripButton btnRedo;
    private FastReport.Controls.ToolStripFontComboBox cbxFontName;
    private FastReport.Controls.ToolStripFontSizeComboBox cbxFontSize;
    private System.Windows.Forms.ToolStripButton btnBold;
    private System.Windows.Forms.ToolStripButton btnItalic;
    private System.Windows.Forms.ToolStripButton btnUnderline;
    private System.Windows.Forms.ToolStripSeparator sep2;
    private System.Windows.Forms.ToolStripButton btnAlignLeft;
    private System.Windows.Forms.ToolStripButton btnAlignCenter;
    private System.Windows.Forms.ToolStripButton btnAlignRight;
    private System.Windows.Forms.ToolStripButton btnAlignJustify;
    private System.Windows.Forms.ToolStripSeparator sep3;
    private System.Windows.Forms.ToolStripButton btnSubscript;
    private System.Windows.Forms.ToolStripButton btnSuperscript;
    private System.Windows.Forms.ToolStripButton btnBullets;
    private FastReport.Controls.ToolStripColorButton btnColor;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private FastReport.Controls.DataTreeView tvData;
    private System.Windows.Forms.ToolStripButton btnOk;
    private System.Windows.Forms.ToolStripButton btnCancel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.Splitter expandableSplitter1;
    private FastReport.Controls.DescriptionControl lblDescription;
    private System.Windows.Forms.ToolStripComboBox cbxZoom;
  }
}