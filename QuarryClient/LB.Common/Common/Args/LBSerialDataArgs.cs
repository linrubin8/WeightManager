using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Common.Args
{
    public class LBSerialDataArgs
    {
        public delegate void LBSerialDataEventHandle(LBSerialDataArgs e);

        private string strReceiveData = "";
        public string ReceiveData
        {
            get
            {
                return strReceiveData;
            }
            set
            {
                strReceiveData = value;
            }
        }

        public LBSerialDataArgs(string strData)
        {
            strReceiveData = strData;
        }
    }
}
