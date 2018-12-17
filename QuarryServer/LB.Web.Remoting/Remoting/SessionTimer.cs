using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.Remoting
{
    public class SessionTimer
    {
        private static Timer mTimer = null;

        public static void StartListenSession()
        {
            if (mTimer == null)
            {
                mTimer = new Timer();
                mTimer.Interval = 1000 ;//每五分钟监听一次是否处于连接状态
                mTimer.Tick += MTimer_Tick;
                mTimer.Enabled = true;
            }
            if (!mTimer.Enabled)
            {
                mTimer.Enabled = true;
            }
        }

        public static void StopListenSession()
        {
            if (mTimer != null)
            {
                mTimer.Enabled = false;
            }
        }

        private static void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                FactoryArgs args = new FactoryArgs(DBHelper.DBName, "系统", 0, false, null, null);
                DBHelper.Provider = new DBMSSQL();
                SqlConnection con = new SqlConnection(SQLServerDAL.GetConnectionString);
                string strDBName = con.Database;
                DBMSSQL.InitSettings(5000, con.DataSource, strDBName, true, "", "");
                con.Close();
                string strSQL = @"
                    delete dbo.SysSession
                    where DATEDIFF(minute,LastCheckTime,getdate())>=10
";
                DBHelper.ExecuteNonQuery(args, CommandType.Text, strSQL, new LBDbParameterCollection(), false);
            }
            catch(Exception ex)
            {

            }
        }

        #region -- 校验Session -- 

        public static void TakeSession(long SessionID)
        {
            FactoryArgs args = new FactoryArgs(DBHelper.DBName, "系统", 0, false, null, null);
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", new t_BigID(SessionID)));
            string strSQL = @"
                    update dbo.SysSession
                    set LastCheckTime = getdate()
                    where SessionID = @SessionID
";
            DBHelper.ExecuteNonQuery(args, CommandType.Text, strSQL, parms, false);
        }

        public static void LogOutSession(long SessionID)
        {
            FactoryArgs args = new FactoryArgs(DBHelper.DBName, "系统", 0, false, null, null);
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", new t_BigID(SessionID)));

            string strSQL = @"
                    delete dbo.SysSession
                    where SessionID = @SessionID
";
            DBHelper.ExecuteNonQuery(args, CommandType.Text, strSQL, parms, false);
        }

        public static bool VerifySession(FactoryArgs args)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", new t_BigID(args.SessionID)));
            string strSQL = @"
select 1
from SysSession
where SessionID = @SessionID
";
            DataTable dtSession = DBHelper.ExecuteQuery(args, strSQL, parms);
            if (dtSession.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
