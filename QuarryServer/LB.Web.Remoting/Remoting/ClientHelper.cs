using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace LB.Web.Remoting
{
    public class ClientHelper
    {
        /// <summary>
        /// 获取本地程序文件信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLocalFile()
        {
            DataTable dtLocal = new DataTable("DLL");
            dtLocal.Columns.Add("FileName", typeof(string));
            dtLocal.Columns.Add("FileTimeStr", typeof(string));
            dtLocal.Columns.Add("FileTime", typeof(DateTime));
            dtLocal.Columns.Add("FileSize", typeof(long));
            
            string strStartUp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Client");
            GetChildFile(strStartUp, ref dtLocal);
            return dtLocal;
        }

        private static void GetChildFile(string strPath, ref DataTable dtFile)
        {
            if (Directory.Exists(strPath))
            {
                FileInfo[] files = new DirectoryInfo(strPath).GetFiles();
                foreach (FileInfo fi in files)
                {
                    if (fi.Name.Contains("LB.SmartClient"))
                    {
                        continue;
                    }

                    if (fi.Name.Contains("ServerConfig.ini"))
                    {
                        continue;
                    }
                    string strStartUp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Client");

                    string strFileName = fi.FullName.Replace(strStartUp, "");
                    if (strFileName.StartsWith("\\"))
                    {
                        strFileName = strFileName.Substring(1, strFileName.Length - 1);
                    }
                    DataRow dr = dtFile.NewRow();
                    dr["FileName"] = strFileName;
                    dr["FileTimeStr"] = fi.LastWriteTime.ToString("yyMMddHHmmss");
                    dr["FileTime"] = fi.LastWriteTime;
                    dr["FileSize"] = fi.Length;
                    dtFile.Rows.Add(dr);
                }

                DirectoryInfo[] directInfos = new DirectoryInfo(strPath).GetDirectories();
                foreach (DirectoryInfo directory in directInfos)
                {
                    if (directory.Name.Equals("ReportFile"))//无需更新报表文件
                    {
                        continue;
                    }
                    GetChildFile(directory.FullName, ref dtFile);
                }
            }
        }
    }
}