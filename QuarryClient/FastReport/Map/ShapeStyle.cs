using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;

namespace FastReport.Map
{
  /// <summary>
  /// Represents the style of a shape.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class ShapeStyle
  {
    #region Fields
    private Color FBorderColor;
    private DashStyle FBorderStyle;
    private float FBorderWidth;
    private Color FFillColor;
    private Font FFont;
    private Color FTextColor;
    private float FPointSize;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color BorderColor
    {
      get { return FBorderColor; }
      set { FBorderColor = value; }
    }

    /// <summary>
    /// Gets or sets the border style.
    /// </summary>
    [DefaultValue(DashStyle.Solid)]
    public DashStyle BorderStyle
    {
      get { return FBorderStyle; }
      set { FBorderStyle = value; }
    }

    /// <summary>
    /// Gets or sets the border width.
    /// </summary>
    [DefaultValue(1f)]
    public float BorderWidth
    {
      get { return FBorderWidth; }
      set { FBorderWidth = value; }
    }

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color FillColor
    {
      get { return FFillColor; }
      set { FFillColor = value; }
    }

    /// <summary>
    /// Gets or sets the font.
    /// </summary>
    public Font Font
    {
      get { return FFont; }
      set { FFont = value; }
    }

    /// <summary>
    /// Gets or sets the text color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color TextColor
    {
      get { return FTextColor; }
      set { FTextColor = value; }
    }

    /// <summary>
    /// Gets or sets the point size, in pixels.
    /// </summary>
    [DefaultValue(10f)]
    public float PointSize
    {
      get { return FPointSize; }
      set { FPointSize = value; }
    }
    #endregion // Properties

    #region Public Methods
    /// <summary>
    /// Copies contents from another similar object.
    /// </summary>
    /// <param name="src">The object to copy the contents from.</param>
    public void Assign(ShapeStyle src)
    {
      BorderColor = src.BorderColor;
      BorderStyle = src.BorderStyle;
      BorderWidth = src.BorderWidth;
      FillColor = src.FillColor;
      Font = src.Font;
      TextColor = src.TextColor;
      PointSize = src.PointSize;
    }

    internal void Serialize(FRWriter writer, string prefix, ShapeStyle c)
    {
      if (BorderColor != c.BorderColor)
        writer.WriteValue(prefix + ".BorderColor", BorderColor);
      if (BorderStyle != c.BorderStyle)
        writer.WriteValue(prefix + ".BorderStyle", BorderStyle);
      if (BorderWidth != c.BorderWidth)
        writer.WriteFloat(prefix + ".BorderWidth", BorderWidth);
      if (FillColor != c.FillColor)
        writer.WriteValue(prefix + ".FillColor", FillColor);
      if (!Font.Equals(c.Font))
        writer.WriteValue(prefix + ".Font", Font);
      if (TextColor != c.TextColor)
        writer.WriteValue(prefix + ".TextColor", TextColor);
      if (PointSize != c.PointSize)
        writer.WriteFloat(prefix + ".PointSize", PointSize);
    }
    #endregion // Public Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeStyle"/> class.
    /// </summary>
    public ShapeStyle()
    {
      BorderColor = Color.DarkGray;
      BorderStyle = DashStyle.Solid;
      BorderWidth = 1;
      FillColor = Color.White;
      Font = DrawUtils.DefaultFont;
      TextColor = Color.Black;
      PointSize = 10;
    }
  }
}
