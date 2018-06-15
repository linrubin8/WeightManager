using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.SM.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.SM.BLL
{
    public class BLLModifyBillDetail : IBLLFunction
    {
        private DALModifyBillDetail _DALModifyBillDetail = null;
        public BLLModifyBillDetail()
        {
            _DALModifyBillDetail = new DAL.DALModifyBillDetail();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13700:
                    strFunName = "Insert";
                    break;
                case 13701:
                    strFunName = "Update";
                    break;
                case 13702:
                    strFunName = "Delete";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args,out t_BigID ModifyBillDetailID, t_BigID ModifyBillHeaderID, t_BigID ItemID, t_BigID CarID, 
            t_Decimal Price,t_ID CalculateType, t_BigID UOMID,t_String Description, t_Decimal MaterialPrice, t_Decimal FarePrice, 
            t_Decimal TaxPrice, t_Decimal BrokerPrice)
        {
            _DALModifyBillDetail.Insert(args, out ModifyBillDetailID, ModifyBillHeaderID, ItemID, CarID, Price, CalculateType, UOMID, Description,
                MaterialPrice, FarePrice,TaxPrice,BrokerPrice);
        }

        public void Update(FactoryArgs args, t_BigID ModifyBillDetailID, t_BigID ItemID, t_BigID CarID,
            t_Decimal Price, t_ID CalculateType, t_BigID UOMID, t_String Description, t_Decimal MaterialPrice, t_Decimal FarePrice,
            t_Decimal TaxPrice, t_Decimal BrokerPrice)
        {
            _DALModifyBillDetail.Update(args, ModifyBillDetailID, ItemID, CarID, Price, CalculateType, UOMID, Description,
                MaterialPrice, FarePrice, TaxPrice, BrokerPrice);
        }

        public void Delete(FactoryArgs args, t_BigID ModifyBillDetailID)
        {
            _DALModifyBillDetail.Delete(args, ModifyBillDetailID);
        }

    }
}
