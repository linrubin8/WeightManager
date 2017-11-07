using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLSM
{
    public interface IBLLSaleCarInOutBill
    {
        void Approve(FactoryArgs args, t_BigID SaleCarInBillID,t_DTSmall ApproveTime);

        void UnApprove(FactoryArgs args, t_BigID SaleCarInBillID);

        void Cancel(FactoryArgs args, t_BigID SaleCarInBillID, t_String CancelDesc);

        void UnCancel(FactoryArgs args, t_BigID SaleCarInBillID);

        void CopySaleBill(FactoryArgs args, t_BigID SaleCarInBillID, t_Decimal NewTotalWeight, 
            out t_BigID NewSaleCarInBillID, out t_BigID NewSaleCarOutBillID, out t_String NewSaleCarInBillCode, 
            out t_String NewSaleCarOutBillCode);
        
    }
}
