using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using System.Drawing.Drawing2D;

namespace FastReport.Barcode
{
    internal enum BarLineType
    {
        White,
        Black,

        // line with 2/5 height (used for PostNet)
        BlackHalf,

        // start/stop/middle lines in EAN, UPC codes
        BlackLong,

        // for Intelligent Mail Barcode
        // see https://upload.wikimedia.org/wikipedia/commons/7/7a/Four_State_Barcode.svg
        BlackTracker,
        BlackAscender,
        BlackDescender,
    }

  /// <summary>
  /// The base class for linear (1D) barcodes.
  /// </summary>
  public class LinearBarcodeBase : BarcodeBase
  {
    #region Fields
    private float FWideBarRatio;
    private float[] FModules;
    private bool FCalcCheckSum;
    internal RectangleF FDrawArea;
    internal RectangleF FBarArea;
    internal static Font FFont = new Font("Arial", 8);
    internal static Font FSmallFont = new Font("Arial", 6);
    internal bool FTextUp;
    internal float FRatioMin;
    internal float FRatioMax;
    internal float FExtra1;
    internal float FExtra2;
    internal string FPattern;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value that determines if the barcode object should calculate
    /// the check digit automatically.
    /// </summary>
    [DefaultValue(true)]
    public bool CalcCheckSum
    {
      get { return FCalcCheckSum; }
      set { FCalcCheckSum = value; }
    }

    /// <summary>
    /// Gets or sets a relative width of wide bars in the barcode.
    /// </summary>
    [DefaultValue(2f)]
    public float WideBarRatio
    {
      get { return FWideBarRatio; }
      set
      {
        FWideBarRatio = value;
        if (FRatioMin != 0 && FWideBarRatio < FRatioMin)
          FWideBarRatio = FRatioMin;
        if (FRatioMax != 0 && FWideBarRatio > FRatioMax)
          FWideBarRatio = FRatioMax;
      }
    }

    /// <summary>
    /// Gets the value indicating that the barcode is numeric.
    /// </summary>
    [Browsable(false)]
    public virtual bool IsNumeric
    {
      get { return true; }
    }

    private string Code
    {
      get
      {
        MakeModules();
        FPattern = GetPattern();
        return FPattern;
      }
    }
    #endregion

    #region Private Methods
    private void CheckText(string text)
    {
      foreach (char i in text)
      {
        if (i < '0' || i > '9')
          throw new Exception(Res.Get("Messages,InvalidBarcode2"));
      }
    }

    private void MakeModules()
    {
      FModules[0] = 1;
      FModules[1] = FModules[0] * WideBarRatio;
      FModules[2] = FModules[1] * 1.5f;
      FModules[3] = FModules[1] * 2;
    }

    private void DoLines(string data, Graphics g, float zoom)
    {
      using (Pen pen = new Pen(Color))
      {
        float currentWidth = 0;
        foreach (char c in data)
        {
          float width;
          BarLineType lt;
          OneBarProps(c, out width, out lt);

          float heightStart = 0;
          float heightEnd = FBarArea.Height;

          if (lt == BarLineType.BlackHalf)
          {
            heightEnd = FBarArea.Height * 2 / 5;
          }
          else if (lt == BarLineType.BlackLong && ShowText)
          {
            heightEnd += 7;
          }
          else if (lt == BarLineType.BlackTracker)
          {
            heightStart = FBarArea.Height * 1 / 3;
            heightEnd = FBarArea.Height * 2 / 3;
          }
          else if (lt == BarLineType.BlackAscender)
          {
            heightEnd = FBarArea.Height * 2 / 3;
          }
          else if (lt == BarLineType.BlackDescender)
          {
            heightStart = FBarArea.Height * 1 / 3;
          }

          width *= zoom;
          heightStart *= zoom;
          heightEnd *= zoom;
          pen.Width = width;

          if (lt == BarLineType.BlackHalf)
          {
            g.DrawLine(pen,
                currentWidth + width / 2,
                FBarArea.Bottom * zoom,
                currentWidth + width / 2,
                FBarArea.Bottom * zoom - heightEnd);
          }
          else if (lt != BarLineType.White)
          {
            g.DrawLine(pen,
                currentWidth + width / 2,
                FBarArea.Top * zoom + heightStart,
                currentWidth + width / 2,
                FBarArea.Top * zoom + heightEnd);
          }

          currentWidth += width;
        }
      }
    }

