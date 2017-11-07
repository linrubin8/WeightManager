using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class BandBaseMenu : BreakableComponentMenu
  {
    private SelectedObjectCollection FSelection;
    public ButtonItem miAddChildBand;
    public ButtonItem miStartNewPage;
    public ButtonItem miPrintOnBottom;

    private void miAddChildBand_Click(object sender, EventArgs e)
    {
      BandBase band = FSelection[0] as BandBase;
      ChildBand child = new ChildBand();
      band.Child = child;
      child.CreateUniqueName();
      child.Height = child.GetPreferredSize().Height;

      Change();
    }

    private void miStartNewPage_Click(object sender, EventArgs e)
    {
      foreach (Base c in FSelection)
      {
        if (c is BandBase && !c.HasRestriction(Restrictions.DontModify))
          (c as BandBase).StartNewPage = miStartNewPage.Checked;
      }
      Change();
    }

    private void miPrintOnBottom_Click(object sender, EventArgs e)
    {
      foreach (Base c in FSelection)
      {
        if (c is BandBase && !c.HasRestriction(Restrictions.DontModify))
          (c as BandBase).PrintOnBottom = miPrintOnBottom.Checked;
      }
      Change();
    }

    public BandBaseMenu(Designer designer)
      : base(designer)
    {
      FSelection = Designer.SelectedObjects;

      miAddChildBand = CreateMenuItem(Res.Get("ComponentMenu,Band,AddChildBand"), miAddChildBand_Click);
      miStartNewPage = CreateMenuItem(Res.Get("ComponentMenu,Band,StartNewPage"), miStartNewPage_Click);
      miStartNewPage.BeginGroup = true;
      miStartNewPage.AutoCheckOnClick = true;
      miPrintOnBottom = CreateMenuItem(Res.Get("ComponentMenu,Band,PrintOnBottom"), miPrintOnBottom_Click);
      miPrintOnBottom.AutoCheckOnClick = true;

      int insertPos = Items.IndexOf(miHyperlink);
      Items.Insert(insertPos, miAddChildBand);
      insertPos = Items.IndexOf(miGrowToBottom);
      Items.Insert(insertPos, miStartNewPage);
      Items.Insert(insertPos + 1, miPrintOnBottom);

      BandBase band = FSelection[0] as BandBase;
      miAddChildBand.Enabled = !band.HasRestriction(Restrictions.DontModify) && band.Child == null;
      bool enabled = !band.HasRestriction(Restrictions.DontModify) && band.FlagUseStartNewPage;
      miStartNewPage.Enabled = enabled;
      miPrintOnBottom.Enabled = enabled;
      miStartNewPage.Checked = band.StartNewPage;
      miPrintOnBottom.Checked = band.PrintOnBottom;
      
      miGrowToBottom.Visible = false;
      miCut.Visible = false;
      miCopy.Visible = false;
      miPaste.BeginGroup = true;
      miBringToFront.Visible = false;
      miSendToBack.Visible = false;
    }
  }
}
