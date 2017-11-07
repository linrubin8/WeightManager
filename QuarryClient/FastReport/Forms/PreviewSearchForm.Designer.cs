namespace FastReport.Forms
{
  partial class PreviewSearchForm
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
      this.lblFind = new System.Windows.Forms.Label();
      this.cbxFind = new System.Windows.Forms.ComboBox();
      this.gbOptions = new System.Windows.Forms.GroupBox();
      this.cbMatchWholeWord = new System.Windows.Forms.CheckBox();
      this.cbMatchCase = new System.Windows.Forms.CheckBox();
      this.gbOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(8, 136);
      this.btnOk.Size = new System.Drawing.Size(123, 23);
      this.btnOk.TabIndex = 2;
      this.btnOk.Text = "Find Next";
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(136, 136);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // lblFind
      // 
      this.lblFind.AutoSize = true;
      this.lblFind.Location = new System.Drawing.Point(8, 6);
      this.lblFind.Name = "lblFind";
      this.lblFind.Size = new System.Drawing.Size(58, 13);
      this.lblFind.TabIndex = 1;
      this.lblFind.Text = "Find what:";
      // 
      // cbxFind
      // 
      this.cbxFind.FormattingEnabled = true;
      this.cbxFind.Location = new System.Drawing.Point(8, 24);
      this.cbxFind.Name = "cbxFind";
      this.cbxFind.Size = new System.Drawing.Size(204, 21);
      this.cbxFind.TabIndex = 0;
      this.cbxFind.TextChanged += new System.EventHandler(this.cbxFind_TextChanged);
      // 
      // gbOptions
      // 
      this.gbOptions.Controls.Add(this.cbMatchWholeWord);
      this.gbOptions.Controls.Add(this.cbMatchCase);
      this.gbOptions.Location = new System.Drawing.Point(8, 52);
      this.gbOptions.Name = "gbOptions";
      this.gbOptions.Size = new System.Drawing.Size(204, 72);
      this.gbOptions.TabIndex = 1;
      this.gbOptions.TabStop = false;
      this.gbOptions.Text = "Find options";
      // 
      // cbMatchWholeWord
      // 
      this.cbMatchWholeWord.AutoSize = true;
      this.cbMatchWholeWord.Location = new System.Drawing.Point(12, 44);
      this.cbMatchWholeWord.Name = "cbMatchWholeWord";
      this.cbMatchWholeWord.Size = new System.Drawing.Size(113, 17);
      this.cbMatchWholeWord.TabIndex = 1;
      this.cbMatchWholeWord.Text = "Match whole word";
      this.cbMatchWholeWord.UseVisualStyleBackColor = true;
      // 
      // cbMatchCase
      // 
      this.cbMatchCase.AutoSize = true;
      this.cbMatchCase.Location = new System.Drawing.Point(12, 20);
      this.cbMatchCase.Name = "cbMatchCase";
      this.cbMatchCase.Size = new System.Drawing.Size(80, 17);
      this.cbMatchCase.TabIndex = 0;
      this.cbMatchCase.Text = "Match case";
      this.cbMatchCase.UseVisualStyleBackColor = true;
      // 
      // PreviewSearchForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(221, 170);
      this.Controls.Add(this.lblFind);
      this.Controls.Add(this.cbxFind);
      this.Controls.Add(this.gbOptions);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "PreviewSearchForm";
      this.Text = "Find";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewSearchForm_FormClosed);
      this.Controls.SetChildIndex(this.gbOptions, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.cbxFind, 0);
      this.Controls.SetChildIndex(this.lblFind, 0);
      this.gbOptions.ResumeLayout(false);
      this.gbOptions.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblFind;
    private System.Windows.Forms.ComboBox cbxFind;
    private System.Windows.Forms.GroupBox gbOptions;
    private System.Windows.Forms.CheckBox cbMatchWholeWord;
    private System.Windows.Forms.CheckBox cbMatchCase;
  }
}
