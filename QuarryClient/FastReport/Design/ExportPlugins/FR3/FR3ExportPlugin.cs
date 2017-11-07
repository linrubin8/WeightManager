using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using FastReport.Table;
using FastReport.Matrix;
using FastReport.Barcode;
using FastReport.MSChart;

namespace FastReport.Design.ExportPlugins.FR3
{
    /// <summary>
    /// Represents the FR3 export plugin.
    /// </summary>
    public class FR3ExportPlugin : ExportPlugin
    {
        #region Fields

        private StreamWriter writer;
        //private ReportPage page;
        //private Base parent;
        //private ReportComponentBase component;

        #endregion // Fields

        #region Properties
        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FR3ExportPlugin"/> class.
        /// </summary>
        public FR3ExportPlugin() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FR3ExportPlugin"/> class with a specified designer.
        /// </summary>
        /// <param name="designer">The report designer.</param>
        public FR3ExportPlugin(Designer designer) : base(designer)
        {
        }

        #endregion // Constructors

        #region Private Methods

        private string ReplaceControlChars(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "&apos;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace(Environment.NewLine, "&#13;&#10;");
            return str;
        }

        private void WriteEngineOptions()
        {
            writer.Write(" EngineOptions.DoublePass=\"" + Report.DoublePass.ToString() + "\"");
            writer.Write(" EngineOptions.UseFileCache=\"" + Report.UseFileCache.ToString() + "\"");
        }

        private void WriteReportOptions()
        {
            writer.Write(" ReportOptions.Author=\"" + Report.ReportInfo.Author + "\"");
            writer.Write(" ReportOptions.Description.Text=\"" + ReplaceControlChars(Report.ReportInfo.Description) + "\"");
        }

        private void WriteDataPage()
        {
            writer.WriteLine("  <TfrxDataPage Name=\"Data\" Height=\"1000\" Left=\"0\" Top=\"0\" Width=\"1000\"/>");
        }

        private void WriteBorder(Border border)
        {
            writer.Write(" Frame.Typ=\"" + "" + "\"");
            writer.Write(" Frame.LeftLine.Color=\"" + UnitsConverter.ColorToTColor(border.LeftLine.Color) + "\"");
            writer.Write(" Frame.TopLine.Color=\"" + UnitsConverter.ColorToTColor(border.TopLine.Color) + "\"");
            writer.Write(" Frame.RightLine.Color=\"" + UnitsConverter.ColorToTColor(border.RightLine.Color) + "\"");
            writer.Write(" Frame.BottomLine.Color=\"" + UnitsConverter.ColorToTColor(border.BottomLine.Color) + "\"");
            writer.Write(" Frame.LeftLine.Style=\"" + UnitsConverter.ConvertLineStyle(border.LeftLine.Style) + "\"");
            writer.Write(" Frame.TopLine.Style=\"" + UnitsConverter.ConvertLineStyle(border.TopLine.Style) + "\"");
            writer.Write(" Frame.RightLine.Style=\"" + UnitsConverter.ConvertLineStyle(border.RightLine.Style) + "\"");
            writer.Write(" Frame.BottomLine.Style=\"" + UnitsConverter.ConvertLineStyle(border.BottomLine.Style) + "\"");
        }

        private void WriteObject(ReportComponentBase obj, string type)
        {
            writer.Write("      <" + type + " Name=\"" + obj.Name + "\"");
            writer.Write(" Height=\"" + obj.Height.ToString() + "\"");
            writer.Write(" Left=\"" + obj.Left.ToString() + "\"");
            writer.Write(" Top=\"" + obj.Top.ToString() + "\"");
            writer.Write(" Width=\"" + obj.Width.ToString() + "\"");
            writer.Write(" Visible=\"" + obj.Visible.ToString() + "\"");
        }

        private void WriteTextObject(TextObject text)
        {
            WriteObject(text, "TfrxMemoView");
            writer.Write(" Color=\"" + UnitsConverter.ColorToTColor(text.FillColor) + "\"");
            writer.Write(" Font.Charset=\"1\"");
            writer.Write(" Font.Color=\"" + UnitsConverter.ColorToTColor(text.TextColor) + "\"");
            writer.Write(" Font.Height=\"" + UnitsConverter.ConvertFontSize(text.Font.Size) + "\"");
            writer.Write(" Font.Name=\"" + text.Font.Name + "\"");
            writer.Write(" Font.Style=\"" + UnitsConverter.ConvertFontStyle(text.Font.Style) + "\"");
            writer.Write(" HAlign=\"" + UnitsConverter.ConvertHorzAlign(text.HorzAlign) + "\"");
            writer.Write(" ParentFont=\"" + "False" + "\"");
            writer.Write(" VAlign=\"" + UnitsConverter.ConvertVertAlign(text.VertAlign) + "\"");
            writer.Write(" Text=\"" + ReplaceControlChars(text.Text) + "\"");
            writer.WriteLine("/>");
        }

        private void WritePictureObject(PictureObject pic)
        {
            WriteObject(pic, "TfrxPictureView");
            writer.WriteLine("/>");
        }

        private void WriteLineObject(LineObject line)
        {
            WriteObject(line, "TfrxLineView");
            writer.WriteLine("/>");
        }

        private void WriteTableObject(TableObject table)
        {
        }

        private void WriteMatrixObject(MatrixObject matrix)
        {
        }

        private void WriteChartMSChartObject(MSChartObject chart)
        {
        }

