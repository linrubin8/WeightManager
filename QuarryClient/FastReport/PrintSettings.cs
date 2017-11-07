using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using FastReport.Utils;
using FastReport.TypeConverters;
using System.Drawing;

namespace FastReport
{
  /// <summary>
  /// Specifies the report printing mode.
  /// </summary>
  public enum PrintMode 
  { 
    /// <summary>
    /// Specifies the default printing mode. One report page produces 
    /// one printed paper sheet of the same size.
    /// </summary>
    Default, 
    
    /// <summary>
    /// Specifies the split mode. Big report page produces several smaller paper sheets.
    /// Use this mode to print A3 report on A4 printer.
    /// </summary>
    Split, 
    
    /// <summary>
    /// Specifies the scale mode. One or several report pages produce one bigger paper sheet.
    /// Use this mode to print A5 report on A4 printer. 
    /// </summary>
    Scale 
  }

  /// <summary>
  /// Specifies the number of report pages printed on one paper sheet.
  /// </summary>
  public enum PagesOnSheet 
  { 
    /// <summary>
    /// Specifies one report page per sheet.
    /// </summary>
    One,

    /// <summary>
    /// Specifies two report pages per sheet.
    /// </summary>
    Two,

    /// <summary>
    /// Specifies four report pages per sheet.
    /// </summary>
    Four,

    /// <summary>
    /// Specifies eight report pages per sheet.
    /// </summary>
    Eight
  }
  
  /// <summary>
  /// Specifies the pages to print.
  /// </summary>
  public enum PrintPages 
  { 
    /// <summary>
    /// Print all report pages.
    /// </summary>
    All, 
    
    /// <summary>
    /// Print odd pages only.
    /// </summary>
    Odd, 
    
    /// <summary>
    /// Print even pages only.
    /// </summary>
    Even
  }
  
  /// <summary>
  /// Specifies the page range to print.
  /// </summary>
  public enum PageRange 
  { 
    /// <summary>
    /// Print all pages.
    /// </summary>
    All, 
    
    /// <summary>
    /// Print current page.
    /// </summary>
    Current, 
    
    /// <summary>
    /// Print pages specified in the <b>PageNumbers</b> property of the <b>PrintSettings</b>.
    /// </summary>
    PageNumbers
  }

