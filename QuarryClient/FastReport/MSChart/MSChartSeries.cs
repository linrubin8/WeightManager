using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using FastReport.TypeEditors;
using FastReport.Data;
using FastReport.Utils;

namespace FastReport.MSChart
{
  /// <summary>
  /// Specifies how the series points are sorted.
  /// </summary>
  public enum SortBy
  {
    /// <summary>
    /// Points are not sorted.
    /// </summary>
    None,
    
    /// <summary>
    /// Points are sorted by X value.
    /// </summary>
    XValue,

    /// <summary>
    /// Points are sorted by Y value.
    /// </summary>
    YValue
  }

  /// <summary>
  /// Specifies the direction in which the series points are sorted.
  /// </summary>
  public enum ChartSortOrder
  {
    /// <summary>
    /// Points are sorted in ascending order.
    /// </summary>
    Ascending,

    /// <summary>
    /// Points are sorted in descending order.
    /// </summary>
    Descending
  }

  /// <summary>
  /// Specifies how the series points are grouped.
  /// </summary>
  public enum GroupBy
  {
    /// <summary>
    /// Points are not grouped.
    /// </summary>
    None,

    /// <summary>
    /// Points are grouped by X value.
    /// </summary>
    XValue,

    /// <summary>
    /// Points are grouped by number specified in the <see cref="MSChartSeries.GroupInterval"/>.
    /// </summary>
    Number,

    /// <summary>
    /// Points are grouped by Years.
    /// </summary>
    Years,

    /// <summary>
    /// Points are grouped by Months.
    /// </summary>
    Months,

    /// <summary>
    /// Points are grouped by Weeks.
    /// </summary>
    Weeks,

    /// <summary>
    /// Points are grouped by Days.
    /// </summary>
    Days,

    /// <summary>
    /// Points are grouped by Hours.
    /// </summary>
    Hours,

    /// <summary>
    /// Points are grouped by Minutes.
    /// </summary>
    Minutes,

    /// <summary>
    /// Points are grouped by Seconds.
    /// </summary>
    Seconds,

    /// <summary>
    /// Points are grouped by Milliseconds.
    /// </summary>
    Milliseconds
  }
  
  /// <summary>
  /// Specifies which pie value to explode.
  /// </summary>
  public enum PieExplode
  {
    /// <summary>
    /// Do not explode pie values.
    /// </summary>
    None,

    /// <summary>
    /// Explode the biggest value.
    /// </summary>
    BiggestValue,

    /// <summary>
    /// Explode the lowest value.
    /// </summary>
    LowestValue,

    /// <summary>
    /// Explode the value specified in the <see cref="MSChartSeries.PieExplodeValue"/> property.
    /// </summary>
    SpecificValue
  }
  
  /// <summary>
  /// Specifies which data points to collect into one point.
  /// </summary>
  public enum Collect
  {
    /// <summary>
    /// Do not collect points.
    /// </summary>
    None,
    
    /// <summary>
    /// Show top N points (<i>N</i> value is specified in the <see cref="MSChartSeries.CollectValue"/>
    /// property), collect other points into one.
    /// </summary>
    TopN,

    /// <summary>
    /// Show bottom N points (<i>N</i> value is specified in the <see cref="MSChartSeries.CollectValue"/>
    /// property), collect other points into one.
    /// </summary>
    BottomN,

    /// <summary>
    /// Collect points which have Y value less than specified 
    /// in the <see cref="MSChartSeries.CollectValue"/> property.
    /// </summary>
    LessThan,

    /// <summary>
    /// Collect points which have Y value less than percent specified 
    /// in the <see cref="MSChartSeries.CollectValue"/> property.
    /// </summary>
    LessThanPercent,

