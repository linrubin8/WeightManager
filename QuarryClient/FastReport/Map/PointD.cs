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
  /// Represents a pair of double coordinates that defines a constituent point.
  /// </summary>
  public struct PointD
  {
    #region Fields
    private double x;
    private double y;
    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets or sets the X-coordinate of a point.
    /// </summary>
    public double X
    {
      get { return x; }
      set { x = value; }
    }

    /// <summary>
    /// Gets or sets the Y-coordinate of a point.
    /// </summary>
    public double Y
    {
      get { return y; }
      set { y = value; }
    }

    #endregion // Properties

    #region Public Methods
    /// <summary>
    /// Creates a new instance of the <see cref="PointD"/> class with specified coordinates.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    public PointD(double x, double y)
    {
      this.x = x;
      this.y = y;
    }
    
    internal void Load(Stream stream)
    {
      byte[] buffer8 = new byte[8];
      stream.Read(buffer8, 0, buffer8.Length);
      X = BitConverter.ToDouble(buffer8, 0);
      stream.Read(buffer8, 0, buffer8.Length);
      Y = BitConverter.ToDouble(buffer8, 0);
    }

    internal void Save(Stream stream)
    {
      byte[] buffer8 = BitConverter.GetBytes(X);
      stream.Write(buffer8, 0, buffer8.Length);
      buffer8 = BitConverter.GetBytes(Y);
      stream.Write(buffer8, 0, buffer8.Length);
    }

    #endregion // Public Methods
  }
}
