using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastReport.MSChart;
using FastReport.Table;
using FastReport.Matrix;

namespace FastReport.Design.ExportPlugins.RDL
{
    /// <summary>
    /// Represents the RDL export plugin.
    /// </summary>
    internal class RDLExportPlugin : ExportPlugin
    {
        #region Fields

        private StreamWriter writer;
        private ReportPage page;
        private Base parent;
        private ReportComponentBase component;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RDLExportPlugin"/> class.
        /// </summary>
        public RDLExportPlugin() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RDLExportPlugin"/> class with a specified designer.
        /// </summary>
        /// <param name="designer">The report designer.</param>
        public RDLExportPlugin(Designer designer) : base(designer)
        {
        }

        #endregion // Constructors

        #region Private Methods

        private string ReplaceControlChars(string str)
        {
            str = str.Replace(Environment.NewLine, "&#13;&#10;");
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "&apos;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }

        private void WriteBorderStyle(string tab)
        {
            if (component.Border.Lines != BorderLines.None)
            {
                bool writeDefaultLine = true;
                bool writeLeftLine = true;
                bool writeRightLine = true;
                bool writeTopLine = true;
                bool writeBottomLine = true;
                if (component.Border.Lines != BorderLines.All)
                {
                    writeDefaultLine = false;
                    string borderLines = component.Border.Lines.ToString();
                    writeLeftLine = borderLines.Contains(BorderLines.Left.ToString());
                    writeRightLine = borderLines.Contains(BorderLines.Right.ToString());
                    writeTopLine = borderLines.Contains(BorderLines.Top.ToString());
                    writeBottomLine = borderLines.Contains(BorderLines.Bottom.ToString());
                }
                writer.WriteLine(tab + "  <BorderStyle>");
                if (writeDefaultLine)
                {
                    writer.WriteLine(tab + "    <Default>" + UnitsConverter.ConvertLineStyle(component.Border.Style) + "</Default>");
                }
                if (writeLeftLine)
                {
                    writer.WriteLine(tab + "    <Left>" + UnitsConverter.ConvertLineStyle(component.Border.LeftLine.Style) + "</Left>");
                }
                if (writeRightLine)
                {
                    writer.WriteLine(tab + "    <Right>" + UnitsConverter.ConvertLineStyle(component.Border.RightLine.Style) + "</Right>");
                }
                if (writeTopLine)
                {
                    writer.WriteLine(tab + "    <Top>" + UnitsConverter.ConvertLineStyle(component.Border.TopLine.Style) + "</Top>");
                }
                if (writeBottomLine)
                {
                    writer.WriteLine(tab + "    <Bottom>" + UnitsConverter.ConvertLineStyle(component.Border.BottomLine.Style) + "</Bottom>");
                }
                writer.WriteLine(tab + "  </BorderStyle>");
            }
        }

        private void WriteBorderColor(string tab)
        {
            writer.WriteLine(tab + "  <BorderColor>");
            writer.WriteLine(tab + "    <Default>" + UnitsConverter.ConvertColor(component.Border.Color) + "</Default>");
            writer.WriteLine(tab + "    <Left>" + UnitsConverter.ConvertColor(component.Border.LeftLine.Color) + "</Left>");
            writer.WriteLine(tab + "    <Right>" + UnitsConverter.ConvertColor(component.Border.RightLine.Color) + "</Right>");
            writer.WriteLine(tab + "    <Top>" + UnitsConverter.ConvertColor(component.Border.TopLine.Color) + "</Top>");
            writer.WriteLine(tab + "    <Bottom>" + UnitsConverter.ConvertColor(component.Border.BottomLine.Color) + "</Bottom>");
            writer.WriteLine(tab + "  </BorderColor>");
        }

        private void WriteBorderWidth(string tab)
        {
            writer.WriteLine(tab + "  <BorderWidth>");
            writer.WriteLine(tab + "    <Default>" + UnitsConverter.PixelsToMillimeters(component.Border.Width) + "</Default>");
            writer.WriteLine(tab + "    <Left>" + UnitsConverter.PixelsToMillimeters(component.Border.LeftLine.Width) + "</Left>");
            writer.WriteLine(tab + "    <Right>" + UnitsConverter.PixelsToMillimeters(component.Border.RightLine.Width) + "</Right>");
            writer.WriteLine(tab + "    <Top>" + UnitsConverter.PixelsToMillimeters(component.Border.TopLine.Width) + "</Top>");
            writer.WriteLine(tab + "    <Bottom>" + UnitsConverter.PixelsToMillimeters(component.Border.BottomLine.Width) + "</Bottom>");
            writer.WriteLine(tab + "  </BorderWidth>");
        }

        private void WriteStyle(string tab)
        {
            writer.WriteLine(tab + "<Style>");
            WriteBorderStyle(tab);
            WriteBorderColor(tab);
            WriteBorderWidth(tab);
            if (component is TextObject)
            {
                TextObject to = component as TextObject;
                writer.WriteLine(tab + "  <FontStyle>" + UnitsConverter.ConvertFontStyle(to.Font.Style) + "</FontStyle>");
                writer.WriteLine(tab + "  <FontFamily>" + UnitsConverter.ConvertFontFamily(to.Font.FontFamily) + "</FontFamily>");
                writer.WriteLine(tab + "  <FontSize>" + UnitsConverter.ConvertFontSize(to.Font.Size) + "</FontSize>");
                writer.WriteLine(tab + "  <TextAlign>" + UnitsConverter.ConvertHorzAlign(to.HorzAlign) + "</TextAlign>");
                writer.WriteLine(tab + "  <VerticalAlign>" + UnitsConverter.ConvertVertAlign(to.VertAlign) + "</VerticalAlign>");
                writer.WriteLine(tab + "  <WritingMode>" + UnitsConverter.ConvertAngleToWritingMode(to.Angle) + "</WritingMode>");
                writer.WriteLine(tab + "  <Color>" + UnitsConverter.ConvertColor(to.TextColor) + "</Color>");
                writer.WriteLine(tab + "  <PaddingBottom>" + UnitsConverter.ConvertPixels(to.Padding.Bottom) + "</PaddingBottom>");
                writer.WriteLine(tab + "  <PaddingLeft>" + UnitsConverter.ConvertPixels(to.Padding.Left) + "</PaddingLeft>");
                writer.WriteLine(tab + "  <PaddingRight>" + UnitsConverter.ConvertPixels(to.Padding.Right) + "</PaddingRight>");
                writer.WriteLine(tab + "  <PaddingTop>" + UnitsConverter.ConvertPixels(to.Padding.Top) + "</PaddingTop>");
                writer.WriteLine(tab + "  <BackgroundColor>" + UnitsConverter.ConvertColor(to.FillColor) + "</BackgroundColor>");
            }
            else if (component is ShapeObject)
            {
                writer.WriteLine(tab + "  <BackgroundColor>" + UnitsConverter.ConvertColor((component as ShapeObject).FillColor) + "</BackgroundColor>");
            }
            else if (component is MSChartObject)
            {
                MSChartObject chart = component as MSChartObject;
                writer.WriteLine(tab + "  <BackgroundColor>" + UnitsConverter.ConvertColor(chart.Chart.BackColor) + "</BackgroundColor>");
                writer.WriteLine(tab + "  <BackgroundGradientType>" + UnitsConverter.ConvertGradientStyle(chart.Chart.BackGradientStyle) + "</BackgroundGradientType>");
                writer.WriteLine(tab + "  <BackgroundGradientEndColor>" + UnitsConverter.ConvertColor(chart.Chart.BackSecondaryColor) + "</BackgroundGradientEndColor>");
                writer.WriteLine(tab + "  <PaddingBottom>" + UnitsConverter.ConvertPixels(chart.Chart.Padding.Bottom) + "</PaddingBottom>");
                writer.WriteLine(tab + "  <PaddingLeft>" + UnitsConverter.ConvertPixels(chart.Chart.Padding.Left) + "</PaddingLeft>");
                writer.WriteLine(tab + "  <PaddingRight>" + UnitsConverter.ConvertPixels(chart.Chart.Padding.Right) + "</PaddingRight>");
                writer.WriteLine(tab + "  <PaddingTop>" + UnitsConverter.ConvertPixels(chart.Chart.Padding.Top) + "</PaddingTop>");
            }
            else if (component is TableObject)
            {
                writer.WriteLine(tab + "  <BackgroundColor>" + UnitsConverter.ConvertColor((component as TableObject).FillColor) + "</BackgroundColor>");
            }
            else if (component is PictureObject)
            {
                PictureObject pic = component as PictureObject;
                writer.WriteLine(tab + "  <PaddingBottom>" + UnitsConverter.ConvertPixels(pic.Padding.Bottom) + "</PaddingBottom>");
                writer.WriteLine(tab + "  <PaddingLeft>" + UnitsConverter.ConvertPixels(pic.Padding.Left) + "</PaddingLeft>");
                writer.WriteLine(tab + "  <PaddingRight>" + UnitsConverter.ConvertPixels(pic.Padding.Right) + "</PaddingRight>");
                writer.WriteLine(tab + "  <PaddingTop>" + UnitsConverter.ConvertPixels(pic.Padding.Top) + "</PaddingTop>");
            }
            writer.WriteLine(tab + "</Style>");
        }

        private void WriteStyleT(Title title, string tab)
        {
            writer.WriteLine(tab + "<Style>");
            writer.WriteLine(tab + "  <BorderColor>");
            writer.WriteLine(tab + "    <Default>" + UnitsConverter.ConvertColor(title.BorderColor) + "</Default>");
            writer.WriteLine(tab + "  </BorderColor>");
            writer.WriteLine(tab + "  <BorderStyle>");
            writer.WriteLine(tab + "    <Default>" + UnitsConverter.ConvertChartDashStyle(title.BorderDashStyle) + "</Default>");
            writer.WriteLine(tab + "  </BorderStyle>");
            writer.WriteLine(tab + "  <BorderWidth>");
            writer.WriteLine(tab + "    <Default>" + UnitsConverter.PixelsToMillimeters(title.BorderWidth) + "</Default>");
            writer.WriteLine(tab + "  </BorderWidth>");
            writer.WriteLine(tab + "  <BackgroundColor>" + UnitsConverter.ConvertColor(title.BackColor) + "</BackgroundColor>");
            writer.WriteLine(tab + "  <BackgroundGradientType>" + UnitsConverter.ConvertGradientStyle(title.BackGradientStyle) + "</BackgroundGradientType>");
            writer.WriteLine(tab + "  <BackgroundGradientEndColor>" + UnitsConverter.ConvertColor(title.BackSecondaryColor) + "</BackgroundGradientEndColor>");
            writer.WriteLine(tab + "  <FontStyle>" + UnitsConverter.ConvertFontStyle(title.Font.Style) + "</FontStyle>");
            writer.WriteLine(tab + "  <FontFamily>" + UnitsConverter.ConvertFontFamily(title.Font.FontFamily) + "</FontFamily>");
            writer.WriteLine(tab + "  <FontSize>" + UnitsConverter.ConvertFontSize(title.Font.Size) + "</FontSize>");
            writer.WriteLine(tab + "  <TextAlign>" + UnitsConverter.ContentAlignmentToTextAlign(title.Alignment) + "</TextAlign>");
            writer.WriteLine(tab + "  <VerticalAlign>" + UnitsConverter.ContentAlignmentToVerticalAlign(title.Alignment) + "</VerticalAlign>");
            writer.WriteLine(tab + "  <Color>" + UnitsConverter.ConvertColor(title.ForeColor) + "</Color>");
            writer.WriteLine(tab + "</Style>");
        }

        private void WriteVisibility(bool visible, string tab)
        {
            if (!visible)
            {
                writer.WriteLine(tab + "<Visibility>");
                writer.WriteLine(tab + "  <Hidden>=true</Hidden>");
                writer.WriteLine(tab + "</Visibility>");
            }
        }

        private void WritePageSize()
        {
            writer.WriteLine("  <PageHeight>" + UnitsConverter.MillimetersToString(page.PaperHeight) + "</PageHeight>");
            writer.WriteLine("  <PageWidth>" + UnitsConverter.MillimetersToString(page.PaperWidth) + "</PageWidth>");
        }

        private void WriteLegend()
        {
            Legend legend = (component as MSChartObject).Chart.Legends[0];
            writer.WriteLine("        <Legend>");
            writer.WriteLine("          <Visible>" + UnitsConverter.ConvertBool(legend.Enabled) + "</Visible>");
            //WriteStyle("          ");
            writer.WriteLine("          <Position>" + UnitsConverter.ConvertLegendDockingAndAlignment(legend.Docking, legend.Alignment) + "</Position>");
            writer.WriteLine("          <Layout>" + UnitsConverter.ConvertLegendStyle(legend.LegendStyle) + "</Layout>");
            writer.WriteLine("          <InsidePlotArea>" + UnitsConverter.ConvertBool(legend.IsDockedInsideChartArea) + "</InsidePlotArea>");
            writer.WriteLine("        </Legend>");
        }

        private void WriteGrid(Grid grid)
        {
            writer.WriteLine("              <ShowGridLines>" + UnitsConverter.ConvertBool(grid.Enabled) + "</ShowGridLines>");
            writer.WriteLine("              <Style>");
            writer.WriteLine("                <BorderStyle>" + UnitsConverter.ConvertChartDashStyle(grid.LineDashStyle) + "</BorderStyle>");
            writer.WriteLine("                <BorderColor>" + UnitsConverter.ConvertColor(grid.LineColor) + "</BorderColor>");
            writer.WriteLine("                <BorderWidth>" + UnitsConverter.PixelsToMillimeters(grid.LineWidth) + "</BorderWidth>");
            writer.WriteLine("              </Style>");
        }

        private void WriteAxis(Axis axis)
        {
            writer.WriteLine("          <Axis>");
            writer.WriteLine("            <Visible>" + UnitsConverter.ConvertAxisEnabled(axis.Enabled) + "</Visible>");
            writer.WriteLine("            <Title>");
            writer.WriteLine("              <Caption>" + axis.Title + "</Caption>");
            writer.WriteLine("              <Style>");
            writer.WriteLine("                <FontStyle>" + UnitsConverter.ConvertFontStyle(axis.TitleFont.Style) + "</FontStyle>");
            writer.WriteLine("                <FontFamily>" + UnitsConverter.ConvertFontFamily(axis.TitleFont.FontFamily) + "</FontFamily>");
            writer.WriteLine("                <FontSize>" + UnitsConverter.ConvertFontSize(axis.TitleFont.Size) + "</FontSize>");
            writer.WriteLine("                <TextAlign>" + UnitsConverter.ConvertStringAlignment(axis.TitleAlignment) + "</TextAlign>");
            writer.WriteLine("                <Color>" + UnitsConverter.ConvertColor(axis.TitleForeColor) + "</Color>");
            writer.WriteLine("              </Style>");
            writer.WriteLine("            </Title>");
            writer.WriteLine("            <Margin>" + UnitsConverter.ConvertBool(axis.IsMarginVisible) + "</Margin>");
            writer.WriteLine("            <MajorTickMarks>" + UnitsConverter.ConvertTickMarkStyle(axis.MajorTickMark.TickMarkStyle) + "</MajorTickMarks>");
            writer.WriteLine("            <MinorTickMarks>" + UnitsConverter.ConvertTickMarkStyle(axis.MinorTickMark.TickMarkStyle) + "</MinorTickMarks>");
            writer.WriteLine("            <MajorGridLines>");
            WriteGrid(axis.MajorGrid);
            writer.WriteLine("            </MajorGridLines>");
            writer.WriteLine("            <MinorGridLines>");
            WriteGrid(axis.MinorGrid);
            writer.WriteLine("            </MinorGridLines>");
            writer.WriteLine("            <Reverse>" + UnitsConverter.ConvertBool(axis.IsReversed) + "</Reverse>");
            writer.WriteLine("            <Interlaced>" + UnitsConverter.ConvertBool(axis.IsInterlaced) + "</Interlaced>");
            writer.WriteLine("          </Axis>");
        }

        private void WriteCategoryAxis()
        {
            writer.WriteLine("        <CategoryAxis>");
            WriteAxis((component as MSChartObject).Chart.ChartAreas[0].AxisX);
            writer.WriteLine("        </CategoryAxis>");
        }

        private void WriteValueAxis()
        {
            writer.WriteLine("        <ValueAxis>");
            WriteAxis((component as MSChartObject).Chart.ChartAreas[0].AxisY);
            writer.WriteLine("        </ValueAxis>");
        }

        private void WriteTitle()
        {
            Title title = (component as MSChartObject).Chart.Titles[0];
            writer.WriteLine("        <Title>");
            writer.WriteLine("          <Caption>" + ReplaceControlChars(title.Text) + "</Caption>");
            WriteStyleT(title, "          ");
            writer.WriteLine("        </Title>");
        }

        private void WriteThreeDProperties()
        {
            MSChartObject chart = component as MSChartObject;
            writer.WriteLine("        <ThreeDProperties>");
            writer.WriteLine("          <Enabled>" + UnitsConverter.ConvertBool(chart.Chart.ChartAreas[0].Area3DStyle.Enable3D) + "</Enabled>");
            writer.WriteLine("          <Rotation>" + chart.Chart.ChartAreas[0].Area3DStyle.Rotation.ToString() + "</Rotation>");
            writer.WriteLine("          <Inclination>" + chart.Chart.ChartAreas[0].Area3DStyle.Inclination.ToString() + "</Inclination>");
            writer.WriteLine("          <Perspective>" + chart.Chart.ChartAreas[0].Area3DStyle.Perspective.ToString() + "</Perspective>");
            writer.WriteLine("          <DepthRatio>" + chart.Chart.ChartAreas[0].Area3DStyle.PointDepth.ToString() + "</DepthRatio>");
            writer.WriteLine("          <Shading>" + UnitsConverter.ConvertLightStyle(chart.Chart.ChartAreas[0].Area3DStyle.LightStyle) + "</Shading>");
            writer.WriteLine("          <GapDepth>" + chart.Chart.ChartAreas[0].Area3DStyle.PointGapDepth.ToString() + "</GapDepth>");
            writer.WriteLine("          <WallThickness>" + chart.Chart.ChartAreas[0].Area3DStyle.WallWidth.ToString() + "</WallThickness>");
            writer.WriteLine("          <Clustered>" + UnitsConverter.ConvertBool(chart.Chart.ChartAreas[0].Area3DStyle.IsClustered) + "</Clustered>");
            writer.WriteLine("        </ThreeDProperties>");
        }

        private void WriteTableColumn(TableColumn column)
        {
            writer.WriteLine("          <TableColumn>");
            writer.WriteLine("            <Width>" + UnitsConverter.PixelsToMillimeters(column.Width) + "</Width>");
            WriteVisibility(column.Visible, "            ");
            writer.WriteLine("          </TableColumn>");
        }

        private void WriteTableColumns()
        {
            writer.WriteLine("        <TableColumns>");
            foreach (TableColumn column in (component as TableObject).Columns)
            {
                WriteTableColumn(column);
            }
            writer.WriteLine("        </TableColumns>");
        }

        private void WriteTableCell(TableCell cell)
        {
            writer.WriteLine("                <TableCell>");
            writer.WriteLine("                  <ReportItems>");
            writer.WriteLine("                    <Textbox Name=\"" + cell.Name + "\">");
            ReportComponentBase tempComponent = component;
            component = cell;
            WriteStyle("                      ");
            component = tempComponent;
            writer.WriteLine("                      <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                      <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                      <Value>" + ReplaceControlChars(cell.Text) + "</Value>");
            writer.WriteLine("                   </Textbox>");
            writer.WriteLine("                  </ReportItems>");
            writer.WriteLine("                  <ColSpan>" + cell.ColSpan.ToString() + "</ColSpan>");
            writer.WriteLine("                </TableCell>");
        }

        private void WriteTableRow(TableRow row, int rowNum)
        {
            writer.WriteLine("            <TableRow>");
            writer.WriteLine("              <TableCells>");
            for (int col = 0; col < (component as TableObject).ColumnCount; col++)
            {
                WriteTableCell((component as TableObject).GetCellData(col, rowNum).Cell);
            }
            writer.WriteLine("              </TableCells>");
            writer.WriteLine("              <Height>" + UnitsConverter.PixelsToMillimeters(row.Height) + "</Height>");
            WriteVisibility(row.Visible, "              ");
            writer.WriteLine("            </TableRow>");
        }

        private void WriteHeader()
        {
            writer.WriteLine("        <Header>");
            writer.WriteLine("          <TableRows>");
            WriteTableRow((component as TableObject).Rows[0], 0);
            writer.WriteLine("          </TableRows>");
            writer.WriteLine("        </Header>");
        }

        private void WriteDetails()
        {
            TableObject table = component as TableObject;
            if (table.Rows.Count > 1)
            {
                writer.WriteLine("        <Details>");
                writer.WriteLine("          <TableRows>");
                WriteTableRow(table.Rows[1], 1);
                writer.WriteLine("          </TableRows>");
                WriteVisibility(table.Rows[1].Visible, "          ");
                writer.WriteLine("        </Details>");
            }
        }

        private void WriteFooter()
        {
            TableObject table = component as TableObject;
            if (table.Rows.Count > 2)
            {
                writer.WriteLine("        <Footer>");
                writer.WriteLine("          <TableRows>");
                WriteTableRow(table.Rows[table.Rows.Count - 1], table.Rows.Count - 1);
                writer.WriteLine("          </TableRows>");
                writer.WriteLine("        </Footer>");
            }
        }

        private void WriteColumnGroupings()
        {
            MatrixObject matrix = component as MatrixObject;
            ReportComponentBase tempComponent = component;
            TableCell cell = matrix.GetCellData(0, 0).Cell;
            writer.WriteLine("        <ColumnGroupings>");
            writer.WriteLine("          <ColumnGrouping>");
            writer.WriteLine("            <Height>" + UnitsConverter.PixelsToMillimeters(matrix.Rows[0].Height) + "</Height>");
            writer.WriteLine("            <DynamicColumns>");
            writer.WriteLine("              <Grouping Name=\"DynamicColumnsGrouping1\">");
            writer.WriteLine("                <GroupExpressions>");
            writer.WriteLine("                  <GroupExpression>" + "" + "</GroupExpression>");
            writer.WriteLine("                </GroupExpressions>");
            writer.WriteLine("              </Grouping>");
            writer.WriteLine("              <ReportItems>");
            cell = matrix.GetCellData(1, 0).Cell;
            writer.WriteLine("                <Textbox Name=\"" + cell.Name + "\">");
            tempComponent = component;
            component = cell;
            WriteStyle("                  ");
            component = tempComponent;
            writer.WriteLine("                  <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                  <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                  <Value>" + cell.Text + "</Value>");
            writer.WriteLine("                </Textbox>");
            writer.WriteLine("              </ReportItems>");
            writer.WriteLine("              <Subtotal>");
            writer.WriteLine("                <ReportItems>");
            cell = matrix.GetCellData(matrix.ColumnCount - 1, 0).Cell;
            writer.WriteLine("                  <Textbox Name=\"" + cell.Name + "\">");
            tempComponent = component;
            component = cell;
            WriteStyle("                    ");
            component = tempComponent;
            writer.WriteLine("                    <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                    <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                    <Value>" + cell.Text + "</Value>");
            writer.WriteLine("                  </Textbox>");
            writer.WriteLine("                </ReportItems>");
            writer.WriteLine("              </Subtotal>");
            WriteVisibility(cell.Visible, "              ");
            writer.WriteLine("            </DynamicColumns>");
            writer.WriteLine("          </ColumnGrouping>");
            writer.WriteLine("        </ColumnGroupings>");
        }

        private void WriteRowGroupings()
        {
            MatrixObject matrix = component as MatrixObject;
            ReportComponentBase tempComponent = component;
            TableCell cell = matrix.GetCellData(0, 0).Cell;
            writer.WriteLine("        <RowGroupings>");
            writer.WriteLine("          <RowGrouping>");
            writer.WriteLine("            <Width>" + UnitsConverter.PixelsToMillimeters(matrix.Columns[0].Width) + "</Width>");
            writer.WriteLine("            <DynamicRows>");
            writer.WriteLine("              <Grouping Name=\"DynamicRowsGrouping1\">");
            writer.WriteLine("                <GroupExpressions>");
            writer.WriteLine("                  <GroupExpression>" + "" + "</GroupExpression>");
            writer.WriteLine("                </GroupExpressions>");
            writer.WriteLine("              </Grouping>");
            writer.WriteLine("              <ReportItems>");
            cell = matrix.GetCellData(0, 1).Cell;
            writer.WriteLine("                <Textbox Name=\"" + cell.Name + "\">");
            tempComponent = component;
            component = cell;
            WriteStyle("                  ");
            component = tempComponent;
            writer.WriteLine("                  <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                  <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                  <Value>" + cell.Text + "</Value>");
            writer.WriteLine("                </Textbox>");
            writer.WriteLine("              </ReportItems>");
            writer.WriteLine("              <Subtotal>");
            writer.WriteLine("                <ReportItems>");
            cell = matrix.GetCellData(0, matrix.RowCount - 1).Cell;
            writer.WriteLine("                  <Textbox Name=\"" + cell.Name + "\">");
            tempComponent = component;
            component = cell;
            WriteStyle("                    ");
            component = tempComponent;
            writer.WriteLine("                    <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                    <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                    <Value>" + cell.Text + "</Value>");
            writer.WriteLine("                  </Textbox>");
            writer.WriteLine("                </ReportItems>");
            writer.WriteLine("              </Subtotal>");
            WriteVisibility(cell.Visible, "              ");
            writer.WriteLine("            </DynamicRows>");
            writer.WriteLine("          </RowGrouping>");
            writer.WriteLine("        </RowGroupings>");
        }

        private void WriteMatrixColumns()
        {
            MatrixObject matrix = component as MatrixObject;
            writer.WriteLine("        <MatrixColumns>");
            writer.WriteLine("          <MatrixColumn>");
            writer.WriteLine("            <Width>" + UnitsConverter.PixelsToMillimeters(matrix.Columns[0].Width) + "</Width>");
            writer.WriteLine("          </MatrixColumn>");
            writer.WriteLine("        </MatrixColumns>");
        }

        private void WriteMatrixRows()
        {
            MatrixObject matrix = component as MatrixObject;
            writer.WriteLine("        <MatrixRows>");
            writer.WriteLine("          <MatrixRow>");
            writer.WriteLine("            <Height>" + UnitsConverter.PixelsToMillimeters(matrix.Rows[1].Height) + "</Height>");
            writer.WriteLine("            <MatrixCells>");
            TableCell cell = matrix.GetCellData(1, 1).Cell;
            writer.WriteLine("              <MatrixCell>");
            writer.WriteLine("                <ReportItems>");
            writer.WriteLine("                  <Textbox Name=\"" + cell.Name + "\">");
            ReportComponentBase tempComponent = component;
            component = cell;
            WriteStyle("                    ");
            component = tempComponent;
            writer.WriteLine("                    <CanGrow>" + UnitsConverter.ConvertBool(cell.CanGrow) + "</CanGrow>");
            writer.WriteLine("                    <CanShrink>" + UnitsConverter.ConvertBool(cell.CanShrink) + "</CanShrink>");
            writer.WriteLine("                    <Value>" + cell.Text + "</Value>");
            writer.WriteLine("                  </Textbox>");
            writer.WriteLine("                </ReportItems>");
            writer.WriteLine("              </MatrixCell>");
            writer.WriteLine("            </MatrixCells>");
            writer.WriteLine("          </MatrixRow>");
            writer.WriteLine("        </MatrixRows>");
        }

        private void WriteReportItem()
        {
            writer.WriteLine("        <Top>" + UnitsConverter.PixelsToMillimeters(component.Top) + "</Top>");
            writer.WriteLine("        <Left>" + UnitsConverter.PixelsToMillimeters(component.Left) + "</Left>");
            writer.WriteLine("        <Height>" + UnitsConverter.PixelsToMillimeters(component.Height) + "</Height>");
            writer.WriteLine("        <Width>" + UnitsConverter.PixelsToMillimeters(component.Width) + "</Width>");
            WriteStyle("        ");
            WriteVisibility(component.Visible, "        ");
        }

        private void WriteLine(LineObject line)
        {
            writer.WriteLine("      <Line Name=\"" + line.Name + "\">");
            component = line;
            WriteReportItem();
            writer.WriteLine("      </Line>");
        }

        private void WriteRectangle(ShapeObject rectangle)
        {
            writer.WriteLine("      <Rectangle Name=\"" + rectangle.Name + "\">");
            component = rectangle;
            WriteReportItem();
            writer.WriteLine("      </Rectangle>");
        }

        private void WriteTextbox(TextObject textbox)
        {
            writer.WriteLine("      <Textbox Name=\"" + textbox.Name + "\">");
            component = textbox;
            WriteReportItem();
            writer.WriteLine("        <CanGrow>" + UnitsConverter.ConvertBool(textbox.CanGrow) + "</CanGrow>");
            writer.WriteLine("        <CanShrink>" + UnitsConverter.ConvertBool(textbox.CanShrink) + "</CanShrink>");
            writer.WriteLine("        <Value>" + ReplaceControlChars(textbox.Text) + "</Value>");
            writer.WriteLine("      </Textbox>");
        }

        private void WriteImage(PictureObject image)
        {
            writer.WriteLine("      <Image Name=\"" + image.Name + "\">");
            component = image;
            WriteReportItem();
            writer.WriteLine("        <Source>External</Source>");
            writer.WriteLine("        <Value>" + image.ImageLocation + "</Value>");
            writer.WriteLine("        <Sizing>" + UnitsConverter.ConvertSizeMode(image.SizeMode) + "</Sizing>");
            writer.WriteLine("      </Image>");
        }

        private void WriteSubreport(SubreportObject subreport)
        {
            writer.WriteLine("      <Subreport Name=\"" + subreport.Name + "\">");
            component = subreport;
            WriteReportItem();
            writer.WriteLine("        <ReportName>" + subreport.Name + "</ReportName>");
            writer.WriteLine("      </Subreport>");
        }

        private void WriteChart(MSChartObject chart)
        {
            writer.WriteLine("      <Chart Name=\"" + chart.Name + "\">");
            component = chart;
            WriteReportItem();
            writer.WriteLine("        <Type>" + UnitsConverter.ConvertChartType(chart.Series[0].SeriesSettings.ChartType) + "</Type>");
            WriteLegend();
            WriteCategoryAxis();
            WriteValueAxis();
            WriteTitle();
            writer.WriteLine("        <Palette>" + UnitsConverter.ConvertChartPalette(chart.Chart.Palette) + "</Palette>");
            WriteThreeDProperties();
            writer.WriteLine("        <CategoryGroupings>");
            writer.WriteLine("          <CategoryGrouping>");
            writer.WriteLine("            <DynamicCategories>");
            writer.WriteLine("              <Grouping Name=\"DynamicCategoriesGroup1\">");
            writer.WriteLine("                <GroupExpressions>");
            writer.WriteLine("                  <GroupExpression></GroupExpression>");
            writer.WriteLine("                </GroupExpressions>");
            writer.WriteLine("              </Grouping>");
            writer.WriteLine("            </DynamicCategories>");
            writer.WriteLine("          </CategoryGrouping>");
            writer.WriteLine("        </CategoryGroupings>");
            writer.WriteLine("      </Chart>");
        }

        private void WriteTable(TableObject table)
        {
            writer.WriteLine("      <Table Name=\"" + table.Name + "\">");
            component = table;
            WriteReportItem();
            WriteTableColumns();
            WriteHeader();
            WriteDetails();
            WriteFooter();
            writer.WriteLine("      </Table>");
        }

        private void WriteMatrix(MatrixObject matrix)
        {
            writer.WriteLine("      <Matrix Name=\"" + matrix.Name + "\">");
            component = matrix;
            WriteReportItem();
            WriteColumnGroupings();
            WriteRowGroupings();
            WriteMatrixColumns();
            WriteMatrixRows();
            writer.WriteLine("      </Matrix>");
        }

        private void WriteReportItems()
        {
            if (parent.AllObjects.Count != 0)
            {
                writer.WriteLine("    <ReportItems>");
                foreach (Object obj in parent.AllObjects)
                {
                    if (obj is LineObject)
                    {
                        WriteLine(obj as LineObject);
                    }
                    else if (obj is ShapeObject)
                    {
                        WriteRectangle(obj as ShapeObject);
                    }
                    else if (obj is TextObject)
                    {
                        if (!(obj is TableCell))
                        {
                            WriteTextbox(obj as TextObject);
                        }
                    }
                    else if (obj is PictureObject)
                    {
                        WriteImage(obj as PictureObject);
                    }
                    else if (obj is SubreportObject)
                    {
                        WriteSubreport(obj as SubreportObject);
                    }
                    else if (obj is MSChartObject)
                    {
                        WriteChart(obj as MSChartObject);
                    }
                    else if (obj is TableObject)
                    {
                        WriteTable(obj as TableObject);
                    }
                    else if (obj is MatrixObject)
                    {
                        WriteMatrix(obj as MatrixObject);
                    }
                    else
                    {
                        TextObject text = new TextObject();
                        ReportComponentBase c = obj as ReportComponentBase;
                        text.Name = c.Name;
                        text.Text = c.Name;
                        text.Top = c.Top;
                        text.Left = c.Left;
                        text.Width = c.Width;
                        text.Height = c.Height;
                        text.Visible = c.Visible;
                        WriteTextbox(text);
                    }
                }
                writer.WriteLine("    </ReportItems>");
            }
        }

        private void WritePageHeader()
        {
            writer.WriteLine("  <PageHeader>");
            string height = "0.0pt";
            string printOn = "";
            if (page.ReportTitle != null)
            {
                ReportTitleBand reportTitle = page.ReportTitle;
                height =  UnitsConverter.PixelsToMillimeters(reportTitle.Height);
                printOn = reportTitle.PrintOn.ToString();
                parent = reportTitle;
                WriteReportItems();
            }
            else if (page.PageHeader != null)
            {
                PageHeaderBand pageHeader = page.PageHeader;
                height = UnitsConverter.PixelsToMillimeters(pageHeader.Height);
                printOn = pageHeader.PrintOn.ToString();
                parent = pageHeader;
                WriteReportItems();
            }
            string printOnFPage = printOn.Contains(PrintOn.FirstPage.ToString()).ToString().ToLower();
            string printOnLPage = printOn.Contains(PrintOn.LastPage.ToString()).ToString().ToLower();
            writer.WriteLine("    <Height>" + height + "</Height>");
            writer.WriteLine("    <PrintOnFirstPage>" + printOnFPage + "</PrintOnFirstPage>");
            writer.WriteLine("    <PrintOnLastPage>" + printOnLPage + "</PrintOnLastPage>");
            writer.WriteLine("  </PageHeader>");
        }

        private void WriteBody()
        {
            writer.WriteLine("  <Body>");
            if ((page.Bands.Count != 0) && (page.Bands[0] is DataBand))
            {
                writer.WriteLine("    <Height>" + UnitsConverter.PixelsToMillimeters(page.Bands[0].Height) + "</Height>");
                parent = page.Bands[0] as DataBand;
                WriteReportItems();
            }
            else
            {
                writer.WriteLine("    <Height>" + "0.0pt" + "</Height>");
            }
            writer.WriteLine("  </Body>");
        }

        private void WritePageFooter()
        {
            writer.WriteLine("  <PageFooter>");
            PageFooterBand pageFooter = page.PageFooter;
            if (pageFooter != null)
            {
                writer.WriteLine("    <Height>" + UnitsConverter.PixelsToMillimeters(pageFooter.Height) + "</Height>");
                string printOn = pageFooter.PrintOn.ToString();
                bool printOnFirstPage = printOn.Contains(PrintOn.FirstPage.ToString());
                bool printOnLastPage = printOn.Contains(PrintOn.LastPage.ToString());
                writer.WriteLine("    <PrintOnFirstPage>" + printOnFirstPage.ToString().ToLower() + "</PrintOnFirstPage>");
                writer.WriteLine("    <PrintOnLastPage>" + printOnLastPage.ToString().ToLower() + "</PrintOnLastPage>");
                parent = pageFooter;
                WriteReportItems();
            }
            else
            {
                writer.WriteLine("    <Height>" + "0.0pt" + "</Height>");
                writer.WriteLine("    <PrintOnFirstPage>" + "true" + "</PrintOnFirstPage>");
                writer.WriteLine("    <PrintOnLastPage>" + "true" + "</PrintOnLastPage>");
            }
            writer.WriteLine("  </PageFooter>");
        }

        private void WritePageMargins()
        {
            writer.WriteLine("  <BottomMargin>" + UnitsConverter.MillimetersToString(page.BottomMargin) + "</BottomMargin>");
            writer.WriteLine("  <LeftMargin>" + UnitsConverter.MillimetersToString(page.LeftMargin) + "</LeftMargin>");
            writer.WriteLine("  <RightMargin>" + UnitsConverter.MillimetersToString(page.RightMargin) + "</RightMargin>");
            writer.WriteLine("  <TopMargin>" + UnitsConverter.MillimetersToString(page.TopMargin) + "</TopMargin>");
        }

        private void WriteReport()
        {
            writer.WriteLine("  <Description>" + ReplaceControlChars(Report.ReportInfo.Description) + "</Description>");
            writer.WriteLine("  <Author>" + Report.ReportInfo.Author + "</Author>");
            WritePageSize();
            WritePageHeader();
            WriteBody();
            WritePageFooter();
            writer.WriteLine("  <Width>" + UnitsConverter.MillimetersToString(page.PaperWidth) + "</Width>");
            WritePageMargins();
        }

        #endregion // Private Methods

        #region Protected Methods

        /// <inheritdoc/>
        protected override string GetFilter()
        {
            return new FastReport.Utils.MyRes("FileFilters").Get("RdlFile");
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void SaveReport(Report report, string filename)
        {
            DialogResult result = MessageBox.Show(new FastReport.Utils.MyRes("Messages").Get("WarningSaveRDL"),
                new FastReport.Utils.MyRes("Messages").Get("Attention"), MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Report = report;
                page = Report.Pages[0] as ReportPage;
                if (page != null)
                {
                    using (writer = new StreamWriter(new FileStream(filename, FileMode.Create)))
                    {
                        writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        writer.WriteLine("<Report xmlns:rd=\"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition\" xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition\">");
                        WriteReport();
                        writer.WriteLine("</Report>");
                    }
                }
            }
        }

        #endregion // Public Methods
    }
}
