using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastReport.Forms;
using FastReport.Utils;
using FastReport.Controls;

namespace FastReport.MSChart
{
  internal partial class GalleryForm : BaseDialogForm
  {
    private static int FLastCategoryIndex;
    private SeriesChartType FSelectedSeriesType;
    
    public SeriesChartType SelectedSeriesType
    {
      get { return FSelectedSeriesType; }
    }
    
    public bool NewArea
    {
      get { return cbNewArea.Checked; }
    }

    private void GalleryForm_Shown(object sender, EventArgs e)
    {
      pcPages.ActivePageIndex = FLastCategoryIndex;
    }

    private void PopulateCategories()
    {
      foreach (GalleryCategory category in ChartGallery.Categories)
      {
        PageControlPage page = new PageControlPage();
        page.Text = Res.Get("Forms,ChartGallery,Categories," + category.Name).Replace("&", "&&");
        page.Parent = pcPages;
        page.Dock = DockStyle.Fill;
        page.BackColor = SystemColors.Window;
        PopulateCategory(category, page);
      }
    }

    private void PopulateCategory(GalleryCategory category, PageControlPage page)
    {
      ListView listView = new ListView();
      listView.Parent = page;
      listView.Location = new Point(16, 32);
      listView.Size = new Size(page.Width - 16, page.Height - 32);
      listView.LargeImageList = ChartGallery.Images;
      listView.BorderStyle = BorderStyle.None;
      listView.SelectedIndexChanged += new EventHandler(listView_SelectedIndexChanged);
      listView.DoubleClick += new EventHandler(listView_DoubleClick);
      
      foreach (GalleryItem item in category.Items)
      {
        ListViewItem lvItem = listView.Items.Add(Res.Get("Forms,ChartGallery,Series," + item.Name));
        lvItem.ImageIndex = item.ImageIndex;
        lvItem.Tag = item.SeriesType;
      }
    }

    private void listView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ((sender as ListView).SelectedItems.Count > 0)
        FSelectedSeriesType = (SeriesChartType)(sender as ListView).SelectedItems[0].Tag;
      btnOk.Enabled = (sender as ListView).SelectedItems.Count > 0;
    }

    private void listView_DoubleClick(object sender, EventArgs e)
    {
      if (btnOk.Enabled)
        DialogResult = DialogResult.OK;
    }
    
