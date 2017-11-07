using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Design.PageDesigners.Page;

namespace FastReport.Forms
{
  internal partial class StandardReportWizardForm : BaseReportWizardForm
  {
    private Report FSampleReportTabular;
    private Report FSampleReportColumnar;
    private Report FSampleStyleReport;
    private StyleSheet FStyleSheet;

    #region "Create Groups" page
    private List<Column> Groups
    {
      get
      {
        List<Column> groups = new List<Column>();
        foreach (ListViewItem item in lvGroups.Items)
        {
          groups.Add(item.Tag as Column);
        }
        return groups;
      }
    }

    public override void ColumnsChanged()
    {
      base.ColumnsChanged();
      UpdateGroups();
    }
    
    private void UpdateGroups()
    {
      lvAvailableColumns1.Items.Clear();
      lvGroups.Items.Clear();

      foreach (Column column in SelectedColumns)
      {
        ListViewItem item = lvAvailableColumns1.Items.Add(column.Alias, column.GetImageIndex());
        item.Tag = column;
      }

      if (lvAvailableColumns1.Items.Count > 0)
        lvAvailableColumns1.Items[0].Selected = true;
      UpdateGroupControls();
    }

    private void UpdateGroupControls()
    {
      btnAddGroup.Enabled = lvAvailableColumns1.SelectedItems.Count > 0;
      btnRemoveGroup.Enabled = lvGroups.SelectedItems.Count > 0;

      bool reorderEnabled = lvGroups.SelectedItems.Count == 1;
      btnGroupUp.Enabled = reorderEnabled;
      btnGroupDown.Enabled = reorderEnabled;
      if (reorderEnabled)
      {
        if (lvGroups.SelectedIndices[0] == 0)
          btnGroupUp.Enabled = false;
        if (lvGroups.SelectedIndices[0] == lvGroups.Items.Count - 1)
          btnGroupDown.Enabled = false;
      }
    }

    private void lvAvailableColumns1_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateGroupControls();
    }

