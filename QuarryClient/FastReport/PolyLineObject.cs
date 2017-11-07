using FastReport.TypeConverters;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace FastReport
{
    /// <summary>
    /// Represents a poly line object.
    /// </summary>
    /// <remarks>
    /// Use the <b>Border.Width</b>, <b>Border.Style</b> and <b>Border.Color</b> properties to set
    /// the line width, style and color.
    /// </remarks>
    public class PolyLineObject : ReportComponentBase, IHasEditor
    {
        #region Fields

        internal PolygonSelectionMode polygonSelectionMode;
        private PointF center;
        private int currentPoint;
        private List<PointF> points;
        private List<byte> pointTypes;

        //Current selected point, for move
        private PolyLineObjectToolBar toolbar;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Returns origin of coordinates relative to the top left corner
        /// </summary>
        [Browsable(false)]
        public float CenterX { get { return center.X; } set { center.X = value; } }

        /// <summary>
        /// Returns origin of coordinates relative to the top left corner
        /// </summary>
        [Browsable(false)]
        public float CenterY { get { return center.Y; } set { center.Y = value; } }

        /// <inheritdoc/>
        [TypeConverter(typeof(UnitsConverter))]
        public override float Height
        {
            get { return base.Height; }
            set
            {
                if (base.Height == 0)
                {
                    base.Height = value;
                    return;
                }
                if (IsSelected)
                {
                    if (polygonSelectionMode == PolygonSelectionMode.Scale)
                    {
                        float oldHeight = base.Height;
                        if (oldHeight == value) return;
                        if (points == null || points.Count < 2) return;
                        float scaleY = value / oldHeight;
                        if (float.IsInfinity(scaleY)) return;
                        if (float.IsNaN(scaleY)) return;
                        if (scaleY == 0) return;
                        center.Y = center.Y * scaleY;
                        for (int i = 0; i < points.Count; i++)
                        {
                            PointF p = points[i];
                            p.Y = p.Y * scaleY;
                            points[i] = p;
                        }
                        base.Height = value;
                    }
                    else
                        recalculateBounds();
                }
                else
                    base.Height = value;
            }
        }

        /// <summary>
        /// Return points array of line
        /// </summary>
        [Browsable(false)]
        public PointF[] PointsArray
        {
            get
            {
                if (points == null || points.Count == 0) return new PointF[0];
                return points.ToArray();
            }
        }

        /// <summary>
        /// Return point types array. 0 - Start of line, 1 - Keep on line
        /// </summary>
        [Browsable(false)]
        public byte[] PointTypesArray
        {
            get
            {
                if (pointTypes == null || pointTypes.Count == 0) return new byte[0];
                return pointTypes.ToArray();
            }
        }

        /// <inheritdoc/>
        [TypeConverter(typeof(UnitsConverter))]
        public override float Width
        {
            get { return base.Width; }
            set
            {
                if (base.Width == 0)
                {
                    base.Width = value;
                    return;
                }
                if (IsSelected)
                {
                    if (polygonSelectionMode == PolygonSelectionMode.Scale)
                    {
                        float oldWidth = base.Width;
                        if (oldWidth == value) return;
                        if (points == null || points.Count < 2) return;
                        float scaleX = value / oldWidth;
                        if (float.IsInfinity(scaleX)) return;
                        if (float.IsNaN(scaleX)) return;
                        if (scaleX == 0) return;
                        center.X = center.X * scaleX;
                        for (int i = 0; i < points.Count; i++)
                        {
                            PointF p = points[i];
                            p.X = p.X * scaleX;
                            points[i] = p;
                        }
                        base.Width = value;
                    }
                    else
                        recalculateBounds();
                }
                else
                    base.Width = value;
            }
        }

        #endregion Properties

        #region Private Methods

        private float getDistance(float px, float py, float px0, float py0, float px1, float py1, out int index)
        {
            float vx = px1 - px0;
            float vy = py1 - py0;
            float wx = px - px0;
            float wy = py - py0;
            float c1 = vx * wx + vy * wy;
            if (c1 <= 0) { index = -1; return (px0 - px) * (px0 - px) + (py0 - py) * (py0 - py); }
            float c2 = vx * vx + vy * vy;
            if (c2 <= c1) { index = 1; return (px1 - px) * (px1 - px) + (py1 - py) * (py1 - py); }
            float b = c1 / c2;
            index = 0;
            float bx = px0 + vx * b;
            float by = py0 + vy * b;
            return (bx - px) * (bx - px) + (by - py) * (by - py);
        }

        #endregion Private Methods

        #region Protected Methods

        /// <summary>
        /// Add point to end of polyline, need to recalculate bounds after add
        /// First point must have zero coordinate and zero type
        /// </summary>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        /// <param name="pointType">0-start,1-line</param>
        protected void addPoint(float localX, float localY, byte pointType)
        {
            if (points == null)
            {
                points = new List<PointF>();
                points.Add(new PointF(localX, localY));
            }
            else
            {
                points.Add(new PointF(localX, localY));
            }
            if (pointTypes == null)
            {
                pointTypes = new List<byte>();
                pointTypes.Add((byte)PathPointType.Start);
            }
            else
            {
                pointTypes.Add(pointType);
            }
        }

        /// <summary>
        /// Delete point from polyline by index
        /// Recalculate bounds
        /// </summary>
        /// <param name="index">Index of point in polyline</param>
        protected void deletePoint(int index)
        {
            if (points == null || points.Count == 0)
                return;
            if (pointTypes[index] == 0 && index < pointTypes.Count - 1)
            {
                pointTypes[index + 1] = 0;
            }
            if (points.Count > 1)
            {
                points.RemoveAt(index);
                pointTypes.RemoveAt(index);
                recalculateBounds();
            }
        }

        /// <summary>
        /// Draw polyline path to graphics
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void drawPoly(FRPaintEventArgs e)
        {
            Pen pen = e.Cache.GetPen(Border.Color, Border.Width * e.ScaleX, Border.DashStyle);
            using (GraphicsPath path = getPath(pen, e.ScaleX, e.ScaleY))
                e.Graphics.DrawPath(pen, path);
        }

        /// <summary>
        /// Calculate GraphicsPath for draw to page
        /// </summary>
        /// <param name="pen">Pen for lines</param>
        /// <param name="scaleX">scale by width</param>
        /// <param name="scaleY">scale by height</param>
        /// <returns>Always returns a non-empty path</returns>
        protected GraphicsPath getPath(Pen pen, float scaleX, float scaleY)
        {
            if (points == null)
            {
                GraphicsPath result = new GraphicsPath();
                result.AddLine(AbsLeft, AbsTop, AbsRight + 1, AbsBottom + 1);
                return result;
            }
            GraphicsPath gp = null;
            if (points.Count > 1)
            {
                PointF[] aPoints = new PointF[points.Count];
                int i = 0;
                foreach (PointF point in points)
                {
                    aPoints[i] = new PointF((point.X + AbsLeft + center.X) * scaleX, (point.Y + AbsTop + center.Y) * scaleY);
                    i++;
                }
                gp = new GraphicsPath(aPoints, pointTypes.ToArray());
            }
            else gp = new GraphicsPath();
            return gp;
        }

        /// <inheritdoc/>
        protected override SelectionPoint[] GetSelectionPoints()
        {
            List<SelectionPoint> selectionPoints = new List<SelectionPoint>();
            if (points == null)
            {
                return new SelectionPoint[] {
                new SelectionPoint(AbsLeft, AbsTop,SizingPoint.RightBottom),
                new SelectionPoint(AbsRight,AbsBottom,SizingPoint.RightBottom)
                };
            }
            if (polygonSelectionMode == PolygonSelectionMode.Scale && points.Count > 1)
            {
                return base.GetSelectionPoints();
            }
            foreach (PointF point in points)
            {
                selectionPoints.Add(new SelectionPoint(point.X + AbsLeft + center.X, point.Y + AbsTop + center.Y, SizingPoint.RightBottom));
            }

            return selectionPoints.ToArray();
        }

        /// <summary>
        /// Insert point to desired place of polyline
        /// </summary>
        /// <param name="index">Index of place from zero to count</param>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        /// <param name="pointType">0-start,1-line</param>
        protected void insertPoint(int index, float localX, float localY, byte pointType)
        {
            if (points == null || points.Count == 0)
            {
                addPoint(localX, localY, pointType);
                return;
            }

            if (pointTypes.Count > index && pointTypes[index] == 0) { pointTypes[index] = 1; pointType = 0; }
            points.Insert(index, new PointF(localX, localY));
            pointTypes.Insert(index, pointType);
            //recalculateBounds();
        }

        /// <summary>
        /// Recalculate position and size of element
        /// </summary>
        protected void recalculateBounds()
        {
            float left = points[0].X;
            float top = points[0].Y;
            float right = points[0].X;
            float bottom = points[0].Y;

            foreach (PointF point in points)
            {
                if (point.X < left)
                    left = point.X;
                else if (point.X > right)
                    right = point.X;
                if (point.Y < top)
                    top = point.Y;
                else if (point.Y > bottom)
                    bottom = point.Y;
            }
            if (Math.Abs(right - left) == 0)
            {
                right += 1;
            }
            if (Math.Abs(bottom - top) == 0)
            {
                bottom += 1;
            }
            Left += left + center.X;
            center.X = -left;
            base.Width = Math.Abs(right - left);
            Top += top + center.Y;
            center.Y = -top;
            base.Height = Math.Abs(bottom - top);
        }

        #endregion Protected Methods

        #region Public Methods

        /// <summary>
        /// Add point to end of polyline and recalculate bounds after add
        /// Can be first point
        /// </summary>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        public void AddPointToEnd(float localX, float localY)
        {
            byte pointType = (byte)(points == null ? 0 : 1);
            if (points == null) addPoint(localX, localY, pointType);
            else insertPoint(points.Count, localX, localY, pointType);
            recalculateBounds();
            if (Report != null)
                if (Report.Designer != null)
                    Report.Designer.SetModified(this, "Change");
        }

        /// <summary>
        /// Add point to start of polyline and recalculate bounds after add
        /// Can be first point
        /// </summary>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        public void AddPointToStart(float localX, float localY)
        {
            byte pointType = (byte)(points == null ? 0 : 0);
            if (points == null) addPoint(localX, localY, pointType);
            else insertPoint(0, localX, localY, pointType);
            recalculateBounds();
            if (Report != null)
                if (Report.Designer != null)
                    Report.Designer.SetModified(this, "Change");
        }

        /// <inheritdoc/>
        public override void Assign(Base source)
        {
            base.Assign(source);

            PolyLineObject src = source as PolyLineObject;
            if (src.points == null) return;
            if (src.pointTypes == null) return;
            points = new List<PointF>(src.points);
            pointTypes = new List<byte>(src.pointTypes);
            center = src.center;
            //recalculateBounds();
        }

        /// <inheritdoc/>
        public override void CheckNegativeSize(FRMouseEventArgs e)
        {
            // do nothing
        }

        /// <inheritdoc/>
        public override void Deserialize(FRReader reader)
        {
            base.Deserialize(reader);
            points = new List<PointF>();
            pointTypes = new List<byte>();
            string polyPoints = reader.ReadStr("PolyPoints");
            foreach (string str in polyPoints.Split('|'))
            {
                string[] point = str.Split('\\');
                if (point.Length == 3)
                {
                    float f1 = float.Parse(point[0].Replace(',', '.'), CultureInfo.InvariantCulture);
                    float f2 = float.Parse(point[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    addPoint(f1, f2, byte.Parse(point[2]));
                }
            }
            if (reader.HasProperty("CenterX"))
                center.X = reader.ReadFloat("CenterX");
            if (reader.HasProperty("CenterY"))
                center.Y = reader.ReadFloat("CenterY");
            //recalculateBounds();
        }

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // draw marker when inserting a line
            if ((points == null || points.Count == 1))
            {
                g.DrawLine(Pens.Black, AbsLeft * e.ScaleX - 6, AbsTop * e.ScaleY, AbsLeft * e.ScaleX + 6, AbsTop * e.ScaleY);
                g.DrawLine(Pens.Black, AbsLeft * e.ScaleX, AbsTop * e.ScaleY - 6, AbsLeft * e.ScaleX, AbsTop * e.ScaleY + 6);
            }
            else
            {
                //g.DrawString(number.ToString(), new Font("Arual", 10), Brushes.Black, AbsLeft * e.ScaleX - 6, AbsTop * e.ScaleY);

                Report report = Report;
                if (report != null && report.SmoothGraphics)
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                }

                drawPoly(e);

                if (report != null && report.SmoothGraphics)
                {
                    g.InterpolationMode = InterpolationMode.Default;
                    g.SmoothingMode = SmoothingMode.Default;
                }

                if (IsSelected && polygonSelectionMode == PolygonSelectionMode.Scale)
                {
                    Pen pen = e.Cache.GetPen(Color.Gray, 1, DashStyle.Dot);
                    g.DrawRectangle(pen, AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY);
                }
            }

            if (IsDesigning && IsSelected)
            {
              if (currentPoint >= 0)
                {
                    toolbar.Visible = false;
                    return;
                }
                try
                {
                    toolbar.Visible = Report.Designer.SelectedObjects.Count == 1;
                }
                catch { toolbar.Visible = false; }
              toolbar.Draw(this, g, new PointF(AbsLeft, AbsTop < 20 ? 20 : AbsTop), e.ScaleX, e.ScaleY);
            }
        }

        /// <inheritdoc/>
        public override SizeF GetPreferredSize()
        {
            return new SizeF(0, 0);
        }

        /// <inheritdoc/>
        public override void HandleMouseDown(FRMouseEventArgs e)
        {
          if (toolbar.Visible)
          {
            int click = toolbar.Click(new PointF(AbsLeft, AbsTop < 20 ? 20 : AbsTop), new PointF(e.X, e.Y));
            if (click != -1)
            {
              switch (click)
              {
                case 0:
                  if (polygonSelectionMode == PolygonSelectionMode.Normal)
                    polygonSelectionMode = PolygonSelectionMode.Scale;
                  else
                    polygonSelectionMode = PolygonSelectionMode.Normal;
                  break;

                case 1:
                  polygonSelectionMode = PolygonSelectionMode.AddToStart;
                  break;

                case 2:
                  polygonSelectionMode = PolygonSelectionMode.AddToEnd;
                  break;

                case 3:
                  polygonSelectionMode = PolygonSelectionMode.AddToLine;
                  break;

                case 4:
                  polygonSelectionMode = PolygonSelectionMode.Delete;
                  break;
              }
              e.Handled = true;
              return;
            }
          }

          if (e.Button == MouseButtons.Left && currentPoint < 0 && this.IsSelected)
          {
            PointF mousePoint = new PointF(e.X, e.Y);
            PointF mousePointAligned = new PointF((int)((e.X - AbsLeft - center.X) / Page.SnapSize.Width) * Page.SnapSize.Width, (int)((e.Y - AbsTop - center.Y) / Page.SnapSize.Height) * Page.SnapSize.Height);
            switch (polygonSelectionMode)
            {
              case PolygonSelectionMode.Normal:
                for (int i = 0; i < points.Count; i++)
                {
                  if (PointInSelectionPoint(AbsLeft + points[i].X + center.X, AbsTop + points[i].Y + center.Y, mousePoint))
                  {
                    currentPoint = i;
                    e.Mode = WorkspaceMode2.Custom;
                    e.Handled = true;
                    break;
                  }
                }
                break;

              case PolygonSelectionMode.AddToStart:
                AddPointToStart(mousePointAligned.X, mousePointAligned.Y);
                currentPoint = 0;
                e.Mode = WorkspaceMode2.Custom;
                e.Handled = true;
                break;

              case PolygonSelectionMode.AddToEnd:
                if (points != null)
                  currentPoint = points.Count;
                AddPointToEnd(mousePointAligned.X, mousePointAligned.Y);
                e.Mode = WorkspaceMode2.Custom;
                e.Handled = true;
                break;

              case PolygonSelectionMode.AddToLine:
                e.Handled = true;
                int result = InsertPointByLocation(mousePointAligned.X, mousePointAligned.Y, 25);
                if (result >= 0)
                {
                  e.Mode = WorkspaceMode2.Custom;
                  currentPoint = result;
                }
                break;

              case PolygonSelectionMode.Delete:
                for (int i = 0; i < points.Count; i++)
                {
                  if (PointInSelectionPoint(AbsLeft + points[i].X + center.X, AbsTop + points[i].Y + center.Y, mousePoint))
                  {
                    currentPoint = i;
                    deletePoint(currentPoint);
                    currentPoint = -1;
                    e.Mode = WorkspaceMode2.None;
                    e.Handled = true;
                    return;
                  }
                }
                break;

              case PolygonSelectionMode.Scale:
                if (points.Count < 2)
                {
                  for (int i = 0; i < points.Count; i++)
                  {
                    if (PointInSelectionPoint(AbsLeft + points[i].X + center.X, AbsTop + points[i].Y + center.Y, mousePoint))
                    {
                      currentPoint = i;
                      e.Mode = WorkspaceMode2.Custom;
                      e.Handled = true;
                      break;
                    }
                  }
                }
                break;
            }
          }

          if (!e.Handled)
            base.HandleMouseDown(e);
        }

        /// <inheritdoc/>
        public override void HandleMouseMove(FRMouseEventArgs e)
        {
            if (currentPoint >= 0)
            {
                PointF point = points[currentPoint];
                point.X += e.Delta.X;
                point.Y += e.Delta.Y;
                points[currentPoint] = point;
                recalculateBounds();
                e.Mode = WorkspaceMode2.Custom;
                e.Handled = true;
            }

            if (!e.Handled)
            {
                base.HandleMouseMove(e);
            }

            if (e.Handled)
            {
                e.Cursor = Cursors.Cross;
            }

            if (IsSelected)
            {
                switch (polygonSelectionMode)
                {
                    case PolygonSelectionMode.AddToEnd:
                    case PolygonSelectionMode.AddToLine:
                    case PolygonSelectionMode.AddToStart:
                        e.Cursor = Cursors.Cross;
                        e.Handled = true;
                        e.Mode = WorkspaceMode2.Custom;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override void HandleMouseUp(FRMouseEventArgs e)
        {
            if (points == null)
            {
                addPoint(0, 0, 0);
                e.Handled = true;
                base.Width = 1;
                base.Height = 1;
                recalculateBounds();
            }

            if (currentPoint >= 0)
            {
                recalculateBounds();
                currentPoint = -1;
            }

            if (IsSelected)
            {
                int mode = (int)polygonSelectionMode;
                if (0 < mode)
                {
                    e.Handled = true;
                }
            }
            if (!e.Handled)
                base.HandleMouseUp(e);
        }

        /// <summary>
        /// Insert point to desired place of polyline
        /// Recalculate bounds after insert
        /// </summary>
        /// <param name="index">Index of place from zero to count</param>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        public void InsertPointByIndex(int index, float localX, float localY)
        {
            byte pointType = (byte)(points == null ? 0 : 1);
            insertPoint(index, localX, localY, pointType);
            if (Report != null)
                if (Report.Designer != null)
                    Report.Designer.SetModified(this, "Change");
        }

        /// <summary>
        /// Insert point to near line
        /// Recalculate bounds after insert
        /// </summary>
        /// <param name="localX">local x - relative to left-top point</param>
        /// <param name="localY">local y - relative to left-top point</param>
        /// <param name="maxDistance">radius for find near point to polyline</param>
        /// <returns>Index of inserted point</returns>
        public int InsertPointByLocation(float localX, float localY, float maxDistance)
        {
            byte pointType = (byte)(points == null ? 0 : 1);
            int result = -1;
            if (points == null || points.Count == 0)
            {
                addPoint(0, 0, 0);
                recalculateBounds();
                if (Report != null)
                    if (Report.Designer != null)
                        Report.Designer.SetModified(this, "Change");
                return 0;
            }
            if (points.Count > 1)
            {
                float nearDistance = maxDistance;//this is max of find value
                float distance;
                int indexOfNear = -1;
                for (int i = 0; i < points.Count - 1; i++)
                {
                    float x2 = points[i].X;
                    float y2 = points[i].Y;
                    float x3 = points[i + 1].X;
                    float y3 = points[i + 1].Y;
                    int indexShift = 0;
                    distance = getDistance(localX, localY, x2, y2, x3, y3, out indexShift);
                    if (distance < nearDistance)
                    {
                        nearDistance = distance;
                        indexOfNear = i + indexShift + 1;
                    }
                }
                if (indexOfNear == -1)
                {
                    if ((localX - points[0].X) * (localX - points[0].X)
                        +
                        (localY - points[0].Y) * (localY - points[0].Y)
                        <
                        (localX - points[points.Count - 1].X) * (localX - points[points.Count - 1].X)
                        +
                        (localY - points[points.Count - 1].Y) * (localY - points[points.Count - 1].Y))
                        indexOfNear = 0;
                    else
                        indexOfNear = points.Count;
                }
                insertPoint(indexOfNear, localX, localY, 1);
                recalculateBounds();
                result = indexOfNear;
                if (Report != null)
                    if (Report.Designer != null)
                        Report.Designer.SetModified(this, "Change");
                return result;
            }
            else if (points.Count == 1)
            {
                insertPoint(1, localX, localY, 1);
                recalculateBounds();
                result = 1;
                if (Report != null)
                    if (Report.Designer != null)
                        Report.Designer.SetModified(this, "Change");
                return result;
            }
            if (result >= 0)
                if (Report != null)
                    if (Report.Designer != null)
                        Report.Designer.SetModified(this, "Change");
            return result;
        }

        /// <inheritdoc/>
        public bool InvokeEditor()
        {
            if (IsSelected)
            {
                if (polygonSelectionMode == PolygonSelectionMode.Scale)
                {
                    Report report = Report;
                    if (report != null)
                    {
                        polygonSelectionMode = PolygonSelectionMode.Normal;
                        report.Designer.SelectionChanged(this);
                    }
                    return true;
                }
                else if (polygonSelectionMode == PolygonSelectionMode.Normal)
                {
                    Report report = Report;
                    if (report != null)
                    {
                        polygonSelectionMode = PolygonSelectionMode.Scale;
                        report.Designer.SelectionChanged(this);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <inheritdoc/>
        public override void OnAfterInsert(InsertFrom source)
        {
            base.OnAfterInsert(source);
            if (Report != null)
            {
                SelectedObjectCollection selectionObjects = Report.Designer.SelectedObjects;
                selectionObjects.Clear();
                selectionObjects.Add(this);
            }
            if (points != null && points.Count < 2)
                polygonSelectionMode = PolygonSelectionMode.AddToEnd;
            if (points != null && points.Count > 1)
                polygonSelectionMode = PolygonSelectionMode.Scale;
        }

        /// <inheritdoc/>
        public override void OnBeforeInsert(int flags)
        {
            float rotation = (float)((flags & 0xF) / 8.0 * Math.PI);
            int numberOfEdges = (flags & 0xF0) >> 4;
            if (numberOfEdges >= 5)
            {
                float x, y;
                for (int i = 0; i < numberOfEdges; i++)
                {
                    base.Width = 100;
                    base.Height = 100;
                    x = 50 + (float)(50 * Math.Cos(rotation + 2 * Math.PI * i / numberOfEdges));
                    y = 50 + (float)(50 * Math.Sin(rotation + 2 * Math.PI * i / numberOfEdges));
                    addPoint(x, y, (byte)(i == 0 ? 0 : 1));
                }
            }
        }

        /// <inheritdoc/>
        public override bool PointInObject(PointF point)
        {
            using (Pen pen = new Pen(Color.Black, 10))
            using (GraphicsPath path = new GraphicsPath())
            {
                if (points != null && points.Count > 1 && currentPoint < 0)
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        path.AddLine(AbsLeft + points[i].X + center.X, AbsTop + points[i].Y + center.Y, AbsLeft + points[i + 1].X + center.X, AbsTop + points[i + 1].Y + center.Y);
                    }
                else
                    path.AddLine(AbsLeft, AbsTop, AbsRight, AbsBottom);

                return path.IsOutlineVisible(point, pen);
            }
        }

        /// <summary>
        /// Delete point from polyline by index
        /// Recalculate bounds after remove
        /// </summary>
        /// <param name="index">Index of point in polyline</param>
        public void RemovePointAt(int index)
        {
            deletePoint(index);
            if (Report != null)
                if (Report.Designer != null)
                    Report.Designer.SetModified(this, "Change");
        }

        /// <inheritdoc/>
        public override void SelectionChanged()
        {
            base.SelectionChanged();
            if (!IsSelected)
                polygonSelectionMode = PolygonSelectionMode.Scale;
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            Border.SimpleBorder = true;
            base.Serialize(writer);
            PolyLineObject c = writer.DiffObject as PolyLineObject;

            StringBuilder sb = new StringBuilder(points.Count * 10);
            for (int i = 0; i < points.Count; i++)
            {
                sb.AppendFormat("{0}\\{1}\\{2}", points[i].X.ToString(CultureInfo.InvariantCulture),
                    points[i].Y.ToString(CultureInfo.InvariantCulture),
                    pointTypes[i]);
                if (i < points.Count - 1)
                    sb.Append("|");
            }

            writer.WriteStr("PolyPoints", sb.ToString());

            writer.WriteFloat("CenterX", center.X);
            writer.WriteFloat("CenterY", center.Y);
        }

        #endregion Public Methods

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineObject"/> class with default settings.
        /// </summary>
        public PolyLineObject()
        {
            FlagSimpleBorder = true;
            FlagUseFill = false;
            points = null;
            pointTypes = null;
            currentPoint = -1;
            polygonSelectionMode = PolygonSelectionMode.Scale;
            center = PointF.Empty;
            toolbar = new PolyLineObjectToolBar();
        }

        #endregion Public Constructors

        #region Internal Enums

        internal enum PolygonSelectionMode : int
        {
            Normal = 0,
            AddToStart = 1,
            AddToEnd = 2,
            AddToLine = 3,
            Delete = 4,
            Scale = 5
        }

        #endregion Internal Enums
    }

    internal class PolyLineObjectToolBar
    {
        #region Public Fields

        public bool Visible;

        #endregion Public Fields

        #region Private Fields

        private Image btn_add = null;
        private Image btn_delete = null;
        private Image btn_end = null;
        private Image btn_pointer = null;
        private Image btn_start = null;
        private float FScaleX;
        private float FScaleY;

        #endregion Private Fields

        #region Public Methods

        public void Draw(PolyLineObject obj, Graphics g, PointF point, float scaleX, float scaleY)
        {
            if (Visible)
            {
                FScaleX = scaleX;
                FScaleY = scaleY;
                point.X = point.X * scaleX;
                point.Y = point.Y * scaleY - 24;
                /*switch(obj.polygonSelectionMode)
                {
                    case PolyLineObject.PolygonSelectionMode.Normal:
                    case PolyLineObject.PolygonSelectionMode.Scale:
                }*/
                if (btn_pointer == null)
                {
                    btn_pointer = Res.GetImage(100);
                    btn_start = Res.GetImage(151);
                    btn_end = Res.GetImage(150);
                    btn_add = Res.GetImage(152);
                    btn_delete = Res.GetImage(51);
                }
                point = DrawImage(g, btn_pointer, point, obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.Scale || obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.Normal);
                point = DrawImage(g, btn_start, point, obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.AddToStart);
                point = DrawImage(g, btn_end, point, obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.AddToEnd);
                point = DrawImage(g, btn_add, point, obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.AddToLine);
                point = DrawImage(g, btn_delete, point, obj.polygonSelectionMode == PolyLineObject.PolygonSelectionMode.Delete);
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal int Click(PointF lefttop, PointF point)
        {
            lefttop.X = lefttop.X * FScaleX;
            lefttop.Y = lefttop.Y * FScaleY - 24;
            point.X = point.X * FScaleX;
            point.Y = point.Y * FScaleY;
            RectangleF rect = new RectangleF(lefttop, getSizes(btn_pointer));
            if (Click(rect, point)) return 0;
            rect.X += rect.Width + 5; rect.Size = getSizes(btn_start);
            if (Click(rect, point)) return 1;
            rect.X += rect.Width + 5; rect.Size = getSizes(btn_end);
            if (Click(rect, point)) return 2;
            rect.X += rect.Width + 5; rect.Size = getSizes(btn_add);
            if (Click(rect, point)) return 3;
            rect.X += rect.Width + 5; rect.Size = getSizes(btn_delete);
            if (Click(rect, point)) return 4;
            return -1;
        }

        #endregion Internal Methods

        #region Private Methods

        private bool Click(RectangleF rect, PointF point)
        {
            if (point.X < rect.Left) return false;
            if (point.X > rect.Right) return false;
            if (point.Y < rect.Top) return false;
            if (point.Y > rect.Bottom) return false;
            return true;
        }

        private PointF DrawImage(Graphics g, Image img, PointF point, bool selected)
        {
            if (selected)
                g.FillRectangle(Brushes.LightGray, point.X, point.Y, img.Width, img.Height);
            else
                g.FillRectangle(Brushes.White, point.X, point.Y, img.Width, img.Height);
            g.DrawImage(img, point);
            if (selected)
                g.DrawRectangle(Pens.Red, point.X, point.Y, img.Width, img.Height);
            else
                g.DrawRectangle(Pens.Black, point.X, point.Y, img.Width, img.Height);
            point.X += img.Width + 5;
            return point;
        }

        private SizeF getSizes(Image img)
        {
            return new SizeF(img.Width, img.Height);
        }

        #endregion Private Methods

        #region Public Constructors

        public PolyLineObjectToolBar()
        {
            FScaleX = 1;
            FScaleY = 1;
            Visible = false;
        }

        #endregion Public Constructors
    }
}