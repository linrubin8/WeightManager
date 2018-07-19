using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.MI.DAL
{
    public class DALDBItemBase
    {
        public void Insert(FactoryArgs args, out t_BigID ItemID, t_BigID ItemTypeID,
            t_String ItemCode, t_String K3ItemCode, t_String ItemName, t_String ItemMode, t_Float ItemRate,
            t_BigID UOMID, t_String Description, t_Bool IsForbid,t_Decimal ItemPrice)
        {
            ItemID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemID", ItemID, true));
            parms.Add(new LBDbParameter("ItemTypeID", ItemTypeID));
            parms.Add(new LBDbParameter("ItemCode", ItemCode));
            parms.Add(new LBDbParameter("K3ItemCode", K3ItemCode));
            parms.Add(new LBDbParameter("ItemName", ItemName));
            parms.Add(new LBDbParameter("ItemMode", ItemMode));
            parms.Add(new LBDbParameter("ItemRate", ItemRate));
            parms.Add(new LBDbParameter("UOMID", UOMID));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("IsForbid", IsForbid));
            parms.Add(new LBDbParameter("ItemPrice", ItemPrice));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbItemBase( ItemTypeID, ItemCode,K3ItemCode, ItemName, ItemMode, ItemRate, UOMID, Description, 
IsForbid, ChangeBy, ChangeTime,ItemPrice)
values( @ItemTypeID, @ItemCode,@K3ItemCode, @ItemName, @ItemMode, @ItemRate, @UOMID, @Description, 
@IsForbid, @ChangeBy, @ChangeTime,@ItemPrice)

set @ItemID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ItemID.Value = Convert.ToInt64(parms["ItemID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID ItemID, t_BigID ItemTypeID,
            t_String K3ItemCode, t_String ItemName, t_String ItemMode, t_Float ItemRate,
            t_BigID UOMID, t_String Description, t_Bool IsForbid, t_Decimal ItemPrice)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("ItemTypeID", ItemTypeID));
            parms.Add(new LBDbParameter("K3ItemCode", K3ItemCode));
            parms.Add(new LBDbParameter("ItemName", ItemName));
            parms.Add(new LBDbParameter("ItemMode", ItemMode));
            parms.Add(new LBDbParameter("ItemRate", ItemRate));
            parms.Add(new LBDbParameter("UOMID", UOMID));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("IsForbid", IsForbid));
            parms.Add(new LBDbParameter("ItemPrice", ItemPrice));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.DbItemBase
set ItemTypeID=@ItemTypeID,
    K3ItemCode = @K3ItemCode,
    ItemName=@ItemName,
    ItemMode=@ItemMode,
    ItemRate=@ItemRate,
    UOMID=@UOMID,
    Description=@Description,
    IsForbid=@IsForbid,
    ItemPrice = @ItemPrice,
    ChangeBy=@ChangeBy,
    ChangeTime=@ChangeTime 
where ItemID = @ItemID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID ItemID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemID", ItemID));

            string strSQL = @"
delete dbo.DbItemBase
where ItemID = @ItemID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void GetMaxCode(FactoryArgs args, out t_String MaxCode)
        {
            MaxCode = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MaxCode", MaxCode, true));
            string strSQL = @"
    select top 1 @MaxCode = ItemCode
    from dbo.DbItemBase
    order by ItemCode desc
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            MaxCode.SetValueWithObject(parms["MaxCode"].Value);
        }
    }
}