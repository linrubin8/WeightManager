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

namespace FastReport.Export.PS
{
        /// <summary>
        /// Specifies the image format in ps export.
        /// </summary>
        public enum PSImageFormat
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
    /// Represents the ps export filter.
    /// </summary>
    public class PSExport : ExportBase
    {
        #region Private Fields      
             
        private string path;
        private string fileNameWOext;
        private string extension;
        private string pageFileName;

        private int quality;
        private int currentPage;
        private int img_nbr;

        private bool pictures;
        private bool FTextInCurves;

        private float pageLeftMargin;
        private float pageTopMargin;

        private List<Stream> generatedStreams;
        private Hashtable hashtable;
        private PSImageFormat imageFormat;
        private ImageFormat format;
        private Dictionary<string, MemoryStream> images;
        private PSDocument ps;
        #endregion

        #region Properties
        /// <summary>
        /// Enable or disable the pictures in PS export
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
        public PSImageFormat ImageFormat
        {
            get { return imageFormat; }
            set
            {
                imageFormat = value;
                format = (imageFormat == PSImageFormat.Jpeg) ?
                System.Drawing.Imaging.ImageFormat.Jpeg :
                System.Drawing.Imaging.ImageFormat.Png;
            }
        }
        /// <summary>
        /// Enable or disable export text in curves
        /// </summary>
        public bool TextInCurves
        {
            get { return FTextInCurves; }
            set { FTextInCurves = value; }
        }
        #endregion

