using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;
using System.IO;

namespace FastReport.Map
{
  /// <summary>
  /// Represents a polygon shape.
  /// </summary>
  public class ShapePolygon : ShapeBase
  {
    #region Fields
    private BoundingBox FBox;
    private List<PointD[]> FParts;
    private int FBlobIndex;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Holds the largest bounding rectangle of this shape.
    /// </summary>
    internal protected RectangleF largestBoundsRect;

    /// <summary>
    /// Gets or sets a bounding box of this shape.
    /// </summary>
    [Browsable(false)]
    public BoundingBox Box
    {
      get { return FBox; }
      set { FBox = value; }
    }

    /// <summary>
    /// Gets or sets a list of polygons in this shape.
    /// </summary>
    [Browsable(false)]
    public List<PointD[]> Parts
    {
      get { return FParts; }
      set { FParts = value; }
    }

    /// <summary>
    /// Gets or sets the shape data in binary format.
    /// </summary>
    [Browsable(false)]
    public byte[] ShapeData
    {
      get
      {
        using (MemoryStream stream = new MemoryStream())
        {
          Save(stream);
          return stream.ToArray();
        }
      }
      set
      {
        using (MemoryStream stream = new MemoryStream(value))
        {
          Load(stream);
        }
      }
    }
    #endregion // Properties

    #region Private Methods
    private void ResetBlobIndex()
    {
      FBlobIndex = -1;
    }
    #endregion

    #region Public Methods
    internal RectangleF GetBBox()
    {
      double minX = Layer.Box.MinX;
      double maxY = Layer.Box.MaxY;
      float scale = Map.ScaleG;
      double w2 = Box.MinX + (Box.MaxX - Box.MinX) / 2;
      double h2 = Box.MinY + (Box.MaxY - Box.MinY) / 2;
      double addx = -w2 * ShapeScale + w2 + ShapeOffsetX;
      double addy = -h2 * ShapeScale + h2 + ShapeOffsetY;

      float left = CoordinateConverter.GetX(Box.MinX * ShapeScale + addx, minX, scale);
      float top = CoordinateConverter.GetY(Box.MaxY * ShapeScale + addy, maxY, scale, Map.MercatorProjection);
      float right = CoordinateConverter.GetX(Box.MaxX * ShapeScale + addx, minX, scale);
      float bottom = CoordinateConverter.GetY(Box.MinY * ShapeScale + addy, maxY, scale, Map.MercatorProjection);

      return new RectangleF(
        Map.AbsLeft + Map.Padding.Left + Map.OffsetXG + left,
        Map.AbsTop + Map.Padding.Top + Map.OffsetYG + top, 
        right - left, 
        bottom - top);
    }

    internal bool IsVisible()
    {
      RectangleF polygonRect = GetBBox();
      if (polygonRect.Right < Map.AbsLeft || polygonRect.Bottom < Map.AbsTop ||
        polygonRect.Left > Map.AbsRight || polygonRect.Top > Map.AbsBottom)
        return false;
      return true;
    }

    internal double DistanceBetweenPoints(PointF pt1, PointF pt2)
    {
      return Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
    }

    internal double DistanceBetweenPoints(PointD pt1, PointD pt2)
    {
      return Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
    }

