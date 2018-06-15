using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;

namespace LB.Controls.LBEditor
{
	partial class CommonFuntion
	{
		#region -- 校验相关 --

		internal static bool Validation(
			out string strErrorMsg, string Text,
			bool bTextBoxReadOnly, string CaptionValidation,
			enTSTextBoxValueType TSValueType, bool CanBeEmpty, int MaxLength,
			string MinValue, string MaxValue, bool IsCode, bool IsNChar, bool IsPercent,
			string FormatString )
		{
			strErrorMsg = "";
			string strTextTrim = Text.Trim();

			// 校验是否允许为空
			if( !CanBeEmpty && Text.Trim() == "" )
			{
				strErrorMsg += string.Format( "<{0}>不可为空！", CaptionValidation );
				return false;
			}

			// 如果是 TextBoxReadOnly，则只需校验是否允许为空
			if( bTextBoxReadOnly )
			{
				return true;
			}

			// 如果允许为空（上面已校验过），并且值为空，则不管值类型为什么，都通过
			if( strTextTrim == "" )
			{
				return true;
			}

			// 校验长度限制
			if( !MaxLengthValidator( ref strErrorMsg, TSValueType, Text, CaptionValidation, IsNChar, MaxLength ) )
			{
				return false;
			}

			// 校验编码输入字符
			if( IsCode && !CodeValidator( ref strErrorMsg, Text, CaptionValidation ) )
			{
				return false;
			}

			// 校验数值
			if( !NumberValidator( ref strErrorMsg, Text,
				CaptionValidation, TSValueType, MaxValue, MinValue,
				IsPercent, FormatString ) )
			{
				return false;
			}

			// 校验日期
			if( !DateTimeValidator( ref strErrorMsg, Text,
				CaptionValidation, TSValueType, MaxValue, MinValue, FormatString ) )
			{
				return false;
			}

			return true;
		}

		private static bool MaxLengthValidator(
			ref string strErrorMessage,
			enTSTextBoxValueType TSValueType,
			string Text, string CaptionValidation, bool IsNChar, int MaxLength )
		{
			if( TSValueType != enTSTextBoxValueType.String || MaxLength < 1 )
			{
				return true;
			}

			string strTextTrim = Text.Trim();

			bool bIsValid = true;
			if( IsNChar )
			{
				if( strTextTrim.Length > MaxLength )
				{
					bIsValid = false;
					strErrorMessage +=
						string.Format( "<{0}>长度只允许{1}个字符！", CaptionValidation, MaxLength );
				}
			}
			else
			{
				if( StringSBCSLength( strTextTrim ) > MaxLength )
				{
					bIsValid = false;
					strErrorMessage +=
						string.Format( "<{0}>长度只允许{1}个单字节字符！", CaptionValidation, MaxLength );
				}
			}

			return bIsValid;
		}

		private static int StringSBCSLength( string StringToCount )
		{
			int iLen = 0;
			char[] chToCount = StringToCount.ToCharArray();
			for( int i = 0, j = chToCount.Length; i < j; i++ )
			{
				if( chToCount[i] > byte.MaxValue )
				{
					iLen += 2;
				}
				else
				{
					iLen++;
				}
			}
			return iLen;
		}

		// 全角空格为12288，半角空格为32
		// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
		private static bool HasDBC( string input )
		{
			bool bHasDBC = false;
			char[] c = input.ToCharArray();
			for( int i = 0; i < c.Length; i++ )
			{
				if( c[i] == 12288 ||
					( c[i] > 65280 && c[i] < 65375 ) )
				{
					bHasDBC = true;
					break;
				}
			}
			return bHasDBC;
		}

		private static bool CodeValidator( ref string strErrorMessage,
			string SelectedCode, string CaptionValidation )
		{
			string strSelectedCode = SelectedCode;
			if( strSelectedCode == "" )
			{
				return true;
			}

			System.Text.RegularExpressions.Regex reg = new Regex( "^[A-Za-z0-9_.][A-Za-z0-9_.]*$" );
			if( !reg.IsMatch( strSelectedCode ) )
			{
				strErrorMessage +=
					string.Format( "编码字段<{0}>只允许包含英文字母、数字或下划线！", CaptionValidation );
				return false;
			}

			if( HasDBC( strSelectedCode ) )
			{
				strErrorMessage +=
					string.Format( "编码字段不允许包含全角字符！", CaptionValidation );
				return false;
			}

			return true;
		}

