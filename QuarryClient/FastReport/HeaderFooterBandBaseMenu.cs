using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class HeaderFooterBandBaseMenu : BandBaseMenu
  {
    public ButtonItem miKeepWithData;
    public ButtonItem miRepeatOnEveryPage;

    private void miKeepWithData_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is HeaderFooterBandBase && !c.HasRestriction(Restrictions.DontModify))
          (c as HeaderFooterBandBase).KeepWithData = miKeepWithData.Checked;
      }
      Change();
    }

    private void miRepeatOnEveryPage_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is HeaderFooterBandBase && !c.HasRestriction(Restrictions.DontModify))
          (c as HeaderFooterBandBase).RepeatOnEveryPage = miRepeatOnEveryPage.Checked;
      }
      Change();
    }

    public HeaderFooterBandBaseMenu(Designer designer)
      : base(designer)
    {
      miKeepWithData = CreateMenuItem(Res.Get("ComponentMenu,HeaderBand,KeepWithData"), new EventHandler(miKeepWithData_Click));
      miKeepWithData.BeginGroup = true;
      miKeepWithData.AutoCheckOnClick = true;
      miRepeatOnEveryPage = CreateMenuItem(Res.Get("ComponentMenu,HeaderBand,RepeatOnEveryPage"), new EventHandler(miRepeatOnEveryPage_Click));
      miRepeatOnEveryPage.AutoCheckOnClick = true;

      miStartNewPage.BeginGroup = false;
      
      int insertPos = Items.IndexOf(miStartNewPage);
      Items.Insert(insertPos, miKeepWithData);
      Items.Insert(insertPos + 1, miRepeatOnEveryPage);

      HeaderFooterBandBase band = Designer.SelectedObjects[0] as HeaderFooterBandBase;
      bool enabled = !band.HasRestriction(Restrictions.DontModify);
      miKeepWithData.Enabled = enabled;
      miRepeatOnEveryPage.Enabled = enabled;
      miKeepWithData.Checked = band.KeepWithData;
      miRepeatOnEveryPage.Checked = band.RepeatOnEveryPage;
    }
  }
}
