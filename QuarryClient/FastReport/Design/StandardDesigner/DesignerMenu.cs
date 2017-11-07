using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Design.ToolWindows;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
	/// <summary>
	/// Represents the designer's main menu.
	/// </summary>
	/// <remarks>
	/// To get this menu, use the following code:
	/// <code>
	/// Designer designer;
	/// DesignerMenu menu = designer.Plugins.FindType("DesignerMenu") as DesignerMenu;
	/// </code>
	/// </remarks>
	[ToolboxItem( false )]
	public partial class DesignerMenu : Bar, IDesignerPlugin
	{
		#region Fields
		private Designer FDesigner;

		/// <summary>
		/// The "File" menu.
		/// </summary>
		public ButtonItem miFile;

		/// <summary>
		/// The "File|New..." menu.
		/// </summary>
		public ButtonItem miFileNew;

		/// <summary>
		/// The "File|Open..." menu.
		/// </summary>
		public ButtonItem miFileOpen;

		/// <summary>
		/// The "File|Close" menu.
		/// </summary>
		public ButtonItem miFileClose;

		/// <summary>
		/// The "File|Save" menu.
		/// </summary>
		public ButtonItem miFileSave;

		/// <summary>
		/// The "File|Save as..." menu.
		/// </summary>
		public ButtonItem miFileSaveAs;

		/// <summary>
		/// The "File|Save All" menu.
		/// </summary>
		public ButtonItem miFileSaveAll;

		/// <summary>
		/// The "File|Page Setup..." menu.
		/// </summary>
		public ButtonItem miFilePageSetup;

		/// <summary>
		/// The "File|Printer Setup..." menu.
		/// </summary>
		public ButtonItem miFilePrinterSetup;

		/// <summary>
		/// The "File|Preview..." menu.
		/// </summary>
		public ButtonItem miFilePreview;

		/// <summary>
		/// The "File|Select Language" menu.
		/// </summary>
		public ButtonItem miFileSelectLanguage;

		/// <summary>
		/// The "File|Exit" menu.
		/// </summary>
		public ButtonItem miFileExit;

		/// <summary>
		/// The "Edit" menu.
		/// </summary>
		public ButtonItem miEdit;

		/// <summary>
		/// The "Edit|Undo" menu.
		/// </summary>
		public ButtonItem miEditUndo;

		/// <summary>
		/// The "Edit|Redo" menu.
		/// </summary>
		public ButtonItem miEditRedo;

		/// <summary>
		/// The "Edit|Cut" menu.
		/// </summary>
		public ButtonItem miEditCut;

		/// <summary>
		/// The "Edit|Copy" menu.
		/// </summary>
		public ButtonItem miEditCopy;

		/// <summary>
		/// The "Edit|Paste" menu.
		/// </summary>
		public ButtonItem miEditPaste;

		/// <summary>
		/// The "Edit|Delete" menu.
		/// </summary>
		public ButtonItem miEditDelete;

		/// <summary>
		/// The "Edit|Delete Page" menu.
		/// </summary>
		public ButtonItem miEditDeletePage;

		/// <summary>
		/// The "Edit|Select All" menu.
		/// </summary>
		public ButtonItem miEditSelectAll;

		/// <summary>
		/// The "Edit|Group" menu.
		/// </summary>
		public ButtonItem miEditGroup;

		/// <summary>
		/// The "Edit|Ungroup" menu.
		/// </summary>
		public ButtonItem miEditUngroup;

		/// <summary>
		/// The "Edit|Find..." menu.
		/// </summary>
		public ButtonItem miEditFind;

		/// <summary>
		/// The "Edit|Replace..." menu.
		/// </summary>
		public ButtonItem miEditReplace;


		/// <summary>
		/// The "View" menu.
		/// </summary>
		public ButtonItem miView;

		/// <summary>
		/// The "View|Toolbars" menu.
		/// </summary>
		public ButtonItem miViewToolbars;

		/// <summary>
		/// The "View|Start Page" menu.
		/// </summary>
		public ButtonItem miViewStartPage;

		/// <summary>
		/// The "View|Options..." menu.
		/// </summary>
		public ButtonItem miViewOptions;


		/// <summary>
		/// The "Insert" menu.
		/// </summary>
		public ButtonItem miInsert;


		/// <summary>
		/// The "Report" menu.
		/// </summary>
		public ButtonItem miReport;

		/// <summary>
		/// The "Report|Options..." menu.
		/// </summary>
		public ButtonItem miReportOptions;


		/// <summary>
		/// The "Data" menu.
		/// </summary>
		public ButtonItem miData;

		/// <summary>
		/// The "Data|Choose Report Data..." menu.
		/// </summary>
		public ButtonItem miDataChoose;

		/// <summary>
		/// The "Data|Add Data Source..." menu.
		/// </summary>
		public ButtonItem miDataAdd;

		/// <summary>
		/// The "Data|Show Data Dictionary" menu.
		/// </summary>
		public ButtonItem miDataShowData;


		/// <summary>
		/// The "Window" menu.
		/// </summary>
		public ButtonItem miWindow;

		/// <summary>
		/// The "Window|Close All" menu.
		/// </summary>
		public ButtonItem miWindowCloseAll;


		/// <summary>
		/// The "Help" menu.
		/// </summary>
		public ButtonItem miHelp;

		/// <summary>
		/// The "Help|Help Contents..." menu.
		/// </summary>
		public ButtonItem miHelpContents;

		/// <summary>
		/// The "Help|About..." menu.
		/// </summary>
		public ButtonItem miHelpAbout;
		#endregion

		#region Properties
		internal Designer Designer
		{
			get
			{
				return FDesigner;
			}
		}

		/// <inheritdoc/>
		public string PluginName
		{
			get
			{
				return Name;
			}
		}
		#endregion

		#region Private Methods
		private void UpdateControls()
		{
			miFileNew.Enabled = Designer.cmdNew.Enabled;
			miFileOpen.Enabled = Designer.cmdOpen.Enabled;
			miFileClose.Enabled = Designer.cmdClose.Enabled;
			miFileClose.Visible = Designer.MdiMode;
			miFileSave.Enabled = Designer.cmdSave.Enabled;
			miFileSaveAs.Enabled = Designer.cmdSaveAs.Enabled;
			miFileSaveAll.Visible = Designer.MdiMode;
			miFileSaveAll.Enabled = Designer.cmdSaveAll.Enabled;
			miFilePageSetup.Enabled = Designer.cmdPageSetup.Enabled;
			miFilePrinterSetup.Enabled = Designer.cmdPrinterSetup.Enabled;
			miFilePreview.Enabled = Designer.cmdPreview.Enabled;
			miEditUndo.Enabled = Designer.cmdUndo.Enabled;
			miEditRedo.Enabled = Designer.cmdRedo.Enabled;
			miEditCut.Enabled = Designer.cmdCut.Enabled;
			miEditCopy.Enabled = Designer.cmdCopy.Enabled;
			miEditDeletePage.Enabled = Designer.cmdDeletePage.Enabled;
			miEditDelete.Enabled = Designer.cmdDelete.Enabled;
			miEditSelectAll.Enabled = Designer.cmdSelectAll.Enabled;
			miEditGroup.Enabled = Designer.cmdGroup.Enabled;
			miEditUngroup.Enabled = Designer.cmdUngroup.Enabled;
			miEditFind.Enabled = Designer.cmdFind.Enabled;
			miEditReplace.Enabled = Designer.cmdReplace.Enabled;
			miInsert.Visible = Designer.cmdInsert.Enabled;
			miDataChoose.Enabled = Designer.cmdChooseData.Enabled;
			miDataAdd.Enabled = Designer.cmdAddData.Enabled;
			miReportOptions.Enabled = Designer.cmdReportSettings.Enabled;
			miViewStartPage.Visible = Designer.MdiMode;
			miWindow.Visible = Designer.MdiMode;
			miHelpContents.Enabled = Designer.cmdHelpContents.Enabled;

			Refresh();
		}

		private void InsertMenuCreateMenus( ObjectInfo rootItem, SubItemsCollection rootMenu )
		{
			foreach( ObjectInfo item in rootItem.Items )
			{
				ButtonItem menuItem = new ButtonItem();
				menuItem.Text = Res.TryGet( item.Text );
				menuItem.Tag = item;
				rootMenu.Add( menuItem );

				if( item.Items.Count > 0 )
				{
					// it's a category
					InsertMenuCreateMenus( item, menuItem.SubItems );
				}
				else
				{
					menuItem.Image = item.Image;
					menuItem.Click += insertMenu_Click;
				}
			}
		}

		private void CreateInsertMenu()
		{
			if( Designer.ActiveReportTab != null && Designer.ActiveReportTab.ActivePage != null )
			{
				ObjectInfo pageItem = RegisteredObjects.FindObject( Designer.ActiveReportTab.ActivePage );
				if( pageItem != null )
				{
					InsertMenuCreateMenus( pageItem, miInsert.SubItems );
				}
			}
		}

		private void miFile_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			// clear existing recent items
			int i = miFile.SubItems.IndexOf( miFileSelectLanguage ) + 1;
			while( miFile.SubItems[i] != miFileExit )
			{
				miFile.SubItems[i].Dispose();
				miFile.SubItems.RemoveAt( i );
			}
			// add new items
			if( Designer.cmdRecentFiles.Enabled && Designer.RecentFiles.Count > 0 )
			{
				foreach( string s in Designer.RecentFiles )
				{
					ButtonItem menuItem = new ButtonItem();
					menuItem.Text = s;
					menuItem.Click += recentFile_Click;
					miFile.SubItems.Insert( i, menuItem );
				}
				// make the first item separator
				i = miFile.SubItems.IndexOf( miFileSelectLanguage ) + 1;
				( miFile.SubItems[i] as ButtonItem ).BeginGroup = true;
			}
		}

		private void miEdit_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			miEditPaste.Enabled = Designer.cmdPaste.Enabled;
		}

		private void miInsert_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			miInsert.SubItems.Clear();
			CreateInsertMenu();
		}

		private void miDataShowDataSources_Click( object sender, EventArgs e )
		{
			ToolWindowBase window = Designer.Plugins.Find( "DictionaryWindow" ) as ToolWindowBase;
			window.Show();
		}

		private void miView_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			// delete list of toolwindows
			while( miView.SubItems[0] != miViewStartPage )
			{
				miView.SubItems[0].Dispose();
				miView.SubItems.RemoveAt( 0 );
			}

			// create list of toolwindows  
			foreach( IDesignerPlugin plugin in Designer.Plugins )
			{
				if( plugin is ToolWindowBase )
				{
					ButtonItem menuItem = new ButtonItem();
					menuItem.Text = ( plugin as ToolWindowBase ).Text;
					menuItem.Image = ( plugin as ToolWindowBase ).Image;
					eShortcut shortcut = ( plugin as ToolWindowBase ).Shortcut;
					if( shortcut != eShortcut.None )
						menuItem.Shortcuts.Add( shortcut );
					menuItem.Tag = plugin;
					menuItem.Click += toolWindow_Click;
					miView.SubItems.Insert( 0, menuItem );
				}
			}

			// delete list of toolbars
			miViewToolbars.SubItems.Clear();

			// create list of toolbars
			foreach( IDesignerPlugin plugin in Designer.Plugins )
			{
				if( plugin is ToolbarBase )
				{
					ButtonItem menuItem = new ButtonItem();
					menuItem.Text = ( plugin as ToolbarBase ).Text;
					menuItem.Tag = plugin;
					menuItem.Checked = ( plugin as ToolbarBase ).Visible;
					menuItem.AutoCheckOnClick = true;
					menuItem.Click += toolbar_Click;
					miViewToolbars.SubItems.Add( menuItem );
				}
			}
		}

		private void miWindow_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			// delete list of windows
			while( miWindow.SubItems[0] != miWindowCloseAll )
			{
				miWindow.SubItems[0].Dispose();
				miWindow.SubItems.RemoveAt( 0 );
			}

			// create list of windows
			int i = 0;
			foreach( DocumentWindow c in Designer.Documents )
			{
				ButtonItem menuItem = new ButtonItem();
				menuItem.Text = ( i + 1 ).ToString() + " " + c.Text;
				menuItem.Tag = c;
				menuItem.Checked = c == Designer.ActiveReportTab;
				menuItem.Click += window_Click;
				miWindow.SubItems.Insert( i, menuItem );
				i++;
			}
		}

		private void toolWindow_Click( object sender, EventArgs e )
		{
			ToolWindowBase window = ( sender as ButtonItem ).Tag as ToolWindowBase;
			window.Show();
		}

		private void toolbar_Click( object sender, EventArgs e )
		{
			ToolbarBase toolbar = ( sender as ButtonItem ).Tag as ToolbarBase;
			toolbar.Visible = !toolbar.Visible;
		}

		private void recentFile_Click( object sender, EventArgs e )
		{
			Designer.UpdatePlugins( null );
			Designer.cmdOpen.LoadFile( ( sender as ButtonItem ).Text );
		}

		private void window_Click( object sender, EventArgs e )
		{
			DocumentWindow window = ( sender as ButtonItem ).Tag as DocumentWindow;
			window.Activate();
		}

		private void insertMenu_Click( object sender, EventArgs e )
		{
			ObjectInfo info = ( sender as ButtonItem ).Tag as ObjectInfo;
			Designer.InsertObject( info, InsertFrom.NewObject );
		}
		#endregion

		#region IDesignerPlugin
		/// <inheritdoc/>
		public void SaveState()
		{
		}

		/// <inheritdoc/>
		public void RestoreState()
		{
		}

		/// <inheritdoc/>
		public void SelectionChanged()
		{
			UpdateControls();
		}

		/// <inheritdoc/>
		public void UpdateContent()
		{
			UpdateControls();
		}

		/// <inheritdoc/>
		public void Lock()
		{
		}

		/// <inheritdoc/>
		public void Unlock()
		{
			UpdateContent();
		}

		// Simon: Localize 改为 Localize_inner
		/// <inheritdoc/>
		public MyRes Localize_inner()
		{
			MyRes res = new MyRes( "Designer,Menu" );

			miFile.Text = res.Get( "File" );
			miFileNew.Text = res.Get( "File,New" );
			miFileOpen.Text = res.Get( "File,Open" );
			miFileClose.Text = res.Get( "File,Close" );
			miFileSave.Text = res.Get( "File,Save" );
			miFileSaveAs.Text = res.Get( "File,SaveAs" );
			miFileSaveAll.Text = res.Get( "File,SaveAll" );
			miFilePageSetup.Text = res.Get( "File,PageSetup" );
			miFilePrinterSetup.Text = res.Get( "File,PrinterSetup" );
			miFilePreview.Text = res.Get( "File,Preview" );
			miFileSelectLanguage.Text = res.Get( "File,SelectLanguage" );
			miFileExit.Text = res.Get( "File,Exit" );

			miEdit.Text = res.Get( "Edit" );
			miEditUndo.Text = res.Get( "Edit,Undo" );
			miEditRedo.Text = res.Get( "Edit,Redo" );
			miEditCut.Text = res.Get( "Edit,Cut" );
			miEditCopy.Text = res.Get( "Edit,Copy" );
			miEditPaste.Text = res.Get( "Edit,Paste" );
			miEditDelete.Text = res.Get( "Edit,Delete" );
			miEditDeletePage.Text = res.Get( "Edit,DeletePage" );
			miEditSelectAll.Text = res.Get( "Edit,SelectAll" );
			miEditGroup.Text = res.Get( "Edit,Group" );
			miEditUngroup.Text = res.Get( "Edit,Ungroup" );
			miEditFind.Text = res.Get( "Edit,Find" );
			miEditReplace.Text = res.Get( "Edit,Replace" );

			miInsert.Text = res.Get( "Insert" );

			miReport.Text = res.Get( "Report" );
			miReportOptions.Text = res.Get( "Report,Options" );

			miData.Text = res.Get( "Data" );
			miDataChoose.Text = res.Get( "Data,Choose" );
			miDataAdd.Text = res.Get( "Data,Add" );
			miDataShowData.Text = res.Get( "Data,ShowData" );

			miView.Text = res.Get( "View" );
			miViewToolbars.Text = res.Get( "View,Toolbars" );
			miViewStartPage.Text = res.Get( "View,StartPage" );
			miViewOptions.Text = res.Get( "View,Options" );

			miWindow.Text = res.Get( "Window" );
			miWindowCloseAll.Text = res.Get( "Window,CloseAll" );

			miHelp.Text = res.Get( "Help" );
			miHelpContents.Text = res.Get( "Help,Contents" );
			miHelpAbout.Text = res.Get( "Help,About" );

			// Simon: 增加 return res;
			return res;
		}

		/// <inheritdoc/>
		public virtual DesignerOptionsPage GetOptionsPage()
		{
			return null;
		}

		/// <inheritdoc/>
		public void UpdateUIStyle()
		{
			Style = UIStyleUtils.GetDotNetBarStyle( Designer.UIStyle );
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates a new menu item.
		/// </summary>
		/// <returns>New menu item.</returns>
		public ButtonItem CreateMenuItem()
		{
			return CreateMenuItem( null );
		}

		/// <summary>
		/// Creates a new menu item.
		/// </summary>
		/// <param name="click">Click handler.</param>
		/// <returns>New menu item.</returns>
		public ButtonItem CreateMenuItem( EventHandler click )
		{
			return CreateMenuItem( null, "", click );
		}

		/// <summary>
		/// Creates a new menu item.
		/// </summary>
		/// <param name="image">Item's image.</param>
		/// <param name="click">Click handler.</param>
		/// <returns>New menu item.</returns>
		public ButtonItem CreateMenuItem( Image image, EventHandler click )
		{
			return CreateMenuItem( image, "", click );
		}

		/// <summary>
		/// Creates a new menu item.
		/// </summary>
		/// <param name="text">Item's text.</param>
		/// <param name="click">Click handler.</param>
		/// <returns>New menu item.</returns>
		public ButtonItem CreateMenuItem( string text, EventHandler click )
		{
			return CreateMenuItem( null, text, click );
		}

		/// <summary>
		/// Creates a new menu item.
		/// </summary>
		/// <param name="image">Item's image.</param>
		/// <param name="text">Item's text.</param>
		/// <param name="click">Click handler.</param>
		/// <returns>New menu item.</returns>
		public ButtonItem CreateMenuItem( Image image, string text, EventHandler click )
		{
			ButtonItem item = new ButtonItem();
			item.Image = image;
			item.Text = text;
			if( click != null )
				item.Click += click;
			return item;
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="DesignerMenu"/> class with default settings.
		/// </summary>
		/// <param name="designer">The report designer.</param>
		public void DesignerMenu_inner( Designer designer )
		{
			// Simon: DesignerMenu 改为 DesignerMenu_inner，去掉  : base()
			FDesigner = designer;

			Name = "MainMenu";
			Font = DrawUtils.DefaultFont;
			MenuBar = true;
			BarType = eBarType.MenuBar;
			Dock = DockStyle.Top;

			// create menu items
			miFile = CreateMenuItem();
			miFileNew = CreateMenuItem( Designer.cmdNew.Invoke );
			miFileOpen = CreateMenuItem( Res.GetImage( 1 ), Designer.cmdOpen.Invoke );
			miFileClose = CreateMenuItem( Designer.cmdClose.Invoke );
			miFileSave = CreateMenuItem( Res.GetImage( 2 ), Designer.cmdSave.Invoke );
			miFileSave.BeginGroup = true;
			miFileSaveAs = CreateMenuItem( Designer.cmdSaveAs.Invoke );
			miFileSaveAll = CreateMenuItem( Res.GetImage( 178 ), Designer.cmdSaveAll.Invoke );
			miFilePageSetup = CreateMenuItem( Designer.cmdPageSetup.Invoke );
			miFilePageSetup.BeginGroup = true;
			miFilePrinterSetup = CreateMenuItem( Designer.cmdPrinterSetup.Invoke );
			miFilePreview = CreateMenuItem( Res.GetImage( 3 ), Designer.cmdPreview.Invoke );
			miFileSelectLanguage = CreateMenuItem( Designer.cmdSelectLanguage.Invoke );
			miFileSelectLanguage.BeginGroup = true;
			miFileExit = CreateMenuItem( Designer.Exit );
			miFileExit.BeginGroup = true;

			miEdit = CreateMenuItem();
			miEditUndo = CreateMenuItem( Res.GetImage( 8 ), Designer.cmdUndo.Invoke );
			miEditRedo = CreateMenuItem( Res.GetImage( 9 ), Designer.cmdRedo.Invoke );
			miEditCut = CreateMenuItem( Res.GetImage( 5 ), Designer.cmdCut.Invoke );
			miEditCut.BeginGroup = true;
			miEditCopy = CreateMenuItem( Res.GetImage( 6 ), Designer.cmdCopy.Invoke );
			miEditPaste = CreateMenuItem( Res.GetImage( 7 ), Designer.cmdPaste.Invoke );
			miEditDelete = CreateMenuItem( Res.GetImage( 51 ), Designer.cmdDelete.Invoke );
			miEditDeletePage = CreateMenuItem( Res.GetImage( 12 ), Designer.cmdDeletePage.Invoke );
			miEditSelectAll = CreateMenuItem( Designer.cmdSelectAll.Invoke );
			miEditGroup = CreateMenuItem( Res.GetImage( 17 ), Designer.cmdGroup.Invoke );
			miEditGroup.BeginGroup = true;
			miEditUngroup = CreateMenuItem( Res.GetImage( 16 ), Designer.cmdUngroup.Invoke );
			miEditFind = CreateMenuItem( Res.GetImage( 181 ), Designer.cmdFind.Invoke );
			miEditFind.BeginGroup = true;
			miEditReplace = CreateMenuItem( Designer.cmdReplace.Invoke );

			miView = CreateMenuItem();
			miViewStartPage = CreateMenuItem( Res.GetImage( 179 ), Designer.cmdViewStartPage.Invoke );
			miViewToolbars = CreateMenuItem();
			miViewToolbars.BeginGroup = true;
			miViewOptions = CreateMenuItem( Designer.cmdOptions.Invoke );
			miViewOptions.BeginGroup = true;

			miInsert = CreateMenuItem();

			miReport = CreateMenuItem();
			miReportOptions = CreateMenuItem( Designer.cmdReportSettings.Invoke );
			miReportOptions.BeginGroup = true;

			miData = CreateMenuItem();
			miDataChoose = CreateMenuItem( Designer.cmdChooseData.Invoke );
			miDataAdd = CreateMenuItem( Res.GetImage( 137 ), Designer.cmdAddData.Invoke );
			miDataShowData = CreateMenuItem( Res.GetImage( 72 ), miDataShowDataSources_Click );

			miWindow = CreateMenuItem();
			miWindowCloseAll = CreateMenuItem( Res.GetImage( 202 ), Designer.cmdCloseAll.Invoke );
			miWindowCloseAll.BeginGroup = true;

			miHelp = CreateMenuItem();
			miHelpContents = CreateMenuItem( Res.GetImage( 90 ), Designer.cmdHelpContents.Invoke );
			miHelpAbout = CreateMenuItem( Designer.cmdAbout.Invoke );
			miHelpAbout.BeginGroup = true;

			// create menu structure
			Items.AddRange( new BaseItem[] { 
        miFile, miEdit, miView, miInsert, miReport, miData, miWindow, miHelp } );
			miFile.SubItems.AddRange( new BaseItem[] { 
        miFileNew, miFileOpen, miFileClose,
        miFileSave, miFileSaveAs, miFileSaveAll,
        miFilePageSetup, miFilePrinterSetup, miFilePreview, 
        miFileSelectLanguage, 
        miFileExit } );
			miEdit.SubItems.AddRange( new BaseItem[] { 
        miEditUndo, miEditRedo, 
        miEditCut, miEditCopy, miEditPaste, miEditDelete, miEditDeletePage, miEditSelectAll, 
        miEditGroup, miEditUngroup,
        miEditFind, miEditReplace } );
			miView.SubItems.AddRange( new BaseItem[] { 
        miViewStartPage, 
        miViewToolbars, 
        miViewOptions } );
			miInsert.SubItems.Add( new ButtonItem() ); // allow to catch PopupOpen
			miReport.SubItems.Add( miReportOptions );
			miData.SubItems.AddRange( new BaseItem[] {
        miDataChoose, miDataAdd, miDataShowData } );
			miWindow.SubItems.Add( miWindowCloseAll );
			miHelp.SubItems.AddRange( new BaseItem[] { 
        miHelpContents, 
        miHelpAbout } );

			// shortcuts
			miFileOpen.Shortcuts.Add( eShortcut.CtrlO );
			miFileSave.Shortcuts.Add( eShortcut.CtrlS );
			miFileSaveAll.Shortcuts.Add( eShortcut.CtrlShiftS );
			miFilePreview.Shortcuts.Add( eShortcut.CtrlP );
			miEditUndo.Shortcuts.Add( eShortcut.CtrlZ );
			miEditRedo.Shortcuts.Add( eShortcut.CtrlY );
			miEditCut.AlternateShortCutText = "Ctrl+X";
			miEditCopy.AlternateShortCutText = "Ctrl+C";
			miEditPaste.AlternateShortCutText = "Ctrl+V";
			miEditFind.Shortcuts.Add( eShortcut.CtrlF );
			miEditReplace.Shortcuts.Add( eShortcut.CtrlH );
			miHelpContents.Shortcuts.Add( eShortcut.F1 );

			// events
			miFile.PopupOpen += miFile_PopupOpen;
			miEdit.PopupOpen += miEdit_PopupOpen;
			miInsert.PopupOpen += miInsert_PopupOpen;
			miView.PopupOpen += miView_PopupOpen;
			miWindow.PopupOpen += miWindow_PopupOpen;

			// Simon: 注释以下两行
			//Localize();
			//Designer.Controls.Add( this );
		}
	}

}
