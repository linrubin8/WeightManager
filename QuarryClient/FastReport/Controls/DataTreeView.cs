using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using FastReport.Data;
using FastReport.Utils;
using FastReport.Dialog;

namespace FastReport.Controls
{
  /// <summary>
  /// This is an internal enum that represents the selected datatree item type.
  /// </summary>
  public enum DataTreeSelectedItemType
  {
    /// <summary>
    /// Nothing selected.
    /// </summary>
    None,
    
    /// <summary>
    /// Data column selected.
    /// </summary>
    DataColumn,
    
    /// <summary>
    /// Total selected.
    /// </summary>
    Total,
    
    /// <summary>
    /// Parameter selected.
    /// </summary>
    Parameter,
    
    /// <summary>
    /// Function selected.
    /// </summary>
    Function,
    
    /// <summary>
    /// Dialog control selected.
    /// </summary>
    DialogControl
  }
  
  /// <summary>
  /// This is an internal control that represents the data tree.
  /// </summary>
  /// <remarks>
  /// This control is for internal use only.
  /// </remarks>
  [ToolboxItem(false)]
  public class DataTreeView : TreeView
  {
    private string FSelectedItem;
    private bool FShowDataSources;
    private bool FShowColumns;
    private bool FShowParameters;
    private bool FShowVariables;
    private bool FShowNone;
    private bool FShowRelations;
    private bool FShowEnabledOnly;
    private bool FShowTotals;
    private bool FShowFunctions;
    private bool FShowDialogs;
    private DataSourceBase FDataSource;

    /// <summary>
    /// DataSource property
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataSourceBase DataSource
    {
      get { return FDataSource; }
      set
      {
        FDataSource = value;
        if (value != null)
          RemoveDataSourcesExcept(Nodes, value);
      }
    }

    private void RemoveDataSourcesExcept(TreeNodeCollection nodes, DataSourceBase value)
    {
      for (int i = 0; i < nodes.Count; i++)
      {
        TreeNode node = nodes[i];
        if (node.Tag == value || value.HasParent(node.Tag as Base))
        {
          node.Expand();
          continue;
        }  
        if (node.Tag is DataConnectionBase)
          RemoveDataSourcesExcept(node.Nodes, value);
        else
        {
          node.Remove();
          i--;
        }  
      }
    }
    
    /// <summary>
    /// SelectedItem property
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedItem
    {
      get 
      { 
        FSelectedItem = "";
        TreeNode node = SelectedNode;
        if (node != null)
        {
          if (node.Tag is Column && !(node.Tag is DataSourceBase))
          {
            while (true)
            {
              if (node.Tag is DataSourceBase)
              {
                FSelectedItem = (node.Tag as DataSourceBase).FullName + "." + FSelectedItem;
                break;
              }  
              FSelectedItem = node.Text + (FSelectedItem == "" ? "" : ".") + FSelectedItem;
              node = node.Parent;
            }
          }
          else if (node.Tag is Parameter || node.Tag is Total || node.Tag is DialogControl)
          {
            while (node != null && node.Tag != null)
            {
              FSelectedItem = node.Text + (FSelectedItem == "" ? "" : ".") + FSelectedItem;
              node = node.Parent;
            }
          }
          else if (node.Tag is MethodInfo)
          {
            MethodInfo info = node.Tag as MethodInfo;
            ParameterInfo[] pars = info.GetParameters();

            FSelectedItem = info.Name + "(" + (pars.Length > 1 ? "".PadRight(pars.Length - 1, ',') : "") + ")";
          }
        }  
        return FSelectedItem;
      }
      set 
      { 
        FSelectedItem = value;
        SetSelectedItem(Nodes, "");
      }
    }

