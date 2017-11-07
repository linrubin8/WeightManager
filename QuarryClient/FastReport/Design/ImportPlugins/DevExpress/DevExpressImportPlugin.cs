using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using FastReport.Barcode;

namespace FastReport.Design.ImportPlugins.DevExpress
{
    /// <summary>
    /// Represents the DevExpess import plubin.
    /// </summary>
    class DevExpressImportPlugin : ImportPlugin
    {
        #region Constants

        private const string TOP_MARGIN_BAND_MASK    = "DevExpress.XtraReports.UI.TopMarginBand";
        private const string BOTTOM_MARGIN_BAND_MASK = "DevExpress.XtraReports.UI.BottomMarginBand";
        private const string REPORT_HEADER_BAND_MASK = "DevExpress.XtraReports.UI.ReportHeaderBand";
        private const string REPORT_FOOTER_BAND_MASK = "DevExpress.XtraReports.UI.ReportFooterBand";
        private const string DETAIL_BAND_MASK        = "DevExpress.XtraReports.UI.DetailBand";

        private const string BAND_CHILD_DEFINITION   = "new DevExpress.XtraReports.UI.XRControl[]";

        private const string DEV_EXPRESS_LABEL       = "DevExpress.XtraReports.UI.XRLabel";
        private const string DEV_EXPRESS_LINE        = "DevExpress.XtraReports.UI.XRLine";
        private const string DEV_EXPRESS_TABLE       = "DevExpress.XtraReports.UI.XRTable";
        private const string DEV_EXPRESS_PICTURE     = "DevExpress.XtraReports.UI.XRPictureBox";
        private const string DEV_EXPRESS_PAGE_INFO   = "DevExpress.XtraReports.UI.XRPageInfo";
        private const string DEV_EXPRESS_SHAPE       = "DevExpress.XtraReports.UI.XRShape";
        private const string DEV_EXPRESS_ZIP_CODE    = "DevExpress.XtraReports.UI.XRZipCode";
        private const string DEV_EXPRESS_BAR_CODE    = "DevExpress.XtraReports.UI.XRBarCode";

        #endregion // Constants

        #region Fields

