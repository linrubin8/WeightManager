using LB.Web.Base.Factory;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.IBLL.IBLL.IBLLMI
{
    public interface IBLLDbCarWeight
    {
        void Insert(FactoryArgs args, t_BigID CarID, t_Decimal CarWeight, t_String Description);
    }
}
