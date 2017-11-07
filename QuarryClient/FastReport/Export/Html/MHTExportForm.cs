using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Mht;
using FastReport.Utils;


namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="MHTExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class MHTExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            MHTExport MHTExport = Export as MHTExport;
            cbWysiwyg.Checked = MHTExport.Wysiwyg;
            cbPictures.Checked = MHTExport.Pictures;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            MHTExport MHTExport = Export as MHTExport;
            MHTExport.Wysiwyg = cbWysiwyg.Checked;
            MHTExport.Pictures = cbPictures.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Mht");
            Text = res.Get("");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPictures.Text = res.Get("Pictures");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MHTExportForm"/> class.
        /// </summary>
        public MHTExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components from left to right
                cbWysiwyg.Left = gbOptions.Width - cbWysiwyg.Left - cbWysiwyg.Width;
                cbWysiwyg.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPictures.Left = gbOptions.Width - cbPictures.Left - cbPictures.Width;
                cbPictures.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

