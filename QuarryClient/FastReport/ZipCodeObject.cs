using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;
using FastReport.TypeEditors;
using System.Drawing.Drawing2D;
using FastReport.TypeConverters;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Represents a zip code object.
  /// </summary>
  /// <remarks>
  /// This object is mainly used in Russia to print postal index on envelopes. It complies with the
  /// GOST R 51506-99.
  /// </remarks>
  public class ZipCodeObject : ReportComponentBase
  {
    #region Fields
    private float FSegmentWidth;
    private float FSegmentHeight;
    private float FSpacing;
    private int FSegmentCount;
    private bool FShowMarkers;
    private bool FShowGrid;

    private string FDataColumn;
    private string FExpression;
    private string FText;

    private static List<Point[]> FDigits;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the width of a single zipcode segment, in pixels.
    /// </summary>
    [Category("Layout")]
    [TypeConverter(typeof(UnitsConverter))]
    public float SegmentWidth
    {
      get { return FSegmentWidth; }
      set { FSegmentWidth = value; }
    }

    /// <summary>
    /// Gets or sets the height of a single zipcode segment, in pixels.
    /// </summary>
    [Category("Layout")]
    [TypeConverter(typeof(UnitsConverter))]
    public float SegmentHeight
    {
      get { return FSegmentHeight; }
      set { FSegmentHeight = value; }
    }

    /// <summary>
    /// Gets or sets the spacing between origins of segments, in pixels.
    /// </summary>
    [Category("Layout")]
    [TypeConverter(typeof(UnitsConverter))]
    public float Spacing
    {
      get { return FSpacing; }
      set { FSpacing = value; }
    }

    /// <summary>
    /// Gets or sets the number of segments in zipcode.
    /// </summary>
    [Category("Layout")]
    [DefaultValue(6)]
    public int SegmentCount
    {
      get { return FSegmentCount; }
      set { FSegmentCount = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the reference markers should be drawn.
    /// </summary>
    /// <remarks>
    /// Reference markers are used by postal service to automatically read the zipcode.
    /// </remarks>
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowMarkers
    {
      get { return FShowMarkers; }
      set { FShowMarkers = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the segment grid should be drawn.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowGrid
    {
      get { return FShowGrid; }
      set { FShowGrid = value; }
    }

    /// <summary>
    /// Gets or sets a data column name bound to this control.
    /// </summary>
    /// <remarks>
    /// Value must be in the form "Datasource.Column".
    /// </remarks>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string DataColumn
    {
      get { return FDataColumn; }
      set { FDataColumn = value; }
    }

    /// <summary>
    /// Gets or sets an expression that contains the zip code.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Expression
    {
      get { return FExpression; }
      set { FExpression = value; }
    }

    /// <summary>
    /// Gets or sets the zip code.
    /// </summary>
    [Category("Data")]
    public string Text
    {
      get { return FText; }
      set { FText = value; }
    }
    #endregion

    #region Private Methods
    private void DrawSegmentGrid(FRPaintEventArgs e, float offsetX, float offsetY)
    {
      Graphics g = e.Graphics;
      SmoothingMode saveSmoothing = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;

      Brush b = e.Cache.GetBrush(Border.Color);
      
      int[] grid = new int[] { 111111, 110001, 101001, 100101, 100011, 111111, 110001, 101001, 100101, 100011, 111111 };
      float ratioX = FSegmentWidth / (Units.Centimeters * 0.5f);
      float ratioY = FSegmentHeight / (Units.Centimeters * 1);
      float pointSize = Units.Millimeters * 0.25f;
      float y = AbsTop;
      
      foreach (int gridRow in grid)
      {
        int row = gridRow;
        float x = AbsLeft;
        
        while (row > 0)
        {
          if (row % 10 == 1)
            g.FillEllipse(b, (x + offsetX - pointSize / 2) * e.ScaleX, (y + offsetY - pointSize / 2) * e.ScaleY,
              pointSize * e.ScaleX, pointSize * e.ScaleY);
          row /= 10;
          
          x += Units.Millimeters * 1 * ratioX;
        }

        y += Units.Millimeters * 1 * ratioY;
      }

      g.SmoothingMode = saveSmoothing;
    }

    private void DrawReferenceLine(FRPaintEventArgs e, float offsetX)
    {
      Graphics g = e.Graphics;
      Brush b = e.Cache.GetBrush(Border.Color);

      g.FillRectangle(b,
        new RectangleF((AbsLeft + offsetX) * e.ScaleX, AbsTop * e.ScaleY,
          Units.Millimeters * 7 * e.ScaleX, Units.Millimeters * 2 * e.ScaleY));

      // draw start line
      if (offsetX == 0)
      {
        g.FillRectangle(b,
          new RectangleF((AbsLeft + offsetX) * e.ScaleX, (AbsTop + Units.Millimeters * 3) * e.ScaleY,
            Units.Millimeters * 7 * e.ScaleX, Units.Millimeters * 1 * e.ScaleY));
      }
    }

    private void DrawSegment(FRPaintEventArgs e, int symbol, float offsetX)
    {
      Graphics g = e.Graphics;
      float offsetY = 0;

      // draw marker
      if (FShowMarkers)
      {
        DrawReferenceLine(e, offsetX);
        if (offsetX == 0)
          return;
        offsetX += Units.Millimeters * 1;
        offsetY = Units.Millimeters * 4;
      }
      else
      {
        // draw inside the object's area - important when you export the object
        offsetX += Border.Width / 2;
        offsetY += Border.Width / 2;
      }
      
      // draw grid
      if (FShowGrid)
        DrawSegmentGrid(e, offsetX, offsetY);
      
      // draw symbol
      if (symbol != -1)
      {
        Point[] digit = FDigits[symbol];
        PointF[] path = new PointF[digit.Length];
        float ratioX = FSegmentWidth / (Units.Centimeters * 0.5f);
        float ratioY = FSegmentHeight / (Units.Centimeters * 1);
        
        for (int i = 0; i < digit.Length; i++)
        {
          path[i] = new PointF((AbsLeft + digit[i].X * Units.Millimeters * ratioX + offsetX) * e.ScaleX,
            (AbsTop + digit[i].Y * Units.Millimeters * ratioY + offsetY) * e.ScaleY);
        }
        
        using (Pen pen = new Pen(Border.Color, Border.Width * e.ScaleX))
        {
          pen.StartCap = LineCap.Round;
          pen.EndCap = LineCap.Round;
          pen.LineJoin = LineJoin.Round;
          g.DrawLines(pen, path);
        }
      }  
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] { new SelectionPoint(AbsLeft, AbsTop, SizingPoint.LeftTop) };
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);

      ZipCodeObject src = source as ZipCodeObject;

      SegmentWidth = src.SegmentWidth;
      SegmentHeight = src.SegmentHeight;
      Spacing = src.Spacing;
      SegmentCount = src.SegmentCount;
      ShowMarkers = src.ShowMarkers;
      ShowGrid = src.ShowGrid;

      DataColumn = src.DataColumn;
      Expression = src.Expression;
      Text = src.Text;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      Width = ((FShowMarkers ? 1 : 0) + FSegmentCount) * FSpacing;
      Height = (FShowMarkers ? FSegmentHeight + Units.Millimeters * 4: FSegmentHeight) + Border.Width;
      
      base.Draw(e);
      
      float offsetX = 0;
      if (FShowMarkers)
      {
        // draw starting marker
        DrawSegment(e, -1, 0);
        offsetX += FSpacing;
      }

      string text = Text.PadLeft(FSegmentCount, '0');
      text = text.Substring(0, FSegmentCount);
      
      foreach (char ch in text)
      {
        int symbol = -1;
        if (ch >= '0' && ch <= '9')
          symbol = (int)ch - (int)'0';
        DrawSegment(e, symbol, offsetX);
        offsetX += FSpacing;
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ZipCodeObject c = writer.DiffObject as ZipCodeObject;
      Border.SimpleBorder = true;
      base.Serialize(writer);

      if (FloatDiff(SegmentWidth, c.SegmentWidth))
        writer.WriteFloat("SegmentWidth", SegmentWidth);
      if (FloatDiff(SegmentHeight, c.SegmentHeight))
        writer.WriteFloat("SegmentHeight", SegmentHeight);
      if (FloatDiff(Spacing, c.Spacing))
        writer.WriteFloat("Spacing", Spacing);
      if (SegmentCount != c.SegmentCount)
        writer.WriteInt("SegmentCount", SegmentCount);
      if (ShowMarkers != c.ShowMarkers)
        writer.WriteBool("ShowMarkers", ShowMarkers);
      if (ShowGrid != c.ShowGrid)
        writer.WriteBool("ShowGrid", ShowGrid);  
      
      if (DataColumn != c.DataColumn)
        writer.WriteStr("DataColumn", DataColumn);
      if (Expression != c.Expression)
        writer.WriteStr("Expression", Expression);
      if (Text != c.Text)
        writer.WriteStr("Text", Text);
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      if ((Page as ReportPage).IsImperialUnitsUsed)
        return new SizeF(Units.Inches * 2, Units.Inches * 0.5f);
      return new SizeF(Units.Centimeters * 5, Units.Centimeters * 1);
    }

    /// <inheritdoc/>
    public override void OnAfterInsert(InsertFrom source)
    {
      base.OnAfterInsert(source);
      if (source != InsertFrom.Clipboard)
        Border.Width = 3;
    }

    /// <inheritdoc/>
    public override SmartTagBase GetSmartTag()
    {
      return new ZipCodeSmartTag(this);
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new ZipCodeObjectMenu(Report.Designer);
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      if (!String.IsNullOrEmpty(DataColumn))
        expressions.Add(DataColumn);
      if (!String.IsNullOrEmpty(Expression))
        expressions.Add(Expression);
      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();
      if (!String.IsNullOrEmpty(DataColumn))
      {
        object value = Report.GetColumnValue(DataColumn);
        Text = value == null ? "" : value.ToString();
      }
      else if (!String.IsNullOrEmpty(Expression))
      {
        object value = Report.Calc(Expression);
        Text = value == null ? "" : value.ToString();
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ZipCodeObject"/> with the default settings.
    /// </summary>
    public ZipCodeObject()
    {
      FSegmentWidth = Units.Centimeters * 0.5f;
      FSegmentHeight = Units.Centimeters * 1;
      FSpacing = Units.Centimeters * 0.9f;
      FSegmentCount = 6;
      FShowMarkers = true;
      FShowGrid = true;
      
      FText = "123456";
      FDataColumn = "";
      FExpression = "";
      
      FlagSimpleBorder = true;
      Border.Width = 3;
      SetFlags(Flags.HasSmartTag, true);
    }
    
    static ZipCodeObject()
    {
      FDigits = new List<Point[]>();
      FDigits.Add(new Point[] { new Point(0, 0), new Point(5, 0), new Point(5, 10), new Point(0, 10), new Point(0, 0) });
      FDigits.Add(new Point[] { new Point(0, 5), new Point(5, 0), new Point(5, 10) });
      FDigits.Add(new Point[] { new Point(0, 0), new Point(5, 0), new Point(5, 5), new Point(0, 10), new Point(5, 10) });
      FDigits.Add(new Point[] { new Point(0, 0), new Point(5, 0), new Point(0, 5), new Point(5, 5), new Point(0, 10) });
      FDigits.Add(new Point[] { new Point(0, 0), new Point(0, 5), new Point(5, 5), new Point(5, 0), new Point(5, 10) });
      FDigits.Add(new Point[] { new Point(5, 0), new Point(0, 0), new Point(0, 5), new Point(5, 5), new Point(5, 10), new Point(0, 10) });
      FDigits.Add(new Point[] { new Point(5, 0), new Point(0, 5), new Point(0, 10), new Point(5, 10), new Point(5, 5), new Point(0, 5) });
      FDigits.Add(new Point[] { new Point(0, 0), new Point(5, 0), new Point(0, 5), new Point(0, 10) });
      FDigits.Add(new Point[] { new Point(0, 5), new Point(0, 0), new Point(5, 0), new Point(5, 10), new Point(0, 10), new Point(0, 5), new Point(5, 5) });
      FDigits.Add(new Point[] { new Point(5, 5), new Point(0, 5), new Point(0, 0), new Point(5, 0), new Point(5, 5), new Point(0, 10) });
    }
  }
}