		private static bool DateTimeValidator(
			ref string strErrorMessage,
			string Text, string CaptionValidation, enTSTextBoxValueType TSValueType,
			string MaxValue, string MinValue, string FormatString )
		{
			if( TSValueType != enTSTextBoxValueType.DateTime )
			{
				return true;
			}

			string strTextTrim = Text.Trim();

			// 校验是否为有效日期
			DateTime timeValue;
			try
			{
				timeValue = Convert.ToDateTime( strTextTrim );
			}
			catch
			{
				strErrorMessage += string.Format( "<{0}>必须为有效日期！", CaptionValidation );
				return false;
			}

			// 解释最大值
			DateTime timeMax = DateTime.MaxValue;
			bool bMax = false;
			try
			{
				timeMax = Convert.ToDateTime( MaxValue );
				bMax = true;
			}
			catch
			{
			}

			// 解释最小值
			DateTime timeMin = DateTime.MinValue;
			bool bMin = false;
			try
			{
				timeMin = Convert.ToDateTime( MinValue );
				bMin = true;
			}
			catch
			{
			}

			// 校验范围
			if( bMax && bMin )
			{
				if( timeValue > timeMax ||
					timeValue < timeMin )
				{
					strErrorMessage += string.Format(
						"<{0}>的取值范围是（{1}～{2}）！",
						CaptionValidation,
						timeMin.ToString( FormatString ),
						timeMax.ToString( FormatString ) );

					return false;
				}
			}
			else if( bMax )
			{
				if( timeValue > timeMax )
				{
					strErrorMessage += string.Format(
						"<{0}>的最大值为（{1}）！",
						CaptionValidation,
						timeMax.ToString( FormatString ) );

					return false;
				}
			}
			else if( bMin )
			{
				if( timeValue > timeMin )
				{
					strErrorMessage += string.Format(
						"<{0}>的最小值为（{1}）！",
						CaptionValidation,
						timeMin.ToString( FormatString ) );

					return false;
				}
			}

			return true;
		}

		private static bool NumberValidator(
			ref string strErrorMessage,
			string Text, string CaptionValidation, enTSTextBoxValueType TSValueType,
			string MaxValue, string MinValue, bool IsPercent, string FormatString )
		{
			if( TSValueType != enTSTextBoxValueType.Number )
			{
				return true;
			}

			string strTextTrim = Text.Trim();

			// 解释最大值
			decimal decMax = 0;
			bool bMax = false;
			try
			{
				decMax = Convert.ToDecimal( MaxValue );
				bMax = true;
			}
			catch
			{
			}

			// 解释最小值
			decimal decMin = 0;
			bool bMin = false;
			try
			{
				decMin = Convert.ToDecimal( MinValue );
				bMin = true;
			}
			catch
			{
			}

			// 校验值为在效数值
			decimal decValue = 0;
			bool bValue = true;
			try
			{
				bool bRealIsPercent = false;
				if( strTextTrim.EndsWith( "%" ) )
				{
					bRealIsPercent = true;
					strTextTrim = strTextTrim.Substring( 0, strTextTrim.Length - 1 );
				}
				decValue = Convert.ToDecimal( strTextTrim );
				if( bRealIsPercent )
				{
					decValue = decValue / 100;
				}
			}
			catch
			{
				bValue = false;
			}

			// 不是数值或超出范围
			bool bPass = bValue;
			if( !bValue ||
				( bMin && decValue < decMin ) ||
				( bMax && decValue > decMax ) )
			{
				if( bMax && bMin )
				{
					strErrorMessage += string.Format(
						"<{0}>必须为有效数值。{1}取值范围：{2}～{3}",
						CaptionValidation, Environment.NewLine, decMin.ToString( FormatString ), decMax.ToString( FormatString ) );
				}
				else if( bMax )
				{
					strErrorMessage += string.Format(
						"<{0}>必须为小于或等于（{1}）的有效数值。",
						CaptionValidation, decMax.ToString( FormatString ) );
				}
				else if( bMin )
				{
					strErrorMessage += string.Format(
						"<{0}>必须为大于或等于（{1}）的有效数值。",
						CaptionValidation, decMin.ToString( FormatString ) );
				}
				else
				{
					strErrorMessage += string.Format( "<{0}>必须为有效数值。", CaptionValidation );
				}

				bPass = false;
			}

			return bPass;
		}

		#endregion -- 校验相关 --

		#region -- 格式化 --

		internal static string Format4Editor(
			string strToFormatText, string strFormatString,
			enTSTextBoxValueType valueType, enPromptButtonType promptType, bool textBoxReadOnly )
		{
			string strTrim = strToFormatText.Trim();
			if( strFormatString == string.Empty ||
				strTrim == string.Empty ||
				promptType == enPromptButtonType.PromptStep ||
				promptType == enPromptButtonType.ComboStep ||
				promptType == enPromptButtonType.Popup ||
				promptType == enPromptButtonType.PopupAllowPrompt ||
				textBoxReadOnly )
			{
				return strToFormatText;
			}

			switch( valueType )
			{
				case enTSTextBoxValueType.DateTime:
					try
					{
						DateTime timeValue = Convert.ToDateTime( strTrim );
						strTrim = timeValue.ToString( strFormatString );
					}
					catch
					{
					}
					break;

				// TODO: 格式化过后，读取值的问题未解决
				case enTSTextBoxValueType.Number:
					try
					{
						decimal decValue = Convert.ToDecimal( strTrim );
						strTrim = decValue.ToString( strFormatString );
						strTrim = strTrim.Replace( ",", "" );
					}
					catch
					{
					}
					break;
			}

			return strTrim;
		}

		#endregion -- 格式化 --
	}
}
