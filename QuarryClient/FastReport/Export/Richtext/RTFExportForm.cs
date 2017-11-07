using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.RichText;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="RTFExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class RTFExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            RTFExport rtfExport = Export as RTFExport;
            cbWysiwyg.Checked = rtfExport.Wysiwyg;
            cbPageBreaks.Checked = rtfExport.PageBreaks;
            if (rtfExport.Pictures)
                cbbPictures.SelectedIndex = rtfExport.ImageFormat == RTFImageFormat.Metafile ? 1 : (rtfExport.ImageFormat == RTFImageFormat.Jpeg ? 2 : 3);
            else 
                cbbPictures.SelectedIndex = 0;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            RTFExport rtfExport = Export as RTFExport;
            rtfExport.Wysiwyg = cbWysiwyg.Checked;
            rtfExport.PageBreaks = cbPageBreaks.Checked;
            rtfExport.Pictures = cbbPictures.SelectedIndex > 0;
            if (cbbPictures.SelectedIndex == 1)
                rtfExport.ImageFormat = RTFImageFormat.Metafile;
            else if (cbbPictures.SelectedIndex == 2)
                rtfExport.ImageFormat = RTFImageFormat.Jpeg;
            else
                rtfExport.ImageFormat = RTFImageFormat.Png;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,RichText");
            Text = res.Get("");
            res = new MyRes("Export,Misc");            
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPageBreaks.Text = res.Get("PageBreaks");
            lblPictures.Text = res.Get("Pictures");
            cbbPictures.Items[0] = res.Get("None");
            cbbPictures.Items[1] = res.Get("Metafile");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RTFExportForm"/> class.
        /// </summary>
        public RTFExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components from left to right
                cbWysiwyg.Left = gbOptions.Width - cbWysiwyg.Left - cbWysiwyg.Width;
                cbWysiwyg.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbPageBreaks.Left = gbOptions.Width - cbPageBreaks.Left - cbPageBreaks.Width;
                cbPageBreaks.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblPictures.Left = gbOptions.Width - lblPictures.Left - lblPictures.Width;
                lblPictures.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                
                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move components from right to left
                cbbPictures.Left = gbOptions.Width - cbbPictures.Left - cbbPictures.Width;
                cbbPictures.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                
                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }
    }
}

