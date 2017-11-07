namespace FastReport.Forms
{
  partial class PreviewForm
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
      this.Preview = new FastReport.Preview.PreviewControl();
      this.SuspendLayout();
      // 
      // Preview
      // 
      this.Preview.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.Preview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Preview.Font = new System.Drawing.Font("Tahoma", 8F);
      this.Preview.Location = new System.Drawing.Point(0, 0);
      this.Preview.Name = "Preview";
      this.Preview.Size = new System.Drawing.Size(418, 316);
      this.Preview.TabIndex = 0;
      // 
      // PreviewForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(418, 316);
      this.Controls.Add(this.Preview);
      this.Font = new System.Drawing.Font("Tahoma", 8F);
      this.KeyPreview = true;
      this.Name = "PreviewForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "PreviewForm";
      this.Shown += new System.EventHandler(this.PreviewForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewForm_FormClosed);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PreviewForm_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion

    public FastReport.Preview.PreviewControl Preview;

  }
}