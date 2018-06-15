using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LB.Controls.LBEditor;

namespace LB.Controls.Args
{
	public class TSEditorSetValueEventArgs
	{
		private enTSTextBoxMemberType mMember;
		private object mobjToSetValue = null;
		private DataRow mdrValueData = null;
		private bool mbHandled = false;
		private int miSelectedIndex = -1;

		public TSEditorSetValueEventArgs( enTSTextBoxMemberType member, object value, int selectedIndex )
		{
			mMember = member;
			mobjToSetValue = value;
			miSelectedIndex = selectedIndex;
		}

		public enTSTextBoxMemberType SetByMember
		{
			get
			{
				return mMember;
			}
		}

		public object ToSetValue
		{
			get
			{
				return mobjToSetValue;
			}
		}

		public DataRow ValueDataRow
		{
			get
			{
				return mdrValueData;
			}
			set
			{
				mdrValueData = value;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return miSelectedIndex;
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
