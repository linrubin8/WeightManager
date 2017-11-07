using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace FastReport.MSChart
{
  internal class SeriesInfo
  {
    public MSChartSeries Series;
    public Series ChartSeries;
    
    public SeriesInfo(MSChartSeries series)
    {
      Series = series;
      ChartSeries = series.SeriesSettings;
    }
  }
  
  internal class SeriesComparer : IComparer<SeriesInfo>
  {
    public SortOrder FSortOrder;
    
    public int Compare(SeriesInfo x, SeriesInfo y)
    {
      int result = 0;
      IComparable i1 = x.ChartSeries.Name;
      IComparable i2 = y.ChartSeries.Name;

      if (i1 != null)
        result = i1.CompareTo(i2);
      else if (i2 != null)
        result = -1;
      if (FSortOrder == SortOrder.Descending)
        result = -result;

      return result;
    }
    
    public SeriesComparer(SortOrder sortOrder)
    {
      FSortOrder = sortOrder;
    }
  }
}