    private string CheckSumModulo10(string data)
    {
      int sum = 0;
      int fak = data.Length;

      for (int i = 0; i < data.Length; i++)
      {
        if ((fak % 2) == 0)
          sum += int.Parse(data[i].ToString());
        else
          sum += int.Parse(data[i].ToString()) * 3;
        fak--;
      }

      if ((sum % 10) == 0)
        return data + "0";
      else
        return data + (10 - (sum % 10)).ToString();
    }

    private void OneBarProps(char code, out float width, out BarLineType lt)
    {
      switch (code)
      {
        case '0':
          width = FModules[0];
          lt = BarLineType.White;
          break;
        case '1':
          width = FModules[1];
          lt = BarLineType.White;
          break;
        case '2':
          width = FModules[2];
          lt = BarLineType.White;
          break;
        case '3':
          width = FModules[3];
          lt = BarLineType.White;
          break;
        case '5':
          width = FModules[0];
          lt = BarLineType.Black;
          break;
        case '6':
          width = FModules[1];
          lt = BarLineType.Black;
          break;
        case '7':
          width = FModules[2];
          lt = BarLineType.Black;
          break;
        case '8':
          width = FModules[3];
          lt = BarLineType.Black;
          break;
        case '9':
          width = FModules[0];
          lt = BarLineType.BlackHalf;
          break;
        case 'A':
          width = FModules[0];
          lt = BarLineType.BlackLong;
          break;
        case 'B':
          width = FModules[1];
          lt = BarLineType.BlackLong;
          break;
        case 'C':
          width = FModules[2];
          lt = BarLineType.BlackLong;
          break;
        case 'D':
          width = FModules[3];
          lt = BarLineType.BlackLong;
          break;

        // E,F,G for Intelligent Mail Barcode
        case 'E':
          width = FModules[1];
          lt = BarLineType.BlackTracker;
          break;
        case 'F':
          width = FModules[1];
          lt = BarLineType.BlackAscender;
          break;
        case 'G':
          width = FModules[1];
          lt = BarLineType.BlackDescender;
          break;

        default:
          // something went wrong  :-(
          // mistyped pattern table
          throw new Exception("Incorrect barcode pattern code: " + code);
      }
    }
    #endregion

    #region Protected Methods
    internal int CharToInt(char c)
    {
      return int.Parse(Convert.ToString(c));
    }

    internal string SetLen(int len)
    {
      string result = "";

      for (int i = 0; i < len - Text.Length; i++)
      {
        result = "0" + result;
      }

      result += Text;
      return result.Substring(0, len);
    }

    internal string DoCheckSumming(string data)
    {
      return CheckSumModulo10(data);
    }

    internal string DoCheckSumming(string data, int len)
    {
      if (CalcCheckSum)
        return DoCheckSumming(SetLen(len - 1));
      return SetLen(len);
    }

    internal string DoConvert(string s)
    {
      StringBuilder builder = new StringBuilder(s);

      for (int i = 0; i < s.Length; i++)
      {
        int v = s[i] - 1;

        if ((i % 2) == 0)
          v += 5;

        builder[i] = (char)v;
      }

      return builder.ToString();
    }

    internal string MakeLong(string data)
    {
      StringBuilder builder = new StringBuilder(data);

      for (int i = 0; i < data.Length; i++)
      {
        char c = builder[i];
        if (c >= '5' && c <= '8')
          c = (char)((int)c - (int)'5' + (int)'A');
        builder[i] = c;
      }

      return builder.ToString();
    }

    internal float GetWidth(string code)
    {
      float result = 0;
      float w;
      BarLineType lt;

      foreach (char c in code)
      {
        OneBarProps(c, out w, out lt);
        result += w;
      }
      return result;
    }

