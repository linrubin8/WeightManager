using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.WinFunction
{
    public class SessionHelper
    {
        private static System.Windows.Forms.Timer mTimer = null;

        public static void StartTakeSession()
        {
            if (mTimer == null)
            {
                mTimer = new System.Windows.Forms.Timer();
                mTimer.Tick += MTimer_Tick;
                mTimer.Interval = 1000 * 60;
                mTimer.Enabled = true;
            }
            if (!mTimer.Enabled)
            {
                mTimer.Enabled = true;
            }
        }

        public static void EndTakeSession()
        {
            if (mTimer != null)
            {
                mTimer.Enabled = false;
            }
        }

        private static void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread thread = new System.Threading.Thread(TakeSessionThread);
                thread.Start();
            }
            catch(Exception ex)
            {

            }
        }

        private static void TakeSessionThread()
        {
            try
            {
                ExecuteSQL.TakeSession();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
