namespace LB.MainForm.Permission
{
    partial class frmPermissionConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.tvPermission = new System.Windows.Forms.TreeView();
            this.cmsPermission = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAddPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeletePermission = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMovePermission = new System.Windows.Forms.ToolStripMenuItem();
            this.PermissionDataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionSPType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionViewType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionType = new LB.Controls.LBDataGridViewComboBoxColumn();
            this.BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LogFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forbid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.cmsPermission.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(989, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LB.MainForm.Properties.Resources.btnClose;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.LBPermissionCode = "";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 37);
            this.btnClose.Text = "关闭";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::LB.MainForm.Properties.Resources.btnSave;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 37);
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(290, 40);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 331);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(293, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 331);
            this.panel1.TabIndex = 3;
            // 
            // grdMain
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMain.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdMain.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PermissionDataName,
            this.PermissionCode,
            this.PermissionSPType,
            this.PermissionViewType,
            this.PermissionType,
            this.BtnDelete,
            this.LogFieldName,
            this.DetailIndex,
            this.Forbid});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Name = "grdMain";
            this.grdMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grdMain.RowTemplate.Height = 23;
            this.grdMain.Size = new System.Drawing.Size(696, 331);
            this.grdMain.TabIndex = 2;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // tvPermission
            // 
            this.tvPermission.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvPermission.Location = new System.Drawing.Point(0, 40);
            this.tvPermission.Name = "tvPermission";
            this.tvPermission.Size = new System.Drawing.Size(290, 331);
            this.tvPermission.TabIndex = 3;
            this.tvPermission.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPermission_AfterSelect);
            this.tvPermission.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPermission_NodeMouseClick);
            this.tvPermission.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvPermission_MouseClick);
            // 
            // cmsPermission
            // 
            this.cmsPermission.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddPermission,
            this.btnEditPermission,
            this.btnDeletePermission,
            this.btnMovePermission});
            this.cmsPermission.Name = "cmsPermission";
            this.cmsPermission.Size = new System.Drawing.Size(149, 92);
            // 
            // btnAddPermission
            // 
            this.btnAddPermission.Name = "btnAddPermission";
            this.btnAddPermission.Size = new System.Drawing.Size(148, 22);
            this.btnAddPermission.Text = "添加下级分类";
            this.btnAddPermission.Click += new System.EventHandler(this.btnAddPermission_Click);
            // 
            // btnEditPermission
            // 
            this.btnEditPermission.Name = "btnEditPermission";
            this.btnEditPermission.Size = new System.Drawing.Size(148, 22);
            this.btnEditPermission.Text = "修改分类";
            this.btnEditPermission.Click += new System.EventHandler(this.btnEditPermission_Click);
            // 
            // btnDeletePermission
            // 
            this.btnDeletePermission.Name = "btnDeletePermission";
            this.btnDeletePermission.Size = new System.Drawing.Size(148, 22);
            this.btnDeletePermission.Text = "删除";
            this.btnDeletePermission.Click += new System.EventHandler(this.btnDeletePermission_Click);
            // 
            // btnMovePermission
            // 
            this.btnMovePermission.Name = "btnMovePermission";
            this.btnMovePermission.Size = new System.Drawing.Size(148, 22);
            this.btnMovePermission.Text = "移至其他目录";
            this.btnMovePermission.Click += new System.EventHandler(this.btnMovePermission_Click);
            // 
            // PermissionDataName
            // 
            this.PermissionDataName.DataPropertyName = "PermissionDataName";
            this.PermissionDataName.HeaderText = "权限名称";
            this.PermissionDataName.Name = "PermissionDataName";
            this.PermissionDataName.Width = 120;
            // 
            // PermissionCode
            // 
            this.PermissionCode.DataPropertyName = "PermissionCode";
            this.PermissionCode.HeaderText = "权限码";
            this.PermissionCode.Name = "PermissionCode";
            this.PermissionCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PermissionCode.Width = 150;
            // 
            // PermissionSPType
            // 
            this.PermissionSPType.DataPropertyName = "PermissionSPType";
            this.PermissionSPType.HeaderText = "SP号";
            this.PermissionSPType.Name = "PermissionSPType";
            // 
            // PermissionViewType
            // 
            this.PermissionViewType.DataPropertyName = "PermissionViewType";
            this.PermissionViewType.HeaderText = "视图号";
            this.PermissionViewType.Name = "PermissionViewType";
            // 
            // PermissionType
            // 
            this.PermissionType.DataPropertyName = "PermissionType";
            this.PermissionType.FieldName = "PermissionType";
            this.PermissionType.HeaderText = "类型";
            this.PermissionType.Name = "PermissionType";
            // 
            // BtnDelete
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "删除";
            this.BtnDelete.DefaultCellStyle = dataGridViewCellStyle3;
            this.BtnDelete.HeaderText = "删除";
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Width = 60;
            // 
            // LogFieldName
            // 
            this.LogFieldName.DataPropertyName = "LogFieldName";
            this.LogFieldName.HeaderText = "日志记录字段";
            this.LogFieldName.Name = "LogFieldName";
            this.LogFieldName.Width = 200;
            // 
            // DetailIndex
            // 
            this.DetailIndex.DataPropertyName = "DetailIndex";
            this.DetailIndex.HeaderText = "顺序号";
            this.DetailIndex.Name = "DetailIndex";
            // 
            // Forbid
            // 
            this.Forbid.DataPropertyName = "Forbid";
            this.Forbid.HeaderText = "禁用";
            this.Forbid.Name = "Forbid";
            // 
            // frmPermissionConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tvPermission);
            this.Controls.Add(this.toolStrip1);
            this.LBPageTitle = "权限设置";
            this.Name = "frmPermissionConfig";
            this.Size = new System.Drawing.Size(989, 371);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.cmsPermission.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnSave;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.TreeView tvPermission;
        private System.Windows.Forms.ContextMenuStrip cmsPermission;
        private System.Windows.Forms.ToolStripMenuItem btnAddPermission;
        private System.Windows.Forms.ToolStripMenuItem btnDeletePermission;
        private System.Windows.Forms.ToolStripMenuItem btnMovePermission;
        private System.Windows.Forms.ToolStripMenuItem btnEditPermission;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionDataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionSPType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermissionViewType;
        private Controls.LBDataGridViewComboBoxColumn PermissionType;
        private System.Windows.Forms.DataGridViewButtonColumn BtnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailIndex;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Forbid;
    }
}