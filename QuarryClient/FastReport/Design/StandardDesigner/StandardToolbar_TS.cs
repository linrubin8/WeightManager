using System;
using System.Collections.Generic;
using System.Text;
using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;

namespace FastReport.Design.StandardDesigner
{
	internal partial class StandardToolbar
	{
		public ButtonItem btnUpload;
		public ButtonItem btnUpdateFields;

		public override void Localize()
		{
			MyRes res = Localize_inner();

			// 按钮标题在 en.xml， zh-CHS.xml 搜索出来修改
			SetItemText( btnUpload, res.Get( "Upload" ) );
			SetItemText( btnUpdateFields, res.Get( "UpdateFields" ) );
		}

		public StandardToolbar( Designer designer )
			: base( designer )
		{
			StandardToolbar_inner( designer );

			// 按钮图片在 buttons.png 中
			btnUpload = CreateButton( "btnStdUpload", Res.GetImage( 239 ), Designer.cmdUpload.Invoke );
			btnUpdateFields = CreateButton( "btnStdUpdateFields", Res.GetImage( 240 ), Designer.cmdUpdateFields.Invoke );

			Items.Clear();
			Items.AddRange( new BaseItem[] {
				btnNew, btnOpen, btnSave, btnSaveAll, btnUpdateFields, btnUpload, btnPreview,
				btnNewPage, btnNewDialog, btnDeletePage, btnPageSetup,
				btnCut, btnCopy, btnPaste, btnFormatPainter,
				btnUndo, btnRedo, 
				btnGroup, btnUngroup, CustomizeItem } );

			Localize();
		}
	}
}
