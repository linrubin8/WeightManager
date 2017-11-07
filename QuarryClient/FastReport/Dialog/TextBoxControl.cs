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
  /// Represents a Windows text box control.
  /// Wraps the <see cref="System.Windows.Forms.TextBox"/> control.
  /// </summary>
  public class TextBoxControl : DataFilterBaseControl
  {
    private TextBox FTextBox;

    #region Properties
    /// <summary>
    /// Gets an internal <b>TextBox</b>.
    /// </summary>
    [Browsable(false)]
    public TextBox TextBox
    {
      get { return FTextBox; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether pressing ENTER in a multiline TextBox control creates a new line of text in the control or activates the default button for the form. 
    /// Wraps the <see cref="System.Windows.Forms.TextBox.AcceptsReturn"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AcceptsReturn
    {
      get { return TextBox.AcceptsReturn; }
      set { TextBox.AcceptsReturn = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.
    /// Wraps the <see cref="System.Windows.Forms.TextBoxBase.AcceptsTab"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AcceptsTab
    {
      get { return TextBox.AcceptsTab; }
      set { TextBox.AcceptsTab = value; }
    }

    /// <summary>
    /// Gets or sets whether the TextBox control modifies the case of characters as they are typed.
    /// Wraps the <see cref="System.Windows.Forms.TextBox.CharacterCasing"/> property.
    /// </summary>
    [DefaultValue(CharacterCasing.Normal)]
    [Category("Behavior")]
    public CharacterCasing CharacterCasing
    {
      get { return TextBox.CharacterCasing; }
      set { TextBox.CharacterCasing = value; }
    }

    /// <summary>
    /// Gets or sets the maximum number of characters the user can type or paste into the text box control.
    /// Wraps the <see cref="System.Windows.Forms.TextBoxBase.MaxLength"/> property.
    /// </summary>
    [DefaultValue(32767)]
    [Category("Behavior")]
    public int MaxLength
    {
      get { return TextBox.MaxLength; }
      set { TextBox.MaxLength = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is a multiline TextBox control. 
    /// Wraps the <see cref="System.Windows.Forms.TextBox.Multiline"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Multiline
    {
      get { return TextBox.Multiline; }
      set { TextBox.Multiline = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether text in the text box is read-only. 
    /// Wraps the <see cref="System.Windows.Forms.TextBoxBase.ReadOnly"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ReadOnly
    {
      get { return TextBox.ReadOnly; }
      set { TextBox.ReadOnly = value; }
    }

    /// <summary>
    /// Gets or sets which scroll bars should appear in a multiline TextBox control.
    /// Wraps the <see cref="System.Windows.Forms.TextBox.ScrollBars"/> property.
    /// </summary>
    [DefaultValue(ScrollBars.None)]
    [Category("Appearance")]
    public ScrollBars ScrollBars
    {
      get { return TextBox.ScrollBars; }
      set { TextBox.ScrollBars = value; }
    }

    /// <summary>
    /// Gets or sets how text is aligned in a TextBox control.
    /// Wraps the <see cref="System.Windows.Forms.TextBox.TextAlign"/> property.
    /// </summary>
    [DefaultValue(HorizontalAlignment.Left)]
    [Category("Appearance")]
    public HorizontalAlignment TextAlign
    {
      get { return TextBox.TextAlign; }
      set { TextBox.TextAlign = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.
    /// Wraps the <see cref="System.Windows.Forms.TextBox.UseSystemPasswordChar"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool UseSystemPasswordChar
    {
      get { return TextBox.UseSystemPasswordChar; }
      set { TextBox.UseSystemPasswordChar = value; }
    }

    /// <summary>
    /// Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.
    /// Wraps the <see cref="System.Windows.Forms.TextBoxBase.WordWrap"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool WordWrap
    {
      get { return TextBox.WordWrap; }
      set { TextBox.WordWrap = value; }
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
      if (!TextBox.Multiline)
        return new SelectionPoint[] { 
          new SelectionPoint(AbsLeft - 2, AbsTop + Height / 2, SizingPoint.LeftCenter),
          new SelectionPoint(AbsLeft + Width + 1, AbsTop + Height / 2, SizingPoint.RightCenter) };
      return base.GetSelectionPoints();
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
      TextBoxControl c = writer.DiffObject as TextBoxControl;
      base.Serialize(writer);

      if (AcceptsReturn != c.AcceptsReturn)
        writer.WriteBool("AcceptsReturn", AcceptsReturn);
      if (AcceptsTab != c.AcceptsTab)
        writer.WriteBool("AcceptsTab", AcceptsTab);
      if (CharacterCasing != c.CharacterCasing)
        writer.WriteValue("CharacterCasing", CharacterCasing);
      if (MaxLength != c.MaxLength)
        writer.WriteInt("MaxLength", MaxLength);
      if (Multiline != c.Multiline)
        writer.WriteBool("Multiline", Multiline);
      if (ReadOnly != c.ReadOnly)
        writer.WriteBool("ReadOnly", ReadOnly);
      if (ScrollBars != c.ScrollBars)
        writer.WriteValue("ScrollBars", ScrollBars);
      if (TextAlign != c.TextAlign)
        writer.WriteValue("TextAlign", TextAlign);
      if (UseSystemPasswordChar != c.UseSystemPasswordChar)
        writer.WriteBool("UseSystemPasswordChar", UseSystemPasswordChar);
      if (WordWrap != c.WordWrap)
        writer.WriteBool("WordWrap", WordWrap);
    }

    /// <inheritdoc/>
    public override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      OnFilterChanged();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>TextBoxControl</b> class with default settings. 
    /// </summary>
    public TextBoxControl()
    {
      FTextBox = new TextBox();
      Control = FTextBox;
      BindableProperty = this.GetType().GetProperty("Text");
    }
  }
}
