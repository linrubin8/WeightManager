using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLDB
{
    public interface IBLLDbSysConfig
    {
        void GetConfigValue(FactoryArgs args, t_String SysConfigFieldName, out t_String SysConfigValue);
    }
}
