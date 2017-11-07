using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbInfraredDeviceConfig
    {
        public void InsertInfraredDeviceConfig(FactoryArgs args,
           t_String MachineName, t_String SerialName, t_ID HeaderXType, t_ID TailXType,
           t_ID HeaderYType, t_ID TailYType, t_Bool IsHeaderEffect, t_Bool IsTailEffect)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("SerialName", SerialName));
            parms.Add(new LBDbParameter("HeaderXType", HeaderXType));
            parms.Add(new LBDbParameter("TailXType", TailXType));
            parms.Add(new LBDbParameter("HeaderYType", HeaderYType));
            parms.Add(new LBDbParameter("TailYType", TailYType));
            parms.Add(new LBDbParameter("IsHeaderEffect", IsHeaderEffect));
            parms.Add(new LBDbParameter("IsTailEffect", IsTailEffect));

            string strSQL = @"
insert into dbo.DbInfraredDeviceConfig( MachineName,SerialName,
    HeaderXType,TailXType,HeaderYType,TailYType,IsHeaderEffect,IsTailEffect )
values( @MachineName,@SerialName,
    @HeaderXType,@TailXType,@HeaderXType,@TailXType,@IsHeaderEffect,@IsTailEffect)


";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UpdateInfraredDeviceConfig(FactoryArgs args,
           t_String MachineName, t_String SerialName, t_ID HeaderXType, t_ID TailXType,
           t_ID HeaderYType, t_ID TailYType, t_Bool IsHeaderEffect, t_Bool IsTailEffect)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("SerialName", SerialName));
            parms.Add(new LBDbParameter("HeaderXType", HeaderXType));
            parms.Add(new LBDbParameter("TailXType", TailXType));
            parms.Add(new LBDbParameter("HeaderYType", HeaderYType));
            parms.Add(new LBDbParameter("TailYType", TailYType));
            parms.Add(new LBDbParameter("IsHeaderEffect", IsHeaderEffect));
            parms.Add(new LBDbParameter("IsTailEffect", IsTailEffect));

            string strSQL = @"
update dbo.DbInfraredDeviceConfig
set SerialName = @SerialName,
    HeaderXType = @HeaderXType,
    TailXType = @TailXType,
    HeaderYType = @HeaderYType,
    TailYType = @TailYType,
    IsHeaderEffect = @IsHeaderEffect,
    IsTailEffect = @IsTailEffect
where MachineName = @MachineName
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetInfraredConfig(FactoryArgs args, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));

            string strSQL = @"
select *
from dbo.DbInfraredDeviceConfig
where MachineName = @MachineName
";
           return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
        
    }
}
