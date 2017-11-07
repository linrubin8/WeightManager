using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.ComponentModel;
using System.Threading;

namespace LB.Web.ServiceTool.WinService
{
    class TSServiceInstaller
    {
        internal static void Install( string ServiceName, string ServicePath )
        {
            string DisplayName = ServiceName;
            string dependencies = null;
            string binaryPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            IntPtr databaseHandle = Win32_Service.OpenSCManager( null, null, 0xf003f );
            IntPtr zero = IntPtr.Zero;
            if( databaseHandle == IntPtr.Zero )
            {
                throw new InvalidOperationException( "执行 OpenSCManager 失败。" );
            }

            try
            {
                // SERVICE_WIN32_OWN_PROCESS = 0x00000010
                // SERVICE_WIN32_SHARE_PROCESS = 0x00000020
                // 如果服务类型是SERVICE_WIN32_OWN_PROCESS就会立即调用 StartServiceCtrlDispatcher函数的执行；
                // 如果服务类型是SERVICE_WIN32_SHARE_PROCESS，通常在初始化所有服务之后再调用它。
                // StartServiceCtrlDispatcher函数的参数就是一个SERVICE_TABLE_ENTRY结构，它包含了进程内所有服务的名称和服务入口点。
                int serviceType = 0x10;
                int startType = (int)ServiceStartMode.Automatic;
                string servicesStartName = null;
                string password = null;

                zero = Win32_Service.CreateService(
                    databaseHandle, ServiceName, DisplayName, 0xf01ff, serviceType, startType,
                    1, ServicePath, null, IntPtr.Zero, dependencies, servicesStartName, password );
                if( zero == IntPtr.Zero )
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                if( zero != IntPtr.Zero )
                {
                    Win32_Service.CloseServiceHandle( zero );
                }
                Win32_Service.CloseServiceHandle( databaseHandle );
            }
        }

        internal static void RemoveService( string ServiceName )
        {
            // 先查找是否还存在该服务
            bool bExists = true;
            try
            {
                using( ServiceController controller = new ServiceController( ServiceName ) )
                {
                    ServiceControllerStatus status = controller.Status;
                }
            }
            catch
            {
                bExists = false;
            }
            if( !bExists )
            {
                return;
            }

            IntPtr databaseHandle = Win32_Service.OpenSCManager( null, null, 0xf003f );
            if( databaseHandle == IntPtr.Zero )
            {
                throw new Win32Exception();
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = Win32_Service.OpenService( databaseHandle, ServiceName, 0x10000 );
                if( zero == IntPtr.Zero )
                {
                    throw new Win32Exception();
                }
                Win32_Service.DeleteService( zero );
            }
            finally
            {
                if( zero != IntPtr.Zero )
                {
                    Win32_Service.CloseServiceHandle( zero );
                }
                Win32_Service.CloseServiceHandle( databaseHandle );
            }

            try
            {
                using( ServiceController controller = new ServiceController( ServiceName ) )
                {
                    if( controller.Status != ServiceControllerStatus.Stopped )
                    {
                        controller.Stop();
                        int num = 10;
                        controller.Refresh();
                        while( ( controller.Status != ServiceControllerStatus.Stopped ) && ( num > 0 ) )
                        {
                            Thread.Sleep( 0x3e8 );
                            controller.Refresh();
                            num--;
                        }
                    }
                }
            }
            catch
            {
            }

            Thread.Sleep( 0x1388 );
        }

        internal static bool ExistsService( string ServiceName )
        {
            bool bExists = true;
            try
            {
                using( ServiceController controller = new ServiceController( ServiceName ) )
                {
                    ServiceControllerStatus status = controller.Status;
                }
            }
            catch
            {
                bExists = false;
            }

            return bExists;
        }

		internal static void Start( string ServiceName )
		{
			using( ServiceController controller = new ServiceController( ServiceName ) )
			{
				controller.Refresh();
				ServiceControllerStatus status = controller.Status;

				if( status == System.ServiceProcess.ServiceControllerStatus.Stopped )
				{
					controller.Start();
				}
			}
		}
	}

}
