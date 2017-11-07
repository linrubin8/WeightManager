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
  /// Provides a user interface for choosing a data column.
  /// </summary>
  public class DataColumnEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;
    private static Size FSize = new Size(0, 0);

    /// <inheritdoc/>
    public override bool IsDropDownResizable
    {
      get { return true; }
    }

    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if ((e.Node.Tag is Column && !(e.Node.Tag is DataSourceBase) && (e.Node.Tag as Column).Columns.Count == 0) || 
        e.Node.Tag == null)
        edSvc.CloseDropDown();
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      if (context != null && context.Instance is Base)
      {
        Base component = context.Instance as Base;
        DataTreeView tree = new DataTreeView();
        tree.BorderStyle = BorderStyle.None;
        tree.ShowRelations = true;
        tree.ShowColumns = true;
        tree.ShowEnabledOnly = true;
        tree.ShowNone = true;
        tree.CreateNodes(component.Report.Dictionary);
        tree.SelectedItem = (string)Value;
        tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(tree_NodeMouseClick);
        if (FSize.Width > 0)
          tree.Size = FSize;
        edSvc.DropDownControl(tree);
        FSize = tree.Size;
        return tree.SelectedItem;
      }
      else
        return Value;
    }
  }
}
