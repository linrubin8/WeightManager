using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Data;
using FastReport.Table;
using FastReport.Utils;
using System.Drawing;
using System.Collections;

namespace FastReport.Matrix
{
  internal class MatrixHelper
  {
    private MatrixObject FMatrix;
    private int FHeaderWidth;
    private int FHeaderHeight;
    private int FBodyWidth;
    private int FBodyHeight;
    private bool FDesignTime;
    private TableResult FResultTable;
    private MatrixDescriptor FTitleDescriptor;
    private MatrixDescriptor FCellHeaderDescriptor;
    private MatrixHeaderDescriptor FNoColumnsDescriptor;
    private MatrixHeaderDescriptor FNoRowsDescriptor;
    private MatrixCellDescriptor FNoCellsDescriptor;
    private object[] FCellValues;
    private int[] FEvenStyleIndices;
    private bool FNoColumns;
    private bool FNoRows;

    #region Properties
    public MatrixObject Matrix
    {
      get { return FMatrix; }
    }
    
    public Report Report
    {
      get { return Matrix.Report; }
    }
    
    public TableResult ResultTable
    {
      get { return FDesignTime ? FResultTable : Matrix.ResultTable; }
    }
    
    public int HeaderHeight
    {
      get { return FHeaderHeight; }
    }

    public int HeaderWidth
    {
      get { return FHeaderWidth; }
    }

    public int BodyWidth
    {
      get { return FBodyWidth; }
    }

    public int BodyHeight
    {
      get { return FBodyHeight; }
    }

    public object[] CellValues
    {
      get { return FCellValues; }
    }
    #endregion
    
    #region Private Methods
    private string ExtractColumnName(string complexName)
    {
      if (complexName.StartsWith("[") && complexName.EndsWith("]"))
        complexName = complexName.Substring(1, complexName.Length - 2);

      if (Report == null)
        return complexName;
      Column column = DataHelper.GetColumn(Report.Dictionary, complexName);
      if (column == null)
        return complexName;
      return column.Alias;
    }

    /// <summary>
    /// Updates HeaderWidth, HeaderHeight, BodyWidth, BodyHeight properties.
    /// </summary>
    private void UpdateTemplateSizes()
    {
      FHeaderWidth = Matrix.Data.Rows.Count;
      if (FHeaderWidth == 0)
        FHeaderWidth = 1;
      if (Matrix.Data.Cells.Count > 1 && !Matrix.CellsSideBySide)
        FHeaderWidth++;

      FHeaderHeight = Matrix.Data.Columns.Count;
      if (FHeaderHeight == 0)
        FHeaderHeight = 1;
      if (Matrix.Data.Cells.Count > 1 && Matrix.CellsSideBySide)
        FHeaderHeight++;
      if (Matrix.ShowTitle)
        FHeaderHeight++;

      FBodyWidth = 1;
      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Columns)
      {
        if (descr.Totals)
          FBodyWidth++;
      }
      if (Matrix.CellsSideBySide && Matrix.Data.Cells.Count > 1)
        FBodyWidth *= Matrix.Data.Cells.Count;

