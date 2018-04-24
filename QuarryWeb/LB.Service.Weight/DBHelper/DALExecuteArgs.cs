using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Web.DBHelper
{
    public delegate void DALExecuteMethod(DALExecuteArgs args);
    public class DALExecuteArgs
    {
        private string mstrDBName;
        public string DBName
        {
            get
            {
                return mstrDBName;
            }
        }

        private string mstrLoginName;
        public string LoginName
        {
            get
            {
                return mstrLoginName;
            }
        }

        private DbConnection mDbConnection = null;
        public DbConnection DbConnection
        {
            get
            {
                return mDbConnection;
            }
        }

        private DbTransaction mDbTrans = null;
        public DbTransaction DbTrans
        {
            get
            {
                return mDbTrans;
            }
        }

        private DataTable mdtSelectResult = null;
        public DataTable SelectResult
        {
            get
            {
                return mdtSelectResult;
            }
            set
            {
                mdtSelectResult = value;
            }
        }

        public DALExecuteArgs(string dbName, string loginName, DbConnection conn, DbTransaction trans)
        {
            mstrDBName = dbName;
            mstrLoginName = loginName;
            mDbConnection = conn;
            mDbTrans = trans;
        }

        public DALExecuteArgs()
        {
            // TODO: Complete member initialization
        }
    }
}
