using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Globalization;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for selecting a component inside the same page.
  /// </summary>
  public class PageComponentRefEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;
    private static Size FSize = new Size();

    private void lb_Click(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    /// <inheritdoc/>
    public override bool IsDropDownResizable
    {
      get { return true; }
    }

    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      if (context != null && context.Instance != null)
      {
        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        ComponentRefListBox lb = new ComponentRefListBox();
        lb.PopulateList((context.Instance as ComponentBase).Page, context.PropertyDescriptor.PropertyType, null);
        if (Value != null)
          lb.SelectedIndex = lb.Items.IndexOf(Value);
        lb.Click += new EventHandler(lb_Click);
        if (FSize.Width > 0)
          lb.Size = FSize;

        edSvc.DropDownControl(lb);
        FSize = lb.Size;

        return lb.SelectedObject;
      }
      return Value;
    }

  }
}
