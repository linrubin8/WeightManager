using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;
using FastReport.Cloud.StorageClient;
using System.Drawing;
using FastReport.Export;

namespace FastReport.Preview
{
	public partial class PreviewControl
	{
		private FastReport.DevComponents.DotNetBar.ButtonItem btnPrintAndClose;
		private FastReport.DevComponents.DotNetBar.ButtonItem btnFax;
		private FastReport.DevComponents.DotNetBar.ButtonItem btnFaxAndClose;
		private FastReport.DevComponents.DotNetBar.ButtonItem btnAmbassador;
		private FastReport.DevComponents.DotNetBar.ButtonItem btnAmbassadorAndClose;

		private void InitializeComponent()
		{
			InitializeComponent_inner();

			this.SuspendLayout();

			this.btnPrintAndClose = new FastReport.DevComponents.DotNetBar.ButtonItem();
			this.btnFax = new FastReport.DevComponents.DotNetBar.ButtonItem();
			this.btnFaxAndClose = new FastReport.DevComponents.DotNetBar.ButtonItem();
			this.btnAmbassador = new FastReport.DevComponents.DotNetBar.ButtonItem();
			this.btnAmbassadorAndClose = new FastReport.DevComponents.DotNetBar.ButtonItem();

			// 
			// btnPrintAndClose
			// 
			this.btnPrintAndClose.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnPrintAndClose.Name = "btnPrintAndClose";
			this.btnPrintAndClose.Text = "PrintAndClose";
			this.btnPrintAndClose.Click += new System.EventHandler( this.btnPrintAndClose_Click );
			// 
			// btnFax
			// 
			this.btnFax.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnFax.Name = "btnFax";
			this.btnFax.Text = "Fax";
			this.btnFax.Click += new System.EventHandler( this.btnFax_Click );
			// 
			// btnFaxAndClose
			// 
			this.btnFaxAndClose.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnFaxAndClose.Name = "btnFaxAndClose";
			this.btnFaxAndClose.Text = "FaxAndClose";
			this.btnFaxAndClose.Click += new System.EventHandler( this.btnFaxAndClose_Click );

			// 
			// btnAmbassador
			// 
			this.btnAmbassador.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnAmbassador.Name = "btnAmbassador";
			this.btnAmbassador.Text = "Ambassador";
			this.btnAmbassador.Click += new System.EventHandler( this.btnAmbassador_Click );
			// 
			// btnAmbassadorAndClose
			// 
			this.btnAmbassadorAndClose.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnAmbassadorAndClose.Name = "btnAmbassadorAndClose";
			this.btnAmbassadorAndClose.Text = "AmbassadorAndClose";
			this.btnAmbassadorAndClose.Click += new System.EventHandler( this.btnAmbassadorAndClose_Click );

			int index = this.toolBar.Items.IndexOf( btnPrint ) + 1;
			this.toolBar.Items.Insert( index, btnAmbassadorAndClose );
			this.toolBar.Items.Insert( index, btnAmbassador );
			this.toolBar.Items.Insert( index, btnFaxAndClose );
			this.toolBar.Items.Insert( index, btnFax );
			this.toolBar.Items.Insert( index, btnPrintAndClose );

			// 隐藏部分按钮
			this.btnEmail.Visible = false;
			this.btnAmbassador.Visible = false;
			this.btnAmbassadorAndClose.Visible = false;
			HideCloudButton();

			this.ResumeLayout( false );
		}

		private void UpdateButtons()
		{
			UpdateButtons_inner();

			btnPrintAndClose.Visible = ( ( Buttons & PreviewButtons.Print ) != 0 && ( Buttons & PreviewButtons.Close ) != 0 );
			btnFax.Visible = ( Buttons & PreviewButtons.Fax ) != 0;
			btnFaxAndClose.Visible = ( ( Buttons & PreviewButtons.Fax ) != 0 && ( Buttons & PreviewButtons.Close ) != 0 );

			btnAmbassador.Visible = ( Buttons & PreviewButtons.Ambassador ) != 0;
			btnAmbassadorAndClose.Visible = ( ( Buttons & PreviewButtons.Ambassador ) != 0 && ( Buttons & PreviewButtons.Close ) != 0 );

			// 隐藏部分按钮
			this.btnEmail.Visible = false;
			HideCloudButton();
		}

		private void HideCloudButton()
		{
			if( btnSave == null || btnSave.SubItems.Count == 0 )
			{
				return;
			}

			foreach( ButtonItem item in btnSave.SubItems )
			{
				ObjectInfo info = item.Tag as ObjectInfo;
				if( info == null )
				{
					continue;
				}

				if( info.Object != null && info.Object.IsSubclassOf( typeof( CloudStorageClient ) ) )
				{
					item.Visible = false;
				}
			}
		}

