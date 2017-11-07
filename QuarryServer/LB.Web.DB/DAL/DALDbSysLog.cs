using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbSysLog
    {
        public void Insert(FactoryArgs args, t_String LoginName,
            t_String LogMachineName, t_String LogMachineIP, t_String LogModule, t_String LogStatusName, t_String LogDescription)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("LoginName", LoginName));
            parms.Add(new LBDbParameter("LogTime",new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("LogMachineName", LogMachineName));
            parms.Add(new LBDbParameter("LogMachineIP", LogMachineIP));
            parms.Add(new LBDbParameter("LogModule", LogModule));
            parms.Add(new LBDbParameter("LogStatusName", LogStatusName));
            parms.Add(new LBDbParameter("LogDescription", LogDescription));
            string strSQL = @"
insert dbo.SbSysLog( LoginName,LogTime, LogMachineName, LogMachineIP, LogModule, LogStatusName, LogDescription)
values(@LoginName, @LogTime, @LogMachineName, @LogMachineIP, @LogModule, @LogStatusName, @LogDescription)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_String SysLogIDStr)
        {
            string strSQL = "delete dbo.SbSysLog where SysLogID in ("+ SysLogIDStr.Value+ ")";
            DBHelper.ExecuteQueryUnCommitted(args, strSQL);
        }
    }
}
