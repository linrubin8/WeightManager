using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Design.PageDesigners.Dialog
{
  internal class Grid
  {
    private int FSnapSize;
    private Bitmap FGridBmp;

    public int SnapSize
    {
      get { return FSnapSize; }
      set 
      {
        FSnapSize = value;
        ResetGridBmp();
      }
    }

    private void ResetGridBmp()
    {
      FGridBmp = new Bitmap(1, 1);
    }

    public void Draw(Graphics g, Rectangle visibleArea)
    {
      if (visibleArea.Width > 0 && visibleArea.Height > 0 && SnapSize > 2)
      {
        int i = 0;
        if (FGridBmp.Width != visibleArea.Width)
        {
          FGridBmp = new Bitmap(visibleArea.Width, 1);
          // draw points on one line
          i = 0;
          while (i < visibleArea.Width)
          {
            FGridBmp.SetPixel(i, 0, Color.Gray);
            i += SnapSize;
          }
        }

        // draw lines
        i = visibleArea.Top;
        while (i < visibleArea.Bottom)
        {
          g.DrawImage(FGridBmp, visibleArea.Left, i);
          i += SnapSize;
        }
      }
    }

    public Grid()
    {
      SnapSize = 8;
    }
  }
}
