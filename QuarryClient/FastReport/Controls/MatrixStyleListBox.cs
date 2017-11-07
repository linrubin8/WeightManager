using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Matrix;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class MatrixStyleListBox : ListBox
  {
    private bool FUpdating;
    private MatrixStyleSheet FStyles;

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
    
    public MatrixStyleSheet Styles
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
        int styleIndex = FStyles.IndexOf(name);
        if (styleIndex != -1)
          name = Res.Get("ComponentsMisc,Matrix," + name);

        Image img = styleIndex == -1 ? Res.GetImage(76) : Styles.GetStyleBitmap(styleIndex);
        e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y + 1);
        using (Brush b = new SolidBrush(e.ForeColor))
        {
          e.Graphics.DrawString(name, e.Font, b,
            e.Bounds.X + 24, e.Bounds.Y + (e.Bounds.Height - DrawUtils.DefaultItemHeight) / 2);
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
      foreach (StyleCollection s in FStyles)
      {
        Items.Add(s.Name);
      }
    }

    public MatrixStyleListBox()
    {
      DrawMode = DrawMode.OwnerDrawFixed;
      ItemHeight = 19;
      IntegralHeight = false;
      Size = new Size(150, 300);
    }
  }
}
