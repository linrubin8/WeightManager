using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;

namespace FastReport.Forms
{
  internal partial class HyperlinkEditorForm : BaseDialogForm
  {
    private ReportComponentBase FReportComponent;
    private Hyperlink FHyperlink;

    public ReportComponentBase ReportComponent
    {
      get { return FReportComponent; }
      set 
      { 
        FReportComponent = value; 
        FHyperlink = value.Hyperlink;
        UpdateControls();
        if (FReportComponent.FlagProvidesHyperlinkValue)
          IsMatrixHyperlink = true;
      }
    }
    
    public Hyperlink Hyperlink
    {
      get { return FHyperlink; }
    }
    
    public bool ModifyAppearance
    {
      get { return cbModifyAppearance.Checked; }
    }
    
    public bool IsMatrixHyperlink
    {
      get { return false; }
      set
      {
        if (value)
        {
          tbParameterExpression1.Enabled = false;
          tbParameterExpression2.Enabled = false;
          tbParameterValue1.Enabled = false;
          tbParameterValue2.Enabled = false;
        }  
      }
    }
    
    private Report Report
    {
      get { return FReportComponent.Report; }
    }

    private void UpdateControls()
    {
      cbxReportPage.PopulateList(Report, typeof(ReportPage), ReportComponent.Page);
      cbxReportParameter2.Report = Report;
      
      switch (Hyperlink.Kind)
      {
        case HyperlinkKind.URL:
          pageControl1.ActivePageIndex = 0;
          tbUrlValue.Text = Hyperlink.Value;
          tbUrlExpression.Text = Hyperlink.Expression;
          break;
        
        case HyperlinkKind.PageNumber:
          pageControl1.ActivePageIndex = 1;
          tbPageNumberValue.Text = Hyperlink.Value;
          tbPageNumberExpression.Text = Hyperlink.Expression;
          break;
        
        case HyperlinkKind.Bookmark:
          pageControl1.ActivePageIndex = 2;
          tbBookmarkValue.Text = Hyperlink.Value;
          tbBookmarkExpression.Text = Hyperlink.Expression;
          break;
        
        case HyperlinkKind.DetailReport:
          pageControl1.ActivePageIndex = 3;
          tbReport.Text = Hyperlink.DetailReportName;
          cbxReportParameter1.Text = Hyperlink.ReportParameter;
          tbParameterValue1.Text = Hyperlink.Value;
          tbParameterExpression1.Text = Hyperlink.Expression;
          break;
        
        case HyperlinkKind.DetailPage:
          pageControl1.ActivePageIndex = 4;
          cbxReportPage.SelectedItem = Report.FindObject(Hyperlink.DetailPageName);
          cbxReportParameter2.Text = Hyperlink.ReportParameter;
          tbParameterValue2.Text = Hyperlink.Value;
          tbParameterExpression2.Text = Hyperlink.Expression;
          break;
        
        case HyperlinkKind.Custom:
          pageControl1.ActivePageIndex = 5;
          tbCustomValue.Text = Hyperlink.Value;
          tbCustomExpression.Text = Hyperlink.Expression;
          break;
      }
    }
    
    private void UpdateHyperlink()
    {
      switch (pageControl1.ActivePageIndex)
      {
        case 0:
          Hyperlink.Kind = HyperlinkKind.URL;
          Hyperlink.Value = tbUrlValue.Text;
          Hyperlink.Expression = tbUrlExpression.Text;
          break;

        case 1:
          Hyperlink.Kind = HyperlinkKind.PageNumber;
          Hyperlink.Value = tbPageNumberValue.Text;
          Hyperlink.Expression = tbPageNumberExpression.Text;
          break;

        case 2:
          Hyperlink.Kind = HyperlinkKind.Bookmark;
          Hyperlink.Value = tbBookmarkValue.Text;
          Hyperlink.Expression = tbBookmarkExpression.Text;
          break;

        case 3:
          Hyperlink.Kind = HyperlinkKind.DetailReport;
          Hyperlink.DetailReportName = tbReport.Text;
          Hyperlink.ReportParameter = cbxReportParameter1.Text;
          Hyperlink.Value = tbParameterValue1.Text;
          Hyperlink.Expression = tbParameterExpression1.Text;
          break;

        case 4:
          Hyperlink.Kind = HyperlinkKind.DetailPage;
          Hyperlink.DetailPageName = cbxReportPage.SelectedObject == null ? 
            "" : 
            ((ReportPage)cbxReportPage.SelectedItem).Name;
          Hyperlink.ReportParameter = cbxReportParameter2.Text;
          Hyperlink.Value = tbParameterValue2.Text;
          Hyperlink.Expression = tbParameterExpression2.Text;
          if (cbxReportPage.SelectedObject != null)
            ((ReportPage)cbxReportPage.SelectedItem).Visible = false;
          break;

        case 5:
          Hyperlink.Kind = HyperlinkKind.Custom;
          Hyperlink.Value = tbCustomValue.Text;
          Hyperlink.Expression = tbCustomExpression.Text;
          break;
      }
    }
    
