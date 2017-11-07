namespace FastReport.Forms
{
  partial class GroupExpertForm
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
      this.lbGroups = new System.Windows.Forms.ListBox();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.gbGroupCondition = new System.Windows.Forms.GroupBox();
      this.cbxDataColumn = new FastReport.Controls.DataColumnComboBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.btnAdd = new System.Windows.Forms.Button();
      this.gbGroups = new System.Windows.Forms.GroupBox();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnEdit = new System.Windows.Forms.Button();
      this.gbGroupCondition.SuspendLayout();
      this.gbGroups.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(216, 252);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(296, 252);
      // 
      // lbGroups
      // 
      this.lbGroups.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.lbGroups.FormattingEnabled = true;
      this.lbGroups.IntegralHeight = false;
      this.lbGroups.Location = new System.Drawing.Point(12, 20);
      this.lbGroups.Name = "lbGroups";
      this.lbGroups.Size = new System.Drawing.Size(252, 112);
      this.lbGroups.TabIndex = 6;
      this.lbGroups.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbGroups_DrawItem);
      this.lbGroups.SelectedIndexChanged += new System.EventHandler(this.lbGroups_SelectedIndexChanged);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(276, 84);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 23);
      this.btnUp.TabIndex = 5;
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnDown
      // 
      this.btnDown.Location = new System.Drawing.Point(276, 108);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(24, 23);
      this.btnDown.TabIndex = 5;
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // gbGroupCondition
      // 
      this.gbGroupCondition.Controls.Add(this.btnAdd);
      this.gbGroupCondition.Controls.Add(this.cbxDataColumn);
      this.gbGroupCondition.Controls.Add(this.lblHint);
      this.gbGroupCondition.Location = new System.Drawing.Point(8, 4);
      this.gbGroupCondition.Name = "gbGroupCondition";
      this.gbGroupCondition.Size = new System.Drawing.Size(364, 88);
      this.gbGroupCondition.TabIndex = 7;
      this.gbGroupCondition.TabStop = false;
      this.gbGroupCondition.Text = "Group condition";
      // 
      // cbxDataColumn
      // 
      this.cbxDataColumn.Location = new System.Drawing.Point(12, 52);
      this.cbxDataColumn.Name = "cbxDataColumn";
      this.cbxDataColumn.Size = new System.Drawing.Size(252, 21);
      this.cbxDataColumn.TabIndex = 5;
      // 
      // lblHint
      // 
      this.lblHint.Location = new System.Drawing.Point(12, 20);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(336, 28);
      this.lblHint.TabIndex = 4;
      this.lblHint.Text = "Select a data column or type an expression, then press \"Add\" button to add a new " +
          "group.";
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(276, 52);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 3;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // gbGroups
      // 
      this.gbGroups.Controls.Add(this.btnDelete);
      this.gbGroups.Controls.Add(this.btnEdit);
      this.gbGroups.Controls.Add(this.btnDown);
      this.gbGroups.Controls.Add(this.lbGroups);
      this.gbGroups.Controls.Add(this.btnUp);
      this.gbGroups.Location = new System.Drawing.Point(8, 96);
      this.gbGroups.Name = "gbGroups";
      this.gbGroups.Size = new System.Drawing.Size(364, 144);
      this.gbGroups.TabIndex = 8;
      this.gbGroups.TabStop = false;
      this.gbGroups.Text = "Groups";
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(276, 48);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 8;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.Location = new System.Drawing.Point(276, 20);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(75, 23);
      this.btnEdit.TabIndex = 7;
      this.btnEdit.Text = "Edit...";
      this.btnEdit.UseVisualStyleBackColor = true;
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // GroupExpertForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(380, 284);
      this.Controls.Add(this.gbGroups);
      this.Controls.Add(this.gbGroupCondition);
      this.Name = "GroupExpertForm";
      this.Text = "Group Expert";
      this.Shown += new System.EventHandler(this.GroupExpertForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GroupExpertForm_FormClosed);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbGroupCondition, 0);
      this.Controls.SetChildIndex(this.gbGroups, 0);
      this.gbGroupCondition.ResumeLayout(false);
      this.gbGroups.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox lbGroups;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.GroupBox gbGroupCondition;
    private System.Windows.Forms.GroupBox gbGroups;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Label lblHint;
    private FastReport.Controls.DataColumnComboBox cbxDataColumn;
  }
}
