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
    /// TSMaxUploadMaxFileAndResultImage 的摘要说明
    /// </summary>
    public class TSMaxUploadMaxFileAndResultImage : IHttpHandler
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

                        bool isFail = false;
                        bool.TryParse( Robj["VrayFail"].ToString(), out isFail );

                        DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                        string strVrayID = Robj["VrayID"].ToString();
                        long VrayID = -1;
                        long.TryParse( strVrayID, out VrayID );

                        if( isFail )
                        {
                            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                            con.Open();
                            tran = con.BeginTransaction();

                            string strInsertSQL = @"update [TSMobileProduct].[dbo].[MaxMaster] set Info = @Info where VrayID = @VrayID";
                            
                            DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,"9:渲染失败")
                                  };

                            DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm );  //更新主表失败状态

                            args.DbTrans.Commit();

                            if( con != null )
                            {
                                con.Close();
                                args = null;
                                tran = null;
                            }
                            return;
                        }

                        bool boolIsUploadFile = false;
                        bool.TryParse( Robj["IsUploadFile"].ToString(), out boolIsUploadFile );
                        bool boolIsMaxFile = false;
                        bool.TryParse( Robj["IsMaxFile"].ToString(), out boolIsMaxFile );
                        args = new DALExecuteArgs( "", "", con, tran );

                        int group = Helper.ComputeGroup();

                        if( boolIsUploadFile )  //上传文件
                        {
                            string strFileBytes = Robj["Bytes"].ToString();
                            string strFileMD5 = Robj["MD5"].ToString();
                            string strFileName = Robj["FileSafeName"].ToString();

                            byte[] Filebytes = StringToBytes( strFileBytes );

                            string strMaxDrawFile = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", group.ToString() );  //文件存放目录

                            if( !Directory.Exists( strMaxDrawFile ) )
                            {
                                Directory.CreateDirectory( strMaxDrawFile );
                            }

                            //strFileName = IfSameFileNameAutoRename( strMaxDrawFile + "\\" + strFileName );  //同名文件自动重命名

                            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                            con.Open();
                            tran = con.BeginTransaction();


                            string strInsertSQL = @"insert into dbo.MaxFiles(MD5) values (@MD5) select @@identity";  //返回文件表ID

                            args = new DALExecuteArgs( "", "", con, tran );

                            DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,strFileMD5),
                                     //DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strMaxDrawFile + "\\" + longFileID.ToString() ),
                                  };

                            DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );

                            long MD5ID = -1;

                            if( dt != null )
                            {
                                long.TryParse( dt.Rows[0][0].ToString(), out MD5ID );
                            }


                            args.DbTrans.Commit();

                            if( con != null )
                            {
                                con.Close();
                                args = null;
                                tran = null;
                            }

                            File.WriteAllBytes( strMaxDrawFile + "\\" + MD5ID.ToString(), Filebytes );  //写入文件

                            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                            con.Open();
                            tran = con.BeginTransaction();

                            strInsertSQL = @"update dbo.MaxFiles set FilePath = @FilePath where FileID = @FileID";

                            args = new DALExecuteArgs( "", "", con, tran );

                            DbParameter[] parm2 = {
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strMaxDrawFile + "\\" + MD5ID.ToString() ),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,MD5ID ),
                                  };

                            DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm2 );
                            
                            args.DbTrans.Commit();

                            if( con != null )
                            {
                                con.Close();
                                args = null;
                                tran = null;
                            }

                            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                            con.Open();
                            tran = con.BeginTransaction();

                            strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType)
                                             update [TSMobileProduct].[dbo].[MaxMaster] set Info = @Info where VrayID = @VrayID";

                            args = new DALExecuteArgs( "", "", con, tran );

                            int fileType = -1;
                            string info = "";
                            if( boolIsMaxFile )
                            {
                                fileType = 6;
                                info = "2:已开始";
                            }
                            else
                            {
                                fileType = 7;
                                info = "3:已完成";
                            }

                            DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,MD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,strFileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,fileType),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,info)
                                  };

                            DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm3 );  //将对应MD5的ID插入关联表，更新主表状态

                            args.DbTrans.Commit();

                            if( con != null )
                            {
                                con.Close();
                                args = null;
                                tran = null;
                            }
                            strReturnJson = "{\"Message\":\"\",\"NeedUploadFile\":\"false\"}";
                        }
                        else  //上传MD5
                        {
                            string strFileMD5 = Robj["MD5"].ToString();

                            string strSelectSQL = "select * from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = '" + strFileMD5 + "'";

                            DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSelectSQL, null );  //先查是否存在相同的MD5

                            long sameMD5ID = 0;

                            if( dtExists.Rows.Count > 0 )  //存在
                            {
                                sameMD5ID = Convert.ToInt64( dtExists.Rows[0]["FileID"] );
                                string strFileName = Robj["FileSafeName"].ToString();

                                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                                con.Open();
                                tran = con.BeginTransaction();

                                string strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType)
                                                        update [TSMobileProduct].[dbo].[MaxMaster] set Info = @info where VrayID = @VrayID";

                                args = new DALExecuteArgs( "", "", con, tran );

                                int fileType = -1;
                                string info = "";
                                if( boolIsMaxFile )
                                {
                                    fileType = 6;
                                    info = "2:已开始";
                                }
                                else
                                {
                                    fileType = 7;
                                    info = "3:已完成";
                                }

                                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,sameMD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,strFileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,fileType),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,info)
                                  };

                                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm );  //将对应MD5的ID插入关联表，更新主表状态

                                args.DbTrans.Commit();

                                if( con != null )
                                {
                                    con.Close();
                                    args = null;
                                    tran = null;
                                }
                                strReturnJson = "{\"Message\":\"\",\"NeedUploadFile\":\"false\"}";
                            }
                            else  //不存在
                            {
                                strReturnJson = "{\"Message\":\"\",\"NeedUploadFile\":\"true\"}";  //直接返回需要上传3DSMAX文件的信息
                            }
                        }
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

        private void updateDB( DbConnection con, DbTransaction tran, long FileID, string strFilePath, long fileID, bool isUpdateFileTable )
        {
            string strUpdateSQL = "";

            con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
            con.Open();
            tran = con.BeginTransaction();

            DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

            if( isUpdateFileTable )
            {
                strUpdateSQL = @"update [TSMobileProduct].[dbo].[MaxFiles] set FilePath = @FilePath where FileID = @FileID";

                args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,FileID),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strFilePath),
                                  };

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strUpdateSQL, true, parm );
            }
            else
            {
                strUpdateSQL = @"update [TSMobileProduct].[dbo].[MaxUsedFiles] set FilePath = @FilePath where FileID = @FileID";

                args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,FileID),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strFilePath),
                                  };

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strUpdateSQL, true, parm );
            }

            args.DbTrans.Commit();
        }

        public static byte[] StringToBytes( string strHexString )
        {
            strHexString = strHexString.Replace( " ", "" );
            byte[] buffer = new byte[strHexString.Length / 2];
            for( int i = 0; i < strHexString.Length; i += 2 )
            {
                buffer[i / 2] = (byte)Convert.ToByte( strHexString.Substring( i, 2 ), 16 );
            }
            return buffer;
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