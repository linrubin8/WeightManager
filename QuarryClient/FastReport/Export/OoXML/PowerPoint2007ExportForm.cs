using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.OoXML;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="PowerPoint2007Export"/>.
    /// For internal use only.
    /// </summary>
    public partial class PowerPoint2007ExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            PowerPoint2007Export pptExport = Export as PowerPoint2007Export;
            comboBox1.SelectedIndex = (int)pptExport.ImageFormat;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            PowerPoint2007Export pptExport = Export as PowerPoint2007Export;
            pptExport.ImageFormat = (PptImageFormat)comboBox1.SelectedIndex;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Pptx");
            Text = res.Get("");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            lblImageFormat.Text = res.Get("Pictures");
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerPoint2007ExportForm"/> class.
        /// </summary>
        public PowerPoint2007ExportForm()
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

