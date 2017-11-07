using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Reflection;
using FastReport.Utils;
using FastReport.TypeEditors;

namespace FastReport.Dialog
{
  /// <summary>
  /// Base class for all dialog controls such as <b>ButtonControl</b>, <b>TextBoxControl</b>.
  /// </summary>
  public abstract class DialogControl : DialogComponentBase, IHasEditor
  {
    #region Fields
    private Control FControl;
    private string FClickEvent;
    private string FDoubleClickEvent;
    private string FEnterEvent;
    private string FLeaveEvent;
    private string FKeyDownEvent;
    private string FKeyPressEvent;
    private string FKeyUpEvent;
    private string FMouseDownEvent;
    private string FMouseMoveEvent;
    private string FMouseUpEvent;
    private string FMouseEnterEvent;
    private string FMouseLeaveEvent;
    private string FResizeEvent;
    private string FTextChangedEvent;
    private string FPaintEvent;
    private PropertyInfo FBindableProperty;
    #endregion

    #region Properties
    /// <summary>
    /// Occurs when the control is clicked.
    /// Wraps the <see cref="System.Windows.Forms.Control.Click"/> event.
    /// </summary>
    public event EventHandler Click;

    /// <summary>
    /// Occurs when the control is double-clicked.
    /// Wraps the <see cref="System.Windows.Forms.Control.DoubleClick"/> event.
    /// </summary>
    public event EventHandler DoubleClick;

    /// <summary>
    /// Occurs when the control is entered.
    /// Wraps the <see cref="System.Windows.Forms.Control.Enter"/> event.
    /// </summary>
    public event EventHandler Enter;

    /// <summary>
    /// Occurs when the input focus leaves the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.Leave"/> event.
    /// </summary>
    public event EventHandler Leave;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// Wraps the <see cref="System.Windows.Forms.Control.KeyDown"/> event.
    /// </summary>
    public event KeyEventHandler KeyDown;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// Wraps the <see cref="System.Windows.Forms.Control.KeyPress"/> event.
    /// </summary>
    public event KeyPressEventHandler KeyPress;

    /// <summary>
    /// Occurs when a key is released while the control has focus.
    /// Wraps the <see cref="System.Windows.Forms.Control.KeyUp"/> event.
    /// </summary>
    public event KeyEventHandler KeyUp;

    /// <summary>
    /// Occurs when the mouse pointer is over the control and a mouse button is pressed.
    /// Wraps the <see cref="System.Windows.Forms.Control.MouseDown"/> event.
    /// </summary>
    public event MouseEventHandler MouseDown;

    /// <summary>
    /// Occurs when the mouse pointer is moved over the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.MouseMove"/> event.
    /// </summary>
    public event MouseEventHandler MouseMove;

    /// <summary>
    /// Occurs when the mouse pointer is over the control and a mouse button is released.
    /// Wraps the <see cref="System.Windows.Forms.Control.MouseUp"/> event.
    /// </summary>
    public event MouseEventHandler MouseUp;

    /// <summary>
    /// Occurs when the mouse pointer enters the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.MouseEnter"/> event.
    /// </summary>
    public event EventHandler MouseEnter;

    /// <summary>
    /// Occurs when the mouse pointer leaves the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.MouseLeave"/> event.
    /// </summary>
    public event EventHandler MouseLeave;

    /// <summary>
    /// Occurs when the control is resized.
    /// Wraps the <see cref="System.Windows.Forms.Control.Resize"/> event.
    /// </summary>
    public event EventHandler Resize;

    /// <summary>
    /// Occurs when the Text property value changes.
    /// Wraps the <see cref="System.Windows.Forms.Control.TextChanged"/> event.
    /// </summary>
    public event EventHandler TextChanged;

    /// <summary>
    /// Occurs when the control is redrawn.
    /// Wraps the <see cref="System.Windows.Forms.Control.Paint"/> event.
    /// </summary>
    public event PaintEventHandler Paint;

