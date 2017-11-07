using LB.Web.Base.Base.Helper;
using System;
using System.Collections.Generic;
using System.Web;

namespace LB.Web.Base.Helper
{
    public class IBLLFunction: IBllObject
    {
        public virtual string GetFunctionName(int iFunctionType)
        {
            return "";
        }
    }
}