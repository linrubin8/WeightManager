using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Forms;
using FastReport.TypeConverters;
using FastReport.Design.PageDesigners.Page;
using FastReport.TypeEditors;

namespace FastReport
{
  /// <summary>
  /// Represents a report page.
  /// </summary>
  /// <remarks>
  /// To get/set a paper size and orientation, use the <see cref="PaperWidth"/>, <see cref="PaperHeight"/>
  /// and <see cref="Landscape"/> properties. Note that paper size is measured in millimeters.
  /// <para/>Report page can contain one or several bands with report objects. Use the <see cref="ReportTitle"/>, 
  /// <see cref="ReportSummary"/>, <see cref="PageHeader"/>, <see cref="PageFooter"/>, 
  /// <see cref="ColumnHeader"/>, <see cref="ColumnFooter"/>, <see cref="Overlay"/> properties
  /// to get/set the page bands. The <see cref="Bands"/> property holds the list of data bands or groups. 
  /// Thus you may add several databands to this property to create master-master reports, for example.
  /// <note type="caution">
  /// Report page can contain bands only. You cannot place report objects such as <b>TextObject</b> on a page.
  /// </note>
  /// </remarks>
  /// <example>
  /// This example shows how to create a page with one <b>ReportTitleBand</b> and <b>DataBand</b> bands and add
  /// it to the report.
  /// <code>
  /// ReportPage page = new ReportPage();
  /// // set the paper in millimeters
  /// page.PaperWidth = 210;
  /// page.PaperHeight = 297;
  /// // create report title
  /// page.ReportTitle = new ReportTitleBand();
  /// page.ReportTitle.Name = "ReportTitle1";
  /// page.ReportTitle.Height = Units.Millimeters * 10;
  /// // create data band
  /// DataBand data = new DataBand();
  /// data.Name = "Data1";
  /// data.Height = Units.Millimeters * 10;
  /// // add data band to the page
  /// page.Bands.Add(data);
  /// // add page to the report
  /// report.Pages.Add(page);
  /// </code>
  /// </example>
  public class ReportPage : PageBase, IParent, IHasEditor
  {
    #region Constants
    
    private const float MAX_PAPER_SIZE_MM = 100000;

    #endregion // Constants
    
    #region Fields
    private float FPaperWidth;
    private float FPaperHeight;
    private int FRawPaperSize;
    private bool FLandscape;
    private float FLeftMargin;
    private float FTopMargin;
    private float FRightMargin;
    private float FBottomMargin;
    private int FFirstPageSource;
    private int FOtherPagesSource;
    private Duplex FDuplex;
    private bool FMirrorMargins;
    private PageColumns FColumns;
    private FloatCollection FGuides;
    private Border FBorder;
    private FillBase FFill;
    private Watermark FWatermark;
    private bool FTitleBeforeHeader;
    private string FOutlineExpression;
    private bool FPrintOnPreviousPage;
    private bool FResetPageNumber;
    private bool FExtraDesignWidth;
    private bool FStartOnOddPage;
    private bool FBackPage;
    private SubreportObject FSubreport;
    private PageHeaderBand FPageHeader;
    private ReportTitleBand FReportTitle;
    private ColumnHeaderBand FColumnHeader;
    private BandCollection FBands;
    private ReportSummaryBand FReportSummary;
    private ColumnFooterBand FColumnFooter;
    private PageFooterBand FPageFooter;
    private OverlayBand FOverlay;
    private string FStartPageEvent;
    private string FFinishPageEvent;
    private string FManualBuildEvent;

    private bool FUnlimitedHeight;
    private bool FUnlimitedWidth;
    private float FUnlimitedHeightValue;
    private float FUnlimitedWidthValue;

    #endregion

    #region Properties
    /// <summary>
    /// This event occurs when the report engine starts this page.
    /// </summary>
    public event EventHandler StartPage;
    
    /// <summary>
    /// This event occurs when the report engine finished this page.
    /// </summary>
    public event EventHandler FinishPage;

    /// <summary>
    /// This event occurs when the report engine is about to print databands in this page.
    /// </summary>
    public event EventHandler ManualBuild;

    /// <summary>
    /// Gets or sets a width of the paper, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float PaperWidth
    {
      get { return FPaperWidth; }
      set { FPaperWidth = value; }
    }

