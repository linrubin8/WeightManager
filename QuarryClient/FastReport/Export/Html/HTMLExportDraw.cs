using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using FastReport.Utils;

namespace FastReport.Export.Html
{
    /// <summary>
    /// Represents the HTML export filter.
    /// </summary>
    public partial class HTMLExport : ExportBase
    {
        private void HTMLFontStyle(FastString FFontDesc, Font font, float LineHeight)
        {
            FFontDesc.Append((((font.Style & FontStyle.Bold) > 0) ? "font-weight:bold;" : String.Empty) +
                (((font.Style & FontStyle.Italic) > 0) ? "font-style:italic;" : "font-style:normal;"));
            if ((font.Style & FontStyle.Underline) > 0 && (font.Style & FontStyle.Strikeout) > 0)
                FFontDesc.Append("text-decoration:underline|line-through;");
            else if ((font.Style & FontStyle.Underline) > 0)
                FFontDesc.Append("text-decoration:underline;");
            else if ((font.Style & FontStyle.Strikeout) > 0)
                FFontDesc.Append("text-decoration:line-through;");
            FFontDesc.Append("font-family:").Append(font.Name).Append(";");
            FFontDesc.Append("font-size:").Append(Px(Math.Round(font.Size * 96 / 72)));
            if (LineHeight > 0)
                FFontDesc.Append("line-height:").Append(Px(LineHeight)).Append(";");
            else
                FFontDesc.Append("line-height: 1.2;");
        }

        private void HTMLPadding(FastString PaddingDesc, Padding padding, float ParagraphOffset)
        {
            PaddingDesc.Append("text-indent:").Append(Px(ParagraphOffset));
            PaddingDesc.Append("padding-left:").Append(Px(padding.Left));
            PaddingDesc.Append("padding-right:").Append(Px(padding.Right));
            PaddingDesc.Append("padding-top:").Append(Px(padding.Top));
            PaddingDesc.Append("padding-bottom:").Append(Px(padding.Bottom));
        }

        private string HTMLBorderStyle(BorderLine line)
        {
            switch (line.Style)
            {
                case LineStyle.Dash:
                case LineStyle.DashDot:
                case LineStyle.DashDotDot:
                    return "dashed";
                case LineStyle.Dot:
                    return "dotted";
                case LineStyle.Double:
                    return "double";
                default:
                    return "solid";
            }
        }

        private float HTMLBorderWidth(BorderLine line)
        {
            if (line.Style == LineStyle.Double)
                return (line.Width * 3 * Zoom);
            else
                return line.Width * Zoom;
        }

        private string HTMLBorderWidthPx(BorderLine line)
        {
            if (line.Style != LineStyle.Double && line.Width == 1 && Zoom == 1)
                return "1px;";
            float width;
            if (line.Style == LineStyle.Double)
                width = line.Width * 3 * Zoom;
            else
                width = line.Width * Zoom;
            return Convert.ToString(Math.Round(width, 2), FNumberFormat) + "px;";
        }

        private void HTMLBorder(FastString BorderDesc, Border border)
        {
            if (!FLayers)
                BorderDesc.Append("border-collapse: separate;");
            if (border.Lines > 0)
            {
                // bottom
                if ((border.Lines & BorderLines.Bottom) > 0)
                    BorderDesc.Append("border-bottom-width:").
                        Append(HTMLBorderWidthPx(border.BottomLine)).
                        Append("border-bottom-color:").
                        Append(ExportUtils.HTMLColor(border.BottomLine.Color)).Append(";border-bottom-style:").
                        Append(HTMLBorderStyle(border.BottomLine)).Append(";");
                else
                    BorderDesc.Append("border-bottom:none;");
                // top
                if ((border.Lines & BorderLines.Top) > 0)
                    BorderDesc.Append("border-top-width:").
                        Append(HTMLBorderWidthPx(border.TopLine)).
                        Append("border-top-color:").
                        Append(ExportUtils.HTMLColor(border.TopLine.Color)).Append(";border-top-style:").
                        Append(HTMLBorderStyle(border.TopLine)).Append(";");
                else
                    BorderDesc.Append("border-top:none;");
                // left
                if ((border.Lines & BorderLines.Left) > 0)
                    BorderDesc.Append("border-left-width:").
                        Append(HTMLBorderWidthPx(border.LeftLine)).
                        Append("border-left-color:").
                        Append(ExportUtils.HTMLColor(border.LeftLine.Color)).Append(";border-left-style:").
                        Append(HTMLBorderStyle(border.LeftLine)).Append(";");
                else
                    BorderDesc.Append("border-left:none;");
                // right
                if ((border.Lines & BorderLines.Right) > 0)
                    BorderDesc.Append("border-right-width:").
                        Append(HTMLBorderWidthPx(border.RightLine)).
                        Append("border-right-color:").
                        Append(ExportUtils.HTMLColor(border.RightLine.Color)).Append(";border-right-style:").
                        Append(HTMLBorderStyle(border.RightLine)).Append(";");
                else
                    BorderDesc.Append("border-right:none;");
            }
            else
                BorderDesc.Append("border:none;");
        }

