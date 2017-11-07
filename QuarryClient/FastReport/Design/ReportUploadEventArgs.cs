using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Design
{
	public delegate void ReportUploadEventHandler( object sender, ReportUploadEventArgs e );

	public class ReportUploadEventArgs
	{
		private Report mReport = null;
		public Report Report
		{
			get
			{
				return mReport;
			}
		}

		private bool mbNeedSaveTemplateFields = false;
		public bool NeedSaveTemplateFields
		{
			get
			{
				return mbNeedSaveTemplateFields;
			}
		}

		internal ReportUploadEventArgs( Report report, bool needSaveTemplateFields )
		{
			mReport = report;
			mbNeedSaveTemplateFields = needSaveTemplateFields;
		}
	}
}
