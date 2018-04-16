using LB.Web.Base.Base.Helper;
using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.IBLL.IBLL.IBLLDB;
using LB.Web.IBLL.IBLL.IBLLSM;
using LB.Web.SM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LB.Web.SM.BLL
{
    public class BLLSaleCarReturnBill : IBLLFunction
    {
        private DALSaleCarReturnBill _DALSaleCarReturnBill;
        private DALSaleCarInOutBill _DALSaleCarInOutBill = null;
        private IBLLSaleCarInOutBill _IBLLSaleCarInOutBill;
        private IBLLDbErrorLog _IBLLDbErrorLog = null;
        private IBLLDbSystemConst _IBLLDbSystemConst = null;
        private IBLLDbSysConfig _IBLLDbSysConfig = null;
        public BLLSaleCarReturnBill()
        {
            _DALSaleCarInOutBill = new DAL.DALSaleCarInOutBill();
            _DALSaleCarReturnBill = new DAL.DALSaleCarReturnBill();
            _IBLLSaleCarInOutBill = (IBLLSaleCarInOutBill)DBHelper.GetFunctionMethod(14100);
            _IBLLDbErrorLog = (IBLLDbErrorLog)DBHelper.GetFunctionMethod(20000);
            _IBLLDbSystemConst = (IBLLDbSystemConst)DBHelper.GetFunctionMethod(20100);
            _IBLLDbSysConfig = (IBLLDbSysConfig)DBHelper.GetFunctionMethod(14300);
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 30000:
                    strFunName = "Insert";
                    break;

                case 30001:
                    strFunName = "Update";
                    break;

                case 30002:
                    strFunName = "Delete";
                    break;

                case 30003:
                    strFunName = "SaveReturnInSalesCameraImage";
                    break;

                case 30004:
                    strFunName = "SaveReturnOutSalesCameraImage";
                    break;

                case 30005:
                    strFunName = "GetCameraImage";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, out t_BigID SaleCarReturnBillID, t_BigID SaleCarInBillID,
            t_Decimal TotalWeight)
        {
            using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(args, SaleCarInBillID))
            {
                if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                {
                    DataRow drBill = dtInBill.Rows[0];
                    int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);

                    if (bolIsCancel)
                    {
                        throw new Exception("该磅单已作废，无法退货！");
                    }

                    if (iBillStatus != 2)
                    {
                        throw new Exception("该磅单未审核或者未完成，无法退货！");
                    }
                }
            }

            //读取退货单号前缀
            t_String SysConfigFieldName = new t_String("SaleReturnBillCode");
            t_String SysConfigValue;
            _IBLLDbSysConfig.GetConfigValue(args, SysConfigFieldName, out SysConfigValue);
            if (SysConfigValue.Value == "")
            {
                SysConfigValue.Value = "TH";
            }

            t_String SaleCarReturnBilCode = new t_String();
            //生成编码
            string strBillFont = SysConfigValue.Value.TrimEnd() + DateTime.Now.ToString("yyyyMM") + "-";
            using (DataTable dtBillCode = _DALSaleCarReturnBill.GetMaxInBillCode(args, strBillFont))
            {
                if (dtBillCode.Rows.Count > 0)
                {
                    DataRow drBillCode = dtBillCode.Rows[0];
                    int iIndex = 1;
                    string strIndex = "";
                    if (drBillCode["SaleCarReturnBilCode"].ToString().TrimEnd().Contains(strBillFont))
                    {
                        iIndex = Convert.ToInt32(drBillCode["SaleCarReturnBilCode"].ToString().TrimEnd().Replace(strBillFont, ""));
                        iIndex += 1;
                        if (iIndex < 10)
                        {
                            strIndex = "00" + iIndex.ToString();
                        }
                        else if (iIndex < 100)
                        {
                            strIndex = "0" + iIndex.ToString();
                        }
                        else
                        {
                            strIndex = iIndex.ToString();
                        }
                        SaleCarReturnBilCode.SetValueWithObject(strBillFont + strIndex);
                    }
                    else
                    {
                        SaleCarReturnBilCode.SetValueWithObject(strBillFont + "-001");
                    }
                }
                else
                {
                    SaleCarReturnBilCode.SetValueWithObject(strBillFont + "001");
                }
            }

            _DALSaleCarReturnBill.Insert(args, out SaleCarReturnBillID, SaleCarReturnBilCode, SaleCarInBillID, TotalWeight);
        }

        public void Update(FactoryArgs args, t_BigID SaleCarReturnBillID,
            t_Decimal CarTare, t_Decimal SuttleWeight, t_ID ReturnType, t_String Description,t_ID ReturnReason,
            out t_String NewSaleCarInBillCode, out t_String NewSaleCarOutBillCode,
            out t_BigID NewSaleCarInBillID, out t_BigID NewSaleCarOutBillID)
        {
            NewSaleCarInBillCode = new t_String();
            NewSaleCarOutBillCode = new t_String();
            t_String NewSaleCarInBillCode_temp = new t_String();
            t_String NewSaleCarOutBillCode_temp = new t_String();
            NewSaleCarInBillID = new t_BigID();
            NewSaleCarOutBillID = new t_BigID();
            t_BigID NewSaleCarInBillID_temp = new t_BigID();
            t_BigID NewSaleCarOutBillID_temp = new t_BigID();
            t_String SaleCarReturnBilCode = new t_String();
            t_BigID SaleCarInBillID = new t_BigID();
            using (DataTable dtReturnBill = _DALSaleCarReturnBill.GetReturnBill(args, SaleCarReturnBillID))
            {
                if (dtReturnBill.Rows.Count > 0)
                {
                    DataRow drReturn = dtReturnBill.Rows[0];
                    SaleCarReturnBilCode.SetValueWithObject(drReturn["SaleCarReturnBilCode"]);
                    SaleCarInBillID.Value = LBConverter.ToInt64(drReturn["SaleCarInBillID"]);
                    int iReturnStatus = LBConverter.ToInt32(drReturn["ReturnStatus"]);

                    if (iReturnStatus == 1)
                    {
                        throw new Exception("该退货记录已完成，无法重复退货！");
                    }
                }
            }

            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                using (DataTable dtInBill = _DALSaleCarInOutBill.GetSaleCarInBill(argsInTrans, SaleCarInBillID))
                {
                    if (dtInBill.Rows.Count > 0)//校验是否已审核或者已作废
                    {
                        DataRow drBill = dtInBill.Rows[0];
                        int iBillStatus = LBConverter.ToInt32(drBill["BillStatus"]);
                        bool bolIsCancel = LBConverter.ToBoolean(drBill["IsCancel"]);
                        t_String ReturnReasonConstText;
                        _IBLLDbSystemConst.GetConstValue(argsInTrans, new t_String("ReturnReason"), new t_String(ReturnReason.Value.ToString()), out ReturnReasonConstText);

                        string strCancelDesc = (ReturnType.Value == 0 ? "完全退货作废" : "部分退货作废")+ ",退货单号【" + SaleCarReturnBilCode.Value + "】，退货原因："+ ReturnReasonConstText.Value;
                        //先取消审核磅单
                        if (iBillStatus == 2)
                        {
                            _IBLLSaleCarInOutBill.UnApprove(argsInTrans, SaleCarInBillID);//取消审核
                            _IBLLSaleCarInOutBill.Cancel(argsInTrans, SaleCarInBillID, new t_String(strCancelDesc));////作废订单
                        }
                        else if (iBillStatus == 0)
                        {
                            //作废订单
                            _IBLLSaleCarInOutBill.Cancel(argsInTrans, SaleCarInBillID, new t_String(strCancelDesc));
                        }

                        if (ReturnType.Value == 0)//完全退货
                        {
                            //无需处理
                        }
                        else if (ReturnType.Value == 1)//部分退货
                        {
                            _IBLLSaleCarInOutBill.CopySaleBill(argsInTrans, SaleCarInBillID, CarTare, out NewSaleCarInBillID_temp, out NewSaleCarOutBillID_temp,
                                out NewSaleCarInBillCode_temp,out NewSaleCarOutBillCode_temp);
                            //_IBLLSaleCarInOutBill.Approve(argsInTrans, NewSaleCarInBillID);
                        }

                        if (bolIsCancel)
                        {
                            throw new Exception("该磅单已作废，无法退货！");
                        }

                        if (iBillStatus != 2)
                        {
                            throw new Exception("该磅单未审核或者未完成，无法退货！");
                        }

                        _DALSaleCarReturnBill.Update(argsInTrans, SaleCarReturnBillID, CarTare, SuttleWeight, ReturnType, Description, ReturnReason, NewSaleCarInBillID_temp);
                    }
                }
            };
            DBHelper.ExecInTrans(args, exec);
            //_DALSaleCarReturnBill.Insert(args, out SaleCarReturnBillID, SaleCarInBillID, TotalWeight);
            NewSaleCarInBillCode.Value = NewSaleCarInBillCode_temp.Value;
            NewSaleCarOutBillCode.Value = NewSaleCarOutBillCode_temp.Value;
            NewSaleCarInBillID.Value = NewSaleCarInBillID_temp.Value;
            NewSaleCarOutBillID.Value = NewSaleCarOutBillID_temp.Value;
        }

        public void SaveReturnInSalesCameraImage(FactoryArgs args, t_BigID SaleCarReturnBillID,
            t_Image MonitoreImg1, t_Image MonitoreImg2, t_Image MonitoreImg3, t_Image MonitoreImg4)
        {
            try
            {
                string strDatePath = GetPicturePath(enImagePathType.InBillPath, DateTime.Now);

                if (MonitoreImg1.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image1.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg1.Value);
                }
                if (MonitoreImg2.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image2.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg2.Value);
                }
                if (MonitoreImg3.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image3.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg3.Value);
                }
                if (MonitoreImg4.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image4.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg4.Value);
                }
            }
            catch (Exception ex)
            {
                this._IBLLDbErrorLog.Insert(args,
                    new t_String("服务器端，保存入场图片时报错，退货单号：" + SaleCarReturnBillID.Value.ToString() + "\n错误信息：" + ex.Message));
                throw ex;
            }
        }


        public void SaveReturnOutSalesCameraImage(FactoryArgs args, t_BigID SaleCarReturnBillID,
            t_Image MonitoreImg1, t_Image MonitoreImg2, t_Image MonitoreImg3, t_Image MonitoreImg4)
        {
            try
            {
                string strDatePath = GetPicturePath(enImagePathType.OutBillPath, DateTime.Now);

                if (MonitoreImg1.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image1.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg1.Value);
                }
                if (MonitoreImg2.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image2.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg2.Value);
                }
                if (MonitoreImg3.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image3.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg3.Value);
                }
                if (MonitoreImg4.Value != null)
                {
                    string strImagePath = Path.Combine(strDatePath, SaleCarReturnBillID.Value.ToString() + "_Image4.jpg");
                    CommonHelper.SaveFile(strImagePath, MonitoreImg4.Value);
                }
            }
            catch (Exception ex)
            {
                this._IBLLDbErrorLog.Insert(args,
                    new t_String("服务器端，保存出场图片时报错，退货单号：" + SaleCarReturnBillID.Value.ToString() + "\n错误信息：" + ex.Message));
                throw ex;
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

            string strPicFile = pathType == enImagePathType.InBillPath ? "InReturnPicture" : "OutReturnPicture";

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

        public void GetCameraImage(FactoryArgs args, t_BigID SaleCarReturnBillID,
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
            
            t_DTSmall CarInTime = new t_DTSmall();
            t_DTSmall CarOutTime = new t_DTSmall();
            using (DataTable dtBill = _DALSaleCarReturnBill.GetReturnBill(args, SaleCarReturnBillID))
            {
                if (dtBill.Rows.Count > 0)
                {
                    DataRow drBill = dtBill.Rows[0];
                    CarInTime.SetValueWithObject(drBill["CarInTime"]);
                    CarOutTime.SetValueWithObject(drBill["CarOutTime"]);
                }
            }

            //读取入场图片
            if (CarInTime.Value != null)
            {
                string strInPath = GetPicturePath(enImagePathType.InBillPath, (DateTime)CarInTime.Value);
                string strPathImg1 = Path.Combine(strInPath, SaleCarReturnBillID.Value.ToString() + "_Image1.jpg");
                string strPathImg2 = Path.Combine(strInPath, SaleCarReturnBillID.Value.ToString() + "_Image2.jpg");
                string strPathImg3 = Path.Combine(strInPath, SaleCarReturnBillID.Value.ToString() + "_Image3.jpg");
                string strPathImg4 = Path.Combine(strInPath, SaleCarReturnBillID.Value.ToString() + "_Image4.jpg");

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
            if (CarOutTime.Value != null)
            {
                string strOutPath = GetPicturePath(enImagePathType.OutBillPath, (DateTime)CarOutTime.Value);
                string strPathImg1 = Path.Combine(strOutPath, SaleCarReturnBillID.Value.ToString() + "_Image1.jpg");
                string strPathImg2 = Path.Combine(strOutPath, SaleCarReturnBillID.Value.ToString() + "_Image2.jpg");
                string strPathImg3 = Path.Combine(strOutPath, SaleCarReturnBillID.Value.ToString() + "_Image3.jpg");
                string strPathImg4 = Path.Combine(strOutPath, SaleCarReturnBillID.Value.ToString() + "_Image4.jpg");

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
    }
}
