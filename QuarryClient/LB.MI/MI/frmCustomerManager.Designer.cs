namespace LB.MI
{
    partial class frmCustomerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomerManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnAdd = new LB.Controls.LBToolStripButton(this.components);
            this.btnOpenEdit = new LB.Controls.LBToolStripButton(this.components);
            this.btnCopy = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new LB.Controls.LBToolStripButton(this.components);
            this.btnImportExcel = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.CustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemainReceivedAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarIsLimit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AmountTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenceNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsForbid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ReceiveTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountNotEnough = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsDisplayAmount = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDisplayPrice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsPrintAmount = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsAllowOverFul = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new LB.Controls.LBSkinButton(this.components);
            this.txtReceiveType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.txtAmountTo = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtAmountFrom = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
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
            this.btnAdd,
            this.btnOpenEdit,
            this.btnCopy,
            this.btnReflesh,
            this.toolStripSeparator1,
            this.btnExportExcel,
            this.btnImportExcel,
            this.toolStripSeparator2});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1001, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 3;
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
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.LBPermissionCode = "DBCustomer_Add";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 37);
            this.btnAdd.Text = "添加新客户";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOpenEdit
            // 
            this.btnOpenEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenEdit.Image")));
            this.btnOpenEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenEdit.LBPermissionCode = "DBCustomer_Update";
            this.btnOpenEdit.Name = "btnOpenEdit";
            this.btnOpenEdit.Size = new System.Drawing.Size(36, 37);
            this.btnOpenEdit.Text = "编辑";
            this.btnOpenEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenEdit.Click += new System.EventHandler(this.btnOpenEdit_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.LBPermissionCode = "DBCustomer_Copy";
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(36, 37);
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
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.LBPermissionCode = "";
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(121, 37);
            this.btnExportExcel.Text = "导出客户资料(Excel)";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.LBPermissionCode = "";
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(121, 37);
            this.btnImportExcel.Text = "导入客户资料(Excel)";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Visible = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
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
            this.CustomerCode,
            this.CustomerName,
            this.RemainReceivedAmount,
            this.Contact,
            this.Phone,
            this.Address,
            this.CarIsLimit,
            this.AmountTypeName,
            this.LicenceNum,
            this.Description,
            this.IsForbid,
            this.ReceiveTypeName,
            this.CreditAmount,
            this.AmountNotEnough,
            this.IsDisplayAmount,
            this.IsDisplayPrice,
            this.IsPrintAmount,
            this.IsAllowOverFul,
            this.CreateBy,
            this.CreateTime,
            this.ChangeBy,
            this.ChangeTime});
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
            this.grdMain.Location = new System.Drawing.Point(0, 86);
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
            this.grdMain.Size = new System.Drawing.Size(1001, 224);
            this.grdMain.TabIndex = 6;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // CustomerCode
            // 
            this.CustomerCode.DataPropertyName = "CustomerCode";
            this.CustomerCode.HeaderText = "编码";
            this.CustomerCode.Name = "CustomerCode";
            this.CustomerCode.ReadOnly = true;
            this.CustomerCode.Width = 80;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // RemainReceivedAmount
            // 
            this.RemainReceivedAmount.DataPropertyName = "RemainReceivedAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.RemainReceivedAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.RemainReceivedAmount.HeaderText = "客户余额";
            this.RemainReceivedAmount.Name = "RemainReceivedAmount";
            this.RemainReceivedAmount.ReadOnly = true;
            // 
            // Contact
            // 
            this.Contact.DataPropertyName = "Contact";
            this.Contact.HeaderText = "联系人名称";
            this.Contact.Name = "Contact";
            this.Contact.ReadOnly = true;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "联系人电话";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "地址";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // CarIsLimit
            // 
            this.CarIsLimit.DataPropertyName = "CarIsLimit";
            this.CarIsLimit.HeaderText = "车辆是否限制";
            this.CarIsLimit.Name = "CarIsLimit";
            this.CarIsLimit.ReadOnly = true;
            this.CarIsLimit.Width = 120;
            // 
            // AmountTypeName
            // 
            this.AmountTypeName.DataPropertyName = "AmountTypeName";
            this.AmountTypeName.HeaderText = "金额格式";
            this.AmountTypeName.Name = "AmountTypeName";
            this.AmountTypeName.ReadOnly = true;
            // 
            // LicenceNum
            // 
            this.LicenceNum.DataPropertyName = "LicenceNum";
            this.LicenceNum.HeaderText = "营业执照号";
            this.LicenceNum.Name = "LicenceNum";
            this.LicenceNum.ReadOnly = true;
            this.LicenceNum.Visible = false;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "备注";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // IsForbid
            // 
            this.IsForbid.DataPropertyName = "IsForbid";
            this.IsForbid.HeaderText = "是否禁用";
            this.IsForbid.Name = "IsForbid";
            this.IsForbid.ReadOnly = true;
            // 
            // ReceiveTypeName
            // 
            this.ReceiveTypeName.DataPropertyName = "ReceiveTypeName";
            this.ReceiveTypeName.HeaderText = "收款方式";
            this.ReceiveTypeName.Name = "ReceiveTypeName";
            this.ReceiveTypeName.ReadOnly = true;
            // 
            // CreditAmount
            // 
            this.CreditAmount.DataPropertyName = "CreditAmount";
            this.CreditAmount.HeaderText = "信用额度";
            this.CreditAmount.Name = "CreditAmount";
            this.CreditAmount.ReadOnly = true;
            // 
            // AmountNotEnough
            // 
            this.AmountNotEnough.DataPropertyName = "AmountNotEnough";
            this.AmountNotEnough.HeaderText = "预警余额";
            this.AmountNotEnough.Name = "AmountNotEnough";
            this.AmountNotEnough.ReadOnly = true;
            // 
            // IsDisplayAmount
            // 
            this.IsDisplayAmount.DataPropertyName = "IsDisplayAmount";
            this.IsDisplayAmount.HeaderText = "显示金额";
            this.IsDisplayAmount.Name = "IsDisplayAmount";
            this.IsDisplayAmount.ReadOnly = true;
            // 
            // IsDisplayPrice
            // 
            this.IsDisplayPrice.DataPropertyName = "IsDisplayPrice";
            this.IsDisplayPrice.HeaderText = "显示单价";
            this.IsDisplayPrice.Name = "IsDisplayPrice";
            this.IsDisplayPrice.ReadOnly = true;
            // 
            // IsPrintAmount
            // 
            this.IsPrintAmount.DataPropertyName = "IsPrintAmount";
            this.IsPrintAmount.HeaderText = "是否打印金额";
            this.IsPrintAmount.Name = "IsPrintAmount";
            this.IsPrintAmount.ReadOnly = true;
            // 
            // IsAllowOverFul
            // 
            this.IsAllowOverFul.DataPropertyName = "IsAllowOverFul";
            this.IsAllowOverFul.HeaderText = "是否允许超额提货";
            this.IsAllowOverFul.Name = "IsAllowOverFul";
            this.IsAllowOverFul.ReadOnly = true;
            // 
            // CreateBy
            // 
            this.CreateBy.DataPropertyName = "CreateBy";
            this.CreateBy.HeaderText = "创建人";
            this.CreateBy.Name = "CreateBy";
            this.CreateBy.ReadOnly = true;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtReceiveType);
            this.panel1.Controls.Add(this.skinLabel11);
            this.panel1.Controls.Add(this.txtAmountTo);
            this.panel1.Controls.Add(this.skinLabel3);
            this.panel1.Controls.Add(this.txtAmountFrom);
            this.panel1.Controls.Add(this.skinLabel2);
            this.panel1.Controls.Add(this.txtCustomerID);
            this.panel1.Controls.Add(this.skinLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 46);
            this.panel1.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BaseColor = System.Drawing.Color.LightGray;
            this.btnSearch.BorderColor = System.Drawing.Color.Gray;
            this.btnSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSearch.DownBack = null;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnSearch.LBPermissionCode = "";
            this.btnSearch.Location = new System.Drawing.Point(884, 10);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(92, 33);
            this.btnSearch.TabIndex = 72;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtReceiveType
            // 
            this.txtReceiveType.CanBeEmpty = false;
            this.txtReceiveType.Caption = "每周";
            this.txtReceiveType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtReceiveType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtReceiveType.DM_UseSelectable = true;
            this.txtReceiveType.FormattingEnabled = true;
            this.txtReceiveType.ItemHeight = 28;
            this.txtReceiveType.Location = new System.Drawing.Point(730, 9);
            this.txtReceiveType.Name = "txtReceiveType";
            this.txtReceiveType.Size = new System.Drawing.Size(131, 34);
            this.txtReceiveType.TabIndex = 70;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel11.Location = new System.Drawing.Point(640, 12);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(84, 27);
            this.skinLabel11.TabIndex = 71;
            this.skinLabel11.Text = "收款方式";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.BackColor = System.Drawing.Color.Transparent;
            this.txtAmountTo.CanBeEmpty = true;
            this.txtAmountTo.Caption = "";
            this.txtAmountTo.DownBack = null;
            this.txtAmountTo.Icon = null;
            this.txtAmountTo.IconIsButton = false;
            this.txtAmountTo.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtAmountTo.IsPasswordChat = '\0';
            this.txtAmountTo.IsSystemPasswordChar = false;
            this.txtAmountTo.Lines = new string[0];
            this.txtAmountTo.Location = new System.Drawing.Point(528, 10);
            this.txtAmountTo.Margin = new System.Windows.Forms.Padding(0);
            this.txtAmountTo.MaxLength = 32767;
            this.txtAmountTo.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtAmountTo.MouseBack = null;
            this.txtAmountTo.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtAmountTo.Multiline = true;
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.NormlBack = null;
            this.txtAmountTo.Padding = new System.Windows.Forms.Padding(5);
            this.txtAmountTo.ReadOnly = false;
            this.txtAmountTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAmountTo.Size = new System.Drawing.Size(85, 29);
            // 
            // 
            // 
            this.txtAmountTo.SkinTxt.AccessibleName = "";
            this.txtAmountTo.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtAmountTo.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtAmountTo.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtAmountTo.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmountTo.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmountTo.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtAmountTo.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtAmountTo.SkinTxt.Multiline = true;
            this.txtAmountTo.SkinTxt.Name = "BaseText";
            this.txtAmountTo.SkinTxt.Size = new System.Drawing.Size(75, 19);
            this.txtAmountTo.SkinTxt.TabIndex = 0;
            this.txtAmountTo.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtAmountTo.SkinTxt.WaterText = "";
            this.txtAmountTo.TabIndex = 68;
            this.txtAmountTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAmountTo.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtAmountTo.WaterText = "";
            this.txtAmountTo.WordWrap = true;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel3.Location = new System.Drawing.Point(502, 13);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(23, 21);
            this.skinLabel3.TabIndex = 67;
            this.skinLabel3.Text = "~";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.BackColor = System.Drawing.Color.Transparent;
            this.txtAmountFrom.CanBeEmpty = true;
            this.txtAmountFrom.Caption = "";
            this.txtAmountFrom.DownBack = null;
            this.txtAmountFrom.Icon = null;
            this.txtAmountFrom.IconIsButton = false;
            this.txtAmountFrom.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtAmountFrom.IsPasswordChat = '\0';
            this.txtAmountFrom.IsSystemPasswordChar = false;
            this.txtAmountFrom.Lines = new string[0];
            this.txtAmountFrom.Location = new System.Drawing.Point(414, 10);
            this.txtAmountFrom.Margin = new System.Windows.Forms.Padding(0);
            this.txtAmountFrom.MaxLength = 32767;
            this.txtAmountFrom.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtAmountFrom.MouseBack = null;
            this.txtAmountFrom.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtAmountFrom.Multiline = true;
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.NormlBack = null;
            this.txtAmountFrom.Padding = new System.Windows.Forms.Padding(5);
            this.txtAmountFrom.ReadOnly = false;
            this.txtAmountFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAmountFrom.Size = new System.Drawing.Size(86, 29);
            // 
            // 
            // 
            this.txtAmountFrom.SkinTxt.AccessibleName = "";
            this.txtAmountFrom.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtAmountFrom.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtAmountFrom.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtAmountFrom.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmountFrom.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmountFrom.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtAmountFrom.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtAmountFrom.SkinTxt.Multiline = true;
            this.txtAmountFrom.SkinTxt.Name = "BaseText";
            this.txtAmountFrom.SkinTxt.Size = new System.Drawing.Size(76, 19);
            this.txtAmountFrom.SkinTxt.TabIndex = 0;
            this.txtAmountFrom.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtAmountFrom.SkinTxt.WaterText = "";
            this.txtAmountFrom.TabIndex = 66;
            this.txtAmountFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAmountFrom.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtAmountFrom.WaterText = "";
            this.txtAmountFrom.WordWrap = true;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel2.Location = new System.Drawing.Point(355, 13);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(56, 21);
            this.skinLabel2.TabIndex = 35;
            this.skinLabel2.Text = "余额";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomerID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCustomerID.CanBeEmpty = true;
            this.txtCustomerID.Caption = "客户名称";
            this.txtCustomerID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCustomerID.LBTitle = "  ";
            this.txtCustomerID.LBTitleVisible = false;
            this.txtCustomerID.Location = new System.Drawing.Point(126, 10);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(222, 29);
            this.txtCustomerID.TabIndex = 34;
            this.txtCustomerID.Load += new System.EventHandler(this.txtCustomerID_Load);
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel1.Location = new System.Drawing.Point(11, 12);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(109, 21);
            this.skinLabel1.TabIndex = 33;
            this.skinLabel1.Text = "客户名称包含";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCustomerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "客户资料管理";
            this.Name = "frmCustomerManager";
            this.Size = new System.Drawing.Size(1001, 310);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnAdd;
        private Controls.LBToolStripButton btnReflesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBToolStripButton btnCopy;
        private Controls.LBToolStripButton btnOpenEdit;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.Panel panel1;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private Controls.LBSkinTextBox txtAmountTo;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private Controls.LBSkinTextBox txtAmountFrom;
        private Controls.LBMetroComboBox txtReceiveType;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private Controls.LBSkinButton btnSearch;
        private Controls.LBToolStripButton btnExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LBToolStripButton btnImportExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainReceivedAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CarIsLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenceNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsForbid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountNotEnough;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDisplayAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDisplayPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrintAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAllowOverFul;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeTime;
    }
}