      FBodyHeight = 1;
      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Rows)
      {
        if (descr.Totals)
          FBodyHeight++;
      }
      if (!Matrix.CellsSideBySide && Matrix.Data.Cells.Count > 1)
        FBodyHeight *= Matrix.Data.Cells.Count;
    }

    private void UpdateColumnDescriptors()
    {
      int left = HeaderWidth;
      int top = 0;
      int width = BodyWidth;
      int dataWidth = 1;

      if (Matrix.Data.Cells.Count > 1 && Matrix.CellsSideBySide)
        dataWidth = Matrix.Data.Cells.Count;

      if (Matrix.ShowTitle)
        top++;

      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Columns)
      {
        if (descr.Totals)
        {
          descr.TemplateTotalColumn = Matrix.Columns[left + width - dataWidth];
          descr.TemplateTotalRow = Matrix.Rows[top];
          descr.TemplateTotalCell = Matrix[left + width - dataWidth, top];
          width -= dataWidth;
        }
        else
        {
          descr.TemplateTotalColumn = null;
          descr.TemplateTotalRow = null;
          descr.TemplateTotalCell = null;
        }

        descr.TemplateColumn = Matrix.Columns[left];
        descr.TemplateRow = Matrix.Rows[top];
        descr.TemplateCell = Matrix[left, top];
        top++;
      }
    }

    private void UpdateRowDescriptors()
    {
      int left = 0;
      int top = HeaderHeight;
      int height = BodyHeight;
      int dataHeight = 1;

      if (Matrix.Data.Cells.Count > 1 && !Matrix.CellsSideBySide)
        dataHeight = Matrix.Data.Cells.Count;

      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Rows)
      {
        if (descr.Totals)
        {
          descr.TemplateTotalColumn = Matrix.Columns[left];
          descr.TemplateTotalRow = Matrix.Rows[top + height - dataHeight];
          descr.TemplateTotalCell = Matrix[left, top + height - dataHeight];
          height -= dataHeight;
        }
        else
        {
          descr.TemplateTotalColumn = null;
          descr.TemplateTotalRow = null;
          descr.TemplateTotalCell = null;
        }

        descr.TemplateColumn = Matrix.Columns[left];
        descr.TemplateRow = Matrix.Rows[top];
        descr.TemplateCell = Matrix[left, top];
        left++;
      }
    }

    private void UpdateCellDescriptors()
    {
      int x = HeaderWidth;
      int y = HeaderHeight;

      foreach (MatrixCellDescriptor descr in Matrix.Data.Cells)
      {
        descr.TemplateColumn = Matrix.Columns[x];
        descr.TemplateRow = Matrix.Rows[y];
        descr.TemplateCell = Matrix[x, y];

        if (Matrix.CellsSideBySide)
          x++;
        else
          y++;
      }
    }
    
    private void UpdateOtherDescriptors()
    {
      FTitleDescriptor.TemplateColumn = null;
      FTitleDescriptor.TemplateRow = null;
      FTitleDescriptor.TemplateCell = null;
      if (Matrix.ShowTitle)
      {
        FTitleDescriptor.TemplateColumn = Matrix.Columns[HeaderWidth];
        FTitleDescriptor.TemplateRow = Matrix.Rows[0];
        FTitleDescriptor.TemplateCell = Matrix[HeaderWidth, 0];
      }
      
      FCellHeaderDescriptor.TemplateColumn = null;
      FCellHeaderDescriptor.TemplateRow = null;
      FCellHeaderDescriptor.TemplateCell = null;
      if (Matrix.Data.Cells.Count > 1)
      {
        if (Matrix.CellsSideBySide)
        {
          FCellHeaderDescriptor.TemplateColumn = Matrix.Columns[HeaderWidth];
          FCellHeaderDescriptor.TemplateRow = Matrix.Rows[HeaderHeight - 1];
        }  
        else
        {
          FCellHeaderDescriptor.TemplateColumn = Matrix.Columns[HeaderWidth - 1];
          FCellHeaderDescriptor.TemplateRow = Matrix.Rows[HeaderHeight];
        }  
      }
    }

    private void ApplyStyle(TableCell cell, string styleName)
    {
      Style style = null;
      int styleIndex = Matrix.StyleSheet.IndexOf(Matrix.Style);
      if (styleIndex != -1)
      {
        StyleCollection styles = Matrix.StyleSheet[styleIndex];
        style = styles[styles.IndexOf(styleName)];
      }

      if (style != null)
        cell.ApplyStyle(style);
    }

    private TableCell CreateCell(string text)
    {
      TableCell cell = new TableCell();
      cell.Font = DrawUtils.DefaultReportFont;
      cell.Text = text;
      cell.HorzAlign = HorzAlign.Center;
      cell.VertAlign = VertAlign.Center;
      ApplyStyle(cell, "Header");
      return cell;
    }

    private TableCell CreateDataCell()
    {
      TableCell cell = new TableCell();
      cell.Font = DrawUtils.DefaultReportFont;
      cell.HorzAlign = HorzAlign.Right;
      cell.VertAlign = VertAlign.Center;
      ApplyStyle(cell, "Body");
      return cell;
    }

    private void SetHint(TableCell cell, string text)
    {
      cell.Assign(Matrix.Styles.DefaultStyle);
      cell.Text = text;
      cell.Font = DrawUtils.DefaultReportFont;
      cell.TextFill = new SolidFill(Color.Gray);
      cell.HorzAlign = HorzAlign.Center;
      cell.VertAlign = VertAlign.Center;
      cell.SetFlags(Flags.CanEdit, false);
    }

    private Point GetBodyLocation()
    {
      // determine the template's body location. Do not rely on HeaderWidth, HeaderHeight - 
      // the template may be empty
      Point result = new Point();
      
      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Columns)
      {
        if (descr.TemplateColumn != null)
          result.X = descr.TemplateColumn.Index;
      }
      foreach (MatrixHeaderDescriptor descr in Matrix.Data.Rows)
      {
        if (descr.TemplateRow != null)
          result.Y = descr.TemplateRow.Index;
      }
      
      return result;
    }

    private void InitResultTable(bool isTemplate)
    {
      Matrix.Data.Columns.AddTotalItems(isTemplate);
      Matrix.Data.Rows.AddTotalItems(isTemplate);
      List<MatrixHeaderItem> columnTerminalItems = Matrix.Data.Columns.RootItem.GetTerminalItems();
      List<MatrixHeaderItem> rowTerminalItems = Matrix.Data.Rows.RootItem.GetTerminalItems();

      // create corner
      List<MatrixDescriptor> descrList = new List<MatrixDescriptor>();

      if (Matrix.ShowTitle)
        descrList.Add(FTitleDescriptor);
      descrList.AddRange(Matrix.Data.Columns.ToArray());
      if (Matrix.Data.Cells.Count > 1 && Matrix.CellsSideBySide)
        descrList.Add(FCellHeaderDescriptor);

      foreach (MatrixDescriptor descr in descrList)
      {
        TableRow row = new TableRow();
        if (descr.TemplateRow != null)
          row.Assign(descr.TemplateRow);
        ResultTable.Rows.Add(row);
      }

      descrList.Clear();
      descrList.AddRange(Matrix.Data.Rows.ToArray());
      if (Matrix.Data.Cells.Count > 1 && !Matrix.CellsSideBySide)
        descrList.Add(FCellHeaderDescriptor);
      
      foreach (MatrixDescriptor descr in descrList)
      {
        TableColumn column = new TableColumn();
        if (descr.TemplateColumn != null)
          column.Assign(descr.TemplateColumn);
        ResultTable.Columns.Add(column);
      }

      // determine the body location
      Point bodyLocation = GetBodyLocation();
      
      // create columns
      foreach (MatrixHeaderItem item in columnTerminalItems)
      {
        foreach (MatrixCellDescriptor descr in Matrix.Data.Cells)
        {
          TableColumn column = new TableColumn();
          if (item.TemplateColumn != null && descr.TemplateColumn != null)
            column.Assign(Matrix.Columns[item.TemplateColumn.Index + (descr.TemplateColumn.Index - bodyLocation.X)]);
          ResultTable.Columns.Add(column);
          
          if (!Matrix.CellsSideBySide)
            break;
        }
      }

      // create rows
      foreach (MatrixHeaderItem item in rowTerminalItems)
      {
        foreach (MatrixCellDescriptor descr in Matrix.Data.Cells)
        {
          TableRow row = new TableRow();
          if (item.TemplateRow != null && descr.TemplateRow != null)
            row.Assign(Matrix.Rows[item.TemplateRow.Index + (descr.TemplateRow.Index - bodyLocation.Y)]);
          ResultTable.Rows.Add(row);

          if (Matrix.CellsSideBySide)
            break;
        }
      }
    }

    private void PrintHeaderCell(MatrixHeaderItem item, TableCellData resultCell, bool isEven)
    {
      TableCell templateCell = item.TemplateCell;
      if (templateCell != null)
      {
        if (FDesignTime)
        {
          if (!item.IsTotal)
            templateCell.Text = item.Value.ToString();
          resultCell.RunTimeAssign(templateCell, true);
        }
        else
        {
          if (Matrix.DataSource != null)
            Matrix.DataSource.CurrentRowNo = item.DataRowNo;
            
          templateCell.SetValue(item.Value);
          if (!item.IsTotal)
            templateCell.Text = templateCell.Format.FormatValue(item.Value);
          templateCell.SaveState();
          if (isEven)
            templateCell.ApplyEvenStyle();
          templateCell.GetData();
          if (String.IsNullOrEmpty(templateCell.Hyperlink.Expression) &&
            (templateCell.Hyperlink.Kind == HyperlinkKind.DetailReport ||
            templateCell.Hyperlink.Kind == HyperlinkKind.DetailPage ||
            templateCell.Hyperlink.Kind == HyperlinkKind.Custom))
            templateCell.Hyperlink.Value = templateCell.Text;
          resultCell.RunTimeAssign(templateCell, true);
          templateCell.RestoreState();
        }
      }
      else
      {
        templateCell = CreateCell(item.IsTotal ? Res.Get("ComponentsMisc,Matrix,Total") : item.Value.ToString());
        resultCell.RunTimeAssign(templateCell, true);
      }
    }

    private void PrintColumnHeader(MatrixHeaderItem root, int left, int top, int level)
    {
      int dataWidth = 1;
      int height = HeaderHeight;
      if (Matrix.Data.Cells.Count > 1 && Matrix.CellsSideBySide)
      {
        dataWidth = Matrix.Data.Cells.Count;
        height--;
      }

      foreach (MatrixHeaderItem item in root.Items)
      {
        Matrix.ColumnValues = item.Values;
        TableCellData resultCell = ResultTable.GetCellData(left, top);
        int span = item.Span * dataWidth;
        resultCell.ColSpan = span;
        if (item.IsTotal)
          resultCell.RowSpan = height - top;

        PrintHeaderCell(item, resultCell, FEvenStyleIndices[level] % 2 != 0);
        PrintColumnHeader(item, left, top + resultCell.RowSpan, level + 1);

        if (item.PageBreak && left > HeaderWidth)
          ResultTable.Columns[left].PageBreak = true;

        left += span;
        FEvenStyleIndices[level]++;
      }
      
      // print cell header
      if (root.Items.Count == 0 && dataWidth > 1)
      {
        foreach (MatrixCellDescriptor descr in Matrix.Data.Cells)
        {
          TableCell templateCell = null;
          TableCellData resultCell = ResultTable.GetCellData(left, top);

          if (root.TemplateColumn != null && descr.TemplateColumn != null &&
            FCellHeaderDescriptor.TemplateColumn != null && FCellHeaderDescriptor.TemplateRow != null)
          {
            templateCell = Matrix[
              root.TemplateColumn.Index + (descr.TemplateColumn.Index - FCellHeaderDescriptor.TemplateColumn.Index),
              FCellHeaderDescriptor.TemplateRow.Index];
          }
          else
            templateCell = CreateCell(ExtractColumnName(descr.Expression));

          templateCell.SaveState();
          templateCell.GetData();
          resultCell.RunTimeAssign(templateCell, true);
          templateCell.RestoreState();
          left++;
        }
      }
    }

    private void PrintColumnHeader()
    {
      Matrix.RowValues = null;
      FEvenStyleIndices = new int[Matrix.Data.Columns.Count];
      PrintColumnHeader(Matrix.Data.Columns.RootItem, HeaderWidth, Matrix.ShowTitle ? 1 : 0, 0);
    }

    private void PrintRowHeader(MatrixHeaderItem root, int left, int top, int level)
    {
      int dataHeight = 1;
      int width = HeaderWidth;
      if (Matrix.Data.Cells.Count > 1 && !Matrix.CellsSideBySide)
      {
        dataHeight = Matrix.Data.Cells.Count;
        width--;
      }

      foreach (MatrixHeaderItem item in root.Items)
      {
        Matrix.RowValues = item.Values;
        TableCellData resultCell = ResultTable.GetCellData(left, top);
        int span = item.Span * dataHeight;
        resultCell.RowSpan = span;
        if (item.IsTotal)
          resultCell.ColSpan = width - left;

        PrintHeaderCell(item, resultCell, FEvenStyleIndices[level] % 2 != 0);
        PrintRowHeader(item, left + resultCell.ColSpan, top, level + 1);

        if (item.PageBreak && top > HeaderHeight)
          ResultTable.Rows[top].PageBreak = true;

        top += span;
        FEvenStyleIndices[level]++;
      }

      // print cell header
      if (root.Items.Count == 0 && dataHeight > 1)
      {
        foreach (MatrixCellDescriptor descr in Matrix.Data.Cells)
        {
          TableCell templateCell = null;
          TableCellData resultCell = ResultTable.GetCellData(left, top);

          if (root.TemplateRow != null && descr.TemplateRow != null &&
            FCellHeaderDescriptor.TemplateColumn != null && FCellHeaderDescriptor.TemplateRow != null)
          {
            templateCell = Matrix[FCellHeaderDescriptor.TemplateColumn.Index,
              root.TemplateRow.Index + (descr.TemplateRow.Index - FCellHeaderDescriptor.TemplateRow.Index)];
          }
          else
            templateCell = CreateCell(ExtractColumnName(descr.Expression));

          templateCell.SaveState();
          templateCell.GetData();
          resultCell.RunTimeAssign(templateCell, true);
          templateCell.RestoreState();
          top++;
        }
      }
    }

    private void PrintRowHeader()
    {
      Matrix.ColumnValues = null;
      FEvenStyleIndices = new int[Matrix.Data.Rows.Count];
      PrintRowHeader(Matrix.Data.Rows.RootItem, 0, HeaderHeight, 0);
    }

    private void PrintCorner()
    {
      int left = 0;
      int top = Matrix.ShowTitle ? 1 : 0;
      int templateTop = FTitleDescriptor.TemplateRow != null ? 1 : 0;

      List<MatrixDescriptor> descrList = new List<MatrixDescriptor>();
      descrList.AddRange(Matrix.Data.Rows.ToArray());
      if (Matrix.Data.Cells.Count > 1 && !Matrix.CellsSideBySide)
        descrList.Add(FCellHeaderDescriptor);
      
      foreach (MatrixDescriptor descr in descrList)
      {
        TableCell templateCell = null;
        if (descr.TemplateColumn != null)
          templateCell = Matrix[descr.TemplateColumn.Index, templateTop];
        else
          templateCell = CreateCell(ExtractColumnName(descr.Expression));

        TableCellData resultCell = ResultTable.GetCellData(left, top);
        templateCell.SaveState();
        templateCell.GetData();
        resultCell.RunTimeAssign(templateCell, true);
        templateCell.RestoreState();
        
        resultCell.RowSpan = HeaderHeight - top;
        left++;
      }
    }

    private void PrintTitle()
    {
      TableCell templateCell = FTitleDescriptor.TemplateCell;
      if (FTitleDescriptor.TemplateCell == null)
        templateCell = CreateCell(Res.Get("ComponentsMisc,Matrix,Title"));

      TableCellData resultCell = ResultTable.GetCellData(HeaderWidth, 0);
      templateCell.SaveState();
      templateCell.GetData();
      resultCell.RunTimeAssign(templateCell, true);
      resultCell.ColSpan = ResultTable.ColumnCount - HeaderWidth;
      templateCell.RestoreState();
      
      // print left-top cell
      if (FTitleDescriptor.TemplateCell == null)
        templateCell.Text = "";
      else
        templateCell = Matrix[0, 0];

      resultCell = ResultTable.GetCellData(0, 0);
      templateCell.SaveState();
      templateCell.GetData();
      resultCell.RunTimeAssign(templateCell, true);
      templateCell.RestoreState();
      resultCell.ColSpan = HeaderWidth;
    }

    private void PrintHeaders()
    {
      PrintColumnHeader();
      PrintRowHeader();
      if (Matrix.ShowTitle)
        PrintTitle();
      PrintCorner();
    }

    private void PrintData_CalcTotals(int pass)
    {
      List<MatrixHeaderItem> columnTerminalItems = Matrix.Data.Columns.RootItem.GetTerminalItems();
      List<MatrixHeaderItem> rowTerminalItems = Matrix.Data.Rows.RootItem.GetTerminalItems();
      int dataCount = Matrix.Data.Cells.Count;
      FCellValues = new object[dataCount];

      foreach (MatrixHeaderItem rowItem in rowTerminalItems)
      {
        foreach (MatrixHeaderItem columnItem in columnTerminalItems)
        {
          if ((pass == 1 && !(rowItem.IsTotal || columnItem.IsTotal)) ||
            (pass == 2 && (rowItem.IsTotal || columnItem.IsTotal)))
            continue;
          
          // at first we calc cells with non-custom functions
          // prepare FCellValues array which will be used when calculating custom functions
          for (int cellIndex = 0; cellIndex < dataCount; cellIndex++)
          {
            if (Matrix.Data.Cells[cellIndex].Function != MatrixAggregateFunction.Custom)
              FCellValues[cellIndex] = GetCellValue(columnItem, rowItem, cellIndex);
          }

          // at second we calc cells with custom functions 
          // (to allow the custom function to use other cells' values)
          for (int cellIndex = 0; cellIndex < dataCount; cellIndex++)
          {
            if (Matrix.Data.Cells[cellIndex].Function == MatrixAggregateFunction.Custom)
              FCellValues[cellIndex] = GetCellValue(columnItem, rowItem, cellIndex);
          }

          Matrix.Data.Cells.SetValues(columnItem.Index, rowItem.Index, FCellValues);
        }
      }
    }

    private void PrintData_CalcPercents()
    {
      int dataCount = Matrix.Data.Cells.Count;
      
      // check if we need to do this
      bool hasPercents = false;
      for (int cellIndex = 0; cellIndex < dataCount; cellIndex++)
      {
        if (Matrix.Data.Cells[cellIndex].Percent != MatrixPercent.None)
          hasPercents = true;
      }
      if (!hasPercents)
        return;
      
      List<MatrixHeaderItem> columnTerminalItems = Matrix.Data.Columns.RootItem.GetTerminalItems();
      List<MatrixHeaderItem> rowTerminalItems = Matrix.Data.Rows.RootItem.GetTerminalItems();
      FCellValues = new object[dataCount];

      foreach (MatrixHeaderItem rowItem in rowTerminalItems)
      {
        foreach (MatrixHeaderItem columnItem in columnTerminalItems)
        {
          for (int cellIndex = 0; cellIndex < dataCount; cellIndex++)
          {
            object cellValue = Matrix.Data.Cells.GetValue(columnItem.Index, rowItem.Index, cellIndex);
            object totalValue = null;
            object value = null;

            int totalColumnIndex = Matrix.Data.Columns[0].TotalsFirst ? 0 : columnTerminalItems.Count - 1;
            int totalRowIndex = Matrix.Data.Rows[0].TotalsFirst ? 0 : rowTerminalItems.Count - 1;

            switch (Matrix.Data.Cells[cellIndex].Percent)
            {
              case MatrixPercent.None:
                value = cellValue;
                break;
              
              case MatrixPercent.ColumnTotal:
                totalValue = Matrix.Data.Cells.GetValue(columnTerminalItems[totalColumnIndex].Index, rowItem.Index, cellIndex);
                break;
              
              case MatrixPercent.RowTotal:
                totalValue = Matrix.Data.Cells.GetValue(columnItem.Index, rowTerminalItems[totalRowIndex].Index, cellIndex);
                break;
              
              case MatrixPercent.GrandTotal:
                totalValue = Matrix.Data.Cells.GetValue(columnTerminalItems[totalColumnIndex].Index, rowTerminalItems[totalRowIndex].Index, cellIndex);
                break;
            }

            if (cellValue != null && totalValue != null)
              value = ((Variant)(new Variant(cellValue) / new Variant(totalValue))).Value;
            
            FCellValues[cellIndex] = value;
          }

          Matrix.Data.Cells.SetValues(columnItem.Index, rowItem.Index, FCellValues);
        }
      }
    }

    private void PrintData()
    {
      // use two passes to calc cell values. This is necessary because this calculation 
      // replaces an array of cell values by the single (aggregated) value. 
      // At the first pass we calc total values only (so they include all cell values, not aggregated ones); 
      // at the second pass we calc other values except total.
      PrintData_CalcTotals(1);
      PrintData_CalcTotals(2);
      // calc percents
      PrintData_CalcPercents();
      
      List<MatrixHeaderItem> columnTerminalItems = Matrix.Data.Columns.RootItem.GetTerminalItems();
      List<MatrixHeaderItem> rowTerminalItems = Matrix.Data.Rows.RootItem.GetTerminalItems();
      int dataCount = Matrix.Data.Cells.Count;
      int top = HeaderHeight;
      Point bodyLocation = GetBodyLocation();
      bool firstTimePrintingData = true;
      FCellValues = new object[dataCount];
      Matrix.RowIndex = 0;

      foreach (MatrixHeaderItem rowItem in rowTerminalItems)
      {
        int left = HeaderWidth;
        Matrix.RowValues = rowItem.Values;
        Matrix.ColumnIndex = 0;

        foreach (MatrixHeaderItem columnItem in columnTerminalItems)
        {
          Matrix.ColumnValues = columnItem.Values;

          for (int cellIndex = 0; cellIndex < dataCount; cellIndex++)
          {
            TableCell templateCell = null;
            TableCellData resultCell = null;
            MatrixCellDescriptor descr = Matrix.Data.Cells[cellIndex];

            if (Matrix.CellsSideBySide)
            {
              if (columnItem.TemplateColumn != null && rowItem.TemplateRow != null && descr.TemplateColumn != null)
              {
                templateCell = Matrix[
                  columnItem.TemplateColumn.Index + (descr.TemplateColumn.Index - bodyLocation.X),
                  rowItem.TemplateRow.Index];
              }
              else
                templateCell = CreateDataCell();

              resultCell = ResultTable.GetCellData(left + cellIndex, top);
            }
            else
            {
              if (columnItem.TemplateColumn != null && rowItem.TemplateRow != null && descr.TemplateColumn != null)
              {
                templateCell = Matrix[columnItem.TemplateColumn.Index,
                  rowItem.TemplateRow.Index + (descr.TemplateRow.Index - bodyLocation.Y)];
              }
              else
                templateCell = CreateDataCell();

              resultCell = ResultTable.GetCellData(left, top + cellIndex);
            }

            if (FDesignTime)
            {
              if (firstTimePrintingData)
                templateCell.Text = "[" + ExtractColumnName(descr.Expression) + "]";
              else
                templateCell.Text = "";
              resultCell.RunTimeAssign(templateCell, true);
            }
            else
            {
              object value = Matrix.Data.GetValue(columnItem.Index, rowItem.Index, cellIndex);
              FCellValues[cellIndex] = value;
              templateCell.Text = templateCell.FormatValue(value);
              templateCell.SaveState();
              if (String.IsNullOrEmpty(templateCell.Hyperlink.Expression) &&
                (templateCell.Hyperlink.Kind == HyperlinkKind.DetailReport ||
                templateCell.Hyperlink.Kind == HyperlinkKind.DetailPage ||
                templateCell.Hyperlink.Kind == HyperlinkKind.Custom))
              {
                string hyperlinkValue = "";
                string separator = templateCell.Hyperlink.ValuesSeparator;
                foreach (object obj in Matrix.ColumnValues)
                {
                  hyperlinkValue += obj.ToString() + separator;
                }
                foreach (object obj in Matrix.RowValues)
                {
                  hyperlinkValue += obj.ToString() + separator;
                }
                templateCell.Hyperlink.Value = hyperlinkValue.Substring(0, hyperlinkValue.Length - separator.Length);
              }

              int evenStyleIndex = Matrix.MatrixEvenStylePriority == MatrixEvenStylePriority.Rows ?
                Matrix.RowIndex : Matrix.ColumnIndex;
              if ((evenStyleIndex + 1) % 2 == 0)
                templateCell.ApplyEvenStyle();
              templateCell.GetData();
              resultCell.RunTimeAssign(templateCell, true);
              templateCell.RestoreState();
            }
          }

          firstTimePrintingData = false;
          Matrix.ColumnIndex++;
          if (Matrix.CellsSideBySide)
          {
            if (Matrix.KeepCellsSideBySide)
              ResultTable.Columns[left].KeepColumns = dataCount;
            left += dataCount;
          }
          else
            left++;
        }

        Matrix.RowIndex++;
        if (Matrix.CellsSideBySide)
          top++;
        else
          top += dataCount;
      }
    }

    private object GetCellValue(MatrixHeaderItem columnItem, MatrixHeaderItem rowItem, int cellIndex)
    {
      if (columnItem.IsTotal || rowItem.IsTotal)
        return GetAggregatedTotalValue(columnItem, rowItem, cellIndex);
      else
        return GetAggregatedValue(Matrix.Data.GetValues(columnItem.Index, rowItem.Index, cellIndex), cellIndex);
    }

    private object GetAggregatedTotalValue(MatrixHeaderItem column, MatrixHeaderItem row, int cellIndex)
    {
      ArrayList list = new ArrayList();

      if (column.IsTotal)
        column = column.Parent;
      if (row.IsTotal)
        row = row.Parent;

      List<MatrixHeaderItem> columnTerminalItems = column.GetTerminalItems();
      List<MatrixHeaderItem> rowTerminalItems = row.GetTerminalItems();

      // collect all values in the specified items
      foreach (MatrixHeaderItem rowItem in rowTerminalItems)
      {
        if (rowItem.IsTotal)
          continue;

        foreach (MatrixHeaderItem columnItem in columnTerminalItems)
        {
          if (columnItem.IsTotal)
            continue;

          ArrayList values = Matrix.Data.GetValues(columnItem.Index, rowItem.Index, cellIndex);
          if (values != null)
            list.AddRange(values);
        }
      }

      return GetAggregatedValue(list, cellIndex);
    }

    private object GetAggregatedValue(ArrayList list, int cellIndex)
    {
      if (list == null || list.Count == 0)
        return null;

      MatrixAggregateFunction function = Matrix.Data.Cells[cellIndex].Function;
      
      // custom function - just calculate the expression
      if (function == MatrixAggregateFunction.Custom)
        return Report.Calc(Matrix.Data.Cells[cellIndex].Expression);
      
      // Count function - just return the number of values
      if (function == MatrixAggregateFunction.Count)
        return list.Count;

      // aggregated value
      Variant aggrValue = new Variant();

      for (int i = 0; i < list.Count; i++)
      {
        object value = list[i];
        if (i == 0)
        {
          // assign the first value to aggrValue
          aggrValue = new Variant(value);
        }
        else
        {
          // handle other values
          switch (function)
          {
            case MatrixAggregateFunction.Sum:
            case MatrixAggregateFunction.Avg:
              aggrValue += new Variant(value);
              break;

            case MatrixAggregateFunction.Min:
              if (new Variant(value) < aggrValue)
                aggrValue = new Variant(value);
              break;

            case MatrixAggregateFunction.Max:
              if (new Variant(value) > aggrValue)
                aggrValue = new Variant(value);
              break;
          }
        }
      }

      // finish Avg calculation
      if (function == MatrixAggregateFunction.Avg)
        aggrValue = aggrValue / list.Count;
      return aggrValue.Value;
    }
    #endregion

    #region Public Methods
    public void BuildTemplate()
    {
      bool noColumns = Matrix.Data.Columns.Count == 0;
      bool noRows = Matrix.Data.Rows.Count == 0;
      bool noCells = Matrix.Data.Cells.Count == 0;

      // create temporary descriptors
      if (noColumns)
        Matrix.Data.Columns.Add(FNoColumnsDescriptor);
      if (noRows)
        Matrix.Data.Rows.Add(FNoRowsDescriptor);
      if (noCells)
        Matrix.Data.Cells.Add(FNoCellsDescriptor);
      
      UpdateTemplateSizes();

      // prepare data for the result table
      string[] columnValues = new string[Matrix.Data.Columns.Count];
      string[] rowValues = new string[Matrix.Data.Rows.Count];
      object[] cellValues = new object[Matrix.Data.Cells.Count];
      for (int i = 0; i < Matrix.Data.Columns.Count; i++)
      {
        columnValues[i] = "[" + ExtractColumnName(Matrix.Data.Columns[i].Expression) + "]";
      }
      for (int i = 0; i < Matrix.Data.Rows.Count; i++)
      {
        rowValues[i] = "[" + ExtractColumnName(Matrix.Data.Rows[i].Expression) + "]";
      }

      Matrix.Data.Clear();
      Matrix.Data.AddValue(columnValues, rowValues, cellValues, 0);
      
      // create the result table
      FDesignTime = true;
      FResultTable = new TableResult();
      InitResultTable(true);
      PrintHeaders();
      PrintData();
      
      // copy the result table to the Matrix
      Matrix.ColumnCount = ResultTable.ColumnCount;
      Matrix.RowCount = ResultTable.RowCount;
      Matrix.FixedColumns = HeaderWidth;
      Matrix.FixedRows = HeaderHeight;
      Matrix.CreateUniqueNames();
      
      for (int x = 0; x < Matrix.ColumnCount; x++)
      {
        Matrix.Columns[x].Assign(ResultTable.Columns[x]);
      }
      for (int y = 0; y < Matrix.RowCount; y++)
      {
        Matrix.Rows[y].Assign(ResultTable.Rows[y]);
      }
      for (int x = 0; x < Matrix.ColumnCount; x++)
      {
        for (int y = 0; y < Matrix.RowCount; y++)
        {
          TableCell cell = Matrix[x, y];
          cell.AssignAll(ResultTable[x, y]);
          cell.SetFlags(Flags.CanEdit, true);
        }
      }

      UpdateDescriptors();
      FResultTable.Dispose();
      
      // clear temporary descriptors, set hints
      if (noColumns)
      {
        SetHint(Matrix[HeaderWidth, Matrix.ShowTitle ? 1 : 0], Res.Get("ComponentsMisc,Matrix,NewColumn"));
        Matrix.Data.Columns.Clear();
      }
      else
      {
        FNoColumnsDescriptor.TemplateColumn = Matrix.Data.Columns[0].TemplateColumn;
        FNoColumnsDescriptor.TemplateRow = Matrix.Data.Columns[0].TemplateRow;
        FNoColumnsDescriptor.TemplateCell = Matrix.Data.Columns[0].TemplateCell;
      }
      if (noRows)
      {
        SetHint(Matrix[0, HeaderHeight], Res.Get("ComponentsMisc,Matrix,NewRow"));
        Matrix.Data.Rows.Clear();
      }
      else
      {
        FNoRowsDescriptor.TemplateColumn = Matrix.Data.Rows[0].TemplateColumn;
        FNoRowsDescriptor.TemplateRow = Matrix.Data.Rows[0].TemplateRow;
        FNoRowsDescriptor.TemplateCell = Matrix.Data.Rows[0].TemplateCell;
      }
      if (noCells)
      {
        SetHint(Matrix[HeaderWidth, HeaderHeight], Res.Get("ComponentsMisc,Matrix,NewCell"));
        Matrix.Data.Cells.Clear();
      }
      else
      {
        FNoCellsDescriptor.TemplateColumn = Matrix.Data.Cells[0].TemplateColumn;
        FNoCellsDescriptor.TemplateRow = Matrix.Data.Cells[0].TemplateRow;
        FNoCellsDescriptor.TemplateCell = Matrix.Data.Cells[0].TemplateCell;
      }
    }

    public void UpdateDescriptors()
    {
      bool noColumns = Matrix.Data.Columns.Count == 0;
      bool noRows = Matrix.Data.Rows.Count == 0;
      bool noCells = Matrix.Data.Cells.Count == 0;

      // create temporary descriptors
      if (noColumns)
        Matrix.Data.Columns.Add(FNoColumnsDescriptor);
      if (noRows)
        Matrix.Data.Rows.Add(FNoRowsDescriptor);
      if (noCells)
        Matrix.Data.Cells.Add(FNoCellsDescriptor);

      UpdateTemplateSizes();
      Matrix.FixedColumns = HeaderWidth;
      Matrix.FixedRows = HeaderHeight;
      UpdateColumnDescriptors();
      UpdateRowDescriptors();
      UpdateCellDescriptors();
      UpdateOtherDescriptors();

      // clear temporary descriptors
      if (noColumns)
        Matrix.Data.Columns.Clear();
      if (noRows)
        Matrix.Data.Rows.Clear();
      if (noCells)
        Matrix.Data.Cells.Clear();
    }

    public void StartPrint()
    {
      if ((Matrix.Data.Columns.Count == 0 && Matrix.Data.Rows.Count == 0) || Matrix.Data.Cells.Count == 0)
        throw new Exception(String.Format(Res.Get("Messages,MatrixError"), Matrix.Name));

      FDesignTime = false;
      FNoColumns = Matrix.Data.Columns.Count == 0;
      FNoRows = Matrix.Data.Rows.Count == 0;

      // create temporary descriptors
      if (FNoColumns)
        Matrix.Data.Columns.Add(FNoColumnsDescriptor);
      if (FNoRows)
        Matrix.Data.Rows.Add(FNoRowsDescriptor);

      Config.ReportSettings.OnProgress(Report, Res.Get("ComponentsMisc,Matrix,FillData"), 0, 0);
      Matrix.Data.Clear();
      Matrix.OnManualBuild(EventArgs.Empty);
    }

    public void AddDataRow()
    {
      object[] columnValues = new object[Matrix.Data.Columns.Count];
      object[] rowValues = new object[Matrix.Data.Rows.Count];
      object[] cellValues = new object[Matrix.Data.Cells.Count];
      string expression = "";

      for (int i = 0; i < Matrix.Data.Columns.Count; i++)
      {
        expression = Matrix.Data.Columns[i].Expression;
        columnValues[i] = String.IsNullOrEmpty(expression) ? null : Report.Calc(expression);
      }
      for (int i = 0; i < Matrix.Data.Rows.Count; i++)
      {
        expression = Matrix.Data.Rows[i].Expression;
        rowValues[i] = String.IsNullOrEmpty(expression) ? null : Report.Calc(expression);
      }
      for (int i = 0; i < Matrix.Data.Cells.Count; i++)
      {
        // skip custom function calculation; it will be calculated when we print the value
        if (Matrix.Data.Cells[i].Function == MatrixAggregateFunction.Custom)
          cellValues[i] = 0;
        else
          cellValues[i] = Report.Calc(Matrix.Data.Cells[i].Expression);
      }

      Matrix.Data.AddValue(columnValues, rowValues, cellValues, Matrix.DataSource.CurrentRowNo);
    }

    public void AddDataRows()
    {
      if (Matrix.DataSource != null)
      {
        Matrix.DataSource.Init(Matrix.Filter);
        while (Matrix.DataSource.HasMoreRows)
        {
          AddDataRow();
          Matrix.DataSource.Next();
        }
      }
    }
    
    public void FinishPrint()
    {
      UpdateDescriptors();
      ResultTable.FixedColumns = HeaderWidth;
      ResultTable.FixedRows = HeaderHeight;

      InitResultTable(false);
      PrintHeaders();
      PrintData();

      // clear temporary descriptors
      if (FNoColumns)
        Matrix.Data.Columns.Clear();
      if (FNoRows)
        Matrix.Data.Rows.Clear();
    }
    
    public void UpdateStyle()
    {
      for (int y = 0; y < Matrix.RowCount; y++)
      {
        for (int x = 0; x < Matrix.ColumnCount; x++)
        {
          string style = x < HeaderWidth || y < HeaderHeight ? "Header" : "Body";
          ApplyStyle(Matrix[x, y], style);
        }
      }
    }
    #endregion

    public MatrixHelper(MatrixObject matrix)
    {
      FMatrix = matrix;
      FTitleDescriptor = new MatrixDescriptor();
      FCellHeaderDescriptor = new MatrixDescriptor();
      FCellHeaderDescriptor.Expression = "Data";
      FNoColumnsDescriptor = new MatrixHeaderDescriptor("", false);
      FNoRowsDescriptor = new MatrixHeaderDescriptor("", false);
      FNoCellsDescriptor = new MatrixCellDescriptor();
    }
  }
}
