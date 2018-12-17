using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.MI.DAL
{
    public class DALDBCustomerType
    {
        public void Insert(FactoryArgs args, out t_BigID CustomerTypeID, t_String CustomerTypeCode,
            t_String CustomerTypeName)
        {
            CustomerTypeID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerTypeID", CustomerTypeID, true ));
            parms.Add(new LBDbParameter("CustomerTypeCode", CustomerTypeCode));
            parms.Add(new LBDbParameter("CustomerTypeName", CustomerTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbCustomerType( CustomerTypeCode, CustomerTypeName, CreateBy, CreateTime, ChangeBy, ChangeTime)
values( @CustomerTypeCode, @CustomerTypeName, @ChangeBy, @ChangeTime, @ChangeBy, @ChangeTime)

set @CustomerTypeID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            CustomerTypeID.Value = Convert.ToInt64(parms["CustomerTypeID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID CustomerTypeID, t_String CustomerTypeCode,
            t_String CustomerTypeName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerTypeID", CustomerTypeID));
            parms.Add(new LBDbParameter("CustomerTypeCode", CustomerTypeCode));
            parms.Add(new LBDbParameter("CustomerTypeName", CustomerTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.DbCustomerType
set CustomerTypeCode=@CustomerTypeCode,
    CustomerTypeName=@CustomerTypeName,
    ChangeBy=@ChangeBy,
    ChangeTime=@ChangeTime 
where CustomerTypeID = @CustomerTypeID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID CustomerTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerTypeID", CustomerTypeID));

            string strSQL = @"
delete dbo.DbCustomerType
where CustomerTypeID = @CustomerTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetCustomerTypeByCode(FactoryArgs args, t_BigID CustomerTypeID, t_String CustomerTypeCode)
        {
            CustomerTypeID.IsNullToZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerTypeID", CustomerTypeID));
            parms.Add(new LBDbParameter("CustomerTypeCode", CustomerTypeCode));
            string strSQL = @"
if @CustomerTypeID = 0
begin
    select CustomerTypeName
    from dbo.DbCustomerType
    where CustomerTypeCode=@CustomerTypeCode
end
else
begin
    select CustomerTypeName
    from dbo.DbCustomerType
    where CustomerTypeCode=@CustomerTypeCode and CustomerTypeID<>@CustomerTypeID
end
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable GetCustomerTypeByName(FactoryArgs args, t_BigID CustomerTypeID, t_String CustomerTypeName)
        {
            CustomerTypeID.IsNullToZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CustomerTypeID", CustomerTypeID));
            parms.Add(new LBDbParameter("CustomerTypeName", CustomerTypeName));
            string strSQL = @"
if @CustomerTypeID = 0
begin
    select CustomerTypeName
    from dbo.DbCustomerType
    where CustomerTypeName=@CustomerTypeName
end
else
begin
    select CustomerTypeName
    from dbo.DbCustomerType
    where CustomerTypeName=@CustomerTypeName and CustomerTypeID<>@CustomerTypeID
end
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}