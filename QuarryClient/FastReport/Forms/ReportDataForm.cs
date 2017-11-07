using System;
using System.Windows.Forms;
using FastReport.Data;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class ReportDataForm : BaseDialogForm
  {
    private Report FReport;

    private bool IsBusinessObjectNode(TreeNode node)
    {
      while (node != null)
      {
        if (node.Tag is BusinessObjectDataSource)
          return true;
        node = node.Parent;
      }
      return false;
    }
    
    private void UpdateNames(TreeNodeCollection root)
    {
      foreach (TreeNode node in root)
      {
        DataComponentBase data = node.Tag as DataComponentBase;
        if (data != null && !(data is DataConnectionBase))
          node.Text = cbAliases.Checked ? data.Alias : data.Name;
        UpdateNames(node.Nodes);
      }
    }

    private void UpdateNames()
    {
      tvData.BeginUpdate();
      UpdateNames(tvData.Nodes);
      tvData.EndUpdate();
    }

    private void CheckEnabled(TreeNodeCollection root)
    {
      foreach (TreeNode node in root)
      {
        DataComponentBase data = node.Tag as DataComponentBase;
        if (data != null && !(data is DataConnectionBase))
          data.Enabled = node.Checked;
        
        // do not check relation columns - they should be handled by its original datasources
        if (!(data is Relation))
          CheckEnabled(node.Nodes);  
      }
    }

    private void EnableItem(TreeNodeCollection root, DataComponentBase item)
    {
      foreach (TreeNode node in root)
      {
        DataComponentBase data = node.Tag as DataComponentBase;
        if (data == item)
        {
          node.Checked = true;
          break;
        }
        CheckEnabled(node.Nodes);
      }
    }

    private void ReportDataForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Config.SaveFormState(this);
      if (DialogResult == DialogResult.OK)
        Done();
    }

    private void tvData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      TreeNode node = e.Node;
      if (!node.Checked && IsBusinessObjectNode(node))
      {
        node.Checked = true;
      }
    }

    private void tvData_AfterCheck(object sender, TreeViewEventArgs e)
    {
      TreeNode node = e.Node;

      if (IsBusinessObjectNode(node))
      {
        BusinessObjectConverter conv = new BusinessObjectConverter(FReport.Dictionary);
        conv.CheckNode(node);
      }
      else
      {
        DataComponentBase data = node.Tag as DataComponentBase;
        if (node.Checked && data is Relation)
          EnableItem(tvData.Nodes, (data as Relation).ParentDataSource);
      }
    }

    private void cbAliases_CheckedChanged(object sender, EventArgs e)
    {
      UpdateNames();
    }

    private void Init()
    {
      tvData.CreateNodes(FReport.Dictionary);

      // remove existing business objects nodes
      for (int i = 0; i < tvData.Nodes.Count; i++)
      {
        if (tvData.Nodes[i].Tag is BusinessObjectDataSource)
        {
          tvData.Nodes.RemoveAt(i);
          i--;
        }
      }

      // create nodes using BOConverter
      BusinessObjectConverter conv = new BusinessObjectConverter(FReport.Dictionary);
      foreach (DataSourceBase data in FReport.Dictionary.DataSources)
      {
        if (data is BusinessObjectDataSource)
          conv.CreateTree(tvData.Nodes, data);
      }
    }
    
    private void Done()
    {
      CheckEnabled(tvData.Nodes);
      FReport.Dictionary.UpdateRelations();

      // create business objects based on tree
      BusinessObjectConverter conv = new BusinessObjectConverter(FReport.Dictionary);
      foreach (TreeNode node in tvData.Nodes)
      {
        if (node.Tag is BusinessObjectDataSource)
        {
          conv.CreateDataSource(node);
        }
      }
    }
    
    public override void Localize()
    {
      base.Localize();
      Text = Res.Get("Forms,ReportData");
      lblHint.Text = Res.Get("Forms,ReportData,Hint");
      cbAliases.Text = Res.Get("Forms,ReportData,Aliases");
    }
    
    public ReportDataForm(Report report)
    {
        FReport = report;
        InitializeComponent();
        Localize();
        Init();
        Config.RestoreFormState(this);

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            lblHint.Left = ClientSize.Width - lblHint.Left - lblHint.Width;
            lblHint.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tvData.RightToLeft = RightToLeft.Yes;
            tvData.RightToLeftLayout = true;
            cbAliases.Left = ClientSize.Width - cbAliases.Left - cbAliases.Width;
            cbAliases.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
            btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
        }
    }
  }
}

