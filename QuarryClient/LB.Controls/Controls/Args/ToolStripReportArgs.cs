using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Args
{
    public class ToolStripReportArgs
    {
        private ToolStrip _ToolStrip;
        public ToolStrip LBToolStrip
        {
            set
            {
                _ToolStrip = value;
            }
            get
            {
                return _ToolStrip;
            }
        }
        private long _ReportTypeID;
        public long ReportTypeID
        {
            set
            {
                _ReportTypeID = value;
            }
            get
            {
                return _ReportTypeID;
            }
        }
        public ToolStripReportArgs()
        {

        }
    }
}
