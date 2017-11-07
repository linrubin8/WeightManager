using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport
{
  /// <summary>
  /// Implements the object's editor.
  /// </summary>
  public interface IHasEditor
  {
    /// <summary>
    /// Invokes the object's editor.
    /// </summary>
    /// <returns><b>true</b> if object was succesfully edited.</returns>
    /// <remarks>
    /// This method is called by FastReport when the object is doubleclicked in the designer.
    /// </remarks>
    bool InvokeEditor();
  }
}
