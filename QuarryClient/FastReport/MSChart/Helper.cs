using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using FastReport.Controls;
using FastReport.Utils;
using System.Globalization;

namespace FastReport.MSChart
{
  internal static class Helper
  {
    private static void AssignGridAppearance(Grid destGrid, Grid srcGrid)
    {
      destGrid.LineColor = srcGrid.LineColor;
      destGrid.LineDashStyle = srcGrid.LineDashStyle;
      destGrid.LineWidth = srcGrid.LineWidth;
    }

    private static void AssignTickMarkAppearance(TickMark destTick, TickMark srcTick)
    {
      destTick.LineColor = srcTick.LineColor;
      destTick.LineDashStyle = srcTick.LineDashStyle;
      destTick.LineWidth = srcTick.LineWidth;
      destTick.Size = srcTick.Size;
      destTick.TickMarkStyle = srcTick.TickMarkStyle;
    }

    public static void AssignAxisAppearance(Axis destAxis, Axis srcAxis)
    {
      destAxis.LineColor = srcAxis.LineColor;
      destAxis.LineDashStyle = srcAxis.LineDashStyle;
      destAxis.LineWidth = srcAxis.LineWidth;
      
      destAxis.LabelStyle.Font = srcAxis.LabelStyle.Font.Clone() as Font;
      destAxis.LabelStyle.ForeColor = srcAxis.LabelStyle.ForeColor;

      AssignGridAppearance(destAxis.MajorGrid, srcAxis.MajorGrid);
      AssignGridAppearance(destAxis.MinorGrid, srcAxis.MinorGrid);
      AssignTickMarkAppearance(destAxis.MajorTickMark, srcAxis.MajorTickMark);
      AssignTickMarkAppearance(destAxis.MinorTickMark, srcAxis.MinorTickMark);
    }

    public static void AssignChartAreaAppearance(ChartArea destArea, ChartArea srcArea)
    {
      AssignAxisAppearance(destArea.AxisX, srcArea.AxisX);
      AssignAxisAppearance(destArea.AxisX2, srcArea.AxisX2);
      AssignAxisAppearance(destArea.AxisY, srcArea.AxisY);
      AssignAxisAppearance(destArea.AxisY2, srcArea.AxisY2);
      
      destArea.BackColor = srcArea.BackColor;
      destArea.BackSecondaryColor = srcArea.BackSecondaryColor;
      destArea.BackGradientStyle = srcArea.BackGradientStyle;
      destArea.BackHatchStyle = srcArea.BackHatchStyle;
      
      destArea.BorderColor = srcArea.BorderColor;
      destArea.BorderDashStyle = srcArea.BorderDashStyle;
      destArea.BorderWidth = srcArea.BorderWidth;
      
      destArea.ShadowColor = srcArea.ShadowColor;
      destArea.ShadowOffset = srcArea.ShadowOffset;
    }
    
    private static void AssignLegendAppearance(Legend destLegend, Legend srcLegend)
    {
      destLegend.BackColor = srcLegend.BackColor;
      destLegend.BackSecondaryColor = srcLegend.BackSecondaryColor;
      destLegend.BackGradientStyle = srcLegend.BackGradientStyle;
      destLegend.BackHatchStyle = srcLegend.BackHatchStyle;
      destLegend.BorderColor = srcLegend.BorderColor;
      destLegend.BorderDashStyle = srcLegend.BorderDashStyle;
      destLegend.BorderWidth = srcLegend.BorderWidth;
    }

    public static void AssignChartAppearance(Chart destChart, Chart srcChart)
    {
      destChart.BackColor = srcChart.BackColor;
      destChart.BackSecondaryColor = srcChart.BackSecondaryColor;
      destChart.BackGradientStyle = srcChart.BackGradientStyle;
      destChart.BackHatchStyle = srcChart.BackHatchStyle;
      destChart.BorderSkin.SkinStyle = srcChart.BorderSkin.SkinStyle;
      destChart.BorderlineColor = srcChart.BorderlineColor;
      destChart.BorderlineDashStyle = srcChart.BorderlineDashStyle;
      destChart.BorderlineWidth = srcChart.BorderlineWidth;
      destChart.Palette = srcChart.Palette;

      if (srcChart.ChartAreas.Count > 0)
      {
        ChartArea srcChartDefaultArea = srcChart.ChartAreas[0];
        for (int i = 0; i < destChart.ChartAreas.Count; i++)
        {
          ChartArea srcChartArea = srcChartDefaultArea;
          if (i < srcChart.ChartAreas.Count)
            srcChartArea = srcChart.ChartAreas[i];
          AssignChartAreaAppearance(destChart.ChartAreas[i], srcChartArea);  
        }
      }
      
      if (srcChart.Legends.Count > 0 && destChart.Legends.Count > 0)
      {
        AssignLegendAppearance(destChart.Legends[0], srcChart.Legends[0]);
      }
    }

