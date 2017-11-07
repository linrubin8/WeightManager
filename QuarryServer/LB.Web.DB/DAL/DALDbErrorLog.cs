using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbErrorLog
    {
        public void Insert(FactoryArgs args, t_String ErrorLogMsg)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ErrorLogMsg", ErrorLogMsg));
            parms.Add(new LBDbParameter("CreateBy",new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CreateTime", new t_DTSmall(DateTime.Now)));
            string strSQL = @"
insert dbo.SbErrorLog(ErrorLogMsg,CreateBy,CreateTime)
values(@ErrorLogMsg,@CreateBy,@CreateTime)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
