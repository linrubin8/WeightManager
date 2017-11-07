using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// The base class for the context menu of the report component.
  /// </summary>
  /// <remarks>
  /// This class represents a context menu of the report component that is displayed when the object 
  /// is right-clicked in the designer. This class implements the following actions: Edit, Cut, Copy, 
  /// Paste, Delete, Bring to Front, Send to Back.
  /// </remarks>
  [ToolboxItem(false)]
  public class ComponentMenuBase : ContextMenuBar
  {
    #region Fields
    private Designer FDesigner;
    private ButtonItem mnuContextRoot;
    
    /// <summary>
    /// The "Name" menu item.
    /// </summary>
    public ButtonItem miName;
    /// <summary>
    /// The "Edit" menu item.
    /// </summary>
    public ButtonItem miEdit;
    /// <summary>
    /// The "Cut" menu item.
    /// </summary>
    public ButtonItem miCut;
    /// <summary>
    /// The "Copy" menu item.
    /// </summary>
    public ButtonItem miCopy;
    /// <summary>
    /// The "Paste" menu item.
    /// </summary>
    public ButtonItem miPaste;
    /// <summary>
    /// The "Delete" menu item.
    /// </summary>
    public ButtonItem miDelete;
    /// <summary>
    /// The "BringToFront" menu item.
    /// </summary>
    public ButtonItem miBringToFront;
    /// <summary>
    /// The "SendToBack" menu item.
    /// </summary>
    public ButtonItem miSendToBack;
    #endregion
    
    #region Properties
    /// <summary>
    /// The reference to the report designer.
    /// </summary>
    public Designer Designer
    {
      get { return FDesigner; }
    }
    
    /// <summary>
    /// Gets a collection of menu items.
    /// </summary>
    /// <remarks>
    /// You should add new items to this collection.
    /// </remarks>
    public new SubItemsCollection Items
    {
      get { return mnuContextRoot.SubItems; }
    }
    #endregion
    
    #region Private Methods
    #endregion
    
    #region Protected Methods
    /// <summary>
    /// This method is called to reflect changes in the designer.
    /// </summary>
    protected void Change()
    {
      Designer.SetModified(null, "Change");
    }

    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    /// <param name="text">Item's text.</param>
    /// <returns>New item.</returns>
    protected ButtonItem CreateMenuItem(string text)
    {
      return CreateMenuItem(null, text, null);
    }

    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    /// <param name="text">Item's text.</param>
    /// <param name="click">Click handler.</param>
    /// <returns>New item.</returns>
    protected ButtonItem CreateMenuItem(string text, EventHandler click)
    {
      return CreateMenuItem(null, text, click);
    }

    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    /// <param name="image">Item's image.</param>
    /// <param name="text">Item's text.</param>
    /// <param name="click">Click handler.</param>
    /// <returns>New item.</returns>
    protected ButtonItem CreateMenuItem(Image image, string text, EventHandler click)
    {
      ButtonItem item = new ButtonItem();
      item.Image = image;
      item.Text = text;
      if (click != null)
        item.Click += click;
      return item;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>ComponentMenuBase</b> class with default settings. 
    /// </summary>
    /// <param name="designer">The reference to a report designer.</param>
    public ComponentMenuBase(Designer designer) : base()
    {
      FDesigner = designer;
      Style = UIStyleUtils.GetDotNetBarStyle(Designer.UIStyle);
      Font = DrawUtils.DefaultFont;
      mnuContextRoot = new ButtonItem();
      base.Items.Add(mnuContextRoot);

      miName = CreateMenuItem("");
      miEdit = CreateMenuItem(Res.Get("ComponentMenu,Component,Edit"), Designer.cmdEdit.Invoke);
      miEdit.BeginGroup = true;
      miCut = CreateMenuItem(Res.GetImage(5), Res.Get("Designer,Menu,Edit,Cut"), Designer.cmdCut.Invoke);
      miCut.BeginGroup = true;
      miCopy = CreateMenuItem(Res.GetImage(6), Res.Get("Designer,Menu,Edit,Copy"), Designer.cmdCopy.Invoke);
      miPaste = CreateMenuItem(Res.GetImage(7), Res.Get("Designer,Menu,Edit,Paste"), Designer.cmdPaste.Invoke);
      miDelete = CreateMenuItem(Res.GetImage(51), Res.Get("Designer,Menu,Edit,Delete"), Designer.cmdDelete.Invoke);
      miBringToFront = CreateMenuItem(Res.GetImage(14), Res.Get("Designer,Toolbar,Layout,BringToFront"), Designer.cmdBringToFront.Invoke);
      miBringToFront.BeginGroup = true;
      miSendToBack = CreateMenuItem(Res.GetImage(15), Res.Get("Designer,Toolbar,Layout,SendToBack"), Designer.cmdSendToBack.Invoke);

      miEdit.Visible = Designer.cmdEdit.Enabled;
      miCut.Enabled = Designer.cmdCut.Enabled;
      miCopy.Enabled = Designer.cmdCopy.Enabled;
      miPaste.Enabled = Designer.cmdPaste.Enabled;
      miDelete.Enabled = Designer.cmdDelete.Enabled;
      miBringToFront.Enabled = Designer.cmdBringToFront.Enabled;
      miSendToBack.Enabled = Designer.cmdSendToBack.Enabled;
      
      SelectedObjectCollection selection = Designer.SelectedObjects;
      miName.Text = (selection.Count == 1 ? 
        selection[0].Name : 
        String.Format(Res.Get("Designer,ToolWindow,Properties,NObjectsSelected"), selection.Count)) + ":";
      miName.FontBold = true;
      miName.HotFontBold = true;
      
      Items.AddRange(new BaseItem[] {
        miName, 
        miEdit, 
        miCut, miCopy, miPaste, miDelete,
        miBringToFront, miSendToBack });
    }
  }
}
