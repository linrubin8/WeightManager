using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.MI.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.MI.BLL
{
    public class BLLDbCustomer : IBLLFunction
    {
        private DALDbCustomer _DALDbCustomer = null;
        private DALDbCar _DALDbCar = null;
        public BLLDbCustomer()
        {
            _DALDbCar = new DAL.DALDbCar();
            _DALDbCustomer = new DAL.DALDbCustomer();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 13400:
                    strFunName = "Customer_Insert";
                    break;

                case 13401:
                    strFunName = "Customer_Update";
                    break;

                case 13402:
                    strFunName = "Customer_Delete";
                    break;

                case 13403:
                    strFunName = "Customer_InsertFromService";
                    break;

                case 13404:
                    strFunName = "Customer_UpdateFromService";
                    break;

                case 13405:
                    strFunName = "Forbid";
                    break;

                case 13406:
                    strFunName = "UnForbid";
                    break;
            }
            return strFunName;
        }

        public void Customer_Insert(FactoryArgs args, out t_BigID CustomerID, out t_String CustomerCode, t_String CustomerName, t_String Contact, t_String Phone, t_String Address,
            t_Bool CarIsLimit, t_ID AmountType, t_String LicenceNum, t_String Description, t_ID ReceiveType,
            t_Decimal CreditAmount, t_Bool IsDisplayPrice, t_Bool IsDisplayAmount, t_Bool IsPrintAmount, t_Bool IsAllowOverFul,
            t_Bool IsAllowEmptyIn, t_Decimal AmountNotEnough, t_String K3CustomerCode)
        {
            CustomerCode = new t_String();
            CustomerID = new t_BigID();
            IsAllowEmptyIn.IsNullToZero();

            using (DataTable dtCustomer = _DALDbCustomer.GetCustomerByName(args, CustomerID, CustomerName))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    throw new Exception("该客户名称已存在！");
                }
            }

            t_String MaxCode;
            _DALDbCustomer.GetMaxCode(args, out MaxCode);
            int CodeIndex = MaxCode.Value == null ? 0 : LBConverter.ToInt32(MaxCode.Value.Replace("K", ""));
            CodeIndex++;
            if (CodeIndex < 10)
                CustomerCode.SetValueWithObject("K000" + CodeIndex.ToString());
            else if (CodeIndex < 100)
                CustomerCode.SetValueWithObject("K00" + CodeIndex.ToString());
            else if (CodeIndex < 1000)
                CustomerCode.SetValueWithObject("K0" + CodeIndex.ToString());
            else
                CustomerCode.SetValueWithObject("K" + CodeIndex.ToString());

            _DALDbCustomer.Customer_Insert(args, out CustomerID, CustomerCode, CustomerName, Contact, Phone, Address, CarIsLimit, AmountType, LicenceNum, Description,
                ReceiveType, CreditAmount, IsDisplayPrice, IsDisplayAmount, IsPrintAmount, IsAllowOverFul, IsAllowEmptyIn, AmountNotEnough,
                K3CustomerCode);
        }

        public void Customer_Update(FactoryArgs args, t_BigID CustomerID, t_String CustomerName, t_String Contact, t_String Phone, t_String Address,
            t_Bool CarIsLimit, t_ID AmountType, t_String LicenceNum, t_String Description, t_ID ReceiveType,
            t_Decimal CreditAmount, t_Bool IsDisplayPrice, t_Bool IsDisplayAmount, t_Bool IsPrintAmount, t_Bool IsAllowOverFul,
            t_Bool IsAllowEmptyIn, t_Decimal AmountNotEnough, t_String K3CustomerCode)
        {
            IsAllowEmptyIn.IsNullToZero();
            using (DataTable dtCustomer = _DALDbCustomer.GetCustomerByName(args, CustomerID, CustomerName))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    throw new Exception("该客户名称已存在！");
                }
            }

            _DALDbCustomer.Customer_Update(args, CustomerID, CustomerName, Contact, Phone, Address, CarIsLimit, AmountType, LicenceNum, Description,
                ReceiveType, CreditAmount, IsDisplayPrice, IsDisplayAmount, IsPrintAmount, IsAllowOverFul, IsAllowEmptyIn, AmountNotEnough,
                K3CustomerCode);
        }

        public void Customer_Delete(FactoryArgs args, t_BigID CustomerID)
        {
            using (DataTable dtCar = _DALDbCustomer.GetCarByCustomer(args, CustomerID))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该客户已关联车辆，无法删除！");
                }
            }
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALDbCar.DeleteCustomerCarByCustomerID(argsInTrans, CustomerID);

                _DALDbCustomer.Customer_Delete(args, CustomerID);
            };
            DBHelper.ExecInTrans(args, exec);

        }

        public void Customer_InsertFromService(FactoryArgs args, out t_BigID CustomerID, out t_String CustomerCode, t_String CustomerName, t_String Contact, t_String Phone, t_String Address,
            t_Bool CarIsLimit, t_ID AmountType, t_String LicenceNum, t_String Description, t_Bool IsForbid, t_ID ReceiveType,
            t_Decimal CreditAmount, t_Bool IsDisplayPrice, t_Bool IsDisplayAmount, t_Bool IsPrintAmount, t_Bool IsAllowOverFul,
            t_Bool IsAllowEmptyIn, t_Decimal AmountNotEnough, t_Decimal TotalReceivedAmount, t_Decimal SalesReceivedAmount, t_String K3CustomerCode)
        {
            CustomerID = new t_BigID();
            CustomerCode = new t_String();
            t_BigID CustomerIDTemp = new t_BigID();
            t_String CustomerCodeTemp = new t_String();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                this.Customer_Insert(argsInTrans, out CustomerIDTemp, out CustomerCodeTemp, CustomerName, Contact, Phone, Address, CarIsLimit, AmountType, LicenceNum, Description,
                 ReceiveType, CreditAmount, IsDisplayPrice, IsDisplayAmount, IsPrintAmount, IsAllowOverFul, IsAllowEmptyIn, AmountNotEnough, K3CustomerCode);

                _DALDbCustomer.UpdateAmount(argsInTrans, CustomerIDTemp, TotalReceivedAmount, SalesReceivedAmount);
            };
            DBHelper.ExecInTrans(args, exec);
            CustomerID.Value = CustomerIDTemp.Value;
            CustomerCode.Value = CustomerCodeTemp.Value;
        }

        public void Customer_UpdateFromService(FactoryArgs args, t_BigID CustomerID, t_String CustomerName, t_String Contact, t_String Phone, t_String Address,
            t_Bool CarIsLimit, t_ID AmountType, t_String LicenceNum, t_String Description, t_Bool IsForbid, t_ID ReceiveType,
            t_Decimal CreditAmount, t_Bool IsDisplayPrice, t_Bool IsDisplayAmount, t_Bool IsPrintAmount, t_Bool IsAllowOverFul,
            t_Bool IsAllowEmptyIn, t_Decimal AmountNotEnough, t_Decimal TotalReceivedAmount, t_Decimal SalesReceivedAmount, t_String K3CustomerCode)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                this.Customer_Update(argsInTrans, CustomerID, CustomerName, Contact, Phone, Address, CarIsLimit, AmountType, LicenceNum, Description,
                    ReceiveType, CreditAmount, IsDisplayPrice, IsDisplayAmount, IsPrintAmount, IsAllowOverFul, IsAllowEmptyIn, AmountNotEnough,
                K3CustomerCode);

                _DALDbCustomer.UpdateAmount(argsInTrans, CustomerID, TotalReceivedAmount, SalesReceivedAmount);

                IsForbid.IsNullToZero();
                if (IsForbid.Value == 1)
                {
                    _DALDbCustomer.Forbid(args, CustomerID);
                }
                else
                {
                    _DALDbCustomer.UnForbid(args, CustomerID);
                }
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void Forbid(FactoryArgs args, t_BigID CustomerID)
        {
            using (DataTable dtCustomer = _DALDbCustomer.GetCustomerByID(args, CustomerID))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    bool bolIsForbid = LBConverter.ToBoolean(dtCustomer.Rows[0]["IsForbid"]);
                    if (bolIsForbid)
                    {
                        throw new Exception("该客户已禁用！");
                    }
                }
            }
            _DALDbCustomer.Forbid(args, CustomerID);
        }

        public void UnForbid(FactoryArgs args, t_BigID CustomerID)
        {
            using (DataTable dtCustomer = _DALDbCustomer.GetCustomerByID(args, CustomerID))
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    bool bolIsForbid = LBConverter.ToBoolean(dtCustomer.Rows[0]["IsForbid"]);
                    if (!bolIsForbid)
                    {
                        throw new Exception("该客户未禁用，无法取消禁用！");
                    }
                }
            }
            _DALDbCustomer.UnForbid(args, CustomerID);
        }
    }
}
