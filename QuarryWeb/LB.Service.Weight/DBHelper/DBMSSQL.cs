using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Web.DBHelper
{
    public class DBMSSQL : IDBBase
    {
        DbCommand IDBBase.CreateCommand()
        {
            if (!bIsReadConfig)
            {
                InitSettings();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = CommandTimeout;
            return cmd;
        }

        //DbConnection IDBBase.CreateConnection( string dbName )
        //{

        //    string strConnectionString = BulidConnectString( dbName );
        //    return new SqlConnection( strConnectionString );
        //}

        DbConnection IDBBase.CreateConnection()
        {
            if (!bIsReadConfig)
            {
                InitSettings();
            }
            return new SqlConnection(MC_ConnectionString);
        }

        DbDataAdapter IDBBase.CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        //DbParameter IDBBase.CreateParameter( TSDbParameter dbParm )
        //{
        //    int iScale = TS.Data.Contants.DbType.GetSqlDbTypeScale( dbParm.DBTypeName );
        //    if( iScale < 0 )
        //    {
        //        iScale = 0;
        //    }

        //    return new SqlParameter( 
        //        "@" + dbParm.ParameterName, 
        //        TS.Data.Contants.DbType.GetSqlDbType( dbParm.DBTypeName ),
        //        TS.Data.Contants.DbType.GetSqlDbTypeSize( dbParm.DBTypeName ),
        //        dbParm.Direction,
        //        true,
        //        (byte)TS.Data.Contants.DbType.GetSqlDbTypePrecision( dbParm.DBTypeName ),
        //        (byte)iScale,
        //        "", 
        //        DataRowVersion.Current,
        //        dbParm.Value == null ? DBNull.Value : dbParm.Value );
        //}

        DbParameter IDBBase.CreateParameter(string parameterName, SqlDbType DbType, int iDBTypeSize, ParameterDirection direction, int iScale, int SqlDbTypePrecision, object objvalue)
        {
            if (iScale < 0)
            {
                iScale = 0;
            }

            return new SqlParameter(
                "@" + parameterName,
                DbType,
                iDBTypeSize,
                direction,
                true,
                (byte)SqlDbTypePrecision,
                (byte)iScale,
                "",
                DataRowVersion.Current,
                objvalue == null ? DBNull.Value : objvalue);
        }

        private Dictionary<string, bool> mdictSnapshot = new Dictionary<string, bool>();
        bool IDBBase.SnapshotSupported(string dbName)
        {
            string strKey = Server.ToLower() + ";" + dbName.ToLower();
            if (mdictSnapshot.ContainsKey(strKey))
            {
                return mdictSnapshot[strKey];
            }
            else
            {
                bool bStatus = false;
                string strSQL = @"if exists(select 1 from sysobjects where name = 'sysdatabases' and xtype = 'V' )
begin
	if exists( select 1 from sys.databases where name = '{0}' and snapshot_isolation_state_desc = 'ON' )
		select 1 as SnapshotStatus
	else
		select 0 as SnapshotStatus
end
else
	select 0 as SnapshotStatus";

                strSQL = string.Format(strSQL, dbName);

                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, BulidConnectString_Master()))
                {
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            bStatus = Convert.ToBoolean(Convert.ToInt32(dt.Rows[0]["SnapshotStatus"]));
                        }
                    }
                }

                mdictSnapshot.Add(strKey, bStatus);
                return bStatus;
            }
        }

        #region -- InitSettings --

        internal const string MC_strConnection = "server={0};database={1};Integrated Security=True;";
        internal const string MC_strConnectionByUser = "server={0};database={1};User ID={2};Password={3};";
        static string MC_ConnectionString = "";
        private static bool bIsReadConfig = false;

        #region -- 配置值 --

        private static int miCommandTimeout;
        private static string mstrUserID = "";
        private static string mstrPassword = "";
        private static string mstrServer = "";
        private static string mstrCtrlDBName = "";
        private static bool mbLoginSecure = false;

        private static int CommandTimeout
        {
            get
            {
                return miCommandTimeout;
            }
        }

        private static string UserID
        {
            get
            {
                return mstrUserID;
            }
        }

        private static string Password
        {
            get
            {
                return mstrPassword;
            }
        }

        private static string Server
        {
            get
            {
                return mstrServer;
            }
        }

        private static string CtrlDBName
        {
            get
            {
                return mstrCtrlDBName;
            }
        }

        private static bool LoginSecure
        {
            get
            {
                return mbLoginSecure;
            }
        }

        internal static void InitSettings(
            int commandTimeout, string server, string strCtrlDBName, bool bLoginSecure, string strUser, string strPassword)
        {
            miCommandTimeout = commandTimeout;
            mstrServer = server;
            mbLoginSecure = bLoginSecure;
            mstrUserID = strUser;
            mstrPassword = strPassword;
            mstrCtrlDBName = strCtrlDBName;
        }

        private static void InitSettings()
        {
            if (!bIsReadConfig)
            {
                try
                {
                    MC_ConnectionString = ConfigurationManager.ConnectionStrings["RegisterConnectionString"].ConnectionString;

                    string[] arry = MC_ConnectionString.Split(';');

                    foreach (string stritem in arry)
                    {
                        if (stritem.Contains("="))
                        {
                            string strValue = stritem.Substring(stritem.IndexOf("=") + 1).ToString();
                            string strKey = stritem.ToLower().Substring(0, stritem.IndexOf("=")).ToString();
                            switch (strKey)
                            {
                                case "server":
                                    mstrServer = strValue;
                                    break;
                                case "database":
                                    mstrCtrlDBName = strValue;
                                    break;
                                case "user id":
                                case "uid":
                                    mstrUserID = strValue;
                                    break;
                                case "Password":
                                case "pwd":
                                    mstrPassword = strValue;
                                    break;
                                case "Integrated Security":
                                case "integrated security":
                                    mbLoginSecure = strValue.ToLower() == "true" ? true : false;
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    throw new Exception("系统缺少连接信息配置<MyConnectionWeb>，或配置有误。");
                }
                bIsReadConfig = true;
            }
        }
        #endregion -- 配置值 --

        private static string GetConnectString(string server, string dbName)
        {
            string strResult = "";
            if (LoginSecure)
            {
                strResult = string.Format(MC_strConnection, server, dbName);
            }
            else
            {
                strResult = string.Format(MC_strConnectionByUser, server, dbName, UserID, Password);
            }

            return strResult;
        }

        public static string BulidConnectString_Control()
        {
            return GetConnectString(Server, CtrlDBName);
        }

        public static string BulidConnectString_Master()
        {
            return GetConnectString(Server, "master");
        }

        public static string BulidConnectString(string DataBaseName)
        {
            return GetConnectString(Server, DataBaseName);
        }

        #endregion -- InitSettings --
    }
}