    public static string[] GetYValuesNames(SeriesChartType chartType)
    {
      MyRes res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Data,SeriesValues");
      switch (chartType)
      {
        case SeriesChartType.Stock:
        case SeriesChartType.Candlestick:
          return new string[] { res.Get("High"), res.Get("Low"), res.Get("Open"), res.Get("Close") };

        case SeriesChartType.Range:
        case SeriesChartType.RangeBar:
        case SeriesChartType.RangeColumn:
        case SeriesChartType.SplineRange:
          return new string[] { res.Get("RangeBegin"), res.Get("RangeEnd") };

        case SeriesChartType.Bubble:
          return new string[] { res.Get("YValue"), res.Get("Size") };

        case SeriesChartType.PointAndFigure:
          return new string[] { res.Get("YValue1"), res.Get("YValue2") };
      }

      return new string[] { res.Get("YValue") };
    }
    
    private static Control controlParent;
    private static Series editedSeries;
    private static EventHandler changeEventHandler;
    private static int top;
    private static float scaleFactor;
    
    private static void AddLabel(string text)
    {
      Label label = new Label();
      label.Parent = controlParent;
      label.Text = text;
      label.Location = new Point(16, top + 4);
      label.AutoSize = true;
    }
    
    private static void AddComboBox(string propertyName, bool isDropDownList, string values)
    {
      ComboBox comboBox = new ComboBox();
      comboBox.Parent = controlParent;
      comboBox.Location = new Point(128, top);
      comboBox.Size = new Size(172, 20);
      comboBox.Items.AddRange(values.Split(new char[] { ';' }));
      comboBox.Items.Insert(0, Res.Get("Forms,ChartEditor,SeriesEditorControl,Appearance,Default"));
      comboBox.DropDownStyle = isDropDownList ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;
      comboBox.Tag = propertyName;
      
      string editedValue = editedSeries[propertyName];
      if (String.IsNullOrEmpty(editedValue))
      {
        comboBox.SelectedIndex = 0;
      }
      else
      {
        if (isDropDownList)
          comboBox.SelectedIndex = comboBox.Items.IndexOf(editedValue);
        else
          comboBox.Text = editedValue;
      }
      
      if (isDropDownList)
        comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
      else
        comboBox.TextChanged += new EventHandler(comboBox_SelectedIndexChanged);
      
      top += 28;
    }

    private static void AddCheckBox(string propertyName, string text)
    {
      CheckBox checkBox = new CheckBox();
      checkBox.Parent = controlParent;
      checkBox.Location = new Point(16, top);
      checkBox.Tag = propertyName;
      checkBox.Text = text;
      checkBox.AutoSize = true;

      string editedValue = editedSeries[propertyName];
      checkBox.Checked = String.Compare(editedValue, "true", true) == 0;
      checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);

      top += 28;
    }
    
    private static void AddNumericUpDown(string propertyName, int minValue, int maxValue)
    {
      NumericUpDown upDown = new NumericUpDown();
      upDown.Parent = controlParent;
      upDown.Location = new Point(128, top);
      upDown.Size = new Size(60, 20);
      upDown.Tag = propertyName;
      upDown.Minimum = minValue;
      upDown.Maximum = maxValue;

      string editedValue = editedSeries[propertyName];
      upDown.Value = String.IsNullOrEmpty(editedValue) ? 
        minValue : Math.Max(Math.Min(int.Parse(editedValue), maxValue), minValue);
      upDown.ValueChanged += new EventHandler(upDown_ValueChanged);  

      top += 28;
    }
    
    private static void AddColorComboBox(string propertyName)
    {
      ColorComboBox comboBox = new ColorComboBox();
      comboBox.Parent = controlParent;
      comboBox.Location = new Point(128, top);
      comboBox.Size = new Size(172, 20);
      comboBox.Tag = propertyName;
      comboBox.ShowColorName = true;

      string editedValue = editedSeries[propertyName];
      if (!String.IsNullOrEmpty(editedValue))
        comboBox.Color = (Color)Converter.FromString(typeof(Color), editedValue);

      comboBox.ColorSelected += new EventHandler(comboBox_ColorSelected);
      top += 28;
    }

    private static void comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = sender as ComboBox;
      string propertyName = comboBox.Tag.ToString();
      
