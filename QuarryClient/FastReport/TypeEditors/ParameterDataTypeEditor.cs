using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using FastReport.Controls;
using FastReport.Forms;
using FastReport.Data;
using System.Collections;

namespace FastReport.TypeEditors
{
  internal class ParameterDataTypeEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void lb_SelectedIndexChanged(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      
      if (context != null && context.Instance is CommandParameter)
      {
        CommandParameter parameter = context.Instance as CommandParameter;
        Type dataType = parameter.GetUnderlyingDataType;
        if (dataType != null)
        {
          string[] names = Enum.GetNames(dataType);
          ArrayList values = new ArrayList();
          values.AddRange(Enum.GetValues(dataType));
          
          ListBox lb = new ListBox();
          lb.Height = 200;
          lb.BorderStyle = BorderStyle.None;
          lb.Items.AddRange(names);
          for (int i = 0; i < values.Count; i++)
          {
            if ((int)values[i] == (int)Value)
            {
              lb.SelectedIndex = i;
              break;
            }
          }
          lb.SelectedIndexChanged += new EventHandler(lb_SelectedIndexChanged);
          
          edSvc.DropDownControl(lb);
          if (lb.SelectedIndex != -1)
            return values[lb.SelectedIndex];
        }
      }
      
      return Value;
    }
  }
}
