using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace FastReport.Controls
{
  internal class LineWidthButtonItem : ButtonItem
  {
    private float FLineWidth;
    private ItemContainer FContainer;
    public event EventHandler WidthSelected;

    public float LineWidth
    {
      get { return FLineWidth; }
      set
      {
        FLineWidth = value;
        foreach (BaseItem item in FContainer.SubItems)
        {
          (item as ButtonItem).Checked = (float)(item as ButtonItem).Tag == value;
        }
      }
    }

    private void AddSubItem(float width, string text)
    {
      ButtonItem item = new ButtonItem();

      Bitmap bmp = new Bitmap(64, 14);
      using (Graphics g = Graphics.FromImage(bmp))
      {
        using (Pen pen = new Pen(Color.Black, width))
        {
          g.DrawLine(pen, 4, 7, 60, 7);
        }
      }

      item.Image = bmp;
      item.Text = text;
      item.ButtonStyle = eButtonStyle.ImageAndText;
      item.Tag = width;
      item.Click += new EventHandler(item_Click);
      FContainer.SubItems.Add(item);
    }

    private void item_Click(object sender, EventArgs e)
    {
      FLineWidth = (float)(sender as ButtonItem).Tag;
      if (WidthSelected != null)
        WidthSelected(this, EventArgs.Empty);
    }

    private void UpdateItems()
    {
      FContainer = new ItemContainer();
      FContainer.LayoutOrientation = eOrientation.Vertical;
      SubItems.Add(FContainer);

      AddSubItem(0.25f, "0.25");
      AddSubItem(0.5f, "0.5");
      AddSubItem(1, "1");
      AddSubItem(1.5f, "1.5");
      AddSubItem(2, "2");
      AddSubItem(3, "3");
      AddSubItem(4, "4");
      AddSubItem(5, "5");
    }

    public LineWidthButtonItem()
    {
      AutoExpandOnClick = true;
      PopupType = ePopupType.ToolBar;
      UpdateItems();
    }
  }
}
