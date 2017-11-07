using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents the collection of dialog components.
  /// </summary>
  public class DialogComponentCollection : FRCollectionBase
  {
    /// <summary>
    /// Gets or sets a component.
    /// </summary>
    /// <param name="index">The index of a component in this collection.</param>
    /// <returns>The component with specified index.</returns>
    public DialogComponentBase this[int index]
    {
      get { return List[index] as DialogComponentBase; }
      set { List[index] = value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogComponentCollection"/> class with default settings.
    /// </summary>
    public DialogComponentCollection() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogComponentCollection"/> class with default settings.
    /// </summary>
    /// <param name="owner">The owner of this collection.</param>
    public DialogComponentCollection(Base owner) : base(owner)
    {
    }
  }
}
