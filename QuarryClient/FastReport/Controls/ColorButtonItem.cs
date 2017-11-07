using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Controls;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Controls
{
  internal class ColorButtonItem : ButtonItem
  {
    private Bitmap FBitmap;
    protected ControlContainerItem FHost;
    private ColorSelector FColorSelector;
    private Color FDefaultColor;

    public Color Color
    {
      get { return FColorSelector.Color; }
      set { FColorSelector.Color = value; }
    }

    public Color DefaultColor
    {
      get { return FDefaultColor; }
      set
      {
        FDefaultColor = value;
        if (FBitmap != null)
        {
            Graphics g = Graphics.FromImage(FBitmap);
            Color color = value == Color.Transparent ? Color.White : value;
            Brush b = new SolidBrush(color);
            g.FillRectangle(b, 1, 13, 15, 3);
            b.Dispose();
            g.Dispose();
        }
      }
    }

    public new int ImageIndex
    {
      get { return -1; }
      set
      {
        FBitmap = Res.GetImage(value);
        Image = FBitmap;
      }
    }
    
    public void SetStyle(UIStyle style)
    {
      FColorSelector.SetStyle(style);
    }

    private void FColorSelector_ColorSelected(object sender, EventArgs e)
    {
      Color = FColorSelector.Color;
      DefaultColor = Color;
      ClosePopup();
      RaiseClick();
    }

    public ColorButtonItem()
    {
      PopupType = ePopupType.ToolBar;
      FColorSelector = new ColorSelector();
      FColorSelector.ColorSelected += new EventHandler(FColorSelector_ColorSelected);
      FHost = new ControlContainerItem();
      FHost.Control = FColorSelector;
      SubItems.Add(FHost);
    }

    public ColorButtonItem(int imageIndex, Color defaultColor)
      : this()
    {
      ImageIndex = imageIndex;
      DefaultColor = defaultColor;
    }

  }
}
