using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Table
{
  internal class TableHelper
  {
    private TableObject FSourceTable;
    private TableResult FResultTable;

    private enum NowPrinting { None, Row, Column }
    private NowPrinting FNowPrinting;
    private bool FRowsPriority;
    private int FOriginalRowIndex;
    private int FOriginalColumnIndex;
    private int FPrintingRowIndex;
    private int FPrintingColumnIndex;
    private List<SpanData> FColumnSpans;
    private List<SpanData> FRowSpans;
    private bool FPageBreak;

    private bool AutoSpans
    {
      get { return FSourceTable.ManualBuildAutoSpans; }
    }

    #region Build the Table
    public void PrintRow(int rowIndex)
    {
      FOriginalRowIndex = rowIndex;

      if (FNowPrinting == NowPrinting.None)
      {
        // we are at the start. Rows will now have priority over columns.
        FRowsPriority = true;
      }

      if (FRowsPriority)
      {
        switch (FNowPrinting)
        {
          case NowPrinting.None:
            FPrintingRowIndex = 0;
            break;

          case NowPrinting.Column:
            FPrintingRowIndex++;
            break;

          case NowPrinting.Row:
            // we have two sequential calls of the PrintRow. But we must print
            // some columns...
            break;
        }

        // add new row, do not copy cells: it will be done in the PrintColumn.
        TableRow row = new TableRow();
        row.Assign(FSourceTable.Rows[rowIndex]);
        row.PageBreak = FPageBreak;
        FResultTable.Rows.Add(row);

        FColumnSpans.Clear();
      }
      else
      {
        if (FNowPrinting == NowPrinting.Column)
        {
          // this is the first row inside a column, reset the index
          FPrintingRowIndex = 0;
        }
        else
        {
          // not the first row, increment the index
          FPrintingRowIndex++;
        }

        TableRow row = null;
        if (FResultTable.Rows.Count <= FPrintingRowIndex)
        {
          // index is outside existing rows. Probably not all rows created yet, 
          // we're at the start. Add new row.
          row = new TableRow();
          row.Assign(FSourceTable.Rows[rowIndex]);
          FResultTable.Rows.Add(row);
        }
        else
        {
          // do not create row, use existing one
          row = FResultTable.Rows[FPrintingRowIndex];
        }
        // apply page break
        row.PageBreak = FPageBreak;

        // copy cells from the template to the result
        CopyCells(FOriginalColumnIndex, FOriginalRowIndex,
          FPrintingColumnIndex, FPrintingRowIndex);
      }

      FNowPrinting = NowPrinting.Row;
      FPageBreak = false;
    }

    public void PrintColumn(int columnIndex)
    {
      FOriginalColumnIndex = columnIndex;

      if (FNowPrinting == NowPrinting.None)
      {
        // we are at the start. Columns will now have priority over rows.
        FRowsPriority = false;
      }

      if (!FRowsPriority)
      {
        switch (FNowPrinting)
        {
          case NowPrinting.None:
            FPrintingColumnIndex = 0;
            break;

          case NowPrinting.Column:
            // we have two sequential calls of the PrintColumn. But we must print
            // some rows...
            break;

          case NowPrinting.Row:
            FPrintingColumnIndex++;
            break;
        }

        // add new column, do not copy cells: it will be done in the PrintRow.
        TableColumn column = new TableColumn();
        column.Assign(FSourceTable.Columns[columnIndex]);
        column.PageBreak = FPageBreak;
        FResultTable.Columns.Add(column);

        FRowSpans.Clear();
      }
      else
      {
        if (FNowPrinting == NowPrinting.Row)
        {
          // this is the first column inside a row, reset the index
          FPrintingColumnIndex = 0;
        }
        else
        {
          // not the first column, increment the index
          FPrintingColumnIndex++;
        }

        TableColumn column = null;
        if (FResultTable.Columns.Count <= FPrintingColumnIndex)
        {
          // index is outside existing columns. Probably not all columns 
          // created yet, we're at the start. Add new column.
          column = new TableColumn();
          column.Assign(FSourceTable.Columns[columnIndex]);
          FResultTable.Columns.Add(column);
        }
        else
        {
          // do not create column, use existing one
          column = FResultTable.Columns[FPrintingColumnIndex];
        }
        // apply page break
        column.PageBreak = FPageBreak;

        // copy cells from the template to the result
        CopyCells(FOriginalColumnIndex, FOriginalRowIndex,
          FPrintingColumnIndex, FPrintingRowIndex);
      }

      FNowPrinting = NowPrinting.Column;
      FPageBreak = false;
    }

    public void PageBreak()
    {
      FPageBreak = true;
    }

    private void CopyCells(int originalColumnIndex, int originalRowIndex,
      int resultColumnIndex, int resultRowIndex)
    {
      TableCell cell = FSourceTable[originalColumnIndex, originalRowIndex];
      TableCellData cellTo = FResultTable.GetCellData(resultColumnIndex, resultRowIndex);
      FSourceTable.PrintingCell = cellTo;
      bool needData = true;

      if (AutoSpans)
      {
        if (FRowsPriority)
        {
          // We are printing columns inside a row. Check if we need to finish the column cell.
          if (FColumnSpans.Count > 0)
          {
            SpanData spanData = FColumnSpans[0];

            // check if we are printing the last column of the cell's span. From now, we will not accept 
            // the first column.
            if (originalColumnIndex == spanData.OriginalCellOrigin.X + spanData.OriginalCell.ColSpan - 1)
              spanData.FinishFlag = true;

            if ((spanData.FinishFlag && originalColumnIndex == spanData.OriginalCellOrigin.X) ||
              (originalColumnIndex < spanData.OriginalCellOrigin.X ||
               originalColumnIndex > spanData.OriginalCellOrigin.X + spanData.OriginalCell.ColSpan - 1))
              FColumnSpans.Clear();
            else
            {
              spanData.ResultCell.ColSpan++;
              needData = false;
            }
          }

          // add the column cell if it has ColSpan > 1
          if (cell.ColSpan > 1 && FColumnSpans.Count == 0)
          {
            SpanData spanData = new SpanData();
            FColumnSpans.Add(spanData);

            spanData.OriginalCell = cell;
            spanData.ResultCell = cellTo;
            spanData.OriginalCellOrigin = new Point(originalColumnIndex, originalRowIndex);
            spanData.ResultCellOrigin = new Point(resultColumnIndex, resultRowIndex);
          }

          // now check the row cells. Do this once for each row.
          if (FPrintingColumnIndex == 0)
          {
            for (int i = 0; i < FRowSpans.Count; i++)
            {
              SpanData spanData = FRowSpans[i];

              // check if we are printing the last row of the cell's span. From now, we will not accept 
              // the first row.
              if (originalRowIndex == spanData.OriginalCellOrigin.Y + spanData.OriginalCell.RowSpan - 1)
                spanData.FinishFlag = true;

              if ((spanData.FinishFlag && originalRowIndex == spanData.OriginalCellOrigin.Y) ||
                (originalRowIndex < spanData.OriginalCellOrigin.Y ||
                 originalRowIndex > spanData.OriginalCellOrigin.Y + spanData.OriginalCell.RowSpan - 1))
              {
                FRowSpans.RemoveAt(i);
                i--;
              }
              else
                spanData.ResultCell.RowSpan++;
            }
          }

          // check if we should skip current cell because it is inside a span
          for (int i = 0; i < FRowSpans.Count; i++)
          {
            SpanData spanData = FRowSpans[i];

            if (resultColumnIndex >= spanData.ResultCellOrigin.X &&
              resultColumnIndex <= spanData.ResultCellOrigin.X + spanData.ResultCell.ColSpan - 1 &&
              resultRowIndex >= spanData.ResultCellOrigin.Y &&
              resultRowIndex <= spanData.ResultCellOrigin.Y + spanData.ResultCell.RowSpan)
            {
              needData = false;
              break;
            }
          }

          // add the row cell if it has RowSpan > 1 and not added yet
          if (cell.RowSpan > 1 && needData)
          {
            SpanData spanData = new SpanData();
            FRowSpans.Add(spanData);

            spanData.OriginalCell = cell;
            spanData.ResultCell = cellTo;
            spanData.OriginalCellOrigin = new Point(originalColumnIndex, originalRowIndex);
            spanData.ResultCellOrigin = new Point(resultColumnIndex, resultRowIndex);
          }
        }
        else
        {
          // We are printing rows inside a column. Check if we need to finish the row cell.
          if (FRowSpans.Count > 0)
          {
            SpanData spanData = FRowSpans[0];

            // check if we are printing the last row of the cell's span. From now, we will not accept 
            // the first row.
            if (originalRowIndex == spanData.OriginalCellOrigin.Y + spanData.OriginalCell.RowSpan - 1)
              spanData.FinishFlag = true;

            if ((spanData.FinishFlag && originalRowIndex == spanData.OriginalCellOrigin.Y) ||
              (originalRowIndex < spanData.OriginalCellOrigin.Y ||
               originalRowIndex > spanData.OriginalCellOrigin.Y + spanData.OriginalCell.RowSpan - 1))
              FRowSpans.Clear();
            else
            {
              spanData.ResultCell.RowSpan++;
              needData = false;
            }
          }

          // add the row cell if it has RowSpan > 1
          if (cell.RowSpan > 1 && FRowSpans.Count == 0)
          {
            SpanData spanData = new SpanData();
            FRowSpans.Add(spanData);

            spanData.OriginalCell = cell;
            spanData.ResultCell = cellTo;
            spanData.OriginalCellOrigin = new Point(originalColumnIndex, originalRowIndex);
            spanData.ResultCellOrigin = new Point(resultColumnIndex, resultRowIndex);
          }

          // now check the column cells. Do this once for each column.
          if (FPrintingRowIndex == 0)
          {
            for (int i = 0; i < FColumnSpans.Count; i++)
            {
              SpanData spanData = FColumnSpans[i];

              // check if we are printing the last column of the cell's span. From now, we will not accept 
              // the first column.
              if (originalColumnIndex == spanData.OriginalCellOrigin.X + spanData.OriginalCell.ColSpan - 1)
                spanData.FinishFlag = true;

              if ((spanData.FinishFlag && originalColumnIndex == spanData.OriginalCellOrigin.X) ||
                (originalColumnIndex < spanData.OriginalCellOrigin.X ||
                 originalColumnIndex > spanData.OriginalCellOrigin.X + spanData.OriginalCell.ColSpan - 1))
              {
                FColumnSpans.RemoveAt(i);
                i--;
              }
              else
                spanData.ResultCell.ColSpan++;
            }
          }

          // check if we should skip current cell because it is inside a span
          for (int i = 0; i < FColumnSpans.Count; i++)
          {
            SpanData spanData = FColumnSpans[i];

            if (resultColumnIndex >= spanData.ResultCellOrigin.X &&
              resultColumnIndex <= spanData.ResultCellOrigin.X + spanData.ResultCell.ColSpan - 1 &&
              resultRowIndex >= spanData.ResultCellOrigin.Y &&
              resultRowIndex <= spanData.ResultCellOrigin.Y + spanData.ResultCell.RowSpan)
            {
              needData = false;
              break;
            }
          }

          // add the column cell if it has ColSpan > 1 and not added yet
          if (cell.ColSpan > 1 && needData)
          {
            SpanData spanData = new SpanData();
            FColumnSpans.Add(spanData);

            spanData.OriginalCell = cell;
            spanData.ResultCell = cellTo;
            spanData.OriginalCellOrigin = new Point(originalColumnIndex, originalRowIndex);
            spanData.ResultCellOrigin = new Point(resultColumnIndex, resultRowIndex);
          }
        }
      }
      else
      {
        cellTo.ColSpan = cell.ColSpan;
        cellTo.RowSpan = cell.RowSpan;
      }

      if (needData)
      {
        cell.SaveState();
        cell.GetData();
        cellTo.RunTimeAssign(cell, true);
        cell.RestoreState();
      }
    }
    #endregion

    #region Aggregate Functions
    #endregion

    public TableHelper(TableObject source, TableResult result)
    {
      FSourceTable = source;
      FResultTable = result;
      FColumnSpans = new List<SpanData>();
      FRowSpans = new List<SpanData>();
    }


    private class SpanData
    {
      public TableCell OriginalCell;
      public TableCellData ResultCell;
      public Point OriginalCellOrigin;
      public Point ResultCellOrigin;
      public bool FinishFlag;
    }
  }
}
