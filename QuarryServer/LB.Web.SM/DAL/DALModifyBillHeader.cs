using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.SM.DAL
{
    public class DALModifyBillHeader
    {
        public DataTable GetCustomerByID(FactoryArgs args, t_BigID CustomerID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            string strSQL = @"

    select 1
    from dbo.DbCustomer
    where CustomerID=@CustomerID

";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable GetModifyBillHeader(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            string strSQL = @"

    select *
    from dbo.ModifyBillHeader
    where ModifyBillHeaderID=@ModifyBillHeaderID

";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public void Insert(FactoryArgs args, out t_BigID ModifyBillHeaderID, t_BigID BillTypeID, t_BigID CustomerID, t_String ModifyBillCode,
            t_DTSmall BillDate, t_DTSmall EffectDate, t_String Description)
        {
            ModifyBillHeaderID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID, true));
            parms.Add(new LBDbParameter("BillTypeID", BillTypeID));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("ModifyBillCode", ModifyBillCode));
            parms.Add(new LBDbParameter("BillDate", BillDate));
            parms.Add(new LBDbParameter("EffectDate", EffectDate));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("CreateBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CreateTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.ModifyBillHeader(CustomerID,BillTypeID, ModifyBillCode, BillDate, EffectDate, Description,
    IsApprove,IsCancel,CreateBy, CreateTime, ChangeBy, ChangeTime)
values( @CustomerID,@BillTypeID, @ModifyBillCode, @BillDate, @EffectDate, @Description, 
    0, 0, @CreateBy, @CreateTime, @ChangeBy, @ChangeTime)

set @ModifyBillHeaderID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ModifyBillHeaderID.Value = Convert.ToInt64(parms["ModifyBillHeaderID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID ModifyBillHeaderID, t_BigID CustomerID, 
            t_DTSmall BillDate, t_DTSmall EffectDate, t_String Description)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("BillDate", BillDate));
            parms.Add(new LBDbParameter("EffectDate", EffectDate));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.ModifyBillHeader
set CustomerID = @CustomerID,
    BillDate = @BillDate,
    EffectDate = @EffectDate,
    Description = @Description,
    ChangeBy = @ChangeBy,
    ChangeTime = @ChangeTime
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));

            string strSQL = @"
