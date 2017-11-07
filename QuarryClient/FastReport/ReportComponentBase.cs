using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Design;
using FastReport.TypeConverters;
using FastReport.Code;
using FastReport.Design.PageDesigners.Page;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// The style of the report object markers.
  /// </summary>
  public enum MarkerStyle 
  { 
    /// <summary>
    /// Rectangle marker.
    /// </summary>
    Rectangle, 
    
    /// <summary>
    /// Small markers at the object's corners.
    /// </summary>
    Corners 
  }
  
  /// <summary>
  /// The automatic shift mode.
  /// </summary>
  public enum ShiftMode 
  { 
    /// <summary>
    /// Do not shift the object.
    /// </summary>
    Never, 
    
    /// <summary>
    /// Shift the object up or down if any object above it shrinks or grows.
    /// </summary>
    Always,
    
    /// <summary>
    /// Shift the object up or down if any object above it shrinks or grows. 
    /// Objects must have overlapped x-coordinates.
    /// </summary>
    WhenOverlapped 
  }
  
  /// <summary>
  /// Specifies where to print an object.
  /// </summary>
  [Flags]
  public enum PrintOn
  {
    /// <summary>
    /// Do not print the object.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Print the object on the first page. If this flag is not set, the object will not
    /// be printed on the first page.
    /// </summary>
    FirstPage = 1,

    /// <summary>
    /// Print the object on the last page. If this flag is not set, the object will not
    /// be printed on the last page. You should set the report's double pass option to make
    /// it work correctly.
    /// </summary>
    LastPage = 2,

    /// <summary>
    /// Print the object on odd pages only.
    /// </summary>
    OddPages = 4,
    
    /// <summary>
    /// Print the object on even pages only.
    /// </summary>
    EvenPages = 8,
    
    /// <summary>
    /// Print the object on band with "Repeat on Every Page" flag when that band is repeated. 
    /// </summary>
    RepeatedBand = 16,

    /// <summary>
    /// Print the object if the report has single page only.
    /// </summary>
    SinglePage = 32
  }
  
  
  /// <summary>
  /// Specifies the style properties to use when style is applied.
  /// </summary>
  public enum StylePriority
  {
    /// <summary>
    /// Use the fill property of the style.
    /// </summary>
    UseFill,
    
    /// <summary>
    /// Use all style properties.
    /// </summary>
    UseAll
  }

  /// <summary>
  /// Base class for all report objects.
  /// </summary>
  public abstract class ReportComponentBase : ComponentBase
  {
    #region Fields
    private bool FPrintable;
    private bool FExportable;
    private Border FBorder;
    private Cursor FCursor;
    private FillBase FFill;
    private string FBookmark;
    private Hyperlink FHyperlink;
    private bool FCanGrow;
    private bool FCanShrink;
    private bool FGrowToBottom;
    private ShiftMode FShiftMode;
    private string FStyle;
    private string FEvenStyle;
    private string FHoverStyle;
    private StylePriority FEvenStylePriority;
    private PrintOn FPrintOn;
    private string FBeforePrintEvent;
    private string FAfterPrintEvent;
    private string FAfterDataEvent;
    private string FClickEvent;
    private string FMouseMoveEvent;
    private string FMouseUpEvent;
    private string FMouseDownEvent;
    private string FMouseEnterEvent;
    private string FMouseLeaveEvent;
    private bool FFlagSimpleBorder;
    private bool FFlagUseBorder;
    private bool FFlagUseFill;
    private bool FFlagPreviewVisible;
    private bool FFlagSerializeStyle;
    private bool FFlagProvidesHyperlinkValue;
    private RectangleF FSavedBounds;
    private bool FSavedVisible;
    private string FSavedBookmark;
    private Border FSavedBorder;
    private FillBase FSavedFill;
    #endregion

    #region Properties
    /// <summary>
    /// This event occurs before the object is added to the preview pages.
    /// </summary>
    public event EventHandler BeforePrint;

    /// <summary>
    /// This event occurs after the object was added to the preview pages.
    /// </summary>
    public event EventHandler AfterPrint;

    /// <summary>
    /// This event occurs after the object was filled with data.
    /// </summary>
    public event EventHandler AfterData;

    /// <summary>
    /// This event occurs when the user clicks the object in the preview window.
    /// </summary>
    public event EventHandler Click;

    /// <summary>
    /// This event occurs when the user moves the mouse over the object in the preview window.
    /// </summary>
    public event MouseEventHandler MouseMove;

    /// <summary>
    /// This event occurs when the user releases the mouse button in the preview window.
    /// </summary>
    public event MouseEventHandler MouseUp;

    /// <summary>
    /// This event occurs when the user clicks the mouse button in the preview window.
    /// </summary>
    public event MouseEventHandler MouseDown;

    /// <summary>
    /// This event occurs when the mouse enters the object's bounds in the preview window.
    /// </summary>
    public event EventHandler MouseEnter;

    /// <summary>
    /// This event occurs when the mouse leaves the object's bounds in the preview window.
    /// </summary>
    public event EventHandler MouseLeave;

    /// <summary>
    /// Gets or sets a value that determines if the object can be printed on the printer.
    /// </summary>
    /// <remarks>
    /// Object with Printable = <b>false</b> is still visible in the preview window, but not on the prinout.
    /// If you want to hide an object in the preview, set the <see cref="ComponentBase.Visible"/> property to <b>false</b>.
    /// </remarks>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Printable
    {
      get { return FPrintable; }
      set { FPrintable = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines if the object can be exported.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Exportable
    {
      get { return FExportable; }
      set { FExportable = value; }
    }

    /// <summary>
    /// Gets or sets an object's border.
    /// </summary>
    [Category("Appearance")]
    public virtual Border Border
    {
      get { return FBorder; }
      set
      {
        FBorder = value;
        if (!String.IsNullOrEmpty(Style))
          Style = "";
      }
    }

    /// <summary>
    /// Gets or sets an object's fill.
    /// </summary>
    /// <remarks>
    /// The fill can be one of the following types: <see cref="SolidFill"/>, <see cref="LinearGradientFill"/>, 
    /// <see cref="PathGradientFill"/>, <see cref="HatchFill"/>.
    /// <para/>To set the solid fill color, use the simpler <see cref="FillColor"/> property.
    /// </remarks>
    /// <example>This example shows how to set the new fill and change its properties:
    /// <code>
    /// textObject1.Fill = new SolidFill(Color.Green);
    /// (textObject1.Fill as SolidFill).Color = Color.Red;
    /// </code>
    /// </example>
    [Category("Appearance")]
    [Editor(typeof(FillEditor), typeof(UITypeEditor))]
    public virtual FillBase Fill
    {
      get { return FFill; }
      set 
      { 
        if (value == null)
          throw new ArgumentNullException("Fill");
        FFill = value;
        if (!String.IsNullOrEmpty(Style))
          Style = "";
      }
    }

    /// <summary>
    /// Gets or sets the fill color in a simple manner.
    /// </summary>
    /// <remarks>
    /// This property can be used in a report script to change the fill color of the object. It is 
    /// equivalent to: <code>reportComponent1.Fill = new SolidFill(color);</code>
    /// </remarks>
    [Browsable(false)]
    public Color FillColor
    {
      get { return Fill is SolidFill ? (Fill as SolidFill).Color : Color.Transparent; }
      set { Fill = new SolidFill(value); }
    }

    /// <summary>
    /// Gets or sets an object's cursor shape.
    /// </summary>
    /// <remarks>
    /// This property is used in the preview mode.
    /// </remarks>
    [Category("Appearance")]
    public Cursor Cursor
    {
      get { return FCursor; }
      set { FCursor = value; }
    }

    /// <summary>
    /// Gets or sets a bookmark expression.
    /// </summary>
    /// <remarks>
    /// This property can contain any valid expression that returns a bookmark name. This can be, for example,
    /// a data column. To navigate to a bookmark, you have to use the <see cref="Hyperlink"/> property.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Navigation")]
    public string Bookmark
    {
      get { return FBookmark; }
      set { FBookmark = value; }
    }

    /// <summary>
    /// Gets or sets a hyperlink.
    /// </summary>
    /// <remarks>
    /// <para>The hyperlink is used to define clickable objects in the preview. 
    /// When you click such object, you may navigate to the external url, the page number, 
    /// the bookmark defined by other report object, or display the external report. 
    /// Set the <b>Kind</b> property of the hyperlink to select appropriate behavior.</para>
    /// <para>Usually you should set the <b>Expression</b> property of the hyperlink to
    /// any valid expression that will be calculated when this object is about to print.
    /// The value of an expression will be used for navigation.</para>
    /// <para>If you want to navigate to
    /// something fixed (URL or page number, for example) you also may set the <b>Value</b>
    /// property instead of <b>Expression</b>.</para>
    /// </remarks>
    [Category("Navigation")]
    [Editor(typeof(HyperlinkEditor), typeof(UITypeEditor))]
    public Hyperlink Hyperlink
    {
      get { return FHyperlink; }
    }

    /// <summary>
    /// Determines if the object can grow.
    /// </summary>
    /// <remarks>
    /// This property is applicable to the bands or text objects that can contain several text lines.
    /// If the property is set to <b>true</b>, object will grow to display all the information that it contains.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool CanGrow
    {
      get { return FCanGrow; }
      set { FCanGrow = value; }
    }

    /// <summary>
    /// Determines if the object can shrink.
    /// </summary>
    /// <remarks>
    /// This property is applicable to the bands or text objects that can contain several text lines.
    /// If the property is set to true, object can shrink to remove the unused space.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool CanShrink
    {
      get { return FCanShrink; }
      set { FCanShrink = value; }
    }

    /// <summary>
    /// Determines if the object must grow to the band's bottom side.
    /// </summary>
    /// <remarks>
    /// If the property is set to true, object grows to the bottom side of its parent. This is useful if
    /// you have several objects on a band, and some of them can grow or shrink.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool GrowToBottom
    {
      get { return FGrowToBottom; }
      set { FGrowToBottom = value; }
    }

    /// <summary>
    /// Gets or sets a shift mode of the object.
    /// </summary>
    /// <remarks>
    /// See <see cref="FastReport.ShiftMode"/> enumeration for details.
    /// </remarks>
    [DefaultValue(ShiftMode.Always)]
    [Category("Behavior")]
    public ShiftMode ShiftMode
    {
      get { return FShiftMode; }
      set { FShiftMode = value; }
    }

    /// <summary>
    /// Gets or sets the style name.
    /// </summary>
    /// <remarks>
    /// Style is a set of common properties such as border, fill, font, text color. The <b>Report</b>
    /// has a set of styles in the <see cref="Report.Styles"/> property. 
    /// </remarks>
    [Editor(typeof(StyleEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public string Style
    {
      get { return FStyle; }
      set
      {
        ApplyStyle(value);
        FStyle = value;
      }
    }

    /// <summary>
    /// Gets or sets a style name that will be applied to even band rows.
    /// </summary>
    /// <remarks>
    /// Style with this name must exist in the <see cref="Report.Styles"/> collection.
    /// </remarks>
    [Editor(typeof(StyleEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public string EvenStyle
    {
      get { return FEvenStyle; }
      set { FEvenStyle = value; }
    }

    /// <summary>
    /// Gets or sets a style name that will be applied to this object when the mouse pointer is over it.
    /// </summary>
    /// <remarks>
    /// Style with this name must exist in the <see cref="Report.Styles"/> collection.
    /// </remarks>
    [Editor(typeof(StyleEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public string HoverStyle
    {
      get { return FHoverStyle; }
      set { FHoverStyle = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines which properties of the even style to use.
    /// </summary>
    /// <remarks>
    /// Usually you will need only the Fill property of the even style to be applied. If you want to 
    /// apply all style settings, set this property to <b>StylePriority.UseAll</b>.
    /// </remarks>
    [DefaultValue(StylePriority.UseFill)]
    [Category("Appearance")]
    public StylePriority EvenStylePriority
    {
      get { return FEvenStylePriority; }
      set { FEvenStylePriority = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines where to print the object.
    /// </summary>
    /// <remarks>
    /// See the <see cref="FastReport.PrintOn"/> enumeration for details.
    /// </remarks>
    [DefaultValue(PrintOn.FirstPage | PrintOn.LastPage | PrintOn.OddPages | PrintOn.EvenPages | PrintOn.RepeatedBand | PrintOn.SinglePage)]
    [Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
    [Category("Behavior")]
    public PrintOn PrintOn
    {
      get { return FPrintOn; }
      set { FPrintOn = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired before the object will be printed in the preview page.
    /// </summary>
    [Category("Build")]
    public string BeforePrintEvent
    {
      get { return FBeforePrintEvent; }
      set { FBeforePrintEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired after the object was printed in the preview page.
    /// </summary>
    [Category("Build")]
    public string AfterPrintEvent
    {
      get { return FAfterPrintEvent; }
      set { FAfterPrintEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired after the object was filled with data.
    /// </summary>
    [Category("Build")]
    public string AfterDataEvent
    {
      get { return FAfterDataEvent; }
      set { FAfterDataEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the user click the object in the preview window.
    /// </summary>
    [Category("Preview")]
    public string ClickEvent
    {
      get { return FClickEvent; }
      set { FClickEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the user 
    /// moves the mouse over the object in the preview window.
    /// </summary>
    [Category("Preview")]
    public string MouseMoveEvent
    {
      get { return FMouseMoveEvent; }
      set { FMouseMoveEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the user 
    /// releases the mouse button in the preview window.
    /// </summary>
    [Category("Preview")]
    public string MouseUpEvent
    {
      get { return FMouseUpEvent; }
      set { FMouseUpEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the user 
    /// clicks the mouse button in the preview window.
    /// </summary>
    [Category("Preview")]
    public string MouseDownEvent
    {
      get { return FMouseDownEvent; }
      set { FMouseDownEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the
    /// mouse enters the object's bounds in the preview window.
    /// </summary>
    [Category("Preview")]
    public string MouseEnterEvent
    {
      get { return FMouseEnterEvent; }
      set { FMouseEnterEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the
    /// mouse leaves the object's bounds in the preview window.
    /// </summary>
    [Category("Preview")]
    public string MouseLeaveEvent
    {
      get { return FMouseLeaveEvent; }
      set { FMouseLeaveEvent = value; }
    }

    /// <inheritdoc/>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Left
    {
      get { return base.Left; }
      set { base.Left = value; }
    }

    /// <inheritdoc/>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Top
    {
      get { return base.Top; }
      set { base.Top = value; }
    }

    /// <inheritdoc/>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Width
    {
      get { return base.Width; }
      set { base.Width = value; }
    }

    /// <inheritdoc/>
    [TypeConverter(typeof(UnitsConverter))]
    public override float Height
    {
      get { return base.Height; }
      set { base.Height = value; }
    }

    /// <summary>
    /// Determines if the object has custom border and use only <b>Border.Width</b>, <b>Border.Style</b> and 
    /// <b>Border.Color</b> properties.
    /// </summary>
    /// <remarks>
    /// This flag is used to disable some toolbar buttons when such object is selected. Applicable to the
    /// ShapeObject and LineObject.
    /// </remarks>
    [Browsable(false)]
    public bool FlagSimpleBorder
    {
      get { return FFlagSimpleBorder; }
      set { FFlagSimpleBorder = value; }
    }

    /// <summary>
    /// Determines if the object uses the <b>Border</b>.
    /// </summary>
    /// <remarks>
    /// This flag is used to disable some toolbar buttons when such object is selected.
    /// </remarks>
    [Browsable(false)]
    public bool FlagUseBorder
    {
      get { return FFlagUseBorder; }
      set { FFlagUseBorder = value; }
    }

    /// <summary>
    /// Determines if the object uses the fill.
    /// </summary>
    /// <remarks>
    /// This flag is used to disable some toolbar buttons when such object is selected.
    /// </remarks>
    [Browsable(false)]
    public bool FlagUseFill
    {
      get { return FFlagUseFill; }
      set { FFlagUseFill = value; }
    }

    /// <summary>
    /// Gets or sets a value indicates that object should not be added to the preview.
    /// </summary>
    [Browsable(false)]
    public bool FlagPreviewVisible
    {
      get { return FFlagPreviewVisible; }
      set { FFlagPreviewVisible = value; }
    }

    /// <summary>
    /// Determines if serializing the Style property is needed.
    /// </summary>
    /// <remarks>
    /// The <b>Style</b> property must be serialized last. Some ancestor classes may turn off the standard Style 
    /// serialization and serialize it by themselves.
    /// </remarks>
    [Browsable(false)]
    public bool FlagSerializeStyle
    {
      get { return FFlagSerializeStyle; }
      set { FFlagSerializeStyle = value; }
    }

    /// <summary>
    /// Determines if an object can provide the hyperlink value automatically.
    /// </summary>
    /// <remarks>
    /// This flag is used in complex objects such as Matrix or Chart. These objects can provide
    /// a hyperlink value automatically, depending on where you click.
    /// </remarks>
    [Browsable(false)]
    public bool FlagProvidesHyperlinkValue
    {
      get { return FFlagProvidesHyperlinkValue; }
      set { FFlagProvidesHyperlinkValue = value; }
    }

    /// <summary>
    /// Gets an object's parent band.
    /// </summary>
    internal BandBase Band
    {
      get
      {
        if (this is BandBase)
          return this as BandBase;

        Base c = Parent;
        while (c != null)
        {
          if (c is BandBase)
            return c as BandBase;
          c = c.Parent;
        }
        return null;
      }
    }

    /// <summary>
    /// Gets an object's parent data band.
    /// </summary>
    internal DataBand DataBand
    {
      get
      {
        if (this is DataBand)
          return this as DataBand;

        Base c = Parent;
        while (c != null)
        {
          if (c is DataBand)
            return c as DataBand;
          c = c.Parent;
        }
        
        ObjectCollection pageBands = Page.AllObjects;
        foreach (Base c1 in pageBands)
        {
          if (c1 is DataBand)
            return c1 as DataBand;
        }
        return null;
      }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeHyperlink()
    {
      return !Hyperlink.Equals(new Hyperlink(null));
    }

    private bool ShouldSerializeBorder()
    {
      return !Border.Equals(new Border());
    }

    private bool ShouldSerializeCursor()
    {
      return Cursor != Cursors.Default;
    }

    private bool ShouldSerializeFill()
    {
      return !(Fill is SolidFill) || (Fill as SolidFill).Color != Color.Transparent;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      ReportComponentBase src = source as ReportComponentBase;
      Printable = src.Printable;
      Exportable = src.Exportable;
      Border = src.Border.Clone();
      Cursor = src.Cursor;
      Fill = src.Fill.Clone();
      Bookmark = src.Bookmark;
      Hyperlink.Assign(src.Hyperlink);
      CanGrow = src.CanGrow;
      CanShrink = src.CanShrink;
      GrowToBottom = src.GrowToBottom;
      ShiftMode = src.ShiftMode;
      FStyle = src.Style;
      EvenStyle = src.EvenStyle;
      HoverStyle = src.HoverStyle;
      EvenStylePriority = src.EvenStylePriority;
      PrintOn = src.PrintOn;
      BeforePrintEvent = src.BeforePrintEvent;
      AfterPrintEvent = src.AfterPrintEvent;
      AfterDataEvent = src.AfterDataEvent;
      ClickEvent = src.ClickEvent;
      MouseMoveEvent = src.MouseMoveEvent;
      MouseUpEvent = src.MouseUpEvent;
      MouseDownEvent = src.MouseDownEvent;
      MouseEnterEvent = src.MouseEnterEvent;
      MouseLeaveEvent = src.MouseLeaveEvent;
    }

    /// <summary>
    /// Copies event handlers from another similar object.
    /// </summary>
    /// <param name="source">The object to copy handlers from.</param>
    public virtual void AssignPreviewEvents(Base source)
    {
      ReportComponentBase src = source as ReportComponentBase;
      if (src == null)
        return;
      Click = src.Click;
      MouseMove = src.MouseMove;
      MouseUp = src.MouseUp;
      MouseDown = src.MouseDown;
      MouseEnter = src.MouseEnter;
      MouseLeave = src.MouseLeave;
    }
    
    /// <summary>
    /// Assigns a format from another, similar object.
    /// </summary>
    /// <param name="source">Source object to assign a format from.</param>
    public virtual void AssignFormat(ReportComponentBase source)
    {
      Border = source.Border.Clone();
      Fill = source.Fill.Clone();
      FStyle = source.Style;
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      base.HandleMouseDown(e);
      // we will use FSavedBounds to keep the delta while moving the object between bands
      FSavedBounds.X = 0;
      FSavedBounds.Y = 0;
    }
    
    /// <inheritdoc/>
    public override void CheckParent(bool immediately)
    {
      if (!(Parent is ComponentBase) || !IsSelected || IsAncestor || Dock != DockStyle.None)
        return;

      if (immediately || 
        Left < 0 || Left > (Parent as ComponentBase).Width || 
        Top < 0 || Top > (Parent as ComponentBase).Height)
      {
        if (HasFlag(Flags.CanChangeParent))
        {
          ObjectCollection list = Page.AllObjects;
          for (int i = list.Count - 1; i >= 0; i--)
          {
            ComponentBase c = list[i] as ComponentBase;
            if (c == null || c == this || !(c is IParent))
              continue;

            if (c != null && (c as IParent).CanContain(this))
            {
              bool inside;
              int bandGap = ReportWorkspace.ClassicView && IsDesigning ? BandBase.HeaderSize : 4;
              if (c is BandBase)
                inside = AbsTop > c.AbsTop - bandGap && AbsTop < c.AbsBottom - 1;
              else
                inside = AbsLeft > c.AbsLeft - 1e-4 && AbsLeft < c.AbsRight - 1e-4 &&
                  AbsTop > c.AbsTop - 1e-4 && AbsTop < c.AbsBottom - 1e-4;

              if (inside)
              {
                if (Parent != c)
                {
                  float saveAbsTop = AbsTop;
                  float saveAbsLeft = AbsLeft;

                  // keep existing offsets if the object is not aligned to the grid
                  float gridXOffset = Converter.DecreasePrecision(Left - (int)(Left / Page.SnapSize.Width + 1e-4) * Page.SnapSize.Width, 2);
                  float gridYOffset = Converter.DecreasePrecision(Top - (int)(Top / Page.SnapSize.Height + 1e-4) * Page.SnapSize.Height, 2);

                  // move the object to the new parent
                  Left = (int)((AbsLeft - c.AbsLeft) / Page.SnapSize.Width + 1e-4) * Page.SnapSize.Width + gridXOffset;
                  Top = (int)((AbsTop - c.AbsTop) / Page.SnapSize.Height + 1e-4) * Page.SnapSize.Height + gridYOffset;
                  Parent = c;

                  // correct the delta
                  FSavedBounds.X += saveAbsLeft - AbsLeft;
                  FSavedBounds.Y += saveAbsTop - AbsTop;

                  // check delta
                  if (Math.Abs(FSavedBounds.X) > Page.SnapSize.Width)
                  {
                    float delta = Math.Sign(FSavedBounds.X) * Page.SnapSize.Width;
                    Left += delta;
                    FSavedBounds.X -= delta;
                  }
                  if (Math.Abs(FSavedBounds.Y) > Page.SnapSize.Height)
                  {
                    float delta = Math.Sign(FSavedBounds.Y) * Page.SnapSize.Height;
                    Top += delta;
                    FSavedBounds.Y -= delta * 0.9f;
                  }
                }
                break;
              }
            }  
          }
        }
        else
        {
          if (Left < 0)
            Left = 0;
          if (Left > (Parent as ComponentBase).Width)
            Left = (Parent as ComponentBase).Width - 2;
          if (Top < 0)
            Top = 0;
          if (Top > (Parent as ComponentBase).Height)
            Top = (Parent as ComponentBase).Height - 2;
        }
      }
    }
    
    /// <summary>
    /// Applies the style settings.
    /// </summary>
    /// <param name="style">Style to apply.</param>
    public virtual void ApplyStyle(Style style)
    {
      if (style.ApplyBorder)
        Border = style.Border.Clone();
      if (style.ApplyFill)
        Fill = style.Fill.Clone();
    }

    internal void ApplyStyle(string style)
    {
      if (!String.IsNullOrEmpty(style) && Report != null)
      {
        StyleCollection styles = Report.Styles;
        int index = styles.IndexOf(style);
        if (index != -1)
          ApplyStyle(styles[index]);
      }
    }
    
    internal void ApplyEvenStyle()
    {
      if (!String.IsNullOrEmpty(EvenStyle) && Report != null)
      {
        StyleCollection styles = Report.Styles;
        int index = styles.IndexOf(EvenStyle);
        if (index != -1)
        {
          Style style = styles[index];
          if (EvenStylePriority == StylePriority.UseFill)
            Fill = style.Fill.Clone();
          else
            ApplyStyle(style);
        }  
      }
    }

    internal void ApplyHoverStyle()
    {
      ApplyStyle(HoverStyle);
    }

    /// <summary>
    /// Saves the current style.
    /// </summary>
    public virtual void SaveStyle()
    {
      FSavedBorder = Border;
      FSavedFill = Fill;
    }

    /// <summary>
    /// Restores the current style.
    /// </summary>
    public virtual void RestoreStyle()
    {
      Border = FSavedBorder;
      Fill = FSavedFill;
    }
    
    /// <summary>
    /// Draws the object's background.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    public void DrawBackground(FRPaintEventArgs e)
    {
      if (Width < 0.01 || Height < 0.01)
        return;
      Fill.Draw(e, AbsBounds);
    }

    /// <summary>
    /// Draws the object's markers.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    public void DrawMarkers(FRPaintEventArgs e)
    {
      if (IsDesigning && Border.Lines != BorderLines.All)
        DrawMarkersInternal(e);
    }
    
    private void DrawMarkersInternal(FRPaintEventArgs e)
    {
      DrawMarkers(e, ReportWorkspace.MarkerStyle);
    }

    /// <summary>
    /// Draws the object's markers.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    /// <param name="style">Marker style</param>
    public void DrawMarkers(FRPaintEventArgs e, MarkerStyle style)
    {
      Graphics g = e.Graphics;
      if (style == MarkerStyle.Corners)
      {
        Pen p = Pens.Black;
        int x = (int)Math.Round(AbsLeft * e.ScaleX);
        int y = (int)Math.Round(AbsTop * e.ScaleY);
        int x1 = (int)Math.Round(AbsRight * e.ScaleX);
        int y1 = (int)Math.Round(AbsBottom * e.ScaleY);
        g.DrawLine(p, x, y, x + 3, y);
        g.DrawLine(p, x, y, x, y + 3);
        g.DrawLine(p, x, y1, x + 3, y1);
        g.DrawLine(p, x, y1, x, y1 - 3);
        g.DrawLine(p, x1, y, x1 - 3, y);
        g.DrawLine(p, x1, y, x1, y + 3);
        g.DrawLine(p, x1, y1, x1 - 3, y1);
        g.DrawLine(p, x1, y1, x1, y1 - 3);
      }
      else if (Math.Abs(Width) > 1 || Math.Abs(Height) > 1)
      {
        g.DrawRectangle(Pens.Gainsboro, AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY);
      }
    }
    
    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      DrawBackground(e);
      base.Draw(e);
    }

    /// <summary>
    /// Determines if the object is visible on current drawing surface.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    public virtual bool IsVisible(FRPaintEventArgs e)
    {
      RectangleF objRect = new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY,
        Width * e.ScaleX + 1, Height * e.ScaleY + 1);
      return e.Graphics.IsVisible(objRect);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ReportComponentBase c = writer.DiffObject as ReportComponentBase;
      base.Serialize(writer);
    
      if (Printable != c.Printable)
        writer.WriteBool("Printable", Printable);
      if (Exportable != c.Exportable)
        writer.WriteBool("Exportable", Exportable);
      Border.Serialize(writer, "Border", c.Border);
      Fill.Serialize(writer, "Fill", c.Fill);
      if (Cursor != c.Cursor)
        writer.WriteValue("Cursor", Cursor);
      Hyperlink.Serialize(writer, c.Hyperlink);
      if (Bookmark != c.Bookmark)
        writer.WriteStr("Bookmark", Bookmark);
      if (writer.SerializeTo != SerializeTo.Preview)
      {
        if (CanGrow != c.CanGrow)
          writer.WriteBool("CanGrow", CanGrow);
        if (CanShrink != c.CanShrink)
          writer.WriteBool("CanShrink", CanShrink);
        if (GrowToBottom != c.GrowToBottom)
          writer.WriteBool("GrowToBottom", GrowToBottom);
        if (ShiftMode != c.ShiftMode)
          writer.WriteValue("ShiftMode", ShiftMode);
        if (FlagSerializeStyle && Style != c.Style)
          writer.WriteStr("Style", Style);
        if (EvenStyle != c.EvenStyle)
          writer.WriteStr("EvenStyle", EvenStyle);
        if (EvenStylePriority != c.EvenStylePriority)
          writer.WriteValue("EvenStylePriority", EvenStylePriority);
        if (HoverStyle != c.HoverStyle)
          writer.WriteStr("HoverStyle", HoverStyle);
        if (PrintOn != c.PrintOn)
          writer.WriteValue("PrintOn", PrintOn);
        if (BeforePrintEvent != c.BeforePrintEvent)
          writer.WriteStr("BeforePrintEvent", BeforePrintEvent);
        if (AfterPrintEvent != c.AfterPrintEvent)
          writer.WriteStr("AfterPrintEvent", AfterPrintEvent);
        if (AfterDataEvent != c.AfterDataEvent)
          writer.WriteStr("AfterDataEvent", AfterDataEvent);
        if (ClickEvent != c.ClickEvent)
          writer.WriteStr("ClickEvent", ClickEvent);
        if (MouseMoveEvent != c.MouseMoveEvent)
          writer.WriteStr("MouseMoveEvent", MouseMoveEvent);
        if (MouseUpEvent != c.MouseUpEvent)
          writer.WriteStr("MouseUpEvent", MouseUpEvent);
        if (MouseDownEvent != c.MouseDownEvent)
          writer.WriteStr("MouseDownEvent", MouseDownEvent);
        if (MouseEnterEvent != c.MouseEnterEvent)
          writer.WriteStr("MouseEnterEvent", MouseEnterEvent);
        if (MouseLeaveEvent != c.MouseLeaveEvent)
          writer.WriteStr("MouseLeaveEvent", MouseLeaveEvent);
      }
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new ReportComponentBaseMenu(Report.Designer);
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      if (Page is ReportPage && (Page as ReportPage).IsImperialUnitsUsed)
        return new SizeF(Units.Inches * 1, Units.Inches * 0.2f);
      return base.GetPreferredSize();
    }
    
    /// <inheritdoc/>
    public override void OnAfterInsert(InsertFrom source)
    {
      if (this is IHasEditor && source == InsertFrom.NewObject && ReportWorkspace.EditAfterInsert)
        (this as IHasEditor).InvokeEditor();
    }

    /// <inheritdoc/>
    public override void OnAfterLoad()
    {
      // if hyperlink is set to external report, we need to fix relative path to report
      Hyperlink.OnAfterLoad();
    }
    #endregion

    #region Report Engine
    /// <summary>
    /// Initializes the object before running a report.
    /// </summary>
    /// <remarks>
    /// This method is used by the report engine, do not call it directly.
    /// </remarks>
    public virtual void InitializeComponent()
    {
      // update the component's style
      Style = Style;
    }

    /// <summary>
    /// Performs a finalization after the report is finished.
    /// </summary>
    /// <remarks>
    /// This method is used by the report engine, do not call it directly.
    /// </remarks>
    public virtual void FinalizeComponent()
    {
    }

    /// <summary>
    /// Saves the object's state before printing it.
    /// </summary>
    /// <remarks>
    /// This method is called by the report engine before processing the object. 
    /// <para/>Do not call it directly. You may override it if you are developing a new FastReport component. 
    /// In this method you should save any object properties that may be changed during the object printing. 
    /// The standard implementation saves the object's bounds, visibility, bookmark and hyperlink.
    /// </remarks>
    public virtual void SaveState()
    {
      FSavedBounds = Bounds;
      FSavedVisible = Visible;
      FSavedBookmark = Bookmark;
      FSavedBorder = Border;
      FSavedFill = Fill;
      Hyperlink.SaveState();
    }

    /// <summary>
    /// Restores the object's state after printing it.
    /// </summary>
    /// <remarks>
    /// This method is called by the report engine after processing the object.
    /// <para/>Do not call it directly. You may override it if you are developing a new FastReport component. 
    /// In this method you should restore the object properties that were saved by the <see cref="SaveState"/> method.
    /// </remarks>
    public virtual void RestoreState()
    {
      Bounds = FSavedBounds;
      Visible = FSavedVisible;
      Bookmark = FSavedBookmark;
      Hyperlink.RestoreState();
      Border = FSavedBorder;
      Fill = FSavedFill;
    }

    /// <summary>
    /// Calculates the object's height.
    /// </summary>
    /// <returns>Actual object's height, in pixels.</returns>
    /// <remarks>
    /// Applicable to objects that contain several text lines, such as TextObject. Returns the height needed
    /// to display all the text lines.
    /// </remarks>
    public virtual float CalcHeight()
    {
      return Height;
    }

    /// <summary>
    /// Gets the data from a datasource that the object is connected to.
    /// </summary>
    /// <remarks>
    /// This method is called by the report engine before processing the object.
    /// <para/>Do not call it directly. You may override it if you are developing a new FastReport component. 
    /// In this method you should get the data from a datasource that the object is connected to.
    /// </remarks>
    public virtual void GetData()
    {
      Hyperlink.Calculate();
      
      if (!String.IsNullOrEmpty(Bookmark))
      {
        object value = Report.Calc(Bookmark);
        Bookmark = value == null ? "" : value.ToString();
      }
    }
    
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      if (!String.IsNullOrEmpty(Hyperlink.Expression))
        expressions.Add(Hyperlink.Expression);
      if (!String.IsNullOrEmpty(Bookmark))
        expressions.Add(Bookmark);
      return expressions.ToArray();
    }

    /// <summary>
    /// This method fires the <b>BeforePrint</b> event and the script code connected to the <b>BeforePrintEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnBeforePrint(EventArgs e)
    {
      if (BeforePrint != null)
        BeforePrint(this, e);
      InvokeEvent(BeforePrintEvent, e);
    }

    /// <summary>
    /// This method fires the <b>AfterPrint</b> event and the script code connected to the <b>AfterPrintEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnAfterPrint(EventArgs e)
    {
      if (AfterPrint != null)
        AfterPrint(this, e);
      InvokeEvent(AfterPrintEvent, e);
    }

    /// <summary>
    /// This method fires the <b>AfterData</b> event and the script code connected to the <b>AfterDataEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnAfterData(EventArgs e)
    {
      if (AfterData != null)
        AfterData(this, e);
      InvokeEvent(AfterDataEvent, e);
    }

    internal void OnAfterData()
    {
      OnAfterData(EventArgs.Empty);
    }

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
    /// This method fires the <b>MouseEnter</b> event and the script code connected to the <b>MouseEnterEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseEnter(EventArgs e)
    {
      if (!String.IsNullOrEmpty(HoverStyle))
      {
        SaveStyle();
        ApplyHoverStyle();
        if (Page != null)
          Page.Refresh();
      }
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
      if (!String.IsNullOrEmpty(HoverStyle))
      {
        RestoreStyle();
        if (Page != null)
          Page.Refresh();
      }
      if (MouseLeave != null)
        MouseLeave(this, e);
      InvokeEvent(MouseLeaveEvent, e);
    }

    /// <summary>
    /// This method is fired when the user scrolls the mouse in the preview window.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMouseWheel(MouseEventArgs e)
    {
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportComponentBase"/> class with default settings.
    /// </summary>
    public ReportComponentBase()
    {
      FBorder = new Border();
      FFill = new SolidFill();
      FCursor = Cursors.Default;
      FHyperlink = new Hyperlink(this);
      FBookmark = "";
      FPrintable = true;
      FExportable = true;
      FFlagUseFill = true;
      FFlagUseBorder = true;
      FFlagPreviewVisible = true;
      FFlagSerializeStyle = true;
      FShiftMode = ShiftMode.Always;
      FStyle = "";
      FEvenStyle = "";
      FHoverStyle = "";
      FPrintOn = PrintOn.FirstPage | PrintOn.LastPage | PrintOn.OddPages | PrintOn.EvenPages | PrintOn.RepeatedBand | PrintOn.SinglePage;
      FBeforePrintEvent = "";
      FAfterPrintEvent = "";
      FAfterDataEvent = "";
      FClickEvent = "";
      FMouseMoveEvent = "";
      FMouseUpEvent = "";
      FMouseDownEvent = "";
      FMouseEnterEvent = "";
      FMouseLeaveEvent = "";
      SetFlags(Flags.CanGroup, true);
      if (BaseName.EndsWith("Object"))
        BaseName = ClassName.Substring(0, ClassName.Length - 6);
    }

  }

}