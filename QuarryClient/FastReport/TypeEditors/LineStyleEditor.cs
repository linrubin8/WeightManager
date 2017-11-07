using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  internal class LineStyleEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void ls_StyleSelected(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this editor is used for Border and Border.Lines properties
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      LineStyleControl ls = new LineStyleControl();
      ls.Style = (LineStyle)Value;
      ls.LineWidth = 2;
      ls.StyleSelected += new EventHandler(ls_StyleSelected);
      
      edSvc.DropDownControl(ls);
      return ls.Style;
    }

  }
}
