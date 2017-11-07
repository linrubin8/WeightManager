using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.ToolWindows
{
  /// <summary>
  /// Represents the "Report Tree" window.
  /// </summary>
  public class ReportTreeWindow : ToolWindowBase
  {
    #region Fields
    private TreeView FTree;
    private List<Base> FComponents;
    private List<TreeNode> FNodes;
    private bool FUpdating;
    #endregion

    #region Private Methods
    private void UpdateTree()
    {
      // if there was no changes in the report structure, do nothing
      if (Designer.ActiveReport != null && FTree.Nodes.Count > 0)
      {
        if (CheckChanges(Designer.ActiveReport, FTree.Nodes[0]))
          return;
      }
      
      FUpdating = true;
      FTree.BeginUpdate();
      FTree.Nodes.Clear();
      FComponents.Clear();
      FNodes.Clear();
      if (Designer.ActiveReport != null)
        EnumComponents(Designer.ActiveReport, FTree.Nodes);
      FTree.ExpandAll();
      FTree.EndUpdate();
      FUpdating = false;
    }

    private void UpdateSelection()
    {
      if (FUpdating)
        return;
      if (Designer.SelectedObjects == null || Designer.SelectedObjects.Count == 0)
        return;

      Base c = Designer.SelectedObjects[0];
      int i = FComponents.IndexOf(c);
      if (i != -1)
      {
        FUpdating = true;
        FTree.SelectedNode = FNodes[i];
        FUpdating = false;
      }
    }

    private void EnumComponents(Base rootComponent, TreeNodeCollection rootNode)
    {
      string name = rootComponent is Report ? 
        "Report - " + Designer.ActiveReportTab.ReportName : rootComponent.Name;
      TreeNode node = rootNode.Add(name);
      node.Tag = rootComponent;
      
      FComponents.Add(rootComponent);
      FNodes.Add(node);
      
      ObjectInfo objItem = RegisteredObjects.FindObject(rootComponent);
      if (objItem != null)
      {
        int imageIndex = objItem.ImageIndex;
        node.ImageIndex = imageIndex;
        node.SelectedImageIndex = imageIndex;
      }

      if (rootComponent.HasFlag(Flags.CanShowChildrenInReportTree))
      {
        foreach (Base component in rootComponent.ChildObjects)
          EnumComponents(component, node.Nodes);
      }
    }
    
    private bool CheckChanges(Base rootComponent, TreeNode rootNode)
    {
      if (rootNode.Tag != rootComponent)
        return false;
      if (!(rootComponent is Report))
      {
        if (rootNode.Text != rootComponent.Name)
          return false;
      }
      if (!rootComponent.HasFlag(Flags.CanShowChildrenInReportTree))
        return true;

      ObjectCollection childObjects = rootComponent.ChildObjects;
      if (childObjects.Count != rootNode.Nodes.Count)
        return false;
      for (int i = 0; i < childObjects.Count; i++)
      {
        if (!CheckChanges(childObjects[i], rootNode.Nodes[i]))
          return false;
      }
      return true;
    }

    private void tree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (FUpdating)
        return;
      Base c = e.Node.Tag as Base;
      FUpdating = true;
      if (!(c is Report))
        Designer.ActiveReportTab.ActivePage = c.Page;
      FUpdating = false;
      if (Designer.SelectedObjects != null)
      {
        Designer.SelectedObjects.Clear();
        Designer.SelectedObjects.Add(c);
        Designer.SelectionChanged(null);
      }  
    }

    private void tree_ItemDrag(object sender, ItemDragEventArgs e)
    {
      if (e.Item is TreeNode && (e.Item as TreeNode).Parent != null)
      {
        FTree.SelectedNode = e.Item as TreeNode;
        Base draggedComponent = FTree.SelectedNode.Tag as Base;
        if (draggedComponent is ComponentBase && 
          (draggedComponent.IsAncestor || !draggedComponent.HasFlag(Flags.CanChangeParent)))
          FTree.DoDragDrop(e.Item, DragDropEffects.None);
        else  
          FTree.DoDragDrop(e.Item, DragDropEffects.Move);
      }  
    }

    private void tree_DragDrop(object sender, DragEventArgs e)
    {
      TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
      TreeNode targetNode = FTree.GetNodeAt(FTree.PointToClient(new Point(e.X, e.Y)));
      Base draggedComponent = draggedNode.Tag as Base;
      Base targetComponent = targetNode.Tag as Base;

      // cases: 
      // - target can contain dragged. Just change parent.
      // - target cannot contain dragged. Change creation order (Z-order).
      if (targetComponent is IParent && (targetComponent as IParent).CanContain(draggedComponent))
      {
        draggedComponent.Parent = targetComponent;
      }
      else
      {
        Base parent = targetComponent.Parent;
        draggedComponent.Parent = parent;
        draggedComponent.ZOrder = targetComponent.ZOrder;
      }

      FTree.SelectedNode = targetNode;
      // update all designer plugins (this one too)
      Designer.SetModified(null, "ChangeParent");
      UpdateSelection();
    }

    private void tree_DragOver(object sender, DragEventArgs e)
    {
      TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
      TreeNode targetNode = FTree.GetNodeAt(FTree.PointToClient(new Point(e.X, e.Y)));
      Base draggedComponent = draggedNode.Tag as Base;
      Base targetComponent = targetNode.Tag as Base;

      // allowed moves are:
      // - target is not dragged
      // - target is not child of dragged
      // - target can contain dragged, or 
      // parent of target can contain dragged
      if (targetComponent != draggedComponent && 
        !targetComponent.HasParent(draggedComponent) &&
        (((targetComponent is IParent) && (targetComponent as IParent).CanContain(draggedComponent)) ||
        (targetComponent.Parent != null && (targetComponent.Parent as IParent).CanContain(draggedComponent))))
        e.Effect = e.AllowedEffect;
      else
      {
        e.Effect = DragDropEffects.None;
        targetNode = draggedNode;
      }

      // disable the Designer.OnSelectionChanged
      FUpdating = true;
      FTree.SelectedNode = targetNode;
      FUpdating = false;
    }

    private void tree_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
        Designer.cmdDelete.Invoke();
    }

    private void tree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      Base c = e.Node.Tag as Base;
      if (c is Report)
        e.CancelEdit = true;
    }

    private void tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      if (e.Label != null)
      {
        Base c = e.Node.Tag as Base;
        string saveName = c.Name;
        try
        {
          c.Name = e.Label;
          Designer.SetModified(this, "Change");
        }
        catch (Exception ex)
        {
          FRMessageBox.Error(ex.Message);
          e.CancelEdit = true;
        }
      }
    }

    private void FTree_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        FTree.SelectedNode = null;
        FTree.SelectedNode = FTree.GetNodeAt(e.Location);
        if (FTree.SelectedNode != null)
        {
          ContextMenuBar menu = (FTree.SelectedNode.Tag as Base).GetContextMenu();
          if (menu != null)
          {
            PopupItem item = menu.Items[0] as PopupItem;
            item.PopupMenu(FTree.PointToScreen(e.Location));
          }  
        }
      }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      UpdateSelection();
    }

    /// <inheritdoc/>
    public override void UpdateContent()
    {
      UpdateTree();
      UpdateSelection();
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      Text = Res.Get("Designer,ToolWindow,ReportTree");
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportTreeWindow"/> class with default settings.
    /// </summary>
    /// <param name="designer">The report designer.</param>
    public ReportTreeWindow(Designer designer) : base(designer)
    {
      Name = "ReportTreeWindow";
      Image = Res.GetImage(189);

      FComponents = new List<Base>();
      FNodes = new List<TreeNode>();
      
      FTree = new TreeView();
      FTree.Dock = DockStyle.Fill;
      FTree.BorderStyle = BorderStyle.None;
      FTree.ShowRootLines = false;
      FTree.HideSelection = false;
      FTree.LabelEdit = true;
      FTree.ImageList = Res.GetImages();
      FTree.AllowDrop = true;
      FTree.AfterSelect += tree_AfterSelect;
      FTree.ItemDrag += tree_ItemDrag;
      FTree.DragOver += tree_DragOver;
      FTree.DragDrop += tree_DragDrop;
      FTree.KeyDown += tree_KeyDown;
      FTree.BeforeLabelEdit += tree_BeforeLabelEdit;
      FTree.AfterLabelEdit += tree_AfterLabelEdit;
      FTree.MouseUp += FTree_MouseUp;
      ParentControl.Controls.Add(FTree);
      Localize();
    }
  }

}
