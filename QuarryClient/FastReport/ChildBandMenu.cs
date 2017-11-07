using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using System.Drawing;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  internal class ChildBandMenu : BandBaseMenu
  {
    public ButtonItem miFillUnusedSpace;

    private void miFillUnusedSpace_Click(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is ChildBand && !c.HasRestriction(Restrictions.DontModify))
          (c as ChildBand).FillUnusedSpace = miFillUnusedSpace.Checked;
      }
      Change();
    }

    private void ud_ValueChanged(object sender, EventArgs e)
    {
      foreach (Base c in Designer.SelectedObjects)
      {
        if (c is ChildBand && !c.HasRestriction(Restrictions.DontModify))
          (c as ChildBand).CompleteToNRows = (int)(sender as NumericUpDown).Value;
      }
      Change();
    }

    public ChildBandMenu(Designer designer)
      : base(designer)
    {
      miFillUnusedSpace = CreateMenuItem(Res.Get("ComponentMenu,ChildBand,FillUnusedSpace"), new EventHandler(miFillUnusedSpace_Click));
      miFillUnusedSpace.BeginGroup = true;
      miFillUnusedSpace.AutoCheckOnClick = true;
      
      miStartNewPage.BeginGroup = false;
      
      int insertPos = Items.IndexOf(miStartNewPage);
      Items.Insert(insertPos, miFillUnusedSpace);
      
      ChildBand childBand = Designer.SelectedObjects[0] as ChildBand;

      if (childBand.Parent is DataBand)
      {
        Panel panel = new Panel();
        panel.BackColor = Color.Transparent;
        panel.Padding = new System.Windows.Forms.Padding(6,0,0,0);

        Label label1 = new Label();
        label1.Text = Res.Get("ComponentMenu,ChildBand,CompleteToNRows");
        label1.AutoSize = true;
        label1.Parent = panel;
        int width = label1.Width + 8;
        label1.AutoSize = false;
        label1.Width = width;
        label1.Dock = DockStyle.Left;
        label1.TextAlign = ContentAlignment.MiddleLeft;

        NumericUpDown ud = new NumericUpDown();
        ud.Parent = panel;
        ud.Left = width + 6;
        ud.Top = 2;
        ud.Width = 50;
        ud.Value = childBand.CompleteToNRows;
        ud.ValueChanged += new EventHandler(ud_ValueChanged);
        
        panel.Width = ud.Right + 8;
        panel.Height = ud.Height + 4;

        ControlContainerItem host = new ControlContainerItem();
        host.Control = panel;
        Items.Insert(insertPos + 1, host);
      }

      ChildBand band = Designer.SelectedObjects[0] as ChildBand;
      miFillUnusedSpace.Enabled = !band.HasRestriction(Restrictions.DontModify);
      miFillUnusedSpace.Checked = band.FillUnusedSpace;
    }
  }
}
