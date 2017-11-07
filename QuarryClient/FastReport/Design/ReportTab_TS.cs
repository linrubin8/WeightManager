using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Design
{
	internal partial class ReportTab
	{

		public bool SaveFile( bool saveAs, out bool hasSavedTemplateFields )
		{
			hasSavedTemplateFields = false;

			// update report's script
			Report.ScriptText = Script;

			while( true )
			{
				OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs( Designer );
				string fileName = Report.FileName;

				// show save dialog in case of untitled report or "save as"
				if( saveAs || String.IsNullOrEmpty( fileName ) )
				{
					if( String.IsNullOrEmpty( fileName ) )
						fileName = Res.Get( "Designer,Untitled" );
					e.FileName = fileName;
					Config.DesignerSettings.OnCustomSaveDialog( Designer, e );
					if( e.Cancel )
						return false;

					fileName = e.FileName;
				}

				OpenSaveReportEventArgs e1 = new OpenSaveReportEventArgs( Report, fileName, e.Data, e.IsPlugin );

				try
				{
					Config.DesignerSettings.OnCustomSaveReport( Designer, e1 );
					hasSavedTemplateFields = e1.HasSavedTemplateFields;

					// don't change the report name if plugin was used
					if( e.IsPlugin )
						fileName = Report.FileName;
					Report.FileName = fileName;
					FModified = false;
					Designer.UpdatePlugins( null );
					if( !e.IsPlugin )
						Designer.cmdRecentFiles.Update( fileName );
					UpdateCaption();
					return true;
				}
				catch
				{
					// something goes wrong, suggest to save to another place
					FRMessageBox.Error( Res.Get( "Messages,CantSaveReport" ) );
					saveAs = true;
				}
			}
		}

		public void UploadFile()
		{
			bool hasSavedTemplateFields;
			bool result = SaveFile( false, out hasSavedTemplateFields );
			if( result )
			{
				ReportUploadEventArgs e = new ReportUploadEventArgs( Report, !hasSavedTemplateFields );

				Config.DesignerSettings.OnCustomUploadReport( Designer, e );
			}
			else
			{
				FRMessageBox.Error( Res.Get( "Messages,UploadMustSaveFirst" ) );
			}
		}

		public void UpdateFields()
		{
			bool hasSavedTemplateFields;
			bool result = SaveFile( false, out hasSavedTemplateFields );
			if( result )
			{
				ReportUpdateFieldsEventArgs e = new ReportUpdateFieldsEventArgs( Report );
				Config.DesignerSettings.OnCustomReportUpdateFields( Designer, e );
			}
			else
			{
				FRMessageBox.Error( Res.Get( "Messages,UpdateFieldsMustSaveFirst" ) );
			}
		}
	}
}