        private void WriteObjects(BandBase band)
        {
            foreach (ReportComponentBase c in band.Objects)
            {
                if (c is TextObject)
                {
                    WriteTextObject(c as TextObject);
                }
                else if (c is PictureObject)
                {
                    WritePictureObject(c as PictureObject);
                }
                else if (c is LineObject)
                {
                    WriteLineObject(c as LineObject);
                }
                else if (c is ShapeObject)
                {
                }
                else if (c is SubreportObject)
                {
                }
                else if (c is TableObject)
                {
                }
                else if (c is MatrixObject)
                {
                }
                else if (c is BarcodeObject)
                {
                }
                else if (c is RichObject)
                {
                }
                else if (c is CheckBoxObject)
                {
                }
                else if (c is MSChartObject)
                {
                }
                else if (c is ZipCodeObject)
                {
                }
                else if (c is CellularTextObject)
                {
                }
            }
        }

        private void WriteChild(ChildBand child)
        {
            if (child != null)
            {
                WriteBand(child, "TfrxChild");
            }
        }

        private void WriteBand(BandBase band, string type)
        {
            writer.Write("    <" + type + " Name=\"" + band.Name + "\"");
            writer.Write(" Height=\"" + band.Height.ToString() + "\"");
            writer.Write(" Left=\"" + band.Left.ToString() + "\"");
            writer.Write(" Top=\"" + band.Top.ToString() + "\"");
            writer.Write(" Width=\"" + band.Width.ToString() + "\"");
            if (band.Child != null)
            {
                writer.Write(" Child=\"" + band.Child.Name + "\"");
            }
            writer.WriteLine(">");
            WriteObjects(band);
            writer.WriteLine("    </" + type + ">");
            WriteChild(band.Child);
        }

        private void WriteReportTitle(ReportTitleBand title)
        {
            if (title != null)
            {
                WriteBand(title, "TfrxReportTitle");
            }
        }

        private void WritePageHeader(PageHeaderBand header)
        {
            if (header != null)
            {
                WriteBand(header, "TfrxPageHeader");
            }
        }

        private void WriteDataBands(BandCollection bands)
        {
            foreach (BandBase band in bands)
                WriteBand(band);
        }

        private void WriteBand(BandBase band)
        {
                if (band is DataBand)
                    WriteBand(band, "TfrxMasterData");
                else if (band is GroupHeaderBand)
                    WriteBand(band, "TfrxGroupHeader");
                else if (band is GroupFooterBand)
                    WriteBand(band, "TfrxGroupFooter");
            if (band.ChildObjects.Count > 0)
            {
                foreach (object b in band.ChildObjects)
                    if(b is BandBase)
                        WriteBand(b as BandBase);
            }
        }

        private void WritePageFooter(PageFooterBand footer)
        {
            if (footer != null)
            {
                WriteBand(footer, "TfrxPageFooter");
            }
        }

        private void WriteReportSummary(ReportSummaryBand summary)
        {
            if (summary != null)
            {
                WriteBand(summary, "TfrxReportSummary");
            }
        }

        private void WriteBands(ReportPage page)
        {
            WriteReportTitle(page.ReportTitle);
            WritePageHeader(page.PageHeader);
            WriteDataBands(page.Bands);
            WritePageFooter(page.PageFooter);
            WriteReportSummary(page.ReportSummary);
        }

        private void WriteReportPage(ReportPage page)
        {
            writer.Write("  <TfrxReportPage Name=\"" + page.Name + "\"");
            writer.Write(" PaperWidth=\"" + page.PaperWidth.ToString() + "\"");
            writer.Write(" PaperHeight=\"" + page.PaperHeight.ToString() + "\"");
            writer.Write(" PaperSize=\"" + page.RawPaperSize.ToString() + "\"");
            writer.Write(" LeftMargin=\"" + page.LeftMargin.ToString() + "\"");
            writer.Write(" RightMargin=\"" + page.RightMargin.ToString() + "\"");
            writer.Write(" TopMargin=\"" + page.TopMargin.ToString() + "\"");
            writer.Write(" BottomMargin=\"" + page.BottomMargin.ToString() + "\"");
            writer.Write(" ColumnWidth=\"" + page.Columns.Width.ToString() + "\"");
            writer.Write(" ColumnPositions.Text=\"" + "" + "\"");
            writer.Write(" HGuides.Text=\"" + "" + "\"");
            writer.Write(" VGuides.Text=\"" + "" + "\"");
            writer.WriteLine(">");
            WriteBands(page);
            writer.WriteLine("  </TfrxReportPage>");
        }

        private void WriteReportPages()
        {
            foreach (ReportPage p in Report.Pages)
            {
                WriteReportPage(p);
            }
        }

        private void WriteReport()
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?>");
            writer.Write("<TfrxReport Version=\"4.8.222\" DotMatrixReport=\"False\"");
            WriteEngineOptions();
            WriteReportOptions();
            writer.WriteLine(">");
            WriteDataPage();
            WriteReportPages();
            writer.WriteLine("</TfrxReport>");
        }

        #endregion // Private Methods

        #region Protected Methods

        /// <inheritdoc/>
        protected override string GetFilter()
        {
            return new FastReport.Utils.MyRes("FileFilters").Get("Fr3File");
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void SaveReport(Report report, string filename)
        {
            Report = report;
            using (writer = new StreamWriter(new FileStream(filename, FileMode.Create)))
            {
                WriteReport();
            }
        }

        #endregion // Public Methods
    }
}
