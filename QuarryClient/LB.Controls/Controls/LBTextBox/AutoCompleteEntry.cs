using System;
using System.Collections;
using System.Data;

namespace LB.Controls.LBTextBox
{
	/// <summary>
	/// Summary description for AutoCompleteDictionaryEntry.
	/// </summary>
	[Serializable]
	public class AutoCompleteEntry : IAutoCompleteEntry
	{

		private string[] matchStrings;
		public string[] MatchStrings
		{
			get
			{
				if (this.matchStrings == null)
				{
					this.matchStrings = new string[] {this.DisplayName};
				}
				return this.matchStrings;
			}
		}

		private string displayName = string.Empty;
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

        private DataRowView _DataBindItem;
        public DataRowView DataBindItem
        {
            get
            {
                return _DataBindItem;
            }
        }

        public AutoCompleteEntry()
		{
		}

		public AutoCompleteEntry(string name, DataRowView drvSource, params string[] matchList)
		{
			this.displayName = name;
			this.matchStrings = matchList;
            this._DataBindItem = drvSource;

        }

		public override string ToString()
		{
			return this.DisplayName;
		}

	}
}
