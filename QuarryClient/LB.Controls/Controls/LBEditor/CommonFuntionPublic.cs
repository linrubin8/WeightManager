using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using LB.Controls.Args;

namespace LB.Controls.LBEditor
{
	public partial class CommonFuntion
	{
		public static bool TSEditorChangeStyleByFocus = true;
		public static bool TSDataGridAllowOrderColumns = true;
		public static bool TSDataGridAllowSortColumns = true;
		public static bool TSDataGridAllowExport = true;
		public static System.Windows.Forms.Shortcut TSPromptShortcut = System.Windows.Forms.Shortcut.F7;

		public static int TSInputStepPromptLength = 0;	// 编码输入提示的长度。即输入了多少位才可以有提示窗口。 -1 表示不启用, 0 表示不限制。

		#region -- 公用的错误处理 --

		public static event TSDealErrorEventHandler DealError;

		internal static void OnDealError( object sender, Exception ex )
		{
			string strMsg = ex.Message + ex.StackTrace;

			TSDealErrorEventArgs args = new TSDealErrorEventArgs( ex );
			if( DealError != null )
			{
				DealError( sender, args );
			}
			else
			{
				HandleDealErrorInternal( sender, args );
			}
		}

		private static void HandleDealErrorInternal( object sender, TSDealErrorEventArgs e )
		{
			if( e.Exception.Message != "" )
			{
				using( TSErrorShower frm = new TSErrorShower( e.Exception ) )
				{
					frm.ShowDialog();
				}
			}
		}

		public static event TSDealErrorEventHandler DevError;

		internal static void OnDevError( object sender, Exception ex )
		{
			TSDealErrorEventArgs args = new TSDealErrorEventArgs( ex );
			if( DevError != null )
			{
				DevError( sender, args );
			}
		}

		#endregion -- 公用的错误处理 --

		#region -- Form 的 Show 及 ShowDialog 事件 --

		public static event EventHandler FormShowEvent;
		public static event EventHandler FormShowDialogEvent;

		internal static void OnFormShowEvent( object sender, EventArgs e )
		{
			if( FormShowEvent != null )
			{
				FormShowEvent( sender, e );
			}
		}

		internal static void OnFormShowDialogEvent( object sender, EventArgs e )
		{
			if( FormShowDialogEvent != null )
			{
				FormShowDialogEvent( sender, e );
			}
		}

		#endregion -- Form 的 Show 及 ShowDialog 事件 --

		#region -- 对 Editor 赋值 --

		public static void AssignEditorValue(
			ITSEditor iEditor, DataRow drRecord, object objValue,
			string TSInitIDColumn, string TSInitCodeColumn, string TSInitTextColumn )
		{
			if( iEditor.InitByMethod && drRecord != null )
			{
				DataColumnCollection columns = drRecord.Table.Columns;

				// 如果指定了 Init 字段，但数据行中却不含有该字段，就不使用方法赋值
				bool bRealByMethod = true;
				if( string.IsNullOrEmpty( TSInitIDColumn ) &&
					string.IsNullOrEmpty( TSInitCodeColumn ) &&
					string.IsNullOrEmpty( TSInitTextColumn ) )
				{
					bRealByMethod = false;
				}
				if( bRealByMethod && !string.IsNullOrEmpty( TSInitIDColumn ) && !columns.Contains( TSInitIDColumn ) )
				{
					bRealByMethod = false;
				}
				if( bRealByMethod && !string.IsNullOrEmpty( TSInitCodeColumn ) && !columns.Contains( TSInitCodeColumn ) )
				{
					bRealByMethod = false;
				}
				if( bRealByMethod && !string.IsNullOrEmpty( TSInitTextColumn ) && !columns.Contains( TSInitTextColumn ) )
				{
					bRealByMethod = false;
				}

				if( !bRealByMethod )
				{
					iEditor.Value4DB = objValue;

					// TODO
					//System.Diagnostics.Debug.Assert(
					//    iEditor.Visible && ( objValue == null || iEditor.Value4DB.ToString() == objValue.ToString() ),
					//    "The " + iEditor.ToString() + " InitByMethod, but does not config TSInitColumn." );
				}
				else
				{
					object objID = null;
					object objCode = null;
					object objText = null;
					if( TSInitIDColumn != "" && columns.Contains( TSInitIDColumn ) )
					{
						objID = drRecord[TSInitIDColumn];
					}
					if( TSInitCodeColumn != "" && columns.Contains( TSInitCodeColumn ) )
					{
						objCode = drRecord[TSInitCodeColumn];
					}
					if( TSInitTextColumn != "" && columns.Contains( TSInitTextColumn ) )
					{
						objText = drRecord[TSInitTextColumn];
					}

					iEditor.InitSelectedValue( objID, objCode, objText );
				}
			}
			else
			{
				iEditor.Value4DB = objValue;
			}
		}

