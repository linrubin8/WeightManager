using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;
using System.IO;
using FastReport.Forms;

namespace FastReport.Design.StandardDesigner
{
  /// <summary>
  /// Represents the standard report designer.
  /// </summary>
  /// <remarks>
  /// This control extends the <see cref="FastReport.Design.Designer"/> control with 
  /// standard menu, status bar, and toolbars.
  /// <para/>To choose toolbars and tool windows in design-time, click the "View" menu
  /// in this control and select what you want to see. Toolbars can be reordered using the mouse.
  /// <para/>To restore the designer layout at runtime, you need to call the 
  /// <see cref="FastReport.Design.Designer.RefreshLayout">RefreshLayout</see> method in your
  /// form's <b>Load</b> event handler. 
  /// </remarks>
  [ToolboxItem(true), ToolboxBitmap(typeof(Report), "Resources.DesignerControl.bmp")]
  public partial class DesignerControl : Designer
  {
    #region Fields
    private DesignerMenu FMainMenu;
    private DesignerStatusBar FStatusBar;
    private StandardToolbar FStandardToolbar;
    private TextToolbar FTextToolbar;
    private BorderToolbar FBorderToolbar;
    private LayoutToolbar FLayoutToolbar;
    private StyleToolbar FStyleToolbar;
    private ContextMenuBar mnuContext;
    private ButtonItem mnuContextRoot;
    private PolygonToolbar FPolygonToolBar;
    
    private bool FShowMainMenu;
    private bool FShowStatusBar;

    private bool useNewStatusBar;
    private LabelItem location;
    private LabelItem size;
    private LabelItem text;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the main menu.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DesignerMenu MainMenu
    {
      get { return FMainMenu; }
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the main menu should be displayed or not.
    /// </summary>
    [SRCategory("Toolbars")]
    [DefaultValue(true)]
    public bool ShowMainMenu
    {
      get { return FShowMainMenu; }
      set
      {
        FShowMainMenu = value;
        FMainMenu.Visible = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the status bar should be displayed or not.
    /// </summary>
    [SRCategory("Toolbars")]
    [DefaultValue(true)]
    public bool ShowStatusBar
    {
      get { return FShowStatusBar; }
      set
      {
        FShowStatusBar = value;
        FStatusBar.Visible = value;
      }
    }
    #endregion
    
    #region Private Methods
    private void CreateToolbarMenu()
    {
      mnuContext = new ContextMenuBar();
      mnuContextRoot = new ButtonItem();
      mnuContext.Items.Add(mnuContextRoot);

      foreach (IDesignerPlugin plugin in Plugins)
      {
        if (plugin is ToolbarBase)
        {
          ButtonItem menuItem = new ButtonItem();
          menuItem.Text = (plugin as ToolbarBase).Text;
          menuItem.Tag = plugin;
          menuItem.Click += new EventHandler(toolbar_Click);
          mnuContextRoot.SubItems.Add(menuItem);
        }
      }

      mnuContextRoot.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(mnuContextRoot_PopupOpen);
      mnuContext.SetContextMenuEx(DotNetBarManager.ToolbarTopDockSite, mnuContextRoot);
    }

    private void mnuContextRoot_PopupOpen(object sender, PopupOpenEventArgs e)
    {
      mnuContext.Style = DotNetBarManager.Style;
      foreach (BaseItem item in mnuContextRoot.SubItems)
      {
        (item as ButtonItem).Checked = (item.Tag as ToolbarBase).Visible;
      }
    }

    private void toolbar_Click(object sender, EventArgs e)
    {
      ToolbarBase toolbar = (sender as ButtonItem).Tag as ToolbarBase;
      toolbar.Visible = !toolbar.Visible;
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void InitPlugins()
    {
      base.InitPlugins();
      FMainMenu = new DesignerMenu(this);
      FStatusBar = new DesignerStatusBar(this);

      FStandardToolbar = new StandardToolbar(this);
      FTextToolbar = new TextToolbar(this);
      FBorderToolbar = new BorderToolbar(this);
      FLayoutToolbar = new LayoutToolbar(this);
      FStyleToolbar = new StyleToolbar(this);
      FPolygonToolBar = new PolygonToolbar(this);

      //FStandardToolbar.EndDrag += new EventHandler(LayoutChanged);
      //FTextToolbar.EndDrag += new EventHandler(LayoutChanged);
      //FBorderToolbar.EndDrag += new EventHandler(LayoutChanged);
      //FLayoutToolbar.EndDrag += new EventHandler(LayoutChanged);
      //FStyleToolbar.EndDrag += new EventHandler(LayoutChanged);

      // set toolbars position
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FStandardToolbar);
      FTextToolbar.DockLine = 1;
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FTextToolbar);
      FBorderToolbar.DockLine = 1;
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FBorderToolbar);
      FLayoutToolbar.DockLine = 2;
      FLayoutToolbar.Hide();
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FLayoutToolbar);
      FStyleToolbar.DockLine = 2;
      FStyleToolbar.Hide();
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FStyleToolbar);
      FPolygonToolBar.DockLine = 2;
      DotNetBarManager.ToolbarTopDockSite.Controls.Add(FPolygonToolBar);
      
      Plugins.AddRange(new IDesignerPlugin[] {
        FMainMenu, FStatusBar, FStandardToolbar, FTextToolbar, FBorderToolbar,
        FLayoutToolbar, FStyleToolbar, FPolygonToolBar });

      CreateToolbarMenu();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void ShowStatus(string location, string size, string text)
    {
        if (!useNewStatusBar)
        {
            FStatusBar.UpdateLocationAndSize(location, size);
            FStatusBar.UpdateText(text);
        }
        else
        {
            this.location.Visible = !String.IsNullOrEmpty(location);
            this.location.Text = location;
            this.size.Visible = !String.IsNullOrEmpty(size);
            this.size.Text = size;

            SelectedObjectCollection selection = SelectedObjects;

            string txt = selection.Count == 0 ? "" : selection.Count > 1 ?
              String.Format(Res.Get("Designer,ToolWindow,Properties,NObjectsSelected"), selection.Count) :
              selection[0].Name;

            if (!String.IsNullOrEmpty(text))
                txt += ":  " + text.Replace('\r', ' ').Replace('\n', ' ');

            this.text.Text = txt;
        }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignerControl"/> class with default settings.
    /// </summary>
    public DesignerControl()
    {
      InitializeComponent();

      ShowMainMenu = true;
      ShowStatusBar = true;
      useNewStatusBar = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignerControl"/> class for compatibility with new statusbar and using with ribbon UI.
    /// </summary>
    public DesignerControl(LabelItem location, LabelItem size, LabelItem text)
    {
        InitializeComponent();

        ShowMainMenu = true;
        ShowStatusBar = false;
        useNewStatusBar = true;

        this.location = location;
        this.size = size;
        this.text = text;
    }
  }
}

