using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbWeightDeviceUserType
    {
        public void InsertUserType(FactoryArgs args,
           out t_BigID WeightDeviceUserTypeID,t_ID WeightDeviceType,t_String MachineName, t_String SerialName)
        {
            WeightDeviceUserTypeID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightDeviceUserTypeID", WeightDeviceUserTypeID,true));
            parms.Add(new LBDbParameter("WeightDeviceType", WeightDeviceType));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("SerialName", SerialName));

            string strSQL = @"
insert into dbo.DbWeightDeviceUserType( WeightDeviceType,MachineName,SerialName )
values( @WeightDeviceType, @MachineName,@SerialName)

set @WeightDeviceUserTypeID = @@identity

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            WeightDeviceUserTypeID.SetValueWithObject(parms["WeightDeviceUserTypeID"].Value);
        }

        public void UpdateUserType(FactoryArgs args,
           t_BigID WeightDeviceUserTypeID, t_ID WeightDeviceType, t_String MachineName, t_String SerialName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightDeviceUserTypeID", WeightDeviceUserTypeID));
            parms.Add(new LBDbParameter("WeightDeviceType", WeightDeviceType));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("SerialName", SerialName));

            string strSQL = @"
update dbo.DbWeightDeviceUserType
set MachineName = @MachineName,
    WeightDeviceType = @WeightDeviceType,
    SerialName = @SerialName
where WeightDeviceUserTypeID = @WeightDeviceUserTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetUserType(FactoryArgs args, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));

            string strSQL = @"
select *
from dbo.DbWeightDeviceUserType
where MachineName = @MachineName
";
           return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public DataTable GetUserConfig(FactoryArgs args, t_BigID WeightDeviceUserTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightDeviceUserTypeID", WeightDeviceUserTypeID));

            string strSQL = @"
select *
from dbo.DbWeightDeviceUserConfig
where WeightDeviceUserTypeID = @WeightDeviceUserTypeID
";
            return DBHelper.ExecuteQuery(args, strSQL, parms);
        }

        public void InsertUserCnofig(FactoryArgs args,
            t_BigID WeightDeviceUserTypeID, t_ID DeviceBoTeLv, t_ID DeviceShuJuWei,
            t_ID DeviceTingZhiWei, t_ID DeviceZhenChangDu, t_ID DeviceZhenQiShiBiaoShi, t_ID DeviceZhenChuLiFangShi, t_ID DeviceChongFuWeiZhi,
            t_ID DeviceChongFuChangDu, t_ID DeviceXiaoShuWei, t_ID DeviceFuHaoKaiShi)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightDeviceUserTypeID", WeightDeviceUserTypeID));
            parms.Add(new LBDbParameter("DeviceBoTeLv", DeviceBoTeLv));
            parms.Add(new LBDbParameter("DeviceShuJuWei", DeviceShuJuWei));
            parms.Add(new LBDbParameter("DeviceTingZhiWei", DeviceTingZhiWei));
            parms.Add(new LBDbParameter("DeviceZhenChangDu", DeviceZhenChangDu));
            parms.Add(new LBDbParameter("DeviceZhenQiShiBiaoShi", DeviceZhenQiShiBiaoShi));
            parms.Add(new LBDbParameter("DeviceZhenChuLiFangShi", DeviceZhenChuLiFangShi));
            parms.Add(new LBDbParameter("DeviceChongFuWeiZhi", DeviceChongFuWeiZhi));
            parms.Add(new LBDbParameter("DeviceChongFuChangDu", DeviceChongFuChangDu));
            parms.Add(new LBDbParameter("DeviceXiaoShuWei", DeviceXiaoShuWei));
            parms.Add(new LBDbParameter("DeviceFuHaoKaiShi", DeviceFuHaoKaiShi));

            string strSQL = @"
insert into dbo.DbWeightDeviceUserConfig(  WeightDeviceUserTypeID, DeviceBoTeLv, DeviceShuJuWei, DeviceTingZhiWei, DeviceZhenChangDu, 
DeviceZhenQiShiBiaoShi, DeviceZhenChuLiFangShi, DeviceChongFuWeiZhi, DeviceChongFuChangDu, DeviceXiaoShuWei, 
DeviceFuHaoKaiShi )
values( @WeightDeviceUserTypeID, @DeviceBoTeLv, @DeviceShuJuWei, @DeviceTingZhiWei, @DeviceZhenChangDu, 
@DeviceZhenQiShiBiaoShi, @DeviceZhenChuLiFangShi, @DeviceChongFuWeiZhi, @DeviceChongFuChangDu, @DeviceXiaoShuWei, 
@DeviceFuHaoKaiShi)

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UpdateUserCnofig(FactoryArgs args,
            t_BigID WeightDeviceUserTypeID, t_ID DeviceBoTeLv, t_ID DeviceShuJuWei,
            t_ID DeviceTingZhiWei, t_ID DeviceZhenChangDu, t_ID DeviceZhenQiShiBiaoShi, t_ID DeviceZhenChuLiFangShi, t_ID DeviceChongFuWeiZhi,
            t_ID DeviceChongFuChangDu, t_ID DeviceXiaoShuWei, t_ID DeviceFuHaoKaiShi)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("WeightDeviceUserTypeID", WeightDeviceUserTypeID));
            parms.Add(new LBDbParameter("DeviceBoTeLv", DeviceBoTeLv));
            parms.Add(new LBDbParameter("DeviceShuJuWei", DeviceShuJuWei));
            parms.Add(new LBDbParameter("DeviceTingZhiWei", DeviceTingZhiWei));
            parms.Add(new LBDbParameter("DeviceZhenChangDu", DeviceZhenChangDu));
            parms.Add(new LBDbParameter("DeviceZhenQiShiBiaoShi", DeviceZhenQiShiBiaoShi));
            parms.Add(new LBDbParameter("DeviceZhenChuLiFangShi", DeviceZhenChuLiFangShi));
            parms.Add(new LBDbParameter("DeviceChongFuWeiZhi", DeviceChongFuWeiZhi));
            parms.Add(new LBDbParameter("DeviceChongFuChangDu", DeviceChongFuChangDu));
            parms.Add(new LBDbParameter("DeviceXiaoShuWei", DeviceXiaoShuWei));
            parms.Add(new LBDbParameter("DeviceFuHaoKaiShi", DeviceFuHaoKaiShi));

            string strSQL = @"
update dbo.DbWeightDeviceUserConfig
set DeviceBoTeLv=@DeviceBoTeLv,  
    DeviceShuJuWei=@DeviceShuJuWei,  
    DeviceTingZhiWei=@DeviceTingZhiWei,  
    DeviceZhenChangDu=@DeviceZhenChangDu, 
    DeviceZhenQiShiBiaoShi=@DeviceZhenQiShiBiaoShi,  
    DeviceZhenChuLiFangShi=@DeviceZhenChuLiFangShi,  
    DeviceChongFuWeiZhi=@DeviceChongFuWeiZhi,  
    DeviceChongFuChangDu=@DeviceChongFuChangDu,  
    DeviceXiaoShuWei=@DeviceXiaoShuWei, 
    DeviceFuHaoKaiShi=@DeviceFuHaoKaiShi
where WeightDeviceUserTypeID = @WeightDeviceUserTypeID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
