using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace FastReport.Controls
{
  internal class HatchComboBox : ComboBox
  {
    private bool FUpdating;
    public event EventHandler HatchSelected;
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ObjectCollection Items
    {
      get { return base.Items; }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HatchStyle Style
    {
      get 
      { 
        if (SelectedItem == null)
          return HatchStyle.BackwardDiagonal;
        return (HatchStyle)SelectedItem; 
      }
      set 
      { 
        FUpdating = true;
        SelectedItem = value;
        FUpdating = false;
      }
    }
    
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      e.DrawBackground();
      Graphics g = e.Graphics;
      if (e.Index >= 0)
      {
        HatchStyle style = (HatchStyle)Items[e.Index];
        Rectangle rect = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, 20, 20);
        using (Brush b = new HatchBrush(style, Color.Black, Color.White))
        {
          g.FillRectangle(b, rect);
          g.DrawRectangle(Pens.Black, rect);
          using (Brush br = new SolidBrush(e.ForeColor))
          {
            g.DrawString(style.ToString(), e.Font, br, e.Bounds.Left + 28, e.Bounds.Top + 5);
          }  
        }
      }
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);
      if (FUpdating)
        return;
      if (HatchSelected != null)
        HatchSelected(this, EventArgs.Empty);
    }
    
    private void UpdateItems()
    {
      HatchStyle[] styles = new HatchStyle[] {
        HatchStyle.BackwardDiagonal,
        HatchStyle.Cross,
        HatchStyle.DarkDownwardDiagonal,
        HatchStyle.DarkHorizontal,
        HatchStyle.DarkUpwardDiagonal,
        HatchStyle.DarkVertical,
        HatchStyle.DashedDownwardDiagonal,
        HatchStyle.DashedHorizontal,
        HatchStyle.DashedUpwardDiagonal,
        HatchStyle.DashedVertical,
        HatchStyle.DiagonalBrick,
        HatchStyle.DiagonalCross,
        HatchStyle.Divot,
        HatchStyle.DottedDiamond,
        HatchStyle.DottedGrid,
        HatchStyle.ForwardDiagonal,
        HatchStyle.Horizontal,
        HatchStyle.HorizontalBrick,
        HatchStyle.LargeCheckerBoard,
        HatchStyle.LargeConfetti,
        HatchStyle.LightDownwardDiagonal,
        HatchStyle.LightHorizontal,
        HatchStyle.LightUpwardDiagonal,
        HatchStyle.LightVertical,
        HatchStyle.NarrowHorizontal,
        HatchStyle.NarrowVertical,
        HatchStyle.OutlinedDiamond,
        HatchStyle.Percent05,
        HatchStyle.Percent10,
        HatchStyle.Percent20,
        HatchStyle.Percent25,
        HatchStyle.Percent30,
        HatchStyle.Percent40,
        HatchStyle.Percent50,
        HatchStyle.Percent60,
        HatchStyle.Percent70,
        HatchStyle.Percent75,
        HatchStyle.Percent80,
        HatchStyle.Percent90,
        HatchStyle.Plaid,
        HatchStyle.Shingle,
        HatchStyle.SmallCheckerBoard,
        HatchStyle.SmallConfetti,
        HatchStyle.SmallGrid,
        HatchStyle.SolidDiamond,
        HatchStyle.Sphere,
        HatchStyle.Trellis,
        HatchStyle.Vertical,
        HatchStyle.Weave,
        HatchStyle.WideDownwardDiagonal,
        HatchStyle.WideUpwardDiagonal,
        HatchStyle.ZigZag };
      Items.Clear();
      foreach (HatchStyle style in styles)
      {
        Items.Add(style);
      }
    }

    public HatchComboBox()
    {
      DrawMode = DrawMode.OwnerDrawFixed;
      DropDownStyle = ComboBoxStyle.DropDownList;
      ItemHeight = 24;
      IntegralHeight = false;
      UpdateItems();
    }
  }
}
