using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Utils;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Reflection;

namespace FastReport.MSChart
{
  internal partial class MSChartObjectEditorForm : BaseDialogForm
  {
    private MSChartObject FOriginalChartObject;
    private MSChartObject FChartObject;
    private ChartEditorControl FChartEditor;
    private SeriesEditorControl FSeriesEditor;
    
    public MSChartObject ChartObject
    {
      get { return FChartObject; }
      set
      {
        FOriginalChartObject = value;
        FChartObject = new MSChartObject();
        FChartObject.AssignAll(FOriginalChartObject);
        for (int i = 0; i < FOriginalChartObject.Series.Count; i++)
        {
          FChartObject.Series[i].Name = FOriginalChartObject.Series[i].Name;
        }
        FChartObject.SetReport(FOriginalChartObject.Report);
        PopulateSeriesTree(value);
      }
    }

    private void PopulateSeriesTree(object select)
    {
      tvChart.Nodes.Clear();
      
      TreeNode chartNode = tvChart.Nodes.Add(Res.Get("Forms,ChartEditor,Chart"));
      chartNode.Tag = ChartObject;
      
      foreach (MSChartSeries series in ChartObject.Series)
      {
        GalleryItem item = ChartGallery.FindItem(series.SeriesSettings.ChartType);
        TreeNode seriesNode = chartNode.Nodes.Add(series.SeriesSettings.Name + " (" + 
          Res.Get("Forms,ChartGallery,Series," + item.Name) + ")");
        seriesNode.Tag = series;
        seriesNode.ImageIndex = item.ImageIndex;
        seriesNode.SelectedImageIndex = seriesNode.ImageIndex;
        if (series == select)
          tvChart.SelectedNode = seriesNode;
      }
      
      chartNode.Expand();
      if (ChartObject == select)
        tvChart.SelectedNode = chartNode;
    }

    private void Init()
    {
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
      tvChart.ImageList = ChartGallery.SmallImages;
      pnSample.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(
        pnSample, new object[] { ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true });
    }

    private void tvChart_AfterSelect(object sender, TreeViewEventArgs e)
    {
      object selected = tvChart.SelectedNode.Tag;
      btnDelete.Enabled = selected is MSChartSeries;
      btnUp.Enabled = selected is MSChartSeries && tvChart.SelectedNode.Index > 0;
      btnDown.Enabled = selected is MSChartSeries && tvChart.SelectedNode.Index < tvChart.SelectedNode.Parent.Nodes.Count - 1;
      ShowPropertyPages(selected);
    }

    private void ShowPropertyPages(object selected)
    {
      if (selected is MSChartObject)
      {
        if (FChartEditor == null)
        {
          FChartEditor = new ChartEditorControl();
          FChartEditor.Location = new Point(pcSeries.Right + pcSeries.Left, pcSeries.Top);
          FChartEditor.Parent = this;
          FChartEditor.Chart = ChartObject;
          FChartEditor.Changed += new EventHandler(RefreshSample);
        }

        if (FSeriesEditor != null)
          FSeriesEditor.Hide();
        FChartEditor.UpdateChartAreas();
        FChartEditor.Show();
      }
      else
      {
        if (FSeriesEditor == null)
        {
          FSeriesEditor = new SeriesEditorControl();
          FSeriesEditor.Location = new Point(pcSeries.Right + pcSeries.Left, pcSeries.Top);
          FSeriesEditor.Parent = this;
          FSeriesEditor.Changed += new EventHandler(RefreshSample);
        }
        
        if (FChartEditor != null)
          FChartEditor.Hide();
        FSeriesEditor.Series = selected as MSChartSeries;
        FSeriesEditor.Show();
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      using (GalleryForm form = new GalleryForm())
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          ChartArea area = ChartObject.Chart.ChartAreas[0];
          bool newChartAreaNeeded = form.SelectedSeriesType == SeriesChartType.Pie ||
            form.SelectedSeriesType == SeriesChartType.Doughnut || 
            form.SelectedSeriesType == SeriesChartType.Funnel ||
            form.SelectedSeriesType == SeriesChartType.Pyramid || 
            form.SelectedSeriesType == SeriesChartType.Kagi ||
            form.SelectedSeriesType == SeriesChartType.Renko ||
            form.SelectedSeriesType == SeriesChartType.PointAndFigure ||
            form.SelectedSeriesType == SeriesChartType.ThreeLineBreak;
          if (form.NewArea || newChartAreaNeeded)
          {
            if (ChartObject.Series.Count > 0)
            {
              area = new ChartArea();
              ChartObject.Chart.ChartAreas.Add(area);
            }
          }
          
          MSChartSeries series = ChartObject.AddSeries(form.SelectedSeriesType);
          series.SeriesSettings.ChartArea = area.Name;
          series.CreateDummyData();
          PopulateSeriesTree(series);
          RefreshSample();
        }
      }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      MSChartSeries series = tvChart.SelectedNode.Tag as MSChartSeries;
      string areaName = series.SeriesSettings.ChartArea;
      if (areaName != "Default")
      {
        // delete non-default area exclusively owned by deleted series
        ChartArea area = ChartObject.Chart.ChartAreas.FindByName(areaName);
        ChartObject.Chart.ChartAreas.Remove(area);
        area.Dispose();
      }
      else
      {
        // delete this area and make the next area default one.
        if (ChartObject.Chart.ChartAreas.Count > 1)
        {
          ChartArea area = ChartObject.Chart.ChartAreas.FindByName(areaName);
          ChartObject.Chart.ChartAreas.Remove(area);
          area.Dispose();
          area = ChartObject.Chart.ChartAreas[0];
          area.Name = "Default";
        }
      }

