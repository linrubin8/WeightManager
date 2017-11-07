using System;
using System.Drawing;
using System.Xml;

namespace FastReport.Export.Svg
{
    public partial class SVGExport : ExportBase
    {
        #region Basic Shapes

        /// <summary>
        /// Method to add rectangle.
        /// </summary>
        private void DrawRectangle(float x, float y, float width, float height,
                                  Color stroke, float strokeThickness, FillBase fill, bool rounded, LineStyle border)
        {
            if ((stroke == Color.Transparent || strokeThickness == 0) &&
                ((fill is SolidFill) && (fill as SolidFill).Color == Color.Transparent)) return;

            XmlElement rect = doc.CreateElement("rect");
            AppndAttr(rect, "x", FloatToString(x));
            AppndAttr(rect, "y", FloatToString(y));
            if (width != 0)
                AppndAttr(rect, "width", FloatToString(width));
            if (height != 0)
                AppndAttr(rect, "height", FloatToString(height));
            if (stroke == Color.Transparent)
                AppndAttr(rect, "stroke", "none");
            else
                AppndAttr(rect, "stroke", ExportUtils.HTMLColorCode(stroke));
            if (strokeThickness > 1)
                AppndAttr(rect, "stroke-width", FloatToString(strokeThickness));
            string strokeDashArray = GetStrokeDash(border);
            if (strokeDashArray != null)
                AppndAttr(rect, "style", "stroke-dasharray: " + strokeDashArray);

            if (rounded)
            {
                AppndAttr(rect, "rx", "10");
                AppndAttr(rect, "ry", "10");
            }
            if (fill is SolidFill)
            {
                if ((fill as SolidFill).Color == Color.Transparent)
                    AppndAttr(rect, "fill", "none");
                else
                    AppndAttr(rect, "fill", ExportUtils.HTMLColorCode((fill as SolidFill).Color));
            }
            else if (fill is GlassFill)
                AppndAttr(rect, "fill", DrawGlass(fill as GlassFill));
            g.AppendChild(rect);
        }

        /// <summary>
        /// Method for add ellips.
        /// </summary>
        private void DrawEllipse(float x, float y, float width, float height,
             Color stroke, float strokeThickness, FillBase fill)
        {
            XmlElement ellipse = doc.CreateElement("ellipse");

            AppndAttr(ellipse, "cx", FloatToString(x + width / 2));
            AppndAttr(ellipse, "cy", FloatToString(y + height / 2));
            if (width != 0)
                AppndAttr(ellipse, "rx", FloatToString(width / 2));
            if (height != 0)
                AppndAttr(ellipse, "ry", FloatToString(height / 2));
            if (stroke == Color.Transparent)
                AppndAttr(ellipse, "stroke", "none");
            else
                AppndAttr(ellipse, "stroke", ExportUtils.HTMLColorCode(stroke));
            if (strokeThickness != 0 && strokeThickness != 1)
                AppndAttr(ellipse, "stroke-width", FloatToString(strokeThickness));
            if (fill is SolidFill)
            {
                if ((fill as SolidFill).Color == Color.Transparent)
                    AppndAttr(ellipse, "fill", "none");
                else
                    AppndAttr(ellipse, "fill", ExportUtils.HTMLColorCode((fill as SolidFill).Color));
            }
            g.AppendChild(ellipse);
        }

