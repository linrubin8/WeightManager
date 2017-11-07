using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Export.Email;

namespace FastReport
{
  /// <summary>
  /// This class contains some global settings that used in the FastReport.Net.
  /// </summary>
  /// <remarks>
  /// This component is intended for use in the Visual Studio IDE to quickly configure
  /// FastReport global settings. To use it, drop the component on your Form and set up
  /// its properties and events.
  /// <para/>Here are some common actions that can be performed with this object:
  /// <list type="bullet">
  ///   <item>
  ///     <description>To define own open/save dialogs that will be used in the report designer,
  ///       use the <see cref="CustomOpenDialog"/>, <see cref="CustomSaveDialog"/>, 
  ///       <see cref="CustomOpenReport"/>, <see cref="CustomSaveReport"/> events;
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>To pass connection string to the connection object defined in a report,
  ///       or to define own database login dialog, use the <see cref="DatabaseLogin"/> event;
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>To adjust the connection object after it is opened,
  ///       use the <see cref="AfterDatabaseLogin"/> event;
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>To define own progress window, use the <see cref="StartProgress"/>,
  ///       <see cref="FinishProgress"/> and <see cref="Progress"/> events;
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>To setup some common properties of the report, designer and preview,
  ///       use properties defined in this class;
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>To set UI style of the designer and preview window,
  ///       use <see cref="UIStyle"/> property.
  ///     </description>
  ///   </item>
  /// </list>
  /// <para/>This component actually uses the <see cref="Config"/> static class which 
  /// contains <see cref="Config.ReportSettings"/>, <see cref="Config.DesignerSettings"/> and
  /// <see cref="Config.PreviewSettings"/> properties. You can use <b>Config</b> class as well.
  /// </remarks>
  [ToolboxItem(true), ToolboxBitmap(typeof(Report), "Resources.EnvironmentSettings.bmp")]
  public class EnvironmentSettings : Component
  {
    /// <summary>
    /// Gets or sets the UI style of the designer and preview windows.
    /// </summary>
    /// <remarks>
    /// This property affects both designer and preview windows.
    /// </remarks>
    [SRCategory("UI")]
    [Description("UI style of the designer and preview window.")]
    public UIStyle UIStyle
    {
      get { return Config.UIStyle; }
      set { Config.UIStyle = value; }
    }

    /// <summary>
    /// Indicates whether the Ribbon-style window should be used.
    /// </summary>
    [SRCategory("UI")]
    [DefaultValue(true)]
    [Description("Indicates whether the Ribbon-style window should be used.")]
    public bool UseRibbonUI
    {
      get { return Config.UseRibbon; }
      set { Config.UseRibbon = value; }
    }

    #region Report
    /// <summary>
    /// Occurs before displaying a progress window.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs before displaying a progress window.")]
    public event EventHandler StartProgress
    {
      add { Config.ReportSettings.StartProgress += value; }
      remove { Config.ReportSettings.StartProgress -= value; }
    }

    /// <summary>
    /// Occurs after closing a progress window.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs after closing a progress window.")]
    public event EventHandler FinishProgress
    {
      add { Config.ReportSettings.FinishProgress += value; }
      remove { Config.ReportSettings.FinishProgress -= value; }
    }

