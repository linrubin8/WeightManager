using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Web.DBHelper
{
    public interface IDBBase
    {
        DbCommand CreateCommand();
        //DbConnection CreateConnection( string dbName );
        DbConnection CreateConnection();
        DbDataAdapter CreateDataAdapter();
        DbParameter CreateParameter(string parameterName, SqlDbType DbType, int iDBTypeSize, ParameterDirection direction, int iScale, int SqlDbTypePrecision, object objvalue);
        //	DbParameter CreateParameter( TSDbParameter dbParm );
        bool SnapshotSupported(string dbName);


    }
}
