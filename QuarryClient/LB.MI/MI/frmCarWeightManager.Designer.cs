namespace LB.MI.MI
{
    partial class frmCarWeightManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarWeightManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddCar = new LB.Controls.LBToolStripButton(this.components);
            this.btnAddCarWeight = new LB.Controls.LBToolStripButton(this.components);
            this.txtSearchDropDown = new LB.Controls.LBMetroComboBox(this.components);
            this.txtSearchText = new LB.Controls.LBSkinTextBox(this.components);
            this.ctlSearcher1 = new LB.Controls.Searcher.CtlSearcher();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdCarWeight = new LB.Controls.LBDataGridView(this.components);
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtBillDateTo = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtBillDateFrom = new LB.Controls.LBDateTimeTextBox(this.components);
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.CarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultCarWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCarWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCarWeight)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripSeparator1,
            this.btnAddCar,
            this.btnAddCarWeight});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1068, 40);
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // btnAddCar
            // 
            this.btnAddCar.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCar.Image")));
            this.btnAddCar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddCar.LBPermissionCode = "DBCar_Add";
            this.btnAddCar.Name = "btnAddCar";
            this.btnAddCar.Size = new System.Drawing.Size(60, 37);
            this.btnAddCar.Text = "新增车辆";
            this.btnAddCar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddCar.Click += new System.EventHandler(this.btnAddCar_Click);
            // 
            // btnAddCarWeight
            // 
            this.btnAddCarWeight.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCarWeight.Image")));
            this.btnAddCarWeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddCarWeight.LBPermissionCode = "DBCarWeight_Add";
            this.btnAddCarWeight.Name = "btnAddCarWeight";
            this.btnAddCarWeight.Size = new System.Drawing.Size(60, 37);
            this.btnAddCarWeight.Text = "新增皮重";
            this.btnAddCarWeight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddCarWeight.Click += new System.EventHandler(this.btnAddCarWeight_Click);
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
            this.txtSearchText.Location = new System.Drawing.Point(301, 10);
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
            this.txtSearchText.TabIndex = 24;
            this.txtSearchText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearchText.Visible = false;
            this.txtSearchText.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSearchText.WaterText = "";
            this.txtSearchText.WordWrap = true;
            // 
            // ctlSearcher1
            // 
            this.ctlSearcher1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlSearcher1.Location = new System.Drawing.Point(0, 40);
            this.ctlSearcher1.Name = "ctlSearcher1";
            this.ctlSearcher1.Size = new System.Drawing.Size(1068, 47);
            this.ctlSearcher1.TabIndex = 5;
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
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CarCode,
            this.CarNum,
            this.CustomerName,
            this.DefaultCarWeight,
            this.LastCarWeight,
            this.LastCreateTime,
            this.CarDescription});
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
            this.grdMain.Size = new System.Drawing.Size(640, 503);
            this.grdMain.TabIndex = 7;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            this.grdMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMain_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdCarWeight);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(640, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 503);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查看车辆历史皮重";
            // 
            // grdCarWeight
            // 
            this.grdCarWeight.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdCarWeight.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grdCarWeight.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdCarWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdCarWeight.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdCarWeight.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grdCarWeight.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdCarWeight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCarWeight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Description});
            this.grdCarWeight.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdCarWeight.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdCarWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCarWeight.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdCarWeight.EnableHeadersVisualStyles = false;
            this.grdCarWeight.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdCarWeight.HeadFont = null;
            this.grdCarWeight.HeadForeColor = System.Drawing.Color.Empty;
            this.grdCarWeight.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdCarWeight.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdCarWeight.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdCarWeight.Location = new System.Drawing.Point(3, 68);
            this.grdCarWeight.Name = "grdCarWeight";
            this.grdCarWeight.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdCarWeight.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdCarWeight.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grdCarWeight.RowTemplate.Height = 23;
            this.grdCarWeight.Size = new System.Drawing.Size(422, 432);
            this.grdCarWeight.TabIndex = 8;
            this.grdCarWeight.TitleBack = null;
            this.grdCarWeight.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdCarWeight.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CarWeight";
            this.dataGridViewTextBoxColumn3.HeaderText = "最新皮重";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CreateTime";
            this.dataGridViewTextBoxColumn4.HeaderText = "最近采集时间";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "皮重来源";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.skinLabel1);
            this.panel1.Controls.Add(this.txtBillDateTo);
            this.panel1.Controls.Add(this.txtBillDateFrom);
            this.panel1.Controls.Add(this.skinLabel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 51);
            this.panel1.TabIndex = 19;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(3, 6);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(83, 32);
            this.skinLabel1.TabIndex = 15;
            this.skinLabel1.Text = "采集时间";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillDateTo
            // 
            this.txtBillDateTo.CanBeEmpty = false;
            this.txtBillDateTo.Caption = "单据日期";
            this.txtBillDateTo.CustomFormat = "";
            this.txtBillDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDateTo.Location = new System.Drawing.Point(235, 9);
            this.txtBillDateTo.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtBillDateTo.Name = "txtBillDateTo";
            this.txtBillDateTo.Size = new System.Drawing.Size(105, 30);
            this.txtBillDateTo.TabIndex = 18;
            this.txtBillDateTo.ValueChanged += new System.EventHandler(this.txtBillDate_ValueChanged);
            // 
            // txtBillDateFrom
            // 
            this.txtBillDateFrom.CanBeEmpty = false;
            this.txtBillDateFrom.Caption = "单据日期";
            this.txtBillDateFrom.CustomFormat = "";
            this.txtBillDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDateFrom.Location = new System.Drawing.Point(92, 8);
            this.txtBillDateFrom.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtBillDateFrom.Name = "txtBillDateFrom";
            this.txtBillDateFrom.Size = new System.Drawing.Size(105, 30);
            this.txtBillDateFrom.TabIndex = 16;
            this.txtBillDateFrom.ValueChanged += new System.EventHandler(this.txtBillDate_ValueChanged);
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(203, 6);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(26, 32);
            this.skinLabel2.TabIndex = 17;
            this.skinLabel2.Text = "至";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CarCode
            // 
            this.CarCode.DataPropertyName = "CarCode";
            this.CarCode.HeaderText = "编码";
            this.CarCode.Name = "CarCode";
            this.CarCode.ReadOnly = true;
            this.CarCode.Width = 70;
            // 
            // CarNum
            // 
            this.CarNum.DataPropertyName = "CarNum";
            this.CarNum.HeaderText = "车牌号码";
            this.CarNum.Name = "CarNum";
            this.CarNum.ReadOnly = true;
            this.CarNum.Width = 120;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "所属客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 120;
            // 
            // DefaultCarWeight
            // 
            this.DefaultCarWeight.DataPropertyName = "DefaultCarWeight";
            this.DefaultCarWeight.HeaderText = "默认皮重";
            this.DefaultCarWeight.Name = "DefaultCarWeight";
            this.DefaultCarWeight.ReadOnly = true;
            // 
            // LastCarWeight
            // 
            this.LastCarWeight.DataPropertyName = "CarWeight";
            this.LastCarWeight.HeaderText = "最新采集皮重";
            this.LastCarWeight.Name = "LastCarWeight";
            this.LastCarWeight.ReadOnly = true;
            this.LastCarWeight.Width = 120;
            // 
            // LastCreateTime
            // 
            this.LastCreateTime.DataPropertyName = "CreateTime";
            this.LastCreateTime.HeaderText = "最近采集时间";
            this.LastCreateTime.Name = "LastCreateTime";
            this.LastCreateTime.ReadOnly = true;
            this.LastCreateTime.Width = 150;
            // 
            // CarDescription
            // 
            this.CarDescription.DataPropertyName = "Description";
            this.CarDescription.HeaderText = "备注";
            this.CarDescription.Name = "CarDescription";
            this.CarDescription.ReadOnly = true;
            this.CarDescription.Width = 80;
            // 
            // frmCarWeightManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctlSearcher1);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "车辆皮重管理";
            this.Name = "frmCarWeightManager";
            this.Size = new System.Drawing.Size(1068, 590);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCarWeight)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnReflesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBMetroComboBox txtSearchDropDown;
        private Controls.LBSkinTextBox txtSearchText;
        private Controls.Searcher.CtlSearcher ctlSearcher1;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LBDataGridView grdCarWeight;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private Controls.LBDateTimeTextBox txtBillDateFrom;
        private Controls.LBDateTimeTextBox txtBillDateTo;
        private System.Windows.Forms.Panel panel1;
        private Controls.LBToolStripButton btnAddCarWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private Controls.LBToolStripButton btnAddCar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultCarWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCarWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarDescription;
    }
}
