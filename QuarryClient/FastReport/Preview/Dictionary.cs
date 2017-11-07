using FastReport.Table;
using FastReport.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FastReport.Preview
{
    internal class Dictionary
  {
    private SortedList<string, DictionaryItem> FNames;
    //private SortedDictionary<string, DictionaryItem> FNames;    
    private Hashtable FBaseNames;
    private PreparedPages FPreparedPages;

    private void AddBaseName(string name)
    {
      for (int i = 0; i < name.Length; i++)
      {
        if (name[i] >= '0' && name[i] <= '9')
        {
          string baseName = name.Substring(0, i);
          int num = int.Parse(name.Substring(i));
          if (FBaseNames.ContainsKey(baseName))
          {
            int maxNum = (int)FBaseNames[baseName];
            if (num < maxNum)
              num = maxNum;
          }
          FBaseNames[baseName] = num;
          return;
        }
      }
    }

    public string CreateUniqueName(string baseName)
    {
      int num = 1;
      if (FBaseNames.ContainsKey(baseName))
        num = (int)FBaseNames[baseName] + 1;
      FBaseNames[baseName] = num;
      return baseName + num.ToString();
    }

    private void Add(string name, string sourceName, Base obj)
    {
      FNames.Add(name, new DictionaryItem(sourceName, obj));
    }

    public string AddUnique(string baseName, string sourceName, Base obj)
    {
      string result = CreateUniqueName(baseName);
      Add(result, sourceName, obj);
      return result;
    }

    public Base GetObject(string name)
    {
        DictionaryItem item;
        if (FNames.TryGetValue(name, out item))
            return item.CloneObject(name);
        else
            return null;

      //int i = FNames.IndexOfKey(name);
      //if (i == -1)
      //  return null;
      //return FNames.Values[i].CloneObject(name);
    }

    public Base GetOriginalObject(string name)
    {
        DictionaryItem item;
        if (FNames.TryGetValue(name, out item))
            return item.OriginalComponent;
        else
            return null;
        
      //  int i = FNames.IndexOfKey(name);
      //if (i == -1)
      //  return null;
      //return FNames.Values[i].OriginalComponent;
    }

    public void Clear()
    {
      FNames.Clear();
      FBaseNames.Clear();
    }

    public void Save(XmlItem rootItem)
    {
        rootItem.Clear();
        foreach (KeyValuePair<string, DictionaryItem> pair in FNames)
        {
            XmlItem xi = rootItem.Add();
            xi.Name = pair.Key;
                xi.ClearProps();
                xi.SetProp("name", pair.Value.SourceName);
            //xi.Text = String.Concat("name=\"", pair.Value.SourceName, "\"");
        }

      //for (int i = 0; i < FNames.Count; i++)
      //{
      //  XmlItem xi = rootItem.Add();
      //  xi.Name = FNames.Keys[i];
      //  xi.Text = "name=\"" + FNames.Values[i].SourceName + "\"";
      //}
    }

    public void Load(XmlItem rootItem)
    {
      Clear();
      for (int i = 0; i < rootItem.Count; i++)
      {
        // rootItem[i].Name is 's1', rootItem[i].Text is 'name="Page0.Shape1"'
        string sourceName = rootItem[i].GetProp("name");
        // split to Page0, Shape1
        string[] objName = sourceName.Split('.');
        // get page number
        int pageN = int.Parse(objName[0].Substring(4));
        // get the object
        Base obj = null;
        if (objName.Length == 2)
          obj = FPreparedPages.SourcePages[pageN].FindObject(objName[1]);
        else
          obj = FPreparedPages.SourcePages[pageN];
        
        // add s1, Page0.Shape1, object
        string name = rootItem[i].Name;
        Add(name, sourceName, obj);
        AddBaseName(name);
      }
    }
    
    public Dictionary(PreparedPages preparedPages)
    {
      FPreparedPages = preparedPages;
      FNames = new SortedList<string, DictionaryItem>(); 
      //FNames = new SortedDictionary<string, DictionaryItem>(); 
      FBaseNames = new Hashtable();
    }

    private class DictionaryItem
    {
      private string FSourceName;
      private Base FOriginalComponent;
      
      public string SourceName
      {
        get { return FSourceName; }
      }
      
      public Base OriginalComponent
      {
        get { return FOriginalComponent; }
      }
      
      public Base CloneObject(string alias)
      {
        Base result = null;
        Type type = FOriginalComponent.GetType();

        // try frequently used objects first. The CreateInstance method is very slow.
        if (type == typeof(TextObject))
          result = new TextObject();
        else if (type == typeof(TableCell))
          result = new TableCell();
        else if (type == typeof(DataBand))
          result = new DataBand();
        else
          result = Activator.CreateInstance(type) as Base;
        
        result.Assign(FOriginalComponent);
        result.OriginalComponent = FOriginalComponent;
        result.Alias = alias;
        result.SetName(FOriginalComponent.Name);
        if (result is ReportComponentBase)
          (result as ReportComponentBase).AssignPreviewEvents(FOriginalComponent);
        return result;
      }

      public DictionaryItem(string name, Base obj)
      {
        FSourceName = name;
        FOriginalComponent = obj;
      }
    }
  }
}
