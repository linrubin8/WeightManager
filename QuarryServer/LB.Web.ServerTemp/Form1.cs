using LB.Web.Remoting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.ServerTemp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
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
            int mPort;
            int.TryParse(strPort, out mPort);
            strServerName = "LRB";
            HttpChannel channel = new HttpChannel(mPort);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
             typeof(WebRemoting), strServerName, WellKnownObjectMode.Singleton);

            WebRemoting.SetRemotingInfo(strDBName, strDBServer, bolLoginSecure, strDBUser, strDBPW);
            WebRemoting.LoadAllBLLFunction();
        }

        private void StartServer()
        {
            int mPort;
            //string strRemotingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBRemoting.Config");
            //IniClass iniClass = new IniClass(strRemotingPath);
            //string strPort = iniClass.ReadValue("Remoting", "Port");
            //string strAddress = iniClass.ReadValue("Remoting", "ServerName");
            //string strDBName = iniClass.ReadValue("Remoting", "DBName");

            //int.TryParse(strPort, out mPort);

            //ChannelServices.RegisterChannel(new HttpChannel(2017), false);
            ////ChannelServices.RegisterChannel(new TcpServerChannel(mPort), false);
            //RemotingConfiguration.RegisterWellKnownServiceType(
            //typeof(LB.Web.Remoting.WebRemoting), "LBProject", WellKnownObjectMode.Singleton);
            
            //WebRemoting.RemotingSendedEvent += WebRemoting_RemotingSendedEvent;


            //HttpChannel channel = new HttpChannel(2017);
            //ChannelServices.RegisterChannel(channel, false);
            //RemotingConfiguration.RegisterWellKnownServiceType(
            // typeof(WebRemoting), "LBProject", WellKnownObjectMode.Singleton);
            //WebRemoting.FaxSendedEvent += WebRemoting_FaxSendedEvent;
            //WebRemoting.RemotingSendedEvent += WebRemoting_RemotingSendedEvent;
            //WebRemoting.SetRemotingInfo(strDBName, strAddress);
            //WebRemoting.LoadAllBLLFunction();
        }

        private void MyFaxBusiness_FaxSendedEvent(string fax)
        {
            this.richTextBox1.BeginInvoke(new MethodInvoker(delegate
            {
                this.richTextBox1.Text += fax;
            }));

        }

        private void WebRemoting_RemotingSendedEvent(string msg)
        {
            this.richTextBox1.BeginInvoke(new MethodInvoker(delegate
            {
                this.richTextBox1.Text += msg;
            }));
        }
    }
}
