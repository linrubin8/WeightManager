using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using FastReport.Utils;
using FastReport.TypeConverters;
using System.Drawing.Drawing2D;

namespace FastReport.Map
{
  /// <summary>
  /// Represents the color scale.
  /// </summary>
  public class ColorScale : ScaleBase
  {
    #region Fields
    private ColorRanges FData;
    private string FFormat;
    private bool FHideIfNoData;
    private string FNoDataText;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets <see cref="ColorRanges"/> that must be displayed in this color scale.
    /// </summary>
    [Browsable(false)]
    public ColorRanges Data
    {
      get { return FData; }
      set { FData = value; }
    }

    /// <summary>
    /// Gets or sets the format string used to format data values.
    /// </summary>
    public string Format
    {
      get { return FFormat; }
      set { FFormat = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the color scale must be hidden if there is no data in it.
    /// </summary>
    [DefaultValue(true)]
    public bool HideIfNoData
    {
      get { return FHideIfNoData; }
      set { FHideIfNoData = value; }
    }

    /// <summary>
    /// Gets or sets the text displayed in the color scale if there is no data in it.
    /// </summary>
    public string NoDataText
    {
      get { return FNoDataText; }
      set { FNoDataText = value; }
    }
    #endregion // Properties

    #region Private Methods
    private Color GetMixedColor(Color color1, Color color2, float p)
    {
      return Color.FromArgb(
        (byte)(color1.R * (1 - p) + color2.R * p),
        (byte)(color1.G * (1 - p) + color2.G * p),
        (byte)(color1.B * (1 - p) + color2.B * p));
    }

    private float GetMaxRangeWidth()
    {
      if (Data != null && Data.RangeCount > 0)
      {
        double maxData = Data.Ranges[Data.RangeCount - 1].EndValue;
        if (Data.Ranges[Data.RangeCount - 1].IsEndValueEmpty)
          maxData = 100000;
        float maxTextWidth = DrawUtils.MeasureString(maxData.ToString(Format), Font).Width + 16;
        if (maxTextWidth < 50)
          maxTextWidth = 50;
        return maxTextWidth / 2;
      }
      return 0;
    }
    
    private float GetDataWidth(bool includingGaps)
    {
      if (Data != null)
        return (Data.RangeCount + (includingGaps ? 2 : 0)) * GetMaxRangeWidth();
      return 0;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(ScaleBase src)
    {
      base.Assign(src);
      ColorScale c = src as ColorScale;
      Format = c.Format;
      HideIfNoData = c.HideIfNoData;
      NoDataText = c.NoDataText;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, ScaleBase diff)
    {
      ColorScale c = diff as ColorScale;
      base.Serialize(writer, prefix, diff);
      
      if (Format != c.Format)
        writer.WriteStr(prefix + ".Format", Format);
      if (HideIfNoData != c.HideIfNoData)
        writer.WriteBool(prefix + ".HideIfNoData", HideIfNoData);
      if (NoDataText != c.NoDataText)
        writer.WriteStr(prefix + ".NoDataText", NoDataText);
    }

    /// <inheritdoc/>
    public override SizeF CalcSize()
    {
      SizeF size = base.CalcSize();
      float dataWidth = GetDataWidth(true);
      if (size.Width < dataWidth)
        size.Width = dataWidth;
      size.Height += 60;
      return size;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e, MapObject parent)
    {
      bool noData = Data == null || Data.RangeCount == 0 || Data.Ranges[0].IsStartValueEmpty;
      if (noData && HideIfNoData)
        return;
      
      base.Draw(e, parent);

      Pen borderPen = e.Cache.GetPen(BorderColor, e.ScaleX, System.Drawing.Drawing2D.DashStyle.Solid);
      Brush textBrush = e.Cache.GetBrush(TextColor);
      Font font = e.Cache.GetFont(Font.Name,
        parent.IsPrinting ? Font.Size : Font.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi,
        Font.Style);
      StringFormat format = e.Cache.GetStringFormat(StringAlignment.Center, StringAlignment.Near,
        StringTrimming.EllipsisCharacter, StringFormatFlags.NoWrap, 0, 0);
      RectangleF drawRect = GetDrawRect(parent);

      if (noData)
      {
        drawRect.Offset(0, drawRect.Height - 36);
        e.Graphics.DrawString(NoDataText, font, textBrush, drawRect, format);
        return;
      }

      float dataWidth = GetDataWidth(false);
      float offsetX = (drawRect.Width - dataWidth) / 2;
      float offsetY = drawRect.Height - 36;
      float sizeX = GetMaxRangeWidth();
      float sizeY = 16;
      bool textOnTop = false;

      double[] values = new double[Data.RangeCount + 1];
      Color[] colors = new Color[Data.RangeCount];
      for (int i = 0; i < Data.RangeCount; i++)
      {
        ColorRange range = Data.Ranges[i];
        values[i] = range.StartValue;
        colors[i] = range.Color;
        if (i == Data.RangeCount - 1)
          values[i + 1] = range.EndValue;
      }

      for (int i = 0; i < values.Length; i++)
      {
        RectangleF rect = new RectangleF((drawRect.Left + offsetX) * e.ScaleX, 
          (drawRect.Top + offsetY) * e.ScaleY, sizeX * e.ScaleX, sizeY * e.ScaleY);
        if (i < values.Length - 1)
        {
          using (LinearGradientBrush b = new LinearGradientBrush(rect, colors[i], GetMixedColor(colors[i], Color.White, 0.5f), 270f))
            e.Graphics.FillRectangle(b, rect);
          e.Graphics.DrawRectangle(borderPen, rect.Left, rect.Top, rect.Width, rect.Height);
        }
        e.Graphics.DrawLine(borderPen,
          rect.Left,
          textOnTop ? rect.Top : rect.Bottom,
          rect.Left,
          textOnTop ? rect.Top - 3 : rect.Bottom + 3);

        string text = values[i].ToString(Format);
        SizeF textSize = e.Graphics.MeasureString(text, font);
        e.Graphics.DrawString(text, font, textBrush, 
          rect.Left - textSize.Width / 2, 
          textOnTop ? rect.Top - textSize.Height - 2 : rect.Bottom + 3);

        textOnTop = !textOnTop;
        offsetX += sizeX;
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorScale"/> class.
    /// </summary>
    public ColorScale()
    {
      FFormat = "0";
      FNoDataText = Res.Get("Forms,MapEditor,MapEditorControl,ColorScale,NoData");
      FHideIfNoData = true;
    }
  }
}
