using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using FastReport.DevComponents.DotNetBar;
using FastReport.Utils;

namespace FastReport.Preview
{
  internal enum PreviewZoomMode
  {
    Normal,
    PageWidth,
    WholePage
  }

  internal class PreviewWorkspace : PanelX
  {
    #region Fields
    private PreviewTab FPreviewTab;
    private PreviewPages FPreviewPages;
    private float FZoom;
    private PreviewZoomMode FZoomMode;
    private int FPageNo;
    private Point FLastMousePoint;
    private Cursor curHand;
    private Cursor curHandMove;
    private ReportComponentBase FActiveObject;
    private SearchInfo FSearchInfo;
    private bool FLocked;
    private bool FLayoutLocked;
    private bool FAllowPaint = true;
    private const int WM_PAINT = 0x000F;
    #endregion

    #region Properties
    private PreviewTab PreviewTab
    {
      get { return FPreviewTab; }
    }

    private PreviewControl Preview
    {
      get { return PreviewTab.Preview; }
    }

    private Report Report
    {
      get { return PreviewTab.Report; }
    }

    private PreparedPages PreparedPages
    {
      get { return PreviewTab.PreparedPages; }
    }

    private PreviewPages PreviewPages
    {
      get { return FPreviewPages; }
    }

    public int PageNo
    {
      get { return FPageNo; }
      set { SetPageNo(value, true); }
    }

    public int PageCount
    {
      get { return PreparedPages.Count; }
    }

    public float Zoom
    {
      get { return FZoom; }
      set { DoZoom(value); }
    }

    public SearchInfo SearchInfo
    {
      get { return FSearchInfo; }
      set { FSearchInfo = value; }
    }

    private Point Offset
    {
      get { return new Point(-AutoScrollPosition.X, -AutoScrollPosition.Y); }
      set
      {
        AutoScrollPosition = new Point(-value.X, -value.Y);
        FindPageNo();
      }
    }

    private GraphicCache GraphicCache
    {
      get { return Report.GraphicCache; }
    }

    public bool Disabled
    {
      get { return PageCount == 0 || FLocked || FLayoutLocked; }
    }
    #endregion

    #region Private Methods
    private void SetPageNo(int value, bool scrollTo)
    {
      if (value > PageCount)
        value = PageCount;
      if (value < 1)
        value = 1;
      bool pageChanged = value != FPageNo;
      FPageNo = value;
      if (Disabled)
        return;

      if (scrollTo)
        ScrollToCurrentPage();

      // update active page border
      if (pageChanged)
      {
        Invalidate();
        UpdatePageNumbers();
      }
    }

    private void ScrollToCurrentPage()
    {
      Rectangle pageBounds = FPreviewPages.GetPageBounds(PageNo - 1);
      pageBounds.Y -= 10;
      Offset = pageBounds.Location;
    }

    private void FindPageNo()
    {
      int pageNo = PreviewPages.FindPage(Offset.Y) + 1;
      if (!PreviewPages.IsSameRow(pageNo - 1, PageNo - 1))
        SetPageNo(pageNo, false);
    }

    private void DrawPages(Graphics g)
    {
      if (Disabled)
        return;

      // draw visible pages
      int firstVisible = PreviewPages.FindFirstVisiblePage(Offset.Y);
      int lastVisible = PreviewPages.FindLastVisiblePage(Offset.Y + ClientSize.Height);

      // draw pages from right to left if needed
      if (Config.RightToLeft)
      {
          int i = firstVisible;
          while (i <= lastVisible)
          {
              List<int> pagesInSameRow = new List<int>();
              pagesInSameRow.Add(i);
              // get list of pages in current row
              while (PreviewPages.IsSameRow(i, i + 1))
              {
                  pagesInSameRow.Add(i + 1);
                  i++;
              }
              if (pagesInSameRow.Count > 1)
              {
                  // swap pages in current row
                  for (int j = 0, k = pagesInSameRow.Count - 1; j < k; j++, k--)
                  {
                      PreviewPages.SwapPageBounds(pagesInSameRow[j], pagesInSameRow[k]);
                  }
              }
              i++;
          }
      }
      for (int i = firstVisible; i <= lastVisible; i++)
      {
        Rectangle pageBounds = PreviewPages.GetPageBounds(i);
        pageBounds.Offset(-Offset.X, -Offset.Y);
        ReportPage page = PreparedPages.GetCachedPage(i);

        // draw shadow around page
        bool drawShadow = !(Preview.ActivePageBorderColor == Color.White && Preview.BackColor == Color.White);
        if (drawShadow)
        {
          ShadowPaintInfo pi = new ShadowPaintInfo();
          pi.Graphics = g;
          pi.Rectangle = new Rectangle(pageBounds.Left - 4, pageBounds.Top - 4, pageBounds.Width + 4, pageBounds.Height + 4);
          pi.Size = 5;
          ShadowPainter.Paint2(pi);
        }

        // shift the origin because page.Draw draws at 0, 0
        g.TranslateTransform((int)pageBounds.Left, (int)pageBounds.Top);
        FRPaintEventArgs e = new FRPaintEventArgs(g, Zoom, Zoom, GraphicCache);
        // draw page
        page.Draw(e);

        // draw search highlight
        if (SearchInfo != null && SearchInfo.Visible && SearchInfo.PageNo == i + 1)
          page.DrawSearchHighlight(e, SearchInfo.ObjNo, SearchInfo.Ranges[SearchInfo.RangeNo]);
        g.ResetTransform();

        // draw border around active page
        if (i == PageNo - 1)
        {
          Pen borderPen = GraphicCache.GetPen(Preview.ActivePageBorderColor, 2, DashStyle.Solid);
          pageBounds.Inflate(-1, -1);
          g.DrawRectangle(borderPen, pageBounds);
        }
      }
    }

