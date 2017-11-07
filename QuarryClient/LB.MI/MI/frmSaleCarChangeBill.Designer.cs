namespace LB.MI.MI
{
    partial class frmSaleCarChangeBill
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAmount = new CCWin.SkinControl.SkinLabel();
            this.lblPrice = new CCWin.SkinControl.SkinLabel();
            this.lblSuttleWeight = new CCWin.SkinControl.SkinLabel();
            this.lblCarTare = new CCWin.SkinControl.SkinLabel();
            this.lblTotalWeight = new CCWin.SkinControl.SkinLabel();
            this.skinLabel21 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel22 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel23 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel24 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel25 = new CCWin.SkinControl.SkinLabel();
            this.lblCalculateType = new CCWin.SkinControl.SkinLabel();
            this.lblReceiveType = new CCWin.SkinControl.SkinLabel();
            this.lblCustomName = new CCWin.SkinControl.SkinLabel();
            this.skinLabel12 = new CCWin.SkinControl.SkinLabel();
            this.lblItemName = new CCWin.SkinControl.SkinLabel();
            this.skinLabel13 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel14 = new CCWin.SkinControl.SkinLabel();
            this.lblCarNum = new CCWin.SkinControl.SkinLabel();
            this.skinLabel16 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtSaleCarInBillCode = new LB.Controls.LBSkinTextBox(this.components);
            this.txtCalculateType = new LB.Controls.LBMetroComboBox(this.components);
            this.txtReceiveType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.txtItemID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSuttleWeight = new System.Windows.Forms.TextBox();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.txtCarTare = new System.Windows.Forms.TextBox();
            this.skinLabel17 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel15 = new CCWin.SkinControl.SkinLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.txtCarID = new LB.Controls.LBTextBox.CoolTextBox();
            this.txtDescription = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1048, 40);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LB.MI.Properties.Resources.btnClose;
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
            this.btnSave.Image = global::LB.MI.Properties.Resources.btnNewSave3;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 37);
            this.btnSave.Text = "保存修改";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.lblPrice);
            this.groupBox1.Controls.Add(this.lblSuttleWeight);
            this.groupBox1.Controls.Add(this.lblCarTare);
            this.groupBox1.Controls.Add(this.lblTotalWeight);
            this.groupBox1.Controls.Add(this.skinLabel21);
            this.groupBox1.Controls.Add(this.skinLabel22);
            this.groupBox1.Controls.Add(this.skinLabel23);
            this.groupBox1.Controls.Add(this.skinLabel24);
            this.groupBox1.Controls.Add(this.skinLabel25);
            this.groupBox1.Controls.Add(this.lblCalculateType);
            this.groupBox1.Controls.Add(this.lblReceiveType);
            this.groupBox1.Controls.Add(this.lblCustomName);
            this.groupBox1.Controls.Add(this.skinLabel12);
            this.groupBox1.Controls.Add(this.lblItemName);
            this.groupBox1.Controls.Add(this.skinLabel13);
            this.groupBox1.Controls.Add(this.skinLabel14);
            this.groupBox1.Controls.Add(this.lblCarNum);
            this.groupBox1.Controls.Add(this.skinLabel16);
            this.groupBox1.Controls.Add(this.skinLabel4);
            this.groupBox1.Controls.Add(this.skinLabel5);
            this.groupBox1.Controls.Add(this.txtSaleCarInBillCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 320);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变更前信息";
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.BorderColor = System.Drawing.Color.White;
            this.lblAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblAmount.Location = new System.Drawing.Point(391, 197);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(98, 30);
            this.lblAmount.TabIndex = 88;
            this.lblAmount.Text = " ";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrice
            // 
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.BorderColor = System.Drawing.Color.White;
            this.lblPrice.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblPrice.Location = new System.Drawing.Point(391, 163);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(98, 30);
            this.lblPrice.TabIndex = 87;
            this.lblPrice.Text = " ";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuttleWeight
            // 
            this.lblSuttleWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblSuttleWeight.BorderColor = System.Drawing.Color.White;
            this.lblSuttleWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblSuttleWeight.Location = new System.Drawing.Point(391, 115);
            this.lblSuttleWeight.Name = "lblSuttleWeight";
            this.lblSuttleWeight.Size = new System.Drawing.Size(98, 30);
            this.lblSuttleWeight.TabIndex = 86;
            this.lblSuttleWeight.Text = " ";
            this.lblSuttleWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCarTare
            // 
            this.lblCarTare.BackColor = System.Drawing.Color.Transparent;
            this.lblCarTare.BorderColor = System.Drawing.Color.White;
            this.lblCarTare.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCarTare.Location = new System.Drawing.Point(391, 70);
            this.lblCarTare.Name = "lblCarTare";
            this.lblCarTare.Size = new System.Drawing.Size(98, 30);
            this.lblCarTare.TabIndex = 85;
            this.lblCarTare.Text = " ";
            this.lblCarTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalWeight
            // 
            this.lblTotalWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalWeight.BorderColor = System.Drawing.Color.White;
            this.lblTotalWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblTotalWeight.Location = new System.Drawing.Point(391, 32);
            this.lblTotalWeight.Name = "lblTotalWeight";
            this.lblTotalWeight.Size = new System.Drawing.Size(98, 30);
            this.lblTotalWeight.TabIndex = 84;
            this.lblTotalWeight.Text = " ";
            this.lblTotalWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel21
            // 
            this.skinLabel21.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel21.BorderColor = System.Drawing.Color.White;
            this.skinLabel21.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel21.Location = new System.Drawing.Point(312, 28);
            this.skinLabel21.Name = "skinLabel21";
            this.skinLabel21.Size = new System.Drawing.Size(60, 30);
            this.skinLabel21.TabIndex = 83;
            this.skinLabel21.Text = "毛 重";
            this.skinLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel22
            // 
            this.skinLabel22.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel22.BorderColor = System.Drawing.Color.White;
            this.skinLabel22.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel22.Location = new System.Drawing.Point(311, 69);
            this.skinLabel22.Name = "skinLabel22";
            this.skinLabel22.Size = new System.Drawing.Size(60, 30);
            this.skinLabel22.TabIndex = 82;
            this.skinLabel22.Text = "皮 重";
            this.skinLabel22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel23
            // 
            this.skinLabel23.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel23.BorderColor = System.Drawing.Color.White;
            this.skinLabel23.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel23.Location = new System.Drawing.Point(312, 160);
            this.skinLabel23.Name = "skinLabel23";
            this.skinLabel23.Size = new System.Drawing.Size(60, 30);
            this.skinLabel23.TabIndex = 81;
            this.skinLabel23.Text = "单 价";
            this.skinLabel23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel24
            // 
            this.skinLabel24.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel24.BorderColor = System.Drawing.Color.White;
            this.skinLabel24.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel24.Location = new System.Drawing.Point(311, 200);
            this.skinLabel24.Name = "skinLabel24";
            this.skinLabel24.Size = new System.Drawing.Size(60, 27);
            this.skinLabel24.TabIndex = 80;
            this.skinLabel24.Text = "金 额";
            this.skinLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel25
            // 
            this.skinLabel25.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel25.BorderColor = System.Drawing.Color.White;
            this.skinLabel25.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel25.Location = new System.Drawing.Point(311, 115);
            this.skinLabel25.Name = "skinLabel25";
            this.skinLabel25.Size = new System.Drawing.Size(60, 30);
            this.skinLabel25.TabIndex = 79;
            this.skinLabel25.Text = "净 重";
            this.skinLabel25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCalculateType
            // 
            this.lblCalculateType.BackColor = System.Drawing.Color.Transparent;
            this.lblCalculateType.BorderColor = System.Drawing.Color.White;
            this.lblCalculateType.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCalculateType.Location = new System.Drawing.Point(115, 241);
            this.lblCalculateType.Name = "lblCalculateType";
            this.lblCalculateType.Size = new System.Drawing.Size(139, 30);
            this.lblCalculateType.TabIndex = 78;
            this.lblCalculateType.Text = " ";
            this.lblCalculateType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReceiveType
            // 
            this.lblReceiveType.BackColor = System.Drawing.Color.Transparent;
            this.lblReceiveType.BorderColor = System.Drawing.Color.White;
            this.lblReceiveType.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblReceiveType.Location = new System.Drawing.Point(115, 200);
            this.lblReceiveType.Name = "lblReceiveType";
            this.lblReceiveType.Size = new System.Drawing.Size(139, 30);
            this.lblReceiveType.TabIndex = 77;
            this.lblReceiveType.Text = " ";
            this.lblReceiveType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomName
            // 
            this.lblCustomName.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomName.BorderColor = System.Drawing.Color.White;
            this.lblCustomName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCustomName.Location = new System.Drawing.Point(115, 166);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(139, 30);
            this.lblCustomName.TabIndex = 76;
            this.lblCustomName.Text = " ";
            this.lblCustomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel12
            // 
            this.skinLabel12.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel12.BorderColor = System.Drawing.Color.White;
            this.skinLabel12.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel12.Location = new System.Drawing.Point(3, 241);
            this.skinLabel12.Name = "skinLabel12";
            this.skinLabel12.Size = new System.Drawing.Size(96, 26);
            this.skinLabel12.TabIndex = 72;
            this.skinLabel12.Text = "计价方式";
            this.skinLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItemName
            // 
            this.lblItemName.BackColor = System.Drawing.Color.Transparent;
            this.lblItemName.BorderColor = System.Drawing.Color.White;
            this.lblItemName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblItemName.Location = new System.Drawing.Point(115, 117);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(139, 30);
            this.lblItemName.TabIndex = 75;
            this.lblItemName.Text = " ";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel13
            // 
            this.skinLabel13.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel13.BorderColor = System.Drawing.Color.White;
            this.skinLabel13.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel13.Location = new System.Drawing.Point(6, 116);
            this.skinLabel13.Name = "skinLabel13";
            this.skinLabel13.Size = new System.Drawing.Size(103, 28);
            this.skinLabel13.TabIndex = 73;
            this.skinLabel13.Text = "货物名称";
            this.skinLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel14
            // 
            this.skinLabel14.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel14.BorderColor = System.Drawing.Color.White;
            this.skinLabel14.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel14.Location = new System.Drawing.Point(11, 161);
            this.skinLabel14.Name = "skinLabel14";
            this.skinLabel14.Size = new System.Drawing.Size(95, 29);
            this.skinLabel14.TabIndex = 74;
            this.skinLabel14.Text = "客户名称";
            this.skinLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCarNum
            // 
            this.lblCarNum.BackColor = System.Drawing.Color.Transparent;
            this.lblCarNum.BorderColor = System.Drawing.Color.White;
            this.lblCarNum.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCarNum.Location = new System.Drawing.Point(115, 76);
            this.lblCarNum.Name = "lblCarNum";
            this.lblCarNum.Size = new System.Drawing.Size(139, 30);
            this.lblCarNum.TabIndex = 73;
            this.lblCarNum.Text = " ";
            this.lblCarNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel16
            // 
            this.skinLabel16.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel16.BorderColor = System.Drawing.Color.White;
            this.skinLabel16.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel16.Location = new System.Drawing.Point(9, 202);
            this.skinLabel16.Name = "skinLabel16";
            this.skinLabel16.Size = new System.Drawing.Size(84, 27);
            this.skinLabel16.TabIndex = 75;
            this.skinLabel16.Text = "收款方式";
            this.skinLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel4.Location = new System.Drawing.Point(-5, 69);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(84, 30);
            this.skinLabel4.TabIndex = 72;
            this.skinLabel4.Text = "车 号";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel5.Location = new System.Drawing.Point(6, 30);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(66, 29);
            this.skinLabel5.TabIndex = 55;
            this.skinLabel5.Text = "单 号";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSaleCarInBillCode
            // 
            this.txtSaleCarInBillCode.BackColor = System.Drawing.Color.Transparent;
            this.txtSaleCarInBillCode.CanBeEmpty = true;
            this.txtSaleCarInBillCode.Caption = "";
            this.txtSaleCarInBillCode.DownBack = null;
            this.txtSaleCarInBillCode.Icon = null;
            this.txtSaleCarInBillCode.IconIsButton = false;
            this.txtSaleCarInBillCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSaleCarInBillCode.IsPasswordChat = '\0';
            this.txtSaleCarInBillCode.IsSystemPasswordChar = false;
            this.txtSaleCarInBillCode.Lines = new string[0];
            this.txtSaleCarInBillCode.Location = new System.Drawing.Point(78, 28);
            this.txtSaleCarInBillCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtSaleCarInBillCode.MaxLength = 32767;
            this.txtSaleCarInBillCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtSaleCarInBillCode.MouseBack = null;
            this.txtSaleCarInBillCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSaleCarInBillCode.Multiline = true;
            this.txtSaleCarInBillCode.Name = "txtSaleCarInBillCode";
            this.txtSaleCarInBillCode.NormlBack = null;
            this.txtSaleCarInBillCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtSaleCarInBillCode.ReadOnly = true;
            this.txtSaleCarInBillCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSaleCarInBillCode.Size = new System.Drawing.Size(188, 34);
            // 
            // 
            // 
            this.txtSaleCarInBillCode.SkinTxt.AccessibleName = "";
            this.txtSaleCarInBillCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtSaleCarInBillCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSaleCarInBillCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSaleCarInBillCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaleCarInBillCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaleCarInBillCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtSaleCarInBillCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtSaleCarInBillCode.SkinTxt.Multiline = true;
            this.txtSaleCarInBillCode.SkinTxt.Name = "BaseText";
            this.txtSaleCarInBillCode.SkinTxt.ReadOnly = true;
            this.txtSaleCarInBillCode.SkinTxt.Size = new System.Drawing.Size(178, 24);
            this.txtSaleCarInBillCode.SkinTxt.TabIndex = 0;
            this.txtSaleCarInBillCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarInBillCode.SkinTxt.WaterText = "";
            this.txtSaleCarInBillCode.TabIndex = 56;
            this.txtSaleCarInBillCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSaleCarInBillCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarInBillCode.WaterText = "";
            this.txtSaleCarInBillCode.WordWrap = true;
            // 
            // txtCalculateType
            // 
            this.txtCalculateType.CanBeEmpty = false;
            this.txtCalculateType.Caption = "每周";
            this.txtCalculateType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtCalculateType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtCalculateType.DM_UseSelectable = true;
            this.txtCalculateType.FormattingEnabled = true;
            this.txtCalculateType.ItemHeight = 28;
            this.txtCalculateType.Location = new System.Drawing.Point(115, 193);
            this.txtCalculateType.Name = "txtCalculateType";
            this.txtCalculateType.Size = new System.Drawing.Size(188, 34);
            this.txtCalculateType.TabIndex = 54;
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
            this.txtReceiveType.Location = new System.Drawing.Point(115, 154);
            this.txtReceiveType.Name = "txtReceiveType";
            this.txtReceiveType.Size = new System.Drawing.Size(188, 34);
            this.txtReceiveType.TabIndex = 53;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel7.Location = new System.Drawing.Point(8, 193);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(96, 26);
            this.skinLabel7.TabIndex = 57;
            this.skinLabel7.Text = "计价方式";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemID
            // 
            this.txtItemID.BackColor = System.Drawing.Color.Transparent;
            this.txtItemID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtItemID.CanBeEmpty = false;
            this.txtItemID.Caption = "客户名称";
            this.txtItemID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtItemID.LBTitle = "  ";
            this.txtItemID.LBTitleVisible = false;
            this.txtItemID.Location = new System.Drawing.Point(116, 70);
            this.txtItemID.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.PopupWidth = 120;
            this.txtItemID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtItemID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtItemID.Size = new System.Drawing.Size(188, 34);
            this.txtItemID.TabIndex = 51;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel9.Location = new System.Drawing.Point(11, 68);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(103, 28);
            this.skinLabel9.TabIndex = 59;
            this.skinLabel9.Text = "货物名称";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomerID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCustomerID.CanBeEmpty = false;
            this.txtCustomerID.Caption = "客户名称";
            this.txtCustomerID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCustomerID.LBTitle = "  ";
            this.txtCustomerID.LBTitleVisible = false;
            this.txtCustomerID.Location = new System.Drawing.Point(116, 113);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(188, 34);
            this.txtCustomerID.TabIndex = 52;
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel10.Location = new System.Drawing.Point(16, 113);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(95, 29);
            this.skinLabel10.TabIndex = 60;
            this.skinLabel10.Text = "客户名称";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel11.Location = new System.Drawing.Point(14, 154);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(84, 27);
            this.skinLabel11.TabIndex = 61;
            this.skinLabel11.Text = "收款方式";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel3.Location = new System.Drawing.Point(308, 29);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(60, 30);
            this.skinLabel3.TabIndex = 71;
            this.skinLabel3.Text = "毛 重";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel2.Location = new System.Drawing.Point(307, 70);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(60, 30);
            this.skinLabel2.TabIndex = 70;
            this.skinLabel2.Text = "皮 重";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel1.Location = new System.Drawing.Point(307, 157);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(60, 30);
            this.skinLabel1.TabIndex = 69;
            this.skinLabel1.Text = "单 价";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtAmount.Location = new System.Drawing.Point(379, 196);
            this.txtAmount.Multiline = true;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(104, 30);
            this.txtAmount.TabIndex = 68;
            // 
            // txtPrice
            // 
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtPrice.Location = new System.Drawing.Point(379, 157);
            this.txtPrice.Multiline = true;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(104, 30);
            this.txtPrice.TabIndex = 67;
            // 
            // txtSuttleWeight
            // 
            this.txtSuttleWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSuttleWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtSuttleWeight.Location = new System.Drawing.Point(379, 114);
            this.txtSuttleWeight.Multiline = true;
            this.txtSuttleWeight.Name = "txtSuttleWeight";
            this.txtSuttleWeight.ReadOnly = true;
            this.txtSuttleWeight.Size = new System.Drawing.Size(104, 30);
            this.txtSuttleWeight.TabIndex = 66;
            this.txtSuttleWeight.TextChanged += new System.EventHandler(this.txtSuttleWeight_TextChanged);
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtTotalWeight.Location = new System.Drawing.Point(379, 29);
            this.txtTotalWeight.Multiline = true;
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.ReadOnly = true;
            this.txtTotalWeight.Size = new System.Drawing.Size(104, 30);
            this.txtTotalWeight.TabIndex = 65;
            // 
            // txtCarTare
            // 
            this.txtCarTare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarTare.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarTare.Location = new System.Drawing.Point(379, 70);
            this.txtCarTare.Multiline = true;
            this.txtCarTare.Name = "txtCarTare";
            this.txtCarTare.ReadOnly = true;
            this.txtCarTare.Size = new System.Drawing.Size(104, 30);
            this.txtCarTare.TabIndex = 64;
            // 
            // skinLabel17
            // 
            this.skinLabel17.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel17.BorderColor = System.Drawing.Color.White;
            this.skinLabel17.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel17.Location = new System.Drawing.Point(307, 197);
            this.skinLabel17.Name = "skinLabel17";
            this.skinLabel17.Size = new System.Drawing.Size(60, 27);
            this.skinLabel17.TabIndex = 63;
            this.skinLabel17.Text = "金 额";
            this.skinLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel15
            // 
            this.skinLabel15.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel15.BorderColor = System.Drawing.Color.White;
            this.skinLabel15.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel15.Location = new System.Drawing.Point(307, 116);
            this.skinLabel15.Name = "skinLabel15";
            this.skinLabel15.Size = new System.Drawing.Size(60, 30);
            this.skinLabel15.TabIndex = 62;
            this.skinLabel15.Text = "净 重";
            this.skinLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.skinLabel8);
            this.groupBox2.Controls.Add(this.txtCarID);
            this.groupBox2.Controls.Add(this.skinLabel6);
            this.groupBox2.Controls.Add(this.txtCalculateType);
            this.groupBox2.Controls.Add(this.skinLabel3);
            this.groupBox2.Controls.Add(this.skinLabel2);
            this.groupBox2.Controls.Add(this.txtReceiveType);
            this.groupBox2.Controls.Add(this.skinLabel1);
            this.groupBox2.Controls.Add(this.skinLabel7);
            this.groupBox2.Controls.Add(this.txtAmount);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.txtSuttleWeight);
            this.groupBox2.Controls.Add(this.txtItemID);
            this.groupBox2.Controls.Add(this.txtTotalWeight);
            this.groupBox2.Controls.Add(this.skinLabel9);
            this.groupBox2.Controls.Add(this.txtCarTare);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Controls.Add(this.skinLabel17);
            this.groupBox2.Controls.Add(this.skinLabel10);
            this.groupBox2.Controls.Add(this.skinLabel15);
            this.groupBox2.Controls.Add(this.skinLabel11);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox2.Location = new System.Drawing.Point(495, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 320);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "变更后信息";
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel6.Location = new System.Drawing.Point(8, 30);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(103, 28);
            this.skinLabel6.TabIndex = 72;
            this.skinLabel6.Text = "车牌号码";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCarID
            // 
            this.txtCarID.BackColor = System.Drawing.Color.Transparent;
            this.txtCarID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCarID.CanBeEmpty = false;
            this.txtCarID.Caption = "车号";
            this.txtCarID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarID.LBTitle = "  ";
            this.txtCarID.LBTitleVisible = false;
            this.txtCarID.Location = new System.Drawing.Point(115, 28);
            this.txtCarID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.PopupWidth = 120;
            this.txtCarID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCarID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCarID.Size = new System.Drawing.Size(188, 34);
            this.txtCarID.TabIndex = 73;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.Transparent;
            this.txtDescription.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtDescription.CanBeEmpty = false;
            this.txtDescription.Caption = "客户名称";
            this.txtDescription.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtDescription.LBTitle = "  ";
            this.txtDescription.LBTitleVisible = false;
            this.txtDescription.Location = new System.Drawing.Point(115, 237);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PopupWidth = 188;
            this.txtDescription.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtDescription.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtDescription.Size = new System.Drawing.Size(368, 64);
            this.txtDescription.TabIndex = 75;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel8.Location = new System.Drawing.Point(28, 239);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(76, 28);
            this.skinLabel8.TabIndex = 74;
            this.skinLabel8.Text = "备 注";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSaleCarChangeBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.LBPageTitle = "变更单据信息";
            this.Name = "frmSaleCarChangeBill";
            this.Size = new System.Drawing.Size(1048, 360);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private Controls.LBMetroComboBox txtCalculateType;
        private Controls.LBSkinTextBox txtSaleCarInBillCode;
        private Controls.LBMetroComboBox txtReceiveType;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private Controls.LBTextBox.CoolTextBox txtItemID;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSuttleWeight;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private System.Windows.Forms.TextBox txtCarTare;
        private CCWin.SkinControl.SkinLabel skinLabel17;
        private CCWin.SkinControl.SkinLabel skinLabel15;
        private System.Windows.Forms.GroupBox groupBox2;
        private CCWin.SkinControl.SkinLabel lblCarNum;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinLabel skinLabel12;
        private CCWin.SkinControl.SkinLabel lblItemName;
        private CCWin.SkinControl.SkinLabel skinLabel13;
        private CCWin.SkinControl.SkinLabel skinLabel14;
        private CCWin.SkinControl.SkinLabel skinLabel16;
        private CCWin.SkinControl.SkinLabel skinLabel21;
        private CCWin.SkinControl.SkinLabel skinLabel22;
        private CCWin.SkinControl.SkinLabel skinLabel23;
        private CCWin.SkinControl.SkinLabel skinLabel24;
        private CCWin.SkinControl.SkinLabel skinLabel25;
        private CCWin.SkinControl.SkinLabel lblCalculateType;
        private CCWin.SkinControl.SkinLabel lblReceiveType;
        private CCWin.SkinControl.SkinLabel lblCustomName;
        private CCWin.SkinControl.SkinLabel lblTotalWeight;
        private CCWin.SkinControl.SkinLabel lblAmount;
        private CCWin.SkinControl.SkinLabel lblPrice;
        private CCWin.SkinControl.SkinLabel lblSuttleWeight;
        private CCWin.SkinControl.SkinLabel lblCarTare;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private Controls.LBTextBox.CoolTextBox txtCarID;
        private Controls.LBTextBox.CoolTextBox txtDescription;
        private CCWin.SkinControl.SkinLabel skinLabel8;
    }
}
