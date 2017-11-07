namespace LB.MainForm.Permission
{
    partial class frmAddPermission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPermission));
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.txtPermissionName = new CCWin.SkinControl.SkinTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new LB.Controls.LBToolStripButton(this.components);
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel2
            // 
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(29, 49);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(83, 32);
            this.skinLabel2.TabIndex = 8;
            this.skinLabel2.Text = "分类名称";
            this.skinLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPermissionName
            // 
            this.txtPermissionName.BackColor = System.Drawing.Color.Transparent;
            this.txtPermissionName.DownBack = null;
            this.txtPermissionName.Icon = null;
            this.txtPermissionName.IconIsButton = false;
            this.txtPermissionName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPermissionName.IsPasswordChat = '\0';
            this.txtPermissionName.IsSystemPasswordChar = false;
            this.txtPermissionName.Lines = new string[0];
            this.txtPermissionName.Location = new System.Drawing.Point(115, 53);
            this.txtPermissionName.Margin = new System.Windows.Forms.Padding(0);
            this.txtPermissionName.MaxLength = 32767;
            this.txtPermissionName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtPermissionName.MouseBack = null;
            this.txtPermissionName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtPermissionName.Multiline = false;
            this.txtPermissionName.Name = "txtPermissionName";
            this.txtPermissionName.NormlBack = null;
            this.txtPermissionName.Padding = new System.Windows.Forms.Padding(5);
            this.txtPermissionName.ReadOnly = false;
            this.txtPermissionName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPermissionName.Size = new System.Drawing.Size(192, 28);
            // 
            // 
            // 
            this.txtPermissionName.SkinTxt.AccessibleName = "";
            this.txtPermissionName.SkinTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "asdfasdf",
            "adsfasdf"});
            this.txtPermissionName.SkinTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtPermissionName.SkinTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPermissionName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPermissionName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPermissionName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtPermissionName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtPermissionName.SkinTxt.Name = "BaseText";
            this.txtPermissionName.SkinTxt.Size = new System.Drawing.Size(182, 18);
            this.txtPermissionName.SkinTxt.TabIndex = 0;
            this.txtPermissionName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPermissionName.SkinTxt.WaterText = "";
            this.txtPermissionName.TabIndex = 7;
            this.txtPermissionName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPermissionName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtPermissionName.WaterText = "";
            this.txtPermissionName.WordWrap = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(402, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.LBPermissionCode = "";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 22);
            this.btnClose.Text = "关闭";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 109);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.txtPermissionName);
            this.Name = "frmAddPermission";
            this.Text = "添加权限分类";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinTextBox txtPermissionName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LBToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnSave;
    }
}