        private ReportPage page;
        private string devText;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DevExpressImportPlugin"/> class.
        /// </summary>
        public DevExpressImportPlugin() : base()
        {
            devText = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevExpressImportPlugin"/> class with a specified designer.
        /// </summary>
        /// <param name="designer">The report designer.</param>
        public DevExpressImportPlugin(Designer designer) : base(designer)
        {
            devText = "";
        }

        #endregion // Constructors

        #region Private Methods

        private string FindObjectName(string mask)
        {
            string name = "";
            int start = devText.IndexOf(mask);
            if (start > -1)
            {
                start += mask.Length;
                int length = devText.IndexOf(";", start) - start;
                name = devText.Substring(start, length).Trim();
            }
            return name;
        }

        private string GetObjectDescription(string name)
        {
            string description = "";
            int start = devText.IndexOf(@"// " + name);
            if (start > -1)
            {
                start = devText.IndexOf(@"//", start + 2);
                int length = devText.IndexOf(@"//", start + 2) - start + 2;
                description = devText.Substring(start, length);
            }
            return description;
        }

        private string GetPropertyValue(string name, string description)
        {
            string value = "";
            int start = description.IndexOf("." + name + " ");
            if (start > -1)
            {
                start += name.Length + 3;
                int length = description.IndexOf(";", start) - start;
                value = description.Substring(start, length).Trim();
            }
            return value;
        }

        private void LoadBand(BandBase band, string description)
        {
            band.Height = UnitsConverter.SizeFToPixels(GetPropertyValue("HeightF", description));
            band.FillColor = UnitsConverter.ConvertBackColor(GetPropertyValue("BackColor", description));
        }

        private List<string> GetObjectNames(string description)
        {
            List<string> names = new List<string>();
            int start = description.IndexOf(BAND_CHILD_DEFINITION);
            if (start > -1)
            {
                start += BAND_CHILD_DEFINITION.Length;
                int end = description.IndexOf("});", start);
                string namesStr = description.Substring(start, end - start + 1).Replace("}", ",");
                int pos = 0;
                while (pos < end)
                {
                    pos = namesStr.IndexOf("this.", pos);
                    if (pos < 0)
                    {
                        break;
                    }
                    names.Add(namesStr.Substring(pos + 5, namesStr.IndexOf(",", pos) - pos - 5));
                    pos += 5;
                }
            }
            return names;
        }

        private string GetObjectType(string name)
        {
            string str = "this." + name + " = new ";
            int start = devText.IndexOf(str) + str.Length;
            int end = devText.IndexOf("();", start);
            return devText.Substring(start, end - start);
        }

        private void LoadComponent(string description, ComponentBase comp)
        {
            comp.Name = GetPropertyValue("Name", description).Replace("\"", "");
            string location = GetPropertyValue("LocationFloat", description);
            if (!String.IsNullOrEmpty(location))
            {
                int start = location.IndexOf("(");
                int comma = location.IndexOf(",", start);
                int end = location.IndexOf(")");
                comp.Left = UnitsConverter.SizeFToPixels(location.Substring(start + 1, comma - start));
                comp.Top = UnitsConverter.SizeFToPixels(location.Substring(comma + 2, end - comma - 1));
            }
        }

        private void LoadBorder(string description, Border border)
        {
            string borders = GetPropertyValue("Borders", description);
            if (!String.IsNullOrEmpty(borders))
            {
                if (borders.IndexOf("Left") > -1)
                {
                    border.Lines |= BorderLines.Left;
                }
                if (borders.IndexOf("Top") > -1)
                {
                    border.Lines |= BorderLines.Top;
                }
                if (borders.IndexOf("Right") > -1)
                {
                    border.Lines |= BorderLines.Right;
                }
                if (borders.IndexOf("Bottom") > -1)
                {
                    border.Lines |= BorderLines.Bottom;
                }
            }
            string color = GetPropertyValue("BorderColor", description);
            if (!String.IsNullOrEmpty(color))
            {
                border.Color = UnitsConverter.ConvertColor(color);
            }
            string style = GetPropertyValue("BorderDashStyle", description);
            if (!String.IsNullOrEmpty(style))
            {
                border.Style = UnitsConverter.ConvertBorderDashStyle(style);
            }
            string width = GetPropertyValue("BorderWidth", description);
            if (!String.IsNullOrEmpty(width))
            {
                border.Width = UnitsConverter.SizeFToPixels(width);
            }
        }

        private void LoadSize(string description, ComponentBase comp)
        {
            string size = GetPropertyValue("SizeF", description);
            if (!String.IsNullOrEmpty(size))
            {
                int start = size.IndexOf("(");
                int comma = size.IndexOf(",", start);
                int end = size.IndexOf(")");
                comp.Width = UnitsConverter.SizeFToPixels(size.Substring(start + 1, comma - start));
                comp.Height = UnitsConverter.SizeFToPixels(size.Substring(comma + 2, end - comma - 1));
            }
        }

        private Font LoadFont(string description)
        {
            string font = GetPropertyValue("Font", description);
            if (!String.IsNullOrEmpty(font))
            {
                int start = font.IndexOf("(");
                int comma = font.IndexOf(",", start);
                int secondComma = font.IndexOf(",", comma + 1);
                string fontFamily = font.Substring(start + 2, comma - start - 3);
                float fontSize = 10.0f;
                if (secondComma > -1)
                {
                    string str = font.Substring(comma + 2, secondComma - comma - 2);
                    fontSize = UnitsConverter.SizeFToPixels(font.Substring(comma + 2, secondComma - comma - 2));
                    FontStyle fontStyle = FontStyle.Regular;
                    if (font.Contains("Bold"))
                    {
                        fontStyle |= FontStyle.Bold;
                    }
                    if (font.Contains("Italic"))
                    {
                        fontStyle |= FontStyle.Italic;
                    }
                    if (font.Contains("Underline"))
                    {
                        fontStyle |= FontStyle.Underline;
                    }
                    if (font.Contains("Strikeout"))
                    {
                        fontStyle |= FontStyle.Strikeout;
                    }
                    return new Font(fontFamily, fontSize, fontStyle);
                }
                else
                {
                    string str = font.Substring(comma + 2, font.IndexOf(")") - comma - 2);
                    fontSize = UnitsConverter.SizeFToPixels(font.Substring(comma + 2, font.IndexOf(")") - comma - 2));
                }
            }
            return new Font("Arial", 10.0f, FontStyle.Regular);
        }

        private void LoadLabel(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            TextObject text = ComponentsFactory.CreateTextObject(name, parent);
            LoadComponent(description, text);
            LoadSize(description, text);
            LoadBorder(description, text.Border);
            text.FillColor = UnitsConverter.ConvertBackColor(GetPropertyValue("BackColor", description));
            text.TextColor = UnitsConverter.ConvertColor(GetPropertyValue("ForeColor", description));
            text.Text = GetPropertyValue("Text", description).Replace("\"", "");
            text.HorzAlign = UnitsConverter.ConvertTextAlignmentToHorzAlign(GetPropertyValue("TextAlignment", description));
            text.VertAlign = UnitsConverter.ConvertTextAlignmentToVertAlign(GetPropertyValue("TextAlignment", description));
            text.Font = LoadFont(description);
        }

        private void LoadLine(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            LineObject line = ComponentsFactory.CreateLineObject(name, parent);
            LoadComponent(description, line);
            LoadSize(description, line);
            line.Border.Color = UnitsConverter.ConvertColor(GetPropertyValue("ForeColor", description));
            line.Border.Style = UnitsConverter.ConvertLineStyle(GetPropertyValue("LineStyle", description));
            string width = GetPropertyValue("LineWidth", description);
            if (!String.IsNullOrEmpty(width))
            {
                line.Border.Width = Convert.ToSingle(width);
            }
        }

        private void LoadPicture(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            PictureObject picture = ComponentsFactory.CreatePictureObject(name, parent);
            LoadComponent(description, picture);
            LoadSize(description, picture);
            picture.SizeMode = UnitsConverter.ConvertImageSizeMode(GetPropertyValue("Sizing", description));
        }

        private void LoadShape(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            ShapeObject shape = ComponentsFactory.CreateShapeObject(name, parent);
            LoadComponent(description, shape);
            LoadSize(description, shape);
            shape.Border.Color = UnitsConverter.ConvertColor(GetPropertyValue("ForeColor", description));
            shape.FillColor = UnitsConverter.ConvertBackColor(GetPropertyValue("FillColor", description));
            shape.Shape = UnitsConverter.ConvertShape(GetPropertyValue("Shape", description));
            string width = GetPropertyValue("LineWidth", description);
            if (!String.IsNullOrEmpty(width))
            {
                shape.Border.Width = Convert.ToSingle(width);
            }
        }

        private void LoadZipCode(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            ZipCodeObject zipCode = ComponentsFactory.CreateZipCodeObject(name, parent);
            LoadComponent(description, zipCode);
            LoadSize(description, zipCode);
            zipCode.FillColor = UnitsConverter.ConvertBackColor(GetPropertyValue("BackColor", description));
            zipCode.Border.Color = UnitsConverter.ConvertColor(GetPropertyValue("ForeColor", description));
            zipCode.Text = GetPropertyValue("Text", description).Replace("\"", "");
        }

        private void LoadBarCode(string name, Base parent)
        {
            string description = GetObjectDescription(name);
            BarcodeObject barcode = ComponentsFactory.CreateBarcodeObject(name, parent);
            LoadComponent(description, barcode);
            LoadSize(description, barcode);
            LoadBorder(description, barcode.Border);
            barcode.FillColor = UnitsConverter.ConvertBackColor(GetPropertyValue("BackColor", description));
            UnitsConverter.ConvertBarcodeSymbology(GetPropertyValue("Symbology", description), barcode);
        }

        private void LoadObjects(string description, Base parent)
        {
            List<string> names = GetObjectNames(description);
            foreach (string name in names)
            {
                string type = GetObjectType(name);
                if (!String.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case DEV_EXPRESS_LABEL:
                            LoadLabel(name, parent);
                            break;
                        case DEV_EXPRESS_PAGE_INFO:
                            LoadLabel(name, parent);
                            break;
                        case DEV_EXPRESS_LINE:
                            LoadLine(name, parent);
                            break;
                        case DEV_EXPRESS_PICTURE:
                            LoadPicture(name, parent);
                            break;
                        case DEV_EXPRESS_SHAPE:
                            LoadShape(name, parent);
                            break;
                        case DEV_EXPRESS_ZIP_CODE:
                            LoadZipCode(name, parent);
                            break;
                        case DEV_EXPRESS_BAR_CODE:
                            LoadBarCode(name, parent);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void LoadTopMarginBand() // Page Header
        {
            string name = FindObjectName(TOP_MARGIN_BAND_MASK);
            if (!String.IsNullOrEmpty(name))
            {
                PageHeaderBand header = ComponentsFactory.CreatePageHeaderBand(page);
                string description = GetObjectDescription(name);
                LoadBand(header, description);
                LoadObjects(description, header);
            }
        }

        private void LoadBottomMarginBand() // Page Footer
        {
            string name = FindObjectName(BOTTOM_MARGIN_BAND_MASK);
            if (!String.IsNullOrEmpty(name))
            {
                PageFooterBand footer = ComponentsFactory.CreatePageFooterBand(page);
                string description = GetObjectDescription(name);
                LoadBand(footer, description);
                LoadObjects(description, footer);
            }
        }

        private void LoadReportHeaderBand() // Report Title
        {
            string name = FindObjectName(REPORT_HEADER_BAND_MASK);
            if (!String.IsNullOrEmpty(name))
            {
                ReportTitleBand title = ComponentsFactory.CreateReportTitleBand(page);
                string description = GetObjectDescription(name);
                LoadBand(title, description);
                LoadObjects(description, title);
            }
        }

        private void LoadReportFooterBand() // Report Summary
        {
            string name = FindObjectName(REPORT_FOOTER_BAND_MASK);
            if (!String.IsNullOrEmpty(name))
            {
                ReportSummaryBand summary = ComponentsFactory.CreateReportSummaryBand(page);
                string description = GetObjectDescription(name);
                LoadBand(summary, description);
                LoadObjects(description, summary);
            }
        }

        private void LoadDetailBand() // Data
        {
            string name = FindObjectName(DETAIL_BAND_MASK);
            if (!String.IsNullOrEmpty(name))
            {
                DataBand data = ComponentsFactory.CreateDataBand(page);
                string description = GetObjectDescription(name);
                LoadBand(data, description);
                LoadObjects(description, data);
            }
        }

        private void LoadReport()
        {
            page = ComponentsFactory.CreateReportPage(Report);
            LoadTopMarginBand();
            LoadBottomMarginBand();
            LoadReportHeaderBand();
            LoadReportFooterBand();
            LoadDetailBand();
        }

        #endregion // Private Methods

        #region Protected Methods

        ///<inheritdoc/>
        protected override string GetFilter()
        {
            return new FastReport.Utils.MyRes("FileFilters").Get("DevExpressFiles");
        }

        #endregion // Protected Methods

        #region Public Methods

        public override void LoadReport(Report report, string filename)
        {
            Report = report;
            Report.Clear();
            devText = File.ReadAllText(filename);
            devText = devText.Remove(0, devText.IndexOf("namespace")).Replace(@"http://", "       ");
            LoadReport();
            page = null;
        }

        #endregion // Public Methods
    }
}