    private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateGroupControls();
    }

    private void btnAddGroup_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvAvailableColumns1.SelectedItems.Count > 0)
      {
        ListViewItem item = lvAvailableColumns1.SelectedItems[0];
        index = item.Index;
        lvAvailableColumns1.Items.Remove(item);
        lvGroups.Items.Add(item);
        item.Selected = false;
      }
      if (index >= lvAvailableColumns1.Items.Count)
        index = lvAvailableColumns1.Items.Count - 1;
      if (index < 0)
        index = 0;
      if (index < lvAvailableColumns1.Items.Count)
        lvAvailableColumns1.Items[index].Selected = true;
      UpdateGroupControls();
    }

    private void btnRemoveGroup_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvGroups.SelectedItems.Count > 0)
      {
        ListViewItem item = lvGroups.SelectedItems[0];
        index = item.Index;
        lvGroups.Items.Remove(item);
        lvAvailableColumns1.Items.Add(item);
        item.Selected = false;
      }
      if (index >= lvGroups.Items.Count)
        index = lvGroups.Items.Count - 1;
      if (index < 0)
        index = 0;
      if (index < lvGroups.Items.Count)
        lvGroups.Items[index].Selected = true;
      UpdateGroupControls();
    }

    private void btnGroupUp_Click(object sender, EventArgs e)
    {
      ListViewItem item = lvGroups.SelectedItems[0];
      int index = item.Index;
      lvGroups.Items.Remove(item);
      lvGroups.Items.Insert(index - 1, item);
      item.Selected = true;
      UpdateGroupControls();
    }

    private void btnGroupDown_Click(object sender, EventArgs e)
    {
      ListViewItem item = lvGroups.SelectedItems[0];
      int index = item.Index;
      lvGroups.Items.Remove(item);
      lvGroups.Items.Insert(index + 1, item);
      item.Selected = true;
      UpdateGroupControls();
    }
    #endregion

    #region "Layout" page
    private void InitSampleReports()
    {
      FSampleReportTabular = new Report();
      FSampleReportTabular.Load(ResourceLoader.GetStream("samplereporttabular.frx"));
      FSampleReportColumnar = new Report();
      FSampleReportColumnar.Load(ResourceLoader.GetStream("samplereportcolumnar.frx"));
      UpdateReportSample();
    }

    private void UpdateReportSample()
    {
      pnPreview.Report = rbTabular.Checked ? FSampleReportTabular : FSampleReportColumnar;
    }
    
    private void rbTabular_CheckedChanged(object sender, EventArgs e)
    {
      UpdateReportSample();
    }
    #endregion

    #region "Style" page
    private void InitStyles()
    {
      FStyleSheet = new StyleSheet();
      FStyleSheet.Load(ResourceLoader.GetStream("reportstyles.frss"));
      FSampleStyleReport = new Report();
      FSampleStyleReport.Load(ResourceLoader.GetStream("samplestylereport.frx"));
      
      // localize stylesheet names
      MyRes res = new MyRes("Forms,StandardReportWizard,Styles");
      foreach (StyleCollection s in FStyleSheet)
      {
        s.Name = res.Get(s.Name);
      }
      
      lbStyles.Items.AddRange(FStyleSheet.ToArray());
      if (lbStyles.Items.Count > 0)
        lbStyles.SelectedIndex = 0;
    }
    
    private void UpdateStyleSample()
    {
      if (lbStyles.SelectedIndex != -1)
      {
        StyleCollection style = FStyleSheet[FStyleSheet.IndexOf((string)lbStyles.SelectedItem)];
        FSampleStyleReport.Styles = style;
        FSampleStyleReport.ApplyStyles();
      }
      pnStylePreview.FullPagePreview = true;
      pnStylePreview.Report = FSampleStyleReport;
    }

    private void lbStyles_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateStyleSample();
    }
    #endregion
    
    #region Create the report
    private void btnFinish_Click(object sender, EventArgs e)
    {
      CreateReport();
    }

    private void CreateReport()
    {
      // disable all datasources, enable selected only
      foreach (Base c in Report.Dictionary.AllObjects)
      {
        if (c is DataSourceBase && c != DataSource)
          (c as DataSourceBase).Enabled = false;
      }
      
      // create page layout
      // by default, empty report contains the following bands: ReportTitle, PageHeader, Data, PageFooter
      ReportPage page = Report.Pages[0] as ReportPage;
      page.Landscape = rbLandscape.Checked;
      float pageWidth = (page.PaperWidth - page.LeftMargin - page.RightMargin) * Units.Millimeters;
      float snapSize = ReportWorkspace.Grid.SnapSize;
      float defaultHeight = page.IsImperialUnitsUsed ? Units.Inches * 0.2f : Units.Millimeters * 5;

      // styles
      MyRes res = new MyRes("Forms,StandardReportWizard,Styles");
      if (lbStyles.SelectedIndex != -1)
      {
        StyleCollection style = FStyleSheet[FStyleSheet.IndexOf((string)lbStyles.SelectedItem)];
        FStyleSheet.Remove(style);
        FSampleStyleReport.Styles = new StyleCollection();
        Report.Styles = style;
        
        // localize style names, fix fonts
        foreach (Style s in style)
        {
          s.Name = res.Get(s.Name);
          s.Font = new Font(DrawUtils.DefaultReportFont.Name, s.Font.Size, s.Font.Style);
        }
      }
      
      // title
      TextObject title = new TextObject();
      title.Parent = page.ReportTitle;
      title.CreateUniqueName();
      title.Dock = DockStyle.Fill;
      title.HorzAlign = HorzAlign.Center;
      title.VertAlign = VertAlign.Center;
      title.Text = DataSource.Alias;
      title.Style = res.Get("Title");
      
      // data and header
      List<Column> selectedColumns = SelectedColumns;
      DataBand dataBand = page.Bands[0] as DataBand;
      dataBand.DataSource = DataSource;

      if (rbTabular.Checked)
      {
        float[] columnWidths = new float[selectedColumns.Count];
        float columnWidth = pageWidth / selectedColumns.Count;
        
        // try fit to grid
        columnWidth = (int)(columnWidth / snapSize) * snapSize;
        for (int i = 0; i < selectedColumns.Count; i++)
        {
          columnWidths[i] = columnWidth;
        }
        
        // compensate column widths to fit pagewidth
        float extraWidth = pageWidth - columnWidth * selectedColumns.Count;
        for (int i = 0; i < selectedColumns.Count; i++)
        {
          if (extraWidth - snapSize < 0)
            break;
          columnWidths[i] += snapSize;
          extraWidth -= snapSize;
        }
        
        // create data and header
        float offsetX = 0;
        for (int i = 0; i < selectedColumns.Count; i++)
        {
          TextObject dataColumn = new TextObject();
          dataColumn.Parent = dataBand;
          dataColumn.CreateUniqueName();
          dataColumn.Bounds = new RectangleF(offsetX, 0, columnWidths[i], defaultHeight);
          dataColumn.Text = "[" + DataSource.Alias + "." + selectedColumns[i].Alias + "]";
          dataColumn.Style = res.Get("Data");

          TextObject headerColumn = new TextObject();
          headerColumn.Parent = page.PageHeader;
          headerColumn.CreateUniqueName();
          headerColumn.Bounds = new RectangleF(offsetX, 0, columnWidths[i], defaultHeight);
          headerColumn.Text = selectedColumns[i].Alias;
          headerColumn.Style = res.Get("Header");

          offsetX += columnWidths[i];
        }
        
        dataBand.Height = defaultHeight;
        dataBand.EvenStyle = res.Get("EvenRows");
        page.PageHeader.Height = defaultHeight + snapSize;
      }
      else
      {
        page.PageHeader = null;

        // calculate max header width
        float headerWidth = 0;
        using (TextObject tempHeader = new TextObject())
        {
          tempHeader.Parent = dataBand;
          tempHeader.Style = res.Get("Header");
          
          for (int i = 0; i < selectedColumns.Count; i++)
          {
            tempHeader.Text = selectedColumns[i].Alias;
            float width = (int)(tempHeader.CalcWidth() / snapSize + 1) * snapSize;
            if (width > headerWidth)
              headerWidth = width;
          }
        }
        
        // create data and header
        float dataWidth = (int)((pageWidth - headerWidth) / snapSize) * snapSize;
        float offsetY = 0;
        for (int i = 0; i < selectedColumns.Count; i++)
        {
          TextObject headerColumn = new TextObject();
          headerColumn.Parent = dataBand;
          headerColumn.CreateUniqueName();
          headerColumn.Bounds = new RectangleF(0, offsetY, headerWidth, defaultHeight);
          headerColumn.Text = selectedColumns[i].Alias;
          headerColumn.Style = res.Get("Header");

          TextObject dataColumn = new TextObject();
          dataColumn.Parent = dataBand;
          dataColumn.CreateUniqueName();
          dataColumn.Bounds = new RectangleF(headerWidth, offsetY, dataWidth, defaultHeight);
          dataColumn.Text = "[" + DataSource.Alias + "." + selectedColumns[i].Alias + "]";
          dataColumn.Style = res.Get("Data");

          offsetY += defaultHeight;
        }

        dataBand.Height = offsetY + snapSize;
      }
      
      // groups
      if (Groups.Count > 0)
      {
        // create group headers
        Base parent = page;
        foreach (Column column in Groups)
        {
          GroupHeaderBand groupHeader = new GroupHeaderBand();
          groupHeader.Parent = parent;
          groupHeader.CreateUniqueName();
          groupHeader.Height = defaultHeight;
          groupHeader.Condition = "[" + DataSource.Alias + "." + column.Alias + "]";
          
          groupHeader.GroupFooter = new GroupFooterBand();
          groupHeader.GroupFooter.CreateUniqueName();
          groupHeader.GroupFooter.Height = defaultHeight;
          
          TextObject groupText = new TextObject();
          groupText.Parent = groupHeader;
          groupText.CreateUniqueName();
          groupText.Dock = DockStyle.Fill;
          groupText.Text = groupHeader.Condition;
          groupText.Style = res.Get("Group");
          
          parent = groupHeader;
        }
        
        // connect last header to the data
        (parent as GroupHeaderBand).Data = dataBand;
      }

      // page footer
      TextObject pageN = new TextObject();
      pageN.Parent = page.PageFooter;
      pageN.CreateUniqueName();
      pageN.Dock = DockStyle.Fill;
      pageN.HorzAlign = HorzAlign.Right;
      pageN.Text = "[PageN]";
      pageN.Style = res.Get("Footer");
      page.PageFooter.Height = defaultHeight;

      // tell the designer to reflect changes
      Report.Designer.SetModified(null, "ChangeReport");
    }
    #endregion

    private void StandardReportWizardForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }

    public override void InitWizard(Report report)
    {
      base.InitWizard(report);
      lvAvailableColumns1.SmallImageList = Res.GetImages();
      lvGroups.SmallImageList = Res.GetImages();
      
      InitSampleReports();
      InitStyles();
    }

    private void Done()
    {
      FSampleReportColumnar.Dispose();
      FSampleReportTabular.Dispose();
      FSampleStyleReport.Dispose();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,StandardReportWizard");
      Text = res.Get("");
      
      panGroups.Text = res.Get("GroupsPage");
      lblCreateGroups.Text = res.Get("CreateGroups");
      lblAvailableColumns1.Text = Res.Get("Forms,BaseReportWizard,AvailableColumns");
      lblGroups.Text = res.Get("Groups");
      panLayout.Text = res.Get("LayoutPage");
      lblLayout.Text = res.Get("DefineLayout");
      gbOrientation.Text = res.Get("Orientation");
      rbPortrait.Text = res.Get("Portrait");
      rbLandscape.Text = res.Get("Landscape");
      gbLayout.Text = res.Get("Layout");
      rbTabular.Text = res.Get("Tabular");
      rbColumnar.Text = res.Get("Columnar");
      panStyle.Text = res.Get("StylePage");
      lblStyles.Text = res.Get("Styles");

      picIcon.Image = ResourceLoader.GetBitmap("reportwizard.png");
      btnGroupUp.Image = Res.GetImage(208);
      btnGroupDown.Image = Res.GetImage(209);
    }

    public StandardReportWizardForm()
    {
      InitializeComponent();
      Localize();
    }
  }
}

