using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.RMT.Base
{
    public class ConfigHelper
    {
        public static string ServiceName;
        public static string ServiceURL;
        public static int Port;
        public static string DBName;
        public static int RemotingForceMode;
        public static string RMTChannel;
        public static bool LoginSecure;
        public static string DBUser;
        public static string DBPW;

        public static void ReadIniConfig()
        {
            RemotingHelper.InitRemotingConfig();
            RemotingHelper.ReadRemotingConfig(out ServiceURL, out ServiceName, out Port, out DBName, out RemotingForceMode,
                out LoginSecure,out DBUser,out DBPW);
        }

        public static void WriteIniConfig()
        {
            RemotingHelper.WriteRemotingConfig(ServiceURL, ServiceName, Port.ToString(), DBName, RemotingForceMode.ToString(),
                LoginSecure,DBUser,DBPW);
        }
    }
}
