using System;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;

namespace FastReport.Forms
{
  internal partial class ConfigureBandsForm : BaseDialogForm
  {
    private Designer FDesigner;
    private ReportPage FPage;
    private BandBase FLastSelectedBand;
    
    public ReportPage Page
    {
      get { return FPage; }
      set 
      {
        FPage = value;
        UpdateBands();
      }
    }

    private TreeNodeCollection AddBand(BandBase band, TreeNodeCollection nodes)
    {
      if (band != null)
      {
        ObjectInfo info = RegisteredObjects.FindObject(band);
        string infoText = Res.Get(info.Text);
        infoText += ": " + band.Name;
        if (band is DataBand || band is GroupHeaderBand)
        {
          if (!String.IsNullOrEmpty(band.GetInfoText()))
            infoText += " (" + band.GetInfoText() + ")";
        }
        TreeNode node = nodes.Add(infoText);
        int imageIndex = info.ImageIndex;
        node.ImageIndex = imageIndex;
        node.SelectedImageIndex = imageIndex;
        node.Tag = band;
        if (band == FLastSelectedBand)
          tvBands.SelectedNode = node;
        AddBand(band.Child, node.Nodes);
        return node.Nodes;
      }
      return null;
    }

    private void EnumDataBand(DataBand band, TreeNodeCollection nodes)
    {
      if (band == null)
        return;
      AddBand(band.Header, nodes);
      TreeNodeCollection dataNodes = AddBand(band, nodes);
      foreach (BandBase b in band.Bands)
      {
        EnumBand(b, dataNodes);
      }
      AddBand(band.Footer, nodes);
    }

    private void EnumGroupHeaderBand(GroupHeaderBand band, TreeNodeCollection nodes)
    {
      if (band == null)
        return;
      AddBand(band.Header, nodes);
      TreeNodeCollection groupNodes = AddBand(band, nodes);
      EnumGroupHeaderBand(band.NestedGroup, groupNodes);
      EnumDataBand(band.Data, groupNodes);
      AddBand(band.GroupFooter, nodes);
      AddBand(band.Footer, nodes);
    }

    private void EnumBand(BandBase band, TreeNodeCollection nodes)
    {
      if (band is DataBand)
        EnumDataBand(band as DataBand, nodes);
      else if (band is GroupHeaderBand)
        EnumGroupHeaderBand(band as GroupHeaderBand, nodes);
    }

    private void EnumBands(TreeNodeCollection nodes)
    {
      if (Page.TitleBeforeHeader)
      {
        AddBand(Page.ReportTitle, nodes);
        AddBand(Page.PageHeader, nodes);
      }
      else
      {
        AddBand(Page.PageHeader, nodes);
        AddBand(Page.ReportTitle, nodes);
      }
      AddBand(Page.ColumnHeader, nodes);
      foreach (BandBase b in Page.Bands)
      {
        EnumBand(b, nodes);
      }
      AddBand(Page.ColumnFooter, nodes);
      AddBand(Page.ReportSummary, nodes);
      AddBand(Page.PageFooter, nodes);
      AddBand(Page.Overlay, nodes);
    }

    private void SetDefaults(BandBase band)
    {
      band.CreateUniqueName();
      band.Height = band.GetPreferredSize().Height;
    }
    
