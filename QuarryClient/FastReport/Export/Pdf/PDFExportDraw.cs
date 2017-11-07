using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Export.TTF;
using System.Drawing.Drawing2D;

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        const float KAPPA1 = 1.5522847498f;
        const float KAPPA2 = 2 - KAPPA1;

        private void DrawPDFUnderline(int fontNumber, Font font, float x, float y, float width, float offsX, Color color, bool transformNeeded, StringBuilder sb)
        {
                ExportTTFFont pdfFont = FPageFonts[fontNumber];
                x = (transformNeeded ? x * PDF_DIVIDER : GetLeft(x)) + offsX;
                y = transformNeeded ? -y * PDF_DIVIDER : GetTop(y);
                float factor = PDF_TTF_DIVIDER * font.Size * FDpiFX * PDF_DIVIDER;
                float uh = GetBaseline(font) * PDF_DIVIDER - pdfFont.TextMetric.otmsUnderscorePosition * factor;
                DrawPDFLine(x, y - uh, x + width * PDF_DIVIDER, y - uh, color, pdfFont.TextMetric.otmsUnderscoreSize * factor, LineStyle.Solid, null, null, sb);
        }

        private void DrawPDFStrikeout(int fontNumber, Font font, float x, float y, float width, float offsX, Color color, bool transformNeeded, StringBuilder sb)
        {
            ExportTTFFont pdfFont = FPageFonts[fontNumber];
            x = (transformNeeded ? x * PDF_DIVIDER : GetLeft(x)) + offsX;
            y = transformNeeded ? -y * PDF_DIVIDER : GetTop(y);
            float factor = PDF_TTF_DIVIDER * font.Size * FDpiFX * PDF_DIVIDER;
            float uh = GetBaseline(font) * PDF_DIVIDER - pdfFont.TextMetric.otmsStrikeoutPosition * factor;
            DrawPDFLine(x, y - uh, x + width * PDF_DIVIDER, y - uh, color, pdfFont.TextMetric.otmsStrikeoutSize * factor, LineStyle.Solid, null, null, sb);
        }

        private void DrawPDFRect(float left, float top, float right, float bottom, Color color, float borderWidth, LineStyle lineStyle, StringBuilder sb)
        {
            if (lineStyle == LineStyle.Solid)
            {
                //старая функция
                GetPDFStrokeColor(color, sb);
                sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("2 J");
                sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
                sb.Append(FloatToString(left)).Append(" ").
                    AppendLine(FloatToString(top)).
                    Append(FloatToString(right - left)).Append(" ").
                    Append(FloatToString(bottom - top)).AppendLine(" re").AppendLine("S");
                return;
            }
            //рисуем прямоугольник по линиям!
            DrawPDFLine(left, top, right, top, color, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(right, top, right, bottom, color, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(right, bottom, left, bottom, color, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(left, bottom, left, top, color, borderWidth, lineStyle, null, null, sb);

        }

        private void DrawPDFFillRect(float Left, float Top, float Width, float Height, FillBase fill, StringBuilder sb)
        {
            if (fill is SolidFill && (fill as SolidFill).Color.A != 0)
            {
                GetPDFFillColor((fill as SolidFill).Color, sb);
                sb.Append(FloatToString(Left)).Append(" ").
                    Append(FloatToString(Top - Height)).Append(" ").
                    Append(FloatToString(Width)).Append(" ").
                    Append(FloatToString(Height)).AppendLine(" re");
                sb.AppendLine("f");
            }
            else if (fill is GlassFill)
            {
                GetPDFFillColor((fill as GlassFill).Color, sb);
                sb.Append(FloatToString(Left)).Append(" ").
                    Append(FloatToString(Top - Height)).Append(" ").
                    Append(FloatToString(Width)).Append(" ").
                    Append(FloatToString(Height / 2)).AppendLine(" re");
                sb.AppendLine("f");
                Color c = (fill as GlassFill).Color;
                c = Color.FromArgb(255, (int)Math.Round(c.R + (255 - c.R) * (fill as GlassFill).Blend),
                    (int)Math.Round(c.G + (255 - c.G) * (fill as GlassFill).Blend),
                    (int)Math.Round(c.B + (255 - c.B) * (fill as GlassFill).Blend));
                GetPDFFillColor(c, sb);
                sb.Append(FloatToString(Left)).Append(" ").
                    Append(FloatToString(Top - Height / 2)).Append(" ").
                    Append(FloatToString(Width)).Append(" ").
                    Append(FloatToString(Height / 2)).AppendLine(" re");
                sb.AppendLine("f");
            }
        }

        private void DrawPDFTriangle(float left, float top, float width, float height, Color fillColor, Color borderColor, float borderWidth, LineStyle lineStyle, StringBuilder sb)
        {
            PointF point1 = new PointF(left + width / 2, top);
            PointF point2 = new PointF(left + width, top - height);
            PointF point3 = new PointF(left, top - height);

            if (lineStyle == LineStyle.Solid)
            {
                if (fillColor.A != 0)
                    GetPDFFillColor(fillColor, sb);
                if (borderColor.A != 0)
                    GetPDFStrokeColor(borderColor, sb);
                sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
                sb.Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).Append(" m ").
                    Append(FloatToString(point2.X)).Append(" ").Append(FloatToString(point2.Y)).Append(" l ").
                    Append(FloatToString(point3.X)).Append(" ").Append(FloatToString(point3.Y)).Append(" l ").
                    Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).AppendLine(" l");
                if (fillColor.A == 0)
                    sb.AppendLine("S");
                else
                    sb.AppendLine("B");
                return;
            }

            if (fillColor.A != 0)
            {
                GetPDFFillColor(fillColor, sb);
                //sb = GetPDFStrokeColor(fillColor, sb);
                //sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                //sb.AppendLine(DrawPDFDash(LineStyle.Solid, borderWidth));
                sb.Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).Append(" m ").
                    Append(FloatToString(point2.X)).Append(" ").Append(FloatToString(point2.Y)).Append(" l ").
                    Append(FloatToString(point3.X)).Append(" ").Append(FloatToString(point3.Y)).Append(" l ").
                    Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).AppendLine(" l");
                sb.AppendLine("F");
            }
            DrawPDFLine(point1.X, point1.Y, point2.X, point2.Y, borderColor, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(point2.X, point2.Y, point3.X, point3.Y, borderColor, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(point3.X, point3.Y, point1.X, point1.Y, borderColor, borderWidth, lineStyle, null, null, sb);
        }

        private void DrawPDFDiamond(float left, float top, float width, float height, Color fillColor, Color borderColor, float borderWidth, LineStyle lineStyle, StringBuilder sb)
        {
            PointF point1 = new PointF(left + width / 2, top);
            PointF point2 = new PointF(left + width, top - height / 2);
            PointF point3 = new PointF(left + width / 2, top - height);
            PointF point4 = new PointF(left, top - height / 2);

            if (lineStyle == LineStyle.Solid)
            {
                //old function
                if (fillColor.A != 0)
                    GetPDFFillColor(fillColor, sb);
                if (borderColor.A != 0)
                    GetPDFStrokeColor(borderColor, sb);
                sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
                sb.Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).Append(" m ").
                    Append(FloatToString(point2.X)).Append(" ").Append(FloatToString(point2.Y)).Append(" l ").
                    Append(FloatToString(point3.X)).Append(" ").Append(FloatToString(point3.Y)).Append(" l ").
                    Append(FloatToString(point4.X)).Append(" ").Append(FloatToString(point4.Y)).Append(" l ").
                    Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).AppendLine(" l");
                if (fillColor.A == 0)
                    sb.AppendLine("S");
                else
                    sb.AppendLine("B");
                return;
            }
            //linestile != solid
            if (fillColor.A != 0)
            {
                GetPDFFillColor(fillColor, sb);
                //sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                //sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
                sb.Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).Append(" m ").
                    Append(FloatToString(point2.X)).Append(" ").Append(FloatToString(point2.Y)).Append(" l ").
                    Append(FloatToString(point3.X)).Append(" ").Append(FloatToString(point3.Y)).Append(" l ").
                    Append(FloatToString(point4.X)).Append(" ").Append(FloatToString(point4.Y)).Append(" l ").
                    Append(FloatToString(point1.X)).Append(" ").Append(FloatToString(point1.Y)).AppendLine(" l");
                sb.AppendLine("F");
            }

            DrawPDFLine(point1.X, point1.Y, point2.X, point2.Y, borderColor, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(point2.X, point2.Y, point3.X, point3.Y, borderColor, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(point3.X, point3.Y, point4.X, point4.Y, borderColor, borderWidth, lineStyle, null, null, sb);
            DrawPDFLine(point4.X, point4.Y, point1.X, point1.Y, borderColor, borderWidth, lineStyle, null, null, sb);

        }

        private void DrawPDFEllipse(float left, float top, float width, float height, Color fillColor, Color borderColor, float borderWidth, LineStyle lineStyle, StringBuilder sb)
        {
            if (fillColor.A != 0)
                GetPDFFillColor(fillColor, sb);
            if (borderColor.A != 0)
                GetPDFStrokeColor(borderColor, sb);
            sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w");
            sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
            float rx = width / 2;
            float ry = height / 2;
            sb.Append(FloatToString(left + width)).Append(" ").Append(FloatToString(top - ry)).AppendLine(" m");
            sb.Append(FloatToString(left + width)).Append(" ").Append(FloatToString(top - ry * KAPPA1)).Append(" ").
                Append(FloatToString(left + rx * KAPPA1)).Append(" ").Append(FloatToString(top - height)).Append(" ").
                Append(FloatToString(left + rx)).Append(" ").Append(FloatToString(top - height)).AppendLine(" c");
            sb.Append(FloatToString(left + rx * KAPPA2)).Append(" ").Append(FloatToString(top - height)).Append(" ").
                Append(FloatToString(left)).Append(" ").Append(FloatToString(top - ry * KAPPA1)).Append(" ").
                Append(FloatToString(left)).Append(" ").Append(FloatToString(top - ry)).AppendLine(" c");
            sb.Append(FloatToString(left)).Append(" ").Append(FloatToString(top - ry * KAPPA2)).Append(" ").
                Append(FloatToString(left + rx * KAPPA2)).Append(" ").Append(FloatToString(top)).Append(" ").
                Append(FloatToString(left + rx)).Append(" ").Append(FloatToString(top)).AppendLine(" c");
            sb.Append(FloatToString(left + rx * KAPPA1)).Append(" ").Append(FloatToString(top)).Append(" ").
                Append(FloatToString(left + width)).Append(" ").Append(FloatToString(top - ry * KAPPA2)).Append(" ").
                Append(FloatToString(left + width)).Append(" ").Append(FloatToString(top - ry)).AppendLine(" c");
            if (fillColor.A == 0)
                sb.AppendLine("S");
            else
                sb.AppendLine("B");
        }

        private void DrawPDFLine(float left, float top, float right, float bottom, Color color, float width,
            LineStyle lineStyle, CapSettings startCap, CapSettings endCap, StringBuilder sb)
        {
            if (!(color.A == 0 || width == 0.0f))// && (lineStyle == LineStyle.Dash || lineStyle == LineStyle.Dot)))
            {
                GetPDFStrokeColor(color, sb);
                //sb.AppendLine(DrawPDFDash(lineStyle, width));
                sb.Append(DrawStyledLine(left, top, right, bottom, lineStyle, width));
                if (startCap != null && startCap.Style == CapStyle.Arrow)
                    sb.Append(DrawArrow(startCap, width, right, bottom, left, top));
                if (endCap != null && endCap.Style == CapStyle.Arrow)
                    sb.Append(DrawArrow(endCap, width, left, top, right, bottom));
            }
        }

        private void DrawPDFPolyLine(float left, float top, float right, float bottom, float centerX, float centerY, PointF[] points, bool isClosed, Color borderColor, float borderWidth, LineStyle lineStyle, StringBuilder sb)
        {
            if (lineStyle == LineStyle.Solid)
            {
                if (borderColor.A != 0)
                    GetPDFStrokeColor(borderColor, sb);
                sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
                bool isFirst = true;
                foreach (PointF point in points)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" m ");
                    }
                    else
                    {
                        sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" l ");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
                if (isClosed)
                    sb.AppendLine("s");
                else
                    sb.AppendLine("S");
            }
            else
            {
                for (int i = 0; i < points.Length - 1; i++)
                    DrawPDFLine(
                        left + (points[i].X + centerX) * PDF_DIVIDER,
                        top - (points[i].Y + centerY) * PDF_DIVIDER,
                        left + (points[i + 1].X + centerX) * PDF_DIVIDER,
                        top - (points[i + 1].Y + centerY) * PDF_DIVIDER,
                        borderColor, borderWidth, lineStyle, null, null, sb);
                if (isClosed)
                    DrawPDFLine(left + (points[points.Length - 1].X + centerX) * PDF_DIVIDER, top - (points[points.Length - 1].Y + centerY) * PDF_DIVIDER, left + (points[0].X + centerX) * PDF_DIVIDER, top - (points[0].Y + centerY) * PDF_DIVIDER, borderColor, borderWidth, lineStyle, null, null, sb);
            }
        }

        private void DrawPDFPolygon(float left, float top, float right, float bottom, float centerX, float centerY, PointF[] points, Color fillColor, Color borderColor, float borderWidth, LineStyle borderStyle, StringBuilder sb)
        {
            if (fillColor.A == 0)
            {
                DrawPDFPolyLine(left, top, right, bottom, centerX, centerY, points, true, borderColor, borderWidth, borderStyle, sb);
            }
            else
            {
                if (borderStyle == LineStyle.Solid)
                {
                    GetPDFFillColor(fillColor, sb);
                    if (borderColor.A != 0)
                        GetPDFStrokeColor(borderColor, sb);
                    sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                    sb.AppendLine(DrawPDFDash(borderStyle, borderWidth));
                    bool isFirst = true;
                    foreach (PointF point in points)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" m ");
                        }
                        else
                        {
                            sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" l ");
                        }
                    }
                    sb.Append(FloatToString(left + (points[0].X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (points[0].Y + centerY) * PDF_DIVIDER)).Append(" l");
                    sb.AppendLine();
                    sb.AppendLine("B");
                }
                else
                {
                    GetPDFFillColor(fillColor, sb);
                    //sb = GetPDFStrokeColor(borderColor, sb);
                    sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
                    sb.AppendLine(DrawPDFDash(borderStyle, borderWidth));
                    bool isFirst = true;
                    foreach (PointF point in points)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" m ");
                        }
                        else
                        {
                            sb.Append(FloatToString(left + (point.X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (point.Y + centerY) * PDF_DIVIDER)).Append(" l ");
                        }
                    }
                    sb.Append(FloatToString(left + (points[0].X + centerX) * PDF_DIVIDER)).Append(" ").Append(FloatToString(top - (points[0].Y + centerY) * PDF_DIVIDER)).Append(" l");
                    sb.AppendLine();
                    sb.AppendLine("f");
                    DrawPDFPolyLine(left, top, right, bottom, centerX, centerY, points, true, borderColor, borderWidth, borderStyle, sb);
                }
            }
        }

        private string DrawStyledLine(float left, float top, float right, float bottom, LineStyle lineStyle, float width)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FloatToString(width)).AppendLine(" w").AppendLine("0 J");

            switch (lineStyle)
            {
                case LineStyle.Dash:
                    sb.Append(DrawDashedLine(left, top, right, bottom, width, "LS"));
                    break;
                case LineStyle.DashDot:
                    sb.Append(DrawDashedLine(left, top, right, bottom, width, "LSPS"));
                    break;
                case LineStyle.DashDotDot:
                    sb.Append(DrawDashedLine(left, top, right, bottom, width, "LSPSPS"));
                    break;
                case LineStyle.Dot:
                    sb.Append(DrawDashedLine(left, top, right, bottom, width, "PS"));
                    break;
                case LineStyle.Double:
                    sb.Append(DrawLine(left, top, right, bottom));
                    break;
                default:
                    sb.Append(DrawLine(left, top, right, bottom));
                    break;
            }

            return sb.ToString();
        }

        // <param name="pattern">L - line, S - space, P - point</param>
        private string DrawDashedLine(float left, float top, float right, float bottom, float linewidth, string pattern)
        {
            StringBuilder sb = new StringBuilder();
            float dash = linewidth * 3.0f;
            float dot = linewidth;
            int currentState = 0;
            bool horizLine = false;
            bool vertLine = false;
            float length = 0, alfa = 0;
            if (left == right)  // vertical line
            {
                vertLine = true;
                if (bottom < top)
                {
                    dash = -dash;
                    dot = -dot;
                }
                length = bottom - top;
            }
            else if (top == bottom)  //  horiz line
            {
                horizLine = true;
                if (right < left)
                {
                    dash = -dash;
                    dot = -dot;
                }
                length = right - left;
            }
            else
            {
                // diag
                length = (float)Math.Sqrt(Math.Pow(right - left, 2) + Math.Pow(bottom - top, 2));
                alfa = (float)Math.Asin((right - left) / length);
            }
            float current = 0, currentX = 0, currentY = 0;
            float delta, deltaX = 0, deltaY = 0;
            bool penDown;
            while (Math.Abs(current) < Math.Abs(length))
            {
                char state = pattern[currentState++];
                if (currentState >= pattern.Length)
                    currentState = 0;
                switch (state)
                {
                    case 'L': // line
                        delta = dash;
                        penDown = true;
                        break;
                    case 'P': // point
                        delta = dot;
                        penDown = true;
                        break;
                    default:  // space
                        delta = dot;
                        penDown = false;
                        break;
                }
                current += delta;
                if (alfa != 0)
                {
                    deltaX = (float)(delta * Math.Sin(alfa));
                    deltaY = (float)(delta * Math.Cos(alfa)) * Math.Sign(bottom - top);
                    currentX += deltaX;
                    currentY += deltaY;
                }
                if (penDown)
                {
                    if (Math.Abs(current) < Math.Abs(length))
                    {
                        if (vertLine)
                            sb.Append(DrawLine(left, top + current - delta, right, top + current));
                        else if (horizLine)
                            sb.Append(DrawLine(left + current - delta, top, left + current, bottom));
                        else
                            sb.Append(DrawLine(left + currentX - deltaX, top + currentY - deltaY, left + currentX, top + currentY));
                    }
                    else
                    {
                        if (vertLine)
                            sb.Append(DrawLine(left, top + current - delta, right, bottom));
                        else if (horizLine)
                            sb.Append(DrawLine(left + current - delta, top, right, bottom));
                        else
                            sb.Append(DrawLine(left + currentX - deltaX, top + currentY - deltaY, right, bottom));
                    }
                }

            }
            return sb.ToString();
        }

        private string DrawLine(float left, float top, float right, float bottom)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FloatToString(left)).Append(" ").
                Append(FloatToString(top)).AppendLine(" m").
                Append(FloatToString(right)).Append(" ").
                Append(FloatToString(bottom)).AppendLine(" l").
                AppendLine("S");
            return sb.ToString();
        }

        private string DrawArrow(CapSettings Arrow, float lineWidth, float x1, float y1, float x2, float y2)
        {
            float k1, a, b, c, d;
            float xp, yp, x3, y3, x4, y4;
            float wd = Arrow.Width * lineWidth * PDF_DIVIDER;
            float ld = Arrow.Height * lineWidth * PDF_DIVIDER;
            if (Math.Abs(x2 - x1) > 0)
            {
                k1 = (y2 - y1) / (x2 - x1);
                a = (float)Math.Pow(k1, 2) + 1;
                b = 2 * (k1 * ((x2 * y1 - x1 * y2) / (x2 - x1) - y2) - x2);
                c = (float)Math.Pow(x2, 2) + (float)Math.Pow(y2, 2) - (float)Math.Pow(ld, 2) +
                    (float)Math.Pow((x2 * y1 - x1 * y2) / (x2 - x1), 2) -
                    2 * y2 * (x2 * y1 - x1 * y2) / (x2 - x1);
                d = (float)Math.Pow(b, 2) - 4 * a * c;
                xp = (-b + (float)Math.Sqrt(d)) / (2 * a);
                if ((xp > x1) && (xp > x2) || (xp < x1) && (xp < x2))
                    xp = (-b - (float)Math.Sqrt(d)) / (2 * a);
                yp = xp * k1 + (x2 * y1 - x1 * y2) / (x2 - x1);
                if (y2 != y1)
                {
                    x3 = xp + wd * (float)Math.Sin(Math.Atan(k1));
                    y3 = yp - wd * (float)Math.Cos(Math.Atan(k1));
                    x4 = xp - wd * (float)Math.Sin(Math.Atan(k1));
                    y4 = yp + wd * (float)Math.Cos(Math.Atan(k1));
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
            StringBuilder result = new StringBuilder(64);
            result.AppendLine("2 J").AppendLine("[] 0 d").Append(FloatToString(x3)).Append(" ").Append(FloatToString(y3)).AppendLine(" m").
                Append(FloatToString(x2)).Append(" ").Append(FloatToString(y2)).AppendLine(" l").
                Append(FloatToString(x4)).Append(" ").Append(FloatToString(y4)).AppendLine(" l").AppendLine("S");
            return result.ToString();
        }


        private string DrawPDFDash(LineStyle lineStyle, float lineWidth)
        {
            if (lineStyle == LineStyle.Solid)
                return "[] 0 d";
            else
            {
                string dash = FloatToString(lineWidth * 2.0f) + " ";
                string dot = FloatToString(lineWidth * 0.05f) + " ";
                StringBuilder result = new StringBuilder(64);
                switch (lineStyle)
                {
                    case LineStyle.Dash:
                        result.Append(dash);
                        break;
                    case LineStyle.DashDot:
                        result.Append(dash).Append(dash).Append(dot).Append(dash);
                        break;
                    case LineStyle.DashDotDot:
                        result.Append(dash).Append(dash).Append(dot).Append(dash).Append(dot).Append(dash);
                        break;
                    case LineStyle.Dot:
                        result.Append(dot).Append(dash);
                        break;
                }
                return String.Format("[{0}] 0 d", result.ToString());
            }
        }


        private void DrawPDFBorder(Border Border, float left, float top, float width, float height, StringBuilder sb)
        {
            if (Border.Shadow)
            {
                DrawPDFFillRect(GetLeft(left + width),
                    GetTop(top + Border.ShadowWidth),
                    Border.ShadowWidth * PDF_DIVIDER,
                    height * PDF_DIVIDER,
                    new SolidFill(Border.ShadowColor), sb);
                DrawPDFFillRect(GetLeft(left + Border.ShadowWidth),
                    GetTop(top + height),
                    (width - Border.ShadowWidth) * PDF_DIVIDER,
                    Border.ShadowWidth * PDF_DIVIDER,
                    new SolidFill(Border.ShadowColor), sb);
            }
            if (Border.Lines != BorderLines.None)
            {
                if (Border.Lines == BorderLines.All &&
                    Border.LeftLine.Equals(Border.RightLine) &&
                    Border.TopLine.Equals(Border.BottomLine) &&
                    Border.LeftLine.Equals(Border.TopLine))
                {
                    if (Border.Width > 0 && Border.Color.A != 0)
                    {
                        DrawPDFRect(GetLeft(left), GetTop(top),
                            GetLeft(left + width), GetTop(top + height),
                            Border.Color, Border.Width * PDF_DIVIDER,
                            Border.Style, sb);

                        if (Border.LeftLine.Style == LineStyle.Double)
                            DrawPDFRect(GetLeft(left + 2), GetTop(top + 2),
                                GetLeft(left + width - 2), GetTop(top + height - 2),
                                Border.Color, Border.Width * PDF_DIVIDER,
                                Border.Style, sb);
                    }
                }
                else
                {
                    float Left = GetLeft(left);
                    float Top = GetTop(top);
                    float Right = GetLeft(left + width);
                    float Bottom = GetTop(top + height);
                    Top -= 0.1f;
                    Bottom += 0.1f;

                    if ((Border.Lines & BorderLines.Left) > 0)
                    {
                        DrawPDFLine(Left, Top, Left, Bottom, Border.LeftLine.Color,
                            Border.LeftLine.Width * PDF_DIVIDER, Border.LeftLine.Style, null, null, sb);

                        if (Border.LeftLine.Style == LineStyle.Double)
                            DrawPDFLine(Left + 2, Top, Left + 2, Bottom, Border.LeftLine.Color,
                                Border.LeftLine.Width * PDF_DIVIDER, Border.LeftLine.Style, null, null, sb);
                    }
                    if ((Border.Lines & BorderLines.Right) > 0)
                    {
                        DrawPDFLine(Right, Top, Right, Bottom, Border.RightLine.Color,
                            Border.RightLine.Width * PDF_DIVIDER, Border.RightLine.Style, null, null, sb);

                        if (Border.RightLine.Style == LineStyle.Double)
                            DrawPDFLine(Right - 2, Top, Right - 2, Bottom, Border.RightLine.Color,
                                Border.RightLine.Width * PDF_DIVIDER, Border.RightLine.Style, null, null, sb);
                    }
                    Top += 0.1f;
                    Bottom -= 0.1f;
                    Left += 0.1f;
                    Right -= 0.1f;
                    if ((Border.Lines & BorderLines.Top) > 0)
                    {
                        DrawPDFLine(Left, Top, Right, Top, Border.TopLine.Color,
                            Border.TopLine.Width * PDF_DIVIDER, Border.TopLine.Style, null, null, sb);

                        if (Border.TopLine.Style == LineStyle.Double)
                            DrawPDFLine(Left, Top - 2, Right, Top - 2, Border.TopLine.Color,
                                Border.TopLine.Width * PDF_DIVIDER, Border.TopLine.Style, null, null, sb);
                    }
                    if ((Border.Lines & BorderLines.Bottom) > 0)
                    {
                        DrawPDFLine(Left, Bottom, Right, Bottom, Border.BottomLine.Color,
                            Border.BottomLine.Width * PDF_DIVIDER, Border.BottomLine.Style, null, null, sb);

                        if (Border.BottomLine.Style == LineStyle.Double)
                            DrawPDFLine(Left, Bottom + 2, Right, Bottom + 2, Border.BottomLine.Color,
                                    Border.BottomLine.Width * PDF_DIVIDER, Border.BottomLine.Style, null, null, sb);
                    }
                }
            }
        }

        private void DrawPDFPolygonChar(GraphicsPath p, float x, float y, Color c, StringBuilder sb)
        {
            if (p.PointCount == 0) return;
            GetPDFFillColor(c, sb);
            PointF[] ps = p.PathPoints;
            byte[] pt = p.PathTypes;
            for (int i = 0; i < p.PointCount;)
            {
                switch (pt[i])
                {
                    case 0://start
                        sb.AppendLine();
                        sb.Append(FloatToString(x + ps[i].X * PDF_DIVIDER)).Append(" ").Append(FloatToString(y - ps[i].Y * PDF_DIVIDER)).Append(" m ");
                        i++;
                        break;
                    case 1://line
                        sb.Append(FloatToString(x + ps[i].X * PDF_DIVIDER)).Append(" ").Append(FloatToString(y - ps[i].Y * PDF_DIVIDER)).Append(" l ");
                        i++;
                        break;
                    case 3://interpolate bezier
                        for (float dt = 1; dt < 6; dt++)
                            DrawPDFBezier(x,y,ps[i - 1], ps[i], ps[i + 1], ps[i + 2], dt / 5, sb);
                        i += 3;
                        break;
                    default:
                        i++;
                        break;
                }
                //fill
            }
            sb.AppendLine();
            sb.AppendLine("f");
        }

        private void DrawPDFPolygonCharOutline(GraphicsPath p, float x, float y, Color c, float borderWidth, StringBuilder sb)
        {
            if (p.PointCount == 0) return;
            GetPDFStrokeColor(c, sb);
            sb.Append(FloatToString(borderWidth * PDF_DIVIDER)).AppendLine(" w").AppendLine("1 J");
            //sb.AppendLine(DrawPDFDash(lineStyle, borderWidth));
            PointF[] ps = p.PathPoints;
            byte[] pt = p.PathTypes;
            for (int i = 0; i < p.PointCount;)
            {
                switch (pt[i])
                {
                    case 0://start
                        sb.AppendLine();
                        sb.Append(FloatToString(x + ps[i].X * PDF_DIVIDER)).Append(" ").Append(FloatToString(y - ps[i].Y * PDF_DIVIDER)).Append(" m ");
                        i++;
                        break;
                    case 1://line
                        sb.Append(FloatToString(x + ps[i].X * PDF_DIVIDER)).Append(" ").Append(FloatToString(y - ps[i].Y * PDF_DIVIDER)).Append(" l ");
                        i++;
                        break;
                    case 3://interpolate bezier
                        for (float dt = 1; dt < 6; dt++)
                            DrawPDFBezier(x, y, ps[i - 1], ps[i], ps[i + 1], ps[i + 2], dt / 5, sb);
                        i += 3;
                        break;
                    default:
                        i++;
                        break;
                }
                //fill
            }
            sb.AppendLine();
            sb.AppendLine("S");
        }

        private void DrawPDFBezier( float x, float y, PointF p0, PointF p1, PointF p2, PointF p3, float t, StringBuilder sb)
        {
            float t1 = 1 - t;
            float px = t1*t1*t1*p0.X+ 3*t1*t1*t*p1.X+3*t*t*t1*p2.X+t*t*t*p3.X;
            float py = t1 * t1 * t1 * p0.Y + 3 * t1 * t1 * t * p1.Y + 3 * t * t * t1 * p2.Y + t * t * t * p3.Y;
            sb.Append(FloatToString(x + px * PDF_DIVIDER)).Append(" ").Append(FloatToString(y - py * PDF_DIVIDER)).Append(" l ");
        }
    }
}