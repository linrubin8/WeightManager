using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FastReport.Controls;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class BorderEditorForm : BaseDialogForm
  {
    private Border FBorder;
    private bool FChanged;

    public Border Border
    {
      get { return FBorder; }
      set
      {
        FBorder = value;
        FSample.Border = value;

        UpdateControls();
      }
    }

    public bool Changed
    {
      get { return FChanged; }
      set { FChanged = value; }
    }

    private LineStyle LineStyle
    {
      get { return lsLineStyle.Style; }
    }

    private float LineWidth
    {
      get { return cbxLineWidth.LineWidth; }
    }

    private Color LineColor
    {
      get { return cbxLineColor.Color; }
    }

    private void Change()
    {
      FChanged = true;
      UpdateControls();
    }

    private void Line_Click(object sender, EventArgs e)
    {
      BorderLines line = BorderLines.None;
      if (sender == cbTopLine)
        line = BorderLines.Top;
      else if (sender == cbBottomLine)
        line = BorderLines.Bottom;
      else if (sender == cbLeftLine)
        line = BorderLines.Left;
      else if (sender == cbRightLine)
        line = BorderLines.Right;

      ToggleLine(line, (sender as CheckBox).Checked);
    }

    private void btnNoLines_Click(object sender, EventArgs e)
    {
      Border.Lines = BorderLines.None;
      Change();
    }

    private void btnAllLines_Click(object sender, EventArgs e)
    {
      Border.Lines = BorderLines.All;
      Border.Style = LineStyle;
      Border.Width = LineWidth;
      Border.Color = LineColor;
      Change();
    }

    private void cbShadow_CheckedChanged(object sender, EventArgs e)
    {
      Border.Shadow = cbShadow.Checked;
      Change();
    }

    private void cbxShadowColor_ColorSelected(object sender, EventArgs e)
    {
      Border.ShadowColor = cbxShadowColor.Color;
      Change();
    }

    private void cbxShadowWidth_WidthSelected(object sender, EventArgs e)
    {
      if (cbxShadowWidth.LineWidth == 0) return;
      Border.ShadowWidth = cbxShadowWidth.LineWidth;
      Change();
    }

    private void cbxLineColor_ColorSelected(object sender, EventArgs e)
    {
      lsLineStyle.LineColor = LineColor;
    }

    private void cbxLineWidth_WidthSelected(object sender, EventArgs e)
    {
      lsLineStyle.LineWidth = LineWidth;
    }

    public void ToggleLine(BorderLines line, bool state)
    {
      BorderLine line1 = null;
      switch (line)
      {
        case BorderLines.Top:
          line1 = Border.TopLine;
          break;
        case BorderLines.Bottom:
          line1 = Border.BottomLine;
          break;
        case BorderLines.Left:
          line1 = Border.LeftLine;
          break;
        case BorderLines.Right:
          line1 = Border.RightLine;
          break;
      }
      if (line1 != null)
      {
        if (state || (line1.Style != LineStyle || line1.Width != LineWidth || line1.Color != LineColor))
        {
          Border.Lines = Border.Lines | line;
          line1.Style = LineStyle;
          line1.Width = LineWidth;
          line1.Color = LineColor;
        }
        else
        {
          Border.Lines = Border.Lines & ~line;
        }
      }
      Change();
    }

    private void FSample_ToggleLine(object sender, ToggleLineEventArgs e)
    {
      ToggleLine(e.Line, e.Toggle);
    }

    private void BorderEditorForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      lblHint.Width = gbLine.Right - lblHint.Left;
    }
    
    public void UpdateControls()
    {
      cbxLineWidth.LineWidth = Border.Width;
      cbxLineColor.Color = Border.Color;
      lsLineStyle.Style = Border.Style;
      lsLineStyle.LineColor = Border.Color;
      lsLineStyle.LineWidth = Border.Width;
      cbTopLine.Checked = (Border.Lines & BorderLines.Top) != 0;
      cbBottomLine.Checked = (Border.Lines & BorderLines.Bottom) != 0;
      cbLeftLine.Checked = (Border.Lines & BorderLines.Left) != 0;
      cbRightLine.Checked = (Border.Lines & BorderLines.Right) != 0;
      cbShadow.Checked = Border.Shadow;
      cbxShadowWidth.LineWidth = Border.ShadowWidth;
      cbxShadowColor.Color = Border.ShadowColor;
      lblShadowWidth.Enabled = Border.Shadow;
      cbxShadowWidth.Enabled = Border.Shadow;
      lblShadowColor.Enabled = Border.Shadow;
      cbxShadowColor.Enabled = Border.Shadow;
      cbxLineWidth.LineWidth = Border.Width;
      FSample.Refresh();
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,Border");
      Text = res.Get("");
      gbBorder.Text = res.Get("Border");
      gbLine.Text = res.Get("Line");
      lblHint.Text = res.Get("Hint");
      btnAllLines.Image = Res.GetImage(36);
      btnNoLines.Image = Res.GetImage(37);
      cbTopLine.Image = Res.GetImage(32);
      cbBottomLine.Image = Res.GetImage(33);
      cbLeftLine.Image = Res.GetImage(34);
      cbRightLine.Image = Res.GetImage(35);
      cbShadow.Text = res.Get("Shadow");
      lblShadowWidth.Text = res.Get("Width");
      lblShadowColor.Text = res.Get("Color");
      lblStyle.Text = res.Get("Style");
      lblLineWidth.Text = res.Get("Width");
      lblLineColor.Text = res.Get("Color");
    }

    public BorderEditorForm()
    {
      InitializeComponent();
      Border = new Border();
      Border.Lines = BorderLines.All;
      FSample.Border = Border;
      Localize();
      UpdateControls();
      FChanged = false;

      this.RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      /*//fix 1248
      Border.Width = cbxLineWidth.LineWidth;
      Border.ShadowWidth = cbxShadowWidth.LineWidth;
      //1248*/
      //fix 1513
      //Border.Width = cbxLineWidth.LineWidth;
      Border.ShadowWidth = cbxShadowWidth.LineWidth;
      //1513
    }
  }

}