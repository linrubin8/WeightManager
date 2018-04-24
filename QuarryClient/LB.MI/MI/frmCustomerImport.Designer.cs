namespace LB.MI.MI
{
    partial class frmCustomerImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomerImport));
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
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnImportExcel = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.grdMain.Location = new System.Drawing.Point(0, 40);
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
            this.grdMain.Size = new System.Drawing.Size(765, 284);
            this.grdMain.TabIndex = 8;
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
            this.btnImportExcel,
            this.toolStripSeparator2});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(765, 40);
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
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.LBPermissionCode = "";
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(60, 37);
            this.btnImportExcel.Text = "确定导入";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // frmCustomerImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "客户资料导入";
            this.Name = "frmCustomerImport";
            this.Size = new System.Drawing.Size(765, 324);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LBDataGridView grdMain;
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
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnImportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}