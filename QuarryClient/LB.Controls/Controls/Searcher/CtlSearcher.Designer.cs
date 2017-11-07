namespace LB.Controls.Searcher
{
    partial class CtlSearcher
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
            this.lblSearch = new CCWin.SkinControl.SkinLabel();
            this.txtColumnName = new DMSkin.Metro.Controls.MetroComboBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.chkIsCheck = new DMSkin.Metro.Controls.MetroCheckBox();
            this.txtSearchDropDown = new LB.Controls.LBMetroComboBox(this.components);
            this.txtSearchText = new LB.Controls.LBSkinTextBox(this.components);
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.BorderColor = System.Drawing.Color.White;
            this.lblSearch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblSearch.Location = new System.Drawing.Point(204, 7);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(94, 32);
            this.lblSearch.TabIndex = 23;
            this.lblSearch.Text = "查询内容包含";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtColumnName
            // 
            this.txtColumnName.DM_UseSelectable = true;
            this.txtColumnName.FormattingEnabled = true;
            this.txtColumnName.ItemHeight = 24;
            this.txtColumnName.Location = new System.Drawing.Point(72, 9);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(117, 30);
            this.txtColumnName.TabIndex = 26;
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel1.Location = new System.Drawing.Point(7, 7);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(59, 32);
            this.skinLabel1.TabIndex = 27;
            this.skinLabel1.Text = "查询列";
            this.skinLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkIsCheck
            // 
            this.chkIsCheck.AutoSize = true;
            this.chkIsCheck.DM_UseSelectable = true;
            this.chkIsCheck.Location = new System.Drawing.Point(575, 16);
            this.chkIsCheck.Name = "chkIsCheck";
            this.chkIsCheck.Size = new System.Drawing.Size(48, 17);
            this.chkIsCheck.TabIndex = 28;
            this.chkIsCheck.Text = "是否";
            this.chkIsCheck.Visible = false;
            // 
            // txtSearchDropDown
            // 
            this.txtSearchDropDown.CanBeEmpty = true;
            this.txtSearchDropDown.Caption = "";
            this.txtSearchDropDown.DM_UseSelectable = true;
            this.txtSearchDropDown.FormattingEnabled = true;
            this.txtSearchDropDown.ItemHeight = 24;
            this.txtSearchDropDown.Location = new System.Drawing.Point(629, 9);
            this.txtSearchDropDown.Name = "txtSearchDropDown";
            this.txtSearchDropDown.Size = new System.Drawing.Size(117, 30);
            this.txtSearchDropDown.TabIndex = 29;
            this.txtSearchDropDown.Visible = false;
            // 
            // txtSearchText
            // 
            this.txtSearchText.BackColor = System.Drawing.Color.Transparent;
            this.txtSearchText.CanBeEmpty = true;
            this.txtSearchText.Caption = "备注";
            this.txtSearchText.DownBack = null;
            this.txtSearchText.Icon = null;
            this.txtSearchText.IconIsButton = false;
            this.txtSearchText.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSearchText.IsPasswordChat = '\0';
            this.txtSearchText.IsSystemPasswordChar = false;
            this.txtSearchText.Lines = new string[0];
            this.txtSearchText.Location = new System.Drawing.Point(301, 10);
            this.txtSearchText.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearchText.MaxLength = 32767;
            this.txtSearchText.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtSearchText.MouseBack = null;
            this.txtSearchText.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtSearchText.Multiline = false;
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.NormlBack = null;
            this.txtSearchText.Padding = new System.Windows.Forms.Padding(5);
            this.txtSearchText.ReadOnly = false;
            this.txtSearchText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchText.Size = new System.Drawing.Size(130, 28);
            // 
            // 
            // 
            this.txtSearchText.SkinTxt.AccessibleName = "";
            this.txtSearchText.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtSearchText.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSearchText.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchText.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchText.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtSearchText.SkinTxt.Location = new System.Drawing.Point(0, 0);
            this.txtSearchText.SkinTxt.Name = "BaseText";
            this.txtSearchText.SkinTxt.TabIndex = 0;
            this.txtSearchText.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSearchText.SkinTxt.WaterText = "";
            this.txtSearchText.SkinTxt.WordWrap = false;
            this.txtSearchText.TabIndex = 24;
            this.txtSearchText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearchText.Visible = false;
            this.txtSearchText.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtSearchText.WaterText = "";
            this.txtSearchText.WordWrap = false;
            // 
            // CtlSearcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSearchDropDown);
            this.Controls.Add(this.chkIsCheck);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.txtColumnName);
            this.Controls.Add(this.txtSearchText);
            this.Controls.Add(this.lblSearch);
            this.Name = "CtlSearcher";
            this.Size = new System.Drawing.Size(829, 47);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LBSkinTextBox txtSearchText;
        private CCWin.SkinControl.SkinLabel lblSearch;
        private DMSkin.Metro.Controls.MetroComboBox txtColumnName;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private DMSkin.Metro.Controls.MetroCheckBox chkIsCheck;
        private LBMetroComboBox txtSearchDropDown;
    }
}
