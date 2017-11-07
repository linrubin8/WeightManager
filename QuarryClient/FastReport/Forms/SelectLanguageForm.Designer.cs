namespace FastReport.Forms
{
  partial class SelectLanguageForm
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
      this.lbxLanguages = new System.Windows.Forms.ListBox();
      this.tbFolder = new FastReport.Controls.TextBoxButton();
      this.lblFolder = new System.Windows.Forms.Label();
      this.lblSelect = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(92, 332);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(172, 332);
      // 
      // lbxLanguages
      // 
      this.lbxLanguages.FormattingEnabled = true;
      this.lbxLanguages.IntegralHeight = false;
      this.lbxLanguages.Location = new System.Drawing.Point(12, 32);
      this.lbxLanguages.Name = "lbxLanguages";
      this.lbxLanguages.Size = new System.Drawing.Size(236, 228);
      this.lbxLanguages.TabIndex = 1;
      this.lbxLanguages.DoubleClick += new System.EventHandler(this.lbxLanguages_DoubleClick);
      // 
      // tbFolder
      // 
      this.tbFolder.Image = null;
      this.tbFolder.Location = new System.Drawing.Point(12, 288);
      this.tbFolder.Name = "tbFolder";
      this.tbFolder.Size = new System.Drawing.Size(236, 21);
      this.tbFolder.TabIndex = 2;
      this.tbFolder.ButtonClick += new System.EventHandler(this.tbFolder_ButtonClick);
      // 
      // lblFolder
      // 
      this.lblFolder.AutoSize = true;
      this.lblFolder.Location = new System.Drawing.Point(12, 268);
      this.lblFolder.Name = "lblFolder";
      this.lblFolder.Size = new System.Drawing.Size(133, 13);
      this.lblFolder.TabIndex = 3;
      this.lblFolder.Text = "Folder with language files:";
      // 
      // lblSelect
      // 
      this.lblSelect.AutoSize = true;
      this.lblSelect.Location = new System.Drawing.Point(12, 12);
      this.lblSelect.Name = "lblSelect";
      this.lblSelect.Size = new System.Drawing.Size(178, 13);
      this.lblSelect.TabIndex = 4;
      this.lblSelect.Text = "Select language from the list below:";
      // 
      // SelectLanguageForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(259, 369);
      this.Controls.Add(this.lblSelect);
      this.Controls.Add(this.lblFolder);
      this.Controls.Add(this.tbFolder);
      this.Controls.Add(this.lbxLanguages);
      this.Name = "SelectLanguageForm";
      this.Text = "Select Language";
      this.Shown += new System.EventHandler(this.SelectLanguageForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectLanguageForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.lbxLanguages, 0);
      this.Controls.SetChildIndex(this.tbFolder, 0);
      this.Controls.SetChildIndex(this.lblFolder, 0);
      this.Controls.SetChildIndex(this.lblSelect, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lbxLanguages;
    private FastReport.Controls.TextBoxButton tbFolder;
    private System.Windows.Forms.Label lblFolder;
    private System.Windows.Forms.Label lblSelect;
  }
}
