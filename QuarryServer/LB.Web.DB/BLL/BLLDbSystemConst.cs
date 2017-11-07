using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using LB.Web.IBLL.IBLL.IBLLDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbSystemConst : IBLLFunction, IBLLDbSystemConst
    {
        private DALDbSystemConst _DALDbSystemConst = null;
        public BLLDbSystemConst()
        {
            _DALDbSystemConst = new DAL.DALDbSystemConst();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 20100:
                    strFunName = "GetConstValue";
                    break;
            }
            return strFunName;
        }

        public void GetConstValue(FactoryArgs args, t_String FieldName,
            t_String ConstValue,out t_String ConstText)
        {
            _DALDbSystemConst.GetConstValue(args, FieldName, ConstValue, out ConstText);
        }
        
    }
}
