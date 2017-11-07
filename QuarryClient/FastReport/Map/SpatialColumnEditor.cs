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

namespace FastReport.Map
{
  internal class SpatialColumnEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;

    private void lb_Click(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      MapLayer layer = null;
      if (context != null && context.Instance is MapLayer)
        layer = context.Instance as MapLayer;

      if (layer != null)
      {
        string[] columnNames = layer.SpatialColumns.ToArray();
        ListBox lb = new ListBox();
        lb.Items.AddRange(columnNames);
        lb.SelectedItem = Value;
        lb.BorderStyle = BorderStyle.None;
        lb.Height = lb.ItemHeight * lb.Items.Count;
        lb.Click += new EventHandler(lb_Click);
        edSvc.DropDownControl(lb);

        if (lb.SelectedIndex >= 0 && lb.SelectedIndex < columnNames.Length)
          return columnNames[lb.SelectedIndex];
      }
      return Value;
    }
  }
}
