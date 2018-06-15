using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Args
{
	public class CellValidateEventArgs
	{
		private string mstrValidateMessage = "";
		private bool mbPass = true;
		private TSDataGridViewTextBoxColumn mTSColumn = null;
		private TSDataGridViewTextBoxCell mTSCell = null;

		public CellValidateEventArgs( TSDataGridViewTextBoxCell tsCell, TSDataGridViewTextBoxColumn tsColumn )
		{
			mTSCell = tsCell;
			mTSColumn = tsColumn;
		}

		public string ValidateMessage
		{
			get
			{
				return mstrValidateMessage;
			}
			set
			{
				mstrValidateMessage = value;
			}
		}

		public bool Pass
		{
			get
			{
				return mbPass;
			}
			set
			{
				mbPass = value;
			}
		}

		public TSDataGridViewTextBoxColumn TSColumn
		{
			get
			{
				return mTSColumn;
			}
		}

		public TSDataGridViewTextBoxCell TSCell
		{
			get
			{
				return mTSCell;
			}
		}
	}
}
