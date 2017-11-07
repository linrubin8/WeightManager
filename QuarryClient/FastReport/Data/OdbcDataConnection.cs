using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Data.ConnectionEditors;
using System.Windows.Forms;

namespace FastReport.Data
{
  /// <summary>
  /// Represents a connection to any database through ODBC.
  /// </summary>
  /// <example>This example shows how to add a new connection to the report.
  /// <code>
  /// Report report1;
  /// OdbcDataConnection conn = new OdbcDataConnection();
  /// conn.ConnectionString = "your_connection_string";
  /// report1.Dictionary.Connections.Add(conn);
  /// conn.CreateAllTables();
  /// </code>
  /// </example>
  public class OdbcDataConnection : DataConnectionBase
  {
    private void GetDBObjectNames(string name, List<string> list)
    {
      DataTable schema = null;
      DbConnection conn = GetConnection();
      try
      {
        using (OdbcCommandBuilder builder = new OdbcCommandBuilder())
        {
          OpenConnection(conn);
          schema = conn.GetSchema("Tables");

          foreach (DataRow row in schema.Rows)
          {
            string tableType = row["TABLE_TYPE"].ToString();
            if (String.Compare(tableType, name) == 0)
            {
              string tableName = row["TABLE_NAME"].ToString();
              string schemaName = "";
              if (schema.Columns.IndexOf("TABLE_SCHEM") != -1)
                schemaName = row["TABLE_SCHEM"].ToString();
              if (String.IsNullOrEmpty(schemaName))
                list.Add(tableName);
              else
                list.Add(schemaName + "." + builder.QuoteIdentifier(tableName, conn as OdbcConnection));
            }
          }
        }
      }
      finally
      {
        DisposeConnection(conn);
      }
    }

    /// <inheritdoc/>
    public override string[] GetTableNames()
    {
      List<string> list = new List<string>();
      GetDBObjectNames("TABLE", list);
      GetDBObjectNames("SYSTEM TABLE", list);
      GetDBObjectNames("VIEW", list);
      return list.ToArray();
    }

    /// <inheritdoc/>
    protected override string GetConnectionStringWithLoginInfo(string userName, string password)
    {
      OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder(ConnectionString);

      builder.Remove("uid");
      builder.Add("uid", userName);

      builder.Remove("pwd");
      builder.Add("pwd", password);

      return builder.ToString();
    }

    /// <inheritdoc/>
    public override string QuoteIdentifier(string value, DbConnection connection)
    {
      // already quoted?
      if (value.EndsWith("\"") || value.EndsWith("]") || value.EndsWith("'") || value.EndsWith("`"))
        return value;

      // Odbc is universal connection, so we need quoting dependent on used database type
      using (OdbcCommandBuilder builder = new OdbcCommandBuilder())
      {
        return builder.QuoteIdentifier(value, connection as OdbcConnection);
      }
    }

    /// <inheritdoc/>
    public override Type GetConnectionType()
    {
      return typeof(OdbcConnection);
    }

    /// <inheritdoc/>
    public override DbDataAdapter GetAdapter(string selectCommand, DbConnection connection,
      CommandParameterCollection parameters)
    {
      OdbcDataAdapter adapter = new OdbcDataAdapter(selectCommand, connection as OdbcConnection);
      foreach (CommandParameter p in parameters)
      {
        OdbcParameter parameter = adapter.SelectCommand.Parameters.Add(p.Name, (OdbcType)p.DataType, p.Size);
        parameter.Value = p.Value;
      }

      return adapter;
    }

    /// <inheritdoc/>
    public override ConnectionEditorBase GetEditor()
    {
      return new OdbcConnectionEditor();
    }

    /// <inheritdoc/>
    public override Type GetParameterType()
    {
      return typeof(OdbcType);
    }

    /// <inheritdoc/>
    public override int GetDefaultParameterType()
    {
      return (int)OdbcType.VarChar;
    }

    /// <inheritdoc/>
    public override string GetConnectionId()
    {
      return "ODBC: " + ConnectionString;
    }
  }
}
