using LB.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MI
{
    public partial class frmAutoPrint : LBUIPageBase
    {
        public bool IsFinish = false;
        int _Count = 0;
        Timer mTimer;
        public frmAutoPrint()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 200;
            mTimer.Tick += MTimer_Tick;

            mTimer.Enabled = true;
        }

        int miIndex = 0;
        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_Count==5)
                {
                    this.mTimer.Enabled = false;
                    this.Close();
                }
                miIndex++;
                _Count++;
                if (miIndex > 8)
                {
                    miIndex = 0;
                }
                this.lblProcess.Text = "保存成功！正在打印磅单，请稍后" + "".PadLeft(miIndex, '.') + "。" + "".PadRight(8 - miIndex, '.');
            }
            catch (Exception ex)
            {
                this.lblProcess.Text = ex.Message;
                this.mTimer.Enabled = false;
            }
        }
    }
}
