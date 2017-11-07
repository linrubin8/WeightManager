using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Web.SM.DAL
{
    public class DALSaleCarReturnBill
    {
        public void Insert(FactoryArgs args, out t_BigID SaleCarReturnBillID,t_String SaleCarReturnBilCode, t_BigID SaleCarInBillID,
            t_Decimal TotalWeight)
        {
            SaleCarReturnBillID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SaleCarReturnBillID", SaleCarReturnBillID,true));
            parms.Add(new LBDbParameter("SaleCarInBillID", SaleCarInBillID));
            parms.Add(new LBDbParameter("TotalWeight", TotalWeight));
            parms.Add(new LBDbParameter("SaleCarReturnBilCode", SaleCarReturnBilCode));
            parms.Add(new LBDbParameter("CreateBy", new t_String(args.LoginName)));
            string strSQL = @"
                insert SaleCarReturnBill(SaleCarInBillID,SaleCarReturnBilCode,ReturnStatus,TotalWeight,BillDate,CarInTime,CreateBy,CreateTime)
                values(@SaleCarInBillID,@SaleCarReturnBilCode,0,@TotalWeight,getdate(),getdate(),@CreateBy,getdate())
    
                set @SaleCarReturnBillID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            SaleCarReturnBillID.Value = Convert.ToInt64(parms["SaleCarReturnBillID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID SaleCarReturnBillID,
            t_Decimal CarTare,t_Decimal SuttleWeight,t_ID ReturnType, t_String Description, t_ID ReturnReason,
            t_BigID NewSaleCarInBillID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SaleCarReturnBillID", SaleCarReturnBillID));
            parms.Add(new LBDbParameter("CarTare", CarTare));
            parms.Add(new LBDbParameter("SuttleWeight", SuttleWeight));
            parms.Add(new LBDbParameter("ReturnType", ReturnType));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ReturnReason", ReturnReason));
            parms.Add(new LBDbParameter("NewSaleCarInBillID", NewSaleCarInBillID));
            string strSQL = @"
                update SaleCarReturnBill
                set ReturnStatus=1,
                    CarTare = @CarTare,
                    SuttleWeight = @SuttleWeight,
                    ReturnType = @ReturnType,
                    Description = @Description,
                    CarOutTime = getdate(),
                    ReturnReason = @ReturnReason,
                    NewSaleCarInBillID = @NewSaleCarInBillID
                where SaleCarReturnBillID = @SaleCarReturnBillID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetReturnBill(FactoryArgs args, t_BigID SaleCarReturnBillID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("SaleCarReturnBillID", SaleCarReturnBillID));
            string strSQL = @"
                select *
                from dbo.SaleCarReturnBill
                where SaleCarReturnBillID=@SaleCarReturnBillID
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable GetMaxInBillCode(FactoryArgs args, string strBillFont)
        {
            string strSQL = @"
select top 1 SaleCarReturnBilCode
from dbo.SaleCarReturnBill
where SaleCarReturnBilCode like '" + strBillFont + @"%'
order by SaleCarReturnBilCode desc
";
            return DBHelper.ExecuteQuery(args, strSQL);
        }
    }
}
