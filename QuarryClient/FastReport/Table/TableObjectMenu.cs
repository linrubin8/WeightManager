using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  internal class TableObjectMenu : BreakableComponentMenu
  {
    public ButtonItem miRepeatHeaders;

    private void miRepeatHeaders_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is TableBase && !c.HasRestriction(Restrictions.DontModify))
          (c as TableBase).RepeatHeaders = miRepeatHeaders.Checked;
      }
      Change();
    }

    public TableObjectMenu(Designer designer) : base(designer)
    {
      miRepeatHeaders = CreateMenuItem(Res.Get("ComponentMenu,TableObject,RepeatHeaders"), new EventHandler(miRepeatHeaders_Click));
      miRepeatHeaders.BeginGroup = true;
      miRepeatHeaders.AutoCheckOnClick = true;

      int insertPos = Items.IndexOf(miCanBreak);
      Items.Insert(insertPos, miRepeatHeaders);

      TableBase table = Designer.SelectedObjects[0] as TableBase;
      bool enabled = !table.HasRestriction(Restrictions.DontModify);
      miRepeatHeaders.Enabled = enabled;
      miRepeatHeaders.Checked = table.RepeatHeaders;
      
      miHyperlink.Visible = false;
      miCanGrow.Visible = false;
      miCanShrink.Visible = false;
    }
  }
}
