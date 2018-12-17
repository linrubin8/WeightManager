using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALSysSession
    {
        public void Insert(FactoryArgs args, out t_BigID SessionID, 
            t_String ClientIP, t_String ClientSerial)
        {
            SessionID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", SessionID,true));
            parms.Add(new LBDbParameter("LoginTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("LoginName", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ClientIP", ClientIP));
            parms.Add(new LBDbParameter("ClientSerial", ClientSerial));
            parms.Add(new LBDbParameter("LastCheckTime", new t_DTSmall(DateTime.Now)));
            string strSQL = @"
insert dbo.SysSession( LoginTime,LoginName, ClientIP, ClientSerial, LastCheckTime)
values(@LoginTime, @LoginName, @ClientIP, @ClientSerial, @LastCheckTime)

set @SessionID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            SessionID.SetValueWithObject(parms["SessionID"].Value);
        }

        public void Delete(FactoryArgs args, t_BigID SessionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", SessionID));
            string strSQL = @"
                delete dbo.SysSession
                where SessionID = @SessionID";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UpdateLastCheckTime(FactoryArgs args, t_BigID SessionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SessionID", SessionID));
            parms.Add(new LBDbParameter("LastCheckTime", new t_DTSmall(DateTime.Now)));
            string strSQL = @"
                update dbo.SysSession
                set LastCheckTime = @LastCheckTime
                where SessionID = @SessionID";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetAllSysSession(FactoryArgs args)
        {
            string strSQL = "select * from SysSession";
            return DBHelper.ExecuteQueryUnCommitted(args, strSQL);
        }
    }
}
