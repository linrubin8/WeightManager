using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Design.PageDesigners.Page
{
  internal class Grid
  {
    private PageUnits FGridUnits;
    private bool FDotted;
    private float FSnapSizeMillimeters;
    private float FSnapSizeCentimeters;
    private float FSnapSizeInches;
    private float FSnapSizeHundrethsOfInch;
    private float FSnapSize;
    private Bitmap FGridBmp;

    public PageUnits GridUnits
    {
      get { return FGridUnits; }
      set
      {
        FGridUnits = value;
        UpdateGridSize();
      }
    }

    public bool Dotted
    {
      get { return FDotted; }
      set { FDotted = value; }
    }
    
    public float SnapSize
    {
      get { return FSnapSize; }
      set
      {
        switch (GridUnits)
        {
          case PageUnits.Millimeters:
            SnapSizeMillimeters = value;
            break;
          case PageUnits.Centimeters:
            SnapSizeCentimeters = value;
            break;
          case PageUnits.Inches:
            SnapSizeInches = value;
            break;
          case PageUnits.HundrethsOfInch:
            SnapSizeHundrethsOfInch = value;
            break;    
        }
      }
    }

    public float SnapSizeMillimeters
    {
      get { return FSnapSizeMillimeters; }
      set 
      {
        FSnapSizeMillimeters = value;
        UpdateGridSize();
      }
    }

    public float SnapSizeCentimeters
    {
      get { return FSnapSizeCentimeters; }
      set
      {
        FSnapSizeCentimeters = value;
        UpdateGridSize();
      }
    }

    public float SnapSizeInches
    {
      get { return FSnapSizeInches; }
      set
      {
        FSnapSizeInches = value;
        UpdateGridSize();
      }
    }

    public float SnapSizeHundrethsOfInch
    {
      get { return FSnapSizeHundrethsOfInch; }
      set
      {
        FSnapSizeHundrethsOfInch = value;
        UpdateGridSize();
      }
    }

    private void UpdateGridSize()
    {
      switch (FGridUnits)
      {
        case PageUnits.Millimeters:
          FSnapSize = FSnapSizeMillimeters * Units.Millimeters;
          break;
        case PageUnits.Centimeters:
          FSnapSize = FSnapSizeCentimeters * Units.Centimeters;
          break;
        case PageUnits.Inches:
          FSnapSize = FSnapSizeInches * Units.Inches;
          break;
        case PageUnits.HundrethsOfInch:
          FSnapSize = FSnapSizeHundrethsOfInch * Units.HundrethsOfInch;
          break;
      }
      ResetGridBmp();
    }
    
    private void ResetGridBmp()
    {
      FGridBmp = new Bitmap(1, 1);
    }

    private void DrawLinesGrid(Graphics g, RectangleF visibleArea, float scale)
    {
      Pen linePen;
      Pen pen5 = new Pen(Color.FromArgb(255, 0xF8, 0xF8, 0xF8));
      Pen pen10 = new Pen(Color.FromArgb(255, 0xE8, 0xE8, 0xE8));

      float dx = GridUnits == PageUnits.Millimeters || GridUnits == PageUnits.Centimeters ? 
        Units.Millimeters * scale : Units.TenthsOfInch * scale;
      float i = visibleArea.Left;
      int i1 = 0;

      while (i < visibleArea.Right)
      {
        if (i1 % 10 == 0)
          linePen = pen10;
        else if (i1 % 5 == 0)
          linePen = pen5;
        else
          linePen = null;
        if (linePen != null)
          g.DrawLine(linePen, i, visibleArea.Top, i, visibleArea.Bottom);
        i += dx;
        i1++;
      }

      i = visibleArea.Top;
      i1 = 0;
      while (i < visibleArea.Bottom)
      {
        if (i1 % 10 == 0)
          linePen = pen10;
        else if (i1 % 5 == 0)
          linePen = pen5;
        else
          linePen = null;
        if (linePen != null)
          g.DrawLine(linePen, visibleArea.Left, i, visibleArea.Right, i);
        i += dx;
        i1++;
      }

      pen5.Dispose();
      pen10.Dispose();
    }

    private void DrawDotGrid(Graphics g, RectangleF visibleArea, float scale)
    {
      float dx = FSnapSize * scale;
      float dy = dx;

      if (visibleArea.Width > 0 && visibleArea.Height > 0 && dx > 2 && dy > 2)
      {
        float i = 0;
        if (FGridBmp.Width != (int)visibleArea.Width)
        {
          FGridBmp = new Bitmap((int)visibleArea.Width, 1);
          // draw points on one line
          i = 0;
          while (i < (int)visibleArea.Width - 1)
          {
            FGridBmp.SetPixel((int)Math.Round(i), 0, Color.Silver);
            i += dx;
          }
        }

        // draw lines
        i = visibleArea.Top;
        while (i < visibleArea.Bottom - 1)
        {
          g.DrawImage(FGridBmp, (int)Math.Round(visibleArea.Left), (int)Math.Round(i));
          i += dy;
        }
      }
    }
    
    public void Draw(Graphics g, RectangleF visibleArea, float scale)
    {
      if (FDotted)
        DrawDotGrid(g, visibleArea, scale);
      else
        DrawLinesGrid(g, visibleArea, scale);
    }
    
    public Grid()
    {
      FGridUnits = PageUnits.Centimeters;
      FDotted = true;
      FSnapSizeMillimeters = 2.5f;
      FSnapSizeCentimeters = 0.25f;
      FSnapSizeInches = 0.1f;
      FSnapSizeHundrethsOfInch = 10f;
      UpdateGridSize();
    }
  }
}
