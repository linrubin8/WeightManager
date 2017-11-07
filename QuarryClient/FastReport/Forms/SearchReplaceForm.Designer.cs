namespace FastReport.Forms
{
  partial class SearchReplaceForm
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
      this.cbxReplace = new System.Windows.Forms.ComboBox();
      this.lblReplace = new System.Windows.Forms.Label();
      this.btnReplaceAll = new System.Windows.Forms.Button();
      this.btnReplace = new System.Windows.Forms.Button();
      this.pnFind = new System.Windows.Forms.Panel();
      this.pnReplace = new System.Windows.Forms.Panel();
      this.pnOptions = new System.Windows.Forms.Panel();
      this.btnFindNext = new System.Windows.Forms.Button();
      this.pnFindButton = new System.Windows.Forms.Panel();
      this.pnReplaceButton = new System.Windows.Forms.Panel();
      this.gbOptions.SuspendLayout();
      this.pnFind.SuspendLayout();
      this.pnReplace.SuspendLayout();
      this.pnOptions.SuspendLayout();
      this.pnFindButton.SuspendLayout();
      this.pnReplaceButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(8, 228);
      this.btnOk.Size = new System.Drawing.Size(96, 23);
      this.btnOk.Visible = false;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(112, 228);
      this.btnCancel.Size = new System.Drawing.Size(96, 23);
      this.btnCancel.Visible = false;
      // 
      // lblFind
      // 
      this.lblFind.AutoSize = true;
      this.lblFind.Location = new System.Drawing.Point(8, 8);
      this.lblFind.Name = "lblFind";
      this.lblFind.Size = new System.Drawing.Size(58, 13);
      this.lblFind.TabIndex = 4;
      this.lblFind.Text = "Find what:";
      // 
      // cbxFind
      // 
      this.cbxFind.FormattingEnabled = true;
      this.cbxFind.Location = new System.Drawing.Point(8, 24);
      this.cbxFind.Name = "cbxFind";
      this.cbxFind.Size = new System.Drawing.Size(200, 21);
      this.cbxFind.TabIndex = 2;
      this.cbxFind.TextChanged += new System.EventHandler(this.cbxFind_TextChanged);
      // 
      // gbOptions
      // 
      this.gbOptions.Controls.Add(this.cbMatchWholeWord);
      this.gbOptions.Controls.Add(this.cbMatchCase);
      this.gbOptions.Location = new System.Drawing.Point(8, 8);
      this.gbOptions.Name = "gbOptions";
      this.gbOptions.Size = new System.Drawing.Size(200, 72);
      this.gbOptions.TabIndex = 3;
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
      // cbxReplace
      // 
      this.cbxReplace.FormattingEnabled = true;
      this.cbxReplace.Location = new System.Drawing.Point(8, 24);
      this.cbxReplace.Name = "cbxReplace";
      this.cbxReplace.Size = new System.Drawing.Size(200, 21);
      this.cbxReplace.TabIndex = 2;
      // 
      // lblReplace
      // 
      this.lblReplace.AutoSize = true;
      this.lblReplace.Location = new System.Drawing.Point(8, 8);
      this.lblReplace.Name = "lblReplace";
      this.lblReplace.Size = new System.Drawing.Size(72, 13);
      this.lblReplace.TabIndex = 4;
      this.lblReplace.Text = "Replace with:";
      // 
      // btnReplaceAll
      // 
      this.btnReplaceAll.Location = new System.Drawing.Point(112, 0);
      this.btnReplaceAll.Name = "btnReplaceAll";
      this.btnReplaceAll.Size = new System.Drawing.Size(96, 23);
      this.btnReplaceAll.TabIndex = 5;
      this.btnReplaceAll.Text = "Replace all";
      this.btnReplaceAll.UseVisualStyleBackColor = true;
      this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
      // 
      // btnReplace
      // 
      this.btnReplace.Location = new System.Drawing.Point(8, 0);
      this.btnReplace.Name = "btnReplace";
      this.btnReplace.Size = new System.Drawing.Size(96, 23);
      this.btnReplace.TabIndex = 5;
      this.btnReplace.Text = "Replace";
      this.btnReplace.UseVisualStyleBackColor = true;
      this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
      // 
      // pnFind
      // 
      this.pnFind.Controls.Add(this.lblFind);
      this.pnFind.Controls.Add(this.cbxFind);
      this.pnFind.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnFind.Location = new System.Drawing.Point(0, 0);
      this.pnFind.Name = "pnFind";
      this.pnFind.Size = new System.Drawing.Size(216, 48);
      this.pnFind.TabIndex = 6;
      // 
      // pnReplace
      // 
      this.pnReplace.Controls.Add(this.lblReplace);
      this.pnReplace.Controls.Add(this.cbxReplace);
      this.pnReplace.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnReplace.Location = new System.Drawing.Point(0, 48);
      this.pnReplace.Name = "pnReplace";
      this.pnReplace.Size = new System.Drawing.Size(216, 48);
      this.pnReplace.TabIndex = 7;
      // 
      // pnOptions
      // 
      this.pnOptions.Controls.Add(this.gbOptions);
      this.pnOptions.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnOptions.Location = new System.Drawing.Point(0, 96);
      this.pnOptions.Name = "pnOptions";
      this.pnOptions.Size = new System.Drawing.Size(216, 92);
      this.pnOptions.TabIndex = 8;
      // 
      // btnFindNext
      // 
      this.btnFindNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnFindNext.Location = new System.Drawing.Point(112, 0);
      this.btnFindNext.Name = "btnFindNext";
      this.btnFindNext.Size = new System.Drawing.Size(96, 23);
      this.btnFindNext.TabIndex = 5;
      this.btnFindNext.Text = "Find next";
      this.btnFindNext.UseVisualStyleBackColor = true;
      this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
      // 
      // pnFindButton
      // 
      this.pnFindButton.Controls.Add(this.btnFindNext);
      this.pnFindButton.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnFindButton.Location = new System.Drawing.Point(0, 188);
      this.pnFindButton.Name = "pnFindButton";
      this.pnFindButton.Size = new System.Drawing.Size(216, 28);
      this.pnFindButton.TabIndex = 9;
      // 
      // pnReplaceButton
      // 
      this.pnReplaceButton.Controls.Add(this.btnReplace);
      this.pnReplaceButton.Controls.Add(this.btnReplaceAll);
      this.pnReplaceButton.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnReplaceButton.Location = new System.Drawing.Point(0, 216);
      this.pnReplaceButton.Name = "pnReplaceButton";
      this.pnReplaceButton.Size = new System.Drawing.Size(216, 28);
      this.pnReplaceButton.TabIndex = 10;
      // 
      // SearchReplaceForm
      // 
      this.AcceptButton = this.btnFindNext;
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(216, 244);
      this.Controls.Add(this.pnReplaceButton);
      this.Controls.Add(this.pnFindButton);
      this.Controls.Add(this.pnOptions);
      this.Controls.Add(this.pnReplace);
      this.Controls.Add(this.pnFind);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.KeyPreview = true;
      this.Name = "SearchReplaceForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Find";
      this.TopMost = true;
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchReplaceForm_FormClosed);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchReplaceForm_KeyDown);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pnFind, 0);
      this.Controls.SetChildIndex(this.pnReplace, 0);
      this.Controls.SetChildIndex(this.pnOptions, 0);
      this.Controls.SetChildIndex(this.pnFindButton, 0);
      this.Controls.SetChildIndex(this.pnReplaceButton, 0);
      this.gbOptions.ResumeLayout(false);
      this.gbOptions.PerformLayout();
      this.pnFind.ResumeLayout(false);
      this.pnFind.PerformLayout();
      this.pnReplace.ResumeLayout(false);
      this.pnReplace.PerformLayout();
      this.pnOptions.ResumeLayout(false);
      this.pnFindButton.ResumeLayout(false);
      this.pnReplaceButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblFind;
    private System.Windows.Forms.ComboBox cbxFind;
    private System.Windows.Forms.GroupBox gbOptions;
    private System.Windows.Forms.CheckBox cbMatchWholeWord;
    private System.Windows.Forms.CheckBox cbMatchCase;
    private System.Windows.Forms.ComboBox cbxReplace;
    private System.Windows.Forms.Label lblReplace;
    private System.Windows.Forms.Button btnReplaceAll;
    private System.Windows.Forms.Button btnReplace;
    private System.Windows.Forms.Panel pnFind;
    private System.Windows.Forms.Panel pnReplace;
    private System.Windows.Forms.Panel pnOptions;
    private System.Windows.Forms.Button btnFindNext;
    private System.Windows.Forms.Panel pnFindButton;
    private System.Windows.Forms.Panel pnReplaceButton;
  }
}
