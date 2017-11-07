namespace LB.MI.MI
{
    partial class frmSaleCarChangeReturnAmount
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
            this.rbCreateReceiveBill = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbCreateReceiveBill
            // 
            this.rbCreateReceiveBill.AutoSize = true;
            this.rbCreateReceiveBill.Location = new System.Drawing.Point(43, 59);
            this.rbCreateReceiveBill.Name = "rbCreateReceiveBill";
            this.rbCreateReceiveBill.Size = new System.Drawing.Size(215, 16);
            this.rbCreateReceiveBill.TabIndex = 0;
            this.rbCreateReceiveBill.TabStop = true;
            this.rbCreateReceiveBill.Text = "剩余金额以充值方式充值至客户账户";
            this.rbCreateReceiveBill.UseVisualStyleBackColor = true;
            // 
            // frmSaleCarChangeReturnAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbCreateReceiveBill);
            this.Name = "frmSaleCarChangeReturnAmount";
            this.Size = new System.Drawing.Size(444, 311);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbCreateReceiveBill;
    }
}