    /// <summary>
    /// Gets an internal <b>Control</b>.
    /// </summary>
    [Browsable(false)]
    public Control Control
    {
      get { return FControl; }
      set { FControl = value; }
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.BackColor"/> property.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public virtual Color BackColor
    {
      get { return Control.BackColor; }
      set { Control.BackColor = value; }
    }

    /// <summary>
    /// Gets or sets the cursor that is displayed when the mouse pointer is over the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.Cursor"/> property.
    /// </summary>
    [Category("Appearance")]
    public Cursor Cursor
    {
      get { return Control.Cursor; }
      set { Control.Cursor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control can respond to user interaction.
    /// Wraps the <see cref="System.Windows.Forms.Control.Enabled"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Enabled
    {
      get { return Control.Enabled; }
      set 
      { 
        Control.Enabled = value;
        OnEnabledChanged();
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.Font"/> property.
    /// </summary>
    [Category("Appearance")]
    public Font Font
    {
      get { return Control.Font; }
      set { Control.Font = value; }
    }

    /// <summary>
    /// Gets or sets the foreground color of the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.ForeColor"/> property.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public virtual Color ForeColor
    {
      get { return Control.ForeColor; }
      set { Control.ForeColor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
    /// Wraps the <see cref="System.Windows.Forms.Control.RightToLeft"/> property.
    /// </summary>
    [DefaultValue(RightToLeft.No)]
    [Category("Appearance")]
    public RightToLeft RightToLeft
    {
      get { return Control.RightToLeft; }
      set { Control.RightToLeft = value; }
    }

    /// <summary>
    /// Gets or sets the tab order of the control within its container.
    /// Wraps the <see cref="System.Windows.Forms.Control.TabIndex"/> property.
    /// </summary>
    [DefaultValue(0)]
    [Category("Behavior")]
    public int TabIndex
    {
      get { return Control.TabIndex; }
      set { Control.TabIndex = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
    /// Wraps the <see cref="System.Windows.Forms.Control.TabStop"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool TabStop
    {
      get { return Control.TabStop; }
      set { Control.TabStop = value; }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// Wraps the <see cref="System.Windows.Forms.Control.Text"/> property.
    /// </summary>
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Category("Data")]
    public virtual string Text
    {
      get { return Control.Text; }
      set { Control.Text = value; }
    }

    /// <summary>
    /// Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.
    /// Wraps the <see cref="System.Windows.Forms.Control.Dock"/> property.
    /// </summary>
    [Category("Layout")]
    public override DockStyle Dock
    {
      get { return Control.Dock; }
      set { Control.Dock = value; }
    }

    /// <summary>
    /// Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.
    /// Wraps the <see cref="System.Windows.Forms.Control.Anchor"/> property.
    /// </summary>
    [Category("Layout")]
    public override AnchorStyles Anchor
    {
      get { return Control.Anchor; }
      set { Control.Anchor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is displayed.
    /// Wraps the <see cref="System.Windows.Forms.Control.Visible"/> property.
    /// </summary>
    [Category("Behavior")]
    public override bool Visible
    {
      get { return base.Visible; }
      set 
      { 
        base.Visible = value;
        Control.Visible = value; 
      }
    }

    /// <summary>
    /// Gets or sets a property that returns actual data contained in a control. This value is used
    /// in the "Data" window.
    /// </summary>
    [Browsable(false)]
    public PropertyInfo BindableProperty
    {
      get { return FBindableProperty; }
      set { FBindableProperty = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Click"/> event.
    /// </summary>
    [Category("Events")]
    public string ClickEvent
    {
      get { return FClickEvent; }
      set { FClickEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="DoubleClick"/> event.
    /// </summary>
    [Category("Events")]
    public string DoubleClickEvent
    {
      get { return FDoubleClickEvent; }
      set { FDoubleClickEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Enter"/> event.
    /// </summary>
    [Category("Events")]
    public string EnterEvent
    {
      get { return FEnterEvent; }
      set { FEnterEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Leave"/> event.
    /// </summary>
    [Category("Events")]
    public string LeaveEvent
    {
      get { return FLeaveEvent; }
      set { FLeaveEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="KeyDown"/> event.
    /// </summary>
    [Category("Events")]
    public string KeyDownEvent
    {
      get { return FKeyDownEvent; }
      set { FKeyDownEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="KeyPress"/> event.
    /// </summary>
    [Category("Events")]
    public string KeyPressEvent
    {
      get { return FKeyPressEvent; }
      set { FKeyPressEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="KeyUp"/> event.
    /// </summary>
    [Category("Events")]
    public string KeyUpEvent
    {
      get { return FKeyUpEvent; }
      set { FKeyUpEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MouseDown"/> event.
    /// </summary>
    [Category("Events")]
    public string MouseDownEvent
    {
      get { return FMouseDownEvent; }
      set { FMouseDownEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MouseMove"/> event.
    /// </summary>
    [Category("Events")]
    public string MouseMoveEvent
    {
      get { return FMouseMoveEvent; }
      set { FMouseMoveEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MouseUp"/> event.
    /// </summary>
    [Category("Events")]
    public string MouseUpEvent
    {
      get { return FMouseUpEvent; }
      set { FMouseUpEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MouseEnter"/> event.
    /// </summary>
    [Category("Events")]
    public string MouseEnterEvent
    {
      get { return FMouseEnterEvent; }
      set { FMouseEnterEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MouseLeave"/> event.
    /// </summary>
    [Category("Events")]
    public string MouseLeaveEvent
    {
      get { return FMouseLeaveEvent; }
      set { FMouseLeaveEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Resize"/> event.
    /// </summary>
    [Category("Events")]
    public string ResizeEvent
    {
      get { return FResizeEvent; }
      set { FResizeEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="TextChanged"/> event.
    /// </summary>
    [Category("Events")]
    public string TextChangedEvent
    {
      get { return FTextChangedEvent; }
      set { FTextChangedEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Paint"/> event.
    /// </summary>
    [Category("Events")]
    public string PaintEvent
    {
      get { return FPaintEvent; }
      set { FPaintEvent = value; }
    }

    /// <inheritdoc/>
    public override float Left
    {
      get { return Control.Left; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontMove))
          Control.Left = (int)value;
      }
    }

    /// <inheritdoc/>
    public override float Top
    {
      get { return Control.Top; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontMove))
          Control.Top = (int)value;
      }
    }

    /// <inheritdoc/>
    public override float Width
    {
      get { return Control.Width; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          Control.Width = (int)value;
      }
    }

    /// <inheritdoc/>
    public override float Height
    {
      get { return Control.Height; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          Control.Height = (int)value;
      }
    }
    #endregion

    #region Private Methods
    private void Control_Click(object sender, EventArgs e)
    {
      OnClick(e);
    }

    private void Control_DoubleClick(object sender, EventArgs e)
    {
      OnDoubleClick(e);
    }

    private void Control_Enter(object sender, EventArgs e)
    {
      OnEnter(e);
    }

    private void Control_Leave(object sender, EventArgs e)
    {
      OnLeave(e);
    }

    private void Control_KeyDown(object sender, KeyEventArgs e)
    {
      OnKeyDown(e);
    }

    private void Control_KeyPress(object sender, KeyPressEventArgs e)
    {
      OnKeyPress(e);
    }

    private void Control_KeyUp(object sender, KeyEventArgs e)
    {
      OnKeyUp(e);
    }

    private void Control_MouseDown(object sender, MouseEventArgs e)
    {
      OnMouseDown(e);
    }

    private void Control_MouseMove(object sender, MouseEventArgs e)
    {
      OnMouseMove(e);
    }

    private void Control_MouseUp(object sender, MouseEventArgs e)
    {
      OnMouseUp(e);
    }

    private void Control_MouseEnter(object sender, EventArgs e)
    {
      OnMouseEnter(e);
    }

    private void Control_MouseLeave(object sender, EventArgs e)
    {
      OnMouseLeave(e);
    }

    private void Control_Resize(object sender, EventArgs e)
    {
      OnResize(e);
    }

    private void Control_TextChanged(object sender, EventArgs e)
    {
      OnTextChanged(e);
    }

    private void Control_Paint(object sender, PaintEventArgs e)
    {
      OnPaint(e);
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        Control.Dispose();
    }

    /// <summary>
    /// Called when the control's Enabled state changed.
    /// </summary>
    protected virtual void OnEnabledChanged()
    {
    }
    
    /// <summary>
    /// Determines whether is necessary to serialize the <b>BackColor</b> property.
    /// </summary>
    /// <returns><b>true</b> if serialization is necessary.</returns>
    protected virtual bool ShouldSerializeBackColor()
    {
      return BackColor != SystemColors.Control;
    }

    /// <summary>
    /// Determines whether is necessary to serialize the <b>Cursor</b> property.
    /// </summary>
    /// <returns><b>true</b> if serialization is necessary.</returns>
    protected virtual bool ShouldSerializeCursor()
    {
      return Cursor != Cursors.Default;
    }

    /// <summary>
    /// Determines whether is necessary to serialize the <b>Font</b> property.
    /// </summary>
    /// <returns><b>true</b> if serialization is necessary.</returns>
    protected virtual bool ShouldSerializeFont()
    {
      return Font.Name != "Tahoma" || Font.Size != 8 || Font.Style != FontStyle.Regular;
    }

    /// <summary>
    /// Determines whether is necessary to serialize the <b>ForeColor</b> property.
    /// </summary>
    /// <returns><b>true</b> if serialization is necessary.</returns>
    protected virtual bool ShouldSerializeForeColor()
    {
      return ForeColor != SystemColors.ControlText;
    }

    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] {
        new SelectionPoint(AbsLeft - 2, AbsTop - 2, SizingPoint.LeftTop),
        new SelectionPoint(AbsLeft + Width + 1, AbsTop - 2, SizingPoint.RightTop),
        new SelectionPoint(AbsLeft - 2, AbsTop + Height + 1, SizingPoint.LeftBottom),
        new SelectionPoint(AbsLeft + Width + 1, AbsTop + Height + 1, SizingPoint.RightBottom),
        new SelectionPoint(AbsLeft + Width / 2, AbsTop - 2, SizingPoint.TopCenter),
        new SelectionPoint(AbsLeft + Width / 2, AbsTop + Height + 1, SizingPoint.BottomCenter),
        new SelectionPoint(AbsLeft - 2, AbsTop + Height / 2, SizingPoint.LeftCenter),
        new SelectionPoint(AbsLeft + Width + 1, AbsTop + Height / 2, SizingPoint.RightCenter) };
    }

    /// <summary>
    /// Draws the selection point.
    /// </summary>
    /// <param name="g"><b>Graphics</b> object to draw on.</param>
    /// <param name="p"><see cref="Pen"/> object.</param>
    /// <param name="b"><see cref="Brush"/> object.</param>
    /// <param name="x">Left coordinate.</param>
    /// <param name="y">Top coordinate.</param>
    protected void DrawSelectionPoint(Graphics g, Pen p, Brush b, float x, float y)
    {
      x = (int)x;
      y = (int)y;
      g.FillRectangle(b, x - 2, y - 2, 5, 5);
      g.DrawLine(p, x - 2, y - 3, x + 2, y - 3);
      g.DrawLine(p, x - 2, y + 3, x + 2, y + 3);
      g.DrawLine(p, x - 3, y - 2, x - 3, y + 2);
      g.DrawLine(p, x + 3, y - 2, x + 3, y + 2);
    }

    /// <summary>
    /// Attaches <b>Control</b> events to its event handlers.
    /// </summary>
    /// <remarks>
    /// Override this method if your custom control has own events.
    /// </remarks>
    /// <example>See the example of <b>AttachEvents</b> implementation used in the <b>CheckBoxControl</b>:
    /// <code>
    /// protected override void AttachEvents()
    /// {
    ///   base.AttachEvents();
    ///   CheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
    /// }
    /// 
    /// private void CheckBox_CheckedChanged(object sender, EventArgs e)
    /// {
    ///   if (CheckedChanged != null)
    ///     CheckedChanged(this, e);
    ///   InvokeEvent(CheckedChangedEvent, e);
    /// }
    /// </code>
    /// </example>
    protected virtual void AttachEvents()
    {
      Control.Click += new EventHandler(Control_Click);
      Control.DoubleClick += new EventHandler(Control_DoubleClick);
      Control.Enter += new EventHandler(Control_Enter);
      Control.Leave += new EventHandler(Control_Leave);
      Control.KeyDown += new KeyEventHandler(Control_KeyDown);
      Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
      Control.KeyUp += new KeyEventHandler(Control_KeyUp);
      Control.MouseDown += new MouseEventHandler(Control_MouseDown);
      Control.MouseMove += new MouseEventHandler(Control_MouseMove);
      Control.MouseUp += new MouseEventHandler(Control_MouseUp);
      Control.MouseEnter += new EventHandler(Control_MouseEnter);
      Control.MouseLeave += new EventHandler(Control_MouseLeave);
      Control.Resize += new EventHandler(Control_Resize);
      Control.TextChanged += new EventHandler(Control_TextChanged);
      Control.Paint += new PaintEventHandler(Control_Paint);
    }

    /// <summary>
    /// Detaches <b>Control</b> events from its event handlers.
    /// </summary>
    /// <remarks>
    /// Override this method if your custom control has own events. In this method, you should
    /// detach control's events that were attached in the <see cref="AttachEvents"/> method.
    /// </remarks>
    /// <example>See the example of <b>DetachEvents</b> implementation used in the <b>CheckBoxControl</b>:
    /// <code>
    /// protected override void DetachEvents()
    /// {
    ///   base.DetachEvents();
    ///   CheckBox.CheckedChanged -= new EventHandler(CheckBox_CheckedChanged);
    /// }
    /// </code>
    /// </example>
    protected virtual void DetachEvents()
    {
      Control.Click -= new EventHandler(Control_Click);
      Control.DoubleClick -= new EventHandler(Control_DoubleClick);
      Control.Enter -= new EventHandler(Control_Enter);
      Control.Leave -= new EventHandler(Control_Leave);
      Control.KeyDown -= new KeyEventHandler(Control_KeyDown);
      Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
      Control.KeyUp -= new KeyEventHandler(Control_KeyUp);
      Control.MouseDown -= new MouseEventHandler(Control_MouseDown);
      Control.MouseMove -= new MouseEventHandler(Control_MouseMove);
      Control.MouseUp -= new MouseEventHandler(Control_MouseUp);
      Control.MouseEnter -= new EventHandler(Control_MouseEnter);
      Control.MouseLeave -= new EventHandler(Control_MouseLeave);
      Control.Resize -= new EventHandler(Control_Resize);
      Control.TextChanged -= new EventHandler(Control_TextChanged);
      Control.Paint -= new PaintEventHandler(Control_Paint);
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void SetParent(Base value)
    {
      base.SetParent(value);
      if (Parent is DialogPage)
        Control.Parent = (Parent as DialogPage).Form;
      else if (Parent is ParentControl)
        Control.Parent = (Parent as ParentControl).Control;
      else if (Parent == null)
        Control.Parent = null;

      // in winforms, the controls are added in opposite order
      if (Control.Parent != null)
        Control.BringToFront();
    }

    /// <inheritdoc/>
    public override void CheckParent(bool immediately)
    {
      if (!IsSelected || IsAncestor || !HasFlag(Flags.CanChangeParent))
        return;

      if (immediately || (!(Parent is DialogPage) && (
        Left < 0 || Left > (Parent as ComponentBase).Width || Top < 0 || Top > (Parent as ComponentBase).Height)))
      {
        ObjectCollection list = Page.AllObjects;
        int i = 0;
        while (i < list.Count)
        {
          if (list[i] is ParentControl && list[i] != this)
            i++;
          else
            list.RemoveAt(i);
        }
        list.Insert(0, Page);
        
        for (i = list.Count - 1; i >= 0; i--)
        {
          ComponentBase c = list[i] as ComponentBase;
          if ((c as IParent).CanContain(this))
            if (AbsLeft > c.AbsLeft - 1e-4 && AbsLeft < c.AbsRight - 1e-4 &&
              AbsTop > c.AbsTop - 1e-4 && AbsTop < c.AbsBottom - 1e-4)
            {
              if (Parent != c)
              {
                Left = (int)Math.Round((AbsLeft - c.AbsLeft) / Page.SnapSize.Width) * Page.SnapSize.Width;
                Top = (int)Math.Round((AbsTop - c.AbsTop) / Page.SnapSize.Height) * Page.SnapSize.Height;
                Parent = c;
              }
              break;
            }
        }
      }  
    }

    /// <inheritdoc/>
    public override void DrawSelection(FRPaintEventArgs e)
    {
      Graphics g = e.Graphics;
      bool firstSelected = Report.Designer.SelectedObjects.IndexOf(this) == 0;
      Pen p = firstSelected ? Pens.Black : Pens.White;
      Brush b = firstSelected ? Brushes.White : Brushes.Black;
      SelectionPoint[] selectionPoints = GetSelectionPoints();

      Pen pen = e.Cache.GetPen(Color.Gray, 1, DashStyle.Dot);
      g.DrawRectangle(pen, AbsLeft - 2, AbsTop - 2, Width + 3, Height + 3);
      if (selectionPoints.Length == 1)
        DrawSelectionPoint(e, p, b, selectionPoints[0].X, selectionPoints[0].Y);
      else
      {
        foreach (SelectionPoint pt in selectionPoints)
        {
          DrawSelectionPoint(e, p, b, pt.X, pt.Y);
        }
      }  
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (Control.Width > 0 && Control.Height > 0)
      {
        using (Bitmap bmp = DrawUtils.DrawToBitmap(Control, true))
        {
          e.Graphics.DrawImage(bmp, (int)AbsLeft, (int)AbsTop);
        }
      }
      base.Draw(e);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      DialogControl c = writer.DiffObject as DialogControl;
      base.Serialize(writer);

      if (BackColor != c.BackColor)
        writer.WriteValue("BackColor", BackColor);
      if (Cursor != c.Cursor)
        writer.WriteValue("Cursor", Cursor);
      if (Enabled != c.Enabled)
        writer.WriteBool("Enabled", Enabled);
      if (!Font.Equals(c.Font))
        writer.WriteValue("Font", Font);
      if (ForeColor != c.ForeColor)
        writer.WriteValue("ForeColor", ForeColor);
      if (RightToLeft != c.RightToLeft)
        writer.WriteValue("RightToLeft", RightToLeft);
      writer.WriteInt("TabIndex", TabIndex);
      if (TabStop != c.TabStop)
        writer.WriteBool("TabStop", TabStop);
      if (Text != c.Text)
        writer.WriteStr("Text", Text);

      if (ClickEvent != c.ClickEvent)
        writer.WriteStr("ClickEvent", ClickEvent);
      if (DoubleClickEvent != c.DoubleClickEvent)
        writer.WriteStr("DoubleClickEvent", DoubleClickEvent);
      if (EnterEvent != c.EnterEvent)
        writer.WriteStr("EnterEvent", EnterEvent);
      if (LeaveEvent != c.LeaveEvent)
        writer.WriteStr("LeaveEvent", LeaveEvent);
      if (KeyDownEvent != c.KeyDownEvent)
        writer.WriteStr("KeyDownEvent", KeyDownEvent);
      if (KeyPressEvent != c.KeyPressEvent)
        writer.WriteStr("KeyPressEvent", KeyPressEvent);
      if (KeyUpEvent != c.KeyUpEvent)
        writer.WriteStr("KeyUpEvent", KeyUpEvent);
      if (MouseDownEvent != c.MouseDownEvent)
        writer.WriteStr("MouseDownEvent", MouseDownEvent);
      if (MouseMoveEvent != c.MouseMoveEvent)
        writer.WriteStr("MouseMoveEvent", MouseMoveEvent);
      if (MouseUpEvent != c.MouseUpEvent)
        writer.WriteStr("MouseUpEvent", MouseUpEvent);
      if (MouseEnterEvent != c.MouseEnterEvent)
        writer.WriteStr("MouseEnterEvent", MouseEnterEvent);
      if (MouseLeaveEvent != c.MouseLeaveEvent)
        writer.WriteStr("MouseLeaveEvent", MouseLeaveEvent);
      if (ResizeEvent != c.ResizeEvent)
        writer.WriteStr("ResizeEvent", ResizeEvent);
      if (TextChangedEvent != c.TextChangedEvent)
        writer.WriteStr("TextChangedEvent", TextChangedEvent);
      if (PaintEvent != c.PaintEvent)
        writer.WriteStr("PaintEvent", PaintEvent);
    }

    /// <summary>
    /// Initializes the control before display it in the dialog form.
    /// </summary>
    /// <remarks>
    /// This method is called when report is run.
    /// </remarks>
    public virtual void InitializeControl()
    {
      AttachEvents();
    }
    
    /// <summary>
    /// Finalizes the control after its parent form is closed.
    /// </summary>
    /// <remarks>
    /// This method is called when report is run.
    /// </remarks>
    public virtual void FinalizeControl()
    {
      DetachEvents();
    }
    
    /// <summary>
    /// Creates the empty event handler for the <b>ClickEvent</b> event in the report's script.
    /// </summary>
    /// <returns><b>true</b> if event handler was created successfully.</returns>
    public bool InvokeEditor()
    {
      Report.Designer.ActiveReportTab.SwitchToCode();
      if (String.IsNullOrEmpty(ClickEvent))
      {
        string newEventName = Name + "_Click";
        if (Report.CodeHelper.AddHandler(typeof(EventHandler), newEventName))
        {
          ClickEvent = newEventName;
          return true;
        }
        else
          return false;
      }
      else
      {
        Report.CodeHelper.LocateHandler(ClickEvent);
      }
      return false;
    }
    
    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    public void Focus()
    {
      Control.Focus();
    }
    
    /// <summary>
    /// Conceals the control from the user.
    /// </summary>
    public void Hide()
    {
      Control.Hide();
    }
    
    /// <summary>
    /// Displays the control to the user.
    /// </summary>
    public void Show()
    {
      Control.Show();
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      base.OnBeforeInsert(flags);
      try
      {
        Text = BaseName;
      }
      catch
      {
      }
    }
    #endregion

    #region Invoke Events
    /// <summary>
    /// This method fires the <b>Click</b> event and the script code connected to the <b>ClickEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnClick(EventArgs e)
    {
      if (Click != null)
        Click(this, e);
      InvokeEvent(ClickEvent, e);
    }

    /// <summary>
    /// This method fires the <b>DoubleClick</b> event and the script code connected to the <b>DoubleClickEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnDoubleClick(EventArgs e)
    {
      if (DoubleClick != null)
        DoubleClick(this, e);
      InvokeEvent(DoubleClickEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Enter</b> event and the script code connected to the <b>EnterEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnEnter(EventArgs e)
    {
      if (Enter != null)
        Enter(this, e);
      InvokeEvent(EnterEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Leave</b> event and the script code connected to the <b>LeaveEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnLeave(EventArgs e)
    {
      if (Leave != null)
        Leave(this, e);
      InvokeEvent(LeaveEvent, e);
    }

    /// <summary>
    /// This method fires the <b>KeyDown</b> event and the script code connected to the <b>KeyDownEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnKeyDown(KeyEventArgs e)
    {
      if (KeyDown != null)
        KeyDown(this, e);
      InvokeEvent(KeyDownEvent, e);
    }

    /// <summary>
    /// This method fires the <b>KeyPress</b> event and the script code connected to the <b>KeyPressEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnKeyPress(KeyPressEventArgs e)
    {
      if (KeyPress != null)
        KeyPress(this, e);
      InvokeEvent(KeyPressEvent, e);
    }

    /// <summary>
    /// This method fires the <b>KeyUp</b> event and the script code connected to the <b>KeyUpEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnKeyUp(KeyEventArgs e)
    {
      if (KeyUp != null)
        KeyUp(this, e);
      InvokeEvent(KeyUpEvent, e);
    }

    /// <summary>
    /// This method fires the <b>MouseDown</b> event and the script code connected to the <b>MouseDownEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseDown(MouseEventArgs e)
    {
      if (MouseDown != null)
        MouseDown(this, e);
      InvokeEvent(MouseDownEvent, e);
    }

    /// <summary>
    /// This method fires the <b>MouseMove</b> event and the script code connected to the <b>MouseMoveEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseMove(MouseEventArgs e)
    {
      if (MouseMove != null)
        MouseMove(this, e);
      InvokeEvent(MouseMoveEvent, e);
    }

    /// <summary>
    /// This method fires the <b>MouseUp</b> event and the script code connected to the <b>MouseUpEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseUp(MouseEventArgs e)
    {
      if (MouseUp != null)
        MouseUp(this, e);
      InvokeEvent(MouseUpEvent, e);
    }

    /// <summary>
    /// This method fires the <b>MouseEnter</b> event and the script code connected to the <b>MouseEnterEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseEnter(EventArgs e)
    {
      if (MouseEnter != null)
        MouseEnter(this, e);
      InvokeEvent(MouseEnterEvent, e);
    }

    /// <summary>
    /// This method fires the <b>MouseLeave</b> event and the script code connected to the <b>MouseLeaveEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseLeave(EventArgs e)
    {
      if (MouseLeave != null)
        MouseLeave(this, e);
      InvokeEvent(MouseLeaveEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Resize</b> event and the script code connected to the <b>ResizeEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnResize(EventArgs e)
    {
      if (Resize != null)
        Resize(this, e);
      InvokeEvent(ResizeEvent, e);
    }

    /// <summary>
    /// This method fires the <b>TextChanged</b> event and the script code connected to the <b>TextChangedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnTextChanged(EventArgs e)
    {
      if (TextChanged != null)
        TextChanged(this, e);
      InvokeEvent(TextChangedEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Paint</b> event and the script code connected to the <b>PaintEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnPaint(PaintEventArgs e)
    {
      if (Paint != null)
        Paint(this, e);
      InvokeEvent(PaintEvent, e);
    }
    #endregion
  }
}