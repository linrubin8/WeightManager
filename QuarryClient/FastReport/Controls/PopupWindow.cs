using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents a popup form.
  /// </summary>
  /// <remarks>
  /// Use this form if you want to show some controls in non-modal borderless form that
  /// behaves like other standard popup controls such as context menu. This form does not
  /// move a focus from the parent form.
  /// </remarks>
  public class PopupWindow : Form
  {
    private Form FOwnerForm;
    private PopupWindowHelper FPopupHelper;
  
    /// <summary>
    /// Shows the form.
    /// </summary>
    /// <param name="ctl">The control which location is used as a reference for <b>pt</b> parameter.</param>
    /// <param name="pt">The location relative to the <b>ctl</b> control.</param>
    public void Show(Control ctl, Point pt)
    {
      Show(ctl.PointToScreen(pt));
    }

    /// <summary>
    /// Shows the form.
    /// </summary>
    /// <param name="ctl">The control which location is used as a reference for <b>x</b>, <b>y</b> parameters.</param>
    /// <param name="x">The x position relative to the <b>ctl</b> control.</param>
    /// <param name="y">The y position relative to the <b>ctl</b> control.</param>
    public void Show(Control ctl, int x, int y)
    {
      Show(ctl, new Point(x, y));
    }

    /// <summary>
    /// Shows the form.
    /// </summary>
    /// <param name="pt">The absolute screen location.</param>
    public void Show(Point pt)
    {
      Rectangle area = Screen.GetWorkingArea(pt);
      if (pt.X + Width > area.Right)
        pt.X = area.Right - Width;
      if (pt.Y + Height > area.Bottom)
        pt.Y = area.Bottom - Height;
      FPopupHelper.ShowPopup(FOwnerForm, this, pt);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PopupWindow"/> class with default settings.
    /// </summary>
    /// <param name="ownerForm">The main form that owns this popup form.</param>
    public PopupWindow(Form ownerForm) : base()
    {
      FOwnerForm = ownerForm;
      FPopupHelper = new PopupWindowHelper();
      FPopupHelper.AssignHandle(ownerForm.Handle);
      FPopupHelper.PopupCancel += PopupCancel;
      FormBorderStyle = FormBorderStyle.FixedToolWindow;
      ControlBox = false;
      StartPosition = FormStartPosition.Manual;
      ShowInTaskbar = false;
    }
    

    /// <summary>
    /// Handler which allows to prevent canceling of popup window
    /// </summary>
    /// <param name="sender">Popup helper</param>
    /// <param name="e">Event arguments</param>
    protected virtual void PopupCancel(object sender, PopupCancelEventArgs e)
    {            
    }
  }
}
