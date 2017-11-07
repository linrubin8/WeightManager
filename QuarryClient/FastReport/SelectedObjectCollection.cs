using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Holds the list of selected objects of <see cref="Base"/> type. Used by the 
  /// <see cref="FastReport.Design.Designer.SelectedObjects"/>.
  /// </summary>
  public class SelectedObjectCollection : ObjectCollection
  {
    /// <summary>
    /// Gets a value indicating that report page is selected.
    /// </summary>
    public bool IsPageSelected
    {
      get { return Count == 1 && this[0] is PageBase; }
    }

    /// <summary>
    /// Gets a value indicating that report is selected.
    /// </summary>
    public bool IsReportSelected
    {
      get { return Count == 1 && this[0] is Report; }
    }
  }
}
