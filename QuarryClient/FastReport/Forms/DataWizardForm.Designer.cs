namespace FastReport.Forms
{
  partial class DataWizardForm
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
      this.btnEditConnection = new System.Windows.Forms.Button();
      this.lblConnString = new FastReport.Controls.LabelLine();
      this.tbConnName = new System.Windows.Forms.TextBox();
      this.lblConnName = new System.Windows.Forms.Label();
      this.lblHint = new System.Windows.Forms.Label();
      this.btnNewConnection = new System.Windows.Forms.Button();
      this.tbConnString = new System.Windows.Forms.TextBox();
      this.lblWhichData = new System.Windows.Forms.Label();
      this.btnConnString = new System.Windows.Forms.Button();
      this.cbxConnections = new System.Windows.Forms.ComboBox();
      this.cbxCheckAll = new System.Windows.Forms.CheckBox();
      this.btnAddQuery = new System.Windows.Forms.Button();
      this.lblWait = new System.Windows.Forms.Label();
      this.lblWhichTables = new System.Windows.Forms.Label();
      this.tvTables = new System.Windows.Forms.TreeView();
      this.pnDatabase = new System.Windows.Forms.Panel();
      this.pnTables = new System.Windows.Forms.Panel();
      this.pnTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.pnBottom.SuspendLayout();
      this.pcPages.SuspendLayout();
      this.pnDatabase.SuspendLayout();
      this.pnTables.SuspendLayout();
      this.SuspendLayout();
      // 
      // pcPages
      // 
      this.pcPages.Controls.Add(this.pnDatabase);
      this.pcPages.Controls.Add(this.pnTables);
      // 
      // btnEditConnection
      // 
      this.btnEditConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEditConnection.Location = new System.Drawing.Point(308, 153);
      this.btnEditConnection.Name = "btnEditConnection";
      this.btnEditConnection.Size = new System.Drawing.Size(144, 23);
      this.btnEditConnection.TabIndex = 10;
      this.btnEditConnection.Text = "Edit connection...";
      this.btnEditConnection.UseVisualStyleBackColor = true;
      this.btnEditConnection.Click += new System.EventHandler(this.btnEditConnection_Click);
      // 
      // lblConnString
      // 
      this.lblConnString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblConnString.Location = new System.Drawing.Point(32, 202);
      this.lblConnString.Name = "lblConnString";
      this.lblConnString.Size = new System.Drawing.Size(420, 14);
      this.lblConnString.TabIndex = 9;
      this.lblConnString.Text = "Connection String";
      // 
      // tbConnName
      // 
      this.tbConnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbConnName.Location = new System.Drawing.Point(12, 154);
      this.tbConnName.Name = "tbConnName";
      this.tbConnName.Size = new System.Drawing.Size(288, 20);
      this.tbConnName.TabIndex = 8;
      this.tbConnName.Text = "Connection1";
      // 
      // lblConnName
      // 
      this.lblConnName.AutoSize = true;
      this.lblConnName.Location = new System.Drawing.Point(12, 134);
      this.lblConnName.Name = "lblConnName";
      this.lblConnName.Size = new System.Drawing.Size(140, 13);
      this.lblConnName.TabIndex = 7;
      this.lblConnName.Text = "Enter the connection name:";
      // 
      // lblHint
      // 
      this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblHint.Location = new System.Drawing.Point(12, 54);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(436, 32);
      this.lblHint.TabIndex = 6;
      this.lblHint.Text = "Select from list of last used connections, or press \"New connection...\" button to" +
          " create new connection.";
      // 
      // btnNewConnection
      // 
      this.btnNewConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNewConnection.Location = new System.Drawing.Point(308, 89);
      this.btnNewConnection.Name = "btnNewConnection";
      this.btnNewConnection.Size = new System.Drawing.Size(144, 23);
      this.btnNewConnection.TabIndex = 5;
      this.btnNewConnection.Text = "New connection...";
      this.btnNewConnection.UseVisualStyleBackColor = true;
      this.btnNewConnection.Click += new System.EventHandler(this.btnNewConnection_Click);
      // 
      // tbConnString
      // 
      this.tbConnString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbConnString.Location = new System.Drawing.Point(12, 226);
      this.tbConnString.Multiline = true;
      this.tbConnString.Name = "tbConnString";
      this.tbConnString.ReadOnly = true;
      this.tbConnString.Size = new System.Drawing.Size(440, 102);
      this.tbConnString.TabIndex = 4;
      this.tbConnString.Visible = false;
      // 
      // lblWhichData
      // 
      this.lblWhichData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblWhichData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblWhichData.Location = new System.Drawing.Point(12, 14);
      this.lblWhichData.Name = "lblWhichData";
      this.lblWhichData.Size = new System.Drawing.Size(436, 28);
      this.lblWhichData.TabIndex = 3;
      this.lblWhichData.Text = "Which data connection should your report use to connect to the database?";
      // 
      // btnConnString
      // 
      this.btnConnString.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnConnString.Location = new System.Drawing.Point(12, 202);
      this.btnConnString.Name = "btnConnString";
      this.btnConnString.Size = new System.Drawing.Size(15, 15);
      this.btnConnString.TabIndex = 2;
      this.btnConnString.UseCompatibleTextRendering = true;
      this.btnConnString.UseVisualStyleBackColor = true;
      this.btnConnString.Click += new System.EventHandler(this.btnConnString_Click);
      // 
      // cbxConnections
      // 
      this.cbxConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbxConnections.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxConnections.FormattingEnabled = true;
      this.cbxConnections.Location = new System.Drawing.Point(12, 90);
      this.cbxConnections.Name = "cbxConnections";
      this.cbxConnections.Size = new System.Drawing.Size(288, 21);
      this.cbxConnections.TabIndex = 0;
      this.cbxConnections.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxConnections_DrawItem);
      this.cbxConnections.SelectedIndexChanged += new System.EventHandler(this.cbxConnections_SelectedIndexChanged);
      // 
      // btnAddQuery
      // 
      this.btnAddQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddQuery.AutoSize = true;
      this.btnAddQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAddQuery.Location = new System.Drawing.Point(351, 304);
      this.btnAddQuery.Name = "btnAddQuery";
      this.btnAddQuery.Size = new System.Drawing.Size(101, 23);
      this.btnAddQuery.TabIndex = 3;
      this.btnAddQuery.Text = "Add SQL query...";
      this.btnAddQuery.UseVisualStyleBackColor = true;
      this.btnAddQuery.Click += new System.EventHandler(this.btnAddTable_Click);
      // 
      // lblWait
      // 
      this.lblWait.BackColor = System.Drawing.SystemColors.Window;
      this.lblWait.Location = new System.Drawing.Point(348, 14);
      this.lblWait.Name = "lblWait";
      this.lblWait.Size = new System.Drawing.Size(100, 23);
      this.lblWait.TabIndex = 2;
      this.lblWait.Text = "Wait";
      this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblWhichTables
      // 
      this.lblWhichTables.AutoSize = true;
      this.lblWhichTables.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblWhichTables.Location = new System.Drawing.Point(12, 14);
      this.lblWhichTables.Name = "lblWhichTables";
      this.lblWhichTables.Size = new System.Drawing.Size(221, 13);
      this.lblWhichTables.TabIndex = 1;
      this.lblWhichTables.Text = "Which tables you want in your report?";
      // 
      // tvTables
      // 
      this.tvTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tvTables.CheckBoxes = true;
      this.tvTables.Location = new System.Drawing.Point(12, 42);
      this.tvTables.Name = "tvTables";
      this.tvTables.ShowLines = false;
      this.tvTables.Size = new System.Drawing.Size(440, 250);
      this.tvTables.TabIndex = 0;
      this.tvTables.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterCheck);
      this.tvTables.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvTables_BeforeExpand);
      // 
      // pnDatabase
      // 
      this.pnDatabase.Controls.Add(this.btnEditConnection);
      this.pnDatabase.Controls.Add(this.lblWhichData);
      this.pnDatabase.Controls.Add(this.lblConnString);
      this.pnDatabase.Controls.Add(this.cbxConnections);
      this.pnDatabase.Controls.Add(this.tbConnName);
      this.pnDatabase.Controls.Add(this.btnConnString);
      this.pnDatabase.Controls.Add(this.lblConnName);
      this.pnDatabase.Controls.Add(this.tbConnString);
      this.pnDatabase.Controls.Add(this.lblHint);
      this.pnDatabase.Controls.Add(this.btnNewConnection);
      this.pnDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnDatabase.Location = new System.Drawing.Point(0, 0);
      this.pnDatabase.Name = "pnDatabase";
      this.pnDatabase.Size = new System.Drawing.Size(463, 340);
      this.pnDatabase.TabIndex = 0;
      // 
      // pnTables
      // 
      this.pnTables.Controls.Add(this.lblWait);
      this.pnTables.Controls.Add(this.btnAddQuery);
      this.pnTables.Controls.Add(this.tvTables);
      this.pnTables.Controls.Add(this.lblWhichTables);
      this.pnTables.Controls.Add(this.cbxCheckAll);
      this.pnTables.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnTables.Location = new System.Drawing.Point(0, 0);
      this.pnTables.Name = "pnTables";
      this.pnTables.Size = new System.Drawing.Size(463, 340);
      this.pnTables.TabIndex = 1;
      // 
      // cbxCheckAll
      // 
      this.cbxCheckAll.AutoSize = true;
      this.cbxCheckAll.Location = new System.Drawing.Point(12, 298);
      this.cbxCheckAll.Name = "cbxCheckAll";
      this.cbxCheckAll.Size = new System.Drawing.Size(68, 17);
      this.cbxCheckAll.TabIndex = 4;
      this.cbxCheckAll.Text = "Check all";
      this.cbxCheckAll.UseVisualStyleBackColor = true;
      this.cbxCheckAll.CheckedChanged += new System.EventHandler(this.cbxCheckAll_CheckedChanged);
      // 
      // DataWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(463, 453);
      this.Name = "DataWizardForm";
      this.Text = "Data Wizard";
      this.Shown += new System.EventHandler(this.DataWizardForm_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataWizardForm_FormClosed);
      this.pnTop.ResumeLayout(false);
      this.pnTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.pnBottom.ResumeLayout(false);
      this.pcPages.ResumeLayout(false);
      this.pnDatabase.ResumeLayout(false);
      this.pnDatabase.PerformLayout();
      this.pnTables.ResumeLayout(false);
      this.pnTables.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox tbConnName;
    private System.Windows.Forms.Label lblConnName;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.Button btnNewConnection;
    private System.Windows.Forms.TextBox tbConnString;
    private System.Windows.Forms.Label lblWhichData;
    private System.Windows.Forms.Button btnConnString;
    private System.Windows.Forms.ComboBox cbxConnections;
    private System.Windows.Forms.Label lblWhichTables;
    private System.Windows.Forms.TreeView tvTables;
    private FastReport.Controls.LabelLine lblConnString;
    private System.Windows.Forms.Button btnEditConnection;
    private System.Windows.Forms.Label lblWait;
    private System.Windows.Forms.Button btnAddQuery;
    private System.Windows.Forms.Panel pnDatabase;
    private System.Windows.Forms.Panel pnTables;
    private System.Windows.Forms.CheckBox cbxCheckAll;
  }
}
