using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Preview
{
  internal class SourcePages : IDisposable
  {
    #region Fields
    private List<ReportPage> FPages;
    private PreparedPages FPreparedPages;
    #endregion

    #region Properties
    public int Count
    {
      get { return FPages.Count; }
    }

    public ReportPage this[int index]
    {
      get { return FPages[index]; }
    }
    #endregion

    #region Private Methods
    private Base CloneObjects(Base source, Base parent)
    {
      if (source is ReportComponentBase && !(source as ReportComponentBase).FlagPreviewVisible)
        return null;

      // create clone object and assign all properties from source
      Base clone = Activator.CreateInstance(source.GetType()) as Base;
      using (XmlItem xml = new XmlItem())
      using (FRWriter writer = new FRWriter(xml))
      using (FRReader reader = new FRReader(null, xml))
      {
        writer.SaveChildren = false;
        writer.SerializeTo = SerializeTo.SourcePages;
        writer.Write(source, clone);
        reader.Read(clone);
      }
      clone.Name = source.Name;
      clone.OriginalComponent = source;
      source.OriginalComponent = clone;
      if (clone is ReportComponentBase)
        (clone as ReportComponentBase).AssignPreviewEvents(source);
      
      // create alias
      string baseName = "";
      string objName = "Page" + FPages.Count.ToString() + "." + clone.Name;
      if (clone is BandBase)
        baseName = "b";
      else if (clone is PageBase)
      {
        baseName = "page";
        objName = "Page" + FPages.Count.ToString();
      }
      else 
        baseName = clone.BaseName[0].ToString().ToLower();
      
      clone.Alias = FPreparedPages.Dictionary.AddUnique(baseName, objName, clone);
      source.Alias = clone.Alias;

      ObjectCollection childObjects = source.ChildObjects;
      foreach (Base c in childObjects)
      {
        CloneObjects(c, clone);
      }

      clone.Parent = parent;
      return clone;
    }
    #endregion

    #region Public Methods
    public void Add(ReportPage page)
    {
      FPages.Add(CloneObjects(page, null) as ReportPage);
    }

    public void Clear()
    {
      while (FPages.Count > 0)
      {
        FPages[0].Dispose();
        FPages.RemoveAt(0);
      }
    }
    
    public int IndexOf(ReportPage page)
    {
      return FPages.IndexOf(page);
    }
    
    public void ApplyWatermark(Watermark watermark)
    {
      foreach (ReportPage page in FPages)
      {
        page.Watermark = watermark.Clone();
      }
    }
    
    public void ApplyPageSize()
    {
    }

    public void Load(XmlItem rootItem)
    {
      Clear();
      for (int i = 0; i < rootItem.Count; i++)
      {
        using (FRReader reader = new FRReader(null, rootItem[i]))
        {
          FPages.Add(reader.Read() as ReportPage);
        }
      }
    }

    public void Save(XmlItem rootItem)
    {
      rootItem.Clear();
      for (int i = 0; i < FPages.Count; i++)
      {
        using (FRWriter writer = new FRWriter(rootItem.Add()))
        {
          writer.Write(FPages[i]);
        }
      }  
    }

    public void Dispose()
    {
      Clear();
    }
    #endregion

    public SourcePages(PreparedPages preparedPages)
    {
      FPreparedPages = preparedPages;
      FPages = new List<ReportPage>();
    }
  }
}
