namespace FastReport.Forms
{
  partial class GroupBandEditorForm
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
      this.gbCondition = new System.Windows.Forms.GroupBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.cbxCondition = new FastReport.Controls.DataColumnComboBox();
      this.gbSettings = new System.Windows.Forms.GroupBox();
      this.cbxSort = new System.Windows.Forms.ComboBox();
      this.lblSort = new System.Windows.Forms.Label();
      this.gbCondition.SuspendLayout();
      this.gbSettings.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(189, 152);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(269, 152);
      // 
      // gbCondition
      // 
      this.gbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbCondition.Controls.Add(this.lblHint);
      this.gbCondition.Controls.Add(this.cbxCondition);
      this.gbCondition.Location = new System.Drawing.Point(8, 4);
      this.gbCondition.Name = "gbCondition";
      this.gbCondition.Size = new System.Drawing.Size(337, 76);
      this.gbCondition.TabIndex = 1;
      this.gbCondition.TabStop = false;
      this.gbCondition.Text = "Group condition";
      // 
      // lblHint
      // 
      this.lblHint.AutoSize = true;
      this.lblHint.Location = new System.Drawing.Point(12, 20);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(209, 13);
      this.lblHint.TabIndex = 4;
      this.lblHint.Text = "Select data column or type an expression:";
      // 
      // cbxCondition
      // 
      this.cbxCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxCondition.Location = new System.Drawing.Point(12, 40);
      this.cbxCondition.Name = "cbxCondition";
      this.cbxCondition.Size = new System.Drawing.Size(313, 21);
      this.cbxCondition.TabIndex = 3;
      // 
      // gbSettings
      // 
      this.gbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbSettings.Controls.Add(this.cbxSort);
      this.gbSettings.Controls.Add(this.lblSort);
      this.gbSettings.Location = new System.Drawing.Point(8, 84);
      this.gbSettings.Name = "gbSettings";
      this.gbSettings.Size = new System.Drawing.Size(337, 56);
      this.gbSettings.TabIndex = 2;
      this.gbSettings.TabStop = false;
      this.gbSettings.Text = "Settings";
      // 
      // cbxSort
      // 
      this.cbxSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxSort.FormattingEnabled = true;
      this.cbxSort.Location = new System.Drawing.Point(145, 20);
      this.cbxSort.Name = "cbxSort";
      this.cbxSort.Size = new System.Drawing.Size(180, 21);
      this.cbxSort.TabIndex = 1;
      // 
      // lblSort
      // 
      this.lblSort.AutoSize = true;
      this.lblSort.Location = new System.Drawing.Point(12, 24);
      this.lblSort.Name = "lblSort";
      this.lblSort.Size = new System.Drawing.Size(31, 13);
      this.lblSort.TabIndex = 0;
      this.lblSort.Text = "Sort:";
      // 
      // GroupBandEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(353, 185);
      this.Controls.Add(this.gbSettings);
      this.Controls.Add(this.gbCondition);
      this.Name = "GroupBandEditorForm";
      this.Text = "Edit Group";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GroupBandEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbCondition, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbSettings, 0);
      this.gbCondition.ResumeLayout(false);
      this.gbCondition.PerformLayout();
      this.gbSettings.ResumeLayout(false);
      this.gbSettings.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbCondition;
    private System.Windows.Forms.GroupBox gbSettings;
    private System.Windows.Forms.ComboBox cbxSort;
    private System.Windows.Forms.Label lblSort;
    private FastReport.Controls.DataColumnComboBox cbxCondition;
    private System.Windows.Forms.Label lblHint;



  }
}
