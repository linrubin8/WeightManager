using System;
using System.Collections;
using System.Collections.Generic;

namespace FastReport.Print
{
  internal class PageNumbersParser
  {
    private List<int> FPages;

    public int Count
    {
      get { return FPages.Count; }
    }

    private bool Parse(string pageNumbers, int total)
    {
      FPages.Clear();
      string s = pageNumbers.Replace(" ", "");
      if (s == "") return false;

      if (s[s.Length - 1] == '-')
        s += total.ToString();
      s += ',';
      
      int i = 0; 
      int j = 0; 
      int n1 = 0;
      int n2 = 0;
      bool isRange = false;

      while (i < s.Length)
      {
        if (s[i] == ',')
        {
          n2 = int.Parse(s.Substring(j, i - j));
          j = i + 1;
          if (isRange)
          {
            while (n1 <= n2)
            {
              FPages.Add(n1 - 1);
              n1++;
            }
          }
          else
            FPages.Add(n2 - 1);
          isRange = false;
        }
        else if (s[i] == '-')
        {
          isRange = true;
          n1 = int.Parse(s.Substring(j, i - j));
          j = i + 1;
        }
        i++;
      }

      return true;
    }

    public bool GetPage(ref int pageNo)
    {
      if (FPages.Count == 0) 
        return false;
      pageNo = FPages[0];
      FPages.RemoveAt(0);
      return true;
    }

    public PageNumbersParser(Report report, int curPage)
    {
      FPages = new List<int>();

      int total = report.PreparedPages.Count;
      if (report.PrintSettings.PageRange == PageRange.Current)
        FPages.Add(curPage - 1);
      else if (!Parse(report.PrintSettings.PageNumbers, total))
      {
        for (int i = 0; i < total; i++)
          FPages.Add(i);
      }

#if Demo
      total = 5;
#endif
      // remove bad page numbers
      for (int i = 0; i < FPages.Count; i++)
      {
        if (FPages[i] >= total || FPages[i] < 0)
        {
          FPages.RemoveAt(i);
          i--;
        }  
      }  
        
      if (report.PrintSettings.PrintPages == PrintPages.Odd)
      {
        int i = 0;
        while (i < FPages.Count)
        {
          if (FPages[i] % 2 == 0) 
            i++;
          else
            FPages.RemoveAt(i);
        }
      }
      else if (report.PrintSettings.PrintPages == PrintPages.Even)
      {
        int i = 0;
        while (i < FPages.Count)
        {
          if (FPages[i] % 2 != 0) 
            i++;
          else
            FPages.RemoveAt(i);
        }
      }

      if (report.PrintSettings.Reverse)
        FPages.Reverse();
    }
  }
}