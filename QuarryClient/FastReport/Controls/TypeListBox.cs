using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Controls
{
  internal class TypeListBox : ListBox
  {
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();
      if (e.Index >= 0)
      {
        using (SolidBrush b = new SolidBrush(e.ForeColor))
        {
          Type type = Items[e.Index] as Type;
          e.Graphics.DrawImage(Res.GetImage(DataTreeHelper.GetTypeImageIndex(type)), e.Bounds.Location);
          e.Graphics.DrawString(type.Name, e.Font, b, e.Bounds.X + 20, e.Bounds.Y);
        }
      }
    }

    public TypeListBox()
    {
      Type[] types = new Type[] { 
        typeof(Boolean),
        typeof(Byte),
        typeof(Char),
        typeof(DateTime),
        typeof(Decimal),
        typeof(Double),
        typeof(Int16),
        typeof(Int32),
        typeof(Int64),
        typeof(SByte),
        typeof(Single),
        typeof(String),
        typeof(TimeSpan), 
        typeof(UInt16),
        typeof(UInt32),
        typeof(UInt64),
        typeof(Byte[]) };
      DrawMode = DrawMode.OwnerDrawFixed;
      ItemHeight = 16;
      Items.AddRange(types);
    }
  }
}
