using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;
using FastReport.Dialog;

namespace FastReport.Design.PageDesigners.Dialog
{
  internal class Guides
  {
    private PageDesignerBase FPageDesigner;
    private SortedList<float, ComponentBase> FVirtualVGuides;
    private SortedList<float, ComponentBase> FVirtualHGuides;
    private List<RectangleF> FVirtualGuides;

    public DialogPage Page
    {
      get { return FPageDesigner.Page as DialogPage; }
    }
    
    public Designer Designer
    {
      get { return FPageDesigner.Designer; }
    }

    private void CheckVirtualVGuide(float x, ComponentBase c)
    {
      x = Converter.DecreasePrecision(x, 2);
      int i = FVirtualVGuides.IndexOfKey(x);
      if (i != -1)
      {
        FVirtualGuides.Add(new RectangleF(x, c.AbsTop, x, FVirtualVGuides.Values[i].AbsTop));
      }
    }

    private void CheckVirtualHGuide(float y, ComponentBase c)
    {
      y = Converter.DecreasePrecision(y, 2);
      int i = FVirtualHGuides.IndexOfKey(y);
      if (i != -1)
      {
        FVirtualGuides.Add(new RectangleF(c.AbsLeft, y, FVirtualHGuides.Values[i].AbsLeft, y));
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

    public void Draw(Graphics g)
    {
      foreach (RectangleF rect in FVirtualGuides)
      {
        g.DrawLine(Pens.CornflowerBlue, rect.Left, rect.Top, rect.Width, rect.Height);
      }
    }

    public void CreateVirtualGuides()
    {
      FVirtualVGuides.Clear();
      FVirtualHGuides.Clear();
      foreach (Base obj in Designer.Objects)
      {
        if (obj is ComponentBase && !Designer.SelectedObjects.Contains(obj))
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
        if (obj is ComponentBase)
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

    public Guides(PageDesignerBase pd)
    {
      FPageDesigner = pd;
      FVirtualVGuides = new SortedList<float, ComponentBase>();
      FVirtualHGuides = new SortedList<float, ComponentBase>();
      FVirtualGuides = new List<RectangleF>();
    }

  }
}