    internal GraphicsPath GetGraphicsPath(FRPaintEventArgs e)
    {
      float left = Map.AbsLeft + Map.Padding.Left + Map.OffsetXG;
      float top = Map.AbsTop + Map.Padding.Top + Map.OffsetYG;
      double minX = Layer.Box.MinX;
      double maxY = Layer.Box.MaxY;
      double w2 = Box.MinX + (Box.MaxX - Box.MinX) / 2;
      double h2 = Box.MinY + (Box.MaxY - Box.MinY) / 2;
      double addx = -w2 * ShapeScale + w2 + ShapeOffsetX;
      double addy = -h2 * ShapeScale + h2 + ShapeOffsetY;
      float scale = Map.ScaleG;

      largestBoundsRect = new RectangleF();
      GraphicsPath path = new GraphicsPath();
      
      foreach (PointD[] part in Parts)
      {
        float polyLeft = 1e6f;
        float polyTop = 1e6f;
        float polyRight = -1e6f;
        float polyBottom = -1e6f;
        List<PointF> points = new List<PointF>();
        
        foreach (PointD point in part)
        {
          PointF pt = new PointF(
            (left + CoordinateConverter.GetX(point.X * ShapeScale + addx, minX, scale)) * e.ScaleX,
            (top + CoordinateConverter.GetY(point.Y * ShapeScale + addy, maxY, scale, Map.MercatorProjection)) * e.ScaleY);
          if (points.Count > 0 && DistanceBetweenPoints(pt, points[points.Count - 1]) < Layer.Accuracy)
            continue;
          points.Add(pt);

          if (pt.X < polyLeft)
            polyLeft = pt.X;
          if (pt.X > polyRight)
            polyRight = pt.X;
          if (pt.Y < polyTop)
            polyTop = pt.Y;
          if (pt.Y > polyBottom)
            polyBottom = pt.Y;
        }

        if (points.Count > 2)
        {
          path.AddPolygon(points.ToArray());

          if (polyRight - polyLeft > largestBoundsRect.Width)
            largestBoundsRect = new RectangleF(polyLeft, polyTop, polyRight - polyLeft, polyBottom - polyTop);
        }
      }

      return path;
    }

    internal void Load(Stream stream)
    {
      Box.Load(stream);

      byte[] buffer4 = new byte[4];
      stream.Read(buffer4, 0, buffer4.Length);
      int numParts = BitConverter.ToInt32(buffer4, 0);

      stream.Read(buffer4, 0, buffer4.Length);
      int numPoints = BitConverter.ToInt32(buffer4, 0);

      int[] parts = new int[numParts + 1];
      for (int i = 0; i < numParts; i++)
      {
        stream.Read(buffer4, 0, buffer4.Length);
        parts[i] = BitConverter.ToInt32(buffer4, 0);
      }
      parts[numParts] = numPoints;

      Parts.Clear();
      for (int i = 0; i < numParts; i++)
      {
        int pointsInPart = parts[i + 1] - parts[i];
        PointD[] part = new PointD[pointsInPart];
        Parts.Add(part);
        for (int j = 0; j < pointsInPart; j++)
        {
          PointD point = new PointD();
          point.Load(stream);
          part[j] = point;
        }
      }
    }

    internal void Save(Stream stream)
    {
      Box.Save(stream);

      int numParts = Parts.Count;
      byte[] buffer4 = BitConverter.GetBytes(numParts);
      stream.Write(buffer4, 0, buffer4.Length);

      int numPoints = 0;
      foreach (PointD[] part in Parts)
      {
        numPoints += part.Length;
      }
      buffer4 = BitConverter.GetBytes(numPoints);
      stream.Write(buffer4, 0, buffer4.Length);

      numPoints = 0;
      foreach (PointD[] part in Parts)
      {
        buffer4 = BitConverter.GetBytes(numPoints);
        stream.Write(buffer4, 0, buffer4.Length);
        numPoints += part.Length;
      }

      foreach (PointD[] part in Parts)
      {
        foreach (PointD point in part)
        {
          point.Save(stream);
        }
      }
    }

    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);

      ShapePolygon src = source as ShapePolygon;
      Box.Assign(src.Box);
      ShapeData = src.ShapeData;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (!IsVisible())
        return;

      ShapeStyle style = UseCustomStyle ? CustomStyle : Layer.DefaultShapeStyle;
      Pen pen = e.Cache.GetPen(style.BorderColor, style.BorderWidth * e.ScaleX, style.BorderStyle, LineJoin.Bevel);
      Color fillColor = style.FillColor;
      if (Layer.Palette != MapPalette.None && !UseCustomStyle)
        fillColor = ColorPalette.GetColor(ShapeIndex, Layer.Palette);
      Brush brush = e.Cache.GetBrush(fillColor);
      bool disposeBrush = false;

