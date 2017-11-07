using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Design.StandardDesigner;
using FastReport.DevComponents.DotNetBar;
using System.Globalization;

namespace FastReport.Design.PageDesigners.Page
{
  internal class ReportPageDesigner : PageDesignerBase
  {
    #region Fields
    private RulerPanel FRulerPanel;
    private ButtonItem miViewGrid;
    private ButtonItem miViewGuides;
    private ButtonItem miViewDeleteHGuides;
    private ButtonItem miViewDeleteVGuides;
    private ButtonItem miViewAutoGuides;
    private ButtonItem miViewUnits;
    private ButtonItem miViewUnitsMillimeters;
    private ButtonItem miViewUnitsCentimeters;
    private ButtonItem miViewUnitsInches;
    private ButtonItem miViewUnitsHundrethsOfInch;

    private ButtonItem miReportTitle;
    private ButtonItem miReportSummary;
    private ButtonItem miPageHeader;
    private ButtonItem miPageFooter;
    private ButtonItem miColumnHeader;
    private ButtonItem miColumnFooter;
    private ButtonItem miOverlay;

    private ButtonItem miReportBands;
    private ButtonItem miReportGroupExpert;
    private ButtonItem miReportStyles;
    #endregion

    #region Properties
    public ReportWorkspace Workspace
    {
      get { return FRulerPanel.Workspace; }
    }
    
    public RulerPanel RulerPanel
    {
      get { return FRulerPanel; }
    }
    #endregion

    #region Private Methods
    private void UpdateName()
    {
      if (Page.Name == "")
        Text = Page.ClassName + (Page.ZOrder + 1).ToString();
      else
        Text = Page.Name;
    }

