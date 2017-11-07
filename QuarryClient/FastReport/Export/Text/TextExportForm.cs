using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Text;
using FastReport.Utils;
using System.Globalization;
using System.IO;


namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="TextExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class TextExportForm : BaseExportForm
    {
        private TextExport previewExport;
        private Report report;
        private int prevPage;
    
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            TextExport textExport = Export as TextExport;
            report = textExport.Report;

            ProfessionalColorTable vs2005ColorTable = new ProfessionalColorTable();
            vs2005ColorTable.UseSystemColors = true;
            toolStrip.Renderer = new ToolStripProfessionalRenderer(vs2005ColorTable);

            cbPageBreaks.Checked = textExport.PageBreaks;
            cbEmptyLines.Checked = textExport.EmptyLines;
            if (textExport.Frames && textExport.TextFrames)
                cbbFrames.SelectedIndex = 1;
            else if (textExport.Frames && !textExport.TextFrames)
                cbbFrames.SelectedIndex = 2;
            else
                cbbFrames.SelectedIndex = 0;            
            cbDataOnly.Checked = textExport.DataOnly;
            if (textExport.Encoding == Encoding.Default)
                cbbCodepage.SelectedIndex = 0;
            else if (textExport.Encoding == Encoding.UTF8)
                cbbCodepage.SelectedIndex = 1;
            else if (textExport.Encoding == Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage))
                cbbCodepage.SelectedIndex = 2;
            udX.Value = (decimal)textExport.ScaleX;
            udY.Value = (decimal)textExport.ScaleY;

            udX.ValueChanged +=new EventHandler(udX_ValueChanged);
            udY.ValueChanged +=new EventHandler(udX_ValueChanged);
            cbbFrames.SelectedIndexChanged +=new EventHandler(cbbFrames_SelectedIndexChanged);

            MyRes res = new MyRes("Preview");
            tbPage.Text = "1";
            prevPage = 1;
            lblTotalPages.Text = String.Format(Res.Get("Misc,ofM"), report.PreparedPages.Count);

            cbFontSize.SelectedIndex = 4;
            
            previewExport = new TextExport();
            previewExport.PreviewMode = true;
            CalcScale();
        }
    
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Text");
            Text = res.Get(""); 
            lblFrames.Text = res.Get("Frames");
            cbEmptyLines.Text = res.Get("EmptyLines");
            cbDataOnly.Text = res.Get("DataOnly");
            cbbFrames.Items[0] = res.Get("FramesNone");
            cbbFrames.Items[1] = res.Get("FramesText");
            cbbFrames.Items[2] = res.Get("FramesGraphic");
            lblCodepage.Text = res.Get("Codepage");
            cbbCodepage.Items[0] = res.Get("Default");
            cbbCodepage.Items[1] = res.Get("Unicode");
            cbbCodepage.Items[2] = res.Get("OEM");
            lblX.Text = res.Get("ScaleX");
            lblY.Text = res.Get("ScaleY");
            btnCalculate.Text = res.Get("AutoScale");
            lblLoss.Text = res.Get("DataLoss");
            lblPageWidth.Text = res.Get("PageWidth");
            lblPageHeight.Text = res.Get("PageHeight");
            gbScale.Text = res.Get("Scale");
            
            res = new MyRes("Export,Misc");            
            gbOptions.Text = res.Get("Options");
            cbPageBreaks.Text = res.Get("PageBreaks");

            res = new MyRes("Preview");
            btnPrint.Image = Res.GetImage(195);
            btnPrint.Text = res.Get("PrintText");
            btnPrint.ToolTipText = res.Get("Print");
            btnSave.ToolTipText = res.Get("Save");
            btnSave.Text = res.Get("SaveText");
            btnZoomOut.ToolTipText = Res.Get("Designer,Toolbar,Zoom,ZoomOut");
            btnZoomIn.ToolTipText = Res.Get("Designer,Toolbar,Zoom,ZoomIn");
            btnFirst.ToolTipText = res.Get("First");
            btnPrior.ToolTipText = res.Get("Prior");
            lblTotalPages.Text = String.Format(Res.Get("Misc,ofM"), 1);
            btnNext.ToolTipText = res.Get("Next");
            btnLast.ToolTipText = res.Get("Last");
            btnClose.Text = Res.Get("Buttons,Close");
            btnPrint.Image = Res.GetImage(195);
            btnSave.Image = Res.GetImage(2);
            btnZoomOut.Image = Res.GetImage(193);
            btnZoomIn.Image = Res.GetImage(192);
            btnFirst.Image = Res.GetImage(185);
            btnPrior.Image = Res.GetImage(186);
            btnNext.Image = Res.GetImage(187);
            btnLast.Image = Res.GetImage(188);
        }

        private void CalcScale()
        {
            TextExport textExport = Export as TextExport;
            using (ProgressForm progressForm = new ProgressForm(null))
            {
                progressForm.Show();
                MyRes res = new MyRes("Export,Text");
                progressForm.ShowProgressMessage(res.Get("ScaleMessage"));
                textExport.EmptyLines = cbEmptyLines.Checked;
                textExport.Frames = cbbFrames.SelectedIndex != 0;
                textExport.TextFrames = cbbFrames.SelectedIndex == 1;
                textExport.DataOnly = cbDataOnly.Checked;
                textExport.ScaleX = (float)udX.Value;
                textExport.ScaleY = (float)udY.Value;
                textExport.CalculateScale(progressForm);
                udX.Value = (decimal)Math.Round(textExport.ScaleX, 2);
                udY.Value = (decimal)Math.Round(textExport.ScaleY, 2);
            }
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (previewExport != null)
            {
                TextExport textExport = Export as TextExport;                
                int pageNo = int.Parse(tbPage.Text) - 1;
                using (ReportPage page = textExport.Report.PreparedPages.GetPage(pageNo))
                {
                    previewExport.PageBreaks = cbPageBreaks.Checked;
                    previewExport.DataOnly = cbDataOnly.Checked;
                    previewExport.Frames = cbbFrames.SelectedIndex != 0;
                    previewExport.TextFrames = cbbFrames.SelectedIndex == 1;
                    previewExport.EmptyLines = cbEmptyLines.Checked;
                    if (cbbCodepage.SelectedIndex == 0)
                        previewExport.Encoding = Encoding.Default;
                    else if (cbbCodepage.SelectedIndex == 1)
                        previewExport.Encoding = Encoding.UTF8;
                    else if (cbbCodepage.SelectedIndex == 2)
                        previewExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
                    previewExport.ScaleX = (float)udX.Value;
                    previewExport.ScaleY = (float)udY.Value;
                    previewExport.SetReport(textExport.Report);
                    tbPreview.Text = previewExport.ExportPage(pageNo);
                    lblPageWidthValue.Text = previewExport.PageWidth.ToString();
                    lblPageHeightValue.Text = previewExport.PageHeight.ToString();
                    lblLoss.Visible = !previewExport.DataSaved;
                }
            }
        }

        private void cbPageBreaks_Click(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void cbbCodepage_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalcScale();
        }

        private void tbPage_TextChanged(object sender, EventArgs e)
        {
            bool validate = false;
            try
            {
                int i = int.Parse(tbPage.Text);
                validate = (i > 0 && i <= report.PreparedPages.Count);
                UpdatePreview();
                btnFirst.Enabled = i > 1;
                btnPrior.Enabled = btnFirst.Enabled;
                btnNext.Enabled = i < report.PreparedPages.Count;
                btnLast.Enabled = btnNext.Enabled;
            }
            catch
            {
            }
            if (!validate)
                tbPage.Text = prevPage.ToString();
            else
                prevPage = int.Parse(tbPage.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            tbPage.Text = "1";
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            int i = int.Parse(tbPage.Text);
            if (i > 1)
                tbPage.Text = (i - 1).ToString();            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = int.Parse(tbPage.Text);
            if (i < report.PreparedPages.Count)
                tbPage.Text = (i + 1).ToString();            
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbPage.Text = report.PreparedPages.Count.ToString();
        }

        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPreview.Font = new Font(tbPreview.Font.FontFamily, int.Parse(cbFontSize.Text));
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (cbFontSize.SelectedIndex > 0)
                cbFontSize.SelectedIndex--;
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (cbFontSize.SelectedIndex < cbFontSize.Items.Count - 1)
                cbFontSize.SelectedIndex++;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextExport parentExport = Export as TextExport;            
            TextExport textExport = new TextExport();            
            textExport.PageBreaks = cbPageBreaks.Checked;
            textExport.EmptyLines = cbEmptyLines.Checked;
            textExport.Frames = cbbFrames.SelectedIndex != 0;
            textExport.TextFrames = cbbFrames.SelectedIndex == 1;
            textExport.DataOnly = cbDataOnly.Checked;
            if (cbbCodepage.SelectedIndex == 0)
                textExport.Encoding = Encoding.Default;
            else if (cbbCodepage.SelectedIndex == 1)
                textExport.Encoding = Encoding.UTF8;
            else if (cbbCodepage.SelectedIndex == 2)
                textExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            textExport.ScaleX = (float)udX.Value;
            textExport.ScaleY = (float)udY.Value;
            textExport.OpenAfterExport = false;
            textExport.AvoidDataLoss = false;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = Path.GetFileNameWithoutExtension(Path.GetFileName(report.FileName));
                dialog.Filter = textExport.FileFilter;
                string defaultExt = dialog.Filter.Split(new char[] { '|' })[1];
                dialog.DefaultExt = Path.GetExtension(defaultExt);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();
                    textExport.Export(report, dialog.FileName);
                }
            }
        }

        private void udX_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void udY_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void cbbFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcScale();
        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            CalcScale();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            TextExport parentExport = Export as TextExport;
            TextExport textExport = new TextExport();
            using(TextExportPrintForm printDialog = new TextExportPrintForm(textExport))
            {
                textExport.PrinterTypes = parentExport.PrinterTypes;
                textExport.Copies = parentExport.Copies;
                printDialog.CurrentPage = tbPage.Text;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (cbbCodepage.SelectedIndex == 1)
                        cbbCodepage.SelectedIndex = 2;
                    textExport.PageBreaks = cbPageBreaks.Checked;
                    textExport.EmptyLines = cbEmptyLines.Checked;
                    textExport.Frames = cbbFrames.SelectedIndex != 0;
                    textExport.TextFrames = cbbFrames.SelectedIndex == 1;
                    textExport.DataOnly = cbDataOnly.Checked;
                    if (cbbCodepage.SelectedIndex == 0)
                        textExport.Encoding = Encoding.Default;
                    else if (cbbCodepage.SelectedIndex == 2)
                        textExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
                    textExport.ScaleX = (float)udX.Value;
                    textExport.ScaleY = (float)udY.Value;
                    textExport.PrintAfterExport = true;
                    textExport.OpenAfterExport = false;
                    textExport.AvoidDataLoss = false;
                    using (MemoryStream memStream = new MemoryStream())
                        textExport.Export(report, memStream);
                }
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TextExportForm"/> class.
        /// </summary>
        public TextExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            //if (Config.RightToLeft)
            //{
            //    RightToLeft = RightToLeft.Yes;

            //    // move components to other side
            //    panPages.Left = 1700/*ClientSize.Width - panPages.Left - panPages.Width*/;
            //    panPages.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //    pcPages.Left = ClientSize.Width - pcPages.Left - pcPages.Width;
            //    pcPages.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //    gbOptions.Left = 1000/*ClientSize.Width - gbOptions.Left - gbOptions.Width*/;
            //    gbOptions.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //    cbPageBreaks.Left = gbOptions.Width - cbPageBreaks.Left - cbPageBreaks.Width;
            //    cbPageBreaks.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //    cbEmptyLines.Left = gbOptions.Width - cbEmptyLines.Left - cbEmptyLines.Width;
            //    cbEmptyLines.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //    cbDataOnly.Left = gbOptions.Width - cbDataOnly.Left - cbDataOnly.Width;
            //    cbDataOnly.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //    gbScale.Left = ClientSize.Width - gbScale.Left - gbScale.Width;
            //    gbScale.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //    picPerforation.Left = ClientSize.Width - picPerforation.Left - picPerforation.Width;
            //    picPerforation.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //    tbPreview.Left = ClientSize.Width - tbPreview.Left - tbPreview.Width;
            //    //tbPreview.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //    // move parent components from left to right
            //    cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
            //    cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //    // move parent components from rigth to left
            //    btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
            //    btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            //}
        }

        private void TextExportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
            else if (e.KeyCode == Keys.PageDown)
                btnNext_Click(null, null);
            else if (e.KeyCode == Keys.PageUp)
                btnPrior_Click(null, null);
        }

        private void TextExportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextExport textExport = Export as TextExport;
            textExport.PageBreaks = cbPageBreaks.Checked;
            textExport.EmptyLines = cbEmptyLines.Checked;
            textExport.Frames = cbbFrames.SelectedIndex != 0;
            textExport.TextFrames = cbbFrames.SelectedIndex == 1;
            textExport.DataOnly = cbDataOnly.Checked;
            if (cbbCodepage.SelectedIndex == 0)
                textExport.Encoding = Encoding.Default;
            else if (cbbCodepage.SelectedIndex == 1)
                textExport.Encoding = Encoding.UTF8;
            else if (cbbCodepage.SelectedIndex == 2)
                textExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            textExport.ScaleX = (float)udX.Value;
            textExport.ScaleY = (float)udY.Value;
        }
    }
}
