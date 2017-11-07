using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Utils;
using System.ComponentModel;

namespace FastReport.Dialog
{
  /// <summary>
  /// Base class for controls that may contain child controls.
  /// </summary>
  public class ParentControl : DialogControl, IParent
  {
    #region Fields
    private DialogComponentCollection FControls;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the collection of child controls.
    /// </summary>
    [Browsable(false)]
    public DialogComponentCollection Controls
    {
      get { return FControls; }
    }
    #endregion
    
    #region IParent
    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      foreach (DialogComponentBase c in FControls)
      {
        list.Add(c);
      }
    }

    /// <inheritdoc/>
    public virtual bool CanContain(Base child)
    {
      return (child is DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void AddChild(Base child)
    {
      if (child is DialogComponentBase)
        FControls.Add(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void RemoveChild(Base child)
    {
      if (child is DialogComponentBase)
        FControls.Remove(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual int GetChildOrder(Base child)
    {
      return FControls.IndexOf(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void SetChildOrder(Base child, int order)
    {
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (order > FControls.Count)
          order = FControls.Count;
        if (oldOrder <= order)
          order--;
        FControls.Remove(child as DialogComponentBase);
        FControls.Insert(order, child as DialogComponentBase);
      }
    }

    /// <inheritdoc/>
    public virtual void UpdateLayout(float dx, float dy)
    {
      // do nothing
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void DrawSelection(FRPaintEventArgs e)
    {
      base.DrawSelection(e);
      e.Graphics.DrawImage(Res.GetImage(75), (int)(AbsLeft + 8), (int)(AbsTop - 8));
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      HandleMouseHover(e);
      if (e.Handled)
        e.Mode = WorkspaceMode2.Move;
      else
      {
        base.HandleMouseDown(e);
        if (e.Handled)
        {
          if (e.ModifierKeys != Keys.Shift)
          {
            e.Mode = WorkspaceMode2.SelectionRect;
            e.ActiveObject = this;
          }
        }  
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      if (IsSelected && new RectangleF(AbsLeft + 8, AbsTop - 8, 16, 16).Contains(new PointF(e.X, e.Y)))
      {
        e.Handled = true;
        e.Cursor = Cursors.SizeAll;
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (e.ActiveObject == this && e.Mode == WorkspaceMode2.SelectionRect)
      {
        ObjectCollection selectedList = new ObjectCollection();
        // find objects inside the selection rect
        foreach (DialogComponentBase c in Controls)
        {
          e.Handled = false;
          c.HandleMouseUp(e);
          // object is inside
          if (e.Handled)
            selectedList.Add(c);
        }
        if (selectedList.Count > 0)
          selectedList.CopyTo(Report.Designer.SelectedObjects);
      }
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>ParentControl</b> class with default settings. 
    /// </summary>
    public ParentControl()
    {
      FControls = new DialogComponentCollection(this);
    }
  }
}
