using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Controls;

namespace FastReport.Forms
{
  internal class DesignerOptionsForm : BaseDialogForm
  {
    private Designer FDesigner;
    private List<DesignerOptionsPage> FOptionsPages;
    private PageControl pageControl1;
  
    private void InitializeComponent()
    {
      this.pageControl1 = new FastReport.Controls.PageControl();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(364, 276);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(444, 276);
      // 
      // pageControl1
      // 
      this.pageControl1.Location = new System.Drawing.Point(12, 12);
      this.pageControl1.Name = "pageControl1";
      this.pageControl1.SelectorWidth = 139;
      this.pageControl1.Size = new System.Drawing.Size(508, 252);
      this.pageControl1.TabIndex = 1;
      this.pageControl1.Text = "pageControl1";
      // 
      // DesignerOptionsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(531, 310);
      this.Controls.Add(this.pageControl1);
      this.Name = "DesignerOptionsForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignerOptions_FormClosing);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.pageControl1, 0);
      this.ResumeLayout(false);

    }

    private void AddPages(DesignerOptionsPage page)
    {
      if (page != null)
      {
        foreach (TabPage tab in page.tc1.TabPages)
        {
          PageControlPage panel = new PageControlPage();
          panel.Text = tab.Text;
          panel.Dock = DockStyle.Fill;
          panel.BackColor = SystemColors.Window;
          while (tab.Controls.Count > 0)
            tab.Controls[0].Parent = panel;
          pageControl1.Controls.Add(panel);
        }
        
        FOptionsPages.Add(page);
        page.Init();
      }
    }

    private void DesignerOptions_FormClosing(object sender, FormClosingEventArgs e)
    {
      foreach (DesignerOptionsPage page in FOptionsPages)
      {
        page.Done(DialogResult);
      }
    }

    public override void Localize()
    {
      base.Localize();
      Text = Res.Get("Designer,Options");
    }

    public DesignerOptionsForm(Designer designer)
    {
      FDesigner = designer;
      FOptionsPages = new List<DesignerOptionsPage>();
      InitializeComponent();
      Localize();
      
      // add default pages
      PluginsOptions pluginsOptions = new PluginsOptions(designer);
      AddPages(pluginsOptions);

      SavingPageOptions savingOptions = new SavingPageOptions(designer);
      AddPages(savingOptions);
      
      List<Type> processedPluginTypes = new List<Type>();
      foreach (IDesignerPlugin plugin in FDesigner.Plugins)
      {
        if (processedPluginTypes.IndexOf(plugin.GetType()) != -1)
          continue;
        DesignerOptionsPage page = plugin.GetOptionsPage();
        AddPages(page);
        processedPluginTypes.Add(plugin.GetType());
      }
      
      int activePageIndex = 0;
      if (FDesigner.ActiveReportTab != null)
      {
        foreach (IDesignerPlugin plugin in FDesigner.ActiveReportTab.Plugins)
        {
          if (processedPluginTypes.IndexOf(plugin.GetType()) != -1)
            continue;
          DesignerOptionsPage page = plugin.GetOptionsPage();
          AddPages(page);
          processedPluginTypes.Add(plugin.GetType());
      
          if (plugin == FDesigner.ActiveReportTab.ActivePageDesigner)
            activePageIndex = pageControl1.Controls.Count - 1;
        }
      }
      
      pageControl1.ActivePageIndex = activePageIndex;
    }
  }
}
