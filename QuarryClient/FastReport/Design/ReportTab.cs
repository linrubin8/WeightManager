using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using FastReport.Controls;
using FastReport.Code;
using FastReport.Utils;
using FastReport.Dialog;
using FastReport.Design.PageDesigners;
using FastReport.Design.PageDesigners.Code;
using FastReport.Editor;
using FastReport.Forms;
using FastReport.Preview;
using FastReport.Data;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design
{
  internal partial class ReportTab : DocumentWindow
  {
    #region Fields
    private bool FDisposed;
    private Designer FDesigner;
    private Report FReport;
    private UndoRedo FUndoRedo;
    private BlobStore FBlobStore;
    private bool FModified;
    private PageBase FActivePage;
    private PluginCollection FPlugins;
    private TabStrip FTabs;
    private bool FTabMoved;
    private bool FUpdatingTabs;
    #endregion
    
    #region Properties
    public Report Report
    {
      get { return FReport; }
    }
    
    public Designer Designer
    {
      get { return FDesigner; }
    }

    public BlobStore BlobStore
    {
      get { return FBlobStore; }
    }

    public PageBase ActivePage
    {
      get { return FActivePage; }
      set
      {
        if (FActivePage != value)
        {
          PageDesignerBase oldPd = GetPageDesigner(FActivePage);
          PageDesignerBase newPd = GetPageDesigner(value);
          oldPd.CancelPaste();
          if (oldPd != newPd)
            oldPd.PageDeactivated();
          FActivePage = value;
          foreach (TabItem tab in FTabs.Tabs)
          {
            if (tab.Tag == FActivePage)
            {
              FTabs.SelectedTab = tab;
              break;
            }
          }
          newPd.Page = value;
          newPd.FillObjects(true);
          newPd.BringToFront();
          if (oldPd != newPd)
            newPd.PageActivated();

          FDesigner.SelectionChanged(null);
        }
      }
    }
    
    public PageDesignerBase ActivePageDesigner
    {
      get { return GetPageDesigner(ActivePage); }
    }

    public int ActivePageIndex
    {
      get { return FTabs.SelectedTabIndex; }
      set 
      { 
        FTabs.SelectedTabIndex = value;
        ActivePage = FTabs.SelectedTab.Tag as PageBase;
      }
    }

    public bool Modified
    {
      get { return FModified; }
    }

    public CodePageDesigner Editor
    {
      get { return GetPageDesigner(null) as CodePageDesigner; }
    }

    public string Script
    {
      get { return Editor.Script; }
      set { Editor.Script = value; }
    }

    public string ReportName
    {
      get 
      {
        if (String.IsNullOrEmpty(FReport.FileName))
          return Res.Get("Designer,Untitled");
        return FReport.FileName;
      }    
    }
    
    internal UndoRedo UndoRedo
    {
      get { return FUndoRedo; }
    }
    
    internal bool CanUndo
    {
      get
      {
        int i = ActivePageIndex;
        if (i == 0)
          return Editor.CanUndo();
        return UndoRedo.UndoCount > 1;
      }
    }

    internal bool CanRedo
    {
      get
      {
        int i = ActivePageIndex;
        if (i == 0)
          return Editor.CanRedo();
        return UndoRedo.RedoCount > 0;
      }
    }
    
    public PluginCollection Plugins
    {
      get { return FPlugins; }
    }
    #endregion

    #region Private Methods
    // (Deprecated) for better appearance, play with ui controls offset
    private Point UIOffset
    {
      get
      {
          return new Point();
      }
    }

    private PageDesignerBase GetPageDesigner(PageBase page)
    {
      Type pdType = page == null ? typeof(CodePageDesigner) : page.GetPageDesignerType();
      // try to find existing page designer
      foreach (IDesignerPlugin plugin in FPlugins)
      {
        if (plugin.GetType() == pdType)
          return plugin as PageDesignerBase;
      }
      // not found, create new one
      PageDesignerBase pd = Activator.CreateInstance(pdType, new object[] { FDesigner }) as PageDesignerBase;
      pd.Location = new Point(UIOffset.X, 0);
      pd.Size = new Size(ParentControl.Width - UIOffset.X, ParentControl.Height - FTabs.Height - UIOffset.Y);
      ParentControl.Controls.Add(pd);
      FPlugins.Add(pd);
      pd.RestoreState();
      pd.UpdateUIStyle();
      return pd;
    }

    private void FTabs_TabMoved(object sender, TabStripTabMovedEventArgs e)
    {
      // do not allow to move the "Code" tab
      if (e.OldIndex == 0 || e.NewIndex == 0)
        e.Cancel = true;
      else
        FTabMoved = true;
    }

    private void FTabs_MouseUp(object sender, MouseEventArgs e)
    {
      if (FTabMoved)
      {
        // clear pages. Do not call Clear because pages will be disposed then
        while (FReport.Pages.Count > 0)
        {
          FReport.Pages.RemoveAt(0);
        }
        // add pages in new order
        foreach (TabItem tab in FTabs.Tabs)
        {
          if (tab.Tag is PageBase)
            FReport.Pages.Add(tab.Tag as PageBase);
        }
        Designer.SetModified(null, "ChangePagesOrder");
      }
      FTabMoved = false;
    }

    private void FTabs_TabItemClose(object sender, TabStripActionEventArgs e)
    {
      // do not allow to close the "Code" tab
      if (FTabs.SelectedTab.Tag == null)
        e.Cancel = true;
      else
        Designer.cmdDeletePage.Invoke();
    }

    private void FTabs_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
    {
      if (FUpdatingTabs)
        return;

      ActivePage = FTabs.SelectedTab.Tag as PageBase;
    }

    private void ParentControl_Resize(object sender, EventArgs e)
    {
      foreach (Control c in ParentControl.Controls)
      {
        if (c is PageDesignerBase)
          c.Size = new Size(ParentControl.Width - UIOffset.X, ParentControl.Height - FTabs.Height - UIOffset.Y);
      }
    }
    #endregion

    #region Protected Methods
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!FDisposed)
      {
        for (int i = 0; i < FPlugins.Count; i++)
        {
          IDesignerPlugin plugin = FPlugins[i];
          if (plugin is IDisposable)
            (plugin as IDisposable).Dispose();
        }
        if (FDesigner.MdiMode && FReport != null)
          FReport.Dispose();
        FUndoRedo.Dispose();
        if (FBlobStore != null)
          FBlobStore.Dispose();
        FDisposed = true;
      }
    }
    #endregion

    #region Public Methods
    public bool CanClose()
    {
      ActivePageDesigner.PageDeactivated();
      if (Designer.AskSave && !SaveCurrentFile())
      {
        ActivePageDesigner.PageActivated();
        return false;
      }
      return true;
    }

    internal void ReportActivated()
    {
      ActivePageDesigner.FillObjects(true);
      ActivePageDesigner.PageActivated();
      UpdateCaption();
    }
    
    internal void ReportDeactivated()
    {
      ActivePageDesigner.PageDeactivated();
    }
    
    internal void SetModified()
    {
      FModified = true;
      UpdateCaption();
    }

    internal void SetModified(object sender, string action, string objName)
    {
      FModified = true;
      FReport.ScriptText = Script;
      if (action != "Script-no-undo")
      {
        FUndoRedo.AddUndo(action, objName);
        FUndoRedo.ClearRedo();
      }
      bool resetSelection = action == "Delete" ? true : false;
      ActivePageDesigner.FillObjects(resetSelection);
      InitPages(ActivePageIndex);
      UpdateCaption();
    }

    internal void ResetModified()
    {
      FModified = false;
      FUndoRedo.ClearUndo();
      FUndoRedo.ClearRedo();
      FUndoRedo.AddUndo("Load", null);
      Editor.Edit.Modified = false;
      UpdateCaption();
    }

    public void SwitchToCode()
    {
      ActivePage = null;
    }

    internal void UpdateCaption()
    {
      Text = ReportName;
      string titleText = "";
      if (String.IsNullOrEmpty(Config.DesignerSettings.Text))
        titleText = "FastReport - " + ReportName;
      else
        titleText = Config.DesignerSettings.Text + ReportName;

      if (Designer.cmdSave.Enabled)
        titleText += "*";

      Form form = Designer.FindForm();
      if (form != null && (form.GetType().Name.EndsWith("DesignerForm") || form.GetType().Name.EndsWith("DesignerRibbonForm")))
        form.Text = titleText;
    }

    public void InitReport()
    {
      // check if report has a reportpage
      bool reportPageExists = false;
      foreach (PageBase page in Report.Pages)
      {
        if (page is ReportPage)
          reportPageExists = true;
      }

      // if it has not, create the page 
      if (!reportPageExists)
      {
        ReportPage reportPage = new ReportPage();
        Report.Pages.Add(reportPage);
        reportPage.CreateUniqueName();
        reportPage.SetDefaults();
      }
      
      Script = Report.ScriptText;
      InitPages(1);
      UpdateCaption();
      ResetModified();
      FPlugins.Unlock();
    }

    public void InitPages(int index)
    {
      FUpdatingTabs = true;
      
      // add tabs
      FTabs.Tabs.Clear();
      
      // code tab
      TabItem codeTab = new TabItem();
      codeTab.Text = Res.Get("Designer,Workspace,Code");
      codeTab.Image = Res.GetImage(61);
      FTabs.Tabs.Add(codeTab);
      if (Designer.Restrictions.DontEditCode)
        codeTab.Visible = false;

      // page tabs
      foreach (PageBase page in FReport.Pages)
      {
        TabItem pageTab = new TabItem();
        pageTab.Tag = page;
        pageTab.Text = page.PageName;
        ObjectInfo info = RegisteredObjects.FindObject(page);
        pageTab.Image = info.Image;
        FTabs.Tabs.Add(pageTab);
      }
      
      FUpdatingTabs = false;
      ActivePageIndex = index;
      FTabs.Refresh();
    }
    
    public void Localize()
    {
      Plugins.Localize();
      InitPages(ActivePageIndex);
    }
    
    public void UpdateUIStyle()
    {
        FTabs.Style = UIStyleUtils.GetTabStripStyle1(Designer.UIStyle);

        //HACK
        if (StyleManager.Style == eStyle.VisualStudio2012Light)
        {
            FTabs.ColorScheme.TabItemHotText = Color.White;
            FTabs.ColorScheme.TabItemSelectedText = Color.White;
        }
        else
        {
            FTabs.ColorScheme = new TabColorScheme(FTabs.Style);
        }

      Plugins.UpdateUIStyle();
      ParentControl_Resize(this, EventArgs.Empty);
    }
    #endregion

    #region Designer Commands
    private bool SaveCurrentFile()
    {
      if (Modified)
      {
        DialogResult res = AskSave();
        if (res == DialogResult.Cancel)
          return false;
        if (Designer.IsPreviewPageDesigner)
        {
          if (res == DialogResult.No)
            Designer.Modified = false;
        }
        else
        {
          if (res == DialogResult.Yes)
            return SaveFile(false);
        }  
      }
      return true;
    }

    internal bool EmptyReport(bool askSave)
    {
      if (askSave)
      {
        if (!SaveCurrentFile())
          return false;
      }
      Designer.Lock();
      try
      {
        Report.FileName = "";
        Report.Clear();
        Report.Dictionary.ReRegisterData();
        Config.DesignerSettings.OnReportLoaded(this, new ReportLoadedEventArgs(Report));
      }
      finally
      {
        InitReport();
        Designer.Unlock();
      }
      return true;
    }

    private DialogResult AskSave()
    {
      string text = Designer.IsPreviewPageDesigner ?
        Res.Get("Messages,SaveChangesToPreviewPage") : String.Format(Res.Get("Messages,SaveChanges"), ReportName);
      return FRMessageBox.Confirm(text, MessageBoxButtons.YesNoCancel);
    }

    public bool LoadFile(string fileName)
    {
      OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs(Designer);
      
      // empty fileName: user pressed "Open" button, show open dialog.
      // non-empty fileName: user choosed a report from recent file list, just load the specified report.
      if (String.IsNullOrEmpty(fileName))
      {
        Config.DesignerSettings.OnCustomOpenDialog(Designer, e);
        if (e.Cancel)
          return false;
        fileName = e.FileName;
      }
      
      bool result = SaveCurrentFile();
      if (result)
      {
        try
        {
          Designer.Lock();
          OpenSaveReportEventArgs e1 = new OpenSaveReportEventArgs(Report, fileName, e.Data, false);
          Config.DesignerSettings.OnCustomOpenReport(Designer, e1);
          Report.FileName = fileName;

          if (Path.GetExtension(fileName).ToLower() != ".rpt")
          {
              Designer.cmdRecentFiles.Update(fileName);
          }

          Config.DesignerSettings.OnReportLoaded(this, new ReportLoadedEventArgs(Report));
          result = true;
        }
#if! DEBUG
        catch (Exception ex)
        {
          EmptyReport(false);
          FRMessageBox.Error(String.Format(Res.Get("Messages,CantLoadReport") + "\r\n" + ex.Message, fileName));
          result = false;
        }
#endif
        finally
        {
          InitReport();
          Designer.Unlock();
        }
      }
      
      return result;
    }

    public bool LoadAutoSaveFile(string filePath)
    {
        OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs(Designer);

        string fileName = Config.AutoSaveFile;

        bool result = SaveCurrentFile();
        if (result)
        {
            try
            {
                Designer.Lock();
                OpenSaveReportEventArgs e1 = new OpenSaveReportEventArgs(Report, fileName, e.Data, false);
                Config.DesignerSettings.OnCustomOpenReport(Designer, e1);
                Report.FileName = filePath;
                Config.DesignerSettings.OnReportLoaded(this, new ReportLoadedEventArgs(Report));
                result = true;
            }
#if! DEBUG
        catch (Exception ex)
        {
          EmptyReport(false);
          FRMessageBox.Error(String.Format(Res.Get("Messages,CantLoadReport") + "\r\n" + ex.Message, fileName));
          result = false;
        }
#endif
            finally
            {
                InitReport();
                Designer.Unlock();
            }
        }

        return result;
    }

    public bool SaveFile(bool saveAs)
    {
      if (File.Exists(Config.AutoSaveFileName))
        File.Delete(Config.AutoSaveFileName);
      if (File.Exists(Config.AutoSaveFile))
        File.Delete(Config.AutoSaveFile);

      // update report's script
      Report.ScriptText = Script;

      while (true)
      {
        OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs(Designer);
        string fileName = Report.FileName;

        // show save dialog in case of untitled report or "save as"
        if (saveAs || String.IsNullOrEmpty(fileName))
        {
          if (String.IsNullOrEmpty(fileName))
            fileName = Res.Get("Designer,Untitled");
          e.FileName = fileName;
          Config.DesignerSettings.OnCustomSaveDialog(Designer, e);
          if (e.Cancel)
            return false;

          fileName = e.FileName;
        }

        OpenSaveReportEventArgs e1 = new OpenSaveReportEventArgs(Report, fileName, e.Data, e.IsPlugin);

        try
        {
          Config.DesignerSettings.OnCustomSaveReport(Designer, e1);
          // don't change the report name if plugin was used
          if (e.IsPlugin)
            fileName = Report.FileName;
          Report.FileName = fileName;
          FModified = false;
          Designer.UpdatePlugins(null);
          if (!e.IsPlugin)
            Designer.cmdRecentFiles.Update(fileName);
          UpdateCaption();
          return true;
        }
        catch
        {
          // something goes wrong, suggest to save to another place
          FRMessageBox.Error(Res.Get("Messages,CantSaveReport"));
          saveAs = true;
        }
      }
    }

    public bool AutoSaveFile()
    {
        bool result = false;

        Report.ScriptText = Script;

        OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs(Designer);
        OpenSaveReportEventArgs e1 = new OpenSaveReportEventArgs(Report, Config.AutoSaveFile, e.Data, e.IsPlugin);

        try
        {
            Designer.Lock();

            Directory.CreateDirectory(Config.AutoSaveFolder);

            using (FileStream f = new FileStream(Config.AutoSaveFile, FileMode.Create))
                Report.Save(f);

            File.WriteAllText(Config.AutoSaveFileName, Report.FileName);

            result = true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            Designer.Unlock();
        }

        return result;
    }
    
    public void Preview()
    {
      ActivePageDesigner.CancelPaste();
      int i = ActivePageIndex;
      Report.ScriptText = Script;
      Designer.MessagesWindow.ClearMessages();
      UndoRedo.AddUndo("Preview", "Report");
      Designer.Lock();

      bool saveProgress = Config.ReportSettings.ShowProgress;
      Config.ReportSettings.ShowProgress = true;
      
      try
      {
        if (Report.Prepare())
          Config.DesignerSettings.OnCustomPreviewReport(Report, EventArgs.Empty);
      }
#if! DEBUG
      catch (Exception e)
      {
        if (!(e is CompilerException))
        {
          using (ExceptionForm form = new ExceptionForm(e))
          {
            form.ShowDialog();
          }
        }
        else
          Designer.MessagesWindow.Show();
      }
#endif
      finally
      {
        Config.ReportSettings.ShowProgress = saveProgress;

        Image previewPicture = null;
        if (Report.ReportInfo.SavePreviewPicture)
        {
          previewPicture = Report.ReportInfo.Picture;
          Report.ReportInfo.Picture = null;
        }
        UndoRedo.GetUndo(1);
        UndoRedo.ClearRedo();
        if (Report.ReportInfo.SavePreviewPicture)
          Report.ReportInfo.Picture = previewPicture;
        // clear dead objects
        Designer.Objects.Clear();
        Designer.SelectedObjects.Clear();
        
        InitPages(i);
        Designer.Unlock();
      }
    }

    public void Undo(int actionsCount)
    {
      int i = ActivePageIndex;
      if (i == 0)
      {
        Editor.Source.Undo();
        return;
      }
      
      Designer.Lock();
      Report.ScriptText = Script;
      UndoRedo.GetUndo(actionsCount);
      Script = Report.ScriptText;
      InitPages(i);
      Designer.Unlock();
    }

    public void Redo(int actionsCount)
    {
      int i = ActivePageIndex;
      if (i == 0)
      {
        Editor.Source.Redo();
        return;
      }

      Designer.Lock();
      UndoRedo.GetRedo(actionsCount);
      Script = Report.ScriptText;
      InitPages(i);
      Designer.Unlock();
    }

    public void NewReportPage()
    {
      ReportPage page = new ReportPage();
      Report.Pages.Add(page);
      page.SetDefaults();
      page.CreateUniqueName();
      InitPages(Report.Pages.Count);
      Designer.SetModified(this, "AddPage");
    }

    public void NewDialog()
    {
      DialogPage page = new DialogPage();
      Report.Pages.Add(page);
      page.SetDefaults();
      page.CreateUniqueName();
      InitPages(Report.Pages.Count);
      Designer.SetModified(this, "AddPage");
    }

    public void DeletePage()
    {
      Designer.Lock();
      Report.Pages.Remove(ActivePage);
      InitPages(Report.Pages.Count);
      Designer.Unlock();
      Designer.SetModified(this, "DeletePage");
    }
    #endregion

    public ReportTab(Designer designer, Report report)
      : base()
    {
      FDesigner = designer;
      FReport = report;
      FPlugins = new PluginCollection(FDesigner);
      FUndoRedo = new UndoRedo(this);
      if (!designer.IsPreviewPageDesigner)
        FBlobStore = new BlobStore(false);
      
      FTabs = new TabStrip();
      FTabs.Height = 30;
      FTabs.Dock = DockStyle.Bottom;
      FTabs.TabAlignment = eTabStripAlignment.Bottom;
      FTabs.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
      FTabs.AutoHideSystemBox = true;
      FTabs.ShowFocusRectangle = false;
      FTabs.CloseButtonVisible = false;
      
      FTabs.TabItemClose += new TabStrip.UserActionEventHandler(FTabs_TabItemClose);
      FTabs.TabMoved += new TabStrip.TabMovedEventHandler(FTabs_TabMoved);
      FTabs.MouseUp += new MouseEventHandler(FTabs_MouseUp);
      FTabs.SelectedTabChanged += new TabStrip.SelectedTabChangedEventHandler(FTabs_SelectedTabChanged);
      
      ParentControl.Controls.Add(FTabs);
      ParentControl.Resize += new EventHandler(ParentControl_Resize);
      
      InitReport();
    }
  }
}
