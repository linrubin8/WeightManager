namespace LB.Controls.Report
{
    partial class frmReport
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.ReportTemplateID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportTemplateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateFileTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddReport = new LB.Controls.LBSkinButton(this.components);
            this.btnEditReport = new LB.Controls.LBSkinButton(this.components);
            this.btnDesignerReport = new LB.Controls.LBSkinButton(this.components);
            this.btnReLoadParm = new LB.Controls.LBSkinButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMain.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdMain.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReportTemplateID,
            this.ReportTemplateName,
            this.TemplateFileTime,
            this.Description});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(15, 25);
            this.grdMain.Name = "grdMain";
            this.grdMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grdMain.RowTemplate.Height = 23;
            this.grdMain.Size = new System.Drawing.Size(470, 150);
            this.grdMain.TabIndex = 0;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // ReportTemplateID
            // 
            this.ReportTemplateID.DataPropertyName = "ReportTemplateID";
            this.ReportTemplateID.HeaderText = "ReportTemplateID";
            this.ReportTemplateID.Name = "ReportTemplateID";
            this.ReportTemplateID.ReadOnly = true;
            this.ReportTemplateID.Visible = false;
            // 
            // ReportTemplateName
            // 
            this.ReportTemplateName.DataPropertyName = "ReportTemplateName";
            this.ReportTemplateName.HeaderText = "报表名称";
            this.ReportTemplateName.Name = "ReportTemplateName";
            this.ReportTemplateName.ReadOnly = true;
            this.ReportTemplateName.Width = 150;
            // 
            // TemplateFileTime
            // 
            this.TemplateFileTime.DataPropertyName = "TemplateFileTime";
            this.TemplateFileTime.HeaderText = "修改时间";
            this.TemplateFileTime.Name = "TemplateFileTime";
            this.TemplateFileTime.ReadOnly = true;
            this.TemplateFileTime.Width = 120;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "备注";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // btnAddReport
            // 
            this.btnAddReport.BackColor = System.Drawing.Color.Transparent;
            this.btnAddReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnAddReport.BorderColor = System.Drawing.Color.Gray;
            this.btnAddReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnAddReport.DownBack = null;
            this.btnAddReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddReport.LBPermissionCode = "";
            this.btnAddReport.Location = new System.Drawing.Point(54, 190);
            this.btnAddReport.MouseBack = null;
            this.btnAddReport.Name = "btnAddReport";
            this.btnAddReport.NormlBack = null;
            this.btnAddReport.Size = new System.Drawing.Size(75, 26);
            this.btnAddReport.TabIndex = 1;
            this.btnAddReport.Text = "添加报表";
            this.btnAddReport.UseVisualStyleBackColor = false;
            this.btnAddReport.Click += new System.EventHandler(this.btnAddReport_Click);
            // 
            // btnEditReport
            // 
            this.btnEditReport.BackColor = System.Drawing.Color.Transparent;
            this.btnEditReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnEditReport.BorderColor = System.Drawing.Color.Gray;
            this.btnEditReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnEditReport.DownBack = null;
            this.btnEditReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEditReport.LBPermissionCode = "";
            this.btnEditReport.Location = new System.Drawing.Point(135, 190);
            this.btnEditReport.MouseBack = null;
            this.btnEditReport.Name = "btnEditReport";
            this.btnEditReport.NormlBack = null;
            this.btnEditReport.Size = new System.Drawing.Size(90, 26);
            this.btnEditReport.TabIndex = 2;
            this.btnEditReport.Text = "修改报表信息";
            this.btnEditReport.UseVisualStyleBackColor = false;
            this.btnEditReport.Click += new System.EventHandler(this.btnEditReport_Click);
            // 
            // btnDesignerReport
            // 
            this.btnDesignerReport.BackColor = System.Drawing.Color.Transparent;
            this.btnDesignerReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnDesignerReport.BorderColor = System.Drawing.Color.Gray;
            this.btnDesignerReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDesignerReport.DownBack = null;
            this.btnDesignerReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDesignerReport.LBPermissionCode = "";
            this.btnDesignerReport.Location = new System.Drawing.Point(231, 190);
            this.btnDesignerReport.MouseBack = null;
            this.btnDesignerReport.Name = "btnDesignerReport";
            this.btnDesignerReport.NormlBack = null;
            this.btnDesignerReport.Size = new System.Drawing.Size(90, 26);
            this.btnDesignerReport.TabIndex = 3;
            this.btnDesignerReport.Text = "设计报表";
            this.btnDesignerReport.UseVisualStyleBackColor = false;
            this.btnDesignerReport.Click += new System.EventHandler(this.btnDesignerReport_Click);
            // 
            // btnReLoadParm
            // 
            this.btnReLoadParm.BackColor = System.Drawing.Color.Transparent;
            this.btnReLoadParm.BaseColor = System.Drawing.Color.LightGray;
            this.btnReLoadParm.BorderColor = System.Drawing.Color.Gray;
            this.btnReLoadParm.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnReLoadParm.DownBack = null;
            this.btnReLoadParm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnReLoadParm.LBPermissionCode = "";
            this.btnReLoadParm.Location = new System.Drawing.Point(327, 190);
            this.btnReLoadParm.MouseBack = null;
            this.btnReLoadParm.Name = "btnReLoadParm";
            this.btnReLoadParm.NormlBack = null;
            this.btnReLoadParm.Size = new System.Drawing.Size(90, 26);
            this.btnReLoadParm.TabIndex = 4;
            this.btnReLoadParm.Text = "更新报表参数";
            this.btnReLoadParm.UseVisualStyleBackColor = false;
            this.btnReLoadParm.Click += new System.EventHandler(this.btnReLoadParm_Click);
            // 
            // frmReport
            // 
            this.Controls.Add(this.btnReLoadParm);
            this.Controls.Add(this.btnDesignerReport);
            this.Controls.Add(this.btnEditReport);
            this.Controls.Add(this.btnAddReport);
            this.Controls.Add(this.grdMain);
            this.LBPageTitle = "报表设计";
            this.Name = "frmReport";
            this.Size = new System.Drawing.Size(498, 229);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportTemplateID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportTemplateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateFileTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private Controls.LBSkinButton btnAddReport;
        private Controls.LBSkinButton btnEditReport;
        private Controls.LBSkinButton btnDesignerReport;
        private Controls.LBSkinButton btnReLoadParm;
    }
}
