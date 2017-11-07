namespace FastReport.Forms
{
  partial class RelationEditorForm
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
      this.lblParentTable = new System.Windows.Forms.Label();
      this.cbxParent = new System.Windows.Forms.ComboBox();
      this.lblChildTable = new System.Windows.Forms.Label();
      this.cbxChild = new System.Windows.Forms.ComboBox();
      this.lblColumns = new System.Windows.Forms.Label();
      this.gvColumns = new System.Windows.Forms.DataGridView();
      this.clParent = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.clChild = new System.Windows.Forms.DataGridViewComboBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(204, 212);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(284, 212);
      // 
      // lblParentTable
      // 
      this.lblParentTable.AutoSize = true;
      this.lblParentTable.Location = new System.Drawing.Point(12, 12);
      this.lblParentTable.Name = "lblParentTable";
      this.lblParentTable.Size = new System.Drawing.Size(70, 13);
      this.lblParentTable.TabIndex = 3;
      this.lblParentTable.Text = "Parent table:";
      // 
      // cbxParent
      // 
      this.cbxParent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxParent.FormattingEnabled = true;
      this.cbxParent.Location = new System.Drawing.Point(12, 32);
      this.cbxParent.Name = "cbxParent";
      this.cbxParent.Size = new System.Drawing.Size(168, 21);
      this.cbxParent.TabIndex = 4;
      this.cbxParent.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxParent_DrawItem);
      this.cbxParent.SelectedIndexChanged += new System.EventHandler(this.cbxParent_SelectedIndexChanged);
      // 
      // lblChildTable
      // 
      this.lblChildTable.AutoSize = true;
      this.lblChildTable.Location = new System.Drawing.Point(192, 12);
      this.lblChildTable.Name = "lblChildTable";
      this.lblChildTable.Size = new System.Drawing.Size(61, 13);
      this.lblChildTable.TabIndex = 3;
      this.lblChildTable.Text = "Child table:";
      // 
      // cbxChild
      // 
      this.cbxChild.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxChild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxChild.FormattingEnabled = true;
      this.cbxChild.Location = new System.Drawing.Point(192, 32);
      this.cbxChild.Name = "cbxChild";
      this.cbxChild.Size = new System.Drawing.Size(168, 21);
      this.cbxChild.TabIndex = 4;
      this.cbxChild.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxParent_DrawItem);
      this.cbxChild.SelectedIndexChanged += new System.EventHandler(this.cbxChild_SelectedIndexChanged);
      // 
      // lblColumns
      // 
      this.lblColumns.AutoSize = true;
      this.lblColumns.Location = new System.Drawing.Point(12, 68);
      this.lblColumns.Name = "lblColumns";
      this.lblColumns.Size = new System.Drawing.Size(47, 13);
      this.lblColumns.TabIndex = 5;
      this.lblColumns.Text = "Columns";
      // 
      // gvColumns
      // 
      this.gvColumns.AllowUserToResizeRows = false;
      this.gvColumns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.gvColumns.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
      this.gvColumns.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.gvColumns.ColumnHeadersHeight = 20;
      this.gvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clParent,
            this.clChild});
      this.gvColumns.Location = new System.Drawing.Point(12, 88);
      this.gvColumns.Name = "gvColumns";
      this.gvColumns.RowHeadersVisible = false;
      this.gvColumns.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.gvColumns.Size = new System.Drawing.Size(348, 112);
      this.gvColumns.TabIndex = 6;
      // 
      // clParent
      // 
      this.clParent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.clParent.DisplayStyleForCurrentCellOnly = true;
      this.clParent.HeaderText = "Parent";
      this.clParent.Name = "clParent";
      this.clParent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // clChild
      // 
      this.clChild.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.clChild.DisplayStyleForCurrentCellOnly = true;
      this.clChild.HeaderText = "Child";
      this.clChild.Name = "clChild";
      this.clChild.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // RelationEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(372, 245);
      this.Controls.Add(this.gvColumns);
      this.Controls.Add(this.lblColumns);
      this.Controls.Add(this.cbxParent);
      this.Controls.Add(this.lblParentTable);
      this.Controls.Add(this.cbxChild);
      this.Controls.Add(this.lblChildTable);
      this.Name = "RelationEditorForm";
      this.Text = "Edit Relation";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RelationEditorForm_FormClosed);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RelationEditorForm_FormClosing);
      this.Controls.SetChildIndex(this.lblChildTable, 0);
      this.Controls.SetChildIndex(this.cbxChild, 0);
      this.Controls.SetChildIndex(this.lblParentTable, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.cbxParent, 0);
      this.Controls.SetChildIndex(this.lblColumns, 0);
      this.Controls.SetChildIndex(this.gvColumns, 0);
      ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblParentTable;
    private System.Windows.Forms.ComboBox cbxParent;
    private System.Windows.Forms.Label lblChildTable;
    private System.Windows.Forms.ComboBox cbxChild;
    private System.Windows.Forms.Label lblColumns;
    private System.Windows.Forms.DataGridView gvColumns;
    private System.Windows.Forms.DataGridViewComboBoxColumn clParent;
    private System.Windows.Forms.DataGridViewComboBoxColumn clChild;

  }
}
