using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using FastReport.Utils;
using System.Drawing.Imaging;
using System.Windows.Forms;
#if PRINT_HOUSE
using System.IO.Compression;
#endif

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        #region AddPictureObject, AddPictureObjectRAW

        private void AddPictureObject( ReportComponentBase obj, bool drawBorder, int quality, StringBuilder sb_in )
        {
            if( ImagesOriginalResolution )
                if( AddPictureObjectRAW( obj as PictureObject, drawBorder, sb_in ) )
                    return;

            float width = obj.Width;// > paperWidth + obj.AbsLeft ? paperWidth + obj.AbsLeft : obj.Width;
            float height = obj.Height;// > paperHeight + obj.AbsTop ? paperHeight + obj.AbsTop : obj.Height;

            if( width < 0.5f || height < 0.5f )
                return;

            Border oldBorder = obj.Border.Clone();
            obj.Border.Lines = BorderLines.None;

            long imageIndex = -1;
            float printZoom = FPrintOptimized ? 4 : 1;

            int bitmapWidth = (int)Math.Round( width * printZoom );
            int bitmapHeight = (int)Math.Round( height * printZoom );

            // check for max bitmap object size
            {
                // 2GB (max .net object size) / 4 (Format32bppArgb is 4 bytes)
                // see http://stackoverflow.com/a/29175905/4667434
                const ulong maxPixels = 536870912;

                if( (ulong)bitmapWidth * (ulong)bitmapHeight >= maxPixels )
                {
                    bitmapWidth = (int)width;
                    bitmapHeight = (int)height;
                }

                if( (ulong)bitmapWidth * (ulong)bitmapHeight >= maxPixels )
                {
                    return;
                }
            }

            using( Bitmap image = new Bitmap( bitmapWidth, bitmapHeight, PixelFormat.Format32bppArgb ) )
            using( Graphics g = Graphics.FromImage( image ) )
            {
                g.TranslateTransform( -obj.AbsLeft * printZoom, -obj.AbsTop * printZoom );
                g.Clear( FTransparentImages ? Color.Transparent : Color.White );

                // if PDF/X-3 then render image with page
                // because PDF/X-3 doesn't support transparency
                if( PdfCompliance == PdfStandard.PdfX_3 &&
                    obj.Page != null &&
                    obj.Page is ReportPage )
                {
                    ReportPage page = obj.Page as ReportPage;
                    float leftMargin = (int)Math.Round( page.LeftMargin * Units.Millimeters * printZoom );
                    float topMargin = (int)Math.Round( page.TopMargin * Units.Millimeters * printZoom );
                    g.TranslateTransform( -leftMargin, -topMargin );

                    obj.Page.Draw( new FRPaintEventArgs( g, printZoom, printZoom, Report.GraphicCache ) );
                }
                else
                {
                    obj.Draw( new FRPaintEventArgs( g, printZoom, printZoom, Report.GraphicCache ) );
                }

                int[] rawBitmap = GetRawBitmap( image );
                string hash = CalculateHash( rawBitmap );
                imageIndex = GetImageIndexByHash( hash );

                if( imageIndex == -1 )
                {
                    using( MemoryStream imageStream = new MemoryStream() )
                    using( MemoryStream maskStream = GetMask( rawBitmap ) )
                    {
                        if( JpegCompression )
                        {
                            ExportUtils.SaveJpeg( image, imageStream, quality );
                            imageStream.Position = 0;
                            imageIndex = WriteImage_Jpeg( imageStream, maskStream, image.Width, image.Height );
                        }
                        else
                        {
                            // grayscale is set to false
                            // because it's already grayscaled
                            // if "Grayscale" option is set to true
                            WritePixelColors( rawBitmap, imageStream, false );
                            imageStream.Position = 0;
                            using( MemoryStream imageDeflateStream = new MemoryStream() )
                            {
                                ExportUtils.ZLibDeflate( imageStream, imageDeflateStream );
                                imageDeflateStream.Position = 0;
                                imageIndex = WriteImage_Deflate( imageDeflateStream, maskStream, image.Width, image.Height );
                            }
                        }
                    }

                    SetImageIndexByHash( hash, imageIndex );
                }
            }

            if( imageIndex == -1 )
                return;

            AddImageToList( imageIndex );

            StringBuilder sb = new StringBuilder( 256 );
            sb.AppendLine( "q" );

            if( obj is PictureObject )
                GetPDFFillTransparent( Color.FromArgb( (byte)( ( 1 - ( obj as PictureObject ).Transparency ) * 255f ), Color.Black ), sb );

            float bWidth = width == 0 ? 1 : width * PDF_DIVIDER;
            float bHeight = height == 0 ? 1 : height * PDF_DIVIDER;

            sb.Append( FloatToString( bWidth ) ).Append( " 0 0 " );
            sb.Append( FloatToString( bHeight ) ).Append( " " );
            sb.Append( FloatToString( GetLeft( obj.AbsLeft ) ) ).Append( " " );
            sb.Append( FloatToString( GetTop( obj.AbsTop + height ) ) );

            sb.AppendLine( " cm" );
            sb.Append( "/Im" ).Append( imageIndex.ToString() ).AppendLine( " Do" );
            sb.AppendLine( "Q" );
            sb_in.Append( sb );
            obj.Border = oldBorder;

            if( drawBorder )
                DrawPDFBorder( obj.Border, obj.AbsLeft, obj.AbsTop, width, height, sb_in );
        }

        private bool AddPictureObjectRAW( PictureObject obj, bool drawBorder, StringBuilder sb_in )
        {
            if( obj == null )
                return false;

            if( obj.Width < 0.5f || obj.Height < 0.5f )
                return false;

            obj.ForceLoadImage();

            if( obj.Image == null )
                return false;

            int rawWidth = obj.Image.Width;
            int rawHeight = obj.Image.Height;

            long imageIndex = -1;

            // convert to PixelFormat.Format32bppArgb
            using( Bitmap image = new Bitmap( obj.Image.Width, obj.Image.Height, PixelFormat.Format32bppArgb ) )
            using( Graphics g = Graphics.FromImage( image ) )
            {
                g.DrawImage( obj.Image, new Rectangle( 0, 0, obj.Image.Width, obj.Image.Height ) );

                int[] rawBitmap = GetRawBitmap( image );

                // for hash we also use Width, Height and SizeMode
                // because there are different masks for them
                string hash = CalculateHash( rawBitmap ) +
                    "|" + obj.SizeMode.ToString() +
                    "|" + obj.Width.ToString() +
                    "|" + obj.Height.ToString();

                imageIndex = GetImageIndexByHash( hash );

                if( imageIndex == -1 )
                {
                    using( MemoryStream colorsRawStream = new MemoryStream() )
                    {
                        bool hasMask = false;
                        byte[] mask = null;

#if PRINT_HOUSE
                        if (ColorSpace == PdfColorSpace.CMYK)
                            using (Stream rawCMYKA = FindRawCMYKA(obj))
                                if (rawCMYKA != null && rawCMYKA.Length > 0)
                                {
                                    if (rawCMYKA.Length != rawWidth * rawHeight * 5)
                                        throw new Exception(string.Format("Incorrect CMYKA data length (length={0}, width={1}, height={2})", rawCMYKA.Length, rawWidth, rawHeight));

                                    mask = new byte[rawWidth * rawHeight];

                                    const int cmykaSamples = 5;
                                    for (int i = 0, mask_i = 0; i < rawCMYKA.Length; i += cmykaSamples, mask_i++)
                                    {
                                        colorsRawStream.WriteByte((byte)rawCMYKA.ReadByte()); // C
                                        colorsRawStream.WriteByte((byte)rawCMYKA.ReadByte()); // M
                                        colorsRawStream.WriteByte((byte)rawCMYKA.ReadByte()); // Y
                                        colorsRawStream.WriteByte((byte)rawCMYKA.ReadByte()); // K
                                        mask[mask_i] = (byte)rawCMYKA.ReadByte(); // A
                                        if (!hasMask && mask[mask_i] != 0xff)
                                            hasMask = true;
                                    }
                                }
#endif


                        if( mask == null )
                        {
                            mask = new byte[rawWidth * rawHeight];

                            for( int i = 0, mask_i = 0; i < rawBitmap.Length; i++, mask_i++ )
                            {
                                mask[mask_i] = (byte)( ( (UInt32)rawBitmap[i] ) >> 24 );
                                if( !hasMask && mask[mask_i] != 0xff )
                                    hasMask = true;
                            }

                            WritePixelColors( rawBitmap, colorsRawStream, obj.Grayscale );
                        }

                        // emulate PictureBoxSizeMode.Normal behavior
                        if( obj.SizeMode == PictureBoxSizeMode.Normal )
                        {
                            for( int x = 0; x < rawWidth; x++ )
                                for( int y = 0; y < rawHeight; y++ )
                                    if( x >= obj.Width || y >= obj.Height )
                                    {
                                        mask[y * rawWidth + x] = 0;
                                        if( !hasMask )
                                            hasMask = true;
                                    }
                        }
                        // emulate PictureBoxSizeMode.CenterImage behavior
                        else if( obj.SizeMode == PictureBoxSizeMode.CenterImage )
                        {
                            if( obj.Width < rawWidth || obj.Height < rawHeight )
                            {
                                float imgWidth = rawWidth;
                                float imgHeight = rawHeight;
                                float imgLeft = obj.AbsLeft + obj.Width / 2 - imgWidth / 2;
                                float imgTop = obj.AbsTop + obj.Height / 2 - imgHeight / 2;

                                float borderWidth = obj.Width;
                                float borderHeight = obj.Height;
                                float borderLeft = obj.AbsLeft;
                                float borderTop = obj.AbsTop;

                                RectangleF imgRect = new RectangleF
                                (
                                    imgLeft,
                                    imgTop,
                                    imgWidth,
                                    imgHeight
                                );

                                RectangleF borderRect = new RectangleF
                                (
                                    borderLeft,
                                    borderTop,
                                    borderWidth,
                                    borderHeight
                                );

                                if( imgRect.IntersectsWith( borderRect ) )
                                {
                                    // todo: need better check
                                    // on high zoom borders don't match image area

                                    RectangleF intersect = RectangleF.Intersect( imgRect, borderRect );
                                    intersect.Width = (int)intersect.Width;
                                    intersect.Height = (int)intersect.Height;

                                    for( int x = 0; x < rawWidth; x++ )
                                        for( int y = 0; y < rawHeight; y++ )
                                        {
                                            int _x = ( x + (int)imgRect.X );
                                            int _y = ( y + (int)imgRect.Y );

                                            if( !intersect.Contains( _x, _y ) )
                                            {
                                                mask[y * rawWidth + x] = 0;
                                                if( !hasMask )
                                                    hasMask = true;
                                            }
                                        }
                                }
                            }
                        }

                        using( MemoryStream colorsDeflateStream = new MemoryStream() )
                        {
                            colorsRawStream.Position = 0;
                            ExportUtils.ZLibDeflate( colorsRawStream, colorsDeflateStream );
                            colorsDeflateStream.Position = 0;

                            using( MemoryStream maskStream = hasMask ? new MemoryStream( mask ) : null )
                            {
                                imageIndex = WriteImage_Deflate( colorsDeflateStream, maskStream, rawWidth, rawHeight );
                            }
                        }
                    }

                    SetImageIndexByHash( hash, imageIndex );
                }
            }

            if( imageIndex == -1 )
                return false;

            AddImageToList( imageIndex );

            StringBuilder sb = new StringBuilder( 256 );
            sb.AppendLine( "q" );

            GetPDFFillTransparent( Color.FromArgb( (byte)( ( 1 - obj.Transparency ) * 255f ), Color.Black ), sb );

            RectangleF area = CalculateArea( obj, rawWidth, rawHeight );
            //double angle = (Math.PI / 180) * -obj.Angle; // get angle in radians

            // PLEASE NOTE
            // to avoid distortion, matrices should be in this order:
            // 1. translate
            // 2. rotate
            // 3. scale.

            // translate matrix
            sb.AppendLine( string.Format( "1 0 0 1 {0} {1} cm",
                FloatToString( area.Left ),
                FloatToString( area.Top )
            ) );

            // we don't yet support rotation
            // rotate matrix
            /*sb.AppendLine(string.Format("{0} {1} {2} {3} 0 0 cm",
                FloatToString(Math.Cos(angle)),
                FloatToString(Math.Sin(angle)),
                FloatToString(-Math.Sin(angle)),
                FloatToString(Math.Cos(angle))
            ));*/

            // scale matrix
            sb.AppendLine( string.Format( "{0} 0 0 {1} 0 0 cm",
                FloatToString( area.Width ),
                FloatToString( area.Height )
            ) );

            // draw image
            sb.AppendLine( string.Format( "/Im{0} Do",
                imageIndex.ToString()
            ) );

            sb.AppendLine( "Q" );
            sb_in.Append( sb );

            if( drawBorder )
                DrawPDFBorder( obj.Border, obj.AbsLeft, obj.AbsTop, /*width*/obj.Width, /*height*/obj.Height, sb_in );

            return true;
        }

        #endregion

        #region WriteMask, WriteImage_Deflate, WriteImage_Jpeg

        private long WriteMask( MemoryStream mask, int width, int height )
        {
            long maskIndex = -1;

            if( mask == null || mask.Length == 0 )
                return maskIndex;

            maskIndex = UpdateXRef();

            WriteLn( pdf, ObjNumber( maskIndex ) );
            WriteLn( pdf, "<<" );
            WriteLn( pdf, "/Type /XObject" );
            WriteLn( pdf, "/Subtype /Image" );
            WriteLn( pdf, "/Width " + width.ToString() );
            WriteLn( pdf, "/Height " + height.ToString() );
            WriteLn( pdf, "/ColorSpace /DeviceGray" );
            WriteLn( pdf, "/BitsPerComponent 8" );
            WriteLn( pdf, "/Interpolate false" );

            WritePDFStream( pdf, mask, maskIndex, FCompressed, FEncrypted, false, true );

            return maskIndex;
        }

        private long WriteImage_Deflate( MemoryStream image, MemoryStream mask, int width, int height )
        {
            long imageIndex = -1;

            if( image == null || image.Length == 0 )
                return imageIndex;

            long maskIndex = WriteMask( mask, width, height );
            imageIndex = UpdateXRef();

            WriteLn( pdf, ObjNumber( imageIndex ) );
            WriteLn( pdf, "<<" );
            WriteLn( pdf, "/Type /XObject" );
            WriteLn( pdf, "/Subtype /Image" );
            WriteLn( pdf, "/Width " + width.ToString() );
            WriteLn( pdf, "/Height " + height.ToString() );
            WriteLn( pdf, "/Predictor 10" );

            if( ColorSpace == PdfColorSpace.RGB )
                WriteLn( pdf, "/ColorSpace /DeviceRGB" );
            else if( ColorSpace == PdfColorSpace.CMYK )
                WriteLn( pdf, "/ColorSpace /DeviceCMYK" );

            WriteLn( pdf, "/BitsPerComponent 8" );
            WriteLn( pdf, "/Filter /FlateDecode" );
            WriteLn( pdf, "/Interpolate false" );

            if( maskIndex != -1 )
                WriteLn( pdf, "/SMask " + ObjNumberRef( maskIndex ) );

            WritePDFStream( pdf, image, imageIndex, false, FEncrypted, false, true );

            return imageIndex;
        }

        private long WriteImage_Jpeg( MemoryStream image, MemoryStream mask, int width, int height )
        {
            long imageIndex = -1;

            if( image == null || image.Length == 0 )
                return imageIndex;

            long maskIndex = WriteMask( mask, width, height );
            imageIndex = UpdateXRef();

            WriteLn( pdf, ObjNumber( imageIndex ) );
            WriteLn( pdf, "<<" );
            WriteLn( pdf, "/Type /XObject" );
            WriteLn( pdf, "/Subtype /Image" );
            WriteLn( pdf, "/Width " + width.ToString() );
            WriteLn( pdf, "/Height " + height.ToString() );
            WriteLn( pdf, "/ColorSpace /DeviceRGB" );
            WriteLn( pdf, "/BitsPerComponent 8" );
            WriteLn( pdf, "/Filter /DCTDecode" );
            WriteLn( pdf, "/Interpolate false" );

            if( maskIndex != -1 )
                WriteLn( pdf, "/SMask " + ObjNumberRef( maskIndex ) );

            WritePDFStream( pdf, image, imageIndex, false, FEncrypted, false, true );

            return imageIndex;
        }

        #endregion

        #region Other methods

        private int[] GetRawBitmap( Bitmap image )
        {
            int raw_size = image.Width * image.Height;
            int[] raw_picture = new int[raw_size];
            BitmapData bmpdata = image.LockBits( new Rectangle( 0, 0, image.Width, image.Height ), ImageLockMode.ReadOnly, image.PixelFormat );
            IntPtr ptr = bmpdata.Scan0;
            System.Runtime.InteropServices.Marshal.Copy( ptr, raw_picture, 0, raw_size );
            image.UnlockBits( bmpdata );
            return raw_picture;
        }

        private string CalculateHash( int[] raw_picture )
        {
            Random hashGenerator = new Random( 9705 );
            Int64 value = 0;
            for( int i = 0; i < raw_picture.Length; i++ )
                value += raw_picture[hashGenerator.Next() % raw_picture.Length] * hashGenerator.Next();
            return Math.Abs( value ).ToString();
        }

        /// <summary>
        /// Calculates mask for <see cref="PixelFormat.Format32bppArgb"/> image.
        /// </summary>
        private MemoryStream GetMask( int[] raw_pixels )
        {
            if( !FTransparentImages )
                return null;

            if( PdfCompliance == PdfStandard.PdfX_3 )
                return null;

            MemoryStream mask_stream = new MemoryStream( raw_pixels.Length );

            bool alpha = false;
            byte pixel;

            for( int i = 0; i < raw_pixels.Length; i++ )
            {
                pixel = (byte)( ( (UInt32)raw_pixels[i] ) >> 24 );
                if( !alpha && pixel != 0xff )
                    alpha = true;
                mask_stream.WriteByte( pixel );
            }

            if( alpha )
            {
                mask_stream.Position = 0;
                return mask_stream;
            }

            return null;
        }

        /// <summary>
        /// Calculates image bounds according to <see cref="PictureBoxSizeMode"/>.
        /// </summary>
        RectangleF CalculateArea( PictureObject obj, int raw_width, int raw_height )
        {
            float width = obj.Width;
            float height = obj.Height;

            float pdfLeft = GetLeft( obj.AbsLeft );
            float pdfTop = GetTop( obj.AbsTop + height );

            switch( obj.SizeMode )
            {
                case PictureBoxSizeMode.Normal:
                    {
                        width = raw_width;
                        height = raw_height;

                        pdfTop = GetTop( obj.AbsTop + height );
                    }
                    break;
                case PictureBoxSizeMode.Zoom:
                    {
                        float w = obj.Width / raw_width;
                        float h = obj.Height / raw_height;

                        if( w > h )
                        {
                            width = raw_width * h;
                            height = raw_height * h;
                            pdfLeft = GetLeft( obj.AbsLeft + obj.Width / 2 - width / 2 );
                        }
                        else
                        {
                            width = raw_width * w;
                            height = raw_height * w;
                            pdfTop = GetTop( obj.AbsTop + height + obj.Height / 2 - height / 2 );
                        }
                    }
                    break;
                case PictureBoxSizeMode.CenterImage:
                    {
                        width = raw_width;
                        height = raw_height;

                        pdfLeft = GetLeft( obj.AbsLeft + obj.Width / 2 - width / 2 );
                        pdfTop = GetTop( obj.AbsTop + height + obj.Height / 2 - height / 2 );
                    }
                    break;
            }

            float pdfWidth = width * PDF_DIVIDER;
            float pdfHeight = height * PDF_DIVIDER;

            return new RectangleF( pdfLeft, pdfTop, pdfWidth, pdfHeight );
        }

        /// <summary>
        /// Writes pixels' colors without alpha to stream according to CMYK or RGB color space.
        /// Pixels should be in the <see cref="PixelFormat.Format32bppArgb"/> format.
        /// </summary>
        void WritePixelColors( int[] pixels, Stream stream, bool grayscale )
        {
            for( int i = 0; i < pixels.Length; i++ )
            {
                byte blue = (byte)( pixels[i] & 0xFF );
                byte green = (byte)( ( pixels[i] >> 8 ) & 0xFF );
                byte red = (byte)( ( pixels[i] >> 16 ) & 0xFF );

                if( ColorSpace == PdfColorSpace.RGB )
                {
                    if( grayscale )
                    {
                        byte gray = (byte)( red * 0.299 + green * 0.587 + blue * 0.114 );
                        stream.WriteByte( gray );
                        stream.WriteByte( gray );
                        stream.WriteByte( gray );
                    }
                    else
                    {
                        stream.WriteByte( red );
                        stream.WriteByte( green );
                        stream.WriteByte( blue );
                    }
                }
                else if( ColorSpace == PdfColorSpace.CMYK )
                {
                    if( grayscale )
                    {
                        byte gray = (byte)( 255 - ( red * 0.299 + green * 0.587 + blue * 0.114 ) );
                        stream.WriteByte( 0 );
                        stream.WriteByte( 0 );
                        stream.WriteByte( 0 );
                        stream.WriteByte( gray );
                    }
                    else
                    {
                        float fred = ( (float)red ) / 255f;
                        float fgreen = ( (float)green ) / 255f;
                        float fblue = ( (float)blue ) / 255f;

                        float fblack = 1 - Math.Max( fred, Math.Max( fgreen, fblue ) );
                        float fcyan = ( 1 - fred - fblack ) / ( 1 - fblack );
                        float fmagenta = ( 1 - fgreen - fblack ) / ( 1 - fblack );
                        float fyellow = ( 1 - fblue - fblack ) / ( 1 - fblack );

                        byte black = (byte)( fblack * 255 );
                        byte cyan = (byte)( fcyan * 255 );
                        byte magenta = (byte)( fmagenta * 255 );
                        byte yellow = (byte)( fyellow * 255 );

                        stream.WriteByte( cyan );
                        stream.WriteByte( magenta );
                        stream.WriteByte( yellow );
                        stream.WriteByte( black );
                    }
                }
            }
        }

