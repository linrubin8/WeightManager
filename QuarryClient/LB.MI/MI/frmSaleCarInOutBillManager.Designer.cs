namespace LB.MI.MI
{
    partial class frmSaleCarInOutBillManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaleCarInOutBillManager));
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.SaleCarOutBillCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillType = new LB.Controls.LBDataGridViewComboBoxColumn();
            this.CustomerTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarTare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuttleWeightT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FarePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokerPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleCarInBillCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalculateType = new LB.Controls.LBDataGridViewComboBoxColumn();
            this.CancelDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDateOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDateIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateByIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTimeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateByOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleCarInBillID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new LB.Controls.LBSkinButton(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCustomerTypeID = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel19 = new CCWin.SkinControl.SkinLabel();
            this.txtBillCodeOut = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel18 = new CCWin.SkinControl.SkinLabel();
            this.btnSumSearch = new LB.Controls.LBSkinButton(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.skinLabel17 = new CCWin.SkinControl.SkinLabel();
            this.cbSumItem = new System.Windows.Forms.CheckBox();
            this.skinLabel16 = new CCWin.SkinControl.SkinLabel();
            this.cbSumCar = new System.Windows.Forms.CheckBox();
            this.skinLabel15 = new CCWin.SkinControl.SkinLabel();
            this.cbSumCustomer = new System.Windows.Forms.CheckBox();
            this.cbUseOutDate = new System.Windows.Forms.CheckBox();
            this.cbUseInDate = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbUnApprove = new System.Windows.Forms.RadioButton();
            this.rbApproved = new System.Windows.Forms.RadioButton();
            this.rbApproveAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCanceled = new System.Windows.Forms.RadioButton();
            this.rbUnCancel = new System.Windows.Forms.RadioButton();
            this.rbCancelAll = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUnFinish = new System.Windows.Forms.RadioButton();
            this.rbFinished = new System.Windows.Forms.RadioButton();
            this.rbFinishAll = new System.Windows.Forms.RadioButton();
            this.txtInBillTimeTo = new System.Windows.Forms.DateTimePicker();
            this.txtInBillDateTo = new System.Windows.Forms.DateTimePicker();
            this.txtOutBillTimeTo = new System.Windows.Forms.DateTimePicker();
            this.txtOutBillDateTo = new System.Windows.Forms.DateTimePicker();
            this.txtOutBillTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.txtOutBillDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtInBillTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.txtInBillDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtOutBillCraeteBy = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.txtItemID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtBillCode = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtCarID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalAmount = new LB.Controls.LBSkinTextBox(this.components);
            this.txtTotalCar = new LB.Controls.LBSkinTextBox(this.components);
            this.txtStuffWeight = new LB.Controls.LBSkinTextBox(this.components);
            this.txtTareWeight = new LB.Controls.LBSkinTextBox(this.components);
            this.txtTotalWeight = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel14 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel13 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel12 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grdSumMain = new LB.Controls.LBDataGridView(this.components);
            this.CustomerNameSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNumSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNameSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprove = new LB.Controls.LBToolStripButton(this.components);
            this.btnUnApprove = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new LB.Controls.LBToolStripButton(this.components);
            this.btnUnCancel = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddInBill = new LB.Controls.LBToolStripButton(this.components);
            this.btnAddOutBill = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSynToK3 = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteBill = new LB.Controls.LBToolStripButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSumMain)).BeginInit();
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
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleCarOutBillCode,
            this.CustomerName,
            this.CarNum,
            this.ItemName,
            this.BillType,
            this.CustomerTypeName,
            this.TotalWeight,
            this.CarTare,
            this.SuttleWeightT,
            this.PriceT,
            this.Amount,
            this.MaterialPrice,
            this.FarePrice,
            this.TaxPrice,
            this.BrokerPrice,
            this.Description,
            this.SaleCarInBillCode,
            this.CalculateType,
            this.CancelDesc,
            this.BillDateOut,
            this.BillDateIn,
            this.CreateByIn,
            this.CreateTimeIn,
            this.CreateByOut,
            this.CreateTimeOut,
            this.SaleCarInBillID,
            this.ChangeDetail});
            this.grdMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.DefaultCellStyle = dataGridViewCellStyle8;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMain.EnableHeadersVisualStyles = false;
            this.grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdMain.HeadFont = null;
            this.grdMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
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
            this.grdMain.Size = new System.Drawing.Size(727, 155);
            this.grdMain.TabIndex = 9;
            this.grdMain.TitleBack = null;
            this.grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // SaleCarOutBillCode
            // 
            this.SaleCarOutBillCode.DataPropertyName = "SaleCarOutBillCode";
            this.SaleCarOutBillCode.HeaderText = "出场单号";
            this.SaleCarOutBillCode.Name = "SaleCarOutBillCode";
            this.SaleCarOutBillCode.ReadOnly = true;
            this.SaleCarOutBillCode.Width = 120;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // CarNum
            // 
            this.CarNum.DataPropertyName = "CarNum";
            this.CarNum.HeaderText = "车号";
            this.CarNum.Name = "CarNum";
            this.CarNum.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "货物名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // BillType
            // 
            this.BillType.DataPropertyName = "BillType";
            this.BillType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.BillType.FieldName = "BillType";
            this.BillType.HeaderText = "单据状态";
            this.BillType.Name = "BillType";
            this.BillType.ReadOnly = true;
            this.BillType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CustomerTypeName
            // 
            this.CustomerTypeName.DataPropertyName = "CustomerTypeName";
            this.CustomerTypeName.HeaderText = "客户类型";
            this.CustomerTypeName.Name = "CustomerTypeName";
            this.CustomerTypeName.ReadOnly = true;
            this.CustomerTypeName.Width = 80;
            // 
            // TotalWeight
            // 
            this.TotalWeight.DataPropertyName = "TotalWeight";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.TotalWeight.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalWeight.HeaderText = "毛重";
            this.TotalWeight.Name = "TotalWeight";
            this.TotalWeight.ReadOnly = true;
            // 
            // CarTare
            // 
            this.CarTare.DataPropertyName = "CarTare";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.CarTare.DefaultCellStyle = dataGridViewCellStyle4;
            this.CarTare.HeaderText = "皮重";
            this.CarTare.Name = "CarTare";
            this.CarTare.ReadOnly = true;
            // 
            // SuttleWeightT
            // 
            this.SuttleWeightT.DataPropertyName = "SuttleWeightT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.SuttleWeightT.DefaultCellStyle = dataGridViewCellStyle5;
            this.SuttleWeightT.HeaderText = "净重(吨)";
            this.SuttleWeightT.Name = "SuttleWeightT";
            this.SuttleWeightT.ReadOnly = true;
            // 
            // PriceT
            // 
            this.PriceT.DataPropertyName = "PriceT";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.PriceT.DefaultCellStyle = dataGridViewCellStyle6;
            this.PriceT.HeaderText = "总单价(吨)";
            this.PriceT.Name = "PriceT";
            this.PriceT.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.HeaderText = "金额";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // MaterialPrice
            // 
            this.MaterialPrice.DataPropertyName = "MaterialPrice";
            this.MaterialPrice.HeaderText = "石价";
            this.MaterialPrice.Name = "MaterialPrice";
            this.MaterialPrice.ReadOnly = true;
            this.MaterialPrice.Width = 80;
            // 
            // FarePrice
            // 
            this.FarePrice.DataPropertyName = "FarePrice";
            this.FarePrice.HeaderText = "运费";
            this.FarePrice.Name = "FarePrice";
            this.FarePrice.ReadOnly = true;
            this.FarePrice.Width = 80;
            // 
            // TaxPrice
            // 
            this.TaxPrice.DataPropertyName = "TaxPrice";
            this.TaxPrice.HeaderText = "税费";
            this.TaxPrice.Name = "TaxPrice";
            this.TaxPrice.ReadOnly = true;
            this.TaxPrice.Width = 80;
            // 
            // BrokerPrice
            // 
            this.BrokerPrice.DataPropertyName = "BrokerPrice";
            this.BrokerPrice.HeaderText = "佣金";
            this.BrokerPrice.Name = "BrokerPrice";
            this.BrokerPrice.ReadOnly = true;
            this.BrokerPrice.Width = 80;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "备注";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // SaleCarInBillCode
            // 
            this.SaleCarInBillCode.DataPropertyName = "SaleCarInBillCode";
            this.SaleCarInBillCode.HeaderText = "入场单号";
            this.SaleCarInBillCode.Name = "SaleCarInBillCode";
            this.SaleCarInBillCode.ReadOnly = true;
            // 
            // CalculateType
            // 
            this.CalculateType.DataPropertyName = "CalculateType";
            this.CalculateType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.CalculateType.FieldName = "CalculateType";
            this.CalculateType.HeaderText = "计价方式";
            this.CalculateType.Name = "CalculateType";
            this.CalculateType.ReadOnly = true;
            // 
            // CancelDesc
            // 
            this.CancelDesc.DataPropertyName = "CancelDesc";
            this.CancelDesc.HeaderText = "作废原因";
            this.CancelDesc.Name = "CancelDesc";
            this.CancelDesc.ReadOnly = true;
            // 
            // BillDateOut
            // 
            this.BillDateOut.DataPropertyName = "BillDateOut";
            this.BillDateOut.HeaderText = "出场过磅时间";
            this.BillDateOut.Name = "BillDateOut";
            this.BillDateOut.ReadOnly = true;
            this.BillDateOut.Width = 120;
            // 
            // BillDateIn
            // 
            this.BillDateIn.DataPropertyName = "BillDateIn";
            this.BillDateIn.HeaderText = "入场过磅时间";
            this.BillDateIn.Name = "BillDateIn";
            this.BillDateIn.ReadOnly = true;
            this.BillDateIn.Width = 120;
            // 
            // CreateByIn
            // 
            this.CreateByIn.DataPropertyName = "CreateByIn";
            this.CreateByIn.HeaderText = "入磅创建人";
            this.CreateByIn.Name = "CreateByIn";
            this.CreateByIn.ReadOnly = true;
            // 
            // CreateTimeIn
            // 
            this.CreateTimeIn.DataPropertyName = "CreateTimeIn";
            this.CreateTimeIn.HeaderText = "入磅创建时间";
            this.CreateTimeIn.Name = "CreateTimeIn";
            this.CreateTimeIn.ReadOnly = true;
            this.CreateTimeIn.Width = 150;
            // 
            // CreateByOut
            // 
            this.CreateByOut.DataPropertyName = "CreateByOut";
            this.CreateByOut.HeaderText = "出磅创建人";
            this.CreateByOut.Name = "CreateByOut";
            this.CreateByOut.ReadOnly = true;
            // 
            // CreateTimeOut
            // 
            this.CreateTimeOut.DataPropertyName = "CreateTimeOut";
            this.CreateTimeOut.HeaderText = "出磅创建时间";
            this.CreateTimeOut.Name = "CreateTimeOut";
            this.CreateTimeOut.ReadOnly = true;
            this.CreateTimeOut.Width = 150;
            // 
            // SaleCarInBillID
            // 
            this.SaleCarInBillID.DataPropertyName = "SaleCarInBillID";
            this.SaleCarInBillID.HeaderText = "SaleCarInBillID";
            this.SaleCarInBillID.Name = "SaleCarInBillID";
            this.SaleCarInBillID.ReadOnly = true;
            this.SaleCarInBillID.Visible = false;
            // 
            // ChangeDetail
            // 
            this.ChangeDetail.DataPropertyName = "ChangeDetail";
            this.ChangeDetail.HeaderText = "直接改单日志";
            this.ChangeDetail.Name = "ChangeDetail";
            this.ChangeDetail.ReadOnly = true;
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
            this.btnSearch.Location = new System.Drawing.Point(877, 115);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCustomerTypeID);
            this.panel1.Controls.Add(this.skinLabel19);
            this.panel1.Controls.Add(this.txtBillCodeOut);
            this.panel1.Controls.Add(this.skinLabel18);
            this.panel1.Controls.Add(this.btnSumSearch);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.cbUseOutDate);
            this.panel1.Controls.Add(this.cbUseInDate);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtInBillTimeTo);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtInBillDateTo);
            this.panel1.Controls.Add(this.txtOutBillTimeTo);
            this.panel1.Controls.Add(this.txtOutBillDateTo);
            this.panel1.Controls.Add(this.txtOutBillTimeFrom);
            this.panel1.Controls.Add(this.txtOutBillDateFrom);
            this.panel1.Controls.Add(this.txtInBillTimeFrom);
            this.panel1.Controls.Add(this.txtInBillDateFrom);
            this.panel1.Controls.Add(this.txtOutBillCraeteBy);
            this.panel1.Controls.Add(this.skinLabel9);
            this.panel1.Controls.Add(this.txtItemID);
            this.panel1.Controls.Add(this.skinLabel8);
            this.panel1.Controls.Add(this.skinLabel6);
            this.panel1.Controls.Add(this.skinLabel7);
            this.panel1.Controls.Add(this.skinLabel5);
            this.panel1.Controls.Add(this.skinLabel4);
            this.panel1.Controls.Add(this.txtBillCode);
            this.panel1.Controls.Add(this.skinLabel3);
            this.panel1.Controls.Add(this.txtCarID);
            this.panel1.Controls.Add(this.skinLabel2);
            this.panel1.Controls.Add(this.txtCustomerID);
            this.panel1.Controls.Add(this.skinLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1296, 218);
            this.panel1.TabIndex = 32;
            // 
            // txtCustomerTypeID
            // 
            this.txtCustomerTypeID.CanBeEmpty = false;
            this.txtCustomerTypeID.Caption = "";
            this.txtCustomerTypeID.DM_UseSelectable = true;
            this.txtCustomerTypeID.FormattingEnabled = true;
            this.txtCustomerTypeID.ItemHeight = 24;
            this.txtCustomerTypeID.Location = new System.Drawing.Point(97, 43);
            this.txtCustomerTypeID.Name = "txtCustomerTypeID";
            this.txtCustomerTypeID.Size = new System.Drawing.Size(167, 30);
            this.txtCustomerTypeID.TabIndex = 69;
            // 
            // skinLabel19
            // 
            this.skinLabel19.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel19.BorderColor = System.Drawing.Color.White;
            this.skinLabel19.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel19.Location = new System.Drawing.Point(3, 46);
            this.skinLabel19.Name = "skinLabel19";
            this.skinLabel19.Size = new System.Drawing.Size(91, 21);
            this.skinLabel19.TabIndex = 68;
            this.skinLabel19.Text = "客户类型";
            this.skinLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillCodeOut
            // 
            this.txtBillCodeOut.BackColor = System.Drawing.Color.Transparent;
            this.txtBillCodeOut.CanBeEmpty = true;
            this.txtBillCodeOut.Caption = "";
            this.txtBillCodeOut.DownBack = null;
            this.txtBillCodeOut.Icon = null;
            this.txtBillCodeOut.IconIsButton = false;
            this.txtBillCodeOut.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCodeOut.IsPasswordChat = '\0';
            this.txtBillCodeOut.IsSystemPasswordChar = false;
            this.txtBillCodeOut.Lines = new string[0];
            this.txtBillCodeOut.Location = new System.Drawing.Point(620, 44);
            this.txtBillCodeOut.Margin = new System.Windows.Forms.Padding(0);
            this.txtBillCodeOut.MaxLength = 32767;
            this.txtBillCodeOut.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtBillCodeOut.MouseBack = null;
            this.txtBillCodeOut.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCodeOut.Multiline = true;
            this.txtBillCodeOut.Name = "txtBillCodeOut";
            this.txtBillCodeOut.NormlBack = null;
            this.txtBillCodeOut.Padding = new System.Windows.Forms.Padding(5);
            this.txtBillCodeOut.ReadOnly = false;
            this.txtBillCodeOut.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBillCodeOut.Size = new System.Drawing.Size(148, 29);
            // 
            // 
            // 
            this.txtBillCodeOut.SkinTxt.AccessibleName = "";
            this.txtBillCodeOut.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtBillCodeOut.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBillCodeOut.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillCodeOut.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillCodeOut.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillCodeOut.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtBillCodeOut.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtBillCodeOut.SkinTxt.Multiline = true;
            this.txtBillCodeOut.SkinTxt.Name = "BaseText";
            this.txtBillCodeOut.SkinTxt.Size = new System.Drawing.Size(138, 19);
            this.txtBillCodeOut.SkinTxt.TabIndex = 0;
            this.txtBillCodeOut.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCodeOut.SkinTxt.WaterText = "";
            this.txtBillCodeOut.TabIndex = 67;
            this.txtBillCodeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBillCodeOut.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCodeOut.WaterText = "";
            this.txtBillCodeOut.WordWrap = true;
            // 
            // skinLabel18
            // 
            this.skinLabel18.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel18.BorderColor = System.Drawing.Color.White;
            this.skinLabel18.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel18.Location = new System.Drawing.Point(536, 46);
            this.skinLabel18.Name = "skinLabel18";
            this.skinLabel18.Size = new System.Drawing.Size(73, 21);
            this.skinLabel18.TabIndex = 66;
            this.skinLabel18.Text = "出场单号";
            this.skinLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSumSearch
            // 
            this.btnSumSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSumSearch.BaseColor = System.Drawing.Color.LightGray;
            this.btnSumSearch.BorderColor = System.Drawing.Color.Gray;
            this.btnSumSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSumSearch.DownBack = null;
            this.btnSumSearch.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnSumSearch.LBPermissionCode = "";
            this.btnSumSearch.Location = new System.Drawing.Point(457, 159);
            this.btnSumSearch.MouseBack = null;
            this.btnSumSearch.Name = "btnSumSearch";
            this.btnSumSearch.NormlBack = null;
            this.btnSumSearch.Size = new System.Drawing.Size(97, 44);
            this.btnSumSearch.TabIndex = 65;
            this.btnSumSearch.Text = "统计分析";
            this.btnSumSearch.UseVisualStyleBackColor = false;
            this.btnSumSearch.Click += new System.EventHandler(this.btnSumSearch_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.skinLabel17);
            this.groupBox4.Controls.Add(this.cbSumItem);
            this.groupBox4.Controls.Add(this.skinLabel16);
            this.groupBox4.Controls.Add(this.cbSumCar);
            this.groupBox4.Controls.Add(this.skinLabel15);
            this.groupBox4.Controls.Add(this.cbSumCustomer);
            this.groupBox4.Location = new System.Drawing.Point(3, 155);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(448, 46);
            this.groupBox4.TabIndex = 64;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "汇总统计条件";
            // 
            // skinLabel17
            // 
            this.skinLabel17.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel17.BorderColor = System.Drawing.Color.White;
            this.skinLabel17.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel17.Location = new System.Drawing.Point(293, 20);
            this.skinLabel17.Name = "skinLabel17";
            this.skinLabel17.Size = new System.Drawing.Size(93, 21);
            this.skinLabel17.TabIndex = 68;
            this.skinLabel17.Text = "按货物名称";
            this.skinLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSumItem
            // 
            this.cbSumItem.Location = new System.Drawing.Point(264, 18);
            this.cbSumItem.Name = "cbSumItem";
            this.cbSumItem.Size = new System.Drawing.Size(23, 27);
            this.cbSumItem.TabIndex = 67;
            this.cbSumItem.UseVisualStyleBackColor = true;
            // 
            // skinLabel16
            // 
            this.skinLabel16.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel16.BorderColor = System.Drawing.Color.White;
            this.skinLabel16.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel16.Location = new System.Drawing.Point(163, 20);
            this.skinLabel16.Name = "skinLabel16";
            this.skinLabel16.Size = new System.Drawing.Size(67, 21);
            this.skinLabel16.TabIndex = 66;
            this.skinLabel16.Text = "按车牌";
            this.skinLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSumCar
            // 
            this.cbSumCar.Location = new System.Drawing.Point(134, 18);
            this.cbSumCar.Name = "cbSumCar";
            this.cbSumCar.Size = new System.Drawing.Size(23, 27);
            this.cbSumCar.TabIndex = 65;
            this.cbSumCar.UseVisualStyleBackColor = true;
            // 
            // skinLabel15
            // 
            this.skinLabel15.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel15.BorderColor = System.Drawing.Color.White;
            this.skinLabel15.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel15.Location = new System.Drawing.Point(45, 20);
            this.skinLabel15.Name = "skinLabel15";
            this.skinLabel15.Size = new System.Drawing.Size(70, 21);
            this.skinLabel15.TabIndex = 64;
            this.skinLabel15.Text = "按客户";
            this.skinLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSumCustomer
            // 
            this.cbSumCustomer.Location = new System.Drawing.Point(16, 18);
            this.cbSumCustomer.Name = "cbSumCustomer";
            this.cbSumCustomer.Size = new System.Drawing.Size(23, 27);
            this.cbSumCustomer.TabIndex = 63;
            this.cbSumCustomer.UseVisualStyleBackColor = true;
            // 
            // cbUseOutDate
            // 
            this.cbUseOutDate.Checked = true;
            this.cbUseOutDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseOutDate.Location = new System.Drawing.Point(10, 80);
            this.cbUseOutDate.Name = "cbUseOutDate";
            this.cbUseOutDate.Size = new System.Drawing.Size(23, 27);
            this.cbUseOutDate.TabIndex = 63;
            this.cbUseOutDate.UseVisualStyleBackColor = true;
            // 
            // cbUseInDate
            // 
            this.cbUseInDate.Location = new System.Drawing.Point(625, 79);
            this.cbUseInDate.Name = "cbUseInDate";
            this.cbUseInDate.Size = new System.Drawing.Size(23, 27);
            this.cbUseInDate.TabIndex = 62;
            this.cbUseInDate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbUnApprove);
            this.groupBox3.Controls.Add(this.rbApproved);
            this.groupBox3.Controls.Add(this.rbApproveAll);
            this.groupBox3.Location = new System.Drawing.Point(583, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 41);
            this.groupBox3.TabIndex = 61;
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
            this.groupBox2.Location = new System.Drawing.Point(295, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 41);
            this.groupBox2.TabIndex = 60;
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
            this.rbUnCancel.Checked = true;
            this.rbUnCancel.Location = new System.Drawing.Point(101, 20);
            this.rbUnCancel.Name = "rbUnCancel";
            this.rbUnCancel.Size = new System.Drawing.Size(83, 16);
            this.rbUnCancel.TabIndex = 1;
            this.rbUnCancel.TabStop = true;
            this.rbUnCancel.Text = "未作废记录";
            this.rbUnCancel.UseVisualStyleBackColor = true;
            // 
            // rbCancelAll
            // 
            this.rbCancelAll.AutoSize = true;
            this.rbCancelAll.Location = new System.Drawing.Point(16, 20);
            this.rbCancelAll.Name = "rbCancelAll";
            this.rbCancelAll.Size = new System.Drawing.Size(71, 16);
            this.rbCancelAll.TabIndex = 0;
            this.rbCancelAll.Text = "全部记录";
            this.rbCancelAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUnFinish);
            this.groupBox1.Controls.Add(this.rbFinished);
            this.groupBox1.Controls.Add(this.rbFinishAll);
            this.groupBox1.Location = new System.Drawing.Point(7, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 41);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "完成状态";
            // 
            // rbUnFinish
            // 
            this.rbUnFinish.AutoSize = true;
            this.rbUnFinish.Location = new System.Drawing.Point(189, 20);
            this.rbUnFinish.Name = "rbUnFinish";
            this.rbUnFinish.Size = new System.Drawing.Size(83, 16);
            this.rbUnFinish.TabIndex = 2;
            this.rbUnFinish.Text = "未完成记录";
            this.rbUnFinish.UseVisualStyleBackColor = true;
            // 
            // rbFinished
            // 
            this.rbFinished.AutoSize = true;
            this.rbFinished.Checked = true;
            this.rbFinished.Location = new System.Drawing.Point(105, 20);
            this.rbFinished.Name = "rbFinished";
            this.rbFinished.Size = new System.Drawing.Size(71, 16);
            this.rbFinished.TabIndex = 1;
            this.rbFinished.TabStop = true;
            this.rbFinished.Text = "完成记录";
            this.rbFinished.UseVisualStyleBackColor = true;
            // 
            // rbFinishAll
            // 
            this.rbFinishAll.AutoSize = true;
            this.rbFinishAll.Location = new System.Drawing.Point(16, 20);
            this.rbFinishAll.Name = "rbFinishAll";
            this.rbFinishAll.Size = new System.Drawing.Size(71, 16);
            this.rbFinishAll.TabIndex = 0;
            this.rbFinishAll.Text = "全部记录";
            this.rbFinishAll.UseVisualStyleBackColor = true;
            // 
            // txtInBillTimeTo
            // 
            this.txtInBillTimeTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtInBillTimeTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtInBillTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtInBillTimeTo.Location = new System.Drawing.Point(1084, 79);
            this.txtInBillTimeTo.Name = "txtInBillTimeTo";
            this.txtInBillTimeTo.Size = new System.Drawing.Size(106, 26);
            this.txtInBillTimeTo.TabIndex = 58;
            // 
            // txtInBillDateTo
            // 
            this.txtInBillDateTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtInBillDateTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtInBillDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInBillDateTo.Location = new System.Drawing.Point(979, 80);
            this.txtInBillDateTo.Name = "txtInBillDateTo";
            this.txtInBillDateTo.Size = new System.Drawing.Size(99, 26);
            this.txtInBillDateTo.TabIndex = 57;
            // 
            // txtOutBillTimeTo
            // 
            this.txtOutBillTimeTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtOutBillTimeTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtOutBillTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtOutBillTimeTo.Location = new System.Drawing.Point(511, 79);
            this.txtOutBillTimeTo.Name = "txtOutBillTimeTo";
            this.txtOutBillTimeTo.Size = new System.Drawing.Size(85, 26);
            this.txtOutBillTimeTo.TabIndex = 56;
            // 
            // txtOutBillDateTo
            // 
            this.txtOutBillDateTo.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtOutBillDateTo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtOutBillDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtOutBillDateTo.Location = new System.Drawing.Point(380, 79);
            this.txtOutBillDateTo.Name = "txtOutBillDateTo";
            this.txtOutBillDateTo.Size = new System.Drawing.Size(125, 26);
            this.txtOutBillDateTo.TabIndex = 55;
            // 
            // txtOutBillTimeFrom
            // 
            this.txtOutBillTimeFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtOutBillTimeFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtOutBillTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtOutBillTimeFrom.Location = new System.Drawing.Point(241, 78);
            this.txtOutBillTimeFrom.Name = "txtOutBillTimeFrom";
            this.txtOutBillTimeFrom.Size = new System.Drawing.Size(96, 26);
            this.txtOutBillTimeFrom.TabIndex = 54;
            // 
            // txtOutBillDateFrom
            // 
            this.txtOutBillDateFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtOutBillDateFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtOutBillDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtOutBillDateFrom.Location = new System.Drawing.Point(112, 78);
            this.txtOutBillDateFrom.Name = "txtOutBillDateFrom";
            this.txtOutBillDateFrom.Size = new System.Drawing.Size(123, 26);
            this.txtOutBillDateFrom.TabIndex = 53;
            // 
            // txtInBillTimeFrom
            // 
            this.txtInBillTimeFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtInBillTimeFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtInBillTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtInBillTimeFrom.Location = new System.Drawing.Point(853, 80);
            this.txtInBillTimeFrom.Name = "txtInBillTimeFrom";
            this.txtInBillTimeFrom.Size = new System.Drawing.Size(86, 26);
            this.txtInBillTimeFrom.TabIndex = 52;
            // 
            // txtInBillDateFrom
            // 
            this.txtInBillDateFrom.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtInBillDateFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.txtInBillDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInBillDateFrom.Location = new System.Drawing.Point(738, 80);
            this.txtInBillDateFrom.Name = "txtInBillDateFrom";
            this.txtInBillDateFrom.Size = new System.Drawing.Size(109, 26);
            this.txtInBillDateFrom.TabIndex = 51;
            // 
            // txtOutBillCraeteBy
            // 
            this.txtOutBillCraeteBy.BackColor = System.Drawing.Color.Transparent;
            this.txtOutBillCraeteBy.CanBeEmpty = true;
            this.txtOutBillCraeteBy.Caption = "";
            this.txtOutBillCraeteBy.DownBack = null;
            this.txtOutBillCraeteBy.Icon = null;
            this.txtOutBillCraeteBy.IconIsButton = false;
            this.txtOutBillCraeteBy.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtOutBillCraeteBy.IsPasswordChat = '\0';
            this.txtOutBillCraeteBy.IsSystemPasswordChar = false;
            this.txtOutBillCraeteBy.Lines = new string[0];
            this.txtOutBillCraeteBy.Location = new System.Drawing.Point(887, 8);
            this.txtOutBillCraeteBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtOutBillCraeteBy.MaxLength = 32767;
            this.txtOutBillCraeteBy.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtOutBillCraeteBy.MouseBack = null;
            this.txtOutBillCraeteBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtOutBillCraeteBy.Multiline = true;
            this.txtOutBillCraeteBy.Name = "txtOutBillCraeteBy";
            this.txtOutBillCraeteBy.NormlBack = null;
            this.txtOutBillCraeteBy.Padding = new System.Windows.Forms.Padding(5);
            this.txtOutBillCraeteBy.ReadOnly = false;
            this.txtOutBillCraeteBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOutBillCraeteBy.Size = new System.Drawing.Size(140, 29);
            // 
            // 
            // 
            this.txtOutBillCraeteBy.SkinTxt.AccessibleName = "";
            this.txtOutBillCraeteBy.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtOutBillCraeteBy.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtOutBillCraeteBy.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtOutBillCraeteBy.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutBillCraeteBy.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutBillCraeteBy.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtOutBillCraeteBy.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtOutBillCraeteBy.SkinTxt.Multiline = true;
            this.txtOutBillCraeteBy.SkinTxt.Name = "BaseText";
            this.txtOutBillCraeteBy.SkinTxt.Size = new System.Drawing.Size(130, 19);
            this.txtOutBillCraeteBy.SkinTxt.TabIndex = 0;
            this.txtOutBillCraeteBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtOutBillCraeteBy.SkinTxt.WaterText = "";
            this.txtOutBillCraeteBy.TabIndex = 50;
            this.txtOutBillCraeteBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtOutBillCraeteBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtOutBillCraeteBy.WaterText = "";
            this.txtOutBillCraeteBy.WordWrap = true;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel9.Location = new System.Drawing.Point(785, 11);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(92, 21);
            this.skinLabel9.TabIndex = 49;
            this.skinLabel9.Text = "司磅员包含";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemID
            // 
            this.txtItemID.BackColor = System.Drawing.Color.Transparent;
            this.txtItemID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtItemID.CanBeEmpty = true;
            this.txtItemID.Caption = "客户名称";
            this.txtItemID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtItemID.LBTitle = "  ";
            this.txtItemID.LBTitleVisible = false;
            this.txtItemID.Location = new System.Drawing.Point(351, 44);
            this.txtItemID.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.PopupWidth = 120;
            this.txtItemID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtItemID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtItemID.Size = new System.Drawing.Size(163, 29);
            this.txtItemID.TabIndex = 48;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel8.Location = new System.Drawing.Point(271, 46);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(79, 21);
            this.skinLabel8.TabIndex = 47;
            this.skinLabel8.Text = "货物名称";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel6.Location = new System.Drawing.Point(341, 81);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(28, 21);
            this.skinLabel6.TabIndex = 44;
            this.skinLabel6.Text = "至";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel7.Location = new System.Drawing.Point(26, 81);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(83, 21);
            this.skinLabel7.TabIndex = 41;
            this.skinLabel7.Text = "出场时间";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel5.Location = new System.Drawing.Point(945, 82);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(28, 21);
            this.skinLabel5.TabIndex = 38;
            this.skinLabel5.Text = "至";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel4.Location = new System.Drawing.Point(645, 81);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(87, 21);
            this.skinLabel4.TabIndex = 35;
            this.skinLabel4.Text = "入场时间";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillCode
            // 
            this.txtBillCode.BackColor = System.Drawing.Color.Transparent;
            this.txtBillCode.CanBeEmpty = true;
            this.txtBillCode.Caption = "";
            this.txtBillCode.DownBack = null;
            this.txtBillCode.Icon = null;
            this.txtBillCode.IconIsButton = false;
            this.txtBillCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCode.IsPasswordChat = '\0';
            this.txtBillCode.IsSystemPasswordChar = false;
            this.txtBillCode.Lines = new string[0];
            this.txtBillCode.Location = new System.Drawing.Point(620, 8);
            this.txtBillCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtBillCode.MaxLength = 32767;
            this.txtBillCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtBillCode.MouseBack = null;
            this.txtBillCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBillCode.Multiline = true;
            this.txtBillCode.Name = "txtBillCode";
            this.txtBillCode.NormlBack = null;
            this.txtBillCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtBillCode.ReadOnly = false;
            this.txtBillCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBillCode.Size = new System.Drawing.Size(148, 29);
            // 
            // 
            // 
            this.txtBillCode.SkinTxt.AccessibleName = "";
            this.txtBillCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtBillCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBillCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtBillCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtBillCode.SkinTxt.Multiline = true;
            this.txtBillCode.SkinTxt.Name = "BaseText";
            this.txtBillCode.SkinTxt.Size = new System.Drawing.Size(138, 19);
            this.txtBillCode.SkinTxt.TabIndex = 0;
            this.txtBillCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCode.SkinTxt.WaterText = "";
            this.txtBillCode.TabIndex = 34;
            this.txtBillCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBillCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBillCode.WaterText = "";
            this.txtBillCode.WordWrap = true;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel3.Location = new System.Drawing.Point(536, 11);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(73, 21);
            this.skinLabel3.TabIndex = 33;
            this.skinLabel3.Text = "入场单号";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCarID
            // 
            this.txtCarID.BackColor = System.Drawing.Color.Transparent;
            this.txtCarID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCarID.CanBeEmpty = true;
            this.txtCarID.Caption = "客户名称";
            this.txtCarID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarID.LBTitle = "  ";
            this.txtCarID.LBTitleVisible = false;
            this.txtCarID.Location = new System.Drawing.Point(351, 8);
            this.txtCarID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.PopupWidth = 120;
            this.txtCarID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCarID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCarID.Size = new System.Drawing.Size(163, 29);
            this.txtCarID.TabIndex = 32;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel2.Location = new System.Drawing.Point(271, 11);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(77, 21);
            this.skinLabel2.TabIndex = 31;
            this.skinLabel2.Text = "车牌号码";
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
            this.txtCustomerID.Location = new System.Drawing.Point(97, 8);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(167, 29);
            this.txtCustomerID.TabIndex = 30;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel1.Location = new System.Drawing.Point(3, 11);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(91, 21);
            this.skinLabel1.TabIndex = 29;
            this.skinLabel1.Text = "客户名称";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTotalAmount);
            this.panel2.Controls.Add(this.txtTotalCar);
            this.panel2.Controls.Add(this.txtStuffWeight);
            this.panel2.Controls.Add(this.txtTareWeight);
            this.panel2.Controls.Add(this.txtTotalWeight);
            this.panel2.Controls.Add(this.skinLabel14);
            this.panel2.Controls.Add(this.skinLabel13);
            this.panel2.Controls.Add(this.skinLabel12);
            this.panel2.Controls.Add(this.skinLabel11);
            this.panel2.Controls.Add(this.skinLabel10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 413);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1296, 32);
            this.panel2.TabIndex = 33;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.Transparent;
            this.txtTotalAmount.CanBeEmpty = true;
            this.txtTotalAmount.Caption = "";
            this.txtTotalAmount.DownBack = null;
            this.txtTotalAmount.Icon = null;
            this.txtTotalAmount.IconIsButton = false;
            this.txtTotalAmount.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalAmount.IsPasswordChat = '\0';
            this.txtTotalAmount.IsSystemPasswordChar = false;
            this.txtTotalAmount.Lines = new string[0];
            this.txtTotalAmount.Location = new System.Drawing.Point(955, 3);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount.MaxLength = 32767;
            this.txtTotalAmount.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtTotalAmount.MouseBack = null;
            this.txtTotalAmount.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalAmount.Multiline = true;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.NormlBack = null;
            this.txtTotalAmount.Padding = new System.Windows.Forms.Padding(5);
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTotalAmount.Size = new System.Drawing.Size(118, 29);
            // 
            // 
            // 
            this.txtTotalAmount.SkinTxt.AccessibleName = "";
            this.txtTotalAmount.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtTotalAmount.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtTotalAmount.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTotalAmount.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalAmount.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalAmount.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtTotalAmount.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtTotalAmount.SkinTxt.Multiline = true;
            this.txtTotalAmount.SkinTxt.Name = "BaseText";
            this.txtTotalAmount.SkinTxt.ReadOnly = true;
            this.txtTotalAmount.SkinTxt.Size = new System.Drawing.Size(108, 19);
            this.txtTotalAmount.SkinTxt.TabIndex = 0;
            this.txtTotalAmount.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalAmount.SkinTxt.WaterText = "";
            this.txtTotalAmount.TabIndex = 44;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalAmount.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalAmount.WaterText = "";
            this.txtTotalAmount.WordWrap = true;
            // 
            // txtTotalCar
            // 
            this.txtTotalCar.BackColor = System.Drawing.Color.Transparent;
            this.txtTotalCar.CanBeEmpty = true;
            this.txtTotalCar.Caption = "";
            this.txtTotalCar.DownBack = null;
            this.txtTotalCar.Icon = null;
            this.txtTotalCar.IconIsButton = false;
            this.txtTotalCar.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalCar.IsPasswordChat = '\0';
            this.txtTotalCar.IsSystemPasswordChar = false;
            this.txtTotalCar.Lines = new string[0];
            this.txtTotalCar.Location = new System.Drawing.Point(759, 3);
            this.txtTotalCar.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalCar.MaxLength = 32767;
            this.txtTotalCar.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtTotalCar.MouseBack = null;
            this.txtTotalCar.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalCar.Multiline = true;
            this.txtTotalCar.Name = "txtTotalCar";
            this.txtTotalCar.NormlBack = null;
            this.txtTotalCar.Padding = new System.Windows.Forms.Padding(5);
            this.txtTotalCar.ReadOnly = true;
            this.txtTotalCar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTotalCar.Size = new System.Drawing.Size(118, 29);
            // 
            // 
            // 
            this.txtTotalCar.SkinTxt.AccessibleName = "";
            this.txtTotalCar.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtTotalCar.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtTotalCar.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTotalCar.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalCar.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalCar.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtTotalCar.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtTotalCar.SkinTxt.Multiline = true;
            this.txtTotalCar.SkinTxt.Name = "BaseText";
            this.txtTotalCar.SkinTxt.ReadOnly = true;
            this.txtTotalCar.SkinTxt.Size = new System.Drawing.Size(108, 19);
            this.txtTotalCar.SkinTxt.TabIndex = 0;
            this.txtTotalCar.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalCar.SkinTxt.WaterText = "";
            this.txtTotalCar.TabIndex = 43;
            this.txtTotalCar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalCar.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalCar.WaterText = "";
            this.txtTotalCar.WordWrap = true;
            // 
            // txtStuffWeight
            // 
            this.txtStuffWeight.BackColor = System.Drawing.Color.Transparent;
            this.txtStuffWeight.CanBeEmpty = true;
            this.txtStuffWeight.Caption = "";
            this.txtStuffWeight.DownBack = null;
            this.txtStuffWeight.Icon = null;
            this.txtStuffWeight.IconIsButton = false;
            this.txtStuffWeight.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtStuffWeight.IsPasswordChat = '\0';
            this.txtStuffWeight.IsSystemPasswordChar = false;
            this.txtStuffWeight.Lines = new string[0];
            this.txtStuffWeight.Location = new System.Drawing.Point(580, 3);
            this.txtStuffWeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtStuffWeight.MaxLength = 32767;
            this.txtStuffWeight.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtStuffWeight.MouseBack = null;
            this.txtStuffWeight.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtStuffWeight.Multiline = true;
            this.txtStuffWeight.Name = "txtStuffWeight";
            this.txtStuffWeight.NormlBack = null;
            this.txtStuffWeight.Padding = new System.Windows.Forms.Padding(5);
            this.txtStuffWeight.ReadOnly = true;
            this.txtStuffWeight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStuffWeight.Size = new System.Drawing.Size(118, 29);
            // 
            // 
            // 
            this.txtStuffWeight.SkinTxt.AccessibleName = "";
            this.txtStuffWeight.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtStuffWeight.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtStuffWeight.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtStuffWeight.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStuffWeight.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStuffWeight.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtStuffWeight.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtStuffWeight.SkinTxt.Multiline = true;
            this.txtStuffWeight.SkinTxt.Name = "BaseText";
            this.txtStuffWeight.SkinTxt.ReadOnly = true;
            this.txtStuffWeight.SkinTxt.Size = new System.Drawing.Size(108, 19);
            this.txtStuffWeight.SkinTxt.TabIndex = 0;
            this.txtStuffWeight.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtStuffWeight.SkinTxt.WaterText = "";
            this.txtStuffWeight.TabIndex = 42;
            this.txtStuffWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtStuffWeight.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtStuffWeight.WaterText = "";
            this.txtStuffWeight.WordWrap = true;
            // 
            // txtTareWeight
            // 
            this.txtTareWeight.BackColor = System.Drawing.Color.Transparent;
            this.txtTareWeight.CanBeEmpty = true;
            this.txtTareWeight.Caption = "";
            this.txtTareWeight.DownBack = null;
            this.txtTareWeight.Icon = null;
            this.txtTareWeight.IconIsButton = false;
            this.txtTareWeight.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTareWeight.IsPasswordChat = '\0';
            this.txtTareWeight.IsSystemPasswordChar = false;
            this.txtTareWeight.Lines = new string[0];
            this.txtTareWeight.Location = new System.Drawing.Point(351, 3);
            this.txtTareWeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtTareWeight.MaxLength = 32767;
            this.txtTareWeight.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtTareWeight.MouseBack = null;
            this.txtTareWeight.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTareWeight.Multiline = true;
            this.txtTareWeight.Name = "txtTareWeight";
            this.txtTareWeight.NormlBack = null;
            this.txtTareWeight.Padding = new System.Windows.Forms.Padding(5);
            this.txtTareWeight.ReadOnly = true;
            this.txtTareWeight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTareWeight.Size = new System.Drawing.Size(118, 29);
            // 
            // 
            // 
            this.txtTareWeight.SkinTxt.AccessibleName = "";
            this.txtTareWeight.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtTareWeight.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtTareWeight.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTareWeight.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTareWeight.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTareWeight.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtTareWeight.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtTareWeight.SkinTxt.Multiline = true;
            this.txtTareWeight.SkinTxt.Name = "BaseText";
            this.txtTareWeight.SkinTxt.ReadOnly = true;
            this.txtTareWeight.SkinTxt.Size = new System.Drawing.Size(108, 19);
            this.txtTareWeight.SkinTxt.TabIndex = 0;
            this.txtTareWeight.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTareWeight.SkinTxt.WaterText = "";
            this.txtTareWeight.TabIndex = 42;
            this.txtTareWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTareWeight.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTareWeight.WaterText = "";
            this.txtTareWeight.WordWrap = true;
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.BackColor = System.Drawing.Color.Transparent;
            this.txtTotalWeight.CanBeEmpty = true;
            this.txtTotalWeight.Caption = "";
            this.txtTotalWeight.DownBack = null;
            this.txtTotalWeight.Icon = null;
            this.txtTotalWeight.IconIsButton = false;
            this.txtTotalWeight.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalWeight.IsPasswordChat = '\0';
            this.txtTotalWeight.IsSystemPasswordChar = false;
            this.txtTotalWeight.Lines = new string[0];
            this.txtTotalWeight.Location = new System.Drawing.Point(109, 3);
            this.txtTotalWeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalWeight.MaxLength = 32767;
            this.txtTotalWeight.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtTotalWeight.MouseBack = null;
            this.txtTotalWeight.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTotalWeight.Multiline = true;
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.NormlBack = null;
            this.txtTotalWeight.Padding = new System.Windows.Forms.Padding(5);
            this.txtTotalWeight.ReadOnly = true;
            this.txtTotalWeight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTotalWeight.Size = new System.Drawing.Size(118, 29);
            // 
            // 
            // 
            this.txtTotalWeight.SkinTxt.AccessibleName = "";
            this.txtTotalWeight.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtTotalWeight.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtTotalWeight.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTotalWeight.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalWeight.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalWeight.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtTotalWeight.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtTotalWeight.SkinTxt.Multiline = true;
            this.txtTotalWeight.SkinTxt.Name = "BaseText";
            this.txtTotalWeight.SkinTxt.ReadOnly = true;
            this.txtTotalWeight.SkinTxt.Size = new System.Drawing.Size(108, 19);
            this.txtTotalWeight.SkinTxt.TabIndex = 0;
            this.txtTotalWeight.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalWeight.SkinTxt.WaterText = "";
            this.txtTotalWeight.TabIndex = 41;
            this.txtTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalWeight.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTotalWeight.WaterText = "";
            this.txtTotalWeight.WordWrap = true;
            // 
            // skinLabel14
            // 
            this.skinLabel14.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel14.BorderColor = System.Drawing.Color.White;
            this.skinLabel14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel14.Location = new System.Drawing.Point(872, 6);
            this.skinLabel14.Name = "skinLabel14";
            this.skinLabel14.Size = new System.Drawing.Size(80, 21);
            this.skinLabel14.TabIndex = 40;
            this.skinLabel14.Text = "金额合计";
            this.skinLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel13
            // 
            this.skinLabel13.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel13.BorderColor = System.Drawing.Color.White;
            this.skinLabel13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel13.Location = new System.Drawing.Point(691, 6);
            this.skinLabel13.Name = "skinLabel13";
            this.skinLabel13.Size = new System.Drawing.Size(65, 21);
            this.skinLabel13.TabIndex = 39;
            this.skinLabel13.Text = "总车数";
            this.skinLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel12
            // 
            this.skinLabel12.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel12.BorderColor = System.Drawing.Color.White;
            this.skinLabel12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel12.Location = new System.Drawing.Point(470, 6);
            this.skinLabel12.Name = "skinLabel12";
            this.skinLabel12.Size = new System.Drawing.Size(107, 21);
            this.skinLabel12.TabIndex = 38;
            this.skinLabel12.Text = "净重合计(吨)";
            this.skinLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel11.Location = new System.Drawing.Point(245, 6);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(103, 21);
            this.skinLabel11.TabIndex = 37;
            this.skinLabel11.Text = "皮重合计(吨)";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.skinLabel10.Location = new System.Drawing.Point(7, 6);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(99, 21);
            this.skinLabel10.TabIndex = 36;
            this.skinLabel10.Text = "毛重合计(吨)";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdSumMain);
            this.pnlMain.Controls.Add(this.grdMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 258);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1296, 155);
            this.pnlMain.TabIndex = 34;
            // 
            // grdSumMain
            // 
            this.grdSumMain.AllowUserToAddRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.grdSumMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.grdSumMain.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdSumMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdSumMain.ColumnFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdSumMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grdSumMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.grdSumMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSumMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerNameSum,
            this.CarNumSum,
            this.ItemNameSum,
            this.CarCount,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9});
            this.grdSumMain.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSumMain.DefaultCellStyle = dataGridViewCellStyle12;
            this.grdSumMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdSumMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdSumMain.EnableHeadersVisualStyles = false;
            this.grdSumMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.grdSumMain.HeadFont = null;
            this.grdSumMain.HeadForeColor = System.Drawing.Color.Empty;
            this.grdSumMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            this.grdSumMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            this.grdSumMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            this.grdSumMain.Location = new System.Drawing.Point(727, 0);
            this.grdSumMain.Name = "grdSumMain";
            this.grdSumMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdSumMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdSumMain.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.grdSumMain.RowTemplate.Height = 23;
            this.grdSumMain.Size = new System.Drawing.Size(300, 155);
            this.grdSumMain.TabIndex = 10;
            this.grdSumMain.TitleBack = null;
            this.grdSumMain.TitleBackColorBegin = System.Drawing.Color.White;
            this.grdSumMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
            // 
            // CustomerNameSum
            // 
            this.CustomerNameSum.DataPropertyName = "CustomerName";
            this.CustomerNameSum.HeaderText = "客户名称";
            this.CustomerNameSum.Name = "CustomerNameSum";
            this.CustomerNameSum.ReadOnly = true;
            // 
            // CarNumSum
            // 
            this.CarNumSum.DataPropertyName = "CarNum";
            this.CarNumSum.HeaderText = "车号";
            this.CarNumSum.Name = "CarNumSum";
            this.CarNumSum.ReadOnly = true;
            // 
            // ItemNameSum
            // 
            this.ItemNameSum.DataPropertyName = "ItemName";
            this.ItemNameSum.HeaderText = "货物名称";
            this.ItemNameSum.Name = "ItemNameSum";
            this.ItemNameSum.ReadOnly = true;
            // 
            // CarCount
            // 
            this.CarCount.DataPropertyName = "CarCount";
            this.CarCount.HeaderText = "车次";
            this.CarCount.Name = "CarCount";
            this.CarCount.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "TotalWeight";
            this.dataGridViewTextBoxColumn5.HeaderText = "毛重";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "CarTare";
            this.dataGridViewTextBoxColumn6.HeaderText = "皮重";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "SuttleWeight";
            this.dataGridViewTextBoxColumn7.HeaderText = "净重";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Amount";
            this.dataGridViewTextBoxColumn9.HeaderText = "金额";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
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
            this.btnApprove,
            this.btnUnApprove,
            this.toolStripSeparator4,
            this.btnCancel,
            this.btnUnCancel,
            this.toolStripSeparator2,
            this.btnAddInBill,
            this.btnAddOutBill,
            this.toolStripSeparator3,
            this.btnSynToK3,
            this.btnDeleteBill});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(1296, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 7;
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
            // btnApprove
            // 
            this.btnApprove.Image = global::LB.MI.Properties.Resources.btnApprove;
            this.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprove.LBPermissionCode = "SalesManager_Approve";
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(60, 37);
            this.btnApprove.Text = "批量审核";
            this.btnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnUnApprove
            // 
            this.btnUnApprove.Image = global::LB.MI.Properties.Resources.btnUnApprove;
            this.btnUnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnApprove.LBPermissionCode = "SalesManager_UnApprove";
            this.btnUnApprove.Name = "btnUnApprove";
            this.btnUnApprove.Size = new System.Drawing.Size(84, 37);
            this.btnUnApprove.Text = "批量取消审核";
            this.btnUnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnApprove.Click += new System.EventHandler(this.btnUnApprove_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::LB.MI.Properties.Resources.btnUnPostInUse;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.LBPermissionCode = "SalesManager_Cancel";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 37);
            this.btnCancel.Text = "批量作废";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUnCancel
            // 
            this.btnUnCancel.Image = global::LB.MI.Properties.Resources.btnReset;
            this.btnUnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnCancel.LBPermissionCode = "SalesManager_UnCancel";
            this.btnUnCancel.Name = "btnUnCancel";
            this.btnUnCancel.Size = new System.Drawing.Size(84, 37);
            this.btnUnCancel.Text = "批量取消作废";
            this.btnUnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnCancel.Click += new System.EventHandler(this.btnUnCancel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // btnAddInBill
            // 
            this.btnAddInBill.Image = global::LB.MI.Properties.Resources.btnEnterEdit;
            this.btnAddInBill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddInBill.LBPermissionCode = "SalesManager_AddInBill";
            this.btnAddInBill.Name = "btnAddInBill";
            this.btnAddInBill.Size = new System.Drawing.Size(108, 37);
            this.btnAddInBill.Text = "手工添加入场记录";
            this.btnAddInBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddInBill.Click += new System.EventHandler(this.btnAddInBill_Click);
            // 
            // btnAddOutBill
            // 
            this.btnAddOutBill.Image = global::LB.MI.Properties.Resources.btnEnterEdit;
            this.btnAddOutBill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddOutBill.LBPermissionCode = "SalesManager_AddOutBill";
            this.btnAddOutBill.Name = "btnAddOutBill";
            this.btnAddOutBill.Size = new System.Drawing.Size(108, 37);
            this.btnAddOutBill.Text = "手工添加出场记录";
            this.btnAddOutBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddOutBill.Click += new System.EventHandler(this.btnAddOutBill_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // btnSynToK3
            // 
            this.btnSynToK3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSynToK3.Image = ((System.Drawing.Image)(resources.GetObject("btnSynToK3.Image")));
            this.btnSynToK3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSynToK3.Name = "btnSynToK3";
            this.btnSynToK3.Size = new System.Drawing.Size(51, 37);
            this.btnSynToK3.Text = "同步K3";
            this.btnSynToK3.Visible = false;
            this.btnSynToK3.Click += new System.EventHandler(this.btnSynToK3_Click);
            // 
            // btnDeleteBill
            // 
            this.btnDeleteBill.Image = global::LB.MI.Properties.Resources.btnEdit;
            this.btnDeleteBill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteBill.LBPermissionCode = "SalesManager_DeleteBill";
            this.btnDeleteBill.Name = "btnDeleteBill";
            this.btnDeleteBill.Size = new System.Drawing.Size(84, 37);
            this.btnDeleteBill.Text = "删除选中榜单";
            this.btnDeleteBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteBill.Click += new System.EventHandler(this.btnDeleteBill_Click);
            // 
            // frmSaleCarInOutBillManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "磅单管理";
            this.Name = "frmSaleCarInOutBillManager";
            this.Size = new System.Drawing.Size(1296, 445);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSumMain)).EndInit();
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnReflesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LBDataGridView grdMain;
        private Controls.LBToolStripButton btnUnApprove;
        private Controls.LBToolStripButton btnApprove;
        private Controls.LBToolStripButton btnCancel;
        private Controls.LBToolStripButton btnUnCancel;
        private Controls.LBSkinButton btnSearch;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private Controls.LBTextBox.CoolTextBox txtCarID;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private Controls.LBSkinTextBox txtBillCode;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private Controls.LBTextBox.CoolTextBox txtItemID;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private Controls.LBSkinTextBox txtOutBillCraeteBy;
        private System.Windows.Forms.DateTimePicker txtInBillDateFrom;
        private System.Windows.Forms.DateTimePicker txtInBillTimeFrom;
        private System.Windows.Forms.DateTimePicker txtOutBillTimeFrom;
        private System.Windows.Forms.DateTimePicker txtOutBillDateFrom;
        private System.Windows.Forms.DateTimePicker txtInBillTimeTo;
        private System.Windows.Forms.DateTimePicker txtInBillDateTo;
        private System.Windows.Forms.DateTimePicker txtOutBillTimeTo;
        private System.Windows.Forms.DateTimePicker txtOutBillDateTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFinishAll;
        private System.Windows.Forms.RadioButton rbFinished;
        private System.Windows.Forms.RadioButton rbUnFinish;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCanceled;
        private System.Windows.Forms.RadioButton rbUnCancel;
        private System.Windows.Forms.RadioButton rbCancelAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbUnApprove;
        private System.Windows.Forms.RadioButton rbApproved;
        private System.Windows.Forms.RadioButton rbApproveAll;
        private System.Windows.Forms.CheckBox cbUseInDate;
        private System.Windows.Forms.CheckBox cbUseOutDate;
        private System.Windows.Forms.Panel panel2;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private CCWin.SkinControl.SkinLabel skinLabel12;
        private CCWin.SkinControl.SkinLabel skinLabel13;
        private CCWin.SkinControl.SkinLabel skinLabel14;
        private Controls.LBSkinTextBox txtTotalWeight;
        private Controls.LBSkinTextBox txtTotalAmount;
        private Controls.LBSkinTextBox txtTotalCar;
        private Controls.LBSkinTextBox txtStuffWeight;
        private Controls.LBSkinTextBox txtTareWeight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LBToolStripButton btnAddInBill;
        private Controls.LBToolStripButton btnAddOutBill;
        private System.Windows.Forms.GroupBox groupBox4;
        private CCWin.SkinControl.SkinLabel skinLabel15;
        private System.Windows.Forms.CheckBox cbSumCustomer;
        private CCWin.SkinControl.SkinLabel skinLabel16;
        private System.Windows.Forms.CheckBox cbSumCar;
        private CCWin.SkinControl.SkinLabel skinLabel17;
        private System.Windows.Forms.CheckBox cbSumItem;
        private Controls.LBSkinButton btnSumSearch;
        private System.Windows.Forms.Panel pnlMain;
        private Controls.LBDataGridView grdSumMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNameSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNumSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNameSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private CCWin.SkinControl.SkinLabel skinLabel18;
        private Controls.LBSkinTextBox txtBillCodeOut;
        private System.Windows.Forms.ToolStripButton btnSynToK3;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleCarOutBillCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private Controls.LBDataGridViewComboBoxColumn BillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarTare;
        private System.Windows.Forms.DataGridViewTextBoxColumn SuttleWeightT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn FarePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokerPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleCarInBillCode;
        private Controls.LBDataGridViewComboBoxColumn CalculateType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CancelDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDateOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDateIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateByIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTimeIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateByOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleCarInBillID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeDetail;
        private CCWin.SkinControl.SkinLabel skinLabel19;
        private Controls.LBMetroComboBox txtCustomerTypeID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Controls.LBToolStripButton btnDeleteBill;
    }
}
