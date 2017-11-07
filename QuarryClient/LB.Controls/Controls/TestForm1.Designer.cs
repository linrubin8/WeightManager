namespace LB.Controls
{
    partial class TestForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.lbToolStripButton1 = new LB.Controls.LBToolStripButton(this.components);
            this.lbToolStripDropDownButton1 = new LB.Controls.LBToolStripDropDownButton(this.components);
            this.wqerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.lbToolStripButton1,
            this.toolStripDropDownButton1,
            this.lbToolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // lbToolStripButton1
            // 
            this.lbToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lbToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("lbToolStripButton1.Image")));
            this.lbToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lbToolStripButton1.LBPermissionCode = "";
            this.lbToolStripButton1.Name = "lbToolStripButton1";
            this.lbToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.lbToolStripButton1.Text = "lbToolStripButton1";
            // 
            // lbToolStripDropDownButton1
            // 
            this.lbToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lbToolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wqerToolStripMenuItem});
            this.lbToolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("lbToolStripDropDownButton1.Image")));
            this.lbToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lbToolStripDropDownButton1.LBPermissionCode = "";
            this.lbToolStripDropDownButton1.Name = "lbToolStripDropDownButton1";
            this.lbToolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.lbToolStripDropDownButton1.Text = "lbToolStripDropDownButton1";
            // 
            // wqerToolStripMenuItem
            // 
            this.wqerToolStripMenuItem.Name = "wqerToolStripMenuItem";
            this.wqerToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.wqerToolStripMenuItem.Text = "wqer";
            // 
            // TestForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TestForm1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private LBToolStripButton lbToolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private LBToolStripDropDownButton lbToolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem wqerToolStripMenuItem;
    }
}