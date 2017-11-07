using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Engine;
using FastReport.Preview;
using FastReport.Design.PageDesigners.Page;
using FastReport.Data;
using FastReport.Controls;
using FastReport.TypeConverters;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  internal enum MouseMode 
  { 
    None, 
    SelectColumn, 
    SelectRow, 
    SelectCell, 
    ResizeColumn, 
    ResizeRow
  }
  
  /// <summary>
  /// Specifies the layout that will be used when printing a big table.
  /// </summary>
  public enum TableLayout
  {
    /// <summary>
    /// The table is printed across a pages then down.
    /// </summary>
    AcrossThenDown,

    /// <summary>
    /// The table is printed down then across a pages.
    /// </summary>
    DownThenAcross,
    
    /// <summary>
    /// The table is wrapped.
    /// </summary>
    Wrapped
  }

  /// <summary>
  /// The base class for table-type controls such as <see cref="TableObject"/> and 
  /// <see cref="FastReport.Matrix.MatrixObject"/>.
  /// </summary>
  public class TableBase : BreakableComponent, IParent
  {
    #region Fields
    private TableRowCollection FRows;
    private TableColumnCollection FColumns;
    private TableStyleCollection FStyles;
    private int FFixedRows;
    private int FFixedColumns;
    private bool FRepeatHeaders;
    private TableLayout FLayout;
    private float FWrappedGap;
    private bool FAdjustSpannedCellsWidth;
    private bool FLockColumnRowChange;
    private bool FLockCorrectSpans;
    private MouseMode FMouseMode;
    private Point FStartSelectionPoint;
    private TableClipboard FClipboard;
    private List<Rectangle> FSpanList;
    private TableResult FResultTable;
    private TableCellData FPrintingCell;
    private bool FSerializingToPreview;
    private static Cursor curDownArrow = ResourceLoader.GetCursor("DownArrow.cur");
    private static Cursor curRightArrow = ResourceLoader.GetCursor("RightArrow.cur");
    //private static float FLeftRtl;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a collection of table rows.
    /// </summary>
    [Browsable(false)]
    public TableRowCollection Rows
    {
      get { return FRows; }
    }

    /// <summary>
    /// Gets a collection of table columns.
    /// </summary>
    [Browsable(false)]
    public TableColumnCollection Columns
    {
      get { return FColumns; }
    }

    internal TableStyleCollection Styles
    {
      get { return FStyles; }
    }

    /// <summary>
    /// Gets or sets the number of fixed rows that will be repeated on each page.
    /// </summary>
    [DefaultValue(0)]
    [Category("Layout")]
    public int FixedRows
    {
      get
      {
        int value = FFixedRows;
        if (value >= Rows.Count)
          value = Rows.Count - 1;
        if (value < 0)
          value = 0;
        return value;
      }
      set { FFixedRows = value; }
    }

    /// <summary>
    /// Gets or sets the number of fixed columns that will be repeated on each page.
    /// </summary>
    [DefaultValue(0)]
    [Category("Layout")]
    public int FixedColumns
    {
      get
      {
        int value = FFixedColumns;
        if (value >= Columns.Count)
          value = Columns.Count - 1;
        if (value < 0)
          value = 0;
        return value;
      }
      set { FFixedColumns = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether is necessary to repeat table header on each page.
    /// </summary>
    /// <remarks>
    /// To define a table header, set the <see cref="FixedRows"/> and <see cref="FixedColumns"/>
    /// properties.
    /// </remarks>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool RepeatHeaders
    {
      get { return FRepeatHeaders; }
      set { FRepeatHeaders = value; }
    }

    /// <summary>
    /// Gets or sets the table layout.
    /// </summary>
    /// <remarks>
    /// This property affects printing the big table that breaks across pages.
    /// </remarks>
    [DefaultValue(TableLayout.AcrossThenDown)]
    [Category("Behavior")]
    public TableLayout Layout
    {
      get { return FLayout; }
      set { FLayout = value; }
    }

    /// <summary>
    /// Gets or sets gap between parts of the table in wrapped layout mode.
    /// </summary>
    /// <remarks>
    /// This property is used if you set the <see cref="Layout"/> property to <b>Wrapped</b>.
    /// </remarks>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(0f)]
    [Category("Behavior")]
    public float WrappedGap
    {
      get { return FWrappedGap; }
      set { FWrappedGap = value; }
    }


    /// <summary>
    /// Gets or sets a value that determines whether to adjust the spanned cell's width when breaking the table across pages.
    /// </summary>
    /// <remarks>
    /// If set to <b>true</b>, the spanned cell's width will be adjusted to accomodate all contained text.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AdjustSpannedCellsWidth
    {
      get { return FAdjustSpannedCellsWidth; }
      set { FAdjustSpannedCellsWidth = value; }
    }

    /// <summary>
    /// Gets or sets the table cell.
    /// </summary>
    /// <param name="col">Column index.</param>
    /// <param name="row">Row index.</param>
    /// <returns>The <b>TableCell</b> object that represents a cell.</returns>
    [Browsable(false)]
    public TableCell this[int col, int row]
    {
      get
      {
        if (col < 0 || col >= FColumns.Count || row < 0 || row >= FRows.Count)
          return null;
        return FRows[row][col];
      }
      set
      {
        if (col < 0 || col >= FColumns.Count || row < 0 || row >= FRows.Count)
          return;
        FRows[row][col] = value;
      }
    }

    /// <summary>
    /// Gets or sets a number of columns in the table.
    /// </summary>
    [Category("Appearance")]
    public virtual int ColumnCount
    {
      get { return Columns.Count; }
      set
      {
        int n = value - Columns.Count;
        for (int i = 0; i < n; i++)
        {
          TableColumn column = new TableColumn();
          Columns.Add(column);
        }
        while (value < Columns.Count)
          Columns.RemoveAt(Columns.Count - 1);
      }
    }

    /// <summary>
    /// Gets or sets a number of rows in the table.
    /// </summary>
    [Category("Appearance")]
    public virtual int RowCount
    {
      get { return Rows.Count; }
      set
      {
        int n = value - Rows.Count;
        for (int i = 0; i < n; i++)
        {
          TableRow row = new TableRow();
          Rows.Add(row);
        }
        while (value < Rows.Count)
          Rows.RemoveAt(Rows.Count - 1);
      }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanGrow
    {
      get { return base.CanGrow; }
      set { base.CanGrow = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanShrink
    {
      get { return base.CanShrink; }
      set { base.CanShrink = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Hyperlink Hyperlink
    {
      get { return base.Hyperlink; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new BreakableComponent BreakTo
    {
      get { return base.BreakTo; }
      set { base.BreakTo = value; }
    }

    internal TableClipboard Clipboard
    {
      get { return FClipboard; }
    }

    internal bool IsResultTable
    {
      get { return this is TableResult; }
    }

    /// <summary>
    /// Gets a table which contains the result of rendering dynamic table.
    /// </summary>
    /// <remarks>
    /// Use this property to access the result of rendering your table in dynamic mode.
    /// It may be useful if you want to center or right-align the result table on a page. 
    /// In this case, you need to add the following code at the end of your ManualBuild event handler:
    /// <code>
    /// // right-align the table
    /// Table1.ResultTable.Left = Engine.PageWidth - Table1.ResultTable.CalcWidth() - 1;
    /// </code>
    /// </remarks>
    [Browsable(false)]
    public TableResult ResultTable
    {
      get { return FResultTable; }
    }

    internal TableCellData PrintingCell
    {
      get { return FPrintingCell; }
      set { FPrintingCell = value; }
    }
    
    internal MouseMode MouseMode
    {
      get { return FMouseMode; }
      set { FMouseMode = value; }
    }

    /// <inheritdoc/>
    public override float Width
    {
      get { return base.Width; }
      set
      {
        if (IsDesigning && !FLockColumnRowChange)
        {
          foreach (TableColumn c in Columns)
          {
            c.Width += (value - base.Width) / Columns.Count;
          }
        }
        base.Width = value;
      }
    }

    /// <inheritdoc/>
    public override float Height
    {
      get { return base.Height; }
      set
      {
        if (IsDesigning && !FLockColumnRowChange)
        {
          foreach (TableRow r in Rows)
          {
            r.Height += (value - base.Height) / Rows.Count;
          }
        }
        base.Height = value;
      }
    }

    /// <inheritdoc/>
    public override bool IsSelected
    {
      get
      {
        if (Report == null)
          return false;
        return Report.Designer.SelectedObjects.IndexOf(this) != -1 || IsInternalSelected;
      }
    }

    private bool IsInternalSelected
    {
      get
      {
        if (Report == null)
          return false;
        SelectedObjectCollection selection = Report.Designer.SelectedObjects;
        return selection.Count > 0 && (
          (selection[0] is TableRow && (selection[0] as TableRow).Parent == this) ||
          (selection[0] is TableColumn && (selection[0] as TableColumn).Parent == this) ||
          (selection[0] is TableCell && (selection[0] as TableCell).Parent != null && (selection[0] as TableCell).Parent.Parent == this));
      }
    }

    internal bool LockCorrectSpans
    {
      get { return FLockCorrectSpans; }
      set { FLockCorrectSpans = value; }
    }
    #endregion

    #region Private Methods
    private delegate void DrawCellProc(FRPaintEventArgs e, TableCell cell);

    private Point GetAddressAtMousePoint(PointF pt, bool checkSpan)
    {
      Point result = new Point();
      float left = AbsLeft;
      for (int x = 0; x < Columns.Count; x++)
      {
        float width = Columns[x].Width;
        if (pt.X >= left && pt.X < left + width)
        {
          result.X = x;
          break;
        }
        left += width;
      }
      if (pt.X >= AbsRight)
        result.X = Columns.Count - 1;

      float top = AbsTop;
      for (int y = 0; y < Rows.Count; y++)
      {
        float height = Rows[y].Height;
        if (pt.Y >= top && pt.Y < top + height)
        {
          result.Y = y;
          break;
        }
        top += height;
      }
      if (pt.Y >= AbsBottom)
        result.Y = Rows.Count - 1;

      if (checkSpan)
      {
        List<Rectangle> spans = GetSpanList();
        foreach (Rectangle span in spans)
        {
          if (span.Contains(result))
          {
            result = span.Location;
            break;
          }
        }
      }

      return result;
    }

    private void SetSelection(int x1, int y1, int x2, int y2)
    {
      Rectangle rect = new Rectangle();
      rect.X = x1 < x2 ? x1 : x2;
      rect.Y = y1 < y2 ? y1 : y2;
      rect.Width = (int)Math.Abs(x1 - x2) + 1;
      rect.Height = (int)Math.Abs(y1 - y2) + 1;

      SelectedObjectCollection selection = Report.Designer.SelectedObjects;
      selection.Clear();
      if (FMouseMode == MouseMode.SelectRow)
      {
        for (int y = rect.Top; y < rect.Bottom; y++)
        {
          selection.Add(Rows[y]);
        }  
      }
      else if (FMouseMode == MouseMode.SelectColumn)
      {
        for (int x = rect.Left; x < rect.Right; x++)
        {
          selection.Add(Columns[x]);
        }  
      }
      else if (FMouseMode == MouseMode.SelectCell)
      {
        List<Rectangle> spans = GetSpanList();

        // widen selection if spans are inside
        foreach (Rectangle span in spans)
        {
          if (rect.IntersectsWith(span))
          {
            if (span.X < rect.X)
            {
              rect.Width += rect.X - span.X;
              rect.X = span.X;
            }
            if (span.Right > rect.Right)
              rect.Width += span.Right - rect.Right;
            if (span.Y < rect.Y)
            {
              rect.Height += rect.Y - span.Y;
              rect.Y = span.Y;
            }
            if (span.Bottom > rect.Bottom)
              rect.Height += span.Bottom - rect.Bottom;
          }
        }

        for (int x = rect.Left; x < rect.Right; x++)
        {
          for (int y = rect.Top; y < rect.Bottom; y++)
          {
            selection.Add(this[x, y]);
          }
        }
      }  
    }

    private void DrawCells(FRPaintEventArgs e, DrawCellProc proc)
    {
      float top = 0;
      
      for (int y = 0; y < Rows.Count; y++)
      {
        float left = 0;
        float height = Rows[y].Height;

        for (int x = 0; x < Columns.Count; x++)
        {
          TableCell cell = this[x, y];
          float width = Columns[x].Width;

          if (!IsInsideSpan(cell) && (!IsPrinting || cell.Printable))
          {
            cell.Left = left;
            cell.Top = top;
            cell.SetPrinting(IsPrinting);
            proc(e, cell);
          }
          
          left += width;
        }
        top += height;
      }
    }

    private void DrawCellsRtl(FRPaintEventArgs e, DrawCellProc proc)
    {
        float top = 0;
      
        for (int y = 0; y < Rows.Count; y++)
        {
            float left = 0;
            float height = Rows[y].Height;

            //bool thereIsColSpan = false;
            //for (int i = Columns.Count - 1; i >= 0; i--)
            //{
            //    TableCell cell = this[i, y];
            //    if (cell.ColSpan > 1)
            //    {
            //        thereIsColSpan = true;
            //    }
            //}

            for (int x = Columns.Count - 1; x >= 0; x--)
            {
                TableCell cell = this[x, y];

                bool thereIsColSpan = false;
                if (cell.ColSpan > 1)
                {
                    thereIsColSpan = true;
                }

                float width = Columns[x].Width;

                //if (thereIsColSpan)
                //{
                //    width *= cell.ColSpan - 1;
                //    left -= width;
                //}

                if (!IsInsideSpan(cell) && (!IsPrinting || cell.Printable))
                {
                    cell.Left = left;
                    cell.Top = top;
                    cell.SetPrinting(IsPrinting);
                    proc(e, cell);

                    if (thereIsColSpan)
                        width *= cell.ColSpan;

                    left += width;
                }

                //if (!thereIsColSpan)
                //    left += width;
                //else
                //    left -= width;
            }

            top += height;
        }
    }

    private void DrawFill(FRPaintEventArgs e, TableCell cell)
    {
      cell.DrawBackground(e);
    }

    private void DrawText(FRPaintEventArgs e, TableCell cell)
    {
      cell.DrawText(e);
    }

    private void DrawDesignBorders(FRPaintEventArgs e, TableCell cell)
    {
      if (cell.Fill is SolidFill && ((cell.Fill as SolidFill).Color == Color.Transparent ||
        (cell.Fill as SolidFill).Color == Color.White))
      {
        cell.DrawMarkers(e, MarkerStyle.Rectangle);
      }
    }

    private void DrawBorders(FRPaintEventArgs e, TableCell cell)
    {
      cell.Border.Draw(e, cell.AbsBounds);
    }

    private void DrawSelectedCells(FRPaintEventArgs e, TableCell cell)
    {
      if (Report.Designer.SelectedObjects.Contains(cell))
      {
        Brush brush = e.Cache.GetBrush(Color.FromArgb(128, SystemColors.Highlight));
        e.Graphics.FillRectangle(brush, cell.AbsLeft * e.ScaleX, cell.AbsTop * e.ScaleY,
          cell.Width * e.ScaleX, cell.Height * e.ScaleY);
      }
    }

    private void DrawSelectedRowsColumns(FRPaintEventArgs e)
    {
      Graphics g = e.Graphics;
      SelectedObjectCollection selection = Report.Designer.SelectedObjects;
      Brush brush = e.Cache.GetBrush(Color.FromArgb(128, SystemColors.Highlight));
      
      foreach (Base c in selection)
      {
        if (c is TableRow)
        {
          TableRow row = c as TableRow;
          g.FillRectangle(brush, AbsLeft * e.ScaleX, (row.Top + AbsTop) * e.ScaleY,
            Width * e.ScaleX, row.Height * e.ScaleY);
        }
        else if (c is TableColumn)
        {
          TableColumn col = c as TableColumn;
          g.FillRectangle(brush, (col.Left + AbsLeft) * e.ScaleX, AbsTop * e.ScaleY,
            col.Width * e.ScaleX, Height * e.ScaleY);
        }
      }
    }

    private void DrawTable(FRPaintEventArgs e)
    {
      DrawCells(e, DrawFill);
      DrawCells(e, DrawText);
      if (IsDesigning)
        DrawCells(e, DrawDesignBorders);
      DrawCells(e, DrawBorders);
      if (IsDesigning && IsSelected)
      {
        DrawCells(e, DrawSelectedCells);
        DrawSelectedRowsColumns(e);
      }
    }

    private void DrawTableRtl(FRPaintEventArgs e)
    {
        DrawCellsRtl(e, DrawFill);
        DrawCellsRtl(e, DrawText);
        if (IsDesigning)
            DrawCellsRtl(e, DrawDesignBorders);
        DrawCellsRtl(e, DrawBorders);
        if (IsDesigning && IsSelected)
        {
            DrawCellsRtl(e, DrawSelectedCells);
            DrawSelectedRowsColumns(e);
        }
    }

    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      TableBase src = source as TableBase;
      FixedRows = src.FixedRows;
      FixedColumns = src.FixedColumns;
      RepeatHeaders = src.RepeatHeaders;
      Layout = src.Layout;
      WrappedGap = src.WrappedGap;
      AdjustSpannedCellsWidth = src.AdjustSpannedCellsWidth;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (ColumnCount == 0 || RowCount == 0)
        return;
      
      FLockColumnRowChange = true;
      Width = Columns[Columns.Count - 1].Right;
      Height = Rows[Rows.Count - 1].Bottom;
      FLockColumnRowChange = false;

      base.Draw(e);
      
      // draw table Right to Left if needed
      if (Config.RightToLeft)
      {
          DrawTableRtl(e);
          // !! »«Õ¿◊¿À‹Õ¿ﬂ ¬≈–—»ﬂ !!
          //Border.Draw(e, new RectangleF(FLeftRtl - Width + AbsLeft, AbsTop, Width, Height));
          Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));
      }
      else
      {
          DrawTable(e);
          Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));
      }

      if (IsDesigning && IsSelected)
        e.Graphics.DrawImage(Res.GetImage(75), (int)(AbsLeft * e.ScaleX + 8), (int)(AbsTop * e.ScaleY - 8));
    }

    /// <inheritdoc/>
    public override bool IsVisible(FRPaintEventArgs e)
    {
      if (RowCount == 0 || ColumnCount == 0)
        return false;
      Width = Columns[Columns.Count - 1].Right;
      Height = Rows[Rows.Count - 1].Bottom;
      RectangleF objRect = new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY,
        Width * e.ScaleX + 1, Height * e.ScaleY + 1);
      return e.Graphics.IsVisible(objRect);
    }

    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      if (IsSelected && new RectangleF(AbsLeft + 8, AbsTop - 8, 16, 16).Contains(new PointF(e.X, e.Y)))
      {
        e.Handled = true;
        e.Cursor = Cursors.SizeAll;
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      if (FMouseMode == MouseMode.None)
      {
        HandleMouseHover(e);
        if (e.Handled)
        {
          e.Mode = WorkspaceMode2.Move;
          if (IsSelected)
          {
            SelectedObjectCollection selection = Report.Designer.SelectedObjects;
            if (!selection.Contains(this))
            {
              selection.Clear();
              selection.Add(this);
            }
          }
        }
        else
        {
          if (PointInObject(new PointF(e.X, e.Y)))
          {
            e.Handled = true;
            e.Mode = WorkspaceMode2.Custom;
            e.ActiveObject = this;
            FMouseMode = MouseMode.SelectCell;
            SelectedObjectCollection selection = Report.Designer.SelectedObjects;

            if (e.Button == MouseButtons.Left)
            {
              FStartSelectionPoint = GetAddressAtMousePoint(new PointF(e.X, e.Y), true);
              TableCell cell = this[FStartSelectionPoint.X, FStartSelectionPoint.Y];

              if (e.ModifierKeys == Keys.Shift)
              {
                // toggle selection
                if (selection.Contains(cell))
                {
                  if (selection.Count > 1)
                    selection.Remove(cell);
                }
                else
                  selection.Add(cell);
              }
              else
              {
                selection.Clear();
                selection.Add(cell);
              }
            }
            else if (e.Button == MouseButtons.Right)
            {
              Point selectionPoint = GetAddressAtMousePoint(new PointF(e.X, e.Y), true);
              Rectangle selectionRect = GetSelectionRect();
              if (!selectionRect.Contains(selectionPoint))
              {
                selection.Clear();
                selection.Add(this[selectionPoint.X, selectionPoint.Y]);
              }
            }
          }
        }
      }
      else if (e.Button == MouseButtons.Left)
      {
        FStartSelectionPoint = GetAddressAtMousePoint(new PointF(e.X, e.Y), false);
        if (FMouseMode == MouseMode.SelectColumn)
          SetSelection(FStartSelectionPoint.X, 0, FStartSelectionPoint.X, Rows.Count - 1);
        else if (FMouseMode == MouseMode.SelectRow)
          SetSelection(0, FStartSelectionPoint.Y, Columns.Count - 1, FStartSelectionPoint.Y);
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      base.HandleMouseMove(e);
      if (!e.Handled && e.Button == MouseButtons.None)
      {
        PointF point = new PointF(e.X, e.Y);
        FMouseMode = MouseMode.None;
        // don't process if mouse is over move area
        HandleMouseHover(e);
        if (e.Handled)
        {
          e.Handled = false;
          return;
        }

        // check column resize or select
        if (IsSelected)
        {
          float left = AbsLeft;
          for (int x = 0; x < Columns.Count; x++)
          {
            float width = Columns[x].Width;
            left += width;
            if (point.Y > AbsTop && point.Y < AbsBottom && point.X > left - 3 && point.X < left + 3)
            {
              // column resize
              PointF pt = new PointF(e.X + 3, e.Y);
              Point pt1 = GetAddressAtMousePoint(pt, false);
              Point pt2 = GetAddressAtMousePoint(pt, true);
              // check if we are in span
              if (pt1 == pt2)
              {
                FMouseMode = MouseMode.ResizeColumn;
                e.Cursor = Cursors.VSplit;
                e.Data = Columns[x];
                break;
              }
            }
            else if (point.Y > AbsTop - 8 && point.Y < AbsTop + 2 && point.X > left - width && point.X < left)
            {
              // column select
              FMouseMode = MouseMode.SelectColumn;
              e.Cursor = curDownArrow;
              e.Data = Columns[x];
              break;
            }
          }

          // check row resize or select
          if (FMouseMode == MouseMode.None)
          {
            float top = AbsTop;
            for (int y = 0; y < Rows.Count; y++)
            {
              float height = Rows[y].Height;
              top += height;
              if (point.X > AbsLeft && point.X < AbsRight && point.Y > top - 3 && point.Y < top + 3)
              {
                // row resize
                PointF pt = new PointF(e.X, e.Y + 3);
                Point pt1 = GetAddressAtMousePoint(pt, false);
                Point pt2 = GetAddressAtMousePoint(pt, true);
                // check if we are in span
                if (pt1 == pt2)
                {
                  FMouseMode = MouseMode.ResizeRow;
                  e.Cursor = Cursors.HSplit;
                  e.Data = Rows[y];
                  break;
                }
              }
              else if (point.X > AbsLeft - 8 && point.X < AbsLeft + 2 && point.Y > top - height && point.Y < top)
              {
                // row select
                FMouseMode = MouseMode.SelectRow;
                e.Cursor = curRightArrow;
                e.Data = Rows[y];
                break;
              }
            }
          }
        }

        if (FMouseMode != MouseMode.None)
        {
          e.Handled = true;
          e.Mode = WorkspaceMode2.Custom;
        }
      }
      else if (e.Mode == WorkspaceMode2.Custom && e.Button == MouseButtons.Left)
      {
        switch (FMouseMode)
        {
          case MouseMode.SelectColumn:
            SetSelection(FStartSelectionPoint.X, 0,
              GetAddressAtMousePoint(new PointF(e.X, e.Y), false).X, Rows.Count - 1);
            break;

          case MouseMode.SelectRow:
            SetSelection(0, FStartSelectionPoint.Y,
              Columns.Count - 1, GetAddressAtMousePoint(new PointF(e.X, e.Y), false).Y);
            break;

          case MouseMode.SelectCell:
            Point pt = GetAddressAtMousePoint(new PointF(e.X, e.Y), false);
            SetSelection(FStartSelectionPoint.X, FStartSelectionPoint.Y, pt.X, pt.Y);
            break;

          case MouseMode.ResizeColumn:
            TableColumn col = e.Data as TableColumn;
            col.Width += e.Delta.X;
            if ((e.ModifierKeys & Keys.Control) != 0 && col.Index < ColumnCount - 1)
            {
              TableColumn nextCol = Columns[col.Index + 1];
              nextCol.Width -= e.Delta.X;
            }
            if (col.Width <= 0)
              col.Width = 1;
            break;

          case MouseMode.ResizeRow:
            TableRow row = e.Data as TableRow;
            row.Height += e.Delta.Y;
            if ((e.ModifierKeys & Keys.Control) != 0 && row.Index < RowCount - 1)
            {
              TableRow nextRow = Rows[row.Index + 1];
              nextRow.Height -= e.Delta.Y;
            }
            if (row.Height <= 0)
              row.Height = 1;
            break;
        }
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (FMouseMode == MouseMode.ResizeRow)
      {
        // update band's height
        if (Parent is BandBase)
          (Parent as BandBase).FixHeight();
        Report.Designer.SetModified(null, "Size", (e.Data as TableRow).Name);
      }
      if (FMouseMode == MouseMode.ResizeColumn)
        Report.Designer.SetModified(null, "Size", (e.Data as TableColumn).Name);
      FMouseMode = MouseMode.None;
    }

    /// <inheritdoc/>
    public override void HandleKeyDown(Control sender, KeyEventArgs e)
    {
      SelectedObjectCollection selection = Report.Designer.SelectedObjects;
      if (!IsSelected || !(selection[0] is TableCell))
        return;

      TableCell topCell = selection[0] as TableCell;
      int left = topCell.Address.X;
      int top = topCell.Address.Y;
      bool selectionChanged = false;

      switch (e.KeyCode)
      {
        case Keys.Enter:
          topCell.HandleKeyDown(sender, e);
          break;
          
        case Keys.Delete:
          foreach (Base c in selection)
          {
            if (c is TableCell)
              (c as TableCell).Text = "";
          }
          Report.Designer.SetModified(null, "Change", Name);
          break;
          
        case Keys.Up:
          top--;
          selectionChanged = true;
          break;
          
        case Keys.Down:
          top += topCell.RowSpan;
          selectionChanged = true;
          break;
          
        case Keys.Left:
          left--;
          selectionChanged = true;
          break;
          
        case Keys.Right:
          left += topCell.ColSpan;
          selectionChanged = true;
          break;
      }

      if (selectionChanged)
      {
        if (left < 0)
          left = 0;
        if (left >= Columns.Count)
          left = Columns.Count - 1;
        if (top < 0)
          top = 0;
        if (top >= Rows.Count)
          top = Rows.Count - 1;
        
        FMouseMode = MouseMode.SelectCell;
        SetSelection(left, top, left, top);
        Report.Designer.SelectionChanged(null);
      }
      e.Handled = true;
    }

    internal void SetResultTable(TableResult table)
    {
      FResultTable = table;
    }

    internal TableCellData GetCellData(int col, int row)
    {
      if (col < 0 || col >= FColumns.Count || row < 0 || row >= FRows.Count)
        return null;
      return FRows[row].CellData(col);
    }

    internal Rectangle GetSelectionRect()
    {
      SelectedObjectCollection selection = Report.Designer.SelectedObjects;
      int minX = 1000;
      int minY = 1000;
      int maxX = 0;
      int maxY = 0;
      foreach (Base c in selection)
      {
        if (c is TableCell)
        {
          TableCell cell = c as TableCell;
          Point a = cell.Address;
          if (a.X < minX)
            minX = a.X;
          if (a.X > maxX)
            maxX = a.X;
          if (a.Y < minY)
            minY = a.Y;
          if (a.Y > maxY)
            maxY = a.Y;
        }
      }
      return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
    }

    internal List<Rectangle> GetSpanList()
    {
      if (FSpanList == null)
      {
        FSpanList = new List<Rectangle>();
        for (int y = 0; y < Rows.Count; y++)
        {
          for (int x = 0; x < Columns.Count; x++)
          {
            TableCellData cell = GetCellData(x, y);
            if (cell.ColSpan > 1 || cell.RowSpan > 1)
              FSpanList.Add(new Rectangle(x, y, cell.ColSpan, cell.RowSpan));
          }
        }
      }

      return FSpanList;
    }

    internal void ResetSpanList()
    {
      FSpanList = null;
    }

    internal bool IsInsideSpan(TableCell cell)
    {
      Point address = cell.Address;
      List<Rectangle> spans = GetSpanList();
      foreach (Rectangle span in spans)
      {
        if (span.Contains(address) && span.Location != address)
          return true;
      }
      return false;
    }

    internal void CorrectSpansOnRowChange(int rowIndex, int correct)
    {
      if (FLockCorrectSpans || (correct == 1 && rowIndex >= Rows.Count))
        return;

      for (int y = 0; y < rowIndex; y++)
      {
        for (int x = 0; x < Columns.Count; x++)
        {
          TableCellData cell = GetCellData(x, y);
          if (rowIndex < y + cell.RowSpan)
            cell.RowSpan += correct;
        }
      }

      ResetSpanList();
    }

    internal void CorrectSpansOnColumnChange(int columnIndex, int correct)
    {
      if (FLockCorrectSpans || (correct == 1 && columnIndex >= Columns.Count))
        return;

      for (int y = 0; y < Rows.Count; y++)
      {
        for (int x = 0; x < columnIndex; x++)
        {
          TableCellData cell = GetCellData(x, y);
          if (columnIndex < x + cell.ColSpan)
            cell.ColSpan += correct;
        }
        
        // correct cells
        Rows[y].CorrectCellsOnColumnChange(columnIndex, correct);
      }

      ResetSpanList();
    }

    /// <summary>
    /// Creates unique names for all table elements such as rows, columns, cells.
    /// </summary>
    public void CreateUniqueNames()
    {
      if (Report == null)
        return;
      FastNameCreator nameCreator = new FastNameCreator(Report.AllNamedObjects);

      foreach (TableRow row in Rows)
      {
        if (String.IsNullOrEmpty(row.Name))
          nameCreator.CreateUniqueName(row);
      }
      foreach (TableColumn column in Columns)
      {
        if (String.IsNullOrEmpty(column.Name))
          nameCreator.CreateUniqueName(column);
      }
      for (int y = 0; y < Rows.Count; y++)
      {
        for (int x = 0; x < Columns.Count; x++)
        {
          TableCell cell = this[x, y];
          if (String.IsNullOrEmpty(cell.Name))
          {
            nameCreator.CreateUniqueName(cell);
            cell.Font = DrawUtils.DefaultReportFont;
          }
          if (cell.Objects != null)
          {
            foreach (ReportComponentBase obj in cell.Objects)
            {
              if (String.IsNullOrEmpty(obj.Name))
                nameCreator.CreateUniqueName(obj);
            }
          }
        }
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      TableBase c = writer.DiffObject as TableBase;
      FSerializingToPreview = writer.SerializeTo == SerializeTo.Preview;
      base.Serialize(writer);

      if (FixedRows != c.FixedRows)
        writer.WriteInt("FixedRows", FixedRows);
      if (FixedColumns != c.FixedColumns)
        writer.WriteInt("FixedColumns", FixedColumns);
      if (RepeatHeaders != c.RepeatHeaders)
        writer.WriteBool("RepeatHeaders", RepeatHeaders);
      if (Layout != c.Layout)
        writer.WriteValue("Layout", Layout);
      if (WrappedGap != c.WrappedGap)
        writer.WriteFloat("WrappedGap", WrappedGap);
      if (AdjustSpannedCellsWidth != c.AdjustSpannedCellsWidth)
        writer.WriteBool("AdjustSpannedCellsWidth", AdjustSpannedCellsWidth);
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      Width = TableColumn.DefaultWidth * 5;
      Height = TableRow.DefaultHeight * 5;
      RowCount = 5;
      ColumnCount = 5;
    }

    /// <inheritdoc/>
    public override void OnAfterInsert(InsertFrom source)
    {
      CreateUniqueNames();
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new TableObjectMenu(Report.Designer);
    }

    internal virtual ContextMenuBar GetColumnContextMenu(TableColumn column)
    {
      return null;
    }

    internal virtual ContextMenuBar GetRowContextMenu(TableRow row)
    {
      return null;
    }

    internal virtual ContextMenuBar GetCellContextMenu(TableCell cell)
    {
      return null;
    }

    internal virtual SmartTagBase GetCellSmartTag(TableCell cell)
    {
      return null;
    }

    internal virtual void HandleCellDoubleClick(TableCell cell)
    {
      // do nothing
    }

    internal void EmulateOuterBorder()
    {
      for (int y = 0; y < RowCount; y++)
      {
        for (int x = 0; x < ColumnCount; x++)
        {
          TableCell cell = this[x, y];
          if (x == 0 && (Border.Lines & BorderLines.Left) != 0)
          {
            cell.Border.LeftLine.Assign(Border.LeftLine);
            cell.Border.Lines |= BorderLines.Left;
          }
          if (x + cell.ColSpan == ColumnCount && (Border.Lines & BorderLines.Right) != 0)
          {
            cell.Border.RightLine.Assign(Border.RightLine);
            cell.Border.Lines |= BorderLines.Right;
          }
          if (y == 0 && (Border.Lines & BorderLines.Top) != 0)
          {
            cell.Border.TopLine.Assign(Border.TopLine);
            cell.Border.Lines |= BorderLines.Top;
          }
          if (y + cell.RowSpan == RowCount && (Border.Lines & BorderLines.Bottom) != 0)
          {
            cell.Border.BottomLine.Assign(Border.BottomLine);
            cell.Border.Lines |= BorderLines.Bottom;
          }
        }
      }
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      return child is TableRow || child is TableColumn || child is TableCell;
    }

    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      foreach (TableColumn column in Columns)
      {
        if (!FSerializingToPreview || column.Visible)
          list.Add(column);
      }
      foreach (TableRow row in Rows)
      {
        if (!FSerializingToPreview || row.Visible)
          list.Add(row);
      }
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      if (child is TableRow)
        Rows.Add(child as TableRow);
      else if (child is TableColumn)
        Columns.Add(child as TableColumn);
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
      if (child is TableRow)
        Rows.Remove(child as TableRow);
      else if (child is TableColumn)
        Columns.Remove(child as TableColumn);
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      if (child is TableColumn)
        return Columns.IndexOf(child as TableColumn);
      else if (child is TableRow)
        return Rows.IndexOf(child as TableRow);
      return 0;  
    }

    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      FLockCorrectSpans = true;

      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (child is TableColumn)
        {
          if (order > Columns.Count)
            order = Columns.Count;
          if (oldOrder <= order)
            order--;
          Columns.Remove(child as TableColumn);
          Columns.Insert(order, child as TableColumn);
        }
        else if (child is TableRow)
        {
          if (order > Rows.Count)
            order = Rows.Count;
          if (oldOrder <= order)
            order--;
          Rows.Remove(child as TableRow);
          Rows.Insert(order, child as TableRow);
        }
      }
  
      FLockCorrectSpans = false;
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();

      foreach (TableRow row in Rows)
      {
        row.SaveState();
      }
      foreach (TableColumn column in Columns)
      {
        column.SaveState();
      }
      for (int y = 0; y < Rows.Count; y++)
      {
        for (int x = 0; x < Columns.Count; x++)
        {
          this[x, y].SaveState();
        }
      }

      CanGrow = true;
      CanShrink = true;
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      base.RestoreState();
      
      foreach (TableRow row in Rows)
      {
        row.RestoreState();
      }
      foreach (TableColumn column in Columns)
      {
        column.RestoreState();
      }
      for (int y = 0; y < Rows.Count; y++)
      {
        for (int x = 0; x < Columns.Count; x++)
        {
          this[x, y].RestoreState();
        }
      }
    }

    /// <summary>
    /// Calculates and returns the table width, in pixels.
    /// </summary>
    public float CalcWidth()
    {
      // first pass, calc non-spanned cells
      for (int x = 0; x < Columns.Count; x++)
      {
        TableColumn column = Columns[x];
        if (!column.AutoSize)
          continue;
        float columnWidth = IsDesigning ? 16 : -1;

        // calc the max column width
        for (int y = 0; y < Rows.Count; y++)
        {
          TableCellData cell = GetCellData(x, y);
          if (cell.ColSpan == 1)
          {
            float cellWidth = cell.CalcWidth();
            if (cellWidth > columnWidth)
              columnWidth = cellWidth;
          }
        }

        // update column width
        if (columnWidth != -1)
          column.Width = columnWidth;
      }

      // second pass, calc spanned cells
      for (int x = 0; x < Columns.Count; x++)
      {
        Columns[x].MinimumBreakWidth = 0;
        for (int y = 0; y < Rows.Count; y++)
        {
          TableCellData cell = GetCellData(x, y);
          if (cell.ColSpan > 1)
          {
            float cellWidth = cell.CalcWidth();
            if (AdjustSpannedCellsWidth && cellWidth > Columns[x].MinimumBreakWidth)
              Columns[x].MinimumBreakWidth = cellWidth;

            // check that spanned columns have enough width
            float columnsWidth = 0;
            for (int i = 0; i < cell.ColSpan; i++)
            {
              columnsWidth += Columns[x + i].Width;
            }

            // if cell is bigger than sum of column width, increase the last column width
            TableColumn lastColumn = Columns[x + cell.ColSpan - 1];
            if (columnsWidth < cellWidth && lastColumn.AutoSize)
              lastColumn.Width += cellWidth - columnsWidth;
          }
        }
      }

      // finally, calculate the table width
      float width = 0;
      for (int i = 0; i < Columns.Count; i++)
      {
        width += Columns[i].Width;
      }

      FLockColumnRowChange = true;
      Width = width;
      FLockColumnRowChange = false;
      return width;
    }

    /// <inheritdoc/>
    public override float CalcHeight()
    {
      if (ColumnCount * RowCount > 1000)
        Config.ReportSettings.OnProgress(Report, Res.Get("ComponentsMisc,Table,CalcBounds"), 0, 0);
      
      // calc width
      CalcWidth();

      // first pass, calc non-spanned cells
      for (int y = 0; y < Rows.Count; y++)
      {
        TableRow row = Rows[y];
        if (!row.AutoSize)
          continue;
        float rowHeight = IsDesigning ? 16 : -1;

        // calc the max row height
        for (int x = 0; x < Columns.Count; x++)
        {
          TableCellData cell = GetCellData(x, y);

          if (cell.RowSpan == 1)
          {
            float cellHeight = cell.CalcHeight(cell.Width);
            if (cellHeight > rowHeight)
              rowHeight = cellHeight;
          }
        }

        // update row height
        if (rowHeight != -1)
          row.Height = rowHeight;
      }

      // second pass, calc spanned cells
      for (int y = 0; y < Rows.Count; y++)
      {
        for (int x = 0; x < Columns.Count; x++)
        {
          TableCellData cell = GetCellData(x, y);
          if (cell.RowSpan > 1)
          {
            float cellHeight = cell.CalcHeight(cell.Width);

            // check that spanned rows have enough height
            float rowsHeight = 0;
            for (int i = 0; i < cell.RowSpan; i++)
            {
              rowsHeight += Rows[y + i].Height;
            }

            // if cell is bigger than sum of row heights, increase the last row height
            TableRow lastRow = Rows[y + cell.RowSpan - 1];
            if (rowsHeight < cellHeight && lastRow.AutoSize)
              lastRow.Height += cellHeight - rowsHeight;
          }
        }
      }

      // finally, calculate the table height
      float height = 0;
      for (int i = 0; i < Rows.Count; i++)
      {
        height += Rows[i].Visible ? Rows[i].Height : 0;
      }

      return height;
    }

    /// <inheritdoc/>
    public override bool Break(BreakableComponent breakTo)
    {
      if (Rows.Count == 0)
        return true;
      if (Height < Rows[0].Height)
        return false;
      TableBase tableTo = breakTo as TableBase;
      if (tableTo == null)
        return true;

      // get the span list
      List<Rectangle> spans = GetSpanList();

      // find the break row index
      int breakRowIndex = 0;
      float rowsHeight = 0;
      while (breakRowIndex < Rows.Count && rowsHeight + Rows[breakRowIndex].Height < Height)
      {
        rowsHeight += Rows[breakRowIndex].Height;
        breakRowIndex++;
      }

      // break the spans
      foreach (Rectangle span in spans)
      {
        if (span.Top < breakRowIndex && span.Bottom > breakRowIndex)
        {
          TableCell cell = this[span.Left, span.Top];
          TableCell cellTo = tableTo[span.Left, span.Top];

          // update cell spans
          cell.RowSpan = breakRowIndex - span.Top;
          cellTo.RowSpan = span.Bottom - breakRowIndex;

          // break the cell
          if (!cell.Break(cellTo))
            cell.Text = "";
          tableTo[span.Left, span.Top] = new TableCell();
          tableTo[span.Left, breakRowIndex] = cellTo;
        }
      }

      // delete last rows until all rows fit
      while (breakRowIndex < Rows.Count)
      {
        Rows.RemoveAt(Rows.Count - 1);
      }

      // delete first rows of the breakTo
      for (int i = 0; i < breakRowIndex; i++)
      {
        tableTo.Rows.RemoveAt(0);
      }

      return true;
    }

    private List<TableCellData> GetAggregateCells(TableCell aggregateCell)
    {
      List<TableCellData> list = new List<TableCellData>();

      // columnIndex, rowIndex is a place where we will print a result.
      // To collect aggregate values that will be used to calculate a result, we need to go
      // to the left and top from this point and collect every cell which OriginalCell is equal to
      // the aggregateCell value. We have to stop when we meet the same row or column.

      int columnIndex = PrintingCell.Address.X;
      int rowIndex = PrintingCell.Address.Y;
      TableColumn startColumn = ResultTable.Columns[columnIndex];
      TableRow startRow = ResultTable.Rows[rowIndex];
      TableColumn aggregateColumn = Columns[aggregateCell.Address.X];
      TableRow aggregateRow = Rows[aggregateCell.Address.Y];

      // check if result is in the same row/column as aggregate cell
      bool sameRow = startRow.OriginalComponent == aggregateRow.OriginalComponent;
      bool sameColumn = startColumn.OriginalComponent == aggregateColumn.OriginalComponent;

      for (int y = rowIndex; y >= 0; y--)
      {
        if (y != rowIndex && ResultTable.Rows[y].OriginalComponent == startRow.OriginalComponent)
          break;

        for (int x = columnIndex; x >= 0; x--)
        {
          if (x != columnIndex && ResultTable.Columns[x].OriginalComponent == startColumn.OriginalComponent)
            break;

          TableCellData cell = ResultTable.GetCellData(x, y);
          if (cell.OriginalCell == aggregateCell)
            list.Add(cell);

          if (sameColumn)
            break;
        }

        if (sameRow)
          break;
      }

      return list;
    }

    /// <summary>
    /// Calculates a sum of values in a specified cell.
    /// </summary>
    /// <param name="aggregateCell">The cell.</param>
    /// <returns>The <b>object</b> that contains calculated value.</returns>
    /// <remarks>
    /// This method can be called from the <b>ManualBuild</b> event handler only.
    /// </remarks>
    public object Sum(TableCell aggregateCell)
    {
      List<TableCellData> list = GetAggregateCells(aggregateCell);
      Variant result = 0;
      bool firstTime = true;

      foreach (TableCellData cell in list)
      {
        if (cell.Value != null)
        {
          Variant varValue = new Variant(cell.Value);
          if (firstTime)
            result = varValue;
          else
            result += varValue;
            
          firstTime = false;  
        }  
      }

      return result.Value;
    }

    /// <summary>
    /// Calculates a minimum of values in a specified cell.
    /// </summary>
    /// <param name="aggregateCell">The cell.</param>
    /// <returns>The <b>object</b> that contains calculated value.</returns>
    /// <remarks>
    /// This method can be called from the <b>ManualBuild</b> event handler only.
    /// </remarks>
    public object Min(TableCell aggregateCell)
    {
      List<TableCellData> list = GetAggregateCells(aggregateCell);
      Variant result = float.PositiveInfinity;
      bool firstTime = true;

      foreach (TableCellData cell in list)
      {
        if (cell.Value != null)
        {
          Variant varValue = new Variant(cell.Value);
          if (firstTime || varValue < result)
            result = varValue;
            
          firstTime = false;  
        }
      }

      return result.Value;
    }

    /// <summary>
    /// Calculates a maximum of values in a specified cell.
    /// </summary>
    /// <param name="aggregateCell">The cell.</param>
    /// <returns>The <b>object</b> that contains calculated value.</returns>
    /// <remarks>
    /// This method can be called from the <b>ManualBuild</b> event handler only.
    /// </remarks>
    public object Max(TableCell aggregateCell)
    {
      List<TableCellData> list = GetAggregateCells(aggregateCell);
      Variant result = float.NegativeInfinity;
      bool firstTime = true;

      foreach (TableCellData cell in list)
      {
        if (cell.Value != null)
        {
          Variant varValue = new Variant(cell.Value);
          if (firstTime || varValue > result)
            result = varValue;

          firstTime = false;
        }
      }

      return result.Value;
    }

    /// <summary>
    /// Calculates an average of values in a specified cell.
    /// </summary>
    /// <param name="aggregateCell">The cell.</param>
    /// <returns>The <b>object</b> that contains calculated value.</returns>
    /// <remarks>
    /// This method can be called from the <b>ManualBuild</b> event handler only.
    /// </remarks>
    public object Avg(TableCell aggregateCell)
    {
      List<TableCellData> list = GetAggregateCells(aggregateCell);
      Variant result = 0;
      int count = 0;
      bool firstTime = true;

      foreach (TableCellData cell in list)
      {
        if (cell.Value != null)
        {
          Variant varValue = new Variant(cell.Value);
          if (firstTime)
            result = varValue;
          else
            result += varValue;
          
          count++;
          firstTime = false;
        }
      }

      return result / (count == 0 ? 1 : count);
    }

    /// <summary>
    /// Calculates number of repeats of a specified cell.
    /// </summary>
    /// <param name="aggregateCell">The cell.</param>
    /// <returns>The <b>object</b> that contains calculated value.</returns>
    /// <remarks>
    /// This method can be called from the <b>ManualBuild</b> event handler only.
    /// </remarks>
    public object Count(TableCell aggregateCell)
    {
      List<TableCellData> list = GetAggregateCells(aggregateCell);
      int count = 0;

      foreach (TableCellData cell in list)
      {
        if (cell.Value != null)
          count++;
      }

      return count;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TableBase"/> class.
    /// </summary>
    public TableBase()
    {
      FRows = new TableRowCollection(this);
      FColumns = new TableColumnCollection(this);
      FClipboard = new TableClipboard(this);
      FStyles = new TableStyleCollection();
      FRepeatHeaders = true;
      CanGrow = true;
      CanShrink = true;
    }
  }
}
