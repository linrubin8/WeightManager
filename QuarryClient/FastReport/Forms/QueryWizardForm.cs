using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;
using FastReport.FastQueryBuilder;
using FastReport.Design;


namespace FastReport.Forms
{
  internal partial class QueryWizardForm : BaseWizardForm
  {
    private TableDataSource FTable;

    public override int VisiblePanelIndex
    {
      get { return base.VisiblePanelIndex; }
      set
      {
        // disable page 2,3 in case of non-sql datasource
        if (FTable.Connection != null && !FTable.Connection.IsSqlBased)
        {
          if (value == 1)
            value = 3;
          if (value == 2)
            value = 0;
        }
        
        if (value == 2)
          UpdateParamTree(null);
        if (value == 3)
        {
          FTable.Alias = tbName.Text;
          FTable.SelectCommand = tbSql.Text;
          try
          {
            FTable.RefreshTable();
            UpdateColumnTree(null);
          }
          catch (Exception e)
          {
            FRMessageBox.Error(e.Message);
          }  
        }
        base.VisiblePanelIndex = value;
      }
    }

    private void tbSql_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.A && e.Control)
            tbSql.SelectAll();
    }

    private void btnQueryBuilder_Click(object sender, EventArgs e)
    {
      if (FTable.Connection != null)
        using (DataConnectionBase conn = Activator.CreateInstance(FTable.Connection.GetType()) as DataConnectionBase)
        {
          conn.Assign(FTable.Connection);
          if (Config.DesignerSettings.ApplicationConnection != null)
            conn.ConnectionString = Config.DesignerSettings.ApplicationConnection.ConnectionString;

          CustomQueryBuilderEventArgs args = new CustomQueryBuilderEventArgs(conn, tbSql.Text);
          Config.DesignerSettings.OnCustomQueryBuilder(this, args);
          tbSql.Text = args.SQL;
        }  
    }

    private void btnAddParameter_Click(object sender, EventArgs e)
    {
      CommandParameter c = new CommandParameter();
      c.Name = FTable.Parameters.CreateUniqueName("Parameter");
      c.DataType = FTable.Connection.GetDefaultParameterType();
      FTable.Parameters.Add(c);
      UpdateParamTree(c);
    }

    private void btnDeleteParameter_Click(object sender, EventArgs e)
    {
      if (tvParameters.SelectedNode == null)
        return;
      Base c = tvParameters.SelectedNode.Tag as Base;
      if (c == null)
        return;

      pgParamProperties.SelectedObject = null;
      c.Dispose();
      UpdateParamTree(null);
    }

    private void btnParameterUp_Click(object sender, EventArgs e)
    {
      if (tvParameters.SelectedNode == null)
        return;
      Base c = tvParameters.SelectedNode.Tag as Base;
      if (c == null)
        return;

      int index = FTable.Parameters.IndexOf(c);
      FTable.Parameters.RemoveAt(index);
      FTable.Parameters.Insert(index - 1, c);

      UpdateParamTree(c);
    }

    private void btnParameterDown_Click(object sender, EventArgs e)
    {
      if (tvParameters.SelectedNode == null)
        return;
      Base c = tvParameters.SelectedNode.Tag as Base;
      if (c == null)
        return;

      int index = FTable.Parameters.IndexOf(c);
      FTable.Parameters.RemoveAt(index);
      FTable.Parameters.Insert(index + 1, c);

      UpdateParamTree(c);
    }

    private void tvParameters_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
        btnDeleteParameter_Click(this, EventArgs.Empty);
    }

    private void btnRefreshColumns_Click(object sender, EventArgs e)
    {
      FTable.RefreshColumns(true);
      UpdateColumnTree(null);
    }

    private void btnAddColumn_Click(object sender, EventArgs e)
    {
      Column c = new Column();
      c.Name = FTable.Columns.CreateUniqueName("Column");
      c.Alias = FTable.Columns.CreateUniqueAlias(c.Alias);
      c.Calculated = true;
      FTable.Columns.Add(c);
      UpdateColumnTree(c);
    }

    private void btnDeleteColumn_Click(object sender, EventArgs e)
    {
      if (tvColumns.SelectedNode == null)
        return;
      Base c = tvColumns.SelectedNode.Tag as Base;
      if (c == null)
        return;

      pgColumnProperties.SelectedObject = null;
      c.Dispose();
      UpdateColumnTree(null);
    }

    private void tvColumns_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
        btnDeleteColumn_Click(this, EventArgs.Empty);
    }

    private void tvParameters_AfterSelect(object sender, TreeViewEventArgs e)
    {
      UpdateParamSelection();
    }

    private void UpdateParamSelection()
    {
      if (tvParameters.SelectedNode == null)
      {
        pgParamProperties.SelectedObject = null;
        btnDeleteParameter.Enabled = false;
        btnParameterUp.Enabled = false;
        btnParameterDown.Enabled = false;
        return;
      }
      pgParamProperties.SelectedObject = tvParameters.SelectedNode.Tag;
      btnDeleteParameter.Enabled = true;
      btnParameterUp.Enabled = tvParameters.SelectedNode.Index > 0;
      btnParameterDown.Enabled = tvParameters.SelectedNode.Index < tvParameters.Nodes.Count - 1;
    }

    private void tvColumns_AfterSelect(object sender, TreeViewEventArgs e)
    {
      pgColumnProperties.SelectedObject = tvColumns.SelectedNode.Tag;
    }

    private void pgParamProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (tvParameters.SelectedNode != null && tvParameters.SelectedNode.Tag is CommandParameter)
        tvParameters.SelectedNode.Text = (tvParameters.SelectedNode.Tag as CommandParameter).Name;
    }

    private void pgColumnProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (tvColumns.SelectedNode != null && tvColumns.SelectedNode.Tag is Column)
        tvColumns.SelectedNode.Text = (tvColumns.SelectedNode.Tag as Column).Alias;
    }

    private void UpdateParamTree(Base focusObj)
    {
      tvParameters.BeginUpdate();
      tvParameters.Nodes.Clear();

      foreach (CommandParameter c in FTable.Parameters)
      {
        TreeNode node = tvParameters.Nodes.Add(c.Name);
        node.Tag = c;
        node.ImageIndex = 231;
        node.SelectedImageIndex = node.ImageIndex;
        if (c == focusObj)
          tvParameters.SelectedNode = node;
      }

      if (focusObj == null && tvParameters.Nodes.Count > 0)
        tvParameters.SelectedNode = tvParameters.Nodes[0];
      tvParameters.EndUpdate();
      UpdateParamSelection();
    }

    private void UpdateColumnTree(Base focusObj)
    {
      tvColumns.BeginUpdate();
      tvColumns.Nodes.Clear();
      
      FTable.InitSchema();
      
      foreach (Column c in FTable.Columns)
      {
        TreeNode node = tvColumns.Nodes.Add(c.Alias);
        node.Tag = c;
        node.ImageIndex = c.GetImageIndex();
        node.SelectedImageIndex = node.ImageIndex;
        if (c == focusObj)
          tvColumns.SelectedNode = node;
      }

      if (focusObj == null && tvColumns.Nodes.Count > 0)
        tvColumns.SelectedNode = tvColumns.Nodes[0];
      tvColumns.EndUpdate();
    }

    private void Init()
    {
      VisiblePanelIndex = 0;
      picIcon.Image = ResourceLoader.GetBitmap("querywizard.png");
      tvParameters.ImageList = Res.GetImages();
      tvColumns.ImageList = Res.GetImages();
      btnAddParameter.Image = Res.GetImage(56);
      btnDeleteParameter.Image = Res.GetImage(51);
      btnParameterUp.Image = Res.GetImage(208);
      btnParameterDown.Image = Res.GetImage(209);
      btnRefreshColumns.Image = Res.GetImage(232);
      btnAddColumn.Image = Res.GetImage(55);
      btnDeleteColumn.Image = Res.GetImage(51);

      tsParameters.Renderer = Config.DesignerSettings.ToolStripRenderer;
      tsColumns.Renderer = Config.DesignerSettings.ToolStripRenderer;
        
      tbName.Text = FTable.Alias;
      tbSql.Text = FTable.SelectCommand;  
    }

    private void TableWizardForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      tbSql.Height = btnQueryBuilder.Top - tbSql.Top - 12;
    }
    
    private void TableWizardForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Config.SaveFormState(this);
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,QueryWizard");
      Text = res.Get("");
      pnName.Text = res.Get("Page1");
      pnSql.Text = res.Get("Page2");
      pnParameters.Text = res.Get("Page3");
      pnColumns.Text = res.Get("Page4");
      lblSetName.Text = res.Get("SetName");
      lblNameHint.Text = res.Get("NameHint");
      lblWhatData.Text = res.Get("WhatData");
      lblTypeSql.Text = res.Get("TypeSql");
      btnQueryBuilder.Text = res.Get("QueryBuilder");
      btnAddParameter.Text = res.Get("AddParameter");
      btnDeleteParameter.Text = res.Get("Delete");
      btnRefreshColumns.Text = res.Get("Refresh");
      btnAddColumn.Text = res.Get("AddColumn");
      btnDeleteColumn.Text = res.Get("Delete");
    }
    
    
    public QueryWizardForm(TableDataSource table)
    {
      FTable = table;
      InitializeComponent();
      Localize();
      Init();
      Config.RestoreFormState(this);
    }
  }
}

