using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Reflection;
using FastReport.Utils;
using FastReport.Dialog;
using FastReport.Data;

namespace FastReport.Controls
{
  internal static partial class DataTreeHelper
  {
	  // Simon: 注释原方法
	//public static void AddColumns(TreeNodeCollection root, ColumnCollection columns, bool enabledOnly, bool showColumns)
	//{
	//  foreach (Column column in columns)
	//  {
	//	if (!enabledOnly || column.Enabled)
	//	{
	//	  TreeNode node = new TreeNode(column.Alias);
	//	  node.Tag = column;
	//	  node.Checked = column.Enabled;

	//	  int imageIndex = column.GetImageIndex();
	//	  node.ImageIndex = imageIndex;
	//	  node.SelectedImageIndex = imageIndex;

	//	  AddColumns(node.Nodes, column.Columns, enabledOnly, showColumns);

	//	  bool isDataSource = column is DataSourceBase;
	//	  bool addNode = showColumns || isDataSource || node.Nodes.Count > 0;

	//	  if (addNode)
	//		root.Add(node);
	//	}
	//  }
	//}

    public static void AddDataSource(Dictionary dictionary, DataSourceBase data, TreeNodeCollection root,
      bool enabledOnly, bool showRelations, bool showColumns)
    {
      AddDataSource(dictionary, data, root, null, new ArrayList(), enabledOnly, showRelations, showColumns, false);
    }

    private static void AddDataSource(Dictionary dictionary, DataSourceBase data, TreeNodeCollection root,
      Relation rel, ArrayList processed, bool enabledOnly, bool showRelations, bool showColumns,
      bool useRelationName)
    {
      if (data == null)
        return;

      TreeNode dataNode = root.Add(rel != null && useRelationName ? rel.Alias : data.Alias);
      dataNode.Tag = rel != null ? (object)rel : (object)data;
      dataNode.Checked = data.Enabled;
      dataNode.ImageIndex = rel != null ? 58 : 222;
      dataNode.SelectedImageIndex = dataNode.ImageIndex;

      bool alreadyProcessed = processed.Contains(data);
      processed.Add(data);

      // process relations
      if (showRelations && !alreadyProcessed)
      {
        foreach (Relation relation in dictionary.Relations)
        {
          if ((!enabledOnly || relation.Enabled) && relation.ChildDataSource == data)
          {
            useRelationName = GetNumberOfRelations(dictionary, relation.ParentDataSource, relation.ChildDataSource) > 1;
            AddDataSource(dictionary, relation.ParentDataSource, dataNode.Nodes, relation, processed,
              enabledOnly, true, showColumns, useRelationName);
          }
        }
      }

      // add columns and/or nested datasources
      AddColumns(dataNode.Nodes, data.Columns, enabledOnly, showColumns);

      processed.Remove(data);
    }

	// Simon: 注释原方法
	//private static void AddParameter(Parameter par, TreeNodeCollection root)
	//{
	//  TreeNode parNode = root.Add(par.Name);
	//  parNode.Tag = par;
	//  parNode.ImageIndex = par.Parameters.Count == 0 ? GetTypeImageIndex(par.DataType) : 234;
	//  parNode.SelectedImageIndex = parNode.ImageIndex;

	//  foreach (Parameter p in par.Parameters)
	//  {
	//	AddParameter(p, parNode.Nodes);
	//  }
	//}

    private static void AddVariable(Parameter par, TreeNodeCollection root)
    {
      TreeNode parNode = root.Add(par.Name);
      parNode.Tag = par;
      parNode.ImageIndex = GetTypeImageIndex(par.DataType);
      parNode.SelectedImageIndex = parNode.ImageIndex;

      foreach (Parameter p in par.Parameters)
      {
        AddParameter(p, parNode.Nodes);
      }
    }

    private static void CreateFunctionsTree(Report report, ObjectInfo rootItem, TreeNodeCollection rootNode)
    {
      foreach (ObjectInfo item in rootItem.Items)
      {
        string text = "";
        int imageIndex = 66;

        MethodInfo func = item.Function;
        if (func != null)
        {
          text = func.Name;
          // if this is an overridden function, show its parameters
          if (rootItem.Name == text)
            text = report.CodeHelper.GetMethodSignature(func, false);
          imageIndex = GetTypeImageIndex(func.ReturnType);
        }
        else
        {
          // it's a category
          text = Res.TryGet(item.Text);
        }

        TreeNode node = rootNode.Add(text);
        node.Tag = func;
        node.ImageIndex = imageIndex;
        node.SelectedImageIndex = imageIndex;

        if (item.Items.Count > 0)
          CreateFunctionsTree(report, item, node.Nodes);
      }
    }

