using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows control that displays a frame around a group of controls with an optional caption.
  /// Wraps the <see cref="System.Windows.Forms.GroupBox"/> control.
  /// </summary>
  public class GroupBoxControl : ParentControl
  {
    private GroupBox FGroupBox;

    /// <summary>
    /// Gets an internal <b>GroupBox</b>.
    /// </summary>
    [Browsable(false)]
    public GroupBox GroupBox
    {
      get { return FGroupBox; }
    }

    /// <summary>
    /// Initializes a new instance of the <b>GroupBoxControl</b> class with default settings. 
    /// </summary>
    public GroupBoxControl()
    {
      FGroupBox = new GroupBox();
      Control = FGroupBox;
    }
  }
}
