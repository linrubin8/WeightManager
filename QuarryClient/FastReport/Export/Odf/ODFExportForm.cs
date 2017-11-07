using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Odf;
using FastReport.Utils;


namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="ODFExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class ODFExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            MyRes res = new MyRes("Export," + ((export is ODSExport) ? "Ods" : "Odt"));
            Text = res.Get("");
            ODFExport odfExport = Export as ODFExport;
            cbWysiwyg.Checked = odfExport.Wysiwyg;
            cbPageBreaks.Checked = odfExport.PageBreaks;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            ODFExport odfExport = Export as ODFExport;
            odfExport.Wysiwyg = cbWysiwyg.Checked;
            odfExport.PageBreaks = cbPageBreaks.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPageBreaks.Text = res.Get("PageBreaks");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ODFExportForm"/> class.
        /// </summary>
        public ODFExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                cbWysiwyg.Left = gbOptions.Width - cbWysiwyg.Left - cbWysiwyg.Width;
                cbWysiwyg.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPageBreaks.Left = gbOptions.Width - cbPageBreaks.Left - cbPageBreaks.Width;
                cbPageBreaks.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

