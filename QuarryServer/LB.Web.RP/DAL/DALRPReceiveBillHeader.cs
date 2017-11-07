using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.RP.DAL
{
    public class DALRPReceiveBillHeader
    {
        public void Insert(FactoryArgs args, out t_BigID ReceiveBillHeaderID,t_String ReceiveBillCode, t_DTSmall BillDate, t_BigID CustomerID, t_Decimal ReceiveAmount,
            t_String Description, t_BigID SaleCarInBillID, t_BigID ReceiveBankID, t_ID RPReceiveType,
            t_Decimal SalesReceiveAmountAdd, t_Decimal SalesReceiveAmountReduce, t_Decimal OriginalAmount,
            t_BigID ChargeTypeID)
        {
            ReceiveBillHeaderID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID, true));
            parms.Add(new LBDbParameter("ReceiveBillCode", ReceiveBillCode));
            parms.Add(new LBDbParameter("SaleCarInBillID", SaleCarInBillID));
            parms.Add(new LBDbParameter("BillDate", BillDate));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("ReceiveAmount", ReceiveAmount));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID));
            parms.Add(new LBDbParameter("RPReceiveType", RPReceiveType));
            parms.Add(new LBDbParameter("CreatedBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("CreateTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("SalesReceiveAmountAdd", SalesReceiveAmountAdd));
            parms.Add(new LBDbParameter("SalesReceiveAmountReduce", SalesReceiveAmountReduce));
            parms.Add(new LBDbParameter("OriginalAmount", OriginalAmount));
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID));

            string strSQL = @"
insert into dbo.RPReceiveBillHeader( ReceiveBillCode, BillDate, CustomerID, ReceiveAmount, 
Description,CreatedBy,CreateTime,ChangedBy,ChangeTime,IsApprove,IsCancel,SaleCarInBillID,
ReceiveBankID,RPReceiveType,SalesReceiveAmountAdd,SalesReceiveAmountReduce,OriginalAmount,ChargeTypeID)
values( @ReceiveBillCode, @BillDate, @CustomerID, @ReceiveAmount, 
@Description,@CreatedBy,@CreateTime,@ChangedBy,@ChangeTime,0,0,@SaleCarInBillID,
@ReceiveBankID,@RPReceiveType,@SalesReceiveAmountAdd,@SalesReceiveAmountReduce,@OriginalAmount,@ChargeTypeID)

set @ReceiveBillHeaderID = @@identity

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReceiveBillHeaderID.SetValueWithObject(parms["ReceiveBillHeaderID"].Value);
        }


        public void Update(FactoryArgs args, t_BigID ReceiveBillHeaderID, t_DTSmall BillDate, t_Decimal ReceiveAmount,
            t_String Description, t_BigID ReceiveBankID, t_ID RPReceiveType,
            t_Decimal SalesReceiveAmountAdd, t_Decimal SalesReceiveAmountReduce, t_Decimal OriginalAmount,
            t_BigID ChargeTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));
            parms.Add(new LBDbParameter("BillDate", BillDate));
            parms.Add(new LBDbParameter("ReceiveAmount", ReceiveAmount));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID));
            parms.Add(new LBDbParameter("RPReceiveType", RPReceiveType));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));
            parms.Add(new LBDbParameter("SalesReceiveAmountAdd", SalesReceiveAmountAdd));
            parms.Add(new LBDbParameter("SalesReceiveAmountReduce", SalesReceiveAmountReduce));
            parms.Add(new LBDbParameter("OriginalAmount", OriginalAmount));
            parms.Add(new LBDbParameter("ChargeTypeID", ChargeTypeID));

            string strSQL = @"
update dbo.RPReceiveBillHeader
set BillDate = @BillDate,
    ReceiveAmount = @ReceiveAmount,
    Description = @Description,
    ChangedBy = @ChangedBy,
    ChangeTime = @ChangeTime,
    ReceiveBankID = @ReceiveBankID,
    RPReceiveType = @RPReceiveType,
    SalesReceiveAmountAdd = @SalesReceiveAmountAdd,
    SalesReceiveAmountReduce = @SalesReceiveAmountReduce,
    OriginalAmount = @OriginalAmount,
    ChargeTypeID = @ChargeTypeID
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));

            string strSQL = @"
delete dbo.RPReceiveBillHeader
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Approve(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));
            parms.Add(new LBDbParameter("ApproveBy", new t_String(args.LoginName)));

            string strSQL = @"
update dbo.RPReceiveBillHeader
set IsApprove = 1,
    ApproveTime = getdate(),
    ApproveBy = @ApproveBy
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UnApprove(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));

            string strSQL = @"
update dbo.RPReceiveBillHeader
set IsApprove = 0,
    ApproveTime = null,
    ApproveBy = ''
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Cancel(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));
            parms.Add(new LBDbParameter("CancelBy", new t_String(args.LoginName)));

            string strSQL = @"
update dbo.RPReceiveBillHeader
set IsCancel = 1,
    CancelTime = getdate(),
    CancelBy = @CancelBy
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UnCancel(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));

            string strSQL = @"
update dbo.RPReceiveBillHeader
set IsCancel = 0,
    CancelTime = null,
    CancelBy = ''
where ReceiveBillHeaderID = @ReceiveBillHeaderID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetMaxBillCode(FactoryArgs args ,string strBillFont)
        {

            string strSQL = @"
select top 1 ReceiveBillCode
from dbo.RPReceiveBillHeader
where ReceiveBillCode like '"+ strBillFont + @"%'
order by ReceiveBillCode desc
";
           return DBHelper.ExecuteQuery(args,strSQL);
        }

        public DataTable GetRPReceiveBillHeader(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBillHeaderID", ReceiveBillHeaderID));
            string strSQL = @"
select *
from dbo.RPReceiveBillHeader
where ReceiveBillHeaderID = @ReceiveBillHeaderID
";
            return DBHelper.ExecuteQuery(args, strSQL,parms);
        }

        public void UpdateCustomerReceiveAmount(FactoryArgs args, t_BigID CustomerID,t_Decimal ReceiveAmount,
            t_Decimal SalesReceiveAmountAdd, t_Decimal SalesReceiveAmountReduce)
        {
            SalesReceiveAmountAdd.IsNullToZero();
            SalesReceiveAmountReduce.IsNullToZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerID", CustomerID));
            parms.Add(new LBDbParameter("ReceiveAmount", ReceiveAmount));
            parms.Add(new LBDbParameter("SalesReceiveAmountAdd", SalesReceiveAmountAdd));
            parms.Add(new LBDbParameter("SalesReceiveAmountReduce", SalesReceiveAmountReduce));
            string strSQL = @"
update dbo.DbCustomer
set TotalReceivedAmount = isnull(TotalReceivedAmount,0)+isnull(@ReceiveAmount,0),
    SalesReceivedAmount= isnull(SalesReceivedAmount,0)+@SalesReceiveAmountAdd-@SalesReceiveAmountReduce
where CustomerID = @CustomerID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
