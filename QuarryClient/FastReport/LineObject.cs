using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using FastReport.TypeConverters;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Represents a line object.
  /// </summary>
  /// <remarks>
  /// Use the <b>Border.Width</b>, <b>Border.Style</b> and <b>Border.Color</b> properties to set 
  /// the line width, style and color. Set the <see cref="Diagonal"/> property to <b>true</b>
  /// if you want to show a diagonal line.
  /// </remarks>
  public class LineObject : ReportComponentBase
  {
    #region Fields
    private bool FDiagonal;
    private CapSettings FStartCap;
    private CapSettings FEndCap;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value indicating that the line is diagonal.
    /// </summary>
    /// <remarks>
    /// If this property is <b>false</b>, the line can be only horizontal or vertical.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool Diagonal
    {
      get { return FDiagonal; }
      set { FDiagonal = value; }
    }

    /// <summary>
    /// Gets or sets the start cap settings.
    /// </summary>
    [Category("Appearance")]
    public CapSettings StartCap
    {
      get { return FStartCap; }
      set { FStartCap = value; }
    }

    /// <summary>
    /// Gets or sets the end cap settings.
    /// </summary>
    [Category("Appearance")]
    public CapSettings EndCap
    {
      get { return FEndCap; }
      set { FEndCap = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override FillBase Fill
    {
      get { return base.Fill; }
      set { base.Fill = value; }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeStartCap()
    {
      return !StartCap.Equals(new CapSettings());
    }

    private bool ShouldSerializeEndCap()
    {
      return !EndCap.Equals(new CapSettings());
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] {
        new SelectionPoint(AbsLeft, AbsTop, SizingPoint.LeftTop),
        new SelectionPoint(AbsLeft + Width, AbsTop + Height, SizingPoint.RightBottom) };
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      LineObject src = source as LineObject;
      Diagonal = src.Diagonal;
      StartCap.Assign(src.StartCap);
      EndCap.Assign(src.EndCap);
    }

    /// <inheritdoc/>
    public override bool PointInObject(PointF point)
    {
      using (Pen pen = new Pen(Color.Black, 10))
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddLine(AbsLeft, AbsTop, AbsRight, AbsBottom);
        return path.IsOutlineVisible(point, pen);
      }
    }

    /// <inheritdoc/>
    public override void CheckNegativeSize(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      return new SizeF(0, 0);
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      base.HandleMouseMove(e);
      if (e.Handled)
        e.Cursor = Cursors.Cross;
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (e.Mode == WorkspaceMode2.SelectionRect)
      {
        GraphicsPath path = new GraphicsPath();
        Pen pen = null;
        if (Width != 0 && Height != 0)
        {
          path.AddLine(AbsLeft, AbsTop, AbsRight, AbsBottom);
          pen = new Pen(Color.Black, 10);
          path.Widen(pen);
        }
        else
        {
          if (Width == 0)
          {
            path.AddLine(AbsLeft - 5, AbsTop, AbsRight - 5, AbsBottom);
            path.AddLine(AbsRight - 5, AbsBottom, AbsRight + 5, AbsBottom);
            path.AddLine(AbsRight + 5, AbsBottom, AbsRight + 5, AbsTop);
          }
          else
          {
            path.AddLine(AbsLeft, AbsTop - 5, AbsRight, AbsBottom - 5);
            path.AddLine(AbsRight, AbsBottom - 5, AbsRight, AbsBottom + 5);
            path.AddLine(AbsRight, AbsBottom + 5, AbsLeft, AbsTop + 5);
          }  
          path.CloseFigure();
        }
        
        Region region = new Region(path);
        e.Handled = region.IsVisible(e.SelectionRect);

        path.Dispose();
        region.Dispose();
        if (pen != null)
          pen.Dispose();
      }
      else if (e.Mode == WorkspaceMode2.Size)
      {
        if (!Diagonal)
        {
          if (Math.Abs(Width) > Math.Abs(Height))
            Height = 0;
          else
            Width = 0;
        }
      }
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      Graphics g = e.Graphics;
      // draw marker when inserting a line
      if (Width == 0 && Height == 0)
      {
        g.DrawLine(Pens.Black, AbsLeft * e.ScaleX - 6, AbsTop * e.ScaleY, AbsLeft * e.ScaleX + 6, AbsTop * e.ScaleY);
        g.DrawLine(Pens.Black, AbsLeft * e.ScaleX, AbsTop * e.ScaleY - 6, AbsLeft * e.ScaleX, AbsTop * e.ScaleY + 6);
        return;
      }

      Report report = Report;
      if (report != null && report.SmoothGraphics)
      {
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = SmoothingMode.AntiAlias;
      }
      
      Pen pen;
      if (StartCap.Style == CapStyle.None && EndCap.Style == CapStyle.None)
        pen = e.Cache.GetPen(Border.Color, Border.Width * e.ScaleX, Border.DashStyle);
      else
      {
        pen = new Pen(Border.Color, Border.Width * e.ScaleX);
        pen.DashStyle = Border.DashStyle;
        CustomLineCap startCap = StartCap.Cap;
        CustomLineCap endCap = EndCap.Cap;
        if (startCap != null)
          pen.CustomStartCap = startCap;
        if (endCap != null)
          pen.CustomEndCap = endCap;
      }  

      float width = Width;
      float height = Height;
      if (!Diagonal)
      {
        if (Math.Abs(width) > Math.Abs(height))
          height = 0;
        else
          width = 0;
      }

      g.DrawLine(pen, AbsLeft * e.ScaleX, AbsTop * e.ScaleY, (AbsLeft + width) * e.ScaleX, (AbsTop + height) * e.ScaleY);
      
      if (StartCap.Style != CapStyle.None || EndCap.Style != CapStyle.None)
        pen.Dispose();
      if (report != null && report.SmoothGraphics && Diagonal)
      {
        g.InterpolationMode = InterpolationMode.Default;
        g.SmoothingMode = SmoothingMode.Default;
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      Border.SimpleBorder = true;
      base.Serialize(writer);
      LineObject c = writer.DiffObject as LineObject;
      
      if (Diagonal != c.Diagonal)
        writer.WriteBool("Diagonal", Diagonal);
      StartCap.Serialize("StartCap", writer, c.StartCap);
      EndCap.Serialize("EndCap", writer, c.EndCap);
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      FDiagonal = flags != 0;
      if (flags == 3 || flags == 4)
        StartCap.Style = CapStyle.Arrow;
      if (flags == 2 || flags == 4)
        EndCap.Style = CapStyle.Arrow;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="LineObject"/> class with default settings.
    /// </summary>
    public LineObject()
    {
      FStartCap = new CapSettings();
      FEndCap = new CapSettings();
      FlagSimpleBorder = true;
      FlagUseFill = false;
    }
  }
}
