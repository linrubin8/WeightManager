using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.TypeConverters;

namespace FastReport
{
  /// <summary>
  /// This class contains the page columns settings. 
  /// It is used in the <see cref="ReportPage.Columns"/> property.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class PageColumns
  {
    private int FCount;
    private float FWidth;
    private FloatCollection FPositions;
    private ReportPage FPage;

    /// <summary>
    /// Gets or sets the number of columns.
    /// </summary>
    /// <remarks>
    /// Set this property to 0 or 1 if you don't want to use columns.
    /// </remarks>
    [DefaultValue(1)]
    public int Count
    {
      get { return FCount; }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException("Count", "Value must be greather than 0");

        FCount = value;
        FWidth = (FPage.PaperWidth - FPage.LeftMargin - FPage.RightMargin) / FCount;
        FPositions.Clear();
        for (int i = 0; i < FCount; i++)
        {
          FPositions.Add(i * Width);
        }
      }
    }

    /// <summary>
    /// Gets or sets the column width.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    public float Width
    {
      get { return FWidth; }
      set { FWidth = value; }
    }

    /// <summary>
    /// Gets or sets a list of column starting positions.
    /// </summary>
    /// <remarks>
    /// Each value represents a column starting position measured in the millimeters.
    /// </remarks>
    public FloatCollection Positions
    {
      get { return FPositions; }
      set { FPositions = value; }
    }

    private bool ShouldSerializeWidth()
    {
      return Count > 1;
    }

    private bool ShouldSerializePositions()
    {
      return Count > 1;
    }

    /// <summary>
    /// Assigns values from another source.
    /// </summary>
    /// <param name="source">Source to assign from.</param>
    public void Assign(PageColumns source)
    {
      Count = source.Count;
      Width = source.Width;
      Positions.Assign(source.Positions);
    }
    
    internal void Serialize(FRWriter writer, PageColumns c)
    {
      if (Count != c.Count)
        writer.WriteInt("Columns.Count", Count);
      if (Count > 1)
      {
        writer.WriteFloat("Columns.Width", Width);
        writer.WriteValue("Columns.Positions", Positions);
      }  
    }
    
    internal PageColumns(ReportPage page)
    {
      FPage = page;
      FPositions = new FloatCollection();
      Count = 1;
    }
  }
}
