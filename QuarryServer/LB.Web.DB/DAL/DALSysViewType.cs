using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.DB.DAL
{
    public class DALSysViewType
    {
        public void Insert(FactoryArgs args, out t_BigID SysViewTypeID, t_String SysViewType, t_String SysViewName)
        {
            SysViewTypeID=new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysViewTypeID",  SysViewTypeID, true));
            parms.Add(new LBDbParameter("SysViewType", SysViewType));
            parms.Add(new LBDbParameter("SysViewName",  SysViewName));

            string strSQL = @"
insert into dbo.SysViewType( SysViewType, SysViewName)
values( @SysViewType, @SysViewName)

set @SysViewTypeID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            SysViewTypeID.SetValueWithObject( Convert.ToInt64(parms["SysViewTypeID"].Value));
        }

        public void Update(FactoryArgs args, t_BigID SysViewTypeID, t_String SysViewType, t_String SysViewName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysViewTypeID", SysViewTypeID));
            parms.Add(new LBDbParameter("SysViewType",  SysViewType));
            parms.Add(new LBDbParameter("SysViewName", SysViewName));

            string strSQL = @"
update dbo.SysViewType
set SysViewType=@SysViewType, 
    SysViewName=@SysViewName
where SysViewTypeID = @SysViewTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID SysViewTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysViewTypeID", SysViewTypeID));

            string strSQL = @"
delete dbo.SysViewType
where SysViewTypeID = @SysViewTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable VerifyExists(FactoryArgs args, t_String SysViewType, t_String SysViewName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SysViewType",  SysViewType));
            parms.Add(new LBDbParameter("SysViewName", SysViewName));
            string strSQL = @"
select *
    from dbo.SysViewType
    where SysViewType=@SysViewType or SysViewName=@SysViewName
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        } 
    }
}