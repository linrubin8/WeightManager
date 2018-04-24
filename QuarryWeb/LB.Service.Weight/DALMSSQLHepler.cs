using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    public class DALMSSQLHepler
    {
        public static void LogHelper(string filename,string strMsg)
        {
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = @"
                        insert dbo.Z_ProgramError
( MethodName, ErrorInfo, ErrorType, CreateTime, InterviewIP)
values( @MethodName, @ErrorInfo, @ErrorType, getdate(), null)";

                    DbParameter[] parm = {
                                     DBHelperSQL .CreateInDbParameter("@MethodName",DbType.String ,filename),
                                     DBHelperSQL .CreateInDbParameter("@ErrorInfo",DbType.String ,strMsg),
                                     DBHelperSQL .CreateInDbParameter("@ErrorType",DbType.Decimal ,0)
                                  };
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, parm );

                tran.Commit();
            }
            catch( Exception ex )
            {
                tran.Rollback();
                strMsg = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}