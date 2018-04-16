using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Web.DBHelper
{
    public class DBHelperSQL
    {
        public static IDBBase Provider = new DBMSSQL();
        private static int iTransMode = 0;
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        //  public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region -- ExecuteNonQuery --

        //public static int ExecuteNonQuery( DALExecuteArgs args, CommandType cmdType, string cmdText, TSDbParameterCollection cmdParms, bool bNeedTrans )
        public static int ExecuteNonQuery(DALExecuteArgs args, CommandType cmdType, string cmdText, bool bNeedTrans, DbParameter[] cmdParms)
        {
            if (args.DbConnection == null)
            {
                if (bNeedTrans)
                {
                    #region -- 没有传入 DbConnection，需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        try
                        {
                            conn.Open();

                            IsolationLevel level;
                            //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                            if (iTransMode == 0)
                            {
                                level = IsolationLevel.ReadCommitted;
                            }
                            else
                            {
                                level = IsolationLevel.ReadUncommitted;
                            }
                            using (DbTransaction trans = conn.BeginTransaction(level))
                            {
                                using (DbCommand cmd = Provider.CreateCommand())
                                {
                                    int iResult = 0;
                                    try
                                    {
                                        iResult = _ExecuteNonQuery(cmd, conn, trans, cmdType, cmdText, cmdParms);
                                        trans.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            trans.Rollback();
                                        }
                                        catch
                                        {
                                        }
                                        throw ex;
                                    }
                                    return iResult;
                                }
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                    #endregion -- 没有传入 DbConnection，需要事务 --
                }
                else
                {
                    #region -- 没有传入 DbConnection，不需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        try
                        {
                            using (DbCommand cmd = Provider.CreateCommand())
                            {
                                return _ExecuteNonQuery(cmd, conn, null, cmdType, cmdText, cmdParms);
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                    #endregion -- 没有传入 DbConnection，不需要事务 --
                }
            }
            else if (args.DbTrans != null)
            {
                #region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    return _ExecuteNonQuery(cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms);
                }

                #endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
            }
            else if (bNeedTrans)
            {
                //#region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

                ////throw new Exception( "当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。" );

                //#endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

                #region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：新建事务 --
                IsolationLevel level;
                //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                if (iTransMode == 0)
                {
                    level = IsolationLevel.ReadCommitted;
                }
                else
                {
                    level = IsolationLevel.ReadUncommitted;
                }
                using (DbTransaction trans = args.DbConnection.BeginTransaction(level))
                {
                    using (DbCommand cmd = Provider.CreateCommand())
                    {
                        int iResult = 0;
                        try
                        {
                            iResult = _ExecuteNonQuery(cmd, args.DbConnection, trans, cmdType, cmdText, cmdParms);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch
                            {
                            }
                            throw ex;
                        }
                        return iResult;
                    }
                }
                #endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：新建事务 --
            }
            else
            {
                #region -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    return _ExecuteNonQuery(cmd, args.DbConnection, null, cmdType, cmdText, cmdParms);
                }

                #endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
            }
        }

        private static int _ExecuteNonQuery(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
            int iResult = cmd.ExecuteNonQuery();
            //GetParameterOut( cmdParms, cmd.Parameters );
            return iResult;
        }

        #endregion -- ExecuteNonQuery --

        #region -- ExecInTrans --

        public delegate void ExecInTransDelegate(DbConnection conn, DbTransaction trans);
        public static void ExecInTrans(DALExecuteArgs args, ExecInTransDelegate execDelegate)
        {
            if (args.DbConnection != null && args.DbTrans != null)
            {
                execDelegate(args.DbConnection, args.DbTrans);
            }
            else
            {
                string dbName = args.DBName;
                //using( DbConnection conn = Provider.CreateConnection( dbName ) )
                using (DbConnection conn = Provider.CreateConnection())
                {
                    try
                    {
                        conn.Open();

                        IsolationLevel level;
                        //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                        if (iTransMode == 0)
                        {
                            level = IsolationLevel.ReadCommitted;
                        }
                        else
                        {
                            level = IsolationLevel.ReadUncommitted;
                        }
                        using (DbTransaction trans = conn.BeginTransaction(level))
                        {
                            using (DbCommand cmd = Provider.CreateCommand())
                            {
                                try
                                {
                                    execDelegate(conn, trans);

                                    trans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        trans.Rollback();
                                    }
                                    catch
                                    {
                                    }
                                    throw ex;
                                }
                            }
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        //public static void ExecInTrans( string dbName, ExecInTransDelegate execDelegate )
        public static void ExecInTrans(ExecInTransDelegate execDelegate)
        {
            using (DbConnection conn = Provider.CreateConnection())
            {
                try
                {
                    conn.Open();

                    IsolationLevel level;
                    //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                    if (iTransMode == 0)
                    {
                        level = IsolationLevel.ReadCommitted;
                    }
                    else
                    {
                        level = IsolationLevel.ReadUncommitted;
                    }
                    using (DbTransaction trans = conn.BeginTransaction(level))
                    {
                        using (DbCommand cmd = Provider.CreateCommand())
                        {
                            try
                            {
                                execDelegate(conn, trans);

                                trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    trans.Rollback();
                                }
                                catch
                                {
                                }
                                throw ex;
                            }
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion -- ExecInTrans --


        #region -- 查询 --

        public enum enQueryTransType
        {
            Non,
            ReadCommitted,
            Snapshot,
            ReadUncommitted
        }

        #region--DataTable --

        private static DataTable ExecuteQuery(DALExecuteArgs args, CommandType cmdType, string cmdText, enQueryTransType transType, params DbParameter[] cmdParms)
        {
            DataTable dtResult = null;

            if (args.DbConnection == null)
            {
                if (transType != enQueryTransType.Non)
                {
                    #region -- 没有传入 DbConnection，需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        try
                        {
                            conn.Open();

                            IsolationLevel level;
                            //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                            if (iTransMode == 0)
                            {
                                if (transType == enQueryTransType.ReadUncommitted)
                                {
                                    level = IsolationLevel.ReadUncommitted;
                                }
                                else if (transType == enQueryTransType.Snapshot && Provider.SnapshotSupported(args.DBName))
                                {
                                    level = IsolationLevel.Snapshot;
                                }
                                else
                                {
                                    level = IsolationLevel.ReadCommitted;
                                }
                            }
                            else
                            {
                                level = IsolationLevel.ReadUncommitted;
                            }

                            using (DbTransaction trans = conn.BeginTransaction(level))
                            {
                                using (DbCommand cmd = Provider.CreateCommand())
                                {
                                    try
                                    {
                                        // 准备参数
                                        PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);

                                        // 执行
                                        dtResult = new DataTable("Reuslt");
                                        DbDataAdapter adapter = Provider.CreateDataAdapter();
                                        adapter.SelectCommand = cmd;
                                        adapter.Fill(dtResult);

                                        // 提交
                                        trans.Commit();

                                        //// 取输出参数值
                                        //GetParameterOut( cmdParms, cmd.Parameters );

                                        return dtResult;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            trans.Rollback();
                                        }
                                        catch
                                        {
                                        }
                                        throw ex;
                                    }
                                }
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                    #endregion -- 没有传入 DbConnection，需要事务 --
                }
                else
                {
                    #region -- 没有传入 DbConnection，不需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        try
                        {
                            using (DbCommand cmd = Provider.CreateCommand())
                            {
                                if (cmdType == CommandType.Text)
                                {
                                    cmdText = "set transaction isolation level read uncommitted;" + Environment.NewLine + cmdText;
                                }
                                // 准备参数
                                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                                // 执行
                                dtResult = new DataTable("Reuslt");
                                DbDataAdapter adapter = Provider.CreateDataAdapter();
                                adapter.SelectCommand = cmd;
                                adapter.Fill(dtResult);

                                //// 取输出参数值
                                //GetParameterOut( cmdParms, cmd.Parameters );

                                return dtResult;
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                    #endregion -- 没有传入 DbConnection，不需要事务 --
                }
            }
            else if (args.DbTrans != null)
            {
                #region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    // 准备参数
                    PrepareCommand(cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms);

                    // 执行
                    dtResult = new DataTable("Reuslt");
                    DbDataAdapter adapter = Provider.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dtResult);

                    //// 取输出参数值
                    //GetParameterOut( cmdParms, cmd.Parameters );

                    return dtResult;
                }

                #endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
            }
            else if (transType != enQueryTransType.Non)
            {
                #region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

                //throw new Exception( "当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。" );
                IsolationLevel level;
                //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                if (iTransMode == 0)
                {
                    if (transType == enQueryTransType.ReadUncommitted)
                    {
                        level = IsolationLevel.ReadUncommitted;
                    }
                    else if (transType == enQueryTransType.Snapshot && Provider.SnapshotSupported(args.DBName))
                    {
                        level = IsolationLevel.Snapshot;
                    }
                    else
                    {
                        level = IsolationLevel.ReadCommitted;
                    }
                }
                else
                {
                    level = IsolationLevel.ReadUncommitted;
                }

                using (DbTransaction trans = args.DbConnection.BeginTransaction(level))
                {
                    using (DbCommand cmd = Provider.CreateCommand())
                    {
                        try
                        {
                            // 准备参数
                            PrepareCommand(cmd, args.DbConnection, trans, cmdType, cmdText, cmdParms);

                            // 执行
                            dtResult = new DataTable("Reuslt");
                            DbDataAdapter adapter = Provider.CreateDataAdapter();
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dtResult);

                            // 提交
                            trans.Commit();

                            //// 取输出参数值
                            //GetParameterOut( cmdParms, cmd.Parameters );

                            return dtResult;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch
                            {
                            }
                            throw ex;
                        }
                    }
                }
                #endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --
            }
            else
            {
                #region -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    if (cmdType == CommandType.Text)
                    {
                        cmdText = "set transaction isolation level read uncommitted;" + Environment.NewLine + cmdText;
                    }
                    // 准备参数
                    PrepareCommand(cmd, args.DbConnection, null, cmdType, cmdText, cmdParms);

                    // 执行
                    dtResult = new DataTable("Reuslt");
                    DbDataAdapter adapter = Provider.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dtResult);

                    //// 取输出参数值
                    //GetParameterOut( cmdParms, cmd.Parameters );

                    return dtResult;
                }

                #endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
            }
        }

        public static DataTable ExecuteQuery(DALExecuteArgs args, string cmdText, enQueryTransType transType)
        {
            return ExecuteQuery(args, cmdText, transType, null);
        }

        public static DataTable ExecuteQuery(DALExecuteArgs args, string cmdText)
        {
            //return ExecuteQuery(args, cmdText, null);
            return ExecuteQuery(args, cmdText, enQueryTransType.Non, null);
        }

        public static DataTable ExecuteQuery(DALExecuteArgs args, string cmdText, bool IsNeedTran)
        {
            //是否需要事务
            if (IsNeedTran)
            {
                return ExecuteQuery(args, cmdText, null);
            }
            else
            {
                return ExecuteQuery(args, cmdText, enQueryTransType.Non, null);
            }
        }

        //public static DataTable ExecuteQuery( DALExecuteArgs args, string cmdText, TSDbParameterCollection cmdParms, enQueryTransType transType )
        public static DataTable ExecuteQuery(DALExecuteArgs args, string cmdText, enQueryTransType transType, params DbParameter[] cmdParms)
        {
            return ExecuteQuery(args, CommandType.Text, cmdText, transType, cmdParms);
        }

        //public static DataTable ExecuteQuery( DALExecuteArgs args, string cmdText, TSDbParameterCollection cmdParms )
        public static DataTable ExecuteQuery(DALExecuteArgs args, string cmdText, params DbParameter[] cmdParms)
        {
            return ExecuteQuery(args, CommandType.Text, cmdText, enQueryTransType.ReadCommitted, cmdParms);
        }
        #endregion--DataTable --

        #region--DataSet--

        //private static DataSet ExecuteQuery4DataSet( DALExecuteArgs args, CommandType cmdType, string cmdText, TSDbParameterCollection cmdParms, enQueryTransType transType )
        private static DataSet ExecuteQuery4DataSet(DALExecuteArgs args, CommandType cmdType, string cmdText, enQueryTransType transType, params DbParameter[] cmdParms)
        {
            DataSet dsResult = null;

            if (args.DbConnection == null)
            {
                if (transType != enQueryTransType.Non)
                {
                    #region -- 没有传入 DbConnection，需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        conn.Open();

                        IsolationLevel level;
                        if (transType == enQueryTransType.Snapshot && Provider.SnapshotSupported(args.DBName))
                        {
                            level = IsolationLevel.Snapshot;
                        }
                        else
                        {
                            level = IsolationLevel.ReadCommitted;
                        }

                        using (DbTransaction trans = conn.BeginTransaction(level))
                        {
                            using (DbCommand cmd = Provider.CreateCommand())
                            {
                                try
                                {
                                    // 准备参数
                                    PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);

                                    // 执行
                                    dsResult = new DataSet();
                                    DbDataAdapter adapter = Provider.CreateDataAdapter();
                                    adapter.SelectCommand = cmd;
                                    adapter.Fill(dsResult);

                                    // 提交
                                    trans.Commit();

                                    //// 取输出参数值
                                    //GetParameterOut( cmdParms, cmd.Parameters );

                                    return dsResult;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        trans.Rollback();
                                    }
                                    catch
                                    {
                                    }
                                    throw ex;
                                }
                            }
                        }
                    }

                    #endregion -- 没有传入 DbConnection，需要事务 --
                }
                else
                {
                    #region -- 没有传入 DbConnection，不需要事务 --

                    //using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
                    using (DbConnection conn = Provider.CreateConnection())
                    {
                        using (DbCommand cmd = Provider.CreateCommand())
                        {
                            // 准备参数
                            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                            // 执行
                            dsResult = new DataSet();
                            DbDataAdapter adapter = Provider.CreateDataAdapter();
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dsResult);

                            //// 取输出参数值
                            //GetParameterOut( cmdParms, cmd.Parameters );

                            return dsResult;
                        }
                    }

                    #endregion -- 没有传入 DbConnection，不需要事务 --
                }
            }
            else if (args.DbTrans != null)
            {
                #region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    // 准备参数
                    PrepareCommand(cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms);

                    // 执行
                    dsResult = new DataSet();
                    DbDataAdapter adapter = Provider.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dsResult);

                    //// 取输出参数值
                    //GetParameterOut( cmdParms, cmd.Parameters );

                    return dsResult;
                }

                #endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
            }
            else if (transType != enQueryTransType.Non)
            {
                #region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

                throw new Exception("当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。");

                #endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --
            }
            else
            {
                #region -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --

                using (DbCommand cmd = Provider.CreateCommand())
                {
                    // 准备参数
                    PrepareCommand(cmd, args.DbConnection, null, cmdType, cmdText, cmdParms);

                    // 执行
                    dsResult = new DataSet();
                    DbDataAdapter adapter = Provider.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dsResult);

                    //// 取输出参数值
                    //GetParameterOut( cmdParms, cmd.Parameters );

                    return dsResult;
                }

                #endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
            }
        }


        //public static DataSet ExecuteQuery4DataSet( DALExecuteArgs args, string cmdText, enQueryTransType transType )
        //{
        //    return ExecuteQuery4DataSet( args, cmdText, null, transType );
        //}
        public static DataSet ExecuteQuery4DataSet(DALExecuteArgs args, string cmdText)
        {
            return ExecuteQuery4DataSet(args, cmdText, enQueryTransType.Non, null);
        }

        public static DataSet ExecuteQuery4DataSet(DALExecuteArgs args, string cmdText, enQueryTransType transType, params DbParameter[] cmdParms)
        {
            return ExecuteQuery4DataSet(args, CommandType.Text, cmdText, transType, cmdParms);
        }

        //public static DataSet ExecuteQuery4DataSet( DALExecuteArgs args, string cmdText, TSDbParameterCollection cmdParms )
        //{
        //    return ExecuteQuery4DataSet( args, CommandType.Text, cmdText, cmdParms, enQueryTransType.ReadCommitted );
        //}
        #endregion--DataSet-
        #endregion -- 查询 --

        #region -- 参数的处理 --

        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        //private static void GetParameterOut( TSDbParameterCollection tsParms, DbParameterCollection dbParms )
        //{
        //    if( tsParms != null && tsParms.Count > 0 )
        //    {
        //        foreach( TSDbParameter parm in tsParms )
        //        {
        //            if( parm.Direction != ParameterDirection.Input )
        //            {
        //                parm.Value = dbParms["@" + parm.ParameterName].Value;
        //            }
        //        }
        //    }
        //}
        #region === 创造DbParameter的实例 ===

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public static DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public static DbParameter CreateInDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输输出DbParameter的实例
        /// </summary>
        public static DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value, bool IsOutPut)
        {
            ParameterDirection direction = ParameterDirection.Input;
            if (IsOutPut)
            {
                direction = ParameterDirection.InputOutput;
            }
            return CreateDbParameter(paraName, dbType, size, value, direction);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>
        public static DbParameter CreateInDbParameter(string paraName, DbType dbType, object value, bool IsOutPut)
        {
            ParameterDirection direction = ParameterDirection.Input;
            if (IsOutPut)
            {
                direction = ParameterDirection.InputOutput;
            }
            return CreateDbParameter(paraName, dbType, 0, value, direction);
        }


        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        private static DbParameter CreateDbParameter(string paraName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            DbParameter para = new SqlParameter();

            para.ParameterName = paraName;

            if (size != 0)
                para.Size = size;

            para.DbType = dbType;

            if (value != null)
                para.Value = value;

            para.Direction = direction;

            return para;
        }

        #endregion
        #endregion -- 参数的处理 --

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (DbConnection conn = Provider.CreateConnection())
            {
                using (DbCommand cmd = Provider.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            //    {
            //        try
            //        {
            //            connection.Open();
            //            object obj = cmd.ExecuteScalar();
            //            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            //            {
            //                return null;
            //            }
            //            else
            //            {
            //                return obj;
            //            }
            //        }
            //        catch (System.Data.SqlClient.SqlException e)
            //        {
            //            connection.Close();
            //            throw e;
            //        }
            //    }
            //}
        }
    }
}