  /// <summary>
  /// This class contains the printer settings. 
  /// It is used in the <see cref="Report.PrintSettings"/> property.
  /// </summary>
  /// <remarks>
  /// Typical use of this class is to setup a printer properties without using the "Print"
  /// dialog. In this case, setup necessary properties and turn off the dialog via the 
  /// <see cref="ShowDialog"/> property.
  /// </remarks>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class PrintSettings : IDisposable
  {
    #region Fields
    private string FPrinter;
    private bool FSavePrinterWithReport;
    private bool FPrintToFile;
    private string FPrintToFileName;
    private PageRange FPageRange;
    private string FPageNumbers;
    private int FCopies;
    private bool FCollate;
    private PrintPages FPrintPages;
    private bool FReverse;
    private Duplex FDuplex;
    private int FPaperSource;
    private PrintMode FPrintMode;
    private float FPrintOnSheetWidth;
    private float FPrintOnSheetHeight;
    private int FPrintOnSheetRawPaperSize;
    private PagesOnSheet FPagesOnSheet;
    private string[] FCopyNames;
    private bool FShowDialog;
    private Graphics FMeasureGraphics;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the printer name.
    /// </summary>
    [TypeConverter(typeof(PrinterConverter))]
    public string Printer
    {
      get { return FPrinter; }
      set 
      { 
        FPrinter = value;
        DisposeMeasureGraphics();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating that the printer name should be saved in a report file.
    /// </summary>
    /// <remarks>
    /// If this property is set to <b>true</b>, the printer name will be saved in a report file.
    /// Next time when you open the report, the printer will be automatically selected.
    /// </remarks>
    [DefaultValue(false)]
    public bool SavePrinterWithReport
    {
      get { return FSavePrinterWithReport; }
      set { FSavePrinterWithReport = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the printing output should be send 
    /// to a file instead of a printer.
    /// </summary>
    /// <remarks>
    /// Also set the <see cref="PrintToFileName"/> property.
    /// </remarks>
    [DefaultValue(false)]
    public bool PrintToFile
    {
      get { return FPrintToFile; }
      set { FPrintToFile = value; }
    }

    /// <summary>
    /// The name of a file to print the report to.
    /// </summary>
    /// <remarks>
    /// This property is used if <see cref="PrintToFile"/> property is <b>true</b>.
    /// </remarks>
    public string PrintToFileName
    {
      get { return FPrintToFileName; }
      set { FPrintToFileName = value; }
    }

    /// <summary>
    /// Gets or sets a value specifies the page range to print.
    /// </summary>
    [DefaultValue(PageRange.All)]
    public PageRange PageRange
    {
      get { return FPageRange; }
      set { FPageRange = value; }
    }

    /// <summary>
    /// Gets or sets the page number(s) to print.
    /// </summary>
    /// <remarks>
    /// This property is used if <see cref="PageRange"/> property is set to <b>PageNumbers</b>.
    /// You can specify the page numbers, separated by commas, or the page ranges.
    /// For example: "1,3,5-12".
    /// </remarks>
    public string PageNumbers
    {
      get { return FPageNumbers; }
      set { FPageNumbers = value; }
    }

    /// <summary>
    /// Gets or sets the number of copies to print.
    /// </summary>
    [DefaultValue(1)]
    public int Copies
    {
      get { return FCopies; }
      set { FCopies = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the printed document should be collated.
    /// </summary>
    [DefaultValue(true)]
    public bool Collate
    {
      get { return FCollate; }
      set { FCollate = value; }
    }

    /// <summary>
    /// Gets or sets a value specifies the pages to print.
    /// </summary>
    [DefaultValue(PrintPages.All)]
    public PrintPages PrintPages
    {
      get { return FPrintPages; }
      set { FPrintPages = value; }
    }

    /// <summary>
    /// Gets or sets a value determines whether to print pages in reverse order.
    /// </summary>
    [DefaultValue(false)]
    public bool Reverse
    {
      get { return FReverse; }
      set { FReverse = value; }
    }

    /// <summary>
    /// Gets or sets the duplex mode.
    /// </summary>
    [DefaultValue(Duplex.Default)]
    public Duplex Duplex
    {
      get { return FDuplex; }
      set { FDuplex = value; }
    }

    /// <summary>
    /// Gets or sets the paper source.
    /// </summary>
    /// <remarks>
    /// This property corresponds to the RAW source number. Default value is 7 which 
    /// corresponds to DMBIN_AUTO.
    /// </remarks>
    [DefaultValue(7)]
    public int PaperSource
    {
      get { return FPaperSource; }
      set { FPaperSource = value; }
    }

    /// <summary>
    /// Gets or sets the print mode.
    /// </summary>
    /// <remarks>
    /// See the <see cref="FastReport.PrintMode"/> enumeration for details. If you use 
    /// the mode other than <b>Default</b>, you must specify the sheet size in the
    /// <see cref="PrintOnSheetWidth"/>, <see cref="PrintOnSheetHeight"/> properties.
    /// </remarks>
    [DefaultValue(PrintMode.Default)]
    public PrintMode PrintMode
    {
      get { return FPrintMode; }
      set { FPrintMode = value; }
    }

    /// <summary>
    /// Gets or sets the width of the paper sheet to print on.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="PrintMode"/> property is not <b>Default</b>.
    /// Specify the paper width in millimeters.
    /// </remarks>
    public float PrintOnSheetWidth
    {
      get { return FPrintOnSheetWidth; }
      set { FPrintOnSheetWidth = value; }
    }

    /// <summary>
    /// Gets or sets the height of the paper sheet to print on.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="PrintMode"/> property is not <b>Default</b>.
    /// Specify the paper height in millimeters.
    /// </remarks>
    public float PrintOnSheetHeight
    {
      get { return FPrintOnSheetHeight; }
      set { FPrintOnSheetHeight = value; }
    }

    /// <summary>
    /// Gets or sets the raw index of a paper size.
    /// </summary>
    [DefaultValue(0)]
    public int PrintOnSheetRawPaperSize
    {
      get { return FPrintOnSheetRawPaperSize; }
      set { FPrintOnSheetRawPaperSize = value; }
    }

    /// <summary>
    /// Gets or sets the number of pages per printed sheet.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="PrintMode"/> property is set to <b>Scale</b>.
    /// </remarks>
    [DefaultValue(PagesOnSheet.One)]
    public PagesOnSheet PagesOnSheet
    {
      get { return FPagesOnSheet; }
      set { FPagesOnSheet = value; }
    }

    /// <summary>
    /// Gets or sets an array of printed copy names, such as "Original", "Copy", etc.
    /// </summary>
    public string[] CopyNames
    {
      get { return FCopyNames; }
      set { FCopyNames = value; }
    }

    /// <summary>
    /// Specifies whether to display the "Print" dialog.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowDialog
    {
      get { return FShowDialog; }
      set { FShowDialog = value; }
    }

    internal Graphics MeasureGraphics
    {
      get
      {
        if (FMeasureGraphics == null)
        {
          PrinterSettings printer = new PrinterSettings();
          try
          {
            if (!String.IsNullOrEmpty(Printer))
              printer.PrinterName = Printer;
          }
          catch
          {
          }  
          
          try
          {
            FMeasureGraphics = printer.CreateMeasurementGraphics();
          }
          catch
          {
            FMeasureGraphics = null;
          }  
        }
        return FMeasureGraphics;
      }
    }
    #endregion
    
    #region Private Methods
    private void DisposeMeasureGraphics()
    {
      if (FMeasureGraphics != null)
        FMeasureGraphics.Dispose();
      FMeasureGraphics = null;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public void Dispose()
    {
      DisposeMeasureGraphics();
    }

    /// <summary>
    /// Assigns values from another source.
    /// </summary>
    /// <param name="source">Source to assign from.</param>
    public void Assign(PrintSettings source)
    {
      Printer = source.Printer;
      SavePrinterWithReport = source.SavePrinterWithReport;
      PrintToFile = source.PrintToFile;
      PrintToFileName = source.PrintToFileName;
      PageRange = source.PageRange;
      PageNumbers = source.PageNumbers;
      Copies = source.Copies;
      Collate = source.Collate;
      PrintPages = source.PrintPages;
      Reverse = source.Reverse;
      Duplex = source.Duplex;
      PaperSource = source.PaperSource;
      PrintMode = source.PrintMode;
      PrintOnSheetWidth = source.PrintOnSheetWidth;
      PrintOnSheetHeight = source.PrintOnSheetHeight;
      PrintOnSheetRawPaperSize = source.PrintOnSheetRawPaperSize;
      PagesOnSheet = source.PagesOnSheet;
      source.CopyNames.CopyTo(CopyNames, 0);
      ShowDialog = source.ShowDialog;
    }
    
    /// <summary>
    /// Resets all settings to its default values.
    /// </summary>
    public void Clear()
    {
      FPrinter = "";
      FSavePrinterWithReport = false;
      FPrintToFile = false;
      FPrintToFileName = "";
      FPageRange = PageRange.All;
      FPageNumbers = "";
      FCopies = 1;
      FCollate = true;
      FPrintPages = PrintPages.All;
      FReverse = false;
      FDuplex = Duplex.Default;
      FPaperSource = 7;
      FPrintMode = PrintMode.Default;
      FPrintOnSheetWidth = 210;
      FPrintOnSheetHeight = 297;
      FPrintOnSheetRawPaperSize = 0;
      FPagesOnSheet = PagesOnSheet.One;
      FCopyNames = new string[0];
      FShowDialog = true;
      DisposeMeasureGraphics();
    }

    internal void Serialize(FRWriter writer, PrintSettings c)
    {
      if (SavePrinterWithReport && Printer != c.Printer)
        writer.WriteStr("PrintSettings.Printer", Printer);
      if (SavePrinterWithReport != c.SavePrinterWithReport)
        writer.WriteBool("PrintSettings.SavePrinterWithReport", SavePrinterWithReport);
      if (PrintToFile != c.PrintToFile)
        writer.WriteBool("PrintSettings.PrintToFile", PrintToFile);
      if (PrintToFileName != c.PrintToFileName)
        writer.WriteStr("PrintSettings.PrintToFileName", PrintToFileName);
      if (PageRange != c.PageRange)
        writer.WriteValue("PrintSettings.PageRange", PageRange);
      if (PageNumbers != c.PageNumbers)
        writer.WriteStr("PrintSettings.PageNumbers", PageNumbers);
      if (Copies != c.Copies)
        writer.WriteInt("PrintSettings.Copies", Copies);
      if (Collate != c.Collate)
        writer.WriteBool("PrintSettings.Collate", Collate);
      if (PrintPages != c.PrintPages)
        writer.WriteValue("PrintSettings.PrintPages", PrintPages);
      if (Reverse != c.Reverse)
        writer.WriteBool("PrintSettings.Reverse", Reverse);
      if (Duplex != c.Duplex)
        writer.WriteValue("PrintSettings.Duplex", Duplex);
      if (PaperSource != c.PaperSource)
        writer.WriteInt("PrintSettings.PaperSource", PaperSource);
      if (PrintMode != c.PrintMode)
        writer.WriteValue("PrintSettings.PrintMode", PrintMode);
      if (PrintOnSheetWidth != c.PrintOnSheetWidth)
        writer.WriteFloat("PrintSettings.PrintOnSheetWidth", PrintOnSheetWidth);
      if (PrintOnSheetHeight != c.PrintOnSheetHeight)
        writer.WriteFloat("PrintSettings.PrintOnSheetHeight", PrintOnSheetHeight);
      if (PrintOnSheetRawPaperSize != c.PrintOnSheetRawPaperSize)
        writer.WriteInt("PrintSettings.PrintOnSheetRawPaperSize", PrintOnSheetRawPaperSize);
      if (PagesOnSheet != c.PagesOnSheet)
        writer.WriteValue("PrintSettings.PagesOnSheet", PagesOnSheet);
      if (!writer.AreEqual(CopyNames, c.CopyNames))
        writer.WriteValue("PrintSettings.CopyNames", CopyNames);
      if (ShowDialog != c.ShowDialog)
        writer.WriteBool("PrintSettings.ShowDialog", ShowDialog);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PrintSettings"/> class with default settings.
    /// </summary>
    public PrintSettings() 
    {
      Clear();
    }
  }
}