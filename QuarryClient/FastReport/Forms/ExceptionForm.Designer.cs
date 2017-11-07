namespace FastReport.Forms
{
  partial class ExceptionForm
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
      this.lblHint = new System.Windows.Forms.Label();
      this.lblStack = new System.Windows.Forms.Label();
      this.tbStack = new System.Windows.Forms.TextBox();
      this.lblException = new System.Windows.Forms.Label();
      this.btnCopyToClipboard = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(252, 92);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(8, 232);
      this.btnCancel.Visible = false;
      // 
      // lblHint
      // 
      this.lblHint.AutoSize = true;
      this.lblHint.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblHint.Location = new System.Drawing.Point(12, 12);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(275, 13);
      this.lblHint.TabIndex = 1;
      this.lblHint.Text = "Your report has thrown the following exception:";
      // 
      // lblStack
      // 
      this.lblStack.AutoSize = true;
      this.lblStack.Location = new System.Drawing.Point(12, 108);
      this.lblStack.Name = "lblStack";
      this.lblStack.Size = new System.Drawing.Size(65, 13);
      this.lblStack.TabIndex = 2;
      this.lblStack.Text = "Stack trace:";
      // 
      // tbStack
      // 
      this.tbStack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbStack.Location = new System.Drawing.Point(12, 128);
      this.tbStack.Multiline = true;
      this.tbStack.Name = "tbStack";
      this.tbStack.ReadOnly = true;
      this.tbStack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbStack.Size = new System.Drawing.Size(433, 146);
      this.tbStack.TabIndex = 3;
      this.tbStack.WordWrap = false;
      // 
      // lblException
      // 
      this.lblException.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblException.Location = new System.Drawing.Point(12, 32);
      this.lblException.Name = "lblException";
      this.lblException.Size = new System.Drawing.Size(433, 52);
      this.lblException.TabIndex = 4;
      this.lblException.Text = "label3";
      // 
      // btnCopyToClipboard
      // 
      this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCopyToClipboard.Location = new System.Drawing.Point(332, 92);
      this.btnCopyToClipboard.Name = "btnCopyToClipboard";
      this.btnCopyToClipboard.Size = new System.Drawing.Size(114, 23);
      this.btnCopyToClipboard.TabIndex = 5;
      this.btnCopyToClipboard.Text = "Copy to clipboard";
      this.btnCopyToClipboard.UseVisualStyleBackColor = true;
      this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
      // 
      // ExceptionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(457, 286);
      this.Controls.Add(this.btnCopyToClipboard);
      this.Controls.Add(this.lblException);
      this.Controls.Add(this.tbStack);
      this.Controls.Add(this.lblHint);
      this.Controls.Add(this.lblStack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(320, 320);
      this.Name = "ExceptionForm";
      this.ShowIcon = false;
      this.Text = "Exception";
      this.Shown += new System.EventHandler(this.ExceptionForm_Shown);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.lblStack, 0);
      this.Controls.SetChildIndex(this.lblHint, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.tbStack, 0);
      this.Controls.SetChildIndex(this.lblException, 0);
      this.Controls.SetChildIndex(this.btnCopyToClipboard, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.Label lblStack;
    private System.Windows.Forms.TextBox tbStack;
    private System.Windows.Forms.Label lblException;
    private System.Windows.Forms.Button btnCopyToClipboard;
  }
}
