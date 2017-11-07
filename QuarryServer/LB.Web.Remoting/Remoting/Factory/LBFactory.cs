using System;
using System.Collections.Generic;
using System.Web;
using LB.Web.Base.Helper;

namespace LB.Web.Remoting.Factory
{
    public class LBFactory
    {
        public LBFactory()
        {
            
        }

        public IBLLFunction GetAssemblyFunction(int iFunctionType)
        {
            //switch (iFunctionType)
            //{
            //    case 9000:
            //    case 9001:
            //    case 9002:
            //        return new BLLSysViewType();
            //    case 10000:
            //    case 10001:
            //    case 10002:
            //    case 10003:
            //        return new BLLDBUser();

            //    case 11000:
            //    case 11001:
            //    case 11002:
            //    case 11003:
            //    case 11010:
            //    case 11011:
            //    case 11012:
            //        return new BLLPermission();

            //    case 12000:
            //    case 12001:
            //    case 12002:
            //        return new BLLDbReportTemplate();

            //    case 13000:
            //    case 13001:
            //        return new BLLDbSysLog();

            //    case 13100:
            //    case 13101:
            //    case 13102:
            //        return new BLLUserPermission();

            //    case 13200:
            //    case 13201:
            //    case 13202:
            //        return new BLLDbBackUpConfig();

            //    case 20100:
            //    case 20101:
            //    case 20102:
            //        return new BLLDBItemType();

            //    case 20200:
            //    case 20201:
            //    case 20202:
            //        return new BLLDBUOM();

            //    case 20300:
            //    case 20301:
            //    case 20302:
            //        return new BLLDBItemBase();

            //    case 13300:
            //    case 13301:
            //    case 13302:
            //    case 13303:
            //    case 13304:
            //    case 13305:
            //    case 13306:
            //        return new BLLRPReceiveBillHeader();

            //    case 13400:
            //    case 13401:
            //    case 13402:
            //        return new BLLDbCustomer();
            //    case 13500:
            //    case 13501:
            //    case 13502:
            //    case 13503:
            //        return new BLLDbCar();

            //    case 13600:
            //    case 13601:
            //    case 13602:
            //    case 13603:
            //    case 13604:
            //    case 13605:
            //    case 13606:
            //    case 13607:
            //    case 13608:
            //        return new BLLModifyBillHeader();

            //    case 13700:
            //    case 13701:
            //    case 13702:
            //        return new BLLModifyBillDetail();

            //    case 13800:
            //        return new BLLDbWeightDeviceUserType();

            //    case 13900:
            //        return new BLLDbCameraConfig();

            //    case 14000:
            //    case 14001:
            //        return new BLLDbDescription();

            //    case 14100:
            //    case 14101:
            //    case 14102:
            //    case 14103:
            //    case 14104:
            //    case 14105:
            //    case 14106:
            //    case 14107:
            //    case 14108:
            //    case 14109:
            //    case 14110:
            //    case 14111:
            //        return new BLLSaleCarInOutBill();

            //    case 14200:
            //        return new BLLDbWeightType();
            //}

            return null;
        }
    }
}