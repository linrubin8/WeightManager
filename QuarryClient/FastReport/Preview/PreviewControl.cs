using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.Export;
using FastReport.Export.Email;
using FastReport.Cloud.StorageClient;
using FastReport.Cloud.StorageClient.Ftp;
using FastReport.Cloud.StorageClient.Box;
using FastReport.Cloud.StorageClient.FastCloud;
using FastReport.Cloud.StorageClient.Dropbox;
using FastReport.Cloud.StorageClient.SkyDrive;
using FastReport.Cloud.StorageClient.GoogleDrive;
using Box = FastReport.Cloud.StorageClient.Box;
using SkyDrive = FastReport.Cloud.StorageClient.SkyDrive;
using GoogleDrive = FastReport.Cloud.StorageClient.GoogleDrive;
using FastReport.Messaging;
using FastReport.Messaging.Xmpp;
using FastReport.DevComponents.DotNetBar;
using FastReport.Forms;

namespace FastReport.Preview
{
	/// <summary>
	/// Represents a Windows Forms control used to preview a report.
	/// </summary>
	/// <remarks>
	/// To use this control, place it on a form and link it to a report using the report's
	/// <see cref="FastReport.Report.Preview"/> property. To show a report, call 
	/// the <b>Report.Show</b> method:                                                   
	/// <code>
	/// report1.Preview = previewControl1;
	/// report1.Show();
	/// </code>
	/// <para>Use this control's methods such as <see cref="Print"/>, <see cref="Save()"/> etc. to
	/// handle the preview. Call <see cref="Clear"/> method to clear the preview.</para>
	/// <para>You can specify whether the standard toolbar is visible in the <see cref="ToolbarVisible"/>
	/// property. The <see cref="StatusbarVisible"/> property allows you to hide/show the statusbar.
	/// </para>
	/// </remarks>
	[ToolboxItem( true ), ToolboxBitmap( typeof( Report ), "Resources.PreviewControl.bmp" )]
	public partial class PreviewControl : UserControl
	{
		#region Fields
		private Report FReport;
		private List<PreviewTab> FDocuments;
		private bool FToolbarVisible;
		private bool FStatusbarVisible;
		private Color FPageBorderColor;
		private Color FActivePageBorderColor;
		private PreviewButtons FButtons;
		private PreviewExports FExports;
		private PreviewClouds FClouds;
		private bool FUpdatingZoom;
		private Timer FUpdateTimer;
		private float FZoomToUpdate;
		private float FDefaultZoom;
		private bool FLocked;
		private PreviewTab FCurrentPreview;
		private bool FFastScrolling;
		private UIStyle FUIStyle;
		private bool FUseBackColor;
		private Point FPageOffset;
		#endregion

		#region Properties
		/// <summary>
		/// Occurs when current page number is changed.
		/// </summary>
		public event EventHandler PageChanged;

