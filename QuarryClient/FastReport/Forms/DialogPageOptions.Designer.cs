namespace FastReport.Forms
{
  partial class DialogPageOptions
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
      this.udSnapSize = new System.Windows.Forms.NumericUpDown();
      this.cbSnapToGrid = new System.Windows.Forms.CheckBox();
      this.cbShowGrid = new System.Windows.Forms.CheckBox();
      this.lblSnapSize = new System.Windows.Forms.Label();
      this.tc1.SuspendLayout();
      this.tab1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udSnapSize)).BeginInit();
      this.SuspendLayout();
      // 
      // tab1
      // 
      this.tab1.Controls.Add(this.udSnapSize);
      this.tab1.Controls.Add(this.cbShowGrid);
      this.tab1.Controls.Add(this.cbSnapToGrid);
      this.tab1.Controls.Add(this.lblSnapSize);
      // 
      // udSnapSize
      // 
      this.udSnapSize.Location = new System.Drawing.Point(120, 60);
      this.udSnapSize.Name = "udSnapSize";
      this.udSnapSize.Size = new System.Drawing.Size(56, 21);
      this.udSnapSize.TabIndex = 12;
      // 
      // cbSnapToGrid
      // 
      this.cbSnapToGrid.AutoSize = true;
      this.cbSnapToGrid.Location = new System.Drawing.Point(16, 40);
      this.cbSnapToGrid.Name = "cbSnapToGrid";
      this.cbSnapToGrid.Size = new System.Drawing.Size(84, 17);
      this.cbSnapToGrid.TabIndex = 10;
      this.cbSnapToGrid.Text = "Snap to grid";
      this.cbSnapToGrid.UseVisualStyleBackColor = true;
      // 
      // cbShowGrid
      // 
      this.cbShowGrid.AutoSize = true;
      this.cbShowGrid.Location = new System.Drawing.Point(16, 16);
      this.cbShowGrid.Name = "cbShowGrid";
      this.cbShowGrid.Size = new System.Drawing.Size(73, 17);
      this.cbShowGrid.TabIndex = 9;
      this.cbShowGrid.Text = "Show grid";
      this.cbShowGrid.UseVisualStyleBackColor = true;
      // 
      // lblSnapSize
      // 
      this.lblSnapSize.AutoSize = true;
      this.lblSnapSize.Location = new System.Drawing.Point(16, 64);
      this.lblSnapSize.Name = "lblSnapSize";
      this.lblSnapSize.Size = new System.Drawing.Size(56, 13);
      this.lblSnapSize.TabIndex = 11;
      this.lblSnapSize.Text = "Snap size:";
      // 
      // DialogPageOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(390, 315);
      this.Name = "DialogPageOptions";
      this.tc1.ResumeLayout(false);
      this.tab1.ResumeLayout(false);
      this.tab1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udSnapSize)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.NumericUpDown udSnapSize;
    private System.Windows.Forms.CheckBox cbSnapToGrid;
    private System.Windows.Forms.CheckBox cbShowGrid;
    private System.Windows.Forms.Label lblSnapSize;
  }
}
