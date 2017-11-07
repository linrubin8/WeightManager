using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastReport.Controls
{
    /// <summary>
    /// Represents a control that may contain several pages. It is similar to the TabControl
    /// but contains no tabs. This control is widely used in wizards.
    /// </summary>
    [Designer("FastReport.VSDesign.PageControlDesigner, FastReport.VSDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c, processorArchitecture=MSIL")]
  [ToolboxItem(false)]
  public class PageControl : ContainerControl
  {
    #region Fields
    private int FSelectorWidth;
    private int FSelectorTabHeight;
    private int FActivePageIndex;
    private int FHighlightPageIndex;
    #endregion
    
    #region Properties
    /// <summary>
    /// Occurs when page is selected.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when page is selected.")]
    public event EventHandler PageSelected;
    
    /// <summary>
    /// Gets or sets a value that determines whether the selector area is visible or not.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("The width of selector area. Set to 0 if you don't want the selector.")]
    public int SelectorWidth
    {
      get { return FSelectorWidth; }
      set 
      {
        FSelectorWidth = value;
        int padding = FSelectorWidth > 0 ? 1 : 0;
        Padding = new Padding(FSelectorWidth, padding, padding, padding);
        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the height of selector tab.
    /// </summary>
    [DefaultValue(35)]
    [Category("Appearance")]
    [Description("The height of selector tab.")]
    public int SelectorTabHeight
    {
      get { return FSelectorTabHeight; }
      set 
      { 
        FSelectorTabHeight = value; 
        Refresh();
      }
    }

    /// <summary>
    /// This property is not relevant to this class
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
      get { return base.Padding; }
      set { base.Padding = value; }
    }
    
    /// <summary>
    /// Gets or sets the active page.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Panel ActivePage
    {
      get { return Controls[ActivePageIndex] as Panel; }
      set { ActivePageIndex = Controls.IndexOf(value); }
    }

    /// <summary>
    /// Gets or sets the index of active page.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ActivePageIndex
    {
      get { return Controls.Count == 0 ? -1 : FActivePageIndex; }
      set 
      { 
        if (value >= Controls.Count)
          value = Controls.Count - 1;
        if (value <= 0)
          value = 0;
        FActivePageIndex = value;
        
        // avoid page reordering
        SuspendLayout();
        foreach (Control c in Controls)
        {
          c.Visible = true;
        }
        for (int i = 0; i < Controls.Count; i++)
        {
          Control c = Controls[i];
          c.Visible = i == FActivePageIndex;
          if (DesignMode && c.Visible)
          {
            ISelectionService service = ((ISelectionService)GetService(typeof(ISelectionService)));
            if (service != null)
              service.SetSelectedComponents(new Control[] { c });
          }
        }
        
        ResumeLayout();
        Refresh();
        OnPageSelected();
      }
    }
    
    /// <summary>
    /// Gets or sets the highlighted page index.
    /// </summary>
    public int HighlightPageIndex
    {
      get { return FHighlightPageIndex; }
      set
      {
        if (FHighlightPageIndex != value)
        {
          FHighlightPageIndex = value;
          Refresh();
        }
      }
    }

    /// <summary>
    /// Gets the collection of pages.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ControlCollection Pages
    {
      get { return Controls; }
    }
    #endregion

    #region Private Methods
    private void DrawTab(Graphics g, int pageIndex)
    {
      if (pageIndex == -1)
        return;
        
      bool active = pageIndex == ActivePageIndex;
      bool highlight = pageIndex == HighlightPageIndex;

      int left = 2;
      int top = 2;
      int width = Padding.Left - left - 1;
      int height = SelectorTabHeight;
      
      top += pageIndex * height;
      if (active)
      {
        left -= 2;
        top -= 2;
        height += 4;
        width += 4;
      }

      Point[] points = new Point[] {
            new Point(left + 2, top),
            new Point(left + width, top),
            new Point(left + width, top + height),
            new Point(left + 2, top + height),
            new Point(left, top + height - 2),
            new Point(left, top + 2),
            new Point(left + 2, top) };

      Brush brush = null;
      if (active)
        brush = new SolidBrush(SystemColors.Window);
      else
        brush = new LinearGradientBrush(new Rectangle(left, top, width, height),
          SystemColors.Window, SystemColors.Control, 0f);

      g.FillPolygon(brush, points);
      g.DrawPolygon(SystemPens.ControlDark, points);
      
      Pen highlightPen = SystemPens.Window;
      if (active || highlight)
        highlightPen = Pens.Orange;
        
      g.DrawLine(highlightPen, left + 1, top + 2, left + 1, top + height - 2);
      g.DrawLine(highlightPen, left + 2, top + 1, left + 2, top + height - 1);
      g.DrawLine(highlightPen, left + 3, top + 1, left + 3, top + height - 1);
      
      Font font = new Font(Font, active ? FontStyle.Bold : FontStyle.Regular);
      int textWidth = active ? width - 16 : width - 24;
      TextRenderer.DrawText(g, Controls[pageIndex].Text, font, 
        new Rectangle(left + 12, top, textWidth, height), SystemColors.ControlText, 
        TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);

      brush.Dispose();
      brush = null;
      font.Dispose();
      font = null;
    }
    
    private void OnPageSelected()
    {
      if (PageSelected != null)
        PageSelected(this, EventArgs.Empty);
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      
      if (SelectorWidth > 0)
      {
        g.DrawRectangle(SystemPens.ControlDark, 
          new Rectangle(Padding.Left - 1, 0, Width - Padding.Left, Height - 1));

        // draw tabs other than active
        for (int i = 0; i < Controls.Count; i++)
        {
          if (i != ActivePageIndex)
            DrawTab(g, i);
        }
        
        // draw active tab
        DrawTab(g, ActivePageIndex);
      }
    }

    /// <inheritdoc/>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      HighlightPageIndex = GetTabAt(e.Location);
    }

    /// <inheritdoc/>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      int pageIndex = GetTabAt(e.Location);
      if (pageIndex != -1)
        ActivePageIndex = pageIndex;
    }

    /// <inheritdoc/>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      HighlightPageIndex = -1;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Gets tab at specified mouse point.
    /// </summary>
    /// <param name="pt">The mouse point.</param>
    /// <returns>Index of tab under mouse; -1 if mouse is outside tab area.</returns>
    public int GetTabAt(Point pt)
    {
      for (int i = 0; i < Controls.Count; i++)
      {
        int left = 2;
        int top = 2;
        int width = Padding.Left - left - 1;
        int height = SelectorTabHeight;

        top += i * height;
        if (i == ActivePageIndex)
        {
          left -= 2;
          top -= 2;
          height += 4;
          width += 4;
        }

        if (new Rectangle(left, top, width, height).Contains(pt))
          return i;
      }
      return -1;
    }
    
    /// <summary>
    /// Selects the next page.
    /// </summary>
    public void SelectNextPage()
    {
      if (ActivePageIndex < Pages.Count - 1)
        ActivePageIndex++;
      else
        ActivePageIndex = 0;
    }

    /// <summary>
    /// Selects the previous page.
    /// </summary>
    public void SelectPrevPage()
    {
      if (ActivePageIndex > 0)
        ActivePageIndex--;
      else
        ActivePageIndex = Pages.Count - 1;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PageControl"/> class with default settings.
    /// </summary>
    public PageControl()
    {
      FHighlightPageIndex = -1;
      FSelectorTabHeight = 35;
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }
  }

  /// <summary>
  /// This class represents a single page of the <see cref="PageControl"/> control.
  /// </summary>
  [ToolboxItem(false)]
  public class PageControlPage : Panel
  {
    /// <summary>
    /// Gets or sets the page caption text.
    /// </summary>
    [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    public override string Text
    {
      get { return base.Text; }
      set 
      { 
        base.Text = value;
        if (Parent is PageControl)
          Parent.Refresh();
      }
    }
  }


}
