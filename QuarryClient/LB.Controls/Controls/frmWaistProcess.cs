using LB.Common.Synchronous;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class frmWaistProcess : LBUIPageBase
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        bool bolFinish = false;
        string strMsg = "";
        public frmWaistProcess()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Thread mThread = new Thread(Action);
            mThread.Start();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (bolFinish)
                {
                    if (strMsg != "")
                    {
                        MessageBox.Show(strMsg);
                    }
                    else
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("同步成功！");
                    }
                    timer.Enabled = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
            }
        }

        private void Action()
        {
            try
            {
                SynchronousPrice.SynchronousClientFromServer();
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
            }
            finally
            {
                bolFinish = true;
            }
        }
    }
}
