using LB.Controls;

namespace LB.SysConfig
{
    partial class frmEditBackUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditBackUp));
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnAdd = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtBackUpName = new LB.Controls.LBSkinTextBox(this.components);
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtBackUpType = new LB.Controls.LBMetroComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtBackUpMinu = new LB.Controls.LBMetroComboBox(this.components);
            this.txtBackUpHour = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtBackUpWeek = new LB.Controls.LBMetroComboBox(this.components);
            this.lblBackUpWeek = new CCWin.SkinControl.SkinLabel();
            this.chkIsEffect = new DMSkin.Metro.Controls.MetroCheckBox();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.txtBackUpFileMaxNum = new LB.Controls.LBSkinTextBox(this.components);
            this.skinToolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.btnDelete,
            this.toolStripSeparator1});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(629, 40);
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
            this.btnClose.Image = global::LB.SysConfig.Properties.Resources.btnClose;
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
            this.btnAdd.Image = global::LB.SysConfig.Properties.Resources.btnNewSave31;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.LBPermissionCode = "PMUserManager_Edit";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 37);
            this.btnAdd.Text = "保存修改";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::LB.SysConfig.Properties.Resources.btnDelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "PMUserManager_Edit";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 37);
            this.btnDelete.Text = "删除备份方案";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(15, 60);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 8;
            this.skinLabel2.Text = "方案名称";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBackUpName
            // 
            this.txtBackUpName.BackColor = System.Drawing.Color.Transparent;
            this.txtBackUpName.CanBeEmpty = false;
            this.txtBackUpName.Caption = "方案名称";
            this.txtBackUpName.DownBack = null;
            this.txtBackUpName.Icon = null;
            this.txtBackUpName.IconIsButton = false;
            this.txtBackUpName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBackUpName.IsPasswordChat = '\0';
            this.txtBackUpName.IsSystemPasswordChar = false;
            this.txtBackUpName.Lines = new string[0];
            this.txtBackUpName.Location = new System.Drawing.Point(147, 64);
            this.txtBackUpName.Margin = new System.Windows.Forms.Padding(0);
            this.txtBackUpName.MaxLength = 32767;
            this.txtBackUpName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtBackUpName.MouseBack = null;
            this.txtBackUpName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBackUpName.Multiline = false;
            this.txtBackUpName.Name = "txtBackUpName";
            this.txtBackUpName.NormlBack = null;
            this.txtBackUpName.Padding = new System.Windows.Forms.Padding(5);
            this.txtBackUpName.ReadOnly = false;
            this.txtBackUpName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBackUpName.Size = new System.Drawing.Size(313, 28);
            // 
            // 
            // 
            this.txtBackUpName.SkinTxt.AccessibleName = "";
            this.txtBackUpName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtBackUpName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBackUpName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBackUpName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBackUpName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBackUpName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtBackUpName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtBackUpName.SkinTxt.Name = "BaseText";
            this.txtBackUpName.SkinTxt.Size = new System.Drawing.Size(303, 18);
            this.txtBackUpName.SkinTxt.TabIndex = 0;
            this.txtBackUpName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBackUpName.SkinTxt.WaterText = "";
            this.txtBackUpName.TabIndex = 7;
            this.txtBackUpName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBackUpName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBackUpName.WaterText = "";
            this.txtBackUpName.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(6, 33);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(111, 32);
            this.skinLabel1.TabIndex = 9;
            this.skinLabel1.Text = "备份时间方式";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBackUpType
            // 
            this.txtBackUpType.CanBeEmpty = false;
            this.txtBackUpType.Caption = "备份时间方式";
            this.txtBackUpType.DM_UseSelectable = true;
            this.txtBackUpType.FormattingEnabled = true;
            this.txtBackUpType.ItemHeight = 24;
            this.txtBackUpType.Location = new System.Drawing.Point(128, 35);
            this.txtBackUpType.Name = "txtBackUpType";
            this.txtBackUpType.Size = new System.Drawing.Size(457, 30);
            this.txtBackUpType.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skinLabel5);
            this.groupBox1.Controls.Add(this.skinLabel4);
            this.groupBox1.Controls.Add(this.txtBackUpMinu);
            this.groupBox1.Controls.Add(this.txtBackUpHour);
            this.groupBox1.Controls.Add(this.skinLabel3);
            this.groupBox1.Controls.Add(this.txtBackUpWeek);
            this.groupBox1.Controls.Add(this.lblBackUpWeek);
            this.groupBox1.Controls.Add(this.skinLabel1);
            this.groupBox1.Controls.Add(this.txtBackUpType);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.Location = new System.Drawing.Point(19, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 125);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备份日期时间设置";
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(225, 79);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(140, 32);
            this.skinLabel5.TabIndex = 19;
            this.skinLabel5.Text = "执行备份的时间点";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(549, 79);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(33, 32);
            this.skinLabel4.TabIndex = 18;
            this.skinLabel4.Text = "分";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBackUpMinu
            // 
            this.txtBackUpMinu.CanBeEmpty = false;
            this.txtBackUpMinu.Caption = "分";
            this.txtBackUpMinu.DM_UseSelectable = true;
            this.txtBackUpMinu.FormattingEnabled = true;
            this.txtBackUpMinu.ItemHeight = 24;
            this.txtBackUpMinu.Items.AddRange(new object[] {
            "0",
            "30"});
            this.txtBackUpMinu.Location = new System.Drawing.Point(480, 79);
            this.txtBackUpMinu.Name = "txtBackUpMinu";
            this.txtBackUpMinu.Size = new System.Drawing.Size(67, 30);
            this.txtBackUpMinu.TabIndex = 17;
            // 
            // txtBackUpHour
            // 
            this.txtBackUpHour.CanBeEmpty = false;
            this.txtBackUpHour.Caption = "时";
            this.txtBackUpHour.DM_UseSelectable = true;
            this.txtBackUpHour.FormattingEnabled = true;
            this.txtBackUpHour.ItemHeight = 24;
            this.txtBackUpHour.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.txtBackUpHour.Location = new System.Drawing.Point(374, 79);
            this.txtBackUpHour.Name = "txtBackUpHour";
            this.txtBackUpHour.Size = new System.Drawing.Size(67, 30);
            this.txtBackUpHour.TabIndex = 16;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(442, 79);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(33, 32);
            this.skinLabel3.TabIndex = 15;
            this.skinLabel3.Text = "时";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBackUpWeek
            // 
            this.txtBackUpWeek.CanBeEmpty = false;
            this.txtBackUpWeek.Caption = "每周";
            this.txtBackUpWeek.DM_UseSelectable = true;
            this.txtBackUpWeek.FormattingEnabled = true;
            this.txtBackUpWeek.ItemHeight = 24;
            this.txtBackUpWeek.Location = new System.Drawing.Point(128, 79);
            this.txtBackUpWeek.Name = "txtBackUpWeek";
            this.txtBackUpWeek.Size = new System.Drawing.Size(91, 30);
            this.txtBackUpWeek.TabIndex = 14;
            // 
            // lblBackUpWeek
            // 
            this.lblBackUpWeek.BackColor = System.Drawing.Color.Transparent;
            this.lblBackUpWeek.BorderColor = System.Drawing.Color.White;
            this.lblBackUpWeek.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBackUpWeek.Location = new System.Drawing.Point(63, 78);
            this.lblBackUpWeek.Name = "lblBackUpWeek";
            this.lblBackUpWeek.Size = new System.Drawing.Size(55, 32);
            this.lblBackUpWeek.TabIndex = 13;
            this.lblBackUpWeek.Text = "每周";
            this.lblBackUpWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkIsEffect
            // 
            this.chkIsEffect.AutoSize = true;
            this.chkIsEffect.DM_UseSelectable = true;
            this.chkIsEffect.Location = new System.Drawing.Point(499, 72);
            this.chkIsEffect.Name = "chkIsEffect";
            this.chkIsEffect.Size = new System.Drawing.Size(72, 17);
            this.chkIsEffect.TabIndex = 14;
            this.chkIsEffect.Text = "是否生效";
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.Location = new System.Drawing.Point(11, 235);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(560, 32);
            this.skinLabel7.TabIndex = 18;
            this.skinLabel7.Text = "为避免备份文件过多导致硬盘容量不足，设置当前目录最大的备份帐套数量";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBackUpFileMaxNum
            // 
            this.txtBackUpFileMaxNum.BackColor = System.Drawing.Color.Transparent;
            this.txtBackUpFileMaxNum.CanBeEmpty = false;
            this.txtBackUpFileMaxNum.Caption = "最大备份帐套数量";
            this.txtBackUpFileMaxNum.DownBack = null;
            this.txtBackUpFileMaxNum.Icon = null;
            this.txtBackUpFileMaxNum.IconIsButton = false;
            this.txtBackUpFileMaxNum.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBackUpFileMaxNum.IsPasswordChat = '\0';
            this.txtBackUpFileMaxNum.IsSystemPasswordChar = false;
            this.txtBackUpFileMaxNum.Lines = new string[] {
        "50"};
            this.txtBackUpFileMaxNum.Location = new System.Drawing.Point(574, 239);
            this.txtBackUpFileMaxNum.Margin = new System.Windows.Forms.Padding(0);
            this.txtBackUpFileMaxNum.MaxLength = 32767;
            this.txtBackUpFileMaxNum.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtBackUpFileMaxNum.MouseBack = null;
            this.txtBackUpFileMaxNum.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtBackUpFileMaxNum.Multiline = false;
            this.txtBackUpFileMaxNum.Name = "txtBackUpFileMaxNum";
            this.txtBackUpFileMaxNum.NormlBack = null;
            this.txtBackUpFileMaxNum.Padding = new System.Windows.Forms.Padding(5);
            this.txtBackUpFileMaxNum.ReadOnly = false;
            this.txtBackUpFileMaxNum.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBackUpFileMaxNum.Size = new System.Drawing.Size(42, 28);
            // 
            // 
            // 
            this.txtBackUpFileMaxNum.SkinTxt.AccessibleName = "";
            this.txtBackUpFileMaxNum.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtBackUpFileMaxNum.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtBackUpFileMaxNum.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBackUpFileMaxNum.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBackUpFileMaxNum.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBackUpFileMaxNum.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtBackUpFileMaxNum.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtBackUpFileMaxNum.SkinTxt.Name = "BaseText";
            this.txtBackUpFileMaxNum.SkinTxt.Size = new System.Drawing.Size(32, 18);
            this.txtBackUpFileMaxNum.SkinTxt.TabIndex = 0;
            this.txtBackUpFileMaxNum.SkinTxt.Text = "50";
            this.txtBackUpFileMaxNum.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBackUpFileMaxNum.SkinTxt.WaterText = "";
            this.txtBackUpFileMaxNum.TabIndex = 8;
            this.txtBackUpFileMaxNum.Text = "50";
            this.txtBackUpFileMaxNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBackUpFileMaxNum.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtBackUpFileMaxNum.WaterText = "";
            this.txtBackUpFileMaxNum.WordWrap = true;
            // 
            // frmEditBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBackUpFileMaxNum);
            this.Controls.Add(this.skinLabel7);
            this.Controls.Add(this.chkIsEffect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtBackUpName);
            this.Controls.Add(this.skinToolStrip1);
            this.LBPageTitle = "编辑备份方案";
            this.Name = "frmEditBackUp";
            this.Size = new System.Drawing.Size(629, 281);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnAdd;
        private Controls.LBToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private LBSkinTextBox txtBackUpName;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private LBMetroComboBox txtBackUpType;
        private System.Windows.Forms.GroupBox groupBox1;
        private CCWin.SkinControl.SkinLabel lblBackUpWeek;
        private LBMetroComboBox txtBackUpWeek;
        private LBMetroComboBox txtBackUpHour;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private LBMetroComboBox txtBackUpMinu;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private DMSkin.Metro.Controls.MetroCheckBox chkIsEffect;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private LBSkinTextBox txtBackUpFileMaxNum;
    }
}
