using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace LB.Web.Base.Helper
{
	public interface IDBBase
	{
		DbCommand CreateCommand();
		DbConnection CreateConnection( string dbName );
		DbDataAdapter CreateDataAdapter();
		DbParameter CreateParameter( LBDbParameter dbParm );
		bool SnapshotSupported( string dbName );
	}
}
