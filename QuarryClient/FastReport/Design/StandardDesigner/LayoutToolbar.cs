using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
  internal class LayoutToolbar: ToolbarBase
  {
    #region Fields
    public ButtonItem btnAlignToGrid;
    public ButtonItem btnLeft;
    public ButtonItem btnCenter;
    public ButtonItem btnRight;
    public ButtonItem btnTop;
    public ButtonItem btnMiddle;
    public ButtonItem btnBottom;
    public ButtonItem btnSameWidth;
    public ButtonItem btnSameHeight;
    public ButtonItem btnSameSize;
    public ButtonItem btnSizeToGrid;
    public ButtonItem btnSpaceHorizontally;
    public ButtonItem btnIncreaseHorizontalSpacing;
    public ButtonItem btnDecreaseHorizontalSpacing;
    public ButtonItem btnRemoveHorizontalSpacing;
    public ButtonItem btnSpaceVertically;
    public ButtonItem btnIncreaseVerticalSpacing;
    public ButtonItem btnDecreaseVerticalSpacing;
    public ButtonItem btnRemoveVerticalSpacing;
    public ButtonItem btnCenterHorizontally;
    public ButtonItem btnCenterVertically;
    public ButtonItem btnBringToFront;
    public ButtonItem btnSendToBack;
    #endregion

    #region Private Methods
    private void UpdateControls()
    {
      bool oneObjSelected = Designer.SelectedComponents.Count > 0;
      bool threeObjSelected = Designer.SelectedComponents.Count >= 3;
      bool severalObjSelected = Designer.SelectedComponents.Count > 1;
      bool canChangeOrder = Designer.SelectedComponents.Count > 0 && 
        Designer.SelectedComponents.First.HasFlag(Flags.CanChangeOrder);
      bool canMove = Designer.SelectedComponents.Count > 0 && 
        Designer.SelectedComponents.First.HasFlag(Flags.CanMove);
      bool canResize = Designer.SelectedComponents.Count > 0 && 
        Designer.SelectedComponents.First.HasFlag(Flags.CanResize);
      
      btnAlignToGrid.Enabled = oneObjSelected && canMove;
      btnLeft.Enabled = severalObjSelected && canMove;
      btnCenter.Enabled = severalObjSelected && canMove;
      btnRight.Enabled = severalObjSelected && canMove;
      btnTop.Enabled = severalObjSelected && canMove;
      btnMiddle.Enabled = severalObjSelected && canMove;
      btnBottom.Enabled = severalObjSelected && canMove;
      btnSameWidth.Enabled = severalObjSelected && canResize;
      btnSameHeight.Enabled = severalObjSelected && canResize;
      btnSameSize.Enabled = severalObjSelected && canResize;
      btnSizeToGrid.Enabled = oneObjSelected && canResize;
      btnSpaceHorizontally.Enabled = threeObjSelected && canMove;
      btnIncreaseHorizontalSpacing.Enabled = severalObjSelected && canMove;
      btnDecreaseHorizontalSpacing.Enabled = severalObjSelected && canMove;
      btnRemoveHorizontalSpacing.Enabled = severalObjSelected && canMove;
      btnSpaceVertically.Enabled = threeObjSelected && canMove;
      btnIncreaseVerticalSpacing.Enabled = severalObjSelected && canMove;
      btnDecreaseVerticalSpacing.Enabled = severalObjSelected && canMove;
      btnRemoveVerticalSpacing.Enabled = severalObjSelected && canMove;
      btnCenterHorizontally.Enabled = oneObjSelected && canMove;
      btnCenterVertically.Enabled = oneObjSelected && canMove;
      btnBringToFront.Enabled = canChangeOrder;
      btnSendToBack.Enabled = canChangeOrder;
    }

    private void btnAlignToGrid_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignToGrid();
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignLeft();
    }

    private void btnCenter_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignCenter();
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignRight();
    }

    private void btnTop_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignTop();
    }

    private void btnMiddle_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignMiddle();
    }

    private void btnBottom_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.AlignBottom();
    }

    private void btnSameWidth_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SameWidth();
    }

    private void btnSameHeight_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SameHeight();
    }

    private void btnSameSize_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SameSize();
    }

    private void btnCenterHorizontally_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.CenterHorizontally();
    }

    private void btnCenterVertically_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.CenterVertically();
    }

    private void btnSizeToGrid_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SizeToGrid();
    }

    private void btnSpaceHorizontally_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SpaceHorizontally();
    }

    private void btnIncreaseHorizontalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.IncreaseHorizontalSpacing();
    }

    private void btnDecreaseHorizontalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.DecreaseHorizontalSpacing();
    }

    private void btnRemoveHorizontalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.RemoveHorizontalSpacing();
    }

    private void btnSpaceVertically_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.SpaceVertically();
    }

    private void btnIncreaseVerticalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.IncreaseVerticalSpacing();
    }

    private void btnDecreaseVerticalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.DecreaseVerticalSpacing();
    }

    private void btnRemoveVerticalSpacing_Click(object sender, EventArgs e)
    {
      Designer.SelectedComponents.RemoveVerticalSpacing();
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
      MyRes res = new MyRes("Designer,Toolbar,Layout");
      Text = res.Get("");

      SetItemText(btnAlignToGrid, res.Get("AlignToGrid"));
      SetItemText(btnLeft, res.Get("Left"));
      SetItemText(btnCenter, res.Get("Center"));
      SetItemText(btnRight, res.Get("Right"));
      SetItemText(btnTop, res.Get("Top"));
      SetItemText(btnMiddle, res.Get("Middle"));
      SetItemText(btnBottom, res.Get("Bottom"));
      SetItemText(btnSameWidth, res.Get("SameWidth"));
      SetItemText(btnSameHeight, res.Get("SameHeight"));
      SetItemText(btnSameSize, res.Get("SameSize"));
      SetItemText(btnSizeToGrid, res.Get("SizeToGrid"));
      SetItemText(btnSpaceHorizontally, res.Get("SpaceHorizontally"));
      SetItemText(btnIncreaseHorizontalSpacing, res.Get("IncreaseHorizontalSpacing"));
      SetItemText(btnDecreaseHorizontalSpacing, res.Get("DecreaseHorizontalSpacing"));
      SetItemText(btnRemoveHorizontalSpacing, res.Get("RemoveHorizontalSpacing"));
      SetItemText(btnSpaceVertically, res.Get("SpaceVertically"));
      SetItemText(btnIncreaseVerticalSpacing, res.Get("IncreaseVerticalSpacing"));
      SetItemText(btnDecreaseVerticalSpacing, res.Get("DecreaseVerticalSpacing"));
      SetItemText(btnRemoveVerticalSpacing, res.Get("RemoveVerticalSpacing"));
      SetItemText(btnCenterHorizontally, res.Get("CenterHorizontally"));
      SetItemText(btnCenterVertically, res.Get("CenterVertically"));
      SetItemText(btnBringToFront, res.Get("BringToFront"));
      SetItemText(btnSendToBack, res.Get("SendToBack"));
    }
    #endregion
    
    public LayoutToolbar(Designer designer) : base(designer)
    {
      Name = "LayoutToolbar";

      btnAlignToGrid = CreateButton("btnLayoutAlignToGrid", Res.GetImage(98), btnAlignToGrid_Click);
      btnSizeToGrid = CreateButton("btnLayoutSizeToGrid", Res.GetImage(57), btnSizeToGrid_Click);
      btnLeft = CreateButton("btnLayoutLeft", Res.GetImage(41), btnLeft_Click);
      btnLeft.BeginGroup = true;
      btnCenter = CreateButton("btnLayoutCenter", Res.GetImage(42), btnCenter_Click);
      btnRight = CreateButton("btnLayoutRight", Res.GetImage(45), btnRight_Click);
      btnTop = CreateButton("btnLayoutTop", Res.GetImage(46), btnTop_Click);
      btnTop.BeginGroup = true;
      btnMiddle = CreateButton("btnLayoutMiddle", Res.GetImage(47), btnMiddle_Click);
      btnBottom = CreateButton("btnLayoutBottom", Res.GetImage(50), btnBottom_Click);
      btnSameWidth = CreateButton("btnLayoutSameWidth", Res.GetImage(83), btnSameWidth_Click);
      btnSameWidth.BeginGroup = true;
      btnSameHeight = CreateButton("btnLayoutSameHeight", Res.GetImage(84), btnSameHeight_Click);
      btnSameSize = CreateButton("btnLayoutSameSize", Res.GetImage(91), btnSameSize_Click);
      btnSpaceHorizontally = CreateButton("btnLayoutSpaceHorizontally", Res.GetImage(44), btnSpaceHorizontally_Click);
      btnSpaceHorizontally.BeginGroup = true;
      btnIncreaseHorizontalSpacing = CreateButton("btnLayoutIncreaseHorizontalSpacing", Res.GetImage(92), btnIncreaseHorizontalSpacing_Click);
      btnDecreaseHorizontalSpacing = CreateButton("btnLayoutDecreaseHorizontalSpacing", Res.GetImage(93), btnDecreaseHorizontalSpacing_Click);
      btnRemoveHorizontalSpacing = CreateButton("btnLayoutRemoveHorizontalSpacing", Res.GetImage(94), btnRemoveHorizontalSpacing_Click);
      btnSpaceVertically = CreateButton("btnLayoutSpaceVertically", Res.GetImage(49), btnSpaceVertically_Click);
      btnSpaceVertically.BeginGroup = true;
      btnIncreaseVerticalSpacing = CreateButton("btnLayoutIncreaseVerticalSpacing", Res.GetImage(95), btnIncreaseVerticalSpacing_Click);
      btnDecreaseVerticalSpacing = CreateButton("btnLayoutDecreaseVerticalSpacing", Res.GetImage(96), btnDecreaseVerticalSpacing_Click);
      btnRemoveVerticalSpacing = CreateButton("btnLayoutRemoveVerticalSpacing", Res.GetImage(97), btnRemoveVerticalSpacing_Click);
      btnCenterHorizontally = CreateButton("btnLayoutCenterHorizontally", Res.GetImage(43), btnCenterHorizontally_Click);
      btnCenterHorizontally.BeginGroup = true;
      btnCenterVertically = CreateButton("btnLayoutCenterVertically", Res.GetImage(48), btnCenterVertically_Click);
      btnBringToFront = CreateButton("btnLayoutBringToFront", Res.GetImage(14), Designer.cmdBringToFront.Invoke);
      btnBringToFront.BeginGroup = true;
      btnSendToBack = CreateButton("btnLayoutSendToBack", Res.GetImage(15), Designer.cmdSendToBack.Invoke);

      Items.AddRange(new BaseItem[] {
        btnAlignToGrid, btnSizeToGrid,
        btnLeft, btnCenter, btnRight,
        btnTop, btnMiddle, btnBottom, 
        btnSameWidth, btnSameHeight, btnSameSize, 
        btnSpaceHorizontally, btnIncreaseHorizontalSpacing, btnDecreaseHorizontalSpacing, btnRemoveHorizontalSpacing,
        btnSpaceVertically, btnIncreaseVerticalSpacing, btnDecreaseVerticalSpacing, btnRemoveVerticalSpacing, 
        btnCenterHorizontally, btnCenterVertically, 
        btnBringToFront, btnSendToBack, CustomizeItem });

      Localize();
    }

  }

}
