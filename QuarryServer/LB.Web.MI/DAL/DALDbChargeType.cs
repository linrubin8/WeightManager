using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.MI.DAL
{
    public class DALDbChargeType
    {
        public void ChargeType_Insert(FactoryArgs args, out t_BigID ChargeTypeID, t_String ChargeTypeCode, t_String ChargeTypeName)
        {
            t_String ChangeBy = new t_String();
            t_DTSmall ChangeTime = new t_DTSmall();
            ChargeTypeID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID, true));
            parms.Add(new LBDbParameter("ChargeTypeCode", ChargeTypeCode));
            parms.Add(new LBDbParameter("ChargeTypeName", ChargeTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbChargeType( ChargeTypeCode,ChargeTypeName,ChangeBy,ChangeTime )
values(@ChargeTypeCode,  @ChargeTypeName, @ChangeBy, @ChangeTime )

set @ChargeTypeID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ChargeTypeID.Value = Convert.ToInt64(parms["ChargeTypeID"].Value);
        }
        public void ChargeType_Update(FactoryArgs args, t_BigID ChargeTypeID, t_String ChargeTypeName)
        {
            t_String ChangeBy = new t_String();
            t_DTSmall ChangeTime = new t_DTSmall();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID));
            parms.Add(new LBDbParameter("ChargeTypeName", ChargeTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.DbChargeType
set ChargeTypeName = @ChargeTypeName,
    ChangeBy = @ChangeBy,
    ChangeTime = @ChangeTime
where ChargeTypeID  =@ChargeTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void ChargeType_Delete(FactoryArgs args, t_BigID ChargeTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID));

            string strSQL = @"
delete dbo.DbChargeType
where ChargeTypeID = @ChargeTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
        
        public void GetMaxCarCode(FactoryArgs args,out t_String MaxCode)
        {
            MaxCode = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MaxCode", MaxCode,true));
            string strSQL = @"
    select top 1 @MaxCode = ChargeTypeCode
    from dbo.DbChargeType
    order by ChargeTypeCode desc
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            MaxCode.SetValueWithObject(parms["MaxCode"].Value);
        }
        
        public DataTable GetChargeTypeByName(FactoryArgs args, t_BigID ChargeTypeID, t_String ChargeTypeName)
        {
            ChargeTypeID.IsNullToZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID));
            parms.Add(new LBDbParameter("ChargeTypeName", ChargeTypeName));
            string strSQL = @"
if @ChargeTypeID = 0
begin
    select ChargeTypeName
    from dbo.DbChargeType
    where ChargeTypeName=@ChargeTypeName
end
else
begin
    select ChargeTypeName
    from dbo.DbChargeType
    where ChargeTypeName=@ChargeTypeName and ChargeTypeID<>@ChargeTypeID
end
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
