using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Controls;

namespace FastReport.Controls
{
  internal class ToolStripColorButton : ToolStripSplitButton
  {
    private Bitmap FBitmap;
    private ColorDropDown FDropDown;
    private Color FDefaultColor;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color Color
    {
      get { return FDropDown.Color; }
      set { FDropDown.Color = value; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color DefaultColor
    {
      get { return FDefaultColor; }
      set
      {
        FDefaultColor = value;
        Graphics g = Graphics.FromImage(FBitmap);
        Color color = value == Color.Transparent ? Color.White : value;
        Brush b = new SolidBrush(color);
        g.FillRectangle(b, 1, 13, 15, 3);
        b.Dispose();
        g.Dispose();
        Invalidate();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int ImageIndex
    {
      get { return -1; }
      set
      {
        FBitmap = Res.GetImage(value);
        Image = FBitmap;
      }
    }
    
    private void FDropDown_ColorSelected(object sender, EventArgs e)
    {
      Color = FDropDown.Color;
      DefaultColor = Color;
      OnButtonClick(EventArgs.Empty);
    }

    public ToolStripColorButton()
    {
      FDropDown = new ColorDropDown();
      FDropDown.ColorSelected += new EventHandler(FDropDown_ColorSelected);
      DropDown = FDropDown;
    }
    
    public ToolStripColorButton(int imageIndex, Color defaultColor) : this()
    {
      ImageIndex = imageIndex;
      DefaultColor = defaultColor;
    }

  }
}
