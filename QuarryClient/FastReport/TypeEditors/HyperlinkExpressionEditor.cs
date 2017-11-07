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
  internal class HyperlinkExpressionEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      string expression = (string)Value;
      Report report = context != null && context.Instance is Hyperlink ? (context.Instance as Hyperlink).Report : null;

      if (report != null)
        return Editors.EditExpression(report, expression);
      return Value;
    }
  }
}
