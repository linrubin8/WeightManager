namespace FastReport.Data.ConnectionEditors
{
  partial class XmlConnectionEditor
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
      this.gbSelect = new System.Windows.Forms.GroupBox();
      this.tbXml = new FastReport.Controls.TextBoxButton();
      this.tbXsd = new FastReport.Controls.TextBoxButton();
      this.lblSelectXml = new System.Windows.Forms.Label();
      this.lblSelectXsd = new System.Windows.Forms.Label();
      this.gbSelect.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbSelect
      // 
      this.gbSelect.Controls.Add(this.tbXml);
      this.gbSelect.Controls.Add(this.tbXsd);
      this.gbSelect.Controls.Add(this.lblSelectXml);
      this.gbSelect.Controls.Add(this.lblSelectXsd);
      this.gbSelect.Location = new System.Drawing.Point(8, 4);
      this.gbSelect.Name = "gbSelect";
      this.gbSelect.Size = new System.Drawing.Size(320, 128);
      this.gbSelect.TabIndex = 1;
      this.gbSelect.TabStop = false;
      this.gbSelect.Text = "Select database file";
      // 
      // tbXml
      // 
      this.tbXml.Image = null;
      this.tbXml.Location = new System.Drawing.Point(12, 92);
      this.tbXml.Name = "tbXml";
      this.tbXml.Size = new System.Drawing.Size(296, 21);
      this.tbXml.TabIndex = 3;
      this.tbXml.ButtonClick += new System.EventHandler(this.tbXml_ButtonClick);
      // 
      // tbXsd
      // 
      this.tbXsd.Image = null;
      this.tbXsd.Location = new System.Drawing.Point(12, 40);
      this.tbXsd.Name = "tbXsd";
      this.tbXsd.Size = new System.Drawing.Size(296, 21);
      this.tbXsd.TabIndex = 2;
      this.tbXsd.ButtonClick += new System.EventHandler(this.tbXsd_ButtonClick);
      // 
      // lblSelectXml
      // 
      this.lblSelectXml.AutoSize = true;
      this.lblSelectXml.Location = new System.Drawing.Point(12, 72);
      this.lblSelectXml.Name = "lblSelectXml";
      this.lblSelectXml.Size = new System.Drawing.Size(80, 13);
      this.lblSelectXml.TabIndex = 1;
      this.lblSelectXml.Text = "Select .xml file:";
      // 
      // lblSelectXsd
      // 
      this.lblSelectXsd.AutoSize = true;
      this.lblSelectXsd.Location = new System.Drawing.Point(12, 20);
      this.lblSelectXsd.Name = "lblSelectXsd";
      this.lblSelectXsd.Size = new System.Drawing.Size(81, 13);
      this.lblSelectXsd.TabIndex = 0;
      this.lblSelectXsd.Text = "Select .xsd file:";
      // 
      // XmlConnectionEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.Controls.Add(this.gbSelect);
      this.Name = "XmlConnectionEditor";
      this.Size = new System.Drawing.Size(336, 140);
      this.gbSelect.ResumeLayout(false);
      this.gbSelect.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbSelect;
    private FastReport.Controls.TextBoxButton tbXml;
    private FastReport.Controls.TextBoxButton tbXsd;
    private System.Windows.Forms.Label lblSelectXml;
    private System.Windows.Forms.Label lblSelectXsd;
  }
}
