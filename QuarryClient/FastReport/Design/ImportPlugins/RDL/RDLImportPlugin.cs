using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastReport.MSChart;
using FastReport.Table;
using FastReport.Matrix;

namespace FastReport.Design.ImportPlugins.RDL
{
    /// <summary>
    /// Represents the RDL import plugin.
    /// </summary>
    public class RDLImportPlugin : ImportPlugin
    {
        #region Fields

        private ReportPage page;
        private ComponentBase component;
        private Base parent;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RDLImportPlugin"/> class.
        /// </summary>
        public RDLImportPlugin() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RDLImportPlugin"/> class with a specified designer.
        /// </summary>
        /// <param name="designer">The report designer.</param>
        public RDLImportPlugin(Designer designer) : base(designer)
        {
        }

        #endregion // Constructors

        #region Private Methods

        private void LoadBorderColor(XmlNode borderColorNode)
        {
            XmlNodeList nodeList = borderColorNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BorderlineColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                    else if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Color = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "Top")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.TopLine.Color = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "Left")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.LeftLine.Color = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "Right")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.RightLine.Color = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "Bottom")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.BottomLine.Color = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
            }
        }

        private void LoadBorderStyle(XmlNode borderStyleNode)
        {
            XmlNodeList nodeList = borderStyleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BorderlineDashStyle = UnitsConverter.ConvertBorderStyleToChartDashStyle(node.InnerText);
                    }
                    else if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Lines = BorderLines.All;
                        (component as ReportComponentBase).Border.Style = UnitsConverter.ConvertBorderStyle(node.InnerText);
                    }
                }
                else if (node.Name == "Top")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Lines |= BorderLines.Top;
                        (component as ReportComponentBase).Border.TopLine.Style = UnitsConverter.ConvertBorderStyle(node.InnerText);
                    }
                }
                else if (node.Name == "Left")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Lines |= BorderLines.Left;
                        (component as ReportComponentBase).Border.LeftLine.Style = UnitsConverter.ConvertBorderStyle(node.InnerText);
                    }
                }
                else if (node.Name == "Right")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Lines |= BorderLines.Right;
                        (component as ReportComponentBase).Border.RightLine.Style = UnitsConverter.ConvertBorderStyle(node.InnerText);
                    }
                }
                else if (node.Name == "Bottom")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Lines |= BorderLines.Bottom;
                        (component as ReportComponentBase).Border.BottomLine.Style = UnitsConverter.ConvertBorderStyle(node.InnerText);
                    }
                }
            }
        }

        private void LoadBorderWidth(XmlNode borderWidthNode)
        {
            XmlNodeList nodeList = borderWidthNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BorderlineWidth = (int)UnitsConverter.SizeToPixels(node.InnerText);
                    }
                    else if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.Width = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                }
                else if (node.Name == "Top")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.TopLine.Width = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                }
                else if (node.Name == "Left")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.LeftLine.Width = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                }
                else if (node.Name == "Right")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.RightLine.Width = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                }
                else if (node.Name == "Bottom")
                {
                    if (component is ReportComponentBase)
                    {
                        (component as ReportComponentBase).Border.BottomLine.Width = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                }
            }
        }

        private void LoadStyle(XmlNode styleNode)
        {
            FontStyle fontStyle = FontStyle.Regular;
            string fontFamily = "Arial";
            float fontSize = 10.0f;
            int paddingTop = 0;
            int paddingLeft = 2;
            int paddingRight = 2;
            int paddingBottom = 0;
            XmlNodeList nodeList = styleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "BorderColor")
                {
                    LoadBorderColor(node);
                }
                else if (node.Name == "BorderStyle")
                {
                    LoadBorderStyle(node);
                }
                else if (node.Name == "BorderWidth")
                {
                    LoadBorderWidth(node);
                }
                else if (node.Name == "BackgroundColor")
                {
                    if (component is ShapeObject)
                    {
                        (component as ShapeObject).FillColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                    else if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BackColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                    else if (component is TableObject)
                    {
                        (component as TableObject).FillColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                    //else if (component is SubreportObject)
                    //{
                    //    (component as SubreportObject).FillColor = UnitsConverter.ConvertColor(node.InnerText);
                    //}
                }
                else if (node.Name == "BackgroundGradientType")
                {
                    if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BackGradientStyle = UnitsConverter.ConvertGradientType(node.InnerText);
                    }
                }
                else if (node.Name == "BackgroundGradientEndColor")
                {
                    if (component is MSChartObject)
                    {
                        (component as MSChartObject).Chart.BackSecondaryColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "FontStyle")
                {
                    fontStyle = UnitsConverter.ConvertFontStyle(node.InnerText);
                }
                else if (node.Name == "FontFamily")
                {
                    fontFamily = node.InnerText;
                }
                else if (node.Name == "FontSize")
                {
                    fontSize = Convert.ToSingle(UnitsConverter.ConvertFontSize(node.InnerText));
                }
                else if (node.Name == "TextAlign")
                {
                    if (component is TextObject)
                    {
                        (component as TextObject).HorzAlign = UnitsConverter.ConvertTextAlign(node.InnerText);
                    }
                }
                else if (node.Name == "VerticalAlign")
                {
                    if (component is TextObject)
                    {
                        (component as TextObject).VertAlign = UnitsConverter.ConvertVerticalAlign(node.InnerText);
                    }
                }
                else if (node.Name == "WritingMode")
                {
                    if (component is TextObject)
                    {
                        (component as TextObject).Angle = UnitsConverter.ConvertWritingMode(node.InnerText);
                    }
                }
                else if (node.Name == "Color")
                {
                    if (component is TextObject)
                    {
                        (component as TextObject).TextColor = UnitsConverter.ConvertColor(node.InnerText);
                    }
                }
                else if (node.Name == "PaddingLeft")
                {
                    paddingLeft = UnitsConverter.SizeToInt(node.InnerText, SizeUnits.Point);
                }
                else if (node.Name == "PaddingRight")
                {
                    paddingRight = UnitsConverter.SizeToInt(node.InnerText, SizeUnits.Point);
                }
                else if (node.Name == "PaddingTop")
                {
                    paddingTop = UnitsConverter.SizeToInt(node.InnerText, SizeUnits.Point);
                }
                else if (node.Name == "PaddingBottom")
                {
                    paddingBottom = UnitsConverter.SizeToInt(node.InnerText, SizeUnits.Point);
                }
            }
            if (component is TextObject)
            {
                (component as TextObject).Font = new Font(fontFamily, fontSize, fontStyle);
                (component as TextObject).Padding = new Padding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
            else if (component is PictureObject)
            {
                (component as PictureObject).Padding = new Padding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
            else if (component is MSChartObject)
            {
                (component as MSChartObject).Chart.Padding = new Padding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
        }

        private void LoadBorderColorT(XmlNode borderColorNode, Title title)
        {
            XmlNodeList nodeList = borderColorNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    title.BorderColor = UnitsConverter.ConvertColor(node.InnerText);
                }
            }
        }

        private void LoadBorderStyleT(XmlNode borderStyleNode, Title title)
        {
            XmlNodeList nodeList = borderStyleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    title.BorderDashStyle = UnitsConverter.ConvertBorderStyleToChartDashStyle(node.InnerText);
                }
            }
        }

        private void LoadBorderWidthT(XmlNode borderWidthNode, Title title)
        {
            XmlNodeList nodeList = borderWidthNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Default")
                {
                    title.BorderWidth = (int)UnitsConverter.SizeToPixels(node.InnerText);
                }
            }
        }

        private void LoadStyleT(XmlNode styleNode, Title title)
        {
            FontStyle fontStyle = FontStyle.Regular;
            string fontFamily = "Arial";
            float fontSize = 10.0f;
            string textAlign = "";
            string vertAlign = "";
            XmlNodeList nodeList = styleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "BorderColor")
                {
                    LoadBorderColorT(node, title);
                }
                else if (node.Name == "BorderStyle")
                {
                    LoadBorderStyleT(node, title);
                }
                else if (node.Name == "BorderWidth")
                {
                    LoadBorderWidthT(node, title);
                }
                else if (node.Name == "BackgroundColor")
                {
                    title.BackColor = UnitsConverter.ConvertColor(node.InnerText);
                }
                else if (node.Name == "BackgroundGradientType")
                {
                    title.BackGradientStyle = UnitsConverter.ConvertGradientType(node.InnerText);
                }
                else if (node.Name == "BackgroundGradientEndColor")
                {
                    title.BackSecondaryColor = UnitsConverter.ConvertColor(node.InnerText);
                }
                else if (node.Name == "FontStyle")
                {
                    fontStyle = UnitsConverter.ConvertFontStyle(node.InnerText);
                }
                else if (node.Name == "FontFamily")
                {
                    fontFamily = node.InnerText;
                }
                else if (node.Name == "FontSize")
                {
                    fontSize = Convert.ToSingle(UnitsConverter.ConvertFontSize(node.InnerText));
                }
                else if (node.Name == "TextAlign")
                {
                    textAlign = node.InnerText;
                }
                else if (node.Name == "VerticalAlign")
                {
                    vertAlign = node.InnerText;
                }
                else if (node.Name == "Color")
                {
                    title.ForeColor = UnitsConverter.ConvertColor(node.InnerText);
                }
            }
            title.Font = new Font(fontFamily, fontSize, fontStyle);
            title.Alignment = UnitsConverter.ConvertTextAndVerticalAlign(textAlign, vertAlign);
        }

        private void LoadVisibility(XmlNode visibilityNode)
        {
            XmlNodeList nodeList = visibilityNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Hidden")
                {
                    component.Visible = !UnitsConverter.BooleanToBool(node.InnerText);
                }
            }
        }

        private void LoadDataPoint(XmlNode dataPointNode)
        {
            XmlNodeList nodeList = dataPointNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
            }
        }

        private void LoadDataPoints(XmlNode dataPointsNode)
        {
            XmlNodeList nodeList = dataPointsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "DataPoint")
                {
                    LoadDataPoint(node);
                }
            }
        }

        private void LoadChartSeries(XmlNode chartSeriesNode)
        {
            XmlNodeList nodeList = chartSeriesNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "DataPoints")
                {
                    LoadDataPoints(node);
                }
            }
        }

        private void LoadChartData(XmlNode chartDataNode)
        {
            XmlNodeList nodeList = chartDataNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ChartSeries")
                {
                    LoadChartSeries(node);
                }
            }
        }

        private void LoadLegend(XmlNode legendNode)
        {
            Legend legend = (component as MSChartObject).Chart.Legends[0];
            XmlNodeList nodeList = legendNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Visible")
                {
                    legend.Enabled = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
                else if (node.Name == "Position")
                {
                    UnitsConverter.ConvertChartLegendPosition(node.InnerText, legend);
                }
                else if (node.Name == "Layout")
                {
                    legend.LegendStyle = UnitsConverter.ConvertChartLegendLayout(node.InnerText);
                }
                else if (node.Name == "InsidePlotArea")
                {
                    legend.IsDockedInsideChartArea = UnitsConverter.BooleanToBool(node.InnerText);
                }
            }
        }

        private void LoadTitleA(XmlNode titleNode, Axis axis)
        {
            XmlNodeList nodeList = titleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Caption")
                {
                    axis.Title = node.InnerText;
                }
                else if (node.Name == "Style")
                {
                    string fontFamily = "Arial";
                    float fontSize = 10.0f;
                    FontStyle fontStyle = FontStyle.Regular;
                    foreach (XmlNode styleNode in node.ChildNodes)
                    {
                        if (styleNode.Name == "FontStyle")
                        {
                            fontStyle = UnitsConverter.ConvertFontStyle(styleNode.InnerText);
                        }
                        else if (styleNode.Name == "FontFamily")
                        {
                            fontFamily = styleNode.InnerText;
                        }
                        else if (styleNode.Name == "FontSize")
                        {
                            fontSize = Convert.ToSingle(UnitsConverter.ConvertFontSize(styleNode.InnerText));
                        }
                        else if (styleNode.Name == "TextAlign")
                        {
                            axis.TitleAlignment = UnitsConverter.ConvertTextAlignToStringAlignment(styleNode.InnerText);
                        }
                        else if (styleNode.Name == "Color")
                        {
                            axis.TitleForeColor = UnitsConverter.ConvertColor(styleNode.InnerText);
                        }
                    }
                    axis.TitleFont = new Font(fontFamily, fontSize, fontStyle);
                }
            }
        }

        private void LoadGridLines(XmlNode gridLinesNode, Grid grid)
        {
            XmlNodeList nodeList = gridLinesNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ShowGridLines")
                {
                    grid.Enabled = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "Style")
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.Name == "BorderStyle")
                        {
                            grid.LineDashStyle = UnitsConverter.ConvertBorderStyleToChartDashStyle(subNode.FirstChild.InnerText);
                        }
                        else if (subNode.Name == "BorderColor")
                        {
                            grid.LineColor = UnitsConverter.ConvertColor(subNode.FirstChild.InnerText);
                        }
                        else if (subNode.Name == "BorderWidth")
                        {
                            grid.LineWidth = (int)UnitsConverter.SizeToPixels(subNode.FirstChild.InnerText);
                        }
                    }
                }
            }
        }

        private void LoadAxis(XmlNode axisNode, Axis axis)
        {
            XmlNodeList nodeList = axisNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Visible")
                {
                    axis.Enabled = UnitsConverter.ConvertAxisVisibleToAxisEnabled(node.InnerText);
                }
                else if (node.Name == "Title")
                {
                    LoadTitleA(node, axis);
                }
                else if (node.Name == "Margin")
                {
                    axis.IsMarginVisible = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "MajorTickMarks")
                {
                    axis.MajorTickMark.TickMarkStyle = UnitsConverter.ConvertTickMarkStyle(node.InnerText);
                }
                else if (node.Name == "MinorTickMarks")
                {
                    axis.MinorTickMark.TickMarkStyle = UnitsConverter.ConvertTickMarkStyle(node.InnerText);
                }
                else if (node.Name == "MajorGridLines")
                {
                    LoadGridLines(node, axis.MajorGrid);
                }
                else if (node.Name == "MinorGridLines")
                {
                    LoadGridLines(node, axis.MinorGrid);
                }
                else if (node.Name == "Reverse")
                {
                    axis.IsReversed = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "Interlaced")
                {
                    axis.IsInterlaced = UnitsConverter.BooleanToBool(node.InnerText);
                }
            }
        }

        private void LoadCategoryAxis(XmlNode categoryAxisNode)
        {
            XmlNodeList nodeList = categoryAxisNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Axis")
                {
                    LoadAxis(node, (component as MSChartObject).Chart.ChartAreas[0].AxisX);
                }
            }
        }

        private void LoadValueAxis(XmlNode valueAxisNode)
        {
            XmlNodeList nodeList = valueAxisNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Axis")
                {
                    LoadAxis(node, (component as MSChartObject).Chart.ChartAreas[0].AxisY);
                }
            }
        }

        private void LoadTitle(XmlNode titleNode)
        {
            Title title = (component as MSChartObject).Chart.Titles[0];
            XmlNodeList nodeList = titleNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Caption")
                {
                    title.Text = node.InnerText;
                }
                else if (node.Name == "Style")
                {
                    LoadStyleT(node, title);
                }
            }
        }

        private void LoadThreeDProperties(XmlNode threeDPropertiesNode)
        {
            XmlNodeList nodeList = threeDPropertiesNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Enabled")
                {
                    (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Enable3D = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "Rotation")
                {
                    int rotation = Convert.ToInt32(node.InnerText);
                    if (rotation >= -180 && rotation <= 180)
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Rotation = rotation;
                    }
                    else
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Rotation = 30;
                    }
                }
                else if (node.Name == "Inclination")
                {
                    int inclination = Convert.ToInt32(node.InnerText);
                    if (inclination >= -90 && inclination <= 90)
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Inclination = inclination;
                    }
                    else
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Inclination = 30;
                    }
                }
                else if (node.Name == "Perspective")
                {
                    int perspective = Convert.ToInt32(node.InnerText);
                    if (perspective >= 0 && perspective <= 60)
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Perspective = perspective;
                    }
                    else
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.Perspective = 0;
                    }
                }
                else if (node.Name == "DepthRatio")
                {
                    (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.PointDepth = Convert.ToInt32(node.InnerText);
                }
                else if (node.Name == "Shading")
                {
                    (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.LightStyle = UnitsConverter.ConvertShading(node.InnerText);
                }
                else if (node.Name == "GapDepth")
                {
                    (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.PointGapDepth = Convert.ToInt32(node.InnerText);
                }
                else if (node.Name == "WallThickness")
                {
                    int width = Convert.ToInt32(node.InnerText);
                    if (width < 0)
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.WallWidth = 0;
                    }
                    else if (width > 30)
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.WallWidth = 30;
                    }
                    else
                    {
                        (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.WallWidth = width;
                    }
                }
                else if (node.Name == "Clustered")
                {
                    (component as MSChartObject).Chart.ChartAreas[0].Area3DStyle.IsClustered = UnitsConverter.BooleanToBool(node.InnerText);
                }
            }
        }

        private void LoadPlotArea(XmlNode plotAreaNode)
        {
            XmlNodeList nodeList = plotAreaNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
            }
        }

        private void LoadTableColumn(XmlNode tableColumnNode, TableColumn column)
        {
            XmlNodeList nodeList = tableColumnNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Width")
                {
                    column.Width = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
            }
        }

        private void LoadTableColumns(XmlNode tableColumnsNode)
        {
            if (tableColumnsNode != null)
            {
                XmlNodeList nodeList = tableColumnsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "TableColumn")
                    {
                        if (component is TableObject)
                        {
                            TableColumn column = new TableColumn();
                            (component as TableObject).Columns.Add(column);
                            LoadTableColumn(node, column);
                        }
                    }
                }
            }
        }

        private void LoadTableCell(XmlNode tableCellNode, ref int col)
        {
            int row = (component as TableObject).RowCount - 1;
            XmlNodeList nodeList = tableCellNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    Base tempParent = parent;
                    ComponentBase tempComponent = component;
                    parent = (component as TableObject).GetCellData(col, row).Cell;
                    LoadReportItems(node);
                    component = tempComponent;
                    parent = tempParent;
                }
                else if (node.Name == "ColSpan")
                {
                    int colSpan = Convert.ToInt32(node.InnerText);
                    (component as TableObject).GetCellData(col, row).Cell.ColSpan = colSpan;
                    col += colSpan - 1;
                }
            }
        }

        private void LoadTableCells(XmlNode tableCellsNode)
        {
            int col = 0;
            XmlNodeList nodeList = tableCellsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "TableCell")
                {
                    LoadTableCell(node, ref col);
                    col++;
                }
            }
        }

        private void LoadTableRow(XmlNode tableRowNode, TableRow row)
        {
            XmlNodeList nodeList = tableRowNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "TableCells")
                {
                    LoadTableCells(node);
                }
                else if (node.Name == "Height")
                {
                    row.Height = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
            }
        }

        private void LoadTableRows(XmlNode tableRowsNode)
        {
            XmlNodeList nodeList = tableRowsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "TableRow")
                {
                    if (component is TableObject)
                    {
                        TableRow row = new TableRow();
                        (component as TableObject).Rows.Add(row);
                        LoadTableRow(node, row);
                    }
                }
            }
        }

        private void LoadHeader(XmlNode headerNode)
        {
            if (headerNode != null)
            {
                XmlNodeList nodeList = headerNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "TableRows")
                    {
                        LoadTableRows(node);
                    }
                }
            }
        }

        private void LoadTableGroup(XmlNode tableGroupNode)
        {
            XmlNodeList nodeList = tableGroupNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Header")
                {
                    LoadHeader(node);
                }
                else if (node.Name == "Footer")
                {
                    LoadFooter(node);
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
            }
        }

        private void LoadTableGroups(XmlNode tableGroupsNode)
        {
            XmlNodeList nodeList = tableGroupsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "TableGroup")
                {
                    LoadTableGroup(node);
                }
            }
        }

        private void LoadDetails(XmlNode detailsNode)
        {
            if (detailsNode != null)
            {
                XmlNodeList nodeList = detailsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "TableRows")
                    {
                        LoadTableRows(node);
                    }
                    else if (node.Name == "Visibility")
                    {
                        LoadVisibility(node);
                    }
                }
            }
        }

        private void LoadFooter(XmlNode footerNode)
        {
            if (footerNode != null)
            {
                XmlNodeList nodeList = footerNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "TableRows")
                    {
                        LoadTableRows(node);
                    }
                }
            }
        }

        private void LoadCorner(XmlNode cornerNode)
        {
            if (cornerNode != null)
            {
                XmlNodeList nodeList = cornerNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "ReportItems")
                    {
                        //LoadReportItems(node);
                    }
                }
            }
        }

        private void LoadDynamicColumns(XmlNode dynamicColumnsNode, List<XmlNode> dynamicColumns)
        {
            XmlNodeList nodeList = dynamicColumnsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Subtotal")
                {
                    XmlNodeList subtotalNodeList = node.ChildNodes;
                    foreach (XmlNode subtotalNode in subtotalNodeList)
                    {
                        if (subtotalNode.Name == "ReportItems")
                        {
                            dynamicColumns.Add(subtotalNode.Clone());
                        }
                    }
                }
                else if (node.Name == "ReportItems")
                {
                    dynamicColumns.Add(node.Clone());
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
            }
        }

        private XmlNode LoadStaticColumn(XmlNode staticColumnNode)
        {
            XmlNode staticColumn = null;
            XmlNodeList nodeList = staticColumnNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    staticColumn = node.Clone();
                }
            }
            return staticColumn;
        }

        private void LoadStaticColumns(XmlNode staticColumnsNode, List<XmlNode> staticColumns)
        {
            XmlNodeList nodeList = staticColumnsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "StaticColumn")
                {
                    staticColumns.Add(LoadStaticColumn(node));
                }
            }
        }

        private float LoadColumnGrouping(XmlNode columnGroupingNode, List<XmlNode> dynamicColumns, List<XmlNode> staticColumns)
        {
            float cornerHeight = 0.8f * Utils.Units.Centimeters;
            XmlNodeList nodeList = columnGroupingNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Height")
                {
                    cornerHeight = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "DynamicColumns")
                {
                    LoadDynamicColumns(node, dynamicColumns);
                }
                else if (node.Name == "StaticColumns")
                {
                    LoadStaticColumns(node, staticColumns);
                }
            }
            return cornerHeight;
        }

        private float LoadColumnGroupings(XmlNode columnGroupingsNode, List<XmlNode> dynamicColumns, List<XmlNode> staticColumns)
        {
            float cornerHeight = 0.8f * Utils.Units.Centimeters;
            if (columnGroupingsNode != null)
            {
                XmlNodeList nodeList = columnGroupingsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "ColumnGrouping")
                    {
                        if (component is MatrixObject)
                        {
                            cornerHeight = LoadColumnGrouping(node, dynamicColumns, staticColumns);
                        }
                    }
                }
            }
            return cornerHeight;
        }

        private void LoadSubtotal(XmlNode subtotalNode)
        {
            XmlNodeList nodeList = subtotalNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    //LoadReportItems(node);
                }
                else if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
            }
        }

        private void LoadDynamicRows(XmlNode dynamicRowsNode, List<XmlNode> dynamicRows)
        {
            XmlNodeList nodeList = dynamicRowsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Subtotal")
                {
                    XmlNodeList subtotalNodeList = node.ChildNodes;
                    foreach (XmlNode subtotalNode in subtotalNodeList)
                    {
                        if (subtotalNode.Name == "ReportItems")
                        {
                            dynamicRows.Add(subtotalNode.Clone());
                        }
                    }
                }
                else if (node.Name == "ReportItems")
                {
                    dynamicRows.Add(node.Clone());
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
            }
        }

        private XmlNode LoadStaticRow(XmlNode staticRowNode)
        {
            XmlNode staticRow = null;
            XmlNodeList nodeList = staticRowNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    staticRow = node.Clone();
                }
            }
            return staticRow;
        }

        private void LoadStaticRows(XmlNode staticRowsNode, List<XmlNode> staticRows)
        {
            XmlNodeList nodeList = staticRowsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "StaticRow")
                {
                    staticRows.Add(LoadStaticRow(node));
                }
            }
        }

        private float LoadRowGrouping(XmlNode rowGroupingNode, List<XmlNode> dynamicRows, List<XmlNode> staticRows)
        {
            float cornerWidth = 2.5f * Utils.Units.Centimeters;
            XmlNodeList nodeList = rowGroupingNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Width")
                {
                    cornerWidth = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "DynamicRows")
                {
                    LoadDynamicRows(node, dynamicRows);
                }
                else if (node.Name == "StaticRows")
                {
                    LoadStaticRows(node, staticRows);
                }
            }
            return cornerWidth;
        }

        private float LoadRowGroupings(XmlNode rowGroupingsNode, List<XmlNode> dynamicRows, List<XmlNode> staticRows)
        {
            float cornerWidth = 2.5f * Utils.Units.Centimeters;
            if (rowGroupingsNode != null)
            {
                XmlNodeList nodeList = rowGroupingsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "RowGrouping")
                    {
                        if (component is MatrixObject)
                        {
                            cornerWidth = LoadRowGrouping(node, dynamicRows, staticRows);
                        }
                    }
                }
            }
            return cornerWidth;
        }

        private void LoadMatrixCell(XmlNode matrixCellNode, MatrixCellDescriptor cell, int col)
        {
            int row = (component as MatrixObject).RowCount - 1;
            XmlNodeList nodeList = matrixCellNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                }
            }
        }

        private void LoadMatrixCells(XmlNode matrixCellsNode)
        {
            int col = 0;
            XmlNodeList nodeList = matrixCellsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "MatrixCell")
                {
                    if (component is MatrixObject)
                    {
                        MatrixCellDescriptor cell = new MatrixCellDescriptor();
                        (component as MatrixObject).Data.Cells.Add(cell);
                        LoadMatrixCell(node, cell, col);
                        col++;
                    }
                }
            }
        }

        private float LoadMatrixRow(XmlNode matrixRowNode, MatrixHeaderDescriptor row)
        {
            float rowHeight = 0.8f * Utils.Units.Centimeters;
            XmlNodeList nodeList = matrixRowNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Height")
                {
                    rowHeight = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "MatrixCells")
                {
                    LoadMatrixCells(node);
                }
            }
            return rowHeight;
        }

        private float LoadMatrixRows(XmlNode matrixRowsNode)
        {
            float rowHeight = 0.8f * Utils.Units.Centimeters;
            if (matrixRowsNode != null)
            {
                XmlNodeList nodeList = matrixRowsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "MatrixRow")
                    {
                        if (component is MatrixObject)
                        {
                            MatrixHeaderDescriptor row = new MatrixHeaderDescriptor();
                            (component as MatrixObject).Data.Rows.Add(row);
                            rowHeight = LoadMatrixRow(node, row);
                        }
                    }
                }
            }
            return rowHeight;
        }

        private float LoadMatrixColumn(XmlNode matrixColumnNode, MatrixHeaderDescriptor column)
        {
            float columnWidth = 2.5f * Utils.Units.Centimeters;
            XmlNodeList nodeList = matrixColumnNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Width")
                {
                    columnWidth = UnitsConverter.SizeToPixels(node.InnerText);
                }
            }
            return columnWidth;
        }

        private float LoadMatrixColumns(XmlNode matrixColumnsNode)
        {
            float columnWidth = 2.5f * Utils.Units.Centimeters;
            if (matrixColumnsNode != null)
            {
                XmlNodeList nodeList = matrixColumnsNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "MatrixColumn")
                    {
                        if (component is MatrixObject)
                        {
                            MatrixHeaderDescriptor column = new MatrixHeaderDescriptor();
                            (component as MatrixObject).Data.Columns.Add(column);
                            columnWidth = LoadMatrixColumn(node, column);
                        }
                    }
                }
            }
            return columnWidth;
        }

        private void LoadReportItem(XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Top")
                {
                    component.Top = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Left")
                {
                    component.Left = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Height")
                {
                    component.Height = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Width")
                {
                    component.Width = UnitsConverter.SizeToPixels(node.InnerText);
                }
                else if (node.Name == "Visibility")
                {
                    LoadVisibility(node);
                }
                else if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
            }
            if (parent is TableCell)
            {
                component.Width = (parent as TableCell).Width;
                component.Height = (parent as TableCell).Height;
            }
        }

        private void LoadLine(XmlNode lineNode)
        {
            component = ComponentsFactory.CreateLineObject(lineNode.Attributes["Name"].Value, parent);
            XmlNodeList nodeList = lineNode.ChildNodes;
            LoadReportItem(nodeList);
        }

        private void LoadRectangle(XmlNode rectangleNode)
        {
            component = ComponentsFactory.CreateShapeObject(rectangleNode.Attributes["Name"].Value, parent);
            (component as ShapeObject).Shape = ShapeKind.Rectangle;
            XmlNodeList nodeList = rectangleNode.ChildNodes;
            LoadReportItem(nodeList);
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    LoadReportItems(node);
                }
            }
        }

        private void LoadTextbox(XmlNode textboxNode)
        {
            component = ComponentsFactory.CreateTextObject(textboxNode.Attributes["Name"].Value, parent);
            XmlNodeList nodeList = textboxNode.ChildNodes;
            LoadReportItem(nodeList);
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "CanGrow")
                {
                    (component as TextObject).CanGrow = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "CanShrink")
                {
                    (component as TextObject).CanShrink = UnitsConverter.BooleanToBool(node.InnerText);
                }
                else if (node.Name == "HideDuplicates")
                {
                    (component as TextObject).Duplicates = Duplicates.Hide;
                }
                else if (node.Name == "Value")
                {
                    (component as TextObject).Text = node.InnerText;
                }
            }
        }

        private void LoadImage(XmlNode imageNode)
        {
            component = ComponentsFactory.CreatePictureObject(imageNode.Attributes["Name"].Value, parent);
            XmlNodeList nodeList = imageNode.ChildNodes;
            LoadReportItem(nodeList);
            foreach (XmlNode node in nodeList)
            {
                //if (node.Name == "Source")
                //{
                //}
                /*else */if (node.Name == "Value")
                {
                    if (File.Exists(node.InnerText))
                    {
                        (component as PictureObject).ImageLocation = node.InnerText;
                    }
                }
                else if (node.Name == "Sizing")
                {
                    (component as PictureObject).SizeMode = UnitsConverter.ConvertSizing(node.InnerText);
                }
            }
        }

        private void LoadSubreport(XmlNode subreportNode)
        {
            component = ComponentsFactory.CreateSubreportObject(subreportNode.Attributes["Name"].Value, parent);
            ReportPage subPage = ComponentsFactory.CreateReportPage(Report);
            DataBand subBand = ComponentsFactory.CreateDataBand(subPage);
            subBand.Height = 2.0f * Utils.Units.Centimeters;
            (component as SubreportObject).ReportPage = subPage;
            XmlNodeList nodeList = subreportNode.ChildNodes;
            LoadReportItem(nodeList);
        }

        private void LoadChart(XmlNode chartNode)
        {
#if!Basic
            component = ComponentsFactory.CreateMSChartObject(chartNode.Attributes["Name"].Value, parent);
            MSChartObject chart = (component as MSChartObject);
            chart.Series.Clear();
            MSChartSeries series = new MSChartSeries();
            chart.Series.Add(series);
            series.CreateUniqueName();
            chart.Chart.Series.Add(new Series());
            Legend legend = new Legend();
            chart.Chart.Legends.Add(legend);
            legend.Enabled = false;
            legend.BackColor = Color.Transparent;
            Title title = new Title();
            chart.Chart.Titles.Add(title);
            title.Visible = true;
            ChartArea chartArea = new ChartArea("Default");
            chart.Chart.ChartAreas.Add(chartArea);
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX = new Axis();
            chartArea.AxisX.IsMarginVisible = false;
            chartArea.AxisY = new Axis();
            chartArea.AxisY.IsMarginVisible = false;
            chartArea.Area3DStyle.LightStyle = LightStyle.None;

            XmlNodeList nodeList = chartNode.ChildNodes;
            LoadReportItem(nodeList);
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Type")
                {
                    series.SeriesSettings.ChartType = UnitsConverter.ConvertChartType(node.InnerText);
                }
                else if (node.Name == "Legend")
                {
                    LoadLegend(node);
                }
                else if (node.Name == "CategoryAxis")
                {
                    LoadCategoryAxis(node);
                }
                else if (node.Name == "ValueAxis")
                {
                    LoadValueAxis(node);
                }
                else if (node.Name == "Title")
                {
                    LoadTitle(node);
                }
                else if (node.Name == "Palette")
                {
                    chart.Chart.Palette = UnitsConverter.ConvertChartPalette(node.InnerText);
                }
                else if (node.Name == "ThreeDProperties")
                {
                    LoadThreeDProperties(node);
                }
            }
