using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.MI.DAL
{
    public class DALDbBank
    {
        public void Bank_Insert(FactoryArgs args, out t_BigID ReceiveBankID,t_String BankCode, t_String BankName)
        {
            t_String ChangeBy = new t_String();
            t_DTSmall ChangeTime = new t_DTSmall();
             ReceiveBankID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID, true));
            parms.Add(new LBDbParameter("BankCode", BankCode));
            parms.Add(new LBDbParameter("BankName", BankName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbReceiveBank( BankCode,BankName,ChangeBy,ChangeTime )
values(@BankCode,  @BankName, @ChangeBy, @ChangeTime )

set @ReceiveBankID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReceiveBankID.Value = Convert.ToInt64(parms["ReceiveBankID"].Value);
        }
        public void Bank_Update(FactoryArgs args, t_BigID ReceiveBankID, t_String BankName)
        {
            t_String ChangeBy = new t_String();
            t_DTSmall ChangeTime = new t_DTSmall();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID));
            parms.Add(new LBDbParameter("BankName", BankName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.DbReceiveBank
set BankName = @BankName,
    ChangeBy = @ChangeBy,
    ChangeTime = @ChangeTime
where ReceiveBankID  =@ReceiveBankID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Bank_Delete(FactoryArgs args, t_BigID ReceiveBankID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID));

            string strSQL = @"
delete dbo.DbReceiveBank
where ReceiveBankID = @ReceiveBankID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
        
        public void GetMaxCarCode(FactoryArgs args,out t_String MaxCode)
        {
            MaxCode = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MaxCode", MaxCode,true));
            string strSQL = @"
    select top 1 @MaxCode = BankCode
    from dbo.DbReceiveBank
    order by BankCode desc
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            MaxCode.SetValueWithObject(parms["MaxCode"].Value);
        }

        public bool VerifyIsRefCustomer(FactoryArgs args, t_BigID CarID, t_BigID CustomerID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CarID", CarID));
            parms.Add(new LBDbParameter("CustomerID", CustomerID));

            string strSQL = @"
select *
from dbo.DbCustomerCar
where CarID = @CarID and 
      CustomerID = @CustomerID
";
            DataTable dt = DBHelper.ExecuteQuery(args, strSQL, parms);
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }

        public DataTable GetBankByName(FactoryArgs args, t_BigID ReceiveBankID, t_String BankName)
        {
            ReceiveBankID.IsNullToZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReceiveBankID", ReceiveBankID));
            parms.Add(new LBDbParameter("BankName", BankName));
            string strSQL = @"
if @ReceiveBankID = 0
begin
    select BankName
    from dbo.DbReceiveBank
    where BankName=@BankName
end
else
begin
    select BankName
    from dbo.DbReceiveBank
    where BankName=@BankName and ReceiveBankID<>@ReceiveBankID
end
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
