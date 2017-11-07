namespace FastReport.Forms
{
  partial class PictureEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureEditorForm));
            this.pbPicture = new System.Windows.Forms.PictureBox();
            this.tvData = new FastReport.Controls.DataTreeView();
            this.tbFileName = new FastReport.Controls.TextBoxButton();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.pcPages = new FastReport.Controls.PageControl();
            this.pnPicture = new FastReport.Controls.PageControlPage();
            this.lblSize = new System.Windows.Forms.Label();
            this.ts1 = new System.Windows.Forms.ToolStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.pnDataColumn = new FastReport.Controls.PageControlPage();
            this.pnFileName = new FastReport.Controls.PageControlPage();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.pnUrl = new FastReport.Controls.PageControlPage();
            this.lblUrl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
            this.pcPages.SuspendLayout();
            this.pnPicture.SuspendLayout();
            this.ts1.SuspendLayout();
            this.pnDataColumn.SuspendLayout();
            this.pnFileName.SuspendLayout();
            this.pnUrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(359, 308);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(439, 308);
            // 
            // pbPicture
            // 
            this.pbPicture.BackColor = System.Drawing.SystemColors.Window;
            this.pbPicture.Location = new System.Drawing.Point(16, 44);
            this.pbPicture.Name = "pbPicture";
            this.pbPicture.Size = new System.Drawing.Size(347, 220);
            this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPicture.TabIndex = 0;
            this.pbPicture.TabStop = false;
            // 
            // tvData
            // 
            this.tvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvData.ExpandedNodes = ((System.Collections.Generic.List<string>)(resources.GetObject("tvData.ExpandedNodes")));
            this.tvData.HideSelection = false;
            this.tvData.ImageIndex = 0;
            this.tvData.Location = new System.Drawing.Point(4, 4);
            this.tvData.Name = "tvData";
            this.tvData.SelectedImageIndex = 0;
            this.tvData.ShowColumns = true;
            this.tvData.ShowDataSources = true;
            this.tvData.ShowDialogs = false;
            this.tvData.ShowEnabledOnly = true;
            this.tvData.ShowFunctions = false;
            this.tvData.ShowNone = true;
            this.tvData.ShowParameters = false;
            this.tvData.ShowRelations = true;
            this.tvData.ShowTotals = false;
            this.tvData.ShowVariables = false;
            this.tvData.Size = new System.Drawing.Size(372, 274);
            this.tvData.TabIndex = 0;
            // 
            // tbFileName
            // 
            this.tbFileName.Image = null;
            this.tbFileName.Location = new System.Drawing.Point(16, 36);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(347, 21);
            this.tbFileName.TabIndex = 0;
            this.tbFileName.ButtonClick += new System.EventHandler(this.tbFileName_ButtonClick);
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(16, 36);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(347, 20);
            this.tbUrl.TabIndex = 0;
            // 
            // pcPages
            // 
            this.pcPages.Controls.Add(this.pnPicture);
            this.pcPages.Controls.Add(this.pnDataColumn);
            this.pcPages.Controls.Add(this.pnFileName);
            this.pcPages.Controls.Add(this.pnUrl);
            this.pcPages.HighlightPageIndex = -1;
            this.pcPages.Location = new System.Drawing.Point(12, 12);
            this.pcPages.Name = "pcPages";
            this.pcPages.SelectorWidth = 120;
            this.pcPages.Size = new System.Drawing.Size(501, 284);
            this.pcPages.TabIndex = 3;
            this.pcPages.Text = "pageControl1";
            // 
            // pnPicture
            // 
            this.pnPicture.BackColor = System.Drawing.SystemColors.Window;
            this.pnPicture.Controls.Add(this.lblSize);
            this.pnPicture.Controls.Add(this.pbPicture);
            this.pnPicture.Controls.Add(this.ts1);
            this.pnPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPicture.Location = new System.Drawing.Point(120, 1);
            this.pnPicture.Name = "pnPicture";
            this.pnPicture.Padding = new System.Windows.Forms.Padding(4);
            this.pnPicture.Size = new System.Drawing.Size(380, 282);
            this.pnPicture.TabIndex = 0;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(16, 252);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(26, 13);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Size";
            // 
            // ts1
            // 
            this.ts1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnPaste,
            this.btnClear,
            this.btnEdit});
            this.ts1.Location = new System.Drawing.Point(4, 4);
            this.ts1.Name = "ts1";
            this.ts1.ShowItemToolTips = false;
            this.ts1.Size = new System.Drawing.Size(372, 25);
            this.ts1.TabIndex = 3;
            this.ts1.Text = "toolStrip1";
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(53, 22);
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(55, 22);
            this.btnPaste.Text = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pnDataColumn
            // 
            this.pnDataColumn.BackColor = System.Drawing.SystemColors.Window;
            this.pnDataColumn.Controls.Add(this.tvData);
            this.pnDataColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDataColumn.Location = new System.Drawing.Point(120, 1);
            this.pnDataColumn.Name = "pnDataColumn";
            this.pnDataColumn.Padding = new System.Windows.Forms.Padding(4);
            this.pnDataColumn.Size = new System.Drawing.Size(380, 282);
            this.pnDataColumn.TabIndex = 1;
            // 
            // pnFileName
            // 
            this.pnFileName.BackColor = System.Drawing.SystemColors.Window;
            this.pnFileName.Controls.Add(this.lblNote);
            this.pnFileName.Controls.Add(this.lblFile);
            this.pnFileName.Controls.Add(this.tbFileName);
            this.pnFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnFileName.Location = new System.Drawing.Point(120, 1);
            this.pnFileName.Name = "pnFileName";
            this.pnFileName.Size = new System.Drawing.Size(380, 282);
            this.pnFileName.TabIndex = 2;
            // 
            // lblNote
            // 
            this.lblNote.Location = new System.Drawing.Point(16, 72);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(347, 68);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "Note: you have to deploy this file together with your report.";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(16, 16);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(56, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "File name:";
            // 
            // pnUrl
            // 
            this.pnUrl.BackColor = System.Drawing.SystemColors.Window;
            this.pnUrl.Controls.Add(this.lblUrl);
            this.pnUrl.Controls.Add(this.tbUrl);
            this.pnUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnUrl.Location = new System.Drawing.Point(120, 1);
            this.pnUrl.Name = "pnUrl";
            this.pnUrl.Size = new System.Drawing.Size(380, 282);
            this.pnUrl.TabIndex = 3;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(16, 16);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(30, 13);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "URL:";
            // 
            // PictureEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(524, 339);
            this.Controls.Add(this.pcPages);
            this.Name = "PictureEditorForm";
            this.Text = "Edit Picture";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PictureEditorForm_FormClosed);
            this.Shown += new System.EventHandler(this.PictureEditorForm_Shown);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.pcPages, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
            this.pcPages.ResumeLayout(false);
            this.pnPicture.ResumeLayout(false);
            this.pnPicture.PerformLayout();
            this.ts1.ResumeLayout(false);
            this.ts1.PerformLayout();
            this.pnDataColumn.ResumeLayout(false);
            this.pnFileName.ResumeLayout(false);
            this.pnFileName.PerformLayout();
            this.pnUrl.ResumeLayout(false);
            this.pnUrl.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pbPicture;
    private FastReport.Controls.DataTreeView tvData;
    private FastReport.Controls.TextBoxButton tbFileName;
    private System.Windows.Forms.TextBox tbUrl;
    private FastReport.Controls.PageControl pcPages;
    private FastReport.Controls.PageControlPage pnPicture;
    private FastReport.Controls.PageControlPage pnDataColumn;
    private FastReport.Controls.PageControlPage pnFileName;
    private FastReport.Controls.PageControlPage pnUrl;
    private System.Windows.Forms.Label lblNote;
    private System.Windows.Forms.Label lblFile;
    private System.Windows.Forms.Label lblUrl;
    private System.Windows.Forms.ToolStrip ts1;
    private System.Windows.Forms.ToolStripButton btnLoad;
    private System.Windows.Forms.ToolStripButton btnPaste;
    private System.Windows.Forms.ToolStripButton btnClear;
    private System.Windows.Forms.Label lblSize;
    private System.Windows.Forms.ToolStripButton btnEdit;
  }
}
