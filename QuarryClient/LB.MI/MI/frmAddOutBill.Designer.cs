namespace LB.MI
{
    partial class frmAddOutBill
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
            this.txtCarID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.txtItemID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.txtCustomerID = new LB.Controls.LBTextBox.CoolTextBox();
            this.skinLabel10 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtCarTare = new System.Windows.Forms.TextBox();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.txtCalculateType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSuttleWeight = new System.Windows.Forms.TextBox();
            this.skinLabel17 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel15 = new CCWin.SkinControl.SkinLabel();
            this.txtReceiveType = new LB.Controls.LBMetroComboBox(this.components);
            this.skinLabel11 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.txtBillTimeIn = new System.Windows.Forms.DateTimePicker();
            this.txtBillDateIn = new System.Windows.Forms.DateTimePicker();
            this.txtAddReason = new LB.Controls.LBTextBox.CoolTextBox();
            this.SuspendLayout();
            // 
            // txtCarID
            // 
            this.txtCarID.BackColor = System.Drawing.Color.Transparent;
            this.txtCarID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCarID.CanBeEmpty = false;
            this.txtCarID.Caption = "车号";
            this.txtCarID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarID.LBTitle = "  ";
            this.txtCarID.LBTitleVisible = false;
            this.txtCarID.Location = new System.Drawing.Point(136, 26);
            this.txtCarID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.PopupWidth = 120;
            this.txtCarID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCarID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCarID.Size = new System.Drawing.Size(188, 34);
            this.txtCarID.TabIndex = 49;
            // 
            // skinLabel8
            // 
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel8.Location = new System.Drawing.Point(68, 26);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(55, 30);
            this.skinLabel8.TabIndex = 52;
            this.skinLabel8.Text = "车 号";
            this.skinLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemID
            // 
            this.txtItemID.BackColor = System.Drawing.Color.Transparent;
            this.txtItemID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtItemID.CanBeEmpty = false;
            this.txtItemID.Caption = "客户名称";
            this.txtItemID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtItemID.LBTitle = "  ";
            this.txtItemID.LBTitleVisible = false;
            this.txtItemID.Location = new System.Drawing.Point(136, 68);
            this.txtItemID.Margin = new System.Windows.Forms.Padding(0);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.PopupWidth = 120;
            this.txtItemID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtItemID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtItemID.Size = new System.Drawing.Size(187, 34);
            this.txtItemID.TabIndex = 50;
            // 
            // skinLabel9
            // 
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel9.Location = new System.Drawing.Point(30, 66);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(103, 28);
            this.skinLabel9.TabIndex = 53;
            this.skinLabel9.Text = "货物名称";
            this.skinLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomerID.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtCustomerID.CanBeEmpty = false;
            this.txtCustomerID.Caption = "客户名称";
            this.txtCustomerID.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCustomerID.LBTitle = "  ";
            this.txtCustomerID.LBTitleVisible = false;
            this.txtCustomerID.Location = new System.Drawing.Point(136, 111);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.PopupWidth = 120;
            this.txtCustomerID.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtCustomerID.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCustomerID.Size = new System.Drawing.Size(187, 34);
            this.txtCustomerID.TabIndex = 51;
            // 
            // skinLabel10
            // 
            this.skinLabel10.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel10.BorderColor = System.Drawing.Color.White;
            this.skinLabel10.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel10.Location = new System.Drawing.Point(35, 111);
            this.skinLabel10.Name = "skinLabel10";
            this.skinLabel10.Size = new System.Drawing.Size(95, 29);
            this.skinLabel10.TabIndex = 54;
            this.skinLabel10.Text = "客户名称";
            this.skinLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel2.Location = new System.Drawing.Point(342, 71);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(60, 30);
            this.skinLabel2.TabIndex = 56;
            this.skinLabel2.Text = "皮 重";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCarTare
            // 
            this.txtCarTare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarTare.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtCarTare.Location = new System.Drawing.Point(416, 69);
            this.txtCarTare.Multiline = true;
            this.txtCarTare.Name = "txtCarTare";
            this.txtCarTare.ReadOnly = true;
            this.txtCarTare.Size = new System.Drawing.Size(188, 33);
            this.txtCarTare.TabIndex = 55;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveAndClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveAndClose.Location = new System.Drawing.Point(281, 312);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(120, 29);
            this.btnSaveAndClose.TabIndex = 64;
            this.btnSaveAndClose.Text = "保存并关闭";
            this.btnSaveAndClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // txtCalculateType
            // 
            this.txtCalculateType.CanBeEmpty = false;
            this.txtCalculateType.Caption = "每周";
            this.txtCalculateType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtCalculateType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtCalculateType.DM_UseSelectable = true;
            this.txtCalculateType.FormattingEnabled = true;
            this.txtCalculateType.ItemHeight = 28;
            this.txtCalculateType.Location = new System.Drawing.Point(136, 153);
            this.txtCalculateType.Name = "txtCalculateType";
            this.txtCalculateType.Size = new System.Drawing.Size(188, 34);
            this.txtCalculateType.TabIndex = 66;
            // 
            // skinLabel7
            // 
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel7.Location = new System.Drawing.Point(33, 156);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(96, 26);
            this.skinLabel7.TabIndex = 67;
            this.skinLabel7.Text = "计价方式";
            this.skinLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel1.Location = new System.Drawing.Point(3, 276);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(127, 29);
            this.skinLabel1.TabIndex = 68;
            this.skinLabel1.Text = "手工录入原因";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel3
            // 
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel3.Location = new System.Drawing.Point(341, 26);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(60, 30);
            this.skinLabel3.TabIndex = 71;
            this.skinLabel3.Text = "毛 重";
            this.skinLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtTotalWeight.Location = new System.Drawing.Point(416, 26);
            this.txtTotalWeight.Multiline = true;
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.Size = new System.Drawing.Size(188, 34);
            this.txtTotalWeight.TabIndex = 70;
            // 
            // skinLabel4
            // 
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel4.Location = new System.Drawing.Point(344, 154);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(60, 30);
            this.skinLabel4.TabIndex = 77;
            this.skinLabel4.Text = "单 价";
            this.skinLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtAmount.Location = new System.Drawing.Point(416, 196);
            this.txtAmount.Multiline = true;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(188, 34);
            this.txtAmount.TabIndex = 76;
            // 
            // txtPrice
            // 
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtPrice.Location = new System.Drawing.Point(416, 155);
            this.txtPrice.Multiline = true;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(188, 32);
            this.txtPrice.TabIndex = 75;
            // 
            // txtSuttleWeight
            // 
            this.txtSuttleWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSuttleWeight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtSuttleWeight.Location = new System.Drawing.Point(416, 111);
            this.txtSuttleWeight.Multiline = true;
            this.txtSuttleWeight.Name = "txtSuttleWeight";
            this.txtSuttleWeight.ReadOnly = true;
            this.txtSuttleWeight.Size = new System.Drawing.Size(188, 34);
            this.txtSuttleWeight.TabIndex = 74;
            // 
            // skinLabel17
            // 
            this.skinLabel17.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel17.BorderColor = System.Drawing.Color.White;
            this.skinLabel17.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel17.Location = new System.Drawing.Point(344, 199);
            this.skinLabel17.Name = "skinLabel17";
            this.skinLabel17.Size = new System.Drawing.Size(60, 27);
            this.skinLabel17.TabIndex = 73;
            this.skinLabel17.Text = "金 额";
            this.skinLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel15
            // 
            this.skinLabel15.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel15.BorderColor = System.Drawing.Color.White;
            this.skinLabel15.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel15.Location = new System.Drawing.Point(342, 115);
            this.skinLabel15.Name = "skinLabel15";
            this.skinLabel15.Size = new System.Drawing.Size(60, 30);
            this.skinLabel15.TabIndex = 72;
            this.skinLabel15.Text = "净 重";
            this.skinLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReceiveType
            // 
            this.txtReceiveType.CanBeEmpty = false;
            this.txtReceiveType.Caption = "每周";
            this.txtReceiveType.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.txtReceiveType.DM_FontWeight = DMSkin.Metro.MetroComboBoxWeight.Bold;
            this.txtReceiveType.DM_UseSelectable = true;
            this.txtReceiveType.FormattingEnabled = true;
            this.txtReceiveType.ItemHeight = 28;
            this.txtReceiveType.Location = new System.Drawing.Point(136, 196);
            this.txtReceiveType.Name = "txtReceiveType";
            this.txtReceiveType.Size = new System.Drawing.Size(186, 34);
            this.txtReceiveType.TabIndex = 78;
            // 
            // skinLabel11
            // 
            this.skinLabel11.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel11.BorderColor = System.Drawing.Color.White;
            this.skinLabel11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel11.Location = new System.Drawing.Point(37, 196);
            this.skinLabel11.Name = "skinLabel11";
            this.skinLabel11.Size = new System.Drawing.Size(84, 27);
            this.skinLabel11.TabIndex = 79;
            this.skinLabel11.Text = "收款方式";
            this.skinLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinLabel5
            // 
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.skinLabel5.Location = new System.Drawing.Point(35, 237);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(95, 29);
            this.skinLabel5.TabIndex = 82;
            this.skinLabel5.Text = "出场时间";
            this.skinLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillTimeIn
            // 
            this.txtBillTimeIn.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillTimeIn.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillTimeIn.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.txtBillTimeIn.Location = new System.Drawing.Point(258, 240);
            this.txtBillTimeIn.Name = "txtBillTimeIn";
            this.txtBillTimeIn.Size = new System.Drawing.Size(85, 26);
            this.txtBillTimeIn.TabIndex = 81;
            // 
            // txtBillDateIn
            // 
            this.txtBillDateIn.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.txtBillDateIn.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBillDateIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBillDateIn.Location = new System.Drawing.Point(135, 240);
            this.txtBillDateIn.Name = "txtBillDateIn";
            this.txtBillDateIn.Size = new System.Drawing.Size(117, 26);
            this.txtBillDateIn.TabIndex = 80;
            // 
            // txtAddReason
            // 
            this.txtAddReason.BackColor = System.Drawing.Color.Transparent;
            this.txtAddReason.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtAddReason.CanBeEmpty = false;
            this.txtAddReason.Caption = "客户名称";
            this.txtAddReason.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtAddReason.LBTitle = "  ";
            this.txtAddReason.LBTitleVisible = false;
            this.txtAddReason.Location = new System.Drawing.Point(135, 276);
            this.txtAddReason.Margin = new System.Windows.Forms.Padding(0);
            this.txtAddReason.Name = "txtAddReason";
            this.txtAddReason.PopupWidth = 188;
            this.txtAddReason.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.txtAddReason.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtAddReason.Size = new System.Drawing.Size(469, 34);
            this.txtAddReason.TabIndex = 83;
            // 
            // frmAddOutBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAddReason);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.txtBillTimeIn);
            this.Controls.Add(this.txtBillDateIn);
            this.Controls.Add(this.txtReceiveType);
            this.Controls.Add(this.skinLabel11);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtSuttleWeight);
            this.Controls.Add(this.skinLabel17);
            this.Controls.Add(this.skinLabel15);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.txtTotalWeight);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.txtCalculateType);
            this.Controls.Add(this.skinLabel7);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtCarTare);
            this.Controls.Add(this.txtCarID);
            this.Controls.Add(this.skinLabel8);
            this.Controls.Add(this.txtItemID);
            this.Controls.Add(this.skinLabel9);
            this.Controls.Add(this.txtCustomerID);
            this.Controls.Add(this.skinLabel10);
            this.LBPageTitle = "手工录入出场单";
            this.Name = "frmAddOutBill";
            this.Size = new System.Drawing.Size(633, 352);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LBTextBox.CoolTextBox txtCarID;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private Controls.LBTextBox.CoolTextBox txtItemID;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private Controls.LBTextBox.CoolTextBox txtCustomerID;
        private CCWin.SkinControl.SkinLabel skinLabel10;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private System.Windows.Forms.TextBox txtCarTare;
        private System.Windows.Forms.Button btnSaveAndClose;
        private Controls.LBMetroComboBox txtCalculateType;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSuttleWeight;
        private CCWin.SkinControl.SkinLabel skinLabel17;
        private CCWin.SkinControl.SkinLabel skinLabel15;
        private Controls.LBMetroComboBox txtReceiveType;
        private CCWin.SkinControl.SkinLabel skinLabel11;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private System.Windows.Forms.DateTimePicker txtBillTimeIn;
        private System.Windows.Forms.DateTimePicker txtBillDateIn;
        private Controls.LBTextBox.CoolTextBox txtAddReason;
    }
}
