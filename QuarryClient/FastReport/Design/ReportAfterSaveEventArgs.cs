using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Design
{
	public delegate void ReportAfterSaveEventHandler( object sender, ReportAfterSaveEventArgs e );

	public class ReportAfterSaveEventArgs
	{
		private Report mReport = null;
		public Report Report
		{
			get
			{
				return mReport;
			}
		}

		private bool mbHasSavedTemplateFields = false;
		public bool HasSavedTemplateFields
		{
			get
			{
				return mbHasSavedTemplateFields;
			}
			set
			{
				mbHasSavedTemplateFields = value;
			}
		}

		//private List<string> mlstTemplateFields = null;
		//public List<string> TemplateFields
		//{
		//    get
		//    {
		//        return mlstTemplateFields;
		//    }
		//}

		internal ReportAfterSaveEventArgs( Report report )
		{
			mReport = report;
			//mlstTemplateFields = templateFields;
		}
	}
}
