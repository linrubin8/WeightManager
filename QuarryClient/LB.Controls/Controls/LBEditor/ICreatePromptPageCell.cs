using LB.Controls.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.LBEditor
{
	public interface ICreatePromptPageCell
	{
		void OnPromptPageCreated( CellPromptPageCreatedArgs args );

		event PromptPageCreatedEventHandler PromptPageCreated;
		event PromptReturnEventHandler PromptReturn;
	}
}
