using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FastReport.Design.ExportPlugins.RDL
{
    /// <summary>
    /// The FR units converter.
    /// </summary>
    internal static class UnitsConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts the float size in pixels to string value in millimeters.
        /// </summary>
        /// <param name="pixels">The float value in pixels.</param>
        /// <returns>The string value in millimeters.</returns>
        public static string PixelsToMillimeters(float pixels)
        {
            return (pixels / ImportPlugins.RDL.SizeUnitsP.Millimeter).ToString() + "mm";
        }

        /// <summary>
        /// Converts the float size in millimeters to string value in millimeters.
        /// </summary>
        /// <param name="millimeters">The float value in millimeters.</param>
        /// <returns>The string value in millimeters.</returns>
        public static string MillimetersToString(float millimeters)
        {
            return millimeters.ToString() + "mm";
        }

        /// <summary>
        /// Converts the bool value to string.
        /// </summary>
        /// <param name="value">The bool value.</param>
        /// <returns>The string value.</returns>
        public static string ConvertBool(bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// Converts the int size in pixels to string value in pt.
        /// </summary>
        /// <param name="pixels">The int value in pixels.</param>
        /// <returns>The string value in pt.</returns>
        public static string ConvertPixels(int pixels)
        {
            return pixels.ToString() + "pt";
        }

        /// <summary>
        /// Converts the Color value to string.
        /// </summary>
        /// <param name="color">The Color value.</param>
        /// <returns>The string representation of Color value.</returns>
        public static string ConvertColor(Color color)
        {
            if (color.ToKnownColor() == 0)
            {
                return Color.Red.Name;
            }
            return color.Name;
        }

        /// <summary>
        /// Converts the LineStyle value to RDL BorderStyle value.
        /// </summary>
        /// <param name="ls">The LineStyle value.</param>
        /// <returns>The string with RDL BorderStyle value.</returns>
        public static string ConvertLineStyle(LineStyle ls)
        {
            if (ls == LineStyle.Dot)
            {
                return "Dotted";
            }
            else if (ls == LineStyle.Dash)
            {
                return "Dashed";
            }
            else if (ls == LineStyle.Double)
            {
                return "Double";
            }
            return "Solid";
        }

        /// <summary>
        /// Converts the GradientStyle value to RDL GradientType value.
        /// </summary>
        /// <param name="gs">The GradientStyle value.</param>
        /// <returns>The string with RDL GradientType value.</returns>
        public static string ConvertGradientStyle(GradientStyle gs)
        {
            if (gs == GradientStyle.Center)
            {
                return "Center";
            }
            else if (gs == GradientStyle.DiagonalLeft)
            {
                return "DiagonalLeft";
            }
            else if (gs == GradientStyle.DiagonalRight)
            {
                return "DiagonalRight";
            }
            else if (gs == GradientStyle.HorizontalCenter)
            {
                return "HorizontalCenter";
            }
            else if (gs == GradientStyle.LeftRight)
            {
                return "LeftRight";
            }
            else if (gs == GradientStyle.TopBottom)
            {
                return "TopBottom";
            }
            else if (gs == GradientStyle.VerticalCenter)
            {
                return "VerticalCenter";
            }
            return "None";
        }

        /// <summary>
        /// Converts the FontStyle value to RDL FontStyle value.
        /// </summary>
        /// <param name="fs">The FontStyle value.</param>
        /// <returns>The string with RDL FontStyle value.</returns>
        public static string ConvertFontStyle(FontStyle fs)
        {
            if (fs == FontStyle.Italic)
            {
                return "Italic";
            }
            return "Normal";
        }

        /// <summary>
        /// Converts the FontFamily value to RDL FontFamily value.
        /// </summary>
        /// <param name="ff">The FontFamily value.</param>
        /// <returns>The string with RDL FontFamily value.</returns>
        public static string ConvertFontFamily(FontFamily ff)
        {
            return ff.Name;
        }

        /// <summary>
        /// Converts the HorzAlign value to RDL TextAlign value.
        /// </summary>
        /// <param name="ha">The HorzAlign value.</param>
        /// <returns>The string with RDL TextAling value.</returns>
        public static string ConvertHorzAlign(HorzAlign ha)
        {
            if (ha == HorzAlign.Center)
            {
                return "Center";
            }
            else if (ha == HorzAlign.Right)
            {
                return "Right";
            }
            return "Left";
        }

        /// <summary>
        /// Converts the VertAling value to RDL VerticalAling value.
        /// </summary>
        /// <param name="va">The VertAling value.</param>
        /// <returns>The string with RDL VerticalAlign value.</returns>
        public static string ConvertVertAlign(VertAlign va)
        {
            if (va == VertAlign.Center)
            {
                return "Middle";
            }
            else if (va == VertAlign.Bottom)
            {
                return "Bottom";
            }
            return "Top";
        }

        /// <summary>
        /// Converts the Angle value to RDL WritingMode value.
        /// </summary>
        /// <param name="angle">The Angle value.</param>
        /// <returns>The string with RDL WritingMode value.</returns>
        public static string ConvertAngleToWritingMode(int angle)
        {
            if (angle == 90)
            {
                return "tb-rl";
            }
            return "lr-tb";
        }

        /// <summary>
        /// Converts the FontSize value to RDL FontSize value.
        /// </summary>
        /// <param name="fs">The FontSize value.</param>
        /// <returns>The string with RDL FontSize value.</returns>
        public static string ConvertFontSize(float fs)
        {
            return fs.ToString() + "pt";
        }

        /// <summary>
        /// Converts the PictureBoxSizeMode value to RDL Sizing value.
        /// </summary>
        /// <param name="sm">The PictureBoxSizeMode value.</param>
        /// <returns>The string with RDL Sizing value.</returns>
        public static string ConvertSizeMode(PictureBoxSizeMode sm)
        {
            if (sm == PictureBoxSizeMode.AutoSize)
            {
                return "AutoSize";
            }
            else if (sm == PictureBoxSizeMode.StretchImage)
            {
                return "Fit";
            }
            else if (sm == PictureBoxSizeMode.Zoom)
            {
                return "FitProportional";
            }
            return "Clip";
        }

        /// <summary>
        /// Converts the SeriesChartType value to RDL Chart.Type value.
        /// </summary>
        /// <param name="type">The SeriesChartType value.</param>
        /// <returns>The string with RDL Chart.Type value.</returns>
        public static string ConvertChartType(SeriesChartType type)
        {
            if (type == SeriesChartType.Area)
            {
                return "Area";
            }
            else if (type == SeriesChartType.Bar)
            {
                return "Bar";
            }
            else if (type == SeriesChartType.Doughnut)
            {
                return "Doughnut";
            }
            else if (type == SeriesChartType.Line)
            {
                return "Line";
            }
            else if (type == SeriesChartType.Pie)
            {
                return "Pie";
            }
            else if (type == SeriesChartType.Bubble)
            {
                return "Bubble";
            }
            return "Column";
        }

        /// <summary>
        /// Converts the ChartColorPalette value to RDL Chart.Palette value.
        /// </summary>
        /// <param name="palette">The ChartColorPalette value.</param>
        /// <returns>The string with RDL Chart.Palette value.</returns>
        public static string ConvertChartPalette(ChartColorPalette palette)
        {
            if (palette == ChartColorPalette.EarthTones)
            {
                return "EarthTones";
            }
            else if (palette == ChartColorPalette.Excel)
            {
                return "Excel";
            }
            else if (palette == ChartColorPalette.Grayscale)
            {
                return "GrayScale";
            }
            else if (palette == ChartColorPalette.Light)
            {
                return "Light";
            }
            else if (palette == ChartColorPalette.Pastel)
            {
                return "Pastel";
            }
            else if (palette == ChartColorPalette.SemiTransparent)
            {
                return "SemiTransparent";
            }
            return "Default";
        }

        /// <summary>
        /// Converts the Legend.Docking and Legend.Alignment values to RDL Chart.Legend.Position value.
        /// </summary>
        /// <param name="docking">The Legend.Docking value.</param>
        /// <param name="alignment">The Legend.Alignment value.</param>
        /// <returns>The string with RDL Chart.Legend.Position value.</returns>
        public static string ConvertLegendDockingAndAlignment(Docking docking, StringAlignment alignment)
        {
            if (docking == Docking.Top && alignment == StringAlignment.Near)
            {
                return "TopLeft";
            }
            else if(docking == Docking.Top && alignment == StringAlignment.Center)
            {
                return "TopCenter";
            }
            else if(docking == Docking.Top && alignment == StringAlignment.Far)
            {
                return "TopRight";
            }
            else if(docking == Docking.Left && alignment == StringAlignment.Near)
            {
                return "LeftTop";
            }
            else if(docking == Docking.Left && alignment == StringAlignment.Center)
            {
                return "LeftCenter";
            }
            else if(docking == Docking.Left && alignment == StringAlignment.Far)
            {
                return "LeftBottom";
            }
            else if(docking == Docking.Right && alignment == StringAlignment.Near)
            {
                return "RightTop";
            }
            else if(docking == Docking.Right && alignment == StringAlignment.Center)
            {
                return "RightCenter";
            }
            else if(docking == Docking.Right && alignment == StringAlignment.Far)
            {
                return "RightBottom";
            }
            else if(docking == Docking.Bottom && alignment == StringAlignment.Near)
            {
                return "BottomLeft";
            }
            else if(docking == Docking.Bottom && alignment == StringAlignment.Center)
            {
                return "BottomCenter";
            }
            else if (docking == Docking.Bottom && alignment == StringAlignment.Far)
            {
                return "BottomRight";
            }
            return "TopLeft";
        }

        /// <summary>
        /// Converts the LegendStyle value to Chart.Legend.Layout value.
        /// </summary>
        /// <param name="ls">The LegendStyle value.</param>
        /// <returns>The string with RDL Chart.Legend.Layout value.</returns>
        public static string ConvertLegendStyle(LegendStyle ls)
        {
            if (ls == LegendStyle.Table)
            {
                return "Table";
            }
            else if (ls == LegendStyle.Row)
            {
                return "Row";
            }
            return "Column";
        }

        /// <summary>
        /// Converts the LightStyle value to RDL Shading value.
        /// </summary>
        /// <param name="ls">The LightStyle value.</param>
        /// <returns>The string with RDL Shading value.</returns>
        public static string ConvertLightStyle(LightStyle ls)
        {
            if (ls == LightStyle.Simplistic)
            {
                return "Simple";
            }
            else if (ls == LightStyle.Realistic)
            {
                return "Real";
            }
            return "Simple";
        }

        /// <summary>
        /// Converts the ChartDashStyle value to RDL BorderStyle value.
        /// </summary>
        /// <param name="cds">The ChartDashStyle value.</param>
        /// <returns>The string with RDL ChartDahsStyle value.</returns>
        public static string ConvertChartDashStyle(ChartDashStyle cds)
        {
            if (cds == ChartDashStyle.Dot)
            {
                return "Dotted";
            }
            else if (cds == ChartDashStyle.Dash)
            {
                return "Dashed";
            }
            return "Solid";
        }

        /// <summary>
        /// Converts the ContentAlignment value to RDL TextAlign value.
        /// </summary>
        /// <param name="ca">The ContentAlignment value.</param>
        /// <returns>The string with RDL TextAlign value.</returns>
        public static string ContentAlignmentToTextAlign(ContentAlignment ca)
        {
            if (ca == ContentAlignment.BottomCenter || ca == ContentAlignment.MiddleCenter || ca == ContentAlignment.TopCenter)
            {
                return "Center";
            }
            else if (ca == ContentAlignment.BottomLeft || ca == ContentAlignment.MiddleLeft || ca == ContentAlignment.TopLeft)
            {
                return "Left";
            }
            else if (ca == ContentAlignment.BottomRight || ca == ContentAlignment.MiddleRight || ca == ContentAlignment.TopRight)
            {
                return "Right";
            }
            return "Left";
        }

        /// <summary>
        /// Converts the ContentAlignment value to RDL VerticalAlign value.
        /// </summary>
        /// <param name="ca">The ContentAlignment value.</param>
        /// <returns>The string with RDL VerticalAlign value.</returns>
        public static string ContentAlignmentToVerticalAlign(ContentAlignment ca)
        {
            if (ca == ContentAlignment.TopCenter || ca == ContentAlignment.TopLeft || ca == ContentAlignment.TopRight)
            {
                return "Top";
            }
            else if (ca == ContentAlignment.MiddleCenter || ca == ContentAlignment.MiddleLeft || ca == ContentAlignment.MiddleRight)
            {
                return "Middle";
            }
            else if (ca == ContentAlignment.BottomCenter || ca == ContentAlignment.BottomLeft || ca == ContentAlignment.BottomRight)
            {
                return "Bottom";
            }
            return "Top";
        }

        /// <summary>
        /// Converts the AxisEnabled value to RDL Axis.Visible value.
        /// </summary>
        /// <param name="ae">The AxisEnabled value.</param>
        /// <returns>The string with RDL Axis.Visible value.</returns>
        public static string ConvertAxisEnabled(AxisEnabled ae)
        {
            if (ae == AxisEnabled.False)
            {
                return "false";
            }
            return "true";
        }

        /// <summary>
        /// Converts the TickMarkStyle value to RDL TickMarkStyle value.
        /// </summary>
        /// <param name="style">The TickMarkStyle value.</param>
        /// <returns>The string with RDL TickMarkStyle value.</returns>
        public static string ConvertTickMarkStyle(TickMarkStyle style)
        {
            if (style == TickMarkStyle.InsideArea)
            {
                return "Inside";
            }
            else if (style == TickMarkStyle.OutsideArea)
            {
                return "Outside";
            }
            else if (style == TickMarkStyle.AcrossAxis)
            {
                return "Cross";
            }
            return "None";
        }

        /// <summary>
        /// Converts the StringAlignment value to RDL TextAlign value.
        /// </summary>
        /// <param name="sa">The StringAlignment value.</param>
        /// <returns>The string with RDL TextAlign value.</returns>
        public static string ConvertStringAlignment(StringAlignment sa)
        {
            if (sa == StringAlignment.Near)
            {
                return "Left";
            }
            else if (sa == StringAlignment.Far)
            {
                return "Right";
            }
            return "Center";
        }

        #endregion // Public Methods
    }
}
