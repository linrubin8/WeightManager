namespace FastReport.Forms
{
  partial class ExpressionEditorForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressionEditorForm));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tbText = new System.Windows.Forms.TextBox();
      this.tvData = new FastReport.Controls.DataTreeView();
      this.expandableSplitter1 = new System.Windows.Forms.Splitter();
      this.lblDescription = new FastReport.Controls.DescriptionControl();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(332, 296);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(412, 296);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tbText);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tvData);
      this.splitContainer1.Panel2.Controls.Add(this.expandableSplitter1);
      this.splitContainer1.Panel2.Controls.Add(this.lblDescription);
      this.splitContainer1.Size = new System.Drawing.Size(497, 284);
      this.splitContainer1.SplitterDistance = 321;
      this.splitContainer1.TabIndex = 1;
      this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
      // 
      // tbText
      // 
      this.tbText.AcceptsReturn = true;
      this.tbText.AcceptsTab = true;
      this.tbText.AllowDrop = true;
      this.tbText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbText.HideSelection = false;
      this.tbText.Location = new System.Drawing.Point(0, 0);
      this.tbText.MaxLength = 0;
      this.tbText.Multiline = true;
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(321, 284);
      this.tbText.TabIndex = 0;
      this.tbText.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbText_DragDrop);
      this.tbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
      this.tbText.DragOver += new System.Windows.Forms.DragEventHandler(this.tbText_DragOver);
      // 
      // tvData
      // 
      this.tvData.AllowDrop = true;
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
      this.tvData.Size = new System.Drawing.Size(172, 181);
      this.tvData.TabIndex = 0;
      this.tvData.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvData_NodeMouseDoubleClick);
      this.tvData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvData_AfterSelect);
      this.tvData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvData_ItemDrag);
      // 
      // expandableSplitter1
      // 
      this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.expandableSplitter1.Location = new System.Drawing.Point(0, 181);
      this.expandableSplitter1.Name = "expandableSplitter1";
      this.expandableSplitter1.Size = new System.Drawing.Size(172, 3);
      this.expandableSplitter1.TabIndex = 2;
      this.expandableSplitter1.TabStop = false;
      this.expandableSplitter1.Visible = false;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoScroll = true;
      this.lblDescription.BackColor = System.Drawing.SystemColors.Window;
      this.lblDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblDescription.Location = new System.Drawing.Point(0, 184);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(172, 100);
      this.lblDescription.TabIndex = 1;
      this.lblDescription.Visible = false;
      // 
      // ExpressionEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(497, 328);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(360, 230);
      this.Name = "ExpressionEditorForm";
      this.ShowIcon = false;
      this.Text = "Edit Expression";
      this.Shown += new System.EventHandler(this.TextEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TextEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox tbText;
    private FastReport.Controls.DataTreeView tvData;
    private System.Windows.Forms.Splitter expandableSplitter1;
    private FastReport.Controls.DescriptionControl lblDescription;
  }
}
