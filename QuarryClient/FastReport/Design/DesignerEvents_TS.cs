using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastReport.Design
{
	partial class OpenSaveReportEventArgs
	{

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
	}
}