        /// <summary>
        /// Method for add triangle.
        /// </summary>
        private void DrawTriangle(float x, float y, float width, float height,
             Color stroke, float strokeThickness, FillBase fill)
        {
            XmlElement polygon = doc.CreateElement("polygon");

            string Points;
            float x1 = width / 2 + x;
            float y1 = y;
            float x2 = width + x;
            float y2 = height + y;
            float x3 = x;
            float y3 = height + y;
            Points = FloatToString(x1) + "," + FloatToString(y1) + "," + FloatToString(x2) + "," + FloatToString(y2) + "," + FloatToString(x3) + "," + FloatToString(y3);
            nsAttr = doc.CreateAttribute("points");
            nsAttr.Value = Points;
            polygon.Attributes.Append(nsAttr);

            if (stroke == Color.Transparent)
                AppndAttr(polygon, "stroke", "none");
            else
                AppndAttr(polygon, "stroke", ExportUtils.HTMLColorCode(stroke));
            if (strokeThickness != 0 && strokeThickness != 1)
                AppndAttr(polygon, "stroke-width", FloatToString(strokeThickness));
            if (fill is SolidFill)
            {
                if ((fill as SolidFill).Color == Color.Transparent)
                    AppndAttr(polygon, "fill", "none");
                else
                    AppndAttr(polygon, "fill", ExportUtils.HTMLColorCode((fill as SolidFill).Color));
            }
            g.AppendChild(polygon);
        }

        /// <summary>
        /// Method for add Diamond.
        /// </summary>
        private void DrawDiamond(float x, float y, float width, float height,
             Color stroke, float strokeThickness, FillBase fill)
        {
            XmlElement polygon = doc.CreateElement("polygon");

            string Points;
            float x1 = width / 2 + x; float y1 = y;
            float x2 = width + x; float y2 = height / 2 + y;
            float x3 = width / 2 + x; float y3 = height + y;
            float x4 = x; float y4 = height / 2 + y;
            Points = FloatToString(x1) + "," + FloatToString(y1) + "," + FloatToString(x2) + "," + FloatToString(y2) + "," + FloatToString(x3) + "," + FloatToString(y3) + "," + FloatToString(x4) + "," + FloatToString(y4);
            nsAttr = doc.CreateAttribute("points");
            nsAttr.Value = Points;
            polygon.Attributes.Append(nsAttr);

            if (stroke == Color.Transparent)
                AppndAttr(polygon, "stroke", "none");
            else
                AppndAttr(polygon, "stroke", ExportUtils.HTMLColorCode(stroke));
            if (strokeThickness != 0 && strokeThickness != 1)
                AppndAttr(polygon, "stroke-width", FloatToString(strokeThickness));
            if (fill is SolidFill)
            {
                if ((fill as SolidFill).Color == Color.Transparent)
                    AppndAttr(polygon, "fill", "none");
                else
                    AppndAttr(polygon, "fill", ExportUtils.HTMLColorCode((fill as SolidFill).Color));
            }
            g.AppendChild(polygon);
        }

        ///<summary>
        ///Method for add line.
        /// </summary>
        private void DrawLine(float x, float y, float x2, float y2, Color stroke, float strokeThickness)
        {
            XmlElement line = doc.CreateElement("line");
            AppndAttr(line, "x1", FloatToString(x));
            AppndAttr(line, "y1", FloatToString(y));
            if (x2 != 0)
                AppndAttr(line, "x2", FloatToString(x2));
            if (y2 != 0)
                AppndAttr(line, "y2", FloatToString(y2));
            //if (stroke != null && stroke != "Black" && stroke != "#ff000000")
            AppndAttr(line, "stroke", ExportUtils.HTMLColorCode(stroke));
            if (strokeThickness != 0 && strokeThickness != 1)
                AppndAttr(line, "stroke-width", FloatToString(strokeThickness));
            g.AppendChild(line);
        }

        ///<summary>
        ///Method for add line with dash.
        /// </summary>
        private void DrawLine(float x, float y, float x2, float y2, Color stroke, float strokeThickness, LineStyle lineStyle)
        {
            DrawLine(x, y, x2, y2, stroke, strokeThickness);
            string strokeDashArray = GetStrokeDash(lineStyle);
            if (lineStyle == LineStyle.Double)
                DrawLine(x + 10, y + 10, x2 + 10, y2 + 10, stroke, strokeThickness);
            if (strokeDashArray != null)
                AppndAttr((XmlElement)g.LastChild, "stroke-dasharray", strokeDashArray);
        }
        #endregion

