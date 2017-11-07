using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.WinFunction.Args
{
    public delegate void CallSPHandle(CallSPArgs e);
    public class CallSPArgs
    {
        private int _SPType = 0;
        public int SPType
        {
            get
            {
                return _SPType;
            }
        }
        private DataTable _SPIN;
        public DataTable DTSPIN
        {
            get
            {
                return _SPIN;
            }
        }

        public CallSPArgs(int iSPType,DataTable dtSPIN)
        {
            _SPType = iSPType;
            _SPIN = dtSPIN;
        }
    }

    public delegate void CallViewHandle(CallViewArgs e);
    public class CallViewArgs
    {
        private int _ViewType = 0;
        public int ViewType
        {
            get
            {
                return _ViewType;
            }
        }
        
        public CallViewArgs(int iViewType )
        {
            _ViewType = iViewType;
        }
    }
}
