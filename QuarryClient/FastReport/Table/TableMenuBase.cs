using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  internal class TableMenuBase : ContextMenuBar
  {
    #region Fields
    private Designer FDesigner;
    private ButtonItem mnuContextRoot;
    #endregion

    #region Properties
    public Designer Designer
    {
      get { return FDesigner; }
    }

    public new SubItemsCollection Items
    {
      get { return mnuContextRoot.SubItems; }
    }
    #endregion

    #region Protected Methods
    protected virtual void Change()
    {
      FDesigner.SetModified(null, "Change");
    }

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
    
    public TableMenuBase(Designer designer) : base()
    {
      FDesigner = designer;
      Style = UIStyleUtils.GetDotNetBarStyle(designer.UIStyle);
      mnuContextRoot = new ButtonItem();
      base.Items.Add(mnuContextRoot);
    }
  }
}
