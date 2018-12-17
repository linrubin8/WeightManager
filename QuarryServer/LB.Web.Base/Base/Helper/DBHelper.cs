using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using LB.Web.Base.Factory;
using LB.Web.Base.Base.Factory;

namespace LB.Web.Base.Helper
{
	public class DBHelper
	{
        public static string DBName = "";
        public static string DBServer = "";
        public static string DBUer = "";
        public static string DBPw = "";
        public static bool LoginSecure = false;
        public static IDBBase Provider = null;

		#region -- ExecuteNonQuery --

		public static int ExecuteNonQuery(FactoryArgs args, CommandType cmdType, string cmdText, LBDbParameterCollection cmdParms, bool bNeedTrans )
		{
			if( args.DbConnection == null )
			{
				if( bNeedTrans )
				{
					#region -- 没有传入 DbConnection，需要事务 --

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						try
						{
							conn.Open();

							IsolationLevel level;
                            /*if( TS.DW.Setting.SettingDW.TransMode == 0 )
							{
								level = IsolationLevel.ReadCommitted;
							}
							else
							{
								level = IsolationLevel.ReadUncommitted;
							}*/
                            level = IsolationLevel.ReadUncommitted;
                            using ( DbTransaction trans = conn.BeginTransaction( level ) )
							{
								using( DbCommand cmd = Provider.CreateCommand() )
								{
									int iResult = 0;
									try
									{
										iResult = _ExecuteNonQuery( cmd, conn, trans, cmdType, cmdText, cmdParms );
										trans.Commit();
									}
									catch( Exception ex )
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

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						try
						{
							using( DbCommand cmd = Provider.CreateCommand() )
							{
								return _ExecuteNonQuery( cmd, conn, null, cmdType, cmdText, cmdParms );
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
			else if( args.DbTrans != null )
			{
				#region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					return _ExecuteNonQuery( cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms );
				}

				#endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
			}
			else if( bNeedTrans )
			{
				//#region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

				////throw new Exception( "当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。" );

				//#endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

				#region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：新建事务 --
				IsolationLevel level;
                //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                //{
                //	level = IsolationLevel.ReadCommitted;
                //}
                //else
                //{
                //	level = IsolationLevel.ReadUncommitted;
                //}
                level = IsolationLevel.ReadUncommitted;
                using ( DbTransaction trans = args.DbConnection.BeginTransaction( level ) )
				{
					using( DbCommand cmd = Provider.CreateCommand() )
					{
						int iResult = 0;
						try
						{
							iResult = _ExecuteNonQuery( cmd, args.DbConnection, trans, cmdType, cmdText, cmdParms );
							trans.Commit();
						}
						catch( Exception ex )
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

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					return _ExecuteNonQuery( cmd, args.DbConnection, null, cmdType, cmdText, cmdParms );
				}

				#endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
			}
		}

		private static int _ExecuteNonQuery( DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, LBDbParameterCollection cmdParms )
		{
			PrepareCommand( cmd, conn, trans, cmdType, cmdText, cmdParms );
			int iResult = cmd.ExecuteNonQuery();
			GetParameterOut( cmdParms, cmd.Parameters );
			return iResult;
		}

		#endregion -- ExecuteNonQuery --

		#region -- ExecInTrans --

		public delegate void ExecInTransDelegate(FactoryArgs argsTrans );
		public static void ExecInTrans(FactoryArgs args, ExecInTransDelegate execDelegate )
		{
			if( args.DbConnection != null && args.DbTrans != null )
			{
				execDelegate( args );
			}
			else
			{
				string dbName = args.DBName;
				using( DbConnection conn = Provider.CreateConnection( dbName ) )
				{
					try
					{
						conn.Open();

						IsolationLevel level;
                        //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                        //{
                        //	level = IsolationLevel.ReadCommitted;
                        //}
                        //else
                        //{
                        //	level = IsolationLevel.ReadUncommitted;
                        //}
                        level = IsolationLevel.ReadUncommitted;
                        using ( DbTransaction trans = conn.BeginTransaction( level ) )
						{
							using( DbCommand cmd = Provider.CreateCommand() )
							{
                                FactoryArgs argsTran = new FactoryArgs( args.DBName, args.LoginName, args.SessionID, args.IsNeedSession, conn, trans );
								try
								{
									execDelegate( argsTran );

									trans.Commit();
								}
								catch( Exception ex )
								{
									try
									{
										trans.Rollback();
									}
									catch
									{
									}
									args.SelectResult = argsTran.SelectResult;
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

		private static DataTable ExecuteQuery(FactoryArgs args, CommandType cmdType, string cmdText, LBDbParameterCollection cmdParms, enQueryTransType transType )
		{
			DataTable dtResult = null;

			if( args.DbConnection == null )
			{
				if( transType != enQueryTransType.Non )
				{
					#region -- 没有传入 DbConnection，需要事务 --

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						try
						{
							conn.Open();

							IsolationLevel level;
							//if( TS.DW.Setting.SettingDW.TransMode == 0 )
							//{
							//	if( transType == enQueryTransType.ReadUncommitted )
							//	{
							//		level = IsolationLevel.ReadUncommitted;
							//	}
							//	else if( transType == enQueryTransType.Snapshot && Provider.SnapshotSupported( args.DBName ) )
							//	{
							//		level = IsolationLevel.Snapshot;
							//	}
							//	else
							//	{
							//		level = IsolationLevel.ReadCommitted;
							//	}
							//}
							//else
							//{
							//	level = IsolationLevel.ReadUncommitted;
							//}

                            level = IsolationLevel.ReadUncommitted;

                            using ( DbTransaction trans = conn.BeginTransaction( level ) )
							{
								using( DbCommand cmd = Provider.CreateCommand() )
								{
									try
									{
										// 准备参数
										PrepareCommand( cmd, conn, trans, cmdType, cmdText, cmdParms );

										// 执行
										dtResult = new DataTable();
										DbDataAdapter adapter = Provider.CreateDataAdapter();
										adapter.SelectCommand = cmd;
										adapter.Fill( dtResult );

										// 提交
										trans.Commit();

										// 取输出参数值
										GetParameterOut( cmdParms, cmd.Parameters );

										return dtResult;
									}
									catch( Exception ex )
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

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						try
						{
							using( DbCommand cmd = Provider.CreateCommand() )
							{
								// 准备参数
								PrepareCommand( cmd, conn, null, cmdType, cmdText, cmdParms );

								// 执行
								dtResult = new DataTable();
								DbDataAdapter adapter = Provider.CreateDataAdapter();
								adapter.SelectCommand = cmd;
								adapter.Fill( dtResult );

								// 取输出参数值
								GetParameterOut( cmdParms, cmd.Parameters );

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
			else if( args.DbTrans != null )
			{
				#region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					// 准备参数
					PrepareCommand( cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms );

					// 执行
					dtResult = new DataTable();
					DbDataAdapter adapter = Provider.CreateDataAdapter();
					adapter.SelectCommand = cmd;
					adapter.Fill( dtResult );

					// 取输出参数值
					GetParameterOut( cmdParms, cmd.Parameters );

					return dtResult;
				}

				#endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
			}
			else if( transType != enQueryTransType.Non )
			{
				#region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

				//throw new Exception( "当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。" );
				IsolationLevel level;
                //if( TS.DW.Setting.SettingDW.TransMode == 0 )
                //{
                //	if( transType == enQueryTransType.ReadUncommitted )
                //	{
                //		level = IsolationLevel.ReadUncommitted;
                //	}
                //	else if( transType == enQueryTransType.Snapshot && Provider.SnapshotSupported( args.DBName ) )
                //	{
                //		level = IsolationLevel.Snapshot;
                //	}
                //	else
                //	{
                //		level = IsolationLevel.ReadCommitted;
                //	}
                //}
                //else
                //{
                //	level = IsolationLevel.ReadUncommitted;
                //}
                level = IsolationLevel.ReadUncommitted;

                using ( DbTransaction trans = args.DbConnection.BeginTransaction( level ) )
				{
					using( DbCommand cmd = Provider.CreateCommand() )
					{
						try
						{
							// 准备参数
							PrepareCommand( cmd, args.DbConnection, trans, cmdType, cmdText, cmdParms );

							// 执行
							dtResult = new DataTable();
							DbDataAdapter adapter = Provider.CreateDataAdapter();
							adapter.SelectCommand = cmd;
							adapter.Fill( dtResult );

							// 提交
							trans.Commit();

							// 取输出参数值
							GetParameterOut( cmdParms, cmd.Parameters );

							return dtResult;
						}
						catch( Exception ex )
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

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					// 准备参数
					PrepareCommand( cmd, args.DbConnection, null, cmdType, cmdText, cmdParms );

					// 执行
					dtResult = new DataTable();
					DbDataAdapter adapter = Provider.CreateDataAdapter();
					adapter.SelectCommand = cmd;
					adapter.Fill( dtResult );

					// 取输出参数值
					GetParameterOut( cmdParms, cmd.Parameters );

					return dtResult;
				}

				#endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
			}
		}

		public static DataTable ExecuteQuery(FactoryArgs args, string cmdText, enQueryTransType transType )
		{
			return ExecuteQuery( args, cmdText, null, transType );
		}

		public static DataTable ExecuteQuery(FactoryArgs args, string cmdText )
		{
			return ExecuteQuery( args, cmdText, null );
		}

		public static DataTable ExecuteQuery(FactoryArgs args, string cmdText, LBDbParameterCollection cmdParms, enQueryTransType transType )
		{
			return ExecuteQuery( args, CommandType.Text, cmdText, cmdParms, transType );
		}

		public static DataTable ExecuteQuery(FactoryArgs args, string cmdText, LBDbParameterCollection cmdParms )
		{
			return ExecuteQuery( args, CommandType.Text, cmdText, cmdParms, enQueryTransType.ReadCommitted );
		}

        public static DataTable ExecuteQueryUnCommitted(FactoryArgs args, string cmdText, LBDbParameterCollection cmdParms)
        {
            return ExecuteQuery(args, CommandType.Text, cmdText, cmdParms, enQueryTransType.ReadUncommitted);
        }

        public static DataTable ExecuteQueryUnCommitted(FactoryArgs args, string cmdText)
        {
            return ExecuteQueryUnCommitted(args, cmdText, null);
        }
		#endregion--DataTable --

		#region--DataSet--

		private static DataSet ExecuteQuery4DataSet(FactoryArgs args, CommandType cmdType, string cmdText, LBDbParameterCollection cmdParms, enQueryTransType transType )
		{
			DataSet dsResult = null;

			if( args.DbConnection == null )
			{
				if( transType != enQueryTransType.Non )
				{
					#region -- 没有传入 DbConnection，需要事务 --

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						conn.Open();

						IsolationLevel level;
						if( transType == enQueryTransType.Snapshot && Provider.SnapshotSupported( args.DBName ) )
						{
							level = IsolationLevel.Snapshot;
						}
						else
						{
							level = IsolationLevel.ReadCommitted;
						}

						using( DbTransaction trans = conn.BeginTransaction( level ) )
						{
							using( DbCommand cmd = Provider.CreateCommand() )
							{
								try
								{
									// 准备参数
									PrepareCommand( cmd, conn, trans, cmdType, cmdText, cmdParms );

									// 执行
									dsResult = new DataSet();
									DbDataAdapter adapter = Provider.CreateDataAdapter();
									adapter.SelectCommand = cmd;
									adapter.Fill( dsResult );

									// 提交
									trans.Commit();

									// 取输出参数值
									GetParameterOut( cmdParms, cmd.Parameters );

									return dsResult;
								}
								catch( Exception ex )
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

					using( DbConnection conn = Provider.CreateConnection( args.DBName ) )
					{
						using( DbCommand cmd = Provider.CreateCommand() )
						{
							// 准备参数
							PrepareCommand( cmd, conn, null, cmdType, cmdText, cmdParms );

							// 执行
							dsResult = new DataSet();
							DbDataAdapter adapter = Provider.CreateDataAdapter();
							adapter.SelectCommand = cmd;
							adapter.Fill( dsResult );

							// 取输出参数值
							GetParameterOut( cmdParms, cmd.Parameters );

							return dsResult;
						}
					}

					#endregion -- 没有传入 DbConnection，不需要事务 --
				}
			}
			else if( args.DbTrans != null )
			{
				#region -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					// 准备参数
					PrepareCommand( cmd, args.DbConnection, args.DbTrans, cmdType, cmdText, cmdParms );

					// 执行
					dsResult = new DataSet();
					DbDataAdapter adapter = Provider.CreateDataAdapter();
					adapter.SelectCommand = cmd;
					adapter.Fill( dsResult );

					// 取输出参数值
					GetParameterOut( cmdParms, cmd.Parameters );

					return dsResult;
				}

				#endregion -- 同时传入 DbConnection, DbTrans：事务 Commit, Rollback 由外部控制 --
			}
			else if( transType != enQueryTransType.Non )
			{
				#region -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --

				throw new Exception( "当需要事务执行时，如果传入 DbConnection，必须同时传入 DbTransaction。" );

				#endregion -- 传入 DbConnection，没有传入 DbTrans，但需要事务：报错 --
			}
			else
			{
				#region -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --

				using( DbCommand cmd = Provider.CreateCommand() )
				{
					// 准备参数
					PrepareCommand( cmd, args.DbConnection, null, cmdType, cmdText, cmdParms );

					// 执行
					dsResult = new DataSet();
					DbDataAdapter adapter = Provider.CreateDataAdapter();
					adapter.SelectCommand = cmd;
					adapter.Fill( dsResult );

					// 取输出参数值
					GetParameterOut( cmdParms, cmd.Parameters );

					return dsResult;
				}

				#endregion -- 传入 DbConnection，没有传入 DbTrans，不需要事务 --
			}
		}


		//public static DataSet ExecuteQuery4DataSet( DALExecuteArgs args, string cmdText, enQueryTransType transType )
		//{
		//    return ExecuteQuery4DataSet( args, cmdText, null, transType );
		//}
		public static DataSet ExecuteQuery4DataSet(FactoryArgs args, string cmdText )
		{
			return ExecuteQuery4DataSet( args, cmdText, null, enQueryTransType.Non );
		}

		public static DataSet ExecuteQuery4DataSet(FactoryArgs args, string cmdText, LBDbParameterCollection cmdParms, enQueryTransType transType )
		{
			return ExecuteQuery4DataSet( args, CommandType.Text, cmdText, cmdParms, transType );
		}

		//public static DataSet ExecuteQuery4DataSet( DALExecuteArgs args, string cmdText, TSDbParameterCollection cmdParms )
		//{
		//    return ExecuteQuery4DataSet( args, CommandType.Text, cmdText, cmdParms, enQueryTransType.ReadCommitted );
		//}
		#endregion--DataSet-
		#endregion -- 查询 --

		#region -- 参数的处理 --

		private static void PrepareCommand( DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, LBDbParameterCollection cmdParms )
		{
			if( conn.State != ConnectionState.Open )
			{
				conn.Open();
			}

			cmd.Connection = conn;
			cmd.CommandText = cmdText;

			if( trans != null )
			{
				cmd.Transaction = trans;
			}

			cmd.CommandType = cmdType;
			if( cmdParms != null )
			{
				foreach( LBDbParameter parm in cmdParms )
				{
					if( parm != null )
					{
						cmd.Parameters.Add( Provider.CreateParameter( parm ) );
					}
				}
			}
		}

		private static void GetParameterOut( LBDbParameterCollection tsParms, System.Data.Common.DbParameterCollection dbParms )
		{
			if( tsParms != null && tsParms.Count > 0 )
			{
				foreach(LBDbParameter parm in tsParms )
				{
					if( parm.Direction != ParameterDirection.Input )
					{
						parm.Value = dbParms["@" + parm.ParameterName].Value;
					}
				}
			}
		}

        #endregion -- 参数的处理 --

        public static event GetBLLObjectMethod GetBLLObjectMethodEevent;
        public static IBLLFunction GetFunctionMethod(int iSPType)
        {
            IBLLFunction function = null;
            if (GetBLLObjectMethodEevent != null)
            {
                GetBLLObjectEventArgs args = new Base.Factory.GetBLLObjectEventArgs(iSPType);
                GetBLLObjectMethodEevent(args);
                function = args.BLLFunction;
            }
            return function;
        }

        public static event StopServerHandle StopServerEvent;
        public static void StopServer()
        {
            if (StopServerEvent != null)
            {
                StopServerEvent(null);
            }
        }
    }
}
