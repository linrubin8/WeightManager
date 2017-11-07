using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Dialog;
using FastReport.Data;

namespace FastReport.Forms
{
  internal partial class GridControlColumnsEditorForm : BaseDialogForm
  {
    private GridControl FGrid;
    private GridControl FSaveGrid;
    private Timer FTimer;
    
    public GridControl Grid
    {
      get { return FGrid; }
      set 
      { 
        FGrid = value;
        FSaveGrid = new GridControl();
        FSaveGrid.Assign(value);
        PopulateColumns();
        btnAddAll.Enabled = FGrid.DataSource != null;
      }
    }

    private GridControlColumnCollection Columns
    {
      get { return FGrid.Columns; }
    }

    private GridControlColumn CurrentColumn
    {
      get
      {
        if (lvColumns.SelectedItems.Count == 0)
          return null;
        return lvColumns.SelectedItems[0].Tag as GridControlColumn;
      }
    }

    private void PopulateColumns()
    {
      lvColumns.Items.Clear();
      foreach (GridControlColumn c in Columns)
      {
        ListViewItem li = lvColumns.Items.Add(c.HeaderText, 113);
        li.Tag = c;
      }
      if (lvColumns.Items.Count > 0)
        lvColumns.Items[0].Selected = true;
      UpdateControls();
      lvColumns.Focus();
    }

    private void UpdateControls()
    {
      bool enabled = CurrentColumn != null;
      btnRemove.Enabled = enabled;
      btnUp.Enabled = enabled;
      btnDown.Enabled = enabled;
      gbProperties.Enabled = enabled;
      
      List<object> selectedObjects = new List<object>();
      foreach (ListViewItem li in lvColumns.SelectedItems)
      {
        selectedObjects.Add(li.Tag);
      }
      frPropertyGrid1.SelectedObjects = selectedObjects.ToArray();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      GridControlColumn column = new GridControlColumn();
      column.HeaderText = "Column";
      Columns.Add(column);
      ListViewItem li = lvColumns.Items.Add(column.HeaderText, 113);
      li.Tag = column;
      li.BeginEdit();
      UpdateControls();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      while (lvColumns.SelectedItems.Count > 0)
      {
        GridControlColumn c = lvColumns.SelectedItems[0].Tag as GridControlColumn;
        Columns.Remove(c);
        lvColumns.Items.Remove(lvColumns.SelectedItems[0]);
      }
    }

    private void btnAddAll_Click(object sender, EventArgs e)
    {
      FGrid.Columns.Clear();
      foreach (Column column in FGrid.DataSource.Columns)
      {
        GridControlColumn gridColumn = new GridControlColumn();
        gridColumn.HeaderText = column.Alias;
        gridColumn.DataColumn = FGrid.DataSource.Alias + "." + column.Alias;
        FGrid.Columns.Add(gridColumn);
      }
      PopulateColumns();
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      if (lvColumns.SelectedItems.Count != 1)
        return;
      int index = lvColumns.SelectedIndices[0];
      if (index > 0)
      {
        ListViewItem li = lvColumns.SelectedItems[0];
        lvColumns.Items.Remove(li);
        lvColumns.Items.Insert(index - 1, li);
        GridControlColumn c = Columns[index];
        Columns.Remove(c);
        Columns.Insert(index - 1, c);
      }
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      if (lvColumns.SelectedItems.Count != 1)
        return;
      int index = lvColumns.SelectedIndices[0];
      if (index < lvColumns.Items.Count - 1)
      {
        ListViewItem li = lvColumns.SelectedItems[0];
        lvColumns.Items.Remove(li);
        lvColumns.Items.Insert(index + 1, li);
        GridControlColumn c = Columns[index];
        Columns.Remove(c);
        Columns.Insert(index + 1, c);
      }
    }

    private void lvColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
      FTimer.Start();
    }

    private void FTimer_Tick(object sender, EventArgs e)
    {
      UpdateControls();
      FTimer.Stop();
    }

    private void lvColumns_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
      if (e.Label == null)
        return;
      if (e.Label == "")
      {
        e.CancelEdit = true;
        return;
      }
      GridControlColumn c = lvColumns.Items[e.Item].Tag as GridControlColumn;
      c.HeaderText = e.Label;
      UpdateControls();
    }

    private void frPropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      List<object> selectedObjects = new List<object>();
      selectedObjects.AddRange(frPropertyGrid1.SelectedObjects);
      foreach (ListViewItem li in lvColumns.Items)
      {
        GridControlColumn column = li.Tag as GridControlColumn;
        if (selectedObjects.Contains(column))
          li.Text = column.HeaderText;
      }
    }

    private void GridControlColumnsEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult != DialogResult.OK)
      {
        FGrid.Assign(FSaveGrid);
      }  
      FTimer.Dispose();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,GridControlColumnsEditor");
      Text = res.Get("");
      gbColumns.Text = res.Get("Columns");
      gbProperties.Text = res.Get("Properties");
      btnAdd.Text = res.Get("Add");
      btnRemove.Text = res.Get("Remove");
      btnAddAll.Text = res.Get("AddAll");
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
    }
    
    public GridControlColumnsEditorForm()
    {
      InitializeComponent();
      Localize();
      lvColumns.SmallImageList = Res.GetImages();
      FTimer = new Timer();
      FTimer.Interval = 50;
      FTimer.Tick += new EventHandler(FTimer_Tick);
    }
  }
}

