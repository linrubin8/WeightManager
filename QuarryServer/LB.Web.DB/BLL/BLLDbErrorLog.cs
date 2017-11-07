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
    public class BLLDbErrorLog : IBLLFunction, IBLLDbErrorLog
    {
        private DALDbErrorLog _DALDbErrorLog = null;
        public BLLDbErrorLog()
        {
            _DALDbErrorLog = new DAL.DALDbErrorLog();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 20000:
                    strFunName = "Insert";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args,t_String ErrorLogMsg)
        {
            _DALDbErrorLog.Insert(args, ErrorLogMsg);
        }
    }
}
