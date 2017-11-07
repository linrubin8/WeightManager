namespace FastReport.Forms
{
  partial class BaseReportWizardForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseReportWizardForm));
      this.panDataSource = new System.Windows.Forms.Panel();
      this.btnCreateNewDatasource = new System.Windows.Forms.Button();
      this.lblSelectDataSource = new System.Windows.Forms.Label();
      this.tvDataSources = new FastReport.Controls.DataTreeView();
      this.panColumns = new System.Windows.Forms.Panel();
      this.btnColumnDown = new System.Windows.Forms.Button();
      this.btnColumnUp = new System.Windows.Forms.Button();
      this.btnRemoveAllColumns = new System.Windows.Forms.Button();
      this.btnRemoveColumn = new System.Windows.Forms.Button();
      this.btnAddAllColumns = new System.Windows.Forms.Button();
      this.btnAddColumn = new System.Windows.Forms.Button();
      this.lblSelectedColumns = new System.Windows.Forms.Label();
      this.lblAvailableColumns = new System.Windows.Forms.Label();
      this.lvSelectedColumns = new System.Windows.Forms.ListView();
      this.lvAvailableColumns = new System.Windows.Forms.ListView();
      this.lblSelectColumns = new System.Windows.Forms.Label();
      this.pnTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.pnBottom.SuspendLayout();
      this.pcPages.SuspendLayout();
      this.panDataSource.SuspendLayout();
      this.panColumns.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnTop
      // 
      this.pnTop.Size = new System.Drawing.Size(465, 68);
      // 
      // lblCaption
      // 
      this.lblCaption.Size = new System.Drawing.Size(0, 17);
      this.lblCaption.Text = "";
      // 
      // pnBottom
      // 
      this.pnBottom.Location = new System.Drawing.Point(0, 358);
      this.pnBottom.Size = new System.Drawing.Size(465, 45);
      // 
      // btnCancel1
      // 
      this.btnCancel1.Location = new System.Drawing.Point(380, 12);
      // 
      // btnFinish
      // 
      this.btnFinish.Location = new System.Drawing.Point(296, 12);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(212, 12);
      // 
      // btnPrevious
      // 
      this.btnPrevious.Location = new System.Drawing.Point(128, 12);
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.panDataSource);
      this.pcPages.Controls.Add(this.panColumns);
      this.pcPages.Size = new System.Drawing.Size(465, 290);
      // 
      // panDataSource
      // 
      this.panDataSource.Controls.Add(this.btnCreateNewDatasource);
      this.panDataSource.Controls.Add(this.lblSelectDataSource);
      this.panDataSource.Controls.Add(this.tvDataSources);
      this.panDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panDataSource.Location = new System.Drawing.Point(0, 0);
      this.panDataSource.Name = "panDataSource";
      this.panDataSource.Size = new System.Drawing.Size(465, 290);
      this.panDataSource.TabIndex = 0;
      // 
      // btnCreateNewDatasource
      // 
      this.btnCreateNewDatasource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCreateNewDatasource.AutoSize = true;
      this.btnCreateNewDatasource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnCreateNewDatasource.Location = new System.Drawing.Point(311, 253);
      this.btnCreateNewDatasource.Name = "btnCreateNewDatasource";
      this.btnCreateNewDatasource.Size = new System.Drawing.Size(142, 23);
      this.btnCreateNewDatasource.TabIndex = 5;
      this.btnCreateNewDatasource.Text = "Create new datasource...";
      this.btnCreateNewDatasource.UseVisualStyleBackColor = true;
      this.btnCreateNewDatasource.Click += new System.EventHandler(this.btnCreateNewDatasource_Click);
      // 
      // lblSelectDataSource
      // 
      this.lblSelectDataSource.AutoSize = true;
      this.lblSelectDataSource.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblSelectDataSource.Location = new System.Drawing.Point(12, 12);
      this.lblSelectDataSource.Name = "lblSelectDataSource";
      this.lblSelectDataSource.Size = new System.Drawing.Size(291, 13);
      this.lblSelectDataSource.TabIndex = 4;
      this.lblSelectDataSource.Text = "Select one of the available datasources or create new one:";
      // 
      // tvDataSources
      // 
      this.tvDataSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tvDataSources.ExpandedNodes = ((System.Collections.Generic.List<string>)(resources.GetObject("tvDataSources.ExpandedNodes")));
      this.tvDataSources.HideSelection = false;
      this.tvDataSources.ImageIndex = 0;
      this.tvDataSources.Location = new System.Drawing.Point(12, 36);
      this.tvDataSources.Name = "tvDataSources";
      this.tvDataSources.SelectedImageIndex = 0;
      this.tvDataSources.ShowColumns = false;
      this.tvDataSources.ShowDataSources = true;
      this.tvDataSources.ShowEnabledOnly = true;
      this.tvDataSources.ShowNone = false;
      this.tvDataSources.ShowParameters = false;
      this.tvDataSources.ShowRelations = false;
      this.tvDataSources.ShowTotals = false;
      this.tvDataSources.ShowVariables = false;
      this.tvDataSources.Size = new System.Drawing.Size(440, 205);
      this.tvDataSources.TabIndex = 3;
      this.tvDataSources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDataSources_AfterSelect);
      // 
      // panColumns
      // 
      this.panColumns.Controls.Add(this.btnColumnDown);
      this.panColumns.Controls.Add(this.btnColumnUp);
      this.panColumns.Controls.Add(this.btnRemoveAllColumns);
      this.panColumns.Controls.Add(this.btnRemoveColumn);
      this.panColumns.Controls.Add(this.btnAddAllColumns);
      this.panColumns.Controls.Add(this.btnAddColumn);
      this.panColumns.Controls.Add(this.lblSelectedColumns);
      this.panColumns.Controls.Add(this.lblAvailableColumns);
      this.panColumns.Controls.Add(this.lvSelectedColumns);
      this.panColumns.Controls.Add(this.lvAvailableColumns);
      this.panColumns.Controls.Add(this.lblSelectColumns);
      this.panColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panColumns.Location = new System.Drawing.Point(0, 0);
      this.panColumns.Name = "panColumns";
      this.panColumns.Size = new System.Drawing.Size(465, 290);
      this.panColumns.TabIndex = 1;
      // 
      // btnColumnDown
      // 
      this.btnColumnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnColumnDown.Location = new System.Drawing.Point(216, 252);
      this.btnColumnDown.Name = "btnColumnDown";
      this.btnColumnDown.Size = new System.Drawing.Size(32, 24);
      this.btnColumnDown.TabIndex = 4;
      this.btnColumnDown.UseVisualStyleBackColor = true;
      this.btnColumnDown.Click += new System.EventHandler(this.btnColumnDown_Click);
      // 
      // btnColumnUp
      // 
      this.btnColumnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnColumnUp.Location = new System.Drawing.Point(216, 228);
      this.btnColumnUp.Name = "btnColumnUp";
      this.btnColumnUp.Size = new System.Drawing.Size(32, 24);
      this.btnColumnUp.TabIndex = 4;
      this.btnColumnUp.UseVisualStyleBackColor = true;
      this.btnColumnUp.Click += new System.EventHandler(this.btnColumnUp_Click);
      // 
      // btnRemoveAllColumns
      // 
      this.btnRemoveAllColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveAllColumns.Location = new System.Drawing.Point(216, 160);
      this.btnRemoveAllColumns.Name = "btnRemoveAllColumns";
      this.btnRemoveAllColumns.Size = new System.Drawing.Size(32, 24);
      this.btnRemoveAllColumns.TabIndex = 4;
      this.btnRemoveAllColumns.Text = "<<";
      this.btnRemoveAllColumns.UseVisualStyleBackColor = true;
      this.btnRemoveAllColumns.Click += new System.EventHandler(this.btnRemoveAllColumns_Click);
      // 
      // btnRemoveColumn
      // 
      this.btnRemoveColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveColumn.Location = new System.Drawing.Point(216, 136);
      this.btnRemoveColumn.Name = "btnRemoveColumn";
      this.btnRemoveColumn.Size = new System.Drawing.Size(32, 24);
      this.btnRemoveColumn.TabIndex = 4;
      this.btnRemoveColumn.Text = "<";
      this.btnRemoveColumn.UseVisualStyleBackColor = true;
      this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
      // 
      // btnAddAllColumns
      // 
      this.btnAddAllColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddAllColumns.Location = new System.Drawing.Point(216, 88);
      this.btnAddAllColumns.Name = "btnAddAllColumns";
      this.btnAddAllColumns.Size = new System.Drawing.Size(32, 24);
      this.btnAddAllColumns.TabIndex = 4;
      this.btnAddAllColumns.Text = ">>";
      this.btnAddAllColumns.UseVisualStyleBackColor = true;
      this.btnAddAllColumns.Click += new System.EventHandler(this.btnAddAllColumns_Click);
      // 
      // btnAddColumn
      // 
      this.btnAddColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddColumn.Location = new System.Drawing.Point(216, 64);
      this.btnAddColumn.Name = "btnAddColumn";
      this.btnAddColumn.Size = new System.Drawing.Size(32, 24);
      this.btnAddColumn.TabIndex = 4;
      this.btnAddColumn.Text = ">";
      this.btnAddColumn.UseVisualStyleBackColor = true;
      this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
      // 
      // lblSelectedColumns
      // 
      this.lblSelectedColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblSelectedColumns.AutoSize = true;
      this.lblSelectedColumns.Location = new System.Drawing.Point(260, 44);
      this.lblSelectedColumns.Name = "lblSelectedColumns";
      this.lblSelectedColumns.Size = new System.Drawing.Size(93, 13);
      this.lblSelectedColumns.TabIndex = 3;
      this.lblSelectedColumns.Text = "Selected columns:";
      // 
      // lblAvailableColumns
      // 
      this.lblAvailableColumns.AutoSize = true;
      this.lblAvailableColumns.Location = new System.Drawing.Point(12, 44);
      this.lblAvailableColumns.Name = "lblAvailableColumns";
      this.lblAvailableColumns.Size = new System.Drawing.Size(95, 13);
      this.lblAvailableColumns.TabIndex = 3;
      this.lblAvailableColumns.Text = "Available columns:";
      // 
      // lvSelectedColumns
      // 
      this.lvSelectedColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvSelectedColumns.HideSelection = false;
      this.lvSelectedColumns.Location = new System.Drawing.Point(260, 64);
      this.lvSelectedColumns.Name = "lvSelectedColumns";
      this.lvSelectedColumns.Size = new System.Drawing.Size(192, 213);
      this.lvSelectedColumns.TabIndex = 2;
      this.lvSelectedColumns.UseCompatibleStateImageBehavior = false;
      this.lvSelectedColumns.View = System.Windows.Forms.View.List;
      this.lvSelectedColumns.SelectedIndexChanged += new System.EventHandler(this.lvSelectedColumns_SelectedIndexChanged);
      // 
      // lvAvailableColumns
      // 
      this.lvAvailableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvAvailableColumns.HideSelection = false;
      this.lvAvailableColumns.Location = new System.Drawing.Point(12, 64);
      this.lvAvailableColumns.Name = "lvAvailableColumns";
      this.lvAvailableColumns.Size = new System.Drawing.Size(192, 213);
      this.lvAvailableColumns.TabIndex = 2;
      this.lvAvailableColumns.UseCompatibleStateImageBehavior = false;
      this.lvAvailableColumns.View = System.Windows.Forms.View.List;
      this.lvAvailableColumns.SelectedIndexChanged += new System.EventHandler(this.lvAvailableColumns_SelectedIndexChanged);
      // 
      // lblSelectColumns
      // 
      this.lblSelectColumns.AutoSize = true;
      this.lblSelectColumns.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblSelectColumns.Location = new System.Drawing.Point(12, 12);
      this.lblSelectColumns.Name = "lblSelectColumns";
      this.lblSelectColumns.Size = new System.Drawing.Size(271, 13);
      this.lblSelectColumns.TabIndex = 1;
      this.lblSelectColumns.Text = "Select data columns that you want to show in a report.";
      // 
      // BaseReportWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(465, 403);
      this.Name = "BaseReportWizardForm";
      this.Text = "Standard Report Wizard";
      this.pnTop.ResumeLayout(false);
      this.pnTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.pnBottom.ResumeLayout(false);
      this.pcPages.ResumeLayout(false);
      this.panDataSource.ResumeLayout(false);
      this.panDataSource.PerformLayout();
      this.panColumns.ResumeLayout(false);
      this.panColumns.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panDataSource;
    private System.Windows.Forms.Button btnCreateNewDatasource;
    private System.Windows.Forms.Label lblSelectDataSource;
    private FastReport.Controls.DataTreeView tvDataSources;
    private System.Windows.Forms.Panel panColumns;
    private System.Windows.Forms.ListView lvAvailableColumns;
    private System.Windows.Forms.Label lblSelectColumns;
    private System.Windows.Forms.Button btnRemoveAllColumns;
    private System.Windows.Forms.Button btnRemoveColumn;
    private System.Windows.Forms.Button btnAddAllColumns;
    private System.Windows.Forms.Button btnAddColumn;
    private System.Windows.Forms.Label lblSelectedColumns;
    private System.Windows.Forms.Label lblAvailableColumns;
    private System.Windows.Forms.ListView lvSelectedColumns;
    private System.Windows.Forms.Button btnColumnDown;
    private System.Windows.Forms.Button btnColumnUp;

  }
}
