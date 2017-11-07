using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using FastReport.Editor;
using FastReport.Utils;
using FastReport.Code;
using FastReport.Data;
using FastReport.Dialog;
using FastReport.Controls;
using FastReport.Design.ToolWindows;
using FastReport.Design.PageDesigners.Code;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using FastReport.DevComponents.DotNetBar;
using FastReport.Forms;

namespace FastReport.Design
{
  /// <summary>
  /// Represents the report's designer control.
  /// </summary>
  /// <remarks>
  /// Usually you don't need to create an instance of this class. The designer can be called
  /// using the <see cref="FastReport.Report.Design()"/> method of 
  /// the <see cref="FastReport.Report"/> instance.
  /// <para/>This control represents pure designer surface + Objects toolbar. If you need
  /// standard menu, statusbar, toolbars and tool windows, use the 
  /// <see cref="FastReport.Design.StandardDesigner.DesignerControl"/> control instead. Also you may 
  /// decide to use a designer's form (<see cref="FastReport.Design.StandardDesigner.DesignerForm"/>)
  /// instead of a control.
  /// <para/>To run a designer, you need to attach a Report instance to it. This can be done via
  /// the <see cref="Report"/> property.
  /// <para/>To call the designer in MDI (Multi-Document Interface) mode, use the 
  /// <see cref="MdiMode"/> property.
  /// <para/>To set up some global properties, use the <see cref="Config"/> static class
  /// or <see cref="EnvironmentSettings"/> component that you can use in the Visual Studio IDE.
  /// </remarks>
  [ToolboxItem(false)]
  [Designer("FastReport.VSDesign.DesignerControlLayoutDesigner, FastReport.VSDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c, processorArchitecture=MSIL")]
  public partial class Designer : UserControl, ISupportInitialize
  {
    #region Fields
    private Report FReport;
    private ReportTab FActiveReportTab;
    private PluginCollection FPlugins;
    private List<string> FRecentFiles;
    private DesignerClipboard FClipboard;
    private bool FMdiMode;
    private List<DocumentWindow> FDocuments;
    private StartPageTab FStartPage;
    private ObjectCollection FObjects;
    private SelectedObjectCollection FSelectedObjects;
    private SelectedObjectCollection FPreviouslySelectedObjects;
    private SelectedComponents FSelectedComponents;
    private SelectedReportComponents FSelectedReportComponents;
    private SelectedTextObjects FSelectedTextObjects;
    private LastFormatting FLastFormatting;
    private bool FModified;
    private bool FFormatPainter;
    private ReportComponentBase FFormatPainterPattern;
    private DesignerRestrictions FRestrictions;
    private bool FIsPreviewPageDesigner;
    private bool FAskSave;
    private string FLayoutState;
    private bool FInitFlag;
    private bool FLayoutNeeded;
    private UIStyle FUIStyle;

    // docking
    private DotNetBarManager dotNetBarManager;
    private DockSite bottomDockSite;
    private DockSite leftDockSite;
    private DockSite rightDockSite;
    private DockSite topDockSite;
    private DockSite tbLeftDockSite;
    private DockSite tbRightDockSite;
    private DockSite tbTopDockSite;
    private DockSite tbBottomDockSite;
    private FastReport.DevComponents.DotNetBar.TabControl FTabs;

    // tools
    private ObjectsToolbar FObjectsToolbar;
    private DictionaryWindow FDataWindow;
    private PropertiesWindow FPropertiesWindow;
    private ReportTreeWindow FReportTreeWindow;
    private MessagesWindow FMessagesWindow;

    // commands
    private NewCommand FcmdNew;
    private NewPageCommand FcmdNewPage;
    private NewDialogCommand FcmdNewDialog;
    private OpenCommand FcmdOpen;
    private SaveCommand FcmdSave;
    private SaveAsCommand FcmdSaveAs;
    private SaveAllCommand FcmdSaveAll;
    private CloseCommand FcmdClose;
    private CloseAllCommand FcmdCloseAll;
    private PreviewCommand FcmdPreview;
    private PrinterSettingsCommand FcmdPrinterSetup;
    private PageSettingsCommand FcmdPageSetup;
    private AddDataCommand FcmdAddData;
    private ChooseDataCommand FcmdChooseData;
    private UndoCommand FcmdUndo;
    private RedoCommand FcmdRedo;
    private CutCommand FcmdCut;
    private CopyCommand FcmdCopy;
    private PasteCommand FcmdPaste;
    private FormatPainterCommand FcmdFormatPainter;
    private DeleteCommand FcmdDelete;
    private DeletePageCommand FcmdDeletePage;
    private SelectAllCommand FcmdSelectAll;
    private GroupCommand FcmdGroup;
    private UngroupCommand FcmdUngroup;
    private EditCommand FcmdEdit;
    private FindCommand FcmdFind;
    private ReplaceCommand FcmdReplace;
    private BringToFrontCommand FcmdBringToFront;
    private SendToBackCommand FcmdSendToBack;
    private InsertCommand FcmdInsert;
    private InsertBandCommand FcmdInsertBand;
    private RecentFilesCommand FcmdRecentFiles;
    private SelectLanguageCommand FcmdSelectLanguage;
    private ViewStartPageCommand FcmdViewStartPage;
    private ReportSettingsCommand FcmdReportSettings;
    private OptionsCommand FcmdOptions;
    private ReportStylesCommand FcmdReportStyles;
    private HelpContentsCommand FcmdHelpContents;
    private AboutCommand FcmdAbout;
    private WelcomeCommand FcmdWelcome;
    private Timer FAutoSaveTimer;
    #endregion

    #region Properties

    /// <summary>
    /// Occurs when designer's UI state changed.
    /// </summary>
    public event EventHandler UIStateChanged;