    internal virtual string GetPattern()
    {
      return "";
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(BarcodeBase source)
    {
      base.Assign(source);

      LinearBarcodeBase src = source as LinearBarcodeBase;
      WideBarRatio = src.WideBarRatio;
      CalcCheckSum = src.CalcCheckSum;
    }

    internal override void Serialize(FRWriter writer, string prefix, BarcodeBase diff)
    {
      base.Serialize(writer, prefix, diff);
      LinearBarcodeBase c = diff as LinearBarcodeBase;

      if (c == null || WideBarRatio != c.WideBarRatio)
        writer.WriteValue(prefix + "WideBarRatio", WideBarRatio);
      if (c == null || CalcCheckSum != c.CalcCheckSum)
        writer.WriteBool(prefix + "CalcCheckSum", CalcCheckSum);
    }

    internal override void Initialize(string text, bool showText, int angle, float zoom)
    {
      text = text.Trim();
      base.Initialize(text, showText, angle, zoom);
    }

    internal override SizeF CalcBounds()
    {
      float barWidth = GetWidth(Code);
      float extra1 = 0;
      float extra2 = 0;

      if (ShowText)
      {
        float txtWidth = 0;
        using (Bitmap bmp = new Bitmap(1, 1))
        {
          Graphics g = Graphics.FromImage(bmp);
          txtWidth = g.MeasureString(Text, FFont, 100000).Width;
        }

        if (barWidth < txtWidth)
        {
          extra1 = (txtWidth - barWidth) / 2 + 2;
          extra2 = extra1;
        }
      }

      if (FExtra1 != 0)
        extra1 = FExtra1;
      if (FExtra2 != 0)
        extra2 = FExtra2;

      FDrawArea = new RectangleF(0, 0, barWidth + extra1 + extra2, 0);
      FBarArea = new RectangleF(extra1, 0, barWidth, 0);

      return new SizeF(FDrawArea.Width * 1.25f, 0);
    }

    internal override void DrawBarcode(Graphics g, RectangleF displayRect)
    {
      float originalWidth = CalcBounds().Width / 1.25f;
      float width = Angle == 90 || Angle == 270 ? displayRect.Height : displayRect.Width;
      float height = Angle == 90 || Angle == 270 ? displayRect.Width : displayRect.Height;
      Zoom = width / originalWidth;
      FBarArea.Height = height / Zoom;
      if (ShowText)
      {
        FBarArea.Height -= 14;
        if (FTextUp)
          FBarArea.Y = 14;
      }
      FDrawArea.Height = height / Zoom;

      GraphicsState state = g.Save();
      try
      {
        // rotate
        g.TranslateTransform(displayRect.Left, displayRect.Top);
        g.RotateTransform(Angle);
        switch (Angle)
        {
          case 90:
            g.TranslateTransform(0, -displayRect.Width);
            break;
          case 180:
            g.TranslateTransform(-displayRect.Width, -displayRect.Height);
            break;
          case 270:
            g.TranslateTransform(-displayRect.Height, 0);
            break;
        }

        g.TranslateTransform(FBarArea.Left * Zoom, 0);
        DoLines(FPattern, g, Zoom);
        if (ShowText)
          DrawText(g, Text);
      }
      finally
      {
        g.Restore(state);
      }
    }

    internal void DrawString(Graphics g, float x1, float x2, string s)
    {
      DrawString(g, x1, x2, s, false);
    }

    internal void DrawString(Graphics g, float x1, float x2, string s, bool small)
    {
      if (String.IsNullOrEmpty(s))
        return;

      // when we print, .Net automatically scales the font. However, we need to handle this process.
      // Downscale the font to the screen resolution, then scale by required value (Zoom).
      float fontZoom = 14f / (int)g.MeasureString(s, FFont).Height * Zoom;
      Font font = small ? FSmallFont : FFont;
      using (Font drawFont = new Font(font.Name, font.Size * fontZoom, font.Style))
      {
        SizeF size = g.MeasureString(s, drawFont);
        size.Width /= Zoom;
        size.Height /= Zoom;
        g.DrawString(s, drawFont, new SolidBrush(Color),
          (x1 + (x2 - x1 - size.Width) / 2) * Zoom,
          (FTextUp ? 0 : FDrawArea.Height - size.Height) * Zoom);
      }
    }

    internal virtual void DrawText(Graphics g, string data)
    {
      data = StripControlCodes(data);
      DrawString(g, 0, FDrawArea.Width, data);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="LinearBarcodeBase"/> class with default settings.
    /// </summary>
    public LinearBarcodeBase()
    {
      FModules = new float[4];
      WideBarRatio = 2;
      FCalcCheckSum = true;
    }
  }
}
