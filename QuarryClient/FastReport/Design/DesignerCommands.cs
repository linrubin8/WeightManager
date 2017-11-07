using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Dialog;
using FastReport.Forms;
using FastReport.Code;
using FastReport.Design.ToolWindows;
using FastReport.Design.PageDesigners.Code;

namespace FastReport.Design
{
  /// <summary>
  /// The base class for all designer commands.
  /// </summary>
  public class DesignerCommand
  {
    private Designer FDesigner;
    private object FSender;

    internal Designer Designer
    {
      get { return FDesigner; }
    }

    internal Report ActiveReport
    {
      get { return Designer.ActiveReport; }
    }

    internal ReportTab ActiveReportTab
    {
      get { return Designer.ActiveReportTab; }
    }

    internal DesignerRestrictions Restrictions
    {
      get { return Designer.Restrictions; }
    }

    internal SelectedObjectCollection SelectedObjects
    {
      get { return Designer.SelectedObjects; }
    }

    internal SelectedReportComponents SelectedReportComponents
    {
      get { return Designer.SelectedReportComponents; }
    }

    internal void SetModified(string action)
    {
      Designer.SetModified(FSender, action);
    }

    /// <summary>
    /// Gets a value indicating that the command is enabled.
    /// </summary>
    /// <remarks>
    /// If you use own controls that invoke designer commands, use this property to refresh
    /// the <b>Enabled</b> state of a control that is bound to this command.
    /// </remarks>
    public bool Enabled
    {
      get { return GetEnabled(); }
    }

    /// <summary>
    /// Defines a custom action for this command.
    /// </summary>
    /// <remarks>
    /// Using custom action, you can override the standard behavior of this designer's command.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to override the "New..." command behavior.
    /// <code>
    /// // add an event handler that will be fired when the designer is run
    /// Config.DesignerSettings.DesignerLoaded += new EventHandler(DesignerSettings_DesignerLoaded);
    ///
    /// void DesignerSettings_DesignerLoaded(object sender, EventArgs e)
    /// {
    ///   // override "New..." command behavior
    ///   (sender as Designer).cmdNew.CustomAction += new EventHandler(cmdNew_CustomAction);
    /// }
    ///
    /// void cmdNew_CustomAction(object sender, EventArgs e)
    /// {
    ///   // show the "Label" wizard instead of standard "Add New Item" dialog
    ///   Designer designer = sender as Designer;
    ///   LabelWizard wizard = new LabelWizard();
    ///   wizard.Run(designer);
    /// }
    /// </code>
    /// </example>
    public event EventHandler CustomAction;

    /// <summary>
    /// Gets a value for the <b>Enabled</b> property.
    /// </summary>
    /// <returns><b>true</b> if command is enabled.</returns>
    protected virtual bool GetEnabled()
    {
      return true;
    }

    /// <summary>
    /// Invokes the command.
    /// </summary>
    public virtual void Invoke()
    {
    }

    /// <summary>
    /// Invokes the command with specified sender and event args.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event args.</param>
    /// <remarks>
    /// This method is compatible with standard <see cref="EventHandler"/> and can be passed
    /// to the event handler constructor directly.
    /// </remarks>
    public void Invoke(object sender, EventArgs e)
    {
      if (CustomAction != null)
        CustomAction(Designer, e);
      else
      {
        FSender = sender;
        Invoke();
      }
    }

    internal DesignerCommand(Designer designer)
    {
      FDesigner = designer;
    }
  }

