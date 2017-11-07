using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Matrix;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  internal class MatrixStyleEditor : UITypeEditor
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
        MatrixObject c = context.Instance as MatrixObject;
        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

        MatrixStyleListBox lb = new MatrixStyleListBox();
        lb.BorderStyle = BorderStyle.None;
        lb.Styles = c.StyleSheet;
        lb.Style = (string)Value;
        lb.StyleSelected += new EventHandler(lb_StyleSelected);
        lb.Height = (c.StyleSheet.Count + 1) * lb.ItemHeight;
        edSvc.DropDownControl(lb);
        return lb.Style;
      }
      return Value;
    }

  }

}
