using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows CheckBox.
  /// Wraps the <see cref="System.Windows.Forms.CheckBox"/> control.
  /// </summary>
  public class CheckBoxControl : ButtonBaseControl
  {
    #region Fields
    private CheckBox FCheckBox;
    private string FCheckedChangedEvent;
    #endregion

    #region Properties
    /// <summary>
    /// Occurs when the value of the <b>Checked</b> property changes.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.CheckedChanged"/> event.
    /// </summary>
    public event EventHandler CheckedChanged;

    /// <summary>
    /// Gets an internal <b>CheckBox</b>.
    /// </summary>
    [Browsable(false)]
    public CheckBox CheckBox
    {
      get { return FCheckBox; }
    }

    /// <summary>
    /// Gets or sets the value that determines the appearance of a CheckBox control.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.Appearance"/> property.
    /// </summary>
    [DefaultValue(Appearance.Normal)]
    [Category("Appearance")]
    public Appearance Appearance
    {
      get { return CheckBox.Appearance; }
      set { CheckBox.Appearance = value; }
    }

    /// <summary>
    /// Gets or sets the horizontal and vertical alignment of the check mark on a CheckBox control.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.CheckAlign"/> property.
    /// </summary>
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Category("Appearance")]
    public ContentAlignment CheckAlign
    {
      get { return CheckBox.CheckAlign; }
      set { CheckBox.CheckAlign = value; }
    }

    /// <summary>
    /// Gets or set a value indicating whether the CheckBox is in the checked state.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.Checked"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool Checked
    {
      get { return CheckBox.Checked; }
      set { CheckBox.Checked = value; }
    }

    /// <summary>
    /// Gets or sets the state of the CheckBox.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.CheckState"/> property.
    /// </summary>
    [DefaultValue(CheckState.Unchecked)]
    [Category("Appearance")]
    public CheckState CheckState
    {
      get { return CheckBox.CheckState; }
      set { CheckBox.CheckState = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the CheckBox will allow three check states rather than two.
    /// Wraps the <see cref="System.Windows.Forms.CheckBox.ThreeState"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool ThreeState
    {
      get { return CheckBox.ThreeState; }
      set { CheckBox.ThreeState = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="CheckedChanged"/> event.
    /// </summary>
    [Category("Events")]
    public string CheckedChangedEvent
    {
      get { return FCheckedChangedEvent; }
      set { FCheckedChangedEvent = value; }
    }
    #endregion

    #region Private Methods
    private void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
      OnCheckedChanged(e);
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (CheckBox.AutoSize)
        return new SelectionPoint[] { new SelectionPoint(AbsLeft - 2, AbsTop - 2, SizingPoint.None) };
      return base.GetSelectionPoints();
    }

    /// <inheritdoc/>
    protected override void AttachEvents()
    {
      base.AttachEvents();
      CheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      CheckBox.CheckedChanged -= new EventHandler(CheckBox_CheckedChanged);
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      return Checked;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      CheckBoxControl c = writer.DiffObject as CheckBoxControl;
      base.Serialize(writer);

      if (Appearance != c.Appearance)
        writer.WriteValue("Appearance", Appearance);
      if (CheckAlign != c.CheckAlign)
        writer.WriteValue("CheckAlign", CheckAlign);
      if (Checked != c.Checked)
        writer.WriteBool("Checked", Checked);
      if (CheckState != c.CheckState)
        writer.WriteValue("CheckState", CheckState);
      if (ThreeState != c.ThreeState)
        writer.WriteBool("ThreeState", ThreeState);
      if (CheckedChangedEvent != c.CheckedChangedEvent)
        writer.WriteStr("CheckedChangedEvent", CheckedChangedEvent);
    }

    /// <summary>
    /// This method fires the <b>CheckedChanged</b> event and the script code connected to the <b>CheckedChangedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnCheckedChanged(EventArgs e)
    {
      OnFilterChanged();
      if (CheckedChanged != null)
        CheckedChanged(this, e);
      InvokeEvent(CheckedChangedEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>CheckBoxControl</b> class with default settings. 
    /// </summary>
    public CheckBoxControl()
    {
      FCheckBox = new CheckBox();
      Control = FCheckBox;
      CheckBox.AutoSize = true;
      BindableProperty = this.GetType().GetProperty("Checked");
    }
  }
}
