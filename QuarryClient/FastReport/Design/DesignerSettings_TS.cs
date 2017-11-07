using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Design
{
	public partial class DesignerSettings
	{
		public event ReportUploadEventHandler CustomReportUpload;
		public event ReportAfterSaveEventHandler CustomReportAfterSave;
		public event GetNCodeImageEventHandler GetNCodeImageEvent;
		public event GetItemImageEventHandler GetItemImageEvent;
		public event GetInvoiceImageEventHandler GetInvoiceImageEvent;
		public event ReportUpdateFieldsEventHandler CustomReportUpdateFields;

		internal void OnCustomUploadReport( object sender, ReportUploadEventArgs e )
		{
			if( CustomReportUpload != null )
			{
				CustomReportUpload( sender, e );
			}
		}

		internal void OnCustomReportUpdateFields( object sender, ReportUpdateFieldsEventArgs e )
		{
			if( CustomReportUpdateFields != null )
			{
				CustomReportUpdateFields( sender, e );
			}
		}

		internal void OnCustomReportAfterSave( object sender, ReportAfterSaveEventArgs e )
		{
			if( CustomReportAfterSave != null )
			{
				CustomReportAfterSave( sender, e );
			}
		}

		internal void OnGetNCodeImageEvent( object sender, GetNCodeImageEventArgs e )
		{
			if( GetNCodeImageEvent != null )
			{
				GetNCodeImageEvent( sender, e );
			}
		}

		internal void OnGetItemImageEvent( object sender, GetItemImageEventArgs e )
		{
			if( GetItemImageEvent != null )
			{
				GetItemImageEvent( sender, e );
			}
		}

		internal void OnGetInvoiceImageEvent( object sender, GetInvoiceImageEventArgs e )
		{
			if( GetInvoiceImageEvent != null )
			{
				GetInvoiceImageEvent( sender, e );
			}
		}
	}
}
