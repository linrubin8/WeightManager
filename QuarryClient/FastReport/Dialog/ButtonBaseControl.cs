using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Implements base behavior of button controls.
  /// </summary>
  public class ButtonBaseControl : DataFilterBaseControl
  {
    #region Properties
    private ButtonBase Button
    {
      get { return Control as ButtonBase; }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the control resizes based on its contents.
    /// Wraps the <see cref="System.Windows.Forms.ButtonBase.AutoSize"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public virtual bool AutoSize
    {
      get { return Button.AutoSize; }
      set { Button.AutoSize = value; }
    }

    /// <summary>
    /// Gets or sets the image that is displayed on a button control.
    /// Wraps the <see cref="System.Windows.Forms.ButtonBase.Image"/> property.
    /// </summary>
    [Category("Appearance")]
    public Image Image
    {
      get { return Button.Image; }
      set { Button.Image = value; }
    }

    /// <summary>
    /// Gets or sets the alignment of the image on the button control.
    /// Wraps the <see cref="System.Windows.Forms.ButtonBase.ImageAlign"/> property.
    /// </summary>
    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Category("Appearance")]
    public ContentAlignment ImageAlign
    {
      get { return Button.ImageAlign; }
      set { Button.ImageAlign = value; }
    }

    /// <summary>
    /// Gets or sets the alignment of the text on the button control.
    /// Wraps the <see cref="System.Windows.Forms.ButtonBase.TextAlign"/> property.
    /// </summary>
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Category("Appearance")]
    public virtual ContentAlignment TextAlign
    {
      get { return Button.TextAlign; }
      set { Button.TextAlign = value; }
    }

    /// <summary>
    /// Gets or sets the position of text and image relative to each other.
    /// Wraps the <see cref="System.Windows.Forms.ButtonBase.TextImageRelation"/> property.
    /// </summary>
    [DefaultValue(TextImageRelation.Overlay)]
    [Category("Appearance")]
    public TextImageRelation TextImageRelation
    {
      get { return Button.TextImageRelation; }
      set { Button.TextImageRelation = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool AutoFill
    {
      get { return base.AutoFill; }
      set { base.AutoFill = value; }
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
    protected override object GetValue()
    {
      return null;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ButtonBaseControl c = writer.DiffObject as ButtonBaseControl;
      base.Serialize(writer);

      if (AutoSize != c.AutoSize)
        writer.WriteBool("AutoSize", AutoSize);
      if (!writer.AreEqual(Image, c.Image))
        writer.WriteValue("Image", Image);
      if (ImageAlign != c.ImageAlign)
        writer.WriteValue("ImageAlign", ImageAlign);
      if (TextAlign != c.TextAlign)
        writer.WriteValue("TextAlign", TextAlign);
      if (TextImageRelation != c.TextImageRelation)
        writer.WriteValue("TextImageRelation", TextImageRelation);
    }
    #endregion

  }
}
