using System.Text;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Csv;
using FastReport.Utils;
using System.Globalization;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="CSVExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class CsvExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            CSVExport csvExport = Export as CSVExport;
            tbSeparator.Text = csvExport.Separator;
            if (csvExport.Encoding == Encoding.Default)
                cbbCodepage.SelectedIndex = 0;
            else if (csvExport.Encoding == Encoding.UTF8)
                cbbCodepage.SelectedIndex = 1;
            else if (csvExport.Encoding == Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage))
                cbbCodepage.SelectedIndex = 2;
            cbDataOnly.Checked = csvExport.DataOnly;
            cbNoQuotes.Checked = csvExport.NoQuotes;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            CSVExport csvExport = Export as CSVExport;
            csvExport.Separator = tbSeparator.Text;
            if (cbbCodepage.SelectedIndex == 0)
                csvExport.Encoding = Encoding.Default;
            else if (cbbCodepage.SelectedIndex == 1)
                csvExport.Encoding = Encoding.UTF8;
            else if (cbbCodepage.SelectedIndex == 2)
                csvExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            csvExport.DataOnly = cbDataOnly.Checked;
            csvExport.NoQuotes = cbNoQuotes.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Csv");
            Text = res.Get("");
            lblSeparator.Text = res.Get("Separator");
            lblCodepage.Text = res.Get("Codepage");                        
            cbbCodepage.Items[0] = res.Get("Default");
            cbbCodepage.Items[1] = res.Get("Unicode");
            cbbCodepage.Items[2] = res.Get("OEM");
            cbDataOnly.Text = res.Get("DataOnly");
            res = new MyRes("Export,Misc");            
            gbOptions.Text = res.Get("Options");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvExportForm"/> class.
        /// </summary>
        public CsvExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                lblSeparator.Left = gbOptions.Width - lblSeparator.Left - lblSeparator.Width;
                lblSeparator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbSeparator.Left = gbOptions.Width - tbSeparator.Left - tbSeparator.Width;
                tbSeparator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblCodepage.Left = gbOptions.Width - lblCodepage.Left - lblCodepage.Width;
                lblCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbbCodepage.Left = gbOptions.Width - cbbCodepage.Left - cbbCodepage.Width;
                cbbCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbDataOnly.Left = gbOptions.Width - cbDataOnly.Left - cbDataOnly.Width;
                cbDataOnly.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbNoQuotes.Left = gbOptions.Width - cbNoQuotes.Left - cbNoQuotes.Width;
                cbNoQuotes.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }
    }
}

