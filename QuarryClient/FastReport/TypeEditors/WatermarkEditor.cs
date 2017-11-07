using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Forms;

namespace FastReport.TypeEditors
{
  internal class WatermarkEditor : UITypeEditor
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
      Watermark watermark = Value as Watermark;

      using (WatermarkEditorForm editor = new WatermarkEditorForm())
      {
        editor.Watermark = watermark;
        editor.HideApplyToAll();
        if (editor.ShowDialog() == DialogResult.OK)
          watermark = editor.Watermark;
      }
      return watermark;
    }
  }
}
