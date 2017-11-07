using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.Design.PageDesigners.Page;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Represents a subreport object.
  /// </summary>
  /// <remarks>
  /// To create a subreport in code, you should create the report page first and 
  /// connect it to the subreport using the <see cref="ReportPage"/> property.
  /// </remarks>
  /// <example>The following example shows how to create a subreport object in code.
  /// <code>
  /// // create the main report page
  /// ReportPage reportPage = new ReportPage();
  /// reportPage.Name = "Page1";
  /// report.Pages.Add(reportPage);
  /// // create report title band
  /// reportPage.ReportTitle = new ReportTitleBand();
  /// reportPage.ReportTitle.Name = "ReportTitle1";
  /// reportPage.ReportTitle.Height = Units.Millimeters * 10;
  /// // add subreport on it
  /// SubreportObject subreport = new SubreportObject();
  /// subreport.Name = "Subreport1";
  /// subreport.Bounds = new RectangleF(0, 0, Units.Millimeters * 25, Units.Millimeters * 5);
  /// reportPage.ReportTitle.Objects.Add(subreport);
  /// // create subreport page
  /// ReportPage subreportPage = new ReportPage();
  /// subreportPage.Name = "SubreportPage1";
  /// report.Pages.Add(subreportPage);
  /// // connect the subreport to the subreport page
  /// subreport.ReportPage = subreportPage;
  /// </code>
  /// </example>
  public class SubreportObject : ReportComponentBase
  {
    private ReportPage FReportPage;
    private bool FPrintOnParent;

    #region Properties
    /// <summary>
    /// Gets or sets a report page that contains the subreport bands and objects.
    /// </summary>
    //[Browsable(false)]
    [Editor(typeof(SubreportPageEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ComponentRefConverter))]
    public ReportPage ReportPage
    {
      get { return FReportPage; }
      set 
      { 
        if (FReportPage != null)
          FReportPage.Subreport = null;
        if (value != null)
        {
          value.Subreport = this;
          value.PageName = Name;
        }  
        FReportPage = value;
      }
    }
    
    /// <summary>
    /// Gets or sets a value indicating that subreport must print its objects on a parent band to which it belongs.
    /// </summary>
    /// <remarks>
    /// Default behavior of the subreport is to print subreport objects they own separate bands.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PrintOnParent
    {
      get { return FPrintOnParent; }
      set { FPrintOnParent = value; }
    }
    
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool Printable
    {
      get { return base.Printable; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool Exportable
    {
      get { return base.Exportable; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override Border Border
    {
      get { return base.Border; }
      set { base.Border = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override FillBase Fill
    {
      get { return base.Fill; }
      set { base.Fill = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Cursor Cursor
    {
      get { return base.Cursor; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Hyperlink Hyperlink
    {
      get { return base.Hyperlink; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanGrow
    {
      get { return base.CanGrow; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanShrink
    {
      get { return base.CanShrink; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string Style
    {
      get { return base.Style; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string BeforePrintEvent
    {
      get { return base.BeforePrintEvent; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string AfterPrintEvent
    {
      get { return base.AfterPrintEvent; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string ClickEvent
    {
      get { return base.ClickEvent; }
    }
    #endregion
    
    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      SubreportObject src = source as SubreportObject;
      PrintOnParent = src.PrintOnParent;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);

      Graphics g = e.Graphics;
      RectangleF textRect = new RectangleF((AbsLeft + 20) * e.ScaleX, (AbsTop + 2) * e.ScaleY,
        (Width - 20) * e.ScaleX, (Height - 2) * e.ScaleY);
      g.DrawImage(Res.GetImage(104), (int)(AbsLeft * e.ScaleX) + 2, (int)(AbsTop * e.ScaleY) + 2);
      g.DrawString(Name, DrawUtils.DefaultFont, Brushes.Black, textRect);
      DrawMarkers(e);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      SubreportObject c = writer.DiffObject as SubreportObject;
      base.Serialize(writer);
      
      writer.WriteRef("ReportPage", ReportPage);
      if (PrintOnParent != c.PrintOnParent)
        writer.WriteBool("PrintOnParent", PrintOnParent);
    }

    /// <inheritdoc/>
    public override void SetName(string value)
    {
      base.SetName(value);
      if (IsDesigning && ReportPage != null)
      {
        ReportPage.PageName = Name;
        Report.Designer.InitPages(Report.Pages.IndexOf(Page) + 1);
      }
    }

    /// <inheritdoc/>
    public override void OnAfterInsert(InsertFrom source)
    {
      ReportPage = new ReportPage();
      Report.Pages.Add(ReportPage);
      ReportPage.SetDefaults();
      ReportPage.ReportTitle.Dispose();
      ReportPage.PageHeader.Dispose();
      ReportPage.PageFooter.Dispose();
      ReportPage.CreateUniqueName();
      FReportPage.PageName = Name;
      Report.Designer.InitPages(Report.Pages.Count);
    }

    /// <inheritdoc/>
    public override void Delete()
    {
      if (ReportPage != null)
      {
        ReportPage.Dispose();
        Report.Designer.InitPages(Report.Pages.IndexOf(Page) + 1);
      }  
      ReportPage = null;
      base.Delete();
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new SubreportObjectMenu(Report.Designer);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="SubreportObject"/> class with default settings.
    /// </summary>
    public SubreportObject()
    {
      Fill = new SolidFill(SystemColors.Control);
      FlagUseBorder = false;
      FlagUseFill = false;
      FlagPreviewVisible = false;
      SetFlags(Flags.CanCopy, false);
    }
  }
}
