namespace FastReport.Forms
{
  partial class BaseExportForm
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
      this.pcPages = new FastReport.Controls.PageControl();
      this.panPages = new FastReport.Controls.PageControlPage();
      this.gbPageRange = new System.Windows.Forms.GroupBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.tbNumbers = new System.Windows.Forms.TextBox();
      this.rbNumbers = new System.Windows.Forms.RadioButton();
      this.rbCurrent = new System.Windows.Forms.RadioButton();
      this.rbAll = new System.Windows.Forms.RadioButton();
      this.cbOpenAfter = new System.Windows.Forms.CheckBox();
      this.pcPages.SuspendLayout();
      this.panPages.SuspendLayout();
      this.gbPageRange.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(152, 232);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(232, 232);
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.panPages);
      this.pcPages.Location = new System.Drawing.Point(12, 16);
      this.pcPages.Name = "pcPages";
      this.pcPages.Size = new System.Drawing.Size(284, 148);
      this.pcPages.TabIndex = 1;
      this.pcPages.Text = "pageControl1";
      // 
      // panPages
      // 
      this.panPages.Controls.Add(this.gbPageRange);
      this.panPages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panPages.Location = new System.Drawing.Point(0, 0);
      this.panPages.Name = "panPages";
      this.panPages.Size = new System.Drawing.Size(284, 148);
      this.panPages.TabIndex = 0;
      // 
      // gbPageRange
      // 
      this.gbPageRange.Controls.Add(this.lblHint);
      this.gbPageRange.Controls.Add(this.tbNumbers);
      this.gbPageRange.Controls.Add(this.rbNumbers);
      this.gbPageRange.Controls.Add(this.rbCurrent);
      this.gbPageRange.Controls.Add(this.rbAll);
      this.gbPageRange.Location = new System.Drawing.Point(8, 8);
      this.gbPageRange.Name = "gbPageRange";
      this.gbPageRange.Size = new System.Drawing.Size(260, 128);
      this.gbPageRange.TabIndex = 3;
      this.gbPageRange.TabStop = false;
      this.gbPageRange.Text = "Page range";
      // 
      // lblHint
      // 
      this.lblHint.Location = new System.Drawing.Point(12, 88);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(236, 28);
      this.lblHint.TabIndex = 4;
      this.lblHint.Text = "Enter page numbers and/or page ranges, separated by commas. For example, 1,3,5-12" +
          "";
      // 
      // tbNumbers
      // 
      this.tbNumbers.Location = new System.Drawing.Point(104, 60);
      this.tbNumbers.Name = "tbNumbers";
      this.tbNumbers.Size = new System.Drawing.Size(144, 20);
      this.tbNumbers.TabIndex = 3;
      this.tbNumbers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNumbers_KeyPress);
      // 
      // rbNumbers
      // 
      this.rbNumbers.AutoSize = true;
      this.rbNumbers.Location = new System.Drawing.Point(12, 60);
      this.rbNumbers.Name = "rbNumbers";
      this.rbNumbers.Size = new System.Drawing.Size(71, 17);
      this.rbNumbers.TabIndex = 2;
      this.rbNumbers.TabStop = true;
      this.rbNumbers.Text = "Numbers:";
      this.rbNumbers.UseVisualStyleBackColor = true;
      // 
      // rbCurrent
      // 
      this.rbCurrent.AutoSize = true;
      this.rbCurrent.Location = new System.Drawing.Point(12, 40);
      this.rbCurrent.Name = "rbCurrent";
      this.rbCurrent.Size = new System.Drawing.Size(62, 17);
      this.rbCurrent.TabIndex = 1;
      this.rbCurrent.TabStop = true;
      this.rbCurrent.Text = "Current";
      this.rbCurrent.UseVisualStyleBackColor = true;
      this.rbCurrent.CheckedChanged += new System.EventHandler(this.rbCurrent_CheckedChanged);
      // 
      // rbAll
      // 
      this.rbAll.AutoSize = true;
      this.rbAll.Location = new System.Drawing.Point(12, 20);
      this.rbAll.Name = "rbAll";
      this.rbAll.Size = new System.Drawing.Size(36, 17);
      this.rbAll.TabIndex = 0;
      this.rbAll.TabStop = true;
      this.rbAll.Text = "All";
      this.rbAll.UseVisualStyleBackColor = true;
      this.rbAll.CheckedChanged += new System.EventHandler(this.rbCurrent_CheckedChanged);
      // 
      // cbOpenAfter
      // 
      this.cbOpenAfter.AutoSize = true;
      this.cbOpenAfter.Checked = true;
      this.cbOpenAfter.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbOpenAfter.Location = new System.Drawing.Point(12, 204);
      this.cbOpenAfter.Name = "cbOpenAfter";
      this.cbOpenAfter.Size = new System.Drawing.Size(114, 17);
      this.cbOpenAfter.TabIndex = 5;
      this.cbOpenAfter.Text = "Open after export";
      this.cbOpenAfter.UseVisualStyleBackColor = true;
      // 
      // BaseExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(319, 266);
      this.Controls.Add(this.cbOpenAfter);
      this.Controls.Add(this.pcPages);
      this.Name = "BaseExportForm";
      this.Text = "BaseExportForm";
      this.Controls.SetChildIndex(this.pcPages, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.cbOpenAfter, 0);
      this.pcPages.ResumeLayout(false);
      this.panPages.ResumeLayout(false);
      this.gbPageRange.ResumeLayout(false);
      this.gbPageRange.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    /// <summary>
    /// "Page Range" groupbox.
    /// </summary>
    protected System.Windows.Forms.GroupBox gbPageRange;

    /// <summary>
    /// "Page Numbers" label.
    /// </summary>
    protected System.Windows.Forms.Label lblHint;

    /// <summary>
    /// "Page Numbers" textbox.
    /// </summary>
    protected System.Windows.Forms.TextBox tbNumbers;

    /// <summary>
    /// "Page Numbers" radiobutton.
    /// </summary>
    protected System.Windows.Forms.RadioButton rbNumbers;

    /// <summary>
    /// "Current Page" radiobutton.
    /// </summary>
    protected System.Windows.Forms.RadioButton rbCurrent;

    /// <summary>
    /// "All Pages" radiobutton.
    /// </summary>
    protected System.Windows.Forms.RadioButton rbAll;

    /// <summary>
    /// PageControl.
    /// </summary>
    protected FastReport.Controls.PageControl pcPages;

    /// <summary>
    /// Pages panel.
    /// </summary>
    protected FastReport.Controls.PageControlPage panPages;
    
    /// <summary>
    /// "Open after export" checkbox.
    /// </summary>
    protected System.Windows.Forms.CheckBox cbOpenAfter;
  }
}
