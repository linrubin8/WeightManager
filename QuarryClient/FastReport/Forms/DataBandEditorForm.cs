using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Controls;

namespace FastReport.Forms
{
  internal partial class DataBandEditorForm : BaseDialogForm
  {
    private DataBand FBand;
    private bool FFirstRun;
    
    private Report Report
    {
      get { return FBand.Report; }
    }
    
    private DataColumnComboBox[] ComboArray
    {
      get { return new DataColumnComboBox[] { cbxSort1, cbxSort2, cbxSort3 }; }
    }
    
    private RadioButton[] AscArray
    {
      get { return new RadioButton[] { rbSortAsc1, rbSortAsc2, rbSortAsc3 }; }
    }

    private RadioButton[] DescArray
    {
      get { return new RadioButton[] { rbSortDesc1, rbSortDesc2, rbSortDesc3 }; }
    }

    private void FillSort(DataSourceBase dataSource, bool reset)
    {
      SortCollection sort = FBand.Sort;
      for (int i = 0; i < ComboArray.Length; i++)
      {
        ComboArray[i].Report = Report;
        ComboArray[i].DataSource = dataSource;
        
        if (i >= sort.Count || reset)
        {
          ComboArray[i].Text = "";
          AscArray[i].Checked = true;
          DescArray[i].Checked = false;
        }
        else
        {
          ComboArray[i].Text = sort[i].Expression;
          AscArray[i].Checked = !sort[i].Descending;
          DescArray[i].Checked = sort[i].Descending;
        }
      }
    }
    
    private void GetSort()
    {
      FBand.Sort.Clear();
      
      for (int i = 0; i < ComboArray.Length; i++)
      {
        if (!String.IsNullOrEmpty(ComboArray[i].Text))
          FBand.Sort.Add(new Sort(ComboArray[i].Text, DescArray[i].Checked));
      }
    }
    
    private void tvDataSource_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (!FFirstRun)
      {
        DataSourceBase data = tvDataSource.SelectedNode.Tag as DataSourceBase;
        FillSort(data, true);
      }
      FFirstRun = false;
    }

    private void tvDataSource_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void tbFilter_ButtonClick(object sender, EventArgs e)
    {
      tbFilter.Text = Editors.EditExpression(Report, tbFilter.Text);
    }

    private void DataBandEditorForm_Shown(object sender, EventArgs e)
    {
      tvDataSource.Focus();
    }
    
    private void DataBandEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }
    
    private void Init()
    {
      FFirstRun = true;
      tbFilter.Image = Res.GetImage(52);
      tvDataSource.CreateNodes(FBand.Report.Dictionary);
      tvDataSource.SelectedItem = FBand.DataSource == null ? "" : FBand.DataSource.FullName;
      tvDataSource.Visible = tvDataSource.Nodes.Count > 1;
      lblNoData.Visible = !tvDataSource.Visible;
      FillSort(FBand.DataSource, false);
      tbFilter.Text = FBand.Filter;
      
      Config.RestoreFormState(this);
    }
    
    private void Done()
    {
      if (DialogResult == DialogResult.OK)
      {
        FBand.DataSource = tvDataSource.SelectedNode == null ? null : tvDataSource.SelectedNode.Tag as DataSourceBase;
        GetSort();
        FBand.Filter = tbFilter.Text;
      }
      Config.SaveFormState(this);
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,DataBandEditor");
      Text = res.Get("");
      pnDataSource.Text = res.Get("DataSource");
      lblNoData.Text = res.Get("NoData");
      pnSort.Text = res.Get("Sort");
      lblSort1.Text = res.Get("SortBy");
      lblSort2.Text = res.Get("ThenBy");
      lblSort3.Text = res.Get("ThenBy");
      for (int i = 0; i < AscArray.Length; i++)
      {
        AscArray[i].Text = res.Get("Ascending");
        DescArray[i].Text = res.Get("Descending");
      }
      
      pnFilter.Text = res.Get("Filter");
      lblFilter.Text = res.Get("Expression");
    }
    
    public DataBandEditorForm(DataBand band)
    {
      FBand = band;
      InitializeComponent();
      Localize();
      Init();
    }
  }
}

