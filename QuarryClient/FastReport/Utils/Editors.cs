using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Forms;

namespace FastReport.Utils
{
  /// <summary>
  /// Contains methods to call common editors.
  /// </summary>
  /// <remarks>
  /// Use this class if you are writing a new component for FastReport.
  /// </remarks>
  public static class Editors
  {
    /// <summary>
    /// Invokes the expression editor.
    /// </summary>
    /// <param name="report">A reference to the report.</param>
    /// <param name="expression">The expression to edit.</param>
    /// <returns>The new expression.</returns>
    public static string EditExpression(Report report, string expression)
    {
      using (ExpressionEditorForm form = new ExpressionEditorForm(report))
      {
        form.ExpressionText = expression;
        if (form.ShowDialog() == DialogResult.OK)
          return form.ExpressionText;
      }
      return expression;
    }
    
    /// <summary>
    /// Invokes the border editor.
    /// </summary>
    /// <param name="border">The <b>Border</b> to edit.</param>
    /// <returns>The new border.</returns>
    public static Border EditBorder(Border border)
    {
      using (BorderEditorForm editor = new BorderEditorForm())
      {
        editor.Border = border.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
          return editor.Border;
      }
      return border;
    }
    
    /// <summary>
    /// Invokes the fill editor.
    /// </summary>
    /// <param name="fill">The fill to edit.</param>
    /// <returns>The new fill.</returns>
    public static FillBase EditFill(FillBase fill)
    {
      using (FillEditorForm editor = new FillEditorForm())
      {
        editor.Fill = fill.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
          return editor.Fill;
      }
      return fill;
    }

    /// <summary>
    /// Invokes the outline editor.
    /// </summary>
    /// <param name="outline">The outline to edit.</param>
    /// <returns>The new outline.</returns>
    public static TextOutline EditOutline(TextOutline outline)
    {
        using (OutlineEditorForm editor = new OutlineEditorForm())
        {
            editor.Outline = outline.Clone();
            if (editor.ShowDialog() == DialogResult.OK)
                return editor.Outline;
        }
        return outline;
    }
  }
}
