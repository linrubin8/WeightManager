using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.XAML;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="XAMLExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class XAMLExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            XAMLExport xamlExport = Export as XAMLExport;
            comboBox1.SelectedIndex = (int)xamlExport.ImageFormat;
            cbToMultipleFiles.Checked = xamlExport.HasMultipleFiles;
            cbScroll.Checked = xamlExport.IsScrolled;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            XAMLExport xamlExport = Export as XAMLExport;
            xamlExport.ImageFormat = (XamlImageFormat)comboBox1.SelectedIndex;
            xamlExport.HasMultipleFiles = cbToMultipleFiles.Checked;
            xamlExport.IsScrolled = cbScroll.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Xaml");
            Text = res.Get("");
            cbScroll.Text = res.Get("IsScrolled");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            lblImageFormat.Text = res.Get("Pictures");
            cbToMultipleFiles.Text = res.Get("ToMultipleFiles");           
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XAMLExportForm"/> class.
        /// </summary>
        public XAMLExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                lblImageFormat.Left = gbOptions.Width - lblImageFormat.Left - lblImageFormat.Width;
                lblImageFormat.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                comboBox1.Left = gbOptions.Width - comboBox1.Left - comboBox1.Width;
                comboBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbToMultipleFiles.Left = ClientSize.Width - cbToMultipleFiles.Left - cbToMultipleFiles.Width;
                cbToMultipleFiles.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbScroll.Left = ClientSize.Width - cbScroll.Left - cbScroll.Width;
                cbScroll.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

