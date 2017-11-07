namespace FastReport.Forms
{
  partial class AskPasswordForm
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
      this.pbPicture = new System.Windows.Forms.PictureBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.tbPassword = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(140, 96);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(220, 96);
      // 
      // pbPicture
      // 
      this.pbPicture.Location = new System.Drawing.Point(12, 12);
      this.pbPicture.Name = "pbPicture";
      this.pbPicture.Size = new System.Drawing.Size(52, 80);
      this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbPicture.TabIndex = 1;
      this.pbPicture.TabStop = false;
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(72, 12);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(172, 39);
      this.lblPassword.TabIndex = 2;
      this.lblPassword.Text = "This report is password protected.\r\n\r\nPlease enter the password:";
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(76, 56);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(216, 20);
      this.tbPassword.TabIndex = 3;
      this.tbPassword.UseSystemPasswordChar = true;
      // 
      // AskPasswordForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(304, 128);
      this.Controls.Add(this.pbPicture);
      this.Controls.Add(this.lblPassword);
      this.Controls.Add(this.tbPassword);
      this.Name = "AskPasswordForm";
      this.Text = "Password";
      this.Shown += new System.EventHandler(this.AskPasswordForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AskPasswordForm_FormClosed);
      this.Controls.SetChildIndex(this.tbPassword, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.lblPassword, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pbPicture, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbPicture;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox tbPassword;
  }
}
