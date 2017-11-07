using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Forms;

namespace FastReport.Design.PageDesigners
{
  internal class PageDesignerBase : Panel, IDesignerPlugin
  {
    #region Fields
    private Designer FDesigner;
    private PageBase FPage;
    private bool FLocked;
    #endregion

    #region Properties
    public PageBase Page
    {
      get { return FPage; }
      set { FPage = value; }
    }

    public Designer Designer
    {
      get { return FDesigner; }
    }

    public bool Locked
    {
      get { return FLocked; }
    }
    
    public Report Report
    {
      get { return Page.Report; }
    }

    public string PluginName
    {
      get { return Name; }
    }
    #endregion

    #region Public Methods
    public virtual Base GetParentForPastedObjects()
    {
      return Page;
    }
    
    // this method is called when the page becomes active. You may, for example, modify the main menu
    // in this method.
    public virtual void PageActivated()
    {
    }

    // this method is called when the page becomes inactive. You should remove items added to the main menu
    // in this method.
    public virtual void PageDeactivated()
    {
    }
    
    public virtual void FillObjects(bool resetSelection)
    {
      Designer.Objects.Clear();
      Designer.Objects.Add(Page.Report);
      Designer.Objects.Add(Page);
      foreach (Base b in Page.AllObjects)
      {
        Designer.Objects.Add(b);
      }

      if (resetSelection)
      {
        Designer.SelectedObjects.Clear();
        Designer.SelectedObjects.Add(Page);
      }
    }
    
    public virtual void Paste(ObjectCollection list, InsertFrom source)
    {
    }
    
    public virtual void CancelPaste()
    {
    }
    
    public virtual void SelectAll()
    {
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        SaveState();
    }
    #endregion

    #region IDesignerPlugin
    public virtual void Localize()
    {
    }

    public virtual void SaveState()
    {
    }

    public virtual void RestoreState()
    {
    }

    public virtual void SelectionChanged()
    {
    }

    public virtual void UpdateContent()
    {
    }

    public virtual void Lock()
    {
      FLocked = true;
    }

    public virtual void Unlock()
    {
      FLocked = false;
      UpdateContent();
    }

    public virtual DesignerOptionsPage GetOptionsPage()
    {
      return null;
    }
    
    public virtual void UpdateUIStyle()
    {
    }
    #endregion

    public PageDesignerBase(Designer designer) : base()
    {
      FDesigner = designer;
    }
  }

}
