using LB.Web.Base.Base.Helper;
using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.IBLL.IBLL.IBLLDB;
using LB.Web.IBLL.IBLL.IBLLMI;
using LB.Web.IBLL.IBLL.IBLLRP;
using LB.Web.IBLL.IBLL.IBLLSM;
using LB.Web.SM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace LB.Web.SM.BLL
{
    public class BLLSaleCarInOutBill : IBLLFunction, IBLLSaleCarInOutBill
    {
        private static long mServerVersion = Convert.ToInt64(DateTime.Now.ToString("hhmmss"));
        private DALSaleCarInOutBill _DALSaleCarInOutBill = null;
        private IBLLDbCarWeight _IBLLDbCarWeight = null;
        private IBLLRPReceiveBillHeader _IBLLRPReceiveBillHeader = null;
        private IBLLDbErrorLog _IBLLDbErrorLog = null;
        private IBLLDbSysConfig _IBLLDbSysConfig = null;
        private IBLLModifyBillHeader _IBLLModifyBillHeader = null;
        public BLLSaleCarInOutBill()
        {
            _DALSaleCarInOutBill = new DAL.DALSaleCarInOutBill();
            _IBLLDbCarWeight = (IBLLDbCarWeight)DBHelper.GetFunctionMethod(20400);
            _IBLLRPReceiveBillHeader = (IBLLRPReceiveBillHeader)DBHelper.GetFunctionMethod(13300);
            _IBLLDbErrorLog = (IBLLDbErrorLog)DBHelper.GetFunctionMethod(20000);
            _IBLLDbSysConfig=(IBLLDbSysConfig)DBHelper.GetFunctionMethod(14300);
            _IBLLModifyBillHeader = (IBLLModifyBillHeader)DBHelper.GetFunctionMethod(13608);
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 14100:
                    strFunName = "InsertInBill";
                    break;
                case 14101:
                    strFunName = "GetCarNotOutBill";
                    break;

                case 14102:
                    strFunName = "InsertOutBill";
                    break;
                case 14103:
                    strFunName = "GetCameraImage";
                    break;

                case 14104:
                    strFunName = "Approve";
                    break;

                case 14105:
                    strFunName = "UnApprove";
                    break;

                case 14106:
                    strFunName = "Cancel";
                    break;

                case 14107:
                    strFunName = "UnCancel";
                    break;

                case 14108:
                    strFunName = "GetSaleCarPriceInfo";
                    break;

                case 14109:
                    strFunName = "UpdateInPrintCount";
                    break;

                case 14110:
                    strFunName = "UpdateOutPrintCount";
                    break;

                case 14111:
                    strFunName = "SaveInSalesCameraImage";
                    break;

                case 14112:
                    strFunName = "InsertOnlyOutBill";
                    break;

                case 14113:
                    strFunName = "ReadSaleInfo";
                    break;

                case 14114:
                    strFunName = "SaveOutSalesCameraImage";
                    break;

                case 14115:
                    strFunName = "InsertChangeBill";
                    break;

                case 14116:
                    strFunName = "UpdateInOutBill";
                    break;

                case 14117:
                    strFunName = "ImportSalesBill";
                    break;

                case 14118:
                    strFunName = "VerifyIsNeedReflesh";//校验是否需要刷新地磅页面
                    break;

                case 14119:
                    strFunName = "InsertPurchaseInBill";
                    break;

                case 14120:
                    strFunName = "InsertPurchaseOutBill";
                    break;

                case 14121:
                    strFunName = "SaveScreenImage";
                    break;

                case 14122:
                    strFunName = "SynchronousBillFromClient";
                    break;

                case 14123:
                    strFunName = "SynchronousFinish";
                    break;
            }
            return strFunName;
        }

        //InBillFrom入场单据来源：0手工单 1系统自动生成单
        public void InsertInBill(FactoryArgs args, out t_BigID SaleCarInBillID, ref t_String SaleCarInBillCode, ref t_DTSmall BillDate, t_BigID CarID,
            t_BigID ItemID, t_ID ReceiveType, t_ID CalculateType, t_Float CarTare, t_BigID CustomerID, t_String Description,
            t_ID InBillFrom, t_ID SaleBillType,t_BigID SaleCarInBillIDFromClient, t_String CreateBy)
        {
            SaleBillType.IsNullToZero();
            InBillFrom.IsNullToZero();
            SaleCarInBillID = new t_BigID();

            if (BillDate.Value == null)
            {
                BillDate = new t_DTSmall(DateTime.Now);
            }
            CustomerID.NullIfZero();

            if (CarID.Value == 0)
            {
                throw new Exception("车牌号不能为空！");
            }
            if (ItemID.Value == 0)
            {
                throw new Exception("货物名称不能为空！");
            }
            if (string.IsNullOrEmpty(CreateBy.Value))
            {
                CreateBy.Value = args.LoginName;
            }
            //linrubin 1226 允许客户名称为空
            /*if (CustomerID.Value == 0)
            {
                throw new Exception("客户名称不能为空！");
            }*/

            //先校验该车辆是否存在入磅但是没有出磅的记录，如果存在则报错
            using (DataTable dtExistsNotOut = _DALSaleCarInOutBill.ExistsNotOut(args, CarID))
            {
                if (dtExistsNotOut.Rows.Count > 0)
                {
                    DateTime dtBillDate = Convert.ToDateTime(dtExistsNotOut.Rows[0]["BillDate"]);
                    throw new Exception("该车辆在[" + dtBillDate.ToString("yyyy-MM-dd HH:mm") + "入场，但是没有出场记录，本次操作失败！");
                }
            }

            //判断原单号是否已存在
            SaleCarInBillIDFromClient.NullIfZero();
            if (SaleCarInBillIDFromClient.Value != null)
            {
                bool bolExists = _DALSaleCarInOutBill.VerifyIfExistsSourceInBill(args, SaleCarInBillIDFromClient);
                if (bolExists)
                {
                    throw new Exception("该入场单已同步，无法再次执行同步！");
                }
            }

            //读取入场单号前缀
            t_String SysConfigFieldName = new t_String("SaleInBillCode");
            t_String SysConfigValue;
            _IBLLDbSysConfig.GetConfigValue(args, SysConfigFieldName, out SysConfigValue);
            if (SysConfigValue.Value == "")
            {
                SysConfigValue.Value = "JC";
            }

            //生成编码
            if (SaleCarInBillCode.Value == null)
            {
                string strBillFont = SysConfigValue.Value + DateTime.Now.ToString("yyyyMM") + "-";
                using (DataTable dtBillCode = _DALSaleCarInOutBill.GetMaxInBillCode(args, strBillFont))
                {
                    if (dtBillCode.Rows.Count > 0)
                    {
                        DataRow drBillCode = dtBillCode.Rows[0];
                        int iIndex = 1;
                        string strIndex = "";
                        if (drBillCode["SaleCarInBillCode"].ToString().TrimEnd().Contains(strBillFont))
                        {
                            iIndex = Convert.ToInt32(drBillCode["SaleCarInBillCode"].ToString().TrimEnd().Replace(strBillFont, ""));
                            iIndex += 1;
                            if (iIndex < 10)
                            {
                                strIndex = "0000" + iIndex.ToString();
                            }
                            else if (iIndex < 100)
                            {
                                strIndex = "000" + iIndex.ToString();
                            }
                            else if (iIndex < 1000)
                            {
                                strIndex = "00" + iIndex.ToString();
                            }
                            else if (iIndex < 10000)
                            {
                                strIndex = "0" + iIndex.ToString();
                            }
                            else
                            {
                                strIndex = iIndex.ToString();
                            }
                            SaleCarInBillCode.SetValueWithObject(strBillFont + strIndex);
                        }
                        else
                        {
                            SaleCarInBillCode.SetValueWithObject(strBillFont + "-00001");
                        }
                    }
                    else
                    {
                        SaleCarInBillCode.SetValueWithObject(strBillFont + "00001");
                    }
                }
            }

            t_BigID SaleCarInBillID_temp = new t_BigID();
            t_String SaleCarInBillCode_temp = new t_String(SaleCarInBillCode.Value);
            t_DTSmall BillDate_temp = new t_DTSmall(BillDate.Value);
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALSaleCarInOutBill.InsertInBill(argsInTrans, out SaleCarInBillID_temp, SaleCarInBillCode_temp, CarID, ItemID,
                    BillDate_temp, ReceiveType, CalculateType, CarTare, CustomerID, Description, SaleBillType, SaleCarInBillIDFromClient, CreateBy);

                if (InBillFrom.Value == 1)
                {
                    _IBLLDbCarWeight.Insert(argsInTrans, CarID, new t_Decimal(CarTare.Value), new t_String("皮重来源：地磅入场皮重"));
                }
            };
            DBHelper.ExecInTrans(args, exec);
            SaleCarInBillID.Value = SaleCarInBillID_temp.Value;
            /*string strDatePath = GetPicturePath(enImagePathType.InBillPath, DateTime.Now);

            if (MonitoreImg1.Value != null)
            {
                string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image1.jpg");
                CommonHelper.SaveFile(strImagePath, MonitoreImg1.Value);
            }
            if (MonitoreImg2.Value != null)
            {
                string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image2.jpg");
                CommonHelper.SaveFile(strImagePath, MonitoreImg2.Value);
            }
            if (MonitoreImg3.Value != null)
            {
                string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image3.jpg");
                CommonHelper.SaveFile(strImagePath, MonitoreImg3.Value);
            }
            if (MonitoreImg4.Value != null)
            {
                string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image4.jpg");
                CommonHelper.SaveFile(strImagePath, MonitoreImg4.Value);
            }*/

            //更新版本号
            mServerVersion = Convert.ToInt64(DateTime.Now.ToString("hhmmss"));
        }

        public void GetCarNotOutBill(FactoryArgs args, t_BigID CarID,
            out t_DTSmall BillDateIn, out t_ID CalculateType, out t_BigID ItemID, out t_BigID CustomerID,
            out t_Decimal CarTare, out t_String Description, out t_ID ReceiveType, out t_String SaleCarInBillCode,
            out t_BigID SaleCarInBillID, out t_ID BillStatus, out t_Bool IsReaded)
        {
            IsReaded = new t_Bool(0);
            BillDateIn = new t_DTSmall();
            CalculateType = new t_ID();
            ItemID = new t_BigID();
            CustomerID = new t_BigID();
            CarTare = new t_Decimal();
            Description = new t_String();
            ReceiveType = new t_ID();
            SaleCarInBillCode = new t_String();
            SaleCarInBillID = new t_BigID();
            BillStatus = new t_ID();
            using (DataTable dtInBill = _DALSaleCarInOutBill.GetCarNotOutBill(args, CarID))
            {
                if (dtInBill.Rows.Count > 0)
                {
                    DataRow drInBill = dtInBill.Rows[0];
                    IsReaded.SetValueWithObject(1);
                    BillDateIn.SetValueWithObject(drInBill["BillDate"]);
                    ItemID.SetValueWithObject(drInBill["ItemID"]);
                    CustomerID.SetValueWithObject(drInBill["CustomerID"]);
                    CarTare.SetValueWithObject(drInBill["CarTare"]);
                    Description.SetValueWithObject(drInBill["Description"]);
                    SaleCarInBillCode.SetValueWithObject(drInBill["SaleCarInBillCode"]);
                    SaleCarInBillID.SetValueWithObject(drInBill["SaleCarInBillID"]);
                    BillStatus.SetValueWithObject(drInBill["BillStatus"]);

                    GetSaleCarPriceInfo(args, CarID, CustomerID, ItemID, out CalculateType, out ReceiveType);
                }
            }
        }

        public void InsertOnlyOutBill(FactoryArgs args, out t_BigID SaleCarOutBillID, out t_DTSmall BillDate, t_BigID CarID,
            t_BigID ItemID, t_BigID CustomerID, t_Float CarTare,
            t_ID ReceiveType, t_ID CalculateType, t_Decimal Price, t_Decimal Amount, t_Decimal TotalWeight,
            t_Decimal SuttleWeight, t_Decimal CustomerPayAmount, t_String Description,t_String CreateBy)
        {
            BillDate = new t_DTSmall();
            t_DTSmall BillDate_temp = new t_DTSmall(DateTime.Now);
            t_BigID SaleCarInBillID = new t_BigID();
            t_String SaleCarInBillCode = new t_String();
            t_String SaleCarOutBillCode = new t_String();
            t_BigID SaleCarOutBillID_temp = new t_BigID();
            SaleCarOutBillID = new t_BigID();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                this.InsertInBill(argsInTrans, out SaleCarInBillID, ref SaleCarInBillCode, ref BillDate_temp, CarID, ItemID, ReceiveType, CalculateType,
                    CarTare, CustomerID, Description, new t_ID(1),new t_ID(),new t_BigID(),new t_String());

                //自动审核出场订单
                this.InsertOutBill(argsInTrans, out SaleCarOutBillID_temp, ref SaleCarOutBillCode, ref BillDate_temp, SaleCarInBillID,
                    CarID, ReceiveType, CalculateType, Price, Amount, TotalWeight, SuttleWeight, CustomerPayAmount, Description, new t_ID(0), new t_ID(),
                    CreateBy, new t_BigID());
            };
            DBHelper.ExecInTrans(args, exec);
            BillDate.Value = BillDate_temp.Value;
            SaleCarOutBillID.Value = SaleCarOutBillID_temp.Value;
        }

        public void InsertOutBill(FactoryArgs args, out t_BigID SaleCarOutBillID, ref t_String SaleCarOutBillCode, ref t_DTSmall BillDate, t_BigID SaleCarInBillID, t_BigID CarID,
            t_ID ReceiveType, t_ID CalculateType, t_Decimal Price, t_Decimal Amount, t_Decimal TotalWeight,
            t_Decimal SuttleWeight, t_Decimal CustomerPayAmount, t_String Description, t_ID IsEmptyOut, t_ID UnAutoApprove,
            t_String CreateBy,t_BigID SaleCarOutBillIDFromClient)
        {
            UnAutoApprove.IsNullToZero();//无需自动审核
            IsEmptyOut.IsNullToZero();//是否空车出
            bool bolIsNeedCancel = false;//是否自动作废，空车入空车出的情况需要作废
            if (IsEmptyOut.Value == 1 || (Amount.Value == 0 && SuttleWeight.Value < 1000))
            {
                bolIsNeedCancel = true;
                Amount.Value = 0;
            }
            //SaleCarOutBillCode = new t_String();
            t_BigID CustomerID = new t_BigID();
            t_String CarNum = new t_String();
            t_ID SaleBillType = new t_ID();
            SaleCarOutBillID = new t_BigID();
            if (BillDate.Value == null)
            {
                BillDate = new t_DTSmall(DateTime.Now);
            }
            if (string.IsNullOrEmpty(CreateBy.Value))
            {
                CreateBy.Value = args.LoginName;
            }
            if (SaleCarInBillID.Value == null && SaleCarInBillID.Value == 0)
            {
                throw new Exception("该车辆未匹配到入场订单，请重新选择入场订单！");
            }
            if (CarID.Value == null || CarID.Value == 0)
            {
                throw new Exception("车牌号码不存在或者车牌号码为空，请重新选择车牌号码！");
            }
            //校验该入场单是否已出场
            using (DataTable dtOut = _DALSaleCarInOutBill.GetCarOutBillByInBillID(args, SaleCarInBillID))
            {
                if (dtOut.Rows.Count > 0)
                {
                    DataRow drOut = dtOut.Rows[0];
                    DateTime dtOutBillDate = Convert.ToDateTime(drOut["BillDate"]);
                    throw new Exception("该入场订单已生成出场记录，出场时间为【" + dtOutBillDate.ToString("yyyy-MM-dd HH:mm") + "】,请重新选择入场订单！");
                }
            }
            //校验该入场订单记录的车牌号码与当前输入的车牌是否一致
            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)
                {
                    DataRow drInBill = dtInBill.Rows[0];
                    long InCarID = LBConverter.ToInt64(drInBill["CarID"]);
                    decimal decCarTare = LBConverter.ToDecimal(drInBill["CarTare"]);
                    CustomerID.SetValueWithObject(drInBill["CustomerID"]);
                    CarNum.SetValueWithObject(drInBill["CarNum"]);
                    SaleBillType.SetValueWithObject(drInBill["SaleBillType"]);
                    if (CarID.Value != InCarID)
                    {
                        throw new Exception("输入的车牌号码与入场订单车牌号码不一致！");
                    }

                    if (SaleBillType.Value == 1)
                    {
                        throw new Exception("该车辆为油车，请点击【汽油采购】模块进行操作！");
                    }

                    if (CalculateType.Value==0 && SaleBillType.Value==1 && TotalWeight.Value - decCarTare != SuttleWeight.Value)//重车时出现皮重-毛重！=净重的异常情况，需要临时纠正净重值，同时写入日志
                    {
                        this._IBLLDbErrorLog.Insert(args,
                            new t_String("服务器重车异常：TotalWeight=" + TotalWeight.Value.ToString() + " CarTare=" + decCarTare.ToString() + " SuttleWeight=" + SuttleWeight.Value.ToString() + " SaleCarInBillID=" + SaleCarInBillID.Value.ToString()));
                        SuttleWeight.Value = TotalWeight.Value - decCarTare;
                        Amount.Value = Price.Value * SuttleWeight.Value;
                    }
                }
            }

            //校验该车辆是否存在多张入场未出场的订单
            using (DataTable dtExistsNotOut = _DALSaleCarInOutBill.ExistsNotOut(args, CarID))
            {
                if (dtExistsNotOut.Rows.Count > 1)
                {
                    throw new Exception("该车辆存在【" + dtExistsNotOut.Rows.Count + "】张入场但是未出场的订单！无法出场！");
                }
            }

            if (string.IsNullOrEmpty(SaleCarOutBillCode.Value))
            {
                //读取出场单号前缀
                t_String SysConfigFieldName = new t_String("SaleOutBillCode");
                t_String SysConfigValue;
                _IBLLDbSysConfig.GetConfigValue(args, SysConfigFieldName, out SysConfigValue);
                if (SysConfigValue.Value == "")
                {
                    SysConfigValue.Value = "XS";
                }

                //生成编码
                string strBillFont = SysConfigValue.Value.TrimEnd() + DateTime.Now.ToString("yyyyMM") + "-";
                using (DataTable dtBillCode = _DALSaleCarInOutBill.GetMaxOutBillCode(args, strBillFont))
                {
                    if (dtBillCode.Rows.Count > 0)
                    {
                        DataRow drBillCode = dtBillCode.Rows[0];
                        int iIndex = 1;
                        string strIndex = "";
                        if (drBillCode["SaleCarOutBillCode"].ToString().TrimEnd().Contains(strBillFont))
                        {
                            iIndex = Convert.ToInt32(drBillCode["SaleCarOutBillCode"].ToString().TrimEnd().Replace(strBillFont, ""));
                            iIndex += 1;
                            if (iIndex < 10)
                            {
                                strIndex = "0000" + iIndex.ToString();
                            }
                            else if (iIndex < 100)
                            {
                                strIndex = "000" + iIndex.ToString();
                            }
                            else if (iIndex < 1000)
                            {
                                strIndex = "00" + iIndex.ToString();
                            }
                            else if (iIndex < 10000)
                            {
                                strIndex = "0" + iIndex.ToString();
                            }
                            else
                            {
                                strIndex = iIndex.ToString();
                            }
                            SaleCarOutBillCode.SetValueWithObject(strBillFont + strIndex);
                        }
                        else
                        {
                            SaleCarOutBillCode.SetValueWithObject(strBillFont + "-00001");
                        }
                    }
                    else
                    {
                        SaleCarOutBillCode.SetValueWithObject(strBillFont + "00001");
                    }
                }
            }

            t_String SaleCarOutBillCode_temp = new t_String(SaleCarOutBillCode.Value);
            t_BigID SaleCarOutBillID_temp = new t_BigID();
            t_DTSmall BillDate_temp = new t_DTSmall(BillDate.Value);
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALSaleCarInOutBill.InsertOutBill(argsInTrans, out SaleCarOutBillID_temp, SaleCarOutBillCode_temp, SaleCarInBillID, CarID, BillDate_temp,
                    TotalWeight, SuttleWeight, Price, Amount, ReceiveType, CalculateType, Description, CreateBy, SaleCarOutBillIDFromClient);

                if (CustomerPayAmount.Value > 0)
                {
                    t_BigID ReceiveBillHeaderID;
                    t_String ReceiveBillCode;
                    _IBLLRPReceiveBillHeader.Insert(argsInTrans, out ReceiveBillHeaderID, out ReceiveBillCode, BillDate_temp, CustomerID,
                        CustomerPayAmount, new t_String("来源：司机支付磅单现金，车号：" + CarNum.Value + " 磅单号：" + SaleCarOutBillCode_temp.Value),
                        SaleCarInBillID, new t_BigID(), new t_ID(0), new t_Decimal(0), new t_Decimal(0), new t_Decimal(0),new t_BigID(1));
                    if (bolIsNeedCancel)
                    {
                        this.Cancel(argsInTrans, SaleCarInBillID, new t_String("空车入空车出"));
                    }
                    else
                    {
                        _IBLLRPReceiveBillHeader.Approve(argsInTrans, ReceiveBillHeaderID);//审核收款单
                    }
                }

                if (bolIsNeedCancel)
                {
                    this.Cancel(argsInTrans, SaleCarInBillID, new t_String("空车入空车出"));
                }
                else if (UnAutoApprove.Value == 0)
                {
                    //自动审核出场订单
                    this.Approve(argsInTrans, SaleCarInBillID, BillDate_temp);
                }
            };
            DBHelper.ExecInTrans(args, exec);
            SaleCarOutBillID.Value = SaleCarOutBillID_temp.Value;
            SaleCarOutBillCode.Value = SaleCarOutBillCode_temp.Value;

            //更新版本号
            mServerVersion = Convert.ToInt64(DateTime.Now.ToString("hhmmss"));
        }

        public void UpdateInOutBill(FactoryArgs args, t_BigID SaleCarInBillID, t_BigID CarID, t_BigID ItemID, t_BigID CustomerID, t_Decimal Price,
            t_Decimal Amount, t_String Description)
        {
            using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(args, SaleCarInBillID))
            {
                DataRow drBill = dtBill.Rows[0];
                t_ID BillStatus = new t_ID(drBill["BillStatus"]);
                t_ID IsCancel = new t_ID(drBill["IsCancel"]);

                if (IsCancel.Value == 1)
                {
                    throw new Exception("该榜单已作废，无法修改单据信息！如需修改，请先反作废后再修改！");
                }

                if (BillStatus.Value == 2)//如果榜单已审核则先反审核单据再改单
                {
                    this.UnApprove(args, SaleCarInBillID);
                }
            }

            _DALSaleCarInOutBill.UpdateInOutBill(args, SaleCarInBillID, CarID,ItemID, CustomerID, Price, Amount, Description);
        }

        public void SaveInSalesCameraImage(FactoryArgs args, t_BigID SaleCarInBillID,
            t_Image MonitoreImg1, t_Image MonitoreImg2, t_Image MonitoreImg3, t_Image MonitoreImg4)
        {
            try
            {
                string strDatePath = GetPicturePath(enImagePathType.InBillPath, DateTime.Now);

                if (MonitoreImg1.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image1.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg1.Value);
                }
                if (MonitoreImg2.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image2.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg2.Value);
                }
                if (MonitoreImg3.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image3.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg3.Value);
                }
                if (MonitoreImg4.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image4.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg4.Value);
                }
            }
            catch (Exception ex)
            {
                this._IBLLDbErrorLog.Insert(args,
                    new t_String("服务器端，保存入场图片时报错，入场单号：" + SaleCarInBillID.Value.ToString() + "\n错误信息：" + ex.Message));
                throw ex;
            }
        }

        public void SaveOutSalesCameraImage(FactoryArgs args, t_BigID SaleCarOutBillID,
            t_Image MonitoreImg1, t_Image MonitoreImg2, t_Image MonitoreImg3, t_Image MonitoreImg4)
        {
            try
            {
                string strDatePath = GetPicturePath(enImagePathType.OutBillPath, DateTime.Now);

                if (MonitoreImg1.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image1.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg1.Value);
                }
                if (MonitoreImg2.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image2.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg2.Value);
                }
                if (MonitoreImg3.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image3.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg3.Value);
                }
                if (MonitoreImg4.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image4.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg4.Value);
                }
            }
            catch (Exception ex)
            {
                this._IBLLDbErrorLog.Insert(args,
                    new t_String("服务器端，保存出场图片时报错，出场单号：" + SaleCarOutBillID.Value.ToString() + "\n错误信息：" + ex.Message));
                throw ex;
            }
        }

        public void SaveScreenImage(FactoryArgs args, t_BigID SaleCarOutBillID,t_Image ScreenPicture)
        {
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory;
                string strCameraPath = Path.Combine(strPath, "LBCameraPicture");
                if (!Directory.Exists(strCameraPath))
                {
                    Directory.CreateDirectory(strCameraPath);
                }
                string strInBillPath = Path.Combine(strCameraPath, "ScreenImage");
                if (!Directory.Exists(strInBillPath))
                {
                    Directory.CreateDirectory(strInBillPath);
                }
                
                if (ScreenPicture.Value != null)
                {
                    string strImagePath = Path.Combine(strInBillPath, SaleCarOutBillID.Value.ToString() + "_Image.jpg");
                    CommonHelper.SaveFile(strImagePath, ScreenPicture.Value);
                }
            }
            catch (Exception ex)
            {
                this._IBLLDbErrorLog.Insert(args,
                    new t_String("服务器端，保存截屏图时报错，出场单号：" + SaleCarOutBillID.Value.ToString() + "\n错误信息：" + ex.Message));
                throw ex;
            }
        }

        public void GetCameraImage(FactoryArgs args, t_BigID SaleCarInBillID,
            out t_Image InMonitoreImg1, out t_Image InMonitoreImg2, out t_Image InMonitoreImg3, out t_Image InMonitoreImg4,
            out t_Image OutMonitoreImg1, out t_Image OutMonitoreImg2, out t_Image OutMonitoreImg3, out t_Image OutMonitoreImg4)
        {
            InMonitoreImg1 = new t_Image();
            InMonitoreImg2 = new t_Image();
            InMonitoreImg3 = new t_Image();
            InMonitoreImg4 = new t_Image();
            OutMonitoreImg1 = new t_Image();
            OutMonitoreImg2 = new t_Image();
            OutMonitoreImg3 = new t_Image();
            OutMonitoreImg4 = new t_Image();

            t_BigID SaleCarOutBillID = new t_BigID();
            t_DTSmall BillDateIn = new t_DTSmall();
            t_DTSmall BillDateOut = new t_DTSmall();
            using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(args, SaleCarInBillID))
            {
                if (dtBill.Rows.Count > 0)
                {
                    DataRow drBill = dtBill.Rows[0];
                    SaleCarOutBillID.SetValueWithObject(drBill["SaleCarOutBillID"]);
                    BillDateIn.SetValueWithObject(drBill["BillDateIn"]);
                    BillDateOut.SetValueWithObject(drBill["BillDateOut"]);
                }
            }

            //读取入场图片
            if (BillDateIn.Value != null)
            {
                string strInPath = GetPicturePath(enImagePathType.InBillPath, (DateTime)BillDateIn.Value);
                string strPathImg1 = Path.Combine(strInPath, SaleCarInBillID.Value.ToString() + "_Image1.jpg");
                string strPathImg2 = Path.Combine(strInPath, SaleCarInBillID.Value.ToString() + "_Image2.jpg");
                string strPathImg3 = Path.Combine(strInPath, SaleCarInBillID.Value.ToString() + "_Image3.jpg");
                string strPathImg4 = Path.Combine(strInPath, SaleCarInBillID.Value.ToString() + "_Image4.jpg");

                if (File.Exists(strPathImg1))
                {
                    InMonitoreImg1.SetValueWithObject(CommonHelper.ReadFile(strPathImg1));
                }
                if (File.Exists(strPathImg2))
                {
                    InMonitoreImg2.SetValueWithObject(CommonHelper.ReadFile(strPathImg2));
                }
                if (File.Exists(strPathImg3))
                {
                    InMonitoreImg3.SetValueWithObject(CommonHelper.ReadFile(strPathImg3));
                }
                if (File.Exists(strPathImg4))
                {
                    InMonitoreImg4.SetValueWithObject(CommonHelper.ReadFile(strPathImg4));
                }
            }
            //读取出场图片
            if (BillDateOut.Value != null)
            {
                string strOutPath = GetPicturePath(enImagePathType.OutBillPath, (DateTime)BillDateOut.Value);
                string strPathImg1 = Path.Combine(strOutPath, SaleCarOutBillID.Value.ToString() + "_Image1.jpg");
                string strPathImg2 = Path.Combine(strOutPath, SaleCarOutBillID.Value.ToString() + "_Image2.jpg");
                string strPathImg3 = Path.Combine(strOutPath, SaleCarOutBillID.Value.ToString() + "_Image3.jpg");
                string strPathImg4 = Path.Combine(strOutPath, SaleCarOutBillID.Value.ToString() + "_Image4.jpg");

                if (File.Exists(strPathImg1))
                {
                    OutMonitoreImg1.SetValueWithObject(CommonHelper.ReadFile(strPathImg1));
                }
                if (File.Exists(strPathImg2))
                {
                    OutMonitoreImg2.SetValueWithObject(CommonHelper.ReadFile(strPathImg2));
                }
                if (File.Exists(strPathImg3))
                {
                    OutMonitoreImg3.SetValueWithObject(CommonHelper.ReadFile(strPathImg3));
                }
                if (File.Exists(strPathImg4))
                {
                    OutMonitoreImg4.SetValueWithObject(CommonHelper.ReadFile(strPathImg4));
                }
            }
        }

        private string GetPicturePath(enImagePathType pathType, DateTime dtDate)
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory;
            string strCameraPath = Path.Combine(strPath, "LBCameraPicture");
            if (!Directory.Exists(strCameraPath))
            {
                Directory.CreateDirectory(strCameraPath);
            }

            string strPicFile = pathType == enImagePathType.InBillPath ? "InBillPicture" : "OutBillPicture";
            string strInBillPath = Path.Combine(strCameraPath, strPicFile);
            if (!Directory.Exists(strInBillPath))
            {
                Directory.CreateDirectory(strInBillPath);
            }
            string strDatePath = Path.Combine(strInBillPath, dtDate.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(strDatePath))
            {
                Directory.CreateDirectory(strDatePath);
            }
            return strDatePath;
        }

        public void Approve(FactoryArgs args, t_BigID SaleCarInBillID, t_DTSmall ApproveTime)
        {
            if (ApproveTime.Value == null)
                ApproveTime = new t_DTSmall(DateTime.Now);
            t_BigID CustomerID = new t_BigID();
            t_Decimal Amount = new t_Decimal(0);
            t_ID ReceiveType = new t_ID(0);
            using (DataTable dtOutBill = _DALSaleCarInOutBill.GetCarOutBillByInBillID(args, SaleCarInBillID))
            {
                if (dtOutBill.Rows.Count == 0)//校验是否已生成出场订单
                {
                    throw new Exception("该订单未有出场记录，无法审核！");
                }
                else
                {
                    Amount.SetValueWithObject(dtOutBill.Rows[0]["Amount"]);
                }
            }

            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                {
                    DataRow drBill = dtInBill.Rows[0];
                    CustomerID.SetValueWithObject(drBill["CustomerID"]);
                    ReceiveType.SetValueWithObject(drBill["ReceiveType"]);
                    int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                    if (iBillStatus == 2)
                    {
                        throw new Exception("该订单已审核，无法再执行审核！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该订单已作废，无法审核！");
                    }
                }
            }

            bool bolVerifyCredit = true;
            //判断当前单据是现金支付还是挂账
            if (ReceiveType.Value == 0)//现金
            {

            }
            else if (ReceiveType.Value == 1 || ReceiveType.Value == 2)//预付和挂账
            {
                bolVerifyCredit = VerifyIsOverRangeCredut(args, CustomerID, Amount);//校验信用额度
            }

            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALSaleCarInOutBill.Approve(argsInTrans, SaleCarInBillID, ApproveTime);

                //if (ReceiveType.Value == 1 || ReceiveType.Value == 2)//预付或者挂账情况下需求更新客户信息的冲销金额
                {
                    _DALSaleCarInOutBill.UpdateCustomerSalesAmount(argsInTrans, CustomerID, Amount);
                }
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void UnApprove(FactoryArgs args, t_BigID SaleCarInBillID)
        {
            t_BigID CustomerID = new t_BigID();
            t_Decimal Amount = new t_Decimal(0);
            t_ID ReceiveType = new t_ID(0);
            using (DataTable dtOutBill = _DALSaleCarInOutBill.GetCarOutBillByInBillID(args, SaleCarInBillID))
            {
                if (dtOutBill.Rows.Count > 0)
                {
                    Amount.SetValueWithObject(dtOutBill.Rows[0]["Amount"]);
                }
            }

            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                {
                    DataRow drBill = dtInBill.Rows[0];
                    CustomerID.SetValueWithObject(drBill["CustomerID"]);
                    ReceiveType.SetValueWithObject(drBill["ReceiveType"]);
                    int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                    if (iBillStatus == 1)
                    {
                        throw new Exception("该订单未审核，无法取消审核！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该订单已作废，无法取消审核！");
                    }
                }
            }
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALSaleCarInOutBill.UnApprove(argsInTrans, SaleCarInBillID);

                //if (ReceiveType.Value == 1 || ReceiveType.Value == 2)//挂账情况下需求退回重新金额
                {
                    Amount.SetValueWithObject(-Amount.Value);
                    _DALSaleCarInOutBill.UpdateCustomerSalesAmount(argsInTrans, CustomerID, Amount);
                }
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void Cancel(FactoryArgs args, t_BigID SaleCarInBillID, t_String CancelDesc)
        {
            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                {
                    DataRow drBill = dtInBill.Rows[0];
                    int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                    if (iBillStatus == 2)
                    {
                        throw new Exception("该订单已审核，无法执行作废！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该订单已作废，无法再执行作废！");
                    }
                }
            }
            _DALSaleCarInOutBill.Cancel(args, SaleCarInBillID, CancelDesc);
        }

        public void UnCancel(FactoryArgs args, t_BigID SaleCarInBillID)
        {
            t_DTSmall CancelByDate = new t_DTSmall(DateTime.Now);
            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                {
                    DataRow drBill = dtInBill.Rows[0];
                    int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                    CancelByDate = drBill["CancelByDate"] == DBNull.Value ?
                       new t_DTSmall(drBill["BillDate"]) : new t_DTSmall(drBill["CancelByDate"]);
                    CancelByDate.Value = ((DateTime)CancelByDate.Value).AddHours(1);//自动延长一个小时的作废期限
                    if (iBillStatus == 2)
                    {
                        throw new Exception("该订单已审核，无法执行取消作废！");
                    }
                    if (!bolIsCancel)
                    {
                        throw new Exception("该订单未作废，无法执行取消作废！");
                    }
                }
            }
            _DALSaleCarInOutBill.UnCancel(args, SaleCarInBillID, CancelByDate);
        }

        //校验是否超出信用额度
        public bool VerifyIsOverRangeCredut(FactoryArgs args, t_BigID CustomerID, t_Decimal SalesAmount)
        {
            bool bolIsPass = true;
            using (DataTable dtCustomer = _DALSaleCarInOutBill.GetCustomer(args, CustomerID))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    DataRow drCustomer = dtCustomer.Rows[0];
                    t_Decimal TotalReceivedAmount = new t_Decimal(drCustomer["TotalReceivedAmount"]);//预收总金额
                    TotalReceivedAmount.IsNullToZero();
                    t_Decimal SalesReceivedAmount = new t_Decimal(drCustomer["SalesReceivedAmount"]);//预收已冲销金额
                    SalesReceivedAmount.IsNullToZero();
                    t_Decimal CreditAmount = new t_Decimal(drCustomer["CreditAmount"]);//信用额度金额
                    CreditAmount.IsNullToZero();
                    t_Bool IsAllowOverFul = new t_Bool(drCustomer["IsAllowOverFul"]);//是否允许超额提货
                    t_Bool IsForbid = new t_Bool(drCustomer["IsForbid"]);
                    t_ID ReceiveType = new t_ID();
                    ReceiveType.SetValueWithObject(drCustomer["ReceiveType"]);

                    if (ReceiveType.Value == 1)//预付
                    {
                        if (TotalReceivedAmount.Value < SalesReceivedAmount.Value + SalesAmount.Value)
                        {
                            throw new Exception("该余额不足，请及时充值，否则无法生成磅单！");
                        }
                    }
                    else
                    {
                        if (IsForbid.Value == 1)//车辆是否限制
                        {
                            if (IsAllowOverFul.Value == 1)//允许超额提货
                            {
                                //判断是否已超出信用额度
                                if (SalesReceivedAmount.Value + SalesAmount.Value - TotalReceivedAmount.Value > CreditAmount.Value)
                                {
                                    bolIsPass = false;
                                    throw new Exception("该用户扣除当前车款[" + ((decimal)SalesAmount.Value).ToString("0.00") + "]元后，已经超出信用额度，请及时充值，否则无法生成磅单！");
                                }
                                else
                                {
                                    bolIsPass = true;
                                }
                            }
                            else
                            {
                                //不允许超额提货，只要余额不足，就不能提货
                                if (SalesReceivedAmount.Value + SalesAmount.Value > TotalReceivedAmount.Value)
                                {
                                    bolIsPass = false;
                                    throw new Exception("该用户当前车款为[" + ((decimal)SalesAmount.Value).ToString("0.00") + "]元，而充值余额为[" + ((decimal)(TotalReceivedAmount.Value - SalesReceivedAmount.Value)).ToString("0.00") + "]，请及时充值，否则无法生成磅单！");
                                }
                                else
                                {
                                    bolIsPass = true;
                                }
                            }
                        }
                        else
                        {
                            // 判断是否已超出信用额度
                            if (SalesReceivedAmount.Value + SalesAmount.Value - TotalReceivedAmount.Value > CreditAmount.Value)
                            {
                                bolIsPass = false;
                                throw new Exception("该用户扣除当前车款[" + ((decimal)SalesAmount.Value).ToString("0.00") + "]元后，已经超出信用额度，请及时充值，否则无法生成磅单！");
                            }
                            else
                            {
                                bolIsPass = true;
                            }
                            //不允许超额提货，只要余额不足，就不能提货
                            /*if (SalesReceivedAmount.Value + SalesAmount.Value > TotalReceivedAmount.Value)
                            {
                                bolIsPass = false;
                                throw new Exception("该用户当前车款为[" + ((decimal)SalesAmount.Value).ToString("0.00") + "]元，而充值余额为[" + ((decimal)(TotalReceivedAmount.Value - SalesReceivedAmount.Value)).ToString("0.00") + "]，请及时充值，否则无法生成磅单！");
                            }
                            else
                            {
                                bolIsPass = true;
                            }*/
                        }
                    }
                }
            }
            return bolIsPass;
        }

        //通过车牌号、物料名称以及客户名称读取默认的计价方式、收款方式
        public void GetSaleCarPriceInfo(FactoryArgs args, t_BigID CarID, t_BigID CustomerID, t_BigID ItemID, out t_ID CalculateType, out t_ID ReceiveType)
        {
            CalculateType = new t_ID(0);//默认按重量计算
            ReceiveType = new t_ID(0);//默认现金支付

            //_DALSaleCarInOutBill.ReadCarID(args, CarNum, out CarID);
            //_DALSaleCarInOutBill.ReadCustomerID(args, CustomerName, out CustomerID);
            //_DALSaleCarInOutBill.ReadItemID(args, ItemName, out ItemID);

            //读取客户默认收款方式
            if (CustomerID.Value > 0)
            {
                _DALSaleCarInOutBill.ReadReceiveType(args, CustomerID, out ReceiveType);
            }

            //读取计价方式
            using (DataTable dtDetailCar = _DALSaleCarInOutBill.ReadModifyDetailByCar(args, CarID, CustomerID, ItemID))
            {
                if (dtDetailCar.Rows.Count > 0)
                {
                    DataRow drDetailCar = dtDetailCar.Rows[0];
                    CalculateType.SetValueWithObject(drDetailCar["CalculateType"]);
                }
                else
                {
                    using (DataTable dtDetail = _DALSaleCarInOutBill.ReadModifyDetailByItem(args, CustomerID, ItemID))
                    {
                        if (dtDetail.Rows.Count > 0)
                        {
                            DataRow drDetail = dtDetail.Rows[0];
                            CalculateType.SetValueWithObject(drDetail["CalculateType"]);
                        }
                    }
                }
            }
        }

        //记录小票打印次数
        public void UpdateInPrintCount(FactoryArgs args, t_BigID SaleCarInBillID)
        {
            _DALSaleCarInOutBill.UpdateInPrintCount(args, SaleCarInBillID);
        }

        //记录磅单打印次数
        public void UpdateOutPrintCount(FactoryArgs args, t_BigID SaleCarOutBillID)
        {
            _DALSaleCarInOutBill.UpdateOutPrintCount(args, SaleCarOutBillID);
        }

        public void ReadSaleInfo(FactoryArgs args,
            out t_ID InsideCarCount, out t_Decimal SalesTotalWeight, out t_ID TotalCar)
        {
            _DALSaleCarInOutBill.GetInsideCarCount(args, out InsideCarCount);
            _DALSaleCarInOutBill.GetTodayTotalWeight(args, out SalesTotalWeight, out TotalCar);
        }

        public void InsertChangeBill(FactoryArgs args, out t_BigID SaleCarChangeBillID,
            t_BigID SaleCarInBillID, t_String ChangeDesc,
            t_String ChangeDetail, t_Bool IsPayMoney, t_Decimal PayMoney,
            t_BigID NewCustomerID, t_BigID NewItemID, t_BigID NewCarID, t_ID NewReceiveType, t_ID NewCalculateType,
            t_Decimal NewPrice, t_Decimal NewAmount,t_String NewDescription)
        {
            t_DTSmall ChangeDate = new t_DTSmall(DateTime.Now);
            t_String ChangeBy = new t_String(args.LoginName);
            SaleCarChangeBillID = new t_BigID();
            t_BigID TempSaleCarChangeBillID = new t_BigID();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_BigID SaleCarOutBillID = new t_BigID(0);
                using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(argsInTrans, SaleCarInBillID))
                {
                    DataRow drBill = dtBill.Rows[0];
                    t_ID BillStatus = new t_ID(drBill["BillStatus"]);
                    SaleCarOutBillID.SetValueWithObject(drBill["SaleCarOutBillID"]);

                    if (SaleCarOutBillID.Value > 0)//已出场的单据不允许走改单流程
                    {
                        throw new Exception("该单据已生成出场单，无法变更，请与相关负责人联系！");
                    }

                    if (BillStatus.Value == 2)
                    {
                        //先反审核磅单
                        _DALSaleCarInOutBill.UnApprove(argsInTrans, SaleCarInBillID);
                    }
                    t_ID IsCancel = new t_ID(drBill["IsCancel"]);
                    if (IsCancel.Value == 0)
                    {
                        //作废磅单
                        _DALSaleCarInOutBill.Cancel(argsInTrans, SaleCarInBillID, ChangeDesc);
                    }
                }

                t_Decimal CustomerPayAmount = new t_Decimal(0);
                //磅单是否存在现金充值单，如果存在则作废
                using (DataTable dtBill = _DALSaleCarInOutBill.GetRPReceiveBillHeader(argsInTrans, SaleCarInBillID))
                {
                    foreach (DataRow drBill in dtBill.Rows)
                    {
                        t_BigID ReceiveBillHeaderID = new t_BigID(drBill["ReceiveBillHeaderID"]);
                        t_Bool IsApprove = new t_Bool(drBill["IsApprove"]);
                        t_Bool IsCancel = new t_Bool(drBill["IsCancel"]);
                        CustomerPayAmount.SetValueWithObject(drBill["ReceiveAmount"]);
                        if (IsApprove.Value == 1)//反审核
                        {
                            _IBLLRPReceiveBillHeader.UnApprove(argsInTrans, ReceiveBillHeaderID);
                        }
                        if (IsCancel.Value == 0)//反作废
                        {
                            _IBLLRPReceiveBillHeader.Cancel(argsInTrans, ReceiveBillHeaderID);
                        }
                    }
                }

                using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(argsInTrans, SaleCarInBillID))
                {
                    DataRow drBill = dtBill.Rows[0];

                    //生成新的入场记录
                    t_BigID NewSaleCarInBillID;
                    t_String NewSaleCarInBillCode;
                    t_String NewSaleCarOutBillCode;
                    t_DTSmall NewBillDate = new t_DTSmall(drBill["BillDateIn"]);
                    this.InsertInBill(argsInTrans, out NewSaleCarInBillID, ref NewSaleCarInBillCode, ref NewBillDate,
                        NewCarID, NewItemID, NewReceiveType,
                        NewCalculateType, new t_Float(drBill["CarTare"]), NewCustomerID,
                        NewDescription, new t_ID(), new t_ID(),new t_BigID(),new t_String());

                    if (SaleCarOutBillID.Value > 0)//有出场磅单
                    {
                        t_BigID NewSaleCarOutBillID;
                        if (IsPayMoney.Value == 1 && PayMoney.Value > 0)
                        {
                            t_BigID ReceiveBillHeaderID;
                            t_String ReceiveBillCode;
                            _IBLLRPReceiveBillHeader.Insert(argsInTrans, out ReceiveBillHeaderID, out ReceiveBillCode,
                                    new t_DTSmall(DateTime.Now), NewCustomerID, PayMoney, new t_String("充值来源：车号" + drBill["CarNum"].ToString().TrimEnd() + "现金充值"),
                                    NewSaleCarInBillID, new t_BigID(), new t_ID(0), new t_Decimal(0), new t_Decimal(0), new t_Decimal(0), new t_BigID(1));
                            _IBLLRPReceiveBillHeader.Approve(argsInTrans, ReceiveBillHeaderID);
                        }

                        //生成出场记录
                        this.InsertOutBill(argsInTrans, out NewSaleCarOutBillID, ref NewSaleCarOutBillCode, ref NewBillDate, NewSaleCarInBillID, new t_BigID(drBill["CarID"]),
                           NewReceiveType, NewCalculateType, NewPrice, NewAmount, new t_Decimal(drBill["TotalWeight"]), new t_Decimal(drBill["SuttleWeight"]),
                           CustomerPayAmount, NewDescription, new t_ID(0), new t_ID(),new t_String(), new t_BigID());
                    }
                }

                _DALSaleCarInOutBill.InsertChangeBill(argsInTrans, out TempSaleCarChangeBillID, SaleCarInBillID,
                        ChangeDate, ChangeBy, ChangeDesc, ChangeDetail);

            };
            DBHelper.ExecInTrans(args, exec);
            SaleCarChangeBillID.Value = TempSaleCarChangeBillID.Value;
        }

        public void CopySaleBill(FactoryArgs args, t_BigID SaleCarInBillID, t_Decimal NewTotalWeight,
            out t_BigID NewSaleCarInBillID, out t_BigID NewSaleCarOutBillID, out t_String NewSaleCarInBillCode, out t_String NewSaleCarOutBillCode)
        {
            NewSaleCarOutBillID = new t_BigID();
            NewSaleCarInBillID = new t_BigID();
            NewSaleCarInBillCode = new t_String();
            NewSaleCarOutBillCode = new t_String();
            t_String NewSaleCarInBillCode_temp = new t_String();
            t_String NewSaleCarOutBillCode_temp = new t_String();
            t_BigID NewSaleCarInBillID_temp = new t_BigID();
            t_BigID NewSaleCarOutBillID_temp = new t_BigID();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_Decimal CustomerPayAmount = new t_Decimal(0);
                using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(argsInTrans, SaleCarInBillID))
                {
                    DataRow drBill = dtBill.Rows[0];
                    t_BigID SaleCarOutBillID = new t_BigID(drBill["SaleCarOutBillID"]);
                    t_DTSmall BillDateIn = new t_DTSmall(drBill["BillDateIn"]);
                    t_DTSmall BillDateOut = new t_DTSmall(drBill["BillDateOut"]);

                    //生成新的入场记录

                    t_DTSmall NewBillDate = new t_DTSmall();
                    this.InsertInBill(argsInTrans, out NewSaleCarInBillID_temp, ref NewSaleCarInBillCode_temp, ref NewBillDate,
                        new t_BigID(drBill["CarID"]), new t_BigID(drBill["ItemID"]), new t_ID(drBill["ReceiveType"]),
                        new t_ID(drBill["CalculateType"]), new t_Float(drBill["CarTare"]), new t_BigID(drBill["CustomerID"]),
                        new t_String(drBill["Description"]), new t_ID(), new t_ID(),new t_BigID(),new t_String());

                    #region -- 复制原单的入场图片 --
                    string strDatePath = GetPicturePath(enImagePathType.InBillPath, (DateTime)BillDateIn.Value);
                    string strInBillImage1_Old = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image1.jpg");
                    string strInBillImage2_Old = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image2.jpg");
                    string strInBillImage3_Old = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image3.jpg");
                    string strInBillImage4_Old = Path.Combine(strDatePath, SaleCarInBillID.Value.ToString() + "_Image4.jpg");

                    string strDatePath_New = GetPicturePath(enImagePathType.InBillPath, (DateTime)NewBillDate.Value);
                    string strInBillImage1_New = Path.Combine(strDatePath_New, NewSaleCarInBillID_temp.Value.ToString() + "_Image1.jpg");
                    string strInBillImage2_New = Path.Combine(strDatePath_New, NewSaleCarInBillID_temp.Value.ToString() + "_Image2.jpg");
                    string strInBillImage3_New = Path.Combine(strDatePath_New, NewSaleCarInBillID_temp.Value.ToString() + "_Image3.jpg");
                    string strInBillImage4_New = Path.Combine(strDatePath_New, NewSaleCarInBillID_temp.Value.ToString() + "_Image4.jpg");

                    if (File.Exists(strInBillImage1_Old) && !File.Exists(strInBillImage1_New))
                    {
                        File.Copy(strInBillImage1_Old, strInBillImage1_New);
                    }
                    if (File.Exists(strInBillImage2_Old) && !File.Exists(strInBillImage2_New))
                    {
                        File.Copy(strInBillImage2_Old, strInBillImage2_New);
                    }
                    if (File.Exists(strInBillImage3_Old) && !File.Exists(strInBillImage3_New))
                    {
                        File.Copy(strInBillImage3_Old, strInBillImage3_New);
                    }
                    if (File.Exists(strInBillImage4_Old) && !File.Exists(strInBillImage4_New))
                    {
                        File.Copy(strInBillImage4_Old, strInBillImage4_New);
                    }

                    #endregion -- 复制原单的入场图片 --

                    if (SaleCarOutBillID.Value > 0)//有出场磅单
                    {
                        decimal decPrice = LBConverter.ToDecimal(drBill["Price"]);
                        decimal decTare = LBConverter.ToDecimal(drBill["CarTare"]);
                        decimal decSuttleWeight = (decimal)NewTotalWeight.Value - decTare;
                        decimal decNewAmount = decPrice * decSuttleWeight;
                        //生成出场记录
                        this.InsertOutBill(argsInTrans, out NewSaleCarOutBillID_temp, ref NewSaleCarOutBillCode_temp, ref NewBillDate, NewSaleCarInBillID_temp, new t_BigID(drBill["CarID"]),
                           new t_ID(drBill["ReceiveType"]), new t_ID(drBill["CalculateType"]), new t_Decimal(drBill["Price"]), new t_Decimal(decNewAmount), NewTotalWeight, new t_Decimal(decSuttleWeight),
                           CustomerPayAmount, new t_String(drBill["Description"]), new t_ID(0), new t_ID(),new t_String(), new t_BigID());

                        #region -- 复制原单的入场图片 --
                        string strDateOutPath = GetPicturePath(enImagePathType.OutBillPath, (DateTime)BillDateOut.Value);
                        string strOutBillImage1_Old = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image1.jpg");
                        string strOutBillImage2_Old = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image2.jpg");
                        string strOutBillImage3_Old = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image3.jpg");
                        string strOutBillImage4_Old = Path.Combine(strDatePath, SaleCarOutBillID.Value.ToString() + "_Image4.jpg");

                        string strDateOutPath_New = GetPicturePath(enImagePathType.OutBillPath, (DateTime)NewBillDate.Value);
                        string strOutBillImage1_New = Path.Combine(strDateOutPath_New, NewSaleCarOutBillID_temp.Value.ToString() + "_Image1.jpg");
                        string strOutBillImage2_New = Path.Combine(strDateOutPath_New, NewSaleCarOutBillID_temp.Value.ToString() + "_Image2.jpg");
                        string strOutBillImage3_New = Path.Combine(strDateOutPath_New, NewSaleCarOutBillID_temp.Value.ToString() + "_Image3.jpg");
                        string strOutBillImage4_New = Path.Combine(strDateOutPath_New, NewSaleCarOutBillID_temp.Value.ToString() + "_Image4.jpg");

                        if (File.Exists(strOutBillImage1_Old) && !File.Exists(strOutBillImage1_New))
                        {
                            File.Copy(strOutBillImage1_Old, strOutBillImage1_New);
                        }
                        if (File.Exists(strOutBillImage2_Old) && !File.Exists(strOutBillImage2_New))
                        {
                            File.Copy(strOutBillImage2_Old, strOutBillImage2_New);
                        }
                        if (File.Exists(strOutBillImage3_Old) && !File.Exists(strOutBillImage3_New))
                        {
                            File.Copy(strOutBillImage3_Old, strOutBillImage3_New);
                        }
                        if (File.Exists(strOutBillImage4_Old) && !File.Exists(strOutBillImage4_New))
                        {
                            File.Copy(strOutBillImage4_Old, strOutBillImage4_New);
                        }

                        #endregion -- 复制原单的入场图片 --
                    }
                }
            };
            DBHelper.ExecInTrans(args, exec);
            NewSaleCarInBillCode.Value = NewSaleCarInBillCode_temp.Value;
            NewSaleCarOutBillCode.Value = NewSaleCarOutBillCode_temp.Value;
            NewSaleCarInBillID.Value = NewSaleCarInBillID_temp.Value;
            NewSaleCarOutBillID.Value = NewSaleCarOutBillID_temp.Value;
        }

        //改单情况1处理方式：仅金额发生变更
        //public void ChangeBillDealAmount1(FactoryArgs args, t_BigID SaleCarInBillID, t_BigID CustomerID,t_String CarNum,
        //    t_Decimal NewPrice, t_Decimal NewAmount, t_Decimal OldPrice, t_Decimal OldAmount,
        //    t_Bool IsPayMoney,t_Decimal PayMoney)
        //{
        //    //新金额大于旧金额
        //    if (NewAmount.Value > OldAmount.Value)
        //    {
        //        if (IsPayMoney.Value == 1)//现金支付
        //        {
        //            if (PayMoney.Value <= 0)
        //            {
        //                throw new Exception("现金支付金额不能为0！");
        //            }

        //            t_BigID ReceiveBillHeaderID;
        //            t_String ReceiveBillCode;
        //            _IBLLRPReceiveBillHeader.Insert(args, out ReceiveBillHeaderID, out ReceiveBillCode,
        //                    new t_DTSmall(DateTime.Now), CustomerID, PayMoney, new t_String("充值来源：车号" + CarNum.Value + "现金充值"));
        //            _IBLLRPReceiveBillHeader.Approve(args, ReceiveBillHeaderID);

        //            //现金不足部分右客户账户扣除
        //            _DALSaleCarInOutBill.UpdateOutBillAmount(args, SaleCarInBillID, NewPrice, NewAmount);
        //        }
        //        else
        //        {
        //            _DALSaleCarInOutBill.UpdateOutBillAmount(args, SaleCarInBillID, NewPrice, NewAmount);
        //        }
        //    }
        //    else
        //    {
        //        //新金额小于旧金额，返还至客户账户
        //        _DALSaleCarInOutBill.UpdateOutBillAmount(args, SaleCarInBillID, NewPrice, NewAmount);
        //    }
        //}
        ////改单情况2处理方式：仅客户名称修改
        //public void ChangeBillDealAmount2(FactoryArgs args, t_BigID SaleCarInBillID, t_BigID NewCustomerID, t_BigID OldCustomerID)
        //{
        //    _DALSaleCarInOutBill.UpdateOutBillCustomer(args, SaleCarInBillID, NewCustomerID);
        //}

        ////改单改单情况3处理方式：客户名称以及金额发生变更
        //public void ChangeBillDealAmount3(FactoryArgs args, t_BigID SaleCarInBillID, t_BigID NewCustomerID, t_BigID OldCustomerID)
        //{
        //    _DALSaleCarInOutBill.UpdateOutBillCustomer(args, SaleCarInBillID, NewCustomerID);

        //}

        public void ImportSalesBill(FactoryArgs args, t_Table DTInBill, t_Table DTOutBill,
            out t_String ErrorExistsBillCode, out t_String ErrorUnFinishBillCode, out t_ID ErrorCount)
        {
            ErrorCount = new t_ID(0);
            ErrorExistsBillCode = new t_String();
            ErrorUnFinishBillCode = new t_String();
            int iErrorCount = 0;
            string strErrorUnFinishBillCode = "";
            string strErrorExistsBillCode = "";
            DataTable dtInBill = DTInBill.Value;
            DataTable dtOutBill = DTOutBill.Value;

            string strNotExistsCarNum = "";
            string strNotExistsCustomerName = "";
            string strNotExistsItemName = "";
            List<string> lstCarNum = new List<string>();
            List<string> lstCustomerName = new List<string>();
            List<string> lstItemName = new List<string>();
            foreach (DataRow drIn in dtInBill.Rows)
            {
                t_String CarNum = new t_String(drIn["CarNum"]);
                t_String ItemName = new t_String(drIn["ItemName"]);
                t_String CustomerName = new t_String(drIn["CustomerName"]);

                //先判断是否存在该
                if (!lstItemName.Contains(ItemName.Value))
                {
                    t_BigID ItemID_Temp;
                    _DALSaleCarInOutBill.GetItemID(args, ItemName, out ItemID_Temp);
                    if (ItemID_Temp.Value == null || ItemID_Temp.Value == 0)
                    {
                        if (strNotExistsItemName != "")
                            strNotExistsItemName += ",";
                        strNotExistsItemName += ItemName.Value;
                    }
                }

                //判断客户名称是否存在
                if (!lstCustomerName.Contains(CustomerName.Value))
                {
                    t_BigID CustomerID_Temp;
                    _DALSaleCarInOutBill.GetCustomerID(args, CustomerName, out CustomerID_Temp);
                    if (CustomerID_Temp.Value == null || CustomerID_Temp.Value == 0)
                    {
                        if (strNotExistsCustomerName != "")
                            strNotExistsCustomerName += ",";
                        strNotExistsCustomerName += CustomerName.Value;
                    }
                }

                //判断车牌号码是否存在
                if (!lstCarNum.Contains(CarNum.Value))
                {
                    t_BigID CarID_Temp;
                    _DALSaleCarInOutBill.GetCarID(args, CarNum, out CarID_Temp);
                    if (CarID_Temp.Value == null || CarID_Temp.Value == 0)
                    {
                        if (strNotExistsCarNum != "")
                            strNotExistsCarNum += ",";
                        strNotExistsCarNum += CarNum.Value;
                    }
                }

                lstCarNum.Add(CarNum.Value);
                lstCustomerName.Add(CustomerName.Value);
                lstItemName.Add(ItemName.Value);
            }

            string strError = "";
            if (strNotExistsCarNum != "")
            {
                if (strError != "")
                    strError += "\n";
                strError += "以下车牌号码不存在，请在系统上添加下列车牌号码再导入：\n" + strNotExistsCarNum;
            }
            if (strNotExistsCustomerName != "")
            {
                if (strError != "")
                    strError += "\n";
                strError += "以下客户不存在，请在系统上添加下列客户再导入：\n" + strNotExistsCustomerName;
            }
            if (strNotExistsItemName != "")
            {
                if (strError != "")
                    strError += "\n";
                strError += "以下物料不存在，请在系统上添加下列物料再导入：\n" + strNotExistsItemName;
            }

            if (strError != "")
                throw new Exception(strError);

            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                foreach (DataRow drIn in dtInBill.Rows)
                {
                    t_BigID SaleCarInBillID = new t_BigID(drIn["SaleCarInBillID"]);
                    t_ID BillTypeID = new t_ID();
                    if (dtInBill.Columns.Contains("BillTypeID"))
                        BillTypeID = new t_ID(drIn["BillTypeID"]);
                    t_String SaleCarInBillCode = new t_String(drIn["SaleCarInBillCode"].ToString());
                    t_String ItemName = new t_String(drIn["ItemName"]);
                    t_String CarNum = new t_String(drIn["CarNum"]);
                    t_String CustomerName = new t_String(drIn["CustomerName"]);
                    t_DTSmall BillDate = new t_DTSmall(drIn["BillDateIn"]);
                    t_ID ReceiveType = new t_ID(drIn["ReceiveType"]);
                    t_ID BillStatus = new t_ID(drIn["BillStatus"]);
                    t_ID CalculateType = new t_ID(drIn["CalculateType"]);
                    t_Float CarTare = new t_Float(drIn["CarTare"]);
                    t_ID PrintCount = new t_ID();
                    if (dtInBill.Columns.Contains("PrintCount"))
                        PrintCount = new t_ID(drIn["PrintCount"]);
                    t_ID IsCancel = new t_ID(drIn["IsCancel"]);
                    t_String ApproveBy = new t_String();
                    if (dtInBill.Columns.Contains("ApproveBy"))
                        ApproveBy = new t_String(drIn["ApproveBy"]);
                    t_DTSmall ApproveTime = new t_DTSmall();
                    if (dtInBill.Columns.Contains("ApproveTime"))
                        ApproveTime = new t_DTSmall(drIn["ApproveTime"]);
                    t_String CreateBy = new t_String(drIn["CreateByIn"].ToString());
                    t_DTSmall CreateTime = new t_DTSmall(drIn["CreateTimeIn"]);
                    t_String CancelBy = new t_String();
                    if (dtInBill.Columns.Contains("CancelBy"))
                        CancelBy = new t_String(drIn["CancelBy"]);
                    t_DTSmall CancelTime = new t_DTSmall();
                    if (dtInBill.Columns.Contains("CancelTime"))
                        CancelTime = new t_DTSmall(drIn["CancelTime"]);
                    t_String Description = new t_String();
                    if (dtInBill.Columns.Contains("Description"))
                        Description = new t_String(drIn["Description"]);
                    t_DTSmall CancelByDate = new t_DTSmall();
                    if (dtInBill.Columns.Contains("CancelByDate"))
                        CancelByDate = new t_DTSmall(drIn["CancelByDate"]);
                    t_String CancelDesc = new t_String();
                    if (dtInBill.Columns.Contains("CancelDesc"))
                        CancelDesc = new t_String(drIn["CancelDesc"]);

                    if (!(IsCancel.Value == 1 || BillStatus.Value == 2))
                    {
                        iErrorCount++;
                        if (strErrorUnFinishBillCode != "")
                        {
                            strErrorUnFinishBillCode += ",";
                        }
                        strErrorUnFinishBillCode += SaleCarInBillCode.Value;
                        continue;
                    }
                    //添加前先判断是否已存在该入场单数据
                    bool bolExists = _DALSaleCarInOutBill.VerifyIfExistsInBillCode(args, SaleCarInBillCode);
                    if (bolExists)
                    {
                        if (strErrorExistsBillCode != "")
                        {
                            strErrorExistsBillCode += ",";
                        }
                        strErrorExistsBillCode += SaleCarInBillCode.Value;
                        iErrorCount++;
                        continue;
                    }

                    t_BigID CarID = new t_BigID(drIn["CarID"]);
                    t_BigID ItemID = new t_BigID(drIn["ItemID"]);
                    t_BigID CustomerID = new t_BigID(drIn["CustomerID"]);
                    _DALSaleCarInOutBill.GetCarID(args, CarNum, out CarID);
                    _DALSaleCarInOutBill.GetCustomerID(args, CustomerName, out CustomerID);
                    _DALSaleCarInOutBill.GetItemID(args, ItemName, out ItemID);

                    t_BigID SaleCarInBillIDNew;
                    t_String SaleCarInBillCodeNew;
                    t_DTSmall BillDateNew = new t_DTSmall();
                    this.InsertInBill(argsInTrans, out SaleCarInBillIDNew, ref SaleCarInBillCodeNew, ref BillDateNew, CarID, ItemID, ReceiveType,
                        CalculateType, CarTare, CustomerID, Description, new t_ID(0), new t_ID(),new t_BigID(), CreateBy);

                    dtOutBill.DefaultView.RowFilter = "SaleCarInBillID=" + SaleCarInBillID.Value;
                    if (dtOutBill.DefaultView.Count > 0)
                    {
                        DataRowView drvOut = dtOutBill.DefaultView[0];
                        t_ID BillTypeID_Out = new t_ID();
                        if (dtOutBill.Columns.Contains("BillTypeID"))
                            BillTypeID_Out = new t_ID(drvOut["BillTypeID"]);
                        t_DTSmall BillDate_Out = new t_DTSmall(drvOut["BillDate"]);
                        t_Decimal TotalWeight_Out = new t_Decimal(drvOut["TotalWeight"]);
                        t_Decimal SuttleWeight_Out = new t_Decimal(drvOut["SuttleWeight"]);
                        t_Decimal Price_Out = new t_Decimal(drvOut["Price"]);
                        t_Decimal Amount_Out = new t_Decimal(drvOut["Amount"]);
                        t_String Description_Out = new t_String();
                        if (dtOutBill.Columns.Contains("Description"))
                            Description_Out = new t_String(drvOut["Description"]);
                        t_String CreateBy_Out = new t_String(drvOut["CreateBy"].ToString());
                        t_DTSmall CreateTime_Out = new t_DTSmall(drvOut["CreateTime"]);
                        t_ID OutPrintCount_Out = new t_ID(drvOut["OutPrintCount"]);
                        t_String SaleCarOutBillCode_Out = new t_String(drvOut["SaleCarOutBillCode"].ToString());

                        t_ID UnAutoApprove = new t_ID();
                        if (IsCancel.Value == 1)
                        {
                            UnAutoApprove.Value = 1;
                        }

                        t_BigID SaleCarOutBillIDNew;
                        t_String SaleCarOutBillCodeNew = new t_String();
                        this.InsertOutBill(argsInTrans, out SaleCarOutBillIDNew, ref SaleCarOutBillCodeNew, ref BillDateNew, SaleCarInBillIDNew,
                            CarID, ReceiveType, CalculateType, Price_Out, Amount_Out, TotalWeight_Out, SuttleWeight_Out, new t_Decimal(0),
                            new t_String("导入数据"), new t_ID(), UnAutoApprove, CreateBy, new t_BigID());

                        using (DataTable dtBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(argsInTrans, SaleCarInBillIDNew))
                        {
                            DataRow drBill = dtBill.Rows[0];
                            t_ID IsCancelNew = new t_ID(drBill["IsCancel"]);
                            t_ID BillStatusNew = new t_ID(drBill["BillStatus"]);
                            if (BillStatusNew.Value == 2)//当前单据处于已审核状态
                            {
                                if (IsCancel.Value == 1)//原单已作废，则先反审核然后作废
                                {
                                    this.UnApprove(argsInTrans, SaleCarInBillIDNew);
                                    this.Cancel(argsInTrans, SaleCarInBillIDNew, CancelDesc);
                                }
                            }
                            else if (IsCancel.Value == 1 && IsCancelNew.Value == 0)//当前单据处于未作废状态,原单已作废，则进行作废
                            {
                                this.Cancel(argsInTrans, SaleCarInBillIDNew, CancelDesc);
                            }

                            _DALSaleCarInOutBill.UpdateOutBillDate(argsInTrans, SaleCarOutBillIDNew, BillDate_Out, CreateBy_Out,
                                CreateTime_Out, SaleCarOutBillCode_Out);
                        }

                        _DALSaleCarInOutBill.UpdateInBillDate(argsInTrans, SaleCarInBillIDNew, BillDate, ApproveBy, ApproveTime, CreateBy, CreateTime, CancelBy,
                            CancelTime, CancelByDate, SaleCarInBillCode);//修改入场单信息
                    }
                }
            };
            DBHelper.ExecInTrans(args, exec);
            ErrorExistsBillCode.Value = strErrorExistsBillCode;
            ErrorUnFinishBillCode.Value = strErrorUnFinishBillCode;
            ErrorCount.Value = iErrorCount;
        }

        public void VerifyIsNeedReflesh(FactoryArgs args, t_BigID ClientVersion, out t_BigID ServerVersion, out t_Bool NeedReflesh)
        {
            NeedReflesh = new t_Bool(0);
            ServerVersion = new t_BigID(ClientVersion.Value);
            if (ClientVersion.Value != mServerVersion)
            {
                NeedReflesh.Value = 1;
                ServerVersion.Value = mServerVersion;
            }
        }

        #region -- 汽油采购磅单 --

        public void InsertPurchaseInBill(FactoryArgs args, out t_BigID SaleCarInBillID, ref t_String SaleCarInBillCode, ref t_DTSmall BillDate, t_BigID CarID,
            t_BigID ItemID, t_ID CalculateType, t_Decimal TotalWeight, t_BigID CustomerID, t_String Description,t_BigID SourceSaleCarInBillID)
        {
            SaleCarInBillID = new t_BigID();
            t_BigID SaleCarInBillIDTemp = new t_BigID();
            t_DTSmall BillDateTemp = new t_DTSmall(DateTime.Now);
            t_String SaleCarInBillCodeTemp = new t_String();
            Description.IsNullToEmpty();

            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_ID ReceiveType = new t_ID(3);
                this.InsertInBill(argsInTrans, out SaleCarInBillIDTemp, ref SaleCarInBillCodeTemp, ref BillDateTemp, CarID, ItemID, ReceiveType, CalculateType, new t_Float(0),
                    CustomerID, Description, new t_ID(), new t_ID(1), SourceSaleCarInBillID,new t_String());

                t_BigID SaleCarOutBillID;
                t_String SaleCarOutBillCode;

                _DALSaleCarInOutBill.InsertOutBill(argsInTrans, out SaleCarOutBillID, new t_String(""), SaleCarInBillIDTemp, CarID, BillDateTemp,
                    TotalWeight, new t_Decimal(0), new t_Decimal(0), new t_Decimal(0), ReceiveType, CalculateType, Description,new t_String(0), new t_BigID());

            };
            DBHelper.ExecInTrans(args, exec);

            SaleCarInBillID.Value = SaleCarInBillIDTemp.Value;
            SaleCarInBillCode.Value = SaleCarInBillCodeTemp.Value;
            BillDate.Value = BillDateTemp.Value;
        }

        public void InsertPurchaseOutBill(FactoryArgs args, t_BigID SaleCarInBillID,out t_BigID SaleCarOutBillID, out t_String SaleCarOutBillCode,
            ref t_DTSmall BillDate, t_Decimal CarTare, t_String Description)
        {
            SaleCarOutBillCode = new t_String();
            SaleCarOutBillID = new t_BigID();
            //生成编码
            string strBillFont = "XS" + DateTime.Now.ToString("yyyyMM") + "-";
            using (DataTable dtBillCode = _DALSaleCarInOutBill.GetMaxOutBillCode(args, strBillFont))
            {
                if (dtBillCode.Rows.Count > 0)
                {
                    DataRow drBillCode = dtBillCode.Rows[0];
                    int iIndex = 1;
                    string strIndex = "";
                    if (drBillCode["SaleCarOutBillCode"].ToString().TrimEnd().Contains(strBillFont))
                    {
                        iIndex = Convert.ToInt32(drBillCode["SaleCarOutBillCode"].ToString().TrimEnd().Replace(strBillFont, ""));
                        iIndex += 1;
                        if (iIndex < 10)
                        {
                            strIndex = "0000" + iIndex.ToString();
                        }
                        else if (iIndex < 100)
                        {
                            strIndex = "000" + iIndex.ToString();
                        }
                        else if (iIndex < 1000)
                        {
                            strIndex = "00" + iIndex.ToString();
                        }
                        else if (iIndex < 10000)
                        {
                            strIndex = "0" + iIndex.ToString();
                        }
                        else
                        {
                            strIndex = iIndex.ToString();
                        }
                        SaleCarOutBillCode.SetValueWithObject(strBillFont + strIndex);
                    }
                    else
                    {
                        SaleCarOutBillCode.SetValueWithObject(strBillFont + "-00001");
                    }
                }
                else
                {
                    SaleCarOutBillCode.SetValueWithObject(strBillFont + "00001");
                }
            }
            t_String SaleCarOutBillCodeTemp = new t_String(SaleCarOutBillCode.Value);
            t_Decimal TotalWeight = new t_Decimal(0);
            t_Decimal SuttleWeight = new t_Decimal(0);
            DataTable dtInBill = _DALSaleCarInOutBill.GetGetSaleCarInOutBill(args, SaleCarInBillID);
            if (dtInBill.Rows.Count > 0)
            {
                TotalWeight.SetValueWithObject(dtInBill.Rows[0]["TotalWeight"]);
                SuttleWeight.SetValueWithObject(TotalWeight.Value - CarTare.Value);
                SaleCarOutBillID.SetValueWithObject(dtInBill.Rows[0]["SaleCarOutBillID"]);
            }
            
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALSaleCarInOutBill.UpdateInBillPurchase(argsInTrans, SaleCarInBillID, CarTare);

                _DALSaleCarInOutBill.UpdateOutBillPurchase(argsInTrans, SaleCarInBillID, SuttleWeight, new t_Decimal(0), new t_Decimal(0), SaleCarOutBillCodeTemp);

                this.Approve(argsInTrans, SaleCarInBillID, new t_DTSmall(DateTime.Now));
            };
            DBHelper.ExecInTrans(args, exec);
        }

        #endregion -- 汽油采购磅单 --

        #region -- 远程同步订单至服务器 --

        public void SynchronousBillFromClient(FactoryArgs args, t_Table DTInOutBill)
        {

            foreach (DataRow dr in DTInOutBill.Value.Rows)
            {
                //SaleCarInBillID, BillTypeID, SaleCarOutBillCode, SaleCarInBillCode, CarID, CarNum, ItemID, ItemName, 
                //ItemRate, ItemMode, BillDateIn, ReceiveType, BillStatus, CustomerID, CustomerName, CalculateType, 
                //CarTare, PrintCount, IsCancel, ApproveBy, ApproveTime, CreateByIn, CreateTimeIn, CancelBy, CancelTime, 
                //SaleCarOutBillID, BillDateOut, TotalWeight, SuttleWeight, SuttleWeightT, Price, PriceT, Amount, 
                //Description, CreateByOut, CreateTimeOut, BillType, CancelDesc, OutPrintCount, ItemTypeName, SaleBillType, 
                //IsSynchronousToServer, SynchronousToServerTime
                t_BigID SaleCarInBillID = new t_BigID(dr["SaleCarInBillID"]);
                t_BigID SaleCarOutBillID = new t_BigID(dr["SaleCarOutBillID"]);
                t_ID BillTypeID = new t_ID(dr["BillTypeID"]);
                t_String SaleCarInBillCode = new t_String(dr["SaleCarInBillCode"].ToString());
                t_String SaleCarOutBillCode = new t_String(dr["SaleCarOutBillCode"].ToString());
                t_BigID ItemID = new t_BigID(dr["ItemID"]);
                t_BigID CarID = new t_BigID(dr["CarID"]);
                t_BigID CustomerID = new t_BigID(dr["CustomerID"]);
                t_DTSmall BillDateIn = new t_DTSmall(dr["BillDateIn"]);
                t_DTSmall BillDateOut = new t_DTSmall(dr["BillDateOut"]);
                t_ID ReceiveType = new t_ID(dr["ReceiveType"]);
                t_ID AmountType = new t_ID(dr["AmountType"]);
                t_ID BillStatus = new t_ID(dr["BillStatus"]);
                t_ID CalculateType = new t_ID(dr["CalculateType"]);
                t_Decimal TotalWeight = new t_Decimal(dr["TotalWeight"]);
                t_Decimal CarTare = new t_Decimal(dr["CarTare"]);
                t_Decimal SuttleWeight = new t_Decimal(dr["SuttleWeight"]);
                t_Decimal Price = new t_Decimal(dr["Price"]);
                t_Decimal Amount = new t_Decimal(dr["Amount"]);
                t_ID PrintCount = new t_ID(dr["PrintCount"]);
                t_ID OutPrintCount = new t_ID(dr["OutPrintCount"]);
                t_ID SaleBillType = new t_ID(dr["SaleBillType"]);
                t_ID IsCancel = new t_ID(dr["IsCancel"]);
                t_String CancelBy = new t_String(dr["CancelBy"]);
                t_DTSmall CancelTime = new t_DTSmall(dr["CancelTime"]);
                t_String ApproveBy = new t_String(dr["ApproveBy"]);
                t_DTSmall ApproveTime = new t_DTSmall(dr["ApproveTime"]);
                t_String CreateByIn = new t_String(dr["CreateByIn"].ToString());
                t_DTSmall CreateTimeIn = new t_DTSmall(dr["CreateTimeIn"]);
                t_String CreateByOut = new t_String(dr["CreateByOut"].ToString());
                t_DTSmall CreateTimeOut = new t_DTSmall(dr["CreateTimeOut"]);
                t_String Description = new t_String(dr["Description"]);
                t_String CancelDesc = new t_String(dr["CancelDesc"]);

                //读取服务器单价
                _IBLLModifyBillHeader.GetCustomerItemPrice(args, ItemID, CarID, CustomerID, CalculateType, out Price);

                using (DataTable dtCustomer = _DALSaleCarInOutBill.GetCustomer(args, CustomerID))
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        int iAmountType = LBConverter.ToInt32(dtCustomer.Rows[0]["AmountType"]);//金额格式：0整数 1一位小数 2两位小数
                        string strFormat = "0";
                        if (iAmountType == 1)
                            strFormat = "0.0";
                        else if (iAmountType == 2)
                            strFormat = "0.00";

                        if (CalculateType.Value == 0)
                            Amount.Value = LBConverter.ToDecimal(((decimal)(SuttleWeight.Value * Price.Value)).ToString(strFormat));
                        else
                            Amount.Value = LBConverter.ToDecimal(((decimal)(Price.Value)).ToString(strFormat));
                    }
                }


                DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
                {
                    this.InsertInBill(argsInTrans, out SaleCarInBillID, ref SaleCarInBillCode, ref BillDateIn, CarID, ItemID, ReceiveType, CalculateType,
                        new t_Float(CarTare.Value), CustomerID, Description, new t_ID(), SaleBillType, SaleCarInBillID, CreateByIn);

                    if (SaleCarOutBillID.Value > 0)//添加入场单
                    {
                        t_ID IsEmptyOut = new t_ID(0);
                        t_ID UnAutoApprove = new t_ID(0);
                        //是否空车出场
                        if (IsCancel.Value == 1 && Math.Abs((decimal)SuttleWeight.Value) < 1000)
                        {
                            IsEmptyOut = new t_ID(1);
                            UnAutoApprove.Value = 1;
                        }
                        if (IsCancel.Value == 1)
                        {
                            UnAutoApprove.Value = 1;
                        }

                        this.InsertOutBill(argsInTrans, out SaleCarOutBillID, ref SaleCarOutBillCode, ref BillDateOut, SaleCarInBillID,
                            CarID, ReceiveType, CalculateType, Price, Amount, TotalWeight, SuttleWeight, new t_Decimal(0), Description,
                            IsEmptyOut, UnAutoApprove, CreateByOut, SaleCarOutBillID);

                        if (IsEmptyOut.Value == 0 && IsCancel.Value == 1)//自动作废
                        {
                            this.Cancel(argsInTrans, SaleCarInBillID, CancelDesc);
                        }
                    }
                    else
                    {
                        if (IsCancel.Value == 1)//自动作废
                        {
                            this.Cancel(argsInTrans, SaleCarInBillID, CancelDesc);
                        }
                    }
                };
                DBHelper.ExecInTrans(args, exec);
            }

        }

        public void SynchronousFinish(FactoryArgs args, t_BigID SaleCarInBillID)
        {
            _DALSaleCarInOutBill.SynchronousFinish(args, SaleCarInBillID);
        }

        #endregion
    }

    public enum enImagePathType
    {
        InBillPath,
        OutBillPath
    }
}
