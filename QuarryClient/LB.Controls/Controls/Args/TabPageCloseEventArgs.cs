using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Args
{
    public delegate void TabPageClosingEventHandle(object sender, TabPageClosingEventArgs e);
    public delegate void TabPageClosedEventHandle(object sender, TabPageClosedEventArgs e);
    public class TabPageClosingEventArgs
    {
        private TabPage _tpClose;
        public TabPage TabPageClose
        {
            get
            {
                return _tpClose;
            }
        }
        private bool _Cancel;
        public bool Cancel
        {
            get
            {
                return _Cancel;
            }
            set
            {
                _Cancel = value;
            }
        }

        public TabPageClosingEventArgs(TabPage tpClose,bool bolCancel)
        {
            _tpClose = tpClose;
            _Cancel = bolCancel;
        }
    }

    public class TabPageClosedEventArgs
    {
        private TabPage _tpClose;
        public TabPage TabPageClose
        {
            get
            {
                return _tpClose;
            }
        }
        public TabPageClosedEventArgs(TabPage tpClose)
        {
            _tpClose = tpClose;
        }
    }
}
