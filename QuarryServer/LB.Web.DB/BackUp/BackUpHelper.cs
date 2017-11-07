using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;

namespace LB.Web.DB.BackUp
{
    public class BackUpHelper
    {
        private static bool mbolIsStart = false;
        private static System.Timers.Timer mTimer;
        private static Thread mThread = null;
        static List<BackUpTask> mlstBackTask = new List<BackUpTask>();//备份任务
        private static string mFilePath = "";

        public static void StartBackUp(string strServerPath)
        {
            if (!mbolIsStart)
            {
                mFilePath =Path.Combine( strServerPath,"AutoBackUp");
                mTimer = new System.Timers.Timer();
                mTimer.Elapsed += MTimer_Elapsed;
                mTimer.Interval = 1000*60*30;
                mTimer.Enabled = true;
                mThread = new Thread(ThreadBackUp);
                mThread.Start();

                mbolIsStart = true;
            }
        }

        private static void MTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string strSQL = "select * from dbo.DbBackUpConfig where IsEffect=1";
                DataTable dtBackUpConfig = SQLServerDAL.Query(strSQL);
                //查询当日的备份清单
                strSQL = "select * from dbo.DbBackUpLog where BackUpTime>='"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
                DataTable dtBackUpLog = SQLServerDAL.Query(strSQL);
                DataView dvBackUpLog = new DataView(dtBackUpLog);
                foreach (DataRow dr in dtBackUpConfig.Rows)
                {
                    int iBackUpType = LBConverter.ToInt32(dr["BackUpType"]);
                    int iBackUpWeek = LBConverter.ToInt32(dr["BackUpWeek"]);
                    int iBackUpHour = LBConverter.ToInt32(dr["BackUpHour"]);
                    int iBackUpMinu = LBConverter.ToInt32(dr["BackUpMinu"]);
                    long lBackUpConfigID = LBConverter.ToInt64(dr["BackUpConfigID"]);
                    string strBackUpName = LBConverter.ToString(dr["BackUpName"]);
                    int iBackUpFileMaxNum = LBConverter.ToInt32(dr["BackUpFileMaxNum"]);
                    DateTime now = DateTime.Now;
                    int iWeek = (int)now.DayOfWeek;
                    int iHour = now.Hour;
                    int iMinu = now.Minute;
                    if (iBackUpType == 0)//每周备份一次
                    {
                        if (iWeek == iBackUpWeek)
                        {
                            if(iHour == iBackUpHour)
                            {
                                if(iMinu>= iBackUpMinu&& iMinu<= iBackUpMinu + 29)
                                {
                                    string strBackUpTimeFrom = DateTime.Now.ToString("yyyy-MM-dd ") + iBackUpHour + ":" + iBackUpMinu;
                                    string strBackUpTimeTo = DateTime.Now.ToString("yyyy-MM-dd ") + iBackUpHour + ":" + (iBackUpMinu+29);
                                    dvBackUpLog.RowFilter = "BackUpConfigID="+ lBackUpConfigID+ 
                                        " and BackUpTime>='"+ strBackUpTimeFrom + "' and BackUpTime<='" + strBackUpTimeTo + "'";

                                    if (dvBackUpLog.Count == 0)
                                    {
                                        lock (mlstBackTask)
                                        {
                                            bool bolIsAdd = true;
                                            foreach(BackUpTask t in mlstBackTask)
                                            {
                                                if (t.BackUpConfigID == lBackUpConfigID)
                                                {
                                                    bolIsAdd = false;
                                                    break;
                                                }
                                            }
                                            if (bolIsAdd)
                                            {
                                                BackUpTask task = new BackUp.BackUpTask(lBackUpConfigID, strBackUpName, iBackUpFileMaxNum);
                                                mlstBackTask.Add(task);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //每日备份一次
                        if (iHour == iBackUpHour)
                        {
                            if (iMinu >= iBackUpMinu && iMinu <= iBackUpMinu + 29)
                            {
                                string strBackUpTimeFrom = DateTime.Now.ToString("yyyy-MM-dd ") + iBackUpHour + ":" + iBackUpMinu;
                                string strBackUpTimeTo = DateTime.Now.ToString("yyyy-MM-dd ") + iBackUpHour + ":" + (iBackUpMinu + 29);
                                dvBackUpLog.RowFilter = "BackUpConfigID=" + lBackUpConfigID +
                                    " and BackUpTime>='" + strBackUpTimeFrom + "' and BackUpTime<='" + strBackUpTimeTo + "'";

                                if (dvBackUpLog.Count == 0)
                                {
                                    lock (mlstBackTask)
                                    {
                                        bool bolIsAdd = true;
                                        foreach (BackUpTask t in mlstBackTask)
                                        {
                                            if (t.BackUpConfigID == lBackUpConfigID)
                                            {
                                                bolIsAdd = false;
                                                break;
                                            }
                                        }
                                        if (bolIsAdd)
                                        {
                                            BackUpTask task = new BackUp.BackUpTask(lBackUpConfigID, strBackUpName, iBackUpFileMaxNum);
                                            mlstBackTask.Add(task);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void ThreadBackUp()
        {
            while (true)
            {
                try
                {
                    lock (mlstBackTask)
                    {
                        if (mlstBackTask.Count > 0)//有备份任务，执行备份
                        {
                            foreach (BackUpTask task in mlstBackTask)
                            {
                                BackUpDB(task);

                                string strSQL = @"insert dbo.DbBackUpLog( BackUpConfigID, BackUpTime, BackUpFileName)
                                                  values({0},getdate(),'{1}')";
                                strSQL = string.Format(strSQL, task.BackUpConfigID, task.BackUpFileName);
                                DataTable dtBackUpConfig = SQLServerDAL.Query(strSQL);
                            }
                            mlstBackTask.Clear();
                        }
                    }
                    Thread.Sleep(1000 *60* 5 );
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static void BackUpDB(BackUpTask task)
        {
            if (!Directory.Exists(mFilePath))
            {
                Directory.CreateDirectory(mFilePath);
            }
            string strSQL = @"  select BackUpLogID,BackUpFileName
                                from dbo.DbBackUpLog 
                                where BackUpConfigID={0}
                                order by BackUpLogID asc";
            strSQL = string.Format(strSQL, task.BackUpConfigID, task.BackUpFileName);
            DataTable dtBackUpLog = SQLServerDAL.Query(strSQL);

            if(dtBackUpLog.Rows.Count>= task.BackUpFileMaxNum)//备份数大于最大数量时，先删除最旧的备份帐套
            {
                DataRow dr = dtBackUpLog.Rows[0];
                string strBakName = dr["BackUpFileName"].ToString().TrimEnd();
                long lBackUpLogID = LBConverter.ToInt64(dr["BackUpLogID"]);
                string strBakFileName = Path.Combine(mFilePath, strBakName);
                if (File.Exists(strBakFileName))
                {
                    File.Delete(strBakFileName);
                }
                string strSQLDel = @"delete from dbo.DbBackUpLog where BackUpLogID="+ lBackUpLogID;
                SQLServerDAL.Query(strSQLDel);
            }

            string strFileName = Path.Combine(mFilePath, task.BackUpFileName);
            using (SqlConnection conn = new SqlConnection(SQLServerDAL.GetConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 1000*30*60;
                    cmd.CommandType = System.Data.CommandType.Text;

                    string strSQLBackUp = "backup database [{0}] to disk = '{1}'";
                    strSQLBackUp = string.Format(strSQLBackUp, conn.Database, strFileName);

                    cmd.CommandText = strSQLBackUp;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }

    public class BackUpTask
    {
        private long _BackUpConfigID;
        private string _BackUpName;
        private int _BackUpFileMaxNum;

        public long BackUpConfigID
        {
            get
            {
                return _BackUpConfigID;
            }
        }

        public int BackUpFileMaxNum
        {
            get
            {
                return _BackUpFileMaxNum;
            }
        }

        public string BackUpFileName
        {
            get
            {
                return _BackUpName + "_" + _BackUpConfigID.ToString() + "_" + DateTime.Now.ToString("yyMMddHHmmss")+".bak";
            }
        }

        public BackUpTask(long lBackUpConfigID,string strBackUpName,int iBackUpFileMaxNum)
        {
            this._BackUpConfigID = lBackUpConfigID;
            this._BackUpName = strBackUpName;
            this._BackUpFileMaxNum = iBackUpFileMaxNum;
        }
    }
}
