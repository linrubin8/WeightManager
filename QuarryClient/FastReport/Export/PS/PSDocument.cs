using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using FastReport.Export.TTF;

namespace FastReport.Export.PS
{
    /// <summary>
    /// Contains Dashes enum
    /// </summary>
    public enum Dashes
    {
        /// <summary>
        /// Specifies the Dash.
        /// </summary>
        Dash,

        /// <summary>
        /// Specifies the Dot.
        /// </summary>
        Dot,

        /// <summary>
        /// Specifies the DashDot.
        /// </summary>
        DashDot,

        /// <summary>
        /// Specifies the DashDotDot.
        /// </summary>
        DashDotDot,

        /// <summary>
        /// Specifies the Double line.
        /// </summary>
        Double
    }
    class PSDocument
    {
        protected float WindowHeight;
        protected float WindowWidth;
        protected StringBuilder PS_DATA = new StringBuilder();
        protected bool FTextInCurves = false;

        internal bool TextInCurves
        {
            get { return FTextInCurves; }
            set { FTextInCurves = value; }
        }
        /// <summary>
        /// Create Window.
        /// </summary>
        public void CreateWindow(string name, float Width, float Height)
        {
            PS_DATA.Append("%!PS-Adobe\n" +
                "%%Title: postscriptdoc\n" +
                "%%Creator: FastReport\n" +
                "%%BoundingBox:   0   0   595   842\n" + 
                "%% Pages: (atend)\n" + 
                "%% DocumentFonts:\n" +
                "%% EndComments\n" +
                "%% EndProlog\n" +
                "%% Page: 1 1\n");
            WindowHeight = Height;
            WindowWidth = Width;
            if (Math.Round(Width) == 595 && Math.Round(Height) == 842) PS_DATA.Append("a4 ");
        }

        protected List<float> TextAlignments(float x, ref float y, string HorizontalAlignment, string VerticalAlignment, float Width, float Height, Font font, List<string> txt_lns,
            float PaddingLeft, float PaddingRight, float PaddingTop, float PaddingBottom, float BorderThikness)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

           float Xold = x;
           float Yold = y;
            int N = txt_lns.Count;
            List<float> x_alignments = new List<float>();
            foreach (string line in txt_lns)
            {
                if (HorizontalAlignment == "Center")
                {
                    x_alignments.Add(x + Width / 2 - objGraphics.MeasureString(line, font).Width / 2f * 0.75f);
                }
                else if (HorizontalAlignment == "Right")
                {
                    x_alignments.Add(x + Width - objGraphics.MeasureString(line, font).Width * 0.75f);
                }
                else if (HorizontalAlignment == "Left") x_alignments.Add(x);
                else if (HorizontalAlignment == "Justify") x_alignments.Add(x);
            }
            
            if (VerticalAlignment == "Center")
            {
                y = y - Height / 2f + ((float)font.Size)/2;
                if (N > 1) y -= ((float)font.Size) / 2 * N;
            }
            if (VerticalAlignment == "Top")
            {
                y = y - Height + (float)font.Size;
            }
            if (VerticalAlignment == "Bottom")
            {
                if (N > 1) y = y - ((float)font.Size) * N;
            }
            //Paddings
            if (Yold - Height + (float)font.Size + PaddingTop > y)
            {
                y = Yold - Height + (float)font.Size + PaddingTop;
            }
            if (Yold - PaddingBottom < y)
            {
                y = Yold - PaddingBottom;
            }
            for (int i = 0; i < x_alignments.Count; i++ )
            {
                if (Xold + PaddingLeft > x_alignments[i])
                {
                    x_alignments[i] = Xold + PaddingLeft;
                }
                if (Xold + Width - PaddingRight < x_alignments[i])
                {
                    x_alignments[i] = Xold + Width - PaddingRight;
                }
            }
            return x_alignments;
        }

