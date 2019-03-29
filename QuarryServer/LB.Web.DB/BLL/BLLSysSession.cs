using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using LB.Web.Encrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLSysSession : IBLLFunction
    {
        private DALSysSession _DALSysSession = null;
        public BLLSysSession()
        {
            _DALSysSession = new DAL.DALSysSession();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 15000:
                    strFunName = "Insert";
                    break;

                case 15001:
                    strFunName = "UpdateLastCheckTime";
                    break;

                case 15002:
                    strFunName = "Delete";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args,out t_BigID SessionID,
            t_String ClientIP, t_String ClientSerial,out t_Bool IsOverFlow)
        {
            SessionID = new t_BigID(0);
            IsOverFlow = new t_Bool(0);
            //判断是否超用户数
            if (LBEncrypt.UseSessionLimit)
            {
                using (DataTable dtSession = _DALSysSession.GetAllSysSession(args))
                {
                    if(dtSession.Rows.Count>= LBEncrypt.SessionLimitCount)//在线用户数超过限定数
                    {
                        IsOverFlow.Value = 1;
                        return;
                    }
                }
            }
            _DALSysSession.Insert(args, out SessionID, ClientIP, ClientSerial);
        }

        public void UpdateLastCheckTime(FactoryArgs args, t_BigID SessionID)
        {
            _DALSysSession.UpdateLastCheckTime(args, SessionID);
        }

        public void Delete(FactoryArgs args, t_BigID SessionID)
        {
            _DALSysSession.Delete(args, SessionID);
        }
    }
}
