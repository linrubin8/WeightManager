using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class AngleControl : Control
  {
    private int FAngle;
    private bool FShowBorder;
    private NumericUpDown udAngle;
    private bool FChanged;
    private bool FUpdating;
    
    public event EventHandler AngleChanged;
    
    public int Angle
    {
      get { return FAngle; }
      set 
      {
        FUpdating = true;
        if (value < 0)
          value += 360;
        FAngle = value % 360; 
        udAngle.Value = FAngle;
        Refresh();
        FUpdating = false;
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

    public bool Changed
    {
      get { return FChanged; }
      set { FChanged = value; }
    }

    private void RotateTo(int x, int y)
    {
      int size = Math.Min(Width, Height - 30);
      int cx = size / 2;
      int cy = cx;
      int r = x - cx == 0 ? (y > cy ? 90 : 270) : (int)(Math.Atan2((y - cy) , (x - cx)) * 180 / Math.PI);
      Angle = (int)Math.Round(r / 15f) * 15;
    }

    private void udAngle_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Angle = (int)udAngle.Value;
      Change();
    }
    
    private void Change()
    {
      FChanged = true;
      if (AngleChanged != null)
        AngleChanged(this, EventArgs.Empty);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen p = null;
      
      // draw control border
      if (FShowBorder)
      {
        using (p = new Pen(Color.FromArgb(127, 157, 185)))
        {
          g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        }
      }
      
      g.SmoothingMode = SmoothingMode.AntiAlias;
      // draw ticks
      int size = Math.Min(Width, Height - 30);
      int cx = size / 2;
      int cy = cx;
      int radius = size / 2 - 10;
      p = new Pen(Color.Silver);
      p.DashStyle = DashStyle.Dot;
      g.DrawEllipse(p, 10, 10, size - 20, size - 20);
      p.Dispose();
      for (int i = 0; i < 360; i+= 45)
      {
        Rectangle rect = new Rectangle(cx + (int)(Math.Cos(Math.PI / 180 * i) * radius) - 2,
          cy + (int)(Math.Sin(Math.PI / 180 * i) * radius) - 2, 4, 4);
        g.FillEllipse(i == FAngle ? Brushes.DarkOrange : SystemBrushes.Window, rect);
        g.DrawEllipse(i == FAngle ? Pens.DarkOrange : Pens.Black, rect);
      }
      
      // draw sample
      using (StringFormat sf = new StringFormat())
      {
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;
        StandardTextRenderer.Draw(Res.Get("Misc,Sample"), g, Font, SystemBrushes.WindowText, SystemPens.WindowText,
          new RectangleF(cx - radius + 1, cy - radius + 1, radius * 2, radius * 2),
          sf, FAngle, 1);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      RotateTo(e.X, e.Y);
    }
    
    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        RotateTo(e.X, e.Y);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      Change();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      udAngle.Location = new Point(10, Height - 30);
      udAngle.Size = new Size(Width - 20, 20);
    }

    public AngleControl()
    {
      udAngle = new NumericUpDown();
      udAngle.Maximum = 360;
      udAngle.Increment = 15;
      udAngle.ValueChanged += new EventHandler(udAngle_ValueChanged);
      Controls.Add(udAngle);
      FShowBorder = true;
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      Size = new Size(100, 130);
      BackColor = SystemColors.Window;
    }

  }
}
