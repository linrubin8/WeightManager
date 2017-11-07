using System;
using System.Collections.Generic;
using FastReport.Utils;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using FastReport.Table;
using FastReport.Forms;
using System.Collections;
using System.Drawing.Imaging;
using System.Xml;

namespace FastReport.Export.Svg
{
    /// <summary>
    /// Specifies the image format in SVG export.
    /// </summary>
    public enum SVGImageFormat
    {
        /// <summary>
        /// Specifies the .png format.
        /// </summary>
        Png,

        /// <summary>
        /// Specifies the .jpg format.
        /// </summary>
        Jpeg
    }

    /// <summary>
    /// Represents the SVG export filter.
    /// </summary>
    public partial class SVGExport : ExportBase
    {
        #region Private Fields       
        private string path;
        private string fileNameWOext;
        private string extension;
        private string pageFileName;
        private List<Stream> generatedStreams;
        private Hashtable hashtable;
        private SVGImageFormat imageFormat;
        private ImageFormat format;
        private Dictionary<string, MemoryStream> images;
        private Graphics graphics;
        private int quality;
        private int currentPage;
        private int img_nbr;
        private float FDpiFX = 96f / DrawUtils.ScreenDpi;
        private bool pictures;
        private bool FEmbedPictures;

        //XML fields
        private XmlAttribute nsAttr;
        private XmlElement root;
        private System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        private XmlElement g;
        XmlElement defs; // define gradients
        int gradNbr = 0;
        private float pageWidth;
        private float pageHeight;
        private int pagesCount;
        //end
        #endregion

        #region Properties
        /// <summary>
        /// Enable or disable the pictures in SVG export
        /// </summary>
        public bool Pictures
        {
            get { return pictures; }
            set { pictures = value; }
        }

        /// <summary>
        /// Gets list of generated streams
        /// </summary>
        public List<Stream> GeneratedStreams
        {
            get { return generatedStreams; }
        }

        /// <summary>
        /// Gets or sets the image format used when exporting.
        /// </summary>
        public SVGImageFormat ImageFormat
        {
            get { return imageFormat; }
            set
            {
                imageFormat = value;
                format = (imageFormat == SVGImageFormat.Jpeg) ?
                System.Drawing.Imaging.ImageFormat.Jpeg :
                System.Drawing.Imaging.ImageFormat.Png;
            }
        }

        /// <summary>
        /// Embed images into svg
        /// </summary>
        public bool EmbedPictures
        {
            get { return FEmbedPictures; }
            set { FEmbedPictures = value; }
        }
        #endregion

        #region Private Methods
        private void SaveImgsToFiles()
        {
            foreach (DictionaryEntry fl_nm_e in hashtable)
            {
                foreach (KeyValuePair<string, MemoryStream> img_e in images)
                {
                    if (fl_nm_e.Key.ToString() == img_e.Key.ToString())
                    {
                        string fullImagePath = Path.Combine(path, fl_nm_e.Value.ToString()) + "." + format.ToString().ToLower();
                        using (FileStream file = new FileStream(fullImagePath, FileMode.Create))
                            img_e.Value.WriteTo(file);
                    }
                }
            }
        }

