using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FastReport.Utils;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace FastReport.Map
{
  /// <summary>
  /// Represents a map point.
  /// </summary>
  public class ShapePoint : ShapeBase
  {
    #region Fields
    private PointD FPoint;
    private RectangleF largestBoundsRect;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the X-coordinate of the point.
    /// </summary>
    public double X
    {
      get { return FPoint.X; }
      set { FPoint.X = value; }
    }

    /// <summary>
    /// Gets or sets the Y-coordinate of the point.
    /// </summary>
    public double Y
    {
      get { return FPoint.Y; }
      set { FPoint.Y = value; }
    }
    #endregion // Properties

    #region Private Methods
    private PointF GetPoint()
    {
      float left = Map.AbsLeft + Map.Padding.Left + Map.OffsetXG;
      float top = Map.AbsTop + Map.Padding.Top + Map.OffsetYG;
      double minX = Layer.Box.MinX;
      double maxY = Layer.Box.MaxY;
      float scale = Map.ScaleG;
      return new PointF(
        left + CoordinateConverter.GetX(FPoint.X + ShapeOffsetX, minX, scale),
        top + CoordinateConverter.GetY(FPoint.Y + ShapeOffsetY, maxY, scale, Map.MercatorProjection));
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      ShapePoint src = source as ShapePoint;
      X = src.X;
      Y = src.Y;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      PointF point = GetPoint();
      point.X *= e.ScaleX;
      point.Y *= e.ScaleY;
      
      ShapeStyle style = UseCustomStyle ? CustomStyle : Layer.DefaultShapeStyle;
      float size = style.PointSize;
      
      Pen pen = e.Cache.GetPen(style.BorderColor, 1, DashStyle.Solid);
      Brush brush = e.Cache.GetBrush(style.FillColor);

      // get color & size from the layer's color & size ranges
      if (!IsValueEmpty)
      {
        brush = e.Cache.GetBrush(Layer.ColorRanges.GetColor(Value));
        float rangeSize = Layer.SizeRanges.GetSize(Value);
        if (rangeSize != 0)
          size = rangeSize;
      }

      // display the selection in the designer
      Report report = Map.Report;
      if (report != null && report.IsDesigning && report.Designer != null &&
        report.Designer.SelectedObjects != null)
      {
        if (report.Designer.SelectedObjects.Contains(this))
          brush = e.Cache.GetBrush(Color.Orange);
      }

      // display the selection in the preview
      if (Map.HotPoint == this)
      {
        size *= 1.5f;
      }

      if (Map.Zoom >= Layer.LabelsVisibleAtZoom)
      {
          e.Graphics.FillEllipse(brush, point.X - size / 2, point.Y - size / 2, size, size);
          e.Graphics.DrawEllipse(pen, point.X - size / 2, point.Y - size / 2, size, size);
      }
      largestBoundsRect = new RectangleF(point.X - 100, point.Y - 20, 200, 20);
    }

    /// <inheritdoc/>
    public override void DrawLabel(FRPaintEventArgs e)
    {
      ShapeStyle style = UseCustomStyle ? CustomStyle : Layer.DefaultShapeStyle;
      string text = "";
      if (Layer.LabelKind != MapLabelKind.Value)
        text = SpatialData.GetValue(Layer.LabelColumn);
      if (!IsValueEmpty)
      {
        if (Layer.LabelKind == MapLabelKind.NameAndValue || Layer.LabelKind == MapLabelKind.Value)
          text += "\r\n" + Value.ToString(Layer.LabelFormat);
      }

      Font font = e.Cache.GetFont(style.Font.Name,
        IsPrinting ? style.Font.Size : style.Font.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi,
        style.Font.Style);
      RectangleF textBounds = largestBoundsRect;
      textBounds.Offset(CenterOffsetX * e.ScaleX, CenterOffsetY * e.ScaleY);
      StringFormat format = e.Cache.GetStringFormat(StringAlignment.Center, StringAlignment.Center, StringTrimming.None, StringFormatFlags.NoClip, 0, 0);
      SolidBrush brush = e.Cache.GetBrush(style.TextColor);
      e.Graphics.DrawString(text, font, brush, textBounds, format);
    }

    internal void Load(Stream stream)
    {
      FPoint.Load(stream);
    }

    internal void Save(Stream stream)
    {
      FPoint.Save(stream);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ShapePoint c = writer.DiffObject as ShapePoint;
      base.Serialize(writer);

      if (X != c.X)
        writer.WriteDouble("X", X);
      if (Y != c.Y)
        writer.WriteDouble("Y", Y);
    }

    /// <inheritdoc/>
    public override bool HitTest(PointF point)
    {
      PointF pt = GetPoint();
      RectangleF bounds = new RectangleF(pt.X - 16, pt.Y - 16, 16, 16);
      if (bounds.Contains(point))
        return true;
      return false;
    }
    #endregion // Public Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapePoint"/> class.
    /// </summary>
    public ShapePoint()
    {
      FPoint = new PointD();
      BaseName = "Point";
    }
  }
}
