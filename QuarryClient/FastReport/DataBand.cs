using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Data;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using System.Data;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// This class represents the Data band.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="DataSource"/> property to connect the band to a datasource. Set the
  /// <see cref="Filter"/> property if you want to filter data rows. The <see cref="Sort"/>
  /// property can be used to sort data rows.
  /// </remarks>
  public class DataBand : BandBase, IHasEditor
  {
    #region Fields
    private DataHeaderBand FHeader;
    private DataFooterBand FFooter;
    private BandCollection FBands;
    private DataSourceBase FDataSource;
    private SortCollection FSort;
    private string FFilter;
    private BandColumns FColumns;
    private bool FPrintIfDetailEmpty;
    private bool FPrintIfDatasourceEmpty;
    private bool FKeepTogether;
    private bool FKeepDetail;
    private string FIdColumn;
    private string FParentIdColumn;
    private float FIndent;
    private bool FKeepSummary;
    private Relation FRelation;
    private bool FCollectChildRows;
    private int FRowCount;
    private int FMaxRows;
    private bool FResetPageNumber;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a header band.
    /// </summary>
    [Browsable(false)]
    public DataHeaderBand Header
    {
      get { return FHeader; }
      set
      {
        SetProp(FHeader, value);
        FHeader = value;
      }
    }

    /// <summary>
    /// Gets a collection of detail bands.
    /// </summary>
    [Browsable(false)]
    public BandCollection Bands
    {
      get { return FBands; }
    }

    /// <summary>
    /// Gets or sets a footer band.
    /// </summary>
    [Browsable(false)]
    public DataFooterBand Footer
    {
      get { return FFooter; }
      set
      {
        SetProp(FFooter, value);
        FFooter = value;
      }
    }

    /// <summary>
    /// Gets or sets a data source.
    /// Please note: data source have to be enabled.
    /// </summary>
    [Category("Data")]
    public DataSourceBase DataSource
    {
      get
      {
          // Simon: 注释原方法
          // 影响：更新参数后数据区数据源没有了
        //if (FDataSource != null && !FDataSource.Enabled)
        //  return null;
        return FDataSource;
      }
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
    /// Gets or sets a number of rows in the virtual data source.
    /// </summary>
    /// <remarks>
    /// Use this property if your data band is not connected to any data source. In this case
    /// the virtual data source with the specified number of rows will be used.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(1)]
    public int RowCount
    {
      get { return FRowCount; }
      set { FRowCount = value; }
    }

    /// <summary>
    /// Limits the maximum number of rows in a datasource. 0 means no limit.
    /// </summary>
    [Category("Data")]
    [DefaultValue(0)]
    public int MaxRows
    {
      get { return FMaxRows; }
      set { FMaxRows = value; }
    }

    /// <summary>
    /// Gets or sets a relation used to establish a master-detail relationship between
    /// this band and its parent.
    /// </summary>
    /// <remarks>
    /// Use this property if there are several relations exist between two data sources.
    /// If there is only one relation (in most cases it is), you can leave this property empty.
    /// </remarks>
    [Category("Data")]
    [Editor(typeof(RelationEditor), typeof(UITypeEditor))]
    public Relation Relation
    {
      get { return FRelation; }
      set { FRelation = value; }
    }

    /// <summary>
    /// Gets the collection of sort conditions.
    /// </summary>
    [Browsable(false)]
    public SortCollection Sort
    {
      get { return FSort; }
    }

    /// <summary>
    /// Gets the row filter expression.
    /// </summary>
    /// <remarks>
    /// This property can contain any valid boolean expression. If the expression returns <b>false</b>,
    /// the corresponding data row will not be printed.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Filter
    {
      get { return FFilter; }
      set { FFilter = value; }
    }

    /// <summary>
    /// Gets the band columns.
    /// </summary>
    [Category("Appearance")]
    public BandColumns Columns
    {
      get { return FColumns; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to print a band if all its detail rows are empty.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PrintIfDetailEmpty
    {
      get { return FPrintIfDetailEmpty; }
      set { FPrintIfDetailEmpty = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to print a band if its datasource is empty.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PrintIfDatasourceEmpty
    {
      get { return FPrintIfDatasourceEmpty; }
      set { FPrintIfDatasourceEmpty = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that all band rows should be printed together on one page.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool KeepTogether
    {
      get { return FKeepTogether; }
      set { FKeepTogether = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the band should be printed together with all its detail rows.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool KeepDetail
    {
      get { return FKeepDetail; }
      set { FKeepDetail = value; }
    }

    /// <summary>
    /// Gets or sets the key column that identifies the data row.
    /// </summary>
    /// <remarks>
    /// <para>This property is used when printing a hierarchic list.</para>
    /// <para>To print the hierarchic list, you have to setup three properties: <b>IdColumn</b>,
    /// <b>ParentIdColumn</b> and <b>Indent</b>. First two properties are used to identify the data
    /// row and its parent; the <b>Indent</b> property specifies the indent that will be used to shift
    /// the databand according to its hierarchy level.</para>
    /// <para/>When printing hierarchy, FastReport shifts the band to the right
    /// (by value specified in the <see cref="Indent"/> property), and also decreases the
    /// width of the band by the same value. You may use the <b>Anchor</b> property of the
    /// objects on a band to indicate whether the object should move with the band, or stay
    /// on its original position, or shrink.
    /// </remarks>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Hierarchy")]
    public string IdColumn
    {
      get { return FIdColumn; }
      set { FIdColumn = value; }
    }

    /// <summary>
    /// Gets or sets the column that identifies the parent data row.
    /// </summary>
    /// <remarks>
    /// This property is used when printing a hierarchic list. See description of the
    /// <see cref="IdColumn"/> property for more details.
    /// </remarks>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Hierarchy")]
    public string ParentIdColumn
    {
      get { return FParentIdColumn; }
      set { FParentIdColumn = value; }
    }

    /// <summary>
    /// Gets or sets the indent that will be used to shift the databand according to its hierarchy level.
    /// </summary>
    /// <remarks>
    /// This property is used when printing a hierarchic list. See description of the
    /// <see cref="IdColumn"/> property for more details.
    /// </remarks>
    [TypeConverter(typeof(UnitsConverter))]
    [DefaultValue(37.8f)]
    [Category("Hierarchy")]
    public float Indent
    {
      get { return FIndent; }
      set { FIndent = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the databand should collect child data rows.
    /// </summary>
    /// <remarks>
    /// This property determines how the master-detail report is printed. Default behavior is:
    /// <para/>MasterData row1
    /// <para/>-- DetailData row1
    /// <para/>-- DetailData row2
    /// <para/>-- DetailData row3
    /// <para/>MasterData row2
    /// <para/>-- DetailData row1
    /// <para/>-- DetailData row2
    /// <para/>When you set this property to <b>true</b>, the master databand will collect all child data rows
    /// under a single master data row:
    /// <para/>MasterData row1
    /// <para/>-- DetailData row1
    /// <para/>-- DetailData row2
    /// <para/>-- DetailData row3
    /// <para/>-- DetailData row4
    /// <para/>-- DetailData row5
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool CollectChildRows
    {
      get { return FCollectChildRows; }
      set { FCollectChildRows = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to reset the page numbers when this band starts print.
    /// </summary>
    /// <remarks>
    /// Typically you should set the <see cref="BandBase.StartNewPage"/> property to <b>true</b> as well.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ResetPageNumber
    {
      get { return FResetPageNumber; }
      set { FResetPageNumber = value; }
    }

    internal bool IsDeepmostDataBand
    {
      get { return Bands.Count == 0; }
    }

    internal bool KeepSummary
    {
      get { return FKeepSummary; }
      set { FKeepSummary = value; }
    }

    internal bool IsHierarchical
    {
      get
      {
        return !String.IsNullOrEmpty(IdColumn) && !String.IsNullOrEmpty(ParentIdColumn);
      }
    }

    internal bool IsDatasourceEmpty
    {
      get { return DataSource == null || DataSource.RowCount == 0; }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeColumns()
    {
      return Columns.Count > 1;
    }

    private void DataSource_Disposed(object sender, EventArgs e)
    {
      FDataSource = null;
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void DeserializeSubItems(FRReader reader)
    {
      if (String.Compare(reader.ItemName, "Sort", true) == 0)
        reader.Read(Sort);
      else
        base.DeserializeSubItems(reader);
    }
    #endregion

    #region IParent
    /// <inheritdoc/>
    public override void GetChildObjects(ObjectCollection list)
    {
      base.GetChildObjects(list);
      if (IsRunning)
        return;

      list.Add(FHeader);
      foreach (BandBase band in FBands)
      {
        list.Add(band);
      }
      list.Add(FFooter);
    }

    /// <inheritdoc/>
    public override bool CanContain(Base child)
    {
      return base.CanContain(child) || (child is DataHeaderBand || child is DataFooterBand ||
        child is DataBand || child is GroupHeaderBand);
    }

    /// <inheritdoc/>
    public override void AddChild(Base child)
    {
      if (IsRunning)
      {
        base.AddChild(child);
        return;
      }
      if (child is DataHeaderBand)
        Header = child as DataHeaderBand;
      else if (child is DataFooterBand)
        Footer = child as DataFooterBand;
      else if (child is DataBand || child is GroupHeaderBand)
        FBands.Add(child as BandBase);
      else
        base.AddChild(child);
    }

    /// <inheritdoc/>
    public override void RemoveChild(Base child)
    {
      base.RemoveChild(child);
      if (IsRunning)
        return;

      if (child is DataHeaderBand && FHeader == child as DataHeaderBand)
        Header = null;
      if (child is DataFooterBand && FFooter == child as DataFooterBand)
        Footer = null;
      if (child is DataBand || child is GroupHeaderBand)
        FBands.Remove(child as BandBase);
    }

    /// <inheritdoc/>
    public override int GetChildOrder(Base child)
    {
      if (child is BandBase && !IsRunning)
        return FBands.IndexOf(child as BandBase);
      return base.GetChildOrder(child);
    }

    /// <inheritdoc/>
    public override void SetChildOrder(Base child, int order)
    {
      if (child is BandBase && !IsRunning)
      {
        if (order > FBands.Count)
          order = FBands.Count;
        int oldOrder = child.ZOrder;
        if (oldOrder != -1 && order != -1 && oldOrder != order)
        {
          if (oldOrder <= order)
            order--;
          FBands.Remove(child as BandBase);
          FBands.Insert(order, child as BandBase);
        }
      }
      else
        base.SetChildOrder(child, order);
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);

      DataBand src = source as DataBand;
      DataSource = src.DataSource;
      RowCount = src.RowCount;
      MaxRows = src.MaxRows;
      Relation = src.Relation;
      Sort.Assign(src.Sort);
      Filter = src.Filter;
      Columns.Assign(src.Columns);
      PrintIfDetailEmpty = src.PrintIfDetailEmpty;
      PrintIfDatasourceEmpty = src.PrintIfDatasourceEmpty;
      KeepTogether = src.KeepTogether;
      KeepDetail = src.KeepDetail;
      IdColumn = src.IdColumn;
      ParentIdColumn = src.ParentIdColumn;
      Indent = src.Indent;
      CollectChildRows = src.CollectChildRows;
      ResetPageNumber = src.ResetPageNumber;
    }

    internal override void UpdateWidth()
    {
      if (Columns.Count > 1)
      {
        Width = Columns.ActualWidth;
      }
      else if (!String.IsNullOrEmpty(IdColumn) && !String.IsNullOrEmpty(ParentIdColumn))
      {
        if (PageWidth != 0)
          Width = PageWidth - Left;
      }
      else
        base.UpdateWidth();
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      DataBand c = writer.DiffObject as DataBand;
      base.Serialize(writer);
      if (writer.SerializeTo == SerializeTo.Preview)
        return;

      if (DataSource != c.DataSource)
        writer.WriteRef("DataSource", DataSource);
      if (RowCount != c.RowCount)
        writer.WriteInt("RowCount", RowCount);
      if (MaxRows != c.MaxRows)
        writer.WriteInt("MaxRows", MaxRows);
      if (Relation != c.Relation)
        writer.WriteRef("Relation", Relation);
      if (Sort.Count > 0)
        writer.Write(Sort);
      if (Filter != c.Filter)
        writer.WriteStr("Filter", Filter);
      Columns.Serialize(writer, c.Columns);
      if (PrintIfDetailEmpty != c.PrintIfDetailEmpty)
        writer.WriteBool("PrintIfDetailEmpty", PrintIfDetailEmpty);
      if (PrintIfDatasourceEmpty != c.PrintIfDatasourceEmpty)
        writer.WriteBool("PrintIfDatasourceEmpty", PrintIfDatasourceEmpty);
      if (KeepTogether != c.KeepTogether)
        writer.WriteBool("KeepTogether", KeepTogether);
      if (KeepDetail != c.KeepDetail)
        writer.WriteBool("KeepDetail", KeepDetail);
      if (IdColumn != c.IdColumn)
        writer.WriteStr("IdColumn", IdColumn);
      if (ParentIdColumn != c.ParentIdColumn)
        writer.WriteStr("ParentIdColumn", ParentIdColumn);
      if (FloatDiff(Indent, c.Indent))
        writer.WriteFloat("Indent", Indent);
      if (CollectChildRows != c.CollectChildRows)
        writer.WriteBool("CollectChildRows", CollectChildRows);
      if (ResetPageNumber != c.ResetPageNumber)
        writer.WriteBool("ResetPageNumber", ResetPageNumber);
    }

    /// <inheritdoc/>
    public override void Delete()
    {
      if (!CanDelete)
        return;
      // remove only this band, keep its subbands
      if (Parent is ReportPage || Parent is DataBand)
      {
        Base parent = Parent;
        int zOrder = ZOrder;
        Parent = null;
        while (Bands.Count > 0)
        {
          BandBase band = Bands[Bands.Count - 1];
          band.Parent = parent;
          band.ZOrder = zOrder;
        }
        Dispose();
      }
    }

    /// <inheritdoc/>
    public override SmartTagBase GetSmartTag()
    {
      return new DataBandSmartTag(this);
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new DataBandMenu(Report.Designer);
    }

    internal override string GetInfoText()
    {
      return DataSource == null ? "" : DataSource.FullName;
    }

    /// <inheritdoc/>
    public bool InvokeEditor()
    {
      using (DataBandEditorForm form = new DataBandEditorForm(this))
      {
        return form.ShowDialog() == DialogResult.OK;
      }
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> list = new List<string>();
      foreach (Sort sort in Sort)
      {
        list.Add(sort.Expression);
      }
      list.Add(Filter);
      return list.ToArray();
    }

    /// <summary>
    /// Initializes the data source connected to this band.
    /// </summary>
    public void InitDataSource()
    {
      if (DataSource == null)
      {
        DataSource = new VirtualDataSource();
        DataSource.SetReport(Report);
      }

      if (DataSource is VirtualDataSource)
        (DataSource as VirtualDataSource).VirtualRowsCount = RowCount;

      DataSourceBase parentDataSource = ParentDataBand == null ? null : ParentDataBand.DataSource;
      bool collectChildRows = ParentDataBand == null ? false : ParentDataBand.CollectChildRows;
      if (Relation != null)
        DataSource.Init(Relation, Filter, Sort, collectChildRows);
      else
        DataSource.Init(parentDataSource, Filter, Sort, collectChildRows);
    }

    internal bool IsDetailEmpty()
    {
      if (PrintIfDetailEmpty || Bands.Count == 0)
        return false;

      foreach (BandBase band in Bands)
      {
        if (!band.IsEmpty())
          return false;
      }
      return true;
    }

    internal override bool IsEmpty()
    {
      InitDataSource();
      if (IsDatasourceEmpty)
        return !PrintIfDatasourceEmpty;

      DataSource.First();
      while (DataSource.HasMoreRows)
      {
        if (!IsDetailEmpty())
          return false;
        DataSource.Next();
      }
      return true;
    }

    /// <inheritdoc/>
    public override void InitializeComponent()
    {
      base.InitializeComponent();
      KeepSummary = false;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="DataBand"/> class.
    /// </summary>
    public DataBand()
    {
      FBands = new BandCollection(this);
      FSort = new SortCollection();
      FFilter = "";
      FColumns = new BandColumns(this);
      FIdColumn = "";
      FParentIdColumn = "";
      FIndent = 37.8f;
      FRowCount = 1;
      SetFlags(Flags.HasSmartTag, true);
    }
  }
}