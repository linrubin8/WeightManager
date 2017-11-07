using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Table
{
  internal class TableClipboard
  {
    #region Fields
    private TableBase FTable;
    private List<TableColumn> FColumns;
    private List<TableRow> FRows;
    private TableCell[,] FCells;
    private Size FCellsSize;
    #endregion

    #region Properties
    public bool CanPasteColumns
    {
      get { return FColumns.Count > 0; }
    }
    
    public bool CanPasteRows
    {
      get { return FRows.Count > 0; }
    }

    public bool CanPasteCells
    {
      get { return !CanPasteColumns && FCells != null; }
    }
    #endregion
    
    #region Private Methods
    private void ClearColumns()
    {
      FColumns.Clear();
      FCells = null;
    }
    
    private void ClearRows()
    {
      FRows.Clear();
    }

    private void CutCells(Rectangle selection, bool remove)
    {
      FCells = new TableCell[selection.Width, selection.Height];
      FCellsSize = selection.Size;
      for (int x = 0; x < selection.Width; x++)
      {
        for (int y = 0; y < selection.Height; y++)
        {
          FCells[x, y] = FTable[x + selection.X, y + selection.Y];
          if (remove)
            FTable[x + selection.X, y + selection.Y] = new TableCell();
        }
      }
    }
    #endregion

    #region Public Methods
    public void CutColumns(TableColumn[] columns)
    {
      ClearColumns();
      FCells = new TableCell[columns.Length, FTable.RowCount];

      for (int x = 0; x < columns.Length; x++)
      {
        FColumns.Add(columns[x]);
        for (int y = 0; y < FTable.RowCount; y++)
        {
          FCells[x, y] = FTable[columns[x].Index, y];
        }
      }
      foreach (TableColumn c in columns)
      {
        FTable.Columns.Remove(c);
      }
    }

    public void PasteColumns(int index)
    {
      for (int x = 0; x < FColumns.Count; x++)
      {
        FTable.Columns.Insert(index + x, FColumns[x]);
        for (int y = 0; y < FTable.RowCount; y++)
        {
          FTable[index + x, y] = FCells[x, y];
        }
      }

      ClearColumns();
    }

    public void CutRows(TableRow[] rows)
    {
      ClearRows();
      foreach (TableRow r in rows)
      {
        FTable.Rows.Remove(r);
        FRows.Add(r);
      }
    }

    public void PasteRows(int index)
    {
      for (int i = 0; i < FRows.Count; i++)
      {
        FTable.Rows.Insert(index + i, FRows[i]);
      }
      ClearRows();
    }

    public void CutCells(Rectangle selection)
    {
      ClearColumns();
      CutCells(selection, true);
    }

    public void CopyCells(Rectangle selection)
    {
      CutCells(selection, false);
    }

    public void PasteCells(Point newLocation)
    {
      for (int x = 0; x < FCellsSize.Width; x++)
      {
        for (int y = 0; y < FCellsSize.Height; y++)
        {
          FTable[x + newLocation.X, y + newLocation.Y] = FCells[x, y].Clone();
        }
      }
    }
    #endregion
    
    public TableClipboard(TableBase table)
    {
      FTable = table;
      FColumns = new List<TableColumn>();
      FRows = new List<TableRow>();
    }
  }
}
