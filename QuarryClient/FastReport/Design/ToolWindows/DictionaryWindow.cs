using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;
using FastReport.DevComponents.DotNetBar.Controls;
using System.Reflection;
using FastReport.Controls;

namespace FastReport.Design.ToolWindows
{
  /// <summary>
  /// Represents the "Data Dictionary" window.
  /// </summary>
  public class DictionaryWindow : ToolWindowBase
  {
    #region Fields
    private Bar FToolbar;
    private ButtonItem btnActions;
    private ButtonItem btnEdit;
    private ButtonItem btnDelete;
    private ButtonItem btnView;
    private ButtonItem miNew;
    private ButtonItem miOpen;
    private ButtonItem miMerge;
    private ButtonItem miSave;
    private ButtonItem miChooseData;
    private ButtonItem miNewDataSource;
    private ButtonItem miNewRelation;
    private ButtonItem miNewParameter;
    private ButtonItem miNewTotal;
    private ButtonItem miNewCalculatedColumn;

    private ContextMenuBar mnuContext;
    private ButtonItem mnuContextRoot;
    private ButtonItem miRename;
    private ButtonItem miEdit;
    private ButtonItem miDelete;
    private ButtonItem miDeleteAlias;
    private ButtonItem miView;
    private TreeViewMultiSelect FTree;
    private Splitter FSplitter;
    private DescriptionControl lblDescription;
    private Report FReport;
    private List<string> FExpandedNodes;
    #endregion
    
