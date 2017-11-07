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
using FastReport.Controls;
using FastReport.Data;

namespace FastReport.TypeEditors
{
  internal class CommandParametersEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

      TableDataSource data = context.Instance as TableDataSource;
      if (data != null)
      {
        using (QueryWizardForm form = new QueryWizardForm(data))
        {
          form.VisiblePanelIndex = 2;
          form.btnFinish.Enabled = true;
          if (form.ShowDialog() == DialogResult.OK)
            data.Report.Designer.SetModified(null, "Change");
        }
      }
      
      return Value;
    }
  }
}
