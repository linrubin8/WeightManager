using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.IBLL.IBLL.IBLLSM;
using LB.Web.SM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.SM.BLL
{
    public class BLLModifyBillHeader : IBLLFunction, IBLLModifyBillHeader
    {
        private DALModifyBillHeader _DALModifyBillHeader = null;
        private DALModifyBillDetail _DALModifyBillDetail = null;
        public BLLModifyBillHeader()
        {
            _DALModifyBillHeader = new DAL.DALModifyBillHeader();
            _DALModifyBillDetail = new DAL.DALModifyBillDetail();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13600:
                    strFunName = "Insert";
                    break;
                case 13601:
                    strFunName = "Update";
                    break;
                case 13602:
                    strFunName = "Delete";
                    break;
                case 13603:
                    strFunName = "Approve";
                    break;
                case 13604:
                    strFunName = "UnApprove";
                    break;
                case 13605:
                    strFunName = "Cancel";
                    break;
                case 13606:
                    strFunName = "UnCancel";
                    break;
                case 13607:
                    strFunName = "GetCustomerLastItemPrice";
                    break;
                case 13608:
                    strFunName = "GetCustomerItemPrice";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, out t_BigID ModifyBillHeaderID,out t_String ModifyBillCode, t_BigID CustomerID, 
            t_DTSmall BillDate,t_DTSmall EffectDate, t_String Description)
        {
            ModifyBillCode = new t_String();
            ModifyBillHeaderID = new t_BigID();
            t_BigID BillTypeID = new t_BigID(2);
            using (DataTable dtCustomer = _DALModifyBillHeader.GetCustomerByID(args, CustomerID))
            {
                if (dtCustomer.Rows.Count == 0)
                {
                    throw new Exception("所选客户不存在，保存不成功！");
                }
            }

            //生成编码
            string strBillFont = "SM" + DateTime.Now.ToString("yyMMdd");
            using (DataTable dtBillCode = _DALModifyBillHeader.GetMaxBillCode(args))
            {
                if (dtBillCode.Rows.Count > 0)
                {
                    DataRow drBillCode = dtBillCode.Rows[0];
                    int iIndex = 1;
                    string strIndex = "";
                    if (drBillCode["ModifyBillCode"].ToString().TrimEnd().Contains(strBillFont))
                    {
                        iIndex = Convert.ToInt32(drBillCode["ModifyBillCode"].ToString().TrimEnd().Replace(strBillFont, ""));
                        iIndex += 1;
                        if (iIndex < 10)
                        {
                            strIndex = "0" + iIndex.ToString();
                        }
                        else
                        {
                            strIndex = iIndex.ToString();
                        }
                        ModifyBillCode.SetValueWithObject(strBillFont + strIndex);
                    }
                    else
                    {
                        ModifyBillCode.SetValueWithObject(strBillFont + "01");
                    }
                }
                else
                {
                    ModifyBillCode.SetValueWithObject(strBillFont + "01");
                }
            }

            _DALModifyBillHeader.Insert(args, out ModifyBillHeaderID, BillTypeID, CustomerID, 
                ModifyBillCode, BillDate, EffectDate, Description);
        }

        public void Update(FactoryArgs args, t_BigID ModifyBillHeaderID, t_BigID CustomerID, 
            t_DTSmall BillDate, t_DTSmall EffectDate, t_String Description)
        {
            using (DataTable dtCustomer = _DALModifyBillHeader.GetCustomerByID(args, CustomerID))
            {
                if (dtCustomer.Rows.Count == 0)
                {
                    throw new Exception("所选客户不存在，保存不成功！");
                }
            }

            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (bolIsApprove)
                {
                    throw new Exception("该调价单已审核，无法保存修改！");
                }

                if (bolIsCancel)
                {
                    throw new Exception("该调价单已作废，无法保存修改！");
                }
            }

            _DALModifyBillHeader.Update(args, ModifyBillHeaderID, CustomerID, BillDate, EffectDate, Description);
        }

        public void Approve(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            t_BigID CustomerID = new t_BigID();
            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                CustomerID.SetValueWithObject(drHeader["CustomerID"]);
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (bolIsApprove)
                {
                    throw new Exception("该调价单已审核，无法再执行审核操作！");
                }

                if (bolIsCancel)
                {
                    throw new Exception("该调价单已作废，无法执行审核操作！");
                }
            }
            _DALModifyBillHeader.Approve(args, ModifyBillHeaderID);
            /*DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                #region -- 审核订单时，更新物料价格表 --

                using (DataTable dtDetail = _DALModifyBillDetail.GetModifyBillDetailByHeaderID(argsInTrans, ModifyBillHeaderID))
                {
                    foreach (DataRow drDetail in dtDetail.Rows)
                    {
                        t_BigID ItemID = new t_BigID(drDetail["ItemID"]);
                        t_BigID CarID = new t_BigID(drDetail["CarID"]);
                        t_ID CalculateType = new t_ID(drDetail["CalculateType"]);
                        t_Decimal Price = new t_Decimal(drDetail["Price"]);


                        using (DataTable dtPrice = _DALModifyBillHeader.GetSMItemPrice(argsInTrans,
                            CustomerID, CarID, ItemID, CalculateType))
                        {
                            if (dtPrice.Rows.Count > 0)
                            {
                                #region --如果已存在记录，则更新单价 --
                                DataRow drPrice = dtPrice.Rows[0];
                                t_BigID ItemPriceID = new t_BigID(drPrice["ItemPriceID"]);

                                _DALModifyBillHeader.UpdateSMItemPrice(argsInTrans, ItemPriceID, Price);
                                #endregion --如果已存在记录，则更新单价 --
                            }
                            else
                            {
                                #region --如果不存在记录，则添加单价 --

                                t_BigID ItemPriceID = new t_BigID();
                                _DALModifyBillHeader.InsertSMItemPrice(argsInTrans, out ItemPriceID, CustomerID, CarID, ItemID, CalculateType, Price);

                                #endregion --如果不存在记录，则添加单价 --
                            }
                        }
                    }
                }
                #endregion

                
            };
            DBHelper.ExecInTrans(args, exec);*/
        }

        public void UnApprove(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            t_BigID CustomerID = new t_BigID();
            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                CustomerID.SetValueWithObject(drHeader["CustomerID"]);
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (!bolIsApprove)
                {
                    throw new Exception("该调价单未审核，无法执行反审核操作！");
                }

                if (bolIsCancel)
                {
                    throw new Exception("该调价单已作废，无法执行反审核操作！");
                }
            }
            _DALModifyBillHeader.UnApprove(args, ModifyBillHeaderID);
            /*DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                #region -- 取消审核订单时，更新或删除物料价格表 --

                using (DataTable dtDetail = _DALModifyBillDetail.GetModifyBillDetailByHeaderID(argsInTrans, ModifyBillHeaderID))
                {
                    foreach (DataRow drDetail in dtDetail.Rows)
                    {
                        t_BigID ItemID = new t_BigID(drDetail["ItemID"]);
                        t_BigID CarID = new t_BigID(drDetail["CarID"]);
                        t_ID CalculateType = new t_ID(drDetail["CalculateType"]);
                        t_Decimal Price = new t_Decimal(drDetail["Price"]);
                        t_BigID ModifyBillDetailID = new t_BigID(drDetail["ModifyBillDetailID"]);

                        t_BigID ItemPriceID = new t_BigID();//价格表记录
                        using (DataTable dtPrice = _DALModifyBillHeader.GetSMItemPrice(argsInTrans,
                            CustomerID, CarID, ItemID, CalculateType))
                        {
                            if (dtPrice.Rows.Count > 0)
                            {
                                #region --如果已存在记录，则更新单价 --

                                DataRow drPrice = dtPrice.Rows[0];
                                ItemPriceID.SetValueWithObject(drDetail["Price"]);

                                #endregion --如果已存在记录，则更新单价 --
                            }
                        }

                        //查询除了本记录之外，最新审核的调价明细行
                        using (DataTable dtLastDetail = _DALModifyBillDetail.GetLastModifyBillDetail(argsInTrans,
                                    ModifyBillDetailID, CustomerID, CarID, ItemID, CalculateType))
                        {
                            if (dtLastDetail.Rows.Count > 0)
                            {
                                #region --如果已存在记录，则更新单价 --
                                DataRow drPrice = dtLastDetail.Rows[0];
                                //t_BigID LastModifyBillDetailID = new t_BigID(drPrice["ModifyBillDetailID"]);
                                t_Decimal LastPrice = new t_Decimal(drPrice["Price"]);

                                _DALModifyBillHeader.UpdateSMItemPrice(argsInTrans, ItemPriceID, LastPrice);
                                #endregion --如果已存在记录，则更新单价 --
                            }
                            else
                            {
                                _DALModifyBillHeader.DeleteSMItemPrice(argsInTrans, ItemPriceID);
                            }
                        }
                    }
                }
                #endregion

                _DALModifyBillHeader.UnApprove(argsInTrans, ModifyBillHeaderID);
            };
            DBHelper.ExecInTrans(args, exec);
            */
        }

        public void Cancel(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (bolIsApprove)
                {
                    throw new Exception("该调价单已审核，无法再执行作废操作！");
                }

                if (bolIsCancel)
                {
                    throw new Exception("该调价单已作废，无法在执行作废操作！");
                }
            }

            _DALModifyBillHeader.Cancel(args, ModifyBillHeaderID);
        }

        public void UnCancel(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (bolIsApprove)
                {
                    throw new Exception("该调价单已审核，无法执行反作废操作！");
                }

                if (!bolIsCancel)
                {
                    throw new Exception("该调价单未作废，无法执行反作废操作！");
                }
            }

            _DALModifyBillHeader.UnCancel(args, ModifyBillHeaderID);
        }

        public void Delete(FactoryArgs args, t_BigID ModifyBillHeaderID)
        {
            using (DataTable dtHeader = _DALModifyBillHeader.GetModifyBillHeader(args, ModifyBillHeaderID))
            {
                DataRow drHeader = dtHeader.Rows[0];
                bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                if (bolIsApprove)
                {
                    throw new Exception("该调价单已审核，无法删除！");
                }

                if (bolIsCancel)
                {
                    throw new Exception("该调价单已作废，无法删除！");
                }
            }

            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                using (DataTable dtDetail = _DALModifyBillDetail.GetModifyBillDetailByHeaderID(argsInTrans, ModifyBillHeaderID))
                {
                    foreach (DataRow dr in dtDetail.Rows)
                    {

                    }
                }
                _DALModifyBillHeader.Delete(argsInTrans, ModifyBillHeaderID);
            };
            DBHelper.ExecInTrans(args, exec);
        }

        /// <summary>
        /// 读取客户最新的物料价格
        /// </summary>
        /// <param name="args"></param>
        /// <param name="CustomerID"></param>
        public void GetCustomerLastItemPrice(FactoryArgs args, t_BigID CustomerID)
        {
            DataTable dtResult;
            Dictionary<string, DataRow> dictResult = new Dictionary<string, DataRow>();
            using(DataTable dtModify = _DALModifyBillHeader.GetModifyBillHeaderByCustomer(args, CustomerID))
            {
                dtResult = dtModify.Clone();
                foreach (DataRow dr in dtModify.Rows)
                {
                    long lItemID = LBConverter.ToInt64(dr["ItemID"]);
                    long lCarID = LBConverter.ToInt64(dr["CarID"]);
                    int iCalculateType = LBConverter.ToInt32(dr["CalculateType"]);

                    string strKey = lItemID.ToString() + "-" + lCarID.ToString()+"-"+ iCalculateType.ToString();//唯一标识的主键
                    if (!dictResult.ContainsKey(strKey))
                    {
                        dictResult.Add(strKey, dr);
                    }
                    else
                    {
                        DateTime dtEffectDate = Convert.ToDateTime(dr["EffectDate"]);
                        DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (dtEffectDate.Subtract(dtNow).TotalDays <= 0)//调价生效日期比当前日期前，则生效
                        {
                            DataRow drValue = dictResult[strKey];
                            DateTime dtCurEffectDate = Convert.ToDateTime(drValue["EffectDate"]);

                            double dDiffDays = dtEffectDate.Subtract(dtCurEffectDate).TotalDays;//日期对比
                            if (dDiffDays > 0)//有更加新的生效日期，则将最新的记录替换旧的记录
                            {
                                dictResult[strKey] = dr;
                            }
                            else if (dDiffDays == 0)//如果两个日期一样，则对比审核时间，优先考虑最近的审核时间
                            {
                                DateTime dtApproveTime= Convert.ToDateTime(dr["ApproveTime"]);
                                DateTime dtCurApproveTime = Convert.ToDateTime(drValue["ApproveTime"]);
                                if (dtApproveTime.Subtract(dtCurApproveTime).TotalSeconds > 0)
                                {
                                    dictResult[strKey] = dr;
                                }
                            }
                        }
                    }
                }

                foreach(KeyValuePair<string,DataRow> keyvalue in dictResult)
                {
                    dtResult.ImportRow(keyvalue.Value);
                }
            }
            args.SelectResult = dtResult;
        }

        /// <summary>
        /// 读取物料报价
        /// </summary>
        /// <param name="args"></param>
        /// <param name="ItemID"></param>
        /// <param name="CarID"></param>
        /// <param name="CalculateType"></param>
        /// <param name="Price"></param>
        public void GetCustomerItemPrice(FactoryArgs args,t_BigID ItemID,t_BigID CarID,t_BigID CustomerID, t_ID CalculateType,out t_Decimal Price)
        {
            Price = new t_Decimal(0);
            DataTable dtResult;
            Dictionary<string, DataRow> dictResult = new Dictionary<string, DataRow>();
            using (DataTable dtModify = _DALModifyBillHeader.GetCarItemPrice(args, ItemID, CarID, CustomerID, CalculateType))
            {
                dtResult = dtModify.Clone();
                foreach (DataRow dr in dtModify.Rows)
                {
                    long lItemID = LBConverter.ToInt64(dr["ItemID"]);
                    long lCarID = LBConverter.ToInt64(dr["CarID"]);
                    int iCalculateType = LBConverter.ToInt32(dr["CalculateType"]);
                    //当调价单明细行的车辆ID有值时，必须与传入的CarID值一致
                    if (lCarID==0 || (lCarID > 0 && lCarID == CarID.Value))
                    {
                        string strKey = lItemID.ToString() + "-" + lCarID.ToString() + "-" + iCalculateType.ToString();//唯一标识的主键

                        if (!dictResult.ContainsKey(strKey))
                        {
                            dictResult.Add(strKey, dr);
                            Price.SetValueWithObject(dr["Price"]);
                        }
                        else
                        {
                            DateTime dtEffectDate = Convert.ToDateTime(dr["EffectDate"]);
                            DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            if (dtEffectDate.Subtract(dtNow).TotalDays <= 0)//调价生效日期比当前日期前，则生效
                            {
                                DataRow drValue = dictResult[strKey];
                                DateTime dtCurEffectDate = Convert.ToDateTime(drValue["EffectDate"]);

                                double dDiffDays = dtEffectDate.Subtract(dtCurEffectDate).TotalDays;//日期对比
                                if (dDiffDays > 0)//有更加新的生效日期，则将最新的记录替换旧的记录
                                {
                                    Price.SetValueWithObject(dr["Price"]);
                                    dictResult[strKey] = dr;
                                }
                                else if (dDiffDays == 0)//如果两个日期一样，则对比审核时间，优先考虑最近的审核时间
                                {
                                    DateTime dtApproveTime = Convert.ToDateTime(dr["ApproveTime"]);
                                    DateTime dtCurApproveTime = Convert.ToDateTime(drValue["ApproveTime"]);
                                    if (dtApproveTime.Subtract(dtCurApproveTime).TotalSeconds > 0)
                                    {
                                        dictResult[strKey] = dr;
                                        Price.SetValueWithObject(dr["Price"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Price.Value == 0)
            {
                using (DataTable dtDetail = _DALModifyBillHeader.ReadItem(args,  ItemID))
                {
                    if (dtDetail.Rows.Count > 0)
                    {
                        DataRow drDetail = dtDetail.Rows[0];
                        Price.SetValueWithObject(drDetail["ItemPrice"]);
                    }
                }
            }
        }
    }
}
