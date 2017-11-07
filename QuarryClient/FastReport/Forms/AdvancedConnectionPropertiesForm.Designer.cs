namespace FastReport.Forms
{
  partial class AdvancedConnectionPropertiesForm
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
      this.frPropertyGrid1 = new FastReport.Controls.FRPropertyGrid();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(132, 377);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(212, 377);
      // 
      // frPropertyGrid1
      // 
      this.frPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.frPropertyGrid1.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.frPropertyGrid1.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
      this.frPropertyGrid1.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.frPropertyGrid1.LineColor = System.Drawing.SystemColors.Control;
      this.frPropertyGrid1.Location = new System.Drawing.Point(8, 8);
      this.frPropertyGrid1.Name = "frPropertyGrid1";
      this.frPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.frPropertyGrid1.Size = new System.Drawing.Size(280, 357);
      this.frPropertyGrid1.TabIndex = 1;
      // 
      // AdvancedConnectionPropertiesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(296, 411);
      this.Controls.Add(this.frPropertyGrid1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.Name = "AdvancedConnectionPropertiesForm";
      this.ShowIcon = false;
      this.Text = "Advanced Properties";
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.frPropertyGrid1, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private FastReport.Controls.FRPropertyGrid frPropertyGrid1;
  }
}