        private void ExportObj(Base c)
        {
            if (c is ReportComponentBase)
            {
                ReportComponentBase obj = c as ReportComponentBase;
                if (obj is CellularTextObject)
                    obj = (obj as CellularTextObject).GetTable();
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
                            AddTextObject(tableback as TextObject, false);
                            // draw table border
                            AddBorder(tableback.Border, tableback.AbsLeft, tableback.AbsTop, tableback.Width, tableback.Height);
                        }
                        // draw cells
                        AddTable(table, true);
                        // draw cells border
                      //  AddTable(table, false);   ?
                    }
                }
                else if (obj is TextObject)
                    AddTextObject(obj as TextObject, true);
                else if (obj is BandBase)
                    AddBandObject(obj as BandBase);
                else if (obj is LineObject)
                    AddLine(obj as LineObject);
                else if (obj is ShapeObject)
                    AddShape(obj as ShapeObject);
                else
                    AddPictureObject(obj as ReportComponentBase);
            }
        }

        private void AddBitmapWatermark(ReportPage page)
        {
            if (page.Watermark.Image != null)
            {
                using (PictureObject pictureWatermark = new PictureObject())
                {
                    pictureWatermark.Left = page.LeftMargin;
                    pictureWatermark.Top = page.TopMargin;
                    pictureWatermark.Width = ExportUtils.GetPageWidth(page) * Units.Millimeters;
                    pictureWatermark.Height = ExportUtils.GetPageHeight(page) * Units.Millimeters;

                    pictureWatermark.SizeMode = PictureBoxSizeMode.Normal;
                    pictureWatermark.Image = new Bitmap((int)pictureWatermark.Width, (int)pictureWatermark.Height);
                    using (Graphics g = Graphics.FromImage(pictureWatermark.Image))
                    {
                        g.Clear(Color.Transparent);
                        page.Watermark.DrawImage(new FRPaintEventArgs(g, 1, 1, Report.GraphicCache),
                            new RectangleF(0, 0, pictureWatermark.Width, pictureWatermark.Height), Report, true);
                    }
                    pictureWatermark.Transparency = page.Watermark.ImageTransparency;
                    pictureWatermark.Fill = new SolidFill(Color.Transparent);
                    pictureWatermark.FillColor = Color.Transparent;
                    AddPictureObject(pictureWatermark);
                }
            }
        }

        private void AddTextWatermark(ReportPage page)
        {
            if (!String.IsNullOrEmpty(page.Watermark.Text))
                using (TextObject textWatermark = new TextObject())
                {
                    textWatermark.HorzAlign = HorzAlign.Center;
                    textWatermark.VertAlign = VertAlign.Center;
                    textWatermark.Left = page.LeftMargin;
                    textWatermark.Top = page.TopMargin;
                    textWatermark.Width = ExportUtils.GetPageWidth(page) * Units.Millimeters;
                    textWatermark.Height = ExportUtils.GetPageHeight(page) * Units.Millimeters;
                    textWatermark.Text = page.Watermark.Text;
                    textWatermark.TextFill = page.Watermark.TextFill;
                    if (page.Watermark.TextRotation == WatermarkTextRotation.Vertical)
                        textWatermark.Angle = 270;
                    else if (page.Watermark.TextRotation == WatermarkTextRotation.ForwardDiagonal)
                        textWatermark.Angle = 360 - (int)(Math.Atan(textWatermark.Height / textWatermark.Width) * (180 / Math.PI));
                    else if (page.Watermark.TextRotation == WatermarkTextRotation.BackwardDiagonal)
                        textWatermark.Angle = (int)(Math.Atan(textWatermark.Height / textWatermark.Width) * (180 / Math.PI));
                    textWatermark.Font = page.Watermark.Font;
                    if (page.Watermark.TextFill is SolidFill)
                        textWatermark.TextColor = (page.Watermark.TextFill as SolidFill).Color;
                    textWatermark.Fill = new SolidFill(Color.Transparent);
                    textWatermark.FillColor = Color.Transparent;
                    AddTextObject(textWatermark, false);
                }
        }

        private void AddTable(TableBase table, bool drawCells)
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
                        if (drawCells)
                        {
                            Border oldBorder = textcell.Border.Clone();
                            textcell.Border.Lines = BorderLines.None;
                            if ((textcell as TextObject) is TextObject)
                                AddTextObject(textcell as TextObject, /*false*/ true);
                            else
                                AddPictureObject(textcell as ReportComponentBase);
                            textcell.Border = oldBorder;
                        }
                        else
                            AddBorder(textcell.Border, textcell.AbsLeft, textcell.AbsTop, textcell.Width, textcell.Height);
                    }
                    x += (table.Columns[j]).Width;
                }
                y += (table.Rows[i]).Height;
            }
        }

        private void AddPictureObject(ReportComponentBase obj)
        {
            if (pictures)
            {
                MemoryStream imageStream = new MemoryStream();
                int with =   (int)((obj is PolygonObject || obj is PolyLineObject) ? (obj.Width + obj.Border.Width): (obj.Width - obj.Border.Width));
                int height = (int)((obj is PolygonObject || obj is PolyLineObject) ? (obj.Height + obj.Border.Width) : (obj.Height - obj.Border.Width));

                using (System.Drawing.Image image = new Bitmap(with, height))
                {
                    using (Graphics g = Graphics.FromImage(image))
                    {
                        using (GraphicCache cache = new GraphicCache())
                        {
                            if (imageFormat == SVGImageFormat.Jpeg)
                                g.Clear(Color.White);
                            else
                                g.Clear(Color.Transparent);

                            float Left = obj.Width >= 0 ? obj.AbsLeft : obj.AbsLeft + obj.Width;
                            float Top = obj.Height >= 0 ? obj.AbsTop  : obj.AbsTop  + obj.Height;
                            if (!(obj is PolygonObject || obj is PolyLineObject))
                            {
                                Left += obj.Border.Width;
                                Top  += obj.Border.Width;
                            }                           

                            g.TranslateTransform(-Left, -Top);
                            obj.Draw(new FRPaintEventArgs(g, 1, 1, cache));
                        }
                    }
                    if (imageFormat == SVGImageFormat.Jpeg)
                        ExportUtils.SaveJpeg(image, imageStream, quality);
                    else
                        image.Save(imageStream, format);
                }
                imageStream.Position = 0;
                string hash = Crypter.ComputeHash(imageStream);
                string imageFileName = fileNameWOext.Replace(" ", "") + hashtable.Count.ToString();

                if (!hashtable.ContainsKey(hash))
                {
                    hashtable.Add(hash, imageFileName);
                    if (path != null && path != "")
                        GeneratedFiles.Add(imageFileName);
                    images.Add(hash, imageStream);
                }
                else
                    imageFileName = hashtable[hash] as string;
                // add image in svg
                if (string.IsNullOrEmpty(path) || EmbedPictures)
                    AddImage(imageFileName, format.ToString().ToLower(), imageStream,  obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height);
                else
                    AddImage(imageFileName, format.ToString().ToLower(), obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height);
                img_nbr++;

                if (obj.Border.Lines != BorderLines.None)
                    AddBorder(obj.Border, obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height);
            }
        }

        private void AddBorder(Border border, float Left, float Top, float Width, float Height)
        {
            if (border.Lines != BorderLines.None)
            {
                using (TextObject emptyText = new TextObject())
                {
                    emptyText.Left = Left;
                    emptyText.Top = Top;
                    emptyText.Width = Width;
                    emptyText.Height = Height;
                    emptyText.Border = border;
                    emptyText.Text = string.Empty;
                    emptyText.FillColor = Color.Transparent;
                    AddTextObject(emptyText, true);
                }
            }
        }

        /// <summary>
        /// Add TextObject.
        /// </summary>
        private void AddTextObject(TextObject obj, bool drawBorder)
        {
            string Left = FloatToString(obj.AbsLeft);
            string Top = FloatToString(obj.AbsTop);
            string Right = FloatToString(obj.AbsLeft + obj.Width);
            string Bottom = FloatToString((obj.AbsTop + obj.Height));
            string Width = FloatToString(obj.Width);
            string Height = FloatToString(obj.Height);

            // if (obj.Clip)

            // draw background
            if (obj.Fill is SolidFill || (obj.Fill is GlassFill && !(obj.Fill as GlassFill).Hatch))
                DrawRectangle(obj.AbsLeft, obj.AbsTop,
                    obj.Width, obj.Height, Color.Transparent, 0, obj.Fill, false, LineStyle.Solid);
            else if (obj.Width > 0 && obj.Height > 0)
            {
                using (PictureObject backgroundPicture = new PictureObject())
                {
                    backgroundPicture.Left = obj.AbsLeft;
                    backgroundPicture.Top = obj.AbsTop;
                    backgroundPicture.Width = obj.Width;
                    backgroundPicture.Height = obj.Height;
                    backgroundPicture.Image = new Bitmap((int)backgroundPicture.Width, (int)backgroundPicture.Height);
                    using (Graphics g = Graphics.FromImage(backgroundPicture.Image))
                    {
                        g.Clear(Color.Transparent);
                        g.TranslateTransform(-obj.AbsLeft, -obj.AbsTop);
                        BorderLines oldLines = obj.Border.Lines;
                        obj.Border.Lines = BorderLines.None;
                        string oldText = obj.Text;
                        obj.Text = String.Empty;
                        obj.Draw(new FRPaintEventArgs(g, 1, 1, Report.GraphicCache));
                        obj.Text = oldText;
                        obj.Border.Lines = oldLines;
                    }
                    AddPictureObject(backgroundPicture as ReportComponentBase);
                }
            }
            if (obj.Underlines)
                DrawUnderlines(obj);

            if (!String.IsNullOrEmpty(obj.Text))
            {
                using (Font f = new Font(obj.Font.Name, obj.Font.Size, obj.Font.Style))
                {
                    RectangleF textRect = new RectangleF(
                      obj.AbsLeft + obj.Padding.Left,
                      obj.AbsTop + obj.Padding.Top,
                      obj.Width - obj.Padding.Horizontal,
                      obj.Height - obj.Padding.Vertical);

                    // break the text to paragraphs, lines, words and runs
                    StringFormat format = obj.GetStringFormat(Report.GraphicCache /*cache*/, 0);
                    Brush textBrush = Report.GraphicCache.GetBrush(obj.TextColor);
                    AdvancedTextRenderer renderer = new AdvancedTextRenderer(obj.Text, graphics, f, textBrush, null,
                        textRect, format, obj.HorzAlign, obj.VertAlign, obj.LineHeight, obj.Angle, obj.FontWidthRatio,
                        obj.ForceJustify, obj.Wysiwyg, obj.HtmlTags, false, FDpiFX);
                    float w = f.Height * 0.1f; // to match .net char X offset
                    // invert offset in case of rtl
                    if (obj.RightToLeft)
                        w = -w;
                    // we don't need this offset if text is centered
                    if (obj.HorzAlign == HorzAlign.Center)
                        w = 0;

                    XmlElement textContainer = DrawTextContainer(obj.AbsLeft, obj.AbsTop, f, obj.TextColor);

                    bool transformNeeded = obj.Angle != 0 || obj.FontWidthRatio != 1;
                    // transform, rotate and scale coordinates if needed
                    if (transformNeeded)
                    {
                        textRect.X = -textRect.Width / 2;
                        textRect.Y = -textRect.Height / 2;
                        float angle = (float)(obj.Angle * Math.PI / 180);
                        float x = (obj.AbsLeft + obj.Width / 2);
                        float y = (obj.AbsTop + obj.Height / 2);
                        AppndAngle(textContainer, angle, x, y);
                    }

                    // render
                    foreach (AdvancedTextRenderer.Paragraph paragraph in renderer.Paragraphs)
                        foreach (AdvancedTextRenderer.Line line in paragraph.Lines)
                        {
                            float lineOffset = 0;
                            float lineHeight = line.CalcHeight();
                            if (lineHeight > obj.Height)
                            {
                                if (obj.VertAlign == VertAlign.Center)
                                    lineOffset = -lineHeight / 2;//-
                                else if (obj.VertAlign == VertAlign.Bottom)
                                    lineOffset = -lineHeight;//-
                            }
                           /* foreach (RectangleF rect in line.Underlines)
                            {
                                // DrawPDFUnderline(ObjectFontNumber, f, rect.Left, rect.Top, rect.Width, w,
                                //    obj.TextColor, transformNeeded, sb);
                            }*/
                            /*foreach (RectangleF rect in line.Strikeouts)
                            {
                                 DrawStrikeout(f, rect.Left, rect.Top, rect.Width, w, obj.TextColor, transformNeeded);
                            }*/

                            foreach (AdvancedTextRenderer.Word word in line.Words)
                            {    if (renderer.HtmlTags)
                                     foreach (AdvancedTextRenderer.Run run in word.Runs)
                                         using (Font fnt = run.GetFont())
                                         {
                                            AppndTspan(textContainer, run.Left, run.Top + lineOffset + lineHeight * 72 / 96/*magic*/, w, run.Text, f);
                                         }
                                 else
                                AppndTspan(textContainer, word.Left, word.Top + lineOffset + lineHeight * 72 / 96/*magic*/, w, word.Text, f);
                            }
                        }
                }
            }
            if (drawBorder)
                DrawBorder(obj.Border, obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height);
        }

        /// <summary>
        /// Add BandObject.
        /// </summary>
        private void AddBandObject(BandBase band)
        {
            using (TextObject newObj = new TextObject())
            {
                newObj.Left = band.AbsLeft;
                newObj.Top = band.AbsTop;
                newObj.Width = band.Width;
                newObj.Height = band.Height;
                newObj.Fill = band.Fill;
                newObj.Border = band.Border;
                AddTextObject(newObj, true);
            }
        }

        /// <summary>
        /// Add Line.
        /// </summary>
        private void AddLine(LineObject line)
        {
            float AbsLeft = line.AbsLeft;
            float AbsTop = line.AbsTop;
            float Width = line.Width;
            float Height = line.Height;
            string Fill = Convert.ToString(ExportUtils.GetColorFromFill(line.Fill));
            float Border = line.Border.Width;
            if (line.StartCap.Style == CapStyle.Arrow)
            {
                float x3, y3, x4, y4;
                DrawArrow(line.StartCap, Border, Width + AbsLeft, Height + AbsTop, AbsLeft, AbsTop, out x3, out y3, out x4, out y4);
                DrawLine(AbsLeft, AbsTop, x3, y3, line.Border.Color, Border);
                DrawLine(AbsLeft, AbsTop, x4, y4, line.Border.Color, Border);
            }
            else
            {
   
            }
            if (line.EndCap.Style == CapStyle.Arrow)
            {
                float x3, y3, x4, y4;
                DrawArrow(line.EndCap, Border, AbsLeft, AbsTop, Width + AbsLeft, Height + AbsTop, out x3, out y3, out x4, out y4);
                DrawLine(Width + AbsLeft, AbsTop + Height, x3, y3, line.Border.Color, Border);
                DrawLine(Width + AbsLeft, AbsTop + Height, x4, y4, line.Border.Color, Border);
            }
            DrawLine(AbsLeft, AbsTop, AbsLeft + Width, AbsTop + Height, line.Border.Color, Border);
        }      

        /// <summary>
        /// Add Shape.
        /// </summary>
        private void AddShape(ShapeObject shape)
        {
            float AbsLeft = shape.AbsLeft;
            float AbsTop = shape.AbsTop;
            float Width = shape.Width;
            float Height = shape.Height;
            float BorderWidth = shape.Border.Width;
            if (shape.Shape == ShapeKind.Rectangle)
                DrawRectangle(AbsLeft, AbsTop, Width, Height, shape.Border.Color, BorderWidth, shape.Fill, false, LineStyle.Solid);
            if (shape.Shape == ShapeKind.RoundRectangle)
                DrawRectangle(AbsLeft, AbsTop, Width, Height, shape.Border.Color, BorderWidth, shape.Fill, true, LineStyle.Solid);
            if (shape.Shape == ShapeKind.Ellipse)
                DrawEllipse(AbsLeft, AbsTop, Width, Height, shape.Border.Color, BorderWidth, shape.Fill);
            if (shape.Shape == ShapeKind.Triangle)
                DrawTriangle(AbsLeft, AbsTop, Width, Height, shape.Border.Color, BorderWidth, shape.Fill);
            if (shape.Shape == ShapeKind.Diamond)
                DrawDiamond(AbsLeft, AbsTop, Width, Height, shape.Border.Color, BorderWidth, shape.Fill);
        }
        #endregion

        #region Private XML Methods
        /// <summary>
        /// Create Window.
        /// </summary>
        private void CreateWindow(string name)
        {
            root = doc.CreateElement("svg");
            AppndAttr(root, "xmlns", "http://www.w3.org/2000/svg");
            AppndAttr(root, "xmlns", "xlink", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/1999/xlink");
            AppndAttr(root, "Width", FloatToString(pageWidth));
            AppndAttr(root, "Height", FloatToString(pageHeight * pagesCount));
            doc.AppendChild(root);
            /* XmlElement title = doc.CreateElement("title");
             title.InnerText = name;
             root.AppendChild(title);*/
        }

        /// <summary>
        /// Add page
        /// </summary>
        /// <param name="page"></param>
        private void AddPage(ReportPage page)
        {
            pageWidth = ExportUtils.GetPageWidth(page) * Units.Millimeters;
            pageHeight = ExportUtils.GetPageHeight(page) * Units.Millimeters;

            g = doc.CreateElement("g");
            // if (!HasMultipleFiles)
            AppndAttr(g, "transform", "translate(" + ExportUtils.FloatToString(page.LeftMargin * Units.Millimeters) +
                ", " + ExportUtils.FloatToString(pageHeight * currentPage + page.TopMargin * Units.Millimeters) + ")");
            root.AppendChild(g);
        }

        [Obsolete("use AppndTspan", false)]
        private void DrawText(XmlElement textObject, Font font/*, float x, float y, float offsX*/, string text/*, int angle*/, Color fontColor)
        {
            if (font.Bold)
                AppndAttr(textObject, "font-weight", "bold");
            if (font.Italic)
                AppndAttr(textObject, "font-style", "italic");
            if (font.Underline)
                AppndAttr(textObject, "text-decoration", "underline");
            AppndAttr(textObject, "fill", ExportUtils.HTMLColorCode(fontColor));
            textObject.InnerText = text;
            g.AppendChild(textObject);
        }
        
        private XmlElement DrawTextContainer(float x, float y, Font font, Color fontColor)
        {
            XmlElement textObject = doc.CreateElement("text");

            AppndAttr(textObject, "x", FloatToString(x));
            AppndAttr(textObject, "y", FloatToString(y));
            AppndAttr(textObject, "font-size", FloatToString(font.Size) + "pt");
            AppndAttr(textObject, "font-family", Convert.ToString(font.Name));
            AppndAttr(textObject, "fill", ExportUtils.HTMLColorCode(fontColor));
            g.AppendChild(textObject);
            return textObject;
        }

        private void AppndAngle(XmlElement element, float angle, float x, float y)
        {
            AppndAttr(element, "transform", "matrix(" + FloatToString(Math.Cos(angle)) + " " +
                                                        FloatToString(Math.Sin(angle)) + " " +
                                                        FloatToString(-Math.Sin(angle)) + " " +
                                                        FloatToString(Math.Cos(angle)) + " " +
                                                        FloatToString(x) + " " +
                                                        FloatToString(y) + ")");
        }

        private void AppndTspan(XmlElement textObject, float x, float y, float offsX, string text, Font font/*, Color fontColor*/)
        {
            XmlElement tspan;
            XmlNode tspanTextNode;

            tspan = doc.CreateElement("tspan");
            tspan.SetAttribute("x", FloatToString(x));
            tspan.SetAttribute("y", FloatToString(y));

            if(true) //if not same in the textObject
            {
                AppndAttr(tspan, "font-size", FloatToString(font.Size) + "pt");
                AppndAttr(tspan, "font-family", Convert.ToString(font.Name));
                if (font.Bold)
                    AppndAttr(tspan, "font-weight", "bold");
                if (font.Italic)
                    AppndAttr(tspan, "font-style", "italic");
                if (font.Underline)
                    AppndAttr(tspan, "text-decoration", "underline");
            }

            tspanTextNode = doc.CreateTextNode(text);
            tspan.AppendChild(tspanTextNode);
            textObject.AppendChild(tspan);
        }
        /// <summary>
        /// Save svg file.
        /// </summary>
        private void Save(string filename)
        {
            doc.Save(filename);
        }

        /// <summary>
        /// Save svg stream.
        /// </summary>
        private void Save(Stream stream)
        {
            doc.Save(stream);
        }
        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void Start()
        {
            if (!HasMultipleFiles)
            {
                this.pageWidth = ExportUtils.GetPageWidth(GetPage(0)) * Units.Millimeters;
                this.pageHeight = ExportUtils.GetPageHeight(GetPage(0)) * Units.Millimeters;
                this.pagesCount = Pages.Length;
                CreateWindow(Name);
            }               
            currentPage = 0;
            if (FileName != "" && FileName != null)
            {
                path = Path.GetDirectoryName(FileName);
                fileNameWOext = Path.GetFileNameWithoutExtension(FileName);
                extension = Path.GetExtension(FileName);
            }
            else
                fileNameWOext = "svgreport";         
            GeneratedFiles.Clear();
            GeneratedStreams.Clear();
            hashtable.Clear();
            graphics = Graphics.FromHwnd(IntPtr.Zero);
        }

        /// <summary>
        /// Begin exporting of page
        /// </summary>
        /// <param name="page"></param>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);
            if (HasMultipleFiles)
            {
                this.pageWidth = ExportUtils.GetPageWidth(GetPage(0)) * Units.Millimeters;
                this.pageHeight = ExportUtils.GetPageHeight(GetPage(0)) * Units.Millimeters;
                this.pagesCount = 1;
                CreateWindow(Name);
                AddPage(page);
            }
            else
                AddPage(page);

            // bitmap watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page);
            // text watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowTextOnTop)
                AddTextWatermark(page);
            // page borders
            if (page.Border.Lines != BorderLines.None)
            {
                using (TextObject pageBorder = new TextObject())
                {
                    pageBorder.Border = page.Border;
                    pageBorder.Left = 0;
                    pageBorder.Top = 0;
                    pageBorder.Width = (ExportUtils.GetPageWidth(page) - page.LeftMargin - page.RightMargin);
                    pageBorder.Height = (ExportUtils.GetPageHeight(page) - page.TopMargin - page.BottomMargin);
                    AddTextObject(pageBorder, true);
                }
            }

            if (path != null && path != "")
                pageFileName = Path.Combine(path, fileNameWOext + currentPage.ToString() + extension);
            else
                pageFileName = null;
        }

        /// <summary>
        /// Export of Band
        /// </summary>
        /// <param name="band"></param>
        protected override void ExportBand(Base band)
        {
            base.ExportBand(band);
            if (band.Parent == null) return;
            ExportObj(band);
            foreach (Base c in band.AllObjects)
            {
                ExportObj(c);
            }
        }

        /// <summary>
        /// End exporting
        /// </summary>
        /// <param name="page"></param>
        protected override void ExportPageEnd(ReportPage page)
        {
            base.ExportPageEnd(page);
            // bitmap watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page);
            // text watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowTextOnTop)
                AddTextWatermark(page);
            if (HasMultipleFiles)
            {
                //export to MultipleFiles
                if (Directory.Exists(path) && !string.IsNullOrEmpty(FileName))
                {
                    // desktop mode
                    if (currentPage == 0)
                    {
                        // save first page in parent Stream
                        Save(Stream);
                        Stream.Position = 0;
                        GeneratedStreams.Add(Stream);
                        GeneratedFiles.Add(FileName);
                    }
                    else
                    {
                        // save all page after first in files
                        Save(pageFileName);
                        GeneratedFiles.Add(pageFileName);
                    }
                }
                else if (string.IsNullOrEmpty(path))
                {
                    if (currentPage == 0)
                    {
                        // save first page in parent Stream
                        Save(Stream);
                        Stream.Position = 0;
                        GeneratedStreams.Add(Stream);
                        GeneratedFiles.Add(FileName);
                    }
                    else
                    {
                        // server mode, save in internal stream collection
                        MemoryStream pageStream = new MemoryStream();
                        Save(pageStream);
                        pageStream.Position = 0;
                        GeneratedStreams.Add(pageStream);
                        GeneratedFiles.Add(pageFileName);
                    }
                }
            }
            // increment page number
            currentPage++;
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            if (!HasMultipleFiles)
            {
                Save(Stream);
                Stream.Position = 0;
                GeneratedFiles.Add(FileName);
            }
            if (Directory.Exists(path) && !string.IsNullOrEmpty(FileName) && !EmbedPictures)
                SaveImgsToFiles();
            graphics.Dispose();
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("SVGFile");
        }
        #endregion

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            base.Serialize(writer);
            writer.WriteValue("ImageFormat", ImageFormat);
            writer.WriteBool("HasMultipleFiles", HasMultipleFiles);
            writer.WriteValue("EmbedPictures", EmbedPictures);
        }

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (SVGExportForm form = new SVGExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SVGExport"/> class.
        /// </summary>
        public SVGExport()
        {
            HasMultipleFiles = false;
            pictures = true;
            quality = 90;
            imageFormat = SVGImageFormat.Png;
            generatedStreams = new List<Stream>();
            hashtable = new Hashtable();
            img_nbr = 0;
            format = System.Drawing.Imaging.ImageFormat.Png;
            images = new Dictionary<string, MemoryStream>();
        }
    }
}
