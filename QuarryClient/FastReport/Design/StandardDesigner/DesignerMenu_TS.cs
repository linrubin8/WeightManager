using System;
using System.Collections.Generic;
using System.Text;
using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;

namespace FastReport.Design.StandardDesigner
{
    public partial class DesignerMenu
    {
        /// <summary>
        /// The "File|Upload" menu.
        /// </summary>
        public ButtonItem miFileUpload;

        /// <summary>
        /// The "File|UpdateFields" menu.
        /// </summary>
        public ButtonItem miUpdateFields;

        public void Localize()
        {
            MyRes res = Localize_inner();

            // 按钮标题在 en.xml， zh-CHS.xml 搜索出来修改
            miFileUpload.Text = res.Get( "File,Upload" );
            miUpdateFields.Text = res.Get( "File,UpdateFields" );
        } 

        public DesignerMenu( Designer designer )
            : base()
        {
            DesignerMenu_inner( designer );

            // 按钮图片在 buttons.png 中
            miFileUpload = CreateMenuItem( Res.GetImage( 239 ), Designer.cmdUpload.Invoke );
            miUpdateFields = CreateMenuItem( Res.GetImage( 240 ), Designer.cmdUpdateFields.Invoke );

            Items.Clear();

            // create menu structure
            Items.AddRange( new BaseItem[] {
                miFile, miEdit, miView, miInsert, miReport, miData, miWindow, miHelp } );
            miFile.SubItems.AddRange( new BaseItem[] {
                miFileNew, miFileOpen, miFileClose,
                miFileSave, miFileSaveAs, miFileSaveAll, miUpdateFields, miFileUpload,
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

            Localize();
            Designer.Controls.Add( this );
        }
    }
}
