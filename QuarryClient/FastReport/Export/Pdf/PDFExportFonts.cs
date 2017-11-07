using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Export.TTF;
using System.IO;

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        private List<ExportTTFFont> FFonts;
        private List<ExportTTFFont> FPageFonts;

        private void AppendFont(StringBuilder Result, int fontNumber, float fontSize, Color fontColor)
        {
            if (!FTextInCurves)
            {
                ExportTTFFont pdffont = FPageFonts[fontNumber];
                Result.Append(pdffont.Name).Append(" ").Append(FloatToString(fontSize)).AppendLine(" Tf");
                GetPDFFillColor(fontColor, Result);
            }
        }

        private int GetObjFontNumber(Font font)
        {
            //if (!FTextInCurves)
            //{
                int i;
                for (i = 0; i < FPageFonts.Count; i++)
                    if (FontEquals(font, FPageFonts[i].SourceFont))
                        break;
                if (i < FPageFonts.Count)
                    return i;
                else
                {
                    FPageFonts.Add(GetGlobalFont(font));
                    return FPageFonts.Count - 1;
                }
            //}
            //else
             //   return 0;
        }

        private ExportTTFFont GetGlobalFont(Font font)
        {
            int i;
            for (i = 0; i < FFonts.Count; i++)
                if (FontEquals(font, FFonts[i].SourceFont))
                    break;
            if (i < FFonts.Count)
                return FFonts[i];
            else
            {
                ExportTTFFont fontitem = new ExportTTFFont(font);
                fontitem.FillOutlineTextMetrix();
                FFonts.Add(fontitem);
                fontitem.Name = "/F" + (FFonts.Count - 1).ToString();
                return fontitem;
            }
        }

        private void WriteFont(ExportTTFFont pdfFont)
        {
            long fontFileId = 0;
            string fontName = pdfFont.GetEnglishFontName();
            // embedded font 
            if (FEmbeddingFonts)
            {
                fontFileId = UpdateXRef();
                WriteLn(pdf, ObjNumber(fontFileId));
                byte[] fontfile = pdfFont.GetFontData();
                MemoryStream fontFileStream = new MemoryStream();
                fontFileStream.Write(fontfile, 0, fontfile.Length);
                WritePDFStream(pdf, fontFileStream, fontFileId, FCompressed, FEncrypted, true, true);
            }

            // descriptor
            long descriptorId = UpdateXRef();
            WriteLn(pdf, ObjNumber(descriptorId));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /FontDescriptor");
            WriteLn(pdf, "/FontName /" + fontName);
            //WriteLn(pdf, "/FontFamily /" + fontName);
            WriteLn(pdf, "/Flags 32");
            WriteLn(pdf, "/FontBBox [" + pdfFont.TextMetric.otmrcFontBox.left.ToString() + " " +
                pdfFont.TextMetric.otmrcFontBox.bottom.ToString() + " " +
                pdfFont.TextMetric.otmrcFontBox.right.ToString() + " " +
                pdfFont.TextMetric.otmrcFontBox.top.ToString() + " ]");
            //WriteLn(pdf, "/Style << /Panose <" + pdfFont.GetPANOSE() + "> >>"); 
            WriteLn(pdf, "/ItalicAngle " + pdfFont.TextMetric.otmItalicAngle.ToString());
            WriteLn(pdf, "/Ascent " + pdfFont.TextMetric.otmAscent.ToString());
            WriteLn(pdf, "/Descent " + pdfFont.TextMetric.otmDescent.ToString());
            WriteLn(pdf, "/Leading " + pdfFont.TextMetric.otmTextMetrics.tmInternalLeading.ToString());
            WriteLn(pdf, "/CapHeight " + pdfFont.TextMetric.otmTextMetrics.tmHeight.ToString());
            WriteLn(pdf, "/StemV " + (50 + Math.Round(Math.Sqrt(pdfFont.TextMetric.otmTextMetrics.tmWeight / 65))).ToString());
            WriteLn(pdf, "/AvgWidth " + pdfFont.TextMetric.otmTextMetrics.tmAveCharWidth.ToString());
            WriteLn(pdf, "/MxWidth " + pdfFont.TextMetric.otmTextMetrics.tmMaxCharWidth.ToString());
            WriteLn(pdf, "/MissingWidth " + pdfFont.TextMetric.otmTextMetrics.tmAveCharWidth.ToString());
            if (FEmbeddingFonts)
                WriteLn(pdf, "/FontFile2 " + ObjNumberRef(fontFileId));
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");

            // ToUnicode
            long toUnicodeId = UpdateXRef();
            WriteLn(pdf, ObjNumber(toUnicodeId));
            StringBuilder toUnicode = new StringBuilder(2048);
            toUnicode.AppendLine("/CIDInit /ProcSet findresource begin");
            toUnicode.AppendLine("12 dict begin");
            toUnicode.AppendLine("begincmap");
            toUnicode.AppendLine("/CIDSystemInfo");
            toUnicode.AppendLine("<< /Registry (Adobe)");
            toUnicode.AppendLine("/Ordering (UCS)");
            toUnicode.AppendLine("/Ordering (Identity)");
            toUnicode.AppendLine("/Supplement 0");
            toUnicode.AppendLine(">> def");
            toUnicode.Append("/CMapName /").Append(pdfFont.GetEnglishFontName().Replace(',', '+')).AppendLine(" def");
            toUnicode.AppendLine("/CMapType 2 def");
            toUnicode.AppendLine("1 begincodespacerange");
            toUnicode.AppendLine("<0000> <FFFF>");
            toUnicode.AppendLine("endcodespacerange");
            toUnicode.Append(pdfFont.UsedGlyphIndexes.Count.ToString()).AppendLine(" beginbfchar");
            for (int i = 0; i < pdfFont.UsedGlyphIndexes.Count; i++)
                toUnicode.Append("<").Append(pdfFont.UsedGlyphIndexes[i].ToString("X4")).Append("> <").Append(pdfFont.UsedAlphabetUnicode[i].ToString("X4")).AppendLine(">");
            toUnicode.AppendLine("endbfchar");
            toUnicode.AppendLine("endcmap");
            toUnicode.AppendLine("CMapName currentdict /CMap defineresource pop");
            toUnicode.AppendLine("end");
            toUnicode.AppendLine("end");

            MemoryStream tounicodeStream = new MemoryStream();
            Write(tounicodeStream, toUnicode.ToString());
            WritePDFStream(pdf, tounicodeStream, toUnicodeId, FCompressed, FEncrypted, true, true);

            //CIDSystemInfo
            long cIDSystemInfoId = UpdateXRef();
            WriteLn(pdf, ObjNumber(cIDSystemInfoId));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Registry (Adobe) /Ordering (Identity) /Supplement 0");
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");

            //DescendantFonts
            long descendantFontId = UpdateXRef();
            WriteLn(pdf, ObjNumber(descendantFontId));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /Font");
            WriteLn(pdf, "/Subtype /CIDFontType2");
            WriteLn(pdf, "/BaseFont /" + fontName);
            WriteLn(pdf, "/CIDToGIDMap /Identity");
            WriteLn(pdf, "/CIDSystemInfo " + ObjNumberRef(cIDSystemInfoId));
            WriteLn(pdf, "/FontDescriptor " + ObjNumberRef(descriptorId));
            Write(pdf, "/W [ ");
            for (int i = 0; i < pdfFont.UsedGlyphIndexes.Count; i++)
                Write(pdf, pdfFont.UsedGlyphIndexes[i].ToString() + " [" + pdfFont.Widths[i].ToString() + "] ");
            WriteLn(pdf, "]");
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");

            // main
            FXRef[(int)(pdfFont.Reference - 1)] = pdf.Position;
            WriteLn(pdf, ObjNumber(pdfFont.Reference));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /Font");
            WriteLn(pdf, "/Subtype /Type0");
            WriteLn(pdf, "/BaseFont /" + fontName);
            WriteLn(pdf, "/Encoding /Identity-H");
            WriteLn(pdf, "/DescendantFonts [" + ObjNumberRef(descendantFontId) + "]");
            WriteLn(pdf, "/ToUnicode " + ObjNumberRef(toUnicodeId));
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");
        }
    }
}
