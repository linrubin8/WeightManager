using FastReport.Export.TTF;
using FastReport.Forms;
using FastReport.Table;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Export.Pdf
{
    /// <summary>
    /// PDF export (Adobe Acrobat)
    /// </summary>
    public partial class PDFExport : ExportBase
    {
        #region Private Constants

        const float PDF_DIVIDER = 0.75f;
        const float PDF_PAGE_DIVIDER = 2.8346400000000003f; // mm to point
        const int PDF_PRINTOPT = 3;
        readonly float PDF_TTF_DIVIDER = 1 / (750 * 96f / DrawUtils.ScreenDpi);

        #endregion

        /// <summary>
        /// Embedded File
        /// </summary>
        public class EmbeddedFile
        {
            private string name;
            private string description;
            private DateTime modDate;
            private EmbeddedRelation relation;
            private string mime;
            private Stream fileStream;
            private long xref;
            private ZUGFeRD_ConformanceLevel zUGFeRD_ConformanceLevel;

            /// <summary>
            /// Name of embedded file.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// Description of embedded file.
            /// </summary>
            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            /// <summary>
            /// Modify Date of embedded file.
            /// </summary>
            public DateTime ModDate
            {
                get { return modDate; }
                set { modDate = value; }
            }

            /// <summary>
            /// Relationship between the embedded document and the PDF part.
            /// </summary>
            public EmbeddedRelation Relation
            {
                get { return relation; }
                set { relation = value; }
            }

            /// <summary>
            /// Valid MIME type. 
            /// </summary>
            public string MIME
            {
                get { return mime; }
                set { mime = value; }
            }

            /// <summary>
            /// Stream of embedded file.
            /// </summary>
            public Stream FileStream
            {
                get { return fileStream; }
                set { fileStream = value; }
            }

            /// <summary>
            /// File reference.
            /// </summary>
            public long Xref
            {
                get { return xref; }
                set { xref = value; }
            }

            /// <summary>
            /// ZUGFeRD Conformance Level.
            /// </summary>
            public ZUGFeRD_ConformanceLevel ZUGFeRDConformanceLevel
            {
                get { return zUGFeRD_ConformanceLevel; }
                set { zUGFeRD_ConformanceLevel = value; }
            }

            /// Initializes a new instance of the <see cref="EmbeddedFile"/> class.
            public EmbeddedFile()
            {
                modDate = DateTime.Now;
                relation = EmbeddedRelation.Alternative;
                zUGFeRD_ConformanceLevel = ZUGFeRD_ConformanceLevel.BASIC;
                mime = "text/xml";
                fileStream = null;
            }
        }

        #region Public Enums

        /// <summary>
        /// Default preview size.
        /// </summary>
        public enum MagnificationFactor
        {
            /// <summary>
            /// Actual size
            /// </summary>
            ActualSize = 0,

            /// <summary>
            /// Fit Page
            /// </summary>
            FitPage = 1,

            /// <summary>
            /// Fit Width
            /// </summary>
            FitWidth = 2,

            /// <summary>
            /// Default
            /// </summary>
            Default = 3,

            /// <summary>
            /// 10%
            /// </summary>
            Percent_10 = 4,

            /// <summary>
            /// 25%
            /// </summary>
            Percent_25 = 5,

            /// <summary>
            /// 50%
            /// </summary>
            Percent_50 = 6,

            /// <summary>
            /// 75%
            /// </summary>
            Percent_75 = 7,

            /// <summary>
            /// 100%
            /// </summary>
            Percent_100 = 8,

            /// <summary>
            /// 125%
            /// </summary>
            Percent_125 = 9,

            /// <summary>
            /// 150%
            /// </summary>
            Percent_150 = 10,

            /// <summary>
            /// 200%
            /// </summary>
            Percent_200 = 11,

            /// <summary>
            /// 400%
            /// </summary>
            Percent_400 = 12,

            /// <summary>
            /// 800%
            /// </summary>
            Percent_800 = 13,
        }

        /// <summary>
        /// Standard of PDF format.
        /// </summary>
        public enum PdfStandard
        {
            /// <summary>
            /// PDF 1.5
            /// </summary>
            None = 0,

            /// <summary>
            /// PDF/A-2a
            /// </summary>
            PdfA_2a = 1,

            /// <summary>
            /// PDF/A-2b
            /// </summary>
            PdfA_2b = 2,

            /// <summary>
            /// PDF/A-3a
            /// </summary>
            PdfA_3a = 3,

            /// <summary>
            /// PDF/A-3b
            /// </summary>
            PdfA_3b = 4,

            /// <summary>
            /// Pdf/X-3
            /// </summary>
            PdfX_3 = 5,

            /// <summary>
            /// Pdf/X-4
            /// </summary>
            PdfX_4 = 6,
        }

        /// <summary>
        /// Color Space.
        /// </summary>
        public enum PdfColorSpace
        {
            /// <summary>
            /// RGB color space
            /// </summary>
            RGB = 0,

            /// <summary>
            /// CMYK color space
            /// </summary>
            CMYK = 1,
        }

        /// <summary>
        /// Types of pdf export.
        /// </summary>
        public enum ExportType
        {
            /// <summary>
            /// Simple export
            /// </summary>
            Export,
            /// <summary>
            /// Web print mode
            /// </summary>
            WebPrint
        }

        /// <summary>
        /// Relationship between the embedded document and the PDF part.
        /// </summary>
        public enum EmbeddedRelation
        {
            /// <summary>
            /// The embedded file contains data which is used for the visual representation.
            /// </summary>
            Data,
            /// <summary>
            /// The embedded file contains the source data for the visual representation derived therefrom in the PDF part.
            /// </summary>
            Source,
            /// <summary>
            /// This data relationship should be used if the embedded data are an alternative representation of the PDF contents.
            /// </summary>
            Alternative,
            /// <summary>
            /// This data relationship is used if the embedded file serves neither as the source nor as the alternative representation, but the file contains additional information.
            /// </summary>
            Supplement,
            /// <summary>
            /// If none of the data relationships above apply or there is an unknown data relationship, this data relationship is used.
            /// </summary>
            Unspecified
        }

        /// <summary>
        /// ZUGFeRD Conformance Level.
        /// </summary>
        public enum ZUGFeRD_ConformanceLevel
        {
            /// <summary>
            /// Basic level.
            /// </summary>
            BASIC,
            /// <summary>
            /// Comfort level.
            /// </summary>
            COMFORT,
            /// <summary>
            /// Extended level.
            /// </summary>
            EXTENDED
        }
        #endregion

        #region Private Fields

        // Options
        private PdfStandard FPdfCompliance = PdfStandard.None;
        private bool FEmbeddingFonts = true;
        private bool FBackground = true;
        private bool FTextInCurves = false;
        private PdfColorSpace FColorSpace = PdfColorSpace.RGB;
        private bool FImagesOriginalResolution = false;
        private bool FPrintOptimized = true;
        private bool FJpegCompression = false;
        private int FJpegQuality = 95;
        // end

        // Document Information
        private string FTitle = "";
        private string FAuthor = "";
        private string FSubject = "";
        private string FKeywords = "";
        private string FCreator = "FastReport";
        private string FProducer = "FastReport.NET";
        // end

        // Security
        private string FOwnerPassword = "";
        private string FUserPassword = "";
        private bool FAllowPrint = true;
        private bool FAllowModify = true;
        private bool FAllowCopy = true;
        private bool FAllowAnnotate = true;
        // end

        // Viewer
        private bool FShowPrintDialog = false;
        private bool FHideToolbar = false;
        private bool FHideMenubar = false;
        private bool FHideWindowUI = false;
        private bool FFitWindow = false;
        private bool FCenterWindow = true;
        private bool FPrintScaling = false;
        private bool FOutline = true;
        private MagnificationFactor FDefaultZoom = MagnificationFactor.ActualSize;
        // end

        // Other options
        private bool FDisplayDocTitle = true;
        private bool FEncrypted = false;
        private bool FCompressed = true;
        // Simon: private bool FTransparentImages = true 图片会出现一边线
        private bool FTransparentImages = false;
        private int FRichTextQuality = 95;
        private int FDefaultPage = 1;
        private float FDpiFX = 96f / DrawUtils.ScreenDpi;
        private bool FBuffered = false;
        private byte[] FColorProfile = null;
        private List<EmbeddedFile> embeddedFiles;
        // end

        // Internal fields
        private string FFileID;
        private long FRootNumber;
        private long FPagesNumber;
        private long FOutlineNumber;
        private long FInfoNumber;
        private long FStartXRef;
        private long FActionDict;
        private long FPrintDict;
        private List<long> FXRef;
        private List<long> FPagesRef;
        private List<string> FTrasparentStroke;
        private List<string> FTrasparentFill;
        private List<float> FPagesHeights;
        private List<float> FPagesTopMargins;
        private float FMarginLeft;
        private float FMarginWoBottom;
        private Stream pdf;
        private NumberFormatInfo FNumberFormatInfo;
        private float paperWidth;
        private float paperHeight;
        private long FMetaFileId;
        private long FStructId;
        private long FColorProfileId;
        private long FAttachmentsNamesId;
        private long FAttachmentsListId;
        private Graphics graphics;
        private StringBuilder contentBuilder;
        private long FContentsPos;
        private ExportType exportMode;
        private string ZUGFeRDDescription;        
        // end

        #endregion

        #region Public Properties

        #region Options

        /// <summary>
        /// Gets or sets PDF Compliance standard.
        /// After set, do not change other settings, it may lead to fail compliance test.
        /// </summary>
        public PdfStandard PdfCompliance
        {
            get
            {
                return FPdfCompliance;
            }
            set
            {
                FPdfCompliance = value;
                switch (FPdfCompliance)
                {
                    case PdfStandard.None:
                        break;
                    case PdfStandard.PdfA_2a:
                    case PdfStandard.PdfA_2b:
                    case PdfStandard.PdfA_3a:
                    case PdfStandard.PdfA_3b:
                        EmbeddingFonts = true;
                        TextInCurves = false;
                        OwnerPassword = "";
                        UserPassword = "";
                        FEncrypted = false;
                        break;
                    case PdfStandard.PdfX_3:
                    case PdfStandard.PdfX_4:
                        OwnerPassword = "";
                        UserPassword = "";
                        FEncrypted = false;
                        break;

                }
            }
        }

        /// <summary>
        /// Enable or disable of embedding the TrueType fonts.
        /// </summary>
        public bool EmbeddingFonts
        {
            get { return FEmbeddingFonts; }
            set
            {
                FEmbeddingFonts = value;
                if (FEmbeddingFonts)
                    FTextInCurves = false;
            }
        }

        /// <summary>
        /// Enable or disable of exporting the background.
        /// </summary>
        public bool Background
        {
            get { return FBackground; }
            set { FBackground = value; }
        }

        /// <summary>
        /// Enable or disable export text in curves
        /// </summary>
        public bool TextInCurves
        {
            get { return FTextInCurves; }
            set
            {
                FTextInCurves = value;
                if (FTextInCurves)
                    FEmbeddingFonts = false;
            }
        }

        /// <summary>
        /// Gets or sets PDF color space
        /// </summary>
        public PdfColorSpace ColorSpace
        {
            get { return FColorSpace; }
            set { FColorSpace = value; }
        }

        /// <summary>
        /// Enables or disables saving images in their original resolution
        /// </summary>
        public bool ImagesOriginalResolution
        {
            get { return FImagesOriginalResolution; }
            set { FImagesOriginalResolution = value; }
        }

        /// <summary>
        /// Enables or disables optimization of images for printing
        /// </summary>
        public bool PrintOptimized
        {
            get { return FPrintOptimized; }
            set { FPrintOptimized = value; }
        }

        /// <summary>
        /// Enable or disable image jpeg compression
        /// </summary>
        public bool JpegCompression
        {
            get { return FJpegCompression; }
            set { FJpegCompression = value; }
        }

        /// <summary>
        /// Sets the quality of images in the PDF
        /// </summary>
        public int JpegQuality
        {
            get { return FJpegQuality; }
            set
            {
                FJpegQuality = value;
                RichTextQuality = value;
            }
        }

        #endregion

        #region Document Information

        /// <summary>
        /// Title of the document.
        /// </summary>
        public string Title
        {
            get { return FTitle; }
            set { FTitle = value; }
        }

        /// <summary>
        /// Author of the document.
        /// </summary>
        public string Author
        {
            get { return FAuthor; }
            set { FAuthor = value; }
        }

        /// <summary>
        /// Subject of the document.
        /// </summary>
        public string Subject
        {
            get { return FSubject; }
            set { FSubject = value; }
        }

        /// <summary>
        /// Keywords of the document.
        /// </summary>
        public string Keywords
        {
            get { return FKeywords; }
            set { FKeywords = value; }
        }

        /// <summary>
        /// Creator of the document.
        /// </summary>
        public string Creator
        {
            get { return FCreator; }
            set { FCreator = value; }
        }

        /// <summary>
        /// Producer of the document.
        /// </summary>
        public string Producer
        {
            get { return FProducer; }
            set { FProducer = value; }
        }

        #endregion

        #region Security

        /// <summary>
        /// Sets the owner password.
        /// </summary>
        public string OwnerPassword
        {
            get { return FOwnerPassword; }
            set { FOwnerPassword = value; }
        }

        /// <summary>
        /// Sets the user password.
        /// </summary>
        public string UserPassword
        {
            get { return FUserPassword; }
            set { FUserPassword = value; }
        }

        /// <summary>
        /// Enable or disable printing in protected document.
        /// </summary>
        public bool AllowPrint
        {
            get { return FAllowPrint; }
            set { FAllowPrint = value; }
        }

        /// <summary>
        /// Enable or disable modifying in protected document.
        /// </summary>
        public bool AllowModify
        {
            get { return FAllowModify; }
            set { FAllowModify = value; }
        }

        /// <summary>
        /// Enable or disable copying in protected document.
        /// </summary>
        public bool AllowCopy
        {
            get { return FAllowCopy; }
            set { FAllowCopy = value; }
        }

        /// <summary>
        /// Enable or disable annotating in protected document.
        /// </summary>
        public bool AllowAnnotate
        {
            get { return FAllowAnnotate; }
            set { FAllowAnnotate = value; }
        }

        #endregion

        #region Viewer

        /// <summary>
        /// Enable or disable the print dialog window after opening
        /// </summary>
        public bool ShowPrintDialog
        {
            get { return FShowPrintDialog; }
            set { FShowPrintDialog = value; }
        }

        /// <summary>
        /// Enable or disable hide the toolbar.
        /// </summary>
        public bool HideToolbar
        {
            get { return FHideToolbar; }
            set { FHideToolbar = value; }
        }

        /// <summary>
        /// Enable or disable hide the menu's bar.
        /// </summary>
        public bool HideMenubar
        {
            get { return FHideMenubar; }
            set { FHideMenubar = value; }
        }

        /// <summary>
        /// Enable or disable hide the Windows UI.
        /// </summary>
        public bool HideWindowUI
        {
            get { return FHideWindowUI; }
            set { FHideWindowUI = value; }
        }

        /// <summary>
        /// Enable or disable of fitting the window
        /// </summary>
        public bool FitWindow
        {
            get { return FFitWindow; }
            set { FFitWindow = value; }
        }

        /// <summary>
        /// Enable or disable of centering the window.
        /// </summary>
        public bool CenterWindow
        {
            get { return FCenterWindow; }
            set { FCenterWindow = value; }
        }

        /// <summary>
        /// Enable or disable of scaling the page for shrink to printable area.
        /// </summary>
        public bool PrintScaling
        {
            get { return FPrintScaling; }
            set { FPrintScaling = value; }
        }

        /// <summary>
        /// Enable or disable of document's Outline.
        /// </summary>
        public bool Outline
        {
            get { return FOutline; }
            set { FOutline = value; }
        }

        /// <summary>
        /// Set default zoom on open document
        /// </summary>
        public MagnificationFactor DefaultZoom
        {
            get { return FDefaultZoom; }
            set { FDefaultZoom = value; }
        }

        #endregion

        #region Other

        /// <summary>
        /// Sets the quality of RichText objects in the PDF
        /// </summary>
        public int RichTextQuality
        {
            get { return FRichTextQuality; }
            set { FRichTextQuality = value; }
        }

        /// <summary>
        /// Enable or disable the compression in PDF document.
        /// </summary>
        public bool Compressed
        {
            get { return FCompressed; }
            set { FCompressed = value; }
        }

        /// <summary>
        /// Enable or disable of images transparency.
        /// </summary>
        public bool TransparentImages
        {
            get { return FTransparentImages; }
            set { FTransparentImages = value; }
        }

        /// <summary>
        /// Enable or disable of displaying document's title.
        /// </summary>
        public bool DisplayDocTitle
        {
            get { return FDisplayDocTitle; }
            set { FDisplayDocTitle = value; }
        }

        /// <summary>
        /// Set default page on open document
        /// </summary>
        public int DefaultPage
        {
            get { return FDefaultPage; }
            set { FDefaultPage = value; }
        }

        /// <summary>
        /// Color Profile (ICC file).
        /// If "null" then default profile will be used
        /// </summary>
        public byte[] ColorProfile
        {
            get { return FColorProfile; }
            set { FColorProfile = value; }
        }

        /// <summary>
        /// Gets or sets pdf export mode
        /// </summary>
        public ExportType ExportMode
        {
            get { return exportMode; }
            set { exportMode = value; }
        }
        #endregion

        #endregion

        #region Private Methods

        private string getPdfVersion()
        {
            switch (PdfCompliance)
            {
                case PdfStandard.PdfA_2a:
                case PdfStandard.PdfA_2b:
                case PdfStandard.PdfA_3a:
                case PdfStandard.PdfA_3b:
                    return "1.7";
                case PdfStandard.PdfX_3: // PDF/X-3:2003
                    return "1.4";
                case PdfStandard.PdfX_4:
                    return "1.6";
                case PdfStandard.None:
                default:
                    return "1.5";
            }
        }

        private bool isPdfX()
        {
            switch (PdfCompliance)
            {
                case PdfStandard.PdfX_3:
                case PdfStandard.PdfX_4:
                    return true;
                default:
                    return false;
            }
        }

        private bool isPdfA()
        {
            switch (PdfCompliance)
            {
                case PdfStandard.PdfA_2a:
                case PdfStandard.PdfA_2b:
                case PdfStandard.PdfA_3a:
                case PdfStandard.PdfA_3b:
                    return true;
                default:
                    return false;
            }
        }

        private void AddPDFHeader()
        {
            WriteLn(pdf, "%PDF-" + getPdfVersion());
            byte[] signature = { 0x25, 0xE2, 0xE3, 0xCF, 0xD3, 0x0D, 0x0A };
            pdf.Write(signature, 0, signature.Length);
            // reserve object for pages
            UpdateXRef();
        }

        private void AddPage(ReportPage page)
        {
            FPageFonts = new List<ExportTTFFont>();
            FTrasparentStroke = new List<string>();
            FTrasparentFill = new List<string>();
            picResList = new List<long>();

            paperWidth = ExportUtils.GetPageWidth(page) * Units.Millimeters;
            paperHeight = ExportUtils.GetPageHeight(page) * Units.Millimeters;

            FMarginWoBottom = (ExportUtils.GetPageHeight(page) - page.TopMargin) * PDF_PAGE_DIVIDER;
            FMarginLeft = page.LeftMargin * PDF_PAGE_DIVIDER;

            FPagesHeights.Add(FMarginWoBottom);
            FPagesTopMargins.Add(page.TopMargin * PDF_PAGE_DIVIDER);

            long FContentsPos = 0;
            StringBuilder contentBuilder = new StringBuilder(65535);

            // page fill
            if (FBackground)
                using (TextObject pageFill = new TextObject())
                {
                    pageFill.Fill = page.Fill;
                    pageFill.Left = -FMarginLeft / PDF_DIVIDER;
                    pageFill.Top = -page.TopMargin * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageFill.Width = ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageFill.Height = ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    AddTextObject(pageFill, false, contentBuilder);
                }

            // bitmap watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page, contentBuilder);

            // text watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowTextOnTop)
                AddTextWatermark(page, contentBuilder);

            // page borders
            if (page.Border.Lines != BorderLines.None)
            {
                using (TextObject pageBorder = new TextObject())
                {
                    pageBorder.Border = page.Border;
                    pageBorder.Left = 0;
                    pageBorder.Top = 0;
                    pageBorder.Width = (ExportUtils.GetPageWidth(page) - page.LeftMargin - page.RightMargin) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageBorder.Height = (ExportUtils.GetPageHeight(page) - page.TopMargin - page.BottomMargin) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    AddTextObject(pageBorder, true, contentBuilder);
                }
            }

            foreach (Base c in page.AllObjects)
            {
                if (c is ReportComponentBase)
                {
                    ReportComponentBase obj = c as ReportComponentBase;
                    if (obj is CellularTextObject)
                        obj = (obj as CellularTextObject).GetTable();
                    if (obj is TableCell)
                        continue;
                    else
                        if (obj is TableBase)
                    {
                        TableBase table = obj as TableBase;
                        if (table.ColumnCount > 0 && table.RowCount > 0)
                        {
                            StringBuilder tableBorder = new StringBuilder(64);
                            using (TextObject tableback = new TextObject())
                            {
                                tableback.Border = table.Border;
                                tableback.Fill = table.Fill;
                                tableback.FillColor = table.FillColor;
                                tableback.Left = table.AbsLeft;
                                tableback.Top = table.AbsTop;
                                float tableWidth = 0;
                                float tableHeight = 0;
                                for (int i = 0; i < table.ColumnCount; i++)
                                    tableWidth += table[i, 0].Width;
                                for (int i = 0; i < table.RowCount; i++)
                                    tableHeight += table.Rows[i].Height;
                                tableback.Width = (tableWidth < table.Width) ? tableWidth : table.Width;
                                tableback.Height = tableHeight;
                                AddTextObject(tableback, false, contentBuilder);
                                DrawPDFBorder(tableback.Border, tableback.AbsLeft, tableback.AbsTop, tableback.Width, tableback.Height, tableBorder);
                            }
                            // draw cells
                            AddTable(table, true, contentBuilder);
                            // draw cells border
                            AddTable(table, false, contentBuilder);
                            // draw table border
                            contentBuilder.Append(tableBorder);
                        }
                    }
                    else if (obj is TextObject)
                        AddTextObject(obj as TextObject, true, contentBuilder);
                    else if (obj is BandBase)
                        AddBandObject(obj as BandBase, contentBuilder);
                    else if (obj is LineObject)
                        AddLine(obj as LineObject, contentBuilder);
                    else if (obj is ShapeObject)
                        AddShape(obj as ShapeObject, contentBuilder);
                    else if (obj is RichObject)
                        AddPictureObject(obj as ReportComponentBase, true, FRichTextQuality, contentBuilder);
                    else if (obj is PolyLineObject)
                        AddPolyLine(obj as PolyLineObject, contentBuilder);
                    else if (!(obj is HtmlObject))
                        AddPictureObject(obj as ReportComponentBase, true, FJpegQuality, contentBuilder);
                }
            }

            // bitmap watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page, contentBuilder);

            // text watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowTextOnTop)
                AddTextWatermark(page, contentBuilder);

            // write page
            FContentsPos = UpdateXRef();
            WriteLn(pdf, ObjNumber(FContentsPos));

            using (MemoryStream tempContentStream = new MemoryStream())
            {
                Write(tempContentStream, contentBuilder.ToString());
                tempContentStream.Position = 0;
                WritePDFStream(pdf, tempContentStream, FContentsPos, FCompressed, FEncrypted, true, true);
            }

            if (!FTextInCurves)
                if (FPageFonts.Count > 0)
                    for (int i = 0; i < FPageFonts.Count; i++)
                        if (!FPageFonts[i].Saved)
                        {
                            FPageFonts[i].Reference = UpdateXRef();
                            FPageFonts[i].Saved = true;
                        }

            long PageNumber = UpdateXRef();
            FPagesRef.Add(PageNumber);
            WriteLn(pdf, ObjNumber(PageNumber));
            StringBuilder sb = new StringBuilder(512);
            sb.AppendLine("<<").AppendLine("/Type /Page");
            sb.Append("/MediaBox [0 0 ").Append(FloatToString(ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER)).Append(" ");
            sb.Append(FloatToString(ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER)).AppendLine(" ]");
            //margins
            if (isPdfX())
                sb.Append("/TrimBox [")
                    .Append(FloatToString(page.LeftMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.TopMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.RightMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.BottomMargin * PDF_PAGE_DIVIDER)).Append("]");

            sb.AppendLine("/Parent 1 0 R");
            if (!isPdfX())
            {
                if (ColorSpace == PdfColorSpace.RGB)
                    sb.AppendLine("/Group << /Type /Group /S /Transparency /CS /DeviceRGB >>");
                else if (ColorSpace == PdfColorSpace.CMYK)
                    sb.AppendLine("/Group << /Type /Group /S /Transparency /CS /DeviceCMYK >>");
            }
            sb.AppendLine("/Resources << ");

            if (FPageFonts.Count > 0)
            {
                sb.Append("/Font << ");
                foreach (ExportTTFFont font in FPageFonts)
                    sb.Append(font.Name).Append(" ").Append(ObjNumberRef(font.Reference)).Append(" ");
                sb.AppendLine(" >>");
            }

            if (isPdfX())
            {
                sb.AppendLine("/ExtGState <<");
                for (int i = 0; i < FTrasparentStroke.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("S << /Type /ExtGState /ca ").Append(1).AppendLine(" >>");
                for (int i = 0; i < FTrasparentFill.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("F << /Type /ExtGState /CA ").Append(1).AppendLine(" >>");
                sb.AppendLine(">>");
            }
            else
            {
                sb.AppendLine("/ExtGState <<");
                for (int i = 0; i < FTrasparentStroke.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("S << /Type /ExtGState /ca ").Append(FTrasparentStroke[i]).AppendLine(" >>");
                for (int i = 0; i < FTrasparentFill.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("F << /Type /ExtGState /CA ").Append(FTrasparentFill[i]).AppendLine(" >>");
                sb.AppendLine(">>");
            }

            if (picResList.Count > 0)
            {
                sb.Append("/XObject << ");
                foreach (long resIndex in picResList)
                    sb.Append("/Im").Append(resIndex.ToString()).Append(" ").Append(ObjNumberRef(resIndex)).Append(" ");
                sb.AppendLine(" >>");
            }

            sb.AppendLine("/ProcSet [/PDF /Text /ImageC ]");
            sb.AppendLine(">>");

            sb.Append("/Contents ").AppendLine(ObjNumberRef(FContentsPos));
            if (FPageAnnots.Length > 0)
            {
                sb.AppendLine(GetPageAnnots());
                FPageAnnots.Length = 0;
            }

            sb.AppendLine(">>");
            sb.AppendLine("endobj");
            Write(pdf, sb.ToString());
        }

        private void AddBitmapWatermark(ReportPage page, StringBuilder sb)
        {
            if (page.Watermark.Image != null)
            {
                using (PictureObject pictureWatermark = new PictureObject())
                {
                    pictureWatermark.Left = -FMarginLeft / PDF_DIVIDER;
                    pictureWatermark.Top = -page.TopMargin * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pictureWatermark.Width = ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pictureWatermark.Height = ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;

                    pictureWatermark.SizeMode = PictureBoxSizeMode.Normal;
                    pictureWatermark.Image = new Bitmap((int)pictureWatermark.Width, (int)pictureWatermark.Height);
                    using (Graphics g = Graphics.FromImage(pictureWatermark.Image))
                    {
                        g.Clear(Color.Transparent);
                        page.Watermark.DrawImage(new FRPaintEventArgs(g, 1, 1, Report.GraphicCache),
                            new RectangleF(0, 0, pictureWatermark.Width, pictureWatermark.Height), Report, true);
                    }
                    pictureWatermark.Transparency = page.Watermark.ImageTransparency;
                    pictureWatermark.Fill = new SolidFill(Color.Transparent);
                    pictureWatermark.FillColor = Color.Transparent;
                    AddPictureObject(pictureWatermark, false, FJpegQuality, sb);
                }
            }
        }

        private void AddTextWatermark(ReportPage page, StringBuilder sb)
        {
            if (!String.IsNullOrEmpty(page.Watermark.Text))
                using (TextObject textWatermark = new TextObject())
                {
                    textWatermark.HorzAlign = HorzAlign.Center;
                    textWatermark.VertAlign = VertAlign.Center;
                    textWatermark.Left = -FMarginLeft / PDF_DIVIDER;
                    textWatermark.Top = -page.TopMargin * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    textWatermark.Width = ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    textWatermark.Height = ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    textWatermark.Text = page.Watermark.Text;
                    textWatermark.TextFill = page.Watermark.TextFill;
                    if (page.Watermark.TextRotation == WatermarkTextRotation.Vertical)
                        textWatermark.Angle = 270;
                    else if (page.Watermark.TextRotation == WatermarkTextRotation.ForwardDiagonal)
                        textWatermark.Angle = 360 - (int)(Math.Atan(textWatermark.Height / textWatermark.Width) * (180 / Math.PI));
                    else if (page.Watermark.TextRotation == WatermarkTextRotation.BackwardDiagonal)
                        textWatermark.Angle = (int)(Math.Atan(textWatermark.Height / textWatermark.Width) * (180 / Math.PI));
                    textWatermark.Font = page.Watermark.Font;
                    if (page.Watermark.TextFill is SolidFill)
                        textWatermark.TextColor = (page.Watermark.TextFill as SolidFill).Color;
                    textWatermark.Fill = new SolidFill(Color.Transparent);
                    textWatermark.FillColor = Color.Transparent;
                    AddTextObject(textWatermark, false, sb);
                }
        }

        private void AddTable(TableBase table, bool drawCells, StringBuilder sb_in)
        {
            float y = 0;
            StringBuilder sb = new StringBuilder(1024);
            for (int i = 0; i < table.RowCount; i++)
            {
                float x = 0;
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    if (!table.IsInsideSpan(table[j, i]))
                    {
                        TableCell textcell = table[j, i];
                        textcell.Left = x;
                        textcell.Top = y;
                        if (drawCells)
                        {
                            Border oldBorder = textcell.Border.Clone();
                            textcell.Border.Lines = BorderLines.None;
                            if ((textcell as TextObject) is TextObject)
                                AddTextObject(textcell as TextObject, false, sb_in);
                            else
                                AddPictureObject(textcell as ReportComponentBase, false, FJpegQuality, sb_in);
                            textcell.Border = oldBorder;
                        }
                        else
                            DrawPDFBorder(textcell.Border, textcell.AbsLeft, textcell.AbsTop, textcell.Width, textcell.Height, sb);
                    }
                    x += (table.Columns[j]).Width;
                }
                y += (table.Rows[i]).Height;
            }
            sb_in.Append(sb);
        }

        private void AddShape(ShapeObject shapeObject, StringBuilder sb)
        {
            if (shapeObject.Shape == ShapeKind.Rectangle && shapeObject.Fill is SolidFill)
            {
                DrawPDFFillRect(
                    GetLeft(shapeObject.AbsLeft), GetTop(shapeObject.AbsTop),
                    shapeObject.Width * PDF_DIVIDER, shapeObject.Height * PDF_DIVIDER,
                    shapeObject.Fill, sb);
                DrawPDFRect(
                    GetLeft(shapeObject.AbsLeft),
                    GetTop(shapeObject.AbsTop),
                    GetLeft(shapeObject.AbsLeft + shapeObject.Width),
                    GetTop(shapeObject.AbsTop + shapeObject.Height),
                    shapeObject.Border.Color, shapeObject.Border.Width * PDF_DIVIDER, shapeObject.Border.Style, sb);
            }
            else if (shapeObject.Shape == ShapeKind.Triangle && shapeObject.Fill is SolidFill)
                DrawPDFTriangle(GetLeft(shapeObject.AbsLeft), GetTop(shapeObject.AbsTop),
                    shapeObject.Width * PDF_DIVIDER, shapeObject.Height * PDF_DIVIDER,
                    shapeObject.FillColor, shapeObject.Border.Color, shapeObject.Border.Width * PDF_DIVIDER, shapeObject.Border.Style, sb);
            else if (shapeObject.Shape == ShapeKind.Diamond && shapeObject.Fill is SolidFill)
                DrawPDFDiamond(GetLeft(shapeObject.AbsLeft), GetTop(shapeObject.AbsTop),
                    shapeObject.Width * PDF_DIVIDER, shapeObject.Height * PDF_DIVIDER,
                    shapeObject.FillColor, shapeObject.Border.Color, shapeObject.Border.Width * PDF_DIVIDER, shapeObject.Border.Style, sb);
            else if (shapeObject.Shape == ShapeKind.Ellipse && shapeObject.Fill is SolidFill)
                DrawPDFEllipse(GetLeft(shapeObject.AbsLeft), GetTop(shapeObject.AbsTop),
                    shapeObject.Width * PDF_DIVIDER, shapeObject.Height * PDF_DIVIDER,
                    shapeObject.FillColor, shapeObject.Border.Color, shapeObject.Border.Width * PDF_DIVIDER, shapeObject.Border.Style, sb);
            else
                AddPictureObject(shapeObject, true, FJpegQuality, sb);
        }

        private void AddPolyLine(PolyLineObject obj, StringBuilder sb)
        {
            int len = obj.PointsArray.Length;
            if (len == 0 || len == 1)
            {
                float localX = GetLeft(obj.AbsLeft);
                float localY = GetTop(obj.AbsTop);
                DrawPDFLine(
                    localX, localY + 6 * PDF_DIVIDER,
                    localX, localY - 6 * PDF_DIVIDER,
                    obj.Border.Color, obj.Border.Width * PDF_DIVIDER, obj.Border.Style, null, null, sb);
                DrawPDFLine(
                    localX - 6 * PDF_DIVIDER, localY,
                    localX + 6 * PDF_DIVIDER, localY,
                    obj.Border.Color, obj.Border.Width * PDF_DIVIDER, obj.Border.Style, null, null, sb);
            }
            else if (len == 2)
            {
                DrawPDFLine(
                    GetLeft(obj.AbsLeft) + (obj.PointsArray[0].X + obj.CenterX) * PDF_DIVIDER,
                    GetTop(obj.AbsTop) - (obj.PointsArray[0].Y + obj.CenterY) * PDF_DIVIDER,
                    GetLeft(obj.AbsLeft) + (obj.PointsArray[1].X + obj.CenterX) * PDF_DIVIDER,
                    GetTop(obj.AbsTop) - (obj.PointsArray[1].Y + obj.CenterY) * PDF_DIVIDER, obj.Border.Color, obj.Border.Width * PDF_DIVIDER, obj.Border.Style, null, null, sb);
            }
            else
            {
                if (obj is PolygonObject)
                {
                    if (obj.Fill is SolidFill)
                        DrawPDFPolygon(GetLeft(obj.AbsLeft),
                        GetTop(obj.AbsTop), GetLeft(obj.AbsLeft + obj.Width), GetTop(obj.AbsTop + obj.Height),
                        obj.CenterX, obj.CenterY, obj.PointsArray, obj.FillColor,
                        obj.Border.Color, obj.Border.Width * PDF_DIVIDER, obj.Border.Style, sb);
                    else
                        AddPictureObject(obj, true, FJpegQuality, sb);
                }
                else
                    DrawPDFPolyLine(GetLeft(obj.AbsLeft),
                        GetTop(obj.AbsTop), GetLeft(obj.AbsLeft + obj.Width), GetTop(obj.AbsTop + obj.Height),
                        obj.CenterX, obj.CenterY, obj.PointsArray, false,
                        obj.Border.Color, obj.Border.Width * PDF_DIVIDER, obj.Border.Style, sb);
            }
        }

        private void AddLine(LineObject l, StringBuilder sb)
        {
            DrawPDFLine(GetLeft(l.AbsLeft),
                GetTop(l.AbsTop), GetLeft(l.AbsLeft + l.Width), GetTop(l.AbsTop + l.Height),
                l.Border.Color, l.Border.Width * PDF_DIVIDER, l.Border.Style, l.StartCap, l.EndCap, sb);
        }

        private void AddBandObject(BandBase band, StringBuilder sb)
        {
            using (TextObject newObj = new TextObject())
            {
                newObj.Left = band.AbsLeft;
                newObj.Top = band.AbsTop;
                newObj.Width = band.Width;
                newObj.Height = band.Height;
                newObj.Fill = band.Fill;
                newObj.Border = band.Border;
                AddTextObject(newObj, true, sb);
            }
        }

        private void AddTextObject(TextObject obj, bool drawBorder, StringBuilder sb_in)
        {
            string Left = FloatToString(GetLeft(obj.AbsLeft));
            string Top = FloatToString(GetTop(obj.AbsTop));
            string Right = FloatToString(GetLeft(obj.AbsLeft + obj.Width));
            string Bottom = FloatToString(GetTop(obj.AbsTop + obj.Height));
            string Width = FloatToString(obj.Width * PDF_DIVIDER);
            string Height = FloatToString(obj.Height * PDF_DIVIDER);

            if (!isPdfX())
                AddAnnot(obj, Left + " " + Bottom + " " + Right + " " + Top);

            StringBuilder sb = new StringBuilder(256);
            StringBuilder image_sb = null;

            sb.AppendLine("q");
            sb.Append(FloatToString(GetLeft(obj.AbsLeft))).Append(" ");
            sb.Append(FloatToString(GetTop(obj.AbsTop + obj.Height))).Append(" ");
            sb.Append(FloatToString((obj.Width) * PDF_DIVIDER)).Append(" ");
            sb.Append(FloatToString((obj.Height) * PDF_DIVIDER)).AppendLine(" re");
            if (obj.Clip)
                sb.AppendLine("W");
            sb.AppendLine("n");

            // draw background
            if (obj.Fill is SolidFill || (obj.Fill is GlassFill && !(obj.Fill as GlassFill).Hatch))
                DrawPDFFillRect(GetLeft(obj.AbsLeft), GetTop(obj.AbsTop),
                    obj.Width * PDF_DIVIDER, obj.Height * PDF_DIVIDER, obj.Fill, sb);
            else if (obj.Width > 0 && obj.Height > 0)
            {
                using (PictureObject backgroundPicture = new PictureObject())
                {
                    backgroundPicture.Left = obj.AbsLeft;
                    backgroundPicture.Top = obj.AbsTop;
                    backgroundPicture.Width = obj.Width;
                    backgroundPicture.Height = obj.Height;
                    backgroundPicture.Image = new Bitmap((int)backgroundPicture.Width, (int)backgroundPicture.Height);
                    using (Graphics g = Graphics.FromImage(backgroundPicture.Image))
                    {
                        g.Clear(Color.Transparent);
                        g.TranslateTransform(-obj.AbsLeft, -obj.AbsTop);
                        BorderLines oldLines = obj.Border.Lines;
                        obj.Border.Lines = BorderLines.None;
                        string oldText = obj.Text;
                        obj.Text = String.Empty;
                        obj.Draw(new FRPaintEventArgs(g, 1, 1, Report.GraphicCache));
                        obj.Text = oldText;
                        obj.Border.Lines = oldLines;
                    }
                    AddPictureObject(backgroundPicture, false, FJpegQuality, sb_in);
                }
            }

            if (obj.Underlines)
                AppendUnderlines(sb, obj);

            if (!String.IsNullOrEmpty(obj.Text))
            {
                int ObjectFontNumber = GetObjFontNumber(obj.Font);
                // obj with HtmlTags uses own font/color for each word/run
                if (!obj.HtmlTags)
                    AppendFont(sb, ObjectFontNumber, obj.Font.Size, obj.TextColor);

                using (Font f = new Font(obj.Font.Name, obj.Font.Size * FDpiFX, obj.Font.Style))
                {
                    RectangleF textRect = new RectangleF(
                      obj.AbsLeft + obj.Padding.Left,
                      obj.AbsTop + obj.Padding.Top,
                      obj.Width - obj.Padding.Horizontal,
                      obj.Height - obj.Padding.Vertical);

                    bool transformNeeded = obj.Angle != 0 || obj.FontWidthRatio != 1;

                    // transform, rotate and scale pdf coordinates if needed
                    if (transformNeeded)
                    {
                        textRect.X = -textRect.Width / 2;
                        textRect.Y = -textRect.Height / 2;

                        float angle = (float)((360 - obj.Angle) * Math.PI / 180);
                        float sin = (float)Math.Sin(angle);
                        float cos = (float)Math.Cos(angle);
                        float x = GetLeft(obj.AbsLeft + obj.Width / 2);
                        float y = GetTop(obj.AbsTop + obj.Height / 2);
                        // offset the origin to the middle of bounding rectangle, then rotate
                        sb.Append(FloatToString(cos)).Append(" ").
                            Append(FloatToString(sin)).Append(" ").
                            Append(FloatToString(-sin)).Append(" ").
                            Append(FloatToString(cos)).Append(" ").
                            Append(FloatToString(x)).Append(" ").
                            Append(FloatToString(y)).AppendLine(" cm");

                        // apply additional matrix to scale x coordinate
                        if (obj.FontWidthRatio != 1)
                            sb.Append(FloatToString(obj.FontWidthRatio)).AppendLine(" 0 0 1 0 0 cm");
                    }

                    // break the text to paragraphs, lines, words and runs
                    StringFormat format = obj.GetStringFormat(Report.GraphicCache /*cache*/, 0);
                    Brush textBrush = Report.GraphicCache.GetBrush(obj.TextColor);
                    AdvancedTextRenderer renderer = new AdvancedTextRenderer(obj.Text, graphics, f, textBrush, null,
                        textRect, format, obj.HorzAlign, obj.VertAlign, obj.LineHeight, obj.Angle, obj.FontWidthRatio,
                        obj.ForceJustify, obj.Wysiwyg, obj.HtmlTags, true, FDpiFX,
                        obj.InlineImageCache);
                    if(obj.HtmlTags == true)
                    {
                        image_sb = new StringBuilder();
                        foreach(PictureObject pobj in obj.GetPictureFromHtmlText(renderer))
                        {
                            obj.ChildObjects.Add(pobj);
                            pobj.Parent = obj;
                            AddPictureObject(pobj, false, FJpegQuality, image_sb);
                        }
                    }
                    float w = f.Height * 0.1f; // to match .net char X offset
                    // invert offset in case of rtl
                    if (obj.RightToLeft)
                        w = -w;
                    // we don't need this offset if text is centered
                    if (obj.HorzAlign == HorzAlign.Center)
                        w = 0;

                    // render
                    foreach (AdvancedTextRenderer.Paragraph paragraph in renderer.Paragraphs)
                        foreach (AdvancedTextRenderer.Line line in paragraph.Lines)
                        {
                            float lineOffset = 0;
                            float lineHeight = line.CalcHeight();
                            float objHeight = (obj.Angle == 0 || obj.Angle == 180) ? 
                                obj.Height - obj.Padding.Vertical : 
                                obj.Width - obj.Padding.Horizontal;
                            if (lineHeight > objHeight)
                            {
                                if (obj.VertAlign == VertAlign.Center)
                                    lineOffset = -lineHeight / 2;
                                else if (obj.VertAlign == VertAlign.Bottom)
                                    lineOffset = -lineHeight;
                            }
                            foreach (RectangleF rect in line.Underlines)
                                DrawPDFUnderline(ObjectFontNumber, f, rect.Left, rect.Top, rect.Width, w,
                                    obj.TextColor, transformNeeded, sb);
                            foreach (RectangleF rect in line.Strikeouts)
                                DrawPDFStrikeout(ObjectFontNumber, f, rect.Left, rect.Top, rect.Width, w,
                                    obj.TextColor, transformNeeded, sb);

                            foreach (AdvancedTextRenderer.Word word in line.Words)
                                if (renderer.HtmlTags)
                                    foreach (AdvancedTextRenderer.Run run in word.Runs)
                                        using (Font fnt = run.GetFont())
                                        {
                                            ObjectFontNumber = GetObjFontNumber(fnt);
                                            AppendFont(sb, ObjectFontNumber, fnt.Size / FDpiFX, run.Style.Color);
                                            AppendText(sb, ObjectFontNumber, fnt, run.Left, run.Top, w, run.Text,
                                                obj.RightToLeft, transformNeeded, obj.TextOutline, run.Style.Color);
                                        }
                                else
                                    AppendText(sb, ObjectFontNumber, f, word.Left, word.Top + lineOffset, w, word.Text,
                                        obj.RightToLeft, transformNeeded, obj.TextOutline, obj.TextColor);
                        }
                }
            }
            sb.AppendLine("Q");
            if (drawBorder)
                DrawPDFBorder(obj.Border, obj.AbsLeft, obj.AbsTop, obj.Width, obj.Height, sb);
            sb_in.Append(sb);
            if (image_sb != null)
                sb_in.Append(image_sb);
        }

        private void AppendUnderlines(StringBuilder Result, TextObject obj)
        {
            float lineHeight = obj.LineHeight == 0 ? obj.Font.GetHeight() : obj.LineHeight;
            lineHeight *= FDpiFX * PDF_DIVIDER;
            float curY = GetTop(obj.AbsTop) - lineHeight;
            float bottom = GetTop(obj.AbsBottom);
            float left = GetLeft(obj.AbsLeft);
            float right = GetLeft(obj.AbsRight);
            float width = obj.Border.Width * PDF_DIVIDER;
            while (curY > bottom)
            {
                DrawPDFLine(left, curY, right, curY, obj.Border.Color, width, LineStyle.Solid, null, null, Result);
                curY -= lineHeight;
            }
        }

        private void DrawText(StringBuilder Result, float x, float y, string s, ExportTTFFont font, Color fontColor)
        {
            bool SimulateItalic = font.NeedStyleSimulation && font.SourceFont.Italic;
            bool SimulateBold = font.NeedStyleSimulation && font.SourceFont.Bold;

            
            Result.AppendLine("BT");
            Result.Append(FloatToString(x)).Append(" ").Append(FloatToString(y)).AppendLine(" Td");
            if (SimulateItalic)
                Result.Append("1 0 0.3333 1 ").Append(FloatToString(x)).Append(' ').Append(FloatToString(y)).AppendLine(" Tm");
            if (SimulateBold)
            {
                GetPDFStrokeColor(fontColor, Result);
                Result.Append("2 Tr ").Append(FloatToString(font.SourceFont.Size / 775.0)).Append(" w ");
            }
            else
                Result.AppendLine("0 Tr");
            Result.Append("<").Append(ExportUtils.StrToHex2(s)).AppendLine("> Tj");
            if (SimulateBold)
                Result.AppendLine("0 Tr");
            Result.AppendLine("ET");
        }
        
        private void DrawTextOutline(StringBuilder Result, float x, float y, string s, TextOutline objTextOutline)
        {
            Result.AppendLine();
            GetPDFStrokeColor(objTextOutline.Color, Result);
            Result.AppendLine("BT");
            Result.Append(FloatToString(x)).Append(" ").Append(FloatToString(y)).AppendLine(" Td");
            Result.AppendLine("1 Tr");
            Result.Append(FloatToString(objTextOutline.Width * PDF_DIVIDER)).AppendLine(" w");
            Result.Append("<").Append(ExportUtils.StrToHex2(s)).AppendLine("> Tj");
            Result.AppendLine("ET");
        }

        private void AppendText(StringBuilder Result, int fontNumber, Font font, float x, float y, float offsX, string text, bool rtl, bool transformNeeded, TextOutline objTextOutline)
        {
            AppendText(Result, fontNumber, font, x, y, offsX, text, rtl, transformNeeded, objTextOutline, Color.Black);
        }

        private void AppendText(StringBuilder Result, int fontNumber, Font font, float x, float y, float offsX, string text, bool rtl, bool transformNeeded, TextOutline objTextOutline, Color fontColor)
        {
            if (!FTextInCurves)
            {
                // text with fonts
                ExportTTFFont pdffont = FPageFonts[fontNumber];
                x = (transformNeeded ? x * PDF_DIVIDER : GetLeft(x)) + offsX;
                y = transformNeeded ? -y * PDF_DIVIDER : GetTop(y);
                y -= GetBaseline(font) * PDF_DIVIDER;

                string s = pdffont.RemapString(text, rtl);

                if (!objTextOutline.Enabled)
                {
                    DrawText(Result, x, y, s, pdffont, fontColor);
                }
                else
                {
                    if (objTextOutline.DrawBehind)
                    {
                        DrawTextOutline(Result, x, y, s, objTextOutline);
                        DrawText(Result, x, y, s, pdffont, fontColor);
                    }
                    else
                    {
                        DrawText(Result, x, y, s, pdffont, fontColor);
                        DrawTextOutline(Result, x, y, s, objTextOutline);
                    }
                }
            }
            else
            {
                x = (transformNeeded ? x * PDF_DIVIDER : GetLeft(x)) + offsX;
                y = transformNeeded ? -y * PDF_DIVIDER : GetTop(y);
                y -= GetBaseline(font) * PDF_DIVIDER;

                ExportTTFFont pdffont = FPageFonts[fontNumber];

                float paddingX;
                float paddingY;
                ExportTTFFont.GlyphTTF[] txt = pdffont.getGlyphString(text, rtl, font.Size, out paddingX, out paddingY);
                if (!objTextOutline.Enabled)
                {
                    float shift = 0;
                    foreach (ExportTTFFont.GlyphTTF ch in txt)
                    {
                        //draw ch;
                        DrawPDFPolygonChar(ch.path, x + shift * PDF_DIVIDER, y, fontColor, Result);
                        shift += ch.width;
                    }
                }
                else
                {
                    if (objTextOutline.DrawBehind)
                    {
                        //outline
                        float shift = 0;
                        foreach (ExportTTFFont.GlyphTTF ch in txt)
                        {
                            //draw ch;
                            DrawPDFPolygonCharOutline(ch.path, x + shift * PDF_DIVIDER, y, objTextOutline.Color, objTextOutline.Width, Result);
                            shift += ch.width;
                        }

                        //fill
                        shift = 0;
                        foreach (ExportTTFFont.GlyphTTF ch in txt)
                        {
                            //draw ch;
                            DrawPDFPolygonChar(ch.path, x + shift * PDF_DIVIDER, y, fontColor, Result);
                            shift += ch.width;
                        }
                    }
                    else
                    {
                        //fill
                        float shift = 0;
                        foreach (ExportTTFFont.GlyphTTF ch in txt)
                        {
                            //draw ch;
                            DrawPDFPolygonChar(ch.path, x + shift * PDF_DIVIDER, y, fontColor, Result);
                            shift += ch.width;
                        }
                        //ouline
                        shift = 0;
                        foreach (ExportTTFFont.GlyphTTF ch in txt)
                        {
                            //draw ch;
                            DrawPDFPolygonCharOutline(ch.path, x + shift * PDF_DIVIDER, y, objTextOutline.Color, objTextOutline.Width, Result);
                            shift += ch.width;
                        }
                    }
                }
            }
        }

        private string GetZoomString(int page, float zoom)
        {
            return String.Format(" /XYZ 0 {0} {1}", Math.Round(FPagesHeights[page - 1] + FPagesTopMargins[page - 1]).ToString(), FloatToString(zoom));
        }

        private void SetMagnificationFactor(int PageNumber, MagnificationFactor factor)
        {
            if (factor == MagnificationFactor.Default)
                return;
            if (FPagesRef.Count <= 0)
                return;

            string Magnificator = "";

            FActionDict = UpdateXRef();
            WriteLn(pdf, ObjNumber(FActionDict));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/S /GoTo");
            switch (factor)
            {
                case MagnificationFactor.ActualSize:
                    Magnificator = GetZoomString(PageNumber, 1f);
                    break;
                case MagnificationFactor.FitPage:
                    Magnificator = " /Fit";
                    break;
                case MagnificationFactor.FitWidth:
                    Magnificator = " /FitH 0";
                    break;
                case MagnificationFactor.Percent_10:
                    Magnificator = GetZoomString(PageNumber, 0.1f);
                    break;
                case MagnificationFactor.Percent_25:
                    Magnificator = GetZoomString(PageNumber, 0.25f);
                    break;
                case MagnificationFactor.Percent_50:
                    Magnificator = GetZoomString(PageNumber, 0.5f);
                    break;
                case MagnificationFactor.Percent_75:
                    Magnificator = GetZoomString(PageNumber, 0.75f);
                    break;
                case MagnificationFactor.Percent_100:
                    Magnificator = GetZoomString(PageNumber, 1f);
                    break;
                case MagnificationFactor.Percent_125:
                    Magnificator = GetZoomString(PageNumber, 1.25f);
                    break;
                case MagnificationFactor.Percent_150:
                    Magnificator = GetZoomString(PageNumber, 1.5f);
                    break;
                case MagnificationFactor.Percent_200:
                    Magnificator = GetZoomString(PageNumber, 2f);
                    break;
                case MagnificationFactor.Percent_400:
                    Magnificator = GetZoomString(PageNumber, 4f);
                    break;
                case MagnificationFactor.Percent_800:
                    Magnificator = GetZoomString(PageNumber, 8f);
                    break;
            }

            string targetPage = ObjNumberRef(FPagesRef[PageNumber - 1]);
            WriteLn(pdf, String.Format("/D [{0}{1}]", targetPage, Magnificator));
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");
        }

        private void AddPDFFooter()
        {
            if (!FTextInCurves)
                foreach (ExportTTFFont font in FFonts)
                    WriteFont(font);

            FPagesNumber = 1;
            FXRef[0] = pdf.Position;
            WriteLn(pdf, ObjNumber(FPagesNumber));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /Pages");
            Write(pdf, "/Kids [");
            foreach (long page in FPagesRef)
                Write(pdf, ObjNumberRef(page) + " ");
            WriteLn(pdf, "]");
            WriteLn(pdf, "/Count " + FPagesRef.Count.ToString());
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");

            if (FOutline)
            {
                FastReport.Preview.Outline outline = Report.PreparedPages.Outline;
                FOutlineNumber = UpdateXRef();
                OutlineTree = new PDFOutlineNode();
                OutlineTree.Number = FOutlineNumber;
                BuildOutline(OutlineTree, outline.Xml);
                WriteOutline(OutlineTree);
            }

            if (!isPdfX() && FDefaultZoom != MagnificationFactor.Default)
                    SetMagnificationFactor(FDefaultPage, FDefaultZoom);

            if (!isPdfX())
                WriteAnnots();

            if (isPdfX())
            {
                AddMetaDataPdfX();
                if (ColorSpace == PdfColorSpace.RGB)
                    AddColorProfile("PDFX", "pdfxprofile");
                else if (ColorSpace == PdfColorSpace.CMYK)
                    AddColorProfile("PDFX", "pdfcmykprofile");
            }

            if (isPdfA())
            {
                AddAttachments();
                AddStructure();
                AddMetaDataPdfA();
                if (ColorSpace == PdfColorSpace.RGB)
                    AddColorProfile("PDFA1", "pdfaprofile");
                else if (ColorSpace == PdfColorSpace.CMYK)
                    AddColorProfile("PDFA1", "pdfcmykprofile");
            }

            FInfoNumber = UpdateXRef();
            StringBuilder sb = new StringBuilder(1024);
            sb.AppendLine(ObjNumber(FInfoNumber));
            sb.Append("<<");
            sb.Append("/Title ");

            if (isPdfX() && String.IsNullOrEmpty(FTitle))
                FTitle = "FastReport.Net";

            PrepareString(FTitle, FEncKey, FEncrypted, FInfoNumber, sb);
            sb.Append("/Author ");
            PrepareString(FAuthor, FEncKey, FEncrypted, FInfoNumber, sb);
            sb.Append("/Subject ");
            PrepareString(FSubject, FEncKey, FEncrypted, FInfoNumber, sb);
            sb.Append("/Keywords ");
            PrepareString(FKeywords, FEncKey, FEncrypted, FInfoNumber, sb);
            sb.Append("/Creator ");
            PrepareString(FCreator, FEncKey, FEncrypted, FInfoNumber, sb);
            sb.Append("/Producer ");
            PrepareString(FProducer, FEncKey, FEncrypted, FInfoNumber, sb);

            string s = "D:" + DateTime.Now.ToString("yyyyMMddHHmmss");

            if (FEncrypted)
            {
                sb.Append("/CreationDate ");
                PrepareString(s, FEncKey, FEncrypted, FInfoNumber, sb);
                sb.Append("/ModDate ");
                PrepareString(s, FEncKey, FEncrypted, FInfoNumber, sb);
            }
            else
            {
                sb.AppendLine("/CreationDate (" + s + ")");
                sb.AppendLine("/ModDate (" + s + ")");
            }

            if (PdfCompliance == PdfStandard.PdfX_3)
            {
                sb.AppendLine("/GTS_PDFXVersion(PDF/X-3:2003)");
                sb.AppendLine("/Trapped /False");
            }
            else if (PdfCompliance == PdfStandard.PdfX_4)
            {
                sb.AppendLine("/GTS_PDFXVersion(PDF/X-4)");
                sb.AppendLine("/Trapped /False");
            }

            sb.AppendLine(">>");
            sb.AppendLine("endobj");

            Write(pdf, sb.ToString());

            FRootNumber = UpdateXRef();
            WriteLn(pdf, ObjNumber(FRootNumber));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /Catalog");
            WriteLn(pdf, "/Version /" + getPdfVersion());
            WriteLn(pdf, "/MarkInfo << /Marked true >>");

            WriteLn(pdf, "/Pages " + ObjNumberRef(FPagesNumber));
            if (!isPdfX())
            {
                if (FDefaultZoom != MagnificationFactor.Default)
                    WriteLn(pdf, "/OpenAction " + ObjNumberRef(FActionDict));
            }

            if (FShowPrintDialog)
            {
                WriteLn(pdf, "/Names <</JavaScript " + ObjNumberRef(FPrintDict) + ">>");
            }

            Write(pdf, "/PageMode ");

            if (FOutline)
            {
                WriteLn(pdf, "/UseOutlines");
                WriteLn(pdf, "/Outlines " + ObjNumberRef(FOutlineNumber));
            }
            else
            {
                WriteLn(pdf, "/UseNone");
            }

            if (isPdfA())
            {
                WriteLn(pdf, "/Metadata " + ObjNumberRef(FMetaFileId));
                if (embeddedFiles.Count > 0)
                {
                    Write(pdf, "/AF " + ObjNumberRef(FAttachmentsListId));
                    WriteLn(pdf, " /Names << /EmbeddedFiles " + ObjNumberRef(FAttachmentsNamesId) + " >>");
                }
                WriteLn(pdf, "/OutputIntents [ " + ObjNumberRef(FColorProfileId) + " ]");
                WriteLn(pdf, "/StructTreeRoot " + ObjNumberRef(FStructId));
            }

            if (isPdfX())
            {
                WriteLn(pdf, "/Metadata " + ObjNumberRef(FMetaFileId));
                WriteLn(pdf, "/OutputIntents [ " + ObjNumberRef(FColorProfileId) + " ]");
            }

            WriteLn(pdf, "/ViewerPreferences <<");

            if (FDisplayDocTitle && !String.IsNullOrEmpty(FTitle))
                WriteLn(pdf, "/DisplayDocTitle true");
            if (FHideToolbar)
                WriteLn(pdf, "/HideToolbar true");
            if (FHideMenubar)
                WriteLn(pdf, "/HideMenubar true");
            if (FHideWindowUI)
                WriteLn(pdf, "/HideWindowUI true");
            if (FFitWindow)
                WriteLn(pdf, "/FitWindow true");
            if (FCenterWindow)
                WriteLn(pdf, "/CenterWindow true");
            if (!FPrintScaling)
                WriteLn(pdf, "/PrintScaling false"); // /None

            WriteLn(pdf, ">>");
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");
            FStartXRef = pdf.Position;
            WriteLn(pdf, "xref");
            WriteLn(pdf, "0 " + (FXRef.Count + 1).ToString());
            WriteLn(pdf, "0000000000 65535 f");
            foreach (long xref in FXRef)
                WriteLn(pdf, PrepXRefPos(xref) + " 00000 n");
            WriteLn(pdf, "trailer");
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Size " + (FXRef.Count + 1).ToString());
            WriteLn(pdf, "/Root " + ObjNumberRef(FRootNumber));
            WriteLn(pdf, "/Info " + ObjNumberRef(FInfoNumber));
            WriteLn(pdf, "/ID [<" + FFileID + "><" + FFileID + ">]");
            if (FEncrypted)
                WriteLn(pdf, GetEncryptionDescriptor());
            WriteLn(pdf, ">>");
            WriteLn(pdf, "startxref");
            WriteLn(pdf, FStartXRef.ToString());
            WriteLn(pdf, "%%EOF");
        }

        private void AddEmbeddedFileItem(EmbeddedFile file)
        {
            long fileRef = UpdateXRef();            
            WriteLn(pdf, ObjNumber(fileRef));
            Write(pdf, "<< /Params << /ModDate (D:" + file.ModDate.ToString("yyyyMMddHHmmss") + ")");
            Write(pdf, " /Size " + file.FileStream.Length.ToString());
            WriteLn(pdf, " >>");
            WriteLn(pdf, "/Subtype /" + file.MIME.Replace("/", "#2f"));
            WriteLn(pdf, "/Type /EmbeddedFile");
            WritePDFStream(pdf, file.FileStream, fileRef, FCompressed, FEncrypted, false, true, true);
            long fileRel = UpdateXRef();
            file.Xref = fileRel;
            WriteLn(pdf, ObjNumber(fileRel));
            WriteLn(pdf, "<< /AFRelationship /" + file.Relation.ToString());
            StringBuilder desc = new StringBuilder();
            PrepareString(file.Description, FEncKey, FEncrypted, fileRel, desc);
            WriteLn(pdf, "/Desc " + desc.ToString());
            Write(pdf, "/EF <<");
            Write(pdf, " /F " + ObjNumberRef(fileRef));
            Write(pdf, " /UF " + ObjNumberRef(fileRef));
            WriteLn(pdf, " >>");
            WriteLn(pdf, "/F (" + file.Name + ")");
            WriteLn(pdf, "/Type /Filespec");
            StringBuilder uf = new StringBuilder();
            StrToUTF16(file.Name, uf);
            WriteLn(pdf, "/UF <" + uf.ToString() + ">");
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");
            
        }

        private void AddAttachments()
        {
            if (embeddedFiles.Count > 0)
            {
                foreach (EmbeddedFile file in embeddedFiles)
                    AddEmbeddedFileItem(file);

                FAttachmentsNamesId = UpdateXRef();
                WriteLn(pdf, ObjNumber(FAttachmentsNamesId));
                Write(pdf, "<< /Names [");
                foreach(EmbeddedFile file in embeddedFiles)
                {
                    Write(pdf, " (" + file.Name + ") ");
                    Write(pdf, ObjNumberRef(file.Xref));
                }
                WriteLn(pdf, " ] >>");
                WriteLn(pdf, "endobj");

                FAttachmentsListId = UpdateXRef();
                WriteLn(pdf, ObjNumber(FAttachmentsListId));
                Write(pdf, "[ ");
                foreach (EmbeddedFile file in embeddedFiles)
                    Write(pdf, ObjNumberRef(file.Xref) + " ");
                WriteLn(pdf, "]");
                WriteLn(pdf, "endobj");
            }
        }

        private void AddStructure()
        {
            long roleMaps = UpdateXRef();
            WriteLn(pdf, ObjNumber(roleMaps));
            WriteLn(pdf, "<<\n/Footnote /Note\n/Endnote /Note\n/Textbox /Sect\n/Header /Sect\n/Footer /Sect\n/InlineShape /Sect\n/Annotation /Sect\n/Artifact /Sect\n/Workbook /Document\n/Worksheet /Part\n/Macrosheet /Part\n/Chartsheet /Part\n/Dialogsheet /Part\n/Slide /Part\n/Chart /Sect\n/Diagram /Figure\n>>\nendobj");

            FStructId = UpdateXRef();
            WriteLn(pdf, ObjNumber(FStructId));
            WriteLn(pdf, "<<\n/Type /StructTreeRoot");
            WriteLn(pdf, "/RoleMap " + ObjNumberRef(roleMaps));
            // /ParentTree /K /ParentTreeNextKey
            WriteLn(pdf, ">>\nendobj");
        }      

        private void AddColorProfile(string GTS, string ICC)
        {
            string profileName = "default";

            // color profile stream
            long FColorProfileStreamId = UpdateXRef();
            WriteLn(pdf, ObjNumber(FColorProfileStreamId));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/N 3");

            if (ColorProfile != null)
            {
                string pname = ParseICCFile(ColorProfile);
                if (pname != null)
                {
                    profileName = pname.Trim('\0');
                    using (MemoryStream profileStream = new MemoryStream(ColorProfile))
                    {
                        WritePDFStream(pdf, profileStream, FColorProfileStreamId, FCompressed, FEncrypted, false, true);
                    }
                }
            }

            if (profileName == "default")
            {
                Assembly a = Assembly.GetExecutingAssembly();
                using (Stream stream = a.GetManifestResourceStream("FastReport.Export.Pdf." + ICC + ".icc"))
                {
                    byte[] buf = new byte[stream.Length];
                    stream.Read(buf, 0, (int)stream.Length);
                    string pname = ParseICCFile(buf);
                    if (pname != null) profileName = pname.Trim('\0');
                    using (MemoryStream profileStream = new MemoryStream(buf))
                    {
                        WritePDFStream(pdf, profileStream, FColorProfileStreamId, FCompressed, FEncrypted, false, true);
                    }
                }
            }

            // color profile intent
            FColorProfileId = UpdateXRef();
            WriteLn(pdf, ObjNumber(FColorProfileId));
            WriteLn(pdf, "<<");
            WriteLn(pdf, "/Type /OutputIntent");
            WriteLn(pdf, "/S /GTS_" + GTS);
            WriteLn(pdf, String.Format("/OutputCondition ({0})", profileName));
            WriteLn(pdf, String.Format("/OutputConditionIdentifier ({0})", profileName));
            WriteLn(pdf, String.Format("/Info ({0})", profileName));
            WriteLn(pdf, "/DestOutputProfile " + ObjNumberRef(FColorProfileStreamId));
            WriteLn(pdf, ">>");
            WriteLn(pdf, "endobj");
        }

        private string ParseICCFile(byte[] file)
        {
            using (MemoryStream profileStream = new MemoryStream(file))
            {
                profileStream.Position = 128;
                byte[] temp = new byte[4];
                profileStream.Read(temp, 0, temp.Length);
                int count = temp[3];//limit to 255 tag count, for error avoid
                uint offset = 0;//if 0 then error
                uint size = 0;//size of desc
                byte[] desc = new byte[4] { 0x64, 0x65, 0x73, 0x63 };
                for (int i = 0; i < count; i++)
                {
                    //try to find desc tag
                    profileStream.Read(temp, 0, temp.Length);//read signature
                    bool flag_eq = true;
                    for (int j = 0; j < temp.Length; j++)
                        if (temp[j] != desc[j])
                            flag_eq = false;
                    profileStream.Read(temp, 0, temp.Length);//read offset
                    if (flag_eq)
                    {
                        offset = (uint)(temp[0] << 24) + (uint)(temp[1] << 16) + (uint)(temp[2] << 8) + (uint)(temp[3]);
                    }
                    profileStream.Read(temp, 0, temp.Length);//read lenght
                    if (flag_eq)
                    {
                        size = (uint)(temp[0] << 24) + (uint)(temp[1] << 16) + (uint)(temp[2] << 8) + (uint)(temp[3]);
                        break;
                    }
                }
                if (offset == 0)
                    return null;
                profileStream.Position = offset;
                profileStream.Read(temp, 0, temp.Length);//read signature
                for (int j = 0; j < temp.Length; j++)
                    if (temp[j] != desc[j])
                        return null;//test 2 for desc error
                profileStream.Read(temp, 0, temp.Length);//read 0
                profileStream.Read(temp, 0, temp.Length);//read lenght
                uint len = (uint)(temp[3]);
                byte[] result = new byte[len];

                profileStream.Read(result, 0, result.Length);
                return Encoding.ASCII.GetString(result);
            }
        }

        private void AddMetaDataPdfA()
        {
            PDFMetaData pmd = new PDFMetaData();
            pmd.Creator = Creator;
            pmd.Description = Subject;
            pmd.Keywords = Keywords;
            pmd.Title = Title;
            pmd.Producer = Producer;
            pmd.CreateDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            pmd.DocumentID = FFileID;
            pmd.InstanceID = FFileID;
            pmd.ZUGFeRD = ZUGFeRDDescription;

            switch (PdfCompliance)
            {
                default:
                    pmd.Part = "2";
                    pmd.Conformance = "A";
                    break;
                case PdfStandard.PdfA_2b:
                    pmd.Part = "2";
                    pmd.Conformance = "B";
                    break;
                case PdfStandard.PdfA_3a:
                    pmd.Part = "3";
                    pmd.Conformance = "A";
                    break;
                case PdfStandard.PdfA_3b:
                    pmd.Part = "3";
                    pmd.Conformance = "B";
                    break;
            }

            FMetaFileId = UpdateXRef();
            WriteLn(pdf, ObjNumber(FMetaFileId));
            WriteLn(pdf, "<< /Type /Metadata /Subtype /XML ");
            using (MemoryStream metaStream = new MemoryStream())
            {
                ExportUtils.WriteLn(metaStream, pmd.MetaDataString);
                metaStream.Position = 0;
                WritePDFStream(pdf, metaStream, FMetaFileId, false, FEncrypted, false, true);
            }
        }

        private void AddMetaDataPdfX()
        {
            string metadata = null;

            if (PdfCompliance == PdfStandard.PdfX_3)
                metadata = "MetaDataX3";
            else if (PdfCompliance == PdfStandard.PdfX_4)
                metadata = "MetaDataX4";
            else
                throw new Exception("Error while adding metadata to PDF. Unknown PDF/X version: " + PdfCompliance.ToString());

            // to pass adobe acrobat compliance test
            if (PdfCompliance == PdfStandard.PdfX_4)
            {
                Author = Creator;
            }

            PDFMetaData pmd = new PDFMetaData(metadata);
            pmd.Creator = Creator;
            pmd.Description = Subject;
            pmd.Keywords = Keywords;
            pmd.Title = Title;
            pmd.Producer = Producer;
            pmd.CreateDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            pmd.DocumentID = FFileID;
            pmd.InstanceID = FFileID;

            // to pass adobe acrobat compliance test
            if (PdfCompliance == PdfStandard.PdfX_4)
            {
                if (pmd.Title == null || pmd.Title.Trim() == "")
                    pmd.Title = "FastReport.Net";
            }

            FMetaFileId = UpdateXRef();
            WriteLn(pdf, ObjNumber(FMetaFileId));
            WriteLn(pdf, "<< /Type /Metadata /Subtype /XML ");
            using (MemoryStream metaStream = new MemoryStream())
            {
                ExportUtils.WriteLn(metaStream, pmd.MetaDataString);
                metaStream.Position = 0;
                WritePDFStream(pdf, metaStream, FMetaFileId, false, FEncrypted, false, true);
            }
        }

        #endregion

        #region Protected Methods
        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("PdfFile");
        }

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (PDFExportForm form = new PDFExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            graphics = Graphics.FromHwnd(IntPtr.Zero);
            FXRef = new List<long>();
            FPagesRef = new List<long>();
            FPagesTopMargins = new List<float>();
            FPagesHeights = new List<float>();
            FFonts = new List<ExportTTFFont>();
            hashList = new Dictionary<string, long>();
            FPageAnnots = new StringBuilder();
            FAnnots = new List<PDFExportAnnotation>();

            FFileID = ExportUtils.GetID().Replace("-", "");
            if (!String.IsNullOrEmpty(FOwnerPassword) || !String.IsNullOrEmpty(FUserPassword))
            {
                FEncrypted = true;
                FEmbeddingFonts = true;
                PrepareKeys();
            }
            if (FBuffered)
                pdf = new MemoryStream();
            else
                pdf = Stream;

            if (Report.PreparedPages.Outline.Xml.Count == 0)
                FOutline = false;

            AddPDFHeader();

            if (FShowPrintDialog)
            {
                long FPrintDictJS = UpdateXRef();
                WriteLn(pdf, ObjNumber(FPrintDictJS));
                WriteLn(pdf, @"<</S/JavaScript/JS(this.print\({bUI:true,bSilent:false,bShrinkToFit:true}\);)>>");
                WriteLn(pdf, "endobj");
                FPrintDict = UpdateXRef();
                WriteLn(pdf, ObjNumber(FPrintDict));
                WriteLn(pdf, "<</Names[(0000000000000000) " + ObjNumberRef(FPrintDictJS) + "] >>");
                WriteLn(pdf, "endobj");
            }

        }

        /*/// <inheritdoc/>
        protected override void ExportPage(int pageNo)
        {
            using (ReportPage page = GetPage(pageNo))
            {
                AddPage(page);
                ObjectCollection allObjects = page.AllObjects;
                for (int i = 0; i < allObjects.Count; i++)
                {
                    ReportComponentBase c = allObjects[i] as ReportComponentBase;
                    if (c != null)
                    {
                        c.Dispose();
                        c = null;
                    }
                }
            }
            if (pageNo % 50 == 0)
                Application.DoEvents();

        }*/

        /// <summary>
        /// Begin exporting of page
        /// </summary>
        /// <param name="page"></param>
        protected override void ExportPageBegin(ReportPage page)
        {
            if (ExportMode == ExportType.Export)
                base.ExportPageBegin(page);
            FPageFonts = new List<ExportTTFFont>();
            FTrasparentStroke = new List<string>();
            FTrasparentFill = new List<string>();
            picResList = new List<long>();

            paperWidth = ExportUtils.GetPageWidth(page) * Units.Millimeters;
            paperHeight = ExportUtils.GetPageHeight(page) * Units.Millimeters;

            FMarginWoBottom = (ExportUtils.GetPageHeight(page) - page.TopMargin) * PDF_PAGE_DIVIDER;
            FMarginLeft = page.LeftMargin * PDF_PAGE_DIVIDER;

            FPagesHeights.Add(FMarginWoBottom);
            FPagesTopMargins.Add(page.TopMargin * PDF_PAGE_DIVIDER);

            FContentsPos = 0;
            contentBuilder = new StringBuilder(65535);

            // page fill
            if (FBackground)
                using (TextObject pageFill = new TextObject())
                {
                    pageFill.Fill = page.Fill;
                    pageFill.Left = -FMarginLeft / PDF_DIVIDER;
                    pageFill.Top = -page.TopMargin * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageFill.Width = ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageFill.Height = ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    AddTextObject(pageFill, false, contentBuilder);
                }

            // bitmap watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page, contentBuilder);

            // text watermark on bottom
            if (page.Watermark.Enabled && !page.Watermark.ShowTextOnTop)
                AddTextWatermark(page, contentBuilder);

            // page borders
            if (page.Border.Lines != BorderLines.None)
            {
                using (TextObject pageBorder = new TextObject())
                {
                    pageBorder.Border = page.Border;
                    pageBorder.Left = 0;
                    pageBorder.Top = 0;
                    pageBorder.Width = (ExportUtils.GetPageWidth(page) - page.LeftMargin - page.RightMargin) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    pageBorder.Height = (ExportUtils.GetPageHeight(page) - page.TopMargin - page.BottomMargin) * PDF_PAGE_DIVIDER / PDF_DIVIDER;
                    AddTextObject(pageBorder, true, contentBuilder);
                }
            }
        }

        /// <summary>
        /// End exporting
        /// </summary>
        /// <param name="page"></param>
        protected override void ExportPageEnd(ReportPage page)
        {
            base.ExportPageEnd(page);

            // bitmap watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowImageOnTop)
                AddBitmapWatermark(page, contentBuilder);

            // text watermark on top
            if (page.Watermark.Enabled && page.Watermark.ShowTextOnTop)
                AddTextWatermark(page, contentBuilder);

            // write page
            FContentsPos = UpdateXRef();
            WriteLn(pdf, ObjNumber(FContentsPos));

            using (MemoryStream tempContentStream = new MemoryStream())
            {
                Write(tempContentStream, contentBuilder.ToString());
                tempContentStream.Position = 0;
                WritePDFStream(pdf, tempContentStream, FContentsPos, FCompressed, FEncrypted, true, true);
            }

            if (!FTextInCurves)
                if (FPageFonts.Count > 0)
                    for (int i = 0; i < FPageFonts.Count; i++)
                        if (!FPageFonts[i].Saved)
                        {
                            FPageFonts[i].Reference = UpdateXRef();
                            FPageFonts[i].Saved = true;
                        }

            long PageNumber = UpdateXRef();
            FPagesRef.Add(PageNumber);
            WriteLn(pdf, ObjNumber(PageNumber));
            StringBuilder sb = new StringBuilder(512);
            sb.AppendLine("<<").AppendLine("/Type /Page");
            sb.Append("/MediaBox [0 0 ").Append(FloatToString(ExportUtils.GetPageWidth(page) * PDF_PAGE_DIVIDER)).Append(" ");
            sb.Append(FloatToString(ExportUtils.GetPageHeight(page) * PDF_PAGE_DIVIDER)).AppendLine(" ]");
            //margins
            if (isPdfX())
                sb.Append("/TrimBox [")
                    .Append(FloatToString(page.LeftMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.TopMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.RightMargin * PDF_PAGE_DIVIDER)).Append(" ")
                    .Append(FloatToString(page.BottomMargin * PDF_PAGE_DIVIDER)).Append("]");

            sb.AppendLine("/Parent 1 0 R");
            if (!isPdfX())
            {
                if (ColorSpace == PdfColorSpace.RGB)
                    sb.AppendLine("/Group << /Type /Group /S /Transparency /CS /DeviceRGB >>");
                else if (ColorSpace == PdfColorSpace.CMYK)
                    sb.AppendLine("/Group << /Type /Group /S /Transparency /CS /DeviceCMYK >>");
            }
            sb.AppendLine("/Resources << ");

            if (FPageFonts.Count > 0)
            {
                sb.Append("/Font << ");
                foreach (ExportTTFFont font in FPageFonts)
                    sb.Append(font.Name).Append(" ").Append(ObjNumberRef(font.Reference)).Append(" ");
                sb.AppendLine(" >>");
            }

            if (isPdfX())
            {
                sb.AppendLine("/ExtGState <<");
                for (int i = 0; i < FTrasparentStroke.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("S << /Type /ExtGState /ca ").Append(1).AppendLine(" >>");
                for (int i = 0; i < FTrasparentFill.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("F << /Type /ExtGState /CA ").Append(1).AppendLine(" >>");
                sb.AppendLine(">>");
            }
            else
            {
                sb.AppendLine("/ExtGState <<");
                for (int i = 0; i < FTrasparentStroke.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("S << /Type /ExtGState /ca ").Append(FTrasparentStroke[i]).AppendLine(" >>");
                for (int i = 0; i < FTrasparentFill.Count; i++)
                    sb.Append("/GS").Append(i.ToString()).Append("F << /Type /ExtGState /CA ").Append(FTrasparentFill[i]).AppendLine(" >>");
                sb.AppendLine(">>");
            }

            if (picResList.Count > 0)
            {
                sb.Append("/XObject << ");
                foreach (long resIndex in picResList)
                    sb.Append("/Im").Append(resIndex.ToString()).Append(" ").Append(ObjNumberRef(resIndex)).Append(" ");
                sb.AppendLine(" >>");
            }

            sb.AppendLine("/ProcSet [/PDF /Text /ImageC ]");
            sb.AppendLine(">>");

            sb.Append("/Contents ").AppendLine(ObjNumberRef(FContentsPos));
            if (FPageAnnots.Length > 0)
            {
                sb.AppendLine(GetPageAnnots());
                FPageAnnots.Length = 0;
            }

            sb.AppendLine(">>");
            sb.AppendLine("endobj");
            Write(pdf, sb.ToString());
        }

        /// <summary>
        /// Export of Band
        /// </summary>
        /// <param name="band"></param>
        protected override void ExportBand(Base band)
        {
            if (ExportMode == ExportType.Export)
            {
                base.ExportBand(band);
                if (band.Parent == null) return;
            }
            ExportObj(band);
            foreach (Base c in band.AllObjects)
            {
                ExportObj(c);
            }
        }

        private void ExportObj(Base c)
        {
            if (c is ReportComponentBase)
            {
                ReportComponentBase obj = c as ReportComponentBase;
                if (obj is CellularTextObject)
                    obj = (obj as CellularTextObject).GetTable();
                if (obj is TableCell)
                    return;
                else
                    if (obj is TableBase)
                {
                    TableBase table = obj as TableBase;
                    if (table.ColumnCount > 0 && table.RowCount > 0)
                    {
                        StringBuilder tableBorder = new StringBuilder(64);
                        using (TextObject tableback = new TextObject())
                        {
                            tableback.Border = table.Border;
                            tableback.Fill = table.Fill;
                            tableback.FillColor = table.FillColor;
                            tableback.Left = table.AbsLeft;
                            tableback.Top = table.AbsTop;
                            float tableWidth = 0;
                            float tableHeight = 0;
                            for (int i = 0; i < table.ColumnCount; i++)
                                tableWidth += table[i, 0].Width;
                            for (int i = 0; i < table.RowCount; i++)
                                tableHeight += table.Rows[i].Height;
                            tableback.Width = (tableWidth < table.Width) ? tableWidth : table.Width;
                            tableback.Height = tableHeight;
                            AddTextObject(tableback, false, contentBuilder);
                            DrawPDFBorder(tableback.Border, tableback.AbsLeft, tableback.AbsTop, tableback.Width, tableback.Height, tableBorder);
                        }
                        // draw cells
                        AddTable(table, true, contentBuilder);
                        // draw cells border
                        AddTable(table, false, contentBuilder);
                        // draw table border
                        contentBuilder.Append(tableBorder);
                    }
                }
                else if (obj is TextObject)
                    AddTextObject(obj as TextObject, true, contentBuilder);
                else if (obj is BandBase)
                    AddBandObject(obj as BandBase, contentBuilder);
                else if (obj is LineObject)
                    AddLine(obj as LineObject, contentBuilder);
                else if (obj is ShapeObject)
                    AddShape(obj as ShapeObject, contentBuilder);
                else if (obj is RichObject)
                    AddPictureObject(obj as ReportComponentBase, true, FRichTextQuality, contentBuilder);
                else if (obj is PolyLineObject)
                    AddPolyLine(obj as PolyLineObject, contentBuilder);
                else if (!(obj is HtmlObject))
                    AddPictureObject(obj as ReportComponentBase, true, FJpegQuality, contentBuilder);
            }
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            AddPDFFooter();
            foreach (ExportTTFFont fnt in FFonts)
                fnt.Dispose();
            if (FBuffered)
                ((MemoryStream)pdf).WriteTo(Stream);
            graphics.Dispose();
        }
        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            base.Serialize(writer);

            // Options
            writer.WriteValue("PdfCompliance", PdfCompliance);
            writer.WriteBool("EmbeddingFonts", EmbeddingFonts);
            writer.WriteBool("Background", Background);
            writer.WriteBool("TextInCurves", TextInCurves);
            writer.WriteValue("ColorSpace", ColorSpace);
            writer.WriteBool("ImagesOriginalResolution", ImagesOriginalResolution);
            writer.WriteBool("PrintOptimized", PrintOptimized);
            writer.WriteBool("JpegCompression", JpegCompression);
            writer.WriteInt("JpegQuality", JpegQuality);
            // end

            // Document Information
            writer.WriteStr("Title", Title);
            writer.WriteStr("Author", Author);
            writer.WriteStr("Subject", Subject);
            writer.WriteStr("Keywords", Keywords);
            writer.WriteStr("Creator", Creator);
            writer.WriteStr("Producer", Producer);
            // end

            // Security
            writer.WriteBool("AllowPrint", AllowPrint);
            writer.WriteBool("AllowModify", AllowModify);
            writer.WriteBool("AllowCopy", AllowCopy);
            writer.WriteBool("AllowAnnotate", AllowAnnotate);
            // end

            // Viewer
            writer.WriteBool("AutoPrint", ShowPrintDialog);
            writer.WriteBool("HideToolbar", HideToolbar);
            writer.WriteBool("HideMenubar", HideMenubar);
            writer.WriteBool("HideWindowUI", HideWindowUI);
            writer.WriteBool("FitWindow", FitWindow);
            writer.WriteBool("CenterWindow", CenterWindow);
            writer.WriteBool("PrintScaling", PrintScaling);
            writer.WriteBool("Outline", Outline);
            writer.WriteValue("DefaultZoom", DefaultZoom);
            // end
        }

        /// <summary>
        /// Add an embedded XML file (only for PDF/A-3 standard).
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="description">Description</param>
        /// <param name="modDate">Modification date</param>
        /// <param name="fileStream">File stream</param>
        public void AddEmbeddedXML(string name, string description, DateTime modDate, Stream fileStream)
        {
            AddEmbeddedXML(name, description, modDate, fileStream, ZUGFeRD_ConformanceLevel.BASIC);
        }

        /// <summary>
        /// Add an embedded XML file (only for PDF/A-3 standard).
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="description">Description</param>
        /// <param name="modDate">Modification date</param>
        /// <param name="fileStream">File stream</param>
        /// <param name="ZUGFeRDLevel">ZUGFeRD Conformance Level</param>
        public void AddEmbeddedXML(string name, string description, DateTime modDate, Stream fileStream, ZUGFeRD_ConformanceLevel ZUGFeRDLevel)
        {
            ZUGFeRDDescription = String.Format("<rdf:Description xmlns:zf=\"urn:ferd:pdfa:CrossIndustryDocument:invoice:1p0#\" rdf:about=\"\" zf:ConformanceLevel=\"{0}\" zf:DocumentFileName=\"{1}\" zf:DocumentType=\"INVOICE\" zf:Version=\"1.0\"/>", ZUGFeRDLevel.ToString(), name);
            AddEmbeddedFile(name, description, modDate, EmbeddedRelation.Alternative, "text/xml", fileStream);
        }

        /// <summary>
        /// Add an embedded file (only for PDF/A-3 standard).
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="description">Description</param>
        /// <param name="modDate">Modification date</param>
        /// <param name="relation">Relation type</param>
        /// <param name="mime">MIME type</param>
        /// <param name="fileStream">File stream</param>
        public void AddEmbeddedFile(string name, string description, DateTime modDate, EmbeddedRelation relation, string mime, Stream fileStream)
        {
            EmbeddedFile file = new EmbeddedFile();
            file.Name = name;
            file.Description = description;
            file.ModDate = modDate;
            file.Relation = relation;
            file.MIME = mime;
            file.FileStream = fileStream;
            embeddedFiles.Add(file);
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFExport"/> class.
        /// </summary>
        public PDFExport()
        {
            FNumberFormatInfo = new NumberFormatInfo();
            FNumberFormatInfo.NumberGroupSeparator = String.Empty;
            FNumberFormatInfo.NumberDecimalSeparator = ".";
            embeddedFiles = new List<EmbeddedFile>();
            exportMode = ExportType.Export;
        }

        #endregion
    }
}