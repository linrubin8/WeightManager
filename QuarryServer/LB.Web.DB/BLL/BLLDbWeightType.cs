using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbWeightType : IBLLFunction
    {
        private DALDbWeightType _DALDbWeightType = null;
        public BLLDbWeightType()
        {
            _DALDbWeightType = new DAL.DALDbWeightType();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 14200:
                    strFunName = "InsertAndUpdate";
                    break;
            }
            return strFunName;
        }

        public void InsertAndUpdate(FactoryArgs args,
           t_ID WeightType, t_String MachineName)
        {
            using (DataTable dtExists = _DALDbWeightType.VerifyExists(args, MachineName))
            {
                if (dtExists.Rows.Count == 0)
                {
                    _DALDbWeightType.Insert(args, WeightType, MachineName);
                }
                else
                {
                    t_BigID WeightTypeID = new t_BigID(dtExists.Rows[0]["WeightTypeID"]);
                    _DALDbWeightType.Update(args, WeightTypeID, WeightType, MachineName);
                }
            }
        }
    }
}