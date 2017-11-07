using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Forms
{
  /// <summary>
  /// Base class for all dialog forms with two buttons, OK and Cancel. 
  /// </summary>
  public partial class BaseDialogForm : Form
  {
    /// <summary>
    /// Localizes the dialog controls.
    /// </summary>
    /// <remarks>
    /// Use this method to set control's captions specific to the current locale.
    /// </remarks>
    public virtual void Localize()
    {
      btnOk.Text = Res.Get("Buttons,Ok");
      btnCancel.Text = Res.Get("Buttons,Cancel");
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDialogForm"/> class.
    /// </summary>
    public BaseDialogForm()
    {
      InitializeComponent();
      this.Font = DrawUtils.DefaultFont;
    }
  }
}