    /// <summary>
    /// Collect points which have Y value greater than specified 
    /// in the <see cref="MSChartSeries.CollectValue"/> property.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Collect points which have Y value greater than percent specified 
    /// in the <see cref="MSChartSeries.CollectValue"/> property.
    /// </summary>
    GreaterThanPercent
  }

  
  /// <summary>
  /// Represents a MS Chart series wrapper.
  /// </summary>
  /// <remarks>
  /// This class provides a data for MS Chart series. The series itself is stored inside the
  /// MS Chart and is accessible via the <see cref="SeriesSettings"/> property.
  /// <para/>You don't need to create an instance of this class directly. Instead, use the
  /// <see cref="MSChartObject.AddSeries"/> method. 
  /// </remarks>
  public class MSChartSeries : Base
  {
    #region Fields
    private string FFilter;
    private SortBy FSortBy;
    private ChartSortOrder FSortOrder;
    private GroupBy FGroupBy;
    private float FGroupInterval;
    private TotalType FGroupFunction;
    private Collect FCollect;
    private float FCollectValue;
    private string FCollectedItemText;
    private Color FCollectedItemColor;
    private PieExplode FPieExplode;
    private string FPieExplodeValue;

    private string FXValue;
    private string FYValue1;
    private string FYValue2;
    private string FYValue3;
    private string FYValue4;
    private string FColor;
    private string FLabel;
    private bool FAutoSeriesForce;
    private string FAutoSeriesColumn;
    #endregion

    #region Properties
    /// <summary>
    /// Gets os sets the data filter expression.
    /// </summary>
    /// <remarks>
    /// The filter is applied for this series only. You can also use the
    /// <see cref="MSChartObject.Filter"/> property to set a filter that will be applied to all
    /// series in a chart.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Filter
    {
      get { return FFilter; }
      set { FFilter = value; }
    }

    /// <summary>
    /// Gets or set the data column or expression for automatically created series.
    /// </summary>
    /// <remarks>
    /// In order to create auto-series, you need to define one series that will be used as a 
    /// template for new series, and set up the <see cref="AutoSeriesColumn"/> property.
    /// The value of this property will be a name of new series. If there is no series 
    /// with such name yet, the new series will be added.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string AutoSeriesColumn
    {
      get { return FAutoSeriesColumn; }
      set { FAutoSeriesColumn = value; }
    }

    /// <summary>
    /// Gets or sets the sort method used to sort data points.
    /// </summary>
    /// <remarks>
    /// You have to specify the <see cref="SortOrder"/> property as well. Data points in this series
    /// will be sorted according selected sort criteria and order.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(SortBy.None)]
    public SortBy SortBy
    {
      get { return FSortBy; }
      set { FSortBy = value; }
    }

    /// <summary>
    /// Gets or set Force automatically created series.
    /// </summary>
    [Category("Data")]
    [DefaultValue(true)]
    public bool AutoSeriesForce
    {
      get { return FAutoSeriesForce; }
      set { FAutoSeriesForce = value; }
    }

    /// <summary>
    /// Gets or sets the sort order used to sort data points.
    /// </summary>
    /// <remarks>
    /// You have to specify the <see cref="SortBy"/> property as well. Data points in this series
    /// will be sorted according selected sort criteria and order.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(ChartSortOrder.Ascending)]
    public ChartSortOrder SortOrder
    {
      get { return FSortOrder; }
      set { FSortOrder = value; }
    }

    /// <summary>
    /// Gets or sets the group method used to group data points.
    /// </summary>
    /// <remarks>
    /// This property is mainly used when series is filled with data with several identical X values.
    /// In this case, you need to set the property to <b>XValue</b>. All identical data points will be 
    /// grouped into one point, their Y values will be summarized. You can choose the summary function
    /// using the <see cref="GroupFunction"/> property.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(GroupBy.None)]
    public GroupBy GroupBy
    {
      get { return FGroupBy; }
      set { FGroupBy = value; }
    }

    /// <summary>
    /// Gets or sets the group interval.
    /// </summary>
    /// <remarks>
    /// This value is used if <see cref="GroupBy"/> property is set to <b>Number</b>.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(1f)]
    public float GroupInterval
    {
      get { return FGroupInterval; }
      set { FGroupInterval = value; }
    }

    /// <summary>
    /// Gets or sets the function used to group data points.
    /// </summary>
    [Category("Data")]
    [DefaultValue(TotalType.Sum)]
    public TotalType GroupFunction
    {
      get { return FGroupFunction; }
      set { FGroupFunction = value; }
    }

