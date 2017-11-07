using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FastReport.Controls
{
    /// <summary>
    /// TreeView control with multiselect support.
    /// </summary>
    /// <remarks>
    /// This control is for internal use only.
    /// </remarks>
    public class TreeViewMultiSelect : TreeView
    {
        List<TreeNode> selectedNodes;

        bool cancelNextSelection = false;
        bool passSelection = false;

        /// <summary>
        /// Creates a new instance of the TreeViewMultiSelect control.
        /// </summary>
        public TreeViewMultiSelect()
        {
            selectedNodes = new List<TreeNode>();
        }

        /// <summary>
        /// Gets a copy of list of selected nodes.
        /// </summary>
        public List<TreeNode> SelectedNodes
        {
            get
            {
                return new List<TreeNode>(selectedNodes);
            }
            /*set
            {
                if (value == null)
                {
                    unhighlightSelectedNodes();
                    selectedNodes.Clear();
                }
                else
                {
                    unhighlightSelectedNodes();
                    selectedNodes = value;
                    highlightSelectedNodes();
                }

                todo: update this.SelectedNode
            }*/
        }

        #region Triggers

        /// <inheritdoc/>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            paintSelectedNodes();
        }

        /// <inheritdoc/>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            paintSelectedNodes();
        }

        /// <inheritdoc/>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            bool control = (ModifierKeys == Keys.Control);

            if (control && SelectedNode == e.Node && selectedNodes.Contains(e.Node) && selectedNodes.Count > 1)
            {
                passSelection = true;
                SelectedNode = selectedNodes[selectedNodes.Count - 2];
                passSelection = false;

                unpaintSelectedNodes();
                selectedNodes.Remove(e.Node);
                paintSelectedNodes();

                cancelNextSelection = true;
            }
        }

        /// <inheritdoc/>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            if (passSelection)
                return;

            if (cancelNextSelection)
            {
                e.Cancel = true;
                cancelNextSelection = false;
                return;
            }

            bool control = (ModifierKeys == Keys.Control);

            if (control && selectedNodes.Contains(e.Node))
            {
                e.Cancel = true;
                unpaintSelectedNodes();
                selectedNodes.Remove(e.Node);
                paintSelectedNodes();
                return;
            }
        }

        /// <inheritdoc/>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            if (passSelection)
                return;

            bool control = (ModifierKeys == Keys.Control);
            bool shift = (ModifierKeys == Keys.Shift);

            if (control && !shift)
            {
                if (selectedNodes.Contains(e.Node))
                {
                    unpaintSelectedNodes();
                    selectedNodes.Remove(e.Node);
                    paintSelectedNodes();
                }
                else
                {
                    unpaintSelectedNodes();
                    selectedNodes.Add(e.Node);
                    paintSelectedNodes();
                }
            }
            else if (!control && shift)
            {
                if (selectedNodes.Count > 0 && isSameLevel(selectedNodes[0], e.Node))
                {
                    List<TreeNode> nodes = getNodesOnSameLevel(selectedNodes[0]);

                    int from = Math.Min(selectedNodes[0].Index, e.Node.Index);
                    int to = Math.Max(selectedNodes[0].Index, e.Node.Index);
                    bool needReverse = selectedNodes[0].Index > e.Node.Index;

                    unpaintSelectedNodes();
                    selectedNodes.Clear();

                    for (int i = from; i <= to; i++)
                        selectedNodes.Add(nodes[i]);

                    if (needReverse)
                        selectedNodes.Reverse();

                    paintSelectedNodes();
                }
            }
            else
            {
                unpaintSelectedNodes();
                selectedNodes.Clear();
                selectedNodes.Add(e.Node);
                paintSelectedNodes();
            }
        }

        #endregion

        #region Helpers

        bool isSameLevel(TreeNode node1, TreeNode node2)
        {
            TreeNode next = node1.NextNode;
            while (next != null)
            {
                if (next == node2)
                    return true;

                next = next.NextNode;
            }

            TreeNode prev = node1.PrevNode;
            while (prev != null)
            {
                if (prev == node2)
                    return true;

                prev = prev.PrevNode;
            }

            return false;
        }

        List<TreeNode> getNodesOnSameLevel(TreeNode node)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            nodes.Add(node);

            TreeNode next = node.NextNode;
            while (next != null)
            {
                nodes.Add(next);
                next = next.NextNode;
            }

            TreeNode prev = node.PrevNode;
            while (prev != null)
            {
                nodes.Insert(0, prev);
                prev = prev.PrevNode;
            }

            return nodes;
        }

        void paintSelectedNodes()
        {
            Color backColor = Focused ? SystemColors.Highlight : SystemColors.Control;
            Color foreColor = Focused ? SystemColors.HighlightText : ForeColor;

            foreach (TreeNode node in selectedNodes)
            {
                node.BackColor = backColor;
                node.ForeColor = foreColor;
            }
        }

        void unpaintSelectedNodes()
        {
            foreach (TreeNode node in selectedNodes)
            {
                node.BackColor = BackColor;
                node.ForeColor = ForeColor;
            }
        }

        #endregion
    }
}