		#endregion -- 对 Editor 赋值 --

		/// <summary>
		/// 计算字符串转为单字节字符的长度
		/// </summary>
		/// <param name="StringToCount">字符串</param>
		/// <returns>单字节长度</returns>
		public static int StringSingleByteLen( string StringToCount )
		{
			return CommonFuntion.StringSBCSLength( StringToCount );
		}

		#region -- Image Value4DB ---

		/// <summary>
		/// 从数据库保存的图片字符串，在指定的目录中保存，文件的扩展名自动确定
		/// </summary>
		/// <param name="strValue4DB">图片数据字符串</param>
		/// <param name="strPath">指定目录</param>
		/// <param name="strFile">文件名，不含扩展名</param>
		public static void SaveFileFromValue4DB( string strValue4DB, string strPath, string strFile )
		{
			Image image = GetImageFromValue4DB( strValue4DB );

			try
			{
				if( image != null )
				{
					using( Image imgTemp = new Bitmap( image ) )
					{
						if( image.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Bmp ) )
						{
							strFile += ".bmp";
						}
						else if( image.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Jpeg ) )
						{
							strFile += ".jpg";
						}
						else if( image.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Gif ) )
						{
							strFile += ".gif";
						}
						else if( image.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Tiff ) )
						{
							strFile += ".tif";
						}
						else if( image.RawFormat.Equals( System.Drawing.Imaging.ImageFormat.Png ) )
						{
							strFile += ".png";
						}

						imgTemp.Save( strPath + ( strPath.EndsWith( @"\" ) ? "" : @"\" ) + strFile, image.RawFormat );
					}
				}
			}
			finally
			{
				if( image != null )
				{
					image.Dispose();
				}
			}
		}

		/// <summary>
		/// 将压缩过的字节数组转换成图片
		/// </summary>
		/// <param name="bytZipped">压缩过的字节数组</param>
		/// <returns>图片</returns>
		public static Image GetImageFromZippedBytes( byte[] bytZipped )
		{
			if( bytZipped == null || bytZipped.Length == 0 )
			{
				return null;
			}

			MemoryStream msUnZip = new MemoryStream();
			using( MemoryStream msZipped = new MemoryStream( bytZipped ) )
			{
				using( DeflateStream unzipStream = new DeflateStream( msZipped, CompressionMode.Decompress ) )
				{
					using( BinaryReader reader = new BinaryReader( unzipStream ) )
					{
						byte[] byeReuslt = new byte[1024];
						int iRead = 0;
						while( ( iRead = reader.Read( byeReuslt, 0, byeReuslt.Length ) ) > 0 )
						{
							msUnZip.Write( byeReuslt, 0, iRead );
						}
					}
				}
			}
			msUnZip.Position = 0;
			return Image.FromStream( msUnZip );
		}

