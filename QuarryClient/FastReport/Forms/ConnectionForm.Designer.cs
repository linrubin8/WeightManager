namespace FastReport.Forms
{
  partial class ConnectionForm
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
      this.cbAlwaysUse = new System.Windows.Forms.CheckBox();
      this.cbxConnections = new System.Windows.Forms.ComboBox();
      this.btnTest = new System.Windows.Forms.Button();
      this.cbLoginPrompt = new System.Windows.Forms.CheckBox();
      this.gbSelect.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(172, 119);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(252, 119);
      // 
      // gbSelect
      // 
      this.gbSelect.Controls.Add(this.cbAlwaysUse);
      this.gbSelect.Controls.Add(this.cbxConnections);
      this.gbSelect.Location = new System.Drawing.Point(8, 4);
      this.gbSelect.Name = "gbSelect";
      this.gbSelect.Size = new System.Drawing.Size(320, 76);
      this.gbSelect.TabIndex = 1;
      this.gbSelect.TabStop = false;
      this.gbSelect.Text = "Select connection type";
      // 
      // cbAlwaysUse
      // 
      this.cbAlwaysUse.AutoSize = true;
      this.cbAlwaysUse.Location = new System.Drawing.Point(12, 48);
      this.cbAlwaysUse.Name = "cbAlwaysUse";
      this.cbAlwaysUse.Size = new System.Drawing.Size(155, 17);
      this.cbAlwaysUse.TabIndex = 1;
      this.cbAlwaysUse.Text = "Always use this connection";
      this.cbAlwaysUse.UseVisualStyleBackColor = true;
      // 
      // cbxConnections
      // 
      this.cbxConnections.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxConnections.FormattingEnabled = true;
      this.cbxConnections.Location = new System.Drawing.Point(12, 20);
      this.cbxConnections.Name = "cbxConnections";
      this.cbxConnections.Size = new System.Drawing.Size(296, 21);
      this.cbxConnections.TabIndex = 0;
      this.cbxConnections.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxConnections_DrawItem);
      this.cbxConnections.SelectedIndexChanged += new System.EventHandler(this.cbxConnections_SelectedIndexChanged);
      // 
      // btnTest
      // 
      this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnTest.AutoSize = true;
      this.btnTest.Enabled = false;
      this.btnTest.Location = new System.Drawing.Point(8, 118);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(93, 23);
      this.btnTest.TabIndex = 2;
      this.btnTest.Text = "Test connection";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // cbLoginPrompt
      // 
      this.cbLoginPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbLoginPrompt.AutoSize = true;
      this.cbLoginPrompt.Location = new System.Drawing.Point(8, 92);
      this.cbLoginPrompt.Name = "cbLoginPrompt";
      this.cbLoginPrompt.Size = new System.Drawing.Size(88, 17);
      this.cbLoginPrompt.TabIndex = 3;
      this.cbLoginPrompt.Text = "Login prompt";
      this.cbLoginPrompt.UseVisualStyleBackColor = true;
      // 
      // ConnectionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(337, 151);
      this.Controls.Add(this.cbLoginPrompt);
      this.Controls.Add(this.btnTest);
      this.Controls.Add(this.gbSelect);
      this.Name = "ConnectionForm";
      this.Text = "Connection";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectionForm_FormClosed);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionForm_FormClosing);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbSelect, 0);
      this.Controls.SetChildIndex(this.btnTest, 0);
      this.Controls.SetChildIndex(this.cbLoginPrompt, 0);
      this.gbSelect.ResumeLayout(false);
      this.gbSelect.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbSelect;
    private System.Windows.Forms.ComboBox cbxConnections;
    private System.Windows.Forms.CheckBox cbAlwaysUse;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.CheckBox cbLoginPrompt;
  }
}
