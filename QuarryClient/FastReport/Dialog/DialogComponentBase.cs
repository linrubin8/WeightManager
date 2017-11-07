using System;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Base class for all dialog components.
  /// </summary>
  public abstract class DialogComponentBase : ComponentBase
  {
    #region Properties
    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
    /// </summary>
    [Category("Layout")]
    public Point Location
    {
      get { return new Point((int)Left, (int)Top); }
      set 
      {
        Left = value.X;
        Top = value.Y;
      }
    }

    /// <summary>
    /// Gets or sets the height and width of the control.
    /// </summary>
    [Category("Layout")]
    public Size Size
    {
      get { return new Size((int)Width, (int)Height); }
      set
      {
        Width = value.Width;
        Height = value.Height;
      }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Left
    {
      get { return base.Left; }
      set { base.Left = value; }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Top
    {
      get { return base.Top; }
      set { base.Top = value; }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Width
    {
      get { return base.Width; }
      set { base.Width = value; }
    }

    /// <inheritdoc/>
    [Browsable(false)]
    public override float Height
    {
      get { return base.Height; }
      set { base.Height = value; }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      BaseAssign(source);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>DialogComponentBase</b> class with default settings. 
    /// </summary>
    public DialogComponentBase()
    {
      if (BaseName.EndsWith("Component"))
        BaseName = ClassName.Remove(ClassName.IndexOf("Component"));
      if (BaseName.EndsWith("Control"))
        BaseName = ClassName.Remove(ClassName.IndexOf("Control"));
    }
  } 
}