		/// <summary>
		/// 将图片转换成压缩过的字节数组
		/// </summary>
		/// <param name="image">图片</param>
		/// <returns>字节数组</returns>
		public static byte[] GetZippedBytesFromImage( Image image )
		{
			if( image == null )
			{
				return null;
			}

			byte[] bytNew = null;

			using( MemoryStream ms = new MemoryStream() )
			{
				using( DeflateStream zipStream = new DeflateStream( ms, CompressionMode.Compress ) )
				{
					byte[] bytImage = GetImageBytes( image );

					zipStream.Write( bytImage, 0, bytImage.Length );
					zipStream.Flush();
				}

				// 取出压缩后的结果
				bytNew = ms.ToArray();

				// 如果用 GetBuffer()，则后面会有好多 0；而使用 ToArray() 则没有这种情况
				//byte[] byt2 = ms.ToArray();
				//// 压缩后的结果在后段差不多有一半为 0，将其截断，只留一个作为结尾的标志
				//int iCount = 0;
				//for( int i = byt.Length - 1; i >= 0; i-- )
				//{
				//    if( iCount == 0 && i > 0 && ( (int)byt[i - 1] ) != 0 )
				//    {
				//        iCount = i;
				//        bytNew = new byte[iCount + 1];
				//    }

				//    if( iCount > 0 )
				//    {
				//        bytNew[i] = byt[i];
				//    }
				//}
			}

			return bytNew;
		}

		/// <summary>
		/// 从数据库保存的图片数据转换成图片
		/// </summary>
		/// <param name="value">图片数据字符串</param>
		/// <returns>图片</returns>
		public static Image GetImageFromValue4DB( string value )
		{
			if( string.IsNullOrEmpty( value ) )
			{
				return null;
			}

			byte[] buffers = null;
			Image img = null;
			if( !string.IsNullOrEmpty( value ) )
			{
				string strCode = CommonFuntion.UnZip( value );
				img = ImageFromBase64( strCode, out buffers );
			}

			return img;
		}

		/// <summary>
		/// 从数据库保存的图片数据转换成图片字节数组
		/// </summary>
		/// <param name="value">图片数据字符串</param>
		/// <returns>图片</returns>
		public static byte[] GetImageBytesFromValue4DB( string value )
		{
			if( string.IsNullOrEmpty( value ) )
			{
				return null;
			}

			byte[] buffers = null;
			Image img = null;
			if( !string.IsNullOrEmpty( value ) )
			{
				string strCode = CommonFuntion.UnZip( value );
				img = ImageFromBase64( strCode, out buffers );
			}

			return buffers;
			//return GetImageBytes( img );
		}

		/// <summary>
		/// 从图片转换成 Base64String
		/// </summary>
		/// <param name="value">图片</param>
		/// <returns>图片的 Base64String</returns>
		public static string GetImageBase64String( Image value )
		{
			if( value == null )
			{
				return null;
			}

			return Base64.ToBase64( GetImageBytes( value ) );
		}

		private static Image ImageFromBase64( string strBase64, out byte[] buffers )
		{
			buffers = null;
			Image img = null;

			if( !string.IsNullOrEmpty( strBase64 ) )
			{
				buffers = Base64.FromBase64( strBase64 );
				img = (new ImageConverter()).ConvertFrom( buffers ) as Image;

				//System.IO.MemoryStream stream = new System.IO.MemoryStream( buffers );
				//img = Image.FromStream( stream );
			}

			return img;
		}

		/// <summary>
		/// 从一个指定的文件，转换成数据库保存格式的字符串
		/// </summary>
		/// <param name="strFile">文件全名，含扩展名及路径</param>
		/// <returns>数据库保存格式的字符串</returns>
		public static string GetValue4DBFromFile( string strFullFileName )
		{
			string strCode = "";

			if( !File.Exists( strFullFileName ) )
			{
				throw new FileNotFoundException();
			}

			using( Image image = Image.FromFile( strFullFileName ) )
			{
				if( image != null )
				{
					strCode = GetImageBase64( image );
					strCode = CommonFuntion.Zip( strCode );
				}
			}

			return strCode;
		}

		/// <summary>
		/// 从一个指定的图片，转换成数据库保存格式的字符串
		/// </summary>
		/// <param name="image">图片</param>
		/// <returns>数据库保存格式的字符串</returns>
		public static string GetValue4DBFromImage( Image image )
		{
			string strCode = "";
			if( image != null )
			{
				strCode = GetImageBase64( image );
				strCode = CommonFuntion.Zip( strCode );
			}

			return strCode;
		}

