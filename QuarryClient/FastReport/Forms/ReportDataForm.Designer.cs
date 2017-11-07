namespace FastReport.Forms
{
  partial class ReportDataForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDataForm));
      this.tvData = new FastReport.Controls.DataTreeView();
      this.cbAliases = new System.Windows.Forms.CheckBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(104, 261);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(184, 261);
      // 
      // tvData
      // 
      this.tvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tvData.CheckBoxes = true;
      this.tvData.ExpandedNodes = ((System.Collections.Generic.List<string>)(resources.GetObject("tvData.ExpandedNodes")));
      this.tvData.ImageIndex = 0;
      this.tvData.Location = new System.Drawing.Point(12, 44);
      this.tvData.Name = "tvData";
      this.tvData.SelectedImageIndex = 0;
      this.tvData.ShowColumns = true;
      this.tvData.ShowDataSources = true;
      this.tvData.ShowEnabledOnly = false;
      this.tvData.ShowFunctions = false;
      this.tvData.ShowLines = false;
      this.tvData.ShowNone = false;
      this.tvData.ShowParameters = false;
      this.tvData.ShowRelations = true;
      this.tvData.ShowTotals = false;
      this.tvData.ShowVariables = false;
      this.tvData.Size = new System.Drawing.Size(248, 182);
      this.tvData.TabIndex = 1;
      this.tvData.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvData_AfterCheck);
      this.tvData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvData_BeforeExpand);
      // 
      // cbAliases
      // 
      this.cbAliases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbAliases.AutoSize = true;
      this.cbAliases.Checked = true;
      this.cbAliases.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbAliases.Location = new System.Drawing.Point(12, 234);
      this.cbAliases.Name = "cbAliases";
      this.cbAliases.Size = new System.Drawing.Size(87, 17);
      this.cbAliases.TabIndex = 2;
      this.cbAliases.Text = "Show aliases";
      this.cbAliases.UseVisualStyleBackColor = true;
      this.cbAliases.CheckedChanged += new System.EventHandler(this.cbAliases_CheckedChanged);
      // 
      // lblHint
      // 
      this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblHint.Location = new System.Drawing.Point(12, 8);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(248, 28);
      this.lblHint.TabIndex = 3;
      this.lblHint.Text = "Select data sources and fields that you want to use in the report:";
      // 
      // ReportDataForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(272, 296);
      this.Controls.Add(this.lblHint);
      this.Controls.Add(this.cbAliases);
      this.Controls.Add(this.tvData);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(280, 330);
      this.Name = "ReportDataForm";
      this.ShowIcon = false;
      this.Text = "Select Data Sources";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportDataForm_FormClosing);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.tvData, 0);
      this.Controls.SetChildIndex(this.cbAliases, 0);
      this.Controls.SetChildIndex(this.lblHint, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private FastReport.Controls.DataTreeView tvData;
    private System.Windows.Forms.CheckBox cbAliases;
    private System.Windows.Forms.Label lblHint;
  }
}
