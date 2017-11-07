using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.DevComponents.AdvTree;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Preview
{
  internal class OutlineControl : UserControl
  {
    private AdvTree FTree;
    private NodeConnector nodeConnector1;
    private ElementStyle elementStyle1;
    private PreviewControl FPreview;
    private PreparedPages FPreparedPages;
    private UIStyle FStyle;
    
    internal PreparedPages PreparedPages
    {
      get { return FPreparedPages; }
      set
      {
        FPreparedPages = value;
        UpdateContent();
      }
    }
    
    internal UIStyle Style
    {
      get { return FStyle; }
      set
      {
        FStyle = value;

        eScrollBarAppearance appearance = eScrollBarAppearance.Default;
        if (FTree.ColorSchemeStyle == eColorSchemeStyle.Office2007)
          appearance = eScrollBarAppearance.ApplicationScroll;
        if (FTree.HScrollBar != null)
          FTree.HScrollBar.Appearance = appearance;
        if (FTree.VScrollBar != null)
          FTree.VScrollBar.Appearance = appearance;

        FTree.Refresh();
      }
    }

    private void EnumNodes(XmlItem rootItem, NodeCollection rootNode)
    {
      // skip root xml item
      if (rootItem.Parent != null)
      {
        string text = rootItem.GetProp("Text");
        Node node = new Node();
        node.Text = text;
        node.Tag = rootItem;
        rootNode.Add(node);
        rootNode = node.Nodes;
      }
      
      for (int i = 0; i < rootItem.Count; i++)
      {
        EnumNodes(rootItem[i], rootNode);
      }
    }
    
    private void UpdateContent()
    {
      FTree.Nodes.Clear();
      if (PreparedPages == null)
        return;
      
      Outline outline = PreparedPages.Outline;
      FTree.BeginUpdate();
      EnumNodes(outline.Xml, FTree.Nodes);
      if (FTree.Nodes.Count == 1)
        FTree.Nodes[0].Expand();
      FTree.EndUpdate();
      
      // to update tree's scrollbars
      Style = Style;
    }

    private void FTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
    {
      // avoid bug when closing the preview
      if (!Visible)
        return;
      if (e.Node == null)
        return;
        
      XmlItem item = e.Node.Tag as XmlItem;
      string s = item.GetProp("Page");
      if (s != "")
      {
        int pageNo = int.Parse(s);
        s = item.GetProp("Offset");
        if (s != "")
        {
          float offset = (float)Converter.FromString(typeof(float), s);
          FPreview.PositionTo(pageNo + 1, new PointF(0, offset));
        }
      }
    }

    internal void SetPreview(PreviewControl preview)
    {
      FPreview = preview;
    }
    
    /// <summary>
    /// Initializes a new instance of the <b>OutlineControl</b> class with default settings. 
    /// </summary>
    public OutlineControl()
    {
      nodeConnector1 = new NodeConnector();
      nodeConnector1.LineColor = SystemColors.ControlText;
      elementStyle1 = new ElementStyle();
      elementStyle1.TextColor = SystemColors.ControlText;

      FTree = new AdvTree();
      FTree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
      FTree.AntiAlias = false;
      FTree.Font = DrawUtils.DefaultFont;
      FTree.BackColor = SystemColors.Window;
      FTree.Dock = DockStyle.Fill;
      FTree.HideSelection = false;
      FTree.HotTracking = true;
      FTree.NodesConnector = nodeConnector1;
      FTree.NodeStyle = elementStyle1;
      FTree.Styles.Add(elementStyle1);
      FTree.AfterNodeSelect += new AdvTreeNodeEventHandler(FTree_AfterNodeSelect);

      Controls.Add(FTree);
    }
  }
}
