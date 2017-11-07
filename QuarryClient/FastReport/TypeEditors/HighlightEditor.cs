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
  internal class HighlightEditor : UITypeEditor
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
      Report report = context.Instance is Base ? 
        (context.Instance as Base).Report : 
        (((object[])context.Instance)[0] as Base).Report;
      SelectedTextObjects objects = new SelectedTextObjects(report.Designer);
      objects.Update();

      using (HighlightEditorForm editor = new HighlightEditorForm(report))
      {
        editor.Conditions = objects.First.Highlight;
        if (editor.ShowDialog() == DialogResult.OK)
        {
          objects.SetConditions(editor.Conditions);
        }  
      }
      return Value;
    }
  }
}
