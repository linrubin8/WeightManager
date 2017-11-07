using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbWeightType
    {
        public void Insert(FactoryArgs args,
           t_ID WeightType, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightType", WeightType));
            parms.Add(new LBDbParameter("MachineName", MachineName));

            string strSQL = @"
insert into dbo.DbWeightType( WeightType,MachineName)
values( @WeightType, @MachineName)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Update(FactoryArgs args,t_BigID WeightTypeID,
          t_ID WeightType, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightTypeID", WeightTypeID));
            parms.Add(new LBDbParameter("WeightType", WeightType));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            
            string strSQL = @"
update dbo.DbWeightType
set WeightType=@WeightType,   
    MachineName=@MachineName
where WeightTypeID = @WeightTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
        
        public DataTable VerifyExists(FactoryArgs args, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));

            string strSQL = @"
                        select * 
                        from dbo.DbWeightType
                        where MachineName=@MachineName";
            return DBHelper.ExecuteQuery(args, strSQL,parms);
        }
    }
}
