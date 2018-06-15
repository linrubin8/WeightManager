using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace LB.Controls.Args
{
	public class TSDataPageRetrieve
	{
		private DataTable mdtSource = null;
		private List<string> mlstPKeyFields = new List<string>();
		private int miRowsPerPage = 40;
		private TSRange mRange = new TSRange();
		private object mTag = null;
		private bool mbHasSumLine = false;
		private List<int> mlstRemovedRows = new List<int>();
		private List<string> mlstQueriedFields = new List<string>();

		public event TSGetDataEventHandler RetrieveSchemaDataEvent;
		public event TSGetOverviewDataEventHandler RetrieveOverviewDataEvent;
		public event TSGetDataEventHandler RetrieveAllDataEvent;
		public event TSGetPageDataEventHandler RetrievePageDataEvent;		// 在此事件中，可有错误产生。其它三个不可以
		public event TSGetDataMoreFieldsEventHandler RetrieveDataMoreFieldsEvent;

		public enum enPageStatus
		{
			Non,
			Partly,
			All
		}

		public int Page
		{
			get
			{
				return miRowsPerPage;
			}
		}

		public object Tag
		{
			get
			{
				return mTag;
			}
		}

		public bool HasSumLine
		{
			get
			{
				return mbHasSumLine;
			}
		}

		public TSDataPageRetrieve( params string[] pkeyFields )
			: this( 40, pkeyFields )
		{
		}

		public TSDataPageRetrieve( int rowsPerPage, params string[] pkeyFields )
		{
			if( pkeyFields.Length == 0 )
			{
				throw new Exception( "创建 TSDataPageRetrieve 必须提供至少一个 PKeyField。" );
			}

			mlstPKeyFields.AddRange( pkeyFields );

			miRowsPerPage = rowsPerPage;
		}

		public enPageStatus GetPageStatus()
		{
			int remainCount;
			List<Point> lstRemain = mRange.GetRemainRange( out remainCount );
			if( remainCount == mRange.MaxIndex + 1 )
			{
				return enPageStatus.Non;
			}
			else if( remainCount == 0 )
			{
				return enPageStatus.All;
			}
			else
			{
				return enPageStatus.Partly;
			}
		}

		public virtual DataTable RetrieveOverviewData( object tag )
		{
			mTag = tag;
			mbHasSumLine = false;
			mlstQueriedFields.Clear();

			TSGetDataEventArgs argsSchema = new TSGetDataEventArgs( tag );
			OnRetrieveSchemaData( argsSchema );
			DataTable dtSchema = argsSchema.ReturnTable;

			if( dtSchema != null )
			{
				TSGetOverviewDataEventArgs argsOverview = new TSGetOverviewDataEventArgs( dtSchema, tag );
				OnRetrieveOverviewData( argsOverview );
				DataTable dtOverview = argsOverview.ReturnTable;

				if( dtOverview != null )
				{
					for( int i = 0, j = dtSchema.Columns.Count; i < j; i++ )
					{
						string strColName = dtSchema.Columns[i].ColumnName;
						if( !dtOverview.Columns.Contains( strColName ) )
						{
							dtOverview.Columns.Add( strColName, dtSchema.Columns[i].DataType );
						}
					}

					for( int i = 0, j = dtSchema.Columns.Count; i < j; i++ )
					{
						string strColName = dtSchema.Columns[i].ColumnName;
						dtOverview.Columns[strColName].SetOrdinal( i );

						if( !mlstQueriedFields.Contains( strColName ) )
						{
							mlstQueriedFields.Add( strColName );
						}
					}

					mdtSource = dtOverview;
					mRange.ResetRange( mdtSource.Rows.Count - 1 );
					mlstRemovedRows.Clear();
				}
			}

			return mdtSource;
		}

		public void UpdateSourceTable( DataTable dtSource, bool hasSumLine )
		{
			mdtSource = dtSource;
			mbHasSumLine = hasSumLine;
			mRange.ResetRange( mdtSource.Rows.Count - ( mbHasSumLine ? 2 : 1 ) );
			mlstRemovedRows.Clear();
		}

		public virtual void RetrieveSelectedData( string selectFieldName, List<string> queryMoreFields )
		{
			#region -- 勾选的行，并而在已有数据中已存在的行，读取更多所需的字段 --

			bool bNeedQuery;
			string strMoreFields;
			List<string> lstMoreFields;
			GetQueryMoreFields( queryMoreFields, out bNeedQuery, out strMoreFields, out lstMoreFields );

			if( bNeedQuery )
			{
				string strMoreFieldsCriteria = GetSelectedDataMoreFieldsCriteria( selectFieldName );
				if( strMoreFieldsCriteria != "" )
				{
					TSGetDataMoreFieldsEventArgs args = new TSGetDataMoreFieldsEventArgs( mTag, strMoreFieldsCriteria, strMoreFields );
					OnRetrieveDataMoreFields( args );

					DataTable dtResult = args.ReturnTable;

					MergeToResult( 0, mdtSource.Rows.Count, dtResult );
				}
			}

			#endregion -- 勾选的行，并而在已有数据中已存在的行，读取更多所需的字段 --

			#region -- 勾选的行，在已有数据中仍不存在的行 --

			int needGetRowsCount;
			List<int> indexes;
			string strPageCriteria = GetSelectedDataCriteria( selectFieldName, out needGetRowsCount, out indexes );
			if( needGetRowsCount > 0  )
			{
				TSGetPageDataEventArgs args = new TSGetPageDataEventArgs( strPageCriteria, needGetRowsCount, mTag );
				this.OnRetrievePageData( args );

				DataTable dtPage = args.PageTable;

				if( dtPage == null )
				{
					mRange.AddIndexes( indexes );
					return;
				}

				MergeToResult( 0, mdtSource.Rows.Count, dtPage );
				mRange.AddIndexes( indexes );
			}

			#endregion -- 勾选的行，在已有数据中仍不存在的行 --
		}

		public virtual void RetrieveAllData( List<string> queryMoreFields )
		{
			int iRemainCount;
			List<Point> lstPoint = mRange.GetRemainRange( out iRemainCount );
			int iQueryAllRowCount = 200;

			#region -- 已有数据中已存在的行，读取更多所需的字段 --

			if( iRemainCount <= iQueryAllRowCount )
			{
				bool bNeedQuery;
				string strMoreFields;
				List<string> lstMoreFields;
				GetQueryMoreFields( queryMoreFields, out bNeedQuery, out strMoreFields, out lstMoreFields );

				if( bNeedQuery )
				{
					string strMoreFieldsCriteria = GetContainDataMoreFieldsCriteria();

					if( strMoreFieldsCriteria != "" )
					{
						TSGetDataMoreFieldsEventArgs args = new TSGetDataMoreFieldsEventArgs( mTag, strMoreFieldsCriteria, strMoreFields );
						OnRetrieveDataMoreFields( args );

						DataTable dtResult = args.ReturnTable;

						MergeToResult( 0, mdtSource.Rows.Count, dtResult );

						// 将本次新增的字段加入记录中，RetrieveAllData 才需要这样做
						if( lstMoreFields != null )
						{
							mlstQueriedFields.AddRange( lstMoreFields );
						}
					}
				}
			}

			#endregion -- 已有数据中已存在的行，读取更多所需的字段 --

			#region -- 在已有数据中仍不存在的行 --

			if( iRemainCount <= iQueryAllRowCount )
			{
				for( int i = 0, j = lstPoint.Count; i < j; i++ )
				{
					Point range = lstPoint[i];
					int iCount = range.Y - range.X + 1;
					if( iCount <= 0 )
					{
						continue;
					}

					string strPageCriteria = GetPageCriteria( range.X, iCount );

					TSGetPageDataEventArgs args = new TSGetPageDataEventArgs( strPageCriteria, iCount, mTag );
					this.OnRetrievePageData( args );

					DataTable dtPage = args.PageTable;

					if( dtPage == null )
					{
						mRange.AddRange( range.X, iCount );
						return;
					}

					MergeToResult( range.X, iCount, dtPage );
					mRange.AddRange( range.X, iCount );
				}
			}
			else
			{
				TSGetDataEventArgs args = new TSGetDataEventArgs( mTag );
				OnRetrieveAllData( args );
				DataTable dtResult = args.ReturnTable;

				// 如果有合计行，应该在 RetrieveAllDataEvent 事件中同时处理
				mdtSource.Clear();
				mdtSource.Merge( dtResult );
				mRange.ResetRange( mdtSource.Rows.Count - ( mbHasSumLine ? 2 : 1 ) );
				mRange.AddRange( 0, mRange.MaxIndex + 1 );
				mlstRemovedRows.Clear();
			}

			#endregion -- 在已有数据中仍不存在的行 --
		}

		private void GetQueryMoreFields( List<string> queryMoreFields, out bool bNeedQuery, out string strMoreFields, out List<string> lstMoreFields )
		{
			bNeedQuery = false;
			strMoreFields = "";
			lstMoreFields = null;

			if( queryMoreFields == null || queryMoreFields.Count == 0 )
			{
				return;
			}

			StringBuilder sbFields = new StringBuilder();
			bool first = true;
			queryMoreFields.Sort();
			for( int i = 0, j = queryMoreFields.Count; i < j; i++ )
			{
				string strTemp = queryMoreFields[i];
				int index = FindInList( mlstQueriedFields, strTemp );
				if( index >= 0 )
				{
					continue;
				}

				if( !first )
				{
					sbFields.Append( "," );
				}
				else
				{
					lstMoreFields = new List<string>();
				}
				sbFields.Append( strTemp );
				lstMoreFields.Add( strTemp );

				first = false;
				bNeedQuery = true;
			}

			if( bNeedQuery )
			{
				foreach( string strTemp in mlstPKeyFields )
				{
					int index = FindInList( lstMoreFields, strTemp );
					if( index >= 0 )
					{
						continue;
					}
					sbFields.Append( "," );
					sbFields.Append( strTemp );
				}

				strMoreFields = sbFields.ToString();
			}
		}

		public bool GetRemainRowsNeedRetrieveAll()
		{
			int iRemainCount;
			mRange.GetRemainRange( out iRemainCount );
			return iRemainCount > 200;
		}

		private void OnRetrieveDataMoreFields( TSGetDataMoreFieldsEventArgs e )
		{
			if( RetrieveDataMoreFieldsEvent != null )
			{
				RetrieveDataMoreFieldsEvent( this, e );
			}
		}

		private void OnRetrieveAllData( TSGetDataEventArgs e )
		{
			if( RetrieveAllDataEvent != null )
			{
				RetrieveAllDataEvent( this, e );
			}
		}

		private void OnRetrieveSchemaData( TSGetDataEventArgs e )
		{
			if( RetrieveSchemaDataEvent != null )
			{
				RetrieveSchemaDataEvent( this, e );
			}
		}

		private void OnRetrieveOverviewData( TSGetOverviewDataEventArgs e )
		{
			if( RetrieveOverviewDataEvent != null )
			{
				RetrieveOverviewDataEvent( this, e );
			}
		}

		private void OnRetrievePageData( TSGetPageDataEventArgs e )
		{
			if( RetrievePageDataEvent != null )
			{
				RetrievePageDataEvent( this, e );
			}
		}

		public virtual void RetrievePageData( int toRowIndex )
		{
			int needRows = miRowsPerPage;
			RetrievePageData( toRowIndex, needRows );
		}

		public virtual void RetrievePageData( int toRowIndex, int needRows )
		{
			int iFrom = toRowIndex;

			mRange.TryGetUnusedRange( ref iFrom, ref needRows );

			if( needRows <= 0 )
			{
				return;
			}

			string strPageCriteria = GetPageCriteria( iFrom, needRows );

			TSGetPageDataEventArgs args = new TSGetPageDataEventArgs( strPageCriteria, needRows, mTag );

			try
			{
				OnRetrievePageData( args );
			}
			catch( Exception ex )
			{
				CommonFuntion.OnDealError( this, ex );
			}

			DataTable dtPage = args.PageTable;

			if( dtPage == null )
			{
				mRange.AddRange( iFrom, needRows );
				return;
			}

			mRange.AddRange( iFrom, needRows );
			MergeToResult( iFrom, needRows, dtPage );
		}

		private void MergeToResult( int from, int needRows, DataTable dtPage )
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Start();

			// 将报表可能增加的列添加至 mdtSource
			for( int i = 0, j = dtPage.Columns.Count; i < j; i++ )
			{
				DataColumn col = dtPage.Columns[i];
				string strColName = col.ColumnName;
				if( !mdtSource.Columns.Contains( strColName ) )
				{
					mdtSource.Columns.Add( strColName, col.DataType );
				}
			}

			int iReturnCount = dtPage.Rows.Count;
			Dictionary<string, DataRow> dictRows = new Dictionary<string, DataRow>( iReturnCount );
			for( int i = 0; i < iReturnCount; i++ )
			{
				DataRow drNew = dtPage.Rows[i];

				string strKey = "";
				for( int x = 0, y = mlstPKeyFields.Count; x < y; x++ )
				{
					string strFieldName = mlstPKeyFields[x];
					strKey += ( x == 0 ? "" : "," ) + drNew[strFieldName].ToString().TrimEnd();
				}

				dictRows.Add( strKey, drNew );
			}

			watch.Stop();
			System.Diagnostics.Debug.WriteLine( "MergeToResult " + mdtSource.Columns.Count.ToString() + "-" + dtPage.Rows.Count.ToString() + " : " + watch.ElapsedMilliseconds.ToString() );
			watch.Start();

			for( int i = 0; i < needRows; i++ )
			{
				int iOrgIndex = from + i;
				DataRow drOrg = mdtSource.Rows[iOrgIndex];
				string strKey = "";
				for( int m = 0, n = mlstPKeyFields.Count; m < n; m++ )
				{
					string strFieldName = mlstPKeyFields[m];
					strKey += ( m == 0 ? "" : "," ) + drOrg[strFieldName].ToString().TrimEnd();
				}

				if( dictRows.ContainsKey( strKey ) )
				{
					//drOrg.ItemArray = dictRows[strKey].ItemArray;
					DataRowState stateOrg = drOrg.RowState;
					try
					{
						drOrg.BeginEdit();
						for( int x = 0, y = mdtSource.Columns.Count; x < y; x++ )
						{
							string strColName = mdtSource.Columns[x].ColumnName;
							if( !dtPage.Columns.Contains( strColName ) )
							{
								continue;
							}

							drOrg[x] = dictRows[strKey][strColName];
						}
					}
					finally
					{
						try
						{
							drOrg.EndEdit();
						}
						catch
						{
						}
					}
					dictRows.Remove( strKey );

					if( stateOrg == DataRowState.Unchanged )
					{
						drOrg.AcceptChanges();
					}
				}
				else
				{
					mlstRemovedRows.Add( iOrgIndex );
				}
			}

			watch.Stop();
			System.Diagnostics.Debug.WriteLine( "MergeToResult " + mdtSource.Columns.Count.ToString() + "-" + dtPage.Rows.Count.ToString() + " : " + watch.ElapsedMilliseconds.ToString() );
		}

		public bool IsRowRemoved( int index )
		{
			return mlstRemovedRows.Contains( index );
		}

		protected virtual string GetPageCriteria( int toRowIndex, int iLen )
		{
			if( iLen <= 0 )
			{
				return "";
			}

			if( toRowIndex < 0 || toRowIndex + 1 > mdtSource.Rows.Count )
			{
				throw new Exception( "指定跳转的行号超出数据源总行数。" );
			}

			int iLastIndex = toRowIndex + iLen - 1;
			if( iLastIndex + 1 > mdtSource.Rows.Count )
			{
				iLastIndex = mdtSource.Rows.Count - 1;
			}

			StringBuilder sbCriteria = new StringBuilder();
			if( mlstPKeyFields.Count == 1 )		// 只有一个关键字段时，用 in 的写法
			{
				string strFieldName = mlstPKeyFields[0];
				sbCriteria.Append( strFieldName + " in (" );
				for( int i = toRowIndex; i <= iLastIndex; i++ )
				{
					if( i > toRowIndex )
					{
						sbCriteria.Append( "," );
					}

					DataRow dr = mdtSource.Rows[i];

					object objValue = dr[strFieldName];
					if( objValue == DBNull.Value )
					{
						throw new Exception( "检索数据的关键字段的值不为空。" );
					}
					else
					{
						bool bIsQuoted;
						bool bIsTime;
						DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
						if( bIsTime )
						{
							sbCriteria.Append( "'" + Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) + "'" );
						}
						else if( bIsQuoted )
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( "'" + strValue + "'" );
						}
						else
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( strValue );
						}
					}
				}
				sbCriteria.Append( ")" );
			}
			else	// 多个关键字段时，用 and 与 or 组合
			{
				for( int i = toRowIndex; i <= iLastIndex; i++ )
				{
					if( i > toRowIndex )
					{
						sbCriteria.Append( " or " );
					}

					DataRow dr = mdtSource.Rows[i];
					string strLine = "";
					foreach( string strFieldName in mlstPKeyFields )
					{
						if( strLine != "" )
						{
							strLine += " and ";
						}

						object objValue = dr[strFieldName];
						if( objValue == DBNull.Value )
						{
							strLine += string.Format( "{0} is null", strFieldName );
						}
						else
						{
							bool bIsQuoted;
							bool bIsTime;
							DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
							if( bIsTime )
							{
								strLine += string.Format( "{0}='{1}'", strFieldName, Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
							}
							else if( bIsQuoted )
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}='{1}'", strFieldName, strValue );
							}
							else
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}={1}", strFieldName, strValue );
							}
						}
					}

					sbCriteria.Append( "(" + strLine + ")" );
				}
			}

			return sbCriteria.ToString();
		}

		protected virtual string GetContainDataMoreFieldsCriteria()
		{
			List<Point> lstPoint = mRange.GetContainRange();
			StringBuilder sbCriteria = new StringBuilder();
			if( mlstPKeyFields.Count == 1 )		// 只有一个关键字段时，用 in 的写法
			{
				string strFieldName = mlstPKeyFields[0];
				sbCriteria.Append( strFieldName + " in (" );
				bool first = true;
				for( int x = 0; x < lstPoint.Count; x++ )
				{
					Point point = lstPoint[x];

					for( int i = point.X; i <= point.Y; i++ )
					{
						if( !first )
						{
							sbCriteria.Append( "," );
						}

						DataRow dr = mdtSource.Rows[i];

						object objValue = dr[strFieldName];
						if( objValue == DBNull.Value )
						{
							throw new Exception( "检索数据的关键字段的值不可为空。" );
						}
						else
						{
							bool bIsQuoted;
							bool bIsTime;
							DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
							if( bIsTime )
							{
								sbCriteria.Append( "'" + Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) + "'" );
							}
							else if( bIsQuoted )
							{
								string strValue = objValue.ToString().TrimEnd();
								sbCriteria.Append( "'" + strValue + "'" );
							}
							else
							{
								string strValue = objValue.ToString().TrimEnd();
								sbCriteria.Append( strValue );
							}
						}

						first = false;
					}
				}
				sbCriteria.Append( ")" );
			}
			else	// 多个关键字段时，用 and 与 or 组合
			{
				bool first = true;
				for( int x = 0; x < lstPoint.Count; x++ )
				{
					Point point = lstPoint[x];

					for( int i = point.X; i <= point.Y; i++ )
					{
						if( !first )
						{
							sbCriteria.Append( " or " );
						}

						DataRow dr = mdtSource.Rows[i];
						string strLine = "";
						foreach( string strFieldName in mlstPKeyFields )
						{
							if( strLine != "" )
							{
								strLine += " and ";
							}

							object objValue = dr[strFieldName];
							if( objValue == DBNull.Value )
							{
								strLine += string.Format( "{0} is null", strFieldName );
							}
							else
							{
								bool bIsQuoted;
								bool bIsTime;
								DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
								if( bIsTime )
								{
									strLine += string.Format( "{0}='{1}'", strFieldName, Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
								}
								else if( bIsQuoted )
								{
									string strValue = objValue.ToString().TrimEnd();
									strLine += string.Format( "{0}='{1}'", strFieldName, strValue );
								}
								else
								{
									string strValue = objValue.ToString().TrimEnd();
									strLine += string.Format( "{0}={1}", strFieldName, strValue );
								}
							}
						}

						sbCriteria.Append( "(" + strLine + ")" );
						first = false;
					}
				}
			}

			return sbCriteria.ToString();
		}

		protected virtual string GetSelectedDataCriteria( string selectFieldName, out int needGetRowsCount, out List<int> indexes )
		{
			indexes = new List<int>();
			needGetRowsCount = 0;

			if( !mdtSource.Columns.Contains( selectFieldName ) )
			{
				return "(1=0)";		// 不读任何数据
			}

			StringBuilder sbCriteria = new StringBuilder();
			if( mlstPKeyFields.Count == 1 )		// 只有一个关键字段时，用 in 的写法
			{
				string strFieldName = mlstPKeyFields[0];
				sbCriteria.Append( strFieldName + " in (" );
				for( int i = 0; i < mdtSource.Rows.Count; i++ )
				{
					if( mRange.ContainsIndex( i ) || i > mRange.MaxIndex )
					{
						continue;
					}

					DataRow dr = mdtSource.Rows[i];
					bool bChecked = false;
					if( dr[selectFieldName] != DBNull.Value )
					{
						bChecked = Convert.ToBoolean( Convert.ToInt32( dr[selectFieldName] ) );
					}
					if( !bChecked )
					{
						continue;
					}

					object objValue = dr[strFieldName];
					if( objValue == DBNull.Value )
					{
						throw new Exception( "检索数据的关键字段的值不可为空。" );
					}
					else
					{
						bool bIsQuoted;
						bool bIsTime;
						DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
						if( bIsTime )
						{
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + "'" + Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) + "'" );
						}
						else if( bIsQuoted )
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + "'" + strValue + "'" );
						}
						else
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + strValue );
						}
					}

					needGetRowsCount++;
					indexes.Add( i );
				}
				sbCriteria.Append( ")" );
			}
			else	// 多个关键字段时，用 and 与 or 组合
			{
				bool bAddOr = false;
				for( int i = 0; i < mdtSource.Rows.Count; i++ )
				{
					if( mRange.ContainsIndex( i ) || i > mRange.MaxIndex )
					{
						continue;
					}

					DataRow dr = mdtSource.Rows[i];
					bool bChecked = false;
					if( dr[selectFieldName] != DBNull.Value )
					{
						bChecked = Convert.ToBoolean( Convert.ToInt32( dr[selectFieldName] ) );
					}
					if( !bChecked )
					{
						continue;
					}

					if( bAddOr )
					{
						sbCriteria.Append( " or " );
					}

					string strLine = "";
					foreach( string strFieldName in mlstPKeyFields )
					{
						if( strLine != "" )
						{
							strLine += " and ";
						}

						object objValue = dr[strFieldName];
						if( objValue == DBNull.Value )
						{
							strLine += string.Format( "{0} is null", strFieldName );
						}
						else
						{
							bool bIsQuoted;
							bool bIsTime;
							DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
							if( bIsTime )
							{
								strLine += string.Format( "{0}='{1}'", strFieldName, Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
							}
							else if( bIsQuoted )
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}='{1}'", strFieldName, strValue );
							}
							else
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}={1}", strFieldName, strValue );
							}
						}
					}

					sbCriteria.Append( "(" + strLine + ")" );
					bAddOr = true;
					needGetRowsCount++;
					indexes.Add( i );
				}
			}

			string strCriteria = sbCriteria.ToString();
			if( strCriteria == "" )
			{
				return "(1=0)";		// 不读任何数据
			}
			else
			{
				return sbCriteria.ToString();
			}
		}

		protected virtual string GetSelectedDataMoreFieldsCriteria( string selectFieldName )
		{
			if( !mdtSource.Columns.Contains( selectFieldName ) )
			{
				return "(1=0)";		// 不读任何数据
			}

			int needGetRowsCount = 0;
			StringBuilder sbCriteria = new StringBuilder();
			if( mlstPKeyFields.Count == 1 )		// 只有一个关键字段时，用 in 的写法
			{
				string strFieldName = mlstPKeyFields[0];
				sbCriteria.Append( strFieldName + " in (" );
				for( int i = 0; i < mdtSource.Rows.Count; i++ )
				{
					if( !mRange.ContainsIndex( i ) )
					{
						continue;
					}

					DataRow dr = mdtSource.Rows[i];
					bool bChecked = false;
					if( dr[selectFieldName] != DBNull.Value )
					{
						bChecked = Convert.ToBoolean( Convert.ToInt32( dr[selectFieldName] ) );
					}
					if( !bChecked )
					{
						continue;
					}

					object objValue = dr[strFieldName];
					if( objValue == DBNull.Value )
					{
						throw new Exception( "检索数据的关键字段的值不可为空。" );
					}
					else
					{
						bool bIsQuoted;
						bool bIsTime;
						DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
						if( bIsTime )
						{
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + "'" + Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) + "'" );
						}
						else if( bIsQuoted )
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + "'" + strValue + "'" );
						}
						else
						{
							string strValue = objValue.ToString().TrimEnd();
							sbCriteria.Append( ( needGetRowsCount > 0 ? "," : "" ) + strValue );
						}
					}

					needGetRowsCount++;
				}
				sbCriteria.Append( ")" );
			}
			else	// 多个关键字段时，用 and 与 or 组合
			{
				bool bAddOr = false;
				for( int i = 0; i < mdtSource.Rows.Count; i++ )
				{
					if( !mRange.ContainsIndex( i ) )
					{
						continue;
					}

					DataRow dr = mdtSource.Rows[i];
					bool bChecked = false;
					if( dr[selectFieldName] != DBNull.Value )
					{
						bChecked = Convert.ToBoolean( Convert.ToInt32( dr[selectFieldName] ) );
					}
					if( !bChecked )
					{
						continue;
					}

					if( bAddOr )
					{
						sbCriteria.Append( " or " );
					}

					string strLine = "";
					foreach( string strFieldName in mlstPKeyFields )
					{
						if( strLine != "" )
						{
							strLine += " and ";
						}

						object objValue = dr[strFieldName];
						if( objValue == DBNull.Value )
						{
							strLine += string.Format( "{0} is null", strFieldName );
						}
						else
						{
							bool bIsQuoted;
							bool bIsTime;
							DataTypeNeedQuoted( mdtSource.Columns[strFieldName].DataType, out bIsQuoted, out bIsTime );
							if( bIsTime )
							{
								strLine += string.Format( "{0}='{1}'", strFieldName, Convert.ToDateTime( objValue ).ToString( "yyyy-MM-dd HH:mm:ss.fff" ) );
							}
							else if( bIsQuoted )
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}='{1}'", strFieldName, strValue );
							}
							else
							{
								string strValue = objValue.ToString().TrimEnd();
								strLine += string.Format( "{0}={1}", strFieldName, strValue );
							}
						}
					}

					sbCriteria.Append( "(" + strLine + ")" );
					bAddOr = true;
					needGetRowsCount++;
				}
			}

			string strCriteria = sbCriteria.ToString();
			if( strCriteria == "" )
			{
				return "(1=0)";		// 不读任何数据
			}
			else
			{
				return sbCriteria.ToString();
			}
		}

		private void DataTypeNeedQuoted( Type type, out bool bIsQuoted, out bool bIsTime )
		{
			bIsQuoted = false;
			bIsTime = false;

			if( type == typeof( int ) || type == typeof( long ) || type == typeof( byte ) || type == typeof( short ) || type == typeof( decimal ) )
			{
				bIsQuoted = false;
				bIsTime = false;
			}
			else if( type == typeof( string ) )
			{
				bIsQuoted = true;
				bIsTime = false;
			}
			else if( type == typeof( DateTime ) )
			{
				bIsQuoted = true;
				bIsTime = true;
			}
			else
			{
				bIsQuoted = false;
				bIsTime = false;
			}
		}

		private int FindInList( List<string> lstFields, string strFieldName )
		{
			if( lstFields == null )
			{
				return -1;
			}

			int index = -1;
			for( int i = 0, j = lstFields.Count; i < j; i++ )
			{
				string temp = lstFields[i].Trim();
				if( temp.Equals( strFieldName.Trim(), StringComparison.CurrentCultureIgnoreCase ) )
				{
					index = i;
					break;
				}
			}
			return index;
		}
	}
}
