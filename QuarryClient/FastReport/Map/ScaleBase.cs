using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeConverters;
using FastReport.TypeEditors;

namespace FastReport.Map
{
  /// <summary>
  /// Specifies the position of a scale control inside the map.
  /// </summary>
  public enum ScaleDock
  {
    /// <summary>
    /// The scale is displayed at top left corner.
    /// </summary>
    TopLeft,

    /// <summary>
    /// The scale is displayed at top center side.
    /// </summary>
    TopCenter,
    
    /// <summary>
    /// The scale is displayed at top right corner.
    /// </summary>
    TopRight,

    /// <summary>
    /// The scale is displayed at middle left side.
    /// </summary>
    MiddleLeft,

    /// <summary>
    /// The scale is displayed at middle right side.
    /// </summary>
    MiddleRight,
    
    /// <summary>
    /// The scale is displayed at bottom left corner.
    /// </summary>
    BottomLeft,

    /// <summary>
    /// The scale is displayed at bottom center side.
    /// </summary>
    BottomCenter,

    /// <summary>
    /// The scale is displayed at bottom right corner.
    /// </summary>
    BottomRight
  }
  
  
  /// <summary>
  /// The base class for scale-type controls such as <see cref="DistanceScale"/> and <see cref="ColorScale"/>.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class ScaleBase
  {
    #region Fields
    private Border FBorder;
    private FillBase FFill;
    private Font FTitleFont;
    private Color FTitleColor;
    private string FTitleText;
    private Font FFont;
    private Color FTextColor;
    private Color FBorderColor;
    private ScaleDock FDock;
    private bool FVisible;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the border.
    /// </summary>
    public Border Border
    {
      get { return FBorder; }
      set 
      {
        if (value == null)
          throw new ArgumentNullException("Border");
        FBorder = value; 
      }
    }

    /// <summary>
    /// Gets or sets the fill.
    /// </summary>
    [Editor(typeof(FillEditor), typeof(UITypeEditor))]
    public FillBase Fill
    {
      get { return FFill; }
      set 
      {
        if (value == null)
          throw new ArgumentNullException("Fill");
        FFill = value; 
      }
    }

    /// <summary>
    /// Gets or sets the title font.
    /// </summary>
    [Category("Appearance")]
    public Font TitleFont
    {
      get { return FTitleFont; }
      set 
      {
        if (value == null)
          throw new ArgumentNullException("TitleFont");
        FTitleFont = value; 
      }
    }

    /// <summary>
    /// Gets or sets the title text color.
    /// </summary>
    public Color TitleColor
    {
      get { return FTitleColor; }
      set { FTitleColor = value; }
    }

    /// <summary>
    /// Gets or sets the title text.
    /// </summary>
    public string TitleText
    {
      get { return FTitleText; }
      set { FTitleText = value; }
    }

    /// <summary>
    /// Gets or sets the font.
    /// </summary>
    [Category("Appearance")]
    public Font Font
    {
      get { return FFont; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Font");
        FFont = value;
      }
    }

    /// <summary>
    /// Gets or sets the text color.
    /// </summary>
    public Color TextColor
    {
      get { return FTextColor; }
      set { FTextColor = value; }
    }

    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    public Color BorderColor
    {
      get { return FBorderColor; }
      set { FBorderColor = value; }
    }

    /// <summary>
    /// Gets or sets the location of the scale.
    /// </summary>
    public ScaleDock Dock
    {
      get { return FDock; }
      set { FDock = value; }
    }

    /// <summary>
    /// Gets or sets the visibility of a scale.
    /// </summary>
    [DefaultValue(true)]
    public bool Visible
    {
      get { return FVisible; }
      set { FVisible = value; }
    }
    #endregion // Properties

    #region Public Methods
    /// <summary>
    /// Copies the contents of another ScaleBase.
    /// </summary>
    /// <param name="src">The ScaleBase instance to copy the contents from.</param>
    public virtual void Assign(ScaleBase src)
    {
      Border = src.Border.Clone();
      Fill = src.Fill.Clone();
      TitleFont = src.TitleFont;
      TitleColor = src.TitleColor;
      TitleText = src.TitleText;
      Font = src.Font;
      TextColor = src.TextColor;
      BorderColor = src.BorderColor;
      Dock = src.Dock;
      Visible = src.Visible;
    }

    /// <summary>
    /// Serializes the scale.
    /// </summary>
    /// <param name="writer">Writer object.</param>
    /// <param name="prefix">Scale property name.</param>
    /// <param name="diff">Another ScaleBase to compare with.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public virtual void Serialize(FRWriter writer, string prefix, ScaleBase diff)
    {
      Border.Serialize(writer, prefix + ".Border", diff.Border);
      Fill.Serialize(writer, prefix + ".Fill", diff.Fill);
      if (!TitleFont.Equals(diff.TitleFont))
        writer.WriteValue(prefix + ".TitleFont", TitleFont);
      if (TitleColor != diff.TitleColor)
        writer.WriteValue(prefix + ".TitleColor", TitleColor);
      if (TitleText != diff.TitleText)
        writer.WriteStr(prefix + ".TitleText", TitleText);
      if (!Font.Equals(diff.Font))
        writer.WriteValue(prefix + ".Font", Font);
      if (TextColor != diff.TextColor)
        writer.WriteValue(prefix + ".TextColor", TextColor);
      if (BorderColor != diff.BorderColor)
        writer.WriteValue(prefix + ".BorderColor", BorderColor);
      if (Dock != diff.Dock)
        writer.WriteValue(prefix + ".Dock", Dock);
      if (Visible != diff.Visible)
        writer.WriteBool(prefix + ".Visible", Visible);
    }

    /// <summary>
    /// Gets the size of the scale, in pixels.
    /// </summary>
    /// <returns>The SizeF structure containing the size of the object.</returns>
    public virtual SizeF CalcSize()
    {
      SizeF size = new SizeF(100, 0);
      if (!String.IsNullOrEmpty(TitleText))
        size.Height += DrawUtils.MeasureString(TitleText, TitleFont).Height + 4;
      return size;
    }

    internal RectangleF GetDrawRect(MapObject parent)
    {
      float parentWidth = parent.Width - parent.Padding.Horizontal;
      float parentHeight = parent.Height - parent.Padding.Vertical;
      RectangleF drawRect = new RectangleF(
        new PointF(parent.AbsLeft + parent.Padding.Left, parent.AbsTop + parent.Padding.Top), 
        CalcSize());
      
      switch (Dock)
      {
        case ScaleDock.TopLeft:
          break;
        case ScaleDock.TopCenter:
          drawRect.Offset((parentWidth - drawRect.Width) / 2, 0);
          break;
        case ScaleDock.TopRight:
          drawRect.Offset(parentWidth - drawRect.Width, 0);
          break;
        case ScaleDock.MiddleLeft:
          drawRect.Offset(0, (parentHeight - drawRect.Height) / 2);
          break;
        case ScaleDock.MiddleRight:
          drawRect.Offset(parentWidth - drawRect.Width, (parentHeight - drawRect.Height) / 2);
          break;
        case ScaleDock.BottomLeft:
          drawRect.Offset(0, parentHeight - drawRect.Height);
          break;
        case ScaleDock.BottomCenter:
          drawRect.Offset((parentWidth - drawRect.Width) / 2, parentHeight - drawRect.Height);
          break;
        case ScaleDock.BottomRight:
          drawRect.Offset(parentWidth - drawRect.Width, parentHeight - drawRect.Height);
          break;
      }

      return drawRect;
    }

    /// <summary>
    /// Draws the object.
    /// </summary>
    /// <param name="e">Draw parameters.</param>
    /// <param name="parent">Parent map object.</param>
    public virtual void Draw(FRPaintEventArgs e, MapObject parent)
    {
      RectangleF drawRect = GetDrawRect(parent);
      Fill.Draw(e, drawRect);
      Border.Draw(e, drawRect);

      if (!String.IsNullOrEmpty(TitleText))
      {
        Brush textBrush = e.Cache.GetBrush(TitleColor);
        Font font = e.Cache.GetFont(TitleFont.Name,
          parent.IsPrinting ? TitleFont.Size : TitleFont.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi,
          TitleFont.Style);
        StringFormat format = e.Cache.GetStringFormat(StringAlignment.Center, StringAlignment.Near, 
          StringTrimming.EllipsisCharacter, StringFormatFlags.NoWrap, 0, 0);
        RectangleF textRect = new RectangleF(drawRect.Left * e.ScaleX, (drawRect.Top + 3) * e.ScaleY,
          drawRect.Width * e.ScaleX, drawRect.Height * e.ScaleY);
        e.Graphics.DrawString(TitleText, font, textBrush, textRect, format);
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ScaleBase"/> class.
    /// </summary>
    public ScaleBase()
    {
      FBorder = new Border();
      FBorder.Color = Color.Silver;
      FBorder.Lines = BorderLines.All;
      FFill = new SolidFill();
      (FFill as SolidFill).Color = Color.White;
      FBorderColor = Color.DarkGray;
      FTitleFont = DrawUtils.DefaultFont;
      FTitleColor = Color.Black;
      FTitleText = "";
      FFont = DrawUtils.DefaultFont;
      FTextColor = Color.Black;
      FVisible = true;
    }
  }
}
