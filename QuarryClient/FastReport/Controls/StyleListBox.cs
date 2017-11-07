using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class StyleListBox : ListBox
  {
    private bool FUpdating;
    private StyleCollection FStyles;

    public event EventHandler StyleSelected;
    
    public string Style
    {
      get 
      { 
        if (SelectedIndex < 1)
          return "";
        return (string)Items[SelectedIndex];
      }
      set 
      { 
        FUpdating = true;
        int i = Items.IndexOf(value);
        SelectedIndex = i != -1 ? i : 0;
        FUpdating = false;
      }
    }
    
    public StyleCollection Styles
    {
      get { return FStyles; }
      set
      {
        FStyles = value;
        if (value != null)
          UpdateItems();
      }
    }
    
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();
      Graphics g = e.Graphics;
      if (e.Index >= 0)
      {
        string name = (string)Items[e.Index];
        using (TextObject sample = new TextObject())
        {
          sample.Bounds = new RectangleF(e.Bounds.Left + 2, e.Bounds.Top + 2, e.Bounds.Width - 4, e.Bounds.Height - 4);
          sample.Text = name;
          sample.HorzAlign = HorzAlign.Center;
          sample.VertAlign = VertAlign.Center;
          if (FStyles != null)
          {
            int index = FStyles.IndexOf(name);
            if (index != -1)
              sample.ApplyStyle(FStyles[index]);
          }  
          using (GraphicCache cache = new GraphicCache())
          {
            sample.Draw(new FRPaintEventArgs(g, 1, 1, cache));
          }
        }
      }
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);
      if (FUpdating)
        return;
      if (StyleSelected != null)
        StyleSelected(this, EventArgs.Empty);
    }
    
    private void UpdateItems()
    {
      Items.Clear();
      Items.Add(Res.Get("Designer,Toolbar,Style,NoStyle"));
      foreach (Style s in FStyles)
      {
        Items.Add(s.Name);
      }
    }

    public StyleListBox()
    {
      DrawMode = DrawMode.OwnerDrawFixed;
      ItemHeight = 32;
      IntegralHeight = false;
      Size = new Size(150, 300);
    }
  }  
}