#endif
        }

        private void LoadTable(XmlNode tableNode)
        {
#if!Basic
            component = ComponentsFactory.CreateTableObject(tableNode.Attributes["Name"].Value, parent);
            XmlNodeList nodeList = tableNode.ChildNodes;
            LoadReportItem(nodeList);
            XmlNode tableColumnsNode = null;
            XmlNode headerNode = null;
            XmlNode detailsNode = null;
            XmlNode footerNode = null;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "TableColumns")
                {
                    tableColumnsNode = node.Clone();
                }
                else if (node.Name == "Header")
                {
                    headerNode = node.Clone();
                }
                else if (node.Name == "Details")
                {
                    detailsNode = node.Clone();
                }
                else if (node.Name == "Footer")
                {
                    footerNode = node.Clone();
                }
            }
            LoadTableColumns(tableColumnsNode);
            LoadHeader(headerNode);
            LoadDetails(detailsNode);
            LoadFooter(footerNode);
            (component as TableObject).CreateUniqueNames();
#endif
        }

        private void LoadMatrix(XmlNode matrixNode)
        {
#if!Basic
            component = ComponentsFactory.CreateMatrixObject(matrixNode.Attributes["Name"].Value, parent);
            MatrixObject matrix = component as MatrixObject;
            matrix.AutoSize = false;

            XmlNodeList nodeList = matrixNode.ChildNodes;
            LoadReportItem(nodeList);
            //XmlNode cornerNode = null;
            XmlNode columnGroupingsNode = null;
            XmlNode rowGroupingsNode = null;
            XmlNode matrixRowsNode = null;
            XmlNode matrixColumnsNode = null;
            foreach (XmlNode node in nodeList)
            {
                //if (node.Name == "Corner")
                //{
                //    cornerNode = node.Clone();
                //}
                /*else */if (node.Name == "ColumnGroupings")
                {
                    columnGroupingsNode = node.Clone();
                }
                else if (node.Name == "RowGroupings")
                {
                    rowGroupingsNode = node.Clone();
                }
                else if (node.Name == "MatrixColumns")
                {
                    matrixColumnsNode = node.Clone();
                }
                else if (node.Name == "MatrixRows")
                {
                    matrixRowsNode = node.Clone();
                }
            }

            //LoadCorner(cornerNode);

            List<XmlNode> dynamicColumns = new List<XmlNode>();
            List<XmlNode> staticColumns = new List<XmlNode>();
            LoadColumnGroupings(columnGroupingsNode, dynamicColumns, staticColumns);

            List<XmlNode> dynamicRows = new List<XmlNode>();
            List<XmlNode> staticRows = new List<XmlNode>();
            LoadRowGroupings(rowGroupingsNode, dynamicRows, staticRows);

            float columnWidth = LoadMatrixColumns(matrixColumnsNode);
            float rowHeight = LoadMatrixRows(matrixRowsNode);

            matrix.CreateUniqueNames();
            matrix.BuildTemplate();

            for (int i = 1; i < matrix.Columns.Count; i++)
            {
                matrix.Columns[i].Width = columnWidth;
            }
            for (int i = 1; i < matrix.Rows.Count; i++)
            {
                matrix.Rows[i].Height = rowHeight;
            }

            for (int i = 0; i < matrix.Columns.Count; i++)
            {
                for (int j = 0; j < matrix.Rows.Count; j++)
                {
                    matrix.GetCellData(i, j).Cell.Text = "";
                }
            }

            for (int i = 0; i < dynamicColumns.Count; i++)
            {
                Base tempParent = parent;
                ComponentBase tempComponent = component;
                parent = matrix.GetCellData(i + 1, 0).Cell;
                LoadReportItems(dynamicColumns[i]);
                component = tempComponent;
                parent = tempParent;
            }
            for (int i = 0; i < dynamicRows.Count; i++)
            {
                Base tempParent = parent;
                ComponentBase tempComponent = component;
                parent = matrix.GetCellData(0, i + 1).Cell;
                LoadReportItems(dynamicRows[i]);
                component = tempComponent;
                parent = tempParent;
            }
#endif
        }

        private void LoadReportItems(XmlNode reportItemsNode)
        {
            XmlNodeList nodeList = reportItemsNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Line")
                {
                    LoadLine(node);
                }
                else if (node.Name == "Rectangle")
                {
                    LoadRectangle(node);
                }
                else if (node.Name == "Textbox")
                {
                    LoadTextbox(node);
                }
                else if (node.Name == "Image")
                {
                    LoadImage(node);
                }
                else if (node.Name == "Subreport")
                {
                    LoadSubreport(node);
                }
                else if (node.Name == "Chart")
                {
                    LoadChart(node);
                }
                else if (node.Name == "Table")
                {
                    LoadTable(node);
                }
                else if (node.Name == "Matrix")
                {
                    LoadMatrix(node);
                }
            }
        }

        private void LoadBody(XmlNode bodyNode)
        {
            if (page == null)
            {
                page = ComponentsFactory.CreateReportPage(Report);
            }
            parent = ComponentsFactory.CreateDataBand(page);
            XmlNodeList nodeList = bodyNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "ReportItems")
                {
                    LoadReportItems(node);
                }
                else if (node.Name == "Height")
                {
                    (parent as DataBand).Height = UnitsConverter.SizeToPixels(node.InnerText);
                }
            }
        }

        private void LoadPageSection(XmlNode pageSectionNode)
        {
            if (pageSectionNode.Name == "PageHeader")
            {
                page.PageHeader = new PageHeaderBand();
                page.PageHeader.CreateUniqueName();
                page.PageHeader.PrintOn = PrintOn.EvenPages | PrintOn.OddPages | PrintOn.RepeatedBand;
                XmlNodeList nodeList = pageSectionNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "Height")
                    {
                        page.PageHeader.Height = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                    else if (node.Name == "PrintOnFirstPage")
                    {
                        if (node.InnerText == "true")
                        {
                            page.PageHeader.PrintOn |= PrintOn.FirstPage;
                        }
                    }
                    else if (node.Name == "PrintOnLastPage")
                    {
                        if (node.InnerText == "true")
                        {
                            page.PageHeader.PrintOn |= PrintOn.LastPage;
                        }
                    }
                    else if (node.Name == "ReportItems")
                    {
                        parent = page.PageHeader;
                        LoadReportItems(node);
                    }
                }
            }
            else if (pageSectionNode.Name == "PageFooter")
            {
                page.PageFooter = new PageFooterBand();
                page.PageFooter.CreateUniqueName();
                page.PageFooter.PrintOn = PrintOn.EvenPages | PrintOn.OddPages | PrintOn.RepeatedBand;
                XmlNodeList nodeList = pageSectionNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Name == "Height")
                    {
                        page.PageFooter.Height = UnitsConverter.SizeToPixels(node.InnerText);
                    }
                    else if (node.Name == "PrintOnFirstPage")
                    {
                        if (node.InnerText == "true")
                        {
                            page.PageFooter.PrintOn |= PrintOn.FirstPage;
                        }
                    }
                    else if (node.Name == "PrintOnLastPage")
                    {
                        if (node.InnerText == "true")
                        {
                            page.PageFooter.PrintOn |= PrintOn.LastPage;
                        }
                    }
                    else if (node.Name == "ReportItems")
                    {
                        parent = page.PageFooter;
                        LoadReportItems(node);
                    }
                }
            }
        }

        private void LoadPage(XmlNode pageNode)
        {
            XmlNodeList nodeList = pageNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "PageHeader")
                {
                    LoadPageSection(node);
                }
                else if (node.Name == "PageFooter")
                {
                    LoadPageSection(node);
                }
                else if (node.Name == "PageHeight")
                {
                    page.PaperHeight = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "PageWidth")
                {
                    page.PaperWidth = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "LeftMargin")
                {
                    page.LeftMargin = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "RightMargin")
                {
                    page.RightMargin = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "TopMargin")
                {
                    page.TopMargin = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "BottomMargin")
                {
                    page.BottomMargin = UnitsConverter.SizeToMillimeters(node.InnerText);
                }
                else if (node.Name == "Style")
                {
                    LoadStyle(node);
                }
            }
        }

        private void LoadReport(XmlNode reportNode)
        {
            bool pageFounded = false;
            XmlNodeList nodeList = reportNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "Description")
                {
                    Report.ReportInfo.Description = node.InnerText;
                }
                else if (node.Name == "Author")
                {
                    Report.ReportInfo.Author = node.InnerText;
                }
                else if (node.Name == "Body")
                {
                    LoadBody(node);
                }
                else if (node.Name == "Page")
                {
                    pageFounded = true;
                    page = ComponentsFactory.CreateReportPage(Report);
                    LoadPage(node);
                }
            }
            if (!pageFounded)
            {
                if (page == null)
                {
                    page = ComponentsFactory.CreateReportPage(Report);
                }
                LoadPage(reportNode);
            }
        }

        #endregion // Private Methods

        #region Protected Methods

        /// <inheritdoc/>
        protected override string GetFilter()
        {
            return new FastReport.Utils.MyRes("FileFilters").Get("RdlFiles");
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void LoadReport(Report report, string filename)
        {
            Report = report;
            Report.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode reportNode = doc.LastChild;
            LoadReport(reportNode);
            page = null;
        }

        #endregion // Public Methods
    }
}
