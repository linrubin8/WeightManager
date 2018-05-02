namespace LB.MI.MI
{
    partial class frmSaleCarInOutBillSynchornous
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaleCarInOutBillSynchornous));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdMain = new LB.Controls.LBDataGridView(this.components);
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnReflesh = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelSelectAll = new LB.Controls.LBToolStripButton(this.components);
            this.btnSelectAll = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSynchronous = new LB.Controls.LBToolStripButton(this.components);
            this.LBSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SynMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleCarOutBillCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillType = new LB.Controls.LBDataGridViewComboBoxColumn();
            this.TotalWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarTare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuttleWeightT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LBSelect,
            this.SynMessage,
            this.SaleCarOutBillCode,
            this.CustomerName,
            this.CarNum,
            this.ItemName,
            this.BillType,
            this.TotalWeight,
            this.CarTare,
            this.SuttleWeightT,
            this.PriceT,
            this.Amount,
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
            this.SaleCarInBillID});
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
            this.grdMain.Location = new System.Drawing.Point(0, 40);
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
            this.grdMain.Size = new System.Drawing.Size(914, 305);
            this.grdMain.TabIndex = 10;
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
            this.toolStripSeparator1,
            this.btnCancelSelectAll,
            this.btnSelectAll,
            this.toolStripSeparator2,
            this.btnSynchronous});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(914, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 8;
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
            // btnCancelSelectAll
            // 
            this.btnCancelSelectAll.Image = global::LB.MI.Properties.Resources.Bool_uncheck;
            this.btnCancelSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelSelectAll.LBPermissionCode = "";
            this.btnCancelSelectAll.Name = "btnCancelSelectAll";
            this.btnCancelSelectAll.Size = new System.Drawing.Size(60, 37);
            this.btnCancelSelectAll.Text = "取消全选";
            this.btnCancelSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelSelectAll.Click += new System.EventHandler(this.btnCancelSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Image = global::LB.MI.Properties.Resources.Bool_checked;
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.LBPermissionCode = "";
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(36, 37);
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // btnSynchronous
            // 
            this.btnSynchronous.Image = global::LB.MI.Properties.Resources.btnVersionDone;
            this.btnSynchronous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSynchronous.LBPermissionCode = "";
            this.btnSynchronous.Name = "btnSynchronous";
            this.btnSynchronous.Size = new System.Drawing.Size(132, 37);
            this.btnSynchronous.Text = "上传选中单据至服务器";
            this.btnSynchronous.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSynchronous.Click += new System.EventHandler(this.btnSynchronous_Click);
            // 
            // LBSelect
            // 
            this.LBSelect.HeaderText = "选择";
            this.LBSelect.Name = "LBSelect";
            this.LBSelect.Width = 50;
            // 
            // SynMessage
            // 
            this.SynMessage.HeaderText = "执行情况";
            this.SynMessage.Name = "SynMessage";
            this.SynMessage.ReadOnly = true;
            this.SynMessage.Width = 150;
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
            this.PriceT.HeaderText = "单价(吨)";
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
            // frmSaleCarInOutBillSynchornous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "同步单据至服务器";
            this.Name = "frmSaleCarInOutBillSynchornous";
            this.Size = new System.Drawing.Size(914, 345);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
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
        private Controls.LBToolStripButton btnSynchronous;
        private Controls.LBDataGridView grdMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LBToolStripButton btnSelectAll;
        private Controls.LBToolStripButton btnCancelSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LBSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn SynMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleCarOutBillCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private Controls.LBDataGridViewComboBoxColumn BillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarTare;
        private System.Windows.Forms.DataGridViewTextBoxColumn SuttleWeightT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
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
    }
}