        private void HTMLAlign(FastString sb, HorzAlign horzAlign, VertAlign vertAlign, bool wordWrap)
        {
            sb.Append("text-align:");
            if (horzAlign == HorzAlign.Left)
                sb.Append("Left");
            else if (horzAlign == HorzAlign.Right)
                sb.Append("Right");
            else if (horzAlign == HorzAlign.Center)
                sb.Append("Center");
            else if (horzAlign == HorzAlign.Justify)
                sb.Append("Justify");
            sb.Append(";vertical-align:");
            if (vertAlign == VertAlign.Top)
                sb.Append("Top");
            else if (vertAlign == VertAlign.Bottom)
                sb.Append("Bottom");
            else if (vertAlign == VertAlign.Center)
                sb.Append("Middle");
            if (wordWrap)
                sb.Append(";word-wrap:break-word");
            sb.Append(";overflow:hidden;");
        }

        private void HTMLRtl(FastString sb, bool rtl)
        {
            if (rtl)
                sb.Append("direction:rtl;");
        }

        private string HTMLGetStylesHeader(int PageNumber)
        {
            FastString header = new FastString();
            if (FSinglePage && FPageBreaks)
            {
                header.AppendLine("<style type=\"text/css\" media=\"print\"><!--");
                header.AppendLine("div.frpage { page-break-after: always; page-break-inside: avoid; }");
                header.AppendLine("--></style>");
            }
            header.AppendLine("<style type=\"text/css\"><!-- ");
            return header.ToString();
        }

        private string HTMLGetStyleHeader(long index, long subindex)
        {
            FastString header = new FastString();
            return header.Append(".").
                Append(FStylePrefix).
                Append("s").
                Append(index.ToString()).
                Append((FSinglePage || FLayers)? String.Empty : String.Concat("-", subindex.ToString())).
                Append(" { ").ToString();
        }

        private void HTMLGetStyle(FastString style, Font Font, Color TextColor, Color FillColor, HorzAlign HAlign, VertAlign VAlign,
            Border Border, System.Windows.Forms.Padding Padding, bool RTL, bool wordWrap, float LineHeight, float ParagraphOffset)
        {
            HTMLFontStyle(style, Font, LineHeight);
            style.Append("color:").Append(ExportUtils.HTMLColor(TextColor)).Append(";");
            style.Append("background-color:");
            style.Append(FillColor.A == 0 ? "transparent" : ExportUtils.HTMLColor(FillColor)).Append(";");
            HTMLAlign(style, HAlign, VAlign, wordWrap);
            HTMLBorder(style, Border);
            HTMLPadding(style, Padding, ParagraphOffset);
            HTMLRtl(style, RTL);
            style.AppendLine("}");
        }

        private string HTMLGetStylesFooter()
        {
            return "--></style>";
        }

        private string HTMLGetAncor(string ancorName)
        {
            FastString ancor = new FastString();
            return ancor.Append("<a name=\"PageN").Append(ancorName).Append("\" style=\"padding:0;margin:0;font-size:1px;\"></a>").ToString();
        }

