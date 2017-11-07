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
    public class BLLDbSysLog : IBLLFunction
    {
        private DALPermission _DALPermission = null;
        private DALDbSysLog _DALDbSysLog = null;
        public BLLDbSysLog()
        {
            _DALDbSysLog = new DAL.DALDbSysLog();
            _DALPermission = new DAL.DALPermission();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13000:
                    strFunName = "Insert";
                    break;

                case 13001:
                    strFunName = "Delete";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, t_String LoginName,
            t_String LogMachineName, t_String LogMachineIP, t_String LogModule, t_String LogStatusName, t_String LogDescription)
        {
            _DALDbSysLog.Insert(args, LoginName, LogMachineName, LogMachineIP, LogModule, LogStatusName, LogDescription);
        }

        public void Delete(FactoryArgs args,t_String SysLogIDStr)
        {
            if(SysLogIDStr.Value!=null&& SysLogIDStr.Value.ToString() != "")
            {
                _DALDbSysLog.Delete(args, SysLogIDStr);
            }
        }
    }
}