		/// <summary>
		/// Gets a reference to the report.
		/// </summary>
		[Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Report Report
		{
			get
			{
				return FReport;
			}
		}

		/// <summary>
		/// Obsolete. Gets or sets the color of page border.
		/// </summary>
		[Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[EditorBrowsable( EditorBrowsableState.Never )]
		public Color PageBorderColor
		{
			get
			{
				return FPageBorderColor;
			}
			set
			{
				FPageBorderColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of active page border.
		/// </summary>
		[DefaultValue( typeof( Color ), "255, 199, 60" )]
		public Color ActivePageBorderColor
		{
			get
			{
				return FActivePageBorderColor;
			}
			set
			{
				FActivePageBorderColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the first page offset from the top left corner of the control.
		/// </summary>
		public Point PageOffset
		{
			get
			{
				return FPageOffset;
			}
			set
			{
				FPageOffset = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the toolbar is visible.
		/// </summary>
		[DefaultValue( true )]
		public bool ToolbarVisible
		{
			get
			{
				return FToolbarVisible;
			}
			set
			{
				FToolbarVisible = value;
				toolBar.Visible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the statusbar is visible.
		/// </summary>
		[DefaultValue( true )]
		public bool StatusbarVisible
		{
			get
			{
				return FStatusbarVisible;
			}
			set
			{
				FStatusbarVisible = value;
				statusBar.Visible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the outline control is visible.
		/// </summary>
		[DefaultValue( false )]
		public bool OutlineVisible
		{
			get
			{
				return outlineControl.Visible;
			}
			set
			{
				splitter.Visible = value;
				outlineControl.Visible = value;
				btnOutline.Checked = value;
			}
		}

		/// <summary>
		/// Specifies the set of buttons available in the toolbar.
		/// </summary>
		[DefaultValue( PreviewButtons.All )]
		public PreviewButtons Buttons
		{
			get
			{
				return FButtons;
			}
			set
			{
				FButtons = value;
				UpdateButtons();
			}
		}

		/// <summary>
		/// Specifies the set of exports that will be available in the preview's "save" menu.
		/// </summary>
		[DefaultValue( PreviewExports.All )]
		public PreviewExports Exports
		{
			get
			{
				return FExports;
			}
			set
			{
				FExports = value;
				UpdateButtons();
			}
		}

		/// <summary>
		/// Specifies the set of exports in clouds that will be available in the preview's "save" menu.
		/// </summary>
		[DefaultValue( PreviewClouds.All )]
		public PreviewClouds Clouds
		{
			get
			{
				return FClouds;
			}
			set
			{
				FClouds = value;
				UpdateButtons();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the fast scrolling method should be used.
		/// </summary>
		/// <remarks>
		/// If you enable this property, the gradient background will be disabled.
		/// </remarks>
		[DefaultValue( false )]
		public bool FastScrolling
		{
			get
			{
				return FFastScrolling;
			}
			set
			{
				FFastScrolling = value;
			}
		}

		/// <summary>
		/// Gets or sets the visual style.
		/// </summary>
		[DefaultValue( UIStyle.VisualStudio2012Light )]
		public UIStyle UIStyle
		{
			get
			{
				return FUIStyle;
			}
			set
			{
				FUIStyle = value;
				UpdateUIStyle();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating that the BackColor property must be used to draw the background area.
		/// </summary>
		/// <remarks>
		/// By default, the background area is drawn using the color defined in the current <b>UIStyle</b>.
		/// </remarks>
		[DefaultValue( false )]
		public bool UseBackColor
		{
			get
			{
				return FUseBackColor;
			}
			set
			{
				FUseBackColor = value;
				UpdateUIStyle();
			}
		}

		/// <summary>
		/// Gets the preview window's toolbar.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Bar ToolBar
		{
			get
			{
				return toolBar;
			}
		}

		/// <summary>
		/// Gets the preview window's statusbar.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Bar StatusBar
		{
			get
			{
				return statusBar;
			}
		}

		internal float DefaultZoom
		{
			get
			{
				return FDefaultZoom;
			}
		}

		internal PreviewTab CurrentPreview
		{
			get
			{
				return FCurrentPreview;
			}
		}

		private bool IsPreviewEmpty
		{
			get
			{
				return CurrentPreview == null || CurrentPreview.Disabled;
			}
		}
		#endregion

		#region Private Methods
		// Simon: 注释原方法
		//private void CreateExportList( ButtonItem button, EventHandler handler )
		//{
		//	List<ObjectInfo> list = new List<ObjectInfo>();
		//	RegisteredObjects.Objects.EnumItems( list );

		//	ButtonItem saveNative = new ButtonItem( "", Res.Get( "Preview,SaveNative" ) + "..." );
		//	saveNative.Click += handler;
		//	button.SubItems.Add( saveNative );

		//	foreach( ObjectInfo info in list )
		//	{
		//		if( info.Object != null && info.Enabled && info.Object.IsSubclassOf( typeof( ExportBase ) ) )
		//		{
		//			ButtonItem item = new ButtonItem( "", Res.TryGet( info.Text ) + "..." );
		//			item.Tag = info;
		//			item.Name = info.Object.Name;
		//			item.Click += handler;
		//			if( info.ImageIndex != -1 )
		//			{
		//				Bitmap image = Res.GetImage( info.ImageIndex );
		//				// avoid errors when several preview are used in threads
		//				lock( image )
		//				{
		//					item.Image = image;
		//				}
		//			}
		//			button.SubItems.Add( item );
		//		}
		//	}
		//}

		private void CreateCloudList( ButtonItem button, EventHandler handler )
		{
			List<ObjectInfo> list = new List<ObjectInfo>();
			RegisteredObjects.Objects.EnumItems( list );
			bool firstTime = true;

			foreach( ObjectInfo info in list )
			{
				if( info.Object != null && info.Enabled && info.Object.IsSubclassOf( typeof( CloudStorageClient ) ) )
				{
					ButtonItem item = new ButtonItem( "", Res.TryGet( info.Text ) + "..." );
					item.Tag = info;
					item.Name = info.Object.Name.Replace( "StorageClient", "" );
					item.Click += handler;
					if( info.ImageIndex != -1 )
					{
						Bitmap image = Res.GetImage( info.ImageIndex );
						// avoid errors when several preview are user in threads
						lock( image )
						{
							item.Image = image;
						}
					}
					if( firstTime )
						item.BeginGroup = true;
					button.SubItems.Add( item );
					firstTime = false;
				}
			}
		}

		private void CreateMessengersList( ButtonItem button, EventHandler handler )
		{
			List<ObjectInfo> list = new List<ObjectInfo>();
			RegisteredObjects.Objects.EnumItems( list );
			bool firstTime = true;

			foreach( ObjectInfo info in list )
			{
				if( info.Object != null && info.Enabled && info.Object.IsSubclassOf( typeof( MessengerBase ) ) )
				{
					ButtonItem item = new ButtonItem( "", Res.TryGet( info.Text ) + "..." );
					item.Tag = info;
					item.Name = info.Object.Name.Replace( "Messenger", "" );
					item.Click += handler;
					if( info.ImageIndex != -1 )
					{
						Bitmap image = Res.GetImage( info.ImageIndex );
						// avoid errors when several preview are user in threads
						lock( image )
						{
							item.Image = image;
						}
					}
					if( firstTime )
						item.BeginGroup = true;
					button.SubItems.Add( item );
					firstTime = false;
				}
			}
		}

		// Simon: UpdateButtons 改为 UpdateButtons_inner
		private void UpdateButtons_inner()
		{
			btnPrint.Visible = ( Buttons & PreviewButtons.Print ) != 0;
			btnOpen.Visible = ( Buttons & PreviewButtons.Open ) != 0;
			btnSave.Visible = ( Buttons & PreviewButtons.Save ) != 0;
			btnEmail.Visible = ( Buttons & PreviewButtons.Email ) != 0 && !Config.EmailSettings.UseMAPI;
			btnEmailMapi.Visible = ( Buttons & PreviewButtons.Email ) != 0 && Config.EmailSettings.UseMAPI;
			btnFind.Visible = ( Buttons & PreviewButtons.Find ) != 0;

			btnOutline.Visible = ( Buttons & PreviewButtons.Outline ) != 0;
			btnPageSetup.Visible = ( Buttons & PreviewButtons.PageSetup ) != 0;
			btnDesign.Visible = ( Buttons & PreviewButtons.Design ) != 0;
			btnEdit.Visible = ( Buttons & PreviewButtons.Edit ) != 0;
			btnWatermark.Visible = ( Buttons & PreviewButtons.Watermark ) != 0;

			btnFirst.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;
			btnPrior.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;
			tbPageNo.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;
			lblTotalPages.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;
			btnNext.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;
			btnLast.Visible = ( Buttons & PreviewButtons.Navigator ) != 0;

			btnClose.Visible = ( Buttons & PreviewButtons.Close ) != 0;

			bool firstTime = true;
			if( !( FExports == PreviewExports.All && FClouds == PreviewClouds.All ) )
				for( int i = 0; i < btnSave.SubItems.Count; i++ )
				{
					string itm_nm = btnSave.SubItems[i].Name;
					if( Enum.IsDefined( typeof( PreviewExports ), itm_nm ) && FExports != PreviewExports.All )
					{
						if( FExports != PreviewExports.None )
						{
							foreach( PreviewExports nm in Enum.GetValues( typeof( PreviewExports ) ) )
							{
								if( !( ( FExports & nm ) == nm ) && itm_nm == nm.ToString() )
								{
									btnSave.SubItems.Remove( btnSave.SubItems[i].Name );
									i--;
									break;
								}
							}
						}
						else
						{
							btnSave.SubItems.Remove( btnSave.SubItems[i].Name );
							i--;
						}
					}
					else if( Enum.IsDefined( typeof( PreviewClouds ), itm_nm ) && FClouds != PreviewClouds.All )
					{
						if( FClouds != PreviewClouds.None )
						{
							foreach( PreviewClouds nm in Enum.GetValues( typeof( PreviewClouds ) ) )
							{
								if( !( ( FClouds & nm ) == nm ) && itm_nm == nm.ToString() )
								{
									btnSave.SubItems.Remove( btnSave.SubItems[i].Name );
									i--;
									break;
								}
								else if( firstTime && btnSave.SubItems[i].BeginGroup != true )
								{
									btnSave.SubItems[i].BeginGroup = true;
									firstTime = false;
								}
							}
						}
						else
						{
							btnSave.SubItems.Remove( btnSave.SubItems[i].Name );
							i--;
						}
					}
				}
		}

		private void Export_Click( object sender, EventArgs e )
		{
			if( IsPreviewEmpty )
				return;

			ObjectInfo info = ( sender as ButtonItem ).Tag as ObjectInfo;
			if( info == null )
				Save();
			else
			{
				ExportBase export = Activator.CreateInstance( info.Object ) as ExportBase;
				export.CurPage = CurrentPreview.PageNo;
				export.AllowSaveSettings = true;
				export.ShowProgress = true;
				try
				{
					export.Export( CurrentPreview.Report );
				}
#if !DEBUG
        catch (Exception ex)
        {
          using (ExceptionForm form = new ExceptionForm(ex))
          {
            form.ShowDialog();
          }
        }
#endif
				finally
				{
				}
			}
		}

		private void SaveToCloud_Click( object sender, EventArgs e )
		{
			if( IsPreviewEmpty )
			{
				return;
			}

			ObjectInfo info = ( sender as ButtonItem ).Tag as ObjectInfo;
			if( info != null )
			{
				CloudStorageClient client = Activator.CreateInstance( info.Object ) as CloudStorageClient;
				if( client is FtpStorageClient )
				{
					XmlItem xi = Config.Root.FindItem( "FtpServer" ).FindItem( "StorageSettings" );
					string server = xi.GetProp( "FtpServer" );
					string username = xi.GetProp( "FtpUsername" );
					FtpStorageClientForm form = new FtpStorageClientForm( server, username, "", Report );
					form.ShowDialog();
				}
				else if( client is BoxStorageClient )
				{
					XmlItem xi = Config.Root.FindItem( "BoxCloud" ).FindItem( "StorageSettings" );
					string id = xi.GetProp( "ClientId" );
					string secret = xi.GetProp( "ClientSecret" );
					if( String.IsNullOrEmpty( id ) || String.IsNullOrEmpty( secret ) )
					{
						Box.ClientInfoForm clientInfoDialog = new Box.ClientInfoForm();
						clientInfoDialog.ShowDialog();
						id = clientInfoDialog.Id;
						secret = clientInfoDialog.Secret;
					}
					BoxStorageClientForm form = new BoxStorageClientForm( new SkyDrive.ClientInfo( "", id, secret ), Report );
					form.ShowDialog();
				}
				else if( client is DropboxStorageClient )
				{
					XmlItem xi = Config.Root.FindItem( "DropboxCloud" ).FindItem( "StorageSettings" );
					string accessToken = xi.GetProp( "AccessToken" );
					if( String.IsNullOrEmpty( accessToken ) )
					{
						ApplicationInfoForm appInfoDialog = new ApplicationInfoForm();
						appInfoDialog.ShowDialog();
						accessToken = appInfoDialog.AccessToken;
					}
					DropboxStorageClientForm form = new DropboxStorageClientForm( accessToken, Report );
					form.ShowDialog();
				}
				else if( client is FastCloudStorageClient )
				{
					FastCloudStorageClientSimpleForm form = new FastCloudStorageClientSimpleForm( Report );
					form.ShowDialog();
				}
				else if( client is GoogleDriveStorageClient )
				{
					XmlItem xi = Config.Root.FindItem( "GoogleDriveCloud" ).FindItem( "StorageSettings" );
					string id = xi.GetProp( "ClientId" );
					string secret = xi.GetProp( "ClientSecret" );
					if( String.IsNullOrEmpty( id ) || String.IsNullOrEmpty( secret ) )
					{
						GoogleDrive.ClientInfoForm clientInfoDialog = new GoogleDrive.ClientInfoForm();
						clientInfoDialog.ShowDialog();
						id = clientInfoDialog.Id;
						secret = clientInfoDialog.Secret;
					}
					GoogleDriveStorageClientForm form = new GoogleDriveStorageClientForm( new SkyDrive.ClientInfo( "", id, secret ), Report );
					form.ShowDialog();
				}
				else if( client is SkyDriveStorageClient )
				{
					XmlItem xi = Config.Root.FindItem( "SkyDriveCloud" ).FindItem( "StorageSettings" );
					string id = xi.GetProp( "ClientId" );
					string secret = xi.GetProp( "ClientSecret" );
					if( String.IsNullOrEmpty( id ) || String.IsNullOrEmpty( secret ) )
					{
						SkyDrive.ClientInfoForm appInfoDialog = new SkyDrive.ClientInfoForm();
						appInfoDialog.ShowDialog();
						id = appInfoDialog.Id;
						secret = appInfoDialog.Secret;
					}
					SkyDriveStorageClientForm form = new SkyDriveStorageClientForm( new SkyDrive.ClientInfo( "", id, secret ), Report );
					form.ShowDialog();
				}
			}
		}

		private void SendViaMessenger_Click( object sender, EventArgs e )
		{
			if( IsPreviewEmpty )
			{
				return;
			}

			ObjectInfo info = ( sender as ButtonItem ).Tag as ObjectInfo;
			if( info != null )
			{
				MessengerBase messenger = Activator.CreateInstance( info.Object ) as MessengerBase;
				if( messenger is XmppMessenger )
				{
					XmlItem xi = Config.Root.FindItem( "XmppMessenger" ).FindItem( "MessengerSettings" );
					string jidFrom = xi.GetProp( "JidFrom" );
					string password = xi.GetProp( "Password" );
					string jidTo = xi.GetProp( "JidTo" );
					XmppMessengerForm form = new XmppMessengerForm( jidFrom, password, jidTo, Report );
					form.ShowDialog();
				}
			}
		}

		private void Email_Click( object sender, EventArgs e )
		{
			if( IsPreviewEmpty )
				return;

			List<string> fileNames = new List<string>();
			ObjectInfo info = ( sender as ButtonItem ).Tag as ObjectInfo;

			if( info == null )
			{
				using( SaveFileDialog dialog = new SaveFileDialog() )
				{
					dialog.Filter = Res.Get( "FileFilters,PreparedReport" );
					dialog.DefaultExt = "*.fpx";
					if( dialog.ShowDialog() == DialogResult.OK )
					{
						Save( dialog.FileName );
						fileNames.Add( dialog.FileName );
					}
				}
			}
			else
			{
				ExportBase export = Activator.CreateInstance( info.Object ) as ExportBase;
				export.CurPage = CurrentPreview.PageNo;
				export.AllowOpenAfter = false;
				export.ShowProgress = true;
				export.Export( CurrentPreview.Report );
				fileNames = export.GeneratedFiles;
			}

			if( fileNames.Count > 0 )
			{
				Form form = FindForm();
				string[] recipientAddresses = CurrentPreview.Report.EmailSettings.Recipients == null ?
				  new string[] { } : CurrentPreview.Report.EmailSettings.Recipients;
				int error = MAPI.SendMail( form == null ? IntPtr.Zero : form.Handle, fileNames.ToArray(),
				  CurrentPreview.Report.EmailSettings.Subject,
				  CurrentPreview.Report.EmailSettings.Message, new string[] { }, recipientAddresses );
				if( error > 1 )
				{
					MessageBox.Show( "MAPISendMail failed! " + MAPI.GetErrorText( error ) );
				}
			}
		}

		private void FUpdateTimer_Tick( object sender, EventArgs e )
		{
			FUpdatingZoom = true;

			int zoom = (int)( FZoomToUpdate * 100 );
			slZoom.Text = zoom.ToString() + "%";
			if( zoom < 100 )
				zoom = (int)Math.Round( ( zoom - 25 ) / 0.75f );
			else if( zoom > 100 )
				zoom = ( zoom - 100 ) / 4 + 100;
			slZoom.Value = zoom;

			FUpdatingZoom = false;
			FUpdateTimer.Stop();
		}

		private void tabControl1_TabItemClose( object sender, TabStripActionEventArgs e )
		{
			DeleteTab( FCurrentPreview );
			e.Cancel = true;
			tabControl.RecalcLayout();
		}

		private void tabControl1_SelectedTabChanged( object sender, FastReport.DevComponents.DotNetBar.TabStripTabChangedEventArgs e )
		{
			if( FLocked )
				return;
			FCurrentPreview = tabControl.SelectedTab as PreviewTab;
			if( FCurrentPreview != null && !FCurrentPreview.Fake )
			{
				FCurrentPreview.BindPreparedPages();
				UpdateOutline();
				UpdateZoom( FCurrentPreview.Zoom );
				UpdatePageNumbers( FCurrentPreview.PageNo, FCurrentPreview.PageCount );
			}
		}

		private void tabControl_Resize( object sender, EventArgs e )
		{
			foreach( PreviewTab tab in FDocuments )
			{
				tab.UpdatePages();
			}
		}

		private bool CanDisposeTabReport( PreviewTab tab )
		{
			if( tab == null || tab.Report == null )
				return false;

			// if the preview is owned by Report, do not dispose
			if( FReport == tab.Report )
				return false;

			// check if the same Report is used in other tabs
			foreach( PreviewTab t in FDocuments )
			{
				if( t != tab && t.Report == tab.Report )
				{
					return false;
				}
			}
			return true;
		}

		// Simon: void Localize 改为 MyRes Localize_inner
		private MyRes Localize_inner()
		{
			MyRes res = new MyRes( "Preview" );
			btnDesign.Text = res.Get( "DesignText" );
			btnDesign.Tooltip = res.Get( "Design" );
			btnPrint.Text = res.Get( "PrintText" );
			btnPrint.Tooltip = res.Get( "Print" );
			btnOpen.Tooltip = res.Get( "Open" );
			btnSave.Tooltip = res.Get( "Save" );
			btnSave.Text = res.Get( "SaveText" );
			btnEmail.Tooltip = res.Get( "Email" );
			btnEmailMapi.Tooltip = res.Get( "Email" );
			btnFind.Tooltip = res.Get( "Find" );
			btnOutline.Tooltip = res.Get( "Outline" );
			btnPageSetup.Tooltip = res.Get( "PageSetup" );
			btnEdit.Tooltip = res.Get( "Edit" );
			btnWatermark.Tooltip = res.Get( "Watermark" );
			btnFirst.Tooltip = res.Get( "First" );
			btnPrior.Tooltip = res.Get( "Prior" );
			btnNext.Tooltip = res.Get( "Next" );
			lblTotalPages.Text = String.Format( Res.Get( "Misc,ofM" ), 1 );
			btnLast.Tooltip = res.Get( "Last" );
			btnClose.Text = Res.Get( "Buttons,Close" );

			btnDesign.Image = Res.GetImage( 68 );
			btnPrint.Image = Res.GetImage( 195 );
			btnOpen.Image = Res.GetImage( 1 );
			btnSave.Image = Res.GetImage( 2 );
			btnEmail.Image = Res.GetImage( 200 );
			btnEmailMapi.Image = Res.GetImage( 200 );
			btnFind.Image = Res.GetImage( 181 );
			btnOutline.Image = Res.GetImage( 196 );
			btnPageSetup.Image = Res.GetImage( 13 );
			btnEdit.Image = Res.GetImage( 198 );
			btnWatermark.Image = Res.GetImage( 194 );
			btnFirst.Image = Res.GetImage( 185 );
			btnPrior.Image = Res.GetImage( 186 );
			btnNext.Image = Res.GetImage( 187 );
			btnLast.Image = Res.GetImage( 188 );
			btnZoomPageWidth.Image = ResourceLoader.GetBitmap( "ZoomPageWidth.png" );
			btnZoomWholePage.Image = ResourceLoader.GetBitmap( "ZoomWholePage.png" );
			btnZoom100.Image = ResourceLoader.GetBitmap( "Zoom100.png" );

			// Simon: 增加返回 return res;
			return res;
		}

		private void Init()
		{
			outlineControl.SetPreview( this );
			FUpdateTimer = new Timer();
			FUpdateTimer.Interval = 50;
			FUpdateTimer.Tick += new EventHandler( FUpdateTimer_Tick );
			FPageBorderColor = Color.FromArgb( 80, 80, 80 );
			FActivePageBorderColor = Color.FromArgb( 255, 199, 60 );
			FPageOffset = new Point( 10, 10 );
			FDefaultZoom = 1;
			FButtons = PreviewButtons.All;
			FExports = PreviewExports.All;
			FClouds = PreviewClouds.All;
			Font = DrawUtils.Default96Font;
			toolBar.Font = Font;
			statusBar.Font = Font;
			CreateExportList( btnSave, new EventHandler( Export_Click ) );
			CreateExportList( btnEmailMapi, new EventHandler( Email_Click ) );
			CreateCloudList( btnSave, new EventHandler( SaveToCloud_Click ) );
			CreateMessengersList( btnSave, new EventHandler( SendViaMessenger_Click ) );
			RestoreState();
			UpdateButtons();
		}

		private void RestoreState()
		{
			XmlItem xi = Config.Root.FindItem( "Preview" );

			string zoom = xi.GetProp( "Zoom" );
			if( !String.IsNullOrEmpty( zoom ) )
				FDefaultZoom = (float)Converter.FromString( typeof( float ), zoom );

			string width = xi.GetProp( "OutlineWidth" );
			if( !String.IsNullOrEmpty( width ) )
				outlineControl.Width = int.Parse( width );
		}

		private void SaveState()
		{
			// Clear();
			outlineControl.Hide();

			XmlItem xi = Config.Root.FindItem( "Preview" );
			xi.SetProp( "Zoom", Converter.ToString( Zoom ) );
			xi.SetProp( "OutlineWidth", outlineControl.Width.ToString() );
		}

		private void UpdateUIStyle()
		{
			switch( UIStyle )
			{
				case UIStyle.Office2003:
				case UIStyle.Office2007Blue:
				case UIStyle.Office2010Blue:
					StyleManager.ChangeStyle( eStyle.Office2010Blue, Color.Empty );
					break;
				case UIStyle.Office2007Silver:
				case UIStyle.Office2010Silver:
					StyleManager.ChangeStyle( eStyle.Office2010Silver, Color.Empty );
					break;
				case UIStyle.Office2007Black:
				case UIStyle.Office2010Black:
					StyleManager.ChangeStyle( eStyle.Office2010Black, Color.Empty );
					break;
				case UIStyle.Office2013:
					StyleManager.ChangeStyle( eStyle.Office2013, Color.Empty );
					break;
				case UIStyle.VisualStudio2005:
				case UIStyle.VisualStudio2010:
					StyleManager.ChangeStyle( eStyle.VisualStudio2010Blue, Color.Empty );
					break;
				case UIStyle.VisualStudio2012Light:
					StyleManager.ChangeStyle( eStyle.VisualStudio2012Light, Color.Empty );
					break;
				case UIStyle.VistaGlass:
					StyleManager.ChangeStyle( eStyle.Windows7Blue, Color.Empty );
					break;
			}

			//UIStyleUtils.UpdateUIStyle();

			eDotNetBarStyle style = UIStyleUtils.GetDotNetBarStyle( UIStyle );
			toolBar.Style = style;
			statusBar.Style = style;
			tabControl.Style = UIStyleUtils.GetTabStripStyle( UIStyle );
			outlineControl.Style = UIStyle;

			foreach( PreviewTab tab in FDocuments )
			{
				tab.Style = UIStyle;
			}
		}

		private void UpdateOutline()
		{
			outlineControl.PreparedPages = FCurrentPreview.PreparedPages;
			OutlineVisible = !FCurrentPreview.PreparedPages.Outline.IsEmpty;
		}

		private void AddFakeTab()
		{
			PreviewTab tab = new PreviewTab( this, null, "", null );
			tab.Fake = true;
			FDocuments.Add( tab );
			tab.AddToTabControl( tabControl );
		}

		private void UpdateTabsVisible()
		{
			tabControl.TabsVisible = FDocuments.Count > 1 && !FDocuments[0].Fake;
		}

		private PreviewTab FindTab( string text )
		{
			foreach( PreviewTab tab in FDocuments )
			{
				if( tab.Text == text )
					return tab;
			}

			return null;
		}

		private PreviewTab FindTabByHyperlinkValue( string value )
		{
			foreach( PreviewTab tab in FDocuments )
			{
				if( tab.HyperlinkValue == value )
					return tab;
			}

			return null;
		}
		#endregion

		#region Protected Methods
		/// <inheritdoc/>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
				FUpdateTimer.Dispose();
				SaveState();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Public Methods
		internal void SetReport( Report report )
		{
			FReport = report;
		}

		internal void UpdatePageNumbers( int pageNo, int totalPages )
		{
			lblStatus.Text = String.Format( Res.Get( "Misc,PageNofM" ), pageNo, totalPages );
			tbPageNo.Text = pageNo.ToString();
			lblTotalPages.Text = String.Format( Res.Get( "Misc,ofM" ), totalPages );
			if( PageChanged != null )
				PageChanged( this, EventArgs.Empty );
		}

		internal void UpdateZoom( float zoom )
		{
			FZoomToUpdate = zoom;
			FUpdateTimer.Start();
		}

		internal void UpdateUrl( string url )
		{
			lblUrl.Text = url;
		}

		internal void ShowPerformance( string text )
		{
			lblPerformance.Text = text;
		}

		internal void DoClick()
		{
			OnClick( EventArgs.Empty );
		}

		// Clears all tabs except the first one. This method is used in the report.Prepare.
		// It is needed to avoid flickering when using stand-alone PreviewControl. 
		// When report is prepared and ShowPrepared method is called, the "fake" tab will
		// be replaced with the new tab.
		internal void ClearTabsExceptFirst()
		{
			while( FDocuments.Count > 1 )
			{
				DeleteTab( FDocuments[FDocuments.Count - 1] );
			}
			if( FDocuments.Count == 1 )
				FDocuments[0].Fake = true;
		}

		internal PreviewTab AddPreviewTab( Report report, string text, Hyperlink hyperlink, bool setActive )
		{
			PreviewTab tab = new PreviewTab( this, report, text, hyperlink );
			FDocuments.Add( tab );
			//report.PreparedPages.ClearPageCache();
			//OutlineVisible = !report.PreparedPages.Outline.IsEmpty;
			tab.AddToTabControl( tabControl );
			tab.UnlockLayout();
			UpdateTabsVisible();
			tab.UpdatePages();

			if( setActive )
			{
				// do not stole the focus
				tabControl.TabStrip.AutoSelectAttachedControl = false;
				tabControl.SelectedTab = tab;
				tabControl.TabStrip.AutoSelectAttachedControl = true;
			}
			else
				tabControl.Refresh();

			if( FDocuments.Count == 2 && FDocuments[0].Fake )
				DeleteTab( FDocuments[0] );
			return tab;
		}

		/// <summary>
		/// Adds a new report tab to the preview control.
		/// </summary>
		/// <param name="report">The <b>Report</b> object that contains the prepared report.</param>
		/// <param name="text">The title for the new tab.</param>
		/// <remarks>
		/// Prepare the report using its <b>Prepare</b> method before you pass it to the <b>report</b> parameter.
		/// </remarks>
		public void AddTab( Report report, string text )
		{
			AddTab( report, text, true );
		}

		/// <summary>
		/// Adds a new report tab to the preview control.
		/// </summary>
		/// <param name="report">The <b>Report</b> object that contains the prepared report.</param>
		/// <param name="text">The title for the new tab.</param>
		/// <param name="setActive">If <b>true</b>, makes the new tab active.</param>
		/// <remarks>
		/// Prepare the report using its <b>Prepare</b> method before you pass it to the <b>report</b> parameter.
		/// </remarks>
		public void AddTab( Report report, string text, bool setActive )
		{
			if( FReport == null )
				SetReport( report );
			AddPreviewTab( report, text, null, setActive );
		}

		/// <summary>
		/// Switches to the tab with specified text.
		/// </summary>
		/// <param name="text">Text of the tab.</param>
		/// <returns><b>true</b> if the tab with specified text exists, or <b>false</b> if there is no such tab.</returns>
		public bool SwitchToTab( string text )
		{
			PreviewTab tab = FindTab( text );
			if( tab != null )
			{
				tabControl.SelectedTab = tab;
				return true;
			}

			return false;
		}

		internal bool SwitchToTab( Hyperlink hyperlink )
		{
			PreviewTab tab = FindTabByHyperlinkValue( hyperlink.Value );
			if( tab != null )
			{
				tabControl.SelectedTab = tab;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Deletes the report tab with specified text.
		/// </summary>
		/// <param name="text">The text of the tab.</param>
		public void DeleteTab( string text )
		{
			PreviewTab tab = FindTab( text );
			if( tab != null )
				DeleteTab( tab );
		}

		/// <summary>
		/// Checks if the tab with specified text exists.
		/// </summary>
		/// <param name="text">The text of the tab.</param>
		/// <returns><b>true</b> if the tab exists.</returns>
		public bool TabExists( string text )
		{
			return FindTab( text ) != null;
		}

		internal void DeleteTab( PreviewTab tab )
		{
			if( CanDisposeTabReport( tab ) )
				tab.Report.Dispose();
			FDocuments.Remove( tab );
			tabControl.Tabs.Remove( tab );
			tab.Dispose();
			UpdateTabsVisible();
		}

		/// <summary>
		/// Displays the text in the status bar.
		/// </summary>
		/// <param name="text">Text to display.</param>
		public void ShowStatus( string text )
		{
			lblStatus.Text = text;
			statusBar.Refresh();
		}

		internal void Lock()
		{
			FLocked = true;
		}

		internal void Unlock()
		{
			FLocked = false;
		}

		/// <summary>
		/// Sets the focus to the preview control.
		/// </summary>
		public new void Focus()
		{
			if( FCurrentPreview != null )
				FCurrentPreview.Focus();
		}
		#endregion

		#region Event handlers
		private void btnDesign_Click( object sender, EventArgs e )
		{
			Design();
		}

		private void btnPrint_Click( object sender, EventArgs e )
		{
			Print();
		}

		private void btnOpen_Click( object sender, EventArgs e )
		{
			Load();
		}

		private void btnEmail_Click( object sender, EventArgs e )
		{
			SendEmail();
		}

		private void btnFind_Click( object sender, EventArgs e )
		{
			Find();
		}

		private void slZoom_ValueChanged( object sender, EventArgs e )
		{
			if( FUpdatingZoom )
				return;

			int val = slZoom.Value;
			if( val < 100 )
				val = (int)Math.Round( val * 0.75f ) + 25;
			else
				val = ( val - 100 ) * 4 + 100;

			Zoom = val / 100f;
			slZoom.Text = val.ToString() + "%";
		}

		private void btnZoomPageWidth_Click( object sender, EventArgs e )
		{
			ZoomPageWidth();
		}

		private void btnZoomWholePage_Click( object sender, EventArgs e )
		{
			ZoomWholePage();
		}

		private void btnZoom100_Click( object sender, EventArgs e )
		{
			Zoom = 1;
		}

		private void btnEdit_Click( object sender, EventArgs e )
		{
			EditPage();
		}

		private void btnFirst_Click( object sender, EventArgs e )
		{
			First();
		}

		private void btnPrior_Click( object sender, EventArgs e )
		{
			Prior();
		}

		private void btnNext_Click( object sender, EventArgs e )
		{
			Next();
		}

		private void btnLast_Click( object sender, EventArgs e )
		{
			Last();
		}

		private void tbPageNo_KeyDown( object sender, KeyEventArgs e )
		{
			if( e.KeyCode == Keys.Enter )
			{
				try
				{
					PageNo = int.Parse( tbPageNo.Text );
				}
				catch
				{
					PageNo = PageCount;
				}
				CurrentPreview.Focus();
			}
		}

		private void tbPageNo_KeyPress( object sender, KeyPressEventArgs e )
		{
			if( e.KeyChar != '\b' && ( e.KeyChar < '0' || e.KeyChar > '9' ) )
				e.Handled = true;
		}

		private void btnWatermark_Click( object sender, EventArgs e )
		{
			EditWatermark();
		}

		private void btnOutline_Click( object sender, EventArgs e )
		{
			OutlineVisible = btnOutline.Checked;
		}

		private void btnPageSetup_Click( object sender, EventArgs e )
		{
			PageSetup();
		}

		private void btnClose_Click( object sender, EventArgs e )
		{
			if( FindForm() != null )
				FindForm().Close();
		}

		private void tbPageNo_Click( object sender, EventArgs e )
		{
			tbPageNo.SelectAll();
		}
		#endregion

		#region Preview commands
		/// <summary>
		/// Prints the current report.
		/// </summary>
		/// <returns><b>true</b> if report was printed; <b>false</b> if user cancels the "Print" dialog.</returns>
		public bool Print()
		{
			if( CurrentPreview == null )
				return false;
			return CurrentPreview.Print();
		}

		/// <summary>
		/// Saves the current report to a .fpx file using the "Save FIle" dialog.
		/// </summary>
		public void Save()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Save();
		}

		/// <summary>
		/// Saves the current report to a specified .fpx file.
		/// </summary>
		public void Save( string fileName )
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Save( fileName );
		}

		/// <summary>
		/// Saves the current report to a stream.
		/// </summary>
		public void Save( Stream stream )
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Save( stream );
		}

		private bool PreLoad()
		{
			if( CurrentPreview == null )
				return false;
			if( FDocuments.Count == 1 && FDocuments[0].Fake )
			{
				Report report = new Report();
				report.SetPreparedPages( new PreparedPages( report ) );
				AddTab( report, "" );
			}
			return true;
		}

		private void PostLoad()
		{
			UpdateOutline();
		}

		/// <summary>
		/// Loads the report from a .fpx file using the "Open File" dialog.
		/// </summary>
		public new void Load()
		{
			if( !PreLoad() )
				return;
			CurrentPreview.Load();
			PostLoad();
		}

		/// <summary>
		/// Loads the report from a specified .fpx file.
		/// </summary>
		public new void Load( string fileName )
		{
			if( !PreLoad() )
				return;
			CurrentPreview.Load( fileName );
			PostLoad();
		}

		/// <summary>
		/// Load the report from a stream.
		/// </summary>
		/// <param name="stream">The stream to load from.</param>
		public new void Load( Stream stream )
		{
			if( !PreLoad() )
				return;
			CurrentPreview.Load( stream );
			PostLoad();
		}

		/// <summary>
		/// Sends an email.
		/// </summary>
		public void SendEmail()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.SendEmail();
		}

		/// <summary>
		/// Finds the text in the current report using the "Find Text" dialog.
		/// </summary>
		public void Find()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Find();
		}

		/// <summary>
		/// Finds the specified text in the current report.
		/// </summary>
		/// <param name="text">Text to find.</param>
		/// <param name="matchCase">A value indicating whether the search is case-sensitive.</param>
		/// <param name="wholeWord">A value indicating whether the search matches whole words only.</param>
		/// <returns><b>true</b> if text found.</returns>
		public bool Find( string text, bool matchCase, bool wholeWord )
		{
			if( CurrentPreview == null )
				return false;
			return CurrentPreview.Find( text, matchCase, wholeWord );
		}

		/// <summary>
		/// Finds the next occurence of text specified in the <b>Find</b> method.
		/// </summary>
		/// <returns><b>true</b> if text found.</returns>
		public bool FindNext()
		{
			if( CurrentPreview == null )
				return false;
			return CurrentPreview.FindNext();
		}

		/// <summary>
		/// Navigates to the first page.
		/// </summary>
		public void First()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.First();
		}

		/// <summary>
		/// Navigates to the previuos page.
		/// </summary>
		public void Prior()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Prior();
		}

		/// <summary>
		/// Navigates to the next page.
		/// </summary>
		public void Next()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Next();
		}

		/// <summary>
		/// Navigates to the last page.
		/// </summary>
		public void Last()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.Last();
		}

		/// <summary>
		/// Gets or sets the current page number.
		/// </summary>
		/// <remarks>
		/// This value is 1-based.
		/// </remarks>
		[Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public int PageNo
		{
			get
			{
				if( CurrentPreview == null )
					return 1;
				return CurrentPreview.PageNo;
			}
			set
			{
				if( CurrentPreview == null )
					return;
				CurrentPreview.PageNo = value;
			}
		}

		/// <summary>
		/// Gets the pages count in the current report.
		/// </summary>
		[Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public int PageCount
		{
			get
			{
				if( CurrentPreview == null )
					return 0;
				return CurrentPreview.PageCount;
			}
		}

		/// <summary>
		/// Gets or sets the zoom factor.
		/// </summary>
		/// <remarks>
		/// <b>1</b> corresponds to 100% zoom.
		/// </remarks>
		[Browsable( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public float Zoom
		{
			get
			{
				if( CurrentPreview == null )
					return 1;
				return CurrentPreview.Zoom;
			}
			set
			{
				if( CurrentPreview != null )
					CurrentPreview.Zoom = value;
			}
		}

		/// <summary>
		/// Zooms in.
		/// </summary>
		public void ZoomIn()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.ZoomIn();
		}

		/// <summary>
		/// Zooms out.
		/// </summary>
		public void ZoomOut()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.ZoomOut();
		}

		/// <summary>
		/// Zooms to fit the page width.
		/// </summary>
		public void ZoomPageWidth()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.ZoomPageWidth();
		}

		/// <summary>
		/// Zooms to fit the whole page.
		/// </summary>
		public void ZoomWholePage()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.ZoomWholePage();
		}

		/// <summary>
		/// Edits the current page in the designer.
		/// </summary>
		public void EditPage()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.EditPage();
		}

		/// <summary>
		/// Edits the current report in the designer.
		/// </summary>
		public void Design()
		{
			if( Report == null )
				return;
			using( Report designedReport = new Report() )
			{
				Report.PreparedPages.Clear();
				Report.PreparedPages.ClearPageCache();

				designedReport.FileName = Report.FileName;
				if( !String.IsNullOrEmpty( Report.FileName ) )
					designedReport.Load( designedReport.FileName );
				else
					using( MemoryStream repStream = new MemoryStream() )
					{
						Report.Save( repStream );
						repStream.Position = 0;
						designedReport.Load( repStream );
					}

				Report.Dictionary.ReRegisterData( designedReport.Dictionary );

				//string s = designedReport.SaveToString();
				//File.WriteAllText(@"c:\1\rep1.frx", s);
				//designedReport.LoadFromString(s);


				if( designedReport.Design() )
				{
					Report.LoadFromString( designedReport.SaveToString() );
					Report.PreparedPages.Clear();
					if( CurrentPreview != null )
					{
						Report.Preview = CurrentPreview.Preview;
						Report.Show();
					}
				}
			}
		}

		/// <summary>
		/// Edits the watermark.
		/// </summary>
		public void EditWatermark()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.EditWatermark();
		}

		/// <summary>
		/// Edits the page settings.
		/// </summary>
		public void PageSetup()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.PageSetup();
		}

		/// <summary>
		/// Navigates to the specified position inside a specified page.
		/// </summary>
		/// <param name="pageNo">The page number (1-based).</param>
		/// <param name="point">The position inside a page, in pixels.</param>
		public void PositionTo( int pageNo, PointF point )
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.PositionTo( pageNo, point );
		}

		/// <summary>
		/// Clears the preview.
		/// </summary>
		public void Clear()
		{
			while( FDocuments.Count > 0 )
			{
				DeleteTab( FDocuments[0] );
			}

			lblStatus.Text = "";
			tbPageNo.Text = "";
		}

		/// <summary>
		/// Refresh the report.
		/// </summary>
		public void RefreshReport()
		{
			if( CurrentPreview == null )
				return;
			CurrentPreview.RefreshReport();
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="PreviewControl"/> class.
		/// </summary>
		public PreviewControl()
		{
			// we need this to ensure that static constructor of the Report was called.
			Report report = new Report();
			report.Dispose();
			BarUtilities.UseTextRenderer = true;
			FDocuments = new List<PreviewTab>();
			InitializeComponent();
			FToolbarVisible = true;
			FStatusbarVisible = true;
			OutlineVisible = false;
			UIStyle = Config.UIStyle;
			Localize();
			Init();
			AddFakeTab();
			RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
		}
	}
}
