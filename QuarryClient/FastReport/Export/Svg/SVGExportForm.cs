using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Svg;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="SVGExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class SVGExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            SVGExport SVGExport = Export as SVGExport;
            comboBox1.SelectedIndex = (int)SVGExport.ImageFormat;
            cbEmbdImgs.Checked = SVGExport.EmbedPictures;
            cbToMultipleFiles.Checked = SVGExport.HasMultipleFiles;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            SVGExport SVGExport = Export as SVGExport;
            SVGExport.ImageFormat = (SVGImageFormat)comboBox1.SelectedIndex;
            SVGExport.EmbedPictures = cbEmbdImgs.Checked;
            SVGExport.HasMultipleFiles = cbToMultipleFiles.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,SVG");
            Text = res.Get("");
            cbEmbdImgs.Text = res.Get("EmbPic");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            lblImageFormat.Text = res.Get("Pictures");
            cbToMultipleFiles.Text = res.Get("ToMultipleFiles");
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SVGExportForm"/> class.
        /// </summary>
        public SVGExportForm()
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

