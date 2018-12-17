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
    public class BLLDBCustomerType : IBLLFunction
    {
        private DALDBCustomerType _DALDBCustomerType = null;
        public BLLDBCustomerType()
        {
            _DALDBCustomerType = new DAL.DALDBCustomerType();
        }
        
        public override string GetFunctionName(int iFunctionType)
        {
            
            string strFunName = "";
            switch (iFunctionType)
            {
                case 14800:
                    strFunName = "DBCustomerType_Insert";
                    break;

                case 14801:
                    strFunName = "DBCustomerType_Update";
                    break;

                case 14802:
                    strFunName = "DBCustomerType_Delete";
                    break;
            }
            return strFunName;
        }

        public void DBCustomerType_Insert(FactoryArgs args, out t_BigID CustomerTypeID, t_String CustomerTypeCode,
            t_String CustomerTypeName)
        {
            CustomerTypeID = new t_BigID();
            using (DataTable dtCustomerType = _DALDBCustomerType.GetCustomerTypeByCode(args, CustomerTypeID, CustomerTypeCode))
            {
                if (dtCustomerType.Rows.Count > 0)
                {
                    throw new Exception("该客户类型编码已存在！");
                }
            }
            using (DataTable dtCustomerType = _DALDBCustomerType.GetCustomerTypeByName(args, CustomerTypeID, CustomerTypeName))
            {
                if (dtCustomerType.Rows.Count > 0)
                {
                    throw new Exception("该客户类型名称已存在！");
                }
            }
            _DALDBCustomerType.Insert(args, out CustomerTypeID, CustomerTypeCode, CustomerTypeName);
        }

        public void DBCustomerType_Update(FactoryArgs args, t_BigID CustomerTypeID, t_String CustomerTypeCode,
            t_String CustomerTypeName)
        {
            using (DataTable dtCustomerType = _DALDBCustomerType.GetCustomerTypeByCode(args, CustomerTypeID, CustomerTypeCode))
            {
                if (dtCustomerType.Rows.Count > 0)
                {
                    throw new Exception("该客户类型编码已存在！");
                }
            }
            using (DataTable dtCustomerType = _DALDBCustomerType.GetCustomerTypeByName(args, CustomerTypeID, CustomerTypeName))
            {
                if (dtCustomerType.Rows.Count > 0)
                {
                    throw new Exception("该客户类型名称已存在！");
                }
            }
            _DALDBCustomerType.Update(args, CustomerTypeID, CustomerTypeCode, CustomerTypeName);
        }

        public void DBCustomerType_Delete(FactoryArgs args, t_BigID CustomerTypeID)
        {
            _DALDBCustomerType.Delete(args, CustomerTypeID);
        }
    }
}