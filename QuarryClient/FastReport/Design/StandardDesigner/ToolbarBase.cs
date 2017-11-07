using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Design.PageDesigners;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
  /// <summary>
  /// Base class for all designer toolbars.
  /// </summary>
  /// <remarks>
  /// Use this class to write own designer's toolbar. To do this:
  /// <para>- in the constructor, set the <b>Name</b> property and create toolbar buttons. 
  /// The <b>Name</b> will be used to restore toolbar's state;</para>
  /// <para>- override the <b>SelectionChanged</b> method. This method is called when current selection
  /// is changed. In this method, you should update buttons state to reflect the current selection. 
  /// Selected objects can be accessed via <b>Designer.SelectedObjects</b> property;</para>
  /// <para>- override the <b>UpdateContent</b> method. This method is called when the report
  /// content was changed. Typically you need to do the same actions in <b>SelectionChanged</b> and
  /// <b>UpdateContent</b> methods;</para>
  /// <para>- to register a toolbar, add its type to the <see cref="DesignerPlugins"/> global collection:
  /// <code>
  /// DesignerPlugins.Add(typeof(MyToolbar));
  /// </code>
  /// </para>
  /// </remarks>
  [ToolboxItem(false)]
  public class ToolbarBase : Bar, IDesignerPlugin
  {
    #region Fields
    private DesignerControl FDesigner;
    private CustomizeItem FCustomizeItem;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the report designer.
    /// </summary>
    public DesignerControl Designer
    {
      get { return FDesigner; }
    }

    /// <inheritdoc/>
    public string PluginName
    {
      get { return Name; }
    }
    
    internal CustomizeItem CustomizeItem
    {
      get { return FCustomizeItem; }
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
    public void Lock()
    {
    }

    /// <inheritdoc/>
    public void Unlock()
    {
      UpdateContent();
    }

    /// <inheritdoc/>
    public virtual void Localize()
    {
      FCustomizeItem.Text = Res.Get("Designer,Toolbar,AddOrRemove");
    }

    /// <inheritdoc/>
    public virtual DesignerOptionsPage GetOptionsPage()
    {
      return null;
    }

    /// <inheritdoc/>
    public virtual void UpdateUIStyle()
    {
      Style = UIStyleUtils.GetDotNetBarStyle(Designer.UIStyle);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Creates a new button.
    /// </summary>
    /// <param name="name">Button's name.</param>
    /// <param name="image">Button's image.</param>
    /// <param name="click">Click handler.</param>
    /// <returns>New button.</returns>
    public ButtonItem CreateButton(string name, Image image, EventHandler click)
    {
      return CreateButton(name, image, "", click);
    }

    /// <summary>
    /// Creates a new button.
    /// </summary>
    /// <param name="name">Button's name.</param>
    /// <param name="image">Button's image.</param>
    /// <param name="tooltip">Button's tooltip text.</param>
    /// <param name="click">Click handler.</param>
    /// <returns>New button.</returns>
    public ButtonItem CreateButton(string name, Image image, string tooltip, EventHandler click)
    {
      ButtonItem item = new ButtonItem();
      item.Name = name;
      item.Image = image;
      item.Tooltip = tooltip;
      if (click != null)
        item.Click += click;
      return item;
    }
    
    internal void SetItemText(BaseItem item, string text)
    {
      item.Tooltip = text;
      item.Text = text;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolbarBase"/> class with default settings.
    /// </summary>
    /// <param name="designer">The report designer.</param>
    /// <remarks>
    /// You don't need to call this constructor. The designer will do this automatically.
    /// </remarks>
    public ToolbarBase(Designer designer) : base()
    {
      FDesigner = designer as DesignerControl;
      Font = DrawUtils.Default96Font;
      GrabHandleStyle = eGrabHandleStyle.Office2003;
      
      FCustomizeItem = new CustomizeItem();
      FCustomizeItem.CustomizeItemVisible = false;
    }
  }


}
