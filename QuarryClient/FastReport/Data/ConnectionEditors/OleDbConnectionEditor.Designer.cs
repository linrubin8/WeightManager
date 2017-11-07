namespace FastReport.Data.ConnectionEditors
{
  partial class OleDbConnectionEditor
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
      this.gbConnection = new System.Windows.Forms.GroupBox();
      this.tbConnection = new System.Windows.Forms.TextBox();
      this.btnBuild = new System.Windows.Forms.Button();
      this.gbConnection.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbConnection
      // 
      this.gbConnection.Controls.Add(this.tbConnection);
      this.gbConnection.Controls.Add(this.btnBuild);
      this.gbConnection.Location = new System.Drawing.Point(8, 4);
      this.gbConnection.Name = "gbConnection";
      this.gbConnection.Size = new System.Drawing.Size(320, 136);
      this.gbConnection.TabIndex = 1;
      this.gbConnection.TabStop = false;
      this.gbConnection.Text = "Connection string:";
      // 
      // tbConnection
      // 
      this.tbConnection.AcceptsReturn = true;
      this.tbConnection.Location = new System.Drawing.Point(12, 52);
      this.tbConnection.Multiline = true;
      this.tbConnection.Name = "tbConnection";
      this.tbConnection.Size = new System.Drawing.Size(296, 72);
      this.tbConnection.TabIndex = 1;
      // 
      // btnBuild
      // 
      this.btnBuild.Location = new System.Drawing.Point(232, 20);
      this.btnBuild.Name = "btnBuild";
      this.btnBuild.Size = new System.Drawing.Size(75, 23);
      this.btnBuild.TabIndex = 2;
      this.btnBuild.Text = "Build...";
      this.btnBuild.UseVisualStyleBackColor = true;
      this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
      // 
      // OleDbConnectionEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.Controls.Add(this.gbConnection);
      this.Name = "OleDbConnectionEditor";
      this.Size = new System.Drawing.Size(336, 148);
      this.gbConnection.ResumeLayout(false);
      this.gbConnection.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbConnection;
    private System.Windows.Forms.TextBox tbConnection;
    private System.Windows.Forms.Button btnBuild;
  }
}