    /// <summary>
    /// Gets or sets the collect method used to collect several data points into one.
    /// </summary>
    /// <remarks>
    /// This instrument for data processing allows to collect several series points into one point.
    /// The collected point will be displaed using the text specified in the <see cref="CollectedItemText"/>
    /// property and color specified in the <see cref="CollectedItemColor"/> property.
    /// <para/>For example, to display top 5 values, set this property to <b>TopN</b> and specify
    /// N value (5) in the <see cref="CollectValue"/> property.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(Collect.None)]
    public Collect Collect
    {
      get { return FCollect; }
      set { FCollect = value; }
    }

    /// <summary>
    /// Gets or sets the collect value used to collect several data points into one.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="Collect"/> property is set to any value other than <b>None</b>.
    /// </remarks>
    [Category("Data")]
    [DefaultValue(0f)]
    public float CollectValue
    {
      get { return FCollectValue; }
      set { FCollectValue = value; }
    }

    /// <summary>
    /// Gets or sets the text for the collected value.
    /// </summary>
    [Category("Data")]
    public string CollectedItemText
    {
      get { return FCollectedItemText; }
      set { FCollectedItemText = value; }
    }

    /// <summary>
    /// Gets or sets the color for the collected value.
    /// </summary>
    /// <remarks>
    /// If this property is set to <b>Transparent</b> (by default), the default palette color
    /// will be used to display a collected point.
    /// </remarks>
    [Category("Data")]
    public Color CollectedItemColor
    {
      get { return FCollectedItemColor; }
      set { FCollectedItemColor = value; }
    }

    /// <summary>
    /// Gets or sets the method used to explode values in pie-type series.
    /// </summary>
    [Category("Data")]
    [DefaultValue(PieExplode.None)]
    public PieExplode PieExplode
    {
      get { return FPieExplode; }
      set { FPieExplode = value; }
    }

