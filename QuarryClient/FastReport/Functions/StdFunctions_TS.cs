using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FastReport.Utils;
using FastReport.Design;
using System.Drawing;

namespace FastReport.Functions
{
	public static partial class StdFunctions
	{/// <summary>
		/// Retrieves a substring from the original string, starting at a specified character position.
		/// </summary>
		/// <param name="s">The original string.</param>
		/// <param name="startIndex">The starting character position of a substring.</param>
		/// <returns>A new string.</returns>
		public static string Substring( string s, int startIndex )
		{
			if( s == null || s.Length <= startIndex )
			{
				return "";
			}
			else
			{
				return s.Substring( startIndex );
			}
		}

		/// <summary>
		/// Retrieves a substring from the original string, starting at a specified character position, 
		/// with a specified length.
		/// </summary>
		/// <param name="s">The original string.</param>
		/// <param name="startIndex">The starting character position of a substring.</param>
		/// <param name="length">The number of characters in the substring.</param>
		/// <returns>A new string.</returns>
		public static string Substring( string s, int startIndex, int length )
		{
			if( s == null || s.Length <= startIndex )
			{
				return "";
			}
			else
			{
				if( startIndex + length > s.Length )
				{
					length = s.Length - startIndex;
				}
				return s.Substring( startIndex, length );
			}
		}

		/// <summary>
		/// 返回字符串指定查找字符串之后的字符串
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <param name="startString">起始查找字符串</param>
		/// <returns>返回字符串指定查找字符串之后的字符串</returns>
		public static string Substring( string s, string startString )
		{
			if( s == null )
			{
				return "";
			}
			else if( string.IsNullOrEmpty( startString ) )
			{
				return "";
			}
			else if( s.IndexOf( startString ) < 0 )
			{
				return "";
			}
			else
			{
				return s.Substring( s.IndexOf( startString ) + startString.Length );
			}
		}

