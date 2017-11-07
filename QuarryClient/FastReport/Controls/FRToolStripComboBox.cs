using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace FastReport.Controls
{
  internal class FRToolStripComboBox : ToolStripComboBox
  {
    private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index >= 0)
      {
        e.DrawBackground();
        using (Brush b = new SolidBrush(e.ForeColor))
        {
          e.Graphics.DrawString((string)Items[e.Index], e.Font, b, e.Bounds.X, e.Bounds.Y);
        }  
      }
    }

    public FRToolStripComboBox()
    {
      AutoSize = false;
      ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
      ComboBox.ItemHeight = 13;
      ComboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
    }
  }
}