using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.WinFunction
{
    public class RemotingObject
    {
        /// <summary>
        /// 获取远程remoting对象
        /// </summary>
        /// <param name="type">远程remoting对象类型 </param>
        /// <param name="url">远程remoting对象地址</param>
        /// <returns>远程remoting对象</returns>
        public static object GetRemotingObject(Type type)
        {
            string ipaddress = GetIPAddress();
            return Activator.GetObject(type, ipaddress);
        }

        public static string GetIPAddress()
        {
            //return "http://localhost:2017/LBProject";
            string strWebLinkPath = Path.Combine(Application.StartupPath, "WebLink.ini");
            IniClass iniClass = new WinFunction.IniClass(strWebLinkPath);
            string strServer = iniClass.ReadValue("Link", "server");
            string strPort = iniClass.ReadValue("Link", "port");
            string strServicename = iniClass.ReadValue("Link", "servicename");
            strServicename = "LRB";
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