    /// <summary>
    /// Gets or sets the edited report.
    /// </summary>
    /// <remarks>
    /// To initialize the designer, you need to pass a Report instance to this property.
    /// This will create the designer's surface associated with the report.
    /// <code>
    /// Designer designer = new Designer();
    /// designer.Parent = form1;
    /// designer.Report = report1;
    /// </code>
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Report Report
    {
      get { return FReport; }
      set
      {
        FReport = value;
        if (FReport != null)
          FReport.Designer = this;
        InitTabs();
      }
    }

    /// <summary>
    /// Gets active report object. 
    /// </summary>
    /// <remarks>
    /// May be <b>null</b> if Start Page selected, or no reports opened.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Report ActiveReport
    {
      get { return FActiveReportTab == null ? null : FActiveReportTab.Report; }
      set
      {
        foreach (DocumentWindow c in Documents)
        {
          if (c is ReportTab && (c as ReportTab).Report == value)
          {
            c.Activate();
            return;
          }
        }
      }
    }

    /// <summary>
    /// Gets a collection of global plugins such as menu, properties window, etc. 
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PluginCollection Plugins
    {
      get { return FPlugins; }
    }

    /// <summary>
    /// Gets a collection of objects on the active page of the active report.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObjectCollection Objects
    {
      get { return FObjects; }
    }

    /// <summary>
    /// Gets a collection of selected objects on the active page of the active report.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectedObjectCollection SelectedObjects
    {
      get { return FSelectedObjects; }
    }

    /// <summary>
    /// Gets a collection of selected objects of the <b>ComponentBase</b> type.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectedComponents SelectedComponents
    {
      get { return FSelectedComponents; }
    }

    /// <summary>
    /// Gets a collection of selected objects of the <b>ReportComponentBase</b> type.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectedReportComponents SelectedReportComponents
    {
      get { return FSelectedReportComponents; }
    }

