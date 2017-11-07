namespace FastReport.Forms
{
  partial class LabelWizardForm
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
      this.cbxLabels = new System.Windows.Forms.ComboBox();
      this.lbxLabels = new System.Windows.Forms.ListBox();
      this.lblManufacturer = new System.Windows.Forms.Label();
      this.lblProduct = new System.Windows.Forms.Label();
      this.gbLabel = new System.Windows.Forms.GroupBox();
      this.btnDelete = new System.Windows.Forms.Button();
      this.gbParameters = new System.Windows.Forms.GroupBox();
      this.lblColumns1 = new System.Windows.Forms.Label();
      this.lblPaperSize1 = new System.Windows.Forms.Label();
      this.lblRows1 = new System.Windows.Forms.Label();
      this.lblColumns = new System.Windows.Forms.Label();
      this.lblSize1 = new System.Windows.Forms.Label();
      this.lblRows = new System.Windows.Forms.Label();
      this.lblPaperSize = new System.Windows.Forms.Label();
      this.lblSize = new System.Windows.Forms.Label();
      this.btnCustom = new System.Windows.Forms.Button();
      this.lblHint = new System.Windows.Forms.Label();
      this.gbLabel.SuspendLayout();
      this.gbParameters.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(200, 316);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(280, 316);
      // 
      // cbxLabels
      // 
      this.cbxLabels.DropDownHeight = 200;
      this.cbxLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxLabels.FormattingEnabled = true;
      this.cbxLabels.IntegralHeight = false;
      this.cbxLabels.Location = new System.Drawing.Point(100, 20);
      this.cbxLabels.Name = "cbxLabels";
      this.cbxLabels.Size = new System.Drawing.Size(236, 21);
      this.cbxLabels.TabIndex = 1;
      this.cbxLabels.SelectedIndexChanged += new System.EventHandler(this.cbxLabels_SelectedIndexChanged);
      // 
      // lbxLabels
      // 
      this.lbxLabels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.lbxLabels.FormattingEnabled = true;
      this.lbxLabels.Location = new System.Drawing.Point(100, 48);
      this.lbxLabels.Name = "lbxLabels";
      this.lbxLabels.Size = new System.Drawing.Size(236, 121);
      this.lbxLabels.TabIndex = 2;
      this.lbxLabels.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxLabels_DrawItem);
      this.lbxLabels.SelectedIndexChanged += new System.EventHandler(this.lbxLabels_SelectedIndexChanged);
      // 
      // lblManufacturer
      // 
      this.lblManufacturer.AutoSize = true;
      this.lblManufacturer.Location = new System.Drawing.Point(8, 24);
      this.lblManufacturer.Name = "lblManufacturer";
      this.lblManufacturer.Size = new System.Drawing.Size(76, 13);
      this.lblManufacturer.TabIndex = 3;
      this.lblManufacturer.Text = "Manufacturer:";
      // 
      // lblProduct
      // 
      this.lblProduct.AutoSize = true;
      this.lblProduct.Location = new System.Drawing.Point(8, 48);
      this.lblProduct.Name = "lblProduct";
      this.lblProduct.Size = new System.Drawing.Size(48, 13);
      this.lblProduct.TabIndex = 3;
      this.lblProduct.Text = "Product:";
      // 
      // gbLabel
      // 
      this.gbLabel.Controls.Add(this.btnDelete);
      this.gbLabel.Controls.Add(this.lbxLabels);
      this.gbLabel.Controls.Add(this.cbxLabels);
      this.gbLabel.Controls.Add(this.lblManufacturer);
      this.gbLabel.Controls.Add(this.lblProduct);
      this.gbLabel.Location = new System.Drawing.Point(8, 44);
      this.gbLabel.Name = "gbLabel";
      this.gbLabel.Size = new System.Drawing.Size(348, 184);
      this.gbLabel.TabIndex = 4;
      this.gbLabel.TabStop = false;
      this.gbLabel.Text = "Label";
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(12, 148);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 5;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // gbParameters
      // 
      this.gbParameters.Controls.Add(this.lblColumns1);
      this.gbParameters.Controls.Add(this.lblPaperSize1);
      this.gbParameters.Controls.Add(this.lblRows1);
      this.gbParameters.Controls.Add(this.lblColumns);
      this.gbParameters.Controls.Add(this.lblSize1);
      this.gbParameters.Controls.Add(this.lblRows);
      this.gbParameters.Controls.Add(this.lblPaperSize);
      this.gbParameters.Controls.Add(this.lblSize);
      this.gbParameters.Location = new System.Drawing.Point(8, 232);
      this.gbParameters.Name = "gbParameters";
      this.gbParameters.Size = new System.Drawing.Size(348, 72);
      this.gbParameters.TabIndex = 5;
      this.gbParameters.TabStop = false;
      this.gbParameters.Text = "Parameters";
      // 
      // lblColumns1
      // 
      this.lblColumns1.AutoSize = true;
      this.lblColumns1.Location = new System.Drawing.Point(300, 44);
      this.lblColumns1.Name = "lblColumns1";
      this.lblColumns1.Size = new System.Drawing.Size(35, 13);
      this.lblColumns1.TabIndex = 3;
      this.lblColumns1.Text = "label4";
      // 
      // lblPaperSize1
      // 
      this.lblPaperSize1.AutoSize = true;
      this.lblPaperSize1.Location = new System.Drawing.Point(96, 44);
      this.lblPaperSize1.Name = "lblPaperSize1";
      this.lblPaperSize1.Size = new System.Drawing.Size(35, 13);
      this.lblPaperSize1.TabIndex = 3;
      this.lblPaperSize1.Text = "label4";
      // 
      // lblRows1
      // 
      this.lblRows1.AutoSize = true;
      this.lblRows1.Location = new System.Drawing.Point(300, 20);
      this.lblRows1.Name = "lblRows1";
      this.lblRows1.Size = new System.Drawing.Size(35, 13);
      this.lblRows1.TabIndex = 2;
      this.lblRows1.Text = "label3";
      // 
      // lblColumns
      // 
      this.lblColumns.AutoSize = true;
      this.lblColumns.Location = new System.Drawing.Point(220, 44);
      this.lblColumns.Name = "lblColumns";
      this.lblColumns.Size = new System.Drawing.Size(51, 13);
      this.lblColumns.TabIndex = 1;
      this.lblColumns.Text = "Columns:";
      // 
      // lblSize1
      // 
      this.lblSize1.AutoSize = true;
      this.lblSize1.Location = new System.Drawing.Point(96, 20);
      this.lblSize1.Name = "lblSize1";
      this.lblSize1.Size = new System.Drawing.Size(35, 13);
      this.lblSize1.TabIndex = 2;
      this.lblSize1.Text = "label3";
      // 
      // lblRows
      // 
      this.lblRows.AutoSize = true;
      this.lblRows.Location = new System.Drawing.Point(220, 20);
      this.lblRows.Name = "lblRows";
      this.lblRows.Size = new System.Drawing.Size(37, 13);
      this.lblRows.TabIndex = 0;
      this.lblRows.Text = "Rows:";
      // 
      // lblPaperSize
      // 
      this.lblPaperSize.AutoSize = true;
      this.lblPaperSize.Location = new System.Drawing.Point(8, 44);
      this.lblPaperSize.Name = "lblPaperSize";
      this.lblPaperSize.Size = new System.Drawing.Size(60, 13);
      this.lblPaperSize.TabIndex = 1;
      this.lblPaperSize.Text = "Paper size:";
      // 
      // lblSize
      // 
      this.lblSize.AutoSize = true;
      this.lblSize.Location = new System.Drawing.Point(8, 20);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(30, 13);
      this.lblSize.TabIndex = 0;
      this.lblSize.Text = "Size:";
      // 
      // btnCustom
      // 
      this.btnCustom.AutoSize = true;
      this.btnCustom.Location = new System.Drawing.Point(8, 316);
      this.btnCustom.Name = "btnCustom";
      this.btnCustom.Size = new System.Drawing.Size(90, 23);
      this.btnCustom.TabIndex = 6;
      this.btnCustom.Text = "Custom label...";
      this.btnCustom.UseVisualStyleBackColor = true;
      this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
      // 
      // lblHint
      // 
      this.lblHint.Location = new System.Drawing.Point(8, 8);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(348, 32);
      this.lblHint.TabIndex = 7;
      this.lblHint.Text = "Select one of the predefined labels, or press \"Custom label...\" button to define " +
          "own label size.";
      // 
      // LabelWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(363, 348);
      this.Controls.Add(this.lblHint);
      this.Controls.Add(this.btnCustom);
      this.Controls.Add(this.gbParameters);
      this.Controls.Add(this.gbLabel);
      this.Name = "LabelWizardForm";
      this.Text = "Label Wizard";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LabelWizardForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbLabel, 0);
      this.Controls.SetChildIndex(this.gbParameters, 0);
      this.Controls.SetChildIndex(this.btnCustom, 0);
      this.Controls.SetChildIndex(this.lblHint, 0);
      this.gbLabel.ResumeLayout(false);
      this.gbLabel.PerformLayout();
      this.gbParameters.ResumeLayout(false);
      this.gbParameters.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cbxLabels;
    private System.Windows.Forms.ListBox lbxLabels;
    private System.Windows.Forms.Label lblManufacturer;
    private System.Windows.Forms.Label lblProduct;
    private System.Windows.Forms.GroupBox gbLabel;
    private System.Windows.Forms.GroupBox gbParameters;
    private System.Windows.Forms.Label lblPaperSize1;
    private System.Windows.Forms.Label lblSize1;
    private System.Windows.Forms.Label lblPaperSize;
    private System.Windows.Forms.Label lblSize;
    private System.Windows.Forms.Label lblColumns1;
    private System.Windows.Forms.Label lblRows1;
    private System.Windows.Forms.Label lblColumns;
    private System.Windows.Forms.Label lblRows;
    private System.Windows.Forms.Button btnCustom;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.Button btnDelete;
  }
}
