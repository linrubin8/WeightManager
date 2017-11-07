using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using FastReport.Design;
using FastReport.TypeConverters;
using FastReport.Design.PageDesigners.Page;
using FastReport.Data;
using FastReport.TypeEditors;
using FastReport.Utils;
using System.Drawing;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  /// <summary>
  /// Represents a table column.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="Width"/> property to set the width of a column. If <see cref="AutoSize"/>
  /// property is <b>true</b>, the column will calculate its width automatically.
  /// <para/>You can also set the <see cref="MinWidth"/> and <see cref="MaxWidth"/> properties
  /// to restrict the column's width.
  /// </remarks>
  public class TableColumn : ComponentBase
  {
    #region Fields
    private float FMinWidth;
    private float FMaxWidth;
    private bool FAutoSize;
    private bool FPageBreak;
    private int FKeepColumns;
    private int FIndex;
    private float FSaveWidth;
    private bool FSaveVisible;
    private float FMinimumBreakWidth;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a width of the column, in pixels.
    /// </summary>
    /// <remarks>
    /// The column width cannot exceed the range defined by the <see cref="MinWidth"/> 
    /// and <see cref="MaxWidth"/> properties.
    /// <note>To convert between pixels and report units, use the constants defined 
    /// in the <see cref="Units"/> class.</note>
    /// </remarks>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Width
    {
      get { return base.Width; }
      set
      {
        value = Converter.DecreasePrecision(value, 2);
        if (value > MaxWidth)
          value = MaxWidth;
        if (value < MinWidth)
          value = MinWidth;
        if (FloatDiff(base.Width, value))
        {
          UpdateLayout(value - base.Width, 0);
          base.Width = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the minimal width for this column, in pixels.
    /// </summary>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(0f)]
    [Category("Layout")]
    public float MinWidth
    {
      get { return FMinWidth; }
      set { FMinWidth = value; }
    }

    /// <summary>
    /// Gets or sets the maximal width for this column, in pixels.
    /// </summary>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(5000f)]
    [Category("Layout")]
    public float MaxWidth
    {
      get { return FMaxWidth; }
      set { FMaxWidth = value; }
    }

    /// <summary>
    /// Gets or sets a value determines if the column should calculate its width automatically.
    /// </summary>
    /// <remarks>
    /// The column width cannot exceed the range defined by the <see cref="MinWidth"/> 
    /// and <see cref="MaxWidth"/> properties.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AutoSize
    {
      get { return FAutoSize; }
      set { FAutoSize = value; }
    }

    /// <summary>
    /// Gets the index of this column.
    /// </summary>
    [Browsable(false)]
    public int Index
    {
      get { return FIndex; }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Left
    {
      get
      {
        TableBase table = Parent as TableBase;
        if (table == null)
          return 0;

        float result = 0;
        for (int i = 0; i < Index; i++)
        {
          result += table.Columns[i].Width;
        }
        return result;
      }
      set { base.Left = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Top
    {
      get { return base.Top; }
      set { base.Top = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Height
    {
      get { return base.Height; }
      set { base.Height = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override DockStyle Dock
    {
      get { return base.Dock; }
      set { base.Dock = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override AnchorStyles Anchor
    {
      get { return base.Anchor; }
      set { base.Anchor = value; }
    }

    /// <summary>
    /// Gets or sets the page break flag for this column.
    /// </summary>
    [Browsable(false)]
    public bool PageBreak
    {
      get { return FPageBreak; }
      set { FPageBreak = value; }
    }

    /// <summary>
    /// Gets or sets the number of columns to keep on the same page.
    /// </summary>
    [Browsable(false)]
    public int KeepColumns
    {
      get { return FKeepColumns; }
      set { FKeepColumns = value; }
    }

    internal float MinimumBreakWidth
    {
      get { return FMinimumBreakWidth; }
      set { FMinimumBreakWidth = value; }
    }
    
    internal static float DefaultWidth
    {
      get { return (int)Math.Round(64 / (0.25f * Units.Centimeters)) * (0.25f * Units.Centimeters); }
    }
    #endregion

    #region Private Methods
    private void UpdateLayout(float dx, float dy)
    {
      TableBase table = Parent as TableBase;
      if (table == null)
        return;

      // update this column cells
      foreach (TableRow row in table.Rows)
      {
        row.CellData(Index).UpdateLayout(Width, row.Height, dx, dy);
      }

      // update spanned cells that contains this column
      List<Rectangle> spanList = table.GetSpanList();
      foreach (Rectangle span in spanList)
      {
        if (Index > span.Left && Index < span.Right)
        {
          TableRow row = table.Rows[span.Top];
          row.CellData(span.Left).UpdateLayout(table.Columns[span.Left].Width, row.Height, dx, dy);
        }
      }
    }
    #endregion
    
    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] {};
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      TableColumn src = source as TableColumn;
      MinWidth = src.MinWidth;
      MaxWidth = src.MaxWidth;
      AutoSize = src.AutoSize;
      KeepColumns = src.KeepColumns;

      base.Assign(source);
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return (Parent as TableBase).GetColumnContextMenu(this);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      TableColumn c = writer.DiffObject as TableColumn;
      base.Serialize(writer);

      if (FloatDiff(MinWidth, c.MinWidth))
        writer.WriteFloat("MinWidth", MinWidth);
      if (FloatDiff(MaxWidth, c.MaxWidth))
        writer.WriteFloat("MaxWidth", MaxWidth);
      if (FloatDiff(Width, c.Width))
        writer.WriteFloat("Width", Width);
      if (AutoSize != c.AutoSize)
        writer.WriteBool("AutoSize", AutoSize);
    }

    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      if (Parent != null)
        Parent.SelectionChanged();
    }

    internal void SetIndex(int value)
    {
      FIndex = value;
    }
    
    internal void SaveState()
    {
      FSaveWidth = Width;
      FSaveVisible = Visible;
    }

    internal void RestoreState()
    {
      Width = FSaveWidth;
      Visible = FSaveVisible;
    }

    /// <inheritdoc/>
    public override void Clear()
    {
      TableBase grid = Parent as TableBase;
      if (grid == null)
        return;

      int colIndex = grid.Columns.IndexOf(this);
      foreach (TableRow row in grid.Rows)
      {
        row[colIndex].Dispose();
      }

      base.Clear();
    }
    #endregion
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TableColumn"/> class.
    /// </summary>
    public TableColumn()
    {
      FMaxWidth = 5000;
      Width = DefaultWidth;
      SetFlags(Flags.CanCopy | Flags.CanDelete | Flags.CanWriteBounds, false);
      BaseName = "Column";
    }
  }
}
