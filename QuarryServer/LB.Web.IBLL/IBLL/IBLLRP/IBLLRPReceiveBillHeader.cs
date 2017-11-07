using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLRP
{
    public interface IBLLRPReceiveBillHeader
    {
        void Insert(FactoryArgs args, out t_BigID ReceiveBillHeaderID, out t_String ReceiveBillCode, t_DTSmall BillDate, t_BigID CustomerID, t_Decimal ReceiveAmount,
            t_String Description,t_BigID SaleCarInBillID, t_BigID ReceiveBankID, t_ID RPReceiveType,
            t_Decimal SalesReceiveAmountAdd, t_Decimal SalesReceiveAmountReduce, t_Decimal OriginalAmount,
            t_BigID ChargeTypeID);

        void Approve(FactoryArgs args, t_BigID ReceiveBillHeaderID);
        void UnApprove(FactoryArgs args, t_BigID ReceiveBillHeaderID);
        void Cancel(FactoryArgs args, t_BigID ReceiveBillHeaderID);
        void UnCancel(FactoryArgs args, t_BigID ReceiveBillHeaderID);
    }
}