    /// <summary>
    /// Gets a collection of selected objects of the <b>TextObject</b> type.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectedTextObjects SelectedTextObjects
    {
      get { return FSelectedTextObjects; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the report was modified.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified
    {
      get { return FModified; }
      set { FModified = value; }
    }
    
    /// <summary>
    /// Gets or sets a value that determines whether to ask user to save changes when closing the designer.
    /// </summary>
    public bool AskSave
    {
      get { return FAskSave; }
      set { FAskSave = value; }
    }
    
    /// <summary>
    /// Gets the designer restrictions.
    /// </summary>
    public DesignerRestrictions Restrictions
    {
      get { return FRestrictions; }
    }

    /// <summary>
    /// Gets or sets a value indicating that designer is run in MDI mode.
    /// </summary>
    /// <remarks>
    /// <para/>To call the designer in MDI (Multi-Document Interface) mode, use the following code:
    /// <code>
    /// DesignerControl designer = new DesignerControl();
    /// designer.MdiMode = true;
    /// designer.ShowDialog();
    /// </code>
    /// </remarks>
    [DefaultValue(false)]
    public bool MdiMode
    {
      get { return FMdiMode; }
      set
      {
        FMdiMode = value;
        UpdateMdiMode();
      }
    }

    /// <summary>
    /// Gets or sets the visual style.
    /// </summary>
    [DefaultValue(UIStyle.VisualStudio2012Light)]
    public UIStyle UIStyle
    {
      get { return FUIStyle; }
      set
      {
        FUIStyle = value;
        UpdateUIStyle();
      }
    }

    /// <summary>
    /// Gets a value indicating that designer is used to edit a preview page.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPreviewPageDesigner
    {
      get { return FIsPreviewPageDesigner; }
      set { FIsPreviewPageDesigner = value; }
    }

    /// <summary>
    /// The "File|New" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public NewCommand cmdNew
    {
      get { return FcmdNew; }
    }

    /// <summary>
    /// The "New Page" toolbar command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public NewPageCommand cmdNewPage
    {
      get { return FcmdNewPage; }
    }

    /// <summary>
    /// The "New Dialog" toolbar command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public NewDialogCommand cmdNewDialog
    {
      get { return FcmdNewDialog; }
    }

    /// <summary>
    /// The "File|Open..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OpenCommand cmdOpen
    {
      get { return FcmdOpen; }
    }

    /// <summary>
    /// The "File|Save" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SaveCommand cmdSave
    {
      get { return FcmdSave; }
    }

    /// <summary>
    /// The "File|Save As..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SaveAsCommand cmdSaveAs
    {
      get { return FcmdSaveAs; }
    }

    /// <summary>
    /// The "File|Save All" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SaveAllCommand cmdSaveAll
    {
      get { return FcmdSaveAll; }
    }

    /// <summary>
    /// The "File|Close" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CloseCommand cmdClose
    {
      get { return FcmdClose; }
    }

    /// <summary>
    /// The "Window|Close All" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CloseAllCommand cmdCloseAll
    {
      get { return FcmdCloseAll; }
    }

    /// <summary>
    /// The "File|Preview..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PreviewCommand cmdPreview
    {
      get { return FcmdPreview; }
    }

    /// <summary>
    /// The "File|Printer Setup..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PrinterSettingsCommand cmdPrinterSetup
    {
      get { return FcmdPrinterSetup; }
    }

    /// <summary>
    /// The "File|Page Setup..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PageSettingsCommand cmdPageSetup
    {
      get { return FcmdPageSetup; }
    }

    /// <summary>
    /// The "Data|Add New Data Source..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AddDataCommand cmdAddData
    {
      get { return FcmdAddData; }
    }

    /// <summary>
    /// The "Data|Choose Report Data..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ChooseDataCommand cmdChooseData
    {
      get { return FcmdChooseData; }
    }

    /// <summary>
    /// The "Edit|Undo" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public UndoCommand cmdUndo
    {
      get { return FcmdUndo; }
    }

    /// <summary>
    /// The "Edit|Redo" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RedoCommand cmdRedo
    {
      get { return FcmdRedo; }
    }

    /// <summary>
    /// The "Edit|Cut" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CutCommand cmdCut
    {
      get { return FcmdCut; }
    }

    /// <summary>
    /// The "Edit|Copy" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CopyCommand cmdCopy
    {
      get { return FcmdCopy; }
    }

    /// <summary>
    /// The "Edit|Paste" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PasteCommand cmdPaste
    {
      get { return FcmdPaste; }
    }

    /// <summary>
    /// The "Format Painter" toolbar command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FormatPainterCommand cmdFormatPainter
    {
      get { return FcmdFormatPainter; }
    }

    /// <summary>
    /// The "Edit|Delete" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DeleteCommand cmdDelete
    {
      get { return FcmdDelete; }
    }

    /// <summary>
    /// The "Edit|Delete Page" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DeletePageCommand cmdDeletePage
    {
      get { return FcmdDeletePage; }
    }

    /// <summary>
    /// The "Edit|Select All" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectAllCommand cmdSelectAll
    {
      get { return FcmdSelectAll; }
    }

    /// <summary>
    /// The "Edit|Group" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GroupCommand cmdGroup
    {
      get { return FcmdGroup; }
    }

    /// <summary>
    /// The "Edit|Ungroup" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public UngroupCommand cmdUngroup
    {
      get { return FcmdUngroup; }
    }

    /// <summary>
    /// The "Edit" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EditCommand cmdEdit
    {
      get { return FcmdEdit; }
    }

    /// <summary>
    /// The "Edit|Find..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FindCommand cmdFind
    {
      get { return FcmdFind; }
    }

    /// <summary>
    /// The "Edit|Replace..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReplaceCommand cmdReplace
    {
      get { return FcmdReplace; }
    }

    /// <summary>
    /// The "Bring To Front" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BringToFrontCommand cmdBringToFront
    {
      get { return FcmdBringToFront; }
    }

    /// <summary>
    /// The "Send To Back" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SendToBackCommand cmdSendToBack
    {
      get { return FcmdSendToBack; }
    }

    /// <summary>
    /// The "Insert" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InsertCommand cmdInsert
    {
      get { return FcmdInsert; }
    }

    /// <summary>
    /// The "Insert Band" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InsertBandCommand cmdInsertBand
    {
      get { return FcmdInsertBand; }
    }

    /// <summary>
    /// The "Recent Files" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RecentFilesCommand cmdRecentFiles
    {
      get { return FcmdRecentFiles; }
    }

    /// <summary>
    /// The "File|Select Language..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectLanguageCommand cmdSelectLanguage
    {
      get { return FcmdSelectLanguage; }
    }

    /// <summary>
    /// The "View|Start Page" command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ViewStartPageCommand cmdViewStartPage
    {
      get { return FcmdViewStartPage; }
    }

    /// <summary>
    /// The "Report|Options..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReportSettingsCommand cmdReportSettings
    {
      get { return FcmdReportSettings; }
    }

    /// <summary>
    /// The "View|Options..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OptionsCommand cmdOptions
    {
      get { return FcmdOptions; }
    }

    /// <summary>
    /// The "Report|Styles..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReportStylesCommand cmdReportStyles
    {
      get { return FcmdReportStyles; }
    }

    /// <summary>
    /// The "Help|Help Contents..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public HelpContentsCommand cmdHelpContents
    {
      get { return FcmdHelpContents; }
    }

    /// <summary>
    /// The "Help|About..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AboutCommand cmdAbout
    {
      get { return FcmdAbout; }
    }

    /// <summary>
    /// The "Show welcome window..." command.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WelcomeCommand cmdWelcome
    {
      get { return FcmdWelcome; }
    }

    /// <summary>
    /// Gets or sets the layout state of the designer.
    /// </summary>
    /// <remarks>
    /// This property is used to store layout in Visual Studio design time. You may also use
    /// it to save and restore the designer's layout in your code. However, consider using the
    /// <see cref="SaveConfig()"/> and <see cref="RestoreConfig()"/> methods that use FastReport 
    /// configuration file.
    /// </remarks>
    [Browsable(false)]
    public string LayoutState
    {
      get
      {
        XmlDocument doc = new XmlDocument();
        doc.Root.Name = "Config";
        SaveDockState(doc.Root);
        using (MemoryStream stream = new MemoryStream())
        {
          doc.Save(stream);
          UTF8Encoding encoding = new UTF8Encoding();
          return encoding.GetString(stream.ToArray());
        }
      }
      set
      {
        FLayoutState = value;
        if (FInitFlag)
          FLayoutNeeded = true;
        else
          RestoreLayout(value);
      }
    }

    /// <summary>
    /// Fires when the layout is changed.
    /// </summary>
    /// <remarks>
    /// This event is for internal use only.
    /// </remarks>
    public event EventHandler LayoutChangedEvent;

    internal StartPageTab StartPage
    {
      get { return FStartPage; }
    }

    // active report tab. May be null if Start Page selected, or no reports opened
    internal ReportTab ActiveReportTab
    {
      get { return FActiveReportTab; }
      set
      {
        if (FActiveReportTab != value)
        {
          if (FActiveReportTab != null)
            FActiveReportTab.ReportDeactivated();
          FActiveReportTab = value;
          if (value != null)
            value.ReportActivated();
          else
            ClearSelection();
          UpdatePlugins(null);
        }
      }
    }

    // list of recent opened files
    internal List<string> RecentFiles
    {
      get { return FRecentFiles; }
    }

    internal List<DocumentWindow> Documents
    {
      get { return FDocuments; }
    }
    
    internal DesignerClipboard Clipboard
    {
      get { return FClipboard; }
    }

    internal DictionaryWindow DataWindow
    {
      get { return FDataWindow; }
    }
    
    internal PropertiesWindow PropertiesWindow
    {
      get { return FPropertiesWindow; }
    }
    
    internal ReportTreeWindow ReportTreeWindow
    {
      get { return FReportTreeWindow; }
    }

    internal MessagesWindow MessagesWindow
    {
      get { return FMessagesWindow; }
    }

    internal CodePageDesigner Editor
    {
      get { return ActiveReportTab.Editor; }
    }

    internal LastFormatting LastFormatting
    {
      get { return FLastFormatting; }
    }
    
    internal bool FormatPainter
    {
      get { return FFormatPainter; }
      set
      {
        if (FFormatPainter != value)
        {
          FFormatPainter = value;
          FFormatPainterPattern = value ? SelectedReportComponents.First : null;
          UpdatePlugins(null);
        }
      }
    }

    internal DotNetBarManager DotNetBarManager
    {
      get { return dotNetBarManager; }
    }

    internal bool IsVSDesignMode
    {
      get { return DesignMode; }
    }

    internal Timer AutoSaveTimer
    {
        get { return FAutoSaveTimer; }
    }
    #endregion

    #region Private Methods
    private void SetupAutoSave()
    {
        FAutoSaveTimer = new Timer();
        int minutes = 0;
        if (!int.TryParse(Config.Root.FindItem("Designer").FindItem("Saving").GetProp("AutoSaveMinutes"), out minutes))
            minutes = 5;
        FAutoSaveTimer.Interval = minutes * 60000;
        FAutoSaveTimer.Tick += delegate(object s1, EventArgs e1)
        {
            if (this != null && !IsPreviewPageDesigner)
            {
                if (cmdSave.Enabled)
                {
                    ActiveReportTab.AutoSaveFile();
                }
                else
                {
                    if (File.Exists(Config.AutoSaveFileName))
                        File.Delete(Config.AutoSaveFileName);
                    if (File.Exists(Config.AutoSaveFile))
                        File.Delete(Config.AutoSaveFile);
                }
            }
        };
    }

    private void UpdateUIStyle()
    {
        switch (UIStyle)
        {
          case UIStyle.Office2003:
          case UIStyle.Office2007Blue:
          case UIStyle.Office2010Blue:
            StyleManager.ChangeStyle(eStyle.Office2010Blue, Color.Empty);
            break;
          case UIStyle.Office2007Silver:
          case UIStyle.Office2010Silver:
            StyleManager.ChangeStyle(eStyle.Office2010Silver, Color.Empty);
            break;
          case UIStyle.Office2007Black:
          case UIStyle.Office2010Black:
            StyleManager.ChangeStyle(eStyle.Office2010Black, Color.Empty);
            break;
          case UIStyle.Office2013:
            StyleManager.ChangeStyle(eStyle.Office2013, Color.Empty);
            break;
          case UIStyle.VisualStudio2005:
          case UIStyle.VisualStudio2010:
            StyleManager.ChangeStyle(eStyle.VisualStudio2010Blue, Color.Empty);
            break;
          case UIStyle.VisualStudio2012Light:
            StyleManager.ChangeStyle(eStyle.VisualStudio2012Light, Color.Empty);
            break;
          case UIStyle.VistaGlass:
            StyleManager.ChangeStyle(eStyle.Windows7Blue, Color.Empty);
            break;
        }

      //UIStyleUtils.UpdateUIStyle();

      dotNetBarManager.Style = UIStyleUtils.GetDotNetBarStyle(UIStyle);

      foreach (DocumentWindow document in Documents)
      {
        if (document is ReportTab)
          (document as ReportTab).UpdateUIStyle();
      }
      FPlugins.UpdateUIStyle();
    }

    // init global designer plugins
    private void InitPluginsInternal()
    {
      SuspendLayout();
      InitPlugins();
      
      foreach (Type pluginType in DesignerPlugins.Plugins) 
      {
        IDesignerPlugin plugin = FPlugins.Add(pluginType);
        if (plugin is ToolWindowBase)
          (plugin as ToolWindowBase).DoDefaultDock();
      }
      
      ResumeLayout();
    }

    private void UpdateMdiMode()
    {
      FTabs.TabsVisible = FMdiMode;
      if (FMdiMode)
        AddStartPageTab();
      else if (StartPage != null)
        StartPage.Close();
    }

    // create initial pages (Start Page, default report)
    private void InitTabs()
    {
      if (FMdiMode)
        AddStartPageTab();
      if (FReport != null)
        AddReportTab(CreateReportTab(FReport));
    }

    private void FTabs_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
    {
      if (e.NewTab is DocumentWindow)
      {
        if (e.NewTab is StartPageTab)
          ActiveReportTab = null;
        else
          ActiveReportTab = e.NewTab as ReportTab;
      }
    }

    private void FTabs_TabItemClose(object sender, TabStripActionEventArgs e)
    {
      ReportTab tab = FTabs.SelectedTab as ReportTab;
      if (!CloseDocument(tab))
        return;

      if (Documents.Count > 0)
        Documents[Documents.Count - 1].Activate();
      else
        ClearSelection();
    }

    private void AddDocument(DocumentWindow doc)
    {
      Documents.Add(doc);
      doc.AddToTabControl(FTabs);
      doc.Activate();
    }
    
    private void RemoveDocument(DocumentWindow doc)
    {
      if (doc is StartPageTab)
        FStartPage = null;
      Documents.Remove(doc);
      doc.Close();
    }

    internal bool CloseDocument(DocumentWindow doc)
    {
      if (doc is ReportTab)
      {
        if (!(doc as ReportTab).CanClose())
          return false;
      }

      RemoveDocument(doc);
      return true;
    }
    
    private void ClearSelection()
    {
      FActiveReportTab = null;
      Objects.Clear();
      SelectedObjects.Clear();
      SelectionChanged(null);
      UpdatePlugins(null);
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Initializes designer plugins such as toolbars and toolwindows.
    /// </summary>
    protected virtual void InitPlugins()
    {
      FObjectsToolbar = new ObjectsToolbar(this);
      FDataWindow = new DictionaryWindow(this);
      FPropertiesWindow = new PropertiesWindow(this);
      FReportTreeWindow = new ReportTreeWindow(this);
      FMessagesWindow = new MessagesWindow(this);

      // make default layout
      FDataWindow.DockTo(rightDockSite);
      FReportTreeWindow.DockTo(FDataWindow);
      FPropertiesWindow.DockTo(rightDockSite, FDataWindow, eDockSide.Bottom);
      FMessagesWindow.DockTo(bottomDockSite);
      FMessagesWindow.Bar.AutoHide = true;
      FMessagesWindow.Hide();
      rightDockSite.Width = 250;

      leftDockSite.MouseUp += new MouseEventHandler(dockSite_MouseUp);
      rightDockSite.MouseUp += new MouseEventHandler(dockSite_MouseUp);
      topDockSite.MouseUp += new MouseEventHandler(dockSite_MouseUp);
      bottomDockSite.MouseUp += new MouseEventHandler(dockSite_MouseUp);
      dotNetBarManager.AutoHideChanged += new EventHandler(LayoutChanged);
      dotNetBarManager.BarDock += new EventHandler(LayoutChanged);

      FDataWindow.VisibleChanged += new EventHandler(LayoutChanged);
      FPropertiesWindow.VisibleChanged += new EventHandler(LayoutChanged);
      FReportTreeWindow.VisibleChanged += new EventHandler(LayoutChanged);
      FMessagesWindow.VisibleChanged += new EventHandler(LayoutChanged);

      FPlugins.AddRange(new IDesignerPlugin[] {
        FObjectsToolbar, FDataWindow, FPropertiesWindow, 
        FReportTreeWindow, FMessagesWindow });

      // add export plugins
      FPlugins.Add(typeof(ExportPlugins.FR3.FR3ExportPlugin));
      FPlugins.Add(typeof(ExportPlugins.RDL.RDLExportPlugin));

      // add import plugins
      FPlugins.Add(typeof(ImportPlugins.RDL.RDLImportPlugin));
      FPlugins.Add(typeof(ImportPlugins.ListAndLabel.ListAndLabelImportPlugin));
      FPlugins.Add(typeof(ImportPlugins.DevExpress.DevExpressImportPlugin));
    }

    /// <inheritdoc/>
    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      dotNetBarManager.ParentForm = FindForm();

      if (DesignMode)
        DisableFloatingBars();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// AutoSave system initialization.
    /// </summary>
    public void StartAutoSave()
    {
        if (IsPreviewPageDesigner)
            return;

        if (File.Exists(Config.AutoSaveFile) &&
            File.Exists(Config.AutoSaveFileName) &&
            Config.Root.FindItem("Designer").FindItem("Saving").GetProp("EnableAutoSave") != "0")
        {
            string filepath = File.ReadAllText(Config.AutoSaveFileName);

            using (LoadAutoSaveForm f = new LoadAutoSaveForm(filepath))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ActiveReportTab.LoadAutoSaveFile(filepath);
                    SetModified();
                }
            }
        }

        if (File.Exists(Config.AutoSaveFileName))
            File.Delete(Config.AutoSaveFileName);
        if (File.Exists(Config.AutoSaveFile))
            File.Delete(Config.AutoSaveFile);

        FAutoSaveTimer.Enabled = Config.Root.FindItem("Designer").FindItem("Saving").GetProp("EnableAutoSave") != "0";
    }

    /// <summary>
    /// Stops the AutoSave system.
    /// </summary>
    public void StopAutoSave()
    {
        if (IsPreviewPageDesigner)
            return;

        FAutoSaveTimer.Stop();

        if (File.Exists(Config.AutoSaveFileName))
            File.Delete(Config.AutoSaveFileName);
        if (File.Exists(Config.AutoSaveFile))
            File.Delete(Config.AutoSaveFile);
    }

    /// <summary>
    /// Call this method if you change something in the report. 
    /// </summary>
    /// <remarks>
    /// This method adds the current report state to the undo buffer and updates all plugins.
    /// </remarks>
    public void SetModified()
    {
      SetModified(null, "Change");
    }

    /// <summary>
    /// Call this method if you change something in the report. 
    /// </summary>
    /// <param name="sender">The object that was modified.</param>
    /// <param name="action">The undo action name.</param>
    /// <remarks>
    /// This method adds the current report state to the undo buffer and updates all plugins.
    /// </remarks>
    public void SetModified(object sender, string action)
    {
      SetModified(sender, action, null);
    }

    /// <summary>
    /// Call this method if you change something in the report.
    /// </summary>
    /// <param name="sender">The object that was modified.</param>
    /// <param name="action">The undo action name.</param>
    /// <param name="objName">The name of modified object.</param>
    public void SetModified(object sender, string action, string objName)
    {
      FModified = true;
      if (ActiveReportTab != null)
        ActiveReportTab.SetModified(sender, action, objName);
      UpdatePlugins(sender);
    }

    /// <summary>
    /// Call this method to tell the designer that current selection is changed.
    /// </summary>
    /// <param name="sender">The plugin that changes the selection (may be <b>null</b>).</param>
    public void SelectionChanged(object sender)
    {
      // check groups
      ObjectCollection selectedObjects = new ObjectCollection();
      SelectedObjects.CopyTo(selectedObjects);
      foreach (Base c in selectedObjects)
      {
        if (c is ComponentBase && (c as ComponentBase).GroupIndex != 0)
        {
          int groupIndex = (c as ComponentBase).GroupIndex;
          foreach (Base c1 in Objects)
          {
            if (c1 is ComponentBase && (c1 as ComponentBase).GroupIndex == groupIndex)
            {
              if (SelectedObjects.IndexOf(c1) == -1)
                SelectedObjects.Add(c1);
            }
          }
        }  
      }

      foreach (Base c in FPreviouslySelectedObjects)
      {
        c.SelectionChanged();
      }

      if (FFormatPainter && FFormatPainterPattern != null)
      {
        foreach (Base c in selectedObjects)
        {
          c.SelectionChanged();
          if (c is ReportComponentBase)
            (c as ReportComponentBase).AssignFormat(FFormatPainterPattern);
        }
      }
      
      SelectedComponents.Update();
      SelectedReportComponents.Update();
      SelectedTextObjects.Update();
      if (ActiveReportTab != null)
        ActiveReportTab.Plugins.SelectionChanged(sender);
      FPlugins.SelectionChanged(sender);
      
      SelectedObjects.CopyTo(FPreviouslySelectedObjects);  

      if (FFormatPainter && FFormatPainterPattern != null)
        SetModified();
      OnUIStateChanged();
    }

    /// <summary>
    /// Locks all plugins. 
    /// </summary>
    /// <remarks>
    /// This method is usually called when we destroy the report to prevent unexpected
    /// errors - such as trying to draw destroyed objects.
    /// </remarks>
    public void Lock()
    {
      if (ActiveReportTab != null)
        ActiveReportTab.Plugins.Lock();
      FPlugins.Lock();
    }

    /// <summary>
    /// Unlocks all plugins. 
    /// </summary>
    /// <remarks>
    /// Call this method after the <b>Lock</b>.
    /// </remarks>
    public void Unlock()
    {
      if (ActiveReportTab != null)
        ActiveReportTab.Plugins.Unlock();
      FPlugins.Unlock();
    }

    /// <summary>
    /// Call this method to refresh all plugins' content.
    /// </summary>
    /// <param name="sender">The plugin that we don't need to refresh.</param>
    public void UpdatePlugins(object sender)
    {
      if (ActiveReportTab != null)
        ActiveReportTab.Plugins.Update(sender);
      FPlugins.Update(sender);
      OnUIStateChanged();
    }
    
    // adds the Start Page tab
    internal void AddStartPageTab()
    {
      if (StartPage != null)
        return;
        
      FStartPage = new StartPageTab(this);
      AddDocument(FStartPage);
      UpdatePlugins(null);
    }

    // adds the report tab
    internal ReportTab CreateReportTab(Report report)
    {
      return new ReportTab(this, report);
    }

    internal void AddReportTab(ReportTab tab)
    {
      AddDocument(tab);
      tab.UpdateUIStyle();
      ActiveReportTab = tab;
    }

    private void OnUIStateChanged()
    {
      if (UIStateChanged != null)
        UIStateChanged(this, EventArgs.Empty);
    }
    #endregion

    #region Layout
    private void CreateLayout()
    {
      FTabs = new FastReport.DevComponents.DotNetBar.TabControl();
      FTabs.Dock = DockStyle.Fill;
      FTabs.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
      FTabs.AutoHideSystemBox = false;
      FTabs.CloseButtonOnTabsVisible = true;
      FTabs.SelectedTabChanged += new TabStrip.SelectedTabChangedEventHandler(FTabs_SelectedTabChanged);
      FTabs.TabItemClose += new TabStrip.UserActionEventHandler(FTabs_TabItemClose);

      Controls.Add(FTabs);

      dotNetBarManager = new DotNetBarManager(this.components);
      leftDockSite = new DockSite();
      rightDockSite = new DockSite();
      topDockSite = new DockSite();
      bottomDockSite = new DockSite();
      tbLeftDockSite = new DockSite();
      tbRightDockSite = new DockSite();
      tbTopDockSite = new DockSite();
      tbBottomDockSite = new DockSite();
      // 
      // dotNetBarManager
      // 
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.F1);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlC);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlA);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlV);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlX);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlZ);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlY);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.Del);
      dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.Ins);

      dotNetBarManager.DefinitionName = "";
      dotNetBarManager.EnableFullSizeDock = false;
      dotNetBarManager.ShowCustomizeContextMenu = false;

      dotNetBarManager.LeftDockSite = leftDockSite;
      dotNetBarManager.RightDockSite = rightDockSite;
      dotNetBarManager.TopDockSite = topDockSite;
      dotNetBarManager.BottomDockSite = bottomDockSite;
      dotNetBarManager.ToolbarLeftDockSite = tbLeftDockSite;
      dotNetBarManager.ToolbarRightDockSite = tbRightDockSite;
      dotNetBarManager.ToolbarTopDockSite = tbTopDockSite;
      dotNetBarManager.ToolbarBottomDockSite = tbBottomDockSite;
      // 
      // leftDockSite
      // 
      leftDockSite.Dock = DockStyle.Left;
      leftDockSite.DocumentDockContainer = new DocumentDockContainer();
      leftDockSite.Name = "leftDockSite";
      // 
      // rightDockSite
      // 
      rightDockSite.Dock = DockStyle.Right;
      rightDockSite.DocumentDockContainer = new DocumentDockContainer();
      rightDockSite.Name = "rightDockSite";
      // 
      // topDockSite
      // 
      topDockSite.Dock = DockStyle.Top;
      topDockSite.DocumentDockContainer = new DocumentDockContainer();
      topDockSite.Name = "topDockSite";
      // 
      // bottomDockSite
      // 
      bottomDockSite.Dock = DockStyle.Bottom;
      bottomDockSite.DocumentDockContainer = new DocumentDockContainer();
      bottomDockSite.Name = "bottomDockSite";
      // 
      // tbLeftDockSite
      // 
      tbLeftDockSite.Dock = DockStyle.Left;
      tbLeftDockSite.Name = "tbLeftDockSite";
      // 
      // tbRightDockSite
      // 
      tbRightDockSite.Dock = DockStyle.Right;
      tbRightDockSite.Name = "tbRightDockSite";
      // 
      // tbTopDockSite
      // 
      tbTopDockSite.Dock = DockStyle.Top;
      tbTopDockSite.Name = "tbTopDockSite";
      // 
      // tbBottomDockSite
      // 
      tbBottomDockSite.Dock = DockStyle.Bottom;
      tbBottomDockSite.Name = "tbBottomDockSite";

      Controls.Add(leftDockSite);
      Controls.Add(rightDockSite);
      Controls.Add(topDockSite);
      Controls.Add(bottomDockSite);
      Controls.Add(tbLeftDockSite);
      Controls.Add(tbRightDockSite);
      Controls.Add(tbTopDockSite);
      Controls.Add(tbBottomDockSite);
    }

    private void SaveDockState(XmlItem root)
    {
      XmlItem xi = root.FindItem("Designer").FindItem("DockRibbon");
      xi.SetProp("Text", dotNetBarManager.LayoutDefinition);
    }

    private void RestoreDockState(XmlItem root)
    {
        XmlItem xi = root.FindItem("Designer").FindItem("DockRibbon");
        string s = xi.GetProp("Text");
        if (s != "")
        {
            // clear toolbar's DockLine property to restore position correctly
            foreach (Bar bar in dotNetBarManager.Bars)
            {
                if (bar.BarType == eBarType.Toolbar)
                    bar.DockLine = 0;
            }

            dotNetBarManager.LayoutDefinition = s;
        }  
    }

    private void SaveRecentFiles()
    {
      XmlItem xi = Config.Root.FindItem("Designer");
      string files = "";
      foreach (string s in RecentFiles)
      {
        files += s + '\r';
      }
      xi.SetProp("RecentFiles", files);
    }

    private void RestoreRecentFiles()
    {
      XmlItem xi = Config.Root.FindItem("Designer");
      string[] files = xi.GetProp("RecentFiles").Split(new char[] { '\r' });
      RecentFiles.Clear();
      foreach (string s in files)
      {
        if (!String.IsNullOrEmpty(s))
          RecentFiles.Add(s);
      }
    }

    /// <summary>
    /// Saves config to a FastReport configuration file.
    /// </summary>
    public void SaveConfig()
    {
      SaveRecentFiles();
      FPlugins.SaveState();
      SaveDockState(Config.Root);
    }

    /// <summary>
    /// Restores config from a FastReport configuration file.
    /// </summary>
    /// <remarks>
    /// Call this method to restore the designer's layout. You need to do this after the
    /// designer's control is placed on a form.
    /// </remarks>
    public void RestoreConfig()
    {
      SuspendLayout();

      RestoreRecentFiles();
      RestoreDockState(Config.Root);
      FPlugins.RestoreState();

      ResumeLayout();
    }

    /// <summary>
    /// Refresh the designer's toolbars and toolwindows layout.
    /// </summary>
    /// <remarks>
    /// Call this method if you use 
    /// <see cref="FastReport.Design.StandardDesigner.DesignerControl">DesignerControl</see>. To restore
    /// the layout that you've created in VS design time, you need to call this method in the form's
    /// <b>Load</b> event handler. If you don't do this, tool windows like Properties, Data, Report Tree
    /// will not be available.
    /// </remarks>
    public void RefreshLayout()
    {
      RestoreLayout(FLayoutState);
    }

    private void RestoreLayout(string value)
    {
      XmlDocument doc = new XmlDocument();
      if (value == null)
        value = "";
      int startIndex = value.IndexOf("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
      if (startIndex != -1)
      {
        UTF8Encoding encoding = new UTF8Encoding();
        using (MemoryStream stream = new MemoryStream(encoding.GetBytes(value.Substring(startIndex))))
        {
          doc.Load(stream);
        }
      }

      SuspendLayout();
      RestoreDockState(doc.Root);
      ResumeLayout();
    }

    /// <inheritdoc/>
    public void BeginInit()
    {
      FInitFlag = true;
    }

    /// <inheritdoc/>
    public void EndInit()
    {
      if (FInitFlag)
      {
        if (FLayoutNeeded)
          RestoreLayout(FLayoutState);
        FInitFlag = false;
        FLayoutNeeded = false;

        if (DesignMode)
          DisableFloatingBars();
      }  
    }
    
    private void DisableFloatingBars()
    {
      foreach (Bar bar in dotNetBarManager.Bars)
      {
        bar.CanUndock = false;
      }
    }

    private void dockSite_MouseUp(object sender, MouseEventArgs e)
    {
      LayoutChanged(sender, EventArgs.Empty);
    }
    
    internal void LayoutChanged(object sender, EventArgs e)
    {
      if (DesignMode && !FInitFlag)
      {
        if (LayoutChangedEvent != null)
          LayoutChangedEvent(this, EventArgs.Empty);
      }
    }
    #endregion

    #region Commands
	// Simon: InitCommands ¸ÄÎª InitCommands_inner
	private void InitCommands_inner()
    {
      FcmdNew = new NewCommand(this);
      FcmdNewPage = new NewPageCommand(this);
      FcmdNewDialog = new NewDialogCommand(this);
      FcmdOpen = new OpenCommand(this);
      FcmdSave = new SaveCommand(this);
      FcmdSaveAs = new SaveAsCommand(this);
      FcmdSaveAll = new SaveAllCommand(this);
      FcmdClose = new CloseCommand(this);
      FcmdCloseAll = new CloseAllCommand(this);
      FcmdPreview = new PreviewCommand(this);
      FcmdPrinterSetup = new PrinterSettingsCommand(this);
      FcmdPageSetup = new PageSettingsCommand(this);
      FcmdAddData = new AddDataCommand(this);
      FcmdChooseData = new ChooseDataCommand(this);
      FcmdUndo = new UndoCommand(this);
      FcmdRedo = new RedoCommand(this);
      FcmdCut = new CutCommand(this);
      FcmdCopy = new CopyCommand(this);
      FcmdPaste = new PasteCommand(this);
      FcmdFormatPainter = new FormatPainterCommand(this);
      FcmdDelete = new DeleteCommand(this);
      FcmdDeletePage = new DeletePageCommand(this);
      FcmdSelectAll = new SelectAllCommand(this);
      FcmdGroup = new GroupCommand(this);
      FcmdUngroup = new UngroupCommand(this);
      FcmdEdit = new EditCommand(this);
      FcmdFind = new FindCommand(this);
      FcmdReplace = new ReplaceCommand(this);
      FcmdBringToFront = new BringToFrontCommand(this);
      FcmdSendToBack = new SendToBackCommand(this);
      FcmdInsert = new InsertCommand(this);
      FcmdInsertBand = new InsertBandCommand(this);
      FcmdRecentFiles = new RecentFilesCommand(this);
      FcmdSelectLanguage = new SelectLanguageCommand(this);
      FcmdViewStartPage = new ViewStartPageCommand(this);
      FcmdReportSettings = new ReportSettingsCommand(this);
      FcmdOptions = new OptionsCommand(this);
      FcmdReportStyles = new ReportStylesCommand(this);
      FcmdHelpContents = new HelpContentsCommand(this);
      FcmdAbout = new AboutCommand(this);
      FcmdWelcome = new WelcomeCommand(this);
    }

    internal void InitPages(int pageNo)
    {
      ActiveReportTab.InitPages(pageNo);
    }

    /// <summary>
    /// Initializes the workspace after the new report is loaded.
    /// </summary>
    public void InitReport()
    {
      ActiveReportTab.InitReport();
      ActiveReportTab.SetModified();
    }

    internal void InsertObject(ObjectInfo[] infos, InsertFrom source)
    {
      ObjectCollection list = new ObjectCollection();
      ActiveReportTab.ActivePageDesigner.CancelPaste();
      SelectedObjects.Clear();

      foreach (ObjectInfo info in infos)
      {
        Base c = Activator.CreateInstance(info.Object) as Base;
        LastFormatting.SetFormatting(c as ReportComponentBase);
        c.OnBeforeInsert(info.Flags);

        list.Add(c);
        c.Parent = ActiveReportTab.ActivePageDesigner.GetParentForPastedObjects();
        c.CreateUniqueName();

        Config.DesignerSettings.OnObjectInserted(this, new ObjectInsertedEventArgs(c, source));
        SelectedObjects.Add(c);
        Objects.Add(c);
      }

      ActiveReportTab.ActivePageDesigner.Paste(list, source);
    }

    internal void InsertObject(ObjectInfo info, InsertFrom source)
    {
      InsertObject(new ObjectInfo[] { info }, source);
    }

    internal void ResetObjectsToolbar(bool ignoreMultiInsert)
    {
      FObjectsToolbar.ClickSelectButton(ignoreMultiInsert);
      FormatPainter = false;
    }

    internal void Exit(object sender, EventArgs e)
    {
      Form form = FindForm();
      if (form != null)
        form.Close();
    }

    /// <summary>
    /// Tries to create a new empty report.
    /// </summary>
    /// <returns><b>true</b> if report was created successfully; <b>false</b> if user cancels the action.</returns>
    public bool CreateEmptyReport()
    {
      bool result = false;
      if (MdiMode)
      {
        Report report = new Report();
        report.Designer = this;
        Config.DesignerSettings.OnReportLoaded(this, new ReportLoadedEventArgs(report));
        AddReportTab(CreateReportTab(report));
        result = true;
      }
      else
        result = ActiveReportTab.EmptyReport(true);

      if (result)
      {
        ActiveReportTab.SetModified();
        UpdatePlugins(null);
      }

      return result;
    }

    internal void ErrorMsg(string msg, string objName)
    {
      MessagesWindow.AddMessage(msg, objName);
    }

    internal void ErrorMsg(string msg, int line, int column)
    {
      MessagesWindow.AddMessage(msg, line, column);
    }

    /// <summary>
    /// Displays a message in the "Messages" window.
    /// </summary>
    /// <param name="msg">Message text.</param>
    public void ShowMessage(string msg)
    {
      MessagesWindow.AddMessage(msg, 1, 1);
    }

    /// <summary>
    /// Clears the "Messages" window.
    /// </summary>
    public void ClearMessages()
    {
      MessagesWindow.ClearMessages();
    }
    
    /// <summary>
    /// Shows the selected object's information in the designer's statusbar.
    /// </summary>
    /// <param name="location">Object's location.</param>
    /// <param name="size">Object's size.</param>
    /// <param name="text">Textual information about the selected object.</param>
    public virtual void ShowStatus(string location, string size, string text)
    {
    }

    /// <summary>
    /// Close all opened reports, ask to save changes.
    /// </summary>
    /// <returns><b>true</b> if all tabs closed succesfully.</returns>
    /// <remarks>
    /// Use this method to close all opened documents and save changes when you closing the main form
    /// that contains the designer control. To do this, create an event handler for your form's FormClosing 
    /// event and call this method inside the handler. If it returns false, set e.Cancel to <b>true</b>.
    /// </remarks>
    public bool CloseAll()
    {
      // close all tabs from the last tab to first.
      while (Documents.Count > 0)
      {
        DocumentWindow c = Documents[Documents.Count - 1];
        if (!CloseDocument(c))
        {
          // tab cannot be closed. Activate it and exit. Do not allow to close the designer.
          c.Activate();
          return false;
        }
      }
      
      return true;
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="Designer"/> class with default settings.
    /// </summary>
    public 
    Designer()
    {
      Report.EnsureInit();
      BarUtilities.UseTextRenderer = true;
      
      FPlugins = new PluginCollection(this);
      FRecentFiles = new List<string>();
      FClipboard = new DesignerClipboard(this);
      FDocuments = new List<DocumentWindow>();
      FObjects = new ObjectCollection();
      FSelectedObjects = new SelectedObjectCollection();
      FPreviouslySelectedObjects = new SelectedObjectCollection();
      FSelectedComponents = new SelectedComponents(this);
      FSelectedReportComponents = new SelectedReportComponents(this);
      FSelectedTextObjects = new SelectedTextObjects(this);
      FRestrictions = Config.DesignerSettings.Restrictions.Clone();
      FAskSave = true;
      FLastFormatting = new LastFormatting();

      InitCommands();
      CreateLayout();
      InitPluginsInternal();
      UpdatePlugins(null);
      UpdateMdiMode();
      UIStyle = Config.UIStyle;
      SetupAutoSave();

      // Right to Left layout
      RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      FObjectsToolbar.DockSide = Config.RightToLeft ? eDockSide.Right : eDockSide.Left;
      //SaveConfig();
    }
  }
}
