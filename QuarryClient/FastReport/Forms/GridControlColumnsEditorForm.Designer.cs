namespace FastReport.Forms
{
  partial class GridControlColumnsEditorForm
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
      this.gbColumns = new System.Windows.Forms.GroupBox();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.lvColumns = new System.Windows.Forms.ListView();
      this.gbProperties = new System.Windows.Forms.GroupBox();
      this.frPropertyGrid1 = new FastReport.Controls.FRPropertyGrid();
      this.btnAddAll = new System.Windows.Forms.Button();
      this.gbColumns.SuspendLayout();
      this.gbProperties.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(392, 319);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(472, 319);
      // 
      // gbColumns
      // 
      this.gbColumns.Controls.Add(this.btnAddAll);
      this.gbColumns.Controls.Add(this.btnDown);
      this.gbColumns.Controls.Add(this.btnUp);
      this.gbColumns.Controls.Add(this.btnRemove);
      this.gbColumns.Controls.Add(this.btnAdd);
      this.gbColumns.Controls.Add(this.lvColumns);
      this.gbColumns.Location = new System.Drawing.Point(8, 4);
      this.gbColumns.Name = "gbColumns";
      this.gbColumns.Size = new System.Drawing.Size(276, 304);
      this.gbColumns.TabIndex = 1;
      this.gbColumns.TabStop = false;
      this.gbColumns.Text = "Columns";
      // 
      // btnDown
      // 
      this.btnDown.Location = new System.Drawing.Point(172, 268);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(24, 24);
      this.btnDown.TabIndex = 4;
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(172, 240);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 24);
      this.btnUp.TabIndex = 3;
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnRemove
      // 
      this.btnRemove.Location = new System.Drawing.Point(172, 48);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(95, 23);
      this.btnRemove.TabIndex = 2;
      this.btnRemove.Text = "Remove";
      this.btnRemove.UseVisualStyleBackColor = true;
      this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(172, 20);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(95, 23);
      this.btnAdd.TabIndex = 1;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // lvColumns
      // 
      this.lvColumns.HideSelection = false;
      this.lvColumns.LabelEdit = true;
      this.lvColumns.Location = new System.Drawing.Point(12, 20);
      this.lvColumns.Name = "lvColumns";
      this.lvColumns.Size = new System.Drawing.Size(152, 272);
      this.lvColumns.TabIndex = 0;
      this.lvColumns.UseCompatibleStateImageBehavior = false;
      this.lvColumns.View = System.Windows.Forms.View.List;
      this.lvColumns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
      this.lvColumns.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvColumns_AfterLabelEdit);
      // 
      // gbProperties
      // 
      this.gbProperties.Controls.Add(this.frPropertyGrid1);
      this.gbProperties.Location = new System.Drawing.Point(292, 4);
      this.gbProperties.Name = "gbProperties";
      this.gbProperties.Size = new System.Drawing.Size(256, 304);
      this.gbProperties.TabIndex = 2;
      this.gbProperties.TabStop = false;
      this.gbProperties.Text = "Properties";
      // 
      // frPropertyGrid1
      // 
      this.frPropertyGrid1.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.frPropertyGrid1.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
      this.frPropertyGrid1.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.frPropertyGrid1.LineColor = System.Drawing.SystemColors.Control;
      this.frPropertyGrid1.Location = new System.Drawing.Point(12, 20);
      this.frPropertyGrid1.Name = "frPropertyGrid1";
      this.frPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
      this.frPropertyGrid1.Size = new System.Drawing.Size(232, 272);
      this.frPropertyGrid1.TabIndex = 0;
      this.frPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.frPropertyGrid1_PropertyValueChanged);
      // 
      // btnAddAll
      // 
      this.btnAddAll.Location = new System.Drawing.Point(172, 76);
      this.btnAddAll.Name = "btnAddAll";
      this.btnAddAll.Size = new System.Drawing.Size(95, 23);
      this.btnAddAll.TabIndex = 5;
      this.btnAddAll.Text = "Add all";
      this.btnAddAll.UseVisualStyleBackColor = true;
      this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
      // 
      // GridControlColumnsEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(556, 353);
      this.Controls.Add(this.gbProperties);
      this.Controls.Add(this.gbColumns);
      this.Name = "GridControlColumnsEditorForm";
      this.Text = "Edit Columns";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GridControlColumnsEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbColumns, 0);
      this.Controls.SetChildIndex(this.gbProperties, 0);
      this.gbColumns.ResumeLayout(false);
      this.gbProperties.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbColumns;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.ListView lvColumns;
    private System.Windows.Forms.GroupBox gbProperties;
    private FastReport.Controls.FRPropertyGrid frPropertyGrid1;
    private System.Windows.Forms.Button btnAddAll;
  }
}
