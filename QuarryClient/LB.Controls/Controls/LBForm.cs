using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBForm : Form
    {
        private LBUIPageBase _LBPage;
        [Description("LBUIPageBase页面（只读）")]//
        public LBUIPageBase LBPage
        {
            get
            {
                return _LBPage;
            }
        }
        public LBForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = LB.StyleResource.Properties.Resources.LBLogo;
        }

        public LBForm(LBUIPageBase page)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = LB.StyleResource.Properties.Resources.LBLogo;
            this.AutoSize = !page.PageAutoSize;
            if (page.PageAutoSize)//当页签设置自定义大小-200
            {
                this.Width = Screen.PrimaryScreen.Bounds.Width-50;
                this.Height = Screen.PrimaryScreen.Bounds.Height - 50;
            }
            else
            {
                this.Width = page.Width + 15;
                this.Height = page.Height + 35;
            }
            this.Text = page.LBPageTitle;
            page.Dock = DockStyle.Fill;
            page.FormClosed += Page_FormClosed;
            this.Controls.Add(page);
        }

        private void Page_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (_LBPage != null)
            {
                bool bolCancel;
                _LBPage.StartClose(out bolCancel);
                e.Cancel = bolCancel;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }
}
