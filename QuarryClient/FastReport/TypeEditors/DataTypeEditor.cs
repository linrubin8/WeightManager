using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using FastReport.Controls;
using FastReport.Forms;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for choosing a data type.
  /// </summary>
  public class DataTypeEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void lb_SelectedIndexChanged(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      Type dataType = Value as Type;
      ListBox lb = new TypeListBox();
      lb.BorderStyle = BorderStyle.None;
      lb.SelectedItem = dataType;
      lb.SelectedIndexChanged += new EventHandler(lb_SelectedIndexChanged);
      edSvc.DropDownControl(lb);
      return lb.SelectedItem;
    }
  }
}
