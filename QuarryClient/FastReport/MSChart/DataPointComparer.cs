using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace FastReport.MSChart
{
  internal class DataPointComparer : IComparer<DataPoint>
  {
    private SortBy FSortBy;
    private ChartSortOrder FSortOrder;

    public int Compare(DataPoint x, DataPoint y)
    {
      int result = 0;
      IComparable i1;
      IComparable i2;

      if (FSortBy == SortBy.XValue)
      {
        if (!String.IsNullOrEmpty(x.AxisLabel))
        {
          i1 = x.AxisLabel as IComparable;
          i2 = y.AxisLabel as IComparable;
        }
        else
        {
          i1 = x.XValue as IComparable;
          i2 = y.XValue as IComparable;
        }
      }
      else
      {
        i1 = x.YValues[0] as IComparable;
        i2 = y.YValues[0] as IComparable;
      }

      if (i1 != null)
        result = i1.CompareTo(i2);
      else if (i2 != null)
        result = -1;
      if (FSortOrder == ChartSortOrder.Descending)
        result = -result;

      return result;
    }

    public DataPointComparer(SortBy sortBy, ChartSortOrder sortOrder)
    {
      FSortBy = sortBy;
      FSortOrder = sortOrder;
    }
  }
}
