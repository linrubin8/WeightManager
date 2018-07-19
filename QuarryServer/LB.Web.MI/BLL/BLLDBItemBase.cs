using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using LB.Web.Contants.DBType;
using LB.Web.Base.Helper;
using LB.Web.MI.DAL;
using LB.Web.Base.Factory;

namespace LB.Web.MI.BLL
{
    public class BLLDBItemBase : IBLLFunction
    {
        private DALDBItemBase _DALDBItemBase = null;
        public BLLDBItemBase()
        {
            _DALDBItemBase = new DAL.DALDBItemBase();
        }
        
        public override string GetFunctionName(int iFunctionType)
        {
            
            string strFunName = "";
            switch (iFunctionType)
            {
                case 20300:
                    strFunName = "DBItemBase_Insert";
                    break;

                case 20301:
                    strFunName = "DBItemBase_Update";
                    break;

                case 20302:
                    strFunName = "DBItemBase_Delete";
                    break;
            }
            return strFunName;
        }

        public void DBItemBase_Insert(FactoryArgs args, out t_BigID ItemID, t_BigID ItemTypeID,
            out t_String ItemCode,t_String K3ItemCode, t_String ItemName, t_String ItemMode, t_Float ItemRate,
            t_BigID UOMID, t_String Description, t_Bool IsForbid,t_Decimal ItemPrice)
        {
            ItemCode = new t_String();

            t_String MaxCode;
            _DALDBItemBase.GetMaxCode(args, out MaxCode);
            int CodeIndex = MaxCode.Value == null ? 0 : LBConverter.ToInt32(MaxCode.Value.Replace("S", ""));
            CodeIndex++;
            if (CodeIndex < 10)
                ItemCode.SetValueWithObject("S00" + CodeIndex.ToString());
            else if (CodeIndex < 100)
                ItemCode.SetValueWithObject("S0" + CodeIndex.ToString());
            else
                ItemCode.SetValueWithObject("S"+CodeIndex.ToString());


            _DALDBItemBase.Insert(args, out ItemID, ItemTypeID, ItemCode, K3ItemCode, ItemName, ItemMode,
                ItemRate, UOMID, Description, IsForbid, ItemPrice);
        }

        public void DBItemBase_Update(FactoryArgs args, t_BigID ItemID, t_BigID ItemTypeID,
             t_String K3ItemCode, t_String ItemName, t_String ItemMode, t_Float ItemRate,
            t_BigID UOMID, t_String Description, t_Bool IsForbid, t_Decimal ItemPrice)
        {
            _DALDBItemBase.Update(args, ItemID, ItemTypeID, K3ItemCode, ItemName, ItemMode,
                ItemRate, UOMID, Description, IsForbid, ItemPrice);
        }

        public void DBItemBase_Delete(FactoryArgs args, t_BigID ItemID)
        {
            _DALDBItemBase.Delete(args, ItemID);
        }

    }
}