		private static string GetImageBase64( Image img )
		{
			byte[] byts = GetImageBytes( img );
			return Base64.ToBase64( byts );
		}

		//private static byte[] GetImageStreamBytes( Image img )
		//{
		//    byte[] b = null;

		//    using( System.IO.MemoryStream stream = new System.IO.MemoryStream() )
		//    {
		//        // 先复制一份图片，否则：如果图片原本是从 Stream 得来的，一旦调用 Save 就会出错
		//        using( Bitmap bmp = new Bitmap( img.Width, img.Height ) )
		//        {
		//            using( Graphics g = Graphics.FromImage( bmp ) )
		//            {
		//                g.DrawImage( img, 0, 0 );
		//            }

		//            bmp.Save( stream, System.Drawing.Imaging.ImageFormat.Gif );
		//            b = stream.GetBuffer();
		//        }
		//    }

		//    return b;
		//}

		/// <summary>
		/// 获取图片的字节数组，图片会转成 Gif 格式以缩小字节数目
		/// </summary>
		/// <param name="img">图片</param>
		/// <returns>字节数组</returns>
		public static byte[] GetImageBytes( Image img )
		{
			if( img == null )
			{
				return null;
			}

			if( img.Height <=0 || img.Width <= 0 )
			{
				return null;
			}

			byte[] bytTemp;
			try
			{
				bytTemp = (byte[])( new ImageConverter() ).ConvertTo( img, typeof( byte[] ) );
			}
			catch
			{
				Bitmap bmp = new Bitmap( img.Width, img.Height );
				using( Graphics g = Graphics.FromImage( bmp ) )
				{
					g.DrawImage( img, 0, 0 );
				}
				bytTemp = (byte[])( new ImageConverter() ).ConvertTo( bmp, typeof( byte[] ) );
			}
			return bytTemp;

			// 以下方法，是另存为 gif
			// 问题：有时图片会变全黑
			// 如果图片太大，可能会报错：参数无效。
			 // 所以要注意限制制保存的图片，要限制文件大小以及图片尺寸
			//byte[] b = null;
			//using( System.IO.MemoryStream stream = new System.IO.MemoryStream() )
			//{
			//    using( Bitmap bmp = new Bitmap( img ) )
			//    {
			//        bmp.Save( stream, System.Drawing.Imaging.ImageFormat.Gif );
			//        b = stream.ToArray();
			//    }
			//}
			//return b;

			// 以下方法不另存，但是是以 Bitmap 的格式取回 byte[]，数组会大很多
			//Bitmap bmp = new Bitmap( img );
			//Rectangle rect = new Rectangle( 0, 0, bmp.Width, bmp.Height );
			//BitmapData bmpData =
			//   bmp.LockBits( rect, ImageLockMode.ReadWrite, bmp.PixelFormat );

			//int bytes = Math.Abs( bmpData.Stride ) * bmp.Height;
			//byte[] rgbValues = new byte[bytes];

			//// Copy the RGB values into the array.
			//Marshal.Copy( bmpData.Scan0, rgbValues, 0, bytes );
			//return rgbValues;
		}

		#endregion -- Image Value4DB ---

		#region -- Zip, UnZip ---

		public static string Zip( string strToZip )
		{
			string strResult = "";

			using( MemoryStream ms = new MemoryStream() )
			{
				using( DeflateStream zipStream = new DeflateStream( ms, CompressionMode.Compress ) )
				{
					byte[] bytSource = System.Text.Encoding.UTF8.GetBytes( strToZip );
					zipStream.Write( bytSource, 0, bytSource.Length );
					zipStream.Flush();
				}

				// 取出压缩后的结果
				byte[] byt = ms.ToArray();

				// 压缩后的结果在后段差不多有一半为 0，将其截断，只留一个作为结尾的标志
				//byte[] bytNew = null;
				//int iCount = 0;
				//for( int i = byt.Length - 1; i >= 0; i-- )
				//{
				//    if( iCount == 0 && i > 0 && ( (int)byt[i - 1] ) != 0 )
				//    {
				//        iCount = i;
				//        bytNew = new byte[iCount + 1];
				//    }

				//    if( iCount > 0 )
				//    {
				//        bytNew[i] = byt[i];
				//    }
				//}

				strResult = Base64.ToBase64( byt );
			}

			return strResult;
		}

