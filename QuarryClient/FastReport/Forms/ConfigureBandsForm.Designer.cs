namespace FastReport.Forms
{
  partial class ConfigureBandsForm
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
      this.tvBands = new System.Windows.Forms.TreeView();
      this.mnuBands = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.sep1 = new System.Windows.Forms.ToolStripSeparator();
      this.miReportTitle = new System.Windows.Forms.ToolStripMenuItem();
      this.miReportSummary = new System.Windows.Forms.ToolStripMenuItem();
      this.miPageHeader = new System.Windows.Forms.ToolStripMenuItem();
      this.miPageFooter = new System.Windows.Forms.ToolStripMenuItem();
      this.miColumnHeader = new System.Windows.Forms.ToolStripMenuItem();
      this.miColumnFooter = new System.Windows.Forms.ToolStripMenuItem();
      this.miDataHeader = new System.Windows.Forms.ToolStripMenuItem();
      this.miData = new System.Windows.Forms.ToolStripMenuItem();
      this.miDataFooter = new System.Windows.Forms.ToolStripMenuItem();
      this.miGroup = new System.Windows.Forms.ToolStripMenuItem();
      this.miGroupFooter = new System.Windows.Forms.ToolStripMenuItem();
      this.miChild = new System.Windows.Forms.ToolStripMenuItem();
      this.miOverlay = new System.Windows.Forms.ToolStripMenuItem();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.mnuBands.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(288, 308);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(288, 280);
      this.btnCancel.Visible = false;
      // 
      // tvBands
      // 
      this.tvBands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tvBands.HideSelection = false;
      this.tvBands.Location = new System.Drawing.Point(12, 12);
      this.tvBands.Name = "tvBands";
      this.tvBands.ShowLines = false;
      this.tvBands.ShowPlusMinus = false;
      this.tvBands.ShowRootLines = false;
      this.tvBands.Size = new System.Drawing.Size(268, 320);
      this.tvBands.TabIndex = 2;
      this.tvBands.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvBands_MouseUp);
      this.tvBands.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBands_AfterSelect);
      this.tvBands.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvBands_MouseDown);
      // 
      // mnuBands
      // 
      this.mnuBands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDelete,
            this.sep1,
            this.miReportTitle,
            this.miReportSummary,
            this.miPageHeader,
            this.miPageFooter,
            this.miColumnHeader,
            this.miColumnFooter,
            this.miDataHeader,
            this.miData,
            this.miDataFooter,
            this.miGroup,
            this.miGroupFooter,
            this.miChild,
            this.miOverlay});
      this.mnuBands.Name = "mnuPopup";
      this.mnuBands.Size = new System.Drawing.Size(155, 318);
      // 
      // miDelete
      // 
      this.miDelete.Name = "miDelete";
      this.miDelete.Size = new System.Drawing.Size(154, 22);
      this.miDelete.Text = "Delete";
      this.miDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // sep1
      // 
      this.sep1.Name = "sep1";
      this.sep1.Size = new System.Drawing.Size(151, 6);
      // 
      // miReportTitle
      // 
      this.miReportTitle.Name = "miReportTitle";
      this.miReportTitle.Size = new System.Drawing.Size(154, 22);
      this.miReportTitle.Text = "Report Title";
      this.miReportTitle.Click += new System.EventHandler(this.miReportTitle_Click);
      // 
      // miReportSummary
      // 
      this.miReportSummary.Name = "miReportSummary";
      this.miReportSummary.Size = new System.Drawing.Size(154, 22);
      this.miReportSummary.Text = "Report Summary";
      this.miReportSummary.Click += new System.EventHandler(this.miReportSummary_Click);
      // 
      // miPageHeader
      // 
      this.miPageHeader.Name = "miPageHeader";
      this.miPageHeader.Size = new System.Drawing.Size(154, 22);
      this.miPageHeader.Text = "Page Header";
      this.miPageHeader.Click += new System.EventHandler(this.miPageHeader_Click);
      // 
      // miPageFooter
      // 
      this.miPageFooter.Name = "miPageFooter";
      this.miPageFooter.Size = new System.Drawing.Size(154, 22);
      this.miPageFooter.Text = "Page Footer";
      this.miPageFooter.Click += new System.EventHandler(this.miPageFooter_Click);
      // 
      // miColumnHeader
      // 
      this.miColumnHeader.Name = "miColumnHeader";
      this.miColumnHeader.Size = new System.Drawing.Size(154, 22);
      this.miColumnHeader.Text = "Column Header";
      this.miColumnHeader.Click += new System.EventHandler(this.miColumnHeader_Click);
      // 
      // miColumnFooter
      // 
      this.miColumnFooter.Name = "miColumnFooter";
      this.miColumnFooter.Size = new System.Drawing.Size(154, 22);
      this.miColumnFooter.Text = "Column Footer";
      this.miColumnFooter.Click += new System.EventHandler(this.miColumnFooter_Click);
      // 
      // miDataHeader
      // 
      this.miDataHeader.Name = "miDataHeader";
      this.miDataHeader.Size = new System.Drawing.Size(154, 22);
      this.miDataHeader.Text = "Data Header";
      this.miDataHeader.Click += new System.EventHandler(this.miDataHeader_Click);
      // 
      // miData
      // 
      this.miData.Name = "miData";
      this.miData.Size = new System.Drawing.Size(154, 22);
      this.miData.Text = "Data";
      this.miData.Click += new System.EventHandler(this.miData_Click);
      // 
      // miDataFooter
      // 
      this.miDataFooter.Name = "miDataFooter";
      this.miDataFooter.Size = new System.Drawing.Size(154, 22);
      this.miDataFooter.Text = "Data Footer";
      this.miDataFooter.Click += new System.EventHandler(this.miDataFooter_Click);
      // 
      // miGroup
      // 
      this.miGroup.Name = "miGroup";
      this.miGroup.Size = new System.Drawing.Size(154, 22);
      this.miGroup.Text = "Group";
      this.miGroup.Click += new System.EventHandler(this.miGroup_Click);
      // 
      // miGroupFooter
      // 
      this.miGroupFooter.Name = "miGroupFooter";
      this.miGroupFooter.Size = new System.Drawing.Size(154, 22);
      this.miGroupFooter.Text = "Group Footer";
      this.miGroupFooter.Click += new System.EventHandler(this.miGroupFooter_Click);
      // 
      // miChild
      // 
      this.miChild.Name = "miChild";
      this.miChild.Size = new System.Drawing.Size(154, 22);
      this.miChild.Text = "Child";
      this.miChild.Click += new System.EventHandler(this.miChild_Click);
      // 
      // miOverlay
      // 
      this.miOverlay.Name = "miOverlay";
      this.miOverlay.Size = new System.Drawing.Size(154, 22);
      this.miOverlay.Text = "Overlay";
      this.miOverlay.Click += new System.EventHandler(this.miOverlay_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(288, 12);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 20;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(288, 40);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 21;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnUp
      // 
      this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUp.Location = new System.Drawing.Point(288, 72);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 23);
      this.btnUp.TabIndex = 22;
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnDown
      // 
      this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDown.Location = new System.Drawing.Point(288, 100);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(24, 23);
      this.btnDown.TabIndex = 23;
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // ConfigureBandsForm
      // 
      this.AcceptButton = null;
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(372, 346);
      this.ControlBox = false;
      this.Controls.Add(this.btnDown);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.tvBands);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(380, 300);
      this.Name = "ConfigureBandsForm";
      this.ShowIcon = false;
      this.Text = "Configure Bands";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureBandsForm_FormClosing);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.tvBands, 0);
      this.Controls.SetChildIndex(this.btnAdd, 0);
      this.Controls.SetChildIndex(this.btnDelete, 0);
      this.Controls.SetChildIndex(this.btnUp, 0);
      this.Controls.SetChildIndex(this.btnDown, 0);
      this.mnuBands.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView tvBands;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.ContextMenuStrip mnuBands;
    private System.Windows.Forms.ToolStripMenuItem miReportTitle;
    private System.Windows.Forms.ToolStripMenuItem miPageHeader;
    private System.Windows.Forms.ToolStripMenuItem miColumnHeader;
    private System.Windows.Forms.ToolStripMenuItem miDataHeader;
    private System.Windows.Forms.ToolStripMenuItem miData;
    private System.Windows.Forms.ToolStripMenuItem miGroup;
    private System.Windows.Forms.ToolStripMenuItem miChild;
    private System.Windows.Forms.ToolStripMenuItem miOverlay;
    private System.Windows.Forms.ToolStripMenuItem miDataFooter;
    private System.Windows.Forms.ToolStripMenuItem miGroupFooter;
    private System.Windows.Forms.ToolStripMenuItem miColumnFooter;
    private System.Windows.Forms.ToolStripMenuItem miReportSummary;
    private System.Windows.Forms.ToolStripMenuItem miPageFooter;
    private System.Windows.Forms.ToolStripMenuItem miDelete;
    private System.Windows.Forms.ToolStripSeparator sep1;
  }
}
