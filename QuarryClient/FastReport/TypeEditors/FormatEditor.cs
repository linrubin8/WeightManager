using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Forms;
using FastReport.Format;

namespace FastReport.TypeEditors
{
  internal class FormatEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      Report report = context.Instance is Base ?
        (context.Instance as Base).Report :
        (((object[])context.Instance)[0] as Base).Report;
      SelectedTextBaseObjects objects = new SelectedTextBaseObjects(report.Designer);
      objects.Update();

      using (FormatEditorForm form = new FormatEditorForm())
      {
        form.TextObject = objects.First as TextObjectBase;
        if (form.ShowDialog() == DialogResult.OK)
        {
          objects.SetFormat(form.Formats);
          report.Designer.SetModified(null, "Change");
        }  
      }
      
      return Value;
    }

  }

}
