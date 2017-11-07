namespace LB.MI.MI
{
    partial class frmSaleCarChangeConfirm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.gbReceive = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblChange);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 170);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变更详细信息";
            // 
            // lblChange
            // 
            this.lblChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChange.Location = new System.Drawing.Point(3, 22);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(426, 145);
            this.lblChange.TabIndex = 0;
            this.lblChange.Text = "label1";
            // 
            // gbReceive
            // 
            this.gbReceive.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbReceive.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.gbReceive.Location = new System.Drawing.Point(0, 170);
            this.gbReceive.Name = "gbReceive";
            this.gbReceive.Size = new System.Drawing.Size(432, 123);
            this.gbReceive.TabIndex = 2;
            this.gbReceive.TabStop = false;
            this.gbReceive.Text = "款项变更处理";
            // 
            // frmSaleCarChangeConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbReceive);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSaleCarChangeConfirm";
            this.Size = new System.Drawing.Size(432, 369);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.GroupBox gbReceive;
    }
}
