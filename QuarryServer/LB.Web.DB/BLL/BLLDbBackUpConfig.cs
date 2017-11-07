using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbBackUpConfig : IBLLFunction
    {
        private DALDbBackUpConfig _DALDbBackUpConfig = null;
        public BLLDbBackUpConfig()
        {
            _DALDbBackUpConfig = new DAL.DALDbBackUpConfig();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 13200:
                    strFunName = "Insert";
                    break;
                case 13201:
                    strFunName = "Update";
                    break;
                case 13202:
                    strFunName = "Delete";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args,
           out t_BigID BackUpConfigID, t_ID BackUpType, t_ID BackUpWeek, t_ID BackUpHour, t_ID BackUpMinu, t_Bool IsEffect,
           t_ID BackUpFileMaxNum, t_String BackUpPath, t_String BackUpName)
        {
            _DALDbBackUpConfig.Insert(args, out BackUpConfigID, BackUpType, BackUpWeek, BackUpHour, BackUpMinu, IsEffect, BackUpFileMaxNum,
                BackUpPath, BackUpName);
        }

        public void Update(FactoryArgs args,
           t_BigID BackUpConfigID, t_ID BackUpType, t_ID BackUpWeek, t_ID BackUpHour, t_ID BackUpMinu, t_Bool IsEffect,
           t_ID BackUpFileMaxNum, t_String BackUpPath, t_String BackUpName)
        {
            _DALDbBackUpConfig.Update(args, BackUpConfigID, BackUpType, BackUpWeek, BackUpHour, BackUpMinu, IsEffect, BackUpFileMaxNum,
                BackUpPath, BackUpName);
        }

        public void Delete(FactoryArgs args,
           t_BigID BackUpConfigID)
        {
            _DALDbBackUpConfig.Delete(args, BackUpConfigID);
        }
    }
}
