using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents a combobox that allows to choose a color.
  /// </summary>
  /// <remarks>
  /// This control may be useful if you write own components for FastReport.
  /// </remarks>
  [ToolboxItem(false)]
  public class ColorComboBox : UserControl
  {
    private ComboBox FCombo;
    private ColorDropDown FDropDown;
    private bool FShowColorName;

    /// <summary>
    /// This event is raised when you select a color.
    /// </summary>
    public event EventHandler ColorSelected;

    /// <summary>
    /// Gets or sets the selected color.
    /// </summary>
    public Color Color
    {
      get { return FDropDown.Color; }
      set 
      { 
        FDropDown.Color = value; 
        Refresh();
      }
    }
    
    /// <summary>
    /// Gets or sets value indicating whether it is necessary to show a color name in a combobox.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool ShowColorName
    {
      get { return FShowColorName; }
      set 
      { 
        FShowColorName = value; 
        Refresh();
      }
    }

    private void FDropDown_ColorSelected(object sender, EventArgs e)
    {
      Refresh();
      if (ColorSelected != null)
        ColorSelected(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
      height = FCombo.Height;
      base.SetBoundsCore(x, y, width, height, specified);
      FCombo.Width = Width;
    }

    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      using (Bitmap bmp = new Bitmap(Width, Height))
      {
        using (Graphics gBmp = Graphics.FromImage(bmp))
        {
          gBmp.FillRectangle(SystemBrushes.Control, 0, 0, Width, Height);
        }
        
        FCombo.Enabled = Enabled;
        FCombo.DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
        
        g.DrawImage(bmp, 0, 0);
        using (Brush b = new SolidBrush(Color))
        {
          Rectangle rect = new Rectangle(4, 4, 24, Height - 9);
          if (Enabled)
            g.FillRectangle(b, rect);
          g.DrawRectangle(SystemPens.ControlDark, rect);
          
          if (ShowColorName)
          {
            string colorName = Converter.ToString(Color);
            TextRenderer.DrawText(g, colorName, Font, new Point(rect.Right + 8, rect.Top), SystemColors.ControlText);
          }
        }
      }
    }

    /// <inheritdoc/>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      FDropDown.Show(this, new Point(0, Height));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorComboBox"/> class with default settings.
    /// </summary>
    public ColorComboBox()
    {
      FCombo = new ComboBox();
      FDropDown = new ColorDropDown();
      FDropDown.ColorSelected += new EventHandler(FDropDown_ColorSelected);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }
  }
}
