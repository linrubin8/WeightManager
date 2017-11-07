using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbSysConfig
    {
        public void Update(FactoryArgs args, t_String SysConfigFieldName, t_String SysConfigValue)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("LoginName",new t_String( args.LoginName)));
            parms.Add(new LBDbParameter("ChangedTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("SysConfigFieldName", SysConfigFieldName));
            parms.Add(new LBDbParameter("SysConfigValue", SysConfigValue));
            string strSQL = @"
if not exists(select 1 from DbSysConfigValue where rtrim(SysConfigFieldName)=rtrim(@SysConfigFieldName))
begin
    insert dbo.DbSysConfigValue( SysConfigFieldName,SysConfigValue)
    values(@SysConfigFieldName,@SysConfigValue)
end
else
begin
    update dbo.DbSysConfigValue
    set SysConfigValue = @SysConfigValue
    where SysConfigFieldName = @SysConfigFieldName
end
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetDbSysConfigField(FactoryArgs args, t_String SysConfigFieldName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysConfigFieldName", SysConfigFieldName));

            string strSQL = @"
select *
from dbo.DbSysConfigField
where SysConfigFieldName = @SysConfigFieldName
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable GetDbSysConfigValue(FactoryArgs args, t_String SysConfigFieldName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysConfigFieldName", SysConfigFieldName));

            string strSQL = @"
select *
from dbo.DbSysConfigValue
where SysConfigFieldName = @SysConfigFieldName
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
