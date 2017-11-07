using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.RMT.Base
{
    internal class RemotingHelper
    {
        internal static void InitRemotingConfig()
        {
            string strRemotingPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "LBRemoting.Config");
            if (!System.IO.File.Exists(strRemotingPath))
            {
                WriteRemotingConfig("127.0.0.1","LBProject", "2017","lb_project","1",true,"sa","Abc12345");
            }
        }

        internal  static void WriteRemotingConfig(string strServerURL,string strServerName,
            string strPort,string strDBName,string strRemotingForceMode,bool bolLoginSecure,
            string strDBUser,string strDBPW)
        {
            string strRemotingPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "LBRemoting.Config");
            IniClass iniClass = new IniClass(strRemotingPath);
            iniClass.WriteValue("Remoting", "Port", strPort);
            iniClass.WriteValue("Remoting", "ServerURL", strServerURL);
            iniClass.WriteValue("Remoting", "ServerName", strServerName);
            iniClass.WriteValue("Remoting", "DBName", strDBName);
            iniClass.WriteValue("Remoting", "LoginSecure", bolLoginSecure?"1":"0");
            iniClass.WriteValue("Remoting", "DBUser", strDBUser);
            iniClass.WriteValue("Remoting", "DBPW", strDBPW);
            iniClass.WriteValue("Remoting", "RemotingForceMode", strRemotingForceMode);
        }

        internal static void ReadRemotingConfig(out string strServerURL,out string strServerName, 
            out int iPort,out string strDBName,out int iRemotingForceMode,out bool bolLoginSecure,
            out string strDBUser, out string strDBPW)
        {
            string strRemotingPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "LBRemoting.Config");
            IniClass iniClass = new IniClass(strRemotingPath);
            string strPort = iniClass.ReadValue("Remoting", "Port");
            int.TryParse(strPort, out iPort);
            strServerURL = iniClass.ReadValue("Remoting", "ServerURL");
            strServerName = iniClass.ReadValue("Remoting", "ServerName");
            strDBName = iniClass.ReadValue("Remoting", "DBName");
            bolLoginSecure = iniClass.ReadValue("Remoting", "LoginSecure")=="1"?true:false;
            strDBUser = iniClass.ReadValue("Remoting", "DBUser");
            strDBPW = iniClass.ReadValue("Remoting", "DBPW");
            string strRemotingForceMode = iniClass.ReadValue("Remoting", "RemotingForceMode");
            int.TryParse(strRemotingForceMode, out iRemotingForceMode);
        }
    }
}
