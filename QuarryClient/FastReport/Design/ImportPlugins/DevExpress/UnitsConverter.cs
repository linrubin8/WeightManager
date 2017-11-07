using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Barcode;

namespace FastReport.Design.ImportPlugins.DevExpress
{
    /// <summary>
    /// The DevExpress units converter.
    /// </summary>
#if! Basic
    public
#else
    internal
#endif
    static class UnitsConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts SizeF to pixels.
        /// </summary>
        /// <param name="str">SizeF value as string.</param>
        /// <returns>The value in pixels.</returns>
        public static float SizeFToPixels(string str)
        {
            float value = 0.0f;
            if (!String.IsNullOrEmpty(str))
            {
                int end = str.IndexOf("F");
                if (end > -1)
                {
                    value = Convert.ToSingle(str.Substring(0, end));
                }
                else
                {
                    value = Convert.ToSingle(str);
                }
            }
            return value;
        }

        /// <summary>
        /// Converts DevExpress Color.
        /// </summary>
        /// <param name="str">The DevExpress Color value as string.</param>
        /// <returns>The Color value.</returns>
        public static Color ConvertColor(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                if (str.IndexOf("FromArgb") > -1)
                {
                    int start = str.IndexOf("byte") + 6;
                    int red = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    start = str.IndexOf("byte", start) + 6;
                    int green = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    start = str.IndexOf("byte", start) + 6;
                    int blue = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    return Color.FromArgb(red, green, blue);
                }
                else
                {
                    return Color.FromName(str.Replace("System.Drawing.Color.", ""));
                }
            }
            return Color.Black;
        }

        /// <summary>
        /// Converts DevExpress BackColor.
        /// </summary>
        /// <param name="str">The DevExpress BackColor value as string.</param>
        /// <returns>The Color value.</returns>
        public static Color ConvertBackColor(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                if (str.IndexOf("FromArgb") > -1)
                {
                    int start = str.IndexOf("byte") + 6;
                    int red = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    start = str.IndexOf("byte", start) + 6;
                    int green = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    start = str.IndexOf("byte", start) + 6;
                    int blue = Convert.ToInt32(str.Substring(start, str.IndexOf(")", start) - start));
                    return Color.FromArgb(red, green, blue);
                }
                else
                {
                    return Color.FromName(str.Replace("System.Drawing.Color.", ""));
                }
            }
            return Color.Transparent;
        }

        /// <summary>
        /// Converts the DevExpress BorderDashStyle to LineStyle.
        /// </summary>
        /// <param name="borderDashStyle">The DevExpress BorderDashStyle value.</param>
        /// <returns>The LineStyle value.</returns>
        public static LineStyle ConvertBorderDashStyle(string borderDashStyle)
        {
            if (borderDashStyle == "DevExpress.XtraPrinting.BorderDashStyle.Dot")
            {
                return LineStyle.Dot;
            }
            else if (borderDashStyle == "DevExpress.XtraPrinting.BorderDashStyle.Dash")
            {
                return LineStyle.Dash;
            }
            else if (borderDashStyle == "DevExpress.XtraPrinting.BorderDashStyle.DashDot")
            {
                return LineStyle.DashDot;
            }
            else if (borderDashStyle == "DevExpress.XtraPrinting.BorderDashStyle.DashDotDot")
            {
                return LineStyle.DashDotDot;
            }
            else if (borderDashStyle == "DevExpress.XtraPrinting.BorderDashStyle.Double")
            {
                return LineStyle.Double;
            }
            return LineStyle.Solid;
        }

        /// <summary>
        /// Converts the DevExpress LineStyle to LineStyle.
        /// </summary>
        /// <param name="lineStyle">The DevExpress LineStyle value.</param>
        /// <returns>The LineStyle value.</returns>
        public static LineStyle ConvertLineStyle(string lineStyle)
        {
            if (lineStyle == "System.Drawing.Drawing2D.DashStyle.Dot")
            {
                return LineStyle.Dot;
            }
            else if (lineStyle == "System.Drawing.Drawing2D.DashStyle.Dash")
            {
                return LineStyle.Dash;
            }
            else if (lineStyle == "System.Drawing.Drawing2D.DashStyle.DashDot")
            {
                return LineStyle.DashDot;
            }
            else if (lineStyle == "System.Drawing.Drawing2D.DashStyle.DashDotDot")
            {
                return LineStyle.DashDotDot;
            }
            else if (lineStyle == "System.Drawing.Drawing2D.DashStyle.Double")
            {
                return LineStyle.Double;
            }
            return LineStyle.Solid;
        }

        /// <summary>
        /// Converts the DevExpress TextAlignment to HorzAlignment.
        /// </summary>
        /// <param name="textAlignment">The DevExpress TextAlignment value.</param>
        /// <returns>The HorzAlign value.</returns>
        public static HorzAlign ConvertTextAlignmentToHorzAlign(string textAlignment)
        {
            if (textAlignment.Contains("Center"))
            {
                return HorzAlign.Center;
            }
            if (textAlignment.Contains("Justify"))
            {
                return HorzAlign.Justify;
            }
            if (textAlignment.Contains("Right"))
            {
                return HorzAlign.Right;
            }
            return HorzAlign.Left;
        }

        /// <summary>
        /// Converts the DevExpress TextAlignment to VertAlignment.
        /// </summary>
        /// <param name="textAlignment">The DevExpress TextAlignment value.</param>
        /// <returns>The VertAlign value.</returns>
        public static VertAlign ConvertTextAlignmentToVertAlign(string textAlignment)
        {
            if (textAlignment.Contains("Middle"))
            {
                return VertAlign.Center;
            }
            if (textAlignment.Contains("Bottom"))
            {
                return VertAlign.Bottom;
            }
            return VertAlign.Top;
        }

        /// <summary>
        /// Converts the DevExpress ImageSizeMode to PictureBoxSizeMode.
        /// </summary>
        /// <param name="sizeMode">The ImageSizeMode value as string.</param>
        /// <returns>The PictureBoxSizeMode value.</returns>
        public static PictureBoxSizeMode ConvertImageSizeMode(string sizeMode)
        {
            if (sizeMode == "DevExpress.XtraPrinting.ImageSizeMode.StretchImage")
            {
                return PictureBoxSizeMode.StretchImage;
            }
            else if (sizeMode == "DevExpress.XtraPrinting.ImageSizeMode.AutoSize")
            {
                return PictureBoxSizeMode.AutoSize;
            }
            else if (sizeMode == "DevExpress.XtraPrinting.ImageSizeMode.CenterImage")
            {
                return PictureBoxSizeMode.CenterImage;
            }
            else if (sizeMode == "DevExpress.XtraPrinting.ImageSizeMode.ZoomImage")
            {
                return PictureBoxSizeMode.Zoom;
            }
            else if (sizeMode == "DevExpress.XtraPrinting.ImageSizeMode.Squeeze")
            {
                return PictureBoxSizeMode.Zoom;
            }
            return PictureBoxSizeMode.Normal;
        }

        /// <summary>
        /// Converts the DevExpress Shape to ShapeKind.
        /// </summary>
        /// <param name="shape">The DevExpress Shape value as string.</param>
        /// <returns>The ShapeKind value.</returns>
        public static ShapeKind ConvertShape(string shape)
        {
            if (shape.Contains("Rectangle"))
            {
                return ShapeKind.Rectangle;
            }
            else if (shape.Contains("Polygon"))
            {
                return ShapeKind.Triangle;
            }
            return ShapeKind.Ellipse;
        }

        /// <summary>
        /// Converts the DevExpress Barcode.Symbology to Barcode.Barcode.
        /// </summary>
        /// <param name="symbology">The DevExpress Barcode.Symbology value as string.</param>
        /// <param name="barcode">The BarcodeObject instance.</param>
        public static void ConvertBarcodeSymbology(string symbology, BarcodeObject barcode)
        {
            if (symbology.Contains("codabar"))
            {
                barcode.Barcode = new BarcodeCodabar();
            }
            else if (symbology.Contains("code128"))
            {
                barcode.Barcode = new Barcode128();
            }
            else if (symbology.Contains("code39"))
            {
                barcode.Barcode = new Barcode39();
            }
            else if (symbology.Contains("code39Extended"))
            {
                barcode.Barcode = new Barcode39Extended();
            }
            else if (symbology.Contains("code93"))
            {
                barcode.Barcode = new Barcode93();
            }
            else if (symbology.Contains("code9eExtended"))
            {
                barcode.Barcode = new Barcode93Extended();
            }
            else if (symbology.Contains("codeMSI"))
            {
                barcode.Barcode = new BarcodeMSI();
            }
            else if (symbology.Contains("dataMatrix"))
            {
                barcode.Barcode = new BarcodeDatamatrix();
            }
            else if (symbology.Contains("eAN128"))
            {
                barcode.Barcode = new BarcodeEAN128();
            }
            else if (symbology.Contains("eAN13"))
            {
                barcode.Barcode = new BarcodeEAN13();
            }
            else if (symbology.Contains("eAN8"))
            {
                barcode.Barcode = new BarcodeEAN8();
            }
            else if (symbology.Contains("industrial2of5"))
            {
                barcode.Barcode = new Barcode2of5Industrial();
            }
            else if (symbology.Contains("interleaved2of5"))
            {
                barcode.Barcode = new Barcode2of5Interleaved();
            }
            else if (symbology.Contains("matrix2of5"))
            {
                barcode.Barcode = new Barcode2of5Matrix();
            }
            else if (symbology.Contains("pDF417"))
            {
                barcode.Barcode = new BarcodePDF417();
            }
            else if (symbology.Contains("postNet"))
            {
                barcode.Barcode = new BarcodePostNet();
            }
            else if (symbology.Contains("qRCode"))
            {
                barcode.Barcode = new BarcodeQR();
            }
            else if (symbology.Contains("uPCA"))
            {
                barcode.Barcode = new BarcodeUPC_A();
            }
            else if (symbology.Contains("uPCE0"))
            {
                barcode.Barcode = new BarcodeUPC_E0();
            }
            else if (symbology.Contains("uPCE1"))
            {
                barcode.Barcode = new BarcodeUPC_E1();
            }
            else if (symbology.Contains("uPCSupplemental2"))
            {
                barcode.Barcode = new BarcodeSupplement2();
            }
            else if (symbology.Contains("uPCSupplemental5"))
            {
                barcode.Barcode = new BarcodeSupplement5();
            }
        }

        #endregion // Public Methods
    }
}
