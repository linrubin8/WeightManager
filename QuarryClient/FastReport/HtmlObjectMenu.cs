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
  public class HtmlObjectMenu : TextObjectBaseMenu
  {
    #region Fields
    private SelectedTextBaseObjects FHtmlObjects;
    #endregion


    /// <summary>
    /// Initializes a new instance of the <b>TextObjectMenu</b> 
    /// class with default settings. 
    /// </summary>
    /// <param name="designer">The reference to a report designer.</param>
    public HtmlObjectMenu(Designer designer) : base(designer)
    {
      FHtmlObjects = new SelectedTextBaseObjects(designer);
      FHtmlObjects.Update();

      MyRes res = new MyRes("ComponentMenu,HtmlObject");

      miAllowExpressions.BeginGroup = false;

      int insertPos = Items.IndexOf(miFormat) + 1;
      insertPos = Items.IndexOf(miAllowExpressions);

      bool enabled = FHtmlObjects.Enabled;

      HtmlObject obj = FHtmlObjects.First as HtmlObject;
    }
  }
}
