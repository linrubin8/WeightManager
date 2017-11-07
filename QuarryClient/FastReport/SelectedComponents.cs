using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using System.Drawing;
using System.Collections;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Holds the list of <see cref="ComponentBase"/> objects currently selected in the designer.
  /// </summary>
  /// <remarks>
  /// This class is used by the "Alignment" toolbar. Use methods of this class to perform some 
  /// operations on the selected objects. 
  /// <para/>Note: after calling any method in this class, call the 
  /// <see cref="Designer.SetModified()">Designer.SetModified</see> method to reflect changes.
  /// <para/>Note: this list contains only objects of <see cref="ComponentBase"/> type. If you want to access all
  /// selected objects, use the <see cref="Designer.SelectedObjects"/> property.
  /// </remarks>
  public class SelectedComponents
  {
    private List<ComponentBase> FList;
    private Designer FDesigner;

    /// <summary>
    /// Gets the first selected object.
    /// </summary>
    public ComponentBase First
    {
      get { return FList.Count > 0 ? FList[0] : null; }
    }

    /// <summary>
    /// Gets the number of selected objects.
    /// </summary>
    public int Count
    {
      get { return FList.Count; }
    }

    private List<ComponentBase> MoveList
    {
      get { return FList.FindAll(CanMove); }
    }

    private List<ComponentBase> ResizeList
    {
      get { return FList.FindAll(CanResize); }
    }

    private bool CanMove(ComponentBase c)
    {
      return c.HasFlag(Flags.CanMove) && !c.HasRestriction(Restrictions.DontMove);
    }

    private bool CanResize(ComponentBase c)
    {
      return c.HasFlag(Flags.CanResize) && !c.HasRestriction(Restrictions.DontResize);
    }

    internal void Update()
    {
      FList.Clear();
      if (FDesigner.SelectedObjects != null)
      {
        foreach (Base c in FDesigner.SelectedObjects)
        {
          if (c is ComponentBase)
            FList.Add(c as ComponentBase);
        }
      }
    }
    
    /// <summary>
    /// Aligns left edges of the selected objects.
    /// </summary>
    public void AlignLeft()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Left = First.Left;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns right edges of the selected objects.
    /// </summary>
    public void AlignRight()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Left = First.Right - c.Width;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns centers of the selected objects.
    /// </summary>
    public void AlignCenter()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Left = First.Left + (First.Width - c.Width) / 2;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns top edges of the selected objects.
    /// </summary>
    public void AlignTop()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Top = First.Top;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns bottom edges of the selected objects.
    /// </summary>
    public void AlignBottom()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Top = First.Bottom - c.Height;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns middles of the selected objects.
    /// </summary>
    public void AlignMiddle()
    {
      foreach (ComponentBase c in MoveList)
      {
        c.Top = First.Top + (First.Height - c.Height) / 2;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Makes the selected objects the same width as the first object.
    /// </summary>
    public void SameWidth()
    {
      foreach (ComponentBase c in ResizeList)
      {
        c.Width = First.Width;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Makes the selected objects the same height as the first object.
    /// </summary>
    public void SameHeight()
    {
      foreach (ComponentBase c in ResizeList)
      {
        c.Height = First.Height;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Makes the selected objects the same size as the first object.
    /// </summary>
    public void SameSize()
    {
      foreach (ComponentBase c in ResizeList)
      {
        c.Width = First.Width;
        c.Height = First.Height;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Centers the selected objects horizontally.
    /// </summary>
    public void CenterHorizontally()
    {
      if (!(First.Parent is ComponentBase))
        return;
      float min = 100000;
      float max = -100000;
      
      foreach (ComponentBase c in MoveList)
      {
        if (c.Left < min)
          min = c.Left;
        if (c.Right > max)
          max = c.Right;
      }

      float parentWidth = (First.Parent as ComponentBase).ClientSize.Width;
      float dx = (parentWidth - (max - min)) / 2;
      foreach (ComponentBase c in MoveList)
      {
        c.Left += dx - min; 
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Centers the selected objects vertically.
    /// </summary>
    public void CenterVertically()
    {
      if (!(First.Parent is ComponentBase))
        return;
      float min = 100000;
      float max = -100000;

      foreach (ComponentBase c in MoveList)
      {
        if (c.Top < min)
          min = c.Top;
        if (c.Bottom > max)
          max = c.Bottom;
      }

      float parentHeight = (First.Parent as ComponentBase).ClientSize.Height;
      float dy = (parentHeight - (max - min)) / 2;
      foreach (ComponentBase c in MoveList)
      {
        c.Top += dy - min;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Aligns the selected objects to the grid.
    /// </summary>
    public void AlignToGrid()
    {
      SizeF snapSize = First.Page.SnapSize;
      foreach (ComponentBase c in MoveList)
      {
        c.Left = (int)Math.Round(c.Left / snapSize.Width) * snapSize.Width;
        c.Top = (int)Math.Round(c.Top / snapSize.Height) * snapSize.Height;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Adjusts the size of selected objects to the grid.
    /// </summary>
    public void SizeToGrid()
    {
      SizeF snapSize = First.Page.SnapSize;
      foreach (ComponentBase c in FList)
      {
        if (c.HasFlag(Flags.CanMove) && !c.HasRestriction(Restrictions.DontMove))
        {
          c.Left = (int)Math.Round(c.Left / snapSize.Width) * snapSize.Width;
          c.Top = (int)Math.Round(c.Top / snapSize.Height) * snapSize.Height;
        }
        if (c.HasFlag(Flags.CanResize) && !c.HasRestriction(Restrictions.DontResize))
        {
          c.Width = (int)Math.Round(c.Width / snapSize.Width) * snapSize.Width;
          c.Height = (int)Math.Round(c.Height / snapSize.Height) * snapSize.Height;
        }
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Spaces the selected objects horizontally.
    /// </summary>
    public void SpaceHorizontally()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Left, c));
      }
      list.Sort();
      if (list.Count < 3)
        return;
      
      float dx = list[list.Count - 1].Obj.Left - list[0].Obj.Right;
      float actualSize = 0;
      for (int i = 1; i < list.Count - 1; i++)
      {
        actualSize += list[i].Obj.Width;
      }
      float sizeBetweenObj = actualSize < dx ? (dx - actualSize) / (list.Count - 1) : 0;
      float count = sizeBetweenObj > 0 ? list.Count - 1 : list.Count;
      for (int i = 1; i < count; i++)
      {
        list[i].Obj.Left = list[i - 1].Obj.Right + sizeBetweenObj;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Increases horizontal spacing between the selected objects.
    /// </summary>
    public void IncreaseHorizontalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Left, c));
      }
      list.Sort();

      SizeF snapSize = First.Page.SnapSize;
      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Left += snapSize.Width * i;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Decreases horizontal spacing between the selected objects.
    /// </summary>
    public void DecreaseHorizontalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Left, c));
      }
      list.Sort();

      SizeF snapSize = First.Page.SnapSize;
      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Left -= snapSize.Width * i;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Removes horizontal spacing between the selected objects.
    /// </summary>
    public void RemoveHorizontalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Left, c));
      }
      list.Sort();

      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Left = list[i - 1].Obj.Right;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Spaces the selected objects vertically.
    /// </summary>
    public void SpaceVertically()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Top, c));
      }
      list.Sort();
      if (list.Count < 3)
        return;

      float dy = list[list.Count - 1].Obj.Top - list[0].Obj.Bottom;
      float actualSize = 0;
      for (int i = 1; i < list.Count - 1; i++)
      {
        actualSize += list[i].Obj.Height;
      }
      float sizeBetweenObj = actualSize < dy ? (dy - actualSize) / (list.Count - 1) : 0;
      float count = sizeBetweenObj > 0 ? list.Count - 1 : list.Count;
      for (int i = 1; i < count; i++)
      {
        list[i].Obj.Top = list[i - 1].Obj.Bottom + sizeBetweenObj;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Increases vertical spacing between the selected objects.
    /// </summary>
    public void IncreaseVerticalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Top, c));
      }
      list.Sort();

      SizeF snapSize = First.Page.SnapSize;
      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Top += snapSize.Height * i;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Decreases vertical spacing between the selected objects.
    /// </summary>
    public void DecreaseVerticalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Top, c));
      }
      list.Sort();

      SizeF snapSize = First.Page.SnapSize;
      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Top -= snapSize.Height * i;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Removes vertical spacing between the selected objects.
    /// </summary>
    public void RemoveVerticalSpacing()
    {
      List<ObjItem> list = new List<ObjItem>();
      foreach (ComponentBase c in MoveList)
      {
        list.Add(new ObjItem(c.Left, c));
      }
      list.Sort();

      for (int i = 1; i < list.Count; i++)
      {
        list[i].Obj.Top = list[i - 1].Obj.Bottom;
      }
      FDesigner.SetModified();
    }

    internal SelectedComponents(Designer designer)
    {
      FDesigner = designer;
      FList = new List<ComponentBase>();
    }


    private class ObjItem : IComparable
    {
      public float Coord;
      public ComponentBase Obj;

      public int CompareTo(object obj)
      {
        if (Coord < (obj as ObjItem).Coord)
          return -1;
        if (Coord > (obj as ObjItem).Coord)
          return 1;
        return 0;
      }

      public ObjItem(float coord, ComponentBase obj)
      {
        Coord = coord;
        Obj = obj;
      }
    }
  }
}
