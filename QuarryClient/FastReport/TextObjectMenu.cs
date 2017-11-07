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
  /// The class introduces some menu items specific to the <b>TextObject</b>.
  /// </summary>
  public class TextObjectMenu : TextObjectBaseMenu
  {
    #region Fields
    /// <summary>
    /// The "Clear" menu item.
    /// </summary>
    public ButtonItem miClear;

    /// <summary>
    /// The "Auto Width" menu item.
    /// </summary>
    public ButtonItem miAutoWidth;

    /// <summary>
    /// The "Word Wrap" menu item.
    /// </summary>
    public ButtonItem miWordWrap;

    private SelectedTextObjects FTextObjects;
    #endregion

    #region Private Methods
    private void miClear_Click(object sender, EventArgs e)
    {
      FTextObjects.ClearText();
    }

    private void miAutoWidth_Click(object sender, EventArgs e)
    {
      FTextObjects.SetAutoWidth(miAutoWidth.Checked);
    }

    private void miWordWrap_Click(object sender, EventArgs e)
    {
      FTextObjects.SetWordWrap(miWordWrap.Checked);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>TextObjectMenu</b> 
    /// class with default settings. 
    /// </summary>
    /// <param name="designer">The reference to a report designer.</param>
    public TextObjectMenu(Designer designer) : base(designer)
    {
      FTextObjects = new SelectedTextObjects(designer);
      FTextObjects.Update();

      MyRes res = new MyRes("ComponentMenu,TextObject");
      miClear = CreateMenuItem(res.Get("Clear"), new EventHandler(miClear_Click));
      miAutoWidth = CreateMenuItem(res.Get("AutoWidth"), new EventHandler(miAutoWidth_Click));
      miAutoWidth.BeginGroup = true;
      miAutoWidth.AutoCheckOnClick = true;
      miWordWrap = CreateMenuItem(res.Get("WordWrap"), new EventHandler(miWordWrap_Click));
      miWordWrap.AutoCheckOnClick = true;

      miAllowExpressions.BeginGroup = false;

      int insertPos = Items.IndexOf(miFormat) + 1;
      Items.Insert(insertPos, miClear);
      insertPos = Items.IndexOf(miAllowExpressions);
      Items.Insert(insertPos, miAutoWidth);
      Items.Insert(insertPos + 1, miWordWrap);

      bool enabled = FTextObjects.Enabled;
      miAutoWidth.Enabled = enabled;
      miWordWrap.Enabled = enabled;

      TextObject obj = FTextObjects.First;
      miAutoWidth.Checked = obj.AutoWidth;
      miWordWrap.Checked = obj.WordWrap;
    }
  }
}
