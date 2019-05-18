using LB.Controls;

namespace LB.MI
{
    partial class frmItemBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemBase));
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblItemCode = new CCWin.SkinControl.SkinLabel();
            this.txtItemCode = new LB.Controls.LBSkinTextBox(this.components);
            this.txtItemName = new LB.Controls.LBSkinTextBox(this.components);
            this.lblItemName = new CCWin.SkinControl.SkinLabel();
            this.txtItemMode = new LB.Controls.LBSkinTextBox(this.components);
            this.lblItemMode = new CCWin.SkinControl.SkinLabel();
            this.txtItemRate = new LB.Controls.LBSkinTextBox(this.components);
            this.lblItemRate = new CCWin.SkinControl.SkinLabel();
            this.lblUOMID = new CCWin.SkinControl.SkinLabel();
            this.lblItemTypeID = new CCWin.SkinControl.SkinLabel();
            this.txtUOMID = new LB.Controls.LBMetroComboBox(this.components);
            this.txtItemTypeID = new LB.Controls.LBMetroComboBox(this.components);
            this.txtDescription = new LB.Controls.LBSkinTextBox(this.components);
            this.lblDescription = new CCWin.SkinControl.SkinLabel();
            this.chkIsForbid = new DMSkin.Metro.Controls.MetroCheckBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtPrice = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtK3ItemCode = new LB.Controls.LBSkinTextBox(this.components);
            this.skinToolStrip1.SuspendLayout();
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
            this.btnSave,
            this.btnDelete,
            this.toolStripSeparator1});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(570, 40);
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
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
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "ItemBase_Update";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 37);
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "ItemBase_Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 37);
            this.btnDelete.Text = "删除";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // lblItemCode
            // 
            this.lblItemCode.BackColor = System.Drawing.Color.Transparent;
            this.lblItemCode.BorderColor = System.Drawing.Color.White;
            this.lblItemCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemCode.Location = new System.Drawing.Point(15, 60);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(83, 32);
            this.lblItemCode.TabIndex = 8;
            this.lblItemCode.Text = "货物编码";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemCode
            // 
            this.txtItemCode.BackColor = System.Drawing.Color.Transparent;
            this.txtItemCode.CanBeEmpty = true;
            this.txtItemCode.Caption = "货物编码";
            this.txtItemCode.DownBack = null;
            this.txtItemCode.Icon = null;
            this.txtItemCode.IconIsButton = false;
            this.txtItemCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemCode.IsPasswordChat = '\0';
            this.txtItemCode.IsSystemPasswordChar = false;
            this.txtItemCode.Lines = new string[0];
            this.txtItemCode.Location = new System.Drawing.Point(115, 64);
            this.txtItemCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemCode.MaxLength = 32767;
            this.txtItemCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtItemCode.MouseBack = null;
            this.txtItemCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.NormlBack = null;
            this.txtItemCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtItemCode.Size = new System.Drawing.Size(150, 28);
            // 
            // 
            // 
            this.txtItemCode.SkinTxt.AccessibleName = "";
            this.txtItemCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtItemCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtItemCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItemCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtItemCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtItemCode.SkinTxt.Name = "BaseText";
            this.txtItemCode.SkinTxt.ReadOnly = true;
            this.txtItemCode.SkinTxt.Size = new System.Drawing.Size(140, 18);
            this.txtItemCode.SkinTxt.TabIndex = 0;
            this.txtItemCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemCode.SkinTxt.WaterText = "";
            this.txtItemCode.TabIndex = 7;
            this.txtItemCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtItemCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemCode.WaterText = "";
            this.txtItemCode.WordWrap = true;
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.Transparent;
            this.txtItemName.CanBeEmpty = false;
            this.txtItemName.Caption = "货物名称";
            this.txtItemName.DownBack = null;
            this.txtItemName.Icon = null;
            this.txtItemName.IconIsButton = false;
            this.txtItemName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemName.IsPasswordChat = '\0';
            this.txtItemName.IsSystemPasswordChar = false;
            this.txtItemName.Lines = new string[0];
            this.txtItemName.Location = new System.Drawing.Point(376, 64);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemName.MaxLength = 32767;
            this.txtItemName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtItemName.MouseBack = null;
            this.txtItemName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemName.Multiline = false;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.NormlBack = null;
            this.txtItemName.Padding = new System.Windows.Forms.Padding(5);
            this.txtItemName.ReadOnly = false;
            this.txtItemName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtItemName.Size = new System.Drawing.Size(161, 28);
            // 
            // 
            // 
            this.txtItemName.SkinTxt.AccessibleName = "";
            this.txtItemName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtItemName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtItemName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItemName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtItemName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtItemName.SkinTxt.Name = "BaseText";
            this.txtItemName.SkinTxt.Size = new System.Drawing.Size(151, 18);
            this.txtItemName.SkinTxt.TabIndex = 0;
            this.txtItemName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemName.SkinTxt.WaterText = "";
            this.txtItemName.TabIndex = 7;
            this.txtItemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtItemName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemName.WaterText = "";
            this.txtItemName.WordWrap = true;
            // 
            // lblItemName
            // 
            this.lblItemName.BackColor = System.Drawing.Color.Transparent;
            this.lblItemName.BorderColor = System.Drawing.Color.White;
            this.lblItemName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(276, 60);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(83, 32);
            this.lblItemName.TabIndex = 8;
            this.lblItemName.Text = "货物名称";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemMode
            // 
            this.txtItemMode.BackColor = System.Drawing.Color.Transparent;
            this.txtItemMode.CanBeEmpty = true;
            this.txtItemMode.Caption = "规格";
            this.txtItemMode.DownBack = null;
            this.txtItemMode.Icon = null;
            this.txtItemMode.IconIsButton = false;
            this.txtItemMode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemMode.IsPasswordChat = '\0';
            this.txtItemMode.IsSystemPasswordChar = false;
            this.txtItemMode.Lines = new string[0];
            this.txtItemMode.Location = new System.Drawing.Point(115, 129);
            this.txtItemMode.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemMode.MaxLength = 32767;
            this.txtItemMode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtItemMode.MouseBack = null;
            this.txtItemMode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemMode.Multiline = false;
            this.txtItemMode.Name = "txtItemMode";
            this.txtItemMode.NormlBack = null;
            this.txtItemMode.Padding = new System.Windows.Forms.Padding(5);
            this.txtItemMode.ReadOnly = false;
            this.txtItemMode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtItemMode.Size = new System.Drawing.Size(150, 28);
            // 
            // 
            // 
            this.txtItemMode.SkinTxt.AccessibleName = "";
            this.txtItemMode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtItemMode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtItemMode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItemMode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemMode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemMode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtItemMode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtItemMode.SkinTxt.Name = "BaseText";
            this.txtItemMode.SkinTxt.Size = new System.Drawing.Size(140, 18);
            this.txtItemMode.SkinTxt.TabIndex = 0;
            this.txtItemMode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemMode.SkinTxt.WaterText = "";
            this.txtItemMode.TabIndex = 7;
            this.txtItemMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtItemMode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemMode.WaterText = "";
            this.txtItemMode.WordWrap = true;
            // 
            // lblItemMode
            // 
            this.lblItemMode.BackColor = System.Drawing.Color.Transparent;
            this.lblItemMode.BorderColor = System.Drawing.Color.White;
            this.lblItemMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemMode.Location = new System.Drawing.Point(19, 129);
            this.lblItemMode.Name = "lblItemMode";
            this.lblItemMode.Size = new System.Drawing.Size(79, 32);
            this.lblItemMode.TabIndex = 8;
            this.lblItemMode.Text = "规格";
            this.lblItemMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemRate
            // 
            this.txtItemRate.BackColor = System.Drawing.Color.Transparent;
            this.txtItemRate.CanBeEmpty = true;
            this.txtItemRate.Caption = "比重(kg/m3)";
            this.txtItemRate.DownBack = null;
            this.txtItemRate.Icon = null;
            this.txtItemRate.IconIsButton = false;
            this.txtItemRate.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemRate.IsPasswordChat = '\0';
            this.txtItemRate.IsSystemPasswordChar = false;
            this.txtItemRate.Lines = new string[0];
            this.txtItemRate.Location = new System.Drawing.Point(387, 129);
            this.txtItemRate.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemRate.MaxLength = 32767;
            this.txtItemRate.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtItemRate.MouseBack = null;
            this.txtItemRate.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemRate.Multiline = false;
            this.txtItemRate.Name = "txtItemRate";
            this.txtItemRate.NormlBack = null;
            this.txtItemRate.Padding = new System.Windows.Forms.Padding(5);
            this.txtItemRate.ReadOnly = false;
            this.txtItemRate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtItemRate.Size = new System.Drawing.Size(150, 28);
            // 
            // 
            // 
            this.txtItemRate.SkinTxt.AccessibleName = "";
            this.txtItemRate.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtItemRate.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtItemRate.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItemRate.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemRate.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemRate.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtItemRate.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtItemRate.SkinTxt.Name = "BaseText";
            this.txtItemRate.SkinTxt.Size = new System.Drawing.Size(140, 18);
            this.txtItemRate.SkinTxt.TabIndex = 0;
            this.txtItemRate.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemRate.SkinTxt.WaterText = "";
            this.txtItemRate.TabIndex = 7;
            this.txtItemRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtItemRate.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemRate.WaterText = "";
            this.txtItemRate.WordWrap = true;
            // 
            // lblItemRate
            // 
            this.lblItemRate.BackColor = System.Drawing.Color.Transparent;
            this.lblItemRate.BorderColor = System.Drawing.Color.White;
            this.lblItemRate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemRate.Location = new System.Drawing.Point(276, 129);
            this.lblItemRate.Name = "lblItemRate";
            this.lblItemRate.Size = new System.Drawing.Size(108, 32);
            this.lblItemRate.TabIndex = 8;
            this.lblItemRate.Text = "比重(kg/m3)";
            this.lblItemRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUOMID
            // 
            this.lblUOMID.BackColor = System.Drawing.Color.Transparent;
            this.lblUOMID.BorderColor = System.Drawing.Color.White;
            this.lblUOMID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUOMID.Location = new System.Drawing.Point(19, 161);
            this.lblUOMID.Name = "lblUOMID";
            this.lblUOMID.Size = new System.Drawing.Size(79, 32);
            this.lblUOMID.TabIndex = 8;
            this.lblUOMID.Text = "单位";
            this.lblUOMID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItemTypeID
            // 
            this.lblItemTypeID.BackColor = System.Drawing.Color.Transparent;
            this.lblItemTypeID.BorderColor = System.Drawing.Color.White;
            this.lblItemTypeID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemTypeID.Location = new System.Drawing.Point(280, 163);
            this.lblItemTypeID.Name = "lblItemTypeID";
            this.lblItemTypeID.Size = new System.Drawing.Size(104, 32);
            this.lblItemTypeID.TabIndex = 8;
            this.lblItemTypeID.Text = "物料分类";
            this.lblItemTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUOMID
            // 
            this.txtUOMID.CanBeEmpty = false;
            this.txtUOMID.Caption = "单位";
            this.txtUOMID.DM_UseSelectable = true;
            this.txtUOMID.FormattingEnabled = true;
            this.txtUOMID.ItemHeight = 24;
            this.txtUOMID.Location = new System.Drawing.Point(115, 163);
            this.txtUOMID.Name = "txtUOMID";
            this.txtUOMID.Size = new System.Drawing.Size(150, 30);
            this.txtUOMID.TabIndex = 13;
            // 
            // txtItemTypeID
            // 
            this.txtItemTypeID.CanBeEmpty = false;
            this.txtItemTypeID.Caption = "物料分类";
            this.txtItemTypeID.DM_UseSelectable = true;
            this.txtItemTypeID.FormattingEnabled = true;
            this.txtItemTypeID.ItemHeight = 24;
            this.txtItemTypeID.Location = new System.Drawing.Point(387, 163);
            this.txtItemTypeID.Name = "txtItemTypeID";
            this.txtItemTypeID.Size = new System.Drawing.Size(150, 30);
            this.txtItemTypeID.TabIndex = 13;
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
            this.txtDescription.Location = new System.Drawing.Point(115, 232);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0);
            this.txtDescription.MaxLength = 32767;
            this.txtDescription.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtDescription.MouseBack = null;
            this.txtDescription.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtDescription.Multiline = false;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.NormlBack = null;
            this.txtDescription.Padding = new System.Windows.Forms.Padding(5);
            this.txtDescription.ReadOnly = false;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescription.Size = new System.Drawing.Size(422, 28);
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
            this.txtDescription.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtDescription.SkinTxt.Name = "BaseText";
            this.txtDescription.SkinTxt.Size = new System.Drawing.Size(412, 18);
            this.txtDescription.SkinTxt.TabIndex = 0;
            this.txtDescription.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.SkinTxt.WaterText = "";
            this.txtDescription.TabIndex = 7;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescription.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.WaterText = "";
            this.txtDescription.WordWrap = true;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.BorderColor = System.Drawing.Color.White;
            this.lblDescription.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDescription.Location = new System.Drawing.Point(19, 228);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 32);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "备注";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkIsForbid
            // 
            this.chkIsForbid.AutoSize = true;
            this.chkIsForbid.DM_UseSelectable = true;
            this.chkIsForbid.Location = new System.Drawing.Point(465, 204);
            this.chkIsForbid.Name = "chkIsForbid";
            this.chkIsForbid.Size = new System.Drawing.Size(72, 17);
            this.chkIsForbid.TabIndex = 15;
            this.chkIsForbid.Text = "是否禁用";
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(19, 194);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(79, 32);
            this.skinLabel1.TabIndex = 16;
            this.skinLabel1.Text = "默认单价";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinLabel1.Click += new System.EventHandler(this.skinLabel1_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.Transparent;
            this.txtPrice.CanBeEmpty = true;
            this.txtPrice.Caption = "默认单价";
            this.txtPrice.DownBack = null;
            this.txtPrice.Icon = null;
            this.txtPrice.IconIsButton = false;
            this.txtPrice.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPrice.IsPasswordChat = '\0';
            this.txtPrice.IsSystemPasswordChar = false;
            this.txtPrice.Lines = new string[0];
            this.txtPrice.Location = new System.Drawing.Point(115, 198);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(0);
            this.txtPrice.MaxLength = 32767;
            this.txtPrice.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtPrice.MouseBack = null;
            this.txtPrice.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPrice.Multiline = false;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.NormlBack = null;
            this.txtPrice.Padding = new System.Windows.Forms.Padding(5);
            this.txtPrice.ReadOnly = false;
            this.txtPrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrice.Size = new System.Drawing.Size(150, 28);
            // 
            // 
            // 
            this.txtPrice.SkinTxt.AccessibleName = "";
            this.txtPrice.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPrice.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPrice.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPrice.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrice.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrice.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPrice.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPrice.SkinTxt.Name = "BaseText";
            this.txtPrice.SkinTxt.Size = new System.Drawing.Size(140, 18);
            this.txtPrice.SkinTxt.TabIndex = 0;
            this.txtPrice.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPrice.SkinTxt.WaterText = "";
            this.txtPrice.TabIndex = 8;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPrice.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPrice.WaterText = "";
            this.txtPrice.WordWrap = true;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(268, 195);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(54, 32);
            this.skinLabel2.TabIndex = 17;
            this.skinLabel2.Text = "元/KG";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(15, 97);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(83, 32);
            this.skinLabel3.TabIndex = 18;
            this.skinLabel3.Text = "K3编码";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtK3ItemCode
            // 
            this.txtK3ItemCode.BackColor = System.Drawing.Color.Transparent;
            this.txtK3ItemCode.CanBeEmpty = true;
            this.txtK3ItemCode.Caption = "K3编码";
            this.txtK3ItemCode.DownBack = null;
            this.txtK3ItemCode.Icon = null;
            this.txtK3ItemCode.IconIsButton = false;
            this.txtK3ItemCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtK3ItemCode.IsPasswordChat = '\0';
            this.txtK3ItemCode.IsSystemPasswordChar = false;
            this.txtK3ItemCode.Lines = new string[0];
            this.txtK3ItemCode.Location = new System.Drawing.Point(115, 97);
            this.txtK3ItemCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtK3ItemCode.MaxLength = 32767;
            this.txtK3ItemCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtK3ItemCode.MouseBack = null;
            this.txtK3ItemCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtK3ItemCode.Multiline = false;
            this.txtK3ItemCode.Name = "txtK3ItemCode";
            this.txtK3ItemCode.NormlBack = null;
            this.txtK3ItemCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtK3ItemCode.ReadOnly = false;
            this.txtK3ItemCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtK3ItemCode.Size = new System.Drawing.Size(422, 28);
            // 
            // 
            // 
            this.txtK3ItemCode.SkinTxt.AccessibleName = "";
            this.txtK3ItemCode.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtK3ItemCode.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtK3ItemCode.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtK3ItemCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtK3ItemCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtK3ItemCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtK3ItemCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtK3ItemCode.SkinTxt.Name = "BaseText";
            this.txtK3ItemCode.SkinTxt.Size = new System.Drawing.Size(412, 18);
            this.txtK3ItemCode.SkinTxt.TabIndex = 0;
            this.txtK3ItemCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtK3ItemCode.SkinTxt.WaterText = "";
            this.txtK3ItemCode.TabIndex = 9;
            this.txtK3ItemCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtK3ItemCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtK3ItemCode.WaterText = "";
            this.txtK3ItemCode.WordWrap = true;
            // 
            // frmItemBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtK3ItemCode);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.chkIsForbid);
            this.Controls.Add(this.txtItemTypeID);
            this.Controls.Add(this.txtUOMID);
            this.Controls.Add(this.lblItemTypeID);
            this.Controls.Add(this.lblUOMID);
            this.Controls.Add(this.lblItemRate);
            this.Controls.Add(this.lblItemMode);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.txtItemRate);
            this.Controls.Add(this.txtItemMode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "编辑物料";
            this.Name = "frmItemBase";
            this.Size = new System.Drawing.Size(570, 280);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnSave;
        private Controls.LBToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private CCWin.SkinControl.SkinLabel lblItemCode;
        private LBSkinTextBox txtItemCode;
        private LBSkinTextBox txtItemName;
        private CCWin.SkinControl.SkinLabel lblItemName;
        private LBSkinTextBox txtItemMode;
        private CCWin.SkinControl.SkinLabel lblItemMode;
        private LBSkinTextBox txtItemRate;
        private CCWin.SkinControl.SkinLabel lblItemRate;
        private CCWin.SkinControl.SkinLabel lblUOMID;
        private CCWin.SkinControl.SkinLabel lblItemTypeID;
        private LBMetroComboBox txtUOMID;
        private LBMetroComboBox txtItemTypeID;
        private LBSkinTextBox txtDescription;
        private CCWin.SkinControl.SkinLabel lblDescription;
        private DMSkin.Metro.Controls.MetroCheckBox chkIsForbid;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private LBSkinTextBox txtPrice;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private LBSkinTextBox txtK3ItemCode;
    }
}
