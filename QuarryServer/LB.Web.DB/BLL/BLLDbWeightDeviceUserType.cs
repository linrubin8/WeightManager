using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbWeightDeviceUserType : IBLLFunction
    {
        private DALDbWeightDeviceUserType _DbWeightDeviceUserType = null;
        public BLLDbWeightDeviceUserType()
        {
            _DbWeightDeviceUserType = new DAL.DALDbWeightDeviceUserType();
        }

        public override string GetFunctionName(int iFunctionType)
        {
            string strFunName = "";
            switch (iFunctionType)
            {
                case 13800:
                    strFunName = "InsertUpdateUserCnofig";
                    break;
            }
            return strFunName;
        }
        
        public void InsertUpdateUserCnofig(FactoryArgs args,
            t_ID WeightDeviceType, t_String MachineName, t_ID DeviceBoTeLv, t_ID DeviceShuJuWei,
            t_ID DeviceTingZhiWei, t_ID DeviceZhenChangDu, t_ID DeviceZhenQiShiBiaoShi, t_ID DeviceZhenChuLiFangShi, t_ID DeviceChongFuWeiZhi,
            t_ID DeviceChongFuChangDu, t_ID DeviceXiaoShuWei, t_ID DeviceFuHaoKaiShi, t_String SerialName)
        {
            DBHelper.ExecInTransDelegate exec = delegate (FactoryArgs argsInTrans)
            {
                t_BigID WeightDeviceUserTypeID = new t_BigID();
                using (DataTable dtUserType = _DbWeightDeviceUserType.GetUserType(argsInTrans, MachineName))
                {
                    if (dtUserType.Rows.Count > 0)
                    {
                        DataRow drUserType = dtUserType.Rows[0];
                        WeightDeviceUserTypeID.SetValueWithObject(drUserType["WeightDeviceUserTypeID"]);

                        _DbWeightDeviceUserType.UpdateUserType(argsInTrans, WeightDeviceUserTypeID, WeightDeviceType, MachineName, SerialName);
                    }
                    else
                    {
                        _DbWeightDeviceUserType.InsertUserType(argsInTrans, out WeightDeviceUserTypeID, WeightDeviceType, MachineName, SerialName);
                    }
                }

                if (WeightDeviceType.Value == 0)
                {
                    using (DataTable dtUserConfig = _DbWeightDeviceUserType.GetUserConfig(argsInTrans, WeightDeviceUserTypeID))
                    {
                        if (dtUserConfig.Rows.Count == 0)
                        {
                            _DbWeightDeviceUserType.InsertUserCnofig(argsInTrans, WeightDeviceUserTypeID, DeviceBoTeLv, DeviceShuJuWei,
                                DeviceTingZhiWei, DeviceZhenChangDu, DeviceZhenQiShiBiaoShi, DeviceZhenChuLiFangShi,
                                DeviceChongFuWeiZhi, DeviceChongFuChangDu, DeviceXiaoShuWei, DeviceFuHaoKaiShi);
                        }
                        else
                        {
                            _DbWeightDeviceUserType.UpdateUserCnofig(argsInTrans, WeightDeviceUserTypeID, DeviceBoTeLv, DeviceShuJuWei,
                                DeviceTingZhiWei, DeviceZhenChangDu, DeviceZhenQiShiBiaoShi, DeviceZhenChuLiFangShi,
                                DeviceChongFuWeiZhi, DeviceChongFuChangDu, DeviceXiaoShuWei, DeviceFuHaoKaiShi);
                        }
                    }
                }
            };
            DBHelper.ExecInTrans(args, exec);
        }
    }
}
