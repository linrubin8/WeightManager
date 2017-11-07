using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;
using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;

namespace FastReport
{
  internal class ZipCodeObjectMenu : ReportComponentBaseMenu
  {
    public ButtonItem miShowMarkers;
    public ButtonItem miShowGrid;

    private void miShowMarkers_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is ZipCodeObject && !c.HasRestriction(Restrictions.DontModify))
          (c as ZipCodeObject).ShowMarkers = miShowMarkers.Checked;
      }
      Change();
    }

    private void miShowGrid_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is ZipCodeObject && !c.HasRestriction(Restrictions.DontModify))
          (c as ZipCodeObject).ShowGrid = miShowGrid.Checked;
      }
      Change();
    }

    public ZipCodeObjectMenu(Designer designer)
      : base(designer)
    {
      miShowMarkers = CreateMenuItem(Res.Get("ComponentMenu,ZipCodeObject,ShowMarkers"), new EventHandler(miShowMarkers_Click));
      miShowMarkers.BeginGroup = true;
      miShowMarkers.AutoCheckOnClick = true;

      miShowGrid = CreateMenuItem(Res.Get("ComponentMenu,ZipCodeObject,ShowGrid"), new EventHandler(miShowGrid_Click));
      miShowGrid.AutoCheckOnClick = true;

      miCanGrow.Visible = false;
      miCanShrink.Visible = false;
      miGrowToBottom.Visible = false;
      int insertPos = Items.IndexOf(miGrowToBottom);
      Items.Insert(insertPos, miShowMarkers);
      Items.Insert(insertPos + 1, miShowGrid);

      ZipCodeObject zip = Designer.SelectedObjects[0] as ZipCodeObject;
      miShowMarkers.Enabled = !zip.HasRestriction(Restrictions.DontModify);
      miShowGrid.Enabled = miShowMarkers.Enabled;
      miShowMarkers.Checked = zip.ShowMarkers;
      miShowGrid.Checked = zip.ShowGrid;
    }
  }
}