        #region Private Methods

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
                            AddTextObject(ps, tableback as TextObject, false);
                            // draw cells
                            AddTable(ps, table, true);
                            // draw cells border
                            AddTable(ps, table, false);
                            // draw table border
                            AddBorder(ps, tableback.Border, tableback.AbsLeft, tableback.AbsTop, tableback.Width, tableback.Height);
                        }
                    }
                }
                else if (obj is TextObject)
                    AddTextObject(ps, obj as TextObject, false);
                else if (obj is BandBase)
                    AddBandObject(ps, obj as BandBase);
                else if (obj is LineObject)
                    AddLine(ps, obj as LineObject);
                else if (obj is ShapeObject)
                    AddShape(ps, obj as ShapeObject);
                else
                    AddPictureObject(ps, obj as ReportComponentBase);
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
                    AddPictureObject(ps, pictureWatermark);
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
                    AddTextObject(ps, textWatermark, false);
                }
        }

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

        private void AddTable(PSDocument ps, TableBase table, bool drawCells)
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
                                AddTextObject(ps, textcell as TextObject, false);
                            else
                                AddPictureObject(ps, textcell as ReportComponentBase);
                            textcell.Border = oldBorder;
                        }
                        else
                            AddBorder(ps, textcell.Border, textcell.AbsLeft, textcell.AbsTop, textcell.Width, textcell.Height);
                    }
                    x += (table.Columns[j]).Width;
                }
                y += (table.Rows[i]).Height;
            }
        }

        private void AddPictureObject(PSDocument ps, ReportComponentBase obj)
        {
            if (pictures)
            {
                MemoryStream imageStream = new MemoryStream();

                using (System.Drawing.Image image = new Bitmap((int)obj.Width, (int)obj.Height))
                {
                    using (Graphics g = Graphics.FromImage(image))
                    {
                        using (GraphicCache cache = new GraphicCache())
                        {
                            if (imageFormat == PSImageFormat.Jpeg)
                                g.Clear(Color.White);

                            float Left = obj.Width >= 0 ? obj.AbsLeft : obj.AbsLeft + obj.Width;
                            float Top = obj.Height >= 0 ? obj.AbsTop : obj.AbsTop + obj.Height;

                            g.TranslateTransform(-Left, -Top);
                            obj.Draw(new FRPaintEventArgs(g, 1, 1, cache));
                        }
                    }
                    if (imageFormat == PSImageFormat.Jpeg)
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
                // add image
                if (!string.IsNullOrEmpty(path))
                    ps.AddImage(imageFileName, format.ToString().ToLower(), (obj.AbsLeft + pageLeftMargin) * 0.75f, (obj.AbsTop + pageTopMargin) * 0.75f, obj.Width * 0.75f, obj.Height * 0.75f);
                else
                    ps.AddImage(imageFileName, format.ToString().ToLower(), imageFileName + "_" + Convert.ToString(img_nbr), (obj.AbsLeft + pageLeftMargin) * 0.75f, (obj.AbsTop + pageTopMargin) * 0.75f, obj.Width * 0.75f, obj.Height * 0.75f);
                img_nbr++;
            }
        }

        private void AddBorder(PSDocument ps, Border border, float Left, float Top, float Width, float Height)
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
                    emptyText.Text = String.Empty;
                    emptyText.FillColor = Color.Transparent;
                    AddTextObject(ps, emptyText, true);
                }
            }
        }

        /// <summary>
        /// Add TextObject.
        /// </summary>
        private void AddTextObject(PSDocument ps, TextObject text, bool Band)
        {
            Font font = text.Font;
            float AbsLeft = text.AbsLeft + pageLeftMargin;
            float AbsTop = text.AbsTop + pageTopMargin;
            float Width = text.Width;
            float Height = text.Height;
            string HorzAlign = Convert.ToString(text.HorzAlign);
            string VertAlign = Convert.ToString(text.VertAlign);
            float BorderWidth = text.Border.Width;
            string BorderLines = Convert.ToString(text.Border.Lines);
            string Text = text.Text;
            float FontSize = text.Font.Size;
            string FontName = Convert.ToString(text.Font.Name);
            bool Bold = text.Font.Bold;
            bool Italic = text.Font.Italic;
            bool Underline = text.Font.Underline;
            float PaddingLeft = text.Padding.Left;
            float PaddingTop = text.Padding.Top;
            float PaddingRight = text.Padding.Right;
            float PaddingBottom = text.Padding.Bottom;
            bool WordWrap = text.WordWrap;
            string BorderBrush;
            string Background;
            string Foreground;
            float Angle = text.Angle;
            float LeftLine = text.Border.LeftLine.Width;
            float TopLine = text.Border.TopLine.Width;
            float RightLine = text.Border.RightLine.Width;
            float BottomLine = text.Border.BottomLine.Width;
            //Dash------------------------------------------------
            string LeftLineDashStile = Convert.ToString(text.Border.LeftLine.Style);
            string TopLineDashStile = Convert.ToString(text.Border.TopLine.Style);
            string RightLineDashStile = Convert.ToString(text.Border.RightLine.Style);
            string BottomLineDashStile = Convert.ToString(text.Border.BottomLine.Style);

            string colorLeftLine = ExportUtils.HTMLColor(text.Border.LeftLine.Color);
            string colorTopLine = ExportUtils.HTMLColor(text.Border.TopLine.Color);
            string colorRightLine = ExportUtils.HTMLColor(text.Border.RightLine.Color);
            string colorBottomLine = ExportUtils.HTMLColor(text.Border.BottomLine.Color);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     

            //GlassFill
            bool Glass = false;
            string colorTop = null;
            if (text.Fill is GlassFill)
            {
                Glass = true;
                Color color = GetBlendColor((text.Fill as GlassFill).Color, (text.Fill as GlassFill).Blend);
                colorTop = ExportUtils.HTMLColor(color);
            }

            NormalizeBorderBrushColor(text, out BorderBrush);
            NormalizeForegroundColor(text, out Foreground);
            NormalizeBackgroundColor(text, out Background);
            //Shadow----
            float ShadowWidth = text.Border.ShadowWidth;
            string ShadowColor = ExportUtils.HTMLColor(text.Border.ShadowColor);

            if (Band)
            {
                HorzAlign = null;
                VertAlign = null;
                Text = null;
                FontSize = 0;
                Foreground = null;
                FontName = null;
                Bold = false;
                Italic = false;
                Underline = false;
                PaddingLeft = 0;
                PaddingTop = 0;
                PaddingRight = 0;
                PaddingBottom = 0;
                WordWrap = false;
            }
            {
                ps.AddTextObject(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, HorzAlign, VertAlign, BorderBrush,
                 BorderWidth * 0.75f, LeftLine * 0.75f, TopLine * 0.75f, RightLine * 0.75f, BottomLine * 0.75f, LeftLineDashStile, TopLineDashStile,
                 RightLineDashStile, BottomLineDashStile, colorLeftLine, colorTopLine, colorRightLine, colorBottomLine,
                 text.Border.Shadow, ShadowColor, ShadowWidth * 0.75f, Background, BorderLines, Text, Foreground, PaddingLeft * 0.75f, PaddingTop * 0.75f, PaddingRight * 0.75f, PaddingBottom * 0.75f, WordWrap, Angle, Glass, colorTop, font);
            }
        }

        /// <summary>
        /// Add BandObject.
        /// </summary>
        private void AddBandObject(PSDocument ps, BandBase band)
        {
            using (TextObject newObj = new TextObject())
            {
                newObj.Left = band.AbsLeft;
                newObj.Top = band.AbsTop;
                newObj.Width = band.Width;
                newObj.Height = band.Height;
                newObj.Fill = band.Fill;
                newObj.Border = band.Border;
                AddTextObject(ps, newObj, true);
            }
        }

        /// <summary>
        /// Add Line.
        /// </summary>
        private void AddLine(PSDocument ps, LineObject line)
        {
            float AbsLeft = line.AbsLeft + pageLeftMargin;
            float AbsTop = line.AbsTop + pageTopMargin;
            float Width = line.Width;
            float Height = line.Height;
            string Fill = Convert.ToString(ExportUtils.GetColorFromFill(line.Fill));
            float Border = line.Border.Width;
            string LineStyle = line.Style;
            string BorderBrush = ExportUtils.HTMLColor(line.Border.Color);
            if (line.StartCap.Style == CapStyle.Arrow)
            {
                float x3, y3, x4, y4;
                DrawArrow(line.StartCap, Border, Width + AbsLeft, Height + AbsTop, AbsLeft, AbsTop, out x3, out y3, out x4, out y4);
                ps.AddLine(AbsLeft * 0.75f, AbsTop * 0.75f, x3 * 0.75f, y3 * 0.75f, BorderBrush, Border * 0.75f);
                ps.AddLine(AbsLeft * 0.75f, AbsTop * 0.75f, x4 * 0.75f, y4 * 0.75f, BorderBrush, Border * 0.75f);
            }

            if (line.EndCap.Style == CapStyle.Arrow)
            {
                float x3, y3, x4, y4;
                DrawArrow(line.EndCap, Border, AbsLeft, AbsTop, Width + AbsLeft, Height + AbsTop, out x3, out y3, out x4, out y4);
                ps.AddLine((Width + AbsLeft) * 0.75f, (AbsTop + Height) * 0.75f, x3 * 0.75f, y3 * 0.75f, BorderBrush, Border * 0.75f);
                ps.AddLine((Width + AbsLeft) * 0.75f, (AbsTop + Height) * 0.75f, x4 * 0.75f, y4 * 0.75f, BorderBrush, Border * 0.75f);
            }
            ps.AddLine(AbsLeft * 0.75f, AbsTop * 0.75f, (AbsLeft + Width) * 0.75f, (AbsTop + Height) * 0.75f, BorderBrush, Border * 0.75f);
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

        /// <summary>
        /// Add Shape.
        /// </summary>
        private void AddShape(PSDocument ps, ShapeObject shape)
        {
            float AbsLeft = shape.AbsLeft + pageLeftMargin;
            float AbsTop = shape.AbsTop + pageTopMargin;
            float Width = shape.Width;
            float Height = shape.Height;
            float BorderWidth = shape.Border.Width;
            string BorderLines = Convert.ToString(shape.Border.Lines);
            string BorderBrush;
            string Background;
            NormalizeColor(shape, out BorderBrush, out Background);
            if (shape.Shape == ShapeKind.Rectangle)
                ps.AddRectangle(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, BorderBrush, BorderWidth * 0.75f, Background, false);
            if (shape.Shape == ShapeKind.RoundRectangle)
                ps.AddRectangle(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, BorderBrush, BorderWidth * 0.75f, Background, true);
            if (shape.Shape == ShapeKind.Ellipse)
                ps.AddEllipse(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, BorderBrush, BorderWidth * 0.75f, Background);
            if (shape.Shape == ShapeKind.Triangle)
                ps.AddTriangle(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, BorderBrush, BorderWidth * 0.75f, Background);
            if (shape.Shape == ShapeKind.Diamond)
                ps.AddDiamond(AbsLeft * 0.75f, AbsTop * 0.75f, Width * 0.75f, Height * 0.75f, BorderBrush, BorderWidth * 0.75f, Background);
        }

        private Color GetBlendColor(Color c, float Blend)
        {
            return Color.FromArgb(255, (int)Math.Round(c.R + (255 - c.R) * Blend),
                    (int)Math.Round(c.G + (255 - c.G) * Blend),
                    (int)Math.Round(c.B + (255 - c.B) * Blend));
        }

        private void NormalizeBorderBrushColor(TextObject obj, out string BorderBrush)
        {
            obj.FillColor = ExportUtils.GetColorFromFill(obj.Fill);
            BorderBrush = ExportUtils.HTMLColor(obj.Border.Color);
        }

        private void NormalizeBackgroundColor(TextObject obj, out string Background)
        {
            obj.FillColor = ExportUtils.GetColorFromFill(obj.Fill);
            if (obj.FillColor.Name == "Transparent")
            {
                Background = "none";
            }
            else Background = ExportUtils.HTMLColor(obj.FillColor);
        }

        private void NormalizeForegroundColor(TextObject obj, out string Foreground)
        {
            obj.FillColor = ExportUtils.GetColorFromFill(obj.Fill);
            Foreground = ExportUtils.HTMLColor(obj.TextColor);
        }

        private void NormalizeColor(ShapeObject obj, out string BorderBrush, out string Background)
        {
            obj.FillColor = ExportUtils.GetColorFromFill(obj.Fill);

            BorderBrush = ExportUtils.HTMLColor(obj.Border.Color);
            if (obj.FillColor.Name == "Transparent")
            {
                Background = "none";
            }
            else Background = ExportUtils.HTMLColor(obj.FillColor);
        }
        #endregion

        #region Protected Methods
        /// <inheritdoc/>
        protected override void Start()
        {
            currentPage = 0;
            if (FileName != "" && FileName != null)
            {
                path = Path.GetDirectoryName(FileName);
                fileNameWOext = Path.GetFileNameWithoutExtension(FileName);
                extension = Path.GetExtension(FileName);
            }
            else
                fileNameWOext = "xamlreport";           
            GeneratedFiles.Clear();
            GeneratedStreams.Clear();
            hashtable.Clear();            
        }

        /// <summary>
        /// Begin exporting of page
        /// </summary>
        /// <param name="page"></param>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);
            ps = new PSDocument(fileNameWOext, ExportUtils.GetPageWidth(page) * Units.Millimeters * 0.75f,
        ExportUtils.GetPageHeight(page) * Units.Millimeters * 0.75f);
            ps.TextInCurves = FTextInCurves;
            pageLeftMargin = page.LeftMargin * Units.Millimeters;
            pageTopMargin = page.TopMargin * Units.Millimeters;
              
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
                    AddTextObject(ps, pageBorder, true);
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
                if (Directory.Exists(path) && !string.IsNullOrEmpty(FileName))
                {
                    // desktop mode
                    if (currentPage == 0)
                    {
                        // save first page in parent Stream
                        ps.Save(Stream);
                        Stream.Position = 0;
                        GeneratedStreams.Add(Stream);
                        GeneratedFiles.Add(FileName);
                    }
                    else
                    {
                        // save all page after first in files
                        ps.Save(pageFileName);
                        GeneratedFiles.Add(pageFileName);
                    }
                }
                else if (string.IsNullOrEmpty(path))
                {
                    if (currentPage == 0)
                    {
                        // save first page in parent Stream
                        ps.Save(Stream);
                        Stream.Position = 0;
                        GeneratedStreams.Add(Stream);
                        GeneratedFiles.Add(FileName);
                    }
                    else
                    {
                        // server mode, save in internal stream collection
                        MemoryStream pageStream = new MemoryStream();
                        ps.Save(pageStream);
                        pageStream.Position = 0;
                        GeneratedStreams.Add(pageStream);
                        GeneratedFiles.Add(pageFileName);
                    }
                }
            }
            ps.Finish();
            // increment page number
            currentPage++;
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            if (Directory.Exists(path) && !string.IsNullOrEmpty(FileName))
                SaveImgsToFiles();
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("PSFile");
        }
#endregion

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            base.Serialize(writer);
            writer.WriteValue("ImageFormat", ImageFormat);
            writer.WriteBool("TextInCurves", TextInCurves);
        }

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (PSExportForm form = new PSExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PSExport"/> class.
        /// </summary>
        public PSExport()
        {
            HasMultipleFiles = true;
            pictures = true;
            quality = 90;
            imageFormat = PSImageFormat.Png;
            generatedStreams = new List<Stream>();
            hashtable = new Hashtable();
            img_nbr = 0;
            format = System.Drawing.Imaging.ImageFormat.Jpeg;
            images = new Dictionary<string, MemoryStream>();
        }
    }

}
