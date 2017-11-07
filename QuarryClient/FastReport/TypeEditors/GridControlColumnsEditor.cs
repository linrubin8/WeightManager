using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Forms;
using FastReport.Format;
using FastReport.Dialog;

namespace FastReport.TypeEditors
{
  internal class GridControlColumnsEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      if (context != null && context.Instance is GridControl)
      {
        using (GridControlColumnsEditorForm editor = new GridControlColumnsEditorForm())
        {
          editor.Grid = context.Instance as GridControl;
          if (editor.ShowDialog() == DialogResult.OK)
          {
            (context.Instance as GridControl).Report.Designer.SetModified(null, "Change");
          }    
        }
      }
      return Value;
    }

  }

}
