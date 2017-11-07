using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Design
{
	/// <summary>
	/// Represents the "File|Upload" command.
	/// </summary>
	public class UploadCommand : DesignerCommand
	{
		/// <inheritdoc/>
		public override void Invoke()
		{
			base.ActiveReportTab.UploadFile();
		}

		internal UploadCommand( Designer designer )
			: base( designer )
		{
		}
	}

	/// <summary>
	/// Represents the "File|Upload" command.
	/// </summary>
	public class UpdateFieldsCommand : DesignerCommand
	{
		/// <inheritdoc/>
		public override void Invoke()
		{
			base.ActiveReportTab.UpdateFields();
		}

		internal UpdateFieldsCommand( Designer designer )
			: base( designer )
		{
		}
	}
}