delete dbo.ModifyBillHeader
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Approve(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            parms.Add(new LBDbParameter("ApproveBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ApproveTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.ModifyBillHeader
set IsApprove = 1,
    ApproveBy = @ApproveBy,
    ApproveTime = @ApproveTime
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UnApprove(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));

            string strSQL = @"
update dbo.ModifyBillHeader
set IsApprove = 0,
    ApproveBy = '',
    ApproveTime = null
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Cancel(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));
            parms.Add(new LBDbParameter("CancelBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CancelTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.ModifyBillHeader
set IsCancel = 1,
    CancelBy = @CancelBy,
    CancelTime = @CancelTime
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UnCancel(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ModifyBillHeaderID", ModifyBillHeaderID));

            string strSQL = @"
update dbo.ModifyBillHeader
set IsCancel = 0,
    CancelBy = '',
    CancelTime = null
where ModifyBillHeaderID = @ModifyBillHeaderID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetSMItemPrice(FactoryArgs args,t_BigID CustomerID, t_BigID CarID, t_BigID ItemID,t_ID CalculateType)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            string strSQL = @"

    select *
    from dbo.SMItemPrice
    where CustomerID=@CustomerID and
        CarID=@CarID and
        ItemID=@ItemID and
        CalculateType=@CalculateType 
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public void UpdateSMItemPrice(FactoryArgs args, t_BigID ItemPriceID,t_Decimal ItemPrice)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemPriceID", ItemPriceID));
            parms.Add(new LBDbParameter("ItemPrice", ItemPrice));

            string strSQL = @"
update dbo.SMItemPrice
set ItemPrice = @ItemPrice
where ItemPriceID = @ItemPriceID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void InsertSMItemPrice(FactoryArgs args,out t_BigID ItemPriceID, t_BigID CustomerID, t_BigID CarID, 
            t_BigID ItemID, t_ID CalculateType, t_Decimal ItemPrice)
        {
            ItemPriceID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemPriceID", ItemPriceID, true));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            parms.Add(new LBDbParameter("ItemPrice", ItemPrice));

            string strSQL = @"
insert into dbo.SMItemPrice(CustomerID,CarID, ItemID, BillDate, CalculateType, ItemPrice)
values( @CustomerID,@CarID, @ItemID, @BillDate, @CalculateType, @ItemPrice)

set @ItemPriceID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ItemPriceID.Value = Convert.ToInt64(parms["ItemPriceID"].Value);
        }

        public void DeleteSMItemPrice(FactoryArgs args, t_BigID ItemPriceID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemPriceID", ItemPriceID));

            string strSQL = @"
delete dbo.SMItemPrice
where ItemPriceID = @ItemPriceID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetMaxBillCode(FactoryArgs args)
        {

            string strSQL = @"
select top 1 ModifyBillCode
from dbo.ModifyBillHeader
where BillDate<getdate()+1
order by ModifyBillCode desc
";
            return DBHelper.ExecuteQuery(args, strSQL);
        }

        public DataTable GetModifyBillHeaderByCustomer(FactoryArgs args,t_BigID CustomerID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            string strSQL = @"
SELECT     h.CustomerID, c.CustomerName,h.EffectDate,h.ApproveTime,
                       d.ModifyBillDetailID, d.ItemID,i.ItemCode,i.ItemRate,i.ItemMode ,i.ItemName, 
                      d.CarID, ca.CarNum, d.Price, d.CalculateType,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst
                            WHERE      (FieldName = 'CalculateType') AND (ConstValue = d.CalculateType)) AS CalculateTypeName, d.UOMID, u.UOMName
FROM         dbo.ModifyBillHeader AS h INNER JOIN
                      dbo.ModifyBillDetail AS d ON h.ModifyBillHeaderID = d.ModifyBillHeaderID INNER JOIN
                      dbo.DbCustomer AS c ON c.CustomerID = h.CustomerID INNER JOIN
                      dbo.DbItemBase AS i ON i.ItemID = d.ItemID INNER JOIN
                      dbo.DbUOM AS u ON u.UOMID = d.UOMID LEFT OUTER JOIN
                      dbo.DbCar AS ca ON ca.CarID = d.CarID
where h.CustomerID = @CustomerID and h.IsApprove=1 and isnull(h.IsCancel,0)=0 and h.EffectDate<=CONVERT(varchar(12) , getdate(), 111 )
order by h.ApproveTime desc
";
            return DBHelper.ExecuteQuery(args, strSQL,parms);
        }

        public DataTable GetCarItemPrice(FactoryArgs args, t_BigID ItemID,t_BigID CarID,t_BigID CustomerID, t_ID CalculateType)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("ItemID", ItemID));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("CalculateType", CalculateType));
            string strSQL = @"
SELECT     h.CustomerID, c.CustomerName,h.EffectDate,h.ApproveTime,
                       d.ModifyBillDetailID, d.ItemID,i.ItemCode,i.ItemRate,i.ItemMode ,i.ItemName, d.MaterialPrice,d.FarePrice,d.TaxPrice,d.BrokerPrice,
                      d.CarID,  d.Price, d.CalculateType,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst
                            WHERE      (FieldName = 'CalculateType') AND (ConstValue = d.CalculateType)) AS CalculateTypeName, d.UOMID, u.UOMName
FROM         dbo.ModifyBillHeader AS h INNER JOIN
                      dbo.ModifyBillDetail AS d ON h.ModifyBillHeaderID = d.ModifyBillHeaderID INNER JOIN
                      dbo.DbCustomer AS c ON c.CustomerID = h.CustomerID INNER JOIN
                      dbo.DbItemBase AS i ON i.ItemID = d.ItemID INNER JOIN
                      dbo.DbUOM AS u ON u.UOMID = d.UOMID 
                    --LEFT OUTER JOIN dbo.DbCar AS ca ON ca.CarID = d.CarID and 
where --d.CarID = @CarID and 
    d.ItemID = @ItemID and 
    isnull(h.CustomerID,0) = isnull(@CustomerID,0) and
    d.CalculateType = @CalculateType and 
    h.IsApprove=1 and 
    isnull(h.IsCancel,0)=0 and 
    h.EffectDate<=CONVERT(varchar(12) , getdate(), 111 )
order by h.ApproveTime desc
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable ReadItem(FactoryArgs args, t_BigID ItemID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemID", ItemID));

            string strSQL = @"
select *
from dbo.DbItemBase
where ItemID=@ItemID
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