    private void pageControl1_PageSelected(object sender, EventArgs e)
    {
      string hint = "";
      switch (pageControl1.ActivePageIndex)
      {
        case 0:
          hint = "HintUrl";
          break;

        case 1:
          hint = "HintPageNumber";
          break;

        case 2:
          hint = "HintBookmark";
          break;

        case 3:
          hint = "HintReport";
          break;

        case 4:
          hint = "HintReportPage";
          break;

        case 5:
          hint = "HintCustom";
          break;
      }
      
      lblHint2.Text = Res.Get("Forms,HyperlinkEditor," + hint);
    }

    private void tbReport_ButtonClick(object sender, EventArgs e)
    {
      OpenSaveDialogEventArgs args = new OpenSaveDialogEventArgs(Report.Designer);
      args.FileName = tbReport.Text;

      Config.DesignerSettings.OnCustomOpenDialog(Report.Designer, args);
      if (!args.Cancel)
      {
        tbReport.Text = args.FileName;
        cbxReportParameter1.Text = "";
      }  
    }

    private void cbxReportParameter1_DropDownOpening(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(tbReport.Text))
      {
        cbxReportParameter1.Report = Report;
        return;
      }
      
      using (Report report = new Report())
      {
        // open report
        OpenSaveReportEventArgs args = new OpenSaveReportEventArgs(report, tbReport.Text, null, false);
        Config.DesignerSettings.OnCustomOpenReport(Report.Designer, args);

        cbxReportParameter1.Report = report;
      }  
    }

    private void tbUrlExpression_ButtonClick(object sender, EventArgs e)
    {
      tbUrlExpression.Text = Editors.EditExpression(Report, tbUrlExpression.Text);
    }

    private void tbPageNumberExpression_ButtonClick(object sender, EventArgs e)
    {
      tbPageNumberExpression.Text = Editors.EditExpression(Report, tbPageNumberExpression.Text);
    }

    private void tbBookmarkExpression_ButtonClick(object sender, EventArgs e)
    {
      tbBookmarkExpression.Text = Editors.EditExpression(Report, tbBookmarkExpression.Text);
    }

    private void tbParameterExpression1_ButtonClick(object sender, EventArgs e)
    {
      tbParameterExpression1.Text = Editors.EditExpression(Report, tbParameterExpression1.Text);
    }

    private void tbParameterExpression2_ButtonClick(object sender, EventArgs e)
    {
      tbParameterExpression2.Text = Editors.EditExpression(Report, tbParameterExpression2.Text);
    }

    private void tbCustomExpression_ButtonClick(object sender, EventArgs e)
    {
      tbCustomExpression.Text = Editors.EditExpression(Report, tbCustomExpression.Text);
    }

    private void HyperlinkEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        UpdateHyperlink();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,HyperlinkEditor");
      Text = res.Get("");

      pnUrl.Text = res.Get("Url");
      lblUrl.Text = res.Get("UrlValue");
      lblUrlExpression.Text = res.Get("UrlExpression");

      pnPageNumber.Text = res.Get("PageNumber");
      lblPageNumber.Text = res.Get("PageNumberValue");
      lblPageNumberExpression.Text = res.Get("PageNumberExpression");

      pnBookmark.Text = res.Get("Bookmark");
      lblBookmark.Text = res.Get("BookmarkValue");
      lblBookmarkExpression.Text = res.Get("BookmarkExpression");

      pnReport.Text = res.Get("Report");
      lblReport.Text = res.Get("ReportName");
      lblReportParameter1.Text = res.Get("ParameterName");
      lblParameterValue1.Text = res.Get("ParameterValue");
      lblParameterExpression1.Text = res.Get("ParameterExpression");

      pnReportPage.Text = res.Get("ReportPage");
      lblReportPage.Text = res.Get("PageName");
      lblReportParameter2.Text = res.Get("ParameterName");
      lblParameterValue2.Text = res.Get("ParameterValue");
      lblParameterExpression2.Text = res.Get("ParameterExpression");

      pnCustom.Text = res.Get("Custom");
      lblCustom.Text = res.Get("CustomValue");
      lblCustomExpression.Text = res.Get("CustomExpression");

      lblHint1.Text = res.Get("Hint");
      cbModifyAppearance.Text = res.Get("ModifyAppearance");
      
      tbUrlExpression.Image = Res.GetImage(52);
      tbPageNumberExpression.Image = Res.GetImage(52);
      tbBookmarkExpression.Image = Res.GetImage(52);
      tbReport.Image = Res.GetImage(1);
      tbParameterExpression1.Image = Res.GetImage(52);
      tbParameterExpression2.Image = Res.GetImage(52);
      tbCustomExpression.Image = Res.GetImage(52);
    }
    
    public HyperlinkEditorForm()
    {
      InitializeComponent();
      Localize();
    }
  }
}

