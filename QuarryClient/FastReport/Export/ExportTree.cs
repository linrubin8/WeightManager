using System;
using System.Collections.Generic;

namespace FastReport.Export
{
    /// <summary>
    /// Binary tree class
    /// </summary>
    internal class BinaryTree
    {
        #region Private variables and constants
        // Acceptable disbalance in branches.
        const int DISBALANCE = 3;        

        private int count;
        private int index;
        private BinaryTreeNode root;
        private BinaryTreeNode[] nodes;
        private float inaccuracy;
        private float maxDistance;
        private float prevValue;
        #endregion

        #region Public properties

        /// <summary>
        /// Maximal value between child and parent
        /// </summary>
        public float MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        /// <summary>
        /// Nodes count
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Root node
        /// </summary>
        public BinaryTreeNode RootNode
        {
            get { return root; }
        }

        /// <summary>
        /// Nodes array. Accending sorting by node value. Available after close of tree.
        /// </summary>
        public BinaryTreeNode[] Nodes
        {
            get { return nodes; }
        }

        /// <summary>
        /// Accecptable inaccuracy of new values. 
        /// </summary>
        public float Inaccuracy
        {
            get { return inaccuracy; }
            set { inaccuracy = value; }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Recursive add value to a node.  
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int AddChild(ref BinaryTreeNode node, float value)
        {
            if (node == null)
            {
                node = new BinaryTreeNode(value);
                count++;
            }
            else
            {
                float diff = value - node.value;
                if (diff < -inaccuracy)
                    if (node.left == null)
                    {
                        node.left = new BinaryTreeNode(value);
                        node.leftCount = 1;
                        count++;
                    }
                    else
                    {
                        node.leftCount = AddChild(ref node.left, value);
                        if (node.leftCount > node.rightCount + DISBALANCE)
                            PollLeft(ref node);
                    }
                else if (diff > inaccuracy)
                    if (node.right == null)
                    {
                        node.right = new BinaryTreeNode(value);
                        node.rightCount = 1;
                        count++;
                    }
                    else
                    {
                        node.rightCount = AddChild(ref node.right, value);
                        if (node.leftCount + DISBALANCE < node.rightCount)
                            PollRight(ref node);
                    }
            }
            return node.leftCount + node.rightCount + 1;
        }

        /// <summary>
        /// Poll right child node for correct balance.
        /// </summary>
        /// <param name="node"></param>
        private void PollRight(ref BinaryTreeNode node)
        {
            BinaryTreeNode top = node.right;
            BinaryTreeNode rightLeft = top.left;
            top.left = node;
            node.right = rightLeft;
            node.rightCount = (node.right == null) ? 0 : node.right.leftCount + node.right.rightCount + 1;
            top.leftCount = node.leftCount + node.rightCount + 1;
            node = top;
        }

        /// <summary>
        /// Poll left child for correct balance.
        /// </summary>
        /// <param name="node"></param>
        private void PollLeft(ref BinaryTreeNode node)
        {
            BinaryTreeNode top = node.left;
            BinaryTreeNode leftRight = top.right;
            top.right = node;
            node.left = leftRight;
            node.leftCount = (node.left == null) ? 0 : node.left.leftCount + node.left.rightCount + 1;
            top.rightCount = node.leftCount + node.rightCount + 1;
            node = top;
        }

        /// <summary>
        /// Recursive indexation of node and childs.
        /// </summary>
        /// <param name="node"></param>
        private void Index(BinaryTreeNode node)
        {
            if (node.left != null)
                Index(node.left);

            if (maxDistance > 0 && index > 0 && node.value - prevValue > maxDistance + inaccuracy)
            {
                float value = prevValue;
                while (value < node.value - maxDistance)
                {
                    value += maxDistance;
                    AddChild(ref root, value);
                }
                prevValue = node.value;
            }
            else if (index < nodes.Length)
            {
                node.index = index;
                nodes[index++] = node;
                prevValue = node.value;                
            }

            if (node.right != null)
                Index(node.right);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Add new value in tree. All equals are skipped.
        /// </summary>
        /// <param name="value"></param>
        public void Add(float value)
        {
            AddChild(ref root, value);
        }

        /// <summary>
        /// Close the tree and make index array.
        /// </summary>
        public void Close()
        {
            int oldCount = 0;
            while (oldCount != count)
            {
                nodes = new BinaryTreeNode[count];
                index = 0;
                oldCount = count;
                Index(root);
            }
        }

        /// <summary>
        /// Seek of value index in the tree.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(float value)
        {
            return Find(root, value);
        }

        /// <summary>
        /// Find of value index in sub-tree of node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Find(BinaryTreeNode node, float value)
        {
            if (node == null)
                return -1;
            if (Math.Abs(value - node.value) <= inaccuracy)
                return node.index;
            else if (value < node.value)
                return Find(node.left, value);
            else
                return Find(node.right, value);
        }

        /// <summary>
        /// Borrow values form List in the tree
        /// </summary>
        /// <param name="array"></param>
        public void FromList(List<float> array)
        {
            foreach (float value in array)
                AddChild(ref root, value);
        }

        /// <summary>
        /// Borrow values form array in the tree
        /// </summary>
        /// <param name="array"></param>
        public void FromArray(float[] array)
        {
            foreach (float value in array)
                AddChild(ref root, value);
        }

        /// <summary>
        /// Clear tree
        /// </summary>
        public void Clear()
        {
            count = 0;
            root = null;
            nodes = null;          
        }
        #endregion

        /// <summary>
        /// Tree constructor
        /// </summary>
        public BinaryTree()
        {
            Clear();           
        }
    }

    /// <summary>
    /// Tree node class
    /// </summary>
    internal class BinaryTreeNode
    {
        /// <summary>
        /// Link to left child
        /// </summary>
        public BinaryTreeNode left;
        
        /// <summary>
        /// Link to right child
        /// </summary>
        public BinaryTreeNode right;
               
        /// <summary>
        /// Node value
        /// </summary>
        public float value;

        /// <summary>
        /// Count of nodes in left sub-tree
        /// </summary>
        public int leftCount;
        
        /// <summary>
        /// Count of nodes in right sub-tree
        /// </summary>
        public int rightCount;

        /// <summary>
        /// Node index
        /// </summary>
        public int index;

        /// <summary>
        /// Node constructor
        /// </summary>
        /// <param name="nodeValue"></param>
        public BinaryTreeNode(float nodeValue)
        {
            value = nodeValue;
            index = -1;
        }
    }
}
