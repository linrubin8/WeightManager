using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  internal class BorderLinesEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void selector_ToggleLine(object sender, ToggleLineEventArgs e)
    {
      if (e.Toggle)
        e.Border.Lines |= e.Line;
      else
        e.Border.Lines &= ~e.Line;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      Border border = null;
      if (context != null)
      { 
        if (context.Instance is Border)
          border = context.Instance as Border;
        else if (context.Instance is object[])
          border = ((object[])context.Instance)[0] as Border;
      }    

      if (border != null)
      {
        BorderSample selector = new BorderSample();
        selector.Border = border;
        selector.ToggleLine += new ToggleLineEventHandler(selector_ToggleLine);
        edSvc.DropDownControl(selector);
        return border.Lines;
      }
      else
        return Value;
    }

  }
}