    private void Change()
    {
      UpdateBands();
      UpdateControls();
      FDesigner.SetModified(null, "ChangeReport");
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      miDelete.Visible = false;
      sep1.Visible = false;
      mnuBands.Show(btnAdd.PointToScreen(new Point(0, btnAdd.Height)));
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode.Tag as BandBase;
      band.Delete();
      tvBands.SelectedNode = tvBands.SelectedNode.NextVisibleNode;
      Change();
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode.Tag as BandBase;
      if (band.Parent == Page || band is DataBand)
        band.ZOrder--;
      else if (band is ChildBand && band.Parent is ChildBand)
      {
        ChildBand parent = band.Parent as ChildBand;
        BandBase newParent = parent.Parent as BandBase;
        parent.Parent = null;
        band.Parent = newParent;
        parent.Child = band.Child;
        band.Child = parent;
      }
      else if (band is GroupHeaderBand && band.Parent is GroupHeaderBand)
      {
        GroupHeaderBand group = band as GroupHeaderBand;
        GroupHeaderBand parent = band.Parent as GroupHeaderBand;
        Base newParent = parent.Parent;
        BandBase child = null;
        if (group.Data != null)
          child = group.Data;
        else
          child = group.NestedGroup;
        int zOrder = parent.ZOrder;
        parent.Parent = null;
        group.Parent = newParent;
        group.ZOrder = zOrder;
        child.Parent = parent;
        parent.Parent = group;
      }
      Change();
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode.Tag as BandBase;
      if (band.Parent == Page || band is DataBand)
        band.ZOrder += 2;
      else if (band is ChildBand && band.Child != null)
      {
        BandBase parent = band.Parent as BandBase;
        ChildBand child = band.Child;
        band.Parent = null;
        child.Parent = parent;
        band.Child = child.Child;
        band.Parent = child;
      }
      else if (band is GroupHeaderBand && (band as GroupHeaderBand).NestedGroup != null)
      {
        GroupHeaderBand group = band as GroupHeaderBand;
        Base parent = band.Parent;
        GroupHeaderBand child = group.NestedGroup;
        int zOrder = group.ZOrder;
        group.Parent = null;
        child.Parent = parent;
        child.ZOrder = zOrder;
        if (child.Data != null)
          group.Data = child.Data;
        else
          group.NestedGroup = child.NestedGroup;
        group.Parent = child;  
      }
      Change();
    }

    private void tvBands_AfterSelect(object sender, TreeViewEventArgs e)
    {
      UpdateControls();
    }

    private void tvBands_MouseDown(object sender, MouseEventArgs e)
    {
      tvBands.SelectedNode = tvBands.GetNodeAt(e.Location);
      UpdateControls();
    }

