using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Design;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  /// <summary>
  /// Represents a table cell.
  /// </summary>
  /// <remarks>
  /// Use <see cref="ColSpan"/>, <see cref="RowSpan"/> properties to set the cell's 
  /// column and row spans. To put an object inside the cell, use its <see cref="Objects"/> property:
  /// <code>
  /// TableCell cell1;
  /// PictureObject picture1 = new PictureObject();
  /// picture1.Bounds = new RectangleF(0, 0, 32, 32);
  /// picture1.Name = "Picture1";
  /// cell1.Objects.Add(picture1);
  /// </code>
  /// </remarks>
  public class TableCell : TextObject, IParent
  {
    #region Fields
    private int FColSpan;
    private int FRowSpan;
    private ReportComponentCollection FObjects;
    private bool FUpdatingLayout;
    private TableCellData FCellData;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a collection of objects contained in this cell.
    /// </summary>
    [Browsable(false)]
    public ReportComponentCollection Objects
    {
      get 
      {
        if (CellData != null)
          return CellData.Objects;
        return FObjects;
      }
    }

    /// <summary>
    /// Gets or sets the column span for this cell.
    /// </summary>
    [DefaultValue(1)]
    [Category("Appearance")]
    public int ColSpan
    {
      get 
      {
        if (CellData != null)
          return CellData.ColSpan;
        return FColSpan;
      }
      set 
      { 
        if (CellData != null)
          CellData.ColSpan = value;
        FColSpan = value;
      }
    }

    /// <summary>
    /// Gets or sets the row span for this cell.
    /// </summary>
    [DefaultValue(1)]
    [Category("Appearance")]
    public int RowSpan
    {
      get 
      {
        if (CellData != null)
          return CellData.RowSpan;
        return FRowSpan;
      }
      set 
      { 
        if (CellData != null)
          CellData.RowSpan = value;
        FRowSpan = value;
      }
    }

    /// <inheritdoc/>
    public override string Text
    {
      get
      {
        if (CellData != null)
          return CellData.Text;
        return base.Text;
      }
      set
      {
        if (CellData != null)
          CellData.Text = value;
        base.Text = value;
      }
    }
    
    internal TableCellData CellData
    {
      get { return FCellData; }
      set { FCellData = value; }
    }
    
    /// <summary>
    /// Gets the address of this cell.
    /// </summary>
    [Browsable(false)]
    public Point Address
    {
      get { return CellData == null ? new Point() : CellData.Address; }
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
    public override DockStyle Dock
    {
      get { return base.Dock; }
      set { base.Dock = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanGrow
    {
      get { return base.CanGrow; }
      set { base.CanGrow = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanShrink
    {
      get { return base.CanShrink; }
      set { base.CanShrink = value; }
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
    public new bool AutoWidth
    {
      get { return base.AutoWidth; }
      set { base.AutoWidth = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Duplicates Duplicates
    {
      get { return base.Duplicates; }
      set { base.Duplicates = value; }
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
    [Browsable(false)]
    public override float Left
    {
      get { return base.Left; }
      set { base.Left = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Top
    {
      get { return base.Top; }
      set { base.Top = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Width
    {
      get 
      { 
        if (CellData != null)
          return CellData.Width;
        return base.Width; 
      }
      set 
      {
        // disable update layout by the cell. The layout is controlled by rows and columns.
        FUpdatingLayout = true;
        base.Width = value;
        FUpdatingLayout = false;
      }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Height
    {
      get 
      { 
        if (CellData != null)
          return CellData.Height;
        return base.Height; 
      }
      set 
      {
        FUpdatingLayout = true;
        base.Height = value;
        FUpdatingLayout = false;
      }
    }

    /// <inheritdoc/>
    public override float AbsLeft
    {
      get { return (Table != null) ? Table.AbsLeft + Left : base.AbsLeft; }
    }

    /// <inheritdoc/>
    public override float AbsTop
    {
      get { return (Table != null) ? Table.AbsTop + Top : base.AbsTop; }
    }

    /// <summary>
    /// Gets the <b>TableBase</b> object which this cell belongs to.
    /// </summary>
    [Browsable(false)]
    public TableBase Table
    {
      get { return Parent == null ? null : Parent.Parent as TableBase; }
    }
    #endregion
    
    #region Public Methods
    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      if (PointInObject(new PointF(e.X, e.Y)) && !Table.IsInsideSpan(this))
        e.Handled = true;
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleDragOver(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleDragDrop(FRMouseEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleDoubleClick()
    {
      Table.HandleCellDoubleClick(this);
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return Table.GetCellContextMenu(this);
    }

    /// <inheritdoc/>
    public override SmartTagBase GetSmartTag()
    {
      return Table.GetCellSmartTag(this);
    }

    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      TableCell src = source as TableCell;
      ColSpan = src.ColSpan;
      RowSpan = src.RowSpan;
    }

    /// <summary>
    /// Creates the exact copy of this cell.
    /// </summary>
    /// <returns>The copy of this cell.</returns>
    public TableCell Clone()
    {
      TableCell cell = new TableCell();
      cell.AssignAll(this);
      return cell;
    }

    /// <summary>
    /// Determines if two cells have identical settings.
    /// </summary>
    /// <param name="cell">Cell to compare with.</param>
    /// <returns><b>true</b> if cells are equal.</returns>
    public bool Equals(TableCell cell)
    {
      // do not override exising Equals method. It is used to compare elements in a list, 
      // and will cause problems in the designer.
      return cell != null &&
        Fill.Equals(cell.Fill) &&
        TextFill.Equals(cell.TextFill) &&
        HorzAlign == cell.HorzAlign &&
        VertAlign == cell.VertAlign &&
        Border.Equals(cell.Border) &&
        Font.Equals(cell.Font) &&
        Formats.Equals(cell.Formats) &&
        Highlight.Equals(cell.Highlight) &&
        Restrictions == cell.Restrictions &&
        Cursor == cell.Cursor &&
        Hyperlink.Equals(cell.Hyperlink) &&
        Padding == cell.Padding &&
        AllowExpressions == cell.AllowExpressions &&
        Brackets == cell.Brackets &&
        HideZeros == cell.HideZeros &&
        HideValue == cell.HideValue &&
        Angle == cell.Angle &&
        RightToLeft == cell.RightToLeft &&
        WordWrap == cell.WordWrap &&
        Underlines == cell.Underlines &&
        Trimming == cell.Trimming &&
        FontWidthRatio == cell.FontWidthRatio &&
        FirstTabOffset == cell.FirstTabOffset &&
        ParagraphOffset == cell.ParagraphOffset &&
        TabWidth == cell.TabWidth &&
        Clip == cell.Clip &&
        Wysiwyg == cell.Wysiwyg &&
        LineHeight == cell.LineHeight &&
        Style == cell.Style &&
        EvenStyle == cell.EvenStyle &&
        HoverStyle == cell.HoverStyle &&
        HtmlTags == cell.HtmlTags &&
        NullValue == cell.NullValue &&
        ProcessAt == cell.ProcessAt &&
        Printable == cell.Printable &&
        Exportable == cell.Exportable &&
        // events
        BeforePrintEvent == cell.BeforePrintEvent &&
        AfterPrintEvent == cell.AfterPrintEvent &&
        AfterDataEvent == cell.AfterDataEvent &&
        ClickEvent == cell.ClickEvent &&
        MouseDownEvent == cell.MouseDownEvent &&
        MouseMoveEvent == cell.MouseMoveEvent &&
        MouseUpEvent == cell.MouseUpEvent &&
        MouseEnterEvent == cell.MouseEnterEvent &&
        MouseLeaveEvent == cell.MouseLeaveEvent;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      TableCell c = writer.DiffObject as TableCell;
      base.Serialize(writer);
      
      if (ColSpan != c.ColSpan)
        writer.WriteInt("ColSpan", ColSpan);
      if (RowSpan != c.RowSpan)  
        writer.WriteInt("RowSpan", RowSpan);
    }

    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      if (Parent != null)
        Parent.SelectionChanged();
    }

    /// <summary>
    /// Changes the cell's style.
    /// </summary>
    /// <param name="style">The new style.</param>
    /// <remarks>
    /// Each cell in a dynamic table object (or in a matrix) has associated style. 
    /// Several cells may share one style. If you try to change the cell's appearance directly 
    /// (like setting cell.TextColor), it may affect other cells in the table. 
    /// To change the single cell, use this method.
    /// </remarks>
    public void SetStyle(TableCell style)
    {
      FCellData.SetStyle(style);
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      if (Objects != null)
      {
        foreach (ReportComponentBase c in Objects)
        {
          expressions.AddRange(c.GetExpressions());
        }
      }

      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();
      OnBeforePrint(EventArgs.Empty);

      if (Objects != null)
      {
        foreach (ReportComponentBase c in Objects)
        {
          c.SaveState();
          c.OnBeforePrint(EventArgs.Empty);
        }
      }
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      OnAfterPrint(EventArgs.Empty);
      base.RestoreState();

      if (Objects != null)
      {
        foreach (ReportComponentBase c in Objects)
        {
          c.OnAfterPrint(EventArgs.Empty);
          c.RestoreState();
        }
      }
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();
      if (Table != null && Table.IsInsideSpan(this))
        Text = "";
      if (Objects != null)
      {
        foreach (ReportComponentBase c in Objects)
        {
          c.GetData();
          c.OnAfterData();
        }
      }
      OnAfterData();
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      bool insideSpan = false;
      if (Table != null)
        insideSpan = Table.IsInsideSpan(this);
      
      return !insideSpan && child is ReportComponentBase && !(child is BandBase) && !(child is TableBase);
    }

    /// <inheritdoc/>
    public void GetChildObjects(ObjectCollection list)
    {
      if (Objects != null)
      {
        foreach (ReportComponentBase obj in Objects)
        {
          list.Add(obj);
        }
      }
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      if (child is ReportComponentBase)
      {
        if (Objects == null)
        {
          FObjects = new ReportComponentCollection(this);
          if (CellData != null)
            CellData.Objects = FObjects;
        }  

        Objects.Add(child as ReportComponentBase);
      }
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
      if (child is ReportComponentBase)
        Objects.Remove(child as ReportComponentBase);
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      if (child is ReportComponentBase)
        return Objects.IndexOf(child as ReportComponentBase);
      return 0;  
    }

    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      if (child is ReportComponentBase)
      {
        int oldOrder = child.ZOrder;
        if (oldOrder != -1 && order != -1 && oldOrder != order)
        {
          if (order > Objects.Count)
            order = Objects.Count;
          if (oldOrder <= order)
            order--;
          Objects.Remove(child as ReportComponentBase);
          Objects.Insert(order, child as ReportComponentBase);
          UpdateLayout(0, 0);
        }
      }
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
      if (FUpdatingLayout || Objects == null)
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

    /// <summary>
    /// Initializes a new instance of the <see cref="TableCell"/> class.
    /// </summary>
    public TableCell()
    {
      FColSpan = 1;
      FRowSpan = 1;
      Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
      SetFlags(Flags.CanDelete | Flags.CanCopy | Flags.CanMove | Flags.CanResize |
        Flags.CanChangeParent | Flags.CanDraw | Flags.CanWriteBounds, false);
      BaseName = "Cell";
    }
  }
}
