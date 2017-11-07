using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Code
{
	internal partial class VbCodeHelper
	{
		protected override string GetTypeConvert( Type type )
		{
			if( type.IsGenericType )
			{
				string result = type.Name;
				result = result.Substring( 0, result.IndexOf( '`' ) );
				result += "(Of ";

				foreach( Type elementType in type.GetGenericArguments() )
				{
					result += GetTypeDeclaration( elementType ) + ",";
				}

				result = result.Substring( 0, result.Length - 1 ) + ")";
				return result;
			}
			else
			{
				string typeName = type.Name;
				typeName = typeName.Replace( "[]", "()" );
				return typeName;
			}
		}

		public override string BeginCalcExpression()
		{
			return "    Private Function CalcExpression(ByVal expression As String, ByVal Value as Global.FastReport.Variant) As Object\r\n      " +
				"    Dim sourceText As String\r\n      ";	// 原写法，没有这一行
		}

		public override string AddExpression( string expr, string value )
		{
			expr = expr.Replace( "\"", "\"\"" );

			// 原写法没有这两行
			// 是为了让它支持表达式在报表设计器里可以换行
			expr = expr.Replace( Environment.NewLine, "\" & vbNewLine & _\r\n    \"" );
			value = value.Replace( Environment.NewLine, " _" + Environment.NewLine );

			return "sourceText = \"" + expr + "\"\r\n         If expression = sourceText Then\r\n        Return " + value + "\r\n      End If\r\n      ";
		}
	}
}
