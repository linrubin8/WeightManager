using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// The class introduces some menu items specific to the <b>TextObjectBase</b>.
  /// </summary>
  public class TextObjectBaseMenu : BreakableComponentMenu
  {
    #region Fields
    /// <summary>
    /// The "Format" menu item.
    /// </summary>
    public ButtonItem miFormat;

    /// <summary>
    /// The "Allow Expressions" menu item.
    /// </summary>
    public ButtonItem miAllowExpressions;

    private SelectedTextBaseObjects FTextObjects;
    #endregion

    #region Private Methods
    private void miFormat_Click(object sender, EventArgs e)
    {
      using (FormatEditorForm form = new FormatEditorForm())
      {
        form.TextObject = FTextObjects.First;
        if (form.ShowDialog() == DialogResult.OK)
        {
          FTextObjects.SetFormat(form.Formats);
          Change();
        }
      }
    }

    private void miAllowExpressions_Click(object sender, EventArgs e)
    {
      FTextObjects.SetAllowExpressions(miAllowExpressions.Checked);
      Change();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>TextObjectBaseMenu</b> 
    /// class with default settings. 
    /// </summary>
    /// <param name="designer">The reference to a report designer.</param>
    public TextObjectBaseMenu(Designer designer) : base(designer)
    {
      FTextObjects = new SelectedTextBaseObjects(designer);
      FTextObjects.Update();

      MyRes res = new MyRes("ComponentMenu,TextObject");
      miFormat = CreateMenuItem(Res.GetImage(168), res.Get("Format"), new EventHandler(miFormat_Click));
      miAllowExpressions = CreateMenuItem(res.Get("AllowExpressions"), new EventHandler(miAllowExpressions_Click));
      miAllowExpressions.BeginGroup = true;
      miAllowExpressions.AutoCheckOnClick = true;

      int insertPos = Items.IndexOf(miEdit) + 1;
      Items.Insert(insertPos, miFormat);
      insertPos = Items.IndexOf(miCut);
      Items.Insert(insertPos, miAllowExpressions);

      TextObjectBase obj = FTextObjects.First;
      bool enabled = !obj.HasRestriction(Restrictions.DontModify);
      miAllowExpressions.Enabled = enabled;
      miAllowExpressions.Checked = obj.AllowExpressions && enabled;
    }
  }
}