    #region Properties
    private bool IsDataComponent
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag is DataComponentBase; }
    }

    private bool IsVariable
    {
      get 
      { 
        return FTree.SelectedNode != null && FTree.SelectedNode.Tag is Parameter && 
          !(FTree.SelectedNode.Parent.Tag is SystemVariables); 
      }
    }

    private bool IsTotal
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag is Total; }
    }

    private bool IsConnection
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag is DataConnectionBase; }
    }

    private bool IsTable
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag is DataSourceBase; }
    }

    private bool IsRelation
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag is Relation; }
    }

    private bool IsEditableColumn
    {
      get 
      {
        TreeNode node = FTree.SelectedNode;
        bool result = node != null && node.Tag is Column;
        if (result)
        {
          // check if column belongs to the datasource, not relation.
          while (node != null)
          {
            if (node.Tag is Relation)
            {
              result = false;
              break;
            }
            else if (node.Tag is DataSourceBase)
              break;
              
            node = node.Parent;  
          }
        }
        
        return result;
      }
    }

    private bool IsDataSources
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag == FReport.Dictionary.DataSources; }
    }

    private bool IsVariables
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag == FReport.Dictionary.Parameters; }
    }

    private bool IsSystemVariables
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag == FReport.Dictionary.SystemVariables; }
    }

    private bool IsTotals
    {
      get { return FTree.SelectedNode != null && FTree.SelectedNode.Tag == FReport.Dictionary.Totals; }
    }

    private bool CanEdit
    {
      get
      {
        return (IsDataComponent || IsVariable || IsTotal) && 
          !Designer.Restrictions.DontEditData &&
          (FTree.SelectedNode.Tag as Base).HasFlag(Flags.CanEdit) &&
          !(FTree.SelectedNode.Tag as Base).HasRestriction(Restrictions.DontEdit);
      }
    }

    private bool CanDelete
    {
      get
      {
        return (IsDataComponent || IsVariable || IsTotal) &&
          !Designer.Restrictions.DontEditData &&
          (FTree.SelectedNode.Tag as Base).HasFlag(Flags.CanDelete) &&
          !(FTree.SelectedNode.Tag as Base).HasRestriction(Restrictions.DontDelete);
      }
    }

    private bool CanCreateCalculatedColumn
    {
      get
      {
        return FTree.SelectedNode != null && FTree.SelectedNode.Tag is DataSourceBase && 
          !(FTree.SelectedNode.Tag is BusinessObjectDataSource);
      }
    }

    private bool IsAliased
    {
      get
      {
        return FTree.SelectedNode != null && FTree.SelectedNode.Tag is DataComponentBase &&
          (FTree.SelectedNode.Tag as DataComponentBase).IsAliased;
      }
    }
    #endregion

    #region Private Methods
    private TreeNode FindNode(TreeNodeCollection parent, string text)
    {
      foreach (TreeNode node in parent)
      {
        if (node.Text == text)
          return node;
      }
      return null;
    }

    private void NavigateTo(string path)
    {
      string[] parts = path.Split(new char[] { '.' });
      TreeNodeCollection parent = FTree.Nodes;
      TreeNode node = null;
      foreach (string part in parts)
      {
        node = FindNode(parent, part);
        parent = node.Nodes;
      }
      FTree.SelectedNode = node;
    }

    private void GetExpandedNodes(TreeNodeCollection nodes)
    {
      foreach (TreeNode node in nodes)
      {
        if (node.IsExpanded)
          FExpandedNodes.Add(node.FullPath);
        GetExpandedNodes(node.Nodes);
      }
    }

    private bool CompareNodes(TreeNodeCollection fromNodes, TreeNodeCollection toNodes)
    {
      if (fromNodes.Count != toNodes.Count)
        return false;
      for (int i = 0; i < fromNodes.Count; i++)
      {
        if (fromNodes[i].Text != toNodes[i].Text || fromNodes[i].ImageIndex != toNodes[i].ImageIndex)
          return false;
        toNodes[i].Tag = fromNodes[i].Tag;  
        if (!CompareNodes(fromNodes[i].Nodes, toNodes[i].Nodes))
          return false;
      }
      return true;
    }

    private void CopyNodes(TreeNodeCollection fromNodes, TreeNodeCollection toNodes)
    {
      foreach (TreeNode fromNode in fromNodes)
      {
        TreeNode toNode = toNodes.Add(fromNode.Text);
        toNode.Tag = fromNode.Tag;
        toNode.ImageIndex = fromNode.ImageIndex;
        toNode.SelectedImageIndex = fromNode.SelectedImageIndex;
        CopyNodes(fromNode.Nodes, toNode.Nodes);
        if (FExpandedNodes.Contains(fromNode.FullPath))
          toNode.Expand();
      }
    }

    private void UpdateTree()
    {
      FExpandedNodes.Clear();
      GetExpandedNodes(FTree.Nodes);

      TreeView buffer = new TreeView();
      if (FReport != null)
      {
        bool canShowData = FReport.Dictionary.Connections.Count > 0;
        foreach (DataSourceBase data in FReport.Dictionary.DataSources)
        {
          if (data.Enabled)
          {
            canShowData = true;
            break;
          }
        }
        
        TreeNode rootNode = null;
        if (canShowData)
        {
          rootNode = buffer.Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,DataSources"));
          rootNode.Tag = FReport.Dictionary.DataSources;
          rootNode.ImageIndex = 53;
          rootNode.SelectedImageIndex = rootNode.ImageIndex;
          DataTreeHelper.CreateDataTree(FReport.Dictionary, rootNode.Nodes, true, true, true, true);
        }

        // system variables
        rootNode = buffer.Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,SystemVariables"));
        rootNode.Tag = FReport.Dictionary.SystemVariables;
        rootNode.ImageIndex = 60;
        rootNode.SelectedImageIndex = rootNode.ImageIndex;
        DataTreeHelper.CreateVariablesTree(FReport.Dictionary.SystemVariables, rootNode.Nodes);
        
        // totals
        rootNode = buffer.Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Totals"));
        rootNode.Tag = FReport.Dictionary.Totals;
        rootNode.ImageIndex = 132;
        rootNode.SelectedImageIndex = rootNode.ImageIndex;
        DataTreeHelper.CreateTotalsTree(FReport.Dictionary.Totals, rootNode.Nodes);

        // parameters
        rootNode = buffer.Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Parameters"));
        rootNode.Tag = FReport.Dictionary.Parameters;
        rootNode.ImageIndex = 234;
        rootNode.SelectedImageIndex = rootNode.ImageIndex;
        DataTreeHelper.CreateParametersTree(FReport.Dictionary.Parameters, rootNode.Nodes);

        // functions
        rootNode = buffer.Nodes.Add(Res.Get("Designer,ToolWindow,Dictionary,Functions"));
        rootNode.ImageIndex = 52;
        rootNode.SelectedImageIndex = rootNode.ImageIndex;
        DataTreeHelper.CreateFunctionsTree(FReport, rootNode.Nodes);
      }

      if (!CompareNodes(buffer.Nodes, FTree.Nodes))
      {
        FTree.BeginUpdate();
        FTree.Nodes.Clear();
        CopyNodes(buffer.Nodes, FTree.Nodes);
        FTree.EndUpdate();
      }

      buffer.Dispose();
      UpdateControls();
    }

    private void Change()
    {
      Designer.SetModified(this, "EditData");
    }
    
    private void UpdateControls()
    {
      btnEdit.Enabled = CanEdit;
      btnDelete.Enabled = CanDelete;
      btnView.Enabled = IsTable;
    }

    private void miNew_Click(object sender, EventArgs e)
    {
      FReport.Dictionary.Clear();
      FReport.Dictionary.ReRegisterData();
      UpdateTree();
      Change();
    }

    private void miOpen_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Dictionary");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          FReport.Dictionary.Load(dialog.FileName);
          UpdateTree();
          Change();
        }
      }
    }

    private void miSave_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dialog = new SaveFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Dictionary");
        dialog.DefaultExt = "frd";
        dialog.FileName = "Dictionary.frd";
        if (dialog.ShowDialog() == DialogResult.OK)
          FReport.Dictionary.Save(dialog.FileName);
      }
    }

    private void miMerge_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Dictionary");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Dictionary dict = new Dictionary();
          dict.Load(dialog.FileName);
          FReport.Dictionary.Merge(dict);
          UpdateTree();
          Change();
        }
      }
    }

    private void btnActions_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      while (btnActions.SubItems[btnActions.SubItems.Count - 1] != miChooseData)
      {
        btnActions.SubItems.RemoveAt(btnActions.SubItems.Count - 1);
      }
      btnActions.SubItems.AddRange(new ButtonItem[] { 
        miNewDataSource, miNewRelation, miNewCalculatedColumn, miNewParameter, miNewTotal });

      miNew.Enabled = Designer.cmdChooseData.Enabled;
      miOpen.Enabled = Designer.cmdChooseData.Enabled;
      miMerge.Enabled = Designer.cmdChooseData.Enabled;
      miSave.Enabled = Designer.cmdChooseData.Enabled;
      miChooseData.Enabled = Designer.cmdChooseData.Enabled;
      miNewDataSource.Enabled = Designer.cmdAddData.Enabled;
      miNewRelation.Enabled = Designer.cmdChooseData.Enabled;
      miNewParameter.Enabled = Designer.cmdChooseData.Enabled;
      miNewTotal.Enabled = Designer.cmdChooseData.Enabled;
      miNewCalculatedColumn.Enabled = Designer.cmdChooseData.Enabled && CanCreateCalculatedColumn;
    }

    private void mnuContextRoot_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      mnuContextRoot.SubItems.Clear();
      if (!Designer.cmdChooseData.Enabled)
      {
        e.Cancel = true;
        return;
      }

      if ((IsDataSources || IsConnection) && Designer.cmdAddData.Enabled)
      {
        mnuContextRoot.SubItems.Add(miNewDataSource);
      }
      else if (IsVariables || IsVariable)
        mnuContextRoot.SubItems.Add(miNewParameter);
      else if (IsTotals || IsTotal)
        mnuContextRoot.SubItems.Add(miNewTotal);
      else if (CanCreateCalculatedColumn)
      {
        mnuContextRoot.SubItems.Add(miNewCalculatedColumn);
        miNewCalculatedColumn.Enabled = true;
      }

      if (CanEdit)
        mnuContextRoot.SubItems.Add(miEdit);
      if (IsTable || IsEditableColumn || IsVariable || IsTotal)
        mnuContextRoot.SubItems.Add(miRename);
      if (CanDelete)
        mnuContextRoot.SubItems.Add(miDelete);
      if (IsAliased)
        mnuContextRoot.SubItems.Add(miDeleteAlias);
      if (IsTable)
        mnuContextRoot.SubItems.Add(miView);

      if (mnuContextRoot.SubItems.Count == 0)
        e.Cancel = true;
    }

    private void miNewRelation_Click(object sender, EventArgs e)
    {
      Relation relation = new Relation();
      FReport.Dictionary.Relations.Add(relation);
      using (RelationEditorForm form = new RelationEditorForm(relation))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          relation.Name = FReport.Dictionary.CreateUniqueName(relation.ParentDataSource.Name + "_" + 
            relation.ChildDataSource.Name);
          UpdateTree();
          Change();
        }
        else
          relation.Dispose();
      }
    }

    private void miNewCalculatedColumn_Click(object sender, EventArgs e)
    {
      DataSourceBase data = FTree.SelectedNode.Tag as DataSourceBase;
      Column c = new Column();
      c.Name = data.Columns.CreateUniqueName("Column");
      c.Alias = data.Columns.CreateUniqueAlias(c.Alias);
      c.Calculated = true;
      data.Columns.Add(c);

      UpdateTree();
      string navigatePath = Res.Get("Designer,ToolWindow,Dictionary,DataSources");
      if (data.Parent is DataConnectionBase)
        navigatePath += "." + data.Parent.Name;
      navigatePath += "." + data.Alias + "." + c.Alias;
      NavigateTo(navigatePath);
      Change();
    }

    private void miNewParameter_Click(object sender, EventArgs e)
    {
      Parameter p = new Parameter();
      ParameterCollection parent = null;
      if (IsVariable)
        parent = (FTree.SelectedNode.Tag as Parameter).Parameters;
      else
        parent = FReport.Dictionary.Parameters;

      p.Name = parent.CreateUniqueName("Parameter");
      parent.Add(p);
      UpdateTree();
      NavigateTo(Res.Get("Designer,ToolWindow,Dictionary,Parameters") + "." + p.FullName);
      Change();
    }

    private void miNewTotal_Click(object sender, EventArgs e)
    {
      using (TotalEditorForm form = new TotalEditorForm(Designer))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          FReport.Dictionary.Totals.Add(form.Total);
          UpdateTree();
          NavigateTo(Res.Get("Designer,ToolWindow,Dictionary,Totals") + "." + form.Total.Name);
          Change();
        }
      }
    }

    private void miRename_Click(object sender, EventArgs e)
    {
      if (FTree.SelectedNode == null)
        return;
      FTree.SelectedNode.BeginEdit();
    }

    private void miEdit_Click(object sender, EventArgs e)
    {
      if (!CanEdit)
        return;
      
      IHasEditor c = FTree.SelectedNode.Tag as IHasEditor;
      if (c != null && c.InvokeEditor())
      {
        UpdateTree();
        Change();
      }
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      if (!CanDelete)
        return;

      (FTree.SelectedNode.Tag as Base).Delete();
      TreeNode parentNode = FTree.SelectedNode.Parent;
      int index = parentNode.Nodes.IndexOf(FTree.SelectedNode);
      FTree.SelectedNode.Remove();
      if (index < parentNode.Nodes.Count)
        FTree.SelectedNode = parentNode.Nodes[index];
      else if (index > 0)
        FTree.SelectedNode = parentNode.Nodes[index - 1];
      Change();
    }

    private void miDeleteAlias_Click(object sender, EventArgs e)
    {
      if (!IsAliased)
        return;
      DataComponentBase c = FTree.SelectedNode.Tag as DataComponentBase;
      c.Alias = c.Name;
      FTree.SelectedNode.Text = c.Name;
      Change();
    }

    private void miView_Click(object sender, EventArgs e)
    {
      DataSourceBase data = FTree.SelectedNode.Tag as DataSourceBase;
      if (data == null)
        return;
        
      try
      {
        data.Init();
      }
      catch (Exception ex)
      {
        FRMessageBox.Error(ex.Message);
        return;
      }
      object dataSource = null;
      if (data is TableDataSource)
        dataSource = (data as TableDataSource).Table;
      else
        dataSource = data.Rows;

      if (dataSource == null)
        return;

      using (Form form = new Form())
      {
        DataGridViewX grid = new DataGridViewX();
        grid.Dock = DockStyle.Fill;
        grid.DataSource = dataSource;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.BorderStyle = BorderStyle.None;
        grid.BackgroundColor = Color.White;
        grid.GridColor = Color.LightGray;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        grid.RowHeadersVisible = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        grid.DataError += new DataGridViewDataErrorEventHandler(grid_DataError);
        form.Controls.Add(grid);
        
        StatusStrip status = new StatusStrip();
        form.Controls.Add(status);
        ToolStripLabel label = new ToolStripLabel();
        label.Text = String.Format(Res.Get("Designer,ToolWindow,Dictionary,NRows"), data.RowCount);
        status.Items.Add(label);
        
        form.Name = "PreviewDataForm";
        form.Text = data.Alias;
        form.Icon = Res.GetIcon(222); 
        form.Font = DrawUtils.DefaultFont;
        form.ShowInTaskbar = false;
        form.Location = new Point(200, 200);
        form.Size = new Size(600, 400);
        form.StartPosition = FormStartPosition.Manual;
        Config.RestoreFormState(form);
        form.Shown += form_Shown;
        form.ShowDialog();
        Config.SaveFormState(form);
      }
    }

    private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      e.Cancel = true;
    }

    private void form_Shown(object sender, EventArgs e)
    {
      DataGridView grid = (sender as Form).Controls[0] as DataGridView;
      DataSourceBase data = FTree.SelectedNode.Tag as DataSourceBase;
      int i = 0;
      while (i < grid.Columns.Count)
      {
        Column c = data.Columns.FindByName(grid.Columns[i].HeaderText);
        if (c != null)
        {
          if (c.Enabled)
            grid.Columns[i].HeaderText = c.Alias;
          else
          {
            grid.Columns.RemoveAt(i);
            i--;
          }
        }
        i++;
      }
    }

    private void FTree_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
        miRename_Click(this, null);
      else if (e.KeyCode == Keys.Delete)
        miDelete_Click(this, null);
      else if (e.Control)
      {
        TreeNode node = FTree.SelectedNode;
        if (node != null && (node.Tag is Parameter) && !(node.Tag is SystemVariable))
        {
          Parameter par = node.Tag as Parameter;
          TreeNode parentNode = node.Parent;
          ParameterCollection parCollection = null;
          if (parentNode.Tag is ParameterCollection)
            parCollection = parentNode.Tag as ParameterCollection;
          else
            parCollection = (parentNode.Tag as Parameter).Parameters;

          if (e.KeyCode == Keys.Up)
          {
            parCollection.MoveUp(par);
            e.Handled = true;
          }
          else if (e.KeyCode == Keys.Down)
          {
            parCollection.MoveDown(par);
            e.Handled = true;
          }

          if (e.Handled)
          {
            // update all designer plugins (this one too)
            Designer.SetModified(null, "EditData");
            NavigateTo(Res.Get("Designer,ToolWindow,Dictionary,Parameters") + "." + par.FullName);
          }
        }
      }
    }

    private void FTree_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TreeNode node = FTree.GetNodeAt(e.Location);
        if (FTree.SelectedNode != node)
          FTree.SelectedNode = node;
      }
    }

    private void FTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      bool canEdit = (IsTable || IsEditableColumn || IsVariable || IsTotal) &&
        !Designer.Restrictions.DontEditData &&
        !(FTree.SelectedNode.Tag as Base).HasRestriction(Restrictions.DontModify);
      
      if (!canEdit)
        e.CancelEdit = true;
    }

    private void FTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      string newLabel = e.Label == null ? FTree.SelectedNode.Text : e.Label;
      if (newLabel == FTree.SelectedNode.Text)
        return;

      Base obj = FTree.SelectedNode.Tag as Base;
      bool duplicateName = false;
      
      if (obj is DataSourceBase)
      {
        if (FReport.Dictionary.FindByAlias(newLabel) != null)
          duplicateName = true;
        else
          (obj as DataSourceBase).Alias = newLabel;
      }
      else if (obj is Column)
      {
        // get column name, take parent columns into account
        string columnName = newLabel;
        TreeNode node = FTree.SelectedNode;
        while (true)
        {
          node = node.Parent;
          if (node.Tag is DataSourceBase)
            break;
          columnName = node.Text + "." + columnName;
        }
        
        DataSourceBase data = obj.Parent as DataSourceBase;
        if (data.Columns.FindByAlias(columnName) != null)
          duplicateName = true;
        else
          (obj as Column).Alias = columnName;
      }
      else if (obj is Parameter)
      {
        TreeNode parentNode = FTree.SelectedNode.Parent;
        ParameterCollection parent = null;
        if (parentNode.Tag is Parameter)
          parent = (parentNode.Tag as Parameter).Parameters;
        else
          parent = FReport.Dictionary.Parameters;
        
        if (parent.FindByName(newLabel) != null)
          duplicateName = true;
        else
          obj.Name = newLabel;
      }
      else if (obj is Total)
      {
        if (FReport.Dictionary.FindByName(newLabel) != null)
          duplicateName = true;
        else
          obj.Name = newLabel;
      }

      if (duplicateName)
      {
        e.CancelEdit = true;
        FRMessageBox.Error(Res.Get("Designer,ToolWindow,Dictionary,DuplicateName"));
      }
      else
        Change();
    }

    private void FTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (FTree.SelectedNode == null)
        return;
      object selected = FTree.SelectedNode.Tag;
      
      Designer.SelectedObjects.Clear();
      if (selected is Base)
        Designer.SelectedObjects.Add(selected as Base);
      Designer.SelectionChanged(this);
      UpdateControls();

      bool descrVisible = selected is MethodInfo || selected is SystemVariable;
      FSplitter.Visible = descrVisible;
      lblDescription.Visible = descrVisible;
      
      if (descrVisible)
        lblDescription.ShowDescription(FReport, selected);
    }

    private void FTree_ItemDrag(object sender, ItemDragEventArgs e)
    {
        DraggedItemCollection draggedItems = new DraggedItemCollection();
        List<TreeNode> selectedNodes;

        if (FTree.SelectedNodes.Contains(e.Item as TreeNode))
        {
            selectedNodes = FTree.SelectedNodes;
        }
        else
        {
            FTree.SelectedNode = e.Item as TreeNode;
            //selectedNodes = new List<TreeNode>() { FTree.SelectedNode }; //.net 2 compatibility code
            selectedNodes = new List<TreeNode>();
            selectedNodes.Add(FTree.SelectedNode);
        }

        foreach (TreeNode n in selectedNodes)
        {
            string selectedItem = "";
            TreeNode node = n;

            if (node == null)
                continue;

            if (node.Tag is Column && !(node.Tag is DataSourceBase))
            {
                while (true)
                {
                    if (node.Tag is DataSourceBase)
                    {
                        selectedItem = (node.Tag as DataSourceBase).FullName + "." + selectedItem;
                        break;
                    }
                    selectedItem = node.Text + (selectedItem == "" ? "" : ".") + selectedItem;
                    node = node.Parent;
                }
            }
            else if (node.Tag is Parameter || node.Tag is Total)
            {
                while (node != null && node.Tag != null)
                {
                    if (node.Tag is Parameter || node.Tag is Total)
                        selectedItem = node.Text + (selectedItem == "" ? "" : ".") + selectedItem;
                    node = node.Parent;
                }
            }
            else if (node.Tag is MethodInfo)
            {
                MethodInfo info = node.Tag as MethodInfo;
                ParameterInfo[] pars = info.GetParameters();
                int parsLength = pars.Length;
                if (parsLength > 0 && pars[0].Name == "thisReport")
                    parsLength--;

                selectedItem = info.Name + "(" + (parsLength > 1 ? "".PadRight(parsLength - 1, ',') : "") + ")";
            }

            if (selectedItem != "")
                draggedItems.Add(new DraggedItem(n.Tag, selectedItem));
        }

        if (draggedItems.Count > 0)
            FTree.DoDragDrop(draggedItems, DragDropEffects.Move);
        else
            FTree.DoDragDrop(e.Item, DragDropEffects.None);
    }

    private void FTree_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
        DraggedItemCollection draggedItems = DragUtils.GetAll(e);
        TreeNode targetNode = FTree.GetNodeAt(FTree.PointToClient(new Point(e.X, e.Y)));
        
        if (draggedItems == null ||
            targetNode == null ||
            targetNode.Tag is SystemVariable ||
            targetNode.Tag is SystemVariables)
            return;

        int allow = 0;
        
        foreach (DraggedItem draggedItem in draggedItems)
        {
            if (draggedItem.Obj is Parameter &&
                !(draggedItem.Obj is SystemVariable) &&
                !(draggedItem.Obj is SystemVariables))
            {
                if (targetNode.Tag is ParameterCollection ||
                    (targetNode.Tag is Parameter &&
                    targetNode.Tag != draggedItem.Obj &&
                    !(targetNode.Tag as Parameter).HasParent(draggedItem.Obj as Parameter)))
                {
                    allow++;
                }
            }
        }

        if (allow != 0 && allow == draggedItems.Count)
            e.Effect = e.AllowedEffect;
    }

    private void FTree_DragDrop(object sender, DragEventArgs e)
    {
        TreeNode targetNode = FTree.GetNodeAt(FTree.PointToClient(new Point(e.X, e.Y)));
        if (targetNode == null)
            return;

        Object targetComponent = targetNode.Tag;
        if ((targetComponent is SystemVariable) || (targetComponent is SystemVariables))
            return;

        DraggedItemCollection draggedItems = DragUtils.GetAll(e);
        if (draggedItems == null)
            return;
        
        string draggedName = "";

        foreach (DraggedItem draggedItem in draggedItems)
        {
            if ((draggedItem.Obj is SystemVariable) || (draggedItem.Obj is SystemVariables))
                continue;

            Parameter draggedComponent = draggedItem.Obj as Parameter;
            
            if (targetComponent is ParameterCollection)
            {
                ParameterCollection collection = targetComponent as ParameterCollection;
                if (collection.IndexOf(draggedComponent) != -1)
                {
                    collection.Remove(draggedComponent);
                    collection.Insert(0, draggedComponent);
                }
                else
                {
                    collection.Add(draggedComponent);
                }
            }
            else if (targetComponent is Parameter)
            {
                if ((targetComponent as Parameter).Parameters.IndexOf(draggedComponent) != -1)
                {
                    draggedComponent.ZOrder = 0;
                }
                else
                {
                    draggedComponent.Parent = targetComponent as Parameter;
                }
            }

            draggedName = draggedComponent.FullName;
        }

        FTree.SelectedNode = targetNode;
        // update all designer plugins (this one too)
        Designer.SetModified(null, "EditData");
        NavigateTo(Res.Get("Designer,ToolWindow,Dictionary,Parameters") + "." + draggedName);
    }
    
    private ButtonItem AddButton(Image image, EventHandler click)
    {
      ButtonItem button = new ButtonItem();
      button.Image = image;
      if (click != null)
        button.Click += click;
      return button;  
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      if (Designer.SelectedObjects.Count == 0 || Designer.SelectedObjects[0] is ComponentBase)
        FTree.SelectedNode = null;
    }

    /// <inheritdoc/>
    public override void UpdateContent()
    {
      FReport = Designer.ActiveReport;
      UpdateTree();
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      MyRes res = new MyRes("Designer,ToolWindow,Dictionary");

      Text = res.Get("");
      btnActions.Text = Res.Get("Buttons,Actions");
      btnEdit.Tooltip = res.Get("Edit");
      btnDelete.Tooltip = res.Get("Delete");
      btnView.Tooltip = res.Get("View");
      miNew.Text = res.Get("New");
      miOpen.Text = res.Get("Open");
      miMerge.Text = res.Get("Merge");
      miSave.Text = res.Get("Save");
      miChooseData.Text = Res.Get("Designer,Menu,Data,Choose");
      miNewDataSource.Text = res.Get("NewDataSource");
      miNewRelation.Text = res.Get("NewRelation");
      miNewParameter.Text = res.Get("NewParameter");
      miNewTotal.Text = res.Get("NewTotal");
      miNewCalculatedColumn.Text = res.Get("NewCalculatedColumn");
      miRename.Text = res.Get("Rename");
      miEdit.Text = res.Get("Edit");
      miDelete.Text = res.Get("Delete");
      miDeleteAlias.Text = res.Get("DeleteAlias");
      miView.Text = res.Get("View");
      UpdateTree();
    }

    /// <inheritdoc/>
    public override void UpdateUIStyle()
    {
      base.UpdateUIStyle();
      FToolbar.Style = UIStyleUtils.GetDotNetBarStyle(Designer.UIStyle);
      mnuContext.Style = FToolbar.Style;
      FSplitter.BackColor = UIStyleUtils.GetControlColor(Designer.UIStyle);
    }

    /// <inheritdoc/>
    public override void SaveState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      xi.SetProp("DescriptionHeight", lblDescription.Height.ToString());
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      string s = xi.GetProp("DescriptionHeight");
      if (s != "")
        lblDescription.Height = int.Parse(s);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="DictionaryWindow"/> class with default settings.
    /// </summary>
    /// <param name="designer">The report designer.</param>
    public DictionaryWindow(Designer designer) : base(designer)
    {
      Name = "DictionaryWindow";
      Image = Res.GetImage(72);

      FToolbar = new Bar();
      FToolbar.Dock = DockStyle.Top;
      FToolbar.Font = DrawUtils.DefaultFont;
      FToolbar.RoundCorners = false;

      btnActions = new ButtonItem();
      btnActions.AutoExpandOnClick = true;
      btnEdit = AddButton(Res.GetImage(68), miEdit_Click);
      btnDelete = AddButton(Res.GetImage(51), miDelete_Click);
      btnView = AddButton(Res.GetImage(54), miView_Click);
      FToolbar.Items.AddRange(new ButtonItem[] { btnActions, btnEdit, btnDelete, btnView });

      miNew = AddButton(Res.GetImage(0), miNew_Click);
      miOpen = AddButton(Res.GetImage(1), miOpen_Click);
      miMerge = AddButton(null, miMerge_Click);
      miSave = AddButton(Res.GetImage(2), miSave_Click);
      miChooseData = AddButton(null, Designer.cmdChooseData.Invoke);
      miChooseData.BeginGroup = true;

      miNewDataSource = AddButton(Res.GetImage(137), Designer.cmdAddData.Invoke);
      miNewDataSource.BeginGroup = true;
      miNewRelation = AddButton(Res.GetImage(139), miNewRelation_Click);
      miNewCalculatedColumn = AddButton(Res.GetImage(55), miNewCalculatedColumn_Click);
      miNewParameter = AddButton(Res.GetImage(56), miNewParameter_Click);
      miNewTotal = AddButton(Res.GetImage(65), miNewTotal_Click);
      btnActions.SubItems.AddRange(new ButtonItem[] { 
        miNew, miOpen, miMerge, miSave, miChooseData, 
        miNewDataSource, miNewRelation, miNewCalculatedColumn, miNewParameter, miNewTotal });
      btnActions.PopupOpen += btnActions_PopupOpen;


      mnuContext = new ContextMenuBar();
      mnuContext.Font = DrawUtils.DefaultFont;
      mnuContextRoot = new ButtonItem();
      mnuContext.Items.Add(mnuContextRoot);
      miRename = AddButton(null, miRename_Click);
      miRename.Shortcuts.Add(eShortcut.F2);
      miEdit = AddButton(Res.GetImage(68), miEdit_Click);
      miDelete = AddButton(Res.GetImage(51), miDelete_Click);
      miDeleteAlias = AddButton(null, miDeleteAlias_Click);
      miView = AddButton(Res.GetImage(54), miView_Click);
      mnuContextRoot.SubItems.AddRange(new ButtonItem[] {
        miRename, miEdit, miDelete, miDeleteAlias, miView });
      mnuContextRoot.PopupOpen += mnuContextRoot_PopupOpen;
      
      FTree = new TreeViewMultiSelect();
      FTree.Dock = DockStyle.Fill;
      FTree.BorderStyle = BorderStyle.None;
      FTree.ImageList = Res.GetImages();
      FTree.LabelEdit = true;
      FTree.HideSelection = false;
      FTree.AllowDrop = true;
      FTree.MouseDown += FTree_MouseDown;
      FTree.BeforeLabelEdit += FTree_BeforeLabelEdit;
      FTree.AfterLabelEdit += FTree_AfterLabelEdit;
      FTree.KeyDown += FTree_KeyDown;
      FTree.AfterSelect += FTree_AfterSelect;
      FTree.DoubleClick += miEdit_Click;
      FTree.ItemDrag += FTree_ItemDrag;
      FTree.DragOver += FTree_DragOver;
      FTree.DragDrop += FTree_DragDrop;
      mnuContext.SetContextMenuEx(FTree, mnuContextRoot);

      FSplitter = new Splitter();
      FSplitter.Dock = DockStyle.Bottom;
      FSplitter.Visible = false;

      lblDescription = new DescriptionControl();
      lblDescription.Dock = DockStyle.Bottom;
      lblDescription.Height = 70;
      lblDescription.Visible = false;

      ParentControl.Controls.AddRange(new Control[] { FTree, FSplitter, lblDescription, FToolbar });
      FExpandedNodes = new List<string>();
      Localize();
    }

    /// <summary>
    /// Describes an item dragged from the "Data Dictionary" window.
    /// </summary>
    public class DraggedItem
    {
      /// <summary>
      /// The dragged object.
      /// </summary>
      public Object Obj;
      
      /// <summary>
      /// The text of dragged object.
      /// </summary>
      public string Text;
      
      internal DraggedItem(Object obj, string text)
      {
        Obj = obj;
        Text = text;
      }
    }
    
    /// <summary>
    /// Collection of dragged items.
    /// </summary>
    public class DraggedItemCollection : List<DraggedItem>
    {
        internal DraggedItemCollection() : base() { }
    }
    
    internal static class DragUtils
    {
        public static DraggedItemCollection GetAll(DragEventArgs e)
        {
            DraggedItemCollection items = (DraggedItemCollection)e.Data.GetData(typeof(DraggedItemCollection));
            
            if (items == null || items.Count == 0)
                return null;

            return items;
        }

        public static DraggedItem GetOne(DragEventArgs e)
        {
            DraggedItemCollection items = (DraggedItemCollection)e.Data.GetData(typeof(DraggedItemCollection));
            
            if (items == null || items.Count == 0)
                return null;

            return items[items.Count - 1];
        }
    }
  }
}
