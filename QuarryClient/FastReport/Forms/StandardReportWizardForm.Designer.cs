namespace FastReport.Forms
{
  partial class StandardReportWizardForm
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
      this.panGroups = new System.Windows.Forms.Panel();
      this.btnAddGroup = new System.Windows.Forms.Button();
      this.btnRemoveGroup = new System.Windows.Forms.Button();
      this.btnGroupUp = new System.Windows.Forms.Button();
      this.btnGroupDown = new System.Windows.Forms.Button();
      this.lblAvailableColumns1 = new System.Windows.Forms.Label();
      this.lblGroups = new System.Windows.Forms.Label();
      this.lvGroups = new System.Windows.Forms.ListView();
      this.lvAvailableColumns1 = new System.Windows.Forms.ListView();
      this.lblCreateGroups = new System.Windows.Forms.Label();
      this.panLayout = new System.Windows.Forms.Panel();
      this.lblLayout = new System.Windows.Forms.Label();
      this.pnPreview = new FastReport.Controls.SampleReportControl();
      this.gbLayout = new System.Windows.Forms.GroupBox();
      this.rbColumnar = new System.Windows.Forms.RadioButton();
      this.rbTabular = new System.Windows.Forms.RadioButton();
      this.gbOrientation = new System.Windows.Forms.GroupBox();
      this.rbLandscape = new System.Windows.Forms.RadioButton();
      this.rbPortrait = new System.Windows.Forms.RadioButton();
      this.panStyle = new System.Windows.Forms.Panel();
      this.lbStyles = new System.Windows.Forms.ListBox();
      this.lblStyles = new System.Windows.Forms.Label();
      this.pnStylePreview = new FastReport.Controls.SampleReportControl();
      this.pnTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.pnBottom.SuspendLayout();
      this.pcPages.SuspendLayout();
      this.panGroups.SuspendLayout();
      this.panLayout.SuspendLayout();
      this.gbLayout.SuspendLayout();
      this.gbOrientation.SuspendLayout();
      this.panStyle.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnFinish
      // 
      this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.panGroups);
      this.pcPages.Controls.Add(this.panLayout);
      this.pcPages.Controls.Add(this.panStyle);
      // 
      // panGroups
      // 
      this.panGroups.Controls.Add(this.btnAddGroup);
      this.panGroups.Controls.Add(this.btnRemoveGroup);
      this.panGroups.Controls.Add(this.btnGroupUp);
      this.panGroups.Controls.Add(this.btnGroupDown);
      this.panGroups.Controls.Add(this.lblAvailableColumns1);
      this.panGroups.Controls.Add(this.lblGroups);
      this.panGroups.Controls.Add(this.lvGroups);
      this.panGroups.Controls.Add(this.lvAvailableColumns1);
      this.panGroups.Controls.Add(this.lblCreateGroups);
      this.panGroups.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panGroups.Location = new System.Drawing.Point(0, 0);
      this.panGroups.Name = "panGroups";
      this.panGroups.Size = new System.Drawing.Size(465, 290);
      this.panGroups.TabIndex = 4;
      // 
      // btnAddGroup
      // 
      this.btnAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddGroup.Location = new System.Drawing.Point(216, 64);
      this.btnAddGroup.Name = "btnAddGroup";
      this.btnAddGroup.Size = new System.Drawing.Size(32, 24);
      this.btnAddGroup.TabIndex = 10;
      this.btnAddGroup.Text = ">";
      this.btnAddGroup.UseVisualStyleBackColor = true;
      this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
      // 
      // btnRemoveGroup
      // 
      this.btnRemoveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveGroup.Location = new System.Drawing.Point(216, 88);
      this.btnRemoveGroup.Name = "btnRemoveGroup";
      this.btnRemoveGroup.Size = new System.Drawing.Size(32, 24);
      this.btnRemoveGroup.TabIndex = 12;
      this.btnRemoveGroup.Text = "<";
      this.btnRemoveGroup.UseVisualStyleBackColor = true;
      this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
      // 
      // btnGroupUp
      // 
      this.btnGroupUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGroupUp.Location = new System.Drawing.Point(216, 228);
      this.btnGroupUp.Name = "btnGroupUp";
      this.btnGroupUp.Size = new System.Drawing.Size(32, 24);
      this.btnGroupUp.TabIndex = 13;
      this.btnGroupUp.UseVisualStyleBackColor = true;
      this.btnGroupUp.Click += new System.EventHandler(this.btnGroupUp_Click);
      // 
      // btnGroupDown
      // 
      this.btnGroupDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGroupDown.Location = new System.Drawing.Point(216, 252);
      this.btnGroupDown.Name = "btnGroupDown";
      this.btnGroupDown.Size = new System.Drawing.Size(32, 24);
      this.btnGroupDown.TabIndex = 14;
      this.btnGroupDown.UseVisualStyleBackColor = true;
      this.btnGroupDown.Click += new System.EventHandler(this.btnGroupDown_Click);
      // 
      // lblAvailableColumns1
      // 
      this.lblAvailableColumns1.AutoSize = true;
      this.lblAvailableColumns1.Location = new System.Drawing.Point(12, 44);
      this.lblAvailableColumns1.Name = "lblAvailableColumns1";
      this.lblAvailableColumns1.Size = new System.Drawing.Size(95, 13);
      this.lblAvailableColumns1.TabIndex = 9;
      this.lblAvailableColumns1.Text = "Available columns:";
      // 
      // lblGroups
      // 
      this.lblGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblGroups.AutoSize = true;
      this.lblGroups.Location = new System.Drawing.Point(260, 44);
      this.lblGroups.Name = "lblGroups";
      this.lblGroups.Size = new System.Drawing.Size(45, 13);
      this.lblGroups.TabIndex = 8;
      this.lblGroups.Text = "Groups:";
      // 
      // lvGroups
      // 
      this.lvGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvGroups.HideSelection = false;
      this.lvGroups.Location = new System.Drawing.Point(260, 64);
      this.lvGroups.Name = "lvGroups";
      this.lvGroups.Size = new System.Drawing.Size(192, 213);
      this.lvGroups.TabIndex = 6;
      this.lvGroups.UseCompatibleStateImageBehavior = false;
      this.lvGroups.View = System.Windows.Forms.View.List;
      this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
      // 
      // lvAvailableColumns1
      // 
      this.lvAvailableColumns1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvAvailableColumns1.HideSelection = false;
      this.lvAvailableColumns1.Location = new System.Drawing.Point(12, 64);
      this.lvAvailableColumns1.Name = "lvAvailableColumns1";
      this.lvAvailableColumns1.Size = new System.Drawing.Size(192, 213);
      this.lvAvailableColumns1.TabIndex = 7;
      this.lvAvailableColumns1.UseCompatibleStateImageBehavior = false;
      this.lvAvailableColumns1.View = System.Windows.Forms.View.List;
      this.lvAvailableColumns1.SelectedIndexChanged += new System.EventHandler(this.lvAvailableColumns1_SelectedIndexChanged);
      // 
      // lblCreateGroups
      // 
      this.lblCreateGroups.AutoSize = true;
      this.lblCreateGroups.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblCreateGroups.Location = new System.Drawing.Point(12, 12);
      this.lblCreateGroups.Name = "lblCreateGroups";
      this.lblCreateGroups.Size = new System.Drawing.Size(129, 13);
      this.lblCreateGroups.TabIndex = 5;
      this.lblCreateGroups.Text = "Create groups (optional).";
      // 
      // panLayout
      // 
      this.panLayout.Controls.Add(this.lblLayout);
      this.panLayout.Controls.Add(this.pnPreview);
      this.panLayout.Controls.Add(this.gbLayout);
      this.panLayout.Controls.Add(this.gbOrientation);
      this.panLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panLayout.Location = new System.Drawing.Point(0, 0);
      this.panLayout.Name = "panLayout";
      this.panLayout.Size = new System.Drawing.Size(465, 290);
      this.panLayout.TabIndex = 5;
      // 
      // lblLayout
      // 
      this.lblLayout.AutoSize = true;
      this.lblLayout.Location = new System.Drawing.Point(12, 12);
      this.lblLayout.Name = "lblLayout";
      this.lblLayout.Size = new System.Drawing.Size(226, 13);
      this.lblLayout.TabIndex = 3;
      this.lblLayout.Text = "Define the paper orientation and data layout.";
      // 
      // pnPreview
      // 
      this.pnPreview.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.pnPreview.FullPagePreview = true;
      this.pnPreview.Location = new System.Drawing.Point(224, 40);
      this.pnPreview.Name = "pnPreview";
      this.pnPreview.Size = new System.Drawing.Size(228, 236);
      this.pnPreview.TabIndex = 2;
      this.pnPreview.Text = "sampleReportControl1";
      // 
      // gbLayout
      // 
      this.gbLayout.Controls.Add(this.rbColumnar);
      this.gbLayout.Controls.Add(this.rbTabular);
      this.gbLayout.Location = new System.Drawing.Point(12, 112);
      this.gbLayout.Name = "gbLayout";
      this.gbLayout.Size = new System.Drawing.Size(200, 72);
      this.gbLayout.TabIndex = 1;
      this.gbLayout.TabStop = false;
      this.gbLayout.Text = "Layout";
      // 
      // rbColumnar
      // 
      this.rbColumnar.AutoSize = true;
      this.rbColumnar.Location = new System.Drawing.Point(12, 44);
      this.rbColumnar.Name = "rbColumnar";
      this.rbColumnar.Size = new System.Drawing.Size(70, 17);
      this.rbColumnar.TabIndex = 1;
      this.rbColumnar.Text = "Columnar";
      this.rbColumnar.UseVisualStyleBackColor = true;
      this.rbColumnar.CheckedChanged += new System.EventHandler(this.rbTabular_CheckedChanged);
      // 
      // rbTabular
      // 
      this.rbTabular.AutoSize = true;
      this.rbTabular.Checked = true;
      this.rbTabular.Location = new System.Drawing.Point(12, 20);
      this.rbTabular.Name = "rbTabular";
      this.rbTabular.Size = new System.Drawing.Size(61, 17);
      this.rbTabular.TabIndex = 0;
      this.rbTabular.TabStop = true;
      this.rbTabular.Text = "Tabular";
      this.rbTabular.UseVisualStyleBackColor = true;
      this.rbTabular.CheckedChanged += new System.EventHandler(this.rbTabular_CheckedChanged);
      // 
      // gbOrientation
      // 
      this.gbOrientation.Controls.Add(this.rbLandscape);
      this.gbOrientation.Controls.Add(this.rbPortrait);
      this.gbOrientation.Location = new System.Drawing.Point(12, 36);
      this.gbOrientation.Name = "gbOrientation";
      this.gbOrientation.Size = new System.Drawing.Size(200, 72);
      this.gbOrientation.TabIndex = 0;
      this.gbOrientation.TabStop = false;
      this.gbOrientation.Text = "Paper orientation";
      // 
      // rbLandscape
      // 
      this.rbLandscape.AutoSize = true;
      this.rbLandscape.Location = new System.Drawing.Point(12, 44);
      this.rbLandscape.Name = "rbLandscape";
      this.rbLandscape.Size = new System.Drawing.Size(76, 17);
      this.rbLandscape.TabIndex = 1;
      this.rbLandscape.Text = "Landscape";
      this.rbLandscape.UseVisualStyleBackColor = true;
      // 
      // rbPortrait
      // 
      this.rbPortrait.AutoSize = true;
      this.rbPortrait.Checked = true;
      this.rbPortrait.Location = new System.Drawing.Point(12, 20);
      this.rbPortrait.Name = "rbPortrait";
      this.rbPortrait.Size = new System.Drawing.Size(61, 17);
      this.rbPortrait.TabIndex = 0;
      this.rbPortrait.TabStop = true;
      this.rbPortrait.Text = "Portrait";
      this.rbPortrait.UseVisualStyleBackColor = true;
      // 
      // panStyle
      // 
      this.panStyle.Controls.Add(this.lbStyles);
      this.panStyle.Controls.Add(this.lblStyles);
      this.panStyle.Controls.Add(this.pnStylePreview);
      this.panStyle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panStyle.Location = new System.Drawing.Point(0, 0);
      this.panStyle.Name = "panStyle";
      this.panStyle.Size = new System.Drawing.Size(465, 290);
      this.panStyle.TabIndex = 6;
      // 
      // lbStyles
      // 
      this.lbStyles.FormattingEnabled = true;
      this.lbStyles.IntegralHeight = false;
      this.lbStyles.Location = new System.Drawing.Point(12, 40);
      this.lbStyles.Name = "lbStyles";
      this.lbStyles.Size = new System.Drawing.Size(228, 236);
      this.lbStyles.TabIndex = 2;
      this.lbStyles.SelectedIndexChanged += new System.EventHandler(this.lbStyles_SelectedIndexChanged);
      // 
      // lblStyles
      // 
      this.lblStyles.AutoSize = true;
      this.lblStyles.Location = new System.Drawing.Point(12, 12);
      this.lblStyles.Name = "lblStyles";
      this.lblStyles.Size = new System.Drawing.Size(118, 13);
      this.lblStyles.TabIndex = 1;
      this.lblStyles.Text = "Select the report style.";
      // 
      // pnStylePreview
      // 
      this.pnStylePreview.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.pnStylePreview.Location = new System.Drawing.Point(252, 40);
      this.pnStylePreview.Name = "pnStylePreview";
      this.pnStylePreview.Size = new System.Drawing.Size(200, 236);
      this.pnStylePreview.TabIndex = 0;
      this.pnStylePreview.Text = "sampleReportControl1";
      this.pnStylePreview.Zoom = 0.75F;
      // 
      // StandardReportWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(465, 403);
      this.Name = "StandardReportWizardForm";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StandardReportWizardForm_FormClosed);
      this.pnTop.ResumeLayout(false);
      this.pnTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.pnBottom.ResumeLayout(false);
      this.pcPages.ResumeLayout(false);
      this.panGroups.ResumeLayout(false);
      this.panGroups.PerformLayout();
      this.panLayout.ResumeLayout(false);
      this.panLayout.PerformLayout();
      this.gbLayout.ResumeLayout(false);
      this.gbLayout.PerformLayout();
      this.gbOrientation.ResumeLayout(false);
      this.gbOrientation.PerformLayout();
      this.panStyle.ResumeLayout(false);
      this.panStyle.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panGroups;
    private System.Windows.Forms.Button btnAddGroup;
    private System.Windows.Forms.Button btnRemoveGroup;
    private System.Windows.Forms.Button btnGroupUp;
    private System.Windows.Forms.Button btnGroupDown;
    private System.Windows.Forms.Label lblAvailableColumns1;
    private System.Windows.Forms.Label lblGroups;
    private System.Windows.Forms.ListView lvAvailableColumns1;
    private System.Windows.Forms.ListView lvGroups;
    private System.Windows.Forms.Label lblCreateGroups;
    private System.Windows.Forms.Panel panLayout;
    private System.Windows.Forms.GroupBox gbLayout;
    private System.Windows.Forms.GroupBox gbOrientation;
    private System.Windows.Forms.Label lblLayout;
    private FastReport.Controls.SampleReportControl pnPreview;
    private System.Windows.Forms.RadioButton rbColumnar;
    private System.Windows.Forms.RadioButton rbTabular;
    private System.Windows.Forms.RadioButton rbLandscape;
    private System.Windows.Forms.RadioButton rbPortrait;
    private System.Windows.Forms.Panel panStyle;
    private System.Windows.Forms.ListBox lbStyles;
    private System.Windows.Forms.Label lblStyles;
    private FastReport.Controls.SampleReportControl pnStylePreview;

  }
}
