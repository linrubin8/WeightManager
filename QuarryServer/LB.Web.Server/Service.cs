using LB.Web.Remoting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceProcess;
using System.Text;

namespace LB.Web.Server
{
    public partial class Service : ServiceBase
    {
        int mPort = 0;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("调用开始OnStart");
            string strRemotingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBRemoting.Config");
            IniClass iniClass = new IniClass(strRemotingPath);
            string strPort = iniClass.ReadValue("Remoting", "Port");
            string strServerName = iniClass.ReadValue("Remoting", "ServerName");
            string strServerURL = iniClass.ReadValue("Remoting", "ServerURL");
            string strDBName = iniClass.ReadValue("Remoting", "DBName");
            string strDBServer = iniClass.ReadValue("Remoting", "DBServer");

            bool bolLoginSecure = iniClass.ReadValue("Remoting", "LoginSecure") == "1" ? true : false;
            string strDBUser = iniClass.ReadValue("Remoting", "DBUser");
            string strDBPW = iniClass.ReadValue("Remoting", "DBPW");
            //WriteLog("读取数据库名称："+ strDBName+" 地址："+ strAddress);
            int.TryParse(strPort,out mPort);
            strServerName = "LRB";
            HttpChannel channel = new HttpChannel(mPort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
             typeof(WebRemoting), strServerName, WellKnownObjectMode.Singleton);

            WebRemoting.SetRemotingInfo(strDBName, strDBServer,bolLoginSecure,strDBUser,strDBPW);
            WebRemoting.LoadAllBLLFunction();
            WriteLog("调用结束OnStart,strServerName="+ strServerName + ",Port=" + mPort.ToString());

        }

        protected override void OnStop()
        {
            ChannelServices.UnregisterChannel(new TcpServerChannel(mPort));
            WebRemoting.StopServer();
        }

        private static void WriteLog(string strLog)
        {
            
            string strLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogServer.log");
            /*if (!File.Exists(strLogPath))
            {
                File.Create(strLogPath);
            }*/
            //StreamWriter sw = File.AppendText(strLogPath);
            //sw.WriteLine(DateTime.Now.ToString("yyMMdd HH:mm:ss") + "----" + strLog);
            //sw.Close();

            /*FileStream fs = new FileStream(strLogPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.WriteLine(DateTime.Now.ToString("yyMMdd HH:mm:ss")+"----"+strLog);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();*/

            EventLog log = new EventLog();
            try
            {
                log.Source = "我的应用程序";
                log.WriteEntry(strLog, EventLogEntryType.Information);
            }
            catch (System.IO.FileNotFoundException exception)
            {
                log.WriteEntry("处理信息2", EventLogEntryType.Error);
            }
        }

    }
}