    private void CreateOwnMenuItems()
    {
      if (!(Designer is DesignerControl))
        return;

      DesignerMenu menu = (Designer as DesignerControl).MainMenu;

      miViewGrid = menu.CreateMenuItem(new EventHandler(MenuViewGrid_Click));
      miViewGrid.AutoCheckOnClick = true;
      miViewGrid.BeginGroup = true;
      miViewGuides = menu.CreateMenuItem(new EventHandler(MenuViewGuides_Click));
      miViewGuides.AutoCheckOnClick = true;
      miViewAutoGuides = menu.CreateMenuItem(new EventHandler(MenuViewAutoGuides_Click));
      miViewAutoGuides.AutoCheckOnClick = true;
      miViewDeleteHGuides = menu.CreateMenuItem(new EventHandler(MenuViewDeleteHGuides_Click));
      miViewDeleteVGuides = menu.CreateMenuItem(new EventHandler(MenuViewDeleteVGuides_Click));

      miViewUnits = menu.CreateMenuItem();
      miViewUnits.BeginGroup = true;
      miViewUnitsMillimeters = menu.CreateMenuItem(new EventHandler(miViewUnits_Click));
      miViewUnitsMillimeters.AutoCheckOnClick = true;
      miViewUnitsCentimeters = menu.CreateMenuItem(new EventHandler(miViewUnits_Click));
      miViewUnitsCentimeters.AutoCheckOnClick = true;
      miViewUnitsInches = menu.CreateMenuItem(new EventHandler(miViewUnits_Click));
      miViewUnitsInches.AutoCheckOnClick = true;
      miViewUnitsHundrethsOfInch = menu.CreateMenuItem(new EventHandler(miViewUnits_Click));
      miViewUnitsHundrethsOfInch.AutoCheckOnClick = true;

      miViewUnits.SubItems.AddRange(new BaseItem[] {
        miViewUnitsMillimeters, miViewUnitsCentimeters, miViewUnitsInches, miViewUnitsHundrethsOfInch });

      miReportBands = menu.CreateMenuItem(new EventHandler(miInsertBands_Click));
      miReportBands.BeginGroup = true;
      miReportGroupExpert = menu.CreateMenuItem(Res.GetImage(86), new EventHandler(miReportGroupExpert_Click));
      miReportStyles = menu.CreateMenuItem(Res.GetImage(87), new EventHandler(Designer.cmdReportStyles.Invoke));

      miReportTitle = menu.CreateMenuItem(Res.GetImage(154), new EventHandler(miReportTitle_Click));
      miReportTitle.AutoCheckOnClick = true;
      miReportSummary = menu.CreateMenuItem(Res.GetImage(155), new EventHandler(miReportSummary_Click));
      miReportSummary.AutoCheckOnClick = true;
      miPageHeader = menu.CreateMenuItem(Res.GetImage(156), new EventHandler(miPageHeader_Click));
      miPageHeader.AutoCheckOnClick = true;
      miPageFooter = menu.CreateMenuItem(Res.GetImage(157), new EventHandler(miPageFooter_Click));
      miPageFooter.AutoCheckOnClick = true;
      miColumnHeader = menu.CreateMenuItem(Res.GetImage(158), new EventHandler(miColumnHeader_Click));
      miColumnHeader.AutoCheckOnClick = true;
      miColumnFooter = menu.CreateMenuItem(Res.GetImage(159), new EventHandler(miColumnFooter_Click));
      miColumnFooter.AutoCheckOnClick = true;
      miOverlay = menu.CreateMenuItem(Res.GetImage(166), new EventHandler(miOverlay_Click));
      miOverlay.AutoCheckOnClick = true;

      // insert new items before the "Options..." item
      int insertPos = menu.miView.SubItems.IndexOf(menu.miViewOptions);
      menu.miView.SubItems.Insert(insertPos, miViewGrid);
      menu.miView.SubItems.Insert(insertPos + 1, miViewGuides);
      menu.miView.SubItems.Insert(insertPos + 2, miViewAutoGuides);
      menu.miView.SubItems.Insert(insertPos + 3, miViewDeleteHGuides);
      menu.miView.SubItems.Insert(insertPos + 4, miViewDeleteVGuides);
      menu.miView.SubItems.Insert(insertPos + 5, miViewUnits);

      insertPos = 0;
      menu.miReport.SubItems.Insert(insertPos, miReportTitle);
      menu.miReport.SubItems.Insert(insertPos + 1, miReportSummary);
      menu.miReport.SubItems.Insert(insertPos + 2, miPageHeader);
      menu.miReport.SubItems.Insert(insertPos + 3, miPageFooter);
      menu.miReport.SubItems.Insert(insertPos + 4, miColumnHeader);
      menu.miReport.SubItems.Insert(insertPos + 5, miColumnFooter);
      menu.miReport.SubItems.Insert(insertPos + 6, miOverlay);

      menu.miReport.SubItems.Insert(insertPos + 7, miReportBands);
      menu.miReport.SubItems.Insert(insertPos + 8, miReportGroupExpert);
      menu.miReport.SubItems.Insert(insertPos + 9, miReportStyles);

      menu.miView.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(miView_PopupOpen);
      menu.miReport.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(miReport_PopupOpen);
    }

    private void DeleteOwnMenuItems()
    {
      if (!(Designer is DesignerControl))
        return;

      DesignerMenu menu = (Designer as DesignerControl).MainMenu;
      menu.miView.PopupOpen -= new DotNetBarManager.PopupOpenEventHandler(miView_PopupOpen);
      menu.miReport.PopupOpen -= new DotNetBarManager.PopupOpenEventHandler(miReport_PopupOpen);

      if (miViewGrid != null)
      {
        menu.miView.SubItems.Remove(miViewGrid);
        menu.miView.SubItems.Remove(miViewGuides);
        menu.miView.SubItems.Remove(miViewAutoGuides);
        menu.miView.SubItems.Remove(miViewDeleteHGuides);
        menu.miView.SubItems.Remove(miViewDeleteVGuides);
        menu.miView.SubItems.Remove(miViewUnits);
        
        miViewGrid.Dispose();
        miViewGuides.Dispose();
        miViewAutoGuides.Dispose();
        miViewDeleteHGuides.Dispose();
        miViewDeleteVGuides.Dispose();
        miViewUnits.Dispose();

        menu.miReport.SubItems.Remove(miReportBands);
        menu.miReport.SubItems.Remove(miReportStyles);
        menu.miReport.SubItems.Remove(miReportGroupExpert);
        menu.miReport.SubItems.Remove(miReportTitle);
        menu.miReport.SubItems.Remove(miPageHeader);
        menu.miReport.SubItems.Remove(miColumnHeader);
        menu.miReport.SubItems.Remove(miColumnFooter);
        menu.miReport.SubItems.Remove(miReportSummary);
        menu.miReport.SubItems.Remove(miPageFooter);
        menu.miReport.SubItems.Remove(miOverlay);
        
        miReportBands.Dispose();
        miReportStyles.Dispose();
        miReportGroupExpert.Dispose();
        miReportTitle.Dispose();
        miPageHeader.Dispose();
        miColumnHeader.Dispose();
        miColumnFooter.Dispose();
        miReportSummary.Dispose();
        miPageFooter.Dispose();
        miOverlay.Dispose();
      }

      miViewGrid = null;
      miViewGuides = null;
      miViewAutoGuides = null;
      miViewDeleteHGuides = null;
      miViewDeleteVGuides = null;
      miViewUnits = null;

      miReportBands = null;
      miReportStyles = null;
      miReportGroupExpert = null;
      miReportTitle = null;
      miPageHeader = null;
      miColumnHeader = null;
      miColumnFooter = null;
      miReportSummary = null;
      miPageFooter = null;
      miOverlay = null;
    }

