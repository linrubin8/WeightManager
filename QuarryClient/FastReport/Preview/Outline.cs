using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Preview
{
  internal class Outline
  {
    private XmlItem FRootItem;
    private XmlItem FCurItem;
    private int FFirstPassPosition;

    public XmlItem Xml
    {
      get { return FRootItem; }
      set
      {
        FRootItem = value;
        FCurItem = FRootItem;
        value.Parent = null;
      }
    }
    
    internal bool IsEmpty
    {
      get { return FRootItem.Count == 0; }
    }
    
    internal XmlItem CurPosition
    {
      get { return FCurItem.Count == 0 ? null : FCurItem[FCurItem.Count - 1]; }
    }

    private void EnumItems(XmlItem item, float shift)
    {
      int pageNo = int.Parse(item.GetProp("Page"));
      float offset = Converter.StringToFloat(item.GetProp("Offset"));
      item.SetProp("Page", Converter.ToString(pageNo + 1));
      item.SetProp("Offset", Converter.ToString(offset + shift));
      
      for (int i = 0; i < item.Count; i++)
      {
        EnumItems(item[i], shift);
      }
    }

    internal void Shift(XmlItem from, float newY)
    {
      if (from == null || from.Parent == null)
        return;
      int i = from.Parent.IndexOf(from);
      if (i + 1 >= from.Parent.Count)
        return;
      from = from.Parent[i + 1];

      float topY = Converter.StringToFloat(from.GetProp("Offset"));
      EnumItems(from, newY - topY);
    }
    
    public void Add(string text, int pageNo, float offsetY)
    {
      FCurItem = FCurItem.Add();
      FCurItem.Name = "item";
      FCurItem.SetProp("Text", text);
      FCurItem.SetProp("Page", pageNo.ToString());
      FCurItem.SetProp("Offset", Converter.ToString(offsetY));
    }
    
    public void LevelRoot()
    {
      FCurItem = FRootItem;
    }
    
    public void LevelUp()
    {
      if (FCurItem != FRootItem)
        FCurItem = FCurItem.Parent;
    }

    public void Clear()
    {
      Clear(-1);
    }

    private void Clear(int position)
    {
      if (position == -1)
        FRootItem.Clear();
      else if (position < FRootItem.Count)
        FRootItem.Items[position].Dispose();

      LevelRoot();
    }

    public void PrepareToFirstPass()
    {
      FFirstPassPosition = FRootItem.Count == 0 ? -1 : FRootItem.Count;
      LevelRoot();
    }

    public void ClearFirstPass()
    {
      Clear(FFirstPassPosition);
    }

    public Outline()
    {
      FRootItem = new XmlItem();
      FRootItem.Name = "outline";
      LevelRoot();
    }
  }
}
