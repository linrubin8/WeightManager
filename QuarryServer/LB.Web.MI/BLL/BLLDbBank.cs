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
    public class BLLDbBank : IBLLFunction
    {
        private DALDbBank _DALDbBank = null;
        public BLLDbBank()
        {
            _DALDbBank = new DAL.DALDbBank();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 14600:
                    strFunName = "Bank_Insert";
                    break;

                case 14601:
                    strFunName = "Bank_Update";
                    break;

                case 14602:
                    strFunName = "Bank_Delete";
                    break;
            }
            return strFunName;
        }

        public void Bank_Insert(FactoryArgs args, out t_BigID ReceiveBankID, out t_String BankCode, 
            t_String BankName)
        {
            BankCode = new t_String();
            ReceiveBankID = new t_BigID();
            using (DataTable dtCar = _DALDbBank.GetBankByName(args, ReceiveBankID, BankName))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该收款银行名称已存在！");
                }
            }

            t_BigID BankID_temp = new t_BigID();
            t_String BankCode_temp = new t_String();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_String MaxCode;
                _DALDbBank.GetMaxCarCode(argsInTrans,out MaxCode);
                int CodeIndex = MaxCode.Value==null?0: LBConverter.ToInt32( MaxCode.Value.Replace("Y", ""));
                CodeIndex++;
                if (CodeIndex < 10)
                    BankCode_temp.SetValueWithObject("Y0" + CodeIndex.ToString());
                else
                    BankCode_temp.SetValueWithObject( "Y"+CodeIndex.ToString());

                _DALDbBank.Bank_Insert(argsInTrans, out BankID_temp, BankCode_temp, BankName);
            };
            DBHelper.ExecInTrans(args, exec);
            ReceiveBankID.Value = BankID_temp.Value;
            BankCode.Value = BankCode_temp.Value;
        }

        public void Bank_Update(FactoryArgs args, t_BigID ReceiveBankID,t_String BankName)
        {
            using (DataTable dtCar = _DALDbBank.GetBankByName(args, ReceiveBankID, BankName))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该收款银行名称已存在！");
                }
            }

            _DALDbBank.Bank_Update(args, ReceiveBankID, BankName);
        }

        public void Bank_Delete(FactoryArgs args, t_BigID ReceiveBankID)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALDbBank.Bank_Delete(argsInTrans, ReceiveBankID);
            };
            DBHelper.ExecInTrans(args, exec);
        }
    }
}
