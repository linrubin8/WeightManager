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
  internal class HyperlinkReportPageEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;
    private static Size FSize = new Size();

    private void lb_Click(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    public override bool IsDropDownResizable
    {
      get { return true; }
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      Hyperlink hyperlink = context.Instance is Hyperlink ?
        context.Instance as Hyperlink : ((object[])context.Instance)[0] as Hyperlink;

      if (hyperlink != null)
      {
        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        ComponentRefListBox lb = new ComponentRefListBox();
        lb.PopulateList(hyperlink.Report, typeof(ReportPage), hyperlink.Parent.Page);
        if (Value != null)
          lb.SelectedIndex = lb.Items.IndexOf(Value);
        lb.Click += new EventHandler(lb_Click);
        if (FSize.Width > 0)
          lb.Size = FSize;

        edSvc.DropDownControl(lb);
        FSize = lb.Size;
        ReportPage page = lb.SelectedObject as ReportPage;
        if (page != null)
          page.Visible = false;
        return page == null ? "" : page.Name;
      }
      return Value;
    }

  }
}
