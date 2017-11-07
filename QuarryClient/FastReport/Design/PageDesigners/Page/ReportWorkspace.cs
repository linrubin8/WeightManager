using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using FastReport.Data;
using FastReport.Utils;
using FastReport.TypeConverters;
using FastReport.Design.ToolWindows;
using FastReport.Format;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.PageDesigners.Page
{
  internal class ReportWorkspace : UserControl
  {
    #region Fields
    private ReportPageDesigner FPageDesigner;
    private Designer FDesigner;
    private WorkspaceMode1 FMode1;
    private WorkspaceMode2 FMode2;
    private bool FMouseDown;
    private bool FMouseMoved;
    private PointF FLastMousePoint;
    private RectangleF FSelectionRect;
    private FRMouseEventArgs FEventArgs;
    private Guides FGuides;
    private System.Windows.Forms.ToolTip FToolTip;
    private SmartTagButton FSmartTag;
    private InsertFrom FInsertionSource;
    private static Cursor FFormatToolCursor;
    private SelectedObjectCollection FSelectedObjectsAtMouseDown;
    private bool FAllowPaint = true;
    private const int WM_PAINT = 0x000F;
    private bool FZoomLock = false;
    #endregion

    #region Properties
    public static Grid Grid = new Grid();
    public static bool ShowGrid;
    public static bool SnapToGrid;
    public static bool ShowGuides;
    public static new float Scale;
    public static MarkerStyle MarkerStyle;
    public static bool AutoGuides;
    public static bool ClassicView;
    public static bool EditAfterInsert;
    public event EventHandler BeforeZoom;
    public event EventHandler AfterZoom;

    public Designer Designer
    {
      get { return FDesigner; }
    }

    public ReportPage Page
    {
      get { return FPageDesigner.Page as ReportPage; }
    }

    internal Guides Guides
    {
      get { return FGuides; }
    }

    public bool Locked
    {
      get { return FPageDesigner.Locked; }
    }

    public Report Report
    {
      get { return Page.Report; }
    }

    private GraphicCache GraphicCache
    {
      get { return Report.GraphicCache; }
    }
    #endregion

    #region Private Methods
    private void DrawSelectionRect(Graphics g)
    {
      RectangleF rect = NormalizeSelectionRect();
      Brush b = GraphicCache.GetBrush(Color.FromArgb(80, SystemColors.Highlight));
      g.FillRectangle(b, rect.Left * Scale, rect.Top * Scale, rect.Width * Scale, rect.Height * Scale);
      Pen pen = GraphicCache.GetPen(SystemColors.Highlight, 1, DashStyle.Dash);
      g.DrawRectangle(pen, rect.Left * Scale, rect.Top * Scale, rect.Width * Scale, rect.Height * Scale);
    }

    private RectangleF NormalizeSelectionRect()
    {
      RectangleF result = FSelectionRect;
      if (FSelectionRect.Left > FSelectionRect.Right)
      {
        result.X = FSelectionRect.Right;
        result.Width = -FSelectionRect.Width;
      }
      if (FSelectionRect.Top > FSelectionRect.Bottom)
      {
        result.Y = FSelectionRect.Bottom;
        result.Height = -FSelectionRect.Height;
      }
      return result;
    }

    private RectangleF GetSelectedRect()
    {
      RectangleF result = new RectangleF(100000, 100000, -100000, -100000);
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase)
        {
          ComponentBase c = obj as ComponentBase;
          if (c.Left < result.Left)
            result.X = c.Left;
          if (c.Right > result.Right)
            result.Width = c.Right - result.Left;
          float cTop = c is BandBase ? 0 : c.Top;
          if (cTop < result.Top)
            result.Y = cTop;
          float cBottom = c is BandBase ? c.Height : c.Bottom;
          if (cBottom > result.Bottom)
            result.Height = cBottom - result.Top;
        }
      }
      if (result.Top == 100000)
        result = new RectangleF();
      return result;
    }

    private void AddBand(BandBase band, BandCollection list)
    {
      if (band != null)
      {
        if (band.Child != null && band.Child.FillUnusedSpace)
        {
          AddBand(band.Child, list);
          list.Add(band);
        }
        else
        {
          list.Add(band);
          AddBand(band.Child, list);
        }
      }
    }

    private void EnumDataBand(DataBand band, BandCollection list)
    {
      if (band == null)
        return;

      AddBand(band.Header, list);
      AddBand(band, list);
      foreach (BandBase b in band.Bands)
      {
        EnumBand(b, list);
      }
      AddBand(band.Footer, list);
    }

    private void EnumGroupHeaderBand(GroupHeaderBand band, BandCollection list)
    {
      if (band == null)
        return;

      AddBand(band.Header, list);
      AddBand(band, list);
      EnumGroupHeaderBand(band.NestedGroup, list);
      EnumDataBand(band.Data, list);
      AddBand(band.GroupFooter, list);
      AddBand(band.Footer, list);
    }

    private void EnumBand(BandBase band, BandCollection list)
    {
      if (band is DataBand)
        EnumDataBand(band as DataBand, list);
      else if (band is GroupHeaderBand)
        EnumGroupHeaderBand(band as GroupHeaderBand, list);
    }

    private void EnumBands(BandCollection list)
    {
      if (Page.TitleBeforeHeader)
      {
        AddBand(Page.ReportTitle, list);
        AddBand(Page.PageHeader, list);
      }
      else
      {
        AddBand(Page.PageHeader, list);
        AddBand(Page.ReportTitle, list);
      }
      AddBand(Page.ColumnHeader, list);
      foreach (BandBase b in Page.Bands)
      {
        EnumBand(b, list);
      }
      AddBand(Page.ColumnFooter, list);
      AddBand(Page.ReportSummary, list);
      AddBand(Page.PageFooter, list);
      AddBand(Page.Overlay, list);
    }

    private void AdjustBands()
    {
      BandCollection bands = new BandCollection();
      EnumBands(bands);
      float curY = ClassicView ? BandBase.HeaderSize : 0;
      float pageWidth = (Page.PaperWidth - Page.LeftMargin - Page.RightMargin) * Units.Millimeters;
      float columnWidth = Page.Columns.Width * Units.Millimeters;

      // lineup bands
      foreach (BandBase b in bands)
      {
        b.Left = 0;
        if (Page.Columns.Count > 1 && b.IsColumnDependentBand)
          b.Width = columnWidth;
        else
          b.Width = pageWidth;
        b.Top = curY;
        curY += b.Height + (ClassicView ? BandBase.HeaderSize : 4 / Scale);
      }
      // update size
      // since we are changing the size inside the OnPaint, avoid weird effects
      int width = (int)Math.Round(pageWidth * Scale) + 1;
      if (Page.ExtraDesignWidth)
        width *= 5;
      int height = (int)Math.Round(curY * Scale);
      if (ClassicView)
        height -= BandBase.HeaderSize - 4;

      if (!FMouseDown || height > Height || Top == 0)
        Size = new Size(width, height);
      else
        Width = width;

      // apply Right to Left layout if needed
      if (Config.RightToLeft)
      {
          Left = FPageDesigner.RulerPanel.VertRuler.Left - Width;
      }
    }

    private bool CheckGridStep()
    {
      bool al = SnapToGrid;
      if ((ModifierKeys & Keys.Alt) > 0)
        al = !al;

      bool result = true;
      float kx = FEventArgs.Delta.X;
      float ky = FEventArgs.Delta.Y;
      if (al)
      {
        result = kx >= Grid.SnapSize || kx <= -Grid.SnapSize || ky >= Grid.SnapSize || ky <= -Grid.SnapSize;
        if (result)
        {
          kx = (int)(kx / Grid.SnapSize) * Grid.SnapSize;
          ky = (int)(ky / Grid.SnapSize) * Grid.SnapSize;
        }
      }
      else
      {
        result = kx != 0 || ky != 0;
      }
      if (ShowGuides && !AutoGuides)
        FGuides.CheckGuides(ref kx, ref ky);
      FEventArgs.Delta.X = kx;
      FEventArgs.Delta.Y = ky;
      return result;
    }

    private void CloneSelectedObjects()
    {
      ObjectCollection list = new ObjectCollection();
      foreach (Base c in Designer.Objects)
      {
        if (Designer.SelectedObjects.IndexOf(c) != -1)
          if (c.HasFlag(Flags.CanCopy))
            list.Add(c);
      }

      // prepare to create unique name
      FastNameCreator nameCreator = new FastNameCreator(Report.AllNamedObjects);
      Designer.SelectedObjects.Clear();

      foreach (Base c in list)
      {
        Base clone = Activator.CreateInstance(c.GetType()) as Base;
        clone.AssignAll(c);
        clone.Name = "";
        clone.Parent = c.Parent;

        nameCreator.CreateUniqueName(clone);
        foreach (Base c1 in clone.AllObjects)
        {
          nameCreator.CreateUniqueName(c1);
        }

        Designer.Objects.Add(clone);
        Designer.SelectedObjects.Add(clone);
      }
    }

    private void HandleDoubleClick()
    {
      if (Designer.SelectedObjects.Count == 1 && Designer.SelectedObjects[0] is ComponentBase)
      {
        ComponentBase c = Designer.SelectedObjects[0] as ComponentBase;
        c.HandleDoubleClick();
      }
    }

    private void MoveSelectedObjects(int xDir, int yDir, bool smooth)
    {
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is PageBase))
        {
          ComponentBase c = obj as ComponentBase;
          c.Left += smooth ? xDir : xDir * Grid.SnapSize;
          c.Top += smooth ? yDir : yDir * Grid.SnapSize;
          c.CheckParent(true);
        }
      }
      Refresh();
      FDesigner.SetModified(FPageDesigner, "Move");
    }

    private void ResizeSelectedObjects(int xDir, int yDir, bool smooth)
    {
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is PageBase))
        {
          ComponentBase c = obj as ComponentBase;
          c.Width += smooth ? xDir : xDir * Grid.SnapSize;
          c.Height += smooth ? yDir : yDir * Grid.SnapSize;
          c.CheckNegativeSize(FEventArgs);
        }
      }
      Refresh();
      FDesigner.SetModified(FPageDesigner, "Size");
    }

    private void SelectNextObject(bool forward)
    {
      int index = 0;
      if (Designer.SelectedObjects.Count != 0)
        index = Designer.Objects.IndexOf(Designer.SelectedObjects[0]);
      index += forward ? 1 : -1;
      if (index < 0)
        index = Designer.Objects.Count - 1;
      if (index > Designer.Objects.Count - 1)
        index = 0;
      Designer.SelectedObjects.Clear();
      Designer.SelectedObjects.Add(Designer.Objects[index]);
      FDesigner.SelectionChanged(null);
    }

    private void ShowToolTip(string text, int x, int y)
    {
      if (FToolTip == null)
        FToolTip = new System.Windows.Forms.ToolTip();
      if (FToolTip != null)
        FToolTip.Show(text, this, x, y);
    }

    private void HideToolTip()
    {
      if (FToolTip != null)
      {
        FToolTip.Hide(this);
        FToolTip.Dispose();
        FToolTip = null;
      }
    }

    private void UpdateStatusBar()
    {
      RectangleF selectedRect = GetSelectedRect();
      bool emptyBounds = Designer.SelectedObjects.IsPageSelected || Designer.SelectedObjects.IsReportSelected ||
        (selectedRect.Width == 0 && selectedRect.Height == 0);
      string location = emptyBounds ? "" :
        Converter.ToString(selectedRect.Left, typeof(UnitsConverter)) + "; " +
        Converter.ToString(selectedRect.Top, typeof(UnitsConverter));
      string size = emptyBounds ? "" :
        Converter.ToString(selectedRect.Width, typeof(UnitsConverter)) + "; " +
        Converter.ToString(selectedRect.Height, typeof(UnitsConverter));

      string text = "";
      if (Designer.SelectedObjects.Count == 1)
      {
        Base obj = Designer.SelectedObjects[0];
        if (obj is TextObject)
        {
          //if ((obj as TextObject).HtmlTags) return;
          text = Converter.ToXml((obj as TextObject).Text);

        }
        else if (obj is BandBase)
          text = (obj as BandBase).GetInfoText();
      }

      Designer.ShowStatus(location, size, text);
    }

    private void UpdateAutoGuides()
    {
      if (!AutoGuides)
        return;

      Page.Guides.Clear();
      foreach (Base c in Designer.Objects)
      {
        if (c is BandBase)
          (c as BandBase).Guides.Clear();
      }
      foreach (Base c in Designer.Objects)
      {
        if (c is ReportComponentBase && !(c is BandBase))
        {
          ReportComponentBase obj = c as ReportComponentBase;
          if (!Page.Guides.Contains(obj.AbsLeft))
            Page.Guides.Add(obj.AbsLeft);
          if (!Page.Guides.Contains(obj.AbsRight))
            Page.Guides.Add(obj.AbsRight);
          BandBase band = obj.Band;
          if (band != null)
          {
            if (!band.Guides.Contains(obj.Top))
              band.Guides.Add(obj.Top);
            if (!band.Guides.Contains(obj.Bottom))
              band.Guides.Add(obj.Bottom);
          }
        }
      }
      FPageDesigner.RulerPanel.HorzRuler.Refresh();
      FPageDesigner.RulerPanel.VertRuler.Refresh();
    }

    private void CreateTitlesForInsertedObjects(DictionaryWindow.DraggedItemCollection items)
    {
        if (items == null)
            return;

        bool changed = false;

        // try to create title for the inserted items
        for (int i = 0; i < items.Count; i++)
        {
            string text = items[i].Text;
            Column column = items[i].Obj as Column;
            ComponentBase insertedObject = Designer.SelectedObjects[i] as ComponentBase;

            if (insertedObject.Parent is DataBand)
            {
                DataBand dataBand = insertedObject.Parent as DataBand;

                // connect databand to data if not connected yet
                if (dataBand.DataSource == null && column != null)
                {
                    // find a parent datasource for a column. Use the "text" which
                    // contains full-qualified name of the column.
                    dataBand.DataSource = DataHelper.GetDataSource(Report.Dictionary, text);
                    changed = true;
                }

                // find a header where to insert the column title
                BandBase header = dataBand.Header;
                if (header == null)
                    header = (dataBand.Page as ReportPage).PageHeader;
                if (header == null)
                    header = (dataBand.Page as ReportPage).ColumnHeader;
                if (header != null)
                {
                    // check for empty space on a header
                    RectangleF newBounds = insertedObject.Bounds;
                    newBounds.Inflate(-1, -1);
                    newBounds.Y = 0;
                    bool hasEmptySpace = true;
                    foreach (ReportComponentBase obj in header.Objects)
                    {
                        if (obj.Bounds.IntersectsWith(newBounds))
                        {
                            hasEmptySpace = false;
                            break;
                        }
                    }

                    // create the title
                    if (hasEmptySpace)
                    {
                        TextObject newObject = new TextObject();
                        newObject.Bounds = insertedObject.Bounds;
                        newObject.Top = 0;
                        newObject.Parent = header;
                        newObject.CreateUniqueName();

                        if (column != null)
                            text = column.Alias;
                        newObject.Text = text;
                        Designer.Objects.Add(newObject);

                        // apply last formatting to the new object
                        Designer.LastFormatting.SetFormatting(newObject);
                        changed = true;
                    }
                }
            }
        }

        if (changed)
            Designer.SetModified(null, "Change");
    }
    #endregion

    #region Protected Methods
    protected override void Dispose(bool disposing)
    {
      CancelPaste();
      base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (Locked)
        return;
      Graphics g = e.Graphics;
      FRPaintEventArgs paintArgs = new FRPaintEventArgs(g, Scale, Scale, GraphicCache);

      // check if workspace is active (in the mdi mode).
      ObjectCollection objects = Designer.Objects;
      if (Designer.ActiveReport != Report)
      {
        objects = Page.AllObjects;
        objects.Add(Page);
      }

      // draw bands
      foreach (Base obj in objects)
      {
        obj.SetDesigning(true);
        if (obj is BandBase)
          (obj as BandBase).Draw(paintArgs);
      }
      // draw objects
      foreach (Base obj in objects)
      {
        if (obj is ComponentBase && !(obj is BandBase) && obj.HasFlag(Flags.CanDraw))
          (obj as ComponentBase).Draw(paintArgs);
      }
      // draw selection
      if (!FMouseDown && Designer.ActiveReport == Report)
      {
        foreach (Base obj in Designer.SelectedObjects)
        {
          if (obj is ComponentBase && obj.HasFlag(Flags.CanDraw))
            (obj as ComponentBase).DrawSelection(paintArgs);
        }
      }

      // draw page margins in "ExtraDesignWidth" mode
      if (Page.ExtraDesignWidth)
      {
        float pageWidth = (Page.PaperWidth - Page.LeftMargin - Page.RightMargin) * Units.Millimeters * Scale;
        Pen pen = GraphicCache.GetPen(Color.Red, 1, DashStyle.Dot);
        for (float x = pageWidth; x < Width; x += pageWidth)
        {
          g.DrawLine(pen, x, 0, x, Height);
        }
      }

      if (ShowGuides)
        FGuides.Draw(g);
      if (FMode2 == WorkspaceMode2.SelectionRect)
        DrawSelectionRect(g);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (Locked)
        return;

      Designer.SelectedObjects.CopyTo(FSelectedObjectsAtMouseDown);

      FMouseDown = true;
      FMouseMoved = false;
      FEventArgs.X = e.X / Scale;
      FEventArgs.Y = e.Y / Scale;
      FEventArgs.Button = e.Button;
      FEventArgs.ModifierKeys = ModifierKeys;
      FEventArgs.Handled = false;

      if (FMode2 == WorkspaceMode2.None)
      {
        // find an object under the mouse
        for (int i = Designer.Objects.Count - 1; i >= 0; i--)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
          {
            c.HandleMouseDown(FEventArgs);
            if (FEventArgs.Handled)
            {
              FMode2 = FEventArgs.Mode;
              break;
            }
          }
        }
      }
      else if (FEventArgs.ActiveObject != null)
      {
        FEventArgs.ActiveObject.HandleMouseDown(FEventArgs);
      }

      if (!FSelectedObjectsAtMouseDown.Equals(Designer.SelectedObjects))
      {
        Designer.SelectionChanged(FPageDesigner);
        Designer.SelectedObjects.CopyTo(FSelectedObjectsAtMouseDown);
      }

      FLastMousePoint.X = FEventArgs.X;
      FLastMousePoint.Y = FEventArgs.Y;
      FSelectionRect = new RectangleF(FEventArgs.X, FEventArgs.Y, 0, 0);
      FGuides.CreateVirtualGuides();
      FSmartTag.Hide();
      Refresh();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (Locked || Designer.ActiveReport != Report)
        return;

      FEventArgs.X = e.X / Scale;
      FEventArgs.Y = e.Y / Scale;
      FEventArgs.Button = FMode1 == WorkspaceMode1.Insert ? MouseButtons.Left : e.Button;
      if (FMode1 == WorkspaceMode1.Insert && FMode2 == WorkspaceMode2.Move)
        Cursor = Cursors.Default;
      FEventArgs.Delta = new PointF(FEventArgs.X - FLastMousePoint.X, FEventArgs.Y - FLastMousePoint.Y);

      if (e.Button == MouseButtons.None && FMode1 == WorkspaceMode1.Select)
      {
        Cursor = Cursors.Default;
        FMode2 = WorkspaceMode2.None;
        FEventArgs.Cursor = Cursor;
        FEventArgs.Mode = FMode2;
        FEventArgs.ActiveObject = null;
        FEventArgs.Handled = false;

        // check object's sizing points
        for (int i = Designer.Objects.Count - 1; i >= 0; i--)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
          {
            c.HandleMouseMove(FEventArgs);
            if (FEventArgs.Handled)
            {
              Cursor = FEventArgs.Cursor;
              FMode2 = FEventArgs.Mode;
              FEventArgs.ActiveObject = c;
              break;
            }
          }
        }

        // no sizing points found, check hover
        if (!FEventArgs.Handled)
        {
          for (int i = Designer.Objects.Count - 1; i >= 0; i--)
          {
            ComponentBase c = Designer.Objects[i] as ComponentBase;
            if (c != null)
            {
              c.HandleMouseHover(FEventArgs);
              if (FEventArgs.Handled)
              {
                Cursor = Designer.FormatPainter ? FFormatToolCursor : FEventArgs.Cursor;
                FEventArgs.ActiveObject = c;
                if (c.HasFlag(Flags.HasSmartTag) && !c.HasRestriction(Restrictions.DontEdit))
                  FSmartTag.Show(c);
                break;
              }
            }
          }
        }

        if (FEventArgs.ActiveObject == null || !FEventArgs.ActiveObject.HasFlag(Flags.HasSmartTag))
          FSmartTag.Hide();
      }
      else if (FMode2 == WorkspaceMode2.Move || FMode2 == WorkspaceMode2.Size)
      {
        // handle drag&drop from the data tree
        bool dragHandled = false;
        if (FMode1 == WorkspaceMode1.DragDrop)
        {
          FEventArgs.DragTarget = null;
          FEventArgs.Handled = false;
          FEventArgs.DragMessage = "";

          for (int i = Designer.Objects.Count - 1; i >= 0; i--)
          {
            ComponentBase c = Designer.Objects[i] as ComponentBase;

            if (c != null &&
                (FEventArgs.DragSources == null || Array.IndexOf(FEventArgs.DragSources, c) == -1) &&
                !c.HasRestriction(Restrictions.DontModify))
            {
              c.HandleDragOver(FEventArgs);
            }

            if (FEventArgs.Handled)
            {
              FEventArgs.DragTarget = c;
              dragHandled = true;
              FEventArgs.Handled = false;
              // handle remained objects to reset its state. To do this, invert mouse location
              FEventArgs.X = -FEventArgs.X;
              FEventArgs.Y = -FEventArgs.Y;
            }
          }

          if (dragHandled)
          {
            // revert back the mouse location
            FEventArgs.X = -FEventArgs.X;
            FEventArgs.Y = -FEventArgs.Y;
          }

          foreach (ComponentBase obj in FEventArgs.DragSources)
              obj.SetFlags(Flags.CanDraw, !dragHandled);
        }

        if (!dragHandled && !CheckGridStep())
          return;

        // if insert is on and user press the mouse button and move the mouse, resize selected objects
        if (FMode1 == WorkspaceMode1.Insert && e.Button == MouseButtons.Left && !FMouseMoved)
        {
          FMode2 = WorkspaceMode2.Size;
          foreach (Base c in Designer.SelectedObjects)
          {
            if (c is ComponentBase)
            {
              (c as ComponentBase).Width = 0;
              (c as ComponentBase).Height = 0;
            }
          }
          FEventArgs.SizingPoint = SizingPoint.RightBottom;
        }

        // ctrl was pressed, clone selected objects
        if (FMode1 == WorkspaceMode1.Select && ModifierKeys == Keys.Control && !FMouseMoved)
          CloneSelectedObjects();

        FMouseMoved = true;
        FEventArgs.Mode = FMode2;
        FEventArgs.Handled = false;
        // first serve the active object
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseMove(FEventArgs);

        // if active object does not reset the handled flag, serve other objects
        if (!FEventArgs.Handled)
        {
          foreach (Base c in Designer.Objects)
          {
            if (c is ComponentBase)
            {
              if (c != FEventArgs.ActiveObject)
                (c as ComponentBase).HandleMouseMove(FEventArgs);
            }
          }
        }

        FLastMousePoint.X += FEventArgs.Delta.X;
        FLastMousePoint.Y += FEventArgs.Delta.Y;

        if (FMode1 == WorkspaceMode1.DragDrop && !dragHandled)
        {
            // correct the location of the dragged object because we skip the GridCheck
            float offset = 0f;
            foreach (ComponentBase obj in FEventArgs.DragSources)
            {
                if (offset == 0f)
                {
                    obj.Left = (int)Math.Round(obj.Left / Grid.SnapSize) * Grid.SnapSize;
                    obj.Top = (int)Math.Round(obj.Top / Grid.SnapSize) * Grid.SnapSize;
                }
                else
                {
                    obj.Left = offset;
                    obj.Top = (int)Math.Round(obj.Top / Grid.SnapSize) * Grid.SnapSize;
                }
                offset = obj.Right + Grid.SnapSize;
            }
        }

        if (FMode1 != WorkspaceMode1.DragDrop || !dragHandled)
        {
          FGuides.CheckVirtualGuides();

          // show tooltip with object's location/size
          string s = "";
          RectangleF selectedRect = GetSelectedRect();
          if (FEventArgs.ActiveObject is BandBase)
            selectedRect = new RectangleF(0, 0, FEventArgs.ActiveObject.Width, FEventArgs.ActiveObject.Height);
          if (FMode2 == WorkspaceMode2.Move)
          {
            s = Converter.ToString(selectedRect.Left, typeof(UnitsConverter)) + "; " +
              Converter.ToString(selectedRect.Top, typeof(UnitsConverter));
          }
          else if (FMode2 == WorkspaceMode2.Size)
          {
            s = Converter.ToString(selectedRect.Width, typeof(UnitsConverter)) + "; " +
              Converter.ToString(selectedRect.Height, typeof(UnitsConverter));
          }
          ShowToolTip(s, e.X + 20, e.Y + 20);
        }
        else
        {
          FGuides.ClearVirtualGuides();
          if (String.IsNullOrEmpty(FEventArgs.DragMessage))
            HideToolTip();
          else
            ShowToolTip(FEventArgs.DragMessage, e.X + 20, e.Y + 20);
        }

        // if active object is band (we are resizing it), update the band structure and vertical ruler
        if (FEventArgs.ActiveObject is BandBase)
          UpdateBands();
        else
          Refresh();
      }
      else if (FMode2 == WorkspaceMode2.SelectionRect)
      {
        FSelectionRect = new RectangleF(FSelectionRect.Left, FSelectionRect.Top,
          FEventArgs.X - FSelectionRect.Left, FEventArgs.Y - FSelectionRect.Top);
        Refresh();
      }
      else if (FMode2 == WorkspaceMode2.Custom)
      {
        if (!CheckGridStep())
          return;

        FMouseMoved = true;
        FEventArgs.Mode = FMode2;
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseMove(FEventArgs);

        FLastMousePoint.X += FEventArgs.Delta.X;
        FLastMousePoint.Y += FEventArgs.Delta.Y;
        Refresh();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (Locked)
        return;

      HideToolTip();

      FEventArgs.X = e.X / Scale;
      FEventArgs.Y = e.Y / Scale;
      FEventArgs.Button = e.Button;
      FEventArgs.Mode = FMode2;
      FEventArgs.Handled = false;

      if (FMode2 == WorkspaceMode2.Move || FMode2 == WorkspaceMode2.Size)
      {
        // handle drag&drop from the data tree
        bool dragHandled = false;
        if (FMode1 == WorkspaceMode1.DragDrop)
        {
          if (FEventArgs.DragTarget != null)
          {
            dragHandled = true;
            FEventArgs.DragTarget.HandleDragDrop(FEventArgs);

            if (FEventArgs.DragSources != null)
            {
              foreach (ComponentBase dragSource in FEventArgs.DragSources)
              {
                Designer.Objects.Remove(dragSource);
                dragSource.Dispose();
              }
              FEventArgs.DragSources = null;
            }

            Designer.SelectedObjects.Clear();
            Designer.SelectedObjects.Add(FEventArgs.DragTarget);
          }
        }

        // serve all objects
        for (int i = 0; i < Designer.Objects.Count; i++)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
            c.HandleMouseUp(FEventArgs);
        }

        // remove objects with zero size
        if (FMode1 == WorkspaceMode1.Insert)
        {
          int i = 0;
          while (i < Designer.SelectedObjects.Count)
          {
            ComponentBase c = Designer.SelectedObjects[i] as ComponentBase;
            if (c != null && c.Width == 0 && c.Height == 0)
            {
              Designer.Objects.Remove(c);
              Designer.SelectedObjects.Remove(c);
              c.Dispose();
              i--;
            }
            i++;
          }
        }

        if (FMode1 != WorkspaceMode1.Select && !dragHandled)
        {
          // during OnInsert call current context may be changed
          WorkspaceMode1 saveMode = FMode1;
          FMode1 = WorkspaceMode1.Select;
          ObjectCollection insertedObjects = new ObjectCollection();
          Designer.SelectedObjects.CopyTo(insertedObjects);
          foreach (Base c in insertedObjects)
          {
            c.OnAfterInsert(FInsertionSource);
          }
          FMode1 = saveMode;
        }

        // check if we actually move a mouse after we clicked it
        string action = FMode1 != WorkspaceMode1.Select ? "Insert" : FMode2 == WorkspaceMode2.Move ? "Move" : "Size";
        if (dragHandled)
          action = "Change";
        if (FMouseMoved || FMode1 != WorkspaceMode1.Select)
          Designer.SetModified(FPageDesigner, action);
      }
      else if (FMode2 == WorkspaceMode2.SelectionRect)
      {
        FEventArgs.SelectionRect = NormalizeSelectionRect();
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseUp(FEventArgs);
      }
      else if (FMode2 == WorkspaceMode2.Custom)
      {
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseUp(FEventArgs);
      }

      bool needReset = FMode1 != WorkspaceMode1.Select;
      if (!FSelectedObjectsAtMouseDown.Equals(Designer.SelectedObjects) || needReset)
        Designer.SelectionChanged(FPageDesigner);

      FMouseDown = false;
      FMode1 = WorkspaceMode1.Select;
      FMode2 = WorkspaceMode2.None;
      FGuides.ClearVirtualGuides();
      UpdateBands();
      if (needReset)
        Designer.ResetObjectsToolbar(false);

      if (e.Button == MouseButtons.Right && Designer.SelectedObjects.Count > 0)
      {
        ContextMenuBar menu = Designer.SelectedObjects[0].GetContextMenu();

        if (menu != null)
        {
          PopupItem item = menu.Items[0] as PopupItem;
          item.PopupMenu(PointToScreen(e.Location));
        }
      }
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);

      if (e.Button == MouseButtons.Left)
        HandleDoubleClick();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (Locked)
        return;

      if ((ModifierKeys & Keys.Control) != 0)
      {
        if (e.Delta > 0)
          ZoomIn();
        else if (e.Delta < 0)
          ZoomOut();
      }
      else
      {
        FEventArgs.WheelDelta = e.Delta;
        FEventArgs.Handled = false;

        // serve all objects
        foreach (Base c in Designer.Objects)
        {
          if (c is ComponentBase)
          {
            (c as ComponentBase).HandleMouseWheel(FEventArgs);
            if (FEventArgs.Handled)
            {
              Refresh();
              return;
            }
          }
        }

        (Parent as PanelX).DoMouseWheel(e);
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      return (keyData & Keys.Up) != 0 || (keyData & Keys.Down) != 0 ||
        (keyData & Keys.Left) != 0 || (keyData & Keys.Right) != 0 ||
        (keyData & Keys.Tab) != 0;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);

      // serve all objects
      foreach (Base c in Designer.Objects)
      {
        if (c is ComponentBase)
        {
          (c as ComponentBase).HandleKeyDown(this, e);
          if (e.Handled)
            return;
        }
      }

      int xDir = 0;
      int yDir = 0;

      switch (e.KeyCode)
      {
        case Keys.Escape:
          Designer.ResetObjectsToolbar(true);
          CancelPaste();
          Designer.SelectedObjects.Clear();
          Designer.SelectedObjects.Add(Page);
          Designer.SelectionChanged(null);
          break;

        case Keys.Enter:
          HandleDoubleClick();
          break;

        case Keys.Insert:
          if ((e.Modifiers & Keys.Control) != 0)
            Designer.cmdCopy.Invoke();
          else if ((e.Modifiers & Keys.Shift) != 0)
            Designer.cmdPaste.Invoke();
          break;

        case Keys.Delete:
          CancelPaste();
          if ((e.Modifiers & Keys.Shift) == 0)
            Designer.cmdDelete.Invoke();
          else
            Designer.cmdCut.Invoke();
          break;

        case Keys.Up:
          yDir = -1;
          break;

        case Keys.Down:
          yDir = 1;
          break;

        case Keys.Left:
          xDir = -1;
          break;

        case Keys.Right:
          xDir = 1;
          break;

        case Keys.Tab:
          SelectNextObject((e.Modifiers & Keys.Shift) == 0);
          break;

        case Keys.Add:
          ZoomIn();
          break;

        case Keys.Subtract:
          ZoomOut();
          break;
      }

      if ((e.Modifiers & Keys.Control) != 0)
      {
        switch (e.KeyCode)
        {
          case Keys.B:
            if (Designer.SelectedTextObjects.Count > 0)
              Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Bold,
                !Designer.SelectedTextObjects.First.Font.Bold);
            break;

          case Keys.I:
            if (Designer.SelectedTextObjects.Count > 0)
              Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Italic,
                !Designer.SelectedTextObjects.First.Font.Italic);
            break;

          case Keys.U:
            if (Designer.SelectedTextObjects.Count > 0)
              Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Underline,
                !Designer.SelectedTextObjects.First.Font.Underline);
            break;

          case Keys.C:
            Designer.cmdCopy.Invoke();
            break;

          case Keys.X:
            Designer.cmdCut.Invoke();
            break;

          case Keys.V:
            Designer.cmdPaste.Invoke();
            break;

          case Keys.A:
            Designer.cmdSelectAll.Invoke();
            break;
        }
      }

      if (xDir != 0 || yDir != 0)
      {
        bool smooth = (e.Modifiers & Keys.Control) != 0;
        if ((e.Modifiers & Keys.Shift) != 0)
          ResizeSelectedObjects(xDir, yDir, smooth);
        else
          MoveSelectedObjects(xDir, yDir, smooth);
      }
    }

    protected override void OnDragOver(DragEventArgs drgevent)
    {
        base.OnDragOver(drgevent);

        if (!Designer.cmdInsert.Enabled)
        {
            drgevent.Effect = DragDropEffects.None;
            return;
        }

        if (FMode1 != WorkspaceMode1.DragDrop)
        {
            DictionaryWindow.DraggedItemCollection draggedItems = DictionaryWindow.DragUtils.GetAll(drgevent);

                if (draggedItems == null || draggedItems.Count == 0)
            {
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            ObjectInfo[] infos = new ObjectInfo[draggedItems.Count];

            for (int i = 0; i < draggedItems.Count; i++)
            {
                Type objType = typeof(TextObject);

                if (draggedItems[i].Obj is Column)
                {
                    Column c = draggedItems[i].Obj as Column;
                    objType = c.GetBindableControlType();
                }

                infos[i] = RegisteredObjects.FindObject(objType);
            }

            Designer.InsertObject(infos, InsertFrom.Dictionary);

            List<ComponentBase> dragSources = new List<ComponentBase>();

            if (Designer.SelectedObjects.Count == draggedItems.Count)
            {
                for (int i = 0; i < Designer.SelectedObjects.Count; i++)
                {
                    ComponentBase obj = Designer.SelectedObjects[i] as ComponentBase;
                    DictionaryWindow.DraggedItem item = draggedItems[i];

                    if (obj == null || item == null)
                        continue;

                    if (obj is TextObject)
                    {
                        TextObject textObj = obj as TextObject;
                        textObj.Text = textObj.GetTextWithBrackets(item.Text);
                        if (item.Obj is Column)
                        {
                            Column c = item.Obj as Column;
                            if (c.DataType == typeof(float) || c.DataType == typeof(double) || c.DataType == typeof(decimal))
                            {
                                textObj.HorzAlign = HorzAlign.Right;
                                textObj.WordWrap = false;
                                textObj.Trimming = StringTrimming.EllipsisCharacter;
                            }
                            textObj.Format = c.GetFormat();
                        }
                    }
                    else if (obj is PictureObject)
                    {
                        (obj as PictureObject).DataColumn = item.Text;
                    }
                    else if (obj is CheckBoxObject)
                    {
                        (obj as CheckBoxObject).DataColumn = item.Text;
                    }

                    dragSources.Add(obj);
                }
            }

            FEventArgs.DragSources = dragSources.ToArray();
        }

        FMode1 = WorkspaceMode1.DragDrop;
        Point pt = PointToClient(new Point(drgevent.X, drgevent.Y));
        OnMouseMove(new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0));
        drgevent.Effect = drgevent.AllowedEffect;
    }

    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);

      DictionaryWindow.DraggedItemCollection items = DictionaryWindow.DragUtils.GetAll(drgevent);

      Point pt = PointToClient(new Point(drgevent.X, drgevent.Y));
      OnMouseUp(new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0));

      if (FEventArgs.DragSources != null && FEventArgs.DragSources.Length > 0)
        CreateTitlesForInsertedObjects(items);
    }

    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);
      CancelPaste();
    }

    protected override void WndProc(ref Message m)
    {
        if ((m.Msg != WM_PAINT) || (FAllowPaint && m.Msg == WM_PAINT))
        {
            base.WndProc(ref m);
        }
    }
    #endregion

    #region Public Methods
    public void Paste(ObjectCollection list, InsertFrom source)
    {
      FInsertionSource = source;
      // find left-top edge of pasted objects
      float minLeft = 100000;
      float minTop = 100000;
      foreach (Base c in list)
      {
        if (c is ComponentBase)
        {
          ComponentBase c1 = c as ComponentBase;
          if (c1.Left < minLeft)
            minLeft = c1.Left;
          if (c1.Top < minTop)
            minTop = c1.Top;
        }
      }
      foreach (Base c in list)
      {
        // correct the left-top
        if (c is ComponentBase)
        {
          ComponentBase c1 = c as ComponentBase;
          c1.Left -= minLeft + Grid.SnapSize * 1000;
          c1.Top -= minTop + Grid.SnapSize * 1000;
          if (c1.Width == 0 && c1.Height == 0)
          {
            SizeF preferredSize = c1.GetPreferredSize();
            c1.Width = preferredSize.Width;
            c1.Height = preferredSize.Height;
            if (SnapToGrid)
            {
              c1.Width = (int)Math.Round(c1.Width / Grid.SnapSize) * Grid.SnapSize;
              c1.Height = (int)Math.Round(c1.Height / Grid.SnapSize) * Grid.SnapSize;
            }
          }
        }
      }
      FMode1 = WorkspaceMode1.Insert;
      FMode2 = WorkspaceMode2.Move;
      FEventArgs.ActiveObject = null;
      int addSize = 0;
      if (ClassicView)
        addSize = BandBase.HeaderSize;
      OnMouseDown(new MouseEventArgs(MouseButtons.Left, 0,
        10 - (int)(Grid.SnapSize * 1000 * Scale), 10 + addSize - (int)(Grid.SnapSize * 1000 * Scale), 0));
    }

    public void CancelPaste()
    {
      if (FMode1 != WorkspaceMode1.Select)
      {
        while (Designer.SelectedObjects.Count > 0)
        {
          Base c = Designer.SelectedObjects[0];
          Designer.SelectedObjects.Remove(c);
          Designer.Objects.Remove(c);
          c.Dispose();
        }
        FMode1 = WorkspaceMode1.Select;
        FMode2 = WorkspaceMode2.None;
        OnMouseUp(new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
      }
    }

    public void UpdateBands()
    {
      Refresh();
      FPageDesigner.RulerPanel.VertRuler.Refresh();
      FPageDesigner.RulerPanel.Structure.Refresh();
    }

    public override void Refresh()
    {
      AdjustBands();
      UpdateStatusBar();
      UpdateAutoGuides();
      base.Refresh();
    }

    public void DeleteHGuides()
    {
      foreach (Base c in Designer.Objects)
      {
        if (c is BandBase)
          (c as BandBase).Guides.Clear();
      }
      Refresh();
      FPageDesigner.RulerPanel.VertRuler.Refresh();
      FDesigner.SetModified(FPageDesigner, "DeleteHGuides");
    }

    public void DeleteVGuides()
    {
      Page.Guides.Clear();
      Refresh();
      FPageDesigner.RulerPanel.HorzRuler.Refresh();
      FDesigner.SetModified(FPageDesigner, "DeleteVGuides");
    }

    public void Zoom(float zoom)
    {
        if (FZoomLock)
            return;

        FAllowPaint = false;
        FZoomLock = true;

        try
        {
            EventHandler beforeZoom = BeforeZoom;
            if (beforeZoom != null)
                beforeZoom.Invoke(this, EventArgs.Empty);

            Scale = zoom;
            Designer.UpdatePlugins(null);

            EventHandler afterZoom = AfterZoom;
            if (afterZoom != null)
                afterZoom.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            FZoomLock = false;
            FAllowPaint = true;
            Refresh();
        }
    }

    public void ZoomIn()
    {
      float zoom = Scale;
      zoom += 0.25f;
      if (zoom > 8f)
        zoom = 8f;
      Zoom(zoom);
    }

    public void ZoomOut()
    {
      float zoom = Scale;
      zoom -= 0.25f;
      if (zoom < 0.25f)
        zoom = 0.25f;
      Zoom(zoom);
    }

    public void FitPageWidth()
    {
      float actualWidth = Width / Scale;
      Zoom((Parent.Width - 10) / actualWidth);
    }

    public void FitWholePage()
    {
      float actualWidth = Width / Scale;
      float actualHeight = Height / Scale;
      float scaleX = (Parent.Width - 10) / actualWidth;
      float scaleY = (Parent.Height - 10) / actualHeight;
      Zoom(scaleX < scaleY ? scaleX : scaleY);
    }

    public void SelectAll()
    {
      Base parent = null;

      if (Designer.SelectedObjects.Count == 0)
        return;

      if (Designer.SelectedObjects[0] is Report || Designer.SelectedObjects[0] is PageBase ||
        Designer.SelectedObjects[0].Report == null)
        parent = Page;
      else if (Designer.SelectedObjects[0] is BandBase)
        parent = Designer.SelectedObjects[0];
      else
      {
        parent = Designer.SelectedObjects[0];
        while (parent != null && !(parent is BandBase))
        {
          parent = parent.Parent;
        }
      }

      Designer.SelectedObjects.Clear();
      if (parent is PageBase)
      {
        // if page is selected, select all bands on the page
        foreach (Base c in parent.AllObjects)
        {
          if (c is BandBase)
            Designer.SelectedObjects.Add(c);
        }
      }
      else if (parent is BandBase)
      {
        // if band is selected, select all objects on the band. Do not select sub-bands.
        foreach (Base c in parent.ChildObjects)
        {
          if (!(c is BandBase))
            Designer.SelectedObjects.Add(c);
        }
      }
      if (Designer.SelectedObjects.Count == 0)
        Designer.SelectedObjects.Add(parent);
      Designer.SelectionChanged(null);
    }

    public Base GetParentForPastedObjects()
    {
      BandCollection bands = new BandCollection();
      EnumBands(bands);
      return bands[0];
    }
    #endregion

    internal ReportWorkspace(ReportPageDesigner pageDesigner)
    {
      FPageDesigner = pageDesigner;
      FDesigner = pageDesigner.Designer;

      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      AllowDrop = true;
      FEventArgs = new FRMouseEventArgs();
      FGuides = new Guides(FPageDesigner);
      FSelectedObjectsAtMouseDown = new SelectedObjectCollection();

      FSmartTag = new SmartTagButton(this);
      Controls.Add(FSmartTag);

      Size = new Size(1, 1);
    }

    static ReportWorkspace()
    {
      FFormatToolCursor = ResourceLoader.GetCursor("Format.cur");
      XmlItem xi = Config.Root.FindItem("Designer").FindItem("Report");
      string units = xi.GetProp("Units");
      switch (units)
      {
        case "Millimeters":
          Grid.GridUnits = PageUnits.Millimeters;
          break;
        case "Centimeters":
          Grid.GridUnits = PageUnits.Centimeters;
          break;
        case "Inches":
          Grid.GridUnits = PageUnits.Inches;
          break;
        case "HundrethsOfInch":
          Grid.GridUnits = PageUnits.HundrethsOfInch;
          break;
      }
      string size = xi.GetProp("SnapSizeMillimeters");
      if (size != "")
        Grid.SnapSizeMillimeters = Converter.StringToFloat(size);
      size = xi.GetProp("SnapSizeCentimeters");
      if (size != "")
        Grid.SnapSizeCentimeters = Converter.StringToFloat(size);
      size = xi.GetProp("SnapSizeInches");
      if (size != "")
        Grid.SnapSizeInches = Converter.StringToFloat(size);
      size = xi.GetProp("SnapSizeHundrethsOfInch");
      if (size != "")
        Grid.SnapSizeHundrethsOfInch = Converter.StringToFloat(size);
      Grid.Dotted = xi.GetProp("DottedGrid") != "0";
      ShowGrid = xi.GetProp("ShowGrid") != "0";
      SnapToGrid = xi.GetProp("SnapToGrid") != "0";
      MarkerStyle = xi.GetProp("MarkerStyle") == "Rectangle" ?
        MarkerStyle.Rectangle : MarkerStyle.Corners;
      string s = xi.GetProp("Scale");
      if (s != "")
        Scale = Converter.StringToFloat(s);
      else
        Scale = 1;
      AutoGuides = xi.GetProp("AutoGuides") == "1";
      ClassicView = xi.GetProp("ClassicView") == "1";
      EditAfterInsert = xi.GetProp("EditAfterInsert") == "1";
      ShowGuides = true;
    }

    private class SmartTagButton : Button
    {
      private ReportWorkspace FWorkspace;
      private ComponentBase FComponent;

      protected override void OnClick(EventArgs e)
      {
        base.OnClick(e);
        Point pt = PointToScreen(new Point(10, 0));
        SmartTagBase smartTag = FComponent.GetSmartTag();
        if (smartTag != null)
          smartTag.Show(pt);
      }

      protected override bool IsInputKey(Keys keyData)
      {
        return (keyData & Keys.Up) != 0 || (keyData & Keys.Down) != 0 ||
          (keyData & Keys.Left) != 0 || (keyData & Keys.Right) != 0 ||
          (keyData & Keys.Tab) != 0;
      }

      protected override void OnKeyDown(KeyEventArgs kevent)
      {
        FWorkspace.OnKeyDown(kevent);
      }

      public void Show(ComponentBase c)
      {
        Point loc = new Point((int)Math.Round(c.AbsRight * ReportWorkspace.Scale) - 14,
          (int)Math.Round(c.AbsTop * ReportWorkspace.Scale) - 5);
        if (loc.Y < 0)
          loc.Y = 0;
        Location = loc;
        Visible = true;
        FComponent = c;
      }

      public SmartTagButton(ReportWorkspace workspace)
      {
        FWorkspace = workspace;
        Size = new Size(10, 10);
        Visible = false;
        Cursor = Cursors.Hand;
        BackColor = Color.White;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Image = Res.GetImage(77);
      }
    }
  }
}
