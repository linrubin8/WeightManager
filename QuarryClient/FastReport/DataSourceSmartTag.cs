using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Data;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Represents a smart tag that is used to choose a data source.
  /// </summary>
  public class DataSourceSmartTag : SmartTagBase
  {
    private DataSourceBase FDataSource;
    
    /// <summary>
    /// Gets or sets a data source.
    /// </summary>
    public DataSourceBase DataSource
    {
      get { return FDataSource; }
      set { FDataSource = value; }
    }

    private void AddDataSource(ToolStripItemCollection items, DataSourceBase data)
    {
      ToolStripMenuItem item = new ToolStripMenuItem();
      item.Tag = data;
      item.Text = data.Alias;
      item.Image = Res.GetImage(222);
      item.Click += new EventHandler(item_Click);
      items.Add(item);
      if (DataSource == data)
        item.Font = new Font(item.Font, FontStyle.Bold);
      else
        item.Font = new Font(item.Font, FontStyle.Regular);
    }

    private void item_Click(object sender, EventArgs e)
    {
      FDataSource = (sender as ToolStripMenuItem).Tag as DataSourceBase;
      ItemClicked();
    }

    /// <inheritdoc/>
    protected override void CreateItems()
    {
      ToolStripMenuItem noneItem = new ToolStripMenuItem();
      noneItem.Text = Res.Get("Misc,None");
      noneItem.Image = Res.GetImage(76);
      noneItem.Click += new EventHandler(item_Click);
      if (DataSource == null)
        noneItem.Font = new Font(noneItem.Font, FontStyle.Bold);
      Menu.Items.Add(noneItem);

      foreach (DataConnectionBase connection in Obj.Report.Dictionary.Connections)
      {
        foreach (DataSourceBase data in connection.Tables)
        {
          if (data.Enabled)
            AddDataSource(Menu.Items, data);
        }
      }
      
      foreach (DataSourceBase data in Obj.Report.Dictionary.DataSources)
      {
        if (data.Enabled)
          AddDataSource(Menu.Items, data);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSourceSmartTag"/> class with default settings.
    /// </summary>
    /// <param name="obj">Report object that owns this smart tag.</param>
    public DataSourceSmartTag(ComponentBase obj) : base(obj)
    {
    }
  }
}
