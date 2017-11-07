namespace FastReport.Forms
{
  partial class AddNewItemForm
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
      this.lvWizards = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(248, 216);
      this.btnOk.Text = "Add";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(328, 216);
      // 
      // lvWizards
      // 
      this.lvWizards.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.lvWizards.HideSelection = false;
      this.lvWizards.Location = new System.Drawing.Point(8, 8);
      this.lvWizards.MultiSelect = false;
      this.lvWizards.Name = "lvWizards";
      this.lvWizards.Size = new System.Drawing.Size(396, 196);
      this.lvWizards.TabIndex = 1;
      this.lvWizards.UseCompatibleStateImageBehavior = false;
      this.lvWizards.View = System.Windows.Forms.View.SmallIcon;
      this.lvWizards.SelectedIndexChanged += new System.EventHandler(this.lvWizards_SelectedIndexChanged);
      this.lvWizards.DoubleClick += new System.EventHandler(this.lvWizards_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 180;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Width = 180;
      // 
      // AddNewItemForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(412, 250);
      this.Controls.Add(this.lvWizards);
      this.Name = "AddNewItemForm";
      this.Text = "Add New Item";
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.lvWizards, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvWizards;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
  }
}
