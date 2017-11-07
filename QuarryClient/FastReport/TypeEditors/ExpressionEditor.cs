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
  /// <summary>
  /// Provides a user interface for editing an expression.
  /// </summary>
  public class ExpressionEditor : UITypeEditor
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
      string expression = (string)Value;
      Report report = context != null && context.Instance is Base ? (context.Instance as Base).Report : null;

      if (report != null)
        return Editors.EditExpression(report, expression);
      return Value;
    }
  }
}