    private void GalleryForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      FLastCategoryIndex = pcPages.ActivePageIndex;
    }

    public override void Localize()
    {
      base.Localize();
      Text = Res.Get("Forms,ChartGallery");
      cbNewArea.Text = Res.Get("Forms,ChartGallery,NewArea");
    }
    
    public GalleryForm()
    {
      InitializeComponent();
      PopulateCategories();
      Localize();
    }
  }
  
  internal static class ChartGallery
  {
    public static List<GalleryCategory> Categories;
    public static ImageList Images;
    public static ImageList SmallImages;
    
    public static GalleryItem FindItem(SeriesChartType seriesType)
    {
      foreach (GalleryCategory category in Categories)
      {
        foreach (GalleryItem item in category.Items)
        {
          if (item.SeriesType == seriesType)
            return item;
        }
      }
      
      return null;
    }
    
    static ChartGallery()
    {
      Categories = new List<GalleryCategory>();
      Images = new ImageList();
      Images.ImageSize = new Size(96, 72);
      Images.ColorDepth = ColorDepth.Depth32Bit;

      SmallImages = new ImageList();
      SmallImages.ImageSize = new Size(48, 36);
      SmallImages.ColorDepth = ColorDepth.Depth32Bit;

      GalleryCategory category = new GalleryCategory("BarColumn");
      category.Items.Add(new GalleryItem(SeriesChartType.Bar, "Bar"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedBar, "StackedBar"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedBar100, "StackedBar100"));
      category.Items.Add(new GalleryItem(SeriesChartType.Column, "Column"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedColumn, "StackedColumn"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedColumn100, "StackedColumn100"));
      Categories.Add(category);

      category = new GalleryCategory("Area");
      category.Items.Add(new GalleryItem(SeriesChartType.Area, "Area"));
      category.Items.Add(new GalleryItem(SeriesChartType.SplineArea, "SplineArea"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedArea, "StackedArea"));
      category.Items.Add(new GalleryItem(SeriesChartType.StackedArea100, "StackedArea100"));
      Categories.Add(category);

      category = new GalleryCategory("Line");
      category.Items.Add(new GalleryItem(SeriesChartType.Line, "Line"));
      category.Items.Add(new GalleryItem(SeriesChartType.FastLine, "FastLine"));
      category.Items.Add(new GalleryItem(SeriesChartType.StepLine, "StepLine"));
      category.Items.Add(new GalleryItem(SeriesChartType.Spline, "Spline"));
      Categories.Add(category);

      category = new GalleryCategory("PointBubble");
      category.Items.Add(new GalleryItem(SeriesChartType.Bubble, "Bubble"));
      category.Items.Add(new GalleryItem(SeriesChartType.Point, "Point"));
      category.Items.Add(new GalleryItem(SeriesChartType.FastPoint, "FastPoint"));
      Categories.Add(category);

      category = new GalleryCategory("Pie");
      category.Items.Add(new GalleryItem(SeriesChartType.Pie, "Pie"));
      category.Items.Add(new GalleryItem(SeriesChartType.Doughnut, "Doughnut"));
      Categories.Add(category);

      category = new GalleryCategory("Circular");
      category.Items.Add(new GalleryItem(SeriesChartType.Polar, "Polar"));
      category.Items.Add(new GalleryItem(SeriesChartType.Radar, "Radar"));
      Categories.Add(category);

      category = new GalleryCategory("Financial");
      category.Items.Add(new GalleryItem(SeriesChartType.Stock, "Stock"));
      category.Items.Add(new GalleryItem(SeriesChartType.Candlestick, "Candlestick"));
      category.Items.Add(new GalleryItem(SeriesChartType.Kagi, "Kagi"));
      category.Items.Add(new GalleryItem(SeriesChartType.Renko, "Renko"));
      category.Items.Add(new GalleryItem(SeriesChartType.PointAndFigure, "PointAndFigure"));
      category.Items.Add(new GalleryItem(SeriesChartType.ThreeLineBreak, "ThreeLineBreak"));
      Categories.Add(category);

      category = new GalleryCategory("PyramidFunnel");
      category.Items.Add(new GalleryItem(SeriesChartType.Pyramid, "Pyramid"));
      category.Items.Add(new GalleryItem(SeriesChartType.Funnel, "Funnel"));
      Categories.Add(category);

      category = new GalleryCategory("Range");
      category.Items.Add(new GalleryItem(SeriesChartType.Range, "Range"));
      category.Items.Add(new GalleryItem(SeriesChartType.SplineRange, "SplineRange"));
      category.Items.Add(new GalleryItem(SeriesChartType.RangeColumn, "RangeColumn"));
      category.Items.Add(new GalleryItem(SeriesChartType.RangeBar, "RangeBar"));
      Categories.Add(category);
    }
  }
  
  internal class GalleryCategory
  {
    public string Name;
    public List<GalleryItem> Items;
    
    public GalleryCategory(string name)
    {
      Name = name;
      Items = new List<GalleryItem>();
    }
  }
  
  internal class GalleryItem
  {
    public SeriesChartType SeriesType;
    public string Name;
    public Bitmap Image;
    public int ImageIndex;
    
    public GalleryItem(SeriesChartType seriesType, string name)
    {
      SeriesType = seriesType;
      Name = name;
      Image = ResourceLoader.GetBitmap("MSChart." + name + ".png");
      ChartGallery.Images.Images.Add(Image);
      ChartGallery.SmallImages.Images.Add(Image.GetThumbnailImage(48, 36, null, IntPtr.Zero));
      ImageIndex = ChartGallery.Images.Images.Count - 1;
    }
  }
}