    public static void CreateDataTree(Dictionary dictionary, TreeNodeCollection root, bool enabledOnly,
      bool relations, bool dataSources, bool columns)
    {
      if (dataSources)
      {
        // create connection node and enumerate connection tables
        foreach (DataConnectionBase connection in dictionary.Connections)
        {
          TreeNode connNode = root.Add(connection.Name);
          connNode.Tag = connection;
          connNode.Checked = true;
          connNode.ImageIndex = 53;
          connNode.SelectedImageIndex = connNode.ImageIndex;

          foreach (TableDataSource data in connection.Tables)
          {
            if (!enabledOnly || data.Enabled)
              AddDataSource(dictionary, data, connNode.Nodes, enabledOnly, relations, columns);
          }
        }

        // process regular tables
        foreach (DataSourceBase data in dictionary.DataSources)
        {
          if (!enabledOnly || data.Enabled)
            AddDataSource(dictionary, data, root, enabledOnly, relations, columns);
        }
      }
      else if (relations)
      {
        // display relations only (used by the Relation type editor)
        foreach (Relation relation in dictionary.Relations)
        {
          if (relation.ParentDataSource == null || relation.ChildDataSource == null)
            continue;

          TreeNode relNode = root.Add(relation.Alias + " (" +
            relation.ParentDataSource.Alias + "->" + relation.ChildDataSource.Alias + ")");
          relNode.Tag = relation;
          relNode.Checked = true;
          relNode.ImageIndex = 58;
          relNode.SelectedImageIndex = relNode.ImageIndex;
        }
      }
    }

    public static void CreateDataTree(Dictionary dictionary, DataConnectionBase connection,
      TreeNodeCollection root)
    {
      SortedList<string, TableDataSource> tables = new SortedList<string, TableDataSource>();
      // sort the tables first
      foreach (TableDataSource table in connection.Tables)
      {
        string tableName = table.Alias;
        // fix the node text if table is not a query: display the TableName instead of Alias
        if (String.IsNullOrEmpty(table.SelectCommand))
          tableName = table.TableName.Replace("\"", "");
        tables.Add(tableName, table);
      }

      foreach (KeyValuePair<string, TableDataSource> keyValue in tables)
      {
        TableDataSource data = keyValue.Value;
        AddDataSource(dictionary, data, root, false, false, true);
        TreeNode node = root[root.Count - 1];
        node.Text = keyValue.Key;
        // add dummy child node for the table which has no schema, to allow expand the node and get schema
        if (data.Columns.Count == 0)
          node.Nodes.Add("dummy");
      }
    }

	// Simon: 注释原方法
	//public static void CreateParametersTree(ParameterCollection parameters, TreeNodeCollection root)
	//{
	//  foreach (Parameter p in parameters)
	//  {
	//	AddParameter(p, root);
	//  }
	//}

    public static void CreateVariablesTree(ParameterCollection parameters, TreeNodeCollection root)
    {
      foreach (Parameter p in parameters)
      {
        AddVariable(p, root);
      }
    }

    public static void CreateTotalsTree(TotalCollection totals, TreeNodeCollection root)
    {
      foreach (Total s in totals)
      {
        TreeNode sumNode = root.Add(s.Name);
        sumNode.Tag = s;
        sumNode.ImageIndex = 132;
        sumNode.SelectedImageIndex = sumNode.ImageIndex;
      }
    }

    public static void CreateFunctionsTree(Report report, TreeNodeCollection root)
    {
      CreateFunctionsTree(report, RegisteredObjects.FindFunctionsRoot(), root);
    }

    public static void CreateDialogControlsTree(Report report, TreeNodeCollection root)
    {
      TreeNode dialogNode = null;

      foreach (Base c in report.AllObjects)
      {
        if (c is DialogControl)
        {
          PropertyInfo bindableProp = (c as DialogControl).BindableProperty;
          if (bindableProp != null)
          {
            if (dialogNode == null)
            {
              dialogNode = root.Add(Res.Get("Designer,ToolWindow,Dictionary,DialogControls"));
              dialogNode.ImageIndex = 136;
              dialogNode.SelectedImageIndex = dialogNode.ImageIndex;
            }

            TreeNode node = dialogNode.Nodes.Add(c.Name + "." + bindableProp.Name);
            node.ImageIndex = GetTypeImageIndex(bindableProp.PropertyType);
            node.SelectedImageIndex = node.ImageIndex;
            node.Tag = c;
          }
        }
      }
    }

    private static int GetNumberOfRelations(Dictionary dictionary, DataSourceBase parent, DataSourceBase child)
    {
      int result = 0;
      foreach (Relation rel in dictionary.Relations)
      {
        if (rel.ParentDataSource == parent && rel.ChildDataSource == child)
          result++;
      }

      return result;
    }

    public static int GetTypeImageIndex(Type dataType)
    {
      int index = 224;
      if (dataType == typeof(string) || dataType == typeof(char))
        index = 223;
      else if (dataType == typeof(float) || dataType == typeof(double))
        index = 225;
      else if (dataType == typeof(decimal))
        index = 226;
      else if (dataType == typeof(DateTime) || dataType == typeof(TimeSpan))
        index = 227;
      else if (dataType == typeof(bool))
        index = 228;
      else if (dataType == typeof(byte[]) || dataType == typeof(Image) || dataType == typeof(Bitmap))
        index = 229;
      return index;
    }
  }
}
