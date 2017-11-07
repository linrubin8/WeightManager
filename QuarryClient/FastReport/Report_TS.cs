using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;

namespace FastReport
{
	public partial class Report
	{
		/// <summary>
		/// 开始打印前触发
		/// </summary>
		public event EventHandler BeginPrint;

		/// <summary>
		/// 结束打印后触发
		/// </summary>
		public event EventHandler EndPrint;

		/// <summary>
		/// 传真时触发
		/// </summary>
		public event EventHandler Fax;

		/// <summary>
		/// 发送门窗大使时触发
		/// </summary>
		public event EventHandler Ambassador;

		/// <summary>
		/// Gets the datasource with specified name.
		/// </summary>
		/// <param name="alias">Alias name of a datasource.</param>
		/// <returns>The datasource object if found, otherwise <b>null</b>.</returns>
		public object GetDataObject( string alias )
		{
			return Dictionary.FindDataObjectByAlias( alias );
		}

		public void OnBeginPrint( EventArgs e )
		{
			if( BeginPrint != null )
			{
				BeginPrint( this, e );
			}
		}

		public void OnEndPrint( EventArgs e )
		{
			if( EndPrint != null )
			{
				EndPrint( this, e );
			}
		}

		public void OnFax( EventArgs e )
		{
			if( Fax != null )
			{
				Fax( this, e );
			}
		}

		public void OnAmbassador( EventArgs e )
		{
			if( Ambassador != null )
			{
				Ambassador( this, e );
			}
		}

		/// <summary>
		/// 清除已注册的数据源
		/// </summary>
		public void ClearRegisterData()
		{
			while( Dictionary.DataSources.Count > 0 )
			{
				Dictionary.DataSources[0].Delete();
				Dictionary.DataSources.RemoveAt( 0 );
			}
		}

		/// <summary>
		/// Exports a report. Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="stream">File name to save export result to.</param>
		public void ExportPdf( Stream stream )
		{
			FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
			export.ShowProgress = true;
			export.Compressed = true;
			export.EmbeddingFonts = true;
			export.Export( this, stream );
		}

		private void ShowPrepared( bool modal, Form mdiParent, IWin32Window owner )
		{
			// create preview form
			if( Preview == null )
			{
				FPreviewForm = new PreviewForm();

				FPreviewForm.MdiParent = mdiParent;
				FPreviewForm.ShowInTaskbar = Config.PreviewSettings.ShowInTaskbar;
				FPreviewForm.TopMost = Config.PreviewSettings.TopMost;
				FPreviewForm.Icon = Config.PreviewSettings.Icon;

				if( String.IsNullOrEmpty( Config.PreviewSettings.Text ) )
				{
					FPreviewForm.Text = String.IsNullOrEmpty( ReportInfo.Name ) ? "" : ReportInfo.Name + " - ";
					FPreviewForm.Text += Res.Get( "Preview" );
				}
				else
					FPreviewForm.Text = Config.PreviewSettings.Text;

				FPreviewForm.FormClosed += new FormClosedEventHandler( OnClosePreview );

				Preview = ( FPreviewForm as PreviewForm ).Preview;
				Preview.UIStyle = Config.UIStyle;
				Preview.FastScrolling = Config.PreviewSettings.FastScrolling;
				Preview.Buttons = Config.PreviewSettings.Buttons;
			}

			if( Config.ReportSettings.ShowPerformance )
				try
				{
					// in case the format string is wrong, use try/catch
					Preview.ShowPerformance( String.Format( Res.Get( "Messages,Performance" ), FTickCount ) );
				}
				catch
				{
				}
			Preview.ClearTabsExceptFirst();
			if( PreparedPages != null )
				Preview.AddPreviewTab( this, GetReportName, null, true );

			Config.PreviewSettings.OnPreviewOpened( Preview );
			if( ReportInfo.SavePreviewPicture && PreparedPages.Count > 0 )
				SavePreviewPicture();

			if( FPreviewForm != null && !FPreviewForm.Visible )
			{
				if( modal )
					FPreviewForm.ShowDialog( owner );
				else
				{
					if( mdiParent == null && owner != null )	// 与原方法修改了此处，增加：  && owner != null
						FPreviewForm.Show( owner );
					else
						FPreviewForm.Show();
				}
			}
		}
	}
}