  /// <summary>
  /// Represents the "File|New" command.
  /// </summary>
  public class NewCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontCreateReport &&
        (Designer.MdiMode || ActiveReportTab != null);
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (AddNewItemForm form = new AddNewItemForm(Designer))
      {
        if (form.ShowDialog() == DialogResult.OK)
          form.SelectedWizard.Run(Designer);
      }
    }

    internal NewCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "New Page" toolbar command.
  /// </summary>
  public class NewPageCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontCreatePage && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      ActiveReportTab.NewReportPage();
    }

    internal NewPageCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "New Dialog" toolbar command.
  /// </summary>
  public class NewDialogCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return
 !Restrictions.DontCreatePage && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      ActiveReportTab.NewDialog();
    }

    internal NewDialogCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Open..." command. Also can be used for loading a file
  /// from the recent files list.
  /// </summary>
  public class OpenCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontLoadReport &&
        (Designer.MdiMode || ActiveReportTab != null);
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      LoadFile("");
    }

    /// <summary>
    /// Loads a specified report file.
    /// </summary>
    /// <param name="fileName">File to load.</param>
    public void LoadFile(string fileName)
    {
      ReportTab reportTab = null;
      if (Designer.MdiMode)
      {
        // check if file is already opened
        if (!String.IsNullOrEmpty(fileName))
        {
          foreach (DocumentWindow c in Designer.Documents)
          {
            if (c is ReportTab && String.Compare((c as ReportTab).Report.FileName, fileName, true) == 0)
            {
              c.Activate();
              return;
            }
          }
        }

        Report report = new Report();
        report.Designer = Designer;
        reportTab = Designer.CreateReportTab(report);
      }
      else
        reportTab = ActiveReportTab;

      if (reportTab.LoadFile(fileName))
      {
        if (Designer.MdiMode)
          Designer.AddReportTab(reportTab);
      }
      else if (Designer.MdiMode)
        reportTab.Dispose();
    }

    internal OpenCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Save" command.
  /// </summary>
  public class SaveCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontSaveReport &&
        ActiveReportTab != null &&
        ActiveReportTab.Modified;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      ActiveReportTab.SaveFile(false);
    }

    internal SaveCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Save As..." command.
  /// </summary>
  public class SaveAsCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontSaveReport && ActiveReportTab != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      ActiveReportTab.SaveFile(true);
    }

    internal SaveAsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Save All" command.
  /// </summary>
  public class SaveAllCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      bool enabled = Designer.MdiMode && Designer.Documents.Count > 0;
      if (Designer.Documents.Count == 1 && Designer.Documents[0] is StartPageTab)
        enabled = false;
      return !Restrictions.DontSaveReport && enabled;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      foreach (DocumentWindow c in Designer.Documents)
      {
        if (c is ReportTab)
          (c as ReportTab).SaveFile(false);
      }
    }

    internal SaveAllCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Close" command.
  /// </summary>
  public class CloseCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return Designer.MdiMode && Designer.Documents.Count > 0;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (Designer.ActiveReportTab != null)
        Designer.CloseDocument(ActiveReportTab);
      else if (Designer.StartPage != null)
        Designer.CloseDocument(Designer.StartPage);
    }

    internal CloseCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Window|Close All" command.
  /// </summary>
  public class CloseAllCommand : CloseCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      int i = 0;
      while (i < Designer.Documents.Count)
      {
        DocumentWindow c = Designer.Documents[i];
        if (c is StartPageTab || !Designer.CloseDocument(c))
          i++;
      }
    }

    internal CloseAllCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Preview..." command.
  /// </summary>
  public class PreviewCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontPreviewReport && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      ActiveReportTab.Preview();
    }

    internal PreviewCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Page Setup..." command.
  /// </summary>
  public class PageSettingsCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontChangePageOptions &&
        ActiveReport != null &&
        ActiveReportTab.ActivePage is ReportPage;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      IHasEditor page = ActiveReportTab.ActivePage as IHasEditor;
      if (page.InvokeEditor())
      {
        SetModified("EditPage");
      }
    }

    internal PageSettingsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Report|Options..." command.
  /// </summary>
  public class ReportSettingsCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontChangeReportOptions && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (ReportOptionsForm form = new ReportOptionsForm(ActiveReport))
      {
        ActiveReport.ScriptText = ActiveReportTab.Script;
        if (form.ShowDialog() == DialogResult.OK)
        {
          ActiveReportTab.Script = ActiveReport.ScriptText;
          (ActiveReportTab.Plugins.Find("Code") as CodePageDesigner).UpdateLanguage();
          SetModified("EditReport");
        }
      }
    }

    internal ReportSettingsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Printer Setup..." command.
  /// </summary>
  public class PrinterSettingsCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontChangeReportOptions && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (PrinterSetupForm editor = new PrinterSetupForm())
      {
        editor.Report = ActiveReport;
        if (editor.ShowDialog() == DialogResult.OK)
        {
          SetModified("EditPrinter");
        }
      }
    }

    internal PrinterSettingsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Undo" command.
  /// </summary>
  public class UndoCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return ActiveReport != null && ActiveReportTab.CanUndo;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      Undo(1);
    }

    /// <summary>
    /// Undo several actions.
    /// </summary>
    /// <param name="actionsCount">Number of actions to undo.</param>
    public void Undo(int actionsCount)
    {
      ActiveReportTab.Undo(actionsCount);
    }

    internal UndoCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Redo" command.
  /// </summary>
  public class RedoCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return ActiveReport != null && ActiveReportTab.CanRedo;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      Redo(1);
    }

    /// <summary>
    /// Redo several actions.
    /// </summary>
    /// <param name="actionsCount">Number of actions to redo.</param>
    public void Redo(int actionsCount)
    {
      ActiveReportTab.Redo(actionsCount);
    }

    internal RedoCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Cut" command.
  /// </summary>
  public class CutCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      if (ActiveReportTab != null && ActiveReportTab.ActivePage == null)
        return true;
      bool enable = ActiveReportTab != null &&
        ActiveReportTab.ActivePage != null &&
        SelectedObjects.Count > 0;
      if (enable)
      {
        enable = false;
        // if at least one object can be copied, allow the operation
        foreach (Base c in SelectedObjects)
        {
          if (c.HasFlag(Flags.CanCopy))
          {
            enable = true;
            break;
          }
        }
      }
      return enable;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (ActiveReportTab != null && ActiveReportTab.ActivePage == null)
        Designer.Editor.Cut();
      else if (Enabled)
        Designer.Clipboard.Cut();
    }

    internal CutCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Copy" command.
  /// </summary>
  public class CopyCommand : CutCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      if (ActiveReportTab != null && ActiveReportTab.ActivePage == null)
        Designer.Editor.Copy();
      else if (Enabled)
        Designer.Clipboard.Copy();
    }

    internal CopyCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Paste" command.
  /// </summary>
  public class PasteCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      if (ActiveReportTab != null && ActiveReportTab.ActivePage == null)
        return true;
      return ActiveReportTab != null && ActiveReportTab.ActivePage != null && Designer.Clipboard.CanPaste;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (ActiveReportTab != null && ActiveReportTab.ActivePage == null)
        Designer.Editor.Paste();
      else
        Designer.Clipboard.Paste();
    }

    internal PasteCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Format Painter" toolbar command.
  /// </summary>
  public class FormatPainterCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return ActiveReportTab != null &&
        ActiveReportTab.ActivePage != null &&
        SelectedReportComponents.Count > 0;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      Designer.FormatPainter = !Designer.FormatPainter;
    }

    internal FormatPainterCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Delete" command.
  /// </summary>
  public class DeleteCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      bool enable = SelectedObjects.Count > 0;
      if (enable && SelectedObjects.Count == 1 &&
        (!SelectedObjects[0].HasFlag(Flags.CanDelete) ||
          SelectedObjects[0].HasRestriction(FastReport.Restrictions.DontDelete)))
        enable = false;
      return enable;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (SelectedObjects.IsPageSelected || SelectedObjects.IsReportSelected)
        return;

      foreach (Base c in SelectedObjects)
      {
        if (!c.HasFlag(Flags.CanDelete) || c.HasRestriction(FastReport.Restrictions.DontDelete))
          continue;
        else
        {
          if (c.IsAncestor)
          {
            FRMessageBox.Error(String.Format(Res.Get("Messages,DeleteAncestor"), c.Name));
            return;
          }
          c.Delete();
        }
      }
      SetModified("Delete");
    }

    internal DeleteCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Delete Page" command.
  /// </summary>
  public class DeletePageCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontDeletePage && ActiveReport != null &&
        ActiveReportTab.ActivePage != null &&
        ActiveReport.Pages.Count > 1 &&
        !ActiveReportTab.ActivePage.IsAncestor &&
        !(ActiveReportTab.ActivePage is ReportPage && (ActiveReportTab.ActivePage as ReportPage).Subreport != null);
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (Enabled)
      {
        ActiveReportTab.DeletePage();
      }
    }

    internal DeletePageCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Select All" command.
  /// </summary>
  public class SelectAllCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return ActiveReportTab != null && ActiveReportTab.ActivePage != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (ActiveReportTab != null)
        ActiveReportTab.ActivePageDesigner.SelectAll();
    }

    internal SelectAllCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Group" command.
  /// </summary>
  public class GroupCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return SelectedObjects.Count > 1;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      Dictionary<int, int> groups = new Dictionary<int, int>();

      // collect used group indices
      ObjectCollection allObjects = ActiveReport.AllObjects;
      foreach (Base c in Designer.Objects)
      {
        if (c is ComponentBase)
        {
          int index = (c as ComponentBase).GroupIndex;
          if (!groups.ContainsKey(index))
            groups.Add(index, 0);
        }
      }

      // find index that not in use
      int groupIndex;
      for (groupIndex = 1; ; groupIndex++)
      {
        if (!groups.ContainsKey(groupIndex))
          break;
      }

      foreach (Base c in SelectedObjects)
      {
        ComponentBase obj = c as ComponentBase;
        if (obj != null && obj.HasFlag(Flags.CanGroup))
          obj.GroupIndex = groupIndex;
      }
    }

    internal GroupCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Ungroup" command.
  /// </summary>
  public class UngroupCommand : GroupCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      foreach (Base c in SelectedObjects)
      {
        if (c is ComponentBase)
          (c as ComponentBase).GroupIndex = 0;
      }
    }

    internal UngroupCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit" command.
  /// </summary>
  public class EditCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return SelectedObjects.Count == 1 && SelectedObjects[0] is IHasEditor;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (!SelectedObjects[0].HasRestriction(FastReport.Restrictions.DontEdit) &&
        (SelectedObjects[0] as IHasEditor).InvokeEditor())
        SetModified("EditObject");
    }

    internal EditCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Edit|Find..." command.
  /// </summary>
  public class FindCommand : DesignerCommand
  {
    internal bool IsReplace;

    private void searchForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      (sender as Form).Dispose();
    }

    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return ActiveReportTab != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      SearchReplaceForm form = new SearchReplaceForm(Designer);
      form.Replace = IsReplace;
      form.FormClosed += new FormClosedEventHandler(searchForm_FormClosed);
      form.Show();
    }

    internal FindCommand(Designer designer)
      : base(designer)
    {
      IsReplace = false;
    }
  }

  /// <summary>
  /// Represents the "Edit|Replace..." command.
  /// </summary>
  public class ReplaceCommand : FindCommand
  {
    internal ReplaceCommand(Designer designer)
      : base(designer)
    {
      IsReplace = true;
    }
  }

  /// <summary>
  /// Represents the "Bring To Front" context menu command.
  /// </summary>
  public class BringToFrontCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      bool enable = SelectedObjects.Count > 0;
      if (enable && SelectedObjects.Count == 1 && !SelectedObjects[0].HasFlag(Flags.CanChangeOrder))
        enable = false;
      return enable;
    }

    /// <inheritdoc/>
    public override void  Invoke()
    {
      foreach (Base c in SelectedObjects)
      {
        c.ZOrder = 1000;
      }
      SetModified("Change");
    }

    internal BringToFrontCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Send To Back" context menu command.
  /// </summary>
  public class SendToBackCommand : BringToFrontCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      foreach (Base c in SelectedObjects)
      {
        c.ZOrder = 0;
      }
      SetModified("Change");
    }

    internal SendToBackCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Insert" command.
  /// </summary>
  /// <remarks>
  /// This command has no default action associated with it. Check the <b>Enabled</b> property
  /// to see if the insert operation is enabled.
  /// </remarks>
  public class InsertCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontInsertObject &&
        ActiveReportTab != null &&
        ActiveReportTab.ActivePage != null;
    }

    internal InsertCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Insert Band" command.
  /// </summary>
  /// <remarks>
  /// This command has no default action associated with it. Check the <b>Enabled</b> property
  /// to see if the insert operation is enabled.
  /// </remarks>
  public class InsertBandCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontInsertObject &&
        !Restrictions.DontInsertBand &&
        ActiveReportTab != null &&
        ActiveReportTab.ActivePage != null;
    }

    internal InsertBandCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Data|Add Data Source..." command.
  /// </summary>
  public class AddDataCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontCreateData && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (DataWizardForm form = new DataWizardForm(ActiveReport))
      {
        /*if (Designer.SelectedObjects.Count == 1 && Designer.SelectedObjects[0] is DataConnectionBase)
        {
          form.Connection = Designer.SelectedObjects[0] as DataConnectionBase;
          form.EditMode = true;
          form.VisiblePanelIndex = 1;
        }*/
        if (form.ShowDialog() == DialogResult.OK)
          SetModified("EditData");
      }
    }

    internal AddDataCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Data|Choose Report Data..." command.
  /// </summary>
  public class ChooseDataCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontEditData && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (ReportDataForm form = new ReportDataForm(ActiveReport))
      {
        if (form.ShowDialog() == DialogResult.OK)
          SetModified("SelectData");
      }
    }

    internal ChooseDataCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Recent Files" command.
  /// </summary>
  /// <remarks>
  /// This command has no default action associated with it. Check the <b>Enabled</b> property
  /// to see if the recent files list is enabled.
  /// </remarks>
  public class RecentFilesCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontShowRecentFiles &&
        (Designer.MdiMode || ActiveReportTab != null);
    }

    internal void Update(string newFile)
    {
      if (!String.IsNullOrEmpty(newFile))
      {
        if (Designer.RecentFiles.IndexOf(newFile) != -1)
          Designer.RecentFiles.RemoveAt(Designer.RecentFiles.IndexOf(newFile));
        Designer.RecentFiles.Add(newFile);
        while (Designer.RecentFiles.Count > 8)
          Designer.RecentFiles.RemoveAt(0);
      }
    }

    internal RecentFilesCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "File|Select Language..." command.
  /// </summary>
  public class SelectLanguageCommand : DesignerCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      using (SelectLanguageForm form = new SelectLanguageForm())
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          Designer.Plugins.Localize();
          if (ActiveReportTab != null)
            ActiveReportTab.Localize();
        }
      }
    }

    internal SelectLanguageCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "View|Options..." command.
  /// </summary>
  public class OptionsCommand : DesignerCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      using (DesignerOptionsForm options = new DesignerOptionsForm(Designer))
      {
        if (options.ShowDialog() == DialogResult.OK)
        {
          Designer.UpdatePlugins(null);
        }
      }
    }

    internal OptionsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "View|Start Page" command.
  /// </summary>
  public class ViewStartPageCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return Designer.MdiMode;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      foreach (DocumentWindow c in Designer.Documents)
      {
        if (c is StartPageTab)
        {
          c.Activate();
          return;
        }
      }
      Designer.AddStartPageTab();
    }

    internal ViewStartPageCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Report|Styles..." command.
  /// </summary>
  public class ReportStylesCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !Restrictions.DontChangeReportOptions && ActiveReport != null;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      if (ActiveReport == null)
        return;
      using (StyleEditorForm form = new StyleEditorForm(ActiveReport))
      {
        if (form.ShowDialog() == DialogResult.OK)
          SetModified("ChangeReport");
      }
    }

    internal ReportStylesCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Help|Help Contents..." command.
  /// </summary>
  public class HelpContentsCommand : DesignerCommand
  {
    private string GetHelpFile()
    {
      string helpFile = "";
      try
      {
        helpFile = Config.ApplicationFolder + "FRNetUserManual.chm";
        if (File.Exists(helpFile))
          return helpFile;
        helpFile = Config.ApplicationFolder + @"..\FastReport.Net Documentation\FRNetUserManual.chm";
        if (File.Exists(helpFile))
          return helpFile;
      }
      catch
      {
      }
      return "";
    }

    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return !String.IsNullOrEmpty(GetHelpFile());
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      Help.ShowHelp(Designer, GetHelpFile());
    }

    internal HelpContentsCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Help|About..." command.
  /// </summary>
  public class AboutCommand : DesignerCommand
  {
    /// <inheritdoc/>
    public override void Invoke()
    {
      using (AboutForm form = new AboutForm())
      {
        form.ShowDialog();
      }
    }

    internal AboutCommand(Designer designer)
      : base(designer)
    {
    }
  }

  /// <summary>
  /// Represents the "Show welcome window..." command.
  /// </summary>
  public class WelcomeCommand : DesignerCommand
  {
    /// <inheritdoc/>
    protected override bool GetEnabled()
    {
      return Config.WelcomeEnabled;
    }

    /// <inheritdoc/>
    public override void Invoke()
    {
      using (WelcomeForm form = new WelcomeForm(Designer))
      {
        form.ShowDialog();
      }
    }

    internal WelcomeCommand(Designer designer)
      : base(designer)
    {
    }
  }
}