    /// <summary>
    /// Gets or sets a height of the paper, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float PaperHeight
    {
      get { return FPaperHeight; }
      set { FPaperHeight = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page has unlimited height.
    /// </summary>
    [DefaultValue(false)]
    [Category("Paper")]
    public bool UnlimitedHeight
    {
        get { return FUnlimitedHeight; }
        set { FUnlimitedHeight = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page has unlimited width.
    /// </summary>
    [DefaultValue(false)]
    [Category("Paper")]
    public bool UnlimitedWidth
    {
        get { return FUnlimitedWidth; }
        set { FUnlimitedWidth = value; }
    }

    /// <summary>
    /// Get or set the current height of unlimited page.
    /// </summary>
    [Browsable(false)]
    public float UnlimitedHeightValue
    {
        get { return FUnlimitedHeightValue; }
        set { FUnlimitedHeightValue = value; }
    }

    /// <summary>
    /// Get or set the current width of unlimited page.
    /// </summary>
    [Browsable(false)]
    public float UnlimitedWidthValue
    {
        get { return FUnlimitedWidthValue; }
        set { FUnlimitedWidthValue = value; }
    }

    /// <summary>
    /// Gets the current page height in pixels.
    /// </summary>
    [Browsable(false)]
    public float HeightInPixels
    {
        get
        {
            return UnlimitedHeight ? UnlimitedHeightValue : PaperHeight * Units.Millimeters;
        }
    }

    /// <summary>
    /// Gets the current page width in pixels.
    /// </summary>
    [Browsable(false)]
    public float WidthInPixels
    {
        get
        {
            if (UnlimitedWidth)
            {
                if (!IsDesigning)
                {
                    return UnlimitedWidthValue;
                }
            }      
            return PaperWidth * Units.Millimeters;
            
        }
    }

    /// <summary>
    /// Gets or sets the raw index of a paper size.
    /// </summary>
    /// <remarks>
    /// This property stores the RawKind value of a selected papersize. It is used to distiguish
    /// between several papers with the same size (for ex. "A3" and "A3 with no margins") used in some
    /// printer drivers. 
    /// <para/>It is not obligatory to set this property. FastReport will select the
    /// necessary paper using the <b>PaperWidth</b> and <b>PaperHeight</b> values.
    /// </remarks>
    [Category("Paper")]
    [DefaultValue(0)]
    public int RawPaperSize
    {
      get { return FRawPaperSize; }
      set { FRawPaperSize = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that page should be in landscape orientation.
    /// </summary>
    /// <remarks>
    /// When you change this property, it will automatically swap paper width and height, as well as paper margins.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Paper")]
    public bool Landscape
    {
      get { return FLandscape; }
      set 
      {
        if (FLandscape != value)
        {
          float e = FPaperWidth;
          FPaperWidth = FPaperHeight;
          FPaperHeight = e;

          float m1 = FLeftMargin;   //     m3
          float m2 = FRightMargin;  //  m1    m2
          float m3 = FTopMargin;    //     m4
          float m4 = FBottomMargin; //

          if (value)
          {
            FLeftMargin = m3;       // rotate counter-clockwise
            FRightMargin = m4;
            FTopMargin = m2;
            FBottomMargin = m1;
          }
          else
          {
            FLeftMargin = m4;       // rotate clockwise
            FRightMargin = m3;
            FTopMargin = m1;
            FBottomMargin = m2;
          }
        }
        FLandscape = value;
      }
    }

    /// <summary>
    /// Gets or sets the left page margin, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float LeftMargin
    {
      get { return FLeftMargin; }
      set { FLeftMargin = value; }
    }

    /// <summary>
    /// Gets or sets the top page margin, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float TopMargin
    {
      get { return FTopMargin; }
      set { FTopMargin = value; }
    }

    /// <summary>
    /// Gets or sets the right page margin, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float RightMargin
    {
      get { return FRightMargin; }
      set { FRightMargin = value; }
    }

    /// <summary>
    /// Gets or sets the bottom page margin, in millimeters.
    /// </summary>
    [TypeConverter(typeof(PaperConverter))]
    [Category("Paper")]
    public float BottomMargin
    {
      get { return FBottomMargin; }
      set { FBottomMargin = value; }
    }

    /// <summary>
    /// Gets or sets the paper source for the first printed page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property represents the paper source (printer tray) that will be used when printing
    /// the first page. To set the source for other pages, use the <see cref="OtherPagesSource"/> property.
    /// </para>
    /// <para>
    /// Note: This property uses the <b>raw</b> number of the paper source.
    /// </para>
    /// </remarks>
    [DefaultValue(7)]
    [Category("Print")]
    public int FirstPageSource
    {
      get { return FFirstPageSource; }
      set { FFirstPageSource = value; }
    }

    /// <summary>
    /// Gets or sets the paper source for all printed pages except the first one.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property represents the paper source (printer tray) that will be used when printing
    /// all pages except the first one. To set source for the first page, use 
    /// the <see cref="FirstPageSource"/> property.
    /// </para>
    /// <para>
    /// Note: This property uses the <b>raw</b> number of the paper source.
    /// </para>
    /// </remarks>
    [DefaultValue(7)]
    [Category("Print")]
    public int OtherPagesSource
    {
      get { return FOtherPagesSource; }
      set { FOtherPagesSource = value; }
    }

    /// <summary>
    /// Gets or sets the printer duplex mode that will be used when printing this page.
    /// </summary>
    [DefaultValue(Duplex.Default)]
    [Category("Print")]
    public Duplex Duplex
    {
      get { return FDuplex; }
      set { FDuplex = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that even pages should swap its left and right margins when
    /// previewed or printed.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool MirrorMargins
    {
      get { return FMirrorMargins; }
      set { FMirrorMargins = value; }
    }

    /// <summary>
    /// Gets the page columns settings.
    /// </summary>
    [Category("Appearance")]
    public PageColumns Columns
    {
      get { return FColumns; }
    }
    
    /// <summary>
    /// Gets or sets the page border that will be printed inside the page printing area.
    /// </summary>
    [Category("Appearance")]
    public Border Border
    {
      get { return FBorder; }
      set { FBorder = value; }
    }

    
    /// <summary>
    /// Gets or sets the page background fill.
    /// </summary>
    [Category("Appearance")]
    [Editor(typeof(FillEditor), typeof(UITypeEditor))]
    public FillBase Fill
    {
      get { return FFill; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Fill");
        FFill = value;
      }
    }

    /// <summary>
    /// Gets or sets the page watermark.
    /// </summary>
    /// <remarks>
    /// To enabled watermark, set its <b>Enabled</b> property to <b>true</b>.
    /// </remarks>
    [Category("Appearance")]
    public Watermark Watermark
    {
      get { return FWatermark; }
      set 
      { 
        if (FWatermark != value)
          if (FWatermark != null)
            FWatermark.Dispose();
        FWatermark = value; 
      }
    }
    
    /// <summary>
    /// Gets or sets a value indicating that <b>ReportTitle</b> band should be printed before the 
    /// <b>PageHeader</b> band.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool TitleBeforeHeader
    {
      get { return FTitleBeforeHeader; }
      set { FTitleBeforeHeader = value; }
    }

    /// <summary>
    /// Gets or sets an outline expression.
    /// </summary>
    /// <remarks>
    /// For more information, see <see cref="BandBase.OutlineExpression"/> property.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string OutlineExpression
    {
      get { return FOutlineExpression; }
      set { FOutlineExpression = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to start to print this page on a free space of the previous page.
    /// </summary>
    /// <remarks>
    /// This property can be used if you have two or more pages in the report template.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PrintOnPreviousPage
    {
      get { return FPrintOnPreviousPage; }
      set { FPrintOnPreviousPage = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that FastReport engine must reset page numbers before printing this page.
    /// </summary>
    /// <remarks>
    /// This property can be used if you have two or more pages in the report template.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ResetPageNumber
    {
      get { return FResetPageNumber; }
      set { FResetPageNumber = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page has extra width in the report designer.
    /// </summary>
    /// <remarks>
    /// This property may be useful if you work with such objects as Matrix and Table.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Design")]
    public bool ExtraDesignWidth
    {
      get { return FExtraDesignWidth; }
      set { FExtraDesignWidth = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page will start on an odd page only.
    /// </summary>
    /// <remarks>
    /// This property is useful to print booklet-type reports. Setting this property to <b>true</b>
    /// means that this page will start to print on an odd page only. If necessary, an empty page
    /// will be added to the prepared report before this page will be printed.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool StartOnOddPage
    {
      get { return FStartOnOddPage; }
      set { FStartOnOddPage = value; }
    }

    /// <summary>
    /// Uses this page as a back page for previously printed pages.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool BackPage
    {
      get { return FBackPage; }
      set { FBackPage = value; }
    }

    /// <summary>
    /// Gets or sets a report title band.
    /// </summary>
    [Browsable(false)]
    public ReportTitleBand ReportTitle
    {
      get { return FReportTitle; }
      set
      {
        SetProp(FReportTitle, value);
        FReportTitle = value;
      }
    }

    /// <summary>
    /// Gets or sets a report summary band.
    /// </summary>
    [Browsable(false)]
    public ReportSummaryBand ReportSummary
    {
      get { return FReportSummary; }
      set
      {
        SetProp(FReportSummary, value);
        FReportSummary = value;
      }
    }

    /// <summary>
    /// Gets or sets a page header band.
    /// </summary>
    [Browsable(false)]
    public PageHeaderBand PageHeader
    {
      get { return FPageHeader; }
      set
      {
        SetProp(FPageHeader, value);
        FPageHeader = value;
      }
    }

    /// <summary>
    /// Gets or sets a page footer band.
    /// </summary>
    [Browsable(false)]
    public PageFooterBand PageFooter
    {
      get { return FPageFooter; }
      set
      {
        SetProp(FPageFooter, value);
        FPageFooter = value;
      }
    }

    /// <summary>
    /// Gets or sets a column header band.
    /// </summary>
    [Browsable(false)]
    public ColumnHeaderBand ColumnHeader
    {
      get { return FColumnHeader; }
      set
      {
        SetProp(FColumnHeader, value);
        FColumnHeader = value;
      }
    }

    /// <summary>
    /// Gets or sets a column footer band.
    /// </summary>
    [Browsable(false)]
    public ColumnFooterBand ColumnFooter
    {
      get { return FColumnFooter; }
      set
      {
        SetProp(FColumnFooter, value);
        FColumnFooter = value;
      }
    }

    /// <summary>
    /// Gets or sets an overlay band.
    /// </summary>
    [Browsable(false)]
    public OverlayBand Overlay
    {
      get { return FOverlay; }
      set
      {
        SetProp(FOverlay, value);
        FOverlay = value;
      }
    }

    /// <summary>
    /// Gets the collection of data bands or group header bands.
    /// </summary>
    /// <remarks>
    /// The <b>Bands</b> property holds the list of data bands or group headers. 
    /// Thus you may add several databands to this property to create master-master reports, for example.
    /// </remarks>
    [Browsable(false)]
    public BandCollection Bands
    {
      get { return FBands; }
    }

    /// <summary>
    /// Gets or sets the page guidelines.
    /// </summary>
    /// <remarks>
    /// This property hold all vertical guidelines. The horizontal guidelines are owned by the bands (see
    /// <see cref="BandBase.Guides"/> property).
    /// </remarks>
    [Browsable(false)]
    public FloatCollection Guides
    {
      get { return FGuides; } 
      set { FGuides = value; }
    }

    /// <summary>
    /// Gets or sets the reference to a parent <b>SubreportObject</b> that owns this page.
    /// </summary>
    /// <remarks>
    /// This property is <b>null</b> for regular report pages. See the <see cref="SubreportObject"/> for details.
    /// </remarks>
    [Browsable(false)]
    public SubreportObject Subreport
    {
      get { return FSubreport; }
      set { FSubreport = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the report engine starts this page.
    /// </summary>
    [Category("Build")]
    public string StartPageEvent
    {
      get { return FStartPageEvent; }
      set { FStartPageEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the report engine finished this page.
    /// </summary>
    [Category("Build")]
    public string FinishPageEvent
    {
      get { return FFinishPageEvent; }
      set { FFinishPageEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script event name that will be fired when the report engine is about 
    /// to print databands in this page.
    /// </summary>
    [Category("Build")]
    public string ManualBuildEvent
    {
      get { return FManualBuildEvent; }
      set { FManualBuildEvent = value; }
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
      get { return base.Width; }
      set { base.Width = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override float Height
    {
      get { return base.Height; }
      set { base.Height = value; }
    }
    
    /// <inheritdoc/>
    public override SizeF SnapSize
    {
      get { return new SizeF(ReportWorkspace.Grid.SnapSize, ReportWorkspace.Grid.SnapSize); }
    }

    /// <summary>
    /// Gets a value indicating that imperial units (inches, hundreths of inches) are used.
    /// </summary>
    [Browsable(false)]
    public bool IsImperialUnitsUsed
    {
      get
      {
        return ReportWorkspace.Grid.GridUnits == PageUnits.Inches ||
          ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch;
      }
    }

    internal bool IsManualBuild
    {
      get { return !String.IsNullOrEmpty(FManualBuildEvent) || ManualBuild != null; }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeBorder()
    {
      return !Border.Equals(new Border());
    }

    private bool ShouldSerializeFill()
    {
      return !(Fill is SolidFill) || (Fill as SolidFill).Color != SystemColors.Window;
    }

    private void DrawBackground(FRPaintEventArgs e, RectangleF rect)
    {
      rect.Width *= e.ScaleX;
      rect.Height *= e.ScaleY;
      Brush brush = null;
      if (Fill is SolidFill)
        brush = e.Cache.GetBrush((Fill as SolidFill).Color);
      else
        brush = Fill.CreateBrush(rect);

      e.Graphics.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
      if (!(Fill is SolidFill))
        brush.Dispose();
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
            if (disposing)
            {
                Watermark.Dispose();
                Watermark = null;
            }
      base.Dispose(disposing);
    }
    #endregion

    #region IParent
    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      if (TitleBeforeHeader)
      {
        list.Add(FReportTitle);
        list.Add(FPageHeader);
      }
      else
      {
        list.Add(FPageHeader);
        list.Add(FReportTitle);
      }
      list.Add(FColumnHeader);
      foreach (BandBase band in FBands)
      {
        list.Add(band);
      }
      list.Add(FReportSummary);
      list.Add(FColumnFooter);
      list.Add(FPageFooter);
      list.Add(FOverlay);
    }

    /// <inheritdoc/>
    public virtual bool CanContain(Base child)
    {
      if (IsRunning)
        return child is BandBase;
      return (child is PageHeaderBand || child is ReportTitleBand || child is ColumnHeaderBand ||
        child is DataBand || child is GroupHeaderBand || child is ColumnFooterBand ||
        child is ReportSummaryBand || child is PageFooterBand || child is OverlayBand);
    }

    /// <inheritdoc/>
    public virtual void AddChild(Base child)
    {
      if (IsRunning)
      {
        FBands.Add(child as BandBase);
        return;
      }
      if (child is PageHeaderBand)
        PageHeader = child as PageHeaderBand;
      if (child is ReportTitleBand)
        ReportTitle = child as ReportTitleBand;
      if (child is ColumnHeaderBand)
        ColumnHeader = child as ColumnHeaderBand;
      if (child is DataBand || child is GroupHeaderBand)
        FBands.Add(child as BandBase);
      if (child is ReportSummaryBand)
        ReportSummary = child as ReportSummaryBand;
      if (child is ColumnFooterBand)
        ColumnFooter = child as ColumnFooterBand;
      if (child is PageFooterBand)
        PageFooter = child as PageFooterBand;
      if (child is OverlayBand)
        Overlay = child as OverlayBand;
    }

    /// <inheritdoc/>
    public virtual void RemoveChild(Base child)
    {
      if (IsRunning)
      {
        FBands.Remove(child as BandBase);
        return;
      }
      if (child is PageHeaderBand && FPageHeader == child as PageHeaderBand)
        PageHeader = null;
      if (child is ReportTitleBand && FReportTitle == child as ReportTitleBand)
        ReportTitle = null;
      if (child is ColumnHeaderBand && FColumnHeader == child as ColumnHeaderBand)
        ColumnHeader = null;
      if (child is DataBand || child is GroupHeaderBand)
        FBands.Remove(child as BandBase);
      if (child is ReportSummaryBand && FReportSummary == child as ReportSummaryBand)
        ReportSummary = null;
      if (child is ColumnFooterBand && FColumnFooter == child as ColumnFooterBand)
        ColumnFooter = null;
      if (child is PageFooterBand && FPageFooter == child as PageFooterBand)
        PageFooter = null;
      if (child is OverlayBand && FOverlay == child as OverlayBand)
        Overlay = null;
    }

    /// <inheritdoc/>
    public virtual int GetChildOrder(Base child)
    {
      return FBands.IndexOf(child as BandBase);
    }

    /// <inheritdoc/>
    public virtual void SetChildOrder(Base child, int order)
    {
      if (order > FBands.Count)
        order = FBands.Count;
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (oldOrder <= order)
          order--;
        FBands.Remove(child as BandBase);
        FBands.Insert(order, child as BandBase);
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
      base.Assign(source);
      
      ReportPage src = source as ReportPage;
      Landscape = src.Landscape;
      PaperWidth = src.PaperWidth;
      PaperHeight = src.PaperHeight;
      RawPaperSize = src.RawPaperSize;
      LeftMargin = src.LeftMargin;
      TopMargin = src.TopMargin;
      RightMargin = src.RightMargin;
      BottomMargin = src.BottomMargin;
      MirrorMargins = src.MirrorMargins;
      FirstPageSource = src.FirstPageSource;
      OtherPagesSource = src.OtherPagesSource;
      Duplex = src.Duplex;
      Columns.Assign(src.Columns);
      Guides.Assign(src.Guides);
      Border = src.Border.Clone();
      Fill = src.Fill.Clone();
      Watermark.Assign(src.Watermark);
      TitleBeforeHeader = src.TitleBeforeHeader;
      OutlineExpression = src.OutlineExpression;
      PrintOnPreviousPage = src.PrintOnPreviousPage;
      ResetPageNumber = src.ResetPageNumber;
      ExtraDesignWidth = src.ExtraDesignWidth;
      BackPage = src.BackPage;
      StartOnOddPage = src.StartOnOddPage;
      StartPageEvent = src.StartPageEvent;
      FinishPageEvent = src.FinishPageEvent;
      ManualBuildEvent = src.ManualBuildEvent;
      UnlimitedHeight = src.UnlimitedHeight;
      UnlimitedWidth = src.UnlimitedWidth;
      UnlimitedHeightValue = src.UnlimitedHeightValue;
      UnlimitedWidthValue = src.UnlimitedWidthValue;
    }

    /// <inheritdoc/>
    public override void DrawSelection(FRPaintEventArgs e)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      // do nothing
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
    public override void Serialize(FRWriter writer)
    {
      ReportPage c = writer.DiffObject as ReportPage;
      base.Serialize(writer);

      if (Landscape != c.Landscape)
        writer.WriteBool("Landscape", Landscape);
      if (FloatDiff(PaperWidth, c.PaperWidth))
        writer.WriteFloat("PaperWidth", PaperWidth);
      if (FloatDiff(PaperHeight, c.PaperHeight))
        writer.WriteFloat("PaperHeight", PaperHeight);
      if (RawPaperSize != c.RawPaperSize)
        writer.WriteInt("RawPaperSize", RawPaperSize);
      if (FloatDiff(LeftMargin, c.LeftMargin))
        writer.WriteFloat("LeftMargin", LeftMargin);
      if (FloatDiff(TopMargin, c.TopMargin))
        writer.WriteFloat("TopMargin", TopMargin);
      if (FloatDiff(RightMargin, c.RightMargin))
        writer.WriteFloat("RightMargin", RightMargin);
      if (FloatDiff(BottomMargin, c.BottomMargin))
        writer.WriteFloat("BottomMargin", BottomMargin);
      if (MirrorMargins != c.MirrorMargins)
        writer.WriteBool("MirrorMargins", MirrorMargins);
      if (FirstPageSource != c.FirstPageSource)
        writer.WriteInt("FirstPageSource", FirstPageSource);
      if (OtherPagesSource != c.OtherPagesSource)
        writer.WriteInt("OtherPagesSource", OtherPagesSource);
      if (Duplex != c.Duplex)
        writer.WriteValue("Duplex", Duplex);
      Columns.Serialize(writer, c.Columns);
      if (Guides.Count > 0)
        writer.WriteValue("Guides", Guides);
      Border.Serialize(writer, "Border", c.Border);
      Fill.Serialize(writer, "Fill", c.Fill);
      Watermark.Serialize(writer, "Watermark", c.Watermark);
      if (TitleBeforeHeader != c.TitleBeforeHeader)
        writer.WriteBool("TitleBeforeHeader", TitleBeforeHeader);
      if (OutlineExpression != c.OutlineExpression)
        writer.WriteStr("OutlineExpression", OutlineExpression);
      if (PrintOnPreviousPage != c.PrintOnPreviousPage)
        writer.WriteBool("PrintOnPreviousPage", PrintOnPreviousPage);
      if (ResetPageNumber != c.ResetPageNumber)
        writer.WriteBool("ResetPageNumber", ResetPageNumber);
      if (ExtraDesignWidth != c.ExtraDesignWidth)
        writer.WriteBool("ExtraDesignWidth", ExtraDesignWidth);
      if (StartOnOddPage != c.StartOnOddPage)
        writer.WriteBool("StartOnOddPage", StartOnOddPage);
      if (BackPage != c.BackPage)
        writer.WriteBool("BackPage", BackPage);
      if (StartPageEvent != c.StartPageEvent)
        writer.WriteStr("StartPageEvent", StartPageEvent);
      if (FinishPageEvent != c.FinishPageEvent)
        writer.WriteStr("FinishPageEvent", FinishPageEvent);
      if (ManualBuildEvent != c.ManualBuildEvent)
        writer.WriteStr("ManualBuildEvent", ManualBuildEvent);
      if (UnlimitedHeight != c.UnlimitedHeight)
        writer.WriteBool("UnlimitedHeight", UnlimitedHeight);
      if (UnlimitedWidth != c.UnlimitedWidth)
        writer.WriteBool("UnlimitedWidth", UnlimitedWidth);
      if (FloatDiff(UnlimitedHeightValue, c.UnlimitedHeightValue))
        writer.WriteFloat("UnlimitedHeightValue", UnlimitedHeightValue);
      if (FloatDiff(UnlimitedWidthValue, c.UnlimitedWidthValue))
        writer.WriteFloat("UnlimitedWidthValue", UnlimitedWidthValue);
    }

    /// <inheritdoc/>
    public override Type GetPageDesignerType()
    {
      return typeof(ReportPageDesigner);
    }

    /// <summary>
    /// Invokes the object's editor.
    /// </summary>
    public bool InvokeEditor()
    {
      using (PageSetupForm editor = new PageSetupForm())
      {
        editor.Page = this;
        return editor.ShowDialog() == DialogResult.OK;
      }
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      if (IsDesigning)
        return;
      
      Graphics g = e.Graphics;
      RectangleF pageRect = new RectangleF(0, 0,
        WidthInPixels - 1 / e.ScaleX, HeightInPixels - 1 / e.ScaleY);
      RectangleF printableRect = new RectangleF(
        LeftMargin * Units.Millimeters,
        TopMargin * Units.Millimeters,
        (PaperWidth - LeftMargin - RightMargin) * Units.Millimeters,
        (PaperHeight - TopMargin - BottomMargin) * Units.Millimeters);

      DrawBackground(e, pageRect);
      Border.Draw(e, printableRect);
      if (Watermark.Enabled)
      {
        if (!Watermark.ShowImageOnTop)
          Watermark.DrawImage(e, pageRect, Report, IsPrinting);
        if (!Watermark.ShowTextOnTop)
          Watermark.DrawText(e, pageRect, Report, IsPrinting);
      }
      
      float leftMargin = (int)Math.Round(LeftMargin * Units.Millimeters * e.ScaleX);
      float topMargin = (int)Math.Round(TopMargin * Units.Millimeters * e.ScaleY);
      g.TranslateTransform(leftMargin, topMargin);
      
      try
      {
        foreach (Base c in AllObjects)
        {
          if (c is ReportComponentBase && c.HasFlag(Flags.CanDraw))
          {
            ReportComponentBase obj = c as ReportComponentBase;
            if (!IsPrinting)
            {
              if (!obj.IsVisible(e))
                continue;
            }
            else
            {
              if (!obj.Printable)
                continue;
            }
            obj.SetDesigning(false);
            obj.SetPrinting(IsPrinting);
            obj.Draw(e);
            obj.SetPrinting(false);
          }
        }
      }
      finally
      {
        g.TranslateTransform(-leftMargin, -topMargin);
      }

      if (Watermark.Enabled)
      {
        if (Watermark.ShowImageOnTop)
          Watermark.DrawImage(e, pageRect, Report, IsPrinting);
        if (Watermark.ShowTextOnTop)
          Watermark.DrawText(e, pageRect, Report, IsPrinting);
      }
    }

    internal void DrawSearchHighlight(FRPaintEventArgs e, int objIndex, CharacterRange range)
    {
      Graphics g = e.Graphics;
      float leftMargin = LeftMargin * Units.Millimeters * e.ScaleX;
      float topMargin = TopMargin * Units.Millimeters * e.ScaleY;

      ObjectCollection allObjects = AllObjects;
      if (objIndex < 0 && objIndex >= allObjects.Count)
        return;
      ISearchable obj = allObjects[objIndex] as ISearchable;
      if (obj != null)
      {
        g.TranslateTransform(leftMargin, topMargin);
        try
        {
          obj.DrawSearchHighlight(e, range);
        }
        finally
        {  
          g.TranslateTransform(-leftMargin, -topMargin);
        }
      }  
    }

    internal void Print(FRPaintEventArgs e)
    {
      try
      {
        SetPrinting(true);
        SetDesigning(false);
        Draw(e);
      }
      finally
      {
        SetPrinting(false);
      }
    }
    
    internal ReportComponentBase HitTest(PointF mouse)
    {
      mouse.X -= LeftMargin * Units.Millimeters;
      mouse.Y -= TopMargin * Units.Millimeters;

      ObjectCollection allObjects = AllObjects;
      for (int i = allObjects.Count - 1; i >= 0; i--)
      {
        ReportComponentBase c = allObjects[i] as ReportComponentBase;
        if (c != null)
        {
          if (c.PointInObject(mouse))
            return c;
        }
      }
      return null;
    }

    /// <inheritdoc/>
    public override void SetDefaults()
    {
      switch (Config.ReportSettings.DefaultPaperSize)
      {
        case DefaultPaperSize.A4:
          PaperWidth = 210;
          PaperHeight = 297;
          break;

        case DefaultPaperSize.Letter:
          PaperWidth = 215.9f;
          PaperHeight = 279.4f;
          break;
      }
      
      float baseHeight = Units.Millimeters * 10;
      if (ReportWorkspace.Grid.GridUnits == PageUnits.Inches || 
        ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch)
        baseHeight = Units.Inches * 0.4f;
      
      ReportTitle = new ReportTitleBand();
      ReportTitle.CreateUniqueName();
      ReportTitle.Height = baseHeight;
      
      PageHeader = new PageHeaderBand();
      PageHeader.CreateUniqueName();
      PageHeader.Height = baseHeight * 0.75f;
      
      DataBand data = new DataBand();
      Bands.Add(data);
      data.CreateUniqueName();
      data.Height = baseHeight * 2;
      
      PageFooter = new PageFooterBand();
      PageFooter.CreateUniqueName();
      PageFooter.Height = baseHeight * 0.5f;

      base.SetDefaults();
    }
    
    internal void InitializeComponents()
    {
      ObjectCollection allObjects = AllObjects;
      foreach (Base obj in allObjects)
      {
        if (obj is ReportComponentBase)
          (obj as ReportComponentBase).InitializeComponent();
      }
    }

    internal void FinalizeComponents()
    {
      ObjectCollection allObjects = AllObjects;
      foreach (Base obj in allObjects)
      {
        if (obj is ReportComponentBase)
          (obj as ReportComponentBase).FinalizeComponent();
      }
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();

      if (!String.IsNullOrEmpty(OutlineExpression))
        expressions.Add(OutlineExpression);

      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void ExtractMacros()
    {
      Watermark.Text = ExtractDefaultMacros(Watermark.Text);
    }

    /// <summary>
    /// This method fires the <b>StartPage</b> event and the script code connected to the <b>StartPageEvent</b>.
    /// </summary>
    public void OnStartPage(EventArgs e)
    {
      if (StartPage != null)
        StartPage(this, e);
      InvokeEvent(StartPageEvent, e);
    }

    /// <summary>
    /// This method fires the <b>FinishPage</b> event and the script code connected to the <b>FinishPageEvent</b>.
    /// </summary>
    public void OnFinishPage(EventArgs e)
    {
      if (FinishPage != null)
        FinishPage(this, e);
      InvokeEvent(FinishPageEvent, e);
    }

    /// <summary>
    /// This method fires the <b>ManualBuild</b> event and the script code connected to the <b>ManualBuildEvent</b>.
    /// </summary>
    public void OnManualBuild(EventArgs e)
    {
      if (ManualBuild != null)
        ManualBuild(this, e);
      InvokeEvent(ManualBuildEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportPage"/> class with default settings.
    /// </summary>
    public ReportPage()
    {
      FPaperWidth = 210;
      FPaperHeight = 297;
      FLeftMargin = 10;
      FTopMargin = 10;
      FRightMargin = 10;
      FBottomMargin = 10;
      FFirstPageSource = 7;
      FOtherPagesSource = 7;
      FDuplex = Duplex.Default;
      FBands = new BandCollection(this);
      FGuides = new FloatCollection();
      FColumns = new PageColumns(this);
      FBorder = new Border();
      FFill = new SolidFill(SystemColors.Window);
      FWatermark = new Watermark();
      FTitleBeforeHeader = true;
      FStartPageEvent = "";
      FFinishPageEvent = "";
      FManualBuildEvent = "";
      BaseName = "Page";
      FUnlimitedHeight = false;
      FUnlimitedWidth = false;
      FUnlimitedHeightValue = MAX_PAPER_SIZE_MM * Units.Millimeters;
      FUnlimitedWidthValue = MAX_PAPER_SIZE_MM * Units.Millimeters;
    }
  }
}