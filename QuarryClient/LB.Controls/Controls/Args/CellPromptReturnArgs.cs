using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public class CellPromptReturnArgs : PromptReturnArgs
	{
		private int miColumnIndex = -1;
		private int miRowIndex = -1;

		public CellPromptReturnArgs(
			int iColumnIndex, int iRowIndex,
			DataRow selectedDataRow, DataTable dtMultiSelect )
			: base( selectedDataRow, dtMultiSelect )
		{
			miColumnIndex = iColumnIndex;
			miRowIndex = iRowIndex;
		}

		public int ColumnIndex
		{
			get
			{
				return miColumnIndex;
			}
		}

		public int RowIndex
		{
			get
			{
				return miRowIndex;
			}
		}
	}
}
