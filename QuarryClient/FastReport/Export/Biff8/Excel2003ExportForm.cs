using System.Windows.Forms;
using FastReport.Export;
using FastReport.Utils;
using FastReport.Export.BIFF8;


namespace FastReport.Forms
{
    internal partial class Excel2003ExportForm : BaseExportForm
    {
        public override void Init(ExportBase export)
        {
            base.Init(export);
            MyRes res = new MyRes("Export,Xls");
            Text = res.Get("");
            Excel2003Document Biff8Export = Export as Excel2003Document;
            cbWysiwyg.Checked = Biff8Export.Wysiwyg;
            cbPageBreaks.Checked = Biff8Export.PageBreaks;
        }
        
        protected override void Done()
        {
            base.Done();
            Excel2003Document Biff8Export = Export as Excel2003Document;
            Biff8Export.Wysiwyg = cbWysiwyg.Checked;
            Biff8Export.PageBreaks = cbPageBreaks.Checked;
        }
        
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPageBreaks.Text = res.Get("PageBreaks");
        }        
        
        public Excel2003ExportForm()
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

