using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class SubreportObjectMenu : ReportComponentBaseMenu
  {
    public ButtonItem miPrintOnParent;

    private void miPrintOnParent_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is SubreportObject && !c.HasRestriction(Restrictions.DontModify))
          (c as SubreportObject).PrintOnParent = miPrintOnParent.Checked;
      }
      Change();
    }

    public SubreportObjectMenu(Designer designer) : base(designer)
    {
      miPrintOnParent = CreateMenuItem(Res.Get("ComponentMenu,SubreportObject,PrintOnParent"), new EventHandler(miPrintOnParent_Click));
      miPrintOnParent.BeginGroup = true;
      miPrintOnParent.AutoCheckOnClick = true;

      miHyperlink.Visible = false;
      miCanGrow.Visible = false;
      miCanShrink.Visible = false;
      miGrowToBottom.Visible = false;
      int insertPos = Items.IndexOf(miGrowToBottom);
      Items.Insert(insertPos, miPrintOnParent);

      SubreportObject subreport = Designer.SelectedObjects[0] as SubreportObject;
      miPrintOnParent.Enabled = !subreport.HasRestriction(Restrictions.DontModify);
      miPrintOnParent.Checked = subreport.PrintOnParent;
    }
  }
}
