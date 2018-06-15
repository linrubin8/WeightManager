using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LB.Controls.LBEditor
{
	public interface ITSPromptForm
	{
		Point PromptCellAddress
		{
			get;
			set;
		}

		event PromptReturnEventHandler PromptReturn;
	}
}