    private void LocalizeMenuItems()
    {
      if (miViewGrid != null)
      {
        MyRes res = new MyRes("Designer,Menu,View");
        miViewGrid.Text = res.Get("Grid");
        miViewGuides.Text = res.Get("Guides");
        miViewAutoGuides.Text = res.Get("AutoGuides");
        miViewDeleteHGuides.Text = res.Get("DeleteHGuides");
        miViewDeleteVGuides.Text = res.Get("DeleteVGuides");
        miViewUnits.Text = res.Get("Units");
        
        res = new MyRes("Forms,ReportPageOptions");
        miViewUnitsMillimeters.Text = res.Get("Millimeters");
        miViewUnitsCentimeters.Text = res.Get("Centimeters");
        miViewUnitsInches.Text = res.Get("Inches");
        miViewUnitsHundrethsOfInch.Text = res.Get("HundrethsOfInch");

        res = new MyRes("Designer,Menu,Report");
        miReportBands.Text = res.Get("Bands");
        miReportGroupExpert.Text = res.Get("GroupExpert");
        miReportStyles.Text = res.Get("Styles");

        res = new MyRes("Objects,Bands");
        miReportTitle.Text = res.Get("ReportTitle");
        miReportSummary.Text = res.Get("ReportSummary");
        miPageHeader.Text = res.Get("PageHeader");
        miPageFooter.Text = res.Get("PageFooter");
        miColumnHeader.Text = res.Get("ColumnHeader");
        miColumnFooter.Text = res.Get("ColumnFooter");
        miOverlay.Text = res.Get("Overlay");
      }
    }
    
    public void SetDefaults(BandBase band)
    {
      band.CreateUniqueName();
      band.Height = band.GetPreferredSize().Height;
    }

    public void Change()
    {
      Designer.SetModified(null, "ChangeReport");
    }

    private void miReport_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      bool bandsEnabled = Designer.cmdInsertBand.Enabled;
      miReportBands.Enabled = bandsEnabled;
      miReportGroupExpert.Enabled = bandsEnabled;

      ReportPage page = Page as ReportPage;
      bool isSubreport = page.Subreport != null;

      miReportTitle.Enabled = bandsEnabled && !isSubreport;
      miReportSummary.Enabled = bandsEnabled && !isSubreport;
      miPageHeader.Enabled = bandsEnabled && !isSubreport;
      miPageFooter.Enabled = bandsEnabled && !isSubreport;
      miColumnHeader.Enabled = bandsEnabled && !isSubreport;
      miColumnFooter.Enabled = bandsEnabled && !isSubreport;
      miOverlay.Enabled = bandsEnabled && !isSubreport;

      miReportTitle.Checked = page.ReportTitle != null;
      miReportSummary.Checked = page.ReportSummary != null;
      miPageHeader.Checked = page.PageHeader != null;
      miPageFooter.Checked = page.PageFooter != null;
      miColumnHeader.Checked = page.ColumnHeader != null;
      miColumnFooter.Checked = page.ColumnFooter != null;
      miOverlay.Checked = page.Overlay != null;
    }

