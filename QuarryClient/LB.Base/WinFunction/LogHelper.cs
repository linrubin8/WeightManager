using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LB.WinFunction
{
    public class LogHelper
    {
        public static void WriteLog( string strLog)
        {
            //string strLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogServer.log");
            //FileStream fs = new FileStream(strLogPath, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs);
            ////开始写入
            //sw.WriteLine(DateTime.Now.ToString("yymmdd HH:mm:ss")+"---"+ strLog);
            ////清空缓冲区
            //sw.Flush();
            ////关闭流
            //sw.Close();
            //fs.Close();
            EventLog log = new EventLog();
            try
            {
                log.Source = "我的应用程序";
                log.WriteEntry(strLog, EventLogEntryType.Information);
                //throw new System.IO.FileNotFoundException("readme.txt文件未找到");
            }
            catch (System.IO.FileNotFoundException exception)
            {
                log.WriteEntry("处理信息2", EventLogEntryType.Error);
            }
        }
    }
}
