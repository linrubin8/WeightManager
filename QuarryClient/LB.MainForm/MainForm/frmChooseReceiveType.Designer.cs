namespace LB.MainForm.MainForm
{
    partial class frmChooseReceiveType
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
            this.txtReceiveType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.lblCustomerName = new CCWin.SkinControl.SkinLabel();
            this.lalsss = new CCWin.SkinControl.SkinLabel();
            this.lblCarNum = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.lblItem = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.lblAmount = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.lblRemainAmount = new CCWin.SkinControl.SkinLabel();
            this.lblPayAmount = new CCWin.SkinControl.SkinLabel();
            this.txtPayAmount = new LB.Controls.LBSkinTextBox(this.components);
            this.lblNeedPay = new CCWin.SkinControl.SkinLabel();
            this.lblNeedPayAmount = new CCWin.SkinControl.SkinLabel();
            this.lblWeixin = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // txtReceiveType
            // 
            this.txtReceiveType.CanBeEmpty = false;
            this.txtReceiveType.Caption = "每周";
            this.txtReceiveType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtReceiveType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtReceiveType.DM_UseSelectable = true;
            this.txtReceiveType.FormattingEnabled = true;
            this.txtReceiveType.ItemHeight = 29;
            this.txtReceiveType.Location = new System.Drawing.Point(151, 245);
            this.txtReceiveType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReceiveType.Name = "txtReceiveType";
            this.txtReceiveType.Size = new System.Drawing.Size(194, 35);
            this.txtReceiveType.TabIndex = 50;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel11.Location = new System.Drawing.Point(0, 245);
            this.skinLabel11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(139, 34);
            this.skinLabel11.TabIndex = 51;
            this.skinLabel11.Text = "收款方式：";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(111, 361);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(104, 41);
            this.btnSubmit.TabIndex = 52;
            this.btnSubmit.Text = "确认";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(241, 361);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 41);
            this.btnCancel.TabIndex = 53;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel10.Location = new System.Drawing.Point(0, 18);
            this.skinLabel10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(143, 36);
            this.skinLabel10.TabIndex = 54;
            this.skinLabel10.Text = "客户名称：";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerName.BorderColor = System.Drawing.Color.White;
            this.lblCustomerName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCustomerName.Location = new System.Drawing.Point(144, 21);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(301, 36);
            this.lblCustomerName.TabIndex = 55;
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lalsss
            // 
            this.lalsss.BackColor = System.Drawing.Color.Transparent;
            this.lalsss.BorderColor = System.Drawing.Color.White;
            this.lalsss.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lalsss.Location = new System.Drawing.Point(0, 102);
            this.lalsss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lalsss.Name = "lalsss";
            this.lalsss.Size = new System.Drawing.Size(143, 36);
            this.lalsss.TabIndex = 56;
            this.lalsss.Text = "车牌号码：";
            this.lalsss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCarNum
            // 
            this.lblCarNum.BackColor = System.Drawing.Color.Transparent;
            this.lblCarNum.BorderColor = System.Drawing.Color.White;
            this.lblCarNum.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblCarNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCarNum.Location = new System.Drawing.Point(144, 106);
            this.lblCarNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCarNum.Name = "lblCarNum";
            this.lblCarNum.Size = new System.Drawing.Size(301, 36);
            this.lblCarNum.TabIndex = 57;
            this.lblCarNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel2.Location = new System.Drawing.Point(0, 142);
            this.skinLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(143, 36);
            this.skinLabel2.TabIndex = 58;
            this.skinLabel2.Text = "货物名称：";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.Transparent;
            this.lblItem.BorderColor = System.Drawing.Color.White;
            this.lblItem.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblItem.Location = new System.Drawing.Point(144, 142);
            this.lblItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(301, 36);
            this.lblItem.TabIndex = 59;
            this.lblItem.Text = "   ";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel1.Location = new System.Drawing.Point(4, 194);
            this.skinLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(143, 36);
            this.skinLabel1.TabIndex = 60;
            this.skinLabel1.Text = "总 金 额：";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.BorderColor = System.Drawing.Color.White;
            this.lblAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAmount.Location = new System.Drawing.Point(144, 194);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(301, 36);
            this.lblAmount.TabIndex = 61;
            this.lblAmount.Text = "   ";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel3.Location = new System.Drawing.Point(0, 60);
            this.skinLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(143, 36);
            this.skinLabel3.TabIndex = 62;
            this.skinLabel3.Text = "账户余额：";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRemainAmount
            // 
            this.lblRemainAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblRemainAmount.BorderColor = System.Drawing.Color.White;
            this.lblRemainAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblRemainAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRemainAmount.Location = new System.Drawing.Point(144, 70);
            this.lblRemainAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRemainAmount.Name = "lblRemainAmount";
            this.lblRemainAmount.Size = new System.Drawing.Size(301, 36);
            this.lblRemainAmount.TabIndex = 63;
            this.lblRemainAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPayAmount
            // 
            this.lblPayAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblPayAmount.BorderColor = System.Drawing.Color.White;
            this.lblPayAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblPayAmount.Location = new System.Drawing.Point(-11, 304);
            this.lblPayAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPayAmount.Name = "lblPayAmount";
            this.lblPayAmount.Size = new System.Drawing.Size(241, 34);
            this.lblPayAmount.TabIndex = 64;
            this.lblPayAmount.Text = "余额不足充值现金：";
            this.lblPayAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.BackColor = System.Drawing.Color.Transparent;
            this.txtPayAmount.CanBeEmpty = true;
            this.txtPayAmount.Caption = "";
            this.txtPayAmount.DownBack = null;
            this.txtPayAmount.Icon = null;
            this.txtPayAmount.IconIsButton = false;
            this.txtPayAmount.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPayAmount.IsPasswordChat = '\0';
            this.txtPayAmount.IsSystemPasswordChar = false;
            this.txtPayAmount.Lines = new string[0];
            this.txtPayAmount.Location = new System.Drawing.Point(228, 299);
            this.txtPayAmount.Margin = new System.Windows.Forms.Padding(0);
            this.txtPayAmount.MaxLength = 32767;
            this.txtPayAmount.MinimumSize = new System.Drawing.Size(37, 35);
            this.txtPayAmount.MouseBack = null;
            this.txtPayAmount.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPayAmount.Multiline = true;
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.NormlBack = null;
            this.txtPayAmount.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtPayAmount.ReadOnly = false;
            this.txtPayAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPayAmount.Size = new System.Drawing.Size(117, 42);
            // 
            // 
            // 
            this.txtPayAmount.SkinTxt.AccessibleName = "";
            this.txtPayAmount.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPayAmount.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPayAmount.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPayAmount.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPayAmount.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPayAmount.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPayAmount.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.txtPayAmount.SkinTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPayAmount.SkinTxt.Multiline = true;
            this.txtPayAmount.SkinTxt.Name = "BaseText";
            this.txtPayAmount.SkinTxt.Size = new System.Drawing.Size(103, 30);
            this.txtPayAmount.SkinTxt.TabIndex = 0;
            this.txtPayAmount.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPayAmount.SkinTxt.WaterText = "";
            this.txtPayAmount.TabIndex = 65;
            this.txtPayAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPayAmount.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPayAmount.WaterText = "";
            this.txtPayAmount.WordWrap = true;
            // 
            // lblNeedPay
            // 
            this.lblNeedPay.BackColor = System.Drawing.Color.Transparent;
            this.lblNeedPay.BorderColor = System.Drawing.Color.White;
            this.lblNeedPay.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblNeedPay.ForeColor = System.Drawing.Color.Red;
            this.lblNeedPay.Location = new System.Drawing.Point(349, 299);
            this.lblNeedPay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNeedPay.Name = "lblNeedPay";
            this.lblNeedPay.Size = new System.Drawing.Size(120, 20);
            this.lblNeedPay.TabIndex = 66;
            this.lblNeedPay.Text = "建议充值";
            this.lblNeedPay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNeedPayAmount
            // 
            this.lblNeedPayAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblNeedPayAmount.BorderColor = System.Drawing.Color.White;
            this.lblNeedPayAmount.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblNeedPayAmount.ForeColor = System.Drawing.Color.Red;
            this.lblNeedPayAmount.Location = new System.Drawing.Point(349, 321);
            this.lblNeedPayAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNeedPayAmount.Name = "lblNeedPayAmount";
            this.lblNeedPayAmount.Size = new System.Drawing.Size(120, 20);
            this.lblNeedPayAmount.TabIndex = 67;
            this.lblNeedPayAmount.Text = " ";
            this.lblNeedPayAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWeixin
            // 
            this.lblWeixin.BackColor = System.Drawing.Color.Transparent;
            this.lblWeixin.BorderColor = System.Drawing.Color.White;
            this.lblWeixin.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblWeixin.ForeColor = System.Drawing.Color.Red;
            this.lblWeixin.Location = new System.Drawing.Point(353, 245);
            this.lblWeixin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeixin.Name = "lblWeixin";
            this.lblWeixin.Size = new System.Drawing.Size(120, 35);
            this.lblWeixin.TabIndex = 68;
            this.lblWeixin.Text = "加收0.5%";
            this.lblWeixin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmChooseReceiveType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblWeixin);
            this.Controls.Add(this.lblNeedPayAmount);
            this.Controls.Add(this.lblNeedPay);
            this.Controls.Add(this.txtPayAmount);
            this.Controls.Add(this.lblPayAmount);
            this.Controls.Add(this.lblRemainAmount);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.lblCarNum);
            this.Controls.Add(this.lalsss);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.skinLabel10);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtReceiveType);
            this.Controls.Add(this.skinLabel11);
            this.LBPageTitle = "确认收款方式";
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmChooseReceiveType";
            this.Size = new System.Drawing.Size(473, 415);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LBMetroComboBox txtReceiveType;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private CCWin.SkinControl.SkinLabel lblCustomerName;
        private CCWin.SkinControl.SkinLabel lalsss;
        private CCWin.SkinControl.SkinLabel lblCarNum;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel lblItem;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel lblAmount;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinLabel lblRemainAmount;
        private CCWin.SkinControl.SkinLabel lblPayAmount;
        private Controls.LBSkinTextBox txtPayAmount;
        private CCWin.SkinControl.SkinLabel lblNeedPay;
        private CCWin.SkinControl.SkinLabel lblNeedPayAmount;
        private CCWin.SkinControl.SkinLabel lblWeixin;
    }
}