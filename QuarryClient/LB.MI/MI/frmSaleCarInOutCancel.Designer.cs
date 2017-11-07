namespace LB.MI.MI
{
    partial class frmSaleCarInOutCancel
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
            this.txtCancelDesc = new LB.Controls.LBSkinTextBox(this.components);
            this.btnSubmit = new System.Windows.Forms.Button();
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.lblCnacelCarNum = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // txtCancelDesc
            // 
            this.txtCancelDesc.BackColor = System.Drawing.Color.Transparent;
            this.txtCancelDesc.CanBeEmpty = true;
            this.txtCancelDesc.Caption = "";
            this.txtCancelDesc.DownBack = null;
            this.txtCancelDesc.Icon = null;
            this.txtCancelDesc.IconIsButton = false;
            this.txtCancelDesc.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtCancelDesc.IsPasswordChat = '\0';
            this.txtCancelDesc.IsSystemPasswordChar = false;
            this.txtCancelDesc.Lines = new string[0];
            this.txtCancelDesc.Location = new System.Drawing.Point(25, 69);
            this.txtCancelDesc.Margin = new System.Windows.Forms.Padding(0);
            this.txtCancelDesc.MaxLength = 32767;
            this.txtCancelDesc.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtCancelDesc.MouseBack = null;
            this.txtCancelDesc.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtCancelDesc.Multiline = true;
            this.txtCancelDesc.Name = "txtCancelDesc";
            this.txtCancelDesc.NormlBack = null;
            this.txtCancelDesc.Padding = new System.Windows.Forms.Padding(5);
            this.txtCancelDesc.ReadOnly = false;
            this.txtCancelDesc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCancelDesc.Size = new System.Drawing.Size(248, 115);
            // 
            // 
            // 
            this.txtCancelDesc.SkinTxt.AccessibleName = "";
            this.txtCancelDesc.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtCancelDesc.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtCancelDesc.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCancelDesc.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCancelDesc.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCancelDesc.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtCancelDesc.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtCancelDesc.SkinTxt.Multiline = true;
            this.txtCancelDesc.SkinTxt.Name = "BaseText";
            this.txtCancelDesc.SkinTxt.Size = new System.Drawing.Size(238, 105);
            this.txtCancelDesc.SkinTxt.TabIndex = 0;
            this.txtCancelDesc.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtCancelDesc.SkinTxt.WaterText = "";
            this.txtCancelDesc.TabIndex = 66;
            this.txtCancelDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCancelDesc.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtCancelDesc.WaterText = "";
            this.txtCancelDesc.WordWrap = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(98, 187);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(91, 33);
            this.btnSubmit.TabIndex = 67;
            this.btnSubmit.Text = "确认作废";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel11.Location = new System.Drawing.Point(25, 42);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(248, 27);
            this.skinLabel11.TabIndex = 68;
            this.skinLabel11.Text = "作废原因";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel1.Location = new System.Drawing.Point(25, 10);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(135, 27);
            this.skinLabel1.TabIndex = 69;
            this.skinLabel1.Text = "当前作废车牌:";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCnacelCarNum
            // 
            this.lblCnacelCarNum.BackColor = System.Drawing.Color.Transparent;
            this.lblCnacelCarNum.BorderColor = System.Drawing.Color.White;
            this.lblCnacelCarNum.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCnacelCarNum.Location = new System.Drawing.Point(161, 10);
            this.lblCnacelCarNum.Name = "lblCnacelCarNum";
            this.lblCnacelCarNum.Size = new System.Drawing.Size(132, 27);
            this.lblCnacelCarNum.TabIndex = 70;
            this.lblCnacelCarNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSaleCarInOutCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCnacelCarNum);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.skinLabel11);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtCancelDesc);
            this.LBPageTitle = "作废原因";
            this.Name = "frmSaleCarInOutCancel";
            this.Size = new System.Drawing.Size(296, 223);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LBSkinTextBox txtCancelDesc;
        private System.Windows.Forms.Button btnSubmit;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel lblCnacelCarNum;
    }
}
