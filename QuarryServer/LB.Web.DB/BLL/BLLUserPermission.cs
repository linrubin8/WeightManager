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
    public class BLLUserPermission : IBLLFunction
    {
        private DALUserPermission _DALUserPermission = null;
        public BLLUserPermission()
        {
            _DALUserPermission = new DAL.DALUserPermission();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13100:
                    strFunName = "GetUserPermissionData";
                    break;

                case 13101:
                    strFunName = "GetUserPermission";
                    break;

                case 13102:
                    strFunName = "SaveUserPermission";
                    break;
            }
            return strFunName;
        }

        public void GetUserPermissionData(FactoryArgs args, t_BigID UserID)
        {
            args.SelectResult = _DALUserPermission.GetUserPermissionData(args, UserID);
        }

        public void GetUserPermission(FactoryArgs args, t_BigID UserID)
        {
            args.SelectResult = _DALUserPermission.GetUserPermission(args, UserID);
        }

        public void SaveUserPermission(FactoryArgs args, t_BigID UserID, t_Table DTUserPermission, t_Table DTUserPermissionData)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALUserPermission.DeleteUserPermission(argsInTrans, UserID);
                _DALUserPermission.DeleteUserPermissionData(argsInTrans, UserID);

                foreach(DataRow dr in DTUserPermissionData.Value.Rows)
                {
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);

                    DTUserPermission.Value.DefaultView.RowFilter = "HasPermission=1 and PermissionID="+lPermissionID;

                    if (DTUserPermission.Value.DefaultView.Count == 0)
                    {
                        bool bolHasPermission = dr["HasPermission"] == DBNull.Value ?
                        false : Convert.ToBoolean(dr["HasPermission"]);
                        t_BigID PermissionDataID = new t_BigID(dr["PermissionDataID"]);
                        if (bolHasPermission)
                        {
                            _DALUserPermission.InsertUserPermissionData(argsInTrans, UserID, PermissionDataID);
                        }
                    }
                }

                foreach (DataRow dr in DTUserPermission.Value.Rows)
                {
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);

                    bool bolHasPermission = dr["HasPermission"] == DBNull.Value ?
                        false : Convert.ToBoolean(dr["HasPermission"]);
                    t_BigID PermissionID = new t_BigID(dr["PermissionID"]);
                    if (bolHasPermission)
                    {
                        _DALUserPermission.InsertUserPermission(argsInTrans, UserID, PermissionID);
                    }
                }
            };
            DBHelper.ExecInTrans(args, exec);
        }
    }
}
