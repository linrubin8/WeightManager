using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace LB.Web.ServiceTool.WinService
{
    class Win32_Service
    {
        [DllImport( "advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        public static extern IntPtr OpenSCManager( string machineName, string databaseName, int access );

        [DllImport( "advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        public static extern IntPtr CreateService( IntPtr databaseHandle, string serviceName, string displayName, int access, int serviceType, int startType, int errorControl, string binaryPath, string loadOrderGroup, IntPtr pTagId, string dependencies, string servicesStartName, string password );

        [ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success ), DllImport( "advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        public static extern bool CloseServiceHandle( IntPtr handle );

        [DllImport( "advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        public static extern IntPtr OpenService( IntPtr databaseHandle, string serviceName, int access );

        [DllImport( "advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        public static extern bool DeleteService( IntPtr serviceHandle );
    }
}