      if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
      {
        if (comboBox.SelectedIndex == 0)
          editedSeries.DeleteCustomProperty(propertyName);
        else
          editedSeries[propertyName] = comboBox.SelectedItem.ToString();
      }
      else
      {
        if (comboBox.Text == comboBox.Items[0].ToString())
          editedSeries.DeleteCustomProperty(propertyName);
        else
          editedSeries[propertyName] = comboBox.Text;
      }
      
      if (changeEventHandler != null)
        changeEventHandler(sender, e);
    }

    private static void checkBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = sender as CheckBox;
      string propertyName = checkBox.Tag.ToString();
      editedSeries[propertyName] = checkBox.Checked ? "true" : "false";

      if (changeEventHandler != null)
        changeEventHandler(sender, e);
    }

    private static void upDown_ValueChanged(object sender, EventArgs e)
    {
      NumericUpDown upDown = sender as NumericUpDown;
      string propertyName = upDown.Tag.ToString();
      editedSeries[propertyName] = upDown.Value.ToString();

      if (changeEventHandler != null)
        changeEventHandler(sender, e);
    }

    private static void comboBox_ColorSelected(object sender, EventArgs e)
    {
      ColorComboBox comboBox = sender as ColorComboBox;
      string propertyName = comboBox.Tag.ToString();
      if (comboBox.Color == Color.Transparent)
        editedSeries.DeleteCustomProperty(propertyName);
      else
        editedSeries[propertyName] = new ColorConverter().ConvertToString(null,
          CultureInfo.InvariantCulture, comboBox.Color);

      if (changeEventHandler != null)
        changeEventHandler(sender, e);
    }

    public static void ConstructCustomPropertiesEditor(Control parent, Series series, 
      EventHandler changeEvent)
    {
      controlParent = parent;
      editedSeries = series;
      changeEventHandler = changeEvent;
      top = 16;
      
      while (parent.Controls.Count > 0)
      {
        parent.Controls[0].Dispose();
      }

      MyRes res = new MyRes("Forms,ChartEditor,SeriesEditorControl,Appearance");
      switch (series.ChartType)
      {
        case SeriesChartType.Bar:
        case SeriesChartType.StackedBar:
        case SeriesChartType.StackedBar100:
        case SeriesChartType.Column:
        case SeriesChartType.StackedColumn:
        case SeriesChartType.StackedColumn100:
        case SeriesChartType.RangeColumn:
        case SeriesChartType.RangeBar:
          AddLabel(res.Get("DrawingStyle"));
          AddComboBox("DrawingStyle", true, "Emboss;Cylinder;Wedge;LightToDark");
          AddLabel(res.Get("PointWidth"));
          AddComboBox("PointWidth", false, "0.4;0.6;0.8;1.0");
          if (series.ChartType == SeriesChartType.Bar || 
            series.ChartType == SeriesChartType.StackedBar ||
            series.ChartType == SeriesChartType.StackedBar100 ||
            series.ChartType == SeriesChartType.RangeBar)
          {  
            AddLabel(res.Get("LabelsStyle"));
            AddComboBox("BarLabelStyle", true, "Left;Right;Center;Outside");
          }
          if (series.ChartType == SeriesChartType.RangeColumn)
            AddCheckBox("DrawSideBySide", res.Get("DrawSideBySide"));
          break;

        case SeriesChartType.Area:
        case SeriesChartType.StackedArea:
        case SeriesChartType.StackedArea100:
          AddCheckBox("ShowMarkerLines", res.Get("ShowMarkerLines"));
          break;

        case SeriesChartType.Pie:
        case SeriesChartType.Doughnut:
          if (series.ChartType == SeriesChartType.Doughnut)
          {
            AddLabel(res.Get("DoughnutRadius"));
            AddNumericUpDown("DoughnutRadius", 0, 99);
          }
          AddLabel(res.Get("LabelsStyle"));
          AddComboBox("PieLabelStyle", true, "Inside;Outside;Disabled");
          AddLabel(Res.Get("Forms,ChartEditor,Common,LineColor"));
          AddColorComboBox("PieLineColor");
          AddLabel(res.Get("DrawingStyle"));
          AddComboBox("PieDrawingStyle", true, "SoftEdge;Concave");
          break;
          
        case SeriesChartType.Polar:
        case SeriesChartType.Radar:
          AddLabel(res.Get("DrawingStyle"));
          if (series.ChartType == SeriesChartType.Polar)
            AddComboBox("PolarDrawingStyle", true, "Marker;Line");
          else
            AddComboBox("RadarDrawingStyle", true, "Area;Marker;Line");
          AddLabel(res.Get("AreaDrawingStyle"));
          AddComboBox("AreaDrawingStyle", true, "Circle;Polygon");
          AddLabel(res.Get("LabelsStyle"));
          AddComboBox("CircularLabelsStyle", true, "Horizontal;Circular;Radial");
          break;
          
        case SeriesChartType.Stock:
        case SeriesChartType.Candlestick:
          AddLabel(res.Get("Markers"));
          AddComboBox("ShowOpenClose", true, "Open;Close;Both");  
          break;
          
        case SeriesChartType.Kagi:
        case SeriesChartType.Renko:
        case SeriesChartType.PointAndFigure:
        case SeriesChartType.ThreeLineBreak:
          AddLabel(res.Get("PriceUpColor"));
          AddColorComboBox("PriceUpColor");
          if (series.ChartType == SeriesChartType.Kagi || 
            series.ChartType == SeriesChartType.PointAndFigure)
          {
            AddLabel(res.Get("ReversalAmount"));
            AddComboBox("ReversalAmount", false, "0.6;0.8;1;1.2;1%;2%;4%");
          }
          if (series.ChartType == SeriesChartType.Renko || 
            series.ChartType == SeriesChartType.PointAndFigure)
          {
            AddLabel(res.Get("BoxSize"));
            AddComboBox("BoxSize", false, "0.2;0.5;0.8;1;1.5;2");
          }
          if (series.ChartType == SeriesChartType.ThreeLineBreak)
          {
            AddLabel(res.Get("NumberOfLinesInBreak"));
            AddComboBox("NumberOfLinesInBreak", false, "1;2;3;4");
          }
          break;
          
        case SeriesChartType.Pyramid:
          AddLabel(res.Get("LabelsStyle"));
          AddComboBox("PyramidLabelStyle", true, "OutsideInColumn;Outside;Inside;Disabled");
          AddLabel(res.Get("LabelsPlacement"));
          AddComboBox("PyramidOutsideLabelPlacement", true, "Left;Right");
          AddLabel(res.Get("PointsGap"));
          AddNumericUpDown("PyramidPointGap", 0, 1000);
          AddLabel(res.Get("MinPointHeight"));
          AddNumericUpDown("PyramidMinPointHeight", 0, 1000);
          AddLabel(res.Get("ValueType"));
          AddComboBox("PyramidValueType", true, "Linear;Surface");
          AddLabel(res.Get("ThreeDDrawingStyle"));
          AddComboBox("Pyramid3DDrawingStyle", true, "CircularBase;SquareBase");
          AddLabel(res.Get("ThreeDRotation"));
          AddNumericUpDown("Pyramid3DRotationAngle", -10, 10);
          break;

        case SeriesChartType.Funnel:
          AddLabel(res.Get("FunnelStyle"));
          AddComboBox("FunnelStyle", true, "YIsHeight;YIsWidth");
          AddLabel(res.Get("LabelsStyle"));
          AddComboBox("FunnelLabelStyle", true, "OutsideInColumn;Outside;Inside;Disabled");
          AddLabel(res.Get("LabelsPlacement"));
          AddComboBox("FunnelOutsideLabelPlacement", true, "Left;Right");
          AddLabel(res.Get("PointsGap"));
          AddNumericUpDown("FunnelPointGap", 0, 1000);
          AddLabel(res.Get("MinPointHeight"));
          AddNumericUpDown("FunnelMinPointHeight", 0, 1000);
          AddLabel(res.Get("ThreeDDrawingStyle"));
          AddComboBox("Funnel3DDrawingStyle", true, "CircularBase;SquareBase");
          AddLabel(res.Get("ThreeDRotation"));
          AddNumericUpDown("Funnel3DRotationAngle", -10, 10);
          break;

        case SeriesChartType.Spline:
        case SeriesChartType.SplineRange:
        case SeriesChartType.SplineArea:
          AddLabel(res.Get("LineTension"));
          AddComboBox("LineTension", false, "0.2;0.4;0.8;1.2");
          if (series.ChartType == SeriesChartType.SplineArea)
            AddCheckBox("ShowMarkerLines", res.Get("ShowMarkerLines"));
          break;
          
        default:
          AddLabel(res.Get("NoSettings"));
          break;
      }
      
      parent.Scale(new SizeF(scaleFactor, scaleFactor));
    }
    
    static Helper()
    {
      using (Bitmap bmp = new Bitmap(1, 1))
      using (Graphics g = Graphics.FromImage(bmp))
      {
        scaleFactor = g.DpiX / 96f;
      }
    }
  }
}
