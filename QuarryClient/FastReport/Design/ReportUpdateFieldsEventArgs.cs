using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Design
{
	public delegate void ReportUpdateFieldsEventHandler( object sender, ReportUpdateFieldsEventArgs e );

	public class ReportUpdateFieldsEventArgs
	{
		private Report mReport = null;
		public Report Report
		{
			get
			{
				return mReport;
			}
		}

		internal ReportUpdateFieldsEventArgs( Report report )
		{
			mReport = report;
		}
	}
}
