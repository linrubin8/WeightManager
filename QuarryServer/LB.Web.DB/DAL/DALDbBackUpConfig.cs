using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbBackUpConfig
    {
        public void Insert(FactoryArgs args,
           out t_BigID BackUpConfigID, t_ID BackUpType, t_ID BackUpWeek, t_ID BackUpHour, t_ID BackUpMinu,t_Bool IsEffect, 
           t_ID BackUpFileMaxNum, t_String BackUpPath, t_String BackUpName)
        {
            BackUpConfigID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("BackUpConfigID", BackUpConfigID, true));
            parms.Add(new LBDbParameter("BackUpType", BackUpType));
            parms.Add(new LBDbParameter("BackUpWeek", BackUpWeek));
            parms.Add(new LBDbParameter("BackUpHour", BackUpHour));
            parms.Add(new LBDbParameter("BackUpMinu", BackUpMinu));
            parms.Add(new LBDbParameter("IsEffect", IsEffect));
            parms.Add(new LBDbParameter("BackUpFileMaxNum", BackUpFileMaxNum));
            parms.Add(new LBDbParameter("BackUpPath", BackUpPath));
            parms.Add(new LBDbParameter("BackUpName", BackUpName));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));

            string strSQL = @"
insert into dbo.DbBackUpConfig( BackUpType, BackUpWeek, BackUpHour, BackUpMinu, IsEffect, 
BackUpFileMaxNum, ChangedBy, ChangedTime, BackUpPath, BackUpName)
values( @BackUpType, @BackUpWeek, @BackUpHour, @BackUpMinu, @IsEffect, 
@BackUpFileMaxNum, @ChangedBy, getdate(), @BackUpPath, @BackUpName)

set @BackUpConfigID = @@identity

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            BackUpConfigID.SetValueWithObject(parms["BackUpConfigID"].Value);
        }

        public void Update(FactoryArgs args,
           t_BigID BackUpConfigID, t_ID BackUpType, t_ID BackUpWeek, t_ID BackUpHour, t_ID BackUpMinu, t_Bool IsEffect,
           t_ID BackUpFileMaxNum, t_String BackUpPath, t_String BackUpName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("BackUpConfigID", BackUpConfigID));
            parms.Add(new LBDbParameter("BackUpType", BackUpType));
            parms.Add(new LBDbParameter("BackUpWeek", BackUpWeek));
            parms.Add(new LBDbParameter("BackUpHour", BackUpHour));
            parms.Add(new LBDbParameter("BackUpMinu", BackUpMinu));
            parms.Add(new LBDbParameter("IsEffect", IsEffect));
            parms.Add(new LBDbParameter("BackUpFileMaxNum", BackUpFileMaxNum));
            parms.Add(new LBDbParameter("BackUpPath", BackUpPath));
            parms.Add(new LBDbParameter("BackUpName", BackUpName));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));

            string strSQL = @"
update dbo.DbBackUpConfig
set BackUpType=@BackUpType,
    BackUpWeek=@BackUpWeek,
    BackUpHour=@BackUpHour,
    BackUpMinu=@BackUpMinu,
    IsEffect=@IsEffect,
    BackUpFileMaxNum=@BackUpFileMaxNum,
    ChangedBy=@ChangedBy,
    ChangedTime=getdate(),
    BackUpPath=@BackUpPath,
    BackUpName=@BackUpName
where BackUpConfigID = @BackUpConfigID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args,
           t_BigID BackUpConfigID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("BackUpConfigID", BackUpConfigID));

            string strSQL = @"
delete dbo.DbBackUpConfig
where BackUpConfigID = @BackUpConfigID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
