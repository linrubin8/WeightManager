using System;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Pdf;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="PDFExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class PDFExportForm : BaseExportForm
    {
        #region Methods

        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            PDFExport pdfExport = Export as PDFExport;

            // Options
            cbPdfStandard.SelectedIndex = (int)pdfExport.PdfCompliance;
            cbEmbeddedFonts.Checked = pdfExport.EmbeddingFonts;
            cbBackground.Checked = pdfExport.Background;
            cbTextInCurves.Checked = pdfExport.TextInCurves;
            cbColorSpace.SelectedIndex = (int)pdfExport.ColorSpace;
            cbOriginalResolution.Checked = pdfExport.ImagesOriginalResolution;
            cbPrintOptimized.Checked = pdfExport.PrintOptimized;
            cbJpegCompression.Checked = pdfExport.JpegCompression;
            nudJpegQuality.Value = pdfExport.JpegQuality;

            // Document Information
            tbTitle.Text = pdfExport.Title;
            tbAuthor.Text = pdfExport.Author;
            tbSubject.Text = pdfExport.Subject;
            tbKeywords.Text = pdfExport.Keywords;
            tbCreator.Text = pdfExport.Creator;
            tbProducer.Text = pdfExport.Producer;

            // Security
            tbOwnerPassword.Text = pdfExport.OwnerPassword;
            tbUserPassword.Text = pdfExport.UserPassword;
            cbPrintTheDocument.Checked = pdfExport.AllowPrint;
            cbModifyTheDocument.Checked = pdfExport.AllowModify;
            cbCopyOfTextAndGraphics.Checked = pdfExport.AllowCopy;
            cbAnnotations.Checked = pdfExport.AllowAnnotate;

            // Viewer
            cbPrintDialog.Checked = pdfExport.ShowPrintDialog;
            cbHideToolbar.Checked = pdfExport.HideToolbar;
            cbHideMenubar.Checked = pdfExport.HideMenubar;
            cbHideUI.Checked = pdfExport.HideWindowUI;
            cbFitWindow.Checked = pdfExport.FitWindow;
            cbCenterWindow.Checked = pdfExport.CenterWindow;
            cbPrintScaling.Checked = pdfExport.PrintScaling;
            cbOutline.Checked = pdfExport.Outline;
            cbbZoom.SelectedIndex = (int)pdfExport.DefaultZoom;
        }

        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            PDFExport pdfExport = Export as PDFExport;

            // Options
            pdfExport.PdfCompliance = (PDFExport.PdfStandard)cbPdfStandard.SelectedIndex;
            pdfExport.EmbeddingFonts = cbEmbeddedFonts.Checked;
            pdfExport.Background = cbBackground.Checked;
            pdfExport.TextInCurves = cbTextInCurves.Checked;
            pdfExport.ColorSpace = (PDFExport.PdfColorSpace)cbColorSpace.SelectedIndex;
            pdfExport.ImagesOriginalResolution = cbOriginalResolution.Checked;
            pdfExport.PrintOptimized = cbPrintOptimized.Checked;
            pdfExport.JpegCompression = cbJpegCompression.Checked;
            pdfExport.JpegQuality = (int)nudJpegQuality.Value;

            // Document Information
            pdfExport.Title = tbTitle.Text;
            pdfExport.Author = tbAuthor.Text;
            pdfExport.Subject = tbSubject.Text;
            pdfExport.Keywords = tbKeywords.Text;
            pdfExport.Creator = tbCreator.Text;
            pdfExport.Producer = tbProducer.Text;

            // Security
            pdfExport.OwnerPassword = tbOwnerPassword.Text;
            pdfExport.UserPassword = tbUserPassword.Text;
            pdfExport.AllowPrint = cbPrintTheDocument.Checked;
            pdfExport.AllowModify = cbModifyTheDocument.Checked;
            pdfExport.AllowCopy = cbCopyOfTextAndGraphics.Checked;
            pdfExport.AllowAnnotate = cbAnnotations.Checked;

            // Viewer
            pdfExport.ShowPrintDialog = cbPrintDialog.Checked;
            pdfExport.HideToolbar = cbHideToolbar.Checked;
            pdfExport.HideMenubar = cbHideMenubar.Checked;
            pdfExport.HideWindowUI = cbHideUI.Checked;
            pdfExport.FitWindow = cbFitWindow.Checked;
            pdfExport.CenterWindow = cbCenterWindow.Checked;
            pdfExport.PrintScaling = cbPrintScaling.Checked;
            pdfExport.Outline = cbOutline.Checked;
            pdfExport.DefaultZoom = (PDFExport.MagnificationFactor)cbbZoom.SelectedIndex;
        }

        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Pdf");

            // Main
            Text = res.Get("");
            panPages.Text = res.Get("Export");

            // Options
            pageControlOptions.Text = Res.Get("Export,Misc,Options");
            gbOptions.Text = Res.Get("Export,Misc,Options");
            lblCompliance.Text = res.Get("PDFCompliance");
            cbEmbeddedFonts.Text = res.Get("EmbeddedFonts");
            cbBackground.Text = res.Get("Background");
            cbTextInCurves.Text = res.Get("TextInCurves");
            gbImages.Text = res.Get("Images");
            lblColorSpace.Text = res.Get("ColorSpace");
            cbOriginalResolution.Text = res.Get("OriginalResolution");
            cbPrintOptimized.Text = res.Get("PrintOptimized");
            cbJpegCompression.Text = res.Get("JpegCompression");
            lblJpegQuality.Text = res.Get("JpegQuality");

            // Document Information
            pageControlInformation.Text = res.Get("Information");
            gbDocumentInfo.Text = res.Get("DocumentInformation");
            lbTitle.Text = res.Get("Title");
            lbAuthor.Text = res.Get("Author");
            lbSubject.Text = res.Get("Subject");
            lbKeywords.Text = res.Get("Keywords");
            lbCreator.Text = res.Get("Creator");
            lbProducer.Text = res.Get("Producer");

            // Security
            pageControlSecurity.Text = res.Get("Security");
            gbAuth.Text = res.Get("Authentification");
            lbOwnerPassword.Text = res.Get("OwnerPassword");
            lbUserPassword.Text = res.Get("UserPassword");
            gbPermissions.Text = res.Get("Permissions");
            cbPrintTheDocument.Text = res.Get("PrintTheDocument");
            cbModifyTheDocument.Text = res.Get("ModifyTheDocument");
            cbCopyOfTextAndGraphics.Text = res.Get("CopyOfTextAndGraphics");
            cbAnnotations.Text = res.Get("AddOrModifyTextAnnotations");

            // Viewer
            pageControlViewer.Text = res.Get("Viewer");
            gbViewerPrfs.Text = res.Get("ViewerPreferences");
            cbPrintDialog.Text = res.Get("ShowPrintDialog");
            cbHideToolbar.Text = res.Get("HideToolbar");
            cbHideMenubar.Text = res.Get("HideMenubar");
            cbHideUI.Text = res.Get("HideWindowUserInterface");
            cbFitWindow.Text = res.Get("FitWindow");
            cbCenterWindow.Text = res.Get("CenterWindow");
            cbPrintScaling.Text = res.Get("PrintScaling");
            cbOutline.Text = res.Get("Outline");
            lblInitialZoom.Text = res.Get("InitialZoom");
            cbbZoom.Items[0] = res.Get("ActualSize");
            cbbZoom.Items[1] = res.Get("FitPage");
            cbbZoom.Items[2] = res.Get("FitWidth");
            cbbZoom.Items[3] = res.Get("Default");

            // apply Right to Left layout if needed
            if (Config.RightToLeft)
            {
                cbPdfStandard.Left = lblCompliance.Left - cbPdfStandard.Width;
                cbColorSpace.Left = lblColorSpace.Left - cbColorSpace.Width;
                nudJpegQuality.Left = lblJpegQuality.Left - nudJpegQuality.Width;
                cbbZoom.Left = lblInitialZoom.Left - cbbZoom.Width;
            }
            else
            {
                cbPdfStandard.Left = lblCompliance.Right;
                cbColorSpace.Left = lblColorSpace.Right;
                nudJpegQuality.Left = lblJpegQuality.Right;
                cbbZoom.Left = lblInitialZoom.Right;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFExportForm"/> class.
        /// </summary>
        public PDFExportForm()
        {
            InitializeComponent();

            cbPdfStandard.SelectedIndexChanged += optionChanged;
            cbEmbeddedFonts.CheckedChanged += optionChanged;
            cbTextInCurves.CheckedChanged += optionChanged;
            cbColorSpace.SelectedIndexChanged += optionChanged;
            cbOriginalResolution.CheckedChanged += optionChanged;
            cbJpegCompression.CheckedChanged += optionChanged;

            // init
            optionChanged(null, null);

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side of Options tab
                lblCompliance.Left = gbOptions.Width - lblCompliance.Left - lblCompliance.Width;
                lblCompliance.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbEmbeddedFonts.Left = gbOptions.Width - cbEmbeddedFonts.Left - cbEmbeddedFonts.Width;
                cbEmbeddedFonts.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbBackground.Left = gbOptions.Width - cbBackground.Left - cbBackground.Width;
                cbBackground.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbTextInCurves.Left = gbOptions.Width - cbTextInCurves.Left - cbTextInCurves.Width;
                cbTextInCurves.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblColorSpace.Left = gbImages.Width - lblColorSpace.Left - lblColorSpace.Width;
                lblColorSpace.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbOriginalResolution.Left = gbImages.Width - cbOriginalResolution.Left - cbOriginalResolution.Width;
                cbOriginalResolution.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPrintOptimized.Left = gbImages.Width - cbPrintOptimized.Left - cbPrintOptimized.Width;
                cbPrintOptimized.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbJpegCompression.Left = gbImages.Width - cbJpegCompression.Left - cbJpegCompression.Width;
                cbJpegCompression.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblJpegQuality.Left = gbImages.Width - lblJpegQuality.Left - lblJpegQuality.Width;
                lblJpegQuality.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move components to other side of Information tab
                lbTitle.Left = gbDocumentInfo.Width - lbTitle.Left - lbTitle.Width;
                lbTitle.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbTitle.Left = gbDocumentInfo.Width - tbTitle.Left - tbTitle.Width;
                tbTitle.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbAuthor.Left = gbDocumentInfo.Width - lbAuthor.Left - lbAuthor.Width;
                lbAuthor.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbAuthor.Left = gbDocumentInfo.Width - tbAuthor.Left - tbAuthor.Width;
                tbAuthor.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbSubject.Left = gbDocumentInfo.Width - lbSubject.Left - lbSubject.Width;
                lbSubject.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbSubject.Left = gbDocumentInfo.Width - tbSubject.Left - tbSubject.Width;
                tbSubject.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbKeywords.Left = gbDocumentInfo.Width - lbKeywords.Left - lbKeywords.Width;
                lbKeywords.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbKeywords.Left = gbDocumentInfo.Width - tbKeywords.Left - tbKeywords.Width;
                tbKeywords.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbCreator.Left = gbDocumentInfo.Width - lbCreator.Left - lbCreator.Width;
                lbCreator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbCreator.Left = gbDocumentInfo.Width - tbCreator.Left - tbCreator.Width;
                tbCreator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbProducer.Left = gbDocumentInfo.Width - lbProducer.Left - lbProducer.Width;
                lbProducer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbProducer.Left = gbDocumentInfo.Width - tbProducer.Left - tbProducer.Width;
                tbProducer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move components to other side of Security tab
                lbOwnerPassword.Left = gbAuth.Width - lbOwnerPassword.Left - lbOwnerPassword.Width;
                lbOwnerPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbOwnerPassword.Left = gbAuth.Width - tbOwnerPassword.Left - tbOwnerPassword.Width;
                tbOwnerPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lbUserPassword.Left = gbAuth.Width - lbUserPassword.Left - lbUserPassword.Width;
                lbUserPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbUserPassword.Left = gbAuth.Width - tbUserPassword.Left - tbUserPassword.Width;
                tbUserPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPrintTheDocument.Left = gbPermissions.Width - cbPrintTheDocument.Left - cbPrintTheDocument.Width;
                cbPrintTheDocument.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbModifyTheDocument.Left = gbPermissions.Width - cbModifyTheDocument.Left - cbModifyTheDocument.Width;
                cbModifyTheDocument.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbCopyOfTextAndGraphics.Left = gbPermissions.Width - cbCopyOfTextAndGraphics.Left - cbCopyOfTextAndGraphics.Width;
                cbCopyOfTextAndGraphics.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbAnnotations.Left = gbPermissions.Width - cbAnnotations.Left - cbAnnotations.Width;
                cbAnnotations.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move components to other side of Viewer tab
                cbPrintDialog.Left = gbViewerPrfs.Width - cbPrintDialog.Left - cbPrintDialog.Width;
                cbPrintDialog.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbHideToolbar.Left = gbViewerPrfs.Width - cbHideToolbar.Left - cbHideToolbar.Width;
                cbHideToolbar.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbHideMenubar.Left = gbViewerPrfs.Width - cbHideMenubar.Left - cbHideMenubar.Width;
                cbHideMenubar.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbHideUI.Left = gbViewerPrfs.Width - cbHideUI.Left - cbHideUI.Width;
                cbHideUI.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbFitWindow.Left = gbViewerPrfs.Width - cbFitWindow.Left - cbFitWindow.Width;
                cbFitWindow.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbCenterWindow.Left = gbViewerPrfs.Width - cbCenterWindow.Left - cbCenterWindow.Width;
                cbCenterWindow.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPrintScaling.Left = gbViewerPrfs.Width - cbPrintScaling.Left - cbPrintScaling.Width;
                cbPrintScaling.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbOutline.Left = gbViewerPrfs.Width - cbOutline.Left - cbOutline.Width;
                cbOutline.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblInitialZoom.Left = gbViewerPrfs.Width - lblInitialZoom.Left - lblInitialZoom.Width;
                lblInitialZoom.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        #endregion

        #region Events

        private void optionChanged(object sender, EventArgs e)
        {
            bool noPdfStandard = false;

            switch (cbPdfStandard.SelectedIndex)
            {
                case 1: // PDF/A-2a
                case 2: // PDF/A-2b
                case 3: // PDF/A-3a
                case 4: // PDF/A-3b
                    pageControlSecurity.Enabled = false;
                    tbUserPassword.Text = "";
                    tbOwnerPassword.Text = "";
                    cbEmbeddedFonts.Checked = true;
                    cbEmbeddedFonts.Enabled = false;
                    cbTextInCurves.Checked = false;
                    cbTextInCurves.Enabled = false;
                    break;
                case 5: // PDF/X-3
                case 6: // PDF/X-4
                    pageControlSecurity.Enabled = false;
                    tbUserPassword.Text = "";
                    tbOwnerPassword.Text = "";
                    cbEmbeddedFonts.Checked = true;
                    cbEmbeddedFonts.Enabled = false;
                    cbTextInCurves.Enabled = true;
                    break;
                case 0: // PDF 1.5
                default:
                    pageControlSecurity.Enabled = true;
                    cbEmbeddedFonts.Enabled = true;
                    cbTextInCurves.Enabled = true;
                    noPdfStandard = true;
                    break;
            }

            if (noPdfStandard)
            {
                // cbEmbeddedFonts
                cbEmbeddedFonts.Enabled = !cbTextInCurves.Checked;
                if (!cbEmbeddedFonts.Enabled)
                    cbEmbeddedFonts.Checked = false;
                // end

                // cbTextInCurves
                cbTextInCurves.Enabled = !cbEmbeddedFonts.Checked;
                if (!cbTextInCurves.Enabled)
                    cbTextInCurves.Checked = false;
                // end
            }

            // cbPrintOptimized
            cbPrintOptimized.Enabled = !cbOriginalResolution.Checked;
            if (!cbPrintOptimized.Enabled)
                cbPrintOptimized.Checked = false;
            // end

            // cbJpegCompression
            if (cbColorSpace.SelectedIndex == (int)PDFExport.PdfColorSpace.CMYK || cbOriginalResolution.Checked)
                cbJpegCompression.Enabled = false;
            else
                cbJpegCompression.Enabled = true;
            if (!cbJpegCompression.Enabled)
                cbJpegCompression.Checked = false;
            // end

            // lblJpegQuality
            lblJpegQuality.Enabled = cbJpegCompression.Checked;
            // end

            // nudJpegQuality
            nudJpegQuality.Enabled = cbJpegCompression.Checked;
            // end
        }

        #endregion
    }
}
