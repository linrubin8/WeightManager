using System;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Class that implements some object's properties such as location, size and visibility.
  /// </summary>
  public abstract class ComponentBase : Base
  {
    #region Fields
    private float FLeft;
    private float FTop;
    private float FWidth;
    private float FHeight;
    private DockStyle FDock;
    private AnchorStyles FAnchor;
    private bool FVisible;
    private int FGroupIndex;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the left coordinate of the object in relation to its container.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///         This property value is measured in the screen pixels. Use
    ///         <see cref="Units"/> class to convert a value to desired units.
    ///     </para>
    ///   <para>
    ///         To obtain absolute coordinate, use <see cref="AbsLeft"/> property.
    ///     </para>
    /// </remarks>
    /// <example>The following example demonstrates how to convert between pixels and units:<code>
    /// TextObject text1;
    /// // set Left to 10mm
    /// text1.Left = Units.Millimeters * 10;
    /// // convert a value to millimeters
    /// MessageBox.Show("Left = " + (text1.Left / Units.Millimeters).ToString() + "mm");
    /// </code></example>
    [Category("Layout")]
    public virtual float Left
    {
      get { return FLeft; }
      set
      {
        value = (float)Math.Round(value, 2);
        if (!IsDesigning || !HasRestriction(Restrictions.DontMove))
        {
          FLeft = value;
          if (Dock != DockStyle.None && Parent != null)
            (Parent as IParent).UpdateLayout(0, 0);
        }  
      }
    }

    /// <summary>
    /// Gets or sets the top coordinate of the object in relation to its container.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///         This property value is measured in the screen pixels. Use
    ///         <see cref="Units"/> class to convert a value to desired units.
    ///     </para>
    ///   <para>
    ///         To obtain absolute coordinate, use <see cref="AbsTop"/> property.
    ///     </para>
    /// </remarks>
    /// <example>The following example demonstrates how to convert between pixels and units:<code>
    /// TextObject text1;
    /// // set Top to 10mm
    /// text1.Top = Units.Millimeters * 10;
    /// // convert a value to millimeters
    /// MessageBox.Show("Top = " + (text1.Top / Units.Millimeters).ToString() + "mm");
    /// </code></example>
    [Category("Layout")]
    public virtual float Top
    {
      get { return FTop; }
      set
      {
        value = (float)Math.Round(value, 2);
        if (!IsDesigning || !HasRestriction(Restrictions.DontMove))
        {
          FTop = value;
          if (Dock != DockStyle.None && Parent != null)
            (Parent as IParent).UpdateLayout(0, 0);
        }  
      }
    }

    /// <summary>
    /// Gets or sets the width of the object.
    /// </summary>
    /// <remarks>
    /// This property value is measured in the screen pixels. Use <see cref="Units"/> class to 
    /// convert a value to desired units.
    /// </remarks>
    /// <example>The following example demonstrates how to convert between pixels and units:<code>
    /// TextObject text1;
    /// // set Width to 10mm
    /// text1.Width = Units.Millimeters * 10;
    /// // convert a value to millimeters
    /// MessageBox.Show("Width = " + (text1.Width / Units.Millimeters).ToString() + "mm");
    /// </code></example>
    [Category("Layout")]
    public virtual float Width
    {
      get { return FWidth; }
      set
      {
        value = (float)Math.Round(value, 2);
        if (FloatDiff(FWidth, value))
        {
          if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          {
            if (this is IParent)
              (this as IParent).UpdateLayout(value - FWidth, 0);
            FWidth = value;
            if (Dock != DockStyle.None && Parent != null)
              (Parent as IParent).UpdateLayout(0, 0);
          }
        }  
      }
    }

    /// <summary>
    /// Gets or sets the height of the object.
    /// </summary>
    /// <remarks>
    /// This property value is measured in the screen pixels. Use <see cref="Units"/> class to 
    /// convert a value to desired units.
    /// </remarks>
    /// <example>The following example demonstrates how to convert between pixels and units:<code>
    /// TextObject text1;
    /// // set Height to 10mm
    /// text1.Height = Units.Millimeters * 10;
    /// // convert a value to millimeters
    /// MessageBox.Show("Height = " + (text1.Height / Units.Millimeters).ToString() + "mm");
    /// </code></example>
    [Category("Layout")]
    public virtual float Height
    {
      get { return FHeight; }
      set
      {
        value = (float)Math.Round(value, 2);
        if (FloatDiff(FHeight, value))
        {
          if (!IsDesigning || !HasRestriction(Restrictions.DontResize))
          {
            if (this is IParent)
              (this as IParent).UpdateLayout(0, value - FHeight);
            FHeight = value;
            if (Dock != DockStyle.None && Parent != null)
              (Parent as IParent).UpdateLayout(0, 0);
          }  
        }  
      }
    }

    /// <summary>
    /// Gets or sets which control borders are docked to its parent control and determines how a control 
    /// is resized with its parent. 
    /// </summary>
    /// <remarks>
    /// <para>Use the <b>Dock</b> property to define how a control is automatically resized as its parent control is 
    /// resized. For example, setting Dock to <c>DockStyle.Left</c> causes the control to align itself with the 
    /// left edges of its parent control and to resize as the parent control is resized.</para>
    /// <para>A control can be docked to one edge of its parent container or can be docked to all edges and 
    /// fill the parent container.</para>
    /// </remarks>
    [DefaultValue(DockStyle.None)]
    [Category("Layout")]
    public virtual DockStyle Dock
    {
      get { return FDock; }
      set 
      { 
        if (FDock != value)
        {
          FDock = value;
          if (Parent != null)
            (Parent as IParent).UpdateLayout(0, 0);
        }
      }
    }

    /// <summary>
    /// Gets or sets the edges of the container to which a control is bound and determines how a control 
    /// is resized with its parent.
    /// </summary>
    /// <remarks>
    /// <para>Use the Anchor property to define how a control is automatically resized as its parent control 
    /// is resized. Anchoring a control to its parent control ensures that the anchored edges remain in the 
    /// same position relative to the edges of the parent control when the parent control is resized.</para>
    /// <para>You can anchor a control to one or more edges of its container. For example, if you have a band 
    /// with a <b>TextObject</b> whose <b>Anchor</b> property value is set to <b>Top, Bottom</b>, the <b>TextObject</b> is stretched to 
    /// maintain the anchored distance to the top and bottom edges of the band as the height of the band 
    /// is increased.</para>
    /// </remarks>
    [DefaultValue(AnchorStyles.Left | AnchorStyles.Top)]
    [Category("Layout")]
    public virtual AnchorStyles Anchor
    {
      get { return FAnchor; }
      set { FAnchor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the object is displayed in the preview window.
    /// </summary>
    /// <remarks>
    /// Setting this property to <b>false</b> will hide the object in the preview window.
    /// </remarks>
    /// <example>The following report script will control the Text1 visibility depending on the value of the
    /// data column:<code>
    /// private void Data1_BeforePrint(object sender, EventArgs e)
    /// {
    ///   Text1.Visible = [Orders.Shipped] == true;
    /// }
    /// </code></example>
    [DefaultValue(true)]
    [Category("Behavior")]
    public virtual bool Visible
    {
      get { return FVisible; }
      set { FVisible = value; }
    }

    /// <summary>
    /// Gets or sets a group index.
    /// </summary>
    /// <remarks>
    /// Group index is used to group objects in the designer (using "Group" button). When you select
    /// any object in a group, entire group becomes selected. To reset a group, set the <b>GroupIndex</b>
    /// to 0 (default value).
    /// </remarks>
    [Browsable(false)]
    public int GroupIndex
    {
      get { return FGroupIndex; }
      set { FGroupIndex = value; }
    }
    
    /// <summary>
    /// Gets the right coordinate of the object in relation to its container.
    /// </summary>
    /// <remarks>
    /// To change the right coordinate, change the <see cref="Left"/> and/or <see cref="Width"/> properties.
    /// </remarks>
    [Browsable(false)]
    public float Right
    {
      get { return Left + Width; }
    }

    /// <summary>
    /// Gets the bottom coordinate of the object in relation to its container.
    /// </summary>
    /// <remarks>
    /// To change the bottom coordinate, change the <see cref="Top"/> and/or <see cref="Height"/> properties.
    /// </remarks>
    [Browsable(false)]
    public float Bottom
    {
      get { return Top + Height; }
    }

    /// <summary>
    /// Gets the absolute left coordinate of the object.
    /// </summary>
    [Browsable(false)]
    public virtual float AbsLeft
    {
      get { return (Parent is ComponentBase) ? Left + (Parent as ComponentBase).AbsLeft : Left; }
    }

    /// <summary>
    /// Gets the absolute top coordinate of the object.
    /// </summary>
    [Browsable(false)]
    public virtual float AbsTop
    {
      get { return (Parent is ComponentBase) ? Top + (Parent as ComponentBase).AbsTop : Top; }
    }

    /// <summary>
    /// Gets the absolute right coordinate of the object.
    /// </summary>
    [Browsable(false)]
    public float AbsRight
    {
      get { return AbsLeft + Width; }
    }

    /// <summary>
    /// Gets the absolute bottom coordinate of the object.
    /// </summary>
    [Browsable(false)]
    public float AbsBottom
    {
      get { return AbsTop + Height; }
    }

    /// <summary>
    /// Gets or sets the bounding rectangle of the object.
    /// </summary>
    /// <remarks>
    /// Assigning a value to this property is equal to assigning values to the <see cref="Left"/>, 
    /// <see cref="Top"/>, <see cref="Width"/>, <see cref="Height"/> properties.
    /// </remarks>
    [Browsable(false)]
    public RectangleF Bounds
    {
      get { return new RectangleF(Left, Top, Width, Height); }
      set 
      {
        Left = value.Left;
        Top = value.Top;
        Width = value.Width;
        Height = value.Height;
      }
    }

    /// <summary>
    /// Gets the absolute bounding rectangle of the object.
    /// </summary>
    [Browsable(false)]
    public RectangleF AbsBounds
    {
      get { return new RectangleF(AbsLeft, AbsTop, Width, Height); }
    }

    /// <summary>
    /// Gets or sets the size of client area of the object.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="FastReport.Dialog.DialogPage"/> class.
    /// </remarks>
    [Browsable(false)]
    public virtual SizeF ClientSize
    {
      get { return new SizeF(Width, Height); }
      set 
      {
        Width = value.Width;
        Height = value.Height;
      } 
    }
    #endregion

    #region Private Methods
    #endregion

    #region Protected Methods
    /// <summary>
    /// Gets the object's selection points.
    /// </summary>
    /// <returns>Array of <see cref="SelectionPoint"/> objects.</returns>
    /// <remarks>
    /// <para>Selection point is a small square displayed at the object's sides when object is selected
    /// in the designer. You can drag this square by the mouse to change the object's size. For example,
    /// the <b>TextObject</b> has eight selection points to change its width and height by the mouse.</para>
    /// <para>If you are developing a new component for FastReport, you may override this method
    /// if your object has non-standard set of selection points. For example, if an object has something like
    /// "AutoSize" property, it would be good to disable all selection points if that property is <b>true</b>,
    /// to disable resizing of the object by the mouse.</para>
    /// </remarks>
    protected virtual SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] {
        new SelectionPoint(AbsLeft, AbsTop, SizingPoint.LeftTop),
        new SelectionPoint(AbsLeft + Width, AbsTop, SizingPoint.RightTop),
        new SelectionPoint(AbsLeft, AbsTop + Height, SizingPoint.LeftBottom),
        new SelectionPoint(AbsLeft + Width, AbsTop + Height, SizingPoint.RightBottom),
        new SelectionPoint(AbsLeft + Width / 2, AbsTop, SizingPoint.TopCenter),
        new SelectionPoint(AbsLeft + Width / 2, AbsTop + Height, SizingPoint.BottomCenter),
        new SelectionPoint(AbsLeft, AbsTop + Height / 2, SizingPoint.LeftCenter),
        new SelectionPoint(AbsLeft + Width, AbsTop + Height / 2, SizingPoint.RightCenter) };
    }

    /// <summary>
    /// Draws the selection point.
    /// </summary>
    /// <param name="e">Paint event args.</param>
    /// <param name="p"><see cref="Pen"/> object.</param>
    /// <param name="b"><see cref="Brush"/> object.</param>
    /// <param name="x">Left coordinate.</param>
    /// <param name="y">Top coordinate.</param>
    protected void DrawSelectionPoint(FRPaintEventArgs e, Pen p, Brush b, float x, float y)
    {
      Graphics g = e.Graphics;
      x = (float)Math.Round(x * e.ScaleX);
      y = (float)Math.Round(y * e.ScaleY);
      g.FillRectangle(b, x - 2, y - 2, 4, 4);
      g.DrawRectangle(p, x - 2, y - 2, 4, 4);
    }
    
    /// <summary>
    /// Gets a value indicating that given point is inside selection point.
    /// </summary>
    /// <param name="x">point's x coordinate.</param>
    /// <param name="y">point's y coordinate.</param>
    /// <param name="point">selection point.</param>
    /// <returns><b>true</b> if <b>(x,y)</b> is inside the <b>point</b></returns>
    protected bool PointInSelectionPoint(float x, float y, PointF point)
    {
      return x >= point.X - 3 && x <= point.X + 3 && y >= point.Y - 3 && y <= point.Y + 3;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      ComponentBase src = source as ComponentBase;
      Left = src.Left;
      Top = src.Top;
      Width = src.Width;
      Height = src.Height;
      Dock = src.Dock;
      Anchor = src.Anchor;
      Visible = src.Visible;
    }
    
    /// <summary>
    /// Checks if given point is inside the object's bounds.
    /// </summary>
    /// <param name="point">point to check.</param>
    /// <returns><b>true</b> if <b>point</b> is inside the object's bounds.</returns>
    /// <remarks>
    /// You can override this method if your objectis not of rectangular form. 
    /// </remarks>
    public virtual bool PointInObject(PointF point)
    {
      return AbsBounds.Contains(point);
    }

    /// <summary>
    /// Checks if the object is inside its parent.
    /// </summary>
    /// <param name="immediately">if <b>true</b>, check now independent of any conditions.</param>
    /// <remarks>
    /// <para>Typically you don't need to use or override this method.</para>
    /// <para>When you move an object with the mouse, it may be moved outside its parent. If so, this method
    /// must find a new parent for the object and correct it's <b>Left</b>, <b>Top</b> and <b>Parent</b>
    /// properties. If <b>immediately</b> parameter is <b>false</b>, you can optimize the method
    /// to search for new parent only if the object's bounds are outside parent. If this parameter is
    /// <b>true</b>, you must skip any optimizations and search for a parent immediately.</para>
    /// </remarks>
    public virtual void CheckParent(bool immediately)
    {
    }
    
    /// <summary>
    /// Corrects the object's size and sizing point if the size becomes negative.
    /// </summary>
    /// <param name="e">Current mouse state.</param>
    /// <para>Typically you don't need to use or override this method.</para>
    /// <para>This method is called by the FastReport designer to check if the object's size becomes negative
    /// when resizing the object by the mouse. Method must correct the object's size and/or position to
    /// make it positive, also change the sizing point if needed.</para>
    public virtual void CheckNegativeSize(FRMouseEventArgs e)
    {
      if (Width < 0 && Height < 0)
      {
        e.SizingPoint = SizingPointHelper.SwapDiagonally(e.SizingPoint);
        Bounds = new RectangleF(Right, Bottom, -Width, -Height);
      }
      else if (Width < 0)
      {
        e.SizingPoint = SizingPointHelper.SwapHorizontally(e.SizingPoint);
        Bounds = new RectangleF(Right, Top, -Width, Height);
      }
      else if (Height < 0)
      {
        e.SizingPoint = SizingPointHelper.SwapVertically(e.SizingPoint);
        Bounds = new RectangleF(Left, Bottom, Width, -Height);
      }
      else
        return;
      Cursor.Current = SizingPointHelper.ToCursor(e.SizingPoint);
    }

    /// <summary>
    /// Gets the preferred size of an object.
    /// </summary>
    /// <returns>Preferred size.</returns>
    /// <remarks>
    /// This method is called by the FastReport designer when you insert a new object.
    /// </remarks>
    public virtual SizeF GetPreferredSize()
    {
      return new SizeF(Units.Millimeters * 25, Units.Millimeters * 5);
    }
    
    /// <summary>
    /// Draws the object.
    /// </summary>
    /// <param name="e">Paint event args.</param>
    /// <remarks>
    /// <para>This method is widely used in the FastReport. It is called each time when the object needs to draw 
    /// or print itself.</para>
    /// <para>In order to draw the object correctly, you should multiply the object's bounds by the <b>scale</b>
    /// parameter.</para>
    /// <para><b>cache</b> parameter is used to optimize the drawing speed. It holds all items such as
    /// pens, fonts, brushes, string formats that was used before. If the item with requested parameters
    /// exists in the cache, it will be returned (instead of create new item and then dispose it).</para>
    /// </remarks>
    public virtual void Draw(FRPaintEventArgs e)
    {
      if (IsDesigning)
      {
        if (IsAncestor)
          e.Graphics.DrawImage(Res.GetImage(99), (int)(AbsRight * e.ScaleX - 9), (int)(AbsTop * e.ScaleY + 2));
      }
    }
    
    /// <summary>
    /// Draw the selection points.
    /// </summary>
    /// <param name="e">Paint event args.</param>
    /// <remarks>
    /// This method draws a set of selection points returned by the <see cref="GetSelectionPoints"/> method.
    /// </remarks>
    public virtual void DrawSelection(FRPaintEventArgs e)
    {
      if (Page == null)
        return;
      bool firstSelected = Report.Designer.SelectedObjects.IndexOf(this) == 0;
      Pen p = firstSelected ? Pens.Black : Pens.White;
      Brush b = firstSelected ? Brushes.White : Brushes.Black;
      SelectionPoint[] selectionPoints = GetSelectionPoints();
      foreach (SelectionPoint pt in selectionPoints)
      {
        DrawSelectionPoint(e, p, b, pt.X, pt.Y);
      }
    }
    
    /// <summary>
    /// Draw the frame around the object to indicate that it accepts the drag&amp;drop operation.
    /// </summary>
    /// <param name="e">Paint event args.</param>
    /// <param name="color">The color of frame.</param>
    public virtual void DrawDragAcceptFrame(FRPaintEventArgs e, Color color)
    {
      RectangleF rect = new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY);
      Pen p = e.Cache.GetPen(color, 1, DashStyle.Dot);
      for (int i = 0; i < 3; i++)
      {
        e.Graphics.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
        rect.Inflate(-1, -1);
      }
    }

    /// <summary>
    /// Handles MouseDown event that occurs when the user clicks the mouse in the designer.
    /// </summary>
    /// <remarks>
    ///   <para>This method is called when the user press the mouse button in the designer.
    ///     The standard implementation does the following:</para>
    ///   <list type="bullet">
    ///     <item>checks if the mouse pointer is inside the object;</item>
    ///     <item>add an object to the selected objects list of the designer;</item>
    ///     <item>sets the <b>e.Handled</b> flag to <b>true</b>.</item>
    ///   </list>
    /// </remarks>
    /// <param name="e">Current mouse state.</param>
    public virtual void HandleMouseDown(FRMouseEventArgs e)
    {
      if (PointInObject(new PointF(e.X, e.Y)))
      {
        SelectedObjectCollection selection = Report.Designer.SelectedObjects;
        if (e.ModifierKeys == Keys.Shift)
        {
          // toggle selection
          if (selection.IndexOf(this) != -1)
          {
            if (selection.Count > 1)
              selection.Remove(this);
          }
          else
            selection.Add(this);
        }
        else
        {
          // select the object if not selected yet
          if (selection.IndexOf(this) == -1)
          {
            selection.Clear();
            selection.Add(this);
          }
        }
        e.Handled = true;
        e.Mode = WorkspaceMode2.Move;
      }  
    }

    /// <summary>
    /// Handles MouseMove event that occurs when the user moves the mouse in the designer.
    /// </summary>
    /// <remarks>
    ///   <para>This method is called when the user moves the mouse in the designer. Typical
    ///     use of this method is to change the mouse cursor to <b>SizeAll</b> when it is over
    ///     an object. The standard implementation does the following:</para>
    ///   <list type="bullet">
    ///     <item>checks if the mouse pointer is inside the object;</item>
    ///     <item>changes the cursor shape (<b>e.Cursor</b> property);</item>
    ///     <item>sets the <b>e.Handled</b> flag to <b>true</b>.</item>
    ///   </list>
    /// </remarks>
    /// <param name="e">Current mouse state.</param>
    public virtual void HandleMouseHover(FRMouseEventArgs e)
    {
      if (PointInObject(new PointF(e.X, e.Y)))
      {
        e.Handled = true;
        e.Cursor = Cursors.SizeAll;
      }  
    }

    /// <summary>
    /// Handles MouseMove event that occurs when the user moves the mouse in the designer.
    /// </summary>
    /// <remarks>
    ///   <para>This method is called when the user moves the mouse in the designer. The
    ///     standard implementation does the following:</para>
    ///   <list type="bullet">
    ///     <item>
    ///             if mouse button is not pressed, check that mouse pointer is inside one of
    ///             the selection points returned by the <see cref="GetSelectionPoints"/>
    ///             method and set the <b>e.SizingPoint</b> member to the corresponding sizing
    ///             point;
    ///         </item>
    ///     <item>if mouse button is pressed, and <b>e.SizingPoint</b> member is not
    ///         <b>SizingPoint.None</b>, resize the object.</item>
    ///   </list>
    /// </remarks>
    /// <param name="e">Current mouse state.</param>
    public virtual void HandleMouseMove(FRMouseEventArgs e)
    {
      if (!IsSelected)
        return;

      if (e.Button == MouseButtons.None)
      {
        PointF point = new PointF(e.X, e.Y);
        e.SizingPoint = SizingPoint.None;
        SelectionPoint[] selectionPoints = GetSelectionPoints();
        foreach (SelectionPoint pt in selectionPoints)
        {
          if (PointInSelectionPoint(pt.X, pt.Y, point))
          {
            e.SizingPoint = pt.SizingPoint;
            break;
          }  
        }
        if (e.SizingPoint != SizingPoint.None)
        {
          e.Handled = true;
          e.Mode = WorkspaceMode2.Size;
          e.Cursor = SizingPointHelper.ToCursor(e.SizingPoint);
        }
      }
      else if (!IsParentSelected)
      {
        if (e.Mode == WorkspaceMode2.Move)
        {
          Left += e.Delta.X;
          Top += e.Delta.Y;
        }
        else if (e.Mode == WorkspaceMode2.Size)
        {
            if ((e.ModifierKeys & Keys.Shift) > 0)
            {
                bool wider = Math.Abs(e.Delta.X) > Math.Abs(e.Delta.Y);
                float width = Width;
                float height = Height;

                switch (e.SizingPoint)
                {
                    case SizingPoint.LeftTop:
                        if (wider)
                        {
                            Left += e.Delta.Y;
                            Width -= e.Delta.Y;
                            Top += Height - (Height * Width / width);
                            Height = Height * Width / width;
                        }
                        else
                        {
                            Top += e.Delta.X;
                            Height -= e.Delta.X;
                            Left += Width - (Width * Height / height);
                            Width = Width * Height / height;
                        }
                        break;
                    case SizingPoint.LeftBottom:
                        if (wider)
                        {
                            Left -= e.Delta.Y;
                            Width += e.Delta.Y;
                            Height = Height * Width / width;
                        }
                        else
                        {
                            Height -= e.Delta.X;
                            Left += Width - (Width * Height / height);
                            Width = Width * Height / height;
                        }
                        break;
                    case SizingPoint.RightTop:
                        if (wider)
                        {
                            Width -= e.Delta.Y;
                            Top += Height - (Height * Width / width);
                            Height = Height * Width / width;
                        }
                        else
                        {
                            Height += e.Delta.X;
                            Top -= e.Delta.X;
                            Width = Width * Height / height;
                        }
                        break;
                    case SizingPoint.RightBottom:
                        if (wider)
                        {
                            Width += e.Delta.Y;
                            Height = Height * Width / width;
                        }
                        else
                        {
                            Height += e.Delta.X;
                            Width = Width * Height / height;
                        }
                        break;
                    case SizingPoint.TopCenter:
                        Top += e.Delta.Y;
                        Height -= e.Delta.Y;
                        Width = Width * Height / height;
                        break;
                    case SizingPoint.BottomCenter:
                        Height += e.Delta.Y;
                        Width = Width * Height / height;
                        break;
                    case SizingPoint.LeftCenter:
                        Left += e.Delta.X;
                        Width -= e.Delta.X;
                        Height = Height * Width / width;
                        break;
                    case SizingPoint.RightCenter:
                        Width += e.Delta.X;
                        Height = Height * Width / width;
                        break;
                }
            }
            else
            {
                switch (e.SizingPoint)
                {
                    case SizingPoint.LeftTop:
                        Left += e.Delta.X;
                        Width -= e.Delta.X;
                        Top += e.Delta.Y;
                        Height -= e.Delta.Y;
                        break;
                    case SizingPoint.LeftBottom:
                        Left += e.Delta.X;
                        Width -= e.Delta.X;
                        Height += e.Delta.Y;
                        break;
                    case SizingPoint.RightTop:
                        Width += e.Delta.X;
                        Top += e.Delta.Y;
                        Height -= e.Delta.Y;
                        break;
                    case SizingPoint.RightBottom:
                        Width += e.Delta.X;
                        Height += e.Delta.Y;
                        break;
                    case SizingPoint.TopCenter:
                        Top += e.Delta.Y;
                        Height -= e.Delta.Y;
                        break;
                    case SizingPoint.BottomCenter:
                        Height += e.Delta.Y;
                        break;
                    case SizingPoint.LeftCenter:
                        Left += e.Delta.X;
                        Width -= e.Delta.X;
                        break;
                    case SizingPoint.RightCenter:
                        Width += e.Delta.X;
                        break;
                }
            }
          CheckNegativeSize(e);
        }
        CheckParent(false);
      }
    }

    /// <summary>
    /// Handles MouseUp event that occurs when the user releases the mouse button in the designer.
    /// </summary>
    /// <remarks>
    ///   <para>This method is called when the user releases the mouse button in the
    ///     designer. The standard implementation does the following:</para>
    ///   <list type="bullet">
    ///     <item>if <b>e.Mode</b> is <b>WorkspaceMode2.SelectionRect</b>, checks if object
    ///         is inside the selection rectangle and sets <b>e.Handled</b> flag if so;</item>
    ///     <item>
    ///             checks that object is inside its parent (calls the
    ///             <see cref="CheckParent"/> method).
    ///         </item>
    ///   </list>
    /// </remarks>
    /// <param name="e">Current mouse state.</param>
    public virtual void HandleMouseUp(FRMouseEventArgs e)
    {
      if (e.Mode == WorkspaceMode2.SelectionRect)
      {
        if (e.SelectionRect.IntersectsWith(new RectangleF(AbsLeft, AbsTop, Width, Height)))
          e.Handled = true;
      }    
      else if (e.Mode == WorkspaceMode2.Move || e.Mode == WorkspaceMode2.Size)
      {
        if (IsSelected)
          CheckParent(true);
      }
    }

    /// <summary>
    /// Handles double click event in the designer.
    /// </summary>
    /// <remarks>
    /// This method is called when the user doubleclicks the object in the designer. Typical implementation
    /// invokes the object's editor (calls the <b>InvokeEditor</b> method) and sets the designer's
    /// <b>Modified</b> flag.
    /// </remarks>
    public virtual void HandleDoubleClick()
    {
      if (HasFlag(Flags.CanEdit) && !HasRestriction(Restrictions.DontEdit) &&
        this is IHasEditor && (this as IHasEditor).InvokeEditor())
        Report.Designer.SetModified(this, "Change");
    }

    /// <summary>
    /// Handles mouse wheel event.
    /// </summary>
    /// <param name="e">Current mouse state.</param>
    public virtual void HandleMouseWheel(FRMouseEventArgs e)
    {
    }

    /// <summary>
    /// Handles the DragOver event in the designer.
    /// </summary>
    /// <param name="e">Current mouse state.</param>
    /// <remarks>
    /// This method is called when the user drags an item from the Data Tree window. This method should
    /// check that the mouse (<b>e.X, e.Y</b>) is inside the object, then set the <b>e.Handled</b> flag 
    /// to <b>true</b> if an item can be dragged into this object.
    /// </remarks>
    public virtual void HandleDragOver(FRMouseEventArgs e)
    {
    }

    /// <summary>
    /// Handles the DragDrop event in the designer.
    /// </summary>
    /// <param name="e">Current mouse state.</param>
    /// <remarks>
    /// This method is called when the user drops an item from the Data Tree window into this object.
    /// This method should copy the information from the <b>e.DraggedObject</b> object and set the 
    /// <b>e.Handled</b> flag to <b>true</b> to complete the drag operation.
    /// </remarks>
    public virtual void HandleDragDrop(FRMouseEventArgs e)
    {
    }

    /// <summary>
    /// Handles KeyDown event in the designer.
    /// </summary>
    /// <param name="sender">The designer's workspace.</param>
    /// <param name="e">Keyboard event parameters.</param>
    /// <remarks>
    /// This method is called when the user presses any key in the designer. Typical implementation
    /// does nothing.
    /// </remarks>
    public virtual void HandleKeyDown(Control sender, KeyEventArgs e)
    {
    }

    /// <summary>
    /// Returns a "smart tag" menu.
    /// </summary>
    /// <remarks>
    /// "Smart tag" is a little button that appears near the object's top-right corner when we are in the
    /// designer and move the mouse over the object. When you click that button you will see a popup window
    /// where you can set up some properties of the object. FastReport uses smart tags to quickly choose
    /// the datasource (for a band) or data column (for objects).
    /// </remarks>
    public virtual SmartTagBase GetSmartTag()
    {
      return null;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ComponentBase c = writer.DiffObject as ComponentBase;
      base.Serialize(writer);
      
      if (HasFlag(Flags.CanWriteBounds))
      {
        if (FloatDiff(Left, c.Left))
          writer.WriteFloat("Left", Left);
        if (FloatDiff(Top, c.Top))
          writer.WriteFloat("Top", Top);
        if (FloatDiff(Width, c.Width))
          writer.WriteFloat("Width", Width);
        if (FloatDiff(Height, c.Height))
          writer.WriteFloat("Height", Height);
      }  
      if (writer.SerializeTo != SerializeTo.Preview)
      {
        if (Dock != c.Dock)
          writer.WriteValue("Dock", Dock);
        if (Anchor != c.Anchor)
          writer.WriteValue("Anchor", Anchor);    
        if (Visible != c.Visible)
          writer.WriteBool("Visible", Visible);
        if (GroupIndex != c.GroupIndex)
          writer.WriteInt("GroupIndex", GroupIndex);
      }  
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new ComponentMenuBase(Report.Designer);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentBase"/> class with default settings.
    /// </summary>
    public ComponentBase()
    {
      FAnchor = AnchorStyles.Left | AnchorStyles.Top;
      FVisible = true;
      SetFlags(Flags.CanWriteBounds | Flags.HasGlobalName, true);
    }
  }
}