    /// <summary>
    /// Occurs when progress state is changed.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs when progress state is changed.")]
    public event ProgressEventHandler Progress
    {
      add { Config.ReportSettings.Progress += value; }
      remove { Config.ReportSettings.Progress -= value; }
    }

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/DatabaseLogin/*'/>
    [SRCategory("Report")]
    [Description("Occurs when database connection is about to open.")]
    public event DatabaseLoginEventHandler DatabaseLogin
    {
      add { Config.ReportSettings.DatabaseLogin += value; }
      remove { Config.ReportSettings.DatabaseLogin -= value; }
    }

    /// <summary>
    /// Occurs after the database connection is established.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs after the database connection is established.")]
    public event AfterDatabaseLoginEventHandler AfterDatabaseLogin
    {
      add { Config.ReportSettings.AfterDatabaseLogin += value; }
      remove { Config.ReportSettings.AfterDatabaseLogin -= value; }
    }

    /// <summary>
    /// Occurs when discovering the business object's structure.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs when discovering the business object's structure.")]
    public event FilterPropertiesEventHandler FilterBusinessObjectProperties
    {
      add { Config.ReportSettings.FilterBusinessObjectProperties += value; }
      remove { Config.ReportSettings.FilterBusinessObjectProperties -= value; }
    }

    /// <summary>
    /// Occurs when determining the kind of business object's property.
    /// </summary>
    [SRCategory("Report")]
    [Description("Occurs when determining the kind of business object's property.")]
    public event GetPropertyKindEventHandler GetBusinessObjectPropertyKind
    {
      add { Config.ReportSettings.GetBusinessObjectPropertyKind += value; }
      remove { Config.ReportSettings.GetBusinessObjectPropertyKind -= value; }
    }

    /// <summary>
    /// Gets or sets the report settings.
    /// </summary>
    [Description("The report settings.")]
    public ReportSettings ReportSettings
    {
      get { return Config.ReportSettings; }
      set { Config.ReportSettings = value; }
    }
    #endregion

    #region Designer
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
    /// environmentSettings1.DesignerLoaded += new EventHandler(DesignerSettings_DesignerLoaded);
    /// 
    /// void DesignerSettings_DesignerLoaded(object sender, EventArgs e)
    /// {
    ///   (sender as DesignerControl).MainMenu.miFileSelectLanguage.Visible = false;
    /// }
    /// </code>
    /// </example>
    [Description("Occurs when the designer is loaded.")]
    public event EventHandler DesignerLoaded
    {
      add { Config.DesignerSettings.DesignerLoaded += value; }
      remove { Config.DesignerSettings.DesignerLoaded -= value; }
    }

    /// <summary>
    /// Occurs when report is loaded in the designer.
    /// </summary>
    /// <remarks>
    /// Use this event handler to register application data in a report.
    /// </remarks>
    [SRCategory("Designer")]
    [Description("Occurs when report is loaded in the designer.")]
    public event ReportLoadedEventHandler ReportLoaded
    {
      add { Config.DesignerSettings.ReportLoaded += value; }
      remove { Config.DesignerSettings.ReportLoaded -= value; }
    }

    /// <summary>
    /// Occurs when object is inserted in the designer.
    /// </summary>
    /// <remarks>
    /// Use this event handler to set some object's properties when it is inserted.
    /// </remarks>
    [SRCategory("Designer")]
    [Description("Occurs when object is inserted in the designer.")]
    public event ObjectInsertedEventHandler ObjectInserted
    {
      add { Config.DesignerSettings.ObjectInserted += value; }
      remove { Config.DesignerSettings.ObjectInserted -= value; }
    }

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomOpenDialog/*'/>
    /// <include file='Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    [SRCategory("Designer")]
    [Description("Occurs when the report designer is about to show the 'Open' dialog.")]
    public event OpenSaveDialogEventHandler CustomOpenDialog
    {
      add { Config.DesignerSettings.CustomOpenDialog += value; }
      remove { Config.DesignerSettings.CustomOpenDialog -= value; }
    }

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomSaveDialog/*'/>
    /// <include file='Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    [SRCategory("Designer")]
    [Description("Occurs when the report designer is about to show the 'Save' dialog.")]
    public event OpenSaveDialogEventHandler CustomSaveDialog
    {
      add { Config.DesignerSettings.CustomSaveDialog += value; }
      remove { Config.DesignerSettings.CustomSaveDialog -= value; }
    }

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomOpenReport/*'/>
    /// <include file='Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    [SRCategory("Designer")]
    [Description("Occurs when the report designer is about to load the report.")]
    public event OpenSaveReportEventHandler CustomOpenReport
    {
      add { Config.DesignerSettings.CustomOpenReport += value; }
      remove { Config.DesignerSettings.CustomOpenReport -= value; }
    }

    /// <include file='Resources/doc.xml' path='//CodeDoc/Topics/EnvironmentSettings/CustomSaveReport/*'/>
    /// <include file='Resources/doc.xml' path='//CodeDoc/Examples/EnvironmentSettings/*'/>
    [SRCategory("Designer")]
    [Description("Occurs when the report designer is about to save the report.")]
    public event OpenSaveReportEventHandler CustomSaveReport
    {
      add { Config.DesignerSettings.CustomSaveReport += value; }
      remove { Config.DesignerSettings.CustomSaveReport -= value; }
    }

    /// <summary>
    /// Occurs when previewing a report from the designer.
    /// </summary>
    /// <remarks>
    /// Use this event to show own preview window.
    /// </remarks>
    /// <example>
    /// <code>
    /// environmentSettings1.CustomPreviewReport += new EventHandler(MyPreviewHandler);
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
    [SRCategory("Designer")]
    [Description("Occurs when previewing a report from the designer.")]
    public event EventHandler CustomPreviewReport
    {
      add { Config.DesignerSettings.CustomPreviewReport += value; }
      remove { Config.DesignerSettings.CustomPreviewReport -= value; }
    }

    /// <summary>
    /// Occurs when getting available table names from the connection.
    /// </summary>
    /// <remarks>
    /// Use this handler to filter the list of tables returned by the connection object.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to hide the table with "Table 1" name from the Data Wizard.
    /// <code>
    /// environmentSettings1.FilterConnectionTables += DesignerSettings_FilterConnectionTables;
    /// 
    /// private void DesignerSettings_FilterConnectionTables(object sender, FilterConnectionTablesEventArgs e)
    /// {
    ///   if (e.TableName == "Table 1")
    ///     e.Skip = true;
    /// }
    /// </code>
    /// </example>
    [SRCategory("Designer")]
    [Description("Occurs when getting available table names from the connection.")]
    public event FilterConnectionTablesEventHandler FilterConnectionTables
    {
      add { Config.DesignerSettings.FilterConnectionTables += value; }
      remove { Config.DesignerSettings.FilterConnectionTables -= value; }
    }

    /// <summary>
    /// Gets or sets the designer settings.
    /// </summary>
    [Description("The designer settings.")]
    public DesignerSettings DesignerSettings
    {
      get { return Config.DesignerSettings; }
      set { Config.DesignerSettings = value; }
    }
    #endregion

    #region Preview
    /// <summary>
    /// Gets or sets the preview settings.
    /// </summary>
    [Description("The preview settings.")]
    public PreviewSettings PreviewSettings
    {
      get { return Config.PreviewSettings; }
      set { Config.PreviewSettings = value; }
    }
    #endregion

    #region Email
    /// <summary>
    /// Gets or sets the email settings.
    /// </summary>
    [Description("The email settings.")]
    public FastReport.Export.Email.EmailSettings EmailSettings
    {
      get { return Config.EmailSettings; }
      set { Config.EmailSettings = value; }
    }
    #endregion
  }
}
