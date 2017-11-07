using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Data
{
	public partial class Column
	{
		/// <inheritdoc/>
		public override string ToString()
		{
			return "FastReport.Data.Column{Name=" + this.Name + ",Type=" + this.DataType.ToString() + "}";
		}
	}
}
