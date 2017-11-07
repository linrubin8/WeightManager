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
    public class BLLDBUOM : IBLLFunction
    {
        private DALDBUOM _DALDBUOM = null;
        public BLLDBUOM()
        {
            _DALDBUOM = new DAL.DALDBUOM();
        }
        
        public override string GetFunctionName(int iFunctionType)
        {
            
            string strFunName = "";
            switch (iFunctionType)
            {
                case 20200:
                    strFunName = "DBUOM_Insert";
                    break;

                case 20201:
                    strFunName = "DBUOM_Update";
                    break;

                case 20202:
                    strFunName = "DBUOM_Delete";
                    break;
            }
            return strFunName;
        }

        public void DBUOM_Insert(FactoryArgs args, out t_BigID UOMID, t_String UOMName, t_Byte UOMType)
        {
            _DALDBUOM.Insert(args, out UOMID, UOMName, UOMType);
        }

        public void DBUOM_Update(FactoryArgs args, t_BigID UOMID, t_String UOMName, t_Byte UOMType)
        {
            _DALDBUOM.Update(args, UOMID, UOMName, UOMType);
        }

        public void DBUOM_Delete(FactoryArgs args, t_BigID UOMID)
        {
            _DALDBUOM.Delete(args, UOMID);
        }
    }
}