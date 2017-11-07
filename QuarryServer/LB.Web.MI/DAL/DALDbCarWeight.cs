using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.MI.DAL
{
    public class DALDbCarWeight
    {
        public void Insert(FactoryArgs args, t_BigID CarID, t_Decimal CarWeight, t_String Description)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("CarWeight", CarWeight));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("CreateBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CreateTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbCarWeight(CarID,CarWeight,Description,CreateBy, CreateTime)
values( @CarID, @CarWeight, @Description, @CreateBy, @CreateTime)

set @CarID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
