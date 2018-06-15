using LB.Controls.LBEditor;
using System;
using System.Collections.Generic;
using System.Text;
namespace LB.Controls.Args
{
	public delegate void PromptParmsNeedEventHandler( object sender, PromptParmsNeedArgs e );

	public class PromptParmsNeedArgs
	{
		private int miBizObjID;
		private int miPromptPageType;
		private StringDict mPageParms;

		public PromptParmsNeedArgs( int iPromptPageType, int iBizObjID, StringDict dictParms )
		{
			miBizObjID = iBizObjID;
			miPromptPageType = iPromptPageType;
			mPageParms = dictParms;

			if( mPageParms == null )
			{
				mPageParms = new StringDict();
			}
		}

		public int PromptPageType
		{
			get
			{
				return miPromptPageType;
			}
		}

		public int BizObjID
		{
			get
			{
				return miBizObjID;
			}
		}

		public StringDict PageParms
		{
			get
			{
				return mPageParms;
			}
		}
	}
}
