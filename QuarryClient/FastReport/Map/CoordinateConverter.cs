using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Map
{
  internal static class CoordinateConverter
  {
    private const double MaxLat = 85.0511287798066;

    public static float GetX(double x, double minX, float scale)
    {
      return (float)((x - minX) * scale);
    }

    public static float GetY(double y, double maxY, float scale, bool mercator)
    {
      if (mercator)
      {
        y = ConvertMercator(y);
        maxY = ConvertMercator(maxY);
      }
      return (float)(-(y - maxY) * scale);
    }

    public static double ConvertMercator(double value)
    {
      if (value > MaxLat)
        value = MaxLat;
      else if (value < -MaxLat)
        value = -MaxLat;

      double sinLat = Math.Sin((Math.PI / 180) * value);
      return (90 / Math.PI) * Math.Log((1 + sinLat) / (1 - sinLat));
    }
  }
}
