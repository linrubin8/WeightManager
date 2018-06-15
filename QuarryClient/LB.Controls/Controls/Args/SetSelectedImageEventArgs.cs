using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public delegate void SetSelectedImageEventHandler( object sender, SetSelectedImageEventArgs e );

	public class SetSelectedImageEventArgs
	{
		private DataRow mdrValueDataRow = null;

		public DataRow ValueDataRow
		{
			get
			{
				return mdrValueDataRow;
			}
		}

		public SetSelectedImageEventArgs( DataRow drValue )
		{
			mdrValueDataRow = drValue;
		}
	}
}
