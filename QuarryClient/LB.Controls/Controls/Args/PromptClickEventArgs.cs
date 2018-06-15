using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public class PromptClickEventArgs
	{
		private ITSPromptForm mPromptForm = null;
		private enPromptButtonClickType mPromptButtonClickType = enPromptButtonClickType.Popup;
		private bool mbHandled = false;
		private bool mbShowPopup = true;
		private ICreatePromptPageControl miEditor = null;

		public PromptClickEventArgs( enPromptButtonClickType promptButtonClickType, bool bShowPopup, ICreatePromptPageControl editor )
		{
			mPromptButtonClickType = promptButtonClickType;
			mbShowPopup = bShowPopup;
			miEditor = editor;
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

					PromptPageCreatedArgs args = new PromptPageCreatedArgs( value );
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
