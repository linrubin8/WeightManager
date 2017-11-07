namespace LB.MainForm
{
    partial class frmSQLScriptBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLScriptBuilder));
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage1 = new CCWin.SkinControl.SkinTabPage();
            this.rtfRichTextBox1 = new CCWin.SkinControl.RtfRichTextBox();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtTableName = new CCWin.SkinControl.SkinTextBox();
            this.btnBuild = new LB.Controls.LBSkinButton(this.components);
            this.cbIdentity = new System.Windows.Forms.CheckBox();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPage1);
            this.skinTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(0, 0);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 0;
            this.skinTabControl1.Size = new System.Drawing.Size(843, 397);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 0;
            // 
            // skinTabPage1
            // 
            this.skinTabPage1.BackColor = System.Drawing.Color.White;
            this.skinTabPage1.Controls.Add(this.cbIdentity);
            this.skinTabPage1.Controls.Add(this.rtfRichTextBox1);
            this.skinTabPage1.Controls.Add(this.skinLabel2);
            this.skinTabPage1.Controls.Add(this.txtTableName);
            this.skinTabPage1.Controls.Add(this.btnBuild);
            this.skinTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage1.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage1.Name = "skinTabPage1";
            this.skinTabPage1.Size = new System.Drawing.Size(843, 361);
            this.skinTabPage1.TabIndex = 0;
            this.skinTabPage1.TabItemImage = null;
            this.skinTabPage1.Text = "生成表数据";
            // 
            // rtfRichTextBox1
            // 
            this.rtfRichTextBox1.HiglightColor = CCWin.SkinControl.RtfRichTextBox.RtfColor.White;
            this.rtfRichTextBox1.Location = new System.Drawing.Point(16, 62);
            this.rtfRichTextBox1.Name = "rtfRichTextBox1";
            this.rtfRichTextBox1.Size = new System.Drawing.Size(815, 287);
            this.rtfRichTextBox1.TabIndex = 8;
            this.rtfRichTextBox1.Text = "";
            this.rtfRichTextBox1.TextColor = CCWin.SkinControl.RtfRichTextBox.RtfColor.Black;
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(12, 15);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 7;
            this.skinLabel2.Text = "输入表名";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.Color.Transparent;
            this.txtTableName.DownBack = null;
            this.txtTableName.Icon = null;
            this.txtTableName.IconIsButton = false;
            this.txtTableName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTableName.IsPasswordChat = '\0';
            this.txtTableName.IsSystemPasswordChar = false;
            this.txtTableName.Lines = new string[0];
            this.txtTableName.Location = new System.Drawing.Point(103, 19);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(0);
            this.txtTableName.MaxLength = 32767;
            this.txtTableName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtTableName.MouseBack = null;
            this.txtTableName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtTableName.Multiline = false;
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.NormlBack = null;
            this.txtTableName.Padding = new System.Windows.Forms.Padding(5);
            this.txtTableName.ReadOnly = false;
            this.txtTableName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTableName.Size = new System.Drawing.Size(137, 28);
            // 
            // 
            // 
            this.txtTableName.SkinTxt.AccessibleName = "";
            this.txtTableName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtTableName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtTableName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTableName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTableName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtTableName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtTableName.SkinTxt.Name = "BaseText";
            this.txtTableName.SkinTxt.Size = new System.Drawing.Size(127, 18);
            this.txtTableName.SkinTxt.TabIndex = 0;
            this.txtTableName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTableName.SkinTxt.WaterText = "";
            this.txtTableName.TabIndex = 6;
            this.txtTableName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTableName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtTableName.WaterText = "";
            this.txtTableName.WordWrap = true;
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.Color.Transparent;
            this.btnBuild.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnBuild.DownBack = null;
            this.btnBuild.LBPermissionCode = "";
            this.btnBuild.Location = new System.Drawing.Point(405, 23);
            this.btnBuild.MouseBack = null;
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.NormlBack = null;
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "确定并生成";
            this.btnBuild.UseVisualStyleBackColor = false;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // cbIdentity
            // 
            this.cbIdentity.AutoSize = true;
            this.cbIdentity.Location = new System.Drawing.Point(255, 27);
            this.cbIdentity.Name = "cbIdentity";
            this.cbIdentity.Size = new System.Drawing.Size(144, 16);
            this.cbIdentity.TabIndex = 9;
            this.cbIdentity.Text = "是否添加Identity标识";
            this.cbIdentity.UseVisualStyleBackColor = true;
            // 
            // frmSQLScriptBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 397);
            this.Controls.Add(this.skinTabControl1);
            this.Name = "frmSQLScriptBuilder";
            this.Text = "SQL脚本生成器";
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPage1.ResumeLayout(false);
            this.skinTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPage1;
        private Controls.LBSkinButton btnBuild;
        private CCWin.SkinControl.SkinTextBox txtTableName;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.RtfRichTextBox rtfRichTextBox1;
        private System.Windows.Forms.CheckBox cbIdentity;
    }
}