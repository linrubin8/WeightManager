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
using System.Globalization;


namespace FastReport.Export.Odf
{
    /// <summary>
    /// Open Document Spreadsheet export (Open Office Calc)
    /// </summary>
    public class ODSExport : ODFExport
    {
        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("OdsFile");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODSExport"/> class.
        /// </summary>
        public ODSExport()
        {
            ExportType = 0;            
        }
    }

    /// <summary>
    /// Open Document Text export (Open Office Writer)
    /// </summary>
    public class ODTExport : ODFExport
    {
        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("OdtFile");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODTExport"/> class.
        /// </summary>
        public ODTExport()
        {
            ExportType = 1;
        }
    }

    /// <summary>
    /// Base class for any ODF exports
    /// </summary>
    public class ODFExport : ExportBase
    {
        #region Constants
        const float odfDivider = 37.82f;
        const float odfPageDiv = 10f;
        const float odfMargDiv = 10f;
        #endregion

        #region Private fields
        private bool FPageBreaks;
        private ExportMatrix FMatrix;
        private bool FWysiwyg;
        private string FCreator;
        private int FExportType;

        private float FPageLeft;
        private float FPageTop;
        private float FPageBottom;
        private float FPageRight;
        private bool FPageLandscape;
        private float FPageWidth;
        private float FPageHeight;

        private bool FFirstPage;

        #endregion

        #region Properties

        internal int ExportType
        {
            get { return FExportType; }
            set { FExportType = value; }
        }

        /// <summary>
        /// Switch of page breaks
        /// </summary>
        public bool PageBreaks
        {
            get { return FPageBreaks; }
            set { FPageBreaks = value; }
        }
                
        /// <summary>
        /// Wysiwyg mode, set for better results
        /// </summary>
        public bool Wysiwyg
        {
            get { return FWysiwyg; }
            set { FWysiwyg = value; }
        }
        
        /// <summary>
        /// Creator of the document
        /// </summary>
        public string Creator
        {
            get { return FCreator; }
            set { FCreator = value; }
        }

        #endregion

        #region Private Methods

        private void Write(Stream stream, string value)
        {
            byte[] buf = Encoding.UTF8.GetBytes(value);
            stream.Write(buf, 0, buf.Length);
        }

        private void WriteLine(Stream stream, string value)
        {
            byte[] buf = Encoding.UTF8.GetBytes(value);
            stream.Write(buf, 0, buf.Length);
            stream.WriteByte(13);
            stream.WriteByte(10);
        }

        private void OdfCreateMeta(ZipArchive zip, string FileName, string Creator)
        {
            StringBuilder sb = new StringBuilder(570);            
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<office:document-meta xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\" ").
                Append("xmlns:xlink=\"http://www.w3.org/1999/xlink\" ").
                Append("xmlns:dc=\"http://purl.org/dc/elements/1.1/\" ").
                AppendLine("xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\">");
            sb.AppendLine("  <office:meta>");
            sb.Append("    <meta:generator>fast-report.com/Fast Report.NET/build:").Append(Config.Version).AppendLine("</meta:generator>");
            sb.Append("    <meta:initial-creator>").Append(ExportUtils.XmlString(Creator, false)).AppendLine("</meta:initial-creator>");
            sb.Append("    <meta:creation-date>").Append(DateTime.Now.Date.ToShortDateString()).Append("T").
                Append(DateTime.Now.TimeOfDay.ToString()).AppendLine("</meta:creation-date>");
            sb.AppendLine("  </office:meta>");
            sb.AppendLine("</office:document-meta>");
            MemoryStream file = new MemoryStream();
            Write(file, sb.ToString());
            zip.AddStream(FileName, file);
        }

        private void OdfCreateMime(ZipArchive zip, string FileName, string MValue)
        {
            MemoryStream file = new MemoryStream();                             
            Write(file, "application/vnd.oasis.opendocument." + MValue);
            zip.AddStream(FileName, file);
        }

