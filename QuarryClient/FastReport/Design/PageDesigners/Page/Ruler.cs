using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Design.PageDesigners.Page
{
  internal class RulerBase : Control
  {
    public bool AllowPaint = true;

    private ReportPageDesigner FPageDesigner;
    private float FOffset;
    private const int WM_PAINT = 0x000F;

    public float Offset
    {
      get { return FOffset; }
      set { FOffset = value; }
    }

    public ReportWorkspace Workspace
    {
      get { return FPageDesigner.Workspace; }
    }

    public ReportPageDesigner PageDesigner
    {
      get { return FPageDesigner; }
    }

    public Designer Designer
    {
      get { return FPageDesigner.Designer; }
    }

    protected bool CheckGridStep(ref float kx, ref float ky)
    {
      bool al = ReportWorkspace.SnapToGrid;
      if (ModifierKeys == Keys.Alt)
        al = !al;

      bool result = true;
      float grid = ReportWorkspace.Grid.SnapSize;
      if (al)
      {
        result = kx >= grid || kx <= -grid || ky >= grid || ky <= -grid;
        if (result)
        {
          kx = (int)(kx / grid) * grid;
          ky = (int)(ky / grid) * grid;
        }
      }
      return result;
    }

    protected override void WndProc(ref Message m)
    {
        if ((m.Msg != WM_PAINT) || (AllowPaint && m.Msg == WM_PAINT))
        {
            base.WndProc(ref m);
        }
    }

    public RulerBase(ReportPageDesigner pd) : base()
    {
      FPageDesigner = pd;
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }
  }

  internal class HorzRuler : RulerBase
  {
    private PointF FLastMousePoint;
    private bool FMouseDown;
    private bool FMouseMoved;
    private int FActiveGuide;

    private void DrawRuler(Graphics g, float start, float size)
    {
      Font font = new Font("Tahoma", 6 * 96f / DrawUtils.ScreenDpi);
      Brush brush = SystemBrushes.WindowText;
      Pen pen = SystemPens.WindowText;
      int w5 = 5;
      int w10 = 10;
      float dx = ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters || ReportWorkspace.Grid.GridUnits == PageUnits.Centimeters ?
        Units.Millimeters * ReportWorkspace.Scale :
        Units.TenthsOfInch * ReportWorkspace.Scale;
      string s = (((int)Math.Round(size / dx)) / 10).ToString();
      float maxw = g.MeasureString(s, font).Width;
      float i = start;
      int i1 = 0;

      while (i < start + size)
      {
        int h = 0;
        if (i1 == 0)
          h = 0;
        else if (i1 % w10 == 0)
          h = 6;
        else if (i1 % w5 == 0)
          h = 4;
        else
          h = 2;

        if (h == 2 && dx * w10 < 41)
          h = 0;
        if (h == 4 && dx * w10 < 21)
          h = 0;

        int w = 0;
        if (h == 6)
        {
          if (maxw > dx * w10 * 1.5f)
            w = w10 * 4;
          else if (maxw > dx * w10 * 0.7f)
            w = w10 * 2;
          else
            w = w10;
        }

        if (w != 0 && i1 % w == 0)
        {
          if (ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters)
            s = (i1).ToString();
          else if (ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch)
            s = (i1 * 10).ToString();
          else
            s = (i1 / 10).ToString();
          SizeF sz = g.MeasureString(s, font);
          g.DrawString(s, font, brush, new PointF(Offset + i - sz.Width / 2 + 1, 7));
        }
        else if (h != 0)
        {
          g.DrawLine(pen, Offset + i, 6 + (13 - h) / 2, Offset + i, 6 + (13 - h) / 2 + h - 1);
        }
        i += dx;
        i1++;
      }
    }

    private void DrawRulerRtl(Graphics g, float start, float size)
    {
      Font font = new Font("Tahoma", 6 * 96f / DrawUtils.ScreenDpi);
      Brush brush = SystemBrushes.WindowText;
      Pen pen = SystemPens.WindowText;
      int w5 = 5;
      int w10 = 10;
      float dx = ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters || ReportWorkspace.Grid.GridUnits == PageUnits.Centimeters ?
        Units.Millimeters * ReportWorkspace.Scale :
        Units.TenthsOfInch * ReportWorkspace.Scale;
      string s = (((int)Math.Round(size / dx)) / 10).ToString();
      float maxw = g.MeasureString(s, font).Width;
      float i = start + size;
      int i1 = 0;

      while (i > start)
      {
        int h = 0;
        if (i1 == 0)
          h = 0;
        else if (i1 % w10 == 0)
          h = 6;
        else if (i1 % w5 == 0)
          h = 4;
        else
          h = 2;

        if (h == 2 && dx * w10 < 41)
          h = 0;
        if (h == 4 && dx * w10 < 21)
          h = 0;

        int w = 0;
        if (h == 6)
        {
          if (maxw > dx * w10 * 1.5f)
            w = w10 * 4;
          else if (maxw > dx * w10 * 0.7f)
            w = w10 * 2;
          else
            w = w10;
        }

        if (w != 0 && i1 % w == 0)
        {
          if (ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters)
            s = (i1).ToString();
          else if (ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch)
            s = (i1 * 10).ToString();
          else  
            s = (i1 / 10).ToString();
          SizeF sz = g.MeasureString(s, font);
          g.DrawString(s, font, brush, new PointF(Offset + i - sz.Width / 2 + 1, 7));
        }    
        else if (h != 0)
        {
          g.DrawLine(pen, Offset + i, 6 + (13 - h) / 2, Offset + i, 6 + (13 - h) / 2 + h - 1);
        }
        i -= dx;
        i1++;
      }
    }
    
    private void DrawGuides(Graphics g)
    {
      FloatCollection guides = Workspace.Page.Guides;
      if (guides == null)
        return;
      for (int i = 0; i < guides.Count; i++)
      {
        Bitmap b = Res.GetImage(i == FActiveGuide ? 174 : 74);
        g.DrawImage(b, (int)Math.Round(Offset + guides[i] * ReportWorkspace.Scale - 4), 16);
      }
    }

    private void MoveGuide(float kx)
    {
      Workspace.Guides.MoveVGuide(FActiveGuide, kx);
      float f = Workspace.Page.Guides[FActiveGuide];
      f += kx;
      Workspace.Page.Guides[FActiveGuide] = Converter.DecreasePrecision(f, 2);
      Workspace.Refresh();
      Refresh();
    }

    private void FixGuide(bool remove)
    {
      float f = Workspace.Page.Guides[FActiveGuide];
      if (remove || f < 0 || f > Workspace.Width / ReportWorkspace.Scale)
        Workspace.Page.Guides.RemoveAt(FActiveGuide);
      FActiveGuide = -1;
      Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (Workspace.Locked)
        return;
      Graphics g = e.Graphics;
      g.SetClip(new RectangleF(Height, 0, Width - Height, Height));
      g.FillRectangle(SystemBrushes.Window, new RectangleF(Offset, 5, Workspace.Width, Height - 10));
      if (Config.RightToLeft)
      {
          DrawRulerRtl(g, 0, Workspace.Width);
      }
      else
      {
          DrawRuler(g, 0, Workspace.Width);
      }
      DrawGuides(g);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (Workspace.Locked)
        return;

      float scale = ReportWorkspace.Scale;
      FLastMousePoint = new PointF(e.X / scale, e.Y / scale);
      FMouseDown = true;
      FMouseMoved = false;
      if (FActiveGuide != -1)
        Workspace.Guides.BeforeMoveVGuide(FActiveGuide);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (Workspace.Locked)
        return;

      float scale = ReportWorkspace.Scale;
      if (e.Button == MouseButtons.None)
      {
        // find guide
        FloatCollection guides = Workspace.Page.Guides;
        if (guides == null)
          return;
        float x = (e.X - Offset) / scale;
        FActiveGuide = -1;
        for (int i = 0; i < guides.Count; i++)
        {
          if (x > guides[i] - 5 && x < guides[i] + 5)
          {
            FActiveGuide = i;
            break;
          }
        }
        Refresh();
      }
      else if (e.Button == MouseButtons.Left)
      {
        if (FActiveGuide == -1)
          return;
        float kx = e.X / scale - FLastMousePoint.X;
        float ky = e.Y / scale - FLastMousePoint.Y;
        if (!CheckGridStep(ref kx, ref ky))
          return;
        FMouseMoved = true;
        MoveGuide(kx);
        FLastMousePoint.X += kx;
        FLastMousePoint.Y += ky;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (Workspace.Locked || !FMouseDown)
        return;

      FMouseDown = false;
      if (FMouseMoved)
      {
        FixGuide(e.Y < -5 || e.Y > Height + 5);
        Workspace.Designer.SetModified(null, "MoveGuide");
      }
      else
      {
        // create new guide
        float x = (e.X - Offset) / ReportWorkspace.Scale;
        if (x < Workspace.Width && e.Y > 5 && e.Y < 20)
        {
          if (ReportWorkspace.SnapToGrid)
            x = (int)(x / ReportWorkspace.Grid.SnapSize) * ReportWorkspace.Grid.SnapSize;
          if (Workspace.Page.Guides == null)
            Workspace.Page.Guides = new FloatCollection();
          Workspace.Page.Guides.Add(x);
          Workspace.Designer.SetModified(null, "AddGuide");
        }
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (Workspace.Locked)
        return;

      if (!FMouseDown && FActiveGuide != -1)
      {
        FActiveGuide = -1;
        Refresh();
      }
    }

    public HorzRuler(ReportPageDesigner pd) : base(pd)
    {
      FActiveGuide = -1;
    }
  }


  internal class VertRuler : RulerBase
  {
    private PointF FLastMousePoint;
    private bool FMouseDown;
    private bool FMouseMoved;
    private int FActiveGuide;
    private BandBase FActiveBand;
    private bool FResizing;

    private void DrawRuler(Graphics g, float start, float size)
    {
      Font font = new Font("Tahoma", 6 * 96f / DrawUtils.ScreenDpi);
      Brush brush = SystemBrushes.WindowText;
      Pen pen = SystemPens.WindowText;
      int w5 = 5;
      int w10 = 10;
      float dx = ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters || ReportWorkspace.Grid.GridUnits == PageUnits.Centimeters ?
        Units.Millimeters * ReportWorkspace.Scale :
        Units.TenthsOfInch * ReportWorkspace.Scale;
      string s = (((int)Math.Round(size / dx)) / 10).ToString();
      float maxw = g.MeasureString(s, font).Width;
      float i = start;
      int i1 = 0;

      while (i < start + size)
      {
        int h = 0;
        if (i1 == 0)
          h = 0;
        else if (i1 % w10 == 0)
          h = 6;
        else if (i1 % w5 == 0)
          h = 4;
        else
          h = 2;

        if (h == 2 && dx * w10 < 41)
          h = 0;
        if (h == 4 && dx * w10 < 21)
          h = 0;

        int w = 0;
        if (h == 6)
        {
          if (maxw > dx * w10 * 1.5f)
            w = w10 * 4;
          else if (maxw > dx * w10 * 0.7f)
            w = w10 * 2;
          else
            w = w10;
        }

        if (w != 0 && i1 % w == 0)
        {
          if (ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters)
            s = (i1).ToString();
          else if (ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch)
            s = (i1 * 10).ToString();
          else
            s = (i1 / 10).ToString();
          SizeF sz = g.MeasureString(s, font);
          PointF p = new PointF(Width - sz.Height - 7, Offset + i + sz.Width / 2);
          GraphicsState state = g.Save();
          g.TranslateTransform(p.X + sz.Height / 2, p.Y + sz.Width / 2);
          g.RotateTransform(-90);
          p.X = sz.Width / 2;
          p.Y = -sz.Height / 2;
          g.DrawString(s, font, brush, p);
          g.Restore(state);
        }
        else if (h != 0)
        {
          g.DrawLine(pen, 6 + (13 - h) / 2, Offset + i, 6 + (13 - h) / 2 + h - 1, Offset + i);
        }
        i += dx;
        i1++;
      }
    }

    private void DrawGuides(Graphics g, BandBase band)
    {
      FloatCollection guides = band.Guides;
      if (guides == null)
        return;
      for (int i = 0; i < guides.Count; i++)
      {
        Bitmap b = Res.GetImage(band == FActiveBand && i == FActiveGuide ? 173 : 73);
        g.DrawImage(b, 16, (int)Math.Round(Offset + (band.Top + guides[i]) * ReportWorkspace.Scale - 4));
      }
    }

    private BandCollection GetBands()
    {
      BandCollection bands = new BandCollection();
      ObjectCollection objects = PageDesigner.Page.AllObjects;

      foreach (Base c in objects)
      {
        if (c is BandBase)
          bands.Add(c as BandBase);
      }
      return bands;
    }

    private BandBase BandAt(float y)
    {
      BandCollection bands = GetBands();
      foreach (BandBase b in bands)
      {
        if (y >= b.Top && y <= b.Bottom + (ReportWorkspace.ClassicView ? BandBase.HeaderSize : 4))
          return b;
      }
      return null;
    }

    private void MoveGuide(float ky)
    {
      Workspace.Guides.MoveHGuide(FActiveBand, FActiveGuide, ky);
      float f = FActiveBand.Guides[FActiveGuide];
      f += ky;
      FActiveBand.Guides[FActiveGuide] = Converter.DecreasePrecision(f, 2);
      Workspace.Refresh();
      Refresh();
    }

    private void ResizeBand(float ky)
    {
      FActiveBand.Height += ky;
      FActiveBand.FixHeight();
      Workspace.UpdateBands();
    }

    private void FixGuide(bool remove)
    {
      float f = FActiveBand.Guides[FActiveGuide];
      if (remove || f < 0 || f > FActiveBand.Height)
        FActiveBand.Guides.RemoveAt(FActiveGuide);
      FActiveGuide = -1;
      Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (Workspace.Locked)
        return;
      Graphics g = e.Graphics;

      float scale = ReportWorkspace.Scale;
      BandCollection bands = GetBands();
      foreach (BandBase b in bands)
      {
        g.FillRectangle(SystemBrushes.Window,
          new RectangleF(5, Offset + b.Top * scale, Width - 10, b.Height * scale + 1));
        DrawRuler(g, b.Top * scale, b.Height * scale);
        DrawGuides(g, b);

        if (ReportWorkspace.ClassicView)
        {
          RectangleF fillRect = new RectangleF(5, Offset + (b.Top - (BandBase.HeaderSize - 1)) * scale,
            Width - 10, (BandBase.HeaderSize - 1) * scale);
          if (b.Top == BandBase.HeaderSize)
          {
            fillRect.Y = 0;
            fillRect.Height += scale;
          }
          b.DrawBandHeader(g, fillRect, true);

          if (b.Top > BandBase.HeaderSize)
          {
            // draw splitter lines
            float lineY = fillRect.Top + fillRect.Height / 2 - 2;
            for (int i = 0; i < 6; i += 2)
            {
              g.DrawLine(SystemPens.ControlDarkDark, 9, lineY + i, Width - 10, lineY + i);
            }
          }
        }
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (Workspace.Locked)
        return;

      float scale = ReportWorkspace.Scale;
      FLastMousePoint = new PointF(e.X / scale, e.Y / scale);
      FMouseDown = true;
      FMouseMoved = false;
      if (FActiveBand != null && FActiveGuide != -1)
        Workspace.Guides.BeforeMoveHGuide(FActiveBand, FActiveGuide);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (Workspace.Locked)
        return;

      float scale = ReportWorkspace.Scale;
      if (e.Button == MouseButtons.None)
      {
        // find band
        float y = (e.Y - Offset) / scale;
        Cursor = Cursors.Default;
        FResizing = false;
        FActiveGuide = -1;
        FActiveBand = BandAt(y);
        if (FActiveBand != null)
        {
          // check band resize
          if (y > FActiveBand.Bottom - 1 &&
            y < FActiveBand.Bottom + (ReportWorkspace.ClassicView ? BandBase.HeaderSize : 4))
          {
            FResizing = true;
            Cursor = Cursors.HSplit;
          }
          else
          {
            // check guides
            FloatCollection guides = FActiveBand.Guides;
            if (guides != null)
            {
              for (int i = 0; i < guides.Count; i++)
              {
                if (y > FActiveBand.Top + guides[i] - 5 &&
                  y < FActiveBand.Top + guides[i] + 5)
                {
                  FActiveGuide = i;
                  break;
                }
              }
            }
          }
        }
        Refresh();
      }
      else if (e.Button == MouseButtons.Left)
      {
        float kx = e.X / scale - FLastMousePoint.X;
        float ky = e.Y / scale - FLastMousePoint.Y;
        if (!CheckGridStep(ref kx, ref ky))
          return;

        if (FActiveBand != null)
        {
          if (FResizing)
          {
            FMouseMoved = true;
            ResizeBand(ky);
          }
          else if (FActiveGuide != -1)
          {
            FMouseMoved = true;
            MoveGuide(ky);
          }
        }
        FLastMousePoint.X += kx;
        FLastMousePoint.Y += ky;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (Workspace.Locked)
        return;

      FMouseDown = false;
      if (FMouseMoved)
      {
        if (FResizing)
          FActiveBand.FixHeight();
        else
          FixGuide(e.X < -5 || e.X > Width + 5);
        Workspace.Designer.SetModified(null, FResizing ? "ResizeBand" : "MoveGuide");
      }
      else
      {
        // create new guide
        if (e.X > 5 && e.X < 20)
        {
          float y = (e.Y - Offset) / ReportWorkspace.Scale;
          BandBase band = BandAt(y);
          if (band != null)
          {
            y = y - band.Top;
            if (ReportWorkspace.SnapToGrid)
              y = (int)(y / ReportWorkspace.Grid.SnapSize) * ReportWorkspace.Grid.SnapSize;
            if (band.Guides == null)
              band.Guides = new FloatCollection();
            band.Guides.Add(y);
            Workspace.Designer.SetModified(null, "AddGuide");
          }
        }
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (Workspace.Locked)
        return;

      if (!FMouseDown && FActiveGuide != -1)
      {
        FActiveGuide = -1;
        Refresh();
      }
    }

    public VertRuler(ReportPageDesigner pd) : base(pd)
    {
      FActiveGuide = -1;
    }
 }
}