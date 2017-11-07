using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Dialog
{
  /// <summary>
  /// Uses a mask to distinguish between proper and improper user input.
  /// Wraps the <see cref="System.Windows.Forms.MaskedTextBox"/> control.
  /// </summary>
  public class MaskedTextBoxControl : DataFilterBaseControl
  {
    private MaskedTextBox FMaskedTextBox;

    #region Properties
    /// <summary>
    /// Gets an internal <b>MaskedTextBox</b>.
    /// </summary>
    [Browsable(false)]
    public MaskedTextBox MaskedTextBox
    {
      get { return FMaskedTextBox; }
    }

    /// <summary>
    /// Gets or sets the input mask to use at run time.
    /// Wraps the <see cref="System.Windows.Forms.MaskedTextBox.Mask"/> property.
    /// </summary>
    [Category("Data")]
    public string Mask
    {
      get { return MaskedTextBox.Mask; }
      set { MaskedTextBox.Mask = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
    /// Wraps the <see cref="System.Windows.Forms.MaskedTextBox.SkipLiterals"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool SkipLiterals
    {
      get { return MaskedTextBox.SkipLiterals; }
      set { MaskedTextBox.SkipLiterals = value; }
    }

    /// <summary>
    /// Gets or sets how text is aligned in a masked text box control.
    /// Wraps the <see cref="System.Windows.Forms.MaskedTextBox.TextAlign"/> property.
    /// </summary>
    [DefaultValue(HorizontalAlignment.Left)]
    [Category("Appearance")]
    public HorizontalAlignment TextAlign
    {
      get { return MaskedTextBox.TextAlign; }
      set { MaskedTextBox.TextAlign = value; }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override bool ShouldSerializeBackColor()
    {
      return BackColor != SystemColors.Window;
    }

    /// <inheritdoc/>
    protected override bool ShouldSerializeCursor()
    {
      return Cursor != Cursors.IBeam;
    }

    /// <inheritdoc/>
    protected override bool ShouldSerializeForeColor()
    {
      return ForeColor != SystemColors.WindowText;
    }

    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] { 
        new SelectionPoint(AbsLeft - 2, AbsTop + Height / 2, SizingPoint.LeftCenter),
        new SelectionPoint(AbsLeft + Width + 1, AbsTop + Height / 2, SizingPoint.RightCenter) };
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      return Text;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      MaskedTextBoxControl c = writer.DiffObject as MaskedTextBoxControl;
      base.Serialize(writer);

      if (Mask != c.Mask)
        writer.WriteStr("Mask", Mask);
      if (SkipLiterals != c.SkipLiterals)
        writer.WriteBool("SkipLiterals", SkipLiterals);
      if (TextAlign != c.TextAlign)
        writer.WriteValue("TextAlign", TextAlign);
    }

    /// <inheritdoc/>
    public override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      OnFilterChanged();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>MaskedTextBoxControl</b> class with default settings. 
    /// </summary>
    public MaskedTextBoxControl()
    {
      FMaskedTextBox = new MaskedTextBox();
      Control = FMaskedTextBox;
      BindableProperty = this.GetType().GetProperty("Text");
    }
  }
}
