using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class CellPromptClickEventArgs
	{
		private int miColumnIndex = -1;
		private int miRowIndex = -1;
		private ITSPromptForm mPromptForm = null;
		private enPromptButtonClickType mPromptButtonClickType = enPromptButtonClickType.Popup;
		private bool mbHandled = false;
		private bool mbShowPopup = true;
		private ICreatePromptPageCell miEditor = null;

		public CellPromptClickEventArgs(
			int iColumnIndex, int iRowIndex,
			enPromptButtonClickType promptButtonClickType, bool bShowPopup, ICreatePromptPageCell editor )
		{
			miColumnIndex = iColumnIndex;
			miRowIndex = iRowIndex;
			mPromptButtonClickType = promptButtonClickType;
			mbShowPopup = bShowPopup;
			miEditor = editor;
		}

		public int ColumnIndex
		{
			get
			{
				return miColumnIndex;
			}
		}

		public int RowIndex
		{
			get
			{
				return miRowIndex;
			}
		}

		public ITSPromptForm PromptForm
		{
			get
			{
				return mPromptForm;
			}
			set
			{
				if( mPromptForm != value )
				{
					mPromptForm = value;

					mPromptForm.PromptCellAddress = new System.Drawing.Point( this.ColumnIndex, this.RowIndex );
					CellPromptPageCreatedArgs args = new CellPromptPageCreatedArgs( value );
					miEditor.OnPromptPageCreated( args );
				}
			}
		}

		public enPromptButtonClickType PromptButtonClickType
		{
			get
			{
				return mPromptButtonClickType;
			}
		}

		public bool ShowPopup
		{
			get
			{
				return mbShowPopup;
			}
		}

		public bool Handled
		{
			get
			{
				return mbHandled;
			}
			set
			{
				mbHandled = value;
			}
		}
	}
}
