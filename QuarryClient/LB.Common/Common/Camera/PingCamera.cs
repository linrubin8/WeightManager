using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LB.Common.Camera
{
    public class PingCamera
    {
        //public static bool PingAddress(string strIP,string strPort)
        //{
        //    if (!string.IsNullOrEmpty(strPort))
        //    {
        //        IPAddress ip = IPAddress.Parse(strIP);
        //        IPEndPoint point = new IPEndPoint(ip, int.Parse(strPort));
        //        try
        //        {
        //            TcpClient tcp = new TcpClient();
  
        //            tcp.Connect(point);
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        private bool IsConnectionSuccessful = false;
        private Exception socketexception;
        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        public bool Connect(string ipAddress,int iPort, int timeoutMSec)
        {
            TimeoutObject.Reset();
            socketexception = null;

            string serverip = Convert.ToString(ipAddress);
            int serverport = iPort;
            TcpClient tcpclient = new TcpClient();

            tcpclient.BeginConnect(serverip, serverport,
                new AsyncCallback(CallBackMethod), tcpclient);

            if (TimeoutObject.WaitOne(timeoutMSec, false))
            {
                if (IsConnectionSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                tcpclient.Close();
                return false;
            }
        }
        private void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }
    }
}