    private void DoZoom(float zoom)
    {
        DoZoom(zoom, new Point(Width / 2, Height / 2));
    }

    private void DoZoom(float zoom, Point relativeTo)
    {
        // pause control painting
        // if you don't do this, the page will flicker while zooming
        FAllowPaint = false;
        {
            Point mouse_position_on_workspace = new Point(Offset.X + relativeTo.X, Offset.Y + relativeTo.Y);
            PointF mouse_position_on_page_percent = new PointF();
            int pageNo = -1;

                // todo: avoid try/catch, upd: if catch does not happen, remove it
                try
                {
                    //    if(PreparedPages.Count)
                    pageNo = PreviewPages.FindPage(mouse_position_on_workspace.X, mouse_position_on_workspace.Y);
                    Rectangle pageBounds = PreviewPages.GetPageBounds(pageNo);
                    if (pageBounds.Width > 0 && pageBounds.Height > 0)
                    {
                        mouse_position_on_page_percent.X = (float)(mouse_position_on_workspace.X - pageBounds.X) / (float)pageBounds.Width;
                        mouse_position_on_page_percent.Y = (float)(mouse_position_on_workspace.Y - pageBounds.Y) / (float)pageBounds.Height;
                    }
                }
                catch { }

            // apply zoom value
            {
                if (zoom < 0.1f)
                    zoom = 0.1f;
                if (zoom > 10)
                    zoom = 10;
                if (Math.Abs(FZoom - 0.1f) < 0.001 && Math.Abs(zoom - 0.35f) < 0.001)
                    zoom = 0.25f;
                FZoom = zoom;
                FZoomMode = PreviewZoomMode.Normal;
                //Application.DoEvents(); // what used to do this line and why it's commented now?
                Preview.UpdateZoom(zoom);
                UpdatePages();
            }

            if (pageNo >= 0 && pageNo < PreviewPages.Count)
            {
                Rectangle pageBounds = PreviewPages.GetPageBounds(pageNo);
                Point  mouse_position_on_page_new = new Point(
                    (int)(pageBounds.Width * mouse_position_on_page_percent.X),
                    (int)(pageBounds.Height * mouse_position_on_page_percent.Y));
                Offset = new Point(pageBounds.X + mouse_position_on_page_new.X - relativeTo.X, pageBounds.Y + mouse_position_on_page_new.Y - relativeTo.Y);
            }
            else
                AutoScrollPosition = AutoScrollPosition;
        }
        FAllowPaint = true;
        Refresh();
    }

    #endregion

    #region Protected Methods
    protected override void OnPaint(PaintEventArgs e)
    {
      BackColor = Preview.BackColor;
      BackgroundImage = Preview.BackgroundImage;
      BackgroundImageLayout = Preview.BackgroundImageLayout;
      DefaultPaint = Preview.UseBackColor;
      base.OnPaint(e);
      DrawPages(e.Graphics);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (Disabled)
        return;

      int pageNo = PreviewPages.FindPage(Offset.X + e.X, Offset.Y + e.Y);
      ReportPage page = PreparedPages.GetCachedPage(pageNo);
      Rectangle pageBounds = PreviewPages.GetPageBounds(pageNo);
      SetPageNo(pageNo + 1, false);
      Cursor = curHandMove;
      FLastMousePoint = e.Location;

      // reset page's NeedRefresh flag
      page.NeedRefresh = false;
      page.NeedModify = false;

      // generate mousedown event
      if (FActiveObject != null)
      {
        FActiveObject.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks,
          (int)Math.Round((Offset.X + e.X - pageBounds.X) / Zoom - page.LeftMargin * Units.Millimeters - FActiveObject.AbsLeft),
          (int)Math.Round((Offset.Y + e.Y - pageBounds.Y) / Zoom - page.TopMargin * Units.Millimeters - FActiveObject.AbsTop),
          e.Delta));
      }

