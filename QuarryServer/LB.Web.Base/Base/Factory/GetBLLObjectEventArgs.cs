using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Web.Base.Base.Factory
{
    public delegate void GetBLLObjectMethod(GetBLLObjectEventArgs args);

    public class GetBLLObjectEventArgs
    {
        private int _SPType;
        public int SPType
        {
            get
            {
                return _SPType;
            }
            set
            {
                _SPType = value;
            }
        }

        private IBLLFunction _Function;
        public IBLLFunction BLLFunction
        {
            get
            {
                return _Function;
            }
            set
            {
                _Function = value;
            }
        }

        public GetBLLObjectEventArgs(int iSPType)
        {
            _SPType = iSPType;
        }
    }

    public delegate void StopServerHandle(EventArgs args);

}
