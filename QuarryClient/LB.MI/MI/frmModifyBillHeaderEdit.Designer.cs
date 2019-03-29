namespace LB.MI.MI
{
    partial class frmModifyBillHeaderEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyBillHeaderEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnAdd = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.btnCopy = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprove = new LB.Controls.LBToolStripButton(this.components);
            this.btnUnApprove = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new LB.Controls.LBToolStripButton(this.components);
            this.btnUnCancel = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddCar = new LB.Controls.LBToolStripButton(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.txtApproveTime = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtApproveBy = new LB.Controls.LBSkinTextBox(this.components);
            this.txtChangeTime = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtCreateTime = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtChangeBy = new LB.Controls.LBSkinTextBox(this.components);
            this.txtEffectDate = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtBillDate = new LB.Controls.LBDateTimeTextBox(this.components);
            this.txtCreateBy = new LB.Controls.LBSkinTextBox(this.components);
            this.txtDescription = new LB.Controls.LBSkinTextBox(this.components);
            this.txtModifyBillCode = new LB.Controls.LBSkinTextBox(this.components);
            this.ctlFieldHeaderPanel1 = new LB.Controls.CtlFieldHeaderPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.skinToolStrip2 = new CCWin.SkinControl.SkinToolStrip();
            this.btnAddDetail = new LB.Controls.LBToolStripButton(this.components);
            this.btnDeleteDetail = new LB.Controls.LBToolStripButton(this.components);
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalculateType = new LB.Controls.LBDataGridViewComboBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FarePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokerPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CraeteBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinToolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.skinToolStrip2.SuspendLayout();
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
            this.skinToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnAdd,
            this.btnSave,
            this.btnDelete,
            this.btnCopy,
            this.btnReflesh,
            this.toolStripSeparator1,
            this.btnApprove,
            this.btnUnApprove,
            this.toolStripSeparator2,
            this.btnCancel,
            this.btnUnCancel,
            this.toolStripSeparator3,
            this.btnAddCar});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1309, 47);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 5;
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
            this.btnClose.Size = new System.Drawing.Size(43, 44);
            this.btnClose.Text = "关闭";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.LBPermissionCode = "PriceManager_Add";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(73, 44);
            this.btnAdd.Text = "继续添加";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(43, 44);
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "PriceManager_Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(43, 44);
            this.btnDelete.Text = "删除";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.LBPermissionCode = "PriceManager_Copy";
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(43, 44);
            this.btnCopy.Text = "复制";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnReflesh
            // 
            this.btnReflesh.Image = ((System.Drawing.Image)(resources.GetObject("btnReflesh.Image")));
            this.btnReflesh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReflesh.LBPermissionCode = "";
            this.btnReflesh.Name = "btnReflesh";
            this.btnReflesh.Size = new System.Drawing.Size(43, 44);
            this.btnReflesh.Text = "刷新";
            this.btnReflesh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReflesh.Click += new System.EventHandler(this.btnReflesh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // btnApprove
            // 
            this.btnApprove.Image = ((System.Drawing.Image)(resources.GetObject("btnApprove.Image")));
            this.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprove.LBPermissionCode = "PriceManager_Approve";
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(43, 44);
            this.btnApprove.Text = "审核";
            this.btnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnUnApprove
            // 
            this.btnUnApprove.Image = ((System.Drawing.Image)(resources.GetObject("btnUnApprove.Image")));
            this.btnUnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnApprove.LBPermissionCode = "PriceManager_UnApprove";
            this.btnUnApprove.Name = "btnUnApprove";
            this.btnUnApprove.Size = new System.Drawing.Size(73, 44);
            this.btnUnApprove.Text = "取消审核";
            this.btnUnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnApprove.Click += new System.EventHandler(this.btnUnApprove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.LBPermissionCode = "PriceManager_Cancel";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(43, 44);
            this.btnCancel.Text = "作废";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUnCancel
            // 
            this.btnUnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnUnCancel.Image")));
            this.btnUnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnCancel.LBPermissionCode = "PriceManager_UnCancel";
            this.btnUnCancel.Name = "btnUnCancel";
            this.btnUnCancel.Size = new System.Drawing.Size(73, 44);
            this.btnUnCancel.Text = "取消作废";
            this.btnUnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnCancel.Click += new System.EventHandler(this.btnUnCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
            // 
            // btnAddCar
            // 
            this.btnAddCar.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCar.Image")));
            this.btnAddCar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddCar.LBPermissionCode = "DBCar_Add";
            this.btnAddCar.Name = "btnAddCar";
            this.btnAddCar.Size = new System.Drawing.Size(73, 44);
            this.btnAddCar.Text = "添加车辆";
            this.btnAddCar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCustomerID);
            this.panel1.Controls.Add(this.txtApproveTime);
            this.panel1.Controls.Add(this.txtApproveBy);
            this.panel1.Controls.Add(this.txtChangeTime);
            this.panel1.Controls.Add(this.txtCreateTime);
            this.panel1.Controls.Add(this.txtChangeBy);
            this.panel1.Controls.Add(this.txtEffectDate);
            this.panel1.Controls.Add(this.txtBillDate);
            this.panel1.Controls.Add(this.txtCreateBy);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.txtModifyBillCode);
            this.panel1.Controls.Add(this.ctlFieldHeaderPanel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 47);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1309, 269);
            this.panel1.TabIndex = 6;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomerID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCustomerID.CanBeEmpty = false;
            this.txtCustomerID.Caption = "客户名称";
            this.txtCustomerID.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtCustomerID.LBTitle = "  ";
            this.txtCustomerID.LBTitleVisible = false;
            this.txtCustomerID.Location = new System.Drawing.Point(461, 84);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(268, 34);
            this.txtCustomerID.TabIndex = 18;
            // 
            // txtApproveTime
            // 
            this.txtApproveTime.CanBeEmpty = true;
            this.txtApproveTime.Caption = "审核日期";
            this.txtApproveTime.CustomFormat = "  ";
            this.txtApproveTime.Enabled = false;
            this.txtApproveTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtApproveTime.Location = new System.Drawing.Point(411, 224);
            this.txtApproveTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtApproveTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtApproveTime.Name = "txtApproveTime";
            this.txtApproveTime.Size = new System.Drawing.Size(265, 30);
            this.txtApproveTime.TabIndex = 17;
            // 
            // txtApproveBy
            // 
            this.txtApproveBy.BackColor = System.Drawing.Color.Transparent;
            this.txtApproveBy.CanBeEmpty = true;
            this.txtApproveBy.Caption = "审核人";
            this.txtApproveBy.DownBack = null;
            this.txtApproveBy.Icon = null;
            this.txtApproveBy.IconIsButton = false;
            this.txtApproveBy.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtApproveBy.IsPasswordChat = '\0';
            this.txtApproveBy.IsSystemPasswordChar = false;
            this.txtApproveBy.Lines = new string[0];
            this.txtApproveBy.Location = new System.Drawing.Point(125, 218);
            this.txtApproveBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtApproveBy.MaxLength = 32767;
            this.txtApproveBy.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtApproveBy.MouseBack = null;
            this.txtApproveBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtApproveBy.Multiline = true;
            this.txtApproveBy.Name = "txtApproveBy";
            this.txtApproveBy.NormlBack = null;
            this.txtApproveBy.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtApproveBy.ReadOnly = true;
            this.txtApproveBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApproveBy.Size = new System.Drawing.Size(268, 35);
            // 
            // 
            // 
            this.txtApproveBy.SkinTxt.AccessibleName = "";
            this.txtApproveBy.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtApproveBy.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtApproveBy.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtApproveBy.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtApproveBy.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtApproveBy.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtApproveBy.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtApproveBy.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtApproveBy.SkinTxt.Multiline = true;
            this.txtApproveBy.SkinTxt.Name = "BaseText";
            this.txtApproveBy.SkinTxt.ReadOnly = true;
            this.txtApproveBy.SkinTxt.Size = new System.Drawing.Size(254, 23);
            this.txtApproveBy.SkinTxt.TabIndex = 0;
            this.txtApproveBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtApproveBy.SkinTxt.WaterText = "";
            this.txtApproveBy.TabIndex = 16;
            this.txtApproveBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtApproveBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtApproveBy.WaterText = "";
            this.txtApproveBy.WordWrap = true;
            // 
            // txtChangeTime
            // 
            this.txtChangeTime.CanBeEmpty = true;
            this.txtChangeTime.Caption = "修改日期";
            this.txtChangeTime.CustomFormat = " ";
            this.txtChangeTime.Enabled = false;
            this.txtChangeTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtChangeTime.Location = new System.Drawing.Point(979, 179);
            this.txtChangeTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtChangeTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtChangeTime.Name = "txtChangeTime";
            this.txtChangeTime.Size = new System.Drawing.Size(265, 30);
            this.txtChangeTime.TabIndex = 15;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.CanBeEmpty = true;
            this.txtCreateTime.Caption = "制单日期";
            this.txtCreateTime.CustomFormat = " ";
            this.txtCreateTime.Enabled = false;
            this.txtCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCreateTime.Location = new System.Drawing.Point(411, 179);
            this.txtCreateTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCreateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.Size = new System.Drawing.Size(265, 30);
            this.txtCreateTime.TabIndex = 14;
            // 
            // txtChangeBy
            // 
            this.txtChangeBy.BackColor = System.Drawing.Color.Transparent;
            this.txtChangeBy.CanBeEmpty = true;
            this.txtChangeBy.Caption = "修改人";
            this.txtChangeBy.DownBack = null;
            this.txtChangeBy.Icon = null;
            this.txtChangeBy.IconIsButton = false;
            this.txtChangeBy.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChangeBy.IsPasswordChat = '\0';
            this.txtChangeBy.IsSystemPasswordChar = false;
            this.txtChangeBy.Lines = new string[0];
            this.txtChangeBy.Location = new System.Drawing.Point(681, 179);
            this.txtChangeBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangeBy.MaxLength = 32767;
            this.txtChangeBy.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtChangeBy.MouseBack = null;
            this.txtChangeBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChangeBy.Multiline = true;
            this.txtChangeBy.Name = "txtChangeBy";
            this.txtChangeBy.NormlBack = null;
            this.txtChangeBy.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtChangeBy.ReadOnly = true;
            this.txtChangeBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtChangeBy.Size = new System.Drawing.Size(268, 35);
            // 
            // 
            // 
            this.txtChangeBy.SkinTxt.AccessibleName = "";
            this.txtChangeBy.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtChangeBy.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtChangeBy.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtChangeBy.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChangeBy.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChangeBy.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtChangeBy.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtChangeBy.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtChangeBy.SkinTxt.Multiline = true;
            this.txtChangeBy.SkinTxt.Name = "BaseText";
            this.txtChangeBy.SkinTxt.ReadOnly = true;
            this.txtChangeBy.SkinTxt.Size = new System.Drawing.Size(254, 23);
            this.txtChangeBy.SkinTxt.TabIndex = 0;
            this.txtChangeBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeBy.SkinTxt.WaterText = "";
            this.txtChangeBy.TabIndex = 15;
            this.txtChangeBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChangeBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeBy.WaterText = "";
            this.txtChangeBy.WordWrap = true;
            // 
            // txtEffectDate
            // 
            this.txtEffectDate.CanBeEmpty = false;
            this.txtEffectDate.Caption = "生效日期";
            this.txtEffectDate.CustomFormat = "  ";
            this.txtEffectDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEffectDate.Location = new System.Drawing.Point(841, 112);
            this.txtEffectDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEffectDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtEffectDate.Name = "txtEffectDate";
            this.txtEffectDate.Size = new System.Drawing.Size(265, 30);
            this.txtEffectDate.TabIndex = 14;
            // 
            // txtBillDate
            // 
            this.txtBillDate.CanBeEmpty = false;
            this.txtBillDate.Caption = "单据日期";
            this.txtBillDate.CustomFormat = "";
            this.txtBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDate.Location = new System.Drawing.Point(841, 68);
            this.txtBillDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBillDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtBillDate.Name = "txtBillDate";
            this.txtBillDate.Size = new System.Drawing.Size(265, 30);
            this.txtBillDate.TabIndex = 13;
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.BackColor = System.Drawing.Color.Transparent;
            this.txtCreateBy.CanBeEmpty = true;
            this.txtCreateBy.Caption = "制单人";
            this.txtCreateBy.DownBack = null;
            this.txtCreateBy.Icon = null;
            this.txtCreateBy.IconIsButton = false;
            this.txtCreateBy.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtCreateBy.IsPasswordChat = '\0';
            this.txtCreateBy.IsSystemPasswordChar = false;
            this.txtCreateBy.Lines = new string[0];
            this.txtCreateBy.Location = new System.Drawing.Point(125, 179);
            this.txtCreateBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtCreateBy.MaxLength = 32767;
            this.txtCreateBy.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtCreateBy.MouseBack = null;
            this.txtCreateBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtCreateBy.Multiline = true;
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.NormlBack = null;
            this.txtCreateBy.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtCreateBy.ReadOnly = true;
            this.txtCreateBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCreateBy.Size = new System.Drawing.Size(268, 35);
            // 
            // 
            // 
            this.txtCreateBy.SkinTxt.AccessibleName = "";
            this.txtCreateBy.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtCreateBy.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtCreateBy.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCreateBy.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCreateBy.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreateBy.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtCreateBy.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtCreateBy.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCreateBy.SkinTxt.Multiline = true;
            this.txtCreateBy.SkinTxt.Name = "BaseText";
            this.txtCreateBy.SkinTxt.ReadOnly = true;
            this.txtCreateBy.SkinTxt.Size = new System.Drawing.Size(254, 23);
            this.txtCreateBy.SkinTxt.TabIndex = 0;
            this.txtCreateBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtCreateBy.SkinTxt.WaterText = "";
            this.txtCreateBy.TabIndex = 11;
            this.txtCreateBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCreateBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtCreateBy.WaterText = "";
            this.txtCreateBy.WordWrap = true;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.Transparent;
            this.txtDescription.CanBeEmpty = true;
            this.txtDescription.Caption = "备注";
            this.txtDescription.DownBack = null;
            this.txtDescription.Icon = null;
            this.txtDescription.IconIsButton = false;
            this.txtDescription.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtDescription.IsPasswordChat = '\0';
            this.txtDescription.IsSystemPasswordChar = false;
            this.txtDescription.Lines = new string[0];
            this.txtDescription.Location = new System.Drawing.Point(125, 130);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0);
            this.txtDescription.MaxLength = 32767;
            this.txtDescription.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtDescription.MouseBack = null;
            this.txtDescription.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.NormlBack = null;
            this.txtDescription.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtDescription.ReadOnly = false;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescription.Size = new System.Drawing.Size(268, 35);
            // 
            // 
            // 
            this.txtDescription.SkinTxt.AccessibleName = "";
            this.txtDescription.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtDescription.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtDescription.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDescription.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtDescription.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtDescription.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescription.SkinTxt.Multiline = true;
            this.txtDescription.SkinTxt.Name = "BaseText";
            this.txtDescription.SkinTxt.Size = new System.Drawing.Size(254, 23);
            this.txtDescription.SkinTxt.TabIndex = 0;
            this.txtDescription.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.SkinTxt.WaterText = "";
            this.txtDescription.TabIndex = 12;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescription.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.WaterText = "";
            this.txtDescription.WordWrap = true;
            // 
            // txtModifyBillCode
            // 
            this.txtModifyBillCode.BackColor = System.Drawing.Color.Transparent;
            this.txtModifyBillCode.CanBeEmpty = true;
            this.txtModifyBillCode.Caption = "调价单编码";
            this.txtModifyBillCode.DownBack = null;
            this.txtModifyBillCode.Icon = null;
            this.txtModifyBillCode.IconIsButton = false;
            this.txtModifyBillCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtModifyBillCode.IsPasswordChat = '\0';
            this.txtModifyBillCode.IsSystemPasswordChar = false;
            this.txtModifyBillCode.Lines = new string[0];
            this.txtModifyBillCode.Location = new System.Drawing.Point(125, 84);
            this.txtModifyBillCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtModifyBillCode.MaxLength = 32767;
            this.txtModifyBillCode.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtModifyBillCode.MouseBack = null;
            this.txtModifyBillCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtModifyBillCode.Multiline = true;
            this.txtModifyBillCode.Name = "txtModifyBillCode";
            this.txtModifyBillCode.NormlBack = null;
            this.txtModifyBillCode.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtModifyBillCode.ReadOnly = true;
            this.txtModifyBillCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtModifyBillCode.Size = new System.Drawing.Size(268, 35);
            // 
            // 
            // 
            this.txtModifyBillCode.SkinTxt.AccessibleName = "";
            this.txtModifyBillCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtModifyBillCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtModifyBillCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtModifyBillCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtModifyBillCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModifyBillCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtModifyBillCode.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtModifyBillCode.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtModifyBillCode.SkinTxt.Multiline = true;
            this.txtModifyBillCode.SkinTxt.Name = "BaseText";
            this.txtModifyBillCode.SkinTxt.ReadOnly = true;
            this.txtModifyBillCode.SkinTxt.Size = new System.Drawing.Size(254, 23);
            this.txtModifyBillCode.SkinTxt.TabIndex = 0;
            this.txtModifyBillCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtModifyBillCode.SkinTxt.WaterText = "";
            this.txtModifyBillCode.TabIndex = 10;
            this.txtModifyBillCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtModifyBillCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtModifyBillCode.WaterText = "";
            this.txtModifyBillCode.WordWrap = true;
            // 
            // ctlFieldHeaderPanel1
            // 
            this.ctlFieldHeaderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlFieldHeaderPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ctlFieldHeaderPanel1.LBColumnCount = 4;
            this.ctlFieldHeaderPanel1.LBControlHorSpace = 10;
            this.ctlFieldHeaderPanel1.LBHorSpace = 20;
            this.ctlFieldHeaderPanel1.LBRowCount = 4;
            this.ctlFieldHeaderPanel1.Location = new System.Drawing.Point(0, 64);
            this.ctlFieldHeaderPanel1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ctlFieldHeaderPanel1.Name = "ctlFieldHeaderPanel1";
            this.ctlFieldHeaderPanel1.Size = new System.Drawing.Size(1309, 205);
            this.ctlFieldHeaderPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1309, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "销售调价单";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.ItemCode,
            this.ItemName,
            this.ItemMode,
            this.UOMName,
            this.ItemRate,
            this.CalculateType,
            this.CarNum,
            this.CarID,
            this.Price,
            this.MaterialPrice,
            this.FarePrice,
            this.TaxPrice,
            this.BrokerPrice,
            this.Description,
            this.CraeteBy,
            this.CreateTime,
            this.ChangeBy,
            this.ChangeTime});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle8;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = null;
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 350);
            this.grdMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdMain.Name = "grdMain";
            this.grdMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.grdMain.RowTemplate.Height = 23;
            this.grdMain.Size = new System.Drawing.Size(1309, 164);
            this.grdMain.TabIndex = 9;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            this.grdMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMain_CellValueChanged);
            // 
            // skinToolStrip2
            // 
            this.skinToolStrip2.Arrow = System.Drawing.Color.Black;
            this.skinToolStrip2.AutoSize = false;
            this.skinToolStrip2.Back = System.Drawing.Color.White;
            this.skinToolStrip2.BackRadius = 4;
            this.skinToolStrip2.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinToolStrip2.Base = System.Drawing.Color.Gainsboro;
            this.skinToolStrip2.BaseFore = System.Drawing.Color.Black;
            this.skinToolStrip2.BaseForeAnamorphosis = false;
            this.skinToolStrip2.BaseForeAnamorphosisBorder = 4;
            this.skinToolStrip2.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinToolStrip2.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip2.BaseHoverFore = System.Drawing.Color.White;
            this.skinToolStrip2.BaseItemAnamorphosis = true;
            this.skinToolStrip2.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.BaseItemBorderShow = true;
            this.skinToolStrip2.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinToolStrip2.BaseItemDown")));
            this.skinToolStrip2.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinToolStrip2.BaseItemMouse")));
            this.skinToolStrip2.BaseItemNorml = null;
            this.skinToolStrip2.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.BaseItemRadius = 4;
            this.skinToolStrip2.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip2.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.BindTabControl = null;
            this.skinToolStrip2.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip2.Fore = System.Drawing.Color.Black;
            this.skinToolStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip2.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.skinToolStrip2.ItemAnamorphosis = true;
            this.skinToolStrip2.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.ItemBorderShow = true;
            this.skinToolStrip2.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip2.ItemRadius = 4;
            this.skinToolStrip2.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddDetail,
            this.btnDeleteDetail});
            this.skinToolStrip2.Location = new System.Drawing.Point(0, 316);
            this.skinToolStrip2.Name = "skinToolStrip2";
            this.skinToolStrip2.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip2.Size = new System.Drawing.Size(1309, 34);
            this.skinToolStrip2.SkinAllColor = true;
            this.skinToolStrip2.TabIndex = 10;
            this.skinToolStrip2.Text = "skinToolStrip2";
            this.skinToolStrip2.TitleAnamorphosis = true;
            this.skinToolStrip2.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip2.TitleRadius = 4;
            this.skinToolStrip2.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAddDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDetail.Image")));
            this.btnAddDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddDetail.LBPermissionCode = "";
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(119, 31);
            this.btnAddDetail.Text = "添加明细行";
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDeleteDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDetail.Image")));
            this.btnDeleteDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteDetail.LBPermissionCode = "";
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(119, 31);
            this.btnDeleteDetail.Text = "添加明细行";
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.Visible = false;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "物料编码";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "物料名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // ItemMode
            // 
            this.ItemMode.DataPropertyName = "ItemMode";
            this.ItemMode.HeaderText = "物料规格";
            this.ItemMode.Name = "ItemMode";
            this.ItemMode.ReadOnly = true;
            // 
            // UOMName
            // 
            this.UOMName.DataPropertyName = "UOMName";
            this.UOMName.HeaderText = "单位";
            this.UOMName.Name = "UOMName";
            this.UOMName.ReadOnly = true;
            // 
            // ItemRate
            // 
            this.ItemRate.DataPropertyName = "ItemRate";
            this.ItemRate.HeaderText = "比重(KG/m2)";
            this.ItemRate.Name = "ItemRate";
            this.ItemRate.ReadOnly = true;
            this.ItemRate.Width = 120;
            // 
            // CalculateType
            // 
            this.CalculateType.DataPropertyName = "CalculateType";
            this.CalculateType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CalculateType.FieldName = "CalculateType";
            this.CalculateType.HeaderText = "计价方式";
            this.CalculateType.Name = "CalculateType";
            // 
            // CarNum
            // 
            this.CarNum.DataPropertyName = "CarNum";
            this.CarNum.HeaderText = "车号";
            this.CarNum.Name = "CarNum";
            this.CarNum.ReadOnly = true;
            this.CarNum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CarNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CarID
            // 
            this.CarID.DataPropertyName = "CarID";
            this.CarID.HeaderText = "CarID";
            this.CarID.Name = "CarID";
            this.CarID.Visible = false;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle3.Format = "N5";
            dataGridViewCellStyle3.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle3;
            this.Price.HeaderText = "单价(总)";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 120;
            // 
            // MaterialPrice
            // 
            this.MaterialPrice.DataPropertyName = "MaterialPrice";
            dataGridViewCellStyle4.Format = "N5";
            this.MaterialPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.MaterialPrice.HeaderText = "石价";
            this.MaterialPrice.Name = "MaterialPrice";
            this.MaterialPrice.Width = 80;
            // 
            // FarePrice
            // 
            this.FarePrice.DataPropertyName = "FarePrice";
            dataGridViewCellStyle5.Format = "N5";
            this.FarePrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.FarePrice.HeaderText = "运费";
            this.FarePrice.Name = "FarePrice";
            this.FarePrice.Width = 80;
            // 
            // TaxPrice
            // 
            this.TaxPrice.DataPropertyName = "TaxPrice";
            dataGridViewCellStyle6.Format = "N5";
            this.TaxPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.TaxPrice.HeaderText = "税金";
            this.TaxPrice.Name = "TaxPrice";
            this.TaxPrice.Width = 80;
            // 
            // BrokerPrice
            // 
            this.BrokerPrice.DataPropertyName = "BrokerPrice";
            dataGridViewCellStyle7.Format = "N5";
            this.BrokerPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.BrokerPrice.HeaderText = "佣金";
            this.BrokerPrice.Name = "BrokerPrice";
            this.BrokerPrice.Width = 80;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "备注";
            this.Description.Name = "Description";
            // 
            // CraeteBy
            // 
            this.CraeteBy.DataPropertyName = "CraeteBy";
            this.CraeteBy.HeaderText = "创建人";
            this.CraeteBy.Name = "CraeteBy";
            this.CraeteBy.ReadOnly = true;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            // 
            // ChangeBy
            // 
            this.ChangeBy.DataPropertyName = "ChangeBy";
            this.ChangeBy.HeaderText = "修改人";
            this.ChangeBy.Name = "ChangeBy";
            this.ChangeBy.ReadOnly = true;
            // 
            // ChangeTime
            // 
            this.ChangeTime.DataPropertyName = "ChangeTime";
            this.ChangeTime.HeaderText = "修改时间";
            this.ChangeTime.Name = "ChangeTime";
            this.ChangeTime.ReadOnly = true;
            // 
            // frmModifyBillHeaderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.skinToolStrip2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinToolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmModifyBillHeaderEdit";
            this.Size = new System.Drawing.Size(1309, 514);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.skinToolStrip2.ResumeLayout(false);
            this.skinToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnAdd;
        private Controls.LBToolStripButton btnSave;
        private Controls.LBToolStripButton btnDelete;
        private Controls.LBToolStripButton btnCopy;
        private Controls.LBToolStripButton btnReflesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBToolStripButton btnAddCar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Controls.CtlFieldHeaderPanel ctlFieldHeaderPanel1;
        private Controls.LBSkinTextBox txtCreateBy;
        private Controls.LBSkinTextBox txtDescription;
        private Controls.LBSkinTextBox txtModifyBillCode;
        private Controls.LBDateTimeTextBox txtBillDate;
        private Controls.LBDateTimeTextBox txtEffectDate;
        private Controls.LBSkinTextBox txtChangeBy;
        private Controls.LBDateTimeTextBox txtCreateTime;
        private Controls.LBDateTimeTextBox txtChangeTime;
        private Controls.LBSkinTextBox txtApproveBy;
        private Controls.LBDateTimeTextBox txtApproveTime;
        private Controls.LBDataGridView grdMain;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip2;
        private Controls.LBToolStripButton btnAddDetail;
        private Controls.LBToolStripButton btnDeleteDetail;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private Controls.LBToolStripButton btnApprove;
        private Controls.LBToolStripButton btnUnApprove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LBToolStripButton btnCancel;
        private Controls.LBToolStripButton btnUnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemRate;
        private Controls.LBDataGridViewComboBoxColumn CalculateType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn FarePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokerPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn CraeteBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeTime;
    }
}
