namespace LB.MI.MI
{
    partial class frmSalesReturnBill
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSelectSaleCarOutBillCode = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.lblWeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skinLabel16 = new CCWin.SkinControl.SkinLabel();
            this.txtCarID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtSuttleWeight = new System.Windows.Forms.TextBox();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.txtCarTare = new System.Windows.Forms.TextBox();
            this.skinLabel15 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.txtItemID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtSaleCarOutBillCode = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtSaleCarInBillCode = new LB.Controls.LBSkinTextBox(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtReturnReason = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel14 = new CCWin.SkinControl.SkinLabel();
            this.txtReturnType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel13 = new CCWin.SkinControl.SkinLabel();
            this.btnSubmitReturn = new System.Windows.Forms.Button();
            this.skinLabel12 = new CCWin.SkinControl.SkinLabel();
            this.btnOutWeight = new System.Windows.Forms.Button();
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.txtOutWeight = new System.Windows.Forms.TextBox();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.btnInWeight = new System.Windows.Forms.Button();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtInWeight = new System.Windows.Forms.TextBox();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.SaleCarReturnBilCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarTare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuttleWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDateIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSteady = new CCWin.SkinControl.SkinLabel();
            this.pnlSteadyStatus = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlCarHeader = new System.Windows.Forms.Panel();
            this.pnlCarTail = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClearValue = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSelectSaleCarOutBillCode
            // 
            this.txtSelectSaleCarOutBillCode.BackColor = System.Drawing.Color.Transparent;
            this.txtSelectSaleCarOutBillCode.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtSelectSaleCarOutBillCode.CanBeEmpty = false;
            this.txtSelectSaleCarOutBillCode.Caption = "车号";
            this.txtSelectSaleCarOutBillCode.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtSelectSaleCarOutBillCode.LBTitle = "  ";
            this.txtSelectSaleCarOutBillCode.LBTitleVisible = false;
            this.txtSelectSaleCarOutBillCode.Location = new System.Drawing.Point(143, 43);
            this.txtSelectSaleCarOutBillCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtSelectSaleCarOutBillCode.Name = "txtSelectSaleCarOutBillCode";
            this.txtSelectSaleCarOutBillCode.PopupWidth = 120;
            this.txtSelectSaleCarOutBillCode.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtSelectSaleCarOutBillCode.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtSelectSaleCarOutBillCode.Size = new System.Drawing.Size(204, 34);
            this.txtSelectSaleCarOutBillCode.TabIndex = 47;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel8.Location = new System.Drawing.Point(3, 43);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(137, 30);
            this.skinLabel8.TabIndex = 48;
            this.skinLabel8.Text = "退货出场单号";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.Color.Black;
            this.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeight.Font = new System.Drawing.Font("黑体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWeight.ForeColor = System.Drawing.Color.Red;
            this.lblWeight.Location = new System.Drawing.Point(350, 31);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(248, 57);
            this.lblWeight.TabIndex = 49;
            this.lblWeight.Text = "-------";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(604, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 40);
            this.label1.TabIndex = 50;
            this.label1.Text = "KG";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skinLabel16);
            this.groupBox1.Controls.Add(this.txtCarID);
            this.groupBox1.Controls.Add(this.skinLabel3);
            this.groupBox1.Controls.Add(this.skinLabel4);
            this.groupBox1.Controls.Add(this.txtSuttleWeight);
            this.groupBox1.Controls.Add(this.txtTotalWeight);
            this.groupBox1.Controls.Add(this.txtCarTare);
            this.groupBox1.Controls.Add(this.skinLabel15);
            this.groupBox1.Controls.Add(this.txtCustomerID);
            this.groupBox1.Controls.Add(this.skinLabel10);
            this.groupBox1.Controls.Add(this.txtItemID);
            this.groupBox1.Controls.Add(this.skinLabel9);
            this.groupBox1.Controls.Add(this.skinLabel2);
            this.groupBox1.Controls.Add(this.txtSaleCarOutBillCode);
            this.groupBox1.Controls.Add(this.skinLabel1);
            this.groupBox1.Controls.Add(this.txtSaleCarInBillCode);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox1.Location = new System.Drawing.Point(18, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 335);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "最近出场数据";
            // 
            // skinLabel16
            // 
            this.skinLabel16.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel16.BorderColor = System.Drawing.Color.White;
            this.skinLabel16.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel16.Location = new System.Drawing.Point(55, 192);
            this.skinLabel16.Name = "skinLabel16";
            this.skinLabel16.Size = new System.Drawing.Size(56, 29);
            this.skinLabel16.TabIndex = 63;
            this.skinLabel16.Text = "车辆";
            this.skinLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtCarID.Location = new System.Drawing.Point(125, 189);
            this.txtCarID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.PopupWidth = 120;
            this.txtCarID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCarID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCarID.Size = new System.Drawing.Size(175, 34);
            this.txtCarID.TabIndex = 62;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel3.Location = new System.Drawing.Point(53, 228);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(60, 30);
            this.skinLabel3.TabIndex = 61;
            this.skinLabel3.Text = "毛 重";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel4.Location = new System.Drawing.Point(52, 262);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(60, 30);
            this.skinLabel4.TabIndex = 60;
            this.skinLabel4.Text = "皮 重";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSuttleWeight
            // 
            this.txtSuttleWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSuttleWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtSuttleWeight.Location = new System.Drawing.Point(125, 297);
            this.txtSuttleWeight.Multiline = true;
            this.txtSuttleWeight.Name = "txtSuttleWeight";
            this.txtSuttleWeight.ReadOnly = true;
            this.txtSuttleWeight.Size = new System.Drawing.Size(175, 30);
            this.txtSuttleWeight.TabIndex = 59;
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtTotalWeight.Location = new System.Drawing.Point(125, 229);
            this.txtTotalWeight.Multiline = true;
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.ReadOnly = true;
            this.txtTotalWeight.Size = new System.Drawing.Size(175, 30);
            this.txtTotalWeight.TabIndex = 58;
            // 
            // txtCarTare
            // 
            this.txtCarTare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarTare.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarTare.Location = new System.Drawing.Point(125, 263);
            this.txtCarTare.Multiline = true;
            this.txtCarTare.Name = "txtCarTare";
            this.txtCarTare.ReadOnly = true;
            this.txtCarTare.Size = new System.Drawing.Size(175, 30);
            this.txtCarTare.TabIndex = 57;
            // 
            // skinLabel15
            // 
            this.skinLabel15.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel15.BorderColor = System.Drawing.Color.White;
            this.skinLabel15.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel15.Location = new System.Drawing.Point(52, 298);
            this.skinLabel15.Name = "skinLabel15";
            this.skinLabel15.Size = new System.Drawing.Size(60, 30);
            this.skinLabel15.TabIndex = 56;
            this.skinLabel15.Text = "净 重";
            this.skinLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtCustomerID.Location = new System.Drawing.Point(125, 148);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(175, 34);
            this.txtCustomerID.TabIndex = 54;
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel10.Location = new System.Drawing.Point(18, 150);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(95, 29);
            this.skinLabel10.TabIndex = 55;
            this.skinLabel10.Text = "客户名称";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtItemID.Location = new System.Drawing.Point(125, 110);
            this.txtItemID.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.PopupWidth = 120;
            this.txtItemID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtItemID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtItemID.Size = new System.Drawing.Size(175, 34);
            this.txtItemID.TabIndex = 52;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel9.Location = new System.Drawing.Point(15, 113);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(98, 28);
            this.skinLabel9.TabIndex = 53;
            this.skinLabel9.Text = "货物名称";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel2.Location = new System.Drawing.Point(6, 75);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(116, 30);
            this.skinLabel2.TabIndex = 51;
            this.skinLabel2.Text = "出场单号";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSaleCarOutBillCode
            // 
            this.txtSaleCarOutBillCode.BackColor = System.Drawing.Color.Transparent;
            this.txtSaleCarOutBillCode.CanBeEmpty = true;
            this.txtSaleCarOutBillCode.Caption = "";
            this.txtSaleCarOutBillCode.DownBack = null;
            this.txtSaleCarOutBillCode.Icon = null;
            this.txtSaleCarOutBillCode.IconIsButton = false;
            this.txtSaleCarOutBillCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSaleCarOutBillCode.IsPasswordChat = '\0';
            this.txtSaleCarOutBillCode.IsSystemPasswordChar = false;
            this.txtSaleCarOutBillCode.Lines = new string[0];
            this.txtSaleCarOutBillCode.Location = new System.Drawing.Point(125, 72);
            this.txtSaleCarOutBillCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtSaleCarOutBillCode.MaxLength = 32767;
            this.txtSaleCarOutBillCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtSaleCarOutBillCode.MouseBack = null;
            this.txtSaleCarOutBillCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSaleCarOutBillCode.Multiline = true;
            this.txtSaleCarOutBillCode.Name = "txtSaleCarOutBillCode";
            this.txtSaleCarOutBillCode.NormlBack = null;
            this.txtSaleCarOutBillCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtSaleCarOutBillCode.ReadOnly = true;
            this.txtSaleCarOutBillCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSaleCarOutBillCode.Size = new System.Drawing.Size(175, 34);
            // 
            // 
            // 
            this.txtSaleCarOutBillCode.SkinTxt.AccessibleName = "";
            this.txtSaleCarOutBillCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtSaleCarOutBillCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSaleCarOutBillCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSaleCarOutBillCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaleCarOutBillCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaleCarOutBillCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtSaleCarOutBillCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtSaleCarOutBillCode.SkinTxt.Multiline = true;
            this.txtSaleCarOutBillCode.SkinTxt.Name = "BaseText";
            this.txtSaleCarOutBillCode.SkinTxt.ReadOnly = true;
            this.txtSaleCarOutBillCode.SkinTxt.Size = new System.Drawing.Size(165, 24);
            this.txtSaleCarOutBillCode.SkinTxt.TabIndex = 0;
            this.txtSaleCarOutBillCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarOutBillCode.SkinTxt.WaterText = "";
            this.txtSaleCarOutBillCode.TabIndex = 50;
            this.txtSaleCarOutBillCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSaleCarOutBillCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarOutBillCode.WaterText = "";
            this.txtSaleCarOutBillCode.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel1.Location = new System.Drawing.Point(6, 36);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(116, 30);
            this.skinLabel1.TabIndex = 49;
            this.skinLabel1.Text = "入场单号";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtSaleCarInBillCode.Location = new System.Drawing.Point(125, 34);
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
            this.txtSaleCarInBillCode.Size = new System.Drawing.Size(175, 34);
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
            this.txtSaleCarInBillCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtSaleCarInBillCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtSaleCarInBillCode.SkinTxt.Multiline = true;
            this.txtSaleCarInBillCode.SkinTxt.Name = "BaseText";
            this.txtSaleCarInBillCode.SkinTxt.ReadOnly = true;
            this.txtSaleCarInBillCode.SkinTxt.Size = new System.Drawing.Size(165, 24);
            this.txtSaleCarInBillCode.SkinTxt.TabIndex = 0;
            this.txtSaleCarInBillCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarInBillCode.SkinTxt.WaterText = "";
            this.txtSaleCarInBillCode.TabIndex = 5;
            this.txtSaleCarInBillCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSaleCarInBillCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSaleCarInBillCode.WaterText = "";
            this.txtSaleCarInBillCode.WordWrap = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtReturnReason);
            this.groupBox2.Controls.Add(this.skinLabel14);
            this.groupBox2.Controls.Add(this.txtReturnType);
            this.groupBox2.Controls.Add(this.skinLabel13);
            this.groupBox2.Controls.Add(this.btnSubmitReturn);
            this.groupBox2.Controls.Add(this.skinLabel12);
            this.groupBox2.Controls.Add(this.btnOutWeight);
            this.groupBox2.Controls.Add(this.skinLabel11);
            this.groupBox2.Controls.Add(this.txtOutWeight);
            this.groupBox2.Controls.Add(this.skinLabel7);
            this.groupBox2.Controls.Add(this.btnInWeight);
            this.groupBox2.Controls.Add(this.skinLabel6);
            this.groupBox2.Controls.Add(this.skinLabel5);
            this.groupBox2.Controls.Add(this.txtInWeight);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox2.Location = new System.Drawing.Point(350, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 335);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "退货操作流程";
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.CanBeEmpty = false;
            this.txtReturnReason.Caption = "每周";
            this.txtReturnReason.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtReturnReason.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtReturnReason.DM_UseSelectable = true;
            this.txtReturnReason.FormattingEnabled = true;
            this.txtReturnReason.ItemHeight = 28;
            this.txtReturnReason.Location = new System.Drawing.Point(143, 295);
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.Size = new System.Drawing.Size(162, 34);
            this.txtReturnReason.TabIndex = 74;
            // 
            // skinLabel14
            // 
            this.skinLabel14.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel14.BorderColor = System.Drawing.Color.White;
            this.skinLabel14.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel14.Location = new System.Drawing.Point(-11, 297);
            this.skinLabel14.Name = "skinLabel14";
            this.skinLabel14.Size = new System.Drawing.Size(148, 30);
            this.skinLabel14.TabIndex = 73;
            this.skinLabel14.Text = "选择退货方原因";
            this.skinLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReturnType
            // 
            this.txtReturnType.CanBeEmpty = false;
            this.txtReturnType.Caption = "每周";
            this.txtReturnType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtReturnType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtReturnType.DM_UseSelectable = true;
            this.txtReturnType.FormattingEnabled = true;
            this.txtReturnType.ItemHeight = 28;
            this.txtReturnType.Location = new System.Drawing.Point(143, 257);
            this.txtReturnType.Name = "txtReturnType";
            this.txtReturnType.Size = new System.Drawing.Size(162, 34);
            this.txtReturnType.TabIndex = 72;
            // 
            // skinLabel13
            // 
            this.skinLabel13.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel13.BorderColor = System.Drawing.Color.White;
            this.skinLabel13.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel13.Location = new System.Drawing.Point(6, 259);
            this.skinLabel13.Name = "skinLabel13";
            this.skinLabel13.Size = new System.Drawing.Size(125, 30);
            this.skinLabel13.TabIndex = 71;
            this.skinLabel13.Text = "选择退货方式";
            this.skinLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSubmitReturn
            // 
            this.btnSubmitReturn.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmitReturn.Location = new System.Drawing.Point(311, 254);
            this.btnSubmitReturn.Name = "btnSubmitReturn";
            this.btnSubmitReturn.Size = new System.Drawing.Size(116, 73);
            this.btnSubmitReturn.TabIndex = 70;
            this.btnSubmitReturn.Text = "确认退货";
            this.btnSubmitReturn.UseVisualStyleBackColor = true;
            this.btnSubmitReturn.Click += new System.EventHandler(this.btnSubmitReturn_Click);
            // 
            // skinLabel12
            // 
            this.skinLabel12.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel12.BorderColor = System.Drawing.Color.White;
            this.skinLabel12.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel12.Location = new System.Drawing.Point(17, 213);
            this.skinLabel12.Name = "skinLabel12";
            this.skinLabel12.Size = new System.Drawing.Size(160, 30);
            this.skinLabel12.TabIndex = 69;
            this.skinLabel12.Text = "第三步：确认退货";
            this.skinLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOutWeight
            // 
            this.btnOutWeight.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOutWeight.Location = new System.Drawing.Point(311, 165);
            this.btnOutWeight.Name = "btnOutWeight";
            this.btnOutWeight.Size = new System.Drawing.Size(116, 35);
            this.btnOutWeight.TabIndex = 68;
            this.btnOutWeight.Text = "称 重";
            this.btnOutWeight.UseVisualStyleBackColor = true;
            this.btnOutWeight.Click += new System.EventHandler(this.btnOutWeight_Click);
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel11.Location = new System.Drawing.Point(54, 168);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(60, 30);
            this.skinLabel11.TabIndex = 67;
            this.skinLabel11.Text = "重量";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOutWeight
            // 
            this.txtOutWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtOutWeight.Location = new System.Drawing.Point(143, 169);
            this.txtOutWeight.Multiline = true;
            this.txtOutWeight.Name = "txtOutWeight";
            this.txtOutWeight.ReadOnly = true;
            this.txtOutWeight.Size = new System.Drawing.Size(162, 30);
            this.txtOutWeight.TabIndex = 66;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel7.Location = new System.Drawing.Point(17, 125);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(160, 30);
            this.skinLabel7.TabIndex = 65;
            this.skinLabel7.Text = "第二步：出场称重";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInWeight
            // 
            this.btnInWeight.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInWeight.Location = new System.Drawing.Point(311, 71);
            this.btnInWeight.Name = "btnInWeight";
            this.btnInWeight.Size = new System.Drawing.Size(116, 35);
            this.btnInWeight.TabIndex = 64;
            this.btnInWeight.Text = "称重并保存";
            this.btnInWeight.UseVisualStyleBackColor = true;
            this.btnInWeight.Click += new System.EventHandler(this.btnInWeight_Click);
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel6.Location = new System.Drawing.Point(54, 74);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(60, 30);
            this.skinLabel6.TabIndex = 63;
            this.skinLabel6.Text = "重量";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel5.Location = new System.Drawing.Point(17, 34);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(160, 30);
            this.skinLabel5.TabIndex = 50;
            this.skinLabel5.Text = "第一步：入场称重";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInWeight
            // 
            this.txtInWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtInWeight.Location = new System.Drawing.Point(143, 75);
            this.txtInWeight.Multiline = true;
            this.txtInWeight.Name = "txtInWeight";
            this.txtInWeight.ReadOnly = true;
            this.txtInWeight.Size = new System.Drawing.Size(162, 30);
            this.txtInWeight.TabIndex = 62;
            // 
            // grdMain
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMain.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdMain.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold);
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleCarReturnBilCode,
            this.CarNum,
            this.CustomerName,
            this.ItemName,
            this.TotalWeight,
            this.CarTare,
            this.SuttleWeight,
            this.BillDateIn});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold);
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold);
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(18, 469);
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
            this.grdMain.Size = new System.Drawing.Size(814, 157);
            this.grdMain.TabIndex = 53;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            this.grdMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdMain_CellDoubleClick);
            // 
            // SaleCarReturnBilCode
            // 
            this.SaleCarReturnBilCode.DataPropertyName = "SaleCarReturnBilCode";
            this.SaleCarReturnBilCode.HeaderText = "退货单号";
            this.SaleCarReturnBilCode.Name = "SaleCarReturnBilCode";
            this.SaleCarReturnBilCode.ReadOnly = true;
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
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemName.HeaderText = "货物名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotalWeight
            // 
            this.TotalWeight.DataPropertyName = "TotalWeight";
            this.TotalWeight.HeaderText = "原毛重";
            this.TotalWeight.Name = "TotalWeight";
            this.TotalWeight.ReadOnly = true;
            this.TotalWeight.Width = 90;
            // 
            // CarTare
            // 
            this.CarTare.DataPropertyName = "CarTare";
            this.CarTare.HeaderText = "原皮重";
            this.CarTare.Name = "CarTare";
            this.CarTare.ReadOnly = true;
            this.CarTare.Width = 90;
            // 
            // SuttleWeight
            // 
            this.SuttleWeight.DataPropertyName = "SuttleWeight";
            this.SuttleWeight.HeaderText = "入场总重";
            this.SuttleWeight.Name = "SuttleWeight";
            this.SuttleWeight.ReadOnly = true;
            this.SuttleWeight.Width = 90;
            // 
            // BillDateIn
            // 
            this.BillDateIn.DataPropertyName = "BillDateIn";
            this.BillDateIn.HeaderText = "第一次过磅";
            this.BillDateIn.Name = "BillDateIn";
            this.BillDateIn.ReadOnly = true;
            this.BillDateIn.Width = 150;
            // 
            // lblSteady
            // 
            this.lblSteady.BackColor = System.Drawing.Color.Transparent;
            this.lblSteady.BorderColor = System.Drawing.Color.White;
            this.lblSteady.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblSteady.Location = new System.Drawing.Point(549, 95);
            this.lblSteady.Name = "lblSteady";
            this.lblSteady.Size = new System.Drawing.Size(64, 23);
            this.lblSteady.TabIndex = 65;
            this.lblSteady.Text = "稳定";
            this.lblSteady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSteadyStatus
            // 
            this.pnlSteadyStatus.Location = new System.Drawing.Point(511, 91);
            this.pnlSteadyStatus.Name = "pnlSteadyStatus";
            this.pnlSteadyStatus.Size = new System.Drawing.Size(30, 30);
            this.pnlSteadyStatus.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(669, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 15);
            this.label8.TabIndex = 70;
            this.label8.Text = "尾";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(814, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 15);
            this.label7.TabIndex = 69;
            this.label7.Text = "头";
            // 
            // pnlCarHeader
            // 
            this.pnlCarHeader.BackColor = System.Drawing.Color.White;
            this.pnlCarHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCarHeader.Location = new System.Drawing.Point(809, 37);
            this.pnlCarHeader.Name = "pnlCarHeader";
            this.pnlCarHeader.Size = new System.Drawing.Size(5, 51);
            this.pnlCarHeader.TabIndex = 68;
            // 
            // pnlCarTail
            // 
            this.pnlCarTail.BackColor = System.Drawing.Color.White;
            this.pnlCarTail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCarTail.Location = new System.Drawing.Point(692, 37);
            this.pnlCarTail.Name = "pnlCarTail";
            this.pnlCarTail.Size = new System.Drawing.Size(5, 51);
            this.pnlCarTail.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(703, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 40);
            this.label5.TabIndex = 66;
            this.label5.Text = "地磅区域";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearValue
            // 
            this.btnClearValue.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearValue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClearValue.Location = new System.Drawing.Point(230, 80);
            this.btnClearValue.Name = "btnClearValue";
            this.btnClearValue.Size = new System.Drawing.Size(101, 30);
            this.btnClearValue.TabIndex = 71;
            this.btnClearValue.Text = "清空内容";
            this.btnClearValue.UseVisualStyleBackColor = true;
            this.btnClearValue.Click += new System.EventHandler(this.btnClearValue_Click);
            // 
            // frmSalesReturnBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClearValue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pnlCarHeader);
            this.Controls.Add(this.pnlCarTail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblSteady);
            this.Controls.Add(this.pnlSteadyStatus);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSelectSaleCarOutBillCode);
            this.Controls.Add(this.skinLabel8);
            this.LBPageTitle = "退货单";
            this.Name = "frmSalesReturnBill";
            this.Size = new System.Drawing.Size(847, 644);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LBTextBox.CoolTextBox txtSelectSaleCarOutBillCode;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private Controls.LBSkinTextBox txtSaleCarInBillCode;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private Controls.LBSkinTextBox txtSaleCarOutBillCode;
        private Controls.LBTextBox.CoolTextBox txtItemID;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private System.Windows.Forms.TextBox txtSuttleWeight;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private System.Windows.Forms.TextBox txtCarTare;
        private CCWin.SkinControl.SkinLabel skinLabel15;
        private System.Windows.Forms.GroupBox groupBox2;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private System.Windows.Forms.TextBox txtInWeight;
        private System.Windows.Forms.Button btnInWeight;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private CCWin.SkinControl.SkinLabel skinLabel12;
        private System.Windows.Forms.Button btnOutWeight;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private System.Windows.Forms.TextBox txtOutWeight;
        private CCWin.SkinControl.SkinLabel skinLabel13;
        private System.Windows.Forms.Button btnSubmitReturn;
        private Controls.LBMetroComboBox txtReturnType;
        private Controls.LBDataGridView grdMain;
        private CCWin.SkinControl.SkinLabel lblSteady;
        private System.Windows.Forms.Panel pnlSteadyStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlCarHeader;
        private System.Windows.Forms.Panel pnlCarTail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleCarReturnBilCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarTare;
        private System.Windows.Forms.DataGridViewTextBoxColumn SuttleWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDateIn;
        private Controls.LBMetroComboBox txtReturnReason;
        private CCWin.SkinControl.SkinLabel skinLabel14;
        private CCWin.SkinControl.SkinLabel skinLabel16;
        private Controls.LBTextBox.CoolTextBox txtCarID;
    }
}