      // get color from the layer's color range
      if (!IsValueEmpty && Layer.ColorRanges.RangeCount > 0)
        brush = e.Cache.GetBrush(Layer.ColorRanges.GetColor(Value));

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
        brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.White, (brush as SolidBrush).Color);
        disposeBrush = true;
      }

      using (GraphicsPath path = GetGraphicsPath(e))
      {
        if (path.PointCount > 0)
        {
          e.Graphics.FillPath(brush, path);
          e.Graphics.DrawPath(pen, path);
        }
      }

      if (disposeBrush)
        brush.Dispose();
    }

    /// <inheritdoc/>
    public override void DrawLabel(FRPaintEventArgs e)
    {
      if (!IsVisible())
        return;

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
      float width = e.Graphics.MeasureString(text, font).Width;
      if (width < largestBoundsRect.Width)
      {
        RectangleF textBounds = largestBoundsRect;
        textBounds.Offset(CenterOffsetX * e.ScaleX, CenterOffsetY * e.ScaleY);
        StringFormat format = e.Cache.GetStringFormat(StringAlignment.Center, StringAlignment.Center, StringTrimming.None, StringFormatFlags.NoClip, 0, 0);
        SolidBrush brush = e.Cache.GetBrush(style.TextColor);
        e.Graphics.DrawString(text, font, brush, textBounds, format);
      }
    }

    /// <inheritdoc/>
    public override bool HitTest(PointF point)
    {
      // first check BBox, it's fast
      if (GetBBox().Contains(point))
      {
        // check graphics path
        GraphicsPath path = GetGraphicsPath(new FRPaintEventArgs(null, 1, 1, null));
        if (path != null)
        {
          bool ok = path.IsVisible(point);
          path.Dispose();
          if (ok)
            return true;
        }
      }
      return false;
    }

    /// <inheritdoc/>
    public override void Simplify(double accuracy)
    {
      for (int i = 0; i < Parts.Count; i++)
      {
        PointD[] part = Parts[i];
        List<PointD> points = new List<PointD>();

        foreach (PointD point in part)
        {
          if (points.Count > 0 && DistanceBetweenPoints(point, points[points.Count - 1]) < accuracy)
            continue;
          points.Add(point);
        }

        if (points.Count > 2)
          Parts[i] = points.ToArray();
        else
        {
          Parts.Remove(part);
          i--;
        }
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      base.Serialize(writer);

      if (writer.SerializeTo != SerializeTo.SourcePages)
      {
        if (writer.BlobStore != null)
        {
          if (FBlobIndex == -1 || FBlobIndex >= writer.BlobStore.Count)
            FBlobIndex = writer.BlobStore.Add(ShapeData);
          writer.WriteInt("BlobIndex", FBlobIndex);
        }
        else
        {
          if (Parts.Count > 0)
            writer.WriteValue("ShapeData", ShapeData);
        }
      }
    }

    /// <inheritdoc/>
    public override void Deserialize(FRReader reader)
    {
      base.Deserialize(reader);
      if (reader.HasProperty("BlobIndex"))
      {
        FBlobIndex = reader.ReadInt("BlobIndex");
        if (reader.BlobStore != null && FBlobIndex != -1)
          ShapeData = reader.BlobStore.Get(FBlobIndex);
      }
    }

    /// <inheritdoc/>
    public override void InitializeComponent()
    {
      ResetBlobIndex();
    }

    /// <inheritdoc/>
    public override void FinalizeComponent()
    {
      ResetBlobIndex();
    }
    #endregion // Public Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapePolygon"/> class.
    /// </summary>
    public ShapePolygon()
    {
      FBox = new BoundingBox();
      FParts = new List<PointD[]>();
      FBlobIndex = -1;
      BaseName = "Polygon";
    }
  }
}