#if PRINT_HOUSE
        /// <summary>
        /// This method attempts to find a raw CMYKA data
        /// which should be stored in a separate file
        /// alongside of the image location.
        /// </summary>
        Stream FindRawCMYKA(PictureObject picObj)
        {
            Stream stream = null;

            if (picObj != null && picObj.ImageLocation != null && picObj.ImageLocation.Trim() != "")
            {
                string file = picObj.ImageLocation + ".cmyka";
                string file_gz = picObj.ImageLocation + ".cmyka.gz";

                if (File.Exists(file))
                {
                    stream = File.OpenRead(file);
                }
                else if (File.Exists(file_gz))
                {
                    //stream = new GZipStream(File.OpenRead(file_gz), CompressionMode.Decompress);

                    using (FileStream reader = File.OpenRead(file_gz))
                    using (GZipStream zip = new GZipStream(reader, CompressionMode.Decompress, true))
                    {
                        stream = new MemoryStream();
                        zip.CopyTo(stream);
                        stream.Position = 0;
                    }
                }
            }

            return stream;
        }
#endif

        #endregion

        #region Picture list and hashes

        private List<long> picResList;
        private Dictionary<string, long> hashList;

        private long GetImageIndexByHash( string hash )
        {
            long result = -1;
            if( hashList.TryGetValue( hash, out result ) )
                return result;
            else
                return -1;
        }

        private void SetImageIndexByHash( string hash, long index )
        {
            hashList.Add( hash, index );
        }

        private void AddImageToList( long imageIndex )
        {
            if( picResList.IndexOf( imageIndex ) == -1 )
                picResList.Add( imageIndex );
        }

        #endregion
    }
}
