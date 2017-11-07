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
    public class BLLDbCameraConfig : IBLLFunction
    {
        private DALDbCameraConfig _DALDbCameraConfig = null;
        public BLLDbCameraConfig()
        {
            _DALDbCameraConfig = new DAL.DALDbCameraConfig();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13900:
                    strFunName = "InsertAndUpdate";
                    break;
            }
            return strFunName;
        }

        public void InsertAndUpdate(FactoryArgs args,
           t_BigID CameraConfigID, t_String MachineName,
           t_String IPAddress1, t_ID Port1, t_String Account1, t_String Password1,t_Bool UseCamera1,
           t_String IPAddress2, t_ID Port2, t_String Account2, t_String Password2, t_Bool UseCamera2,
           t_String IPAddress3, t_ID Port3, t_String Account3, t_String Password3, t_Bool UseCamera3,
           t_String IPAddress4, t_ID Port4, t_String Account4, t_String Password4, t_Bool UseCamera4)
        {
            using(DataTable dtExists= _DALDbCameraConfig.VerifyExists(args, MachineName))
            {
                if (dtExists.Rows.Count == 0)
                {
                    _DALDbCameraConfig.Insert(args, out CameraConfigID, MachineName, IPAddress1, Port1, Account1, Password1, UseCamera1,
                        IPAddress2, Port2, Account2, Password2, UseCamera2, IPAddress3, Port3, Account3, Password3, UseCamera3,
                        IPAddress4, Port4, Account4, Password4, UseCamera4);
                }
                else
                {
                    CameraConfigID = new t_BigID(dtExists.Rows[0]["CameraConfigID"]);
                    _DALDbCameraConfig.Update(args, CameraConfigID, MachineName, IPAddress1, Port1, Account1, Password1, UseCamera1,
                        IPAddress2, Port2, Account2, Password2, UseCamera2, IPAddress3, Port3, Account3, Password3, UseCamera3,
                        IPAddress4, Port4, Account4, Password4, UseCamera4);
                }
            }
        }
        
    }
}
