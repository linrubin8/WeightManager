using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents a popup window that alows to choose a color.
  /// </summary>
  /// <remarks>
  /// This control may be useful if you write own components for FastReport.
  /// </remarks>
  public class ColorPopup : PopupWindow
  {
    private ColorSelector FSelector;

    /// <summary>
    /// This event is raised when you select a color.
    /// </summary>
    public event EventHandler ColorSelected;

    /// <summary>
    /// Gets or sets the selected color.
    /// </summary>
    public Color Color
    {
      get { return FSelector.Color; }
      set { FSelector.Color = value; }
    }

    private void FSelector_ColorSelected(object sender, EventArgs e)
    {
      Close();
      if (ColorSelected != null)
        ColorSelected(this, EventArgs.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorPopup"/> class with default settings.
    /// </summary>
    /// <param name="ownerForm">The main form that owns this popup control.</param>
    public ColorPopup(Form ownerForm) : base(ownerForm)
    {
      FSelector = new ColorSelector();
      Controls.Add(FSelector);
      Font = ownerForm.Font;
      ClientSize = FSelector.Size;
      BackColor = SystemColors.Window;
      FSelector.ColorSelected += new EventHandler(FSelector_ColorSelected);
    }

  }
}
