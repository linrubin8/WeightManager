using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using FastReport.Controls;
using FastReport.Data;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for choosing a data source.
  /// </summary>
  internal class DataSourceEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;
    private static Size FSize = new Size(0, 0);

    private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Node.Tag is DataSourceBase || e.Node.Tag == null)
        edSvc.CloseDropDown();
    }

    public override bool IsDropDownResizable
    {
      get { return true; }
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      if (context != null && context.Instance != null)
      {
        Base component = context.Instance is Base ? context.Instance as Base : ((object[])context.Instance)[0] as Base;
        DataTreeView tree = new DataTreeView();
        tree.BorderStyle = BorderStyle.None;
        tree.ShowColumns = false;
        tree.ShowEnabledOnly = true;
        tree.ShowNone = true;
        tree.CreateNodes(component.Report.Dictionary);
        tree.SelectedItem = Value == null ? "" : (Value as DataSourceBase).FullName;
        tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(tree_NodeMouseClick);
        if (FSize.Width > 0)
          tree.Size = FSize;
        edSvc.DropDownControl(tree);
        FSize = tree.Size;
        return tree.SelectedNode.Tag as DataSourceBase;
      }
      else
        return Value;
    }
  }
}
