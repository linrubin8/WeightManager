using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class ComponentRefListBox : ListBox
  {
    public Base SelectedObject
    {
      get { return SelectedIndex <= 0 ? null : Items[SelectedIndex] as Base; }
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();

      Base c = Items[e.Index] as Base;
      Image img = c == null ? Res.GetImage(76) : RegisteredObjects.FindObject(c).Image;
      e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y + 1);
      using (Brush b = new SolidBrush(e.ForeColor))
      {
        e.Graphics.DrawString(c == null ? Res.Get("Misc,None") : c.Name, e.Font, b,
          e.Bounds.X + 24, e.Bounds.Y + (e.Bounds.Height - DrawUtils.DefaultItemHeight) / 2);
      }    
    }
    
    public void PopulateList(Base parent, Type componentType, object instance)
    {
      Items.Clear();
      Items.Add(0);

      FastReport.ObjectCollection list = parent.AllObjects;
      foreach (Base c in list)
      {
        if (c != instance && (c.GetType() == componentType || c.GetType().IsSubclassOf(componentType)))
          Items.Add(c);
      }
    }

    public ComponentRefListBox()
    {
      ItemHeight = 19;
      BorderStyle = BorderStyle.None;
      DrawMode = DrawMode.OwnerDrawFixed;
      Height = 8 * ItemHeight;
    }
  }
}
