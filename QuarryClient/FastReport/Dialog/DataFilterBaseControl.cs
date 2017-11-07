using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Data;
using FastReport.Data;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using System.Collections;

namespace FastReport.Dialog
{
  /// <summary>
  /// The base class for all controls that support the data filtering feature.
  /// </summary>
  /// <remarks>
  /// <para/>The data filtering allows you to bind the control to a <see cref="DataColumn"/>. 
  /// It will be automatically filled by data from the datasource's column. When you select/check 
  /// item(s) and close the dialog with OK button, it will filter the datasource according to your selection.
  /// <para/>You can set the filter operation using the <see cref="FilterOperation"/> property.
  /// </remarks>
  public abstract class DataFilterBaseControl : DialogControl
  {
    private bool FAutoFill;
    private bool FAutoFilter;
    private string FDataColumn;
    private string FReportParameter;
    private FilterOperation FFilterOperation;
    private DataFilterBaseControl FDetailControl;
    private DataSourceFilter.FilterElement FFilterElement;
    private string FDataLoadedEvent;

    #region Properties
    /// <summary>
    /// Occurs after the control is filled with data.
    /// </summary>
    public event EventHandler DataLoaded;

    /// <summary>
    /// Gets or sets a value that determines whether to fill the control with data automatically.
    /// </summary>
    /// <remarks>
    /// The default value of this property is <b>true</b>. If you set it to <b>false</b>,
    /// you need to call the <see cref="FillData()"/> method manually.
    /// </remarks>
    [DefaultValue(true)]
    [Category("Data Filtering")]
    public bool AutoFill
    {
      get { return FAutoFill; }
      set { FAutoFill = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to filter the datasource automatically 
    /// when you close the dialog by OK button.
    /// </summary>
    /// <remarks>
    /// The default value of this property is <b>true</b>. If you set it to <b>false</b>,
    /// you need to call the <see cref="FilterData()"/> method manually.
    /// </remarks>
    [DefaultValue(true)]
    [Category("Data Filtering")]
    public bool AutoFilter
    {
      get { return FAutoFilter; }
      set { FAutoFilter = value; }
    }

    /// <summary>
    /// Gets or sets a data column name that will be used to fill this control with data.
    /// </summary>
    /// <remarks>
    /// This property must contain both datasource name and column name, for example:
    /// <b>Orders.OrderID</b>. You also may use relations, for example: <b>Orders.Customers.CompanyName</b>.
    /// </remarks>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Data Filtering")]
    public string DataColumn
    {
      get { return FDataColumn; }
      set { FDataColumn = value; }
    }

    /// <summary>
    /// Gets or sets name of report parameter which value will be set to value contained 
    /// in this control when you close the dialog.
    /// </summary>
    [Editor(typeof(ReportParameterEditor), typeof(UITypeEditor))]
    [Category("Data Filtering")]
    public string ReportParameter
    {
      get { return FReportParameter; }
      set { FReportParameter = value; }
    }

    /// <summary>
    /// Gets or sets a value that specifies the filter operation.
    /// </summary>
    [DefaultValue(FilterOperation.Equal)]
    [Category("Data Filtering")]
    public FilterOperation FilterOperation
    {
      get { return FFilterOperation; }
      set { FFilterOperation = value; }
    }

    /// <summary>
    /// Gets or sets the detail control used in cascaded filtering.
    /// </summary>
    [TypeConverter(typeof(ComponentRefConverter))]
    [Editor(typeof(PageComponentRefEditor), typeof(UITypeEditor))]
    [Category("Data Filtering")]
    public DataFilterBaseControl DetailControl
    {
      get { return FDetailControl; }
      set 
      { 
        FDetailControl = value;
        if (IsDesigning && value != null)
        {
          AutoFilter = false;
          value.AutoFill = false;
        }
      }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="DataLoaded"/> event.
    /// </summary>
    [Category("Events")]
    public string DataLoadedEvent
    {
      get { return FDataLoadedEvent; }
      set { FDataLoadedEvent = value; }
    }
    #endregion

    #region Private Methods
    private void FilterData(DataSourceFilter filter)
    {
      object value = GetValue();
      
      // convert string value to appropriate data format
      if (value is string)
      {
        Column dataColumn = DataHelper.GetColumn(Report.Dictionary, DataColumn);
        if (dataColumn != null)
          value = Convert.ChangeType((string)value, dataColumn.DataType);
      }
      
      FFilterElement = filter.Add(value, FilterOperation);
    }

    private DataSourceFilter GetDataSourceFilter()
    {
      if (!String.IsNullOrEmpty(DataColumn))
      {
        DataSourceBase ds = DataHelper.GetDataSource(Report.Dictionary, DataColumn);
        if (ds != null)
        {
          DataSourceFilter filter = ds.AdditionalFilter[DataColumn] as DataSourceFilter;
          if (filter == null)
          {
            filter = new DataSourceFilter();
            ds.AdditionalFilter[DataColumn] = filter;
          }
          
          return filter;
        }
      }
      
      return null;
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Fills the control with data.
    /// </summary>
    /// <param name="dataSource">The data source.</param>
    /// <param name="column">The data column.</param>
    /// <example>Here is the example of <b>FillData</b> method implementation:
    /// <code>
    /// protected override void FillData(DataSourceBase dataSource, Column column)
    /// {
    ///   Items.Clear();
    ///   dataSource.First();
    ///   while (dataSource.HasMoreRows)
    ///   {
    ///     Items.Add(dataSource[column].ToString());
    ///     dataSource.Next();
    ///   }
    /// }
    /// </code>
    /// </example>
    protected virtual void FillData(DataSourceBase dataSource, Column column)
    {
    }

    /// <summary>
    /// Returns list of values that can be used to fill control with data.
    /// </summary>
    /// <param name="dataSource">The data source.</param>
    /// <param name="column">The data column.</param>
    /// <returns>List of string values.</returns>
    /// <remarks>
    /// This method is used by the <b>FillData</b> method to fill list-type controls
    /// such as ListBox with data. The result list contains distinct values.
    /// </remarks>
    protected string[] GetListOfData(DataSourceBase dataSource, Column column)
    {
      List<string> list = new List<string>();
      Hashtable uniqueValues = new Hashtable();
      
      dataSource.First();
      while (dataSource.HasMoreRows)
      {
        string value = column.Value.ToString();
        if (!uniqueValues.ContainsKey(value))
        {
          list.Add(value);
          uniqueValues.Add(value, null);
        }
        dataSource.Next();
      }

      return list.ToArray();
    }

    /// <summary>
    /// Returns value entered in the control.
    /// </summary>
    /// <returns>The value of type supported by this control.</returns>
    /// <remarks>
    /// This method must return a value entered by the user. For example, TextBox 
    /// control must return its Text property value. If this control supports multi-selection,
    /// return selected values in an array, for example string[] array for CheckedListBox.
    /// </remarks>
    protected abstract object GetValue();

    /// <summary>
    /// Handles the cascaded filter internal logic.
    /// </summary>
    /// <remarks>
    /// This method should be called in your custom dialog control that supports data filtering.
    /// Call it when the value in your control is changed.
    /// </remarks>
    protected void OnFilterChanged()
    {
      if (DetailControl != null)
      {
        ResetFilter();
        if (Enabled)
          FilterData();
        
        DetailControl.ResetFilter();
        if (DetailControl.Enabled)
        {
          DetailControl.FillData(this);
          DetailControl.OnFilterChanged();
        }
      }
    }

    /// <inheritdoc/>
    protected override void OnEnabledChanged()
    {
      base.OnEnabledChanged();
      ResetFilter();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      DataFilterBaseControl c = writer.DiffObject as DataFilterBaseControl;
      base.Serialize(writer);

      if (AutoFill != c.AutoFill)
        writer.WriteBool("AutoFill", AutoFill);
      if (AutoFilter != c.AutoFilter)
        writer.WriteBool("AutoFilter", AutoFilter);
      if (DataColumn != c.DataColumn)
        writer.WriteStr("DataColumn", DataColumn);
      if (ReportParameter != c.ReportParameter)
        writer.WriteStr("ReportParameter", ReportParameter);
      if (FilterOperation != c.FilterOperation)
        writer.WriteValue("FilterOperation", FilterOperation);
      if (DetailControl != c.DetailControl)
        writer.WriteRef("DetailControl", DetailControl);
      if (DataLoadedEvent != c.DataLoadedEvent)
        writer.WriteStr("DataLoadedEvent", DataLoadedEvent);
    }

    /// <inheritdoc/>
    public override void InitializeControl()
    {
      base.InitializeControl();
      if (AutoFill)
        FillData();
    }

    /// <summary>
    /// Fills the control with data from a datasource.
    /// </summary>
    /// <remarks>
    /// Call this method if you set the <see cref="AutoFill"/> property to <b>false</b>.
    /// </remarks>
    public void FillData()
    {
      FillData((DataSourceBase)null);
    }

    /// <summary>
    /// Fills the control with data from a datasource.
    /// </summary>
    /// <param name="parentData">Parent data source</param>
    /// <remarks>
    /// Call this method if you need to implement cascaded filter. In the <b>parentData</b> parameter,
    /// pass the parent data source that will be used to set up master-detail relationship with
    /// data source in this control.
    /// </remarks>
    public void FillData(DataSourceBase parentData)
    {
      if (!String.IsNullOrEmpty(DataColumn))
      {
        Column dataColumn = DataHelper.GetColumn(Report.Dictionary, DataColumn);
        if (dataColumn != null)
        {
          if (parentData != null)
            parentData.Init();
          DataSourceBase dataSource = dataColumn.Parent as DataSourceBase;
          dataSource.Init(parentData, "", null, true);
          FillData(dataSource, dataColumn);
          OnDataLoaded(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Fills the control with data from a datasource.
    /// </summary>
    /// <param name="parentControl">Parent control</param>
    /// <remarks>
    /// Call this method if you need to implement cascaded filter. In the <b>parentControl</b> parameter,
    /// pass the parent control which performs filtering on a parent data source.
    /// </remarks>
    public void FillData(DataFilterBaseControl parentControl)
    {
      if (!String.IsNullOrEmpty(parentControl.DataColumn))
      {
        Column dataColumn = DataHelper.GetColumn(Report.Dictionary, parentControl.DataColumn);
        if (dataColumn != null)
        {
          DataSourceBase dataSource = dataColumn.Parent as DataSourceBase;
          FillData(dataSource);
        }
      }
    }

    /// <summary>
    /// Applies the filter to a datasource.
    /// </summary>
    /// <remarks>
    /// Call this method if you set the <see cref="AutoFilter"/> property to <b>false</b>.
    /// </remarks>
    public void FilterData()
    {
      DataSourceFilter filter = GetDataSourceFilter();
      if (filter != null)
      {
        ResetFilter();
        if (Enabled)
          FilterData(filter);
      }
    }

    /// <summary>
    /// Resets the filter set by this control.
    /// </summary>
    public void ResetFilter()
    {
      DataSourceFilter filter = GetDataSourceFilter();
      if (filter != null)
        filter.Remove(FFilterElement);
      FFilterElement = null;
    }

    internal void SetReportParameter()
    {
      if (!String.IsNullOrEmpty(ReportParameter) && Report != null)
      {
        object value = GetValue();
        Parameter parameter = Report.GetParameter(ReportParameter);
        if (parameter != null)
        {
          if (value is string)
            parameter.AsString = (string)value;
          else
            parameter.Value = value;
        }
      }
    }

    /// <summary>
    /// This method fires the <b>DataLoaded</b> event and the script code connected to the <b>DataLoadedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnDataLoaded(EventArgs e)
    {
      if (DataLoaded != null)
        DataLoaded(this, e);
      InvokeEvent(DataLoadedEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>DataFilterBaseControl</b> class with default settings. 
    /// </summary>
    public DataFilterBaseControl()
    {
      FAutoFill = true;
      FAutoFilter = true;
      FDataColumn = "";
      FReportParameter = "";
      FDataLoadedEvent = "";
    }
  }
}
