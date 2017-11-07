namespace FastReport.Forms
{
  partial class QueryWizardForm
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
      this.tbName = new System.Windows.Forms.TextBox();
      this.lblSetName = new System.Windows.Forms.Label();
      this.lblTypeSql = new System.Windows.Forms.Label();
      this.tbSql = new System.Windows.Forms.TextBox();
      this.btnQueryBuilder = new System.Windows.Forms.Button();
      this.lblWhatData = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tvColumns = new System.Windows.Forms.TreeView();
      this.tsColumns = new System.Windows.Forms.ToolStrip();
      this.btnRefreshColumns = new System.Windows.Forms.ToolStripButton();
      this.btnAddColumn = new System.Windows.Forms.ToolStripButton();
      this.btnDeleteColumn = new System.Windows.Forms.ToolStripButton();
      this.pgColumnProperties = new FastReport.Controls.FRPropertyGrid();
      this.pnName = new System.Windows.Forms.Panel();
      this.lblNameHint = new System.Windows.Forms.Label();
      this.pnSql = new System.Windows.Forms.Panel();
      this.pnColumns = new System.Windows.Forms.Panel();
      this.pnParameters = new System.Windows.Forms.Panel();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tvParameters = new System.Windows.Forms.TreeView();
      this.tsParameters = new System.Windows.Forms.ToolStrip();
      this.btnAddParameter = new System.Windows.Forms.ToolStripButton();
      this.btnDeleteParameter = new System.Windows.Forms.ToolStripButton();
      this.btnParameterUp = new System.Windows.Forms.ToolStripButton();
      this.btnParameterDown = new System.Windows.Forms.ToolStripButton();
      this.pgParamProperties = new FastReport.Controls.FRPropertyGrid();
      this.pnTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.pnBottom.SuspendLayout();
      this.pcPages.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tsColumns.SuspendLayout();
      this.pnName.SuspendLayout();
      this.pnSql.SuspendLayout();
      this.pnColumns.SuspendLayout();
      this.pnParameters.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tsParameters.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnTop
      // 
      this.pnTop.Size = new System.Drawing.Size(442, 68);
      // 
      // picIcon
      // 
      this.picIcon.Location = new System.Drawing.Point(363, 8);
      // 
      // pnBottom
      // 
      this.pnBottom.Location = new System.Drawing.Point(0, 355);
      this.pnBottom.Size = new System.Drawing.Size(442, 45);
      // 
      // btnCancel1
      // 
      this.btnCancel1.Location = new System.Drawing.Point(356, 12);
      // 
      // btnFinish
      // 
      this.btnFinish.Location = new System.Drawing.Point(272, 12);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(188, 12);
      // 
      // btnPrevious
      // 
      this.btnPrevious.Location = new System.Drawing.Point(104, 12);
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.pnName);
      this.pcPages.Controls.Add(this.pnSql);
      this.pcPages.Controls.Add(this.pnParameters);
      this.pcPages.Controls.Add(this.pnColumns);
      this.pcPages.Size = new System.Drawing.Size(442, 287);
      // 
      // tbName
      // 
      this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbName.Location = new System.Drawing.Point(12, 62);
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(418, 20);
      this.tbName.TabIndex = 2;
      // 
      // lblSetName
      // 
      this.lblSetName.AutoSize = true;
      this.lblSetName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblSetName.Location = new System.Drawing.Point(12, 14);
      this.lblSetName.Name = "lblSetName";
      this.lblSetName.Size = new System.Drawing.Size(193, 13);
      this.lblSetName.TabIndex = 4;
      this.lblSetName.Text = "Set the name of the table object.";
      // 
      // lblTypeSql
      // 
      this.lblTypeSql.AutoSize = true;
      this.lblTypeSql.Location = new System.Drawing.Point(12, 38);
      this.lblTypeSql.Name = "lblTypeSql";
      this.lblTypeSql.Size = new System.Drawing.Size(324, 13);
      this.lblTypeSql.TabIndex = 3;
      this.lblTypeSql.Text = "Type your SQL statement or use the Query Builder to construct it.";
      // 
      // tbSql
      // 
      this.tbSql.AcceptsReturn = true;
      this.tbSql.AcceptsTab = true;
      this.tbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSql.Location = new System.Drawing.Point(12, 62);
      this.tbSql.MaxLength = 1000000;
      this.tbSql.Multiline = true;
      this.tbSql.Name = "tbSql";
      this.tbSql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbSql.Size = new System.Drawing.Size(418, 178);
      this.tbSql.TabIndex = 2;
      this.tbSql.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSql_KeyDown);
      // 
      // btnQueryBuilder
      // 
      this.btnQueryBuilder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnQueryBuilder.AutoSize = true;
      this.btnQueryBuilder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnQueryBuilder.Location = new System.Drawing.Point(338, 252);
      this.btnQueryBuilder.Name = "btnQueryBuilder";
      this.btnQueryBuilder.Size = new System.Drawing.Size(94, 23);
      this.btnQueryBuilder.TabIndex = 1;
      this.btnQueryBuilder.Text = "Query builder...";
      this.btnQueryBuilder.UseVisualStyleBackColor = true;
      this.btnQueryBuilder.Click += new System.EventHandler(this.btnQueryBuilder_Click);
      // 
      // lblWhatData
      // 
      this.lblWhatData.AutoSize = true;
      this.lblWhatData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblWhatData.Location = new System.Drawing.Point(12, 14);
      this.lblWhatData.Name = "lblWhatData";
      this.lblWhatData.Size = new System.Drawing.Size(249, 13);
      this.lblWhatData.TabIndex = 0;
      this.lblWhatData.Text = "What data should be loaded into the table?";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tvColumns);
      this.splitContainer1.Panel1.Controls.Add(this.tsColumns);
      this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(12, 13, 0, 12);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.pgColumnProperties);
      this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 12, 12, 12);
      this.splitContainer1.Size = new System.Drawing.Size(442, 287);
      this.splitContainer1.SplitterDistance = 223;
      this.splitContainer1.TabIndex = 0;
      // 
      // tvColumns
      // 
      this.tvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvColumns.HideSelection = false;
      this.tvColumns.Location = new System.Drawing.Point(12, 38);
      this.tvColumns.Name = "tvColumns";
      this.tvColumns.ShowLines = false;
      this.tvColumns.ShowRootLines = false;
      this.tvColumns.Size = new System.Drawing.Size(211, 237);
      this.tvColumns.TabIndex = 1;
      this.tvColumns.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvColumns_AfterSelect);
      this.tvColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvColumns_KeyDown);
      // 
      // tsColumns
      // 
      this.tsColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshColumns,
            this.btnAddColumn,
            this.btnDeleteColumn});
      this.tsColumns.Location = new System.Drawing.Point(12, 13);
      this.tsColumns.Name = "tsColumns";
      this.tsColumns.Size = new System.Drawing.Size(211, 25);
      this.tsColumns.TabIndex = 0;
      this.tsColumns.Text = "toolStrip1";
      // 
      // btnRefreshColumns
      // 
      this.btnRefreshColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRefreshColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRefreshColumns.Name = "btnRefreshColumns";
      this.btnRefreshColumns.Size = new System.Drawing.Size(23, 22);
      this.btnRefreshColumns.Click += new System.EventHandler(this.btnRefreshColumns_Click);
      // 
      // btnAddColumn
      // 
      this.btnAddColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAddColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddColumn.Name = "btnAddColumn";
      this.btnAddColumn.Size = new System.Drawing.Size(23, 22);
      this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
      // 
      // btnDeleteColumn
      // 
      this.btnDeleteColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDeleteColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDeleteColumn.Name = "btnDeleteColumn";
      this.btnDeleteColumn.Size = new System.Drawing.Size(23, 22);
      this.btnDeleteColumn.Click += new System.EventHandler(this.btnDeleteColumn_Click);
      // 
      // pgColumnProperties
      // 
      this.pgColumnProperties.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.pgColumnProperties.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
      this.pgColumnProperties.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.pgColumnProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pgColumnProperties.LineColor = System.Drawing.SystemColors.Control;
      this.pgColumnProperties.Location = new System.Drawing.Point(0, 12);
      this.pgColumnProperties.Name = "pgColumnProperties";
      this.pgColumnProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
      this.pgColumnProperties.Size = new System.Drawing.Size(203, 263);
      this.pgColumnProperties.TabIndex = 0;
      this.pgColumnProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgColumnProperties_PropertyValueChanged);
      // 
      // pnName
      // 
      this.pnName.Controls.Add(this.lblNameHint);
      this.pnName.Controls.Add(this.lblSetName);
      this.pnName.Controls.Add(this.tbName);
      this.pnName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnName.Location = new System.Drawing.Point(0, 0);
      this.pnName.Name = "pnName";
      this.pnName.Size = new System.Drawing.Size(442, 287);
      this.pnName.TabIndex = 0;
      // 
      // lblNameHint
      // 
      this.lblNameHint.AutoSize = true;
      this.lblNameHint.Location = new System.Drawing.Point(12, 38);
      this.lblNameHint.Name = "lblNameHint";
      this.lblNameHint.Size = new System.Drawing.Size(242, 13);
      this.lblNameHint.TabIndex = 5;
      this.lblNameHint.Text = "This name will be displayed in the \"Data\" window.";
      // 
      // pnSql
      // 
      this.pnSql.Controls.Add(this.lblTypeSql);
      this.pnSql.Controls.Add(this.lblWhatData);
      this.pnSql.Controls.Add(this.tbSql);
      this.pnSql.Controls.Add(this.btnQueryBuilder);
      this.pnSql.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnSql.Location = new System.Drawing.Point(0, 0);
      this.pnSql.Name = "pnSql";
      this.pnSql.Size = new System.Drawing.Size(442, 287);
      this.pnSql.TabIndex = 1;
      // 
      // pnColumns
      // 
      this.pnColumns.Controls.Add(this.splitContainer1);
      this.pnColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnColumns.Location = new System.Drawing.Point(0, 0);
      this.pnColumns.Name = "pnColumns";
      this.pnColumns.Size = new System.Drawing.Size(442, 287);
      this.pnColumns.TabIndex = 2;
      // 
      // pnParameters
      // 
      this.pnParameters.Controls.Add(this.splitContainer2);
      this.pnParameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnParameters.Location = new System.Drawing.Point(0, 0);
      this.pnParameters.Name = "pnParameters";
      this.pnParameters.Size = new System.Drawing.Size(442, 287);
      this.pnParameters.TabIndex = 3;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tvParameters);
      this.splitContainer2.Panel1.Controls.Add(this.tsParameters);
      this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(12, 13, 0, 12);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.pgParamProperties);
      this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 12, 12, 12);
      this.splitContainer2.Size = new System.Drawing.Size(442, 287);
      this.splitContainer2.SplitterDistance = 223;
      this.splitContainer2.TabIndex = 1;
      // 
      // tvParameters
      // 
      this.tvParameters.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvParameters.HideSelection = false;
      this.tvParameters.Location = new System.Drawing.Point(12, 38);
      this.tvParameters.Name = "tvParameters";
      this.tvParameters.ShowLines = false;
      this.tvParameters.ShowRootLines = false;
      this.tvParameters.Size = new System.Drawing.Size(211, 237);
      this.tvParameters.TabIndex = 1;
      this.tvParameters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvParameters_AfterSelect);
      this.tvParameters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvParameters_KeyDown);
      // 
      // tsParameters
      // 
      this.tsParameters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddParameter,
            this.btnDeleteParameter,
            this.btnParameterUp,
            this.btnParameterDown});
      this.tsParameters.Location = new System.Drawing.Point(12, 13);
      this.tsParameters.Name = "tsParameters";
      this.tsParameters.Size = new System.Drawing.Size(211, 25);
      this.tsParameters.TabIndex = 0;
      this.tsParameters.Text = "toolStrip1";
      // 
      // btnAddParameter
      // 
      this.btnAddParameter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAddParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddParameter.Name = "btnAddParameter";
      this.btnAddParameter.Size = new System.Drawing.Size(23, 22);
      this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
      // 
      // btnDeleteParameter
      // 
      this.btnDeleteParameter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDeleteParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDeleteParameter.Name = "btnDeleteParameter";
      this.btnDeleteParameter.Size = new System.Drawing.Size(23, 22);
      this.btnDeleteParameter.Click += new System.EventHandler(this.btnDeleteParameter_Click);
      // 
      // btnParameterUp
      // 
      this.btnParameterUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnParameterUp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnParameterUp.Name = "btnParameterUp";
      this.btnParameterUp.Size = new System.Drawing.Size(23, 22);
      this.btnParameterUp.Click += new System.EventHandler(this.btnParameterUp_Click);
      // 
      // btnParameterDown
      // 
      this.btnParameterDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnParameterDown.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnParameterDown.Name = "btnParameterDown";
      this.btnParameterDown.Size = new System.Drawing.Size(23, 22);
      this.btnParameterDown.Click += new System.EventHandler(this.btnParameterDown_Click);
      // 
      // pgParamProperties
      // 
      this.pgParamProperties.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.pgParamProperties.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
      this.pgParamProperties.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
      this.pgParamProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pgParamProperties.LineColor = System.Drawing.SystemColors.Control;
      this.pgParamProperties.Location = new System.Drawing.Point(0, 12);
      this.pgParamProperties.Name = "pgParamProperties";
      this.pgParamProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
      this.pgParamProperties.Size = new System.Drawing.Size(203, 263);
      this.pgParamProperties.TabIndex = 0;
      this.pgParamProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgParamProperties_PropertyValueChanged);
      // 
      // QueryWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(442, 400);
      this.MinimumSize = new System.Drawing.Size(450, 430);
      this.Name = "QueryWizardForm";
      this.Text = "Table";
      this.Shown += new System.EventHandler(this.TableWizardForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableWizardForm_FormClosed);
      this.pnTop.ResumeLayout(false);
      this.pnTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.pnBottom.ResumeLayout(false);
      this.pcPages.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tsColumns.ResumeLayout(false);
      this.tsColumns.PerformLayout();
      this.pnName.ResumeLayout(false);
      this.pnName.PerformLayout();
      this.pnSql.ResumeLayout(false);
      this.pnSql.PerformLayout();
      this.pnColumns.ResumeLayout(false);
      this.pnParameters.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.tsParameters.ResumeLayout(false);
      this.tsParameters.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label lblSetName;
    private System.Windows.Forms.TextBox tbSql;
    private System.Windows.Forms.Button btnQueryBuilder;
    private System.Windows.Forms.Label lblWhatData;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStrip tsColumns;
    private System.Windows.Forms.ToolStripButton btnRefreshColumns;
    private FastReport.Controls.FRPropertyGrid pgColumnProperties;
    private System.Windows.Forms.TreeView tvColumns;
    private System.Windows.Forms.ToolStripButton btnAddColumn;
    private System.Windows.Forms.Label lblTypeSql;
    private System.Windows.Forms.ToolStripButton btnDeleteColumn;
    private System.Windows.Forms.Panel pnName;
    private System.Windows.Forms.Panel pnSql;
    private System.Windows.Forms.Panel pnColumns;
    private System.Windows.Forms.Label lblNameHint;
    private System.Windows.Forms.Panel pnParameters;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TreeView tvParameters;
    private System.Windows.Forms.ToolStrip tsParameters;
    private System.Windows.Forms.ToolStripButton btnDeleteParameter;
    private System.Windows.Forms.ToolStripButton btnParameterUp;
    private System.Windows.Forms.ToolStripButton btnParameterDown;
    private FastReport.Controls.FRPropertyGrid pgParamProperties;
    private System.Windows.Forms.ToolStripButton btnAddParameter;
  }
}
