using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Forms;
using FastReport.Utils;
using System.Drawing;

namespace FastReport.TypeEditors
{
  internal class ImageEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Images");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          return Image.FromFile(dialog.FileName);
        }
      }


      return Value;
    }
  }
}
