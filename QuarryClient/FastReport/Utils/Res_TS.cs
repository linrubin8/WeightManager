using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace FastReport.Utils
{
	public static partial class Res
	{
		private static void LoadImages()
		{
			FImagesLoaded = true;
			FImages = new List<Bitmap>();

			using( Bitmap images = ResourceLoader.GetBitmap( "buttons.png" ) )
			{
				int x = 0;
				int y = 0;
				bool done = false;

				do
				{
					Bitmap oneImage = new Bitmap( 16, 16 );
					using( Graphics g = Graphics.FromImage( oneImage ) )
					{
						g.DrawImage( images, new Rectangle( 0, 0, 16, 16 ), new Rectangle( x, y, 16, 16 ), GraphicsUnit.Pixel );
					}
					FImages.Add( oneImage );

					x += 16;
					if( x >= images.Width )
					{
						x = 0;
						y += 16;
					}
					done = y > images.Height;
				}
				while( !done );
			}
		}

		internal static void LoadDefaultLocale()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if( currentCulture.Name.Equals( "zh-CN" ) ||
				currentCulture.Name.Equals( "zh-CHS" ) ||
				( currentCulture.Parent != null && currentCulture.Parent.Name.Equals( "zh-CHS" ) ) )
			{
				using( Stream stream = ResourceLoader.GetStream( "zh-CHS.xml" ) )
				{
					LoadLocale( stream );
				}
				return;
			}

			string strLocaleFolder = LocaleFolder;
			if( !Directory.Exists( strLocaleFolder ) )
				return;

			if( String.IsNullOrEmpty( DefaultLocaleName ) )
			{
				// locale is set to "Auto"
				string[] files = Directory.GetFiles( strLocaleFolder, "*.frl" );


				foreach( string file in files )
				{
					// find the CultureInfo for given file
					string localeName = Path.GetFileNameWithoutExtension( file );
					CultureInfo localeCulture = null;
					foreach( CultureInfo info in CultureInfo.GetCultures( CultureTypes.NeutralCultures ) )
					{
						if( String.Compare( info.EnglishName, localeName, true ) == 0 )
						{
							localeCulture = info;
							break;
						}
					}

					// if current culture equals to or is subculture of localeCulture, load the locale
					if( currentCulture.Equals( localeCulture ) ||
					  ( currentCulture.Parent != null && currentCulture.Parent.Equals( localeCulture ) ) )
					{
						LoadLocale( file );
						break;
					}
				}
			}
			else
			{
				// locale is set to specific name
				LoadLocale( LocaleFolder + DefaultLocaleName + ".frl" );
			}
		}
	}
}
