using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.PS;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="PSExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class PSExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            PSExport PSExport = Export as PSExport;
            comboBox1.SelectedIndex = (int)PSExport.ImageFormat;
            chCurves.Checked = PSExport.TextInCurves;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            PSExport PSExport = Export as PSExport;
            PSExport.ImageFormat = (PSImageFormat)comboBox1.SelectedIndex;
            PSExport.TextInCurves = chCurves.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,PS");
            Text = res.Get("");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            lblImageFormat.Text = res.Get("Pictures");
            res = new MyRes("Export,Pdf");
            chCurves.Text = res.Get("TextInCurves");
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PSExportForm"/> class.
        /// </summary>
        public PSExportForm()
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

