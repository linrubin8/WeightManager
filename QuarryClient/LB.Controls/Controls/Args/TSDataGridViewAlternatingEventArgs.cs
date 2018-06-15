using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public class TSDataGridViewAlternatingEventArgs
	{
		private DataRow mdrLast = null;
		private DataRow mdrCurrent = null;
		private bool mbIsSame = false;
		private bool mbHandled = false;

		public TSDataGridViewAlternatingEventArgs( DataRow drLast, DataRow drCurrent )
		{
			mdrLast = drLast;
			mdrCurrent = drCurrent;
		}

		public DataRow LastDataRow
		{
			get
			{
				return mdrLast;
			}
		}

		public DataRow CurrentDataRow
		{
			get
			{
				return mdrCurrent;
			}
		}

		public bool IsSame
		{
			get
			{
				return mbIsSame;
			}
			set
			{
				mbIsSame = value;
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
