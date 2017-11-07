using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class DataBandMenu : BandBaseMenu
  {
    private SelectedObjectCollection FSelection;
    public ButtonItem miAddDetailDataBand;
    public ButtonItem miKeepTogether;
    public ButtonItem miKeepDetail;

    private List<DataBand> ModifyList
    {
      get 
      { 
        List<DataBand> list = new List<DataBand>();
        foreach (Base c in FSelection)
        {
          if (c is DataBand && !c.HasRestriction(Restrictions.DontModify))
            list.Add(c as DataBand);
        }
        return list;
      }
    }

    private void miAddDetailDataBand_Click(object sender, EventArgs e)
    {
      DataBand band = FSelection[0] as DataBand;
      DataBand detailData = new DataBand();
      detailData.Parent = band;
      detailData.CreateUniqueName();
      detailData.Height = detailData.GetPreferredSize().Height;

      Change();
    }

    private void miKeepTogether_Click(object sender, EventArgs e)
    {
      foreach (DataBand band in ModifyList)
      {
        band.KeepTogether = miKeepTogether.Checked;
      }
      Change();
    }

    private void miKeepDetail_Click(object sender, EventArgs e)
    {
      foreach (DataBand band in ModifyList)
      {
        band.KeepDetail = miKeepDetail.Checked;
      }
      Change();
    }

    public DataBandMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;

      miAddDetailDataBand = CreateMenuItem(Res.Get("ComponentMenu,DataBand,AddDetailDataBand"), miAddDetailDataBand_Click);
      miKeepTogether = CreateMenuItem(Res.Get("ComponentMenu,DataBand,KeepTogether"), miKeepTogether_Click);
      miKeepTogether.BeginGroup = true;
      miKeepTogether.AutoCheckOnClick = true;
      miKeepDetail = CreateMenuItem(Res.Get("ComponentMenu,DataBand,KeepDetail"), miKeepDetail_Click);
      miKeepDetail.AutoCheckOnClick = true;

      miStartNewPage.BeginGroup = false;

      int insertPos = Items.IndexOf(miAddChildBand);
      Items.Insert(insertPos + 1, miAddDetailDataBand);
      insertPos = Items.IndexOf(miStartNewPage);
      Items.Insert(insertPos, miKeepTogether);
      Items.Insert(insertPos + 1, miKeepDetail);

      DataBand band = FSelection[0] as DataBand;
      bool enabled = !band.HasRestriction(Restrictions.DontModify);
      miAddDetailDataBand.Enabled = enabled && band.Bands.Count == 0;
      miKeepTogether.Enabled = enabled;
      miKeepDetail.Enabled = enabled;
      miKeepTogether.Checked = band.KeepTogether;
      miKeepDetail.Checked = band.KeepDetail;
    }
  }
}
