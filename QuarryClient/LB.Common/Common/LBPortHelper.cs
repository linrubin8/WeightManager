using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LB.Common
{
    public class LBPortHelper
    {
        /// <summary>
        /// 网络是否稳定
        /// </summary>
        public static bool IsWebSteady = true;
        private static Thread thread = null;


        public static void StartCheckConnect()
        {
            thread = new Thread(StartAction);
            thread.Start();
        }

        public static void EndThread()
        {
            try
            {
                if (thread != null)
                {
                    thread.Abort();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private static void StartAction()
        {
            while (true)
            {
                try
                {
                    IsWebSteady = ExecuteSQL.TestConnectStatus();

                    Thread.Sleep(5000);
                }
                catch(Exception ex)
                {
                    IsWebSteady = false;
                }
            }
        }
    }
}
