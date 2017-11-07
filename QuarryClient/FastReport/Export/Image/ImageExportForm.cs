using System;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;

namespace FastReport.Export.Image
{
    /// <summary>
    /// Form for <see cref="ImageExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class ImageExportForm : BaseExportForm
  {
    private void cbxImageFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      int index = cbxImageFormat.SelectedIndex;
      bool isJpeg = index == 2;
      bool isTiff = index == 4;

      lblQuality.Enabled = isJpeg;
      udQuality.Enabled = isJpeg;
      cbMultiFrameTiff.Enabled = isTiff;
      cbMonochrome.Enabled = isTiff;
      lblX.Visible = isTiff;
      udResolutionY.Visible = isTiff;
      cbMultiFrameTiff_CheckedChanged(null, EventArgs.Empty);
    }

    private void cbMultiFrameTiff_CheckedChanged(object sender, EventArgs e)
    {
      cbSeparateFiles.Enabled = !cbMultiFrameTiff.Checked || !cbMultiFrameTiff.Enabled;
    }

    /// <inheritdoc/>
    public override void Init(ExportBase export)
    {
      base.Init(export);
      ImageExport imageExport = Export as ImageExport;
      cbxImageFormat.SelectedIndex = (int)imageExport.ImageFormat;
      udResolution.Value = imageExport.ResolutionX;
      udResolutionY.Value = imageExport.ResolutionY;
      udQuality.Value = imageExport.JpegQuality;
      cbSeparateFiles.Checked = imageExport.SeparateFiles;
      cbMultiFrameTiff.Checked = imageExport.MultiFrameTiff;
      cbMonochrome.Checked = imageExport.MonochromeTiff;
      cbMultiFrameTiff_CheckedChanged(null, EventArgs.Empty);
    }

    /// <inheritdoc/>
    protected override void Done()
    {
      base.Done();
      ImageExport imageExport = Export as ImageExport;
      imageExport.ImageFormat = (ImageExportFormat)cbxImageFormat.SelectedIndex;
      if (imageExport.ImageFormat == ImageExportFormat.Tiff)
      {
        imageExport.ResolutionX = (int)udResolution.Value;
        imageExport.ResolutionY = (int)udResolutionY.Value;
      }
      else
      {
        imageExport.Resolution = (int)udResolution.Value;
      }
      imageExport.JpegQuality = (int)udQuality.Value;
      imageExport.SeparateFiles = cbSeparateFiles.Checked;
      imageExport.MultiFrameTiff = cbMultiFrameTiff.Checked;
      imageExport.MonochromeTiff = cbMonochrome.Checked;
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Export,Image");
      Text = res.Get("");
      gbOptions.Text = Res.Get("Export,Misc,Options");
      lblImageFormat.Text = res.Get("ImageFormat");
      lblResolution.Text = res.Get("Resolution");
      lblQuality.Text = res.Get("Quality");
      cbSeparateFiles.Text = res.Get("SeparateFiles");
      cbMultiFrameTiff.Text = res.Get("MultiFrame");
      cbMonochrome.Text = res.Get("Monochrome");
      cbxImageFormat.Items.AddRange(new string[] {
        res.Get("Bmp"), res.Get("Png"), res.Get("Jpeg"), res.Get("Gif"), res.Get("Tiff"), res.Get("Metafile") });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageExportForm"/> class.
    /// </summary>
    public ImageExportForm()
    {
        InitializeComponent();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            lblImageFormat.Left = gbOptions.Width - lblImageFormat.Left - lblImageFormat.Width;
            lblImageFormat.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbxImageFormat.Left = gbOptions.Width - cbxImageFormat.Left - cbxImageFormat.Width;
            cbxImageFormat.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblResolution.Left = gbOptions.Width - lblResolution.Left - lblResolution.Width;
            lblResolution.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            udResolution.Left = gbOptions.Width - udResolution.Left - udResolution.Width;
            udResolution.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            udResolutionY.Left = gbOptions.Width - udResolutionY.Left - udResolutionY.Width;
            udResolutionY.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblQuality.Left = gbOptions.Width - lblQuality.Left - lblQuality.Width;
            lblQuality.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            udQuality.Left = gbOptions.Width - udQuality.Left - udQuality.Width;
            udQuality.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbSeparateFiles.Left = gbOptions.Width - cbSeparateFiles.Left - cbSeparateFiles.Width;
            cbSeparateFiles.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbMultiFrameTiff.Left = gbOptions.Width - cbMultiFrameTiff.Left - cbMultiFrameTiff.Width;
            cbMultiFrameTiff.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbMonochrome.Left = gbOptions.Width - cbMonochrome.Left - cbMonochrome.Width;
            cbMonochrome.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

