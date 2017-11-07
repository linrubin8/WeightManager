using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Design
{
	public delegate void GetInvoiceImageEventHandler( object sender, GetInvoiceImageEventArgs e );

	public class GetInvoiceImageEventArgs
	{
		private long _InvoiceDetailID;
		public long InvoiceDetailID
		{
			get
			{
				return _InvoiceDetailID;
			}
		}

		private Image _InvoiceImage = null;
		public Image InvoiceImage
		{
			get
			{
				return _InvoiceImage;
			}
			set
			{
				_InvoiceImage = value;
			}
		}

		public GetInvoiceImageEventArgs( long invoiceDetailID )
		{
			_InvoiceDetailID = invoiceDetailID;
		}
	}
}
