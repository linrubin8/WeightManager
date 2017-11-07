using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Design.ExportPlugins.FR3
{
    /// <summary>
    /// The FR3 units converter.
    /// </summary>
    public static class UnitsConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts Color to TColor.
        /// </summary>
        /// <param name="color">Color value.</param>
        /// <returns>String that contains TColor value.</returns>
        public static string ColorToTColor(Color color)
        {
            return (color.B << 16 | color.G << 8 | color.R).ToString();
        }

        /// <summary>
        /// Converts font style.
        /// </summary>
        /// <param name="fontStyle">FontStyle value.</param>
        /// <returns>String that contains converted value.</returns>
        public static string ConvertFontStyle(FontStyle fontStyle)
        {
            int fs = 0;
            if (FontStyle.Bold == (fontStyle & FontStyle.Bold))
            {
                fs = 1;
            }
            if (FontStyle.Italic == (fontStyle & FontStyle.Italic))
            {
                fs = fs | 2;
            }
            if (FontStyle.Underline == (fontStyle & FontStyle.Underline))
            {
                fs = fs | 4;
            }
            if (FontStyle.Strikeout == (fontStyle & FontStyle.Strikeout))
            {
                fs = fs | 8;
            }
            return fs.ToString();
        }

        /// <summary>
        /// Converts horizontal alignment of text.
        /// </summary>
        /// <param name="ha">HorzAlign value.</param>
        /// <returns>String that contains converted value.</returns>
        public static string ConvertHorzAlign(HorzAlign ha)
        {
            string result = "";
            switch (ha)
            {
                case HorzAlign.Left:
                    result = "haLeft";
                    break;
                case HorzAlign.Center:
                    result = "haCenter";
                    break;
                case HorzAlign.Right:
                    result = "haRight";
                    break;
                case HorzAlign.Justify:
                    result = "haBlock";
                    break;
                default:
                    result = "haLeft";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Converts vertical alignment of text.
        /// </summary>
        /// <param name="va">VertAlign value.</param>
        /// <returns>String that contains coverted value.</returns>
        public static string ConvertVertAlign(VertAlign va)
        {
            string result = "";
            switch (va)
            {
                case VertAlign.Top:
                    result = "vaTop";
                    break;
                case VertAlign.Center:
                    result = "vaCenter";
                    break;
                case VertAlign.Bottom:
                    result = "vaBottom";
                    break;
                default:
                    result = "vaTop";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Converts font size to delphi font height.
        /// </summary>
        /// <param name="size">Font size value.</param>
        /// <returns>String that contains font height value.</returns>
        public static string ConvertFontSize(float size)
        {
            return ((int)(-Math.Round(size * 96 / 72, 0))).ToString();
        }

        /// <summary>
        /// Convert line style to frame style.
        /// </summary>
        /// <param name="style">Line style value.</param>
        /// <returns>String that contains converted value.</returns>
        public static string ConvertLineStyle(LineStyle style)
        {
            string result = "";
            switch (style)
            {
                case LineStyle.Solid:
                    result = "fsSolid";
                    break;
                case LineStyle.Dash:
                    result = "fsDash";
                    break;
                case LineStyle.DashDot:
                    result = "fsDashDot";
                    break;
                case LineStyle.DashDotDot:
                    result = "fsDashDotDot";
                    break;
                case LineStyle.Dot:
                    result = "fsDot";
                    break;
                case LineStyle.Double:
                    result = "fsDouble";
                    break;
                default:
                    result = "fsSolid";
                    break;
            }
            return result;
        }

        #endregion // Public Methods
    }
}
