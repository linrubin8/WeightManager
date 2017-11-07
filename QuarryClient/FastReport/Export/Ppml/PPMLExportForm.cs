using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Ppml;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="PPMLExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class PPMLExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            PPMLExport PPMLExport = Export as PPMLExport;
            comboBox1.SelectedIndex = (int)PPMLExport.ImageFormat;
            chCurves.Checked = PPMLExport.TextInCurves;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            PPMLExport PPMLExport = Export as PPMLExport;
            PPMLExport.ImageFormat = (PPMLImageFormat)comboBox1.SelectedIndex;
            PPMLExport.TextInCurves = chCurves.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,PPML");
            Text = res.Get("");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            lblImageFormat.Text = res.Get("Pictures");
            res = new MyRes("Export,Pdf");
            chCurves.Text = res.Get("TextInCurves");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PPMLExportForm"/> class.
        /// </summary>
        public PPMLExportForm()
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
                chCurves.Left = gbOptions.Width - chCurves.Left - chCurves.Width;
                chCurves.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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
