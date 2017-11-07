using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;
using System.Windows.Forms;
using FastReport.Utils;
using System.Drawing;
using System.Collections;

namespace FastReport
{
  /// <summary>
  /// Class represent a smart tag that is used to choose a data column.
  /// </summary>
  public class DataColumnSmartTag : SmartTagBase
  {
    private DataSourceBase FRootDataSource;
    private string FDataColumn;
    
    /// <summary>
    /// Gets or sets the data column name.
    /// </summary>
    public string DataColumn
    {
      get { return FDataColumn; }
      set { FDataColumn = value; }
    }
    
    private void AddDataSource(ToolStripItemCollection items, DataSourceBase data, Relation rel, ArrayList processed)
    {
      bool alreadyProcessed = processed.Contains(data);
      processed.Add(data);

      if (!alreadyProcessed)
      {
        foreach (Relation relation in Obj.Report.Dictionary.Relations)
        {
          if (relation != rel && relation.Enabled && relation.ChildDataSource == data && relation.ParentDataSource != null)
          {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = relation.ParentDataSource.Alias;
            item.Image = Res.GetImage(58);
            items.Add(item);
            if (DataColumn.IndexOf(GetDataColumn(item)) == 0)
              item.Font = new Font(item.Font, FontStyle.Bold);
            else
              item.Font = new Font(item.Font, FontStyle.Regular);
            AddDataSource(item.DropDownItems, relation.ParentDataSource, relation, processed);
          }
        }
      }
      
      foreach (Column c in data.Columns)
      {
        ToolStripMenuItem item = new ToolStripMenuItem();
        item.Tag = c;
        item.Text = c.Alias;
        item.Image = Res.GetImage(c.GetImageIndex());
        item.Click += new EventHandler(item_Click);
        items.Add(item);
        if (DataColumn == GetDataColumn(item))
          item.Font = new Font(item.Font, FontStyle.Bold);
        else
          item.Font = new Font(item.Font, FontStyle.Regular);
      }
      
      processed.Remove(data);
    }

    private void AddDataSource(ToolStripItemCollection items, DataSourceBase data)
    {
      ArrayList processed = new ArrayList();
      AddDataSource(items, data, null, processed);
    }

    private void noneItem_Click(object sender, EventArgs e)
    {
      FDataColumn = "";
      ItemClicked();
    }

    private void item_Click(object sender, EventArgs e)
    {
      FDataColumn = GetDataColumn(sender as ToolStripMenuItem);
      ItemClicked();
    }

    private string GetDataColumn(ToolStripMenuItem item)
    {
      string result = "";
      while (item != null)
      {
        result = result == "" ? item.Text : item.Text + "." + result;
        item = item.OwnerItem as ToolStripMenuItem;
      }
      result = FRootDataSource.Alias + "." + result;
      return result;
    }

    /// <inheritdoc/>
    protected override void CreateItems()
    {
      ToolStripMenuItem noneItem = new ToolStripMenuItem();
      noneItem.Text = Res.Get("Misc,None");
      noneItem.Image = Res.GetImage(76);
      noneItem.Click += new EventHandler(noneItem_Click);
      if (DataColumn == "")
        noneItem.Font = new Font(noneItem.Font, FontStyle.Bold);
      Menu.Items.Add(noneItem);

      // find the datasource to display its columns
      FRootDataSource = FindRootDataSource();
      if (FRootDataSource != null)
        AddDataSource(Menu.Items, FRootDataSource);
    }
    
    /// <summary>
    /// Gets a root datasource for the object currently edited.
    /// </summary>
    /// <returns>The <b>DataSourceBase</b> object if found; <b>null</b> otherwise.</returns>
    protected virtual DataSourceBase FindRootDataSource()
    {
      DataSourceBase data = null;
      DataBand band = (Obj as ReportComponentBase).DataBand;
      if (band != null)
        data = band.DataSource;
      if (data == null)
      {
        ObjectCollection allData = Obj.Report.Dictionary.AllObjects;
        foreach (Base c in allData)
        {
          if (c is DataSourceBase && (c as DataSourceBase).Enabled)
          {
            data = c as DataSourceBase;
            break;
          }
        }
      }
      return data;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumnSmartTag"/> class with default settings.
    /// </summary>
    /// <param name="obj">Report object that owns this smart tag.</param>
    public DataColumnSmartTag(ComponentBase obj) : base(obj)
    {
    }
  }
}
