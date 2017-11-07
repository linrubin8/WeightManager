using FastReport.Table;
using FastReport.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Export.Html
{
    /// <summary>
    /// Represents the HTML export filter.
    /// </summary>
    public partial class HTMLExport : ExportBase
    {
        private bool doPageBreak;

        private string GetStyle(Font Font, Color TextColor, Color FillColor, 
            bool RTL, HorzAlign HAlign, Border Border, bool WordWrap, float LineHeight, float Width, float Height, bool Clip)
        {
            FastString style = new FastString(256);

            if (Font != null)
            {
                if (Zoom != 1)
                {
                    using (Font newFont = new Font(Font.FontFamily, Font.Size * Zoom, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont))
                        HTMLFontStyle(style, newFont, LineHeight);
                }
                else
                    HTMLFontStyle(style, Font, LineHeight);
            }
            style.Append("text-align:");
            if (HAlign == HorzAlign.Left)
                style.Append(RTL ? "right" : "left");
            else if (HAlign == HorzAlign.Right)
                style.Append(RTL ? "left" : "right");
            else if (HAlign == HorzAlign.Center)
                style.Append("center");
            else
                style.Append("justify");
            style.Append(";");

            if (WordWrap)
                style.Append("word-wrap:break-word;");

            if (Clip)
                style.Append("overflow:hidden;");

            style.Append("position:absolute;color:"). 
                Append(ExportUtils.HTMLColor(TextColor)).
                Append(";background-color:").
                Append(FillColor.A == 0 ? "transparent" : ExportUtils.HTMLColor(FillColor)).
                Append(";").Append(RTL ? "direction:rtl;" : String.Empty);

            Border newBorder = Border;
            HTMLBorder(style, newBorder);
            style.Append("width:").Append(Px(Width * Zoom)).Append("height:").Append(Px(Height * Zoom));
            return style.ToString();
        }

        private int UpdateCSSTable(ReportComponentBase obj)
        {
            string style;
            if (obj is TextObject)
            {
                TextObject textObj = obj as TextObject;
                style = GetStyle(textObj.Font, textObj.TextColor, textObj.FillColor, 
                    textObj.RightToLeft, textObj.HorzAlign, textObj.Border, textObj.WordWrap, textObj.LineHeight,
                    textObj.Width, textObj.Height, textObj.Clip);
            }
            else if (obj is HtmlObject)
            {
                HtmlObject htmlObj = obj as HtmlObject;
                style = GetStyle(DrawUtils.DefaultTextObjectFont, Color.Black, htmlObj.FillColor,
                    false, HorzAlign.Left, htmlObj.Border, true, 0, htmlObj.Width, htmlObj.Height, false);
            }
            else
                style = GetStyle(null, Color.White, obj.FillColor, false, HorzAlign.Center, obj.Border, false, 0, obj.Width, obj.Height, false);
            return UpdateCSSTable(style);
        }

        private int UpdateCSSTable(string style)
        {
            int i = FCSSStyles.IndexOf(style);
            if (i == -1)
            {
                i = FCSSStyles.Count;
                FCSSStyles.Add(style);
            }
            return i;
        }

        private void ExportPageStylesLayers(FastString styles, int PageNumber)
        {
            if (FPrevStyleListIndex < FCSSStyles.Count)
            {
                styles.Append(HTMLGetStylesHeader(PageNumber));
                for (int i = FPrevStyleListIndex; i < FCSSStyles.Count; i++)
                    styles.Append(HTMLGetStyleHeader(i, PageNumber)).Append(FCSSStyles[i]).AppendLine("}");
                styles.AppendLine(HTMLGetStylesFooter());
            }
        }

        private string GetStyleTag(int index)
        {           
            return String.Format("class=\"{0}s{1}\"", 
                FStylePrefix, 
                index.ToString()
            );
        }

        private void Layer(FastString Page, ReportComponentBase obj,
            float Left, float Top, float Width, float Height, FastString Text, string style, FastString addstyletag)
        {
            if (Page != null && obj != null)
            {
                Page.Append("<div ").Append(style).Append(" style=\"").
                    Append(!String.IsNullOrEmpty(obj.ClickEvent) ? "cursor:pointer;" : "").
                    Append("left:").Append(Px((LeftMargin + Left) * Zoom)).Append("top:").Append(Px((TopMargin + Top) * Zoom)).
                    Append("width:").Append(Px(Width * Zoom)).Append("height:").Append(Px(Height * Zoom));

                if (addstyletag != null)
                    Page.Append(addstyletag);

                Page.Append("\"");

                if (!String.IsNullOrEmpty(obj.ClickEvent) && !String.IsNullOrEmpty(ReportID))
                {
                    string eventParam = String.Format("{0},{1},{2},{3}",
                                obj.Name,
                                CurPage,
                                obj.AbsLeft.ToString("#0"),
                                obj.AbsTop.ToString("#0")
                    );
                    Page.Append(" onclick=\"").
                        AppendFormat(OnClickTemplate, ReportID, "click", eventParam).
                        Append("\"");
                }

                Page.Append(">");
                if (Text == null)
                    Page.Append(NBSP);
                else
                    Page.Append(Text);
                Page.AppendLine("</div>");
            }
        }

        private FastString GetSpanText(TextObjectBase obj, FastString text, 
            float top, float width, 
            float ParagraphOffset)
        {
            FastString style = new FastString();
            style.Append("display:block;border:0;width:").Append(Px(width * Zoom));
            if (ParagraphOffset != 0)
                style.Append("text-indent:").Append(Px(ParagraphOffset * Zoom));
            if (obj.Padding.Left != 0)
                style.Append("padding-left:").Append(Px((obj.Padding.Left) * Zoom));
            if (obj.Padding.Right != 0)
                style.Append("padding-right:").Append(Px(obj.Padding.Right * Zoom));
            if (top != 0)
                style.Append("margin-top:").Append(Px(top * Zoom));

            string href = String.Empty;

            if (!String.IsNullOrEmpty(obj.Hyperlink.Value))
            {
                string hrefStyle = String.Empty;
                if (obj is TextObject)
                {
                    TextObject textObject = obj as TextObject;
                    hrefStyle = String.Format("style=\"color:{0}{1}\"",
                        ExportUtils.HTMLColor(textObject.TextColor),
                        !textObject.Font.Underline ? ";text-decoration:none" : String.Empty
                        );
                }
                string url = ExportUtils.HtmlURL(obj.Hyperlink.Value);
                if (obj.Hyperlink.Kind == HyperlinkKind.URL)
                    href = String.Format("<a {0} href=\"{1}\">", hrefStyle, obj.Hyperlink.Value);
                else if (obj.Hyperlink.Kind == HyperlinkKind.DetailReport)
                {
                    url = String.Format("{0},{1},{2}",
                        ExportUtils.HtmlString(obj.Name), // object name for security reasons
                        ExportUtils.HtmlString(obj.Hyperlink.ReportParameter),
                        ExportUtils.HtmlString(obj.Hyperlink.Value));
                    string onClick = String.Format(OnClickTemplate, ReportID, "detailed_report", url);
                    href = String.Format("<a {0} href=\"#\" onclick=\"{1}\">", hrefStyle, onClick);
                }
                else if (obj.Hyperlink.Kind == HyperlinkKind.DetailPage)
                {
                    url = String.Format("{0},{1},{2}",
                        ExportUtils.HtmlString(obj.Name),
                        ExportUtils.HtmlString(obj.Hyperlink.ReportParameter),
                        ExportUtils.HtmlString(obj.Hyperlink.Value));
                    string onClick = String.Format(OnClickTemplate, ReportID, "detailed_page", url);
                    href = String.Format("<a {0} href=\"#\" onclick=\"{1}\">", hrefStyle, onClick);
                }
                else if (SinglePage)
                {
                    if (obj.Hyperlink.Kind == HyperlinkKind.Bookmark)
                        href = String.Format("<a {0} href=\"#{1}\">", hrefStyle, url);
                    else if (obj.Hyperlink.Kind == HyperlinkKind.PageNumber)
                        href = String.Format("<a {0} href=\"#PageN{1}\">", hrefStyle, url);
                }
                else
                {
                    string onClick = String.Empty;
                    if (obj.Hyperlink.Kind == HyperlinkKind.Bookmark)
                        onClick = String.Format(OnClickTemplate, ReportID, "bookmark", url);
                    else if (obj.Hyperlink.Kind == HyperlinkKind.PageNumber)
                        onClick = String.Format(OnClickTemplate, ReportID, "goto", url);

                    if (onClick != String.Empty)
                        href = String.Format("<a {0} href=\"#\" onclick=\"{1}\">", hrefStyle, onClick);
                }
            }

            FastString result = new FastString(128);
            result.Append("<div ").
                Append(GetStyleTag(UpdateCSSTable(style.ToString()))).Append(">").
                Append(href).Append(text).Append(href != String.Empty ? "</a>" : String.Empty).
                Append("</div>"); 

            return result;
        }

        private void LayerText(FastString Page, TextObject obj)
        {
            float top = 0;
            if (obj.VertAlign != VertAlign.Top)
            {
                Graphics g = htmlMeasureGraphics;
                using (Font f = new Font(obj.Font.Name, obj.Font.Size, obj.Font.Style))
                {
                    RectangleF textRect = new RectangleF(obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height);
                    StringFormat format = obj.GetStringFormat(Report.GraphicCache, 0);
                    Brush textBrush = Report.GraphicCache.GetBrush(obj.TextColor);
                    AdvancedTextRenderer renderer = new AdvancedTextRenderer(obj.Text, g, f, textBrush, null,
                        textRect, format, obj.HorzAlign, obj.VertAlign, obj.LineHeight, obj.Angle, obj.FontWidthRatio,
                        obj.ForceJustify, obj.Wysiwyg, obj.HtmlTags, false, Zoom);
                    if (renderer.Paragraphs.Count > 0)
                        if (renderer.Paragraphs[0].Lines.Count > 0)
                        {
                            float height = renderer.Paragraphs[0].Lines[0].CalcHeight();
                            if (height > obj.Height)
                                top = - (height - obj.Height) / 2;
                            else
                                top = renderer.Paragraphs[0].Lines[0].Top - obj.AbsTop;
                        }
                }
            }


            LayerBack(Page, obj,
                GetSpanText(obj, ExportUtils.HtmlString(obj.Text, obj.HtmlTags),
                top + obj.Padding.Top,
                obj.Width - obj.Padding.Horizontal,
                obj.ParagraphOffset));
        }

        private void LayerHtml(FastString Page, HtmlObject obj)
        {
            LayerBack(Page, obj,
                GetSpanText(obj, new Utils.FastString(obj.Text),
                obj.Padding.Top,
                obj.Width - obj.Padding.Horizontal,
                0));
        }

        private string GetLayerPicture(ReportComponentBase obj, out float Width, out float Height)
        {                        
            string result = String.Empty;
            Width = 0;
            Height = 0;

            if (obj != null)
            {
                if (FPictures)
                {
                    MemoryStream PictureStream = new MemoryStream();
                    System.Drawing.Imaging.ImageFormat FPictureFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                    if (FImageFormat == ImageFormat.Png)
                        FPictureFormat = System.Drawing.Imaging.ImageFormat.Png;
                    else if (FImageFormat == ImageFormat.Jpeg)
                        FPictureFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (FImageFormat == ImageFormat.Gif)
                        FPictureFormat = System.Drawing.Imaging.ImageFormat.Gif;

                    Width = obj.Width == 0 ? obj.Border.LeftLine.Width : obj.Width;
                    Height = obj.Height == 0 ? obj.Border.TopLine.Width : obj.Height;

                    if (Math.Abs(Width) * Zoom < 1 && Zoom > 0)
                        Width = 1 / Zoom;

                    if (Math.Abs(Height) * Zoom < 1 && Zoom > 0)
                        Height = 1 / Zoom;

                    using (System.Drawing.Image image = new Bitmap((int)(Math.Round(Width * Zoom)), (int)(Math.Round(Height * Zoom))))
                    {
                        using (Graphics g = Graphics.FromImage(image))
                        {
                            if (obj is TextObjectBase)
                                g.Clear(Color.White);

                            float Left = Width > 0 ? obj.AbsLeft : obj.AbsLeft + Width;
                            float Top = Height > 0 ? obj.AbsTop : obj.AbsTop + Height;

                            float dx = 0; 
                            float dy = 0; 
                            g.TranslateTransform((-Left - dx) * Zoom, (-Top - dy) * Zoom);

                            BorderLines oldLines = obj.Border.Lines;
                            obj.Border.Lines = BorderLines.None;
                            obj.Draw(new FRPaintEventArgs(g, Zoom, Zoom, Report.GraphicCache));
                            obj.Border.Lines = oldLines;
                        }

                        if (FPictureFormat == System.Drawing.Imaging.ImageFormat.Jpeg)
                            ExportUtils.SaveJpeg(image, PictureStream, 95);
                        else
                            image.Save(PictureStream, FPictureFormat);
                    }
                    PictureStream.Position = 0;
                    result = HTMLGetImage(0, 0, 0, Crypter.ComputeHash(PictureStream), true, null, PictureStream);
                }
            }
            return result;
        }

        private void LayerPicture(FastString Page, ReportComponentBase obj, FastString text)
        {
            if (FPictures)
            {
                int styleindex = UpdateCSSTable(obj);
                string style = GetStyleTag(styleindex);
                string old_text = String.Empty;

                if (IsMemo(obj))
                {
                    old_text = (obj as TextObject).Text;
                    (obj as TextObject).Text = String.Empty;
                }
                
                float Width, Height;
                string pic = GetLayerPicture(obj, out Width, out Height);

                if (IsMemo(obj))
                    (obj as TextObject).Text = old_text;

                FastString addstyle = new FastString(128);
                addstyle.Append(" background: url('").Append(pic).Append("') no-repeat !important;-webkit-print-color-adjust:exact;");

                //if (String.IsNullOrEmpty(text))
                //    text = NBSP;

                float x = Width > 0 ? obj.AbsLeft : (obj.AbsLeft + Width);
                float y = Height > 0 ? FHPos + obj.AbsTop : (FHPos + obj.AbsTop + Height);

                Layer(Page, obj, x, y, Width, Height, text, style, addstyle);
            }
        }

        private void LayerShape(FastString Page, ShapeObject obj, FastString text)
        {
            int styleindex = UpdateCSSTable(obj);
            string style = GetStyleTag(styleindex);
            string old_text = String.Empty;

            float Width, Height;
            string pic = GetLayerPicture(obj, out Width, out Height);
            FastString addstyle = new FastString(64);
            if (obj.Shape == ShapeKind.Rectangle || obj.Shape == ShapeKind.RoundRectangle)
            {
                if (obj.FillColor.A != 0)
                    addstyle.Append("background:").Append(System.Drawing.ColorTranslator.ToHtml(obj.FillColor)).Append(";");
                addstyle.Append("border-style:solid;");
                if (obj.Border.Width != 3)
                    addstyle.Append("border-width:").Append(ExportUtils.FloatToString(obj.Border.Width)).Append("px;");
                addstyle.Append("border-color:").Append(System.Drawing.ColorTranslator.ToHtml(obj.Border.Color)).Append(";");
                if (obj.Shape == ShapeKind.RoundRectangle)
                    addstyle.Append("border-radius:15px;");
            }
            
            float x = obj.Width > 0 ? obj.AbsLeft : (obj.AbsLeft + obj.Width);
            float y = obj.Height > 0 ? FHPos + obj.AbsTop : (FHPos + obj.AbsTop + obj.Height);            
            Layer(Page, obj, x, y, obj.Width, obj.Height, text, style, addstyle);
            addstyle.Clear();
        }

        private void LayerBack(FastString Page, ReportComponentBase obj, FastString text)
        {
            if (obj.Border.Shadow)
            {
                using (TextObject shadow = new TextObject())
                {
                    shadow.Left = obj.AbsLeft + obj.Border.ShadowWidth + obj.Border.LeftLine.Width;
                    shadow.Top = obj.AbsTop + obj.Height + obj.Border.BottomLine.Width;
                    shadow.Width = obj.Width + obj.Border.RightLine.Width;
                    shadow.Height = obj.Border.ShadowWidth + obj.Border.BottomLine.Width;
                    shadow.FillColor = obj.Border.ShadowColor;
                    shadow.Border.Lines = BorderLines.None;
                    LayerBack(Page, shadow, null);

                    shadow.Left = obj.AbsLeft + obj.Width + obj.Border.RightLine.Width;
                    shadow.Top = obj.AbsTop + obj.Border.ShadowWidth + obj.Border.TopLine.Width;
                    shadow.Width = obj.Border.ShadowWidth + obj.Border.RightLine.Width;
                    shadow.Height = obj.Height;
                    LayerBack(Page, shadow, null);
                }
            }
                        
            if (obj.Fill is SolidFill)
                Layer(Page, obj, obj.AbsLeft, FHPos + obj.AbsTop, obj.Width, obj.Height, text, GetStyleTag(UpdateCSSTable(obj)), null);
            else
                LayerPicture(Page, obj, text);
        }

        private void LayerTable(FastString Page, FastString CSS, TableBase table)
        {
            float y = 0;
            for (int i = 0; i < table.RowCount; i++)
            {
                float x = 0;
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    if (!table.IsInsideSpan(table[j, i]))
                    {
                        TableCell textcell = table[j, i];

                        textcell.Left = x;
                        textcell.Top = y;

                        // custom draw
                        CustomDrawEventArgs e = new CustomDrawEventArgs();
                        e.Report = Report;
                        e.ReportObject = textcell;
                        e.Layers = Layers;
                        e.Zoom = Zoom;
                        e.Left = textcell.AbsLeft;
                        e.Top = FHPos + textcell.AbsTop;
                        e.Width = textcell.Width;
                        e.Height = textcell.Height;
                        OnCustomDraw(e);
                        if (e.Done)
                        {
                            CSS.Append(e.CSS);
                            Page.Append(e.Html);
                        }
                        else
                        {
                            if (textcell is TextObject && !(textcell as TextObject).TextOutline.Enabled && IsMemo(textcell))
                                LayerText(Page, textcell as TextObject);
                            else
                            {
                                LayerBack(Page, textcell as ReportComponentBase, null);
                                LayerPicture(Page, textcell as ReportComponentBase, null);
                            }
                        }
                    }
                    x += (table.Columns[j]).Width;
                }
                y += (table.Rows[i]).Height;
            }
        }

        private bool IsMemo(ReportComponentBase Obj)
        {
            return (Obj is TextObject) && 
                   ((Obj as TextObject).Angle == 0) && 
                   (Obj as TextObject).FontWidthRatio == 1 && 
                   !(Obj as TextObject).TextOutline.Enabled &&
                   !(Obj as TextObject).Underlines;
        }

        private void Watermark(FastString Page, ReportPage page, bool drawText)
        {
            using (PictureObject pictureWatermark = new PictureObject())
            {
                pictureWatermark.Left = 0;
                pictureWatermark.Top = 0;

                pictureWatermark.Width = (ExportUtils.GetPageWidth(page) - page.LeftMargin - page.RightMargin) * Units.Millimeters;
                pictureWatermark.Height = (ExportUtils.GetPageHeight(page) - page.TopMargin - page.BottomMargin) * Units.Millimeters;

                pictureWatermark.SizeMode = PictureBoxSizeMode.Normal;
                pictureWatermark.Image = new Bitmap((int)pictureWatermark.Width, (int)pictureWatermark.Height);

                using (Graphics g = Graphics.FromImage(pictureWatermark.Image))
                {
                    g.Clear(Color.Transparent);
                    if (drawText)
                        page.Watermark.DrawText(new FRPaintEventArgs(g, Zoom, Zoom, Report.GraphicCache),
                            new RectangleF(0, 0, pictureWatermark.Width, pictureWatermark.Height), Report, true);
                    else
                        page.Watermark.DrawImage(new FRPaintEventArgs(g, Zoom, Zoom, Report.GraphicCache),
                            new RectangleF(0, 0, pictureWatermark.Width, pictureWatermark.Height), Report, true);
                    pictureWatermark.Transparency = page.Watermark.ImageTransparency;
                    LayerBack(Page, pictureWatermark, null);
                    LayerPicture(Page, pictureWatermark, null);
                }
            }
        }

        private void ExportHTMLPageLayeredBegin(HTMLThreadData d)
        {
            if (!FSinglePage && !WebMode)
                FCSSStyles.Clear();

            CSS = new FastString();
            htmlPage = new FastString();

            ReportPage reportPage = d.page;

            if (reportPage != null)
            {
                MaxWidth = ExportUtils.GetPageWidth(reportPage) * Units.Millimeters;
                MaxHeight = ExportUtils.GetPageHeight(reportPage) * Units.Millimeters;

                if (enableMargins)
                {
                    LeftMargin = reportPage.LeftMargin * Units.Millimeters;
                    TopMargin = reportPage.TopMargin * Units.Millimeters;
                }
                else
                {
                    MaxWidth -= (reportPage.LeftMargin + reportPage.RightMargin) * Units.Millimeters;
                    MaxHeight -= (reportPage.TopMargin + reportPage.BottomMargin) * Units.Millimeters;
                    LeftMargin = 0;
                    TopMargin = 0;
                }

                currentPage = d.PageNumber - 1;

                ExportHTMLPageStart(htmlPage, d.PageNumber, d.CurrentPage);

                doPageBreak = (FSinglePage && FPageBreaks);

                htmlPage.Append(HTMLGetAncor((d.PageNumber).ToString()));

                htmlPage.Append("<div ").Append(doPageBreak ? "class=\"frpage\"" : String.Empty).
                    Append(" style=\"position:relative;width:").Append(Px(MaxWidth * Zoom + 3)).
                    Append("height:").Append(Px(MaxHeight * Zoom));

                if (reportPage.Fill is SolidFill)
                {
                    SolidFill fill = reportPage.Fill as SolidFill;
                    htmlPage.Append("; background-color:").
                        Append(fill.Color.A == 0 ? "transparent" : ExportUtils.HTMLColor(fill.Color));
                }
                htmlPage.Append("\">");

                if (!(reportPage.Fill is SolidFill))
                {
                    // to-do for picture background
                }

                if (reportPage.Watermark.Enabled && !reportPage.Watermark.ShowImageOnTop)
                    Watermark(htmlPage, reportPage, false);

                if (reportPage.Watermark.Enabled && !reportPage.Watermark.ShowTextOnTop)
                    Watermark(htmlPage, reportPage, true);
            }
        }

        private void ExportHTMLPageLayeredEnd(HTMLThreadData d)
        {
            // to do
            if (d.page.Watermark.Enabled && d.page.Watermark.ShowImageOnTop)
                Watermark(htmlPage, d.page, false);

            if (d.page.Watermark.Enabled && d.page.Watermark.ShowTextOnTop)
                Watermark(htmlPage, d.page, true);

            ExportPageStylesLayers(CSS, d.PageNumber);

            if (FSinglePage)
            {
                //if (!WebMode)
                //    FHPos += MaxHeight;
                //else
                    FHPos = 0;
                FPrevStyleListIndex = FCSSStyles.Count;
            }

            htmlPage.Append("</div>");

            ExportHTMLPageFinal(CSS, htmlPage, d, MaxWidth, MaxHeight);
        }

        private void ExportBandLayers(Base band)
        {
            LayerBack(htmlPage, band as ReportComponentBase, null);
            foreach (Base c in band.AllObjects)
            {
                if (c is ReportComponentBase)
                {
                    ReportComponentBase obj = c as ReportComponentBase;

                    // custom draw
                    CustomDrawEventArgs e = new CustomDrawEventArgs();
                    e.Report = Report;
                    e.ReportObject = obj;
                    e.Layers = Layers;
                    e.Zoom = Zoom;
                    e.Left = obj.AbsLeft;
                    e.Top = FHPos + obj.AbsTop;
                    e.Width = obj.Width;
                    e.Height = obj.Height;

                    OnCustomDraw(e);
                    if (e.Done)
                    {
                        CSS.Append(e.CSS);
                        htmlPage.Append(e.Html);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(obj.Bookmark))
                            htmlPage.Append("<a name=\"").Append(obj.Bookmark).Append("\"></a>");

                        if (obj is CellularTextObject)
                            obj = (obj as CellularTextObject).GetTable();
                        if (obj is TableCell)
                            continue;
                        else if (obj is TableBase)
                        {
                            TableBase table = obj as TableBase;
                            if (table.ColumnCount > 0 && table.RowCount > 0)
                            {
                                using (TextObject tableback = new TextObject())
                                {
                                    tableback.Border = table.Border;
                                    tableback.Fill = table.Fill;
                                    tableback.FillColor = table.FillColor;
                                    tableback.Left = table.AbsLeft;
                                    tableback.Top = table.AbsTop;
                                    float tableWidth = 0;
                                    float tableHeight = 0;
                                    for (int i = 0; i < table.ColumnCount; i++)
                                        tableWidth += table[i, 0].Width;
                                    for (int i = 0; i < table.RowCount; i++)
                                        tableHeight += table.Rows[i].Height;
                                    tableback.Width = (tableWidth < table.Width) ? tableWidth : table.Width;
                                    tableback.Height = tableHeight;
                                    LayerText(htmlPage, tableback);
                                }
                                LayerTable(htmlPage, CSS, table);
                            }
                        }
                        else if (IsMemo(obj))
                        {
                            LayerText(htmlPage, obj as TextObject);
                        }
                        else if (obj is HtmlObject)
                        {
                            LayerHtml(htmlPage, obj as HtmlObject);
                        }
                        else if (obj is BandBase)
                        {
                            LayerBack(htmlPage, obj, null);
                        }
                        else if (obj is LineObject)
                        {
                            LayerPicture(htmlPage, obj, null);
                        }
                        else if (obj is ShapeObject && ((obj as ShapeObject).Shape == ShapeKind.Rectangle || (obj as ShapeObject).Shape == ShapeKind.RoundRectangle))
                        {
                            LayerShape(htmlPage, obj as ShapeObject, null);
                        }
                        else
                        {
                            LayerBack(htmlPage, obj, null);
                            LayerPicture(htmlPage, obj, null);
                        }
                    }
                }
            }
        }       
    }
}
