using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Controls.Args
{
    public class PageEventArgs
    {
        public delegate void GetPageEventHandle(PageEventArgs e);
        private int _PageType;
        private Dictionary<string, object> _PageParms;
        private LBUIPageBase _LBUIPageBase;

        public int PageType
        {
            get
            {
                return _PageType;
            }
        }

        public Dictionary<string, object> PageParms
        {
            get
            {
                return _PageParms;
            }
        }

        public LBUIPageBase PageResult
        {
            get
            {
                return _LBUIPageBase;
            }
            set
            {
                _LBUIPageBase = value;
            }
        }

        public PageEventArgs(int iPageType, Dictionary<string, object> pageParms)
        {
            _PageType = iPageType;
            _PageParms = pageParms;
        }
    }
}
