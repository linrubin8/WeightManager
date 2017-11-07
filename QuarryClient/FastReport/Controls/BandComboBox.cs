using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class BandComboBox : ComboBox
  {
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();

      if (e.Index < 0)
        return;
      BandBase c = Items[e.Index] as BandBase;
      Image img = c == null ? Res.GetImage(76) : RegisteredObjects.FindObject(c).Image;
      e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y);
      using (Brush b = new SolidBrush(e.ForeColor))
      {
        string text = "";
        if (c == null)
          text = Res.Get("Misc,None");
        else
        {
          text = c.Name;
          if (c is DataBand)
            text += ": " + c.GetInfoText();
          else if (c is DataFooterBand)
            text += ": " + c.DataBand.GetInfoText();
          else if (c is GroupFooterBand)
            text += ": " + (c.Parent as GroupHeaderBand).GetInfoText();
        }
        e.Graphics.DrawString(text, e.Font, b,
          e.Bounds.X + 24, e.Bounds.Y + (e.Bounds.Height - ItemHeight) / 2);
      }
    }

    public BandComboBox()
    {
      DrawMode = DrawMode.OwnerDrawFixed;
      ItemHeight = DrawUtils.DefaultItemHeight + 2;
    }
  }
}
