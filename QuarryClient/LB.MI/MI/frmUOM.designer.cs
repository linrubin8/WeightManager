using LB.Controls;

namespace LB.MI
{
    partial class frmUOM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUOM));
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtUOMName = new LB.Controls.LBSkinTextBox(this.components);
            this.lblUOMName = new CCWin.SkinControl.SkinLabel();
            this.lblUOMType = new CCWin.SkinControl.SkinLabel();
            this.txtUOMType = new LB.Controls.LBMetroComboBox(this.components);
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
            this.skinToolStrip1.Size = new System.Drawing.Size(350, 40);
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
            this.btnSave.LBPermissionCode = "DBUOM_Update";
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
            this.btnDelete.LBPermissionCode = "DBUOM_Delete";
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
            // txtUOMName
            // 
            this.txtUOMName.BackColor = System.Drawing.Color.Transparent;
            this.txtUOMName.CanBeEmpty = false;
            this.txtUOMName.Caption = "分类名称";
            this.txtUOMName.DownBack = null;
            this.txtUOMName.Icon = null;
            this.txtUOMName.IconIsButton = false;
            this.txtUOMName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUOMName.IsPasswordChat = '\0';
            this.txtUOMName.IsSystemPasswordChar = false;
            this.txtUOMName.Lines = new string[0];
            this.txtUOMName.Location = new System.Drawing.Point(121, 53);
            this.txtUOMName.Margin = new System.Windows.Forms.Padding(0);
            this.txtUOMName.MaxLength = 32767;
            this.txtUOMName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUOMName.MouseBack = null;
            this.txtUOMName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUOMName.Multiline = false;
            this.txtUOMName.Name = "txtUOMName";
            this.txtUOMName.NormlBack = null;
            this.txtUOMName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUOMName.ReadOnly = false;
            this.txtUOMName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUOMName.Size = new System.Drawing.Size(200, 28);
            // 
            // 
            // 
            this.txtUOMName.SkinTxt.AccessibleName = "";
            this.txtUOMName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtUOMName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtUOMName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUOMName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUOMName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUOMName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUOMName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUOMName.SkinTxt.Name = "BaseText";
            this.txtUOMName.SkinTxt.Size = new System.Drawing.Size(190, 18);
            this.txtUOMName.SkinTxt.TabIndex = 0;
            this.txtUOMName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUOMName.SkinTxt.WaterText = "";
            this.txtUOMName.TabIndex = 7;
            this.txtUOMName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUOMName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUOMName.WaterText = "";
            this.txtUOMName.WordWrap = true;
            // 
            // lblUOMName
            // 
            this.lblUOMName.BackColor = System.Drawing.Color.Transparent;
            this.lblUOMName.BorderColor = System.Drawing.Color.White;
            this.lblUOMName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUOMName.Location = new System.Drawing.Point(21, 49);
            this.lblUOMName.Name = "lblUOMName";
            this.lblUOMName.Size = new System.Drawing.Size(83, 32);
            this.lblUOMName.TabIndex = 8;
            this.lblUOMName.Text = "单位名称";
            this.lblUOMName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUOMType
            // 
            this.lblUOMType.BackColor = System.Drawing.Color.Transparent;
            this.lblUOMType.BorderColor = System.Drawing.Color.White;
            this.lblUOMType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUOMType.Location = new System.Drawing.Point(21, 86);
            this.lblUOMType.Name = "lblUOMType";
            this.lblUOMType.Size = new System.Drawing.Size(83, 32);
            this.lblUOMType.TabIndex = 14;
            this.lblUOMType.Text = "单位类型";
            this.lblUOMType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUOMType
            // 
            this.txtUOMType.CanBeEmpty = false;
            this.txtUOMType.Caption = "物料分类";
            this.txtUOMType.DM_UseSelectable = true;
            this.txtUOMType.FormattingEnabled = true;
            this.txtUOMType.ItemHeight = 24;
            this.txtUOMType.Location = new System.Drawing.Point(121, 88);
            this.txtUOMType.Name = "txtUOMType";
            this.txtUOMType.Size = new System.Drawing.Size(200, 30);
            this.txtUOMType.TabIndex = 15;
            // 
            // frmUOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtUOMType);
            this.Controls.Add(this.lblUOMType);
            this.Controls.Add(this.lblUOMName);
            this.Controls.Add(this.txtUOMName);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "计量单位";
            this.Name = "frmUOM";
            this.Size = new System.Drawing.Size(350, 144);
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
        private LBSkinTextBox txtUOMName;
        private CCWin.SkinControl.SkinLabel lblUOMName;
        private CCWin.SkinControl.SkinLabel lblUOMType;
        private LBMetroComboBox txtUOMType;
    }
}
