namespace LB.RPReceive.RPReceive
{
    partial class frmReceiveBillQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceiveBillQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnAdd = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtApproveBy = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.btnSearch = new LB.Controls.LBSkinButton(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbUnApprove = new System.Windows.Forms.RadioButton();
            this.rbApproved = new System.Windows.Forms.RadioButton();
            this.rbApproveAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCanceled = new System.Windows.Forms.RadioButton();
            this.rbUnCancel = new System.Windows.Forms.RadioButton();
            this.rbCancelAll = new System.Windows.Forms.RadioButton();
            this.txtBillCoding = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtAmountTo = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtAmountFrom = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtBillTimeTo = new System.Windows.Forms.DateTimePicker();
            this.txtBillDateTo = new System.Windows.Forms.DateTimePicker();
            this.txtBillTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.txtBillDateFrom = new System.Windows.Forms.DateTimePicker();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtRPReceiveType = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel13 = new CCWin.SkinControl.SkinLabel();
            this.ReceiveBillCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApproveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiveAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsApprove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsCancel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChangedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApproveBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.skinToolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiveBillCode,
            this.ApproveTime,
            this.CustomerName,
            this.ReceiveAmount,
            this.ChargeTypeName,
            this.BankName,
            this.Description,
            this.IsApprove,
            this.IsCancel,
            this.ChangedBy,
            this.ChangeTime,
            this.ApproveBy,
            this.BillDate});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = null;
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 181);
            this.grdMain.Name = "grdMain";
            this.grdMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdMain.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.grdMain.RowTemplate.Height = 23;
            this.grdMain.Size = new System.Drawing.Size(1092, 191);
            this.grdMain.TabIndex = 3;
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
            this.btnAdd,
            this.btnReflesh,
            this.toolStripSeparator1});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1092, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 2;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LB.RPReceive.Properties.Resources.btnClose;
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
            this.btnAdd.Image = global::LB.RPReceive.Properties.Resources.btnOpenAdd;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.LBPermissionCode = "RPReceive_Add";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 37);
            this.btnAdd.Text = "添加充值单";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.txtRPReceiveType);
            this.panel1.Controls.Add(this.skinLabel13);
            this.panel1.Controls.Add(this.txtApproveBy);
            this.panel1.Controls.Add(this.skinLabel7);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txtBillCoding);
            this.panel1.Controls.Add(this.skinLabel5);
            this.panel1.Controls.Add(this.skinLabel4);
            this.panel1.Controls.Add(this.txtAmountTo);
            this.panel1.Controls.Add(this.skinLabel3);
            this.panel1.Controls.Add(this.txtAmountFrom);
            this.panel1.Controls.Add(this.skinLabel2);
            this.panel1.Controls.Add(this.txtBillTimeTo);
            this.panel1.Controls.Add(this.txtBillDateTo);
            this.panel1.Controls.Add(this.txtBillTimeFrom);
            this.panel1.Controls.Add(this.txtBillDateFrom);
            this.panel1.Controls.Add(this.skinLabel6);
            this.panel1.Controls.Add(this.txtCustomerID);
            this.panel1.Controls.Add(this.skinLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 141);
            this.panel1.TabIndex = 5;
            // 
            // txtApproveBy
            // 
            this.txtApproveBy.BackColor = System.Drawing.Color.Transparent;
            this.txtApproveBy.CanBeEmpty = true;
            this.txtApproveBy.Caption = "";
            this.txtApproveBy.DownBack = null;
            this.txtApproveBy.Icon = null;
            this.txtApproveBy.IconIsButton = false;
            this.txtApproveBy.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtApproveBy.IsPasswordChat = '\0';
            this.txtApproveBy.IsSystemPasswordChar = false;
            this.txtApproveBy.Lines = new string[0];
            this.txtApproveBy.Location = new System.Drawing.Point(737, 49);
            this.txtApproveBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtApproveBy.MaxLength = 32767;
            this.txtApproveBy.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtApproveBy.MouseBack = null;
            this.txtApproveBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtApproveBy.Multiline = true;
            this.txtApproveBy.Name = "txtApproveBy";
            this.txtApproveBy.NormlBack = null;
            this.txtApproveBy.Padding = new System.Windows.Forms.Padding(5);
            this.txtApproveBy.ReadOnly = false;
            this.txtApproveBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtApproveBy.Size = new System.Drawing.Size(125, 29);
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
            this.txtApproveBy.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtApproveBy.SkinTxt.Multiline = true;
            this.txtApproveBy.SkinTxt.Name = "BaseText";
            this.txtApproveBy.SkinTxt.Size = new System.Drawing.Size(115, 19);
            this.txtApproveBy.SkinTxt.TabIndex = 0;
            this.txtApproveBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtApproveBy.SkinTxt.WaterText = "";
            this.txtApproveBy.TabIndex = 73;
            this.txtApproveBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtApproveBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtApproveBy.WaterText = "";
            this.txtApproveBy.WordWrap = true;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel7.Location = new System.Drawing.Point(645, 52);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(89, 21);
            this.skinLabel7.TabIndex = 72;
            this.skinLabel7.Text = "审核人";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnSearch.Location = new System.Drawing.Point(599, 89);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(92, 44);
            this.btnSearch.TabIndex = 71;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbUnApprove);
            this.groupBox3.Controls.Add(this.rbApproved);
            this.groupBox3.Controls.Add(this.rbApproveAll);
            this.groupBox3.Location = new System.Drawing.Point(306, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 52);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "审核状态";
            // 
            // rbUnApprove
            // 
            this.rbUnApprove.AutoSize = true;
            this.rbUnApprove.Location = new System.Drawing.Point(194, 20);
            this.rbUnApprove.Name = "rbUnApprove";
            this.rbUnApprove.Size = new System.Drawing.Size(83, 16);
            this.rbUnApprove.TabIndex = 2;
            this.rbUnApprove.Text = "未审核记录";
            this.rbUnApprove.UseVisualStyleBackColor = true;
            // 
            // rbApproved
            // 
            this.rbApproved.AutoSize = true;
            this.rbApproved.Checked = true;
            this.rbApproved.Location = new System.Drawing.Point(102, 20);
            this.rbApproved.Name = "rbApproved";
            this.rbApproved.Size = new System.Drawing.Size(83, 16);
            this.rbApproved.TabIndex = 1;
            this.rbApproved.TabStop = true;
            this.rbApproved.Text = "已审核记录";
            this.rbApproved.UseVisualStyleBackColor = true;
            // 
            // rbApproveAll
            // 
            this.rbApproveAll.AutoSize = true;
            this.rbApproveAll.Location = new System.Drawing.Point(16, 20);
            this.rbApproveAll.Name = "rbApproveAll";
            this.rbApproveAll.Size = new System.Drawing.Size(71, 16);
            this.rbApproveAll.TabIndex = 0;
            this.rbApproveAll.Text = "全部记录";
            this.rbApproveAll.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCanceled);
            this.groupBox2.Controls.Add(this.rbUnCancel);
            this.groupBox2.Controls.Add(this.rbCancelAll);
            this.groupBox2.Location = new System.Drawing.Point(18, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 52);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "作废状态";
            // 
            // rbCanceled
            // 
            this.rbCanceled.AutoSize = true;
            this.rbCanceled.Location = new System.Drawing.Point(189, 20);
            this.rbCanceled.Name = "rbCanceled";
            this.rbCanceled.Size = new System.Drawing.Size(83, 16);
            this.rbCanceled.TabIndex = 2;
            this.rbCanceled.Text = "已作废记录";
            this.rbCanceled.UseVisualStyleBackColor = true;
            // 
            // rbUnCancel
            // 
            this.rbUnCancel.AutoSize = true;
            this.rbUnCancel.Location = new System.Drawing.Point(101, 20);
            this.rbUnCancel.Name = "rbUnCancel";
            this.rbUnCancel.Size = new System.Drawing.Size(83, 16);
            this.rbUnCancel.TabIndex = 1;
            this.rbUnCancel.Text = "未作废记录";
            this.rbUnCancel.UseVisualStyleBackColor = true;
            // 
            // rbCancelAll
            // 
            this.rbCancelAll.AutoSize = true;
            this.rbCancelAll.Checked = true;
            this.rbCancelAll.Location = new System.Drawing.Point(16, 20);
            this.rbCancelAll.Name = "rbCancelAll";
            this.rbCancelAll.Size = new System.Drawing.Size(71, 16);
            this.rbCancelAll.TabIndex = 0;
            this.rbCancelAll.TabStop = true;
            this.rbCancelAll.Text = "全部记录";
            this.rbCancelAll.UseVisualStyleBackColor = true;
            // 
            // txtBillCoding
            // 
            this.txtBillCoding.BackColor = System.Drawing.Color.Transparent;
            this.txtBillCoding.CanBeEmpty = true;
            this.txtBillCoding.Caption = "";
            this.txtBillCoding.DownBack = null;
            this.txtBillCoding.Icon = null;
            this.txtBillCoding.IconIsButton = false;
            this.txtBillCoding.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCoding.IsPasswordChat = '\0';
            this.txtBillCoding.IsSystemPasswordChar = false;
            this.txtBillCoding.Lines = new string[0];
            this.txtBillCoding.Location = new System.Drawing.Point(737, 10);
            this.txtBillCoding.Margin = new System.Windows.Forms.Padding(0);
            this.txtBillCoding.MaxLength = 32767;
            this.txtBillCoding.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtBillCoding.MouseBack = null;
            this.txtBillCoding.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCoding.Multiline = true;
            this.txtBillCoding.Name = "txtBillCoding";
            this.txtBillCoding.NormlBack = null;
            this.txtBillCoding.Padding = new System.Windows.Forms.Padding(5);
            this.txtBillCoding.ReadOnly = false;
            this.txtBillCoding.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBillCoding.Size = new System.Drawing.Size(125, 29);
            // 
            // 
            // 
            this.txtBillCoding.SkinTxt.AccessibleName = "";
            this.txtBillCoding.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtBillCoding.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBillCoding.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillCoding.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillCoding.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillCoding.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtBillCoding.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtBillCoding.SkinTxt.Multiline = true;
            this.txtBillCoding.SkinTxt.Name = "BaseText";
            this.txtBillCoding.SkinTxt.Size = new System.Drawing.Size(115, 19);
            this.txtBillCoding.SkinTxt.TabIndex = 0;
            this.txtBillCoding.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCoding.SkinTxt.WaterText = "";
            this.txtBillCoding.TabIndex = 68;
            this.txtBillCoding.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBillCoding.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCoding.WaterText = "";
            this.txtBillCoding.WordWrap = true;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel5.Location = new System.Drawing.Point(645, 13);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(89, 21);
            this.skinLabel5.TabIndex = 67;
            this.skinLabel5.Text = "充值单号";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel4.Location = new System.Drawing.Point(0, 51);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(161, 21);
            this.skinLabel4.TabIndex = 66;
            this.skinLabel4.Text = "充值日期范围";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtAmountTo.Location = new System.Drawing.Point(555, 10);
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
            this.txtAmountTo.TabIndex = 65;
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
            this.skinLabel3.Location = new System.Drawing.Point(529, 13);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(23, 21);
            this.skinLabel3.TabIndex = 64;
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
            this.txtAmountFrom.Location = new System.Drawing.Point(441, 10);
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
            this.txtAmountFrom.TabIndex = 63;
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
            this.skinLabel2.Location = new System.Drawing.Point(305, 13);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(113, 21);
            this.skinLabel2.TabIndex = 62;
            this.skinLabel2.Text = "充值金额范围";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillTimeTo
            // 
            this.txtBillTimeTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillTimeTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtBillTimeTo.Location = new System.Drawing.Point(534, 49);
            this.txtBillTimeTo.Name = "txtBillTimeTo";
            this.txtBillTimeTo.Size = new System.Drawing.Size(106, 26);
            this.txtBillTimeTo.TabIndex = 61;
            // 
            // txtBillDateTo
            // 
            this.txtBillDateTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillDateTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDateTo.Location = new System.Drawing.Point(413, 49);
            this.txtBillDateTo.Name = "txtBillDateTo";
            this.txtBillDateTo.Size = new System.Drawing.Size(115, 26);
            this.txtBillDateTo.TabIndex = 60;
            // 
            // txtBillTimeFrom
            // 
            this.txtBillTimeFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillTimeFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtBillTimeFrom.Location = new System.Drawing.Point(279, 49);
            this.txtBillTimeFrom.Name = "txtBillTimeFrom";
            this.txtBillTimeFrom.Size = new System.Drawing.Size(99, 26);
            this.txtBillTimeFrom.TabIndex = 59;
            // 
            // txtBillDateFrom
            // 
            this.txtBillDateFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillDateFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDateFrom.Location = new System.Drawing.Point(167, 49);
            this.txtBillDateFrom.Name = "txtBillDateFrom";
            this.txtBillDateFrom.Size = new System.Drawing.Size(106, 26);
            this.txtBillDateFrom.TabIndex = 58;
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel6.Location = new System.Drawing.Point(379, 52);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(28, 21);
            this.skinLabel6.TabIndex = 57;
            this.skinLabel6.Text = "至";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtCustomerID.Location = new System.Drawing.Point(167, 10);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(135, 29);
            this.txtCustomerID.TabIndex = 32;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel1.Location = new System.Drawing.Point(14, 13);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(120, 21);
            this.skinLabel1.TabIndex = 31;
            this.skinLabel1.Text = "客户名称包含";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRPReceiveType
            // 
            this.txtRPReceiveType.BackColor = System.Drawing.Color.Transparent;
            this.txtRPReceiveType.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtRPReceiveType.CanBeEmpty = true;
            this.txtRPReceiveType.Caption = "";
            this.txtRPReceiveType.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtRPReceiveType.LBTitle = "  ";
            this.txtRPReceiveType.LBTitleVisible = false;
            this.txtRPReceiveType.Location = new System.Drawing.Point(953, 9);
            this.txtRPReceiveType.Margin = new System.Windows.Forms.Padding(0);
            this.txtRPReceiveType.Name = "txtRPReceiveType";
            this.txtRPReceiveType.PopupWidth = 120;
            this.txtRPReceiveType.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtRPReceiveType.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtRPReceiveType.Size = new System.Drawing.Size(134, 34);
            this.txtRPReceiveType.TabIndex = 75;
            // 
            // skinLabel13
            // 
            this.skinLabel13.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel13.BorderColor = System.Drawing.Color.White;
            this.skinLabel13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel13.Location = new System.Drawing.Point(867, 9);
            this.skinLabel13.Name = "skinLabel13";
            this.skinLabel13.Size = new System.Drawing.Size(83, 32);
            this.skinLabel13.TabIndex = 74;
            this.skinLabel13.Text = "充值方式";
            this.skinLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReceiveBillCode
            // 
            this.ReceiveBillCode.DataPropertyName = "ReceiveBillCode";
            this.ReceiveBillCode.HeaderText = "充值单号";
            this.ReceiveBillCode.Name = "ReceiveBillCode";
            this.ReceiveBillCode.ReadOnly = true;
            this.ReceiveBillCode.Width = 120;
            // 
            // ApproveTime
            // 
            this.ApproveTime.DataPropertyName = "ApproveTime";
            this.ApproveTime.HeaderText = "审核时间";
            this.ApproveTime.Name = "ApproveTime";
            this.ApproveTime.ReadOnly = true;
            this.ApproveTime.Width = 120;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.CustomerName.DefaultCellStyle = dataGridViewCellStyle3;
            this.CustomerName.HeaderText = "客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ReceiveAmount
            // 
            this.ReceiveAmount.DataPropertyName = "ReceiveAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.ReceiveAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ReceiveAmount.HeaderText = "充值金额";
            this.ReceiveAmount.Name = "ReceiveAmount";
            this.ReceiveAmount.ReadOnly = true;
            // 
            // ChargeTypeName
            // 
            this.ChargeTypeName.DataPropertyName = "ChargeTypeName";
            this.ChargeTypeName.HeaderText = "充值方式";
            this.ChargeTypeName.Name = "ChargeTypeName";
            this.ChargeTypeName.ReadOnly = true;
            this.ChargeTypeName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChargeTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BankName
            // 
            this.BankName.DataPropertyName = "BankName";
            this.BankName.HeaderText = "收款银行";
            this.BankName.Name = "BankName";
            this.BankName.ReadOnly = true;
            this.BankName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BankName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "备注";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 150;
            // 
            // IsApprove
            // 
            this.IsApprove.DataPropertyName = "IsApprove";
            this.IsApprove.HeaderText = "已审核";
            this.IsApprove.Name = "IsApprove";
            this.IsApprove.ReadOnly = true;
            this.IsApprove.Width = 80;
            // 
            // IsCancel
            // 
            this.IsCancel.DataPropertyName = "IsCancel";
            this.IsCancel.HeaderText = "已作废";
            this.IsCancel.Name = "IsCancel";
            this.IsCancel.ReadOnly = true;
            this.IsCancel.Width = 80;
            // 
            // ChangedBy
            // 
            this.ChangedBy.DataPropertyName = "ChangedBy";
            this.ChangedBy.HeaderText = "修改人";
            this.ChangedBy.Name = "ChangedBy";
            this.ChangedBy.ReadOnly = true;
            // 
            // ChangeTime
            // 
            this.ChangeTime.DataPropertyName = "ChangeTime";
            this.ChangeTime.HeaderText = "修改时间";
            this.ChangeTime.Name = "ChangeTime";
            this.ChangeTime.ReadOnly = true;
            this.ChangeTime.Width = 120;
            // 
            // ApproveBy
            // 
            this.ApproveBy.DataPropertyName = "ApproveBy";
            this.ApproveBy.HeaderText = "审核人";
            this.ApproveBy.Name = "ApproveBy";
            this.ApproveBy.ReadOnly = true;
            // 
            // BillDate
            // 
            this.BillDate.DataPropertyName = "BillDate";
            this.BillDate.HeaderText = "单据日期";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
            this.BillDate.Width = 120;
            // 
            // frmReceiveBillQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "充值单管理";
            this.Name = "frmReceiveBillQuery";
            this.Size = new System.Drawing.Size(1092, 372);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LBDataGridView grdMain;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnAdd;
        private Controls.LBToolStripButton btnReflesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.DateTimePicker txtBillTimeTo;
        private System.Windows.Forms.DateTimePicker txtBillDateTo;
        private System.Windows.Forms.DateTimePicker txtBillTimeFrom;
        private System.Windows.Forms.DateTimePicker txtBillDateFrom;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private Controls.LBSkinTextBox txtAmountFrom;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private Controls.LBSkinTextBox txtAmountTo;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private Controls.LBSkinTextBox txtBillCoding;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbUnApprove;
        private System.Windows.Forms.RadioButton rbApproved;
        private System.Windows.Forms.RadioButton rbApproveAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCanceled;
        private System.Windows.Forms.RadioButton rbUnCancel;
        private System.Windows.Forms.RadioButton rbCancelAll;
        private Controls.LBSkinButton btnSearch;
        private Controls.LBSkinTextBox txtApproveBy;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private Controls.LBTextBox.CoolTextBox txtRPReceiveType;
        private CCWin.SkinControl.SkinLabel skinLabel13;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveBillCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApproveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChargeTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsApprove;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApproveBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
    }
}
