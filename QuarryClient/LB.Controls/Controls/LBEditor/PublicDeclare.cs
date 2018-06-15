using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using LB.Controls.Args;

namespace LB.Controls.LBEditor
{
	// TSUniformEditor 控件的类型
	public enum enTSUniformEditorType
	{
		ComboBox = 0,
		TextBox = 1,
		CheckBox = 2
	}

	// TSEditor 值的类型，校验需要此信息
	public enum enTSTextBoxValueType
	{
		String = 0,
		DateTime = 1,
		Number = 2
	}

	// 边框样式
	[Editor( "System.Windows.Forms.Design.BorderSidesEditor, System.Design, Version=2.0.0.0", typeof( UITypeEditor ) )]
	[Flags]
	public enum enBorderSides
	{
		None = 0,
		Left = 1,
		Top = 2,
		Right = 4,
		Bottom = 8,
		All = 15
	}

	public enum enPromptButtonType
	{
		None = 0,
		Prompt = 1,				// 弹出窗口
		Popup = 2,				// 下拉式小窗口
		PromptStep = 4,			// 弹出窗口，支持逐级提示
		Combo = 5,				// combo 选项可以不存在
		ComboStep = 6,			// combo 选项必须存在
		PopupAllowPrompt = 8,	// 主要为下拉式，双击时也支持弹出窗口
		GridCellButton = 9		// 单元格显示为按钮 
	}

	public enum enPromptButtonClickType
	{
		Prompt = 1,
		Popup = 2
	}

	public enum enTSTextBoxMemberType
	{
		ID = 1,
		Code = 2,
		Text = 3
	}

	public enum enTSTextBoxMemberConfigType
	{
		Default = 0,
		ID = 1,
		Code = 2,
		Text = 3
	}

	// 翻页类型
	public enum enTSPagingType
	{
		First = -1,
		Previous = -2,
		Next = -3,
		Last = -4,
		PreviousMore = -5,
		NextMore = -6,
		Normal = -9
	}

	public delegate void TSDealErrorEventHandler( object sender, TSDealErrorEventArgs e );

	public delegate void TSEditorInputPromptGetDataEventHandler( object sender, TSEditorInputPromptGetDataEventArgs e );

	public delegate void TSEditorSetValueEventHandler( object sender, TSEditorSetValueEventArgs e );

	public delegate void PromptClickEventHandler( object sender, PromptClickEventArgs e );

	public delegate void PromptReturnEventHandler( object sender, PromptReturnArgs e );

	public delegate void PromptPageCreatedEventHandler( object sender, PromptPageCreatedArgs e );

	public delegate void TSPagingEventHandler( object sender, TSPagingEventArgs e );

	public delegate void TSParseErrorEventHandler( object sender, TSParseErrorEventArgs e );

    //TODO TSDataGridView 注释
 //   public delegate void CellPromptClickEventHandler( object sender, CellPromptClickEventArgs e );

	//public delegate void CellPromptReturnEventHandler( object sender, CellPromptReturnArgs e );

	//public delegate void CellValidateEventHandler( object sender, CellValidateEventArgs e );

	public delegate void TSDataGridViewAlternatingEventHandler( object sender, TSDataGridViewAlternatingEventArgs e );

	public delegate void ImageBoxClickedEventHandler( object sender, ImageBoxClickEventArgs e );
}
