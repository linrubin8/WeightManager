using System;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Html;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="HTMLExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class HTMLExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            HTMLExport htmlExport = Export as HTMLExport;
            cbWysiwyg.Checked = htmlExport.Wysiwyg;
            cbPictures.Checked = htmlExport.Pictures;
            cbSinglePage.Checked = htmlExport.SinglePage;
            cbSubFolder.Checked = htmlExport.SubFolder;
            cbNavigator.Checked = htmlExport.Navigator;
            cbLayers.Checked = htmlExport.Layers;
            cbEmbPic.Checked = htmlExport.EmbedPictures;
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            HTMLExport htmlExport = Export as HTMLExport;
            htmlExport.Layers = cbLayers.Checked;
            htmlExport.Wysiwyg = cbWysiwyg.Checked;
            htmlExport.Pictures = cbPictures.Checked;
            htmlExport.SinglePage = cbSinglePage.Checked;
            htmlExport.SubFolder = cbSubFolder.Checked;
            htmlExport.Navigator = cbNavigator.Checked;
            htmlExport.EmbedPictures = cbEmbPic.Checked;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Html");
            Text = res.Get("");
            cbLayers.Text = res.Get("Layers");
            cbSinglePage.Text = res.Get("SinglePage");
            cbSubFolder.Text = res.Get("SubFolder");
            cbNavigator.Text = res.Get("Navigator");
            cbEmbPic.Text = res.Get("EmbPic");
            res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            cbPictures.Text = res.Get("Pictures");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTMLExportForm"/> class.
        /// </summary>
        public HTMLExportForm()
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
                cbSubFolder.Left = gbOptions.Width - cbSubFolder.Left - cbSubFolder.Width;
                cbSubFolder.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbNavigator.Left = gbOptions.Width - cbNavigator.Left - cbNavigator.Width;
                cbNavigator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbSinglePage.Left = gbOptions.Width - cbSinglePage.Left - cbSinglePage.Width;
                cbSinglePage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbLayers.Left = gbOptions.Width - cbLayers.Left - cbLayers.Width;
                cbLayers.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbEmbPic.Left = gbOptions.Width - cbEmbPic.Left - cbEmbPic.Width;
                cbEmbPic.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        private void cbPictures_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPictures.Checked)
                cbEmbPic.Visible = true;
            else cbEmbPic.Visible = false;
        }
    }
}

