namespace LB.MI.MI
{
    partial class frmSynBaseInfoFromServer
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
            this.lvCustomer = new System.Windows.Forms.ListView();
            this.单号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvCar = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvPrice = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWaist = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvCustomer
            // 
            this.lvCustomer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.单号,
            this.columnHeader1});
            this.lvCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCustomer.Location = new System.Drawing.Point(3, 17);
            this.lvCustomer.Name = "lvCustomer";
            this.lvCustomer.Size = new System.Drawing.Size(246, 366);
            this.lvCustomer.TabIndex = 2;
            this.lvCustomer.UseCompatibleStateImageBehavior = false;
            this.lvCustomer.View = System.Windows.Forms.View.Details;
            // 
            // 单号
            // 
            this.单号.Text = "客户名称";
            this.单号.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "同步结果";
            this.columnHeader1.Width = 500;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvCustomer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 386);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户资料";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvCar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(252, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 386);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "车辆资料";
            // 
            // lvCar
            // 
            this.lvCar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lvCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCar.Location = new System.Drawing.Point(3, 17);
            this.lvCar.Name = "lvCar";
            this.lvCar.Size = new System.Drawing.Size(246, 366);
            this.lvCar.TabIndex = 2;
            this.lvCar.UseCompatibleStateImageBehavior = false;
            this.lvCar.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "车牌号码";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "同步结果";
            this.columnHeader3.Width = 500;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvPrice);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(504, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 386);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "价格资料";
            // 
            // lvPrice
            // 
            this.lvPrice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.lvPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrice.Location = new System.Drawing.Point(3, 17);
            this.lvPrice.Name = "lvPrice";
            this.lvPrice.Size = new System.Drawing.Size(270, 366);
            this.lvPrice.TabIndex = 2;
            this.lvPrice.UseCompatibleStateImageBehavior = false;
            this.lvPrice.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "同步结果";
            this.columnHeader5.Width = 500;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblWaist);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 50);
            this.panel1.TabIndex = 3;
            // 
            // lblWaist
            // 
            this.lblWaist.AutoSize = true;
            this.lblWaist.Font = new System.Drawing.Font("宋体", 12F);
            this.lblWaist.ForeColor = System.Drawing.Color.Red;
            this.lblWaist.Location = new System.Drawing.Point(20, 19);
            this.lblWaist.Name = "lblWaist";
            this.lblWaist.Size = new System.Drawing.Size(280, 16);
            this.lblWaist.TabIndex = 0;
            this.lblWaist.Text = "正在执行同步，请耐心等待。。。。。";
            // 
            // frmSynBaseInfoFromServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.LBPageTitle = "K3同步进度";
            this.Name = "frmSynBaseInfoFromServer";
            this.Size = new System.Drawing.Size(780, 436);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCustomer;
        private System.Windows.Forms.ColumnHeader 单号;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvCar;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvPrice;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWaist;
    }
}