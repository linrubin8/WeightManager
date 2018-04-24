using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace LB.Service
{
    public class RemotingObject
    {
        /// <summary>
        /// 获取远程remoting对象
        /// </summary>
        /// <param name="type">远程remoting对象类型 </param>
        /// <param name="url">远程remoting对象地址</param>
        /// <returns>远程remoting对象</returns>
        public object GetRemotingObject(Type type)
        {
            string ipaddress = GetIPAddress();
            return Activator.GetObject(type, ipaddress);
        }

        public static string GetIPAddress()
        {
            string strServer = ConfigurationManager.AppSettings["server"].ToString();
            string strPort = ConfigurationManager.AppSettings["port"].ToString();
            string strServicename = "LRB";
            string strUrl = "";
            if (strPort == "" || strPort == "0")
            {
                strUrl = "http://" + strServer + "/"+ strServicename;
            }
            else
            {
                strUrl = "http://" + strServer + ":" + strPort + "/"+ strServicename;
            }
            return strUrl;
        }
    }
}
