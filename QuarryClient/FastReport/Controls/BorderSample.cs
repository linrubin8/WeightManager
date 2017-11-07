using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class ToggleLineEventArgs
  {
    public Border Border;
    public BorderLines Line;
    public bool Toggle;

    public ToggleLineEventArgs(Border border, BorderLines line, bool toggle)
    {
      Border = border;
      Line = line;
      Toggle = toggle;
    }
  }

  internal delegate void ToggleLineEventHandler(object sender, ToggleLineEventArgs e);

  internal class BorderSample : Control
  {
    private Border FBorder;
    public event ToggleLineEventHandler ToggleLine;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Border Border
    {
      get { return FBorder; }
      set { FBorder = value; }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      // draw control frame
      Pen p = new Pen(Color.FromArgb(127, 157, 185));
      g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
      p.Dispose();
      // draw corners
      p = SystemPens.ControlDark;
      g.DrawLine(p, 10, 10, 10, 5);
      g.DrawLine(p, 10, 10, 5, 10);
      g.DrawLine(p, 10, Height - 11, 10, Height - 6);
      g.DrawLine(p, 10, Height - 11, 5, Height - 11);
      g.DrawLine(p, Width - 11, 10, Width - 11, 5);
      g.DrawLine(p, Width - 11, 10, Width - 6, 10);
      g.DrawLine(p, Width - 11, Height - 11, Width - 11, Height - 6);
      g.DrawLine(p, Width - 11, Height - 11, Width - 6, Height - 11);
      // draw text
      StringFormat sf = new StringFormat();
      sf.Alignment = StringAlignment.Center;
      sf.LineAlignment = StringAlignment.Center;
      g.DrawString(Res.Get("Misc,Sample"), Font, SystemBrushes.WindowText, DisplayRectangle, sf);
      sf.Dispose();
      // draw border
      if (Border != null)
      {
        using (GraphicCache cache = new GraphicCache())
        {
          Border.Draw(new FRPaintEventArgs(g, 1, 1, cache), new RectangleF(10, 10, Width - 21, Height - 21));
        }  
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      BorderLines line = BorderLines.None;
      if (e.X > 12 && e.X < Width - 12 && e.Y > 5 && e.Y < 18)
        line = BorderLines.Top;
      else if (e.X > 12 && e.X < Width - 12 && e.Y > Height - 18 && e.Y < Height - 5)
        line = BorderLines.Bottom;
      else if (e.X > 5 && e.X < 18 && e.Y > 12 && e.Y < Height - 12)
        line = BorderLines.Left;
      else if (e.X > Width - 18 && e.X < Width - 5 && e.Y > 12 && e.Y < Height - 12)
        line = BorderLines.Right;
      if (Border != null && ToggleLine != null)
      {
        ToggleLine(this, new ToggleLineEventArgs(Border, line, (Border.Lines & line) == 0));
        Refresh();
      }  
    }

    public BorderSample()
    {
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      BackColor = SystemColors.Window;
      Size = new Size(160, 94);
    }
  }
}
