using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Forms
{
  internal partial class BaseReportWizardForm : BaseWizardForm
  {
    private Report FReport;
    
    public Report Report
    {
      get { return FReport; }
    }
    
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataSourceBase DataSource
    {
      get { return tvDataSources.SelectedNode == null ? null : tvDataSources.SelectedNode.Tag as DataSourceBase; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Column> SelectedColumns
    {
      get
      {
        List<Column> selectedColumns = new List<Column>();
        foreach (ListViewItem item in lvSelectedColumns.Items)
        {
          selectedColumns.Add(item.Tag as Column);
        }
        return selectedColumns;
      }
    }

    public override int VisiblePanelIndex
    {
      get { return base.VisiblePanelIndex; }
      set
      {
        base.VisiblePanelIndex = value;
        if (value == 1)
          UpdateColumnControls();
      }
    }

    #region "Select Datasource" page
    private void UpdateDatasources()
    {
      foreach (Base c in FReport.Dictionary.AllObjects)
      {
        if (c is DataSourceBase)
          (c as DataSourceBase).Enabled = true;
      }
      tvDataSources.CreateNodes(FReport.Dictionary);

      if (tvDataSources.Nodes.Count > 0)
        tvDataSources.SelectedNode = tvDataSources.Nodes[0];
    }

    private void btnCreateNewDatasource_Click(object sender, EventArgs e)
    {
      using (DataWizardForm form = new DataWizardForm(FReport))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          tvDataSources.CreateNodes(FReport.Dictionary);
          DataConnectionBase connection = form.Connection;
          string tableName = "";
          foreach (DataSourceBase table in connection.Tables)
          {
            if (table.Enabled)
            {
              tableName = table.Alias;
              break;
            }
          }
          if (tableName != "")
            tvDataSources.SelectedItem = tableName;
        }
      }
    }

    private void tvDataSources_AfterSelect(object sender, TreeViewEventArgs e)
    {
      UpdateColumns();
      btnNext.Enabled = DataSource != null;
    }
    #endregion

    #region "Select Columns" page
    private void UpdateColumns()
    {
      lvAvailableColumns.Items.Clear();
      lvSelectedColumns.Items.Clear();
      
      if (DataSource != null)
      {
        foreach (Column column in DataSource.Columns)
        {
          ListViewItem item = lvAvailableColumns.Items.Add(column.Alias, column.GetImageIndex());
          item.Tag = column;
        }
      }

      if (lvAvailableColumns.Items.Count > 0)
        lvAvailableColumns.Items[0].Selected = true;
      UpdateColumnControls();
    }

    private void UpdateColumnControls()
    {
      btnAddColumn.Enabled = lvAvailableColumns.SelectedItems.Count > 0;
      btnRemoveColumn.Enabled = lvSelectedColumns.SelectedItems.Count > 0;
      btnAddAllColumns.Enabled = lvAvailableColumns.Items.Count > 0;
      btnRemoveAllColumns.Enabled = lvSelectedColumns.Items.Count > 0;
      btnNext.Enabled = lvSelectedColumns.Items.Count > 0;

      bool reorderEnabled = lvSelectedColumns.SelectedItems.Count == 1;
      btnColumnUp.Enabled = reorderEnabled;
      btnColumnDown.Enabled = reorderEnabled;
      if (reorderEnabled)
      {
        if (lvSelectedColumns.SelectedIndices[0] == 0)
          btnColumnUp.Enabled = false;
        if (lvSelectedColumns.SelectedIndices[0] == lvSelectedColumns.Items.Count - 1)
          btnColumnDown.Enabled = false;
      }
    }
    
    private void lvAvailableColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateColumnControls();
    }

    private void lvSelectedColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateColumnControls();
    }

    public virtual void ColumnsChanged()
    {
      UpdateColumnControls();
    }
    
    private void btnAddColumn_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvAvailableColumns.SelectedItems.Count > 0)
      {
        ListViewItem item = lvAvailableColumns.SelectedItems[0];
        index = item.Index;
        lvAvailableColumns.Items.Remove(item);
        lvSelectedColumns.Items.Add(item);
        item.Selected = false;
      }
      if (index >= lvAvailableColumns.Items.Count)
        index = lvAvailableColumns.Items.Count - 1;
      if (index < 0)
        index = 0;  
      if (index < lvAvailableColumns.Items.Count)
        lvAvailableColumns.Items[index].Selected = true;
      ColumnsChanged();  
    }

    private void btnAddAllColumns_Click(object sender, EventArgs e)
    {
      while (lvAvailableColumns.Items.Count > 0)
      {
        ListViewItem item = lvAvailableColumns.Items[0];
        lvAvailableColumns.Items.Remove(item);
        lvSelectedColumns.Items.Add(item);
      }
      ColumnsChanged();
    }

    private void btnRemoveColumn_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvSelectedColumns.SelectedItems.Count > 0)
      {
        ListViewItem item = lvSelectedColumns.SelectedItems[0];
        index = item.Index;
        lvSelectedColumns.Items.Remove(item);
        lvAvailableColumns.Items.Add(item);
        item.Selected = false;
      }
      if (index >= lvSelectedColumns.Items.Count)
        index = lvSelectedColumns.Items.Count - 1;
      if (index < 0)
        index = 0;
      if (index < lvSelectedColumns.Items.Count)
        lvSelectedColumns.Items[index].Selected = true;
      ColumnsChanged();
    }

    private void btnRemoveAllColumns_Click(object sender, EventArgs e)
    {
      while (lvSelectedColumns.Items.Count > 0)
      {
        ListViewItem item = lvSelectedColumns.Items[0];
        lvSelectedColumns.Items.Remove(item);
        lvAvailableColumns.Items.Add(item);
      }
      ColumnsChanged();
    }

    private void btnColumnUp_Click(object sender, EventArgs e)
    {
      ListViewItem item = lvSelectedColumns.SelectedItems[0];
      int index = item.Index;
      lvSelectedColumns.Items.Remove(item);
      lvSelectedColumns.Items.Insert(index - 1, item);
      item.Selected = true;
      ColumnsChanged();
    }

    private void btnColumnDown_Click(object sender, EventArgs e)
    {
      ListViewItem item = lvSelectedColumns.SelectedItems[0];
      int index = item.Index;
      lvSelectedColumns.Items.Remove(item);
      lvSelectedColumns.Items.Insert(index + 1, item);
      item.Selected = true;
      ColumnsChanged();
    }
    #endregion

    public virtual void InitWizard(Report report)
    {
      FReport = report;
      VisiblePanelIndex = 0;
      lvAvailableColumns.SmallImageList = Res.GetImages();
      lvSelectedColumns.SmallImageList = Res.GetImages();
      btnCreateNewDatasource.Enabled = !Config.DesignerSettings.Restrictions.DontCreateData;
      UpdateDatasources();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,BaseReportWizard");
      
      panDataSource.Text = res.Get("SelectDataSourcePage");
      lblSelectDataSource.Text = res.Get("SelectDataSource");
      btnCreateNewDatasource.Text = res.Get("CreateNewDatasource");
      panColumns.Text = res.Get("SelectColumnsPage");
      lblSelectColumns.Text = res.Get("SelectColumns");
      lblAvailableColumns.Text = res.Get("AvailableColumns");
      lblSelectedColumns.Text = res.Get("SelectedColumns");

      btnColumnUp.Image = Res.GetImage(208);
      btnColumnDown.Image = Res.GetImage(209);
    }
    
    public BaseReportWizardForm()
    {
      InitializeComponent();
    }
  }
}

