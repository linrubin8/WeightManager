namespace LB.Web.ServiceMonitor
{
	partial class frmRemoteConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemoteConfig));
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstRemotingForceMode = new System.Windows.Forms.ComboBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.clbService = new System.Windows.Forms.CheckedListBox();
            this.btnInstallSure = new System.Windows.Forms.Button();
            this.btnUnInstall = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTestRun = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDBPW = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLoginSecure = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "端口号";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(108, 96);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(147, 21);
            this.txtPort.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(11, 225);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(105, 225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "注意：修改以下配置，客户端必须同时修改";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "本机名称";
            // 
            // txtMachineName
            // 
            this.txtMachineName.Location = new System.Drawing.Point(108, 42);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.ReadOnly = true;
            this.txtMachineName.Size = new System.Drawing.Size(147, 21);
            this.txtMachineName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "本机内网IP";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(108, 69);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(147, 21);
            this.txtIP.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "连接模式";
            // 
            // lstRemotingForceMode
            // 
            this.lstRemotingForceMode.BackColor = System.Drawing.Color.White;
            this.lstRemotingForceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstRemotingForceMode.FormattingEnabled = true;
            this.lstRemotingForceMode.Items.AddRange(new object[] {
            "客户端登录设置",
            "强制局域网模式",
            "强制互联网模式"});
            this.lstRemotingForceMode.Location = new System.Drawing.Point(108, 121);
            this.lstRemotingForceMode.Name = "lstRemotingForceMode";
            this.lstRemotingForceMode.Size = new System.Drawing.Size(147, 20);
            this.lstRemotingForceMode.TabIndex = 8;
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(199, 225);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 11;
            this.btnInstall.Text = "安装服务>>";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "服务名";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(356, 15);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(297, 21);
            this.txtServiceName.TabIndex = 13;
            // 
            // clbService
            // 
            this.clbService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbService.CheckOnClick = true;
            this.clbService.FormattingEnabled = true;
            this.clbService.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.clbService.Location = new System.Drawing.Point(294, 42);
            this.clbService.Name = "clbService";
            this.clbService.ScrollAlwaysVisible = true;
            this.clbService.Size = new System.Drawing.Size(359, 66);
            this.clbService.TabIndex = 14;
            // 
            // btnInstallSure
            // 
            this.btnInstallSure.Location = new System.Drawing.Point(362, 121);
            this.btnInstallSure.Name = "btnInstallSure";
            this.btnInstallSure.Size = new System.Drawing.Size(100, 23);
            this.btnInstallSure.TabIndex = 15;
            this.btnInstallSure.Text = "保存并安装服务";
            this.btnInstallSure.UseVisualStyleBackColor = true;
            this.btnInstallSure.Click += new System.EventHandler(this.btnInstallSure_Click);
            // 
            // btnUnInstall
            // 
            this.btnUnInstall.Location = new System.Drawing.Point(496, 121);
            this.btnUnInstall.Name = "btnUnInstall";
            this.btnUnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnUnInstall.TabIndex = 16;
            this.btnUnInstall.Text = "卸载服务";
            this.btnUnInstall.UseVisualStyleBackColor = true;
            this.btnUnInstall.Click += new System.EventHandler(this.btnUnInstall_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(311, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(342, 29);
            this.label4.TabIndex = 17;
            this.label4.Text = "如果服务启动异常，可以尝试使用测试方式手工启动，以便检查启动异常的原因。";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(310, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(342, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "点击下面的按钮，测试启动上面列表所选的服务：";
            // 
            // btnTestRun
            // 
            this.btnTestRun.Location = new System.Drawing.Point(440, 209);
            this.btnTestRun.Name = "btnTestRun";
            this.btnTestRun.Size = new System.Drawing.Size(75, 23);
            this.btnTestRun.TabIndex = 18;
            this.btnTestRun.Text = "测试启动";
            this.btnTestRun.UseVisualStyleBackColor = true;
            this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "数据库账号";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(108, 174);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(147, 21);
            this.txtDBUser.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "数据库密码";
            // 
            // txtDBPW
            // 
            this.txtDBPW.Location = new System.Drawing.Point(107, 203);
            this.txtDBPW.Name = "txtDBPW";
            this.txtDBPW.PasswordChar = '*';
            this.txtDBPW.Size = new System.Drawing.Size(147, 21);
            this.txtDBPW.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "身份验证";
            // 
            // txtLoginSecure
            // 
            this.txtLoginSecure.BackColor = System.Drawing.Color.White;
            this.txtLoginSecure.DisplayMember = "0";
            this.txtLoginSecure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtLoginSecure.FormattingEnabled = true;
            this.txtLoginSecure.Items.AddRange(new object[] {
            "Windows身份验证",
            "SQL Server身份验证"});
            this.txtLoginSecure.Location = new System.Drawing.Point(108, 149);
            this.txtLoginSecure.Name = "txtLoginSecure";
            this.txtLoginSecure.Size = new System.Drawing.Size(147, 20);
            this.txtLoginSecure.TabIndex = 24;
            // 
            // frmRemoteConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 254);
            this.Controls.Add(this.txtLoginSecure);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDBPW);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDBUser);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnTestRun);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUnInstall);
            this.Controls.Add(this.btnInstallSure);
            this.Controls.Add(this.clbService);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.lstRemotingForceMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMachineName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRemoteConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务配置";
            this.Load += new System.EventHandler(this.frmRemoteConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtMachineName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtIP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox lstRemotingForceMode;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.CheckedListBox clbService;
        private System.Windows.Forms.Button btnInstallSure;
        private System.Windows.Forms.Button btnUnInstall;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnTestRun;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDBPW;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox txtLoginSecure;
    }
}