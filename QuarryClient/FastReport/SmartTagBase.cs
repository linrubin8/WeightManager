using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Controls;
using FastReport.Design;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// The base class for smart tags.
  /// </summary>
  /// <remarks>
  /// <para>
  /// "Smart tag" is a little button that appears near the object's top-right corner when we are in the
  /// designer and move the mouse over the object. When you click that button you will see a popup window
  /// where you can set up some properties of the object. FastReport uses smart tags to quickly choose
  /// the datasource (for a band) or data column (for objects).
  /// </para>
  /// <para>
  /// Smart tag is internally a ContextMenuStrip.
  /// </para>
  /// </remarks>
  public class SmartTagBase
  {
    private ContextMenuStrip FMenu;
    private ComponentBase FObj;
    
    /// <summary>
    /// Gets the underlying context menu.
    /// </summary>
    public ContextMenuStrip Menu
    {
      get { return FMenu; }
    }
    
    /// <summary>
    /// Gets the report object that invokes this smart tag.
    /// </summary>
    public ComponentBase Obj
    {
      get { return FObj; }
    }
    
    /// <summary>
    /// Gets the report designer.
    /// </summary>
    public Designer Designer
    {
      get { return FObj.Report.Designer; }
    }

    /// <summary>
    /// Called when the menu item is clicked.
    /// </summary>
    /// <remarks>
    /// Override this method to define a reaction on the menu item click.
    /// </remarks>
    protected virtual void ItemClicked()
    {
      Designer.SetModified(this, "Change", Obj.Name);
    }

    
    /// <summary>
    /// Creates the smart tag menu items.
    /// </summary>
    /// <remarks>
    /// Override this method to create the smart tag menu.
    /// </remarks>
    protected virtual void CreateItems()
    {
    }
    
    /// <summary>
    /// Displays a smart tag at the specified screen location.
    /// </summary>
    /// <remarks>
    /// Do not call this method directly. It is called automatically when click on smart tag button.
    /// </remarks>
    /// <param name="pt">Screen location.</param>
    public void Show(Point pt)
    {
      CreateItems();
      FMenu.Show(pt);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SmartTagBase"/> class with default settings.
    /// </summary>
    /// <param name="obj">Report object that owns this smart tag.</param>
    public SmartTagBase(ComponentBase obj)
    {
      FObj = obj;
      FMenu = new ContextMenuStrip();
      FMenu.Renderer = Config.DesignerSettings.ToolStripRenderer;
    }
  }
}
