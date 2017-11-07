using LB.Web.Base.Base.Helper;
using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.RP.BLL.Factory
{
    public class AssemblyStart
    {
        public AssemblyStart()
        {
            DBHelper.GetBLLObjectMethodEevent += DBHelper_GetBLLObjectMethodEevent;
        }

        private void DBHelper_GetBLLObjectMethodEevent(Base.Base.Factory.GetBLLObjectEventArgs args)
        {
            int iSPType = args.SPType;
            if (iSPType == 0 || args.BLLFunction != null)
            {
                return;
            }

            switch (iSPType)
            {
                case 13300:
                case 13301:
                case 13302:
                case 13303:
                case 13304:
                case 13305:
                case 13306:
                    args.BLLFunction = new BLLRPReceiveBillHeader();
                    break;
                    
            }
        }
    }
}
