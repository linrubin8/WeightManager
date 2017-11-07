namespace LB.SysConfig.SysConfig
{
    partial class frmCarTareManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarTareManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.BillDateIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarTare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctlSearcher1 = new LB.Controls.Searcher.CtlSearcher();
            this.txtSearchDropDown = new LB.Controls.LBMetroComboBox(this.components);
            this.txtSearchText = new LB.Controls.LBSkinTextBox(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.ctlSearcher1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnReflesh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(508, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            // grdMain
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMain.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdMain.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillDateIn,
            this.CarNum,
            this.CustomerName,
            this.CarTare});
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
            this.grdMain.Location = new System.Drawing.Point(0, 87);
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
            this.grdMain.Size = new System.Drawing.Size(508, 361);
            this.grdMain.TabIndex = 3;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // BillDateIn
            // 
            this.BillDateIn.DataPropertyName = "BillDateIn";
            this.BillDateIn.HeaderText = "记录皮重时间";
            this.BillDateIn.Name = "BillDateIn";
            this.BillDateIn.ReadOnly = true;
            this.BillDateIn.Width = 150;
            // 
            // CarNum
            // 
            this.CarNum.DataPropertyName = "CarNum";
            this.CarNum.HeaderText = "车号";
            this.CarNum.Name = "CarNum";
            this.CarNum.ReadOnly = true;
            this.CarNum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CarTare
            // 
            this.CarTare.DataPropertyName = "CarTare";
            this.CarTare.HeaderText = "皮重";
            this.CarTare.Name = "CarTare";
            this.CarTare.ReadOnly = true;
            // 
            // ctlSearcher1
            // 
            this.ctlSearcher1.Controls.Add(this.txtSearchDropDown);
            this.ctlSearcher1.Controls.Add(this.txtSearchText);
            this.ctlSearcher1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlSearcher1.Location = new System.Drawing.Point(0, 40);
            this.ctlSearcher1.Name = "ctlSearcher1";
            this.ctlSearcher1.Size = new System.Drawing.Size(508, 47);
            this.ctlSearcher1.TabIndex = 31;
            // 
            // txtSearchDropDown
            // 
            this.txtSearchDropDown.CanBeEmpty = true;
            this.txtSearchDropDown.Caption = "";
            this.txtSearchDropDown.DM_UseSelectable = true;
            this.txtSearchDropDown.FormattingEnabled = true;
            this.txtSearchDropDown.ItemHeight = 24;
            this.txtSearchDropDown.Location = new System.Drawing.Point(629, 9);
            this.txtSearchDropDown.Name = "txtSearchDropDown";
            this.txtSearchDropDown.Size = new System.Drawing.Size(117, 30);
            this.txtSearchDropDown.Style = DMSkin.Metro.MetroColorStyle.Blue;
            this.txtSearchDropDown.TabIndex = 29;
            this.txtSearchDropDown.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.txtSearchDropDown.Visible = false;
            // 
            // txtSearchText
            // 
            this.txtSearchText.BackColor = System.Drawing.Color.Transparent;
            this.txtSearchText.CanBeEmpty = true;
            this.txtSearchText.Caption = "备注";
            this.txtSearchText.DownBack = null;
            this.txtSearchText.Icon = null;
            this.txtSearchText.IconIsButton = false;
            this.txtSearchText.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSearchText.IsPasswordChat = '\0';
            this.txtSearchText.IsSystemPasswordChar = false;
            this.txtSearchText.Lines = new string[0];
            this.txtSearchText.Location = new System.Drawing.Point(301, 9);
            this.txtSearchText.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearchText.MaxLength = 32767;
            this.txtSearchText.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtSearchText.MouseBack = null;
            this.txtSearchText.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSearchText.Multiline = false;
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.NormlBack = null;
            this.txtSearchText.Padding = new System.Windows.Forms.Padding(5);
            this.txtSearchText.ReadOnly = false;
            this.txtSearchText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchText.Size = new System.Drawing.Size(130, 28);
            // 
            // 
            // 
            this.txtSearchText.SkinTxt.AccessibleName = "";
            this.txtSearchText.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtSearchText.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSearchText.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchText.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchText.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchText.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtSearchText.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtSearchText.SkinTxt.Name = "BaseText";
            this.txtSearchText.SkinTxt.Size = new System.Drawing.Size(120, 18);
            this.txtSearchText.SkinTxt.TabIndex = 0;
            this.txtSearchText.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSearchText.SkinTxt.WaterText = "";
            this.txtSearchText.SkinTxt.WordWrap = false;
            this.txtSearchText.TabIndex = 24;
            this.txtSearchText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearchText.Visible = false;
            this.txtSearchText.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSearchText.WaterText = "";
            this.txtSearchText.WordWrap = false;
            // 
            // frmCarTareManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.ctlSearcher1);
            this.Controls.Add(this.toolStrip1);
            this.LBPageTitle = "皮重库清单";
            this.Name = "frmCarTareManager";
            this.Size = new System.Drawing.Size(508, 448);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ctlSearcher1.ResumeLayout(false);
            this.ctlSearcher1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDateIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarTare;
        private Controls.Searcher.CtlSearcher ctlSearcher1;
        private Controls.LBMetroComboBox txtSearchDropDown;
        private Controls.LBSkinTextBox txtSearchText;
        private Controls.LBToolStripButton btnReflesh;
    }
}
