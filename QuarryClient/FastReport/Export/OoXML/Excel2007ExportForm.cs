using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.OoXML;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="Excel2007Export"/>.
    /// For internal use only.
    /// </summary>
    public partial class Excel2007ExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            MyRes res = new MyRes("Export,Xlsx");
            Text = res.Get("");
            Excel2007Export ooxmlExport = Export as Excel2007Export;
            cbWysiwyg.Checked = ooxmlExport.Wysiwyg;
            cbPageBreaks.Checked = ooxmlExport.PageBreaks;
            cbDataOnly.Checked = ooxmlExport.DataOnly;
            cbSeamless.Checked = ooxmlExport.Seamless;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            Excel2007Export ooxmlExport = Export as Excel2007Export;
            ooxmlExport.Wysiwyg = cbWysiwyg.Checked;
            ooxmlExport.PageBreaks = cbPageBreaks.Checked;
            ooxmlExport.DataOnly = cbDataOnly.Checked;
            ooxmlExport.Seamless = cbSeamless.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPageBreaks.Text = res.Get("PageBreaks");
            cbSeamless.Text = res.Get("Seamless");
            cbDataOnly.Text = Res.Get("Export,Csv,DataOnly");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Excel2007ExportForm"/> class.
        /// </summary>
        public Excel2007ExportForm()
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
                cbSeamless.Left = gbOptions.Width - cbSeamless.Left - cbSeamless.Width;
                cbSeamless.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

