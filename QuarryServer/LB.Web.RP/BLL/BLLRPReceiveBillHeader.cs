using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.IBLL.IBLL.IBLLRP;
using LB.Web.RP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.RP.BLL
{
    public class BLLRPReceiveBillHeader : IBLLFunction, IBLLRPReceiveBillHeader
    {
        private DALRPReceiveBillHeader _DALRPReceiveBillHeader = null;
        public BLLRPReceiveBillHeader()
        {
            _DALRPReceiveBillHeader = new DAL.DALRPReceiveBillHeader();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13300:
                    strFunName = "Insert";
                    break;
                case 13301:
                    strFunName = "Update";
                    break;
                case 13302:
                    strFunName = "Delete";
                    break;
                case 13303:
                    strFunName = "Approve";
                    break;
                case 13304:
                    strFunName = "UnApprove";
                    break;
                case 13305:
                    strFunName = "Cancel";
                    break;
                case 13306:
                    strFunName = "UnCancel";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, out t_BigID ReceiveBillHeaderID, out t_String ReceiveBillCode, t_DTSmall BillDate, t_BigID CustomerID, t_Decimal ReceiveAmount,
            t_String Description, t_BigID SaleCarInBillID,t_BigID ReceiveBankID,t_ID RPReceiveType,
            t_Decimal SalesReceiveAmountAdd,t_Decimal SalesReceiveAmountReduce,t_Decimal OriginalAmount,
            t_BigID ChargeTypeID)
        {
            BillDate = new t_DTSmall(DateTime.Now);
            ReceiveBankID.NullIfZero();
            RPReceiveType.IsNullToZero();
            ReceiveBillCode = new t_String();

            if (ReceiveAmount.Value == 0)
            {
                throw new Exception("充值金额不能为0！");
            }
            //生成编码
            string strBillFont = "SK" + DateTime.Now.ToString("yyyyMM")+"-";
            using (DataTable dtBillCode = _DALRPReceiveBillHeader.GetMaxBillCode(args, strBillFont))
            {
                if (dtBillCode.Rows.Count > 0)
                {
                    DataRow drBillCode = dtBillCode.Rows[0];
                    int iIndex = 1;
                    string strIndex = "";
                    if (drBillCode["ReceiveBillCode"].ToString().TrimEnd().Contains(strBillFont))
                    {
                        iIndex = Convert.ToInt32(drBillCode["ReceiveBillCode"].ToString().TrimEnd().Replace(strBillFont, ""));
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
                        ReceiveBillCode.SetValueWithObject(strBillFont + strIndex);
                    }
                    else
                    {
                        ReceiveBillCode.SetValueWithObject(strBillFont + "00001");
                    }
                }
                else
                {
                    ReceiveBillCode.SetValueWithObject(strBillFont + "00001");
                }
            }

            _DALRPReceiveBillHeader.Insert(args, out ReceiveBillHeaderID, ReceiveBillCode, BillDate, CustomerID, ReceiveAmount, Description, SaleCarInBillID,
                ReceiveBankID,RPReceiveType, SalesReceiveAmountAdd,SalesReceiveAmountReduce,OriginalAmount,ChargeTypeID);
        }

        public void Update(FactoryArgs args, t_BigID ReceiveBillHeaderID, t_DTSmall BillDate, t_Decimal ReceiveAmount,
            t_String Description, t_BigID ReceiveBankID, t_ID RPReceiveType, t_Decimal SalesReceiveAmountAdd, 
            t_Decimal SalesReceiveAmountReduce, t_Decimal OriginalAmount, t_BigID ChargeTypeID)
        {
            RPReceiveType.IsNullToZero();

            if (ReceiveAmount.Value == 0)
            {
                throw new Exception("充值金额不能为0！");
            }

            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    if (bolIsApprove)
                    {
                        throw new Exception("该充值单已审核，无法进行修改！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法进行修改！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法进行修改！");
                }
            }
            _DALRPReceiveBillHeader.Update(args, ReceiveBillHeaderID, BillDate, ReceiveAmount, 
                Description, ReceiveBankID, RPReceiveType, SalesReceiveAmountAdd, SalesReceiveAmountReduce, OriginalAmount,
                ChargeTypeID);
        }

        public void Delete(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    if (bolIsApprove)
                    {
                        throw new Exception("该充值单已审核，无法进行删除！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法进行删除！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法进行删除！");
                }
            }
            _DALRPReceiveBillHeader.Delete(args, ReceiveBillHeaderID);
        }

        public void Approve(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            t_Decimal ReceiveAmount = new t_Decimal(0);
            t_Decimal SalesReceiveAmountAdd = new t_Decimal(0);
            t_Decimal SalesReceiveAmountReduce = new t_Decimal(0);
            t_BigID CustomerID = new t_BigID();
            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    ReceiveAmount.SetValueWithObject(drHeader["ReceiveAmount"]);
                    SalesReceiveAmountAdd.SetValueWithObject(drHeader["SalesReceiveAmountAdd"]);
                    SalesReceiveAmountReduce.SetValueWithObject(drHeader["SalesReceiveAmountReduce"]);
                    CustomerID.SetValueWithObject(drHeader["CustomerID"]);
                    if (bolIsApprove)
                    {
                        throw new Exception("该充值单已审核，无法再进行审核！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法进行审核！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法进行审核！");
                }
            }
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALRPReceiveBillHeader.Approve(argsInTrans, ReceiveBillHeaderID);

                //修改客户收款金额
                _DALRPReceiveBillHeader.UpdateCustomerReceiveAmount(argsInTrans, CustomerID, ReceiveAmount, SalesReceiveAmountAdd, SalesReceiveAmountReduce);
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void UnApprove(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            t_Decimal ReceiveAmount = new t_Decimal(0);
            t_BigID CustomerID = new t_BigID();
            t_Decimal SalesReceiveAmountAdd = new t_Decimal(0);
            t_Decimal SalesReceiveAmountReduce = new t_Decimal(0);
            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    ReceiveAmount.SetValueWithObject(drHeader["ReceiveAmount"]);
                    SalesReceiveAmountAdd.SetValueWithObject(drHeader["SalesReceiveAmountAdd"]);
                    SalesReceiveAmountReduce.SetValueWithObject(drHeader["SalesReceiveAmountReduce"]);
                    CustomerID.SetValueWithObject(drHeader["CustomerID"]);
                    if (!bolIsApprove)
                    {
                        throw new Exception("该充值单未审核，无法执行取消审核！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法执行取消审核！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法执行取消审核！");
                }
            }
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALRPReceiveBillHeader.UnApprove(argsInTrans, ReceiveBillHeaderID);
                //扣减客户收款金额
                ReceiveAmount.SetValueWithObject(-ReceiveAmount.Value);
                SalesReceiveAmountAdd.SetValueWithObject(-SalesReceiveAmountAdd.Value);
                SalesReceiveAmountReduce.SetValueWithObject(-SalesReceiveAmountReduce.Value);
                _DALRPReceiveBillHeader.UpdateCustomerReceiveAmount(argsInTrans, CustomerID, ReceiveAmount, SalesReceiveAmountAdd, SalesReceiveAmountReduce);
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void Cancel(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    if (bolIsApprove)
                    {
                        throw new Exception("该充值单已审核，无法再进行作废！");
                    }
                    if (bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法进行作废！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法进行作废！");
                }
            }
            _DALRPReceiveBillHeader.Cancel(args, ReceiveBillHeaderID);
        }

        public void UnCancel(FactoryArgs args, t_BigID ReceiveBillHeaderID)
        {
            using (DataTable dtHeader = _DALRPReceiveBillHeader.GetRPReceiveBillHeader(args, ReceiveBillHeaderID))
            {
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];
                    bool bolIsApprove = LBConverter.ToBoolean(drHeader["IsApprove"]);
                    bool bolIsCancel = LBConverter.ToBoolean(drHeader["IsCancel"]);
                    if (bolIsApprove)
                    {
                        throw new Exception("该充值单未审核，无法执行取消审核！");
                    }
                    if (!bolIsCancel)
                    {
                        throw new Exception("该充值单已作废，无法执行取消审核！");
                    }
                }
                else
                {
                    throw new Exception("该充值单已删除，无法执行取消审核！");
                }
            }
            _DALRPReceiveBillHeader.UnCancel(args, ReceiveBillHeaderID);
        }
    }
}
