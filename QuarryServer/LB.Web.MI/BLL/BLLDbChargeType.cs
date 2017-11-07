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
    public class BLLDbChargeType : IBLLFunction
    {
        private DALDbChargeType _DALDbChargeType = null;
        public BLLDbChargeType()
        {
            _DALDbChargeType = new DAL.DALDbChargeType();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 14700:
                    strFunName = "ChargeType_Insert";
                    break;

                case 14701:
                    strFunName = "ChargeType_Update";
                    break;

                case 14702:
                    strFunName = "ChargeType_Delete";
                    break;
            }
            return strFunName;
        }

        public void ChargeType_Insert(FactoryArgs args, out t_BigID ChargeTypeID, 
            out t_String ChargeTypeCode, t_String ChargeTypeName)
        {
            ChargeTypeCode = new t_String();
            ChargeTypeID = new t_BigID();
            using (DataTable dtCar = _DALDbChargeType.GetChargeTypeByName(args, ChargeTypeID, ChargeTypeName))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该收款方式已存在！");
                }
            }

            t_BigID ChargeTypeID_temp = new t_BigID();
            t_String ChargeTypeCode_temp = new t_String();
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_String MaxCode;
                _DALDbChargeType.GetMaxCarCode(argsInTrans,out MaxCode);
                int CodeIndex = MaxCode.Value==null?0: LBConverter.ToInt32( MaxCode.Value.Replace("F", ""));
                CodeIndex++;
                if (CodeIndex < 10)
                    ChargeTypeCode_temp.SetValueWithObject("F0" + CodeIndex.ToString());
                else
                    ChargeTypeCode_temp.SetValueWithObject( "F"+CodeIndex.ToString());

                _DALDbChargeType.ChargeType_Insert(argsInTrans, out ChargeTypeID_temp, ChargeTypeCode_temp, ChargeTypeName);
            };
            DBHelper.ExecInTrans(args, exec);
            ChargeTypeID.Value = ChargeTypeID_temp.Value;
            ChargeTypeCode.Value = ChargeTypeCode_temp.Value;
        }

        public void ChargeType_Update(FactoryArgs args, t_BigID ChargeTypeID, t_String ChargeTypeName)
        {
            using (DataTable dtCar = _DALDbChargeType.GetChargeTypeByName(args, ChargeTypeID, ChargeTypeName))
            {
                if (dtCar.Rows.Count > 0)
                {
                    throw new Exception("该收款方式已存在！");
                }
            }

            _DALDbChargeType.ChargeType_Update(args, ChargeTypeID, ChargeTypeName);
        }

        public void ChargeType_Delete(FactoryArgs args, t_BigID ChargeTypeID)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                _DALDbChargeType.ChargeType_Delete(argsInTrans, ChargeTypeID);
            };
            DBHelper.ExecInTrans(args, exec);
        }
    }
}
