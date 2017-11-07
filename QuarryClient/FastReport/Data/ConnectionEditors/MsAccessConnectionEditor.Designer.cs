namespace FastReport.Data.ConnectionEditors
{
  partial class MsAccessConnectionEditor
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
      this.gbDatabase = new System.Windows.Forms.GroupBox();
      this.tbPassword = new System.Windows.Forms.TextBox();
      this.tbUserName = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.lblUserName = new System.Windows.Forms.Label();
      this.tbDatabase = new FastReport.Controls.TextBoxButton();
      this.lblDatabase = new System.Windows.Forms.Label();
      this.btnAdvanced = new System.Windows.Forms.Button();
      this.labelLine1 = new FastReport.Controls.LabelLine();
      this.gbDatabase.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbDatabase
      // 
      this.gbDatabase.Controls.Add(this.tbPassword);
      this.gbDatabase.Controls.Add(this.tbUserName);
      this.gbDatabase.Controls.Add(this.lblPassword);
      this.gbDatabase.Controls.Add(this.lblUserName);
      this.gbDatabase.Controls.Add(this.tbDatabase);
      this.gbDatabase.Controls.Add(this.lblDatabase);
      this.gbDatabase.Location = new System.Drawing.Point(8, 4);
      this.gbDatabase.Name = "gbDatabase";
      this.gbDatabase.Size = new System.Drawing.Size(320, 128);
      this.gbDatabase.TabIndex = 1;
      this.gbDatabase.TabStop = false;
      this.gbDatabase.Text = "Database";
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(120, 96);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.Size = new System.Drawing.Size(188, 20);
      this.tbPassword.TabIndex = 5;
      this.tbPassword.UseSystemPasswordChar = true;
      // 
      // tbUserName
      // 
      this.tbUserName.Location = new System.Drawing.Point(120, 72);
      this.tbUserName.Name = "tbUserName";
      this.tbUserName.Size = new System.Drawing.Size(188, 20);
      this.tbUserName.TabIndex = 4;
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(12, 100);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(57, 13);
      this.lblPassword.TabIndex = 3;
      this.lblPassword.Text = "Password:";
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(12, 76);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(62, 13);
      this.lblUserName.TabIndex = 2;
      this.lblUserName.Text = "User name:";
      // 
      // tbDatabase
      // 
      this.tbDatabase.Image = null;
      this.tbDatabase.Location = new System.Drawing.Point(12, 40);
      this.tbDatabase.Name = "tbDatabase";
      this.tbDatabase.Size = new System.Drawing.Size(296, 21);
      this.tbDatabase.TabIndex = 1;
      this.tbDatabase.ButtonClick += new System.EventHandler(this.tbDatabase_ButtonClick);
      // 
      // lblDatabase
      // 
      this.lblDatabase.AutoSize = true;
      this.lblDatabase.Location = new System.Drawing.Point(12, 20);
      this.lblDatabase.Name = "lblDatabase";
      this.lblDatabase.Size = new System.Drawing.Size(103, 13);
      this.lblDatabase.TabIndex = 0;
      this.lblDatabase.Text = "Database file name:";
      // 
      // btnAdvanced
      // 
      this.btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdvanced.AutoSize = true;
      this.btnAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAdvanced.Location = new System.Drawing.Point(250, 140);
      this.btnAdvanced.Name = "btnAdvanced";
      this.btnAdvanced.Size = new System.Drawing.Size(77, 23);
      this.btnAdvanced.TabIndex = 2;
      this.btnAdvanced.Text = "Advanced...";
      this.btnAdvanced.UseVisualStyleBackColor = true;
      this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
      // 
      // labelLine1
      // 
      this.labelLine1.Location = new System.Drawing.Point(8, 164);
      this.labelLine1.Name = "labelLine1";
      this.labelLine1.Size = new System.Drawing.Size(320, 16);
      this.labelLine1.TabIndex = 3;
      // 
      // MsAccessConnectionEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.Controls.Add(this.labelLine1);
      this.Controls.Add(this.btnAdvanced);
      this.Controls.Add(this.gbDatabase);
      this.Name = "MsAccessConnectionEditor";
      this.Size = new System.Drawing.Size(336, 183);
      this.gbDatabase.ResumeLayout(false);
      this.gbDatabase.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbDatabase;
    private System.Windows.Forms.TextBox tbPassword;
    private System.Windows.Forms.TextBox tbUserName;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblUserName;
    private FastReport.Controls.TextBoxButton tbDatabase;
    private System.Windows.Forms.Label lblDatabase;
    private System.Windows.Forms.Button btnAdvanced;
    private FastReport.Controls.LabelLine labelLine1;
  }
}
