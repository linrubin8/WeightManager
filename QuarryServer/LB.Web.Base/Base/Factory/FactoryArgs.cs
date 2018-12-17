using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web;

namespace LB.Web.Base.Factory
{
    public delegate void DALExecuteMethod(FactoryArgs args);

    public class FactoryArgs
    {
        private string _DBName;
        public string DBName
        {
            get
            {
                return _DBName;
            }
        }

        private string _LoginName;
        public string LoginName
        {
            get
            {
                return _LoginName;
            }
        }

        private bool _IsNeedSession=true;
        public bool IsNeedSession
        {
            get
            {
                return _IsNeedSession;
            }
        }

        private long _SessionID;
        public long SessionID
        {
            get
            {
                return _SessionID;
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

        private DataTable mspINParm;
        public DataTable SPINParm
        {
            get
            {
                return mspINParm;
            }
            set
            {
                mspINParm = value;
            }
        }

        public FactoryArgs(string dbName, string loginName,long sessionID,bool isNeedSession, DbConnection conn, DbTransaction trans)
        {
            _IsNeedSession = isNeedSession;
            _SessionID = sessionID;
            _DBName = dbName;
            _LoginName = loginName;
            mDbConnection = conn;
            mDbTrans = trans;
        } 
    }
}