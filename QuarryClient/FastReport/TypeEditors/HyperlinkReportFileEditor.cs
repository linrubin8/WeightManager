using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.TypeEditors
{
  internal class HyperlinkReportFileEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      Hyperlink hyperlink = context.Instance is Hyperlink ?
        context.Instance as Hyperlink : ((object[])context.Instance)[0] as Hyperlink;
      
      if (hyperlink != null)
      {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
          dialog.Filter = Res.Get("FileFilters,Report");
          dialog.FileName = hyperlink.DetailReportName;
          
          if (dialog.ShowDialog() == DialogResult.OK)
            return dialog.FileName;
        }
      }
      return Value;
    }
  }
}
