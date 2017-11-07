using System;
using FastReport.Utils;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    private ReportPage FPage;
    private float FColumnStartY;
    private string FPageNameForRecalc;

    private DataBand FindDeepmostDataBand(ReportPage page)
    {
      DataBand result = null;
      foreach (Base c in page.AllObjects)
      {
        if (c is DataBand)
          result = c as DataBand;
      }
      return result;
    }

    private void RunReportPage(ReportPage page)
    {
      FPage = page;
      InitReprint();
      FPageNameForRecalc = null;
      FPage.OnStartPage(EventArgs.Empty);
      bool previousPage = StartFirstPage();
      OnStateChanged(FPage, EngineState.ReportPageStarted);
      OnStateChanged(FPage, EngineState.PageStarted);

      DataBand keepSummaryBand = FindDeepmostDataBand(page);
      if (keepSummaryBand != null)
        keepSummaryBand.KeepSummary = true;

      if (FPage.IsManualBuild)
        FPage.OnManualBuild(EventArgs.Empty);
      else
        RunBands(page.Bands);

      OnStateChanged(FPage, EngineState.PageFinished);
      OnStateChanged(FPage, EngineState.ReportPageFinished);
      EndLastPage();
      //recalculate unlimited
      if (page.UnlimitedHeight || page.UnlimitedWidth)
      {
        PreparedPages.ModifyPageSize(page.Name);
        if(previousPage && FPageNameForRecalc!=null)
          PreparedPages.ModifyPageSize(FPageNameForRecalc);
      }
      //recalculate unlimited
      FPage.OnFinishPage(EventArgs.Empty);

      if (FPage.BackPage)
      {
        PreparedPages.InterleaveWithBackPage(PreparedPages.CurPage);
      }
    }

    private void RunReportPages()
    {
      for (int i = 0; i < Report.Pages.Count; i++)
      {
        ReportPage page = Report.Pages[i] as ReportPage;
        if (page != null && page.Visible && page.Subreport == null)
          RunReportPage(page);
        if (Report.Aborted)
          break;
      }
    }

    private void RunBands(BandCollection bands)
    {
      for (int i = 0; i < bands.Count; i++)
      {
        BandBase band = bands[i];
        if (band is DataBand)
          RunDataBand(band as DataBand);
        else if (band is GroupHeaderBand)
          RunGroup(band as GroupHeaderBand);
        if (Report.Aborted)
          break;
      }
    }

    private void ShowPageHeader()
    {
      ShowBand(FPage.PageHeader);
    }

    private void ShowPageFooter()
    {
      ShowBand(FPage.PageFooter);
    }
    
    private bool StartFirstPage()
    {
      FPage.InitializeComponents();

      CurX = 0;
      CurY = 0;
      FCurColumn = 0;

      if (FPage.ResetPageNumber)
        ResetLogicalPageNumber();

      bool previousPage = FPage.PrintOnPreviousPage && PreparedPages.Count > 0;
      // check that previous page has the same size
      if (previousPage)
      {
        using (ReportPage page = PreparedPages.GetPage(PreparedPages.Count - 1))
        {
          if (page.PaperWidth == FPage.PaperWidth)
          {
            if (page.UnlimitedWidth == FPage.UnlimitedWidth)
            {
              previousPage = true;
              if (FPage.UnlimitedWidth)
                FPageNameForRecalc = page.Name;
            }
            else
            {
              previousPage = false;
            }
          }
          else if (page.UnlimitedWidth && FPage.UnlimitedWidth)
          {
            previousPage = true;
            FPageNameForRecalc = page.Name;
          }
          else { previousPage = false; }
          if (previousPage)
          {
            if (page.PaperHeight == FPage.PaperHeight)
            {
              if (page.UnlimitedHeight == FPage.UnlimitedHeight)
              {
                previousPage = true;
                if (FPage.UnlimitedHeight)
                  FPageNameForRecalc = page.Name;
              }
              else
              {
                previousPage = false;
              }
            }
            else if (page.UnlimitedHeight && FPage.UnlimitedHeight)
            {
              previousPage = true;
            }
            else { previousPage = false; }
          }
        }
      }

      // update CurY or add new page
      if (previousPage)
        CurY = PreparedPages.GetLastY();
      else
      {
        PreparedPages.AddPage(FPage);
        if (FPage.StartOnOddPage && (CurPage % 2) == 1)
          PreparedPages.AddPage(FPage);
      }

      // page numbers
      if (FIsFirstReportPage)
        FFirstReportPage = CurPage;
      if (FIsFirstReportPage && previousPage)
        IncLogicalPageNumber();
      FIsFirstReportPage = false;

      OutlineRoot();
      AddPageOutline();

      // show report title and page header
      if (previousPage)
        ShowBand(FPage.ReportTitle);
      else
      {
        if (FPage.Overlay != null)
          ShowBand(FPage.Overlay);
        if (FPage.TitleBeforeHeader)
        {
          ShowBand(FPage.ReportTitle);
          ShowPageHeader();
        }
        else
        {
          ShowPageHeader();
          ShowBand(FPage.ReportTitle);
        }
      }

      // show column header
      FColumnStartY = CurY;
      ShowBand(FPage.ColumnHeader);

      // calculate CurX before starting column event depending on Right to Left or Left to Right layout
      if (Config.RightToLeft)
      {
          CurX = FPage.Columns.Positions[FPage.Columns.Positions.Count - 1] * Units.Millimeters;
      }
      else
      {
          CurX = 0;
      }

      // start column event
      OnStateChanged(FPage, EngineState.ColumnStarted);
      ShowProgress();
      return previousPage;
    }

    private void EndLastPage()
    {
      // end column event
      OnStateChanged(FPage, EngineState.ColumnFinished);

      if (FPage.ReportSummary != null)
      {
        // do not show column footer here! It's a special case and is handled in the ShowBand.
        ShowBand(FPage.ReportSummary);
      }
      else
      {
        ShowBand(FPage.ColumnFooter);
      }

      ShowPageFooter();
      OutlineRoot();
      FPage.FinalizeComponents();
    }

    private void EndColumn()
    {
      EndColumn(true);
    }

    private void EndColumn(bool showColumnFooter)
    {
      // end column event
      OnStateChanged(FPage, EngineState.ColumnFinished);

      // check keep
      if (FKeeping)
        CutObjects();

      ShowReprintFooters();

      if (showColumnFooter)
        ShowBand(FPage.ColumnFooter);

      FCurColumn++;

      if (FCurColumn >= FPage.Columns.Count)
        FCurColumn = 0;

      // apply Right to Left layot if needed
      if (Config.RightToLeft)
      {
          FCurX = FPage.Columns.Positions[FPage.Columns.Count - FCurColumn - 1] * Units.Millimeters;
      }
      else
      {
          FCurX = FCurColumn == 0 ? 0 : FPage.Columns.Positions[FCurColumn] * Units.Millimeters;
      }

      if (CurColumn == 0)
      {
          EndPage();
      }
      else
      {
          StartColumn();
      }

      // end keep
      if (FKeeping)
        PasteObjects();
    }

    private void StartColumn()
    {
      FCurY = FColumnStartY;

      ShowBand(FPage.ColumnHeader);
      ShowReprintHeaders();

      // start column event
      OnStateChanged(FPage, EngineState.ColumnStarted);
    }

    private void EndPage()
    {
      EndPage(true);
    }

    internal void EndPage(bool startPage)
    {
      OnStateChanged(FPage, EngineState.PageFinished);
      ShowPageFooter();

      if (Report.MaxPages > 0 && PreparedPages.Count >= Report.MaxPages)
        Report.Abort();
      if (startPage)
        StartPage();
    }

    private void StartPage()
    {
      // apply Right to Left layout if needed 
      if (Config.RightToLeft)
      {
          CurX = FPage.Columns.Positions[FPage.Columns.Positions.Count - 1] * Units.Millimeters;
      }
      else
      {
          CurX = 0;
      }

      CurY = 0;
      FCurColumn = 0;

      PreparedPages.AddPage(FPage);
      AddPageOutline();

      if (FPage.Overlay != null)
        ShowBand(FPage.Overlay);
      ShowPageHeader();
      OnStateChanged(FPage, EngineState.PageStarted);

      FColumnStartY = CurY;

      StartColumn();
      ShowProgress();
    }

    /// <summary>
    /// Starts a new page.
    /// </summary>
    public void StartNewPage()
    {
      EndPage();
    }

    /// <summary>
    /// Starts a new column.
    /// </summary>
    public void StartNewColumn()
    {
      EndColumn();
    }

    private void ShowProgress()
    {
      if (!Config.ReportSettings.ShowProgress) return;
      string msg = Report.DoublePass && FirstPass ?
        Res.Get("Messages,GeneratingPageFirstPass") :
        Res.Get("Messages,GeneratingPage");
      Config.ReportSettings.OnProgress(Report, String.Format(msg, CurPage + 1), CurPage + 1, TotalPages);
    }
  }
}
