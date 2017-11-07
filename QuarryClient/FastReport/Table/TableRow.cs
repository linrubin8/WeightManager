using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using FastReport.Design;
using FastReport.TypeConverters;
using FastReport.Data;
using FastReport.TypeEditors;
using FastReport.Utils;
using FastReport.Design.PageDesigners.Page;
using System.Drawing;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  /// <summary>
  /// Represents a table row.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="Height"/> property to set the height of a row. If <see cref="AutoSize"/>
  /// property is <b>true</b>, the row will calculate its height automatically.
  /// <para/>You can also set the <see cref="MinHeight"/> and <see cref="MaxHeight"/> properties
  /// to restrict the row's height.
  /// </remarks>
  public class TableRow : ComponentBase, IParent
  {
    #region Fields
    private List<TableCellData> FCells;
    private float FMinHeight;
    private float FMaxHeight;
    private bool FAutoSize;
    private bool FPageBreak;
    private int FKeepRows;
    private int FIndex;
    private float FSaveHeight;
    private bool FSaveVisible;
    private bool FSerializingToPreview;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a height of the row, in pixels.
    /// </summary>
    /// <remarks>
    /// The row height cannot exceed the range defined by the <see cref="MinHeight"/> 
    /// and <see cref="MaxHeight"/> properties.
    /// <note>To convert between pixels and report units, use the constants defined 
    /// in the <see cref="Units"/> class.</note>
    /// </remarks>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Height
    {
      get { return base.Height; }
      set
      {
        value = Converter.DecreasePrecision(value, 2);
        if (value > MaxHeight)
          value = MaxHeight;
        if (value < MinHeight)
          value = MinHeight;
        if (FloatDiff(base.Height, value))
        {
          UpdateLayout(0, value - base.Height);
          base.Height = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the minimal height for this row, in pixels.
    /// </summary>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(0f)]
    [Category("Layout")]
    public float MinHeight
    {
      get { return FMinHeight; }
      set { FMinHeight = value; }
    }

    /// <summary>
    /// Gets or sets the maximal height for this row, in pixels.
    /// </summary>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(5000f)]
    [Category("Layout")]
    public float MaxHeight
    {
      get { return FMaxHeight; }
      set { FMaxHeight = value; }
    }

    /// <summary>
    /// Gets or sets a value determines if the row should calculate its height automatically.
    /// </summary>
    /// <remarks>
    /// The row height cannot exceed the range defined by the <see cref="MinHeight"/> 
    /// and <see cref="MaxHeight"/> properties.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AutoSize
    {
      get { return FAutoSize; }
      set { FAutoSize = value; }
    }

    /// <summary>
    /// Gets the index of this row.
    /// </summary>
    [Browsable(false)]
    public int Index
    {
      get { return FIndex; }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Top
    {
      get
      {
        TableBase table = Parent as TableBase;
        if (table == null)
          return 0;
        
        float result = 0;
        for (int i = 0; i < Index; i++)
        {
          result += table.Rows[i].Height;
        }
        return result;
      }
      set { base.Top = value; }
    }

    /// <summary>
    /// Gets or sets the cell with specified index.
    /// </summary>
    /// <param name="col">Column index.</param>
    /// <returns>The <b>TableCell</b> object.</returns>
    [Browsable(false)]
    public TableCell this[int col]
    {
      get
      {
        TableCellData cellData = CellData(col);
        TableCell cell = cellData.Cell;
        cell.SetParent(this);
        cell.SetValue(cellData.Value);
        return cell;
      }
      set
      {
        TableCellData cellData = CellData(col);
        cellData.AttachCell(value);
      }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Left
    {
      get { return base.Left; }
      set { base.Left = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Width
    {
      get { return base.Width; }
      set { base.Width = value; }
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
    /// Gets or sets the page break flag for this row.
    /// </summary>
    [Browsable(false)]
    public bool PageBreak
    {
      get { return FPageBreak; }
      set { FPageBreak = value; }
    }

    /// <summary>
    /// Gets or sets the number of rows to keep on the same page.
    /// </summary>
    [Browsable(false)]
    public int KeepRows
    {
      get { return FKeepRows; }
      set { FKeepRows = value; }
    }
    
    internal static float DefaultHeight
    {
      get { return (int)Math.Round(18 / (0.25f * Units.Centimeters)) * (0.25f * Units.Centimeters); }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] { };
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      return child is TableCell;
    }

    /// <inheritdoc/>
    public void GetChildObjects(ObjectCollection list)
    {
      TableBase table = Parent as TableBase;
      if (table == null)
        return;

      for (int i = 0; i < table.Columns.Count; i++)
      {
        if (!FSerializingToPreview || table.Columns[i].Visible)
          list.Add(this[i]);
      }
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      // support deserializing the cells
      if (child is TableCell)
      {
        this[FCells.Count] = child as TableCell;
        child.SetParent(this);
      }  
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
    }

    private TableCellData FindCellData(TableCell cell)
    {
      foreach (TableCellData cellData in FCells)
      {
        if (cellData.Cell == cell)
          return cellData;
      }
      return null;
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      TableCellData cellData = FindCellData(child as TableCell);
      return cellData == null ? 0 : FCells.IndexOf(cellData);
    }
    
    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      TableCellData cellData = FindCellData(child as TableCell);
      if (cellData == null)
        return;
        
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (order > FCells.Count)
          order = FCells.Count;
        if (oldOrder <= order)
          order--;
        FCells.Remove(cellData);
        FCells.Insert(order, cellData);
      }
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
      TableBase table = Parent as TableBase;
      if (table == null)
        return;

      // update this row cells
      for (int i = 0; i < table.Columns.Count; i++)
      {
        CellData(i).UpdateLayout(table.Columns[i].Width, Height, dx, dy);
      }

      // update spanned cells that contains this row
      List<Rectangle> spanList = table.GetSpanList();
      foreach (Rectangle span in spanList)
      {
        if (Index > span.Top && Index < span.Bottom)
        {
          TableRow row = table.Rows[span.Top];
          row.CellData(span.Left).UpdateLayout(table.Columns[span.Left].Width, row.Height, dx, dy);
        }
      }

    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      TableRow src = source as TableRow;
      MinHeight = src.MinHeight;
      MaxHeight = src.MaxHeight;
      AutoSize = src.AutoSize;
      KeepRows = src.KeepRows;

      base.Assign(source);
    }
    
    internal TableCellData CellData(int col)
    {
      while (col >= FCells.Count)
      {
        FCells.Add(new TableCellData());
      }

      TableCellData cellData = FCells[col];
      cellData.Table = Parent as TableBase;
      cellData.Address = new Point(col, Index);
      return cellData;
    }
    
    internal void CorrectCellsOnColumnChange(int index, int correct)
    {
      if (correct == 1)
        FCells.Insert(index, new TableCellData());
      else if (index < FCells.Count)
        FCells.RemoveAt(index);  
    }
    
    internal void SetIndex(int value)
    {
      FIndex = value;
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return (Parent as TableBase).GetRowContextMenu(this);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      TableRow c = writer.DiffObject as TableRow;
      FSerializingToPreview = writer.SerializeTo == SerializeTo.Preview;
      base.Serialize(writer);

      if (FloatDiff(MinHeight, c.MinHeight))
        writer.WriteFloat("MinHeight", MinHeight);
      if (FloatDiff(MaxHeight, c.MaxHeight))
        writer.WriteFloat("MaxHeight", MaxHeight);
      if (FloatDiff(Height, c.Height))
        writer.WriteFloat("Height", Height);
      if (AutoSize != c.AutoSize)
        writer.WriteBool("AutoSize", AutoSize);

      if (Parent is TableResult)
      {
        // write children by itself
        SetFlags(Flags.CanWriteChildren, true);
        writer.SaveChildren = true;

        TableResult table = Parent as TableResult;
        foreach (TableColumn column in table.ColumnsToSerialize)
        {
          TableCell cell = this[column.Index];
          writer.Write(cell);
        }
      }  
    }

    /// <inheritdoc/>
    public override void Clear()
    {
      base.Clear();
      foreach (TableCellData cell in FCells)
      {
        cell.Dispose();
      }
      FCells.Clear();
    }

    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      if (Parent != null)
        Parent.SelectionChanged();
    }
    
    internal void SaveState()
    {
      FSaveHeight = Height;
      FSaveVisible = Visible;
    }

    internal void RestoreState()
    {
      Height = FSaveHeight;
      Visible = FSaveVisible;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TableRow"/> class.
    /// </summary>
    public TableRow()
    {
      FCells = new List<TableCellData>();
      FMaxHeight = 5000;
      Height = DefaultHeight;
      SetFlags(Flags.CanCopy | Flags.CanDelete | Flags.CanWriteBounds, false);
      BaseName = "Row";
    }
  }
}
