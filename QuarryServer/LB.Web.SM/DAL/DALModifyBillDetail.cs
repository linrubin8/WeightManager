using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.SM.DAL
{
    public class DALModifyBillDetail
    {
        public void Insert(FactoryArgs args, out t_BigID ModifyBillDetailID, t_BigID ModifyBillHeaderID, t_BigID ItemID, t_BigID CarID,
            t_Decimal Price, t_ID CalculateType, t_BigID UOMID, t_String Description)
        {
            ModifyBillDetailID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillDetailID", ModifyBillDetailID, true));
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("Price", Price));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            parms.Add(new LBDbParameter("UOMID", UOMID));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("CreateBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CreateTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.ModifyBillDetail(ModifyBillHeaderID, ItemID, CarID, Price, CalculateType, UOMID, 
    Description, CreateBy, CreateTime, ChangeBy, ChangeTime)
values( @ModifyBillHeaderID, @ItemID, @CarID, @Price, @CalculateType, @UOMID, 
    @Description, @CreateBy, @CreateTime, @ChangeBy, @ChangeTime)

set @ModifyBillDetailID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ModifyBillDetailID.Value = Convert.ToInt64(parms["ModifyBillDetailID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID ModifyBillDetailID, t_BigID ItemID, t_BigID CarID,
            t_Decimal Price, t_ID CalculateType, t_BigID UOMID, t_String Description)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillDetailID", ModifyBillDetailID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("Price", Price));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            parms.Add(new LBDbParameter("UOMID", UOMID));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.ModifyBillDetail
set ItemID = @ItemID,
    CarID = @CarID,
    Price = @Price,
    Description = @Description,
    CalculateType = @CalculateType,
    UOMID = @UOMID,
    ChangeBy = @ChangeBy,
    ChangeTime = @ChangeTime
where ModifyBillDetailID = @ModifyBillDetailID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetModifyBillDetailByHeaderID(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            string strSQL = @"

    select *
    from dbo.ModifyBillDetail
    where ModifyBillHeaderID=@ModifyBillHeaderID

";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public void Delete(FactoryArgs args, t_BigID ModifyBillDetailID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillDetailID", ModifyBillDetailID));

            string strSQL = @"
delete dbo.ModifyBillDetail
where ModifyBillDetailID = @ModifyBillDetailID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        //按审核日期计算读取最近审核的明细行记录
        public DataTable GetLastModifyBillDetail(FactoryArgs args, t_BigID ModifyBillDetailID, 
            t_BigID CustomerID, t_BigID CarID, t_BigID ItemID, t_ID CalculateType)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillDetailID", ModifyBillDetailID));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            string strSQL = @"

    select top 1 d.*
    from dbo.ModifyBillDetail d
        inner join dbo.ModifyBillHeader h on
            d.ModifyBillHeaderID = h.ModifyBillHeaderID
    where   ModifyBillDetailID<>@ModifyBillDetailID and
            CustomerID = @CustomerID and
            CarID = @CarID and
            ItemID = @ItemID and
            CalculateType = @CalculateType and
            h.IsApprove=1
    order by ApproveTime desc
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
