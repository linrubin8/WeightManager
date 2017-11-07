using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Data;

namespace FastReport.Design
{
	/// <summary>
	/// Provides a data for the designer ReportLoaded event.
	/// </summary>
	public class ReportLoadedEventArgs
	{
		private Report FReport;

		/// <summary>
		/// The current report.
		/// </summary>
		public Report Report
		{
			get
			{
				return FReport;
			}
		}

		internal ReportLoadedEventArgs( Report report )
		{
			FReport = report;
		}
	}

	/// <summary>
	/// Represents the method that will handle the designer ReportLoaded event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void ReportLoadedEventHandler( object sender, ReportLoadedEventArgs e );


	/// <summary>
	/// Provides a data for the designer ObjectInserted event.
	/// </summary>
	public class ObjectInsertedEventArgs
	{
		private Base FObject;
		private InsertFrom FInsertSource;

		/// <summary>
		/// Gets the inserted object.
		/// </summary>
		public Base Object
		{
			get
			{
				return FObject;
			}
		}

		/// <summary>
		/// Gets the source where the object is inserted from.
		/// </summary>
		public InsertFrom InsertSource
		{
			get
			{
				return FInsertSource;
			}
		}

		internal ObjectInsertedEventArgs( Base obj, InsertFrom source )
		{
			FObject = obj;
			FInsertSource = source;
		}
	}

	/// <summary>
	/// Represents the method that will handle the designer ObjectInserted event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void ObjectInsertedEventHandler( object sender, ObjectInsertedEventArgs e );


	/// <summary>
	/// Provides a data for the designer's custom dialog events.
	/// </summary>
	public class OpenSaveDialogEventArgs
	{
		private string FFileName;
		private bool FCancel;
		private object FData;
		private Designer FDesigner;
		private bool FIsPlugin;

		/// <summary>
		/// Gets or sets a file name.
		/// </summary>
		/// <remarks>
		/// This property contains the location of a report. If you work with files (like the 
		/// standard "Open" and "Save" dialogs do), treat this property as a file name. 
		/// </remarks>
		public string FileName
		{
			get
			{
				return FFileName;
			}
			set
			{
				FFileName = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating that the dialog was cancelled.
		/// </summary>
		/// <remarks>
		/// This property is used to tell the designer that the user was cancelled the dialog.
		/// </remarks>
		public bool Cancel
		{
			get
			{
				return FCancel;
			}
			set
			{
				FCancel = value;
			}
		}

		/// <summary>
		/// Gets or sets the custom data that is shared across events.
		/// </summary>
		/// <remarks>
		/// You may set the Data in the OpenDialog event and use it later in the OpenReport event.
		/// </remarks>
		public object Data
		{
			get
			{
				return FData;
			}
			set
			{
				FData = value;
			}
		}

		/// <summary>
		/// Gets a report designer.
		/// </summary>
		public Designer Designer
		{
			get
			{
				return FDesigner;
			}
		}

		internal bool IsPlugin
		{
			get
			{
				return FIsPlugin;
			}
			set
			{
				FIsPlugin = value;
			}
		}

		internal OpenSaveDialogEventArgs( Designer designer )
		{
			FDesigner = designer;
			FFileName = "";
		}
	}

	/// <summary>
	/// Represents the method that will handle the designer's custom dialogs event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void OpenSaveDialogEventHandler( object sender, OpenSaveDialogEventArgs e );


	/// <summary>
	/// Provides a data for the designer's custom dialog events.
	/// </summary>
	public partial class OpenSaveReportEventArgs
	{
		// Simon: Ôö¼Ó partial
		private Report FReport;
		private string FFileName = "";
		private object FData;
		private bool FIsPlugin;

		/// <summary>
		/// Gets a report.
		/// </summary>
		/// <remarks>
		/// Use this report in the load/save operations.
		/// </remarks>
		public Report Report
		{
			get
			{
				return FReport;
			}
		}

		/// <summary>
		/// Gets a file name.
		/// </summary>
		/// <remarks>
		/// This property contains the location of a report that was selected by the user in the 
		/// open/save dialogs. If you work with files (like the standard "Open" and "Save" dialogs do), 
		/// treat this property as a file name. 
		/// </remarks>
		public string FileName
		{
			get
			{
				return FFileName;
			}
		}

		/// <summary>
		/// Gets the custom data that was set in the OpenDialog event.
		/// </summary>
		public object Data
		{
			get
			{
				return FData;
			}
		}

		internal bool IsPlugin
		{
			get
			{
				return FIsPlugin;
			}
		}

		internal OpenSaveReportEventArgs( Report report, string fileName, object data, bool isPlugin )
		{
			FReport = report;
			FFileName = fileName;
			FData = data;
			FIsPlugin = isPlugin;
		}
	}

	/// <summary>
	/// Represents the method that will handle the designer's custom dialogs event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void OpenSaveReportEventHandler( object sender, OpenSaveReportEventArgs e );


	/// <summary>
	/// Provides data for the FilterConnectionTables event.
	/// </summary>
	public class FilterConnectionTablesEventArgs
	{
		private DataConnectionBase FConnection;
		private string FTableName;
		private bool FSkip;

		/// <summary>
		/// Gets the Connection object.
		/// </summary>
		public DataConnectionBase Connection
		{
			get
			{
				return FConnection;
			}
		}

		/// <summary>
		/// Gets the table name.
		/// </summary>
		public string TableName
		{
			get
			{
				return FTableName;
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether this table should be skipped.
		/// </summary>
		public bool Skip
		{
			get
			{
				return FSkip;
			}
			set
			{
				FSkip = value;
			}
		}

		internal FilterConnectionTablesEventArgs( DataConnectionBase connection, string tableName )
		{
			FConnection = connection;
			FTableName = tableName;
		}
	}

	/// <summary>
	/// Represents the method that will handle the FilterConnectionTables event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void FilterConnectionTablesEventHandler( object sender, FilterConnectionTablesEventArgs e );


	/// <summary>
	/// Provides data for the CustomQueryBuilder event.
	/// </summary>
	public class CustomQueryBuilderEventArgs
	{
		private DataConnectionBase FConnection;
		private string FSQL;

		/// <summary>
		/// Gets the Connection object.
		/// </summary>
		public DataConnectionBase Connection
		{
			get
			{
				return FConnection;
			}
		}

		/// <summary>
		/// Gets or sets the query text.
		/// </summary>
		public string SQL
		{
			get
			{
				return FSQL;
			}
			set
			{
				FSQL = value;
			}
		}

		internal CustomQueryBuilderEventArgs( DataConnectionBase connection, string sql )
		{
			FConnection = connection;
			FSQL = sql;
		}
	}

	/// <summary>
	/// Represents the method that will handle the CustomQueryBuilder event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void CustomQueryBuilderEventHandler( object sender, CustomQueryBuilderEventArgs e );

}