    private void miView_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      miViewGrid.Checked = ReportWorkspace.ShowGrid;
      miViewGuides.Checked = ReportWorkspace.ShowGuides;
      bool autoGuides = ReportWorkspace.AutoGuides;
      miViewAutoGuides.Checked = autoGuides;
      miViewDeleteHGuides.Enabled = !autoGuides;
      miViewDeleteVGuides.Enabled = !autoGuides;

      miViewUnitsMillimeters.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters;
      miViewUnitsCentimeters.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Centimeters;
      miViewUnitsInches.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Inches;
      miViewUnitsHundrethsOfInch.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch;
    }

    private void MenuViewGrid_Click(object sender, EventArgs e)
    {
      ReportWorkspace.ShowGrid = miViewGrid.Checked;
      Workspace.Refresh();
    }

    private void MenuViewGuides_Click(object sender, EventArgs e)
    {
      ReportWorkspace.ShowGuides = miViewGuides.Checked;
      Workspace.Refresh();
    }

    private void MenuViewAutoGuides_Click(object sender, EventArgs e)
    {
      ReportWorkspace.AutoGuides = miViewAutoGuides.Checked;
      Workspace.Refresh();
    }

    private void MenuViewDeleteHGuides_Click(object sender, EventArgs e)
    {
      Workspace.DeleteHGuides();
    }

    private void MenuViewDeleteVGuides_Click(object sender, EventArgs e)
    {
      Workspace.DeleteVGuides();
    }

    private void miViewUnits_Click(object sender, EventArgs e)
    {
      if (sender == miViewUnitsMillimeters)
        ReportWorkspace.Grid.GridUnits = PageUnits.Millimeters;
      else if (sender == miViewUnitsCentimeters)
        ReportWorkspace.Grid.GridUnits = PageUnits.Centimeters;
      else if (sender == miViewUnitsInches)
        ReportWorkspace.Grid.GridUnits = PageUnits.Inches;
      else
        ReportWorkspace.Grid.GridUnits = PageUnits.HundrethsOfInch;
      
      UpdateContent();
    }

    private void miInsertBands_Click(object sender, EventArgs e)
    {
      using (ConfigureBandsForm form = new ConfigureBandsForm(Designer))
      {
        form.Page = Page as ReportPage;
        form.ShowDialog();
      }
    }

    private void miReportGroupExpert_Click(object sender, EventArgs e)
    {
      using (GroupExpertForm form = new GroupExpertForm(Designer))
      {
        if (form.ShowDialog() == DialogResult.OK)
          Designer.SetModified(null, "ChangeReport");
      }
    }

    private void miReportTitle_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.ReportTitle = new ReportTitleBand();
        SetDefaults(page.ReportTitle);
      }
      else
      {
        page.ReportTitle = null;
      }
      Change();
    }

    private void miReportSummary_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.ReportSummary = new ReportSummaryBand();
        SetDefaults(page.ReportSummary);
      }
      else
      {
        page.ReportSummary = null;
      }
      Change();
    }

    private void miPageHeader_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.PageHeader = new PageHeaderBand();
        SetDefaults(page.PageHeader);
      }
      else
      {
        page.PageHeader = null;
      }
      Change();
    }

    private void miPageFooter_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.PageFooter = new PageFooterBand();
        SetDefaults(page.PageFooter);
      }
      else
      {
        page.PageFooter = null;
      }
      Change();
    }

    private void miColumnHeader_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.ColumnHeader = new ColumnHeaderBand();
        SetDefaults(page.ColumnHeader);
      }
      else
      {
        page.ColumnHeader = null;
      }
      Change();
    }

    private void miColumnFooter_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.ColumnFooter = new ColumnFooterBand();
        SetDefaults(page.ColumnFooter);
      }
      else
      {
        page.ColumnFooter = null;
      }
      Change();
    }

    private void miOverlay_Click(object sender, EventArgs e)
    {
      ReportPage page = Page as ReportPage;
      if ((sender as ButtonItem).Checked)
      {
        page.Overlay = new OverlayBand();
        SetDefaults(page.Overlay);
      }
      else
      {
        page.Overlay = null;
      }
      Change();
    }
    #endregion
    
    #region Public Methods
    public override Base GetParentForPastedObjects()
    {
      return Workspace.GetParentForPastedObjects();
    }

    public override void PageActivated()
    {
      base.PageActivated();
      DeleteOwnMenuItems();
      CreateOwnMenuItems();
      LocalizeMenuItems();
    }

    public override void PageDeactivated()
    {
      base.PageDeactivated();
      DeleteOwnMenuItems();
    }

    public override void Paste(ObjectCollection list, InsertFrom source)
    {
      Workspace.Paste(list, source);
    }

    public override void CancelPaste()
    {
      Workspace.CancelPaste();
    }

    public override void SelectAll()
    {
      Workspace.SelectAll();
    }
    #endregion
    
    #region IDesignerPlugin
    public override void SaveState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      string units = "";
      switch (ReportWorkspace.Grid.GridUnits)
      {
        case PageUnits.Millimeters:
          units = "Millimeters";
          break;
        case PageUnits.Centimeters:
          units = "Centimeters";
          break;
        case PageUnits.Inches:
          units = "Inches";
          break;
        case PageUnits.HundrethsOfInch:
          units = "HundrethsOfInch";
          break;
      }
      xi.SetProp("Units", units);
      xi.SetProp("SnapSizeMillimeters", ReportWorkspace.Grid.SnapSizeMillimeters.ToString(CultureInfo.InvariantCulture.NumberFormat));
      xi.SetProp("SnapSizeCentimeters", ReportWorkspace.Grid.SnapSizeCentimeters.ToString(CultureInfo.InvariantCulture.NumberFormat));
      xi.SetProp("SnapSizeInches", ReportWorkspace.Grid.SnapSizeInches.ToString(CultureInfo.InvariantCulture.NumberFormat));
      xi.SetProp("SnapSizeHundrethsOfInch", ReportWorkspace.Grid.SnapSizeHundrethsOfInch.ToString(CultureInfo.InvariantCulture.NumberFormat));
      xi.SetProp("ShowGrid", ReportWorkspace.ShowGrid ? "1" : "0");
      xi.SetProp("SnapToGrid", ReportWorkspace.SnapToGrid ? "1" : "0");
      xi.SetProp("DottedGrid", ReportWorkspace.Grid.Dotted ? "1" : "0");
      xi.SetProp("MarkerStyle", ReportWorkspace.MarkerStyle.ToString());
      xi.SetProp("BandStructureSplitter", RulerPanel.SplitterDistance.ToString());
      xi.SetProp("Scale", ReportWorkspace.Scale.ToString());
      xi.SetProp("AutoGuides", ReportWorkspace.AutoGuides ? "1" : "0");
      xi.SetProp("ClassicView", ReportWorkspace.ClassicView ? "1" : "0");
      xi.SetProp("EditAfterInsert", ReportWorkspace.EditAfterInsert ? "1" : "0");
    }

    public override void RestoreState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      string s = xi.GetProp("BandStructureSplitter");
      if (s != "")
        RulerPanel.SplitterDistance = int.Parse(s);
      // all other ReportWorkspace properties are restored in the ReportWorkspace static constructor
    }

    public override void SelectionChanged()
    {
      base.SelectionChanged();
      UpdateContent();
    }

    public override void UpdateContent()
    {
      if (Locked)
        return;
      base.UpdateContent();
      UpdateName();
      RulerPanel.Structure.btnConfigure.Enabled = Designer.cmdInsertBand.Enabled;
      RulerPanel.SetStructureVisible(!ReportWorkspace.ClassicView);
      RulerPanel.Refresh();
    }

    public override void Localize()
    {
      base.Localize();
      RulerPanel.Structure.Localize();
      LocalizeMenuItems();
      UpdateContent();
    }

    public override DesignerOptionsPage GetOptionsPage()
    {
      return new ReportPageOptions(this);
    }

    public override void UpdateUIStyle()
    {
      FRulerPanel.UpdateUIStyle();
    }
    #endregion

    public ReportPageDesigner(Designer designer) : base(designer)
    {
      Name = "Report";
      FRulerPanel = new RulerPanel(this);
      FRulerPanel.Dock = DockStyle.Fill;
      Controls.Add(FRulerPanel);
    }
  }
}