		public static string UnZip( string strZipped )
		{
			string strUnZip = "";
			byte[] bytZiped = Base64.FromBase64( strZipped );

			using( MemoryStream msZipped = new MemoryStream( bytZiped ) )
			{
				using( DeflateStream unzipStream = new DeflateStream( msZipped, CompressionMode.Decompress ) )
				{
					using( StreamReader reader = new StreamReader( unzipStream ) )
					{
						strUnZip = reader.ReadToEnd();
					}
				}
			}

			return strUnZip;
		}

		public static byte[] ZipFile( string strFileName )
		{
			byte[] bytNew = null;

			using( MemoryStream ms = new MemoryStream() )
			{
				using( DeflateStream zipStream = new DeflateStream( ms, CompressionMode.Compress ) )
				{
					using( System.IO.FileStream fileStream = new FileStream( strFileName, FileMode.Open, FileAccess.Read ) )
					{
						using( System.IO.BinaryReader reader = new BinaryReader( fileStream ) )
						{
							byte[] bytSource = new byte[1024];
							int iRead = 0;
							while( ( iRead = reader.Read( bytSource, 0, bytSource.Length ) ) > 0 )
							{
								zipStream.Write( bytSource, 0, iRead );
							}
						}
					}
					zipStream.Flush();
				}

				// 取出压缩后的结果
				bytNew = ms.ToArray();

				// 压缩后的结果在后段差不多有一半为 0，将其截断，只留一个作为结尾的标志
				//int iCount = 0;
				//for( int i = byt.Length - 1; i >= 0; i-- )
				//{
				//    if( iCount == 0 && i > 0 && ( (int)byt[i - 1] ) != 0 )
				//    {
				//        iCount = i;
				//        bytNew = new byte[iCount + 1];
				//    }

				//    if( iCount > 0 )
				//    {
				//        bytNew[i] = byt[i];
				//    }
				//}
			}

			return bytNew;
		}

		public static void UnZipToFile( byte[] bytZipped, string strFileName )
		{
			if( File.Exists( strFileName ) )
			{
				File.SetAttributes( strFileName, FileAttributes.Normal );
			}
			using( MemoryStream msZipped = new MemoryStream( bytZipped ) )
			{
				using( DeflateStream unzipStream = new DeflateStream( msZipped, CompressionMode.Decompress ) )
				{
					using( BinaryReader reader = new BinaryReader( unzipStream ) )
					{
						using( FileStream fileStream = new FileStream( strFileName, FileMode.Create ) )
						{
							byte[] byeReuslt = new byte[1024];
							int iRead = 0;
							while( ( iRead = reader.Read( byeReuslt, 0, byeReuslt.Length ) ) > 0 )
							{
								fileStream.Write( byeReuslt, 0, iRead );
							}
						}
					}
				}
			}
		}

		#endregion -- Zip, UnZip ---

		public static bool CompareValueObject( object objX, object objY )
		{
			if( ( objX == null || objX == DBNull.Value ) && ( objY == null || objY == DBNull.Value ) )
			{
				return true;
			}
			else if( ( objX == null || objX == DBNull.Value ) && ( objY != null && objY != DBNull.Value ) )
			{
				return false;
			}
			else if( ( objX != null && objX != DBNull.Value ) && ( objY == null || objY == DBNull.Value ) )
			{
				return false;
			}
			else if( ( objX.GetType() == typeof( long ) ||
						objX.GetType() == typeof( int ) ||
						objX.GetType() == typeof( short ) ||
						objX.GetType() == typeof( byte ) ) &&
				( objY.GetType() == typeof( long ) ||
						objY.GetType() == typeof( int ) ||
						objY.GetType() == typeof( short ) ||
						objY.GetType() == typeof( byte ) ) )
			{
				return ( ( Convert.ToInt64( objX ) ) == ( Convert.ToInt64( objY ) ) );
			}
			else if( ( objX.GetType() == typeof( decimal ) ||
						objX.GetType() == typeof( float ) ||
						objX.GetType() == typeof( double ) ) &&
				( objY.GetType() == typeof( decimal ) ||
						objY.GetType() == typeof( float ) ||
						objY.GetType() == typeof( double ) ) )
			{
				return ( ( Convert.ToDecimal( objX ) ) == ( Convert.ToDecimal( objY ) ) );
			}
			else if( objX.GetType() == typeof( DateTime ) && objY.GetType() == typeof( DateTime ) )
			{
				return ( ( Convert.ToDateTime( objX ) ) == ( Convert.ToDateTime( objY ) ) );
			}
			else
			{
				return ( objX.ToString().Trim().Equals( objY.ToString().TrimEnd(),
					StringComparison.CurrentCultureIgnoreCase ) );
			}
		}

