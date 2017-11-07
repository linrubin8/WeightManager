using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBToolStripDropDownReportViewButton : LBToolStripDropDownButton
    {
        private long _ReportTypeID;
        public long ReportTypeID
        {
            get
            {
                return _ReportTypeID;
            }
            set
            {
                _ReportTypeID = value;
            }
        }

        public LBToolStripDropDownReportViewButton()
        {
            InitializeComponent();
        }

        public LBToolStripDropDownReportViewButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

    }
}
