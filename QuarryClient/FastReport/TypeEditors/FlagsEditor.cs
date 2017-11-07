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
  /// Provides a user interface for editing a flags enumeration.
  /// </summary>
  public class FlagsEditor : UITypeEditor
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
      
      FlagsControl control = new FlagsControl();
      control.Flags = Value as Enum;
      edSvc.DropDownControl(control);
      return control.Flags;
    }

  }
}
