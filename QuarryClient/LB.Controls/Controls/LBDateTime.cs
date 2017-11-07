using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using static DMSkin.Metro.Drawing.MetroPaint.ForeColor;

namespace LB.Controls
{
    public partial class LBDateTime : System.Windows.Forms.UserControl, ILBTextBox
    {
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker txtBillDate;
        public System.Windows.Forms.DateTimePicker TextBox
        {
            get
            {
                return txtBillDate;
            }
        }

        public LBDateTime()
        {
            InitializeComponent();
            
            this.lblTitle.TextChanged += lblTitle_TextChanged;
            this.lblTitle.VisibleChanged += LblTitle_VisibleChanged;
            this.SizeChanged += CoolTextBox_SizeChanged;

        }

        public LBDateTime(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            
            this.lblTitle.TextChanged += lblTitle_TextChanged;
            this.lblTitle.VisibleChanged += LblTitle_VisibleChanged;
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtBillDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(17, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "  ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBillDate
            // 
            this.txtBillDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtBillDate.Location = new System.Drawing.Point(20, 0);
            this.txtBillDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtBillDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.txtBillDate.Name = "txtBillDate";
            this.txtBillDate.Size = new System.Drawing.Size(164, 30);
            this.txtBillDate.TabIndex = 2;
            // 
            // LBDateTime
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtBillDate);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LBDateTime";
            this.Size = new System.Drawing.Size(184, 25);
            this.ResumeLayout(false);

        }

        private void CoolTextBox_SizeChanged(object sender, EventArgs e)
        {
            if (!this.lblTitle.Visible)
            {
                this.txtBillDate.Width = this.Width;
            }
            else
            {
                this.txtBillDate.Width = this.Width - this.lblTitle.Width - 5;
            }
        }

        private void LblTitle_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.lblTitle.Visible)
            {
                this.txtBillDate.Width = this.Width;
            }
            else
            {
                this.txtBillDate.Width = this.Width - this.lblTitle.Width - 5;
            }
        }

        void lblTitle_TextChanged(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            SizeF sizeF = graphics.MeasureString(this.lblTitle.Text, this.lblTitle.Font);
            this.lblTitle.Width = (int)sizeF.Width + 5;
            this.txtBillDate.Width = this.Width - this.lblTitle.Width - 5;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string LBTitle
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;

            }
        }

        public bool LBTitleVisible
        {
            get
            {
                return this.lblTitle.Visible;
            }
            set
            {
                this.lblTitle.Visible = value;

            }
        }

        private bool _CanBeEmpty = true;
        [Description("是否可为空")]
        public bool CanBeEmpty
        {
            get
            {
                return _CanBeEmpty;
            }
            set
            {
                _CanBeEmpty = value;
            }
        }

        private string _Caption = "";
        [Description("控件名称")]
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
            }
        }

        [Description("值是否为空")]
        public bool IsEmptyValue
        {
            get
            {
                return this.Text.TrimEnd() == "" ? true : false;
            }
        }
    }
}