		#region -- 导出 Excel --

		public static void DataTable2Excel( DataTable dtToConvert, string fileName )
		{
			#region -- 循环导出 --

			XSSFWorkbook workbook = new XSSFWorkbook();

			int iRowIndex = 0;
			int iSheetIndex = 1;
			string strSheetName = iSheetIndex.ToString().PadLeft( 2, '0' ) + "_" + DateTime.Now.ToString( "yyMMdd_HHmm_sss" );
			ISheet sheet = workbook.CreateSheet( strSheetName );
			bool bCreateHeader = true;
			int iRowCount = dtToConvert.Rows.Count;

			for( int x = 0; x < iRowCount; x++ )
			{
				if( bCreateHeader )
				{
					#region -- 添加列名行 --

					iRowIndex = 0;
					IRow rowHeader = sheet.CreateRow( iRowIndex );
					for( int i = 0, j = dtToConvert.Columns.Count; i < j; i++ )
					{
						DataColumn col = dtToConvert.Columns[i];

						ICell cell = rowHeader.CreateCell( i );
						cell.SetCellValue( FormatCellText( col.ColumnName ) );
					}

					#endregion -- 添加列名行 --

					bCreateHeader = false;
				}

				#region -- 添加数据行 --

				iRowIndex++;
				IRow row = sheet.CreateRow( iRowIndex );

				DataRow dgvRow = dtToConvert.Rows[x];
				for( int m = 0, n = dtToConvert.Columns.Count; m < n; m++ )
				{
					ICell cell = row.CreateCell( m );

					#region -- 单元格 --

					object objValue = dtToConvert.Rows[iRowIndex-1][m];
					Type type = dtToConvert.Columns[m].DataType;
					if( type == typeof( long ) || type == typeof( int ) || type == typeof( decimal ) || type == typeof( float ) || type == typeof( byte ) )
					{
						if( objValue != DBNull.Value && objValue != null )
						{
							cell.SetCellValue( Convert.ToDouble( objValue ) );
						}
					}
					else if( objValue != null )
					{
						string strValue = objValue.ToString().TrimEnd();
						cell.SetCellValue( FormatCellText( strValue ) );
					}

					#endregion -- 单元格 --
				}

				#endregion -- 添加数据行 --

				// Excel 分 Sheet，以免超出一个工作表的最大行数
				if( x > 0 && ( x + 1 ) % 65000 == 0 )
				{
					iSheetIndex++;
					strSheetName = iSheetIndex.ToString().PadLeft( 2, '0' ) + "_" + DateTime.Now.ToString( "yyMMdd_HHmm_sss" );
					sheet = workbook.CreateSheet( strSheetName );
					bCreateHeader = true;
				}
			}

			#endregion -- 循环导出 --

			// 保存文件
			using( FileStream file = new FileStream( fileName, FileMode.OpenOrCreate ) )
			{
				workbook.Write( file );
				file.Close();
			}
		}

		private static string FormatCellText( string strOrgText )
		{
			if( strOrgText.Contains( "(%)" ) )
			{
				strOrgText = strOrgText.Remove( strOrgText.IndexOf( "(%)" ), 3 );
			}
			return strOrgText;
		}

		#endregion -- 导出 Excel --
	}
}
