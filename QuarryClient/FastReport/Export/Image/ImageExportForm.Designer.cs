namespace FastReport.Export.Image
{
  partial class ImageExportForm
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
      this.gbOptions = new System.Windows.Forms.GroupBox();
      this.cbMonochrome = new System.Windows.Forms.CheckBox();
      this.lblX = new System.Windows.Forms.Label();
      this.cbMultiFrameTiff = new System.Windows.Forms.CheckBox();
      this.cbSeparateFiles = new System.Windows.Forms.CheckBox();
      this.udQuality = new System.Windows.Forms.NumericUpDown();
      this.lblQuality = new System.Windows.Forms.Label();
      this.udResolutionY = new System.Windows.Forms.NumericUpDown();
      this.udResolution = new System.Windows.Forms.NumericUpDown();
      this.lblResolution = new System.Windows.Forms.Label();
      this.cbxImageFormat = new System.Windows.Forms.ComboBox();
      this.lblImageFormat = new System.Windows.Forms.Label();
      this.gbPageRange.SuspendLayout();
      this.pcPages.SuspendLayout();
      this.panPages.SuspendLayout();
      this.gbOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udQuality)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udResolutionY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udResolution)).BeginInit();
      this.SuspendLayout();
      // 
      // gbPageRange
      // 
      this.gbPageRange.Location = new System.Drawing.Point(8, 4);
      // 
      // pcPages
      // 
      this.pcPages.Location = new System.Drawing.Point(0, 0);
      this.pcPages.Size = new System.Drawing.Size(276, 324);
      // 
      // panPages
      // 
      this.panPages.Controls.Add(this.gbOptions);
      this.panPages.Size = new System.Drawing.Size(276, 324);
      this.panPages.Controls.SetChildIndex(this.gbOptions, 0);
      this.panPages.Controls.SetChildIndex(this.gbPageRange, 0);
      // 
      // cbOpenAfter
      // 
      this.cbOpenAfter.Location = new System.Drawing.Point(8, 332);
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(112, 356);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(192, 356);
      // 
      // gbOptions
      // 
      this.gbOptions.Controls.Add(this.cbMonochrome);
      this.gbOptions.Controls.Add(this.lblX);
      this.gbOptions.Controls.Add(this.cbMultiFrameTiff);
      this.gbOptions.Controls.Add(this.cbSeparateFiles);
      this.gbOptions.Controls.Add(this.udQuality);
      this.gbOptions.Controls.Add(this.lblQuality);
      this.gbOptions.Controls.Add(this.udResolutionY);
      this.gbOptions.Controls.Add(this.udResolution);
      this.gbOptions.Controls.Add(this.lblResolution);
      this.gbOptions.Controls.Add(this.cbxImageFormat);
      this.gbOptions.Controls.Add(this.lblImageFormat);
      this.gbOptions.Location = new System.Drawing.Point(8, 136);
      this.gbOptions.Name = "gbOptions";
      this.gbOptions.Size = new System.Drawing.Size(260, 184);
      this.gbOptions.TabIndex = 4;
      this.gbOptions.TabStop = false;
      this.gbOptions.Text = "Options";
      // 
      // cbMonochrome
      // 
      this.cbMonochrome.AutoSize = true;
      this.cbMonochrome.Location = new System.Drawing.Point(12, 156);
      this.cbMonochrome.Name = "cbMonochrome";
      this.cbMonochrome.Size = new System.Drawing.Size(112, 17);
      this.cbMonochrome.TabIndex = 7;
      this.cbMonochrome.Text = "Monochrome TIFF";
      this.cbMonochrome.UseVisualStyleBackColor = true;
      // 
      // lblX
      // 
      this.lblX.AutoSize = true;
      this.lblX.Location = new System.Drawing.Point(170, 52);
      this.lblX.Name = "lblX";
      this.lblX.Size = new System.Drawing.Size(13, 13);
      this.lblX.TabIndex = 6;
      this.lblX.Text = "x";
      // 
      // cbMultiFrameTiff
      // 
      this.cbMultiFrameTiff.AutoSize = true;
      this.cbMultiFrameTiff.Location = new System.Drawing.Point(12, 132);
      this.cbMultiFrameTiff.Name = "cbMultiFrameTiff";
      this.cbMultiFrameTiff.Size = new System.Drawing.Size(105, 17);
      this.cbMultiFrameTiff.TabIndex = 5;
      this.cbMultiFrameTiff.Text = "Multi-frame TIFF";
      this.cbMultiFrameTiff.UseVisualStyleBackColor = true;
      this.cbMultiFrameTiff.CheckedChanged += new System.EventHandler(this.cbMultiFrameTiff_CheckedChanged);
      // 
      // cbSeparateFiles
      // 
      this.cbSeparateFiles.AutoSize = true;
      this.cbSeparateFiles.Location = new System.Drawing.Point(12, 108);
      this.cbSeparateFiles.Name = "cbSeparateFiles";
      this.cbSeparateFiles.Size = new System.Drawing.Size(157, 17);
      this.cbSeparateFiles.TabIndex = 4;
      this.cbSeparateFiles.Text = "Separate file for each page";
      this.cbSeparateFiles.UseVisualStyleBackColor = true;
      // 
      // udQuality
      // 
      this.udQuality.Location = new System.Drawing.Point(104, 76);
      this.udQuality.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.udQuality.Name = "udQuality";
      this.udQuality.Size = new System.Drawing.Size(60, 20);
      this.udQuality.TabIndex = 3;
      this.udQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // lblQuality
      // 
      this.lblQuality.AutoSize = true;
      this.lblQuality.Location = new System.Drawing.Point(12, 80);
      this.lblQuality.Name = "lblQuality";
      this.lblQuality.Size = new System.Drawing.Size(69, 13);
      this.lblQuality.TabIndex = 2;
      this.lblQuality.Text = "Jpeg quality:";
      // 
      // udResolutionY
      // 
      this.udResolutionY.Location = new System.Drawing.Point(188, 48);
      this.udResolutionY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.udResolutionY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.udResolutionY.Name = "udResolutionY";
      this.udResolutionY.Size = new System.Drawing.Size(60, 20);
      this.udResolutionY.TabIndex = 3;
      this.udResolutionY.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
      // 
      // udResolution
      // 
      this.udResolution.Location = new System.Drawing.Point(104, 48);
      this.udResolution.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.udResolution.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.udResolution.Name = "udResolution";
      this.udResolution.Size = new System.Drawing.Size(60, 20);
      this.udResolution.TabIndex = 3;
      this.udResolution.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
      // 
      // lblResolution
      // 
      this.lblResolution.AutoSize = true;
      this.lblResolution.Location = new System.Drawing.Point(12, 52);
      this.lblResolution.Name = "lblResolution";
      this.lblResolution.Size = new System.Drawing.Size(61, 13);
      this.lblResolution.TabIndex = 2;
      this.lblResolution.Text = "Resolution:";
      // 
      // cbxImageFormat
      // 
      this.cbxImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxImageFormat.FormattingEnabled = true;
      this.cbxImageFormat.Location = new System.Drawing.Point(104, 20);
      this.cbxImageFormat.Name = "cbxImageFormat";
      this.cbxImageFormat.Size = new System.Drawing.Size(144, 21);
      this.cbxImageFormat.TabIndex = 1;
      this.cbxImageFormat.SelectedIndexChanged += new System.EventHandler(this.cbxImageFormat_SelectedIndexChanged);
      // 
      // lblImageFormat
      // 
      this.lblImageFormat.AutoSize = true;
      this.lblImageFormat.Location = new System.Drawing.Point(12, 24);
      this.lblImageFormat.Name = "lblImageFormat";
      this.lblImageFormat.Size = new System.Drawing.Size(45, 13);
      this.lblImageFormat.TabIndex = 0;
      this.lblImageFormat.Text = "Format:";
      // 
      // ImageExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(276, 389);
      this.Name = "ImageExportForm";
      this.Text = "Export to Image";
      this.gbPageRange.ResumeLayout(false);
      this.gbPageRange.PerformLayout();
      this.pcPages.ResumeLayout(false);
      this.panPages.ResumeLayout(false);
      this.gbOptions.ResumeLayout(false);
      this.gbOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udQuality)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udResolutionY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udResolution)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbOptions;
    private System.Windows.Forms.ComboBox cbxImageFormat;
    private System.Windows.Forms.Label lblImageFormat;
    private System.Windows.Forms.NumericUpDown udResolution;
    private System.Windows.Forms.Label lblResolution;
    private System.Windows.Forms.CheckBox cbSeparateFiles;
    private System.Windows.Forms.NumericUpDown udQuality;
    private System.Windows.Forms.Label lblQuality;
    private System.Windows.Forms.CheckBox cbMultiFrameTiff;
    private System.Windows.Forms.Label lblX;
    private System.Windows.Forms.NumericUpDown udResolutionY;
    private System.Windows.Forms.CheckBox cbMonochrome;
  }
}
