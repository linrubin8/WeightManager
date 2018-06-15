using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Win.Controls
{
	public delegate void PromptConstDataEventHandler( object sender, PromptConstDataArgs e );

	public class PromptConstDataArgs
	{
		private string mstrFieldName = "";
		private string mstrCustomerCriteria = "";

		public PromptConstDataArgs( string strFieldName, string strCustomerCriteria )
		{
			mstrFieldName = strFieldName;
			mstrCustomerCriteria = strCustomerCriteria;
		}

		public string FieldName
		{
			get
			{
				return mstrFieldName;
			}
		}

		public string CustomerCriteria
		{
			get
			{
				return mstrCustomerCriteria;
			}
			set
			{
				mstrCustomerCriteria = value;
			}
		}
	}
}
