using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public class PromptReturnArgs
	{
		private DataRow mSelectedDataRow;
		private DataTable mdtMultiSelect = null;
		private bool mbClosePage = true;

		public PromptReturnArgs( DataRow selectedDataRow, DataTable dtMultiSelect )
		{
			mSelectedDataRow = selectedDataRow;
			mdtMultiSelect = dtMultiSelect;
		}

		public DataRow SelectedDataRow
		{
			get
			{
				return this.mSelectedDataRow;
			}
		}

		public DataTable DTMultiSelect
		{
			get
			{
				return this.mdtMultiSelect;
			}
		}

		public bool ClosePage
		{
			get
			{
				return mbClosePage;
			}
			set
			{
				mbClosePage = value;
			}
		}
	}
}
