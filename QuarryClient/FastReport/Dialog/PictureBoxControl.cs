using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using System.Drawing.Drawing2D;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows picture box control for displaying an image.
  /// Wraps the <see cref="System.Windows.Forms.PictureBox"/> control.
  /// </summary>
  public class PictureBoxControl : DialogControl
  {
    private PictureBox FPictureBox;

    #region Properties
    /// <summary>
    /// Gets an internal <b>PictureBox</b>.
    /// </summary>
    [Browsable(false)]
    public PictureBox PictureBox
    {
      get { return FPictureBox; }
    }

    /// <summary>
    /// Indicates the border style for the control.
    /// Wraps the <see cref="System.Windows.Forms.PictureBox.BorderStyle"/> property.
    /// </summary>
    [DefaultValue(BorderStyle.None)]
    [Category("Appearance")]
    public BorderStyle BorderStyle
    {
      get { return PictureBox.BorderStyle; }
      set { PictureBox.BorderStyle = value; }
    }

    /// <summary>
    /// Gets or sets the image that the PictureBox displays.
    /// Wraps the <see cref="System.Windows.Forms.PictureBox.Image"/> property.
    /// </summary>
    [Category("Appearance")]
    public Image Image
    {
      get { return PictureBox.Image; }
      set { PictureBox.Image = value; }
    }

    /// <summary>
    /// Indicates how the image is displayed. 
    /// Wraps the <see cref="System.Windows.Forms.PictureBox.SizeMode"/> property.
    /// </summary>
    [DefaultValue(PictureBoxSizeMode.Normal)]
    [Category("Behavior")]
    public PictureBoxSizeMode SizeMode
    {
      get { return PictureBox.SizeMode; }
      set { PictureBox.SizeMode = value; }
    }
    
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string Text
    {
      get { return base.Text; }
      set { base.Text = value; }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeImage()
    {
      return Image != null;
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (PictureBox.SizeMode == PictureBoxSizeMode.AutoSize)
        return new SelectionPoint[] { new SelectionPoint(AbsLeft - 2, AbsTop - 2, SizingPoint.None) };
      return base.GetSelectionPoints();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);
      Pen pen = e.Cache.GetPen(Color.Gray, 1, DashStyle.Dash);
      e.Graphics.DrawRectangle(pen, AbsLeft, AbsTop, Width - 1, Height - 1);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      PictureBoxControl c = writer.DiffObject as PictureBoxControl;
      base.Serialize(writer);

      if (BorderStyle != c.BorderStyle)
        writer.WriteValue("BorderStyle", BorderStyle);
      if (!writer.AreEqual(Image, c.Image))
        writer.WriteValue("Image", Image);
      if (SizeMode != c.SizeMode)
        writer.WriteValue("SizeMode", SizeMode);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>PictureBoxControl</b> class with default settings. 
    /// </summary>
    public PictureBoxControl()
    {
      FPictureBox = new PictureBox();
      Control = FPictureBox;
    }
  }
}
