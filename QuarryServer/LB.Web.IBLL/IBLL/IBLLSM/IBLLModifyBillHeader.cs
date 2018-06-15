using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLSM
{
    public interface IBLLModifyBillHeader
    {
        void GetCustomerItemPrice(FactoryArgs args, t_BigID ItemID, t_BigID CarID, t_BigID CustomerID, t_ID CalculateType, out t_Decimal Price,
            out t_Decimal MaterialPrice, out t_Decimal FarePrice, out t_Decimal TaxPrice, out t_Decimal BrokerPrice);
    }
}
