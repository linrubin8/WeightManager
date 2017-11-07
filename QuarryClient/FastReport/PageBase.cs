using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Design;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Base class for report pages and dialog forms.
  /// </summary>
  public abstract class PageBase : ComponentBase
  {
    #region Fields
    private string FPageName;
    private bool FNeedRefresh;
    private bool FNeedModify;
    #endregion

    #region Properties
    internal string PageName
    {
      get 
      {
        if (!String.IsNullOrEmpty(FPageName))
          return FPageName;
        return Name;
          
        //string pageName = Name;
        //if (pageName.StartsWith(BaseName))
        //  pageName = pageName.Replace(BaseName, Res.Get("Objects," + ClassName));

        //return pageName; 
      }
      set { FPageName = value; }
    }
    
    internal bool NeedRefresh
    {
      get { return FNeedRefresh; }
      set { FNeedRefresh = value; }
    }

    internal bool NeedModify
    {
      get { return FNeedModify; }
      set { FNeedModify = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Restrictions Restrictions
    {
      get { return base.Restrictions; }
      set { base.Restrictions = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override AnchorStyles Anchor
    {
      get { return base.Anchor; }
      set { base.Anchor = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override DockStyle Dock
    {
      get { return base.Dock; }
      set { base.Dock = value; }
    }

    /// <summary>
    /// Gets the snap size for this page.
    /// </summary>
    [Browsable(false)]
    public virtual SizeF SnapSize
    {
      get { return new SizeF(0, 0); }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Gets a page designer for this page type.
    /// </summary>
    /// <returns>The page designer.</returns>
    public abstract Type GetPageDesignerType();

    /// <summary>
    /// This method is called by the designer when you create a new page. 
    /// </summary>
    /// <remarks>
    /// You may create the default page layout (add default bands, set default page size, etc).
    /// </remarks>
    public virtual void SetDefaults()
    {
      Config.DesignerSettings.OnPageAdded(this, EventArgs.Empty);
    }
    
    /// <summary>
    /// Causes the page to refresh in the preview window.
    /// </summary>
    /// <remarks>
    /// Call this method when you handle object's MouseMove, MouseDown, MouseUp, MouseEnter, MouseLeave events
    /// and want to refresh the preview window.
    /// <note type="caution">
    /// If you have changed some objects on a page, the <b>Refresh</b> method will not save the changes. 
    /// This means when you print or export the page, you will see original (unmodified) page content.
    /// If you want to save the changes, you have to use the <see cref="Modify"/> method instead.
    /// </note>
    /// </remarks>
    public void Refresh()
    {
      FNeedRefresh = true;
    }

    /// <summary>
    /// Modifies the page content and refresh it in the preview window.
    /// </summary>
    /// <remarks>
    /// Call this method when you handle object's Click, MouseDown or MouseUp events
    /// and want to modify an object and refresh the preview window. 
    /// </remarks>
    public void Modify()
    {
      FNeedModify = true;
      FNeedRefresh = true;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PageBase"/> class with default settings.
    /// </summary>
    public PageBase()
    {
      SetFlags(Flags.CanMove | Flags.CanResize | Flags.CanDelete | Flags.CanChangeOrder | 
        Flags.CanChangeParent | Flags.CanCopy, false);
    }
  }
}