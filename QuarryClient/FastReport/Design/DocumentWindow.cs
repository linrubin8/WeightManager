using System;
using System.Collections.Generic;
using System.Text;
using FastReport.DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Drawing;

namespace FastReport.Design
{
  internal class DocumentWindow : TabItem
  {
    public Control ParentControl
    {
      get { return AttachedControl; }
    }

    public void AddToTabControl(FastReport.DevComponents.DotNetBar.TabControl tabs)
    {
      TabControlPanel panel = AttachedControl as TabControlPanel;
      tabs.Tabs.Add(this);
      tabs.Controls.Add(panel);
      tabs.ApplyDefaultPanelStyle(panel);
      panel.Padding = new System.Windows.Forms.Padding(0);
      
      if (tabs.Style == eTabStripStyle.VS2005Document)
      {
        panel.Style.BorderSide = eBorderSide.Bottom;
        panel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
        panel.Style.BorderColor.Color = SystemColors.ControlDark;
      }
    }

    public void Activate()
    {
      Parent.SelectedTab = this;
    }
    
    public void Close()
    {
      Parent.Tabs.Remove(this);
      Dispose();
    }
    
    public DocumentWindow()
    {
      TabControlPanel panel = new TabControlPanel();
      panel.Dock = DockStyle.Fill;
      
      panel.TabItem = this;
      AttachedControl = panel;
    }
  }
}
