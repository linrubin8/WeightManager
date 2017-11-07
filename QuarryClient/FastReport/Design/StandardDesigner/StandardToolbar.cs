using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
	internal partial class StandardToolbar : ToolbarBase
	{
		#region Fields
		public ButtonItem btnNew;
		public ButtonItem btnOpen;
		public ButtonItem btnSave;
		public ButtonItem btnSaveAll;
		public ButtonItem btnPreview;
		public ButtonItem btnNewPage;
		public ButtonItem btnNewDialog;
		public ButtonItem btnDeletePage;
		public ButtonItem btnPageSetup;
		public ButtonItem btnCut;
		public ButtonItem btnCopy;
		public ButtonItem btnPaste;
		public ButtonItem btnFormatPainter;
		public ButtonItem btnUndo;
		public ButtonItem btnRedo;
		public ButtonItem btnGroup;
		public ButtonItem btnUngroup;
		private UndoDropDown FUndoDropDown;
		private RedoDropDown FRedoDropDown;
		private Timer FClipboardTimer;
		private Timer FPreviewTimer;
		#endregion

		#region Private Methods
		private void UpdateControls()
		{
			btnNew.Enabled = Designer.cmdNew.Enabled;
			btnOpen.Enabled = Designer.cmdOpen.Enabled;
			btnSave.Enabled = Designer.cmdSave.Enabled;
			btnSaveAll.Enabled = Designer.cmdSaveAll.Enabled;
			btnPreview.Enabled = Designer.cmdPreview.Enabled;
			btnNewPage.Enabled = Designer.cmdNewPage.Enabled;
			btnNewDialog.Enabled = Designer.cmdNewDialog.Enabled;
			btnDeletePage.Enabled = Designer.cmdDeletePage.Enabled;
			btnPageSetup.Enabled = Designer.cmdPageSetup.Enabled;
			btnCut.Enabled = Designer.cmdCut.Enabled;
			btnCopy.Enabled = Designer.cmdCopy.Enabled;
			btnFormatPainter.Enabled = Designer.cmdFormatPainter.Enabled;
			btnFormatPainter.Checked = Designer.FormatPainter;
			btnUndo.Enabled = Designer.cmdUndo.Enabled;
			btnRedo.Enabled = Designer.cmdRedo.Enabled;
			btnGroup.Enabled = Designer.cmdGroup.Enabled;
			btnUngroup.Enabled = Designer.cmdUngroup.Enabled;
		}

		private void btnOpen_PopupOpen( object sender, PopupOpenEventArgs e )
		{
			btnOpen.SubItems.Clear();
			if( Designer.cmdRecentFiles.Enabled )
			{
				foreach( string s in Designer.RecentFiles )
				{
					ButtonItem item = new ButtonItem();
					item.Text = s;
					item.Click += recentItem_Click;
					btnOpen.SubItems.Insert( 0, item );
				}
			}
			if( btnOpen.SubItems.Count == 0 )
				btnOpen.SubItems.Add( new ButtonItem() );
		}

		private void recentItem_Click( object sender, EventArgs e )
		{
			Designer.cmdOpen.LoadFile( ( sender as ButtonItem ).Text );
		}

		private void FClipboardTimer_Tick( object sender, EventArgs e )
		{
			btnPaste.Enabled = Designer.cmdPaste.Enabled;
		}

		private void FPreviewTimer_Tick( object sender, EventArgs e )
		{
			FPreviewTimer.Stop();
			Designer.cmdPreview.Invoke( sender, e );
		}

		private void btnPreview_Click( object sender, EventArgs e )
		{
			FPreviewTimer.Start();
		}
		#endregion

		#region Protected Methods
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				FClipboardTimer.Dispose();
				FPreviewTimer.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Public Methods
		public override void SelectionChanged()
		{
			base.SelectionChanged();
			UpdateControls();
		}

		public override void UpdateContent()
		{
			base.UpdateContent();
			UpdateControls();
		}

		// Simon: Localize 改为 Localize_inner 
		public MyRes Localize_inner()
		{
			base.Localize();
			MyRes res = new MyRes( "Designer,Toolbar,Standard" );
			Text = res.Get( "" );

			SetItemText( btnNew, res.Get( "New" ) );
			SetItemText( btnOpen, res.Get( "Open" ) );
			SetItemText( btnSave, res.Get( "Save" ) );
			SetItemText( btnSaveAll, res.Get( "SaveAll" ) );
			SetItemText( btnPreview, res.Get( "Preview" ) );
			SetItemText( btnNewPage, res.Get( "NewPage" ) );
			SetItemText( btnNewDialog, res.Get( "NewDialog" ) );
			SetItemText( btnDeletePage, res.Get( "DeletePage" ) );
			SetItemText( btnPageSetup, res.Get( "PageSetup" ) );
			SetItemText( btnCut, res.Get( "Cut" ) );
			SetItemText( btnCopy, res.Get( "Copy" ) );
			SetItemText( btnPaste, res.Get( "Paste" ) );
			SetItemText( btnFormatPainter, res.Get( "FormatPainter" ) );
			SetItemText( btnUndo, res.Get( "Undo" ) );
			SetItemText( btnRedo, res.Get( "Redo" ) );
			SetItemText( btnGroup, res.Get( "Group" ) );
			SetItemText( btnUngroup, res.Get( "Ungroup" ) );

			// Simon: 增加 return res;
			return res;
		}
		#endregion

		// Simon: StandardToolbar 改为 StandardToolbar_inner，去掉 : base(designer) 
		public void StandardToolbar_inner( Designer designer )
		{
			Name = "StandardToolbar";

			btnNew = CreateButton( "btnStdNew", Res.GetImage( 4 ), Designer.cmdNew.Invoke );
			btnOpen = CreateButton( "btnStdOpen", Res.GetImage( 1 ), Designer.cmdOpen.Invoke );
			btnOpen.PopupOpen += btnOpen_PopupOpen;
			btnOpen.SubItems.Add( new ButtonItem() );
			btnSave = CreateButton( "btnStdSave", Res.GetImage( 2 ), Designer.cmdSave.Invoke );
			btnSaveAll = CreateButton( "btnStdSaveAll", Res.GetImage( 178 ), Designer.cmdSaveAll.Invoke );
			btnPreview = CreateButton( "btnStdPreview", Res.GetImage( 3 ), btnPreview_Click );
			btnNewPage = CreateButton( "btnStdNewPage", Res.GetImage( 10 ), Designer.cmdNewPage.Invoke );
			btnNewPage.BeginGroup = true;
			btnNewDialog = CreateButton( "btnStdNewDialog", Res.GetImage( 11 ), Designer.cmdNewDialog.Invoke );
			btnDeletePage = CreateButton( "btnStdDeletePage", Res.GetImage( 12 ), Designer.cmdDeletePage.Invoke );
			btnPageSetup = CreateButton( "btnStdPageSetup", Res.GetImage( 13 ), Designer.cmdPageSetup.Invoke );
			btnCut = CreateButton( "btnStdCut", Res.GetImage( 5 ), Designer.cmdCut.Invoke );
			btnCut.BeginGroup = true;
			btnCopy = CreateButton( "btnStdCopy", Res.GetImage( 6 ), Designer.cmdCopy.Invoke );
			btnPaste = CreateButton( "btnStdPaste", Res.GetImage( 7 ), Designer.cmdPaste.Invoke );
			btnFormatPainter = CreateButton( "btnStdFormatPainter", Res.GetImage( 18 ), Designer.cmdFormatPainter.Invoke );
			btnFormatPainter.AutoCheckOnClick = true;
			btnUndo = CreateButton( "btnStdUndo", Res.GetImage( 8 ), Designer.cmdUndo.Invoke );
			btnUndo.BeginGroup = true;
			btnRedo = CreateButton( "btnStdRedo", Res.GetImage( 9 ), Designer.cmdRedo.Invoke );
			btnGroup = CreateButton( "btnStdGroup", Res.GetImage( 17 ), Designer.cmdGroup.Invoke );
			btnGroup.BeginGroup = true;
			btnUngroup = CreateButton( "btnStdUngroup", Res.GetImage( 16 ), Designer.cmdUngroup.Invoke );
			FUndoDropDown = new UndoDropDown( Designer, btnUndo );
			FRedoDropDown = new RedoDropDown( Designer, btnRedo );

			// Simon: 注释以下原代码
			//	Items.AddRange( new BaseItem[] {
			//btnNew, btnOpen, btnSave, btnSaveAll, btnPreview,
			//btnNewPage, btnNewDialog, btnDeletePage, btnPageSetup,
			//btnCut, btnCopy, btnPaste, btnFormatPainter,
			//btnUndo, btnRedo, 
			//btnGroup, btnUngroup, CustomizeItem } );

			// Simon: 注释以下原代码
			//	Localize();

			FClipboardTimer = new Timer();
			FClipboardTimer.Interval = 500;
			FClipboardTimer.Tick += FClipboardTimer_Tick;
			FClipboardTimer.Start();

			FPreviewTimer = new Timer();
			FPreviewTimer.Interval = 20;
			FPreviewTimer.Tick += FPreviewTimer_Tick;
		}
	}

}
