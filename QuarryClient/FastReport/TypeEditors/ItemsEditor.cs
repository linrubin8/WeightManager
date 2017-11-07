using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using FastReport.Forms;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for editing a string collection.
  /// </summary>
  public class ItemsEditor : UITypeEditor
  {
    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      if (context != null && context.Instance != null)
      {
        using (StringCollectionEditorForm editor = new StringCollectionEditorForm())
        {
          editor.List = Value as IList;
          if (editor.ShowDialog() == DialogResult.OK)
          {
            if (context.Instance is Base)
              (context.Instance as Base).Report.Designer.SetModified(null, "Change");
          }
        }
      }
      return Value;
    }

  }

}
