using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.MI.DAL
{
    public class DALDBUOM
    {
        public void Insert(FactoryArgs args, out t_BigID UOMID, t_String UOMName, t_Byte UOMType)
        {
            UOMID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UOMID",  UOMID,true ));
            parms.Add(new LBDbParameter("UOMName",  UOMName));
            parms.Add(new LBDbParameter("UOMType",  UOMType));

            string strSQL = @"
insert into dbo.DbUOM( UOMName, UOMType)
values( @UOMName, @UOMType)

set @UOMID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            UOMID.Value = Convert.ToInt64(parms["UOMID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID UOMID, t_String UOMName, t_Byte UOMType)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UOMID", UOMID));
            parms.Add(new LBDbParameter("UOMName", UOMName));
            parms.Add(new LBDbParameter("UOMType", UOMType));

            string strSQL = @"
update dbo.DbUOM
set UOMName=@UOMName, 
    UOMType=@UOMType
where UOMID = @UOMID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID UOMID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UOMID", UOMID));

            string strSQL = @"
delete dbo.DbUOM
where UOMID = @UOMID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}