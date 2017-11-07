using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for editing an angle in degrees.
  /// </summary>
  public class AngleEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

      AngleControl rc = new AngleControl();
      rc.ShowBorder = false;
      rc.Angle = (int)Value;
      edSvc.DropDownControl(rc);
      return rc.Angle;
    }

  }
}
