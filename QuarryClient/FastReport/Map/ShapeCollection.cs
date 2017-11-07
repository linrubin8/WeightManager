using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Map
{
  /// <summary>
  /// Holds the list of objects of <see cref="ShapeBase"/> type.
  /// </summary>
  public class ShapeCollection : FRCollectionBase
  {
    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">Index of an element.</param>
    /// <returns>The element at the specified index.</returns>
    public ShapeBase this[int index]  
    {
      get { return List[index] as ShapeBase; }
      set { List[index] = value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeCollection"/> class with default settings.
    /// </summary>
    public ShapeCollection() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeCollection"/> class with specified owner.
    /// </summary>
    public ShapeCollection(Base owner)
      : base(owner)
    {
    }
  }
}