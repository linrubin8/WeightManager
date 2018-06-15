using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Controls.Args
{
	public class TSEditorInputPromptGetDataEventArgs : System.EventArgs
	{
		private string mstrInputText = "";
		private DataView mdvPromptData = null;
		private bool mbHandled = false;
		private bool mbGetDataByValue = false;
		private object objValue = null;
		private bool mbHasError = false;
		private string mstrErrorMsg;

		public TSEditorInputPromptGetDataEventArgs( string strInputText )
		{
			mstrInputText = strInputText;
		}

		public TSEditorInputPromptGetDataEventArgs( bool getDataByValue, object value )
		{
			mbGetDataByValue = getDataByValue;
			objValue = value;
		}

		public string InputText
		{
			get
			{
				return mstrInputText;
			}
		}

		public bool GetDataByValue
		{
			get
			{
				return mbGetDataByValue;
			}
		}

		public object Value
		{
			get
			{
				return objValue;
			}
		}

		public DataView PromptData
		{
			get
			{
				return mdvPromptData;
			}
			set
			{
				mdvPromptData = value;
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

		public bool HasError
		{
			get
			{
				return mbHasError;
			}
		}

		public string ErrorMsg
		{
			get
			{
				return mstrErrorMsg;
			}
		}

		public void SetError( bool hasError, string errorMsg )
		{
			mbHasError = hasError;
			mstrErrorMsg = errorMsg;
		}
	}
}