        private string HTMLGetImageTag(string file)
        {
            return "<img src=\"" + file + "\" alt=\"\"/>";
        }

        private string HTMLGetImage(int PageNumber, int CurrentPage, int ImageNumber, string hash, bool Base,
            System.Drawing.Image Metafile, MemoryStream PictureStream)
        {
            if (FPictures)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Bmp;
                if (FImageFormat == ImageFormat.Png)
                    format = System.Drawing.Imaging.ImageFormat.Png;
                else if (FImageFormat == ImageFormat.Jpeg)
                    format = System.Drawing.Imaging.ImageFormat.Jpeg;
                else if (FImageFormat == ImageFormat.Gif)
                    format = System.Drawing.Imaging.ImageFormat.Gif;
                FastString ImageFileNameBuilder = new FastString(48);
                
                string ImageFileName;
                if (!FSaveStreams)
                    ImageFileNameBuilder.Append(Path.GetFileName(FTargetFileName)).Append(".");
                ImageFileNameBuilder.Append(hash).
                    Append(".").Append(format.ToString().ToLower());

                ImageFileName = ImageFileNameBuilder.ToString();

                if (!FWebMode && !(FPreview || FPrint))
                {
                    if (Base)
                    {
                        if (Metafile != null && !EmbedPictures)
                        {
                            if (FSaveStreams)
                            {
                                MemoryStream ImageFileStream = new MemoryStream();
                                Metafile.Save(ImageFileStream, format);
                                GeneratedUpdate(FTargetPath + ImageFileName, ImageFileStream);
                            }
                            else
                            {
                                using (FileStream ImageFileStream =
                                    new FileStream(FTargetPath + ImageFileName, FileMode.Create))
                                    Metafile.Save(ImageFileStream, format);
                            }
                        }
                        else if (PictureStream != null && !EmbedPictures)
                        {
                            if (FFormat == HTMLExportFormat.HTML)
                            {
                                string fileName = FTargetPath + ImageFileName;
                                FileInfo info = new FileInfo(fileName);
                                if (!(info.Exists && info.Length == PictureStream.Length))
                                {
                                    if (FSaveStreams)
                                    {
                                        GeneratedUpdate(FTargetPath + ImageFileName, PictureStream);
                                    }
                                    else
                                    {
                                        using (FileStream ImageFileStream =
                                        new FileStream(fileName, FileMode.Create))
                                            PictureStream.WriteTo(ImageFileStream);
                                    }
                                }
                            }
                            else
                            {
                                PicsArchiveItem item;
                                item.FileName = ImageFileName;
                                item.Stream = PictureStream;
                                bool founded = false;
                                for (int i = 0; i < FPicsArchive.Count; i++)
                                    if (item.FileName == FPicsArchive[i].FileName)
                                    {
                                        founded = true;
                                        break;
                                    }
                                if (!founded)
                                    FPicsArchive.Add(item);
                            }
                        }
                        if (!FSaveStreams)
                            GeneratedFiles.Add(FTargetPath + ImageFileName);
                    }
                    if (EmbedPictures)
                    {
                        return "data:image/jpg;base64," + Convert.ToBase64String(PictureStream.ToArray());
                    }
                    else if (FSubFolder && FSinglePage && !FNavigator)
                        return ExportUtils.HtmlURL(FSubFolderPath + ImageFileName);
                    else
                        return ExportUtils.HtmlURL(ImageFileName);
                }
                else
                {
                    if (EmbedPictures)
                    {
                        return "data:image/jpg;base64," + Convert.ToBase64String(PictureStream.ToArray());
                    }
                    else
                    {
                        if (FPrint || FPreview)
                        {
                            FPrintPageData.Pictures.Add(PictureStream);
                            FPrintPageData.Guids.Add(hash);
                        }
                        else if (Base)
                        {
                            FPages[CurrentPage].Pictures.Add(PictureStream);
                            FPages[CurrentPage].Guids.Add(hash);
                        }
                        return FWebImagePrefix + "=" + hash + FWebImageSuffix;
                    }
                }
            }
            else
                return String.Empty;
        }
    }
}
