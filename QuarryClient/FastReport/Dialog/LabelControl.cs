using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a standard Windows label.
  /// Wraps the <see cref="System.Windows.Forms.Label"/> control.
  /// </summary>
  public class LabelControl : DialogControl
  {
    private Label FLabel;

    #region Properties
    /// <summary>
    /// Gets an internal <b>Label</b>.
    /// </summary>
    [Browsable(false)]
    public Label Label
    {
      get { return FLabel; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// Wraps the <see cref="System.Windows.Forms.Label.AutoSize"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Layout")]
    public bool AutoSize
    {
      get { return Label.AutoSize; }
      set { Label.AutoSize = value; }
    }

    /// <summary>
    /// Gets or sets the alignment of text in the label.
    /// Wraps the <see cref="System.Windows.Forms.Label.TextAlign"/> property.
    /// </summary>
    [DefaultValue(ContentAlignment.TopLeft)]
    [Category("Appearance")]
    public ContentAlignment TextAlign
    {
      get { return Label.TextAlign; }
      set { Label.TextAlign = value; }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (Label.AutoSize)
        return new SelectionPoint[] { new SelectionPoint(AbsLeft - 2, AbsTop - 2, SizingPoint.None) };
      return base.GetSelectionPoints();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      LabelControl c = writer.DiffObject as LabelControl;
      base.Serialize(writer);

      if (AutoSize != c.AutoSize)
        writer.WriteBool("AutoSize", AutoSize);
      if (TextAlign != c.TextAlign)
        writer.WriteValue("TextAlign", TextAlign);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>LabelControl</b> class with default settings. 
    /// </summary>
    public LabelControl()
    {
      FLabel = new Label();
      Control = FLabel;
      
      Label.AutoSize = true;
    }
  }
}
