using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// This class represents the context menu of the <see cref="BreakableComponent"/>.
  /// </summary>
  /// <remarks>
  /// This class adds the "Can Break" menu item to the component context menu.
  /// </remarks>
  [ToolboxItem(false)]
  public class BreakableComponentMenu : ReportComponentBaseMenu
  {
    private SelectedObjectCollection FSelection;
    /// <summary>
    /// The "Can Break" menu item.
    /// </summary>
    public ButtonItem miCanBreak;

    private void miCanBreak_Click(object sender, EventArgs e)
    {
      foreach (Base c in FSelection)
      {
        if (c is BreakableComponent && !c.HasRestriction(Restrictions.DontModify))
          (c as BreakableComponent).CanBreak = miCanBreak.Checked;
      }
      Change();
    }

    /// <summary>
    /// Initializes a new instance of the <b>BreakableComponentMenu</b> class with default settings. 
    /// </summary>
    public BreakableComponentMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;

      miCanBreak = CreateMenuItem(Res.Get("ComponentMenu,BreakableComponent,CanBreak"), new EventHandler(miCanBreak_Click));
      miCanBreak.AutoCheckOnClick = true;

      int insertPos = Items.IndexOf(miCanShrink);
      Items.Insert(insertPos + 1, miCanBreak);

      BreakableComponent component = FSelection[0] as BreakableComponent;
      miCanBreak.Enabled = !component.HasRestriction(Restrictions.DontModify);
      miCanBreak.Checked = component.CanBreak;
    }
  }
}
