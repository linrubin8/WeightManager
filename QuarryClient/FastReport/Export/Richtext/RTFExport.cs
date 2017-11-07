using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Export;


namespace FastReport.Export.RichText
{

    /// <summary>
    /// Specifies the image format in RTF export.
    /// </summary>
    public enum RTFImageFormat
    {
        /// <summary>
        /// Specifies the .png format.
        /// </summary>
        Png,

        /// <summary>
        /// Specifies the .jpg format.
        /// </summary>
        Jpeg,

        /// <summary>
        /// Specifies the .emf format.
        /// </summary>
        Metafile
    }

    /// <summary>
    /// Represents the RTF export filter.
    /// </summary>
    public class RTFExport : ExportBase
    {
        #region Constants
        const float Xdivider = 15.05F;
        const float Ydivider1 = 14.8F;
        const float Ydivider2 = 14.8F;
        const float Ydivider3 = 14.7F;
        const float MargDivider = 56.695239F;
        const float FONT_DIVIDER = 15F;
        const float IMAGE_DIVIDER = 25.3F;
        const int PIC_BUFF_SIZE = 512;        
        #endregion

        #region Private fields
        private List<string> FColorTable;
        private bool FPageBreaks;
        private List<string> FFontTable;        
        private ExportMatrix FMatrix;
        private bool FWysiwyg;
        private string FCreator;
        private bool FAutoSize;
        private MyRes Res;
        private RTFImageFormat FImageFormat;
        private bool FPictures;
        private string FTempFile;
        private int FJpegQuality;
        private float FDpiFactor;
        private float YDiv;
        private Color TextColor;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the quality of Jpeg images in RTF file.
        /// </summary>
        /// <remarks>
        /// Default value is 90. This property will be used if you select Jpeg 
        /// in the <see cref="ImageFormat"/> property.
        /// </remarks>
        public int JpegQuality
        {
            get { return FJpegQuality; }
            set { FJpegQuality = value; }
        }

