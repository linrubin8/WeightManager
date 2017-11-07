using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using FastReport.Utils;
using FastReport.TypeConverters;

namespace FastReport.Forms
{
  internal partial class PageSetupForm : BaseDialogForm
  {
    private PrinterSettings FPrinterSettings;
    private ReportPage FPage;
    private bool FUpdating;
    private Watermark FWatermark;
    
    public ReportPage Page
    {
      get { return FPage; }
      set 
      { 
        FPage = value;
        FWatermark = FPage.Watermark.Clone();
        UpdateControls();
      }
    }

    private bool Equal(float a, float b)
    {
      return Math.Abs(a - b) < 2;
    }
    
    private bool PaperSizeEqual(PaperSize ps, float width, float height, int rawKind)
    {
      float psWidth = ps.Width / 100f * 25.4f;
      float psHeight = ps.Height / 100f * 25.4f;

      bool sizeEqual = (Equal(psWidth, width) && Equal(psHeight, height)) ||
        (Equal(psWidth, height) && Equal(psHeight, width));
      if (rawKind == 0)
        return sizeEqual;
      return sizeEqual && ps.RawKind == rawKind;
    }
    
    private void Swap(ref float a, ref float b)
    {
      float c = a;
      a = b;
      b = c;
    }
    
    private void UpdateControls()
    {
      FUpdating = true;
      MyRes res = new MyRes("Forms,PageSetup");

      string savePrinterName = FPrinterSettings.PrinterName;
      FPrinterSettings.PrinterName = Page.Report.PrintSettings.Printer;
      if (!FPrinterSettings.IsValid)
        FPrinterSettings.PrinterName = savePrinterName;
      
      // paper
      cbxPaper.Items.Add(res.Get("Custom"));
      foreach (PaperSize ps in FPrinterSettings.PaperSizes)
      {
        cbxPaper.Items.Add(ps.PaperName);
      }
      // probe PaperWidth, PaperHeight, RawPaperSize
      if (Page.RawPaperSize != 0)
      {
        foreach (PaperSize ps in FPrinterSettings.PaperSizes)
        {
          if (cbxPaper.SelectedIndex == -1 && PaperSizeEqual(ps, Page.PaperWidth, Page.PaperHeight, Page.RawPaperSize))
            cbxPaper.SelectedIndex = cbxPaper.Items.IndexOf(ps.PaperName);
        }
      }
      // probe PaperWidth, PaperHeight only
      if (cbxPaper.SelectedIndex == -1)
      {
        foreach (PaperSize ps in FPrinterSettings.PaperSizes)
        {
          if (cbxPaper.SelectedIndex == -1 && PaperSizeEqual(ps, Page.PaperWidth, Page.PaperHeight, 0))
            cbxPaper.SelectedIndex = cbxPaper.Items.IndexOf(ps.PaperName);
        }
      }
      if (cbxPaper.SelectedIndex == -1 && cbxPaper.Items.Count > 0)
        cbxPaper.SelectedIndex = 0;
      tbWidth.Text = Converter.ToString(Page.PaperWidth, typeof(PaperConverter));
      tbHeight.Text = Converter.ToString(Page.PaperHeight, typeof(PaperConverter));
        
      // orientation
      rbPortrait.Checked = !Page.Landscape;
      rbLandscape.Checked = Page.Landscape;
      
      // margins
      tbLeft.Text = Converter.ToString(Page.LeftMargin, typeof(PaperConverter));
      tbRight.Text = Converter.ToString(Page.RightMargin, typeof(PaperConverter));
      tbTop.Text = Converter.ToString(Page.TopMargin, typeof(PaperConverter));
      tbBottom.Text = Converter.ToString(Page.BottomMargin, typeof(PaperConverter));
      cbMirrorMargins.Checked = Page.MirrorMargins;

      // sources
      int indexOfAutofeed = 0;
      foreach (PaperSource ps in FPrinterSettings.PaperSources)
      {
        cbxFirstPage.Items.Add(ps.SourceName);
        cbxOtherPages.Items.Add(ps.SourceName);
        if (ps.Kind == PaperSourceKind.AutomaticFeed)
          indexOfAutofeed = cbxFirstPage.Items.Count - 1;
        if (Page.FirstPageSource == ps.RawKind)
          cbxFirstPage.SelectedIndex = cbxFirstPage.Items.Count - 1;
        if (Page.OtherPagesSource == ps.RawKind)
          cbxOtherPages.SelectedIndex = cbxOtherPages.Items.Count - 1;
      }
      if (cbxFirstPage.SelectedIndex == -1 && cbxFirstPage.Items.Count > 0)
        cbxFirstPage.SelectedIndex = indexOfAutofeed;
      if (cbxOtherPages.SelectedIndex == -1 && cbxOtherPages.Items.Count > 0)
        cbxOtherPages.SelectedIndex = indexOfAutofeed;

      // columns
      udCount.Value = Page.Columns.Count;
      tbColumnWidth.Text = Converter.ToString(Page.Columns.Width, typeof(PaperConverter));
      string s = "";
      foreach (float pos in Page.Columns.Positions)
      {
        s += Converter.ToString(pos, typeof(PaperConverter)) + "\r\n";
      }
      tbPositions.Text = s;
      
      // duplex
      cbxDuplex.Items.Add(res.Get("DupDefault"));
      cbxDuplex.Items.Add(res.Get("DupSimplex"));
      cbxDuplex.Items.Add(res.Get("DupVertical"));
      cbxDuplex.Items.Add(res.Get("DupHorizontal"));
      cbxDuplex.SelectedIndex = Page.Duplex == Duplex.Default ? 0 : (int)Page.Duplex;
      
      cbExtraWidth.Checked = Page.ExtraDesignWidth;
      cbUnlimitedWidth.Checked = Page.UnlimitedWidth;
      cbUnlimitedHeight.Checked = Page.UnlimitedHeight;
      
      FUpdating = false;
    }

