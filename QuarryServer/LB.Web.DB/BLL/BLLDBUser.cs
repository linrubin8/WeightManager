using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using LB.Web.Contants.DBType;
using LB.Web.Base.Helper;
using LB.Web.DB.DAL;
using LB.Web.Base.Factory;

namespace LB.Web.DB.BLL
{
    public class BLLDBUser : IBLLFunction
    {
        private DALDBUser _DALDBUser = null;
        public BLLDBUser()
        {
            _DALDBUser = new DAL.DALDBUser();
        }
        
        public override string GetFunctionName(int iFunctionType)
        {
            
            string strFunName = "";
            switch (iFunctionType)
            {
                case 10000:
                    strFunName = "DBUser_Insert";
                    break;

                case 10001:
                    strFunName = "DBUser_Update";
                    break;

                case 10002:
                    strFunName = "DBUser_Delete";
                    break;

                case 10003:
                    strFunName = "DBUser_ChangePassword";
                    break;
            }
            return strFunName;
        }

        public void DBUser_Insert(FactoryArgs args, out t_BigID UserID,t_String LoginName, 
            t_String UserPassword, t_String UserName,
            t_ID UserType, t_ID UserSex)
        {
            UserID = new t_BigID();

            using (DataTable dtUser = _DALDBUser.GetUserByLoginName(args,UserID, LoginName))
            {
                if (dtUser.Rows.Count > 0)
                {
                    throw new Exception("该账户名称已存在！");
                }
            }

            _DALDBUser.Insert(args, out UserID, LoginName, UserPassword, UserName, UserType, UserSex);
        }

        public void DBUser_Update(FactoryArgs args, t_BigID UserID, t_String LoginName, t_String UserPassword, t_String UserName,
            t_ID UserType, t_ID UserSex)
        {
            using (DataTable dtUser = _DALDBUser.GetUserByLoginName(args, UserID, LoginName))
            {
                if (dtUser.Rows.Count > 0)
                {
                    throw new Exception("该账户名称已存在！");
                }
            }

            _DALDBUser.Update(args, UserID, LoginName, UserPassword, UserName, UserType, UserSex);
        }

        public void DBUser_Delete(FactoryArgs args, t_BigID UserID)
        {
            _DALDBUser.Delete(args, UserID);
        }

        public void DBUser_ChangePassword(FactoryArgs args, t_BigID UserID, t_String UserPassword)
        {
            _DALDBUser.ChangePassword(args, UserID, UserPassword);
        }

    }
}