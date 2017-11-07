using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;

namespace FastReport.Code
{
	internal partial class CsCodeHelper
	{

		protected override string GetTypeConvert( Type type )
		{
			if( type.IsGenericType )
			{
				return "";
			}
			else
			{
				if( type == typeof( string ) || type == typeof( DBNull ) )
				{
					return "ToString";
				}
				else if( type == typeof( byte ) )
				{
					return "ToByte";
				}
				else if( type == typeof( char ) )
				{
					return "ToChar";
				}
				else if( type == typeof( DateTime ) )
				{
					return "ToDateTime";
				}
				else if( type == typeof( decimal ) )
				{
					return "ToDecimal";
				}
				else if( type == typeof( double ) )
				{
					return "ToDouble";
				}
				else if( type == typeof( short ) )
				{
					return "ToInt16";
				}
				else if( type == typeof( int ) )
				{
					return "ToInt32";
				}
				else if( type == typeof( long ) )
				{
					return "ToInt64";
				}
				else if( type == typeof( sbyte ) )
				{
					return "ToSByte";
				}
				else if( type == typeof( Single ) )
				{
					return "ToSingle";
				}
				else if( type == typeof( ushort ) )
				{
					return "ToUInt16";
				}
				else if( type == typeof( uint ) )
				{
					return "ToUInt32";
				}
				else if( type == typeof( ulong ) )
				{
					return "ToUInt64";
				}

				return "";
			}
		}

		public override string AddExpression( string expr, string value )
		{
			// 原写法
			//expr = expr.Replace( "\\", "\\\\" );
			//expr = expr.Replace( "\"", "\\\"" );

			// 在字符串前加 @ 的写法
			//expr = expr.Replace( "\\", "\\\\" );
			expr = expr.Replace( "\"", "\"\"" );

			return "if (expression == @\"" + expr + "\")\r\n        return " + value + ";\r\n      ";
		}

		public override string ReplaceColumnName( string name, Type type )
		{
			string convert = GetTypeConvert( type );
			if( convert != "" )
			{
				string result = "(Convert." + convert + "(Report.GetColumnValue(\"" + name + "\")))";
				return result;
			}
			else
			{
				string typeName = GetTypeDeclaration( type );
				string result = "((" + typeName + ")Report.GetColumnValue(\"" + name + "\"";
				result += "))";
				return result;
			}
		}

		public override string ReplaceParameterName( Parameter parameter )
		{
			string convert = GetTypeConvert( parameter.DataType );
			if( convert != "" )
			{
				string result = "(Convert." + convert + "(Report.GetParameterValue(\"" + parameter.FullName + "\")))";
				return result;
			}
			else
			{
				string typeName = GetTypeDeclaration( parameter.DataType );
				return "((" + typeName + ")Report.GetParameterValue(\"" + parameter.FullName + "\"))";
			}
		}

		public override string ReplaceVariableName( Parameter parameter )
		{
			string convert = GetTypeConvert( parameter.DataType );
			if( convert != "" )
			{
				string result = "(Convert." + convert + "(Report.GetParameterValue(\"" + parameter.FullName + "\")))";
				return result;
			}
			else
			{
				string typeName = GetTypeDeclaration( parameter.DataType );
				return "((" + typeName + ")Report.GetVariableValue(\"" + parameter.FullName + "\"))";
			}
		}
	}
}
