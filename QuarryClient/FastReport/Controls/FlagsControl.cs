using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class FlagsControl : CheckedListBox
  {
    private Type FEnumType;
    
    public Enum Flags
    {
      get 
      { 
        string s = "";
        foreach (object o in CheckedItems)
        {
          s += (string)o + ", ";
        }
        if (s != "")
        {
          s = s.Remove(s.Length - 2);
          return (Enum)Enum.Parse(FEnumType, s);
        }
        return (Enum)Enum.ToObject(FEnumType, 0);  
      }
      set 
      {
        FEnumType = value.GetType();
        string[] names = Enum.GetNames(FEnumType);
        Array values = Enum.GetValues(FEnumType);
        int enumValue = (int)Enum.ToObject(FEnumType, value);

        float maxWidth = 0;
        for (int i = 0; i < names.Length; i++)
        {
          int val = (int)values.GetValue(i);
          if (val != 0 && (val & 3) != 3)
          {
            Items.Add(names[i]);
            SetItemChecked(Items.Count - 1, (enumValue & val) != 0);
            float itemWidth = DrawUtils.MeasureString(names[i]).Width;
            if (itemWidth > maxWidth)
              maxWidth = itemWidth;
          }  
        }
        
        Width = (int)maxWidth + 20;
        Height = (Items.Count + 1) * (DrawUtils.DefaultItemHeight + 1);
      }
    }
    
    public FlagsControl()
    {
      BorderStyle = BorderStyle.None;
      Font = DrawUtils.DefaultFont;
      CheckOnClick = true;
    }
  }
}