    private void UpdateColumns()
    {
      float paperWidth = (float)Converter.FromString(tbWidth.Text, typeof(PaperConverter));
      float leftMargin = (float)Converter.FromString(tbLeft.Text, typeof(PaperConverter));
      float rightMargin = (float)Converter.FromString(tbRight.Text, typeof(PaperConverter));
      float columnWidth = (paperWidth - leftMargin - rightMargin) / (float)udCount.Value;
      
      tbColumnWidth.Text = Converter.ToString(columnWidth, typeof(PaperConverter));
      string s = "";
      for (int i = 0; i < udCount.Value; i++)
      {
        float pos = i * columnWidth;
        s += Converter.ToString(pos, typeof(PaperConverter)) + "\r\n";
      }
      tbPositions.Text = s;
    }

    private void cbxPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating || cbxPaper.SelectedIndex == 0)
        return;
      foreach (PaperSize ps in FPrinterSettings.PaperSizes)
      {
        if (ps.PaperName == (string)cbxPaper.SelectedItem)
        {
          float psWidth = ps.Width / 100f * 25.4f;
          float psHeight = ps.Height / 100f * 25.4f;
          if (ps.Kind == PaperKind.A4)
          {
            psWidth = 210;
            psHeight = 297;
          }
          if (rbLandscape.Checked)
            Swap(ref psWidth, ref psHeight);
          
          FUpdating = true;
          tbWidth.Text = Converter.ToString(psWidth, typeof(PaperConverter));
          tbHeight.Text = Converter.ToString(psHeight, typeof(PaperConverter));
          UpdateColumns();
          FUpdating = false;
          break;
        }
      }
    }

    private void rbPortrait_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || !(sender as RadioButton).Checked)
        return;
      string m1 = tbLeft.Text;   //     m3
      string m2 = tbRight.Text;  //  m1    m2
      string m3 = tbTop.Text;    //     m4
      string m4 = tbBottom.Text; //
      string w = tbWidth.Text;
      string h = tbHeight.Text;

      // avoid reset papersize to custom
      FUpdating = true;
      tbWidth.Text = h;
      tbHeight.Text = w;

      if (rbLandscape.Checked)
      {
        tbLeft.Text = m3;       // rotate counter-clockwise
        tbRight.Text = m4;
        tbTop.Text = m2;
        tbBottom.Text = m1;
      }
      else
      {
        tbLeft.Text = m4;       // rotate clockwise
        tbRight.Text = m3;
        tbTop.Text = m1;
        tbBottom.Text = m2;
      }
      
      FUpdating = false;
      UpdateColumns();
      pnOrientation.Refresh();
    }

    private void tbHeight_TextChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      cbxPaper.SelectedIndex = 0;
      UpdateColumns();
    }

    private void tbLeft_TextChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateColumns();
    }

    private void udCount_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      UpdateColumns();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      using (WatermarkEditorForm form = new WatermarkEditorForm())
      {
        form.Watermark = FWatermark;
        form.HideApplyToAll();
        if (form.ShowDialog() == DialogResult.OK)
          FWatermark = form.Watermark;
      }
    }

    private void pnOrientation_Paint(object sender, PaintEventArgs e)
    {
      using (Bitmap bmp = ResourceLoader.GetBitmap(rbPortrait.Checked ? "portrait.png" : "landscape.png"))
      {
        e.Graphics.DrawImage(bmp, (pnOrientation.Width - bmp.Width) / 2, (pnOrientation.Height - bmp.Height) / 2);
      }
    }

    private void PageSetupForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      tbPositions.Height = pnColumns.Height - tbPositions.Top - 16;
      cbMirrorMargins.Width = pnMargins.Width - cbMirrorMargins.Left * 2;
    }

    private void PageSetupForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        UpdatePage();
    }

    private void UpdatePage()
    {
      Page.Landscape = rbLandscape.Checked;
      Page.PaperWidth = (float)Converter.FromString(tbWidth.Text, typeof(PaperConverter));
      Page.PaperHeight = (float)Converter.FromString(tbHeight.Text, typeof(PaperConverter));
      Page.RawPaperSize = cbxPaper.SelectedIndex == 0 ? 0 : FPrinterSettings.PaperSizes[cbxPaper.SelectedIndex - 1].RawKind;

      Page.LeftMargin = (float)Converter.FromString(tbLeft.Text, typeof(PaperConverter));
      Page.RightMargin = (float)Converter.FromString(tbRight.Text, typeof(PaperConverter));
      Page.TopMargin = (float)Converter.FromString(tbTop.Text, typeof(PaperConverter));
      Page.BottomMargin = (float)Converter.FromString(tbBottom.Text, typeof(PaperConverter));
      Page.MirrorMargins = cbMirrorMargins.Checked;
      if (cbxFirstPage.Items.Count > 0)
      {
        Page.FirstPageSource = FPrinterSettings.PaperSources[cbxFirstPage.SelectedIndex].RawKind;
        Page.OtherPagesSource = FPrinterSettings.PaperSources[cbxOtherPages.SelectedIndex].RawKind;
      }
      
      Page.Columns.Count = (int)udCount.Value;
      Page.Columns.Width = (float)Converter.FromString(tbColumnWidth.Text, typeof(PaperConverter));
      string s = tbPositions.Text;
      s = s.Replace("\r\n", ";");
      string[] positions = s.Split(new char[] { ';' });
      Page.Columns.Positions.Clear();
      foreach (string pos in positions)
      {
        if (!String.IsNullOrEmpty(pos.Trim()))
          Page.Columns.Positions.Add((float)Converter.FromString(pos, typeof(PaperConverter)));
      }
      
      Page.Duplex = (Duplex)(cbxDuplex.SelectedIndex == 0 ? -1 : cbxDuplex.SelectedIndex);
      Page.Watermark = FWatermark;
      Page.ExtraDesignWidth = cbExtraWidth.Checked;
      Page.UnlimitedWidth = cbUnlimitedWidth.Checked;
      Page.UnlimitedHeight = cbUnlimitedHeight.Checked;
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,PageSetup");
      Text = res.Get("");
      
      pnPaper.Text = res.Get("Paper");
      pnMargins.Text = res.Get("Margins");
      pnSource.Text = res.Get("Source");
      pnColumns.Text = res.Get("Columns");
      pnOther.Text = res.Get("Other");
      
      lblWidth.Text = res.Get("Width");
      lblHeight.Text = res.Get("Height");
      gbOrientation.Text = res.Get("Orientation");
      rbPortrait.Text = res.Get("Portrait");
      rbLandscape.Text = res.Get("Landscape");
      lblLeft.Text = res.Get("Left");
      lblRight.Text = res.Get("Right");
      lblTop.Text = res.Get("Top");
      lblBottom.Text = res.Get("Bottom");
      cbMirrorMargins.Text = res.Get("MirrorMargins");
      lblFirstPage.Text = res.Get("FirstPage");
      lblOtherPages.Text = res.Get("OtherPages");
      lblCount.Text = res.Get("Count");
      lblColumnWidth.Text = res.Get("ColumnWidth");
      lblPositions.Text = res.Get("Positions");
      lblDuplex.Text = res.Get("Duplex");
      btnEdit.Text = res.Get("EditWatermark");
      cbExtraWidth.Text = res.Get("ExtraWidth");
      cbUnlimitedWidth.Text = res.Get("UnlimitedWidth");
      cbUnlimitedHeight.Text = res.Get("UnlimitedHeight");
    }

    public PageSetupForm()
    {
        InitializeComponent();
        Localize();
        FPrinterSettings = new PrinterSettings();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            lblWidth.Left = pnPaper.Width - lblWidth.Left - lblWidth.Width;
            lblWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbWidth.Left = pnPaper.Width - tbWidth.Left - tbWidth.Width;
            tbWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblHeight.Left = pnPaper.Width - lblHeight.Left - lblHeight.Width;
            lblHeight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbHeight.Left = pnPaper.Width - tbHeight.Left - tbHeight.Width;
            tbHeight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            rbPortrait.Left = gbOrientation.Width - rbPortrait.Left - rbPortrait.Width;
            rbPortrait.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbLandscape.Left = gbOrientation.Width - rbLandscape.Left - rbLandscape.Width;
            rbLandscape.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pnOrientation.Left = gbOrientation.Width - pnOrientation.Left - pnOrientation.Width;
            pnOrientation.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            lblLeft.Left = pnMargins.Width - lblLeft.Left - lblLeft.Width;
            lblLeft.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbLeft.Left = pnMargins.Width - tbLeft.Left - tbLeft.Width;
            tbLeft.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblTop.Left = pnMargins.Width - lblTop.Left - lblTop.Width;
            lblTop.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbTop.Left = pnMargins.Width - tbTop.Left - tbTop.Width;
            tbTop.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblRight.Left = pnMargins.Width - lblRight.Left - lblRight.Width;
            lblRight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbRight.Left = pnMargins.Width - tbRight.Left - tbRight.Width;
            tbRight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblBottom.Left = pnMargins.Width - lblBottom.Left - lblBottom.Width;
            lblBottom.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbBottom.Left = pnMargins.Width - tbBottom.Left - tbBottom.Width;
            tbBottom.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbMirrorMargins.Left = pnMargins.Width - cbMirrorMargins.Left - cbMirrorMargins.Width;
            cbMirrorMargins.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            lblFirstPage.Left = pnSource.Width - lblFirstPage.Left - lblFirstPage.Width;
            lblFirstPage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblOtherPages.Left = pnSource.Width - lblOtherPages.Left - lblOtherPages.Width;
            lblOtherPages.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            lblCount.Left = pnColumns.Width - lblCount.Left - lblCount.Width;
            lblCount.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            udCount.Left = pnColumns.Width - udCount.Left - udCount.Width;
            udCount.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblColumnWidth.Left = pnColumns.Width - lblColumnWidth.Left - lblColumnWidth.Width;
            lblColumnWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbColumnWidth.Left = pnColumns.Width - tbColumnWidth.Left - tbColumnWidth.Width;
            tbColumnWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblPositions.Left = pnColumns.Width - lblPositions.Left - lblPositions.Width;
            lblPositions.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbPositions.Left = pnColumns.Width - tbPositions.Left - tbPositions.Width;
            tbPositions.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            lblDuplex.Left = pnOther.Width - lblDuplex.Left - lblDuplex.Width;
            lblDuplex.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbExtraWidth.Left = pnOther.Width - cbExtraWidth.Left - cbExtraWidth.Width;
            cbExtraWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbUnlimitedHeight.Left = pnOther.Width - cbUnlimitedHeight.Left - cbUnlimitedHeight.Width;
            cbUnlimitedHeight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbUnlimitedWidth.Left = pnOther.Width - cbUnlimitedWidth.Left - cbUnlimitedWidth.Width;
            cbUnlimitedWidth.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
            btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
        }
    }
  }
}