        #region Custom Shapes
        private void DrawBorder(Border Border, float left, float top, float width, float height)
        {
            if (Border.Shadow)
            {
                DrawRectangle(left + width,
                    top + Border.ShadowWidth,
                    Border.ShadowWidth,
                    height,
                    Color.Transparent,
                    0,
                    new SolidFill(Border.ShadowColor),
                    false,
                    LineStyle.Solid);
                DrawRectangle(left + Border.ShadowWidth,
                        top + height,
                        width - Border.ShadowWidth,
                        Border.ShadowWidth,
                        Color.Transparent,
                        0,
                        new SolidFill(Border.ShadowColor),
                        false,
                        LineStyle.Solid);
            }
            if (Border.Lines != BorderLines.None)
            {
                if (Border.Lines == BorderLines.All &&
                    Border.LeftLine.Equals(Border.RightLine) &&
                    Border.TopLine.Equals(Border.BottomLine) &&
                    Border.LeftLine.Equals(Border.TopLine))
                {
                    if (Border.Width > 0 && Border.Color != Color.Transparent)
                    {
                        SolidFill f = new SolidFill();
                        f.Color = Color.Transparent;
                        DrawRectangle(left, top,
                            width, height, Border.Color, Border.Width, f,
                            false,
                            Border.Style);

                        if (Border.LeftLine.Style == LineStyle.Double)
                            DrawRectangle(left + 2, top + 2,
                                width - 2, height - 2,
                                Border.Color, Border.Width, f,
                                false, Border.Style);
                    }
                }
                else
                {
                    float Left = left;
                    float Top = top;
                    float Right = left + width;
                    float Bottom = top + height;
                    Top -= 0.1f;
                    Bottom += 0.1f;

                    if ((Border.Lines & BorderLines.Left) > 0)
                    {
                        DrawLine(Left, Top, Left, Bottom, Border.LeftLine.Color, Border.LeftLine.Width, Border.LeftLine.Style);
                        if (Border.LeftLine.Style == LineStyle.Double)
                            DrawLine(Left + 2, Top, Left + 2, Bottom, Border.LeftLine.Color, Border.LeftLine.Width, Border.LeftLine.Style);
                    }
                    if ((Border.Lines & BorderLines.Right) > 0)
                    {
                        DrawLine(Right, Top, Right, Bottom, Border.RightLine.Color, Border.RightLine.Width, Border.RightLine.Style);
                        if (Border.RightLine.Style == LineStyle.Double)
                            DrawLine(Right - 2, Top, Right - 2, Bottom, Border.RightLine.Color, Border.RightLine.Width, Border.RightLine.Style);
                    }
                    /*   Top += 0.1f;       //????????
                       Bottom -= 0.1f;
                       Left += 0.1f;
                       Right -= 0.1f;*/
                    if ((Border.Lines & BorderLines.Top) > 0)
                    {
                        DrawLine(Left, Top, Right, Top, Border.TopLine.Color, Border.TopLine.Width, Border.TopLine.Style);
                        if (Border.TopLine.Style == LineStyle.Double)
                            DrawLine(Left, Top - 2, Right, Top - 2, Border.TopLine.Color, Border.TopLine.Width, Border.TopLine.Style);
                    }
                    if ((Border.Lines & BorderLines.Bottom) > 0)
                    {
                        DrawLine(Left, Bottom, Right, Bottom, Border.BottomLine.Color, Border.BottomLine.Width, Border.BottomLine.Style);
                        if (Border.BottomLine.Style == LineStyle.Double)
                            DrawLine(Left, Bottom + 2, Right, Bottom + 2, Border.BottomLine.Color, Border.BottomLine.Width, Border.BottomLine.Style);
                    }
                }
            }
        }

