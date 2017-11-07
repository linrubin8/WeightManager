using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data.Common;
using FastReport.TypeConverters;
using FastReport.Forms;
using FastReport.Data;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Specifies the default paper size used when creating a new report.
  /// </summary>
  public enum DefaultPaperSize
  {
    /// <summary>
    /// A4 paper (210 x 297 mm).
    /// </summary>
    A4,
    
    /// <summary>
    /// Letter paper (8.5 x 11 inches, 216 x 279 mm).
    /// </summary>
    Letter
  }
  
  /// <summary>
  /// This class contains settings that will be applied to the Report component.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public class ReportSettings
  {
    private Language FDefaultLanguage;
    private DefaultPaperSize FDefaultPaperSize;
    private ProgressForm FProgress;
    private bool FShowProgress;
    private bool FShowPerformance;
    private bool FUsePropValuesToDiscoverBO;
    private string FImageLocationRoot;

    /// <summary>
    /// Occurs before displaying a progress window.
    /// </summary>
    public event EventHandler StartProgress;

    /// <summary>
    /// Occurs after closing a progress window.
    /// </summary>
    public event EventHandler FinishProgress;

    /// <summary>
    /// Occurs after printing a report.
    /// </summary>
    public event EventHandler ReportPrinted;

    /// <summary>
    /// Occurs when progress state is changed.
    /// </summary>
    public event ProgressEventHandler Progress;

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/DatabaseLogin/*'/>
    public event DatabaseLoginEventHandler DatabaseLogin;

    /// <summary>
    /// Occurs after the database connection is established.
    /// </summary>
    public event AfterDatabaseLoginEventHandler AfterDatabaseLogin;

    /// <summary>
    /// Occurs when discovering the business object's structure.
    /// </summary>
    public event FilterPropertiesEventHandler FilterBusinessObjectProperties;

    /// <summary>
    /// Occurs when determining the kind of business object's property.
    /// </summary>
    public event GetPropertyKindEventHandler GetBusinessObjectPropertyKind;

    /// <summary>
    /// Occurs when discovering the structure of business object of ICustomTypeDescriptor type 
    /// with no instance specified.
    /// </summary>
    /// <remarks>
    /// The event handler must return an instance of that type.
    /// </remarks>
    public event GetTypeInstanceEventHandler GetBusinessObjectTypeInstance;

    /// <summary>
    /// Gets or sets the default script language.
    /// </summary>
    [DefaultValue(Language.CSharp)]
    public Language DefaultLanguage
    {
      get { return FDefaultLanguage; }
      set { FDefaultLanguage = value; }
    }

    /// <summary>
    /// Gets or sets the default paper size used when creating a new report.
    /// </summary>
    [DefaultValue(DefaultPaperSize.A4)]
    public DefaultPaperSize DefaultPaperSize
    {
      get { return FDefaultPaperSize; }
      set { FDefaultPaperSize = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to show the progress window
    /// when perform time-consuming operations such as run, print, export.
    /// </summary>
    [DefaultValue(true)]
    public bool ShowProgress
    {
      get { return FShowProgress; }
      set { FShowProgress = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to show the information about
    /// the report performance (report generation time, memory consumed) in the 
    /// lower right corner of the preview window.
    /// </summary>
    [DefaultValue(false)]
    public bool ShowPerformance
    {
      get { return FShowPerformance; }
      set { FShowPerformance = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the business object engine will use property values 
    /// when possible to discover the BO structure.
    /// </summary>
    [DefaultValue(true)]
    public bool UsePropValuesToDiscoverBO
    {
      get { return FUsePropValuesToDiscoverBO; }
      set { FUsePropValuesToDiscoverBO = value; }
    }

    /// <summary>
    /// Gets or sets the default path for root of PictureObject.ImageLocation path.
    /// </summary>
    [DefaultValue("")]
    public string ImageLocationRoot
    {
      get { return FImageLocationRoot; }
      set { FImageLocationRoot = value; }
    }

    internal void OnStartProgress(Report report)
    {
      FProgress = null;
      report.SetAborted(false);

      if (ShowProgress)
      {
        if (StartProgress != null)
          StartProgress(report, EventArgs.Empty);
        else 
        {
          FProgress = new ProgressForm(report);
          FProgress.ShowProgressMessage(Res.Get("Messages,PreparingData"));
          FProgress.Show();
          FProgress.Refresh();
        }
      }
    }

    internal void OnFinishProgress(Report report)
    {
      if (ShowProgress)
      {
        if (FinishProgress != null)
          FinishProgress(report, EventArgs.Empty);
        else if (FProgress != null)
        {
          FProgress.Close();
          FProgress.Dispose();
          FProgress = null;
        }
      }  
    }

    internal void OnProgress(Report report, string message)
    {
      OnProgress(report, message, 0, 0);
    }

    internal void OnProgress(Report report, string message, int pageNumber, int totalPages)
    {
      if (ShowProgress)
      {
        if (Progress != null)
          Progress(report, new ProgressEventArgs(message, pageNumber, totalPages));
        else if (FProgress != null)
          FProgress.ShowProgressMessage(message);
      }    
    }
    
    internal void OnReportPrinted(object sender)
    {
      if (ReportPrinted != null)
        ReportPrinted(sender, EventArgs.Empty);
    }

    internal void OnDatabaseLogin(DataConnectionBase sender, DatabaseLoginEventArgs e)
    {
      if (Config.DesignerSettings.ApplicationConnection != null && 
        sender.GetType() == Config.DesignerSettings.ApplicationConnectionType)
      {
        e.ConnectionString = Config.DesignerSettings.ApplicationConnection.ConnectionString;
      }
      
      if (DatabaseLogin != null)
        DatabaseLogin(sender, e);
    }

    internal void OnAfterDatabaseLogin(DataConnectionBase sender, AfterDatabaseLoginEventArgs e)
    {
      if (AfterDatabaseLogin != null)
        AfterDatabaseLogin(sender, e);
    }

    internal void OnFilterBusinessObjectProperties(object sender, FilterPropertiesEventArgs e)
    {
      if (FilterBusinessObjectProperties != null)
        FilterBusinessObjectProperties(sender, e);
    }

    internal void OnGetBusinessObjectPropertyKind(object sender, GetPropertyKindEventArgs e)
    {
      if (GetBusinessObjectPropertyKind != null)
        GetBusinessObjectPropertyKind(sender, e);
    }

    internal void OnGetBusinessObjectTypeInstance(object sender, GetTypeInstanceEventArgs e)
    {
      if (GetBusinessObjectTypeInstance != null)
        GetBusinessObjectTypeInstance(sender, e);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportSettings"/> class.
    /// </summary>
    public ReportSettings()
    {
      FShowProgress = true;
      FUsePropValuesToDiscoverBO = true;
    }
  }
}
