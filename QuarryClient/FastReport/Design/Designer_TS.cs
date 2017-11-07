using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FastReport.Design
{
	public partial class Designer
	{
		private UploadCommand FcmdUpload;
		private UpdateFieldsCommand FcmdUpdateFields;

		/// <summary>
		/// The "File|Upload" command.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public UploadCommand cmdUpload
		{
			get
			{
				return FcmdUpload;
			}
		}

		/// <summary>
		/// The "File|UpdateFields" command.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public UpdateFieldsCommand cmdUpdateFields
		{
			get
			{
				return FcmdUpdateFields;
			}
		}

		private void InitCommands()
		{
			InitCommands_inner();

			FcmdUpload = new UploadCommand( this );
			FcmdUpdateFields = new UpdateFieldsCommand( this );
		}
	}
}
