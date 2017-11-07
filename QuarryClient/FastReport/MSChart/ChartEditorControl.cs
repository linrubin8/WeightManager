using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FastReport.Controls;
using FastReport.Utils;
using FastReport.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace FastReport.MSChart
{
  internal partial class ChartEditorControl : UserControl
  {
    private MSChartObject FChart;
    private bool FUpdating;

    public event EventHandler Changed;

    public MSChartObject Chart
    {
      get { return FChart; }
      set 
      {
        FChart = value;
        UpdateControls();
      }
    }
    
    public int ActivePageIndex
    {
      get { return pageControl1.ActivePageIndex; }
      set { pageControl1.ActivePageIndex = value; }
    }
    
    private Chart MSChart
    {
      get { return FChart.Chart; }
    }
    
    private Report Report
    {
      get { return FChart.Report; }
    }
    
    private ChartArea ChartArea
    {
      get { return MSChart.ChartAreas.FindByName(cbxChartArea.SelectedItem.ToString()); }
    }
    
    private Axis Axis
    {
      get { return cbxAxis.Items.Count == 0 ? null : ChartArea.Axes[cbxAxis.SelectedIndex]; }
    }
    
    private Grid Grid
    {
      get
      {
        if (cbxAxisGrid.SelectedIndex == 0)
          return Axis.MajorGrid;
        return Axis.MinorGrid;
      }
    }

    private TickMark Tick
    {
      get
      {
        if (cbxAxisTick.SelectedIndex == 0)
          return Axis.MajorTickMark;
        return Axis.MinorTickMark;
      }
    }
    
    private StripLine Strip
    {
      get
      {
        if (lbAxisStrips.SelectedIndex != -1)
          return Axis.StripLines[lbAxisStrips.SelectedIndex];
        return null;
      }
    }
    
    private CustomLabel CustomLabel
    {
      get 
      {
        if (lbCustomLabels.SelectedIndex != -1)
          return Axis.CustomLabels[lbCustomLabels.SelectedIndex];
        return null;
      }
    }
    
    private Legend Legend
    {
      get
      {
        return MSChart.Legends.Count > 0 ? MSChart.Legends[0] : null;
      }
    }

    private Title Title
    {
      get
      {
        return MSChart.Titles.Count > 0 ? MSChart.Titles[0] : null;
      }
    }

    private void UpdateControls()
    {
      FUpdating = true;
      
      #region Data tab
      // data source
      cbxDataSource.Items.Clear();
      cbxDataSource.Items.Add(Res.Get("Misc,None"));
      cbxDataSource.SelectedIndex = 0;

      foreach (Base c in Report.Dictionary.AllObjects)
      {
        DataSourceBase ds = c as DataSourceBase;
        if (ds != null && ds.Enabled)
        {
          cbxDataSource.Items.Add(ds.Alias);
          if (Chart.DataSource == ds)
            cbxDataSource.SelectedIndex = cbxDataSource.Items.Count - 1;
        }  
      }
      
      // filter
      tbFilter.Text = Chart.Filter;
      
      // autoseries
      cbxAutoSeriesData.Report = Report;
      cbxAutoSeriesData.Text = Chart.AutoSeriesColumn;
      cbxAutoSeriesColor.Report = Report;
      cbxAutoSeriesColor.Text = Chart.AutoSeriesColor;
      cbxAutoSeriesSort.SelectedIndex = (int)Chart.AutoSeriesSortOrder;
      
      // align X values
      cbAlignXValues.Checked = Chart.AlignXValues;
      cbAutoSeriesForce.Checked = Chart.AutoSeriesForce;
      #endregion

      #region Appearance tab
      // back color
      cbxBackColor.Color = MSChart.BackColor;
      cbxSecondaryColor.Color = MSChart.BackSecondaryColor;
      cbxGradient.SelectedIndex = (int)MSChart.BackGradientStyle;
      cbxHatchStyle.SelectedIndex = (int)MSChart.BackHatchStyle;

      // border
      cbxBorderSkin.SelectedIndex = (int)MSChart.BorderSkin.SkinStyle;
      cbxBorderColor.Color = MSChart.BorderlineColor;
      cbxBorderStyle.SelectedIndex = (int)MSChart.BorderlineDashStyle;
      udBorderWidth.Value = MSChart.BorderlineWidth;
      
      // palette
      cbxSeriesPalette.SelectedIndex = (int)MSChart.Palette;
      #endregion

      #region Legend tab
      if (Legend == null)
        pgLegend.Enabled = false;
      else
      {
        pgLegend.Enabled = true;
        
        cbLegendEnabled.Checked = Legend.Enabled;
        cbxLegendStyle.SelectedIndex = (int)Legend.LegendStyle;
        UpdateLegendDockButtons();
        cbLegendInsideChartArea.Checked = Legend.IsDockedInsideChartArea && 
          Legend.DockedToChartArea == "Default";
          
        cbxLegendBackColor.Color = Legend.BackColor;
        cbxLegendSecondaryColor.Color = Legend.BackSecondaryColor;
        cbxLegendGradient.SelectedIndex = (int)Legend.BackGradientStyle;
        cbxLegendHatchStyle.SelectedIndex = (int)Legend.BackHatchStyle;
        cbxLegendBorderColor.Color = Legend.BorderColor;
        cbxLegendBorderStyle.SelectedIndex = (int)Legend.BorderDashStyle;
        udLegendBorderWidth.Value = Legend.BorderWidth;
        cbxLegendShadowColor.Color = Legend.ShadowColor;
        udLegendShadowOffset.Value = Legend.ShadowOffset;
        tbLegendFont.Text = Converter.ToString(Legend.Font);
        cbxLegendForeColor.Color = Legend.ForeColor;  
      }  
      #endregion

      #region Title tab
      if (Title == null)
        pgTitle.Enabled = false;
      else
      {
        pgTitle.Enabled = true;

        cbTitleVisible.Checked = Title.Visible;
        tbTitleText.Text = Title.Text;
        cbxTitleTextOrientation.SelectedIndex = (int)Title.TextOrientation;
        cbxTitleTextStyle.SelectedIndex = (int)Title.TextStyle;
        UpdateTitleDockButtons();
        cbTitleInsideChartArea.Checked = Title.IsDockedInsideChartArea &&
          Title.DockedToChartArea == "Default";

        cbxTitleBackColor.Color = Title.BackColor;
        cbxTitleSecondaryColor.Color = Title.BackSecondaryColor;
        cbxTitleGradient.SelectedIndex = (int)Title.BackGradientStyle;
        cbxTitleHatchStyle.SelectedIndex = (int)Title.BackHatchStyle;
        cbxTitleBorderColor.Color = Title.BorderColor;
        cbxTitleBorderStyle.SelectedIndex = (int)Title.BorderDashStyle;
        udTitleBorderWidth.Value = Title.BorderWidth;
        cbxTitleShadowColor.Color = Title.ShadowColor;
        udTitleShadowOffset.Value = Title.ShadowOffset;
        tbTitleFont.Text = Converter.ToString(Title.Font);
        cbxTitleForeColor.Color = Title.ForeColor;
      }
      #endregion

      FUpdating = false;
    }
    
    private void UpdateLegendDockButtons()
    {
      Button[] dockButtons = new Button[] {
          btnL1, btnL2, btnL3, btnL4, btnL5, btnL6, btnL7, btnL8, btnL9, btnL10, btnL11, btnL12 };
      int activeBtnTag = (int)Legend.Docking * 3 + (int)Legend.Alignment;
      foreach (Button btn in dockButtons)
      {
        btn.BackColor = int.Parse(btn.Tag.ToString()) == activeBtnTag ? 
          Color.Orange : SystemColors.ButtonFace;
      }
    }

    private void UpdateTitleDockButtons()
    {
      Button[] dockButtons = new Button[] {
          btnT1, btnT2, btnT3, btnT4, btnT5, btnT6, btnT7, btnT8, btnT9, btnT10, btnT11, btnT12 };

      int titleAlign = 0;
      switch (Title.Alignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          titleAlign = 0;
          break;

        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          titleAlign = 1;
          break;

        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          titleAlign = 2;
          break;    
      }
      
      int activeBtnTag = (int)Title.Docking * 3 + titleAlign;
      foreach (Button btn in dockButtons)
      {
        btn.BackColor = int.Parse(btn.Tag.ToString()) == activeBtnTag ?
          Color.Orange : SystemColors.ButtonFace;
      }
    }

    private void UpdateChartAreaControls()
    {
      FUpdating = true;

      // appearance
      cbxAreaBackColor.Color = ChartArea.BackColor;
      cbxAreaSecondaryColor.Color = ChartArea.BackSecondaryColor;
      cbxAreaGradient.SelectedIndex = (int)ChartArea.BackGradientStyle;
      cbxAreaHatchStyle.SelectedIndex = (int)ChartArea.BackHatchStyle;
      cbxAreaBorderColor.Color = ChartArea.BorderColor;
      cbxAreaBorderStyle.SelectedIndex = (int)ChartArea.BorderDashStyle;
      udAreaBorderWidth.Value = ChartArea.BorderWidth;
      cbxAreaShadowColor.Color = ChartArea.ShadowColor;
      udAreaShadowOffset.Value = ChartArea.ShadowOffset;

      // 3d
      cb3DEnable.Checked = ChartArea.Area3DStyle.Enable3D;
      trb3DXRotation.Value = ChartArea.Area3DStyle.Inclination;
      ud3DXRotation.Value = ChartArea.Area3DStyle.Inclination;
      trb3DYRotation.Value = ChartArea.Area3DStyle.Rotation;
      ud3DYRotation.Value = ChartArea.Area3DStyle.Rotation;
      trb3DPerspective.Value = ChartArea.Area3DStyle.Perspective;
      ud3DPerspective.Value = ChartArea.Area3DStyle.Perspective;
      ud3DWallWidth.Value = ChartArea.Area3DStyle.WallWidth;
      ud3DAxisDepth.Value = ChartArea.Area3DStyle.PointDepth;
      cbx3DLightStyle.SelectedIndex = (int)ChartArea.Area3DStyle.LightStyle;
      cb3DClustered.Checked = ChartArea.Area3DStyle.IsClustered;

      FUpdating = false;
      UpdateAxes();
    }

    private void UpdateAxisControls()
    {
      FUpdating = true;
      
      #region General tab
      cbAxisEnabled.Checked = Axis.Enabled != AxisEnabled.False;
      cbAxisMargin.Checked = Axis.IsMarginVisible;
      cbAxisLogarithmic.Checked = Axis.IsLogarithmic;
      cbAxisReversed.Checked = Axis.IsReversed;
      
      udAxisInterval.Value = (decimal)Axis.Interval;
      cbxAxisIntervalType.SelectedIndex = (int)Axis.IntervalType;
      
      cbxAxisLineColor.Color = Axis.LineColor;
      cbxAxisLineStyle.SelectedIndex = (int)Axis.LineDashStyle;
      udAxisLineWidth.Value = Axis.LineWidth;
      
      cbAxisInterlaced.Checked = Axis.IsInterlaced;
      cbxAxisInterlacedColor.Color = Axis.InterlacedColor;
      #endregion
      
      #region Labels tab
      cbAxisLabelEnabled.Checked = Axis.LabelStyle.Enabled;
      tbAxisLabelFormat.Text = Axis.LabelStyle.Format;
      tbAxisLabelFont.Text = Converter.ToString(Axis.LabelStyle.Font);
      cbxAxisLabelForeColor.Color = Axis.LabelStyle.ForeColor;
      udAxisLabelAngle.Value = Axis.LabelStyle.Angle;
      
      tbAxisTitleText.Text = Axis.Title;
      tbAxisTitleFont.Text = Converter.ToString(Axis.TitleFont);
      cbxAxisTitleForeColor.Color = Axis.TitleForeColor;
      #endregion

      #region Grid tab
      cbxAxisGrid.SelectedIndex = 0;
      #endregion

      #region Ticks tab
      cbxAxisTick.SelectedIndex = 0;
      #endregion
      
      #region Custom labels tab
      lbCustomLabels.Items.Clear();
      foreach (CustomLabel label in Axis.CustomLabels)
      {
        lbCustomLabels.Items.Add(Res.Get("Forms,ChartEditor,ChartEditorControl,Axes,CustomLabels,Label"));
      }

      if (lbCustomLabels.Items.Count > 0)
        lbCustomLabels.SelectedIndex = 0;
      #endregion

      #region Strips tab
      lbAxisStrips.Items.Clear();
      foreach (StripLine strip in Axis.StripLines)
      {
        lbAxisStrips.Items.Add(Res.Get("Forms,ChartEditor,ChartEditorControl,Axes,Strips,Strip"));
      }
      
      if (lbAxisStrips.Items.Count > 0)
        lbAxisStrips.SelectedIndex = 0;
      #endregion

      FUpdating = false;
      
      btnCopyFromXAxis.Visible = cbxAxis.SelectedIndex == 1;
      btnCopyFromPrimaryAxis.Visible = cbxAxis.SelectedIndex > 1;
      
      UpdateAxisGridControls();
      UpdateAxisTickControls();
      UpdateAxisStripControls();
      UpdateAxisCustomLabels();
    }
    
    private void UpdateAxisGridControls()
    {
      FUpdating = true;
      
      cbAxisGridEnabled.Checked = Grid.Enabled;
      cbxAxisGridLineColor.Color = Grid.LineColor;
      cbxAxisGridLineStyle.SelectedIndex = (int)Grid.LineDashStyle;
      udAxisGridLineWidth.Value = Grid.LineWidth;
      
      FUpdating = false;
    }

    private void UpdateAxisTickControls()
    {
      FUpdating = true;

      cbAxisTickEnabled.Checked = Tick.Enabled;
      cbxAxisTickStyle.SelectedIndex = (int)Tick.TickMarkStyle;
      udAxisTickSize.Value = (decimal)Tick.Size;
      cbxAxisTickLineColor.Color = Tick.LineColor;
      cbxAxisTickLineStyle.SelectedIndex = (int)Tick.LineDashStyle;
      udAxisTickLineWidth.Value = Tick.LineWidth;

      FUpdating = false;
    }
    
    private void UpdateAxisStripControls()
    {
      FUpdating = true;

      if (Strip == null)
        tabStripSettings.Enabled = false;
      else
      {
        tabStripSettings.Enabled = true;
      
        udStripStart.Value = (decimal)Strip.IntervalOffset;
        udStripWidth.Value = (decimal)Strip.StripWidth;
        udStripInterval.Value = (decimal)Strip.Interval;
        cbxStripStartType.SelectedIndex = (int)Strip.IntervalOffsetType;
        cbxStripWidthType.SelectedIndex = (int)Strip.StripWidthType;
        cbxStripIntervalType.SelectedIndex = (int)Strip.IntervalType;
        
        cbxStripBackColor.Color = Strip.BackColor;
        cbxStripSecondaryColor.Color = Strip.BackSecondaryColor;
        cbxStripGradient.SelectedIndex = (int)Strip.BackGradientStyle;
        cbxStripHatchStyle.SelectedIndex = (int)Strip.BackHatchStyle;
        
        cbxStripBorderColor.Color = Strip.BorderColor;
        cbxStripBorderStyle.SelectedIndex = (int)Strip.BorderDashStyle;
        udStripBorderWidth.Value = Strip.BorderWidth;
        
        tbStripText.Text = Strip.Text;
        tbStripTextFont.Text = Converter.ToString(Strip.Font);
        cbxStripTextForeColor.Color = Strip.ForeColor;
        cbxStripTextHorzAlign.SelectedIndex = (int)Strip.TextAlignment;
        cbxStripTextVertAlign.SelectedIndex = (int)Strip.TextLineAlignment;
        cbxStripTextOrientation.SelectedIndex = (int)Strip.TextOrientation;
      }
      
      FUpdating = false;
    }

    private void UpdateAxisCustomLabels()
    {
      FUpdating = true;

      if (CustomLabel == null)
        pnCustomLabel.Enabled = false;
      else
      {
        pnCustomLabel.Enabled = true;
        
        tbCustomLabelText.Text = CustomLabel.Text;
        udCustomLabelStart.Value = (decimal)CustomLabel.FromPosition;
        udCustomLabelEnd.Value = (decimal)CustomLabel.ToPosition;
        udCustomLabelRow.Value = CustomLabel.RowIndex;
        cbxCustomLabelForeColor.Color = CustomLabel.ForeColor;
        cbxCustomLabelMarkColor.Color = CustomLabel.MarkColor;
        cbxCustomLabelMarkStyle.SelectedIndex = (int)CustomLabel.LabelMark;
      }
      
      FUpdating = false;
    }

    public void UpdateChartAreas()
    {
      cbxChartArea.Items.Clear();
      foreach (ChartArea area in MSChart.ChartAreas)
      {
        cbxChartArea.Items.Add(area.Name);
      }
      
      cbxChartArea.SelectedIndex = 0;
    }
    
    private void UpdateAxes()
    {
      cbxAxis.Items.Clear();
      foreach (Axis axis in ChartArea.Axes)
      {
        cbxAxis.Items.Add(axis.Name);
      }
      
      pgAxes.Enabled = cbxAxis.Items.Count > 0;
      if (cbxAxis.Items.Count > 0)
        cbxAxis.SelectedIndex = 0;
    }
    
    private void OnChange()
    {
      if (Changed != null)
        Changed(this, EventArgs.Empty);
    }

    private void Init()
    {
      MyRes res = null;
      MyRes cmnRes = new MyRes("Forms,ChartEditor,Common");

      #region Comboboxes
      tbFilter.Image = Res.GetImage(52);
      tbAxisLabelFont.Image = Res.GetImage(59);
      tbAxisTitleFont.Image = Res.GetImage(59);
      tbStripTextFont.Image = Res.GetImage(59);
      tbLegendFont.Image = Res.GetImage(59);
      tbTitleFont.Image = Res.GetImage(59);

      res = new MyRes("Forms,GroupBandEditor");
      cbxAutoSeriesSort.Items.Clear();
      cbxAutoSeriesSort.Items.AddRange(new string[] {
        res.Get("NoSort"), res.Get("Ascending"), res.Get("Descending") });

      cbxGradient.Items.Clear();
      cbxGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));

      cbxHatchStyle.Items.Clear();
      cbxHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));

      cbxBorderSkin.Items.Clear();
      cbxBorderSkin.Items.AddRange(Enum.GetNames(typeof(BorderSkinStyle)));

      cbxBorderStyle.Items.Clear();
      cbxBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxSeriesPalette.Items.Clear();
      cbxSeriesPalette.Items.AddRange(Enum.GetNames(typeof(ChartColorPalette)));
      
      lbTemplate.Items.Clear();
      lbTemplate.Items.AddRange(new string[] {
        "Custom", "Blue", "Gray", "Green", "Pink", "Sand", "Steel", "Light" });

      cbxAreaGradient.Items.Clear();
      cbxAreaGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));

      cbxAreaHatchStyle.Items.Clear();
      cbxAreaHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));

      cbxAreaBorderStyle.Items.Clear();
      cbxAreaBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbx3DLightStyle.Items.Clear();
      cbx3DLightStyle.Items.AddRange(Enum.GetNames(typeof(LightStyle)));

      cbxAxisIntervalType.Items.Clear();
      cbxAxisIntervalType.Items.AddRange(Enum.GetNames(typeof(DateTimeIntervalType)));
      
      cbxAxisLineStyle.Items.Clear();
      cbxAxisLineStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxCustomLabelMarkStyle.Items.Clear();
      cbxCustomLabelMarkStyle.Items.AddRange(Enum.GetNames(typeof(LabelMarkStyle)));

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes");
      cbxAxisGrid.Items.Clear();
      cbxAxisGrid.Items.AddRange(new string[] {
        res.Get("Major"), res.Get("Minor") });

      cbxAxisTick.Items.Clear();
      cbxAxisTick.Items.AddRange(new string[] {
        res.Get("Major"), res.Get("Minor") });

      cbxAxisGridLineStyle.Items.Clear();
      cbxAxisGridLineStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxAxisTickStyle.Items.Clear();
      cbxAxisTickStyle.Items.AddRange(Enum.GetNames(typeof(TickMarkStyle)));

      cbxAxisTickLineStyle.Items.Clear();
      cbxAxisTickLineStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxStripStartType.Items.Clear();
      cbxStripStartType.Items.AddRange(Enum.GetNames(typeof(IntervalType)));

      cbxStripWidthType.Items.Clear();
      cbxStripWidthType.Items.AddRange(Enum.GetNames(typeof(IntervalType)));

      cbxStripIntervalType.Items.Clear();
      cbxStripIntervalType.Items.AddRange(Enum.GetNames(typeof(IntervalType)));

      cbxStripGradient.Items.Clear();
      cbxStripGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));

      cbxStripHatchStyle.Items.Clear();
      cbxStripHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));

      cbxStripBorderStyle.Items.Clear();
      cbxStripBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxStripTextOrientation.Items.Clear();
      cbxStripTextOrientation.Items.AddRange(Enum.GetNames(typeof(TextOrientation)));

      cbxStripTextHorzAlign.Items.Clear();
      cbxStripTextHorzAlign.Items.AddRange(Enum.GetNames(typeof(StringAlignment)));

      cbxStripTextVertAlign.Items.Clear();
      cbxStripTextVertAlign.Items.AddRange(Enum.GetNames(typeof(StringAlignment)));

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Legend");
      cbxLegendStyle.Items.Clear();
      cbxLegendStyle.Items.AddRange(new string[] {
        res.Get("StyleColumn"), res.Get("StyleRow"), res.Get("StyleTable") });

      cbxLegendGradient.Items.Clear();
      cbxLegendGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));

      cbxLegendHatchStyle.Items.Clear();
      cbxLegendHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));

      cbxLegendBorderStyle.Items.Clear();
      cbxLegendBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));

      cbxTitleTextOrientation.Items.Clear();
      cbxTitleTextOrientation.Items.AddRange(Enum.GetNames(typeof(TextOrientation)));

      cbxTitleTextStyle.Items.Clear();
      cbxTitleTextStyle.Items.AddRange(Enum.GetNames(typeof(TextStyle)));

      cbxTitleGradient.Items.Clear();
      cbxTitleGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));

      cbxTitleHatchStyle.Items.Clear();
      cbxTitleHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));

      cbxTitleBorderStyle.Items.Clear();
      cbxTitleBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));
      #endregion
      
      #region Data
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Data");
      pgData.Text = res.Get("");
      lblDataSource.Text = res.Get("DataSource");
      lblFilter.Text = res.Get("Filter");
      lblAutoSeries.Text = res.Get("AutoSeries");
      lblAutoSeriesData.Text = res.Get("DataColumn");
      lblAutoSeriesColor.Text = res.Get("ColorColumn");
      lblAutoSeriesSort.Text = res.Get("SeriesSort");
      cbAlignXValues.Text = res.Get("AlignXValues");
      cbAutoSeriesForce.Text = res.Get("ForceAutoSeries");
      #endregion

      #region Appearance
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Appearance");
      pgAppearance.Text = res.Get("");
      lblTemplate.Text = res.Get("Template");
      btnLoadTemplate.Text = res.Get("Load");
      btnSaveTemplate.Text = res.Get("Save");
      lblSeriesPalette.Text = res.Get("SeriesPalette");
      lblBackColor.Text = cmnRes.Get("BackColor");
      lblSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblGradient.Text = cmnRes.Get("Gradient");
      lblHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblBorderSkin.Text = res.Get("BorderSkin");
      lblBorderColor.Text = cmnRes.Get("BorderColor");
      lblBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblBorderWidth.Text = cmnRes.Get("BorderWidth");
      #endregion
      
      #region Chart area
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,ChartArea");
      pgChartArea.Text = res.Get("");
      lblChartArea.Text = res.Get("Area");
      lblAreaBackColor.Text = cmnRes.Get("BackColor");
      lblAreaSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblAreaGradient.Text = cmnRes.Get("Gradient");
      lblAreaHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblAreaBorderColor.Text = cmnRes.Get("BorderColor");
      lblAreaBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblAreaBorderWidth.Text = cmnRes.Get("BorderWidth");
      lblAreaShadowColor.Text = cmnRes.Get("ShadowColor");
      lblAreaShadowOffset.Text = cmnRes.Get("ShadowOffset");
      #endregion
      
      #region 3D
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,ThreeD");
      pg3D.Text = res.Get("");
      cb3DEnable.Text = res.Get("Enable");
      lbl3DXRotation.Text = res.Get("XRotation");
      lbl3DYRotation.Text = res.Get("YRotation");
      lbl3DPerspective.Text = res.Get("Perspective");
      lbl3DWallWidth.Text = res.Get("WallWidth");
      lbl3DAxisDepth.Text = res.Get("AxisDepth");
      lbl3DLightStyle.Text = res.Get("LightStyle");
      cb3DClustered.Text = res.Get("ClusteredSeries");
      #endregion
      
      #region Axes
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes");
      pgAxes.Text = res.Get("");
      lblAxis.Text = res.Get("Axis");
      btnCopyFromXAxis.Text = res.Get("CopyFromX");
      btnCopyFromPrimaryAxis.Text = res.Get("CopyFromPrimary");
      
      #region General
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,General");
      tabAxisGeneral.Text = res.Get("");
      cbAxisEnabled.Text = res.Get("Enabled");
      cbAxisMargin.Text = res.Get("MarginVisible");
      cbAxisLogarithmic.Text = res.Get("Logarithmic");
      cbAxisReversed.Text = res.Get("Reversed");
      lblAxisInterval.Text = res.Get("Interval");
      lblAxisLineColor.Text = cmnRes.Get("LineColor");
      lblAxisLineStyle.Text = cmnRes.Get("LineStyle");
      lblAxisLineWidth.Text = cmnRes.Get("LineWidth");
      cbAxisInterlaced.Text = res.Get("Interlaced");
      lblAxisInterlaceColor.Text = res.Get("InterlacedColor");
      #endregion
      
      #region Labels
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Labels");
      tabAxisLabels.Text = res.Get("");
      cbAxisLabelEnabled.Text = res.Get("Enabled");
      lblAxisLabelFormat.Text = cmnRes.Get("Format");
      lblAxisLabelFont.Text = cmnRes.Get("Font");
      lblAxisLabelForeColor.Text = cmnRes.Get("ForeColor");
      lblAxisLabelAngle.Text = res.Get("Angle");
      #endregion

      #region Title
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Title");
      tabAxisTitle.Text = res.Get("");
      lblAxisTitleText.Text = cmnRes.Get("Text");
      lblAxisTitleFont.Text = cmnRes.Get("Font");
      lblAxisTitleForeColor.Text = cmnRes.Get("ForeColor");
      #endregion

      #region Custom labels
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,CustomLabels");
      tabAxisCustomLabels.Text = res.Get("");
      lblCustomLabelText.Text = cmnRes.Get("Text");
      lblCustomLabelStart.Text = res.Get("Start");
      lblCustomLabelEnd.Text = res.Get("End");
      lblCustomLabelRow.Text = res.Get("Row");
      lblCustomLabelForeColor.Text = cmnRes.Get("ForeColor");
      lblCustomLabelMarkColor.Text = res.Get("MarkColor");
      lblCustomLabelMarkStyle.Text = res.Get("MarkStyle");
      #endregion
      
      #region Grid
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Grid");
      tabAxisGrid.Text = res.Get("");
      lblAxisGrid.Text = res.Get("GridLine");
      cbAxisGridEnabled.Text = res.Get("Enabled");
      lblAxisGridLineColor.Text = cmnRes.Get("LineColor");
      lblAxisGridLineStyle.Text = cmnRes.Get("LineStyle");
      lblAxisGridLineWidth.Text = cmnRes.Get("LineWidth");
      #endregion
      
      #region Ticks
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Ticks");
      tabAxisTicks.Text = res.Get("");
      lblAxisTick.Text = res.Get("Ticks");
      cbAxisTickEnabled.Text = res.Get("Enabled");
      lblAxisTickStyle.Text = res.Get("TickStyle");
      lblAxisTickSize.Text = res.Get("TickSize");
      lblAxisTickLineColor.Text = cmnRes.Get("LineColor");
      lblAxisTickLineStyle.Text = cmnRes.Get("LineStyle");
      lblAxisTickLineWidth.Text = cmnRes.Get("LineWidth");
      #endregion
      
      #region Strips
      tabAxisStrips.Text = Res.Get("Forms,ChartEditor,ChartEditorControl,Axes,Strips");
      
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Strips,Interval");
      tabStripInterval.Text = res.Get("");
      lblStripStart.Text = res.Get("Start");
      lblStripWidth.Text = res.Get("Width");
      lblStripInterval.Text = res.Get("Interval");

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Strips,Appearance");
      tabStripAppearance.Text = res.Get("");
      lblStripBackColor.Text = cmnRes.Get("BackColor");
      lblStripSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblStripGradient.Text = cmnRes.Get("Gradient");
      lblStripHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblStripBorderColor.Text = cmnRes.Get("BorderColor");
      lblStripBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblStripBorderWidth.Text = cmnRes.Get("BorderWidth");

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Axes,Strips,Text");
      tabStripText.Text = res.Get("");
      lblStripText.Text = cmnRes.Get("Text");
      lblStripFont.Text = cmnRes.Get("Font");
      lblStripTextForeColor.Text = cmnRes.Get("ForeColor");
      lblStripTextAlign.Text = res.Get("HorizontalAlign");
      lblStripLineAlign.Text = res.Get("VerticalAlign");
      lblStripTextOrientation.Text = res.Get("TextOrientation");
      #endregion
      #endregion
      
      #region Legend
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Legend");
      pgLegend.Text = res.Get("");
      cbLegendEnabled.Text = res.Get("Enabled");

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Legend,General");
      tabLegendGeneral.Text = res.Get("");
      lblLegendStyle.Text = res.Get("LegendStyle");
      lblLegendDock.Text = cmnRes.Get("Dock");
      cbLegendInsideChartArea.Text = cmnRes.Get("InsideChartArea");

      tabLegendAppearance.Text = Res.Get("Forms,ChartEditor,ChartEditorControl,Legend,Appearance");
      lblLegendBackColor.Text = cmnRes.Get("BackColor");
      lblLegendSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblLegendGradient.Text = cmnRes.Get("Gradient");
      lblLegendHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblLegendBorderColor.Text = cmnRes.Get("BorderColor");
      lblLegendBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblLegendBorderWidth.Text = cmnRes.Get("BorderWidth");
      lblLegendShadowColor.Text = cmnRes.Get("ShadowColor");
      lblLegendShadowOffset.Text = cmnRes.Get("ShadowOffset");
      lblLegendFont.Text = cmnRes.Get("Font");
      lblLegendForeColor.Text = cmnRes.Get("ForeColor");
      #endregion
      
      #region Title
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Title");
      pgTitle.Text = res.Get("");
      cbTitleVisible.Text = res.Get("Visible");

      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Title,General");
      tabTitleGeneral.Text = res.Get("");
      lblTitleText.Text = cmnRes.Get("Text");
      lblTitleTextOrientation.Text = res.Get("TextOrientation");
      lblTitleTextStyle.Text = res.Get("TextStyle");
      lblTitleDock.Text = cmnRes.Get("Dock");
      cbTitleInsideChartArea.Text = cmnRes.Get("InsideChartArea");

      tabTitleAppearance.Text = Res.Get("Forms,ChartEditor,ChartEditorControl,Title,Appearance");
      lblTitleBackColor.Text = cmnRes.Get("BackColor");
      lblTitleSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblTitleGradient.Text = cmnRes.Get("Gradient");
      lblTitleHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblTitleBorderColor.Text = cmnRes.Get("BorderColor");
      lblTitleBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblTitleBorderWidth.Text = cmnRes.Get("BorderWidth");
      lblTitleShadowColor.Text = cmnRes.Get("ShadowColor");
      lblTitleShadowOffset.Text = cmnRes.Get("ShadowOffset");
      lblTitleFont.Text = cmnRes.Get("Font");
      lblTitleForeColor.Text = cmnRes.Get("ForeColor");
      #endregion
    }

    public ChartEditorControl()
    {
      InitializeComponent();
      Init();
    }

    #region Data tab
    private void cbxDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      DataSourceBase ds = Report.GetDataSource((string)cbxDataSource.Items[cbxDataSource.SelectedIndex]);
      Chart.DataSource = ds;
      cbxAutoSeriesData.DataSource = ds;
      cbxAutoSeriesColor.DataSource = ds;
      OnChange();
    }

    private void tbFilter_ButtonClick(object sender, EventArgs e)
    {
      tbFilter.Text = Editors.EditExpression(Report, tbFilter.Text);
    }

    private void tbFilter_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Chart.Filter = tbFilter.Text;
      OnChange();
    }

    private void cbxAutoSeriesData_Leave(object sender, EventArgs e)
    {
      Chart.AutoSeriesColumn = cbxAutoSeriesData.Text;
      OnChange();
    }

    private void cbxAutoSeriesColor_Leave(object sender, EventArgs e)
    {
      Chart.AutoSeriesColor = cbxAutoSeriesColor.Text;
      OnChange();
    }

    private void cbxAutoSeriesSort_Leave(object sender, EventArgs e)
    {
      Chart.AutoSeriesSortOrder = (SortOrder)cbxAutoSeriesSort.SelectedIndex;
      OnChange();
    }

    private void cbAlignXValues_Leave(object sender, EventArgs e)
    {
      Chart.AlignXValues = cbAlignXValues.Checked;
      OnChange();
    }
    #endregion

    #region Appearance tab
    private void LoadTemplate(Stream stream)
    {
      using (Chart tempChart = new Chart())
      {
        tempChart.Serializer.Content = SerializationContents.All;
        tempChart.Serializer.Load(stream);
        
        Helper.AssignChartAppearance(MSChart, tempChart);
      }
      
      UpdateControls();
      OnChange();
    }

    private void lbTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating || lbTemplate.SelectedIndex == 0)
        return;

      string templateName = (string)lbTemplate.SelectedItem;
      using (Stream stream = ResourceLoader.GetStream("MSChart." + templateName + ".xml"))
      {
        LoadTemplate(stream);
      }
    }

    private void lbTemplate_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      string item = lbTemplate.Items[e.Index].ToString();
      item = Res.Get("Forms,ChartEditor,ChartEditorControl,Appearance,Templates," + item);
      TextRenderer.DrawText(e.Graphics, item, e.Font, e.Bounds.Location, e.ForeColor);
    }
    
    private void btnLoadTemplate_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,XmlFile");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          using (FileStream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
          {
            LoadTemplate(stream);
          }
        }
      }
    }

    private void btnSaveTemplate_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dialog = new SaveFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,XmlFile");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          MSChart.Serializer.Content = SerializationContents.All;
          MSChart.Serializer.Save(dialog.FileName);
        }
      }
    }

    private void cbxBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BackColor = cbxBackColor.Color;
      OnChange();
    }

    private void cbxSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BackSecondaryColor = cbxSecondaryColor.Color;
      OnChange();
    }

    private void cbxGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BackGradientStyle = (GradientStyle)cbxGradient.SelectedIndex;
      OnChange();
    }

    private void cbxHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BackHatchStyle = (ChartHatchStyle)cbxHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxBorderSkin_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BorderSkin.SkinStyle = (BorderSkinStyle)cbxBorderSkin.SelectedIndex;
      OnChange();
    }

    private void cbxBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BorderlineColor = cbxBorderColor.Color;
      OnChange();
    }

    private void cbxBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BorderlineDashStyle = (ChartDashStyle)cbxBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.BorderlineWidth = (int)udBorderWidth.Value;
      OnChange();
    }

    private void cbxSeriesPalette_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      MSChart.Palette = (ChartColorPalette)cbxSeriesPalette.SelectedIndex;
      OnChange();
    }
    #endregion

    #region Chart area tab
    private void cbxChartArea_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateChartAreaControls();
    }

    private void cbxAreaBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BackColor = cbxAreaBackColor.Color;  
      OnChange();
    }

    private void cbxAreaSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BackSecondaryColor = cbxAreaSecondaryColor.Color;  
      OnChange();
    }

    private void cbxAreaGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BackGradientStyle = (GradientStyle)cbxAreaGradient.SelectedIndex;
      OnChange();
    }

    private void cbxAreaHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BackHatchStyle = (ChartHatchStyle)cbxAreaHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxAreaBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BorderColor = cbxAreaBorderColor.Color;  
      OnChange();
    }

    private void cbxAreaBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BorderDashStyle = (ChartDashStyle)cbxAreaBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udAreaBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.BorderWidth = (int)udAreaBorderWidth.Value;  
      OnChange();
    }

    private void cbxAreaShadowColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.ShadowColor = cbxAreaShadowColor.Color;  
      OnChange();
    }

    private void cbxAreaShadowOffset_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.ShadowOffset = (int)udAreaShadowOffset.Value;  
      OnChange();
    }
    #endregion

    #region 3D tab
    private void cb3DEnable_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.Area3DStyle.Enable3D = cb3DEnable.Checked;
      OnChange();
    }

    private void trb3DXRotation_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      FUpdating = true;
      if (sender == trb3DXRotation)
        ud3DXRotation.Value = trb3DXRotation.Value;
      else
        trb3DXRotation.Value = (int)ud3DXRotation.Value;
      ChartArea.Area3DStyle.Inclination = trb3DXRotation.Value;
      FUpdating = false;
      OnChange();
    }

    private void trb3DYRotation_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      FUpdating = true;
      if (sender == trb3DYRotation)
        ud3DYRotation.Value = trb3DYRotation.Value;
      else
        trb3DYRotation.Value = (int)ud3DYRotation.Value;
      ChartArea.Area3DStyle.Rotation = trb3DYRotation.Value;
      FUpdating = false;
      OnChange();
    }

    private void trb3DPerspective_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      FUpdating = true;
      if (sender == trb3DPerspective)
        ud3DPerspective.Value = trb3DPerspective.Value;
      else
        trb3DPerspective.Value = (int)ud3DPerspective.Value;
      ChartArea.Area3DStyle.Perspective = trb3DPerspective.Value;
      FUpdating = false;
      OnChange();
    }

    private void ud3DWallWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.Area3DStyle.WallWidth = (int)ud3DWallWidth.Value;
      OnChange();
    }

    private void ud3DAxisDepth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.Area3DStyle.PointDepth = (int)ud3DAxisDepth.Value;
      OnChange();
    }

    private void cbx3DLightStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.Area3DStyle.LightStyle = (LightStyle)cbx3DLightStyle.SelectedIndex;
      OnChange();
    }

    private void cb3DClustered_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartArea.Area3DStyle.IsClustered = cb3DClustered.Checked;
      OnChange();
    }
    #endregion

    #region Axes tab
    private void cbxAxis_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateAxisControls();
    }

    private void btnCopyFromXAxis_Click(object sender, EventArgs e)
    {
      Helper.AssignAxisAppearance(Axis, ChartArea.AxisX);
      UpdateAxisControls();
      OnChange();
    }

    private void btnCopyFromPrimaryAxis_Click(object sender, EventArgs e)
    {
      Axis primaryAxis = cbxAxis.SelectedIndex == 2 ? ChartArea.AxisX : ChartArea.AxisY;
      Helper.AssignAxisAppearance(Axis, primaryAxis);
      UpdateAxisControls();
      OnChange();
    }

    #region General tab
    private void cbAxisEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.Enabled = cbAxisEnabled.Checked ? AxisEnabled.Auto : AxisEnabled.False;
      OnChange();
    }

    private void cbAxisMargin_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.IsMarginVisible = cbAxisMargin.Checked;
      OnChange();
    }

    private void cbAxisLogarithmic_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.IsLogarithmic = cbAxisLogarithmic.Checked;
      OnChange();
    }

    private void cbAxisReversed_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.IsReversed = cbAxisReversed.Checked;
      OnChange();
    }

    private void udAxisInterval_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.Interval = (double)udAxisInterval.Value;
      OnChange();
    }

    private void cbxAxisIntervalType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.IntervalType = (DateTimeIntervalType)cbxAxisIntervalType.SelectedIndex;
      OnChange();
    }

    private void cbAxisInterlaced_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.IsInterlaced = cbAxisInterlaced.Checked;
      OnChange();
    }

    private void cbxAxisLineColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LineColor = cbxAxisLineColor.Color;
      OnChange();
    }

    private void cbxAxisLineStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LineDashStyle = (ChartDashStyle)cbxAxisLineStyle.SelectedIndex;
      OnChange();
    }

    private void udAxisLineWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LineWidth = (int)udAxisLineWidth.Value;
      OnChange();
    }

    private void cbxAxisInterlacedColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.InterlacedColor = cbxAxisInterlacedColor.Color;
      OnChange();
    }
    #endregion

    #region Labels tab
    private void cbAxisLabelEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LabelStyle.Enabled = cbAxisLabelEnabled.Checked;
      OnChange();
    }
    
    private void tbAxisLabelFormat_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LabelStyle.Format = tbAxisLabelFormat.Text;
      OnChange();
    }

    private void tbAxisLabelFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Axis.LabelStyle.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Axis.LabelStyle.Font = dialog.Font;
          tbAxisLabelFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxAxisLabelForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LabelStyle.ForeColor = cbxAxisLabelForeColor.Color;
      OnChange();
    }

    private void udAxisLabelAngle_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.LabelStyle.Angle = (int)udAxisLabelAngle.Value;
      OnChange();
    }
    #endregion

    #region Title tab
    private void tbAxisTitleText_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.Title = tbAxisTitleText.Text;
      OnChange();
    }

    private void tbAxisTitleFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Axis.TitleFont;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Axis.TitleFont = dialog.Font;
          tbAxisTitleFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxAxisTitleForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Axis.TitleForeColor = cbxAxisTitleForeColor.Color;
      OnChange();
    }
    #endregion

    #region Custom labels tab
    private void lbCustomLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateAxisCustomLabels();
    }

    private void btnAddCustomLabel_Click(object sender, EventArgs e)
    {
      CustomLabel label = new CustomLabel();
      Axis.CustomLabels.Add(label);
      lbCustomLabels.Items.Add(Res.Get("Forms,ChartEditor,ChartEditorControl,Axes,CustomLabels,Label"));
      lbCustomLabels.SelectedIndex = lbCustomLabels.Items.Count - 1;
      OnChange();
    }

    private void btnDeleteCustomLabel_Click(object sender, EventArgs e)
    {
      int index = lbCustomLabels.SelectedIndex;
      lbCustomLabels.Items.RemoveAt(index);
      Axis.CustomLabels.RemoveAt(index);
      if (index >= lbCustomLabels.Items.Count)
        index--;
      if (index >= 0)
        lbCustomLabels.SelectedIndex = index;
      OnChange();
    }

    private void tbCustomLabelText_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.Text = tbCustomLabelText.Text;  
      OnChange();
    }

    private void udCustomLabelStart_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.FromPosition = (double)udCustomLabelStart.Value;
      OnChange();
    }

    private void udCustomLabelEnd_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.ToPosition = (double)udCustomLabelEnd.Value;
      OnChange();
    }

    private void udCustomLabelRow_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.RowIndex = (int)udCustomLabelRow.Value;
      OnChange();
    }

    private void cbxCustomLabelForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.ForeColor = cbxCustomLabelForeColor.Color;
      OnChange();
    }

    private void cbxCustomLabelMarkColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.MarkColor = cbxCustomLabelMarkColor.Color;
      OnChange();
    }

    private void cbxCustomLabelMarkStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      CustomLabel.LabelMark = (LabelMarkStyle)cbxCustomLabelMarkStyle.SelectedIndex;
      OnChange();
    }
    #endregion

    #region Grid tab
    private void cbxAxisGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateAxisGridControls();
    }

    private void cbAxisGridEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Grid.Enabled = cbAxisGridEnabled.Checked;
      OnChange();
    }

    private void cbxAxisGridLineColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Grid.LineColor = cbxAxisGridLineColor.Color;
      OnChange();
    }

    private void cbxAxisGridLineStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Grid.LineDashStyle = (ChartDashStyle)cbxAxisGridLineStyle.SelectedIndex;
      OnChange();
    }

    private void udAxisGridLineWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Grid.LineWidth = (int)udAxisGridLineWidth.Value;
      OnChange();
    }
    #endregion

    #region Ticks tab
    private void cbxAxisTick_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateAxisTickControls();  
    }

    private void cbAxisTickEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.Enabled = cbAxisTickEnabled.Checked;
      OnChange();
    }

    private void cbxAxisTickStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.TickMarkStyle = (TickMarkStyle)cbxAxisTickStyle.SelectedIndex;
      OnChange();
    }

    private void udAxisTickSize_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.Size = (float)udAxisTickSize.Value;
      OnChange();
    }

    private void cbxAxisTickLineColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.LineColor = cbxAxisTickLineColor.Color;
      OnChange();
    }

    private void cbxAxisTickLineStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.LineDashStyle = (ChartDashStyle)cbxAxisTickLineStyle.SelectedIndex;
      OnChange();
    }

    private void udAxisTickLineWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Tick.LineWidth = (int)udAxisTickLineWidth.Value;
      OnChange();
    }
    #endregion

    #region Strips tab
    private void lbAxisStrips_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateAxisStripControls();
    }

    private void btnAddStrip_Click(object sender, EventArgs e)
    {
      StripLine strip = new StripLine();
      Axis.StripLines.Add(strip);
      lbAxisStrips.Items.Add(Res.Get("Forms,ChartEditor,ChartEditorControl,Axes,Strips,Strip"));
      lbAxisStrips.SelectedIndex = lbAxisStrips.Items.Count - 1;
      OnChange();
    }

    private void btnDeleteStrip_Click(object sender, EventArgs e)
    {
      int index = lbAxisStrips.SelectedIndex;
      lbAxisStrips.Items.RemoveAt(index);
      Axis.StripLines.RemoveAt(index);
      if (index >= lbAxisStrips.Items.Count)
        index--;
      if (index >= 0)
        lbAxisStrips.SelectedIndex = index;
      OnChange();
    }

    private void udStripStart_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.IntervalOffset = (double)udStripStart.Value;
      OnChange();
    }

    private void udStripWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.StripWidth = (double)udStripWidth.Value;
      OnChange();
    }

    private void udStripInterval_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.Interval = (double)udStripInterval.Value;
      OnChange();
    }

    private void cbxStripStartType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.IntervalOffsetType = (DateTimeIntervalType)cbxStripStartType.SelectedIndex;
      OnChange();
    }

    private void cbxStripWidthType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.StripWidthType = (DateTimeIntervalType)cbxStripWidthType.SelectedIndex;
      OnChange();
    }

    private void cbxStripIntervalType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.IntervalType = (DateTimeIntervalType)cbxStripIntervalType.SelectedIndex;
      OnChange();
    }

    private void cbxStripBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BackColor = cbxStripBackColor.Color;
      OnChange();
    }

    private void cbxStripSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BackSecondaryColor = cbxStripSecondaryColor.Color;
      OnChange();
    }

    private void cbxStripGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BackGradientStyle = (GradientStyle)cbxStripGradient.SelectedIndex;
      OnChange();
    }

    private void cbxStripHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BackHatchStyle = (ChartHatchStyle)cbxStripHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxStripBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BorderColor = cbxStripBorderColor.Color;
      OnChange();
    }

    private void cbxStripBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BorderDashStyle = (ChartDashStyle)cbxStripBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udStripBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.BorderWidth = (int)udStripBorderWidth.Value;
      OnChange();
    }

    private void tbStripText_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.Text = tbStripText.Text;
      OnChange();
    }

    private void tbStripTextFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Strip.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Strip.Font = dialog.Font;
          tbStripTextFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxStripTextForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.ForeColor = cbxStripTextForeColor.Color;
      OnChange();
    }

    private void cbxStripTextHorzAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.TextAlignment = (StringAlignment)cbxStripTextHorzAlign.SelectedIndex;
      OnChange();
    }

    private void cbxStripTextVertAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.TextLineAlignment = (StringAlignment)cbxStripTextVertAlign.SelectedIndex;
      OnChange();
    }

    private void cbxStripTextOrientation_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Strip.TextOrientation = (TextOrientation)cbxStripTextOrientation.SelectedIndex;
      OnChange();
    }
    #endregion
    #endregion

    #region Legend tab
    private void cbLegendEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.Enabled = cbLegendEnabled.Checked;  
      OnChange();
    }
    
    private void cbxLegendStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.LegendStyle = (LegendStyle)cbxLegendStyle.SelectedIndex;
      OnChange();
    }

    private void btnL12_Click(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      int dock = int.Parse((sender as Button).Tag.ToString());
      Legend.Docking = (Docking)(dock / 3);
      Legend.Alignment = (StringAlignment)(dock % 3);
      UpdateLegendDockButtons();
      OnChange();
    }

    private void cbLegendInsideChartArea_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.IsDockedInsideChartArea = cbLegendInsideChartArea.Checked;
      Legend.DockedToChartArea = Legend.IsDockedInsideChartArea ? "Default" : "";
      OnChange();
    }

    private void cbxLegendBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BackColor = cbxLegendBackColor.Color;
      OnChange();
    }

    private void cbxLegendSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BackSecondaryColor = cbxLegendSecondaryColor.Color;
      OnChange();
    }

    private void cbxLegendGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BackGradientStyle = (GradientStyle)cbxLegendGradient.SelectedIndex;
      OnChange();
    }

    private void cbxLegendHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BackHatchStyle = (ChartHatchStyle)cbxLegendHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxLegendBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BorderColor = cbxLegendBorderColor.Color;
      OnChange();
    }

    private void cbxLegendBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BorderDashStyle = (ChartDashStyle)cbxLegendBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udLegendBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.BorderWidth = (int)udLegendBorderWidth.Value;
      OnChange();
    }

    private void cbxLegendShadowColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.ShadowColor = cbxLegendShadowColor.Color;
      OnChange();
    }

    private void udLegendShadowOffset_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.ShadowOffset = (int)udLegendShadowOffset.Value;
      OnChange();
    }

    private void tbLegendFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Legend.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Legend.Font = dialog.Font;
          tbLegendFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxLegendForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Legend.ForeColor = cbxLegendForeColor.Color;
      OnChange();
    }
    #endregion

    #region Title tab
    private void lblTitleVisible_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.Visible = cbTitleVisible.Checked;
      OnChange();
    }

    private void tbTitleText_Leave(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.Text = tbTitleText.Text;
      OnChange();
    }

    private void cbxTitleTextOrientation_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.TextOrientation = (TextOrientation)cbxTitleTextOrientation.SelectedIndex;
      OnChange();
    }

    private void cbxTitleTextStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.TextStyle = (TextStyle)cbxTitleTextStyle.SelectedIndex;
      OnChange();
    }

    private void btnT12_Click(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      int dock = int.Parse((sender as Button).Tag.ToString());
      Title.Docking = (Docking)(dock / 3);
      switch (dock % 3)
      {
        case 0:
          Title.Alignment = ContentAlignment.TopLeft;
          break;
          
        case 1:
          Title.Alignment = ContentAlignment.TopCenter;
          break;
          
        case 2:
          Title.Alignment = ContentAlignment.TopRight;
          break;  
      }
      
      UpdateTitleDockButtons();
      OnChange();
    }

    private void cbTitleDockInsideChartArea_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.IsDockedInsideChartArea = cbTitleInsideChartArea.Checked;
      Title.DockedToChartArea = Title.IsDockedInsideChartArea ? "Default" : "";
      OnChange();
    }

    private void cbxTitleBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BackColor = cbxTitleBackColor.Color;
      OnChange();
    }

    private void cbxTitleSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BackSecondaryColor = cbxTitleSecondaryColor.Color;
      OnChange();
    }

    private void cbxTitleGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BackGradientStyle = (GradientStyle)cbxTitleGradient.SelectedIndex;
      OnChange();
    }

    private void cbxTitleHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BackHatchStyle = (ChartHatchStyle)cbxTitleHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxTitleBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BorderColor = cbxTitleBorderColor.Color;
      OnChange();
    }

    private void cbxTitleBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BorderDashStyle = (ChartDashStyle)cbxTitleBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udTitleBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.BorderWidth = (int)udTitleBorderWidth.Value;
      OnChange();
    }

    private void cbxTitleShadowColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.ShadowColor = cbxTitleShadowColor.Color;
      OnChange();
    }

    private void udTitleShadowOffset_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.ShadowOffset = (int)udTitleShadowOffset.Value;
      OnChange();
    }

    private void tbTitleFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Title.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Title.Font = dialog.Font;
          tbTitleFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxTitleForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Title.ForeColor = cbxTitleForeColor.Color;
      OnChange();
    }
    #endregion

    private void cbForceAutoSeries_Leave(object sender, EventArgs e)
    {
      Chart.AutoSeriesForce = cbAutoSeriesForce.Checked;
      OnChange();
    }
  }
}
