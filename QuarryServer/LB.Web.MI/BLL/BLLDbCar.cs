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
    public class BLLDbCar: IBLLFunction
    {
        private DALDbCar _DALDbCar = null;
        public BLLDbCar()
        {
            _DALDbCar = new DAL.DALDbCar();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 13500:
                    strFunName = "Car_Insert";
                    break;

                case 13501:
                    strFunName = "Car_Update";
                    break;

                case 13502:
                    strFunName = "Car_Delete";
                    break;

                case 13503:
                    strFunName = "Car_RefCustomer";
                    break;
            }
            return strFunName;
        }

        public void Car_Insert(FactoryArgs args, out t_BigID CarID,out t_String CarCode,t_String CarNum, 
            t_BigID CustomerID,t_String Description,t_Decimal DefaultCarWeight)
        {
            CarCode = new t_String();
            CarID = new t_BigID();
            DefaultCarWeight.IsNullToZero();
            using (DataTable dtCar = _DALDbCar.GetCarByName(args, CarID, CarNum))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该车牌号码已存在！");
                }
            }

            t_BigID CarID_temp = new t_BigID();
            t_String CarCode_temp = new t_String();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_String MaxCode;
                _DALDbCar.GetMaxCarCode(argsInTrans,out MaxCode);

                int CodeIndex = MaxCode.Value == null?0:LBConverter.ToInt32( MaxCode.Value.Replace("C", ""));
                CodeIndex++;
                if (CodeIndex < 10)
                    CarCode_temp.SetValueWithObject("C000" + CodeIndex.ToString());
                else if (CodeIndex < 100)
                    CarCode_temp.SetValueWithObject("C00" + CodeIndex.ToString());
                else if (CodeIndex < 1000)
                    CarCode_temp.SetValueWithObject("C0" + CodeIndex.ToString());
                else
                    CarCode_temp.SetValueWithObject( "C"+CodeIndex.ToString());

                _DALDbCar.Car_Insert(argsInTrans, out CarID_temp, CarCode_temp, CarNum,Description, DefaultCarWeight);

                if (CustomerID.Value > 0)
                {
                    _DALDbCar.InsertCustomerCar(argsInTrans, CarID_temp, CustomerID);
                }
            };
            DBHelper.ExecInTrans(args, exec);
            CarID.Value = CarID_temp.Value;
            CarCode.Value = CarCode_temp.Value;
        }

        public void Car_Update(FactoryArgs args, t_BigID CarID, t_String CarNum, 
            t_BigID CustomerID, t_String Description, t_Decimal DefaultCarWeight)
        {
            DefaultCarWeight.IsNullToZero();
            using (DataTable dtCar = _DALDbCar.GetCarByName(args, CarID, CarNum))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该车牌号码已存在！");
                }
            }

            _DALDbCar.Car_Update(args, CarID, CarNum, Description, DefaultCarWeight);
        }

        public void Car_Delete(FactoryArgs args, t_BigID CarID)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALDbCar.DeleteCustomerCarByCarID(argsInTrans, CarID);

                _DALDbCar.Customer_Delete(argsInTrans, CarID);
            };
            DBHelper.ExecInTrans(args, exec);
        }

        public void Car_RefCustomer(FactoryArgs args, t_BigID CarID,t_BigID CustomerID)
        {
            bool bolExists = _DALDbCar.VerifyIsRefCustomer(args,CarID,CustomerID);
            if (!bolExists)
            {
                DataTable dtRefCustomer = _DALDbCar.GetRefCustomer(args, CarID);
                if (dtRefCustomer.Rows.Count > 0)
                {
                    throw new Exception("该车辆已关联了客户【"+ dtRefCustomer.Rows[0]["CustomerName"].ToString().TrimEnd()+ "】,无法再关联其他客户！");
                }
                _DALDbCar.InsertCustomerCar(args, CarID, CustomerID);
            }
        }
    }
}
