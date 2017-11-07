using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.ToolWindows
{
  /// <summary>
  /// Base class for all tool windows such as "Properties", "Data Dictionary" etc.
  /// </summary>
  /// <remarks>
  /// <para>Use this class to create own tool window. To do this:</para>
  /// <para>- in the constructor, set the <b>Name</b> and <b>Image</b> properties and create necessary controls. 
  /// The <b>Name</b> will be used to restore window's state;</para>
  /// <para>- override the <b>SelectionChanged</b> method. This method is called when current selection
  /// is changed. In this method, you should update buttons state to reflect the current selection. 
  /// Selected objects can be accessed via <b>Designer.SelectedObjects</b> property;</para>
  /// <para>- override the <b>UpdateContent</b> method. This method is called when the report
  /// content was changed. Typically you need to do the same actions in <b>SelectionChanged</b> and
  /// <b>UpdateContent</b> methods;</para>
  /// <para>- to register a toolwindow, add its type to the <see cref="DesignerPlugins"/> global collection:
  /// <code>
  /// DesignerPlugins.Add(typeof(MyToolWindow));
  /// </code>
  /// </para>
  /// </remarks>
  public class ToolWindowBase : DockContainerItem, IDesignerPlugin
  {
    #region Fields
    private Designer FDesigner;
    private bool FLocked;
    private eShortcut FShortcut;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the report designer.
    /// </summary>
    public Designer Designer
    {
      get { return FDesigner; }
    }

    /// <summary>
    /// Gets a value indicating that window is locked.
    /// </summary>
    public bool Locked
    {
      get { return FLocked; }
    }

    /// <summary>
    /// Gets or sets shortcut keys used to show this toolwindow.
    /// </summary>
    public eShortcut Shortcut
    {
      get { return FShortcut; }
      set { FShortcut = value; }
    }
    
    /// <inheritdoc/>
    public string PluginName
    {
      get { return Name; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the toolwindow can be closed by the x button.
    /// </summary>
    public bool CanHide
    {
      get { return Bar.CanHide; }
      set { Bar.CanHide = value; }
    }

    internal Bar Bar
    {
      get
      {
        BaseItem item = this;
        while (item.Parent != null)
          item = item.Parent;
        return item.ContainerControl as Bar;
      }
    }

    /// <summary>
    /// Gets a parent control that contains all controls.
    /// </summary>
    /// <remarks>
    /// Add your control to the parent control Controls collection.
    /// </remarks>
    public Control ParentControl
    {
      get { return Control; }
    }
    #endregion

    #region Private Methods
    private Bar CreateBar()
    {
      Bar bar = new Bar();
      bar.Name = Name + "Bar";
      bar.CanHide = true;
      bar.CloseSingleTab = true;
      bar.GrabHandleStyle = eGrabHandleStyle.Caption;
      bar.LayoutType = eLayoutType.DockContainer;
      bar.Stretch = true;
      bar.AutoSyncBarCaption = true;

      DockTo(bar);
      return bar;
    }
    #endregion
    
    #region Protected Methods
    /// <inheritdoc/>
    protected override void OnContainerChanged(object objOldContainer)
    {
      base.OnContainerChanged(objOldContainer);
      Bar bar = Bar;
      if (bar != null)
      {
        bar.Font = DrawUtils.DefaultFont;
        bar.PaddingLeft = -2;
        bar.PaddingRight = -2;
        bar.PaddingTop = -2;
        bar.PaddingBottom = -2;
      }
    }
    #endregion

    #region Public Methods
    internal void DoDefaultDock()
    {
      DockTo(Designer.DotNetBarManager.RightDockSite, Designer.DataWindow, eDockSide.Top);
    }
    
    internal void DockTo(DockSite site)
    {
      site.GetDocumentUIManager().Dock(CreateBar());
    }

    internal void DockTo(DockSite site, ToolWindowBase referenceWindow, eDockSide side)
    {
      site.GetDocumentUIManager().Dock(referenceWindow.Bar, CreateBar(), side);
    }

    internal void DockTo(ToolWindowBase win)
    {
      DockTo(win.Bar);
    }

    internal void DockTo(Bar bar)
    {
      bar.Controls.Add(Control);
      bar.Items.Add(this);
    }

    /// <summary>
    /// Shows the toolwindow.
    /// </summary>
    public void Show()
    {
      // force SetDockContainerVisible to do the work
      Visible = false;
      BarUtilities.SetDockContainerVisible(this, true);
      Activate();
    }

    /// <summary>
    /// Hides the toolwindow.
    /// </summary>
    public void Hide()
    {
      BarUtilities.SetDockContainerVisible(this, false);
    }

    internal void Close()
    {
      Bar bar = Bar;
      if (bar != null)
        bar.CloseDockTab(this);
    }

    internal void Activate()
    {
      Bar bar = Bar;
      if (bar != null)
      {
        bar.SelectedDockContainerItem = this;
        if (bar.AutoHide)
          bar.AutoHide = false;
      }  
    }
    #endregion

    #region IDesignerPlugin
    /// <inheritdoc/>
    public virtual void SaveState()
    {
    }

    /// <inheritdoc/>
    public virtual void RestoreState()
    {
    }

    /// <inheritdoc/>
    public virtual void SelectionChanged()
    {
    }

    /// <inheritdoc/>
    public virtual void UpdateContent()
    {
    }

    /// <inheritdoc/>
    public virtual void Lock()
    {
      FLocked = true;
    }

    /// <inheritdoc/>
    public virtual void Unlock()
    {
      FLocked = false;
      UpdateContent();
    }
    
    /// <inheritdoc/>
    public virtual void Localize()
    {
    }

    /// <summary>
    /// Implements <see cref="IDesignerPlugin.GetOptionsPage"/> method.
    /// </summary>
    /// <returns>The options page, if implemented; otherwise, <b>null</b>.</returns>
    public virtual DesignerOptionsPage GetOptionsPage()
    {
      return null;
    }

    /// <inheritdoc/>
    public virtual void UpdateUIStyle()
    {
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolWindowBase"/> class with default settings.
    /// </summary>
    /// <param name="designer">The report designer.</param>
    /// <remarks>
    /// You don't need to call this constructor. The designer will do this automatically.
    /// </remarks>
    public ToolWindowBase(Designer designer) : base()
    {
      FDesigner = designer;
      FShortcut = eShortcut.None;
      Control = new PanelDockContainer();
    }
  }

}
