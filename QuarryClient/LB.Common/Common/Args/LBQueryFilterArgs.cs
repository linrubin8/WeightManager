using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Common.Args
{
    public class LBQueryFilterArgs
    {
        public delegate void GetCustomFilterEventHandle(LBQueryFilterArgs e);

        private string strFilter = "";
        public string Filter
        {
            get
            {
                return strFilter;
            }
            set
            {
                strFilter = value;
            }
        }

        public LBQueryFilterArgs()
        {

        }
    }
}