      // reset search highlight
      if (SearchInfo != null && SearchInfo.Visible)
      {
        SearchInfo.Visible = false;
        Refresh();
      }

      // modify and refresh the page if requested in the object's script
      if (page.NeedModify)
        PreparedPages.ModifyPage(pageNo, page);
      if (page.NeedRefresh)
        Refresh();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (Disabled)
        return;

      // find an object under mouse
      int pageNo = PreviewPages.FindPage(Offset.X + e.X, Offset.Y + e.Y);
      ReportPage page = PreparedPages.GetCachedPage(pageNo);
      Rectangle pageBounds = PreviewPages.GetPageBounds(pageNo);
      ReportComponentBase obj = page.HitTest(new PointF((Offset.X + e.X - pageBounds.X) / Zoom,
        (Offset.Y + e.Y - pageBounds.Y) / Zoom));

      // reset page's NeedRefresh flag
      page.NeedRefresh = false;
      page.NeedModify = false;

      // generate mouse enter, mouseleave events
      if (obj != FActiveObject)
      {
        if (FActiveObject != null)
          FActiveObject.OnMouseLeave(EventArgs.Empty);
        if (obj != null)
          obj.OnMouseEnter(EventArgs.Empty);
      }
      FActiveObject = obj;

      // generate mousemove event
      if (FActiveObject != null)
      {
        FActiveObject.OnMouseMove(new MouseEventArgs(e.Button, e.Clicks,
          (int)Math.Round((Offset.X + e.X - pageBounds.X) / Zoom - page.LeftMargin * Units.Millimeters - FActiveObject.AbsLeft),
          (int)Math.Round((Offset.Y + e.Y - pageBounds.Y) / Zoom - page.TopMargin * Units.Millimeters - FActiveObject.AbsTop),
          e.Delta));
      }

      string url = "";
      Cursor cursor = Cursors.Default;
      if (obj != null)
      {
        cursor = obj.Cursor;
        url = obj.Hyperlink.Value;
      }
      Cursor = cursor == Cursors.Default ? (e.Button == MouseButtons.Left ? curHandMove : curHand) : cursor;
      Preview.UpdateUrl(url);

      // prevent report panning if active object supports mouse events (currently Map object only)
      if (FActiveObject == null || !FActiveObject.HasFlag(Flags.InterceptsPreviewMouseEvents))
      if (e.Button == MouseButtons.Left)
      {
        Offset = new Point(Offset.X - (e.X - FLastMousePoint.X), Offset.Y - (e.Y - FLastMousePoint.Y));
        FLastMousePoint = e.Location;
      }

      // refresh page if requested in the object's script
      if (page.NeedModify)
        PreparedPages.ModifyPage(pageNo, page);
      if (page.NeedRefresh)
        Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (Disabled)
        return;

      Cursor = curHand;

      // find object under mouse to invoke its PreviewClick event
      int pageNo = PreviewPages.FindPage(Offset.X + e.X, Offset.Y + e.Y);
      ReportPage page = PreparedPages.GetCachedPage(pageNo);
      Rectangle pageBounds = PreviewPages.GetPageBounds(pageNo);

      // reset NeedRefresh flag
      page.NeedRefresh = false;
      page.NeedModify = false;
      Report.NeedRefresh = false;

