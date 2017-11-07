namespace LB.SysConfig
{
    partial class frmLogManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtLogModule = new CCWin.SkinControl.SkinTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new LB.Controls.LBToolStripButton();
            this.btnClose = new LB.Controls.LBToolStripButton();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.grdMain = new LB.Controls.LBDataGridView();
            this.txtLogStatusName = new DMSkin.Metro.Controls.MetroComboBox();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtLoginName = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtLogTimeFrom = new DMSkin.Metro.Controls.MetroDateTime();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtLogTimeTo = new DMSkin.Metro.Controls.MetroDateTime();
            this.btnSearch = new LB.Controls.LBSkinButton();
            this.SysLogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogMachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogMachineIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.skinToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(4, 8);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 8;
            this.skinLabel2.Text = "操作模块";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogModule
            // 
            this.txtLogModule.BackColor = System.Drawing.Color.Transparent;
            this.txtLogModule.DownBack = null;
            this.txtLogModule.Icon = null;
            this.txtLogModule.IconIsButton = false;
            this.txtLogModule.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLogModule.IsPasswordChat = '\0';
            this.txtLogModule.IsSystemPasswordChar = false;
            this.txtLogModule.Lines = new string[0];
            this.txtLogModule.Location = new System.Drawing.Point(90, 10);
            this.txtLogModule.Margin = new System.Windows.Forms.Padding(0);
            this.txtLogModule.MaxLength = 32767;
            this.txtLogModule.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtLogModule.MouseBack = null;
            this.txtLogModule.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLogModule.Multiline = false;
            this.txtLogModule.Name = "txtLogModule";
            this.txtLogModule.NormlBack = null;
            this.txtLogModule.Padding = new System.Windows.Forms.Padding(5);
            this.txtLogModule.ReadOnly = false;
            this.txtLogModule.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLogModule.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtLogModule.SkinTxt.AccessibleName = "";
            this.txtLogModule.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtLogModule.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtLogModule.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLogModule.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogModule.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogModule.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtLogModule.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtLogModule.SkinTxt.Name = "BaseText";
            this.txtLogModule.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtLogModule.SkinTxt.TabIndex = 0;
            this.txtLogModule.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLogModule.SkinTxt.WaterText = "";
            this.txtLogModule.TabIndex = 7;
            this.txtLogModule.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLogModule.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLogModule.WaterText = "";
            this.txtLogModule.WordWrap = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLogTimeTo);
            this.panel1.Controls.Add(this.skinLabel5);
            this.panel1.Controls.Add(this.txtLogTimeFrom);
            this.panel1.Controls.Add(this.skinLabel4);
            this.panel1.Controls.Add(this.txtLoginName);
            this.panel1.Controls.Add(this.skinLabel3);
            this.panel1.Controls.Add(this.txtLogStatusName);
            this.panel1.Controls.Add(this.skinLabel1);
            this.panel1.Controls.Add(this.skinLabel2);
            this.panel1.Controls.Add(this.txtLogModule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1317, 45);
            this.panel1.TabIndex = 5;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(230, 9);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(83, 32);
            this.skinLabel1.TabIndex = 10;
            this.skinLabel1.Text = "操作类型";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::LB.SysConfig.Properties.Resources.btnDelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "LogManager_Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 37);
            this.btnDelete.Text = "删除选中行";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnDelete,
            this.toolStripSeparator1});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1317, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 3;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
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
            //this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SysLogID,
            this.LogModule,
            this.LogStatusName,
            this.LoginName,
            this.LogTime,
            this.LogMachineName,
            this.LogMachineIP});
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
            this.grdMain.HeadFont = null;
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 85);
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
            this.grdMain.Size = new System.Drawing.Size(1317, 276);
            this.grdMain.TabIndex = 4;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // txtLogStatusName
            // 
            this.txtLogStatusName.DM_UseSelectable = true;
            this.txtLogStatusName.FormattingEnabled = true;
            this.txtLogStatusName.ItemHeight = 24;
            this.txtLogStatusName.Items.AddRange(new object[] {
            "",
            "操作",
            "查询"});
            this.txtLogStatusName.Location = new System.Drawing.Point(319, 10);
            this.txtLogStatusName.Name = "txtLogStatusName";
            this.txtLogStatusName.Size = new System.Drawing.Size(92, 30);
            this.txtLogStatusName.TabIndex = 12;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(417, 10);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(58, 32);
            this.skinLabel3.TabIndex = 13;
            this.skinLabel3.Text = "操作人";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLoginName
            // 
            this.txtLoginName.BackColor = System.Drawing.Color.Transparent;
            this.txtLoginName.DownBack = null;
            this.txtLoginName.Icon = null;
            this.txtLoginName.IconIsButton = false;
            this.txtLoginName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLoginName.IsPasswordChat = '\0';
            this.txtLoginName.IsSystemPasswordChar = false;
            this.txtLoginName.Lines = new string[0];
            this.txtLoginName.Location = new System.Drawing.Point(478, 10);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(0);
            this.txtLoginName.MaxLength = 32767;
            this.txtLoginName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtLoginName.MouseBack = null;
            this.txtLoginName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLoginName.Multiline = false;
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.NormlBack = null;
            this.txtLoginName.Padding = new System.Windows.Forms.Padding(5);
            this.txtLoginName.ReadOnly = false;
            this.txtLoginName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLoginName.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtLoginName.SkinTxt.AccessibleName = "";
            this.txtLoginName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtLoginName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtLoginName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLoginName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoginName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoginName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtLoginName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtLoginName.SkinTxt.Name = "BaseText";
            this.txtLoginName.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtLoginName.SkinTxt.TabIndex = 0;
            this.txtLoginName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLoginName.SkinTxt.WaterText = "";
            this.txtLoginName.TabIndex = 14;
            this.txtLoginName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLoginName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLoginName.WaterText = "";
            this.txtLoginName.WordWrap = true;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(618, 9);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(75, 32);
            this.skinLabel4.TabIndex = 15;
            this.skinLabel4.Text = "操作时间";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogTimeFrom
            // 
            this.txtLogTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtLogTimeFrom.Location = new System.Drawing.Point(699, 10);
            this.txtLogTimeFrom.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtLogTimeFrom.Name = "txtLogTimeFrom";
            this.txtLogTimeFrom.Size = new System.Drawing.Size(127, 30);
            this.txtLogTimeFrom.TabIndex = 16;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(832, 10);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(37, 32);
            this.skinLabel5.TabIndex = 17;
            this.skinLabel5.Text = "至";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogTimeTo
            // 
            this.txtLogTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtLogTimeTo.Location = new System.Drawing.Point(875, 10);
            this.txtLogTimeTo.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtLogTimeTo.Name = "txtLogTimeTo";
            this.txtLogTimeTo.Size = new System.Drawing.Size(127, 30);
            this.txtLogTimeTo.TabIndex = 18;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BaseColor = System.Drawing.Color.LightGray;
            this.btnSearch.BorderColor = System.Drawing.Color.Gray;
            this.btnSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSearch.DownBack = null;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnSearch.LBPermissionCode = "";
            this.btnSearch.Location = new System.Drawing.Point(1024, 10);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(75, 31);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // SysLogID
            // 
            this.SysLogID.DataPropertyName = "SysLogID";
            this.SysLogID.HeaderText = "流水号";
            this.SysLogID.Name = "SysLogID";
            this.SysLogID.ReadOnly = true;
            this.SysLogID.Visible = false;
            // 
            // LogModule
            // 
            this.LogModule.DataPropertyName = "LogModule";
            this.LogModule.HeaderText = "操作模块";
            this.LogModule.Name = "LogModule";
            this.LogModule.ReadOnly = true;
            this.LogModule.Width = 500;
            // 
            // LogStatusName
            // 
            this.LogStatusName.DataPropertyName = "LogStatusName";
            this.LogStatusName.HeaderText = "操作类型";
            this.LogStatusName.Name = "LogStatusName";
            this.LogStatusName.ReadOnly = true;
            // 
            // LoginName
            // 
            this.LoginName.DataPropertyName = "LoginName";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.LoginName.DefaultCellStyle = dataGridViewCellStyle3;
            this.LoginName.HeaderText = "操作人";
            this.LoginName.Name = "LoginName";
            this.LoginName.ReadOnly = true;
            this.LoginName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LoginName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LogTime
            // 
            this.LogTime.DataPropertyName = "LogTime";
            this.LogTime.HeaderText = "操作时间";
            this.LogTime.Name = "LogTime";
            this.LogTime.ReadOnly = true;
            this.LogTime.Width = 150;
            // 
            // LogMachineName
            // 
            this.LogMachineName.DataPropertyName = "LogMachineName";
            this.LogMachineName.HeaderText = "客户端电脑名称";
            this.LogMachineName.Name = "LogMachineName";
            this.LogMachineName.ReadOnly = true;
            this.LogMachineName.Width = 150;
            // 
            // LogMachineIP
            // 
            this.LogMachineIP.DataPropertyName = "LogMachineIP";
            this.LogMachineIP.HeaderText = "客户端电脑IP";
            this.LogMachineIP.Name = "LogMachineIP";
            this.LogMachineIP.ReadOnly = true;
            this.LogMachineIP.Width = 150;
            // 
            // frmLogManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "操作日志";
            this.Name = "frmLogManager";
            this.Size = new System.Drawing.Size(1317, 361);
            this.panel1.ResumeLayout(false);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinTextBox txtLogModule;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBToolStripButton btnDelete;
        private Controls.LBToolStripButton btnClose;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBDataGridView grdMain;
        private DMSkin.Metro.Controls.MetroComboBox txtLogStatusName;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinTextBox txtLoginName;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private DMSkin.Metro.Controls.MetroDateTime txtLogTimeFrom;
        private DMSkin.Metro.Controls.MetroDateTime txtLogTimeTo;
        private Controls.LBSkinButton btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysLogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogMachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogMachineIP;
    }
}
