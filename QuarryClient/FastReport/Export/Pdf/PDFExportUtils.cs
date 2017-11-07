using FastReport.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        private float GetTop(float p)
        {
            return FMarginWoBottom - p * PDF_DIVIDER;
        }

        private float GetLeft(float p)
        {
            return FMarginLeft + p * PDF_DIVIDER;
        }

        private string FloatToString(double value)
        {
            return Convert.ToString(Math.Round(value, 2), FNumberFormatInfo);
        }

        private string FloatToString(double value, int digits)
        {
          return Convert.ToString(Math.Round(value, digits), FNumberFormatInfo);
        }

        private string StringToPdfUnicode(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length * 2 + 2);
            sb.Append((char)254).Append((char)255);
            foreach (char c in s)
                sb.Append((char)(c >> 8)).Append((char)(c & 0xFF));
            return sb.ToString();
        }

        private void PrepareString(string text, byte[] key, bool encode, long id, StringBuilder sb)
        {
            string s = encode ? RC4CryptString(StringToPdfUnicode(text), key, id) : StringToPdfUnicode(text);
            sb.Append("(");
            EscapeSpecialChar(s, sb);
            sb.Append(")");
        }

        private void Write(Stream stream, string value)
        {
            stream.Write(ExportUtils.StringToByteArray(value), 0, value.Length);
        }

        private void WriteLn(Stream stream, string value)
        {
            stream.Write(ExportUtils.StringToByteArray(value), 0, value.Length);
            stream.WriteByte(0x0d);
            stream.WriteByte(0x0a);
        }

        private void StrToUTF16(string str, StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(str))
            {
                sb.Append("FEFF");
                foreach (char c in str)
                    sb.Append(((int)c).ToString("X4"));
            }
        }

        private void EscapeSpecialChar(string input, StringBuilder sb)
        {
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '(':
                        sb.Append(@"\(");
                        break;
                    case ')':
                        sb.Append(@"\)");
                        break;
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    default:
                        sb.Append(input[i]);
                        break;
                }
            }
        }

        private float GetBaseline(Font f)
        {
            float baselineOffset = f.SizeInPoints / f.FontFamily.GetEmHeight(f.Style) * f.FontFamily.GetCellAscent(f.Style);
            return DrawUtils.ScreenDpi / 72f * baselineOffset;
        }

        private void GetPDFFillColor(Color color, StringBuilder sb)
        {
            GetPDFFillTransparent(color, sb);
            if (ColorSpace == PdfColorSpace.CMYK)
            {
                GetCMYKColor(color, sb);
                sb.AppendLine(" k");
            }
            else if (ColorSpace == PdfColorSpace.RGB)
            {
                GetPDFColor(color, sb);
                sb.AppendLine(" rg");
            }
        }

        private void GetPDFFillTransparent(Color color, StringBuilder sb)
        {
            string value = FloatToString((float)color.A / 255f);
            int i = FTrasparentStroke.IndexOf(value);
            if (i == -1)
            {
                FTrasparentStroke.Add(value);
                i = FTrasparentStroke.Count - 1;
            }
            sb.Append("/GS").Append(i.ToString()).AppendLine("S gs");
        }

        private void GetPDFStrokeColor(Color color, StringBuilder sb)
        {
            GetPDFStrokeTransparent(color, sb);
            if (ColorSpace == PdfColorSpace.CMYK)
            {
                GetCMYKColor(color, sb);
                sb.AppendLine(" K");
            }
            else if(ColorSpace == PdfColorSpace.RGB)
            {
                GetPDFColor(color, sb);
                sb.AppendLine(" RG");
            }
        }

        private void GetPDFStrokeTransparent(Color color, StringBuilder sb)
        {
            string value = FloatToString((float)color.A / 255f);
            int i = FTrasparentFill.IndexOf(value);
            if (i == -1)
            {
                FTrasparentFill.Add(value);
                i = FTrasparentFill.Count - 1;
            }
            sb.Append("/GS").Append(i.ToString()).AppendLine("F gs");
        }

        private void GetPDFColor(Color color, StringBuilder sb)
        {
            if (color == Color.Black)
                sb.Append("0 0 0");
            else if (color == Color.White)
                sb.Append("1 1 1");
            else
            {
                sb.Append(FloatToString((float)color.R / 255f, 3)).Append(" ").
                    Append(FloatToString((float)color.G / 255f, 3)).Append(" ").
                    Append(FloatToString((float)color.B / 255f, 3));
            }
        }

        private void GetCMYKColor(Color color, StringBuilder sb)
        {
            if (color == Color.Black)
                sb.Append("0 0 0 1");
            else if (color == Color.White)
                sb.Append("0 0 0 0");
            else
            {
                CMYKColor cmyk = new CMYKColor(color);
                sb.Append(FloatToString((float)cmyk.C / 100f, 3)).Append(" ").
                    Append(FloatToString((float)cmyk.M / 100f, 3)).Append(" ").
                    Append(FloatToString((float)cmyk.Y / 100f, 3)).Append(" ").
                    Append(FloatToString((float)cmyk.K / 100f, 3));
            }
        }

        private bool FontEquals(Font font1, Font font2)
        {
            return (font1.Name == font2.Name) && font1.Style.Equals(font2.Style);
        }

        private string PrepXRefPos(long p)
        {
            string pos = p.ToString();
            return new string('0', 10 - pos.Length) + pos;
        }

        private string ObjNumber(long FNumber)
        {
            return String.Concat(FNumber.ToString(), " 0 obj");
        }

        private string ObjNumberRef(long FNumber)
        {
            return String.Concat(FNumber.ToString(), " 0 R");
        }

        private long UpdateXRef()
        {
            FXRef.Add(pdf.Position);
            return FXRef.Count;
        }

        private void WritePDFStream(Stream target, Stream source, long id, bool compress, bool encrypt, bool startingBrackets, bool endingBrackets)
        {
            WritePDFStream(target, source, id, compress, encrypt, startingBrackets, endingBrackets, false);
        }

        private void WritePDFStream(Stream target, Stream source, long id, bool compress, bool encrypt, bool startingBrackets, bool endingBrackets, bool enableLength1)
        {
            MemoryStream tempStream;

            if (startingBrackets)
                WriteLn(target, "<<");

            using (tempStream = new MemoryStream())
            {
                if (compress)
                {
                    ExportUtils.ZLibDeflate(source, tempStream);
                    WriteLn(pdf, "/Filter /FlateDecode");
                }
                else
                {
                    //source.CopyTo(tempStream);
                    byte[] tempBuff = new byte[source.Length - source.Position];
                    source.Read(tempBuff, (int)source.Position, (int)(source.Length - source.Position));
                    tempStream.Write(tempBuff, 0, tempBuff.Length);
                    tempStream.Position = 0;
                }

                WriteLn(pdf, "/Length " + tempStream.Length.ToString());

                if (enableLength1)
                    WriteLn(pdf, "/Length1 " + source.Length.ToString());

                if (endingBrackets)
                    WriteLn(target, ">>");
                else
                    WriteLn(target, "");

                WriteLn(target, "stream");

                if (encrypt)
                    RC4CryptStream(tempStream, target, FEncKey, id);
                else
                    tempStream.WriteTo(target);

                target.WriteByte(0x0a);
                WriteLn(target, "endstream");
                WriteLn(target, "endobj");
            }
        }

    }
}
