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
  /// Represents a Windows button control. 
  /// Wraps the <see cref="System.Windows.Forms.Button"/> control.
  /// </summary>
  public class ButtonControl : ButtonBaseControl
  {
    private Button FButton;
    
    #region Properties
    /// <summary>
    /// Gets an internal <b>Button</b>.
    /// </summary>
    [Browsable(false)]
    public Button Button
    {
      get { return FButton; }
    }

    /// <summary>
    /// Gets or sets a value that is returned to the parent form when the button is clicked.
    /// Wraps the <see cref="System.Windows.Forms.Button.DialogResult"/> property.
    /// </summary>
    [DefaultValue(DialogResult.None)]
    [Category("Behavior")]
    public DialogResult DialogResult
    {
      get { return Button.DialogResult; }
      set { Button.DialogResult = value; }
    }

    /// <inheritdoc/>
    [DefaultValue(false)]
    public override bool AutoSize
    {
      get { return base.AutoSize; }
      set { base.AutoSize = value; }
    }

    /// <inheritdoc/>
    [DefaultValue(ContentAlignment.MiddleCenter)]
    public override ContentAlignment TextAlign
    {
      get { return base.TextAlign; }
      set { base.TextAlign = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool AutoFilter
    {
      get { return base.AutoFilter; }
      set { base.AutoFilter = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string DataColumn
    {
      get { return base.DataColumn; }
      set { base.DataColumn = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    public new string ReportParameter
    {
      get { return base.ReportParameter; }
      set { base.ReportParameter = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new FilterOperation FilterOperation
    {
      get { return base.FilterOperation; }
      set { base.FilterOperation = value; }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ButtonControl c = writer.DiffObject as ButtonControl;
      base.Serialize(writer);

      if (DialogResult != c.DialogResult)
        writer.WriteValue("DialogResult", DialogResult);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>ButtonControl</b> class with default settings. 
    /// </summary>
    public ButtonControl()
    {
      FButton = new Button();
      Control = FButton;
    }
  }
}
