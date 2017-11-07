using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.SysConfig.SysConfig
{
    public class ExportSQLConfig
    {
        static StringBuilder StrBuilder = new StringBuilder();

        public static void ExportAction()
        {
            StringBuilder strBuildSQL;
            strBuildSQL = new StringBuilder();
            strBuildSQL.AppendLine("delete dbo.DbSystemConst");
            strBuildSQL.AppendLine("delete dbo.DbBillType");
            strBuildSQL.AppendLine("delete dbo.DbPermissionData");
            strBuildSQL.AppendLine("delete dbo.DbPermission");
            //strBuildSQL.AppendLine("delete dbo.DbReportType");
            strBuildSQL.AppendLine("delete dbo.DbSysConfigField");
            strBuildSQL.AppendLine("delete dbo.SysSPType");
            strBuildSQL.AppendLine("delete dbo.SysViewType");

            string strSQLOut = "";
            DataTable dtOutData;
            strSQLOut = GetInsertSQLCommon("SystemConstID", "DbSystemConst", true,true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("BillTypeID", "DbBillType", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("PermissionID", "DbPermission", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("PermissionDataID", "DbPermissionData", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("ReportTypeID", "DbReportType", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("SysConfigFieldID", "DbSysConfigField", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("SysSPTypeID", "SysSPType", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            strSQLOut = "";
            strSQLOut = GetInsertSQLCommon("SysViewTypeID", "SysViewType", true, true, out dtOutData);
            strBuildSQL.AppendLine(strSQLOut);

            CreateSQLFile(strBuildSQL.ToString(), "SQL更新");
            MessageBox.Show("导出成功！");
        }

        public static void ExportReportView()
        {
//            StringBuilder strBuildSQL;
//            strBuildSQL = new StringBuilder();
//            strBuildSQL.AppendLine(@"
//delete dbo.DbReportTemplate
//where ReportTemplateID In (
//    select ReportTemplateID
//    from dbo.DbReportView)
	
//delete dbo.DbReportViewField
//delete dbo.DbReportView
//");
//            strBuildSQL.AppendLine(@"");

//            StringBuilder strReturn = new StringBuilder();
//            string strInsertSQLOut = "";
//            DataTable dtOutData=GetTableData(@"
//                select *
//                from dbo.DbReportTemplate
//                where ReportTemplateID In(
//                    select ReportTemplateID
//                    from dbo.DbReportView) ", true);

//            string strSelectFieldNames = "";

//            foreach (DataColumn dc in dtOutData.Columns)
//            {
//                if (strSelectFieldNames != "")
//                    strSelectFieldNames += ",";
//                strSelectFieldNames += dc.ColumnName;
//            }

//            foreach (DataRow dr in dtOutData.Rows)
//            {
//                string strPKeyValue = dr["ReportTemplateID"].ToString().TrimEnd();
//                DataTable dtTemp;
//                GenDataSQL("select * from DbReportTemplate where ReportTemplateID= " + strPKeyValue, false, out strInsertSQLOut, out dtTemp);

//                strReturn.AppendLine(strInsertSQLOut);

//                if (bolIsNeedUpdate && strPKeyName != "")
//                {
//                    strReturn.AppendLine(@"else
//begin
//    update " + strTableName + @"
//    set " + strUpdateSQLOut + @"
//    where " + strPKeyName + " = " + strPKeyValue + @"
//end");
//                }
//            }

//            string strSQLOut = "";
//            DataTable dtOutData;
//            strSQLOut = GetInsertSQLCommon("ReportViewID", "DbReportView", true, true, out dtOutData);
//            strBuildSQL.AppendLine(strSQLOut);
        }

        private static bool CreateSQLFile(string strText, string strFileName)
        {
            try
            {
                string strPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\窗型资料";
                string strPathTable = strPath + @"\" + strFileName + ".sql";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (File.Exists(strPathTable))
                {
                    File.Delete(strPathTable);
                }
                try
                {
                    //操作表

                    System.IO.FileStream fileTable = new System.IO.FileStream(strPathTable, System.IO.FileMode.Create);
                    System.IO.StreamWriter writerTable = new System.IO.StreamWriter(fileTable, System.Text.Encoding.Default);

                    writerTable.Write(strText);
                    writerTable.Flush();
                    fileTable.Close();

                    //listView1.Invoke((MethodInvoker)delegate
                    //{
                    //    ListViewItem item = new ListViewItem(strFileName);
                    //    item.SubItems.Add(strPathTable);
                    //    item.SubItems.Add("生成成功！");
                    //    this.listView1.Items.Add(item);
                    //});
                    return true;
                }
                catch (Exception ex)
                {
                    //listView1.Invoke((MethodInvoker)delegate
                    //{
                    //    ListViewItem item = new ListViewItem(strFileName);
                    //    item.SubItems.Add(strPathTable);
                    //    item.SubItems.Add(ex.Message);
                    //    this.listView1.Items.Add(item);
                    //});
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region -- Connect --

        internal static DataTable GetTableData(string strLine, bool isTable)
        {
            string sql = strLine;
            if (isTable)
            {
                sql = "select * from " + strLine;
            }

            return LB.WinFunction.ExecuteSQL.CallDirectSQL(sql);
        }

        private static string GetInsertSQLCommon(string strPKeyName, string strTableName, bool bolIdentity, bool bolIsNeedUpdate, out DataTable dtOutData)
        {
            return GetInsertSQLInner(strPKeyName, strTableName, "", bolIdentity, bolIsNeedUpdate, out dtOutData);
        }

        private static string GetInsertSQLCondiction(string strPKeyName, string strTableName, string strWhere, bool bolIdentity, bool bolIsNeedUpdate, out DataTable dtOutData)
        {
            return GetInsertSQLInner(strPKeyName, strTableName, strWhere, bolIdentity, bolIsNeedUpdate, out dtOutData);
        }

        private static string GetInsertSQLInner(string strPKeyName, string strTableName, string strWhere, bool bolIdentity, bool bolIsNeedUpdate, out DataTable dtOutData)
        {
            StringBuilder strReturn = new StringBuilder();
            string strInsertSQLOut = "";
            string strUpdateSQLOut = "";
            GenDataSQL("select * from " + strTableName + " " + (strWhere == "" ? "" : "where " + strWhere), bolIdentity, out strInsertSQLOut, out dtOutData);

            string strSelectFieldNames = "";

            foreach (DataColumn dc in dtOutData.Columns)
            {

                if (strSelectFieldNames != "")
                    strSelectFieldNames += ",";
                strSelectFieldNames += dc.ColumnName;
            }

            foreach (DataRow dr in dtOutData.Rows)
            {
                string strPKeyValue = dr[strPKeyName].ToString().TrimEnd();
                DataTable dtTemp;
                GenDataSQL("select * from " + strTableName + " where " + strPKeyName + "= " + strPKeyValue, bolIdentity, out strInsertSQLOut, out dtTemp);
                if (bolIsNeedUpdate && strPKeyName != "")
                {
                    strUpdateSQLOut = "";
                    foreach (DataColumn dc in dtOutData.Columns)
                    {
                        if (!dc.ColumnName.ToUpper().Equals(strPKeyName.ToUpper()))
                        {
                            if (strUpdateSQLOut != "")
                                strUpdateSQLOut += ",\n";
                            if (dc.DataType == typeof(string))
                            {
                                if (dr[dc.ColumnName].ToString().TrimEnd().Contains("'"))
                                {
                                    dr[dc.ColumnName] = dr[dc.ColumnName].ToString().TrimEnd().Replace("'", "''");
                                }
                                strUpdateSQLOut += dc.ColumnName + "='" + dr[dc.ColumnName].ToString().TrimEnd() + "'";
                            }
                            else if (dc.DataType == typeof(byte[]))
                            {
                                byte[] strByte = dr[dc.ColumnName] as byte[];
                                StringBuilder sb = new StringBuilder();
                                if (strByte != null)
                                {
                                    int capacity = strByte.Length * 2;
                                    foreach (byte b in strByte)
                                    {
                                        sb.Append(b.ToString("X2"));
                                    }
                                    strUpdateSQLOut += dc.ColumnName + "=0x" + sb.ToString();
                                }
                                else
                                {
                                    strUpdateSQLOut += dc.ColumnName + "=null";
                                }

                            }
                            else
                            {
                                strUpdateSQLOut += dc.ColumnName + "=" + (dr[dc.ColumnName] == DBNull.Value ? "null" : dr[dc.ColumnName].ToString().TrimEnd());
                            }
                        }
                    }
                }

                if (bolIdentity)
                {
                    strReturn.AppendLine(@"
if not exists(select 1 from " + strTableName + @" where " + strPKeyName + " = " + strPKeyValue + @")
begin
    " + strInsertSQLOut + @"
end

");
                }
                else
                {
                    strReturn.AppendLine(strInsertSQLOut);
                }

                if (bolIsNeedUpdate && strPKeyName != "")
                {
                    strReturn.AppendLine(@"else
begin
    update " + strTableName + @"
    set " + strUpdateSQLOut + @"
    where " + strPKeyName + " = " + strPKeyValue + @"
end");
                }
            }
            return strReturn.ToString();
        }

        private static void GenDataSQL(string strLine, bool bolIdentity, out string strDataSQL, out DataTable dtOutData)
        {
            strDataSQL = "";
            StringBuilder sbSQL = new StringBuilder("");
            bool isTable = true;
            string strDB = "";
            string table = ParseTableName(strLine, out isTable, out strDB);

            // 是否需要添加语句： set identity_insert
            bool bNeedSet = false;
            string strIDSQL = "select syscolumns.name from sysobjects inner join syscolumns on sysobjects.id = syscolumns.id where sysobjects.name='{0}' and colstat=1 and sysobjects.xtype='U'";
            strIDSQL = string.Format(strIDSQL, table);
            //if (strDB != "")
            //{
            //    strIDSQL = "use " + strDB + ";" + strIDSQL;
            //}
            DataTable dtID = GetTableData(strIDSQL, false);
            string strKeyName = "";
            if (dtID.Rows.Count > 0)
            {
                bNeedSet = true;
                if (bolIdentity)
                {
                    string strContent = string.Format("set IDENTITY_INSERT {0} on", table);
                    sbSQL.AppendLine(strContent);
                }
                else
                {
                    strKeyName = dtID.Rows[0]["name"].ToString().TrimEnd();
                }
            }

            DataTable dt = GetTableData(strLine, isTable);
            dtOutData = dt;
            StrBuilder.AppendLine(table);
            string strColumns = "insert " + table + " (";
            string strColumnsSQL = "";
            for (int i = 0, j = dt.Columns.Count; i < j; i++)
            {
                if (!dt.Columns[i].ColumnName.Equals(strKeyName) || bolIdentity)
                {
                    if (strColumnsSQL != "")
                        strColumnsSQL += ",";
                    strColumnsSQL += dt.Columns[i].ColumnName;
                }

            }
            strColumns += strColumnsSQL;
            strColumns += " ) ";

            for (int i = 0, j = dt.Rows.Count; i < j; i++)
            {
                DataRow dr = dt.Rows[i];
                string strContent = strColumns + "values(";
                string strValueSQL = "";
                for (int x = 0, y = dt.Columns.Count; x < y; x++)
                {
                    Type typ = dt.Columns[x].DataType;
                    if (!dt.Columns[x].ColumnName.Equals(strKeyName) || bolIdentity)
                    {
                        bool bQuoted = false;
                        if (typ == typeof(string) || typ == typeof(DateTime))
                        {
                            bQuoted = true;
                        }
                        string strValue = "";
                        if (dr[dt.Columns[x].ColumnName] == DBNull.Value)
                        {
                            strValue = "null";
                            if (strValueSQL != "")
                                strValueSQL += ",";
                            strValueSQL += strValue;
                        }
                        else
                        {
                            if (dt.Columns[x].DataType == typeof(bool))
                            {
                                strValue = Convert.ToInt32(dr[dt.Columns[x].ColumnName]).ToString().TrimEnd();
                            }
                            else if (dt.Columns[x].DataType == typeof(byte[]))
                            {
                                byte[] strByte = dr[dt.Columns[x].ColumnName] as byte[];
                                int capacity = strByte.Length * 2;
                                StringBuilder sb = new StringBuilder();
                                if (strByte != null)
                                {
                                    foreach (byte b in strByte)
                                    {
                                        sb.Append(b.ToString("X2"));
                                    }
                                }
                                strValue = "0x" + sb.ToString();
                            }
                            else
                            {
                                strValue = dr[dt.Columns[x].ColumnName].ToString().TrimEnd();
                            }
                            if (bQuoted)
                            {
                                //strValue = strValue.Replace( "\\", "\\\\" );
                                //strValue = strValue.Replace( "\"", "\\\"" );
                                strValue = strValue.Replace("'", "''");
                            }
                            if (strValueSQL != "")
                                strValueSQL += ",";
                            strValueSQL += (bQuoted ? "'" : "") + strValue + (bQuoted ? "'" : "");
                        }
                    }
                }
                strContent += strValueSQL;
                strContent += ")";

                //System.Diagnostics.Debug.WriteLine( "InsertOneLine Start: " + DateTime.Now.ToString( "mm:ss.fff" ) );
                sbSQL.AppendLine(strContent);

                //System.Diagnostics.Debug.WriteLine( "InsertOneLine End: " + DateTime.Now.ToString( "mm:ss.fff" ) );

                System.Threading.Thread.Sleep(1);
            }

            if (bNeedSet && bolIdentity)
            {
                string strContent = string.Format("set IDENTITY_INSERT {0} off", table);
                sbSQL.AppendLine(strContent);
            }

            strDataSQL = sbSQL.ToString();
        }

        //private DataTable GetTableData(string strDB, string strLine, bool isTable)
        //{
        //    string sql = strLine;
        //    if (isTable)
        //    {
        //        sql = "select * from " + strLine;
        //    }

        //    string strConn = this.GetConnectString();
        //    strConn = string.Format(strConn, strDB);

        //    System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sql, strConn);
        //    DataTable dtSPParam = new DataTable();
        //    da.Fill(dtSPParam);

        //    return dtSPParam;
        //}

        private static string ParseTableName(string strLine, out bool isTable, out string strDB)
        {
            isTable = true;
            strDB = "";

            string table = "";
            strLine = strLine.ToLower().Trim();
            if (strLine.StartsWith("select"))
            {
                table = strLine.Substring(strLine.IndexOf("from") + 4);
                if (table.IndexOf("where") >= 0)
                {
                    table = table.Substring(0, table.IndexOf("where"));
                }
                table = table.Trim();
                isTable = false;
            }
            else
            {
                table = strLine;
                isTable = true;
            }

            if (table.IndexOf("dbo.") >= 0)
            {
                if (table.IndexOf("dbo.") > 0)
                {
                    strDB = table.Substring(0, table.IndexOf("dbo.") - 1);
                }

                table = table.Substring(table.IndexOf("dbo.") + 4);
            }

            return table;
        }

        #endregion
    }
}
