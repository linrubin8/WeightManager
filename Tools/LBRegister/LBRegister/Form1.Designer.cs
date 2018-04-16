namespace LBRegister
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeadLine = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegister = new System.Windows.Forms.TextBox();
            this.rbWeight = new System.Windows.Forms.RadioButton();
            this.rbGrooveWeight = new System.Windows.Forms.RadioButton();
            this.rbGrooveCount = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "序列号";
            // 
            // txtSeries
            // 
            this.txtSeries.Location = new System.Drawing.Point(67, 12);
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(304, 21);
            this.txtSeries.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "有效期";
            // 
            // txtDeadLine
            // 
            this.txtDeadLine.Location = new System.Drawing.Point(67, 77);
            this.txtDeadLine.Name = "txtDeadLine";
            this.txtDeadLine.Size = new System.Drawing.Size(304, 21);
            this.txtDeadLine.TabIndex = 3;
            this.txtDeadLine.Value = new System.DateTime(2099, 1, 8, 21, 50, 0, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "确认生成注册码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "注册码";
            // 
            // txtRegister
            // 
            this.txtRegister.Location = new System.Drawing.Point(67, 104);
            this.txtRegister.Name = "txtRegister";
            this.txtRegister.ReadOnly = true;
            this.txtRegister.Size = new System.Drawing.Size(304, 21);
            this.txtRegister.TabIndex = 6;
            // 
            // rbWeight
            // 
            this.rbWeight.AutoSize = true;
            this.rbWeight.Checked = true;
            this.rbWeight.Location = new System.Drawing.Point(22, 46);
            this.rbWeight.Name = "rbWeight";
            this.rbWeight.Size = new System.Drawing.Size(71, 16);
            this.rbWeight.TabIndex = 7;
            this.rbWeight.TabStop = true;
            this.rbWeight.Text = "称重软件";
            this.rbWeight.UseVisualStyleBackColor = true;
            // 
            // rbGrooveWeight
            // 
            this.rbGrooveWeight.AutoSize = true;
            this.rbGrooveWeight.Location = new System.Drawing.Point(110, 46);
            this.rbGrooveWeight.Name = "rbGrooveWeight";
            this.rbGrooveWeight.Size = new System.Drawing.Size(71, 16);
            this.rbGrooveWeight.TabIndex = 8;
            this.rbGrooveWeight.TabStop = true;
            this.rbGrooveWeight.Text = "入槽称重";
            this.rbGrooveWeight.UseVisualStyleBackColor = true;
            // 
            // rbGrooveCount
            // 
            this.rbGrooveCount.AutoSize = true;
            this.rbGrooveCount.Location = new System.Drawing.Point(200, 46);
            this.rbGrooveCount.Name = "rbGrooveCount";
            this.rbGrooveCount.Size = new System.Drawing.Size(95, 16);
            this.rbGrooveCount.TabIndex = 9;
            this.rbGrooveCount.TabStop = true;
            this.rbGrooveCount.Text = "车辆计数软件";
            this.rbGrooveCount.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 166);
            this.Controls.Add(this.rbGrooveCount);
            this.Controls.Add(this.rbGrooveWeight);
            this.Controls.Add(this.rbWeight);
            this.Controls.Add(this.txtRegister);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDeadLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSeries);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDeadLine;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegister;
        private System.Windows.Forms.RadioButton rbWeight;
        private System.Windows.Forms.RadioButton rbGrooveWeight;
        private System.Windows.Forms.RadioButton rbGrooveCount;
    }
}

