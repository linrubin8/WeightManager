using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.WinFunction
{
    public class LBRegisterPermission
    {
        public static bool IsRegister = false;//是否具有软件使用权限
        public static int ProductType = 0;//产品类型
        public static DateTime DeadLine = DateTime.MinValue;//到期日期
        public static bool Permission_ModelSynchorToServer = false;//同步数据至服务器权限

        public static void VerifyPermission()
        {
            Dictionary<string, object> dictModel;
            string RegisterInfoJson;
            ExecuteSQL.ReadRegister(out IsRegister, out ProductType, out DeadLine,out dictModel);
            if (dictModel.ContainsKey("SynToServer"))
            {
                int iPermissoin;
                int.TryParse(dictModel["SynToServer"].ToString(), out iPermissoin);
                if (iPermissoin > 0)
                {
                    Permission_ModelSynchorToServer = true;
                }
            }
        }

    }
}