        private bool gBorderLines(string BorderLines, out bool None, out bool Left, out bool Right,
            out bool Top, out bool Bottom)
        {
            string[] masLines = BorderLines.Split(',', ' ');//names lines borders 
            bool All = false;
             None = false;
             Left = false;
             Right = false;
             Top = false;
             Bottom = false;
            for (int i = 0; i < masLines.Length; i++)
            {
                if (masLines[i] == "All")
                {
                    All = true;
                }
                if (masLines[i] == "None")
                {
                    None = true;
                }
                if (masLines[i] == "Left")
                {
                    Left = true;
                }
                if (masLines[i] == "Right")
                {
                    Right = true;
                }
                if (masLines[i] == "Top")
                {
                    Top = true;
                }
                if (masLines[i] == "Bottom")
                {
                    Bottom = true;
                }
            }
            return All;
        }

        private void createText(float x, float y, string HorizontalAlignment, string VerticalAlignment, float Width, float Height, Font font, string textstr,
            float PaddingLeft, float PaddingRight, float PaddingTop, float PaddingBottom, float BorderThickness, string Foreground, string Background, float Angle)
        {
            List<float> t_x; float t_y = y;
            
            Bitmap objBmpImage = new Bitmap(1, 1);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            List<string> txt_lns = new List<string>();
            string[] words = textstr.Split(' ');
         string line = "";
         for (int n = 0; n < words.Length; n++) {
             string testLine = line + words[n];
             if ((float)objGraphics.MeasureString(testLine, font).Width * 0.75f > Width)
             {
                 txt_lns.Add(line);
                 line = words[n] + " ";
             }
             else {
                    if (words[n].Contains("\n") || words[n].Contains("\r"))
                    {
                        int rn = testLine.IndexOf('\r') != -1 ? testLine.IndexOf('\r') : testLine.IndexOf('\n');
                        line = testLine.Remove(rn);
                        txt_lns.Add(line);
                        line = testLine.Remove(0, rn).Replace("\r","").Replace("\n", "") + " ";
                    }
                    else
                        line = testLine + " ";
             }
         }
         if (txt_lns.Count == 0) 
         txt_lns.Add(line);
         else if (txt_lns[txt_lns.Count - 1] != line) 
             txt_lns.Add(line);

            t_x = TextAlignments(x, ref t_y, HorizontalAlignment, VerticalAlignment, Width, Height, font, txt_lns, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom, BorderThickness);

            if(FTextInCurves)
            AddCurveTextLine(t_x, t_y, Foreground, Background, font, Width, Height, txt_lns, Angle);
            else
                AddTextLine(t_x, t_y, Foreground, font, Width, Height, txt_lns, Angle);
        }