    private void tvBands_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        miDelete.Visible = true;
        sep1.Visible = true;
        mnuBands.Show(tvBands.PointToScreen(e.Location));
      }
    }

    private void miPageHeader_Click(object sender, EventArgs e)
    {
      Page.PageHeader = new PageHeaderBand();
      SetDefaults(Page.PageHeader);
      Change();  
    }

    private void miReportTitle_Click(object sender, EventArgs e)
    {
      Page.ReportTitle = new ReportTitleBand();
      SetDefaults(Page.ReportTitle);
      Change();
    }

    private void miColumnHeader_Click(object sender, EventArgs e)
    {
      Page.ColumnHeader = new ColumnHeaderBand();
      SetDefaults(Page.ColumnHeader);
      Change();
    }

    private void miDataHeader_Click(object sender, EventArgs e)
    {
      if (tvBands.SelectedNode.Tag is DataBand)
      {
        DataBand data = tvBands.SelectedNode.Tag as DataBand;
        data.Header = new DataHeaderBand();
        SetDefaults(data.Header);
      }
      else if (tvBands.SelectedNode.Tag is GroupHeaderBand)
      {
        GroupHeaderBand group = tvBands.SelectedNode.Tag as GroupHeaderBand;
        group.Header = new DataHeaderBand();
        SetDefaults(group.Header);
      }
      Change();
    }

    private void miData_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode != null ? tvBands.SelectedNode.Tag as BandBase : null;
      DataBand newBand = new DataBand();
      if (band is DataBand)
        (band as DataBand).Bands.Add(newBand);
      else
        Page.Bands.Add(newBand);
      SetDefaults(newBand);
      Change();
    }

    private void miGroup_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode != null ? tvBands.SelectedNode.Tag as BandBase : null;
      GroupHeaderBand group = new GroupHeaderBand();
      
      // if we select something other than databand or a group, add a group to the first page databand.
      if (!(band is DataBand) && !(band is GroupHeaderBand))
      {
        foreach (BandBase b in Page.Bands)
        {
          if (b is DataBand)
          {
            band = b;
            break;
          }
        }
      }
      
      if (band is GroupHeaderBand)
      {
        GroupHeaderBand parent = band as GroupHeaderBand;
        if (parent.Data != null)
        {
          group.Data = parent.Data;
          parent.NestedGroup = group;
        }
        else if (parent.NestedGroup != null)
        {
          group.NestedGroup = parent.NestedGroup;
          parent.NestedGroup = group;
        }
      }
      else if (band is DataBand)
      {
        if (band.Parent is GroupHeaderBand)
        {
          GroupHeaderBand parent = band.Parent as GroupHeaderBand;
          group.Data = band as DataBand;
          parent.NestedGroup = group;
        }
        else
        {
          int zOrder = band.ZOrder;
          Base parent = band.Parent;
          group.Data = band as DataBand;
          group.Parent = parent;
          group.ZOrder = zOrder;
        }
      }
      else
      {
        Page.Bands.Add(group);
        group.Data = new DataBand();
        SetDefaults(group.Data);
      }
      SetDefaults(group);
      group.GroupFooter = new GroupFooterBand();
      SetDefaults(group.GroupFooter);
      Change();
    }

    private void miChild_Click(object sender, EventArgs e)
    {
      BandBase band = tvBands.SelectedNode.Tag as BandBase;
      band.Child = new ChildBand();
      SetDefaults(band.Child);
      Change();
    }

    private void miDataFooter_Click(object sender, EventArgs e)
    {
      if (tvBands.SelectedNode.Tag is DataBand)
      {
        DataBand data = tvBands.SelectedNode.Tag as DataBand;
        data.Footer = new DataFooterBand();
        SetDefaults(data.Footer);
      }
      else if (tvBands.SelectedNode.Tag is GroupHeaderBand)
      {
        GroupHeaderBand group = tvBands.SelectedNode.Tag as GroupHeaderBand;
        group.Footer = new DataFooterBand();
        SetDefaults(group.Footer);
      }
      Change();
    }

    private void miGroupFooter_Click(object sender, EventArgs e)
    {
      GroupHeaderBand groupHeader = tvBands.SelectedNode.Tag as GroupHeaderBand;
      groupHeader.GroupFooter = new GroupFooterBand();
      SetDefaults(groupHeader.GroupFooter);
      Change();
    }

    private void miColumnFooter_Click(object sender, EventArgs e)
    {
      Page.ColumnFooter = new ColumnFooterBand();
      SetDefaults(Page.ColumnFooter);
      Change();
    }

    private void miReportSummary_Click(object sender, EventArgs e)
    {
      Page.ReportSummary = new ReportSummaryBand();
      SetDefaults(Page.ReportSummary);
      Change();
    }

    private void miPageFooter_Click(object sender, EventArgs e)
    {
      Page.PageFooter = new PageFooterBand();
      SetDefaults(Page.PageFooter);
      Change();
    }

    private void miOverlay_Click(object sender, EventArgs e)
    {
      Page.Overlay = new OverlayBand();
      SetDefaults(Page.Overlay);
      Change();
    }

    private void UpdateBands()
    {
      FLastSelectedBand = tvBands.SelectedNode != null ? tvBands.SelectedNode.Tag as BandBase : null;
      tvBands.BeginUpdate();
      tvBands.Nodes.Clear();
      EnumBands(tvBands.Nodes);
      tvBands.ExpandAll();
      tvBands.EndUpdate();
      if (FLastSelectedBand == null && tvBands.Nodes.Count > 0)
        tvBands.SelectedNode = tvBands.Nodes[0];
    }

    private void UpdateControls()
    {
      BandBase selected = tvBands.SelectedNode != null ? tvBands.SelectedNode.Tag as BandBase : null;
      bool isSubreport = Page.Subreport != null;
      
      miPageHeader.Enabled = Page.PageHeader == null && !isSubreport;
      miReportTitle.Enabled = Page.ReportTitle == null && !isSubreport;
      miColumnHeader.Enabled = Page.ColumnHeader == null && !isSubreport;
      miDataHeader.Enabled = (selected is DataBand && (selected as DataBand).Header == null) ||
        (selected is GroupHeaderBand && (selected as GroupHeaderBand).Header == null);
      miDataFooter.Enabled = (selected is DataBand && (selected as DataBand).Footer == null) ||
        (selected is GroupHeaderBand && (selected as GroupHeaderBand).Footer == null);
      miGroupFooter.Enabled = selected is GroupHeaderBand && (selected as GroupHeaderBand).GroupFooter == null;
      miColumnFooter.Enabled = Page.ColumnFooter == null && !isSubreport;
      miReportSummary.Enabled = Page.ReportSummary == null && !isSubreport;
      miPageFooter.Enabled = Page.PageFooter == null && !isSubreport;
      miChild.Enabled = selected != null && selected.Child == null;
      miOverlay.Enabled = Page.Overlay == null && !isSubreport;

      btnUp.Enabled = selected is GroupHeaderBand || selected is DataBand || selected is ChildBand;
      btnDown.Enabled = btnUp.Enabled;
      btnDelete.Enabled = selected != null && !(selected is DataBand && selected.Parent is GroupHeaderBand);
      miDelete.Enabled = btnDelete.Enabled;

      FDesigner.SelectedObjects.Clear();
      FDesigner.SelectedObjects.Add(selected);
      FDesigner.SelectionChanged(null);
    }

    private void ConfigureBandsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Config.SaveFormState(this);
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,ConfigureBands");
      MyRes res1 = new MyRes("Objects,Bands");
      Text = res.Get("");
      btnAdd.Text = res.Get("Add");
      btnDelete.Text = res.Get("Delete");
      btnOk.Text = Res.Get("Buttons,Close");

      miDelete.Text = res.Get("Delete");
      miPageHeader.Text = res1.Get("PageHeader");
      miReportTitle.Text = res1.Get("ReportTitle");
      miColumnHeader.Text = res1.Get("ColumnHeader");
      miDataHeader.Text = res1.Get("DataHeader");
      miData.Text = res1.Get("Data");
      miGroup.Text = res1.Get("GroupHeader");
      miDataFooter.Text = res1.Get("DataFooter");
      miGroupFooter.Text = res1.Get("GroupFooter");
      miColumnFooter.Text = res1.Get("ColumnFooter");
      miReportSummary.Text = res1.Get("ReportSummary");
      miPageFooter.Text = res1.Get("PageFooter");
      miChild.Text = res1.Get("Child");
      miOverlay.Text = res1.Get("Overlay");

      miDelete.Image = Res.GetImage(51);
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
      miPageHeader.Image = Res.GetImage(156);
      miReportTitle.Image = Res.GetImage(154);
      miColumnHeader.Image = Res.GetImage(158);
      miDataHeader.Image = Res.GetImage(160);
      miData.Image = Res.GetImage(162);
      miGroup.Image = Res.GetImage(163);
      miDataFooter.Image = Res.GetImage(161);
      miGroupFooter.Image = Res.GetImage(164);
      miColumnFooter.Image = Res.GetImage(159);
      miReportSummary.Image = Res.GetImage(155);
      miPageFooter.Image = Res.GetImage(157);
      miChild.Image = Res.GetImage(165);
      miOverlay.Image = Res.GetImage(166);
    }
    
    public ConfigureBandsForm(Designer designer)
    {
        FDesigner = designer;
        InitializeComponent();
        Localize();
        tvBands.ImageList = Res.GetImages();
        mnuBands.Renderer = Config.DesignerSettings.ToolStripRenderer;
        Config.RestoreFormState(this);

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            btnAdd.Left = ClientSize.Width - btnAdd.Left - btnAdd.Width;
            btnDelete.Left = ClientSize.Width - btnDelete.Left - btnDelete.Width;
            btnUp.Left = ClientSize.Width - btnUp.Left - btnUp.Width;
            btnDown.Left = ClientSize.Width - btnDown.Left - btnDown.Width;
            tvBands.Left = ClientSize.Width - tvBands.Left - tvBands.Width;
            btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
            btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
        }
    }
  }
}

