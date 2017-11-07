using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Globalization;
using FastReport.TypeConverters;
using FastReport.Utils;

namespace FastReport.Map
{
  /// <summary>
  /// Represents a set of color ranges used to highlight polygons based on analytical value.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class ColorRanges
  {
    #region Fields
    private List<ColorRange> FRanges;
    private Color FStartColor;
    private Color FMiddleColor;
    private Color FEndColor;
    private bool FShowInColorScale;
    #endregion // Fields

    #region Properties
     /// <summary>
    /// Gets the list of ranges.
    /// </summary>
    public List<ColorRange> Ranges
    {
      get { return FRanges; }
    }

    /// <summary>
    /// Gets or sets the number of ranges.
    /// </summary>
    public int RangeCount
    {
      get { return Ranges.Count; }
      set
      {
        if (Ranges.Count < value)
        {
          while (Ranges.Count < value)
            Ranges.Add(new ColorRange());
        }
        else if (Ranges.Count > value)
        {
          while (Ranges.Count > value)
            Ranges.RemoveAt(Ranges.Count - 1);
        }
      }
    }

    /// <summary>
    /// Gets or sets the start color.
    /// </summary>
    public Color StartColor
    {
      get { return FStartColor; }
      set { FStartColor = value; }
    }

    /// <summary>
    /// Gets or sets the middle color.
    /// </summary>
    public Color MiddleColor
    {
      get { return FMiddleColor; }
      set { FMiddleColor = value; }
    }

    /// <summary>
    /// Gets or sets the end color.
    /// </summary>
    public Color EndColor
    {
      get { return FEndColor; }
      set { FEndColor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the map's color scale must display data from this color ranges.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowInColorScale
    {
      get { return FShowInColorScale; }
      set { FShowInColorScale = value; }
    }

    /// <summary>
    /// Gets or sets ranges as a string.
    /// </summary>
    [Browsable(false)]
    public string RangesAsString
    {
      get { return GetRangesAsString(); }
      set { SetRangesAsString(value); }
    }
    #endregion // Properties

    #region Private Methods
    private string GetRangesAsString()
    {
      StringBuilder result = new StringBuilder();
      foreach (ColorRange range in Ranges)
      {
        result.Append(range.GetAsString()).Append("\r\n");
      }
      if (result.Length > 2)
        result.Remove(result.Length - 2, 2);
      return result.ToString();
    }

    private void SetRangesAsString(string value)
    {
      Ranges.Clear();
      if (String.IsNullOrEmpty(value))
        return;
      string[] values = value.Split(new string[] { "\r\n" }, StringSplitOptions.None);
      foreach (string val in values)
      {
        ColorRange range = new ColorRange();
        range.SetAsString(val);
        Ranges.Add(range);
      }
    }

    private Color GetMixedColor(Color color1, Color color2, float p)
    {
      return Color.FromArgb(
        (byte)(color1.R * (1 - p) + color2.R * p),
        (byte)(color1.G * (1 - p) + color2.G * p),
        (byte)(color1.B * (1 - p) + color2.B * p));
    }

    private void SetRangeColor(ColorRange range, Color color)
    {
      if (range.IsColorEmpty)
        range.Color = color;
    }

    private void FillRangeColors(int startIndex, int endIndex)
    {
      int numColors = endIndex - startIndex;
      if (numColors < 2)
        return;

      float colorDelta = 1f / numColors;
      Color startColor = Ranges[startIndex].Color;
      Color endColor = Ranges[endIndex].Color;

      for (int i = 1; i < numColors; i++)
      {
        SetRangeColor(Ranges[i + startIndex], GetMixedColor(startColor, endColor, i * colorDelta));
      }
    }
    
    private void FillRangeColors()
    {
      if (RangeCount == 0)
        return;

      SetRangeColor(Ranges[0], StartColor);
      SetRangeColor(Ranges[RangeCount - 1], EndColor);
      if (RangeCount > 2)
      {
        if (MiddleColor != Color.Transparent)
        {
          int middle = RangeCount / 2;
          SetRangeColor(Ranges[middle], MiddleColor);
          FillRangeColors(0, middle);
          FillRangeColors(middle, RangeCount - 1);
        }
        else
          FillRangeColors(0, RangeCount - 1);
      }
    }

    private void FillRangeValues(double min, double max)
    {
      double delta = (max - min) / RangeCount;
      for (int i = 0; i < RangeCount; i++)
      {
        ColorRange range = Ranges[i];
        if (range.IsStartValueEmpty)
          range.StartValue = min + delta * i;
        // make last EndValue bigger to fit largest data value in this range
        if (range.IsEndValueEmpty)
          range.EndValue = min + delta * (i + 1) + (i == RangeCount - 1 ? 0.1 : 0);
      }
    }
    #endregion // Private Methods

    #region Public Methods
    /// <summary>
    /// Copies the contents of another ColorRanges.
    /// </summary>
    /// <param name="src">The ColorRanges instance to copy the contents from.</param>
    public void Assign(ColorRanges src)
    {
      StartColor = src.StartColor;
      MiddleColor = src.MiddleColor;
      EndColor = src.EndColor;
      ShowInColorScale = src.ShowInColorScale;
      RangeCount = src.RangeCount;
      for (int i = 0; i < RangeCount; i++)
        Ranges[i].Assign(src.Ranges[i]);
    }

    /// <summary>
    /// Gets a color associated with given analytical value.
    /// </summary>
    /// <param name="value">The analytical value.</param>
    /// <returns>The color associated with this value, or <b>Color.Transparent</b> if no association found.</returns>
    public Color GetColor(double value)
    {
      foreach (ColorRange range in Ranges)
      {
        if (value >= range.StartValue && value < range.EndValue)
          return range.Color;
      }
      return Color.White;
    }

    internal void Fill(double min, double max)
    {
      FillRangeColors();
      FillRangeValues(min, max);
    }
    
    internal void SaveState()
    {
      foreach (ColorRange range in Ranges)
      {
        range.SaveState();
      }
    }

    internal void RestoreState()
    {
      foreach (ColorRange range in Ranges)
      {
        range.RestoreState();
      }
    }

    internal void Serialize(FRWriter writer, string prefix)
    {
      writer.WriteValue(prefix + ".StartColor", StartColor);
      writer.WriteValue(prefix + ".MiddleColor", MiddleColor);
      writer.WriteValue(prefix + ".EndColor", EndColor);
      writer.WriteBool(prefix + ".ShowInColorScale", ShowInColorScale);
      writer.WriteStr(prefix + ".RangesAsString", RangesAsString);
    }
    #endregion // Public Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorRanges"/> class.
    /// </summary>
    public ColorRanges()
    {
      FRanges = new List<ColorRange>();
      FStartColor = Color.Red;
      FMiddleColor = Color.Yellow;
      FEndColor = Color.Green;
      FShowInColorScale = true;
    }
  }

  /// <summary>
  /// Represents a single color range.
  /// </summary>
  public class ColorRange
  {
    #region Fields
    private Color FColor;
    private double FStartValue;
    private double FEndValue;
    private ColorRange FState;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets color of the range.
    /// </summary>
    public Color Color
    {
      get { return FColor; }
      set { FColor = value; }
    }

    /// <summary>
    /// Gets or sets start value of the range.
    /// </summary>
    public double StartValue
    {
      get { return FStartValue; }
      set { FStartValue = value; }
    }

    /// <summary>
    /// Gets or sets end value of the range.
    /// </summary>
    public double EndValue
    {
      get { return FEndValue; }
      set { FEndValue = value; }
    }

    internal bool IsColorEmpty
    {
      get { return Color == Color.Transparent; }
    }

    internal bool IsStartValueEmpty
    {
      get { return double.IsNaN(StartValue); }
    }

    internal bool IsEndValueEmpty
    {
      get { return double.IsNaN(EndValue); }
    }
    #endregion // Properties

    #region Public Methods
    /// <summary>
    /// Copies the contents of another ColorRange.
    /// </summary>
    /// <param name="src">The ColorRange instance to copy the contents from.</param>
    public void Assign(ColorRange src)
    {
      Color = src.Color;
      StartValue = src.StartValue;
      EndValue = src.EndValue;
    }
    
    internal void SaveState()
    {
      if (FState == null)
        FState = new ColorRange();
      FState.Assign(this);
    }

    internal void RestoreState()
    {
      if (FState != null)
        Assign(FState);
    }

    internal string GetAsString()
    {
      return Converter.ToString(Color) + ";" + StartValue.ToString(CultureInfo.InvariantCulture.NumberFormat) + ";" +
        EndValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
    }

    internal void SetAsString(string value)
    {
      Reset();
      if (String.IsNullOrEmpty(value))
        return;
      string[] val = value.Split(new char[] { ';' });
      if (val.Length != 3)
        return;
      Color = (Color)Converter.FromString(typeof(Color), val[0]);
      StartValue = double.Parse(val[1], CultureInfo.InvariantCulture.NumberFormat);
      EndValue = double.Parse(val[2], CultureInfo.InvariantCulture.NumberFormat);
    }

    internal void Reset()
    {
      FColor = Color.Transparent;
      FStartValue = double.NaN;
      FEndValue = double.NaN;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorRange"/> class.
    /// </summary>
    public ColorRange()
    {
      Reset();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorRange"/> class with a specified parameters.
    /// </summary>
    /// <param name="color">The color of the range.</param>
    /// <param name="startValue">The start value of the range.</param>
    /// <param name="endValue">The end value of the range.</param>
    public ColorRange(Color color, double startValue, double endValue)
    {
      FColor = color;
      FStartValue = startValue;
      FEndValue = endValue;
    }
  }
}
