using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Controls;
using FastReport.Forms;
using FastReport.Design.PageDesigners.Page;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
  internal class BorderToolbar : ToolbarBase
  {
    #region Fields
    public ButtonItem btnTop;
    public ButtonItem btnBottom;
    public ButtonItem btnLeft;
    public ButtonItem btnRight;
    public ButtonItem btnAll;
    public ButtonItem btnNone;
    public ButtonItem btnBorderProps;
    public ColorButtonItem btnFillColor;
    public ButtonItem btnFillStyle;
    public ColorButtonItem btnLineColor;
    public LineWidthButtonItem btnWidth;
    public LineStyleButtonItem btnStyle;
    #endregion

    #region Private Methods
    private void UpdateControls()
    {
      bool enabled = Designer.SelectedReportComponents.Enabled;
      bool simple = Designer.SelectedReportComponents.SimpleBorder;
      bool useBorder = Designer.SelectedReportComponents.BorderEnabled;
      
      bool borderEnabled = enabled && !simple && useBorder;
      btnTop.Enabled = borderEnabled;
      btnBottom.Enabled = borderEnabled;
      btnLeft.Enabled = borderEnabled;
      btnRight.Enabled = borderEnabled;
      btnAll.Enabled = borderEnabled;
      btnNone.Enabled = borderEnabled;
      btnFillColor.Enabled = enabled && Designer.SelectedReportComponents.FillEnabled;
      btnFillStyle.Enabled = enabled && Designer.SelectedReportComponents.FillEnabled;
      btnLineColor.Enabled = enabled && useBorder;
      btnWidth.Enabled = enabled && useBorder;
      btnStyle.Enabled = enabled && useBorder;
      btnBorderProps.Enabled = borderEnabled;

      if (enabled)
      {
        Border border = Designer.SelectedReportComponents.First.Border;
        btnTop.Checked = (border.Lines & BorderLines.Top) != 0;
        btnBottom.Checked = (border.Lines & BorderLines.Bottom) != 0;
        btnLeft.Checked = (border.Lines & BorderLines.Left) != 0;
        btnRight.Checked = (border.Lines & BorderLines.Right) != 0;
        btnLineColor.Color = border.Color;
        if (Designer.SelectedReportComponents.First.Fill is SolidFill)
          btnFillColor.Color = (Designer.SelectedReportComponents.First.Fill as SolidFill).Color;
        btnWidth.LineWidth = border.Width;
        btnStyle.LineStyle = border.Style;
      }
    }

    private void btnTop_Click(object sender, EventArgs e)
    {
      btnTop.Checked = !btnTop.Checked;
      Designer.SelectedReportComponents.ToggleLine(BorderLines.Top, btnTop.Checked);
    }

    private void btnBottom_Click(object sender, EventArgs e)
    {
      btnBottom.Checked = !btnBottom.Checked;
      Designer.SelectedReportComponents.ToggleLine(BorderLines.Bottom, btnBottom.Checked);
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      btnLeft.Checked = !btnLeft.Checked;
      Designer.SelectedReportComponents.ToggleLine(BorderLines.Left, btnLeft.Checked);
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      btnRight.Checked = !btnRight.Checked;
      Designer.SelectedReportComponents.ToggleLine(BorderLines.Right, btnRight.Checked);
    }

    private void btnAll_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.ToggleLine(BorderLines.All, true);
    }

    private void btnNone_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.ToggleLine(BorderLines.All, false);
    }

    private void btnLineColor_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetLineColor(btnLineColor.DefaultColor);
    }

    private void btnFillColor_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetColor(btnFillColor.DefaultColor);
    }

    private void btnFillProps_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.InvokeFillEditor();
    }

    private void cbxWidth_WidthSelected(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetWidth(btnWidth.LineWidth);
    }

    private void cbxStyle_StyleSelected(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetLineStyle(btnStyle.LineStyle);
    }

    private void btnBorderProps_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.InvokeBorderEditor();
    }
    #endregion
    
    #region Public Methods
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      UpdateControls();
    }

    public override void UpdateContent()
    {
      base.UpdateContent();
      UpdateControls();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Designer,Toolbar,Border");
      Text = res.Get("");

      SetItemText(btnTop, res.Get("Top"));
      SetItemText(btnBottom, res.Get("Bottom"));
      SetItemText(btnLeft, res.Get("Left"));
      SetItemText(btnRight, res.Get("Right"));
      SetItemText(btnAll, res.Get("All"));
      SetItemText(btnNone, res.Get("None"));
      SetItemText(btnFillColor, res.Get("FillColor"));
      SetItemText(btnFillStyle, res.Get("FillStyle"));
      SetItemText(btnLineColor, res.Get("LineColor"));
      SetItemText(btnWidth, res.Get("Width"));
      SetItemText(btnStyle, res.Get("Style"));
      SetItemText(btnBorderProps, res.Get("Props"));
    }

    public override void UpdateUIStyle()
    {
      base.UpdateUIStyle();
      btnFillColor.SetStyle(Designer.UIStyle);
      btnLineColor.SetStyle(Designer.UIStyle);
    }
    #endregion

    public BorderToolbar(Designer designer) : base(designer)
    {
      Name = "BorderToolbar";

      btnTop = CreateButton("btnBorderTop", Res.GetImage(32), btnTop_Click);
      btnBottom = CreateButton("btnBorderBottom", Res.GetImage(33), btnBottom_Click);
      btnLeft = CreateButton("btnBorderLeft", Res.GetImage(34), btnLeft_Click);
      btnRight = CreateButton("btnBorderRight", Res.GetImage(35), btnRight_Click);
      btnAll = CreateButton("btnBorderAll", Res.GetImage(36), btnAll_Click);
      btnAll.BeginGroup = true;
      btnNone = CreateButton("btnBorderNone", Res.GetImage(37), btnNone_Click);
      btnFillColor = new ColorButtonItem(38, Color.Transparent);
      btnFillColor.Name = "btnBorderFillColor";
      btnFillColor.Click += btnFillColor_Click;
      btnFillColor.BeginGroup = true;
      btnFillStyle = CreateButton("btnBorderFillStyle", Res.GetImage(141), btnFillProps_Click);
      btnLineColor = new ColorButtonItem(39, Color.Black);
      btnLineColor.Name = "btnBorderLineColor";
      btnLineColor.Click += btnLineColor_Click;
      btnWidth = new LineWidthButtonItem();
      btnWidth.Name = "btnBorderWidth";
      btnWidth.Image = Res.GetImage(71);
      btnWidth.WidthSelected += cbxWidth_WidthSelected;
      btnStyle = new LineStyleButtonItem();
      btnStyle.Name = "btnBorderStyle";
      btnStyle.Image = Res.GetImage(85);
      btnStyle.StyleSelected += cbxStyle_StyleSelected;
      btnBorderProps = CreateButton("btnBorderBorderProps", Res.GetImage(40), btnBorderProps_Click);

      Items.AddRange(new BaseItem[] {
        btnTop, btnBottom, btnLeft, btnRight, 
        btnAll, btnNone, btnBorderProps, 
        btnFillColor, btnFillStyle, btnLineColor, btnWidth, btnStyle, CustomizeItem });

      Localize();
    }
  }

}
