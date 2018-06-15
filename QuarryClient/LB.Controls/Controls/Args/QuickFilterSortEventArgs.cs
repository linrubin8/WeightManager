using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Args
{
	public delegate void QuickFilterSortEventHandler( object sender, QuickFilterSortEventArgs e );

	public class QuickFilterSortEventArgs
	{
		private TSDataGridViewTextBoxColumn mColumn;
		public TSDataGridViewTextBoxColumn Column
		{
			get
			{
				return mColumn;
			}
		}

		private string mstrColumnName = "";
		public string ColumnName
		{
			get
			{
				return mstrColumnName;
			}
		}

		private TSColumnSortInfo mSortInfo = null;
		public TSColumnSortInfo SortInfo
		{
			get
			{
				return mSortInfo;
			}
			set
			{
				mSortInfo = value;
			}
		}

		private List<TSColumnSortInfo> mSortCustom = null;
		public List<TSColumnSortInfo> SortCustom
		{
			get
			{
				return mSortCustom;
			}
			set
			{
				mSortCustom = value;
			}
		}

		private TSColumnFilterInfo mFilterInfo= null;
		public TSColumnFilterInfo FilterInfo
		{
			get
			{
				return mFilterInfo;
			}
			set
			{
				mFilterInfo = value;
			}
		}

		private bool mbApplySort = false;
		public bool ApplySort
		{
			get
			{
				return mbApplySort;
			}
			//set
			//{
			//    mbApplySort = value;
			//}
		}

		private bool mbApplyFilter = false;
		public bool ApplyFilter
		{
			get
			{
				return mbApplyFilter;
			}
			//set
			//{
			//    mbApplyFilter = value;
			//}
		}

		private bool mbSortIsCustom = false;
		public bool SortIsCustom
		{
			get
			{
				return mbSortIsCustom;
			}
			//set
			//{
			//    mbApplySort = value;
			//}
		}

		public QuickFilterSortEventArgs( TSDataGridViewTextBoxColumn column, bool applySort, bool applyFilter, bool sortIsCustom )
		{
			mColumn = column;
			if( column != null )
			{
				mstrColumnName = column.DataPropertyName;
			}
			mbApplySort = applySort;
			mbApplyFilter = applyFilter;
			mbSortIsCustom = sortIsCustom;
		}
	}
}
