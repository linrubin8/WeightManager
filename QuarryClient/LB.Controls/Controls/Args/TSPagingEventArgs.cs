using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class TSPagingEventArgs
	{
		private enTSPagingType mPagingType = enTSPagingType.Normal;
		private int miPageIndex = -1;

		public TSPagingEventArgs( enTSPagingType pagingType, int pageIndex )
		{
			mPagingType = pagingType;
			miPageIndex = pageIndex;
		}

		public enTSPagingType PagingType
		{
			get
			{
				return mPagingType;
			}
		}

		public int PageIndex
		{
			get
			{
				return miPageIndex;
			}
		}
	}
}