        private void AddTextLine(List<float> t_x, float t_y, string Foreground, Font font, float Width, float Height, List<string> txt_lns, float Angle)
        {
            bool fstart = true;
            string internal_data = "";
            string gsave;
            string coords;
            float cur_y = 0f;

            for (int i = 0; i < txt_lns.Count; i++)
            {
                string text_col = ColorToPsRgb(Foreground);

                if (Angle == 0)
                    internal_data = "/" + PSFont(font.Name) + " findfont " + FloatToString(font.Size) + " scalefont setfont " + FloatToString(t_x[i]) + " "
                    + FloatToString(WindowHeight - t_y - Height) + " moveto " + text_col + " setrgbcolor (" + txt_lns[i] + ") show ";
                else
                {
                    if (Angle <= 90)
                    {
                        t_x[i] += Width / 2;
                        t_y -= Height / 2;
                    }

                    if (fstart)
                    {
                        gsave = " gsave "; fstart = false;
                        coords = FloatToString(t_x[i]) + " " + FloatToString(WindowHeight - t_y - Height) + " translate 0 0 moveto " + -Angle + " rotate ";
                    }
                    else
                    {
                        gsave = "";
                        coords = FloatToString(t_x[i] - t_x[0]) + " " + FloatToString(cur_y) + " moveto ";
                    }
                    cur_y -= font.GetHeight() * 0.75f;
                    internal_data = gsave + "/" + font.Name + " findfont " + FloatToString(font.Size) + " scalefont setfont " +
                    coords + text_col + " setrgbcolor (" + txt_lns[i] + ") show ";
                    if (i == txt_lns.Count - 1) internal_data += "grestore ";
                }
                PS_DATA.Append(internal_data + " ");
                t_y += font.GetHeight() * 0.75f;
            }         
        }
        /// Add TextLine in curves
        private void AddCurveTextLine(List<float> t_x, float t_y, string Foreground, string Background, Font font, float Width, float Height, List<string> txt_lns, float Angle)
        {
            ExportTTFFont pdffont = new ExportTTFFont(font);
            float paddingX;
            float paddingY;
            float cur_y = t_y + Height;

            for (int i = 0; i < txt_lns.Count; i++)
            {
                ExportTTFFont.GlyphTTF[] txt = pdffont.getGlyphString(txt_lns[i], false, font.Size, out paddingX, out paddingY);
                float cur_x = t_x[i];
                if (Angle == 0)
                {
                    foreach (ExportTTFFont.GlyphTTF g in txt)
                    {
                        if (g.path.PointCount == 0)
                        { cur_x += g.width * 0.75f; continue; }
                        PointF[] ps = g.path.PathPoints;
                        byte[] pt = g.path.PathTypes;
                        for (int j = 0; j < g.path.PointCount;)
                        {
                            switch (pt[j])
                            {
                                case 0://start
                                    if (j > 0) ClosePath();
                                    MoveTo(cur_x + ps[j].X * 0.75f, cur_y + ps[j].Y * 0.75f);
                                    j++;
                                    break;
                                case 1://line
                                    AppendLine(cur_x + ps[j].X * 0.75f, cur_y + ps[j].Y * 0.75f);
                                    j++;
                                    break;
                                case 3://interpolate bezier
                                    AppendBezier(cur_x, cur_y, ps[j], ps[j + 1], ps[j + 2]);
                                    j += 3;
                                    break;
                                default:
                                    i++;
                                    break;
                            }
                        }
                        ClosePath();
                        EndFig(Foreground, Background);
                        cur_x += g.width * 0.75f;
                    }
                    cur_y += font.GetHeight() * 0.75f;
                }
                else
                {
                }
            }
        }
        ///<summary>
        ///Method for add TextObject.
        /// </summary>
        public void AddTextObject(float x, float y, float Width, float Height,
       string HorizontalAlignment, string VerticalAlignment, string BorderBrush, float BorderThickness,
       float LeftLine, float TopLine, float RightLine, float BottomLine, string LeftLineDashStile,
       string TopLineDashStile, string RightLineDashStile, string BottomLineDashStile, string colorLeftLine,
       string colorTopLine, string colorRightLine, string colorBottomLine, bool Shadow, string ShadowColor, float ShadowWidth, string Background, string BorderLines,
       string Text,string Foreground, float PaddingLeft, float PaddingTop, float PaddingRight, float PaddingBottom, bool WordWrap, float Angle, bool Glass, string colorTop, Font font)
        {
            bool All = false;
            bool None = false;
            bool Left = false;
            bool Right = false;
            bool Top = false;
            bool Bottom = false;
            All = gBorderLines(BorderLines, out None, out Left, out Right, out Top, out Bottom);

            if (All && (LeftLine == TopLine && TopLine == RightLine && RightLine == BottomLine) &&
               (LeftLineDashStile == TopLineDashStile && TopLineDashStile == RightLineDashStile &&
               RightLineDashStile == BottomLineDashStile && BottomLineDashStile == "Solid") &&
               (colorLeftLine == colorTopLine && colorTopLine == colorRightLine && colorRightLine == colorBottomLine /*&& colorBottomLine == Background*/))
            {
                AddRectangle(x, y, Width, Height, BorderBrush, BorderThickness, Background, false);
            }
            else
            {
                if (Background != "none")
                {
                    AddRectangle(x, y, Width, Height, BorderBrush, 0, Background, false);
                }
                if (Left || All)
                {
                    if (LeftLineDashStile == "Solid")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine);
                    }
                    if (LeftLineDashStile == "Dash")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine, Dashes.Dash);
                    }
                    if (LeftLineDashStile == "Dot")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine, Dashes.Dot);
                    }
                    if (LeftLineDashStile == "DashDot")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine, Dashes.DashDot);
                    }
                    if (LeftLineDashStile == "DashDotDot")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine, Dashes.DashDotDot);
                    }
                    if (LeftLineDashStile == "Double")
                    {
                        AddLine(x, y, x, y + Height, colorLeftLine, LeftLine);
                        AddLine(x - BorderThickness * 2, y - BorderThickness * 2, x - BorderThickness * 2, y + Height + BorderThickness * 2, colorLeftLine, LeftLine);                 
                    }
                }
                if (Right || All)
                {
                    if (RightLineDashStile == "Solid")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine);
                    }
                    if (RightLineDashStile == "Dash")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine, Dashes.Dash);
                    }
                    if (RightLineDashStile == "Dot")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine, Dashes.Dot);
                    }
                    if (RightLineDashStile == "DashDot")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine, Dashes.DashDot);
                    }
                    if (RightLineDashStile == "DashDotDot")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine, Dashes.DashDotDot);
                    }
                    if (RightLineDashStile == "Double")
                    {
                        AddLine(x + Width, y, x + Width, y + Height, colorRightLine, RightLine);
                        AddLine(x + Width + BorderThickness * 2, y - BorderThickness * 2, x + Width + BorderThickness * 2, y + Height + BorderThickness * 2, colorRightLine, RightLine);
                    }
                }
                if (Top || All)
                {
                    if (TopLineDashStile == "Solid")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine);
                    }
                    if (TopLineDashStile == "Dash")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine, Dashes.Dash);
                    }
                    if (TopLineDashStile == "Dot")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine, Dashes.Dot);
                    }
                    if (TopLineDashStile == "DashDot")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine, Dashes.DashDot);
                    }
                    if (TopLineDashStile == "DashDotDot")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine, Dashes.DashDotDot);
                    }
                    if (TopLineDashStile == "Double")
                    {
                        AddLine(x, y, x + Width, y, colorTopLine, TopLine);
                        AddLine(x - BorderThickness * 2, y - BorderThickness * 2, x + Width + BorderThickness * 2, y - BorderThickness * 2, colorTopLine, TopLine);
                    }
                }
                    if (Bottom || All)
                    {
                        if (BottomLineDashStile == "Solid")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine);
                        }
                        if (BottomLineDashStile == "Dash")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine, Dashes.Dash);
                        }
                        if (BottomLineDashStile == "Dot")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine, Dashes.Dot);
                        }
                        if (BottomLineDashStile == "DashDot")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine, Dashes.DashDot);
                        }
                        if (BottomLineDashStile == "DashDotDot")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine, Dashes.DashDotDot);
                        }
                        if (BottomLineDashStile == "Double")
                        {
                            AddLine(x, y + Height, x + Width, y + Height, colorBottomLine, BottomLine);
                            AddLine(x - BorderThickness * 2, y + Height + BorderThickness * 2, x + Width + BorderThickness * 2, y + Height + BorderThickness * 2, colorBottomLine, BottomLine);
                        }
                    }
                }
            //Glass--------------------
            if (Glass)
            {
                AddRectangle(x, y, Width, Height / 2, BorderBrush, 0, colorTop, false);
                AddRectangle(x, y + Height / 2, Width, Height / 2, BorderBrush, 0, Background, false);
            }
            //Shadow-------------------
            if (Shadow)
            {
                AddLine(x + ShadowWidth, y + Height + ShadowWidth / 2, x + Width + ShadowWidth, y + Height + ShadowWidth / 2, ShadowColor, ShadowWidth);

                AddLine(x + Width + ShadowWidth / 2, y + ShadowWidth, x + Width + ShadowWidth / 2, y + Height + ShadowWidth, ShadowColor, ShadowWidth);
            }
               
         //   List<float> t_x; float t_y = y;
          //  t_x = TextAlignments(x, ref t_y, HorizontalAlignment, VerticalAlignment, Width, Height, font, Text, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom, BorderThickness);
            
                if (Text != null)
                {
                    createText(x, y, HorizontalAlignment, VerticalAlignment, Width, Height, font, Text, PaddingLeft, PaddingRight, PaddingTop, PaddingBottom, BorderThickness, Foreground, Background, Angle);
                }
        }
        /// <summary>
        /// Method to add rectangle.
        /// </summary>
        public void AddRectangle(float x, float y, float Width, float Height,
                                  string Stroke, float StrokeThickness, string Fill, bool Rounded)
        {
            if (StrokeThickness == 0 && Fill == "none") return;
            string rgb_stroke;
            string rgb_fill;
            string fill_str = "";
            string border_col = "";
            string rect_stroke = "";
            string gsave = "gsave ";
            string grestore = "grestore ";

            if (StrokeThickness == 0)
            {
                gsave = ""; grestore = "";
            }
            else
            {
                rgb_stroke = ColorToPsRgb(Stroke);
                border_col = rgb_stroke + " setrgbcolor ";
            }
            if (Fill != "none")
            {
                rgb_fill = ColorToPsRgb(Fill);
                fill_str = gsave + rgb_fill + " setrgbcolor fill " + grestore;
            }

            rect_stroke = FloatToString(StrokeThickness) + " setlinewidth ";
            string internal_data;
            if (Rounded)
            {
                string x1 = FloatToString(x);
                string y1 = FloatToString(WindowHeight - y - Height);
                string x2 = FloatToString(x);
                string y2 = FloatToString(WindowHeight - y);
                string x3 = FloatToString(x + Width);
                string y3 = FloatToString(WindowHeight - y); ;
                string x4 = FloatToString(x + Width);
                string y4 = FloatToString(WindowHeight - y - Height);
            internal_data = FloatToString(StrokeThickness) + " setlinewidth " + FloatToString(x + Width/2) + " " +
            FloatToString(WindowHeight - y - Height) + " moveto " +
            x1 + " " + y1 + " " + x2 + " " + y2 + " 5 arct " +
            x2 + " " + y2 + " " + x3 + " " + y3 + " 5 arct " + 
            x3 + " " + y3 + " " + x4 + " " + y4 + " 5 arct " +
            x4 + " " + y4 + " " + x1 + " " + y1 + " 5 arct closepath " + fill_str + border_col + "stroke";
            }
            else
            {
                internal_data = FloatToString(StrokeThickness) + " setlinewidth " + FloatToString(x) + " " +
            FloatToString(WindowHeight - y - Height) + " newpath moveto " + FloatToString(x) + " " + FloatToString(WindowHeight - y) +
            " lineto  " + FloatToString(x + Width) + " " + FloatToString(WindowHeight - y) +
            " lineto  " + FloatToString(x + Width) + " " + FloatToString(WindowHeight - y - Height) +
            " lineto  closepath " + fill_str + border_col + "stroke";
            }
            PS_DATA.Append(internal_data + " ");
        }

        /// <summary>
        /// Method for add ellips.
        /// </summary>
        public void AddEllipse(float x, float y, float Width, float Height,
             string Stroke, float StrokeThickness, string Fill)
        {
            if (StrokeThickness == 0 && Fill == "none") return;
            string rgb_stroke;
            string rgb_fill;
            string fill_str = "";
            string border_col = "";
            string ell_stroke = "";
            string gsave = "gsave ";
            string grestore = "grestore ";

            if (StrokeThickness == 0)
            {
                gsave = ""; grestore = "";
            }
            else
            {
                rgb_stroke = ColorToPsRgb(Stroke);
                border_col = rgb_stroke + " setrgbcolor ";
            }
            if (Fill != "none")
            {
                rgb_fill = ColorToPsRgb(Fill);
                fill_str = gsave + rgb_fill + " setrgbcolor fill " + grestore;
            }
            ell_stroke = FloatToString(StrokeThickness) + " setlinewidth ";
            string internal_data = "";


            internal_data = FloatToString(StrokeThickness) + " setlinewidth " +
            FloatToString(x + Width/2) + " " + FloatToString(WindowHeight - y - Height / 2) +
            " " + FloatToString(Width/2) + " 0 360 arc closepath "
             + fill_str + border_col + "stroke";

            PS_DATA.Append(internal_data + " ");
        }

        /// <summary>
        /// Method for add triangle.
        /// </summary>
        public void AddTriangle(float x, float y, float Width, float Height,
             string Stroke, float StrokeThickness, string Fill)
        {
            if (StrokeThickness == 0 && Fill == "none") return;
            float x2 = Width + x;
            float y2 = y;
            float x3 = x + Width/2;
            float y3 = y;

            string rgb_stroke;
            string rgb_fill;
            string fill_str = "";
            string border_col = "";
            string tri_stroke = "";
            string gsave = "gsave ";
            string grestore = "grestore ";

            if (StrokeThickness == 0)
            { gsave = ""; grestore = ""; }
            else
            {
                rgb_stroke = ColorToPsRgb(Stroke);
                border_col = rgb_stroke + " setrgbcolor ";
            }
            if (Fill != "none")
            {
                rgb_fill = ColorToPsRgb(Fill);
                fill_str = gsave + rgb_fill + " setrgbcolor fill " + grestore;
            }
                tri_stroke = FloatToString(StrokeThickness) + " setlinewidth ";

            string internal_data = FloatToString(StrokeThickness) + " setlinewidth " + FloatToString(x) + " " +
            FloatToString(WindowHeight - y - Height) + " newpath moveto " + FloatToString(x2) + " " + FloatToString(WindowHeight - Height - y2) +
            " lineto  " + FloatToString(x3) + " " + FloatToString(WindowHeight - y3) +
            " lineto  closepath " + fill_str + border_col + "stroke";

            PS_DATA.Append(internal_data + " ");
        }

        /// <summary>
        /// Method for add Diamond.
        /// </summary>
        public void AddDiamond(float x, float y, float Width, float Height,
             string Stroke, float StrokeThickness, string Fill)
        {
            float x1 = Width / 2 + x; 
            float y1 = y;
            float x2 = Width + x; 
            float y2 = Height / 2 + y;
            float x3 = Width / 2 + x; 
            float y3 = y;
            float x4 = x; 
            float y4 = Height / 2 + y;

            string rgb_stroke;
            string rgb_fill;
            string fill_str = "";
            string border_col = "";
            string tri_stroke = "";
            string gsave = "gsave ";
            string grestore = "grestore ";

            if (StrokeThickness == 0)
            { gsave = ""; grestore = ""; }
            else
            {
                rgb_stroke = ColorToPsRgb(Stroke);
                border_col = rgb_stroke + " setrgbcolor ";
            }
            if (Fill != "none")
            {
                rgb_fill = ColorToPsRgb(Fill);
                fill_str = gsave + rgb_fill + " setrgbcolor fill " + grestore;
            }
            tri_stroke = FloatToString(StrokeThickness) + " setlinewidth ";

            string internal_data = FloatToString(StrokeThickness) + " setlinewidth " + FloatToString(x1) + " " +
            FloatToString(WindowHeight - y1 - Height) + " newpath moveto " + FloatToString(x2) + " " + FloatToString(WindowHeight - y2) +
            " lineto  " + FloatToString(x3) + " " + FloatToString(WindowHeight - y3) +
            " lineto  " + FloatToString(x4) + " " + FloatToString(WindowHeight - y4) +
            " lineto  closepath " + fill_str + border_col + "stroke";

            PS_DATA.Append(internal_data + " ");
        }

        ///<summary>
        ///Method for add line.
        /// </summary>
        public void AddLine(float x, float y, float x2, float y2, string Stroke, float StrokeThickness)
        {
            StartFig(StrokeThickness);
            MoveTo(x, y);
            AppendLine(x2, y2);
            EndFig(Stroke);
        }

        ///<summary>
        ///Method for add line with dash.
        /// </summary>
        public void AddLine(float x, float y, float x2, float y2, string Stroke, float StrokeThickness, Dashes dash)
        {
            string line_col = "";
            string line_stroke = "";
            string rgb = ColorToPsRgb(Stroke);
            
            string StrokeDashArray = "";
            if (dash == Dashes.Dash)
            {
                StrokeDashArray = " [5] 0 setdash ";
            }
            if (dash == Dashes.Dot)
            {
                StrokeDashArray = "[2 2] 0 setdash";
            }
            if (dash == Dashes.DashDot)
            {
                StrokeDashArray = "[2 2 5 2] 0 setdash";

            }
            if (dash == Dashes.DashDotDot)
            {
                StrokeDashArray = "[2 2 2 2 5 2] 0 setdash";
            }
            if (dash == Dashes.Double)
            {
                StrokeDashArray = "";
                AddLine(x+10, y+10, x2+10, y2+10, Stroke, StrokeThickness);
            }

            line_col = rgb + " setrgbcolor ";
            line_stroke = FloatToString(StrokeThickness) + " setlinewidth " + StrokeDashArray + " ";
            string internal_data = line_stroke + FloatToString(x) + " " +
            FloatToString(WindowHeight - y) + " newpath moveto " + FloatToString(x2) + " " + FloatToString(WindowHeight - y2) +
            " lineto  " + line_col + "stroke [ ] 0 setdash";
            PS_DATA.Append(internal_data + " ");
        }

        public void AddBezier(float x, float y, PointF p0, PointF p1, PointF p2, PointF p3,
             string Stroke, float StrokeThickness)
        {
            string line_col = "";
            string line_stroke = "";
            string rgb = ColorToPsRgb(Stroke);
            line_col = rgb + " setrgbcolor ";
            line_stroke = FloatToString(StrokeThickness) + " setlinewidth ";
            string internal_data = line_stroke + FloatToString(x + p0.X) + " " +
            FloatToString(WindowHeight - y - p0.Y) + " newpath moveto " +
            FloatToString(x + p1.X) + " " + FloatToString(WindowHeight - y - p1.Y) + " " +
            FloatToString(x + p2.X) + " " + FloatToString(WindowHeight - y - p2.Y) + " " +
            FloatToString(x + p3.X) + " " + FloatToString(WindowHeight - y - p3.Y) + " " +
            " curveto " + line_col + "stroke";
            PS_DATA.Append(internal_data + " ");
        }
        private void AppendBezier(float x, float y, PointF p1, PointF p2, PointF p3)
        {
            string internal_data =
            FloatToString(x + p1.X * 0.75f) + " " + FloatToString(WindowHeight - y - p1.Y * 0.75f) + " " +
            FloatToString(x + p2.X * 0.75f) + " " + FloatToString(WindowHeight - y - p2.Y * 0.75f) + " " +
            FloatToString(x + p3.X * 0.75f) + " " + FloatToString(WindowHeight - y - p3.Y * 0.75f) + " " +
            " curveto ";
            PS_DATA.Append(internal_data);
        }
        private void AppendLine(float x2, float y2)
        {
            string internal_data = 
            FloatToString(x2) + " " + FloatToString(WindowHeight - y2) +
            " lineto ";
            PS_DATA.Append(internal_data);
        }
        private void StartFig(float StrokeThickness)
        {
            string line_stroke = " ";   
            line_stroke = FloatToString(StrokeThickness) + " setlinewidth ";
            PS_DATA.Append(line_stroke + " ");
        }
        private void MoveTo(float x, float y)
        {                     
            PS_DATA.Append(" " + FloatToString(x) + " " +
            FloatToString(WindowHeight - y) + " moveto ");
        }
        private void ClosePath()
        {
            PS_DATA.Append(" closepath ");
        }
        private void EndFig(string stroke, string fill)
        {    
            string rgb_stroke = ColorToPsRgb(stroke);
            string l = "";      
            string rgb_fill = ColorToPsRgb(stroke);
            l += /*" gsave " +*/ rgb_fill + " setrgbcolor fill "/*grestore "*/;
            PS_DATA.Append(l + rgb_stroke +" ");
        }
        private void EndFig(string stroke)
        {
            string rgb_stroke = ColorToPsRgb(stroke);
            PS_DATA.Append(" gsave " + rgb_stroke + "  setrgbcolor stroke grestore ");
        }

        /// <summary>
        /// Add image
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="name"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddImage(string filename, string format, string name, float left, float top, float width, float height)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                string s_left = FloatToString(left);
                string s_top = FloatToString(WindowHeight - height - top);
                string s_width = FloatToString(Math.Round((int)width/0.75f));
                string s_height = FloatToString(Math.Round((int)height /0.75f));
                string img = " gsave " +
                             s_left + " " + s_top + " translate " + //set lower left of image at (360, 72)
                             FloatToString(Math.Round(width)) + " " + FloatToString(Math.Round(height)) + " scale " +        //size of rendered image is 175 points by 47 points
                             s_width + " " +                  //number of columns per row
                             s_height + " " +                  //number of rows
                             "8 " +                    //bits per color channel (1, 2, 4, or 8)
                             "[" + s_width +" 0 0 -" + s_height +" 0 " + s_height + "] " + //transform array... maps unit square to pixel
                             "(" + filename + "." + format + ") (r) file /DCTDecode filter " + //opens the file and filters the image data
                             "false " +                //pull channels from separate sources
                             "3 " +                    // 3 color channels (RGB)
                             "colorimage " +
                             "grestore ";
                PS_DATA.Append(img);
            }           
        }

        internal void AddImage(string filename, string format, float left, float top, float width, float height)
        {
            AddImage(filename, format, null, left, top, width, height);
        }
        public void Finish()
        {
            PS_DATA.Append("showpage\n");
            PS_DATA.Append("%%Trailer\n" +
                       "%% Pages: 1\n" +
                       "%% EOF");
        }
        /// <summary>
        /// Save file.
        /// </summary>
        public void Save(string filename)
        {
            System.IO.File.WriteAllText(filename, PS_DATA.ToString());
        }

        /// <summary>
        /// Save stream.
        /// </summary>
        public void Save(Stream stream)
        {
            stream.Write(System.Text.Encoding.UTF8.GetBytes(PS_DATA.ToString()), 0, System.Text.Encoding.UTF8.GetBytes(PS_DATA.ToString()).Length);
        }

        string ColorToPsRgb(string htmlcolor)
        {
            return FloatToString((double)System.Drawing.ColorTranslator.FromHtml(htmlcolor).R / 255) + " " +
                         FloatToString((double)System.Drawing.ColorTranslator.FromHtml(htmlcolor).G / 255) + " " +
                         FloatToString((double)System.Drawing.ColorTranslator.FromHtml(htmlcolor).B / 255);
        }
        private string FloatToString(double flt)
        {
            return ExportUtils.FloatToString(flt);
        }
        private string PSFont(string font)
        {
            return font.Replace(" ", "-");
        }

        /// <param name="name"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public PSDocument(string name, float Width, float Height)
        {
            CreateWindow(name, Width, Height);
        }
        public PSDocument()
        {
        }
    }
}
