using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.MSChart
{
  /// <summary>
  /// Represents a collection of <see cref="MSChartSeries"/> objects.
  /// </summary>
  public class SeriesCollection : FRCollectionBase
  {
    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">Index of an element.</param>
    /// <returns>The element at the specified index.</returns>
    public MSChartSeries this[int index]
    {
      get { return List[index] as MSChartSeries; }
    }
    
    /// <summary>
    /// Resets series data.
    /// </summary>
    public void ResetData()
    {
      foreach (MSChartSeries series in this)
      {
        series.ClearValues();
      }
    }
    
    /// <summary>
    /// Processes the current data row.
    /// </summary>
    public void ProcessData()
    {
      foreach (MSChartSeries series in this)
      {
        series.ProcessData();
      }
    }
    
    /// <summary>
    /// Finishes the series data.
    /// </summary>
    public void FinishData()
    {
      foreach (MSChartSeries series in this)
      {
        series.FinishData();
      }
    }

    internal SeriesCollection(Base owner)
      : base(owner)
    {
    }
  }
}
