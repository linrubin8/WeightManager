using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Controls.Report
{
    public class ReportArgs
    {
        private DataSet _DSDataSource = null;
        public DataSet DSDataSource
        {
            get
            {
                return _DSDataSource;
            }
            set
            {
                _DSDataSource = value;
            }
        }

        private DataRow _RecordDR = null;
        public DataRow RecordDR
        {
            get
            {
                return _RecordDR;
            }
        }

        private long _ReportTypeID;
        public long ReportTypeID
        {
            get
            {
                return _ReportTypeID;
            }
        }

        public ReportArgs(long iReportTypeID, DataSet dsDataSource, DataRow recordDR)
        {
            this._DSDataSource = dsDataSource;
            this._RecordDR = recordDR;
            this._ReportTypeID = iReportTypeID;
        }
    }
}
