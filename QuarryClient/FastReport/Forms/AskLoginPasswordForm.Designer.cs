namespace FastReport.Forms
{
  partial class AskLoginPasswordForm
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
      this.tbPassword = new System.Windows.Forms.TextBox();
      this.lblLogin = new System.Windows.Forms.Label();
      this.lblPassword = new System.Windows.Forms.Label();
      this.tbLogin = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(144, 84);
      this.btnOk.TabIndex = 2;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(224, 84);
      this.btnCancel.TabIndex = 3;
      // 
      // pbPicture
      // 
      this.pbPicture.Location = new System.Drawing.Point(12, 12);
      this.pbPicture.Name = "pbPicture";
      this.pbPicture.Size = new System.Drawing.Size(52, 80);
      this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbPicture.TabIndex = 4;
      this.pbPicture.TabStop = false;
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(196, 40);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(100, 20);
      this.tbPassword.TabIndex = 1;
      this.tbPassword.UseSystemPasswordChar = true;
      // 
      // lblLogin
      // 
      this.lblLogin.AutoSize = true;
      this.lblLogin.Location = new System.Drawing.Point(80, 16);
      this.lblLogin.Name = "lblLogin";
      this.lblLogin.Size = new System.Drawing.Size(35, 13);
      this.lblLogin.TabIndex = 7;
      this.lblLogin.Text = "label1";
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(80, 44);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(35, 13);
      this.lblPassword.TabIndex = 8;
      this.lblPassword.Text = "label2";
      // 
      // tbLogin
      // 
      this.tbLogin.Location = new System.Drawing.Point(196, 12);
      this.tbLogin.Name = "tbLogin";
      this.tbLogin.Size = new System.Drawing.Size(100, 20);
      this.tbLogin.TabIndex = 0;
      // 
      // AskLoginPasswordForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(309, 118);
      this.Controls.Add(this.tbLogin);
      this.Controls.Add(this.lblPassword);
      this.Controls.Add(this.lblLogin);
      this.Controls.Add(this.pbPicture);
      this.Controls.Add(this.tbPassword);
      this.Name = "AskLoginPasswordForm";
      this.Text = "Database Login";
      this.TopMost = true;
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.tbPassword, 0);
      this.Controls.SetChildIndex(this.pbPicture, 0);
      this.Controls.SetChildIndex(this.lblLogin, 0);
      this.Controls.SetChildIndex(this.lblPassword, 0);
      this.Controls.SetChildIndex(this.tbLogin, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbPicture;
    private System.Windows.Forms.TextBox tbPassword;
    private System.Windows.Forms.Label lblLogin;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox tbLogin;
  }
}
