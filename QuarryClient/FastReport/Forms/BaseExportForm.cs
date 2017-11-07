using System;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Export;

namespace FastReport.Forms
{
  /// <summary>
  /// Base form for all export options dialog forms.
  /// </summary>
  public partial class BaseExportForm : BaseDialogForm
  {
    /// <summary>
    /// Represents the "Open after export" button visibility.
    /// </summary>
    public bool OpenAfterVisible
    {
      get
      {
        return cbOpenAfter.Visible;
      }
      set
      {
        cbOpenAfter.Visible = value;
      }
    }

    private ExportBase FExport;
    
    /// <summary>
    /// Gets a reference to the currently editing export filter.
    /// </summary>
    protected ExportBase Export
    {
      get { return FExport; }
    }
    
    private void tbNumbers_KeyPress(object sender, KeyPressEventArgs e)
    {
      rbNumbers.Checked = true;
    }

    private void rbCurrent_CheckedChanged(object sender, EventArgs e)
    {
      if ((sender as RadioButton).Checked)
        tbNumbers.Text = "";
    }

    /// <inheritdoc/>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);
      if (DialogResult == DialogResult.OK)
      {
        string s = tbNumbers.Text;
        foreach (char c in s)
        {
          if (!(c == ' ' || c == ',' || c == '-' || (c >= '0' && c <= '9')))
          {
            FRMessageBox.Error(Res.Get("Forms,PrinterSetup,Error") + "\r\n" +
              Res.Get("Forms,PrinterSetup,Hint"));
            tbNumbers.Focus();
            e.Cancel = true;
            break;
          }
        }
      }
    }

    /// <inheritdoc/>
    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      // needed for 120dpi mode
      lblHint.Width = tbNumbers.Right - lblHint.Left;
    }

    /// <inheritdoc/>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
      base.OnFormClosed(e);
      if (DialogResult == DialogResult.OK)
        Done();
    }

    /// <summary>
    /// Called when editing is done.
    /// </summary>
    /// <remarks>
    /// Override this method to pass edited values from the dialog controls to the export filter.
    /// </remarks>
    /// <example>See the example of this method implementation that is used in the <b>ImageExport</b>.
    /// <code>
    /// protected override void Done()
    /// {
    ///   base.Done();
    ///   ImageExport imageExport = Export as ImageExport;
    ///   imageExport.ImageFormat = (ImageExportFormat)cbxImageFormat.SelectedIndex;
    ///   imageExport.Resolution = (int)udResolution.Value;
    ///   imageExport.JpegQuality = (int)udQuality.Value;
    ///   imageExport.SeparateFiles = cbSeparateFiles.Checked;
    /// }
    /// </code>
    /// </example>
    protected virtual void Done()
    {
      if (rbAll.Checked)
        Export.PageRange = PageRange.All;
      else if (rbCurrent.Checked)
        Export.PageRange = PageRange.Current;
      else
        Export.PageRange = PageRange.PageNumbers;
      Export.PageNumbers = tbNumbers.Text;
      Export.OpenAfterExport = cbOpenAfter.Checked;
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,PrinterSetup");
      gbPageRange.Text = res.Get("PageRange");
      rbAll.Text = res.Get("All");
      rbCurrent.Text = res.Get("Current");
      rbNumbers.Text = res.Get("Numbers");
      lblHint.Text = res.Get("Hint");
      cbOpenAfter.Text = Res.Get("Export,Misc,OpenAfterExport");
    }
    
    /// <summary>
    /// Initializes controls with initial values.
    /// </summary>
    /// <param name="export">The export filter to edit.</param>
    /// <remarks>
    /// Override this method to pass values from the export filter to the dialog controls.
    /// </remarks>
    /// <example>See the example of this method implementation that is used in the <b>ImageExport</b>.
    /// <code>
    /// public override void Init(ExportBase export)
    /// {
    ///   base.Init(export);
    ///   ImageExport imageExport = Export as ImageExport;
    ///   cbxImageFormat.SelectedIndex = (int)imageExport.ImageFormat;
    ///   udResolution.Value = imageExport.Resolution;
    ///   udQuality.Value = imageExport.JpegQuality;
    ///   cbSeparateFiles.Checked = imageExport.SeparateFiles;
    /// }
    /// </code>
    /// </example>
    public virtual void Init(ExportBase export)
    {
      FExport = export;
      Localize();
      
      rbAll.Checked = Export.PageRange == PageRange.All;
      rbCurrent.Checked = Export.PageRange == PageRange.Current;
      rbNumbers.Checked = Export.PageRange == PageRange.PageNumbers;
      tbNumbers.Text = Export.PageNumbers;
      cbOpenAfter.Checked = Export.OpenAfterExport;
      
      cbOpenAfter.Enabled = export.AllowOpenAfter;
      this.RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseExportForm"/> class with default settings.
    /// </summary>
    public BaseExportForm()
    {
      InitializeComponent();

      // apply Right to Left layout
      if (Config.RightToLeft)
      {
          RightToLeft = RightToLeft.Yes;

          // move components from left to right
          rbAll.Left = gbPageRange.Width - rbAll.Left - rbAll.Width;
          rbAll.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
          rbCurrent.Left = gbPageRange.Width - rbCurrent.Left - rbCurrent.Width;
          rbCurrent.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
          rbNumbers.Left = gbPageRange.Width - rbNumbers.Left - rbNumbers.Width;
          rbNumbers.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

          // move components from right to left
          tbNumbers.Left = gbPageRange.Width - tbNumbers.Left - tbNumbers.Width;
          tbNumbers.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
      }
    }
  }
}

