using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Controls
{
  internal class LineStyleButtonItem : ButtonItem
  {
    private LineStyle FLineStyle;
    private ItemContainer FContainer;
    public event EventHandler StyleSelected;

    public LineStyle LineStyle
    {
      get { return FLineStyle; }
      set
      {
        FLineStyle = value;
        foreach (BaseItem item in FContainer.SubItems)
        {
          (item as ButtonItem).Checked = (LineStyle)(item as ButtonItem).Tag == value;
        }
      }
    }

    private void AddSubItem(LineStyle style)
    {
      ButtonItem item = new ButtonItem();
      
      Bitmap bmp = new Bitmap(88, 14);
      using (Graphics g = Graphics.FromImage(bmp))
      {
        using (Pen pen = new Pen(Color.Black, 2))
        {
          DashStyle[] styles = new DashStyle[] { 
            DashStyle.Solid, DashStyle.Dash, DashStyle.Dot, DashStyle.DashDot, DashStyle.DashDotDot, DashStyle.Solid };
          pen.DashStyle = styles[(int)style];
          if (style == LineStyle.Double)
          {
            pen.Width = 5;
            pen.CompoundArray = new float[] { 0, 0.4f, 0.6f, 1 };
          }
          g.DrawLine(pen, 4, 7, 84, 7);
        }
      }
      
      item.Image = bmp;
      item.Tag = style;
      item.Click += new EventHandler(item_Click);
      FContainer.SubItems.Add(item);
    }

    private void item_Click(object sender, EventArgs e)
    {
      FLineStyle = (LineStyle)(sender as ButtonItem).Tag;
      if (StyleSelected != null)
        StyleSelected(this, EventArgs.Empty);
    }
    
    private void UpdateItems()
    {
      FContainer = new ItemContainer();
      FContainer.LayoutOrientation = eOrientation.Vertical;
      SubItems.Add(FContainer);
      
      AddSubItem(LineStyle.Solid);
      AddSubItem(LineStyle.Dash);
      AddSubItem(LineStyle.Dot);
      AddSubItem(LineStyle.DashDot);
      AddSubItem(LineStyle.DashDotDot);
      AddSubItem(LineStyle.Double);
    }

    public LineStyleButtonItem()
    {
      AutoExpandOnClick = true;
      PopupType = ePopupType.ToolBar;
      UpdateItems();
    }
  }
}
