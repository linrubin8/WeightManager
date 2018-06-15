using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace LB.Controls.LBEditor
{
	/// <summary>
	/// Transforms text to and from base64 encoding using streams.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The built in System.Convert.ToBase64String and FromBase64String methods are prone
	/// to error with OutOfMemoryException when used with larger strings or byte arrays.
	/// </para>
	/// <para>
	/// This class remedies the problem by using classes from the System.Security.Cryptography
	/// namespace to do the byte conversion with streams and buffered output.
	/// </para>
	/// </remarks>
	public static class Base64
	{
		/// <summary>
		/// Converts a byte array to a base64 string one block at a time.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static string ToBase64( byte[] data )
		{
			StringBuilder builder = new StringBuilder();

			using( StringWriter writer = new StringWriter( builder ) )
			{
				using( ToBase64Transform transformation = new ToBase64Transform() )
				{
					// Transform the data in chunks the size of InputBlockSize.
					byte[] bufferedOutputBytes = new byte[transformation.OutputBlockSize];
					int i = 0;
					int inputBlockSize = transformation.InputBlockSize;

					while( data.Length - i > inputBlockSize )
					{
						transformation.TransformBlock( data, i, data.Length - i, bufferedOutputBytes, 0 );
						i += inputBlockSize;
						writer.Write( Encoding.UTF8.GetString( bufferedOutputBytes ) );
					}

					// Transform the final block of data.
					bufferedOutputBytes = transformation.TransformFinalBlock( data, i, data.Length - i );
					writer.Write( Encoding.UTF8.GetString( bufferedOutputBytes ) );

					// Free up any used resources.
					transformation.Clear();
				}

				writer.Close();
			}

			return builder.ToString();
		}

		/// <summary>
		/// Converts a base64 string to a byte array.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns></returns>
		public static byte[] FromBase64( string s )
		{
			byte[] bytes;
			
			using( var writer = new MemoryStream() )
			{
				byte[] bufferedOutputBytes;
				//char[] inputBytes = s.ToCharArray();
				byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes( s );

				using( FromBase64Transform transformation = new FromBase64Transform( FromBase64TransformMode.IgnoreWhiteSpaces ) )
				{
					bufferedOutputBytes = new byte[transformation.OutputBlockSize];

					// Transform the data in chunks the size of InputBlockSize.
					var i = 0;

					while( inputBytes.Length - i > 4 )
					{
						transformation.TransformBlock( inputBytes, i, 4, bufferedOutputBytes, 0 );
						i += 4;
						writer.Write( bufferedOutputBytes, 0, transformation.OutputBlockSize );
					}

					// Transform the final block of data.
					bufferedOutputBytes = transformation.TransformFinalBlock( inputBytes, i, inputBytes.Length - i );
					writer.Write( bufferedOutputBytes, 0, bufferedOutputBytes.Length );

					// Free up any used resources.
					transformation.Clear();
				}

				writer.Position = 0;
				bytes = writer.GetBuffer();

				writer.Close();
			}

			return bytes;
		}
	}
}