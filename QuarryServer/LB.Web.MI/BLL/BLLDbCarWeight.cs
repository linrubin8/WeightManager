using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.IBLL.IBLL.IBLLMI;
using LB.Web.MI.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.MI.BLL
{
    public class BLLDbCarWeight : IBLLFunction, IBLLDbCarWeight
    {
        private DALDbCarWeight _DALDbCarWeight = null;
        public BLLDbCarWeight()
        {
            _DALDbCarWeight = new DAL.DALDbCarWeight();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 20400:
                    strFunName = "Insert";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, t_BigID CarID,t_Decimal CarWeight, t_String Description)
        {
            _DALDbCarWeight.Insert(args, CarID, CarWeight, Description);
        }
    }
}