		private void Localize()
		{
			MyRes res = Localize_inner();

			btnPrintAndClose.Text = res.Get( "PrintAndCloseText" );
			btnPrintAndClose.Tooltip = res.Get( "PrintAndClose" );
			btnFax.Text = res.Get( "FaxText" );
			btnFax.Tooltip = res.Get( "Fax" );
			btnFaxAndClose.Text = res.Get( "FaxAndCloseText" );
			btnFaxAndClose.Tooltip = res.Get( "FaxAndClose" );

			btnAmbassador.Text = res.Get( "AmbassadorText" );
			btnAmbassador.Tooltip = res.Get( "Ambassador" );
			btnAmbassadorAndClose.Text = res.Get( "AmbassadorAndCloseText" );
			btnAmbassadorAndClose.Tooltip = res.Get( "AmbassadorAndClose" );

			btnPrintAndClose.Image = Res.GetImage( 195 );
		}

		private void btnPrintAndClose_Click( object sender, EventArgs e )
		{
			Print();

			if( FindForm() != null )
			{
				FindForm().Close();
			}
		}

		private void btnFax_Click( object sender, EventArgs e )
		{
			this.Report.OnFax( EventArgs.Empty );
		}

		private void btnFaxAndClose_Click( object sender, EventArgs e )
		{
			this.Report.OnFax( EventArgs.Empty );

			if( FindForm() != null )
			{
				FindForm().Close();
			}
		}

		private void btnAmbassador_Click( object sender, EventArgs e )
		{
			this.Report.OnAmbassador( EventArgs.Empty );
		}

		private void btnAmbassadorAndClose_Click( object sender, EventArgs e )
		{
			this.Report.OnAmbassador( EventArgs.Empty );

			if( FindForm() != null )
			{
				FindForm().Close();
			}
		}

		private void CreateExportList( ButtonItem button, EventHandler handler )
		{
			List<ObjectInfo> list = new List<ObjectInfo>();
			RegisteredObjects.Objects.EnumItems( list );

            if( ( Config.PreviewSettings.ExportButtons & enExportButtons.Frx ) == enExportButtons.Frx )
            {
                ButtonItem saveNative = new ButtonItem( "", Res.Get( "Preview,SaveNative" ) + "..." );
                saveNative.Click += handler;
                button.SubItems.Add( saveNative );
            }

			foreach( ObjectInfo info in list )
			{
				if( info.Object != null && info.Enabled && info.Object.IsSubclassOf( typeof( ExportBase ) ) )
				{
					if( !ExportConfig( info ) )
					{
						continue;
					}
					ButtonItem item = new ButtonItem( "", Res.TryGet( info.Text ) + "..." );
					item.Tag = info;
					item.Click += handler;
					if( info.ImageIndex != -1 )
					{
						Bitmap image = Res.GetImage( info.ImageIndex );
						// avoid errors when several preview are used in threads
						lock ( image )
						{
							item.Image = image;
						}
					}
					button.SubItems.Add( item );
				}
			}
		}

		private bool ExportConfig( ObjectInfo info )
		{
            if( info.Text.IndexOf( "Text" ) > 0 ||
				info.Text.IndexOf( "Svg" ) > 0 ||
				info.Text.IndexOf( "Ppml" ) > 0 ||
				info.Text.IndexOf( "Dbf" ) > 0 ||
				info.Text.IndexOf( "PS" ) > 0 )
			{
				return false;
			}
			else if( info.Text.IndexOf( "Html" ) > 0 ||
				info.Text.IndexOf( "Mht" ) > 0 ||
				info.Text.IndexOf( "Xaml" ) > 0 )
			{
				return ( Config.PreviewSettings.ExportButtons & enExportButtons.Html ) == enExportButtons.Html;
			}
			else if( info.Text.IndexOf( "Pdf" ) > 0 )
			{
				return ( Config.PreviewSettings.ExportButtons & enExportButtons.Pdf ) == enExportButtons.Pdf;
			}
			else if( info.Text.IndexOf( "RichText" ) > 0 ||
				info.Text.IndexOf( "Xlsx" ) > 0 ||
				info.Text.IndexOf( "Docx" ) > 0 ||
				info.Text.IndexOf( "Pptx" ) > 0 ||
                info.Text.IndexOf( "Ods" ) > 0 ||
                info.Text.IndexOf( "Odt" ) > 0 ||
                info.Text.IndexOf( "Csv" ) > 0 ||
                info.Text.IndexOf( "Xml" ) > 0 )
			{
				return ( Config.PreviewSettings.ExportButtons & enExportButtons.Office ) == enExportButtons.Office;
            }
            else if( info.Text.IndexOf( "Image" ) > 0 )
            {
                return ( Config.PreviewSettings.ExportButtons & enExportButtons.Image ) == enExportButtons.Image;
            }
            else if( info.Text.IndexOf( "Xps" ) > 0 )
            {
                return ( Config.PreviewSettings.ExportButtons & enExportButtons.Xps ) == enExportButtons.Xps;
            }

            return false;
		}
	}
}
