namespace LB.Web.ServiceMonitor
{
    partial class frmWebLinkConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWebLinkConfig));
            this.txtT3LoginName = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtNetOrderUrl = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.cbTrantion = new System.Windows.Forms.ComboBox();
            this.tbConnection = new System.Windows.Forms.TabControl();
            this.tpWeb = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNetPassword = new System.Windows.Forms.TextBox();
            this.txtNetUser = new System.Windows.Forms.TextBox();
            this.txtNetDB = new System.Windows.Forms.TextBox();
            this.txtNetServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tpM3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtT3Password = new System.Windows.Forms.TextBox();
            this.txtT3User = new System.Windows.Forms.TextBox();
            this.txtT3DB = new System.Windows.Forms.TextBox();
            this.txtT3Server = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbSaveInfoUrl = new System.Windows.Forms.ComboBox();
            this.tbConnection.SuspendLayout();
            this.tpWeb.SuspendLayout();
            this.tpM3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtT3LoginName
            // 
            this.txtT3LoginName.Location = new System.Drawing.Point(102, 12);
            this.txtT3LoginName.Name = "txtT3LoginName";
            this.txtT3LoginName.Size = new System.Drawing.Size(271, 21);
            this.txtT3LoginName.TabIndex = 4;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(19, 16);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(53, 12);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "M3登录名";
            // 
            // txtNetOrderUrl
            // 
            this.txtNetOrderUrl.Location = new System.Drawing.Point(102, 42);
            this.txtNetOrderUrl.Name = "txtNetOrderUrl";
            this.txtNetOrderUrl.Size = new System.Drawing.Size(271, 21);
            this.txtNetOrderUrl.TabIndex = 6;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(19, 46);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(77, 12);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "网上订单地址";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(19, 76);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(53, 12);
            this.lbl3.TabIndex = 7;
            this.lbl3.Text = "事务类型";
            // 
            // cbTrantion
            // 
            this.cbTrantion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrantion.FormattingEnabled = true;
            this.cbTrantion.Items.AddRange(new object[] {
            "分布式事务",
            "网上订单及M3使用不同事务"});
            this.cbTrantion.Location = new System.Drawing.Point(102, 72);
            this.cbTrantion.Name = "cbTrantion";
            this.cbTrantion.Size = new System.Drawing.Size(271, 20);
            this.cbTrantion.TabIndex = 8;
            // 
            // tbConnection
            // 
            this.tbConnection.Controls.Add(this.tpWeb);
            this.tbConnection.Controls.Add(this.tpM3);
            this.tbConnection.Location = new System.Drawing.Point(8, 128);
            this.tbConnection.Name = "tbConnection";
            this.tbConnection.SelectedIndex = 0;
            this.tbConnection.Size = new System.Drawing.Size(376, 196);
            this.tbConnection.TabIndex = 9;
            // 
            // tpWeb
            // 
            this.tpWeb.Controls.Add(this.label8);
            this.tpWeb.Controls.Add(this.txtNetPassword);
            this.tpWeb.Controls.Add(this.txtNetUser);
            this.tpWeb.Controls.Add(this.txtNetDB);
            this.tpWeb.Controls.Add(this.txtNetServer);
            this.tpWeb.Controls.Add(this.label5);
            this.tpWeb.Controls.Add(this.label6);
            this.tpWeb.Controls.Add(this.label7);
            this.tpWeb.Location = new System.Drawing.Point(4, 22);
            this.tpWeb.Name = "tpWeb";
            this.tpWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tpWeb.Size = new System.Drawing.Size(368, 170);
            this.tpWeb.TabIndex = 0;
            this.tpWeb.Text = "网上订单";
            this.tpWeb.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "密码:";
            // 
            // txtNetPassword
            // 
            this.txtNetPassword.Location = new System.Drawing.Point(91, 133);
            this.txtNetPassword.Name = "txtNetPassword";
            this.txtNetPassword.PasswordChar = '*';
            this.txtNetPassword.Size = new System.Drawing.Size(257, 21);
            this.txtNetPassword.TabIndex = 23;
            // 
            // txtNetUser
            // 
            this.txtNetUser.Location = new System.Drawing.Point(91, 94);
            this.txtNetUser.Name = "txtNetUser";
            this.txtNetUser.Size = new System.Drawing.Size(257, 21);
            this.txtNetUser.TabIndex = 22;
            // 
            // txtNetDB
            // 
            this.txtNetDB.Location = new System.Drawing.Point(91, 55);
            this.txtNetDB.Name = "txtNetDB";
            this.txtNetDB.Size = new System.Drawing.Size(257, 21);
            this.txtNetDB.TabIndex = 21;
            // 
            // txtNetServer
            // 
            this.txtNetServer.Location = new System.Drawing.Point(91, 16);
            this.txtNetServer.Name = "txtNetServer";
            this.txtNetServer.Size = new System.Drawing.Size(257, 21);
            this.txtNetServer.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "用户名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "数据库名称:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "服务器地址:";
            // 
            // tpM3
            // 
            this.tpM3.Controls.Add(this.label4);
            this.tpM3.Controls.Add(this.txtT3Password);
            this.tpM3.Controls.Add(this.txtT3User);
            this.tpM3.Controls.Add(this.txtT3DB);
            this.tpM3.Controls.Add(this.txtT3Server);
            this.tpM3.Controls.Add(this.label3);
            this.tpM3.Controls.Add(this.label2);
            this.tpM3.Controls.Add(this.label1);
            this.tpM3.Location = new System.Drawing.Point(4, 22);
            this.tpM3.Name = "tpM3";
            this.tpM3.Padding = new System.Windows.Forms.Padding(3);
            this.tpM3.Size = new System.Drawing.Size(368, 170);
            this.tpM3.TabIndex = 1;
            this.tpM3.Text = "M3";
            this.tpM3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "密码:";
            // 
            // txtT3Password
            // 
            this.txtT3Password.Location = new System.Drawing.Point(91, 133);
            this.txtT3Password.Name = "txtT3Password";
            this.txtT3Password.PasswordChar = '*';
            this.txtT3Password.Size = new System.Drawing.Size(257, 21);
            this.txtT3Password.TabIndex = 16;
            // 
            // txtT3User
            // 
            this.txtT3User.Location = new System.Drawing.Point(91, 94);
            this.txtT3User.Name = "txtT3User";
            this.txtT3User.Size = new System.Drawing.Size(257, 21);
            this.txtT3User.TabIndex = 15;
            // 
            // txtT3DB
            // 
            this.txtT3DB.Location = new System.Drawing.Point(91, 55);
            this.txtT3DB.Name = "txtT3DB";
            this.txtT3DB.Size = new System.Drawing.Size(257, 21);
            this.txtT3DB.TabIndex = 14;
            // 
            // txtT3Server
            // 
            this.txtT3Server.Location = new System.Drawing.Point(91, 16);
            this.txtT3Server.Name = "txtT3Server";
            this.txtT3Server.Size = new System.Drawing.Size(257, 21);
            this.txtT3Server.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据库名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "服务器地址:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(99, 340);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(218, 340);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 11;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "终端信息保存位置";
            // 
            // cbSaveInfoUrl
            // 
            this.cbSaveInfoUrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSaveInfoUrl.FormattingEnabled = true;
            this.cbSaveInfoUrl.Items.AddRange(new object[] {
            "订单终端信息扩展表",
            "订单单头"});
            this.cbSaveInfoUrl.Location = new System.Drawing.Point(126, 101);
            this.cbSaveInfoUrl.Name = "cbSaveInfoUrl";
            this.cbSaveInfoUrl.Size = new System.Drawing.Size(247, 20);
            this.cbSaveInfoUrl.TabIndex = 13;
            // 
            // frmWebLinkConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(394, 369);
            this.Controls.Add(this.cbSaveInfoUrl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbConnection);
            this.Controls.Add(this.cbTrantion);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.txtNetOrderUrl);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txtT3LoginName);
            this.Controls.Add(this.lbl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmWebLinkConfig";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网上订单配置";
            this.Load += new System.EventHandler(this.frmWebLinkConfig_Load);
            this.tbConnection.ResumeLayout(false);
            this.tpWeb.ResumeLayout(false);
            this.tpWeb.PerformLayout();
            this.tpM3.ResumeLayout(false);
            this.tpM3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtT3LoginName;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtNetOrderUrl;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.ComboBox cbTrantion;
        private System.Windows.Forms.TabControl tbConnection;
        private System.Windows.Forms.TabPage tpWeb;
        private System.Windows.Forms.TabPage tpM3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtT3Password;
        private System.Windows.Forms.TextBox txtT3User;
        private System.Windows.Forms.TextBox txtT3DB;
        private System.Windows.Forms.TextBox txtT3Server;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNetPassword;
        private System.Windows.Forms.TextBox txtNetUser;
        private System.Windows.Forms.TextBox txtNetDB;
        private System.Windows.Forms.TextBox txtNetServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbSaveInfoUrl;
    }
}