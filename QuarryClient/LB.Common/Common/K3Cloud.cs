using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Common
{
    public class K3Cloud
    {
        public static void InsertK3Bill(string strSalesOutBillCode,string strSalesInBillCode,string strOutBillDate, string strCustomerID,
            string strCarNum, string strItemID,decimal decTotalWeight,decimal decCarTare, decimal decSuttleWeight,
            decimal decPrice,decimal decAmount)
        {
            string str ="the third is \"c\" ! ";
            string strJson = @"
{
    \""Creator\"": \""\"",
    \""NeedUpDateFields\"": [],
    \""NeedReturnFields\"": [],
    \""IsDeleteEntry\"": \""True\"",
    \""SubSystemId\"": \""\"",
    \""IsVerifyBaseDataField\"": \""false\"",
    \""IsEntryBatchFill\"": \""True\"",
    \""Model\"": {
        \""FID\"": \""0\"",
        \""FBillTypeID\"": {
            \""FNumber\"": \""XSCKD01_SYS\""
        },
        \""FBillNo\"": \"""+ strSalesOutBillCode + @"\"",
        \""FDate\"": \""" + DateTime.Now.ToString("yyyy-MM-dd") + @"\"",
        \""FSaleOrgId\"": {
            \""FNumber\"": \""10\""
        },
        \""FCustomerID\"": {
            \""FNumber\"": \""1122-1\""
        },
        \""FStockOrgId\"": {
            \""FNumber\"": \""10\""
        },

        \""FNote\"": \""\"",
        \""FReceiverID\"": {
            \""FNumber\"": \""1122-1\""
        },
        \""FSettleID\"": {
            \""FNumber\"": \""1122-1\""
        },

        \""FPayerID\"": {
            \""FNumber\"": \""1122-1\""
        },
        \""FOwnerTypeIdHead\"": \""BD_OwnerOrg\"",
        \""FOwnerIdHead\"": {
            \""FNumber\"": \""\""
        },
        \""FCDateOffsetValue\"": 0,
        \""FIsTotalServiceOrCost\"": false,
        \""F_PAEZ_CCNum\"": \"""+ strSalesInBillCode + @"\"",
        \""F_PAEZ_RCNum\"": \""" + strSalesOutBillCode + @"\"",
        \""SubHeadEntity\"": {
            \""FSettleCurrID\"": {
                \""FNumber\"": \""PRE001\""
            },
            \""FThirdBillNo\"": \""\"",
            \""FThirdBillId\"": \""\"",
            \""FThirdSrcType\"": \"" \"",
            \""FSettleOrgID\"": {
                \""FNumber\"": \""10\""
            },
            \""FSettleTypeID\"": {
                \""FNumber\"": \""\""
            },
            \""FReceiptConditionID\"": {
                \""FNumber\"": \""\""
            },
            \""FPriceListId\"": {
                \""FNumber\"": \""\""
            },
            \""FDiscountListId\"": {
                \""FNumber\"": \""\""
            },
            \""FIsIncludedTax\"": true,
            \""FLocalCurrID\"": {
                \""FNumber\"": \""PRE001\""
            },
            \""FExchangeTypeID\"": {
                \""FNumber\"": \""HLTX01_SYS\""
            },
            \""FExchangeRate\"": 1,
            \""FIsPriceExcludeTax\"": true
        },
        \""FEntity\"": [
            {
                \""FENTRYID\"": 0,
                \""FRowType\"": \""Standard\"",
                \""FMaterialID\"": {
                    \""FNumber\"": \""2.01.0001.0001\""
                },
                \""FUnitID\"": {
                    \""FNumber\"": \""ton\""
                },
                \""FInventoryQty\"": 0,
                \""FParentMatId\"": {
                    \""FNumber\"": \""\""
                },
                \""FRealQty\"": 1,
                \""FDisPriceQty\"": 0,
                \""FPrice\"": "+ decPrice + @",
                \""FTaxPrice\"": 0,
                \""FIsFree\"": false,
                \""FBomID\"": {
                    \""FNumber\"": \""\""
                },
                \""FProduceDate\"": null,
                \""FOwnerTypeID\"": \""BD_OwnerOrg\"",
                \""FOwnerID\"": {
                    \""FNumber\"": \""10\""
                },
                \""FEntryTaxRate\"": 17,
                \""FAuxUnitQty\"": 0,
                \""FExtAuxUnitId\"": {
                    \""FNumber\"": \""\""
                },
                \""FExtAuxUnitQty\"": 0,
                \""FStockID\"": {
                    \""FNumber\"": \""10-CK100\""
                },
                \""FStockStatusID\"": {
                    \""FNumber\"": \""KCZT01_SYS\""
                },
                \""FQualifyType\"": \""\"",
                \""FMtoNo\"": null,
                \""FEntrynote\"": null,
                \""FDiscountRate\"": 0,
                \""FActQty\"": 0,
                \""FSalUnitID\"": {
                    \""FNumber\"": \""ton\""
                },
                \""FSALUNITQTY\"": "+ decSuttleWeight + @",
                \""FSALBASEQTY\"": " + decSuttleWeight + @",
                \""FPRICEBASEQTY\"": " + decSuttleWeight + @",
                \""FOUTCONTROL\"": false,
                \""FARNOTJOINQTY\"": "+ decSuttleWeight + @",
                \""FQmEntryID\"": 0,
                \""FConvertEntryID\"": 0,
                \""FSOEntryId\"": 0,
                \""F_PAEZ_CheHao\"": \"""+strCarNum+@"\"",
                \""F_PAEZ_Qty\"": "+decTotalWeight+@",
                \""FThirdEntryId\"": null,
                \""F_PAEZ_PiZhong\"": "+decCarTare+@",
                \""F_PAEZ_JingZHong\"": "+decSuttleWeight+@",
                \""FBeforeDisPriceQty\"": 0,
                \""F_PAEZ_JingZhongDun\"": "+ (decSuttleWeight /1000)+ @",
                \""F_PAEZ_ShiJie\"": 0,
                \""F_PAEZ_YunFei\"": 0,
                \""FSignQty\"": 0,
                \""F_PAEZ_ShuiJin\"": 0,
                \""F_PAEZ_YongJin\"": 0,
                \""F_PAEZ_SKFangShi\"": \""预付\""
            }
        ]
    }
}";
            //K3CloudApiClient client = new K3CloudApiClient("http://jr1099.ik3cloud.com/K3Cloud/");//正式地址
            K3CloudApiClient client = new K3CloudApiClient("http://jr1099.test.ik3cloud.com/K3cloud/");//测试地址
            var ret = client.ValidateLogin("20180424162627", "005", "jr1099!@", 2052);
            var result = JObject.Parse(ret)["LoginResultType"].Value<int>();
            // 登陆成功
            if (result == 1)
            {
                strJson = strJson.Replace("\r\n","").Replace(" ", "").Replace("\\", "");
                var test = JObject.Parse(strJson);
                string strOut = client.Save("SAL_OUTSTOCK", strJson);
                //throw new Exception(strOut);
            }
            else
            {

            }
        }
    }
}
