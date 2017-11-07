namespace LB.SysConfig
{
    partial class frmAddUser
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new LB.Controls.LBToolStripButton(this.components);
            this.btnDelete = new LB.Controls.LBToolStripButton(this.components);
            this.txtUserName = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtLoginName = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtUserPassword = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtUserType = new DMSkin.Metro.Controls.MetroComboBox();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtUserSex = new DMSkin.Metro.Controls.MetroComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(570, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            this.btnSave.Image = global::LB.SysConfig.Properties.Resources.btnNewSave3;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.LBPermissionCode = "PMUserManager_Edit";
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 37);
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::LB.SysConfig.Properties.Resources.btnDelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.LBPermissionCode = "PMUserManager_Del";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 37);
            this.btnDelete.Text = "删除";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.DownBack = null;
            this.txtUserName.Icon = null;
            this.txtUserName.IconIsButton = false;
            this.txtUserName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserName.IsPasswordChat = '\0';
            this.txtUserName.IsSystemPasswordChar = false;
            this.txtUserName.Lines = new string[0];
            this.txtUserName.Location = new System.Drawing.Point(98, 107);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserName.MaxLength = 32767;
            this.txtUserName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUserName.MouseBack = null;
            this.txtUserName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NormlBack = null;
            this.txtUserName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserName.ReadOnly = false;
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtUserName.SkinTxt.AccessibleName = "";
            this.txtUserName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtUserName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtUserName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUserName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUserName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUserName.SkinTxt.Name = "BaseText";
            this.txtUserName.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtUserName.SkinTxt.TabIndex = 0;
            this.txtUserName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserName.SkinTxt.WaterText = "";
            this.txtUserName.TabIndex = 3;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserName.WaterText = "";
            this.txtUserName.WordWrap = true;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(12, 103);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(83, 32);
            this.skinLabel1.TabIndex = 4;
            this.skinLabel1.Text = "用户名称";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(12, 60);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 6;
            this.skinLabel2.Text = "登录账户";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLoginName
            // 
            this.txtLoginName.BackColor = System.Drawing.Color.Transparent;
            this.txtLoginName.DownBack = null;
            this.txtLoginName.Icon = null;
            this.txtLoginName.IconIsButton = false;
            this.txtLoginName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLoginName.IsPasswordChat = '\0';
            this.txtLoginName.IsSystemPasswordChar = false;
            this.txtLoginName.Lines = new string[0];
            this.txtLoginName.Location = new System.Drawing.Point(98, 64);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(0);
            this.txtLoginName.MaxLength = 32767;
            this.txtLoginName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtLoginName.MouseBack = null;
            this.txtLoginName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtLoginName.Multiline = false;
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.NormlBack = null;
            this.txtLoginName.Padding = new System.Windows.Forms.Padding(5);
            this.txtLoginName.ReadOnly = false;
            this.txtLoginName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLoginName.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtLoginName.SkinTxt.AccessibleName = "";
            this.txtLoginName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtLoginName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtLoginName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLoginName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoginName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoginName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtLoginName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtLoginName.SkinTxt.Name = "BaseText";
            this.txtLoginName.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtLoginName.SkinTxt.TabIndex = 0;
            this.txtLoginName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLoginName.SkinTxt.WaterText = "";
            this.txtLoginName.TabIndex = 5;
            this.txtLoginName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLoginName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtLoginName.WaterText = "";
            this.txtLoginName.WordWrap = true;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(266, 60);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(83, 32);
            this.skinLabel3.TabIndex = 8;
            this.skinLabel3.Text = "登录密码";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtUserPassword.DownBack = null;
            this.txtUserPassword.Icon = null;
            this.txtUserPassword.IconIsButton = false;
            this.txtUserPassword.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserPassword.IsPasswordChat = '●';
            this.txtUserPassword.IsSystemPasswordChar = true;
            this.txtUserPassword.Lines = new string[0];
            this.txtUserPassword.Location = new System.Drawing.Point(352, 64);
            this.txtUserPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserPassword.MaxLength = 32767;
            this.txtUserPassword.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUserPassword.MouseBack = null;
            this.txtUserPassword.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserPassword.Multiline = false;
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.NormlBack = null;
            this.txtUserPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserPassword.ReadOnly = false;
            this.txtUserPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserPassword.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtUserPassword.SkinTxt.AccessibleName = "";
            this.txtUserPassword.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtUserPassword.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtUserPassword.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUserPassword.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserPassword.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserPassword.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUserPassword.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUserPassword.SkinTxt.Name = "BaseText";
            this.txtUserPassword.SkinTxt.PasswordChar = '●';
            this.txtUserPassword.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtUserPassword.SkinTxt.TabIndex = 0;
            this.txtUserPassword.SkinTxt.UseSystemPasswordChar = true;
            this.txtUserPassword.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserPassword.SkinTxt.WaterText = "";
            this.txtUserPassword.TabIndex = 7;
            this.txtUserPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserPassword.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserPassword.WaterText = "";
            this.txtUserPassword.WordWrap = true;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(266, 107);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(83, 32);
            this.skinLabel4.TabIndex = 9;
            this.skinLabel4.Text = "用户类型";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserType
            // 
            this.txtUserType.DM_UseSelectable = true;
            this.txtUserType.FormattingEnabled = true;
            this.txtUserType.ItemHeight = 24;
            this.txtUserType.Location = new System.Drawing.Point(352, 107);
            this.txtUserType.Name = "txtUserType";
            this.txtUserType.Size = new System.Drawing.Size(137, 30);
            this.txtUserType.TabIndex = 11;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(12, 152);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(83, 32);
            this.skinLabel5.TabIndex = 13;
            this.skinLabel5.Text = "性别";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserSex
            // 
            this.txtUserSex.DM_UseSelectable = true;
            this.txtUserSex.FormattingEnabled = true;
            this.txtUserSex.ItemHeight = 24;
            this.txtUserSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.txtUserSex.Location = new System.Drawing.Point(98, 154);
            this.txtUserSex.Name = "txtUserSex";
            this.txtUserSex.PromptText = "男";
            this.txtUserSex.Size = new System.Drawing.Size(137, 30);
            this.txtUserSex.TabIndex = 14;
            // 
            // frmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 207);
            this.Controls.Add(this.txtUserSex);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.txtUserType);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmAddUser";
            this.Text = "添加用户";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LBToolStripButton btnClose;
        private Controls.LBToolStripButton btnSave;
        private CCWin.SkinControl.SkinTextBox txtUserName;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinTextBox txtLoginName;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox txtUserPassword;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private DMSkin.Metro.Controls.MetroComboBox txtUserType;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private DMSkin.Metro.Controls.MetroComboBox txtUserSex;
        private Controls.LBToolStripButton btnDelete;
    }
}