      // generate mouseup event
      if (FActiveObject != null)
      {
        FActiveObject.OnMouseUp(new MouseEventArgs(e.Button, e.Clicks,
          (int)Math.Round((Offset.X + e.X - pageBounds.X) / Zoom - page.LeftMargin * Units.Millimeters - FActiveObject.AbsLeft),
          (int)Math.Round((Offset.Y + e.Y - pageBounds.Y) / Zoom - page.TopMargin * Units.Millimeters - FActiveObject.AbsTop),
          e.Delta));

        Hyperlink hyperlink = FActiveObject.Hyperlink;
        switch (hyperlink.Kind)
        {
          case HyperlinkKind.URL:
            try
            {
              if(!String.IsNullOrEmpty(hyperlink.Value))
                Process.Start(hyperlink.Value);
            }
            catch
            {
            }
            break;

          case HyperlinkKind.PageNumber:
            PageNo = int.Parse(hyperlink.Value);
            break;

          case HyperlinkKind.Bookmark:
            Bookmarks.BookmarkItem bookmark = PreparedPages.Bookmarks.Find(hyperlink.Value);
            if (bookmark != null)
              PositionTo(bookmark.PageNo + 1, new PointF(0, bookmark.OffsetY));
            break;

          case HyperlinkKind.DetailReport:
            if (hyperlink.Value == "")
              break;
            // if detail report name is empty, refresh this report
            if (String.IsNullOrEmpty(hyperlink.DetailReportName))
            {
              hyperlink.SetParameters(Report);
              Report.NeedRefresh = true;
            }
            else
            {
              // open a new report. check if such tab is opened already
              // check if this tab equals to the hyperlink value, open a new tab if so
              if (PreviewTab.HyperlinkValue == hyperlink.Value || !Preview.SwitchToTab(hyperlink))
              {
                FLocked = true;
                try
                {
                  Report report = hyperlink.GetReport(true);
                  report.Prepare();
                  Preview.AddPreviewTab(report, hyperlink.Value, hyperlink, true);
                }
                finally
                {
                  FLocked = false;
                }
              }
            }
            break;

          case HyperlinkKind.DetailPage:
            if (hyperlink.Value == "")
              break;
            ReportPage reportPage = Report.FindObject(hyperlink.DetailPageName) as ReportPage;
            if (reportPage != null)
            {
              hyperlink.SetParameters(Report);

              // open a new report. check if such tab is opened already
              // check if this tab equals to the hyperlink value, open a new tab if so
              if (PreviewTab.HyperlinkValue == hyperlink.Value || !Preview.SwitchToTab(hyperlink))
              {
                FLocked = true;
                Preview.Lock();
                PreparedPages pages = new PreparedPages(Report);
                try
                {
                  Report.SetPreparedPages(pages);
                  Report.PreparePage(reportPage);
                }
                finally
                {
                  FLocked = false;
                  Preview.Unlock();
                  Preview.AddPreviewTab(Report, hyperlink.Value, hyperlink, true);
                }
              }
            }
            break;
        }

        if (FActiveObject != null)
          FActiveObject.OnClick(EventArgs.Empty);
      }

