using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    private List<BandBase> FReprintHeaders;
    private List<BandBase> FKeepReprintHeaders;
    private List<BandBase> FReprintFooters;
    private List<BandBase> FKeepReprintFooters;

    private void InitReprint()
    {
      FReprintHeaders = new List<BandBase>();
      FKeepReprintHeaders = new List<BandBase>();
      FReprintFooters = new List<BandBase>();
      FKeepReprintFooters = new List<BandBase>();
    }

    private void ShowReprintHeaders()
    {
      float saveOriginX = FOriginX;
      
      foreach (BandBase band in FReprintHeaders)
      {
        band.Repeated = true;
        FOriginX = band.ReprintOffset;
        ShowBand(band);
        band.Repeated = false;
      }
      
      FOriginX = saveOriginX;
    }
    
    private void ShowReprintFooters()
    {
      ShowReprintFooters(true);
    }
    
    private void ShowReprintFooters(bool repeated)
    {
      float saveOriginX = FOriginX;

      // show footers in reverse order
      for (int i = FReprintFooters.Count - 1; i >= 0; i--)
      {
        BandBase band = FReprintFooters[i];
        band.Repeated = repeated;
        band.FlagCheckFreeSpace = false;
        FOriginX = band.ReprintOffset;
        ShowBand(band);
        band.Repeated = false;
        band.FlagCheckFreeSpace = true;
      }

      FOriginX = saveOriginX;
    }
    
    private void AddReprint(BandBase band)
    {
      // save current offset and use it later when reprinting a band.
      // it is required when printing subreports
      band.ReprintOffset = FOriginX;

      if (FKeeping)
      {
        if (band is DataHeaderBand || band is GroupHeaderBand)
          FKeepReprintHeaders.Add(band);
        else
          FKeepReprintFooters.Add(band);  
      }  
      else
      {
        if (band is DataHeaderBand || band is GroupHeaderBand)
          FReprintHeaders.Add(band);
        else
          FReprintFooters.Add(band);
      }  
    }
    
    private void RemoveReprint(BandBase band)
    {
      if (FKeepReprintHeaders.Contains(band))
        FKeepReprintHeaders.Remove(band);
      if (FReprintHeaders.Contains(band))
        FReprintHeaders.Remove(band);
      if (FKeepReprintFooters.Contains(band))
        FKeepReprintFooters.Remove(band);
      if (FReprintFooters.Contains(band))
        FReprintFooters.Remove(band);
    }
    
    private void StartKeepReprint()
    {
      FKeepReprintHeaders.Clear();
      FKeepReprintFooters.Clear();
    }
    
    private void EndKeepReprint()
    {
      foreach (BandBase band in FKeepReprintHeaders)
      {
        FReprintHeaders.Add(band);
      }
      foreach (BandBase band in FKeepReprintFooters)
      {
        FReprintFooters.Add(band);
      }
      FKeepReprintHeaders.Clear();
      FKeepReprintFooters.Clear();
    }
    
    private float GetFootersHeight()
    {
      float result = 0;
      bool saveRepeated = false;

      foreach (BandBase band in FReprintFooters)
      {
        saveRepeated = band.Repeated;
        band.Repeated = true;
        result += GetBandHeightWithChildren(band);
        band.Repeated = saveRepeated;
      }
      foreach (BandBase band in FKeepReprintFooters)
      {
        saveRepeated = band.Repeated;
        band.Repeated = true;
        result += GetBandHeightWithChildren(band);
        band.Repeated = saveRepeated;
      }
      
      return result;
    }
  }
}
