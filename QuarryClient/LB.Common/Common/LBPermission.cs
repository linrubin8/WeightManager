using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Common
{
    public class LBPermission
    {
        public static DataTable DTPermissionData;
        public static DataTable DTPermission;
        /// <summary>
        /// 读取所有权限配置信息
        /// </summary>
        public static void ReadAllPermission()
        {
            DTPermission = ExecuteSQL.CallView(103);
            DTPermissionData = ExecuteSQL.CallView(104);
        }

        /// <summary>
        /// 校验用户权限，如果没有权限则报错
        /// </summary>
        /// <param name="strPermissionCode"></param>
        public static void VerifyUserPermission(string strButtonName,string strPermissionCode)
        {
            if (strPermissionCode != null && strPermissionCode != "")
            {
                string strPermissionDataName;
                string strPermissionName;
                bool bolHasPermission = GetPermission(strPermissionCode, out strPermissionDataName, out strPermissionName);

                if (!bolHasPermission)
                {
                    throw new Exception("该用户没有【" + strPermissionName + "—>" + strPermissionDataName + "】权限！");
                }
                else
                {
                    //如果是查询动作，则记录到日志
                    LBLog.InsertViewSysLog(strButtonName,strPermissionCode);
                }
            }
        }

        /// <summary>
        /// 校验用户权限，返回布尔值
        /// </summary>
        /// <param name="strPermissionCode">权限校验码</param>
        /// <returns></returns>
        public static bool GetUserPermission(string strPermissionCode)
        {
            bool bolHasPermission = true;
            if (strPermissionCode != null && strPermissionCode != "")
            {
                string strPermissionDataName;
                string strPermissionName;
                bolHasPermission = GetPermission(strPermissionCode, out strPermissionDataName, out strPermissionName);  
            }
            return bolHasPermission;
        }

        private static bool GetPermission(string strPermissionCode,
            out string strPermissionDataName,out string strPermissionName)
        {
            bool bolHasPermission = true;
            strPermissionDataName = "";
            strPermissionName = "";
            DataTable dtSPIn = new DataTable();
            dtSPIn.Columns.Add("UserID", typeof(long));
            dtSPIn.Columns.Add("PermissionDataName", typeof(string));
            dtSPIn.Columns.Add("PermissionName", typeof(string));
            dtSPIn.Columns.Add("PermissionCode", typeof(string));
            dtSPIn.Columns.Add("HasPermission", typeof(bool));

            DataRow drNew = dtSPIn.NewRow();
            drNew["PermissionCode"] = strPermissionCode;
            drNew["UserID"] = LoginInfo.UserID;
            dtSPIn.Rows.Add(drNew);

            DataSet dsReturn;
            DataTable dtReturn;
            //GetUserPermission
            ExecuteSQL.CallSP(11000, dtSPIn, out dsReturn, out dtReturn);

            if(dtReturn == null)
            {
                throw new Exception("校验权限失败！");
            }

            if (dtReturn!=null && dtReturn.Rows.Count > 0)
            {
                DataRow drResult = dtReturn.Rows[0];
                bolHasPermission = drResult["HasPermission"] == DBNull.Value ?
                    false : Convert.ToBoolean(drResult["HasPermission"]);
                strPermissionDataName = drResult["PermissionDataName"].ToString().TrimEnd();
                strPermissionName = drResult["PermissionName"].ToString().TrimEnd();
            }
            return bolHasPermission;
        }
    }
}
