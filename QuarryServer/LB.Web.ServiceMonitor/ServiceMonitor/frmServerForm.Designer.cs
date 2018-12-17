namespace LB.Web.ServiceMonitor
{
	partial class frmServerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerForm));
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIconTS = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemOpenForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.itemM3 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.itemWebAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStart4itemWebAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStop4itemWebAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRestart4itemWebAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDBAutoBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStart4DBAutoBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStop4DBAutoBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRestart4DBAutoBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tcService = new System.Windows.Forms.TabControl();
            this.tpgM3 = new System.Windows.Forms.TabPage();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnWebLink = new System.Windows.Forms.Button();
            this.btnReStart = new System.Windows.Forms.Button();
            this.tpWebAutoUpdate = new System.Windows.Forms.TabPage();
            this.btnReStart4WebAutoUpdate = new System.Windows.Forms.Button();
            this.btnRefresh4WebAutoUpdate = new System.Windows.Forms.Button();
            this.btnStop4WebAutoUpdate = new System.Windows.Forms.Button();
            this.btnStart4WebAutoUpdate = new System.Windows.Forms.Button();
            this.txtStatus4WebAutoUpdate = new System.Windows.Forms.TextBox();
            this.picStatus4WebAutoUpdate = new System.Windows.Forms.PictureBox();
            this.txtName4WebAutoUpdate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpDBAutoBackUp = new System.Windows.Forms.TabPage();
            this.btnReStart4DBAutoBackUp = new System.Windows.Forms.Button();
            this.btnRefresh4DBAutoBackUp = new System.Windows.Forms.Button();
            this.btnStop4DBAutoBackUp = new System.Windows.Forms.Button();
            this.btnStart4DBAutoBackUp = new System.Windows.Forms.Button();
            this.txtStatus4DBAutoBackUp = new System.Windows.Forms.TextBox();
            this.picStatus4DBAutoBackUp = new System.Windows.Forms.PictureBox();
            this.txtName4DBAutoBackUp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tpPath = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInPath = new System.Windows.Forms.TextBox();
            this.txtOutPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.contextMenuIcon.SuspendLayout();
            this.tcService.SuspendLayout();
            this.tpgM3.SuspendLayout();
            this.tpWebAutoUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus4WebAutoUpdate)).BeginInit();
            this.tpDBAutoBackUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus4DBAutoBackUp)).BeginInit();
            this.tpPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // picStatus
            // 
            this.picStatus.Image = global::LB.Web.Properties.Resources.ServerStopBig;
            this.picStatus.Location = new System.Drawing.Point(24, 137);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(80, 80);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus.TabIndex = 0;
            this.picStatus.TabStop = false;
            // 
            // txtServerURL
            // 
            this.txtServerURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerURL.BackColor = System.Drawing.SystemColors.Control;
            this.txtServerURL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtServerURL.Location = new System.Drawing.Point(31, 68);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(294, 14);
            this.txtServerURL.TabIndex = 2;
            this.txtServerURL.Text = "ServerURL";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStatus.Location = new System.Drawing.Point(142, 96);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(182, 16);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.Text = "正在检测服务状态...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "服务地址：";
            // 
            // notifyIconTS
            // 
            this.notifyIconTS.ContextMenuStrip = this.contextMenuIcon;
            this.notifyIconTS.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTS.Icon")));
            this.notifyIconTS.Text = "服务器";
            this.notifyIconTS.Visible = true;
            this.notifyIconTS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconTS_MouseDoubleClick);
            // 
            // contextMenuIcon
            // 
            this.contextMenuIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemOpenForm,
            this.toolStripSeparator2,
            this.itemConfig,
            this.itemM3,
            this.itemWebAutoUpdate,
            this.itemDBAutoBackUp,
            this.toolStripSeparator1,
            this.itemExit});
            this.contextMenuIcon.Name = "contextMenuIcon";
            this.contextMenuIcon.Size = new System.Drawing.Size(149, 148);
            // 
            // itemOpenForm
            // 
            this.itemOpenForm.Name = "itemOpenForm";
            this.itemOpenForm.Size = new System.Drawing.Size(148, 22);
            this.itemOpenForm.Text = "打开窗口";
            this.itemOpenForm.Click += new System.EventHandler(this.itemOpenForm_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // itemConfig
            // 
            this.itemConfig.Name = "itemConfig";
            this.itemConfig.Size = new System.Drawing.Size(148, 22);
            this.itemConfig.Text = "配置及安装";
            this.itemConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // itemM3
            // 
            this.itemM3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemStart,
            this.itemStop,
            this.itemRestart});
            this.itemM3.Name = "itemM3";
            this.itemM3.Size = new System.Drawing.Size(148, 22);
            this.itemM3.Text = "M3服务";
            // 
            // itemStart
            // 
            this.itemStart.Name = "itemStart";
            this.itemStart.Size = new System.Drawing.Size(100, 22);
            this.itemStart.Text = "启动";
            this.itemStart.Click += new System.EventHandler(this.itemStart_Click);
            // 
            // itemStop
            // 
            this.itemStop.Name = "itemStop";
            this.itemStop.Size = new System.Drawing.Size(100, 22);
            this.itemStop.Text = "停止";
            this.itemStop.Click += new System.EventHandler(this.itemStop_Click);
            // 
            // itemRestart
            // 
            this.itemRestart.Name = "itemRestart";
            this.itemRestart.Size = new System.Drawing.Size(100, 22);
            this.itemRestart.Text = "重启";
            this.itemRestart.Click += new System.EventHandler(this.btnReStart_Click);
            // 
            // itemWebAutoUpdate
            // 
            this.itemWebAutoUpdate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemStart4itemWebAutoUpdate,
            this.itemStop4itemWebAutoUpdate,
            this.itemRestart4itemWebAutoUpdate});
            this.itemWebAutoUpdate.Name = "itemWebAutoUpdate";
            this.itemWebAutoUpdate.Size = new System.Drawing.Size(148, 22);
            this.itemWebAutoUpdate.Text = "自动更新服务";
            // 
            // itemStart4itemWebAutoUpdate
            // 
            this.itemStart4itemWebAutoUpdate.Name = "itemStart4itemWebAutoUpdate";
            this.itemStart4itemWebAutoUpdate.Size = new System.Drawing.Size(100, 22);
            this.itemStart4itemWebAutoUpdate.Text = "启动";
            this.itemStart4itemWebAutoUpdate.Click += new System.EventHandler(this.itemStart4itemWebAutoUpdate_Click);
            // 
            // itemStop4itemWebAutoUpdate
            // 
            this.itemStop4itemWebAutoUpdate.Name = "itemStop4itemWebAutoUpdate";
            this.itemStop4itemWebAutoUpdate.Size = new System.Drawing.Size(100, 22);
            this.itemStop4itemWebAutoUpdate.Text = "停止";
            this.itemStop4itemWebAutoUpdate.Click += new System.EventHandler(this.itemStop4itemWebAutoUpdate_Click);
            // 
            // itemRestart4itemWebAutoUpdate
            // 
            this.itemRestart4itemWebAutoUpdate.Name = "itemRestart4itemWebAutoUpdate";
            this.itemRestart4itemWebAutoUpdate.Size = new System.Drawing.Size(100, 22);
            this.itemRestart4itemWebAutoUpdate.Text = "重启";
            this.itemRestart4itemWebAutoUpdate.Click += new System.EventHandler(this.btnReStart4WebAutoUpdate_Click);
            // 
            // itemDBAutoBackUp
            // 
            this.itemDBAutoBackUp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemStart4DBAutoBackUp,
            this.itemStop4DBAutoBackUp,
            this.itemRestart4DBAutoBackUp});
            this.itemDBAutoBackUp.Name = "itemDBAutoBackUp";
            this.itemDBAutoBackUp.Size = new System.Drawing.Size(148, 22);
            this.itemDBAutoBackUp.Text = "自动备份服务";
            // 
            // itemStart4DBAutoBackUp
            // 
            this.itemStart4DBAutoBackUp.Name = "itemStart4DBAutoBackUp";
            this.itemStart4DBAutoBackUp.Size = new System.Drawing.Size(100, 22);
            this.itemStart4DBAutoBackUp.Text = "启用";
            this.itemStart4DBAutoBackUp.Click += new System.EventHandler(this.itemStart4DBAutoBackUp_Click);
            // 
            // itemStop4DBAutoBackUp
            // 
            this.itemStop4DBAutoBackUp.Name = "itemStop4DBAutoBackUp";
            this.itemStop4DBAutoBackUp.Size = new System.Drawing.Size(100, 22);
            this.itemStop4DBAutoBackUp.Text = "停止";
            this.itemStop4DBAutoBackUp.Click += new System.EventHandler(this.itemStop4DBAutoBackUp_Click);
            // 
            // itemRestart4DBAutoBackUp
            // 
            this.itemRestart4DBAutoBackUp.Name = "itemRestart4DBAutoBackUp";
            this.itemRestart4DBAutoBackUp.Size = new System.Drawing.Size(100, 22);
            this.itemRestart4DBAutoBackUp.Text = "重启";
            this.itemRestart4DBAutoBackUp.Click += new System.EventHandler(this.btnReStart4DBAutoBackUp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(148, 22);
            this.itemExit.Text = "退出";
            this.itemExit.Click += new System.EventHandler(this.itemExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Location = new System.Drawing.Point(154, 120);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(74, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop.Location = new System.Drawing.Point(154, 148);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "停止服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfig.Location = new System.Drawing.Point(154, 204);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(74, 23);
            this.btnConfig.TabIndex = 7;
            this.btnConfig.Text = "配置及安装";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(13, 92);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(103, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "刷新服务";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timerStatus
            // 
            this.timerStatus.Interval = 3000;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "服务名称：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.Control;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Location = new System.Drawing.Point(31, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(294, 14);
            this.txtName.TabIndex = 10;
            this.txtName.Text = "ServiceName";
            // 
            // tcService
            // 
            this.tcService.Controls.Add(this.tpgM3);
            this.tcService.Controls.Add(this.tpWebAutoUpdate);
            this.tcService.Controls.Add(this.tpDBAutoBackUp);
            this.tcService.Controls.Add(this.tpPath);
            this.tcService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcService.Location = new System.Drawing.Point(0, 0);
            this.tcService.Name = "tcService";
            this.tcService.SelectedIndex = 0;
            this.tcService.Size = new System.Drawing.Size(337, 268);
            this.tcService.TabIndex = 11;
            // 
            // tpgM3
            // 
            this.tpgM3.Controls.Add(this.btnRegister);
            this.tpgM3.Controls.Add(this.btnConfig);
            this.tpgM3.Controls.Add(this.btnWebLink);
            this.tpgM3.Controls.Add(this.btnReStart);
            this.tpgM3.Controls.Add(this.txtName);
            this.tpgM3.Controls.Add(this.label2);
            this.tpgM3.Controls.Add(this.btnRefresh);
            this.tpgM3.Controls.Add(this.btnStop);
            this.tpgM3.Controls.Add(this.btnStart);
            this.tpgM3.Controls.Add(this.label1);
            this.tpgM3.Controls.Add(this.txtStatus);
            this.tpgM3.Controls.Add(this.txtServerURL);
            this.tpgM3.Controls.Add(this.picStatus);
            this.tpgM3.Location = new System.Drawing.Point(4, 22);
            this.tpgM3.Name = "tpgM3";
            this.tpgM3.Padding = new System.Windows.Forms.Padding(3);
            this.tpgM3.Size = new System.Drawing.Size(329, 242);
            this.tpgM3.TabIndex = 0;
            this.tpgM3.Text = "M3服务";
            this.tpgM3.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegister.Location = new System.Drawing.Point(27, 219);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(74, 20);
            this.btnRegister.TabIndex = 13;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnWebLink
            // 
            this.btnWebLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWebLink.Location = new System.Drawing.Point(234, 223);
            this.btnWebLink.Name = "btnWebLink";
            this.btnWebLink.Size = new System.Drawing.Size(90, 23);
            this.btnWebLink.TabIndex = 12;
            this.btnWebLink.Text = "网上订单配置";
            this.btnWebLink.UseVisualStyleBackColor = true;
            this.btnWebLink.Visible = false;
            this.btnWebLink.Click += new System.EventHandler(this.btnWebLink_Click);
            // 
            // btnReStart
            // 
            this.btnReStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReStart.Location = new System.Drawing.Point(154, 176);
            this.btnReStart.Name = "btnReStart";
            this.btnReStart.Size = new System.Drawing.Size(74, 23);
            this.btnReStart.TabIndex = 11;
            this.btnReStart.Text = "重启服务";
            this.btnReStart.UseVisualStyleBackColor = true;
            this.btnReStart.Click += new System.EventHandler(this.btnReStart_Click);
            // 
            // tpWebAutoUpdate
            // 
            this.tpWebAutoUpdate.Controls.Add(this.btnReStart4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.btnRefresh4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.btnStop4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.btnStart4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.txtStatus4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.picStatus4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.txtName4WebAutoUpdate);
            this.tpWebAutoUpdate.Controls.Add(this.label3);
            this.tpWebAutoUpdate.Location = new System.Drawing.Point(4, 22);
            this.tpWebAutoUpdate.Name = "tpWebAutoUpdate";
            this.tpWebAutoUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tpWebAutoUpdate.Size = new System.Drawing.Size(329, 242);
            this.tpWebAutoUpdate.TabIndex = 1;
            this.tpWebAutoUpdate.Text = "自动更新服务";
            this.tpWebAutoUpdate.ToolTipText = "自动更新服务";
            this.tpWebAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // btnReStart4WebAutoUpdate
            // 
            this.btnReStart4WebAutoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReStart4WebAutoUpdate.Location = new System.Drawing.Point(150, 191);
            this.btnReStart4WebAutoUpdate.Name = "btnReStart4WebAutoUpdate";
            this.btnReStart4WebAutoUpdate.Size = new System.Drawing.Size(63, 23);
            this.btnReStart4WebAutoUpdate.TabIndex = 19;
            this.btnReStart4WebAutoUpdate.Text = "重启服务";
            this.btnReStart4WebAutoUpdate.UseVisualStyleBackColor = true;
            this.btnReStart4WebAutoUpdate.Click += new System.EventHandler(this.btnReStart4WebAutoUpdate_Click);
            // 
            // btnRefresh4WebAutoUpdate
            // 
            this.btnRefresh4WebAutoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh4WebAutoUpdate.Location = new System.Drawing.Point(10, 66);
            this.btnRefresh4WebAutoUpdate.Name = "btnRefresh4WebAutoUpdate";
            this.btnRefresh4WebAutoUpdate.Size = new System.Drawing.Size(103, 23);
            this.btnRefresh4WebAutoUpdate.TabIndex = 18;
            this.btnRefresh4WebAutoUpdate.Text = "刷新服务";
            this.btnRefresh4WebAutoUpdate.UseVisualStyleBackColor = true;
            this.btnRefresh4WebAutoUpdate.Click += new System.EventHandler(this.btnRefresh4WebAutoUpdate_Click);
            // 
            // btnStop4WebAutoUpdate
            // 
            this.btnStop4WebAutoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop4WebAutoUpdate.Location = new System.Drawing.Point(150, 155);
            this.btnStop4WebAutoUpdate.Name = "btnStop4WebAutoUpdate";
            this.btnStop4WebAutoUpdate.Size = new System.Drawing.Size(63, 23);
            this.btnStop4WebAutoUpdate.TabIndex = 16;
            this.btnStop4WebAutoUpdate.Text = "停止服务";
            this.btnStop4WebAutoUpdate.UseVisualStyleBackColor = true;
            this.btnStop4WebAutoUpdate.Click += new System.EventHandler(this.btnStop4WebAutoUpdate_Click);
            // 
            // btnStart4WebAutoUpdate
            // 
            this.btnStart4WebAutoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart4WebAutoUpdate.Location = new System.Drawing.Point(150, 119);
            this.btnStart4WebAutoUpdate.Name = "btnStart4WebAutoUpdate";
            this.btnStart4WebAutoUpdate.Size = new System.Drawing.Size(63, 23);
            this.btnStart4WebAutoUpdate.TabIndex = 15;
            this.btnStart4WebAutoUpdate.Text = "启动服务";
            this.btnStart4WebAutoUpdate.UseVisualStyleBackColor = true;
            this.btnStart4WebAutoUpdate.Click += new System.EventHandler(this.btnStart4WebAutoUpdate_Click);
            // 
            // txtStatus4WebAutoUpdate
            // 
            this.txtStatus4WebAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus4WebAutoUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus4WebAutoUpdate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus4WebAutoUpdate.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStatus4WebAutoUpdate.Location = new System.Drawing.Point(139, 70);
            this.txtStatus4WebAutoUpdate.Name = "txtStatus4WebAutoUpdate";
            this.txtStatus4WebAutoUpdate.Size = new System.Drawing.Size(182, 16);
            this.txtStatus4WebAutoUpdate.TabIndex = 14;
            this.txtStatus4WebAutoUpdate.Text = "正在检测服务状态...";
            // 
            // picStatus4WebAutoUpdate
            // 
            this.picStatus4WebAutoUpdate.Image = global::LB.Web.Properties.Resources.ServerStopBig;
            this.picStatus4WebAutoUpdate.Location = new System.Drawing.Point(24, 127);
            this.picStatus4WebAutoUpdate.Name = "picStatus4WebAutoUpdate";
            this.picStatus4WebAutoUpdate.Size = new System.Drawing.Size(80, 80);
            this.picStatus4WebAutoUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus4WebAutoUpdate.TabIndex = 13;
            this.picStatus4WebAutoUpdate.TabStop = false;
            // 
            // txtName4WebAutoUpdate
            // 
            this.txtName4WebAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName4WebAutoUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.txtName4WebAutoUpdate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName4WebAutoUpdate.Location = new System.Drawing.Point(28, 37);
            this.txtName4WebAutoUpdate.Name = "txtName4WebAutoUpdate";
            this.txtName4WebAutoUpdate.Size = new System.Drawing.Size(294, 14);
            this.txtName4WebAutoUpdate.TabIndex = 12;
            this.txtName4WebAutoUpdate.Text = "ServiceName";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "服务名称：";
            // 
            // tpDBAutoBackUp
            // 
            this.tpDBAutoBackUp.Controls.Add(this.btnReStart4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.btnRefresh4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.btnStop4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.btnStart4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.txtStatus4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.picStatus4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.txtName4DBAutoBackUp);
            this.tpDBAutoBackUp.Controls.Add(this.label4);
            this.tpDBAutoBackUp.Location = new System.Drawing.Point(4, 22);
            this.tpDBAutoBackUp.Name = "tpDBAutoBackUp";
            this.tpDBAutoBackUp.Padding = new System.Windows.Forms.Padding(3);
            this.tpDBAutoBackUp.Size = new System.Drawing.Size(329, 242);
            this.tpDBAutoBackUp.TabIndex = 2;
            this.tpDBAutoBackUp.Text = "自动备份服务";
            this.tpDBAutoBackUp.UseVisualStyleBackColor = true;
            // 
            // btnReStart4DBAutoBackUp
            // 
            this.btnReStart4DBAutoBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReStart4DBAutoBackUp.Location = new System.Drawing.Point(150, 191);
            this.btnReStart4DBAutoBackUp.Name = "btnReStart4DBAutoBackUp";
            this.btnReStart4DBAutoBackUp.Size = new System.Drawing.Size(63, 23);
            this.btnReStart4DBAutoBackUp.TabIndex = 26;
            this.btnReStart4DBAutoBackUp.Text = "重启服务";
            this.btnReStart4DBAutoBackUp.UseVisualStyleBackColor = true;
            this.btnReStart4DBAutoBackUp.Click += new System.EventHandler(this.btnReStart4DBAutoBackUp_Click);
            // 
            // btnRefresh4DBAutoBackUp
            // 
            this.btnRefresh4DBAutoBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh4DBAutoBackUp.Location = new System.Drawing.Point(10, 66);
            this.btnRefresh4DBAutoBackUp.Name = "btnRefresh4DBAutoBackUp";
            this.btnRefresh4DBAutoBackUp.Size = new System.Drawing.Size(103, 23);
            this.btnRefresh4DBAutoBackUp.TabIndex = 25;
            this.btnRefresh4DBAutoBackUp.Text = "刷新服务";
            this.btnRefresh4DBAutoBackUp.UseVisualStyleBackColor = true;
            this.btnRefresh4DBAutoBackUp.Click += new System.EventHandler(this.btnRefresh4DBAutoBackUp_Click);
            // 
            // btnStop4DBAutoBackUp
            // 
            this.btnStop4DBAutoBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop4DBAutoBackUp.Location = new System.Drawing.Point(150, 155);
            this.btnStop4DBAutoBackUp.Name = "btnStop4DBAutoBackUp";
            this.btnStop4DBAutoBackUp.Size = new System.Drawing.Size(63, 23);
            this.btnStop4DBAutoBackUp.TabIndex = 24;
            this.btnStop4DBAutoBackUp.Text = "停止服务";
            this.btnStop4DBAutoBackUp.UseVisualStyleBackColor = true;
            this.btnStop4DBAutoBackUp.Click += new System.EventHandler(this.btnStop4DBAutoBackUp_Click);
            // 
            // btnStart4DBAutoBackUp
            // 
            this.btnStart4DBAutoBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart4DBAutoBackUp.Location = new System.Drawing.Point(150, 119);
            this.btnStart4DBAutoBackUp.Name = "btnStart4DBAutoBackUp";
            this.btnStart4DBAutoBackUp.Size = new System.Drawing.Size(63, 23);
            this.btnStart4DBAutoBackUp.TabIndex = 23;
            this.btnStart4DBAutoBackUp.Text = "启动服务";
            this.btnStart4DBAutoBackUp.UseVisualStyleBackColor = true;
            this.btnStart4DBAutoBackUp.Click += new System.EventHandler(this.btnStart4DBAutoBackUp_Click);
            // 
            // txtStatus4DBAutoBackUp
            // 
            this.txtStatus4DBAutoBackUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus4DBAutoBackUp.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus4DBAutoBackUp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus4DBAutoBackUp.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStatus4DBAutoBackUp.Location = new System.Drawing.Point(139, 70);
            this.txtStatus4DBAutoBackUp.Name = "txtStatus4DBAutoBackUp";
            this.txtStatus4DBAutoBackUp.Size = new System.Drawing.Size(182, 16);
            this.txtStatus4DBAutoBackUp.TabIndex = 22;
            this.txtStatus4DBAutoBackUp.Text = "正在检测服务状态...";
            // 
            // picStatus4DBAutoBackUp
            // 
            this.picStatus4DBAutoBackUp.Image = global::LB.Web.Properties.Resources.ServerStopBig;
            this.picStatus4DBAutoBackUp.Location = new System.Drawing.Point(24, 127);
            this.picStatus4DBAutoBackUp.Name = "picStatus4DBAutoBackUp";
            this.picStatus4DBAutoBackUp.Size = new System.Drawing.Size(80, 80);
            this.picStatus4DBAutoBackUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus4DBAutoBackUp.TabIndex = 21;
            this.picStatus4DBAutoBackUp.TabStop = false;
            // 
            // txtName4DBAutoBackUp
            // 
            this.txtName4DBAutoBackUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName4DBAutoBackUp.BackColor = System.Drawing.SystemColors.Control;
            this.txtName4DBAutoBackUp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName4DBAutoBackUp.Location = new System.Drawing.Point(28, 37);
            this.txtName4DBAutoBackUp.Name = "txtName4DBAutoBackUp";
            this.txtName4DBAutoBackUp.Size = new System.Drawing.Size(294, 14);
            this.txtName4DBAutoBackUp.TabIndex = 20;
            this.txtName4DBAutoBackUp.Text = "ServiceName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "服务名称：";
            // 
            // tpPath
            // 
            this.tpPath.Controls.Add(this.btnSave);
            this.tpPath.Controls.Add(this.txtOutPath);
            this.tpPath.Controls.Add(this.txtInPath);
            this.tpPath.Controls.Add(this.label6);
            this.tpPath.Controls.Add(this.label5);
            this.tpPath.Location = new System.Drawing.Point(4, 22);
            this.tpPath.Name = "tpPath";
            this.tpPath.Padding = new System.Windows.Forms.Padding(3);
            this.tpPath.Size = new System.Drawing.Size(329, 242);
            this.tpPath.TabIndex = 3;
            this.tpPath.Text = "图片保存地址";
            this.tpPath.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "入场图片保存路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "出场图片保存路径";
            // 
            // txtInPath
            // 
            this.txtInPath.Location = new System.Drawing.Point(11, 22);
            this.txtInPath.Name = "txtInPath";
            this.txtInPath.Size = new System.Drawing.Size(310, 21);
            this.txtInPath.TabIndex = 2;
            // 
            // txtOutPath
            // 
            this.txtOutPath.Location = new System.Drawing.Point(8, 71);
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.Size = new System.Drawing.Size(310, 21);
            this.txtOutPath.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(114, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(337, 268);
            this.Controls.Add(this.tcService);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmServerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.contextMenuIcon.ResumeLayout(false);
            this.tcService.ResumeLayout(false);
            this.tpgM3.ResumeLayout(false);
            this.tpgM3.PerformLayout();
            this.tpWebAutoUpdate.ResumeLayout(false);
            this.tpWebAutoUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus4WebAutoUpdate)).EndInit();
            this.tpDBAutoBackUp.ResumeLayout(false);
            this.tpDBAutoBackUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus4DBAutoBackUp)).EndInit();
            this.tpPath.ResumeLayout(false);
            this.tpPath.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picStatus;
		private System.Windows.Forms.TextBox txtServerURL;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NotifyIcon notifyIconTS;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnConfig;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Timer timerStatus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ContextMenuStrip contextMenuIcon;
        private System.Windows.Forms.ToolStripMenuItem itemConfig;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem itemExit;
		private System.Windows.Forms.ToolStripMenuItem itemOpenForm;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TabControl tcService;
		private System.Windows.Forms.TabPage tpgM3;
		private System.Windows.Forms.TabPage tpWebAutoUpdate;
		private System.Windows.Forms.TextBox txtName4WebAutoUpdate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnRefresh4WebAutoUpdate;
		private System.Windows.Forms.Button btnStop4WebAutoUpdate;
		private System.Windows.Forms.Button btnStart4WebAutoUpdate;
		private System.Windows.Forms.TextBox txtStatus4WebAutoUpdate;
		private System.Windows.Forms.PictureBox picStatus4WebAutoUpdate;
        private System.Windows.Forms.TabPage tpDBAutoBackUp;
        private System.Windows.Forms.Button btnRefresh4DBAutoBackUp;
        private System.Windows.Forms.Button btnStop4DBAutoBackUp;
        private System.Windows.Forms.Button btnStart4DBAutoBackUp;
        private System.Windows.Forms.TextBox txtStatus4DBAutoBackUp;
        private System.Windows.Forms.PictureBox picStatus4DBAutoBackUp;
        private System.Windows.Forms.TextBox txtName4DBAutoBackUp;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem itemM3;
        private System.Windows.Forms.ToolStripMenuItem itemStart;
        private System.Windows.Forms.ToolStripMenuItem itemStop;
        private System.Windows.Forms.ToolStripMenuItem itemWebAutoUpdate;
        private System.Windows.Forms.ToolStripMenuItem itemDBAutoBackUp;
        private System.Windows.Forms.ToolStripMenuItem itemStart4itemWebAutoUpdate;
        private System.Windows.Forms.ToolStripMenuItem itemStop4itemWebAutoUpdate;
        private System.Windows.Forms.ToolStripMenuItem itemStart4DBAutoBackUp;
        private System.Windows.Forms.ToolStripMenuItem itemStop4DBAutoBackUp;
        private System.Windows.Forms.Button btnReStart;
        private System.Windows.Forms.Button btnReStart4WebAutoUpdate;
        private System.Windows.Forms.Button btnReStart4DBAutoBackUp;
		private System.Windows.Forms.ToolStripMenuItem itemRestart;
		private System.Windows.Forms.ToolStripMenuItem itemRestart4itemWebAutoUpdate;
		private System.Windows.Forms.ToolStripMenuItem itemRestart4DBAutoBackUp;
        private System.Windows.Forms.Button btnWebLink;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TabPage tpPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOutPath;
        private System.Windows.Forms.TextBox txtInPath;
        private System.Windows.Forms.Label label6;
    }
}