using LB.Controls;

namespace LB.MI
{
    partial class frmItemType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemType));
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtItemTypeName = new LB.Controls.LBSkinTextBox(this.components);
            this.lblItemTypeName = new CCWin.SkinControl.SkinLabel();
            this.txtChangeBy = new LB.Controls.LBSkinTextBox(this.components);
            this.txtChangeTime = new LB.Controls.LBSkinTextBox(this.components);
            this.lblChangeTime = new CCWin.SkinControl.SkinLabel();
            this.lblChangeBy = new CCWin.SkinControl.SkinLabel();
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
            this.btnSave.LBPermissionCode = "";
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
            this.btnDelete.LBPermissionCode = "ItemType_Delete";
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
            // txtItemTypeName
            // 
            this.txtItemTypeName.BackColor = System.Drawing.Color.Transparent;
            this.txtItemTypeName.CanBeEmpty = false;
            this.txtItemTypeName.Caption = "分类名称";
            this.txtItemTypeName.DownBack = null;
            this.txtItemTypeName.Icon = null;
            this.txtItemTypeName.IconIsButton = false;
            this.txtItemTypeName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemTypeName.IsPasswordChat = '\0';
            this.txtItemTypeName.IsSystemPasswordChar = false;
            this.txtItemTypeName.Lines = new string[0];
            this.txtItemTypeName.Location = new System.Drawing.Point(121, 53);
            this.txtItemTypeName.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemTypeName.MaxLength = 32767;
            this.txtItemTypeName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtItemTypeName.MouseBack = null;
            this.txtItemTypeName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtItemTypeName.Multiline = false;
            this.txtItemTypeName.Name = "txtItemTypeName";
            this.txtItemTypeName.NormlBack = null;
            this.txtItemTypeName.Padding = new System.Windows.Forms.Padding(5);
            this.txtItemTypeName.ReadOnly = false;
            this.txtItemTypeName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtItemTypeName.Size = new System.Drawing.Size(331, 28);
            // 
            // 
            // 
            this.txtItemTypeName.SkinTxt.AccessibleName = "";
            this.txtItemTypeName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtItemTypeName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtItemTypeName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItemTypeName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemTypeName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemTypeName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtItemTypeName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtItemTypeName.SkinTxt.Name = "BaseText";
            this.txtItemTypeName.SkinTxt.Size = new System.Drawing.Size(321, 18);
            this.txtItemTypeName.SkinTxt.TabIndex = 0;
            this.txtItemTypeName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemTypeName.SkinTxt.WaterText = "";
            this.txtItemTypeName.TabIndex = 7;
            this.txtItemTypeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtItemTypeName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtItemTypeName.WaterText = "";
            this.txtItemTypeName.WordWrap = true;
            // 
            // lblItemTypeName
            // 
            this.lblItemTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblItemTypeName.BorderColor = System.Drawing.Color.White;
            this.lblItemTypeName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemTypeName.Location = new System.Drawing.Point(21, 49);
            this.lblItemTypeName.Name = "lblItemTypeName";
            this.lblItemTypeName.Size = new System.Drawing.Size(83, 32);
            this.lblItemTypeName.TabIndex = 8;
            this.lblItemTypeName.Text = "分类名称";
            this.lblItemTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtChangeBy.Location = new System.Drawing.Point(121, 86);
            this.txtChangeBy.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangeBy.MaxLength = 32767;
            this.txtChangeBy.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtChangeBy.MouseBack = null;
            this.txtChangeBy.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChangeBy.Multiline = false;
            this.txtChangeBy.Name = "txtChangeBy";
            this.txtChangeBy.NormlBack = null;
            this.txtChangeBy.Padding = new System.Windows.Forms.Padding(5);
            this.txtChangeBy.ReadOnly = true;
            this.txtChangeBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtChangeBy.Size = new System.Drawing.Size(150, 28);
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
            this.txtChangeBy.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtChangeBy.SkinTxt.Name = "BaseText";
            this.txtChangeBy.SkinTxt.ReadOnly = true;
            this.txtChangeBy.SkinTxt.Size = new System.Drawing.Size(140, 18);
            this.txtChangeBy.SkinTxt.TabIndex = 0;
            this.txtChangeBy.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeBy.SkinTxt.WaterText = "";
            this.txtChangeBy.TabIndex = 7;
            this.txtChangeBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChangeBy.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeBy.WaterText = "";
            this.txtChangeBy.WordWrap = true;
            // 
            // txtChangeTime
            // 
            this.txtChangeTime.BackColor = System.Drawing.Color.Transparent;
            this.txtChangeTime.CanBeEmpty = true;
            this.txtChangeTime.Caption = "修改时间";
            this.txtChangeTime.DownBack = null;
            this.txtChangeTime.Icon = null;
            this.txtChangeTime.IconIsButton = false;
            this.txtChangeTime.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChangeTime.IsPasswordChat = '\0';
            this.txtChangeTime.IsSystemPasswordChar = false;
            this.txtChangeTime.Lines = new string[0];
            this.txtChangeTime.Location = new System.Drawing.Point(364, 86);
            this.txtChangeTime.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangeTime.MaxLength = 32767;
            this.txtChangeTime.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtChangeTime.MouseBack = null;
            this.txtChangeTime.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChangeTime.Multiline = true;
            this.txtChangeTime.Name = "txtChangeTime";
            this.txtChangeTime.NormlBack = null;
            this.txtChangeTime.Padding = new System.Windows.Forms.Padding(5);
            this.txtChangeTime.ReadOnly = true;
            this.txtChangeTime.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtChangeTime.Size = new System.Drawing.Size(150, 32);
            // 
            // 
            // 
            this.txtChangeTime.SkinTxt.AccessibleName = "";
            this.txtChangeTime.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtChangeTime.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtChangeTime.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtChangeTime.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChangeTime.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChangeTime.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtChangeTime.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtChangeTime.SkinTxt.Multiline = true;
            this.txtChangeTime.SkinTxt.Name = "BaseText";
            this.txtChangeTime.SkinTxt.ReadOnly = true;
            this.txtChangeTime.SkinTxt.Size = new System.Drawing.Size(140, 22);
            this.txtChangeTime.SkinTxt.TabIndex = 0;
            this.txtChangeTime.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeTime.SkinTxt.WaterText = "";
            this.txtChangeTime.TabIndex = 7;
            this.txtChangeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChangeTime.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChangeTime.WaterText = "";
            this.txtChangeTime.WordWrap = true;
            // 
            // lblChangeTime
            // 
            this.lblChangeTime.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeTime.BorderColor = System.Drawing.Color.White;
            this.lblChangeTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChangeTime.Location = new System.Drawing.Point(282, 86);
            this.lblChangeTime.Name = "lblChangeTime";
            this.lblChangeTime.Size = new System.Drawing.Size(79, 32);
            this.lblChangeTime.TabIndex = 8;
            this.lblChangeTime.Text = "修改时间";
            this.lblChangeTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChangeBy
            // 
            this.lblChangeBy.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeBy.BorderColor = System.Drawing.Color.White;
            this.lblChangeBy.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChangeBy.Location = new System.Drawing.Point(25, 86);
            this.lblChangeBy.Name = "lblChangeBy";
            this.lblChangeBy.Size = new System.Drawing.Size(79, 32);
            this.lblChangeBy.TabIndex = 8;
            this.lblChangeBy.Text = "修改人";
            this.lblChangeBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmItemType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblChangeTime);
            this.Controls.Add(this.lblChangeBy);
            this.Controls.Add(this.lblItemTypeName);
            this.Controls.Add(this.txtChangeTime);
            this.Controls.Add(this.txtChangeBy);
            this.Controls.Add(this.txtItemTypeName);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "编辑物料分类";
            this.Name = "frmItemType";
            this.Size = new System.Drawing.Size(570, 144);
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
        private LBSkinTextBox txtItemTypeName;
        private CCWin.SkinControl.SkinLabel lblItemTypeName;
        private LBSkinTextBox txtChangeBy;
        private LBSkinTextBox txtChangeTime;
        private CCWin.SkinControl.SkinLabel lblChangeTime;
        private CCWin.SkinControl.SkinLabel lblChangeBy;
    }
}
