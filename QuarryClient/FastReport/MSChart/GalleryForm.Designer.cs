namespace FastReport.MSChart
{
  partial class GalleryForm
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
      this.cbNewArea = new System.Windows.Forms.CheckBox();
      this.pcPages = new FastReport.Controls.PageControl();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Enabled = false;
      this.btnOk.Location = new System.Drawing.Point(308, 412);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(388, 412);
      // 
      // cbNewArea
      // 
      this.cbNewArea.AutoSize = true;
      this.cbNewArea.Location = new System.Drawing.Point(12, 416);
      this.cbNewArea.Name = "cbNewArea";
      this.cbNewArea.Size = new System.Drawing.Size(198, 17);
      this.cbNewArea.TabIndex = 4;
      this.cbNewArea.Text = "Add new series with own chart area";
      this.cbNewArea.UseVisualStyleBackColor = true;
      // 
      // pcPages
      // 
      this.pcPages.Location = new System.Drawing.Point(12, 12);
      this.pcPages.Name = "pcPages";
      this.pcPages.SelectorWidth = 140;
      this.pcPages.Size = new System.Drawing.Size(452, 388);
      this.pcPages.TabIndex = 5;
      this.pcPages.Text = "pageControl1";
      // 
      // GalleryForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(475, 447);
      this.Controls.Add(this.pcPages);
      this.Controls.Add(this.cbNewArea);
      this.Name = "GalleryForm";
      this.Text = "Gallery";
      this.Shown += new System.EventHandler(this.GalleryForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GalleryForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.cbNewArea, 0);
      this.Controls.SetChildIndex(this.pcPages, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox cbNewArea;
    private FastReport.Controls.PageControl pcPages;
  }
}
