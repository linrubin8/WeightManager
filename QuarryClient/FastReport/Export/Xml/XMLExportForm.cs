using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Xml;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="XMLExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class XMLExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            XMLExport xmlExport = Export as XMLExport;
            cbWysiwyg.Checked = xmlExport.Wysiwyg;
            cbPageBreaks.Checked = xmlExport.PageBreaks;
            cbDataOnly.Checked = xmlExport.DataOnly;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            XMLExport xmlExport = Export as XMLExport;
            xmlExport.Wysiwyg = cbWysiwyg.Checked;
            xmlExport.PageBreaks = cbPageBreaks.Checked;
            xmlExport.DataOnly = cbDataOnly.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Xml");
            Text = res.Get("");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPageBreaks.Text = res.Get("PageBreaks");
            cbDataOnly.Text = Res.Get("Export,Csv,DataOnly");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLExportForm"/> class.
        /// </summary>
        public XMLExportForm()
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
                cbDataOnly.Left = gbOptions.Width - cbDataOnly.Left - cbDataOnly.Width;
                cbDataOnly.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

