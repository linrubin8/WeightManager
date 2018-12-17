namespace LB.SysConfig.SysConfig
{
    partial class frmSessionManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSessionManager));
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SessionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCheckTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.skinToolStrip1.SuspendLayout();
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
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SessionID,
            this.LoginName,
            this.LoginTime,
            this.LastCheckTime,
            this.ClientIP});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = null;
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 40);
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
            this.grdMain.Size = new System.Drawing.Size(750, 270);
            this.grdMain.TabIndex = 5;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // skinToolStrip1
            // 
            this.skinToolStrip1.Arrow = System.Drawing.Color.Black;
            this.skinToolStrip1.Back = System.Drawing.Color.White;
            this.skinToolStrip1.BackRadius = 4;
            this.skinToolStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinToolStrip1.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinToolStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseForeAnamorphosis = false;
            this.skinToolStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinToolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinToolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.BaseItemAnamorphosis = true;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemDown")));
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemMouse")));
            this.skinToolStrip1.BaseItemNorml = null;
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BindTabControl = null;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnReflesh,
            this.btnDelete,
            this.toolStripSeparator1});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(750, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 4;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LB.SysConfig.Properties.Resources.btnClose;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.LBPermissionCode = "";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 37);
            this.btnClose.Text = "关闭";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::LB.SysConfig.Properties.Resources.btnDelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "SysSession_Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 37);
            this.btnDelete.Text = "删除记录";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // SessionID
            // 
            this.SessionID.DataPropertyName = "SessionID";
            this.SessionID.HeaderText = "登录号";
            this.SessionID.Name = "SessionID";
            this.SessionID.ReadOnly = true;
            this.SessionID.Width = 80;
            // 
            // LoginName
            // 
            this.LoginName.DataPropertyName = "LoginName";
            this.LoginName.HeaderText = "登录账号";
            this.LoginName.Name = "LoginName";
            this.LoginName.ReadOnly = true;
            // 
            // LoginTime
            // 
            this.LoginTime.DataPropertyName = "LoginTime";
            this.LoginTime.HeaderText = "登录时间";
            this.LoginTime.Name = "LoginTime";
            this.LoginTime.ReadOnly = true;
            this.LoginTime.Width = 200;
            // 
            // LastCheckTime
            // 
            this.LastCheckTime.DataPropertyName = "LastCheckTime";
            this.LastCheckTime.HeaderText = "最近连接时间";
            this.LastCheckTime.Name = "LastCheckTime";
            this.LastCheckTime.ReadOnly = true;
            this.LastCheckTime.Width = 200;
            // 
            // ClientIP
            // 
            this.ClientIP.DataPropertyName = "ClientIP";
            this.ClientIP.HeaderText = "客户端IP地址";
            this.ClientIP.Name = "ClientIP";
            this.ClientIP.ReadOnly = true;
            this.ClientIP.Width = 150;
            // 
            // btnReflesh
            // 
            this.btnReflesh.Image = ((System.Drawing.Image)(resources.GetObject("btnReflesh.Image")));
            this.btnReflesh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReflesh.LBPermissionCode = "";
            this.btnReflesh.Name = "btnReflesh";
            this.btnReflesh.Size = new System.Drawing.Size(36, 37);
            this.btnReflesh.Text = "刷新";
            this.btnReflesh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReflesh.Click += new System.EventHandler(this.btnReflesh_Click);
            // 
            // frmSessionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "备注信息维护";
            this.Name = "frmSessionManager";
            this.Size = new System.Drawing.Size(750, 310);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCheckTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientIP;
        private Controls.LBToolStripButton btnReflesh;
    }
}
