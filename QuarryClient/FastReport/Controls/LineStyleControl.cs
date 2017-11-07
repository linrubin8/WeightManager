using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastReport.Controls
{
  internal class LineStyleControl : Control
  {
    private LineStyle[] FStyles;
    private LineStyle FStyle;
    private float FLineWidth;
    private Color FLineColor;
    private bool FShowBorder;

    public event EventHandler StyleSelected;
    
    public LineStyle Style
    {
      get { return FStyle; }
      set
      {
        FStyle = value;
        Refresh();
      }
    }
    
    public float LineWidth
    {
      get { return FLineWidth; }
      set
      {
        FLineWidth = value;
        Refresh();
      }
    }

    public Color LineColor
    {
      get { return FLineColor; }
      set
      {
        FLineColor = value;
        Refresh();
      }
    }

    public bool ShowBorder
    {
      get { return FShowBorder; }
      set 
      { 
        FShowBorder = value;
        Refresh();
      }  
    }

    private void DrawHighlight(Graphics g, Rectangle rect)
    {
      Brush brush = new SolidBrush(Color.FromArgb(193, 210, 238));
      g.FillRectangle(brush, rect);
      Pen pen = new Pen(Color.FromArgb(49, 106, 197));
      g.DrawRectangle(pen, rect);
      brush.Dispose();
      pen.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen p = null;
      // draw control border
      if (FShowBorder)
      {
        p = new Pen(Color.FromArgb(127, 157, 185));
        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        p.Dispose();
      }
      // draw items
      for (int i = 0; i < FStyles.Length; i++)
      {
        // highlight active style
        if (FStyles[i] == FStyle)
          DrawHighlight(g, new Rectangle(4, i * 15 + 4, Width - 9, 15));
        p = new Pen(FLineColor, FLineWidth < 1.5f ? 1.5f : FLineWidth);
        DashStyle[] styles = new DashStyle[] { 
          DashStyle.Solid, DashStyle.Dash, DashStyle.Dot, DashStyle.DashDot, DashStyle.DashDotDot, DashStyle.Solid };
        p.DashStyle = styles[(int)FStyles[i]];
        if (FStyles[i] == LineStyle.Double)
        {
          p.Width *= 2.5f;
          p.CompoundArray = new float[] { 0, 0.4f, 0.6f, 1 };
        }  
        g.DrawLine(p, 8, i * 15 + 12, Width - 8, i * 15 + 12);
        p.Dispose();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      int i = (e.Y - 4) / 15;
      if (i < 0)
        i = 0;
      if (i > FStyles.Length - 1)
        i = FStyles.Length - 1;
      
      Style = FStyles[i];
      if (StyleSelected != null)
        StyleSelected(this, EventArgs.Empty);
    }

    public LineStyleControl()
    {
      FStyles = new LineStyle[] {
        LineStyle.Solid, LineStyle.Dash, LineStyle.Dot, LineStyle.DashDot, LineStyle.DashDotDot, LineStyle.Double };
      FLineColor = Color.Black;
      FLineWidth = 1;
      FShowBorder = true;
      BackColor = SystemColors.Window;
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      Size = new Size(70, 100);
    }
  }
}
