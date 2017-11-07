using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.DB.BLL
{
    public class BLLSysViewType : IBLLFunction
    {
        private DAL.DALSysViewType _DALSysViewType = null;
        public BLLSysViewType()
        {
            _DALSysViewType = new DAL.DALSysViewType();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 9000:
                    strFunName = "Insert";
                    break;
                case 9001:
                    strFunName = "Update";
                    break;
                case 9002:
                    strFunName = "Delete";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, out t_BigID SysViewTypeID, t_String SysViewType, t_String SysViewName)
        {
            SysViewTypeID = new t_BigID();

            using (DataTable dtView = _DALSysViewType.VerifyExists(args, SysViewType, SysViewName))
            {
                if (dtView.Rows.Count > 0)
                {
                    DataRow drView = dtView.Rows[0];
                    string strSysViewType = drView["SysViewType"].ToString().TrimEnd();
                    string strSysViewName = drView["SysViewName"].ToString().TrimEnd();
                    if (SysViewType.Value == strSysViewType)
                    {
                        throw new Exception("该视图号已存在！");
                    }
                    if (SysViewName.Value == strSysViewName)
                    {
                        throw new Exception("该视图名称已存在！");
                    }
                }
            }

            _DALSysViewType.Insert(args, out SysViewTypeID, SysViewType, SysViewName);
        }

        public void Update(FactoryArgs args, t_BigID SysViewTypeID, t_String SysViewType, t_String SysViewName)
        {
            using (DataTable dtView = _DALSysViewType.VerifyExists(args, SysViewType, SysViewName))
            {
                if (dtView.Rows.Count > 0)
                {
                    foreach (DataRow drView in dtView.Rows)
                    {
                        long lSysViewTypeID = Convert.ToInt64(drView["SysViewTypeID"]);
                        string strSysViewType = drView["SysViewType"].ToString().TrimEnd();
                        string strSysViewName = drView["SysViewName"].ToString().TrimEnd();
                        if (SysViewTypeID.Value != lSysViewTypeID)
                        {
                            if (SysViewType.Value == strSysViewType)
                            {
                                throw new Exception("该视图号已存在！");
                            }
                            if (SysViewName.Value == strSysViewName)
                            {
                                throw new Exception("该视图名称已存在！");
                            }
                        }
                    }
                }
            }

            _DALSysViewType.Update(args, SysViewTypeID, SysViewType, SysViewName);
        }

        public void Delete(FactoryArgs args, t_BigID SysViewTypeID)
        {
            _DALSysViewType.Delete(args, SysViewTypeID);
        }
    }
}