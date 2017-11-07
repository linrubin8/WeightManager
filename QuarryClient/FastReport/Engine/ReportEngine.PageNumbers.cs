using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    private List<PageNumberInfo> FPageNumbers;
    private int FLogicalPageNo;

    private void InitPageNumbers()
    {
      FPageNumbers = new List<PageNumberInfo>();
      FLogicalPageNo = 0;
    }
    
    internal void IncLogicalPageNumber()
    {
      FLogicalPageNo++;
      int index = CurPage - FFirstReportPage;
      if (FirstPass || index >= FPageNumbers.Count)
      {
        PageNumberInfo info = new PageNumberInfo();
        FPageNumbers.Add(info);
        info.PageNo = FLogicalPageNo;
      }
    }
    
    /// <summary>
    /// Resets the logical page numbers.
    /// </summary>
    public void ResetLogicalPageNumber()
    {
      if (!FirstPass)
        return;

      for (int i = FPageNumbers.Count - 1; i >= 0; i--)
      {
        PageNumberInfo info = FPageNumbers[i];
        info.TotalPages = FLogicalPageNo;
        if (info.PageNo == 1)
          break;
      }
      
      FLogicalPageNo = 0;
    }
    
    private int GetLogicalPageNumber()
    {
      int index = CurPage - FFirstReportPage;
      return FPageNumbers[index].PageNo + Report.InitialPageNumber - 1;
    }
    
    private int GetLogicalTotalPages()
    {
      int index = CurPage - FFirstReportPage;
      return FPageNumbers[index].TotalPages + Report.InitialPageNumber - 1;
    }


    private class PageNumberInfo
    {
      public int PageNo;
      public int TotalPages;
    }
  }
}
