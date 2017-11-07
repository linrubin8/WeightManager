using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Design.PageDesigners.Page;

namespace FastReport.Forms
{
  internal partial class GroupExpertForm : BaseDialogForm
  {
    private Designer FDesigner;
    private Report FReport;
    private ReportPage FPage;
    
    private void FillGroups(GroupHeaderBand group)
    {
      if (group == null)
        return;
      lbGroups.Items.Add(group);
      FillGroups(group.NestedGroup);
    }

    private void UpdateButtonsState()
    {
      int itemIndex = lbGroups.SelectedIndex;
      btnEdit.Enabled = itemIndex != -1;
      btnDelete.Enabled = itemIndex != -1;
      btnUp.Enabled = itemIndex > 0;
      btnDown.Enabled = itemIndex  >= 0 && itemIndex < lbGroups.Items.Count - 1;
    }
    
    private void lbGroups_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.Graphics.FillRectangle((e.State & DrawItemState.Selected) != 0 ? SystemBrushes.Highlight : SystemBrushes.Window,
        new Rectangle(e.Bounds.X + 22, e.Bounds.Y, e.Bounds.Width - 22, e.Bounds.Height));

      if (e.Index < 0)
        return;
      GroupHeaderBand c = lbGroups.Items[e.Index] as GroupHeaderBand;
      Image img = RegisteredObjects.FindObject(c).Image;
      e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y);
      TextRenderer.DrawText(e.Graphics, c.GetInfoText(), e.Font,
        new Point(e.Bounds.X + 24, e.Bounds.Y + (e.Bounds.Height - lbGroups.ItemHeight) / 2), e.ForeColor);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(cbxDataColumn.Text))
        return;
        
      GroupHeaderBand group = new GroupHeaderBand();
      group.Condition = cbxDataColumn.Text;
      group.SetReport(FReport);
      
      lbGroups.Items.Add(group);
      lbGroups.SelectedItem = group;
    }

    private void lbGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateButtonsState();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      GroupHeaderBand group = lbGroups.SelectedItem as GroupHeaderBand;
      group.InvokeEditor();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      int index = lbGroups.SelectedIndex;
      lbGroups.Items.RemoveAt(index);
      if (index >= lbGroups.Items.Count)
        index--;
      if (index < 0)
        index = 0;
      if (index < lbGroups.Items.Count)
        lbGroups.SelectedIndex = index;
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      object item = lbGroups.SelectedItem;
      int index = lbGroups.SelectedIndex;
      lbGroups.Items.Remove(item);
      lbGroups.Items.Insert(index - 1, item);
      lbGroups.SelectedItem = item;
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      object item = lbGroups.SelectedItem;
      int index = lbGroups.SelectedIndex;
      lbGroups.Items.Remove(item);
      lbGroups.Items.Insert(index + 1, item);
      lbGroups.SelectedItem = item;
    }

    private void GroupExpertForm_Shown(object sender, EventArgs e)
    {
      lblHint.Width = gbGroupCondition.Width - lblHint.Left * 2;
      lbGroups.Height = gbGroups.Height - lbGroups.Top - 12;
    }

    private void GroupExpertForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }

    private void Init()
    {
      cbxDataColumn.Report = FReport;
      lbGroups.ItemHeight = DrawUtils.DefaultItemHeight + 2;

      // fill existing groups
      foreach (BandBase band in FPage.Bands)
      {
        if (band is GroupHeaderBand)
        {
          FillGroups(band as GroupHeaderBand);
          break;
        }
      }

      UpdateButtonsState();
    }

    private void Done()
    {
      if (DialogResult == DialogResult.OK)
      {
        float defaultHeight = Units.Millimeters * 10;
        if (ReportWorkspace.Grid.GridUnits == PageUnits.Inches ||
          ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch)
          defaultHeight = Units.Inches * 0.4f;

        GroupHeaderBand initialGroup = null;
        DataBand data = null;
        int childIndex = -1;
        foreach (BandBase band in FPage.Bands)
        {
          if (band is GroupHeaderBand)
          {
            childIndex = band.ZOrder;
            initialGroup = band as GroupHeaderBand;
            data = (band as GroupHeaderBand).GroupDataBand;
            break;
          }
          else if (band is DataBand)
          {
            childIndex = band.ZOrder;
            data = band as DataBand;
            break;
          }
        }

        // report has no groups nor databands, create a databand
        if (childIndex == -1)
        {
          data = new DataBand();
          data.Height = defaultHeight;
          childIndex = 0;
        }

        // connect groups to each other
        data.Parent = null;
        Base parent = null;
        for (int i = 0; i < lbGroups.Items.Count; i++)
        {
          GroupHeaderBand group = lbGroups.Items[i] as GroupHeaderBand;
          group.Parent = parent;
          group.Data = i < lbGroups.Items.Count - 1 ? null : data;
          parent = group;
        }
        
        // insert a group to the report page
        if (lbGroups.Items.Count > 0)
        {
          GroupHeaderBand firstGroup = lbGroups.Items[0] as GroupHeaderBand;
          FPage.Bands.Insert(childIndex, firstGroup);
          
          // create unique names
          if (String.IsNullOrEmpty(firstGroup.GroupDataBand.Name))
            firstGroup.GroupDataBand.CreateUniqueName();
          for (int i = 0; i < lbGroups.Items.Count; i++)
          {
            GroupHeaderBand group = lbGroups.Items[i] as GroupHeaderBand;
            if (String.IsNullOrEmpty(group.Name))
            {
              group.Height = defaultHeight;
              group.CreateUniqueName();
              
              // create text object with group name
              TextObject text = new TextObject();
              text.Parent = group;
              text.CreateUniqueName();
              text.Bounds = new RectangleF(new PointF(0, 0), text.GetPreferredSize());
              text.Text = "[" + group.Condition + "]";
              
              // create group footer
              group.GroupFooter = new GroupFooterBand();
              group.GroupFooter.Height = defaultHeight;
              group.GroupFooter.CreateUniqueName();
            }  
          }
        }  
        else
        {
          if (!String.IsNullOrEmpty(data.Name))
            FPage.Bands.Insert(childIndex, data);
        }

        // delete initial group if it was deleted in the expert
        if (initialGroup != null && !lbGroups.Items.Contains(initialGroup))
          initialGroup.Dispose();
      }
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,GroupExpert");
      Text = res.Get("");
      gbGroupCondition.Text = res.Get("Condition");
      lblHint.Text = res.Get("Hint");
      btnAdd.Text = res.Get("Add");
      gbGroups.Text = res.Get("Groups");
      btnEdit.Text = res.Get("Edit");
      btnDelete.Text = res.Get("Delete");
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
    }

    public GroupExpertForm(Designer designer)
    {
      FDesigner = designer;
      FReport = designer.ActiveReport;
      FPage = designer.ActiveReportTab.ActivePage as ReportPage;
      InitializeComponent();
      Init();
      Localize();
    }
  }
}

