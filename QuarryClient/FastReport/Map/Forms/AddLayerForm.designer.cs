namespace FastReport.Map.Forms
{
  partial class AddLayerForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLayerForm));
        this.rbShapefile = new System.Windows.Forms.RadioButton();
        this.cbEmbed = new System.Windows.Forms.CheckBox();
        this.rbEmptyLayer = new System.Windows.Forms.RadioButton();
        this.lblSource = new System.Windows.Forms.Label();
        this.tbShapefile = new FastReport.Controls.TextBoxButton();
        this.report1 = new FastReport.Report();
        ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
        this.SuspendLayout();
        // 
        // btnOk
        // 
        this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOk.Location = new System.Drawing.Point(317, 184);
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCancel.Location = new System.Drawing.Point(397, 184);
        // 
        // rbShapefile
        // 
        this.rbShapefile.AutoSize = true;
        this.rbShapefile.Checked = true;
        this.rbShapefile.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.rbShapefile.Location = new System.Drawing.Point(28, 52);
        this.rbShapefile.Name = "rbShapefile";
        this.rbShapefile.Size = new System.Drawing.Size(105, 17);
        this.rbShapefile.TabIndex = 1;
        this.rbShapefile.TabStop = true;
        this.rbShapefile.Text = "ESRI shapefile";
        this.rbShapefile.UseVisualStyleBackColor = true;
        this.rbShapefile.CheckedChanged += new System.EventHandler(this.rbShapefile_CheckedChanged);
        // 
        // cbEmbed
        // 
        this.cbEmbed.AutoSize = true;
        this.cbEmbed.Checked = true;
        this.cbEmbed.CheckState = System.Windows.Forms.CheckState.Checked;
        this.cbEmbed.Location = new System.Drawing.Point(48, 108);
        this.cbEmbed.Name = "cbEmbed";
        this.cbEmbed.Size = new System.Drawing.Size(186, 17);
        this.cbEmbed.TabIndex = 2;
        this.cbEmbed.Text = "Embed the shapefile in the report";
        this.cbEmbed.UseVisualStyleBackColor = true;
        // 
        // rbEmptyLayer
        // 
        this.rbEmptyLayer.AutoSize = true;
        this.rbEmptyLayer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.rbEmptyLayer.Location = new System.Drawing.Point(28, 144);
        this.rbEmptyLayer.Name = "rbEmptyLayer";
        this.rbEmptyLayer.Size = new System.Drawing.Size(322, 17);
        this.rbEmptyLayer.TabIndex = 1;
        this.rbEmptyLayer.Text = "Empty layer with geodata provided by an application";
        this.rbEmptyLayer.UseVisualStyleBackColor = true;
        this.rbEmptyLayer.CheckedChanged += new System.EventHandler(this.rbShapefile_CheckedChanged);
        // 
        // lblSource
        // 
        this.lblSource.AutoSize = true;
        this.lblSource.Location = new System.Drawing.Point(12, 20);
        this.lblSource.Name = "lblSource";
        this.lblSource.Size = new System.Drawing.Size(94, 13);
        this.lblSource.TabIndex = 4;
        this.lblSource.Text = "Select the source:";
        // 
        // tbShapefile
        // 
        this.tbShapefile.Image = null;
        this.tbShapefile.Location = new System.Drawing.Point(48, 76);
        this.tbShapefile.Name = "tbShapefile";
        this.tbShapefile.Size = new System.Drawing.Size(424, 21);
        this.tbShapefile.TabIndex = 6;
        this.tbShapefile.ButtonClick += new System.EventHandler(this.tbShapefile_ButtonClick);
        // 
        // report1
        // 
        this.report1.ReportResourceString = resources.GetString("report1.ReportResourceString");
        // 
        // AddLayerForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.ClientSize = new System.Drawing.Size(483, 220);
        this.Controls.Add(this.rbShapefile);
        this.Controls.Add(this.lblSource);
        this.Controls.Add(this.cbEmbed);
        this.Controls.Add(this.tbShapefile);
        this.Controls.Add(this.rbEmptyLayer);
        this.Name = "AddLayerForm";
        this.Text = "Add layer";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddLayerForm_FormClosed);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLayerForm_FormClosing);
        this.Controls.SetChildIndex(this.rbEmptyLayer, 0);
        this.Controls.SetChildIndex(this.tbShapefile, 0);
        this.Controls.SetChildIndex(this.cbEmbed, 0);
        this.Controls.SetChildIndex(this.lblSource, 0);
        this.Controls.SetChildIndex(this.rbShapefile, 0);
        this.Controls.SetChildIndex(this.btnCancel, 0);
        this.Controls.SetChildIndex(this.btnOk, 0);
        ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rbShapefile;
    private System.Windows.Forms.CheckBox cbEmbed;
    private System.Windows.Forms.RadioButton rbEmptyLayer;
    private System.Windows.Forms.Label lblSource;
    private FastReport.Controls.TextBoxButton tbShapefile;
      private Report report1;
  }
}
