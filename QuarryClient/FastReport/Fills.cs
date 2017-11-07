using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;

namespace FastReport
{
  /// <summary>
  /// Base class for all fills.
  /// </summary>
  [TypeConverter(typeof(FillConverter))]
  public abstract class FillBase
  {
    internal string Name
    {
      get { return GetType().Name.Replace("Fill", ""); }
    }

    internal bool FloatDiff(float f1, float f2)
    {
      return Math.Abs(f1 - f2) > 1e-4;
    }

    /// <summary>
    /// Creates exact copy of this fill.
    /// </summary>
    /// <returns>Copy of this object.</returns>
    public abstract FillBase Clone();

    /// <summary>
    /// Creates the GDI+ Brush object.
    /// </summary>
    /// <param name="rect">Drawing rectangle.</param>
    /// <returns>Brush object.</returns>
    public abstract Brush CreateBrush(RectangleF rect);

    /// <summary>
    /// Serializes the fill.
    /// </summary>
    /// <param name="writer">Writer object.</param>
    /// <param name="prefix">Name of the fill property.</param>
    /// <param name="fill">Fill object to compare with.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public virtual void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      if (fill.GetType() != GetType())
        writer.WriteStr(prefix, Name);
    }

    /// <summary>
    /// Fills the specified rectangle.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    /// <param name="rect">Drawing rectangle.</param>
    public virtual void Draw(FRPaintEventArgs e, RectangleF rect)
    {
      rect = new RectangleF(rect.Left * e.ScaleX, rect.Top * e.ScaleY, rect.Width * e.ScaleX, rect.Height * e.ScaleY);
      using (Brush brush = CreateBrush(rect))
      {
        e.Graphics.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
      }
    }
  }
  
  /// <summary>
  /// Class represents the solid fill.
  /// </summary>
  public class SolidFill : FillBase
  {
    private Color FColor;

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color Color
    {
      get { return FColor; }
      set { FColor = value; }
    }

    /// <inheritdoc/>
    public override FillBase Clone()
    {
      return new SolidFill(Color);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return Color.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      SolidFill f = obj as SolidFill;
      return f != null && Color == f.Color;
    }

    /// <inheritdoc/>
    public override Brush CreateBrush(RectangleF rect)
    {
      return new SolidBrush(Color);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      base.Serialize(writer, prefix, fill);
      SolidFill c = fill as SolidFill;
      
      if (c == null || c.Color != Color)
        writer.WriteValue(prefix + ".Color", Color);
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e, RectangleF rect)
    {
      if (Color == Color.Transparent)
        return;
      Brush brush = e.Cache.GetBrush(Color);
      e.Graphics.FillRectangle(brush, rect.Left * e.ScaleX, rect.Top * e.ScaleY, rect.Width * e.ScaleX, rect.Height * e.ScaleY);
    }
    
    /// <summary>
    /// Initializes the <see cref="SolidFill"/> class with Transparent color.
    /// </summary>
    public SolidFill() : this(Color.Transparent)
    {
    }
    
    /// <summary>
    /// Initializes the <see cref="SolidFill"/> class with specified color.
    /// </summary>
    /// <param name="color"></param>
    public SolidFill(Color color)
    {
      Color = color;
    }
  }
  
  /// <summary>
  /// Class represents the linear gradient fill.
  /// </summary>
  public class LinearGradientFill : FillBase
  {
    private Color FStartColor;
    private Color FEndColor;
    private int FAngle;
    private float FFocus;
    private float FContrast;

    /// <summary>
    /// Gets or sets the start color of the gradient. 
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color StartColor
    {
      get { return FStartColor; }
      set { FStartColor = value; }
    }

    /// <summary>
    /// Gets or sets the end color of the gradient. 
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color EndColor
    {
      get { return FEndColor; }
      set { FEndColor = value; }
    }

    /// <summary>
    /// Gets or sets the angle of the gradient.
    /// </summary>
    [Editor(typeof(AngleEditor), typeof(UITypeEditor))]
    public int Angle
    {
      get { return FAngle; }
      set { FAngle = value % 360; }
    }
    
    /// <summary>
    /// Gets or sets the focus point of the gradient.
    /// </summary>
    /// <remarks>
    /// Value is a floating point value from 0 to 1.
    /// </remarks>
    public float Focus
    {
      get { return FFocus; }
      set 
      { 
        if (value < 0)
          value = 0;
        if (value > 1)
          value = 1;
        FFocus = value; 
      }
    }
    
    /// <summary>
    /// Gets or sets the gradient contrast.
    /// </summary>
    /// <remarks>
    /// Value is a floating point value from 0 to 1.
    /// </remarks>
    public float Contrast
    {
      get { return FContrast; }
      set 
      {
        if (value < 0)
          value = 0;
        if (value > 1)
          value = 1;
        FContrast = value; 
      }
    }

    /// <inheritdoc/>
    public override FillBase Clone()
    {
      return new LinearGradientFill(StartColor, EndColor, Angle, Focus, Contrast);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return StartColor.GetHashCode() ^ (EndColor.GetHashCode() << 1) ^ 
        ((Angle.GetHashCode() + 1) << 2) ^ ((Focus.GetHashCode() + 1) << 10) ^ ((Contrast.GetHashCode() + 1) << 20);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      LinearGradientFill f = obj as LinearGradientFill;
      return f != null && StartColor == f.StartColor && EndColor == f.EndColor && Angle == f.Angle && 
        !FloatDiff(Focus, f.Focus) && !FloatDiff(Contrast, f.Contrast);
    }

    /// <inheritdoc/>
    public override Brush CreateBrush(RectangleF rect)
    {
      // workaround the gradient bug
      rect.Inflate(1, 1);
      
      LinearGradientBrush result = new LinearGradientBrush(rect, StartColor, EndColor, Angle);
      result.SetSigmaBellShape(Focus, Contrast);
      return result;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      base.Serialize(writer, prefix, fill);
      LinearGradientFill c = fill as LinearGradientFill;

      if (c == null || c.StartColor != StartColor)
        writer.WriteValue(prefix + ".StartColor", StartColor);
      if (c == null || c.EndColor != EndColor)
        writer.WriteValue(prefix + ".EndColor", EndColor);
      if (c == null || c.Angle != Angle)
        writer.WriteInt(prefix + ".Angle", Angle);
      if (c == null || FloatDiff(c.Focus, Focus))
        writer.WriteFloat(prefix + ".Focus", Focus);
      if (c == null || FloatDiff(c.Contrast, Contrast))
        writer.WriteFloat(prefix + ".Contrast", Contrast);
    }

    /// <summary>
    /// Initializes the <see cref="LinearGradientFill"/> class with default settings.
    /// </summary>
    public LinearGradientFill() : this(Color.Black, Color.White, 0, 100, 100)
    {
    }

    /// <summary>
    /// Initializes the <see cref="LinearGradientFill"/> class with start and end colors.
    /// </summary>
    /// <param name="startColor">Start color.</param>
    /// <param name="endColor">End color.</param>
    public LinearGradientFill(Color startColor, Color endColor) : this(startColor, endColor, 0)
    {
    }

    /// <summary>
    /// Initializes the <see cref="LinearGradientFill"/> class with start, end colors and angle.
    /// </summary>
    /// <param name="startColor">Start color.</param>
    /// <param name="endColor">End color.</param>
    /// <param name="angle">Angle.</param>
    public LinearGradientFill(Color startColor, Color endColor, int angle) : this(startColor, endColor, angle, 0, 100)
    {
    }

    /// <summary>
    /// Initializes the <see cref="LinearGradientFill"/> class with start and end colors, angle, focus and contrast.
    /// </summary>
    /// <param name="startColor">Start color.</param>
    /// <param name="endColor">End color.</param>
    /// <param name="angle">Angle.</param>
    /// <param name="focus">Focus.</param>
    /// <param name="contrast">Contrast.</param>
    public LinearGradientFill(Color startColor, Color endColor, int angle, float focus, float contrast)
    {
      StartColor = startColor;
      EndColor = endColor;
      Angle = angle;
      Focus = focus;
      Contrast = contrast;
    }
    
  }

  
  /// <summary>
  /// The style of the path gradient.
  /// </summary>
  public enum PathGradientStyle 
  { 
    /// <summary>
    /// Elliptic gradient.
    /// </summary>
    Elliptic, 
    
    /// <summary>
    /// Rectangular gradient.
    /// </summary>
    Rectangular
  }


  /// <summary>
  /// Class represents the path gradient fill.
  /// </summary>
  public class PathGradientFill : FillBase
  {
    private Color FCenterColor;
    private Color FEdgeColor;
    private PathGradientStyle FStyle;

    /// <summary>
    /// Gets or sets the center color of the gradient.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color CenterColor
    {
      get { return FCenterColor; }
      set { FCenterColor = value; }
    }

    /// <summary>
    /// Gets or sets the edge color of the gradient.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color EdgeColor
    {
      get { return FEdgeColor; }
      set { FEdgeColor = value; }
    }

    /// <summary>
    /// Gets or sets the style of the gradient.
    /// </summary>
    public PathGradientStyle Style
    {
      get { return FStyle; }
      set { FStyle = value; }
    }

    /// <inheritdoc/>
    public override FillBase Clone()
    {
      return new PathGradientFill(CenterColor, EdgeColor, Style);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return CenterColor.GetHashCode() ^ (EdgeColor.GetHashCode() << 1) ^ ((Style.GetHashCode() + 1) << 2);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      PathGradientFill f = obj as PathGradientFill;
      return f != null && CenterColor == f.CenterColor && EdgeColor == f.EdgeColor && Style == f.Style;
    }

    /// <inheritdoc/>
    public override Brush CreateBrush(RectangleF rect)
    {
      GraphicsPath path = new GraphicsPath();
      if (Style == PathGradientStyle.Rectangular)
        path.AddRectangle(rect);
      else
      {
        float radius = (float)Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height) / 2 + 1;
        PointF center = new PointF(rect.Left + rect.Width / 2 - 1, rect.Top + rect.Height / 2 - 1);
        RectangleF r = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        path.AddEllipse(r);
      }
      PathGradientBrush result = new PathGradientBrush(path);
      path.Dispose();
      result.CenterColor = CenterColor;
      result.SurroundColors = new Color[] { EdgeColor };
      return result;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      base.Serialize(writer, prefix, fill);
      PathGradientFill c = fill as PathGradientFill;

      if (c == null || c.CenterColor != CenterColor)
        writer.WriteValue(prefix + ".CenterColor", CenterColor);
      if (c == null || c.EdgeColor != EdgeColor)
        writer.WriteValue(prefix + ".EdgeColor", EdgeColor);
      if (c == null || c.Style != Style)
        writer.WriteValue(prefix + ".Style", Style);
    }

    /// <summary>
    /// Initializes the <see cref="PathGradientFill"/> class with default settings.
    /// </summary>
    public PathGradientFill() : this(Color.Black, Color.White, PathGradientStyle.Elliptic)
    {
    }

    /// <summary>
    /// Initializes the <see cref="PathGradientFill"/> class with center, edge colors and style.
    /// </summary>
    /// <param name="centerColor">Center color.</param>
    /// <param name="edgeColor">Edge color.</param>
    /// <param name="style">Gradient style.</param>
    public PathGradientFill(Color centerColor, Color edgeColor, PathGradientStyle style)
    {
      CenterColor = centerColor;
      EdgeColor = edgeColor;
      Style = style;
    }
  }

  /// <summary>
  /// Class represents the hatch fill.
  /// </summary>
  public class HatchFill : FillBase
  {
    private Color FForeColor;
    private Color FBackColor;
    private HatchStyle FStyle;

    /// <summary>
    /// Gets or sets the foreground color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color ForeColor
    {
      get { return FForeColor; }
      set { FForeColor = value; }
    }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color BackColor
    {
      get { return FBackColor; }
      set { FBackColor = value; }
    }

    /// <summary>
    /// Gets or sets the hatch style.
    /// </summary>
    public HatchStyle Style
    {
      get { return FStyle; }
      set { FStyle = value; }
    }

    /// <inheritdoc/>
    public override FillBase Clone()
    {
      return new HatchFill(ForeColor, BackColor, Style);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return ForeColor.GetHashCode() ^ (BackColor.GetHashCode() << 1) ^ ((Style.GetHashCode() + 1) << 2);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      HatchFill f = obj as HatchFill;
      return f != null && ForeColor == f.ForeColor && BackColor == f.BackColor && Style == f.Style;
    }

    /// <inheritdoc/>
    public override Brush CreateBrush(RectangleF rect)
    {
      return new HatchBrush(Style, ForeColor, BackColor);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      base.Serialize(writer, prefix, fill);
      HatchFill c = fill as HatchFill;

      if (c == null || c.ForeColor != ForeColor)
        writer.WriteValue(prefix + ".ForeColor", ForeColor);
      if (c == null || c.BackColor != BackColor)
        writer.WriteValue(prefix + ".BackColor", BackColor);
      if (c == null || c.Style != Style)
        writer.WriteValue(prefix + ".Style", Style);
    }
    
    /// <summary>
    /// Initializes the <see cref="HatchFill"/> class with default settings.
    /// </summary>
    public HatchFill() : this(Color.Black, Color.White, HatchStyle.BackwardDiagonal)
    {
    }
    
    /// <summary>
    /// Initializes the <see cref="HatchFill"/> class with foreground, background colors and hatch style.
    /// </summary>
    /// <param name="foreColor">Foreground color.</param>
    /// <param name="backColor">Background color.</param>
    /// <param name="style">Hatch style.</param>
    public HatchFill(Color foreColor, Color backColor, HatchStyle style)
    {
      ForeColor = foreColor;
      BackColor = backColor;
      Style = style;
    }
  }


  /// <summary>
  /// Class represents the glass fill.
  /// </summary>
  public class GlassFill : FillBase
  {
    private Color FColor;
    private float FBlend;
    private bool FHatch;

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public Color Color
    {
      get { return FColor; }
      set { FColor = value; }
    }
    
    /// <summary>
    /// Gets or sets the blend value.
    /// </summary>
    /// <remarks>Value must be between 0 and 1.
    /// </remarks>
    [DefaultValue(0.2f)]
    public float Blend
    {
      get { return FBlend; }
      set { FBlend = value < 0 ? 0 : value > 1 ? 1 : value; }
    }
    
    /// <summary>
    /// Gets or sets a value determines whether to draw a hatch or not.
    /// </summary>
    [DefaultValue(true)]
    public bool Hatch
    {
      get { return FHatch; }
      set { FHatch = value; }
    }

    /// <inheritdoc/>
    public override FillBase Clone()
    {
      return new GlassFill(Color, Blend, Hatch);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return Color.GetHashCode() ^ (Blend.GetHashCode() + 1) ^ ((Hatch.GetHashCode() + 1) << 2);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      GlassFill f = obj as GlassFill;
      return f != null && Color == f.Color && Blend == f.Blend && Hatch == f.Hatch;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e, RectangleF rect)
    {
      rect = new RectangleF(rect.Left * e.ScaleX, rect.Top * e.ScaleY, rect.Width * e.ScaleX, rect.Height * e.ScaleY);

      // draw fill
      using (SolidBrush b = new SolidBrush(Color))
      {
        e.Graphics.FillRectangle(b, rect.Left, rect.Top, rect.Width, rect.Height);
      }
      
      // draw hatch
      if (Hatch)
      {
        using (HatchBrush b = new HatchBrush(HatchStyle.DarkUpwardDiagonal,
          Color.FromArgb(40, Color.White), Color.Transparent))
        {
          e.Graphics.FillRectangle(b, rect.Left, rect.Top, rect.Width, rect.Height);
        }
      }

      // draw blend
      using (SolidBrush b = new SolidBrush(Color.FromArgb((int)(Blend * 255), Color.White)))
      {
        e.Graphics.FillRectangle(b, rect.Left, rect.Top, rect.Width, rect.Height / 2);
      }
    }

    /// <inheritdoc/>
    public override Brush CreateBrush(RectangleF rect)
    {
      return new SolidBrush(Color);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer, string prefix, FillBase fill)
    {
      base.Serialize(writer, prefix, fill);
      GlassFill c = fill as GlassFill;

      if (c == null || c.Color != Color)
        writer.WriteValue(prefix + ".Color", Color);
      if (c == null || c.Blend != Blend)
        writer.WriteFloat(prefix + ".Blend", Blend);
      if (c == null || c.Hatch != Hatch)
        writer.WriteBool(prefix + ".Hatch", Hatch);
    }

    /// <summary>
    /// Initializes the <see cref="GlassFill"/> class with default settings.
    /// </summary>
    public GlassFill() : this(Color.White, 0.2f, true)
    {
    }

    /// <summary>
    /// Initializes the <see cref="GlassFill"/> class with given color, blend ratio and hatch style.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <param name="blend">Blend ratio (0..1).</param>
    /// <param name="hatch">Display the hatch.</param>
    public GlassFill(Color color, float blend, bool hatch)
    {
      Color = color;
      Blend = blend;
      Hatch = hatch;
    }
  }
}
