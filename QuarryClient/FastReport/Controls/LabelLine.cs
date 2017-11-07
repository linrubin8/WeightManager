using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents the label with line.
  /// </summary>
  [ToolboxItem(false)]
  public class LabelLine : Control
  {
    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      int x = 0;
      if (!String.IsNullOrEmpty(Text))
      {
        TextRenderer.DrawText(g, Text, Font, new Point(0, 0), ForeColor);
        x += TextRenderer.MeasureText(Text, Font).Width + 4;
      }
      g.DrawLine(Pens.Silver, x, Height / 2, Width, Height / 2);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="LabelLine"/> class.
    /// </summary>
    public LabelLine()
    {
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    }
  }
}
