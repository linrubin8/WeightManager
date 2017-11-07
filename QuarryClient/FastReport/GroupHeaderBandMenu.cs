using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class GroupHeaderBandMenu : HeaderFooterBandBaseMenu
  {
    private SelectedObjectCollection FSelection;
    public ButtonItem miKeepTogether;
    public ButtonItem miResetPageNumber;

    private List<GroupHeaderBand> ModifyList
    {
      get 
      { 
        List<GroupHeaderBand> list = new List<GroupHeaderBand>();
        foreach (Base c in FSelection)
        {
          if (c is GroupHeaderBand && !c.HasRestriction(Restrictions.DontModify))
            list.Add(c as GroupHeaderBand);
        }
        return list;
      }
    }

    private void miKeepTogether_Click(object sender, EventArgs e)
    {
      foreach (GroupHeaderBand band in ModifyList)
      {
        band.KeepTogether = miKeepTogether.Checked;
      }
      Change();
    }

    private void miResetPageNumber_Click(object sender, EventArgs e)
    {
      foreach (GroupHeaderBand band in ModifyList)
      {
        band.ResetPageNumber = miResetPageNumber.Checked;
      }
      Change();
    }

    public GroupHeaderBandMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;

      miKeepTogether = CreateMenuItem(Res.Get("ComponentMenu,DataBand,KeepTogether"), new EventHandler(miKeepTogether_Click));
      miKeepTogether.AutoCheckOnClick = true;
      miResetPageNumber = CreateMenuItem(Res.Get("ComponentMenu,GroupHeaderBand,ResetPageNumber"), new EventHandler(miResetPageNumber_Click));
      miResetPageNumber.AutoCheckOnClick = true;

      int insertPos = Items.IndexOf(miStartNewPage);
      Items.Insert(insertPos, miKeepTogether);
      Items.Insert(insertPos + 1, miResetPageNumber);

      GroupHeaderBand band = FSelection[0] as GroupHeaderBand;
      bool enabled = !band.HasRestriction(Restrictions.DontModify);
      miKeepTogether.Enabled = enabled;
      miResetPageNumber.Enabled = enabled;
      miKeepTogether.Checked = band.KeepTogether;
      miResetPageNumber.Checked = band.ResetPageNumber;
    }
  }
}
