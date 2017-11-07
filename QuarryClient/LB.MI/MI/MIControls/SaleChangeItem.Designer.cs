namespace LB.MI.MI.MIControls
{
    partial class SaleChangeItem
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblChangeFrom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblChangeTo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.lblTitle.Location = new System.Drawing.Point(3, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(56, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            // 
            // lblChangeFrom
            // 
            this.lblChangeFrom.AutoSize = true;
            this.lblChangeFrom.Font = new System.Drawing.Font("宋体", 12F);
            this.lblChangeFrom.Location = new System.Drawing.Point(107, 13);
            this.lblChangeFrom.Name = "lblChangeFrom";
            this.lblChangeFrom.Size = new System.Drawing.Size(56, 16);
            this.lblChangeFrom.TabIndex = 1;
            this.lblChangeFrom.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(226, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "=>";
            // 
            // lblChangeTo
            // 
            this.lblChangeTo.AutoSize = true;
            this.lblChangeTo.Font = new System.Drawing.Font("宋体", 12F);
            this.lblChangeTo.Location = new System.Drawing.Point(263, 13);
            this.lblChangeTo.Name = "lblChangeTo";
            this.lblChangeTo.Size = new System.Drawing.Size(56, 16);
            this.lblChangeTo.TabIndex = 3;
            this.lblChangeTo.Text = "label1";
            // 
            // SaleChangeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblChangeTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblChangeFrom);
            this.Controls.Add(this.lblTitle);
            this.Name = "SaleChangeItem";
            this.Size = new System.Drawing.Size(406, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblChangeFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblChangeTo;
    }
}
