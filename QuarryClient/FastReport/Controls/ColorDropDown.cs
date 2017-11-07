using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents a drop-down control that allows to choose a color.
  /// </summary>
  /// <remarks>
  /// This control may be useful if you write own components for FastReport.
  /// </remarks>
  [ToolboxItem(false)]
  public class ColorDropDown : ToolStripDropDown
  {
    private ToolStripControlHost FHost;
    private ColorSelector FColorSelector;

    /// <summary>
    /// This event is raised when you select a color.
    /// </summary>
    public event EventHandler ColorSelected;

    /// <summary>
    /// Gets or sets the selected color.
    /// </summary>
    public Color Color
    {
      get { return FColorSelector.Color; }
      set { FColorSelector.Color = value; }
    }

    private void FColorSelector_ColorSelected(object sender, EventArgs e)
    {
      Close();
      if (ColorSelected != null)
        ColorSelected(this, EventArgs.Empty);
    }

    private void ColorDropDown_Opening(object sender, CancelEventArgs e)
    {
      FColorSelector.Localize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorDropDown"/> class with default settings.
    /// </summary>
    public ColorDropDown()
    {
      Font = DrawUtils.DefaultFont;
      FColorSelector = new ColorSelector();
      FColorSelector.ColorSelected += new EventHandler(FColorSelector_ColorSelected);
      FHost = new ToolStripControlHost(FColorSelector);
      Items.Add(FHost);
      Opening += new CancelEventHandler(ColorDropDown_Opening);
      //BackColor = Config.DesignerSettings.CustomRenderer.ControlColor;
    }
  }
}
