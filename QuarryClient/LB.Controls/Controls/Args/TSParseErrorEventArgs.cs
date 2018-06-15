using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public class TSParseErrorEventArgs : System.EventArgs
	{
		private DataRow mdrRow = null;
		private bool mbExistsError = false;
		private string mstrErrorMessage = "";
		private bool mbHandled = false;

		public TSParseErrorEventArgs( DataRow drRow )
		{
			mdrRow = drRow;
		}

		public DataRow CurrentDataRow
		{
			get
			{
				return mdrRow;
			}
		}

		public bool ExistsError
		{
			get
			{
				return mbExistsError;
			}
			set
			{
				mbExistsError = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return mstrErrorMessage;
			}
			set
			{
				mstrErrorMessage = value;
			}
		}

		public bool Handled
		{
			get
			{
				return mbHandled;
			}
			set
			{
				mbHandled = value;
			}
		}
	}
}