        private void OdfCreateManifest(ZipArchive zip, string FileName, int PicCount, string MValue)
        {
            MemoryStream file = new MemoryStream();
            WriteLine(file, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            WriteLine(file, "<manifest:manifest xmlns:manifest=\"urn:oasis:names:tc:opendocument:xmlns:manifest:1.0\">");
            WriteLine(file, "  <manifest:file-entry manifest:media-type=\"application/vnd.oasis.opendocument." + MValue + "\" manifest:full-path=\"/\"/>");
            WriteLine(file, "  <manifest:file-entry manifest:media-type=\"text/xml\" manifest:full-path=\"content.xml\"/>");
            WriteLine(file, "  <manifest:file-entry manifest:media-type=\"text/xml\" manifest:full-path=\"content.xml\"/>");
            WriteLine(file, "  <manifest:file-entry manifest:media-type=\"text/xml\" manifest:full-path=\"styles.xml\"/>");
            WriteLine(file, "  <manifest:file-entry manifest:media-type=\"text/xml\" manifest:full-path=\"meta.xml\"/>");
            string s = ".png"; //Config.FullTrust ? ".emf" : 
            for (int i = 1; i <= PicCount; i++)
                WriteLine(file, "  <manifest:file-entry  manifest:media-type=\"image\" manifest:full-path=\"Pictures/Pic" + i.ToString() + s + "\"/>");
            WriteLine(file, "</manifest:manifest>");
            zip.AddStream(FileName, file);
        }

        private string OdfGetFrameName(LineStyle Style)
        {
            if (Style == LineStyle.Double)
                return "double";
            else
                return "solid";
        }

        private string OdfMakeXmlHeader()
        {
            return " xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\"" +
                " xmlns:style=\"urn:oasis:names:tc:opendocument:xmlns:style:1.0\"" +
                " xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\"" +
                " xmlns:table=\"urn:oasis:names:tc:opendocument:xmlns:table:1.0\"" +
                " xmlns:draw=\"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0\"" +
                " xmlns:fo=\"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0\"" +
                " xmlns:xlink=\"http://www.w3.org/1999/xlink\"" +
                " xmlns:dc=\"http://purl.org/dc/elements/1.1/\"" +
                " xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\"" +
                " xmlns:number=\"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0\"" +
                " xmlns:svg=\"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0\"" +
                " xmlns:chart=\"urn:oasis:names:tc:opendocument:xmlns:chart:1.0\"" +
                " xmlns:dr3d=\"urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0\"" +
                " xmlns:math=\"http://www.w3.org/1998/Math/MathML\"" +
                " xmlns:form=\"urn:oasis:names:tc:opendocument:xmlns:form:1.0\"" +
                " xmlns:script=\"urn:oasis:names:tc:opendocument:xmlns:script:1.0\"" +
                " xmlns:dom=\"http://www.w3.org/2001/xml-events\"" +
                " xmlns:xforms=\"http://www.w3.org/2002/xforms\"" +
                " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"" +
                " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
        }

        private void OdfMakeDocStyles(ZipArchive zip, string FileName)
        {
            MemoryStream file = new MemoryStream();
            WriteLine(file, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Write(file, "<office:document-styles ");
            Write(file, OdfMakeXmlHeader());
            WriteLine(file, ">");
            WriteLine(file, "<office:automatic-styles>");
            // rework!
            WriteLine(file, "<style:page-layout style:name=\"pm1\">");
            WriteLine(file, "<style:page-layout-properties " +
                "fo:page-width=\"" + ExportUtils.FloatToString(FPageWidth / odfPageDiv) + "cm\" " +
                "fo:page-height=\"" + ExportUtils.FloatToString(FPageHeight / odfPageDiv) + "cm\" " +
                "fo:margin-top=\"" + ExportUtils.FloatToString(FPageTop / odfMargDiv) + "cm\" " +
                "fo:margin-bottom=\"" + ExportUtils.FloatToString(FPageBottom / odfMargDiv) + "cm\" " +
                "fo:margin-left=\"" + ExportUtils.FloatToString(FPageLeft / odfMargDiv) + "cm\" " +
                "fo:margin-right=\"" + ExportUtils.FloatToString(FPageRight / odfMargDiv) + "cm\"/>");
            WriteLine(file, "</style:page-layout>");
            WriteLine(file, "</office:automatic-styles>");
            WriteLine(file, "<office:master-styles>");
            WriteLine(file, "<style:master-page style:name=\"PageDef\" style:page-layout-name=\"pm1\">");
            WriteLine(file, "<style:header style:display=\"false\"/>");
            WriteLine(file, "<style:footer style:display=\"false\"/>");
            WriteLine(file, "</style:master-page>");
            WriteLine(file, "</office:master-styles>");
            WriteLine(file, "</office:document-styles>");
            zip.AddStream(FileName, file);
        }

        private void OdfTableCellStyles(ExportIEMStyle Style, Stream file)
        {
            Write(file, "<style:table-cell-properties fo:background-color=\"" +
                ExportUtils.HTMLColorCode(Style.FillColor) + "\" " +
                "style:repeat-content=\"false\" fo:wrap-option=\"wrap\" ");
            if (Style.Angle > 0)
            {
                Write(file, "style:rotation-angle=\"" + (360 - Style.Angle).ToString() + "\" " +
                    "style:rotation-align=\"none\" ");
            }
            if (Style.VAlign == VertAlign.Center)
                Write(file, "style:vertical-align=\"middle\" ");
            if (Style.VAlign == VertAlign.Top)
                Write(file, "style:vertical-align=\"top\" ");
            if (Style.VAlign == VertAlign.Bottom)
                Write(file, "style:vertical-align=\"bottom\" ");
            if ((Style.Border.Lines & BorderLines.Left) > 0)
                Write(file, "fo:border-left=\"" +
                    ExportUtils.FloatToString(Style.Border.Width / odfDivider) + "cm " +
                    OdfGetFrameName(Style.Border.Style) + " " +
                    ExportUtils.HTMLColorCode(Style.Border.Color) + "\" ");
            if ((Style.Border.Lines & BorderLines.Right) > 0)
                Write(file, "fo:border-right=\"" +
                    ExportUtils.FloatToString(Style.Border.Width / odfDivider) + "cm " +
                    OdfGetFrameName(Style.Border.Style) + " " +
                    ExportUtils.HTMLColorCode(Style.Border.Color) + "\" ");
            if ((Style.Border.Lines & BorderLines.Top) > 0)
                Write(file, "fo:border-top=\"" +
                    ExportUtils.FloatToString(Style.Border.Width / odfDivider) + "cm " +
                    OdfGetFrameName(Style.Border.Style) + " " +
                    ExportUtils.HTMLColorCode(Style.Border.Color) + "\" ");
            if ((Style.Border.Lines & BorderLines.Bottom) > 0)
                Write(file, "fo:border-bottom=\"" +
                    ExportUtils.FloatToString(Style.Border.Width / odfDivider) + "cm " +
                    OdfGetFrameName(Style.Border.Style) + " " +
                    ExportUtils.HTMLColorCode(Style.Border.Color) + "\" ");
            WriteLine(file, "/>");
            WriteLine(file, "</style:style>");
        }

        private void OdfFontFaceDecals(Stream file)
        {
            List<string> FList = new List<string>();
            ExportIEMStyle Style;
            for (int i = 0; i < FMatrix.StylesCount; i++)
            {
                Style = FMatrix.StyleById(i);
                if ((Style.Font != null) && (FList.IndexOf(Style.Font.Name) == -1))
                    FList.Add(Style.Font.Name);
            }
            WriteLine(file, "<office:font-face-decls>");
            FList.Sort();
            for (int i = 0; i < FList.Count; i++)
                WriteLine(file, "<style:font-face style:name=\"" + FList[i] +
                    "\" svg:font-family=\"&apos;" + FList[i] + "&apos;\" " +
                    "style:font-pitch=\"variable\"/>");
            WriteLine(file, "</office:font-face-decls>");
        }

        private void OdfColumnStyles(Stream file)
        {
            List<string> FList = new List<string>();
            string s;
            for (int i = 1; i < FMatrix.Width; i++)
            {
                s = ExportUtils.FloatToString((FMatrix.XPosById(i) - FMatrix.XPosById(i - 1)) / odfDivider);
                if (FList.IndexOf(s) == -1)
                    FList.Add(s);
            }
            FList.Sort();
            for (int i = 0; i < FList.Count; i++)
            {
                WriteLine(file, "<style:style style:name=\"co" + FList[i] + "\" " +
                    "style:family=\"table-column\">");
                WriteLine(file, "<style:table-column-properties fo:break-before=\"auto\" " +
                    "style:column-width=\"" + FList[i] + "cm\"/>");
                WriteLine(file, "</style:style>");
            }
        }

        private void OdfRowStyles(Stream file)
        {
            List<string> FList = new List<string>();
            string s;
            for (int i = 0; i < FMatrix.Height - 1; i++)
            {
                s = ExportUtils.FloatToString((FMatrix.YPosById(i + 1) - FMatrix.YPosById(i)) / odfDivider);
                if (FList.IndexOf(s) == -1)
                    FList.Add(s);
            }
            FList.Sort();
            for (int i = 0; i < FList.Count; i++)
            {
                WriteLine(file, "<style:style style:name=\"ro" + FList[i] + "\" " +
                    "style:family=\"table-row\">");
                WriteLine(file, "<style:table-row-properties fo:break-before=\"auto\" " +
                    "style:row-height=\"" + FList[i] + "cm\"/>");
                WriteLine(file, "</style:style>");
            }
            WriteLine(file, "<style:style style:name=\"ro_breaked\" " +
                "style:family=\"table-row\">");
            WriteLine(file, "<style:table-row-properties fo:break-before=\"page\" " +
                "style:row-height=\"0.001cm\"/>");
            WriteLine(file, "</style:style>");
            WriteLine(file, "<style:style style:name=\"ta1\" style:family=\"table\" style:master-page-name=\"PageDef\">");
            WriteLine(file, "<style:table-properties table:display=\"true\" style:writing-mode=\"lr-tb\"/>");
            WriteLine(file, "</style:style>");
//            WriteLine(file, "<style:style style:name=\"ceb\" style:family=\"table-cell\" style:display=\"false\"/>");
            WriteLine(file, "<style:style style:name=\"ceb\" style:family=\"table-cell\" />");
        }

        private void ExportODF(Stream stream)
        {
            string s;
            int fx, fy, dx, dy;

            ZipArchive zip = new ZipArchive();

            int PicCount = 0;
            //int Page = 0;

            string ExportMime = FExportType == 0 ? "spreadsheet" : "text";
            OdfCreateMime(zip, "mimetype", ExportMime);
            OdfMakeDocStyles(zip, "styles.xml");

            #region Content.xml

            MemoryStream file = new MemoryStream();
            WriteLine(file, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Write(file, "<office:document-content ");
            Write(file, OdfMakeXmlHeader());
            WriteLine(file, ">");
            WriteLine(file, "<office:scripts/>");
            ExportIEMStyle Style;
            OdfFontFaceDecals(file);
            WriteLine(file, "<office:automatic-styles>");
            OdfColumnStyles(file);
            OdfRowStyles(file);

            for (int i = 0; i < FMatrix.StylesCount; i++)
            {
                Style = FMatrix.StyleById(i);
                WriteLine(file, "<style:style style:name=\"ce" + i.ToString() + "\" " +
                    "style:family=\"table-cell\" >"); //style:parent-style-name=\"Default\"
                if (FExportType == 0)
                {

                    Write(file, "<style:paragraph-properties ");
                    if (Style.HAlign == HorzAlign.Left)
                        Write(file, "fo:text-align=\"start\" ");
                    if (Style.HAlign == HorzAlign.Center)
                        Write(file, "fo:text-align=\"center\" ");
                    if (Style.HAlign == HorzAlign.Right)
                        Write(file, "fo:text-align=\"end\" ");
                    if (Style.Padding.Left > 0)
                        Write(file, "fo:margin-left=\"" +
                            ExportUtils.FloatToString(Style.Padding.Left / odfDivider) + "cm\" ");
                    if (Style.Padding.Right > 0)
                        Write(file, "fo:margin-right=\"" +
                            ExportUtils.FloatToString(Style.Padding.Right / odfDivider) + "cm\" ");
                    if (Style.Padding.Top > 0)
                        Write(file, "fo:margin-top=\"" +
                            ExportUtils.FloatToString(Style.Padding.Top / odfDivider) + "cm\" ");
                    if (Style.Padding.Bottom > 0)
                        Write(file, "fo:margin-bottom=\"" +
                            ExportUtils.FloatToString(Style.Padding.Bottom / odfDivider) + "cm\" ");
                    WriteLine(file, "/>");
                    
                    Write(file, "<style:text-properties style:font-name=\"" + Style.Font.Name + "\" " +
                        "fo:font-size=\"" + ExportUtils.FloatToString(Style.Font.Size) + "pt\" ");
                    if ((Style.Font.Style & FontStyle.Underline) > 0)
                    {
                        Write(file, "style:text-underline-style=\"solid\" " +
                            "style:text-underline-width=\"auto\" " +
                            "style:text-underline-color=\"font-color\" ");
                    }
                    if ((Style.Font.Style & FontStyle.Italic) > 0)
                        Write(file, "fo:font-style=\"italic\" ");
                    if ((Style.Font.Style & FontStyle.Bold) > 0)
                        Write(file, "fo:font-weight=\"bold\" ");
                    Write(file, "fo:color=\"" + ExportUtils.HTMLColorCode(Style.TextColor) + "\"");
                    WriteLine(file, "/>");

                }
                OdfTableCellStyles(Style, file);
            }

            if (FExportType == 1)
            {
                WriteLine(file, "<style:style style:name=\"PB\" " +
//                    "style:family=\"paragraph\" style:display=\"false\"/>");
                    "/>"); //style:family=\"paragraph\" 
                for (int i = 0; i < FMatrix.StylesCount; i++)
                {
                    Style = FMatrix.StyleById(i);
                    WriteLine(file, "<style:style style:name=\"P" + i.ToString() + "\" " +
                        "style:family=\"paragraph\" >"); //style:parent-style-name=\"Default\"

                    Write(file, "<style:text-properties style:font-name=\"" +
                        Style.Font.Name + "\" fo:font-size=\"" +
                        ExportUtils.FloatToString(Style.Font.Size) + "pt\" ");
                    if ((Style.Font.Style & FontStyle.Underline) > 0)
                        Write(file, " style:text-underline-style=\"solid\" " +
                            "style:text-underline-width=\"auto\" " +
                            "style:text-underline-color=\"font-color\" ");
                    if ((Style.Font.Style & FontStyle.Italic) > 0)
                        Write(file, " style:font-style=\"italic\" ");
                    if ((Style.Font.Style & FontStyle.Bold) > 0)
                        Write(file, " fo:font-weight=\"bold\" style:font-weight-asian=\"bold\" style:font-weight-complex=\"bold\""); 
                    WriteLine(file, " fo:color=\"" +
                        ExportUtils.HTMLColorCode(Style.TextColor) + "\"/>");

                    Write(file, "<style:paragraph-properties ");
                    if (Style.HAlign == HorzAlign.Left)
                        Write(file, "fo:text-align=\"start\" ");
                    if (Style.HAlign == HorzAlign.Center)
                        Write(file, "fo:text-align=\"center\" ");
                    if (Style.HAlign == HorzAlign.Right)
                        Write(file, "fo:text-align=\"end\" ");
                    if (Style.Padding.Left > 0)
                        Write(file, "fo:margin-left=\"" +
                            ExportUtils.FloatToString(Style.Padding.Left / odfDivider) + "cm\" ");
                    if (Style.Padding.Right > 0)
                        Write(file, "fo:margin-right=\"" +
                            ExportUtils.FloatToString(Style.Padding.Right / odfDivider) + "cm\" ");
                    if (Style.Padding.Top > 0)
                        Write(file, "fo:margin-top=\"" +
                            ExportUtils.FloatToString(Style.Padding.Top / odfDivider) + "cm\" ");
                    if (Style.Padding.Bottom > 0)
                        Write(file, "fo:margin-bottom=\"" +
                            ExportUtils.FloatToString(Style.Padding.Bottom / odfDivider) + "cm\" ");
                    WriteLine(file, "/>");
                    WriteLine(file, "</style:style>");
                }
            }

            WriteLine(file, "<style:style style:name=\"gr1\" style:family=\"graphic\">");
            WriteLine(file, "<style:graphic-properties draw:stroke=\"none\" " +
                "draw:fill=\"none\" draw:textarea-horizontal-align=\"left\" " +
                "draw:textarea-vertical-align=\"top\" draw:color-mode=\"standard\" " +
                "draw:luminance=\"0%\" draw:contrast=\"0%\" draw:gamma=\"100%\" " +
                "draw:red=\"0%\" draw:green=\"0%\" draw:blue=\"0%\" " +
                "fo:clip=\"rect(0cm 0cm 0cm 0cm)\" draw:image-opacity=\"100%\" " +
                "style:mirror=\"none\"/>");
            WriteLine(file, "</style:style>");

            WriteLine(file, "</office:automatic-styles>");

            // body
            WriteLine(file, "<office:body>");
            WriteLine(file, "<office:spreadsheet>");
            WriteLine(file, "<table:table table:name=\"Table\" table:style-name=\"ta1\" table:print=\"false\">");

            for (int x = 1; x < FMatrix.Width; x++)
                WriteLine(file, "<table:table-column table:style-name=\"co" +
                    ExportUtils.FloatToString((FMatrix.XPosById(x) -
                    FMatrix.XPosById(x - 1)) / odfDivider) + "\"/>");

            for (int y = 0; y < FMatrix.Height - 1; y++)
            {
                //if (FMatrix.YPosById(y) >= FMatrix.PageBreak(Page))
                //{
                //    Page++;
                //    if (FPageBreaks)
                //        WriteLine(file, "<table:table-row table:style-name=\"ro_breaked\"/>");
                //}
                WriteLine(file, "<table:table-row table:style-name=\"ro" +
                    ExportUtils.FloatToString((FMatrix.YPosById(y + 1) -
                    FMatrix.YPosById(y)) / odfDivider) + "\">");
                for (int x = 0; x < FMatrix.Width; x++)
                {
                    int i = FMatrix.Cell(x, y);
                    if (i != -1)
                    {
                        ExportIEMObject Obj = FMatrix.ObjectById(i);
                        if (Obj.Counter == 0)
                        {
                            Obj.Counter = 1;
                            FMatrix.ObjectPos(i, out fx, out fy, out dx, out dy);

                            Write(file, "<table:table-cell table:style-name=\"ce" +
                                Obj.StyleIndex.ToString() + "\" ");
                            if (dx > 1)
                                Write(file, "table:number-columns-spanned=\"" + dx.ToString() + "\" ");
                            if (dy > 1)
                                Write(file, "table:number-rows-spanned=\"" + dy.ToString() + "\" ");
                            WriteLine(file, ">");
                            if (Obj.IsText)
                            {
                                s = ExportUtils.OdtString(Obj.Text, Obj.HtmlTags);
                                Write(file, "<text:p");
                                if (FExportType == 1)
                                    Write(file, " text:style-name=\"P" + Obj.StyleIndex.ToString() + "\"");
                                WriteLine(file, ">" + s + "</text:p>");
                            }
                            else
                            {
                                if (Obj.Width > 0)
                                {
                                    PicCount++;
                                    s = ".png"; //Config.FullTrust ? ".emf" : 
                                    zip.AddStream("Pictures/Pic" + PicCount.ToString() + s, Obj.PictureStream);
                                    if (FExportType == 1)
                                        Write(file, "<text:p>");
                                    
                                    Write(file, "<draw:frame draw:z-index=\"" + (PicCount - 1).ToString() + "\" " +
                                        "draw:name=\"Pictures" + PicCount.ToString() + "\" " +
                                        "draw:style-name=\"gr1\" " +
                                        //"draw:text-style-name=\"P1\" " +
                                        "svg:width=\"" + ExportUtils.FloatToString(Obj.Width / odfDivider) + "cm\" " +
                                        "svg:height=\"" + ExportUtils.FloatToString(Obj.Height / odfDivider) + "cm\" " +
                                        "svg:x=\"0cm\" svg:y=\"0cm\">");
                                    Write(file, "<draw:image " +
                                        "xlink:href=\"Pictures/Pic" + PicCount.ToString() + s + "\" " +
                                        "text:anchor-type=\"frame\" xlink:type=\"simple\" xlink:show=\"embed\" xlink:actuate=\"onLoad\"/>");
                                    Write(file, "</draw:frame>");
                                    if (FExportType == 1)
                                        WriteLine(file, "</text:p>");
                                }
                            }
                            WriteLine(file, "</table:table-cell>");
                        }
                        else
                        {
                            Write(file, "<table:covered-table-cell table:style-name=\"ceb\"");
                            if (FExportType == 1)
                                WriteLine(file, "><text:p text:style-name=\"PB\"/></table:covered-table-cell>");
                            else
                                WriteLine(file, "/>");
                        }
                    }
                    else
                    {                        
                        Write(file, "<table:table-cell");
                        if (FExportType == 1)
                            WriteLine(file, "><text:p text:style-name=\"PB\"/></table:table-cell>");
                        else
                            WriteLine(file, "/>");                            
                    }
                }
                WriteLine(file, "</table:table-row>");
            }
            WriteLine(file, "</table:table>");
            WriteLine(file, "</office:spreadsheet>");
            WriteLine(file, "</office:body>");
            WriteLine(file, "</office:document-content>");
            zip.AddStream("content.xml", file);
            #endregion

            OdfCreateManifest(zip, "META-INF/manifest.xml", PicCount, ExportMime);
            OdfCreateMeta(zip, "meta.xml", Creator);

            zip.SaveToStream(Stream);
            zip.Clear();
        }

        private string GetCellPos(int x, int y)
        {
            return (char)(x + (byte)'A') + (y + 1).ToString();
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (ODFExportForm form = new ODFExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            FMatrix = new ExportMatrix();
            if (FWysiwyg)
                FMatrix.Inaccuracy = 0.5f;
            else
                FMatrix.Inaccuracy = 10;
            FMatrix.RotatedAsImage = true;// false;
            FMatrix.PlainRich = true;
            FMatrix.AreaFill = true;
            //FMatrix.CropAreaFill = true;
            FMatrix.Report = Report;
            FMatrix.MaxCellHeight = 400;            
            FMatrix.Images = true;
            FMatrix.ImageFormat = ImageFormat.Png;
            FMatrix.ShowProgress = ShowProgress;
            FFirstPage = true;
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
            if (FFirstPage)
            {
                FPageBottom = page.BottomMargin;
                FPageLeft = page.LeftMargin;
                FPageRight = page.RightMargin;
                FPageTop = page.TopMargin;
                FPageWidth = ExportUtils.GetPageWidth(page);
                FPageHeight = ExportUtils.GetPageHeight(page);
                FPageLandscape = page.Landscape;
                FFirstPage = false;
            }
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            FMatrix.Prepare();
            ExportODF(Stream);
        }

        #endregion


        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
          base.Serialize(writer);
          writer.WriteBool("Wysiwyg", Wysiwyg);
          writer.WriteBool("PageBreaks", PageBreaks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODFExport"/> class.
        /// </summary>
        public ODFExport()
        {
            FExportType = 0;
            FPageBreaks = true;
            FWysiwyg = true;
            FCreator = "FastReport";
        }
    }
}
