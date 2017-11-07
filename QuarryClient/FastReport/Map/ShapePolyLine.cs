using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Map
{
  /// <summary>
  /// Represents a line shape.
  /// </summary>
  public class ShapePolyLine : ShapePolygon
  {
    #region Public Methods
    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (!IsVisible())
        return;

      float left = Map.AbsLeft + Map.OffsetXG;
      float top = Map.AbsTop + Map.OffsetYG;
      double minX = Layer.Box.MinX;
      double maxY = Layer.Box.MaxY;
      double w2 = Box.MinX + (Box.MaxX - Box.MinX) / 2;
      double h2 = Box.MinY + (Box.MaxY - Box.MinY) / 2;
      double addx = -w2 * ShapeScale + w2 + ShapeOffsetX;
      double addy = -h2 * ShapeScale + h2 + ShapeOffsetY;
      float scale = Map.ScaleG;
      ShapeStyle style = Layer.DefaultShapeStyle;

      using (GraphicsPath path = new GraphicsPath())
      {
        foreach (PointD[] part in Parts)
        {
          List<PointF> points = new List<PointF>();
          foreach (PointD point in part)
          {
            PointF pt = new PointF(
              (left + CoordinateConverter.GetX(point.X * ShapeScale + addx, minX, scale)) * e.ScaleX,
              (top + CoordinateConverter.GetY(point.Y * ShapeScale + addy, maxY, scale, Map.MercatorProjection)) * e.ScaleY);
            if (points.Count > 0 && DistanceBetweenPoints(pt, points[points.Count - 1]) < Layer.Accuracy)
              continue;
            points.Add(pt);
          }

          if (points.Count >= 2)
          {
            PointF[] poly = points.ToArray();
            path.AddLines(poly);
          }
        }

        Pen pen = e.Cache.GetPen(style.BorderColor, style.BorderWidth * e.ScaleX, style.BorderStyle, LineJoin.Bevel);
        e.Graphics.DrawPath(pen, path);
      }
    }

    /// <inheritdoc/>
    public override void DrawLabel(FRPaintEventArgs e)
    {
    }

    /// <inheritdoc/>
    public override bool HitTest(PointF point)
    {
      if (GetBBox().Contains(point))
        return true;
      return false;
    }
    #endregion // Public Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapePolyLine"/> class.
    /// </summary>
    public ShapePolyLine()
    {
      BaseName = "Line";
    }
  }
}
