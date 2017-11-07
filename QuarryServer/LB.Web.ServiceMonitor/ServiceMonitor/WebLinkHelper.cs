using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.ServiceMonitor
{
    internal class WebLinkHelper
    {
        private static string mstrWebConnectionString = string.Empty;
        public static string WebConnectionString
        {
            get
            {
                return WebLinkHelper.mstrWebConnectionString;
            }
        }

        private static string mstrT3ConnectionString = string.Empty;
        public static string T3ConnectionString
        {
            get
            {
                return WebLinkHelper.mstrT3ConnectionString;
            }
        }

        private static string mstrT3LoginName = string.Empty;
        public static string T3LoginName
        {
            get
            {
                return WebLinkHelper.mstrT3LoginName;
            }
        }

        private static string mstrWebServiceURL = string.Empty;
        public static string WebServiceURL
        {
            get
            {
                return WebLinkHelper.mstrWebServiceURL;
            }
        }

        private static int miSynchronType = 0;
        public static int SynchronType
        {
            get
            {
                return WebLinkHelper.miSynchronType;
            }
        }

        private static int miSaveInfo = 0;
        public static int SaveInfo
        {
            get
            {
                return WebLinkHelper.miSaveInfo;
            }
        }

        //private static string mstrConfigRest = string.Empty;

        //private static string MC_strConnectionByUser = "server={0};database={1};User ID={2};Password={3};TimeOut=30;";

        public static void GetConfig()
        {
            try
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = strPath.Substring( 0, strPath.LastIndexOf( @"\" ) );   // 去掉文件名，得到目录
                strPath += ( strPath.EndsWith( @"\ServerPlg\" ) ? "" : @"\ServerPlg\" ) + "WebLink.ini";

                if( File.Exists( strPath ) )
                {
                    string[] readText = File.ReadAllLines( strPath, Encoding.GetEncoding( "GB2312" ) );
                    mstrWebConnectionString = readText[0];
                    mstrT3ConnectionString = readText[1];
                    mstrT3LoginName = readText[2];
                    mstrWebServiceURL = readText[3];// + "/WebInterFace.asmx";

                    if( readText.Length >= 5 && readText[4].ToString().TrimEnd() != string.Empty )
                    {
                        int.TryParse( readText[4], out miSynchronType );
                    }

                    if( readText.Length >= 6 && readText[5].ToString().TrimEnd() != string.Empty )
                    {
                        int.TryParse( readText[5], out miSaveInfo );
                    }

                    //StringBuilder sb = new StringBuilder();
                    //for( int i = 5; i < readText.Length; i++ )
                    //{
                    //    sb.AppendLine( readText[i] );
                    //}
                    //mstrConfigRest = sb.ToString();
                }
            }
            catch( Exception ex )
            {
                MonitorHelper.ShowNormalMessage( "读取连接数据库的文件出错：" + ex.Message );
            }
        }

        public static void GetConnectionMsg( string strConnection, TextBox txtServer, TextBox txtDataBase,
            TextBox txtUserID, TextBox txtPassword )
        {
            string strServer = string.Empty;
            string strDataBase = string.Empty;
            string strUserID = string.Empty;
            string strPassword = string.Empty;

            if( string.IsNullOrWhiteSpace( strConnection ) )
                return;

            string[] strs = strConnection.Split( new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries );
            foreach( string str in strs )
            {
                string strTemp = str.ToLower();
                if( strTemp.StartsWith( "server=" ) && strTemp.Length > 7 )
                    strServer = str.Substring( 7 ).Trim();
                else if( strTemp.StartsWith( "database=" ) && strTemp.Length > 9 )
                    strDataBase = str.Substring( 9 ).Trim();
                else if( strTemp.StartsWith( "user id=" ) && strTemp.Length > 8 )
                    strUserID = str.Substring( 8 ).Trim();
                else if( strTemp.StartsWith( "password=" ) )
                    strPassword = str.Substring( 9 ).Trim();
            }

            txtServer.Text = strServer;
            txtDataBase.Text = strDataBase;
            txtUserID.Text = strUserID;
            txtPassword.Text = strPassword;
        }

        public static void SaveConfig( List<string> OrderConfigLists )
        {
            try
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = strPath.Substring( 0, strPath.LastIndexOf( @"\" ) );   // 去掉文件名，得到目录
                strPath += ( strPath.EndsWith( @"\ServerPlg\" ) ? "" : @"\ServerPlg\" ) + "WebLink.ini";

                //创建名称为WebLink.ini文件
                using( FileStream fs = new FileStream( strPath, FileMode.Create ) )
                {
                    //fs.SetLength(0);
                    using( StreamWriter sw = new StreamWriter( fs, Encoding.GetEncoding( "GB2312" ) ) )
                    {
                        for( int i = 0, j = OrderConfigLists.Count; i < j; i++ )
                        {
                            sw.Write( OrderConfigLists[i].ToString() );
                        }
                        sw.Close();
                    }
                    fs.Close();
                }
            }
            catch( Exception ex )
            {
                MonitorHelper.ShowNormalMessage( "写入连接数据库的文件出错：" + ex.Message );
            }
        }
    }

}