    /// <summary>
    /// SelectedItemType property
    /// </summary>
    public DataTreeSelectedItemType SelectedItemType
    {
      get
      {
        TreeNode node = SelectedNode;
        if (node != null)
        {
          if (node.Tag is Column && !(node.Tag is DataSourceBase))
            return DataTreeSelectedItemType.DataColumn;
          else if (node.Tag is Parameter)
            return DataTreeSelectedItemType.Parameter;
          else if (node.Tag is Total)
            return DataTreeSelectedItemType.Total;
          else if (node.Tag is DialogControl)
            return DataTreeSelectedItemType.DialogControl;
          else if (node.Tag is MethodInfo)
            return DataTreeSelectedItemType.Function;
        }
        return DataTreeSelectedItemType.None;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowDataSources
    {
      get { return FShowDataSources; }
      set { FShowDataSources = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowColumns
    {
      get { return FShowColumns; }
      set { FShowColumns = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowParameters
    {
      get { return FShowParameters; }
      set { FShowParameters = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowVariables
    {
      get { return FShowVariables; }
      set { FShowVariables = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowNone
    {
      get { return FShowNone; }
      set { FShowNone = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowRelations
    {
      get { return FShowRelations; }
      set { FShowRelations = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowEnabledOnly
    {
      get { return FShowEnabledOnly; }
      set { FShowEnabledOnly = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowTotals
    {
      get { return FShowTotals; }
      set { FShowTotals = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowFunctions
    {
      get { return FShowFunctions; }
      set { FShowFunctions = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowDialogs
    {
      get { return FShowDialogs; }
      set { FShowDialogs = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> ExpandedNodes
    {
      get
      {
        List<string> result = new List<string>();
        GetExpandedNodes(Nodes, result);
        return result;
      }
      set
      {
        EnumNodes(Nodes, value);
      }
    }

    private void GetExpandedNodes(TreeNodeCollection nodes, List<string> list)
    {
      foreach (TreeNode node in nodes)
      {
        if (node.IsExpanded)
          list.Add(node.FullPath);
        GetExpandedNodes(node.Nodes, list);
      }
    }
    
    private void EnumNodes(TreeNodeCollection nodes, List<string> list)
    {
      foreach (TreeNode node in nodes)
      {
        if (list.Contains(node.FullPath))
          node.Expand();
        EnumNodes(node.Nodes, list);
      }
    }

    private void SetSelectedItem(TreeNodeCollection nodes, string prefix)
    {
      foreach (TreeNode node in nodes)
      {
        string nodeName = "";
        if (node.Tag is DataSourceBase || node.Tag is Column)
          nodeName = prefix + (node.Tag as DataComponentBase).Alias;
        else if (node.Tag is Relation)
          nodeName = prefix + (node.Tag as Relation).ParentDataSource.Alias;
        else if (node.Tag is Parameter)
          nodeName = prefix + (node.Tag as Parameter).Name;
        
        if (nodeName == FSelectedItem)
        {
          SelectedNode = node;
          break;
        }
        SetSelectedItem(node.Nodes, nodeName == "" ? prefix : nodeName + ".");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionary"></param>
    public void CreateNodes(Dictionary dictionary)
    {
      Nodes.Clear();
      if (ShowNone)
      {
        TreeNode noneNode = Nodes.Add(Res.Get("Misc,None"));
        noneNode.ImageIndex = 76;
        noneNode.SelectedImageIndex = noneNode.ImageIndex;
      }
      if (ShowVariables || ShowParameters || ShowTotals)
      {
        if (ShowDataSources)
        {
          TreeNode dataNode = Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,DataSources"));
          dataNode.ImageIndex = 53;
          dataNode.SelectedImageIndex = dataNode.ImageIndex;
          DataTreeHelper.CreateDataTree(dictionary, dataNode.Nodes, ShowEnabledOnly, ShowRelations, ShowDataSources,
            ShowColumns);
        }  

        if (ShowParameters && dictionary.Parameters.Count > 0)
        {
          TreeNode parNode = Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Parameters"));
          parNode.ImageIndex = 234;
          parNode.SelectedImageIndex = parNode.ImageIndex;
          DataTreeHelper.CreateParametersTree(dictionary.Parameters, parNode.Nodes);
        }
        
        if (ShowVariables)
        {
          TreeNode sysNode = Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,SystemVariables"));
          sysNode.ImageIndex = 60;
          sysNode.SelectedImageIndex = sysNode.ImageIndex;
          DataTreeHelper.CreateVariablesTree(dictionary.SystemVariables, sysNode.Nodes);
        }

        if (ShowFunctions)
        {
          TreeNode funcNode = Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Functions"));
          funcNode.ImageIndex = 52;
          funcNode.SelectedImageIndex = funcNode.ImageIndex;
          DataTreeHelper.CreateFunctionsTree(dictionary.Report, funcNode.Nodes);
        }

        if (ShowTotals && dictionary.Totals.Count > 0)
        {
          TreeNode sumNode = Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Totals"));
          sumNode.ImageIndex = 132;
          sumNode.SelectedImageIndex = sumNode.ImageIndex;
          DataTreeHelper.CreateTotalsTree(dictionary.Totals, sumNode.Nodes);
        }

        if (ShowDialogs)
        {
          DataTreeHelper.CreateDialogControlsTree(dictionary.Report, Nodes);
        }
      }
      else
      {
        DataTreeHelper.CreateDataTree(dictionary, Nodes, ShowEnabledOnly, ShowRelations, ShowDataSources, ShowColumns);
      }
    }
    
    /// <summary>
    /// Creates a new instance of the DataTreeView control.
    /// </summary>
    public DataTreeView()
    {
      ImageList = Res.GetImages();
      FSelectedItem = "";
      ShowDataSources = true;
      ShowColumns = true;
    }
  }
}
