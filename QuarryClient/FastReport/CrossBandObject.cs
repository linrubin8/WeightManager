using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Design.PageDesigners.Page;

namespace FastReport
{
  /// <summary>
  /// Specifies the shape of the <b>CrossBandObject</b>.
  /// </summary>
  internal enum CrossBandShape
  {
    /// <summary>
    /// Specifies the vertical line shape.
    /// </summary>
    Line,
    
    /// <summary>
    /// Specifies the rectangle shape.
    /// </summary>
    Rectangle
  }
  
  
  /// <summary>
  /// Represents an object that can be printed across several bands.
  /// </summary>
  internal class CrossBandObject : ReportComponentBase
  {
    private CrossBandShape FShape;
    private BandBase FStartBand;
    private BandBase FEndBand;
    private float FEndBandHeight;

    #region Properties
    /// <summary>
    /// Gets or sets the object's shape.
    /// </summary>
    [DefaultValue(ShapeKind.Rectangle)]
    [Category("Appearance")]
    public CrossBandShape Shape
    {
      get { return FShape; }
      set { FShape = value; }
    }

    [Category("Layout")]
    public BandBase EndBand
    {
      get { return FEndBand; }
      set { FEndBand = value; }
    }

    [Category("Layout")]
    public float EndBandHeight
    {
      get { return FEndBandHeight; }
      set 
      { 
        FEndBandHeight = value;
        if (FEndBand != null)
          Height = FEndBand.AbsTop - AbsTop + FEndBandHeight;
      }
    }

    public override float Top
    {
      get { return base.Top; }
      set
      {
        base.Top = value;
        if (FEndBand != null)
          FEndBandHeight = AbsBottom - FEndBand.AbsTop;
      }
    }
    
    public override float Height
    {
      get { return base.Height; }
      set
      {
        base.Height = value;
        if (FEndBand != null)
          FEndBandHeight = AbsBottom - FEndBand.AbsTop;
      }
    }
    #endregion

    #region Private Methods
    private void FindStartEndBands()
    {
      FStartBand = null;
      FEndBand = null;
      
      List<BandBase> list = new List<BandBase>();
      foreach (Base c in Page.AllObjects)
      {
        if (c is BandBase)
          list.Add(c as BandBase);
      }

      int bandGap = ReportWorkspace.ClassicView && IsDesigning ? BandBase.HeaderSize : 4;
      foreach (BandBase c in list)
      {
        bool topInside = AbsTop > c.AbsTop - bandGap && AbsTop < c.AbsBottom + 1;

        if (topInside)
        {
          FStartBand = c;
          break;
        }
      }
      foreach (BandBase c in list)
      {
        bool bottomInside = AbsBottom > c.AbsTop - bandGap && AbsBottom < c.AbsBottom + 1;

        if (bottomInside)
        {
          FEndBand = c;
          break;
        }
      }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (Shape == CrossBandShape.Line)
        return new SelectionPoint[] {
          new SelectionPoint(AbsLeft, AbsTop, SizingPoint.TopCenter),
          new SelectionPoint(AbsLeft, AbsTop + Height, SizingPoint.BottomCenter) };
      return base.GetSelectionPoints();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      CrossBandObject src = source as CrossBandObject;
      Shape = src.Shape;
      EndBand = src.EndBand;
      EndBandHeight = src.EndBandHeight;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (IsDesigning)
      {
        // force set height
        EndBandHeight = EndBandHeight;
      }
      
      base.Draw(e);

      if (Shape == CrossBandShape.Line)
      {
        Border.Lines = BorderLines.Left;
        Width = 0;
      }

      Border.Draw(e, new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY));
    }

    /// <inheritdoc/>
    public override bool PointInObject(PointF point)
    {
      using (Pen pen = new Pen(Color.Black, 10))
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddLine(AbsLeft, AbsTop, AbsRight, AbsTop);
        path.AddLine(AbsRight, AbsTop, AbsRight, AbsBottom);
        path.AddLine(AbsRight, AbsBottom, AbsLeft, AbsBottom);
        path.AddLine(AbsLeft, AbsBottom, AbsLeft, AbsTop);
        return path.IsOutlineVisible(point, pen);
      }
    }

    /// <inheritdoc/>
    public override void CheckParent(bool immediately)
    {
      if (!(Parent is BandBase) || !IsSelected || IsAncestor)
        return;

      if (Top < 0 || Top > (Parent as BandBase).Height || EndBand == null ||
        EndBandHeight < 0 || EndBandHeight > EndBand.Height)
      {
        FindStartEndBands();

        if (FStartBand != null && HasFlag(Flags.CanChangeParent))
        {
          Top = (int)Math.Round((AbsTop - FStartBand.AbsTop) / Page.SnapSize.Height) * Page.SnapSize.Height;
          Parent = FStartBand;
        }
        if (FEndBand != null)
        {
          Height = FEndBand.AbsTop - AbsTop + (int)Math.Round((AbsBottom - FEndBand.AbsTop) / Page.SnapSize.Height) * Page.SnapSize.Height;
        }
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      CrossBandObject c = writer.DiffObject as CrossBandObject;
      base.Serialize(writer);

      if (Shape != c.Shape)
        writer.WriteValue("Shape", Shape);
      if (EndBand != null)
        writer.WriteRef("EndBand", EndBand);
      if (FloatDiff(EndBandHeight, c.EndBandHeight))
        writer.WriteFloat("EndBandHeight", EndBandHeight);
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      Shape = (CrossBandShape)flags;
      Border.Lines = BorderLines.All;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="CrossBandObject"/> class with the default settings.
    /// </summary>
    public CrossBandObject()
    {
      Border.Lines = BorderLines.All;
    }
  }
}
