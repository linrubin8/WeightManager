using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using LB.Web.ServiceTool.WinService;

namespace LB.Web.ServiceTool
{
    public class ServiceHelper
    {
        private string _mstrServiceName = string.Empty;
        public string ServiceName
        {
            get
            {
                return _mstrServiceName;
            }
        }

        private string _mstrServicePath = string.Empty;

        public ServiceHelper( string strServiceName, string strServicePath )
        {
            _mstrServiceName = strServiceName;
            _mstrServicePath = strServicePath;
        }

        public void Install()
        {
            // 安装服务
            TSServiceInstaller.Install( _mstrServiceName, _mstrServicePath );

            // 写注册：自动运行 Monitor
			//WriteRegAutoRun();
        }

        // 写注册表
		//private void WriteRegAutoRun()
		//{
		//	RegistryKey key = Registry.LocalMachine.OpenSubKey( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true );
		//	//string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
		//	//strPath = strPath.Substring( 0, strPath.LastIndexOf( @"\" ) );	// 去掉文件名，得到目录
		//	////TODO:添加监视器
		//	//strPath += ( strPath.EndsWith( @"\" ) ? "" : @"\" ) + "LB.Web.ServiceMonitorDBAutoBackUp.exe"; //这个是监视器

		//	key.SetValue( _mstrServiceName, _mstrServicePath );

		//	// 立即运行
		//	//RunServiceMonitor( strPath );
		//}

        //private void RunServiceMonitor( string strPath )
        //{
        //    ProcessStartInfo startInfo = new ProcessStartInfo( strPath );

        //    Process process = new Process();
        //    process.StartInfo = startInfo;
        //    process.Start();
        //}

        public void Uninstall()
        {
            // 删除服务
            TSServiceInstaller.RemoveService( _mstrServiceName );

            // 删除注册表信息
			//DelRegAutoRun();
        }

        // 删除注册表信息
		//private void DelRegAutoRun()
		//{
		//	RegistryKey key = Registry.LocalMachine.OpenSubKey( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true );

		//	key.DeleteValue( _mstrServiceName );
		//}

        public bool ExistsService()
        {
            return TSServiceInstaller.ExistsService( _mstrServiceName );
        }

		public void StartService()
		{
			TSServiceInstaller.Start( _mstrServiceName );
		}
	}
}
