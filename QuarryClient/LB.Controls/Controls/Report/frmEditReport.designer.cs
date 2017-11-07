namespace LB.Controls.Report
{
    partial class frmEditReport
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
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtReportTemplateName = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.txtDescription = new CCWin.SkinControl.SkinTextBox();
            this.btnSaveReport = new LB.Controls.LBSkinButton(this.components);
            this.btnDeleteReport = new LB.Controls.LBSkinButton(this.components);
            this.btnUpLoadReport = new LB.Controls.LBSkinButton(this.components);
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtReportPath = new CCWin.SkinControl.SkinTextBox();
            this.btnSelectReport = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbPaperTransverse = new DMSkin.Metro.Controls.MetroRadioButton();
            this.rbPaperLengthways = new DMSkin.Metro.Controls.MetroRadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbAutoPaperSize = new DMSkin.Metro.Controls.MetroRadioButton();
            this.rbManualPaperSize = new DMSkin.Metro.Controls.MetroRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAutoPaperType = new DMSkin.Metro.Controls.MetroRadioButton();
            this.rbManualPaperType = new DMSkin.Metro.Controls.MetroRadioButton();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.txtPaperSizeWidth = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.txtPaperSizeHeight = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.txtPaperType = new DMSkin.Metro.Controls.MetroComboBox();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtPrinterName = new DMSkin.Metro.Controls.MetroComboBox();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.txtPrintCount = new CCWin.SkinControl.SkinTextBox();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(3, 13);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 8;
            this.skinLabel2.Text = "报表名称";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReportTemplateName
            // 
            this.txtReportTemplateName.BackColor = System.Drawing.Color.Transparent;
            this.txtReportTemplateName.DownBack = null;
            this.txtReportTemplateName.Icon = null;
            this.txtReportTemplateName.IconIsButton = false;
            this.txtReportTemplateName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtReportTemplateName.IsPasswordChat = '\0';
            this.txtReportTemplateName.IsSystemPasswordChar = false;
            this.txtReportTemplateName.Lines = new string[0];
            this.txtReportTemplateName.Location = new System.Drawing.Point(107, 17);
            this.txtReportTemplateName.Margin = new System.Windows.Forms.Padding(0);
            this.txtReportTemplateName.MaxLength = 32767;
            this.txtReportTemplateName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtReportTemplateName.MouseBack = null;
            this.txtReportTemplateName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtReportTemplateName.Multiline = false;
            this.txtReportTemplateName.Name = "txtReportTemplateName";
            this.txtReportTemplateName.NormlBack = null;
            this.txtReportTemplateName.Padding = new System.Windows.Forms.Padding(5);
            this.txtReportTemplateName.ReadOnly = false;
            this.txtReportTemplateName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReportTemplateName.Size = new System.Drawing.Size(509, 28);
            // 
            // 
            // 
            this.txtReportTemplateName.SkinTxt.AccessibleName = "";
            this.txtReportTemplateName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtReportTemplateName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtReportTemplateName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReportTemplateName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReportTemplateName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReportTemplateName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtReportTemplateName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtReportTemplateName.SkinTxt.Name = "BaseText";
            this.txtReportTemplateName.SkinTxt.Size = new System.Drawing.Size(499, 18);
            this.txtReportTemplateName.SkinTxt.TabIndex = 0;
            this.txtReportTemplateName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtReportTemplateName.SkinTxt.WaterText = "";
            this.txtReportTemplateName.TabIndex = 7;
            this.txtReportTemplateName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtReportTemplateName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtReportTemplateName.WaterText = "";
            this.txtReportTemplateName.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(3, 55);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(51, 32);
            this.skinLabel1.TabIndex = 10;
            this.skinLabel1.Text = "备注";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.Transparent;
            this.txtDescription.DownBack = null;
            this.txtDescription.Icon = null;
            this.txtDescription.IconIsButton = false;
            this.txtDescription.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtDescription.IsPasswordChat = '\0';
            this.txtDescription.IsSystemPasswordChar = false;
            this.txtDescription.Lines = new string[0];
            this.txtDescription.Location = new System.Drawing.Point(107, 59);
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
            this.txtDescription.Size = new System.Drawing.Size(349, 28);
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
            this.txtDescription.SkinTxt.Size = new System.Drawing.Size(339, 18);
            this.txtDescription.SkinTxt.TabIndex = 0;
            this.txtDescription.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.SkinTxt.WaterText = "";
            this.txtDescription.TabIndex = 9;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescription.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtDescription.WaterText = "";
            this.txtDescription.WordWrap = true;
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnSaveReport.BorderColor = System.Drawing.Color.Gray;
            this.btnSaveReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSaveReport.DownBack = null;
            this.btnSaveReport.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSaveReport.LBPermissionCode = "";
            this.btnSaveReport.Location = new System.Drawing.Point(180, 324);
            this.btnSaveReport.MouseBack = null;
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.NormlBack = null;
            this.btnSaveReport.Size = new System.Drawing.Size(81, 26);
            this.btnSaveReport.TabIndex = 11;
            this.btnSaveReport.Text = "保存报表";
            this.btnSaveReport.UseVisualStyleBackColor = false;
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // btnDeleteReport
            // 
            this.btnDeleteReport.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnDeleteReport.BorderColor = System.Drawing.Color.Gray;
            this.btnDeleteReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDeleteReport.DownBack = null;
            this.btnDeleteReport.Font = new System.Drawing.Font("宋体", 10F);
            this.btnDeleteReport.LBPermissionCode = "";
            this.btnDeleteReport.Location = new System.Drawing.Point(267, 324);
            this.btnDeleteReport.MouseBack = null;
            this.btnDeleteReport.Name = "btnDeleteReport";
            this.btnDeleteReport.NormlBack = null;
            this.btnDeleteReport.Size = new System.Drawing.Size(81, 26);
            this.btnDeleteReport.TabIndex = 12;
            this.btnDeleteReport.Text = "删除报表";
            this.btnDeleteReport.UseVisualStyleBackColor = false;
            this.btnDeleteReport.Click += new System.EventHandler(this.btnDeleteReport_Click);
            // 
            // btnUpLoadReport
            // 
            this.btnUpLoadReport.BackColor = System.Drawing.Color.Transparent;
            this.btnUpLoadReport.BaseColor = System.Drawing.Color.LightGray;
            this.btnUpLoadReport.BorderColor = System.Drawing.Color.Gray;
            this.btnUpLoadReport.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnUpLoadReport.DownBack = null;
            this.btnUpLoadReport.Font = new System.Drawing.Font("宋体", 10F);
            this.btnUpLoadReport.LBPermissionCode = "";
            this.btnUpLoadReport.Location = new System.Drawing.Point(354, 324);
            this.btnUpLoadReport.MouseBack = null;
            this.btnUpLoadReport.Name = "btnUpLoadReport";
            this.btnUpLoadReport.NormlBack = null;
            this.btnUpLoadReport.Size = new System.Drawing.Size(81, 26);
            this.btnUpLoadReport.TabIndex = 13;
            this.btnUpLoadReport.Text = "设计报表";
            this.btnUpLoadReport.UseVisualStyleBackColor = false;
            this.btnUpLoadReport.Click += new System.EventHandler(this.btnUpLoadReport_Click);
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(3, 97);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(83, 32);
            this.skinLabel3.TabIndex = 14;
            this.skinLabel3.Text = "报表路径";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReportPath
            // 
            this.txtReportPath.BackColor = System.Drawing.Color.Transparent;
            this.txtReportPath.DownBack = null;
            this.txtReportPath.Icon = null;
            this.txtReportPath.IconIsButton = false;
            this.txtReportPath.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtReportPath.IsPasswordChat = '\0';
            this.txtReportPath.IsSystemPasswordChar = false;
            this.txtReportPath.Lines = new string[0];
            this.txtReportPath.Location = new System.Drawing.Point(107, 101);
            this.txtReportPath.Margin = new System.Windows.Forms.Padding(0);
            this.txtReportPath.MaxLength = 32767;
            this.txtReportPath.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtReportPath.MouseBack = null;
            this.txtReportPath.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtReportPath.Multiline = false;
            this.txtReportPath.Name = "txtReportPath";
            this.txtReportPath.NormlBack = null;
            this.txtReportPath.Padding = new System.Windows.Forms.Padding(5);
            this.txtReportPath.ReadOnly = false;
            this.txtReportPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReportPath.Size = new System.Drawing.Size(416, 28);
            // 
            // 
            // 
            this.txtReportPath.SkinTxt.AccessibleName = "";
            this.txtReportPath.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtReportPath.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtReportPath.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReportPath.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReportPath.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReportPath.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtReportPath.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtReportPath.SkinTxt.Name = "BaseText";
            this.txtReportPath.SkinTxt.Size = new System.Drawing.Size(406, 18);
            this.txtReportPath.SkinTxt.TabIndex = 0;
            this.txtReportPath.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtReportPath.SkinTxt.WaterText = "";
            this.txtReportPath.TabIndex = 15;
            this.txtReportPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtReportPath.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtReportPath.WaterText = "";
            this.txtReportPath.WordWrap = true;
            // 
            // btnSelectReport
            // 
            this.btnSelectReport.Location = new System.Drawing.Point(526, 106);
            this.btnSelectReport.Name = "btnSelectReport";
            this.btnSelectReport.Size = new System.Drawing.Size(90, 23);
            this.btnSelectReport.TabIndex = 16;
            this.btnSelectReport.Text = "选择本地报表";
            this.btnSelectReport.UseVisualStyleBackColor = true;
            this.btnSelectReport.Click += new System.EventHandler(this.btnSelectReport_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.skinLabel10);
            this.groupBox1.Controls.Add(this.txtPaperSizeWidth);
            this.groupBox1.Controls.Add(this.skinLabel9);
            this.groupBox1.Controls.Add(this.txtPaperSizeHeight);
            this.groupBox1.Controls.Add(this.skinLabel8);
            this.groupBox1.Controls.Add(this.skinLabel7);
            this.groupBox1.Controls.Add(this.txtPaperType);
            this.groupBox1.Controls.Add(this.skinLabel6);
            this.groupBox1.Controls.Add(this.skinLabel5);
            this.groupBox1.Controls.Add(this.txtPrinterName);
            this.groupBox1.Controls.Add(this.skinLabel4);
            this.groupBox1.Location = new System.Drawing.Point(7, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 180);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机设置";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbPaperTransverse);
            this.panel3.Controls.Add(this.rbPaperLengthways);
            this.panel3.Location = new System.Drawing.Point(100, 134);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 32);
            this.panel3.TabIndex = 38;
            // 
            // rbPaperTransverse
            // 
            this.rbPaperTransverse.AutoSize = true;
            this.rbPaperTransverse.DM_UseSelectable = true;
            this.rbPaperTransverse.Enabled = false;
            this.rbPaperTransverse.Location = new System.Drawing.Point(106, 7);
            this.rbPaperTransverse.Name = "rbPaperTransverse";
            this.rbPaperTransverse.Size = new System.Drawing.Size(48, 17);
            this.rbPaperTransverse.TabIndex = 30;
            this.rbPaperTransverse.Text = "横向";
            // 
            // rbPaperLengthways
            // 
            this.rbPaperLengthways.AutoSize = true;
            this.rbPaperLengthways.Checked = true;
            this.rbPaperLengthways.DM_UseSelectable = true;
            this.rbPaperLengthways.Enabled = false;
            this.rbPaperLengthways.Location = new System.Drawing.Point(6, 7);
            this.rbPaperLengthways.Name = "rbPaperLengthways";
            this.rbPaperLengthways.Size = new System.Drawing.Size(48, 17);
            this.rbPaperLengthways.TabIndex = 31;
            this.rbPaperLengthways.TabStop = true;
            this.rbPaperLengthways.Text = "纵向";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbAutoPaperSize);
            this.panel2.Controls.Add(this.rbManualPaperSize);
            this.panel2.Location = new System.Drawing.Point(100, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 32);
            this.panel2.TabIndex = 37;
            // 
            // rbAutoPaperSize
            // 
            this.rbAutoPaperSize.AutoSize = true;
            this.rbAutoPaperSize.Checked = true;
            this.rbAutoPaperSize.DM_UseSelectable = true;
            this.rbAutoPaperSize.Enabled = false;
            this.rbAutoPaperSize.Location = new System.Drawing.Point(6, 7);
            this.rbAutoPaperSize.Name = "rbAutoPaperSize";
            this.rbAutoPaperSize.Size = new System.Drawing.Size(72, 17);
            this.rbAutoPaperSize.TabIndex = 30;
            this.rbAutoPaperSize.TabStop = true;
            this.rbAutoPaperSize.Text = "自动识别";
            // 
            // rbManualPaperSize
            // 
            this.rbManualPaperSize.AutoSize = true;
            this.rbManualPaperSize.DM_UseSelectable = true;
            this.rbManualPaperSize.Enabled = false;
            this.rbManualPaperSize.Location = new System.Drawing.Point(106, 7);
            this.rbManualPaperSize.Name = "rbManualPaperSize";
            this.rbManualPaperSize.Size = new System.Drawing.Size(72, 17);
            this.rbManualPaperSize.TabIndex = 31;
            this.rbManualPaperSize.Text = "手工指定";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbAutoPaperType);
            this.panel1.Controls.Add(this.rbManualPaperType);
            this.panel1.Location = new System.Drawing.Point(100, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 32);
            this.panel1.TabIndex = 36;
            // 
            // rbAutoPaperType
            // 
            this.rbAutoPaperType.AutoSize = true;
            this.rbAutoPaperType.Checked = true;
            this.rbAutoPaperType.DM_UseSelectable = true;
            this.rbAutoPaperType.Enabled = false;
            this.rbAutoPaperType.Location = new System.Drawing.Point(6, 7);
            this.rbAutoPaperType.Name = "rbAutoPaperType";
            this.rbAutoPaperType.Size = new System.Drawing.Size(72, 17);
            this.rbAutoPaperType.TabIndex = 30;
            this.rbAutoPaperType.TabStop = true;
            this.rbAutoPaperType.Text = "自动识别";
            // 
            // rbManualPaperType
            // 
            this.rbManualPaperType.AutoSize = true;
            this.rbManualPaperType.DM_UseSelectable = true;
            this.rbManualPaperType.Enabled = false;
            this.rbManualPaperType.Location = new System.Drawing.Point(106, 7);
            this.rbManualPaperType.Name = "rbManualPaperType";
            this.rbManualPaperType.Size = new System.Drawing.Size(72, 17);
            this.rbManualPaperType.TabIndex = 31;
            this.rbManualPaperType.Text = "手工指定";
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel10.Location = new System.Drawing.Point(6, 129);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(89, 32);
            this.skinLabel10.TabIndex = 29;
            this.skinLabel10.Text = "页面方向";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPaperSizeWidth
            // 
            this.txtPaperSizeWidth.BackColor = System.Drawing.Color.Transparent;
            this.txtPaperSizeWidth.DownBack = null;
            this.txtPaperSizeWidth.Enabled = false;
            this.txtPaperSizeWidth.Icon = null;
            this.txtPaperSizeWidth.IconIsButton = false;
            this.txtPaperSizeWidth.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPaperSizeWidth.IsPasswordChat = '\0';
            this.txtPaperSizeWidth.IsSystemPasswordChar = false;
            this.txtPaperSizeWidth.Lines = new string[0];
            this.txtPaperSizeWidth.Location = new System.Drawing.Point(526, 100);
            this.txtPaperSizeWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtPaperSizeWidth.MaxLength = 32767;
            this.txtPaperSizeWidth.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtPaperSizeWidth.MouseBack = null;
            this.txtPaperSizeWidth.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPaperSizeWidth.Multiline = false;
            this.txtPaperSizeWidth.Name = "txtPaperSizeWidth";
            this.txtPaperSizeWidth.NormlBack = null;
            this.txtPaperSizeWidth.Padding = new System.Windows.Forms.Padding(5);
            this.txtPaperSizeWidth.ReadOnly = false;
            this.txtPaperSizeWidth.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPaperSizeWidth.Size = new System.Drawing.Size(68, 28);
            // 
            // 
            // 
            this.txtPaperSizeWidth.SkinTxt.AccessibleName = "";
            this.txtPaperSizeWidth.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPaperSizeWidth.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPaperSizeWidth.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPaperSizeWidth.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPaperSizeWidth.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPaperSizeWidth.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPaperSizeWidth.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPaperSizeWidth.SkinTxt.Name = "BaseText";
            this.txtPaperSizeWidth.SkinTxt.Size = new System.Drawing.Size(58, 18);
            this.txtPaperSizeWidth.SkinTxt.TabIndex = 0;
            this.txtPaperSizeWidth.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPaperSizeWidth.SkinTxt.WaterText = "";
            this.txtPaperSizeWidth.TabIndex = 28;
            this.txtPaperSizeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPaperSizeWidth.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPaperSizeWidth.WaterText = "";
            this.txtPaperSizeWidth.WordWrap = true;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel9.Location = new System.Drawing.Point(466, 96);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(59, 32);
            this.skinLabel9.TabIndex = 27;
            this.skinLabel9.Text = "宽(mm)";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPaperSizeHeight
            // 
            this.txtPaperSizeHeight.BackColor = System.Drawing.Color.Transparent;
            this.txtPaperSizeHeight.DownBack = null;
            this.txtPaperSizeHeight.Enabled = false;
            this.txtPaperSizeHeight.Icon = null;
            this.txtPaperSizeHeight.IconIsButton = false;
            this.txtPaperSizeHeight.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPaperSizeHeight.IsPasswordChat = '\0';
            this.txtPaperSizeHeight.IsSystemPasswordChar = false;
            this.txtPaperSizeHeight.Lines = new string[0];
            this.txtPaperSizeHeight.Location = new System.Drawing.Point(395, 100);
            this.txtPaperSizeHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtPaperSizeHeight.MaxLength = 32767;
            this.txtPaperSizeHeight.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtPaperSizeHeight.MouseBack = null;
            this.txtPaperSizeHeight.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPaperSizeHeight.Multiline = false;
            this.txtPaperSizeHeight.Name = "txtPaperSizeHeight";
            this.txtPaperSizeHeight.NormlBack = null;
            this.txtPaperSizeHeight.Padding = new System.Windows.Forms.Padding(5);
            this.txtPaperSizeHeight.ReadOnly = false;
            this.txtPaperSizeHeight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPaperSizeHeight.Size = new System.Drawing.Size(68, 28);
            // 
            // 
            // 
            this.txtPaperSizeHeight.SkinTxt.AccessibleName = "";
            this.txtPaperSizeHeight.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPaperSizeHeight.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPaperSizeHeight.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPaperSizeHeight.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPaperSizeHeight.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPaperSizeHeight.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPaperSizeHeight.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPaperSizeHeight.SkinTxt.Name = "BaseText";
            this.txtPaperSizeHeight.SkinTxt.Size = new System.Drawing.Size(58, 18);
            this.txtPaperSizeHeight.SkinTxt.TabIndex = 0;
            this.txtPaperSizeHeight.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPaperSizeHeight.SkinTxt.WaterText = "";
            this.txtPaperSizeHeight.TabIndex = 26;
            this.txtPaperSizeHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPaperSizeHeight.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPaperSizeHeight.WaterText = "";
            this.txtPaperSizeHeight.WordWrap = true;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel8.Location = new System.Drawing.Point(330, 98);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(59, 32);
            this.skinLabel8.TabIndex = 25;
            this.skinLabel8.Text = "高(mm)";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel7.Location = new System.Drawing.Point(6, 94);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(89, 32);
            this.skinLabel7.TabIndex = 24;
            this.skinLabel7.Text = "纸张大小";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPaperType
            // 
            this.txtPaperType.DM_UseSelectable = true;
            this.txtPaperType.Enabled = false;
            this.txtPaperType.FormattingEnabled = true;
            this.txtPaperType.ItemHeight = 24;
            this.txtPaperType.Location = new System.Drawing.Point(395, 63);
            this.txtPaperType.Name = "txtPaperType";
            this.txtPaperType.Size = new System.Drawing.Size(199, 30);
            this.txtPaperType.TabIndex = 21;
            // 
            // skinLabel6
            // 
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel6.Location = new System.Drawing.Point(324, 60);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(65, 32);
            this.skinLabel6.TabIndex = 20;
            this.skinLabel6.Text = "纸张格式";
            this.skinLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel5.Location = new System.Drawing.Point(6, 59);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(89, 32);
            this.skinLabel5.TabIndex = 17;
            this.skinLabel5.Text = "纸张格式";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrinterName
            // 
            this.txtPrinterName.DM_UseSelectable = true;
            this.txtPrinterName.FormattingEnabled = true;
            this.txtPrinterName.ItemHeight = 24;
            this.txtPrinterName.Location = new System.Drawing.Point(100, 27);
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.Size = new System.Drawing.Size(494, 30);
            this.txtPrinterName.TabIndex = 16;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel4.Location = new System.Drawing.Point(6, 27);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(89, 32);
            this.skinLabel4.TabIndex = 15;
            this.skinLabel4.Text = "默认打印机";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel11.Location = new System.Drawing.Point(459, 55);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(86, 32);
            this.skinLabel11.TabIndex = 18;
            this.skinLabel11.Text = "打印次数";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrintCount
            // 
            this.txtPrintCount.BackColor = System.Drawing.Color.Transparent;
            this.txtPrintCount.DownBack = null;
            this.txtPrintCount.Icon = null;
            this.txtPrintCount.IconIsButton = false;
            this.txtPrintCount.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPrintCount.IsPasswordChat = '\0';
            this.txtPrintCount.IsSystemPasswordChar = false;
            this.txtPrintCount.Lines = new string[0];
            this.txtPrintCount.Location = new System.Drawing.Point(548, 59);
            this.txtPrintCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtPrintCount.MaxLength = 32767;
            this.txtPrintCount.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtPrintCount.MouseBack = null;
            this.txtPrintCount.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPrintCount.Multiline = false;
            this.txtPrintCount.Name = "txtPrintCount";
            this.txtPrintCount.NormlBack = null;
            this.txtPrintCount.Padding = new System.Windows.Forms.Padding(5);
            this.txtPrintCount.ReadOnly = false;
            this.txtPrintCount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrintCount.Size = new System.Drawing.Size(68, 28);
            // 
            // 
            // 
            this.txtPrintCount.SkinTxt.AccessibleName = "";
            this.txtPrintCount.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPrintCount.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPrintCount.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPrintCount.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrintCount.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrintCount.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPrintCount.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPrintCount.SkinTxt.Name = "BaseText";
            this.txtPrintCount.SkinTxt.Size = new System.Drawing.Size(58, 18);
            this.txtPrintCount.SkinTxt.TabIndex = 0;
            this.txtPrintCount.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPrintCount.SkinTxt.WaterText = "";
            this.txtPrintCount.TabIndex = 27;
            this.txtPrintCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPrintCount.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPrintCount.WaterText = "";
            this.txtPrintCount.WordWrap = true;
            // 
            // frmEditReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPrintCount);
            this.Controls.Add(this.skinLabel11);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectReport);
            this.Controls.Add(this.txtReportPath);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.btnUpLoadReport);
            this.Controls.Add(this.btnDeleteReport);
            this.Controls.Add(this.btnSaveReport);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtReportTemplateName);
            this.Name = "frmEditReport";
            this.Size = new System.Drawing.Size(641, 366);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinTextBox txtReportTemplateName;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinTextBox txtDescription;
        private Controls.LBSkinButton btnSaveReport;
        private Controls.LBSkinButton btnDeleteReport;
        private Controls.LBSkinButton btnUpLoadReport;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox txtReportPath;
        private System.Windows.Forms.Button btnSelectReport;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private DMSkin.Metro.Controls.MetroComboBox txtPrinterName;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private DMSkin.Metro.Controls.MetroComboBox txtPaperType;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private CCWin.SkinControl.SkinTextBox txtPaperSizeWidth;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private CCWin.SkinControl.SkinTextBox txtPaperSizeHeight;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private DMSkin.Metro.Controls.MetroRadioButton rbManualPaperType;
        private DMSkin.Metro.Controls.MetroRadioButton rbAutoPaperType;
        private System.Windows.Forms.Panel panel3;
        private DMSkin.Metro.Controls.MetroRadioButton rbPaperTransverse;
        private DMSkin.Metro.Controls.MetroRadioButton rbPaperLengthways;
        private System.Windows.Forms.Panel panel2;
        private DMSkin.Metro.Controls.MetroRadioButton rbAutoPaperSize;
        private DMSkin.Metro.Controls.MetroRadioButton rbManualPaperSize;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private CCWin.SkinControl.SkinTextBox txtPrintCount;
    }
}
