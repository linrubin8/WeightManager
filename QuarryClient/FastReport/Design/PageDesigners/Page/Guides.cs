using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Design.PageDesigners.Page
{
  internal class Guides
  {
    private ReportPageDesigner FPageDesigner;
    private SortedList<float, ComponentBase> FVirtualVGuides;
    private SortedList<float, ComponentBase> FVirtualHGuides;
    private List<RectangleF> FVirtualGuides;
    private List<LinkInfo> FGuideLinks;

    public ReportPage Page
    {
      get { return FPageDesigner.Page as ReportPage; }
    }

    public ReportWorkspace Workspace
    {
      get { return FPageDesigner.Workspace; }
    }
    
    public Designer Designer
    {
      get { return FPageDesigner.Designer; }
    }
    
    private void CheckVGuide(ref float kx, float coord)
    {
      if (Page.Guides == null)
        return;

      float closestGuide = float.PositiveInfinity;
      foreach (float f in Page.Guides)
      {
        if (Math.Abs(coord + kx - f) < Math.Abs(coord + kx - closestGuide))
          closestGuide = f;
      }

      if (Math.Abs(coord + kx - closestGuide) < ReportWorkspace.Grid.SnapSize * 2)
        kx = closestGuide - coord;
    }

    private void CheckHGuide(ref float ky, float coord, ComponentBase c)
    {
      BandBase band = c.Parent as BandBase;
      if (band == null || band.Guides == null)
        return;
      
      float closestGuide = float.PositiveInfinity;
      foreach (float f in band.Guides)
      {
        if (Math.Abs(coord + ky - f) < Math.Abs(coord + ky - closestGuide))
          closestGuide = f;
      }

      if (Math.Abs(coord + ky - closestGuide) < ReportWorkspace.Grid.SnapSize * 2)
        ky = closestGuide - coord;
    }

    private void CheckVirtualVGuide(float x, ComponentBase c)
    {
      x = Converter.DecreasePrecision(x, 2);
      int i = FVirtualVGuides.IndexOfKey(x);
      if (i != -1)
      {
        float scale = ReportWorkspace.Scale;
        FVirtualGuides.Add(new RectangleF(x * scale, c.AbsTop * scale,
          x * scale, FVirtualVGuides.Values[i].AbsTop * scale));
      }
    }

    private void CheckVirtualHGuide(float y, ComponentBase c)
    {
      y = Converter.DecreasePrecision(y, 2);
      int i = FVirtualHGuides.IndexOfKey(y);
      if (i != -1)
      {
        float scale = ReportWorkspace.Scale;
        FVirtualGuides.Add(new RectangleF(c.AbsLeft * scale, y * scale,
          FVirtualHGuides.Values[i].AbsLeft * scale, y * scale));
      }
    }

    private void AddVGuide(float x, ComponentBase c)
    {
      x = Converter.DecreasePrecision(x, 2);
      if (!FVirtualVGuides.ContainsKey(x))
        FVirtualVGuides.Add(x, c);
    }

    private void AddHGuide(float y, ComponentBase c)
    {
      y = Converter.DecreasePrecision(y, 2);
      if (!FVirtualHGuides.ContainsKey(y))
        FVirtualHGuides.Add(y, c);
    }

    private void DrawGuides(Graphics g, object obj)
    {
      FloatCollection guides = null;
      bool vertical = false;
      float offs = 0;

      if (obj is ReportPage)
      {
        guides = (obj as ReportPage).Guides;
        vertical = true;
      }
      else if (obj is BandBase)
      {
        guides = (obj as BandBase).Guides;
        offs = (obj as BandBase).Top;
      }
      if (guides != null)
      {
        Pen pen = new Pen(Color.CornflowerBlue);
        pen.DashStyle = DashStyle.Dot;
        foreach (float f in guides)
        {
          float scale = ReportWorkspace.Scale;
          if (vertical)
            g.DrawLine(pen, f * scale, 0, f * scale, Workspace.Height);
          else
          {
            if (f > 0 && f < (obj as BandBase).Height)
              g.DrawLine(pen, 0, (f + offs) * scale, Workspace.Width, (f + offs) * scale);
          }  
        }
        pen.Dispose();
      }
    }

    private void DrawVirtualGuides(Graphics g)
    {
      Pen pen = new Pen(Color.CornflowerBlue);
      pen.DashStyle = DashStyle.Dot;
      foreach (RectangleF rect in FVirtualGuides)
      {
        g.DrawLine(pen, rect.Left, rect.Top, rect.Width, rect.Height);
      }
      pen.Dispose();
    }

    public void Draw(Graphics g)
    {
      // draw guides
      DrawGuides(g, Page);
      foreach (Base obj in Designer.Objects)
      {
        if (obj is BandBase)
          DrawGuides(g, obj);
      }
      // draw virtual guides
      DrawVirtualGuides(g);
    }

    public void CreateVirtualGuides()
    {
      FVirtualVGuides.Clear();
      FVirtualHGuides.Clear();
      foreach (Base obj in Designer.Objects)
      {
        if (obj is ComponentBase && !(obj is BandBase) && !Designer.SelectedObjects.Contains(obj))
        {
          ComponentBase c = obj as ComponentBase;
          AddVGuide(c.AbsLeft, c);
          AddVGuide(c.AbsRight, c);
          AddHGuide(c.AbsTop, c);
          AddHGuide(c.AbsBottom, c);
        }
      }
    }

    public void CheckVirtualGuides()
    {
      FVirtualGuides.Clear();
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is BandBase))
        {
          ComponentBase c = obj as ComponentBase;
          CheckVirtualVGuide(c.AbsLeft, c);
          CheckVirtualVGuide(c.AbsRight, c);
          CheckVirtualHGuide(c.AbsTop, c);
          CheckVirtualHGuide(c.AbsBottom, c);
        }
      }
    }

    public void ClearVirtualGuides()
    {
      FVirtualGuides.Clear();
    }

    public void CheckGuides(ref float kx, ref float ky)
    {
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is BandBase))
        {
          ComponentBase c = obj as ComponentBase;
          CheckVGuide(ref kx, c.Left);
          CheckVGuide(ref kx, c.Right);
          CheckHGuide(ref ky, c.Top, c);
          CheckHGuide(ref ky, c.Bottom, c);
        }
      }
    }

    public void BeforeMoveHGuide(BandBase band, int guide)
    {
      FGuideLinks.Clear();
      foreach (Base obj in Designer.Objects)
      {
        if (obj is ReportComponentBase && !(obj is BandBase) && obj.Parent == band)
        {
          ReportComponentBase c = obj as ReportComponentBase;
          LinkPoint link = LinkPoint.None;
          if (Math.Abs(c.Top - band.Guides[guide]) < 0.01)
            link = LinkPoint.Top;
          else if (Math.Abs(c.Bottom - band.Guides[guide]) < 0.01)
            link = LinkPoint.Bottom;
          if (link != LinkPoint.None)
          {
            LinkInfo info = new LinkInfo();
            info.Obj = c;
            info.Link = link;
            // check if object is also linked to another guide
            int i = band.Guides.IndexOf(c.Top);
            if (i != -1 && i != guide)
              info.DoubleLinked = true;
            i = band.Guides.IndexOf(c.Bottom);
            if (i != -1 && i != guide)
              info.DoubleLinked = true;
            FGuideLinks.Add(info);
          }
        }
      }
    }
    
    public void MoveHGuide(BandBase band, int guide, float ky)
    {
      foreach (LinkInfo link in FGuideLinks)
      {
        if (!link.DoubleLinked)
          link.Obj.Top += ky;
        else
        {
          if (link.Link == LinkPoint.Top)
          {
            link.Obj.Top += ky;
            link.Obj.Height -= ky;
          }
          else if (link.Link == LinkPoint.Bottom)
            link.Obj.Height += ky;
        }
      }
    }

    public void BeforeMoveVGuide(int guide)
    {
      FGuideLinks.Clear();
      foreach (Base obj in Designer.Objects)
      {
        if (obj is ReportComponentBase && !(obj is BandBase))
        {
          ReportComponentBase c = obj as ReportComponentBase;
          LinkPoint link = LinkPoint.None;
          if (Math.Abs(c.Left - Page.Guides[guide]) < 0.01)
            link = LinkPoint.Left;
          else if (Math.Abs(c.Right - Page.Guides[guide]) < 0.01)
            link = LinkPoint.Right;
          if (link != LinkPoint.None)
          {
            LinkInfo info = new LinkInfo();
            info.Obj = c;
            info.Link = link;
            // check if object is also linked to another guide
            int i = Page.Guides.IndexOf(c.Left);
            if (i != -1 && i != guide)
              info.DoubleLinked = true;
            i = Page.Guides.IndexOf(c.Right);
            if (i != -1 && i != guide)
              info.DoubleLinked = true;
            FGuideLinks.Add(info);
          }
        }
      }
    }

    public void MoveVGuide(int guide, float kx)
    {
      foreach (LinkInfo link in FGuideLinks)
      {
        if (!link.DoubleLinked)
          link.Obj.Left += kx;
        else
        {
          if (link.Link == LinkPoint.Left)
          {
            link.Obj.Left += kx;
            link.Obj.Width -= kx;
          }
          else if (link.Link == LinkPoint.Right)
            link.Obj.Width += kx;
        }
      }
    }

    public Guides(ReportPageDesigner pd)
    {
      FPageDesigner = pd;
      FVirtualVGuides = new SortedList<float, ComponentBase>();
      FVirtualHGuides = new SortedList<float, ComponentBase>();
      FVirtualGuides = new List<RectangleF>();
      FGuideLinks = new List<LinkInfo>();
    }

    
    private enum LinkPoint { None, Left, Top, Right, Bottom }

    private class LinkInfo
    {
      public ReportComponentBase Obj;
      public LinkPoint Link;
      public bool DoubleLinked;
    }

  }
}