		/// <summary>
		/// 返回原字符串指定查找起始及结束字符串之间的字符串
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <param name="startString">查找的起始字符串</param>
		/// <param name="endString">查找的结束字符串</param>
		/// <returns>返回原字符串指定查找起始及结束字符串之间的字符串</returns>
		public static string Substring( string s, string startString, string endString )
		{
			if( s == null )
			{
				return "";
			}
			else if( string.IsNullOrEmpty( startString ) )
			{
				return "";
			}
			else if( string.IsNullOrEmpty( endString ) )
			{
				return "";
			}
			else if( s.IndexOf( startString ) < 0 || s.IndexOf( endString ) < 0 )
			{
				return "";
			}
			else
			{
				int start = s.IndexOf( startString );
				int end = s.IndexOf( endString );
				if( end - start - startString.Length > 0 )
				{
					return s.Substring( start + startString.Length, end - start - startString.Length );
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>
		/// 返回字符串指定查找字符串之前的字符串
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <param name="startString">查找字符串</param>
		/// <returns>返回字符串指定查找字符串之前的字符串</returns>
		public static string GetLeftStr( string s, string endString )
		{
			if( s == null )
			{
				return "";
			}
			else if( string.IsNullOrEmpty( endString ) )
			{
				return "";
			}
			else if( s.IndexOf( endString ) < 0 )
			{
				return "";
			}
			else
			{
				return s.Substring( 0, s.IndexOf( endString ) );
			}
		}

		/// <summary>
		/// 返回字符串指定起始位置与结束位置之间的字符串。
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <param name="startIndex">起始位置</param>
		/// <param name="endIndex">结束位置</param>
		/// <returns>字符串指定起始位置与结束位置之间的字符串</returns>
		public static string Mid( string s, int startIndex, int endIndex )
		{
			if( s == null || s.Length <= startIndex )
			{
				return "";
			}
			else
			{
				if( endIndex + 1 > s.Length )
				{
					endIndex = s.Length - 1;
				}
				int length = endIndex - startIndex + 1;
				return s.Substring( startIndex, length );
			}
		}

		public static int InStr( string source, string findString )
		{
			if( source == null || findString == null )
			{
				return -1;
			}
			else
			{
				return source.IndexOf( findString );
			}
		}

		/// <summary>
		/// 将金额由数值转换成中文大写。如果金额超过 仟亿 位，则不作转换。
		/// </summary>
		/// <param name="Amount">金额数值</param>
		/// <param name="FullMode">是否为全部转换模式。true 表示将所有位转换； false 表示将重复的零省略。</param>
		/// <returns>中文大写</returns>
		public static string ToAmoutChinese( decimal Amount, bool FullMode )
		{
			string input = "";
			if( Amount == 0 )
			{
				return "零元整";
			}
			Amount = decimal.Round( Amount, 2 );
			long num = Convert.ToInt64( (decimal)( Amount * 100M ) );
			if( num.ToString().Length > 14 )
			{
				// 金额过大，不作转换
				return num.ToString();
			}
			string[] textArray = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
			string[] textArray2 = new string[] { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };
			char[] chArray = num.ToString().ToCharArray();
			int length = chArray.Length;
			for( int i = length; i > 0; i-- )
			{
				input = textArray[Convert.ToInt32( chArray[i - 1].ToString() )] + textArray2[length - i] + input;
			}
			if( !FullMode )
			{
				input = input.Replace( "零亿", "亿" ).Replace( "零万", "万" ).Replace( "零元", "元" );
				input = new Regex( "(零.)+" ).Replace( input, "零" );
				input = new Regex( "零+" ).Replace( input, "零" ).Replace( "零亿", "亿" ).Replace( "零万", "万" ).Replace( "零元", "元" ).Replace( "亿万", "亿" );
				input = new Regex( "零$" ).Replace( input, "整" );
			}
			return input;
		}

		/// <summary>
		/// 检查指定的值是否为 null, 0 或 空字符串
		/// </summary>
		/// <param name="value">需要检查的值</param>
		/// <returns><b>true</b> 如果值为 null, 0 或 空字符串</returns>
		public static bool IsNullEmptyOrZero( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				return true;
			}

			string strValue = value as string;
			if( strValue != null )
			{
				strValue = strValue.Trim();
				if( strValue == "" )
				{
					return true;
				}
				else
				{
					return false;
				}
			}


			try
			{
				decimal decValue = Convert.ToDecimal( value );
				if( decValue == 0 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}

			try
			{
				long lngValue = Convert.ToInt64( value );
				if( lngValue == 0 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}

			return false;
		}

		/// <summary>
		/// 如果 value 不是 null, 0 或空字符串，那么返回 prefix + value；否则，返回空
		/// </summary>
		/// <param name="value">值</param>
		/// <param name="prefix">前缀</param>
		/// <returns>如果 value 不是 null, 0 或空字符串，那么返回 prefix + value；否则，返回空</returns>
		public static string AddPrefixIfHasValue( object value, string prefix )
		{
			return AddPrefixIfHasValue( value, null, prefix, "" );
		}

		/// <summary>
		/// 如果 value 不是 null, 0 或空字符串，那么返回 prefix + value + suffix；否则，返回空
		/// </summary>
		/// <param name="value">值</param>
		/// <param name="prefix">前缀</param>
		/// <param name="suffix">后缀</param>
		/// <returns>如果 value 不是 null, 0 或空字符串，那么返回 prefix + value + suffix；否则，返回空</returns>
		public static string AddPrefixIfHasValue( object value, string prefix, string suffix )
		{
			return AddPrefixIfHasValue( value, null, prefix, suffix );
		}

		/// <summary>
		/// 如果 value 不是 null, 0 或空字符串，那么返回 prefix + value + suffix；否则，返回空
		/// </summary>
		/// <param name="value">值</param>
		/// <param name="valueFormat">对值格式化，仅当值为数值或日期类型时有效</param>
		/// <param name="prefix">前缀</param>
		/// <param name="suffix">后缀</param>
		/// <returns>如果 value 不是 null, 0 或空字符串，那么返回 prefix + value + suffix；否则，返回空</returns>
		public static string AddPrefixIfHasValue( object value, string valueFormat, string prefix, string suffix )
		{
			if( IsNullEmptyOrZero( value ) )
			{
				return "";
			}

			if( value is bool )
			{
				return prefix + ( (bool)value ? "是" : "否" ) + suffix;
			}

			string strValue = value as string;
			if( strValue != null )
			{
				return prefix + strValue.Trim() + suffix;
			}

			try
			{
				decimal decValue = Convert.ToDecimal( value );
				if( string.IsNullOrEmpty( valueFormat ) )
				{
					return prefix + decValue.ToString() + suffix;
				}
				else
				{
					return prefix + decValue.ToString( valueFormat ) + suffix;
				}
			}
			catch
			{
			}

			try
			{
				long lngValue = Convert.ToInt64( value );
				if( string.IsNullOrEmpty( valueFormat ) )
				{
					return prefix + lngValue.ToString() + suffix;
				}
				else
				{
					return prefix + lngValue.ToString( valueFormat ) + suffix;
				}
			}
			catch
			{
			}

			try
			{
				DateTime dtValue = Convert.ToDateTime( value );
				if( string.IsNullOrEmpty( valueFormat ) )
				{
					return prefix + dtValue.ToString( "yyyy-MM-dd" ) + suffix;
				}
				else
				{
					return prefix + dtValue.ToString( valueFormat ) + suffix;
				}
			}
			catch
			{
			}

			return prefix + value.ToString().TrimEnd() + suffix;
		}

		/// <summary>
		/// 根据编码类型及名称读取图片
		/// </summary>
		/// <param name="NCodeName">编码名称</param>
		/// <param name="NCodeClassID">编码类型</param>
		/// <returns>图片</returns>
		public static Image GetNCodeImage( string NCodeName, int NCodeClassID )
		{
			GetNCodeImageEventArgs args = new GetNCodeImageEventArgs( NCodeName, NCodeClassID );
			Config.DesignerSettings.OnGetNCodeImageEvent( null, args );
			return args.NCodeImage;
		}

		/// <summary>
		/// 根据物料流水号读取图片
		/// </summary>
		/// <param name="ItemNCodeID">物料流水号</param>
		/// <returns>图片</returns>
		public static Image GetItemImage( long ItemNCodeID )
		{
			GetItemImageEventArgs args = new GetItemImageEventArgs( ItemNCodeID );
			Config.DesignerSettings.OnGetItemImageEvent( null, args );
			return args.NCodeImage;
		}

		/// <summary>
		/// 根据销售单行号读取订单图片
		/// </summary>
		/// <param name="NCodeName">销售单行号</param>
		/// <returns>图片</returns>
		public static Image GetInvoiceImage( long InvoiceDetailID )
		{
			GetInvoiceImageEventArgs args = new GetInvoiceImageEventArgs( InvoiceDetailID );
			Config.DesignerSettings.OnGetInvoiceImageEvent( null, args );
			return args.InvoiceImage;
		}

		public static Image ToImage( byte[] bytes )
		{
			return ImageHelper.Load( bytes );
		}

		internal static void Register()
		{
			Register_inner();

			Type str = typeof( StdFunctions );
			RegisteredObjects.AddFunction( str.GetMethod( "InStr" ), "Text" );
			RegisteredObjects.AddFunction( str.GetMethod( "Mid" ), "Text" );

			RegisteredObjects.AddFunction( str.GetMethod( "Substring", new Type[] { typeof( string ), typeof( string ) } ), "Text,Substring" );
			RegisteredObjects.AddFunction( str.GetMethod( "Substring", new Type[] { typeof( string ), typeof( string ), typeof( string ) } ), "Text,Substring" );

			Type myConv = typeof( StdFunctions );
			RegisteredObjects.AddFunction( myConv.GetMethod( "ToAmoutChinese", new Type[] { typeof( decimal ), typeof( bool ) } ), "Conversion" );
			RegisteredObjects.AddFunction( myConv.GetMethod( "ToImage", new Type[] { typeof( byte[] ) } ), "Conversion" );

			Type misc = typeof( StdFunctions );
			RegisteredObjects.AddFunction( misc.GetMethod( "IsNullEmptyOrZero" ), "ProgramFlow" );
			RegisteredObjects.AddFunction( misc.GetMethod( "AddPrefixIfHasValue", new Type[] { typeof( object ), typeof( string ) } ), "ProgramFlow" );
			RegisteredObjects.AddFunction( misc.GetMethod( "AddPrefixIfHasValue", new Type[] { typeof( object ), typeof( string ), typeof( string ) } ), "ProgramFlow" );
			RegisteredObjects.AddFunction( misc.GetMethod( "AddPrefixIfHasValue", new Type[] { typeof( object ), typeof( string ), typeof( string ), typeof( string ) } ), "ProgramFlow" );

			#region Data

			RegisteredObjects.AddFunctionCategory( "Data", "Data" );
			Type typData = typeof( StdFunctions );
			RegisteredObjects.AddFunction( typData.GetMethod( "GetNCodeImage", new Type[] { typeof( string ), typeof( int ) } ), "Data" );
			RegisteredObjects.AddFunction( typData.GetMethod( "GetItemImage", new Type[] { typeof( long ) } ), "Data" );
			RegisteredObjects.AddFunction( typData.GetMethod( "GetInvoiceImage", new Type[] { typeof( long ) } ), "Data" );

			#endregion Data
		}
	}
}
