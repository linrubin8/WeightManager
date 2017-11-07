namespace FastReport.Forms
{
  partial class StringCollectionEditorForm
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
      this.tbLines = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(251, 252);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(331, 252);
      // 
      // tbLines
      // 
      this.tbLines.AcceptsReturn = true;
      this.tbLines.AcceptsTab = true;
      this.tbLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbLines.Location = new System.Drawing.Point(8, 8);
      this.tbLines.Multiline = true;
      this.tbLines.Name = "tbLines";
      this.tbLines.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbLines.Size = new System.Drawing.Size(399, 232);
      this.tbLines.TabIndex = 0;
      // 
      // StringCollectionEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(415, 286);
      this.Controls.Add(this.tbLines);
      this.Name = "StringCollectionEditorForm";
      this.Shown += new System.EventHandler(this.StringCollectionEditorForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StringCollectionEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.tbLines, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox tbLines;

  }
}
