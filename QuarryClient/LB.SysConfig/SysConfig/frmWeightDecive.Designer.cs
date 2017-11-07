namespace LB.SysConfig.SysConfig
{
    partial class frmWeightDecive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeightDecive));
            this.txtWeightDeviceType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtSerialName = new LB.Controls.LBMetroComboBox(this.components);
            this.txtDeviceBoTeLv = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceShuJuWei = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceTingZhiWei = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceZhenChangDu = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceZhenQiShiBiaoShi = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceZhenChuLiFangShi = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceChongFuWeiZhi = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.txtDeviceChongFuChangDu = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnTest = new LB.Controls.LBToolStripButton(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.skinToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtWeightDeviceType
            // 
            this.txtWeightDeviceType.CanBeEmpty = true;
            this.txtWeightDeviceType.Caption = "";
            this.txtWeightDeviceType.DM_UseSelectable = true;
            this.txtWeightDeviceType.FormattingEnabled = true;
            this.txtWeightDeviceType.ItemHeight = 24;
            this.txtWeightDeviceType.Location = new System.Drawing.Point(134, 28);
            this.txtWeightDeviceType.Name = "txtWeightDeviceType";
            this.txtWeightDeviceType.Size = new System.Drawing.Size(201, 30);
            this.txtWeightDeviceType.TabIndex = 20;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(4, 28);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(124, 32);
            this.skinLabel3.TabIndex = 21;
            this.skinLabel3.Text = "仪表型号";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(350, 28);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(124, 32);
            this.skinLabel1.TabIndex = 22;
            this.skinLabel1.Text = "串口号";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSerialName
            // 
            this.txtSerialName.CanBeEmpty = true;
            this.txtSerialName.Caption = "";
            this.txtSerialName.DM_UseSelectable = true;
            this.txtSerialName.FormattingEnabled = true;
            this.txtSerialName.ItemHeight = 24;
            this.txtSerialName.Location = new System.Drawing.Point(480, 28);
            this.txtSerialName.Name = "txtSerialName";
            this.txtSerialName.Size = new System.Drawing.Size(201, 30);
            this.txtSerialName.TabIndex = 23;
            // 
            // txtDeviceBoTeLv
            // 
            this.txtDeviceBoTeLv.CanBeEmpty = true;
            this.txtDeviceBoTeLv.Caption = "";
            this.txtDeviceBoTeLv.DM_UseSelectable = true;
            this.txtDeviceBoTeLv.FormattingEnabled = true;
            this.txtDeviceBoTeLv.ItemHeight = 24;
            this.txtDeviceBoTeLv.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600"});
            this.txtDeviceBoTeLv.Location = new System.Drawing.Point(134, 75);
            this.txtDeviceBoTeLv.Name = "txtDeviceBoTeLv";
            this.txtDeviceBoTeLv.PromptText = "4800";
            this.txtDeviceBoTeLv.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceBoTeLv.TabIndex = 25;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(4, 75);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(124, 32);
            this.skinLabel2.TabIndex = 24;
            this.skinLabel2.Text = "波特率";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceShuJuWei
            // 
            this.txtDeviceShuJuWei.CanBeEmpty = true;
            this.txtDeviceShuJuWei.Caption = "";
            this.txtDeviceShuJuWei.DM_UseSelectable = true;
            this.txtDeviceShuJuWei.FormattingEnabled = true;
            this.txtDeviceShuJuWei.ItemHeight = 24;
            this.txtDeviceShuJuWei.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.txtDeviceShuJuWei.Location = new System.Drawing.Point(480, 75);
            this.txtDeviceShuJuWei.Name = "txtDeviceShuJuWei";
            this.txtDeviceShuJuWei.PromptText = "8";
            this.txtDeviceShuJuWei.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceShuJuWei.TabIndex = 27;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(350, 75);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(124, 32);
            this.skinLabel4.TabIndex = 26;
            this.skinLabel4.Text = "数据位";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceTingZhiWei
            // 
            this.txtDeviceTingZhiWei.CanBeEmpty = true;
            this.txtDeviceTingZhiWei.Caption = "";
            this.txtDeviceTingZhiWei.DM_UseSelectable = true;
            this.txtDeviceTingZhiWei.FormattingEnabled = true;
            this.txtDeviceTingZhiWei.ItemHeight = 24;
            this.txtDeviceTingZhiWei.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.txtDeviceTingZhiWei.Location = new System.Drawing.Point(134, 125);
            this.txtDeviceTingZhiWei.Name = "txtDeviceTingZhiWei";
            this.txtDeviceTingZhiWei.PromptText = "1";
            this.txtDeviceTingZhiWei.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceTingZhiWei.TabIndex = 29;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(4, 125);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(124, 32);
            this.skinLabel5.TabIndex = 28;
            this.skinLabel5.Text = "停止位";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceZhenChangDu
            // 
            this.txtDeviceZhenChangDu.CanBeEmpty = true;
            this.txtDeviceZhenChangDu.Caption = "";
            this.txtDeviceZhenChangDu.DM_UseSelectable = true;
            this.txtDeviceZhenChangDu.FormattingEnabled = true;
            this.txtDeviceZhenChangDu.ItemHeight = 24;
            this.txtDeviceZhenChangDu.Items.AddRange(new object[] {
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
            "16"});
            this.txtDeviceZhenChangDu.Location = new System.Drawing.Point(480, 125);
            this.txtDeviceZhenChangDu.Name = "txtDeviceZhenChangDu";
            this.txtDeviceZhenChangDu.PromptText = "12";
            this.txtDeviceZhenChangDu.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceZhenChangDu.TabIndex = 31;
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel6.Location = new System.Drawing.Point(350, 125);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(124, 32);
            this.skinLabel6.TabIndex = 30;
            this.skinLabel6.Text = "帧长度";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceZhenQiShiBiaoShi
            // 
            this.txtDeviceZhenQiShiBiaoShi.CanBeEmpty = true;
            this.txtDeviceZhenQiShiBiaoShi.Caption = "";
            this.txtDeviceZhenQiShiBiaoShi.DM_UseSelectable = true;
            this.txtDeviceZhenQiShiBiaoShi.FormattingEnabled = true;
            this.txtDeviceZhenQiShiBiaoShi.ItemHeight = 24;
            this.txtDeviceZhenQiShiBiaoShi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.txtDeviceZhenQiShiBiaoShi.Location = new System.Drawing.Point(136, 173);
            this.txtDeviceZhenQiShiBiaoShi.Name = "txtDeviceZhenQiShiBiaoShi";
            this.txtDeviceZhenQiShiBiaoShi.PromptText = "2";
            this.txtDeviceZhenQiShiBiaoShi.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceZhenQiShiBiaoShi.TabIndex = 33;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.Location = new System.Drawing.Point(6, 173);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(124, 32);
            this.skinLabel7.TabIndex = 32;
            this.skinLabel7.Text = "帧起始标识";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceZhenChuLiFangShi
            // 
            this.txtDeviceZhenChuLiFangShi.CanBeEmpty = true;
            this.txtDeviceZhenChuLiFangShi.Caption = "帧处理方式";
            this.txtDeviceZhenChuLiFangShi.DM_UseSelectable = true;
            this.txtDeviceZhenChuLiFangShi.FormattingEnabled = true;
            this.txtDeviceZhenChuLiFangShi.ItemHeight = 24;
            this.txtDeviceZhenChuLiFangShi.Location = new System.Drawing.Point(482, 173);
            this.txtDeviceZhenChuLiFangShi.Name = "txtDeviceZhenChuLiFangShi";
            this.txtDeviceZhenChuLiFangShi.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceZhenChuLiFangShi.TabIndex = 35;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel8.Location = new System.Drawing.Point(352, 173);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(124, 32);
            this.skinLabel8.TabIndex = 34;
            this.skinLabel8.Text = "帧处理方式";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceChongFuWeiZhi
            // 
            this.txtDeviceChongFuWeiZhi.CanBeEmpty = true;
            this.txtDeviceChongFuWeiZhi.Caption = "重量位数";
            this.txtDeviceChongFuWeiZhi.DM_UseSelectable = true;
            this.txtDeviceChongFuWeiZhi.FormattingEnabled = true;
            this.txtDeviceChongFuWeiZhi.ItemHeight = 24;
            this.txtDeviceChongFuWeiZhi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.txtDeviceChongFuWeiZhi.Location = new System.Drawing.Point(136, 223);
            this.txtDeviceChongFuWeiZhi.Name = "txtDeviceChongFuWeiZhi";
            this.txtDeviceChongFuWeiZhi.PromptText = "3";
            this.txtDeviceChongFuWeiZhi.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceChongFuWeiZhi.TabIndex = 37;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel9.Location = new System.Drawing.Point(6, 223);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(124, 32);
            this.skinLabel9.TabIndex = 36;
            this.skinLabel9.Text = "重量位数";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceChongFuChangDu
            // 
            this.txtDeviceChongFuChangDu.CanBeEmpty = true;
            this.txtDeviceChongFuChangDu.Caption = "重量长度";
            this.txtDeviceChongFuChangDu.DM_UseSelectable = true;
            this.txtDeviceChongFuChangDu.FormattingEnabled = true;
            this.txtDeviceChongFuChangDu.ItemHeight = 24;
            this.txtDeviceChongFuChangDu.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.txtDeviceChongFuChangDu.Location = new System.Drawing.Point(482, 223);
            this.txtDeviceChongFuChangDu.Name = "txtDeviceChongFuChangDu";
            this.txtDeviceChongFuChangDu.PromptText = "6";
            this.txtDeviceChongFuChangDu.Size = new System.Drawing.Size(201, 30);
            this.txtDeviceChongFuChangDu.TabIndex = 39;
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel10.Location = new System.Drawing.Point(352, 223);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(124, 32);
            this.skinLabel10.TabIndex = 38;
            this.skinLabel10.Text = "重量长度";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skinLabel3);
            this.groupBox1.Controls.Add(this.txtDeviceChongFuChangDu);
            this.groupBox1.Controls.Add(this.txtWeightDeviceType);
            this.groupBox1.Controls.Add(this.skinLabel10);
            this.groupBox1.Controls.Add(this.skinLabel1);
            this.groupBox1.Controls.Add(this.txtDeviceChongFuWeiZhi);
            this.groupBox1.Controls.Add(this.txtSerialName);
            this.groupBox1.Controls.Add(this.skinLabel9);
            this.groupBox1.Controls.Add(this.skinLabel2);
            this.groupBox1.Controls.Add(this.txtDeviceZhenChuLiFangShi);
            this.groupBox1.Controls.Add(this.txtDeviceBoTeLv);
            this.groupBox1.Controls.Add(this.skinLabel8);
            this.groupBox1.Controls.Add(this.skinLabel4);
            this.groupBox1.Controls.Add(this.txtDeviceZhenQiShiBiaoShi);
            this.groupBox1.Controls.Add(this.txtDeviceShuJuWei);
            this.groupBox1.Controls.Add(this.skinLabel7);
            this.groupBox1.Controls.Add(this.skinLabel5);
            this.groupBox1.Controls.Add(this.txtDeviceZhenChangDu);
            this.groupBox1.Controls.Add(this.txtDeviceTingZhiWei);
            this.groupBox1.Controls.Add(this.skinLabel6);
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 266);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仪表参数设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblWeight);
            this.groupBox2.Location = new System.Drawing.Point(3, 332);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(712, 83);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "仪表测试数据";
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.Color.Black;
            this.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeight.Font = new System.Drawing.Font("黑体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWeight.ForeColor = System.Drawing.Color.Red;
            this.lblWeight.Location = new System.Drawing.Point(25, 19);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(658, 54);
            this.lblWeight.TabIndex = 0;
            this.lblWeight.Text = "-------";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnTest});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(729, 40);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 42;
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
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "WeightDevice_Save";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 37);
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Image = ((System.Drawing.Image)(resources.GetObject("btnTest.Image")));
            this.btnTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTest.LBPermissionCode = "";
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(36, 37);
            this.btnTest.Text = "测试";
            this.btnTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // frmWeightDecive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.skinToolStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.LBPageTitle = "电子显示器设置";
            this.Name = "frmWeightDecive";
            this.Size = new System.Drawing.Size(729, 424);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.LBMetroComboBox txtWeightDeviceType;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private Controls.LBMetroComboBox txtSerialName;
        private Controls.LBMetroComboBox txtDeviceBoTeLv;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private Controls.LBMetroComboBox txtDeviceShuJuWei;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private Controls.LBMetroComboBox txtDeviceTingZhiWei;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private Controls.LBMetroComboBox txtDeviceZhenChangDu;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private Controls.LBMetroComboBox txtDeviceZhenQiShiBiaoShi;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private Controls.LBMetroComboBox txtDeviceZhenChuLiFangShi;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private Controls.LBMetroComboBox txtDeviceChongFuWeiZhi;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private Controls.LBMetroComboBox txtDeviceChongFuChangDu;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblWeight;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnSave;
        private Controls.LBToolStripButton btnTest;
    }
}
