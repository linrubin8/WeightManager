using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    /// <summary>
    /// TSMaxUploadColorCard 的摘要说明
    /// </summary>
    public class TSMaxUploadColorCard : IHttpHandler
    {
        private string strReturnJson = "";

        public void ProcessRequest( HttpContext context )
        {
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability( HttpCacheability.NoCache );
            using( var reader = new StreamReader( context.Request.InputStream ) )
            {
                string xmlData = reader.ReadToEnd();
                if( !string.IsNullOrEmpty( xmlData ) )
                {
                    DbConnection con = null;
                    DbTransaction tran = null;
                    try
                    {
                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();
                        JObject Robj = JObject.Parse( xmlData );
                        long longFileID = -1;
                        long.TryParse( Robj["FileID"].ToString(), out longFileID );
                        string strImageCardName = Robj["ColorCardName"].ToString();
                        string strFileBytes = Robj["strColorCardBytes"].ToString();
                        string UserID = Robj["UserID"].ToString();

                        int group = Helper.ComputeGroup();

                        string strMaxDrawFile = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", group.ToString() );  //色卡存放目录
                        if( !Directory.Exists(strMaxDrawFile) )
                        {
                            Directory.CreateDirectory( strMaxDrawFile );
                        }
                        byte[] fileBytes = Helper.StringToBytes( strFileBytes );
                        File.WriteAllBytes( strMaxDrawFile + "\\" + longFileID, fileBytes );

                        updateDB( con, tran, longFileID, strMaxDrawFile + "\\" + longFileID );
                        strReturnJson = "{\"Message\":\"\"}";
                    }
                    catch( Exception ex )
                    {
                        strReturnJson = "{\"Message\":\"" + ex.Message + "\"}";
                        if( tran != null )
                        {
                            tran.Rollback();
                        }
                    }
                    finally
                    {
                        if( con != null )
                        {
                            con.Close();
                        }
                        context.Response.Write( strReturnJson );
                    }
                }
            }
        }

        private void updateDB( DbConnection con, DbTransaction tran, long FileID, string strFilePath )
        {
            string strUpdateSQL = "";
            
            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
            con.Open();
            tran = con.BeginTransaction();
            

            strUpdateSQL = @"update [TSMobileProduct].[dbo].[MaxFiles] set FilePath = @FilePath where FileID = @FileID";

            DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

            args = new DALExecuteArgs( "", "", con, tran );

            DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,FileID),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strFilePath),
                                  };

            DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strUpdateSQL, true, parm );

            args.DbTrans.Commit();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}