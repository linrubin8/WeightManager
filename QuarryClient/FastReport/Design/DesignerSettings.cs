using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.Common;
using FastReport.Data;
using FastReport.Preview;
using FastReport.Utils;
using FastReport.TypeConverters;
using FastReport.Design;
using FastReport.Design.ImportPlugins.RDL;
using FastReport.Design.ImportPlugins.ListAndLabel;
using FastReport.Design.ImportPlugins.DevExpress;

namespace FastReport.Design
{
  /// <summary>
  /// This class contains settings that will be applied to the report designer.
  /// </summary>
  [TypeConverter(typeof(FRExpandableObjectConverter))]
  public partial class DesignerSettings
  {
    private Icon FIcon;
    private Font FDefaultFont;
    private bool FShowInTaskbar;
    private string FText;
    private DesignerRestrictions FRestrictions;
    private List<ConnectionEntry> FCustomConnections;
    private DbConnection FApplicationConnection;
    private Type FApplicationConnectionType;
    private ToolStripRenderer FToolStripRenderer;

    /// <summary>
    /// Occurs when the designer is loaded.
    /// </summary>
    /// <remarks>
    /// Use this event if you want to customize some aspects of the designer, for example,
    /// to hide some menu items.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to hide the "File|Select Language..." menu item.
    /// <code>
    /// Config.DesignerSettings.DesignerLoaded += new EventHandler(DesignerSettings_DesignerLoaded);
    /// 
    /// void DesignerSettings_DesignerLoaded(object sender, EventArgs e)
    /// {
    ///   (sender as DesignerControl).MainMenu.miFileSelectLanguage.Visible = false;
    /// }
    /// </code>
    /// </example>
    public event EventHandler DesignerLoaded;

    /// <summary>
    /// Occurs when the designer is closed.
    /// </summary>
    public event EventHandler DesignerClosed;

    /// <summary>
    /// Occurs when the report is loaded.
    /// </summary>
    public event ReportLoadedEventHandler ReportLoaded;

    /// <summary>
    /// Occurs when a report page or a dialog form is added to the report.
    /// </summary>
    /// <remarks>
    /// Use this event if you want to customize the page properties.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to change the default page margins.
    /// <code>
    /// Config.DesignerSettings.PageAdded += new EventHandler(DesignerSettings_PageAdded);
    /// 
    /// void DesignerSettings_PageAdded(object sender, EventArgs e)
    /// {
    ///   if (sender is ReportPage)
    ///     (sender as ReportPage).TopMargin = 0;
    /// }
    /// </code>
    /// </example>
    public event EventHandler PageAdded;

    /// <summary>
    /// Occurs when object is inserted.
    /// </summary>
    public event ObjectInsertedEventHandler ObjectInserted;

    /// <include file='../Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomOpenDialog/*'/>
    /// <include file='../Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    public event OpenSaveDialogEventHandler CustomOpenDialog;

    /// <include file='../Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomSaveDialog/*'/>
    /// <include file='../Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    public event OpenSaveDialogEventHandler CustomSaveDialog;

    /// <include file='../Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomOpenReport/*'/>
    /// <include file='../Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    public event OpenSaveReportEventHandler CustomOpenReport;

    /// <include file='../Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomSaveReport/*'/>
    /// <include file='../Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    public event OpenSaveReportEventHandler CustomSaveReport;

    /// <summary>
    /// Occurs when previewing a report from the designer.
    /// </summary>
    /// <remarks>
    /// Use this event to show own preview window.
    /// </remarks>
    /// <example>
    /// <code>
    /// Config.DesignerSettings.CustomPreviewReport += new EventHandler(MyPreviewHandler);
    /// 
    /// private void MyPreviewHandler(object sender, EventArgs e)
    /// {
    ///   Report report = sender as Report;
    ///   using (MyPreviewForm form = new MyPreviewForm())
    ///   {
    ///     report.Preview = form.previewControl1;
    ///     report.ShowPreparedReport();
    ///     form.ShowDialog();
    ///   }
    /// }
    /// </code>
    /// </example>
    public event EventHandler CustomPreviewReport;

