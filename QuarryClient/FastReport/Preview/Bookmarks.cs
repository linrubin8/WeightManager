using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;
using System.Collections;

namespace FastReport.Preview
{
  internal class Bookmarks
  {
    private List<BookmarkItem> FItems;
    private List<BookmarkItem> FFirstPassItems;
  
    internal int CurPosition
    {
      get { return FItems.Count; }
    }
    
    internal void Shift(int index, float newY)
    {
      if (index < 0 || index >= FItems.Count)
        return;
        
      float topY = FItems[index].OffsetY;
      float shift = newY - topY;
      
      for (int i = index; i < FItems.Count; i++)
      {
        FItems[i].PageNo++;
        FItems[i].OffsetY += shift;
      }
    }
    
    public void Add(string name, int pageNo, float offsetY)
    {
      BookmarkItem item = new BookmarkItem();
      item.Name = name;
      item.PageNo = pageNo;
      item.OffsetY = offsetY;
      
      FItems.Add(item);
    }
    
    public int GetPageNo(string name)
    {
      BookmarkItem item = Find(name);
      if (item == null)
        item = Find(name, FFirstPassItems);
      return item == null ? 0 : item.PageNo + 1;
    }
    
    public BookmarkItem Find(string name)
    {
      return Find(name, FItems);
    }

    private BookmarkItem Find(string name, List<BookmarkItem> items)
    {
      if (items == null)
        return null;

      foreach (BookmarkItem item in items)
      {
        if (item.Name == name)
          return item;
      }
      
      return null;
    }

    public void Clear()
    {
      FItems.Clear();
    }

    public void ClearFirstPass()
    {
      FFirstPassItems = FItems;
      FItems = new List<BookmarkItem>();
    }

    public void Save(XmlItem rootItem)
    {
      rootItem.Clear();
      foreach (BookmarkItem item in FItems)
      {
        XmlItem xi = rootItem.Add();
        xi.Name = "item";
        xi.SetProp("Name", item.Name);
        xi.SetProp("Page", item.PageNo.ToString());
        xi.SetProp("Offset", Converter.ToString(item.OffsetY));
      }
    }

    public void Load(XmlItem rootItem)
    {
      Clear();
      for (int i = 0; i < rootItem.Count; i++)
      {
        XmlItem item = rootItem[i];
        Add(item.GetProp("Name"), int.Parse(item.GetProp("Page")),
          (float)Converter.FromString(typeof(float), item.GetProp("Offset")));
      }
    }
    
    public Bookmarks()
    {
      FItems = new List<BookmarkItem>();
    }
    
    
    internal class BookmarkItem
    {
      public string Name;
      public int PageNo;
      public float OffsetY;
    }
  }
}