      // modify and refresh the page if requested in the object's script
      if (page.NeedModify)
        PreparedPages.ModifyPage(pageNo, page);
      if (page.NeedRefresh)
        Refresh();
      if (Report.NeedRefresh)
      {
        bool saveShowProgress = Config.ReportSettings.ShowProgress;
        Config.ReportSettings.ShowProgress = false;
        RefreshReport();
        Config.ReportSettings.ShowProgress = saveShowProgress;
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (Disabled)
        return;
      Preview.DoClick();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (Disabled)
        return;

      if ((ModifierKeys & Keys.Control) != 0)
        DoZoom(Zoom + Math.Sign(e.Delta) * 0.25f, new Point(e.X, e.Y));
      else
      {
        if (FActiveObject != null && FActiveObject.HasFlag(Flags.InterceptsPreviewMouseEvents))
        {
          FActiveObject.OnMouseWheel(e);
          Refresh();
        }
        else
        {
          DoMouseWheel(e);
          FindPageNo();
        }
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (Disabled)
        return;

      if ((e.KeyData & Keys.Control) == 0)
      {
        switch (e.KeyCode)
        {
          case Keys.Up:
            Offset = new Point(Offset.X, Offset.Y - VScrollBar.SmallChange);
            break;
          case Keys.Down:
            Offset = new Point(Offset.X, Offset.Y + VScrollBar.SmallChange);
            break;
          case Keys.Left:
            Offset = new Point(Offset.X - HScrollBar.SmallChange, Offset.Y);
            break;
          case Keys.Right:
            Offset = new Point(Offset.X + HScrollBar.SmallChange, Offset.Y);
            break;
          case Keys.PageUp:
            Offset = new Point(Offset.X, Offset.Y - VScrollBar.LargeChange);
            break;
          case Keys.PageDown:
            Offset = new Point(Offset.X, Offset.Y + VScrollBar.LargeChange);
            break;
          case Keys.Home:
            PreviewTab.First();
            break;
          case Keys.End:
            PreviewTab.Last();
            break;
          case Keys.Add:
            PreviewTab.ZoomIn();
            break;
          case Keys.Subtract:
            PreviewTab.ZoomOut();
            break;
        }
      }
      else
      {
        switch (e.KeyCode)
        {
          case Keys.F:
            if ((Preview.Buttons & PreviewButtons.Find) != 0)
              PreviewTab.Find();
            break;
          case Keys.P:
            if ((Preview.Buttons & PreviewButtons.Print) != 0)
              PreviewTab.Print();
            break;
        }
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      return (keyData & Keys.Up) != 0 || (keyData & Keys.Down) != 0 ||
        (keyData & Keys.Left) != 0 || (keyData & Keys.Right) != 0;
    }

    protected override void OnScroll(ScrollEventArgs se)
    {
      base.OnScroll(se);
      if (Disabled)
        return;

      if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
        FindPageNo();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (Disabled)
        return;

      if (FZoomMode == PreviewZoomMode.PageWidth)
        ZoomPageWidth();
      else if (FZoomMode == PreviewZoomMode.WholePage)
        ZoomWholePage();
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
    public void UpdatePages()
    {
      PreviewPages.Clear();
      if (FLayoutLocked)
        return;

      for (int i = 0; i < PreparedPages.Count; i++)
      {
          SizeF pageSize = PreparedPages.GetPageSize(i);
          PreviewPages.AddPage(pageSize, Zoom);
      }
      PreviewPages.LayoutPages(Width - SystemInformation.VerticalScrollBarWidth, Preview.PageOffset);
      FLocked = false;
      UpdatePageNumbers();

      Size maxSize = PreviewPages.GetMaxSize(Preview.PageOffset);
      if (maxSize.Width > Width - SystemInformation.VerticalScrollBarWidth)
        maxSize.Height += SystemInformation.HorizontalScrollBarHeight;
      if (maxSize.Height > Height - SystemInformation.HorizontalScrollBarHeight)
        maxSize.Width += SystemInformation.VerticalScrollBarWidth;
      AutoScrollMinSize = maxSize;

      SetPageNo(PageNo, false);
      Refresh();
    }

    public void UpdatePageNumbers()
    {
      Preview.UpdatePageNumbers(PageNo, PageCount);
    }

    public void PositionTo(int pageNo, PointF point)
    {
      if (Disabled)
        return;

      RectangleF pageBounds = PreviewPages.GetPageBounds(pageNo - 1);
      ReportPage page = PreparedPages.GetCachedPage(pageNo - 1);
      Offset = new Point((int)Math.Round(pageBounds.Left + (page.LeftMargin * Units.Millimeters + point.X) * Zoom),
        (int)Math.Round(pageBounds.Top + (page.TopMargin * Units.Millimeters + point.Y) * Zoom) - 10);
      SetPageNo(pageNo, false);
    }

    public void RefreshReport()
    {
      FLocked = true;
      try
      {
        PreviewPages.Clear();
        PreparedPages.ClearPageCache();
        if (PreviewTab.DetailReportPage != null)
          Report.PreparePage(PreviewTab.DetailReportPage);
        else
          Report.InternalRefresh();
      }
      finally
      {
        FLocked = false;
      }
      UpdatePages();
    }

    public void UnlockLayout()
    {
      FLayoutLocked = false;
    }

    public void ZoomWholePage()
    {
      if (Disabled)
        return;

      SizeF pageSize = PreparedPages.GetPageSize(PageNo - 1);
      if (pageSize.Width / ClientSize.Width > pageSize.Height / ClientSize.Height)
        Zoom = (ClientSize.Width - 20) / pageSize.Width;
      else
        Zoom = (ClientSize.Height - 20) / pageSize.Height;
      FZoomMode = PreviewZoomMode.WholePage;
    }

    public void ZoomPageWidth()
    {
      if (Disabled)
        return;

      SizeF pageSize = PreparedPages.GetPageSize(PageNo - 1);
      Zoom = (ClientSize.Width - 20 - SystemInformation.VerticalScrollBarWidth) / pageSize.Width;
      FZoomMode = PreviewZoomMode.PageWidth;
    }
    #endregion

    public PreviewWorkspace(PreviewTab previewTab)
    {
      FPreviewTab = previewTab;
      FPreviewPages = new PreviewPages();
      curHand = ResourceLoader.GetCursor("Hand.cur");
      curHandMove = ResourceLoader.GetCursor("HandMove.cur");

      //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      Cursor = curHand;
      Dock = DockStyle.Fill;
      AutoScroll = true;
      ColorSchemeStyle = eDotNetBarStyle.Office2007;
      FastScrolling = Preview.FastScrolling;
      FLayoutLocked = true;
      //RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      VScrollBar.Dock = Config.RightToLeft ? DockStyle.Left : DockStyle.Right;
      //HScrollBar.RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
    }
  }
}
