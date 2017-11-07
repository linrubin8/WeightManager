using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Design.PageDesigners.Page;
using FastReport.TypeEditors;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Base class for all bands.
  /// </summary>
  public abstract class BandBase : BreakableComponent, IParent
  {
    #region Fields
    private ChildBand FChild;
    private ReportComponentCollection FObjects;
    private FloatCollection FGuides;
    private bool FStartNewPage;
    private bool FFirstRowStartsNewPage;
    private bool FPrintOnBottom;
    private bool FKeepChild;
    private string FOutlineExpression;
    private int FRowNo;
    private int FAbsRowNo;
    private bool FIsFirstRow;
    private bool FIsLastRow;
    private bool FRepeated;
    private bool FUpdatingLayout;
    private bool FFlagUseStartNewPage;
    private bool FFlagCheckFreeSpace;
    private bool FFlagMustBreak;
    private int FSavedOriginalObjectsCount;
    private float FReprintOffset;
    private string FBeforeLayoutEvent;
    private string FAfterLayoutEvent;
    internal static int HeaderSize = 20;
    #endregion

    #region Properties
    /// <summary>
    /// This event occurs before the band layouts its child objects.
    /// </summary>
    public event EventHandler BeforeLayout;

    /// <summary>
    /// This event occurs after the child objects layout was finished.
    /// </summary>
    public event EventHandler AfterLayout;

    /// <summary>
    /// Gets or sets a value indicating that the band should be printed from a new page.
    /// </summary>
    /// <remarks>
    /// New page is not generated when printing very first group or data row. This is made to avoid empty
    /// first page.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool StartNewPage
    {
      get { return FStartNewPage; }
      set { FStartNewPage = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the first row can start a new report page.
    /// </summary>
    /// <remarks>
    /// Use this property if <see cref="StartNewPage"/> is set to <b>true</b>. Normally the new page
    /// is not started when printing the first data row, to avoid empty first page. 
    /// </remarks>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool FirstRowStartsNewPage
    {
      get { return FFirstRowStartsNewPage; }
      set { FFirstRowStartsNewPage = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the band should be printed on the page bottom.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PrintOnBottom
    {
      get { return FPrintOnBottom; }
      set { FPrintOnBottom = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the band should be printed together with its child band.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool KeepChild
    {
      get { return FKeepChild; }
      set { FKeepChild = value; }
    }

    /// <summary>
    /// Gets or sets an outline expression.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Outline is a tree control displayed in the preview window. It represents the prepared report structure. 
    /// Each outline node can be clicked to navigate to the item in the prepared report.
    /// </para>
    /// <para>
    /// To create the outline, set this property to any valid expression that represents the outline node text. 
    /// This expression will be calculated when band is about to print, and its value will be added to the
    /// outline. Thus, nodes' hierarchy in the outline is similar to the bands' hierarchy 
    /// in a report. That means there will be the main and subordinate outline nodes, corresponding 
    /// to the main and subordinate bands in a report (a report with two levels of data or with groups can 
    /// exemplify the point).
    /// </para>
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Navigation")]
    public string OutlineExpression
    {
      get { return FOutlineExpression; }
      set { FOutlineExpression = value; }
    }

    /// <summary>
    /// Gets or sets a child band that will be printed right after this band.
    /// </summary>
    /// <remarks>
    /// Typical use of child band is to print several objects that can grow or shrink. It also can be done
    /// using the shift feature (via <see cref="ShiftMode"/> property), but in some cases it's not possible.
    /// </remarks>
    [Browsable(false)]
    public ChildBand Child
    {
      get { return FChild; }
      set 
      { 
        SetProp(FChild, value);
        FChild = value;
      }
    }

    /// <summary>
    /// Gets a collection of report objects belongs to this band.
    /// </summary>
    [Browsable(false)]
    public ReportComponentCollection Objects
    {
      get { return FObjects; }
    }

    /// <summary>
    /// Gets a value indicating that band is reprinted on a new page.
    /// </summary>
    /// <remarks>
    /// This property is applicable to the <b>DataHeaderBand</b> and <b>GroupHeaderBand</b> only. 
    /// It returns <b>true</b> if its <b>RepeatOnAllPages</b> property is <b>true</b> and band is 
    /// reprinted on a new page.
    /// </remarks>
    [Browsable(false)]
    public bool Repeated
    {
      get { return FRepeated; }
      set 
      { 
        FRepeated = value;
        // set this flag for child bands as well
        BandBase child = Child;
        while (child != null)
        {
          child.Repeated = value;
          child = child.Child;
        }
      }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired before the band layouts its child objects.
    /// </summary>
    [Category("Build")]
    public string BeforeLayoutEvent
    {
      get { return FBeforeLayoutEvent; }
      set { FBeforeLayoutEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired after the child objects layout was finished.
    /// </summary>
    [Category("Build")]
    public string AfterLayoutEvent
    {
      get { return FAfterLayoutEvent; }
      set { FAfterLayoutEvent = value; }
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
    public override float AbsLeft
    {
      get { return IsRunning ? base.AbsLeft : Left; }
    }

    /// <inheritdoc/>
    public override float AbsTop
    {
      get { return IsRunning ? base.AbsTop : Top; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override DockStyle Dock
    {
      get { return base.Dock; }
      set { base.Dock = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override AnchorStyles Anchor
    {
      get { return base.Anchor; }
      set { base.Anchor = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool GrowToBottom
    {
      get { return base.GrowToBottom; }
      set { base.GrowToBottom = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new ShiftMode ShiftMode
    {
      get { return base.ShiftMode; }
      set { base.ShiftMode = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [DefaultValue(false)]
    public new bool CanBreak
    {
      get { return base.CanBreak; }
      set { base.CanBreak = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new BreakableComponent BreakTo
    {
      get { return base.BreakTo; }
      set { base.BreakTo = value; }
    }

    /// <summary>
    /// Gets or sets collection of guide lines for this band.
    /// </summary>
    [Browsable(false)]
    public FloatCollection Guides
    {
      get { return FGuides; }
      set { FGuides = value; }
    }

    /// <summary>
    /// Gets a row number (the same value returned by the "Row#" system variable).
    /// </summary>
    /// <remarks>
    /// This property can be used when running a report. It may be useful to print hierarchical 
    /// row numbers in a master-detail report, like this:
    /// <para/>1.1
    /// <para/>1.2
    /// <para/>2.1
    /// <para/>2.2
    /// <para/>To do this, put the Text object on a detail data band with the following text in it:
    /// <para/>[Data1.RowNo].[Data2.RowNo]
    /// </remarks>
    [Browsable(false)]
    public int RowNo
    {
      get { return FRowNo; }
      set 
      { 
        FRowNo = value;
        if (Child != null)
          Child.RowNo = value;
      }
    }

    /// <summary>
    /// Gets an absolute row number (the same value returned by the "AbsRow#" system variable).
    /// </summary>
    [Browsable(false)]
    public int AbsRowNo
    {
      get {
            return FAbsRowNo;
          }

      set 
      { 
        FAbsRowNo = value;
        if (Child != null)
          Child.AbsRowNo = value;
      }
    }

    /// <summary>
    /// Gets a value indicating that this is the first data row.
    /// </summary>
    [Browsable(false)]
    public bool IsFirstRow
    {
      get { return FIsFirstRow; }
      set { FIsFirstRow = value; }
    }

    /// <summary>
    /// Gets a value indicating that this is the last data row.
    /// </summary>
    [Browsable(false)]
    public bool IsLastRow
    {
      get { return FIsLastRow; }
      set { FIsLastRow = value; }
    }

    internal bool HasBorder
    {
      get { return !Border.Equals(new Border()); }
    }
    
    internal bool HasFill
    {
      get { return !(Fill is SolidFill) || (Fill as SolidFill).Color != Color.Transparent; }
    }
    
    internal bool CanDelete
    {
      get 
      { 
        // do not delete the sole band on the page
        ObjectCollection pageObjects = Page.AllObjects;
        ObjectCollection bands = new ObjectCollection();
        foreach (Base obj in pageObjects)
        {
          // fix: it was possible to delete the band if it has a child band
          if (obj is BandBase && (obj == this || !(obj is ChildBand)))
            bands.Add(obj);
        }
        return bands.Count > 1;
      }
    }
    
    internal DataBand ParentDataBand
    {
      get
      {
        Base c = Parent;
        while (c != null)
        {
          if (c is DataBand)
            return c as DataBand;
          if (c is ReportPage && (c as ReportPage).Subreport != null)
            c = (c as ReportPage).Subreport;
          c = c.Parent;
        }
        return null;
      }
    }
    
    internal bool FlagUseStartNewPage
    {
      get { return FFlagUseStartNewPage; }
      set { FFlagUseStartNewPage = value; }
    }
    
    internal bool FlagCheckFreeSpace
    {
      get { return FFlagCheckFreeSpace; }
      set 
      { 
        FFlagCheckFreeSpace = value;
        // set flag for child bands as well
        BandBase child = Child;
        while (child != null)
        {
          child.FlagCheckFreeSpace = value;
          child = child.Child;
        }
      }
    }
    
    internal bool FlagMustBreak
    {
      get { return FFlagMustBreak; }
      set { FFlagMustBreak = value; }
    }
    
    internal float ReprintOffset
    {
      get { return FReprintOffset; }
      set { FReprintOffset = value; }
    }

    internal float PageWidth
    {
      get
      {
        ReportPage page = Page as ReportPage;
        if (page != null)
          return page.WidthInPixels - (page.LeftMargin + page.RightMargin) * Units.Millimeters;
        return 0;
      }
    }

    private float DesignWidth
    {
      get
      {
        ReportPage page = Page as ReportPage;
        if (page != null && page.ExtraDesignWidth)
        {
          if (page.Columns.Count <= 1 || !IsColumnDependentBand)
            return Width * 5;
        }
        return Width;
      }
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      foreach (ReportComponentBase obj in FObjects)
      {
        list.Add(obj);
      }
      if (!IsRunning)
        list.Add(FChild);
    }

    /// <inheritdoc/>
    public virtual bool CanContain(Base child)
    {
      if (IsRunning)
        return child is ReportComponentBase;
      return ((child is ReportComponentBase && !(child is BandBase)) || child is ChildBand);
    }

    /// <inheritdoc/>
    public virtual void AddChild(Base child)
    {
      if (child is ChildBand && !IsRunning)
        Child = child as ChildBand;
      else
        FObjects.Add(child as ReportComponentBase);
    }

    /// <inheritdoc/>
    public virtual void RemoveChild(Base child)
    {
      if (child is ChildBand && FChild == child as ChildBand)
        Child = null;
      else
        FObjects.Remove(child as ReportComponentBase);
    }

    /// <inheritdoc/>
    public virtual int GetChildOrder(Base child)
    {
      return FObjects.IndexOf(child as ReportComponentBase);
    }

    /// <inheritdoc/>
    public virtual void SetChildOrder(Base child, int order)
    {
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (order > FObjects.Count)
          order = FObjects.Count;
        if (oldOrder <= order)
          order--;
        FObjects.Remove(child as ReportComponentBase);
        FObjects.Insert(order, child as ReportComponentBase);
        UpdateLayout(0, 0);
      }
    }

    /// <inheritdoc/>
    public virtual void UpdateLayout(float dx, float dy)
    {
      if (FUpdatingLayout)
        return;
      FUpdatingLayout = true;  
      try
      {
        RectangleF remainingBounds = new RectangleF(0, 0, Width, Height);
        remainingBounds.Width += dx;
        remainingBounds.Height += dy;
        foreach (ReportComponentBase c in Objects)
        {
          if ((c.Anchor & AnchorStyles.Right) != 0)
          {
            if ((c.Anchor & AnchorStyles.Left) != 0)
              c.Width += dx;
            else
              c.Left += dx;
          }
          else if ((c.Anchor & AnchorStyles.Left) == 0)
          {
            c.Left += dx / 2;
          }
          if ((c.Anchor & AnchorStyles.Bottom) != 0)
          {
            if ((c.Anchor & AnchorStyles.Top) != 0)
              c.Height += dy;
            else
              c.Top += dy;
          }
          else if ((c.Anchor & AnchorStyles.Top) == 0)
          {
            c.Top += dy / 2;
          }
          switch (c.Dock)
          {
            case DockStyle.Left:
              c.Bounds = new RectangleF(remainingBounds.Left, remainingBounds.Top, c.Width, remainingBounds.Height);
              remainingBounds.X += c.Width;
              remainingBounds.Width -= c.Width;
              break;
              
            case DockStyle.Top:
              c.Bounds = new RectangleF(remainingBounds.Left, remainingBounds.Top, remainingBounds.Width, c.Height);
              remainingBounds.Y += c.Height;
              remainingBounds.Height -= c.Height;
              break;
              
            case DockStyle.Right:
              c.Bounds = new RectangleF(remainingBounds.Right - c.Width, remainingBounds.Top, c.Width, remainingBounds.Height);
              remainingBounds.Width -= c.Width;
              break;
              
            case DockStyle.Bottom:
              c.Bounds = new RectangleF(remainingBounds.Left, remainingBounds.Bottom - c.Height, remainingBounds.Width, c.Height);
              remainingBounds.Height -= c.Height;
              break;
              
            case DockStyle.Fill:
              c.Bounds = remainingBounds;
              remainingBounds.Width = 0;
              remainingBounds.Height = 0;
              break;        
          }
        }
      }
      finally
      {  
        FUpdatingLayout = false;
      }  
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      BandBase src = source as BandBase;
      Guides.Assign(src.Guides);
      StartNewPage = src.StartNewPage;
      FirstRowStartsNewPage = src.FirstRowStartsNewPage;
      PrintOnBottom = src.PrintOnBottom;
      KeepChild = src.KeepChild;
      OutlineExpression = src.OutlineExpression;
      BeforeLayoutEvent = src.BeforeLayoutEvent;
      AfterLayoutEvent = src.AfterLayoutEvent;
    }
    
    internal virtual void UpdateWidth()
    {
      // update band width. It is needed for anchor/dock
      ReportPage page = Page as ReportPage;
      if (page != null)
      {
        if (page.Columns.Count <= 1 || !IsColumnDependentBand)
          Width = PageWidth;
      }
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      Graphics g = e.Graphics;

      UpdateWidth();
      if (IsDesigning)
      {
        RectangleF bounds = Bounds;
        bounds.X *= e.ScaleX;
        bounds.Y *= e.ScaleY;
        bounds.Width = DesignWidth * e.ScaleX;
        bounds.Height *= e.ScaleY;

        if (ReportWorkspace.ClassicView && Width != 0)
        {
          RectangleF fillRect = new RectangleF(bounds.Left, bounds.Top - (HeaderSize - 1) * e.ScaleY, 
            bounds.Width, (HeaderSize - 1) * e.ScaleY);
          if (Bounds.Top == HeaderSize)
          {
            fillRect.Y = 0;
            fillRect.Height += e.ScaleY;
          }
          DrawBandHeader(g, fillRect, true);
          
          ObjectInfo info = RegisteredObjects.FindObject(this);
          string text = Res.Get(info.Text);
          if (GetInfoText() != "")
            text += ": " + GetInfoText();
          fillRect.X += 4;
          TextRenderer.DrawText(g, text, DrawUtils.Default96Font, 
            new Rectangle((int)fillRect.Left, (int)fillRect.Top, (int)fillRect.Width, (int)fillRect.Height),
            SystemColors.WindowText, TextFormatFlags.VerticalCenter);
        }
        
        g.FillRectangle(SystemBrushes.Window, bounds.Left, (int)Math.Round(bounds.Top),
          bounds.Width, bounds.Height + (ReportWorkspace.ClassicView ? 1 : 4));
        DrawBackground(e);
        if (ReportWorkspace.ShowGrid)
          ReportWorkspace.Grid.Draw(g, bounds, e.ScaleX);
        
        if (!ReportWorkspace.ClassicView)
        {
          Pen pen = e.Cache.GetPen(Color.Silver, 1, DashStyle.Dot);
          g.DrawLine(pen, bounds.Left, bounds.Bottom + 1, bounds.Right + 1, bounds.Bottom + 1);
          g.DrawLine(pen, bounds.Left + 1, bounds.Bottom + 2, bounds.Right + 1, bounds.Bottom + 2);
          g.DrawLine(pen, bounds.Left, bounds.Bottom + 3, bounds.Right + 1, bounds.Bottom + 3);
        }
      }
      else
      {      
        DrawBackground(e);
        Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));
      }  
    }

    /// <inheritdoc/>
    public override void DrawSelection(FRPaintEventArgs e)
    {
      DrawSelectionPoint(e, Pens.Black, Brushes.White, Left + Width / 2, Top + Height + 2);
    }

    /// <inheritdoc/>
    public override bool PointInObject(PointF point)
    {
      if (ReportWorkspace.ClassicView && IsDesigning)
        return new RectangleF(Left, Top - (HeaderSize - 1), DesignWidth, Height + HeaderSize - 1).Contains(point);
      return new RectangleF(Left, Top, DesignWidth, Height).Contains(point);
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      base.HandleMouseDown(e);
      if (e.Handled)
      {
        if (e.ModifierKeys != Keys.Shift)
        {
          e.Mode = WorkspaceMode2.SelectionRect;
          e.ActiveObject = this;
        }  
      }  
    }

    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      base.HandleMouseHover(e);
      if (e.Handled)
        e.Cursor = Cursors.Default;
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      PointF point = new PointF(e.X, e.Y);

      if (e.Button == MouseButtons.None)
      {
        e.SizingPoint = SizingPoint.None;
        if (point.Y > Bounds.Bottom && point.Y < Bounds.Bottom + 4)
        {
          e.Mode = WorkspaceMode2.Size;
          e.SizingPoint = SizingPoint.BottomCenter;
          e.Cursor = Cursors.HSplit;
          e.Handled = true;
        }
      }
      else
      {
        if (e.ActiveObject == this && e.Mode == WorkspaceMode2.Size)
        {
          if (e.SizingPoint == SizingPoint.BottomCenter)
          {
            Height += e.Delta.Y;
            FixHeight();
          }
          e.Handled = true;
        }
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      if (e.Mode == WorkspaceMode2.SelectionRect)
      {
        ObjectCollection selectedList = new ObjectCollection();
        // find objects inside the selection rect
        for (int i = 0; i < Report.Designer.Objects.Count; i++)
        {
          Base c = Report.Designer.Objects[i];
          if (c is ComponentBase && !(c is BandBase))
          {
            e.Handled = false;
            (c as ComponentBase).HandleMouseUp(e);
            // object is inside
            if (e.Handled)
              selectedList.Add(c);
          }
        }
        if (selectedList.Count > 0)
          selectedList.CopyTo(Report.Designer.SelectedObjects);
      }
      FixHeight();
    }

    internal void FixHeight()
    {
      float maxHeight = Height;
      foreach (ReportComponentBase c in Objects)
      {
        if (!(c is CrossBandObject) && c.Bottom > maxHeight)
          maxHeight = c.Bottom;
      }
      if (maxHeight < 0)
        maxHeight = 0;
      Height = maxHeight;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      BandBase c = writer.DiffObject as BandBase;
      base.Serialize(writer);
      if (writer.SerializeTo == SerializeTo.Preview)
        return;

      if (StartNewPage != c.StartNewPage)
        writer.WriteBool("StartNewPage", StartNewPage);
      if (FirstRowStartsNewPage != c.FirstRowStartsNewPage)
        writer.WriteBool("FirstRowStartsNewPage", FirstRowStartsNewPage);
      if (PrintOnBottom != c.PrintOnBottom)
        writer.WriteBool("PrintOnBottom", PrintOnBottom);
      if (KeepChild != c.KeepChild)
        writer.WriteBool("KeepChild", KeepChild);
      if (OutlineExpression != c.OutlineExpression)
        writer.WriteStr("OutlineExpression", OutlineExpression);
      if (Guides.Count > 0)
        writer.WriteValue("Guides", Guides);
      if (BeforeLayoutEvent != c.BeforeLayoutEvent)
        writer.WriteStr("BeforeLayoutEvent", BeforeLayoutEvent);
      if (AfterLayoutEvent != c.AfterLayoutEvent)
        writer.WriteStr("AfterLayoutEvent", AfterLayoutEvent);
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      SizeF result = new SizeF(0, 0);
      switch (ReportWorkspace.Grid.GridUnits)
      {
        case PageUnits.Millimeters:
          result = new SizeF(Units.Millimeters * 10, Units.Millimeters * 10);
          break;
        case PageUnits.Centimeters:
          result = new SizeF(Units.Centimeters * 1, Units.Centimeters * 1);
          break;
        case PageUnits.Inches:
          result = new SizeF(Units.Inches * 0.5f, Units.Inches * 0.5f);
          break;
        case PageUnits.HundrethsOfInch:
          result = new SizeF(Units.HundrethsOfInch * 50, Units.HundrethsOfInch * 50);
          break;
      }
      return result;
    }

    /// <inheritdoc/>
    public override void Delete()
    {
      if (CanDelete)
        Dispose();
    }
    
    internal virtual string GetInfoText()
    {
      return "";
    }

    internal void DrawBandHeader(Graphics g, RectangleF rect, bool drawTopLine)
    {
      Color color1 = Color.Empty;
      
      if (this is GroupHeaderBand || this is GroupFooterBand)
        color1 = Color.FromArgb(144, 228, 0);
      else if (this is DataBand)
        color1 = Color.FromArgb(255, 144, 0);
      
      Color color2 = Color.FromArgb(100, color1);
      Color color3 = Color.FromArgb(180, Color.White);
      Color color4 = Color.Transparent;

      g.FillRectangle(Brushes.White, rect);

      using (LinearGradientBrush b = new LinearGradientBrush(rect, color1, color2, 90))
      {
        g.FillRectangle(b, rect);
      }

      rect.Height /= 3;
      using (LinearGradientBrush b = new LinearGradientBrush(rect, color3, color4, 90))
      {
        g.FillRectangle(b, rect);
      }

      if (drawTopLine)
      {
        using (Pen p = new Pen(color1))
        {
          g.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Top);
        }
      }  
    }
    
    internal bool IsColumnDependentBand
    {
      get
      {
        BandBase b = this;
        if (b is ChildBand)
        {
          while (b is ChildBand)
          {
            b = b.Parent as BandBase;
          }
        }
        if (b is DataHeaderBand || b is DataBand || b is DataFooterBand ||
          b is GroupHeaderBand || b is GroupFooterBand ||
          b is ColumnHeaderBand || b is ColumnFooterBand || b is ReportSummaryBand)
          return true;
        return false;
      }
    }
    #endregion
    
    #region Report Engine
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      if (!String.IsNullOrEmpty(OutlineExpression))
        expressions.Add(OutlineExpression);
      
      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();
      FSavedOriginalObjectsCount = Objects.Count;
      SetRunning(true);
      SetDesigning(false);
      OnBeforePrint(EventArgs.Empty);

      foreach (ReportComponentBase obj in Objects)
      {
        obj.SaveState();
        obj.SetRunning(true);
        obj.SetDesigning(false);
        obj.OnBeforePrint(EventArgs.Empty);
      }

      // apply even style
      if (RowNo % 2 == 0)
      {
        ApplyEvenStyle();

        foreach (ReportComponentBase obj in Objects)
        {
          obj.ApplyEvenStyle();
        }
      }
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      OnAfterPrint(EventArgs.Empty);
      base.RestoreState();
      while (Objects.Count > FSavedOriginalObjectsCount)
      {
        Objects[Objects.Count - 1].Dispose();
      }
      SetRunning(false);

      foreach (ReportComponentBase obj in Objects)
      {
        obj.OnAfterPrint(EventArgs.Empty);
        obj.RestoreState();
        obj.SetRunning(false);
      }
    }

    /// <inheritdoc/>
    public override float CalcHeight()
    {
      OnBeforeLayout(EventArgs.Empty);

      // sort objects by Top
      ReportComponentCollection sortedObjects = Objects.SortByTop();
      
      // calc height of each object
      float[] heights = new float[sortedObjects.Count];
      for (int i = 0; i < sortedObjects.Count; i++)
      {
        ReportComponentBase obj = sortedObjects[i];
        float height = obj.Height;
        if (obj.Visible && (obj.CanGrow || obj.CanShrink))
        {
          float height1 = obj.CalcHeight();
          if ((obj.CanGrow && height1 > height) || (obj.CanShrink && height1 < height))
            height = height1;
        }
        heights[i] = height;
      }

      // calc shift amounts
      float[] shifts = new float[sortedObjects.Count];
      for (int i = 0; i < sortedObjects.Count; i++)
      {
        ReportComponentBase parent = sortedObjects[i];
        float shift = heights[i] - parent.Height;
        if (shift == 0)
          continue;

        for (int j = i + 1; j < sortedObjects.Count; j++)
        {
          ReportComponentBase child = sortedObjects[j];
          if (child.ShiftMode == ShiftMode.Never)
            continue;
          
          if (child.Top >= parent.Bottom - 1e-4)
          {
            if (child.ShiftMode == ShiftMode.WhenOverlapped &&
              (child.Left > parent.Right - 1e-4 || parent.Left > child.Right - 1e-4))
              continue;

            float parentShift = shifts[i];
            float childShift = shifts[j];
            if (shift > 0)
              childShift = Math.Max(shift + parentShift, childShift);
            else
              childShift = Math.Min(shift + parentShift, childShift);
            shifts[j] = childShift;  
          }
        }
      }
      
      // update location and size of each component, calc max height
      float maxHeight = 0;
      for (int i = 0; i < sortedObjects.Count; i++)
      {
        ReportComponentBase obj = sortedObjects[i];
        DockStyle saveDock = obj.Dock;
        obj.Dock = DockStyle.None;
        obj.Height = heights[i];
        obj.Top += shifts[i];
        if (obj.Visible && obj.Bottom > maxHeight)
          maxHeight = obj.Bottom;
        obj.Dock = saveDock;
      }
      
      if ((CanGrow && maxHeight > Height) || (CanShrink && maxHeight < Height))
        Height = maxHeight;

      // perform grow to bottom
      foreach (ReportComponentBase obj in Objects)
      {
        if (obj.GrowToBottom)
          obj.Height = Height - obj.Top;
      }
      
      OnAfterLayout(EventArgs.Empty);
      return Height;  
    }

    /// <inheritdoc/>
    public override bool Break(BreakableComponent breakTo)
    {
      // first we find the break line. It's a minimum Top coordinate of the object that cannot break.
      float breakLine = Height;
      bool breakLineFound = true;
      do
      {
        breakLineFound = true;
        foreach (ReportComponentBase obj in Objects)
        {
          bool canBreak = true;
          if (obj.Top < breakLine && obj.Bottom > breakLine)
          {
            canBreak = false;
            BreakableComponent breakable = obj as BreakableComponent;
            if (breakable != null && breakable.CanBreak)
            {
              using (BreakableComponent clone = Activator.CreateInstance(breakable.GetType()) as BreakableComponent)
              {
                clone.AssignAll(breakable);
                clone.Height = breakLine - clone.Top;
                // to allow access to the Report
                clone.Parent = breakTo;
                canBreak = clone.Break(null);
              }
            }  
          }

          if (!canBreak)
          {
            breakLine = Math.Min(obj.Top, breakLine);
            // enumerate objects again
            breakLineFound = false;
            break;
          }
        }
      }
      while (!breakLineFound);
      
      // now break the components
      int i = 0;
      while (i < Objects.Count)
      {
        ReportComponentBase obj = Objects[i];
        if (obj.Bottom > breakLine)
        {
          if (obj.Top < breakLine)
          {
            BreakableComponent breakComp = Activator.CreateInstance(obj.GetType()) as BreakableComponent;
            breakComp.AssignAll(obj);
            breakComp.Parent = breakTo;

            breakComp.CanGrow = true;
            breakComp.CanShrink = false;
            breakComp.Height -= breakLine - obj.Top;
            breakComp.Top = 0;
            obj.Height = breakLine - obj.Top;
            (obj as BreakableComponent).Break(breakComp);
          }
          else
          {
            obj.Top -= breakLine;
            obj.Parent = breakTo;
            continue;
          }
        }
        i++;
      }
      
      Height = breakLine;
      breakTo.Height -= breakLine;
      return Objects.Count > 0;
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();
      foreach (ReportComponentBase obj in Objects)
      {
        obj.GetData();
        obj.OnAfterData();

        // break the component if it is of BreakableComponent an has non-empty BreakTo property
        if (obj is BreakableComponent && (obj as BreakableComponent).BreakTo != null &&
          (obj as BreakableComponent).BreakTo.GetType() == obj.GetType())
          (obj as BreakableComponent).Break((obj as BreakableComponent).BreakTo);
      }
      OnAfterData();
    }

    internal virtual bool IsEmpty()
    {
      return true;
    }
    
    private void AddBookmark(ReportComponentBase obj)
    {
      if (Report != null)
        Report.Engine.AddBookmark(obj.Bookmark);
    }
    
    internal void AddBookmarks()
    {
      AddBookmark(this);
      foreach (ReportComponentBase obj in Objects)
      {
        AddBookmark(obj);
      }
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new BandBaseMenu(Report.Designer);
    }

    /// <inheritdoc/>
    public override void InitializeComponent()
    {
      base.InitializeComponent();
      AbsRowNo = 0;
    }

    /// <summary>
    /// This method fires the <b>BeforeLayout</b> event and the script code connected to the <b>BeforeLayoutEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnBeforeLayout(EventArgs e)
    {
      if (BeforeLayout != null)
        BeforeLayout(this, e);
      InvokeEvent(BeforeLayoutEvent, e);
    }

    /// <summary>
    /// This method fires the <b>AfterLayout</b> event and the script code connected to the <b>AfterLayoutEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public void OnAfterLayout(EventArgs e)
    {
      if (AfterLayout != null)
        AfterLayout(this, e);
      InvokeEvent(AfterLayoutEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="BandBase"/> class with default settings.
    /// </summary>
    public BandBase()
    {
      FObjects = new ReportComponentCollection(this);
      FGuides = new FloatCollection();
      FBeforeLayoutEvent = "";
      FAfterLayoutEvent = "";
      FOutlineExpression = "";
      CanBreak = false;
      ShiftMode = ShiftMode.Never;
      if (BaseName.EndsWith("Band"))
        BaseName = ClassName.Remove(ClassName.IndexOf("Band"));
      SetFlags(Flags.CanMove | Flags.CanChangeOrder | Flags.CanChangeParent | Flags.CanCopy | Flags.CanGroup, false);
      FlagUseStartNewPage = true;
      FlagCheckFreeSpace = true;
    }

  }

}