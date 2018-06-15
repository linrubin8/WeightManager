using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class CellPromptPageCreatedArgs : PromptPageCreatedArgs
	{
		public CellPromptPageCreatedArgs( ITSPromptForm ipage )
			: base( ipage )
		{
		}

		public int ColumnIndex
		{
			get
			{
				if( base.PromptPage != null )
				{
					return base.PromptPage.PromptCellAddress.X;
				}
				else
				{
					return -1;
				}
			}
		}

		public int RowIndex
		{
			get
			{
				if( base.PromptPage != null )
				{
					return base.PromptPage.PromptCellAddress.Y;
				}
				else
				{
					return -1;
				}
			}
		}
	}
}
