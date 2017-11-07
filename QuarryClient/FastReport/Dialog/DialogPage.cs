using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using FastReport.Design.PageDesigners.Dialog;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents the special kind of report page that wraps the <see cref="System.Windows.Forms.Form"/>
  /// and used to display dialog forms.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="Controls"/> property to add/remove controls to/from a dialog form.
  /// <para/>If you set the <b>Visible</b> property to <b>false</b>, this dialog form will be
  /// skippen when you run a report.
  /// </remarks>
  /// <example>This example shows how to create a dialog form with one button in code.
  /// <code>
  /// DialogPage form = new DialogPage();
  /// // set the width and height in pixels
  /// form.Width = 200;
  /// form.Height = 200;
  /// form.Name = "Form1";
  /// // create a button
  /// ButtonControl button = new ButtonControl();
  /// button.Location = new Point(20, 20);
  /// button.Size = new Size(75, 25);
  /// button.Text = "The button";
  /// // add the button to the form
  /// form.Controls.Add(button);
  /// </code>
  /// </example>
  public class DialogPage : PageBase, IParent
  {
    #region Fields
    private ButtonControl FAcceptButton;
    private ButtonControl FCancelButton;
    private Form FForm;
    private DialogComponentCollection FControls;
    private Bitmap FFormBitmap;
    private string FLoadEvent;
    private string FFormClosedEvent;
    private string FFormClosingEvent;
    private string FShownEvent;
    private string FResizeEvent;
    private string FPaintEvent;
    private DialogControl FErrorControl;
    private Color FErrorControlBackColor;
    private Timer FErrorControlTimer;
    private int FErrorControlTimerTickCount;
    private bool FActiveInWeb;
    #endregion

    #region Properties
    /// <summary>
    /// Occurs before a form is displayed for the first time.
    /// Wraps the <see cref="System.Windows.Forms.Form.Load"/> event.
    /// </summary>
    public event EventHandler Load;

    /// <summary>
    /// Occurs after the form is closed.
    /// Wraps the <see cref="System.Windows.Forms.Form.FormClosed"/> event.
    /// </summary>
    public event FormClosedEventHandler FormClosed;

    /// <summary>
    /// Occurs before the form is closed.
    /// Wraps the <see cref="System.Windows.Forms.Form.FormClosing"/> event.
    /// </summary>
    public event FormClosingEventHandler FormClosing;

    /// <summary>
    /// Occurs whenever the form is first displayed.
    /// Wraps the <see cref="System.Windows.Forms.Form.Shown"/> event.
    /// </summary>
    public event EventHandler Shown;

    /// <summary>
    /// Occurs when the form is resized.
    /// Wraps the <see cref="System.Windows.Forms.Control.Resize"/> event.
    /// </summary>
    public event EventHandler Resize;

    /// <summary>
    /// Occurs when the form is redrawn.
    /// Wraps the <see cref="System.Windows.Forms.Control.Paint"/> event.
    /// </summary>
    public event PaintEventHandler Paint;

    /// <summary>
    /// Gets an internal <b>Form</b>.
    /// </summary>
    [Browsable(false)]
    public Form Form
    {
      get { return FForm; }
    }

    /// <summary>
    /// Gets or sets an active state in Web application.
    /// </summary>
    [Browsable(false)]
    public bool ActiveInWeb
    {
        get { return FActiveInWeb; }
        set { FActiveInWeb = value; }
    }

    /// <summary>
    /// Gets or sets the button on the form that is clicked when the user presses the ENTER key.
    /// Wraps the <see cref="System.Windows.Forms.Form.AcceptButton"/> property.
    /// </summary>
    [TypeConverter(typeof(ComponentRefConverter))]
    [Editor(typeof(PageComponentRefEditor), typeof(UITypeEditor))]
    [Category("Misc")]
    public ButtonControl AcceptButton
    {
      get { return FAcceptButton; }
      set 
      {
        if (FAcceptButton != value)
        {
          if (FAcceptButton != null)
            FAcceptButton.Disposed -= new EventHandler(AcceptButton_Disposed);
          if (value != null)
            value.Disposed += new EventHandler(AcceptButton_Disposed);
        }
        FAcceptButton = value;
        Form.AcceptButton = value == null ? null : value.Button;
      }
    }

    /// <summary>
    /// Gets or sets the button control that is clicked when the user presses the ESC key.
    /// Wraps the <see cref="System.Windows.Forms.Form.CancelButton"/> property.
    /// </summary>
    [TypeConverter(typeof(ComponentRefConverter))]
    [Editor(typeof(PageComponentRefEditor), typeof(UITypeEditor))]
    [Category("Misc")]
    public ButtonControl CancelButton
    {
      get { return FCancelButton; }
      set 
      {
        if (FCancelButton != value)
        {
          if (FCancelButton != null)
            FCancelButton.Disposed -= new EventHandler(CancelButton_Disposed);
          if (value != null)
            value.Disposed += new EventHandler(CancelButton_Disposed);
        }
        FCancelButton = value; 
        Form.CancelButton = value == null ? null : value.Button;
      }
    }

    /// <summary>
    /// Gets or sets the background color for the form.
    /// Wraps the <see cref="System.Windows.Forms.Form.BackColor"/> property.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public Color BackColor
    {
      get { return Form.BackColor; }
      set 
      { 
        Form.BackColor = value; 
        ResetFormBitmap();
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// Wraps the <see cref="System.Windows.Forms.Control.Font"/> property.
    /// </summary>
    [Category("Appearance")]
    public Font Font
    {
      get { return Form.Font; }
      set { Form.Font = value; }
    }

    /// <summary>
    /// Gets or sets the border style of the form.
    /// Wraps the <see cref="System.Windows.Forms.Form.FormBorderStyle"/> property.
    /// </summary>
    [DefaultValue(FormBorderStyle.FixedDialog)]
    [Category("Appearance")]
    public FormBorderStyle FormBorderStyle
    {
      get { return Form.FormBorderStyle; }
      set 
      { 
        Form.FormBorderStyle = value;
        ResetFormBitmap();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
    /// Wraps the <see cref="System.Windows.Forms.Control.RightToLeft"/> property.
    /// </summary>
    [DefaultValue(RightToLeft.No)]
    [Category("Appearance")]
    public RightToLeft RightToLeft
    {
      get { return Form.RightToLeft; }
      set 
      { 
        Form.RightToLeft = value;
        ResetFormBitmap();
      }
    }

    /// <summary>
    /// Gets or sets the text associated with this form.
    /// Wraps the <see cref="System.Windows.Forms.Form.Text"/> property.
    /// </summary>
    [Category("Appearance")]
    public string Text
    {
      get { return Form.Text; }
      set 
      { 
        Form.Text = value;
        ResetFormBitmap();
      }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Load"/> event.
    /// </summary>
    [Category("Events")]
    public string LoadEvent
    {
      get { return FLoadEvent; }
      set { FLoadEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="FormClosed"/> event.
    /// </summary>
    [Category("Events")]
    public string FormClosedEvent
    {
      get { return FFormClosedEvent; }
      set { FFormClosedEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="FormClosing"/> event.
    /// </summary>
    [Category("Events")]
    public string FormClosingEvent
    {
      get { return FFormClosingEvent; }
      set { FFormClosingEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="Shown"/> event.
    /// </summary>
    [Category("Events")]
    public string ShownEvent
    {
      get { return FShownEvent; }
      set { FShownEvent = value; }
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
    /// <see cref="Paint"/> event.
    /// </summary>
    [Category("Events")]
    public string PaintEvent
    {
      get { return FPaintEvent; }
      set { FPaintEvent = value; }
    }

    /// <summary>
    /// Gets the collection of controls contained within the form.
    /// </summary>
    [Browsable(false)]
    public DialogComponentCollection Controls
    {
      get { return FControls; }
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
    public override float Width
    {
      get { return Form.Width; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          Form.Width = (int)value;
        ResetFormBitmap();
      }
    }

    /// <inheritdoc/>
    public override float Height
    {
      get { return Form.Height; }
      set
      {
        if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          Form.Height = (int)value;
        ResetFormBitmap();
      }
    }

    /// <inheritdoc/>
    public override SizeF SnapSize
    {
      get { return new SizeF(DialogWorkspace.Grid.SnapSize, DialogWorkspace.Grid.SnapSize); }
    }

    /// <inheritdoc/>
    public override SizeF ClientSize
    {
      get { return new SizeF(Form.ClientSize.Width, Form.ClientSize.Height); }
      set { Form.ClientSize = new Size((int)value.Width, (int)value.Height); }
    }
    
    [Browsable(false)]
    internal Bitmap FormBitmap
    {
      get 
      { 
        if (FFormBitmap == null)
          ResetFormBitmap();
        return FFormBitmap; 
      }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeBackColor()
    {
      return BackColor != SystemColors.Control;
    }

    private bool ShouldSerializeFont()
    {
      return Font.Name != "Tahoma" || Font.Size != 8 || Font.Style != FontStyle.Regular;
    }

    private Point Offset()
    {
      Point offset = new Point(0, 0);
      offset = Form.PointToScreen(offset);
      offset.X -= Form.Left;
      offset.Y -= Form.Top;
      return offset;
    }

    private void ResetFormBitmap()
    {
      if (FFormBitmap != null)
      {
        FFormBitmap.Dispose();
        FFormBitmap = null;
      }
      if (!IsDesigning)
        return;

      FFormBitmap = DrawUtils.DrawToBitmap(Form, false);
      // WinXP form has round edges which are filled black (DrawToBitmap issue/bug).
      DrawUtils.FloodFill(FFormBitmap, 0, 0, Color.FromArgb(0, 0, 0), Color.White);
      DrawUtils.FloodFill(FFormBitmap, FFormBitmap.Width - 1, 0, Color.FromArgb(0, 0, 0), Color.White);
    }

    private void DrawSelectionPoint(Graphics g, float x, float y)
    {
      x = (int)x;
      y = (int)y;
      Pen p = Pens.Black;
      g.FillRectangle(Brushes.White, x - 2, y - 2, 5, 5);
      g.DrawLine(p, x - 2, y - 3, x + 2, y - 3);
      g.DrawLine(p, x - 2, y + 3, x + 2, y + 3);
      g.DrawLine(p, x - 3, y - 2, x - 3, y + 2);
      g.DrawLine(p, x + 3, y - 2, x + 3, y + 2);
    }

    private string CreateButtonName(string baseName)
    {
      if (Report.FindObject(baseName) == null)
        return baseName;

      int i = 1;
      while (Report.FindObject(baseName + i.ToString()) != null)
      {
        i++;
      }
      return baseName + i.ToString();
    }

    private void AcceptButton_Disposed(object sender, EventArgs e)
    {
      AcceptButton = null;
    }

    private void CancelButton_Disposed(object sender, EventArgs e)
    {
      CancelButton = null;
    }

    private void Form_Load(object sender, EventArgs e)
    {
      OnLoad(e);
    }

    private void Form_FormClosed(object sender, FormClosedEventArgs e)
    {
      OnFormClosed(e);
    }

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
      OnFormClosing(e);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      OnShown(e);
    }

    private void Form_Resize(object sender, EventArgs e)
    {
      OnResize(e);
    }

    private void Form_Paint(object sender, PaintEventArgs e)
    {
      OnPaint(e);
    }

    private void SetErrorControl(DialogControl control)
    {
      FErrorControl = control;
      if (control != null)
      {
        control.Focus();
        if (FErrorControlTimer == null)
        {
          FErrorControlTimerTickCount = 0;
          FErrorControlBackColor = FErrorControl.BackColor;
          FErrorControlTimer = new Timer();
          FErrorControlTimer.Interval = 300;
          FErrorControlTimer.Tick += new EventHandler(FErrorControlTimer_Tick);
          FErrorControlTimer.Start();
        }
      }
    }

    private void FErrorControlTimer_Tick(object sender, EventArgs e)
    {
      FErrorControl.BackColor = FErrorControlTimerTickCount % 2 == 0 ? Color.Red : FErrorControlBackColor;

      FErrorControlTimerTickCount++;
      if (FErrorControlTimerTickCount > 5)
      {
        FErrorControlTimer.Stop();
        FErrorControlTimer.Dispose();
        FErrorControlTimer = null;
      }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        Form.Dispose();
    }

    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] {
        new SelectionPoint(Width - Offset().X + 1, Height - Offset().Y + 1, SizingPoint.RightBottom),
        new SelectionPoint(Width / 2 - Offset().X, Height - Offset().Y + 1, SizingPoint.BottomCenter),
        new SelectionPoint(Width - Offset().X + 1, Height / 2 - Offset().Y, SizingPoint.RightCenter) };
    }
    #endregion

    #region IParent
    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      foreach (DialogComponentBase c in FControls)
      {
        list.Add(c);
      }
    }

    /// <inheritdoc/>
    public virtual bool CanContain(Base child)
    {
      return (child is DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void AddChild(Base child)
    {
      if (child is DialogComponentBase)
        FControls.Add(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void RemoveChild(Base child)
    {
      if (child is DialogComponentBase)
        FControls.Remove(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual int GetChildOrder(Base child)
    {
      return FControls.IndexOf(child as DialogComponentBase);
    }

    /// <inheritdoc/>
    public virtual void SetChildOrder(Base child, int order)
    {
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (order > FControls.Count)
          order = FControls.Count;
        if (oldOrder <= order)
          order--;
        FControls.Remove(child as DialogComponentBase);
        FControls.Insert(order, child as DialogComponentBase);
      }
    }

    /// <inheritdoc/>
    public virtual void UpdateLayout(float dx, float dy)
    {
      // do nothing
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      BaseAssign(source);
    }

    /// <inheritdoc/>
    public override Type GetPageDesignerType()
    {
      return typeof(DialogPageDesigner);
    }

    /// <inheritdoc/>
    public override void DrawSelection(FRPaintEventArgs e)
    {
      Pen pen = e.Cache.GetPen(Color.Gray, 1, DashStyle.Dot);
      e.Graphics.DrawRectangle(pen, -Offset().X - 2, -Offset().Y - 2, Width + 3, Height + 3);
      SelectionPoint[] selectionPoints = GetSelectionPoints();
      foreach (SelectionPoint pt in selectionPoints)
      {
        DrawSelectionPoint(e.Graphics, pt.X, pt.Y);
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      e.Handled = true;
      e.Mode = WorkspaceMode2.SelectionRect;
      e.ActiveObject = this;
    }

    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      base.HandleMouseHover(e);
      if (e.Handled)
        e.Cursor = Cursors.Default;
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (e.Mode == WorkspaceMode2.SelectionRect)
      {
        SelectedObjectCollection selection = Report.Designer.SelectedObjects;
        selection.Clear();
        // find objects inside the selection rect
        foreach (DialogComponentBase c in Controls)
        {
          e.Handled = false;
          c.HandleMouseUp(e);
          // object is inside
          if (e.Handled)
            selection.Add(c);
        }
        if (selection.Count == 0)
          selection.Add(this);
      }
    }

    /// <inheritdoc/>
    public override void HandleDoubleClick()
    {
      Report.Designer.ActiveReportTab.SwitchToCode();
      if (String.IsNullOrEmpty(LoadEvent))
      {
        string newEventName = Name + "_Load";
        if (Report.CodeHelper.AddHandler(typeof(EventHandler), newEventName))
        {
          LoadEvent = newEventName;
          Report.Designer.SetModified(null, "Change");
        }
      }
      else
      {
        Report.CodeHelper.LocateHandler(LoadEvent);
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      DialogPage c = writer.DiffObject as DialogPage;
      base.Serialize(writer);

      if (AcceptButton != c.AcceptButton)
        writer.WriteRef("AcceptButton", AcceptButton);
      if (CancelButton != c.CancelButton)
        writer.WriteRef("CancelButton", CancelButton);
      if (BackColor != c.BackColor)
        writer.WriteValue("BackColor", BackColor);
      if (!Font.Equals(c.Font))
        writer.WriteValue("Font", Font);
      if (FormBorderStyle != c.FormBorderStyle)
        writer.WriteValue("FormBorderStyle", FormBorderStyle);
      if (RightToLeft != c.RightToLeft)
        writer.WriteValue("RightToLeft", RightToLeft);
      if (Text != c.Text)
        writer.WriteStr("Text", Text);

      if (LoadEvent != c.LoadEvent)
        writer.WriteStr("LoadEvent", LoadEvent);
      if (FormClosedEvent != c.FormClosedEvent)
        writer.WriteStr("FormClosedEvent", FormClosedEvent);
      if (FormClosingEvent != c.FormClosingEvent)
        writer.WriteStr("FormClosingEvent", FormClosingEvent);
      if (ShownEvent != c.ShownEvent)
        writer.WriteStr("ShownEvent", ShownEvent);
      if (ResizeEvent != c.ResizeEvent)
        writer.WriteStr("ResizeEvent", ResizeEvent);
      if (PaintEvent != c.PaintEvent)
        writer.WriteStr("PaintEvent", PaintEvent);
    }
    
    internal void InitializeControls()
    {
      Form.Hide();
      Form.StartPosition = FormStartPosition.CenterScreen;
      Form.Load += new EventHandler(Form_Load);
      Form.FormClosed += new FormClosedEventHandler(Form_FormClosed);
      Form.FormClosing += new FormClosingEventHandler(Form_FormClosing);
      Form.Shown += new EventHandler(Form_Shown);
      Form.Resize += new EventHandler(Form_Resize);
      Form.Paint += new PaintEventHandler(Form_Paint);
      
      ObjectCollection allObjects = AllObjects;
      foreach (Base c in allObjects)
      {
        if (c is DialogControl)
          (c as DialogControl).InitializeControl();
      }
    }

    internal void FinalizeControls()
    {
      Form.Load -= new EventHandler(Form_Load);
      Form.FormClosed -= new FormClosedEventHandler(Form_FormClosed);
      Form.FormClosing -= new FormClosingEventHandler(Form_FormClosing);
      Form.Shown -= new EventHandler(Form_Shown);
      Form.Resize -= new EventHandler(Form_Resize);
      Form.Paint -= new PaintEventHandler(Form_Paint);

      ObjectCollection allObjects = AllObjects;
      foreach (Base c in allObjects)
      {
        if (c is DialogControl)
          (c as DialogControl).FinalizeControl();
      }
    }
    
    /// <summary>
    /// Shows the form as a modal dialog box with the currently active window set as its owner.
    /// Wraps the <see cref="System.Windows.Forms.Form.ShowDialog()"/> method.
    /// </summary>
    /// <returns>One of the <b>DialogResult</b> values.</returns>
    public DialogResult ShowDialog()
    {
      try
      {
        InitializeControls();
        return Form.ShowDialog();
      }
      finally
      {
        FinalizeControls();
      }
    }

    /// <inheritdoc/>
    public override void SetDefaults()
    {
      ButtonControl btnOk = new ButtonControl();
      btnOk.Parent = this;
      btnOk.Name = CreateButtonName("btnOk");
      btnOk.Text = Res.Get("Buttons,OK");
      btnOk.Location = new Point((int)ClientSize.Width - 166, (int)ClientSize.Height - 31);
      btnOk.Size = new Size(75, 23);
      btnOk.DialogResult = DialogResult.OK;
      btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      ButtonControl btnCancel = new ButtonControl();
      btnCancel.Parent = this;
      btnCancel.Name = CreateButtonName("btnCancel");
      btnCancel.Text = Res.Get("Buttons,Cancel");
      btnCancel.Location = new Point((int)ClientSize.Width - 83, (int)ClientSize.Height - 31);
      btnCancel.Size = new Size(75, 23);
      btnCancel.DialogResult = DialogResult.Cancel;
      btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      AcceptButton = btnOk;
      CancelButton = btnCancel;
      
      base.SetDefaults();
    }

    /// <summary>
    /// This method fires the <b>Load</b> event and the script code connected to the <b>LoadEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnLoad(EventArgs e)
    {
      if (Load != null)
        Load(this, e);
      InvokeEvent(LoadEvent, e);
    }

    /// <summary>
    /// This method fires the <b>FormClosed</b> event and the script code connected to the <b>FormClosedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnFormClosed(FormClosedEventArgs e)
    {
      if (FormClosed != null)
        FormClosed(this, e);
      InvokeEvent(FormClosedEvent, e);
    }

    /// <summary>
    /// This method fires the <b>FormClosing</b> event and the script code connected to the <b>FormClosingEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnFormClosing(FormClosingEventArgs e)
    {
      if (FForm.DialogResult == DialogResult.OK)
      {
        // filter data
        SetErrorControl(null);
        foreach (Base c in AllObjects)
        {
          DataFilterBaseControl c1 = c as DataFilterBaseControl;
          if (c1 != null && c1.Enabled)
          {
            try
            {
              if (c1.AutoFilter)
                c1.FilterData();
              c1.SetReportParameter();
            }
            catch
            {
              SetErrorControl(c1);
            }
          }

          if (FErrorControl != null)
          {
            e.Cancel = true;
            break;
          }
        }
      }

      if (FormClosing != null)
        FormClosing(this, e);
      InvokeEvent(FormClosingEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Shown</b> event and the script code connected to the <b>ShownEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnShown(EventArgs e)
    {
      if (Shown != null)
        Shown(this, e);
      InvokeEvent(ShownEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Resize</b> event and the script code connected to the <b>ResizeEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnResize(EventArgs e)
    {
      if (Resize != null)
        Resize(this, e);
      InvokeEvent(ResizeEvent, e);
    }

    /// <summary>
    /// This method fires the <b>Paint</b> event and the script code connected to the <b>PaintEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnPaint(PaintEventArgs e)
    {
      if (Paint != null)
        Paint(this, e);
      InvokeEvent(PaintEvent, e);
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>DialogPage</b> class. 
    /// </summary>
    public DialogPage()
    {
      FControls = new DialogComponentCollection(this);
      FForm = new Form();
      FForm.ShowIcon = false;
      FForm.ShowInTaskbar = false;
      FForm.FormBorderStyle = FormBorderStyle.FixedDialog;
      FForm.MinimizeBox = false;
      FForm.MaximizeBox = false;
      FForm.Font = DrawUtils.DefaultFont;
      FActiveInWeb = false;
      BaseName = "Form";
    }
  }
}