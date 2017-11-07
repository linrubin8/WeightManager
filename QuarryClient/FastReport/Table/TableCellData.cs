using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FastReport.Table
{
  internal class TableCellData : IDisposable
  {
    private string FText;
    private object FValue;
    private string FHyperlinkValue;
    private int FColSpan;
    private int FRowSpan;
    private ReportComponentCollection FObjects;
    private TableCell FStyle;
    private TableCell FCell;
    private TableBase FTable;
    private Point FAddress;

    public TableBase Table
    {
      get { return FTable; }
      set { FTable = value; }
    }

    public ReportComponentCollection Objects
    {
      get { return FObjects; }
      set { FObjects = value; }
    }

    public string Text
    {
      get { return FText; }
      set { FText = value; }
    }
    
    public object Value
    {
      get { return FValue; }
      set { FValue = value; }
    }

    public string HyperlinkValue
    {
      get { return FHyperlinkValue; }
      set { FHyperlinkValue = value; }
    }

    public int ColSpan
    {
      get { return FColSpan; }
      set 
      {
        if (FColSpan != value)
        {
          float oldWidth = Width;
          FColSpan = value;
          float newWidth = Width;
          UpdateLayout(newWidth, Height, newWidth - oldWidth, 0);
          
          if (Table != null)
            Table.ResetSpanList();
        }  
      }
    }

    public int RowSpan
    {
      get { return FRowSpan; }
      set 
      {
        if (FRowSpan != value)
        {
          float oldHeight = Height;
          FRowSpan = value;
          float newHeight = Height;
          UpdateLayout(Width, newHeight, 0, newHeight - oldHeight);

          if (Table != null)
            Table.ResetSpanList();
        }
      }
    }
    
    public Point Address
    {
      get { return FAddress; }
      set { FAddress = value; }
    }
    
    public TableCell Cell
    {
      get 
      {
        if (Table.IsResultTable)
        {
          TableCell cell = FStyle;
          if (cell == null)
            cell = Table.Styles.DefaultStyle;
          
          if (FCell != null)
          {
            cell.Alias = FCell.Alias;
            cell.OriginalComponent = FCell.OriginalComponent;
          }
          cell.CellData = this;
          cell.Hyperlink.Value = HyperlinkValue;
          return cell;
        }
        
        if (FCell == null)
        {
          FCell = new TableCell();
          FCell.CellData = this;
        }
        return FCell; 
      }
    }

    public TableCell Style
    {
      get { return FStyle; }
    }
    
    public TableCell OriginalCell
    {
      get { return FCell; }
    }
    
    public float Width
    {
      get 
      {
        if (Table == null)
          return 0;

        float result = 0;
        for (int i = 0; i < ColSpan; i++)
        {
          if (Address.X + i < Table.Columns.Count)
            result += Table.Columns[Address.X + i].Width;
        }
        return result;
      }
    }
    
    public float Height
    {
      get
      {
        if (Table == null)
          return 0;

        float result = 0;
        for (int i = 0; i < RowSpan; i++)
        {
          if (Address.Y + i < Table.Rows.Count)
            result += Table.Rows[Address.Y + i].Height;
        }
        return result;
      }
    }

    // this method is called when we load the table
    public void AttachCell(TableCell cell)
    {
      if (FCell != null)
      {
        FCell.CellData = null;
        FCell.Dispose();
      }  

      Text = cell.Text;
      ColSpan = cell.ColSpan;
      RowSpan = cell.RowSpan;
      FObjects = cell.Objects;
      FStyle = null;
      FCell = cell;
      cell.CellData = this;
    }

    // this method is called when we copy cells or clone columns/rows in a designer
    public void Assign(TableCellData source)
    {
      AttachCell(source.Cell);
    }
    
    // this method is called when we print a table. We should create a copy of the cell and set the style.
    public void RunTimeAssign(TableCell cell, bool copyChildren)
    {
      Text = cell.Text;
      Value = cell.Value;
      HyperlinkValue = cell.Hyperlink.Value;
      // don't copy ColSpan, RowSpan - they will be handled in the TableHelper.
      //ColSpan = cell.ColSpan;
      //RowSpan = cell.RowSpan;
      
      // clone objects
      FObjects = null;
      if (cell.Objects != null && copyChildren)
      {
        FObjects = new ReportComponentCollection();
        foreach (ReportComponentBase obj in cell.Objects)
        {
          if (obj.Visible)
          {
            ReportComponentBase cloneObj = Activator.CreateInstance(obj.GetType()) as ReportComponentBase;
            cloneObj.AssignAll(obj);
            FObjects.Add(cloneObj);
          }
        }
      }

      // add the cell to the style list. If the list contains such style,
      // return the existing style; in other case, create new style based
      // on the given cell.
      SetStyle(cell);
      // FCell is used to reference the original cell. It is necessary to use Alias, OriginalComponent
      FCell = cell;

      // reset object's location as if we set ColSpan and RowSpan to 1.
      // It is nesessary when printing spanned cells because the span of such cells will be corrected
      // when print new rows/columns and thus will move cell objects.
      if (FObjects != null)
        UpdateLayout(Width, Height, Width - cell.CellData.Width, Height - cell.CellData.Height);
    }

    public void SetStyle(TableCell style)
    {
      FStyle = Table.Styles.Add(style);
    }
    
    public void Dispose()
    {
      if (FStyle == null && FCell != null)
        FCell.Dispose();
      FCell = null;
      FStyle = null;
    }
    
    public float CalcWidth()
    {
      TableCell cell = Cell;
      cell.SetReport(Table.Report);
      return cell.CalcWidth();  
    }
    
    public float CalcHeight(float width)
    {
      TableCell cell = Cell;
      cell.SetReport(Table.Report);
      cell.Width = width;
      float cellHeight = cell.CalcHeight();
      
      if (FObjects != null)
      {
// pasted from BandBase.cs

        // sort objects by Top
        ReportComponentCollection sortedObjects = FObjects.SortByTop();

        // calc height of each object
        float[] heights = new float[sortedObjects.Count];
        for (int i = 0; i < sortedObjects.Count; i++)
        {
          ReportComponentBase obj = sortedObjects[i];
          float height = obj.Height;
          if (obj.CanGrow || obj.CanShrink)
          {
            float height1 = obj.CalcHeight();
            if ((obj.CanGrow && height1 > height) || (obj.CanShrink && height1 < height))
              height = height1;
          }
          heights[i] = height;
        }

        // calc shift amounts
        float[] shifts = new float[sortedObjects.Count];
        for (int i = 0; i < sortedObjects.Count; i++)
        {
          ReportComponentBase parent = sortedObjects[i];
          float shift = heights[i] - parent.Height;
          if (shift == 0)
            continue;

          for (int j = i + 1; j < sortedObjects.Count; j++)
          {
            ReportComponentBase child = sortedObjects[j];
            if (child.ShiftMode == ShiftMode.Never)
              continue;

            if (child.Top >= parent.Bottom - 1e-4)
            {
              if (child.ShiftMode == ShiftMode.WhenOverlapped &&
                (child.Left > parent.Right - 1e-4 || parent.Left > child.Right - 1e-4))
                continue;

              float parentShift = shifts[i];
              float childShift = shifts[j];
              if (shift > 0)
                childShift = Math.Max(shift + parentShift, childShift);
              else
                childShift = Math.Min(shift + parentShift, childShift);
              shifts[j] = childShift;
            }
          }
        }

        // update location and size of each component, calc max height
        float maxHeight = 0;
        for (int i = 0; i < sortedObjects.Count; i++)
        {
          ReportComponentBase obj = sortedObjects[i];
          obj.Height = heights[i];
          obj.Top += shifts[i];
          if (obj.Bottom > maxHeight)
            maxHeight = obj.Bottom;
        }
        
        if (cellHeight < maxHeight)
          cellHeight = maxHeight;

        // perform grow to bottom
        foreach (ReportComponentBase obj in FObjects)
        {
          if (obj.GrowToBottom)
            obj.Height = cellHeight - obj.Top;
        }

// -----------------------

      }
      
      return cellHeight;
    }
    
    public void UpdateLayout(float width, float height, float dx, float dy)
    {
      if (Objects == null)
        return;
      
      TableCell cell = Cell;
      cell.Width = width;
      cell.Height = height;
      cell.UpdateLayout(dx, dy);
    }
    
    public TableCellData()
    {
      FColSpan = 1;
      FRowSpan = 1;
      FText = "";
      FHyperlinkValue = "";
    }
  }
}
