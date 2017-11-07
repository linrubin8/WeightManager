using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  internal class StyleEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;

    private void lb_StyleSelected(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }
    
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      if (context != null && context.Instance != null)
      {
        Base c = context.Instance is Base ? context.Instance as Base : ((object[])context.Instance)[0] as Base;
        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        StyleListBox lb = new StyleListBox();
        lb.BorderStyle = BorderStyle.None;
        lb.Styles = c.Report.Styles;
        lb.Style = (string)Value;
        lb.StyleSelected += new EventHandler(lb_StyleSelected);
        lb.Height = (lb.Items.Count < 8 ? lb.Items.Count : 8) * lb.ItemHeight;
        edSvc.DropDownControl(lb);
        return lb.Style;
      }
      return Value;
    }

  }

}
