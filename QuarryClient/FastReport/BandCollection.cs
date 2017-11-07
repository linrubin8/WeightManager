using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Represents a collection of bands.
  /// </summary>
  public class BandCollection : FRCollectionBase
  {
    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">Index of an element.</param>
    /// <returns>The element at the specified index.</returns>
    public BandBase this[int index]  
    {
      get { return List[index] as BandBase; }
      set { List[index] = value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BandCollection"/> class with default settings.
    /// </summary>
    public BandCollection() : this(null)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BandCollection"/> class with specified owner.
    /// </summary>
    /// <param name="owner">Owner that owns this collection.</param>
    public BandCollection(Base owner) : base(owner)
    {
    }
  }
}