using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Controls
{
  internal class DataColumnDropDown : ToolStripDropDown
  {
    private ToolStripControlHost FHost;
    private DataTreeView FTree;

    public event EventHandler ColumnSelected;

    public DataTreeView DataTree
    {
      get { return FTree; }
    }
    
    public DataSourceBase DataSource
    {
      get { return FTree.DataSource; }
      set { FTree.DataSource = value; }
    }

    public string Column
    {
      get { return FTree.SelectedItem; }
      set { FTree.SelectedItem = value; }
    }

    private void FTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if ((e.Node.Tag == null || Column != "") && ColumnSelected != null)
      {
        Close();
        ColumnSelected(this, EventArgs.Empty);
      }
    }

    public void CreateNodes(Report report)
    {
      FTree.CreateNodes(report.Dictionary);
    }

    public void SetSize(int width, int height)
    {
      FTree.Size = new Size(width - 2, height);
    }

    public DataColumnDropDown()
    {
      FTree = new DataTreeView();
      FTree.Size = new Size(200, 250);
      FTree.BorderStyle = BorderStyle.None;
      FTree.HideSelection = false;
      FTree.ShowEnabledOnly = true;
      FTree.ShowNone = true;
      FTree.ShowRelations = true;
      FTree.ShowVariables = false;
      FTree.ShowParameters = false;
      FTree.Font = DrawUtils.DefaultFont;
      FTree.AfterSelect += new TreeViewEventHandler(FTree_AfterSelect);
      FHost = new ToolStripControlHost(FTree);
      Items.Add(FHost);
    }
  }
}
