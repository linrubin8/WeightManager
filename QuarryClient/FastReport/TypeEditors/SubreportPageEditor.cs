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
  internal class SubreportPageEditor : UITypeEditor
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
      // this method is called when we click on drop-down arrow
      if (context != null && context.Instance != null)
      {
        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        SubreportObject subreport = context.Instance as SubreportObject;
        ComponentRefListBox lb = new ComponentRefListBox();
        lb.PopulateList(subreport.Report, context.PropertyDescriptor.PropertyType, null);
        // remove wrong pages from lb
        lb.Items.RemoveAt(0);
        SubreportObject sr = subreport;
        while (sr != null)
        {
          if (lb.Items.Contains(sr.Page))
            lb.Items.Remove(sr.Page);
          sr = (sr.Page as ReportPage).Subreport;
        }

        if (Value != null)
          lb.SelectedIndex = lb.Items.IndexOf(Value);
        lb.Click += new EventHandler(lb_Click);
        if (FSize.Width > 0)
          lb.Size = FSize;

        edSvc.DropDownControl(lb);
        FSize = lb.Size;

        ReportPage page = lb.SelectedObject as ReportPage;
        if (page == null)
          page = subreport.ReportPage;
        return page;
      }
      return Value;
    }

  }
}