        /// <summary>
        /// Gets or sets the image format that will be used to save pictures in RTF file.
        /// </summary>
        /// <remarks>
        /// Default value is <b>Metafile</b>. This format is better for exporting such objects as
        /// <b>MSChartObject</b> and <b>ShapeObject</b>.
        /// </remarks>
        public RTFImageFormat ImageFormat
        {
            get { return FImageFormat; }
            set { FImageFormat = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that pictures are enabled.
        /// </summary>
        public bool Pictures
        {
            get { return FPictures; }
            set { FPictures = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that page breaks are enabled.
        /// </summary>
        public bool PageBreaks
        {
            get { return FPageBreaks; }
            set { FPageBreaks = value; }
        }
                
        /// <summary>
        /// Gets or sets a value that determines whether the wysiwyg mode should be used 
        /// for better results.
        /// </summary>
        /// <remarks>
        /// Default value is <b>true</b>. In wysiwyg mode, the resulting rtf file will look
        /// as close as possible to the prepared report. On the other side, it may have a lot 
        /// of small rows/columns, which will make it less editable. If you set this property
        /// to <b>false</b>, the number of rows/columns in the resulting file will be decreased.
        /// You will get less wysiwyg, but more editable file.
        /// </remarks>
        public bool Wysiwyg
        {
            get { return FWysiwyg; }
            set { FWysiwyg = value; }
        }
        
        /// <summary>
        /// Gets or sets the creator of the document.
        /// </summary>
        public string Creator
        {
            get { return FCreator; }
            set { FCreator = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the rows in the resulting table 
        /// should calculate its height automatically.
        /// </summary>
        /// <remarks>
        /// Default value for this property is <b>false</b>. In this mode, each row in the
        /// resulting table has fixed height to get maximum wysiwyg. If you set it to <b>true</b>,
        /// the height of resulting table will be calculated automatically by the Word processor.
        /// The document will be more editable, but less wysiwyg.
        /// </remarks>
        public bool AutoSize
        {
            get { return FAutoSize; }
            set { FAutoSize = value; }
        }
        #endregion

        #region Private Methods
  
        private string GetRTFBorders(ExportIEMStyle Style)
        {

            //// +debug
            //Style.Border.Lines = BorderLines.All;
            //Style.Border.Width = 1;
            //// -debug

            StringBuilder result = new StringBuilder(256);
            // top
            if ((Style.Border.Lines & BorderLines.Top) > 0)
                result.Append("\\clbrdrt").
                    Append(GetRTFLineStyle(Style.Border.TopLine.Style)).
                    Append("\\brdrw").
                    Append(((int)Math.Round(Style.Border.TopLine.Width * 20)).ToString()).
                    Append("\\brdrcf").
                    Append(GetRTFColorFromTable(GetRTFColor(Style.Border.TopLine.Color)));
            // left
            if ((Style.Border.Lines & BorderLines.Left) > 0)
                result.Append("\\clbrdrl").
                    Append(GetRTFLineStyle(Style.Border.LeftLine.Style)).
                    Append("\\brdrw").
                    Append(((int)Math.Round(Style.Border.LeftLine.Width * 20)).ToString()).
                    Append("\\brdrcf").
                    Append(GetRTFColorFromTable(GetRTFColor(Style.Border.LeftLine.Color)));
            // bottom
            if((Style.Border.Lines & BorderLines.Bottom) > 0)
                result.Append("\\clbrdrb").
                    Append(GetRTFLineStyle(Style.Border.BottomLine.Style)).
                    Append("\\brdrw").
                    Append(((int)Math.Round(Style.Border.BottomLine.Width * 20)).ToString()).
                    Append("\\brdrcf").
                    Append(GetRTFColorFromTable(GetRTFColor(Style.Border.BottomLine.Color)));
            // right
            if ((Style.Border.Lines & BorderLines.Right) > 0)
                result.Append("\\clbrdrr").
                    Append(GetRTFLineStyle(Style.Border.RightLine.Style)).
                    Append("\\brdrw").
                    Append(((int)Math.Round(Style.Border.RightLine.Width * 20)).ToString()).
                    Append("\\brdrcf").
                    Append(GetRTFColorFromTable(GetRTFColor(Style.Border.RightLine.Color)));
            return result.ToString();
        }

        private string GetRTFLineStyle(LineStyle lineStyle)
        {
            switch (lineStyle)
            {
                case LineStyle.Dash:
                    return "\\brdrdash";
                case LineStyle.DashDot:
                    return "\\brdrdashd";
                case LineStyle.DashDotDot:
                    return "\\brdrdashdd";
                case LineStyle.Dot:
                    return "\\brdrdot";
                case LineStyle.Double:
                    return "\\brdrdb";                
                default:
                    return "\\brdrs";
            }
        }

        private string GetRTFColor(Color c)
        {
            StringBuilder result = new StringBuilder(64);
            result.Append("\\red").Append(Convert.ToString(c.R)).
                Append("\\green").Append(Convert.ToString(c.G)).
                Append("\\blue").Append(Convert.ToString(c.B)).Append(";");
            return result.ToString();
        }

        private string GetRTFFontStyle(FontStyle f)
        {
            StringBuilder result = new StringBuilder(8);
            if ((f & FontStyle.Italic) != 0)
                result.Append("\\i");
            if ((f & FontStyle.Bold) != 0)
                result.Append("\\b");
            if ((f & FontStyle.Underline) != 0)
                result.Append("\\ul");
            return result.ToString();
        }

        private string GetRTFColorFromTable(string f)
        {
            string Result;
            int i = FColorTable.IndexOf(f);
            if (i != -1)
                Result = (i + 1).ToString();
            else
            {
                FColorTable.Add(f);
                Result = FColorTable.Count.ToString();
            }
            return Result;
        }

        private string GetRTFFontName(string f)
        {
            string Result;
            int i = FFontTable.IndexOf(f);
            if (i != -1)
                Result = (i).ToString();
            else
            {
                FFontTable.Add(f);
                Result = (FFontTable.Count - 1).ToString();
            }
            return Result;
        }

        private string GetRTFHAlignment(HorzAlign HAlign)
        {
            switch (HAlign)
            {
                case HorzAlign.Right:
                    return "\\qr";
                case HorzAlign.Center:
                    return "\\qc";
                case HorzAlign.Justify:
                    return "\\qj";
                default:
                    return "\\ql";
            }
        }
    
        private string GetRTFVAlignment(VertAlign VAlign)
        {
            switch (VAlign)
            {
                case VertAlign.Top:
                    return "\\clvertalt";
                case VertAlign.Center:
                    return "\\clvertalc";
                default:
                    return "\\clvertalb";
            }
        }

        private string StrToRTFSlash(string Value)
        {
            StringBuilder Result = new StringBuilder();
            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i] == '\\')
                    Result.Append("\\\\");
                else if (Value[i] == '{')
                    Result.Append("\\{");
                else if (Value[i] == '}')
                    Result.Append("\\}");
                  else if ((Value[i] == '\r') && (i < (Value.Length - 1)) && (Value[i + 1] == '\n'))
                  {
                    Result.Append("\\line\r\n");
                    i++;
                  }
                  else
                    Result.Append(Value[i]);
            }
            return Result.ToString();
        }

        private string ParseHtmlTags(string s)
        {

            int Index = 0;
            int Begin = 0;
            int End = 0;
            string Tag;
            string Text;
            string result;
            string TagClose = "";
            CurrentStyle current_style = new CurrentStyle();
            CurrentStyle previos_style;

            current_style.Size = 10;
            current_style.Bold = false;
            current_style.Italic = false;
            current_style.Underline = false;
            current_style.Colour = Color.FromName("Black");
            current_style.Strike = false;
            current_style.Sub = false;
            current_style.Sup = false;

            Stack<CurrentStyle> style_stack = new Stack<CurrentStyle>();


            Begin = s.IndexOfAny(new char[1] { '<' }, Index);

            if (Begin == -1) result = s;
            else {
                result = "";
                while (Begin != -1)
                {
                    if (Begin != 0 && Index == 0)
                    {
                        if (Index == 0)
                        {
                            result += s.Substring(Index, Begin);
                        }
                    }

                    End = s.IndexOfAny(new char[1] { '>' }, Begin + 1);
                    if (End == -1) break;

                    Tag = s.Substring(Begin + 1, End - Begin - 1);

                    bool CloseTag = Tag.StartsWith("/");

                    if (CloseTag) Tag = Tag.Remove(0, 1);

                    string[] items = Tag.Split(' ');

                    Tag = items[0].ToUpper();
                    TagClose = "";

                    bool PutOnStack = true;

                    if (!CloseTag)
                    {
                        current_style.LastTag = Tag;
                        switch (Tag)
                        {
                            case "B": 
                                current_style.Bold = true;
                                TagClose = "\\b ";
                                break;
                            case "I": 
                                current_style.Italic = true;
                                TagClose = "\\i ";
                                break;
                            case "U": 
                                current_style.Underline = true; 
                                TagClose = "\\ul ";
                                break;
                            case "STRIKE": current_style.Strike = true; break;
                            case "SUB": current_style.Sub = true; break;
                            case "SUP": current_style.Sup = true; break;
                            case "FONT":
                                  {
                                    if (items.Length > 1)
                                    {
                                        string[] attrs = items[1].Split('=');
                                        if (attrs[0] == "color")
                                        {
                                            TagClose = "\\cf" + GetRTFColorFromTable(GetRTFColor(System.Drawing.ColorTranslator.FromHtml(attrs[1].Replace("\"", "")))) + " "; 
                                            
                                        }         
                                    }                                      
                                }
                                /*current_style.Font = items[1];*/
                                //                            ParseFont(items[1], current_style, out current_style);
                                break;
                            default:
                                TagClose = Tag;
                                PutOnStack = false;
                                break;
                        }
                        if (PutOnStack) style_stack.Push(current_style);
                    }
                    else
                    {
                        if (style_stack.Count > 0)
                        {
                            previos_style = style_stack.Pop();
                            if (previos_style.LastTag != Tag)
                            {
                                throw new Exception("Unaligned HTML TAGS");
                            }
                            switch (Tag)
                            {
                                case "B": TagClose = "\\b0 "; break;
                                case "I": TagClose = "\\i0 "; break;
                                case "U": TagClose = "\\ul0 "; break;
                                case "STRIKE": break;
                                case "SUB": break;
                                case "SUP": break;
                                case "FONT":
                                    TagClose = "\\cf" + GetRTFColorFromTable(GetRTFColor((TextColor))) + " ";
                                    /*current_style.Font = items[1];*/
                                    //                            ParseFont(items[1], current_style, out current_style);
                                    break;
                                default:
                                    throw new Exception("Unsupported HTML TAG");
                            }
                            current_style = previos_style;
                        }
                    }

                    Index = End + 1;
                    Begin = s.IndexOfAny(new char[1] { '<' }, Index);

                    if (Begin == -1)
                    {
                        Text = s.Substring(Index);
                    }
                    else
                    {
                        Text = s.Substring(Index, Begin - Index);
                    }
                    result += TagClose + Text;
                }
            }

            return result;
        }

        private string StrToRTFUnicodeEx(string Value, bool HtmlTags)
        {
            Value = StrToRTFUnicode(StrToRTFSlash(Value));
            if (HtmlTags)
                Value = ParseHtmlTags(Value);
            return Value;
        }

        private string StrToRTFUnicode(string Value)
        {
            StringBuilder Result = new StringBuilder(128);            
            foreach(UInt16 c in Value)
            {
                if (c > 127)
                    Result.Append("\\u").Append(c.ToString()).Append("\\'3f");
                else
                    Result.Append((char)c);
            }
            return Result.ToString();
        }

        private void Prepare()
        {
            int i;
            ExportIEMObject Obj;
            for (int y = 0; y < FMatrix.Height; y++)
                for (int x = 0; x < FMatrix.Width; x++)
                {
                    i = FMatrix.Cell(x, y);
                    if (i != -1)
                    {
                        Obj = FMatrix.ObjectById(i);
                        if (Obj.Counter != -1)
                        {
                            Obj.Counter = -1;
                            if (Obj.Style != null)
                            {
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.FillColor));
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.Border.LeftLine.Color));
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.Border.RightLine.Color));
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.Border.TopLine.Color));
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.Border.BottomLine.Color));
                                GetRTFColorFromTable(GetRTFColor(Obj.Style.TextColor));                                
                                GetRTFFontName(Obj.Style.Font.Name);
                            }
                        }
                    }
                }        
        }

        private string SetPageProp(int Page)
        {
            StringBuilder result = new StringBuilder(64);
            result.Append("\\pgwsxn").
                Append(((int)Math.Round(FMatrix.PageWidth(Page) * MargDivider)).ToString()).
                Append("\\pghsxn").
                Append(((int)Math.Round(FMatrix.PageHeight(Page) * MargDivider)).ToString()).
                Append("\\marglsxn").
                Append(((int)Math.Round(FMatrix.PageLMargin(Page) * MargDivider)).ToString()).
                Append("\\margrsxn").
                Append(((int)Math.Round(FMatrix.PageRMargin(Page) * MargDivider)).ToString()).
                Append("\\margtsxn").
                Append(((int)Math.Round(FMatrix.PageTMargin(Page) * MargDivider)).ToString()).
                Append("\\margbsxn").
                Append(((int)Math.Round(FMatrix.PageBMargin(Page) * MargDivider)).ToString()).
                Append(FMatrix.Landscape(Page) ? "\\lndscpsxn" : String.Empty);
            return result.ToString();
        }

        private void Write(Stream stream, string str)
        {
            byte[] buff = Converter.StringToByteArray(str);
            stream.Write(buff, 0, buff.Length);
        }

        private void WriteLine(Stream stream, string str)
        {
            Write(stream, str);
            Write(stream, "\r\n");
        }

        private Stream GetTempFileStream()
        {
            FTempFile = Path.GetTempPath() + Path.GetRandomFileName();
            return new FileStream(FTempFile, FileMode.Create);
        }

        private void DeleteTempFile()
        {
            if (File.Exists(FTempFile))
	        File.Delete(FTempFile);
        }

        private string GetRTFString(ExportIEMStyle style, string text, float drow, bool HtmlTags)
        {
            StringBuilder CellsStream = new StringBuilder();

            CellsStream.Append(GetRTFHAlignment(style.HAlign)).Append("{");
            CellsStream.Append("\\f").Append(GetRTFFontName(style.Font.Name));

            string s = StrToRTFUnicodeEx(ExportUtils.TruncReturns(text), HtmlTags);

            double fh = style.Font.Height * FDpiFactor * YDiv * 0.98;
            double lh = style.LineHeight * 8.0;

            if (s.Length > 0)
            {
                CellsStream.Append("\\fs").Append((Math.Round(style.Font.Size * 2)).ToString());
                CellsStream.Append(GetRTFFontStyle(style.Font.Style)).Append("\\cf");
                CellsStream.Append(GetRTFColorFromTable(GetRTFColor((style.TextColor))));
                CellsStream.Append(style.RTL ? "\\rtlch" : String.Empty);

                CellsStream.Append("\\sb").
                    Append(((int)Math.Round(style.Padding.Top * YDiv)).ToString()).
                    Append("\\sa").
                    Append(((int)Math.Round(style.Padding.Bottom * YDiv)).ToString()).
                    Append("\\li").
                    Append(((int)Math.Round((style.Padding.Left) * Xdivider)).ToString()).
                    Append("\\ri").
                    Append(((int)Math.Round((style.Padding.Right) * Xdivider)).ToString()).
                    Append("\\sl-").
                    Append(((int)Math.Round(fh + lh)).ToString()).
                    Append("\\slmult0 ");
                if (s.StartsWith("\t") && style.FirstTabOffset != 0)
                {
                  // replace first tab symbol with \\fi
                  s = "\\fi" + ((int)Math.Round((style.FirstTabOffset) * Xdivider)).ToString() + " " + s.Remove(0, 1);
                  // convert multiline text to rtf paragraphs. \\fi can be applied to a paragraph only
                  s = s.Replace("\\line\r\n\t", "\\par ");
                }
                CellsStream.Append(s);
            }
            else
            {
                int j = (int)(drow / FONT_DIVIDER);
                j = j > 20 ? 20 : j;
                CellsStream.Append("\\fs").Append(j.ToString());
            }

            CellsStream.Append("\\cell}");
            return CellsStream.ToString(); 
        }

        private string GetRTFMetafile(ExportIEMObject Obj)
        {
            byte[] picbuff = new Byte[PIC_BUFF_SIZE];
            string scale = ((int)(100 / FDpiFactor)).ToString();
            StringBuilder CellsStream = new StringBuilder(256);
            CellsStream.Append("{");
            Obj.PictureStream.Position = 0;
            if (FPictures && Config.FullTrust && (FImageFormat == RTFImageFormat.Metafile))
            {
                CellsStream.Append("\\sb0\\li0\\sl0\\slmult0\\qc\\clvertalc {");
                float dx = Obj.Width;
                float dy = Obj.Height;
                CellsStream.Append("\\pict\\picw").Append(dx.ToString());
                CellsStream.Append("\\pich").Append(dy.ToString());
                CellsStream.Append("\\picscalex").Append(scale);
                CellsStream.Append("\\picscaley").Append(scale);
                CellsStream.Append("\\picwGoal").Append(Convert.ToString(dx * 15));
                CellsStream.Append("\\pichGoal").Append(Convert.ToString(dy * 15));
                CellsStream.Append("\\emfblip\r\n");
                int n;
                do
                {
                    n = Obj.PictureStream.Read(picbuff, 0, PIC_BUFF_SIZE);
                    for (int z = 0; z < n; z++)
                    {
                        CellsStream.Append(ExportUtils.XConv[picbuff[z] >> 4]);
                        CellsStream.Append(ExportUtils.XConv[picbuff[z] & 0xF]);
                    }
                    CellsStream.Append("\r\n");
                }
                while (n == PIC_BUFF_SIZE);
                CellsStream.Append("}");
            }
            else if (FPictures && (FImageFormat != RTFImageFormat.Metafile))
            {
                CellsStream.Append("\\sb0\\li0\\sl0\\slmult0\\qc\\clvertalc {");
                float dx = (int)Obj.Width;
                float dy = (int)Obj.Height;
                CellsStream.Append("\\pict\\picw").Append(dx.ToString());
                CellsStream.Append("\\pich").Append(dy.ToString());
                CellsStream.Append("\\picscalex").Append(scale);
                CellsStream.Append("\\picscaley").Append(scale);
                CellsStream.Append("\\");
                CellsStream.Append(FImageFormat == RTFImageFormat.Jpeg ? "jpegblip\r\n" : "pngblip\r\n");
                int n;
                do
                {
                    n = Obj.PictureStream.Read(picbuff, 0, PIC_BUFF_SIZE);
                    for (int z = 0; z < n; z++)
                    {
                        CellsStream.Append(ExportUtils.XConv[picbuff[z] >> 4]);
                        CellsStream.Append(ExportUtils.XConv[picbuff[z] & 0xF]);
                    }
                    CellsStream.Append("\r\n");
                }
                while (n == PIC_BUFF_SIZE);
                CellsStream.Append("}");
            }
            CellsStream.Append("\\cell}\r\n");
            return CellsStream.ToString();
        }

        private string GetRTFHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\\rtf1\\ansi");
            sb.Append("{\\fonttbl");
            for (int i = 0; i < FFontTable.Count; i++)
                sb.Append("{\\f" + Convert.ToString(i) + " " + FFontTable[i] + "}");
            sb.Append("}");
            sb.Append("{\\colortbl;");
            for (int i = 0; i < FColorTable.Count; i++)
                sb.Append(FColorTable[i]);
            sb.Append("}");
            return sb.ToString();
        }

        private string GetRTFMetaInfo()
        {
            StringBuilder buf = new StringBuilder(256);
            buf.Append("{\\info{\\title ").Append(StrToRTFUnicodeEx(Report.ReportInfo.Name, false)).
                Append("}{\\author ").Append(StrToRTFUnicodeEx(FCreator, false)).
                Append("}{\\creatim\\yr").Append(String.Format("{0:yyyy}", DateTime.Now)).
                Append("\\mo").Append(String.Format("{0:MM}", DateTime.Now)).
                Append("\\dy").Append(String.Format("{0:dd}", DateTime.Now)).
                Append("\\hr").Append(String.Format("{0:HH}", DateTime.Now)).
                Append("\\min").Append(String.Format("{0:mm}", DateTime.Now)).AppendLine("}}");
            return buf.ToString();
        }

        struct CurrentStyle
        {
            public int Size;
            public bool Bold;
            public bool Italic;
            public bool Underline;
            public bool Strike;
            public bool Sub;
            public bool Sup;
            public Color Colour;
            public string LastTag;
        };

        private void ExportRTF(Stream stream)
        {            
            int  i, j, x, fx, fy, dx, dy, pbk;
            int  dcol, drow, xoffs;
            ExportIEMObject Obj;
            Prepare();
            //Write a header is below now   

            pbk = 0;
            Write(stream, SetPageProp(pbk));
            if (ShowProgress)
              Config.ReportSettings.OnProgress(Report, Res.Get("SavePage") + " " + (pbk + 1).ToString()); 

            for (int y = 0; y < FMatrix.Height - 1; y++)
            {
                if (FPageBreaks)
                    if (pbk < FMatrix.PagesCount)
                        if (FMatrix.PageBreak(pbk) <= FMatrix.YPosById(y))
                        {
//                            WriteLine(stream, "\\pagebb\\sect");
                            WriteLine(stream, "\\pard\\sect");
                            pbk++;
                            if (pbk < FMatrix.PagesCount)
                                Write(stream, SetPageProp(pbk));
                            if (ShowProgress)
                                Config.ReportSettings.OnProgress(Report,
                                Res.Get("SavePage") + " " + (pbk + 1).ToString());
                        }
                if (pbk == FMatrix.PagesCount - 1)
                    YDiv = Ydivider3;
                else if (pbk > 0)
                    YDiv = Ydivider1;
                else
                    YDiv = Ydivider2;
                drow = (int)Math.Round((FMatrix.YPosById(y + 1) - FMatrix.YPosById(y)) * YDiv);

                StringBuilder buff = new StringBuilder(512);
                buff.Append(FAutoSize ? "\\trrh" : "\\trrh-" + (drow).ToString() + "\\trgaph15");

                xoffs = (int)Math.Round(FMatrix.XPosById(0));                
                StringBuilder CellsStream = new StringBuilder();
                
                for (x = 0; x <= FMatrix.Width - 2; x++)
                {
                    i = FMatrix.Cell(x, y);
                    if (i != -1)
                    {
                        Obj = FMatrix.ObjectById(i);
                        FMatrix.ObjectPos(i, out fx, out fy, out dx, out dy);
                        if (Obj.Counter == -1)
                        {
                            if (dy > 1)
                                buff.Append("\\clvmgf");
                            if (Obj.Style != null)
                            {
                                buff.Append("\\clcbpat").Append(GetRTFColorFromTable(GetRTFColor(Obj.Style.FillColor)));
                                buff.Append(GetRTFVAlignment(Obj.Style.VAlign)).Append(GetRTFBorders(Obj.Style)).Append("\\cltxlrtb");
                                if (Obj.Style.Angle == 90)
                                    buff.Append(" \\cltxtbrl ");
                                else if (Obj.Style.Angle == 270)
                                    buff.Append(" \\cltxbtlr ");
                            }
                            
                            dcol = (int)Math.Round((Obj.Left + Obj.Width - xoffs) * Xdivider);

                            buff.Append("\\cellx").Append(dcol.ToString());

                            if (Obj.IsText)
                            {
                                // Write a text 
                                TextColor = Obj.Style.TextColor;
                                CellsStream.AppendLine(GetRTFString(Obj.Style, Obj.Text, drow, Obj.HtmlTags));
                            }
                            else
                            {
                                // Write a picture
                                CellsStream.Append(GetRTFMetafile(Obj));
                            }
                            Obj.Counter = y + 1;
                        }
                        else 
                        {
                            if ((dy > 1) && (Obj.Counter != y + 1))
                            {
                                buff.Append("\\clvmrg").
                                    Append((Obj.Style != null) ? GetRTFBorders(Obj.Style) : String.Empty).
                                    Append("\\cltxlrtb");
                                dcol = (int)Math.Round((Obj.Left + Obj.Width - xoffs) * Xdivider);
                                buff.Append("\\cellx").Append(dcol.ToString());
                                j = (int)(drow / FONT_DIVIDER);
                                j = j > 20 ? 20 : j;
                                CellsStream.Append("{\\fs").Append(j.ToString());
                                CellsStream.AppendLine("\\cell}");
                                Obj.Counter = y + 1;
                            }
                        }
                    }
                }
                if (CellsStream.Length > 0)
                {
                    WriteLine(stream, "\\trowd");
                    WriteLine(stream, buff.ToString());
                    WriteLine(stream, "\\pard\\intbl");
                    WriteLine(stream, CellsStream.ToString());
                    WriteLine(stream, "\\pard\\intbl{\\trowd");
                    WriteLine(stream, buff.ToString());
                    WriteLine(stream, "\\row}");
                }
            }
            WriteLine(stream, "\\pard\\fs2\\par}"); //insert empty text with minimum size for avoiding creating a new page
            WriteLine(stream, "}");
           
            #region Write a header           
            byte[] b = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(b, 0, (int)stream.Length);           
            stream.SetLength(0);
            WriteLine(stream, GetRTFHeader());                                    
            WriteLine(stream, GetRTFMetaInfo());
            stream.Write(b, 0, b.Length);
            #endregion

            stream.Flush();
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (RTFExportForm form = new RTFExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("RtfFile");
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            FColorTable = new List<string>();
            FFontTable = new List<string>();
            FMatrix = new ExportMatrix();
            if (FWysiwyg)
                FMatrix.Inaccuracy = 0.5f;
            else
                FMatrix.Inaccuracy = 10;
            FMatrix.RotatedAsImage = false;
            FMatrix.PlainRich = false;
            FMatrix.AreaFill = true;
            FMatrix.CropAreaFill = true;
            FMatrix.ShowProgress = ShowProgress;
            FMatrix.Report = Report;
            if (FImageFormat != RTFImageFormat.Metafile)
            {
                FMatrix.FullTrust = false;
                FMatrix.ImageFormat = FImageFormat == RTFImageFormat.Jpeg ?
                  System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png;
                FMatrix.JpegQuality = FJpegQuality;
            }
        }

        /// <inheritdoc/>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);
            FMatrix.AddPageBegin(page);
        }

        /// <inheritdoc/>
        protected override void ExportBand(Base band)
        {
            base.ExportBand(band);
            if (band.Parent == null) return;
            FMatrix.AddBand(band);
        }

        /// <inheritdoc/>
        protected override void ExportPageEnd(ReportPage page)
        {
            FMatrix.AddPageEnd(page);
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            FMatrix.Prepare();
            ExportRTF(Stream);
        }

        #endregion

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
          base.Serialize(writer);
          writer.WriteBool("Wysiwyg", Wysiwyg);
          writer.WriteBool("PageBreaks", PageBreaks);
          writer.WriteBool("Pictures", Pictures);
          writer.WriteValue("ImageFormat", ImageFormat);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RTFExport"/> class.
        /// </summary>
        public RTFExport()
        {
            FDpiFactor = 96f / DrawUtils.ScreenDpi;
            FPageBreaks = true;
            FWysiwyg = true;
            FAutoSize = false;
            FPictures = true;
            FImageFormat = RTFImageFormat.Metafile;
            FJpegQuality = 90;
            FCreator = "FastReport";
            Res = new MyRes("Export,Misc");
        }
    }
}
