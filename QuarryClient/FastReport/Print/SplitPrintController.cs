using System;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using FastReport.Utils;
using System.Drawing;

namespace FastReport.Print
{
  internal class SplitPrintController : PrintControllerBase
  {
    #region Fields
    private List<SplittedPage> FSplittedPages;
    private float FAddX = 5;
    private float FAddY = 5;
    private float FOffsetX;
    private float FOffsetY;
    private bool FLandscape;
    #endregion

    #region Private methods
    private ReportPage GetNextSplittedPage()
    {
      if (FSplittedPages.Count == 0) 
        return null;

      PageNo = FSplittedPages[0].PageNo;
      FOffsetX = FSplittedPages[0].OffsetX;
      FOffsetY = FSplittedPages[0].OffsetY;
      FLandscape = FSplittedPages[0].Landscape;
      FSplittedPages.RemoveAt(0);

      return Report.PreparedPages.GetPage(PageNo);
    }

    private bool HasMoreSplittedPages()
    {
      return FSplittedPages.Count > 0; 
    }

    private void TrySplit(float a, float b, float c, float d, out int x, out int y)
    {
      x = Math.Abs(Math.Truncate(a / c) * c - a) < 11 ?
        (int)Math.Round(a / c) : (int)Math.Truncate(a / c) + 1;

      y = Math.Abs(Math.Truncate(b / d) * d - b) < 11 ?
        (int)Math.Round(b / d) : (int)Math.Truncate(b / d) + 1;
    }

    private void SplitPage(float a, float b, float c, float d, out int x, out int y, out bool needRotate)
    {
      needRotate = false;

      TrySplit(a, b, c, d, out x, out y);

      int tempX = x;
      int tempY = y;

      float tempC = c;
      c = d;
      d = tempC;

      TrySplit(a, b, c, d, out x, out y);

      if (x * y >= tempX * tempY)
      {
        x = tempX;
        y = tempY;
      }
      else
        needRotate = true;
    }
    #endregion

    #region Public methods
    public override void QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
    {
      Page = GetNextSplittedPage();
      if (Page != null)
      {
        SetPaperSize(Report.PrintSettings.PrintOnSheetWidth, Report.PrintSettings.PrintOnSheetHeight,
          Report.PrintSettings.PrintOnSheetRawPaperSize, e);
        e.PageSettings.Landscape = FLandscape;
        SetPaperSource(Page, e);
        Duplex duplex = Page.Duplex;
        if (duplex != Duplex.Default)
          e.PageSettings.PrinterSettings.Duplex = duplex;
      }
    }

    public override void PrintPage(object sender, PrintPageEventArgs e)
    {
      StartPage(e);

      Graphics g = e.Graphics;
      g.PageUnit = GraphicsUnit.Pixel;
      g.TranslateTransform(FOffsetX * Units.Millimeters * g.DpiX / 96, FOffsetY * Units.Millimeters * g.DpiY / 96);
      FRPaintEventArgs paintArgs = new FRPaintEventArgs(g, g.DpiX / 96, g.DpiY / 96, Report.GraphicCache);
      Page.Print(paintArgs);

      Page.Dispose();

      FinishPage(e);
      e.HasMorePages = HasMoreSplittedPages();
    }
    #endregion

    public SplitPrintController(Report report, PrintDocument doc, int curPage) : base(report, doc, curPage)
    { 
      FSplittedPages = new List<SplittedPage>();

      // get hard margins
      float leftMargin = doc.PrinterSettings.DefaultPageSettings.HardMarginX / 100f * 25.4f;
      float topMargin = doc.PrinterSettings.DefaultPageSettings.HardMarginY / 100f * 25.4f;
      float rightMargin = leftMargin;
      float bottomMargin = topMargin;

      int countX;
      int countY;
      bool needChangeOrientation;

      while (true)
      {
        Page = GetNextPage();
        if (Page == null) 
          break;

        if (!Page.UnlimitedHeight && !Page.UnlimitedWidth)
        {
            SplitPage(Page.PaperWidth, Page.PaperHeight,
                Report.PrintSettings.PrintOnSheetWidth, Report.PrintSettings.PrintOnSheetHeight,
                out countX, out countY, out needChangeOrientation);
        }
        else
        {
            SplitPage(Page.WidthInPixels / Units.Millimeters, Page.HeightInPixels / Units.Millimeters,
                Report.PrintSettings.PrintOnSheetWidth, Report.PrintSettings.PrintOnSheetHeight,
                out countX, out countY, out needChangeOrientation);
        }

        bool landscape = false;
        if (needChangeOrientation)
          landscape = true;

        float pieceX = landscape ? Report.PrintSettings.PrintOnSheetHeight : Report.PrintSettings.PrintOnSheetWidth;
        float pieceY = landscape ? Report.PrintSettings.PrintOnSheetWidth : Report.PrintSettings.PrintOnSheetHeight;

        float marginY = 0;
        float printedY = 0;
        float offsY = -topMargin;

        for (int y = 1; y <= countY; y++)
        {
          float marginX = 0;
          float printedX = 0;
          float offsX = -leftMargin;

          for (int x = 1; x <= countX; x++)
          {
            FSplittedPages.Add(new SplittedPage(PageNo, offsX, offsY, landscape));

            printedX += (pieceX - marginX - rightMargin) - FAddX;
            offsX = -printedX;
            marginX = leftMargin;
          }

          printedY += (pieceY - marginY - bottomMargin) - FAddY;
          offsY = -printedY;
          marginY = topMargin;
        }
        
        Page.Dispose();
      }
    }


    private class SplittedPage
    {
      // physical pageno
      public int PageNo;
      // offsetx, in mm
      public float OffsetX;
      // offsety, in mm
      public float OffsetY;
      // determines if we should rotate the output page
      public bool Landscape;

      public SplittedPage(int pageNo, float ofsX, float ofsY, bool landscape)
      {
        PageNo = pageNo;
        OffsetX = ofsX;
        OffsetY = ofsY;
        Landscape = landscape;
      }
    }
  }
}