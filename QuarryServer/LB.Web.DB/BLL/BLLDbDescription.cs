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
    public class BLLDbDescription : IBLLFunction
    {
        private DALDbDescription _DALDbDescription = null;
        public BLLDbDescription()
        {
            _DALDbDescription = new DAL.DALDbDescription();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 14000:
                    strFunName = "InsertAndUpdate";
                    break;
                case 14001:
                    strFunName = "Delete";
                    break;

            }
            return strFunName;
        }

        public void InsertAndUpdate(FactoryArgs args,
           t_String Description)
        {
            using (DataTable dtExists = _DALDbDescription.VerifyExists(args, Description))
            {
                if (dtExists.Rows.Count == 0)
                {
                    t_BigID DescriptionID;
                    _DALDbDescription.Insert(args, out DescriptionID, Description);
                }
                else
                {
                    t_BigID DescriptionID = new t_BigID(dtExists.Rows[0]["DescriptionID"]);
                    _DALDbDescription.Update(args, DescriptionID, Description);
                }
            }
        }

        public void Delete(FactoryArgs args,
           t_BigID DescriptionID)
        {
            _DALDbDescription.Delete(args, DescriptionID);
        }
    }
}