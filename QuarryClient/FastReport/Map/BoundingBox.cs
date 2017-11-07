using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;
using System.IO;
using System.Globalization;
using System.ComponentModel;

namespace FastReport.Map
{
  /// <summary>
  /// Represents four coordinates that define a bounding box.
  /// </summary>
  [TypeConverter(typeof(ExpandableObjectConverter))]
  public class BoundingBox
  {
    #region Fields
    private double minX;
    private double minY;
    private double maxX;
    private double maxY;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the minimum X-coordinate of a bounding box.
    /// </summary>
    public double MinX
    {
      get { return minX; }
      set { minX = value; }
    }

    /// <summary>
    /// Gets or sets the minimum Y-coordinate of a bounding box.
    /// </summary>
    public double MinY
    {
      get { return minY; }
      set { minY = value; }
    }

    /// <summary>
    /// Gets or sets the maximum X-coordinate of a bounding box.
    /// </summary>
    public double MaxX
    {
      get { return maxX; }
      set { maxX = value; }
    }

    /// <summary>
    /// Gets or sets the maximum Y-coordinate of a bounding box.
    /// </summary>
    public double MaxY
    {
      get { return maxY; }
      set { maxY = value; }
    }
    #endregion // Properties

    #region Private Methods
    private string ToString(double value)
    {
      return value.ToString(CultureInfo.InvariantCulture.NumberFormat);
    }

    private double FromString(string value)
    {
      return double.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Copies the contents of another <see cref="BoundingBox"/> instance.
    /// </summary>
    /// <param name="src">Source box to copy the contents from.</param>
    public void Assign(BoundingBox src)
    {
      MinX = src.MinX;
      MinY = src.MinY;
      MaxX = src.MaxX;
      MaxY = src.MaxY;
    }
    
    internal string GetAsString()
    {
      StringBuilder result = new StringBuilder();
      result.Append(ToString(minX)).Append(",").
        Append(ToString(minY)).Append(",").
        Append(ToString(maxX)).Append(",").
        Append(ToString(maxY));
      return result.ToString();
    }

    internal void SetAsString(string value)
    {
      string[] arr = value.Split(new char[] { ',' });
      minX = FromString(arr[0]);
      minY = FromString(arr[1]);
      maxX = FromString(arr[2]);
      maxY = FromString(arr[3]);
    }

    internal void Load(Stream stream)
    {
      byte[] buffer8 = new byte[8];
      stream.Read(buffer8, 0, buffer8.Length);
      MinX = BitConverter.ToDouble(buffer8, 0);
      stream.Read(buffer8, 0, buffer8.Length);
      MinY = BitConverter.ToDouble(buffer8, 0);
      stream.Read(buffer8, 0, buffer8.Length);
      MaxX = BitConverter.ToDouble(buffer8, 0);
      stream.Read(buffer8, 0, buffer8.Length);
      MaxY = BitConverter.ToDouble(buffer8, 0);
    }

    internal void Save(Stream stream)
    {
      byte[] buffer8 = BitConverter.GetBytes(MinX);
      stream.Write(buffer8, 0, buffer8.Length);
      buffer8 = BitConverter.GetBytes(MinY);
      stream.Write(buffer8, 0, buffer8.Length);
      buffer8 = BitConverter.GetBytes(MaxX);
      stream.Write(buffer8, 0, buffer8.Length);
      buffer8 = BitConverter.GetBytes(MaxY);
      stream.Write(buffer8, 0, buffer8.Length);
    }
    #endregion // Public Methods
  }
}
