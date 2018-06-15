using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class TSDealErrorEventArgs : System.EventArgs
	{
		private Exception mEx = null;
		private bool mbHandled = false;

		public TSDealErrorEventArgs( Exception ex )
		{
			mEx = ex;
		}

		public Exception Exception
		{
			get
			{
				return mEx;
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
