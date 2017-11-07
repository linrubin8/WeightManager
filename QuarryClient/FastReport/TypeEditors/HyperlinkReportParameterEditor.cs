using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Forms;
using FastReport.Utils;
using FastReport.Controls;
using FastReport.Data;

namespace FastReport.TypeEditors
{
  internal class HyperlinkReportParameterEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;
    private static Size FSize = new Size(0, 0);

    public override bool IsDropDownResizable
    {
      get { return true; }
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void tree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Action == TreeViewAction.ByMouse && (e.Node.Tag is Parameter || e.Node.Index == 0))
        edSvc.CloseDropDown();
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      Hyperlink hyperlink = context.Instance is Hyperlink ?
        context.Instance as Hyperlink : ((object[])context.Instance)[0] as Hyperlink;

      if (hyperlink != null && 
        (hyperlink.Kind == HyperlinkKind.DetailReport ||
        hyperlink.Kind == HyperlinkKind.DetailPage))
      {
        Report report = null;
        bool isAnotherReport = hyperlink.Kind == HyperlinkKind.DetailReport && 
          !String.IsNullOrEmpty(hyperlink.DetailReportName);
        if (isAnotherReport)
          report = hyperlink.GetReport(false);
        else
          report = hyperlink.Report;
        
        DataTreeView tree = new DataTreeView();
        tree.BorderStyle = BorderStyle.None;
        tree.ShowDataSources = false;
        tree.ShowRelations = false;
        tree.ShowColumns = false;
        tree.ShowNone = true;
        tree.ShowVariables = false;
        tree.ShowParameters = true;
        tree.ShowTotals = false;
        tree.CreateNodes(report.Dictionary);
        tree.SelectedItem = (string)Value;
        tree.AfterSelect += new TreeViewEventHandler(tree_AfterSelect);
        if (FSize.Width > 0)
          tree.Size = FSize;
        edSvc.DropDownControl(tree);

        if (isAnotherReport)
          report.Dispose();
        FSize = tree.Size;
        return tree.SelectedItem;
      }
      
      return Value;
    }
  }
}
