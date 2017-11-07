using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
  internal class UndoRedoDropDown : ItemContainer
  {
    private bool FUpdating;
    protected Designer FDesigner;
    protected ButtonItem FParent;
    protected ListBox lbxActions;
    protected Label lblUndoRedo;
    protected ControlContainerItem FActionsHost;
    protected ControlContainerItem FLabelHost;

    private void SetSelected(int index)
    {
      if (FUpdating)
        return;
      FUpdating = true;
      int saveTop = lbxActions.TopIndex;
      lbxActions.BeginUpdate();
      lbxActions.SelectedIndices.Clear();
      if (lbxActions.Items.Count > 0)
      {
        for (int i = index; i >= 0; i--)
          lbxActions.SelectedIndices.Add(i);
      }
      lbxActions.TopIndex = saveTop;
      lbxActions.EndUpdate();
      UpdateLabel();
      FUpdating = false;
    }

    private void lbxActions_MouseMove(object sender, MouseEventArgs e)
    {
      int index = lbxActions.IndexFromPoint(e.X, e.Y);
      SetSelected(index);
    }

    private void lbxActions_MouseDown(object sender, MouseEventArgs e)
    {
      int actionsCount = lbxActions.SelectedItems.Count + 1;
      FParent.ClosePopup();
      DoUndoRedo(actionsCount);  
    }

    private void UpdateSizes()
    {
      float maxWidth = DrawUtils.MeasureString(String.Format(Res.Get("Designer,UndoRedo,UndoN"), 100), lblUndoRedo.Font).Width + 10;
      for (int i = 0; i < lbxActions.Items.Count; i++)
      {
        string s = (string)lbxActions.Items[i];
        float width = DrawUtils.MeasureString(s, lbxActions.Font).Width;
        if (width > maxWidth)
          maxWidth = width;
      }
      float maxHeight = (lbxActions.Items.Count > 10 ? 10 : lbxActions.Items.Count) * lbxActions.ItemHeight;
      FActionsHost.Size = new Size((int)maxWidth + 10, (int)maxHeight);
      lblUndoRedo.Size = new Size((int)maxWidth + 10, 20);
    }

    protected virtual void PopulateItems()
    {
    }

    protected virtual void UpdateLabel()
    {
    }
    
    protected virtual void DoUndoRedo(int actionsCount)
    {
    }

    private void parent_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      PopulateItems();
      UpdateSizes();
      SetSelected(0);
    }

    public UndoRedoDropDown(Designer designer, ButtonItem parent)
    {
      FDesigner = designer;
      FParent = parent;
      
      LayoutOrientation = eOrientation.Vertical;
      parent.PopupType = ePopupType.ToolBar;
      parent.SubItems.Add(this);
      parent.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(parent_PopupOpen);

      lbxActions = new ListBox();
      lbxActions.Size = new Size(150, 200);
      lbxActions.BorderStyle = BorderStyle.None;
      lbxActions.SelectionMode = SelectionMode.MultiSimple;
      lbxActions.MouseMove += new MouseEventHandler(lbxActions_MouseMove);
      lbxActions.MouseDown += new MouseEventHandler(lbxActions_MouseDown);

      FActionsHost = new ControlContainerItem();
      FActionsHost.Control = lbxActions;

      SubItems.Add(FActionsHost);

      lblUndoRedo = new Label();
      lblUndoRedo.AutoSize = false;
      lblUndoRedo.Size = new Size(150, 30);
      lblUndoRedo.TextAlign = ContentAlignment.MiddleCenter;
      lblUndoRedo.BackColor = Color.Transparent;

      FLabelHost = new ControlContainerItem();
      FLabelHost.Control = lblUndoRedo;
      
      SubItems.Add(FLabelHost);
    }

  }


  internal class UndoDropDown : UndoRedoDropDown
  {
    protected override void PopulateItems()
    {
      lbxActions.Items.Clear();
      if (FDesigner.ActiveReport != null)
      {
        string[] undoNames = FDesigner.ActiveReportTab.UndoRedo.UndoNames;
        foreach (string s in undoNames)
        {
          lbxActions.Items.Add(s);
        }
      }
    }

    protected override void UpdateLabel()
    {
      lblUndoRedo.Text = String.Format(Res.Get("Designer,UndoRedo,UndoN"), lbxActions.SelectedItems.Count);
    }

    protected override void DoUndoRedo(int actionsCount)
    {
      FDesigner.cmdUndo.Undo(actionsCount);
    }

    public UndoDropDown(Designer designer, ButtonItem parent)
      : base(designer, parent)
    {
    }
  }


  internal class RedoDropDown : UndoRedoDropDown
  {
    protected override void PopulateItems()
    {
      lbxActions.Items.Clear();
      if (FDesigner.ActiveReport != null)
      {
        string[] undoNames = FDesigner.ActiveReportTab.UndoRedo.RedoNames;
        foreach (string s in undoNames)
        {
          lbxActions.Items.Add(s);
        }
      }
    }

    protected override void UpdateLabel()
    {
      lblUndoRedo.Text = String.Format(Res.Get("Designer,UndoRedo,RedoN"), lbxActions.SelectedItems.Count);
    }

    protected override void DoUndoRedo(int actionsCount)
    {
      FDesigner.cmdRedo.Redo(actionsCount);
    }

    public RedoDropDown(Designer designer, ButtonItem parent)
      : base(designer, parent)
    {
    }
  }


}