    /// <summary>
    /// Gets or sets the value that must be exploded.
    /// </summary>
    /// <remarks>
    /// This property is used if <see cref="PieExplode"/> property is set
    /// to <b>SpecificValue</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string PieExplodeValue
    {
      get { return FPieExplodeValue; }
      set { FPieExplodeValue = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression that returns the X value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string XValue
    {
      get { return FXValue; }
      set { FXValue = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression that returns the first Y value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string YValue1
    {
      get { return FYValue1; }
      set { FYValue1 = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression returns the second Y value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string YValue2
    {
      get { return FYValue2; }
      set { FYValue2 = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression returns the third Y value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string YValue3
    {
      get { return FYValue3; }
      set { FYValue3 = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression returns the fourth Y value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string YValue4
    {
      get { return FYValue4; }
      set { FYValue4 = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression that returns the color of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Color
    {
      get { return FColor; }
      set { FColor = value; }
    }

    /// <summary>
    /// Gets or sets the data column or expression returns the label value of data point.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Label
    {
      get { return FLabel; }
      set { FLabel = value; }
    }

    /// <summary>
    /// Gets a reference to MS Chart <b>Series</b> object.
    /// </summary>
    /// <remarks>
    /// Use this property to set many options available for the <b>Series</b> object. These options
    /// include: visual appearance, labels, marks, value types. Refer to the Microsoft Chart control
    /// documentation to learn more.
    /// </remarks>
    [Category("Appearance")]
    public Series SeriesSettings
    {
      get 
      { 
        MSChartObject parent = Parent as MSChartObject;
        if (parent == null)
          return null;
        int index = parent.Series.IndexOf(this);
        if (index < parent.Chart.Series.Count)
          return parent.Chart.Series[index];
        return null;  
      }
    }
    
    /// <summary>
    /// Gets a number of Y value per data point.
    /// </summary>
    /// <remarks>
    /// Number of Y values depends on series type. Most of series have only one Y value. Financial
    /// series such as Stock and Candlestick, use four Y values.
    /// </remarks>
    [Browsable(false)]
    public int YValuesPerPoint
    {
      get { return SeriesSettings.YValuesPerPoint; }
    }
    
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Restrictions Restrictions
    {
      get { return base.Restrictions; }
      set { base.Restrictions = value; }
    }
    #endregion

    #region Private Methods
    private object Calc(string expression)
    {
      if (!String.IsNullOrEmpty(expression))
        return Report.Calc(expression);
      return null;  
    }

    private void InternalProcessData()
    {
      object xValue = Calc(XValue);
      if (xValue != null)
      {
        string[] expressions = new string[] { YValue1, YValue2, YValue3, YValue4 };
        object[] values = new object[YValuesPerPoint];

        for (int i = 0; i < YValuesPerPoint; i++)
        {
          values[i] = Calc(expressions[i]);
        }

        DataPoint point = SeriesSettings.Points[SeriesSettings.Points.AddXY(xValue, values)];
        
        // color
        object color = Calc(Color);
        if (color != null)
        {
          if (color is System.Drawing.Color)
            point.Color = (System.Drawing.Color)color;
          else if (color is Int32)
            point.Color = System.Drawing.Color.FromArgb(255, System.Drawing.Color.FromArgb((int)color));
          else if (color is String)
            point.Color = System.Drawing.Color.FromName((string)color);
        }

        // label
        object label = Calc(Label);
        if (label != null)
          point.Label = label.ToString();
      }  
    }

    private void SortData()
    {
      SeriesSettings.Sort(new DataPointComparer(SortBy, SortOrder));
    }

    private void CollectValues()
    {
      double others = 0;

      if (Collect == Collect.TopN || Collect == Collect.BottomN)
      {
        for (int i = (int)CollectValue; i < SeriesSettings.Points.Count; )
        {
          others += SeriesSettings.Points[i].YValues[0];
          SeriesSettings.Points.RemoveAt(i);
        }
      }
      else
      {
        double totalValue = 0;
        if (Collect == Collect.LessThanPercent || Collect == Collect.GreaterThanPercent)
        {
          foreach (DataPoint point in SeriesSettings.Points)
          {
            totalValue += point.YValues[0];
          }
        }

        for (int i = 0; i < SeriesSettings.Points.Count; )
        {
          double value = SeriesSettings.Points[i].YValues[0];
          if ((Collect == Collect.LessThan && value < CollectValue) ||
            (Collect == Collect.GreaterThan && value > CollectValue) ||
            (Collect == Collect.LessThanPercent && value / totalValue * 100 < CollectValue) ||
            (Collect == Collect.GreaterThanPercent && value / totalValue * 100 > CollectValue))
          {
            others += value;
            SeriesSettings.Points.RemoveAt(i);
          }
          else
            i++;
        }
      }

      if (others > 0 && !String.IsNullOrEmpty(CollectedItemText))
      {
        SeriesSettings.Points.AddXY(CollectedItemText, others);
        DataPoint point = SeriesSettings.Points[SeriesSettings.Points.Count - 1];
        if (CollectedItemColor != System.Drawing.Color.Transparent)
          point.Color = CollectedItemColor;
      }  
    }
    
    private void GroupData()
    {
      Chart chart = (Parent as MSChartObject).Chart;
      string function = "";

      switch (GroupFunction)
      {
        case TotalType.Sum:
          function = "SUM";
          break;
          
        case TotalType.Min:
          function = "MIN";
          break;

        case TotalType.Max:
          function = "MAX";
          break;

        case TotalType.Avg:
          function = "AVE";
          break;

        case TotalType.Count:
          function = "COUNT";
          break;
      }
      
      if (GroupBy == GroupBy.XValue)
        chart.DataManipulator.GroupByAxisLabel(function, SeriesSettings);
      else
      {
        IntervalType type = IntervalType.Number;
        switch (GroupBy)
        {
          case GroupBy.Years:
            type = IntervalType.Years;
            break;

          case GroupBy.Months:
            type = IntervalType.Months;
            break;

          case GroupBy.Weeks:
            type = IntervalType.Weeks;
            break;

          case GroupBy.Days:
            type = IntervalType.Days;
            break;

          case GroupBy.Hours:
            type = IntervalType.Hours;
            break;

          case GroupBy.Minutes:
            type = IntervalType.Minutes;
            break;

          case GroupBy.Seconds:
            type = IntervalType.Seconds;
            break;

          case GroupBy.Milliseconds:
            type = IntervalType.Milliseconds;
            break;
        }

        chart.DataManipulator.Group(function, GroupInterval, type, SeriesSettings);
      }  
    }
    
    private void ExplodePoint()
    {
      if (SeriesSettings.Points.Count == 0)
        return;
        
      if (PieExplode == PieExplode.SpecificValue)
      {
        object pieExplodeValue = Calc(PieExplodeValue);
        if (pieExplodeValue != null)
        {
          foreach (DataPoint point in SeriesSettings.Points)
          {
            if (point.AxisLabel == pieExplodeValue.ToString())
            {
              point["Exploded"] = "true";
              break;
            }
          }
        }
      }
      else
      {
        List<DataPoint> points = new List<DataPoint>();
        foreach (DataPoint point in SeriesSettings.Points)
        {
          points.Add(point);
        }

        points.Sort(new DataPointComparer(SortBy.YValue, ChartSortOrder.Ascending));

        DataPoint explodePoint = null;
        if (PieExplode == PieExplode.BiggestValue)
          explodePoint = points[points.Count - 1];
        else
          explodePoint = points[0];

        explodePoint["Exploded"] = "true";
      }
    }
    #endregion
    
    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      MSChartSeries src = source as MSChartSeries;

      Filter = src.Filter;
      SortOrder = src.SortOrder;
      SortBy = src.SortBy;
      GroupBy = src.GroupBy;
      GroupInterval = src.GroupInterval;
      GroupFunction = src.GroupFunction;
      Collect = src.Collect;
      CollectValue = src.CollectValue;
      CollectedItemText = src.CollectedItemText;
      CollectedItemColor = src.CollectedItemColor;
      PieExplode = src.PieExplode;
      PieExplodeValue = src.PieExplodeValue;
      AutoSeriesForce = src.AutoSeriesForce;
      AutoSeriesColumn = src.AutoSeriesColumn;

      XValue = src.XValue;
      YValue1 = src.YValue1;
      YValue2 = src.YValue2;
      YValue3 = src.YValue3;
      YValue4 = src.YValue4;
      Color = src.Color;
      Label = src.Label;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      MSChartSeries s = writer.DiffObject as MSChartSeries;
      base.Serialize(writer);

      if (Filter != s.Filter)
        writer.WriteStr("Filter", Filter);
      if (SortOrder != s.SortOrder)
        writer.WriteValue("SortOrder", SortOrder);
      if (SortBy != s.SortBy)
        writer.WriteValue("SortBy", SortBy);
      if (GroupBy != s.GroupBy)
        writer.WriteValue("GroupBy", GroupBy);
      if (GroupInterval != s.GroupInterval)
        writer.WriteFloat("GroupInterval", GroupInterval);
      if (GroupFunction != s.GroupFunction)
        writer.WriteValue("GroupFunction", GroupFunction);
      if (Collect != s.Collect)
        writer.WriteValue("Collect", Collect);
      if (CollectValue != s.CollectValue)
        writer.WriteFloat("CollectValue", CollectValue);
      if (CollectedItemText != s.CollectedItemText)
        writer.WriteStr("CollectedItemText", CollectedItemText);
      if (CollectedItemColor != s.CollectedItemColor)
        writer.WriteValue("CollectedItemColor", CollectedItemColor);
      if (PieExplode != s.PieExplode)
        writer.WriteValue("PieExplode", PieExplode);
      if (PieExplodeValue != s.PieExplodeValue)
        writer.WriteStr("PieExplodeValue", PieExplodeValue);

      if (XValue != s.XValue)
        writer.WriteStr("XValue", XValue);
      if (YValue1 != s.YValue1)
        writer.WriteStr("YValue1", YValue1);
      if (YValue2 != s.YValue2)
        writer.WriteStr("YValue2", YValue2);
      if (YValue3 != s.YValue3)
        writer.WriteStr("YValue3", YValue3);
      if (YValue4 != s.YValue4)
        writer.WriteStr("YValue4", YValue4);
      if (Color != s.Color)
        writer.WriteStr("Color", Color);
      if (Label != s.Label)
        writer.WriteStr("Label", Label);
      if (!AutoSeriesForce)
        writer.WriteBool("AutoSeriesForce", AutoSeriesForce);
      if (AutoSeriesColumn != s.AutoSeriesColumn)
        writer.WriteStr("AutoSeriesColumn", AutoSeriesColumn);
    }

    /// <inheritdoc/>
    public override void Deserialize(FRReader reader)
    {
      base.Deserialize(reader);
      if (reader.HasProperty("GroupByXValue"))
        GroupBy = GroupBy.XValue;
    }

    /// <summary>
    /// Clears all data points in this series.
    /// </summary>
    public void ClearValues()
    {
      SeriesSettings.Points.Clear();
    }
    
    /// <summary>
    /// Adds a data point with specified X and Y values.
    /// </summary>
    /// <param name="xValue">X value.</param>
    /// <param name="yValues">Array of Y values.</param>
    /// <remarks>
    /// Note: number of values in the <b>yValues</b> parameter must be the same as value returned 
    /// by the <see cref="YValuesPerPoint"/> property.
    /// </remarks>
    public void AddValue(object xValue, params object[] yValues)
    {
      SeriesSettings.Points.AddXY(xValue, yValues);
    }
    
    internal void ProcessData()
    {
      object match = true;
      if (!String.IsNullOrEmpty(Filter))
        match = Report.Calc(Filter);
      if (match is bool && (bool)match == true)
        InternalProcessData();
    }
    
    internal void FinishData()
    {
      // sort is required if we group by value, not by axis label
      bool sortThenGroup = GroupBy != GroupBy.XValue;
      if (!sortThenGroup)
      {
        if (GroupBy != GroupBy.None)
          GroupData();
      }
      
      // sort
      if (Collect == Collect.TopN)
      {
        SortBy = SortBy.YValue;
        SortOrder = ChartSortOrder.Descending;
      }  
      else if (Collect == Collect.BottomN)
      {
        SortBy = SortBy.YValue;
        SortOrder = ChartSortOrder.Ascending;
      }
      if (SortBy != SortBy.None)
        SortData();
      
      // group
      if (sortThenGroup)
      {
        if (GroupBy != GroupBy.None)
          GroupData();
      }
      
      // collect topn values
      if (Collect != Collect.None)
        CollectValues();
      
      // explode values
      if (PieExplode != PieExplode.None)
        ExplodePoint();
    }

    internal void CreateDummyData()
    {
      SeriesSettings.Points.Clear();
      SeriesSettings.Points.AddXY("A", 1);
      SeriesSettings.Points.AddXY("B", 3);
      SeriesSettings.Points.AddXY("C", 2);
      SeriesSettings.Points.AddXY("D", 4);
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();

      if (!String.IsNullOrEmpty(PieExplodeValue))
        expressions.Add(PieExplodeValue);

      if (!String.IsNullOrEmpty(XValue))
        expressions.Add(XValue);
      if (!String.IsNullOrEmpty(YValue1))
        expressions.Add(YValue1);
      if (!String.IsNullOrEmpty(YValue2))
        expressions.Add(YValue2);
      if (!String.IsNullOrEmpty(YValue3))
        expressions.Add(YValue3);
      if (!String.IsNullOrEmpty(YValue4))
        expressions.Add(YValue4);
      if (!String.IsNullOrEmpty(Color))
        expressions.Add(Color);
      if (!String.IsNullOrEmpty(Label))
        expressions.Add(Label);
      if (!String.IsNullOrEmpty(Filter))
        expressions.Add(Filter);
      
      return expressions.ToArray();
    }
    #endregion

    /// <summary>
    /// Creates a new instance of the <see cref="MSChartSeries"/> class with default settings.
    /// </summary>
    public MSChartSeries()
    {
      FFilter = "";
      FGroupInterval = 1;
      FCollectedItemText = "";
      FCollectedItemColor = System.Drawing.Color.Transparent;
      FPieExplodeValue = "";

      FXValue = "";
      FYValue1 = "";
      FYValue2 = "";
      FYValue3 = "";
      FYValue4 = "";
      FColor = "";
      FLabel = "";
      FAutoSeriesForce = true;
      BaseName = "Series";
    }
  }
}
