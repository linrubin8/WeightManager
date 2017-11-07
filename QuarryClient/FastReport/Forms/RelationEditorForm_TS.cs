using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastReport.Data;

namespace FastReport.Forms
{
	partial class RelationEditorForm
	{
		private string[] GetColumns( DataSourceBase data )
		{
			string[] result = GetColumns_org( data );
			List<string> lstResutl = new List<string>();
			lstResutl.AddRange( result );
			lstResutl.Sort();
			string[] resultFinal = new string[result.Length];
			lstResutl.CopyTo( resultFinal );
			return resultFinal;
		}
	}
}
