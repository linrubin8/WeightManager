using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Base.Helper
{
	public class SettingValue
	{
		private static bool mbDisplayMoreInfo = false;	// 显示更多错误信息
		private static bool mbPageConfig = true;
		private static bool mbPageConfigCache = true;
		private static bool mbExecInDB = false;
		private static bool mbCheckLocker = true;

		public static bool DisplayMoreInfo
		{
			get
			{
				return mbDisplayMoreInfo;
			}
		}

		public static bool PageConfig
		{
			get
			{
				return mbPageConfig;
			}
		}

		public static bool PageConfigCache
		{
			get
			{
				return mbPageConfigCache;
			}
		}

		public static bool ExecInDB
		{
			get
			{
				return mbExecInDB;
			}
		}

		public static bool CheckLocker
		{
			get
			{
				return mbCheckLocker;
			}
		}

		public static void Init( 
			int commandTimeout, string server, string strCtrlDBName, bool bLoginSecure, string strUser, string strPassword,
			bool displayMoreInfo, bool pageConfig, bool pageConfigCache, bool execInDB, bool bCheckLocker )
		{
			mbDisplayMoreInfo = displayMoreInfo;
			mbPageConfig = pageConfig;
			mbPageConfigCache = pageConfigCache;
			mbExecInDB = execInDB;
			mbCheckLocker = bCheckLocker;

			DBMSSQL.InitSettings( commandTimeout, server, strCtrlDBName, bLoginSecure, strUser, strPassword );
		}
	}
}