        private void DrawArrow(CapSettings Arrow, float lineWidth, float x1, float y1, float x2, float y2, out float x3, out float y3, out float x4, out float y4)
        {
            float k1, a, b, c, d;
            float xp, yp;
            float wd = Arrow.Width * lineWidth;
            float ld = Arrow.Height * lineWidth;
            if (Math.Abs(x2 - x1) > 0)
            {
                k1 = (y2 - y1) / (x2 - x1);
                a = (float)(Math.Pow(k1, 2) + 1);
                b = 2 * (k1 * ((x2 * y1 - x1 * y2) / (x2 - x1) - y2) - x2);
                c = (float)(Math.Pow(x2, 2) + Math.Pow(y2, 2) - Math.Pow(ld, 2) +
                    Math.Pow((x2 * y1 - x1 * y2) / (x2 - x1), 2) -
                    2 * y2 * (x2 * y1 - x1 * y2) / (x2 - x1));
                d = (float)(Math.Pow(b, 2) - 4 * a * c);
                xp = (float)((-b + Math.Sqrt(d)) / (2 * a));
                if ((xp > x1) && (xp > x2) || (xp < x1) && (xp < x2))
                    xp = (float)((-b - Math.Sqrt(d)) / (2 * a));
                yp = xp * k1 + (x2 * y1 - x1 * y2) / (x2 - x1);
                if (y2 != y1)
                {
                    x3 = (float)(xp + wd * Math.Sin(Math.Atan(k1)));
                    y3 = (float)(yp - wd * Math.Cos(Math.Atan(k1)));
                    x4 = (float)(xp - wd * Math.Sin(Math.Atan(k1)));
                    y4 = (float)(yp + wd * Math.Cos(Math.Atan(k1)));
                }
                else
                {
                    x3 = xp; y3 = yp - wd;
                    x4 = xp; y4 = yp + wd;
                }
            }
            else
            {
                xp = x2; yp = y2 - ld;
                if ((yp > y1) && (yp > y2) || (yp < y1) && (yp < y2))
                    yp = y2 + ld;
                x3 = xp - wd; y3 = yp;
                x4 = xp + wd; y4 = yp;
            }
        }

        private void DrawUnderlines(TextObject obj)
        {
            float lineHeight = obj.LineHeight == 0 ? obj.Font.GetHeight() : obj.LineHeight;
            lineHeight *= FDpiFX;
            float curY = obj.AbsTop + lineHeight;
            float bottom = obj.AbsBottom;
            float left = obj.AbsLeft;
            float right = obj.AbsRight;
            float width = obj.Border.Width;
            while (curY < bottom)
            {
                DrawLine(left, curY, right, curY, obj.Border.Color, width);
                curY += lineHeight;
            }
        }
        #endregion
        private string DrawGlass(GlassFill fill) //need add hashtable for unique gradients
        {
            Color colorTop = GetBlendColor(fill.Color, fill.Blend);
            if (defs == null)
            {
                defs = doc.CreateElement("defs");
                g.AppendChild(defs);
            }
            XmlElement linearGradient = doc.CreateElement("linearGradient");
            AppndAttr(linearGradient, "id", "grad" + Convert.ToString(gradNbr));
            AppndAttr(linearGradient, "x1", "0%");
            AppndAttr(linearGradient, "x2", "0%");
            AppndAttr(linearGradient, "y1", "100%");
            AppndAttr(linearGradient, "y2", "0%");
            defs.AppendChild(linearGradient);

            XmlElement stop = doc.CreateElement("stop");
            AppndAttr(stop, "offset", "50%");
            AppndAttr(stop, "stop-color", ExportUtils.HTMLColorCode(colorTop));
            AppndAttr(stop, "stop-opacity", "1");
            linearGradient.AppendChild(stop);

            stop = doc.CreateElement("stop");
            AppndAttr(stop, "offset", "50%");
            AppndAttr(stop, "stop-color", ExportUtils.HTMLColorCode(fill.Color));
            AppndAttr(stop, "stop-opacity", "1");
            linearGradient.AppendChild(stop);

            string res = "url(#grad" + Convert.ToString(gradNbr) + ")";
            gradNbr++;
            return res;
        }
    }
}