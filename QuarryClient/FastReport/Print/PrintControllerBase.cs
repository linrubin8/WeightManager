using System;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using FastReport.Utils;

namespace FastReport.Print
{
  internal abstract class PrintControllerBase
  {
    #region Fields
    private Report FReport;
    private PrintDocument FDoc;
    private PageNumbersParser FPages;
    private ReportPage FPage;
    private int FPageNo;
    private int FPrintedPageCount;
    #endregion

    #region Properties
    public Report Report
    {
      get { return FReport; }
    }

    public PrintDocument Doc
    {
      get { return FDoc; }
    }

    public PageNumbersParser Pages
    {
      get { return FPages; }
    }

    public ReportPage Page
    {
      get { return FPage; }
      set { FPage = value; }
    }

    public int PageNo
    {
      get { return FPageNo; }
      set { FPageNo = value; }
    }
    #endregion

    #region Private Methods
    private PaperSize FindPaperSize(PrinterSettings.PaperSizeCollection paperSizes,
      float paperWidth, float paperHeight, int rawKind)
    {
      foreach (PaperSize ps in paperSizes)
      {
        // convert hundreds of inches to mm
        float psWidth = ps.Width / 100f * 25.4f;
        float psHeight = ps.Height / 100f * 25.4f;
        // check if page has the same kind and size
        bool sizeEqual = Math.Abs(paperWidth - psWidth) < 5 && Math.Abs(paperHeight - psHeight) < 5;
        if (sizeEqual)
        {
          if (rawKind == 0 || ps.RawKind == rawKind)
            return ps;
        }
      }

      return null;
    }
    #endregion

    #region Protected methods
    protected ReportPage GetNextPage()
    {
      FPageNo = 0;
      if (Pages.GetPage(ref FPageNo))
        return Report.PreparedPages.GetPage(FPageNo);
      return null;
    }

    protected bool HasMorePages()
    {
      return Pages.Count > 0; 
    }

    protected void SetPaperSize(ReportPage page, QueryPageSettingsEventArgs e)
    {
      float width = page.PaperWidth;
      float height = page.PaperHeight;
      if (page.Landscape)
      {
        width = page.PaperHeight;
        height = page.PaperWidth;
      }
      SetPaperSize(width, height, page.RawPaperSize, e);
    }

    protected void SetPaperSize(float paperWidth, float paperHeight, int rawKind, QueryPageSettingsEventArgs e)
    {
      PaperSize ps = null;

      // check PaperWidth, PaperHeight, RawKind
      if (rawKind != 0)
        ps = FindPaperSize(e.PageSettings.PrinterSettings.PaperSizes, paperWidth, paperHeight, rawKind);

      // check PaperWidth, PaperHeight only
      if (ps == null)
        ps = FindPaperSize(e.PageSettings.PrinterSettings.PaperSizes, paperWidth, paperHeight, 0);

      // paper size not found, create custom one
      if (ps == null)
      {
        ps = new PaperSize();
        ps.Width = (int)Math.Round(paperWidth / 25.4f * 100);
        ps.Height = (int)Math.Round(paperHeight / 25.4f * 100);
      }

      e.PageSettings.PaperSize = ps;
    }

    protected void SetPaperSource(ReportPage page, QueryPageSettingsEventArgs e)
    {
      int rawKind = Report.PrintSettings.PaperSource;
      // it's set to Automatic, try page.PaperSource
      if (rawKind == 7)
        rawKind = PageNo == 0 ? page.FirstPageSource : page.OtherPagesSource;
      // do not change paper source if it is AutomaticFeed
      if (rawKind == 7)
        return;

      foreach (PaperSource ps in e.PageSettings.PrinterSettings.PaperSources)
      {
        if (ps.RawKind == rawKind)
        {
          e.PageSettings.PaperSource = ps;
          return;
        }
      }
    }

    protected void StartPage(PrintPageEventArgs e)
    {
      FPrintedPageCount++;
      Config.ReportSettings.OnProgress(Report, 
        String.Format(Res.Get("Messages,PrintingPage"), FPrintedPageCount), FPrintedPageCount, 0);
    }

    protected void FinishPage(PrintPageEventArgs e)
    {
      if (Report.Aborted)
        e.Cancel = true;
    }
    #endregion

    #region Public methods
    public abstract void QueryPageSettings(object sender, QueryPageSettingsEventArgs e);
    public abstract void PrintPage(object sender, PrintPageEventArgs e);
    #endregion

    public PrintControllerBase(Report report, PrintDocument doc, int curPage)
    {
      FReport = report;
      FDoc = doc;
      FPages = new PageNumbersParser(report, curPage);

      // select the printer
      if (!String.IsNullOrEmpty(report.PrintSettings.Printer))
        FDoc.PrinterSettings.PrinterName = report.PrintSettings.Printer;
        
      // print to file
      if (report.PrintSettings.PrintToFile)
      {
        FDoc.PrinterSettings.PrintFileName = report.PrintSettings.PrintToFileName;
        FDoc.PrinterSettings.PrintToFile = true;
      }
        
      // set job name
      if (!String.IsNullOrEmpty(report.ReportInfo.Name))
        FDoc.DocumentName = report.ReportInfo.Name;
      else
        FDoc.DocumentName = report.FileName;

      // set copies and collation
      if (report.PrintSettings.Copies < 1)
        report.PrintSettings.Copies = 1;
      FDoc.PrinterSettings.Copies = (short)report.PrintSettings.Copies;
      FDoc.PrinterSettings.Collate = report.PrintSettings.Collate;
    }
  }
}