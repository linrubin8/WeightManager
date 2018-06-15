using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LB.Controls.LBEditor
{
	public interface ITSEditor : ICreatePromptPageControl
	{
		string Name
		{
			get;
			set;
		}

		bool CanBeEmpty
		{
			get;
			set;
		}

		bool Checked
		{
			get;
			set;
		}

		object Value4DB
		{
			get;
			set;
		}

		bool InitByMethod
		{
			get;
		}

		bool ReadOnly
		{
			get;
			set;
		}

		bool TextBoxReadOnly
		{
			get;
			set;
		}

		bool Visible
		{
			get;
			set;
		}

		bool Enabled
		{
			get;
			set;
		}

		bool Focused
		{
			get;
		}

		string Caption
		{
			get;
			set;
		}

		Color CaptionForeColor
		{
			get;
			set;
		}

		bool Multiline
		{
			get;
			set;
		}

		string CaptionValidation
		{
			get;
			set;
		}

		string Text
		{
			get;
			set;
		}

		int SelectedIndex
		{
			get;
			set;
		}

		char PasswordChar
		{
			get;
			set;
		}

		CharacterCasing CharacterCasing
		{
			get;
			set;
		}

		void InitSelectedValue( object objSelectedID, object objSelectedCode, object objSelectedText );
		bool Validation( out string strErrorMsg );
		bool ValidationCode( out string strErrorMsg );
		void SetErrorMessage( string strErrorMsg );
		bool Focus();
		int GetMinWidth();
		void FirePromptReturn4NCodeOrList();
		void FirePromptClick();
		void FireValueChangedByEdit();

		event EventHandler SelectedIndexChanged;
		event EventHandler CheckedChanged;
		event EventHandler TextChanged;
		event KeyEventHandler KeyDown;
		event KeyPressEventHandler KeyPress;
		event EventHandler LostFocus;
		event PromptClickEventHandler PromptClick;
		event EventHandler ReadOnlyChanged;
		event EventHandler VisibleChanged;
		event EventHandler ValueChangedByEdit;
	}
}
