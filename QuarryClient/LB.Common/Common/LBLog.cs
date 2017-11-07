using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common
{
    public class LBLog
    {
        public static void AssemblyStart()
        {
            LB.WinFunction.ExecuteSQL.CallSPEvent += ExecuteSQL_CallSPEvent;
        }

        private static void ExecuteSQL_CallSPEvent(WinFunction.Args.CallSPArgs e)
        {
            if (e.SPType > 0)
            {
                InsertSysLog(e.SPType,e.DTSPIN);
            }
        }

        public static void InsertSysLog(string strPermissionCode)
        {
            if (strPermissionCode != "")
            {
                DataTable dtPermissionData = LBPermission.DTPermissionData;
                DataTable dtPermission = LBPermission.DTPermission;

                DataRow[] drDataArg = dtPermissionData.Select( "PermissionCode='"+ strPermissionCode + "'");
                if (drDataArg.Length > 0)
                {
                    DataRow dr = drDataArg[0];
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                    string strPermissionDataName = dr["PermissionDataName"].ToString().TrimEnd();
                    int iPermissionType = dr["PermissionType"] == DBNull.Value ?
                    0 : Convert.ToInt16(dr["PermissionType"]);

                    string strPermissionModule = "";
                    GetPermissionModule(lPermissionID, ref strPermissionModule);

                    strPermissionModule += "->" + strPermissionDataName;
                    InsertLog(strPermissionModule, iPermissionType, "");
                }
            }
        }
        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="strPermissionCode"></param>
        public static void InsertViewSysLog(string strButtonName,string strPermissionCode)
        {
            if (strPermissionCode != "")
            {
                DataTable dtPermissionData = LBPermission.DTPermissionData;
                DataTable dtPermission = LBPermission.DTPermission;

                DataRow[] drDataArg = dtPermissionData.Select("PermissionCode='" + strPermissionCode + "' and PermissionType=1");
                if (drDataArg.Length > 0)
                {
                    DataRow dr = drDataArg[0];
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                    string strPermissionDataName = dr["PermissionDataName"].ToString().TrimEnd();
                    int iPermissionType = dr["PermissionType"] == DBNull.Value ?
                    0 : Convert.ToInt16(dr["PermissionType"]);

                    string strPermissionModule = "";
                    GetPermissionModule(lPermissionID, ref strPermissionModule);

                    strPermissionModule += "->" + strPermissionDataName;
                    strPermissionModule = "点击【" + strButtonName + "】 -> " + strPermissionModule;
                    InsertLog(strPermissionModule, iPermissionType, "");
                }
            }
        }

        public static void InsertSysLog(int iPermissionSPType,DataTable dtSPIN)
        {
            if (iPermissionSPType > 0)
            {
                DataTable dtPermissionData = LBPermission.DTPermissionData;
                DataTable dtPermission = LBPermission.DTPermission;

                DataRow[] drDataArg = dtPermissionData.Select("PermissionSPType=" + iPermissionSPType );
                if (drDataArg.Length > 0)
                {
                    DataRow dr = drDataArg[0];
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                    string strPermissionDataName = dr["PermissionDataName"].ToString().TrimEnd();
                    string strLogFieldName = dr["LogFieldName"].ToString().TrimEnd();
                    int iPermissionType = dr["PermissionType"] == DBNull.Value ?
                    0 : Convert.ToInt16(dr["PermissionType"]);

                    string strPermissionModule = "";
                    GetPermissionModule(lPermissionID, ref strPermissionModule);

                    strPermissionModule += "->" + strPermissionDataName;

                    List<string> lstFieldName = new List<string>();
                    string[] strAry = strLogFieldName.Split(',');
                    foreach(string str in strAry)
                    {
                        if (str != "")
                        {
                            if (!lstFieldName.Contains(str))
                            {
                                lstFieldName.Add(str);
                            }
                        }
                    }
                    foreach (DataRow drSPIN in dtSPIN.Rows)
                    {
                        string strValue = "";
                        foreach(string strFieldName in lstFieldName)
                        {
                            if (dtSPIN.Columns.Contains(strFieldName))
                            {
                                if (strValue != "")
                                    strValue += ",";
                                strValue += drSPIN[strFieldName].ToString().TrimEnd();
                            }
                        }

                        string strPermModule = strPermissionModule + (strValue==""?"":"["+ strValue+"]");
                        InsertLog(strPermModule, iPermissionType, "");
                    }
                }
            }
        }

        /// <summary>
        /// 读取功能模块字符串
        /// </summary>
        /// <param name="lPermissionID"></param>
        /// <returns></returns>
        private static void GetPermissionModule(long lPermissionID,ref string strPermissionModule)
        {
            DataTable dtPermission = LBPermission.DTPermission;
            DataRow[] drPermAry = dtPermission.Select("PermissionID=" + lPermissionID);
            if (drPermAry.Length > 0)
            {
                DataRow drPerm = drPermAry[0];
                long lParentPermissionID = drPerm["ParentPermissionID"] == DBNull.Value ?
                    0 : Convert.ToInt64(drPerm["ParentPermissionID"]);
                string strPermissionName = drPerm["PermissionName"].ToString().TrimEnd();

                if (strPermissionModule != "")
                {
                    strPermissionModule = "->"+ strPermissionModule;
                }
                strPermissionModule = strPermissionName+ strPermissionModule;

                if (lParentPermissionID > 0)
                {
                    GetPermissionModule(lParentPermissionID, ref strPermissionModule);
                }
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="LogModule"></param>
        /// <param name="iPermissionType"></param>
        /// <param name="LogDescription"></param>
        private static void InsertLog(
            string LogModule, int iPermissionType, string LogDescription)
        {
            string LoginName = LoginInfo.LoginName;
            string LogMachineName = LoginInfo.MachineName;
            string LogMachineIP = LoginInfo.MachineIP;
            string LogStatusName = iPermissionType == 0 ? "操作" : "查询";

            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBParameter("LoginName", enLBDbType.String, LoginName));
            parms.Add(new LBParameter("LogMachineName", enLBDbType.String, LogMachineName));
            parms.Add(new LBParameter("LogMachineIP", enLBDbType.String, LogMachineIP));
            parms.Add(new LBParameter("LogModule", enLBDbType.String, LogModule));
            parms.Add(new LBParameter("LogStatusName", enLBDbType.String, LogStatusName));
            parms.Add(new LBParameter("LogDescription", enLBDbType.String, LogDescription));
            DataSet dsReturn;
            Dictionary<string, object> dictResult;
            ExecuteSQL.CallSP(13000, parms, out dsReturn, out dictResult);
        }
    }
}
