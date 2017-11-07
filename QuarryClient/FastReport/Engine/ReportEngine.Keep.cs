using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    private bool FKeeping;
    private int FKeepPosition;
    private XmlItem FKeepOutline;
    private int FKeepBookmarks;
    private float FKeepCurY;
    private float FKeepDeltaY;

    private void StartKeep(BandBase band)
    {
      // do not keep the first row on a page, avoid empty first page
      if (FKeeping || (band != null && band.AbsRowNo == 1 && !band.FirstRowStartsNewPage))
        return;
      FKeeping = true;

      FKeepPosition = PreparedPages.CurPosition;
      FKeepOutline = PreparedPages.Outline.CurPosition;
      FKeepBookmarks = PreparedPages.Bookmarks.CurPosition;
      FKeepCurY = CurY;
      Report.Dictionary.Totals.StartKeep();
      StartKeepReprint();
    }

    /// <summary>
    /// Starts the keep mechanism.
    /// </summary>
    /// <remarks>
    /// Use this method along with the <see cref="EndKeep"/> method if you want to keep
    /// several bands together. Call <b>StartKeep</b> method before printing the first band 
    /// you want to keep, then call the <b>EndKeep</b> method after printing the last band you want to keep.
    /// </remarks>
    public void StartKeep()
    {
      StartKeep(null);
    }

    /// <summary>
    /// Ends the keep mechanism.
    /// </summary>
    /// <remarks>
    /// Use this method along with the <see cref="StartKeep()"/> method if you want to keep
    /// several bands together. Call <b>StartKeep</b> method before printing the first band 
    /// you want to keep, then call the <b>EndKeep</b> method after printing the last band you want to keep.
    /// </remarks>
    public void EndKeep()
    {
      if (FKeeping)
      {
        Report.Dictionary.Totals.EndKeep();
        EndKeepReprint();
        FKeeping = false;
      }
    }

    private void CutObjects()
    {
      FKeepDeltaY = CurY - FKeepCurY;
      PreparedPages.CutObjects(FKeepPosition);
      CurY = FKeepCurY;
    }

    private void PasteObjects()
    {
      PreparedPages.PasteObjects(CurX, CurY);
      PreparedPages.Outline.Shift(FKeepOutline, CurY);
      PreparedPages.Bookmarks.Shift(FKeepBookmarks, CurY);
      EndKeep();
      CurY += FKeepDeltaY;
    }
  }
}