      ChartObject.DeleteSeries(ChartObject.Series.IndexOf(series));
      PopulateSeriesTree(ChartObject);
      RefreshSample();
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      MSChartSeries series = tvChart.SelectedNode.Tag as MSChartSeries;
      Series chartSeries = series.SeriesSettings;
      
      int index = ChartObject.Series.IndexOf(series);
      ChartObject.Series.RemoveAt(index);
      ChartObject.Chart.Series.RemoveAt(index);
      ChartObject.Series.Insert(index - 1, series);
      ChartObject.Chart.Series.Insert(index - 1, chartSeries);

      PopulateSeriesTree(series);
      RefreshSample();
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      MSChartSeries series = tvChart.SelectedNode.Tag as MSChartSeries;
      Series chartSeries = series.SeriesSettings;

      int index = ChartObject.Series.IndexOf(series);
      ChartObject.Series.RemoveAt(index);
      ChartObject.Chart.Series.RemoveAt(index);
      ChartObject.Series.Insert(index + 1, series);
      ChartObject.Chart.Series.Insert(index + 1, chartSeries);

      PopulateSeriesTree(series);
      RefreshSample();
    }

    private void RefreshSample(object sender, EventArgs e)
    {
      RefreshSample();
    }

    private void RefreshSample()
    {
      pnSample.Refresh();
    }
    
    private void MSChartObjectEditorForm_Shown(object sender, EventArgs e)
    {
      tvChart.Focus();
    }

    private void pnSample_Paint(object sender, PaintEventArgs e)
    {
      try
      {
        ChartObject.Chart.Printing.PrintPaint(e.Graphics, pnSample.DisplayRectangle);
      }
      catch (Exception ex)
      {
        e.Graphics.ResetClip();
        using (StringFormat sf = new StringFormat())
        {
          sf.Alignment = StringAlignment.Center;
          sf.LineAlignment = StringAlignment.Center;
          e.Graphics.DrawString(ex.Message, Font, Brushes.Red, pnSample.DisplayRectangle, sf);
        }
      }  
    }

    private void pnSample_MouseDown(object sender, MouseEventArgs e)
    {
      Chart chart = ChartObject.Chart;
      chart.Size = pnSample.Size;
      HitTestResult hitTest = chart.HitTest(e.X, e.Y);
      switch (hitTest.ChartElementType)
      {
        case ChartElementType.Axis:
        case ChartElementType.AxisLabelImage:
        case ChartElementType.AxisLabels:
        case ChartElementType.AxisTitle:
        case ChartElementType.Gridlines:
        case ChartElementType.StripLines:
        case ChartElementType.TickMarks:
          FChartEditor.ActivePageIndex = 4;
          tvChart.SelectedNode = tvChart.Nodes[0];
          break;
        
        case ChartElementType.DataPoint:
        case ChartElementType.DataPointLabel:
          foreach (TreeNode node in tvChart.Nodes[0].Nodes)
          {
            if ((node.Tag as MSChartSeries).SeriesSettings == hitTest.Series)
            {
              tvChart.SelectedNode = node;
              break;
            }
          }
          break;
        
        case ChartElementType.LegendArea:
        case ChartElementType.LegendHeader:
        case ChartElementType.LegendItem:
        case ChartElementType.LegendTitle:
          FChartEditor.ActivePageIndex = 5;
          tvChart.SelectedNode = tvChart.Nodes[0];
          break;
        
        case ChartElementType.PlottingArea:
          FChartEditor.ActivePageIndex = 2;
          tvChart.SelectedNode = tvChart.Nodes[0];
          break;
        
        case ChartElementType.Title:
          FChartEditor.ActivePageIndex = 6;
          tvChart.SelectedNode = tvChart.Nodes[0];
          break;
      }
    }

    private void MSChartObjectEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
      {
        FOriginalChartObject.AssignAll(ChartObject);
        for (int i = 0; i < FOriginalChartObject.Series.Count; i++)
        {
          FOriginalChartObject.Series[i].SetName(ChartObject.Series[i].Name);
        }
      }
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,ChartEditor");
      Text = res.Get("");
      lblHint.Text = res.Get("Hint");
      btnAdd.Text = res.Get("Add");
      btnDelete.Text = res.Get("Delete");
    }
    
    public MSChartObjectEditorForm()
    {
      InitializeComponent();
      Init();
      Localize();
    }
  }
}