    /// <summary>
    /// Occurs when getting available table names from the connection.
    /// </summary>
    /// <remarks>
    /// Use this handler to filter the list of tables returned by the connection object.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to hide the table with "Table 1" name from the Data Wizard.
    /// <code>
    /// Config.DesignerSettings.FilterConnectionTables += DesignerSettings_FilterConnectionTables;
    /// 
    /// private void DesignerSettings_FilterConnectionTables(object sender, FilterConnectionTablesEventArgs e)
    /// {
    ///   if (e.TableName == "Table 1")
    ///     e.Skip = true;
    /// }
    /// </code>
    /// </example>
    public event FilterConnectionTablesEventHandler FilterConnectionTables;

    /// <summary>
    /// Occurs when the query builder is called.
    /// </summary>
    /// <remarks>
    /// Subscribe to this event if you want to replace the embedded query builder with your own one.
    /// </remarks>
    public event CustomQueryBuilderEventHandler CustomQueryBuilder;
    
    /// <summary>
    /// Gets or sets the icon for the designer window.
    /// </summary>
    public Icon Icon
    {
      get { return FIcon; }
      set { FIcon = value; }
    }
    
    /// <summary>
    /// Gets or sets the default font used in a report.
    /// </summary>
    public Font DefaultFont
    {
      get { return FDefaultFont; }
      set { FDefaultFont = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the designer window is displayed in the Windows taskbar. 
    /// </summary>
    [DefaultValue(false)]
    public bool ShowInTaskbar
    {
      get { return FShowInTaskbar; }
      set { FShowInTaskbar = value; }
    }

    /// <summary>
    /// Gets the designer restrictions flags.
    /// </summary>
    public DesignerRestrictions Restrictions
    {
      get { return FRestrictions; }
      set { FRestrictions = value; }
    }

    /// <summary>
    /// Gets or sets the title text for the designer window.
    /// </summary>
    /// <remarks>
    /// If no text is set, the default text "FastReport -" will be used.
    /// </remarks>
    public string Text
    {
      get { return FText; }
      set { FText = value; }
    }

    /// <summary>
    /// Gets or sets application-defined DbConnection object that will be used in the designer 
    /// to create a new datasource.
    /// </summary>
    /// <remarks>
    /// The application connection object is used in the "Data Wizard" to create new datasources. 
    /// In this mode, you can't create any other connections in the wizard; only application
    /// connection is available. You still able to choose tables or create a new queries inside 
    /// this connection. The connection information (ConnectionString) is not stored in the report file.
    /// </remarks>
    public DbConnection ApplicationConnection
    {
      get { return FApplicationConnection; }
      set 
      { 
        // be sure that add-ins were initialized
        Report dummyReport = new Report();
        dummyReport.Dispose();

        FApplicationConnection = value;
        FindConnectorType();
      }
    }

    /// <summary>
    /// Gets the toolstrip renderer.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripRenderer ToolStripRenderer
    {
      get 
      { 
        if (FToolStripRenderer == null)
        {
          ProfessionalColorTable vs2005ColorTable = new ProfessionalColorTable();
          vs2005ColorTable.UseSystemColors = true;
          FToolStripRenderer = new ToolStripProfessionalRenderer(vs2005ColorTable);
        }
        return FToolStripRenderer; 
      }
    }

    internal Type ApplicationConnectionType
    {
      get { return FApplicationConnectionType; }
    }
    
    internal List<ConnectionEntry> CustomConnections
    {
      get { return FCustomConnections; }
    }

    private void FindConnectorType()
    {
      FApplicationConnectionType = null;
      if (FApplicationConnection == null)
        return;
      
      // find appropriate connector
      List<ObjectInfo> registeredObjects = new List<ObjectInfo>();
      RegisteredObjects.Objects.EnumItems(registeredObjects);

      foreach (ObjectInfo info in registeredObjects)
      {
        if (info.Object != null && info.Object.IsSubclassOf(typeof(DataConnectionBase)))
        {
          // MsAccessDataConnection is the subclass of OleDBDataConnection, skip it
          if (info.Object.Name == "MsAccessDataConnection")
            continue;
          using (DataConnectionBase conn = Activator.CreateInstance(info.Object) as DataConnectionBase)
          {
            if (conn.GetConnectionType() == FApplicationConnection.GetType())
            {
              FApplicationConnectionType = conn.GetType();
              return;
            }
          }
        }
      }

      throw new Exception(FApplicationConnection.GetType().Name + " connection is not supported.");
    }

    internal void OnDesignerLoaded(object sender, EventArgs e)
    {
      if (DesignerLoaded != null)
        DesignerLoaded(sender, e);
    }

    internal void OnDesignerClosed(object sender, EventArgs e)
    {
        if (DesignerClosed != null)
            DesignerClosed(sender, e);
    }

    internal void OnReportLoaded(object sender, ReportLoadedEventArgs e)
    {
      if (ReportLoaded != null)
        ReportLoaded(sender, e);
    }

    internal void OnPageAdded(object sender, EventArgs e)
    {
      if (PageAdded != null)
        PageAdded(sender, e);
    }

    internal void OnObjectInserted(object sender, ObjectInsertedEventArgs e)
    {
      if (ObjectInserted != null)
        ObjectInserted(sender, e);
    }

    internal void OnCustomOpenDialog(object sender, OpenSaveDialogEventArgs e)
    {
      if (CustomOpenDialog != null)
        CustomOpenDialog(sender, e);
      else
      {
        // standard open dialog
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
          List<ImportPlugin> importPlugins = new List<ImportPlugin>();
          string filter = Res.Get("FileFilters,Report");

          foreach (IDesignerPlugin plugin in e.Designer.Plugins)
          {
            if (plugin is ImportPlugin)
            {
              importPlugins.Add(plugin as ImportPlugin);
              filter += "|" + (plugin as ImportPlugin).Filter;
            }
          }

          dialog.Filter = filter;

          e.Cancel = dialog.ShowDialog() != DialogResult.OK;
          e.FileName = dialog.FileName;
          if (dialog.FilterIndex != 1)
            e.Data = importPlugins[dialog.FilterIndex - 2];
        }
      }
    }

    internal void OnCustomSaveDialog(object sender, OpenSaveDialogEventArgs e)
    {
      if (CustomSaveDialog != null)
        CustomSaveDialog(sender, e);
      else
      {
        // standard save dialog
        using (SaveFileDialog dialog = new SaveFileDialog())
        {
          string filter = Res.Get("FileFilters,Report");
          if (e.Designer.ActiveReport.ScriptLanguage == Language.CSharp)
            filter += "|" + Res.Get("FileFilters,CsFile");
          else  
            filter += "|" + Res.Get("FileFilters,VbFile");

          List<ExportPlugin> exportPlugins = new List<ExportPlugin>();
          foreach (IDesignerPlugin plugin in e.Designer.Plugins)
          {
              if (plugin is ExportPlugin)
              {
                  exportPlugins.Add(plugin as ExportPlugin);
                  filter += "|" + (plugin as ExportPlugin).Filter;
              }
          }
          
          dialog.Filter = filter;
          dialog.FileName = e.FileName;
          dialog.DefaultExt = "frx";

          e.Cancel = dialog.ShowDialog() != DialogResult.OK;
          // SaveFileDialog bug workaround
          string[] filters = dialog.Filter.Split(new char[] { '|' });
          string ext = Path.GetExtension(filters[(dialog.FilterIndex - 1) * 2 + 1]);
          e.FileName = Path.ChangeExtension(dialog.FileName, ext);
          
          if (dialog.FilterIndex != 1)
          {
              if (dialog.FilterIndex > 2)
              {
                  e.Data = exportPlugins[dialog.FilterIndex - 3];
              }
              else
              {
                  e.Data = dialog.FilterIndex;
              }
              e.IsPlugin = true;
          }
        }
      }
    }

    internal void OnCustomOpenReport(object sender, OpenSaveReportEventArgs e)
    {
      if (CustomOpenReport != null)
        CustomOpenReport(sender, e);
      else
      {
        // standard open report
        if (e.Data == null)
        {
          string ext = Path.GetExtension(e.FileName).ToLower();
          if (ext == ".rdl" || ext == ".rdlc")
          {
            new RDLImportPlugin().LoadReport(e.Report, e.FileName);
          }
          else if (ext == ".rpt")
          {
            new ListAndLabelImportPlugin().LoadReport(e.Report, e.FileName);
          }
          else if (ext == ".repx")
          {
            new DevExpressImportPlugin().LoadReport(e.Report, e.FileName);
          }
          else
            e.Report.Load(e.FileName);
        }
        else
          (e.Data as ImportPlugin).LoadReport(e.Report, e.FileName);
      }
      OnReportLoaded(sender, new ReportLoadedEventArgs(e.Report));
    }

    internal void OnCustomSaveReport(object sender, OpenSaveReportEventArgs e)
    {
      if (CustomSaveReport != null)
        CustomSaveReport(sender, e);
      else
      {
          // standard save report
          if (e.Data == null)
              e.Report.Save(e.FileName);
          else
          {
              if (e.Data is ExportPlugin)
              {
                  (e.Data as ExportPlugin).SaveReport(e.Report, e.FileName);
              }
              else
              {
                  e.Report.GenerateReportAssembly(e.FileName);
              }
          }
      }
    }

    internal void OnCustomPreviewReport(object sender, EventArgs e)
    {
      if (CustomPreviewReport != null)
        CustomPreviewReport(sender, e);
      else
      {
        Report report = sender as Report;
        PreviewControl savePreview = report.Preview;
        report.Preview = null;

        try
        {
          report.ShowPrepared(true, report.Designer.FindForm());
        }
        finally
        {
          report.Preview = savePreview;
        }
      }
    }

    internal void OnFilterConnectionTables(object sender, FilterConnectionTablesEventArgs e)
    {
      if (FilterConnectionTables != null)
        FilterConnectionTables(sender, e);
    }

    internal void OnCustomQueryBuilder(object sender, CustomQueryBuilderEventArgs e)
    {
      if (CustomQueryBuilder != null)
        CustomQueryBuilder(sender, e);
      else
      {
        FastReport.FastQueryBuilder.QueryBuilder qb = new FastReport.FastQueryBuilder.QueryBuilder(e.Connection);
        qb.UseJoin = true;
        if (qb.DesignQuery() == DialogResult.OK)
          e.SQL = qb.GetSql();
      }
    }

    /// <summary>
    /// Adds a custom connection used in the "Data Wizard" window.
    /// </summary>
    /// <remarks>
    /// Use this method to provide own connection strings for the "Data Wizard" dialog. To do this, you need 
    /// to pass the type of connection object and connection string associated with it. You must use one of the
    /// connection objects registered in FastReport that inherit from the 
    /// <see cref="FastReport.Data.DataConnectionBase"/> class.
    /// <para/>To clear the custom connections, use the <see cref="ClearCustomConnections"/> method.
    /// </remarks>
    /// <example>
    /// This example shows how to add own connection string.
    /// <code>
    /// Config.DesignerSettings.AddCustomConnection(typeof(MsAccessDataConnection), @"Data Source=c:\data.mdb");
    /// </code>
    /// </example>
    public void AddCustomConnection(Type connectionType, string connectionString)
    {
      if (!connectionType.IsSubclassOf(typeof(DataConnectionBase)))
        throw new Exception("The 'connectionType' parameter should be of the 'DataConnectionBase' type.");
      
      FCustomConnections.Add(new ConnectionEntry(connectionType, connectionString));
    }
    
    /// <summary>
    /// Clears the custom connections added by the <b>AddCustomConnection</b> method.
    /// </summary>
    public void ClearCustomConnections()
    {
      FCustomConnections.Clear();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DesignerSettings"/> class.
    /// </summary>
    public DesignerSettings()
    {
      FIcon = ResourceLoader.GetIcon("icon16.ico");
      FDefaultFont = DrawUtils.DefaultReportFont;
      FRestrictions = new DesignerRestrictions();
      FText = "";
      FCustomConnections = new List<ConnectionEntry>();
    }
  }


  internal class ConnectionEntry
  {
    public Type Type;
    public string ConnectionString;
    
    public ConnectionEntry(Type type, string connectionString)
    {
      Type = type;
      ConnectionString = connectionString;
    }
  }
}
