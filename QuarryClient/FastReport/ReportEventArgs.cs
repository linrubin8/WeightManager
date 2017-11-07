using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;
using System.Data.Common;
using System.ComponentModel;

namespace FastReport
{
  /// <summary>
  /// Provides data for the <see cref="FastReport.Report.LoadBaseReport"/> event.
  /// </summary>
  public class CustomLoadEventArgs : EventArgs
  {
    private string FFileName;
    private Report FReport;

    /// <summary>
    /// Gets a name of the file to load the report from.
    /// </summary>
    public string FileName
    {
      get { return FFileName; }
    }

    /// <summary>
    /// The reference to a report.
    /// </summary>
    public Report Report
    {
      get { return FReport; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomLoadEventArgs"/> class using the specified
    /// file name and the report.
    /// </summary>
    /// <param name="fileName">The name of the file to load the report from.</param>
    /// <param name="report">The report.</param>
    public CustomLoadEventArgs(string fileName, Report report)
    {
      FFileName = fileName;
      FReport = report;
    }
  }

  /// <summary>
  /// Provides data for the <see cref="FastReport.Report.CustomCalc"/> event.
  /// </summary>
  public class CustomCalcEventArgs : EventArgs
  {
    private string FExpr;
    private object FObject;
    private Report FReport;

    /// <summary>
    /// Gets an expression.
    /// </summary>
    public string Expression
    {
      get { return FExpr; }
    }

    /// <summary>
    /// Gets or sets a object.
    /// </summary>
    public object CalculatedObject
    {
      get { return FObject; }
      set { FObject = value; }
    }

    /// <summary>
    /// The reference to a report.
    /// </summary>
    public Report Report
    {
      get { return FReport; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomLoadEventArgs"/> class using the specified
    /// file name and the report.
    /// </summary>
    /// <param name="expression">The text of expression.</param>
    /// <param name="Object">The name of the file to load the report from.</param>
    /// <param name="report">The report.</param>
    public CustomCalcEventArgs(string expression, object Object, Report report)
    {
      FExpr = expression;
      FObject = Object;
      FReport = report;
    }
  }

  /// <summary>
  /// Represents the method that will handle the <see cref="Report.LoadBaseReport"/> event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void CustomLoadEventHandler(object sender, CustomLoadEventArgs e);

  /// <summary>
  /// Represents the method that will handle the event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void CustomCalcEventHandler(object sender, CustomCalcEventArgs e);

  /// <summary>
  /// Provides data for the Progress event.
  /// </summary>
  public class ProgressEventArgs
  {
    private string FMessage;
    private int FProgress;
    private int FTotal;
    
    /// <summary>
    /// Gets a progress message.
    /// </summary>
    public string Message
    {
      get { return FMessage; }
    }
    
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int Progress
    {
      get { return FProgress; }
    }
    
    /// <summary>
    /// Gets the number of total pages.
    /// </summary>
    public int Total
    {
      get { return FTotal; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressEventArgs"/> class using the specified
    /// message, page number and total number of pages.
    /// </summary>
    /// <param name="message">The progress message.</param>
    /// <param name="progress">Current page number.</param>
    /// <param name="total">Number of total pages.</param>
    public ProgressEventArgs(string message, int progress, int total)
    {
      FMessage = message;
      FProgress = progress;
      FTotal = total;
    }
  }

  /// <summary>
  /// Represents the method that will handle the Progress event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);


  /// <summary>
  /// Provides data for the DatabaseLogin event.
  /// </summary>
  public class DatabaseLoginEventArgs
  {
    private string FConnectionString;
    private string FUserName;
    private string FPassword;
    
    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string ConnectionString
    {
      get { return FConnectionString; }
      set { FConnectionString = value; }
    }
    
    /// <summary>
    /// Gets or sets an user name.
    /// </summary>
    public string UserName
    {
      get { return FUserName; }
      set { FUserName = value; }
    }
    
    /// <summary>
    /// Gets or sets a password.
    /// </summary>
    public string Password
    {
      get { return FPassword; }
      set { FPassword = value; }
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseLoginEventArgs"/> class using the specified
    /// connection string.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    public DatabaseLoginEventArgs(string connectionString)
    {
      FConnectionString = connectionString;
      FUserName = "";
      FPassword = "";
    }
  }


  /// <summary>
  /// Represents the method that will handle the DatabaseLogin event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void DatabaseLoginEventHandler(object sender, DatabaseLoginEventArgs e);


  /// <summary>
  /// Provides data for the AfterDatabaseLogin event.
  /// </summary>
  public class AfterDatabaseLoginEventArgs
  {
    private DbConnection FConnection;
    
    /// <summary>
    /// Gets the <b>DbConnection</b> object.
    /// </summary>
    public DbConnection Connection
    {
      get { return FConnection; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AfterDatabaseLoginEventArgs"/> class using 
    /// the specified connection.
    /// </summary>
    /// <param name="connection">The connection object.</param>
    public AfterDatabaseLoginEventArgs(DbConnection connection)
    {
      FConnection = connection;
    }
  }

  /// <summary>
  /// Represents the method that will handle the AfterDatabaseLogin event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void AfterDatabaseLoginEventHandler(object sender, AfterDatabaseLoginEventArgs e);


  /// <summary>
  /// Provides data for the FilterProperties event.
  /// </summary>
  public class FilterPropertiesEventArgs
  {
    private PropertyDescriptor FProperty;
    private bool FSkip;

    /// <summary>
    /// Gets the property descriptor.
    /// </summary>
    public PropertyDescriptor Property
    {
      get { return FProperty; }
      set { FProperty = value; }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether this property should be skipped.
    /// </summary>
    public bool Skip
    {
      get { return FSkip; }
      set { FSkip = value; }
    }

    internal FilterPropertiesEventArgs(PropertyDescriptor property)
    {
      FProperty = property;
      FSkip = false;
    }
  }

  /// <summary>
  /// Represents the method that will handle the FilterProperties event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void FilterPropertiesEventHandler(object sender, FilterPropertiesEventArgs e);


  /// <summary>
  /// Provides data for the GetPropertyKind event.
  /// </summary>
  public class GetPropertyKindEventArgs
  {
    private string FPropertyName;
    private Type FPropertyType;
    private PropertyKind FPropertyKind;

    /// <summary>
    /// Gets the property name.
    /// </summary>
    public string PropertyName
    {
      get { return FPropertyName; }
    }

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public Type PropertyType
    {
      get { return FPropertyType; }
    }

    /// <summary>
    /// Gets or sets the kind of property.
    /// </summary>
    public PropertyKind PropertyKind
    {
      get { return FPropertyKind; }
      set { FPropertyKind = value; }
    }

    internal GetPropertyKindEventArgs(string propertyName, Type propertyType, PropertyKind propertyKind)
    {
      FPropertyName = propertyName;
      FPropertyType = propertyType;
      FPropertyKind = propertyKind;
    }
  }

  /// <summary>
  /// Represents the method that will handle the GetPropertyKind event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void GetPropertyKindEventHandler(object sender, GetPropertyKindEventArgs e);


  /// <summary>
  /// Provides data for the GetTypeInstance event.
  /// </summary>
  public class GetTypeInstanceEventArgs
  {
    private Type FType;
    private object FInstance;

    /// <summary>
    /// Gets the type.
    /// </summary>
    public Type Type
    {
      get { return FType; }
    }

    /// <summary>
    /// Gets or sets the instance of type.
    /// </summary>
    public object Instance
    {
      get { return FInstance; }
      set { FInstance = value; }
    }

    internal GetTypeInstanceEventArgs(Type type)
    {
      FType = type;
    }
  }

  /// <summary>
  /// Represents the method that will handle the GetPropertyKind event.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The event data.</param>
  public delegate void GetTypeInstanceEventHandler(object sender, GetTypeInstanceEventArgs e);
}
