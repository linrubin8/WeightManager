using LB.Web.Base.Base.Helper;
using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.DB.BLL.Factory
{
    public class AssemblyStart
    {
        public AssemblyStart()
        {
            DBHelper.GetBLLObjectMethodEevent += DBHelper_GetBLLObjectMethodEevent;
        }

        private void DBHelper_GetBLLObjectMethodEevent(Base.Base.Factory.GetBLLObjectEventArgs args)
        {
            //300~399
            int iSPType = args.SPType;
            if (iSPType == 0 || args.BLLFunction != null)
            {
                return;
            }

            switch (iSPType)
            {
                case 10000:
                case 10001:
                case 10002:
                case 10003:
                    args.BLLFunction = new BLLDBUser();
                    break;

                case 11000:
                case 11001:
                case 11002:
                case 11003:
                case 11010:
                case 11011:
                case 11012:
                    args.BLLFunction = new BLLPermission();
                    break;

                case 12000:
                case 12001:
                case 12002:
                case 12003:
                case 12004:
                case 12005:
                case 12006:
                    args.BLLFunction = new BLLDbReportTemplate();
                    break;

                case 13000:
                case 13001:
                    args.BLLFunction = new BLLDbSysLog();
                    break;

                case 13100:
                case 13101:
                case 13102:
                    args.BLLFunction = new BLLUserPermission();
                    break;

                case 13200:
                case 13201:
                case 13202:
                    args.BLLFunction = new BLLDbBackUpConfig();
                    break;

                case 13800:
                    args.BLLFunction = new BLLDbWeightDeviceUserType();
                    break;

                case 13900:
                    args.BLLFunction = new BLLDbCameraConfig();
                    break;
                case 14000:
                case 14001:
                    args.BLLFunction = new BLLDbDescription();
                    break;

                case 14200:
                    args.BLLFunction = new BLLDbWeightType();
                    break;

                case 14300:
                case 14301:
                    args.BLLFunction = new BLLDbSysConfig();
                    break;

                case 14400:
                case 14401:
                case 14402:
                case 14403:
                case 14404:
                case 14405:
                case 14406:
                case 14407:
                    args.BLLFunction = new BLLDbReportView();
                    break;

                case 14500:
                    args.BLLFunction = new DbInfraredDeviceConfig();
                    break;

                case 20000:
                    args.BLLFunction = new BLLDbErrorLog();
                    break;

                case 20100:
                    args.BLLFunction = new BLLDbSystemConst();
                    break;

                case 30000:
                case 30001:
                case 30002:
                    args.BLLFunction = new BLLSysSession();
                    break;
            }
        }
    }
}
