using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Controls.Report
{
    public class ReportRequestArgs
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
            set
            {
                _RecordDR = value;
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

        private long _ReportTemplateID;
        public long ReportTemplateID
        {
            get
            {
                return _ReportTemplateID;
            }
            set
            {
                _ReportTemplateID = value;
            }
        }

        private DataRow drReportTemplateConfig;
        public DataRow ReportTemplateConfig
        {
            get
            {
                return drReportTemplateConfig;
            }
            set
            {
                drReportTemplateConfig = value;
            }
        }

        public ReportRequestArgs(long lReportTemplateID,long lReportTypeID, DataSet dsDataSource, DataRow recordDR)
        {
            this._ReportTemplateID = lReportTemplateID;
            this._RecordDR = recordDR;
            this._DSDataSource = dsDataSource;
            this._ReportTypeID = lReportTypeID;
        }
    }
}
