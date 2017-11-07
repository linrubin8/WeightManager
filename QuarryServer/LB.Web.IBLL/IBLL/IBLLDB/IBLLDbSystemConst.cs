using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLDB
{
    public interface IBLLDbSystemConst
    {
        void GetConstValue(FactoryArgs args, t_String FieldName,
            t_String ConstValue, out t_String ConstText);
    }
}
