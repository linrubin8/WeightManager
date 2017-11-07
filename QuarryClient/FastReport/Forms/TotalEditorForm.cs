using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Code;
using FastReport.Data;

namespace FastReport.Forms
{
  internal partial class TotalEditorForm : BaseDialogForm
  {
    private ReportPage FPage;
    private Report FReport;
    private Total FTotal;
    
    public Total Total
    {
      get { return FTotal; }
      set 
      { 
        FTotal = value; 
        UpdateControls();
      }
    }
    
    private void UpdateControls()
    {
      tbTotalName.Text = FTotal.Name;
      cbxFunction.SelectedIndex = (int)FTotal.TotalType;
      
      if (FTotal.TotalType != TotalType.Count)
        cbxDataColumn.Text = FTotal.Expression;

      if (FTotal.Evaluator != null && cbxDataBand.Items.IndexOf(FTotal.Evaluator) != -1)
        cbxDataBand.SelectedItem = FTotal.Evaluator;
      
      cbInvisibleRows.Checked = FTotal.IncludeInvisibleRows;
      tbEvaluateCondition.Text = FTotal.EvaluateCondition;

      if (FTotal.PrintOn != null && cbxPrintOn.Items.IndexOf(FTotal.PrintOn) != -1)
        cbxPrintOn.SelectedItem = FTotal.PrintOn;
      
      cbResetAfterPrint.Checked = FTotal.ResetAfterPrint;
      cbResetRepeated.Checked = FTotal.ResetOnReprint;  
    }

    private void tbEvaluateCondition_ButtonClick(object sender, EventArgs e)
    {
      using (ExpressionEditorForm form = new ExpressionEditorForm(FReport))
      {
        form.ExpressionText = tbEvaluateCondition.Text;
        if (form.ShowDialog() == DialogResult.OK)
          tbEvaluateCondition.Text = form.ExpressionText;
      }
    }

    private void cbxDataBand_SelectedIndexChanged(object sender, EventArgs e)
    {
      cbxPrintOn.Items.Clear();
      cbxPrintOn.Items.Add(0);
      
      if (cbxDataBand.SelectedIndex != -1)
      {
        DataBand dataBand = cbxDataBand.SelectedItem as DataBand;
        ObjectCollection list = new ObjectCollection();
        ReportPage page = dataBand.Page as ReportPage;
        while (page != null)
        {
          list.AddRange(page.AllObjects);
          if (page.Subreport != null)
            page = page.Subreport.Page as ReportPage;
          else
            break;  
        }
        
        foreach (Base c in list)
        {
          Base band = c as BandBase;
          if (c is ChildBand)
            band = (c as ChildBand).GetTopParentBand;
          if (band is DataFooterBand || band is GroupFooterBand || 
            band is ColumnFooterBand || band is PageFooterBand || band is ReportSummaryBand)
            cbxPrintOn.Items.Add(c);
        }
      }

      cbxPrintOn.SelectedIndex = 0;
    }

    private void cbxFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
      cbxDataColumn.Enabled = cbxFunction.SelectedIndex != 4;
    }

    private void TotalEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        Done();
    }

    private void Init()
    {
      cbxDataColumn.Report = FReport;
      cbxFunction.SelectedIndex = 0;
      
      foreach (Base c in FReport.AllObjects)
      {
        if (c is DataBand)
          cbxDataBand.Items.Add(c);
      }
      if (cbxDataBand.Items.Count > 0)
        cbxDataBand.SelectedIndex = 0;
        
      Total total = new Total();
      total.Name = FReport.Dictionary.CreateUniqueName("Total");
      Total = total;
    }

    private void Done()
    {
      FTotal.Name = tbTotalName.Text;
      FTotal.TotalType = (TotalType)cbxFunction.SelectedIndex;
      
      FTotal.Expression = cbxDataColumn.Text;

      if (cbxDataBand.SelectedIndex != -1)
        FTotal.Evaluator = cbxDataBand.SelectedItem as DataBand;
      else
        FTotal.Evaluator = null;
      
      FTotal.IncludeInvisibleRows = cbInvisibleRows.Checked;
      FTotal.EvaluateCondition = tbEvaluateCondition.Text;

      if (cbxPrintOn.SelectedIndex != 0)
        FTotal.PrintOn = cbxPrintOn.SelectedItem as BandBase;
      else
        FTotal.PrintOn = null;
      FTotal.ResetAfterPrint = cbResetAfterPrint.Checked;
      FTotal.ResetOnReprint = cbResetRepeated.Checked;  
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,TotalEditor");
      Text = res.Get("");
      gbTotal.Text = res.Get("Total");
      lblTotalName.Text = res.Get("TotalName");
      lblFunction.Text = res.Get("Function");
      lblDataColumnOrExpression.Text = res.Get("DataColumnOrExpression");
      lblDataBand.Text = res.Get("DataBand");
      lblEvaluateCondition.Text = res.Get("EvaluateCondition");
      lblPrintOn.Text = res.Get("PrintOn");
      gbOptions.Text = res.Get("Options");
      cbResetAfterPrint.Text = res.Get("ResetAfterPrint");
      cbResetRepeated.Text = res.Get("ResetRepeated");
      cbInvisibleRows.Text = res.Get("InvisibleRows");
      tbEvaluateCondition.Image = Res.GetImage(52);
      cbxFunction.Items.AddRange(new object[] { 
        res.Get("Sum"), res.Get("Min"), res.Get("Max"), res.Get("Avg"), res.Get("Count") });
    }

    public TotalEditorForm(Designer designer)
    {
      FReport = designer.ActiveReport;
      FPage = designer.ActiveReportTab.ActivePage as ReportPage;
      InitializeComponent();
      Localize();
      Init();
    }
  }


}

