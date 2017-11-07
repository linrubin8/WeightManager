using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Table;
using FastReport.Data;
using FastReport.Utils;
using FastReport.Design.PageDesigners.Page;
using FastReport.TypeEditors;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Matrix
{
  internal enum MatrixElement 
  { 
    None, 
    Column, 
    Row, 
    Cell 
  }

  /// <summary>
  /// Describes how the even style is applied to a matrix.
  /// </summary>
  public enum MatrixEvenStylePriority
  {
    /// <summary>
    /// The even style is applied to matrix rows.
    /// </summary>
    Rows,

    /// <summary>
    /// The even style is applied to matrix columns.
    /// </summary>
    Columns
  }
  

  /// <summary>
  /// Represents the matrix object that is used to print pivot table (also known as cross-tab).
  /// </summary>
  /// <remarks>
  /// The matrix consists of the following elements: columns, rows and data cells. Each element is
  /// represented by the <b>descriptor</b>. The <see cref="MatrixHeaderDescriptor"/> class is used
  /// for columns and rows; the <see cref="MatrixCellDescriptor"/> is used for data cells.
  /// The <see cref="Data"/> property holds three collections of descriptors - <b>Columns</b>,
  /// <b>Rows</b> and <b>Cells</b>.
  /// <para/>To create the matrix in a code, you should perform the following actions:
  /// <list type="bullet">
  ///   <item>
  ///     <description>create an instance of the <b>MatrixObject</b> and add it to the report;</description>
  ///   </item>
  ///   <item>
  ///     <description>create descriptors for columns, rows and cells and add it to the
  ///     collections inside the <see cref="Data"/> property;</description>
  ///   </item>
  ///   <item>
  ///     <description>call the <see cref="BuildTemplate"/> method to create the matrix template
  ///     that will be used to create a result;</description>
  ///   </item>
  ///   <item>
  ///     <description>modify the matrix template (change captions, set the visual appearance).</description>
  ///   </item>
  /// </list>
  /// <para/>To connect the matrix to a datasource, use the <see cref="DataSource"/> property. If
  /// this property is not set, the result matrix will be empty. In this case you may use 
  /// the <see cref="ManualBuild"/> event handler to fill the matrix.
  /// </remarks>
  /// <example>This example demonstrates how to create a matrix in a code.
  /// <code>
  /// // create an instance of MatrixObject
  /// MatrixObject matrix = new MatrixObject();
  /// matrix.Name = "Matrix1";
  /// // add it to the report title band of the first report page
  /// matrix.Parent = (report.Pages[0] as ReportPage).ReportTitle;
  /// 
  /// // create two column descriptors
  /// MatrixHeaderDescriptor column = new MatrixHeaderDescriptor("[MatrixDemo.Year]");
  /// matrix.Data.Columns.Add(column);
  /// column = new MatrixHeaderDescriptor("[MatrixDemo.Month]");
  /// matrix.Data.Columns.Add(column);
  /// 
  /// // create one row descriptor
  /// MatrixHeaderDescriptor row = new MatrixHeaderDescriptor("[MatrixDemo.Name]");
  /// matrix.Data.Rows.Add(row);
  /// 
  /// // create one data cell
  /// MatrixCellDescriptor cell = new MatrixCellDescriptor("[MatrixDemo.Revenue]", MatrixAggregateFunction.Sum);
  /// matrix.Data.Cells.Add(cell);
  /// 
  /// // connect matrix to a datasource
  /// matrix.DataSource = Report.GetDataSource("MatrixDemo");
  /// 
  /// // create the matrix template
  /// matrix.BuildTemplate();
  /// 
  /// // change the style
  /// matrix.Style = "Green";
  /// 
  /// // change the column and row total's text to "Grand Total"
  /// matrix.Data.Columns[0].TemplateTotalCell.Text = "Grand Total";
  /// matrix.Data.Rows[0].TemplateTotalCell.Text = "Grand Total";
  /// </code>
  /// </example>
  public 
  class MatrixObject : TableBase
  {
    #region Fields
    private bool FAutoSize;
    private bool FCellsSideBySide;
    private bool FKeepCellsSideBySide;
    private DataSourceBase FDataSource;
    private string FFilter;
    private bool FShowTitle;
    private string FStyle;
    private MatrixData FData;
    private string FManualBuildEvent;
    private string FModifyResultEvent;
    private MatrixHelper FHelper;
    private bool FSaveVisible;
    private TableCell FSelectedCell;
    private bool FDragSelectedCell;
    private DragInfo FDragInfo;
    private MatrixStyleSheet FStyleSheet;
    private object[] FColumnValues;
    private object[] FRowValues;
    private int FColumnIndex;
    private int FRowIndex;
    private MatrixEvenStylePriority FMatrixEvenStylePriority;
    #endregion
    
    #region Properties
    /// <summary>
    /// Allows to fill the matrix in code.
    /// </summary>
    /// <remarks>
    /// In most cases the matrix is connected to a datasource via the <see cref="DataSource"/> 
    /// property. When you run a report, the matrix is filled with datasource values automatically.
    /// <para/>Using this event, you can put additional values to the matrix or even completely fill it
    /// with own values (if <see cref="DataSource"/> is set to <b>null</b>. To do this, call the
    /// <b>Data.AddValue</b> method. See the <see cref="MatrixData.AddValue(object[],object[],object[])"/>
    /// method for more details.
    /// </remarks>
    /// <example>This example shows how to fill a matrix with own values.
    /// <code>
    /// // suppose we have a matrix with one column, row and data cell.
    /// // provide 3 one-dimensional arrays with one element in each to the AddValue method
    /// Matrix1.Data.AddValue(
    ///   new object[] { 1996 },
    ///   new object[] { "Andrew Fuller" }, 
    ///   new object[] { 123.45f });
    /// Matrix1.Data.AddValue(
    ///   new object[] { 1997 },
    ///   new object[] { "Andrew Fuller" }, 
    ///   new object[] { 21.35f });
    /// Matrix1.Data.AddValue(
    ///   new object[] { 1997 },
    ///   new object[] { "Nancy Davolio" }, 
    ///   new object[] { 421.5f });
    /// 
    /// // this code will produce the following matrix:
    /// //               |  1996  |  1997  |
    /// // --------------+--------+--------+
    /// // Andrew Fuller |  123.45|   21.35|
    /// // --------------+--------+--------+
    /// // Nancy Davolio |        |  421.50|
    /// // --------------+--------+--------+
    /// </code>
    /// </example>
    public event EventHandler ManualBuild;

    /// <summary>
    /// Allows to modify the prepared matrix elements such as cells, rows, columns.
    /// </summary>
    public event EventHandler ModifyResult;

    /// <summary>
    /// Gets or sets a value that determines whether the matrix must calculate column/row sizes automatically.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool AutoSize
    {
      get { return FAutoSize; }
      set
      {
        FAutoSize = value;
        foreach (TableColumn column in Columns)
        {
          column.AutoSize = AutoSize;
        }
        foreach (TableRow row in Rows)
        {
          row.AutoSize = AutoSize;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value that determines how to print multiple data cells.
    /// </summary>
    /// <remarks>
    /// This property can be used if matrix has two or more data cells. Default property value
    /// is <b>false</b> - that means the data cells will be stacked.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool CellsSideBySide
    {
      get { return FCellsSideBySide; }
      set 
      {
        if (FCellsSideBySide != value)
        {
          FCellsSideBySide = value;
          if (IsDesigning)
          {
            foreach (MatrixCellDescriptor descr in Data.Cells)
            {
              descr.TemplateColumn = null;
              descr.TemplateRow = null;
            }

            BuildTemplate();
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating that the side-by-side cells must be kept together on the same page.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool KeepCellsSideBySide
    {
      get { return FKeepCellsSideBySide; }
      set { FKeepCellsSideBySide = value; }
    }

    /// <summary>
    /// Gets or sets a data source.
    /// </summary>
    /// <remarks>
    /// When you create the matrix in the designer by drag-drop data columns into it,
    /// this property will be set automatically. However you need to set it if you create 
    /// the matrix in code.
    /// </remarks>
    [Category("Data")]
    public DataSourceBase DataSource
    {
      get { return FDataSource; }
      set
      {
        if (FDataSource != value)
        {
          if (FDataSource != null)
            FDataSource.Disposed -= new EventHandler(DataSource_Disposed);
          if (value != null)
            value.Disposed += new EventHandler(DataSource_Disposed);
        }
        FDataSource = value;
      }
    }

    /// <summary>
    /// Gets the row filter expression.
    /// </summary>
    /// <remarks>
    /// This property can contain any valid boolean expression. If the expression returns <b>false</b>,
    /// the corresponding data row will be skipped.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Filter
    {
      get { return FFilter; }
      set { FFilter = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a title row.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ShowTitle
    {
      get { return FShowTitle; }
      set
      {
        FShowTitle = value;
        if (IsDesigning)
          BuildTemplate();
      }
    }

    /// <summary>
    /// Gets or sets a matrix style.
    /// </summary>
    [Editor(typeof(MatrixStyleEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public new string Style
    {
      get { return FStyle; }
      set 
      { 
        FStyle = value;
        Helper.UpdateStyle();
      }
    }

    /// <summary>
    /// Gets or sets even style priority for matrix cells.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(MatrixEvenStylePriority.Rows)]
    public MatrixEvenStylePriority MatrixEvenStylePriority
    {
      get { return FMatrixEvenStylePriority; }
      set { FMatrixEvenStylePriority = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="ManualBuild"/> event.
    /// </summary>
    /// <remarks>
    /// See the <see cref="ManualBuild"/> event for more details.
    /// </remarks>
    [Category("Build")]
    public string ManualBuildEvent
    {
      get { return FManualBuildEvent; }
      set { FManualBuildEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="ModifyResult"/> event.
    /// </summary>
    /// <remarks>
    /// See the <see cref="ModifyResult"/> event for more details.
    /// </remarks>
    [Category("Build")]
    public string ModifyResultEvent
    {
      get { return FModifyResultEvent; }
      set { FModifyResultEvent = value; }
    }

    /// <summary>
    /// Gets the object that holds the collection of descriptors used
    /// to build a matrix.
    /// </summary>
    /// <remarks>
    /// See the <see cref="MatrixData"/> class for more details.
    /// </remarks>
    [Browsable(false)]
    public MatrixData Data
    {
      get { return FData; }
    }

    /// <summary>
    /// Gets or sets array of values that describes the currently printing column.
    /// </summary>
    /// <remarks>
    /// Use this property when report is running. It can be used to highlight matrix elements
    /// depending on values of the currently printing column. To do this:
    /// <list type="bullet">
    ///   <item>
    ///     <description>select the cell that you need to highlight;</description>
    ///   </item>
    ///   <item>
    ///     <description>click the "Highlight" button on the "Text" toolbar;</description>
    ///   </item>
    ///   <item>
    ///     <description>add a new highlight condition. Use the <b>Matrix.ColumnValues</b> to 
    ///     refer to the value you need to analyze. Note: these values are arrays of <b>System.Object</b>, 
    ///     so you need to cast it to actual type before making any comparisons. Example of highlight
    ///     condition: <c>(int)Matrix1.ColumnValues[0] == 2000</c>.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    [Browsable(false)]
    public object[] ColumnValues
    {
      get { return FColumnValues; }
      set { FColumnValues = value; }
    }

    /// <summary>
    /// Gets or sets array of values that describes the currently printing row.
    /// </summary>
    /// <remarks>
    /// Use this property when report is running. It can be used to highlight matrix elements
    /// depending on values of the currently printing row. To do this:
    /// <list type="bullet">
    ///   <item>
    ///     <description>select the cell that you need to highlight;</description>
    ///   </item>
    ///   <item>
    ///     <description>click the "Highlight" button on the "Text" toolbar;</description>
    ///   </item>
    ///   <item>
    ///     <description>add a new highlight condition. Use the <b>Matrix.RowValues</b> to 
    ///     refer to the value you need to analyze. Note: these values are arrays of <b>System.Object</b>, 
    ///     so you need to cast it to actual type before making any comparisons. Example of highlight
    ///     condition: <c>(string)Matrix1.RowValues[0] == "Andrew Fuller"</c>.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    [Browsable(false)]
    public object[] RowValues
    {
      get { return FRowValues; }
      set { FRowValues = value; }
    }

    /// <summary>
    /// Gets or sets the index of currently printing column.
    /// </summary>
    /// <remarks>
    /// This property may be used to print even columns with alternate color. To do this:
    /// <list type="bullet">
    ///   <item>
    ///     <description>select the cell that you need to highlight;</description>
    ///   </item>
    ///   <item>
    ///     <description>click the "Highlight" button on the "Text" toolbar;</description>
    ///   </item>
    ///   <item>
    ///     <description>add a new highlight condition that uses the <b>Matrix.ColumnIndex</b>,
    ///     for example: <c>Matrix1.ColumnIndex % 2 == 1</c>.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    [Browsable(false)]
    public int ColumnIndex
    {
      get { return FColumnIndex; }
      set { FColumnIndex = value; }
    }

    /// <summary>
    /// Gets or sets the index of currently printing row.
    /// </summary>
    /// <remarks>
    /// This property may be used to print even rows with alternate color. To do this:
    /// <list type="bullet">
    ///   <item>
    ///     <description>select the cell that you need to highlight;</description>
    ///   </item>
    ///   <item>
    ///     <description>click the "Highlight" button on the "Text" toolbar;</description>
    ///   </item>
    ///   <item>
    ///     <description>add a new highlight condition that uses the <b>Matrix.RowIndex</b>,
    ///     for example: <c>Matrix1.RowIndex % 2 == 1</c>.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    [Browsable(false)]
    public int RowIndex
    {
      get { return FRowIndex; }
      set { FRowIndex = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override int ColumnCount
    {
      get { return base.ColumnCount; }
      set { base.ColumnCount = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override int RowCount
    {
      get { return base.RowCount; }
      set { base.RowCount = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new int FixedRows
    {
      get { return base.FixedRows; }
      set { base.FixedRows = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new int FixedColumns
    {
      get { return base.FixedColumns; }
      set { base.FixedColumns = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanBreak
    {
      get { return base.CanBreak; }
      set { base.CanBreak = value; }
    }
    
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool GrowToBottom
    {
      get { return base.GrowToBottom; }
      set { base.GrowToBottom = value; }
    }

    internal MatrixStyleSheet StyleSheet
    {
      get { return FStyleSheet; }
    }
    
    private MatrixHelper Helper
    {
      get { return FHelper; }
    }
    
    private bool IsResultMatrix
    {
      get { return !IsDesigning && Data.Columns.Count == 0 && Data.Rows.Count == 0; }
    }
    
    private bool DragCellMode
    {
      get { return FSelectedCell != null && FDragSelectedCell; }
    }

    private BandBase ParentBand
    {
      get
      {
        BandBase parentBand = this.Band;
        if (parentBand is ChildBand)
          parentBand = (parentBand as ChildBand).GetTopParentBand;
        return parentBand;
      }
    }

    private DataBand FootersDataBand
    {
      get
      {
        DataBand dataBand = null;
        if (ParentBand is GroupFooterBand)
          dataBand = ((ParentBand as GroupFooterBand).Parent as GroupHeaderBand).GroupDataBand;
        else if (ParentBand is DataFooterBand)
          dataBand = ParentBand.Parent as DataBand;
        return dataBand;
      }
    }
    
    private bool IsOnFooter
    {
      get 
      { 
        DataBand dataBand = FootersDataBand;
        if (dataBand != null)
        {
          return DataSource == dataBand.DataSource;
        }
        return false;
      }
    }
    #endregion
    
    #region Private Methods
    private void DrawSelectedCellFrame(FRPaintEventArgs e, TableCell cell)
    {
      Pen p = e.Cache.GetPen(Color.Black, 3, System.Drawing.Drawing2D.DashStyle.Solid);
      e.Graphics.DrawRectangle(p, cell.AbsLeft * e.ScaleX, cell.AbsTop * e.ScaleY,
        cell.Width * e.ScaleX, cell.Height * e.ScaleY);
    }

    private void DrawDragIndicator(FRPaintEventArgs e)
    {
      FDragInfo.TargetCell.DrawDragAcceptFrame(e, Color.Red);
      
      if (FDragInfo.Indicator.Width > 0 || FDragInfo.Indicator.Height > 0)
      {
        Graphics g = e.Graphics;
        int left = (int)Math.Round((FDragInfo.Indicator.Left + AbsLeft) * e.ScaleX);
        int top = (int)Math.Round((FDragInfo.Indicator.Top + AbsTop) * e.ScaleY);
        int right = (int)Math.Round(FDragInfo.Indicator.Width * e.ScaleX) + left;
        int bottom = (int)Math.Round(FDragInfo.Indicator.Height * e.ScaleY) + top;

        Pen p = e.Cache.GetPen(Color.Red, 1, DashStyle.Solid);
        g.DrawLine(p, left, top, right, bottom);
        
        p = Pens.Red;
        Brush b = Brushes.Red;
        
        if (FDragInfo.Indicator.Width == 0)
        {
          Point[] poly = new Point[] {
            new Point(left, top),
            new Point(left - 4, top - 4),
            new Point(left + 4, top - 4),
            new Point(left, top) };
          g.FillPolygon(b, poly);
          g.DrawPolygon(p, poly);
          
          poly = new Point[] {
            new Point(left, bottom),
            new Point(left - 4, bottom + 4),
            new Point(left + 4, bottom + 4),
            new Point(left, bottom) };
          g.FillPolygon(b, poly);
          g.DrawPolygon(p, poly);
        }
        else
        {
          Point[] poly = new Point[] {
            new Point(left, top),
            new Point(left - 4, top - 4),
            new Point(left - 4, top + 4),
            new Point(left, top) };
          g.FillPolygon(b, poly);
          g.DrawPolygon(p, poly);

          poly = new Point[] {
            new Point(right, top),
            new Point(right + 4, top - 4),
            new Point(right + 4, top + 4),
            new Point(right, top) };
          g.FillPolygon(b, poly);
          g.DrawPolygon(p, poly);
        }
      }  
    }
    
    private void GetMatrixElement(TableCell cell, out MatrixElement element, out MatrixDescriptor descriptor,
      out bool isTotal)
    {
      element = MatrixElement.None;
      descriptor = null;
      isTotal = false;

      bool noColumns = Data.Columns.Count == 0;
      bool noRows = Data.Rows.Count == 0;
      bool noCells = Data.Cells.Count == 0;

      // create temporary descriptors
      if (noColumns)
        Data.Columns.Add(new MatrixHeaderDescriptor("", false));
      if (noRows)
        Data.Rows.Add(new MatrixHeaderDescriptor("", false));
      if (noCells)
        Data.Cells.Add(new MatrixCellDescriptor());

      Helper.UpdateDescriptors();
      
      foreach (MatrixHeaderDescriptor descr in Data.Columns)
      {
        if (descr.TemplateCell == cell || descr.TemplateTotalCell == cell)
        {
          element = MatrixElement.Column;
          if (!noColumns)
            descriptor = descr;
          isTotal = descr.TemplateTotalCell == cell;
        }
      }
      
      foreach (MatrixHeaderDescriptor descr in Data.Rows)
      {
        if (descr.TemplateCell == cell || descr.TemplateTotalCell == cell)
        {
          element = MatrixElement.Row;
          if (!noRows)
            descriptor = descr;
          isTotal = descr.TemplateTotalCell == cell;
        }
      }
      
      foreach (MatrixCellDescriptor descr in Data.Cells)
      {
        if (descr.TemplateCell == cell)
        {
          element = MatrixElement.Cell;
          if (!noCells)
            descriptor = descr;
        }
      }
      
      if (cell.Address.X >= FixedColumns && cell.Address.Y >= FixedRows)
        element = MatrixElement.Cell;

      if (noColumns)
        Data.Columns.Clear();
      if (noRows)
        Data.Rows.Clear();
      if (noCells)
        Data.Cells.Clear();
    }

    private void RefreshTemplate(bool reset)
    {
      Helper.UpdateDescriptors();

      for (int x = 0; x < Helper.BodyWidth; x++)
      {
        for (int y = 0; y < Helper.BodyHeight; y++)
        {
          TableCell cell = this[x + FixedColumns, y + FixedRows];
          if (reset)
            cell.Text = "";
          else
            cell.SetFlags(Flags.CanEdit, false);
        }
      }

      MatrixElement element;
      MatrixDescriptor descriptor;
      bool isTotal;
      
      for (int x = 0; x < ColumnCount; x++)
      {
        for (int y = 0; y < RowCount; y++)
        {
          TableCell cell = this[x, y];
          GetMatrixElement(cell, out element, out descriptor, out isTotal);
          bool enableSmartTag = descriptor != null && !isTotal;
          cell.SetFlags(Flags.HasSmartTag, enableSmartTag);
        }
      }
    }


    private void CreateResultTable()
    {
      SetResultTable(new TableResult());
      // assign properties from this object. Do not use Assign method: TableResult is incompatible with MatrixObject.
      ResultTable.OriginalComponent = OriginalComponent;
      ResultTable.Alias = Alias;
      ResultTable.Border = Border.Clone();
      ResultTable.Fill = Fill.Clone();
      ResultTable.Bounds = Bounds;
      ResultTable.RepeatHeaders = RepeatHeaders;
      ResultTable.Layout = Layout;
      ResultTable.WrappedGap = WrappedGap;
      ResultTable.AdjustSpannedCellsWidth = AdjustSpannedCellsWidth;
      ResultTable.SetReport(Report);
      ResultTable.AfterData += new EventHandler(ResultTable_AfterData);
    }

    private void DisposeResultTable()
    {
      ResultTable.Dispose();
      SetResultTable(null);
    }

    private void ResultTable_AfterData(object sender, EventArgs e)
    {
      OnModifyResult(e);
    }

    private void DataSource_Disposed(object sender, EventArgs e)
    {
      FDataSource = null;
    }
    
    private void WireEvents(bool wire)
    {
      if (IsOnFooter)
      {
        DataBand dataBand = FootersDataBand;
        if (wire)
          dataBand.BeforePrint += new EventHandler(dataBand_BeforePrint);
        else
          dataBand.BeforePrint -= new EventHandler(dataBand_BeforePrint);
      }
    }

    private void dataBand_BeforePrint(object sender, EventArgs e)
    {
      bool firstRow = (sender as DataBand).IsFirstRow;
      if (firstRow)
        Helper.StartPrint();

      object match = true;
      if (!String.IsNullOrEmpty(Filter))
        match = Report.Calc(Filter);

      if (match is bool && (bool)match == true)
        Helper.AddDataRow();
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void DeserializeSubItems(FRReader reader)
    {
      if (String.Compare(reader.ItemName, "MatrixColumns", true) == 0)
        reader.Read(Data.Columns);
      else if (String.Compare(reader.ItemName, "MatrixRows", true) == 0)
        reader.Read(Data.Rows);
      else if (String.Compare(reader.ItemName, "MatrixCells", true) == 0)
        reader.Read(Data.Cells);
      else
        base.DeserializeSubItems(reader);
    }

    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (AutoSize)
        return new SelectionPoint[] { new SelectionPoint(AbsLeft, AbsTop, SizingPoint.LeftTop) };
      return base.GetSelectionPoints();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      MatrixObject src = source as MatrixObject;
      AutoSize = src.AutoSize;
      CellsSideBySide = src.CellsSideBySide;
      KeepCellsSideBySide = src.KeepCellsSideBySide;
      DataSource = src.DataSource;
      Filter = src.Filter;
      ShowTitle = src.ShowTitle;
      Style = src.Style;
      MatrixEvenStylePriority = src.MatrixEvenStylePriority;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (IsDesigning && AutoSize)
      {
        AutoSize = true;
        CalcHeight();
      }  
      
      base.Draw(e);
      
      if (FSelectedCell != null)
        DrawSelectedCellFrame(e, FSelectedCell);
        
      if (FDragInfo.Target != MatrixElement.None)
        DrawDragIndicator(e);
        
      if (!IsResultMatrix)
        RefreshTemplate(false);
    }

    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      base.SelectionChanged();

      FSelectedCell = null;
      FDragInfo.Source = MatrixElement.None;
      FDragInfo.SourceDescriptor = null;
      if (!IsSelected || Report.Designer.SelectedObjects.Count != 1)
        return;

      TableCell selectedCell = Report.Designer.SelectedObjects[0] as TableCell;
      if (selectedCell == null)
        return;

      bool isTotal;
      GetMatrixElement(selectedCell, out FDragInfo.Source, out FDragInfo.SourceDescriptor, out isTotal);
      
      if (FDragInfo.SourceDescriptor == null || isTotal)
      {
        FDragInfo.Source = MatrixElement.None;
        FDragInfo.SourceDescriptor = null;
      }
      else
        FSelectedCell = selectedCell;
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      if (DragCellMode)
      {
        e.Handled = true;
        e.Mode = WorkspaceMode2.Custom;
        e.ActiveObject = this;
      }
      else  
        base.HandleMouseDown(e);
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      if (DragCellMode && e.Button == MouseButtons.Left)
      {
        e.DragSource = FSelectedCell;
        HandleDragOver(e);
      }
      else
      {
        base.HandleMouseMove(e);

        if (AutoSize && (MouseMode == MouseMode.ResizeColumn || MouseMode == MouseMode.ResizeRow))
        {
          MouseMode = MouseMode.None;
          e.Handled = false;
          e.Mode = WorkspaceMode2.None;
          e.Cursor = Cursors.Default;
        }

        if (FSelectedCell != null && e.Button == MouseButtons.None)
        {
          PointF point = new PointF(e.X, e.Y);
          RectangleF innerRect = FSelectedCell.AbsBounds;
          RectangleF outerRect = innerRect;
          innerRect.Inflate(-3, -3);
          outerRect.Inflate(3, 3);

          FDragSelectedCell = outerRect.Contains(point) && !innerRect.Contains(point);
          if (FDragSelectedCell)
          {
            e.Handled = true;
            e.Cursor = Cursors.SizeAll;
          }  
        }
      }  
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      if (DragCellMode)
      {
        HandleDragDrop(e);
        FSelectedCell = null;
        Report.Designer.SetModified(this, "Change");
      }
      else
        base.HandleMouseUp(e);
    }

    /// <inheritdoc/>
    public override void HandleDragOver(FRMouseEventArgs e)
    {
      // matrix that is defined in the base report cannot be configured such way.
      if (IsAncestor)
        return;
      
      FDragInfo.Target = MatrixElement.None;
      FDragInfo.Indicator = Rectangle.Empty;
      if (!(e.DragSource is TextObject))
        return;

      bool noColumns = Data.Columns.Count == 0;
      bool noRows = Data.Rows.Count == 0;
      bool noCells = Data.Cells.Count == 0;

      // create temporary descriptors
      if (noColumns)
        Data.Columns.Add(new MatrixHeaderDescriptor("", false));
      if (noRows)
        Data.Rows.Add(new MatrixHeaderDescriptor("", false));
      if (noCells)
        Data.Cells.Add(new MatrixCellDescriptor());

      Helper.UpdateDescriptors();
      PointF point = new PointF(e.X, e.Y);
      
      // determine the location where to insert a new item: column, row or cell
      foreach (MatrixHeaderDescriptor descr in Data.Columns)
      {
        TableCell cell = descr.TemplateCell;
        if (cell != e.DragSource && cell.PointInObject(point))
        {
          FDragInfo.Target = MatrixElement.Column;
          FDragInfo.TargetIndex = Data.Columns.IndexOf(descr) + 1;
          FDragInfo.TargetCell = cell;
          float top = cell.Bottom;
          float width = cell.Width;
          
          if (noColumns || point.Y < cell.AbsTop + cell.Height / 2)
          {
            if (descr.TemplateTotalCell != null)
              width += descr.TemplateTotalCell.Width;
            top = cell.Top;
            FDragInfo.TargetIndex--;
          }  

          FDragInfo.Indicator = new RectangleF(cell.Left, top, noColumns ? 0 : width, 0);
          e.DragMessage = Res.Get("ComponentsMisc,Matrix,NewColumn");
        }  
      }
      
      foreach (MatrixHeaderDescriptor descr in Data.Rows)
      {
        TableCell cell = descr.TemplateCell;
        if (cell != e.DragSource && cell.PointInObject(point))
        {
          FDragInfo.Target = MatrixElement.Row;
          FDragInfo.TargetIndex = Data.Rows.IndexOf(descr) + 1;
          FDragInfo.TargetCell = cell;
          float left = cell.Right;
          float height = cell.Height;
          
          if (noRows || point.X < cell.AbsLeft + cell.Width / 2)
          {
            if (descr.TemplateTotalCell != null)
              height += descr.TemplateTotalCell.Height;
            left = cell.Left;
            FDragInfo.TargetIndex--;
          }

          FDragInfo.Indicator = new RectangleF(left, cell.Top, 0, noRows ? 0 : height);
          e.DragMessage = Res.Get("ComponentsMisc,Matrix,NewRow");
        }
      }

      foreach (MatrixCellDescriptor descr in Data.Cells)
      {
        TableCell cell = descr.TemplateCell;
        if (cell != e.DragSource && cell.PointInObject(point))
        {
          FDragInfo.Target = MatrixElement.Cell;
          FDragInfo.TargetCell = cell;
          bool preferLeftRight = Math.Min(point.X - cell.AbsLeft, cell.AbsRight - point.X) < 10;
          
          if (Data.Cells.Count < 2 || CellsSideBySide)
          {
            FDragInfo.TargetIndex = Data.Cells.IndexOf(descr) + 1;
            FDragInfo.CellsSideBySide = true;
            float left = cell.Right;

            if (point.X < cell.AbsLeft + cell.Width / 2)
            {
              left = cell.Left;
              FDragInfo.TargetIndex--;
            }  

            FDragInfo.Indicator = new RectangleF(left, cell.Top, 0, cell.Height);
          }
          if ((Data.Cells.Count < 2 && !preferLeftRight) || (Data.Cells.Count >= 2 && !CellsSideBySide))
          {
            FDragInfo.TargetIndex = Data.Cells.IndexOf(descr) + 1;
            FDragInfo.CellsSideBySide = false;
            float top = cell.Bottom;
            
            if (point.Y < cell.AbsTop + cell.Height / 2)
            {
              top = cell.Top;
              FDragInfo.TargetIndex--;
            }

            FDragInfo.Indicator = new RectangleF(cell.Left, top, cell.Width, 0);
          }
          
          if (noCells)
          {
            FDragInfo.TargetIndex = 0;
            FDragInfo.Indicator = RectangleF.Empty;
          }

          e.DragMessage = Res.Get("ComponentsMisc,Matrix,NewCell");
        }
      }

      if (noColumns)
        Data.Columns.Clear();
      if (noRows)
        Data.Rows.Clear();
      if (noCells)
        Data.Cells.Clear();
      
      e.Handled = PointInObject(point);
    }

    /// <inheritdoc/>
    public override void HandleDragDrop(FRMouseEventArgs e)
    {
      if (FDragInfo.Target != MatrixElement.None)
      {
        string draggedText = (e.DragSource as TextObject).Text;
        MatrixDescriptor sourceDescr = FDragInfo.SourceDescriptor;
        MatrixDescriptor targetDescr = null;

        // insert new item.
        switch (FDragInfo.Target)
        {
          case MatrixElement.Column:
            targetDescr = new MatrixHeaderDescriptor(draggedText);
            if (sourceDescr != null)
              targetDescr.Assign(sourceDescr);
            if (FDragInfo.Source == MatrixElement.Cell)
              targetDescr.TemplateCell = null;
            Data.Columns.Insert(FDragInfo.TargetIndex, targetDescr as MatrixHeaderDescriptor);
            break;

          case MatrixElement.Row:
            targetDescr = new MatrixHeaderDescriptor(draggedText);
            if (sourceDescr != null)
              targetDescr.Assign(sourceDescr);
            if (FDragInfo.Source == MatrixElement.Cell)
              targetDescr.TemplateCell = null;
            Data.Rows.Insert(FDragInfo.TargetIndex, targetDescr as MatrixHeaderDescriptor);
            break;

          case MatrixElement.Cell:
            targetDescr = new MatrixCellDescriptor(draggedText);
            if (sourceDescr != null)
              targetDescr.Assign(sourceDescr);
            if (FDragInfo.Source != MatrixElement.Cell)
              targetDescr.TemplateCell = null;
            CellsSideBySide = FDragInfo.CellsSideBySide;
            Data.Cells.Insert(FDragInfo.TargetIndex, targetDescr as MatrixCellDescriptor);
            break;
        }
        
        // remove source item
        switch (FDragInfo.Source)
        {
          case MatrixElement.Column:
            Data.Columns.Remove(sourceDescr as MatrixHeaderDescriptor);
            break;
            
          case MatrixElement.Row:
            Data.Rows.Remove(sourceDescr as MatrixHeaderDescriptor);
            break;
            
          case MatrixElement.Cell:
            Data.Cells.Remove(sourceDescr as MatrixCellDescriptor);
            break;
        }

        if (DataSource == null)
        {
          if (draggedText.StartsWith("[") && draggedText.EndsWith("]"))
            draggedText = draggedText.Substring(1, draggedText.Length - 2);
          DataSource = DataHelper.GetDataSource(Report.Dictionary, draggedText);
        }  

        Helper.BuildTemplate();
        if (targetDescr != null)
        {
          Report.Designer.SelectedObjects.Clear();
          Report.Designer.SelectedObjects.Add(targetDescr.TemplateCell);
        }
      }

      FDragInfo.Target = MatrixElement.None;
      FDragInfo.Source = MatrixElement.None;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      if (writer.SerializeTo != SerializeTo.SourcePages)
      {
        writer.Write(Data.Columns);
        writer.Write(Data.Rows);
        writer.Write(Data.Cells);
      }  
      else
        RefreshTemplate(true);

      base.Serialize(writer);
      MatrixObject c = writer.DiffObject as MatrixObject;
      
      if (AutoSize != c.AutoSize)
        writer.WriteBool("AutoSize", AutoSize);
      if (CellsSideBySide != c.CellsSideBySide)
        writer.WriteBool("CellsSideBySide", CellsSideBySide);
      if (KeepCellsSideBySide != c.KeepCellsSideBySide)
        writer.WriteBool("KeepCellsSideBySide", KeepCellsSideBySide);
      if (DataSource != c.DataSource)
        writer.WriteRef("DataSource", DataSource);
      if (Filter != c.Filter)
        writer.WriteStr("Filter", Filter);
      if (ShowTitle != c.ShowTitle)
        writer.WriteBool("ShowTitle", ShowTitle);
      if (Style != c.Style)
        writer.WriteStr("Style", Style);
      if (MatrixEvenStylePriority != c.MatrixEvenStylePriority)
        writer.WriteValue("MatrixEvenStylePriority", MatrixEvenStylePriority);
      if (ManualBuildEvent != c.ManualBuildEvent)
        writer.WriteStr("ManualBuildEvent", ManualBuildEvent);
      if (ModifyResultEvent != c.ModifyResultEvent)
        writer.WriteStr("ModifyResultEvent", ModifyResultEvent);
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      BuildTemplate();
    }

    /// <summary>
    /// Creates or updates the matrix template.
    /// </summary>
    /// <remarks>
    /// Call this method after you modify the matrix descriptors using the <see cref="Data"/>
    /// object's properties. 
    /// </remarks>
    public void BuildTemplate()
    {
      Helper.BuildTemplate();
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new MatrixObjectMenu(Report.Designer);
    }

    internal override ContextMenuBar GetCellContextMenu(TableCell cell)
    {
      if (Report.Designer.SelectedObjects.Count == 1)
      {
        MatrixElement element;
        MatrixDescriptor descriptor;
        bool isTotal;
        GetMatrixElement(cell, out element, out descriptor, out isTotal);
        
        switch (element)
        {
          case MatrixElement.Column:
          case MatrixElement.Row:
            if (isTotal)
              return new MatrixTotalMenu(this, element, descriptor);
            else if (descriptor != null)
              return new MatrixHeaderMenu(this, element, descriptor);
            break;  
              
          case MatrixElement.Cell:
            if (descriptor != null)
              return new MatrixCellMenu(this, element, descriptor);
            break;  
        }
      }
      
      return new MatrixCellMenuBase(this, MatrixElement.None, null);
    }

    internal override SmartTagBase GetCellSmartTag(TableCell cell)
    {
      MatrixElement element;
      MatrixDescriptor descriptor;
      bool isTotal;
      GetMatrixElement(cell, out element, out descriptor, out isTotal);

      if (descriptor != null && !isTotal)
        return new MatrixCellSmartTag(this, descriptor);
      return null;
    }

    internal override void HandleCellDoubleClick(TableCell cell)
    {
      MatrixElement element;
      MatrixDescriptor descriptor;
      bool isTotal;
      GetMatrixElement(cell, out element, out descriptor, out isTotal);

      if (descriptor != null && !isTotal)
      {
        using (ExpressionEditorForm form = new ExpressionEditorForm(Report))
        {
          form.ExpressionText = descriptor.Expression;
          if (form.ShowDialog() == DialogResult.OK)
          {
            descriptor.Expression = form.ExpressionText;
            BuildTemplate();
            Report.Designer.SetModified(cell, "Change");
          }
        }
      }
      else
      {
        if (cell.HasFlag(Flags.CanEdit) && !cell.HasRestriction(Restrictions.DontEdit) && cell.InvokeEditor())
          Report.Designer.SetModified(cell, "Change");
      }
    }

    /// <inheritdoc/>
    public override void HandleKeyDown(Control sender, KeyEventArgs e)
    {
      bool myKey = false;
      SelectedObjectCollection selection = Report.Designer.SelectedObjects;
      if (!IsSelected || !(selection[0] is TableCell))
        return;

      TableCell cell = selection[0] as TableCell;
      MatrixElement element;
      MatrixDescriptor descriptor;
      bool isTotal;
      GetMatrixElement(cell, out element, out descriptor, out isTotal);
      
      switch (e.KeyCode)
      {
        case Keys.Delete:
          if (element != MatrixElement.None)
          {
            if (descriptor != null && !IsAncestor)
            {
              if (isTotal)
                (descriptor as MatrixHeaderDescriptor).Totals = false;
              else
              {
                switch (element)
                {
                  case MatrixElement.Column:
                    Data.Columns.Remove(descriptor as MatrixHeaderDescriptor);
                    break;
                    
                  case MatrixElement.Row:
                    Data.Rows.Remove(descriptor as MatrixHeaderDescriptor);
                    break;
                    
                  case MatrixElement.Cell:
                    Data.Cells.Remove(descriptor as MatrixCellDescriptor);
                    break;
                }
              }
            }
            
            myKey = true;
          }
          break;
          
        case Keys.Enter:
          if (descriptor != null && !isTotal)
          {
            HandleCellDoubleClick(cell);
            myKey = true;
          }
          break;
      }
      
      if (myKey)
      {
        e.Handled = true;
        BuildTemplate();
        Report.Designer.SetModified(this, "Change");
      }  
      else
        base.HandleKeyDown(sender, e);
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override void InitializeComponent()
    {
      base.InitializeComponent();
      WireEvents(true);
    }

    /// <inheritdoc/>
    public override void FinalizeComponent()
    {
      base.FinalizeComponent();
      WireEvents(false);
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      Helper.UpdateDescriptors();
      List<MatrixDescriptor> descrList = new List<MatrixDescriptor>();
      descrList.AddRange(Data.Columns.ToArray());
      descrList.AddRange(Data.Rows.ToArray());
      descrList.AddRange(Data.Cells.ToArray());
      
      foreach (MatrixDescriptor descr in descrList)
      {
        expressions.Add(descr.Expression);
        if (descr.TemplateCell != null)
          descr.TemplateCell.AllowExpressions = false;
      }
      
      if (!String.IsNullOrEmpty(Filter))
        expressions.Add(Filter);
      
      return expressions.ToArray();
    }
    
    /// <inheritdoc/>
    public override void SaveState()
    {
      FSaveVisible = Visible;
      BandBase parent = Parent as BandBase;
      if (!Visible || (parent != null && !parent.Visible))
        return;

      // create the result table that will be rendered in the preview
      CreateResultTable();
      Visible = false;

      if (parent != null)
      {
        parent.Height = Top;
        parent.CanGrow = false;
        parent.CanShrink = false;
        parent.AfterPrint += new EventHandler(ResultTable.GeneratePages);
      }
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();

      if (!IsOnFooter)
      {
        Helper.StartPrint();
        Helper.AddDataRows();
      }
      
      Helper.FinishPrint();
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      BandBase parent = Parent as BandBase;
      if (!FSaveVisible || (parent != null && !parent.Visible))
        return;

      if (parent != null)
        parent.AfterPrint -= new EventHandler(ResultTable.GeneratePages);

      DisposeResultTable();
      Visible = FSaveVisible;
    }

    /// <summary>
    /// This method fires the <b>ManualBuild</b> event and the script code connected to the <b>ManualBuildEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnManualBuild(EventArgs e)
    {
      if (ManualBuild != null)
        ManualBuild(this, e);
      InvokeEvent(ManualBuildEvent, e);
    }

    /// <summary>
    /// This method fires the <b>ModifyResult</b> event and the script code connected to the <b>ModifyResultEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnModifyResult(EventArgs e)
    {
      if (ModifyResult != null)
        ModifyResult(this, e);
      InvokeEvent(ModifyResultEvent, e);
    }

    /// <summary>
    /// Adds a value in the matrix.
    /// </summary>
    /// <param name="columnValues">Array of column values.</param>
    /// <param name="rowValues">Array of row values.</param>
    /// <param name="cellValues">Array of data values.</param>
    /// <remarks>
    /// This is a shortcut method to call the matrix <b>Data.AddValue</b>.
    /// See the <see cref="MatrixData.AddValue(object[],object[],object[])"/> method for more details.
    /// </remarks>
    public void AddValue(object[] columnValues, object[] rowValues, object[] cellValues)
    {
      Data.AddValue(columnValues, rowValues, cellValues, 0);
    }

    /// <summary>
    /// Gets the value of the data cell with the specified index.
    /// </summary>
    /// <param name="index">Zero-based index of the data cell.</param>
    /// <returns>The cell's value.</returns>
    /// <remarks>
    /// Use this method in the cell's expression if the cell has custom totals 
    /// (the total function is set to "Custom"). The example:
    /// <para/>Matrix1.Value(0) / Matrix1.Value(1)
    /// <para/>will return the result of dividing the first data cell's value by the second one.
    /// </remarks>
    public Variant Value(int index)
    {
      object value = Helper.CellValues[index];
      if (value == null)
        value = 0;
      return new Variant(value);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="MatrixObject"/> class.
    /// </summary>
    public MatrixObject()
    {
      FAutoSize = true;
      FData = new MatrixData();
      FManualBuildEvent = "";
      FHelper = new MatrixHelper(this);
      FDragInfo = new DragInfo();
      FStyleSheet = new MatrixStyleSheet();
      FStyleSheet.Load(ResourceLoader.GetStream("cross.frss"));
      FStyle = "";
      FFilter = "";
    }
    
    
    private class DragInfo
    {
      public MatrixElement Source;
      public MatrixElement Target;
      public MatrixDescriptor SourceDescriptor;
      public int TargetIndex;
      public RectangleF Indicator;
      public bool CellsSideBySide;
      public TableCell TargetCell;
    }
  }
}
