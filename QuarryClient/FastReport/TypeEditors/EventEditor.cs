using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms.Design;

namespace FastReport.TypeEditors
{
  internal class EventEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    private void OnClick(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    public override object EditValue(ITypeDescriptorContext context, 
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      if (context != null && context.Instance != null)
      {
        Base component = null;
        if (context.Instance is Base)
          component = context.Instance as Base;
        else if (context.Instance is object[])
          component = ((object[])context.Instance)[0] as Base;
        Report report = component.Report;  

        string scriptEventName = context.PropertyDescriptor.Name;
        // cut off "Event" ("xxxEvent")
        string eventName = scriptEventName.Replace("Event", "");
        EventInfo eventInfo = component.GetType().GetEvent(eventName);
        // get handlers that match this event
        List<string> handlers = report.CodeHelper.GetEvents(eventInfo.EventHandlerType);

        edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
        ListBox lb = new ListBox();
        lb.Height = 100;
        lb.BorderStyle = BorderStyle.None;

        lb.Items.AddRange(handlers.ToArray());
        if (Value != null)
          lb.SelectedIndex = lb.Items.IndexOf(Value);
        lb.DoubleClick += new EventHandler(OnClick);
        lb.Click += new EventHandler(OnClick);

        edSvc.DropDownControl(lb);
              
        if (lb.SelectedItem != null)
          return ((string)lb.SelectedItem);
      }  
      return Value;
    }
      
  }
}