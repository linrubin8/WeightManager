using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class ComponentRefComboBox : ComboBox
  {
    public Base SelectedObject
    {
      get { return SelectedIndex <= 0 ? null : Items[SelectedIndex] as Base; }
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();

      if (e.Index >= 0)
      {
        Base c = Items[e.Index] as Base;
        Image img = c == null ? Res.GetImage(76) : RegisteredObjects.FindObject(c).Image;
        e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y + 1);
        using (Brush b = new SolidBrush(e.ForeColor))
        {
          e.Graphics.DrawString(c == null ? Res.Get("Misc,None") : c.Name, e.Font, b,
            e.Bounds.X + 24, e.Bounds.Y + (e.Bounds.Height - DrawUtils.DefaultItemHeight) / 2);
        }
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

    public ComponentRefComboBox()
    {
      ItemHeight = 19;
      DropDownStyle = ComboBoxStyle.DropDownList;
      DrawMode = DrawMode.OwnerDrawFixed;
    }
  }
}
