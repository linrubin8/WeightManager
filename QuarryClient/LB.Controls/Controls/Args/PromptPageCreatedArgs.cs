using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class PromptPageCreatedArgs
	{
		private ITSPromptForm mipage = null;

		public PromptPageCreatedArgs( ITSPromptForm ipage )
		{
			mipage = ipage;
		}

		public ITSPromptForm PromptPage
		{
			get
			{
				return mipage;
			}
		}
	}
}
