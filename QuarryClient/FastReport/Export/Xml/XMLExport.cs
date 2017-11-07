using System;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;
using System.Globalization;
using FastReport.Format;


namespace FastReport.Export.Xml
{
    /// <summary>
    /// Represents the Excel 2003 XML export filter.
    /// </summary>
    public class XMLExport : ExportBase
    {
        #region Constants
        float Xdivider = 1.376f;
        float Ydivider = 1.376f;
        float MargDiv = 25.4F;
        int XLMaxHeight = 409;

        #endregion

        #region Private fields
        private bool FPageBreaks;
        private ExportMatrix FMatrix;
        private bool FWysiwyg;
        private string FCreator;
        private bool FDataOnly;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that determines whether to insert page breaks in the output file or not.
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
        /// Default value is <b>true</b>. In wysiwyg mode, the resulting Excel file will look
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
        /// Gets or sets the name of document creator.
        /// </summary>
        public string Creator
        {
            get { return FCreator; }
            set { FCreator = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether to export the databand rows only.
        /// </summary>
        public bool DataOnly
        {
          get { return FDataOnly; }
          set { FDataOnly = value; }
        }

        #endregion

        #region Private Methods

        private string XmlAlign(HorzAlign horzAlign, VertAlign vertAlign, int angle)
        {
            string Fh = "Left", Fv = "Top";
            if (angle == 0 || angle == 180)
            {
                if (horzAlign == HorzAlign.Left)
                    Fh = "Left";
                else if (horzAlign == HorzAlign.Right)
                    Fh = "Right";
                else if (horzAlign == HorzAlign.Center)
                    Fh = "Center";
                else if (horzAlign == HorzAlign.Justify)
                    Fh = "Justify";
                if (vertAlign == VertAlign.Top)
                    Fv = "Top";
                else if (vertAlign == VertAlign.Bottom)
                    Fv = "Bottom";
                else if (vertAlign == VertAlign.Center)
                    Fv = "Center";
            }
            else if (angle == 90)
            {
                if (horzAlign == HorzAlign.Left)
                    Fv = "Top";
                else if (horzAlign == HorzAlign.Right)
                    Fv = "Bottom";
                else if (horzAlign == HorzAlign.Center)
                    Fv = "Center";
                if (vertAlign == VertAlign.Top)
                    Fh = "Right";
                else if (vertAlign == VertAlign.Bottom)
                    Fh = "Left";
                else if (vertAlign == VertAlign.Center)
                    Fh = "Center";
            }
            else
            {
                if (horzAlign == HorzAlign.Left)
                    Fv = "Bottom";
                else if (horzAlign == HorzAlign.Right)
                    Fv = "Top";
                else if (horzAlign == HorzAlign.Center)
                    Fv = "Center";
                if (vertAlign == VertAlign.Top)
                    Fh = "Right";
                else if (vertAlign == VertAlign.Bottom)
                    Fh = "Left";
                else if (vertAlign == VertAlign.Center)
                    Fh = "Center";
            }
            return "ss:Horizontal=\"" + Fh + "\" ss:Vertical=\"" + Fv + "\"";
        }

        private void ExportXML(Stream stream)
        {            
            int i, x, y, fx, fy, dx, dy;
            ExportIEMObject Obj;
            ExportIEMStyle EStyle;

            StringBuilder builder = new StringBuilder(8448);
            
            builder.AppendLine("<?xml version=\"1.0\"?>");
            builder.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            builder.Append("<?fr-application created=\"").Append(FCreator).AppendLine("\"?>");
            builder.AppendLine("<?fr-application homesite=\"http://www.fast-report.com\"?>");
            builder.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            builder.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            builder.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            builder.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            builder.AppendLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            builder.AppendLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            builder.Append("<Title>").Append(Report.ReportInfo.Name).AppendLine("</Title>");
            builder.Append("<Author>").Append(Report.ReportInfo.Author).AppendLine("</Author>");
            builder.Append("<Created>").Append(DateTime.Now.Date.ToString()).Append("T").Append(DateTime.Now.TimeOfDay.ToString()).AppendLine("Z</Created>");
            builder.Append("<Version>").Append(Report.ReportInfo.Version).AppendLine("</Version>");
            builder.AppendLine("</DocumentProperties>");
            builder.AppendLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            builder.AppendLine("<ProtectStructure>False</ProtectStructure>");
            builder.AppendLine("<ProtectWindows>False</ProtectWindows>");
            builder.AppendLine("</ExcelWorkbook>");

            builder.AppendLine("<Styles>");
            for (x = 0; x < FMatrix.StylesCount; x++)
            {
                EStyle = FMatrix.StyleById(x);                    
                builder.Append("<Style ss:ID=\"s").Append(x.ToString()).AppendLine("\">");
                builder.AppendLine(GetXMLFont(EStyle));
                builder.AppendLine(GetXMLInterior(EStyle));
                builder.AppendLine(GetXMLAlignment(EStyle));
                builder.AppendLine(GetXMLBorders(EStyle));
                builder.Append(GetXMLFormat(EStyle));
                builder.AppendLine("</Style>");
            }
            builder.AppendLine("</Styles>");
            
            builder.AppendLine("<Worksheet ss:Name=\"Page 1\">");
            // add table
            builder.Append("<Table ss:ExpandedColumnCount=\"").Append(FMatrix.Width.ToString()).Append("\"").
                Append(" ss:ExpandedRowCount=\"").Append(FMatrix.Height.ToString()).AppendLine("\" x:FullColumns=\"1\" x:FullRows=\"1\">");
            for (x = 1; x < FMatrix.Width; x++)
                builder.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"").
                    Append(ExportUtils.FloatToString((FMatrix.XPosById(x) - FMatrix.XPosById(x - 1)) / Xdivider)).
                    AppendLine("\"/>");
            WriteBuf(stream, builder);

            for (y = 0; y < FMatrix.Height - 1; y++)
            {
                builder.Append("<Row ss:Height=\"").
                    Append(ExportUtils.FloatToString((FMatrix.YPosById(y + 1) - FMatrix.YPosById(y)) / Ydivider)).
                    AppendLine("\">");
                for (x = 0; x < FMatrix.Width; x++)
                {                        
                    i = FMatrix.Cell(x, y);
                    if (i != -1)
                    {
                        Obj = FMatrix.ObjectById(i);
                        if (Obj.Counter == 0)
                        {
                            builder.Append("<Cell ss:Index=\"").
                                Append(Convert.ToString(x + 1) + "\" ");
                            FMatrix.ObjectPos(i, out fx, out fy, out dx, out dy);
                            Obj.Counter = 1;
                            if (Obj.IsText)
                            {
                                if (dx > 1)
                                    builder.Append("ss:MergeAcross=\"").Append(Convert.ToString(dx++ - 1)).Append("\" ");
                                if (dy > 1)
                                    builder.Append("ss:MergeDown=\"").Append(Convert.ToString(dy++ - 1)).Append("\" ");
                                builder.Append("ss:StyleID=\"s").Append(Obj.StyleIndex.ToString()).AppendLine("\">");

                                decimal value = 0;
                                bool isNumeric = ExportUtils.ParseTextToDecimal(Obj.Text, Obj.Style.Format, out value);
                                string type = isNumeric ? "ss:Type=\"Number\"" : "ss:Type=\"String\"";
                                string data = Obj.HtmlTags ? "ss:Data" : "Data";
                                string xmlns = Obj.HtmlTags ? " xmlns=\"http://www.w3.org/TR/REC-html40\"" : String.Empty;
                                string strValue = isNumeric ?
                                    Convert.ToString(value, CultureInfo.InvariantCulture.NumberFormat) :
                                    ExportUtils.XmlString(Obj.Text, Obj.HtmlTags);
                                builder.Append("<").Append(data).Append(" ").Append(type).Append(xmlns).
                                    Append(">").Append(strValue).Append("</").Append(data).Append("></Cell>");
                            }
                        } 
                    }
                    else
                        builder.Append("<Cell ss:Index=\"").Append(Convert.ToString(x + 1)).Append("\"/>");
                }
                builder.AppendLine("</Row>");
                if (builder.Length > 8192)
                    WriteBuf(stream, builder);
            }                
            builder.AppendLine("</Table>");
            builder.AppendLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            if (FMatrix.PagesCount > 0)
            {
                builder.AppendLine("<PageSetup>");
                if (FMatrix.Landscape(0))
                    builder.AppendLine("<Layout x:Orientation=\"Landscape\"/>");

                builder.AppendLine(string.Format(CultureInfo.InvariantCulture, "<PageMargins x:Bottom=\"{0:F2}\"" +
                                                  " x:Left=\"{1:F2}\"" +
                                                  " x:Right=\"{2:F2}\"" +
                                                  " x:Top=\"{3:F2}\"/>",
                                                  FMatrix.PageBMargin(0) / MargDiv,
                                                  FMatrix.PageLMargin(0) / MargDiv,
                                                  FMatrix.PageRMargin(0) / MargDiv,
                                                  FMatrix.PageTMargin(0) / MargDiv));
                builder.AppendLine("</PageSetup>");
                builder.AppendLine("<Print>");
                builder.AppendLine("<ValidPrinterInfo/>");
                builder.AppendLine("<PaperSizeIndex>" + FMatrix.RawPaperSize(0).ToString() + "</PaperSizeIndex>");
                builder.AppendLine("</Print>");
            }
            builder.AppendLine("</WorksheetOptions>");
            // add page breaks
            if (FPageBreaks)
            {
                builder.AppendLine("<PageBreaks xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                builder.AppendLine("<RowBreaks>");
                int page = 0;
                for (i = 0; i <= FMatrix.Height - 1; i++)
                {
                    if (FMatrix.YPosById(i) >= FMatrix.PageBreak(page))
                    {
                        builder.AppendLine("<RowBreak>");
                        builder.AppendLine(string.Format("<Row>{0}</Row>", i));
                        builder.AppendLine("</RowBreak>");
                        page++;
                    }
                }
                builder.AppendLine("</RowBreaks>");
                builder.AppendLine("</PageBreaks>");
            }
            builder.AppendLine("</Worksheet>");
            builder.AppendLine("</Workbook>");
            WriteBuf(stream, builder);
        }

        private void WriteBuf(Stream stream, StringBuilder buf)
        {
            // write the resulting string to a stream
            byte[] bytes = Encoding.UTF8.GetBytes(buf.ToString());
            stream.Write(bytes, 0, bytes.Length);
            buf.Length = 0;
        }

        private string GetXMLBorders(ExportIEMStyle style)
        {
            StringBuilder result = new StringBuilder(128);
            result.AppendLine("<Borders>");
            if ((style.Border.Lines & BorderLines.Left) > 0)
                result.Append("<Border ss:Position=\"Left\" ").
                    Append("ss:LineStyle=\"").
                    AppendFormat(GetXMLLineStyle(style.Border.LeftLine.Style)).
                    Append("\" ").
                    Append("ss:Weight=\"").
                    Append(GetXMLWeight(style.Border.LeftLine.Width)).
                    Append("\" ").
                    Append("ss:Color=\"").
                    Append(ExportUtils.HTMLColorCode(style.Border.LeftLine.Color)).
                    AppendLine("\"/>");
            if ((style.Border.Lines & BorderLines.Top) > 0)
                result.Append("<Border ss:Position=\"Top\" ").
                    Append("ss:LineStyle=\"").
                    AppendFormat(GetXMLLineStyle(style.Border.TopLine.Style)).
                    Append("\" ").
                    Append("ss:Weight=\"").
                    Append(GetXMLWeight(style.Border.TopLine.Width)).
                    Append("\" ").
                    Append("ss:Color=\"").
                    Append(ExportUtils.HTMLColorCode(style.Border.TopLine.Color)).
                    AppendLine("\"/>");
            if ((style.Border.Lines & BorderLines.Bottom) > 0)
                result.AppendLine("<Border ss:Position=\"Bottom\" ").
                    Append("ss:LineStyle=\"").
                    AppendFormat(GetXMLLineStyle(style.Border.BottomLine.Style)).
                    Append("\" ").
                    Append("ss:Weight=\"").
                    Append(GetXMLWeight(style.Border.BottomLine.Width)).
                    Append("\" ").
                    Append("ss:Color=\"").
                    Append(ExportUtils.HTMLColorCode(style.Border.BottomLine.Color)).
                    AppendLine("\"/>");
            if ((style.Border.Lines & BorderLines.Right) > 0)
                result.AppendLine("<Border ss:Position=\"Right\" ").
                    Append("ss:LineStyle=\"").
                    AppendFormat(GetXMLLineStyle(style.Border.RightLine.Style)).
                    Append("\" ").
                    Append("ss:Weight=\"").
                    Append(GetXMLWeight(style.Border.RightLine.Width)).
                    Append("\" ").
                    Append("ss:Color=\"").
                    Append(ExportUtils.HTMLColorCode(style.Border.RightLine.Color)).
                    AppendLine("\"/>");
            result.Append("</Borders>");
            return result.ToString();
        }

        private string GetXMLLineStyle(LineStyle style)
        {
            switch (style)
            {
                case LineStyle.Dash:
                    return "Dash";
                case LineStyle.DashDot:
                    return "DashDot";
                case LineStyle.DashDotDot:
                    return "DashDotDot";
                case LineStyle.Dot:
                    return "Dot";
                case LineStyle.Double:
                    return "Double";
                default:
                    return "Continuous";
            }            
        }

        private string GetXMLAlignment(ExportIEMStyle style)
        {
            StringBuilder result = new StringBuilder(64);
            result.Append("<Alignment ").Append(XmlAlign(style.HAlign, style.VAlign, style.Angle)).
                Append(" ss:WrapText=\"1\" ").
                Append(((style.Angle > 0 && style.Angle <= 90) ? "ss:Rotate=\"" + (-style.Angle).ToString() + "\"" : String.Empty)).
                Append(((style.Angle > 90 && style.Angle <= 180) ? "ss:Rotate=\"" + (180 - style.Angle).ToString() + "\"" : String.Empty)).
                Append(((style.Angle > 180 && style.Angle < 270) ? "ss:Rotate=\"" + (270 - style.Angle).ToString() + "\"" : String.Empty)).
                Append(((style.Angle >= 270 && style.Angle < 360) ? "ss:Rotate=\"" + (360 - style.Angle).ToString() + "\"" : String.Empty)).
                Append("/>");
            return result.ToString();
        }

        private string GetXMLInterior(ExportIEMStyle style)
        {
            if (style.FillColor.A != 0)  
                return "<Interior ss:Color=\"" +
                    ExportUtils.HTMLColorCode(style.FillColor) + "\" ss:Pattern=\"Solid\"/>";
            return String.Empty;
        }

        private string GetXMLFont(ExportIEMStyle style)
        {
            StringBuilder result = new StringBuilder(128);
            result.Append("<Font ss:FontName=\"").Append(style.Font.Name).Append("\" ss:Size=\"").
                Append(ExportUtils.FloatToString(style.Font.Size)).Append("\" ss:Color=\"").
                Append(ExportUtils.HTMLColorCode(style.TextColor)).Append("\" ").
                Append(((style.Font.Style & FontStyle.Bold) > 0 ? "ss:Bold=\"1\" " : String.Empty)).
                Append(((style.Font.Style & FontStyle.Italic) > 0 ? "ss:Italic=\"1\" " : String.Empty)).
                Append(((style.Font.Style & FontStyle.Underline) > 0 ? "ss:Underline=\"Single\" " : String.Empty)).
                Append("/>");
            return result.ToString();
        }

        private string GetXMLWeight(float lineWeight)
        {
            float LineWeight = lineWeight * Xdivider;
            return ((int)Math.Round(LineWeight > 3 ? 3 : LineWeight)).ToString();
        }

        private string GetXMLFormat(ExportIEMStyle style)
        {
          if (style.Format is NumberFormat || style.Format is CurrencyFormat)
          {
            return "<NumberFormat ss:Format=\"" + ExportUtils.GetExcelFormatSpecifier(style.Format) + "\"/>\r\n";
          }
          return String.Empty;
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (XMLExportForm form = new XMLExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("XlsFile");
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            FMatrix = new ExportMatrix();
            if (FWysiwyg)
                FMatrix.Inaccuracy = 0.5f;
            else
                FMatrix.Inaccuracy = 10;
            FMatrix.RotatedAsImage = false;
            FMatrix.PlainRich = true;
            FMatrix.AreaFill = true;
            FMatrix.CropAreaFill = true;
            FMatrix.Report = Report;
            FMatrix.Images = false;
            FMatrix.DataOnly = DataOnly;
            FMatrix.ShowProgress = ShowProgress;
            FMatrix.MaxCellHeight = Ydivider * XLMaxHeight;
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
            MyRes Res = new MyRes("Export,Misc");
            if (ShowProgress)
                Config.ReportSettings.OnProgress(Report, Res.Get("SaveFile"));
            ExportXML(Stream);
        }

        #endregion

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
          base.Serialize(writer);
          writer.WriteBool("Wysiwyg", Wysiwyg);
          writer.WriteBool("PageBreaks", PageBreaks);
          writer.WriteBool("DataOnly", DataOnly);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLExport"/> class.
        /// </summary>       
        public XMLExport()
        {                         
            FPageBreaks = true;
            FWysiwyg = true;
            FCreator = "FastReport";
        }
    }
}
