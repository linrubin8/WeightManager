using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.MSChart
{
  internal partial class SeriesEditorControl : UserControl
  {
    private MSChartSeries FSeries;
    private bool FUpdating;
    
    public event EventHandler Changed;
    
    public MSChartSeries Series
    {
      get { return FSeries; }
      set
      {
        FSeries = value;
        UpdateControls();
      }
    }

    public int ActivePageIndex
    {
      get { return pageControl2.ActivePageIndex; }
      set { pageControl2.ActivePageIndex = value; }
    }

    private Series ChartSeries
    {
      get { return FSeries.SeriesSettings; }
    }
    
    private Report Report
    {
      get { return FSeries.Report; }
    }
    
    private MSChartObject Chart
    {
      get { return Series.Parent as MSChartObject; }
    }

    private void UpdateControls()
    {
      FUpdating = true;
      
      #region Data tab
      // name
      tbName.Text = ChartSeries.Name;

      // data panel
      int yValues = ChartSeries.YValuesPerPoint;
      string[] yValuesNames = Helper.GetYValuesNames(ChartSeries.ChartType);
      bool valueVisible = false;
      
      cbxXValue.Report = Report;
      cbxXValue.DataSource = Chart.DataSource;
      cbxXValue.Text = Series.XValue;

      cbxYValue1.Report = Report;
      cbxYValue1.DataSource = Chart.DataSource;
      cbxYValue1.Text = Series.YValue1;
      lblYValue1.Text = yValuesNames[0];

      cbxYValue2.Report = Report;
      cbxYValue2.DataSource = Chart.DataSource;
      cbxYValue2.Text = Series.YValue2;
      valueVisible = yValues > 1;
      cbxYValue2.Visible = valueVisible;
      lblYValue2.Visible = valueVisible;
      if (valueVisible)
        lblYValue2.Text = yValuesNames[1];

      cbxYValue3.Report = Report;
      cbxYValue3.DataSource = Chart.DataSource;
      cbxYValue3.Text = Series.YValue3;
      valueVisible = yValues > 2;
      cbxYValue3.Visible = valueVisible;
      lblYValue3.Visible = valueVisible;
      if (valueVisible)
        lblYValue3.Text = yValuesNames[2];

      cbxYValue4.Report = Report;
      cbxYValue4.DataSource = Chart.DataSource;
      cbxYValue4.Text = Series.YValue4;
      valueVisible = yValues > 3;
      cbxYValue4.Visible = valueVisible;
      lblYValue4.Visible = valueVisible;
      if (valueVisible)
        lblYValue4.Text = yValuesNames[3];
      
      pnData.Height = cbxYValue1.Top * (yValues + 1);
      pnOtherData.Location = new Point(pnData.Left, pnData.Bottom);

      // other data panel
      cbxColor.Report = Report;
      cbxColor.DataSource = Chart.DataSource;
      cbxColor.Text = Series.Color;
      
      tbFilter.Text = Series.Filter;

      cbxXValueType.SelectedIndex = (int)ChartSeries.XValueType;
      cbxYValueType.SelectedIndex = (int)ChartSeries.YValueType;
      cbxXAxisType.SelectedIndex = (int)ChartSeries.XAxisType;
      cbxYAxisType.SelectedIndex = (int)ChartSeries.YAxisType;

      // autoseries
      cbxAutoSeriesData.Report = Report;
      cbxAutoSeriesData.Text = Series.AutoSeriesColumn;
      cbAutoSeriesForce.Enabled = Chart.AutoSeriesForce;
      cbAutoSeriesForce.Checked = Series.AutoSeriesForce;
      cbxAutoSeriesData.DataSource = Chart.DataSource;
      cbxAutoSeriesData.Enabled = Chart.AutoSeriesForce;

      #endregion      

      #region Values tab
      colX.HeaderText = lblXValue.Text;
      colY1.HeaderText = yValuesNames[0];
      colY2.Visible = yValues > 1;
      if (colY2.Visible)
        colY2.HeaderText = yValuesNames[1];
      colY3.Visible = yValues > 2;
      if (colY3.Visible)
        colY3.HeaderText = yValuesNames[2];
      colY4.Visible = yValues > 3;
      if (colY4.Visible)
        colY4.HeaderText = yValuesNames[3];

      gvValues.Rows.Clear();
      foreach (DataPoint point in ChartSeries.Points)
      {
        object[] values = new object[yValues + 1];
        values[0] = point.AxisLabel;
        if (String.IsNullOrEmpty(point.AxisLabel))
          values[0] = point.XValue;
        for (int i = 0; i < yValues; i++)
        {
          values[i + 1] = point.YValues[i];
        }
        
        gvValues.Rows.Add(values);
      }
      #endregion

      #region Data processing tab
      // sort by
      cbxSortBy.SelectedIndex = (int)Series.SortBy;
      
      // sort order
      cbxSortOrder.SelectedIndex = (int)Series.SortOrder;
      
      // group
      cbxGroupBy.SelectedIndex = (int)Series.GroupBy;
      udGroupInterval.Value = (decimal)Series.GroupInterval;
      cbxGroupFunction.SelectedIndex = (int)Series.GroupFunction;
      
      // collect
      cbxCollectData.SelectedIndex = (int)Series.Collect;
      udCollectValue.Value = (decimal)Series.CollectValue;
      tbCollectedItemText.Text = Series.CollectedItemText;
      cbxCollectedItemColor.Color = Series.CollectedItemColor;
      
      // explode
      pnExplode.Visible = ChartSeries.ChartType == SeriesChartType.Pie || 
        ChartSeries.ChartType == SeriesChartType.Doughnut;
      cbxExplode.SelectedIndex = (int)Series.PieExplode;
      tbExplodedValue.Text = Series.PieExplodeValue;
      #endregion

      #region Appearance tab
      Helper.ConstructCustomPropertiesEditor(pgAppearance, ChartSeries, Changed);
      #endregion

      #region Fill & Border tab
      // palette
      cbxPalette.SelectedIndex = (int)ChartSeries.Palette;

      // colors
      cbxBackColor.Color = ChartSeries.Color;
      cbxSecondaryColor.Color = ChartSeries.BackSecondaryColor;

      // gradient
      cbxGradient.SelectedIndex = (int)ChartSeries.BackGradientStyle;
      
      // hatch
      cbxHatchStyle.SelectedIndex = (int)ChartSeries.BackHatchStyle;
      
      // border
      cbxBorderColor.Color = ChartSeries.BorderColor;
      cbxBorderStyle.SelectedIndex = (int)ChartSeries.BorderDashStyle;
      udBorderWidth.Value = ChartSeries.BorderWidth;
      
      // shadow
      cbxShadowColor.Color = ChartSeries.ShadowColor;
      udShadowOffset.Value = ChartSeries.ShadowOffset;
      #endregion

      #region Labels tab
      // label view
      cbxLabelView.SelectedIndex = 0;
      tbLabelPattern.Text = ChartSeries.Label;
      tbLabelFormat.Text = ChartSeries.LabelFormat;
      
      // label font
      tbLabelFont.Text = Converter.ToString(ChartSeries.Font);
      
      // label colors
      cbxLabelForeColor.Color = ChartSeries.LabelForeColor;
      cbxLabelBackColor.Color = ChartSeries.LabelBackColor;
      
      // label border
      cbxLabelBorderColor.Color = ChartSeries.LabelBorderColor;
      cbxLabelBorderStyle.SelectedIndex = (int)ChartSeries.LabelBorderDashStyle;
      udLabelBorderWidth.Value = ChartSeries.LabelBorderWidth;
      #endregion
      
      #region Markers tab
      // marker style
      cbxMarkerStyle.SelectedIndex = (int)ChartSeries.MarkerStyle;

      // marker size
      udMarkerSize.Value = ChartSeries.MarkerSize;

      // marker step
      udMarkerStep.Value = ChartSeries.MarkerStep;

      // marker color
      cbxMarkerColor.Color = ChartSeries.MarkerColor;
      
      // border color
      cbxMarkerBorderColor.Color = ChartSeries.MarkerBorderColor;
      
      // border width
      udMarkerBorderWidth.Value = ChartSeries.MarkerBorderWidth;

      #endregion
      
      FUpdating = false;
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
      tbLabelFont.Image = Res.GetImage(59);
      tbExplodedValue.Image = Res.GetImage(52);

      cbxXValueType.Items.Clear();
      cbxXValueType.Items.AddRange(new string[] {
        "Auto", "Double", "Single", "Int32", "Int64", "UInt32", "UInt64", 
        "String", "DateTime", "Date", "Time", "DateTimeOffset" });
      
      cbxYValueType.Items.Clear();
      cbxYValueType.Items.AddRange(new string[] {
        "Auto", "Double", "Single", "Int32", "Int64", "UInt32", "UInt64", 
        "DateTime", "Date", "Time", "DateTimeOffset" });

      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Data");
      cbxXAxisType.Items.Clear();
      cbxXAxisType.Items.AddRange(new string[] {
        res.Get("PrimaryAxis"), res.Get("SecondaryAxis") });

      cbxYAxisType.Items.Clear();
      cbxYAxisType.Items.AddRange(new string[] {
        res.Get("PrimaryAxis"), res.Get("SecondaryAxis") });

      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,DataProcessing");
      cbxSortBy.Items.Clear();
      cbxSortBy.Items.AddRange(new string[] {
        Res.Get("Forms,GroupBandEditor,NoSort"), res.Get("SortByX"), res.Get("SortByY") });

      res = new MyRes("Forms,GroupBandEditor");
      cbxSortOrder.Items.Clear();
      cbxSortOrder.Items.AddRange(new string[] {
        res.Get("Ascending"), res.Get("Descending") });

      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,DataProcessing,GroupBy");
      cbxGroupBy.Items.Clear();
      cbxGroupBy.Items.AddRange(new string[] {
        res.Get("None"), res.Get("XValue"), res.Get("Number"), res.Get("Years"), res.Get("Months"),
        res.Get("Weeks"), res.Get("Days"), res.Get("Hours"), res.Get("Minutes"), res.Get("Seconds"),
        res.Get("Milliseconds") });

      res = new MyRes("Forms,TotalEditor");
      cbxGroupFunction.Items.Clear();
      cbxGroupFunction.Items.AddRange(new object[] { 
        res.Get("Sum"), res.Get("Min"), res.Get("Max"), res.Get("Avg"), res.Get("Count") });

      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,DataProcessing,CollectData");
      cbxCollectData.Items.Clear();
      cbxCollectData.Items.AddRange(new string[] {
        res.Get("None"), res.Get("TopN"), res.Get("BottomN"), 
        res.Get("LessThan"), res.Get("LessThanPercent"), 
        res.Get("GreaterThan"), res.Get("GreaterThanPercent") });

      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,DataProcessing,Explode");
      cbxExplode.Items.Clear();
      cbxExplode.Items.AddRange(new string[] {
        res.Get("None"), res.Get("Biggest"), res.Get("Lowest"), res.Get("Specific") });
      
      cbxPalette.Items.Clear();
      cbxPalette.Items.AddRange(Enum.GetNames(typeof(ChartColorPalette)));
        
      cbxGradient.Items.Clear();
      cbxGradient.Items.AddRange(Enum.GetNames(typeof(GradientStyle)));
        
      cbxHatchStyle.Items.Clear();
      cbxHatchStyle.Items.AddRange(Enum.GetNames(typeof(ChartHatchStyle)));
      
      cbxBorderStyle.Items.Clear();
      cbxBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));
      
      cbxLabelView.Items.Clear();
      cbxLabelView.Items.AddRange(new string[] {
        "Custom", "X", "Y", "Percent", "X: Y", "X: Percent" });

      cbxLabelBorderStyle.Items.Clear();
      cbxLabelBorderStyle.Items.AddRange(Enum.GetNames(typeof(ChartDashStyle)));
      
      cbxMarkerStyle.Items.Clear();
      cbxMarkerStyle.Items.AddRange(Enum.GetNames(typeof(System.Windows.Forms.DataVisualization.Charting.MarkerStyle)));
      #endregion
      
      #region Data
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Data");
      pgData.Text = res.Get("");
      lblName.Text = res.Get("Name");
      lblXValue.Text = res.Get("XValue");
      lblColor.Text = res.Get("Color");
      lblFilter.Text = res.Get("Filter");
      lblXValueType.Text = res.Get("XValueType");
      lblYValueType.Text = res.Get("YValueType");
      lblXAxisType.Text = res.Get("XAxisType");
      lblYAxisType.Text = res.Get("YAxisType");
      res = new MyRes("Forms,ChartEditor,ChartEditorControl,Data");
      cbAutoSeriesForce.Text = res.Get("ForceAutoSeries");
      lblAutoSeriesData.Text = res.Get("DataColumn");
      lblAutoSeries.Text = res.Get("AutoSeries");
      #endregion

      #region Values
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Values");
      pgValues.Text = res.Get("");
      #endregion
      
      #region Data processing
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,DataProcessing");
      pgDataProcessing.Text = res.Get("");
      lblSortBy.Text = res.Get("SortBy");
      lblSortOrder.Text = res.Get("SortOrder");
      lblGroupBy.Text = res.Get("GroupBy");
      lblGroupInterval.Text = res.Get("GroupInterval");
      lblGroupFunction.Text = res.Get("GroupFunction");
      lblCollectData.Text = res.Get("CollectData");
      lblCollectValue.Text = res.Get("Value");
      lblCollectedItemText.Text = res.Get("CollectedText");
      lblCollectedItemColor.Text = res.Get("CollectedColor");
      lblExplode.Text = res.Get("Explode");
      lblExplodedValue.Text = res.Get("Value");
      #endregion

      #region Appearance
      pgAppearance.Text = Res.Get("Forms,ChartEditor,SeriesEditorControl,Appearance");
      #endregion
      
      #region Fill & border
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,FillBorder");
      pgFillBorder.Text = res.Get("");
      lblPalette.Text = res.Get("Palette");
      lblBackColor.Text = cmnRes.Get("Color");
      lblSecondaryColor.Text = cmnRes.Get("SecondaryColor");
      lblGradient.Text = cmnRes.Get("Gradient");
      lblHatchStyle.Text = cmnRes.Get("HatchStyle");
      lblBorderColor.Text = cmnRes.Get("BorderColor");
      lblBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblBorderWidth.Text = cmnRes.Get("BorderWidth");
      lblShadowColor.Text = cmnRes.Get("ShadowColor");
      lblShadowOffset.Text = cmnRes.Get("ShadowOffset");
      #endregion
      
      #region Labels
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Labels");
      pgLabels.Text = res.Get("");
      lblLabelView.Text = res.Get("LabelView");
      lblLabelPattern.Text = res.Get("Pattern");
      lblLabelFormat.Text = cmnRes.Get("Format");
      lblLabelFont.Text = cmnRes.Get("Font");
      lblLabelForeColor.Text = cmnRes.Get("ForeColor");
      lblLabelBackColor.Text = cmnRes.Get("BackColor");
      lblLabelBorderColor.Text = cmnRes.Get("BorderColor");
      lblLabelBorderStyle.Text = cmnRes.Get("BorderStyle");
      lblLabelBorderWidth.Text = cmnRes.Get("BorderWidth");
      #endregion
      
      #region Markers
      res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Markers");
      pgMarkers.Text = res.Get("");
      lblMarkerStyle.Text = res.Get("Style");
      lblMarkerSize.Text = res.Get("Size");
      lblMarkerStep.Text = res.Get("Step");
      lblMarkerColor.Text = res.Get("Color");
      lblMarkerBorderColor.Text = cmnRes.Get("BorderColor");
      lblMarkerBorderWidth.Text = cmnRes.Get("BorderWidth");
      #endregion
   }
    
    public SeriesEditorControl()
    {
      InitializeComponent();
      Init();
    }

    #region Data tab
    private void tbName_Leave(object sender, EventArgs e)
    {
      ChartSeries.Name = tbName.Text;
      OnChange();
    }

    private void cbxXValue_Leave(object sender, EventArgs e)
    {
      Series.XValue = cbxXValue.Text;
      OnChange();
    }

    private void cbxYValue1_Leave(object sender, EventArgs e)
    {
      Series.YValue1 = cbxYValue1.Text;
      OnChange();
    }

    private void cbxYValue2_Leave(object sender, EventArgs e)
    {
      Series.YValue2 = cbxYValue2.Text;
      OnChange();
    }

    private void cbxYValue3_Leave(object sender, EventArgs e)
    {
      Series.YValue3 = cbxYValue3.Text;
      OnChange();
    }

    private void cbxYValue4_Leave(object sender, EventArgs e)
    {
      Series.YValue4 = cbxYValue4.Text;
      OnChange();
    }

    private void cbxColor_Leave(object sender, EventArgs e)
    {
      Series.Color = cbxColor.Text;
      OnChange();
    }

    private void tbFilter_Leave(object sender, EventArgs e)
    {
      Series.Filter = tbFilter.Text;
      OnChange();
    }

    private void tbFilter_ButtonClick(object sender, EventArgs e)
    {
      tbFilter.Text = Editors.EditExpression(Report, tbFilter.Text);
    }

    private void cbxXValueType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.XValueType = (ChartValueType)cbxXValueType.SelectedIndex;
      OnChange();
    }

    private void cbxYValueType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.YValueType = (ChartValueType)cbxYValueType.SelectedIndex;
      OnChange();
    }

    private void cbxXAxisType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.XAxisType = (AxisType)cbxXAxisType.SelectedIndex;
      OnChange();
    }

    private void cbxYAxisType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.YAxisType = (AxisType)cbxYAxisType.SelectedIndex;
      OnChange();
    }
    #endregion

    #region Values tab
    private void gvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      GetGridValues();
    }

    private void gvValues_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
      GetGridValues();
    }

    private void gvValues_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
      GetGridValues();
    }
    
    private void GetGridValues()
    {
      if (FUpdating)
        return;

      if (gvValues.IsCurrentCellInEditMode)
        gvValues.EndEdit();

      int yValuesCount = Series.YValuesPerPoint;
      ChartSeries.Points.Clear();

      for (int i = 0; i < gvValues.Rows.Count - 1; i++)
      {
        object[] yValues = new object[yValuesCount];
        object xValue = gvValues[0, i].Value;
        if (ChartSeries.XValueType != ChartValueType.String)
        {
          try
          {
            xValue = double.Parse((string)xValue);
          }
          catch
          {
            xValue = 0;
          }
        }

        for (int j = 0; j < yValuesCount; j++)
        {
          object yValue = gvValues[j + 1, i].Value;
          try
          {
            yValues[j] = double.Parse(yValue.ToString());
          }
          catch
          {
            yValues[j] = 0;
          }
        }

        ChartSeries.Points.AddXY(xValue, yValues);
      }

      OnChange();
    }
    #endregion

    #region Data processing tab
    private void cbxSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
      Series.SortBy = (SortBy)cbxSortBy.SelectedIndex;
      cbxSortOrder.Enabled = Series.SortBy != SortBy.None;
      lblSortOrder.Enabled = cbxSortOrder.Enabled;
      OnChange();
    }

    private void cbxSortOrder_Leave(object sender, EventArgs e)
    {
      Series.SortOrder = (ChartSortOrder)cbxSortOrder.SelectedIndex;
      OnChange();
    }

    private void cbxGroupBy_SelectedIndexChanged(object sender, EventArgs e)
    {
      Series.GroupBy = (GroupBy)cbxGroupBy.SelectedIndex;
      udGroupInterval.Enabled = Series.GroupBy != GroupBy.None && Series.GroupBy != GroupBy.XValue;
      lblGroupInterval.Enabled = udGroupInterval.Enabled;
      cbxGroupFunction.Enabled = Series.GroupBy != GroupBy.None;
      lblGroupFunction.Enabled = cbxGroupFunction.Enabled;
      OnChange();
    }

    private void udGroupInterval_Leave(object sender, EventArgs e)
    {
      Series.GroupInterval = (float)udGroupInterval.Value;
      OnChange();
    }

    private void cbxGroupFunction_Leave(object sender, EventArgs e)
    {
      Series.GroupFunction = (TotalType)cbxGroupFunction.SelectedIndex;
      OnChange();
    }

    private void cbxCollectData_SelectedIndexChanged(object sender, EventArgs e)
    {
      pnCollectData.Enabled = cbxCollectData.SelectedIndex != 0;
      if (!FUpdating)
      {
        Series.Collect = (Collect)cbxCollectData.SelectedIndex;
        OnChange();
      }
    }

    private void udCollectValue_Leave(object sender, EventArgs e)
    {
      Series.CollectValue = (int)udCollectValue.Value;
      OnChange();
    }

    private void tbCollectedItemText_Leave(object sender, EventArgs e)
    {
      Series.CollectedItemText = tbCollectedItemText.Text;
      OnChange();
    }

    private void cbxCollectedItemColor_ColorSelected(object sender, EventArgs e)
    {
      Series.CollectedItemColor = cbxCollectedItemColor.Color;
      OnChange();
    }

    private void cbxExplode_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool valueEnabled = cbxExplode.SelectedIndex == 3;
      lblExplodedValue.Enabled = valueEnabled;
      tbExplodedValue.Enabled = valueEnabled;
      if (!valueEnabled)
        tbExplodedValue.Text = "";
      if (!FUpdating)
      {
        Series.PieExplode = (PieExplode)cbxExplode.SelectedIndex;
        if (!valueEnabled)
          Series.PieExplodeValue = "";
        OnChange();
      }
    }

    private void tbExplodedValue_Leave(object sender, EventArgs e)
    {
      Series.PieExplodeValue = tbExplodedValue.Text;
      OnChange();
    }

    private void tbExplodedValue_ButtonClick(object sender, EventArgs e)
    {
      tbExplodedValue.Text = Editors.EditExpression(Report, tbExplodedValue.Text);
    }
    #endregion

    #region Fill & Border tab
    private void cbxPalette_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.Palette = (ChartColorPalette)cbxPalette.SelectedIndex;
      OnChange();
    }

    private void cbxBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.Color = cbxBackColor.Color;
      OnChange();
    }

    private void cbxSecondaryColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BackSecondaryColor = cbxSecondaryColor.Color;
      OnChange();
    }

    private void cbxGradient_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BackGradientStyle = (GradientStyle)cbxGradient.SelectedIndex;
      OnChange();
    }

    private void cbxHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BackHatchStyle = (ChartHatchStyle)cbxHatchStyle.SelectedIndex;
      OnChange();
    }

    private void cbxBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BorderColor = cbxBorderColor.Color;
      OnChange();
    }

    private void cbxBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BorderDashStyle = (ChartDashStyle)cbxBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.BorderWidth = (int)udBorderWidth.Value;
      OnChange();  
    }

    private void cbxShadowColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.ShadowColor = cbxShadowColor.Color;
      OnChange();
    }

    private void udShadowOffset_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.ShadowOffset = (int)udShadowOffset.Value;
      OnChange();
    }
    #endregion

    #region Labels tab
    private void cbxLabelView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      
      string pattern = "";
      switch (cbxLabelView.SelectedIndex)
      {
        case 1:
          pattern = "#VALX";
          break;
        case 2:
          pattern = "#VALY";
          break;
        case 3:
          pattern = "#PERCENT";
          break;
        case 4:
          pattern = "#VALX: #VALY";
          break;
        case 5:
          pattern = "#VALX: #PERCENT";
          break;
      }  

      tbLabelPattern.Text = pattern;
      ChartSeries.Label = pattern;
      OnChange();
    }

    private void tbLabelPattern_Leave(object sender, EventArgs e)
    {
      ChartSeries.Label = tbLabelPattern.Text;
      OnChange();
    }

    private void tbLabelFormat_Leave(object sender, EventArgs e)
    {
      ChartSeries.LabelFormat = tbLabelFormat.Text;
      OnChange();
    }

    private void tbLabelFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = ChartSeries.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ChartSeries.Font = dialog.Font;
          tbLabelFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxLabelForeColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.LabelForeColor = cbxLabelForeColor.Color;
      OnChange();
    }

    private void cbxLabelBackColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.LabelBackColor = cbxLabelBackColor.Color;
      OnChange();
    }

    private void cbxLabelBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.LabelBorderColor = cbxLabelBorderColor.Color;
      OnChange();
    }

    private void cbxLabelBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.LabelBorderDashStyle = (ChartDashStyle)cbxLabelBorderStyle.SelectedIndex;
    }

    private void udLabelBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.LabelBorderWidth = (int)udLabelBorderWidth.Value;
      OnChange();
    }
    #endregion

    #region Markers tab
    private void cbxMarkerStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerStyle = (System.Windows.Forms.DataVisualization.Charting.MarkerStyle)cbxMarkerStyle.SelectedIndex;
      OnChange();
    }

    private void udMarkerSize_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerSize = (int)udMarkerSize.Value;
      OnChange();
    }

    private void udMarkerStep_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerStep = (int)udMarkerStep.Value;
      OnChange();
    }

    private void cbxMarkerColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerColor = cbxMarkerColor.Color;
      OnChange();
    }

    private void cbxMarkerBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerBorderColor = cbxMarkerBorderColor.Color;
      OnChange();
    }

    private void udMarkerBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      ChartSeries.MarkerBorderWidth = (int)udMarkerBorderWidth.Value;
      OnChange();
    }
    #endregion

    private void cbxAutoSeriesData_Leave(object sender, EventArgs e)
    {
      Series.AutoSeriesColumn = cbxAutoSeriesData.Text;
      OnChange();
    }

    private void cbAutoSeriesForce_Leave(object sender, EventArgs e)
    {
      Series.AutoSeriesForce = cbAutoSeriesForce.Checked;
      OnChange();
    }
  }
}
