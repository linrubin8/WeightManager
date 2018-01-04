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
    public class BLLDBItemType : IBLLFunction
    {
        private DALDBItemType _DALDBItemType = null;
        public BLLDBItemType()
        {
            _DALDBItemType = new DAL.DALDBItemType();
        }
        
        public override string GetFunctionName(int iFunctionType)
        {
            
            string strFunName = "";
            switch (iFunctionType)
            {
                case 20500:
                    strFunName = "DBItemType_Insert";
                    break;

                case 20501:
                    strFunName = "DBItemType_Update";
                    break;

                case 20502:
                    strFunName = "DBItemType_Delete";
                    break;
            }
            return strFunName;
        }

        public void DBItemType_Insert(FactoryArgs args, out t_BigID ItemTypeID, t_String ItemTypeName)
        {
            _DALDBItemType.Insert(args, out ItemTypeID, ItemTypeName);
        }

        public void DBItemType_Update(FactoryArgs args, t_BigID ItemTypeID, t_String ItemTypeName)
        {
            _DALDBItemType.Update(args, ItemTypeID, ItemTypeName);
        }

        public void DBItemType_Delete(FactoryArgs args, t_BigID ItemTypeID)
        {
            _DALDBItemType.Delete(args, ItemTypeID);
        }
    }
}