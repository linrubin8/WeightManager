using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common
{
    public class SysConfigValue
    {
        public static void SaveSysConfig(string SysConfigFieldName,object ConfigValue)
        {
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SysConfigFieldName", enLBDbType.String, SysConfigFieldName));
            parmCol.Add(new LBParameter("SysConfigValue", enLBDbType.String, ConfigValue));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14300, parmCol, out dsReturn, out dictValue);
        }

        public static void GetSysConfig(string SysConfigFieldName,out string ConfigValue)
        {
            ConfigValue = "";
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SysConfigFieldName", enLBDbType.String, SysConfigFieldName));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14301, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SysConfigValue"))
            {
                ConfigValue = dictValue["SysConfigValue"].ToString();
            }
        }

        public static void GetSysConfig(string SysConfigFieldName, out decimal ConfigValue)
        {
            ConfigValue = 0;
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SysConfigFieldName", enLBDbType.String, SysConfigFieldName));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14301, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SysConfigValue"))
            {
                string strConfigValue = dictValue["SysConfigValue"].ToString();
                decimal.TryParse(strConfigValue,out ConfigValue);
            }
        }

        public static void GetSysConfig(string SysConfigFieldName, out int ConfigValue)
        {
            ConfigValue = 0;
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SysConfigFieldName", enLBDbType.String, SysConfigFieldName));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14301, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SysConfigValue"))
            {
                string strConfigValue = dictValue["SysConfigValue"].ToString();
                int.TryParse(strConfigValue, out ConfigValue);
            }
        }

        public static void GetSysConfig(string SysConfigFieldName, out bool ConfigValue)
        {
            ConfigValue = false;
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("SysConfigFieldName", enLBDbType.String, SysConfigFieldName));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(14301, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SysConfigValue"))
            {
                int iConfigValue;
                string strConfigValue = dictValue["SysConfigValue"].ToString();
                int.TryParse(strConfigValue, out iConfigValue);
                if (iConfigValue > 0)
                {
                    ConfigValue = true;
                }
            }
        }
    }
}
