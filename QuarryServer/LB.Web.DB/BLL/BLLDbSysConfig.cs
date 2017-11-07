using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbSysConfig : IBLLFunction
    {
        private DALDbSysConfig _DALDbSysConfig = null;
        public BLLDbSysConfig()
        {
            _DALDbSysConfig = new DAL.DALDbSysConfig();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 14300:
                    strFunName = "Update";
                    break;

                case 14301:
                    strFunName = "GetConfigValue";
                    break;
            }
            return strFunName;
        }

        public void Update(FactoryArgs args, t_String SysConfigFieldName, t_String SysConfigValue)
        {
            t_ID SysConfigDataType = new t_ID(0);//0字符串 1小数 2整数  3布尔值
            t_String SysConfigFieldText = new t_String("");
            using (DataTable dtConfig = _DALDbSysConfig.GetDbSysConfigField(args, SysConfigFieldName))
            {
                if (dtConfig.Rows.Count == 0)
                {
                    throw new Exception("该系统设置无效！");
                }
                else
                {
                    DataRow drConfig = dtConfig.Rows[0];
                    SysConfigDataType.SetValueWithObject(drConfig["SysConfigDataType"]);
                    SysConfigFieldText.SetValueWithObject(drConfig["SysConfigFieldText"]);
                }
            }

            if (SysConfigDataType.Value == 1)
            {
                decimal decValue;
                if(!decimal.TryParse(SysConfigValue.Value,out decValue))
                {
                    throw new Exception("【"+SysConfigFieldText.Value+"】为小数类型，当前传入的值为【"+ SysConfigValue.Value + "】，无法保存！");
                }
            }

            if (SysConfigDataType.Value == 2)
            {
                int iValue;
                if (!int.TryParse(SysConfigValue.Value, out iValue))
                {
                    throw new Exception("【" + SysConfigFieldText.Value + "】为整数类型，当前传入的值为【" + SysConfigValue.Value + "】，无法保存！");
                }
            }

            if (SysConfigDataType.Value == 3)
            {
                int iValue;
                if (!int.TryParse(SysConfigValue.Value, out iValue))
                {
                    throw new Exception("【" + SysConfigFieldText.Value + "】为布尔值类型，当前传入的值为【" + SysConfigValue.Value + "】，无法保存！");
                }
            }

            _DALDbSysConfig.Update(args, SysConfigFieldName, SysConfigValue);
        }

        public void GetConfigValue(FactoryArgs args, t_String SysConfigFieldName,out t_String SysConfigValue)
        {
            SysConfigValue = new t_String("");
            t_String SysConfigDefaultValue = new t_String("");
            using (DataTable dtConfig = _DALDbSysConfig.GetDbSysConfigField(args, SysConfigFieldName))
            {
                if (dtConfig.Rows.Count == 0)
                {
                    throw new Exception("该系统设置无效！");
                }
                else
                {
                    DataRow drConfig = dtConfig.Rows[0];
                    SysConfigDefaultValue.SetValueWithObject(drConfig["SysConfigDefaultValue"]);
                }
            }

            using (DataTable dtConfigValue = _DALDbSysConfig.GetDbSysConfigValue(args, SysConfigFieldName))
            {
                if (dtConfigValue.Rows.Count == 0)
                {
                    SysConfigValue.Value = SysConfigDefaultValue.Value;
                }
                else
                {
                    DataRow drConfig = dtConfigValue.Rows[0];
                    SysConfigValue.SetValueWithObject(drConfig["SysConfigValue"]);
                }
            }
        }
    }
}
